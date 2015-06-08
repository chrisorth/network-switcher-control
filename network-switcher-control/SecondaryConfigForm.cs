using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Data.SQLite;

namespace network_switcher_control
{
    public partial class SecondaryConfigForm : Form
    {
        private int MainID { get; set; }
        private int SecondaryID {get; set;}

        public SecondaryConfigForm(int mainID)
        {
            InitializeComponent();

            MainID = mainID;
            SecondaryID = 0;
        }

        public SecondaryConfigForm(int mainID, int secondaryID)
        {
            InitializeComponent();

            MainID = mainID;
            SecondaryID = secondaryID;
        }

        private void SecondaryConfigForm_Load(object sender, EventArgs e)
        {

            ipAddr1TextBox.Text = String.Empty;
            ipAddr2TextBox.Text = String.Empty;
            ipAddr3TextBox.Text = String.Empty;
            ipAddr4TextBox.Text = String.Empty;

            netmask1TextBox.Text = String.Empty;
            netmask2TextBox.Text = String.Empty;
            netmask3TextBox.Text = String.Empty;
            netmask4TextBox.Text = String.Empty;

            if (SecondaryID == 0)
            {
                SQLiteTools slt = new SQLiteTools();

                int newSecondaryID = slt.GetNextSecondaryNetworkID();

                string sql = "SELECT * FROM MainNetworkConfig WHERE ID=@ID LIMIT 1";
                using (SQLiteConnection sqlconn = new SQLiteConnection(Program.SQLiteConnectionString))
                {
                    sqlconn.Open();

                    using (SQLiteCommand sqlcmd = new SQLiteCommand(sql, sqlconn))
                    {
                        sqlcmd.Parameters.Add(new SQLiteParameter("@ID", DbType.Int32));
                        sqlcmd.Parameters["@ID"].Value = MainID;

                        using (SQLiteDataReader reader = sqlcmd.ExecuteReader())
                        {
                            reader.Read();

                            configurationNameTextBox.Text = (string)reader["ConnectionName"];
                        }
                    }
                }
            }
            else
            {
                string sql = "SELECT * FROM SecondaryNetworkConfig WHERE ID=@ID LIMIT 1";
                using (SQLiteConnection sqlconn = new SQLiteConnection(Program.SQLiteConnectionString))
                {
                    sqlconn.Open();

                    using (SQLiteCommand sqlcmd = new SQLiteCommand(sql, sqlconn))
                    {
                        sqlcmd.Parameters.Add(new SQLiteParameter("@ID", DbType.Int32));
                        sqlcmd.Parameters["@ID"].Value = SecondaryID;

                        using (SQLiteDataReader reader = sqlcmd.ExecuteReader())
                        {
                            reader.Read();

                            string[] ipArr = ((string)reader["IpAddress"]).Split('.');
                            ipAddr1TextBox.Text = ipArr[0];
                            ipAddr2TextBox.Text = ipArr[1];
                            ipAddr3TextBox.Text = ipArr[2];
                            ipAddr4TextBox.Text = ipArr[3];

                            string[] nmArr = ((string)reader["NetMask"]).Split('.');
                            netmask1TextBox.Text = nmArr[0];
                            netmask2TextBox.Text = nmArr[1];
                            netmask3TextBox.Text = nmArr[2];
                            netmask4TextBox.Text = nmArr[3];
                        }
                    }
                }
            }
        }


    }
}
