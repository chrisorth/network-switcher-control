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

        public ConfigurationSelectorForm(ConfigSelectorMode csm)
        {
            InitializeComponent();

            SelectorMode = csm;
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
            else if (SelectorMode == ConfigSelectorMode.SelectSecondary ||
                SelectorMode == ConfigSelectorMode.EditSecondary)
            {
                sql = "SELECT * FROM SecondaryNetworkConfig";
            }

            using (SQLiteConnection sqlconn = new SQLiteConnection(Program.SQLiteConnectionString))
            using (SQLiteCommand sqlCmd = new SQLiteCommand(sql, sqlconn))
            {
                sqlconn.Open();
                try
                {
                    using (SQLiteDataReader reader = sqlCmd.ExecuteReader())
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
            int configurationIDSelected = (int)dgv[0, e.RowIndex].Value;

            ConfigurationForm cf = new ConfigurationForm(false, configurationIDSelected);
            cf.ShowDialog();
        }
    }
}
