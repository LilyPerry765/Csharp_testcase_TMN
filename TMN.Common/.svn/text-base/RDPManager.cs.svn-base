using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace TMN
{
    public class RDPManager
    {

        // Get a handle to an application window.  
        [DllImport("USER32.DLL")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        // Activate an application window.  
        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("USER32.DLL")]
        public static extern IntPtr SetFocus(IntPtr hWnd);

        public string RDCTest(string machineName)
        {
            try
            {
                System.Diagnostics.Process rdcProcess = new Process();
                string strExE = Environment.ExpandEnvironmentVariables(@"%SystemRoot%\system32\mstsc.exe");

                rdcProcess.StartInfo.FileName = strExE;
                bool rslt = rdcProcess.Start();

                this.SendKey(machineName);

                return "Started Remote Desktop Connection.";

            }
            catch
            {
                throw;
            }
        }


        // Send a series of key presses to the application.  
        private void SendKey(string machineName)
        {
            System.Threading.Thread.Sleep(10000);

            IntPtr mstscHandle = FindWindow(null, "Remote Desktop Connection");

            // Verify that mstc is a running process.           

            if (mstscHandle == IntPtr.Zero)
            {
                System.Windows.Forms.MessageBox.Show("mstsc is not running.");
                return;
            }
            else
            {
                SetForegroundWindow(mstscHandle);
                SetFocus(mstscHandle);
                System.Windows.Forms.SendKeys.SendWait(machineName);
                System.Windows.Forms.SendKeys.SendWait("{ENTER}");

            }
        }



        public void RdcTest(string server, string domain, string username, string password, string centerName)
        {

            RDPPwHelper pwHelper = new RDPPwHelper();
            //string encyptedPassword = pwHelper.encryptpw("pendar");
            string encyptedPassword = pwHelper.encryptpw(password);
            //Calling the CryptUnprotectData API to encypt the password, 
            //Read this link for how to do this:
            //http://www.devnewsgroups.net/group/microsoft.public.dotnet.framework/topic21805.aspx

            string assemblyDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string filename = Path.Combine(assemblyDir, centerName + "_.rdp");


            //if (!File.Exists(filename))
            {
                using (FileStream fs = File.Create(filename))
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    File.SetAttributes(filename,  FileAttributes.Hidden );
                    sw.WriteLine("screen mode id:i:2");
                    sw.WriteLine("desktopwidth:i:800");
                    sw.WriteLine("desktopheight:i:600");
                    sw.WriteLine("disable wallpaper:i:0");
                    sw.WriteLine("session bpp:i:15");
                    sw.WriteLine("compression:i:1");
                    sw.WriteLine("authentication level:i:2");
                    sw.WriteLine("prompt for credentials:i:0");
                    sw.WriteLine("full address:s:" + server);
                    //sw.WriteLine("username:s:omc"); // + username);
                    sw.WriteLine("username:s:"+ username);
                    //sw.WriteLine("domain:s:" + domain);
                    sw.WriteLine("password 51:b:" + encyptedPassword);
                    
                }
            }
            Process rdcProcess = new Process();
            string strExE = Environment.ExpandEnvironmentVariables(@"%SystemRoot%\system32\mstsc.exe");
            rdcProcess.StartInfo.FileName = strExE;
            rdcProcess.StartInfo.Arguments = "\"" + filename + "\"";
            rdcProcess.Start();

            new System.Threading.Thread(() =>
                {
                    System.Threading.Thread.Sleep(5000);
                    File.Delete(filename);
                }).Start();

        }



    }
}
