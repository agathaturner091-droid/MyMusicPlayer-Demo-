using System.Security.Cryptography;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Security.Permissions;
using System.Drawing.Text;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace MusicPlayer
{
    public partial class Main : Form
    {
        private OpenFiles _openFiles;

        private List<string> allLocalPaths = new List<string>();
        private float[] currentEqGains = new float[10];

        private AudioFileReader audioFile;
        private AudioEqualizerProcessor _eqProcessor;
        private WaveOutEvent waveOut;

        public static int CurrentUserId = 0;

        public Main()
        {
            InitializeComponent();
            CustomizeDesing();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;

            this.FormClosing += Main_FormClosing;

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
            //数据处理
            if (playListDictionary.ContainsKey(name))
            {
                playListDictionary[name].AddRange(paths);
                MessageBox.Show($"已将歌曲添加到现有歌单：{name}");
            }
            else
            {
                playListDictionary.Add(name, paths);
            }

            // 直接调用刷新方法重绘 UI
            RefreshPlaylists();
        }

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
            OpenChildForm(new Equalizer(this));
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

            int buttonHeight = 40; // 每个歌单按钮的高度

            panelPlayListSubMenu.Height = playListDictionary.Count * buttonHeight;

            // 遍历生成按钮
            foreach (var item in playListDictionary)
            {
                string playlistName = item.Key;
                List<string> paths = item.Value;

                Button btn = new Button();
                btn.Text = playlistName; //用 Padding 控制
                btn.Dock = DockStyle.Top; // 自动顶上去
                btn.Height = buttonHeight;
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                btn.TextAlign = ContentAlignment.MiddleLeft;
                btn.ForeColor = Color.LightGray;
                btn.Cursor = Cursors.Hand;

                btn.Font = new Font("Microsoft YaHei UI", 10F, FontStyle.Bold);
                btn.Padding = new Padding(70, 0, 0, 0); // 左边距 70

                // 绑定点击事件
                btn.Click += (s, e) => {
                    // 确保 OpenFiles 显示出来
                    if (!panelChildForm.Controls.Contains(_openFiles))
                    {
                        panelChildForm.Controls.Add(_openFiles);
                    }
                    _openFiles.Show();
                    _openFiles.BringToFront();

                    // 调用 OpenFiles 里的方法加载歌曲 (注意方法名是 LoadPlaylistToGrid)
                    _openFiles.LoadPlaylistToGrid(playlistName, paths);
                };

                // 添加到子菜单容器
                panelPlayListSubMenu.Controls.Add(btn);
            }

            panelPlayListSubMenu.Visible = true;
        }

        // 供调用的更新接口
        public void UpdateEqGain(int bandIndex, float gainDb)
        {
            if (bandIndex >= 0 && bandIndex < currentEqGains.Length)
            {
                currentEqGains[bandIndex] = gainDb; // 存下这个数值
            }

            if (_eqProcessor != null)
            {
                _eqProcessor.SetGain(bandIndex, gainDb);
            }
        }
        public float[] GetCurrentEqGains() => currentEqGains;

        private void DisposeWave()
        {
            waveOut?.Stop();
            waveOut?.Dispose();
            audioFile?.Dispose();
        }
        public void PlayMusic(string path)
        {
            DisposeWave(); // 释放旧资源

            try
            {
                //  创建读取器
                audioFile = new AudioFileReader(path);

                //实例化均衡器处理器，传入读取器
                _eqProcessor = new AudioEqualizerProcessor(audioFile);

                // 实例化播放设备 (如果之前没实例化过)
                if (waveOut == null)
                {
                    waveOut = new WaveOutEvent();
                }

                // 将【处理器】初始化到播放设备中
                waveOut.Init(_eqProcessor);

                // 开始播放
                waveOut.Play();
            }
            catch (Exception ex)
            {
                MessageBox.Show("播放失败: " + ex.Message);
            }
        }
        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            OpenFiles of = (OpenFiles)Application.OpenForms["OpenFiles"];
            if (of != null)
            {
                SqliteHelper db = new SqliteHelper();

                int vol = of.GetCurrentVolume();
                string mode = of.GetCurrentMode();

                // 假设：取均衡器第1频段为低音，第10频段为高音
                float baseVal = currentEqGains[0];
                float trebleVal = currentEqGains[9];

                db.SaveAudioSettings(Main.CurrentUserId, vol, mode, baseVal, trebleVal);

            }
        }
    }
}
