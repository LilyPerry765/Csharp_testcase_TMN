using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using Enterprise;

namespace TMN
{
    class TelnetClient : IDisposable
    {
        private const int DEFAULT_PORT = 23;
        private const int BUFFER_LENGTH = 1024;

        private Socket socket = null;
        private string ip;
        private int port;

        public event EventHandler Connected;

        public TelnetClient(string ip)
            : this(ip, DEFAULT_PORT)
        {
        }

        public TelnetClient(string ip, int port)
        {
            this.ip = ip;
            this.port = port;
        }

        public bool ConnectIfNeeded()
        {
            try
            {
                if (socket == null || !socket.Connected)
                {
                    if (socket != null)
                        socket.Close();
                    Logger.WriteDebug("Connecting...");
                    socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    socket.Connect(ip, port);
                    Logger.WriteDebug("Connected.");
                    OnConnected();
                }
                return true;
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
                Logger.WriteCritical("Connection failed!");
            }
            return false;
        }

        private void OnConnected()
        {
            if (Connected != null)
            {
                Connected(this, EventArgs.Empty);
            }
        }

        public string WaitFor(params string[] lookingStrings)
        {
            return WaitFor(60 * 1000, lookingStrings);
        }

        private object telnetLock = new object();
        public string WaitFor(int timeout, params string[] lookingStrings)
        {
            //lock (telnetLock)
            {
                try
                {
                    //if (ConnectIfNeeded())
                    {
                        string result = null;
                        double waitingTime = 0;
                        bool anyStringsFound = false;
                        DateTime startTime = DateTime.Now;
                        do
                        {
                            byte[] buffer = new byte[BUFFER_LENGTH];
                            if (socket.Connected)
                                socket.Receive(buffer);
                            else
                                throw new TimeoutException("connection failed");
                            int validLength = Array.IndexOf(buffer, (byte)0);
                            if (validLength == -1) validLength = BUFFER_LENGTH;

                            string temp = ASCIIEncoding.ASCII.GetString(buffer, 0, validLength);
                            if (temp.Trim() != "")
                            {
                                result += temp;
                                startTime = DateTime.Now;
                                //Logger.WriteDebug(result);
                            }

                            anyStringsFound = lookingStrings.Any(s => result.Contains(s));
                            waitingTime = (DateTime.Now - startTime).TotalMilliseconds;
                            //Logger.WriteDebug("" + waitingTime);
                        } while (!anyStringsFound && waitingTime < timeout );
                        if (anyStringsFound)
                        {
                            return result;
                        }
                        else
                        {
                            Logger.WriteDebug(result);
                            Logger.WriteWarning("Waiting for string(s) \"{0}\" timed out after {1} milliseconds.", string.Join(" OR ", lookingStrings), (int)waitingTime);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.Write(ex);
                }
                return null;
            }
        }

        public void Send(string command)
        {
            try
            {
                //if (ConnectIfNeeded())
                if (socket.Connected)
                {
                    socket.Send(ASCIIEncoding.ASCII.GetBytes(command + "\r"));
                }
                else
                    throw new Exception("connection failed.");
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
            }
        }

        public void Dispose()
        {
            try
            {
                socket.Close();
                socket.Dispose();
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
            }
        }
    }
}
