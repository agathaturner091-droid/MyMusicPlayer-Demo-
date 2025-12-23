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

        public SqliteHelper()
        {
            // 每次实例化时检查数据库和表是否存在
            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            // 如果数据库文件不存在，SQLiteConnection 打开时会自动创建文件
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();

                // 创建 Users 表的 SQL 语句
                // 对应论文设计：id(主键), username, password, email
                string sql = @"
                    CREATE TABLE IF NOT EXISTS Users (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT, 
                        Username TEXT NOT NULL,
                        Password TEXT NOT NULL,
                        Email TEXT NOT NULL
                    );";

                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void RegisterUser(string email, string pwd, string user)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();

                // 1. 查重：检查用户名或邮箱是否已存在
                string checksql = "SELECT COUNT(*) FROM Users WHERE username = @user OR email = @email";
                using (SQLiteCommand checkCmd = new SQLiteCommand(checksql, conn))
                {
                    checkCmd.Parameters.AddWithValue("@user", user);
                    checkCmd.Parameters.AddWithValue("@email", email);
                    long count = (long)checkCmd.ExecuteScalar();
                    if (count > 0)
                    {
                        MessageBox.Show("注册失败：用户名或邮箱已存在！");
                        return;
                    }
                }

                // 2. 插入数据：对应设计表字段
                string sql = "INSERT INTO Users (email, password, username) VALUES (@email, @pwd, @user)";
                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@pwd", pwd);
                    cmd.Parameters.AddWithValue("@user", user);

                    int result = cmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        MessageBox.Show("注册成功！请返回登录。");
                    }
                }
            }
        }

        public bool CheckLogin(string identifier, string pwd)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();

                // 支持用“用户名”或“邮箱”登录
                string sql = "SELECT COUNT(*) FROM Users WHERE (username = @id OR email = @id) AND password = @pwd";

                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", identifier);
                    cmd.Parameters.AddWithValue("@pwd", pwd);

                    long count = (long)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
        }
    }
}
