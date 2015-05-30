﻿using System;
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
    public partial class ConfigurationForm : Form
    {
        private bool IsNewConfig { get; set; }
        private int ConfigID { get; set; }
        private MainNetworkConfigItem MainConfigItem { get; set; }

        public ConfigurationForm()
        {
            InitializeComponent();

            IsNewConfig = true;

            this.Text = "New Configuration";
        }

        public ConfigurationForm(bool isNewConfiguration, int configID)
        {
            InitializeComponent();

            this.IsNewConfig = isNewConfiguration;
            this.ConfigID = configID;

            if (IsNewConfig)
            {
                this.Text = "New Configuration";
            }
            else
            {
                this.Text = "Edit Configuration";
            }
        }

        private void configurationForm_Load(object sender, EventArgs e)
        {
            SQLiteTools sqliteTools = new SQLiteTools(Program.SQLiteConn);
            MainConfigItem = new MainNetworkConfigItem();

            if (IsNewConfig)
            {
                MainConfigItem.setID(sqliteTools.GetNextMainNetwokID());

                ipAddr1TextBox.Text = String.Empty;
                ipAddr2TextBox.Text = String.Empty;
                ipAddr3TextBox.Text = String.Empty;
                ipAddr4TextBox.Text = String.Empty;

                netmask1TextBox.Text = String.Empty;
                netmask2TextBox.Text = String.Empty;
                netmask3TextBox.Text = String.Empty;
                netmask4TextBox.Text = String.Empty;

                defaultGateway1TextBox.Text = String.Empty;
                defaultGateway2TextBox.Text = String.Empty;
                defaultGateway3TextBox.Text = String.Empty;
                defaultGateway4TextBox.Text = String.Empty;

                primaryDns1TextBox.Text = String.Empty;
                primaryDns2TextBox.Text = String.Empty;
                primaryDns3TextBox.Text = String.Empty;
                primaryDns4TextBox.Text = String.Empty;

                secondaryDns1TextBox.Text = String.Empty;
                secondaryDns2TextBox.Text = String.Empty;
                secondaryDns3TextBox.Text = String.Empty;
                secondaryDns4TextBox.Text = String.Empty;
            }
            else
            {
                string sql = String.Format("SELECT * FROM MainNetworkConfig WHERE ID = {0} LIMIT 1", ConfigID);
                SQLiteCommand sqliteCmd = new SQLiteCommand(sql, Program.SQLiteConn);
                SQLiteDataReader reader = sqliteCmd.ExecuteReader();

                try
                {
                    reader.Read();

                    MainConfigItem.setID((int)reader["ID"]);
                    MainConfigItem.setConnectionName((string)reader["ConnectionName"]);
                    MainConfigItem.setIPAddress((string)reader["IpAddr"]);
                    MainConfigItem.setNetMask((string)reader["NetMask"]);
                    MainConfigItem.setDefaultGateway((string)reader["DefaultGateWay"]);
                    MainConfigItem.setPrimaryDNS((string)reader["PrimaryDNS"]);
                    MainConfigItem.setSecondaryDNS((string)reader["SecondaryDNS"]);
                }
                catch (InvalidOperationException)
                {                    
                }

                configurationNameTextBox.Text = MainConfigItem.ConnectionName;

                string[] ipAddrArr = MainConfigItem.IPAddress.Split();
                ipAddr1TextBox.Text = ipAddrArr[0];
                ipAddr2TextBox.Text = ipAddrArr[1];
                ipAddr3TextBox.Text = ipAddrArr[2];
                ipAddr4TextBox.Text = ipAddrArr[3];

                string[] netmaskArr = MainConfigItem.NetMask.Split();
                netmask1TextBox.Text = netmaskArr[0];
                netmask2TextBox.Text = netmaskArr[1];
                netmask3TextBox.Text = netmaskArr[2];
                netmask4TextBox.Text = netmaskArr[3];

                string[] defgateArr = MainConfigItem.DefaultGateway.Split();
                defaultGateway1TextBox.Text = defgateArr[0];
                defaultGateway2TextBox.Text = defgateArr[1];
                defaultGateway3TextBox.Text = defgateArr[2];
                defaultGateway4TextBox.Text = defgateArr[3];

                string[] pridnsArr = MainConfigItem.DefaultGateway.Split();
                primaryDns1TextBox.Text = pridnsArr[0];
                primaryDns2TextBox.Text = pridnsArr[1];
                primaryDns3TextBox.Text = pridnsArr[2];
                primaryDns4TextBox.Text = pridnsArr[3];

                string[] secdnsArr = MainConfigItem.SecondaryDNS.Split();
                secondaryDns1TextBox.Text = secdnsArr[0];
                secondaryDns2TextBox.Text = secdnsArr[1];
                secondaryDns3TextBox.Text = secdnsArr[2];
                secondaryDns4TextBox.Text = secdnsArr[3];

            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            string sql = String.Empty;

            string ipAddr = String.Format("{0}.{1}.{2}.{3}", ipAddr1TextBox.Text, ipAddr2TextBox.Text, ipAddr3TextBox.Text, ipAddr4TextBox.Text);
            string netMask = String.Format("{0}.{1}.{2}.{3}", netmask1TextBox.Text, netmask2TextBox.Text, netmask3TextBox.Text, netmask4TextBox.Text);
            string defaultGateway = String.Format("{0}.{1}.{2}.{3}", defaultGateway1TextBox.Text, defaultGateway2TextBox.Text, defaultGateway3TextBox.Text, defaultGateway4TextBox.Text);
            string primaryDns = String.Format("{0}.{1}.{2}.{3}", primaryDns1TextBox.Text, primaryDns2TextBox.Text, primaryDns3TextBox.Text, primaryDns4TextBox.Text);
            string secondaryDns = String.Format("{0}.{1}.{2}.{3}", secondaryDns1TextBox.Text, secondaryDns2TextBox.Text, secondaryDns3TextBox.Text, secondaryDns4TextBox.Text);

            if (IsNewConfig)
            {
                sql = "INSERT INTO MainNetworkConfig (ID, ConnectionName, IpAddress, NetMask, DefaultGateway, PrimaryDNS, SecondaryDNS) VALUES (@id, @conname, @ipaddr, @netmask, @defgateway, @primarydns, @secondarydns)";

                SQLiteCommand sqlcmd = new SQLiteCommand(sql, Program.SQLiteConn);
                sqlcmd.Parameters.Add(new SQLiteParameter("@id", DbType.Int32));
                sqlcmd.Parameters.Add(new SQLiteParameter("@conname", DbType.String));
                sqlcmd.Parameters.Add(new SQLiteParameter("@ipaddr", DbType.String));
                sqlcmd.Parameters.Add(new SQLiteParameter("@netmask", DbType.String));
                sqlcmd.Parameters.Add(new SQLiteParameter("@defgateway", DbType.String));
                sqlcmd.Parameters.Add(new SQLiteParameter("@primarydns", DbType.String));
                sqlcmd.Parameters.Add(new SQLiteParameter("@csecondarydns", DbType.String));
                
                
                sqlcmd.Parameters.AddWithValue("@id", MainConfigItem.ID);
                sqlcmd.Parameters.AddWithValue("@conname", MainConfigItem.ConnectionName);
                sqlcmd.Parameters.AddWithValue("@ipaddr", MainConfigItem.IPAddress);
                sqlcmd.Parameters.AddWithValue("@netmask", MainConfigItem.NetMask);
                sqlcmd.Parameters.AddWithValue("@defgateway", MainConfigItem.DefaultGateway);
                sqlcmd.Parameters.AddWithValue("@primarydns", MainConfigItem.PrimaryDNS);
                sqlcmd.Parameters.AddWithValue("@secondarydns", MainConfigItem.SecondaryDNS);
                
 
                int count = sqlcmd.ExecuteNonQuery();

                if (count > 0)
                {
                    MessageBox.Show("Successful");
                }
            }
            else
            {
                sql = String.Format("UPDATE MainNetworkConfig SET ConnectionName = '{0}', IpAddress = '{1}', NetMask = '{2}', DefaultGateway = '{3}', PrimaryDNS = '{4}', SecondaryDNS = '{5}'",
                        MainConfigItem.ConnectionName, MainConfigItem.IPAddress, MainConfigItem.NetMask, MainConfigItem.DefaultGateway, MainConfigItem.PrimaryDNS, MainConfigItem.SecondaryDNS);
            }

        }
    }
}