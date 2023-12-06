using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace TMN
{
	[ServiceContract]
	public interface IMainService
	{
        [OperationContract]
        void CheckAccount(string clientUserName, string clientPassword);

		[OperationContract()]
        void UpgradeService(string serverUserName, string serverPassword);
	}
}
