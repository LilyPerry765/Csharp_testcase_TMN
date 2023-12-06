using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using Enterprise;
using System.Text.RegularExpressions;
using System.Security.Principal;

namespace TMN
{
    public static class Impersonation
    {
        [DllImport("advapi32.DLL", SetLastError = true)]
        private static extern int LogonUser(string lpszUsername, string lpszDomain, string lpszPassword, int dwLogonType, int dwLogonProvider, out IntPtr phToken);

        public static bool TryImpersonate(string userName, string password, string domain)
        {
            
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(domain))
            {
                Logger.WriteWarning("Impersonation information not found!");
                return false;
            }
            else
            {
                try
                {
                    Logger.WriteInfo("Impersonating user \"{0}\" on domain \"{1}\"...", userName, domain);
                    
                    IntPtr u = IntPtr.Zero;
                    int result = LogonUser(userName, domain, password, 9, 0, out u);
                    var impersonationcontext = new WindowsIdentity(u, "LogonUser", WindowsAccountType.Normal, true).Impersonate();
                    Logger.WriteInfo("Impersonation tried.");
                    // impersonationcontext.Undo();
                    return true;
                }
                catch (Exception ex)
                {
                    Logger.Write(ex);
                }
            }
            
            return false;
        }
        public static string domain = null;
        public static bool TryImpersonateByPath(string userName, string password, string pathOnRemoteMachine)
        {
            const string ipExtracterPathern = @"\\\\(?<IP>.+?)\\.*";

            
            try
            {
                var match = Regex.Match(pathOnRemoteMachine, ipExtracterPathern);
                if (match.Success && match.Groups["IP"].Success)
                    domain = Regex.Match(pathOnRemoteMachine, ipExtracterPathern).Groups["IP"].Value;
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
            }
            
            return TryImpersonate(userName, password, domain);
        }

    }
}
