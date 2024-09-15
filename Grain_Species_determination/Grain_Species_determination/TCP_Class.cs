using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.Management;

namespace Grain_Species_determination
{
    public class TCP_Class
    {
        private int[] gPort;
        private int[] gPort2;
        private string[] gIp;
        private string[] gIp2;
        private string[] gInstall;
        private string gRootpath;
        private string gFTP_Server;
        private string[] gFTP_Login;
        public string fuuid = "";
        private string gMachine_Code;
        private string gCam_serial;
        IniFile gIni = new IniFile();
        public Socket gServer;
        public IPAddress gAddr;
        public IPEndPoint gEndpoint;
        private int gEV_Max = 0;
        private int gEV_Min = 0;
        private int gConv_Max = 0;
        private int gConv_Min = 0;
        private int gFlow_Delay = 0;
        private int gData_Count1 = 0;
        private int gData_Count2 = 0;
        private int gSock_Count1 = 0;
        private int gSock_Count2 = 0;

        //생성자에서 파라미터를 프로그램 경로 값을 받는다.
        public TCP_Class(string brRootpath, string brSetting)
        {
            this.gRootpath = $"{brRootpath}\\bin\\";
            gfSetting_socket(brSetting);
        }


        public int[] PORT { get { return gPort; } }
        public string[] IP { get { return gIp; } }
        public int[] PORT2 { get { return gPort2; } }
        public string[] IP2 { get { return gIp2; } }
        public string[] INSTALL { get { return gInstall; } }
        public string ROOTPATH{ get { return this.gRootpath;}}
        public string FTP_URL { get { return this.gFTP_Server; } }
        public string[] FTP_Loginfo { get { return this.gFTP_Login; } }
        public string MACHINE_CODE { get { return this.gMachine_Code; } }
        public int FLOW_TIME { get { return gFlow_Delay; } }
        public int Recon_time1 { get { return gData_Count1; } }
        public int Recon_time2 { get { return gData_Count2; } }
        public int SOCKET_CNT1 { get { return gSock_Count1; } }
        public int SOCKET_CNT2 { get { return gSock_Count2; } }
        public string CAM_SERIAL { get { return gCam_serial; } }
        //the value of Param is for 
        //
        public bool gfSetting_socket(string brNameini) // 기본 소켓 설정을 위한 값만 셋팅한다.
        {
            bool sResult = File.Exists(gRootpath+brNameini);
            if (sResult)
            {
                gIni.Load(gRootpath + brNameini);
                gSock_Count1 = gIni["Inform"]["Count"].ToInt();
                //gSock_Count2 = gIni["Flow"]["Count"].ToInt();
                gIp = new string[gSock_Count1];
                gPort = new int[gSock_Count1];
                for (int sLoop = 0; sLoop < gSock_Count1; sLoop++)
                {
                    gIp[sLoop] = gIni["Inform"][$"Ip{sLoop}"].ToString();
                    gPort[sLoop] = gIni["Inform"][$"Port{sLoop}"].ToInt();
                }
                gFTP_Server = gIni["FTP"]["DNS"].ToString();
                gFTP_Login = new string[2];
                gFTP_Login[0] = gIni["FTP"]["ID"].ToString();
                gFTP_Login[1] = gIni["FTP"]["PWD"].ToString();
                #region Convertable

                //gIp2 = new string[gSock_Count2];
                //gPort2 = new int[gSock_Count2];                
                //for (int sLoop = 0; sLoop < gSock_Count2; sLoop++)
                //{                    
                //    gIp2[sLoop] = gIni["Flow"][$"Ip{sLoop}"].ToString();
                //    gPort2[sLoop] = gIni["Flow"][$"Port{sLoop}"].ToInt();
                //}
                //gEV_Max = gIni["Setting"]["EV_Max"].ToInt();
                //gEV_Min = gIni["Setting"]["EV_Min"].ToInt();
                //gConv_Max = gIni["Setting"]["Conv_Max"].ToInt();
                //gConv_Min = gIni["Setting"]["Conv_Min"].ToInt();
                //int sCount = gIni["Installation"]["Count"].ToInt();
                //gInstall = new string[sCount];
                //for (int sLoop = 0; sLoop < sCount; sLoop++)
                //{
                //    gInstall[sLoop] = gIni["Installation"][$"EV_{sLoop}"].ToString();
                //}
                //gFlow_Delay = gIni["Setting"]["Delay"].ToInt();
                //gData_Count1 = gIni["Setting"]["Recon_Delay1"].ToInt();
                //gData_Count2 = gIni["Setting"]["Recon_Delay2"].ToInt();
                #endregion

                gMachine_Code = gIni["Inform"]["Code"].ToString();
                gCam_serial = gIni["Inform"]["Camera_Serial"].ToString();
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT UUID FROM Win32_ComputerSystemProduct");                
                foreach (ManagementObject obj in searcher.Get())
                {
                    fuuid = obj["UUID"].ToString();
                }
                gIni["UUID"]["UUID"] = fuuid;
                gIni.Save(gRootpath+brNameini);
                
                return true;
            }
            else
            {
                return false;
            }
            
            
            /// 기본 ini 폼
            /// [Inform]
            /// Count=1
            /// Ip0=127.0.0.1
            /// Port0=100
        }
        public string  gfSetting_DB(string brNameini)
        {
            gIni.Load(gRootpath+brNameini);

            string[] sDB_consist = new string[4];
            
            sDB_consist[0] = gIni["DB"]["Server"].ToString();
            sDB_consist[1] = gIni["DB"]["Database"].ToString();
            sDB_consist[2] = gIni["DB"]["uid"].ToString();
            sDB_consist[3] = gIni["DB"]["pwd"].ToString();

            string sCon_Str = string.Format(
                "Server={0};" 
                +" Database={1};"
                +" uid={2};"
                +" pwd={3}"
                , sDB_consist[0]
                , sDB_consist[1]
                , sDB_consist[2]
                , sDB_consist[3]);
            return sCon_Str;
            
        }

