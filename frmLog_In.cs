using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.Sql;

namespace Space_Battleships
{
    public partial class frmLog_In : Form
    {
        public frmLog_In()
        {
            InitializeComponent();
        }

        MainManu MM = new MainManu();
        SoundManager SM = new SoundManager();
        DataBase_Manager DBM = new DataBase_Manager();

        static string UserName;

        private void frmLog_In_Load(object sender, EventArgs e)
        {
            
        }

        private void btnLogIn_Click(object sender, EventArgs e)
        {            
            string UName = txtEnterUserName.Text;

            bool res = NameTest(UName);

            if (res == true)
            {
                DBM.Call_GameDataBase(UName);
                UserName = DBM.UP.UserName;
                
                MM.FrmSleep();
                DBM.UpDate_SystemDataBase(0, 100, 100, 4, false, 1);

                Level Lv = new Level();
                Lv.Show();
                this.Close();

            }

            else if (res == false)
            {
                MessageBox.Show("Invalid User name!,Try Again");
            }
        }

        private bool NameTest(string UserName)
        {
            if (UserName.Length <= 0 || UserName.Length > 10 || UserName == null)
            {
                return false;
            }
            return true;
        }

        public String Sand_LogName
        {
            get
            {
                return UserName;
            }
        }
        
        private void label1_MouseClick(object sender, MouseEventArgs e)
        {
            this.Hide();
        }

        private void lblGoBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DeleteBTN_Click(object sender, EventArgs e)
        {
            UserName = txtEnterUserName.Text;
            DBM.RemoveUser(UserName,true);
        }
    
    
    }
}
