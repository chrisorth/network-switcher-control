namespace network_switcher_control
{
    partial class ConfigurationSelectorForm
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
            this.configurationDataGridView = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.configurationDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // configurationDataGridView
            // 
            this.configurationDataGridView.AllowUserToAddRows = false;
            this.configurationDataGridView.AllowUserToDeleteRows = false;
            this.configurationDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.configurationDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.configurationDataGridView.Location = new System.Drawing.Point(12, 12);
            this.configurationDataGridView.Name = "configurationDataGridView";
            this.configurationDataGridView.ReadOnly = true;
            this.configurationDataGridView.Size = new System.Drawing.Size(697, 238);
            this.configurationDataGridView.TabIndex = 0;
            this.configurationDataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.configurationDataGridView_CellDoubleClick);
            // 
            // ConfigurationSelectorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(721, 262);
            this.Controls.Add(this.configurationDataGridView);
            this.Name = "ConfigurationSelectorForm";
            this.Text = "ConfigurationSelectorForm";
            this.Load += new System.EventHandler(this.ConfigurationSelectorForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.configurationDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView configurationDataGridView;
    }
}