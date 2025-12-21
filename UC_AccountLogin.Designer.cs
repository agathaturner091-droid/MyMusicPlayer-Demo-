namespace MusicPlayer
{
    partial class UC_AccountLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UC_AccountLogin));
            panelBorder = new Panel();
            panelBackground = new Panel();
            textBoxAccount = new TextBox();
            labelAcountText = new Label();
            labelWelcome = new Label();
            pictureBoxOnlyLogoWhiteBlack = new PictureBox();
            labelPassword = new Label();
            btnLogIn = new Button();
            panelBorderPassword = new Panel();
            panelBackgroundPassword = new Panel();
            textBoxPassword = new TextBox();
            labelClose = new Label();
            labelBack = new Label();
            panelBorder.SuspendLayout();
            panelBackground.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxOnlyLogoWhiteBlack).BeginInit();
            panelBorderPassword.SuspendLayout();
            panelBackgroundPassword.SuspendLayout();
            SuspendLayout();
            // 
            // panelBorder
            // 
            panelBorder.Anchor = AnchorStyles.None;
            panelBorder.BackColor = Color.FromArgb(124, 124, 124);
            panelBorder.Controls.Add(panelBackground);
            panelBorder.Font = new Font("Microsoft YaHei UI", 10.5F);
            panelBorder.Location = new Point(40, 245);
            panelBorder.Name = "panelBorder";
            panelBorder.Padding = new Padding(1);
            panelBorder.Size = new Size(394, 61);
            panelBorder.TabIndex = 9;
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
            textBoxAccount.KeyDown += textBoxAccount_KeyDown;
            // 
            // labelAcountText
            // 
            labelAcountText.Anchor = AnchorStyles.None;
            labelAcountText.AutoSize = true;
            labelAcountText.Font = new Font("思源黑体 CN", 11F, FontStyle.Bold);
            labelAcountText.ForeColor = Color.White;
            labelAcountText.Location = new Point(40, 205);
            labelAcountText.Name = "labelAcountText";
            labelAcountText.Size = new Size(187, 27);
            labelAcountText.TabIndex = 8;
            labelAcountText.Text = "Email or Username";
            // 
            // labelWelcome
            // 
            labelWelcome.Anchor = AnchorStyles.None;
            labelWelcome.AutoSize = true;
            labelWelcome.Font = new Font("思源黑体 CN", 25.8000011F, FontStyle.Bold, GraphicsUnit.Point, 134);
            labelWelcome.ForeColor = Color.White;
            labelWelcome.Location = new Point(74, 105);
            labelWelcome.Name = "labelWelcome";
            labelWelcome.Size = new Size(351, 64);
            labelWelcome.TabIndex = 7;
            labelWelcome.Text = "Welcome Back ";
            // 
            // pictureBoxOnlyLogoWhiteBlack
            // 
            pictureBoxOnlyLogoWhiteBlack.Anchor = AnchorStyles.None;
            pictureBoxOnlyLogoWhiteBlack.Image = (Image)resources.GetObject("pictureBoxOnlyLogoWhiteBlack.Image");
            pictureBoxOnlyLogoWhiteBlack.Location = new Point(214, 38);
            pictureBoxOnlyLogoWhiteBlack.Name = "pictureBoxOnlyLogoWhiteBlack";
            pictureBoxOnlyLogoWhiteBlack.Size = new Size(50, 50);
            pictureBoxOnlyLogoWhiteBlack.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBoxOnlyLogoWhiteBlack.TabIndex = 6;
            pictureBoxOnlyLogoWhiteBlack.TabStop = false;
            // 
            // labelPassword
            // 
            labelPassword.Anchor = AnchorStyles.None;
            labelPassword.AutoSize = true;
            labelPassword.Font = new Font("思源黑体 CN", 11F, FontStyle.Bold);
            labelPassword.ForeColor = Color.White;
            labelPassword.Location = new Point(40, 317);
            labelPassword.Name = "labelPassword";
            labelPassword.Size = new Size(101, 27);
            labelPassword.TabIndex = 8;
            labelPassword.Text = "Password";
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
            btnLogIn.Location = new Point(40, 452);
            btnLogIn.Name = "btnLogIn";
            btnLogIn.Size = new Size(394, 51);
            btnLogIn.TabIndex = 10;
            btnLogIn.UseVisualStyleBackColor = false;
            btnLogIn.Click += btnLogIn_Click;
            // 
            // panelBorderPassword
            // 
            panelBorderPassword.Anchor = AnchorStyles.None;
            panelBorderPassword.BackColor = Color.FromArgb(124, 124, 124);
            panelBorderPassword.Controls.Add(panelBackgroundPassword);
            panelBorderPassword.Font = new Font("Microsoft YaHei UI", 10.5F);
            panelBorderPassword.Location = new Point(40, 356);
            panelBorderPassword.Name = "panelBorderPassword";
            panelBorderPassword.Padding = new Padding(1);
            panelBorderPassword.Size = new Size(394, 61);
            panelBorderPassword.TabIndex = 9;
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
            // labelClose
            // 
            labelClose.AutoSize = true;
            labelClose.Font = new Font("Microsoft YaHei UI", 7.8F, FontStyle.Regular, GraphicsUnit.Point, 134);
            labelClose.ForeColor = Color.White;
            labelClose.Location = new Point(447, 17);
            labelClose.Name = "labelClose";
            labelClose.Size = new Size(20, 19);
            labelClose.TabIndex = 13;
            labelClose.Text = "✕";
            labelClose.Click += labelClose_Click;
            // 
            // labelBack
            // 
            labelBack.AutoSize = true;
            labelBack.Font = new Font("Microsoft YaHei UI", 10F);
            labelBack.ForeColor = Color.White;
            labelBack.Location = new Point(19, 15);
            labelBack.Name = "labelBack";
            labelBack.Size = new Size(23, 23);
            labelBack.TabIndex = 13;
            labelBack.Text = "<";
            labelBack.Click += labelBack_Click;
            // 
            // UC_AccountLogin
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(18, 18, 18);
            Controls.Add(labelBack);
            Controls.Add(labelClose);
            Controls.Add(btnLogIn);
            Controls.Add(panelBorderPassword);
            Controls.Add(panelBorder);
            Controls.Add(labelPassword);
            Controls.Add(labelAcountText);
            Controls.Add(labelWelcome);
            Controls.Add(pictureBoxOnlyLogoWhiteBlack);
            MaximumSize = new Size(481, 548);
            MinimumSize = new Size(481, 548);
            Name = "UC_AccountLogin";
            Size = new Size(481, 548);
            panelBorder.ResumeLayout(false);
            panelBackground.ResumeLayout(false);
            panelBackground.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxOnlyLogoWhiteBlack).EndInit();
            panelBorderPassword.ResumeLayout(false);
            panelBackgroundPassword.ResumeLayout(false);
            panelBackgroundPassword.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panelBorder;
        private TextBox textBoxAccount;
        private Label labelAcountText;
        private Label labelWelcome;
        private PictureBox pictureBoxOnlyLogoWhiteBlack;
        private Label labelPassword;
        private TextBox textBoxPassword;
        private Button btnLogIn;
        private Panel panelBackground;
        private Panel panelBorderPassword;
        private Panel panelBackgroundPassword;
        private Label labelClose;
        private Label labelBack;
    }
}
