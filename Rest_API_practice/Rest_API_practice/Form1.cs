using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Http;
using System.IO;
using Newtonsoft.Json.Linq;

namespace Rest_API_practice
{
    public partial class Form1 : Form
    {
        string sUrl  = "http://data.ex.co.kr/openapi/safeDriving/forecast?key=test&type=json";
        public Form1()
        {
            InitializeComponent();
        }

        public string callWebClient()
        {
            string result = string.Empty;
            try
            {
                WebClient client = new WebClient();

                //특정 요청 헤더값을 추가해준다. 
                client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");

                using (Stream data = client.OpenRead(sUrl))
                {
                    using (StreamReader reader = new StreamReader(data))
                    {
                        string s = reader.ReadToEnd();
                        result = s;

                        reader.Close();
                        data.Close();
                    }
                }

            }
            catch (Exception e)
            {
                //통신 실패시 처리로직
                Console.WriteLine(e.ToString());
            }
            return result;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string webClientResult = callWebClient();

            JObject sJobj = JObject.Parse(webClientResult);

            JArray sList = JArray.Parse(sJobj["list"].ToString());

            //foreach (var o in sList)
            //{
            //    richTextBox1.AppendText(string.Format("{0} : {1}\n", "날짜", o["sdate"]));
            //    richTextBox1.AppendText(string.Format("{0} : {1}\n", "전국교통량", o["cjunkook"]));
            //    richTextBox1.AppendText(string.Format("{0} : {1}\n", "지방교통량", o["cjibangDir"]));
            //    richTextBox1.AppendText(string.Format("{0} : {1}\n", "서울->대전 소요시간", o["csudj"]));
            //    richTextBox1.AppendText(string.Format("{0} : {1}\n", "서울->대구 소요시간", o["csudg"]));
            //    richTextBox1.AppendText(string.Format("{0} : {1}\n", "서울->울산 소요시간", o["csuus"]));
            //}

            JObject sJElement = JObject.Parse(sList[0].ToString());

            richTextBox1.AppendText(sJElement["sdate"].ToString());
            


            //richTextBox1.AppendText(webClientResult);


        }
    }
}
