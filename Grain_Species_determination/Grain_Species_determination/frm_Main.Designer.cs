namespace Grain_Species_determination
{
    partial class frm_Main
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.tim_Time = new System.Windows.Forms.Timer(this.components);
            this.tim_Video = new System.Windows.Forms.Timer(this.components);
            this.pb_Cam1 = new System.Windows.Forms.PictureBox();
            this.pb_Analysis = new System.Windows.Forms.PictureBox();
            this.pb_Kind_Grain = new System.Windows.Forms.PictureBox();
            this.lbl_Result = new System.Windows.Forms.Label();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tim_Take = new System.Windows.Forms.Timer(this.components);
            this.lbl_Time = new System.Windows.Forms.Label();
            this.lbl_Date = new System.Windows.Forms.Label();
            this.lbl_Day = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Cam1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Analysis)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Kind_Grain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // tim_Time
            // 
            this.tim_Time.Interval = 1000;
            this.tim_Time.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // tim_Video
            // 
            this.tim_Video.Interval = 300;
            this.tim_Video.Tick += new System.EventHandler(this.tim_Video_Tick);
            // 
            // pb_Cam1
            // 
            this.pb_Cam1.BackColor = System.Drawing.Color.Black;
            this.pb_Cam1.Location = new System.Drawing.Point(135, 337);
            this.pb_Cam1.Name = "pb_Cam1";
            this.pb_Cam1.Size = new System.Drawing.Size(847, 478);
            this.pb_Cam1.TabIndex = 0;
            this.pb_Cam1.TabStop = false;
            // 
            // pb_Analysis
            // 
            this.pb_Analysis.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.pb_Analysis.Image = global::Grain_Species_determination.Properties.Resources.n_Analysis;
            this.pb_Analysis.Location = new System.Drawing.Point(871, 857);
            this.pb_Analysis.Name = "pb_Analysis";
            this.pb_Analysis.Size = new System.Drawing.Size(160, 60);
            this.pb_Analysis.TabIndex = 1;
            this.pb_Analysis.TabStop = false;
            this.pb_Analysis.MouseDown += new System.Windows.Forms.MouseEventHandler(this.psAnalyze_press);
            this.pb_Analysis.MouseUp += new System.Windows.Forms.MouseEventHandler(this.psAnalyze_unpress);
            // 
            // pb_Kind_Grain
            // 
            this.pb_Kind_Grain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.pb_Kind_Grain.Image = global::Grain_Species_determination.Properties.Resources.n_Kind_Learning;
            this.pb_Kind_Grain.Location = new System.Drawing.Point(80, 857);
            this.pb_Kind_Grain.Name = "pb_Kind_Grain";
            this.pb_Kind_Grain.Size = new System.Drawing.Size(200, 60);
            this.pb_Kind_Grain.TabIndex = 2;
            this.pb_Kind_Grain.TabStop = false;
            this.pb_Kind_Grain.MouseDown += new System.Windows.Forms.MouseEventHandler(this.psKind_Learn_press);
            this.pb_Kind_Grain.MouseUp += new System.Windows.Forms.MouseEventHandler(this.psKind_Learn_unpress);
            // 
            // lbl_Result
            // 
            this.lbl_Result.AutoSize = true;
            this.lbl_Result.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.lbl_Result.Font = new System.Drawing.Font("나눔스퀘어", 80F);
            this.lbl_Result.ForeColor = System.Drawing.Color.White;
            this.lbl_Result.Location = new System.Drawing.Point(1269, 342);
            this.lbl_Result.Name = "lbl_Result";
            this.lbl_Result.Size = new System.Drawing.Size(0, 118);
            this.lbl_Result.TabIndex = 3;
            this.lbl_Result.Click += new System.EventHandler(this.label1_Click);
            // 
            // chart1
            // 
            this.chart1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            chartArea3.AxisX.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.True;
            chartArea3.AxisX.IsLabelAutoFit = false;
            chartArea3.AxisX.LabelStyle.Font = new System.Drawing.Font("나눔스퀘어", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea3.AxisX.LabelStyle.ForeColor = System.Drawing.Color.White;
            chartArea3.AxisX.MajorGrid.Enabled = false;
            chartArea3.AxisX.MajorGrid.Interval = 0D;
            chartArea3.AxisX.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            chartArea3.AxisX.TitleForeColor = System.Drawing.Color.White;
            chartArea3.AxisY.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.False;
            chartArea3.AxisY.MajorGrid.Enabled = false;
            chartArea3.AxisY.MajorGrid.Interval = 0D;
            chartArea3.AxisY.TitleForeColor = System.Drawing.Color.White;
            chartArea3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            chartArea3.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea3);
            legend3.Enabled = false;
            legend3.Name = "Legend1";
            this.chart1.Legends.Add(legend3);
            this.chart1.Location = new System.Drawing.Point(1124, 617);
            this.chart1.Name = "chart1";
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar;
            series3.Color = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(175)))), ((int)(((byte)(58)))));
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            this.chart1.Series.Add(series3);
            this.chart1.Size = new System.Drawing.Size(692, 300);
            this.chart1.TabIndex = 8;
            this.chart1.Text = "chart1";
            // 
            // tim_Take
            // 
            this.tim_Take.Interval = 500;
            this.tim_Take.Tick += new System.EventHandler(this.tim_Take_Tick);
            // 
            // lbl_Time
            // 
            this.lbl_Time.AutoSize = true;
            this.lbl_Time.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Time.Font = new System.Drawing.Font("나눔스퀘어라운드 Regular", 155F);
            this.lbl_Time.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(237)))), ((int)(((byte)(253)))));
            this.lbl_Time.Location = new System.Drawing.Point(1278, 18);
            this.lbl_Time.Name = "lbl_Time";
            this.lbl_Time.Size = new System.Drawing.Size(654, 229);
            this.lbl_Time.TabIndex = 10;
            this.lbl_Time.Text = "09:28";
            // 
            // lbl_Date
            // 
            this.lbl_Date.AutoSize = true;
            this.lbl_Date.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.lbl_Date.Font = new System.Drawing.Font("나눔스퀘어", 25F);
            this.lbl_Date.ForeColor = System.Drawing.Color.White;
            this.lbl_Date.Location = new System.Drawing.Point(1118, 121);
            this.lbl_Date.Name = "lbl_Date";
            this.lbl_Date.Size = new System.Drawing.Size(193, 38);
            this.lbl_Date.TabIndex = 11;
            this.lbl_Date.Text = "2024.05.21";
            // 
            // lbl_Day
            // 
            this.lbl_Day.AutoSize = true;
            this.lbl_Day.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.lbl_Day.Font = new System.Drawing.Font("나눔스퀘어라운드 Bold", 24.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_Day.ForeColor = System.Drawing.Color.White;
            this.lbl_Day.Location = new System.Drawing.Point(1205, 166);
            this.lbl_Day.Name = "lbl_Day";
            this.lbl_Day.Size = new System.Drawing.Size(105, 36);
            this.lbl_Day.TabIndex = 12;
            this.lbl_Day.Text = "화요일";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = global::Grain_Species_determination.Properties.Resources.Main;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1920, 1080);
            this.pictureBox1.TabIndex = 13;
            this.pictureBox1.TabStop = false;
            // 
            // frm_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.ClientSize = new System.Drawing.Size(1920, 1080);
            this.Controls.Add(this.lbl_Day);
            this.Controls.Add(this.lbl_Date);
            this.Controls.Add(this.lbl_Time);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.lbl_Result);
            this.Controls.Add(this.pb_Kind_Grain);
            this.Controls.Add(this.pb_Analysis);
            this.Controls.Add(this.pb_Cam1);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frm_Main";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.DoubleClick += new System.EventHandler(this.frm_Main_DoubleClick);
            ((System.ComponentModel.ISupportInitialize)(this.pb_Cam1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Analysis)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Kind_Grain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer tim_Time;
        private System.Windows.Forms.Timer tim_Video;
        private System.Windows.Forms.PictureBox pb_Cam1;
        private System.Windows.Forms.PictureBox pb_Analysis;
        private System.Windows.Forms.PictureBox pb_Kind_Grain;
        private System.Windows.Forms.Label lbl_Result;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Timer tim_Take;
        private System.Windows.Forms.Label lbl_Time;
        private System.Windows.Forms.Label lbl_Date;
        private System.Windows.Forms.Label lbl_Day;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

