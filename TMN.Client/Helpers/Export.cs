using System;
using System.Data;
using System.IO;
using System.Text;
using System.Xml;
using System.Threading;
using System.Collections;
using System.Collections.Generic;

namespace TMN
{
	public static class Export
	{
		public static void ToText(DataTable table, string path)
		{
			if (table != null)
			{
				Thread t = new Thread(() =>
				{
					StreamWriter sw = new StreamWriter(path, false, Encoding.Unicode);
					foreach (DataRow row in table.Rows)
					{
						sw.Write(row[3].ToString());

						sw.WriteLine();
						sw.WriteLine();
						sw.WriteLine();
						sw.WriteLine();
						sw.WriteLine();
					}
					sw.Flush();
					sw.Close();
				});
				t.Start();
			}
		}

		public static void ToXML(DataTable table, string path)
		{
			if (table != null)
			{
				Thread t = new Thread(() =>
				{
					XmlWriterSettings setting = new XmlWriterSettings
					{
						Indent = false,
						Encoding = System.Text.Encoding.UTF8,
						NewLineChars = "\r\n"
					};

					XmlWriter xw = XmlWriter.Create(path, setting);

					xw.WriteComment(DateTime.Now.ToString());

					table.TableName = "LogAlarms";
					table.WriteXml(xw);

					xw.Flush();
					xw.Close();
				});
				t.Start();
			}
		}

		public static void ToExcel(DataTable table, string path)
		{
			if (table != null)
			{
				Thread t = new Thread(() =>
				{
					Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
					Microsoft.Office.Interop.Excel.Workbook book = app.Workbooks.Add(1);
					Microsoft.Office.Interop.Excel.Worksheet sheet = book.Worksheets[1];

					sheet.Name = "Sheet 1";

					for (int row = 0; row < table.Rows.Count; row++)
					{
						sheet.Cells.set_Item(row + 1, 1, table.Rows[row].ItemArray[3].ToString());

						//for (int col = 0; col < table.Columns.Count; col++)
						//{
						//    sheet.Cells.set_Item(row + 1, col + 1, table.Rows[row].ItemArray[col].ToString());
						//}
					}
					app.Visible = true;
				});
				t.Start();
			}
		}


		public static void ToText(List<ArchiveService.LogAlarm> list, string path)
		{
			if (list != null)
			{
				Thread t = new Thread(() =>
					{
						using (StreamWriter sw = new StreamWriter(path, false, Encoding.Unicode))
						{
							foreach (ArchiveService.LogAlarm item in list)
							{
								sw.WriteLine();
								sw.Write(item.Data);
								sw.WriteLine();
								sw.Write("-------------------------------------------------------");
								sw.WriteLine();
							}
							sw.Flush();
							sw.Close();
						}
					});
				t.Start();
			}
		}

		public static void ToExcel(List<ArchiveService.LogAlarm> list, string pat)
		{
			//if (list != null)
			//{
			//    Thread t = new Thread(() =>
			//    {
			//        Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
			//        Microsoft.Office.Interop.Excel.Workbook book = app.Workbooks.Add(1);
			//        Microsoft.Office.Interop.Excel.Worksheet sheet = book.Worksheets[1];

			//        sheet.Name = "Sheet 1";

			//        for (int row = 0; row < list.Count ; row++)
			//        {
			//            sheet.Cells.set_Item(row + 1, 1,list[row].ToString()); //  table.Rows[row].ItemArray[3].ToString());

			//            //for (int col = 0; col < table.Columns.Count; col++)
			//            //{
			//            //    sheet.Cells.set_Item(row + 1, col + 1, table.Rows[row].ItemArray[col].ToString());
			//            //}
			//        }
			//        app.Visible = true;
			//    });
			//    t.Start();
			//}
		}

		public static void ToXML(List<ArchiveService.LogAlarm> list, string path)
		{
			if (list != null)
			{
				Thread t = new Thread(() =>
				{
					XmlWriterSettings setting = new XmlWriterSettings
					{
						Indent = false,
						Encoding = System.Text.Encoding.UTF8,
						NewLineChars = "\r\n"
					};

					using (XmlWriter xw = XmlWriter.Create(path, setting))
					{
						xw.WriteStartDocument();
						xw.WriteComment(DateTime.Now.ToString());
						xw.WriteStartElement("ArchiveAlarm");

						foreach (ArchiveService.LogAlarm item in list)
						{
							xw.WriteStartElement("Log");
							xw.WriteElementString("Title", item.Title );
							xw.WriteElementString("Data", item.Data );
							xw.WriteEndElement();
						}

						xw.WriteEndElement();
						xw.WriteEndDocument();
					}
				});
				t.Start();
			}
		}


		public static void ToText(List<ArchiveService.UserLog> list, string path)
		{
			if (list != null)
			{
				Thread t = new Thread(() =>
				{
					using (StreamWriter sw = new StreamWriter(path, false, Encoding.Unicode))
					{
						foreach (ArchiveService.UserLog item in list)
						{
							
							sw.Write(item.Date+"       " + item.Action+"       "+ item.Description);
							sw.WriteLine();
							sw.WriteLine();
						}
						sw.Flush();
						sw.Close();
					}
				});
				t.Start();
			}
		}

		public static void ToExcel(List<ArchiveService.UserLog> list, string pat)
		{
			//if (list != null)
			//{
			//    Thread t = new Thread(() =>
			//    {
			//        Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
			//        Microsoft.Office.Interop.Excel.Workbook book = app.Workbooks.Add(1);
			//        Microsoft.Office.Interop.Excel.Worksheet sheet = book.Worksheets[1];

			//        sheet.Name = "Sheet 1";

			//        for (int row = 0; row < list.Count ; row++)
			//        {
			//            sheet.Cells.set_Item(row + 1, 1,list[row].ToString()); //  table.Rows[row].ItemArray[3].ToString());

			//            //for (int col = 0; col < table.Columns.Count; col++)
			//            //{
			//            //    sheet.Cells.set_Item(row + 1, col + 1, table.Rows[row].ItemArray[col].ToString());
			//            //}
			//        }
			//        app.Visible = true;
			//    });
			//    t.Start();
			//}
		}

		public static void ToXML(List<ArchiveService.UserLog> list, string path)
		{
			if (list != null)
			{
				Thread t = new Thread(() =>
				{
					XmlWriterSettings setting = new XmlWriterSettings
					{
						Indent = false,
						Encoding = System.Text.Encoding.UTF8,
						NewLineChars = "\r\n"
					};

					using (XmlWriter xw = XmlWriter.Create(path, setting))
					{
						xw.WriteStartDocument();
						xw.WriteComment(DateTime.Now.ToString());
						xw.WriteStartElement("UserLog");

						foreach (ArchiveService.UserLog item in list)
						{
							xw.WriteStartElement("Log");
							xw.WriteElementString("Date", item.Date.ToString());
							xw.WriteElementString("Action", item.Action);
							xw.WriteElementString("Description", item.Description);
							xw.WriteEndElement();
						}

						xw.WriteEndElement();
						xw.WriteEndDocument();
					}
				});
				t.Start();
			}
		}
	}
}
