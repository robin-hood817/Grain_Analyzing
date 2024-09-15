using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JSW_Honam_seed
{
    public class Sensor_data
    {
        #region Properties
        public double SQR15 { get { return 32768; } }
        public double DEF_SPEED { get { return 2000; } }
        public double GRAVITY_ACC { get{ return 16 * 9.8; } }
        public double ANGLE_VAL { get { return 180; } }        



        #endregion
        /// <summary>
        /// 진동 센서 변수
        /// </summary>
    //가속도 값
        public double ACC_outputX;
        public double ACC_outputY;
        public double ACC_outputZ;
        //각속도 값
        public double ANG_voutputX;
        public double ANG_vLowX = -1;
        public double ANG_vHighX = 1;
        public double ANG_voutputY;
        public double ANG_vLowY = -1;
        public double ANG_vHighY = 1;
        public double ANG_voutputZ;
        public double ANG_vLowZ = -1;
        public double ANG_vHighZ = 1;
        //각도 값
        public double ANG_outputX;
        public double ANG_outputY;
        public double ANG_outputZ;




        /// <summary>
        /// 곡물 흐름 센서 변수
        /// </summary>
        //기산 시스템 KM6052
        //01 04 00 80 00 01 30 22        
        public string gfbytetobin(byte[] brData)
        {
            string sHex = BitConverter.ToString(brData);            
            sHex = sHex.Replace("-", "");
            string sbin = Convert.ToString(Convert.ToInt32(sHex, 16), 2);
            sbin = sbin.PadLeft(sHex.Length * 4, '0');
            return sHex;
        }

        public Sensor_data()
        {
            
        }
    }
}
