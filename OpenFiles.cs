using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Linq;
using TagLib;
using WMPLib;
using System.Drawing.Text;
using System.Timers;
using System.Data.Entity.Core.Objects;
using System.Drawing.Drawing2D;

namespace MusicPlayer
{
    public partial class OpenFiles : Form
    {
        //储存搜索到的音乐数据
        private List<MusicInfo> musicList = new List<MusicInfo>();

        // 用于缓存所有本地导入的歌曲路径，防止切走后丢失
        private List<string> _allLocalMusicCache = new List<string>();

        //创建播放器实例
        private WMPLib.WindowsMediaPlayer player = new WMPLib.WindowsMediaPlayer();
        //记录当前鼠标悬停的列索引，-1为没有悬停
        private int hotColumnIndex = -1;

        //更新播放进度的计时器
        private System.Windows.Forms.Timer playTimer = new System.Windows.Forms.Timer();

        private Main mainForm;

        //随机状态,false 无随机，true 随机播放
        private bool isRandom = false;
        private Random rnd = new Random();
        //循环状态：0 无循环，1 列表循环，2单曲循环
        private int cycleMode = 0;

        //全局变量标记是否正在拖拽进度条
        private bool isDraggingProgress = false;
        private bool isDraggingVolume = false;

        //定义右键菜单实例
        private RightClickMenu contextMenu;


        public OpenFiles(Main main)
        {
            InitializeComponent();
            //初始化
            SetupDataGridView();

            panelSecondBackgroundHigh.Visible = false;

            //第一个面板的渐变
            panelBackgroundHigh.Paint += DrawGradingBackground;
            //第二个
            if (panelSecondBackgroundHigh != null)
            {
                panelSecondBackgroundHigh.Paint += DrawGradingBackground;
            }

            //接收主界面的引用
            this.mainForm = main;

            //初始化播放器设置
            player.settings.volume = 100;
            //监看播放状态（播放/暂停）
            player.PlayStateChange += Player_PlayStateChange;

            //初始化计时器（每秒执行一次）
            playTimer.Interval = 100;
            playTimer.Tick += PlayTimer_Tick;

            //绑定主界面的基础交互
            BindMainUIEvents();
            btnSearch.TabStop = false;

            dataGridViewPlayList.Visible = false;
            btnSearch.Visible = false;
            labelAdd.Visible = true;
            labelSearch.Visible = true;

            labelSearch.MouseClick += labelSearch_MouseClick;
            labelAdd.GotFocus += (s, e) => { this.Focus(); };
            labelSearch.GotFocus += (s, e) => { this.Focus(); };

            //禁止表头高亮选中有颜色
            dataGridViewPlayList.EnableHeadersVisualStyles = false;
            dataGridViewPlayList.ColumnHeadersDefaultCellStyle.SelectionBackColor = dataGridViewPlayList.ColumnHeadersDefaultCellStyle.BackColor;
            dataGridViewPlayList.ColumnHeadersDefaultCellStyle.SelectionForeColor = dataGridViewPlayList.ColumnHeadersDefaultCellStyle.ForeColor;

            //关闭自动播放
            player.settings.autoStart = true;

            //初始化的时候，确保播放按钮是“播放”
            mainForm.pictureBoxPlay.Visible = true;
            mainForm.pictureBoxStop.Visible = false;

            //可取消选择
            dataGridViewPlayList.MouseDown += (s, e) => {
                if (dataGridViewPlayList.HitTest(e.X, e.Y).Type == DataGridViewHitTestType.None)
                {
                    dataGridViewPlayList.ClearSelection();
                }
            };

            //初始化循环/随机图标状态
            RefreshModeUI();
        }

