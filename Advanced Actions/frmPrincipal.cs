/* Sujet  : frmPrincipal
 * Auteur : Esseiva Nicolas
 * Date   : 18.06.2017
 * Dernière modif. : 18.06.2017
 * Description :  Permet d'exécuter une action à un moment défini.
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advanced_Actions.Properties;
using System.Windows.Forms;
using System.IO;
using System.Configuration;
using System.Collections;
using System.Runtime.InteropServices;
using System.Web.Script.Serialization;

namespace Advanced_Actions
{
    public partial class frmPrincipal : Form
    {
        ArrayList FilePaths = new ArrayList();
        ArrayList FileArgs = new ArrayList();
        public static ArrayList LastFileArgs = new ArrayList();
        private bool CloseContextMenu = true;

        private static bool IsEnabled = true;
        private static string AnswerRename = "";

        public frmPrincipal()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Appliques les paramètres
        /// </summary>
        private void _btnApply_Click(object sender, EventArgs e)
        {   // Sauvegarde des paramètres
            try
            {
                bool SD = _BoxDays.Checked;
                bool RD = _BoxInterval2.Checked;
                bool RT = _BoxInterval1.Checked;

                if (((_SelHour.SelectedIndex > _SelMaxHour.SelectedIndex) && (_SelMin.SelectedIndex > _SelMaxMin.SelectedIndex) && _boxMaxHour.Checked) && (/*(SD && !RD && RT) ||*/ (SD && !RD && !RT)))
                {

                    MessageBox.Show("Vous ne pouvez pas choisir une heure maximale supérieure à l'heure de départ avec ces paramètres choisis.", "ATTENTION !", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    // Choisir le nom de la sauvegarde
                    string SettingName;
                    frmSaisieSave Saisie = new frmSaisieSave();
                    Saisie.Question("Indiquez le nom de la sauvegarde", "Sauvegarder sous...");
                    Saisie.SetPos(true);

                    Saisie.SetControls(true, "Créer une nouvelle sauvegarde", false);

                    if (AnswerRename == "" && Settings.Default.NumberSettings != 0)
                        AnswerRename = (string)Settings.Default[("SettingsName" + (Settings.Default.ActiveSettingsNumber))];
                    Saisie.LoadAnswer(AnswerRename);

                    if ((Saisie.ShowDialog() == DialogResult.OK) && Saisie.Valeur.Length != 0)
                    {
                        // Verification nom existant
                        SettingName = Saisie.Valeur;

                        bool Wait = true;
                        bool Exist = false;
                        short i = 1;
                        while (Wait && i < 11)
                        {
                            string NameTemp = (string)Settings.Default[("SettingsName" + i)];
                            if (NameTemp == SettingName)
                            {
                                Wait = false;
                                Exist = true;
                            }
                            i++;
                        }
                        if (Exist)
                        {   // Si le nom existe déjà
                            if ((Saisie.BoxValeur) && (MessageBox.Show("Une sauvegarde du même nom existe." + Environment.NewLine + "Remplacer ?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes))
                            {   // Remplacer
                                short IndexTemp = (short)(i - 1);
                                SetSettings(IndexTemp, SettingName);
                            }
                            else if (!Saisie.BoxValeur && (i - 1) != Settings.Default.ActiveSettingsNumber)
                            {   // Annuler
                                MessageBox.Show("Une sauvegarde du même nom existe." + Environment.NewLine + "Veuillez choisir un autre nom.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                AnswerRename = SettingName;
                                _btnApply.PerformClick();
                            }
                            else if (!Saisie.BoxValeur && (i - 1) == Settings.Default.ActiveSettingsNumber)
                            {
                                // Renommer
                                short IndexTemp = (short)(Settings.Default.ActiveSettingsNumber);
                                SetSettings(IndexTemp, SettingName);

                                // Updates
                                // Mise à jour sélection des settings
                                string NameTemp = "";
                                short j = 1;
                                toolStripComboBox1.Items.Clear();
                                while (j <= (short)Settings.Default.NumberSettings)
                                {
                                    NameTemp = (string)(Settings.Default[("SettingsName" + j)]);
                                    toolStripComboBox1.Items.Add(NameTemp);

                                    var ControlTemp = (ToolStripMenuItem)_MenuStrip.Items.Find(("tsmiAction" + j), true).FirstOrDefault();
                                    ControlTemp.Text = NameTemp;
                                    ControlTemp.Enabled = true;
                                    ControlTemp.Checked = (bool)(Settings.Default[("Enabled" + j)]);

                                    j++;
                                }

                                // Sélection des setting
                                toolStripComboBox1.SelectedIndex = (IndexTemp - 1);

                                // Button
                                _btnToggle_Click(null, EventArgs.Empty);


                                TimeSpan DeltaTemp = frmLoad.GetActionsDates[(Settings.Default.ActiveSettingsNumber - 1)] - DateTime.Now;
                                string MSG_Time = "-";
                                string MSG_Date = "-";
                                if (DeltaTemp.TotalSeconds > 0)
                                {
                                    int Delta = (int)DeltaTemp.TotalSeconds;
                                    MSG_Time = string.Format("{0}J {1}H {2}min {3}sec", DeltaTemp.Days, DeltaTemp.Hours, DeltaTemp.Minutes, DeltaTemp.Seconds);
                                    MSG_Date = frmLoad.GetActionsDates[(Settings.Default.ActiveSettingsNumber - 1)].ToString();
                                }
                                string MSG_Name = (string)Settings.Default[("SettingsName" + Settings.Default.ActiveSettingsNumber)];
                                _txtActiveSettings.Text = "Sauvegarde : " + MSG_Name + " | Prochaine activation dans : " + MSG_Time + " | Le : " + MSG_Date;
                            }
                        }
                        else
                        {   // Nom non existant
                            if (Saisie.BoxValeur)
                            {   // Nouvelle sauvegarde
                                if (Settings.Default.NumberSettings != 10)
                                {
                                    Settings.Default.NumberSettings++;
                                    short IndexTemp = (short)(toolStripComboBox1.Items.Count + 1);
                                    SetSettings(IndexTemp, SettingName);
                                }
                                else
                                {
                                    MessageBox.Show("Nombre limite de sauvegarde atteint !", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    AnswerRename = Saisie.Valeur;
                                    _btnApply.PerformClick();
                                }
                            }
                            else if (Settings.Default.NumberSettings != 0)
                            {   // Renommer
                                short IndexTemp = (short)(Settings.Default.ActiveSettingsNumber);
                                SetSettings(IndexTemp, SettingName);

                                // Updates
                                // Mise à jour sélection des settings
                                string NameTemp = "";
                                short j = 1;
                                toolStripComboBox1.Items.Clear();
                                while (j <= (short)Settings.Default.NumberSettings)
                                {
                                    NameTemp = (string)(Settings.Default[("SettingsName" + j)]);
                                    toolStripComboBox1.Items.Add(NameTemp);

                                    var ControlTemp = (ToolStripMenuItem)_MenuStrip.Items.Find(("tsmiAction" + j), true).FirstOrDefault();
                                    ControlTemp.Text = NameTemp;
                                    ControlTemp.Enabled = true;
                                    ControlTemp.Checked = (bool)(Settings.Default[("Enabled" + j)]);

                                    j++;
                                }

                                // Sélection des setting
                                toolStripComboBox1.SelectedIndex = (IndexTemp - 1);

                                // Button
                                _btnToggle_Click(null, EventArgs.Empty);


                                TimeSpan DeltaTemp = frmLoad.GetActionsDates[(Settings.Default.ActiveSettingsNumber - 1)] - DateTime.Now;
                                string MSG_Time = "-";
                                string MSG_Date = "-";
                                if (DeltaTemp.TotalSeconds > 0)
                                {
                                    int Delta = (int)DeltaTemp.TotalSeconds;
                                    MSG_Time = string.Format("{0}J {1}H {2}min {3}sec", DeltaTemp.Days, DeltaTemp.Hours, DeltaTemp.Minutes, DeltaTemp.Seconds);
                                    MSG_Date = frmLoad.GetActionsDates[(Settings.Default.ActiveSettingsNumber - 1)].ToString();
                                }
                                string MSG_Name = (string)Settings.Default[("SettingsName" + Settings.Default.ActiveSettingsNumber)];
                                _txtActiveSettings.Text = "Sauvegarde : " + MSG_Name + " | Prochaine activation dans : " + MSG_Time + " | Le : " + MSG_Date;
                            }
                            else
                            {
                                _txtActiveSettings.Text = "Aucune sauvegarde sélectionnée";
                                MessageBox.Show("Aucune sauvegarde sélectionnée." + Environment.NewLine + "", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                AnswerRename = SettingName;
                                _btnApply.PerformClick();
                            }
                        }
                    }
                    AnswerRename = "";
                }
            }
            catch (Exception ee)
            {
                Console.WriteLine(ee);
                MessageBox.Show(ee.ToString(), "ERREUR !", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Recrée les settings pour une utilisation prochaine
        /// </summary>
        private void RefreshOnStart()
        {
            // Creation des valeurs vides pour l'utilisation
            Settings.Default.Reload();

            for (short i = 1; i < 11; i++)
            {
                NewSettings(i);
            }

            Settings.Default.NumberSettings = 0;
            for (int i = 1; i < 11; i++)
            {
                if ((string)Settings.Default[("SettingsName" + i)] != "")
                {
                    Settings.Default.NumberSettings++;
                }
                else
                {
                    i = 11;
                }
            }
            // Mise à jour sélection des settings
            if (Settings.Default.NumberSettings != 0)
            {
                string NameTemp = "";
                short i = 1;
                toolStripComboBox1.Items.Clear();
                while (i <= (short)Settings.Default.NumberSettings)
                {
                    NameTemp = (string)(Settings.Default[("SettingsName" + i)]);
                    toolStripComboBox1.Items.Add(NameTemp);

                    var ControlTemp = (ToolStripMenuItem)_MenuStrip.Items.Find(("tsmiAction" + i), true).FirstOrDefault();
                    ControlTemp.Text = NameTemp;
                    ControlTemp.Enabled = true;
                    ControlTemp.Checked = (bool)(Settings.Default[("Enabled" + i)]);
                    i++;
                }

                // Chargement des paramètres #1
                short IndexTemp = Settings.Default.ActiveSettingsNumber;
                LoadSettings(IndexTemp);

                // Sélection du setting de base
                toolStripComboBox1.SelectedIndex = (IndexTemp - 1);
            }
        }

        /// <summary>
        /// Créer les paramètres
        /// </summary>
        private void NewSettings(short Index)
        {
            var property = new SettingsProperty(Settings.Default.Properties["SystemAction"]);
            property.Name = ("SystemAction" + Index);
            Settings.Default.Properties.Add(property);

            property = new SettingsProperty(Settings.Default.Properties["SystemAction_ON"]);
            property.Name = ("SystemAction_ON" + Index);
            Settings.Default.Properties.Add(property);

            property = new SettingsProperty(Settings.Default.Properties["CMD"]);
            property.Name = ("CMD" + Index);
            Settings.Default.Properties.Add(property);

            property = new SettingsProperty(Settings.Default.Properties["CMD_ON"]);
            property.Name = ("CMD_ON" + Index);
            Settings.Default.Properties.Add(property);

            property = new SettingsProperty(Settings.Default.Properties["App"]);
            property.Name = ("App" + Index);
            Settings.Default.Properties.Add(property);

            property = new SettingsProperty(Settings.Default.Properties["App_ON"]);
            property.Name = ("App_ON" + Index);
            Settings.Default.Properties.Add(property);

            property = new SettingsProperty(Settings.Default.Properties["None_ON"]);
            property.Name = ("None_ON" + Index);
            Settings.Default.Properties.Add(property);

            property = new SettingsProperty(Settings.Default.Properties["SpecificDays"]);
            property.Name = ("SpecificDays" + Index);
            Settings.Default.Properties.Add(property);

            property = new SettingsProperty(Settings.Default.Properties["Day"]);
            property.Name = ("Day" + Index);
            Settings.Default.Properties.Add(property);

            property = new SettingsProperty(Settings.Default.Properties["RepeatOnDay"]);
            property.Name = ("RepeatOnDay" + Index);
            Settings.Default.Properties.Add(property);

            property = new SettingsProperty(Settings.Default.Properties["RepeatODNBR"]);
            property.Name = ("RepeatODNBR" + Index);
            Settings.Default.Properties.Add(property);

            property = new SettingsProperty(Settings.Default.Properties["RepeatODType"]);
            property.Name = ("RepeatODType" + Index);
            Settings.Default.Properties.Add(property);

            property = new SettingsProperty(Settings.Default.Properties["RepeatOnTime"]);
            property.Name = ("RepeatOnTime" + Index);
            Settings.Default.Properties.Add(property);

            property = new SettingsProperty(Settings.Default.Properties["RepeatOTNBR"]);
            property.Name = ("RepeatOTNBR" + Index);
            Settings.Default.Properties.Add(property);

            property = new SettingsProperty(Settings.Default.Properties["RepeatOTType"]);
            property.Name = ("RepeatOTType" + Index);
            Settings.Default.Properties.Add(property);

            property = new SettingsProperty(Settings.Default.Properties["SpecificTime"]);
            property.Name = ("SpecificTime" + Index);
            Settings.Default.Properties.Add(property);

            property = new SettingsProperty(Settings.Default.Properties["StartOn"]);
            property.Name = ("StartOn" + Index);
            Settings.Default.Properties.Add(property);

            property = new SettingsProperty(Settings.Default.Properties["EndOn"]);
            property.Name = ("EndOn" + Index);
            Settings.Default.Properties.Add(property);

            property = new SettingsProperty(Settings.Default.Properties["App_Args"]);
            property.Name = ("App_Args" + Index);
            Settings.Default.Properties.Add(property);

            property = new SettingsProperty(Settings.Default.Properties["Enabled"]);
            property.Name = ("Enabled" + Index);
            Settings.Default.Properties.Add(property);

            property = new SettingsProperty(Settings.Default.Properties["MaxHour_ON"]);
            property.Name = ("MaxHour_ON" + Index);
            Settings.Default.Properties.Add(property);

            property = new SettingsProperty(Settings.Default.Properties["MaxHour"]);
            property.Name = ("MaxHour" + Index);
            Settings.Default.Properties.Add(property);
        }

        /// <summary>
        /// Mets à jour les paramètres
        /// </summary>
        /// <param name="Index">Sauvegarde #</param>
        /// <param name="SettingName">Nom de la sauvegarde</param>
        private void SetSettings(short Index, string SettingName = "")
        {
            try
            {
                Settings.Default[("SettingsName" + Index)] = (string)SettingName;
                Settings.Default[("Enabled" + Index)] = IsEnabled;

                // SystemAction, CMD, App
                Settings.Default[("SystemAction" + Index)] = (short)_SelActionSYS.SelectedIndex;
                Settings.Default[("CMD" + Index)] = (string)_SelCMD.Text;
                Settings.Default[("App" + Index)] = (ArrayList)(FilePaths.Clone());

                // App_Args
                Settings.Default[("App_Args" + Index)] = (ArrayList)(FileArgs.Clone());

                // SystemAction_ON, CMD_ON, App_ON
                Settings.Default[("SystemAction_ON" + Index)] = (bool)_BoxActionSys.Checked;
                Settings.Default[("CMD_ON" + Index)] = (bool)_BoxActionCMD.Checked;
                Settings.Default[("App_ON" + Index)] = (bool)_BoxActionApp.Checked;
                Settings.Default[("None_ON" + Index)] = (bool)_boxActionNull.Checked;

                // SpecificDays, Day
                Settings.Default[("SpecificDays" + Index)] = (bool)_BoxDays.Checked;

                short Temp = 0;
                if (_SelLundi.Checked)
                    Temp = (short)(Temp | 1);
                if (_SelMardi.Checked)
                    Temp = (short)(Temp | 2);
                if (_SelMercredi.Checked)
                    Temp = (short)(Temp | 4);
                if (_SelJeudi.Checked)
                    Temp = (short)(Temp | 8);
                if (_SelVendredi.Checked)
                    Temp = (short)(Temp | 16);
                if (_SelSamedi.Checked)
                    Temp = (short)(Temp | 32);
                if (_SelDimanche.Checked)
                    Temp = (short)(Temp | 64);

                if (!((Temp == 0) && !(_BoxDays.Checked)) || !(_BoxDays.Checked))
                {
                    Settings.Default[("Day" + Index)] = (short)Temp;

                    // RepeatOnDay, RepeatODNBR, RepeatODType
                    Settings.Default[("RepeatOnDay" + Index)] = (bool)_BoxInterval2.Checked;
                    Settings.Default[("RepeatODNBR" + Index)] = (short)_SelInterval2.Value;
                    Settings.Default[("RepeatODType" + Index)] = (short)_SelIntervalType2.SelectedIndex;

                    // RepeatOnTime, RepeatOTNBR, RepeatOTType
                    Settings.Default[("RepeatOnTime" + Index)] = (bool)_BoxInterval1.Checked;
                    Settings.Default[("RepeatOTNBR" + Index)] = (short)_SelInterval1.Value;
                    Settings.Default[("RepeatOTType" + Index)] = (short)_SelIntervalType1.SelectedIndex;

                    // SpecificTime
                    Settings.Default[("SpecificTime" + Index)] = (DateTime)(new DateTime(2, 2, 2, (_SelHour.SelectedIndex), (_SelMin.SelectedIndex), 0));

                    // MaxHour, MaxHour_ON
                    Settings.Default[("MaxHour_ON" + Index)] = (bool)_boxMaxHour.Checked;
                    Settings.Default[("MaxHour" + Index)] = (DateTime)(new DateTime(2, 2, 2, (_SelMaxHour.SelectedIndex), (_SelMaxMin.SelectedIndex), 0));

                    // StartOn
                    if (_boxCalStart.Checked)
                        Settings.Default[("StartOn" + Index)] = (DateTime)(DateTime.Today);
                    else
                        Settings.Default[("StartOn" + Index)] = (DateTime)(_CalStart.Value.Date);

                    // EndOn
                    if (_boxCalEnd.Checked)
                        Settings.Default[("EndOn" + Index)] = (DateTime)(_CalEnd.MaxDate);
                    else
                        Settings.Default[("EndOn" + Index)] = (DateTime)(_CalEnd.Value.Date);

                    // Save
                    Settings.Default.Save();

                    // Cherche prochaine action
                    frmLoad.CheckDates = true;

                    // Mise à jour sélection des settings
                    string NameTemp = "";
                    short i = 1;
                    toolStripComboBox1.Items.Clear();
                    while (i <= (short)Settings.Default.NumberSettings)
                    {
                        NameTemp = (string)(Settings.Default[("SettingsName" + i)]);
                        toolStripComboBox1.Items.Add(NameTemp);

                        var ControlTemp = (ToolStripMenuItem)_MenuStrip.Items.Find(("tsmiAction" + i), true).FirstOrDefault();
                        ControlTemp.Text = NameTemp;
                        ControlTemp.Enabled = true;
                        ControlTemp.Checked = (bool)(Settings.Default[("Enabled" + i)]);

                        i++;
                    }
                    // Sélection du setting
                    toolStripComboBox1.SelectedIndex = (Index - 1);

                    // Updates
                    // Button
                    _btnToggle_Click(null, EventArgs.Empty);

                    // ActiveSetting
                    Settings.Default.ActiveSettingsNumber = Index;

                    TimeSpan DeltaTemp = frmLoad.GetActionsDates[(Settings.Default.ActiveSettingsNumber - 1)] - DateTime.Now;
                    string MSG_Time = "-";
                    string MSG_Date = "-";
                    if (DeltaTemp.TotalSeconds > 0)
                    {
                        int Delta = (int)DeltaTemp.TotalSeconds;
                        MSG_Time = string.Format("{0}J {1}H {2}min {3}sec", DeltaTemp.Days, DeltaTemp.Hours, DeltaTemp.Minutes, DeltaTemp.Seconds);
                        MSG_Date = frmLoad.GetActionsDates[(Settings.Default.ActiveSettingsNumber - 1)].ToString();
                    }
                    string MSG_Name = (string)Settings.Default[("SettingsName" + Settings.Default.ActiveSettingsNumber)];
                    _txtActiveSettings.Text = "Sauvegarde : " + MSG_Name + " | Prochaine activation dans : " + MSG_Time + " | Le : " + MSG_Date;

                }
                else
                {
                    MessageBox.Show("Erreur !"+Environment.NewLine+"Veuillez réessayer sour un autre nom", "ERREUR !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ee)
            {
                Console.WriteLine(ee);
                MessageBox.Show(ee.ToString(), "ERREUR !", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Charge les paramètres
        /// </summary>
        /// <param name="Index">Sauvegade #</param>
        private void LoadSettings(short Index)
        {
            try
            {
                _BoxDays.Checked = (bool)Settings.Default[("SpecificDays" + Index)];

                short Temp = (short)Settings.Default[("Day" + Index)];

                _SelLundi.Checked = false;
                _SelMardi.Checked = false;
                _SelMercredi.Checked = false;
                _SelJeudi.Checked = false;
                _SelVendredi.Checked = false;
                _SelSamedi.Checked = false;
                _SelDimanche.Checked = false;

                if ((Temp & 1) > 0)
                    _SelLundi.Checked = true;
                if ((Temp & 2) > 0)
                    _SelMardi.Checked = true;
                if ((Temp & 4) > 0)
                    _SelMercredi.Checked = true;
                if ((Temp & 8) > 0)
                    _SelJeudi.Checked = true;
                if ((Temp & 16) > 0)
                    _SelVendredi.Checked = true;
                if ((Temp & 32) > 0)
                    _SelSamedi.Checked = true;
                if ((Temp & 64) > 0)
                    _SelDimanche.Checked = true;


                _BoxInterval2.Checked = (bool)Settings.Default[("RepeatOnDay" + Index)];
                _SelInterval2.Value = (short)Settings.Default[("RepeatODNBR" + Index)];
                _SelIntervalType2.SelectedIndex = (short)Settings.Default[("RepeatODType" + Index)];

                _BoxInterval1.Checked = (bool)Settings.Default[("RepeatOnTime" + Index)];
                _SelInterval1.Value = (short)Settings.Default[("RepeatOTNBR" + Index)];
                _SelIntervalType1.SelectedIndex = (short)Settings.Default[("RepeatOTType" + Index)];

                DateTime DateTemp = (DateTime)Settings.Default[("SpecificTime" + Index)];
                _SelHour.SelectedIndex = DateTemp.Hour;
                _SelMin.SelectedIndex = DateTemp.Minute;

                _boxMaxHour.Checked = (bool)Settings.Default[("MaxHour_ON" + Index)];
                DateTemp = (DateTime)Settings.Default[("MaxHour" + Index)];
                _SelMaxHour.SelectedIndex = DateTemp.Hour;
                _SelMaxMin.SelectedIndex = DateTemp.Minute;

                _CalStart.Value = (DateTime)Settings.Default[("StartOn" + Index)];
                _CalEnd.Value = (DateTime)Settings.Default[("EndOn" + Index)];

                _BoxActionSys.Checked = (bool)Settings.Default[("SystemAction_ON" + Index)];
                _BoxActionCMD.Checked = (bool)Settings.Default[("CMD_ON" + Index)];
                _BoxActionApp.Checked = (bool)Settings.Default[("App_ON" + Index)];
                _boxActionNull.Checked = (bool)Settings.Default[("None_ON" + Index)];

                _SelActionSYS.SelectedIndex = (short)Settings.Default[("SystemAction" + Index)];
                _SelCMD.Text = (string)Settings.Default[("CMD" + Index)];

                FilePaths = (ArrayList)(((ArrayList)(Settings.Default[("App" + Index)])).Clone());
                FileArgs = (ArrayList)(((ArrayList)(Settings.Default[("App_Args" + Index)])).Clone());
                LastFileArgs = (ArrayList)(((ArrayList)Settings.Default[("App_Args" + Index)]).Clone());

                IsEnabled = (bool)Settings.Default[("Enabled" + Index)];
                _btnToggle_Click(null, EventArgs.Empty);

                Settings.Default.ActiveSettingsNumber = (short)Index;
                Settings.Default.Save();

                if (Settings.Default.NumberSettings != 0)
                {
                    TimeSpan DeltaTemp = frmLoad.GetActionsDates[(Settings.Default.ActiveSettingsNumber - 1)] - DateTime.Now;
                    string MSG_Time = "-";
                    string MSG_Date = "-";
                    if (DeltaTemp.TotalSeconds > 0)
                    {
                        int Delta = (int)DeltaTemp.TotalSeconds;
                        MSG_Time = string.Format("{0}J {1}H {2}min {3}sec", DeltaTemp.Days, DeltaTemp.Hours, DeltaTemp.Minutes, DeltaTemp.Seconds);
                        MSG_Date = frmLoad.GetActionsDates[(Settings.Default.ActiveSettingsNumber - 1)].ToString();
                    }
                    string MSG_Name = (string)Settings.Default[("SettingsName" + Settings.Default.ActiveSettingsNumber)];
                    _txtActiveSettings.Text = "Sauvegarde : " + MSG_Name + " | Prochaine activation dans : " + MSG_Time + " | Le : " + MSG_Date;
                }
                else
                {
                    _txtActiveSettings.Text = "Aucune sauvegarde sélectionnée";
                }
            }
            catch (Exception ee)
            {
                Console.WriteLine(ee);
                MessageBox.Show(ee.ToString(), "ERREUR !", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Evenement du chargement des paramètres
        /// </summary>
        private void _btnLoadSettings_Click(object sender, EventArgs e)
        {   // Charger paramètres sélectionnés
            try
            {
                if (Settings.Default.NumberSettings != 0)
                {
                    short IndexTemp = (short)(toolStripComboBox1.SelectedIndex + 1);
                    string NameTemp = (string)toolStripComboBox1.Items[(IndexTemp - 1)];

                    LoadSettings(IndexTemp);
                    TimeSpan DeltaTemp = frmLoad.GetActionsDates[(Settings.Default.ActiveSettingsNumber - 1)] - DateTime.Now;
                    string MSG_Time = "-";
                    string MSG_Date = "-";
                    if (DeltaTemp.TotalSeconds > 0)
                    {
                        int Delta = (int)DeltaTemp.TotalSeconds;
                        MSG_Time = string.Format("{0}J {1}H {2}min {3}sec", DeltaTemp.Days, DeltaTemp.Hours, DeltaTemp.Minutes, DeltaTemp.Seconds);
                        MSG_Date = frmLoad.GetActionsDates[(Settings.Default.ActiveSettingsNumber - 1)].ToString();
                    }
                    string MSG_Name = (string)Settings.Default[("SettingsName" + Settings.Default.ActiveSettingsNumber)];
                    _txtActiveSettings.Text = "Sauvegarde : " + MSG_Name + " | Prochaine activation dans : " + MSG_Time + " | Le : " + MSG_Date;
                }
                else
                {
                    _txtActiveSettings.Text = "Aucune sauvegarde sélectionnée";
                    MessageBox.Show("Aucune sauvegarde sélectionnée."+Environment.NewLine+"", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ee)
            {
                Console.WriteLine(ee);
                MessageBox.Show(ee.ToString(), "ERREUR !", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        /// <summary>
        /// Initialisations
        /// </summary>
        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            try
            {
                // Inits & Updates
                _SelMin.SelectedIndex = 0;
                _SelHour.SelectedIndex = 0;
                _SelMaxHour.SelectedIndex = 0;
                _SelMaxMin.SelectedIndex = 0;
                _SelIntervalType1.SelectedIndex = 0;
                _SelIntervalType2.SelectedIndex = 0;
                _SelActionSYS.SelectedIndex = 0;
                _CalEnd.Value = DateTime.Today.AddYears(2);

                // Refresh
                RefreshOnStart();
                UpdateEnableControls(false, Settings.Default.ActiveSettingsNumber);
            }
            catch (Exception ee)
            {
                Console.WriteLine(ee);
                MessageBox.Show(ee.ToString(), "ERREUR !", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Mets à jour le bouton ON/OFF local ET/OU global
        /// </summary>
        /// <param name="GlobalOnly">Uniquement le global</param>
        /// <param name="TempIndex">Index du setting local</param>
        public void UpdateEnableControls(bool GlobalOnly = false, short TempIndex = 1)
        {
            if (!GlobalOnly)
            {
                if ((bool)Settings.Default[("Enabled" + TempIndex)])
                {
                    _btnToggle.Text = "ON | Désactiver cette action";
                    _btnToggle.BackColor = Color.Lime;
                }
                else
                {
                    _btnToggle.Text = "OFF | Activer cette action";
                    _btnToggle.BackColor = Color.Crimson;
                }
            }
            if (Settings.Default.Enabled)
            {
                toggleToolStripMenuItem.Text = "ON | Désactiver toutes les actions";
                toggleToolStripMenuItem.Image = Properties.Resources.Led_Green;
                _tsmiGlbEnable.Image = Properties.Resources.Led_Green;
            }
            else
            {
                toggleToolStripMenuItem.Text = "OFF | Activer toutes les actions";
                toggleToolStripMenuItem.Image = Properties.Resources.Led_Red;
                _tsmiGlbEnable.Image = Properties.Resources.Led_Red;
            }
        }

        #region Checkboxes and misc stuff

        private void frmPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {   // Prevents completly closing form
            e.Cancel = true;
            this.Hide();
        }

        private void _BoxDays_CheckedChanged(object sender, EventArgs e)
        {   // Toggle stuff
            _BoxInterval2.Enabled = !_BoxDays.Checked && !_BoxInterval1.Checked;
            _SelInterval2.Enabled = false;
            _SelIntervalType2.Enabled = false;

            _SelLundi.Enabled = _BoxDays.Checked;
            _SelMardi.Enabled = _BoxDays.Checked;
            _SelMercredi.Enabled = _BoxDays.Checked;
            _SelJeudi.Enabled = _BoxDays.Checked;
            _SelVendredi.Enabled = _BoxDays.Checked;
            _SelSamedi.Enabled = _BoxDays.Checked;
            _SelDimanche.Enabled = _BoxDays.Checked;
            CheckBtnApply();
        }

        private void _BoxInterval2_CheckedChanged(object sender, EventArgs e)
        {   // Toggle stuff
            _BoxDays.Enabled = !_BoxInterval2.Checked;
            //_BoxInterval1.Enabled = !_BoxInterval2.Checked;
            _SelLundi.Enabled = false;  
            _SelMardi.Enabled = false;  
            _SelMercredi.Enabled = false;  
            _SelJeudi.Enabled = false;  
            _SelVendredi.Enabled = false;  
            _SelSamedi.Enabled = false;
            _SelDimanche.Enabled = false;  

            _SelInterval2.Enabled = _BoxInterval2.Checked;
            _SelIntervalType2.Enabled = _BoxInterval2.Checked;
        }

        private void _BoxInterval1_CheckedChanged(object sender, EventArgs e)
        {   // Toggle stuff
            //_BoxInterval2.Enabled = !_BoxInterval1.Checked && !_BoxDays.Checked;

            _SelInterval1.Enabled = _BoxInterval1.Checked;
            _SelIntervalType1.Enabled = _BoxInterval1.Checked;
        }

        private void _boxCalStart_CheckedChanged(object sender, EventArgs e)
        {   // Toggle stuff
            _CalStart.Enabled = !_boxCalStart.Checked;
        }

        private void _boxCalEnd_CheckedChanged(object sender, EventArgs e)
        {   // Toggle stuff
            _CalEnd.Enabled = !_boxCalEnd.Checked;
        }

        private void _BoxActionSys_CheckedChanged(object sender, EventArgs e)
        {   // Toggle stuff
            _SelActionSYS.Enabled = _BoxActionSys.Checked;
            CheckBtnApply();
        }

        private void _BoxActionCMD_CheckedChanged(object sender, EventArgs e)
        {   // Toggle stuff
            _SelCMD.Enabled = _BoxActionCMD.Checked;
            CheckBtnApply();
        }

        private void _BoxActionApp_CheckedChanged(object sender, EventArgs e)
        {   // Toggle stuff
            _BtnChooseApp.Enabled = _BoxActionApp.Checked;
            CheckBtnApply();
        }

        private void _boxActionNull_CheckedChanged(object sender, EventArgs e)
        {   // Toggle stuff
            _BoxActionSys.Enabled = !_boxActionNull.Checked;
            _SelActionSYS.Enabled = (_BoxActionSys.Checked & !_boxActionNull.Checked);
            _BoxActionApp.Enabled = !_boxActionNull.Checked;
            _SelCMD.Enabled = (_BoxActionCMD.Checked & !_boxActionNull.Checked);
            _BoxActionCMD.Enabled = !_boxActionNull.Checked;
            _BtnChooseApp.Enabled = (_BoxActionApp.Checked & !_boxActionNull.Checked);
            CheckBtnApply();
        }

        /// <summary>
        /// Check requirements to enable button
        /// </summary>
        private void CheckBtnApply()
        {   // Check requirements to enable button
            if ((_BoxActionSys.Checked | _BoxActionCMD.Checked | _BoxActionApp.Checked | _boxActionNull.Checked) && (
                (_BoxDays.Checked && 
                (_SelLundi.Checked || _SelMardi.Checked || _SelMercredi.Checked || _SelJeudi.Checked || _SelVendredi.Checked || _SelSamedi.Checked || _SelDimanche.Checked)) || 
                !_BoxDays.Checked))
                _btnApply.Enabled = true;
            else
                _btnApply.Enabled = false;
        }

        private void interrompreLeServiceToolStripMenuItem_Click(object sender, EventArgs e)
        {   // Quit all
            frmLoad.MustQuit = true;
            this.Close();
        }

        private void quitterToolStripMenuItem_Click(object sender, EventArgs e)
        {   // Close the window
            this.Close();
        }

        /// <summary>
        /// Choose files to open on action
        /// </summary>
        private void _BtnChooseApp_Click(object sender, EventArgs e)
        {   // Let user select files
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                FilePaths.Clear();
                FileArgs.Clear();
                LastFileArgs.Clear();
                string[] temp = openFileDialog1.FileNames;
                foreach (string item in temp)
                {   // Store files
                    FilePaths.Add(item);
                    FileArgs.Add(null);
                    LastFileArgs.Add(null);
                }
            }
        }

        private void _btnSeeLinks_Click(object sender, EventArgs e)
        {   // Let uset change arguments and manage app links
            frmAppLinks.FilePaths = (ArrayList)this.FilePaths.Clone();
            frmAppLinks.FileArgs = (ArrayList)this.FileArgs.Clone();
            frmAppLinks.LastFileArgs = (ArrayList)LastFileArgs.Clone();

            frmAppLinks links = new frmAppLinks();
            links.Main(FilePaths, FileArgs);
            if (links.ShowDialog() == DialogResult.OK)
            {
                this.FilePaths = (ArrayList)frmAppLinks.FilePaths.Clone();
                this.FileArgs = (ArrayList)frmAppLinks.FileArgs.Clone();
                LastFileArgs = (ArrayList)frmAppLinks.LastFileArgs.Clone();
            }
        }

        public void _btnToggle_Click(object sender, EventArgs e)
        {   // Enable/Disable actions
            short TempIndex = (short)(toolStripComboBox1.SelectedIndex + 1);
            if (e != EventArgs.Empty)
                IsEnabled = !IsEnabled;

            if (IsEnabled)
            {
                _btnToggle.Text = "ON | Désactiver cette action";
                _btnToggle.BackColor = Color.Lime;
            }
            else
            {
                _btnToggle.Text = "OFF | Activer cette action";
                _btnToggle.BackColor = Color.Crimson;
            }
        }

        private void prochaineActionÀToolStripMenuItem_MouseDown(object sender, MouseEventArgs e)
        {   // Tell not to close menu if clicked on those controls
            CloseContextMenu = false;
        }

        private void _ContextMenu_Closing(object sender, ToolStripDropDownClosingEventArgs e)
        {   // Prevent closing when closing on certain controls
            e.Cancel = !CloseContextMenu;
            CloseContextMenu = true;
        }

        private void toggleToolStripMenuItem_Click(object sender, EventArgs e)
        {   // Enable/Disable actions
            Settings.Default.Enabled = !Settings.Default.Enabled;
            Settings.Default.Save();
            UpdateEnableControls(true);
            frmLoad.CheckDates = true;
        }

        private void _ContextMenu_Opened(object sender, EventArgs e)
        {   // Enable Timer to show time left
            _tmrTimeLeft.Enabled = true;

            // Mise à jour sélection des settings
            string NameTemp = "";
            short i = 1;
            while (i <= (short)Settings.Default.NumberSettings)
            {
                NameTemp = (string)(Settings.Default[("SettingsName" + i)]);

                var ControlTemp = (ToolStripMenuItem)_ContextMenu.Items.Find(("_tsmiActions" + i), true).FirstOrDefault();
                ControlTemp.Text = NameTemp;
                ControlTemp.Enabled = true;
                ControlTemp.Checked = false;

                i++;
            }
        }

        private void _ContextMenu_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {   // Disable timer
            _tmrTimeLeft.Enabled = false;
        }

        private void _ContextMenu_Opening(object sender, CancelEventArgs e)
        {   // Update values
            short IndexTemp;
            frmLoad.getDeltaTime(frmLoad.GetTheNextDate(frmLoad.GetActionsDates, out IndexTemp), DateTime.Now, IndexTemp);
            TimeLefttoolStripMenuItem.Text = frmLoad.NotifMSG1;
            DatetoolStripMenuItem2.Text = frmLoad.NotifMSG2;
            NametoolStripMenuItem5.Text = frmLoad.NotifMSG3;
            UpdateEnableControls(true);
        }

        private void _tmrTimeLeft_Tick(object sender, EventArgs e)
        {   // Timer Context menu
            short IndexTemp;
            frmLoad.getDeltaTime(frmLoad.GetTheNextDate(frmLoad.GetActionsDates, out IndexTemp), DateTime.Now, IndexTemp);
            TimeLefttoolStripMenuItem.Text = frmLoad.NotifMSG1;
            DatetoolStripMenuItem2.Text = frmLoad.NotifMSG2;
            NametoolStripMenuItem5.Text = frmLoad.NotifMSG3;
        }

        #endregion

        private void tsmiAction_CheckedChanged(object sender, EventArgs e)
        {
            if (frmLoad.CCPrin)
            {
                short i = 1;
                while (i <= (short)Settings.Default.NumberSettings)
                {
                    var ControlTemp = (ToolStripMenuItem)_MenuStrip.Items.Find(("tsmiAction" + i), true).FirstOrDefault();
                    Settings.Default[("Enabled" + i)] = ControlTemp.Checked;
                    i++;
                }
                Settings.Default.Save();
                frmLoad.CheckDates = true;
            }
        }

        private void _btnRename_Click(object sender, EventArgs e)
        {   // Sauvegarde des paramètres sous un nouveau nom
            try
            {
                if (Settings.Default.NumberSettings != 0)
                {
                    // Choisir le nom de la sauvegarde
                    string SettingName;
                    frmSaisieSave Saisie = new frmSaisieSave();
                    Saisie.Question("Indiquez le nouveau"+Environment.NewLine+"nom de \"" + Settings.Default[("SettingsName" + (Settings.Default.ActiveSettingsNumber))] + "\"", "Sauvegarder sous...");
                    Saisie.SetPos(true);

                    if (AnswerRename == "")
                        AnswerRename = (string)Settings.Default[("SettingsName" + (Settings.Default.ActiveSettingsNumber))];
                    Saisie.LoadAnswer(AnswerRename);

                    if (Saisie.ShowDialog() == DialogResult.OK && Saisie.Valeur.Length != 0)
                    {
                        // Verification nom existant
                        SettingName = Saisie.Valeur;

                        bool Wait = true;
                        bool Exist = false;
                        short i = 1;
                        while (Wait && i < 10)
                        {
                            string NameTemp0 = (string)Settings.Default[("SettingsName" + i)];
                            if (NameTemp0 == SettingName)
                            {
                                Wait = false;
                                Exist = true;
                            }
                            i++;
                        }
                        if (Exist)
                        {   // Si le nom existe déjà
                            MessageBox.Show("Une sauvegarde du même nom existe."+Environment.NewLine+"Veuillez choisir un autre nom.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            AnswerRename = (string)Settings.Default[("SettingsName" + i)];
                            _btnRename.PerformClick();
                            AnswerRename = "";
                        }
                        else
                        {   // Sinon remplacer
                            short IndexTemp = (short)(Settings.Default.ActiveSettingsNumber);
                            Settings.Default[("SettingsName" + IndexTemp)] = SettingName;
                            Settings.Default.Save();

                            // Updates
                            // Mise à jour sélection des settings
                            string NameTemp = "";
                            short j = 1;
                            toolStripComboBox1.Items.Clear();
                            while (j <= (short)Settings.Default.NumberSettings)
                            {
                                NameTemp = (string)(Settings.Default[("SettingsName" + j)]);
                                toolStripComboBox1.Items.Add(NameTemp);

                                var ControlTemp = (ToolStripMenuItem)_MenuStrip.Items.Find(("tsmiAction" + j), true).FirstOrDefault();
                                ControlTemp.Text = NameTemp;
                                ControlTemp.Enabled = true;
                                ControlTemp.Checked = (bool)(Settings.Default[("Enabled" + j)]);

                                j++;
                            }

                            // Sélection des setting
                            toolStripComboBox1.SelectedIndex = (IndexTemp - 1);

                            // Button
                            _btnToggle_Click(null, EventArgs.Empty);

                            TimeSpan DeltaTemp = frmLoad.GetActionsDates[(Settings.Default.ActiveSettingsNumber-1)] - DateTime.Now;
                            string MSG_Time = "-";
                            string MSG_Date = "-";
                            if (DeltaTemp.TotalSeconds > 0)
                            {
                                int Delta = (int)DeltaTemp.TotalSeconds;
                                MSG_Time = string.Format("{0}J {1}H {2}min {3}sec", DeltaTemp.Days, DeltaTemp.Hours, DeltaTemp.Minutes, DeltaTemp.Seconds);
                                MSG_Date = frmLoad.GetActionsDates[(Settings.Default.ActiveSettingsNumber - 1)].ToString();
                            }
                            string MSG_Name = (string)Settings.Default[("SettingsName" + Settings.Default.ActiveSettingsNumber)];
                            _txtActiveSettings.Text = "Sauvegarde : " + MSG_Name + " | Prochaine activation dans : " + MSG_Time + " | Le : " + MSG_Date;

                        }
                    }
                }
                else
                {
                    _txtActiveSettings.Text = "Aucune sauvegarde sélectionnée";
                    MessageBox.Show("Aucune sauvegarde sélectionnée."+Environment.NewLine+"", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ee)
            {
                Console.WriteLine(ee);
                MessageBox.Show(ee.ToString(), "ERREUR !", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void _tsmiGlbEnable_Click(object sender, EventArgs e)
        {
            Settings.Default.Enabled = !Settings.Default.Enabled;
            Settings.Default.Save();
            UpdateEnableControls(true);
            frmLoad.CheckDates = true;
        }

        private void _tsmiActions_CheckedChanged(object sender, EventArgs e)
        {
            short i = 1;
            while (i <= (short)Settings.Default.NumberSettings)
            {
                var ControlTemp = (ToolStripMenuItem)_ContextMenu.Items.Find(("_tsmiActions" + i), true).FirstOrDefault();
                if (ControlTemp.Checked)
                {
                    ControlTemp.Checked = false;
                    frmLoad.FlagExecuteAction = true;
                    frmLoad.FlagExecuteActionIndex = i;
                }
                i++;
            }
            Settings.Default.Save();
        }

        private void tsmiActions_DropDownOpened(object sender, EventArgs e)
        {
            frmLoad.CCPrin = true;
            frmLoad.CCLoad = false;
        }

        private void _tmrUpdate_Tick(object sender, EventArgs e)
        {
            if (frmLoad.CCLoad)
            {
                Settings.Default.Reload();
                string NameTemp = "";
                short j = 1;
                while (j <= (short)Settings.Default.NumberSettings)
                {
                    NameTemp = (string)(Settings.Default[("SettingsName" + j)]);

                    var ControlTemp = (ToolStripMenuItem)_MenuStrip.Items.Find(("tsmiAction" + j), true).FirstOrDefault();
                    ControlTemp.Text = NameTemp;
                    ControlTemp.Enabled = true;
                    ControlTemp.Checked = (bool)(Settings.Default[("Enabled" + j)]);

                    j++;
                }
            }
        }

        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show(string.Format("ATTENTION !"+Environment.NewLine+"Vous êtes sur le point de supprimer les sauvegardes."+Environment.NewLine+"Cette fonction est réversible en changeant manuellement le paramètre \"NumberSettings\" du fichier de sauvegarde à la valeur de {0}."+Environment.NewLine+"(Le fichier de sauvegarde se trouve sous AppData/Local/EsseivaN)"+Environment.NewLine+"Êtes-vous sûr de vouloir effacer les sauvegardes ?", Settings.Default.NumberSettings), "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    short i = 1;

                    while (i < 11)
                    {
                        Settings.Default[("SettingsName" + i)] = "";
                        i++;
                    }
                    Settings.Default.NumberSettings = 0;
                    Settings.Default.ActiveSettingsNumber = 0;
                    Settings.Default.Save();

                    // Refresh
                    // Mise à jour sélection des settings
                    i = 1;
                    toolStripComboBox1.Items.Clear();
                    while (i < 11)
                    {
                        var ControlTemp = (ToolStripMenuItem)_MenuStrip.Items.Find(("tsmiAction" + i), true).FirstOrDefault();
                        ControlTemp.Text = "-";
                        ControlTemp.Enabled = false;
                        ControlTemp.Checked = false;
                        i++;
                    }

                    i = 1;
                    while (i < 11)
                    {
                        var ControlTemp = (ToolStripMenuItem)_ContextMenu.Items.Find(("_tsmiActions" + i), true).FirstOrDefault();
                        ControlTemp.Text = "-";
                        ControlTemp.Enabled = false;
                        ControlTemp.Checked = false;

                        i++;
                    }

                    UpdateEnableControls(false, 1);
                }
            }
            catch (Exception ee)
            {
                Console.WriteLine(ee);
                MessageBox.Show(ee.ToString(), "ERREUR !", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void _txtActiveSettings_Enter(object sender, EventArgs e)
        {
            if (_btnApply.CanFocus)
                _btnApply.Focus();
            else
                _btnLoadSettings.Focus();
        }

        private void _SelDayWeek_CheckedChanged(object sender, EventArgs e)
        {
            CheckBtnApply();
        }

        private void AideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmHelp Aide = new frmHelp();
            Aide.ShowDialog();
        }

        private void _boxMaxHour_CheckedChanged(object sender, EventArgs e)
        {
            _SelMaxHour.Enabled = _boxMaxHour.Checked;
            _SelMaxMin.Enabled = _boxMaxHour.Checked;
            _lblHours2.Enabled = _boxMaxHour.Checked;
            _lblMinutes2.Enabled = _boxMaxHour.Checked;
        }
    }
}

    //AppSettings settings = new AppSettings();

    //public class AppSettings<T> where T : new()
    //{
    //    private const string DEFAULT_FILENAME = "ActionsSettings.json";

    //    public void Save(string fileName = DEFAULT_FILENAME)
    //    {
    //        File.WriteAllText(fileName, (new JavaScriptSerializer()).Serialize(this));
    //    }

    //    public static void Save(T pSettings, string fileName = DEFAULT_FILENAME)
    //    {
    //        File.WriteAllText(fileName, (new JavaScriptSerializer()).Serialize(pSettings));
    //    }

    //    public static T Load(string fileName = DEFAULT_FILENAME)
    //    {
    //        T t = new T();
    //        if (File.Exists(fileName))
    //            t = (new JavaScriptSerializer()).Deserialize<T>(File.ReadAllText(fileName));
    //        return t;
    //    }
    //}