using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Text;
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

                // 1. 用户表语句
                string sqlUser = @"CREATE TABLE IF NOT EXISTS Users (
                    id INTEGER PRIMARY KEY AUTOINCREMENT, 
                    username TEXT NOT NULL,
                    password TEXT NOT NULL,
                    email TEXT NOT NULL);";

                // 2. 歌曲表语句
                string sqlMusic = @"CREATE TABLE IF NOT EXISTS MusicTracks (
                    trackId INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
                    userId INTEGER,
                    songName TEXT,
                    artist TEXT,
                    album TEXT,
                    duration TEXT NOT NULL,
                    createTime DATETIME,
                    filePath TEXT NOT NULL UNIQUE,
                    fileFormat TEXT NOT NULL);";

                // 3. 歌单表语句
                string sqlPlaylist = @"CREATE TABLE IF NOT EXISTS Playlists (
                    playlistId INTEGER PRIMARY KEY AUTOINCREMENT,
                    userId INTEGER NOT NULL,
                    playlistName TEXT NOT NULL,
                    createTime DATETIME,
                    songCount INTEGER DEFAULT 0);";

                //4、歌单歌曲关联表
                string sqlMap = @"CREATE TABLE IF NOT EXISTS PlaylistTrackMap (
                    mapId INTEGER PRIMARY KEY AUTOINCREMENT,
                    playlistId INTEGER NOT NULL,
                    trackId INTEGER NOT NULL);";

                string sqlAudio = @"CREATE TABLE IF NOT EXISTS AudioSettings (
                    settingId INTEGER PRIMARY KEY AUTOINCREMENT,
                    userId INTEGER NOT NULL UNIQUE, 
                    volume INTEGER DEFAULT 50,
                    eqBase REAL DEFAULT 0.0,
                    eqTreble REAL DEFAULT 0.0,
                    playMode TEXT DEFAULT 'Order',
                    updateTime DATETIME);";

                using (SQLiteCommand cmd = new SQLiteCommand(sqlUser, conn)) { cmd.ExecuteNonQuery(); }
                using (SQLiteCommand cmd = new SQLiteCommand(sqlMusic, conn)) { cmd.ExecuteNonQuery(); }
                using (SQLiteCommand cmd = new SQLiteCommand(sqlPlaylist, conn)) { cmd.ExecuteNonQuery(); }
                using (SQLiteCommand cmd = new SQLiteCommand(sqlMap, conn)) { cmd.ExecuteNonQuery(); }
                using (SQLiteCommand cmd = new SQLiteCommand(sqlAudio, conn)) { cmd.ExecuteNonQuery(); }
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

        public int GetUserIdAfterLogin(string account, string pwd)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                string sql = "SELECT id FROM Users WHERE username = @user AND password = @pwd";
                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@user", account);
                    cmd.Parameters.AddWithValue("@pwd", pwd);
                    object result = cmd.ExecuteScalar();
                    return result != null ? Convert.ToInt32(result) : 0;
                }
            }
        }

        public void SaveTrack(string name, string artist, string album, string duration, string path, string format, int userId)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                string sql = "INSERT OR IGNORE INTO MusicTracks (userId, songName, artist, album, duration, filePath, fileFormat, createTime) " +
                                    "VALUES (@uid, @name, @artist, @album, @duration, @path, @format, @time)";

                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@uid", userId); // 绑定当前用户ID
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@artist", artist);
                    cmd.Parameters.AddWithValue("@album", album);
                    cmd.Parameters.AddWithValue("@duration", duration);
                    cmd.Parameters.AddWithValue("@path", path);
                    cmd.Parameters.AddWithValue("@format", format);
                    cmd.Parameters.AddWithValue("@time", DateTime.Now);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<string> GetUserMusicPaths(int userId)
        {
            List<string> paths = new List<string>();
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                string sql = "SELECT filePath FROM MusicTracks WHERE userId = @uid";
                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@uid", userId);
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            paths.Add(reader["filePath"].ToString());
                        }
                    }
                }
            }
            return paths;
        }

        public int CreatePlaylistAndGetId(string name, int userId)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                string sql = "INSERT INTO Playlists (userId, playlistName, createTime, songCount) VALUES (@uid, @name, @time, 0); SELECT last_insert_rowid();";
                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@uid", userId);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@time", DateTime.Now);
                    object result = cmd.ExecuteScalar();
                    return result != null ? Convert.ToInt32(result) : 0;
                }
            }
        }

        public void CreatePlaylist(string name, int userId, int count)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                // 插入歌单基本信息
                string sql = "INSERT INTO Playlists (userId, playlistName, createTime, songCount) VALUES (@uid, @name, @time, @count)";
                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@uid", userId);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@time", DateTime.Now);
                    cmd.Parameters.AddWithValue("@count", count); // 歌曲总数
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void AddTrackToPlaylist(int playlistId, int trackId)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                string sql = "INSERT INTO PlaylistTrackMap (playlistId, trackId) VALUES (@pid, @tid)";
                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@pid", playlistId);
                    cmd.Parameters.AddWithValue("@tid", trackId);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public int GetTrackIdByPath(string path)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                string sql = "SELECT trackId FROM MusicTracks WHERE filePath = @path";
                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@path", path);
                    object result = cmd.ExecuteScalar();
                    return result != null ? Convert.ToInt32(result) : 0;
                }
            }
        }

        public void SaveAudioSettings(int userId, int volume, string mode)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                // 使用 INSERT OR REPLACE 处理：如果用户配置已存在则更新，不存在则插入
                string sql = @"INSERT OR REPLACE INTO AudioSettings (userId, volume, playMode, updateTime) 
                       VALUES (@uid, @vol, @mode, @time)";
                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@uid", userId);
                    cmd.Parameters.AddWithValue("@vol", volume);
                    cmd.Parameters.AddWithValue("@mode", mode);
                    cmd.Parameters.AddWithValue("@time", DateTime.Now);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
