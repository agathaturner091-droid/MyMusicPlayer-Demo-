namespace MusicPlayer
{
    partial class OpenFiles
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OpenFiles));
            btnSearch = new Button();
            dataGridViewPlayList = new DataGridView();
            panelBackgroundHigh = new Panel();
            labelAdd = new Label();
            panelMiddleLine = new Panel();
            labelSearch = new Label();
            panelBackgroundLow = new Panel();
            panelSecondBackgroundHigh = new Panel();
            ((System.ComponentModel.ISupportInitialize)dataGridViewPlayList).BeginInit();
            panelBackgroundHigh.SuspendLayout();
            SuspendLayout();
            // 
            // btnSearch
            // 
            btnSearch.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSearch.BackColor = Color.Transparent;
            btnSearch.FlatAppearance.BorderSize = 0;
            btnSearch.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnSearch.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.Image = (Image)resources.GetObject("btnSearch.Image");
            btnSearch.Location = new Point(1091, 72);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(30, 30);
            btnSearch.TabIndex = 6;
            btnSearch.UseVisualStyleBackColor = false;
            btnSearch.Click += btnSearch_Click_1;
            // 
            // dataGridViewPlayList
            // 
            dataGridViewPlayList.AllowUserToAddRows = false;
            dataGridViewPlayList.AllowUserToDeleteRows = false;
            dataGridViewPlayList.AllowUserToResizeColumns = false;
            dataGridViewPlayList.AllowUserToResizeRows = false;
            dataGridViewPlayList.BackgroundColor = Color.FromArgb(18, 18, 18);
            dataGridViewPlayList.BorderStyle = BorderStyle.None;
            dataGridViewPlayList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewPlayList.Dock = DockStyle.Fill;
            dataGridViewPlayList.EnableHeadersVisualStyles = false;
            dataGridViewPlayList.GridColor = Color.FromArgb(86, 94, 102);
            dataGridViewPlayList.Location = new Point(0, 314);
            dataGridViewPlayList.MultiSelect = false;
            dataGridViewPlayList.Name = "dataGridViewPlayList";
            dataGridViewPlayList.ReadOnly = true;
            dataGridViewPlayList.RowHeadersWidth = 51;
            dataGridViewPlayList.RowTemplate.Height = 35;
            dataGridViewPlayList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewPlayList.Size = new Size(1281, 359);
            dataGridViewPlayList.TabIndex = 2;
            dataGridViewPlayList.CellDoubleClick += dataGridViewPlayList_CellDoubleClick;
            dataGridViewPlayList.CellMouseClick += dataGridViewPlayList_CellMouseClick;
            dataGridViewPlayList.CellMouseEnter += dataGridViewPlayList_CellMouseEnter;
            dataGridViewPlayList.CellMouseLeave += dataGridViewPlayList_CellMouseLeave;
            dataGridViewPlayList.CellPainting += dataGridViewPlayList_CellPainting;
            dataGridViewPlayList.MouseDown += dataGridViewPlayList_MouseDown;
            // 
            // panelBackgroundHigh
            // 
            panelBackgroundHigh.BackColor = Color.FromArgb(18, 18, 18);
            panelBackgroundHigh.Controls.Add(labelAdd);
            panelBackgroundHigh.Dock = DockStyle.Top;
            panelBackgroundHigh.Location = new Point(0, 0);
            panelBackgroundHigh.Name = "panelBackgroundHigh";
            panelBackgroundHigh.Size = new Size(1281, 157);
            panelBackgroundHigh.TabIndex = 7;
            panelBackgroundHigh.Paint += panelbackground_Paint;
            // 
            // labelAdd
            // 
            labelAdd.Anchor = AnchorStyles.Top;
            labelAdd.AutoSize = true;
            labelAdd.BackColor = Color.Transparent;
            labelAdd.Font = new Font("思源黑体 CN", 13F, FontStyle.Bold);
            labelAdd.ForeColor = Color.FromArgb(167, 167, 167);
            labelAdd.Location = new Point(507, 67);
            labelAdd.Name = "labelAdd";
            labelAdd.Size = new Size(266, 32);
            labelAdd.TabIndex = 9;
            labelAdd.Text = "Let's add some songs！";
            // 
            // panelMiddleLine
            // 
            panelMiddleLine.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panelMiddleLine.BackColor = Color.FromArgb(47, 47, 47);
            panelMiddleLine.Location = new Point(0, 150);
            panelMiddleLine.Name = "panelMiddleLine";
            panelMiddleLine.Size = new Size(1281, 1);
            panelMiddleLine.TabIndex = 11;
            // 
            // labelSearch
            // 
            labelSearch.Anchor = AnchorStyles.Top;
            labelSearch.AutoSize = true;
            labelSearch.BackColor = Color.FromArgb(18, 18, 18);
            labelSearch.Cursor = Cursors.Hand;
            labelSearch.Font = new Font("思源黑体 CN", 13F, FontStyle.Bold);
            labelSearch.ForeColor = Color.White;
            labelSearch.Location = new Point(935, 214);
            labelSearch.Name = "labelSearch";
            labelSearch.Size = new Size(217, 32);
            labelSearch.TabIndex = 9;
            labelSearch.Text = "FIND SOME SONGS";
            // 
            // panelBackgroundLow
            // 
            panelBackgroundLow.BackColor = Color.FromArgb(18, 18, 18);
            panelBackgroundLow.Dock = DockStyle.Fill;
            panelBackgroundLow.Location = new Point(0, 314);
            panelBackgroundLow.Name = "panelBackgroundLow";
            panelBackgroundLow.Size = new Size(1281, 359);
            panelBackgroundLow.TabIndex = 7;
            // 
            // panelSecondBackgroundHigh
            // 
            panelSecondBackgroundHigh.BackColor = Color.FromArgb(18, 18, 18);
            panelSecondBackgroundHigh.Dock = DockStyle.Top;
            panelSecondBackgroundHigh.Location = new Point(0, 157);
            panelSecondBackgroundHigh.Name = "panelSecondBackgroundHigh";
            panelSecondBackgroundHigh.Size = new Size(1281, 157);
            panelSecondBackgroundHigh.TabIndex = 10;
            // 
            // OpenFiles
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            ClientSize = new Size(1281, 673);
            Controls.Add(labelSearch);
            Controls.Add(btnSearch);
            Controls.Add(panelBackgroundLow);
            Controls.Add(dataGridViewPlayList);
            Controls.Add(panelSecondBackgroundHigh);
            Controls.Add(panelBackgroundHigh);
            Controls.Add(panelMiddleLine);
            FormBorderStyle = FormBorderStyle.None;
            Name = "OpenFiles";
            Text = "Form2";
            ((System.ComponentModel.ISupportInitialize)dataGridViewPlayList).EndInit();
            panelBackgroundHigh.ResumeLayout(false);
            panelBackgroundHigh.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button btnSearch;
        private Panel panelBackgroundHigh;
        private Label labelSearch;
        private Label labelAdd;
        private Panel panelMiddleLine;
        private Panel panelBackgroundLow;
        private Panel panelSecondBackgroundHigh;
        internal DataGridView dataGridViewPlayList;
    }
}