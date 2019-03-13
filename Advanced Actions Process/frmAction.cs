using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Advanced_Actions_Process
{
    public partial class frmAction : Form
    {
        public frmAction()
        {
            InitializeComponent();
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.ShowInTaskbar = false;
            this.Load += new EventHandler(Form1_Load);
        }

        void Form1_Load(object sender, EventArgs e)
        {
            this.Size = new Size(0, 0);
        }

        private void ouvrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form openForm = new Advanced_Actions.frmPrincipal();
        }

        private void exécuterLactionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Advanced_Actions.frmPrincipal.ExecuteAction();
        }

        private void quitterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
