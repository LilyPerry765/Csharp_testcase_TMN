using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Reflection;

namespace TMN
{
    public class TextSettings
    {
        private static object lockFile = new object();
        private static Dictionary<string, string> results = new Dictionary<string, string>();
        private static string _settingText;
        private static string path = null;
        private static string settings
        {
            get
            {

                if (_settingText == null)
                {
                    if(path == null)
                     path = string.Format(@"{0}\Text.Settings.txt", Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
                    if (File.Exists(path))
                    {
                        lock (lockFile)
                        {
                            using (StreamReader reader = new StreamReader(path, Encoding.ASCII))
                            {
                                _settingText = reader.ReadToEnd();
                            }
                        }
                    }
                    else
                    {
                        _settingText = "";
                    }
                }
                return _settingText;
            }
            set
            {
                lock (lockFile)
                {
                    if(path == null)
                      path = string.Format(@"{0}\Text.Settings.txt", Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
                    using (StreamWriter writer = new StreamWriter(path, false))
                    {
                        writer.Write(value);
                        _settingText = value;
                    }
                }
            }
        }

        public static void Set(string name, string value)
        {
            var match = Regex.Match(settings, string.Format(@";{0}=[\w.]+;", name));
            if (match.Success)
                settings = settings.Replace(match.Value, string.Format(";{0}={1};", name, value));
            else
                settings = settings + string.Format(";{0}={1};", name, value);

            try
            {
                results[name] = value;
            }
            catch
            {
                results.Add(name, value);
            }
        }

        public static string Get(string name, string defaultValue)
        {
            try
            {
                return results[name];
            }
            catch
            {
                string result = defaultValue;
                var match = Regex.Match(settings, string.Format(@";{0}=(?<value>[\w.]+);", name));
                if (match.Success)
                    result = match.Groups["value"].Value;
                return result;
            }
        }

        public static void Delete(string name)
        {
                string temp = settings;
                var matchs = Regex.Matches(settings, string.Format(@";{0}[\S ]*?=[\w.]+;", name));
                foreach (Match match in matchs)
                {
                    temp = temp.Replace(match.Value, "");
                }
                settings = temp;
        }
    }
}
