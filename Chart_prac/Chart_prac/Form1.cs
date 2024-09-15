using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms.DataVisualization;
using System.Drawing.Printing;

namespace Chart_prac
{
    public partial class Form1 : Form
    {
        int [] fAdd = new int [100];
        double [] fNew = new double[10];
        string[] fName = new string[10];
        private int fIndex = 0;
        private double fDou=0;
        private bool fChart1;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Random sRnd = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i<100; i++)
            {
                fAdd[i] = sRnd.Next(0, 101);
            }
            for (int i = 0; i < 10; i++)
            {
                fName[i] = (i+1).ToString();
            }
   //         chart1.ChartAreas.Clear();
   //         chart1.ChartAreas.Add("one");
            
            timer1.Enabled = false;
            timer1.Interval = 50;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*chart1.Series[0].Points.Clear();
            //    chart1.ChartAreas.Clear();
            if (fChart1)
            {
                for (int i = 0; i < 10; i++) { fNew[i] = i + 0.2; }
                for (int i = 0; i < 10; i++) { fAdd[i] = i + 1; }

                chart1.Series[0].Points.DataBindXY(fAdd, fNew);
                fChart1 = false;
            }
            else
            {
                for (int i = 9; i >= 0; i--) { fNew[i] = i - 0.2; }
                for (int i = 9; i >= 0; i--) { fAdd[i] = i - 1; }

                chart1.Series[0].Points.DataBindXY(fNew, fAdd);
                fChart1 = true;
            }
            
            chart1.Series[0].XAxisType = AxisType.Primary;
         //   chart1.ChartAreas.Add("one");
            
            
            //fAdd += 10;*/
            chart1.Series[0].Points.Clear();
            chart1.Series[0].Name = "Flow";
//chart1.ChartAreas.Clear();
            if ((sender as Button).Text == "Right")
            {
                int[] sOn_chart = new int[10];
                int sCurr = 0;
                if (fIndex <= 98)
                {
                    fIndex++;
                }
                
                sCurr = fIndex;
                for (int i = 0;i<10;i++, fIndex++)
                {
                    sOn_chart[i] = fAdd[fIndex];
                }

                fIndex = sCurr;
                
                chart1.Series[0].Points.DataBindXY(fName, sOn_chart);
                
            }
            else
            {

                int[] sOn_chart = new int[10];
                int sCurr = 0;
                if (fIndex != 0)
                {
                    fIndex--;
                }
                
                sCurr = fIndex;
                for (int i = 0; i < 10; i++, fIndex++)
                {
                    sOn_chart[i] = fAdd[fIndex];
                }

                fIndex = sCurr;
                
                chart1.Series[0].Points.DataBindXY(fName, sOn_chart);
                
            }

            label1.Text = fIndex.ToString();

        }

        private void button2_Click(object sender, EventArgs e)
        {
           string a = chart1.Series[0].Points[1].XValue.ToString();
            MessageBox.Show(a);
        }

        private void uiBtn_Chart_Click(object sender, EventArgs e)

        {

            int sCnt = 0;


            // fill the data table with values

            var dt = new DataTable();

            dt.Columns.Add("Id", typeof(int));

            dt.Columns.Add("Name", typeof(string));

            dt.Columns.Add("School", typeof(string));

            dt.Columns.Add("Score", typeof(int));



            dt.Rows.Add(0, "범범조조", "School1", 100);

            dt.Rows.Add(1, "안정환", "School1", 30);

            dt.Rows.Add(2, "류현진", "School1", 40);
            
            dt.Rows.Add(3, "정형돈", "School2", 80);

            dt.Rows.Add(4, "김성주", "School2", 70);



            // bind the data table to chart

            this.chart1.Series.Clear();



            var series = chart1.Series.Add("Score");

            series.XValueMember = "Id";

            series.YValueMembers = "Score";

            



            //series.ChartType = SeriesChartType.Column;

            this.chart1.DataSource = dt;

            this.chart1.DataBind();



            // custom labels 
            // groupby로 datatable 내의 column을 school 필드로 묶음
            foreach (var g in dt.AsEnumerable().GroupBy(x => x.Field<string>("School"))) 

            {

                string school = g.Key;

                var names = g.Select(r => new {Id = r.Field<int>("Id")
                                              ,Name = r.Field<string>("Name")});

                // find min-max

                int min = names.Min(y => y.Id);

                int max = names.Max(y => y.Id);

                // city labels

                foreach (var name in names)

                {

                    var label = new CustomLabel(name.Id - 1, name.Id + 1,

                        name.Name, 0, LabelMarkStyle.None);

                    this.chart1.ChartAreas[0].AxisX.CustomLabels.Add(label);

                }

                // city states

                var statelabel = new CustomLabel(min, max, school, 1,

                    LabelMarkStyle.LineSideMark);

                this.chart1.ChartAreas[0].AxisX.CustomLabels.Add(statelabel);
                label1.Text = sCnt.ToString();
                sCnt++;
            }

        }
        double x;
        private void timer1_Tick(object sender, EventArgs e)
        {
            chart2.Series[0].Points.AddXY(x, 3 * Math.Sin(5 + x) + 5 * Math.Cos(3 * x));
            chart2.Series[1].Points.AddXY(x, 5 * Math.Sin(7 + x) + 5 * Math.Cos(5 * x));

            if (chart2.Series[0].Points.Count > 100)
            {
                chart2.Series[0].Points.RemoveAt(0);
                chart2.Series[1].Points.RemoveAt(0);
            }
            double xvalue= chart2.Series[0].Points[0].XValue;
            chart2.ChartAreas[0].AxisX.Minimum = double.Parse(xvalue.ToString("F2"));
            label3.Text = $"XValue : {xvalue}";
            chart2.ChartAreas[0].AxisX.Maximum = x;
            label2.Text = $"X : {x}";

            xvalue = chart2.Series[1].Points[0].XValue;
            chart2.ChartAreas[0].AxisX.Minimum = double.Parse(xvalue.ToString("F2"));
            label3.Text = $"XValue : {xvalue}";
            chart2.ChartAreas[0].AxisX.Maximum = x;
            label2.Text = $"X : {x}";

            x += 0.1;

        }

        private void chart2_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled)
            {
                timer1.Enabled = false;
            }
            else
            {
                timer1.Enabled = true;
            }
        }
    }
}
