using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Csh_Lab_5
{
    public partial class MainForm : Form
    {
        private BindingSource m_bindingSource;
        private List<Country> m_myData;

        public MainForm()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            m_myData = new List<Country>();
            m_bindingSource = new BindingSource();

            m_myData.AddRange(new Country[] {
                new Country("Германия", "Берлин", 357385, 82175684, new DateTime(2015, 12, 31), @"..\..\source\flags\flag_Germany.png", @"..\..\source\symbols\symbol_Germany.png"),
                new Country("Франция", "Париж", 674685, 65595620, new DateTime(2017, 1, 1), @"..\..\source\flags\flag_France.png", @"..\..\source\symbols\symbol_France.png"),
                new Country("КНР", "Пекин", 9598962, 1404328611, new DateTime(2019, 1, 1), @"..\..\source\flags\flag_China.png", @"..\..\source\symbols\symbol_China.png"),
                new Country("КНДР", "Пхеньян", 120540, 25564184, new DateTime(2018, 1, 1), @"..\..\source\flags\flag_NorthKorea.png", @"..\..\source\symbols\symbol_NorthKorea.png"),
                new Country("Россия", "Москва", 17125191, 146780720, new DateTime(2019, 1, 1), @"..\..\source\flags\flag_Russia.png", @"..\..\source\symbols\symbol_Russian.png"),
                new Country("Великобритания", "Лондон", 243809, 66273576, new DateTime(2018, 1, 1), @"..\..\source\flags\flag_UnitedKingdom.png", @"..\..\source\symbols\symbol_UnitedKingdom.png")
            });
            m_bindingSource.DataSource = m_myData;
            m_bindingSource.CurrentChanged += BindingSource_CurrentChanged;


            bindingNavigator1.BindingSource = m_bindingSource;

            m_propertyGrid.DataBindings.Add(nameof(m_propertyGrid.SelectedObject), m_bindingSource, null);
            m_propertyGrid.Enabled = false;

            // настройка pictupeBox:
            m_pictureFlag.DataBindings.Add(nameof(m_pictureFlag.ImageLocation), m_bindingSource, "ImageFlag", true);
            m_pictureFlag.SizeMode = PictureBoxSizeMode.StretchImage;
            m_pictureSymbol.DataBindings.Add(nameof(m_pictureSymbol.ImageLocation), m_bindingSource, "ImageSymbol", true);
            m_pictureSymbol.SizeMode = PictureBoxSizeMode.StretchImage;

            // настройка dataGrid:
            m_dataGrid.AutoGenerateColumns = false;
            m_dataGrid.DataSource = m_bindingSource;
            m_dataGrid.CellValidating += DataGrid_CellValidating;
            m_dataGrid.Columns.AddRange( new DataGridViewTextBoxColumn[] { 
                new DataGridViewTextBoxColumn { HeaderText = "Наименование страны", DataPropertyName = "CountryName", ValueType = typeof(string) },
                new DataGridViewTextBoxColumn { HeaderText = "Наименование столицы", DataPropertyName = "CapitalName", ValueType = typeof(string) },
                new DataGridViewTextBoxColumn { HeaderText = "Площадь km^2", DataPropertyName = "Square", ValueType = typeof(int) },
                new DataGridViewTextBoxColumn { HeaderText = "Численность населения", DataPropertyName = "Population", ValueType = typeof(int) },
                new DataGridViewTextBoxColumn { HeaderText = "Дата актуальности данных", DataPropertyName = "RelevantesDate", ValueType = typeof(DateTime) }
            });

            // настройка cart:
            m_chart.DataSource = m_bindingSource;
            m_chart.Legends.Clear();
            m_chart.Titles.Add("Распределение стран по площади");
            m_chart.Series[0].XValueMember = "CountryName";
            m_chart.Series[0].YValueMembers = "Square";

            m_toolBtnSave.Click += ToolBtn_Save;
            m_toolBtnOpen.Click += ToolBtn_Load;
            m_toolBtnSearch.Click += ToolBtn_Search;
            m_toolBtnClear.Click += ToolBtn_ClearAll;
        }

        private void BindingSource_CurrentChanged(object sender, EventArgs e)
        {
            m_chart.DataBind();
        }

        private void DataGrid_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            // площадь:
            if (e.ColumnIndex == 2)
            {
                if (!int.TryParse(e.FormattedValue.ToString(), out _))
                {
                    e.Cancel = true;
                    MessageBox.Show("Площадь страны может принимат значение типа int!");
                }
            }

            // численность населения:
            else if (e.ColumnIndex == 3)
            {
                if (!int.TryParse(e.FormattedValue.ToString(), out _))
                {
                    e.Cancel = true;
                    MessageBox.Show("Численность населения страны может принимат значение типа int!");
                }
            }

            // дата:
            else if (e.ColumnIndex == 4)
            {
                if (!DateTime.TryParse(e.FormattedValue.ToString(), out _))
                {
                    e.Cancel = true;
                    MessageBox.Show("Не формат дата!");
                }    
            }
        }

        private void ToolBtn_Save(object sender, EventArgs e)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Country>));
            SaveFileDialog file = new SaveFileDialog();
            if (file.ShowDialog() == DialogResult.OK)
            {
                using (FileStream stream = File.OpenWrite(file.FileName + ".xml"))
                {
                    serializer.Serialize(stream, m_myData);
                }
            }
        }

        private void ToolBtn_Load(object sender, EventArgs e)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Country>));
            OpenFileDialog file = new OpenFileDialog();
            if (file.ShowDialog() == DialogResult.OK)
            {
                using (FileStream stream = File.OpenRead(file.FileName))
                {
                    try
                    {
                        m_myData = (List<Country>)serializer.Deserialize(stream);
                        m_bindingSource.DataSource = m_myData;
                        m_bindingSource.ResetBindings(false);
                    }
                    catch (Exception expt)
                    {
                        MessageBox.Show(expt.Message);
                    }   
                }
            }
        }

        private void ToolBtn_ClearAll(object sender, EventArgs e)
        {
            
            m_myData.Clear();
            m_bindingSource.ResetBindings(true);
        }

        private void ToolBtn_Search(object sender, EventArgs e)
        {
            string searchValue = m_toolTxtBox.Text.Trim().ToLower();
            if (!"".Equals(searchValue) && m_myData.Count > 0)
            {
                for (int c = 0; c < m_dataGrid.ColumnCount; c++)
                {
                    for (int r = 0; r < m_dataGrid.RowCount; r++)
                    {
                        if (m_dataGrid[c,r].Value != null && m_dataGrid[c, r].Value.ToString().ToLower().Equals(searchValue))
                        {
                            m_dataGrid.CurrentCell = m_dataGrid[c, r];
                            return;
                        }
                    }
                }
            }
        }
    }
}
