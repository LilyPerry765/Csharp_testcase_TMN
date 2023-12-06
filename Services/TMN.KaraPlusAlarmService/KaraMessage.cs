using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Enterprise;
using System.Globalization;

namespace TMN
{
    public enum  ReporterEnum :byte
    {
        SUC = 0,
        DTC = 1,
        TACC = 2,
        CSM = 3,
        DISPATCHER = 5
    }

    public enum ImportanceEnum : byte
    {
        INFO =0,
        MINOR = 1,
        MAJOR = 2,
        CRITICAL = 3
    }

    public enum  StateEnum : byte
    {
        Failed = 0,
        Recovery = 1
    }

    public class KaraEvent
    {
        public int MsgID
        {
            get;
            set;
        }

        public string HashCode
        {
            get;
            set;
        }

        public byte ModuleNo
        {
            get;
            set;
        }

        public byte RackNo
        {
            get;
            set;
        }

        public byte UnitNo
        {
            get;
            set;
        }

        public byte CardNo
        {
            get;
            set;
        }

        public string EventDate
        {
            get;
            set;
        }

        public string EventTime
        {
            get;
            set;
        }

        public DateTime EventDateTime
        {
            get
            {
                try
                {
                    return DateTime.Parse(string.Format("{0} {1}", EventDate, EventTime));
                }
                catch
                {
                    Logger.WriteException("Can't Parse date And time from input date={0} time={1}", EventDate, EventTime);
                }
                return DateTime.MinValue;
            }
        }

        public byte ReporterType
        {
            get;
            set;
        }

        public ReporterEnum ReporterTypeEnum
        {
            get
            {
                return (ReporterEnum)ReporterType;
            }
        }

        public byte EventState
        {
            get;
            set;
        }

        public StateEnum EventStateEnum
        {
            get
            {
                return (StateEnum)EventState;
            }
        }

        public byte FaultCode
        {
            get;
            set;
        }


        public string FaultDescription
        {
            get;
            set;
        }

        public byte Importance
        {
            get;
            set;
        }

        public ImportanceEnum ImportanceEnum
        {
            get
            {
                return (ImportanceEnum)Importance;
            }
        }

        public byte Activation
        {
            get;
            set;
        }


        public string Location
        {
            get
            {
                return string.Format("{0}-{1}-{2}-{3}", ModuleNo, RackNo, UnitNo, CardNo); 
            }
        }

        public string Data
        {
            get
            {
                PersianCalendar calendar = new PersianCalendar();
                StringBuilder builder = new StringBuilder();
                builder.AppendFormat("Reporter Type :   {0}", ReporterTypeEnum.ToString());
                builder.AppendLine();
                builder.AppendLine();
                builder.Append("Physical Address");
                builder.AppendLine();
                builder.AppendLine();
                builder.AppendFormat("Module No = {0}       Rack No = {1}", ModuleNo, RackNo);
                builder.AppendLine();
                builder.AppendFormat("Unit No = {0}         Card No = {1}", UnitNo, CardNo);
                builder.AppendLine();
                builder.AppendLine();
                builder.AppendFormat("Fault Description :   {0}", FaultDescription);
                builder.AppendLine();
                builder.AppendLine();
                builder.AppendFormat("Date :    {0}/{1}/{2}    ", calendar.GetYear(EventDateTime), calendar.GetMonth(EventDateTime), calendar.GetDayOfMonth(EventDateTime));
                builder.AppendFormat("Time :    {0}:{1}:{2}", calendar.GetHour(EventDateTime), calendar.GetMinute(EventDateTime), calendar.GetSecond(EventDateTime));
                builder.AppendLine();
                builder.AppendLine();
                builder.AppendFormat("Event State : {0}", EventStateEnum.ToString());
                builder.AppendLine();
                builder.AppendLine();
                return builder.ToString().ToUpper();
            }
        }

        public IEnumerable<KaraEvent> GetEvents(string wherePart, string serverName, string userName, string passName, bool withFault)
        {
            //string adminConnection = string.Format(@"Data Source={0};Initial Catalog=KTDSSAdmin;User ID={1};Password={2}", serverName, userName, passName);

            StringBuilder sql = new StringBuilder();
            if (withFault)
            {
                sql.Append("SELECT [EventSerialNo] ,[HashCode] ,e.[ModuleNo] ,[RackNo] ,[UnitNo] ,[CardNo] ,[EventDate] ,[EventTime]");
                sql.Append(",e.[ReporterType],[EventState] ,e.[FaultCode] ,[FaultDescription] ,[Importance] ,[Activation]");
                sql.Append("FROM [dbo].[MTNEvents] e inner join [KTDSSAdmin].[dbo].[AdmRoutineEvents] re ");
                sql.Append("on e.[ModuleNo] = re.[ModuleNo] and e.[ReporterType] = re.[ReporterType] and e.[FaultCode] = re.[FaultCode] ");
                sql.AppendFormat("WHERE {0} ORDER BY [EventSerialNo]", wherePart);
            }
            else
            {
                sql.Append("SELECT [EventSerialNo] ,[HashCode] ,[ModuleNo] ,[RackNo] ,[UnitNo] ,[CardNo] ,[EventDate] ,[EventTime]");
                sql.Append(",[ReporterType],[EventState] ,[FaultCode]");
                sql.Append("FROM [dbo].[MTNEvents] ");
                sql.AppendFormat("WHERE {0} ORDER BY [EventSerialNo]", wherePart);
            }


            string mngConnection = string.Format(@"Data Source={0};Initial Catalog=KTDSSMaintenance;User ID={1};Password={2}", serverName, userName, passName);

            SqlConnection cnn = new SqlConnection(mngConnection);
            SqlCommand cmd = new SqlCommand(sql.ToString(), cnn);
            SqlDataReader dr = null;
            try
            {
                cnn.Open();
                dr = cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
            }
            using (cnn)
            {
                while (dr.Read())
                {
                    yield return new KaraEvent()
                    {
                        MsgID = (int)dr["EventSerialNo"],
                        HashCode = "" + dr["HashCode"],
                        ModuleNo = (byte)dr["ModuleNo"],
                        RackNo = (byte)dr["RackNo"],
                        UnitNo = (byte)dr["UnitNo"],
                        CardNo = (byte)dr["CardNo"],
                        EventDate = "" + dr["EventDate"],
                        EventTime = "" + dr["EventTime"],
                        ReporterType = (byte)dr["ReporterType"],
                        EventState = (byte)dr["EventState"],
                        FaultCode = (byte)dr["FaultCode"],
                        FaultDescription = withFault ? "" + dr["FaultDescription"] : "",
                        Importance = withFault ? (byte)dr["Importance"] : (byte)0,
                        Activation = withFault ? (byte)dr["Activation"] : (byte)0
                    };
                }
            }
            cmd.Dispose();
        }

        public static SqlConnection GetConnection(string serverName, string userName, string passName)
        {
            string mngConnection = string.Format(@"Data Source={0};Initial Catalog=KTDSSMaintenance;User ID={1};Password={2}", serverName, userName, passName);
            return new SqlConnection(mngConnection);
        }

    }
}
