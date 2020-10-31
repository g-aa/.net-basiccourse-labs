using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Csh_Lab_4
{
    class TestForm : Form
    {
        private static string m_title = "Режим тестирования: картокчка <{0}> из <{1}>";
        private static string m_question = "Выберети правильный варинт перевода для слова:\n\"{0}\" -";

        private int m_cardsCount;
        private int m_cardsIndex;
        private int m_answerCnt;

        public int TestResult { get; private set; }

        private WordsDictionary m_dictionary;

        private Tuple<WordCard, List<string>> m_actualCard;

        private Label lbl_question;
        
        private Button btn_send;
        private Button btn_next;

        private RadioButton[] rBtn_answer;

        private List<string> m_usedCards;

        public TestForm(WordsDictionary dictionary, int cardsCnt, int answerCnt)
        {
            m_dictionary = dictionary;

            if (cardsCnt >= dictionary.WordsCount)
            {
                MessageBox.Show("Число карточек снижено в 2 раза, по причине равенства или большего колличества тестируемых карточек над числом карт в словаре!");
                m_cardsCount = cardsCnt / 2;
            }
            else
            {
                m_cardsCount = cardsCnt;
            }

            if (cardsCnt >= dictionary.WordsCount)
            {
                MessageBox.Show("Число выбора вариантов ответа снижено в 2 раза, по причине равенства или большего колличества тестируемых карточек над числом карт в словаре!");
                m_answerCnt = answerCnt / 2;
            }
            else
            {
                m_answerCnt = answerCnt;
            }

            m_cardsIndex = 0;
            m_usedCards = new List<string>(m_cardsCount);
            
            Point location = new Point(20, 20);
            Size btnSize = new Size(170, 30);
            Font font = new Font("Consolas", 12, FontStyle.Regular);
            
            this.Text = string.Format(m_title, 1, m_cardsCount);
            this.Font = font;
            this.AutoSize = true;
            this.AutoSizeMode = AutoSizeMode.GrowOnly;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;


            GetNewTestCard();   // получить первую карточку

            lbl_question = new Label
            {
                Parent = this,
                Text = string.Format(m_question, m_actualCard.Item1.EnglishWord),
                AutoSize = true,
                Font = font,
                Location = location
            };
            location.Y = lbl_question.Size.Height + 20;


            rBtn_answer = new RadioButton[m_answerCnt];
            for (int i = 0; i < m_answerCnt; i++)
            {
                rBtn_answer[i] = new RadioButton()
                {
                    Parent = this,
                    Text = m_actualCard.Item2[i],
                    AutoSize = true,
                    Font = font,
                    Location = new Point(location.X + 150, location.Y),
                    Checked = (i > 0) ? false : true
                };
                location.Y += rBtn_answer[i].Size.Height + 10;
            }

            btn_send = new Button
            {
                Parent = this,
                Font = font,
                Size = btnSize,
                Text = "Ответить",
                Location = location,
            };
            btn_send.Click += Btn_Send;
            location.X = btn_send.Size.Width + 100;

            btn_next = new Button
            {
                Parent = this,
                Font = font,
                Size = btnSize,
                Text = "Следующий",
                Enabled = false,
                Location = location
            };
            btn_next.Click += Btn_Next;
        }

        private void Btn_Next(object sender, EventArgs e)
        {
            GetNewTestCard();
            lbl_question.Text = string.Format(m_question, m_actualCard.Item1.EnglishWord);
            this.Text = string.Format(m_title, m_cardsIndex, m_cardsCount);

            for (int i = 0; i < m_answerCnt; i++)
            {
                rBtn_answer[i].BackColor = this.BackColor;
                rBtn_answer[i].Text = m_actualCard.Item2[i];
                rBtn_answer[i].Enabled = true;
                rBtn_answer[i].Checked = (i > 0) ? false : true;
            }
            btn_next.Enabled = false;
        }

        private void Btn_Send(object sender, EventArgs e)
        {
            foreach (var item in rBtn_answer)
            {
                if (item.Checked)
                {
                    if (m_actualCard.Item1.IsContained(item.Text))
                    {
                        TestResult += 1;
                        item.BackColor = Color.Green;
                    }
                    else
                    {
                        TestResult += 0;
                        item.BackColor = Color.Red;
                    }
                }
                item.Enabled = false;
            }
            btn_next.Enabled = (m_cardsIndex < m_cardsCount) ? true : false;
            if (m_cardsCount == m_cardsIndex)
                TestResult = (int)((double)TestResult/m_cardsCount * 100);
        }

        private void GetNewTestCard() 
        {                       
            while (true)
            {
                m_actualCard = m_dictionary.GetRandTranslations(m_answerCnt);

                if (!m_usedCards.Contains(m_actualCard.Item1.EnglishWord)) 
                {
                    m_cardsIndex += 1;
                    m_usedCards.Add(m_actualCard.Item1.EnglishWord);
                    break;
                }
            }
        }
    }
}
