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
using System.Data.Sql;
using System.Data.SqlClient;


namespace Space_Battleships
{

    public partial class Level : Form
    {
        public Level()
        {
            InitializeComponent();
            KeyDown += new KeyEventHandler(Form1_KeyDown);
            Space.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button1_MouseDown);
        }
                                                          //System Internal Use

        const int PS = 10;                                                                     
        SoundManager SM = new SoundManager();
        DataBase_Manager DBM = new DataBase_Manager();
        Space_WeaponsStation SWS = new Space_WeaponsStation();
        frmLog_In LOG = new frmLog_In();
        MainManu MM = new MainManu();
        VictoryFrm VCF = new VictoryFrm();

        Object[] Enemies = new Object[PS];
        Boolean[] On_Move = new Boolean[PS];
        Boolean[] Is_Active = new Boolean[PS];
        public Boolean End = false;
        Boolean MotherShip_isAlive = false;

        PictureBox fire1 = new PictureBox();
        PictureBox fire2 = new PictureBox();
        PictureBox fire3 = new PictureBox();
        public PictureBox Space = new PictureBox();       
        
        Point Missile_Loc = new Point();
        int x, z, M, B, X, Y, CurrentLevel, Select = 0, Count;
        Boolean UControls = true , AlarmFlag = false;
        Pl_Missile Missile;
                                                                       //User Properties
        PictureBox[] Bullets = new PictureBox[PS];
        string UserName = "";                                                   
        int Missiles, Points;     
        float Armor;
        public Boolean Armed_Arms;
        Boolean R, L, U, D;                                               //Directions
        public Boolean IsHit = false, MissileHit = false;                 //Hits

        int Bullets_Fire = 0, Missile_Fire = 0, Missile_Launch, Read_time = 850;            //System Timing


        private void Form6_Load(object sender, EventArgs e)
        {
            TMR1.Enabled = true;
            TMR2.Enabled = true;

            DBM.Call_SystemDataBase();
            GetUser_Data();
            SalectLevel();
            //progressBar1.Value *= 3;
            
            User_Panel.Text = "Missiles: " + Missiles + "\nArmor: " + Armor + "\nPoints: " + Points;
            
            Space.Controls.Add(LvText);
            Space.Controls.Add(Player);
            Space.Controls.Add(User_Panel);

        
            User_Panel.BackColor = Color.Transparent;

            for (z = 0; z < PS; z++)
            {
                Is_Active[z] = true;
                On_Move[z] = false;
                Bullets[z] = new PictureBox();
                Bullets[z].Image = global::Space_Battleships.Properties.Resources.bullet;
                Bullets[z].SizeMode = PictureBoxSizeMode.Normal;
            }       
        }

        private void SalectLevel()
        {
            switch (CurrentLevel)
            {
                case 1:
                    this.Text = "Level 1";
                    SM.Level1_PlayMusic();
                    Get_LogName();
                    LvText.Text = "The Milky Way Galaxy\n-The outer solar system\n-Kuiper Belt\n-Somewhere near planet Pluto\n\nYou are located 5.7 billion km away\nfrom planet Earth.\nA long journey ahead of you, Good luck.";
                    Space.Image = global::Space_Battleships.Properties.Resources.Level1;
                    Space.SetBounds(0, 0, 1600, 900);
                    Space.SizeMode = PictureBoxSizeMode.Normal;
                    this.Controls.Add(Space);
                    Space.SendToBack();
                    break;

                case 2:
                    this.Text = "Level 2";
                    SM.Level2_PlayMusic();
                    Get_LogName();
                    LvText.Text = "The Milky Way Galaxy\n-The Iner solar system\n-Finally some Sunlight!! You're right next to Saturn Planet\nand his famous Moon Titan...\n\nYou are located 1.2 billion km away\nfrom planet Earth.\nDont give up now!!";
                    Space.Image = global::Space_Battleships.Properties.Resources.Level2;
                    Space.SetBounds(0, 0, 1600, 900);
                    Space.SizeMode = PictureBoxSizeMode.Normal;
                    this.Controls.Add(Space);
                    Space.SendToBack();
                    break;

                case 3:
                    this.Text = "Level 3";
                    SM.Level3_PlayMusic();
                    Get_LogName();
                    LvText.ForeColor = Color.Red;                    
                    LvText.Text = "The Milky Way Galaxy\n-The Iner solar system\n W-O-W!! this is outstanding!! Aolmost Home!...\n\nYou're in the backyard of Planet Earth\n\nOne last Push!\n K-i-l-l   T-h-e-m   A-l-l!!!";
                    Space.Image = global::Space_Battleships.Properties.Resources.level3;
                    Space.SetBounds(0, 0, 1600, 900);
                    Space.SizeMode = PictureBoxSizeMode.Normal;
                    this.Controls.Add(Space);
                    Space.SendToBack();
                    break;

            }

        }

