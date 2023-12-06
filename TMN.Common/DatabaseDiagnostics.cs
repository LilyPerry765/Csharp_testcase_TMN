using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using Enterprise;
using System.Windows;
using System.Net.NetworkInformation;
using System.Data.SqlClient;
using System.Configuration;

namespace TMN
{
    public static class DatabaseDiagnostics
    {
        public static event Action Connected, Disconnected, ActiveConnectionChanged;

        private static string activeConnection = null; // = Properties.Settings.Default.TMNConnectionString;
        private static bool lastConnectionDisconnected;
        private static DateTime lastCheckTime;

        private static void ChangeConnection()
        {
            try
            {

				if (activeConnection == ConfigurationManager.ConnectionStrings["TMN.Properties.Settings.TMNConnectionString"].ToString())
                {
					activeConnection = ConfigurationManager.ConnectionStrings["TMN.Properties.Settings.TMNConnectionStringBack"].ToString();
                }
                else
                {
					activeConnection = ConfigurationManager.ConnectionStrings["TMN.Properties.Settings.TMNConnectionString"].ToString();
                }
                Logger.WriteImportant("OMC server changed to \"{0}\".", DatabaseDiagnostics.GetServerAddress(activeConnection));


                using (SqlConnection con = new SqlConnection(activeConnection))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "USE Master;ALTER DATABASE [TMN.t] SET PARTNER FAILOVER;";
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                OnConnectionChanged();
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
                Logger.WriteCritical("Could not swap connection.");
            }
        }

        private static void OnConnectionChanged()
        {
            try
            {
                if (ActiveConnectionChanged != null)
                {
                    Application.Current.Dispatcher.Invoke((Action)delegate()
                      {
                          ActiveConnectionChanged();
                      });
                }
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
            }
        }

        //private static bool CheckActiveConnection()
        //{
            
        //    string address = GetServerAddress(activeConnection);

        //    using (Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
        //    {
        //        try
        //        {
        //            s.Connect(address, 1433);
        //            lastCheckTime = DateTime.Now;
        //            return true;
        //        }
        //        catch //(Exception ex)
        //        {
        //            //   Logger.Write(ex);
        //        }
        //        return false;
        //    }
        //}

        private static bool isConnected;
        private static bool IsConnected
        {
            set
            {
                if (value != isConnected)
                {
                    isConnected = value;
                    if (isConnected)
                    {
                        OnConnected();
                    }
                    else
                    {
                        OnDisconnected();
                    }
                }
            }
        }

        private static void OnConnected()
        {
            if (Connected != null)
            {
                Application.Current.Dispatcher.Invoke((Action)delegate()
                     {
                         Connected();
                     });
            }
        }

        private static void OnDisconnected()
        {
            if (Disconnected != null)
            {
                Application.Current.Dispatcher.Invoke((Action)delegate()
                      {
                          Disconnected();
                      });
            }
        }

        private static bool CheckRequired
        {
            get
            {
                return (DateTime.Now - lastCheckTime).TotalSeconds > 120;
            }
        }

        #region Public Members

        public static string ServerName
        {
            get
            {
                return GetServerAddress(activeConnection);
            }
        }

        public static string GetServerAddress(string connection)
        {
            string pattern = @"Data Source=(?<Address>.+?)(\\.+)?;";
            string server = Regex.Match(connection, pattern, RegexOptions.IgnoreCase).Groups["Address"].Value;
            if (server == ".")
            {
                return "localhost";
            }
            else
            {
                return server;
            }
        }

        public static string GetConnectionString()
        {
            lock (typeof(DatabaseDiagnostics))
            {
                try
                {
                    if (CheckRequired && !CheckActiveConnection())
                    {
                        if (lastConnectionDisconnected)
                        {
                            IsConnected = false;
                        }
                        else
                        {
                            lastConnectionDisconnected = true;
                        }
                        ChangeConnection();
                    }
                    else
                    {
                        lastConnectionDisconnected = false;
                        IsConnected = true;
                    }
                }
                catch (Exception ex)
                {
                    Logger.Write(ex);
                }
                return activeConnection;
            }
        }

        #endregion

        public static bool CheckActiveConnection()
        {


            //if (!NetworkInterface.GetIsNetworkAvailable())
            //{
            //    return false;
            //}

			if (activeConnection == null)
				activeConnection = ConfigurationManager.ConnectionStrings["TMN.Properties.Settings.TMNConnectionString"].ToString();

            bool isSqlServerOnline = false;
            SqlConnectionStringBuilder conString = new SqlConnectionStringBuilder(activeConnection);

            //try
            //{
            //    using (Ping p = new Ping())
            //    {
            //        string sqlSvr = conString.DataSource;
            //        if (sqlSvr != null)
            //            isSqlServerOnline = p.Send(sqlSvr).Status == IPStatus.Success;
            //    }
            //}
            //catch (PingException)
            //{
            //    isSqlServerOnline = false;
            //}

            //if (isSqlServerOnline)
            {
                try
                {
                    conString.ConnectTimeout = 3;
                    using (SqlConnection conn = new SqlConnection(conString.ToString()))
                    {
                        conn.Open();
                        isSqlServerOnline = true;
                        lastCheckTime = DateTime.Now;
                    }
                }
                catch (Exception)
                {
                    isSqlServerOnline = false;
                    //lastCheckTime = DateTime.Now.AddSeconds(-45);
                }
            }


            return isSqlServerOnline;
        }
    }
}
