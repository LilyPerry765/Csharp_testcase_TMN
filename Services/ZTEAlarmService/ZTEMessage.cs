using System.Collections.Generic;
using System.Data.SqlClient;
using Enterprise;
using System;

namespace ZTEAlarmService
{
	class ZTEMessage
	{
		public int Module
		{
			get;
			set;
		}

		public int AID
		{
			get;
			set;
		}

		public string CodeTitle
		{
			get;
			set;
		}

		public string ReasonTitle
		{
			get;
			set;
		}

		public int State
		{
			get;
			set;
		}

		public int Rack
		{
			get;
			set;
		}

		public int shelf
		{
			get;
			set;
		}

		public int Card
		{
			get;
			set;
		}

		public string StartTime
		{
			get;
			set;
		}

		public string Content
		{
			get;
			set;
		}

		public string EndTime
		{
			get;
			set;
		}

		public static List<ZTEMessage> GetNewMessage( string connectionString,  string query)
		{
			List<ZTEMessage> collection = new List<ZTEMessage>();
			if (collection.Count != 0)
				collection.Clear();
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				using (SqlCommand command = new SqlCommand(query, connection))
				{
					connection.Open();
					using (SqlDataReader reader = command.ExecuteReader())
					{
						if (reader.HasRows)
						{
							while (reader.Read())
							{
								ZTEMessage message = new ZTEMessage()
								{
									CodeTitle = (string)reader["CodeTitle"].ToString(),
									ReasonTitle = (string)reader["ReasonTitle"].ToString(),
									StartTime = (string)reader["StartTime"].ToString(),
									Content = (string)reader["Content"].ToString(),
									EndTime = string.Empty,
									AID = (int)reader["Aid"],
									Module = (byte)reader["Module"],
									State = (byte)reader["State"],
									Rack = (byte)reader["Rack"],
									shelf = (byte)reader["Shelf"],
									Card = (byte)reader["Card"]
								};
								collection.Add(message);
							}
						}
						connection.Close();
					}
				}
				return collection;
			}
		}

		public static List<ZTEMessage> GetRecoveryMessage(string connectionString, string query)
		{
			List<ZTEMessage> collection = new List<ZTEMessage>();
			if (collection.Count != 0)
				collection.Clear();
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				using (SqlCommand command = new SqlCommand(query, connection))
				{
					connection.Open();
					using (SqlDataReader reader = command.ExecuteReader())
					{
						if (reader.HasRows)
						{
							while (reader.Read())
							{
								ZTEMessage message = new ZTEMessage()
								{
									CodeTitle = (string)reader["CodeTitle"].ToString(),
									ReasonTitle = (string)reader["ReasonTitle"].ToString(),
									StartTime = (string)reader["StartTime"].ToString(),
									Content = (string)reader["Content"].ToString(),
									EndTime = (string)reader["EndTime"].ToString(),
									AID = (int)reader["Aid"],
									Module = (byte)reader["Module"],
									State = (byte)reader["State"],
									Rack = (byte)reader["Rack"],
									shelf = (byte)reader["Shelf"],
									Card = (byte)reader["Card"]
								};
								collection.Add(message);
							}
						}
						connection.Close();
					}
				}
				return collection;
			}
		}
	}
}
