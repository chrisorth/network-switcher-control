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
    public partial class ConfigurationSelectorForm : Form
    {
        private ConfigSelectorMode SelectorMode { get; set; }
        public int MainConfigurationSelectedID { get; private set; }
        public int SecondaryConfigurationSelectedID { get; private set; }

        public ConfigurationSelectorForm(ConfigSelectorMode csm)
        {
            InitializeComponent();

            SelectorMode = csm;
            MainConfigurationSelectedID = -1;
            SecondaryConfigurationSelectedID = -1;
        }

        public ConfigurationSelectorForm(ConfigSelectorMode csm, int mainID)
        {
            InitializeComponent();

            SelectorMode = csm;
            MainConfigurationSelectedID = mainID;
            SecondaryConfigurationSelectedID = -1;
        }

        public enum ConfigSelectorMode
        {            
            SelectPrimary,
            EditPrimary,
            SelectSecondary,
            EditSecondary
        }

        private void ConfigurationSelectorForm_Load(object sender, EventArgs e)
        {
            //Let's load the data that we're looking for into the datagridview so that we can do what we want with it.
            string sql = String.Empty;

            if (SelectorMode == ConfigSelectorMode.SelectPrimary ||
                SelectorMode == ConfigSelectorMode.EditPrimary)
            {
                sql = "SELECT * FROM MainNetworkConfig";
            }
            else if (SelectorMode == ConfigSelectorMode.SelectSecondary)   
            {
                sql = "SELECT * FROM SecondaryNetworkConfig";
            }
            else if (SelectorMode == ConfigSelectorMode.EditSecondary)
            {
                sql = "SELECT * FROM SecondaryNetworkConfig WHERE MainNetworkConfigID=@MainNetworkConfigID";
            }

            using (SQLiteConnection sqlconn = new SQLiteConnection(Program.SQLiteConnectionString))
            using (SQLiteCommand sqlcmd = new SQLiteCommand(sql, sqlconn))
            {
                sqlconn.Open();

                if (SelectorMode == ConfigSelectorMode.EditSecondary)
                {
                    sqlcmd.Parameters.Add(new SQLiteParameter("@MainNetworkConfigID", DbType.Int32));
                    sqlcmd.Parameters["@MainNetworkConfigID"].Value = MainConfigurationSelectedID;
                }

                try
                {
                    using (SQLiteDataReader reader = sqlcmd.ExecuteReader())
                    {                        
                        //Get names to make columns...
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            string colname = reader.GetName(i);
                            configurationDataGridView.Columns.Add(colname, colname);
                        }

                        //fill in columns
                        while (reader.Read())
                        {
                            int newRow = configurationDataGridView.Rows.Add();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                object obj = reader[i];

                                if (obj.GetType() == typeof(Int32))
                                {
                                    configurationDataGridView.Rows[newRow].Cells[i].ValueType = typeof(int);
                                    configurationDataGridView.Rows[newRow].Cells[i].Value = (int)obj;
                                }
                                else if (obj.GetType() == typeof(String))
                                {
                                    configurationDataGridView.Rows[newRow].Cells[i].ValueType = typeof(string);
                                    configurationDataGridView.Rows[newRow].Cells[i].Value = (string)obj;
                                }
                            }
                        }
                        //rtrnVal = (int)reader["ID"] + 1;
                    }
                }
                catch (InvalidOperationException)
                {
                    //rtrnVal = 1;
                }
            }
        }

        private void configurationDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            int configurationIDSelected = -1;
            int secondaryConfigurationIDSelected = -1;

            if (this.SelectorMode == ConfigSelectorMode.EditPrimary)
            {
                configurationIDSelected = (int)dgv[0, e.RowIndex].Value;
                ConfigurationForm cf = new ConfigurationForm(false, configurationIDSelected);
                cf.ShowDialog();
            }
            else if (this.SelectorMode == ConfigSelectorMode.SelectPrimary)
            {
                MainConfigurationSelectedID = (int)dgv[0, e.RowIndex].Value;
                this.Close();
            }
            else if (this.SelectorMode == ConfigSelectorMode.EditSecondary)
            {
                SecondaryConfigurationSelectedID = (int)dgv[0, e.RowIndex].Value;
                MainConfigurationSelectedID = (int)dgv[1, e.RowIndex].Value;

                SecondaryConfigForm scf = new SecondaryConfigForm(this.MainConfigurationSelectedID, this.SecondaryConfigurationSelectedID);
                scf.ShowDialog();
            }
            else if (this.SelectorMode == ConfigSelectorMode.SelectSecondary)
            {
                SecondaryConfigurationSelectedID = (int)dgv[0, e.RowIndex].Value;
                MainConfigurationSelectedID = (int)dgv[1, e.RowIndex].Value;

                this.Close();
            }

        }
    }
}
