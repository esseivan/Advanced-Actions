namespace Advanced_Actions
{
    partial class frmLoad
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLoad));
            this._Notification = new System.Windows.Forms.NotifyIcon(this.components);
            this._NotifMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ouvrirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.exécuterLactionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._tsmiActions1 = new System.Windows.Forms.ToolStripMenuItem();
            this._tsmiActions2 = new System.Windows.Forms.ToolStripMenuItem();
            this._tsmiActions3 = new System.Windows.Forms.ToolStripMenuItem();
            this._tsmiActions4 = new System.Windows.Forms.ToolStripMenuItem();
            this._tsmiActions5 = new System.Windows.Forms.ToolStripMenuItem();
            this._tsmiActions6 = new System.Windows.Forms.ToolStripMenuItem();
            this._tsmiActions7 = new System.Windows.Forms.ToolStripMenuItem();
            this._tsmiActions8 = new System.Windows.Forms.ToolStripMenuItem();
            this._tsmiActions9 = new System.Windows.Forms.ToolStripMenuItem();
            this._tsmiActions10 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiActions = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAction1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAction2 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAction3 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAction4 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAction5 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAction6 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAction7 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAction8 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAction10 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAction9 = new System.Windows.Forms.ToolStripMenuItem();
            this.toggleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.NametoolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.TimeLefttoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.leToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DatetoolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.quitterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._tmrTimeLeft = new System.Windows.Forms.Timer(this.components);
            this.TimerCheckDates = new System.Windows.Forms.Timer(this.components);
            this._tmrUpdate = new System.Windows.Forms.Timer(this.components);
            this._NotifMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // _Notification
            // 
            this._Notification.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this._Notification.BalloonTipText = "Ce programme s\'exécute en arrière-plan";
            this._Notification.BalloonTipTitle = "Advanced Actions";
            this._Notification.ContextMenuStrip = this._NotifMenu;
            this._Notification.Icon = ((System.Drawing.Icon)(resources.GetObject("_Notification.Icon")));
            this._Notification.Text = "Advanced Actions";
            this._Notification.Visible = true;
            this._Notification.DoubleClick += new System.EventHandler(this._Notification_DoubleClick);
            // 
            // _NotifMenu
            // 
            this._NotifMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ouvrirToolStripMenuItem,
            this.toolStripSeparator2,
            this.toolStripSeparator5,
            this.exécuterLactionToolStripMenuItem,
            this.tsmiActions,
            this.toggleToolStripMenuItem,
            this.toolStripSeparator8,
            this.toolStripSeparator6,
            this.toolStripMenuItem4,
            this.NametoolStripMenuItem5,
            this.toolStripSeparator7,
            this.toolStripMenuItem1,
            this.TimeLefttoolStripMenuItem,
            this.toolStripSeparator3,
            this.leToolStripMenuItem,
            this.DatetoolStripMenuItem2,
            this.toolStripSeparator4,
            this.toolStripSeparator1,
            this.quitterToolStripMenuItem});
            this._NotifMenu.Name = "_NotifMenu";
            this._NotifMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this._NotifMenu.Size = new System.Drawing.Size(170, 294);
            this._NotifMenu.Closed += new System.Windows.Forms.ToolStripDropDownClosedEventHandler(this._NotifMenu_Closed);
            this._NotifMenu.Closing += new System.Windows.Forms.ToolStripDropDownClosingEventHandler(this._NotifMenu_Closing);
            this._NotifMenu.Opening += new System.ComponentModel.CancelEventHandler(this._NotifMenu_Opening);
            this._NotifMenu.Opened += new System.EventHandler(this._NotifMenu_Opened);
            // 
            // ouvrirToolStripMenuItem
            // 
            this.ouvrirToolStripMenuItem.Name = "ouvrirToolStripMenuItem";
            this.ouvrirToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.ouvrirToolStripMenuItem.Text = "Ouvrir";
            this.ouvrirToolStripMenuItem.Click += new System.EventHandler(this.ouvrirToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(166, 6);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(166, 6);
            // 
            // exécuterLactionToolStripMenuItem
            // 
            this.exécuterLactionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._tsmiActions1,
            this._tsmiActions2,
            this._tsmiActions3,
            this._tsmiActions4,
            this._tsmiActions5,
            this._tsmiActions6,
            this._tsmiActions7,
            this._tsmiActions8,
            this._tsmiActions9,
            this._tsmiActions10});
            this.exécuterLactionToolStripMenuItem.Name = "exécuterLactionToolStripMenuItem";
            this.exécuterLactionToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.exécuterLactionToolStripMenuItem.Text = "Exécuter l\'action";
            // 
            // _tsmiActions1
            // 
            this._tsmiActions1.CheckOnClick = true;
            this._tsmiActions1.Enabled = false;
            this._tsmiActions1.Name = "_tsmiActions1";
            this._tsmiActions1.Size = new System.Drawing.Size(79, 22);
            this._tsmiActions1.Text = "-";
            this._tsmiActions1.CheckedChanged += new System.EventHandler(this._tsmiActions_CheckedChanged);
            // 
            // _tsmiActions2
            // 
            this._tsmiActions2.CheckOnClick = true;
            this._tsmiActions2.Enabled = false;
            this._tsmiActions2.Name = "_tsmiActions2";
            this._tsmiActions2.Size = new System.Drawing.Size(79, 22);
            this._tsmiActions2.Text = "-";
            this._tsmiActions2.CheckedChanged += new System.EventHandler(this._tsmiActions_CheckedChanged);
            // 
            // _tsmiActions3
            // 
            this._tsmiActions3.CheckOnClick = true;
            this._tsmiActions3.Enabled = false;
            this._tsmiActions3.Name = "_tsmiActions3";
            this._tsmiActions3.Size = new System.Drawing.Size(79, 22);
            this._tsmiActions3.Text = "-";
            this._tsmiActions3.CheckedChanged += new System.EventHandler(this._tsmiActions_CheckedChanged);
            // 
            // _tsmiActions4
            // 
            this._tsmiActions4.CheckOnClick = true;
            this._tsmiActions4.Enabled = false;
            this._tsmiActions4.Name = "_tsmiActions4";
            this._tsmiActions4.Size = new System.Drawing.Size(79, 22);
            this._tsmiActions4.Text = "-";
            this._tsmiActions4.CheckedChanged += new System.EventHandler(this._tsmiActions_CheckedChanged);
            // 
            // _tsmiActions5
            // 
            this._tsmiActions5.CheckOnClick = true;
            this._tsmiActions5.Enabled = false;
            this._tsmiActions5.Name = "_tsmiActions5";
            this._tsmiActions5.Size = new System.Drawing.Size(79, 22);
            this._tsmiActions5.Text = "-";
            this._tsmiActions5.CheckedChanged += new System.EventHandler(this._tsmiActions_CheckedChanged);
            // 
            // _tsmiActions6
            // 
            this._tsmiActions6.CheckOnClick = true;
            this._tsmiActions6.Enabled = false;
            this._tsmiActions6.Name = "_tsmiActions6";
            this._tsmiActions6.Size = new System.Drawing.Size(79, 22);
            this._tsmiActions6.Text = "-";
            this._tsmiActions6.CheckedChanged += new System.EventHandler(this._tsmiActions_CheckedChanged);
            // 
            // _tsmiActions7
            // 
            this._tsmiActions7.CheckOnClick = true;
            this._tsmiActions7.Enabled = false;
            this._tsmiActions7.Name = "_tsmiActions7";
            this._tsmiActions7.Size = new System.Drawing.Size(79, 22);
            this._tsmiActions7.Text = "-";
            this._tsmiActions7.CheckedChanged += new System.EventHandler(this._tsmiActions_CheckedChanged);
            // 
            // _tsmiActions8
            // 
            this._tsmiActions8.CheckOnClick = true;
            this._tsmiActions8.Enabled = false;
            this._tsmiActions8.Name = "_tsmiActions8";
            this._tsmiActions8.Size = new System.Drawing.Size(79, 22);
            this._tsmiActions8.Text = "-";
            this._tsmiActions8.CheckedChanged += new System.EventHandler(this._tsmiActions_CheckedChanged);
            // 
            // _tsmiActions9
            // 
            this._tsmiActions9.CheckOnClick = true;
            this._tsmiActions9.Enabled = false;
            this._tsmiActions9.Name = "_tsmiActions9";
            this._tsmiActions9.Size = new System.Drawing.Size(79, 22);
            this._tsmiActions9.Text = "-";
            this._tsmiActions9.CheckedChanged += new System.EventHandler(this._tsmiActions_CheckedChanged);
            // 
            // _tsmiActions10
            // 
            this._tsmiActions10.CheckOnClick = true;
            this._tsmiActions10.Enabled = false;
            this._tsmiActions10.Name = "_tsmiActions10";
            this._tsmiActions10.Size = new System.Drawing.Size(79, 22);
            this._tsmiActions10.Text = "-";
            this._tsmiActions10.CheckedChanged += new System.EventHandler(this._tsmiActions_CheckedChanged);
            // 
            // tsmiActions
            // 
            this.tsmiActions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAction1,
            this.tsmiAction2,
            this.tsmiAction3,
            this.tsmiAction4,
            this.tsmiAction5,
            this.tsmiAction6,
            this.tsmiAction7,
            this.tsmiAction8,
            this.tsmiAction10,
            this.tsmiAction9});
            this.tsmiActions.Name = "tsmiActions";
            this.tsmiActions.Size = new System.Drawing.Size(169, 22);
            this.tsmiActions.Text = "Actions activées";
            this.tsmiActions.DropDownOpened += new System.EventHandler(this.tsmiActions_DropDownOpened);
            // 
            // tsmiAction1
            // 
            this.tsmiAction1.CheckOnClick = true;
            this.tsmiAction1.Enabled = false;
            this.tsmiAction1.Name = "tsmiAction1";
            this.tsmiAction1.Size = new System.Drawing.Size(79, 22);
            this.tsmiAction1.Text = "-";
            this.tsmiAction1.CheckedChanged += new System.EventHandler(this.tsmiAction1_CheckedChanged);
            // 
            // tsmiAction2
            // 
            this.tsmiAction2.CheckOnClick = true;
            this.tsmiAction2.Enabled = false;
            this.tsmiAction2.Name = "tsmiAction2";
            this.tsmiAction2.Size = new System.Drawing.Size(79, 22);
            this.tsmiAction2.Text = "-";
            this.tsmiAction2.CheckedChanged += new System.EventHandler(this.tsmiAction1_CheckedChanged);
            // 
            // tsmiAction3
            // 
            this.tsmiAction3.CheckOnClick = true;
            this.tsmiAction3.Enabled = false;
            this.tsmiAction3.Name = "tsmiAction3";
            this.tsmiAction3.Size = new System.Drawing.Size(79, 22);
            this.tsmiAction3.Text = "-";
            this.tsmiAction3.CheckedChanged += new System.EventHandler(this.tsmiAction1_CheckedChanged);
            // 
            // tsmiAction4
            // 
            this.tsmiAction4.CheckOnClick = true;
            this.tsmiAction4.Enabled = false;
            this.tsmiAction4.Name = "tsmiAction4";
            this.tsmiAction4.Size = new System.Drawing.Size(79, 22);
            this.tsmiAction4.Text = "-";
            this.tsmiAction4.CheckedChanged += new System.EventHandler(this.tsmiAction1_CheckedChanged);
            // 
            // tsmiAction5
            // 
            this.tsmiAction5.CheckOnClick = true;
            this.tsmiAction5.Enabled = false;
            this.tsmiAction5.Name = "tsmiAction5";
            this.tsmiAction5.Size = new System.Drawing.Size(79, 22);
            this.tsmiAction5.Text = "-";
            this.tsmiAction5.CheckedChanged += new System.EventHandler(this.tsmiAction1_CheckedChanged);
            // 
            // tsmiAction6
            // 
            this.tsmiAction6.CheckOnClick = true;
            this.tsmiAction6.Enabled = false;
            this.tsmiAction6.Name = "tsmiAction6";
            this.tsmiAction6.Size = new System.Drawing.Size(79, 22);
            this.tsmiAction6.Text = "-";
            this.tsmiAction6.CheckedChanged += new System.EventHandler(this.tsmiAction1_CheckedChanged);
            // 
            // tsmiAction7
            // 
            this.tsmiAction7.CheckOnClick = true;
            this.tsmiAction7.Enabled = false;
            this.tsmiAction7.Name = "tsmiAction7";
            this.tsmiAction7.Size = new System.Drawing.Size(79, 22);
            this.tsmiAction7.Text = "-";
            this.tsmiAction7.CheckedChanged += new System.EventHandler(this.tsmiAction1_CheckedChanged);
            // 
            // tsmiAction8
            // 
            this.tsmiAction8.CheckOnClick = true;
            this.tsmiAction8.Enabled = false;
            this.tsmiAction8.Name = "tsmiAction8";
            this.tsmiAction8.Size = new System.Drawing.Size(79, 22);
            this.tsmiAction8.Text = "-";
            this.tsmiAction8.CheckedChanged += new System.EventHandler(this.tsmiAction1_CheckedChanged);
            // 
            // tsmiAction10
            // 
            this.tsmiAction10.CheckOnClick = true;
            this.tsmiAction10.Enabled = false;
            this.tsmiAction10.Name = "tsmiAction10";
            this.tsmiAction10.Size = new System.Drawing.Size(79, 22);
            this.tsmiAction10.Text = "-";
            this.tsmiAction10.CheckedChanged += new System.EventHandler(this.tsmiAction1_CheckedChanged);
            // 
            // tsmiAction9
            // 
            this.tsmiAction9.CheckOnClick = true;
            this.tsmiAction9.Enabled = false;
            this.tsmiAction9.Name = "tsmiAction9";
            this.tsmiAction9.Size = new System.Drawing.Size(79, 22);
            this.tsmiAction9.Text = "-";
            this.tsmiAction9.CheckedChanged += new System.EventHandler(this.tsmiAction1_CheckedChanged);
            // 
            // toggleToolStripMenuItem
            // 
            this.toggleToolStripMenuItem.Name = "toggleToolStripMenuItem";
            this.toggleToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.toggleToolStripMenuItem.Text = "Désactiver";
            this.toggleToolStripMenuItem.Click += new System.EventHandler(this.toggleToolStripMenuItem_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(166, 6);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(166, 6);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(169, 22);
            this.toolStripMenuItem4.Text = "Prochaine action :";
            this.toolStripMenuItem4.MouseDown += new System.Windows.Forms.MouseEventHandler(this.prochaineActionÀToolStripMenuItem_MouseDown);
            // 
            // NametoolStripMenuItem5
            // 
            this.NametoolStripMenuItem5.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.NametoolStripMenuItem5.Name = "NametoolStripMenuItem5";
            this.NametoolStripMenuItem5.Size = new System.Drawing.Size(169, 22);
            this.NametoolStripMenuItem5.Text = "-";
            this.NametoolStripMenuItem5.MouseDown += new System.Windows.Forms.MouseEventHandler(this.prochaineActionÀToolStripMenuItem_MouseDown);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(166, 6);
            this.toolStripSeparator7.MouseDown += new System.Windows.Forms.MouseEventHandler(this.prochaineActionÀToolStripMenuItem_MouseDown);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(169, 22);
            this.toolStripMenuItem1.Text = "Dans :";
            this.toolStripMenuItem1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.prochaineActionÀToolStripMenuItem_MouseDown);
            // 
            // TimeLefttoolStripMenuItem
            // 
            this.TimeLefttoolStripMenuItem.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.TimeLefttoolStripMenuItem.Name = "TimeLefttoolStripMenuItem";
            this.TimeLefttoolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.TimeLefttoolStripMenuItem.Text = "0";
            this.TimeLefttoolStripMenuItem.MouseDown += new System.Windows.Forms.MouseEventHandler(this.prochaineActionÀToolStripMenuItem_MouseDown);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(166, 6);
            this.toolStripSeparator3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.prochaineActionÀToolStripMenuItem_MouseDown);
            // 
            // leToolStripMenuItem
            // 
            this.leToolStripMenuItem.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.leToolStripMenuItem.Name = "leToolStripMenuItem";
            this.leToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.leToolStripMenuItem.Text = "Le :";
            this.leToolStripMenuItem.MouseDown += new System.Windows.Forms.MouseEventHandler(this.prochaineActionÀToolStripMenuItem_MouseDown);
            // 
            // DatetoolStripMenuItem2
            // 
            this.DatetoolStripMenuItem2.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.DatetoolStripMenuItem2.Name = "DatetoolStripMenuItem2";
            this.DatetoolStripMenuItem2.Size = new System.Drawing.Size(169, 22);
            this.DatetoolStripMenuItem2.Text = "00.00.0000";
            this.DatetoolStripMenuItem2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.prochaineActionÀToolStripMenuItem_MouseDown);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(166, 6);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(166, 6);
            // 
            // quitterToolStripMenuItem
            // 
            this.quitterToolStripMenuItem.Image = global::Advanced_Actions.Properties.Resources.Cancel;
            this.quitterToolStripMenuItem.Name = "quitterToolStripMenuItem";
            this.quitterToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.quitterToolStripMenuItem.Text = "Quitter";
            this.quitterToolStripMenuItem.Click += new System.EventHandler(this.quitterToolStripMenuItem_Click);
            // 
            // _tmrTimeLeft
            // 
            this._tmrTimeLeft.Interval = 1000;
            this._tmrTimeLeft.Tick += new System.EventHandler(this._tmrTimeLeft_Tick);
            // 
            // TimerCheckDates
            // 
            this.TimerCheckDates.Enabled = true;
            this.TimerCheckDates.Interval = 1000;
            this.TimerCheckDates.Tick += new System.EventHandler(this.TimerCheckDates_Tick);
            // 
            // _tmrUpdate
            // 
            this._tmrUpdate.Enabled = true;
            this._tmrUpdate.Tick += new System.EventHandler(this._tmrUpdate_Tick);
            // 
            // frmLoad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(375, 338);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmLoad";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "frmAction";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmLoad_FormClosing);
            this.Load += new System.EventHandler(this.frmLoad_Load);
            this._NotifMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon _Notification;
        private System.Windows.Forms.ContextMenuStrip _NotifMenu;
        private System.Windows.Forms.ToolStripMenuItem ouvrirToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem exécuterLactionToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem quitterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toggleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem TimeLefttoolStripMenuItem;
        private System.Windows.Forms.Timer _tmrTimeLeft;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem leToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DatetoolStripMenuItem2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem _tsmiActions1;
        private System.Windows.Forms.ToolStripMenuItem _tsmiActions2;
        private System.Windows.Forms.ToolStripMenuItem _tsmiActions3;
        private System.Windows.Forms.ToolStripMenuItem _tsmiActions4;
        private System.Windows.Forms.ToolStripMenuItem _tsmiActions5;
        private System.Windows.Forms.ToolStripMenuItem _tsmiActions6;
        private System.Windows.Forms.ToolStripMenuItem _tsmiActions7;
        private System.Windows.Forms.ToolStripMenuItem _tsmiActions8;
        private System.Windows.Forms.ToolStripMenuItem _tsmiActions9;
        private System.Windows.Forms.ToolStripMenuItem _tsmiActions10;
        private System.Windows.Forms.Timer TimerCheckDates;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem NametoolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem tsmiActions;
        private System.Windows.Forms.ToolStripMenuItem tsmiAction1;
        private System.Windows.Forms.ToolStripMenuItem tsmiAction2;
        private System.Windows.Forms.ToolStripMenuItem tsmiAction3;
        private System.Windows.Forms.ToolStripMenuItem tsmiAction4;
        private System.Windows.Forms.ToolStripMenuItem tsmiAction5;
        private System.Windows.Forms.ToolStripMenuItem tsmiAction6;
        private System.Windows.Forms.ToolStripMenuItem tsmiAction7;
        private System.Windows.Forms.ToolStripMenuItem tsmiAction8;
        private System.Windows.Forms.ToolStripMenuItem tsmiAction10;
        private System.Windows.Forms.ToolStripMenuItem tsmiAction9;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.Timer _tmrUpdate;
    }
}