        //绑定主界面控件的点击事件
        private void BindMainUIEvents()
        {
            if (mainForm == null) return;

            //进度条
            mainForm.pictureBoxLoadLine.MouseDown += (s, e) =>
            {
                isDraggingProgress = true;
                UpdateProgressByMouse(e.X);
            };
            mainForm.pictureBoxLoadLine.MouseMove += (s, e) =>
            {
                if (isDraggingProgress)
                {
                    UpdateProgressByMouse(e.X);
                }
            };
            mainForm.pictureBoxLoadLine.MouseUp += (s, e) =>
            {
                if (isDraggingProgress)
                {
                    isDraggingProgress = false;
                    //最终确定播放位置
                    double ratio = (double)e.X / mainForm.pictureBoxLoadLine.Width;
                    if (player.currentMedia != null)
                    {
                        player.controls.currentPosition = ratio * player.currentMedia.duration;
                    }
                }
            };
            //音量条
            mainForm.pictureBoxVolumeLoadLine.MouseDown += (s, e) =>
            {
                isDraggingVolume = true;
                SetVolumeByMouse(e.X);
            };
            mainForm.pictureBoxVolumeLoadLine.MouseMove += (s, e) =>
            {
                if (isDraggingVolume)
                {
                    SetVolumeByMouse(e.X);
                }
            };
            mainForm.pictureBoxVolumeLoadLine.MouseUp += (s, e) =>
            {
                isDraggingVolume = false;
            };

            //初始化音量并立即绘制
            player.settings.volume = 100;
            DrawCustomBar(mainForm.pictureBoxVolumeLoadLine, 1.0, Color.White);
            DrawCustomBar(mainForm.pictureBoxLoadLine, 0, Color.White);

            //播放/暂停
            mainForm.pictureBoxPlay.Click += (s, e) => { player.controls.play(); };
            mainForm.pictureBoxStop.Click += (s, e) => { player.controls.pause(); };

            //下一首
            mainForm.pictureBoxNextSong.Click += (s, e) => { PlayNext(); };

            //上一首
            mainForm.pictureBoxLastSong.Click += (s, e) => { PlayPrevious(); };

            //音量条点击
            mainForm.pictureBoxVolumeLoadLine.MouseClick += (s, e) =>
            {
                double ratio = (double)e.X / mainForm.pictureBoxVolumeLoadLine.Width;
                ratio = Math.Max(0, Math.Min(1, ratio));
                player.settings.volume = (int)(ratio * 100);

                //立即刷新音量条显示（白色）
                DrawCustomBar(mainForm.pictureBoxVolumeLoadLine, ratio, Color.White);
            };

            //随机按钮点击
            mainForm.pictureBoxRandomNot.Click += (s, e) => { isRandom = true; RefreshModeUI(); };
            mainForm.pictureBoxRandom.Click += (s, e) => { isRandom = false; RefreshModeUI(); };

            //循环按钮点击
            mainForm.pictureBoxCycleNot.Click += (s, e) => { cycleMode = 1; RefreshModeUI(); };
            mainForm.pictureBoxCycle.Click += (s, e) => { cycleMode = 2; RefreshModeUI(); };
            mainForm.pictureBoxCycleSolo.Click += (s, e) => { cycleMode = 0; RefreshModeUI(); };

            //初始调用一次，确保图标正确
            RefreshModeUI();
        }

        private void PictureBoxLoadLine_MouseDown(object? sender, MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        //绘制自定义进度条
        private void DrawCustomBar(PictureBox pb, double progress, Color barColor)
        {
            if (pb == null || pb.Width <= 0 || pb.Height <= 0) return;

            //进度条限流
            if (double.IsNaN(progress) || double.IsInfinity(progress)) progress = 0;
            progress = Math.Max(0, Math.Min(1, progress));

            Bitmap bmp = new Bitmap(pb.Width, pb.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                //抗锯齿
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                //绘制背景
                g.Clear(Color.FromArgb(40, 40, 40));

                //计算实际宽度
                int drawWidth = (int)(pb.Width * progress);

                if (drawWidth > 0)
                {
                    using (SolidBrush brush = new SolidBrush(barColor))
                    {
                        g.FillRectangle(brush, 0, 0, drawWidth, pb.Height);
                    }
                }
            }
            //释放
            if (pb.Image != null) pb.Image.Dispose();
            pb.Image = bmp;
        }

        private void labelSearch_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && labelAdd.Visible)
            {
                btnSearch_Click_1(btnSearch, EventArgs.Empty);
            }
        }

