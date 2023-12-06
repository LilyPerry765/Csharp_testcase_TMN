using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace TMN
{
    
    public partial class Room
    {


        //public IEnumerable<double> GetSensorLastData(SensorTypes sensorType)
        //{
        //    TMNModelDataContext db = new TMNModelDataContext();
        //    foreach (Sensor sensor in this.Sensors.Where(s => s.TypeID == (byte)sensorType))
        //    {
        //        var val = db.Sensors.Single(s => s == sensor).SensorDatas.OrderByDescending(sd => sd.Date).Select(sd => sd.Value).FirstOrDefault();
        //        if (val.HasValue)
        //        {
        //            yield return val.Value;
        //        }
        //    }
        //}

        public IEnumerable<Sensor> GetSensorsByType(SensorTypes type)
        {
            return this.Sensors.Where(s => s.TypeID == (byte)type);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
