using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grain_Species_determination
{
    public partial class frm_Analysis : Form
    {
        public string fSql_constr;
        public string fMachine_Code;

        public bool fTaken=false;

        public Label[] lbl_Kind;

        private Bitmap[] _bitmap = null;
        public frm_Analysis()
        {
            InitializeComponent();
            lbl_Kind = new Label[]
            {
                lbl_Kind1,
                lbl_Kind2,
                lbl_Kind3,
                lbl_Kind4,
                lbl_Kind5,
                lbl_Kind6,
                lbl_Kind7,
                lbl_Kind8,
                lbl_Kind9

            };
            
        }
        private void button2_Click(object sender, EventArgs e)
        {
            

            using (SqlConnection use_sqlcon = new SqlConnection(fSql_constr))
            {
                use_sqlcon.Open();
                string sSQL = "Delete TB_Grain_Code where Machine_Code = @Code";

                using (SqlCommand use_SC = new SqlCommand(sSQL, use_sqlcon))
                {
                    use_SC.Parameters.AddWithValue("@Code", fMachine_Code);
                    use_SC.ExecuteNonQuery();
                }
                for (int sLoop = 0; sLoop < lvw_Dirlist.Items.Count; sLoop++)
                {

                    sSQL = "insert into TB_Grain_Code Values (@Code, @Grain_Kind, @Check)";
                    using (SqlCommand use_SC = new SqlCommand(sSQL, use_sqlcon))
                    {
                        use_SC.Parameters.AddWithValue("@Code", fMachine_Code);
                        use_SC.Parameters.AddWithValue("Grain_Kind", lvw_Dirlist.Items[sLoop].ToString());
                        use_SC.Parameters.AddWithValue("@Check", "N");

                        use_SC.ExecuteNonQuery();
                    }
                }
            }
            

            this.Hide();
        }

        private void frm_Analysis_Load(object sender, EventArgs e)
        {
            lvw_Dirlist.Items.Clear();
            
            using (SqlConnection use_sqlcon = new SqlConnection(fSql_constr))
            {
                use_sqlcon.Open();


                  string  sSQL = "Select * from TB_Grain_Code where Machine_Code = @Code";
                using (SqlCommand use_SC = new SqlCommand(sSQL, use_sqlcon))
                {
                    use_SC.Parameters.AddWithValue("@Code", fMachine_Code);                   

                    SqlDataReader sSDR = use_SC.ExecuteReader();
                    int sLoop = 0;
                    while (sSDR.Read())
                    {
                        lbl_Kind[sLoop].Text = sSDR["Grain_Kind"].ToString();
                        sLoop++;
                    }
                }
                
            }

        }
        private void psSelect_Kind(object sender, EventArgs e)
        {
            //품종 라벨 선택 시 색변경 및 해당 품종 FTP 폴더 선택
        }

        public void psDraw_pb(Bitmap[] brbitmap)
        {
            pb_taken.Visible = true;
            _bitmap[0] = brbitmap[0];
            Redraw_Cam1(pb_taken.CreateGraphics());
        }

        private void Redraw_Cam1(Graphics g)//this is the function that is called to draw the image on the screen
        {
            try
            {
                //_mutexImage[0].WaitOne();

                if (_bitmap[0] != null)
                {

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
                
            }
        }

        

        #region Button_press_Event



        private void psLearning_press(object sender, MouseEventArgs e)
        {
            PictureBox sPb = sender as PictureBox;
            sPb.Image = Properties.Resources.p_Learning;
        }

        private void psLearning_unpress(object sender, MouseEventArgs e)
        {
            PictureBox sPb = sender as PictureBox;
            sPb.Image = Properties.Resources.n_Learning;
        }

        private void psExit_press(object sender, MouseEventArgs e)
        {
            PictureBox sPb = sender as PictureBox;
            sPb.Image = Properties.Resources.p_exit;
        }

        private void psExit_unpress(object sender, MouseEventArgs e)
        {
            PictureBox sPb = sender as PictureBox;
            sPb.Image = Properties.Resources.n_exit;

            this.Hide();
        }

        private void pstake_press(object sender, MouseEventArgs e)
        {
            PictureBox sPb = sender as PictureBox;
            sPb.Image = Properties.Resources.p_take;
        }

        private void pstake_unpress(object sender, MouseEventArgs e)
        {
            PictureBox sPb = sender as PictureBox;
            sPb.Image = Properties.Resources.n_take;

            fTaken = true;
        }
 

        private void psRegister_press(object sender, MouseEventArgs e)
        {
            PictureBox sPb = sender as PictureBox;
            sPb.Image = Properties.Resources.p_Register;
        }

        private void psRegister_unpress(object sender, MouseEventArgs e)
        {
            PictureBox sPb = sender as PictureBox;
            sPb.Image = Properties.Resources.n_Register;
            string sGrain_Kind = tb_Register.Text;
            if (sGrain_Kind != "")
            {

                tb_Register.Text = "";
            }

        }
        #endregion


    }
}
