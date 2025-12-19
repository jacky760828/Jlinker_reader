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

            Add_NEW_FONT_FanRow(table, parser, 3, "12V負載第一點", ControlMode.Voltage, ControlMode.PWM, ControlMode.None, ControlMode.None);
            Add_NEW_FONT_FanRow(table, parser, 4, "12V負載第二點", ControlMode.Voltage, ControlMode.PWM, ControlMode.None, ControlMode.None);
            Add_NEW_FONT_FanRow(table, parser, 5, "12V負載第三點", ControlMode.Voltage, ControlMode.PWM, ControlMode.None, ControlMode.None);
            Add_NEW_FONT_FanRow(table, parser, 6, "12V開始Duty", ControlMode.Voltage, ControlMode.PWM, ControlMode.None, ControlMode.None);
            Add_NEW_FONT_FanRow(table, parser, 6, "12V結束Duty", ControlMode.Voltage, ControlMode.PWM, ControlMode.None, ControlMode.None);
            Add_NEW_FONT_FanRow(table, parser, 7, "12V風扇設定時間", ControlMode.Voltage, ControlMode.PWM, ControlMode.Slope, ControlMode.None);
            Add_NEW_FONT_FanRow(table, parser, 7, "風扇緩衝時間", ControlMode.Voltage, ControlMode.PWM, ControlMode.Slope, ControlMode.None);
            Add_NEW_FONT_FanRow(table, parser, 8, "5V負載第一點", ControlMode.Voltage, ControlMode.PWM, ControlMode.None, ControlMode.None);
            Add_NEW_FONT_FanRow(table, parser, 9, "5V負載第二點", ControlMode.Voltage, ControlMode.PWM, ControlMode.None, ControlMode.None);
            Add_NEW_FONT_FanRow(table, parser, 10, "5V開始Duty", ControlMode.Voltage, ControlMode.PWM, ControlMode.None, ControlMode.None);
            Add_NEW_FONT_FanRow(table, parser, 10, "5V結束Duty", ControlMode.Voltage, ControlMode.PWM, ControlMode.None, ControlMode.None);
            Add_NEW_FONT_FanRow(table, parser, 11, "3V負載第一點", ControlMode.Voltage, ControlMode.PWM, ControlMode.None, ControlMode.None);
            Add_NEW_FONT_FanRow(table, parser, 12, "3V負載第二點", ControlMode.Voltage, ControlMode.PWM, ControlMode.None, ControlMode.None);
            Add_NEW_FONT_FanRow(table, parser, 13, "3V開始Duty", ControlMode.Voltage, ControlMode.PWM, ControlMode.None, ControlMode.None);
            Add_NEW_FONT_FanRow(table, parser, 13, "3V結束Duty", ControlMode.Voltage, ControlMode.PWM, ControlMode.None, ControlMode.None);
            Add_NEW_FONT_FanRow(table, parser, 14, "OTP負載第一點", ControlMode.Voltage, ControlMode.PWM, ControlMode.None, ControlMode.None);
            Add_NEW_FONT_FanRow(table, parser, 15, "OTP負載第二點", ControlMode.Voltage, ControlMode.PWM, ControlMode.None, ControlMode.None);
            Add_NEW_FONT_FanRow(table, parser, 16, "OTP負載第三點", ControlMode.Voltage, ControlMode.PWM, ControlMode.None, ControlMode.None);
            Add_NEW_FONT_FanRow(table, parser, 17, "OTP開始Duty", ControlMode.Voltage, ControlMode.PWM, ControlMode.None, ControlMode.None);
            Add_NEW_FONT_FanRow(table, parser, 17, "OTP結束Duty", ControlMode.Voltage, ControlMode.PWM, ControlMode.None, ControlMode.None);
            Add_NEW_FONT_FanRow(table, parser, 18, "MOS_OTP", ControlMode.Voltage, ControlMode.PWM, ControlMode.None, ControlMode.None);
            Add_NEW_FONT_FanRow(table, parser, 19, "TA_OTP", ControlMode.Voltage, ControlMode.PWM, ControlMode.None, ControlMode.None);
            Add_NEW_FONT_FanRow(table, parser, 20, "OPP", ControlMode.Voltage, ControlMode.PWM, ControlMode.None, ControlMode.OPP_NEW);

        }

        private void FillOldFirmwareRows(DataTable table, FanDataParser parser)
        {

            AddFanRow(table, parser, 3, "12V負載第一點/12V起轉PWM", ControlMode.Voltage, ControlMode.PWM, ControlMode.None, ControlMode.None);
            AddFanRow(table, parser, 4, "12V負載第二點", ControlMode.Voltage, ControlMode.PWM, ControlMode.None, ControlMode.None);
            AddFanRow(table, parser, 4, "12V負載第三點", ControlMode.Voltage, ControlMode.PWM, ControlMode.None, ControlMode.None);

            AddFanRow(table, parser, 5, "5V負載第一點/12V結束PWM", ControlMode.Voltage, ControlMode.PWM, ControlMode.None, ControlMode.None);
            AddFanRow(table, parser, 6, "5V開始Duty", ControlMode.Voltage, ControlMode.PWM, ControlMode.None, ControlMode.None);
            AddFanRow(table, parser, 7, "5V負載第二點/5V結束PWM", ControlMode.Voltage, ControlMode.PWM, ControlMode.Slope, ControlMode.None);

            AddFanRow(table, parser, 8, "3V負載第一點/3V起轉PWM", ControlMode.Voltage, ControlMode.PWM, ControlMode.None, ControlMode.None);
            AddFanRow(table, parser, 9, "3V負載第二點", ControlMode.Voltage, ControlMode.PWM, ControlMode.None, ControlMode.None);
            AddFanRow(table, parser, 10, "3V結束Duty", ControlMode.Voltage, ControlMode.PWM, ControlMode.None, ControlMode.None);

            AddFanRow(table, parser, 11, "低溫判斷點1", ControlMode.Voltage, ControlMode.PWM, ControlMode.None, ControlMode.None);
            AddFanRow(table, parser, 11, "低溫判斷點2", ControlMode.Voltage, ControlMode.PWM, ControlMode.None, ControlMode.None);
            AddFanRow(table, parser, 12, "低溫判斷點3", ControlMode.Voltage, ControlMode.PWM, ControlMode.None, ControlMode.None);

            AddFanRow(table, parser, 12, "OTP負載第一點", ControlMode.Voltage, ControlMode.PWM, ControlMode.None, ControlMode.None);
            AddFanRow(table, parser, 13, "OTP負載第二點", ControlMode.Voltage, ControlMode.PWM, ControlMode.None, ControlMode.None);
            AddFanRow(table, parser, 13, "OTP負載第三點", ControlMode.Voltage, ControlMode.PWM, ControlMode.None, ControlMode.None);

            AddFanRow(table, parser, 14, "最大PWM轉速", ControlMode.Voltage, ControlMode.PWM, ControlMode.None, ControlMode.None);
            AddFanRow(table, parser, 14, "風扇PWM轉速", ControlMode.Voltage, ControlMode.PWM, ControlMode.None, ControlMode.None);
            AddFanRow(table, parser, 15, "最低PWM轉速", ControlMode.Voltage, ControlMode.PWM, ControlMode.None, ControlMode.None);
            AddFanRow(table, parser, 15, "風扇設定時間", ControlMode.Voltage, ControlMode.PWM, ControlMode.BufferTime, ControlMode.None);
            AddFanRow(table, parser, 17, "風扇緩衝時間", ControlMode.Voltage, ControlMode.PWM, ControlMode.BufferTime, ControlMode.None);
            AddFanRow(table, parser, 18, "SR_OTP", ControlMode.Voltage, ControlMode.PWM, ControlMode.None, ControlMode.None);
            AddFanRow(table, parser, 18, "TA_OTP", ControlMode.Voltage, ControlMode.PWM, ControlMode.None, ControlMode.None);
            AddFanRow(table, parser, 19, "OPP", ControlMode.Voltage, ControlMode.PWM, ControlMode.None, ControlMode.OPP_Watter);

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
                        if (desc == "12V結束Duty")
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
                        if (desc == "12V風扇設定時間")
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
                        if (desc == "5V結束Duty")
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
                        if (desc == "3V結束Duty")
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
                    if (desc == "OTP結束Duty")
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
                    string voltage = FormatDisplay(parser.Decode(low, voltageMode), voltageMode);
                    string pwm = FormatDisplay(parser.Decode(high, pwmMode), pwmMode);
                    table.Rows.Add( desc, voltage, pwm, "NA", "NA");
                    break;
                case 4:
                    {
                        if (desc == "12V負載第二點")
                        {
                            string V12_2 = FormatDisplay(parser.Decode(low, voltageMode), voltageMode);
                            table.Rows.Add( desc, V12_2, "NA", "NA", "NA");
                        }
                        else
                        {
                            string V12_3 = FormatDisplay(parser.Decode(high, voltageMode), voltageMode);
                            table.Rows.Add( desc, V12_3, "NA", "NA", "NA");
                        }
                    }
                    break;
                case 5:
                    string V5_1 = FormatDisplay(parser.Decode(high, voltageMode), voltageMode);
                    string PWM_12V_end = FormatDisplay(parser.Decode(low, pwmMode), pwmMode);
                    table.Rows.Add( desc, V5_1, PWM_12V_end, "NA", "NA");
                    break;
                case 6:
                    string PWM_5V_start = FormatDisplay(parser.Decode(low, pwmMode), pwmMode);
                    table.Rows.Add(desc, 0, PWM_5V_start, "NA", "NA");
                    break;
                case 7:

                    string PWM_5V_end = FormatDisplay(parser.Decode(high, pwmMode), pwmMode);
                    string V5_2 = FormatDisplay(parser.Decode(low, voltageMode), voltageMode);
                    table.Rows.Add( desc, V5_2, PWM_5V_end, "NA", "NA");
                    break;

                case 8:

                    string PWM_3V_start = FormatDisplay(parser.Decode(high, pwmMode), pwmMode);
                    string V3_1 = FormatDisplay(parser.Decode(low, voltageMode), voltageMode);
                    table.Rows.Add(desc, V3_1, PWM_3V_start, "NA", "NA");
                    break;

                case 9:
                    string V3_2 = FormatDisplay(parser.Decode(high, voltageMode), voltageMode);
                    table.Rows.Add( desc, V3_2, "NA", "NA", "NA");
                    break;
                case 10:
                    string PWM_3V_end = FormatDisplay(parser.Decode(low, pwmMode), pwmMode);
                    table.Rows.Add( desc, "NA", PWM_3V_end, "NA", "NA");
                    break;
                case 11:
                    if (desc == "低溫判斷點2")
                    {
                        string LT_2 = FormatDisplay(parser.Decode(low, voltageMode), voltageMode);
                        table.Rows.Add(desc, LT_2, "NA", "NA", "NA");
                    }
                    else
                    {
                        string LT_1 = FormatDisplay(parser.Decode(high, voltageMode), voltageMode);
                        table.Rows.Add( desc, LT_1, "NA", "NA", "NA");
                    }

                    break;
                case 12:
                    if (desc == "低溫判斷點3")
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
                    if (desc == "OTP第二點")
                    {
                        string OTP_2 = FormatDisplay(parser.Decode(high, voltageMode), voltageMode);
                        table.Rows.Add( desc, OTP_2, "NA", "NA", "NA");
                    }
                    else
                    {
                        string OTP_3 = FormatDisplay(parser.Decode(low, voltageMode), voltageMode);
                        table.Rows.Add( desc, OTP_3, "NA", "NA", "NA");
                    }

                    break;
                case 14:
                    if (desc == "最大PWM轉速")
                    {
                        end_pwm_12v = (uint)parser.Decode(high, pwmMode);
                        string MAX_PWM = FormatDisplay(parser.Decode(high, pwmMode), pwmMode);
                        table.Rows.Add( desc, "NA", MAX_PWM, "NA", "NA");
                    }
                    else
                    {
                        string OTP_PWM = FormatDisplay(parser.Decode(low, pwmMode), pwmMode);
                        table.Rows.Add( desc, "NA", OTP_PWM, "NA", "NA");
                    }

                    break;
                case 15:
                    if (desc == "最低PWM轉速")
                    {
                        start_pwm_12v = (uint)parser.Decode(low, pwmMode);
                        string MIN_PWM = FormatDisplay(parser.Decode(low, pwmMode), pwmMode);
                        table.Rows.Add( desc, "NA", MIN_PWM, "NA", "NA");


                    }
                    else
                    {
                        var Slope_time = (uint)parser.Decode(high, ControlMode.Slope);
                        double percent = (double)(end_pwm_12v - start_pwm_12v) / 100.0;
                        double buffer_timer_value = percent * Slope_time * 1920 / 1000.0;
                        var temp_buffer_timer = (int)Math.Round(buffer_timer_value, MidpointRounding.AwayFromZero);
                        string FAN_TIME = FormatDisplay(temp_buffer_timer, ControlMode.Slope);
                        table.Rows.Add( desc, "NA", "NA", FAN_TIME, "NA");
                    }
                    break;

                case 17:
                    string buf_time = FormatDisplay(parser.Decode(low, ControlMode.BufferTime), ControlMode.BufferTime);
                    table.Rows.Add( desc, "NA", "NA", buf_time, "NA");
                    break;
                case 18:
                    if (desc == "SR_OTP")
                    {
                        string SR_OTP = FormatDisplay(parser.Decode(high, voltageMode), voltageMode);
                        table.Rows.Add( desc, SR_OTP, "NA", "NA", "NA");
                    }
                    else
                    {
                        string OTP_PWM = FormatDisplay(parser.Decode(low, voltageMode), voltageMode);
                        table.Rows.Add( desc, OTP_PWM, "NA", "NA", "NA");
                    }

                    break;
                case 19:
                    string OPP = FormatDisplay(parser.Decode(low, ControlMode.OPP_Watter), ControlMode.OPP_Watter);
                    table.Rows.Add( desc, "NA", "NA", "NA", OPP);
                    break;
            }

            // table.Rows.Add(index, description, voltage, pwm, time, watt);

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


        
    }



}