using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Csh_Lab_4
{
    class CardForm : Form
    {
        private WordCard m_card;

        private TextBox m_enWord;
        private TextBox m_ruWord;
        private ComboBox m_wordType;
        private ListBox m_ruWords;
        private WordsDictionary m_words;

        public CardForm(WordsDictionary cards)
        {
            Size size = new Size(170, 30);
            Font font = new Font("Consolas", 12, FontStyle.Regular);
                                    
            this.Text = "My card form";
            this.Font = font;
            this.AutoSize = true;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.m_card = new WordCard();
            this.m_words = cards;

            TableLayoutPanel table = new TableLayoutPanel
            {
                Parent = this,
                Padding = new Padding(Font.Height),
                AutoSize = true,
                RowCount = 3,
                ColumnCount = 4
            };
            table.SuspendLayout();

            // добавить английское слово:
            Label label = new Label
            {
                Parent = table,
                Text = "Слово (en):",
                AutoSize = true,
                Font = font,
                Anchor = AnchorStyles.None | AnchorStyles.Left
            };
            table.Controls.Add(label, 0, 0);

            m_enWord = new TextBox
            {
                Parent = table,
                Font = font,
                Anchor = AnchorStyles.None | AnchorStyles.Left
            };
            m_enWord.TextChanged += enWord_TextChanged;
            m_enWord.Width = size.Width;
            table.Controls.Add(m_enWord, 1, 0);

            // добавить перевод:
            label = new Label
            {
                Parent = table,
                Text = "Перевод (ru):",
                AutoSize = true,
                Font = font,
                Anchor = AnchorStyles.None | AnchorStyles.Left
            };
            table.Controls.Add(label, 2, 0);

            m_ruWord = new TextBox
            {
                Parent = table,
                Font = font,
                Anchor = AnchorStyles.None | AnchorStyles.Left
            };
            m_ruWord.Width = size.Width;
            table.Controls.Add(m_ruWord, 3, 0);

            // добавить тип слову:
            label = new Label
            {
                Parent = table,
                Text = "Тип слова:",
                AutoSize = true,
                Font = font,
                Anchor = AnchorStyles.Top | AnchorStyles.Left
            };
            table.Controls.Add(label, 0, 1);

            m_wordType = new ComboBox()
            {
                Font = font,
                Parent = table,
                Anchor = AnchorStyles.Top | AnchorStyles.Left
            };
            m_wordType.SelectedValueChanged += wordType_SelectedValueChanged;
            m_wordType.Width = size.Width;
            m_wordType.Items.AddRange(Enum.GetNames(typeof(WordType)));
            m_wordType.SelectedIndex = 0; 
            table.Controls.Add(m_wordType, 1, 1);


            m_ruWords = new ListBox();
            m_ruWords.Font = font;
            m_ruWords.Parent = table;
            m_ruWords.Width = size.Width;
            m_ruWords.Anchor = AnchorStyles.None | AnchorStyles.Left;
            table.Controls.Add(m_ruWords, 3, 1);
            
            Button button = new Button
            {
                Parent = table,
                Text = "Сохранить карту",
                Font = font,
                Anchor = AnchorStyles.None
            };
            button.Size = size;
            button.Click += btn_save;
            table.Controls.Add(button, 1, 2);

            button = new Button
            {
                Parent = table,
                Text = "Добавить перевод",
                Font = font,
                Anchor = AnchorStyles.None
            };
            button.Size = size;
            button.Click += btn_addRuWords;
            table.Controls.Add(button, 3, 2);

            table.ResumeLayout();
        }

        private void wordType_SelectedValueChanged(object sender, EventArgs e)
        {
            m_card.TypeOfWord = (WordType)Enum.Parse(typeof(WordType), (string)((ComboBox)sender).SelectedItem);
        }

        private void enWord_TextChanged(object sender, EventArgs eArgs)
        {
            TextBox textBox = (TextBox)sender;
            try
            {
                m_card.EnglishWord = textBox.Text;
            }
            catch (ArgumentException e)
            {
                textBox.Text = textBox.Text.Remove(textBox.Text.Length - 1, 1);
                MessageBox.Show(e.Message,"Worning !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btn_save(object o, EventArgs eArgs)
        {
            if (!"".Equals(m_card.EnglishWord) && m_card.TranslationsCount != 0)
            {
                if (!m_words.AddCard(m_card))
                {
                    MessageBox.Show("Карта \"" + m_card.EnglishWord + "\" уже существует в словаре!", "Worning !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    m_enWord.Text = "";
                    m_ruWord.Text = "";
                    m_ruWords.Items.Clear();
                    m_wordType.SelectedIndex = 0;
                }
            }
            else
            {
                MessageBox.Show("Поля не могут быть пустыми!", "Worning !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btn_addRuWords(object o, EventArgs eArgs)
        {
            try
            {
                string s = m_ruWord.Text;
                if (!"".Equals(s))
                {
                    if (m_card.AddTranslation(s))
                    {
                        m_ruWords.Items.Add(s);
                    }
                    else 
                    {
                        MessageBox.Show("Слов: \"" + s + "\" уже добавленно !", "Worning !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (ArgumentException e)
            {
                MessageBox.Show(e.Message, "Worning !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
