using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MusicPlayer
{
    public partial class UC_Register : UserControl
    {
        public event EventHandler RequestBack;
        public UC_Register()
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

        private void btnLogIn_Click(object sender, EventArgs e)
        {
            string email = textBoxAccount.Text.Trim();
            string pwd = textBoxPassword.Text;
            string user = textBoxUsername.Text.Trim();

            if (string.IsNullOrEmpty(email)||string.IsNullOrEmpty(pwd) || string.IsNullOrEmpty(user))
            {
                MessageBox.Show("请输入完整信息！");
                return;
            }    
            SqliteHelper db = new SqliteHelper();
            db.RegisterUser(email,pwd,user);

            RequestBack?.Invoke(this, EventArgs.Empty);
        }
    }
}
