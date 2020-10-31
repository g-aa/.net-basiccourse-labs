using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;

namespace Csh_Lab_4
{
    class WordsDictionary
    {
        private Dictionary<int, WordCard> m_wordCards;
        private Random m_random;

        public WordsDictionary()
        {
            m_wordCards = new Dictionary<int, WordCard>();
            m_random = new Random();
        }

        public bool AddCard(WordCard wordCard)
        {
            int hash = wordCard.EnglishWord.GetHashCode();
            if (!m_wordCards.ContainsKey(hash)) 
            {
                m_wordCards.Add(hash, wordCard);
                return true;
            }
            return false;
        }

        public WordCard this[string englishWord]
        {
            get => m_wordCards[englishWord.Trim().ToLower().GetHashCode()];
            set => m_wordCards[englishWord.Trim().ToLower().GetHashCode()] = value;
        }

        public int GetWordsCount(WordType type)
        {
            int count = 0;
            if (WordType.None.Equals(type))
            {
                count = m_wordCards.Count;
            }
            else
            {
                foreach (KeyValuePair<int, WordCard> item in m_wordCards)
                {
                    count += item.Value.TypeOfWord.Equals(type) ? 1 : 0;
                }
            }
            return count;
        }

        public int WordsCount { get => this.GetWordsCount(WordType.None); }

        public int NounCount { get => this.GetWordsCount(WordType.Noun); }

        public int AdjectiveCount { get => this.GetWordsCount(WordType.Adjective); }

        public int VerbCount { get => this.GetWordsCount(WordType.Verb); }

        public int WordsMean 
        { 
            get 
            {
                if (m_wordCards.Count != 0)
                {
                    int meanLength = 0;
                    foreach (KeyValuePair<int, WordCard> item in m_wordCards)
                    {
                        meanLength += item.Value.WordLength;
                    }
                    return meanLength / m_wordCards.Count;
                }
                return 0;
            } 
        }

        public int Count { get => m_wordCards.Count; }

        public List<string> EnglishWords
        {
            get 
            {
                List<string> result = null;
                if (m_wordCards.Count > 0)
                {
                    result = new List<string>(m_wordCards.Count);
                    foreach (var item in m_wordCards)
                    {
                        result.Add(item.Value.EnglishWord);
                    }
                }
                return result;
            }
        }

        public Tuple<WordCard, List<string>> GetRandTranslations(int translationsCount)
        {
            if (m_wordCards.Count < 4)
            {
                throw new Exception("Тестирование можно проводить только при наличии 4 карточек!");
            }
                
            WordCard card = null;
            List<string> allRuWords = new List<string>();

            int count = 0;
            int cardNum = m_random.Next(0, m_wordCards.Count);
            foreach (var item in m_wordCards)
            {
                if (count != cardNum)
                {
                    allRuWords.AddRange(item.Value.Translations);
                }
                else
                {
                    card = item.Value;
                }
                count += 1;
            }

            if (allRuWords.Count < translationsCount - 1)
            {
                throw new Exception("Число русских слов для тестирование меньше числа выборки !");
            }

            List<string> testTranslations = new List<string>(translationsCount);
            for (int i = 0; i < translationsCount - 1; i++)
            {
                testTranslations.Add(allRuWords[m_random.Next(0, allRuWords.Count)]);
            }

            testTranslations.Insert(m_random.Next(0, translationsCount - 1), card.Translations[m_random.Next(0, card.TranslationsCount)]);
            return new Tuple<WordCard, List<string>>(card, testTranslations);
        }


        #region Serialization
        public static void SaveToXml(string path, WordsDictionary cards)
        {
            TextWriter outStream = null;
            try
            {
                outStream = new StreamWriter(path);
                XmlSerializer serializer = new XmlSerializer(typeof(WordCard[]));
                WordCard[] wordCards = new WordCard[cards.m_wordCards.Count];
                cards.m_wordCards.Values.CopyTo(wordCards, 0);
                serializer.Serialize(outStream, wordCards);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                outStream.Close();
            }
        }

        public static void SaveToBin(string path, WordsDictionary cards)
        {
            FileStream outStream = null;
            try
            {
                outStream = new FileStream(path, FileMode.Create, FileAccess.Write);
                BinaryFormatter formatter = new BinaryFormatter();
                
                WordCard[] wordCards = new WordCard[cards.m_wordCards.Count];
                cards.m_wordCards.Values.CopyTo(wordCards, 0);
                formatter.Serialize(outStream, wordCards);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                outStream.Close();
            }
        }

        public static WordsDictionary LoadFromXml(string path)
        {
            WordsDictionary words = null;
            TextReader stream = null;
            try
            {
                stream = new StreamReader(path);
                XmlSerializer serializer = new XmlSerializer(typeof(WordCard[]));

                WordCard[] cardsArray = (WordCard[])serializer.Deserialize(stream);
                words = new WordsDictionary();
                foreach (var item in cardsArray)
                {
                    words.AddCard(item);
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                stream.Close();
            }

            return words;
        }

        public static WordsDictionary LoadFromBin(string path)
        {
            WordsDictionary words = null;
            FileStream stream = null;
            try
            {
                stream = new FileStream(path, FileMode.Open, FileAccess.Read);
                BinaryFormatter formatter = new BinaryFormatter();

                WordCard[] wordCards = (WordCard[])formatter.Deserialize(stream);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                stream.Close();
            }

            return words;
        }

        #endregion

        public override string ToString()
        {
            if (m_wordCards.Count > 0)
            {
                StringBuilder result = new StringBuilder();
                int index = 1;
                foreach (var card in m_wordCards)
                {
                    string formatTitle = "№{0} {1," + card.Value.EnglishWord.Length + "}:  - {2}";
                    string formatItem = "     {0," + card.Value.EnglishWord.Length + "} - {1}";

                    string[] translations = card.Value.Translations;
                    for (int i = 0; i < translations.Length; i++)
                    {
                        if (i > 0)
                        {
                            result.AppendLine(string.Format(formatItem, "", translations[i]));
                        }
                        else
                        {
                            result.AppendLine(string.Format(formatTitle, index, card.Value.EnglishWord, translations[i]));
                        }
                    }
                    result.Append("\n");
                    index += 1;
                }
                return result.ToString();
            }
            return string.Empty;
        }
        }
}
