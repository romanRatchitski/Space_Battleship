using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Space_Battleships
{
    public partial class MainManu : Form
    {
        public MainManu()
        {
            InitializeComponent();
        }

        static Boolean Tmr_Enabled = true;
        static Boolean HideFrm = false;
        static SoundManager SM = new SoundManager();
        static MainManu frm1 = new MainManu();

        private void MainManu_Load(object sender, EventArgs e)
        {
            SM.MainManu_PlayMusic();
            TMR1();
            
        }
        
        public void FrmSleep()
        {
            SM.StopPlayer1();
            HideFrm = true;
        }

        public void ReOpen()
        {
            HideFrm = false;
            Tmr_Enabled = true;
            TMR1();
        }

        private void btnStartGame_Click(object sender, EventArgs e)
        {
            frmLog_In LogIn1 = new frmLog_In();
            LogIn1.Show();
        }

        private void btnHighScores_Click(object sender, EventArgs e)
        {
            frmHigh_Scores HS1 = new frmHigh_Scores();
            HS1.Show();
        }


        private void ExitBTN_Click(object sender, EventArgs e)
        {
            SM.StopPlayer1();
            Tmr_Enabled = false;
            this.Dispose();
        }
       

        public MainManu SendStartFRM
        {
            get
            {
                return frm1;
            }
        }

       
        private void TMR1()
        {
            System.Windows.Forms.Timer Timer1 = new System.Windows.Forms.Timer();
            Timer1.Interval = 60;
            Timer1.Tick += timerTick1;
            Timer1.Enabled = true;
            Timer1.Start();

            if (Tmr_Enabled == false)
                Timer1.Stop();
        }


          private void timerTick1(object sender, EventArgs e)
          {
              if (HideFrm == true)
              {
                  this.Hide();
                  Tmr_Enabled = false;
              }

              ((System.Windows.Forms.Timer)sender).Stop();

              TMR1();
          }

        
    }
}
