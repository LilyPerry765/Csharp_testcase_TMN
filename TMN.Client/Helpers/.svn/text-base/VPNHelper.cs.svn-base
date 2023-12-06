using System;
using System.Collections.Generic;
using System.Net;
using Enterprise;
using TMN.VPNHandler;

namespace TMN
{
    public class VPNHelper
    {
        RasHandle handle = null;
        RasPhoneBook phone = new RasPhoneBook();
        RasDialer dial = new RasDialer();

        public VPNHelper()
        {
        }

        public void Create(string name, string ip)
        {
            phone.Open();

            RasEntry entry = RasEntry.CreateVpnEntry(name, ip, RasVpnStrategy.PptpFirst,
                RasDevice.GetDeviceByName("(PPTP)", RasDeviceType.Vpn), false);

            phone.Entries.Add(entry);
        }

        public bool  Connect(string name, string username, string password)
        {
            dial.EntryName = name;
            dial.PhoneBookPath = RasPhoneBook.GetPhoneBookPath(RasPhoneBookType.AllUsers);

            try
            {
                dial.Credentials = new NetworkCredential(username, password);
                handle = dial.DialAsync();
                return true;

            }
            catch (Exception)
            {
                Logger.Write(LogType.Error, string.Format("cannot connect to ({0}) VPN ", dial.EntryName));
                return false;
            }
        }

        public void  Disconnect(string name)
        {
            if (dial.IsBusy)
            {
                // The connection attempt has not been completed, cancel the attempt.
                dial.DialAsyncCancel();
            }
            else
            {
                // The connection attempt has completed, attempt to find the connection in the active connections.
                RasConnection connection = RasConnection.GetActiveConnectionByName(name, RasPhoneBook.GetPhoneBookPath(RasPhoneBookType.AllUsers).ToString());
                if (connection != null)
                {
                    // The connection has been found, disconnect it.
                    connection.HangUp();
                }
            }
        }

        public void DisconnectAll()
        {
            RasPhoneBook p = new RasPhoneBook();
            p.Open();
            foreach (RasEntry entry in p.Entries)
            {
                Disconnect(entry.Name);
            }
        }

        public bool hasVPN(string name)
        {
            RasPhoneBook p = new RasPhoneBook();
            p.Open();
            foreach (RasEntry entry in p.Entries)
            {
                if (entry.Name == name)
                    return true;
            }
            return false;
        }

        public bool IsConnect(string name)
        {
            List<string> vpnList = GetOpenVPNs();
            foreach (string item in vpnList)
            {
                if (item == name)
                    return true;
            }
            return false;
        }

        private List<string> GetOpenVPNs()
        {
            List<string> list = new List<string>();
            foreach (RasConnection connection in RasConnection.GetActiveConnections())
            {
                list.Add(connection.EntryName);
            }
            return list;
        }

        private List<string> GetAllVPNs()
        {
            List<string> list = new List<string>();
            RasPhoneBook p = new RasPhoneBook();
            p.Open();
            foreach (RasEntry entry in p.Entries)
            {
                list.Add(entry.Name);
            }
            return list;
        }
    }
}
