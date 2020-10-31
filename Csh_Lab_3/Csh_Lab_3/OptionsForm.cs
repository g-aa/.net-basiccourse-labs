using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Csh_Lab_3
{
    class OptionsForm : Form
    {
        public PaintOptions Options { private set; get; }

        private Label lbl_color;

        public OptionsForm(Font font, PaintOptions options)
        {
            Options = options;
            
            // Форма:
            this.Text = "Окно с параметрами";
            this.Font = font;
            this.AutoSize = true;
            this.AutoSizeMode = AutoSizeMode.GrowOnly;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            // компоненты формы:
            TableLayoutPanel table = new TableLayoutPanel
            {
                Parent = this,
                Padding = new Padding(Font.Height),
                AutoSize = true,
                RowCount = 6,
                ColumnCount = 2,
            };
            table.Parent = this;
            table.SuspendLayout();

            // настройки цвета фигуры:
            Button m_btnColor = new Button
            {
                Text = "Настройки цвета:",
                Font = font,
                Parent = table,
                Anchor = AnchorStyles.Left,
                AutoSize = true
            };
            m_btnColor.Click += btn_color;
            table.Controls.Add(m_btnColor, 0, 0);

            lbl_color = new Label()
            {
                Text = " ",
                BackColor = options.FigureColor,
                Font = font,
                Parent = table,
                Anchor = AnchorStyles.Left,
                AutoSize = true
            };
            table.Controls.Add(lbl_color, 1, 0);

            // настройка размеров точки:
            Label label = new Label()
            {
                Text = "Размер точки:",
                Font = font,
                Parent = table,
                Anchor = AnchorStyles.Left,
                AutoSize = true
            };
            table.Controls.Add(label, 0, 1);

            NumericUpDown numeric = new NumericUpDown()
            {
                Value = options.PointRadius,
                Minimum = options.MinRadius,
                Maximum = options.MaxRadius,
                Font = font,
                Parent = table,
                Anchor = AnchorStyles.Left,
            };
            numeric.ValueChanged += num_round;
            table.Controls.Add(numeric, 1, 1);

            // настройка размера линии:
            label = new Label()
            {
                Text = "Толщина линии:",
                Font = font,
                Parent = table,
                Anchor = AnchorStyles.Left,
                AutoSize = true
            };
            table.Controls.Add(label, 0, 2);

            numeric = new NumericUpDown()
            {
                Value = options.LineWidth,
                Minimum = options.MinWidth,
                Maximum = options.MaxWidth,
                Font = font,
                Parent = table,
                Anchor = AnchorStyles.Left,
            };
            numeric.ValueChanged += num_widch;
            table.Controls.Add(numeric, 1, 2);

            // настройка характера движения фигуры:
            label = new Label()
            {
                Text = "Характер движения:",
                Font = font,
                Parent = table,
                Anchor = AnchorStyles.Left,
                AutoSize = true
            };
            table.Controls.Add(label, 0, 3);

            ComboBox comboBox = new ComboBox()
            {
                Font = font,
                Parent = table,
                Anchor = AnchorStyles.Left
            };
            comboBox.Width = 200;
            comboBox.Items.AddRange(new String[] { "случайный", "с сохранением формы"});
            comboBox.SelectedIndex = options.ChaoticMovement ? 0 : 1;
            comboBox.DropDownClosed += cBox_chaotic;
            table.Controls.Add(comboBox, 1, 3);

            // настройка скорости Vfx:
            label = new Label()
            {
                Text = nameof(Options.Vfx) + ":",
                Font = font,
                Parent = table,
                Anchor = AnchorStyles.Left,
                AutoSize = true
            };
            table.Controls.Add(label, 0, 4);

            numeric = new NumericUpDown()
            {
                Value = options.Vfx,
                Minimum = options.MinVf,
                Maximum = options.MaxVf,
                Font = font,
                Parent = table,
                Anchor = AnchorStyles.Left,
            };
            numeric.ValueChanged += num_Vfx;
            table.Controls.Add(numeric, 1, 4);

            // настройка скорости Vfy:
            label = new Label()
            {
                Text = nameof(Options.Vfy) + ":",
                Font = font,
                Parent = table,
                Anchor = AnchorStyles.Left,
                AutoSize = true
            };
            table.Controls.Add(label, 0, 5);

            numeric = new NumericUpDown()
            {
                Value = options.Vfy,
                Minimum = options.MinVf,
                Maximum = options.MaxVf,
                Font = font,
                Parent = table,
                Anchor = AnchorStyles.Left,
            };
            numeric.ValueChanged += num_Vfy;
            table.Controls.Add(numeric, 1, 5);

            table.ResumeLayout();
        }


        private void btn_color(object obj, EventArgs eArg)
        { 
            ColorDialog dialog = new ColorDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.Options.FigureColor = dialog.Color;
                lbl_color.BackColor = dialog.Color;
            }
        }

        private void num_round(object obj, EventArgs eArg)
        {
            NumericUpDown numeric = (NumericUpDown)obj;
            this.Options.PointRadius = (int)numeric.Value;
        }

        private void num_widch(object obj, EventArgs eArg)
        {
            NumericUpDown numeric = (NumericUpDown)obj;
            this.Options.LineWidth = (int)numeric.Value;
        }

        private void num_Vfx(object obj, EventArgs eArg)
        {
            NumericUpDown numeric = (NumericUpDown)obj;
            this.Options.Vfx = (int)numeric.Value;
        }

        private void num_Vfy(object obj, EventArgs eArg)
        {
            NumericUpDown numeric = (NumericUpDown)obj;
            this.Options.Vfy = (int)numeric.Value;
        }

        private void cBox_chaotic(object obj, EventArgs eArg)
        {
            ComboBox box = (ComboBox)obj;
            Options.ChaoticMovement = ("случайный".Equals(box.SelectedItem));    
        }
    }
}
