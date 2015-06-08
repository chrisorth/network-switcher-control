namespace network_switcher_control
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.networkNotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.networkContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addNewMainConfigButton = new System.Windows.Forms.Button();
            this.editMainConfigurationButton = new System.Windows.Forms.Button();
            this.addSecondaryConfigButton = new System.Windows.Forms.Button();
            this.networkContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // networkNotifyIcon
            // 
            this.networkNotifyIcon.ContextMenuStrip = this.networkContextMenu;
            this.networkNotifyIcon.Text = "notifyIcon1";
            this.networkNotifyIcon.Visible = true;
            this.networkNotifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.networkNotifyIcon_MouseClick);
            this.networkNotifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.networkNotifyIcon_MouseDoubleClick);
            // 
            // networkContextMenu
            // 
            this.networkContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.networkContextMenu.Name = "networkContextMenu";
            this.networkContextMenu.Size = new System.Drawing.Size(93, 26);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // addNewMainConfigButton
            // 
            this.addNewMainConfigButton.Location = new System.Drawing.Point(12, 12);
            this.addNewMainConfigButton.Name = "addNewMainConfigButton";
            this.addNewMainConfigButton.Size = new System.Drawing.Size(175, 23);
            this.addNewMainConfigButton.TabIndex = 1;
            this.addNewMainConfigButton.Text = "Add New Main Configuration";
            this.addNewMainConfigButton.UseVisualStyleBackColor = true;
            this.addNewMainConfigButton.Click += new System.EventHandler(this.addNewMainConfigButton_Click);
            // 
            // editMainConfigurationButton
            // 
            this.editMainConfigurationButton.Location = new System.Drawing.Point(12, 41);
            this.editMainConfigurationButton.Name = "editMainConfigurationButton";
            this.editMainConfigurationButton.Size = new System.Drawing.Size(175, 23);
            this.editMainConfigurationButton.TabIndex = 2;
            this.editMainConfigurationButton.Text = "Edit a Main Configuration";
            this.editMainConfigurationButton.UseVisualStyleBackColor = true;
            this.editMainConfigurationButton.Click += new System.EventHandler(this.editMainConfigurationButton_Click);
            // 
            // addSecondaryConfigButton
            // 
            this.addSecondaryConfigButton.Location = new System.Drawing.Point(12, 70);
            this.addSecondaryConfigButton.Name = "addSecondaryConfigButton";
            this.addSecondaryConfigButton.Size = new System.Drawing.Size(175, 23);
            this.addSecondaryConfigButton.TabIndex = 3;
            this.addSecondaryConfigButton.Text = "Add Secondary Config";
            this.addSecondaryConfigButton.UseVisualStyleBackColor = true;
            this.addSecondaryConfigButton.Click += new System.EventHandler(this.addSecondaryConfigButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(199, 262);
            this.Controls.Add(this.addSecondaryConfigButton);
            this.Controls.Add(this.editMainConfigurationButton);
            this.Controls.Add(this.addNewMainConfigButton);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.networkContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon networkNotifyIcon;
        private System.Windows.Forms.ContextMenuStrip networkContextMenu;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Button addNewMainConfigButton;
        private System.Windows.Forms.Button editMainConfigurationButton;
        private System.Windows.Forms.Button addSecondaryConfigButton;
    }
}

