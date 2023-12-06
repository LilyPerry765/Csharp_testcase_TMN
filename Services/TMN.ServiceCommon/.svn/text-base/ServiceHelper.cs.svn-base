using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enterprise;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using System.ServiceProcess;

namespace TMN
{
    public class ServiceHelper
    {
        public static void UpgradeService(IService service)
        {
            string assemblyDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string updaterPath = Path.Combine(assemblyDir, "TMN.Updater.exe");
            if (File.Exists(updaterPath))
            {
                Logger.WriteInfo("Starting update process...");
                var p = Process.Start(new ProcessStartInfo(updaterPath, "/silent")
                  {
                      WorkingDirectory = assemblyDir

                  });
            }
            else
            {
                Logger.WriteWarning("Updater({0}) was not found. Update failed.", updaterPath);
                service.Stop();
            }
        }


        /// <summary>
        /// Checks the given args in order to determine whether upgrade is requested.
        /// </summary>
        public static bool UpgradeRequested(string[] args)
        {
            return args.Contains("/update") || args.Contains("/upgrade") || args.Contains("/u");
        }

    }
}
