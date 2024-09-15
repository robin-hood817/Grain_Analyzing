using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management;
using System.Security.Cryptography;
using System.Net;
using System.IO;
using System.Net.Sockets;
using System.Data.SqlClient;
using FTech_SentechEx;
using System.Threading;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using static System.Net.WebRequestMethods;

namespace Grain_Species_determination
{
    public partial class frm_Main : Form
    {
        private string fRootpath = "";
        private TCP_Class cl_TCP;
        private FtpWebResponse ftp_Resp;
        private frm_Register REGISTER;
        private frm_Processing LEARNING;
        private frm_Search SEARCH;
        private frm_Analysis ANALYSIS;
        private Form CURR;


        private Button btn_Mode;        
        private Dictionary<string, int> Dict_Tcp;

        private SentechEx[] _camera = null;
        private Mutex[] _mutexImage = null;
        private Bitmap[] _bitmap = null;
        public PictureBox pb_Curr;
        public delegate void PaintDelegate(Graphics g);

        private string fCon_Str;
        public bool fOnOff = false;
        private bool fconnection = false;
        private bool[] _isWorkingThreadCamera;

        public frm_Main()
        {
            InitializeComponent();
            fRootpath = Application.StartupPath;
            cl_TCP = new TCP_Class(fRootpath, "Setting.ini");
            REGISTER = new frm_Register();
            LEARNING = new frm_Processing();
            SEARCH = new frm_Search();
            ANALYSIS = new frm_Analysis();
            Dict_Tcp = new Dictionary<string, int>();
            


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            /*Mac Test
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                //MAC 주소 가져오기
               PhysicalAddress macAddress = nic.GetPhysicalAddress();
                string mac = string.Join(":", macAddress.GetAddressBytes().Select(b => b.ToString("X2")));

                richTextBox1.AppendText($"인터페이스: {nic.Name}, MAC 주소: {mac}\n");
            }
            */


            if (cl_TCP.gfSetting_socket("Setting.ini"))
            {
                // 매번 프로그램 실행 시 UUID 값을 받아 DB의 등록 테이블과 비교                
                // 내용

                fCon_Str = cl_TCP.gfSetting_DB("Setting.ini");
                //===========UUID 사용 안함 Machine Code 기계마다 생성==========//
                /*                
                                using (SqlConnection use_Sql = new SqlConnection(fCon_Str))
                                {
                                    string sSQL = $"select * from TB_Register where Mac_Addr = @prm_UUID";
                                    use_Sql.Open();
                                    using (SqlCommand use_SC = new SqlCommand())
                                    {
                                        use_SC.Connection = use_Sql;
                                        use_SC.CommandText = sSQL;
                                        use_SC.Parameters.AddWithValue("@prm_UUID", cl_TCP.fuuid);
                                        SqlDataReader sSDA = use_SC.ExecuteReader();
                                        sRegistered = sSDA.Read();

                                    }
                                }*/
                chart1.ChartAreas[0].AxisY.Maximum = 100;
                chart1.ChartAreas[0].AxisY.Minimum = 0;
                chart1.Series[0].Points.AddXY("고시히카리", 0);
                chart1.Series[0].Points.AddXY("신동진", 0);
                chart1.Series[0].Points.AddXY("조명 1호", 0);




                if (cl_TCP.MACHINE_CODE.Length < 12)
                {
                    REGISTER.ShowDialog(this);
                }
                else
                {
                    bool sRows = false;
                    using (SqlConnection use_Sql = new SqlConnection(fCon_Str))
                    {
                        string sSQL = $"select * from TB_Machine_Master where Machine_Code = @prm_UUID";
                        use_Sql.Open();
                        using (SqlCommand use_SC = new SqlCommand())
                        {
                            use_SC.Connection = use_Sql;
                            use_SC.CommandText = sSQL;
                            use_SC.Parameters.AddWithValue("@prm_UUID", cl_TCP.MACHINE_CODE);
                            SqlDataReader sRDR = use_SC.ExecuteReader();
                            sRows = sRDR.HasRows;

                        }
                    }

                    if (!sRows) { Close(); return; }

                    if (!cl_TCP.gfFTP_Exist(cl_TCP.FTP_Loginfo[0], cl_TCP.FTP_Loginfo[1], "ftp://control-ideal.iptime.org:2126/Learning", cl_TCP.MACHINE_CODE))
                    {
                        cl_TCP.gfFTPMakeDir(cl_TCP.FTP_Loginfo[0], cl_TCP.FTP_Loginfo[1], $"ftp://control-ideal.iptime.org:2126/Learning/{cl_TCP.MACHINE_CODE}");
                    }

                    if (!cl_TCP.gfFTP_Exist(cl_TCP.FTP_Loginfo[0], cl_TCP.FTP_Loginfo[1], "ftp://control-ideal.iptime.org:2126/Teaching", cl_TCP.MACHINE_CODE))
                    {
                        cl_TCP.gfFTPMakeDir(cl_TCP.FTP_Loginfo[0], cl_TCP.FTP_Loginfo[1], $"ftp://control-ideal.iptime.org:2126/Teaching/{cl_TCP.MACHINE_CODE}");
                    }
                    
                    for (int sLoop = 0; sLoop < cl_TCP.SOCKET_CNT1; sLoop++)
                    {
                        string sKey = $"{cl_TCP.IP[sLoop]}:{cl_TCP.PORT[sLoop]}";
                        Dict_Tcp.Add(sKey, sLoop);
                    }
                    fconnection = false;


                //    _camera = new SentechEx[1];
                    _mutexImage = new Mutex[1];
                    _bitmap = new Bitmap[1];
                    //_isWorkingThreadCamera = new bool[1];

                    /*for (int i = 0; i < 1; i++)
                    {
                        _camera[i] = new SentechEx();
                        _mutexImage[i] = new Mutex();
                    }*/

                    int index = 0;
                    //카메라 
                    try
                    {
/*                        _camera[index].OpenBySerial(cl_TCP.CAM_SERIAL);
                        long width = 0, height = 0;
                        width = _camera[index].Width;
                        height = _camera[index].Height;*/

                        /*CreateBitmap(index, (int)width, (int)height, PixelFormat.Format32bppRgb);
                        //CreateBitmap(index, (int)width, (int)height, PixelFormat.Format8bppIndexed);
                        _camera[index].SetValueEnumString("TriggerSelector", "FrameStart");//this has to be set first.
                        _camera[index].SetValueEnumString("AcquisitionMode", "Continuous");
                        _camera[index].SetValueEnumString("ExposureMode", "Timed");
                        _camera[index].SetValueFloat("ExposureTime", 40000);
                        _camera[index].SetValueEnumString("TriggerMode", "On");
                        _camera[index].SetValueEnumString("TriggerSource", "Software");
                        _camera[index].SetValueEnumString("LineSelector", "Line2");
                        _camera[index].SetValueEnumString("LineMode", "Output");
                        _camera[index].SetValueEnumString("LineSource", "ExposureActive");
                        _camera[index].SetEnableImageCallback(false);*/

                     //   _camera[index].Start();

                       // psGrabRun();

                        //tim_Video.Start();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("카메라를 확인해 주세요!");
                    }


                }

            }
        }
        private void CreateBitmap(int index, int width, int height, System.Drawing.Imaging.PixelFormat format)
        {
            if (format == PixelFormat.Format8bppIndexed)
            {
                _bitmap[index] = new Bitmap((int)width, (int)height, PixelFormat.Format8bppIndexed);
                if (_bitmap[index].PixelFormat == PixelFormat.Format8bppIndexed)
                {
                    ColorPalette colorPalette = _bitmap[index].Palette;
                    for (int i = 0; i < 256; i++)
                    {
                        colorPalette.Entries[i] = Color.FromArgb(i, i, i);
                    }

                    _bitmap[index].Palette = colorPalette;
                }
            }
            else
                //_bitmap[index] = new Bitmap((int)width, (int)height, PixelFormat.Format24bppRgb);Format32bppRgb
                _bitmap[index] = new Bitmap((int)width, (int)height, format);
        }
        void Redraw_Cam1(Graphics g)//this is the function that is called to draw the image on the screen
        {
            try
            {
                _mutexImage[0].WaitOne();

                if (_bitmap[0] != null)
                {
                    PictureBox sPbcam = pb_Cam1;
                    /*if (pan_Learning.Visible)
                    {
                        sPbcam = pbCam1;
                    }
                    else
                    {
                        sPbcam = pb_Cur;
                    }*/
                    g.DrawImage(_bitmap[0], 0, 0, _bitmap[0].Height, _bitmap[0].Width);
                    //g.DrawImage(_bitmap[0], 0, 0, 650, 1100);
                    //_frameCount++;
                    //lblFrameCount.Text = _frameCount.ToString();
                    //if(_frameCount%_framesPerGarlic == 0)  This was necessary when there were multiple shots
                    // _camera[0].ExecuteCommand("AcquisitionStart");
                    /*string sSeperation = btn_Mode == btn_Classify ? "Classify" : "Learning";
                    string sPath = $"{fRootpath}\\bin\\";
                    if (!Directory.Exists(sPath + "Classify")) Directory.CreateDirectory(sPath + "Classify");
                    if (!Directory.Exists(sPath + "Learning")) Directory.CreateDirectory(sPath + "Learning");*/
                    //_bitmap[0].Save(sPath + sSeperation + "\\" + DateTime.Now.ToString("yyyyMMdd-HHmmss") + "_" + sSeperation + ".jpg", ImageFormat.Jpeg);

                }
            }
            catch (System.Exception exc)
            {
                System.Diagnostics.Trace.WriteLine(exc.Message, "System Exception");
            }
            finally
            {
                _mutexImage[0].ReleaseMutex();
            }
        }
        private static void DisplayThread_Cam1(object aParameters)
        {
            object[] lParameters = (object[])aParameters;
            frm_Main lThis = (frm_Main)lParameters[0];

            while (lThis._isWorkingThreadCamera[0])
            {
                Thread.Sleep(10);

                EventWaitHandle handle = lThis._camera[0].HandleGrabDone;
                if (handle.WaitOne(100) == true)
                {
                    lThis._mutexImage[0].WaitOne();

                    BitmapData bmpData = lThis._bitmap[0].LockBits(new Rectangle(0, 0, lThis._bitmap[0].Width, lThis._bitmap[0].Height),
                        ImageLockMode.ReadWrite, lThis._bitmap[0].PixelFormat);

                    IntPtr ptrBmp = bmpData.Scan0;
                    int bpp = 8;
                    //if (lThis._bitmap[0].PixelFormat == PixelFormat.Format24bppRgb)
                    //{
                    bpp = 24;
                    Marshal.Copy(lThis._camera[0].ColorBuffer, 0, ptrBmp, lThis._bitmap[0].Width * lThis._bitmap[0].Height * bpp / 8);
                    //}
                    /*                    else
                                            Marshal.Copy(lThis._camera[0].Buffer, 0, ptrBmp, lThis._bitmap[0].Width * lThis._bitmap[0].Height * bpp / 8);*/
                    lThis._bitmap[0].UnlockBits(bmpData);

                    lThis.BeginInvoke(new PaintDelegate(lThis.Redraw_Cam1), new object[1] { lThis.pb_Cam1.CreateGraphics() });

                    lThis._camera[0].OnResetEventGrabDone();

                    lThis._mutexImage[0].ReleaseMutex();
                }
            }
        }
        private FtpWebResponse pfFtpConnect(String url, string method, Action<FtpWebRequest> action = null)
        {
            var request = WebRequest.Create(url) as FtpWebRequest;
            request.UseBinary = true;
            request.Method = method;
            request.Credentials = new NetworkCredential("controlap", "!qazwsx!@34");
            if (action != null)
            {
                action(request);
            }
            return request.GetResponse() as FtpWebResponse;
        }


