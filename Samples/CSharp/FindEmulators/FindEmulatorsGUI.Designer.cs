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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.PWM12V_END = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.V3_12V = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.PWM12V_START = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.V2_12V = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.V1_12V = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.PWM5V_END = new System.Windows.Forms.TextBox();
            this.PWM5V_START = new System.Windows.Forms.TextBox();
            this.V2_5V = new System.Windows.Forms.TextBox();
            this.V1_5V = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.PWM3V_END = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.PWM3V_START = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.V2_3V = new System.Windows.Forms.TextBox();
            this.V1_3V = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.OTP_PWM_END = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.V3_OTP = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.V2_OTP_LT = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.V1_OTP_LT = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.V1_OTP = new System.Windows.Forms.TextBox();
            this.V2_OTP = new System.Windows.Forms.TextBox();
            this.V3_OTP_LT = new System.Windows.Forms.TextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.MAX_PWM = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.MIN_PWM = new System.Windows.Forms.TextBox();
            this.label32 = new System.Windows.Forms.Label();
            this.slope_timer = new System.Windows.Forms.TextBox();
            this.label33 = new System.Windows.Forms.Label();
            this.buffer_time = new System.Windows.Forms.TextBox();
            this.SR = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.SR_OTP = new System.Windows.Forms.TextBox();
            this.TA_OTP = new System.Windows.Forms.TextBox();
            this.label35 = new System.Windows.Forms.Label();
            this.opp = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
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
            this.Log_text.Location = new System.Drawing.Point(853, 32);
            this.Log_text.Name = "Log_text";
            this.Log_text.Size = new System.Drawing.Size(528, 283);
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
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Location = new System.Drawing.Point(18, 347);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(873, 319);
            this.tabControl1.TabIndex = 24;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Moccasin;
            this.tabPage1.Controls.Add(this.buffer_time);
            this.tabPage1.Controls.Add(this.label33);
            this.tabPage1.Controls.Add(this.slope_timer);
            this.tabPage1.Controls.Add(this.label32);
            this.tabPage1.Controls.Add(this.MIN_PWM);
            this.tabPage1.Controls.Add(this.label31);
            this.tabPage1.Controls.Add(this.label25);
            this.tabPage1.Controls.Add(this.MAX_PWM);
            this.tabPage1.Controls.Add(this.PWM12V_END);
            this.tabPage1.Controls.Add(this.label14);
            this.tabPage1.Controls.Add(this.V3_12V);
            this.tabPage1.Controls.Add(this.label13);
            this.tabPage1.Controls.Add(this.PWM12V_START);
            this.tabPage1.Controls.Add(this.label12);
            this.tabPage1.Controls.Add(this.V2_12V);
            this.tabPage1.Controls.Add(this.label11);
            this.tabPage1.Controls.Add(this.V1_12V);
            this.tabPage1.Controls.Add(this.label10);
            this.tabPage1.Location = new System.Drawing.Point(4, 28);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(865, 287);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "12V感測電流和PWM";
            this.tabPage1.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // PWM12V_END
            // 
            this.PWM12V_END.Location = new System.Drawing.Point(579, 136);
            this.PWM12V_END.Name = "PWM12V_END";
            this.PWM12V_END.Size = new System.Drawing.Size(100, 29);
            this.PWM12V_END.TabIndex = 9;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(448, 136);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(125, 18);
            this.label14.TabIndex = 8;
            this.label14.Text = "PWM輸出結束:";
            // 
            // V3_12V
            // 
            this.V3_12V.Location = new System.Drawing.Point(285, 157);
            this.V3_12V.Name = "V3_12V";
            this.V3_12V.Size = new System.Drawing.Size(100, 29);
            this.V3_12V.TabIndex = 7;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(27, 160);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(249, 18);
            this.label13.TabIndex = 6;
            this.label13.Text = "12V_MCU電流檢知電壓第三點:";
            // 
            // PWM12V_START
            // 
            this.PWM12V_START.Location = new System.Drawing.Point(579, 48);
            this.PWM12V_START.Name = "PWM12V_START";
            this.PWM12V_START.Size = new System.Drawing.Size(100, 29);
            this.PWM12V_START.TabIndex = 5;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(448, 56);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(125, 18);
            this.label12.TabIndex = 4;
            this.label12.Text = "PWM輸出開始:";
            // 
            // V2_12V
            // 
            this.V2_12V.Location = new System.Drawing.Point(285, 102);
            this.V2_12V.Name = "V2_12V";
            this.V2_12V.Size = new System.Drawing.Size(100, 29);
            this.V2_12V.TabIndex = 3;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(27, 105);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(249, 18);
            this.label11.TabIndex = 2;
            this.label11.Text = "12V_MCU電流檢知電壓第二點:";
            // 
            // V1_12V
            // 
            this.V1_12V.Location = new System.Drawing.Point(282, 45);
            this.V1_12V.Name = "V1_12V";
            this.V1_12V.Size = new System.Drawing.Size(100, 29);
            this.V1_12V.TabIndex = 1;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(27, 48);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(249, 18);
            this.label10.TabIndex = 0;
            this.label10.Text = "12V_MCU電流檢知電壓第一點:";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.Moccasin;
            this.tabPage2.Controls.Add(this.PWM5V_END);
            this.tabPage2.Controls.Add(this.PWM5V_START);
            this.tabPage2.Controls.Add(this.V2_5V);
            this.tabPage2.Controls.Add(this.V1_5V);
            this.tabPage2.Controls.Add(this.label16);
            this.tabPage2.Controls.Add(this.label15);
            this.tabPage2.Controls.Add(this.label17);
            this.tabPage2.Controls.Add(this.label18);
            this.tabPage2.Location = new System.Drawing.Point(4, 28);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(865, 287);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "5V感測電流和PWM";
            // 
            // PWM5V_END
            // 
            this.PWM5V_END.Location = new System.Drawing.Point(591, 140);
            this.PWM5V_END.Name = "PWM5V_END";
            this.PWM5V_END.Size = new System.Drawing.Size(100, 29);
            this.PWM5V_END.TabIndex = 13;
            // 
            // PWM5V_START
            // 
            this.PWM5V_START.Location = new System.Drawing.Point(591, 52);
            this.PWM5V_START.Name = "PWM5V_START";
            this.PWM5V_START.Size = new System.Drawing.Size(100, 29);
            this.PWM5V_START.TabIndex = 11;
            this.PWM5V_START.TextChanged += new System.EventHandler(this.PWM5V_START_TextChanged);
            // 
            // V2_5V
            // 
            this.V2_5V.Location = new System.Drawing.Point(305, 117);
            this.V2_5V.Name = "V2_5V";
            this.V2_5V.Size = new System.Drawing.Size(100, 29);
            this.V2_5V.TabIndex = 4;
            // 
            // V1_5V
            // 
            this.V1_5V.Location = new System.Drawing.Point(305, 60);
            this.V1_5V.Name = "V1_5V";
            this.V1_5V.Size = new System.Drawing.Size(100, 29);
            this.V1_5V.TabIndex = 3;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(58, 128);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(241, 18);
            this.label16.TabIndex = 2;
            this.label16.Text = "5V_MCU電流檢知電壓第一點:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(58, 63);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(241, 18);
            this.label15.TabIndex = 1;
            this.label15.Text = "5V_MCU電流檢知電壓第一點:";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(460, 140);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(125, 18);
            this.label17.TabIndex = 12;
            this.label17.Text = "PWM輸出結束:";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(460, 60);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(125, 18);
            this.label18.TabIndex = 10;
            this.label18.Text = "PWM輸出開始:";
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.Moccasin;
            this.tabPage3.Controls.Add(this.PWM3V_END);
            this.tabPage3.Controls.Add(this.label19);
            this.tabPage3.Controls.Add(this.PWM3V_START);
            this.tabPage3.Controls.Add(this.label20);
            this.tabPage3.Controls.Add(this.V2_3V);
            this.tabPage3.Controls.Add(this.V1_3V);
            this.tabPage3.Controls.Add(this.label21);
            this.tabPage3.Controls.Add(this.label22);
            this.tabPage3.Location = new System.Drawing.Point(4, 28);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(865, 287);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "3.3V感測電流和PWM";
            // 
            // PWM3V_END
            // 
            this.PWM3V_END.Location = new System.Drawing.Point(617, 155);
            this.PWM3V_END.Name = "PWM3V_END";
            this.PWM3V_END.Size = new System.Drawing.Size(100, 29);
            this.PWM3V_END.TabIndex = 21;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(481, 166);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(125, 18);
            this.label19.TabIndex = 20;
            this.label19.Text = "PWM輸出結束:";
            // 
            // PWM3V_START
            // 
            this.PWM3V_START.Location = new System.Drawing.Point(617, 70);
            this.PWM3V_START.Name = "PWM3V_START";
            this.PWM3V_START.Size = new System.Drawing.Size(100, 29);
            this.PWM3V_START.TabIndex = 19;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(481, 81);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(125, 18);
            this.label20.TabIndex = 18;
            this.label20.Text = "PWM輸出開始:";
            // 
            // V2_3V
            // 
            this.V2_3V.Location = new System.Drawing.Point(337, 140);
            this.V2_3V.Name = "V2_3V";
            this.V2_3V.Size = new System.Drawing.Size(100, 29);
            this.V2_3V.TabIndex = 17;
            // 
            // V1_3V
            // 
            this.V1_3V.Location = new System.Drawing.Point(337, 78);
            this.V1_3V.Name = "V1_3V";
            this.V1_3V.Size = new System.Drawing.Size(100, 29);
            this.V1_3V.TabIndex = 16;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(90, 143);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(241, 18);
            this.label21.TabIndex = 15;
            this.label21.Text = "3V_MCU電流檢知電壓第二點:";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(90, 81);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(241, 18);
            this.label22.TabIndex = 14;
            this.label22.Text = "3V_MCU電流檢知電壓第一點:";
            this.label22.Click += new System.EventHandler(this.label22_Click);
            // 
            // tabPage4
            // 
            this.tabPage4.BackColor = System.Drawing.Color.Moccasin;
            this.tabPage4.Controls.Add(this.TA_OTP);
            this.tabPage4.Controls.Add(this.SR_OTP);
            this.tabPage4.Controls.Add(this.label34);
            this.tabPage4.Controls.Add(this.SR);
            this.tabPage4.Controls.Add(this.OTP_PWM_END);
            this.tabPage4.Controls.Add(this.label23);
            this.tabPage4.Controls.Add(this.V3_OTP);
            this.tabPage4.Controls.Add(this.label24);
            this.tabPage4.Controls.Add(this.V2_OTP);
            this.tabPage4.Controls.Add(this.V1_OTP);
            this.tabPage4.Controls.Add(this.label26);
            this.tabPage4.Controls.Add(this.label27);
            this.tabPage4.Location = new System.Drawing.Point(4, 28);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(865, 287);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "OTP和風扇PWM";
            // 
            // OTP_PWM_END
            // 
            this.OTP_PWM_END.Location = new System.Drawing.Point(613, 106);
            this.OTP_PWM_END.Name = "OTP_PWM_END";
            this.OTP_PWM_END.Size = new System.Drawing.Size(100, 29);
            this.OTP_PWM_END.TabIndex = 19;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(467, 109);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(125, 18);
            this.label23.TabIndex = 18;
            this.label23.Text = "PWM輸出開始:";
            // 
            // V3_OTP
            // 
            this.V3_OTP.Location = new System.Drawing.Point(315, 161);
            this.V3_OTP.Name = "V3_OTP";
            this.V3_OTP.Size = new System.Drawing.Size(100, 29);
            this.V3_OTP.TabIndex = 17;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(57, 164);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(249, 18);
            this.label24.TabIndex = 16;
            this.label24.Text = "12V_MCU電流檢知電壓第三點:";
            // 
            // V2_OTP_LT
            // 
            this.V2_OTP_LT.Location = new System.Drawing.Point(1068, 492);
            this.V2_OTP_LT.Name = "V2_OTP_LT";
            this.V2_OTP_LT.Size = new System.Drawing.Size(100, 29);
            this.V2_OTP_LT.TabIndex = 13;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(57, 109);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(249, 18);
            this.label26.TabIndex = 12;
            this.label26.Text = "12V_MCU電流檢知電壓第二點:";
            // 
            // V1_OTP_LT
            // 
            this.V1_OTP_LT.Location = new System.Drawing.Point(1068, 432);
            this.V1_OTP_LT.Name = "V1_OTP_LT";
            this.V1_OTP_LT.Size = new System.Drawing.Size(100, 29);
            this.V1_OTP_LT.TabIndex = 11;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(57, 52);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(249, 18);
            this.label27.TabIndex = 10;
            this.label27.Text = "12V_MCU電流檢知電壓第一點:";
            // 
            // tabPage5
            // 
            this.tabPage5.BackColor = System.Drawing.Color.Moccasin;
            this.tabPage5.Controls.Add(this.opp);
            this.tabPage5.Controls.Add(this.label35);
            this.tabPage5.Location = new System.Drawing.Point(4, 28);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(865, 287);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "OPP觸發點";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(604, 239);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(109, 22);
            this.checkBox1.TabIndex = 25;
            this.checkBox1.Text = "checkBox1";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(604, 275);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(109, 22);
            this.checkBox2.TabIndex = 26;
            this.checkBox2.Text = "checkBox2";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // V1_OTP
            // 
            this.V1_OTP.Location = new System.Drawing.Point(315, 49);
            this.V1_OTP.Name = "V1_OTP";
            this.V1_OTP.Size = new System.Drawing.Size(100, 29);
            this.V1_OTP.TabIndex = 20;
            // 
            // V2_OTP
            // 
            this.V2_OTP.Location = new System.Drawing.Point(314, 106);
            this.V2_OTP.Name = "V2_OTP";
            this.V2_OTP.Size = new System.Drawing.Size(100, 29);
            this.V2_OTP.TabIndex = 20;
            // 
            // V3_OTP_LT
            // 
            this.V3_OTP_LT.Location = new System.Drawing.Point(1068, 544);
            this.V3_OTP_LT.Name = "V3_OTP_LT";
            this.V3_OTP_LT.Size = new System.Drawing.Size(100, 29);
            this.V3_OTP_LT.TabIndex = 20;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(939, 435);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(123, 18);
            this.label28.TabIndex = 20;
            this.label28.Text = "低溫判斷點V1:";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(939, 492);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(123, 18);
            this.label29.TabIndex = 27;
            this.label29.Text = "低溫判斷點V2:";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(939, 547);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(123, 18);
            this.label30.TabIndex = 28;
            this.label30.Text = "低溫判斷點V3:";
            // 
            // MAX_PWM
            // 
            this.MAX_PWM.Location = new System.Drawing.Point(127, 202);
            this.MAX_PWM.Name = "MAX_PWM";
            this.MAX_PWM.Size = new System.Drawing.Size(100, 29);
            this.MAX_PWM.TabIndex = 10;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(27, 205);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(85, 18);
            this.label25.TabIndex = 11;
            this.label25.Text = "最大轉數:";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(484, 216);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(85, 18);
            this.label31.TabIndex = 12;
            this.label31.Text = "斜率時間:";
            // 
            // MIN_PWM
            // 
            this.MIN_PWM.Location = new System.Drawing.Point(127, 237);
            this.MIN_PWM.Name = "MIN_PWM";
            this.MIN_PWM.Size = new System.Drawing.Size(100, 29);
            this.MIN_PWM.TabIndex = 13;
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(27, 240);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(85, 18);
            this.label32.TabIndex = 14;
            this.label32.Text = "最低轉數:";
            // 
            // slope_timer
            // 
            this.slope_timer.Location = new System.Drawing.Point(575, 205);
            this.slope_timer.Name = "slope_timer";
            this.slope_timer.Size = new System.Drawing.Size(100, 29);
            this.slope_timer.TabIndex = 15;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(448, 248);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(121, 18);
            this.label33.TabIndex = 16;
            this.label33.Text = "風扇緩衝時間:";
            // 
            // buffer_time
            // 
            this.buffer_time.Location = new System.Drawing.Point(576, 240);
            this.buffer_time.Name = "buffer_time";
            this.buffer_time.Size = new System.Drawing.Size(100, 29);
            this.buffer_time.TabIndex = 17;
            // 
            // SR
            // 
            this.SR.AutoSize = true;
            this.SR.Location = new System.Drawing.Point(57, 234);
            this.SR.Name = "SR";
            this.SR.Size = new System.Drawing.Size(108, 18);
            this.SR.TabIndex = 21;
            this.SR.Text = "SR_OTP電壓:";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(474, 234);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(110, 18);
            this.label34.TabIndex = 22;
            this.label34.Text = "TA_OTP電壓:";
            // 
            // SR_OTP
            // 
            this.SR_OTP.Location = new System.Drawing.Point(176, 231);
            this.SR_OTP.Name = "SR_OTP";
            this.SR_OTP.Size = new System.Drawing.Size(100, 29);
            this.SR_OTP.TabIndex = 23;
            // 
            // TA_OTP
            // 
            this.TA_OTP.Location = new System.Drawing.Point(590, 231);
            this.TA_OTP.Name = "TA_OTP";
            this.TA_OTP.Size = new System.Drawing.Size(100, 29);
            this.TA_OTP.TabIndex = 24;
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(193, 78);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(73, 18);
            this.label35.TabIndex = 0;
            this.label35.Text = "opp瓦數:";
            // 
            // opp
            // 
            this.opp.Location = new System.Drawing.Point(272, 75);
            this.opp.Name = "opp";
            this.opp.Size = new System.Drawing.Size(100, 29);
            this.opp.TabIndex = 21;
            // 
            // FindEmulatorsGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1477, 678);
            this.Controls.Add(this.label30);
            this.Controls.Add(this.label29);
            this.Controls.Add(this.label28);
            this.Controls.Add(this.V3_OTP_LT);
            this.Controls.Add(this.V2_OTP_LT);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.V1_OTP_LT);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.product);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.fw_v);
            this.Controls.Add(this.TxtAddr);
            this.Controls.Add(this.burn_button);
            this.Controls.Add(this.Log_text);
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
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
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
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.TextBox V1_12V;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox V2_12V;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox PWM12V_START;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox PWM12V_END;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox V3_12V;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox V2_5V;
        private System.Windows.Forms.TextBox V1_5V;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox PWM5V_END;
        private System.Windows.Forms.TextBox PWM5V_START;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox PWM3V_END;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox PWM3V_START;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox V2_3V;
        private System.Windows.Forms.TextBox V1_3V;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox OTP_PWM_END;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox V3_OTP;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TextBox V2_OTP_LT;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.TextBox V1_OTP_LT;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.TextBox V1_OTP;
        private System.Windows.Forms.TextBox V2_OTP;
        private System.Windows.Forms.TextBox V3_OTP_LT;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.TextBox MAX_PWM;
        private System.Windows.Forms.TextBox slope_timer;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.TextBox MIN_PWM;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.TextBox buffer_time;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.TextBox TA_OTP;
        private System.Windows.Forms.TextBox SR_OTP;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label SR;
        private System.Windows.Forms.TextBox opp;
        private System.Windows.Forms.Label label35;
    }
}

