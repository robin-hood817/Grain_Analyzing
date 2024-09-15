using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Windows.Forms;
using System.Xml;

namespace Weather_Broad
{
    internal class Weather_Api
    {
        public string[] fAver;
        public string[] fTemp;
        public int fCout;
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

            XmlNodeList nodes = document.DocumentElement.SelectNodes("descendant::location");

            fAver = new string[nodes.Count];
            fTemp = new string[nodes.Count];
            int sLoop = 0;
            foreach (XmlNode node in nodes)
            {
                
                var getData = node.SelectSingleNode("Temp");
                var Status = node.SelectSingleNode("wfKor");
                fAver[sLoop] = getData.ToString();
                fTemp[sLoop] = Status.ToString();
                sLoop++;


            }
        }
    }
}