        private void SetupDataGridView()
        {
            //允许多选
            dataGridViewPlayList.MultiSelect = true;
            dataGridViewPlayList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            //基础样式
            dataGridViewPlayList.BackgroundColor = Color.FromArgb(18, 18, 18);//深色背景
            dataGridViewPlayList.BorderStyle = BorderStyle.None;//移除外边框
            dataGridViewPlayList.CellBorderStyle = DataGridViewCellBorderStyle.None;//去掉单元格线条
            dataGridViewPlayList.RowHeadersVisible = false;//隐藏最左侧的状态列
            dataGridViewPlayList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;//点击后选中整行
            dataGridViewPlayList.ReadOnly = true;//禁止编辑内容
            dataGridViewPlayList.AllowUserToResizeColumns = false;//禁止拉伸列宽
            dataGridViewPlayList.AllowUserToResizeRows = false;//列高
            dataGridViewPlayList.EnableHeadersVisualStyles = false;//允许自定义表头

            //设置按钮为平铺
            btnSearch.FlatStyle = FlatStyle.Flat;
            //背景色透明
            btnSearch.BackColor = Color.Transparent;
            //移除边框线
            btnSearch.FlatAppearance.BorderSize = 0;
            //不希望悬停时变色
            btnSearch.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnSearch.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnSearch.Parent = panelSecondBackgroundHigh;

            //字体与颜色
            //默认文字颜色为（179，179，179）
            dataGridViewPlayList.DefaultCellStyle.BackColor = Color.FromArgb(18, 18, 18);
            dataGridViewPlayList.DefaultCellStyle.ForeColor = Color.FromArgb(179, 179, 179);
            //字体
            dataGridViewPlayList.DefaultCellStyle.Font = new Font("微软雅黑", 11, FontStyle.Regular);

            //默认选中的背景和文字颜色
            dataGridViewPlayList.DefaultCellStyle.SelectionBackColor = Color.FromArgb(40, 40, 40);
            dataGridViewPlayList.DefaultCellStyle.SelectionForeColor = Color.White;

            //表头样式（标题行）
            dataGridViewPlayList.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewPlayList.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(18, 18, 18);
            dataGridViewPlayList.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(179, 179, 179);
            //禁止自己调整
            dataGridViewPlayList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewPlayList.ColumnHeadersHeight = 45;//表头高度
            dataGridViewPlayList.ColumnHeadersDefaultCellStyle.Padding = new Padding(0);
            //初始化列
            dataGridViewPlayList.Columns.Clear();
            dataGridViewPlayList.Columns.Add(new DataGridViewTextBoxColumn { Name = "#", FillWeight = 3 });
            dataGridViewPlayList.Columns.Add(new DataGridViewTextBoxColumn { Name = "标题", FillWeight = 20 });
            dataGridViewPlayList.Columns.Add(new DataGridViewTextBoxColumn { Name = "专辑", FillWeight = 12 });
            dataGridViewPlayList.Columns.Add(new DataGridViewTextBoxColumn { Name = "添加日期", FillWeight = 10 });
            dataGridViewPlayList.Columns.Add(new DataGridViewTextBoxColumn { Name = "时长", FillWeight = 8 });
            dataGridViewPlayList.Columns.Add(new DataGridViewTextBoxColumn { Name = "Path", Visible = false });

            //居中
            dataGridViewPlayList.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewPlayList.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            //让列宽根据比例自动伸缩
            dataGridViewPlayList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //调整行高，以便给“歌名+歌手”留出位置
            dataGridViewPlayList.RowTemplate.Height = 75;

            //绑定自绘事件
            dataGridViewPlayList.CellPainting -= dataGridViewPlayList_CellPainting;
            dataGridViewPlayList.CellPainting += dataGridViewPlayList_CellPainting;

            //绑定播放事件
            dataGridViewPlayList.CellDoubleClick -= dataGridViewPlayList_CellDoubleClick;
            dataGridViewPlayList.CellDoubleClick += dataGridViewPlayList_CellDoubleClick;

        }


