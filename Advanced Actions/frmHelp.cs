using System;
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
    public partial class frmHelp : Form
    {
        public frmHelp()
        {
            InitializeComponent();
        }

        private void _btnPlus_Click(object sender, EventArgs e)
        {
            if (((Txt.Height < Screen.PrimaryScreen.Bounds.Height * 0.95) && (Txt.Width < Screen.PrimaryScreen.Bounds.Width * 0.95)))
                Txt.Font = new Font(FontFamily.GenericSansSerif, Txt.Font.Size + 1);
        }

        private void _btnMinus_Click(object sender, EventArgs e)
        {
            if (Txt.Font.Size > 0.25)
                Txt.Font = new Font(FontFamily.GenericSansSerif, Txt.Font.Size - 1);
        }

        private void frmHelp_Load(object sender, EventArgs e)
        {
            Txt.Text = "Ce programme permet d'effectuer jusqu'à 10 actions à un certain délai d'intervalle." + Environment.NewLine +
                "Différentes options sont à disposition :" + Environment.NewLine +
                "\t- Répétition aux jours de la semaine (Lundi à Dimanche). À ces jours, l'action sera effectuée." + Environment.NewLine +
                "\t- Répétition avec une intervalle de heures/minutes. En supplément avec l'option du dessus, elle" + Environment.NewLine +
                "\t  sera effectée uniquement si le jour de la semaine est un jour sélectionné. Autrement, l'action" + Environment.NewLine +
                "\t  s'effectuera à cette intervalle à partir de la date de départ" + Environment.NewLine +
                "\t- Répétition avec une intervalle de jours/semaines/mois (Incompatible avec les options du dessus)." + Environment.NewLine +
                "\t  L'action s'effectuera à cette intervalle à partir de la date de départ." + Environment.NewLine +
                "\t- Heure de début définissant l'heure de départ ou l'heure de l'exécution de l'action. Cette option" + Environment.NewLine +
                "\t  doit obligatoirement être définie" + Environment.NewLine + Environment.NewLine +
                "Différentes actions sont à disposition :" + Environment.NewLine +
                "\t- Action système" + Environment.NewLine +
                "\t- Commande effectuée dans l'invite de commande (CMD)" + Environment.NewLine +
                "\t- Applications à exécuter. Possible d'ajouter des arguments pour les différentes applications.";
        }
    }
}
