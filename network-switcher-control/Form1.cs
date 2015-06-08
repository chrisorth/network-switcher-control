using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace network_switcher_control
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            networkNotifyIcon.Icon = new Icon(SystemIcons.Application, 40, 40);
            networkNotifyIcon.Text = "Network Switcher Control";
        }

        #region MainForm Handlers

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.Visible = false;
            this.ShowInTaskbar = false;
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                this.Visible = false;
                e.Cancel = true;
            }
        }

        #endregion

        #region networkNotifyIcon Handlers

        private void networkNotifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                networkContextMenu.Show();
            }
        }

        private void networkNotifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Visible = true;
        }

        #endregion

        #region exitToolStripMenuItem Handlers

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        #endregion

        private void addNewMainConfigButton_Click(object sender, EventArgs e)
        {
            ConfigurationForm cfgForm = new ConfigurationForm();
            cfgForm.ShowDialog();
        }

        private void editMainConfigurationButton_Click(object sender, EventArgs e)
        {
            ConfigurationSelectorForm csf = new ConfigurationSelectorForm(ConfigurationSelectorForm.ConfigSelectorMode.EditPrimary);
            csf.ShowDialog();
        }

        private void addSecondaryConfigButton_Click(object sender, EventArgs e)
        {
            ConfigurationSelectorForm csf = new ConfigurationSelectorForm(ConfigurationSelectorForm.ConfigSelectorMode.SelectPrimary);
            csf.ShowDialog();

            SecondaryConfigForm scf = new SecondaryConfigForm(csf.MainConfigurationSelectedID);
            scf.ShowDialog();  
        }
    }
}