        private void psGrabRun()
        {

            //bt_Analysis.Visible = true;
            //Button btn = (Button)sender;
            int index = 0;

            _camera[index].ExecuteCommand("TriggerSoftware");

            int bufferSize = Convert.ToInt32(_camera[index].Width * _camera[index].Height);

            byte[] image = null;
            image = new byte[bufferSize];

            image = _camera[index].Grab(1000);

            BitmapData bmpData = _bitmap[index].LockBits(new Rectangle(0, 0, _bitmap[index].Width, _bitmap[index].Height), ImageLockMode.ReadWrite, _bitmap[index].PixelFormat);
            IntPtr drawImage = bmpData.Scan0;


            Marshal.Copy(image, 0, drawImage, bufferSize);

            _bitmap[index].UnlockBits(bmpData);
            PictureBox sPbcam = pb_Cam1;
            /*if (pan_Learning.Visible)
            {
                sPbcam = pbCam1;
            }
            else
            {
                sPbcam = pb_Cur;
            }*/

            //  BeginInvoke(new PaintDelegate(Redraw_Cam1), new object[1] { sPbcam.CreateGraphics() });
            if (!ANALYSIS.fTaken)
            {
                Redraw_Cam1(sPbcam.CreateGraphics());
            }
            else
            {
                ANALYSIS.psDraw_pb(_bitmap);
                ANALYSIS.fTaken = false;
            }
            
        }



