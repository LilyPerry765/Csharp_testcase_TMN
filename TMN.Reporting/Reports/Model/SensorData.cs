using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TMN.Reports.Model
{
   public class SensorVal
    {
        SensorData sd;
        public SensorVal(SensorData sd)
        {
            this.sd = sd;
        }

        public String SensorTitle
        {
            get
            {
                return sd.Sensor.Title;
            }
        }

        public int ModuleNumber
        {
            get
            {
                return sd.Sensor.ModulNumber ?? -1;
            }
        }

        public double? Value
        {
            get
            {
                return sd.Value;
            }
        }

        public string RoomName
        {
            get
            {
                return sd.Sensor.Room.Name;
            }
        }

        public string Date
        {
            get
            {
                return sd.Date.ToPersianDate().ToString("yy/MM/dd HH:mm");
            }
        }

    }
}
