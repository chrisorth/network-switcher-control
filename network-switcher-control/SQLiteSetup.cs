using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;

using System.Data.SQLite;

namespace network_switcher_control
{
    class SQLiteSetup
    {
        public static readonly int CurrentDatabaseVersion = 2;

        public static void RunDBSetup(string sqlFileName)
        {
            string sql = String.Empty;

            using(SQLiteConnection sqlconn = new SQLiteConnection(Program.SQLiteConnectionString))
            using (SQLiteCommand sqliteCommand = new SQLiteCommand(sqlconn))
            {
                sqlconn.Open();

                if (!File.Exists(sqlFileName))
                {                    
                    SQLiteConnection.CreateFile(sqlFileName);
                }
                

                int usersDBVersion = DatabaseVersion(sqlFileName);

                if (File.Exists(sqlFileName))
                {
                    if (usersDBVersion < 1)
                    {
                        //setup the database after a fresh install
                        sql = "CREATE TABLE DatabaseVersion (Version INT)";
                        sqliteCommand.CommandText = sql;
                        sqliteCommand.ExecuteNonQuery();

                        sql = "CREATE TABLE MainNetworkConfig(ID INT, ConnectionName VARCHAR(255), IpAddress VARCHAR(15), NetMask VARCHAR(15), DefaultGateway VARCHAR(15), PrimaryDNS VARCHAR(15), SecondaryDNS VARCHAR(15))";
                        sqliteCommand.CommandText = sql;
                        sqliteCommand.ExecuteNonQuery();

                        sql = "CREATE TABLE SecondaryNetworkConfig(ID INT, MainNetworkConfigID INT, IpAddress VARCHAR(15), NetMask VARCHAR(15))";
                        sqliteCommand.CommandText = sql;
                        sqliteCommand.ExecuteNonQuery();

                        sql = "INSERT INTO DatabaseVersion (Version) VALUES (1)";
                        sqliteCommand.CommandText = sql;
                        sqliteCommand.ExecuteNonQuery();

                        usersDBVersion = 1;
                    }

                    // if any changes query the databaseversion and do the updates inserts or creates.
                    if (usersDBVersion < 2)
                    {
                        // add a sort by column number in the MainNetworkConfig then update the DatabaseVersion
                        sql = "ALTER TABLE MainNetworkConfig ADD COLUMN MenuPosition INT";
                        sqliteCommand.CommandText = sql;
                        sqliteCommand.ExecuteNonQuery();

                        sql = "UPDATE DatabaseVersion SET Version=2 WHERE Version=1";
                        sqliteCommand.CommandText = sql;
                        sqliteCommand.ExecuteNonQuery();

                        usersDBVersion = 2;
                    }
                }
            }
        }

        public static int DatabaseVersion(string sqlFileName)
        {
            if (!File.Exists(sqlFileName))
            {
                return 0;
            }

            int rtrnVal = 0;
            string sql = "SELECT Version FROM DatabaseVersion LIMIT 1";

            using (SQLiteConnection sqlConn = new SQLiteConnection(Program.SQLiteConnectionString))
            using (SQLiteCommand sqliteCommand = new SQLiteCommand(sql, sqlConn))
            {
                sqlConn.Open();

                try
                {
                    using (SQLiteDataReader reader = sqliteCommand.ExecuteReader())
                    {
                        reader.Read();
                        rtrnVal = (int)reader["Version"];                        
                    }
                }
                catch (SQLiteException)
                {
                }
                
            }
            return rtrnVal;

        }
    }
}