        private void timer1_Tick(object sender, EventArgs e)
        {
            // 시간 설정 필요


            //======================


            if (!fconnection)
            {
                foreach (int feVal in Dict_Tcp.Values)
                {
                    //         psCli_connect(cl_TCP.IP[feVal], cl_TCP.PORT[feVal]);
                }
            }

            string sPath = $"{fRootpath}\\bin\\{cl_TCP.MACHINE_CODE}";
            if (!Directory.Exists(sPath))
            {
                Directory.CreateDirectory(sPath);
            }
            string sBackup_Path = $"{sPath}\\{DateTime.Today.Day}Backup";
            if (!Directory.Exists(sPath))
            {
                Directory.CreateDirectory(sPath);
            }
            string sFTP_Path = $"{sPath}\\Send";
            if (!Directory.Exists(sPath))
            {
                Directory.CreateDirectory(sPath);
            }

        }

        /* private void psPageswitching(object sender, EventArgs e)
         {
             foreach (Label felbl in lbl_Select)
             {
                 felbl.BackColor = Color.Black;
                 felbl.ForeColor = Color.White;
             }
             Label lbl_curr = sender as Label;
             lbl_curr.BackColor = Color.White;
             lbl_curr.ForeColor = Color.Black;
             string sName = lbl_curr.Name;
             int sidx = int.Parse(sName.Substring(("lbl_list").Length));
             switch (sidx)
             {
                 case 1:// Search
                     if (CURR != null)
                     {
                         CURR.Hide();
                     }
                     SEARCH.Dock = DockStyle.Fill;
                     SEARCH.TopLevel = false;
                     CURR = SEARCH;
                     pan_Search.Controls.Add(SEARCH);
                     SEARCH.Show();
                     break;
                 case 2://Learning
                     if (CURR != null)
                     {
                         CURR.Hide();
                     }
                     LEARNING.Dock = DockStyle.Fill;
                     LEARNING.TopLevel = false;
                     CURR = LEARNING;
                     pan_Search.Controls.Add(LEARNING);
                     LEARNING.Show();
                     break;
                 case 3://History
                     if (CURR != null)
                     {
                         CURR.Hide();
                     }
                     HISTORY.Dock = DockStyle.Fill;
                     HISTORY.TopLevel = false;
                     CURR = HISTORY;
                     pan_Search.Controls.Add(HISTORY);
                     HISTORY.Show();
                     break;
                 default:
                     break;
             }
         }*/

