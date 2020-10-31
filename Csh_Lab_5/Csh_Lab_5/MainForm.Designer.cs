namespace Csh_Lab_5
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.m_pictureFlag = new System.Windows.Forms.PictureBox();
            this.m_dataGrid = new System.Windows.Forms.DataGridView();
            this.m_chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.m_propertyGrid = new System.Windows.Forms.PropertyGrid();
            this.bindingNavigator1 = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorAddNewItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorDeleteItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.m_toolBtnClear = new System.Windows.Forms.ToolStripButton();
            this.m_toolBtnOpen = new System.Windows.Forms.ToolStripButton();
            this.m_toolBtnSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.m_toolTxtBox = new System.Windows.Forms.ToolStripTextBox();
            this.m_toolBtnSearch = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.m_pictureSymbol = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.m_pictureFlag)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dataGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_chart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator1)).BeginInit();
            this.bindingNavigator1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_pictureSymbol)).BeginInit();
            this.SuspendLayout();
            // 
            // m_pictureFlag
            // 
            this.m_pictureFlag.Location = new System.Drawing.Point(623, 28);
            this.m_pictureFlag.Name = "m_pictureFlag";
            this.m_pictureFlag.Size = new System.Drawing.Size(306, 223);
            this.m_pictureFlag.TabIndex = 1;
            this.m_pictureFlag.TabStop = false;
            // 
            // m_dataGrid
            // 
            this.m_dataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.m_dataGrid.Location = new System.Drawing.Point(0, 28);
            this.m_dataGrid.Name = "m_dataGrid";
            this.m_dataGrid.Size = new System.Drawing.Size(617, 300);
            this.m_dataGrid.TabIndex = 2;
            // 
            // m_chart
            // 
            chartArea1.Name = "ChartArea1";
            this.m_chart.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.m_chart.Legends.Add(legend1);
            this.m_chart.Location = new System.Drawing.Point(0, 334);
            this.m_chart.Name = "m_chart";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.m_chart.Series.Add(series1);
            this.m_chart.Size = new System.Drawing.Size(617, 265);
            this.m_chart.TabIndex = 3;
            this.m_chart.Text = "chart1";
            // 
            // m_propertyGrid
            // 
            this.m_propertyGrid.Location = new System.Drawing.Point(623, 257);
            this.m_propertyGrid.Name = "m_propertyGrid";
            this.m_propertyGrid.Size = new System.Drawing.Size(533, 342);
            this.m_propertyGrid.TabIndex = 4;
            // 
            // bindingNavigator1
            // 
            this.bindingNavigator1.AddNewItem = this.bindingNavigatorAddNewItem;
            this.bindingNavigator1.CountItem = this.bindingNavigatorCountItem;
            this.bindingNavigator1.CountItemFormat = "из {0}";
            this.bindingNavigator1.DeleteItem = this.bindingNavigatorDeleteItem;
            this.bindingNavigator1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2,
            this.bindingNavigatorAddNewItem,
            this.bindingNavigatorDeleteItem,
            this.m_toolBtnClear,
            this.m_toolBtnOpen,
            this.m_toolBtnSave,
            this.toolStripSeparator1,
            this.m_toolTxtBox,
            this.m_toolBtnSearch,
            this.toolStripSeparator2});
            this.bindingNavigator1.Location = new System.Drawing.Point(0, 0);
            this.bindingNavigator1.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.bindingNavigator1.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.bindingNavigator1.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.bindingNavigator1.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.bindingNavigator1.Name = "bindingNavigator1";
            this.bindingNavigator1.PositionItem = this.bindingNavigatorPositionItem;
            this.bindingNavigator1.Size = new System.Drawing.Size(1156, 25);
            this.bindingNavigator1.TabIndex = 6;
            this.bindingNavigator1.Text = "bindingNavigator1";
            // 
            // bindingNavigatorAddNewItem
            // 
            this.bindingNavigatorAddNewItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorAddNewItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorAddNewItem.Image")));
            this.bindingNavigatorAddNewItem.Name = "bindingNavigatorAddNewItem";
            this.bindingNavigatorAddNewItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorAddNewItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorAddNewItem.Text = "Добавить";
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(36, 22);
            this.bindingNavigatorCountItem.Text = "из {0}";
            this.bindingNavigatorCountItem.ToolTipText = "Общее число элементов";
            // 
            // bindingNavigatorDeleteItem
            // 
            this.bindingNavigatorDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorDeleteItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorDeleteItem.Image")));
            this.bindingNavigatorDeleteItem.Name = "bindingNavigatorDeleteItem";
            this.bindingNavigatorDeleteItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorDeleteItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorDeleteItem.Text = "Удалить";
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveFirstItem.Text = "Переместить в начало";
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMovePreviousItem.Text = "Переместить назад";
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.AccessibleName = "Положение";
            this.bindingNavigatorPositionItem.AutoSize = false;
            this.bindingNavigatorPositionItem.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(50, 23);
            this.bindingNavigatorPositionItem.Text = "0";
            this.bindingNavigatorPositionItem.ToolTipText = "Текущее положение";
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveNextItem.Text = "Переместить вперед";
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveLastItem.Text = "Переместить в конец";
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // m_toolBtnClear
            // 
            this.m_toolBtnClear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_toolBtnClear.Image = ((System.Drawing.Image)(resources.GetObject("m_toolBtnClear.Image")));
            this.m_toolBtnClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_toolBtnClear.Name = "m_toolBtnClear";
            this.m_toolBtnClear.Size = new System.Drawing.Size(23, 22);
            this.m_toolBtnClear.Text = "Пустой список";
            // 
            // m_toolBtnOpen
            // 
            this.m_toolBtnOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_toolBtnOpen.Image = ((System.Drawing.Image)(resources.GetObject("m_toolBtnOpen.Image")));
            this.m_toolBtnOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_toolBtnOpen.Name = "m_toolBtnOpen";
            this.m_toolBtnOpen.Size = new System.Drawing.Size(23, 22);
            this.m_toolBtnOpen.Text = "Открыть список";
            // 
            // m_toolBtnSave
            // 
            this.m_toolBtnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_toolBtnSave.Image = ((System.Drawing.Image)(resources.GetObject("m_toolBtnSave.Image")));
            this.m_toolBtnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_toolBtnSave.Name = "m_toolBtnSave";
            this.m_toolBtnSave.Size = new System.Drawing.Size(23, 22);
            this.m_toolBtnSave.Text = "Сохранить список";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // m_toolTxtBox
            // 
            this.m_toolTxtBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.m_toolTxtBox.Name = "m_toolTxtBox";
            this.m_toolTxtBox.Size = new System.Drawing.Size(100, 25);
            // 
            // m_toolBtnSearch
            // 
            this.m_toolBtnSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_toolBtnSearch.Image = ((System.Drawing.Image)(resources.GetObject("m_toolBtnSearch.Image")));
            this.m_toolBtnSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_toolBtnSearch.Name = "m_toolBtnSearch";
            this.m_toolBtnSearch.Size = new System.Drawing.Size(23, 22);
            this.m_toolBtnSearch.Text = "Поиск";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // m_pictureSymbol
            // 
            this.m_pictureSymbol.Location = new System.Drawing.Point(935, 28);
            this.m_pictureSymbol.Name = "m_pictureSymbol";
            this.m_pictureSymbol.Size = new System.Drawing.Size(221, 223);
            this.m_pictureSymbol.TabIndex = 7;
            this.m_pictureSymbol.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1156, 601);
            this.Controls.Add(this.m_pictureSymbol);
            this.Controls.Add(this.bindingNavigator1);
            this.Controls.Add(this.m_propertyGrid);
            this.Controls.Add(this.m_chart);
            this.Controls.Add(this.m_dataGrid);
            this.Controls.Add(this.m_pictureFlag);
            this.Name = "MainForm";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.m_pictureFlag)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_dataGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_chart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator1)).EndInit();
            this.bindingNavigator1.ResumeLayout(false);
            this.bindingNavigator1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_pictureSymbol)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox m_pictureFlag;
        private System.Windows.Forms.PictureBox m_pictureSymbol;
        private System.Windows.Forms.DataGridView m_dataGrid;
        private System.Windows.Forms.DataVisualization.Charting.Chart m_chart;
        private System.Windows.Forms.PropertyGrid m_propertyGrid;
        private System.Windows.Forms.BindingNavigator bindingNavigator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorAddNewItem;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorDeleteItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.ToolStripButton m_toolBtnClear;
        private System.Windows.Forms.ToolStripButton m_toolBtnOpen;
        private System.Windows.Forms.ToolStripButton m_toolBtnSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripTextBox m_toolTxtBox;
        private System.Windows.Forms.ToolStripButton m_toolBtnSearch;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    }
}