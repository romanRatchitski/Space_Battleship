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
    class Alien_C
    {
        const int CS = 5;

        private PictureBox Alien_pic;
        private PictureBox Player = new PictureBox();
        private PictureBox Space = new PictureBox();
        private Point Alien_Loc;

        private Form F = new Form();
        public Thread Trd;
        private int Movement_Speed;
        private int Alien_health;
        private int x, z, B, M, J, X, Bounds;

        private Boolean DirFlag;
        public Boolean Explosion_Event;
        public Boolean Fire;
        public Boolean IsAlive;
        public Boolean IsHit;
        private Boolean Tmr_Enabled;

        private Explosion Exp;

        PictureBox[] Pl_Bullets = new PictureBox[10];
        PictureBox[] Bullets = new PictureBox[CS];
        Boolean[] On_Move = new Boolean[CS];

        delegate void setLocationCallBack_M(int Sides);

        public Alien_C(int bounds, Point pos, Form f, PictureBox space, PictureBox player, PictureBox[] blts)
        {
            this.Player = player;
            this.Alien_Loc = pos;
            this.Alien_health = 140;
            this.Movement_Speed = 32;
            this.Bounds = bounds;
            this.Pl_Bullets = blts;
            this.Explosion_Event = false;
            this.DirFlag = false;
            this.Fire = true;
            this.IsAlive = true;
            this.IsHit = false;
            this.Tmr_Enabled = true;
            this.Space = space;
            this.F = f;


            Alien_pic = new PictureBox();
            Alien_pic.Image = global::Space_Battleships.Properties.Resources.Alien_C;
            Alien_pic.SizeMode = PictureBoxSizeMode.Normal;
            Alien_pic.SetBounds(Alien_Loc.X, Alien_Loc.Y, 160, 100);
            Space.Controls.Add(Alien_pic);
            Alien_pic.BringToFront();
            Alien_pic.BackColor = Color.Transparent;

            Alien_pic.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button1_MouseDown);

            for (z = 0; z < CS; z++)
            {
                On_Move[z] = false;
                Bullets[z] = new PictureBox();
                Bullets[z].Image = global::Space_Battleships.Properties.Resources.A_Bullet;
                Bullets[z].SizeMode = PictureBoxSizeMode.Normal;
            }

            Trd = new Thread(new ThreadStart(UFO_C_Move));
            Trd.Start();

            TMR1();
        }

        private void button1_MouseDown(object sender, MouseEventArgs mea)
        {
            if (Player.Visible == true)
            {
                Level Lv = (Level)F;

                if (Lv.Armed_Arms == false)
                    Lv.GunVisual_BasicKit();

                if (Lv.Armed_Arms == true)
                    Lv.GunVisual_ArmedKit();
            }
        }


        public PictureBox Get_Pic()
        {
            return this.Alien_pic;
        }


        public void Gun_Visual()
        {
            if (B < CS)
            {
                Space.Controls.Add(Bullets[B]);
                Bullets[B].SetBounds(Alien_pic.Location.X + 68, Alien_pic.Location.Y + 90, 10, 15);
                Bullets[B].BringToFront();
                Bullets[B].BackColor = Color.Transparent;
               
                try { Bullets[B].Show(); }
                catch (Exception) { }
                
                On_Move[B] = true;
                B++;
            }

            else
            {
                B = 0;
            }

        }

        public void Stop_Shoting()
        {
            this.Fire = false;
        }

        private void Dispose()
        {
            for (x = 0; x < CS; x++)
            {
                Bullets[x].Dispose();
            }
        }


        private void Kill()
        {
            IsAlive = false;
            Fire = false;
            this.Alien_pic.SendToBack();
            Space.Controls.Remove(this.Alien_pic);
            Point pos = new Point(Alien_pic.Location.X, Alien_pic.Location.Y + 385);
            Exp = new Explosion(0, pos, Space, 5, 50);
            Alien_pic.Dispose();
            Stop_Shoting();
            Tmr_Enabled = false;
            Trd.Abort();
            Dispose();
        }

       
        private void TMR1()
        {
            System.Windows.Forms.Timer Timer1 = new System.Windows.Forms.Timer();
            Timer1.Interval = 25;
            Timer1.Tick += timerTick1;
            Timer1.Enabled = Tmr_Enabled;
            Timer1.Start();

            if (Tmr_Enabled == false)
                Timer1.Stop();       
        }


        private void timerTick1(object sender, EventArgs e)
        {
            if (IsHit == true && IsAlive == true)
            {
                IsHit = false;
                Alien_health -= 6;

                if (Alien_health <= 0)
                {
                    Kill();
                }
            }

            if (Player.Visible == false)
                this.Stop_Shoting();


            if (Explosion_Event == true && IsAlive == true)
            {
                Kill();
            }

            if (Explosion_Event == true && IsAlive == true)
            {
                Explosion_Event = false;
                Alien_health -= 150;
                IsHit = true;
            }

            for (x = 0; x < 10; x++)
            {
                if (IsTouching(this.Alien_pic, Pl_Bullets[x]) && this.IsAlive == true)
                {
                    this.IsHit = true;
                    Pl_Bullets[x].Hide();
                }
            }

            for (M = 0; M < CS; M++)
            {
                if (On_Move[M] == true)
                {
                    Bullets[M].Top += 60;

                    if (Bullets[M].Location.Y >= 900)
                    {
                        On_Move[M] = false;
                        Bullets[M].Hide();
                    }
                }
            }

            for (J = 0; J < CS; J++)
            {
                if (IsTouching(Player, Bullets[J]))
                {
                    Bullets[J].Hide();

                    Level Lv = (Level)F;
                    Lv.IsHit = true;
                }
            }

            if (IsTouching(Alien_pic, Player) && IsAlive == true && Player.Visible)
            {
                this.Alien_pic.SendToBack();
                Level Lv = (Level)F;

                if (Lv.End == false)
                {
                    Lv.Kill();
                    Kill();
                }
            }
                                        //Fire..
            X++;
            if (X == 15 && Fire == true)
            {
                Gun_Visual();
                X = 0;
            }

            if (F.BackColor == Color.Black)
            {
                Dispose();
                this.Trd.Abort();
                Tmr_Enabled = false;
            }

            ((System.Windows.Forms.Timer)sender).Stop();

            TMR1();
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

        private void UFO_C_Move()
        {
            while (true)
            {
                setLocation_M(5);
                Thread.Sleep(Movement_Speed);
            }
        }

        private void setLocation_M(int Sides)
        {
            if (F.InvokeRequired)
            {
                try
                {
                    setLocationCallBack_M d = new setLocationCallBack_M(setLocation_M);
                    F.Invoke(d, new object[] { Sides });
                }

                catch (ObjectDisposedException)
                {

                }
            }

            else
            {
                if (DirFlag == false)
                    Alien_pic.Left -= Sides;

                if (DirFlag == true)
                    Alien_pic.Left += Sides;

                if (Alien_pic.Location.X == Bounds)
                    DirFlag = true;

                if (Alien_pic.Location.X == Bounds + 150)
                    DirFlag = false;

            }

        }
    }

}



