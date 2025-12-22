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
        private OpenFiles _openFiles;

        private List<string> allLocalPaths = new List<string>();

        public Main()
        {
            InitializeComponent();
            CustomizeDesing();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;

            //初始化窗体
            _openFiles = new OpenFiles(this);
            _openFiles.TopLevel = false;
            _openFiles.FormBorderStyle = FormBorderStyle.None;
            _openFiles.Dock = DockStyle.Fill;

            // 将它放入主显示面板
            panelChildForm.Controls.Add(_openFiles);

            // 默认显示
            panelChildForm.Show();
            panelChildForm.BringToFront();
        }


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
            btnItem.Size = new Size(panelPlayListSubMenu.Width - 5, 45);
            btnItem.FlatStyle=FlatStyle.Flat;
            btnItem.FlatAppearance.BorderSize = 0;
            btnItem.TextAlign = ContentAlignment.MiddleLeft;
            btnItem.ForeColor = Color.White;
            btnItem.Cursor = Cursors.Hand;

            //点击歌名：再OpenFiles的数据表中展示歌曲
            btnItem.Click += (s, e) =>
            {
                //加载属于该歌单的paths
                _openFiles.LoadPlaylistToGrid(name, playListDictionary[name]);
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
            // 强制关闭歌单子菜单
            panelPlayListSubMenu.Visible = false;
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
            _openFiles.Show();
            _openFiles.BringToFront();

            // 调用重置方法，让它从歌单模式切回普通模式
            _openFiles.ResetToAllMusic();
        }

        private void btnPlayList_Click(object sender, EventArgs e)
        {
            if (panelPlayListSubMenu.Controls.Count > 0)
            {
                panelPlayListSubMenu.Visible = !panelPlayListSubMenu.Visible;
            }
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
            // 传入一个全新的空列表 new List<string>()
            // 这样 EditDetails 内部判断路径数 = 0，就会进入“新建空歌单”模式
            EditDetails editForm = new EditDetails(new List<string>(), _openFiles);
            editForm.ShowDialog();
        }

        public void AddNewPlaylistToUI(string name, List<string> paths)
        {
            Button btnPlaylist = new Button();
            // 缩进
            btnPlaylist.Text = "      " + name; 
            btnPlaylist.Size = new Size(panelPlayListSubMenu.Width - 10, 30);
            btnPlaylist.TextAlign = ContentAlignment.MiddleLeft;
            btnPlaylist.FlatStyle = FlatStyle.Flat;
            btnPlaylist.FlatAppearance.BorderSize = 0;
            btnPlaylist.ForeColor = Color.White;
            btnPlaylist.Cursor = Cursors.Hand; 

            btnPlaylist.Click += (s, e) => {
                // 调用 OpenFiles 里的方法显示歌曲
                if (!panelChildForm.Controls.Contains(_openFiles))
                {
                    panelChildForm.Controls.Add(_openFiles);
                }
                // 显示窗体并置顶
                _openFiles.Show();
                _openFiles.BringToFront();

                //加载该歌单的数据
                _openFiles.LoadPlaylistToGrid(name, paths);
            };

            panelPlayListSubMenu.Controls.Add(btnPlaylist);
            // 自动展开显示新歌单
            panelPlayListSubMenu.Visible = true; 
        }

        public void RefreshPlaylists()
        {
            // 清空旧按钮
            panelPlayListSubMenu.Controls.Clear();

            // 从数据库获取所有歌单名称 (假设你使用的是 SQLiteHelper)
            List<string> playlists = SQLiteStudio.GetPlaylists();

            int buttonHeight = 40; // 每个歌单按钮的高度

            // 3. 动态调整子菜单高度
            // 如果没有歌单，高度为0；如果有，高度 = 数量 * 40
            panelPlayListSubMenu.Height = playlists.Count * buttonHeight;

            // 4. 倒序添加（保证显示顺序）
            for (int i = playlists.Count - 1; i >= 0; i--)
            {
                Button btn = new Button();
                btn.Text = playlists[i];
                btn.Dock = DockStyle.Top;
                btn.Height = buttonHeight;
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                btn.TextAlign = ContentAlignment.MiddleLeft;
                btn.ForeColor = Color.LightGray;

                // 设置你要求的字体
                btn.Font = new Font("Microsoft YaHei UI", 10F, FontStyle.Bold);

                // 设置你要求的内边距 (Left=70, Top=0, Right=0, Bottom=0)
                btn.Padding = new Padding(70, 0, 0, 0);

                // 绑定点击事件，点击后在 OpenFiles 中显示该歌单内容
                btn.Click += (s, e) => {
                    _openFiles.LoadPlaylistSongs(btn.Text);
                    _openFiles.BringToFront();
                };

                panelPlayListSubMenu.Controls.Add(btn);
            }
        }
    }
}
