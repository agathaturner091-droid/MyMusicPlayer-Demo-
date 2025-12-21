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
            textBoxAccountPlayListName = new TextBox();
            pictureBoxSave = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBoxSave).BeginInit();
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
            // textBoxAccountPlayListName
            // 
            textBoxAccountPlayListName.BackColor = Color.FromArgb(62, 62, 62);
            textBoxAccountPlayListName.BorderStyle = BorderStyle.None;
            textBoxAccountPlayListName.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            textBoxAccountPlayListName.ForeColor = Color.White;
            textBoxAccountPlayListName.Location = new Point(111, 90);
            textBoxAccountPlayListName.Multiline = true;
            textBoxAccountPlayListName.Name = "textBoxAccountPlayListName";
            textBoxAccountPlayListName.Size = new Size(272, 49);
            textBoxAccountPlayListName.TabIndex = 5;
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
            // EditDetails
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(40, 40, 40);
            ClientSize = new Size(503, 259);
            Controls.Add(pictureBoxSave);
            Controls.Add(textBoxAccountPlayListName);
            Controls.Add(labelEdit);
            FormBorderStyle = FormBorderStyle.None;
            Name = "EditDetails";
            Text = "EditDetails";
            ((System.ComponentModel.ISupportInitialize)pictureBoxSave).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelEdit;
        private TextBox textBoxAccountPlayListName;
        private PictureBox pictureBoxSave;
    }
}