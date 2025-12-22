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

        public EditDetails(List<string> paths,OpenFiles openFiles)
        {
            InitializeComponent();

            this._tempPaths = paths;
            this._openFiles = openFiles;
        }

        private void pictureBoxSave_Click(object sender, EventArgs e)
        {
            string newName=textBoxPlayListName.Text.Trim();

            if (string.IsNullOrEmpty(newName))
            {
                MessageBox.Show("请输入歌单名称！");
                return;
            }

            //找到Main界面，并调用方法
            if (_openFiles.Tag is Main main)
            {
                main.CreateNewPlayListUI(newName, _tempPaths);
                this.Close();
            }
            else
            {
                //如果Tag没设，尝试通过ParentForm找
                Main m = (Main)Application.OpenForms["Main"];
                m?.CreateNewPlayListUI(newName, _tempPaths);
                this.Close();
            }
        }
    }
}
