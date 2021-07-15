using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Media;

namespace Space_Battleships
{
    public partial class Space_WeaponsStation : Form
    {
        public Space_WeaponsStation()
        {
            InitializeComponent();
        }

        SoundManager SM = new SoundManager();
        DataBase_Manager DBM = new DataBase_Manager();

        private Boolean flag = false;

        private int Total_Price;
        private int Level;
        private Boolean Armed_Arms;
        private int Health;
        private float Armor;
        private int Missiles;
        private int Points;

        private void Space_WeaponsStation_Load(object sender, EventArgs e)
        {
            //Cursor.Show();
            DBM.Call_SystemDataBase();
            GetUser_Data();

            Health = progressBar1.Value;
            numericUpDown1.Maximum = 100 - Health;
            Health = 0;

            label5.Text = "Missiles: " + Missiles + "\nArmor: " + Armor + "\nPoints: " + Points;

            if (Armed_Arms == true)
                checkBox1.Enabled = false;

            SM.Space_WeaponsStation_PlayMusic();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            if (numericUpDown1.Value + progressBar1.Value > 100)
                flag = true;

            else if (numericUpDown1.Value + progressBar1.Value < 100)
                flag = false;

            Total_Price = ((int)numericUpDown1.Value *2)
                + ((int)numericUpDown2.Value * 3)
                + ((int)numericUpDown3.Value * 400);


            label6.Text = "Total Price:" + (Total_Price += CB_IsOn());

            if (Total_Price > Points)
            {
                button1.Enabled = false;
                button1.BackColor = Color.Red;
            }

            else
            {
                button1.Enabled = true;
                button1.BackColor = Color.Green;
            }

            if (flag == true)
                numericUpDown1.Maximum = numericUpDown1.Value;
        }


        private int CB_IsOn()
        {
            if (checkBox1.Checked == true)
                return 200;

            return 0;
        }


        private void button1_Click_1(object sender, EventArgs e)
        {
           
            Points -= Total_Price;
            Health += (int)numericUpDown1.Value;
            Armor += (float)numericUpDown2.Value;
            Missiles += (int)numericUpDown3.Value;

            progressBar1.Value += Health;

            label5.Text = "Missiles: " + Missiles + "\nArmor: " + Armor + "\nPoints: " + Points;

            if (checkBox1.Checked == true && checkBox1.Enabled == true)
            {
                Armed_Arms = true;
                checkBox1.Checked = false;
                checkBox1.Enabled = false;
            }

            Health = progressBar1.Value;
            numericUpDown1.Maximum = 100 - Health;
            Health = 0;

            Total_Price = 0;
            numericUpDown1.Value = 0;
            numericUpDown2.Value = 0;
            numericUpDown3.Value = 0;
            Health = 0;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Level++;
            DBM.UpDate_SystemDataBase(Points, progressBar1.Value, Armor, Missiles, Armed_Arms, Level);

            timer1.Stop();
            SM.StopPlayer1();

            Level Lv = new Level();
            Lv.Show();
            this.Close();
        }

        private void GetUser_Data()
        {
            Points = DBM.UD.Points;
            progressBar1.Value = DBM.UD.Health;
            Armor = DBM.UD.Armor;
            Missiles = DBM.UD.Missiles;
            Armed_Arms = DBM.UD.Armed_Arms;
            Level = DBM.UD.Level;
        }

    }

}

