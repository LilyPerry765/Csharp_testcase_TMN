using System;
using System.Collections.Generic;
using System.Management;
using Enterprise;
using System.Linq;

namespace TMN
{
    public static class LicenseManager
    {
        public static List<string> GetLicenses(bool encoded = true)
        {

            List<string> addresses = new List<string>();
            for (int macDeviceId = 1; macDeviceId <= 9; macDeviceId++)
            {
                try
                {
                    string path = String.Format("win32_networkadapter.deviceid='{0}'", macDeviceId);
                    ManagementObject networkAdp = new ManagementObject(path);
                    networkAdp.Get();
                    object mm = networkAdp["MACAddress"];
                    if (mm != null)
                    {
                        if (encoded)
                            addresses.Add(Cryptographer.Encode(mm.ToString()).Trim());
                        else
                            addresses.Add(mm.ToString());
                    }
                }
                catch
                {
                }
            }
            if (!addresses.Any(s => s != ""))
                addresses = GetLicenses2(encoded);
            return addresses;
        }

        private static List<string> GetLicenses2(bool encoded = true)
        {
            List<string> addresses = new List<string>();
            ManagementClass oMClass = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection colMObj = oMClass.GetInstances();
            foreach (ManagementObject objMO in colMObj)
            {
                object mm = objMO["MACAddress"];
                if (mm != null)
                {
                    if (encoded)
                        addresses.Add(Cryptographer.Encode(mm.ToString()).Trim());
                    else
                        addresses.Add(mm.ToString());
                }
            }
            return addresses;
        }
        
        public static string GetAccessCode()
        {
            List<string> addresses = GetLicenses(false);

            return addresses.FirstOrDefault(s => s != "");
        }

        public static string GetLicense(string accessCode)
        {
            return Cryptographer.Encode(accessCode.ToString()).Trim();
        }

        public static string GetLicense()
        {
            var validLicenses = LicenseManager.GetLicenses();
            if (validLicenses.Any(s=> s != ""))
                validLicenses = LicenseManager.GetLicenses2();

            return validLicenses.FirstOrDefault(s => s != "");
        }

        public static bool? IsLicenseValid(string license)
        {
            var validLicenses = LicenseManager.GetLicenses();
            if (validLicenses.Any(s => s != ""))
                validLicenses = LicenseManager.GetLicenses2();

            return (validLicenses.Any(s => s == license.Trim()));
        }
    }
}
