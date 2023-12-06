using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enterprise;
using System.Threading;
using System.Net.Sockets;
using System.Text.RegularExpressions;

namespace TMN
{
    public class HuaweiAlarmReceiver : IDisposable
    {
        private Socket s = null;
        private string userName, password, switchType;
        public string IP;
        private int port;
        private const string LoginCommand = "LGI:OP=\"{0}\",PWD=\"{1}\";";
        private const string StopAlarmCommand = "SET CWSON: SWT=OFF;";
        private const string RequestAlarmCommand = "LST ALMLOG:;";
        private const string RequestAlarmCommandForNGNUMG = "LST ALMAF;";
        //private const string RequestAlarmCommandForNGN = "LST ALMLOG: almtp=flt, clrflg=cleared-0;";
        private const string RequestAlarmCommandForNGN = "LST ALMLOG: clrflg=cleared-0;";
        private const string RequestAlarmCommandForRecovery = "LST ALMLOG: CSN={0};";
        private const string RequestAlarmCommandForRecoveryNGN = "LST ALMLOG: SCSN={0};";
        private const short Second = 1000;
        //private const int HUAWEI_PORT = 6000;


        public bool isUMG = false;

        public HuaweiAlarmReceiver(string switchIP, string userName, string password, string switchType, int port)
        {
            this.IP = switchIP;
            this.userName = userName;
            this.password = password;
            this.switchType = switchType;
            this.port = port;
        }

        ~HuaweiAlarmReceiver()
        {
            Dispose();
        }

        private string GetOfficeName(string input)
        {
            try
            {
                string pattern = @"\r\n\+{3}\s+(?<OfficeName>.+?)\s+\d+-\d+-\d+";
                var match = Regex.Match(input, pattern);
                if (match.Success)
                {
                    return match.Groups["OfficeName"].Value;
                }
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
            }
            return null;
        }

