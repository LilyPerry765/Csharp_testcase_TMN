using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Timers;
using Enterprise;
using TMN;
using System.Text;

namespace ZTEAlarmService
{
	class ServiceCore : TmnService
	{
		Timer timer = null;
		DateTime? lastAlarmDate;
		List<string> newMessages = new List<string>();

		private void InitializeTimer()
		{
			try
			{
				string ip = (string)RegSettings.Get("ZTESqlServerIP");

				if (CheckConnection(ip))
				{
					timer = new Timer(AlarmQueryInterval);
					timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
					timer.Start();
					Logger.WriteInfo("Alarms will be checked about every {0} seconds.", AlarmQueryInterval);
				}
			}
			catch (Exception ex)
			{

				Logger.Write(ex);
			}
		}

		void timer_Elapsed(object sender, ElapsedEventArgs e)
		{
			timer.Stop();
			try
			{
				string ip = (string)RegSettings.Get("ZTESqlServerIP");
				if (CheckConnection(ip))
				{
					Logger.WriteInfo("Ping OK . Connect to {0}", Center.Current.Name);
					CollectAlarms();
				}
				else
				{
					ServiceState.ReportActivity(ServiceTypes.AlarmService, "مشکل درارتباط شبکه ای با سوییچ", "Could not ping switch!");
				}

			}
			catch (Exception ex)
			{
				Logger.Write(ex);
				ServiceState.ReportActivity(ServiceTypes.AlarmService, "مشکل در سرويس", ex.Message);
			}
			timer.Start();
		}

		public override void Start()
		{
			Logger.WriteEnd("ZTE Alarm Service starting.");
			SaveActiveAlarms();
			InitializeTimer();
			Logger.WriteEnd("ZTE Alarm Service started");
		}

		public override void Stop()
		{
			if (timer != null)
			{
				Logger.WriteEnd("ZTE Alarm Service stopping.");
				SaveActiveAlarms();
				timer.Stop();
				Logger.WriteEnd("ZTE Alarm Service stopped.");
			}
		}

		private bool CheckConnection(string ip)
		{
			Ping ping = new Ping();
			PingReply result = ping.Send(IPAddress.Parse(ip));
			if (result.Status == IPStatus.Success)
				return true;
			else
				return false;
		}

		public void CollectAlarms()
		{
			try
			{
				int insertCount = 0;
				int recoveryCount = 0;

				ServiceState.ReportActivity(ServiceTypes.AlarmService);

				using (var db = new TMN.TMNModelDataContext())
				{
					if (lastAlarmDate == null)
					{
						int count = db.LogAlarms.Where(la => la.CenterID == Center.CurrentCenterID).Count();
						if (count > 0)
							lastAlarmDate = db.LogAlarms.Where(la => la.CenterID == Center.CurrentCenterID).Max(la => la.Time);
						else
							lastAlarmDate = DateTime.Now;
					}
				}

				if (lastAlarmDate != null)
				{
					Logger.WriteInfo("Receiving alarms...");
					List<ZTEMessage> messages = GetMessageByDate(lastAlarmDate);
					if (messages.Count() > 0)
						lastAlarmDate = messages.Max(m => DateTime.Parse(m.StartTime.Replace(',', ' ')));

					foreach (var message in messages)
					{
						insertCount++;
						SaveAlarm(message);
						newMessages.Add(message.AID.ToString());
					}
					Logger.WriteInfo("{0} insert record", insertCount);


					Logger.WriteInfo("Recovering alarms...");
					List<ZTEMessage> recoveryList = FindRecoveredMessage(newMessages);
					if (recoveryList != null && recoveryList.Count > 0)
					{
						foreach (ZTEMessage r in recoveryList)
						{
							recoveryCount++;

							StringBuilder sb = new StringBuilder();
							sb.AppendFormat("EndTime : {0}\n", r.EndTime);
							sb.AppendFormat("Content : {0}\n", r.Content);

							MarkAlarmAsRecovered(r.AID, sb.ToString());
							newMessages.Remove(r.AID.ToString());
						}
					}
					Logger.WriteDebug("{0} messages recoverd.", recoveryCount);
				}

			}
			catch (Exception ex)
			{
				Logger.Write(ex);
				ServiceState.ReportActivity(ServiceTypes.AlarmService, "مشکل در سرويس", ex.Message);
			}
		}

		private List<ZTEMessage> GetMessageByDate(DateTime? startDate)
		{
			string ip = (string)RegSettings.Get("ZTESqlServerIP");
			string userName = (string)RegSettings.Get("ZTESqlServerUser");
			string password = (string)RegSettings.Get("ZTESqlServerPass");

			string query = string.Format("SELECT * FROM [viewCurAlarm] WHERE [StartTime] > '{0}'", startDate.Value.ToString("yyy/MM/dd,HH:mm:ss"));
			if (IsWinAuthentication)
			{
				string connection = string.Format(@"Data Source={0};Initial Catalog=ALMSYS;Integrated Security=True", ip);
				return ZTEMessage.GetNewMessage(connection, query);
			}
			else
			{
				string connection = string.Format(@"Data Source={0};Initial Catalog=ALMSYS;User ID={1};Password={2}", ip, userName, password);
				return ZTEMessage.GetNewMessage(connection, query);
			}
		}

