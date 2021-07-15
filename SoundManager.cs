using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;

namespace Space_Battleships
{
    class SoundManager
    {
        WMPLib.WindowsMediaPlayer Player1 = new WMPLib.WindowsMediaPlayer();
        WMPLib.WindowsMediaPlayer Player2 = new WMPLib.WindowsMediaPlayer();
        WMPLib.WindowsMediaPlayer Player3 = new WMPLib.WindowsMediaPlayer();
        WMPLib.WindowsMediaPlayer Player4 = new WMPLib.WindowsMediaPlayer();

        SoundPlayer sp1 = new SoundPlayer(Space_Battleships.Properties.Resources.Machine_Gun);
     
        public SoundManager()
        {                                     //Constrctor
        
         }

        public void MainManu_PlayMusic()
        {
            Player1.URL = "Sounds/Manu_Music.mp3";
            Player1.controls.play();
            Player1.settings.volume = 100;
            Player1.settings.setMode("loop", true);
        }

        public void Level1_PlayMusic()
        {
            Player1.URL = "Sounds/L1_Music.mp3";
            Player1.controls.play();
            Player1.settings.volume = 100;
            Player1.settings.setMode("loop", true);
        }

        public void Level2_PlayMusic()
        {
            Player1.URL = "Sounds/L2_Music.mp3";
            Player1.controls.play();
            Player1.settings.volume = 100;
            Player1.settings.setMode("loop", true);
        }

        public void Level3_PlayMusic()
        {
            Player1.URL = "Sounds/L3_Music.mp3";
            Player1.controls.play();
            Player1.settings.volume = 100;
            Player1.settings.setMode("loop", true);
        }

        public void Final_AttackFull()
        {
            Player1.URL = "Sounds/Final_AttackFull.mp3";            
            Player1.controls.play();
            Player1.settings.volume = 100;
            Player1.settings.setMode("loop", true);                
        }

        public void Final_AttackHalf()
        {
            Player1.URL = "Sounds/Final_AttackHalf.mp3";
            Player1.controls.play();
            Player1.settings.volume = 100;
            Player1.settings.setMode("loop", true);
        }

        public void Victory_Music()
        {
            Player1.URL = "Sounds/End_Music.mp3";
            Player1.controls.play();
            Player1.settings.volume = 100;
            Player1.settings.setMode("loop", false);
        }

        public void Space_WeaponsStation_PlayMusic()
        {
            Player1.URL = "Sounds/WeaponsStation_Music.mp3";
            Player1.controls.play();
            Player1.settings.volume = 80;
            Player1.settings.setMode("loop", true);
        }

        public void MachineGun_PlaySound()
        {            
            sp1.Play();
        }

        public void Tolking_PlaySound()
        {
            Player2.URL = "Sounds/Tolking.mp3";
            Player2.controls.play();
            Player2.settings.volume = 100;
            Player2.settings.setMode("loop", true);
        }

        public void MissileLaunch_PlaySound()
        {
            Player2.URL = "Sounds/Missle Launch.wav";
            Player2.controls.play();
            Player2.settings.volume = 70;
            Player2.settings.setMode("loop", false);
        }

        public void Explosion_PlaySound()
        {
           Player3.URL = "Sounds/Explosion.wav";
           Player3.controls.play();
           Player3.settings.volume = 90;
           Player3.settings.setMode("loop", false);
           TMR1();
        }

        public void DangerAlarm_PlaySound()
        {
            Player4.URL = "Sounds/Y_R_In_Danger.wav";
            Player4.controls.play();
            Player4.settings.volume = 70;
            Player4.settings.setMode("loop", true);
        }

        public void StopPlayer1()
        {
            Player1.controls.stop();
        }

        public void StopPlayer2()
        {
            Player2.controls.stop();
        }

        public void StopPlayer3()
        {
            Player3.controls.stop();
        }

        public void StopPlayer4()
        {
            Player4.controls.stop();
        }


        private void TMR1()
        {
            System.Windows.Forms.Timer Timer1 = new System.Windows.Forms.Timer();
            Timer1.Interval = 4500;  //4500
            Timer1.Tick += timerTick1;
            Timer1.Enabled = true;
            Timer1.Start();
        }

        private void timerTick1(object sender, EventArgs e)
        {
            StopPlayer3();

            ((System.Windows.Forms.Timer)sender).Stop();
        }

    }
}
