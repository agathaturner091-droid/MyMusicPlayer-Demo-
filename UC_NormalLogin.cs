using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MusicPlayer
{
    public partial class UC_NormalLogin : UserControl
    {
        //传递到外面，用户想切换到账户登录
        public event EventHandler RequestSwitch;
        public event EventHandler RequestRegister;
        public UC_NormalLogin()
        {
            InitializeComponent();
        }

        private void labelClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnToAccount_Click(object sender, EventArgs e)
        {
            RequestSwitch?.Invoke(this, EventArgs.Empty);
        }

        private void labelRegister_Click(object sender, EventArgs e)
        {
            RequestRegister?.Invoke(this, EventArgs.Empty);
        }
    }
}
