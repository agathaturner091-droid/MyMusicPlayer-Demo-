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

        private System.Windows.Forms.Timer mouseCheckTimer;

        public RightClickMenu(List<string> paths, OpenFiles parent)
        {
            InitializeComponent();
            this._paths = paths;
            this._parent = parent;

            this.ShowInTaskbar = false;

            // 调用示例
            RemoveHoverEffect(btnShare);
            RemoveHoverEffect(btnRemove);
            RemoveHoverEffect(btnAdd);

            // 参数：(目标Panel, 鼠标移入时的颜色, 默认颜色)
            BindEvents(panelShare, btnShare_Click);
            BindEvents(panelRemove, btnRemove_Click);
            BindEvents(panelAdd, btnAdd_Click);

            // 标准方案：失去焦点时自动关闭菜单
            this.Click += (s, e) => this.Close();

            // 关键点 3：优化后的失焦关闭逻辑
            this.Deactivate += async (s, e) => {
                // 给系统 200 毫秒时间来处理可能发生的 Click 事件
                await System.Threading.Tasks.Task.Delay(200);

                // 只有当鼠标真的不在菜单范围内，且用户没有按着鼠标键时，才关闭
                if (!this.Bounds.Contains(Cursor.Position) && Control.MouseButtons == MouseButtons.None)
                {
                    if (!this.IsDisposed) this.Close();
                }
            };

            btnShare.BringToFront();
            btnRemove.BringToFront();
            btnAdd.BringToFront();

            btnShare.Enabled = true;
            btnRemove.Enabled = true;
            btnAdd.Enabled = true;
            btnShare.Visible = true;
            btnRemove.Visible = true;
            btnAdd.Visible = true;
        }



        private void BindEvents(Panel p, EventHandler clickEvent)
        {
            // 鼠标移入变色，移出透明
            p.MouseEnter += (s, e) => p.BackColor = Color.FromArgb(60, 60, 60);
            p.MouseLeave += (s, e) => {
                if (!p.ClientRectangle.Contains(p.PointToClient(Cursor.Position)))
                    p.BackColor = Color.Transparent;
            };

            foreach (Control c in p.Controls)
            {
                c.Click += clickEvent;

                // 可选：让子控件进入时也能触发 Panel 的变色
                c.MouseEnter += (s, e) => p.BackColor = Color.FromArgb(60, 60, 60);
            }
        }

        private void btnShare_Click(object sender, EventArgs e)
        {
            this.Close();
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
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            this.Close();
            var result = MessageBox.Show($"确定要从列表中移除选中的{_paths.Count}首歌曲吗？", "提示", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                //倒序删除DataGridView中的行，防止索引错乱
                for (int i = _parent.dataGridViewPlayList.SelectedRows.Count - 1; i >= 0; i--)
                {
                    _parent.dataGridViewPlayList.Rows.Remove(_parent.dataGridViewPlayList.SelectedRows[i]);
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.Close();
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