		private List<ZTEMessage> FindRecoveredMessage(List<string> messages)
		{
			try
			{
				if (messages.Count > 0)
				{
					string value = string.Empty;
					foreach (string item in messages)
						value += "," + item;

					if (value.Length > 0)
					{
						value = value.Substring(1);

						string ip = (string)RegSettings.Get("ZTESqlServerIP");
						string userName = (string)RegSettings.Get("ZTESqlServerUser");
						string password = (string)RegSettings.Get("ZTESqlServerPass");

						string query = string.Format("IF EXISTS ( SELECT [Aid] FROM [viewHistoryAlarm] WHERE [Aid] IN ({0}) )  SELECT * FROM [viewHistoryAlarm] WHERE [Aid] IN ({0}) ", value);
						if (IsWinAuthentication)
						{
							string connection = string.Format(@"Data Source={0};Initial Catalog=ALMSYS;Integrated Security=True", ip);
							return ZTEMessage.GetRecoveryMessage(connection, query);
						}
						else
						{
							string connection = string.Format(@"Data Source={0};Initial Catalog=ALMSYS;User ID={1};Password={2}", ip, userName, password);
							return ZTEMessage.GetRecoveryMessage(connection, query);
						}
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Write(ex);
			}
			return null;
		}

		private void SaveAlarm(ZTEMessage message)
		{

			StringBuilder sb = new StringBuilder();
			sb.AppendFormat("Alarm : {0}\n", message.AID);
			sb.AppendFormat("Title : {0}\n", message.CodeTitle);
			sb.AppendFormat("Severity : {0}\n", message.State);
			sb.AppendFormat("Start Time : {0}\n", message.StartTime);
			sb.AppendFormat("Module : {0}\n", message.Module);
			sb.AppendFormat("Rack : {0}\n", message.Rack);
			sb.AppendFormat("Shelf : {0}\n", message.shelf);
			sb.AppendFormat("Card : {0}\n", message.Card);
			sb.AppendFormat("Reason : {0}\n", message.ReasonTitle);

			using (TMNModelDataContext db = new TMNModelDataContext())
			{
				LogAlarm log = new LogAlarm()
				{
					ID = Guid.NewGuid(),
					CenterID = Center.CurrentCenterID,
					Severity = (byte)message.State,
					Data = sb.ToString(),
					Time = DateTime.Parse(message.StartTime.ToString().Replace(',', ' ')),
					Title = message.CodeTitle.Length > 50 ? message.CodeTitle.Substring(0, 47) + "..." : message.CodeTitle,
					IsRead = false,
					Location = string.Format("M={0},R={1},Sh={2},C={3}", message.Module, message.Rack, message.shelf, message.Card),
					MessageID = message.AID
				};
				db.LogAlarms.InsertOnSubmit(log);
				db.SubmitChanges();
			}
		}

		private void MarkAlarmAsRecovered(int messageID, string details)
		{
			using (var db = new TMNModelDataContext())
			{
				LogAlarm alarm = db.LogAlarms.Where(a => a.CenterID == Center.CurrentCenterID && a.MessageID == messageID).SingleOrDefault();
				alarm.IsRead = true;
                if (alarm.Severity >= 11 && alarm.Severity <= 13)
                    alarm.Severity = (byte)(alarm.Severity % 10);
				alarm.Data += "\n-----------------------------\n  RECOVERY ALARM  \n" + details;
				db.SubmitChanges();
			}
		}

		private void SaveActiveAlarms()
		{
			string newAlarms = string.Empty;
			foreach (string item in newMessages)
				newAlarms += "," + item;

			if (newAlarms.Length > 0)
				newAlarms = newAlarms.Substring(1);

			TMN.RegSettings.Save("ZTEActiveAlarms", newAlarms);
		}

		private void RetrievalActiveAlarms()
		{
			string newAlarms = (string)TMN.RegSettings.Get("ZTEActiveAlarms");
			if (newAlarms.Length > 0)
			{
				string[] newAlarmsArray = newAlarms.Split(',');

				if (newAlarmsArray.Length > 0)
				{
					foreach (string item in newAlarmsArray)
						newMessages.Add(item);
				}
			}
		}

		public bool IsWinAuthentication
		{
			get
			{
				string value = (string)RegSettings.Get("ZTEConnectionMethod", ConnectionType.WindowsAuthentication.ToString());
				if (value == ConnectionType.WindowsAuthentication.ToString())
					return true;
				else
					return false;

			}
		}
	}
}