        public void gsRegist_Code(string brCode, string brName)
        {
            gIni.Load(gRootpath + brName);
            gIni["Inform"]["Code"] = brCode;
            gIni.Save(gRootpath + brName);

        }
        public void gfFTPMakeDir(string brID, string brPWD, string brPath)
        {                                                                      //ftp://ip:포트번호/폴더명/
            FtpWebRequest requestFTPUploader = (FtpWebRequest)WebRequest.Create(brPath);

            requestFTPUploader.Credentials = new NetworkCredential(brID, brPWD);

            var request = requestFTPUploader;

            request.Method = WebRequestMethods.Ftp.MakeDirectory;

            try
            {
                using (var resp = (FtpWebResponse)request.GetResponse())
                {
                    resp.Close();
                }
            }
            catch (WebException e)
            {
            }

        }
        public bool gfFTP_Exist(string brID, string brPWD, string brConfirmPath, string brSearchDir)
        {
            bool sResult = false;
            FtpWebRequest ftp = (FtpWebRequest)WebRequest.Create(brConfirmPath);
            ftp.Method = WebRequestMethods.Ftp.ListDirectory;
            ftp.Credentials = new NetworkCredential(brID, brPWD);
            ftp.UseBinary = true;
            WebResponse sRes = ftp.GetResponse();
            Stream sSendFile = sRes.GetResponseStream();



            using (StreamReader use_SR = new StreamReader(sSendFile, Encoding.Default))
            {
                while (true)
                {
                    string buf = use_SR.ReadLine();
                    if (string.IsNullOrWhiteSpace(buf))
                    {

                        break;
                    }
                    if (buf == brSearchDir)
                    {
                        sResult = true;
                    }
                }
            }

            return sResult;

        }



        //private FtpWebResponse psFtpConnect(String url, string method, Action<FtpWebRequest> action = null)
        //{
        //    var request = WebRequest.Create(url) as FtpWebRequest;
        //    request.UseBinary = true;
        //    request.Method = method;
        //    request.Credentials = new NetworkCredential("controlap", "!qazwsx!@34");
        //    if (action != null)
        //    {
        //        action(request);
        //    }
        //    return request.GetResponse() as FtpWebResponse;
        //}


        //private void btn_Connect_Click(object sender, EventArgs e)
        //{
        //    string sUrl = textBox1.Text;
        //    using (var res = psFtpConnect(sUrl, WebRequestMethods.Ftp.ListDirectory))
        //    {
        //        using (var stream = res.GetResponseStream())
        //        {
        //            using (var rd = new StreamReader(stream))
        //            {
        //                while (true)
        //                {
        //                    string buf = rd.ReadLine();
        //                    if (string.IsNullOrWhiteSpace(buf))
        //                    {
        //                        break;
        //                    }
        //                    listBox1.Items.Add(buf);
        //                }
        //            }
        //        }
        //    }
        //}
    }
}
