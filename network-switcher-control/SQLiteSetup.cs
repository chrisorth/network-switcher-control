using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using System.Data.SQLite;

namespace network_switcher_control
{
    class SQLiteSetup
    {
        public static readonly int CurrentDatabaseVersion = 1;

        public static void RunDBSetup(string sqlFileName)
        {
            bool sqlFileCreated = false;
            string sql = String.Empty;
            SQLiteCommand sqliteCommand;

            if (!File.Exists(sqlFileName))
            {
                SQLiteConnection.CreateFile(sqlFileName);

                sqlFileCreated = true;
            }

            SQLiteConnection sqlConn = new SQLiteConnection(String.Format("Data Source={0};Version=3", sqlFileName));
            sqlConn.Open();

            if (sqlFileCreated)
            {
                //setup the database after a fresh install
                sql = "CREATE TABLE DatabaseVersion (version INT)";
                sqliteCommand = new SQLiteCommand(sql, sqlConn);
                sqliteCommand.ExecuteNonQuery();

                sql = "CREATE TABLE MainNetworkConfig(ID INT, Name VARCHAR(50), ConnectionName VARCHAR(255), IpAddr VARCHAR(15), NetMask VARCHAR(15), DefaultGateway VARCHAR(15), Dns1 VARCHAR(15), Dns2 VARCHAR(15))";
                sqliteCommand = new SQLiteCommand(sql, sqlConn);
                sqliteCommand.ExecuteNonQuery();

                sql = "CREATE TABLE SecondaryNetworkConfig(ID INT, MainNetworkConfigID INT, IpAddr VARCHAR(15), NetMask VARCHAR(15))";
                sqliteCommand = new SQLiteCommand(sql, sqlConn);
                sqliteCommand.ExecuteNonQuery();

                sql = "INSERT INTO DatabaseVersion (Version) VALUES (1)";
                sqliteCommand = new SQLiteCommand(sql, sqlConn);
                sqliteCommand.ExecuteNonQuery();
            }

            // if any changes query the databaseversion and do the updates inserts or creates.

            sqlConn.Close();              
        }

        public static int DatabaseVersion(string sqlFileName)
        {
            if (!File.Exists(sqlFileName))
            {
                return 0;
            }

            string sql = String.Empty;
            SQLiteCommand sqliteCommand;
            SQLiteConnection sqlConn =  new SQLiteConnection(String.Format("Data Source={0};Version=3", sqlFileName));
            sqlConn.Open();

            sql = "SELECT TOP 1 version FROM DatabaseVersion";
            sqliteCommand = new SQLiteCommand(sql, sqlConn);
            SQLiteDataReader reader = sqliteCommand.ExecuteReader();

            return (int)reader["version"];

        }
    }
}
