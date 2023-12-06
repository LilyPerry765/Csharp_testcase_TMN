using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMN;
using Enterprise;

namespace MessageSenderService
{
	public class ServiceManager
	{
		mCore.SMS objSMS = new mCore.SMS();

		public ServiceManager()
		{
			mCore.License objLic = objSMS.License();
			objLic.Company = "ZURICH INSURANCE GROUP (HK)";
			objLic.LicenseType = "PRO-DISTRIBUTION";
			objLic.Key = "FJ4F-C51W-MCER-BRAW";
		}

		private void SetParameters()
		{
			string port = (string)RegSettings.Get("MessageSenderPortName");

			objSMS.Port = port;
			objSMS.BaudRate = (mCore.BaudRate.BaudRate_9600);
			objSMS.DataBits = (mCore.DataBits.Eight);
			objSMS.Parity = (mCore.Parity.None);
			objSMS.StopBits = (mCore.StopBits.One);
			objSMS.FlowControl = (mCore.FlowControl.None);
			objSMS.DisableCheckPIN = false;
			//objSMS.Timeout = 30;
			objSMS.SendDelay = 1;
			objSMS.SendRetry = 1;

			objSMS.Encoding = mCore.Encoding.Unicode_16Bit;
			objSMS.DeliveryReport = false;
			objSMS.LongMessage = mCore.LongMessage.Concatenate;
			objSMS.SMSC = "+9891100500";

			objSMS.NewDeliveryReport += new mCore.SMS.NewDeliveryReportEventHandler(objSMS_NewDeliveryReport);
		}

		void objSMS_NewDeliveryReport(object sender, mCore.NewDeliveryReportEventArgs e)
		{
			if (e.Status)
				Logger.WriteInfo("one message send to {0} . the SendTime is {1} ", e.Phone, e.SentTimeStamp);
			else
				Logger.WriteError("could not send message to {0}", e.Phone);
		}

        public bool Connect()
        {

            if (!objSMS.IsConnected)
            {
                //objSMS.Connect();
                //SetParameters();
                SetParameters();
                objSMS.Connect();

                //Logger.WriteInfo("connect to port {0} .", objSMS.Port);
                return true;
            }
            else
            {
                Logger.WriteError("could not connect to port {0} .", objSMS.Port);
                return false;
            }
        }

		public void Disconnect()
		{
			objSMS.Disconnect();
		}

		public string Send(string message, string number)
		{
            if (!objSMS.IsConnected)
            {
                SetParameters();
                objSMS.Connect();
            }
			string result = objSMS.SendSMS(number, message, false);
			return result;
		}
	}
}
