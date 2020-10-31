using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Numerics;


namespace Csh_Lab_2
{
    static class Program
    {
        private const string sDef = "Указанно не верное значение - '{0}'";
        static void Main(string[] args)
        {
            string[] sMenu = { "1 – Ввод матрицы", "2 - Операции над матрицами", "3 – Вывод результатов", "0 - Выход из программы" };
            MyMatrix M1 = null;
            MyMatrix M2 = null;
            MyMatrix Mres = null;
            while (true)
            {
                Console.WriteLine("Взаимодействие с MyMatrix:\n");
                Array.ForEach(sMenu, s => { Console.WriteLine("\t{0}", s); });
                char ch = char.ToLower(Console.ReadKey(true).KeyChar);
                switch (ch)
                {
                    case '1':
                        MatrixGenerator(ref M1, ref M2);
                        break;
                    case '2':
                        Mres = MatrixOperationMenu(M1, M2);
                        break;
                    case '3':
                        AboutMatrix(M1, "M1");
                        AboutMatrix(M2, "M2");
                        AboutMatrix(Mres, "Mres");
                        break;
                    case '0':
                        return;
                    default:
                        Console.WriteLine(sDef, ch);
                        break;
                }
                Console.WriteLine();
            }
        }

        private static void MatrixGenerator(ref MyMatrix m1, ref MyMatrix m2)
        {
            string[] sMenu = { "1 – Создать матрицу M1", "1 – Создать матрицу M2", "0 - Возврат в предыдущее меню" };
            while (true)
            {
                Console.WriteLine("Ввод матриц:\n");
                Array.ForEach(sMenu, s => { Console.WriteLine("\t{0}", s); });
                char ch = char.ToLower(Console.ReadKey(true).KeyChar);
                switch (ch)
                {
                    case '1':
                        m1 = MatrixSelector("M1");
                        break;
                    case '2':
                        m2 = MatrixSelector("M2");
                        break;
                    case '0':
                        return;
                    default:
                        Console.WriteLine(sDef, ch);
                        break;
                }
                Console.WriteLine();
            }
        }

        private static MyMatrix MatrixSelector(string name)
        {
            MyMatrix matrix = null;            
            string[] sMenu = { "1 – Создать еденичную матрицу", "2 – Создать нулевую матрицу", "3 – Создать рандомную матрицу", "0 - Возврат в предыдущее меню" };
            while (true)
            {    
                Console.WriteLine("Сгенерировать матрицу \"{0}:\"\n", name);
                Array.ForEach(sMenu, s => { Console.WriteLine("\t{0}", s); });
                char ch = char.ToLower(Console.ReadKey(true).KeyChar);
                try
                {
                    switch (ch)
                    {
                        case '1':
                            Console.WriteLine("Введите размер size:");
                            matrix = MyMatrix.GetUnity(int.Parse(Console.ReadLine()));
                            Console.WriteLine("Создана еденичная матрица: {0}x{1}, число элементов {2}", matrix.Rows, matrix.Cols, matrix.Size);
                            break;
                        case '2':
                            Console.WriteLine("Введите размер size:");
                            matrix = MyMatrix.GetEmpty(int.Parse(Console.ReadLine()));                        
                            Console.WriteLine("Создана нулевая матрица: {0}x{1}, число элементов {2}", matrix.Rows, matrix.Cols, matrix.Size);
                            break;
                        case '3':
                            Console.WriteLine("Введите размер row,cols:");
                            string[] buf = Console.ReadLine().Split(',');
                            matrix = MyMatrix.Randomized(int.Parse(buf[0]), int.Parse(buf[1]));
                            Console.WriteLine("Создана рандомная матрица: {0}x{1}, число элементов {2}", matrix.Rows, matrix.Cols, matrix.Size);
                            break;
                        case '0':
                            return matrix;
                        default:
                            Console.WriteLine(sDef, ch);
                            break;
                    }
                }
                catch (Exception expt)
                {
                    Console.WriteLine(expt.Message);
                }
                Console.WriteLine();
            }
        }

        private static MyMatrix MatrixOperationMenu(MyMatrix m1, MyMatrix m2)
        {
            MyMatrix result = null;
            string[] sMenu = { "1 – M1 + M2", "2 – M1 - M2", "3 – M1 * M2", "4 – M1 * d", "0 - Возврат в предыдущее меню" };
            while (true)
            {
                Console.WriteLine("Ввод матриц:\n");
                Array.ForEach(sMenu, s => { Console.WriteLine("\t{0}", s); });
                char ch = char.ToLower(Console.ReadKey(true).KeyChar);
                try 
	            {	        
		            switch (ch)
                    {
                        case '1':
                            result = m1 + m2;
                            return result;
                        case '2':
                            result = m1 - m2;
                            return result;
                        case '3':
                            result = m1 * m2;
                            return result;
                        case '4':
                            Console.WriteLine("Введите число d на которе будет умножаться матрица:");
                            int d = int.Parse(Console.ReadLine());
                            result = m1 * d;
                            return result;
                        case '0':
                            return null;
                        default:
                            Console.WriteLine(sDef, ch);
                        break;
                    }
	            }
	            catch (Exception expt)
	            {
                    Console.WriteLine(expt.Message);
	            }
                Console.WriteLine();
            }
        }

        private static void AboutMatrix(MyMatrix m, string mName)
        {
            Console.WriteLine("Информация по объекту {0}: ", mName);
            if(m != null)
            {
                Console.WriteLine("Диагональная: {0}", m.IsDiagonal);
                Console.WriteLine("Пустая ли матрица: {0}", m.IsEmpty);
                Console.WriteLine("Квадратная: {0}", m.IsSquared);
                Console.WriteLine("Еденичная: {0}", m.IsUnity);
                Console.WriteLine("Симметричная: {0}", m.IsSymmetric);
                try
                {
                    Console.WriteLine("След матрицы: {0}", m.Trace());
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            Console.WriteLine((m != null)? m.ToString() : "М1 = null!");
            Console.WriteLine();
        }
    }
}
