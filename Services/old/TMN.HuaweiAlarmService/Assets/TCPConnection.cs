using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using Enterprise;


namespace TMN
{
    public class TCPConnection
    {
        #region Fields
        public Socket clientSocket;
        private byte[] byteData = new byte[8192];
        private IPEndPoint serverEP;
        public event MHandler Recieve;
        public event NetEvents SendError;
        public event NetEvents RecieveError;
        public event NetEvents ConnectError;
        public event NetEvents evConnected;
        public int port;
        public IPAddress ipaddress;
        public bool Trytoreconnect = true;
        public bool Connected
        {
            get
            {
                return clientSocket.Connected;
            }
        }
        #endregion

        #region Constructor
        public TCPConnection()
        {


        }
        #endregion

        #region Methods

        public void Disconnect()
        {
            if (clientSocket.Connected)
            {
                clientSocket.Disconnect(true);
            }
        }

        public void Connect(IPAddress IP, int Port)
        {
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverEP = new IPEndPoint(IP, Port);
            ipaddress = IP;
            port = Port;

            clientSocket.BeginConnect(serverEP, new AsyncCallback(eConnect), clientSocket);
            byteData = new byte[2048];
        }

        public void Send(string Command)
        {
            try
            {
                if (clientSocket.Connected)
                {
                    byte[] DataCommand = System.Text.Encoding.ASCII.GetBytes(Command);
                    clientSocket.BeginSend(DataCommand, 0, DataCommand.Length, SocketFlags.None, new AsyncCallback(eSend), clientSocket);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(ex, "Disconnect has occured");
                //error for disconnection
            }
        }
        #endregion

        #region SendProcess
        private void eConnect(IAsyncResult iar)
        {
            try
            {
                Socket remote = (Socket)iar.AsyncState;
                remote.EndConnect(iar);
                //Start Receiving data asynchronously
                clientSocket.BeginReceive(byteData,
                                          0,
                                          byteData.Length,
                                          SocketFlags.None,
                                          new AsyncCallback(eReceive),
                                          clientSocket);
                if (evConnected != null)
                {
                    evConnected(this);
                }
            }
            catch
            {
                if (ConnectError != null)
                {
                    ConnectError(this);
                }
            }
        }

        private void eSend(IAsyncResult iar)
        {
            try
            {
                Socket remote = (Socket)iar.AsyncState;
                int sent = remote.EndSend(iar);
            }
            catch (SocketException)
            {
                if (SendError != null)
                {
                    SendError(this);
                }
            }
        }

        private void eReceive(IAsyncResult iar)
        {
            try
            {
                Socket remote = (Socket)iar.AsyncState;
                int recv = remote.EndReceive(iar);

                if (recv > 0)
                {
                    string msg = System.Text.Encoding.ASCII.GetString(byteData, 0, recv);
                    Recieve(msg);
                    byteData = new byte[1024];
                }
                clientSocket.BeginReceive(byteData, 0, byteData.Length, SocketFlags.None, new AsyncCallback(eReceive), remote);

            }
            catch (SocketException)
            {
                if (RecieveError != null)
                {
                    RecieveError(this);
                }
                // when disconnect occurs
                clientSocket.Close();
                //connectin recovery procedure
                if (Trytoreconnect)
                {
                    while (!clientSocket.Connected)
                    {
                        Connect(ipaddress, port);
                        System.Threading.Thread.Sleep(20000);
                    }
                }

            }
        }
        #endregion

        #region Destructor
        ~TCPConnection()
        {
            clientSocket.Close();
        }
        #endregion
    }
}
