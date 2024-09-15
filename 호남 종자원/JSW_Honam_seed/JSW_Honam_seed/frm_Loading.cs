using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JSW_Honam_seed
{
    public partial class frm_Loading : Form
    {
        frm_Sensor SENSORS;
        int fCnt=0;
        public frm_Loading()
        {
            InitializeComponent();
            pictureBox1.Image = JSW_Honam_seed.Properties.Resources.Loading1920;
            SENSORS = new frm_Sensor();
        }

        private void frm_Loading_Load(object sender, EventArgs e)
        {
            pictureBox1.Refresh();
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (fCnt == 1)
            {
                SENSORS.Show();
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

            if (SENSORS.IsDisposed)
            {
                this.Close();
            }


        }

        private void frm_Loading_KeyDown(object sender, KeyEventArgs e)
        {

        }
    }
}
