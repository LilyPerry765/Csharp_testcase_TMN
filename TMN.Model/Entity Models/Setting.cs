using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enterprise;
using System.Transactions;

namespace TMN
{
    public partial class Setting
    {
        #region Constants
        public const string NEW_ALARM_INSERTED = "CenterNewAlarmInserted";
        public const string NEW_CRITICAL_ALARM_INSERTED = "CenterNewAlarmInsertedCritical_";
        public const string NEW_MAJOR_ALARM_INSERTED = "CenterNewAlarmInsertedMajor_";
        public const string NEW_MINOR_ALARM_INSERTED = "CenterNewAlarmInsertedMinor_";
        public const string NEW_POWER_ALARM_INSERTED = "CenterNewAlarmInsertedPower_";
        public const string NEW_SENSOR_POWER_ALARM_INSERTED = "CenterNewSensorPowerAlarmInserted_";
        public const string NEW_CIRCUIT_OPEN_ALARM_INSERTED = "CenterNewCircuitOpenAlarmInserted_";
        public const string NEW_CIRCUIT_SHORT_ALARM_INSERTED = "CenterNewCircuitShortAlarmInserted_";
        public const string OLD_CIRCUIT_OPEN_ALARM_INSERTED = "CenterOldCircuitOpenAlarmInserted_";
        public const string OLD_CIRCUIT_SHORT_ALARM_INSERTED = "CenterOldCircuitShortAlarmInserted_";

		public const string IsContactsChange = "IsContactsChange";

        public const string UPDATER_USERNAME = "UpdaterUsername";
        public const string UPDATER_PASSWORD = "UpdaterPassword";
        /// <summary>
        /// Interval for querying sensor data in milli seconds.
        /// </summary>
        public const string SENSOR_QUERY_INTERVAL = "SensorQueryInterval";
        /// <summary>
        /// Interval for saving sensor data in seconds.
        /// </summary>
        public const string SENSOR_SAVE_INTERVAL = "SensorSaveInterval";

        /// <summary>
        /// Interval for saving sensor data in seconds.
        /// </summary>
        public const string SENSOR_CHANGED = "SensorChanged_";
        
        /// <summary>
        /// Days before deleting old sensor data.
        /// </summary>
        public const string SENSOR_EXPIRE_DAYS = "SensorExpireDays";
        /// <summary>
        /// seconds before deleting old alarm data.
        /// </summary>
        public const string ALARM_EXPIRE_SECONDS = "AlarmExpireSeconds";
        /// <summary>
        /// Alarm service activity timeout in seconds.
        /// </summary>
        public const string ALARM_SERVICE_ACTIVITY_TIMEOUT = "AlarmServiceActivityTimeout";
        /// <summary>
        /// Sensor service activity timeout in seconds.
        /// </summary>
        public const string SENSOR_SERVICE_ACTIVITY_TIMEOUT = "SensorServiceActivityTimeout";
        /// <summary>
        /// Alarm query interval in seconds.
        /// </summary>
        public const string ALARM_QUERY_INTERVAL = "AlarmQueryInterval";
        /// <summary>
        /// Rregional alarm panel interval in seconds
        /// </summary>
        public const string REGIONAL_ALARM_PANEL_INTERVAL = "RegionalAlarmPanelInterval";

        /// <summary>
        /// Rregional alarm panel interval in seconds
        /// </summary>
        public const string VOICE_ALARM_INTERVAL = "VoiceAlarmInterval";

		public const string TCP_PORT_NUMBER = "2000";

        /// <summary>
        /// Rregional alarm panel interval in seconds
        /// </summary>
        //public const string DC_VISIBILITY_INTERVAL = "DisconnectVisibilityInterval";

        /// <summary>
        /// Center alarm panel interval in seconds.
        /// </summary>
        public const string CENTER_ALARM_PANEL_INTERVAL = "CenterAlarmPanelInterval";

        /// <summary>
        /// voice OF disconnection.
        /// </summary>
        public const string SOUND_ALARM_DC = "SoundAlarmDC";

