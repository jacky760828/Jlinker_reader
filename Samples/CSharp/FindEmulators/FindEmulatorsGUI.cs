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
using static JLink_Find_Emulators.FanDataParser; // signed long long




namespace JLink_Find_Emulators
{
    public partial class FindEmulatorsGUI : Form
    {

        // private Fan_data_read fan_read = new Fan_data_read();

        byte[] acOut;
        int divisor = 1920;
        byte[] acIn;
        string[] models = { "650W", "750W", "850W", "1000W", "550W" };
        public enum FirmwareKind { Old, New, Unknown }
        public enum HEX_FirmwareKind { Old, New, Unknown }
        uint start_pwm_12v;
        uint end_pwm_12v;
        public byte[] dsp_file_content = null;



        #region HEX_READ

        //public enum FirmwareKind
        //{
        //    Old, 
        //    New,
        //    Unknown
        //}
        BindingList<FanParamRow> _rows = new BindingList<FanParamRow>();
        public enum HEX_ControlMode
        {
            None,
            Voltage,
            PWM,
            Slope,
            BufferTime,
            OPP_Watter,
            OPP_NEW
        }
        public const double MCU_FW_Model_Type_Flash = 0x3E1C;
        public const double M12V_StartDuty_StartLoad = 0X3e20;//defile
        public const double Def_Fan_V12I_V1 = M12V_StartDuty_StartLoad;

        public const double M12V_EndLoad_SlopeLoad = 0X3e24;

        public const double M5V_StartLoad_M12V_EndDuty = 0X3e28;
        public const double M5V_SlopeLoad_StartDuty = 0X3e2c;
        public const double M5V_EndDuty_EndLoad = 0X3e30;
        public const double M3V_StartDuty_StartLoad = 0X3e34;
        public const double M3V_EndLoad_SlopeLoad = 0X3e38;
        public const double M3V_EndDuty = 0X3e3c;
        public const double LowOTP_60Load_30Load = 0X3e40;
        public const double OTP_StartLevel_LowOTP_MaxLoad = 0X3e44;
        public const double MiddleLevel_OTP_EndLevel = 0X3e48;
        public const double MaxDuty_OTP_MiddleDuty = 0X3e4c;
        public const double FanDutySetTime_StopDuty = 0X3e50;
        public const double FanDutySetTimeFall_PFCMOSGain = 0X3e54;
        public const double TempHysteresis_StopDelayTime = 0X3e58;
        public const double SROTP_TaOTP_Level = 0X3e5c;
        public const double OPProtectLevel = 0X3e60;
        public const double IFlash_OPP_Factor = 0X3e64;



        float Float_12V_V3;
        int hexValue = 0x4E0; // 十六進位 0x4E0 = 十進位 1248
        int divisor_HEX = 1920;
        int hexValue2 = 0x258;//600
        int hexValue3 = 0x493e0;//600
        uint start_pwm_12v_HEX;
        uint end_pwm_12v_HEX;
        string byteStr;

        class Product
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string watt { get; set; }
            public string item { get; set; }
        };
        List<Product> products = new List<Product>
        {
            new Product { Id = 10, Name = "FocusV4", watt = "750" ,item="01"},
            new Product { Id = 10, Name = "FocusV4", watt = "850", item="02"},
            new Product { Id = 10, Name = "FocusV4", watt = "1000",item="03"},
            new Product { Id = 10, Name = "FocusV4", watt = "1200",item="04"},
            ////////////////////////////////////////////////////////////////
            new Product { Id = 14, Name = "Core V2", watt = "650" ,item="01"},
            new Product { Id = 14, Name = "Core V2", watt = "750",item="02"},
            new Product { Id = 14, Name = "Core V2", watt = "850",item="03"},
            new Product { Id = 14, Name = "Core V2", watt = "1000",item="04"},
                   ////////////////////////////////////////////////////////////////
            new Product { Id = 20, Name = "Focus V5", watt = "750",item="01" },
            new Product { Id = 20, Name = "Focus V5",watt = "850",item="02"},
            new Product { Id = 20, Name = "Focus V5", watt = "1000",item="03" },
                ////////////////////////////////////////////////////////////////
            new Product { Id = 23, Name = "Focus V4 Fractal Design Evie", watt= "750",item="01"},
            new Product { Id = 23, Name = "Focus V4 Fractal Design Evie", watt = "850" ,item="02"},
            new Product { Id = 23, Name = "Focus V4 Fractal Design Evie", watt = "1000" ,item="03"},
            //////////////////////////////////////////////////////////////////////////////////////
            new Product { Id = 21, Name = "Core V2 VK", watt= "650",item="01"},
            new Product { Id = 21, Name = "Core V2 VK", watt = "750" ,item="02"},
            new Product { Id = 21, Name = "Core V2 VK", watt = "850" ,item="03"},
            new Product { Id = 21, Name = "Core V2 VK", watt = "1000" ,item="03"},
               //////////////////////////////////////////////////////////////////////////////////////
            new Product { Id = 5, Name = "TX1600_nVidia", watt = "1600" ,item="04"},
        };

       
        public class FanParamRow
        {
            public string Group { get; set; }      // 12V / 5V / OTP ...
            public string Name { get; set; }       // Start Voltage / End Duty
            public string Value1 { get; set; }    // 主要值
            public string Firmware { get; set; }   // Old / New
            public string Address { get; set; }    // 0x3E20
        }
        private float FromHexWord(string hex)
        {
            uint raw = Convert.ToUInt32(hex, 16);
            byte[] bytes = BitConverter.GetBytes(raw);
            return BitConverter.ToSingle(bytes, 0);
        }

