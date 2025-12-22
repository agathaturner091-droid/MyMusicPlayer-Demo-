namespace MusicPlayer
{
    partial class RightClickMenu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RightClickMenu));
            btnAdd = new Button();
            panelAdd = new Panel();
            pictureBoxAdd = new PictureBox();
            panelRemove = new Panel();
            pictureBoxRemove = new PictureBox();
            btnRemove = new Button();
            panelShare = new Panel();
            pictureBoxShare = new PictureBox();
            btnShare = new Button();
            panelAdd.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxAdd).BeginInit();
            panelRemove.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxRemove).BeginInit();
            panelShare.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxShare).BeginInit();
            SuspendLayout();
            // 
            // btnAdd
            // 
            btnAdd.BackColor = Color.FromArgb(40, 40, 40);
            btnAdd.FlatAppearance.BorderSize = 0;
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.Font = new Font("思源黑体 CN", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 134);
            btnAdd.ForeColor = Color.FromArgb(177, 177, 177);
            btnAdd.Location = new Point(51, 6);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(216, 50);
            btnAdd.TabIndex = 0;
            btnAdd.Text = "Add to playlist";
            btnAdd.TextAlign = ContentAlignment.MiddleLeft;
            btnAdd.UseVisualStyleBackColor = false;
            btnAdd.Click += btnAdd_Click;
            // 
            // panelAdd
            // 
            panelAdd.Controls.Add(pictureBoxAdd);
            panelAdd.Controls.Add(btnAdd);
            panelAdd.Location = new Point(4, 5);
            panelAdd.Name = "panelAdd";
            panelAdd.Size = new Size(270, 62);
            panelAdd.TabIndex = 1;
            // 
            // pictureBoxAdd
            // 
            pictureBoxAdd.Image = (Image)resources.GetObject("pictureBoxAdd.Image");
            pictureBoxAdd.Location = new Point(14, 13);
            pictureBoxAdd.Name = "pictureBoxAdd";
            pictureBoxAdd.Size = new Size(35, 35);
            pictureBoxAdd.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBoxAdd.TabIndex = 1;
            pictureBoxAdd.TabStop = false;
            // 
            // panelRemove
            // 
            panelRemove.Controls.Add(pictureBoxRemove);
            panelRemove.Controls.Add(btnRemove);
            panelRemove.Location = new Point(4, 68);
            panelRemove.Name = "panelRemove";
            panelRemove.Size = new Size(270, 62);
            panelRemove.TabIndex = 1;
            // 
            // pictureBoxRemove
            // 
            pictureBoxRemove.Image = (Image)resources.GetObject("pictureBoxRemove.Image");
            pictureBoxRemove.Location = new Point(14, 13);
            pictureBoxRemove.Name = "pictureBoxRemove";
            pictureBoxRemove.Size = new Size(35, 35);
            pictureBoxRemove.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBoxRemove.TabIndex = 1;
            pictureBoxRemove.TabStop = false;
            // 
            // btnRemove
            // 
            btnRemove.BackColor = Color.FromArgb(40, 40, 40);
            btnRemove.FlatAppearance.BorderSize = 0;
            btnRemove.FlatStyle = FlatStyle.Flat;
            btnRemove.Font = new Font("思源黑体 CN", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 134);
            btnRemove.ForeColor = Color.FromArgb(177, 177, 177);
            btnRemove.Location = new Point(51, 6);
            btnRemove.Name = "btnRemove";
            btnRemove.Size = new Size(216, 50);
            btnRemove.TabIndex = 0;
            btnRemove.Text = "Remove from playlist";
            btnRemove.TextAlign = ContentAlignment.MiddleLeft;
            btnRemove.UseVisualStyleBackColor = false;
            btnRemove.Click += btnRemove_Click;
            // 
            // panelShare
            // 
            panelShare.Controls.Add(pictureBoxShare);
            panelShare.Controls.Add(btnShare);
            panelShare.Location = new Point(4, 131);
            panelShare.Name = "panelShare";
            panelShare.Size = new Size(270, 62);
            panelShare.TabIndex = 1;
            // 
            // pictureBoxShare
            // 
            pictureBoxShare.Image = (Image)resources.GetObject("pictureBoxShare.Image");
            pictureBoxShare.Location = new Point(14, 13);
            pictureBoxShare.Name = "pictureBoxShare";
            pictureBoxShare.Size = new Size(35, 35);
            pictureBoxShare.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBoxShare.TabIndex = 1;
            pictureBoxShare.TabStop = false;
            // 
            // btnShare
            // 
            btnShare.BackColor = Color.FromArgb(40, 40, 40);
            btnShare.FlatAppearance.BorderSize = 0;
            btnShare.FlatStyle = FlatStyle.Flat;
            btnShare.Font = new Font("思源黑体 CN", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 134);
            btnShare.ForeColor = Color.FromArgb(177, 177, 177);
            btnShare.Location = new Point(51, 6);
            btnShare.Name = "btnShare";
            btnShare.Size = new Size(216, 50);
            btnShare.TabIndex = 0;
            btnShare.Text = "Share";
            btnShare.TextAlign = ContentAlignment.MiddleLeft;
            btnShare.UseVisualStyleBackColor = false;
            btnShare.Click += btnShare_Click;
            // 
            // RightClickMenu
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(40, 40, 40);
            ClientSize = new Size(277, 198);
            Controls.Add(panelShare);
            Controls.Add(panelRemove);
            Controls.Add(panelAdd);
            FormBorderStyle = FormBorderStyle.None;
            Name = "RightClickMenu";
            ShowIcon = false;
            Text = "RightClickMenu";
            panelAdd.ResumeLayout(false);
            panelAdd.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxAdd).EndInit();
            panelRemove.ResumeLayout(false);
            panelRemove.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxRemove).EndInit();
            panelShare.ResumeLayout(false);
            panelShare.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxShare).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button btnAdd;
        private Panel panelAdd;
        private PictureBox pictureBoxAdd;
        private Panel panelRemove;
        private PictureBox pictureBoxRemove;
        private Button btnRemove;
        private Panel panelShare;
        private PictureBox pictureBoxShare;
        private Button btnShare;
    }
}