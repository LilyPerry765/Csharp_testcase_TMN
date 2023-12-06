using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enterprise;
using System.Threading;
using System.Net.Sockets;
using System.Text.RegularExpressions;

namespace NecAlarmService
{
    public partial class Client
    {
        Telnet.Terminal terminal;

        public Client()
        {
            Connect();
        }

        public bool Connect()
        {
            bool connectResult;

            try
            {
                const string switchUser = "swsh";
                const string switchPass = "swsh123";
                terminal = new Telnet.Terminal("192.168.0.10", 23, 10, 80, 1000);
                if (terminal.Connect())
                {
                    Logger.WriteDebug("Connected to switch !!!");

                    terminal.WaitForString("Login:");
                    Logger.WriteDebug("Got 'login' phrase!");

                    terminal.SendResponse(switchUser, true);

                    string result = "";
                    //Send Password command
                    for (int i = 0; i <= 2; i++)
                    {
                        result = terminal.WaitForString("Password", false, 120) ?? "";
                        Logger.WriteDebug("waitinig for 'Password' phrase, result: {0}", result);
                        if (result.Contains("Password"))
                            break;
                    }
                    if (!result.Contains("Password"))
                        return false;
                    terminal.SendResponse(switchPass, true);

                    //send stop alarm command
                    Logger.WriteDebug("waiting for 'swsh >' for send command");
                    for (int i = 0; i <= 2; i++)
                    {
                        result = terminal.WaitForString("swsh >", false, 120) ?? "";
                        Logger.WriteDebug("result{0}: {1}", i, result);
                        if (result.Contains("swsh >"))
                            break;
                    }
                    if (!result.Contains("swsh >"))
                        return false;
                    Logger.WriteInfo("Stopping auto alarm receiving...");
                    terminal.SendResponse("stop amc almsg_no=all", true); // Stop getting alarams
                    Logger.WriteDebug("waiting for 'swsh >' for send command");
                    for (int i = 0; i <= 2; i++)
                    {
                        result = terminal.WaitForString("swsh >", false, 120) ?? "";
                        Logger.WriteDebug("result{0}: {1}", i, result);
                        if (result.Contains("swsh >"))
                            break;
                    }
                    if (!result.Contains("swsh >"))
                        return false;
                    Logger.WriteInfo("Terminal is ready.");
                    return true;
                }
                else
                {
                    Logger.WriteError("Connect to switch failed!");
                    connectResult = false;
                }
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
                connectResult = false;
            }

            return connectResult;
        }

        public void Disconnect()
        {
            terminal.Close();
            Logger.WriteInfo("Disconnected.");
        }

        public bool SendCommand(string command, string regexPatern, out string result)
        {
            try
            {
                if (!terminal.IsOpenConnection())
                {
                    Logger.WriteWarning("Terminal is closed, trying to reconnect...");
                    if (!Connect())
                    {
                        Logger.WriteError(result = "Terminal is close");
                        return false;
                    }
                }

                terminal.VirtualScreen.CleanScreen();

                terminal.SendResponse(command, true);
                // terminal.VirtualScreen.WriteLine(command);

                Logger.WriteInfo("Command \"{0}\" sent. Waiting for result...", command);

                Regex regex = new Regex(regexPatern, RegexOptions.Singleline);
                result = "";
                do
                {
                    Thread.Sleep(500);
                    result = terminal.WaitForRegEx(new Regex(".+", RegexOptions.Singleline), 120).Value.Trim();
                    //Logger.WriteDebug(result);
                } while (!regex.IsMatch(result));

                // result = terminal.WaitForString(regexPatern, false, 120);

                if (String.IsNullOrWhiteSpace(result))
                {
                    Logger.WriteWarning("Regex patern was not found");
                    return false;
                }
                else
                {
                    Logger.WriteInfo("Command result received");
                    return true;
                }
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
                result = null;
                return false;
            }



        }

        //private bool ConnectNeax(string phone, ref string message)
        //{
        //    bool connectResult;

        //    try
        //    {
        //        string command = "can subd st_ope=spsal n=" + phone.Trim();
        //        const string switchUser = "swsh";
        //        const string switchPass = "swsh123";

        //        Telnet.Terminal terminal = new Telnet.Terminal("192.168.0.10");
        //        if (terminal.Connect())
        //        {
        //            Logger.WriteDebug("Connected to switch !!!");

        //            terminal.WaitForString("Login:");
        //            Logger.WriteDebug("Got 'login' phrase!");

        //            terminal.SendResponse(switchUser, true);

        //            string result = "";
        //            //Send Password command
        //            for (int i = 0; i <= 2; i++)
        //            {
        //                result = terminal.WaitForString("Password", false, 120) ?? "";
        //                Logger.WriteDebug("waitinig for 'Password' phrase, result: {0}", result);
        //                if (result.Contains("Password"))
        //                    break;
        //            }
        //            if (!result.Contains("Password"))
        //                return false;
        //            terminal.SendResponse(switchPass, true);

        //            //send stop alarm command
        //            Logger.WriteDebug("waiting for 'swsh >' for send command");
        //            for (int i = 0; i <= 2; i++)
        //            {
        //                result = terminal.WaitForString("swsh >", false, 120) ?? "";
        //                Logger.WriteDebug("result{0}: {1}", i, result);
        //                if (result.Contains("swsh >"))
        //                    break;
        //            }
        //            if (!result.Contains("swsh >"))
        //                return false;
        //            terminal.SendResponse("stop amc almsg_no=all", true); // Stop getting alarams


        //            //send connect phone command
        //            Logger.WriteDebug("waiting for 'swsh >' for send command");
        //            for (int i = 0; i <= 2; i++)
        //            {
        //                result = terminal.WaitForString("swsh >", false, 120) ?? "";
        //                Logger.WriteDebug("result{0}: {1}", i, result);
        //                if (result.Contains("swsh >"))
        //                    break;
        //            }
        //            if (!result.Contains("swsh >"))
        //                return false;
        //            terminal.SendResponse(command, true);


        //            result = terminal.WaitForRegEx("can subd (err|end).+", 120);
        //            while (string.IsNullOrEmpty(result))
        //            {
        //                Logger.WriteWarning("Empty result");
        //                result = terminal.WaitForRegEx("can subd (err|end).+", 120);
        //            }

        //            Logger.WriteDebug("can subd result returned: '{0}'", result);
        //            string matchPattern = @"can subd (?<status>\w{3})(?:-(?<Err>\w+))?";

        //            var match = System.Text.RegularExpressions.Regex.Match(result, matchPattern);
        //            if (!match.Success)
        //            {
        //                Logger.WriteError("No Match for result!");
        //                message = "No Match for result!";
        //                connectResult = false;
        //            }
        //            else
        //            {

        //                if (match.Groups["status"].Value.ToLower().Equals("err"))
        //                {
        //                    if (match.Groups["Err"].Value.ToLower().Equals("b01"/*Already have requested state!*/))
        //                        connectResult = true;
        //                    else
        //                    {
        //                        message = result;
        //                        connectResult = false;/*Unknown error*/
        //                    }
        //                }
        //                else
        //                    connectResult = true;
        //            }

        //            terminal.Close();
        //        }
        //        else
        //        {
        //            message = "Can not connect to IP address: 192.168.0.10";
        //            Logger.WriteDebug("Connect to switch failed!");
        //            connectResult = false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        message = ex.Message;
        //        Logger.Write(ex);
        //        connectResult = false;
        //    }

        //    return connectResult;
        //}
    }
}
