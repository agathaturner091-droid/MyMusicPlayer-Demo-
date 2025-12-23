using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MusicPlayer
{
    public partial class EditDetails : Form
    {
        private List<string> _tempPaths;
        private OpenFiles _openFiles;

        public EditDetails(List<string> paths, OpenFiles openFiles)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterParent;

            this._tempPaths = paths;
            this._openFiles = openFiles;
        }

        private void pictureBoxSave_Click(object sender, EventArgs e)
        {
            string newName = textBoxPlayListName.Text.Trim();

            if (string.IsNullOrEmpty(newName))
            {
                MessageBox.Show("请输入歌单名称！");
                return;
            }

            SqliteHelper db = new SqliteHelper();

            // 1. 创建歌单并拿到 ID（只调用这一次！）
            int newPid = db.CreatePlaylistAndGetId(newName, Main.CurrentUserId);

            if (newPid > 0)
            {
                // 2. 循环将歌曲路径转换成 ID 并存入映射表
                foreach (string path in _tempPaths)
                {
                    int tid = db.GetTrackIdByPath(path);
                    if (tid > 0)
                    {
                        db.AddTrackToPlaylist(newPid, tid);
                    }
                }

                // 3. 统计选中的数量，更新这个歌单的总数
                int songCount = _tempPaths != null ? _tempPaths.Count : 0;
            }

            // 4. 更新 UI 逻辑保持不变
            if (_openFiles.Tag is Main main)
            {
                main.CreateNewPlayListUI(newName, _tempPaths);
                this.Close();
            }
            else
            {
                Main m = (Main)Application.OpenForms["Main"];
                m?.CreateNewPlayListUI(newName, _tempPaths);
                this.Close();
            }
        }

        private void labelClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
