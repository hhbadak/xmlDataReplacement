namespace xmlDataReplacement
{
    partial class XmlData
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
            this.dgv_list = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.firmalarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMI_xmlTedarik = new System.Windows.Forms.ToolStripMenuItem();
            this.tSS_xmlTedarik = new System.Windows.Forms.ToolStripSeparator();
            this.TSMI_xmlDunyası = new System.Windows.Forms.ToolStripMenuItem();
            this.tSS_xmlDunyası = new System.Windows.Forms.ToolStripSeparator();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_list)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgv_list
            // 
            this.dgv_list.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv_list.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_list.Location = new System.Drawing.Point(0, 46);
            this.dgv_list.Name = "dgv_list";
            this.dgv_list.RowHeadersWidth = 51;
            this.dgv_list.RowTemplate.Height = 24;
            this.dgv_list.Size = new System.Drawing.Size(799, 404);
            this.dgv_list.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.firmalarToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 30);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // firmalarToolStripMenuItem
            // 
            this.firmalarToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSMI_xmlTedarik,
            this.tSS_xmlTedarik,
            this.TSMI_xmlDunyası,
            this.tSS_xmlDunyası});
            this.firmalarToolStripMenuItem.Name = "firmalarToolStripMenuItem";
            this.firmalarToolStripMenuItem.Size = new System.Drawing.Size(77, 26);
            this.firmalarToolStripMenuItem.Text = "Firmalar";
            // 
            // TSMI_xmlTedarik
            // 
            this.TSMI_xmlTedarik.Name = "TSMI_xmlTedarik";
            this.TSMI_xmlTedarik.Size = new System.Drawing.Size(170, 26);
            this.TSMI_xmlTedarik.Text = "XmlTedarik";
            this.TSMI_xmlTedarik.Click += new System.EventHandler(this.TSMI_xmlTedarik_Click);
            // 
            // tSS_xmlTedarik
            // 
            this.tSS_xmlTedarik.Name = "tSS_xmlTedarik";
            this.tSS_xmlTedarik.Size = new System.Drawing.Size(167, 6);
            // 
            // TSMI_xmlDunyası
            // 
            this.TSMI_xmlDunyası.Name = "TSMI_xmlDunyası";
            this.TSMI_xmlDunyası.Size = new System.Drawing.Size(170, 26);
            this.TSMI_xmlDunyası.Text = "XmlDünyası";
            // 
            // tSS_xmlDunyası
            // 
            this.tSS_xmlDunyası.Name = "tSS_xmlDunyası";
            this.tSS_xmlDunyası.Size = new System.Drawing.Size(167, 6);
            // 
            // XmlData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dgv_list);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "XmlData";
            this.Text = "XmlData";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.dgv_list)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_list;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem firmalarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem TSMI_xmlTedarik;
        private System.Windows.Forms.ToolStripSeparator tSS_xmlTedarik;
        private System.Windows.Forms.ToolStripMenuItem TSMI_xmlDunyası;
        private System.Windows.Forms.ToolStripSeparator tSS_xmlDunyası;
    }
}