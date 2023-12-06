using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Enterprise;

namespace MessageSenderService
{
	public static  class LogRegister
	{
		//public static void Create()
		//{
		//    try
		//    {
		//        string name = string.Format("SMSLog{0}", DateTime.Now.ToString("yyyy-mm-dd"));

		//        if (!File.Exists(name))
		//        {
		//            using (FileStream fs = File.Create(name))
		//            {
		//                fs.Close();
		//            }
		//        }
		//    }
		//    catch (Exception ex)
		//    {
		//        Logger.Write(ex);
		//    }
		//}

		public static void Write(string value)
		{
			try
			{
				string name = string.Format("SMSLog{0}", DateTime.Now.ToString("yyyy-mm-dd"));

				if (!File.Exists(name))
				{
					using (FileStream fs = File.Create(name))
					{
						fs.Close();
					}
				}

				using (StreamWriter sw = new StreamWriter(name))
				{
					sw.WriteLine(value);
					sw.Close();
				}
			}
			catch (Exception ex)
			{
				Logger.Write(ex);
			}
		}
	}
}