        #region TCP
        private async void psCli_connect(string brIP, int brPort)
        {
            try
            {
                TcpClient tc_Cli = new TcpClient();

                IPAddress sIA = IPAddress.Parse(brIP);

                await tc_Cli.ConnectAsync(sIA, brPort);

                _ = psClientWork(tc_Cli);
            }
            catch (Exception)
            {

            }
        }

        private async Task psClientWork(TcpClient brCli)
        {
            NetworkStream NS_Cli = brCli.GetStream();
            //"192.168.10.243:100"            
            string sIP = brCli.Client.RemoteEndPoint.ToString();
            int sidx = -1;
            ///sData_dist의 값은 곡물 흐름과 진동 센서의 구분 값
            ///true = 진동 센서
            ///false = 곡물 흐름
            bool sData_dist;
            byte[] sbuffer = new byte[4096];
            int sRead;
            string sRec_str = "";
            while ((sRead = await NS_Cli.ReadAsync(sbuffer, 0, sbuffer.Length)) > 0 && sidx > -1)
            {
                fconnection = true;

                byte[] sReal = new byte[sRead];
                Array.Copy(sbuffer, sReal, sRead);

                sRec_str = BitConverter.ToString(sReal);
                sRec_str = sRec_str.Replace("-", "");

                //서버에서 판정 값과 각 설정 값을 받아온다.




                //Thread.Sleep(10);
            }

            fconnection = false;


        }
        #endregion

