namespace Grain_Species_determination
{
    partial class frm_Register
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tb_Code = new System.Windows.Forms.TextBox();
            this.pb_bg = new System.Windows.Forms.PictureBox();
            this.tb_Addr = new System.Windows.Forms.TextBox();
            this.tb_companyNo = new System.Windows.Forms.TextBox();
            this.tb_Name = new System.Windows.Forms.TextBox();
            this.tb_Phone = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pb_confirm = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pb_bg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_confirm)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.label2.Font = new System.Drawing.Font("나눔스퀘어", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(799, 679);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 18);
            this.label2.TabIndex = 1;
            this.label2.Text = "설치주소";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.label5.Font = new System.Drawing.Font("나눔스퀘어", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(799, 519);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 18);
            this.label5.TabIndex = 4;
            this.label5.Text = "담당자";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.label6.Font = new System.Drawing.Font("나눔스퀘어", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(799, 599);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 18);
            this.label6.TabIndex = 5;
            this.label6.Text = "연락처";
            // 
            // tb_Code
            // 
            this.tb_Code.Font = new System.Drawing.Font("굴림", 20F);
            this.tb_Code.Location = new System.Drawing.Point(802, 383);
            this.tb_Code.Name = "tb_Code";
            this.tb_Code.Size = new System.Drawing.Size(317, 38);
            this.tb_Code.TabIndex = 11;
            this.tb_Code.TextChanged += new System.EventHandler(this.tb_Code_TextChanged);
            // 
            // pb_bg
            // 
            this.pb_bg.BackColor = System.Drawing.Color.Transparent;
            this.pb_bg.Image = global::Grain_Species_determination.Properties.Resources.Register;
            this.pb_bg.Location = new System.Drawing.Point(759, 188);
            this.pb_bg.Name = "pb_bg";
            this.pb_bg.Size = new System.Drawing.Size(400, 700);
            this.pb_bg.TabIndex = 17;
            this.pb_bg.TabStop = false;
            this.pb_bg.WaitOnLoad = true;
            // 
            // tb_Addr
            // 
            this.tb_Addr.Font = new System.Drawing.Font("굴림", 20F);
            this.tb_Addr.Location = new System.Drawing.Point(802, 703);
            this.tb_Addr.Multiline = true;
            this.tb_Addr.Name = "tb_Addr";
            this.tb_Addr.Size = new System.Drawing.Size(317, 38);
            this.tb_Addr.TabIndex = 18;
            this.tb_Addr.TextChanged += new System.EventHandler(this.tb_Code_TextChanged);
            // 
            // tb_companyNo
            // 
            this.tb_companyNo.Font = new System.Drawing.Font("굴림", 20F);
            this.tb_companyNo.Location = new System.Drawing.Point(802, 463);
            this.tb_companyNo.Name = "tb_companyNo";
            this.tb_companyNo.Size = new System.Drawing.Size(317, 38);
            this.tb_companyNo.TabIndex = 19;
            this.tb_companyNo.TextChanged += new System.EventHandler(this.tb_Code_TextChanged);
            // 
            // tb_Name
            // 
            this.tb_Name.Font = new System.Drawing.Font("굴림", 20F);
            this.tb_Name.Location = new System.Drawing.Point(802, 543);
            this.tb_Name.Name = "tb_Name";
            this.tb_Name.Size = new System.Drawing.Size(317, 38);
            this.tb_Name.TabIndex = 20;
            this.tb_Name.TextChanged += new System.EventHandler(this.tb_Code_TextChanged);
            // 
            // tb_Phone
            // 
            this.tb_Phone.Font = new System.Drawing.Font("굴림", 20F);
            this.tb_Phone.Location = new System.Drawing.Point(802, 623);
            this.tb_Phone.Name = "tb_Phone";
            this.tb_Phone.Size = new System.Drawing.Size(317, 38);
            this.tb_Phone.TabIndex = 21;
            this.tb_Phone.TextChanged += new System.EventHandler(this.tb_Code_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.label1.Font = new System.Drawing.Font("나눔스퀘어", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(799, 439);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 18);
            this.label1.TabIndex = 22;
            this.label1.Text = "사업자번호";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.label3.Font = new System.Drawing.Font("나눔스퀘어", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(799, 359);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 18);
            this.label3.TabIndex = 23;
            this.label3.Text = "제품번호";
            // 
            // pb_confirm
            // 
            this.pb_confirm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.pb_confirm.Image = global::Grain_Species_determination.Properties.Resources.n_confirm;
            this.pb_confirm.Location = new System.Drawing.Point(800, 764);
            this.pb_confirm.Name = "pb_confirm";
            this.pb_confirm.Size = new System.Drawing.Size(320, 60);
            this.pb_confirm.TabIndex = 24;
            this.pb_confirm.TabStop = false;
            this.pb_confirm.MouseDown += new System.Windows.Forms.MouseEventHandler(this.psPressed);
            this.pb_confirm.MouseUp += new System.Windows.Forms.MouseEventHandler(this.psUnpressed);
            // 
            // frm_Register
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.ClientSize = new System.Drawing.Size(1920, 1080);
            this.Controls.Add(this.pb_confirm);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tb_Phone);
            this.Controls.Add(this.tb_Name);
            this.Controls.Add(this.tb_companyNo);
            this.Controls.Add(this.tb_Addr);
            this.Controls.Add(this.tb_Code);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pb_bg);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frm_Register";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frm_Register";
            this.Load += new System.EventHandler(this.frm_Register_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pb_bg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_confirm)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tb_Code;
        private System.Windows.Forms.PictureBox pb_bg;
        private System.Windows.Forms.TextBox tb_Addr;
        private System.Windows.Forms.TextBox tb_companyNo;
        private System.Windows.Forms.TextBox tb_Name;
        private System.Windows.Forms.TextBox tb_Phone;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pb_confirm;
    }
}