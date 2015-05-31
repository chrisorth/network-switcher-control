using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;

using System.Data.SQLite;

namespace network_switcher_control
{
    static class Program
    {
        public static readonly string SQLITE_DATABASE_NAME = "dbNetworkConfig.sqlite";

        public static string SQLiteConnectionString { get; private set; }
        
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            SQLiteConnectionString = String.Format("Data Source={0};Version=3", SQLITE_DATABASE_NAME);
            
            if (SQLiteSetup.DatabaseVersion(SQLITE_DATABASE_NAME) != SQLiteSetup.CurrentDatabaseVersion)
            {
                SQLiteSetup.RunDBSetup(SQLITE_DATABASE_NAME);
            }      

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());


        }
    }
}
