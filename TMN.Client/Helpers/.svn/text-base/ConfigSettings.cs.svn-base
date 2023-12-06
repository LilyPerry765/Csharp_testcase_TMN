using System.Configuration;

namespace TMN.Helpers
{
    public static class ConfigSettings
    {

        public static string Get(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        public static string Get(string key, string defaultValue)
        {
            return Get(key) ?? defaultValue;
        }

        public static void Save(string key, string value)
        {
            ConfigurationManager.AppSettings.Add(key, value);
          //  ConfigurationManager.AppSettings.Set(key, value);
            Properties.Settings.Default.
        }

        public static void Delete(string key)
        {
            ConfigurationManager.AppSettings.Remove(key);
        }
    }
}