        private void btn_Learning_Click(object sender, EventArgs e)
        {
            using (SqlConnection use_sqlcon = new SqlConnection(fCon_Str))
            {
                use_sqlcon.Open();
                string sSQL = "select * from TB_Grain_Code where Machine_Code = @Code";
                string sKind = "";
                using (SqlCommand use_SC = new SqlCommand(sSQL, use_sqlcon))
                {
                    use_SC.Parameters.AddWithValue("@Code", cl_TCP.MACHINE_CODE);

                    SqlDataReader sSDA = use_SC.ExecuteReader();
                    int sLoop = 0;
                    bool sData_Saved = false;
                    while (sSDA.Read())
                    {
                        sData_Saved = true;
                        //cb_Grain[sLoop].Text = $"{sSDA["Grain_Kind"]}";
                        sLoop++;
                    }

                    if (sData_Saved)
                    {
                    //    pan_Learning.Visible = true;
                    }
                }


            }


        }
        private void btn_take_Click(object sender, EventArgs e)
        {
            psGrabRun();
        }

        private void frm_Main_DoubleClick(object sender, EventArgs e)
        {
            fOnOff = false;
            this.Close();
        }

        #region FTP function

        private bool pfFtp_upload(string brStart, string brDestination, string brID, string brpassword)
        {

            FtpWebRequest ftp = (FtpWebRequest)WebRequest.Create("ftp://control-ideal.iptime.org/HDD3/Data/HMI/정성운_백업/에프티피.txt");
            ftp.Method = WebRequestMethods.Ftp.UploadFile;

            ftp.Credentials = new NetworkCredential(brID, brpassword);

            StreamReader fileToSend = new StreamReader("D:\\현재 프로젝트 모음\\c#\\Log_Del\\Log_Del\\bin\\Debug\\에프티피.txt");
            byte[] fileContents = Encoding.UTF8.GetBytes(fileToSend.ReadToEnd());
            //fileToSend.Close(); //Close the open file

            Stream sendFile = ftp.GetRequestStream();
            sendFile.Write(fileContents, 0, fileContents.Length);
            sendFile.Close(); //Close the sending stream

            FtpWebResponse ftpResponse = (FtpWebResponse)ftp.GetResponse();
            //            listBox1.Items.Add(ftpResponse.StatusDescription); //Output the results of the transfer
            ftpResponse.Close();

            return true;
        }



        #endregion

        private void button3_Click_1(object sender, EventArgs e)
        {
            _camera[0].Stop();
            _camera[0].Close();
            this.Close();
            fOnOff = false;
        }

        private void tim_Video_Tick(object sender, EventArgs e)
        {
            //psGrabRun();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void psAnalyze_press(object sender, MouseEventArgs e)
        {
            PictureBox sPb = sender as PictureBox;
            sPb.Image = Properties.Resources.p_Analysis;

        }

        private void psAnalyze_unpress(object sender, MouseEventArgs e)
        {
            PictureBox sPb = sender as PictureBox;
            sPb.Image = Properties.Resources.n_Analysis;
        }

        private void psKind_Learn_press(object sender, MouseEventArgs e)
        {
            PictureBox sPb = sender as PictureBox;
            sPb.Image = Properties.Resources.p_Kind_Learning;
        }

        private void psKind_Learn_unpress(object sender, MouseEventArgs e)
        {
            PictureBox sPb = sender as PictureBox;
            sPb.Image = Properties.Resources.n_Kind_Learning;

            //frm_Analysis 띄우기
            //부모 자식관계로 본 폼에서 사진을 찍고 비트맵만 넘긴다.
            //학습 창에서 촬영을 누를 경우 bool 값 변경 타이머로 계속 확인 필요
            //초기화가 필요한 컴포넌트
            /// listview
            /// image count
            /// Kind labels
            ANALYSIS.fSql_constr = fCon_Str;
            ANALYSIS.fMachine_Code = cl_TCP.MACHINE_CODE;
            tim_Take.Enabled = true;
            ANALYSIS.Show();
        }

        private void tim_Take_Tick(object sender, EventArgs e)
        {
            if (ANALYSIS.fTaken)
            {
                try
                {
                    psGrabRun();
                    ANALYSIS.fTaken = false;
                }
                catch (Exception)
                {
                    //카메라 셋칭
                }
                
                
            }
            
        }
    }
}
