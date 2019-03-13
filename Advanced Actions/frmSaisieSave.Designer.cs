namespace Advanced_Actions
{
    partial class frmSaisieSave
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
            this.layout = new System.Windows.Forms.TableLayoutPanel();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnAnnuler = new System.Windows.Forms.Button();
            this.txtValeur = new System.Windows.Forms.TextBox();
            this.lblDonnee = new System.Windows.Forms.Label();
            this._boxNewSave = new System.Windows.Forms.CheckBox();
            this.layout.SuspendLayout();
            this.SuspendLayout();
            // 
            // layout
            // 
            this.layout.ColumnCount = 6;
            this.layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layout.Controls.Add(this.btnOK, 3, 1);
            this.layout.Controls.Add(this.btnAnnuler, 3, 2);
            this.layout.Controls.Add(this.txtValeur, 2, 3);
            this.layout.Controls.Add(this.lblDonnee, 1, 1);
            this.layout.Controls.Add(this._boxNewSave, 2, 2);
            this.layout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layout.Location = new System.Drawing.Point(0, 0);
            this.layout.MinimumSize = new System.Drawing.Size(280, 128);
            this.layout.Name = "layout";
            this.layout.RowCount = 6;
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layout.Size = new System.Drawing.Size(320, 128);
            this.layout.TabIndex = 1;
            // 
            // btnOK
            // 
            this.btnOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnOK.Location = new System.Drawing.Point(223, 23);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(74, 24);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnAnnuler
            // 
            this.btnAnnuler.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnAnnuler.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAnnuler.Location = new System.Drawing.Point(223, 53);
            this.btnAnnuler.Name = "btnAnnuler";
            this.btnAnnuler.Size = new System.Drawing.Size(74, 24);
            this.btnAnnuler.TabIndex = 3;
            this.btnAnnuler.Text = "Annuler";
            this.btnAnnuler.UseVisualStyleBackColor = true;
            this.btnAnnuler.Click += new System.EventHandler(this.btnAnnuler_Click);
            // 
            // txtValeur
            // 
            this.layout.SetColumnSpan(this.txtValeur, 2);
            this.txtValeur.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtValeur.Location = new System.Drawing.Point(23, 83);
            this.txtValeur.Name = "txtValeur";
            this.txtValeur.Size = new System.Drawing.Size(274, 20);
            this.txtValeur.TabIndex = 1;
            // 
            // lblDonnee
            // 
            this.lblDonnee.AutoSize = true;
            this.layout.SetColumnSpan(this.lblDonnee, 2);
            this.lblDonnee.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDonnee.Location = new System.Drawing.Point(23, 23);
            this.lblDonnee.Margin = new System.Windows.Forms.Padding(3);
            this.lblDonnee.Name = "lblDonnee";
            this.lblDonnee.Size = new System.Drawing.Size(194, 24);
            this.lblDonnee.TabIndex = 0;
            this.lblDonnee.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _boxNewSave
            // 
            this._boxNewSave.AutoSize = true;
            this._boxNewSave.Location = new System.Drawing.Point(23, 53);
            this._boxNewSave.Margin = new System.Windows.Forms.Padding(3, 3, 23, 3);
            this._boxNewSave.Name = "_boxNewSave";
            this._boxNewSave.Size = new System.Drawing.Size(174, 17);
            this._boxNewSave.TabIndex = 5;
            this._boxNewSave.Text = "Créer une nouvelle sauvegarde";
            this._boxNewSave.UseVisualStyleBackColor = true;
            // 
            // frmSaisieSave
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.CancelButton = this.btnAnnuler;
            this.ClientSize = new System.Drawing.Size(320, 128);
            this.ControlBox = false;
            this.Controls.Add(this.layout);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(340, 171);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(340, 171);
            this.Name = "frmSaisieSave";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Saisie";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.layout.ResumeLayout(false);
            this.layout.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel layout;
        private System.Windows.Forms.Label lblDonnee;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnAnnuler;
        private System.Windows.Forms.TextBox txtValeur;
        private System.Windows.Forms.CheckBox _boxNewSave;
    }
}