        public  bool Login()
        {
            try
            {
                Logger.WriteInfo("Logging in to {0}...", IP);
                string cmdResult;
                if (SendCommand(string.Format(LoginCommand, userName, password), out cmdResult))
                {
                    bool loginResult;
                    if (cmdResult.ToLower().Contains("invalid account. Login failed".ToLower()) || cmdResult.ToLower().Contains("Invalid username or password".ToLower()))
                    {
                        Logger.WriteCritical("Login faild! Invalid Username \"{0}\". \n {1}", userName, cmdResult);
                        ServiceState.ReportActivity(ServiceTypes.AlarmService,"رمز عبور یا نام کاربری درست وارد نشده است", cmdResult);

                        loginResult = false;
                    }
                    else if (cmdResult.ToLower().Contains("password is incorrect. Login failed".ToLower()))
                    {
                        Logger.WriteCritical("Login faild! Invalid Password. \n {0}", cmdResult);
                        ServiceState.ReportActivity(ServiceTypes.AlarmService, "رمز عبور درست وارد نشده است", cmdResult);

                        loginResult = false;
                    }
                    else if (cmdResult.ToLower().Contains("login successfully") || cmdResult.ToLower().Contains("logged in successfully") || cmdResult.ToLower().Contains("accomplished"))
                    {
                        var officeName = GetOfficeName(cmdResult);
                        Logger.WriteInfo("User \"{0}\" logged in successfully{1}. \n {2}", userName, officeName != null ? " to \"" + officeName + "\" " : "", cmdResult);
                        StopAutoSendingAlarms();
                        loginResult = true;
                    }
                    else if (cmdResult.ToLower().Contains("Logout first before you login again".ToLower()))
                    {
                        Logger.WriteCritical("The user \"{0}\" is already logged in. Maybe it has not enough permission. \n {1}", userName, cmdResult);
                        ServiceState.ReportActivity(ServiceTypes.AlarmService, "کاربر احتمالا مجوز  دسترسی به سوییچ را ندارد ", cmdResult);

                        loginResult = false;
                    }
                    else
                    {
                        Logger.WriteCritical("Unknown result received:\n{0}", cmdResult);
                        ServiceState.ReportActivity(ServiceTypes.AlarmService, "دلیل نا مشخص ...", cmdResult);

                        loginResult = false;
                    }

                    //if (loginResult == false)
                    //    ServiceState.ReportActivity(ServiceTypes.AlarmService, "اشکلال در Login به سوييچ", cmdResult);

                    return loginResult;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
                return false;
            }

        }

        private void StopAutoSendingAlarms()
        {
            Logger.WriteInfo("Stopping auto sending alarms...");
            string cmdResult;
            SendCommand(StopAlarmCommand, out cmdResult);
        }

        public string RequestActiveAlarms()
        {
            try
            {
                //string cmd = (switchType != "NGNUMG") ? (switchType != "NGN") ?  RequestAlarmCommand : RequestAlarmCommandForNGN : RequestAlarmCommandForNGNUMG;
                string cmd = (switchType == "NGN") ? isUMG ? RequestAlarmCommandForNGNUMG : RequestAlarmCommandForNGN : RequestAlarmCommand;
                Logger.WriteInfo("Requesting alarms list of {0}...", IP);
                string result;
                if (SendCommand(cmd, out result))
                {
                    if (result.ToLower().Contains("The Operator has no authority".ToLower()) || result.ToLower().Contains("User has not logged in".ToLower()) || result.ToLower().Contains("Insufficient authority".ToLower()))
                    {
                        Logger.WriteInfo("The user is not logged in!");
                        if (Login())
                        {
                            // The user is logged in for sure now
                            if (SendCommand(cmd, out result))
                            {
                                if (result.ToLower().Contains("The Operator has no authority".ToLower()) || result.ToLower().Contains(" has been offline due to timeout".ToLower()) || result.ToLower().Contains("Insufficient authority".ToLower()))
                                {
                                    Logger.WriteWarning("The user has not enough permission.\n {0}", result);

                                    ServiceState.ReportActivity(ServiceTypes.AlarmService, "کاربر احتمالا مجوز  دسترسی به سوییچ را ندارد!");
                                    //ServiceState.ReportActivity(ServiceTypes.AlarmService, "کاربر دسترسی لازم را ندارد.");
                                }
                                else
                                {
                                    // User is logged in and command result is alarm data
                                    return result;
                                }
                            }
                        }
                    }
                    else
                    {
                        // User is logged in and command result is alarm data
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
            }
            return null;
        }

        public string RequestAlarmRecovery(int alarmID)
        {
            try
            {
                Logger.WriteInfo("Requesting alarm recovery in {0}...", IP);
                string result;
                string cmd = (switchType == "NGN") ? RequestAlarmCommandForRecoveryNGN : RequestAlarmCommandForRecovery;
                if (SendCommand(string.Format(cmd, alarmID), out result))
                {
                    if (result.ToLower().Contains("The Operator has no authority".ToLower()))
                    {
                        Logger.WriteWarning("The user is not logged in!");
                        if (Login())
                        {
                            // The user is logged in for sure now
                            if (SendCommand(string.Format(RequestAlarmCommandForRecovery, alarmID), out result))
                            {
                                if (result.ToLower().Contains("The Operator has no authority".ToLower()))
                                {
                                    // The user is logged in but has not enough permission!
                                    Logger.WriteWarning("The user has not enough permission.");
                                    ServiceState.ReportActivity(ServiceTypes.AlarmService, "کاربر احتمالا مجوز  دسترسی به سوییچ را ندارد! ");
                                }
                                else
                                {
                                    // User is logged in and command result is alarm data
                                    return result;
                                }
                            }
                        }
                    }
                    else
                    {
                        // User is logged in and command result is alarm data
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
            }
            return null;
        }

        private bool GarantConnection()
        {
            try
            {
                if (s == null || !s.Connected)
                {
                    Logger.WriteDebug("Connecting {0}...", IP);
                    if (s != null)
                        s.Close();
                    s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
                    {
                        SendTimeout = 60 * Second,
                        ReceiveTimeout = 60 * Second
                    };
                    s.Connect(IP, port);
                }
                return true;
            }
            catch (System.Security.SecurityException ex)
            {
                Logger.Write(ex);
                ServiceState.ReportActivity(ServiceTypes.AlarmService, "عدم دسترسی . ممکن است مجوز ورود نداشته باشید", ex.Message);

                return false;
            }
            catch (System.ArgumentOutOfRangeException ex)
            {
                Logger.Write(ex);
                ServiceState.ReportActivity(ServiceTypes.AlarmService, "شماره پورت مجاز نیست", ex.Message);

                return false;
            }
            catch (System.ArgumentException ex)
            {
                Logger.Write(ex);
                ServiceState.ReportActivity(ServiceTypes.AlarmService, "شماره پورت یا آدرس IP درست وارد نشده است ", ex.Message);

                return false;
            }
            catch (System.NotSupportedException ex)
            {
                Logger.Write(ex);
                ServiceState.ReportActivity(ServiceTypes.AlarmService, "شماره پورت یا آدرس IP درست وارد نشده است ", ex.Message);

                return false;
            }
            catch (System.Net.Sockets.SocketException ex)
            {
                Logger.Write(ex);
                ServiceState.ReportActivity(ServiceTypes.AlarmService, "شماره پورت یا آدرس IP درست وارد نشده است ", ex.Message);

                return false;
            }
            catch (System.ObjectDisposedException ex)
            {
                Logger.Write(ex);
                ServiceState.ReportActivity(ServiceTypes.AlarmService, "اتصال بسته شده است", ex.Message);

                return false;
            }
            catch (System.InvalidOperationException ex)
            {
                Logger.Write(ex);
                ServiceState.ReportActivity(ServiceTypes.AlarmService, "مشکل در اتصال به سوييچ", ex.Message);

                return false;
            }
            catch (Exception ex)
            {
                Logger.Write(ex);

                if (ex.Message.ToLower().Contains("unreachable network"))
                {
                    ServiceState.ReportActivity(ServiceTypes.AlarmService, "ارتباط شبکه ای با سوييچ قطع است.", ex.Message);
                }
                else
                {
                    ServiceState.ReportActivity(ServiceTypes.AlarmService, "مشکل در اتصال به سوييچ", ex.Message);
                }

                return false;
            }
        }

        private bool SendCommand(string command, out string result )
        {
            try
            {
                if (GarantConnection())
                {
                    result = string.Empty;
                    byte[] cmd = System.Text.Encoding.ASCII.GetBytes(command);
                    // Hiding password in log :D
                    Logger.WriteDebug("Sending command...\n{0}", Regex.Replace(command, @"PWD="".*""", "PWD=\"***\""));
                    s.Send(cmd);
                    Logger.WriteDebug("Receiving switch response...");
                    int cnt = 0;
                    do
                    {
                        Thread.Sleep(500);
                        cnt++;
                        byte[] responseBuffer = new byte[8 * 1024];
                        s.Receive(responseBuffer);
                        int endOfData = Array.IndexOf(responseBuffer, (byte)0);
                        if (endOfData == -1)
                        {
                            endOfData = responseBuffer.Length;
                        }
                        result += System.Text.Encoding.ASCII.GetString(responseBuffer, 0, endOfData);
                    } while (!result.Trim().EndsWith("---    END") && cnt < 20);
                    if (cnt < 20)
                    {
                        // Logger.WriteDebug("Iterated {0} times.", cnt);
                    }
                    else
                    {
                        Logger.WriteWarning("No response received within {0} tries. Receiving aborted.", cnt);
                        return false;
                    }
                    //  Logger.WriteDebug(result);
                    return true;
                }
                result = "Could not connect.";
                return false;

            }
            catch (Exception ex)
            {
                Logger.Write(ex);
                string lowerMessage = ex.Message.ToLower();
                if (lowerMessage.Contains(" connection was aborted by the software in your host machine".ToLower()) || lowerMessage.Contains("existing connection was forcibly closed by the remote host".ToLower()))
                {
                    //Retry Command
                    return SendCommand(command, out result);
                }
                else
                {
                    result = null;
                    return false;
                }
            }
        }

        public void Dispose()
        {
            try
            {
                if (s != null)
                    s.Close();
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
            }
        }

        internal string RequestActiveAlarmsForTest()
        {
            Logger.WriteImportant("YOU ARE IN TEST MODE! NO ALARMS ACTUALLY RECEIVED!");
            StringBuilder b = new StringBuilder();
            const string str = "ALARM  {0}     Fault  Major alarm    Exchange  567     Hardware system \r\n Alarm occurrence time  =  {1:yyyy-MM-dd HH:mm:ss}\n  Function sub-system  =  Transmission system \r\n  A/B indication  =  A  \r\n   Module number  =  0  \r\n      Alarm name  =  TEST ALARM{0} \r\n Location information  =  PlaceNo=0 RowNo=0 ColNo=4 FrameNo=5 SlotNo=7 CardNo=21  E1OnCardNo=10 ConnectedModuleNo=1 OFCNo=255 OFCName=<NULL> TkGroupNo=217 TkGroupTitle=outdoor-3  \r\n    Other information  =  No details available.\r\n      Recovery advice  =  This is a test alarm.\r\n\r\n"; ;
            for (int i = 0; i < 1000; i++)
            {
                b.Append(string.Format(str, i, DateTime.Now));
            }
            return b.ToString();
        }
    }
}
