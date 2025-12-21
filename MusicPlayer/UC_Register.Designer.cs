namespace MusicPlayer
{
    partial class UC_Register
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UC_Register));
            labelBack = new Label();
            labelClose = new Label();
            btnLogIn = new Button();
            panelBorderPassword = new Panel();
            panelBackgroundPassword = new Panel();
            textBoxPassword = new TextBox();
            panelBorder = new Panel();
            panelBackground = new Panel();
            textBoxAccount = new TextBox();
            labelCreatPassword = new Label();
            labelAcountText = new Label();
            labelWelcome = new Label();
            pictureBoxOnlyLogoWhiteBlack = new PictureBox();
            labelUsername = new Label();
            panelBorderUsername = new Panel();
            panelBackgroundUsername = new Panel();
            textBoxUsername = new TextBox();
            panelBorderPassword.SuspendLayout();
            panelBackgroundPassword.SuspendLayout();
            panelBorder.SuspendLayout();
            panelBackground.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxOnlyLogoWhiteBlack).BeginInit();
            panelBorderUsername.SuspendLayout();
            panelBackgroundUsername.SuspendLayout();
            SuspendLayout();
            // 
            // labelBack
            // 
            labelBack.AutoSize = true;
            labelBack.Font = new Font("Microsoft YaHei UI", 10F);
            labelBack.ForeColor = Color.White;
            labelBack.Location = new Point(20, 16);
            labelBack.Name = "labelBack";
            labelBack.Size = new Size(23, 23);
            labelBack.TabIndex = 21;
            labelBack.Text = "<";
            labelBack.Click += labelBack_Click;
            // 
            // labelClose
            // 
            labelClose.AutoSize = true;
            labelClose.Font = new Font("Microsoft YaHei UI", 7.8F, FontStyle.Regular, GraphicsUnit.Point, 134);
            labelClose.ForeColor = Color.White;
            labelClose.Location = new Point(448, 18);
            labelClose.Name = "labelClose";
            labelClose.Size = new Size(20, 19);
            labelClose.TabIndex = 22;
            labelClose.Text = "✕";
            labelClose.Click += labelClose_Click;
            // 
            // btnLogIn
            // 
            btnLogIn.Anchor = AnchorStyles.None;
            btnLogIn.BackgroundImageLayout = ImageLayout.None;
            btnLogIn.FlatAppearance.BorderSize = 0;
            btnLogIn.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnLogIn.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnLogIn.FlatStyle = FlatStyle.Flat;
            btnLogIn.Font = new Font("思源黑体 CN", 13.7999992F, FontStyle.Bold, GraphicsUnit.Point, 134);
            btnLogIn.ForeColor = Color.Black;
            btnLogIn.Image = (Image)resources.GetObject("btnLogIn.Image");
            btnLogIn.Location = new Point(41, 571);
            btnLogIn.Name = "btnLogIn";
            btnLogIn.Size = new Size(394, 51);
            btnLogIn.TabIndex = 20;
            btnLogIn.UseVisualStyleBackColor = false;
            btnLogIn.Click += btnLogIn_Click;
            // 
            // panelBorderPassword
            // 
            panelBorderPassword.Anchor = AnchorStyles.None;
            panelBorderPassword.BackColor = Color.FromArgb(124, 124, 124);
            panelBorderPassword.Controls.Add(panelBackgroundPassword);
            panelBorderPassword.Font = new Font("Microsoft YaHei UI", 10.5F);
            panelBorderPassword.Location = new Point(43, 355);
            panelBorderPassword.Name = "panelBorderPassword";
            panelBorderPassword.Padding = new Padding(1);
            panelBorderPassword.Size = new Size(394, 61);
            panelBorderPassword.TabIndex = 18;
            // 
            // panelBackgroundPassword
            // 
            panelBackgroundPassword.BackColor = Color.FromArgb(18, 18, 18);
            panelBackgroundPassword.Controls.Add(textBoxPassword);
            panelBackgroundPassword.Dock = DockStyle.Fill;
            panelBackgroundPassword.Font = new Font("Microsoft YaHei UI", 10.5F);
            panelBackgroundPassword.Location = new Point(1, 1);
            panelBackgroundPassword.Name = "panelBackgroundPassword";
            panelBackgroundPassword.Padding = new Padding(10, 14, 1, 15);
            panelBackgroundPassword.Size = new Size(392, 59);
            panelBackgroundPassword.TabIndex = 10;
            // 
            // textBoxPassword
            // 
            textBoxPassword.BackColor = Color.FromArgb(18, 18, 18);
            textBoxPassword.BorderStyle = BorderStyle.None;
            textBoxPassword.Dock = DockStyle.Fill;
            textBoxPassword.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            textBoxPassword.ForeColor = Color.White;
            textBoxPassword.Location = new Point(10, 14);
            textBoxPassword.Multiline = true;
            textBoxPassword.Name = "textBoxPassword";
            textBoxPassword.Size = new Size(381, 30);
            textBoxPassword.TabIndex = 4;
            // 
            // panelBorder
            // 
            panelBorder.Anchor = AnchorStyles.None;
            panelBorder.BackColor = Color.FromArgb(124, 124, 124);
            panelBorder.Controls.Add(panelBackground);
            panelBorder.Font = new Font("Microsoft YaHei UI", 10.5F);
            panelBorder.Location = new Point(43, 244);
            panelBorder.Name = "panelBorder";
            panelBorder.Padding = new Padding(1);
            panelBorder.Size = new Size(394, 61);
            panelBorder.TabIndex = 19;
            // 
            // panelBackground
            // 
            panelBackground.BackColor = Color.FromArgb(18, 18, 18);
            panelBackground.Controls.Add(textBoxAccount);
            panelBackground.Dock = DockStyle.Fill;
            panelBackground.Font = new Font("Microsoft YaHei UI", 10.5F);
            panelBackground.Location = new Point(1, 1);
            panelBackground.Name = "panelBackground";
            panelBackground.Padding = new Padding(10, 14, 1, 15);
            panelBackground.Size = new Size(392, 59);
            panelBackground.TabIndex = 10;
            // 
            // textBoxAccount
            // 
            textBoxAccount.BackColor = Color.FromArgb(18, 18, 18);
            textBoxAccount.BorderStyle = BorderStyle.None;
            textBoxAccount.Dock = DockStyle.Fill;
            textBoxAccount.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            textBoxAccount.ForeColor = Color.White;
            textBoxAccount.Location = new Point(10, 14);
            textBoxAccount.Multiline = true;
            textBoxAccount.Name = "textBoxAccount";
            textBoxAccount.Size = new Size(381, 30);
            textBoxAccount.TabIndex = 4;
            // 
            // labelCreatPassword
            // 
            labelCreatPassword.Anchor = AnchorStyles.None;
            labelCreatPassword.AutoSize = true;
            labelCreatPassword.Font = new Font("思源黑体 CN", 11F, FontStyle.Bold);
            labelCreatPassword.ForeColor = Color.White;
            labelCreatPassword.Location = new Point(43, 316);
            labelCreatPassword.Name = "labelCreatPassword";
            labelCreatPassword.Size = new Size(155, 27);
            labelCreatPassword.TabIndex = 16;
            labelCreatPassword.Text = "Creat password";
            // 
            // labelAcountText
            // 
            labelAcountText.Anchor = AnchorStyles.None;
            labelAcountText.AutoSize = true;
            labelAcountText.Font = new Font("思源黑体 CN", 11F, FontStyle.Bold);
            labelAcountText.ForeColor = Color.White;
            labelAcountText.Location = new Point(43, 204);
            labelAcountText.Name = "labelAcountText";
            labelAcountText.Size = new Size(141, 27);
            labelAcountText.TabIndex = 17;
            labelAcountText.Text = "Email address";
            // 
            // labelWelcome
            // 
            labelWelcome.Anchor = AnchorStyles.None;
            labelWelcome.AutoSize = true;
            labelWelcome.Font = new Font("思源黑体 CN", 25.8000011F, FontStyle.Bold, GraphicsUnit.Point, 134);
            labelWelcome.ForeColor = Color.White;
            labelWelcome.Location = new Point(91, 104);
            labelWelcome.Name = "labelWelcome";
            labelWelcome.Size = new Size(313, 64);
            labelWelcome.TabIndex = 15;
            labelWelcome.Text = "Register Now";
            // 
            // pictureBoxOnlyLogoWhiteBlack
            // 
            pictureBoxOnlyLogoWhiteBlack.Anchor = AnchorStyles.None;
            pictureBoxOnlyLogoWhiteBlack.Image = (Image)resources.GetObject("pictureBoxOnlyLogoWhiteBlack.Image");
            pictureBoxOnlyLogoWhiteBlack.Location = new Point(217, 37);
            pictureBoxOnlyLogoWhiteBlack.Name = "pictureBoxOnlyLogoWhiteBlack";
            pictureBoxOnlyLogoWhiteBlack.Size = new Size(50, 50);
            pictureBoxOnlyLogoWhiteBlack.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBoxOnlyLogoWhiteBlack.TabIndex = 14;
            pictureBoxOnlyLogoWhiteBlack.TabStop = false;
            // 
            // labelUsername
            // 
            labelUsername.Anchor = AnchorStyles.None;
            labelUsername.AutoSize = true;
            labelUsername.Font = new Font("思源黑体 CN", 11F, FontStyle.Bold);
            labelUsername.ForeColor = Color.White;
            labelUsername.Location = new Point(44, 429);
            labelUsername.Name = "labelUsername";
            labelUsername.Size = new Size(106, 27);
            labelUsername.TabIndex = 16;
            labelUsername.Text = "Username";
            // 
            // panelBorderUsername
            // 
            panelBorderUsername.Anchor = AnchorStyles.None;
            panelBorderUsername.BackColor = Color.FromArgb(124, 124, 124);
            panelBorderUsername.Controls.Add(panelBackgroundUsername);
            panelBorderUsername.Font = new Font("Microsoft YaHei UI", 10.5F);
            panelBorderUsername.Location = new Point(44, 468);
            panelBorderUsername.Name = "panelBorderUsername";
            panelBorderUsername.Padding = new Padding(1);
            panelBorderUsername.Size = new Size(394, 61);
            panelBorderUsername.TabIndex = 18;
            // 
            // panelBackgroundUsername
            // 
            panelBackgroundUsername.BackColor = Color.FromArgb(18, 18, 18);
            panelBackgroundUsername.Controls.Add(textBoxUsername);
            panelBackgroundUsername.Dock = DockStyle.Fill;
            panelBackgroundUsername.Font = new Font("Microsoft YaHei UI", 10.5F);
            panelBackgroundUsername.Location = new Point(1, 1);
            panelBackgroundUsername.Name = "panelBackgroundUsername";
            panelBackgroundUsername.Padding = new Padding(10, 14, 1, 15);
            panelBackgroundUsername.Size = new Size(392, 59);
            panelBackgroundUsername.TabIndex = 10;
            // 
            // textBoxUsername
            // 
            textBoxUsername.BackColor = Color.FromArgb(18, 18, 18);
            textBoxUsername.BorderStyle = BorderStyle.None;
            textBoxUsername.Dock = DockStyle.Fill;
            textBoxUsername.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            textBoxUsername.ForeColor = Color.White;
            textBoxUsername.Location = new Point(10, 14);
            textBoxUsername.Multiline = true;
            textBoxUsername.Name = "textBoxUsername";
            textBoxUsername.Size = new Size(381, 30);
            textBoxUsername.TabIndex = 4;
            // 
            // UC_Register
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(18, 18, 18);
            Controls.Add(labelBack);
            Controls.Add(labelClose);
            Controls.Add(btnLogIn);
            Controls.Add(panelBorderUsername);
            Controls.Add(panelBorderPassword);
            Controls.Add(labelUsername);
            Controls.Add(panelBorder);
            Controls.Add(labelCreatPassword);
            Controls.Add(labelAcountText);
            Controls.Add(labelWelcome);
            Controls.Add(pictureBoxOnlyLogoWhiteBlack);
            Name = "UC_Register";
            Size = new Size(481, 668);
            panelBorderPassword.ResumeLayout(false);
            panelBackgroundPassword.ResumeLayout(false);
            panelBackgroundPassword.PerformLayout();
            panelBorder.ResumeLayout(false);
            panelBackground.ResumeLayout(false);
            panelBackground.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxOnlyLogoWhiteBlack).EndInit();
            panelBorderUsername.ResumeLayout(false);
            panelBackgroundUsername.ResumeLayout(false);
            panelBackgroundUsername.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelBack;
        private Label labelClose;
        private Button btnLogIn;
        private Panel panelBorderPassword;
        private Panel panelBackgroundPassword;
        private TextBox textBoxPassword;
        private Panel panelBorder;
        private Panel panelBackground;
        private TextBox textBoxAccount;
        private Label labelCreatPassword;
        private Label labelAcountText;
        private Label labelWelcome;
        private PictureBox pictureBoxOnlyLogoWhiteBlack;
        private Label labelUsername;
        private Panel panelBorderUsername;
        private Panel panelBackgroundUsername;
        private TextBox textBoxUsername;
    }
}
