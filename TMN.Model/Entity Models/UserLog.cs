using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enterprise;

namespace TMN
{
	public partial class UserLog
	{
		public static void Log(TMNModelDataContext context, ActionType action, string description, string technicalReport)
		{
			try
			{
				context.UserLogs.InsertOnSubmit(new UserLog
		   {
			   ID = Guid.NewGuid(),
			   CenterID = Center.Current.ID,
			   UserID = User.Current.ID,
			   Date = DateTime.Now,
			   Action = ((int)action).ToString(),
			   Description = description,
			   TechnicalReport = technicalReport
		   });
			}
			catch (Exception ex)
			{
				Logger.Write(ex);
			}
		}
	}
}
