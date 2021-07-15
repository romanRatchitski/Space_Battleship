using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Media;
using System.Timers;


namespace Space_Battleships
{
    class Al_Missile
    {

        const int M = 10;
        delegate void setLocationCallBack(int Down);
      
        public PictureBox Missile_pic;
        PictureBox Player = new PictureBox();
        PictureBox[] Pl_Bullets = new PictureBox[10];
        PictureBox Space = new PictureBox();

        public Boolean Explosion_Event;
        private int Missile_speed;
        private int Missile_health;
        private Boolean Tmr_Enabled;
        public Boolean IsHit;
        public Boolean IsAlive;
        public Form F = new Form();
        public Thread Trd;
        Point Missile_Loc; //location
        double L=0 , R=0 , Engle=0;
        
        int x, J;
        int time = 25;
        int Action = 1;

        public Al_Missile(Form F, int speed, Point Zero, PictureBox space, PictureBox player, PictureBox[] pl_bullets)
        {
            this.F = F;
            this.Missile_speed = speed;
            this.Missile_Loc = Zero;
            this.Space = space;
            this.Player = player;
            this.Pl_Bullets = pl_bullets;
            this.Missile_health = 100;
            this.Tmr_Enabled = true;
            this.IsAlive = true;
            this.Explosion_Event = false;

            Missile_pic = new PictureBox();
            Missile_pic.Image = global::Space_Battleships.Properties.Resources.al_missle;
            Missile_pic.SizeMode = PictureBoxSizeMode.Normal;
            Missile_pic.SetBounds(Missile_Loc.X - 30, Missile_Loc.Y - 20, 40, 50);
            Space.Controls.Add(Missile_pic);
            Missile_pic.BackColor = Color.Transparent;
            //Missile_pic.BringToFront();

            Missile_pic.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button1_MouseDown);

            Trd = new Thread(new ThreadStart(Move));
            Trd.Start();

            TMR1();
            TMR2();
        }

        private void button1_MouseDown(object sender, MouseEventArgs mea)
        {
            if (F is Level)
            {
                Level Lv = (Level)F;

                if (Missile_pic.Location.X > Player.Location.X - 80 && Missile_pic.Location.X < Player.Location.X + 80)
                {
                    if (Lv.Armed_Arms == false)
                        Lv.GunVisual_BasicKit();

                    if (Lv.Armed_Arms == true)
                        Lv.GunVisual_ArmedKit();
                }
            }
        }

        private void Kill()
        {
            IsAlive = false;
            this.Missile_pic.SendToBack();
            Space.Controls.Remove(this.Missile_pic);
            Point pos = new Point(Missile_pic.Location.X, Missile_pic.Location.Y + 385);
            Explosion Exp = new Explosion(3, pos, Space, 5, 50);
            Missile_pic.Dispose();
            Trd.Abort();           
        }

        private void VorteX(Form form)
        {
            var Original = form.Location;
            var Rnd = new Random();
            const int Vortex_Amplitude = 40;

            for (int X = 0; X < 20; X++)
            {
                form.Location = new Point(Original.X + Rnd.Next(-Vortex_Amplitude, Vortex_Amplitude), Original.Y + Rnd.Next(-Vortex_Amplitude, Vortex_Amplitude));
                System.Threading.Thread.Sleep(10);
            }
            form.Location = Original;
        }

        
        private void TMR1()
        {
            System.Windows.Forms.Timer Timer1 = new System.Windows.Forms.Timer();
            Timer1.Interval = time;
            Timer1.Tick += timerTick1;
            Timer1.Enabled = Tmr_Enabled;
            Timer1.Start();

            if (Tmr_Enabled == false)
                Timer1.Stop();
        }

        private void timerTick1(object sender, EventArgs e)
        {
            if (F.BackColor == Color.Black)
            {
                this.Trd.Abort();
                Tmr_Enabled = false;
            }
            
            if (Action == 1)
            {
                if (IsHit == true && IsAlive == true)
                {
                    IsHit = false;
                    Missile_health -= 15;

                    if (Missile_health <= 0)
                    {
                        Action = 2;
                        time = 50;
                        Kill();
                    }

                }
                //Explosion_Event
                if (Explosion_Event == true)
                {
                    Explosion_Event = false; 
                    Kill();
                }

                for (x = 0; x < 10; x++)
                {
                    if (IsTouching(this.Missile_pic, Pl_Bullets[x]) && this.IsAlive == true)
                    {
                        this.IsHit = true;
                        Pl_Bullets[x].Hide();
                    }
                }

                for (J = 0; J < M; J++)
                {
                    if (IsTouching(Player, this.Missile_pic) && IsAlive == true && Player.Visible==true)
                    {
                        Action = 2;
                        time=50;
                        Kill();
                       
                        Level Lv = (Level)F;
                        Lv.MissileHit = true;                        
                    }
                }

                ((System.Windows.Forms.Timer)sender).Stop();
                TMR1(); 
            }

            else if (Action == 2)
            {
                VorteX(F);
                Tmr_Enabled = false;
                ((System.Windows.Forms.Timer)sender).Stop();
            }                         
        }

        private void TMR2()
        {
            System.Windows.Forms.Timer Timer2 = new System.Windows.Forms.Timer();
            Timer2.Interval = 20;
            Timer2.Tick += timerTick2;
            Timer2.Enabled = Tmr_Enabled;
            Timer2.Start();

            if (Tmr_Enabled == false)
                Timer2.Stop();        
        }

        private void timerTick2(object sender, EventArgs e)
        {
            Engle = (Player.Location.X + 30) - Missile_pic.Location.X;
            Engle /= 10;

            if (Engle > 0)
            {
                L = Engle;
                R = 0;
            }

            else if (Engle < 0)
            {
                R = Engle;
                L = 0;
            }     
           

            ((System.Windows.Forms.Timer)sender).Stop();
            TMR2();
        }



        private bool IsTouching(PictureBox p1, PictureBox p2)
        {
            if (p1.Location.X + p1.Width < p2.Location.X)
                return false;
            if (p2.Location.X + p2.Width < p1.Location.X)
                return false;
            if (p1.Location.Y + p1.Height < p2.Location.Y)
                return false;
            if (p2.Location.Y + p2.Height < p1.Location.Y)
                return false;
            return true;
        }



        private void Move()
        {
            while (true)
            {
                setLocation(5);
                Thread.Sleep(Missile_speed);
            }
        }

        private void setLocation(int Down)
        {
            if (F.InvokeRequired)
            {
                try
                {
                    setLocationCallBack d = new setLocationCallBack(setLocation);
                    F.Invoke(d, new object[] { Down });
                }

                catch (ObjectDisposedException)
                {

                }
            }

            else
            {
                Missile_pic.Top += Down;
                
                Missile_pic.Left += ((int)L);
                Missile_pic.Left += ((int)R);

                if (Missile_pic.Location.Y == 900)
                {
                    IsAlive = false;
                    Missile_pic.Dispose();
                    Tmr_Enabled = false;
                    Trd.Abort();
                }
            
            
            }
        }





    
    }
}
