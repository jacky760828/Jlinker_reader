using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using JLinkSDK;

using U8 = System.Byte;   // unsigned char
using U16 = System.UInt16; // unsigned short
using U32 = System.UInt32; // unsigned int
using U64 = System.UInt64; // unsigned long long

using I8 = System.SByte; // signed char
using I16 = System.Int16; // signed short
using I32 = System.Int32; // signed int
using I64 = System.Int64;
using System.Buffers.Binary; // signed long long




namespace JLink_Find_Emulators
{
   
    public enum ControlMode
    {
        Voltage = 1,
        PWM = 2,
        Slope = 3,
        BufferTime = 4,
        OPP_Watter = 5
    }

    public partial class FindEmulatorsGUI : Form
    {
        private Fan_data_read fan_read = new Fan_data_read();

        byte[] acOut;
        int divisor = 1920;
        byte[] acIn;
        string[] models = { "650W", "750W", "850W", "1000W", "550W" };
        public enum FirmwareKind { Old, New, Unknown }
        uint start_pwm_12v;
        uint end_pwm_12v;
        public byte[] dsp_file_content = null;
        /*********************************************************************
        *
        *       FindDevicesGUI()
        *
        *  Function description
        *    Constructor for the GUI class. 
        */
        public FindEmulatorsGUI()
        {
            InitializeComponent();
        }


        public FirmwareKind GetFirmware()
        {
            if (OLD_FONT.Checked) return FirmwareKind.Old;
            if (NEW_FONT.Checked) return FirmwareKind.New;
            return FirmwareKind.Unknown;
        }

        /*********************************************************************
        *
        *       SearchDevices()
        *
        *  Function description
        *    This function is triggered, when the user presses the Search 
         *    button. This function reads the connected devices with the 
         *    SDK GetEmuList function to retrieve all data. Afterwards, 
         *    the data is feed into a data table, which is linked to a 
         *    DataGridView. 
        */
        private unsafe void SearchDevices(object sender, EventArgs e)
        {
            int NumDevices;
            int i;
           
            const int MaxDevices = 255;

            string mcu_name = "M031FB0AE";
            JLINKARM_EMU_CONNECT_INFO[] CInfo = new JLINKARM_EMU_CONNECT_INFO[MaxDevices];
            DataTable DataTbl;

            //
            // Retrieve data from JLink DLL and get number of connected devices. 
            //
            NumDevices = JLink.GetEmuList(MaxDevices, CInfo, MaxDevices);

            if (CInfo[0].Connection > 0)
            {
                status_lab.Text = CInfo[0].acNickName + "已連線";
            }
            else
            {
                status_lab.Text = "未連線";
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void FindEmulatorsGUI_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sourcedir = @"c:\Core_V2";
            string msg;
            DirectoryInfo sdinfo = new DirectoryInfo(sourcedir);
            if (!sdinfo.Exists)
            {
                sdinfo.Create();
                MessageBox.Show("目錄建立c:\\Core_V2");
            }

            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = "c:\\Core_V2";
            openFileDialog1.Filter = "Database files (*.hex)|*.hex;";
            openFileDialog1.FilterIndex = 0;
            openFileDialog1.RestoreDirectory = true;


            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                dsp_file_content = null;
                dsp_file_content = File.ReadAllBytes(openFileDialog1.FileName);
                tb_app_bin_path.Text = openFileDialog1.FileName;
                //MessageBox.Show("Open DSP bin file success");
                //   int num = (int)MessageBox.Show("Open DSP Image File success", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);//DefaultDesktopOnly  ServiceNotification
                // Cyacd_found = true;


            }
            else
            {
                //MessageBox.Show("Open DSP bin file fail");
                //  int num = (int)MessageBox.Show("Open DSP Image File", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                //int num = (int)Show("Open DSP Image File", "Error", MessageBoxButtons.OK);//, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                tb_app_bin_path.Text = "";
                dsp_file_content = null;
            }
        }