        public void Missile_Visual()
        {
            Missile_Loc = new Point(Player.Location.X + 28, Player.Location.Y - 65);
            Missile = new Pl_Missile(this, 50, Missile_Loc, Space, Enemies);
        }

        public void GunVisual_BasicKit()
        {
            SM.MachineGun_PlaySound();
            Bullets_Fire = 2;
            
            if (B < PS)
            {
                Space.Controls.Add(Bullets[B]);
                Bullets[B].SetBounds(Player.Location.X + 30, Player.Location.Y - 55, 10, 15);
                Bullets[B].BringToFront();
                Bullets[B].BackColor = Color.Transparent;
                Bullets[B].Show();
                On_Move[B] = true;
                B++;
            }

            else
            {
                B = 0;
            }
        }

        public void GunVisual_ArmedKit()
        {
            SM.MachineGun_PlaySound();
            Bullets_Fire = 2;
            
            if (B < PS - 1)
            {
                Space.Controls.Add(Bullets[B]);
                Bullets[B].SetBounds(Player.Location.X + 10, Player.Location.Y - 30, 10, 15);
                Bullets[B].BringToFront();
                Bullets[B].BackColor = Color.Transparent;
                Bullets[B].Show();
                On_Move[B] = true;

                Space.Controls.Add(Bullets[B + 1]);
                Bullets[B + 1].SetBounds(Player.Location.X + 70, Player.Location.Y - 10, 10, 15);
                Bullets[B + 1].BringToFront();
                Bullets[B + 1].BackColor = Color.Transparent;
                Bullets[B + 1].Show();
                On_Move[B + 1] = true;
                B += 2;
            }

            else
            {
                B = 0;
            }

        }


        private void button1_MouseDown(object sender, MouseEventArgs mea)
        {
            if (UControls == true)
            {
                if (mea.Button == MouseButtons.Right)
                {
                    if (Missiles > 0 && Missile_Launch == 0)
                    {
                        Missile_Fire = 60;
                        SM.MissileLaunch_PlaySound();
                        Missiles--;
                        Missile_Launch = 100;
                        User_Panel.Text = "Missiles: " + Missiles + "\nArmor: " + Armor + "\nPoints: " + Points;
                        Missile_Visual();
                    }
                }

                else if (mea.Button == MouseButtons.Left)
                {
                    //if (Cursor.Position.X - 240 > Player.Location.X && Cursor.Position.X - 380 < Player.Location.X + 100)
                  //  {
                        if (Armed_Arms == false)
                            GunVisual_BasicKit();

                        if (Armed_Arms == true)
                            GunVisual_ArmedKit();
                   // }
                }
               
            }
        }


        void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (UControls == true)
            {
                X = Player.Location.X;
                Y = Player.Location.Y;

                if (e.KeyCode == Keys.D)
                    R = true;                       

                else if (e.KeyCode == Keys.A)
                    L = true;                                

                else if (e.KeyCode == Keys.W)
                    U = true;                        

                else if (e.KeyCode == Keys.S)
                    D = true;                                 

                else if (e.KeyCode == Keys.Q)
                {
                    Safe_Exit();
                    this.Close();
                }

                else if (e.KeyCode == Keys.M)
                {
                    Safe_Exit();
                    this.Close();
                    MM.ReOpen();
                    MM.Show();
                }

                Player.Location = new Point(X, Y);
            }

            else if (e.KeyCode == Keys.Q)
            {
                Safe_Exit();
                this.Close();
            }

