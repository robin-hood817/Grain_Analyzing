using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;
using System.Net;

namespace JSW_Honam_seed
{
    public class TCP_Class
    {
        IniFile gIni = new IniFile();
        public Socket gServer;
        public IPAddress gAddr;
        public IPEndPoint gEndpoint;

        private string[] gIp;
        private string[] gIp2;
        private string[] gInstall;
        private string gRootpath;
        
        private int gEV_Max = 0;
        private int gEV_Min = 0;
        private int gConv_Max = 0;
        private int gConv_Min = 0;
        private int gFlow_Delay = 0;
        private int gData_Count1 = 0;
        private int gData_Count2 = 0;
        private int gSock_Count1 = 0;
        private int gSock_Count2 = 0;
        private int[] gPort;
        private int[] gPort2;

        private bool gMode = false;



        //생성자에서 파라미터를 프로그램 경로 값을 받는다.
        public TCP_Class(string brRootpath)
        {
            this.gRootpath = $"{brRootpath}\\bin\\";
        }


        public int[] PORT { get { return gPort; } }
        public string[] IP { get { return gIp; } }
        public int[] PORT2 { get { return gPort2; } }
        public string[] IP2 { get { return gIp2; } }
        public int EV_Y_Max { get { return gEV_Max; } }
        public int EV_Y_Min { get { return gEV_Min; } }
        public int Conv_Y_Max { get { return gConv_Max; } }
        public int Conv_Y_Min { get { return gConv_Min; } }
        public string[] INSTALL { get { return gInstall; } }
        public string ROOTPATH{ get { return this.gRootpath;}}
        public int FLOW_TIME { get { return gFlow_Delay; } }
        public int Recon_time1 { get { return gData_Count1; } }
        public int Recon_time2 { get { return gData_Count2; } }
        public int SOCKET_CNT1 { get { return gSock_Count1; } }
        public int SOCKET_CNT2 { get { return gSock_Count2; } }
        public bool MODE { get { return gMode; } }

        //the value of Param is for 
        //
        public bool gfSetting_socket(string brNameini) // 기본 소켓 설정을 위한 값만 셋팅한다.
        {
            bool sResult = File.Exists(gRootpath+brNameini);
            if (sResult)
            {
                gIni.Load(gRootpath + brNameini);
                gSock_Count1 = gIni["Inform"]["Count"].ToInt();
                gSock_Count2 = gIni["Flow"]["Count"].ToInt();
                gIp = new string[gSock_Count1];
                gPort = new int[gSock_Count1];
                gIp2 = new string[gSock_Count2];
                gPort2 = new int[gSock_Count2];
                for (int sLoop = 0; sLoop < gSock_Count1; sLoop++)
                {
                    gIp[sLoop] = gIni["Inform"][$"Ip{sLoop}"].ToString();
                    gPort[sLoop] = gIni["Inform"][$"Port{sLoop}"].ToInt();
                }
                for (int sLoop = 0; sLoop < gSock_Count2; sLoop++)
                {                    
                    gIp2[sLoop] = gIni["Flow"][$"Ip{sLoop}"].ToString();
                    gPort2[sLoop] = gIni["Flow"][$"Port{sLoop}"].ToInt();
                }
                gEV_Max = gIni["Setting"]["EV_Max"].ToInt();
                gEV_Min = gIni["Setting"]["EV_Min"].ToInt();
                gConv_Max = gIni["Setting"]["Conv_Max"].ToInt();
                gConv_Min = gIni["Setting"]["Conv_Min"].ToInt();
                int sCount = gIni["Installation"]["Count"].ToInt();
                gInstall = new string[sCount];
                for (int sLoop = 0; sLoop < sCount; sLoop++)
                {
                    gInstall[sLoop] = gIni["Installation"][$"EV_{sLoop}"].ToString();
                }
                gFlow_Delay = gIni["Setting"]["Delay"].ToInt();
                gData_Count1 = gIni["Setting"]["Recon_Delay1"].ToInt();
                gData_Count2 = gIni["Setting"]["Recon_Delay2"].ToInt();
                gMode = gIni["Setting"]["MODE"].ToString() == "M";
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
        public string  gfSetting_DB()
        {
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

        //=============뒤에 stx와 etx를 각각 넣어 패킷의 처음으로 잘랐기 때문에 끝을 찾아 위치 값을 반환한다========//
        public int gfConfirm_packet(string brmsg, byte brETX)
        {
            int sResult;

            int sETX;

            // sSTX = brmsg.IndexOf(Convert.ToChar(brSTX));
            sETX = brmsg.IndexOf(Convert.ToChar(brETX));


            //======STX값이라도 있을경우 추후 값을 받도록 한다========//
            if (sETX == -1) { sResult = 0; }
            //======둘다 값이 들어왔다고 판단하고 진행========//
            else { sResult = sETX; }



            return sResult;
        }

    }
}
