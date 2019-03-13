/*
 * Sujet  : Permet de demander des valeurs à l'utilisateur
 * Auteur : Esseiva Nicolas
 * Date   : 15.12.2016
 * Dernière modif. : 06.02.2017
 * 
 * Utilisation :
 *              - Appeler la méthode "Question" afin d'initialiser le formulare
 *              - ( Possibilité de positionner la fenêtre avec la méthode "SetPos" )
 *              - ( Possibilité de redimensionner la fenêtre avec la méthode "SetSize" )
 *              - Utiliser la fonction "ShowDialog" et tester le résultat "DialogResult"
 *              - Récuperer la valeur entrée par l'utilisateur en utilisant "Valeur" (Read only)
 */

#region Exemple d'utilisation
/*
  private void Main()
       {
           frmSaisie Dialogue = new frmSaisie();
           Dialogue.Question("Entrez votre nom", false);
           Dialogue.SetPos(false, 50, 50);
           if (Dialogue.ShowDialog() == DialogResult.OK)
           {
               label1.Text = Dialogue.Valeur;
           }
       }
 */
#endregion


using System;
using System.Drawing;
using System.Windows.Forms;

namespace Advanced_Actions
{
    /// <summary>
    /// Formulaire demandant à l'utilisateur une valeur.
    /// <para>Utilisation :</para>
    /// <para>- Appeler la méthode "Question"</para>
    /// <para>- Appeler la méthode "ShowDialog"</para>
    /// <para>- Tester "DialogResult"</para>
    /// <para>- Récupérer la variable "Valeur"</para>
    /// </summary>
    public partial class frmSaisie : Form
    {
        #region Déclarations

        // Variable qui stock l'input de l'utilisateur
        static string valeur;
        public string Valeur
        {
            get
            {
                return valeur;
            }
        }

        static bool OnOff;
        public bool ONOFF
        {
            get
            {
                return OnOff;
            }
        }

        // Position de la fenêtre (Statique pour sauvegarder quand elle est fermée)
        static Point DialogWindowPos;

        #endregion

        #region Initialisations

        // Constructeur
        public frmSaisie()
        {
            InitializeComponent();

            // Initialiser les résultats du dialogue
            btnOK.DialogResult = DialogResult.OK;
            btnAnnuler.DialogResult = DialogResult.Cancel;

            // Centrer la fenêtre
            SetPos(true);
        }

        /// <summary>
        /// Initialisation de la question (et du titre)
        /// </summary>
        /// <param name="Question">Question à demander</param>
        /// <param name="Titre">Titre du formulaire. Par défaut : "Saise"</param>
        public void Question(string Question, string Titre = "Saisie")
        {
            // Question
            lblDonnee.Text = Question;

            // Titre
            this.Text = Titre;
        }

        /// <summary>
        /// Initialisation de la position de la fenêtre (Optionnel)
        /// </summary>
        /// <param name="Center">True : Centrer la fenêtre au milieu. Par défaut : True</param>
        /// <param name="PositionX">Si pas centré, définit la position X.</param>
        /// <param name="PositionY">Si pas centré, définit la position Y</param>
        public void SetPos(bool Center, int PositionX = 0, int PositionY = 0)
        {
            if (Center) // Centrer la fenêtre
                DialogWindowPos = new Point((Screen.PrimaryScreen.WorkingArea.Width / 2) - (this.Width / 2), (Screen.PrimaryScreen.WorkingArea.Height / 2) - (this.Height / 2));
            else    // Sinon placer la fenêtre
            {
                DialogWindowPos = new Point(PositionX, PositionY);
            }
        }

        /// <summary>
        /// Définit la taille du formulaire (Optionnel)
        /// <para>Par défaut : 300x170</para>
        /// </summary>
        /// <param name="Width">Largeur</param>
        /// <param name="Height">Hauteur</param>
        public void SetSize(int Width, int Height)
        {   // Si supérieur aux limites
            if ((Width > this.MinimumSize.Width) && (Height > this.MinimumSize.Height))
            {
                this.Size = new Size(Width, Height);
            }
        }

        /// <summary>
        /// Réponse prédéfinie
        /// </summary>
        /// <param name="Text">Réponse</param>
        public void LoadAnswer(string Text,bool TextEnabled = true)
        {
            txtValeur.Text = Text;
            _boxToggle.Checked = TextEnabled;
        }

        #endregion

        #region Fonctionnement

        private void Form2_Load(object sender, EventArgs e)
        {   // Déplacer la fenêtre à la position précédente
            this.Location = DialogWindowPos;
            // Effacer l'input
            valeur = "";
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            CloseForm();
        }

        private void btnAnnuler_Click(object sender, EventArgs e)
        {
            CloseForm();
        }

        private void CloseForm()
        {
            // Sauvegarde la valeur
            valeur = txtValeur.Text;
            // Initialise l'input pour la prochaine fois
            txtValeur.Clear();
            txtValeur.Focus();
            // Sauvegarde la position de la fenêtre
            DialogWindowPos = this.Location;
            // Fermer le formulaire
            Close();
        }

        #endregion

        private void _boxToggle_CheckedChanged(object sender, EventArgs e)
        {
            txtValeur.Enabled = _boxToggle.Checked;
            OnOff = _boxToggle.Checked;
        }
    }
}

