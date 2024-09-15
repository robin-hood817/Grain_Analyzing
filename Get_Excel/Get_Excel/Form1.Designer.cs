namespace Get_Excel
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
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.lbl_Code = new System.Windows.Forms.Label();
            this.lbl_City = new System.Windows.Forms.Label();
            this.lbl_Gu = new System.Windows.Forms.Label();
            this.lbl_dong = new System.Windows.Forms.Label();
            this.lbl_Codeshow = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(264, 110);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(113, 20);
            this.comboBox1.TabIndex = 0;
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(454, 110);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(108, 20);
            this.comboBox2.TabIndex = 1;
            // 
            // lbl_Code
            // 
            this.lbl_Code.AutoSize = true;
            this.lbl_Code.Font = new System.Drawing.Font("여기어때 잘난체", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_Code.Location = new System.Drawing.Point(59, 61);
            this.lbl_Code.Name = "lbl_Code";
            this.lbl_Code.Size = new System.Drawing.Size(95, 27);
            this.lbl_Code.TabIndex = 3;
            this.lbl_Code.Tag = "1";
            this.lbl_Code.Text = "label1";
            // 
            // lbl_City
            // 
            this.lbl_City.AutoSize = true;
            this.lbl_City.Font = new System.Drawing.Font("여기어때 잘난체", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_City.Location = new System.Drawing.Point(259, 61);
            this.lbl_City.Name = "lbl_City";
            this.lbl_City.Size = new System.Drawing.Size(95, 27);
            this.lbl_City.TabIndex = 4;
            this.lbl_City.Tag = "2";
            this.lbl_City.Text = "label1";
            // 
            // lbl_Gu
            // 
            this.lbl_Gu.AutoSize = true;
            this.lbl_Gu.Font = new System.Drawing.Font("여기어때 잘난체", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_Gu.Location = new System.Drawing.Point(465, 61);
            this.lbl_Gu.Name = "lbl_Gu";
            this.lbl_Gu.Size = new System.Drawing.Size(97, 27);
            this.lbl_Gu.TabIndex = 5;
            this.lbl_Gu.Tag = "3";
            this.lbl_Gu.Text = "label2";
            // 
            // lbl_dong
            // 
            this.lbl_dong.AutoSize = true;
            this.lbl_dong.Font = new System.Drawing.Font("여기어때 잘난체", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_dong.Location = new System.Drawing.Point(653, 61);
            this.lbl_dong.Name = "lbl_dong";
            this.lbl_dong.Size = new System.Drawing.Size(97, 27);
            this.lbl_dong.TabIndex = 6;
            this.lbl_dong.Tag = "4";
            this.lbl_dong.Text = "label3";
            // 
            // lbl_Codeshow
            // 
            this.lbl_Codeshow.AutoSize = true;
            this.lbl_Codeshow.Font = new System.Drawing.Font("여기어때 잘난체", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_Codeshow.Location = new System.Drawing.Point(59, 103);
            this.lbl_Codeshow.Name = "lbl_Codeshow";
            this.lbl_Codeshow.Size = new System.Drawing.Size(95, 27);
            this.lbl_Codeshow.TabIndex = 7;
            this.lbl_Codeshow.Text = "label1";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(658, 112);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lbl_Codeshow);
            this.Controls.Add(this.lbl_dong);
            this.Controls.Add(this.lbl_Gu);
            this.Controls.Add(this.lbl_City);
            this.Controls.Add(this.lbl_Code);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.comboBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Click += new System.EventHandler(this.Form1_Click);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label lbl_Code;
        private System.Windows.Forms.Label lbl_City;
        private System.Windows.Forms.Label lbl_Gu;
        private System.Windows.Forms.Label lbl_dong;
        private System.Windows.Forms.Label lbl_Codeshow;
        private System.Windows.Forms.Button button1;
    }
}

