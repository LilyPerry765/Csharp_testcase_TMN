using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ArchiveService
{
	[ServiceContract]
	public interface IMainService
	{
		[OperationContract]
		List<ArchiveService.LogAlarm> GetLogAlarm(string database, string query);

		[OperationContract]
		List<ArchiveService.UserLog> GetUserLog(string database, string query);

		[OperationContract]
		List<ArchiveService.SensorData> GetSensorData(string database, string query);


		[OperationContract]
		int GetCountLogAlarm(string database, string query);

		[OperationContract]
		int GetCountUserLog(string database, string query);

		[OperationContract]
		int GetCountSensorData(string database, string query);


		[OperationContract]
		void ShowLog(string log);
	}
}
