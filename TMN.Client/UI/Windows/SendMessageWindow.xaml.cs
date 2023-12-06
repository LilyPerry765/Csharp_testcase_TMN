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
using Enterprise;

namespace TMN
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
            objSMS.DeliveryReport = false;
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
                MessageBox.Show("پیام با موفقیت ارسال شد", "اطلاع", MessageBoxButton.OKCancel);
                return result;
            }
            catch (Exception)
            {
                MessageBox.Show("پیام با موفقیت ارسال نشد", "خطا", MessageBoxButton.OKCancel);
                return string.Empty;
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            Send(txtMessage.Text, txtNumber.Text);
        }
    }
}
