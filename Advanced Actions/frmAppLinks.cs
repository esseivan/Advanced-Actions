using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Advanced_Actions
{
    public partial class frmAppLinks : Form
    {
        public static ArrayList FilePaths;
        public static ArrayList FileArgs;
        public static ArrayList LastFileArgs;


        public frmAppLinks()
        {
            InitializeComponent();
            
        }

        public void Main(ArrayList Paths, ArrayList Args)
        {
            short Counter = 0;
            ClearPaths();
            if (Paths.Count == 0)
            {
                Label NewLabel = new Label();
                NewLabel.Name = "_lblInfo";
                NewLabel.Text = "Aucun lien trouvé";
                NewLabel.Dock = DockStyle.Fill;
                NewLabel.AutoSize = true;
                NewLabel.Margin = new Padding(3);
                this.tableLayoutPanel2.Controls.Add(NewLabel, 1, 0);
                this.tableLayoutPanel2.RowCount++;
                this.tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
            }
            else
            {
                foreach (string item in Paths)
                {
                    Counter++;

                    Label NewLabel = new Label();
                    NewLabel.Name = string.Format("_lblLink" + (Counter));
                    NewLabel.Text = Counter + " : " + item;
                    NewLabel.Dock = DockStyle.Fill;
                    NewLabel.Margin = new Padding(3);

                    Button NewBtn = new Button();
                    NewBtn.Name = string.Format("_btnLink" + (Counter));
                    NewBtn.Text = string.Format("Argument Lien " + (Counter));
                    NewBtn.Dock = DockStyle.Fill;
                    NewBtn.UseVisualStyleBackColor = true;
                    NewBtn.TabStop = false;
                    NewBtn.Click += NewBtn_Click;

                    Panel NewPanel = new Panel();
                    NewPanel.Name = string.Format("_Led" + (Counter));
                    NewPanel.Dock = DockStyle.Fill;
                    NewPanel.Margin = new Padding(6);

                    this.tableLayoutPanel2.Controls.Add(NewPanel, 0, (Counter - 1));
                    this.tableLayoutPanel2.Controls.Add(NewLabel, 1, (Counter - 1));
                    this.tableLayoutPanel2.Controls.Add(NewBtn, 3, (Counter - 1));
                    this.tableLayoutPanel2.RowCount++;
                    this.tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));

                    _SelDel.Items.Add(Counter);

                }
                this.tableLayoutPanel2.RowCount++;
                this.tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
            }
        }

        private void NewBtn_Click(object sender, EventArgs e)
        {   // Get button number
            string button = ((sender as Button).Name).ToString();
            button = button.Split('k')[1];
            Console.WriteLine("[BUTTON CLICKED] Arg : " + button);
            short Number = (short)(short.Parse(button) - 1);

            // Init
            bool Temp = !(string.IsNullOrEmpty((string)FileArgs[Number]));
            frmSaisie saisie = new frmSaisie();
            saisie.Question("Ajouter un argument ?", "Arguments");
            saisie.LoadAnswer((string)LastFileArgs[Number], Temp);
            saisie.SetPos(true);

            // Copie si OK
            if (saisie.ShowDialog() == DialogResult.OK)
            {
                if (saisie.ONOFF)
                {
                    FileArgs[Number] = saisie.Valeur;
                }
                else
                {
                    FileArgs[Number] = null;
                }
                LastFileArgs[Number] = saisie.Valeur;
            }
        }

        private void _btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmAppLinks_Load(object sender, EventArgs e)
        {
            _btnOK.DialogResult = DialogResult.OK;
            _btnCancel.DialogResult = DialogResult.Cancel;
        }

        private void _SelDel_SelectedIndexChanged(object sender, EventArgs e)
        {
            short Counter = 1;
            Control Temp;
            Temp = this.Controls.Find(("_Led" + (Counter)), true).FirstOrDefault();

            while (Temp != null)
            {
                Temp.BackColor = this.BackColor;
                Counter++;
                Temp = this.Controls.Find(("_Led" + (Counter)), true).FirstOrDefault();
            }

            Controls.Find(("_Led" + (_SelDel.SelectedIndex + 1)),true).FirstOrDefault().BackColor = Color.Lime;            
        }

        private void _btnDel_Click(object sender, EventArgs e)
        {
            if (_SelDel.SelectedIndex >= 0)
            {
                short NbrTemp = (short)_SelDel.SelectedIndex;
                FilePaths.RemoveAt(NbrTemp);
                FileArgs.RemoveAt(NbrTemp);
                LastFileArgs.RemoveAt(NbrTemp);

                Main(FilePaths, FileArgs);
                if (NbrTemp > _SelDel.Items.Count-1)
                    _SelDel.SelectedIndex = (_SelDel.Items.Count-1);
                else
                    _SelDel.SelectedIndex = NbrTemp;
            }
            else
            {
                MessageBox.Show("Aucun chemin sélectionné !", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void _btnAdd_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string[] temp = openFileDialog1.FileNames;
                foreach (string item in temp)
                {
                    FilePaths.Add(item);
                    FileArgs.Add(null);
                    LastFileArgs.Add(null);
                }

                ClearPaths();
                Main(FilePaths, FileArgs);
            }
        }

        private void ClearPaths()
        {
            Control Temp;
            short Counter = 1;

            _SelDel.Items.Clear();

            Temp = this.Controls.Find(("_lblLink" + (Counter)), true).FirstOrDefault();
            while (Temp != null)
            {
                Temp.Dispose();
                Counter++;
                Temp = this.Controls.Find(("_lblLink" + (Counter)), true).FirstOrDefault();
            }

            Counter = 1;

            Temp = this.Controls.Find(("_btnLink" + (Counter)), true).FirstOrDefault();
            while (Temp != null)
            {
                Temp.Dispose();
                Counter++;
                Temp = this.Controls.Find(("_btnLink" + (Counter)), true).FirstOrDefault();
            }

            Counter = 1;

            Temp = this.Controls.Find(("_Led" + (Counter)), true).FirstOrDefault();
            while (Temp != null)
            {
                Temp.Dispose();
                Counter++;
                Temp = this.Controls.Find(("_Led" + (Counter)), true).FirstOrDefault();
            }
        }
    }
}
