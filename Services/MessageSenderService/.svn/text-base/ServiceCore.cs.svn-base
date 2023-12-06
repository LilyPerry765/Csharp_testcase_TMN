using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMN;
using Enterprise;
using System.Timers;
using System.IO;

namespace MessageSenderService
{
	public class ServiceCore : TmnService
	{
		Timer timer = null;
		ServiceManager manager = new ServiceManager();
		List<Tuple<Guid, string>> contacts = new List<Tuple<Guid, string>>();
		List<MessageSendInfo> messageInfolist = new List<MessageSendInfo>();
		//int currentDay = DateTime.Now.Day;

		public ServiceCore()
		{
			if (timer == null)
			{
				timer = new Timer();
				timer.Interval = double.Parse(RegSettings.Get("MessageSenderTimerInterval", "10000").ToString());
				timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
			}
		}

		void timer_Elapsed(object sender, ElapsedEventArgs e)
		{
			timer.Stop();

			//if (currentDay != DateTime.Now.Day)
			//    LogRegister.Create();

			//SendEveryDailySMS();

			//if (Setting.Get("IsContactsChange", "0") == "1")
			//{
			//    Logger.WriteInfo("Restarting message sender service");
			//    using (TMNModelDataContext db = new TMNModelDataContext())
			//    {
			//        contacts = (from c in db.Contacts
			//                    join cc in db.CenterContacts on c.ID equals cc.ContactID
			//                    where cc.AlarmType == (int)MessageAlarmType.AlarmOnTime
			//                    select Tuple.Create(
			//                        cc.CenterID,
			//                        c.Number
			//                    )).ToList();
			//    }
			//    Logger.WriteInfo("Restarted message sender service");
			//    Setting.Set("IsContactsChange", "0");
			//}

			CheckForCirsuits();

			timer.Start();
		}

