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
    public partial class VictoryFrm : Form
    {
        public VictoryFrm()
        {
            InitializeComponent();
        }

        private Boolean Tmr_Enabled = true;

        SoundManager SM = new SoundManager();
        MainManu MM = new MainManu();

        private void VictoryFrm_Load(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.URL = "Sounds/Space Shuttle Landing.mp4";
            SM.Victory_Music();
            SM.Tolking_PlaySound();

            TMR1();
        }

        private void TMR1()
        {
            System.Windows.Forms.Timer Timer1 = new System.Windows.Forms.Timer();
            Timer1.Interval = 85000;    //85000
            Timer1.Tick += timerTick1;
            Timer1.Enabled = true;
            Timer1.Start();

            if (Tmr_Enabled == false)
                Timer1.Stop();

        }

        private void timerTick1(object sender, EventArgs e)
        {
            
            MM.ReOpen();
            MM.Show();
            SM.StopPlayer2();
          
            this.Close();

            Tmr_Enabled = false;
            ((System.Windows.Forms.Timer)sender).Stop();
        }
    
    
    
    
    
    
    
    }
}
