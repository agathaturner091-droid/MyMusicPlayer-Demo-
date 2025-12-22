namespace MusicPlayer
{
    partial class EditDetails
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditDetails));
            labelEdit = new Label();
            pictureBoxSave = new PictureBox();
            panelBorder = new Panel();
            panelBackground = new Panel();
            textBoxPlayListName = new TextBox();
            ((System.ComponentModel.ISupportInitialize)pictureBoxSave).BeginInit();
            panelBorder.SuspendLayout();
            panelBackground.SuspendLayout();
            SuspendLayout();
            // 
            // labelEdit
            // 
            labelEdit.AutoSize = true;
            labelEdit.Font = new Font("思源黑体 CN", 14.999999F, FontStyle.Bold, GraphicsUnit.Point, 134);
            labelEdit.ForeColor = Color.White;
            labelEdit.Location = new Point(27, 27);
            labelEdit.Name = "labelEdit";
            labelEdit.Size = new Size(157, 36);
            labelEdit.TabIndex = 0;
            labelEdit.Text = "Edit Details";
            // 
            // pictureBoxSave
            // 
            pictureBoxSave.Image = (Image)resources.GetObject("pictureBoxSave.Image");
            pictureBoxSave.Location = new Point(342, 167);
            pictureBoxSave.Name = "pictureBoxSave";
            pictureBoxSave.Size = new Size(128, 56);
            pictureBoxSave.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBoxSave.TabIndex = 6;
            pictureBoxSave.TabStop = false;
            // 
            // panelBorder
            // 
            panelBorder.Anchor = AnchorStyles.None;
            panelBorder.BackColor = Color.FromArgb(124, 124, 124);
            panelBorder.Controls.Add(panelBackground);
            panelBorder.Font = new Font("Microsoft YaHei UI", 10.5F);
            panelBorder.Location = new Point(108, 86);
            panelBorder.Name = "panelBorder";
            panelBorder.Padding = new Padding(1);
            panelBorder.Size = new Size(281, 61);
            panelBorder.TabIndex = 10;
            // 
            // panelBackground
            // 
            panelBackground.BackColor = Color.FromArgb(18, 18, 18);
            panelBackground.Controls.Add(textBoxPlayListName);
            panelBackground.Dock = DockStyle.Fill;
            panelBackground.Font = new Font("Microsoft YaHei UI", 10.5F);
            panelBackground.Location = new Point(1, 1);
            panelBackground.Name = "panelBackground";
            panelBackground.Padding = new Padding(10, 14, 1, 15);
            panelBackground.Size = new Size(279, 59);
            panelBackground.TabIndex = 10;
            // 
            // textBoxPlayListName
            // 
            textBoxPlayListName.BackColor = Color.FromArgb(18, 18, 18);
            textBoxPlayListName.BorderStyle = BorderStyle.None;
            textBoxPlayListName.Dock = DockStyle.Fill;
            textBoxPlayListName.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            textBoxPlayListName.ForeColor = Color.White;
            textBoxPlayListName.Location = new Point(10, 14);
            textBoxPlayListName.Multiline = true;
            textBoxPlayListName.Name = "textBoxPlayListName";
            textBoxPlayListName.Size = new Size(268, 30);
            textBoxPlayListName.TabIndex = 4;
            // 
            // EditDetails
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(40, 40, 40);
            ClientSize = new Size(503, 259);
            Controls.Add(panelBorder);
            Controls.Add(pictureBoxSave);
            Controls.Add(labelEdit);
            FormBorderStyle = FormBorderStyle.None;
            Name = "EditDetails";
            Text = "EditDetails";
            ((System.ComponentModel.ISupportInitialize)pictureBoxSave).EndInit();
            panelBorder.ResumeLayout(false);
            panelBackground.ResumeLayout(false);
            panelBackground.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelEdit;
        private PictureBox pictureBoxSave;
        private Panel panelBorder;
        private Panel panelBackground;
        private TextBox textBoxPlayListName;
    }
}