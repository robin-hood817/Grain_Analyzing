using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml;
using Excel = Microsoft.Office.Interop.Excel;

namespace Weather_Broad
{
    public partial class Form1 : Form
    {
        public string[] fTime;
        public double[] fTemp;
        public string[] fWf;
        public int fOrder=0;
        public bool fSet = false;
        Series chart_temp;


        private int fCnt;
        

        //Weather_Api Cwa;
        public Form1()
        {
            InitializeComponent();
         //   Cwa = new Weather_Api();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        public async Task Running()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync("http://www.kma.go.kr/wid/queryDFSRSS.jsp?zone=4888036000");

            if (!response.IsSuccessStatusCode)
            {
                MessageBox.Show($"서버에서 오류를 반환했습니다. 반환 코드 = {response.StatusCode}");
                return;
            }

            string content = await response.Content.ReadAsStringAsync();

            XmlDocument document = new XmlDocument();
            document.LoadXml(content);

            XmlNodeList nodes = document.DocumentElement.SelectNodes("descendant::data");

            fWf = new string[nodes.Count];
            fTemp = new double[nodes.Count];
            fTime = new string[nodes.Count];

            int sLoop = 0;
            foreach (XmlNode node in nodes)
            {
                var sHour = node.SelectSingleNode("hour");
                var getData = node.SelectSingleNode("temp");
                var Status = node.SelectSingleNode("wfKor");
                fTime[sLoop] = sHour.InnerText;
                fTemp[sLoop] = Convert.ToDouble(getData.InnerText);
                fWf[sLoop] = Status.InnerText;
                sLoop++;


            }

            
            fSet = true;
        }

        private void lbl_location_Click(object sender, EventArgs e)
        {
         //   Task.Run(() => Running());

            int sLoop=0;


 
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Task.Run(() => Running());
            lbl_Date.Text = DateTime.Now.ToString();
            timer1_Tick(sender, e);
            timer1.Interval = 1000;
            timer1.Enabled = true;
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            Ch_Temp.Series.Clear();

            chart_temp = Ch_Temp.Series.Add("온도");

            Title chart_title = new Title();

            chart_title.Text = "";

            chart_temp.LegendText = "온도";

            chart_temp.ChartType = SeriesChartType.FastLine;

            chart_temp.Color = Color.SkyBlue;
            chart_temp.BorderWidth = 2;
            Ch_Temp.ChartAreas[0].AxisX.Interval = 3;
            Ch_Temp.ChartAreas[0].AxisY.Interval = 5;

            


        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            psSetting_Weather(fOrder);
        }

        private int pfimage_checking(string brStatus)
        {
            int sOrder=0;

            switch (brStatus)
            {
                case "맑음":
                    sOrder = 0;
                    break;
                case "비":
                    sOrder = 1;
                    break;
                case "구름 많음":
                    sOrder = 2;
                    break;
                case "흐림":
                    sOrder = 3;
                    break;
                default:
                    break;
            }

            return sOrder;
        }

        private void btn_next_Click(object sender, EventArgs e)
        {
            if ((sender as PictureBox).Name == "btn_next")
            {
                if (fOrder == 20) { fOrder = 20; }
                else { fOrder++; }
                psSetting_Weather(fOrder);
                Ch_Temp.Series[0].Points.DataBindXY(fTime, fTemp);
               
            }
            else
            {
                if (fOrder == 0) { fOrder = 0; }
                else { fOrder--; }
                psSetting_Weather(fOrder);
                Ch_Temp.Series[0].Points.DataBindXY(fTime, fTemp);
                
                
            }
        }

        private void psSetting_Weather(int brOrder)
        {
            if (fSet)
            {
                lbl_Temp.Text = fTemp[brOrder].ToString();
                lbl_Time.Text = fTime[brOrder];
                int Status = pfimage_checking(fWf[brOrder]);
                pb_weather.Image = iL_weather.Images[Status];
                try
                {
                    for (int sLoop = 0; sLoop < 21; sLoop++)
                    {
                        Ch_Temp.Series[0].Points[sLoop].Label = fTemp[sLoop].ToString();
                    }
                }
                catch (Exception)
                {

                }
                

            }
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }
    }
}
