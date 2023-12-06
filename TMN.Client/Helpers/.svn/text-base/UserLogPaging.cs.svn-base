using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Enterprise;

namespace TMN
{
	public class UserLogPaging
	{
		public UserLogPaging()
		{
			PageNumber = 1;
		}

		public List<ArchiveService.UserLog> CalculateSearch(Guid id, bool isShowAllUserLog,string search)
		{
			try
			{
				if ( isShowAllUserLog)
				{
					string query = string.Format("SELECT * FROM [UserLog] WHERE [Date] LIKE '%{0}%' OR [Description] LIKE '%{0}%' ", search);
					List<ArchiveService.UserLog> list = null;

					using (ArchiveServiceReference.MainServiceClient service = new ArchiveServiceReference.MainServiceClient())
					{
						list = service.GetUserLog(DatabaseName, query);
						return list;
					}
				}
				else
				{
					string query = string.Format("SELECT * FROM [UserLog] WHERE ( [UserID] = '{0}' ) AND ( [Date] LIKE '%{1}%' OR [Description] LIKE '%{1}%' ) ", id.ToString(), search);
					List<ArchiveService.UserLog> list = null;

					using (ArchiveServiceReference.MainServiceClient service = new ArchiveServiceReference.MainServiceClient())
					{
						list = service.GetUserLog(DatabaseName, query);
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

		public List<ArchiveService.UserLog> CalculateNormal(Guid id, bool isShowAllUserLog)
		{
			try
			{
				if (isShowAllUserLog)
				{
					string query = "SELECT * FROM [UserLog]";
					//string query = "select top 10 * from [UserLog]";
					List<ArchiveService.UserLog> list = null;

					using (ArchiveServiceReference.MainServiceClient service = new ArchiveServiceReference.MainServiceClient())
					{
						list = service.GetUserLog(DatabaseName, query);
						return list;
					}
				}
				else
				{
					string query = string.Format("SELECT * FROM [UserLog] WHERE [UserID] = '{0}' ", id.ToString());
					List<ArchiveService.UserLog> list = null;

					using (ArchiveServiceReference.MainServiceClient service = new ArchiveServiceReference.MainServiceClient())
					{
						list = service.GetUserLog(DatabaseName, query);
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

		//private void GetTotalRow(Guid id, string search)
		//{
		//    List<ArchiveService.UserLog> list = null;

		//    if ((UserLogPaging.IsSearchMode) && (UserLogPaging.IsShowAllUserLog))
		//    {
		//        string query = string.Format("SELECT COUNT([ID]) FROM [UserLog] WHERE [Date] LIKE '%{0}%' OR [Description] LIKE '%{0}%' ", search);

		//        using (ArchiveServiceReference.MainServiceClient service = new ArchiveServiceReference.MainServiceClient())
		//        {
		//            list = service.GetUserLog(DatabaseName, query);
		//            int count = list.Count;
		//            TotalPage = count / PageSize;
		//            if (count % PageSize > 0)
		//            {
		//                TotalPage += 1;
		//            }
		//        }
		//    }
		//    else if ((UserLogPaging.IsSearchMode) && (!UserLogPaging.IsShowAllUserLog))
		//    {
		//        string query = string.Format("SELECT COUNT([ID]) FROM [UserLog] WHERE ( [UserID] = '{0}' ) AND ( [Date] LIKE '%{1}%' OR [Description] LIKE '%{1}%' )", id.ToString(), search);

		//        using (ArchiveServiceReference.MainServiceClient service = new ArchiveServiceReference.MainServiceClient())
		//        {
		//            list = service.GetUserLog(DatabaseName, query);
		//            int count = list.Count;
		//            TotalPage = count / PageSize;
		//            if (count % PageSize > 0)
		//            {
		//                TotalPage += 1;
		//            }
		//        }
		//    }
		//    else if ((!UserLogPaging.IsSearchMode) && (UserLogPaging.IsShowAllUserLog))
		//    {
		//        string query = string.Format("SELECT COUNT([ID]) FROM [UserLog]");

		//        using (ArchiveServiceReference.MainServiceClient service = new ArchiveServiceReference.MainServiceClient())
		//        {
		//            list = service.GetUserLog(DatabaseName, query);
		//            int count = list.Count;
		//            TotalPage = count / PageSize;
		//            if (count % PageSize > 0)
		//            {
		//                TotalPage += 1;
		//            }
		//        }
		//    }
		//    else if ((!UserLogPaging.IsSearchMode) && (!UserLogPaging.IsShowAllUserLog))
		//    {
		//        string query = string.Format("SELECT COUNT([ID]) FROM [UserLog] WHERE [UserID] = '{0}' ", id.ToString());

		//        using (ArchiveServiceReference.MainServiceClient service = new ArchiveServiceReference.MainServiceClient())
		//        {
		//            list = service.GetUserLog(DatabaseName, query);
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
		//    }
		//}

		public List<ArchiveService.UserLog> GetRecords(Guid id, bool isSearchMode, bool isShowAllUserLog, string search)
		{
			List<ArchiveService.UserLog> list = null;

			if ((isSearchMode) && (isShowAllUserLog))
			{
				string queryCount = string.Format("SELECT COUNT([ID]) FROM [UserLog] WHERE [Date] LIKE '%{0}%' OR [Description] LIKE '%{0}%' ", search);

				string query = string.Format(" DECLARE @PageNum AS INT;" +
											  "DECLARE @PageSize AS INT;" +
											  "SET @PageNum = {0};" +
											  "SET @PageSize = {1};" +
											  "WITH OrdersRN AS" +
											  "(" +
											  "  SELECT ROW_NUMBER() OVER(ORDER BY ID) AS RowNum" +
											  ", * FROM [UserLog] WHERE [Data] LIKE '%{2}%' OR [Description] LIKE '%{2}%' " +
											  ")" +
											  "SELECT * " +
											  " FROM OrdersRN" +
											  " WHERE RowNum BETWEEN (@PageNum - 1) * @PageSize + 1 " +
											  " AND @PageNum * @PageSize", PageNumber, PageSize, search);


				using (ArchiveServiceReference.MainServiceClient service = new ArchiveServiceReference.MainServiceClient())
				{
					list = service.GetUserLog(DatabaseName, query);
					int count = list.Count;
					TotalPage = count / PageSize;
					if (count % PageSize > 0)
					{
						TotalPage += 1;
					}

					list = service.GetUserLog(DatabaseName, query);
					return list;
				}
			}
			else if ((isSearchMode) && (!isShowAllUserLog))
			{
				string queryCount = string.Format("SELECT COUNT([ID]) FROM [UserLog] WHERE ( [UserID] = '{0}' ) AND ( [Date] LIKE '%{1}%' OR [Description] LIKE '%{1}%' )", id.ToString(), search);

				string query = string.Format(" DECLARE @PageNum AS INT;" +
									  "DECLARE @PageSize AS INT;" +
									  "SET @PageNum = {0};" +
									  "SET @PageSize = {1};" +
									  "WITH OrdersRN AS" +
									  "(" +
									  "  SELECT ROW_NUMBER() OVER(ORDER BY ID) AS RowNum" +
									  ", * FROM [UserLog] WHERE ( [UserID] = '{2}' ) AND ( [Data] LIKE '%{3}%' OR [Description] LIKE '%{3}%' ) " +
									  ")" +
									  "SELECT * " +
									  " FROM OrdersRN" +
									  " WHERE RowNum BETWEEN (@PageNum - 1) * @PageSize + 1 " +
									  " AND @PageNum * @PageSize", PageNumber, PageSize, id.ToString(), search);

				using (ArchiveServiceReference.MainServiceClient service = new ArchiveServiceReference.MainServiceClient())
				{
					list = service.GetUserLog(DatabaseName, query);
					int count = list.Count;
					TotalPage = count / PageSize;
					if (count % PageSize > 0)
					{
						TotalPage += 1;
					}

					list = service.GetUserLog(DatabaseName, query);
					return list;
				}
			}
			else if ((!isSearchMode) && (isShowAllUserLog))
			{
				string queryCount = string.Format("SELECT COUNT([ID]) FROM [UserLog]");

				string query = string.Format(" DECLARE @PageNum AS INT;" +
											  "DECLARE @PageSize AS INT;" +
											  "SET @PageNum = {0};" +
											  "SET @PageSize = {1};" +
											  "WITH OrdersRN AS" +
											  "(" +
											  "  SELECT ROW_NUMBER() OVER(ORDER BY ID) AS RowNum" +
											  ", * FROM [UserLog] " +
											  ")" +
											  "SELECT * " +
											  " FROM OrdersRN" +
											  " WHERE RowNum BETWEEN (@PageNum - 1) * @PageSize + 1 " +
											  " AND @PageNum * @PageSize", PageNumber, PageSize);

				using (ArchiveServiceReference.MainServiceClient service = new ArchiveServiceReference.MainServiceClient())
				{
					list = service.GetUserLog(DatabaseName, query);
					int count = list.Count;
					TotalPage = count / PageSize;
					if (count % PageSize > 0)
					{
						TotalPage += 1;
					}

					list = service.GetUserLog(DatabaseName, query);
					return list;
				}
			}
			else if ((!isSearchMode) && (!isShowAllUserLog))
			{
				string queryCount = string.Format("SELECT COUNT([ID]) FROM [UserLog] WHERE [UserID] = '{0}' ", id.ToString());

				string query = string.Format(" DECLARE @PageNum AS INT;" +
											  "DECLARE @PageSize AS INT;" +
											  "SET @PageNum = {0};" +
											  "SET @PageSize = {1};" +
											  "WITH OrdersRN AS" +
											  "(" +
											  "  SELECT ROW_NUMBER() OVER(ORDER BY ID) AS RowNum" +
											  ", * FROM [UserLog] WHERE [UserID] = '{2}' " +
											  ")" +
											  "SELECT * " +
											  " FROM OrdersRN" +
											  " WHERE RowNum BETWEEN (@PageNum - 1) * @PageSize + 1 " +
											  " AND @PageNum * @PageSize", PageNumber, PageSize, id.ToString());

				using (ArchiveServiceReference.MainServiceClient service = new ArchiveServiceReference.MainServiceClient())
				{
					list = service.GetUserLog(DatabaseName, query);
					int count = list.Count;
					TotalPage = count / PageSize;
					if (count % PageSize > 0)
					{
						TotalPage += 1;
					}

					list = service.GetUserLog(DatabaseName, query);
					return list;
				}
			}
			else
			{
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

		public string DatabaseName
		{
			get;
			set;
		}


	}
}
