using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Net;

namespace TMN
{
	public delegate void TCPServerReceiveMessageEventHandler(string message);
	public delegate void TCPServerAcceptClientEventHandler(TcpClient client);

	public class TCPServer
	{
		#region Variables

		private TcpListener tcpListener;
		private Thread listenThread;
		private Thread clientsThread;

		#endregion

		#region Events

		public event TCPServerReceiveMessageEventHandler OnReceiveMessage;
		public event TCPServerAcceptClientEventHandler OnAcceptClient;

		#endregion

		#region Constrator

		public TCPServer()
		{
		}

		public TCPServer(int port)
		{
			this.Port = port;
		}

		#endregion

		#region Public Methods

		public void Listen()
		{
			tcpListener = new TcpListener(IPAddress.Any, this.Port);

			listenThread = new Thread(new ThreadStart(ListenForClients));
			listenThread.IsBackground = true;
			listenThread.Start();
		}

		public void SendMessage(string message, object goal)
		{
			TcpClient tcp = (TcpClient)goal;
			NetworkStream ns = tcp.GetStream();
			byte[] buffer = Encoding.ASCII.GetBytes(message);
			ns.Write(buffer, 0, buffer.Length);
			ns.Flush();
		}

		public void Stop()
		{
			TCPServer.IsPortBusy = false;

			tcpListener.Stop();
			listenThread.Abort();
			if (clientsThread != null)
				clientsThread.Abort();
		}

		#endregion

		#region Private Methods

		private void ListenForClients()
		{
			try
			{
				tcpListener.Start();

				TCPServer.IsPortBusy = true;

				while (true)
				{
					TcpClient tcpClient = tcpListener.AcceptTcpClient();

					OnAcceptClient(tcpClient);

					clientsThread = new Thread(new ParameterizedThreadStart(HandleForReceive));
					clientsThread.IsBackground = true;
					clientsThread.Start(tcpClient);
				}
			}
			catch (Exception)
			{
				TCPServer.IsPortBusy = false;
			}
		}

		private void HandleForReceive(object client)
		{
			try
			{
				TcpClient tcp = (TcpClient)client;
				NetworkStream ns = tcp.GetStream();
				byte[] buffer = new byte[1024];
				int receiveCount;
				string data;

				while (true)
				{
					receiveCount = 0;
					data = string.Empty;

					receiveCount = ns.Read(buffer, 0, buffer.Length);

					if (receiveCount > 0)
					{
						data = Encoding.ASCII.GetString(buffer, 0, receiveCount);

						OnReceiveMessage(data);
					}
				}
			}
			catch (Exception)
			{
				Stop();
			}
		}

		#endregion

		#region Properties

		public int Port
		{
			get;
			set;
		}

		public static bool IsPortBusy
		{
			get;
			set;
		}

		#endregion
	}
}
