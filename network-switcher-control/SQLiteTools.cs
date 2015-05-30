using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.SQLite;

namespace network_switcher_control
{
    public class SQLiteTools
    {
        private SQLiteConnection SQLiteConnection { get; set; }

        public SQLiteTools(SQLiteConnection sqlConn)
        {
            SQLiteConnection = sqlConn;
        }

        public int GetNextMainNetwokID()
        {
            int rtrnVal;

            SQLiteCommand sqlCmd;
            string sql = "SELECT ID FROM MainNetworkConfig ORDER BY ID DESC LIMIT 1";

            try
            {
                sqlCmd = new SQLiteCommand(sql, SQLiteConnection);
                SQLiteDataReader reader = sqlCmd.ExecuteReader();

                reader.Read();
                rtrnVal = (int)reader["ID"] + 1;
            }
            catch(InvalidOperationException)
            {
                rtrnVal = 1;
            }
            return rtrnVal;
        }
    }
}
