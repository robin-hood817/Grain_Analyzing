using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace datatable_use
{
    public partial class Form1 : Form
    {
        private string fRootpath;
        private DataTable fDt;
        private bool fCon = true;

        public Form1()
        {
            InitializeComponent();
        }

        public void psReading_csv()
        {
            using (StreamReader sSr = new StreamReader(fRootpath + "\\bin\\2022-12-01-1.csv", Encoding.GetEncoding("ks_c_5601-1987")))
            {
                int sLoop = 0;
                while (sLoop < 100)
                {
                    sLoop++;
                    string sData = sSr.ReadLine();
                    string[] sDatas = sData.Split(',');

                    if (fCon) 
                    {
                        fCon = false;
                    }
                    else
                    {
                        DataRow sDR = fDt.NewRow();

                        sDR["DateTime"] = sDatas[0] + sDatas[1];
                        sDR["Temperature"] = Convert.ToDouble(sDatas[3]);
                        sDR["Humidity"] = Convert.ToDouble(sDatas[4]);

                        fDt.Rows.Add(sDR);
                    }                 
                    

                    /* listBox1.Items.Add(string.Format("{0}  {1}",sDatas[0], sDatas[1]));
                    listBox2.Items.Add(sDatas[3] + " ℃");
                    listBox3.Items.Add(sDatas[4] + " %");*/
                }

                var series1 = chart1.Series.Add("Data_Temperature");

                series1.XValueMember = "DateTime";

                series1.YValueMembers = "Temperature";              

                series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;

                series1.BorderWidth = 3;

                series1.BorderColor = Color.Blue;

                var series2 = chart1.Series.Add("Data_Humidity");

                series2.XValueMember = "DateTime";

                series2.YValueMembers = "Humidity";

                series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;

                series2.BorderWidth = 3;

                series2.BorderColor = Color.Red;


                chart1.DataSource = fDt;

                chart1.ChartAreas[0].AxisX.MajorGrid.Interval = 1;

                chart1.ChartAreas[0].AxisX.LabelStyle.Interval = 1;

                chart1.DataBind();



            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            fRootpath = Application.StartupPath;

            fDt = new DataTable();

            fDt.Columns.Add("DateTime", typeof(string));
            fDt.Columns.Add("Temperature", typeof(double));
            fDt.Columns.Add("Humidity", typeof(double));
            
        }

        private void btn_Read_Click(object sender, EventArgs e)
        {
            psReading_csv();
        }

        private void listBox3_Click(object sender, EventArgs e)
        {
            string sidx = (sender as ListBox).SelectedIndex.ToString();

            listBox1.SelectedIndex = Convert.ToInt16(sidx);
            listBox2.SelectedIndex = Convert.ToInt16(sidx);
            listBox3.SelectedIndex = Convert.ToInt16(sidx);
        }
    }
}