        private void LogLine(String Text)
        {
            Log_text.Text += Text + '\n';
            Log_text.SelectionStart = Log_text.Text.Length;
            Log_text.ScrollToCaret();
        }
        private int Connect()
        {
            int Result;
            byte[] acOut;
            byte[] acIn;
            string sError;
            //
            // Init locals
            //
            acOut = new byte[256];
            acIn = new byte[256];
            //
            // Select HIF
            LogLine("Connecting to J-Link...");
            sError = JLink.OpenEx(null, pJLINK_LOG_callback_pfErrorOut);
            if (sError != null)
            {
                LogLine(sError);
                return -1;
            }
            LogLine("O.K.");
            //
            // Setup settings file
            //
            acIn = Array.ConvertAll((Encoding.ASCII.GetBytes("ProjectFile = " + 0)), q => Convert.ToByte(q));
            JLink.ExecCommand(acIn, acOut, 256);


            //
            // Select device
            //
            acIn = Array.ConvertAll((Encoding.ASCII.GetBytes("Device = " + MCU_lab.Text)), q => Convert.ToByte(q));
            JLink.ExecCommand(acIn, acOut, 256);
            //
            // Select TIF (JTAG / SWD / ...) + TIF speed
            //
            //if (CmbTIF.Text == "JTAG")
            // {
            // JLink.TIF_Select(0);
            // }
            // else
            // {
            JLink.TIF_Select(1);
            // }
            JLink.SetSpeed(Convert.ToInt32(4000));//
            //
            // Connect to target
            //
            Result = JLink.Connect();
            LogLine("Connecting to the target...");
            if (Result < 0)
            {
                LogLine("Failed");
                JLink.Close();
                //    return -1;
            }
            LogLine("O.K.12345");
            return 0;
        }
        private void burn_button_Click(object sender, EventArgs e)
        {
            int Result;
            U32 Addr;

            Connect();

            Addr = Convert.ToUInt32(TxtAddr.Text, 16);          // Get Addr
            Result = JLink.DownloadFile(tb_app_bin_path.Text, Addr);
            if (Result < 0)
            {
                LogLine("Failed");
            }
            else
            {
                LogLine("O.K.");
            }
            //
            // Close connection
            //
            JLink.Close();
            LogLine("Disconnected from target and J-Link.");
            return;


        }






        /*********************************************************************
       *
       *       JLINK_LOG_callback_pfErrorOut()
       *
       *  Function description
       *    Callback function passed to the J-Link DLL to manage error out.
       */
        private static void JLINK_LOG_callback_pfErrorOut(string s)
        {
        }

        private static JLink.JLINK_LOG pJLINK_LOG_callback_pfErrorOut = JLINK_LOG_callback_pfErrorOut;

