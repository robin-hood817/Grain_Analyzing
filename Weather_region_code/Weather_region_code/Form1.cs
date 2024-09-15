using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace Weather_region_code
{
    
    public partial class Form1 : Form
    {
        string fRootpath;
        string ftxt_path;
        Thread fList_up;
        Progress_Bar clPb;
        string[] fCode;
        string[] fAddr;
        int fCnt = 0;

        public Form1()
        {
            InitializeComponent();
            fRootpath = Application.StartupPath;
            ftxt_path = fRootpath + "\\bin\\Code_List.csv";
            clPb = new Progress_Bar();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        //    clPb.Show();
            fList_up = new Thread(new ThreadStart(psListup));
            fList_up.Start();
            psListup();
            timer1.Interval = 1000;
            

          
        }

        private void psListup()
        {
            StreamReader sr = new StreamReader(ftxt_path, Encoding.Default);
            int sCnt = 0;
            timer1.Start();
            while (!sr.EndOfStream)
            {
                string sStr = sr.ReadLine();                
                sCnt++;
            }
            timer1.Stop();
            fCode = new string[sCnt];
            fAddr = new string[sCnt];



            int sIndex = 0;

            clPb.psSet_Bar(sCnt);

            while (!sr.EndOfStream)
            {
                string sStr = sr.ReadLine();

                string sCode = sStr.Substring(1, 10);

                fCode[sIndex] = sCode;

                string sAddr = sStr.Substring(12, sStr.Length - 16);

                fAddr[sIndex] = sAddr;

                clPb.psUpdate_Bar();

                sIndex++;
                
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            clPb.psUpdate_Bar();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            fCnt++;
            label1.Text = fCnt.ToString();
        }
    }
}
