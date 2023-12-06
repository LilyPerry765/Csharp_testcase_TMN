using System;
using Microsoft.Win32;
using System.Reflection;
using Enterprise;

namespace TMN
{
    public static class RegSettings
    {
        // Application name must not be dynamic because this file is referenced as a link in other projects of this solution and some of their settings are shared.
        private const string ApplicationName = "TMN";

        public static object Get(string key)
        {
            return Get(key, null);
        }

        public static object Get(string key, object defaultValue)
        {
            try
            {
                return Registry.LocalMachine.CreateSubKey(string.Format("Software\\{0}", ApplicationName), RegistryKeyPermissionCheck.ReadSubTree).GetValue(key, defaultValue);
            }
            catch (Exception ex)
            {
                Logger.Write(ex, "Problem getting settings from registry.");
            }
            return null;
        }

        public static void Save(string key, object value)
        {
            Registry.LocalMachine.CreateSubKey(string.Format("Software\\{0}", ApplicationName), RegistryKeyPermissionCheck.ReadWriteSubTree).SetValue(key, value);
        }

        public static void Delete(string key)
        {
            try
            {
                Registry.LocalMachine.OpenSubKey(string.Format("Software\\{0}", ApplicationName), RegistryKeyPermissionCheck.ReadWriteSubTree).DeleteValue(key, false);
            }
            catch (Exception ex)
            {
                Logger.Write(LogType.Warning, ex);
            }
        }
    }
}
