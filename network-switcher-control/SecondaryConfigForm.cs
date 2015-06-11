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
                string sql = "SELECT MainNetworkConfig.ConnectionName, SecondaryNetworkConfig.*";
                sql += " FROM MainNetworkConfig";
                sql += " INNER JOIN SecondaryNetworkConfig";
                sql += " ON MainNetworkConfig.ID = SecondaryNetworkConfig.MainNetworkConfigID";
                sql += " WHERE SecondaryNetworkConfig.ID=@ID LIMIT 1";

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

                            configurationNameTextBox.Text = (string)reader["ConnectionName"];

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


        private void ValidateAddressPart(TextBox textBox)
        {
            int tmpint = Int32.Parse(textBox.Text);

            if (tmpint < 0)
            {
                MessageBox.Show("Address cannot be less than 0.");
                tmpint = 0;
            }
            else if (tmpint > 255)
            {
                MessageBox.Show("Address connot be greater than 255.");
                tmpint = 255;
            }

            textBox.Text = tmpint.ToString();
        }

        private void ipAddr1TextBox_Leave(object sender, EventArgs e)
        {
            ValidateAddressPart((TextBox)sender);
        }

        private void ipAddr2TextBox_Leave(object sender, EventArgs e)
        {
            ValidateAddressPart((TextBox)sender);
        }

        private void ipAddr3TextBox_Leave(object sender, EventArgs e)
        {
            ValidateAddressPart((TextBox)sender);
        }

        private void ipAddr4TextBox_Leave(object sender, EventArgs e)
        {
            ValidateAddressPart((TextBox)sender);
        }

        private void netmask1TextBox_Leave(object sender, EventArgs e)
        {
            ValidateAddressPart((TextBox)sender);
        }

        private void netmask2TextBox_Leave(object sender, EventArgs e)
        {
            ValidateAddressPart((TextBox)sender);
        }

        private void netmask3TextBox_Leave(object sender, EventArgs e)
        {
            ValidateAddressPart((TextBox)sender);
        }

        private void netmask4TextBox_Leave(object sender, EventArgs e)
        {
            ValidateAddressPart((TextBox)sender);
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            string ipstr = String.Format("{0}.{1}.{2}.{3}", ipAddr1TextBox.Text, ipAddr2TextBox.Text, ipAddr3TextBox.Text, ipAddr4TextBox.Text);
            string netmaskstr = String.Format("{0}.{1}.{2}.{3}", netmask1TextBox.Text, netmask2TextBox.Text, netmask3TextBox.Text, netmask4TextBox.Text);

            if (SecondaryID == 0) // means we have a new secondary setup
            {
                SQLiteTools slt = new SQLiteTools();

                int newSecondaryID = slt.GetNextSecondaryNetworkID();

                string sql = "INSERT INTO SecondaryNetworkConfig (ID, MainNetworkConfigID, IpAddress, NetMask) VALUES (@SecondaryID, @MainNetworkConfigID, @IP, @Netmask)";
                using (SQLiteConnection sqlconn = new SQLiteConnection(Program.SQLiteConnectionString))
                {
                    sqlconn.Open();

                    using (SQLiteCommand sqlcmd = new SQLiteCommand(sql, sqlconn))
                    {
                        sqlcmd.Parameters.Add(new SQLiteParameter("@SecondaryID", DbType.Int32));
                        sqlcmd.Parameters.Add(new SQLiteParameter("@MainNetworkConfigID", DbType.Int32));
                        sqlcmd.Parameters.Add(new SQLiteParameter("@IP", DbType.String));
                        sqlcmd.Parameters.Add(new SQLiteParameter("@Netmask", DbType.String));

                        sqlcmd.Parameters["@SecondaryID"].Value = newSecondaryID;
                        sqlcmd.Parameters["@MainNetworkConfigID"].Value = MainID;
                        sqlcmd.Parameters["@IP"].Value = ipstr;
                        sqlcmd.Parameters["@Netmask"].Value = netmaskstr;

                        sqlcmd.ExecuteNonQuery();
                    }
                }
            }
            else //update a secondary setup
            {
                string sql = "UPDATE SecondaryNetworkConfig SET IpAddress=@IP, NetMask=@Netmask WHERE ID=@SecondaryID AND MainNetworkConfigID=@MainNetworkConfigID";
                using (SQLiteConnection sqlconn = new SQLiteConnection(Program.SQLiteConnectionString))
                {
                    sqlconn.Open();

                    using (SQLiteCommand sqlcmd = new SQLiteCommand(sql, sqlconn))
                    {
                        sqlcmd.Parameters.Add(new SQLiteParameter("@SecondaryID", DbType.Int32));
                        sqlcmd.Parameters.Add(new SQLiteParameter("@MainNetworkConfigID", DbType.Int32));
                        sqlcmd.Parameters.Add(new SQLiteParameter("@IP", DbType.String));
                        sqlcmd.Parameters.Add(new SQLiteParameter("@Netmask", DbType.String));

                        sqlcmd.Parameters["@SecondaryID"].Value = SecondaryID;
                        sqlcmd.Parameters["@MainNetworkConfigID"].Value = MainID;
                        sqlcmd.Parameters["@IP"].Value = ipstr;
                        sqlcmd.Parameters["@Netmask"].Value = netmaskstr;

                        sqlcmd.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
