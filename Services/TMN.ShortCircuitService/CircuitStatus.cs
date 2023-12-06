using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Enterprise;
using System.ComponentModel;

namespace TMN
{

    class CircuitStatus
    {

        private CircuitStatus()
        {
            //Use Parse method instead.
        }

        public int DeviceNumber
        {
            get;
            private set;
        }

        public Dictionary<int, CircuitEnum> Digits = new Dictionary<int, CircuitEnum>();
        public CircuitEnum this[int circuitNumber]
        {
            get
            {
                if(circuitNumber <= 100)
                    throw new NotSupportedException("The provided circuit number is not supported.");
                return Digits[circuitNumber];
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Device={0}", DeviceNumber);
            foreach(int key in Digits.Keys)
                sb.AppendFormat(", D{0} = {1}", key, Digits[key].ToString());
            return sb.ToString();
        }

        public static CircuitStatus Parse(string input, int currentIndex)
        {
            try
            {
                input = input.Replace(":", "0");
                var e = new CircuitStatus();
                e.DeviceNumber = int.Parse("" + input[2]);
                if (input.Length == 38 && input.StartsWith("[S") && input.EndsWith("]"))
                {
                    for (int i = 1; i <= 30; i++)
                    {
                        int key = currentIndex * 30 + i + 100;
                        try
                        {
                            e.Digits.Add(key, (CircuitEnum)Enum.Parse(typeof(CircuitEnum), "" + input[i + 2]));
                        }
                        catch
                        {
                            Logger.WriteException("Can't parse circuit number {0} value.", (key).ToString());
                        }
                    }
                }
                else
                 throw new FormatException("Recieved invalid format data");

                return e;
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
            }
            return null;
        }
    }
}
