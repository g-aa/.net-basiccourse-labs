using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace Csh_Lab_3
{
    public enum LineType { None, Curved, Filled, Polygone, Beziers };

    public class PaintOptions
    {
        private int m_pointRadius;
        private int m_pointLocation;
        private int m_lineWidth;
        private int m_Vfx;
        private int m_Vfy;


        // основные поля для работы:
        public Color FigureColor { get; set; }
        public int PointRadius 
        { 
            get 
            {
                return m_pointRadius;
            }
            set 
            {
                if (MinRadius <= value && value <= MaxRadius)
                {
                    m_pointRadius = value;
                    PointLocation = value;
                }
            } 
        }
        public int PointLocation 
        {
            get 
            {
                return m_pointLocation;
            } 
            private set 
            {
                m_pointLocation = 2 * value;
            } 
        }
        public int LineWidth 
        {
            get
            {
                return m_lineWidth;
            }
            set
            {
                if (MinWidth <= value && value <= MaxWidth)
                {
                    m_lineWidth = value;
                }
            }
        }
        public bool ChaoticMovement { get; set; }
        public LineType LType { get; set; }
        public int Vfx 
        { 
            get 
            {
                return m_Vfx;
            }
            set 
            {
                if (MinVf <= value && value <= MaxVf)
                {
                    m_Vfx = value;
                }
            } 
        }
        public int Vfy 
        {
            get 
            {
                return m_Vfy;
            }
            set 
            {
                if (MinVf <= value && value <= MaxVf)
                {
                    m_Vfy = value;
                }
            } 
        }


        // вспомогательные:
        public int MaxWidth { get; private set; }
        public int MinWidth { get; private set; }
        public int MaxRadius { get; private set; }
        public int MinRadius { get; private set; }
        public int MinVf { get; private set; }
        public int MaxVf { get; private set; }


        // конструктор по умолчанию:
        public PaintOptions() 
        {
            MinRadius = 5;
            MaxRadius = 10;
            MinWidth = 1;
            MaxWidth = 5;
            MinVf = 0;
            MaxVf = 20;

            FigureColor = Color.Red;
            PointRadius = MinRadius;
            PointLocation = PointRadius;
            LineWidth = MinWidth;
            ChaoticMovement = true;
            LType = LineType.None;
            Vfx = 5;
            Vfy = 5;
        }

        public PaintOptions(PaintOptions other)
        {
            MinRadius = other.MinRadius;
            MaxRadius = other.MaxRadius;
            MinWidth = other.MinWidth;
            MaxWidth = other.MaxWidth;
            MinVf = other.MinVf;
            MaxVf = other.MaxVf;

            FigureColor = other.FigureColor;
            PointRadius = other.PointRadius;
            PointLocation = other.PointLocation;
            LineWidth = other.LineWidth;
            ChaoticMovement = other.ChaoticMovement;
            LType = other.LType;
            Vfx = other.Vfx;
            Vfy = other.Vfy;
        }
    }

    class MainForm : Form
    {
        private Font m_font;
        private Timer m_moveTimer;
        private PaintOptions m_options;
        private List<Point> m_points;
        private List<Point> m_dsChaotic;
        private Point m_dsSynchron;


        // флаги:
        private bool m_bAddPoints;
        private bool m_bMovePoint;


        // индексы:
        private int m_iMovePoint;

        public MainForm(Font font, string formTitle, Size startSize)
        { 
            // Форма:
            this.Text = formTitle;
            this.Font = font;
            this.AutoSize = true;
            this.AutoSizeMode = AutoSizeMode.GrowOnly;
            this.Size = startSize;
            this.DoubleBuffered = true;


            //вспомогательные данные:
            string[] btnsText = { "Точки", "Параметры", "Движение", "Очистить", "Кривая", "Ломанная", "Бейзеры", "Закрашенная" };
            Size btnsSize = new Size { Width = 120, Height = 40 };
            m_font = font;
            m_points = new List<Point>();
            m_dsChaotic = new List<Point>();
            m_options = new PaintOptions();
            m_bAddPoints = true;



            // обработчики формы:
            this.Paint += MainForm_paintPointsLines;
            this.KeyPreview = true;
            this.KeyDown += MainForm_KeyDown;
            this.MouseDown += MainForm_MouseDown;
            this.MouseUp += MainForm_MouseUp;
            this.MouseMove += MainForm_MouseMove;


            // настройка таймера:
            m_moveTimer = new Timer();
            m_moveTimer.Enabled = false;
            m_moveTimer.Interval = 30;
            m_moveTimer.Tick += MoveTimer_Tick;


            // компоненты формы:
            TableLayoutPanel table = new TableLayoutPanel
            {
                Parent = this,
                Padding = new Padding(Font.Height),
                AutoSize = true,
                RowCount = btnsText.Length,
                // CellBorderStyle = TableLayoutPanelCellBorderStyle.InsetDouble
            };
            table.Parent = this;
            table.SuspendLayout();

            // кнопки:
            EventHandler[] btn_clicked = new EventHandler[] { btn_point, btn_options, btn_move, btn_clear, btn_curve, btn_line, btn_beizers, btn_painted };
            for (int c = 0; c < btnsText.Length; c++)
            {
                Button btn = new Button
                {
                    Text = btnsText[c],
                    Font = font,
                    Parent = table,
                    Anchor = AnchorStyles.None,
                    Size = btnsSize
                };
                
                btn.Click += btn_clicked[c];
                table.Controls.Add(btn, 0, c);
            }
            table.ResumeLayout();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case (Keys.Up):
                    if (m_moveTimer.Enabled)
                    {
                        m_options.Vfy += 2;
                        SubRecalcSpeed();
                    }
                    else
                    {
                        SubManualMoveUpRecalcPos(-m_options.Vfy);
                    }
                    break;
                case (Keys.Down):
                    if (m_moveTimer.Enabled)
                    {
                        m_options.Vfy -= 2;
                        SubRecalcSpeed();
                    }
                    else
                    {
                        SubManualMoveDownRecalcPos(m_options.Vfy);
                    }
                    break;
                case (Keys.Right):
                    if (m_moveTimer.Enabled)
                    {
                        m_options.Vfx += 2;
                        SubRecalcSpeed();
                    }
                    else
                    {
                        SubManualMoveRightRecalcPos(m_options.Vfx);
                    }
                    break;
                case (Keys.Left):
                    if (m_moveTimer.Enabled)
                    {
                        m_options.Vfx -= 2;
                        SubRecalcSpeed();
                    }
                    else
                    {
                        SubManualMoveLeftRecalcPos(-m_options.Vfx);
                    }
                    break;
            }
            return true; //base.ProcessCmdKey(ref msg, keyData);
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case (Keys.Add):
                    if (m_moveTimer.Enabled)
                    {
                        m_options.Vfx += 2;
                        m_options.Vfy += 2;
                        SubRecalcSpeed();
                    }
                    break;
                case (Keys.Subtract):
                    if (m_moveTimer.Enabled)
                    {
                        m_options.Vfx -= 2;
                        m_options.Vfy -= 2;
                        SubRecalcSpeed();
                    }
                    break;
                case (Keys.Space):
                    if(m_points.Count > 0)
                    { 
                        if (SubMoveStartSpeed())
                        {
                            m_moveTimer.Enabled = !m_moveTimer.Enabled;
                        }
                    }
                    break;
                case (Keys.Escape):
                    SubClear();
                    break;
                default:
                    break;
            }
            e.Handled = true;
        }

        private void MoveTimer_Tick(object sender, EventArgs e)
        {
            if (m_options.ChaoticMovement)
            {
                SubChaoticMoveRecalcPos();
            }
            else 
            {
                SubSynchronMoveRecalcPos();
            }
        }


        #region buttons
        private void btn_point(object sender, EventArgs eArg)
        {
            m_moveTimer.Enabled = false;
            m_bAddPoints = !m_bAddPoints;
            m_options.LType = LineType.None;
            Refresh();
        }

        private void btn_options(object sender, EventArgs eArg)
        {
            m_bAddPoints = false;
            m_moveTimer.Enabled = false;
            OptionsForm optionsForm = new OptionsForm(m_font, m_options);
            optionsForm.ShowDialog();
            this.m_options = optionsForm.Options;
            this.Refresh();
        }

        private void btn_move(object sender, EventArgs eArg)
        {
            if (SubMoveStartSpeed())
            {
                m_moveTimer.Enabled = !m_moveTimer.Enabled;
            }
        }

        private void btn_clear(object sender, EventArgs eArg)
        {
            this.SubClear();
        }

        private void btn_curve(object sender, EventArgs eArg)
        {
            m_bAddPoints = false;
            if (m_points.Count >= 3)
            {
                m_options.LType = LineType.Curved;
                this.Refresh();
            }
            else
            {
                MessageBox.Show("Для построения сплайна требуется минимум 3 точки!", "Событие", MessageBoxButtons.OK);
            }
        }

        private void btn_line(object sender, EventArgs eArg)
        {
            m_bAddPoints = false;
            if (m_points.Count >= 2)
            {
                m_options.LType = LineType.Polygone;
                this.Refresh();
            }
            else
            {
                MessageBox.Show("Для построения требуется минимум 2 точки!", "Событие", MessageBoxButtons.OK);
            }
        }

        private void btn_beizers(object sender, EventArgs eArg)
        {
            m_bAddPoints = false;
            if (m_points.Count == 4)
            {
                m_options.LType = LineType.Beziers;
                Refresh();
            }
            else
            {
                MessageBox.Show("Кривую Бейзеры можно построить только для 4 точек!", "Событие", MessageBoxButtons.OK);
            }
        }

        private void btn_painted(object sender, EventArgs eArg)
        {
            m_bAddPoints = false;
            if (m_points.Count >= 3)
            {
                m_options.LType = LineType.Filled;
                this.Refresh();
            }
            else
            {
                MessageBox.Show("Для построения закрашенной фигуры требуется минимум 3 точки!", "Событие", MessageBoxButtons.OK);
            }
        }
        #endregion


        #region mouse
        private void MainForm_MouseDown(object sender, MouseEventArgs eArg)
        {
            if (m_bAddPoints) 
            {
                foreach (Point item in m_points)
                {
                    int dx = eArg.Location.X - item.X;
                    int dy = eArg.Location.Y - item.Y;
                    if (dx * dx + dy * dy <= m_options.PointLocation * m_options.PointLocation)
                    {
                        m_bMovePoint = true;
                        m_iMovePoint = m_points.IndexOf(item);
                        break;
                    }
                }

                if (!m_bMovePoint)
                {
                    m_points.Add(new Point(eArg.Location.X, eArg.Location.Y)); 
                }
                Refresh();
            }
        }

        private void MainForm_MouseUp(object sender, MouseEventArgs eArg)
        {
            m_bMovePoint = false;
        }

        private void MainForm_MouseMove(object sender, MouseEventArgs eArg) 
        {
            if (m_bMovePoint) 
            {
                m_points.RemoveAt(m_iMovePoint);
                m_points.Insert(m_iMovePoint, new Point(eArg.Location.X, eArg.Location.Y));
                Refresh();
            }
        }
        #endregion

        #region paintZone
        private void MainForm_paintPointsLines(object sender, PaintEventArgs e)
        {
            if (m_points.Count > 0)
            {
                Graphics g = e.Graphics;
                Brush brush = new SolidBrush(m_options.FigureColor);
                Pen pen = new Pen(brush, m_options.LineWidth);
                foreach (Point point in m_points)
                {
                    RectangleF r = new RectangleF { X = point.X - m_options.PointRadius, Y = point.Y - m_options.PointRadius, Height = m_options.PointRadius*2, Width = m_options.PointRadius*2 };
                    g.FillEllipse(brush, r);
                }

                switch (m_options.LType)
                {
                    case LineType.Polygone:
                        g.DrawPolygon(pen, m_points.ToArray());
                        break;
                    case LineType.Curved:
                        g.DrawClosedCurve(pen, m_points.ToArray());
                        break;
                    case LineType.Filled:
                        g.FillClosedCurve(brush, m_points.ToArray());
                        break;
                    case LineType.Beziers:
                        g.DrawBeziers(pen, m_points.ToArray());
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion


        #region subfunctions

        #region ManualMove
        private void SubManualMoveLeftRecalcPos(int dx)
        {
            if (m_points.Count > 0 && dx < 0)
            {
                Rectangle formSize = this.ClientRectangle;
                List<Point> tempPts = new List<Point>(m_points.Count);
                for (int i = 0; i < m_points.Count; i++)
                {
                    if (formSize.X + dx < m_points[i].X)
                    {
                        tempPts.Add(new Point(m_points[i].X + dx, m_points[i].Y));
                    }
                    else
                    {
                        return;
                    }
                }
                m_points.Clear();
                m_points.AddRange(tempPts);
                Refresh();
            }
            return;
        }

        private void SubManualMoveRightRecalcPos(int dx)
        {
            if (m_points.Count > 0 && dx > 0)
            {
                Rectangle formSize = this.ClientRectangle;
                List<Point> tempPts = new List<Point>(m_points.Count);
                for (int i = 0; i < m_points.Count; i++)
                {
                    if (m_points[i].X < formSize.Right - dx)
                    {
                        tempPts.Add(new Point(m_points[i].X + dx, m_points[i].Y));
                    }
                    else
                    {
                        return;
                    }
                }
                m_points.Clear();
                m_points.AddRange(tempPts);
                Refresh();
            }
            return;
        }

        private void SubManualMoveUpRecalcPos(int dy)
        {
            if (m_points.Count > 0 && dy < 0)
            {
                Rectangle formSize = this.ClientRectangle;
                List<Point> tempPts = new List<Point>(m_points.Count);
                for (int i = 0; i < m_points.Count; i++)
                {
                    if (formSize.Y + dy < m_points[i].Y)
                    {
                        tempPts.Add(new Point(m_points[i].X, m_points[i].Y + dy));
                    }
                    else
                    {
                        return;
                    }
                }
                m_points.Clear();
                m_points.AddRange(tempPts);
                Refresh();
            }
            return;
        }

        private void SubManualMoveDownRecalcPos(int dy)
        {
            if (m_points.Count > 0 && dy > 0)
            {
                Rectangle formSize = this.ClientRectangle;
                List<Point> tempPts = new List<Point>(m_points.Count);
                for (int i = 0; i < m_points.Count; i++)
                {
                    if (m_points[i].Y < formSize.Bottom - dy)
                    {
                        tempPts.Add(new Point(m_points[i].X, m_points[i].Y + dy));
                    }
                    else
                    {
                        return;
                    }
                }
                m_points.Clear();
                m_points.AddRange(tempPts);
                Refresh();
            }
            return;
        }
        #endregion

        #region ChaoticSynchronMove
        private bool SubMoveStartSpeed()
        {
            m_bAddPoints = false;
            if (m_points.Count > 0)
            {
                Random random = new Random();
                Func<Random, int> FunctRand = r => (r.Next(1, 100) % 2 != 0) ? -1 : 1;
                if (m_options.ChaoticMovement && (m_points.Count != m_dsChaotic.Count))
                {
                    
                    for (int i = m_dsChaotic.Count; i < m_points.Count; i++)
                    {
                        m_dsChaotic.Add(new Point(FunctRand(random) * m_options.Vfx, FunctRand(random) * m_options.Vfy));
                    }
                }
                else if (!m_options.ChaoticMovement)
                {
                    m_dsSynchron.X = FunctRand(random) * m_options.Vfx;
                    m_dsSynchron.Y = FunctRand(random) * m_options.Vfy;
                }
                return true;
            }
            else
            {
                MyMessage("Для включения режима перемещения требуется хотябы 1 точка!");
            }
            return false;
        }

        private void SubChaoticMoveRecalcPos()
        {
            Rectangle formSize = this.ClientRectangle;
            for (int i = 0; i < m_points.Count; i++)
            {
                Point tempPts = m_points[i];
                Point tempDs = m_dsChaotic[i];
                if (m_points[i].X <= formSize.X)
                {
                    tempDs.X = (m_dsChaotic[i].X > 0) ? m_dsChaotic[i].X : -m_dsChaotic[i].X;
                    tempPts.X = formSize.X;
                }
                else if (m_points[i].X >= formSize.Right)
                {
                    tempDs.X = (m_dsChaotic[i].X < 0) ? m_dsChaotic[i].X : -m_dsChaotic[i].X;
                    tempPts.X = formSize.Right;
                }

                if (m_points[i].Y <= formSize.Y)
                {
                    tempDs.Y = (m_dsChaotic[i].Y > 0) ? m_dsChaotic[i].Y : -m_dsChaotic[i].Y;
                    tempPts.Y = formSize.Y;
                }
                else if (m_points[i].Y >= formSize.Bottom)
                {
                    tempDs.Y = (m_dsChaotic[i].Y < 0) ? m_dsChaotic[i].Y : -m_dsChaotic[i].Y;
                    tempPts.Y = formSize.Bottom;
                }

                if (tempDs.X != m_dsChaotic[i].X || tempDs.Y != m_dsChaotic[i].Y)
                {
                    m_dsChaotic[i] = tempDs;
                }
                m_points[i] = new Point(tempPts.X + m_dsChaotic[i].X, tempPts.Y + m_dsChaotic[i].Y);
            }
            Refresh();
        }

        private void SubSynchronMoveRecalcPos()
        {
            Rectangle formSize = this.ClientRectangle;
            bool flag = false;
            foreach (Point item in m_points)
            {
                if (item.X <= formSize.X || item.X >= formSize.Right)
                {
                    m_dsSynchron.X = (m_dsSynchron.X > 0) ? -m_options.Vfx : m_options.Vfx;
                    flag = true;
                }

                if (item.Y <= formSize.Y || item.Y >= formSize.Bottom)
                {
                    m_dsSynchron.Y = (m_dsSynchron.Y > 0) ? -m_options.Vfy : m_options.Vfy;
                    flag = true;
                }

                if (flag)
                {
                    break;
                }
            }

            for (int i = 0; i < m_points.Count; i++)
            {
                Point tempPts = m_points[i];
                if (m_points[i].X <= formSize.X)
                {
                    tempPts.X = formSize.X;
                }
                else if (m_points[i].X >= formSize.Right)
                {
                    tempPts.X = formSize.Right;
                }

                if (m_points[i].Y <= formSize.Y)
                {
                    tempPts.Y = formSize.Y;
                }
                else if (m_points[i].Y >= formSize.Bottom)
                {
                    tempPts.Y = formSize.Bottom;
                }
                m_points[i] = new Point(tempPts.X + m_dsSynchron.X, tempPts.Y + m_dsSynchron.Y);
            }
            Refresh();
        }

        private void SubRecalcSpeed()
        {
            if (m_options.ChaoticMovement)
            {
                for (int i = 0; i < m_dsChaotic.Count; i++)
                {
                    m_dsChaotic[i] = new Point((m_dsChaotic[i].X > 0) ? m_options.Vfx : -m_options.Vfx, (m_dsChaotic[i].Y > 0) ? m_options.Vfy : -m_options.Vfy);
                }
            }
            else 
            {
                m_dsSynchron.X = m_dsSynchron.X > 0 ? m_options.Vfx : -m_options.Vfx;
                m_dsSynchron.Y = m_dsSynchron.Y > 0 ? m_options.Vfy : -m_options.Vfy;
            }
        }

        #endregion

        private void SubClear()
        {
            this.m_bAddPoints = false;
            this.m_moveTimer.Enabled = false;
            this.m_points.Clear();
            this.m_dsChaotic.Clear();
            this.m_options.LType = LineType.None;
            this.Refresh();
        }

        private void MyMessage(string s)
        {
            MessageBox.Show(s, "Событие", MessageBoxButtons.OK);
        }

        #endregion
    }
}
