using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Threading;
using System.Windows.Forms;
using System.Threading;
using System.Media;



namespace Space_Battleships
{
    class Explosion
    {
        int Explosion_ID;
        int Interval1;
        int Interval2;
        Point Explosion_Loc;
        public PictureBox Exp_pic;
        PictureBox Space = new PictureBox();
        SoundManager SM = new SoundManager();
        Boolean Tmr_Enabled = true;

        public Explosion(int id, Point Pos, PictureBox space, int T1, int T2)
        {
            this.Explosion_ID = id;
            this.Interval1 = T1;
            this.Interval2 = T2;
            this.Explosion_Loc = Pos;
            this.Space = space;
            
            Exp_pic = new PictureBox();
            Set_Explosion(Explosion_ID);
            Exp_pic.SizeMode = PictureBoxSizeMode.Normal;          
            Space.Controls.Add(Exp_pic);
            Exp_pic.BackColor = Color.Transparent;
          //  Exp_pic.BringToFront();

            TMR1();                     
        }

        private void Set_Explosion(int ID)
        {
            switch (ID)
            {
                case 0:
                Exp_pic.Image = global::Space_Battleships.Properties.Resources.Alien_explosion;
                Exp_pic.SetBounds(Explosion_Loc.X - 100, Explosion_Loc.Y - 420, 240, 220);     //240,220          
                break;

                case 1:
                Exp_pic.Image = global::Space_Battleships.Properties.Resources.player_explosion;
                Exp_pic.SetBounds(Explosion_Loc.X - 100, Explosion_Loc.Y - 420, 240, 200);
                break;

                case 2:
                Exp_pic.Image = global::Space_Battleships.Properties.Resources.Missle_Explosion;
                Exp_pic.SetBounds(Explosion_Loc.X - 160, Explosion_Loc.Y - 110, 240, 220);  
                Exp_pic.BringToFront();
                break;
                  
                case 3:
                Exp_pic.Image = global::Space_Battleships.Properties.Resources.Al_MissileExplostion;
                Exp_pic.SetBounds(Explosion_Loc.X - 100, Explosion_Loc.Y - 500, 200, 200);
                break;
            }
        }

      
        private void TMR1()
        {
            System.Windows.Forms.Timer Timer1 = new System.Windows.Forms.Timer();
            Timer1.Interval = Interval1;
            Timer1.Tick += timerTick1;
            Exp_pic.Visible = false;
            Timer1.Enabled = true;
            Timer1.Start();

            if (Tmr_Enabled == false)
                Timer1.Stop();        
        }

       
        private void timerTick1(object sender, EventArgs e)
        {
            SM.Explosion_PlaySound();
            Exp_pic.Visible = true;
            ((System.Windows.Forms.Timer)sender).Stop();

            TMR2();
        }

        private void TMR2()
        {
            System.Windows.Forms.Timer Timer2 = new System.Windows.Forms.Timer();
            Timer2.Interval = Interval2;
            Timer2.Tick += timerTick2;
            Timer2.Enabled = true;
            Timer2.Start();

            if (Tmr_Enabled == false)
                Timer2.Stop();
        }

        private void timerTick2(object sender, EventArgs e)
        {
            Exp_pic.Dispose();
            Tmr_Enabled = false;
            ((System.Windows.Forms.Timer)sender).Stop();
        }



    }
}
