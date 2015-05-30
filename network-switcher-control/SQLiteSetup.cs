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
            string sql = String.Empty;
            SQLiteCommand sqliteCommand;

            if (!File.Exists(sqlFileName))
            {
                SQLiteConnection.CreateFile(sqlFileName);
            }

            SQLiteConnection sqlConn = new SQLiteConnection(String.Format("Data Source={0};Version=3", sqlFileName));
            sqlConn.Open();

            int usersDBVersion = DatabaseVersion(sqlFileName);

            if (File.Exists(sqlFileName))
            {
                if (usersDBVersion < CurrentDatabaseVersion)
                {
                    //setup the database after a fresh install
                    sql = "CREATE TABLE DatabaseVersion (Version INT)";
                    sqliteCommand = new SQLiteCommand(sql, sqlConn);
                    sqliteCommand.ExecuteNonQuery();

                    sql = "CREATE TABLE MainNetworkConfig(ID INT, ConnectionName VARCHAR(255), IpAddress VARCHAR(15), NetMask VARCHAR(15), DefaultGateway VARCHAR(15), PrimaryDNS VARCHAR(15), SecondaryDNS VARCHAR(15))";
                    sqliteCommand = new SQLiteCommand(sql, sqlConn);
                    sqliteCommand.ExecuteNonQuery();

                    sql = "CREATE TABLE SecondaryNetworkConfig(ID INT, MainNetworkConfigID INT, IpAddress VARCHAR(15), NetMask VARCHAR(15))";
                    sqliteCommand = new SQLiteCommand(sql, sqlConn);
                    sqliteCommand.ExecuteNonQuery();

                    sql = "INSERT INTO DatabaseVersion (Version) VALUES (1)";
                    sqliteCommand = new SQLiteCommand(sql, sqlConn);
                    sqliteCommand.ExecuteNonQuery();

                    usersDBVersion++;
                }

                // if any changes query the databaseversion and do the updates inserts or creates.
            }

            

            sqlConn.Close();              
        }

        public static int DatabaseVersion(string sqlFileName)
        {
            if (!File.Exists(sqlFileName))
            {
                return 0;
            }

            int rtrnVal = 0;
            string sql = String.Empty;
            SQLiteCommand sqliteCommand;
            SQLiteConnection sqlConn =  new SQLiteConnection(String.Format("Data Source={0};Version=3", sqlFileName));
            sqlConn.Open();

            sql = "SELECT Version FROM DatabaseVersion LIMIT 1";
            sqliteCommand = new SQLiteCommand(sql, sqlConn);

            SQLiteDataReader reader;
            try
            {
                reader = sqliteCommand.ExecuteReader();

                reader.Read();
                rtrnVal = (int)reader["Version"];
            }
            catch (SQLiteException)
            {
            }
            sqlConn.Close();

            return rtrnVal;

        }
    }
}
