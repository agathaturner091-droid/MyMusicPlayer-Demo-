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
            if (db.CheckLogin(accountInput, pwdInput))
            {
                MessageBox.Show("登录成功，欢迎使用MeloVibe！");

                Form parentForm = this.FindForm();
                if (parentForm != null)
                {
                    parentForm.DialogResult= DialogResult.OK;
                    parentForm.Close();
                }
            }
            else
            {
                MessageBox.Show("账户或密码错误，请重试！");
            }
        }
    }
}
