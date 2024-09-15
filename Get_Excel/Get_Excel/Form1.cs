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
using Excel = Microsoft.Office.Interop.Excel;

namespace Get_Excel
{
    public partial class Form1 : Form
    {
        static Excel.Application excelApp = null;
        static Excel.Workbook workBook = null;
        static Excel.Worksheet workSheet = null;
        string fRootPath;
        string fExcelPath;
        Dictionary<string, string> fCode_region;
        
        int fCurr = 0;

        public Form1()
        {
            InitializeComponent();
            fRootPath = Application.StartupPath;
            fExcelPath = fRootPath + "\\bin\\Code_List.csv"; 
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            fCode_region = new Dictionary<string, string>();
            
            /* excelApp = new Excel.Application();                             // 엑셀 어플리케이션 생성
             workBook = excelApp.Workbooks.Open(fExcelPath);                       // 워크북 열기
             workSheet = workBook.Worksheets.get_Item(1) as Excel.Worksheet;

             Excel.Range range = workSheet.UsedRange;

            for (int i = 1 ; i < 5; i++)
             {
                 Control sName = pfGet_Label(i.ToString());
                 if (sName != null)
                 {
                     sName.Text = (string)(range.Cells[3, i] as Excel.Range).Value2;
                 }
             }*/

            StreamReader sr = new StreamReader(fExcelPath, Encoding.Default);
            while (!sr.EndOfStream)
            {
                string sStr = sr.ReadLine();
                string sConfirm = sStr.Substring(sStr.Length-3, 2);
                if (sConfirm != "폐지")
                {
                    //지역 별 코드 입력
                    string sCode = sStr.Substring(1, 10);
                    //지역 입력
                    string sAddr = sStr.Substring(12, sStr.Length - 16);
                    comboBox2.Items.Add(sAddr);

                    fCode_region.Add(sAddr, sCode);
                }

                
            }



            /* for (int row = 4; row < range.Rows.Count -4; row++)
             {
                 try
                 {
                     int sCurr = (range.Cells[row, 1] as Excel.Range).Value2;
                     if (fCity.ContainsKey(sCurr))
                     {
                         fCity.Add(sCurr,
                                     (string)(range.Cells[row, 2] as Excel.Range).Value2);

                     }
                     sCurr = (range.Cells[row, 3] as Excel.Range).Value2;
                     if (fRegion.ContainsKey(sCurr))
                     {
                         fRegion.Add(sCurr,
                                    (string)(range.Cells[row, 4] as Excel.Range).Value2);
                     }
                     sCurr = (range.Cells[row, 3] as Excel.Range).Value2;
                     if (fDong.ContainsKey(sCurr))
                     {
                         fRegion.Add(sCurr,
                                    (string)(range.Cells[row, 4] as Excel.Range).Value2);
                     }

                 }
                 catch (Exception)
                 {

                    MessageBox.Show(row.ToString());
                 }

             }*/

        }

        Control pfGet_Label(string brNo)
        {
            foreach (Control control in this.Controls)
            {
                if (control is Label)
                {
                    string sTag = control.Tag != null ? control.Tag.ToString():" "; 
                    if (sTag == brNo)
                    {                        
                        return control;
                    }
                    
                    
                }
            }


            return null;
        }

        private void Form1_Click(object sender, EventArgs e)
        {

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sRegion="";
            if (comboBox2.SelectedIndex != 0)
            {
                sRegion = comboBox2.Items[comboBox2.SelectedIndex].ToString();
            }
            

            List<string> sCode = new List<string>(fCode_region.Keys);

            foreach (string cRegion in sCode)
            {
                if (cRegion == sRegion)
                {
                    lbl_Codeshow.Text=fCode_region[sRegion];
                } 
            }
        }
    }
}
