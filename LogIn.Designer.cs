namespace MusicPlayer
{
    partial class LogIn
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogIn));
            ucNormal = new UC_NormalLogin();
            SuspendLayout();
            // 
            // ucNormal
            // 
            ucNormal.BackColor = Color.FromArgb(18, 18, 18);
            ucNormal.Location = new Point(-2, -3);
            ucNormal.MaximumSize = new Size(574, 411);
            ucNormal.MinimumSize = new Size(574, 411);
            ucNormal.Name = "ucNormal";
            ucNormal.Size = new Size(574, 411);
            ucNormal.TabIndex = 0;
            // 
            // LogIn
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(18, 18, 18);
            ClientSize = new Size(574, 411);
            ControlBox = false;
            Controls.Add(ucNormal);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "LogIn";
            Text = "MeloVibe";
            ResumeLayout(false);
        }

        #endregion

        private UC_NormalLogin ucNormal;
    }
}