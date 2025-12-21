using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;

namespace MusicPlayer
{
    public class SqliteHelper
    {
        private static string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "UserSystem.db");
        private string connStr = $"Data Source={dbPath};Version=3;";

        public void RegisterUser(string email, string pwd, string user)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();

                string checksql = "SELECT COUNT(*) FROM [Users] WHERE Username = @user OR Email = @email";
                using (SQLiteCommand checkCmd = new SQLiteCommand(checksql, conn))
                {
                    checkCmd.Parameters.AddWithValue("@user", user);
                    checkCmd.Parameters.AddWithValue("@email", email);
                    if ((long)checkCmd.ExecuteScalar() > 0)
                    {
                        MessageBox.Show("用户名或邮箱已存在！");
                        return;
                    }
                }

                string sql = "INSERT INTO [Users] ([Email], [Password], [Username]) VALUES (@email, @pwd, @user)";
                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@pwd", pwd);
                    cmd.Parameters.AddWithValue("@user", user);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("注册成功！");
                }
            }
        }

        public bool CheckLogin(string identifier, string pwd)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();

                //用户名或邮箱匹配，且密码必须正确
                string sql = "SELECT COUNT(*) FROM [Users] WHERE ([Username] = @id OR [Email] = @id) AND [Password] = @pwd";

                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", identifier);
                    cmd.Parameters.AddWithValue("@pwd", pwd);
                    return (long)cmd.ExecuteScalar() > 0;
                }
            }
        }
    }
}