        /// <summary>
        /// voice of critical alarm.
        /// </summary>
        public const string SOUND_ALARM_CRITICAL = "SoundAlarmCritical";

        /// <summary>
        /// voice of major alarm.
        /// </summary>
        public const string SOUND_ALARM_MAJOR = "SoundAlarmMajor";

        /// <summary>
        /// voice of minor alarm.
        /// </summary>
        public const string SOUND_ALARM_MINOR = "SoundAlarmMinor";

        /// <summary>
        /// voice of minor alarm.
        /// </summary>
        public const string SOUND_ALARM_POWER = "SoundAlarmPower";

        /// <summary>
        /// voice of minor alarm.
        /// </summary>
        public const string SOUND_ALARM_CABLE = "SoundAlarmCable";

		/// <summary>
		/// TMN Circuit Alarm
		/// </summary>
        public const string CIRCUIT_ALARM_SHARE_FOLDER = "CircuitAlarmShareFolder";
		public const string CIRCUIT_ALARM_SERVER = "CircuitAlarmServer";

		/// <summary>
		/// AccountCreatorService setting
		/// </summary>
        public const string ACCOUNT_CREATOR_IP = "AccountCreatorIp";
        public const string ACCOUNT_CREATOR_PORT = "AccountCreatorPort";



        /// <summary>
        /// TMN Circuit Alarm
        /// </summary>
        public const string DEFAULT_CIRCUIT_ALARM_SHARE_FOLDER = "tmn.r";

        /// <summary>
        /// AccountCreatorService setting
        /// </summary>
        public const string DEFAULT_ACCOUNT_CREATOR_PORT = "6300";

        /// <summary>
        /// Default alarm service activity timeout in seconds.
        /// </summary>
        public const int DEFAULT_ALARM_SERVICE_ACTIVITY_TIMEOUT = 60;
        /// <summary>
        /// Default timeout for sensor service activity in seconds.
        /// </summary>
        public const int DEFAULT_SENSOR_SERVICE_ACTIVITY_TIMEOUT = 15;
        /// <summary>
        /// Default interval for querying sensor data in milli seconds.
        /// </summary>
        public const int DEFAULT_SENSOR_QUERY_INTERVAL = 3000;
        /// <summary>
        /// Default interval for saving sensor data in seconds.
        /// </summary>
        public const int DEFAULT_SENSOR_SAVE_INTERVAL = 10 * 60;
        /// <summary>
        /// Default days before sensor data is deleted.
        /// </summary>
        public const int DEFAULT_SENSOR_EXPIRE_DAYS = 1;
        /// <summary>
        /// Default seconds before alarm data is deleted.
        /// </summary>
        public const int DEFAULT_ALARM_EXPIRE_SECONDS = 1 * 60 * 60;
        /// <summary>
        /// Default interval for alarm query interval in seconds
        /// </summary>
        public const int DEFAULT_ALARM_QUERY_INTERVAL = 10;
        /// <summary>
        /// Default regional alarm panel interval in seconds
        /// </summary>
        public const int DEFAULT_REGIONAL_ALARM_PANEL_INTERVAL = 3;
        /// <summary>
        /// Default center alarm panel interval in seconds.
        /// </summary>
        public const int DEFAULT_CENTER_ALARM_PANEL_INTERVAL = 3;

        /// <summary>
        /// Default regional alarm panel interval in seconds
        /// </summary>
        public const int DEFAULT_VOICE_ALARM_INTERVAL = 20;

        /// <summary>
        /// Default regional alarm panel interval in seconds
        /// </summary>
        //public const int DEFAULT_DC_VISIBILITY_INTERVAL = 5;

        /// <summary>
        /// Default SOUND alarm FOR DISCONNECTION 
        /// </summary>
        public const string DEFAULT_SOUND_ALARM_DC = @"Sounds\DC.WAV";

