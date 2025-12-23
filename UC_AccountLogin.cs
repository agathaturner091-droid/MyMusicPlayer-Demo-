using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MusicPlayer
{
    public partial class UC_AccountLogin : UserControl
    {
        public event EventHandler RequestBack;
        public UC_AccountLogin()
        {
            InitializeComponent();
        }

        private void labelBack_Click(object sender, EventArgs e)
        {
            RequestBack?.Invoke(this, EventArgs.Empty);
        }

        private void labelClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBoxAccount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
            }
        }

        private void btnLogIn_Click(object sender, EventArgs e)
        {
            string accountInput = textBoxAccount.Text.Trim();
            string pwdInput = textBoxPassword.Text;

            if (string.IsNullOrEmpty(accountInput) || string.IsNullOrEmpty(pwdInput))
            {
                MessageBox.Show("账户密码不能为空");
                return;
            }

            SqliteHelper db = new SqliteHelper();
            int loggedInId = db.GetUserIdAfterLogin(accountInput, pwdInput);

            if (loggedInId > 0)
            {
                // 1. 成功赋值
                Main.CurrentUserId = loggedInId;

                MessageBox.Show("登录成功！");

                // 2. 关键：尝试获取当前已经存在的 OpenFiles 窗体实例并加载设置
                OpenFiles of = (OpenFiles)Application.OpenForms["OpenFiles"];
                if (of != null)
                {
                    of.AutoLoadUserMusic(); // 这会触发 LoadAudioSettings() 弹窗
                }

                // 3. 处理关闭登录窗口的逻辑
                Form parentForm = this.FindForm();
                if (parentForm != null)
                {
                    parentForm.DialogResult = DialogResult.OK; // 告诉主窗口登录成功
                    parentForm.Close(); // 关闭登录对话框
                }
            }
            else
            {
                MessageBox.Show("账户或密码错误！");
            }
        }
    }
}
