using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Enterprise;

namespace TMN
{
    class SensorStatus
    {

        private SensorStatus()
        {
            //Use Parse method instead.

            IsSensor = true;
        }

        public int DeviceNumber
        {
            get;
            private set;
        }

        public double Temperature1
        {
            get;
            private set;
        }

        public double Temperature2
        {
            get;
            private set;
        }

        public double Temperature3
        {
            get;
            private set;
        }

        public double Humidity
        {
            get;
            private set;
        }

        //public bool Power1
        //{
        //    get;
        //    private set;
        //}

        //public bool Power2
        //{
        //    get;
        //    private set;
        //}

        //public bool Power3
        //{
        //    get;
        //    private set;
        //}

        //public bool Power4
        //{
        //    get;
        //    private set;
        //}

        //public bool Power5
        //{
        //    get;
        //    private set;
        //}

        //public bool Power6
        //{
        //    get;
        //    private set;
        //}

        //public bool Power7
        //{
        //    get;
        //    private set;
        //}

        //public bool Power8
        //{
        //    get;
        //    private set;
        //}

        public bool HasPower
        {
            get;
            private set;
        }

        public bool IsSensor
        {
            get;
            private set;
        }
        public double this[int sensorNumber]
        {
            get
            {
                switch (sensorNumber)
                {
                    case 1:
                        return Temperature1;
                    case 2:
                        return Temperature2;
                    case 3:
                        return Temperature3;
                    case 4:
                        return Humidity;
                    default:
                        throw new NotSupportedException("The provided sensor number is not supported.");
                }
            }
        }

        public bool[] power = new bool[8] { false, false, false, false, false, false, false, false };
        public bool Power(int powerNumber)
        {
            if (powerNumber > 18 && powerNumber < 11)
                throw new NotSupportedException("The provided power number is not supported.");
            return power[powerNumber - 11];
        }

        public Dictionary<int, CircuitEnum> circuit = new Dictionary<int, CircuitEnum>();
        public CircuitEnum Circuit(int circuitNumber)
        {
            if (circuitNumber <= 100)
                throw new NotSupportedException("The provided circuit number is not supported.");
            return circuit[circuitNumber];
        }




        public override string ToString()
        {

            if(IsSensor)
                return string.Format("Device={0}, T1={1}, T2={2}, T3={3}, H={4}, P1={5}, P2={6}, P3={7}, P4={8}, P5={9}, P6={10}, P7={11}, P8={12}", DeviceNumber, Temperature1, Temperature2, Temperature3, Humidity, Power(11), Power(12), Power(13), Power(14), Power(15), Power(16), Power(17), Power(18));

           StringBuilder sb = new StringBuilder();
           sb.AppendFormat("Device={0}", DeviceNumber);
           foreach (int key in circuit.Keys)
               sb.AppendFormat(", C{0} = {1}", key, circuit[key].ToString());
           return sb.ToString();
        }

        public static SensorStatus Parse(string input, int currentIndex)
        {
            try
            {
                input = input.Replace(":", "0").Replace("?", "0");
                var e = new SensorStatus();
                if ((input.Length == 24 || input.Length == 20) && input.StartsWith("[S") && input.EndsWith("]"))
                {
                    
                    GroupCollection groups;
                    groups = Regex.Match(input, @"\[S(?<Device>\d)(?<T1>\d{3})(?<T2>\d{3})(?<T3>\d{3})(?<H>\d{3})(?<D1>\d)(?<D2>\d)(?<D3>\d)(?<D4>\d)(?<D5>\d)?(?<D6>\d)?(?<D7>\d)?(?<D8>\d)?\]").Groups;

                    e.DeviceNumber = int.Parse(groups["Device"].Value);
                    e.Temperature1 = double.Parse(groups["T1"].Value) / 10;
                    e.Temperature2 = double.Parse(groups["T2"].Value) / 10;
                    e.Temperature3 = double.Parse(groups["T3"].Value) / 10;
                    e.Humidity = double.Parse(groups["H"].Value) / 10;
                    e.HasPower = true;
                    e.IsSensor = true;
                    for (int i = 1; i <= 8; i++)
                    {
                        try
                        {
                            e.power[i - 1] = groups["D" + i.ToString()].Value == "1" ? true : false;
                        }
                        catch
                        {
                            if (i > 4)
                                e.HasPower = false;
                            Logger.WriteException("Can't parse power sensor number {0} value.", i.ToString());
                        }
                    }

                }
                else if (input.Length == 38 && input.StartsWith("[S") && input.EndsWith("]"))
                {
                    e.IsSensor = false;
                    for (int i = 1; i <= 30; i++)
                    {
                        int key = (currentIndex - 1) * 30 + i + 100;
                        try
                        {
                            e.circuit.Add(key, (CircuitEnum)Enum.Parse(typeof(CircuitEnum), "" + input[i + 2]));
                        }
                        catch
                        {
                            Logger.WriteException("Can't parse circuit number {0} value.", (key).ToString());
                        }
                    }
                }
                

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
