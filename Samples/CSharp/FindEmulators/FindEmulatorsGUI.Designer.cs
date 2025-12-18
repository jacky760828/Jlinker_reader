namespace JLink_Find_Emulators
{
  partial class FindEmulatorsGUI
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
            this.button2 = new System.Windows.Forms.Button();
            this.status_lab = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.tb_app_bin_path = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.MCU_lab = new System.Windows.Forms.Label();
            this.Log_text = new System.Windows.Forms.RichTextBox();
            this.burn_button = new System.Windows.Forms.Button();
            this.TxtAddr = new System.Windows.Forms.TextBox();
            this.fw_v = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.product = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.V2_OTP_LT = new System.Windows.Forms.TextBox();
            this.V1_OTP_LT = new System.Windows.Forms.TextBox();
            this.NEW_FONT = new System.Windows.Forms.CheckBox();
            this.OLD_FONT = new System.Windows.Forms.CheckBox();
            this.V3_OTP_LT = new System.Windows.Forms.TextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.fandataGridView = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fandataGridView)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(3, 32);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(103, 52);
            this.button2.TabIndex = 1;
            this.button2.Text = "連線";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.SearchDevices);
            // 
            // status_lab
            // 
            this.status_lab.AutoSize = true;
            this.status_lab.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.status_lab.Location = new System.Drawing.Point(130, 39);
            this.status_lab.Name = "status_lab";
            this.status_lab.Size = new System.Drawing.Size(86, 31);
            this.status_lab.TabIndex = 3;
            this.status_lab.Text = "未連線";
            this.status_lab.Click += new System.EventHandler(this.label1_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(635, 78);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(104, 50);
            this.button1.TabIndex = 4;
            this.button1.Text = "讀取檔案";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tb_app_bin_path
            // 
            this.tb_app_bin_path.Location = new System.Drawing.Point(12, 91);
            this.tb_app_bin_path.Name = "tb_app_bin_path";
            this.tb_app_bin_path.Size = new System.Drawing.Size(602, 29);
            this.tb_app_bin_path.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(529, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 18);
            this.label1.TabIndex = 6;
            this.label1.Text = "IC名稱:";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(601, 49);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(0, 18);
            this.linkLabel1.TabIndex = 7;
            // 
            // MCU_lab
            // 
            this.MCU_lab.AutoSize = true;
            this.MCU_lab.Location = new System.Drawing.Point(601, 49);
            this.MCU_lab.Name = "MCU_lab";
            this.MCU_lab.Size = new System.Drawing.Size(97, 18);
            this.MCU_lab.TabIndex = 8;
            this.MCU_lab.Text = "M031FB0AE";
            this.MCU_lab.Click += new System.EventHandler(this.MCU_lab_Click);
            // 
            // Log_text
            // 
            this.Log_text.Location = new System.Drawing.Point(0, 6);
            this.Log_text.Name = "Log_text";
            this.Log_text.Size = new System.Drawing.Size(903, 262);
            this.Log_text.TabIndex = 9;
            this.Log_text.Text = "";
            // 
            // burn_button
            // 
            this.burn_button.Location = new System.Drawing.Point(519, 147);
            this.burn_button.Name = "burn_button";
            this.burn_button.Size = new System.Drawing.Size(109, 56);
            this.burn_button.TabIndex = 10;
            this.burn_button.Text = "燒錄";
            this.burn_button.UseVisualStyleBackColor = true;
            this.burn_button.Click += new System.EventHandler(this.burn_button_Click);
            // 
            // TxtAddr
            // 
            this.TxtAddr.Location = new System.Drawing.Point(158, 147);
            this.TxtAddr.Name = "TxtAddr";
            this.TxtAddr.Size = new System.Drawing.Size(216, 29);
            this.TxtAddr.TabIndex = 11;
            this.TxtAddr.Text = "20000000";
            // 
            // fw_v
            // 
            this.fw_v.Location = new System.Drawing.Point(-4, 157);
            this.fw_v.Name = "fw_v";
            this.fw_v.Size = new System.Drawing.Size(127, 37);
            this.fw_v.TabIndex = 12;
            this.fw_v.Text = "產品規格";
            this.fw_v.UseVisualStyleBackColor = true;
            this.fw_v.Click += new System.EventHandler(this.fw_v_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(81, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(13, 18);
            this.label2.TabIndex = 15;
            this.label2.Text = "/";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(151, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(13, 18);
            this.label3.TabIndex = 16;
            this.label3.Text = "/";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 18);
            this.label4.TabIndex = 17;
            this.label4.Text = "year";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(1, 263);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(127, 34);
            this.button3.TabIndex = 18;
            this.button3.Text = "韌體版本";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // product
            // 
            this.product.AutoSize = true;
            this.product.Location = new System.Drawing.Point(131, 207);
            this.product.Name = "product";
            this.product.Size = new System.Drawing.Size(0, 18);
            this.product.TabIndex = 19;
            this.product.UseMnemonic = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(134, 253);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(260, 88);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "版本資訊";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(170, 44);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(39, 18);
            this.label6.TabIndex = 19;
            this.label6.Text = "time";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(95, 44);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 18);
            this.label5.TabIndex = 18;
            this.label5.Text = "month";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(15, 309);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(108, 18);
            this.label7.TabIndex = 21;
            this.label7.Text = "年/月日/時分";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(427, 253);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(85, 18);
            this.label8.TabIndex = 22;
            this.label8.Text = "模組規格:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(451, 297);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(32, 18);
            this.label9.TabIndex = 23;
            this.label9.Text = "0W";
            this.label9.Click += new System.EventHandler(this.label9_Click);
            // 
            // V2_OTP_LT
            // 
            this.V2_OTP_LT.Location = new System.Drawing.Point(1360, 481);
            this.V2_OTP_LT.Name = "V2_OTP_LT";
            this.V2_OTP_LT.Size = new System.Drawing.Size(100, 29);
            this.V2_OTP_LT.TabIndex = 13;
            // 
            // V1_OTP_LT
            // 
            this.V1_OTP_LT.Location = new System.Drawing.Point(1360, 427);
            this.V1_OTP_LT.Name = "V1_OTP_LT";
            this.V1_OTP_LT.Size = new System.Drawing.Size(100, 29);
            this.V1_OTP_LT.TabIndex = 11;
            this.V1_OTP_LT.TextChanged += new System.EventHandler(this.V1_OTP_LT_TextChanged);
            // 
            // NEW_FONT
            // 
            this.NEW_FONT.AutoSize = true;
            this.NEW_FONT.Location = new System.Drawing.Point(579, 305);
            this.NEW_FONT.Name = "NEW_FONT";
            this.NEW_FONT.Size = new System.Drawing.Size(88, 22);
            this.NEW_FONT.TabIndex = 25;
            this.NEW_FONT.Text = "新格式";
            this.NEW_FONT.UseVisualStyleBackColor = true;
            this.NEW_FONT.CheckedChanged += new System.EventHandler(this.NEW_FONT_CheckedChanged);
            // 
            // OLD_FONT
            // 
            this.OLD_FONT.AutoSize = true;
            this.OLD_FONT.Location = new System.Drawing.Point(687, 305);
            this.OLD_FONT.Name = "OLD_FONT";
            this.OLD_FONT.Size = new System.Drawing.Size(88, 22);
            this.OLD_FONT.TabIndex = 26;
            this.OLD_FONT.Text = "舊格式";
            this.OLD_FONT.UseVisualStyleBackColor = true;
            // 
            // V3_OTP_LT
            // 
            this.V3_OTP_LT.Location = new System.Drawing.Point(1360, 544);
            this.V3_OTP_LT.Name = "V3_OTP_LT";
            this.V3_OTP_LT.Size = new System.Drawing.Size(100, 29);
            this.V3_OTP_LT.TabIndex = 20;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(1220, 430);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(123, 18);
            this.label28.TabIndex = 20;
            this.label28.Text = "低溫判斷點V1:";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(1220, 492);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(123, 18);
            this.label29.TabIndex = 27;
            this.label29.Text = "低溫判斷點V2:";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(1220, 547);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(123, 18);
            this.label30.TabIndex = 28;
            this.label30.Text = "低溫判斷點V3:";
            // 
            // tabControl1
            // 
            this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(52, 358);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1092, 303);
            this.tabControl1.TabIndex = 29;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.fandataGridView);
            this.tabPage1.Location = new System.Drawing.Point(4, 31);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1084, 268);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "機種風扇參數";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // fandataGridView
            // 
            this.fandataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.fandataGridView.Location = new System.Drawing.Point(0, 21);
            this.fandataGridView.Name = "fandataGridView";
            this.fandataGridView.RowHeadersWidth = 62;
            this.fandataGridView.RowTemplate.Height = 31;
            this.fandataGridView.Size = new System.Drawing.Size(1081, 332);
            this.fandataGridView.TabIndex = 0;
            this.fandataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.Log_text);
            this.tabPage2.Location = new System.Drawing.Point(4, 31);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(911, 268);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Jlink-log";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // FindEmulatorsGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1509, 746);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label30);
            this.Controls.Add(this.label29);
            this.Controls.Add(this.label28);
            this.Controls.Add(this.V3_OTP_LT);
            this.Controls.Add(this.V2_OTP_LT);
            this.Controls.Add(this.OLD_FONT);
            this.Controls.Add(this.NEW_FONT);
            this.Controls.Add(this.V1_OTP_LT);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.product);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.fw_v);
            this.Controls.Add(this.TxtAddr);
            this.Controls.Add(this.burn_button);
            this.Controls.Add(this.MCU_lab);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tb_app_bin_path);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.status_lab);
            this.Controls.Add(this.button2);
            this.Name = "FindEmulatorsGUI";
            this.Text = "Core_V2燒錄程式";
            this.Load += new System.EventHandler(this.FindEmulatorsGUI_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fandataGridView)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label status_lab;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox tb_app_bin_path;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label MCU_lab;
        private System.Windows.Forms.RichTextBox Log_text;
        private System.Windows.Forms.Button burn_button;
        private System.Windows.Forms.TextBox TxtAddr;
        private System.Windows.Forms.Button fw_v;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label product;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox NEW_FONT;
        private System.Windows.Forms.CheckBox OLD_FONT;
        private System.Windows.Forms.TextBox V2_OTP_LT;
        private System.Windows.Forms.TextBox V1_OTP_LT;
        private System.Windows.Forms.TextBox V3_OTP_LT;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView fandataGridView;
    }
}

