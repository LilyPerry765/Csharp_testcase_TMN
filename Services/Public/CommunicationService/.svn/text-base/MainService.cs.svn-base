using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Enterprise;
using System.IO;
using System.Reflection;
using System.Diagnostics;

namespace TMN
{
	public class MainService : IMainService
	{
		public void CheckAccount(string clientUserName, string clientPassword)
		{
			try
			{
				string assemblyDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
				string accountCreator = Path.Combine(assemblyDir, "AccountCreator.exe");

				if (File.Exists(accountCreator))
				{
					Logger.WriteInfo("Starting check account...");

                    ProcessStartInfo processInfo = new ProcessStartInfo(accountCreator, string.Format(" {0} {1} ", clientUserName, clientPassword));
					processInfo.WorkingDirectory = assemblyDir;

					new System.Threading.Thread((arg) =>
					{
						try
						{
							Process.Start((ProcessStartInfo)arg);
						}
						catch (Exception ex)
						{
							Logger.Write(ex);
						}
					}).Start(processInfo);

					Logger.WriteDebug("Check account started.");
				}
				else
				{
					Logger.WriteWarning("Check account ({0}) was not found. ", accountCreator);
				}
			}
			catch (Exception ex )
			{

				Logger.Write(ex);
			}
		}

        public void UpgradeService(string serverUserName, string serverPassword)
		{
			try
			{
				string assemblyDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
				string updaterPath = Path.Combine(assemblyDir, "TMN.Updater.exe");
				if (File.Exists(updaterPath))
				{
					Logger.WriteInfo("Starting updater process...");
					//var startInfo = new ProcessStartInfo(updaterPath, string.Format("/silent /username {0} /password {1}", Setting.UpdaterUserName, Setting.UpdaterPassword));
                    var startInfo = new ProcessStartInfo(updaterPath, string.Format("/silent /username {0} /password {1}", serverUserName, serverPassword));
					startInfo.WorkingDirectory = assemblyDir;
					new System.Threading.Thread((arg) =>
					{
						try
						{
							Process.Start((ProcessStartInfo)arg);
						}
						catch (Exception ex)
						{
							Logger.Write(ex);
						}
					}).Start(startInfo);
					Logger.WriteDebug("Updater process started.");
				}
				else
				{
					Logger.WriteWarning("Updater({0}) was not found. Update failed.", updaterPath);
					//this.Stop();
				}
			}
			catch (Exception ex)
			{
				Logger.Write(ex);
				//this.Stop();
			}
		}
	}
}
