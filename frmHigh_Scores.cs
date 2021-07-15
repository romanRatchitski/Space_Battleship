using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Space_Battleships
{
    public partial class frmHigh_Scores : Form
    {
        public frmHigh_Scores()
        {
            InitializeComponent();
        }

        int cnt = 1;
        int cnt2 = 1;
        DataBase_Manager DBM = new DataBase_Manager();

        private void frmHigh_Scores_Load(object sender, EventArgs e)
        {
            DBM.ShowHigh_Scores(this);
        }

       
        public void InsertNames(string Name)
        {
            switch (cnt)
            {
                case 1: lblName1.Text = Name;
                    cnt++;
                    break;
                case 2: lblName2.Text = Name;
                    cnt++;
                    break;
                case 3: lblName3.Text = Name;
                    cnt++;
                    break;
                case 4: lblName4.Text = Name;
                    cnt++;
                    break;
                case 5: lblName5.Text = Name;
                    cnt++;
                    break;
            }
        }

        public void InsertScores(string Score)
        {
            switch (cnt2)
            {
                case 1: lblScore1.Text = Score;
                    cnt2++;
                    break;
                case 2: lblScore2.Text = Score;
                    cnt2++;
                    break;
                case 3: lblScore3.Text = Score;
                    cnt2++;
                    break;
                case 4: lblScore4.Text = Score;
                    cnt2++;
                    break;
                case 5: lblScore5.Text = Score;
                    cnt2++;
                    break;
            }
        }

        private void lblGoBack_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
       
    }
}
