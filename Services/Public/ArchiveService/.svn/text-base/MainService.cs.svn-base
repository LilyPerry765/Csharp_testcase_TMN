using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data.SqlClient;
using Enterprise;

namespace ArchiveService
{
	public class MainService : IMainService
	{
		public List<ArchiveService.LogAlarm> GetLogAlarm(string database, string query)
		{
			try
			{
				List<LogAlarm> collection = new List<LogAlarm>();
				string connectionString = string.Format(@"Data Source=.\SQLEXPRESS;Integrated Security=True;User Instance=True;AttachDbFilename=D:\BackupDatabase\{0}", database);
				string commandText = query;
				if (collection.Count != 0)
					collection.Clear();
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					using (SqlCommand command = new SqlCommand(commandText, connection))
					{
						connection.Open();
						Logger.WriteInfo("Getting for ArchiveLogAlarm data");
						using (SqlDataReader reader = command.ExecuteReader())
						{
							if (reader.HasRows)
							{
								while (reader.Read())
								{
									LogAlarm alarm = new LogAlarm()
									{
										Title = (string)reader["Title"].ToString(),
										Severity = (byte?)reader["Severity"],
										Data = (string)reader["Data"].ToString(),
										Time = (DateTime)reader["Time"],
										Location = reader["Location"].ToString(),
										CenterID = (Guid?)reader["CenterID"]
									};
									collection.Add(alarm);
								}
							}
							connection.Close();
						}
						Logger.WriteInfo("found {0} row(s)", collection.Count);
					}
					return collection;
				}
			}
			catch (Exception ex)
			{
				Logger.Write(ex);
				return null;
			}
		}

		public List<ArchiveService.UserLog> GetUserLog(string database, string query)
		{
			try
			{
				List<UserLog> collection = new List<UserLog>();
				string connectionString = string.Format(@"Data Source=.\SQLEXPRESS;Integrated Security=True;User Instance=True;AttachDbFilename=D:\BackupDatabase\{0}", database);
				string commandText = query;
				if (collection.Count != 0)
					collection.Clear();
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					using (SqlCommand command = new SqlCommand(commandText, connection))
					{
						connection.Open();
						Logger.WriteInfo("getting for ArchiveUserLog data");
						using (SqlDataReader reader = command.ExecuteReader())
						{
							if (reader.HasRows)
							{
								while (reader.Read())
								{
									UserLog userLog = new UserLog()
									{
										CenterID = (Guid)reader["CenterID"],
										UserID = (Guid)reader["UserID"],
										Date = (DateTime?)reader["Date"],
										Action = (string)reader["Action"].ToString(),
										Description = (string)reader["Description"].ToString()
									};
									collection.Add(userLog);
								}
							}
							connection.Close();
						}
						Logger.WriteInfo("found {0} row(s)", collection.Count);
					}
					return collection;
				}
			}
			catch (Exception ex)
			{
				Logger.Write(ex);
				return null;
			}
		}

		public List<ArchiveService.SensorData> GetSensorData(string database, string query)
		{
			try
			{
				List<SensorData> collection = new List<SensorData>();

				string connectionString = string.Format(@"Data Source=.\SQLEXPRESS;Integrated Security=True;User Instance=True;AttachDbFilename=D:\BackupDatabase\{0}", database);
				string commandText = query;
				if (collection.Count != 0)
					collection.Clear();
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					using (SqlCommand command = new SqlCommand(commandText, connection))
					{
						connection.Open();
						Logger.WriteInfo("getting for ArchiveSensorData data");
						using (SqlDataReader reader = command.ExecuteReader())
						{
							if (reader.HasRows)
							{
								while (reader.Read())
								{
									SensorData userLog = new SensorData()
									{
										SensorID = (Guid)reader["SensorID"],
										Date = (DateTime)reader["Date"],
										Value = (double)reader["Value"]
									};
									collection.Add(userLog);
								}
							}
							connection.Close();
						}
						Logger.WriteInfo("found {0} row(s)", collection.Count);
					}
					return collection;
				}
			}
			catch (Exception ex)
			{
				Logger.Write(ex);
				return null;
			}
		}


		public int GetCountLogAlarm(string database, string query)
		{
			try
			{
				int count = 0;
				string connectionString = string.Format(@"Data Source=.\SQLEXPRESS;Integrated Security=True;User Instance=True;AttachDbFilename=D:\BackupDatabase\{0}", database);
				string commandText = query;
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					using (SqlCommand command = new SqlCommand(commandText, connection))
					{
						connection.Open();
						Logger.WriteInfo("Calculating for count of ArchiveLogAlarm");
						count = (int)command.ExecuteScalar();
						connection.Close();
						Logger.WriteInfo("found {0} row(s)", count);
						return count;
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Write(ex);
				return 0;
			}
		}

		public int GetCountUserLog(string database, string query)
		{
			try
			{
				int count = 0;
				string connectionString = string.Format(@"Data Source=.\SQLEXPRESS;Integrated Security=True;User Instance=True;AttachDbFilename=D:\BackupDatabase\{0}", database);
				string commandText = query;
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					using (SqlCommand command = new SqlCommand(commandText, connection))
					{
						connection.Open();
						Logger.WriteInfo("Calculate for count of ArchiveUserLog");
						count = (int)command.ExecuteScalar();
						connection.Close();
						Logger.WriteInfo("found {0} row(s)", count);
						return count;
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Write(ex);
				return 0;
			}
		}

		public int GetCountSensorData(string database, string query)
		{
			try
			{
				int count = 0;
				string connectionString = string.Format(@"Data Source=.\SQLEXPRESS;Integrated Security=True;User Instance=True;AttachDbFilename=D:\BackupDatabase\{0}", database);
				string commandText = query;
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					using (SqlCommand command = new SqlCommand(commandText, connection))
					{
						connection.Open();
						Logger.WriteInfo("Calculate for count of ArchiveSensorData");
						count = (int)command.ExecuteScalar();
						connection.Close();
						Logger.WriteInfo("found {0} row(s)", count);
						return count;
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Write(ex);
				return 0;
			}
		}


		public void  ShowLog(string log)
		{
			Logger.WriteInfo(log);
		}
	}
}
