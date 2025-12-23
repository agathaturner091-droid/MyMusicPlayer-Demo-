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
            int songCount = _tempPaths != null ? _tempPaths.Count : 0;

            db.CreatePlaylist(newName, Main.CurrentUserId, songCount);
            // ------------------------------------

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
