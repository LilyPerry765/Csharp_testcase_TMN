using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ArchiveService
{
	[DataContract]
	public class UserLog
	{
		[DataMember]
		public Guid CenterID
		{
			get;
			set;
		}

		[DataMember]
		public Guid UserID
		{
			get;
			set;
		}

		[DataMember]
		public Nullable<DateTime> Date
		{
			get;
			set;
		}

		[DataMember]
		public string Action
		{
			get;
			set;
		}

		[DataMember]
		public string Description
		{
			get;
			set;
		}
	}
}
