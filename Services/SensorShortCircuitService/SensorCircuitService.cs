
using System.ServiceProcess;

namespace Sensor_ShortCircuitService
{
    partial class SensorCircuitService : ServiceBase
    {
        SensorServiceCore sensorCore;
        CircuitServiceCore circuitCore;

        public SensorCircuitService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            base.OnStart(args);

            if(sensorCore == null && circuitCore == null )
            {
                sensorCore = new SensorServiceCore();
                circuitCore = new CircuitServiceCore();
            }
            sensorCore.Start();
            circuitCore.Start();
        }

        protected override void OnStop()
        {
            base.OnStop();
            sensorCore.Stop();
            circuitCore.Stop();
        }
    }
}
