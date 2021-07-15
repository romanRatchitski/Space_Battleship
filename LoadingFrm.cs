using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Space_Battleships
{
    public partial class LoadingFrm : Form
    {
        public LoadingFrm()
        {
            InitializeComponent();
        }

        MainManu MM = new MainManu();

        private void LoadingFrm_Load(object sender, EventArgs e)
        {
            TMR.Enabled = true;
        }

        private void TMR_Tick(object sender, EventArgs e)
        {
            progressBar1.Value += 1;

            if (progressBar1.Value == 100)
            {
                TMR.Stop();
                TMR.Enabled = false;
                this.Hide();

                MM.Show();
            }

        }
    }
}
