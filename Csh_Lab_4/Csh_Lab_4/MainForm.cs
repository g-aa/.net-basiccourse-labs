using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Csh_Lab_4
{
    class MainForm : Form
    {    
        private enum StreamType { Bin, Xml };
        
        private WordsDictionary m_cards;

        private Label m_NoneCount;
        private Label m_NounCount;
        private Label m_AdjectiveCount;
        private Label m_VerbCount;
        private Label m_MeanWords;

        private Label m_BestResult;
        private Label m_LastResult;

        private int m_CardsCnt;
        private int m_AnswerCnt;

        public MainForm()
        {
            Font font = new Font("Consolas", 12, FontStyle.Regular);
            Size btnSize = new Size(170, 30);
            
            this.Text = "Main form ictionary";
            this.Font = font;
            this.AutoSize = true;
            this.AutoSizeMode = AutoSizeMode.GrowOnly;

            this.m_cards = new WordsDictionary();
            this.m_CardsCnt = 4;
            this.m_AnswerCnt = 4;

            //
            TableLayoutPanel table = new TableLayoutPanel
            {
                Parent = this,
                Padding = new Padding(Font.Height),
                AutoSize = true,
                RowCount = 4,
                ColumnCount = 2,
            };
            table.SuspendLayout();

            // настройка статистики словаря:
            TableLayoutPanel subTable = new TableLayoutPanel
            {
                Parent = table,
                Padding = new Padding(Font.Height),
                AutoSize = true,
                RowCount = 6,
                ColumnCount = 2,
            };

            subTable.Controls.Add(new Label { Parent = subTable, Text = "Статистика по словарю", Font = font, AutoSize = true, Anchor = AnchorStyles.Left | AnchorStyles.None }, 0, 0);
            subTable.Controls.Add(new Label { Parent = subTable, Text = "Колличество слов:", Font = font, AutoSize = true, Anchor = AnchorStyles.Left | AnchorStyles.None }, 0, 1);
            subTable.Controls.Add(new Label { Parent = subTable, Text = "Колличество сущ.:", Font = font, AutoSize = true, Anchor = AnchorStyles.Left | AnchorStyles.None }, 0, 2);
            subTable.Controls.Add(new Label { Parent = subTable, Text = "Колличество прил.:", Font = font, AutoSize = true, Anchor = AnchorStyles.Left | AnchorStyles.None }, 0, 3);
            subTable.Controls.Add(new Label { Parent = subTable, Text = "Колличество глаг.:", Font = font, AutoSize = true, Anchor = AnchorStyles.Left | AnchorStyles.None }, 0, 4);
            subTable.Controls.Add(new Label { Parent = subTable, Text = "Средняя длина снова:", Font = font, AutoSize = true, Anchor = AnchorStyles.Left | AnchorStyles.None }, 0, 5);

            m_NoneCount = new Label() { Parent = subTable, Text = m_cards.WordsCount.ToString(), Font = font, AutoSize = true, Anchor = AnchorStyles.Left | AnchorStyles.None };
            subTable.Controls.Add(m_NoneCount, 1, 1);

            m_NounCount = new Label() { Parent = subTable, Text = m_cards.NounCount.ToString(), Font = font, AutoSize = true, Anchor = AnchorStyles.Left | AnchorStyles.None };
            subTable.Controls.Add(m_NounCount, 1, 2);

            m_AdjectiveCount = new Label() { Parent = subTable, Text = m_cards.AdjectiveCount.ToString(), Font = font, AutoSize = true, Anchor = AnchorStyles.Left | AnchorStyles.None };
            subTable.Controls.Add(m_AdjectiveCount, 1, 3);

            m_VerbCount = new Label() { Parent = subTable, Text = m_cards.VerbCount.ToString(), Font = font, AutoSize = true, Anchor = AnchorStyles.Left | AnchorStyles.None };
            subTable.Controls.Add(m_VerbCount, 1, 4);

            m_MeanWords = new Label() { Parent = subTable, Text = m_cards.WordsMean.ToString(), Font = font, AutoSize = true, Anchor = AnchorStyles.Left | AnchorStyles.None };
            subTable.Controls.Add(m_MeanWords, 1, 5);
            table.Controls.Add(subTable, 0, 0);

            // статистика по тесту:
            subTable = new TableLayoutPanel
            {
                Parent = this,
                Padding = new Padding(Font.Height),
                AutoSize = true,
                RowCount = 4,
                ColumnCount = 2,
            };

            subTable.Controls.Add(new Label { Parent = subTable, Text = "Статистика по тесту", Font = font, AutoSize = true, Anchor = AnchorStyles.Left | AnchorStyles.None }, 0, 0);
            subTable.Controls.Add(new Label { Parent = subTable, Text = "Число карточек для теста:", Font = font, AutoSize = true, Anchor = AnchorStyles.Left | AnchorStyles.None }, 0, 1);
            subTable.Controls.Add(new Label { Parent = subTable, Text = "Число вариантов ответа:", Font = font, AutoSize = true, Anchor = AnchorStyles.Left | AnchorStyles.None }, 0, 2);
            subTable.Controls.Add(new Label { Parent = subTable, Text = "Последний результат:", Font = font, AutoSize = true, Anchor = AnchorStyles.Left | AnchorStyles.None }, 0, 3);
            subTable.Controls.Add(new Label { Parent = subTable, Text = "Лучший результат:", Font = font, AutoSize = true, Anchor = AnchorStyles.Left | AnchorStyles.None }, 0, 4);

            NumericUpDown numeric = new NumericUpDown() { Parent = subTable, Font = font, Value = m_CardsCnt, Minimum = 2, Maximum = 8, Anchor = AnchorStyles.Left };
            numeric.ValueChanged += Numeric_CardCnt;
            subTable.Controls.Add(numeric, 1, 1);

            numeric = new NumericUpDown() { Parent = subTable, Font = font, Value = m_AnswerCnt, Minimum = 2, Maximum = 8, Anchor = AnchorStyles.Left };
            numeric.ValueChanged += Numeric_AnswerCnt;
            subTable.Controls.Add(numeric, 1, 2);

            m_LastResult = new Label() { Parent = subTable, Text = "0 %", Font = font, AutoSize = true, Anchor = AnchorStyles.Left | AnchorStyles.None };
            subTable.Controls.Add(m_LastResult, 1, 3);

            m_BestResult = new Label() { Parent = subTable, Text = "0 %", Font = font, AutoSize = true, Anchor = AnchorStyles.Left | AnchorStyles.None };
            subTable.Controls.Add(m_BestResult, 1, 4);
            table.Controls.Add(subTable, 1, 0);

            // кнопки:
            Button button = new Button { Parent = table, Font = font, Size = btnSize, Anchor = AnchorStyles.None, Text = "Добавить карточку" };
            button.Click += Btn_Add;
            table.Controls.Add(button, 0, 1);

            // button = new Button { Parent = table, Font = font, Size = btnSize, Anchor = AnchorStyles.None, Text = "Редактировать" };
            
            // table.Controls.Add(button, 0, 2);

            button = new Button { Parent = table, Font = font, Size = btnSize, Anchor = AnchorStyles.None, Text = "Сохранить *.xml" };
            button.Click += Btn_XmlSave;
            table.Controls.Add(button, 0, 3);

            button = new Button { Parent = table, Font = font, Size = btnSize, Anchor = AnchorStyles.None, Text = "Распечатать" };
            button.Click += Btn_Print;
            table.Controls.Add(button, 1, 1);

            button = new Button { Parent = table, Font = font, Size = btnSize, Anchor = AnchorStyles.None, Text = "Тест" };
            button.Click += Btn_Test;
            table.Controls.Add(button, 1, 2);

            button = new Button { Parent = table, Font = font, Size = btnSize, Anchor = AnchorStyles.None, Text = "Загрузить *.xml" };
            button.Click += Btn_XmlLoad;
            table.Controls.Add(button, 1, 3);

            button = new Button { Parent = table, Font = font, Size = btnSize, Anchor = AnchorStyles.None, Text = "Сохранить *.bin" };
            button.Click += Btn_BinSave;
            table.Controls.Add(button, 0, 4);

            button = new Button { Parent = table, Font = font, Size = btnSize, Anchor = AnchorStyles.None, Text = "Загрузить *.bin" };
            button.Click += Btn_BinLoad;
            table.Controls.Add(button, 1, 4);

            table.ResumeLayout();
        }

        private void Numeric_AnswerCnt(object sender, EventArgs e)
        {
            NumericUpDown numeric = (NumericUpDown)sender;
            this.m_AnswerCnt = (int)numeric.Value;
        }

        private void Numeric_CardCnt(object sender, EventArgs e)
        {
            NumericUpDown numeric = (NumericUpDown)sender;
            this.m_CardsCnt = (int)numeric.Value;
        }

        private void Btn_Test(object sender, EventArgs eArgs)
        {
            if (m_cards.Count >= 4)
            {
                TestForm test = new TestForm(m_cards, m_CardsCnt, m_AnswerCnt);
                test.ShowDialog(this);

                this.m_LastResult.Text = test.TestResult.ToString() + " %";
                int g = int.Parse(this.m_BestResult.Text.Replace("%", ""));

                if (int.Parse(this.m_BestResult.Text.Replace("%", "")) < test.TestResult)
                {
                    this.m_BestResult.Text = test.TestResult.ToString() + " %";
                }
            }
            else 
            {
                MessageBox.Show("Для проведения тестирования число карточек должно быть не менее 4!");
            }
        }
        
        private void Btn_Print(object sender, EventArgs eArgs)
        {
            PrintForm print = new PrintForm(m_cards);
            print.ShowDialog(this);
        }

        private void Btn_XmlLoad(object sender, EventArgs eArgs)
        {
            this.BinXmlLoader(StreamType.Xml);
        }

        private void Btn_BinLoad(object sender, EventArgs eArgs)
        {
            this.BinXmlLoader(StreamType.Bin);
        }

        private void Btn_XmlSave(object sender, EventArgs eArgs)
        {
            this.BinXmlWriter(StreamType.Xml);
        }

        private void Btn_BinSave(object sender, EventArgs eArgs)
        {
            this.BinXmlWriter(StreamType.Bin);
        }
        
        private void Btn_Add(object o, EventArgs eArgs)
        {
            CardForm card = new CardForm(m_cards);
            card.ShowDialog(this);
            RefreshWordsInfo();
        }

        
        private void RefreshWordsInfo()
        {
            m_NoneCount.Text = m_cards.WordsCount.ToString();
            m_NounCount.Text = m_cards.NounCount.ToString();
            m_AdjectiveCount.Text = m_cards.AdjectiveCount.ToString();
            m_VerbCount.Text = m_cards.VerbCount.ToString();
            m_MeanWords.Text = m_cards.WordsMean.ToString();
        }

        private void BinXmlLoader(StreamType type)
        {
            string filterType = type.Equals(StreamType.Bin) ? "bin files (*.bin)|*.bin" : "xml files (*.xml)|*.xml";
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = @"c:\";
                openFileDialog.Filter = filterType;
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string path = openFileDialog.FileName;
                    WordsDictionary temp = type.Equals(StreamType.Bin) ? WordsDictionary.LoadFromBin(path) : WordsDictionary.LoadFromXml(path);
                    if (temp != null)
                    {
                        m_cards = temp;
                    }
                }
            }
        }

        private void BinXmlWriter(StreamType type)
        {
            if (m_cards.Count > 0)
            {
                string filterType = type.Equals(StreamType.Bin) ? "bin files (*.bin)|*.bin" : "xml files (*.xml)|*.xml";
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.InitialDirectory = @"c:\";
                    saveFileDialog.Filter = filterType;
                    saveFileDialog.FilterIndex = 1;
                    saveFileDialog.RestoreDirectory = true;

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string path = saveFileDialog.FileName;

                        if (type.Equals(StreamType.Bin))
                        {
                            WordsDictionary.SaveToBin(path, m_cards);
                        }
                        else
                        {
                            WordsDictionary.SaveToXml(path, m_cards);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("База пуста, нечего сохранять!");
            }
        }
    }
}
