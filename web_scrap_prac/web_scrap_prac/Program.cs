using HtmlAgilityPack;
using System;
using System.Net;

namespace web_scrap_prac
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 웹 페이지에서 데이터 가져오기
            string url = "https://www.naver.com";
            WebClient webClient = new WebClient();
            string html = webClient.DownloadString(url);

            // HTML 파싱하기
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            // XPath를 사용하여 실시간 검색어 선택하기
            HtmlNodeCollection nodes = htmlDoc.DocumentNode.SelectNodes("//div[@class='ah_roll_area PM_CL_realtimeKeyword_rolling_base']//span[@class='ah_k']");
            if (nodes != null)
            {
                Console.WriteLine("네이버 실시간 검색어:");
                foreach (HtmlNode node in nodes)
                {
                    // 선택한 요소에서 데이터 추출하기
                    string keyword = node.InnerText;
                    Console.WriteLine(keyword);
                }
            }
            else
            {
                Console.WriteLine("실시간 검색어를 가져올 수 없습니다.");
            }
        }
    }
}