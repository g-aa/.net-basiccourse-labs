using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Csh_Lab_5
{
    [Serializable]
    public class Country
    {
        private int     m_population;
        private int     m_square;
        private string  m_capitalName;
        private string  m_countryName;
        private DateTime m_relevantesDate;

        private string m_imgFlag;
        private string m_imgSymbol;

        public Country()
        {
            m_countryName = string.Empty;
            m_capitalName = string.Empty;
            m_relevantesDate = DateTime.Today;
            m_population = 0;
            m_square = 0;
            m_imgFlag = string.Empty;
            m_imgSymbol = string.Empty;
        }

        public Country(string country, string capital, int square, int population, DateTime date, string flagPath, string symbolPath) : this()
        {
            if (country == null || capital == null || flagPath == null || symbolPath == null)
            {
                throw new ArgumentNullException("Один из критических параметров принимает значение null");
            }

            m_countryName = ("".Equals(country.Trim())) ? "Страна не извесна" : country.Trim();
            m_capitalName = ("".Equals(capital.Trim())) ? "Столица не известна" : capital.Trim();
            m_relevantesDate = date;
            m_square = square;
            m_population = population;

            if (flagPath != null && !"".Equals(flagPath.Trim()))
            {
                m_imgFlag = flagPath;
            }
            
            if (symbolPath != null && !"".Equals(symbolPath.Trim()))
            {
                m_imgSymbol = symbolPath;
            }
        }

        [DisplayName("Страна"), Category("Критический параметр")]
        [Description("Наименование страны")]
        public string CountryName
        {
            get => m_countryName;
            set 
            {
                if (value != null)
                {
                    m_countryName = ("".Equals(value.Trim())) ? "Страна не извесна" : value.Trim();
                }
            }
        }

        [DisplayName("Столица"), Category("Дополнительный параметр")]
        [Description("Наименование столицы")]
        public string CapitalName
        {
            get => m_capitalName;
            set
            {
                if (value != null)
                {
                    m_capitalName = ("".Equals(value.Trim())) ? "Столица не известна" : value.Trim();
                }
            }
        }

        [DisplayName("Площадь территории, [km^2]"), Category("Критический параметр")]
        [Description("Площадь территории страны")]
        public int Square
        {
            get => m_square;
            set
            {
                if (value > 0)
                {
                    m_square = value;
                }
            }
            
        }

        [DisplayName("Численность населения, [чел.]"), Category("Дополнительный параметр")]
        [Description("Численность населения страны")]
        public int Population
        {
            get => m_population;
            set
            {
                if (value > 0)
                {
                    m_population = value;
                }
            }
        }

        [DisplayName("Дата актуальности данных"), Category("Дополнительный параметр")]
        [Description("Дата актуальности данных")]
        public DateTime RelevantesDate
        {
            get => m_relevantesDate;
            set => m_relevantesDate = value;
        }

        [DisplayName("Путь к файлу с флвгом"), Category("Дополнительный параметр")]
        [Description("Путь к файлу с флагом")]
        public string ImageFlag
        {
            get => m_imgFlag;
            set
            {
                if (value != null && !"".Equals(value.Trim()))
                {
                    m_imgFlag = value;
                }
            }
        }

        [DisplayName("Путь к файлу с символом"), Category("Дополнительный параметр")]
        [Description("Путь к файлу с символов")]
        public string ImageSymbol
        {
            get => m_imgSymbol;
            set
            {
                if (value != null && !"".Equals(value.Trim()))
                {
                    m_imgSymbol = value;
                }
            }
        }
    }
}
