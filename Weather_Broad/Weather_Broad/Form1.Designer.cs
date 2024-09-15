namespace Weather_Broad
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.lbl_location = new System.Windows.Forms.Label();
            this.lbl_Date = new System.Windows.Forms.Label();
            this.btn_prev = new System.Windows.Forms.PictureBox();
            this.btn_next = new System.Windows.Forms.PictureBox();
            this.iL_weather = new System.Windows.Forms.ImageList(this.components);
            this.pb_weather = new System.Windows.Forms.PictureBox();
            this.lbl_Temp = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lbl_Time = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Ch_Temp = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.btn_prev)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_next)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_weather)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Ch_Temp)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_location
            // 
            this.lbl_location.AutoSize = true;
            this.lbl_location.Font = new System.Drawing.Font("여기어때 잘난체", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_location.ForeColor = System.Drawing.Color.Black;
            this.lbl_location.Location = new System.Drawing.Point(28, 29);
            this.lbl_location.Name = "lbl_location";
            this.lbl_location.Size = new System.Drawing.Size(298, 27);
            this.lbl_location.TabIndex = 0;
            this.lbl_location.Text = "경상남도 거창군 마리면";
            this.lbl_location.Click += new System.EventHandler(this.lbl_location_Click);
            // 
            // lbl_Date
            // 
            this.lbl_Date.AutoSize = true;
            this.lbl_Date.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_Date.ForeColor = System.Drawing.Color.Black;
            this.lbl_Date.Location = new System.Drawing.Point(280, 9);
            this.lbl_Date.Name = "lbl_Date";
            this.lbl_Date.Size = new System.Drawing.Size(75, 12);
            this.lbl_Date.TabIndex = 1;
            this.lbl_Date.Text = "2023-04-14";
            // 
            // btn_prev
            // 
            this.btn_prev.BackgroundImage = global::Weather_Broad.Properties.Resources.기상청_btn_reverse;
            this.btn_prev.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_prev.Location = new System.Drawing.Point(932, 9);
            this.btn_prev.Name = "btn_prev";
            this.btn_prev.Size = new System.Drawing.Size(41, 70);
            this.btn_prev.TabIndex = 3;
            this.btn_prev.TabStop = false;
            this.btn_prev.Click += new System.EventHandler(this.btn_next_Click);
            // 
            // btn_next
            // 
            this.btn_next.BackgroundImage = global::Weather_Broad.Properties.Resources.기상청_btn;
            this.btn_next.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_next.Location = new System.Drawing.Point(1004, 9);
            this.btn_next.Name = "btn_next";
            this.btn_next.Size = new System.Drawing.Size(42, 70);
            this.btn_next.TabIndex = 2;
            this.btn_next.TabStop = false;
            this.btn_next.Click += new System.EventHandler(this.btn_next_Click);
            // 
            // iL_weather
            // 
            this.iL_weather.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("iL_weather.ImageStream")));
            this.iL_weather.TransparentColor = System.Drawing.Color.Transparent;
            this.iL_weather.Images.SetKeyName(0, "sunny-removebg-preview.png");
            this.iL_weather.Images.SetKeyName(1, "rainy-removebg-preview.png");
            this.iL_weather.Images.SetKeyName(2, "double_cloud-removebg-preview.png");
            this.iL_weather.Images.SetKeyName(3, "cloud-removebg-preview.png");
            // 
            // pb_weather
            // 
            this.pb_weather.Location = new System.Drawing.Point(33, 82);
            this.pb_weather.Name = "pb_weather";
            this.pb_weather.Size = new System.Drawing.Size(141, 136);
            this.pb_weather.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb_weather.TabIndex = 4;
            this.pb_weather.TabStop = false;
            // 
            // lbl_Temp
            // 
            this.lbl_Temp.AutoSize = true;
            this.lbl_Temp.Font = new System.Drawing.Font("여기어때 잘난체", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_Temp.Location = new System.Drawing.Point(790, 149);
            this.lbl_Temp.Name = "lbl_Temp";
            this.lbl_Temp.Size = new System.Drawing.Size(77, 21);
            this.lbl_Temp.TabIndex = 5;
            this.lbl_Temp.Text = "label1";
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lbl_Time
            // 
            this.lbl_Time.AutoSize = true;
            this.lbl_Time.Font = new System.Drawing.Font("여기어때 잘난체", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_Time.Location = new System.Drawing.Point(790, 108);
            this.lbl_Time.Name = "lbl_Time";
            this.lbl_Time.Size = new System.Drawing.Size(77, 21);
            this.lbl_Time.TabIndex = 6;
            this.lbl_Time.Text = "label1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("여기어때 잘난체", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(681, 108);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 21);
            this.label1.TabIndex = 8;
            this.label1.Text = "시간";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("여기어때 잘난체", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.Location = new System.Drawing.Point(681, 149);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 21);
            this.label2.TabIndex = 7;
            this.label2.Text = "온도";
            // 
            // Ch_Temp
            // 
            chartArea1.Name = "ChartArea1";
            this.Ch_Temp.ChartAreas.Add(chartArea1);
            this.Ch_Temp.Dock = System.Windows.Forms.DockStyle.Bottom;
            legend1.Name = "Legend1";
            this.Ch_Temp.Legends.Add(legend1);
            this.Ch_Temp.Location = new System.Drawing.Point(0, 274);
            this.Ch_Temp.Name = "Ch_Temp";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.Ch_Temp.Series.Add(series1);
            this.Ch_Temp.Size = new System.Drawing.Size(1064, 327);
            this.Ch_Temp.TabIndex = 9;
            this.Ch_Temp.Text = "chart1";
            this.Ch_Temp.Click += new System.EventHandler(this.chart1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1064, 601);
            this.Controls.Add(this.Ch_Temp);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbl_Time);
            this.Controls.Add(this.lbl_Temp);
            this.Controls.Add(this.pb_weather);
            this.Controls.Add(this.btn_prev);
            this.Controls.Add(this.btn_next);
            this.Controls.Add(this.lbl_Date);
            this.Controls.Add(this.lbl_location);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.btn_prev)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_next)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_weather)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Ch_Temp)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_location;
        private System.Windows.Forms.Label lbl_Date;
        private System.Windows.Forms.PictureBox btn_next;
        private System.Windows.Forms.PictureBox btn_prev;
        private System.Windows.Forms.ImageList iL_weather;
        private System.Windows.Forms.PictureBox pb_weather;
        private System.Windows.Forms.Label lbl_Temp;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lbl_Time;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataVisualization.Charting.Chart Ch_Temp;
    }
}

