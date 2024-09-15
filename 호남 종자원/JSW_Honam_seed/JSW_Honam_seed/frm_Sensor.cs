using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.IO.Ports;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;
using System.Timers;
using System.Collections;
using static System.Windows.Forms.AxHost;
using static System.Net.WebRequestMethods;

namespace JSW_Honam_seed
{
    public partial class frm_Sensor : Form
    {
        Sensor_data[] cl_SD; // 진동 센서
        Sensor_data[] cl_Flow;
        TCP_Class cl_TCP;        
        private Dictionary<string, int> Dict_vibe;
        private Dictionary<string, int> Dict_grainflow;
        private System.Timers.Timer tim_Send;
        private TcpClient[] tcp_Vibe;
        private TcpClient[] tcp_Flow;
        
        private Chart[] chrt_EV;
        private PictureBox[] pb_Flow;
        private PictureBox[] pb_Block;

        
        private bool[] fRec_con;
        private bool[] fVibe_con;
        private bool[] fFlow_con;
        private int[] fFlow_check;
        private int[] fVibe_check;
        private int[] fRe_Send;
        private bool fLogging=false;
        private bool fMode = false;
        private string[] fRec_Flow;
        private string[] fRec_Vibe;
        private bool[] fImg_switch;


        string[] fSTX_list = new string[]
        {
            "5551",
            "5552",
            "5553"
        };
        #region gyro Command packet
        private byte[] fKisan_RTU = new byte[]
        { 0x01, 0x04, 0x00, 0x80, 0x00, 0x01, 0x30, 0x22 };
        byte[] fgyro_Calibration = new byte[] { 0xFF, 0xAA, 0x67 };
        byte[] fgyro_H_install = new byte[] { 0xFF, 0xAA, 0x65 };
        byte[] fgyro_V_install = new byte[] { 0xFF, 0xAA, 0x66 };
        #endregion

        private string fRootpath = "";              
        private double fDist = 0;
       
        

        public frm_Sensor()
        {
            InitializeComponent();
            Dict_vibe = new Dictionary<string, int>();
            Dict_grainflow = new Dictionary<string, int>();                                    
            fRec_con = Enumerable.Repeat(true, 16).ToArray();// new bool[16] { true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true};
            fVibe_con = Enumerable.Repeat(false, 16).ToArray();
            fFlow_con = Enumerable.Repeat(false, 16).ToArray();
            fRe_Send= Enumerable.Repeat(0, 16).ToArray();
            fImg_switch = Enumerable.Repeat(false, 13).ToArray();

            fFlow_check = new int[16];
            fVibe_check = new int[16];
            cl_SD = new Sensor_data[16];
            cl_Flow = new Sensor_data[16];
            for (int sLoop = 0; sLoop < 16; sLoop++)
            {
                cl_SD[sLoop] = new Sensor_data();
                cl_Flow[sLoop] = new Sensor_data();
            }

            chrt_EV = new Chart[]
            {
                chrt_EV1,
                chrt_EV2,
                chrt_EV3,
                chrt_EV4,
                chrt_EV5,
                chrt_EV6,
                chrt_EV7,
                chrt_EV8,
                chrt_EV9,
                chrt_EV10,
                chrt_EV11,
                chrt_EV12,
                chrt_EV13,
                chrt_Conv1,
                chrt_Conv2,
                chrt_Conv3
            };
            pb_Flow = new PictureBox[]
            {
                pb_flow1,
                pb_flow2,
                pb_flow3,
                pb_flow4,
                pb_flow5,
                pb_flow6,
                pb_flow7,
                pb_flow8,
                pb_flow9,
                pb_flow10,
                pb_flow11,
                pb_flow12,
                pb_flow13
            };
            pb_Block = new PictureBox[]
            {
                pb_Block1,
                pb_Block2,
                pb_Block3,
                pb_Block4,
                pb_Block5,
                pb_Block6,
                pb_Block7,
                pb_Block8,
                pb_Block9,
                pb_Block10,
                pb_Block11,
                pb_Block12,
                pb_Block13,
                pb_Block14,
                pb_Block15,
                pb_Block16
            };
            
            
            fRootpath = Application.StartupPath;
            cl_TCP = new TCP_Class(fRootpath);

            fRec_Flow = new string[16];
            fRec_Vibe = new string[16];
            // frmLOAD = new frm_Loading();

        }