        //搜索/添加本地音乐
        private void btnSearch_Click_1(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    string path = fbd.SelectedPath;

                    // 清空本地缓存路径，防止逻辑层面重复 
                    if (_allLocalMusicCache != null)
                    {
                        _allLocalMusicCache.Clear();
                    }

                    // 清空界面表格，防止 UI 层面重复显示
                    if (dataGridViewPlayList != null)
                    {
                        dataGridViewPlayList.Rows.Clear();
                    }

                    // 执行搜索并填充新数据
                    SearchMusic(path);
                }
            }
        }
        private void SearchMusic(string path)
        {
            SqliteHelper dbHelper = new SqliteHelper();
            int currentId = MusicPlayer.Main.CurrentUserId;

            try
            {
                // 搜索的音频格式
                string[] extensions = { ".mp3", ".wav", ".flac", ".m4a" };

                // 获取目录下所有符合条件的文件
                var files = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories)
                                     .Where(f => extensions.Contains(Path.GetExtension(f).ToLower()))
                                     .ToList();

                // 保存到缓存
                _allLocalMusicCache = new List<string>(files);

                // 如果没有歌曲
                if (files.Count == 0)
                {
                    dataGridViewPlayList.Visible = false;
                    labelAdd.Visible = true;
                    labelSearch.Visible = true;
                    return;
                }

                foreach (var file in files)
                {
                    string fileName = Path.GetFileNameWithoutExtension(file);
                    string extension = Path.GetExtension(file);

                    // 预设默认值
                    string artist = "未知歌手";
                    string album = "未知专辑";
                    string duration = "00:00";

                    // 使用代码里已有的 TagLib 尝试获取真实属性
                    try
                    {
                        using (var tfile = TagLib.File.Create(file))
                        {
                            artist = string.Join(",", tfile.Tag.Performers);
                            if (string.IsNullOrEmpty(artist)) artist = "未知歌手";
                            album = tfile.Tag.Album ?? "未知专辑";
                            duration = tfile.Properties.Duration.ToString(@"mm\:ss");
                        }
                    }
                    catch { /* 忽略读取失败的文件，使用默认值 */ }

                    // 执行数据库保存
                    dbHelper.SaveTrack(fileName, artist, album, duration, file, extension, currentId);
                }

                // 切换 UI 状态
                labelAdd.Visible = false;
                labelSearch.Visible = false;
                panelBackgroundHigh.Visible = false;
                if (panelBackgroundLow != null) panelBackgroundLow.Visible = false;

                dataGridViewPlayList.Visible = true;
                btnSearch.Visible = true;
                panelSecondBackgroundHigh.Visible = true;

                int count = 1;
                foreach (var file in files)
                {
                    AddSongToGrid(file, ref count);
                }

                // UI 扫尾
                dataGridViewPlayList.ClearSelection();
                dataGridViewPlayList.CurrentCell = null;
                panelMiddleLine.Visible = true;
                panelMiddleLine.BringToFront();

                MessageBox.Show($"搜索完成，已加载 {dataGridViewPlayList.Rows.Count} 首歌曲。");
            }
            catch (Exception ex)
            {
                MessageBox.Show("搜索出错：" + ex.Message);
            }
        }

        private void dataGridViewPlayList_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            //处理普通数据行的绘制
            if (e.RowIndex >= 0)
            {
                //先画出背景颜色（处理选中高亮色）
                e.PaintBackground(e.CellBounds, true);

                //如果是“标题”列（索引为1）
                if (e.ColumnIndex == 1)
                {
                    string fullText = e.Value?.ToString() ?? "";
                    //将数据拆分成 歌名 和 歌手
                    string[] parts = fullText.Split('-');
                    string songName = parts[0].Trim();
                    string artistName = parts.Length > 1 ? parts[1].Trim() : "未知歌手";

                    //根据行高自动垂直居中
                    int totalTextHeight = 40;
                    int startY = e.CellBounds.Y + (e.CellBounds.Height - totalTextHeight) / 2;

                    //歌名：白色，12号微软雅黑
                    using (Font songFont = new Font("微软雅黑", 11, FontStyle.Bold))
                    {
                        e.Graphics.DrawString(songName, songFont, Brushes.White,
                            e.CellBounds.X + 5, startY);
                    }
                    //歌手：灰色（179,179,179），9号微软雅黑
                    using (Font artistFont = new Font("微软雅黑", 11, FontStyle.Regular))
                    using (Brush artistBrush = new SolidBrush(Color.FromArgb(179, 179, 179)))
                    {
                        e.Graphics.DrawString(artistName, artistFont, artistBrush,
                            e.CellBounds.X + 5, startY + 25);
                    }

                    e.Handled = true;//告诉系统，表格已画好，不需要操作了
                }
                //其他列
                else if (e.ColumnIndex >= 0)
                {
                    //判断当前列是否需要居中
                    var flags = TextFormatFlags.VerticalCenter;

                    if (e.ColumnIndex == 0 || e.ColumnIndex == 4)
                        flags |= TextFormatFlags.HorizontalCenter;//添加水平居中
                    else
                        flags |= TextFormatFlags.Left;

                    //统一灰色，且垂直居中
                    TextRenderer.DrawText(e.Graphics, e.Value?.ToString(), e.CellStyle.Font,
                        e.CellBounds, Color.FromArgb(179, 179, 179),
                        flags);

                    e.Handled = true;
                }
            }
            //处理表头的绘制（排序标志的位置）
            if (e.RowIndex == -1 && e.ColumnIndex >= 0)
            {
                e.PaintBackground(e.CellBounds, true);//绘制表头的背景

                //判断颜色，如果当前列==鼠标所在的列，则白色，否则灰色
                Color textColor = (e.ColumnIndex == hotColumnIndex) ? Color.White : Color.FromArgb(179, 179, 179);

                //判断是否需要居中
                var flags = TextFormatFlags.VerticalCenter | TextFormatFlags.NoPadding;

                if (e.ColumnIndex == 0 || e.ColumnIndex == 4)
                    flags |= TextFormatFlags.HorizontalCenter;
                else
                    flags |= TextFormatFlags.Left;

                //绘制表头文字
                TextRenderer.DrawText(e.Graphics, e.Value?.ToString(), e.CellStyle.Font,
                    e.CellBounds, textColor, flags);

                //检查当前列是否正在排序
                if (dataGridViewPlayList.SortedColumn != null && dataGridViewPlayList.SortedColumn.Index == e.ColumnIndex)
                {
                    //计算文字宽度
                    Size textSize = TextRenderer.MeasureText(e.Value.ToString(), e.CellStyle.Font);

                    float iconY = e.CellBounds.Y + (e.ClipBounds.Height - 5) / 2f;
                    float iconX;
                    if ((flags & TextFormatFlags.HorizontalCenter) == TextFormatFlags.HorizontalCenter)
                    {
                        iconX = e.CellBounds.Right - 15;
                    }
                    else
                    {
                        iconX = e.CellBounds.X + textSize.Width + 10;
                    }

                    //绘制排序小三角形
                    PointF[] points;
                    if (dataGridViewPlayList.SortOrder == SortOrder.Ascending)
                        points = new PointF[] { new PointF(iconX, iconY + 5), new PointF(iconX + 5, iconY), new PointF(iconX + 10, iconY + 5) };
                    else
                        points = new PointF[] { new PointF(iconX, iconY), new PointF(iconX + 5, iconY + 5), new PointF(iconX + 10, iconY) };

                    e.Graphics.FillPolygon(new SolidBrush(Color.FromArgb(179, 179, 179)), points);
                }
                e.Handled = true;
            }
        }

        private void dataGridViewPlayList_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex >= 0)//确保是表头,且索引有效
            {
                hotColumnIndex = e.ColumnIndex;//记录鼠标在哪一列
                //强调重绘表头
                dataGridViewPlayList.InvalidateColumn(e.ColumnIndex);
            }
        }

        private void dataGridViewPlayList_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                int lastHotIndex = hotColumnIndex;
                hotColumnIndex = -1;
                //恢复默认颜色
                dataGridViewPlayList.InvalidateColumn(lastHotIndex);
            }

        }

        private void dataGridViewPlayList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //确保是有效行，不是表头
            if (e.RowIndex >= 0)
            {
                //获得index 5的文件路径
                string filePath = dataGridViewPlayList.Rows[e.RowIndex].Cells[5].Value?.ToString();

                if (System.IO.File.Exists(filePath))
                {
                    player.URL = filePath;
                    player.controls.play();

                    //立即启动计时器并先强制执行一次UI更新
                    playTimer.Start();
                    UpdateUIImmediately();
                }
            }
        }

        private void PlayTimer_Tick(object sender, EventArgs e)
        {
            if (player.playState == WMPPlayState.wmppsPlaying && !isDraggingProgress)
            {
                //更新时间标签
                mainForm.labelStart.Text = player.controls.currentPositionString;
                mainForm.labelEnd.Text = player.currentMedia.durationString;

                //检查duration是否大于0
                double currentPos = player.controls.currentPosition;
                double totalLen = player.currentMedia.duration;

                double progress = 0;
                if (totalLen > 0)
                {
                    progress = currentPos / totalLen;
                }

                //绘制背景进度条
                DrawCustomBar(mainForm.pictureBoxLoadLine, progress, Color.White);
            }
        }

        //监听播放状态：播放/暂停图标的显示
        private void Player_PlayStateChange(int NewState)
        {
            if (NewState == (int)WMPPlayState.wmppsPlaying)
            {
                mainForm.pictureBoxPlay.Visible = false;
                mainForm.pictureBoxStop.Visible = true;
            }
            else
            {
                mainForm.pictureBoxPlay.Visible = true;
                mainForm.pictureBoxStop.Visible = false;
            }

            //当一首歌结束时
            if (NewState == 8)
            {
                this.BeginInvoke(new Action(() =>
                {
                    if (cycleMode == 2)//单曲循环
                    {
                        player.controls.play();
                    }
                    else
                    {
                        PlayNext();
                    }
                }));
            }
        }

        //下一首
        private void PlayNext()
        {
            if (dataGridViewPlayList.Rows.Count == 0) return;

            int nextIndex;
            int currentIndex = dataGridViewPlayList.CurrentRow != null ? dataGridViewPlayList.CurrentRow.Index : 0;

            if (isRandom)
            {
                //生成一个不等于当前索引的随机数
                if (dataGridViewPlayList.Rows.Count > 1)
                {
                    do
                    {
                        nextIndex = rnd.Next(0, dataGridViewPlayList.Rows.Count);
                    } while (nextIndex == currentIndex);
                }
                else
                {
                    nextIndex = 0;
                }
            }
            else
            {
                //顺序逻辑
                nextIndex = (currentIndex + 1) % dataGridViewPlayList.Rows.Count;
            }

            PlayByIndex(nextIndex);
        }
        //上一首
        private void PlayPrevious()
        {
            if (dataGridViewPlayList.Rows.Count == 0) return;

            //获取当前选中的行索引
            int currenIndex = dataGridViewPlayList.CurrentRow != null ? dataGridViewPlayList.CurrentRow.Index : 0;

            //计算上一首的索引（如果是第一首，就跳到最后一首）
            int prevIndex = (currenIndex - 1 + dataGridViewPlayList.Rows.Count) % dataGridViewPlayList.Rows.Count;

            //选中新行
            dataGridViewPlayList.ClearSelection();
            dataGridViewPlayList.Rows[prevIndex].Selected = true;
            dataGridViewPlayList.CurrentCell = dataGridViewPlayList.Rows[prevIndex].Cells[0];

            //播放
            string path = dataGridViewPlayList.Rows[prevIndex].Cells[5].Value?.ToString();
            if (!string.IsNullOrEmpty(path))
            {
                player.URL = path;
                player.controls.play();
            }
        }

        //刷新图标显示逻辑
        private void RefreshModeUI()
        {
            mainForm.pictureBoxRandom.Visible = isRandom;
            mainForm.pictureBoxRandomNot.Visible = !isRandom;

            mainForm.pictureBoxCycleNot.Visible = (cycleMode == 0);
            mainForm.pictureBoxCycle.Visible = (cycleMode == 1);
            mainForm.pictureBoxCycleSolo.Visible = (cycleMode == 2);
        }

        //封装一个通用的播放方法
        private void PlayByIndex(int index)
        {
            if (index >= 0 && index < dataGridViewPlayList.Rows.Count)
            {
                dataGridViewPlayList.ClearSelection();
                dataGridViewPlayList.Rows[index].Selected = true;
                dataGridViewPlayList.CurrentCell = dataGridViewPlayList.Rows[index].Cells[0];

                //让滚动条跟随到选中的行
                dataGridViewPlayList.FirstDisplayedScrollingRowIndex = index;

                string path = dataGridViewPlayList.Rows[index].Cells[5].Value.ToString();
                player.URL = path;
                player.controls.play();
            }
        }

        //更新ui表现
        private void UpdateProgressByMouse(int mouseX)
        {
            double ratio = (double)mouseX / mainForm.pictureBoxLoadLine.Width;
            ratio = Math.Max(0, Math.Min(1, ratio));
            DrawCustomBar(mainForm.pictureBoxLoadLine, ratio, Color.White);
        }
        private void SetVolumeByMouse(int mouseX)
        {
            double ratio = (double)mouseX / mainForm.pictureBoxVolumeLoadLine.Width;
            ratio = Math.Max(0, Math.Min(1, ratio));
            player.settings.volume = (int)(ratio * 100);
            DrawCustomBar(mainForm.pictureBoxVolumeLoadLine, ratio, Color.White);
        }

        private void panelbackground_Paint(object sender, PaintEventArgs e)
        {
            LinearGradientBrush brush = new LinearGradientBrush(
                panelBackgroundHigh.ClientRectangle,
                Color.FromArgb(42, 42, 42),
                Color.FromArgb(18, 18, 18),
                LinearGradientMode.Vertical);

            e.Graphics.FillRectangle(brush, panelBackgroundHigh.ClientRectangle);
        }

        private void DrawGradingBackground(object sender, PaintEventArgs e)
        {
            Control control = sender as Control;
            if (control == null) return;

            Color colorTop = Color.FromArgb(42, 42, 42);
            Color colorBottom = Color.FromArgb(18, 18, 18);

            using (LinearGradientBrush brush = new LinearGradientBrush(
                    control.ClientRectangle,
                    colorTop,
                    colorBottom,
                    LinearGradientMode.Vertical))
            {
                e.Graphics.FillRectangle(brush, control.ClientRectangle);
            }
        }

        //即时更新方法
        private void UpdateUIImmediately()
        {
            mainForm.labelStart.Text = player.controls.currentPositionString;
            mainForm.labelEnd.Text = player.currentMedia.durationString;
            double progress = player.currentMedia.duration > 0 ?
                player.controls.currentPosition / player.currentMedia.duration : 0;
            DrawCustomBar(mainForm.pictureBoxLoadLine, progress, Color.White);
        }

        private void dataGridViewPlayList_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // 检查是否为鼠标右键点击，且点击在有效行上
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                // 处理行选中（如果当前行未选中，则选中它）
                if (!dataGridViewPlayList.Rows[e.RowIndex].Selected)
                {
                    dataGridViewPlayList.ClearSelection();
                    dataGridViewPlayList.Rows[e.RowIndex].Selected = true;
                }

                //防止菜单重叠（如果旧菜单还在，先关闭并释放）
                if (contextMenu != null && !contextMenu.IsDisposed)
                {
                    contextMenu.Close();
                    contextMenu.Dispose();
                }

                // 获取选中的歌曲路径
                List<string> selectedPaths = new List<string>();
                foreach (DataGridViewRow row in dataGridViewPlayList.SelectedRows)
                {
                    if (row.Cells[5].Value != null)
                    {
                        selectedPaths.Add(row.Cells[5].Value.ToString());
                    }
                }

                contextMenu = new RightClickMenu(selectedPaths, this);
                contextMenu.StartPosition = FormStartPosition.Manual;
                contextMenu.Location = Cursor.Position;

                contextMenu.FormClosed += (s, ev) => {
                    this.BeginInvoke(new Action(() => {
                        this.Activate();
                        this.Focus();
                    }));
                };

                contextMenu.Show();
                contextMenu.Activate();
            }
        }

        //接受并显示歌单内容
        public void LoadPlaylistToGrid(string playlistName, List<string> paths)
        {
            // 基础清理与状态切换
            dataGridViewPlayList.Rows.Clear();
            panelBackgroundHigh.Visible = false;
            if (panelBackgroundLow != null) panelBackgroundLow.Visible = false;
            panelSecondBackgroundHigh.Visible = true;
            dataGridViewPlayList.Visible = true;
            labelAdd.Visible = false;
            labelSearch.Visible = false;

            if (paths == null || paths.Count == 0)
            {
                this.BringToFront();
                return;
            }

            // 使用 HashSet 记录已处理过的路径，防止传入的 paths 列表本身有重复
            HashSet<string> uniquePaths = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            int count = 1;
            foreach (var file in paths)
            {
                // 如果文件不存在，或者该路径已经添加过了，则跳过
                if (!System.IO.File.Exists(file) || uniquePaths.Contains(file))
                    continue;

                try
                {
                    using (var tfile = TagLib.File.Create(file))
                    {
                        var artists = string.Join(",", tfile.Tag.Performers);
                        if (string.IsNullOrEmpty(artists)) artists = "未知歌手";

                        string songTitle = tfile.Tag.Title ?? Path.GetFileNameWithoutExtension(file);
                        string combinedTitle = $"{songTitle}-{artists}";
                        string album = tfile.Tag.Album ?? "未知专辑";
                        string durationStr = tfile.Properties.Duration.ToString(@"mm\:ss");
                        string dataAdded = new FileInfo(file).CreationTime.ToString("yyyy-MM-dd");

                        // 添加到表格
                        dataGridViewPlayList.Rows.Add(count++, combinedTitle, album, dataAdded, durationStr, file);

                        // 将路径加入已处理集合
                        uniquePaths.Add(file);
                    }
                }
                catch
                {
                    dataGridViewPlayList.Rows.Add(count++, Path.GetFileNameWithoutExtension(file), "未知专辑", "", "00:00", file);
                    uniquePaths.Add(file);
                }
            }

            // 强制显示
            panelMiddleLine.BringToFront();
            btnSearch.BringToFront();
            this.BringToFront();
        }

        private void dataGridViewPlayList_MouseDown(object sender, MouseEventArgs e)
        {
            // 如果点击的是 DataGridView 的空白处（没有行的地方）
            if (dataGridViewPlayList.HitTest(e.X, e.Y).Type == DataGridViewHitTestType.None)
            {
                dataGridViewPlayList.ClearSelection();
            }
        }

        public void ResetToAllMusic()
        {
            // 判断缓存里有没有歌
            if (_allLocalMusicCache != null && _allLocalMusicCache.Count > 0)
            {
                // === 情况 A：之前导入过歌曲，恢复显示 ===

                // 切换 UI：隐藏欢迎页，显示列表页
                labelAdd.Visible = false;
                labelSearch.Visible = false;
                panelBackgroundHigh.Visible = false;
                if (panelBackgroundLow != null) panelBackgroundLow.Visible = false;

                panelSecondBackgroundHigh.Visible = true;
                dataGridViewPlayList.Visible = true;
                btnSearch.Visible = true;

                // 重新填充表格 (复用缓存)
                dataGridViewPlayList.Rows.Clear();
                int count = 1;
                foreach (var file in _allLocalMusicCache)
                {
                    AddSongToGrid(file, ref count);
                }
            }
            else
            {
                // === 情况 B：没有任何歌曲，显示初始欢迎页 ===
                labelAdd.Visible = true;
                labelSearch.Visible = true;

                panelBackgroundHigh.Visible = true;
                panelSecondBackgroundHigh.Visible = false;
                if (panelBackgroundLow != null) panelBackgroundLow.Visible = true;

                dataGridViewPlayList.Visible = false;
                dataGridViewPlayList.Rows.Clear();
            }

            // 确保分割线和搜索按钮在最上层
            panelMiddleLine.BringToFront();
            btnSearch.BringToFront();

            this.BringToFront();
        }

        private void AddSongToGrid(string file, ref int count)
        {
            if (!System.IO.File.Exists(file)) return;
            try
            {
                using (var tfile = TagLib.File.Create(file))
                {
                    var artists = string.Join(",", tfile.Tag.Performers);
                    if (string.IsNullOrEmpty(artists)) artists = "未知歌手";
                    string songTitle = tfile.Tag.Title ?? Path.GetFileNameWithoutExtension(file);
                    string combinedTitle = $"{songTitle}-{artists}";
                    string album = tfile.Tag.Album ?? "未知专辑";
                    string durationStr = tfile.Properties.Duration.ToString(@"mm\:ss");
                    string dataAdded = new FileInfo(file).CreationTime.ToString("yyyy-MM-dd");

                    dataGridViewPlayList.Rows.Add(count++, combinedTitle, album, dataAdded, durationStr, file);
                }
            }
            catch
            {
                dataGridViewPlayList.Rows.Add(count++, Path.GetFileNameWithoutExtension(file), "未知专辑", "", "00:00", file);
            }
        }

        public void AutoLoadUserMusic()
        {
            if (mainForm == null) return;

            SqliteHelper dbHelper = new SqliteHelper();
            List<string> userFiles = dbHelper.GetUserMusicPaths(Main.CurrentUserId);

            if (userFiles.Count > 0)
            {
                this._allLocalMusicCache = new List<string>(userFiles);

                this.LoadPlaylistToGrid("我的库", userFiles);

                labelAdd.Visible = false;
                labelSearch.Visible = false;
                dataGridViewPlayList.Visible = true;
                panelSecondBackgroundHigh.Visible = true;
                btnSearch.Visible = true;
            }
        }
    }
    //音乐信息
    public class MusicInfo
    {
        public int Index { get; set; }
        public string Title { get; set; }
        public string Album {  get; set; }
        public string DataAdded {  get; set; }
        public string Duration {  get; set; }
        public string Path {  get; set; }
    }
}
