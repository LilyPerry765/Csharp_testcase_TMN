using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using TMN.UI.Windows;
using Enterprise;
using System.ServiceModel;

namespace TMN
{
	public class LogAlarmPaging
	{
		public LogAlarmPaging()
		{
			PageNumber = 1;
		}

		public List<ArchiveService.LogAlarm> CalculateSearch(Guid id, string search)
		{
			try
			{
				string query = string.Format("SELECT * FROM [LogAlarm] WHERE ( [CenterID] = '{0}' ) AND ( [Title] LIKE '%{1}%' OR  [Data] LIKE '%{1}%' OR [Title] LIKE '%{1}%' ) ", id.ToString(), search);
				List<ArchiveService.LogAlarm> list = null;

				using (ArchiveServiceReference.MainServiceClient service = new ArchiveServiceReference.MainServiceClient())
				{
					list = service.GetLogAlarm(Database, query);
					return list;
				}
			}
			catch (Exception ex)
			{
				Logger.Write(ex);
				return null;
			}
		}

		public List<ArchiveService.LogAlarm> CalculateNormal(Guid id)
		{
			try
			{
				string query = string.Format("SELECT * FROM [LogAlarm] WHERE [CenterID] = '{0}' ", id.ToString());

				List<ArchiveService.LogAlarm> list = null;

				using (ArchiveServiceReference.MainServiceClient service = new ArchiveServiceReference.MainServiceClient())
				{
					list = service.GetLogAlarm(Database, query);
					return list;
				}
			}
			catch (Exception ex)
			{
				Logger.Write(ex);
				return null;
			}
		}

		//private void GetTotalRow(Guid id, string search)
		//{
		//    List<ArchiveService.LogAlarm> list = null;

		//    if (LogAlarmPaging.IsSearchMode)
		//    {
		//        string query = string.Format("SELECT COUNT([ID]) FROM [LogAlarm] WHERE ( [CenterID] = '{0}' ) AND ( [Title] LIKE '%{1}%' OR  [Data] LIKE '%{1}%' OR [Title] LIKE '%{1}%' ) ", id.ToString(), search);

		//        using (ArchiveServiceReference.MainServiceClient service = new ArchiveServiceReference.MainServiceClient())
		//        {
		//            list = service.GetLogAlarm(DatabaseName, query);
		//            int count = list.Count;
		//            TotalPage = count / PageSize;
		//            if (count % PageSize > 0)
		//            {
		//                TotalPage += 1;
		//            }
		//        }

		//    }
		//    else
		//    {
		//        string query = string.Format("SELECT COUNT([ID]) FROM [LogAlarm] WHERE [CenterID] = '{0}' ", id.ToString());

		//        using (ArchiveServiceReference.MainServiceClient service = new ArchiveServiceReference.MainServiceClient())
		//        {
		//            list = service.GetLogAlarm(DatabaseName, query);
		//            int count = list.Count;
		//            TotalPage = count / PageSize;
		//            if (count % PageSize > 0)
		//            {
		//                TotalPage += 1;
		//            }
		//        }
		//    }
		//}

		public List<ArchiveService.LogAlarm> GetRecords(Guid id,bool isSearchMode, string search)
		{
			try
			{
				List<ArchiveService.LogAlarm> list = null;

				if (isSearchMode)
				{
					string queryCount = string.Format("SELECT COUNT([ID]) FROM [LogAlarm] WHERE ( [CenterID] = '{0}' ) AND ( [Title] LIKE '%{1}%' OR  [Data] LIKE '%{1}%' OR [Title] LIKE '%{1}%' ) ", id.ToString(), search);

					string queryPaging = string.Format(" DECLARE @PageNum AS INT;" +
												  "DECLARE @PageSize AS INT;" +
												  "SET @PageNum = {0};" +
												  "SET @PageSize = {1};" +
												  "WITH OrdersRN AS" +
												  "(" +
												  "  SELECT ROW_NUMBER() OVER(ORDER BY ID) AS RowNum" +
												  ", * FROM [LogAlarm] where [CenterID] = '{2}' AND ( [Title] LIKE '%{3}%' OR [Data] LIKE '%{3}%' OR [Title] LIKE '%{3}%' ) " +
												  ")" +
												  "SELECT * " +
												  " FROM OrdersRN" +
												  " WHERE RowNum BETWEEN (@PageNum - 1) * @PageSize + 1 " +
												  " AND @PageNum * @PageSize", PageNumber, PageSize, id.ToString(), search);


					using (ArchiveServiceReference.MainServiceClient service = new ArchiveServiceReference.MainServiceClient())
					{

						int count = service.GetCountLogAlarm(Database, queryCount);
						TotalPage = count / PageSize;
						if (count % PageSize > 0)
						{
							TotalPage += 1;
						}

						list = service.GetLogAlarm(Database, queryPaging);
						return list;
					}
				}
				else
				{
					string queryCount = string.Format("SELECT COUNT([ID]) FROM [LogAlarm] WHERE [CenterID] = '{0}' ", id.ToString());

					string queryPaging = string.Format(" DECLARE @PageNum AS INT;" +
												  "DECLARE @PageSize AS INT;" +
												  "SET @PageNum = {0};" +
												  "SET @PageSize = {1};" +
												  "WITH OrdersRN AS" +
												  "(" +
												  "  SELECT ROW_NUMBER() OVER(ORDER BY ID) AS RowNum" +
												  ", * FROM [LogAlarm] where [CenterID] = '{2}'" +
												  ")" +
												  "SELECT * " +
												  " FROM OrdersRN" +
												  " WHERE RowNum BETWEEN (@PageNum - 1) * @PageSize + 1 " +
												  " AND @PageNum * @PageSize", PageNumber, PageSize, id.ToString());

					using (ArchiveServiceReference.MainServiceClient service = new ArchiveServiceReference.MainServiceClient())
					{
						int count = service.GetCountLogAlarm(Database, queryCount);
						TotalPage = count / PageSize;
						if (count % PageSize > 0)
						{
							TotalPage += 1;
						}

						list = service.GetLogAlarm(Database, queryPaging);
						return list;
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Write(ex);
				return null;
			}
		}

		public int PageNumber
		{
			get;
			set;
		}

		public int PageSize
		{
			get;
			set;
		}

		public int TotalPage
		{
			get;
			set;
		}

		public string Database
		{
			get;
			set;
		}
	}
}

