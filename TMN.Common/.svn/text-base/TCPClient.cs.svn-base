using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using Enterprise;

namespace TMN
{
	public delegate void TCPClientReceiveMessageEventHandler(string message);

	public class TCPClient
	{
		#region Variables

		private TcpClient tcpClient;
		private Thread receiveThread;

		#endregion

		#region Events

		public event TCPClientReceiveMessageEventHandler OnReceiveMessage;

		#endregion

		#region Constrator

		public TCPClient()
		{
		}

		public TCPClient(string ip, int port)
		{
			this.IP = ip;
			this.Port = port;
		}

		#endregion

		#region Public Methods

		public void Connect()
		{
			try
			{
				tcpClient = new TcpClient();
				tcpClient.Connect(this.IP, this.Port);

				this.IsConnected = true;

				receiveThread = new Thread(new ThreadStart(HandleForReceive));
				receiveThread.IsBackground = true;
				receiveThread.Start();
			}
			catch (Exception)
			{
				this.IsConnected = false;
			}
		}

		public void SendMessage(string message)
		{
			NetworkStream ns = tcpClient.GetStream();
			byte[] buffer = Encoding.ASCII.GetBytes(message);
			ns.Write(buffer, 0, buffer.Length);
			ns.Flush();
		}

		public void Disconnect()
		{
			this.IsConnected = false;

			tcpClient.Close();
			receiveThread.Abort();
		}

		#endregion

		#region Private Methods

		private void HandleForReceive()
		{
			try
			{
				NetworkStream ns = tcpClient.GetStream();
				byte[] buffer = new byte[1024];
				int receiveCount;
				string message;

				while (true)
				{
					receiveCount = 0;
					message = string.Empty;

					receiveCount = ns.Read(buffer, 0, buffer.Length);
					if (receiveCount > 0)
					{
						message = Encoding.ASCII.GetString(buffer, 0, receiveCount);

						OnReceiveMessage(message);
					}
				}
			}
			catch (Exception)
			{
				Disconnect();
			}
		}

		#endregion

		#region Properties

		public bool IsConnected
		{
			get;
			set;
		}

		public int Port
		{
			get;
			set;
		}

		public string IP
		{
			get;
			set;
		}

		#endregion
	}
}

