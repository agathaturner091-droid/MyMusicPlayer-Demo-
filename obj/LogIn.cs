using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MusicPlayer
{
    public partial class LogIn : Form
    {
        public LogIn()
        {
            InitializeComponent();
            ucNormal.RequestSwitch += UcNormal_RequestSwitch;
            ucNormal.RequestRegister += UcNormal_RequestRegister;
        }

        private void ShowUserControl(UserControl uc)
        {
            this.Controls.Clear();
            uc.Dock = DockStyle.Fill;

            this.ClientSize = uc.Size;
            this.Controls.Add(uc);

            this.Location = new Point(
                (Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2,
                (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2
                );
        }
        private void UcNormal_RequestRegister(object? sender, EventArgs e)
        {
            this.Controls.Remove(ucNormal);

            UC_Register ucRegister = new UC_Register();
            ucRegister.Dock = DockStyle.Fill;

            ucRegister.RequestBack += (s, args) =>
            {
                ShowUserControl(ucNormal);
            };
            ShowUserControl(ucRegister);
        }
        private void UcNormal_RequestSwitch(object sender, EventArgs e)
        {
            //移除旧控件，添加新的
            this.Controls.Remove(ucNormal);

            UC_AccountLogin ucAccount = new UC_AccountLogin();
            ucAccount.Dock = DockStyle.Fill;

            //返回事件
            ucAccount.RequestBack += (s, args) =>
            {
                ShowUserControl(ucNormal);
            };
            ShowUserControl(ucAccount);

        }

        private void labelClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

    }
}
