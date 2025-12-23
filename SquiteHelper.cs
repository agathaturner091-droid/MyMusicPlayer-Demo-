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
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();

                // 1. 用户表)
                string sqlUser = @"CREATE TABLE IF NOT EXISTS Users (
                            id INTEGER PRIMARY KEY AUTOINCREMENT, 
                            username TEXT NOT NULL,
                            password TEXT NOT NULL,
                            email TEXT NOT NULL);";

                // 2. 音频资源索引表
                string sqlMusic = @"CREATE TABLE IF NOT EXISTS MusicTracks (
                            trackId INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
                            songName TEXT,
                            artist TEXT,
                            album TEXT,
                            duration TEXT,
                            createTime DATETIME,
                            filePath TEXT NOT NULL UNIQUE,
                            fileFormat TEXT);";

                using (SQLiteCommand cmd = new SQLiteCommand(sqlUser, conn)) { cmd.ExecuteNonQuery(); }
                using (SQLiteCommand cmd = new SQLiteCommand(sqlMusic, conn)) { cmd.ExecuteNonQuery(); }
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

        public void SaveTrack(string name, string artist, string album, string duration, string path, string format)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                string sql = @"INSERT OR IGNORE INTO MusicTracks 
                       (songName, artist, album, duration, createTime, filePath, fileFormat) 
                       VALUES (@name, @artist, @album, @dur, @time, @path, @fmt)";

                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@artist", artist);
                    cmd.Parameters.AddWithValue("@album", album);
                    cmd.Parameters.AddWithValue("@dur", duration);
                    cmd.Parameters.AddWithValue("@time", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd.Parameters.AddWithValue("@path", path);
                    cmd.Parameters.AddWithValue("@fmt", format);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
