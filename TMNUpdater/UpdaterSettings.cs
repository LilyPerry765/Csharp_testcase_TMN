using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using Enterprise;

namespace TMN
{
    public class UpdaterSettings
    {
        private static System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(UpdaterSettings));
        private static string storagePath = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "UpdateSettings.xml");

        private UpdaterSettings()
        {
        }

        private static UpdaterSettings instance;
        public static UpdaterSettings Default
        {
            get
            {
                if (instance == null)
                {
                    try
                    {
                        instance = Deserialize();
                    }
                    catch (Exception ex)
                    {
                        Logger.Write(ex);
                    }
                }
                return instance;
            }
        }

        public string UpdateUrl
        {
            get;
            set;
        }

        public string[] Services
        {
            get;
            set;
        }

        public string[] Files
        {
            get;
            set;
        }

        public string[] Forced
        {
            get;
            set;
        }

        public void Save()
        {
            Serialize();
        }

        private void Serialize()
        {
            using (StreamWriter writer = new StreamWriter(storagePath))
            {
                serializer.Serialize(writer, this);
            }
        }

        private static UpdaterSettings Deserialize()
        {
            using (StreamReader reader = new StreamReader(storagePath))
            {
                return (UpdaterSettings)serializer.Deserialize(reader);
            }
        }
    }
}
