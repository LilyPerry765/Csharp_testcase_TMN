using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TMN;
using Enterprise;

namespace MessageSenderService
{
    public partial class SendMessageWindow : Window
    {
        mCore.SMS objSMS = new mCore.SMS();

        public SendMessageWindow()
        {
            InitializeComponent();

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
            objSMS.DeliveryReport = true ;
            objSMS.LongMessage = mCore.LongMessage.Concatenate;
            objSMS.SMSC = "+9891100500";
        }

        private bool Connect()
        {
            if (objSMS.IsConnected)
                objSMS.Disconnect();

            SetParameters();

            if (objSMS.Connect())
                return true;
            else
                return false;
        }

        private void Disconnect()
        {
            objSMS.Disconnect();
        }

        public string Send(string message, string number)
        {
            try
            {
                Connect();
                string result = objSMS.SendSMS(number, message, false);
                Disconnect();
                TMN.MessageBox.Show("پیام با موفقیت ارسال شد", "اطلاع", MessageBoxButton.OK );
                return result;
            }
            catch (Exception ex)
            {
                TMN.MessageBox.Show("خطا در ارسال پیام", "خطا", MessageBoxButton.OK );
                Logger.Write(ex);
                return string.Empty;
            }
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            Send(txtMessage.Text, txtNumber.Text);
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
