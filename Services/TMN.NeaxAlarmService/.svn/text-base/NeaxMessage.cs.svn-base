using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using Enterprise;

namespace TMN
{
    class NeaxMessage
    {
        public int MSG_ID
        {
            get;
            set;
        }

        public int MESSAGE_STATUS
        {
            get;
            set;
        }

        public DateTime MESSAGE_DATE
        {
            get;
            set;
        }

        public string ALARM_BODY
        {
            get;
            set;
        }

        public int ALARM_CLASS
        {
            get;
            set;
        }

        public int MESSAGE_NUMBER
        {
            get;
            set;
        }

        public int RECOVER_NUMBER
        {
            get;
            set;
        }

        public string KEY_INFORMATION
        {
            get;
            set;
        }

        public string DETAIL
        {
            get;
            set;
        }

        public static IEnumerable<NeaxMessage> GetMessages(string accessFilePath, string selectCommandText)
        {
            List<NeaxMessage> result = new List<NeaxMessage>();
            using (OleDbConnection cnn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0; Mode=Read; Data Source=" + accessFilePath))
            {
                OleDbCommand cmd = new OleDbCommand(selectCommandText, cnn);
                OleDbDataReader dr = null;
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
                        result.Add( new NeaxMessage()
                        {
                            ALARM_BODY = (string)dr["Alarm_Body"],
                            ALARM_CLASS = (int)dr["Alarm_Class"],
                            MESSAGE_DATE = (DateTime)dr["Message_Date"],
                            DETAIL = (string)dr["Detail"],
                            KEY_INFORMATION = (string)dr["KEY_INFORMATION"],
                            MSG_ID = (int)dr["MSG_ID"],
                            RECOVER_NUMBER = (int)dr["RECOVER_NUMBER"],
                            MESSAGE_NUMBER = (int)dr["MESSAGE_NUMBER"],
                            MESSAGE_STATUS = (int)dr["MESSAGE_STATUS"]
                        });
                    }
                }
            }
            return result;
        }
    }
}
