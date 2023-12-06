using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TMN.Reports.Model
{
    public class SensorState
    {
        private Sensor sensor;
        public SensorState(Sensor sensor)
        {
            this.sensor = sensor;
        }

        public string Title
        {
            get
            {
                return string.Format("{0}- {1}", sensor.ModulNumber ?? -1, sensor.Title ?? "");
            }
        }

        public int Count
        {
            get
            {
                var lastHoure = new TMNModelDataContext().GetDate().Value.AddHours(-1);
                return sensor.SensorDatas.Count(sd => sd.Date >= lastHoure);
            }
        }
    }
}
