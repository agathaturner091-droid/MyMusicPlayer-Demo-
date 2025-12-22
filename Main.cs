using System.Security.Cryptography;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Security.Permissions;
using System.Drawing.Text;

namespace MusicPlayer
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            CustomizeDesing();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;

            //传入
            OpenFiles of = new OpenFiles(this);
            of.TopLevel = false;

        }

        private OpenFiles _openFilesForm;

        //存储歌单名和歌曲路径的对应关系
        private Dictionary<string, List<string>> playListDictionary = new Dictionary<string, List<string>>();

        public void CreateNewPlayListUI(string name, List<string> paths)
        {
            //数据处理：如果歌单已存在则合并，否不存在则新建
            if (playListDictionary.ContainsKey(name))
            {
                playListDictionary[name].AddRange(paths);
                MessageBox.Show($"已将歌曲添加到现有歌单：{name}");
                return;
            }

            playListDictionary.Add(name, paths);

            //创建子菜单按钮
            Button btnItem=new Button();
            btnItem.Text = "      " + name;
            btnItem.Size = new Size(panelPlayListSubMenu.Width - 5, 35);
            btnItem.FlatStyle=FlatStyle.Flat;
            btnItem.FlatAppearance.BorderSize = 0;
            btnItem.TextAlign = ContentAlignment.MiddleLeft;
            btnItem.ForeColor = Color.White;
            btnItem.Cursor = Cursor.Hand;

            //点击歌名：再OpenFiles的数据表中展示歌曲
            btnItem.Click += (s, e) =>
            {
                //加载属于该歌单的paths
                _openFiles.LoadPlayListToGrid(name, playListDictionary[name]);
            };

            //添加到容器
            panelPlayListSubMenu.Controls.Add(btnItem);

            //确保是展开状态
            panelPlayListSubMenu.Visible = true;
        }

        //动态UI生成：在左侧列表创建点击项


        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wparam, int lparam);
        private void panelSetting_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, 0xA1, 0x2, 0);
            }
        }
        private void labelClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void labelMax_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
            }
        }

        private void labelMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Main_Shown(object sender, EventArgs e)
        {
            pictureBoxPicture.Visible = false;
            Application.DoEvents();

            //遮罩层
            Form overlay = new Form();
            try
            {
                //无边框
                overlay.FormBorderStyle = FormBorderStyle.None;
                overlay.BackColor = Color.Black;
                //透明度
                overlay.Opacity = 0.8;
                //不在任务栏显示
                overlay.ShowInTaskbar = false;
                overlay.StartPosition = FormStartPosition.Manual;
                overlay.Location = this.Location;
                overlay.Size = this.Size;
                //归属关系，让主窗口有遮罩层，且永远在主窗口上面
                overlay.Owner = this;

                overlay.Show();


                LogIn login = new LogIn();
                login.ShowInTaskbar = false;
                login.StartPosition = FormStartPosition.CenterScreen;

                DialogResult result = login.ShowDialog(overlay);

                if (result == DialogResult.OK)
                {
                    pictureBoxPicture.Visible = true;

                    this.Focus();
                }
                else
                {
                    Application.Exit();
                }
            }
            finally
            {
                //无论成功与否，销毁遮罩层
                overlay.Dispose();//释放资源
                overlay = null;

                //获得焦点
                this.Focus();
            }
        }

        private void CustomizeDesing()
        {
            panelToolsSubMenu.Visible = false;
        }
        private void HideSubMenu()
        {
            if (panelToolsSubMenu.Visible == true)
                panelToolsSubMenu.Visible = false;
        }
        private void ShowSubMenu(Panel subMenu)
        {
            if (subMenu.Visible == false)
            {
                HideSubMenu();
                subMenu.Visible = true;
            }
            else
                subMenu.Visible = false;
        }

        private void btnMedia_Click(object sender, EventArgs e)
        {
            if (_openFilesForm == null || _openFilesForm.IsDisposed)
            {
                _openFilesForm = new OpenFiles(this);
                _openFilesForm.TopLevel = false;
                _openFilesForm.Dock = DockStyle.Fill;
                panelChildForm.Controls.Add(_openFilesForm);
            }

            _openFilesForm.BringToFront();
            _openFilesForm.Show();
        }
        private void btnPlayList_Click(object sender, EventArgs e)
        {
            panelPlayListSubMenu.Visible = !panelPlayListSubMenu.Visible;
        }
        private void btnEqualizer_Click(object sender, EventArgs e)
        {

            OpenChildForm(new Form3());
            //...
            //代码
            //...
            HideSubMenu();
        }

        private void btnTools_Click(object sender, EventArgs e)
        {
            ShowSubMenu(panelToolsSubMenu);
        }
        #region ToolsSubMenu
        private void button14_Click(object sender, EventArgs e)
        {
            //...
            //代码
            //...
            HideSubMenu();
        }
        private void button13_Click(object sender, EventArgs e)
        {
            //...
            //代码
            //...
            HideSubMenu();
        }
        private void button12_Click(object sender, EventArgs e)
        {
            //...
            //代码
            //...
            HideSubMenu();
        }
        private void button11_Click(object sender, EventArgs e)
        {
            //...
            //代码
            //...
            HideSubMenu();
        }
        #endregion
        private void btnHelp_Click(object sender, EventArgs e)
        {
            //...
            //代码
            //...
            HideSubMenu();
        }

        private Form activeForm = null;
        private void OpenChildForm(Form childForm)
        {
            if (activeForm != null)
                activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelChildForm.Controls.Add(childForm);
            panelChildForm.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void pictureBoxAdd_Click(object sender, EventArgs e)
        {
            EditDetails editForm = new EditDetails(new List<string>(), _openFiles);
            editForm.ShowDialog();
        }
    }
}
