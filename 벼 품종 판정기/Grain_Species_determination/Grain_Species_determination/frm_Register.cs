using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grain_Species_determination
{
    public partial class frm_Register : Form
    {
        TCP_Class cl_TCP;        
        TextBox[] tb_Info;

        string fRootpath;
        string fSql_constr;
        string fMachine_Code;
        public frm_Register()
        {
            InitializeComponent();
            fRootpath = Application.StartupPath;
            cl_TCP = new TCP_Class(fRootpath, "Setting.ini");
            tb_Info = new TextBox[] {
                tb_Code,
                tb_companyNo,
                tb_Name,
                tb_Phone,
                tb_Addr
            };

        }

        private void tb_Code_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string sCode = tb_Code.Text;
                if (sCode.IndexOf("IDEAL-") > -1 && sCode.Length > 12)
                {
                    using (SqlConnection use_sql = new SqlConnection(fSql_constr))
                    {
                        use_sql.Open();
                        string sSQL = "select * from TB_Machine_Master where Machine_Code = @Code";
                        using (SqlCommand use_SC = new SqlCommand(sSQL, use_sql))
                        {
                            use_SC.Parameters.AddWithValue("@Code", sCode);

                            SqlDataReader sSDA = use_SC.ExecuteReader();
                            if (sSDA.Read())
                            {
                                this.Height = 500;
                                this.Width = 400;
                                
                                pb_bg.Visible = false;
                                tb_Code.Visible = false;
                                fMachine_Code = sCode;
 
                                
                            }
                            else
                            {
                                MessageBox.Show($"{sCode}는 없는 코드 입니다.");
                            }
                        }
                    }


                    
                }
                else
                {
                    MessageBox.Show($"{sCode}는 없는 코드 입니다.");
                }
            }
        }

        private void frm_Register_Load(object sender, EventArgs e)
        {
            fSql_constr = cl_TCP.gfSetting_DB("Setting.ini");
            pictureBox1.Parent = pb_bg;
        }


        private void btn_Confirm_Click(object sender, EventArgs e)
        {
    
        }

        private void tim_Process_Tick(object sender, EventArgs e)
        {
            
            this.Hide();
        }


        /// <summary>
        /// 버튼을 클릭 할 때
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void psPressed(object sender, MouseEventArgs e)
        {
            PictureBox sPb = sender as PictureBox;
            sPb.Image = Properties.Resources.p_confirm;
        }


        /// <summary>
        /// 버튼에서 클릭을 땠을 경우
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void psUnpressed(object sender, MouseEventArgs e)
        {
            PictureBox sPb = sender as PictureBox;
            sPb.Image = Properties.Resources.n_confirm;
            bool sConfirm = true;
            /// 0 : Address
            /// 1 : Manager
            /// 2 : Phone
            /// 3 : Company
            string[] sInfo = new string[5];
            for (int sLoop = 0; sLoop < tb_Info.Length; sLoop++)
            {
                string sTxt = tb_Info[sLoop].Text;
                if (sTxt.Length <= 2)
                {
                    sConfirm = false;
                }
                else
                {
                    sInfo[sLoop] = sTxt;
                }
                
            }

            if (sConfirm)
            {
                try
                {
                    using (SqlConnection use_sqlcon = new SqlConnection(fSql_constr))
                    {
                        use_sqlcon.Open();
                        bool sHasRow = false;
                        string sSQL = "select * from TB_Machine_Master where Machine_Code = @Code";
                        using (SqlCommand use_SC = new SqlCommand(sSQL, use_sqlcon))
                        {
                            use_SC.Parameters.AddWithValue("@Code", sInfo[0]);
                            using(SqlDataReader sSDR = use_SC.ExecuteReader())
                            {
                                sHasRow = sSDR.Read();
                            }
                            
                            
                        }

                        if (sHasRow)
                        {


                            sSQL = "insert into TB_Machine_Register Values (@Code, @Addr, @Name, @Phone, @Del_Check, @FTP, @Buisness)";
                            using (SqlCommand use_SC = new SqlCommand(sSQL, use_sqlcon))
                            {
                                SqlTransaction sTran_pt = use_sqlcon.BeginTransaction();
                                //트렌젝션 처리
                                try
                                {
                                    use_SC.Transaction = sTran_pt;

                                    fMachine_Code = tb_Code.Text;
                                    use_SC.Parameters.AddWithValue("@Code", fMachine_Code);
                                    int sLen = fMachine_Code.Length;
                                    use_SC.Parameters.AddWithValue("@Addr", sInfo[4]);
                                    sLen = sInfo[0].Length;
                                    use_SC.Parameters.AddWithValue("@Name", sInfo[2]);
                                    sLen = sInfo[1].Length;
                                    use_SC.Parameters.AddWithValue("@Phone", sInfo[3]);
                                    sLen = sInfo[2].Length;
                                    use_SC.Parameters.AddWithValue("@Del_Check", "N");

                                    use_SC.Parameters.AddWithValue("FTP", $"ftp://control-ideal.iptime.org:2126/{fMachine_Code}");
                                    sLen = ($"ftp://control-ideal.iptime.org:2126/{fMachine_Code}").Length;
                                    use_SC.Parameters.AddWithValue("@Buisness", sInfo[1]);
                                    sLen = sInfo[3].Length;

                                    use_SC.ExecuteNonQuery();

                                    sTran_pt.Commit();
                                    string sDate = DateTime.Now.ToString("yyyy-MM-dd");
                                    string sTime = DateTime.Now.ToString("HH:mm:ss");
                                    //psLogging($"{sDate}\\Event.csv", $"{sDate},{sTime},REGISTER,Registered,{fMachine_Code}");
                                    cl_TCP.gsRegist_Code(fMachine_Code, "Setting.ini");
                                    this.Hide();
                                }
                                catch (Exception)
                                {
                                    string sDate = DateTime.Now.ToString("yyyy-MM-dd");
                                    string sTime = DateTime.Now.ToString("HH:mm:ss");
                                    // psLogging($"{sDate}\\Event.csv", $"{sDate},{sTime},REGISTER,DB Error,{sSQL}");
                                    sTran_pt.Rollback();
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("존재하지 않는 제품번호 입니다.");
                        }
                    }
                }
                catch (Exception)
                {

                    
                }
            }
            else // 전부작성 하지 않음
            {
                for (int sLoop = 0; sLoop < tb_Info.Length; sLoop++)
                {
                    string sTxt = tb_Info[sLoop].Text;
                    if (sTxt.Length <= 2)
                    {
                        tb_Info[sLoop].BackColor = Color.Red;
                        tb_Info[sLoop].ForeColor = Color.White;
                    }
                }
            }
        }

        #region Log_function

        private void psLogging(string brPath, string brLog)
        {
            string sPath = $"{fRootpath}\\bin\\Log\\{brPath}";
            using (StreamWriter use_SW = new StreamWriter(sPath, true, Encoding.Default))
            {
                string sDate = DateTime.Now.ToString();
                use_SW.WriteLine(brLog);
            }
        }

        #endregion

        private void tb_Code_TextChanged(object sender, EventArgs e)
        {
            TextBox stb = sender as TextBox;

            stb.BackColor = Color.White;
            stb.ForeColor = Color.Black;
        }
    }
}