        static float FromHexfileWord(string hexWord)
        {
            uint raw = Convert.ToUInt32(hexWord, 16);
            byte[] bytes = BitConverter.GetBytes(raw);
            Array.Reverse(bytes);           // 手動反轉 Endian
            return BitConverter.ToSingle(bytes, 0);
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
        double Voltage_read(string line, int startIndex, ControlMode mode)
        {


            string hexStr = SwapHexBytes(line.Substring(startIndex, 4));
            int decimalValue = Convert.ToInt32(hexStr, 16);

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
         string Format(double value, ControlMode mode)
        {
            switch (mode)
            {
                case ControlMode.Voltage:
                    return value.ToString("0.###") + " V";

                case ControlMode.PWM:
                    return value.ToString("0.#") + " %";

                case ControlMode.BufferTime:
                    return value.ToString("0.#") + " s";

                case ControlMode.OPP_Watter:
                    return value.ToString();
                case ControlMode.OPP_NEW:
                    return value.ToString("0.##") + "倍";

                default:
                    return "-";
            }
        }

        #endregion



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
        public HEX_FirmwareKind GetHexFirmware()
        {
            if (HEX_OLD.Checked) return HEX_FirmwareKind.Old;
            if (HEX_NEW.Checked) return HEX_FirmwareKind.New;
            return HEX_FirmwareKind.Unknown;
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

            Hex_read.AutoGenerateColumns = false;
            Hex_read.Columns.Clear();
            Hex_read.DataSource = _rows;

            Hex_read.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "群組",
                DataPropertyName = "Group"
            });
            Hex_read.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "項目",
                DataPropertyName = "Name",
                Width = 180
            });
            Hex_read.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "值1",
                DataPropertyName = "Value1"
            });

            Hex_read.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "FW",
                DataPropertyName = "Firmware"
            });
            Hex_read.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Address",
                DataPropertyName = "Address"
            });






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
                }
            }
        //
        // Close connection
        //
        Close:
            JLink.Close();
            LogLine("Disconnected from target and J-Link.");
        }
        //private DataTable BuildParamTable()
        //{
        //    DataTable table = new DataTable();
        //    table.Columns.Add("Index", typeof(int));
        //    table.Columns.Add("描述", typeof(string));
        //    table.Columns.Add("電壓", typeof(string));
        //    table.Columns.Add("PWM", typeof(string));
        //    table.Columns.Add("時間", typeof(string));
        //    table.Columns.Add("功率", typeof(string));
        //    return table;
        //}

        private string FormatDisplay(double value, FanDataParser.ControlMode mode)
        {
            switch (mode)
            {
                case FanDataParser.ControlMode.Voltage:
                    return value.ToString("0.###") + " V";

                case FanDataParser.ControlMode.PWM:
                    return value.ToString("0.#") + " %";

                case FanDataParser.ControlMode.BufferTime:
                    return value.ToString("0.#") + " 秒";

                case FanDataParser.ControlMode.OPP_Watter:
                    return value.ToString()+" W";
                case FanDataParser.ControlMode.Slope:
                    return value.ToString("0.#") + " 秒";
                case ControlMode.OPP_NEW:
                    return value.ToString("0.##") + "倍";
                default:
                    return "-";
            }
        }

 #region 讀取機台參數

        private void ShowParametersInGrid(FanDataParser parser)
        {
            DataTable table = new DataTable();
            //table.Columns.Add("Index", typeof(int));
            table.Columns.Add("Description", typeof(string));
            table.Columns.Add("電壓", typeof(string));
            table.Columns.Add("PWM", typeof(string));
            table.Columns.Add("秒", typeof(string));
            table.Columns.Add("輸出功率", typeof(string));
            var fw = GetFirmware();

               FillFanTable(table, parser, fw);

            // 顯示到你的 DataGridView
            fandataGridView.DataSource = table;
        }

        private void FillNewFirmwareRows(DataTable table, FanDataParser parser)
        {

            Add_NEW_FONT_FanRow(table, parser, 3, FanData.DESC_12V_FIRST, ControlMode.Voltage, ControlMode.PWM, ControlMode.None, ControlMode.None);
            Add_NEW_FONT_FanRow(table, parser, 4, FanData.DESC_12V_TWO, ControlMode.Voltage, ControlMode.PWM, ControlMode.None, ControlMode.None);
            Add_NEW_FONT_FanRow(table, parser, 5, FanData.DESC_12V_THREE, ControlMode.Voltage, ControlMode.PWM, ControlMode.None, ControlMode.None);
            Add_NEW_FONT_FanRow(table, parser, 6, FanData.DESC_12V_START_DUTY, ControlMode.Voltage, ControlMode.PWM, ControlMode.None, ControlMode.None);
            Add_NEW_FONT_FanRow(table, parser, 6, FanData.DESC_12V_END_DUTY, ControlMode.Voltage, ControlMode.PWM, ControlMode.None, ControlMode.None);
            Add_NEW_FONT_FanRow(table, parser, 7, FanData.FAN_SETTING_TIME, ControlMode.Voltage, ControlMode.PWM, ControlMode.Slope, ControlMode.None);
            Add_NEW_FONT_FanRow(table, parser, 7, FanData.FAN_BUFFER_TIME, ControlMode.Voltage, ControlMode.PWM, ControlMode.Slope, ControlMode.None);
            Add_NEW_FONT_FanRow(table, parser, 8, FanData.DESC_5V_FIRST, ControlMode.Voltage, ControlMode.PWM, ControlMode.None, ControlMode.None);
            Add_NEW_FONT_FanRow(table, parser, 9, FanData.DESC_5V_TWO, ControlMode.Voltage, ControlMode.PWM, ControlMode.None, ControlMode.None);
            Add_NEW_FONT_FanRow(table, parser, 10, FanData.DESC_5V_START_DUTY, ControlMode.Voltage, ControlMode.PWM, ControlMode.None, ControlMode.None);
            Add_NEW_FONT_FanRow(table, parser, 10, FanData.DESC_5V_END_DUTY, ControlMode.Voltage, ControlMode.PWM, ControlMode.None, ControlMode.None);
            Add_NEW_FONT_FanRow(table, parser, 11, FanData.DESC_3V_FIRST, ControlMode.Voltage, ControlMode.PWM, ControlMode.None, ControlMode.None);
            Add_NEW_FONT_FanRow(table, parser, 12, FanData.DESC_3V_TWO, ControlMode.Voltage, ControlMode.PWM, ControlMode.None, ControlMode.None);
            Add_NEW_FONT_FanRow(table, parser, 13, FanData.DESC_3V_START_DUTY, ControlMode.Voltage, ControlMode.PWM, ControlMode.None, ControlMode.None);
            Add_NEW_FONT_FanRow(table, parser, 13, FanData.DESC_3V_END_DUTY, ControlMode.Voltage, ControlMode.PWM, ControlMode.None, ControlMode.None);
            Add_NEW_FONT_FanRow(table, parser, 14, FanData.OTP_FIRST, ControlMode.Voltage, ControlMode.PWM, ControlMode.None, ControlMode.None);
            Add_NEW_FONT_FanRow(table, parser, 15, FanData.OTP_TWO, ControlMode.Voltage, ControlMode.PWM, ControlMode.None, ControlMode.None);
            Add_NEW_FONT_FanRow(table, parser, 16, FanData.OTP_THREE, ControlMode.Voltage, ControlMode.PWM, ControlMode.None, ControlMode.None);
            Add_NEW_FONT_FanRow(table, parser, 17, FanData.FAN_START_DUTY, ControlMode.Voltage, ControlMode.PWM, ControlMode.None, ControlMode.None);
            Add_NEW_FONT_FanRow(table, parser, 17, FanData.FAN_END_DUTY, ControlMode.Voltage, ControlMode.PWM, ControlMode.None, ControlMode.None);
            Add_NEW_FONT_FanRow(table, parser, 18, FanData.SR_OTP, ControlMode.Voltage, ControlMode.PWM, ControlMode.None, ControlMode.None);
            Add_NEW_FONT_FanRow(table, parser, 19, FanData.TA_OTP, ControlMode.Voltage, ControlMode.PWM, ControlMode.None, ControlMode.None);
            Add_NEW_FONT_FanRow(table, parser, 20, FanData.OPP, ControlMode.Voltage, ControlMode.PWM, ControlMode.None, ControlMode.OPP_NEW);

        }

        private void FillOldFirmwareRows(DataTable table, FanDataParser parser)
        {

            //AddFanRow(table, parser, 3, "12V負載第一點/12V起轉PWM", ControlMode.Voltage, ControlMode.PWM, ControlMode.None, ControlMode.None);
            AddFanRow(table, parser, 3, FanData.DESC_12V_FIRST, ControlMode.Voltage, ControlMode.PWM, ControlMode.None, ControlMode.None);
            AddFanRow(table, parser, 3, FanData.DESC_12V_START_DUTY, ControlMode.Voltage, ControlMode.PWM, ControlMode.None, ControlMode.None);
            AddFanRow(table, parser, 4, FanData.DESC_12V_TWO, ControlMode.Voltage, ControlMode.PWM, ControlMode.None, ControlMode.None);
            AddFanRow(table, parser, 4, FanData.DESC_12V_THREE, ControlMode.Voltage, ControlMode.PWM, ControlMode.None, ControlMode.None);
            AddFanRow(table, parser, 5, FanData.DESC_12V_END_DUTY, ControlMode.Voltage, ControlMode.PWM, ControlMode.None, ControlMode.None);
            // AddFanRow(table, parser, 5, "5V負載第一點/12V結束PWM", ControlMode.Voltage, ControlMode.PWM, ControlMode.None, ControlMode.None);
            AddFanRow(table, parser, 5, FanData.DESC_5V_FIRST, ControlMode.Voltage, ControlMode.PWM, ControlMode.None, ControlMode.None);
            AddFanRow(table, parser, 6, FanData.DESC_5V_START_DUTY, ControlMode.Voltage, ControlMode.PWM, ControlMode.None, ControlMode.None);
            AddFanRow(table, parser, 7, FanData.DESC_5V_TWO, ControlMode.Voltage, ControlMode.PWM, ControlMode.Slope, ControlMode.None);
            AddFanRow(table, parser, 7, FanData.DESC_5V_END_DUTY, ControlMode.Voltage, ControlMode.PWM, ControlMode.Slope, ControlMode.None);
            // AddFanRow(table, parser, 8, "3V負載第一點/3V起轉PWM", ControlMode.Voltage, ControlMode.PWM, ControlMode.None, ControlMode.None);
            AddFanRow(table, parser, 8, FanData.DESC_3V_FIRST, ControlMode.Voltage, ControlMode.PWM, ControlMode.None, ControlMode.None);
            AddFanRow(table, parser, 8, FanData.DESC_3V_START_DUTY, ControlMode.Voltage, ControlMode.PWM, ControlMode.None, ControlMode.None);
            AddFanRow(table, parser, 9, FanData.DESC_3V_TWO, ControlMode.Voltage, ControlMode.PWM, ControlMode.None, ControlMode.None);
            AddFanRow(table, parser, 10, FanData.DESC_3V_END_DUTY, ControlMode.Voltage, ControlMode.PWM, ControlMode.None, ControlMode.None);

            AddFanRow(table, parser, 11, FanData.LT_1 , ControlMode.Voltage, ControlMode.PWM, ControlMode.None, ControlMode.None);
            AddFanRow(table, parser, 11, FanData.LT_2, ControlMode.Voltage, ControlMode.PWM, ControlMode.None, ControlMode.None);
            AddFanRow(table, parser, 12, FanData.LT_3, ControlMode.Voltage, ControlMode.PWM, ControlMode.None, ControlMode.None);

            AddFanRow(table, parser, 12, FanData.OTP_FIRST, ControlMode.Voltage, ControlMode.PWM, ControlMode.None, ControlMode.None);
            AddFanRow(table, parser, 13, FanData.OTP_TWO, ControlMode.Voltage, ControlMode.PWM, ControlMode.None, ControlMode.None);
            AddFanRow(table, parser, 13, FanData.OTP_THREE, ControlMode.Voltage, ControlMode.PWM, ControlMode.None, ControlMode.None);

            AddFanRow(table, parser, 14, FanData.MAX_PWM, ControlMode.Voltage, ControlMode.PWM, ControlMode.None, ControlMode.None);
            AddFanRow(table, parser, 15, FanData.MIN_PWM, ControlMode.Voltage, ControlMode.PWM, ControlMode.None, ControlMode.None);
            AddFanRow(table, parser, 15, FanData.FAN_SETTING_TIME, ControlMode.Voltage, ControlMode.PWM, ControlMode.BufferTime, ControlMode.None);
            AddFanRow(table, parser, 17, FanData.FAN_BUFFER_TIME, ControlMode.Voltage, ControlMode.PWM, ControlMode.BufferTime, ControlMode.None);
            AddFanRow(table, parser, 18, FanData.SR_OTP, ControlMode.Voltage, ControlMode.PWM, ControlMode.None, ControlMode.None);
            AddFanRow(table, parser, 18, FanData.TA_OTP, ControlMode.Voltage, ControlMode.PWM, ControlMode.None, ControlMode.None);
            AddFanRow(table, parser, 19, FanData.OPP, ControlMode.Voltage, ControlMode.PWM, ControlMode.None, ControlMode.OPP_Watter);

        }



        private void FillFanTable( DataTable table,FanDataParser parser,FirmwareKind fw)
        {
            if (fw == FirmwareKind.New)
            {
                FillNewFirmwareRows(table, parser);
            }
            else
            {
                FillOldFirmwareRows(table, parser);
            }
        }



