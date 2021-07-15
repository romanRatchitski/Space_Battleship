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
    class Pl_Missile
    {

        const int M = 10;
        delegate void setLocationCallBack(int Up);

        PictureBox Space = new PictureBox(); 
        PictureBox Missile_pic;

        public Thread Trd;
        private Boolean Tmr_Enabled;
        public Boolean IsAlive = false;
        Form F = new Form();        
        Point Missile_Loc; 
        Object[] Enemies;
       
        int x;
        int time;
        int Action = 1;
        int Missile_speed;

        public Pl_Missile(Form F, int speed, Point Zero, PictureBox space, Object[] enemies)
        {
            this.Missile_speed = speed;
            this.Missile_Loc = Zero;
            this.Space = space;
            this.Enemies = enemies;
            this.Tmr_Enabled = true;
            this.IsAlive = true;
            this.time = 5000;
            this.F = F;

            Missile_pic = new PictureBox();
            Missile_pic.Image = global::Space_Battleships.Properties.Resources.Missle_Pic;
            Missile_pic.SizeMode = PictureBoxSizeMode.Normal;
            Missile_pic.SetBounds(Missile_Loc.X-30, Missile_Loc.Y-20, 60, 100);
            Space.Controls.Add(Missile_pic);
            //Missile_pic.BringToFront();
            Missile_pic.BackColor = Color.Transparent;

            Trd = new Thread(new ThreadStart(Move));
            Trd.Start();

            TMR1();
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
                Execute_Event();
                time = 100;
                Action = 2;
                
                ((System.Windows.Forms.Timer)sender).Stop();
                TMR1();
            }

            else if (Action == 2)
            {
                Missile_pic.Dispose();
                Point Pos = new Point(Missile_pic.Location.X, Missile_pic.Location.Y);
                Explosion Exp1 = new Explosion(2, Pos, Space , 1, 30);
                IsAlive = false;
                time = 100;
                Action = 3;
                
                ((System.Windows.Forms.Timer)sender).Stop();
                TMR1();
            }

            else if (Action == 3)
            {
                VorteX(F);
                Tmr_Enabled = false;
                Trd.Abort();
                
                ((System.Windows.Forms.Timer)sender).Stop();
            }
            
            ((System.Windows.Forms.Timer)sender).Stop();         
        }

        private void Execute_Event()
        {
            for (x = 0; x < Enemies.Length ; x++)
            {
                if (Enemies[x] is Alien_A)
                {
                    Alien_A hostile = (Alien_A)Enemies[x];

                    if (hostile.IsAlive && hostile.Get_Pic().Location.X < 1600)
                        hostile.Explosion_Event = true;
                }

                if (Enemies[x] is Alien_B)
                {
                    Alien_B hostile = (Alien_B)Enemies[x];

                    if (hostile.IsAlive && hostile.Get_Pic().Location.X < 1600)
                        hostile.Explosion_Event = true;
                }

                if (Enemies[x] is Alien_C)
                {
                    Alien_C hostile = (Alien_C)Enemies[x];

                    if (hostile.IsAlive && hostile.Get_Pic().Location.X < 1600)
                        hostile.Explosion_Event = true;
                }

                if (Enemies[x] is Alien_D)
                {
                    Alien_D hostile = (Alien_D)Enemies[x];

                    if (hostile.IsAlive && hostile.Get_Pic().Location.X < 1600)
                        hostile.Explosion_Event = true;
                }

                if (Enemies[x] is Alien_E)
                {
                    Alien_E hostile = (Alien_E)Enemies[x];

                    if (hostile.IsAlive && hostile.Get_Pic().Location.X < 1600)
                        hostile.Explosion_Event = true;
                }

            }
        }

        private void VorteX(Form form)
        {
            var Original = form.Location;
            var Rnd = new Random();
            const int Vortex_Amplitude = 80;

            for (int X = 0; X < 30; X++)
            {
                form.Location = new Point(Original.X + Rnd.Next(-Vortex_Amplitude, Vortex_Amplitude), Original.Y + Rnd.Next(-Vortex_Amplitude, Vortex_Amplitude));
                System.Threading.Thread.Sleep(20);
            }
            form.Location = Original;
        }

        private void Move()
        {
            while (true)
            {
                setLocation(5);
                Thread.Sleep(Missile_speed);
            }
        }

        private void setLocation(int Up)
        {
            if (F.InvokeRequired)
            {
                try
                {
                    setLocationCallBack d = new setLocationCallBack(setLocation);
                    F.Invoke(d, new object[] { Up });
                }

                catch (ObjectDisposedException)
                {

                }
            }

            else
            {
                Missile_pic.Top -= Up;
            }
        }
       
    
    }
}