        private void fw_v_Click(object sender, EventArgs e)
        {
            JLINKARM_EMU_CONNECT_INFO[] aConnectInfo;
            int r;
            int i;
            int Result;
            byte[] acOut;
            byte[] acIn;
            string sError;
            string sTmp;
            U32[] acData;
            U8[] acTmp;
            U32 NumItems;
            U32 Addr;
            //
            // Init locals
            //
            acOut = new byte[256];
            acIn = new byte[256];
            acData = new U32[16];
            acTmp = new U8[100];
            //
            // Select HIF
            //
            product.Text = "";
            //
            // Print information about connected probes/ programmers
            //
            LogLine("Listing information about connected probes/ programmers...");
            aConnectInfo = new JLINKARM_EMU_CONNECT_INFO[16];
            r = JLink.GetEmuList(JLink.HOSTIF_USB | JLink.HOSTIF_IP, aConnectInfo, 16);  // Get information about up to 16 probes/ programmers connected via USB or TCP/IP
            if (r < 0) {
                LogLine("Failed to retrieve information about connected probes/ programmers...");
            } else {
                LogLine("Found " + r + " connected probes/ programmers!");
                r = (r < 16) ? r : 16;   // Do not report about more probes/ programmers than we were able to store
                for (i = 0; i < r; i++) {
                    if (aConnectInfo[i].Connection == JLink.HOSTIF_USB) {
                        sTmp = "USB";
                    } else {
                        sTmp = "TCP/IP";
                    }
                    LogLine("#" + (i + 1) + " | S/N = " + aConnectInfo[i].SerialNumber + " | Product = " + aConnectInfo[i].acProduct + " | Connected via " + sTmp);
                }
            }
            LogLine("O.K.");
            //
            // Open connection to J-Link
            //
            LogLine("Connecting to J-Link...");
            sError = JLink.OpenEx(null, pJLINK_LOG_callback_pfErrorOut);
            if (sError != null) {
                LogLine(sError);
                return;
            }
            LogLine("O.K.");
            //
            // Setup settings file
            //
            acIn = Array.ConvertAll((Encoding.ASCII.GetBytes("ProjectFile = " + 0)), q => Convert.ToByte(q));
            JLink.ExecCommand(acIn, acOut, 256);
            //
            // Select device
            //
            acIn = Array.ConvertAll((Encoding.ASCII.GetBytes("Device = " + MCU_lab.Text)), q => Convert.ToByte(q));
            JLink.ExecCommand(acIn, acOut, 256);
            //
            // Select TIF (JTAG / SWD / ...) + TIF speed
            //
            // if (CmbTIF.Text == "JTAG") {
            //  JLink.TIF_Select(0);
            //} else {
            JLink.TIF_Select(1);
            // }
            JLink.SetSpeed(Convert.ToInt32(4000));
            //
            // Connect to target
            //
            Result = JLink.Connect();
            LogLine("Connecting to the target...");
            if (Result < 0) {
                LogLine("Failed");
                goto Close;
            }
            LogLine("O.K.");
            //
            // Read 16 words from the target
            //

            // string READ_addr=(0x3e04).ToString();


            NumItems = 4;                                  // Number of items to read
                                                           // Addr = Convert.ToUInt32(READ_addr, 16);      // Get Addr
            Addr = 0x3e04;
            Result = JLink.ReadMemEx(Addr, NumItems * 4, acData);
            LogLine("Trying to read " + NumItems + " words...");
            if (Result < 0) {
                LogLine("Failed");
                goto Close;
            } else {
                LogLine("O.K.");
                //
                // Output bytes
                //

                for (i = 0; i < 4; i += 1) {
                    // textBox1.Text += String.Format("{0:X8}", acData[i + 3]) + " ";

                    byte[] bytes = BitConverter.GetBytes(acData[i]);
                    //    Array.Reverse(bytes);
                    string result = Encoding.ASCII.GetString(bytes);
                    product.Text += result;
                    //    textBox1.Text += String.Format("{0:X8}", acData[i + 2]) + " ";
                    // textBox1.Text += String.Format("{0:X8}", acData[i+ 1]) + " ";
                    //  textBox1.Text += String.Format("{0:X8}", acData[i + 0]) + "\n";
                }


                /*
                for (i = 0; i < 4; i += 1)
                {
                    byte[] bytes = BitConverter.GetBytes(acData[i]);
                    //string result = Encoding.ASCII.GetString(bytes);
                    string result = Encoding.UTF8.GetString(bytes);
                    short data = (short)(bytes[1] << 8 | bytes[0]);
                    short data2 = (short)(bytes[3] << 8 | bytes[2]);
                    //int decimalValue = Convert.ToInt32(bytes, 16);
                    //textBox1.Text= data.ToString()+data2.ToString();
                }
                */
            }
        //
        // Close connection
        //
        Close:
            JLink.Close();
            LogLine("Disconnected from target and J-Link.");
        }