        private void frm_Sensor_Load(object sender, EventArgs e)
        {          
            
            
            if (cl_TCP.gfSetting_socket("Setting.ini"))
            {
                
                foreach (PictureBox fepb in pb_Flow)
                {
                    fepb.Image = Properties.Resources.stable;
                }
                int sLoop = 0;
                foreach (string feIP in cl_TCP.IP)
                {
                    //   psCli_connect(feIP, cl_TCP.PORT[sLoop]);
                    string sKey = $"{feIP}:{cl_TCP.PORT[sLoop]}";                    
                    Dict_vibe.Add(sKey, sLoop);  
                    
                    sLoop++;
                }
                sLoop = 0;
                foreach (string feIP in cl_TCP.IP2)
                {
                    //   psCli_connect(feIP, cl_TCP.PORT[sLoop]);
                    string sKey = $"{feIP}:{cl_TCP.PORT2[sLoop]}";                    
                    Dict_grainflow.Add(sKey, sLoop);
                    sLoop++;
                }

                for (int sLoop2 = 0; sLoop2 < 13; sLoop2++)
                {
                    chrt_EV[sLoop2].ChartAreas[0].BackImage = fRootpath + "\\bin\\middleline.png";
                    chrt_EV[sLoop2].ChartAreas[0].AxisY.Maximum = cl_TCP.EV_Y_Max;
                    chrt_EV[sLoop2].ChartAreas[0].AxisY.Minimum = cl_TCP.EV_Y_Min;
                    chrt_EV[sLoop2].ChartAreas[0].AxisX.Maximum = 100;
                    chrt_EV[sLoop2].ChartAreas[0].AxisX.Minimum = 0;
                    pb_Block[sLoop].Image = Properties.Resources.KakaoTalk_20240729_150845351_01;
                }
                for (int sLoop2 = 13; sLoop2 < 16; sLoop2++)
                {
                    chrt_EV[sLoop2].ChartAreas[0].BackImage = fRootpath + "\\bin\\middleline.png";
                    chrt_EV[sLoop2].ChartAreas[0].AxisY.Maximum = cl_TCP.Conv_Y_Max;
                    chrt_EV[sLoop2].ChartAreas[0].AxisY.Minimum = cl_TCP.Conv_Y_Min;
                    chrt_EV[sLoop2].ChartAreas[0].AxisX.Maximum = 100;
                    chrt_EV[sLoop2].ChartAreas[0].AxisX.Minimum = 0;
                    pb_Block[sLoop].Image = Properties.Resources.Block_normal;
                }
                //Reconnection timer                
                
                tim_Reconnection.Interval = 1000;                
                tim_Reconnection.Start();

                if (cl_TCP.MODE)
                {
                    //Packet Send timer
                    tim_Send = new System.Timers.Timer();
                    tim_Send.Interval = 300;
                    tim_Send.Elapsed += new ElapsedEventHandler(psSend_IO);
                }
                
                tim_UI_Tick(sender, e);
            }
            else // ini 설정 파일 없을 시 확인 문구 띄운 후 프로그램 종료
            {
                MessageBox.Show("ini 파일이 존재하지 않습니다.");
                this.Close(); return;
            }

        }


        private async void psCli_connect(string brIP, int brPort, int bridx)
        {
            TcpClient tc_Cli = new TcpClient();
            /// 값이 들어올 경우 해당 데이터의 endpoint를 체크할 수 있다.
            /// 그럴 경우 endpoint의 형태는 IP:POrt이기 때문에 해당 값을 key 값으로 잡아 index check         
            try
            {
                IPAddress sIA = IPAddress.Parse(brIP);
                if (cl_TCP.MODE)
                {
                    if (brPort >= 20000)
                    {

                        string sPath = $"{cl_TCP.ROOTPATH}Log\\FiberSensor\\{DateTime.Now.ToString("yyyy-MM-dd")}";
                        sPath += $"\\Event_{DateTime.Now.ToString("MM-dd")}_{bridx}.csv";
                        using (StreamWriter use_SR = new StreamWriter(sPath, true, Encoding.Default))
                        {
                            use_SR.WriteLine($"{DateTime.Now},EV_Vibe{bridx}번,Connecting");
                        }
                    }
                    else
                    {
                        string sPath = $"{cl_TCP.ROOTPATH}Log\\GyroSensor\\{DateTime.Now.ToString("yyyy-MM-dd")}";
                        sPath += $"\\Event_{DateTime.Now.ToString("MM-dd")}_{bridx}.csv";
                        using (StreamWriter use_SR = new StreamWriter(sPath, true, Encoding.Default))
                        {
                            use_SR.WriteLine($"{DateTime.Now},EV_Fiber{bridx}번,Connecting");
                        }
                    }

                }


                await tc_Cli.ConnectAsync(sIA, brPort);
            }
            catch (Exception)
            {

/*                string sPath = $"{cl_TCP.ROOTPATH}Log\\GyroSensor\\{DateTime.Now.ToString("yyyy-MM-dd")}";
                sPath += $"\\Event_{DateTime.Now.ToString("MM-dd")}_{bridx}.csv";

                using (StreamWriter use_SR = new StreamWriter(sPath, true, Encoding.Default))
                {
                    use_SR.WriteLine($"{DateTime.Now},Connection,Failed");
                }*/
                
            }                     
            _ = psClientWork(tc_Cli);
            
            
        }

