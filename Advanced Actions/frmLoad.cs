/* Sujet  : frmLoad
 * Auteur : Esseiva Nicolas
 * Date   : 18.06.2017
 * Dernière modif. : 18.06.2017
 * Description :  Permet l'affichage en barre de notifications et permet de continuer en background
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Advanced_Actions.Properties;
using System.Windows.Forms;
using System.Diagnostics;
using System.Collections;
using System.Timers;

namespace Advanced_Actions
{
    public partial class frmLoad : Form
    {
        System.Timers.Timer Timer1 = new System.Timers.Timer();
        System.Timers.Timer Timer2 = new System.Timers.Timer();
        System.Timers.Timer Timer3 = new System.Timers.Timer();
        System.Timers.Timer Timer4 = new System.Timers.Timer();
        System.Timers.Timer Timer5 = new System.Timers.Timer();
        System.Timers.Timer Timer6 = new System.Timers.Timer();
        System.Timers.Timer Timer7 = new System.Timers.Timer();
        System.Timers.Timer Timer8 = new System.Timers.Timer();
        System.Timers.Timer Timer9 = new System.Timers.Timer();
        System.Timers.Timer Timer10 = new System.Timers.Timer();
        System.Timers.Timer TimerNotif = new System.Timers.Timer();

        public static bool CCPrin = false;
        public static bool CCLoad = false;
        public static bool FlagExecuteAction = false;
        public static short FlagExecuteActionIndex = -1;
        public static bool TimerNotifFlag = false;

        private static DateTime[] ActionsDates = new DateTime[10];
        public static DateTime[] GetActionsDates
        {
            get
            {
                return ActionsDates;
            }
        }

        private static bool CheckDates0 = false;
        public static bool CheckDates
        {
            set
            {
                CheckDates0 = value;
            }
        }


        public static string NotifMSG1 = "-";
        public static string NotifMSG2 = "-";
        public static string NotifMSG3 = "-";

        frmPrincipal MainForm = new frmPrincipal();
        System.Windows.Forms.Timer _tmrTimeLeft2 = new System.Windows.Forms.Timer();

        public static bool MustQuit = false;
        private bool CloseContextMenu = true;
        public static short ContinueCheck = 0;

        public frmLoad()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Permet le calcul de la prochaine date d'exécution
        /// </summary>
        public static void GetNextDates(DateTime[] DateTimeArray, short Delay = 0)
        {
            #region Initialization
            // Default values
            DateTime NextDate = new DateTime(2, 2, 2, 1, 1, 1);

            if (Settings.Default.Enabled)
            {
                // Repeat for each actions
                for (short i = 1; i <= (short)Settings.Default.NumberSettings; i++)
                {   // Check if end date passed

                    if (DateTime.Compare(DateTime.Today, (DateTime)Settings.Default[("EndOn" + i)]) == 1)
                        Settings.Default[("Enabled" + i)] = false;

                    // Check both enabled
                    if ((bool)Settings.Default[("Enabled" + i)])
                    {
                        #endregion Initialization

                        #region Get data
                        bool SD = (bool)Settings.Default[("SpecificDays" + i)];
                        bool RD = (bool)Settings.Default[("RepeatOnDay" + i)];
                        bool RT = (bool)Settings.Default[("RepeatOnTime" + i)];

                        bool MaxHour_ON = (bool)Settings.Default[("MaxHour_ON" + i)];

                        short Days = (short)Settings.Default[("Day" + i)];                      // Répétitions des jours

                        short IntervalDate = (short)Settings.Default[("RepeatODNBR" + i)];      // Intervalle
                        short RepeatDateType = (short)Settings.Default[("RepeatODType" + i)];   // En Mois/Semaines/Jours

                        DateTime StartDate = (DateTime)Settings.Default[("StartOn" + i)];       // Date de début
                        DateTime SpecificTime = ((DateTime)Settings.Default[("SpecificTime" + i)]).AddHours(0).AddMinutes(0);   // Heure spécifique
                        DateTime MaxHour = (DateTime)Settings.Default[("MaxHour" + i)];         // Max hour
                        DateTime CurrentTime = DateTime.Now.AddSeconds(Delay);                  // Current time

                        DateTime Temp;                                                          // Used date in calculs
                        short Interval = -1;
                        short RepeatType = -1;

                        if ((bool)Settings.Default[("RepeatOnTime" + i)])
                        {
                            Interval = (short)Settings.Default[("RepeatOTNBR" + i)];            // Intervalle
                            RepeatType = (short)Settings.Default[("RepeatOTType" + i)];         // En Heure/Minute
                        }

                        #endregion Récupération des données nécessaires

                        #region Specific days && Repeat on time OR Specific days

                        if ((SD && !RD && RT) || (SD && !RD && !RT))
                        {   // Specific days && Repeat on time OR Specific days

                            // Get the valid date (Startdate OR This day)
                            Temp = StartDate.AddHours(SpecificTime.Hour).AddMinutes(SpecificTime.Minute);   // Default : start date
                            if ((DateTime.Compare(Temp.AddSeconds(10), DateTime.Today) != 1))                // If start date (with 10s margin) < today
                                Temp = DateTime.Today.AddHours(SpecificTime.Hour).AddMinutes(SpecificTime.Minute);  // Temp = today

                            // Wait for the right day of week & for time to be upper than now (with 10s margin)
                            DayOfWeek Day1 = Temp.DayOfWeek;
                            bool IsAnyOK = false;

                            bool OK1 = false;
                            bool OK2 = false;
                            bool OK3 = false;
                            while (!OK1 || !OK2 || !OK3)
                            {
                                OK1 = true;
                                // Wait for Action date to be in future for now
                                while ((DateTime.Compare(Temp.AddSeconds(10), CurrentTime) < 0))
                                {
                                    OK1 = false;
                                    if (RT)
                                    {   // Add interval of time
                                        if (RepeatType == 0)
                                            Temp = Temp.AddHours(Interval);
                                        else if (RepeatType == 1)
                                            Temp = Temp.AddMinutes(Interval);
                                    }
                                    else if (!RT)
                                    {   // Add a day
                                        Temp = Temp.AddDays(1);
                                    }
                                }

                                OK2 = true;
                                // Wait for action to be on a designed day
                                while (!IsAnyOK)
                                {
                                    OK2 = false;
                                    // Check the day
                                    Day1 = Temp.DayOfWeek;
                                    IsAnyOK = false;
                                    if (((Days & 1) > 0) && Day1 == (DayOfWeek)(1))
                                        IsAnyOK = true;
                                    else if (((Days & 2) > 0) && Day1 == (DayOfWeek)(2))
                                        IsAnyOK = true;
                                    else if (((Days & 4) > 0) && Day1 == (DayOfWeek)(3))
                                        IsAnyOK = true;
                                    else if (((Days & 8) > 0) && Day1 == (DayOfWeek)(4))
                                        IsAnyOK = true;
                                    else if (((Days & 16) > 0) && Day1 == (DayOfWeek)(5))
                                        IsAnyOK = true;
                                    else if (((Days & 32) > 0) && Day1 == (DayOfWeek)(6))
                                        IsAnyOK = true;
                                    else if (((Days & 64) > 0) && Day1 == (DayOfWeek)(0))
                                        IsAnyOK = true;

                                    if (RT && !IsAnyOK)
                                    {   // Add interval of time
                                        if (RepeatType == 0)
                                            Temp = Temp.AddHours(Interval);
                                        else if (RepeatType == 1)
                                            Temp = Temp.AddMinutes(Interval);
                                    }
                                    else if (!RT && !IsAnyOK)
                                    {   // Add a day
                                        Temp = Temp.AddDays(1);
                                    }
                                }

                                OK3 = true;
                                // Check that action date is before max hour
                                while ((SD && !RD && RT) && MaxHour_ON && DateTime.Compare(Temp, new DateTime(Temp.Year, Temp.Month, Temp.Day, MaxHour.Hour, MaxHour.Minute, 0)) > 0)
                                {
                                    OK3 = false;
                                    if (RT)
                                    {   // Add interval of time
                                        if (RepeatType == 0)
                                            Temp = Temp.AddHours(Interval);
                                        else if (RepeatType == 1)
                                            Temp = Temp.AddMinutes(Interval);
                                    }
                                    else if (!RT)
                                    {   // Add a day
                                        Temp = Temp.AddDays(1);
                                    }
                                }
                            }
                            NextDate = Temp;
                        }
                        #endregion Specific days && Repeat on time OR Specific days

                        #region Repeat on day
                        else if (!SD && RD && !RT)
                        {   // Repeat on day

                            // Get the valid date (start date)
                            Temp = StartDate.AddHours(SpecificTime.Hour).AddMinutes(SpecificTime.Minute);

                            // Wait for time to be upper than now (with 10s margin)
                            while (DateTime.Compare(Temp.AddSeconds(10), CurrentTime) != 1)
                            {
                                // Add the inteval (month, week or day)
                                if (RepeatDateType == 0)    // Months
                                    Temp = Temp.AddMonths(IntervalDate);
                                if (RepeatDateType == 1)    // Week
                                    Temp = Temp.AddDays(IntervalDate * 7);
                                if (RepeatDateType == 2)    // Days
                                    Temp = Temp.AddDays(IntervalDate);
                            }
                            NextDate = Temp;
                        }
                        #endregion Repeat on day

                        #region Repeat on time
                        else if (!SD && !RD && RT)
                        {   // Repeat on time

                            // Get the valid date (start date)
                            Temp = StartDate.AddHours(SpecificTime.Hour).AddMinutes(SpecificTime.Minute);

                            // Wait for time to be upper than now (with -10s margin)
                            while (DateTime.Compare(Temp.AddSeconds(-10), CurrentTime) != 1)
                            {
                                if (RepeatType == 0)
                                    Temp = Temp.AddHours(Interval);
                                else if (RepeatType == 1)
                                    Temp = Temp.AddMinutes(Interval);
                            }
                            NextDate = Temp;
                        }
                        #endregion Repeat on time

                        #region Specific time
                        else
                        {   // None, specific time

                            // Get the valid date (Startdate OR This day)
                            Temp = StartDate.AddHours(SpecificTime.Hour).AddMinutes(SpecificTime.Minute);   // Default : start date
                            if ((DateTime.Compare(Temp.AddSeconds(10), DateTime.Today) != 1))                // If start date (with 10s margin) < today
                                Temp = DateTime.Today.AddHours(SpecificTime.Hour).AddMinutes(SpecificTime.Minute);  // Temp = today

                            if (DateTime.Compare(Temp.AddSeconds(10), CurrentTime) != 1)
                                Temp = Temp.AddDays(1);
                            NextDate = Temp;
                        }
                        #endregion Specific time

                        #region Finalization
                        // Check not end date passed
                        if (!(DateTime.Compare(NextDate, (DateTime)Settings.Default[("EndOn" + i)]) == 1))
                        {
                            Console.WriteLine("[Next Action #" + i + "] Action In : " + NextDate.ToString());
                            ContinueCheck = 0;
                        }
                        else
                        {   // End date passed
                            Console.WriteLine("[Next Action #" + i + "] No next date : End passed");
                            Console.WriteLine("[Next Action #" + i + "] No next date : Planned date was (not for sure) : " + NextDate.ToString());
                            NextDate = new DateTime(2, 2, 2, 1, 1, 1);
                            NotifMSG1 = "-";
                            NotifMSG2 = "-";
                            NotifMSG3 = "-";
                        }
                    }
                    else
                    {   // Action enable off
                        Console.WriteLine("[Next Action #" + i + "] No next date : Action enable OFF");
                        NotifMSG1 = "-";
                        NotifMSG2 = "-";
                        NotifMSG3 = "-";
                    }
                    DateTimeArray[(i - 1)] = NextDate;
                }
            }
            else
            {   // Global enale off
                Console.WriteLine("[Next Action #ALL] No next date : Global enable OFF");
                NotifMSG1 = "-";
                NotifMSG2 = "-";
                NotifMSG3 = "-";
            }
            #endregion Finalization
        }

        public void ExecuteAction(short Index, bool Force = false)
        {
            bool Sys_ON = (bool)Settings.Default[("SystemAction_ON" + Index)];
            short Sys = (short)Settings.Default[("SystemAction" + Index)];
            bool CMD_ON = (bool)Settings.Default[("CMD_ON" + Index)];
            string CMD = (string)Settings.Default[("CMD" + Index)];
            bool App_ON = (bool)Settings.Default[("App_ON" + Index)];
            ArrayList App = (ArrayList)((ArrayList)(Settings.Default[("App" + Index)])).Clone();
            ArrayList App_Args = (ArrayList)((ArrayList)(Settings.Default[("App_Args" + Index)])).Clone();

            if ((Settings.Default.Enabled && (bool)Settings.Default[("Enabled" + Index)]) || Force)
            {
                if (Sys_ON)
                {
                    Console.WriteLine("[ACTION EXECUTED] System : " + Sys);
                    switch (Sys)
                    {
                        case 0:
                            {   // Mettre en veille
                                Process.Start("CMD.exe", "/c shutdown -h -f -t 0");
                                break;
                            }
                        case 1:
                            {   // Fermer la session
                                Process.Start("CMD.exe", "/c shutdown -l -f -t 0");
                                break;
                            }
                        case 2:
                            {   // Arêter
                                Process.Start("CMD.exe", "/c shutdown -s -f -t 0");
                                break;
                            }
                        case 3:
                            {   // Redémarrer
                                Process.Start("CMD.exe", "/c shutdown -r -f -t 0");
                                break;
                            }
                    }
                }

                if (CMD_ON)
                {
                    Console.WriteLine("[ACTION EXECUTED] CMD : " + CMD);
                    Process.Start("CMD.exe", ("/c " + CMD));
                }

                if (App_ON)
                {
                    Console.WriteLine("[ACTION EXECUTED] App");
                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.WindowStyle = ProcessWindowStyle.Normal;

                    for (int i = 0; i < App.Count; i++)
                    {
                        Process proc = new Process();
                        startInfo.FileName = (string)App[i];
                        startInfo.Arguments = (string)App_Args[i];
                        proc.StartInfo = startInfo;
                        proc.Start();
                    }
                }
            }
            if(!FlagExecuteAction)
                CheckForAction(10);
        }

        public static DateTime GetTheNextDate(DateTime[] DateTimeArray, out short Index)
        {
            DateTime NextDate = DateTime.MaxValue;
            short i = 0;
            short IndexTemp = -1;
            for (; i < Settings.Default.NumberSettings; i++)
            {
                if ((ActionsDates[i] != new DateTime(2, 2, 2, 1, 1, 1)) && (ActionsDates[i] != new DateTime(1, 1, 1, 1, 1, 1)) && (ActionsDates[i] != new DateTime(1, 1, 1, 0, 0, 0)))
                    if (ActionsDates[i] < NextDate)
                    {
                        NextDate = ActionsDates[i];
                        IndexTemp = i;
                    }
            }
            if (IndexTemp < 0)
                IndexTemp = 0;
            Index = (short)(IndexTemp + 1);

            return ActionsDates[IndexTemp];
        }

        /// <summary>
        /// Verifie le temps restant pour la prochaine date
        /// </summary>
        public void CheckForAction(short Delay = 0)
        {
            bool OK = false;

            GetNextDates(ActionsDates, Delay);

            for (short i = 0; i < Settings.Default.NumberSettings; i++)
            {
                if (Settings.Default.Enabled && (bool)Settings.Default[("Enabled" + (i + 1))])
                {
                    short ValTemp = (short)DateTime.Compare(ActionsDates[i], DateTime.Now);
                    OK = (ValTemp <= 0);

                    if (!OK)
                    {   // Get intervak in seconds
                        int IntervalTimer = (int)(getDeltaTime(ActionsDates[i], DateTime.Now, i) * 1000);
                        // Minimum 10 sec
                        if (IntervalTimer <= 10 * 1000)
                            IntervalTimer = 10 * 1000;
                        Console.WriteLine("New interval @ Timer" + (i + 1) + " :" + IntervalTimer);
                        switch (i)
                        {
                            case (0):
                                {
                                    Timer1.Interval = IntervalTimer;
                                    Timer1.Enabled = true;
                                    break;
                                }
                            case (1):
                                {
                                    Timer2.Interval = IntervalTimer;
                                    Timer2.Enabled = true;
                                    break;
                                }
                            case (2):
                                {
                                    Timer3.Interval = IntervalTimer;
                                    Timer3.Enabled = true;
                                    break;
                                }
                            case (3):
                                {
                                    Timer4.Interval = IntervalTimer;
                                    Timer4.Enabled = true;
                                    break;
                                }
                            case (4):
                                {
                                    Timer5.Interval = IntervalTimer;
                                    Timer5.Enabled = true;
                                    break;
                                }
                            case (5):
                                {
                                    Timer6.Interval = IntervalTimer;
                                    Timer6.Enabled = true;
                                    break;
                                }
                            case (6):
                                {
                                    Timer7.Interval = IntervalTimer;
                                    Timer7.Enabled = true;
                                    break;
                                }
                            case (7):
                                {
                                    Timer8.Interval = IntervalTimer;
                                    Timer8.Enabled = true;
                                    break;
                                }
                            case (8):
                                {
                                    Timer9.Interval = IntervalTimer;
                                    Timer9.Enabled = true;
                                    break;
                                }
                            case (9):
                                {
                                    Timer10.Interval = IntervalTimer;
                                    Timer10.Enabled = true;
                                    break;
                                }
                        }
                        if (((IntervalTimer - 10 * 1000) < TimerNotif.Interval) || TimerNotif.Enabled == false)
                        {   // Timer -10 min (10 * 60 * 1000 ms)
                            int IntervalTemp = (IntervalTimer - 10 * 60 * 1000);
                            if (IntervalTemp <= 1000)
                                IntervalTemp = 1000;
                            TimerNotif.Interval = IntervalTemp;
                            Console.WriteLine("[NOTIF TIMER] Interval : " + IntervalTemp);
                            TimerNotif.Enabled = true;
                            TimerNotifFlag = false;
                        }
                    }
                }
            }
        }

        private void OnTimedEvent1(object sender, ElapsedEventArgs e)
        {
            Timer1.Enabled = false;
            ExecuteAction(1);
            CheckForAction(10);
        }
        private void OnTimedEvent2(object sender, ElapsedEventArgs e)
        {
            Timer2.Enabled = false;
            ExecuteAction(2);
            CheckForAction();
        }
        private void OnTimedEvent3(object sender, ElapsedEventArgs e)
        {
            Timer3.Enabled = false;
            ExecuteAction(3);
            CheckForAction();
        }
        private void OnTimedEvent4(object sender, ElapsedEventArgs e)
        {
            Timer4.Enabled = false;
            ExecuteAction(4);
            CheckForAction();
        }
        private void OnTimedEvent5(object sender, ElapsedEventArgs e)
        {
            Timer5.Enabled = false;
            ExecuteAction(5);
            CheckForAction();
        }
        private void OnTimedEvent6(object sender, ElapsedEventArgs e)
        {
            Timer6.Enabled = false;
            ExecuteAction(6);
            CheckForAction();
        }
        private void OnTimedEvent7(object sender, ElapsedEventArgs e)
        {
            Timer7.Enabled = false;
            ExecuteAction(7);
            CheckForAction();
        }
        private void OnTimedEvent8(object sender, ElapsedEventArgs e)
        {
            Timer8.Enabled = false;
            ExecuteAction(8);
            CheckForAction();
        }
        private void OnTimedEvent9(object sender, ElapsedEventArgs e)
        {
            Timer9.Enabled = false;
            ExecuteAction(9);
            CheckForAction();
        }
        private void OnTimedEvent10(object sender, ElapsedEventArgs e)
        {
            Timer10.Enabled = false;
            ExecuteAction(10);
            CheckForAction();
        }

        /// <summary>
        /// Get value in seconds between 2 dates
        /// </summary>
        public static int getDeltaTime(DateTime TAction, DateTime TCurrent, short Index = 0)
        {
            int Delta = -1;
            if (Settings.Default.Enabled)
            {
                TimeSpan DeltaTemp = TAction - TCurrent;
                if (DeltaTemp.TotalSeconds > 0)
                {
                    Delta = (int)DeltaTemp.TotalSeconds;
                    NotifMSG1 = string.Format("{0}J {1}H {2}min {3}sec", DeltaTemp.Days, DeltaTemp.Hours, DeltaTemp.Minutes, DeltaTemp.Seconds);

                    NotifMSG3 = "";
                    if (Index != 0)
                        NotifMSG3 = (string)Settings.Default[("SettingsName" + Index)];
                    NotifMSG2 = TAction.ToString();
                }
                else
                {
                    NotifMSG1 = "-";
                    NotifMSG2 = "-";
                    NotifMSG3 = "-";
                }
            }
            else
            {
                NotifMSG1 = "-";
                NotifMSG2 = "-";
                NotifMSG3 = "-";
            }

            return Delta;
        }

        void frmLoad_Load(object sender, EventArgs e)
        {   // Ne pas afficher cette fenêtre
            this.Size = new Size(0, 0);
            _Notification.ShowBalloonTip(5);
            // Afficher la fenêtre principale
            MainForm.FormClosing += MainForm_FormClosing;
            MainForm.Shown += MainForm_Shown;
            MainForm.Show();

            CheckForAction(10);

            Timer1.Elapsed += new ElapsedEventHandler(OnTimedEvent1);
            Timer2.Elapsed += new ElapsedEventHandler(OnTimedEvent2);
            Timer3.Elapsed += new ElapsedEventHandler(OnTimedEvent3);
            Timer4.Elapsed += new ElapsedEventHandler(OnTimedEvent4);
            Timer5.Elapsed += new ElapsedEventHandler(OnTimedEvent5);
            Timer6.Elapsed += new ElapsedEventHandler(OnTimedEvent6);
            Timer7.Elapsed += new ElapsedEventHandler(OnTimedEvent7);
            Timer8.Elapsed += new ElapsedEventHandler(OnTimedEvent8);
            Timer9.Elapsed += new ElapsedEventHandler(OnTimedEvent9);
            Timer10.Elapsed += new ElapsedEventHandler(OnTimedEvent10);
            TimerNotif.Elapsed += new ElapsedEventHandler(TimerNotif_Elapsed);
        }

        private void TimerNotif_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (!_tmrTimeLeft2.Enabled)
            {
                short temp = (short)_tmrTimeLeft2.Interval;
                _tmrTimeLeft2.Enabled = true;
            }
            TimerNotifFlag = true;
            TimerNotif.Enabled = false;
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            this.ouvrirToolStripMenuItem.Enabled = false;
            _tmrTimeLeft2.Interval = 1000;
            _tmrTimeLeft2.Tick += _tmrTimeLeft2_Tick;
            _tmrTimeLeft2.Enabled = true;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MustQuit)
            {
                this.Close();
                MustQuit = false;
            }
            this.ouvrirToolStripMenuItem.Enabled = true;
            _tmrTimeLeft2.Enabled = false;
        }

        #region NotifMenu

        private void ouvrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _tmrTimeLeft2.Enabled = true;
            this.ouvrirToolStripMenuItem.Enabled = false;
            // Afficher la fenêtre principale
            MainForm.Show();
        }

        private void exécuterLactionToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void quitterToolStripMenuItem_Click(object sender, EventArgs e)
        {   // Quitter
            this.Close();
        }

        #endregion

        private void frmLoad_FormClosing(object sender, FormClosingEventArgs e)
        {   // Demander confirmation
            DialogResult DR = MessageBox.Show("Attention ! Les actions ne seront plus executées si vous fermez l'application.\nVoulez-vous continuer ?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (DR == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void _Notification_DoubleClick(object sender, EventArgs e)
        {
            _tmrTimeLeft2.Enabled = true;
            this.ouvrirToolStripMenuItem.Enabled = false;
            // Afficher la fenêtre principale
            MainForm.Show();
        }


        private void toggleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings.Default.Enabled = !Settings.Default.Enabled;
            Settings.Default.Save();

            if (e != EventArgs.Empty)
                MainForm._btnToggle_Click(null, EventArgs.Empty);

            if (Settings.Default.Enabled)
            {
                toggleToolStripMenuItem.Text = "ON | Désactiver toutes les actions";
                toggleToolStripMenuItem.Image = Properties.Resources.Led_Green;
            }
            else
            {
                toggleToolStripMenuItem.Text = "OFF | Activer toutes les actions";
                toggleToolStripMenuItem.Image = Properties.Resources.Led_Red;
            }
            MainForm.UpdateEnableControls(true);
            CheckDates = true;
        }

        private void _NotifMenu_Opened(object sender, EventArgs e)
        {
            _tmrTimeLeft.Enabled = true;

            // Mise à jour sélection des settings
            string NameTemp = "";
            short i = 1;

            while (i <= (short)Settings.Default.NumberSettings)
            {
                NameTemp = (string)(Settings.Default[("SettingsName" + i)]);

                var ControlTemp1 = (ToolStripMenuItem)_NotifMenu.Items.Find(("_tsmiActions" + i), true).FirstOrDefault();
                ControlTemp1.Text = NameTemp;
                ControlTemp1.Enabled = true;
                ControlTemp1.Checked = false;

                i++;
            }

            NameTemp = "";
            i = 1;
            while (i <= (short)Settings.Default.NumberSettings)
            {
                NameTemp = (string)(Settings.Default[("SettingsName" + i)]);

                var ControlTemp = (ToolStripMenuItem)_NotifMenu.Items.Find(("tsmiAction" + i), true).FirstOrDefault();
                ControlTemp.Text = NameTemp;
                ControlTemp.Enabled = true;
                ControlTemp.Checked = (bool)(Settings.Default[("Enabled" + i)]);

                i++;
            }
        }

        private void _NotifMenu_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            _tmrTimeLeft.Enabled = false;
        }

        private void _tmrTimeLeft_Tick(object sender, EventArgs e)
        {   // Timer ContextMenu Notif
            short IndexTemp;
            DateTime NextActionDate = GetTheNextDate(ActionsDates, out IndexTemp);
            getDeltaTime(NextActionDate, DateTime.Now, IndexTemp);
            TimeLefttoolStripMenuItem.Text = NotifMSG1;
            DatetoolStripMenuItem2.Text = NotifMSG2;
            NametoolStripMenuItem5.Text = NotifMSG3;

            if (NotifMSG1 != "-")
                TimeLefttoolStripMenuItem.Image = Properties.Resources.Timer;
            else
                TimeLefttoolStripMenuItem.Image = null;

        }
        private void _tmrTimeLeft2_Tick(object sender, EventArgs e)
        {
            if (ContinueCheck < 10)
            {
                short Index = 0;
                DateTime NextActionDate = GetTheNextDate(ActionsDates, out Index);
                getDeltaTime(NextActionDate, DateTime.Now, Index);

                if (Settings.Default.NumberSettings != 0)
                {
                    string MSG_Time = "-";
                    string MSG_Date = "-";
                    string MSG_Name = "-";

                    if (Settings.Default.Enabled)
                    {
                        DateTime Temp = frmLoad.GetActionsDates[(Index - 1)];
                        TimeSpan DeltaTemp = Temp - DateTime.Now;
                        if (DeltaTemp.TotalSeconds > 0)
                        {
                            MSG_Name = (string)Settings.Default[("SettingsName" + Index)];
                            int Delta = (int)DeltaTemp.TotalSeconds;
                            MSG_Time = string.Format("{0}J {1}H {2}min {3}sec", DeltaTemp.Days, DeltaTemp.Hours, DeltaTemp.Minutes, DeltaTemp.Seconds);
                            MSG_Date = frmLoad.GetActionsDates[(Index - 1)].ToString();

                            if ((DeltaTemp.TotalMinutes <= 10) && (TimerNotifFlag))
                            {
                                _Notification.ShowBalloonTip(5, _Notification.BalloonTipTitle, string.Format("L'action \"{0}\" s'effectuera dans {1} minute(s) {2} seconde(s)", MSG_Name, DeltaTemp.Minutes, DeltaTemp.Seconds), _Notification.BalloonTipIcon);
                                TimerNotifFlag = false;
                            }
                        }
                        else
                        {
                            CheckForAction();
                        }
                        

                        MainForm.Text = "Advanced Actions | " + " (" + MSG_Name + ") " + MSG_Time;

                        DeltaTemp = frmLoad.GetActionsDates[(Settings.Default.ActiveSettingsNumber - 1)] - DateTime.Now;
                        MSG_Time = "-";
                        MSG_Date = "-";
                        if (DeltaTemp.TotalSeconds > 0)
                        {
                            int Delta = (int)DeltaTemp.TotalSeconds;
                            MSG_Time = string.Format("{0}J {1}H {2}min {3}sec", DeltaTemp.Days, DeltaTemp.Hours, DeltaTemp.Minutes, DeltaTemp.Seconds);
                            MSG_Date = frmLoad.GetActionsDates[(Settings.Default.ActiveSettingsNumber - 1)].ToString();
                        }
                        MSG_Name = (string)Settings.Default[("SettingsName" + Settings.Default.ActiveSettingsNumber)];
                        MainForm._txtActiveSettings.Text = "Sauvegarde : " + MSG_Name + " | Prochaine activation dans : " + MSG_Time + " | Le : " + MSG_Date;
                    }
                    else
                    {
                        MSG_Name = "-";
                        ContinueCheck++;
                        MainForm.Text = "Advanced Actions | " + " (" + MSG_Name + ") " + MSG_Time;

                    }
                }
                else
                {
                    MainForm._txtActiveSettings.Text = "Aucune sauvegarde sélectionnée";
                    MainForm.Text = "Advanced Actions";
                    ContinueCheck++;
                    Console.WriteLine("[ContinueCheck incremented] :" + ContinueCheck);
                    GetNextDates(ActionsDates);
                }
                if (FlagExecuteAction)
                {
                    ExecuteAction(FlagExecuteActionIndex,true);
                    FlagExecuteAction = false;
                    FlagExecuteActionIndex = -1;
                }
            }
        }

        private void _NotifMenu_Opening(object sender, CancelEventArgs e)
        {
            short IndexTemp;
            DateTime NextActionDate = GetTheNextDate(ActionsDates, out IndexTemp);
            getDeltaTime(NextActionDate, DateTime.Now, IndexTemp);
            if (NotifMSG1 != "-")
                TimeLefttoolStripMenuItem.Image = Properties.Resources.Timer;
            else
                TimeLefttoolStripMenuItem.Image = null;

            TimeLefttoolStripMenuItem.Text = NotifMSG1;
            DatetoolStripMenuItem2.Text = NotifMSG2;
            NametoolStripMenuItem5.Text = NotifMSG3;
            if (Settings.Default.Enabled)
            {
                toggleToolStripMenuItem.Text = "ON | Désactiver toutes les actions";
                toggleToolStripMenuItem.Image = Properties.Resources.Led_Green;
            }
            else
            {
                toggleToolStripMenuItem.Text = "OFF | Activer toutes les actions";
                toggleToolStripMenuItem.Image = Properties.Resources.Led_Red;
            }
            MainForm.UpdateEnableControls(true);
        }

        private void prochaineActionÀToolStripMenuItem_MouseDown(object sender, MouseEventArgs e)
        {
            CloseContextMenu = false;
        }

        private void _NotifMenu_Closing(object sender, ToolStripDropDownClosingEventArgs e)
        {
            e.Cancel = !CloseContextMenu;
            CloseContextMenu = true;
        }

        private void _tsmiActions_CheckedChanged(object sender, EventArgs e)
        {
            short i = 1;
            while (i <= (short)Settings.Default.NumberSettings)
            {

                var ControlTemp = (ToolStripMenuItem)_NotifMenu.Items.Find(("_tsmiActions" + i), true).FirstOrDefault();
                if (ControlTemp.Checked)
                {
                    ControlTemp.Checked = false;
                    FlagExecuteAction = true;
                    FlagExecuteActionIndex = i;
                }
                i++;
            }
        }

        private void TimerCheckDates_Tick(object sender, EventArgs e)
        {
            if (CheckDates0)
            {
                CheckForAction();
                CheckDates0 = false;
            }
        }

        private void tsmiAction1_CheckedChanged(object sender, EventArgs e)
        {
            if (CCLoad)
            {
                short i = 1;
                while (i <= (short)Settings.Default.NumberSettings)
                {
                    var ControlTemp = (ToolStripMenuItem)_NotifMenu.Items.Find(("tsmiAction" + i), true).FirstOrDefault();
                    Settings.Default[("Enabled" + i)] = ControlTemp.Checked;
                    i++;
                }
                CheckDates0 = true;
                Settings.Default.Save();
            }
        }

        private void tsmiActions_DropDownOpened(object sender, EventArgs e)
        {
            CCLoad = true;
            CCPrin = false;
        }

        private void _tmrUpdate_Tick(object sender, EventArgs e)
        {
            if (frmLoad.CCPrin)
            {
                Settings.Default.Reload();
                string NameTemp = "";
                short j = 1;
                while (j <= (short)Settings.Default.NumberSettings)
                {
                    NameTemp = (string)(Settings.Default[("SettingsName" + j)]);

                    var ControlTemp = (ToolStripMenuItem)_NotifMenu.Items.Find(("tsmiAction" + j), true).FirstOrDefault();
                    ControlTemp.Text = NameTemp;
                    ControlTemp.Enabled = true;
                    ControlTemp.Checked = (bool)(Settings.Default[("Enabled" + j)]);

                    j++;
                }
            }
        }
    }
}