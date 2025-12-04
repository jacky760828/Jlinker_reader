using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace JLink_Find_Emulators
{
    internal class Fan_data_read
    {
        private readonly int divisor = 1920;
        
        public enum ControlMode
        {
            Voltage,
            PWM,
            Slope,
            BufferTime,
            OPP_Watter
        }

        public (double high, double low) ReadPair(uint word, ControlMode highMode, ControlMode lowMode)
        {

            double highValue = VoltageRead(word, 1, highMode);
            double lowValue = VoltageRead(word, 0, lowMode);
            return (highValue, lowValue);


        }

        public double VoltageRead(uint raw, int shift, ControlMode mode)
        {
            if (shift == 1)
                raw >>= 16;
            else
                raw &= 0xFFFF;

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

                default:
                    return 0;
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