            else if (e.KeyCode == Keys.M)
            {
                Safe_Exit();
                this.Close();
                MM.ReOpen();
                MM.Show();
            }
        }

        private void Level1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.D)
                R = false;

            if (e.KeyData == Keys.A)
                L = false;

            if (e.KeyData == Keys.W)
                U = false;

            if (e.KeyData == Keys.S)
                D = false;
        }


        private void timer1_Tick(object sender, EventArgs e)           //Movment
        {                                                        
            if (R == true && (Player.Location.X <= 1500))
            {
                X += 4;
                Player.Location = new Point(X, Y);
            }

            if (L == true && (Player.Location.X >= 0))
            {
                X -= 4;
                Player.Location = new Point(X, Y);
            }

            if (U == true && (Player.Location.Y >= 10))
            {
                Y -= 4;
                Player.Location = new Point(X, Y);
            }

            if (D == true && (Player.Location.Y <= 750))
            {
                Y += 4;
                Player.Location = new Point(X, Y);
            }
       
        }    

        public void Kill()
        {            
            SM.StopPlayer4();
            Player.Visible = false;
            Point pos = new Point(Player.Location.X, Player.Location.Y + 330);
            Explosion Exp1 = new Explosion(1, pos, Space, 5, 1000);
            this.UControls = false;
            
            LvText.Visible = true;
            LvText.ForeColor = Color.Red;
            LvText.Location = new System.Drawing.Point(600, 600);
            LvText.Size = new System.Drawing.Size(450, 70);
            LvText.Font = new Font("Garamond", 54, FontStyle.Bold);
            LvText.Text = "Game Over!!";
            
            Label Lb = new Label();
            Lb.Visible = true;
            Lb.Location = new System.Drawing.Point(610, 680);
            Lb.Size = new System.Drawing.Size(250, 50);
            Lb.Font = new Font("Garamond", 24, FontStyle.Bold);
            Lb.ForeColor = Color.Red;
            Lb.Text = "Prass M";
            Lb.BackColor = Color.Transparent;
            Lb.BringToFront();
            Space.Controls.Add(Lb);

            progressBar1.Value = 0;
            Missiles = 0;
            Armor = 0;
            User_Panel.Text = "Missiles: " + Missiles + "\nArmor: " + Armor + "\nPoints: " + Points;
            DBM.Update_GameDataBase(Points, UserName);
        }

        private void GetUser_Data()
        {
            Points = DBM.UD.Points;
            progressBar1.Value = DBM.UD.Health;
            Armor = DBM.UD.Armor;
            Missiles = DBM.UD.Missiles;
            Armed_Arms = DBM.UD.Armed_Arms;
            CurrentLevel = DBM.UD.Level;
        }
       
 
        private void EventTimer_Tick(object sender, EventArgs e)
        {
            if (Read_time > 0)
            {
                Read_time--;
            
                if (Read_time == 0)
                {
                    LvText.Visible = false;
                    
                    if(CurrentLevel == 1)
                        AttackWave_L1();

                    if(CurrentLevel == 2)
                        AttackWave_L2();

                    if (CurrentLevel == 3)
                        AttackWave_L3();
                }
            }

            if (Missile_Launch > 0)           
                Missile_Launch--;
            

            if (Count > 0)
            {
                if (Count == 1)
                {
                  if(CurrentLevel == 1)
                     AttackWave_L1();

                  if(CurrentLevel == 2)
                     AttackWave_L2();
                  
                  if(CurrentLevel == 3)
                     AttackWave_L3();
                }
            }

            //End Of Level - Victory!!

            if (End == true)
            {

                Player.Top -= 3;
                if (Player.Location.Y <= -250 && Player.Visible)
                {
                    DBM.UpDate_SystemDataBase(Points, (progressBar1.Value), Armor, Missiles, Armed_Arms, CurrentLevel);         //Save User Data
                    Safe_Exit();                                                                                              //Safe Exit & Move to Next Level..

                    if (CurrentLevel < 3)
                        SWS.Show();

                    else
                    {
                        DBM.Update_GameDataBase(Points, UserName);
                        VCF.Show();
                    }

                    this.Dispose();
                }
            }



            if (Missile_Fire > 0)
            {
                fire3.Image = global::Space_Battleships.Properties.Resources.flameM;
                fire3.SizeMode = PictureBoxSizeMode.Normal;
                fire3.SetBounds(Player.Location.X + 22, Player.Location.Y - 55, 40, 76);
                Space.Controls.Add(fire3);
                //fire3.BringToFront();
                fire3.BackColor = Color.Transparent;

                Missile_Fire--;
            }

            else if (Missile_Fire == 0)
                Space.Controls.Remove(fire3);


            if (Bullets_Fire > 0 && Armed_Arms == true)
            {
                fire1.Image = global::Space_Battleships.Properties.Resources.flame;
                fire1.SizeMode = PictureBoxSizeMode.Normal;
                fire1.SetBounds(Player.Location.X + 55, Player.Location.Y + 18, 30, 50);
                Space.Controls.Add(fire1);
                fire1.BringToFront();
                fire1.BackColor = Color.Transparent;

                fire2.Image = global::Space_Battleships.Properties.Resources.flame;
                fire2.SizeMode = PictureBoxSizeMode.Normal;
                fire2.SetBounds(Player.Location.X - 10, Player.Location.Y + 18, 30, 50);
                Space.Controls.Add(fire2);
                fire2.BringToFront();
                fire2.BackColor = Color.Transparent;

                Bullets_Fire--;
            }

            else if (Bullets_Fire == 0 && Armed_Arms == true)
            {
                Space.Controls.Remove(fire1);
                Space.Controls.Remove(fire2);
            }

            if (Bullets_Fire > 0 && Armed_Arms == false)
            {
                fire1.Image = global::Space_Battleships.Properties.Resources.flame;
                fire1.SizeMode = PictureBoxSizeMode.Normal;
                fire1.SetBounds(Player.Location.X + 20, Player.Location.Y - 48, 30, 50);
                Space.Controls.Add(fire1);
                fire1.BringToFront();
                fire1.BackColor = Color.Transparent;

                Bullets_Fire--;
            }

            else if (Bullets_Fire == 0 && Armed_Arms == false)
                Space.Controls.Remove(fire1);
    
            //Who Is Still Active?
            for (z = 0; z < PS; z++)
            {
                if (Enemies[z] is Alien_A)
                {
                    Alien_A hostile = (Alien_A)Enemies[z];
                    if (hostile.IsAlive == false && Is_Active[z] == true)
                    {
                        Is_Active[z] = false;
                        Count--;
                        Points += 40;
                        User_Panel.Text = "Missiles: " + Missiles + "\nArmor: " + Armor + "\nPoints: " + Points;
                    }
                }

                if (Enemies[z] is Alien_B)
                {
                    Alien_B hostile = (Alien_B)Enemies[z];
                    if (hostile.IsAlive == false && Is_Active[z] == true)
                    {
                        Is_Active[z] = false;
                        Count--;
                        Points += 60;
                        User_Panel.Text = "Missiles: " + Missiles + "\nArmor: " + Armor + "\nPoints: " + Points;
                    }
                }

                if (Enemies[z] is Alien_C)
                {
                    Alien_C hostile = (Alien_C)Enemies[z];
                    if (hostile.IsAlive == false && Is_Active[z] == true)
                    {
                        Is_Active[z] = false;
                        Count--;
                        Points += 50;
                        User_Panel.Text = "Missiles: " + Missiles + "\nArmor: " + Armor + "\nPoints: " + Points;
                    }
                }

                if (Enemies[z] is Alien_D)
                {
                    Alien_D hostile = (Alien_D)Enemies[z];
                    if (hostile.IsAlive == false && Is_Active[z] == true)
                    {
                        Is_Active[z] = false;
                        Count--;
                        Points += 65;
                        User_Panel.Text = "Missiles: " + Missiles + "\nArmor: " + Armor + "\nPoints: " + Points;
                    }
                }

                if (Enemies[z] is Alien_E)
                {
                    Alien_E hostile = (Alien_E)Enemies[z];
                    if (hostile.IsAlive == false && Is_Active[z] == true)
                    {
                        Is_Active[z] = false;
                        Points += 500;
                        User_Panel.Text = "Missiles: " + Missiles + "\nArmor: " + Armor + "\nPoints: " + Points;
                        End = true;
                    }
                }                      
            }
            
            //Is Hit!! (Bullet)
            if (IsHit == true)
            {
                IsHit = false;
                if (Armor > 0)
                {
                    Armor -= (float)1;                      //-2
                    User_Panel.Text = "Missiles: " + Missiles + "\nArmor: " + Armor + "\nPoints: " + Points;

                    if (Armor < 0)
                    {
                        Armor = 0;
                        User_Panel.Text = "Missiles: " + Missiles + "\nArmor: " + Armor + "\nPoints: " + Points;
                    }
                }

                else
                {
                    Armor = 0;
                    User_Panel.Text = "Missiles: " + Missiles + "\nArmor: " + Armor + "\nPoints: " + Points;
                    try
                    {
                        progressBar1.Value -= 2;         //-3
                        if (progressBar1.Value < 45 && AlarmFlag == false)
                        {
                            AlarmFlag = true;
                            SM.DangerAlarm_PlaySound();
                        }
                    }
                    //Game Over
                    catch (ArgumentOutOfRangeException) { Kill(); }                    
                }
            }

            //Is Hit!! (Missile)
            if (MissileHit == true)
            {
                MissileHit = false;

                if (Armor > 0)
                {
                    Armor -= 70;
                    User_Panel.Text = "Missiles: " + Missiles + "\nArmor: " + Armor + "\nPoints: " + Points;

                    if (Armor < 0)
                    {
                        Armor = 0;
                        User_Panel.Text = "Missiles: " + Missiles + "\nArmor: " + Armor + "\nPoints: " + Points;
                    }
                }

                else
                    try  //Game Over
                    { progressBar1.Value -= 70; }
                    catch (ArgumentOutOfRangeException) { Kill(); }               
            }


            //Moving Objects
            for (M = 0; M < PS; M++)
            {
                if (On_Move[M] == true)
                {
                    Bullets[M].Top -= 25;

                    if (Bullets[M].Location.Y <= 0)
                    {
                        On_Move[M] = false;
                        Bullets[M].Hide();
                    }

                    if (Bullets[M].Visible == false)
                    {
                        On_Move[M] = false;
                        Bullets[M].Location = new Point(0, 0);
                    }
                }
            }

           
        }


        //Safe Exit
        private void Safe_Exit()
        {
            TMR1.Enabled = false;
            TMR2.Enabled = false;
            SM.StopPlayer1();
            SM.StopPlayer4();
            UControls = false;
          
            this.BackColor = Color.Black;          //Kill Threads  
        }

        public void MotherShip_MusicControl()
        {
            SM.StopPlayer1();
            SM.Final_AttackHalf();
        }

        private void Get_LogName()
        {
            UserName = LOG.Sand_LogName;
        }

        private void AttackWave_L1()
        {
            Point Pos = new Point(0, 0);
            Select++;

            switch (Select)
            {
                case 1:
                    Count = 6;
                    Pos = new Point(1700, 50);
                    Enemies[0] = new Alien_A(300, Pos, this, Space, Player, Bullets);        //A,A,A,B,B
                    Pos.X += 200;
                    Enemies[1] = new Alien_A(700, Pos, this, Space, Player, Bullets);        
                    Pos.X += 200;
                    Enemies[2] = new Alien_A(1100, Pos, this, Space, Player, Bullets);
                    Pos = new Point(1900, 350);
                    Enemies[3] = new Alien_B(500, Pos, this, Space, Player, Bullets);
                    Pos.X += 200;
                    Enemies[4] = new Alien_B(800, Pos, this, Space, Player, Bullets);
                    break;

                case 2:
                    Count = 6;
                    Pos = new Point(1800, 50);
                    Enemies[5] = new Alien_A(500, Pos, this, Space, Player, Bullets);
                    Pos.X += 200;
                    Enemies[6] = new Alien_A(900, Pos, this, Space, Player, Bullets);
                    Pos = new Point(1800, 350);
                    Enemies[7] = new Alien_B(200, Pos, this, Space, Player, Bullets);
                    Pos.X += 200;
                    Enemies[8] = new Alien_B(600, Pos, this, Space, Player, Bullets);
                    Pos.X += 200;
                    Enemies[9] = new Alien_B(1000, Pos, this, Space, Player, Bullets);
                    break;

                case 3:
                    Count = 6;
                    Pos = new Point(1800, 50);
                    Enemies[0] = new Alien_D(400, Pos, this, Space, Player, Bullets);
                    Pos.X += 600;
                    Enemies[1] = new Alien_D(700, Pos, this, Space, Player, Bullets);
                    Pos = new Point(2000, 350);
                    Enemies[2] = new Alien_C(300, Pos, this, Space, Player, Bullets);
                    Pos.X += 200;
                    Enemies[3] = new Alien_C(600, Pos, this, Space, Player, Bullets);
                    Pos.X += 200;
                    Enemies[4] = new Alien_C(900, Pos, this, Space, Player, Bullets);

                    for (x = 0; x < 5; x++)
                        Is_Active[x] = true;
                    break;

                case 4:
                    Count = 6;
                    Pos = new Point(2000, 50);
                    Enemies[5] = new Alien_A(300, Pos, this, Space, Player, Bullets);
                    Pos.X += 200;
                    Enemies[6] = new Alien_A(600, Pos, this, Space, Player, Bullets);
                    Pos.X += 200;
                    Enemies[7] = new Alien_A(900, Pos, this, Space, Player, Bullets);
                    Pos.X += 200;
                    Enemies[8] = new Alien_A(1200, Pos, this, Space, Player, Bullets);
                    Pos = new Point(1800, 350);
                    Enemies[9] = new Alien_D(800, Pos, this, Space, Player, Bullets);

                    for (x = 5; x < PS; x++)
                        Is_Active[x] = true;
                    break;

                case 5:
                    End = true;
                    break;
            }

        }

        private void AttackWave_L2()
        {
            Point Pos = new Point(0, 0);
            Select++;

            switch (Select)
            {
                case 1:
                    Count = 7;
                    Pos = new Point(1700, 50);
                    Enemies[0] = new Alien_C(300, Pos, this, Space, Player, Bullets);  //c
                    Pos.X += 200;
                    Enemies[1] = new Alien_D(700, Pos, this, Space, Player, Bullets);   //d
                    Pos.X += 200;
                    Enemies[2] = new Alien_C(1100, Pos, this, Space, Player, Bullets);   //c
                    Pos.X += 200;
                    Pos = new Point(1700, 350);
                    Enemies[3] = new Alien_B(300, Pos, this, Space, Player, Bullets);    //b
                    Pos.X += 200;
                    Enemies[4] = new Alien_B(700, Pos, this, Space, Player, Bullets);    //b
                    Pos.X += 200;
                    Enemies[5] = new Alien_B(1100, Pos, this, Space, Player, Bullets);   //b
                    Pos.X += 200;                    
                    break;

                case 2:
                    Count = 7;
                    Pos = new Point(1700, 50);
                    Enemies[6] = new Alien_C(400, Pos, this, Space, Player, Bullets);
                    Pos.X += 200;
                    Enemies[7] = new Alien_C(600, Pos, this, Space, Player, Bullets);
                    Pos.X += 200;
                    Enemies[8] = new Alien_C(800, Pos, this, Space, Player, Bullets);
                    Pos.X += 200;
                    Enemies[9] = new Alien_C(1000, Pos, this, Space, Player, Bullets);
                    Pos = new Point(1700, 350);
                    Enemies[0] = new Alien_A(500, Pos, this, Space, Player, Bullets);
                    Pos.X += 200;
                    Enemies[1] = new Alien_A(700, Pos, this, Space, Player, Bullets);
                    Pos.X += 200;

                    for (x = 0; x < 2; x++)
                        Is_Active[x] = true;
                    break;

                case 3:
                    Count = 7;
                    Pos = new Point(1700, 50);
                    Enemies[2] = new Alien_D(300, Pos, this, Space, Player, Bullets);
                    Pos.X += 500;
                    Enemies[3] = new Alien_D(1100, Pos, this, Space, Player, Bullets);
                    Pos = new Point(1700, 300);
                    Enemies[4] = new Alien_C(400, Pos, this, Space, Player, Bullets);
                    Pos.X += 500;
                    Enemies[5] = new Alien_C(1000, Pos, this, Space, Player, Bullets);
                    Pos = new Point(1700, 450);
                    Enemies[6] = new Alien_A(500, Pos, this, Space, Player, Bullets);
                    Pos.X += 500;
                    Enemies[7] = new Alien_A(900, Pos, this, Space, Player, Bullets);

                    for (x = 2; x < 8; x++)
                        Is_Active[x] = true;
                    break;

                case 4:
                    Count = 9;
                    Pos = new Point(2000, 50);
                    Enemies[0] = new Alien_A(300, Pos, this, Space, Player, Bullets);
                    Pos.X += 200;
                    Enemies[1] = new Alien_A(600, Pos, this, Space, Player, Bullets);
                    Pos.X += 200;
                    Enemies[2] = new Alien_A(800, Pos, this, Space, Player, Bullets);
                    Pos.X += 200;
                    Enemies[3] = new Alien_A(1100, Pos, this, Space, Player, Bullets);
                    Pos = new Point(2000, 350);
                    Enemies[4] = new Alien_A(300, Pos, this, Space, Player, Bullets);
                    Pos.X += 200;
                    Enemies[5] = new Alien_A(600, Pos, this, Space, Player, Bullets);
                    Pos.X += 200;
                    Enemies[6] = new Alien_A(800, Pos, this, Space, Player, Bullets);
                    Pos.X += 200;
                    Enemies[7] = new Alien_A(1100, Pos, this, Space, Player, Bullets);

                    for (x = 0; x < 8; x++)
                        Is_Active[x] = true;
                    break;

                case 5:
                    End = true;
                    break;
            }
        }

        private void AttackWave_L3()
        {
            Point Pos = new Point(0, 0);
            Select++;

            switch (Select)
            {
                case 1:
                    Count = 7;
                    Pos = new Point(1700, 50);
                    Enemies[0] = new Alien_A(300, Pos, this, Space, Player, Bullets);
                    Pos.X += 400;
                    Enemies[1] = new Alien_D(600, Pos, this, Space, Player, Bullets);
                    Pos.X += 200;
                    Enemies[2] = new Alien_D(800, Pos, this, Space, Player, Bullets);
                    Pos.X += 100;
                    Enemies[3] = new Alien_A(1100, Pos, this, Space, Player, Bullets);
                    Pos = new Point(1700, 200);
                    Enemies[4] = new Alien_A(400, Pos, this, Space, Player, Bullets);
                    Pos.X += 300;
                    Enemies[5] = new Alien_A(1000, Pos, this, Space, Player, Bullets);
                    break;

                case 2:
                    Count = 8;
                    Pos = new Point(1700, 50);
                    Enemies[0] = new Alien_C(300, Pos, this, Space, Player, Bullets);
                    Pos.X += 200;   
                    Enemies[1] = new Alien_A(700, Pos, this, Space, Player, Bullets);
                    Pos.Y += 100;   
                    Enemies[2] = new Alien_C(400, Pos, this, Space, Player, Bullets);
                    Pos.X += 200;   
                    Pos.Y += 100;
                    Enemies[3] = new Alien_C(550, Pos, this, Space, Player, Bullets);
                    Pos.X += 200;
                    Enemies[4] = new Alien_C(900, Pos, this, Space, Player, Bullets);
                    Pos.X += 200;
                    Pos.Y -= 100;
                    Enemies[5] = new Alien_C(1050, Pos, this, Space, Player, Bullets);
                    Pos.X += 200;
                    Pos.Y -= 100;
                    Enemies[6] = new Alien_C(1150, Pos, this, Space, Player, Bullets);

                    for (x = 0; x < 7; x++)
                        Is_Active[x] = true;
                    break;

                case 3:
                    Count = 7;
                    Pos = new Point(1700, 50);
                    Enemies[0] = new Alien_D(300, Pos, this, Space, Player, Bullets);
                    Pos.X += 600;
                    Enemies[1] = new Alien_D(1100, Pos, this, Space, Player, Bullets);
                    Pos = new Point(1700, 200);
                    Enemies[2] = new Alien_A(400, Pos, this, Space, Player, Bullets);
                    Pos.X += 500;
                    Enemies[3] = new Alien_A(1000, Pos, this, Space, Player, Bullets);
                    Pos = new Point(1700, 350);
                    Enemies[4] = new Alien_B(500, Pos, this, Space, Player, Bullets);
                    Pos.X += 500;
                    Enemies[5] = new Alien_B(900, Pos, this, Space, Player, Bullets);
                    Pos = new Point(1900, 50);
                   
                    for (x = 0; x < 6; x++)
                        Is_Active[x] = true;
                    break;

                case 4:
                    Count = 4;
                    Select--;

                    if (MotherShip_isAlive == false)
                    {
                        MotherShip_isAlive = true;
                        Pos = new Point(2720, -60);                       
                        Enemies[0] = new Alien_E(50, Pos, this, Space, Player, Bullets);
                        Is_Active[0] = true;
                        SM.StopPlayer1();
                        SM.Final_AttackFull();
                    }
                    
                    Pos = new Point(3000 , 400);
                    Enemies[1] = new Alien_C(350, Pos, this, Space, Player, Bullets);
                    Pos.X += 200;
                    Enemies[2] = new Alien_C(750, Pos, this, Space, Player, Bullets);
                    Pos.X += 200;
                    Enemies[3] = new Alien_C(1050, Pos, this, Space, Player, Bullets);

                    for (x = 1; x < 4; x++)
                        Is_Active[x] = true;
                    break;

            }

        }

    }
}






