using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grain_Species_determination
{
    public partial class frm_Loading : Form
    {
        frm_Main MAIN;
        

        int fCnt=0;
        public frm_Loading()
        {
            InitializeComponent();
            MAIN = new frm_Main();
            
        }

        private void frm_Loading_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.Loading1920;
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (fCnt == 4)
            {
                MAIN.Show();
            }
            else if (fCnt == 5)
            {
             
                this.Hide();
            }
            else if (fCnt > 6)
            {
                fCnt = 7;

            }
            fCnt++;

            if (MAIN.IsDisposed)
            {
                this.Close();
            }


        }

        private void frm_Loading_KeyDown(object sender, KeyEventArgs e)
        {

        }
    }
}
