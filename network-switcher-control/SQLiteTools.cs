using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.SQLite;

namespace network_switcher_control
{
    public class SQLiteTools
    {      

        public SQLiteTools()
        {
        }

        public int GetNextMainNetwokID()
        {
            int rtrnVal;
            string sql = "SELECT ID FROM MainNetworkConfig ORDER BY ID DESC LIMIT 1";

            using (SQLiteConnection sqlconn = new SQLiteConnection(Program.SQLiteConnectionString))
            using (SQLiteCommand sqlCmd = new SQLiteCommand(sql, sqlconn))
            {
                sqlconn.Open();
                try
                {
                    using (SQLiteDataReader reader = sqlCmd.ExecuteReader())
                    {
                        reader.Read();
                        rtrnVal = (int)reader["ID"] + 1;
                    }
                }
                catch (InvalidOperationException)
                {
                    rtrnVal = 1;
                }
            }
            return rtrnVal;
        }

        public int GetNextSecondaryNetworkID()
        {
            int rtrnVal;
            string sql = "SELECT ID FROM SecondaryNetworkConfig ORDER BY ID DESC LIMIT 1";

            using(SQLiteConnection sqlconn = new SQLiteConnection(Program.SQLiteConnectionString))
            using (SQLiteCommand sqlcmd = new SQLiteCommand(sql, sqlconn))
            {
                sqlconn.Open();
                try
                {
                    using (SQLiteDataReader reader = sqlcmd.ExecuteReader())
                    {
                        reader.Read();
                        rtrnVal = (int)reader["ID"] + 1;
                    }
                }
                catch (InvalidOperationException)
                {
                    rtrnVal = 1;
                }
            }

            return rtrnVal;
        }
    }
}
