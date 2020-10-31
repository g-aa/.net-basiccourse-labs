using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csh_Lab_4
{
    public enum WordType { None, Noun, Adjective, Verb };

    [Serializable]
    public class WordCard
    {
        private WordType        m_wType;
        private string          m_enWord;
        private List<string>    m_ruWord;

        public WordCard()
        {
            m_wType = WordType.None;
            m_enWord = string.Empty;
            m_ruWord = new List<string>();
        }
        
        public WordCard(string englishWord, WordType wordType, string russianWords) : this()
        {
            this.EnglishWord = englishWord;
            this.AddTranslation(russianWords);
            this.TypeOfWord = wordType;
        }

        public WordCard(WordCard other)
        {
            if (other != null)
            {
                m_wType = other.m_wType;
                m_enWord = other.m_enWord;
                m_ruWord = new List<string>(other.m_ruWord);
            }
            else
            {
                throw new ArgumentNullException("Параметр other принимает значение null!");
            }
        }

        public bool AddTranslation(string russianWord)
        {
            if (russianWord != null)
            {
                string temp = russianWord.Trim().ToLower();
                if (temp.All((char ch) => { return 'а' <= ch && ch <= 'я'; }))
                {
                    if (m_ruWord.IndexOf(temp) >= 0)
                    {
                        return false;
                    }
                    m_ruWord.Add(temp);
                    return true;
                }
                else
                {
                    throw new ArgumentException("Параметр russianWord не может содержать только буквы киррилицы!");
                }
            }
            else
            {
                throw new ArgumentNullException("Параметр russianWord не может принимать значение null!");
            }
        }

        public bool IsContained(string russianWord)
        {
            if (russianWord != null)
            {
                string temp = russianWord.Trim().ToLower();
                if (temp.All((char ch) => { return 'а' <= ch && ch <= 'я'; }))
                {
                    if (m_ruWord.IndexOf(temp) >= 0)
                    {
                        return true;
                    }
                    return false;
                }
                else
                {
                    throw new ArgumentException("Параметр russianWord не может содержать только буквы киррилицы!");
                }
            }
            else
            {
                throw new ArgumentNullException("Параметр russianWord не может принимать значение null!");
            }
        }

        public string EnglishWord
        {
            get => m_enWord;

            set
            {
                string temp = value.Trim().ToLower();
                if (temp != null)
                {
                    if (!temp.All((char ch) => { return 'a' <= ch && ch <= 'z'; }))
                    {
                        throw new ArgumentException("Параметр englishWord может содержать только латинские буквы!");
                    }
                    m_enWord = temp;
                }
                else
                {
                    throw new ArgumentNullException("Параметр englishWord не может принимать значение null!");
                }
            }
        }
        
        public WordType TypeOfWord { get => m_wType; set => m_wType = value; }
        
        public string[] Translations 
        { 
            get => m_ruWord.ToArray();
            set 
            {
                m_ruWord.Clear();
                m_ruWord.AddRange(value);
            } 
        }
        
        public int TranslationsCount { get => m_ruWord.Count; }
        
        public int WordLength { get => m_enWord.Length; }

        public override string ToString()
        {
            return string.Format("{0}, translation count: {1}", m_enWord, m_ruWord.Count);
        }
    }
}