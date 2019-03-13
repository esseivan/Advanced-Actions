namespace Advanced_Actions_Process
{
    partial class frmAction
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAction));
            this._Notification = new System.Windows.Forms.NotifyIcon(this.components);
            this._NotifMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ouvrirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exécuterLactionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.prochaineActionÀToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.quitterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._NotifMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // _Notification
            // 
            this._Notification.ContextMenuStrip = this._NotifMenu;
            this._Notification.Icon = ((System.Drawing.Icon)(resources.GetObject("_Notification.Icon")));
            this._Notification.Text = "Advanced Actions";
            this._Notification.Visible = true;
            // 
            // _NotifMenu
            // 
            this._NotifMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ouvrirToolStripMenuItem,
            this.toolStripSeparator2,
            this.exécuterLactionToolStripMenuItem,
            this.prochaineActionÀToolStripMenuItem,
            this.hToolStripMenuItem,
            this.toolStripSeparator1,
            this.quitterToolStripMenuItem});
            this._NotifMenu.Name = "_NotifMenu";
            this._NotifMenu.Size = new System.Drawing.Size(198, 126);
            // 
            // ouvrirToolStripMenuItem
            // 
            this.ouvrirToolStripMenuItem.Name = "ouvrirToolStripMenuItem";
            this.ouvrirToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.ouvrirToolStripMenuItem.Text = "Ouvrir";
            this.ouvrirToolStripMenuItem.Click += new System.EventHandler(this.ouvrirToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(194, 6);
            // 
            // exécuterLactionToolStripMenuItem
            // 
            this.exécuterLactionToolStripMenuItem.Name = "exécuterLactionToolStripMenuItem";
            this.exécuterLactionToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.exécuterLactionToolStripMenuItem.Text = "Exécuter l\'action";
            this.exécuterLactionToolStripMenuItem.Click += new System.EventHandler(this.exécuterLactionToolStripMenuItem_Click);
            // 
            // prochaineActionÀToolStripMenuItem
            // 
            this.prochaineActionÀToolStripMenuItem.Enabled = false;
            this.prochaineActionÀToolStripMenuItem.Name = "prochaineActionÀToolStripMenuItem";
            this.prochaineActionÀToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.prochaineActionÀToolStripMenuItem.Text = "Prochaine action dans :";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(194, 6);
            // 
            // quitterToolStripMenuItem
            // 
            this.quitterToolStripMenuItem.Name = "quitterToolStripMenuItem";
            this.quitterToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.quitterToolStripMenuItem.Text = "Quitter";
            this.quitterToolStripMenuItem.Click += new System.EventHandler(this.quitterToolStripMenuItem_Click);
            // 
            // hToolStripMenuItem
            // 
            this.hToolStripMenuItem.Enabled = false;
            this.hToolStripMenuItem.Name = "hToolStripMenuItem";
            this.hToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.hToolStripMenuItem.Text = "0 H";
            // 
            // frmAction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmAction";
            this.ShowIcon = false;
            this.Text = "frmAction";
            this._NotifMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon _Notification;
        private System.Windows.Forms.ContextMenuStrip _NotifMenu;
        private System.Windows.Forms.ToolStripMenuItem ouvrirToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem exécuterLactionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem prochaineActionÀToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem quitterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hToolStripMenuItem;
    }
}