        private void button3_Click(object sender, EventArgs e)
        {
            JLINKARM_EMU_CONNECT_INFO[] aConnectInfo;
            int r;
            int i;
            int Result;
            byte[] acOut;
            byte[] acIn;
            string sError;
            string sTmp;
            U32[] acData;
            U8[] acTmp;
            U32 NumItems;
            U32 Addr;
            //
            // Init locals
            //
            acOut = new byte[256];
            acIn = new byte[256];
            acData = new U32[24];
            acTmp = new U8[100];
            //
            // Select HIF
            //
            var fw=GetFirmware();
            //
            // Print information about connected probes/ programmers
            //
            //LogLine("Listing information about connected probes/ programmers...");
            aConnectInfo = new JLINKARM_EMU_CONNECT_INFO[16];
            r = JLink.GetEmuList(JLink.HOSTIF_USB | JLink.HOSTIF_IP, aConnectInfo, 16);  // Get information about up to 16 probes/ programmers connected via USB or TCP/IP
            if (r < 0)
            {
                LogLine("Failed to retrieve information about connected probes/ programmers...");
            }
            else
            {
                LogLine("Found " + r + " connected probes/ programmers!");
                r = (r < 16) ? r : 16;   // Do not report about more probes/ programmers than we were able to store
                for (i = 0; i < r; i++)
                {
                    if (aConnectInfo[i].Connection == JLink.HOSTIF_USB)
                    {
                        sTmp = "USB";
                    }
                    else
                    {
                        sTmp = "TCP/IP";
                    }
                    LogLine("#" + (i + 1) + " | S/N = " + aConnectInfo[i].SerialNumber + " | Product = " + aConnectInfo[i].acProduct + " | Connected via " + sTmp);
                }
            }
            LogLine("O.K.");
            //
            // Open connection to J-Link
            //
            LogLine("Connecting to J-Link...");
            sError = JLink.OpenEx(null, pJLINK_LOG_callback_pfErrorOut);
            if (sError != null)
            {
                LogLine(sError);
                return;
            }
            LogLine("O.K.");
            //
            // Setup settings file
            //
            acIn = Array.ConvertAll((Encoding.ASCII.GetBytes("ProjectFile = " + 0)), q => Convert.ToByte(q));
            JLink.ExecCommand(acIn, acOut, 256);
            //
            // Select device
            //
            acIn = Array.ConvertAll((Encoding.ASCII.GetBytes("Device = " + MCU_lab.Text)), q => Convert.ToByte(q));
            JLink.ExecCommand(acIn, acOut, 256);
            //
            // Select TIF (JTAG / SWD / ...) + TIF speed
            //
            // if (CmbTIF.Text == "JTAG") {
            //  JLink.TIF_Select(0);
            //} else {
            JLink.TIF_Select(1);
            // }
            JLink.SetSpeed(Convert.ToInt32(4000));
            //
            // Connect to target
            //
            Result = JLink.Connect();
            LogLine("Connecting to the target...");
            if (Result < 0)
            {
                LogLine("Failed");
                goto Close;
            }
            LogLine("O.K.");
            //
            // Read 16 words from the target
            //

            // string READ_addr=(0x3e04).ToString();


            NumItems = 25;                                  // Number of items to read
                                                            // Addr = Convert.ToUInt32(READ_addr, 16);      // Get Addr
            Addr = 0x3e14;
            Result = JLink.ReadMemEx(Addr, NumItems * 4, acData);
            LogLine("Trying to read " + NumItems + " words...");
            if (Result < 0)
            {
                LogLine("Failed");
                goto Close;
            }
            else
            {
                LogLine("O.K.");
                //
                // Output bytes
                //

                for (i = 0; i < 4; i += 1)
                {
                    byte[] bytes = BitConverter.GetBytes(acData[i]);
                    //string result = Encoding.ASCII.GetString(bytes);
                    string result = Encoding.UTF8.GetString(bytes);
                    short data = (short)(bytes[1] << 8 | bytes[0]);
                    short data2 = (short)(bytes[3] << 8 | bytes[2]);
                    //int decimalValue = Convert.ToInt32(bytes, 16);
                    if (i == 0)
                    {
                        string year = data.ToString();
                        label4.Text = year;
                    }
                    if (i == 1)
                    {
                        label5.Text = data2.ToString();
                        label6.Text = data.ToString();

                    }

                    if (i == 2)
                    {


                        //string fw_models = models[data2];
                       // label9.Text = fw_models;
                    }

                }
                if (fw == FirmwareKind.Old)
                  {
                     var (pwm_12, v1_12) = fan_read.ReadPair(acData[3], Fan_data_read.ControlMode.PWM, Fan_data_read.ControlMode.Voltage);
                      V1_12V.Text = v1_12 + "V";
                     PWM12V_START.Text = pwm_12 + "%";
                  }else if (fw == FirmwareKind.New)
                  {
                    var decimalValue = fan_read.FromHexWord(acData[3]);
                    V1_12V.Text = decimalValue+"V";
                  }

                if (fw == FirmwareKind.Old)
                {
                    var (v1_12_3, v1_12_2) = fan_read.ReadPair(acData[4], Fan_data_read.ControlMode.Voltage, Fan_data_read.ControlMode.Voltage);
                    V2_12V.Text = v1_12_2 + "V";
                    V3_12V.Text = v1_12_3 + "V";
                } else if (fw == FirmwareKind.New)
                {
                    var decimalValue = fan_read.FromHexWord(acData[4]);
                    V2_12V.Text = decimalValue + "V";
                }
                if (fw == FirmwareKind.Old)
                {
                    var (v1_5v, PWM12V_end) = fan_read.ReadPair(acData[5], Fan_data_read.ControlMode.Voltage, Fan_data_read.ControlMode.PWM);
                    V1_5V.Text = v1_5v + "V";
                    PWM12V_END.Text = PWM12V_end + "%";
                }else if (fw == FirmwareKind.New)
                {
                    var decimalValue = fan_read.FromHexWord(acData[5]);
                    V3_12V.Text = decimalValue + "V";

                }

                if (fw == FirmwareKind.Old)
                {
                    var (nul, PWM5V_start) = fan_read.ReadPair(acData[6], Fan_data_read.ControlMode.Voltage, Fan_data_read.ControlMode.PWM);
                    PWM5V_START.Text = PWM5V_start + "%";
                }
                else if (fw == FirmwareKind.New)
                {
                    var (PWM12V_start, PWM12V_end) = fan_read.ReadPair(acData[6], Fan_data_read.ControlMode.Slope, Fan_data_read.ControlMode.Slope);
                    PWM12V_START.Text = PWM12V_start + "%";
                    PWM12V_END.Text = PWM12V_end + "%";

                }

                if (fw == FirmwareKind.Old)
                {
                    var (PWM5V_end, v2_5v) = fan_read.ReadPair(acData[7], Fan_data_read.ControlMode.PWM, Fan_data_read.ControlMode.Voltage);
                    PWM5V_END.Text = PWM5V_end + "%";
                    V2_5V.Text = v2_5v + "V";
                }else if (fw == FirmwareKind.New)
                {
                    var (PWM12V_start, PWM12V_end) = fan_read.ReadPair(acData[7], Fan_data_read.ControlMode.Slope, Fan_data_read.ControlMode.Slope);
                    slope_timer.Text = PWM12V_start + "秒";
                    buffer_time.Text = PWM12V_end + "秒";

                }

                if (fw == FirmwareKind.Old)
                {

                    var (PWM3V_start, v1_3v) = fan_read.ReadPair(acData[8], Fan_data_read.ControlMode.PWM, Fan_data_read.ControlMode.Voltage);
                    PWM3V_START.Text = PWM3V_start + "%";
                    V1_3V.Text = v1_3v + "V";
                } else if (fw == FirmwareKind.New)
                {
                    var decimalValue = fan_read.FromHexWord(acData[8]);
                    V1_5V.Text = decimalValue + "V";

                }

                if (fw == FirmwareKind.Old)
                {
                    var (v2_3v, nul2) = fan_read.ReadPair(acData[9], Fan_data_read.ControlMode.Voltage, Fan_data_read.ControlMode.Voltage);
                    V2_3V.Text = v2_3v + "V";
                } else if (fw == FirmwareKind.New)
                {
                    var decimalValue = fan_read.FromHexWord(acData[9]);
                    V2_5V.Text = decimalValue + "V";

                }

                if (fw == FirmwareKind.Old)
                {
                   var (nul3, PWM3V_end) = fan_read.ReadPair(acData[10], Fan_data_read.ControlMode.Voltage, Fan_data_read.ControlMode.PWM);
                   PWM3V_END.Text = PWM3V_end + "%";
                }
                else if (fw == FirmwareKind.New)
                {
                    var (PWM5V_start, PWM5V_end) = fan_read.ReadPair(acData[10], Fan_data_read.ControlMode.Slope, Fan_data_read.ControlMode.Slope);
                    PWM5V_START.Text = PWM5V_start + "%";
                    PWM5V_END.Text = PWM5V_end + "%";

                }

                if (fw == FirmwareKind.Old)
                {
                    var (V1_otp_LT, V2_otp_LT) = fan_read.ReadPair(acData[11], Fan_data_read.ControlMode.Voltage, Fan_data_read.ControlMode.Voltage);
                    V1_OTP_LT.Text = V1_otp_LT + "V";
                    V2_OTP_LT.Text = V2_otp_LT + "V";
                }
                else if (fw == FirmwareKind.New)
                {
                    var decimalValue = fan_read.FromHexWord(acData[11]);
                    V1_3V.Text = decimalValue + "V";

                }
                if (fw == FirmwareKind.Old)
                {
                    var (V1_otp, V3_otp_LT) = fan_read.ReadPair(acData[12], Fan_data_read.ControlMode.Voltage, Fan_data_read.ControlMode.Voltage);
                    V1_OTP.Text = V1_otp + "V";
                    V3_OTP_LT.Text = V3_otp_LT + "V";
                }
                else if (fw == FirmwareKind.New)
                {
                    var decimalValue = fan_read.FromHexWord(acData[12]);
                    V2_3V.Text = decimalValue + "V";

                }

                if (fw == FirmwareKind.Old)
                {
                    var (V2_otp, V3_otp) = fan_read.ReadPair(acData[13], Fan_data_read.ControlMode.Voltage, Fan_data_read.ControlMode.Voltage);
                    V2_OTP.Text = V2_otp + "V";
                    V3_OTP.Text = V3_otp + "V";
                }else if (fw == FirmwareKind.New)
                {
                    var (PWM3V_start, PWM3V_end) = fan_read.ReadPair(acData[13], Fan_data_read.ControlMode.Slope, Fan_data_read.ControlMode.Slope);
                    PWM3V_START.Text = PWM3V_start + "%";
                    PWM3V_END.Text = PWM3V_end + "%";


                }

                if (fw == FirmwareKind.Old)
                {
                    var (MAX_pwm, OTP_PWM_end) = fan_read.ReadPair(acData[14], Fan_data_read.ControlMode.PWM, Fan_data_read.ControlMode.PWM);
                    end_pwm_12v = (uint)MAX_pwm;
                    MAX_PWM.Text = MAX_pwm + "%";
                    OTP_PWM_END.Text = OTP_PWM_end + "%";
                }else if (fw == FirmwareKind.New)
                {
                    var decimalValue = fan_read.FromHexWord(acData[14]);
                    V1_OTP.Text = decimalValue + "V";

                }
                if (fw == FirmwareKind.Old)
                {
                    var (Slope_time, MIN_pwm) = fan_read.ReadPair(acData[15], Fan_data_read.ControlMode.Slope, Fan_data_read.ControlMode.PWM);
                    start_pwm_12v = (uint)MIN_pwm;
                    double percent = (double)(end_pwm_12v - start_pwm_12v) / 100.0;
                    double buffer_timer_value = percent * Slope_time * 1920 / 1000.0;
                    var temp_buffer_timer = (int)Math.Round(buffer_timer_value, MidpointRounding.AwayFromZero);
                    slope_timer.Text = temp_buffer_timer + "秒";
                    MIN_PWM.Text = MIN_pwm + "%";
                } 
                else if (fw == FirmwareKind.New)
                {
                    var decimalValue = fan_read.FromHexWord(acData[15]);
                    V2_OTP.Text = decimalValue + "V";

                }

                if (fw == FirmwareKind.Old)
                {
                    var (v1, v2) = fan_read.ReadPair(acData[17], Fan_data_read.ControlMode.OPP_Watter, Fan_data_read.ControlMode.OPP_Watter);
                    double StopDelayTime = ((int)v1 << 16) | (int)v2;
                    buffer_time.Text = (StopDelayTime / 1000).ToString() + '秒';
                } else if (fw == FirmwareKind.New)
                {
                    var decimalValue = fan_read.FromHexWord(acData[16]);
                    V3_OTP.Text = decimalValue + "V";


                }
                if (fw == FirmwareKind.Old)
                {
                    var (SR_otp, TA_otp) = fan_read.ReadPair(acData[18], Fan_data_read.ControlMode.Voltage, Fan_data_read.ControlMode.Voltage);
                    SR_OTP.Text = SR_otp + "V";
                    TA_OTP.Text = TA_otp + "V";
                }
                else if (fw == FirmwareKind.New)
                {
                    var (OTP_PWM_start, OTP_PWM_end) = fan_read.ReadPair(acData[17], Fan_data_read.ControlMode.Slope, Fan_data_read.ControlMode.Slope);
                    OTP_PWM_START.Text = OTP_PWM_start + "%";
                    OTP_PWM_END.Text = OTP_PWM_end + "%";
                }
                if (fw == FirmwareKind.Old)
                {
                    var (w1, w2) = fan_read.ReadPair(acData[19], Fan_data_read.ControlMode.OPP_Watter, Fan_data_read.ControlMode.OPP_Watter);
                    double Watter = ((int)w1 << 16) | (int)w2;
                    opp.Text = (Watter).ToString() + 'W';
                }
                else if (fw == FirmwareKind.New)
                {
                    var decimalValue = fan_read.FromHexWord(acData[18]);
                    SR_OTP.Text = decimalValue + "V";

                }

                if (fw == FirmwareKind.Old)
                {
                }
                else if (fw == FirmwareKind.New)
                {
                    var decimalValue = fan_read.FromHexWord(acData[19]);
                    TA_OTP.Text = decimalValue + "V";

                }
                if (fw == FirmwareKind.Old)
                {
                }
                else if (fw == FirmwareKind.New)
                {
                    var (OPP_max, OTP_PWM_end) = fan_read.ReadPair(acData[20], Fan_data_read.ControlMode.Slope, Fan_data_read.ControlMode.Slope);
                   OPP_max = OPP_max/100;
                    opp.Text = OPP_max+ "倍";

                }
            }

        //NumItems=8;                                  // Number of items to read
        // Addr = Convert.ToUInt32(READ_addr, 16);      // Get Addr

        //
        // Close connection
        //
        Close:
            JLink.Close();
            LogLine("Disconnected from target and J-Link.");
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void MCU_lab_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

       static string SwapHexBytes(string hex)
        {
            if (hex.Length % 2 != 0)
            {
                throw new ArgumentException("Hex string length must be even.");
            }

            char[] swapped = new char[hex.Length];

            for (int i = 0; i < hex.Length; i += 2)
            {
                swapped[i] = hex[hex.Length - 2 - i];
                swapped[i + 1] = hex[hex.Length - 1 - i];
            }

            return new string(swapped);
        }

        double Voltage_read(U32 decimalValue, int shift_on_off, ControlMode mode)
        {
            if(shift_on_off==1)
            {
               decimalValue= decimalValue >> 16;
            }
            else
            {
                decimalValue &= 0x0000ffff;
            }
            //string hexStr = SwapHexBytes(line.Substring(startIndex, 4));
            //int decimalValue = Convert.ToInt32(hexStr, 16);

            switch (mode)
            {
                case ControlMode.Voltage:
                    return decimalValue / 1000.0;
                    break;
                case ControlMode.PWM:
                    double result = Math.Round((double)decimalValue / (double)divisor * 100, 1);
                    return result;
                    break;
                case ControlMode.Slope://difficult

                    // return decimalValue / 1000.0;
                    return decimalValue;
                    break;
                case ControlMode.BufferTime:
                    return decimalValue / 1000.0;
                    break;

                case ControlMode.OPP_Watter:
                    return decimalValue;
                    break;

                default:
                    return 0;
                    break;
            }

        }

        //static float FromHexWord(string hexWord)
        //{
        //    if (hexWord.Length != 8)
        //        throw new ArgumentException("Hex word must be exactly 8 characters.");

        //    uint u = Convert.ToUInt32(hexWord, 16);

        //    // 換 endian（MCU 給的是小端，要轉成 IEEE754 順序）
        //    u = BinaryPrimitives.ReverseEndianness(u);

        //    // 使用 BitConverter 轉成 float（所有 .NET 都支援）
        //    byte[] bytes = BitConverter.GetBytes(u);

        //    return BitConverter.ToSingle(bytes, 0);
        //}

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void PWM5V_START_TextChanged(object sender, EventArgs e)
        {

        }

        private void label22_Click(object sender, EventArgs e)
        {

        }

        private void NEW_FONT_CheckedChanged(object sender, EventArgs e)
        {

        }
    }



}