		private void SendEveryDailySMS()
		{
			try
			{
				if (DateTime.Now.ToString("HH:mm") == "08:00" && IsDailySendMessage == false )
				{
					Logger.WriteInfo("Checking for send message at 8:00 oclock");
					Logger.WriteInfo("Getting data for find contacts to send message at 8:00 oclock");
					using (TMNModelDataContext db = new TMNModelDataContext())
					{
						List<Tuple<Guid, string, Guid?>> centers = new List<Tuple<Guid, string, Guid?>>();
						List<Tuple<Guid>> regions = new List<Tuple<Guid>>();
						List<Tuple< string, string>> contacts = new List<Tuple< string, string>>();

						StringBuilder sb = new StringBuilder();
						string temp = string.Empty;
						bool hasOpenCircuit = false;

						var openCircuitData = db.Sensors.Where(s => (s.ModulNumber > 100))
							.Select(s => new { CenterID = s.Room.CenterID.Value, Sensor = s, SensorData = s.SensorDatas.OrderByDescending(sd => sd.Date).FirstOrDefault() })
							.Where(sd => sd.SensorData.Value == (byte)CircuitEnum.OpenCircuit).ToList();

						centers = (from c in db.Centers
								   select Tuple.Create(c.ID, c.Name, c.RegionID)).ToList();

						regions = (from r in db.Regions
								   select Tuple.Create(r.ID)).ToList();


						Logger.WriteInfo("Count of OpenCircuits is {0}", openCircuitData.Count);
						if (openCircuitData.Count != 0)
						{
							foreach (var region in regions)
							{
								sb.Clear();
								foreach (var center in centers.Where(c => c.Item3 == region.Item1))
								{
									temp = string.Empty;
									foreach (var data in openCircuitData)
									{
										if (center.Item1 == data.CenterID)
										{
											int space = data.Sensor.Title.IndexOf(' ');
											if (space != -1)
												temp += "و" + data.Sensor.Title.Substring(0, space);
											else
												temp += "و" + data.Sensor.Title.Substring(0, 2);

											temp = temp.Substring(1);
											hasOpenCircuit = true;
										}
									}

									if (hasOpenCircuit)
									{
										sb.Append("-");
										sb.Append(string.Format("کابل {0} مرکز {1}", temp, center.Item2));
										hasOpenCircuit = false;
									}
								}

								if (sb.ToString() != string.Empty)
									sb.Append(" قطع هستند ");

								Logger.WriteInfo("Getting Contacts data");
								contacts = (from co in db.Contacts
											join cc in db.CenterContacts on co.ID equals cc.ContactID
											join cn in db.Centers on cc.CenterID equals cn.ID
											join r in db.Regions on cn.RegionID equals r.ID
											where (cc.AlarmType == (int)MessageAlarmType.AlarmDaily) && (cn.RegionID == region.Item1)
											select Tuple.Create( co.Name, co.Number)).Distinct().ToList();

								Logger.WriteInfo("Count of ontacts is {0}", contacts.Count);
								if (contacts != null)
								{
									File.AppendAllText(@"C:\Windows\MessageSender", "Begin send message at 8:00 oclock A.M"+Environment.NewLine );
									foreach (var  contact in contacts)
									{
										if (sb.ToString() != string.Empty)
										{
											string log = string.Format("{0}    {1}    {2}    {3}  {4}", sb.ToString(), contact.Item1, contact.Item2, DateTime.Now, Environment.NewLine);
											//LogRegister.Write(log);
											Logger.WriteInfo(log);
										}
											manager.Send(sb.ToString(), contact.Item2);
									}

									IsDailySendMessage = true;
									File.AppendAllText(@"C:\Windows\MessageSender", "End send message at 8:00 oclock A.M" + Environment.NewLine);
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Write(ex);
			}
		}

		private void SendOpenCircuitSMS(string message, List<Tuple<Guid, string>> dictionary, Guid centerid)
		{
			foreach (var item in dictionary)
			{
				if (item.Item1 == centerid)
				{
					manager.Send(message, item.Item2);

					Logger.WriteInfo("send message to {0} on {1} . {2}", item.Item2, DateTime.Now, message);

					string log = string.Format("{0}    {1}    {2}    {3} ", message, item.Item2, DateTime.Now, Environment.NewLine);
					File.AppendAllText(@"C:\Windows\MessageSender", log);

					//LogRegister.Write( log);

				}
			}
		}

		private void SendShortCircuitSMS(string message, List<Tuple<Guid, string>> dictionary, Guid centerid)
		{
			foreach (var item in dictionary)
			{
				if (item.Item1 == centerid)
				{
					manager.Send(message, item.Item2);

					Logger.WriteInfo("send message to {0} on {1} . {2}", item.Item2, DateTime.Now, message);

					string log = string.Format("{0}    {1}    {2}    {3} ", message, item.Item2, DateTime.Now, Environment.NewLine);
					File.AppendAllText(@"C:\Windows\MessageSender", log);

					//LogRegister.Write( log);
				}
			}
		}

		private void SendDisableSMSService(string message, List<Tuple<Guid, string>> dictionary, Guid centerid)
		{
			foreach (var item in dictionary)
			{
				if (item.Item1 == centerid)
				{
					manager.Send(message, item.Item2);

					Logger.WriteInfo("send message to {0} on {1} . {2}", item.Item2, DateTime.Now, message);

					string log = string.Format("{0}    {1}    {2}    {3} ", message, item.Item2, DateTime.Now, Environment.NewLine);
					File.AppendAllText(@"C:\Windows\MessageSender", log);

				}
			}
		}

		public void CheckForCirsuits()
		{
			try
			{
				Logger.WriteInfo("Checking for circuit data ... ");
				manager.Connect();

				using (TMNModelDataContext db = new TMNModelDataContext())
				{
					List<SensorData> openCircuitData = db.Sensors.Where(s => (s.ModulNumber > 100) && (s.Max == 1))
						.Select(s => s.SensorDatas.OrderByDescending(sd => sd.Date).FirstOrDefault())
						.Where(sd => sd.Value == (byte)CircuitEnum.OpenCircuit).ToList();

					foreach (SensorData item in openCircuitData)
					{
						int space = item.Sensor.Title.IndexOf(' ');
						string circuitNumber = item.Sensor.Title.Substring(0, space);
						string centerName = item.Sensor.Room.Center.Name;
						Guid centerID = item.Sensor.Room.Center.ID;

						item.Sensor.Max = 0;
						db.SubmitChanges();

						string text = string.Format("کابل شماره {0} مرکز {1} قطع شد", circuitNumber, centerName);
						SendOpenCircuitSMS(text, contacts, centerID);
					}

					List<SensorData> shortCircuitData = db.Sensors.Where(s => (s.ModulNumber > 100) && (s.Max == 0))
						.Select(s => s.SensorDatas.OrderByDescending(sd => sd.Date).FirstOrDefault())
						.Where(sd => (sd.Value == (byte)CircuitEnum.ShortCircuit) || (sd.Value == (byte)CircuitEnum.NORMAL)).ToList();

					foreach (SensorData item in shortCircuitData)
					{
						int space = item.Sensor.Title.IndexOf(' ');
						string circuitNumber = item.Sensor.Title.Substring(0, space);
						string centerName = item.Sensor.Room.Center.Name;
						Guid centerID = item.Sensor.Room.Center.ID;

						item.Sensor.Max = 1;
						db.SubmitChanges();

						string text = string.Format("کابل شماره {0} مرکز {1} وصل شد", circuitNumber, centerName);
						SendShortCircuitSMS(text, contacts, centerID);
					}
				}
				manager.Disconnect();
			}
			catch (Exception ex)
			{
				Logger.Write(ex);
			}
		}

		//public void CheckForCirsuits()
		//{
		//    try
		//    {
		//        Logger.WriteInfo("Checking for circuit data ... ");
		//        manager.Connect();

		//        using (TMNModelDataContext db = new TMNModelDataContext())
		//        {
		//            List<SensorData> openCircuitData = db.Sensors.Where(s => (s.ModulNumber > 100) && (s.Max == 1))
		//                .Select(s => s.SensorDatas.OrderByDescending(sd => sd.Date).FirstOrDefault())
		//                .Where(sd => sd.Value == (byte)CircuitEnum.OpenCircuit).ToList();


		//            foreach (SensorData item in openCircuitData)
		//            {
		//                int space = item.Sensor.Title.IndexOf(' ');
		//                string circuitNumber = item.Sensor.Title.Substring(0, space);
		//                string centerName = item.Sensor.Room.Center.Name;
		//                Guid centerID = item.Sensor.Room.Center.ID;

		//                int openCircuitCount = messageInfolist.Where(t => t.CircuitNumber == item.Sensor.ID).Count();

		//                DateTime maxDate = messageInfolist.Where(t => t.CircuitNumber == item.Sensor.ID)
		//                                   .OrderBy(t => t.CurrentDate).Select(t => t.CurrentDate).LastOrDefault();

		//                DateTime minDate = messageInfolist.Where(t => t.CircuitNumber == item.Sensor.ID)
		//                                   .OrderBy(t => t.CurrentDate).Select(t => t.CurrentDate).FirstOrDefault();

		//                TimeSpan compareDate = maxDate - minDate;

		//                if ((openCircuitCount == 5) && (compareDate.TotalSeconds <= 3610))
		//                {
		//                    string message = string.Format("سرویس ارسال پیامک کابل {0} مرکز {1} قطع شد", circuitNumber, centerName);
		//                    SendDisableSMSService(message, contacts, centerID);

		//                    double? d = null;
		//                    item.Sensor.Max = d;
		//                    db.SubmitChanges();
		//                }
		//                else
		//                {
		//                    if (openCircuitCount >= 5)
		//                    {
		//                        //messageInfolist.RemoveAll(t => t.CircuitNumber == item.Sensor.ID && t.CurrentDate == minDate);
		//                        MessageSendInfo mm = messageInfolist.Where(t => t.CircuitNumber == item.Sensor.ID && t.CurrentDate == minDate).FirstOrDefault();
		//                        messageInfolist.Remove(mm);
		//                    }
		//                    else
		//                    {
		//                        messageInfolist.Add(new MessageSendInfo(item.Sensor.ID, DateTime.Now));
		//                    }

		//                    item.Sensor.Max = 0;
		//                    db.SubmitChanges();

		//                    string text = string.Format("کابل شماره {0} مرکز {1} قطع شد", circuitNumber, centerName);
		//                    SendOpenCircuitSMS(text, contacts, centerID);
		//                }
		//            }

		//            List<SensorData> shortCircuitData = db.Sensors.Where(s => (s.ModulNumber > 100) && (s.Max == 0))
		//                .Select(s => s.SensorDatas.OrderByDescending(sd => sd.Date).FirstOrDefault())
		//                .Where(sd => (sd.Value == (byte)CircuitEnum.ShortCircuit) || (sd.Value == (byte)CircuitEnum.NORMAL)).ToList();


		//            foreach (SensorData item in shortCircuitData)
		//            {
		//                int space = item.Sensor.Title.IndexOf(' ');
		//                string circuitNumber = item.Sensor.Title.Substring(0, space);
		//                string centerName = item.Sensor.Room.Center.Name;
		//                Guid centerID = item.Sensor.Room.Center.ID;

		//                item.Sensor.Max = 1;
		//                db.SubmitChanges();

		//                string text = string.Format("کابل شماره {0} مرکز {1} وصل شد", circuitNumber, centerName);
		//                SendShortCircuitSMS(text, contacts, centerID);
		//            }
		//        }
		//        manager.Disconnect();
		//    }
		//    catch (Exception ex)
		//    {
		//        Logger.Write(ex);
		//    }
		//}

		public override void Start()
		{
			Logger.WriteInfo("starting MessageSenderService ...");
			//IsDailySendMessage = false;
			using (TMNModelDataContext db = new TMNModelDataContext())
			{
				contacts = (from c in db.Contacts
							join cc in db.CenterContacts on c.ID equals cc.ContactID
							where cc.AlarmType == (int)MessageAlarmType.AlarmOnTime
							select Tuple.Create(
								cc.CenterID,
								c.Number
							)).ToList();
			}
			timer.Start();
			Logger.WriteStart("MessageSenderService started ...");
		}

		public override void Stop()
		{
			Logger.WriteInfo("Stopping MessageSenderService ...");
			timer.Stop();
			Logger.WriteEnd("MessageSenderService stopped .");
		}


		public bool IsDailySendMessage
		{
			get;
			set;
		}
	}
}