private void AddFanRow(
DataTable table,
FanDataParser parser,
int index,
string desc,
ControlMode voltageMode,
ControlMode pwmMode,
ControlMode timeMode,
ControlMode wattMode)
        {
            uint raw = parser.RawData[index];

            ushort high = (ushort)(raw >> 16);
            ushort low = (ushort)(raw & 0xFFFF);
            switch (index)
            {

                case 3:
                    if (desc == FanData.DESC_12V_FIRST)
                    {
                        string voltage = FormatDisplay(parser.Decode(low, voltageMode), voltageMode);

                        table.Rows.Add(desc, voltage, "NA", "NA", "NA");
                    }
                    else
                    {
                        string pwm = FormatDisplay(parser.Decode(high, pwmMode), pwmMode);
                        table.Rows.Add(desc, "NA", pwm, "NA", "NA");

                    }
                    break;
                case 4:
                    {
                        if (desc == FanData.DESC_12V_TWO)
                        {
                            string V12_2 = FormatDisplay(parser.Decode(low, voltageMode), voltageMode);
                            table.Rows.Add(desc, V12_2, "NA", "NA", "NA");
                        }
                        else
                        {
                            string V12_3 = FormatDisplay(parser.Decode(high, voltageMode), voltageMode);
                            table.Rows.Add(desc, V12_3, "NA", "NA", "NA");
                        }
                    }
                    break;
                case 5:

                    if (desc == FanData.DESC_12V_END_DUTY)
                    {
                        string PWM_12V_end = FormatDisplay(parser.Decode(low, pwmMode), pwmMode);
                        table.Rows.Add(desc, "NA", PWM_12V_end, "NA", "NA");
                    }
                    else
                    {
                        string V5_1 = FormatDisplay(parser.Decode(high, voltageMode), voltageMode);
                        table.Rows.Add(desc, V5_1, "NA", "NA", "NA");
                    }
                    break;
                case 6:
                    string PWM_5V_start = FormatDisplay(parser.Decode(low, pwmMode), pwmMode);
                    table.Rows.Add(desc, 0, PWM_5V_start, "NA", "NA");
                    break;
                case 7:

                    string PWM_5V_end = FormatDisplay(parser.Decode(high, pwmMode), pwmMode);
                    string V5_2 = FormatDisplay(parser.Decode(low, voltageMode), voltageMode);
                    table.Rows.Add(desc, V5_2, PWM_5V_end, "NA", "NA");
                    break;

                case 8:

                    if (desc == FanData.DESC_3V_START_DUTY)
                    {
                        string PWM_3V_start = FormatDisplay(parser.Decode(high, pwmMode), pwmMode);
                       
                        table.Rows.Add(desc, "NA", PWM_3V_start, "NA", "NA");
                    }
                    else
                    {    
                        string V3_1 = FormatDisplay(parser.Decode(low, voltageMode), voltageMode);
                        table.Rows.Add(desc, V3_1, "NA", "NA", "NA");

                    }


                    break;

                case 9:
                    string V3_2 = FormatDisplay(parser.Decode(high, voltageMode), voltageMode);
                    table.Rows.Add(desc, V3_2, "NA", "NA", "NA");
                    break;
                case 10:
                    string PWM_3V_end = FormatDisplay(parser.Decode(low, pwmMode), pwmMode);
                    table.Rows.Add(desc, "NA", PWM_3V_end, "NA", "NA");
                    break;
                case 11:
                    if (desc == FanData.LT_2)
                    {
                        string LT_2 = FormatDisplay(parser.Decode(low, voltageMode), voltageMode);
                        table.Rows.Add(desc, LT_2, "NA", "NA", "NA");
                    }
                    else
                    {
                        string LT_1 = FormatDisplay(parser.Decode(high, voltageMode), voltageMode);
                        table.Rows.Add(desc, LT_1, "NA", "NA", "NA");
                    }

                    break;
                case 12:
                    if (desc == FanData.LT_3)
                    {
                        string LT_3 = FormatDisplay(parser.Decode(low, voltageMode), voltageMode);
                        table.Rows.Add(desc, LT_3, "NA", "NA", "NA");
                    }
                    else
                    {
                        string OTP_1 = FormatDisplay(parser.Decode(high, voltageMode), voltageMode);
                        table.Rows.Add(desc, OTP_1, "NA", "NA", "NA");
                    }

                    break;
                case 13:
                    if (desc == FanData.OTP_TWO)
                    {
                        string OTP_2 = FormatDisplay(parser.Decode(high, voltageMode), voltageMode);
                        table.Rows.Add(desc, OTP_2, "NA", "NA", "NA");
                    }
                    else
                    {
                        string OTP_3 = FormatDisplay(parser.Decode(low, voltageMode), voltageMode);
                        table.Rows.Add(desc, OTP_3, "NA", "NA", "NA");
                    }

                    break;
                case 14:
                    if (desc == FanData.MAX_PWM)
                    {
                        end_pwm_12v = (uint)parser.Decode(high, pwmMode);
                        string MAX_PWM = FormatDisplay(parser.Decode(high, pwmMode), pwmMode);
                        table.Rows.Add(desc, "NA", MAX_PWM, "NA", "NA");
                    }
                    else
                    {
                        string OTP_PWM = FormatDisplay(parser.Decode(low, pwmMode), pwmMode);
                        table.Rows.Add(desc, "NA", OTP_PWM, "NA", "NA");
                    }

                    break;
                case 15:
                    if (desc == FanData.MIN_PWM)
                    {
                        start_pwm_12v = (uint)parser.Decode(low, pwmMode);
                        string MIN_PWM = FormatDisplay(parser.Decode(low, pwmMode), pwmMode);
                        table.Rows.Add(desc, "NA", MIN_PWM, "NA", "NA");


                    }
                    else
                    {
                        var Slope_time = (uint)parser.Decode(high, ControlMode.Slope);
                        double percent = (double)(end_pwm_12v - start_pwm_12v) / 100.0;
                        double buffer_timer_value = percent * Slope_time * 1920 / 1000.0;
                        var temp_buffer_timer = (int)Math.Round(buffer_timer_value, MidpointRounding.AwayFromZero);
                        string FAN_TIME = FormatDisplay(temp_buffer_timer, ControlMode.Slope);
                        table.Rows.Add(desc, "NA", "NA", FAN_TIME, "NA");
                    }
                    break;

                case 17:
                    string buf_time = FormatDisplay(parser.Decode(low, ControlMode.BufferTime), ControlMode.BufferTime);
                    table.Rows.Add(desc, "NA", "NA", buf_time, "NA");
                    break;
                case 18:
                    if (desc == FanData.SR_OTP)
                    {
                        string SR_OTP = FormatDisplay(parser.Decode(high, voltageMode), voltageMode);
                        table.Rows.Add(desc, SR_OTP, "NA", "NA", "NA");
                    }
                    else
                    {
                        string OTP_PWM = FormatDisplay(parser.Decode(low, voltageMode), voltageMode);
                        table.Rows.Add(desc, OTP_PWM, "NA", "NA", "NA");
                    }

                    break;
                case 19:
                    string OPP = FormatDisplay(parser.Decode(low, ControlMode.OPP_Watter), ControlMode.OPP_Watter);
                    table.Rows.Add(desc, "NA", "NA", "NA", OPP);
                    break;
            }

            // table.Rows.Add(index, description, voltage, pwm, time, watt);

        }

