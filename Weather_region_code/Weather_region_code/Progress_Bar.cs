using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Weather_region_code
{
    public partial class Progress_Bar : Form
    {
        public Progress_Bar()
        {
            InitializeComponent();
        }

        private void Progress_Bar_Load(object sender, EventArgs e)
        {
            //progressBar1.Step = 1000;
        }

        public void psSet_Bar(int brMax)
        {
            progressBar1.Maximum = brMax;
        }

        public void psUpdate_Bar()
        {
            progressBar1.Value += 1;
            
        }
    }
}
