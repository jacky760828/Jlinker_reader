using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace JLink_Find_Emulators
{
    //  internal class Fan_data_read
    internal class FanDataParser
    {
        public uint[] RawData;

        private readonly int divisor = 1920;

           public enum ControlMode
        {
           // None,
            Voltage,
            PWM,
            Slope,
            BufferTime,
            OPP_Watter,
            NEW_F_PWM,
            OPP_NEW,
            None
        }
        public FanDataParser(uint[] acData)
        {
            RawData = acData;
        }

        //public (double high, double low) ReadPair(uint value, ControlMode highMode, ControlMode lowMode)
        //{
        //    ushort high = (ushort)(value >> 16);
        //    ushort low = (ushort)(value & 0xFFFF);

        //    return (Decode(high, highMode), Decode(low, lowMode));
        //}

        public double Decode(double raw, ControlMode mode)
        {
          
            //if (shift == 1)
            //    raw >>= 16;
            //else
            //    raw &= 0xFFFF;

            switch (mode)
            {
                case ControlMode.Voltage:
                    return raw / 1000.0;

                case ControlMode.PWM:
                    return Math.Round((double)raw / divisor * 100, 1);

                case ControlMode.Slope:
                case ControlMode.OPP_Watter:
                    return raw;

                case ControlMode.BufferTime:
                    return raw / 1000.0;
                case ControlMode.NEW_F_PWM:
                      return raw;
                case ControlMode.OPP_NEW:


                    return raw/100;
                    //return raw;


                default:
                    return 0;
            }

        }
     



        //    // ==== 套用 UI ====
        //    //fandataGridView.DataSource = table;
        //}
        private string Format(double value, ControlMode mode)
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
                    return value.ToString("0.###")+"倍";

                default:
                    return "-";
            }
        }


  


        public float FromHexWord(uint hexWord)
        {
            // uint u = Convert.ToUInt32(hexWord, 16);          // 32-bit pattern                                                            
            //uint u = BinaryPrimitives.ReverseEndianness(hexWord); // 如果給的是「記憶體位元組順序」(AE 47 21 3F)，就把位元組反轉成真正位元樣式
            byte[] bytes = BitConverter.GetBytes(hexWord);
            return BitConverter.ToSingle(bytes, 0);
           // return BitConverter.Int32BitsToSingle(unchecked((int)u));
        }

    }




}