        /// <summary>
        /// Default sound alarm for critical
        /// </summary>
        public const string DEFAULT_SOUND_ALARM_CRITICAL = @"Sounds\CRITICAL.WAV";

        /// <summary>
        /// Default Sound Alarm for major
        /// </summary>
        public const string DEFAULT_SOUND_ALARM_MAJOR = @"Sounds\MAJOR.WAV";

        /// <summary>
        /// Default Sound Alarm For Minor
        /// </summary>
        public const string DEFAULT_SOUND_ALARM_MINOR = @"Sounds\MINOR.WAV";

        /// <summary>
        /// Default Sound Alarm For Minor
        /// </summary>
        public const string DEFAULT_SOUND_ALARM_POWER = @"Sounds\POWER.WAV";

        /// <summary>
        /// Default Sound Alarm For Minor
        /// </summary>
        public const string DEFAULT_SOUND_ALARM_CABLE = @"Sounds\CABEL.WAV";



        #endregion

        public static string UpdaterUserName
        {
            get
            {
                return Get<string>(UPDATER_USERNAME);
            }
            set
            {
                Set(UPDATER_USERNAME, value);
            }
        }

        public static string UpdaterPassword
        {
            get
            {
                return Get<string>(UPDATER_PASSWORD, null, true);
            }
            set
            {
                Set(UPDATER_PASSWORD, value, true);
            }
        }

        public static T Get<T>(string name)
        {
            return Get(name, default(T));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static T Get<T>(string name, T defaultValue)
        {
            return Get(name, defaultValue, false);
        }

        /// <summary>
        /// get setting from database
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static T GetSingle<T>(string name, T defaultValue)
        {
            return GetSingle(name, defaultValue, false);
        }

        static Dictionary<string, string> settings = new Dictionary<string, string>();
        private static void GetAll()
        {
            using (var db = new TMNModelDataContext())
            {
                settings = db.Settings.Select(s=> new{s.Name, s.Value}).ToDictionary(s => s.Name, s=> s.Value);
                lastUpdate = DateTime.Now;
                isDirty = false;
            }
        }
        private static bool isDirty { get; set; }
        private static DateTime lastUpdate { get; set; }

        public static T GetSingle<T>(string name, T defaultValue, bool decrypt)
        {
            Setting setting = null;
            try
            {
                using (var db = new TMNModelDataContext())
                {
                    setting = db.Settings.SingleOrDefault(s => s.Name == name);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
                Logger.WriteWarning("Could not get setting \"{0}\" from storage. The default value will be used.", name);
            }
            if (settings.Keys.Contains(name))
            {
                string value = decrypt ? Cryptographer.Decode(settings[name]) : settings[name];
                return (T)Convert.ChangeType(value, typeof(T));
            }
            return defaultValue;
        }

        public static T Get<T>(string name, T defaultValue, bool decrypt)
        {
            //Setting setting = null;
            try
            {
                if (settings.Count == 0 || isDirty || DateTime.Now.Subtract(lastUpdate).Minutes > 5)
                    GetAll();
                //using(var db = new TMNModelDataContext())
                //{
                //    setting = db.Settings.SingleOrDefault(s => s.Name == name);
                //}
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
                Logger.WriteWarning("Could not get setting \"{0}\" from storage. The default value will be used.", name);
            }
            if (settings.Keys.Contains(name))
            {
                string value = decrypt ? Cryptographer.Decode(settings[name]) : settings[name];
                return (T)Convert.ChangeType(value, typeof(T));
            }
            return defaultValue;
        }

        public static void Set(string name, string value, bool encrypt = false)
        {
            using (var db = new TMNModelDataContext())
            {
                var setting = db.Settings.FirstOrDefault(s => s.Name == name);
                if (setting == null)
                {
                    setting = new Setting();
                    setting.Name = name;
                    db.Settings.InsertOnSubmit(setting);
                }
                setting.Value = encrypt ? Cryptographer.Encode(value) : value;
                db.SubmitChanges();
            }
        }

        public static void IsDirty()
        {
            isDirty = true;
        }
        
    }

}
