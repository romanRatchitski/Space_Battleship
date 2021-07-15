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
    class Alien_E
    {
        const int ES = 30;

        SoundManager SM = new SoundManager();

        private PictureBox Alien_pic;
        private PictureBox Player = new PictureBox();
        private PictureBox Space = new PictureBox();
        private Point Alien_Loc;

        public Thread Trd;
        private int Movement_Speed;
        private int Alien_health;
        private int Ctr = 0;
        private int x, z, B, M, J, X, Random, Bounds;

        private Boolean Music_On;
        private Boolean DirFlag;
        public Boolean Explosion_Event;
        public Boolean Fire;
        public Boolean IsAlive;
        public Boolean IsHit;
        private Boolean Tmr_Enabled;
        //Label LB = new Label();
        
        private Form F = new Form();
        private Explosion Exp;
        private Al_Missile Missile1;
        private Al_Missile Missile2;

        private Random Rnd = new Random();

        PictureBox[] Pl_Bullets = new PictureBox[10];
        PictureBox[] Bullets = new PictureBox[ES];
        Boolean[] On_Move = new Boolean[ES];

        delegate void setLocationCallBack_M(int Sides);
    
        public Alien_E(int bounds, Point pos ,Form f, PictureBox space, PictureBox player, PictureBox[] blts)
        {
            this.Player = player;
            this.Alien_Loc = pos;
            this.Alien_health = 1000;
            this.Movement_Speed = 40;
            this.Bounds = bounds;
            this.Pl_Bullets = blts;
            this.Explosion_Event = false;
            this.DirFlag = false;
            this.Fire = true;
            this.IsAlive = true;
            this.IsHit = false;
            this.Music_On = false;
            this.Tmr_Enabled = true;
            this.Space = space;
            this.F = f;
            this.Random = Rnd.Next((Bounds / 100), (Bounds / 10));    //15,40

            if (Random <= 10)
                Random += 20;

            Alien_pic = new PictureBox();
            Alien_pic.Image = global::Space_Battleships.Properties.Resources.Mother_ship;
            Alien_pic.SizeMode = PictureBoxSizeMode.Normal;
            Alien_pic.SetBounds(Alien_Loc.X, Alien_Loc.Y, 480, 460);
            Space.Controls.Add(Alien_pic);
            Alien_pic.SendToBack();
            Alien_pic.BackColor = Color.Transparent;

            Alien_pic.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button1_MouseDown);

           /*
            F.Controls.Add(LB);
            LB.Size = new System.Drawing.Size(100, 100);
            LB.Font = new Font("Garamond", 18, FontStyle.Bold);
            LB.BackColor = Color.Red;
            LB.BringToFront();
            */

            for (z = 0; z < ES; z++)
            {
                On_Move[z] = false;
                Bullets[z] = new PictureBox();
                Bullets[z].Image = global::Space_Battleships.Properties.Resources.A_Bullet;
                Bullets[z].SizeMode = PictureBoxSizeMode.Normal;
            }

            
            Trd = new Thread(new ThreadStart(UFO_E_Move));
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
            if (B < ES - 3)
            {
                Space.Controls.Add(Bullets[B]);
                Bullets[B].SetBounds(Alien_pic.Location.X + 30, Alien_pic.Location.Y + 230, 10, 15);
                Bullets[B].BringToFront();
                Bullets[B].BackColor = Color.Transparent;
                Bullets[B].Show();
                On_Move[B] = true;
                
                Space.Controls.Add(Bullets[B + 1]);
                Bullets[B + 1].SetBounds(Alien_pic.Location.X + 460, Alien_pic.Location.Y + 230, 10, 15);
                Bullets[B + 1].BringToFront();
                Bullets[B + 1].BackColor = Color.Transparent;
                Bullets[B + 1].Show();
                On_Move[B + 1] = true;

                Space.Controls.Add(Bullets[B + 2]);
                Bullets[B + 2].SetBounds(Alien_pic.Location.X + 240, Alien_pic.Location.Y + 500, 10, 15);
                Bullets[B + 2].BringToFront();
                Bullets[B + 2].BackColor = Color.Transparent;
                Bullets[B + 2].Show();
                On_Move[B + 2] = true;
                B += 3;            
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
            for (x = 0; x < ES; x++)
            {
                Bullets[x].Dispose();
            }
        }


        private void VorteX(Form form)
        {
            var Original = form.Location;
            var Rnd = new Random();
            const int Vortex_Amplitude = 80;

            for (int X = 0; X < 20; X++)
            {
                form.Location = new Point(Original.X + Rnd.Next(-Vortex_Amplitude, Vortex_Amplitude), Original.Y + Rnd.Next(-Vortex_Amplitude, Vortex_Amplitude));
                System.Threading.Thread.Sleep(10);
            }
            form.Location = Original;
        }



        private void Kill()
        {
            IsAlive = false;
            Fire = false;
            this.Alien_pic.SendToBack();
            Space.Controls.Remove(this.Alien_pic);
            
            Point pos = new Point(Alien_pic.Location.X, Alien_pic.Location.Y + 500);
            Exp = new Explosion(0, pos, Space, 5, 50);
            pos = new Point(Alien_pic.Location.X + 150, Alien_pic.Location.Y + 500);
            Exp = new Explosion(0, pos, Space, 350, 50);
            pos = new Point(Alien_pic.Location.X + 250, Alien_pic.Location.Y + 500);
            Exp = new Explosion(0, pos, Space, 650, 50);
            pos = new Point(Alien_pic.Location.X + 200, Alien_pic.Location.Y + 600);
            Exp = new Explosion(0, pos, Space, 750, 50);
            pos = new Point(Alien_pic.Location.X + 300, Alien_pic.Location.Y + 550);
            Exp = new Explosion(0, pos, Space, 1050, 50);
            
            Alien_pic.Dispose();
            Stop_Shoting();
            Tmr_Enabled = false;
            Trd.Abort();
            Dispose();
            //VorteX(F);
        }

        private void TMR1()
        {
            System.Windows.Forms.Timer Timer1 = new System.Windows.Forms.Timer();
            Timer1.Interval = 10;
            Timer1.Tick += timerTick1;
            Timer1.Enabled = Tmr_Enabled;
            Timer1.Start();

            if (Tmr_Enabled == false)
                Timer1.Stop();
        }

        private void timerTick1(object sender, EventArgs e)
        {
          //  LB.Text = "H = " + Damage;

            if (Alien_pic.Location.X < 1600 && Music_On == false)
            {
                Music_On = true;     
                Level Lv = (Level)F;
                Lv.MotherShip_MusicControl();
            }


            if (IsHit == true && IsAlive == true)
            {
                IsHit = false;
                Alien_health -= 2;          //-2

                if (Alien_health <= 0)
                {
                    Kill();
                }
            }

            if (Player.Visible == false)
                this.Stop_Shoting();

            if (Explosion_Event == true && IsAlive == true)
            {
                Explosion_Event = false;
                Alien_health -= 150;        //-150
                IsHit = true;

                try
                {
                    Missile1.Explosion_Event = true;
                    Missile2.Explosion_Event = true;
                }

                catch (NullReferenceException) { }
            }

            for (x = 0; x < 10; x++)
            {
                if (IsTouching(this.Alien_pic, Pl_Bullets[x]) && this.IsAlive == true)
                {
                    this.IsHit = true;
                    Pl_Bullets[x].Hide();
                }
            }

            for (M = 0; M < ES; M++)
            {
                if (On_Move[M] == true)
                {
                    Bullets[M].Top += 45;

                    if (Bullets[M].Location.Y >= 900)
                    {
                        On_Move[M] = false;
                        Bullets[M].Hide();
                    }
                }
            }

            for (J = 0; J < ES; J++)
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
                Kill();

                if (F is Level)
                {
                    Level L = (Level)F;
                    L.Kill();
                }
            }
            //Fire!!
            X++;
            if (X == 15 && Fire == true)
            {
                Gun_Visual();
                Ctr++;
                X = 0;

                if (Ctr == Random)
                {
                    Point Pos = new Point(Alien_pic.Location.X + 100, Alien_pic.Location.Y + 350);
                    Missile1 = new Al_Missile(F, 70, Pos, Space, Player, Pl_Bullets);
                    Missile1.Missile_pic.BringToFront();

                    Pos = new Point(Alien_pic.Location.X + 400, Alien_pic.Location.Y + 350);
                    Missile2 = new Al_Missile(F, 40, Pos, Space, Player, Pl_Bullets);
                    Missile2.Missile_pic.BringToFront();
                    
                    Ctr = 0;
                }
            }

            if (F.BackColor == Color.Black)
            {
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

        private void UFO_E_Move()
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
                {
                    DirFlag = true;
                }

                if (Alien_pic.Location.X == Bounds + 900)       
                {
                    DirFlag = false;
                }
            }
        }
    }
}
