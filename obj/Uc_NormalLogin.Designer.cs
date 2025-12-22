namespace MusicPlayer
{
    partial class UC_NormalLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UC_NormalLogin));
            labelClose = new Label();
            labelRegister = new Label();
            labelFirstQuestion = new Label();
            btnToAccount = new Button();
            labelWelcome = new Label();
            labelIntruction = new Label();
            pictureBoxLogo = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBoxLogo).BeginInit();
            SuspendLayout();
            // 
            // labelClose
            // 
            labelClose.AutoSize = true;
            labelClose.Font = new Font("Microsoft YaHei UI", 7.8F, FontStyle.Regular, GraphicsUnit.Point, 134);
            labelClose.ForeColor = Color.White;
            labelClose.Location = new Point(539, 17);
            labelClose.Name = "labelClose";
            labelClose.Size = new Size(20, 19);
            labelClose.TabIndex = 12;
            labelClose.Text = "✕";
            labelClose.Click += labelClose_Click;
            // 
            // labelRegister
            // 
            labelRegister.Anchor = AnchorStyles.None;
            labelRegister.AutoSize = true;
            labelRegister.Font = new Font("思源黑体 CN", 10F, FontStyle.Bold);
            labelRegister.ForeColor = Color.White;
            labelRegister.Location = new Point(321, 313);
            labelRegister.Name = "labelRegister";
            labelRegister.Size = new Size(81, 25);
            labelRegister.TabIndex = 11;
            labelRegister.Text = "Register";
            labelRegister.Click += labelRegister_Click;
            // 
            // labelFirstQuestion
            // 
            labelFirstQuestion.Anchor = AnchorStyles.None;
            labelFirstQuestion.AutoSize = true;
            labelFirstQuestion.Font = new Font("微软雅黑", 8F);
            labelFirstQuestion.ForeColor = Color.FromArgb(162, 163, 165);
            labelFirstQuestion.Location = new Point(130, 316);
            labelFirstQuestion.Name = "labelFirstQuestion";
            labelFirstQuestion.Size = new Size(187, 20);
            labelFirstQuestion.TabIndex = 10;
            labelFirstQuestion.Text = "First time using MeloVibe?";
            // 
            // btnToAccount
            // 
            btnToAccount.Anchor = AnchorStyles.None;
            btnToAccount.BackgroundImageLayout = ImageLayout.None;
            btnToAccount.FlatAppearance.BorderSize = 0;
            btnToAccount.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnToAccount.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnToAccount.FlatStyle = FlatStyle.Flat;
            btnToAccount.Font = new Font("思源黑体 CN", 13.7999992F, FontStyle.Bold, GraphicsUnit.Point, 134);
            btnToAccount.ForeColor = Color.Black;
            btnToAccount.Image = (Image)resources.GetObject("btnToAccount.Image");
            btnToAccount.Location = new Point(215, 240);
            btnToAccount.Name = "btnToAccount";
            btnToAccount.Size = new Size(128, 51);
            btnToAccount.TabIndex = 9;
            btnToAccount.UseVisualStyleBackColor = false;
            btnToAccount.Click += btnToAccount_Click;
            // 
            // labelWelcome
            // 
            labelWelcome.Anchor = AnchorStyles.None;
            labelWelcome.AutoSize = true;
            labelWelcome.Font = new Font("思源黑体 CN", 13.7999992F, FontStyle.Bold, GraphicsUnit.Point, 134);
            labelWelcome.ForeColor = Color.White;
            labelWelcome.Location = new Point(149, 145);
            labelWelcome.Name = "labelWelcome";
            labelWelcome.Size = new Size(280, 34);
            labelWelcome.TabIndex = 7;
            labelWelcome.Text = "Welcome to MeloVibe ！";
            // 
            // labelIntruction
            // 
            labelIntruction.Anchor = AnchorStyles.None;
            labelIntruction.AutoSize = true;
            labelIntruction.Font = new Font("思源黑体 CN", 13.7999992F, FontStyle.Bold, GraphicsUnit.Point, 134);
            labelIntruction.ForeColor = Color.White;
            labelIntruction.Location = new Point(85, 179);
            labelIntruction.Name = "labelIntruction";
            labelIntruction.Size = new Size(393, 34);
            labelIntruction.TabIndex = 8;
            labelIntruction.Text = "Local Music Search and Playback.";
            // 
            // pictureBoxLogo
            // 
            pictureBoxLogo.Anchor = AnchorStyles.None;
            pictureBoxLogo.Image = (Image)resources.GetObject("pictureBoxLogo.Image");
            pictureBoxLogo.Location = new Point(162, 69);
            pictureBoxLogo.MaximumSize = new Size(240, 60);
            pictureBoxLogo.MinimumSize = new Size(240, 60);
            pictureBoxLogo.Name = "pictureBoxLogo";
            pictureBoxLogo.Size = new Size(240, 60);
            pictureBoxLogo.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBoxLogo.TabIndex = 6;
            pictureBoxLogo.TabStop = false;
            // 
            // UC_NormalLogin
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(18, 18, 18);
            Controls.Add(labelClose);
            Controls.Add(labelRegister);
            Controls.Add(labelFirstQuestion);
            Controls.Add(btnToAccount);
            Controls.Add(labelWelcome);
            Controls.Add(labelIntruction);
            Controls.Add(pictureBoxLogo);
            MaximumSize = new Size(574, 411);
            MinimumSize = new Size(574, 411);
            Name = "UC_NormalLogin";
            Size = new Size(574, 411);
            ((System.ComponentModel.ISupportInitialize)pictureBoxLogo).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelClose;
        private Label labelRegister;
        private Label labelFirstQuestion;
        private Button btnToAccount;
        private Label labelWelcome;
        private Label labelIntruction;
        private PictureBox pictureBoxLogo;
    }
}
