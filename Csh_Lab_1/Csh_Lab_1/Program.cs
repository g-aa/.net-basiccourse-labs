using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Csh_Lab_1
{
    class Program
    {
        private const string sDef = "\nУказанно не верное значение - '{0}'"; 
        static void Main(string[] args)
        {
            Type t = null;
            string[] sMenu = { "1 – Общая информация по типам", "2 – Выбрать из списка", "3 – Ввести имя типа", "4 – Параметры консоли", "0 - Выход из программы" };

            while (true)
            {
                Console.WriteLine("Информация по типам:\n");
                Array.ForEach(sMenu, s => { Console.WriteLine("\t{0}", s); });
                char ch = char.ToLower(Console.ReadKey(true).KeyChar);
                switch (ch)
                {
                    case '1' :
                        ShowAllTypeInfo();
                        //ShowTypeInformation(t);
                        break;
                    case '2' :
                        SelectType(ref t);
                        break;
                    case '3' :
                        break;
                    case '4':
                        break;
                    case '0' :
                        return;
                    default:
                        Console.WriteLine(sDef, ch);
                        break;
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Полная информация типов в сборке
        /// </summary>
        public static void ShowAllTypeInfo()
        {
            Assembly myAsm = Assembly.GetExecutingAssembly();
            Type[] thisAssemblyTypes = myAsm.GetTypes();

            Assembly[] refAssemblies = AppDomain.CurrentDomain.GetAssemblies();
            List<Type> types = new List<Type>();
            
            foreach (Assembly asm in refAssemblies)
                types.AddRange(asm.GetTypes());

            int nRTypes = 0;
            int nVTypes = 0; 
            int nITypes = 0;
            Type tMaxMethods = types[0];
            MethodInfo mLongName = types[0].GetMethods()[0];
            MethodInfo mCountParam = types[0].GetMethods()[0];

            foreach (var t in types)
            {
                // подсчет типов по видам:
                if (t.IsClass)
                    nRTypes++;
                else if (t.IsValueType)
                    nVTypes++;
                else if (t.IsInterface)
                    nITypes++;

                // поиск типа с самым большим числом методов:
                if (tMaxMethods.GetMethods().Length < t.GetMethods().Length)
                    tMaxMethods = t;

                foreach (var m in t.GetMethods())
                {
                    // поиск метода с самым длинным именем:                   
                    if (mLongName.Name.Length < m.Name.Length)
                        mLongName = m;

                    // поиск метода с самым большим числом параметров:
                    if (mCountParam.GetParameters().Length < m.GetParameters().Length)
                        mCountParam = m;
                }
            }

            // вывод параметров в консоль:
            string fInfoFormat = "\t{0,-50}{1}";
            Console.WriteLine("\nОбщая информация по типам:\n");
            Console.WriteLine(fInfoFormat, "Подключенные сборки:", refAssemblies.Length);
            Console.WriteLine(fInfoFormat, "Всего типов по всем подключенным сборкам:", types.Count);
            Console.WriteLine(fInfoFormat, "Ссылочные типы:", nRTypes);
            Console.WriteLine(fInfoFormat, "Значимые типы:", nVTypes);
            Console.WriteLine(fInfoFormat, "Типы-интерфейсы:", nITypes);
            Console.WriteLine(fInfoFormat, "Тип с максимальным числом методов:", tMaxMethods.FullName);
            Console.WriteLine(fInfoFormat, "Самое длинное название метода:", mLongName.GetType().FullName + "." + mLongName.Name);
            Console.WriteLine(fInfoFormat, "Метод с наибольшим числом аргументов:", mCountParam.GetType().FullName + "." + mCountParam.Name);
            Console.WriteLine("\nНажмите любую клавишу, чтобы вернуться в главное меню");
            Console.ReadKey();
        }



        /// <summary> 
        /// Выбрать тип из списта 
        /// </summary>
        /// <param name="t"> Ссылка на тип </param>
        public static void SelectType(ref Type t) 
        {
            Type temp = t;
            string[] sTypeMenu = { "1 – uint", "2 – int", "3 – long", "4 – float", "5 – double", "6 – char", "7 - string", "8 – MyClass", "9 – MyStruct", "0 – Выход в главное меню"};

            Console.WriteLine("Выберете тип из списка:");
            Array.ForEach(sTypeMenu, s => { Console.WriteLine("\t{0}", s); });
            while (true)
            {
                char ch = char.ToLower(Console.ReadKey(true).KeyChar);
                switch (ch)
                {
                    case '1':
                        t = typeof(uint);
                        return;
                    case '2':
                        t = typeof(int);
                        return;
                    case '3':
                        t = typeof(long);
                        return;
                    case '4':
                        t = typeof(float);
                        return;
                    case '5':
                        t = typeof(double);
                        return;
                    case '6':
                        t = typeof(char);
                        return;
                    case '7':
                        t = typeof(string);
                        return;
                    case '8':
                        return;
                    case '9':
                        return;
                    case '0':
                        t = temp;
                        return;
                    default:
                        Console.WriteLine("Указанно не верное значение - '{0}'", ch);
                        break;
                }
            }
        }

        /// <summary>
        /// Вывод общей информации по типу
        /// </summary>
        /// <param name="type"> Входной параметр типа </param>
        static void ShowTypeInformation(Type type) 
        {
            string fInfoFormat = "\t{0,-30}{1}";
            Console.WriteLine("{0,-30}{1}\n", "Информация по типу:", type.FullName);
            Console.WriteLine(fInfoFormat, "Значимый тип:", (type.IsValueType != false) ? "+" : "-");
            Console.WriteLine(fInfoFormat, "Пространство имен:", type.Namespace);
            Console.WriteLine(fInfoFormat, "Сборка:", type.Assembly.GetName().Name);
            Console.WriteLine(fInfoFormat, "Общее число элементов:", type.GetMembers().Length);
            Console.WriteLine(fInfoFormat, "Число методов:", type.GetMethods().Length);
            Console.WriteLine(fInfoFormat, "Число свойств:", type.GetProperties().Length);
            Console.WriteLine(fInfoFormat, "Число полей:", type.GetFields().Length);

            var fields = type.GetFields();
            string[] arrStr = new string[type.GetFields().Length];
            for (int i = 0; i < arrStr.Length; i++)
            {
                arrStr[i] = fields[i].Name;
            }
            Console.WriteLine(fInfoFormat, "Список полей:", (arrStr.Length != 0) ? string.Join(", ", arrStr) : "-");

            var propertiesk = type.GetProperties();
            arrStr = new string[type.GetProperties().Length];
            for (int i = 0; i < arrStr.Length; i++)
            {
                arrStr[i] = propertiesk[i].Name;
            }
            Console.WriteLine(fInfoFormat, "Список свойств:", (arrStr.Length != 0) ? string.Join(", ", arrStr):"-");

            Console.WriteLine("\nНажмите 'm' для вывода дополнительной информации по методам;\nНажмите '0' для выхода в главное меню");
            while (true)
            {
                char ch = char.ToLower(Console.ReadKey(true).KeyChar);
                switch (ch)
                {
                    case 'm':
                        var mD = new Dictionary<string, int>();
                        foreach (var item in type.GetMethods())
                        {
                            if (mD.ContainsKey(item.Name))
                                mD[item.Name] += 1;
                            else
                                mD.Add(item.Name, 1);
                        }
                        Console.WriteLine("{0,-30}{1}\n", "Методы типа:", type.FullName);
                        Console.WriteLine(fInfoFormat, "Название", "Число перегрузок");
                        foreach (var kv in mD)
                            Console.WriteLine("\t{0,-30}{1,8}", kv.Key, kv.Value);
                        break;
                    case '0':
                        return;
                    default:
                        Console.WriteLine(sDef, ch);
                        break;
                }
            }
        }
    }
}