#endregion

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

                var parser = new FanDataParser(acData);
                ShowParametersInGrid(parser);
               
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


    private void Add_NEW_FONT_FanRow(
    DataTable table,
    FanDataParser parser,
    int index,
    string desc,
    ControlMode voltageMode,
    ControlMode pwmMode,
    ControlMode timeMode,
    ControlMode wattMode)
        {
            uint raw = parser.RawData[index];

            ushort high = (ushort)(raw >> 16);
            ushort low = (ushort)(raw & 0xFFFF);
            switch (index)
            {
                case 3:
                    var v1_12 = FormatDisplay(parser.FromHexWord(raw), voltageMode);
                    table.Rows.Add(desc, v1_12, "NA", "NA", "NA");
                    // V1_12V.Text = decimalValue + "V";
                    break;

                case 4:
                    var v2_12 = FormatDisplay(parser.FromHexWord(raw), voltageMode);
                    table.Rows.Add( desc, v2_12, "NA", "NA", "NA");
                    // V1_12V.Text = decimalValue + "V";
                    break;
                case 5:
                    var v3_12 = FormatDisplay(parser.FromHexWord(raw), voltageMode);
                    table.Rows.Add( desc, v3_12, "NA", "NA", "NA");
                    // V1_12V.Text = decimalValue + "V";
                    break;
                 case 6:
                    {
                        if (desc == FanData.DESC_12V_END_DUTY)
                        {
                            string V12_end_duty = FormatDisplay(parser.Decode(low, ControlMode.NEW_F_PWM), pwmMode);
                            table.Rows.Add( desc, "NA",V12_end_duty, "NA", "NA");
                        }
                        else
                        {
                            string V12_start_duty = FormatDisplay(parser.Decode(high, ControlMode.NEW_F_PWM),pwmMode);
                            table.Rows.Add(desc, "NA",V12_start_duty, "NA", "NA");
                        }

                    }
                    break;
                case 7:
                    {
                        if (desc == FanData.FAN_SETTING_TIME)
                        {
                            string V12_set_time = FormatDisplay(parser.Decode(high, ControlMode.Slope), ControlMode.Slope);
                            table.Rows.Add( desc, "NA","NA", V12_set_time, "NA");
                        }
                        else
                        {
                            string V12_buffer_time = FormatDisplay(parser.Decode(low, ControlMode.Slope), ControlMode.Slope);
                            table.Rows.Add( desc, "NA", "NA", V12_buffer_time, "NA");
                        }

                    }
                    break;

                case 8:
                    var v1_5 = FormatDisplay(parser.FromHexWord(raw), voltageMode);
                    table.Rows.Add(desc, v1_5, "NA", "NA", "NA");
                    // V1_12V.Text = decimalValue + "V";
                    break;

                case 9:
                    var v2_5 = FormatDisplay(parser.FromHexWord(raw), voltageMode);
                    table.Rows.Add(desc, v2_5, "NA", "NA", "NA");
                    // V1_12V.Text = decimalValue + "V";
                    break;
                case 10:
                    {
                        if (desc == FanData.DESC_5V_END_DUTY)
                        {
                            string V5_end_duty = FormatDisplay(parser.Decode(low, ControlMode.NEW_F_PWM), pwmMode);
                            table.Rows.Add( desc, "NA", V5_end_duty, "NA", "NA");
                        }
                        else
                        {
                            string V5_start_duty = FormatDisplay(parser.Decode(high, ControlMode.NEW_F_PWM), pwmMode);
                            table.Rows.Add( desc, "NA", V5_start_duty, "NA", "NA");
                        }

                    }
                    break;
                case 11:
                    var v1_3 = FormatDisplay(parser.FromHexWord(raw), voltageMode);
                    table.Rows.Add( desc, v1_3, "NA", "NA", "NA");
                    // V1_12V.Text = decimalValue + "V";
                    break;

                case 12:
                    var v2_3 = FormatDisplay(parser.FromHexWord(raw), voltageMode);
                    table.Rows.Add( desc, v2_3, "NA", "NA", "NA");
                    // V1_12V.Text = decimalValue + "V";
                    break;
                case 13:
                    {
                        if (desc == FanData.DESC_3V_END_DUTY)
                        {
                            string V3_end_duty = FormatDisplay(parser.Decode(low, ControlMode.NEW_F_PWM), pwmMode);
                            table.Rows.Add(desc, "NA", V3_end_duty, "NA", "NA");
                        }
                        else
                        {
                            string V3_start_duty = FormatDisplay(parser.Decode(high, ControlMode.NEW_F_PWM), pwmMode);
                            table.Rows.Add( desc, "NA", V3_start_duty, "NA", "NA");
                        }

                    }
                    break;
                case 14:
                    var v1_OTP = FormatDisplay(parser.FromHexWord(raw), voltageMode);
                    table.Rows.Add(desc, v1_OTP, "NA", "NA", "NA");
                    // V1_12V.Text = decimalValue + "V";
                    break;

                case 15:
                    var v2_OTP = FormatDisplay(parser.FromHexWord(raw), voltageMode);
                    table.Rows.Add( desc, v2_OTP, "NA", "NA", "NA");
                    // V1_12V.Text = decimalValue + "V";
                    break;
                case 16:
                    var v3_OTP = FormatDisplay(parser.FromHexWord(raw), voltageMode);
                    table.Rows.Add( desc, v3_OTP, "NA", "NA", "NA");
                    // V1_12V.Text = decimalValue + "V";
                    break;
                case 17:
                    if (desc == FanData.FAN_END_DUTY)
                    {
                        string OTP_end_duty = FormatDisplay(parser.Decode(low, ControlMode.NEW_F_PWM), pwmMode);
                        table.Rows.Add( desc, "NA", OTP_end_duty, "NA", "NA");
                    }
                    else
                    {
                        string OTP_start_duty = FormatDisplay(parser.Decode(high, ControlMode.NEW_F_PWM), pwmMode);
                        table.Rows.Add( desc, "NA", OTP_start_duty, "NA", "NA");
                    }
                    // V1_12V.Text = decimalValue + "V";
                    break;
                case 18:
                    var MOS_OTP = FormatDisplay(parser.FromHexWord(raw), voltageMode);
                    table.Rows.Add( desc, MOS_OTP, "NA", "NA", "NA");
                    // V1_12V.Text = decimalValue + "V";
                    break;
                case 19:
                    var TA_OTP = FormatDisplay(parser.FromHexWord(raw), voltageMode);
                    table.Rows.Add(desc, TA_OTP, "NA", "NA", "NA");
                    // V1_12V.Text = decimalValue + "V";
                    break;
                case 20:
                    var OPP = FormatDisplay(parser.Decode(high, ControlMode.OPP_NEW), ControlMode.OPP_NEW);
                    table.Rows.Add( desc, "NA", "NA", "NA", OPP);
                    // V1_12V.Text = decimalValue + "V";
                    break;
            }

            // table.Rows.Add(index, description, voltage, pwm, time, watt);

        }





        
        
        
        private void file_read_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "HEX Files (*.hex)|*.hex|All Files (*.*)|*.*",
                Title = "選擇 HEX 文件"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtFilePath.Text = openFileDialog.FileName;
            }
        }
        static string HexToAscii(string hex)
        {
            // 每 2 個十六進制字符代表 1 個 byte
            byte[] bytes = new byte[hex.Length / 2];

            for (int i = 0; i < bytes.Length; i++)
            {
                // 取兩個字元轉換成 byte
                bytes[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
            }

            // 轉換成 ASCII 字符串
            return Encoding.ASCII.GetString(bytes);
        }
        private void button5_Click(object sender, EventArgs e)
        {
            string filePath = txtFilePath.Text;
            if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
            {
                MessageBox.Show("請選擇一個有效的 HEX 文件！", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                List<byte> binaryData = new List<byte>();
                // listBoxHexData.Items.Clear();

                _rows.Clear();   // 清空表格
                var fw = GetHexFirmware();

                using (StreamReader sr = new StreamReader(filePath))
                {
                    string line;

                    while ((line = sr.ReadLine()) != null)
                    {
                        if (!line.StartsWith(":")) continue; // 確保是 HEX 格式行

                        // listBoxHexData.Items.Add(line);

                        int byteCount = Convert.ToInt32(line.Substring(1, 2), 16);//開頭
                        int address = Convert.ToInt32(line.Substring(3, 4), 16);//3取4個byte
                        int recordType = Convert.ToInt32(line.Substring(7, 2), 16);//7取兩個byte

                        if (recordType == 0) // 00 = Data Record
                        {

                            //  var fw = GetFirmware();

                            if (address == 0x3e04)
                            {
                                // for (int i = 0; i < byteCount; i++)
                                //  {
                                //  byteStr = line.Substring(9 + (i * 2), 24);
                                byteStr = line.Substring(9, 26);
                                // byte dataByte = Convert.ToByte(byteStr, 16);
                                //  binaryData.Add(dataByte);
                                // }
                                //listBoxHexData.Items.Add(line); // 顯示 HEX 文件的每一行

                                string asciiString = HexToAscii(byteStr);
                                //  asciiString.Substring(4, 4)
                                string Model = asciiString.Substring(6, 2);
                                int _Model = int.Parse(Model);
                                string power = asciiString.Substring(11, 2);
                                int _power = int.Parse(power);
                                var productName = products.FirstOrDefault(p => p.Id == _Model)?.Name;



                                var result = products.FirstOrDefault(p => p.Name == productName && p.item == power);
                                //  label9.Text=$"結果：{result.watt}W";
                                //// 檢查是否找到
                                if (result != null)
                                {
                                    label9.Text = $"瓦數：{result.watt}W";
                                }
                                else
                                {
                                    label9.Text = "未找到符合條件的產品";
                                }

                                label8.Text = productName;
                               // fw_ver.Text = asciiString;


                            }
                            if (address == 0X3e14)
                            {

                                byteStr = line.Substring(9, 4);
                                byteStr = SwapHexBytes(byteStr);
                                int decimalValue = Convert.ToInt32(byteStr, 16); // 轉換為10進制
                                string Year = decimalValue.ToString(); // 轉換為字串
                                                                       // string asciiString = result;
                                year.Text = Year;


                            }
                            if (address == 0X3e18)
                            {

                                byteStr = line.Substring(9, 4);

                                byteStr = SwapHexBytes(byteStr);
                                int decimalValue = Convert.ToInt32(byteStr, 16); // 轉換為10進制
                                string result = decimalValue.ToString(); // 轉換為字串
                                                                         // string asciiString = result;
                                TIME.Text = result;
                                byteStr = line.Substring(13, 4);
                                byteStr = SwapHexBytes(byteStr);
                                int decim = Convert.ToInt32(byteStr, 16); // 轉換為10進制

                                string re = decim.ToString(); // 轉換為字串
                                                              // string ascii = re;
                                moth.Text = re;

                            }
                            if (address == MCU_FW_Model_Type_Flash)
                            {

                                byteStr = line.Substring(9, 4);

                                byteStr = SwapHexBytes(byteStr);
                                uint decimalValue = Convert.ToUInt32(byteStr, 16); // 轉換為10進制

                                if ((decimalValue & 0X0001) == 0X0001)
                                {
                                    FAN_control.Text = "風扇為100%";

                                }
                                else
                                {
                                    FAN_control.Text = "風扇為韌體控制";
                                }
                                if ((decimalValue & 0X0002) == 0X0002)
                                {
                                    uart_use.Text = "可監控韌體";
                                }
                                else
                                {
                                    uart_use.Text = "不可監控韌體";
                                }

                                if ((decimalValue & 0X0004) == 0X0004)
                                {
                                    protect.Text = "保護功能移除";
                                }
                                else
                                {
                                    protect.Text = "有保護功能";

                                }



                                string result = decimalValue.ToString(); // 轉換為字串
                                int number = Convert.ToInt32(result, 16);
                                //// 轉回十進位字串
                            }

                            // old.Checked = true;
                            // new_fw.Checked = false;


                            if (address == M12V_StartDuty_StartLoad)
                            {
                                if (fw == HEX_FirmwareKind.Old)
                                {
                                    start_pwm_12v = (uint)Voltage_read(line, 13, ControlMode.PWM);
                                    _rows.Add(new FanParamRow
                                    {
                                        Group = "12V",
                                        //Name = "12V起轉DUTY",
                                        Name = FanData.DESC_12V_START_DUTY,
                                        Value1 = Format(start_pwm_12v, ControlMode.PWM),
                                        Firmware = "Old",
                                        Address = "0x3E20"
                                    });

                                    var start_v1_12v = Voltage_read(line, 9, ControlMode.Voltage);
                                    _rows.Add(new FanParamRow
                                    {
                                        Group = "12V",
                                        Name = FanData.DESC_12V_FIRST,
                                        Value1 = Format(start_v1_12v, ControlMode.Voltage),
                                        // Unit1 = "%",
                                        //Value2 = Voltage_read(line, 9, ControlMode.Voltage).ToString(),
                                        //Unit2 = "V",
                                        Firmware = "Old",
                                        Address = "0x3E20"
                                    });


                                }
                                else
                                {
                                    float v = FromHexfileWord(line.Substring(9, 8));
                                    _rows.Add(new FanParamRow
                                    {
                                        Group = "12V",
                                        Name = FanData.DESC_12V_FIRST,
                                        Value1 = Format(v, ControlMode.Voltage),
                                        Firmware = "NEW",
                                        Address = "0x3E20"
                                    });
                                }
                            }
                            if (address == M12V_EndLoad_SlopeLoad)
                            {
                                if (fw == HEX_FirmwareKind.Old)
                                {
                                    var v1 = Voltage_read(line, 9, ControlMode.Voltage);
                                    _rows.Add(new FanParamRow
                                    {
                                        Group = "12V",
                                        Name = FanData.DESC_12V_TWO,
                                        Value1 = Format(v1, ControlMode.Voltage),
                                        Firmware = "Old",
                                        Address = "0x3E24"
                                    });

                                    var start_v1_12v = Voltage_read(line, 13, ControlMode.Voltage);
                                    _rows.Add(new FanParamRow
                                    {
                                        Group = "12V",
                                        Name = FanData.DESC_12V_THREE,
                                        Value1 = Format(start_v1_12v, ControlMode.Voltage),
                                        Firmware = "Old",
                                        Address = "0x3E24"
                                    });
                                }
                                else
                                {
                                    float v = FromHexfileWord(line.Substring(9, 8));
                                    _rows.Add(new FanParamRow
                                    {
                                        Group = "12V",
                                        Name = FanData.DESC_12V_TWO,
                                        Value1 = Format(v, ControlMode.Voltage),
                                        // Value1 = v.ToString("0.###"),
                                        //  Unit1 = "V",
                                        Firmware = "NEW",
                                        Address = "0x3E24"
                                    });
                                }

                            }

                            if (address == M5V_StartLoad_M12V_EndDuty)
                            {
                                if (fw == HEX_FirmwareKind.Old)
                                {
                                    end_pwm_12v = (uint)Voltage_read(line, 9, ControlMode.PWM);
                                    var v5_first = Voltage_read(line, 13, ControlMode.Voltage);

                                    _rows.Add(new FanParamRow
                                    {
                                        Group = "12V",
                                        Name = FanData.DESC_12V_END_DUTY,
                                        // Value1 = end_pwm_12v.ToString(),
                                        //Unit1 = "V",
                                        Value1 = Format(end_pwm_12v, ControlMode.PWM),
                                        // Unit2 = "%",
                                        Firmware = "Old",
                                        Address = "0x3E28"
                                    });
                                    _rows.Add(new FanParamRow
                                    {
                                        Group = "5V",
                                        Name = FanData.DESC_5V_FIRST,
                                        // Value1 = end_pwm_12v.ToString(),
                                        //Unit1 = "V",
                                        Value1 = Format(v5_first, ControlMode.Voltage),
                                        // Unit2 = "%",
                                        Firmware = "Old",
                                        Address = "0x3E28"
                                    });

                                }
                                else
                                {

                                    float v = FromHexfileWord(line.Substring(9, 8));
                                    _rows.Add(new FanParamRow
                                    {
                                        Group = "12V",
                                        Name = FanData.DESC_12V_THREE,
                                        Value1 = Format(v, ControlMode.Voltage),
                                        Firmware = "NEW",
                                        Address = "0x3E28"
                                    });
                                }

                            }
                            if (address == M5V_SlopeLoad_StartDuty)
                            {

                                if (fw == HEX_FirmwareKind.Old)
                                {

                                    var end_pwm_5v = Voltage_read(line, 9, ControlMode.PWM);


                                    _rows.Add(new FanParamRow
                                    {
                                        Group = "5V",
                                        Name = FanData.DESC_5V_START_DUTY,
                                        Value1 = Format(end_pwm_5v, ControlMode.PWM),
                                        Firmware = "Old",
                                        Address = "0x3E2C"
                                    });

                                }
                                else
                                {
                                    var v1 = Voltage_read(line, 9, ControlMode.Slope);
                                    var v2 = Voltage_read(line, 13, ControlMode.Slope);

                                    _rows.Add(new FanParamRow
                                    {
                                        Group = "12V",
                                        Name = FanData.DESC_12V_START_DUTY,
                                        Value1 = Format(v2, ControlMode.PWM),
                                        Firmware = "NEW",
                                        Address = "0x3E2C"
                                    });
                                    _rows.Add(new FanParamRow
                                    {
                                        Group = "12V",
                                        Name = FanData.DESC_12V_END_DUTY,
                                        Value1 = Format(v1, ControlMode.PWM),
                                        Firmware = "NEW",
                                        Address = "0x3E2C"
                                    });
                                }
                            }

                            if (address == M5V_EndDuty_EndLoad)
                            {

                                if (fw == HEX_FirmwareKind.Old)
                                {

                                    var v1 = Voltage_read(line, 13, ControlMode.PWM);
                                    var v2 = Voltage_read(line, 9, ControlMode.Voltage);
                                    _rows.Add(new FanParamRow
                                    {
                                        Group = "5V",
                                        Name = FanData.DESC_5V_END_DUTY,
                                        Value1 = Format(v1, ControlMode.PWM),
                                        Firmware = "Old",
                                        Address = "0x3E30"
                                    });
                                    _rows.Add(new FanParamRow
                                    {
                                        Group = "5V",
                                        Name = FanData.DESC_5V_TWO,
                                        Value1 = Format(v2, ControlMode.Voltage),
                                        Firmware = "Old",
                                        Address = "0x3E30"
                                    });
                                }
                                else
                                {

                                    var v1 = Voltage_read(line, 9, ControlMode.Slope);
                                    var v2 = Voltage_read(line, 13, ControlMode.Slope);
                                    _rows.Add(new FanParamRow
                                    {
                                        Group = "12V",
                                        Name = FanData.FAN_BUFFER_TIME,
                                        Value1 = Format(v1, ControlMode.BufferTime),
                                        Firmware = "NEW",
                                        Address = "0x3E30"
                                    });
                                    _rows.Add(new FanParamRow
                                    {
                                        Group = "12V",
                                        Name = FanData.FAN_SETTING_TIME,
                                        Value1 = Format(v2, ControlMode.BufferTime),
                                        Firmware = "NEW",
                                        Address = "0x3E30"
                                    });
                                }


                            }
                            if (address == M3V_StartDuty_StartLoad)
                            {
                                if (fw == HEX_FirmwareKind.Old)
                                {
                                    var v1 = Voltage_read(line, 13, ControlMode.PWM);
                                    var v2 = Voltage_read(line, 9, ControlMode.Voltage);
                                    _rows.Add(new FanParamRow
                                    {
                                        Group = "3V",
                                        Name = FanData.DESC_3V_START_DUTY,
                                        Value1 = Format(v1, ControlMode.PWM),
                                        // Unit1 = "%",
                                        Firmware = "Old",
                                        Address = "0x3E34"
                                    });
                                    _rows.Add(new FanParamRow
                                    {
                                        Group = "3V",
                                        Name = FanData.DESC_3V_FIRST,
                                        Value1 = Format(v2, ControlMode.Voltage),
                                        // Unit1 = "%",
                                        Firmware = "Old",
                                        Address = "0x3E34"
                                    });
                                }
                                else
                                {
                                    float v = FromHexfileWord(line.Substring(9, 8));
                                    _rows.Add(new FanParamRow
                                    {
                                        Group = "5V",
                                        Name = FanData.DESC_5V_FIRST,
                                        Value1 = Format(v, ControlMode.Voltage),
                                        Firmware = "NEW",
                                        Address = "0x3E34"
                                    });

                                }

                            }
                            if (address == M3V_EndLoad_SlopeLoad)
                            {

                                if (fw == HEX_FirmwareKind.Old)
                                {
                                    var v2 = Voltage_read(line, 13, ControlMode.Voltage);
                                    _rows.Add(new FanParamRow
                                    {
                                        Group = "3V",
                                        Name = FanData.DESC_3V_TWO,
                                        Value1 = Format(v2, ControlMode.Voltage),
                                        Firmware = "Old",
                                        Address = "0x3E38"
                                    });
                                }
                                else
                                {
                                    float v = FromHexfileWord(line.Substring(9, 8));
                                    _rows.Add(new FanParamRow
                                    {
                                        Group = "5V",
                                        Name = FanData.DESC_5V_TWO,
                                        Value1 = Format(v, ControlMode.Voltage),
                                        Firmware = "NEW",
                                        Address = "0x3E38"
                                    });

                                }

                            }
                            if (address == M3V_EndDuty)
                            {
                                if (fw == HEX_FirmwareKind.Old)
                                {
                                    var v1 = Voltage_read(line, 9, ControlMode.PWM);
                                    _rows.Add(new FanParamRow
                                    {
                                        Group = "3V",
                                        Name = FanData.DESC_3V_END_DUTY,
                                        Value1 = Format(v1, ControlMode.PWM),
                                        // Unit1 = "%",
                                        Firmware = "Old",
                                        Address = "0x3E3C"
                                    });

                                }
                                else
                                {
                                    var v1 = Voltage_read(line, 9, ControlMode.Slope);
                                    var v2 = Voltage_read(line, 13, ControlMode.Slope);

                                    _rows.Add(new FanParamRow
                                    {
                                        Group = "5V",
                                        Name = FanData.DESC_5V_START_DUTY,
                                        Value1 = Format(v2, ControlMode.PWM),
                                        Firmware = "NEW",
                                        Address = "0x3E3C"
                                    });
                                    _rows.Add(new FanParamRow
                                    {
                                        Group = "5V",
                                        Name = FanData.DESC_5V_END_DUTY,
                                        Value1 = Format(v1, ControlMode.PWM),
                                        Firmware = "NEW",
                                        Address = "0x3E3C"
                                    });

                                }

                            }

                            if (address == LowOTP_60Load_30Load)
                            {




                                if (fw == HEX_FirmwareKind.Old)
                                {
                                    var v1 = Voltage_read(line, 9, ControlMode.Voltage);
                                    var v2 = Voltage_read(line, 13, ControlMode.Voltage);
                                    _rows.Add(new FanParamRow
                                    {
                                        Group = "LT",
                                        Name = FanData.LT_1,
                                        Value1 = Format(v1, ControlMode.Voltage),
                                        Firmware = "Old",
                                        Address = "0x3E40"
                                    });
                                    _rows.Add(new FanParamRow
                                    {
                                        Group = "LT",
                                        Name = FanData.LT_2,
                                        Value1 = Format(v2, ControlMode.Voltage),
                                        Firmware = "Old",
                                        Address = "0x3E40"
                                    });
                                }
                                else
                                {
                                    float v = FromHexfileWord(line.Substring(9, 8));
                                    _rows.Add(new FanParamRow
                                    {
                                        Group = "3V",
                                        Name = FanData.DESC_3V_FIRST,
                                        Value1 = Format(v, ControlMode.Voltage),
                                        Firmware = "NEW",
                                        Address = "0x3E40"
                                    });

                                }
                            }
                            if (address == OTP_StartLevel_LowOTP_MaxLoad)
                            {

                                if (fw == HEX_FirmwareKind.Old)
                                {

                                    var v1 = Voltage_read(line, 9, ControlMode.Voltage);
                                    var v2 = Voltage_read(line, 13, ControlMode.Voltage);

                                    _rows.Add(new FanParamRow
                                    {

                                        Group = "LT",
                                        Name = FanData.LT_3,
                                        Value1 = Format(v1, ControlMode.Voltage),
                                        Firmware = "Old",
                                        Address = "0x3E44"
                                    });


                                    _rows.Add(new FanParamRow
                                    {

                                        Group = "OTP",
                                        Name = FanData.OTP_FIRST,
                                        Value1 = Format(v2, ControlMode.Voltage),
                                        Firmware = "Old",
                                        Address = "0x3E44"
                                    });




                                }
                                else
                                {
                                    float v = FromHexfileWord(line.Substring(9, 8));
                                    _rows.Add(new FanParamRow
                                    {
                                        Group = "3V",
                                        Name = FanData.DESC_3V_TWO,
                                        Value1 = Format(v, ControlMode.Voltage),
                                        Firmware = "NEW",
                                        Address = "0x3E44"
                                    });

                                }

                            }
                            if (address == MiddleLevel_OTP_EndLevel)
                            {

                                if (fw == HEX_FirmwareKind.Old)
                                {

                                    var v1 = Voltage_read(line, 9, ControlMode.Voltage);
                                    var v2 = Voltage_read(line, 13, ControlMode.Voltage);

                                    _rows.Add(new FanParamRow
                                    {

                                        Group = "OTP",
                                        Name = FanData.OTP_TWO,
                                        Value1 = Format(v2, ControlMode.Voltage),
                                        Firmware = "Old",
                                        Address = "0x3E48"
                                    });


                                    _rows.Add(new FanParamRow
                                    {

                                        Group = "OTP",
                                        Name = FanData.OTP_THREE,
                                        Value1 = Format(v1, ControlMode.Voltage),
                                        Firmware = "Old",
                                        Address = "0x3E48"
                                    });
                                }
                                else
                                {
                                    var v1 = (uint)Voltage_read(line, 9, ControlMode.Slope);
                                    var v2 = (uint)Voltage_read(line, 13, ControlMode.Slope);

                                    _rows.Add(new FanParamRow
                                    {
                                        Group = "3V",
                                        Name = FanData.DESC_3V_START_DUTY,
                                        Value1 = Format(v2, ControlMode.PWM),
                                        Firmware = "NEW",
                                        Address = "0x3E48"
                                    });
                                    _rows.Add(new FanParamRow
                                    {
                                        Group = "3V",
                                        Name = FanData.DESC_3V_END_DUTY,
                                        Value1 = Format(v1, ControlMode.PWM),
                                        Firmware = "NEW",
                                        Address = "0x3E48"
                                    });

                                }
                            }
                            if (address == MaxDuty_OTP_MiddleDuty)
                            {

                                if (fw == HEX_FirmwareKind.Old)
                                {

                                    var v1 = Voltage_read(line, 9, ControlMode.PWM);
                                    var v2 = Voltage_read(line, 13, ControlMode.PWM);

                                    _rows.Add(new FanParamRow
                                    {
                                        Group = "OTP",
                                        Name = FanData.FAN_DUTY,
                                        Value1 = Format(v1, ControlMode.PWM),

                                        Firmware = "Old",
                                        Address = "0x3E4C"
                                    });
                                    _rows.Add(new FanParamRow
                                    {
                                        Group = "OTP",
                                        Name = FanData.MAX_PWM,
                                        Value1 = Format(v2, ControlMode.PWM),
                                        Firmware = "Old",
                                        Address = "0x3E4C"
                                    });
                                }
                                else
                                {

                                    float v = FromHexfileWord(line.Substring(9, 8));
                                    _rows.Add(new FanParamRow
                                    {
                                        Group = "OTP",
                                        Name = FanData.OTP_FIRST,
                                        Value1 = Format(v, ControlMode.Voltage),
                                        //  Unit1 = "V",
                                        Firmware = "NEW",
                                        Address = "0x3E4C"
                                    });

                                }
                            }
                            if (address == FanDutySetTime_StopDuty)
                            {

                                if (fw == HEX_FirmwareKind.Old)
                                {

                                    double value1 = Voltage_read(line, 13, ControlMode.Slope);
                                    double value2 = Voltage_read(line, 9, ControlMode.PWM);
                                    double percent = (double)(end_pwm_12v - start_pwm_12v) / 100.0;
                                    double value3 = percent * value1 * 1920 / 1000.0;
                                    int FAN_SET_TIME = (int)Math.Round(value3, MidpointRounding.AwayFromZero);

                                    _rows.Add(new FanParamRow
                                    {
                                        Group = "OTP",
                                        Name = FanData.FAN_SETTING_TIME,
                                        Value1 = Format(FAN_SET_TIME, ControlMode.BufferTime),
                                        Firmware = "Old",
                                        Address = "0x3E50"
                                    });
                                    _rows.Add(new FanParamRow
                                    {
                                        Group = "OTP",
                                        Name = FanData.FAN_DUTY,
                                        Value1 = Format(value2, ControlMode.PWM),
                                        Firmware = "Old",
                                        Address = "0x3E50"
                                    });

                                }
                                else
                                {
                                    float v = FromHexfileWord(line.Substring(9, 8));
                                    _rows.Add(new FanParamRow
                                    {
                                        Group = "OTP",
                                        Name = FanData.OTP_TWO,
                                        Value1 = Format(v, ControlMode.Voltage),
                                        //  Unit1 = "V",
                                        Firmware = "NEW",
                                        Address = "0x3E50"
                                    });


                                }

                            }
                            if (address == FanDutySetTimeFall_PFCMOSGain)
                            {

                                if (fw == HEX_FirmwareKind.Old)
                                {

                                }
                                else
                                {
                                    float v = FromHexfileWord(line.Substring(9, 8));
                                    _rows.Add(new FanParamRow
                                    {
                                        Group = "OTP",
                                        Name = FanData.OTP_THREE,
                                        Value1 = Format(v, ControlMode.Voltage),
                                        Firmware = "NEW",
                                        Address = "0x3E54"
                                    });

                                }
                            }

                            ///////////////////////////////////////////////////////
                            if (address == TempHysteresis_StopDelayTime)
                            {

                                if (fw == HEX_FirmwareKind.Old)
                                {
                                    double value1 = Voltage_read(line, 13, ControlMode.OPP_Watter);
                                    double value2 = Voltage_read(line, 9, ControlMode.OPP_Watter);
                                    double value3 = ((int)value1 << 16) | (int)value2;
                                    double BUFFER_TIME = value3 / 1000;

                                    _rows.Add(new FanParamRow
                                    {
                                        Group = "OTP",
                                        Name = FanData.FAN_BUFFER_TIME,
                                        Value1 = Format(BUFFER_TIME, ControlMode.BufferTime),
                                        Firmware = "Old",
                                        Address = "0x3E58"
                                    });

                                }
                                else
                                {
                                    var v1 = (uint)Voltage_read(line, 9, ControlMode.Slope);
                                    var v2 = (uint)Voltage_read(line, 13, ControlMode.Slope);

                                    _rows.Add(new FanParamRow
                                    {
                                        Group = "OTP",
                                        Name = FanData.FAN_START_DUTY,
                                        Value1 = Format(v2, ControlMode.PWM),
                                        Firmware = "NEW",
                                        Address = "0x3E58"
                                    });
                                    _rows.Add(new FanParamRow
                                    {
                                        Group = "OTP",
                                        Name = FanData.FAN_END_DUTY,
                                        Value1 = Format(v1, ControlMode.PWM),
                                        Firmware = "NEW",
                                        Address = "0x3E58"
                                    });

                                }

                            }
                            ////////////////////////////////////////////////////////////////////////
                            if (address == SROTP_TaOTP_Level)
                            {
                                if (fw == HEX_FirmwareKind.Old)
                                {
                                    var v1 = Voltage_read(line, 9, ControlMode.Voltage);
                                    var v2 = Voltage_read(line, 13, ControlMode.Voltage);

                                    _rows.Add(new FanParamRow
                                    {
                                        Group = "OTP",
                                        Name = FanData.TA_OTP,
                                        Value1 = Format(v1, ControlMode.Voltage),
                                        Firmware = "Old",
                                        Address = "0x3E5C"
                                    });
                                    _rows.Add(new FanParamRow
                                    {
                                        Group = "OTP",
                                        Name = FanData.SR_OTP,
                                        Value1 = Format(v2, ControlMode.Voltage),
                                        Firmware = "Old",
                                        Address = "0x3E5C"
                                    });
                                }
                                else
                                {
                                    //  string IFlash_Fan_MOS_OTP_V = line.Substring(9, 8);
                                    float v = FromHexfileWord(line.Substring(9, 8));
                                    _rows.Add(new FanParamRow
                                    {
                                        Group = "OTP",
                                        Name = FanData.TA_OTP,
                                        Value1 = Format(v, ControlMode.Voltage),
                                        //Unit1 = "V",
                                        //Value2 = Voltage_read(line, 13, ControlMode.Slope).ToString(),
                                        //Unit2 = "%",
                                        Firmware = "NEW",
                                        Address = "0x3E5C"

                                    });
                                }

                            }
                            if (address == OPProtectLevel)
                            {

                                if (fw == HEX_FirmwareKind.Old)
                                {
                                    var v1 = Voltage_read(line, 9, ControlMode.OPP_Watter);

                                    _rows.Add(new FanParamRow
                                    {
                                        Group = "OPP",
                                        Name = FanData.OPP,
                                        Value1 = Format(v1, ControlMode.OPP_Watter),
                                        // Value2 = Voltage_read(line, 9, ControlMode.PWM).ToString(),
                                        // Unit2 = "V",
                                        Firmware = "Old",
                                        Address = "0x3E60"
                                    });
                                }
                                else
                                {
                                    float v = FromHexfileWord(line.Substring(9, 8));
                                    _rows.Add(new FanParamRow
                                    {
                                        Group = "OTP",
                                        Name = FanData.TA_OTP,
                                        Value1 = Format(v, ControlMode.Voltage),
                                        Firmware = "NEW",
                                        Address = "0x3E60"

                                    });
                                }
                            }

                            if (address == IFlash_OPP_Factor)
                            {

                                if (fw == HEX_FirmwareKind.Old)
                                {
                                }
                                else
                                {
                                    double v = Voltage_read(line, 13, ControlMode.OPP_Watter);
                                    v = v / 100;
                                    _rows.Add(new FanParamRow
                                    {
                                        Group = "OPP",
                                        Name = "OPP",
                                        Value1 = Format(v, ControlMode.OPP_NEW),
                                        Firmware = "NEW",
                                        Address = "0x3E64"

                                    });
                                }
                            }
                        }
                        else if (recordType == 1) // 01 = End of File Record
                        {
                            break; // 結束文件解析
                        }



                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("讀取文件時出錯：" + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Hex_read_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Hex_read_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void fandataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
 }



