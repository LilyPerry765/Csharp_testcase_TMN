using System.Linq;
using Enterprise;
using System.Transactions;
using System.Runtime.Serialization;

namespace TMN
{
    
    public partial class Sensor
    {
        public SensorTypes SensorType
        {
            get
            {
                return (SensorTypes)TypeID.Value;
            }
            set
            {
                TypeID = (byte)value;
            }
        }

        
        public bool IsMute
        {
            get;
            set;
        }

        
        public double? LastValue
        {
            get
            {
                using (TMNModelDataContext db = new TMNModelDataContext())
                {
                    int timeout = Setting.Get(Setting.SENSOR_SERVICE_ACTIVITY_TIMEOUT, Setting.DEFAULT_SENSOR_SERVICE_ACTIVITY_TIMEOUT);
                    var validDate = db.GetDate().Value.AddSeconds(-timeout);
                    SensorData lastData = null;
                    using (new TransactionScope(TransactionScopeOption.Required, new TransactionOptions()
                    {
                        IsolationLevel = IsolationLevel.ReadUncommitted
                    }))
                    {
                        lastData = this.SensorDatas.Where(sd => sd.SensorID == ID).OrderByDescending(sd => sd.Date).FirstOrDefault(); ;
                    }
                    if (lastData == null) // || lastData.Date < validDate)
                    {
                        return null;
                    }
                    else if (lastData.Date < validDate && (SensorType == SensorTypes.Humidity || SensorType == SensorTypes.Temperature))
                    {
                        return null;
                    }
                    else
                    {
                        return lastData.Value;
                    }
                }
            }
        }

        public override string ToString()
        {
            return Title;
        }

        partial void OnModulNumberChanged()
        {
            TypeID = (byte)(ModulNumber != 4 ? ModulNumber > 3  ? ModulNumber < 10 || ModulNumber > 18 ? ModulNumber <= 100 || ModulNumber > 160 ? SensorTypes.NONE : SensorTypes.Circuit : SensorTypes.POWER : SensorTypes.Temperature: SensorTypes.Humidity );
        }

    }
}
