using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;

namespace MusicPlayer
{

    public partial class RightClickMenu : Form
    {
        private List<string> _paths;
        private OpenFiles _parent;

        public RightClickMenu(List<string> paths, OpenFiles parent)
        {
            InitializeComponent();
            this._paths = paths;
            this._parent = parent;

            this.ShowInTaskbar = false;
            this.Owner = parent.FindForm();

            // 调用示例
            RemoveHoverEffect(btnShare);
            RemoveHoverEffect(btnRemove);
            RemoveHoverEffect(btnAdd);

            // 参数：(目标Panel, 鼠标移入时的颜色, 默认颜色)
            BindHoverEvents(panelShare, Color.FromArgb(60, 60, 60), Color.Transparent);
            BindHoverEvents(panelRemove, Color.FromArgb(60, 60, 60), Color.Transparent);
            BindHoverEvents(panelAdd, Color.FromArgb(60, 60, 60), Color.Transparent);
        }



        private void BindHoverEvents(Panel p, Color hoverColor, Color defaultColor)
        {
            // 定义进入逻辑
            EventHandler enter = (s, e) => p.BackColor = hoverColor;
            // 定义离开逻辑
            EventHandler leave = (s, e) => p.BackColor = defaultColor;

            p.MouseEnter += enter;
            p.MouseLeave += leave;

            foreach (Control c in p.Controls)
            {
                c.MouseEnter += enter;
                c.MouseLeave += leave;
            }
        }

        private void btnShare_Click(object sender, EventArgs e)
        {
            //单个文件导出
            if (_paths.Count == 1)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.FileName = Path.GetFileName(_paths[0]);
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    File.Copy(_paths[0], sfd.FileName, true);
                    MessageBox.Show("分享成功！");
                }
            }
            //多个文件导出选择文件夹
            else
            {
                FolderBrowserDialog fbd = new FolderBrowserDialog();
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    foreach (var path in _paths)
                    {
                        string dest = Path.Combine(fbd.SelectedPath, Path.GetFileName(path));
                        File.Copy(path, dest, true);
                    }
                    MessageBox.Show($"已成功分享{_paths.Count}首歌曲到目标目录。");
                }
            }
            this.Close();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show($"确定要从列表中移除选中的{_paths.Count}首歌曲吗？", "提示", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                //倒序删除DataGridView中的行，防止索引错乱
                for (int i = _parent.dataGridViewPlayList.SelectedRows.Count - 1; i >= 0; i--)
                {
                    _parent.dataGridViewPlayList.Rows.Remove(_parent.dataGridViewPlayList.SelectedRows[i]);
                }
            }
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //弹出创建歌单的界面
            EditDetails editForm = new EditDetails(_paths, _parent);
            //等用户操作完再继续
            editForm.ShowDialog();

            this.Close();
        }
        void RemoveHoverEffect(Button btn)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            // 将鼠标滑过和按下时的颜色设为与按钮背景色相同
            btn.FlatAppearance.MouseOverBackColor = btn.BackColor;
            btn.FlatAppearance.MouseDownBackColor = btn.BackColor;
        }

        private void RightClickMenu_MouseLeave(object sender, EventArgs e)
        {
            // 检查鼠标当前位置是否真的在窗口矩形范围之外
            if (!this.ClientRectangle.Contains(this.PointToClient(Cursor.Position)))
            {
                this.Close();
            }
        }
    }
}