        private void psSend_IO(object sender, ElapsedEventArgs e)
        {
            foreach (int fetup in Dict_grainflow.Values)            
            {                
                if (fFlow_con[fetup] && fRec_con[fetup])
                {                    
                    NetworkStream NS_Cli = tcp_Flow[fetup].GetStream();
                    try
                    {
                        NS_Cli.Write(fKisan_RTU, 0, fKisan_RTU.Length);
                        fRec_con[fetup] = false;
                        string sPath = $"{cl_TCP.ROOTPATH}Log\\FiberSensor\\{DateTime.Now.ToString("yyyy-MM-dd")}";
                        sPath += $"\\Data_{DateTime.Now.ToString("MM-dd")}_{fetup}.csv";
                        if (fLogging && cl_TCP.MODE)
                        {
                            using (StreamWriter use_SR = new StreamWriter(sPath, true, Encoding.Default))
                            {
                                string sPacket = "'0104008000013022";
                                use_SR.WriteLine($"{DateTime.Now},Send,{sPacket}");
                            }
                        }
                        
                    }
                    catch (Exception)
                    {
                        fRec_con[fetup] = true;

                    }

                }

               


                
                
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

            if (Dict_vibe.ContainsKey(sIP))
            {
                sidx = Dict_vibe[sIP];
                sData_dist = true;
                if (cl_TCP.MODE)
                {
                    string sPath = $"{cl_TCP.ROOTPATH}Log\\GyroSensor\\{DateTime.Now.ToString("yyyy-MM-dd")}";
                    sPath += $"\\Event_{DateTime.Now.ToString("MM-dd")}_{sidx}.csv";

                    using (StreamWriter use_SR = new StreamWriter(sPath, true, Encoding.Default))
                    {
                        use_SR.WriteLine($"{DateTime.Now},EV_Vibe{sidx}번,Opened");
                    }
                }
                                                              
                switch (cl_TCP.INSTALL[sidx][0])
                {
                    case 'H':
                        NS_Cli.Write(fgyro_H_install,0,fgyro_H_install.Length);
                        break;
                    case 'V':
                        NS_Cli.Write(fgyro_V_install, 0, fgyro_V_install.Length);
                        break;
                    case 'C':
                        NS_Cli.Write(fgyro_Calibration, 0, fgyro_Calibration.Length);
                        break;
                    default:
                        break;
                }
                
                fVibe_con[sidx] = true;
            }
            else
            {
                sidx = Dict_grainflow[sIP];
                if (cl_TCP.MODE)
                {
                    string sPath = $"{cl_TCP.ROOTPATH}Log\\FiberSensor\\{DateTime.Now.ToString("yyyy-MM-dd")}";
                    sPath += $"\\Event_{DateTime.Now.ToString("MM-dd")}_{sidx}.csv";

                    using (StreamWriter use_SR = new StreamWriter(sPath, true, Encoding.Default))
                    {
                        use_SR.WriteLine($"{DateTime.Now},EV_Flow{sidx}번,Opened");
                    }
                }

                sData_dist = false;
                fFlow_con[sidx] = true;
            }
            
            byte[] sbuffer = new byte[4096];
            int sRead;            
            string sRec_str="";
            while ((sRead = await NS_Cli.ReadAsync(sbuffer, 0, sbuffer.Length))>0 && sidx > -1)
            {

                byte[] sReal = new byte[sRead];
                Array.Copy(sbuffer, sReal, sRead);

                sRec_str = BitConverter.ToString(sReal);
                sRec_str = sRec_str.Replace("-", "");


                if (sData_dist) // 진동 센서
                {
                    fRec_Vibe[sidx] += sRec_str;
                    string sTemp = BitConverter.ToString(sReal);
                    if (fRec_Vibe[sidx].Length > 65)
                    {
                        fVibe_check[sidx] = 0;
                        int[] sAxis = new int[]
                        {
                        fRec_Vibe[sidx].IndexOf(fSTX_list[0]),
                        fRec_Vibe[sidx].IndexOf(fSTX_list[1]),
                        fRec_Vibe[sidx].IndexOf(fSTX_list[2])
                        };
                        
                        while (pfSTX_detection(sAxis) && fRec_Vibe[sidx].Length > 65)
                        {
                            byte[] sData = new byte[6];
                            sData[0] = Convert.ToByte(fRec_Vibe[sidx].Substring(sAxis[0] + 4, 2), 16);
                            sData[1] = Convert.ToByte(fRec_Vibe[sidx].Substring(sAxis[0] + 6, 2), 16);
                            sData[2] = Convert.ToByte(fRec_Vibe[sidx].Substring(sAxis[0] + 8, 2), 16);
                            sData[3] = Convert.ToByte(fRec_Vibe[sidx].Substring(sAxis[0] + 10, 2), 16);
                            sData[4] = Convert.ToByte(fRec_Vibe[sidx].Substring(sAxis[0] + 12, 2), 16);
                            sData[5] = Convert.ToByte(fRec_Vibe[sidx].Substring(sAxis[0] + 14, 2), 16);
                            
                            //    sName = "ACCELERATION";
                            double sX = (short)((sData[1] << 8) | sData[0]);
                            sX = sX / cl_SD[sidx].SQR15 * 16 * 9.8;
                            cl_SD[sidx].ACC_outputX = sX;
                            double sY = (short)((sData[3] << 8) | sData[2]);
                            sY = sY / cl_SD[sidx].SQR15 * 16 * 9.8;
                            cl_SD[sidx].ACC_outputY = sY;
                            double sZ = (short)((sData[5] << 8) | sData[4]);
                            sZ = sZ / cl_SD[sidx].SQR15 * 16 * 9.8;
                            cl_SD[sidx].ACC_outputZ = sZ;

                            sData[0] = Convert.ToByte(fRec_Vibe[sidx].Substring(sAxis[1] + 4, 2), 16);
                            sData[1] = Convert.ToByte(fRec_Vibe[sidx].Substring(sAxis[1] + 6, 2), 16);
                            sData[2] = Convert.ToByte(fRec_Vibe[sidx].Substring(sAxis[1] + 8, 2), 16);
                            sData[3] = Convert.ToByte(fRec_Vibe[sidx].Substring(sAxis[1] + 10, 2), 16);
                            sData[4] = Convert.ToByte(fRec_Vibe[sidx].Substring(sAxis[1] + 12, 2), 16);
                            sData[5] = Convert.ToByte(fRec_Vibe[sidx].Substring(sAxis[1] + 14, 2), 16);

                            //sName = "ANGULAR VELOCITY";
                            sX = (short)((sData[1] << 8) | sData[0]);
                            sX = sX / cl_SD[sidx].SQR15 * cl_SD[sidx].DEF_SPEED;
                            cl_SD[sidx].ANG_voutputX = sX;
                            sY = (short)((sData[3] << 8) | sData[2]);
                            sY = sY / cl_SD[sidx].SQR15 * cl_SD[sidx].DEF_SPEED;
                            cl_SD[sidx].ANG_voutputY = sY;
                            sZ = (short)((sData[5] << 8) | sData[4]);
                            sZ = sZ / cl_SD[sidx].SQR15 * cl_SD[sidx].DEF_SPEED;
                            cl_SD[sidx].ANG_voutputZ = Math.Round(sZ, 2);

                            sData[0] = Convert.ToByte(fRec_Vibe[sidx].Substring(sAxis[2] + 4, 2), 16);
                            sData[1] = Convert.ToByte(fRec_Vibe[sidx].Substring(sAxis[2] + 6, 2), 16);
                            sData[2] = Convert.ToByte(fRec_Vibe[sidx].Substring(sAxis[2] + 8, 2), 16);
                            sData[3] = Convert.ToByte(fRec_Vibe[sidx].Substring(sAxis[2] + 10, 2), 16);
                            sData[4] = Convert.ToByte(fRec_Vibe[sidx].Substring(sAxis[2] + 12, 2), 16);
                            sData[5] = Convert.ToByte(fRec_Vibe[sidx].Substring(sAxis[2] + 14, 2), 16);

                            //sName = "ANGLE";
                            sX = (short)((sData[1] << 8) | sData[0]);
                            sX = sX / cl_SD[sidx].SQR15 * cl_SD[sidx].ANGLE_VAL;
                            cl_SD[sidx].ANG_outputX = sX;
                            sY = (short)((sData[3] << 8) | sData[2]);
                            sY = sY / cl_SD[sidx].SQR15 * cl_SD[sidx].ANGLE_VAL;
                            cl_SD[sidx].ANG_outputY = sY;
                            sZ = (short)((sData[5] << 8) | sData[4]);
                            sZ = sZ / cl_SD[sidx].SQR15 * cl_SD[sidx].ANGLE_VAL;
                            cl_SD[sidx].ANG_outputZ = sZ;

                            fRec_Vibe[sidx] = fRec_Vibe[sidx].Substring(sAxis[0] + 66);


                            //55 51 81 FF 4B 00 FC 07 17 0B 96 55 52 00 00 03 00 00 00 17 0B CC 55 53 56 01 8E 02 A2 FC 17 0B 4F
                            //Length 66                        
                            sAxis = new int[]
                            {
                                fRec_Vibe[sidx].IndexOf(fSTX_list[0]),
                                fRec_Vibe[sidx].IndexOf(fSTX_list[1]),
                                fRec_Vibe[sidx].IndexOf(fSTX_list[2])
                            };
                            
                        }

                        chrt_EV[sidx].Series[0].Points.AddXY(DateTime.Now.ToString("HH:mm:ss"), cl_SD[sidx].ANG_voutputX);
                        if (chrt_EV[sidx].Series[0].Points.Count > 50) chrt_EV[sidx].Series[0].Points.RemoveAt(0);

                        sRec_str = "";
                        string sPath = $"{cl_TCP.ROOTPATH}Log\\GyroSensor\\{DateTime.Now.ToString("yyyy-MM-dd")}";
                        
                        if (!Directory.Exists(sPath))
                        {
                            Directory.CreateDirectory(sPath);
                        }
                        sPath += $"\\Data_{DateTime.Now.ToString("MM-dd")}_{sidx}.csv";

                        if (fLogging && cl_TCP.MODE)
                        {
                            using (StreamWriter use_SR = new StreamWriter(sPath, true, Encoding.Default))
                            {
                                
                                use_SR.WriteLine($"{DateTime.Now},Rec,{cl_SD[sidx].ACC_outputX},{cl_SD[sidx].ANG_outputX},{cl_SD[sidx].ANG_voutputX}," +
                                    $"{cl_SD[sidx].ACC_outputY},{cl_SD[sidx].ANG_outputY},{cl_SD[sidx].ANG_voutputY}," +
                                    $"{cl_SD[sidx].ACC_outputZ},{cl_SD[sidx].ANG_outputZ},{cl_SD[sidx].ANG_voutputZ}");
                            }
                        }
                    }
                    
                    



                }
                else // 곡물 흐름 센서
                {
                    
                    fRec_Flow[sidx] += sRec_str;
                    if (fRec_Flow[sidx].Contains("010402")&& fRec_Flow[sidx].Length > 13)
                    {
                        fRe_Send[sidx] = 0;
                        fRec_con[sidx] = true;
                        string sData = fRec_Flow[sidx].Substring(6, 4);                        
                        if (sData == "0080")
                        {
                            
                            fFlow_check[sidx] = 0;
                            if (!fImg_switch[sidx])
                            {
                                pb_Flow[sidx].Image = Properties.Resources.Active;
                                fImg_switch[sidx] = true;
                            }
                        }
                        else
                        {
                            
                            if (fImg_switch[sidx])
                            {
                                if (fFlow_check[sidx] > cl_TCP.FLOW_TIME)
                                {
                                    pb_Flow[sidx].Image = Properties.Resources.stable; 
                                    fFlow_check[sidx] = cl_TCP.FLOW_TIME + 1;
                                    fImg_switch[sidx] = false;
                                }
                                else
                                {
                                    fFlow_check[sidx]++;
                                }
                            }
                        }
                        string sPath = $"{cl_TCP.ROOTPATH}Log\\FiberSensor\\{DateTime.Now.ToString("yyyy-MM-dd")}";
                        if (!Directory.Exists(sPath))
                        {
                            Directory.CreateDirectory(sPath);
                        }
                        sPath += $"\\Data_{DateTime.Now.ToString("MM-dd")}_{sidx}.csv";
                        if (fLogging && cl_TCP.MODE)
                        {
                            using (StreamWriter use_SR = new StreamWriter(sPath, true, Encoding.Default))
                            {
                                use_SR.WriteLine($"{DateTime.Now},Rec,{sRec_str}");
                            }
                        }

                        fRec_Flow[sidx] = string.Empty;



                    }
                }
                

                //Thread.Sleep(10);
            }

            if (sData_dist)
            {
              //  richTextBox1.AppendText($"Over{sidx}\n");
                chrt_EV[sidx].Series[0].Points.Clear();
                if (cl_TCP.MODE)
                {
                    string sPath = $"{cl_TCP.ROOTPATH}Log\\GyroSensor\\{DateTime.Now.ToString("yyyy-MM-dd")}\\Event_{DateTime.Now.ToString("MM-dd")}_{sidx}.csv";
                    using (StreamWriter use_SR = new StreamWriter(sPath, true, Encoding.Default))
                    {
                        use_SR.WriteLine($"{DateTime.Now},EV_Vibe{sidx}번,Closed");
                    }
                }

                fVibe_con[sidx] = false;
                brCli.Close();
            }
            else
            {
                pb_Flow[sidx].Image = null;
                if (cl_TCP.MODE)
                {
                    string sPath = $"{cl_TCP.ROOTPATH}Log\\FiberSensor\\{DateTime.Now.ToString("yyyy-MM-dd")}\\Event_{DateTime.Now.ToString("MM-dd")}_{sidx}.csv";
                    using (StreamWriter use_SR = new StreamWriter(sPath, true, Encoding.Default))
                    {
                        use_SR.WriteLine($"{DateTime.Now},EV_Flow{sidx}번,Closed");
                    }
                }

                fFlow_con[sidx] = false;
                brCli.Close();
            }

        }

        public string gfbytetobin(byte[] brData)
        {
            string sHex = BitConverter.ToString(brData);
            sHex = sHex.Replace("-", "");
            string sbin = Convert.ToString(Convert.ToInt32(sHex, 16), 2);
            sbin = sbin.PadLeft(sHex.Length * 4, '0');
            return sHex;
        }

        #region vibration sensor
        /*private void psFlowSensor(object sender, SerialDataReceivedEventArgs e)
        {
            byte[] sBuffer = new byte[1024];
            int sLen = sp_Flow1.Read(sBuffer, 0, sBuffer.Length);          
            for (int sLoop = 0; sLoop < sLen; sLoop++)
            {
                fReal.Add(sBuffer[sLoop]);
            }
            string sData = Encoding.ASCII.GetString(fReal.ToArray());
            fRec_rest = sData;
            if (fReal.Contains(0x0A) && fReal.Contains(0x0D) && sData.Contains("Vi312"))
            {
                

                string[] sSplited = sData.Split(',');
                ///0 : Product name
                ///1 : Sequencce
                ///2 : Vibrate val
                ///3 : Min val
                ///4 : Max val
                int sVal_vib = int.Parse(sSplited[2]);
                fDist = sVal_vib;
                if (sVal_vib > 5)
                {
                    fCnt = 0;
                }
                fReal.Clear();
            }

            
        }*/
        #endregion


        //private void psReceive_Serial(object sender, SerialDataReceivedEventArgs e)
        //{
        //    byte[] sRec = new byte[1024];
        //    int sLen = fClient.Receive(sRec, 0, sRec.Length, SocketFlags.None);

        //    int sLen = sp_Flow1.Read(sRec, 0, sRec.Length);

        //    byte[] sTrans = new byte[sLen];
        //    for (int sLoop = 0; sLoop < sLen; sLoop++)
        //    {
        //        sTrans[sLoop] = sRec[sLoop];
        //    }

        //    fRec_str += BitConverter.ToString(sTrans);
        //    fRec_str = fRec_str.Replace("-", "");

        //    if (fRec_str.Length > 132)
        //    {
        //        int[] sAxis = new int[]
        //        {
        //                fRec_str.IndexOf(fSTX_list[0]),
        //                fRec_str.IndexOf(fSTX_list[1]),
        //                fRec_str.IndexOf(fSTX_list[2])
        //        };



        //        while (pfSTX_detection(sAxis) && fRec_str.Length > 132)
        //        {
        //            byte[] sData = new byte[6];
        //            sData[0] = Convert.ToByte(fRec_str.Substring(sAxis[0] + 4, 2), 16);
        //            sData[1] = Convert.ToByte(fRec_str.Substring(sAxis[0] + 6, 2), 16);
        //            sData[2] = Convert.ToByte(fRec_str.Substring(sAxis[0] + 8, 2), 16);
        //            sData[3] = Convert.ToByte(fRec_str.Substring(sAxis[0] + 10, 2), 16);
        //            sData[4] = Convert.ToByte(fRec_str.Substring(sAxis[0] + 12, 2), 16);
        //            sData[5] = Convert.ToByte(fRec_str.Substring(sAxis[0] + 14, 2), 16);

        //            sName = "ACCELERATION";
        //            double sX = (short)((sData[1] << 8) | sData[0]);
        //            sX = sX / SQR15 * 16 * 9.8;
        //            cl_SD.ACC_outputX = sX;
        //            double sY = (short)((sData[3] << 8) | sData[2]);
        //            sY = sY / SQR15 * 16 * 9.8;
        //            cl_SD.ACC_outputY = sY;
        //            double sZ = (short)((sData[5] << 8) | sData[4]);
        //            sZ = sZ / SQR15 * 16 * 9.8;
        //            cl_SD.ACC_outputZ = sZ;

        //            sData[0] = Convert.ToByte(fRec_str.Substring(sAxis[1] + 4, 2), 16);
        //            sData[1] = Convert.ToByte(fRec_str.Substring(sAxis[1] + 6, 2), 16);
        //            sData[2] = Convert.ToByte(fRec_str.Substring(sAxis[1] + 8, 2), 16);
        //            sData[3] = Convert.ToByte(fRec_str.Substring(sAxis[1] + 10, 2), 16);
        //            sData[4] = Convert.ToByte(fRec_str.Substring(sAxis[1] + 12, 2), 16);
        //            sData[5] = Convert.ToByte(fRec_str.Substring(sAxis[1] + 14, 2), 16);

        //            sName = "ANGULAR VELOCITY";
        //            sX = (short)((sData[1] << 8) | sData[0]);
        //            sX = sX / SQR15 * DEF_SPEED;
        //            cl_SD.ANG_voutputX = sX;
        //            if (sX < cl_SD.ANG_vLowX) cl_SD.ANG_vLowX = Math.Round(sX, 2);
        //            else if (sX > cl_SD.ANG_vHighX) cl_SD.ANG_vHighX = Math.Round(sX, 2);
        //            sY = (short)((sData[3] << 8) | sData[2]);
        //            sY = sY / SQR15 * DEF_SPEED;
        //            cl_SD.ANG_voutputY = sY;
        //            if (sY < cl_SD.ANG_vLowY) cl_SD.ANG_vLowY = Math.Round(sY, 2);
        //            else if (sY > cl_SD.ANG_vHighY) cl_SD.ANG_vHighY = Math.Round(sY, 2);
        //            sZ = (short)((sData[5] << 8) | sData[4]);
        //            sZ = sZ / SQR15 * DEF_SPEED;
        //            cl_SD.ANG_voutputZ = Math.Round(sZ, 2);
        //            if (sZ < cl_SD.ANG_vLowZ) cl_SD.ANG_vLowZ = Math.Round(sZ, 2);
        //            else if (sZ > cl_SD.ANG_vHighZ) cl_SD.ANG_vHighZ = Math.Round(sZ, 2);

        //            sData[0] = Convert.ToByte(fRec_str.Substring(sAxis[2] + 4, 2), 16);
        //            sData[1] = Convert.ToByte(fRec_str.Substring(sAxis[2] + 6, 2), 16);
        //            sData[2] = Convert.ToByte(fRec_str.Substring(sAxis[2] + 8, 2), 16);
        //            sData[3] = Convert.ToByte(fRec_str.Substring(sAxis[2] + 10, 2), 16);
        //            sData[4] = Convert.ToByte(fRec_str.Substring(sAxis[2] + 12, 2), 16);
        //            sData[5] = Convert.ToByte(fRec_str.Substring(sAxis[2] + 14, 2), 16);

        //            sName = "ANGLE";
        //            sX = (short)((sData[1] << 8) | sData[0]);
        //            sX = sX / SQR15 * ANGLE_VAL;
        //            cl_SD.ANG_outputX = sX;
        //            sY = (short)((sData[3] << 8) | sData[2]);
        //            sY = sY / SQR15 * ANGLE_VAL;
        //            cl_SD.ANG_outputY = sY;
        //            sZ = (short)((sData[5] << 8) | sData[4]);
        //            sZ = sZ / SQR15 * ANGLE_VAL;
        //            cl_SD.ANG_outputZ = sZ;

        //            fRec_str = fRec_str.Substring(sAxis[0] + 66);


        //            55 51 81 FF 4B 00 FC 07 17 0B 96 55 52 00 00 03 00 00 00 17 0B CC 55 53 56 01 8E 02 A2 FC 17 0B 4F
        //            Length 66
        //            sAxis = new int[]
        //            {
        //                    fRec_str.IndexOf(fSTX_list[0]),
        //                    fRec_str.IndexOf(fSTX_list[1]),
        //                    fRec_str.IndexOf(fSTX_list[2])
        //            };
        //        }

        //    }
        //}

        private bool pfSTX_detection(int[] brAxis)
        {
            bool sResult = true;

            for (int sLoop = 0; sLoop < 3; sLoop++)
            {
                if (brAxis[sLoop] < 0)
                {
                    sResult = false;
                }
            }

            return sResult;
        }



        private void tim_UI_Tick(object sender, EventArgs e)
        {
            lbl_Date.Text = DateTime.Now.ToString("yyyy.MM.dd");
            lbl_Day.Text = DateTime.Now.ToString("ddd") + "요일";
            lbl_Time.Text = DateTime.Now.ToString("hh:mm");

            if (cl_TCP.MODE)
            {



                // DateTime sToday = DateTime.Today;

                if (DateTime.Now.Hour < 5 && DateTime.Now.Hour > 20)
                {
                    fLogging = false;
                    if (tim_Send.Enabled && cl_TCP.MODE)
                    {
                        tim_Send.Stop();
                    }
                }
                else
                {
                    fLogging = true;
                    if (!tim_Send.Enabled && cl_TCP.MODE)
                    {
                        tim_Send.Start();
                    }

                    fRec_con = Enumerable.Repeat(true, 16).ToArray();
                }


                for (int sLoop = 0; sLoop < 2; sLoop++)
                {
                    DateTime sTmw = DateTime.Today.AddDays(sLoop);
                    string sPath = $"{cl_TCP.ROOTPATH}Log\\FiberSensor\\{sTmw.ToString("yyyy-MM-dd")}";
                    if (!Directory.Exists(sPath))
                    {
                        Directory.CreateDirectory(sPath);
                    }
                    for (int sLoop2 = 0; sLoop2 < 13; sLoop2++)
                    {
                        if (!System.IO.File.Exists(sPath + $"\\Event_{sTmw.ToString("MM-dd")}_{sLoop2}.csv"))
                        {
                            using (StreamWriter use_SR = new StreamWriter(sPath + $"\\Event_{sTmw.ToString("MM-dd")}_{sLoop2}.csv", true, Encoding.Default))
                            {
                                use_SR.WriteLine($"Date time,EVENT");
                            }
                        }
                        if (!System.IO.File.Exists(sPath + $"\\Data_{sTmw.ToString("MM-dd")}_{sLoop2}.csv"))
                        {
                            using (StreamWriter use_SR = new StreamWriter(sPath + $"\\Data_{sTmw.ToString("MM-dd")}_{sLoop2}.csv", true, Encoding.Default))
                            {
                                use_SR.WriteLine($"Date time,Status,Packet");
                            }
                        }
                    }
                    sPath = $"{cl_TCP.ROOTPATH}Log\\GyroSensor\\{sTmw.ToString("yyyy-MM-dd")}";
                    if (!Directory.Exists(sPath))
                    {
                        Directory.CreateDirectory(sPath);
                    }

                    for (int sLoop2 = 0; sLoop2 < 16; sLoop2++)
                    {
                        if (!System.IO.File.Exists(sPath + $"\\Event_{sTmw.ToString("MM-dd")}_{sLoop2}.csv"))
                        {
                            using (StreamWriter use_SR = new StreamWriter(sPath + $"\\Event_{sTmw.ToString("MM-dd")}_{sLoop2}.csv", true, Encoding.Default))
                            {
                                use_SR.WriteLine($"Date time,EVENT");
                            }
                        }
                        if (!System.IO.File.Exists(sPath + $"\\Data_{sTmw.ToString("MM-dd")}_{sLoop2}.csv"))
                        {
                            using (StreamWriter use_SR = new StreamWriter(sPath + $"\\Data_{sTmw.ToString("MM-dd")}_{sLoop2}.csv", true, Encoding.Default))
                            {
                                //                        use_SR.WriteLine($"{DateTime.Now},Rec,{cl_SD[sidx].ACC_outputX},{cl_SD[sidx].ANG_outputX},{cl_SD[sidx].ANG_voutputX}," +
                                //$"{cl_SD[sidx].ACC_outputY},{cl_SD[sidx].ANG_outputY},{cl_SD[sidx].ANG_voutputY}," +
                                //$"{cl_SD[sidx].ACC_outputZ},{cl_SD[sidx].ANG_outputZ},{cl_SD[sidx].ANG_voutputZ}");
                                use_SR.WriteLine($"Date time,Status,ACCX,ANGX,ANGVX,ACCY,ANGY,ANGVY,ACCZ,ANGZ,ANGVZ");
                            }
                        }
                    }


                }

                for (int sLoop = 0; sLoop < 13; sLoop++)
                {
                    if (!fRec_con[sLoop])
                    {
                        fRec_con[sLoop] = fRe_Send[sLoop]++ >= 3;
                        if (fLogging && fRe_Send[sLoop] >= 3 && cl_TCP.MODE)
                        {
                            string sPath = $"{cl_TCP.ROOTPATH}Log\\FiberSensor\\{DateTime.Now.ToString("yyyy-MM-dd")}";
                            sPath += $"\\Event_{DateTime.Now.ToString("MM-dd")}_{sLoop}.csv";
                            using (StreamWriter use_SR = new StreamWriter(sPath, true, Encoding.Default))
                            {

                                use_SR.WriteLine($"{DateTime.Now},Send,Error");
                            }
                        }
                    }

                }

                foreach (int fetup in Dict_vibe.Values)
                {
                    if (fVibe_check[fetup]++ > cl_TCP.Recon_time2)
                    {
                        tcp_Vibe[fetup].Close();
                        fVibe_con[fetup] = false;
                        fVibe_check[fetup] = 0;
                    };
                }



            }
        }
        private void tim_Load_Tick(object sender, EventArgs e)
        {
            tim_Load.Stop();
            //frmLOAD.Hide();
          //  tim_transparency.Start();

        }

        private void psSocket_Reconnection(object sender, EventArgs e)
        {
            //1초에 한번씩 번갈아가며 connection 상태 확인
            string sSec = DateTime.Now.ToString("ss");
            int sOdd_even = int.Parse(sSec);
            if (sOdd_even % 2 == 0)// gyro sensor connection
            {

                foreach (int feidx in Dict_vibe.Values)
                {                    
                    if (!fVibe_con[feidx])
                    {
                        try
                        {
                            tcp_Vibe[feidx].Close();
                            
                        }
                        catch (Exception)
                        {
                            if (cl_TCP.MODE)
                            {
                                string sPath1 = $"{cl_TCP.ROOTPATH}Log\\GyroSensor\\{DateTime.Now.ToString("yyyy-MM-dd")}";
                                sPath1 += $"\\Event_{DateTime.Now.ToString("MM-dd")}_{feidx}.csv";
                                using (StreamWriter use_SR = new StreamWriter(sPath1, true, Encoding.Default))
                                {

                                    use_SR.WriteLine($"{DateTime.Now},TRY,Reconnection");
                                }
                            }
                        }
                        psCli_connect(cl_TCP.IP[feidx], cl_TCP.PORT[feidx], feidx);
                        string sPath = $"{cl_TCP.ROOTPATH}Log\\GyroSensor\\{DateTime.Now.ToString("yyyy-MM-dd")}";
                        sPath += $"\\Event_{DateTime.Now.ToString("MM-dd")}_{feidx}.csv";
                        if (cl_TCP.MODE)
                        {
                            using (StreamWriter use_SR = new StreamWriter(sPath, true, Encoding.Default))
                            {

                                use_SR.WriteLine($"{DateTime.Now},TRY,Reconnection");
                            }
                        }
                        
                    }
                    else
                    {
                        ///카운트 증가로 일정 시간 이후 재 연결
                        
                    }
                    
                }
            }
            else
            {

                foreach (int feidx in Dict_grainflow.Values)
                {                    
                    if (!fFlow_con[feidx])
                    {                        
                        try
                        {
                            tcp_Flow[feidx].Close();

                        }
                        catch (Exception)
                        {

                        }                        
                        psCli_connect(cl_TCP.IP2[feidx], cl_TCP.PORT2[feidx], feidx);
                        if (cl_TCP.MODE)
                        {
                            string sPath = $"{cl_TCP.ROOTPATH}Log\\FiberSensor\\{DateTime.Now.ToString("yyyy-MM-dd")}";
                            sPath += $"\\Event_{DateTime.Now.ToString("MM-dd")}_{feidx}.csv";
                            using (StreamWriter use_SR = new StreamWriter(sPath, true, Encoding.Default))
                            {

                                use_SR.WriteLine($"{DateTime.Now},TRY,Reconnection");
                            }
                        }

                    }
                }
            }
        }

        private void lbl_Time_DoubleClick(object sender, EventArgs e)
        {
            richTextBox1.Visible = !richTextBox1.Visible;
            richTextBox2.Visible = !richTextBox2.Visible;
            richTextBox3.Visible = !richTextBox3.Visible;
        }

        private void frm_Sensor_Shown(object sender, EventArgs e)
        {
            tim_UI.Start();
            
        }

        private void pb_Block9_Click(object sender, EventArgs e)
        {
            string sName = (sender as PictureBox).Name;
            int sidx = int.Parse(sName.Substring(8));
            chrt_EV[sidx-1].Series[0].Points.Clear();
        }

        private void lbl_Date_DoubleClick(object sender, EventArgs e)
        {
            foreach (Chart fechart in chrt_EV)
            {
                fechart.Series[0].Points.Clear();
            }
        }

        private void frm_Sensor_FormClosing(object sender, FormClosingEventArgs e)
        {
            for (int sLoop = 0; sLoop < Dict_grainflow.Values.Count; sLoop++)
            {
                tcp_Vibe[sLoop].Close();
            }            
            for (int sLoop = 0; sLoop < Dict_vibe.Values.Count; sLoop++)
            {
                tcp_Flow[sLoop].Close();
            }
        }

        private void pb_flow1_Click(object sender, EventArgs e)
        {
            PictureBox spb = sender as PictureBox;
            spb.Image = null;

        }

        private void label1_DoubleClick(object sender, EventArgs e)
        {
            string sName = (sender as Label).Name;
            int sidx = int.Parse(sName.Substring(4));
            chrt_EV[sidx-1].Series[0].Points.AddXY(DateTime.Now.ToString("HH:mm:ss"), 1);
        }

        private void frm_Sensor_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyData == ( Keys.Control | Keys.Q ))
            {


                    tim_Send.Close();
                    tim_Send.Dispose();

                    this.Close();
                
            }
        }

        private void lbl_Day_DoubleClick(object sender, EventArgs e)
        {
            if (cl_TCP.MODE)
            {
                tim_Send.Close();
                tim_Send.Dispose();
            }
            

            this.Close();
        }
    }
}
