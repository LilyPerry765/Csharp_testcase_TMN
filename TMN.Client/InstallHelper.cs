using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Xml;
using System.IO;


namespace TMN
{
    [RunInstaller(true)]
    public partial class InstallHelper : Installer
    {
        public InstallHelper()
        {
            
        }
        

        public override void Install(IDictionary stateSaver)
        {
            base.Install(stateSaver);

            UpdateConnectionString();
            UpdateSettings();
        }

        private void UpdateSettings()
        {
            try
            {
                Log("start set update folder on updatesetting.xml");
                // In order to get the value from the textBox named 'EDITA1' I needed to add the line:
                // '/PathValue = [EDITA1]' to the CustomActionData property of the CustomAction We added. 
                string updateServer = Context.Parameters["US"];
                Log("update server is : " + updateServer);
                string updateFolder = Context.Parameters["UF"];
                Log("update folder is : " + updateFolder);
                string updatepath = "\\\\" + updateServer + "\\" + updateFolder;
                Log("update path is : "  + updatepath);

                // Get the path to the executable file that is being installed on the target computer
                string assemblypath = Context.Parameters["assemblypath"];
                string updateSettingPath = System.IO.Path.GetDirectoryName(assemblypath) + @"\UpdateSettings.xml";
                Log("path of xml file is : " + updateSettingPath);

                // Write the path to the app.config file
                XmlDocument doc = new XmlDocument();
                doc.Load(updateSettingPath);
                Log("UpdateSettings.xml loaded");

                XmlNode updaterSettingsNode = null;
                foreach (XmlNode node in doc.ChildNodes)
                    if (node.Name == "UpdaterSettings")
                        updaterSettingsNode = node;

                if (updaterSettingsNode != null)
                {
                    Log("xml node UpdaterSettings finded");
                    XmlNode updateUrlNode = null;
                    foreach (XmlNode node in updaterSettingsNode.ChildNodes)
                        if (node.Name == "UpdateUrl")
                            updateUrlNode = node;

                    if (updateUrlNode != null)
                    {
                        Log("xml node UpdateUrl finded");
                        //XmlCDataSection tex;
                        //tex = doc.CreateCDataSection(updatepath);
                        //updateUrlNode.RemoveAll();
                        updateUrlNode.InnerText = updatepath;
                        Log("new value of updateUrlNode is: " + updateUrlNode.InnerText);
                        doc.Save(updateSettingPath);
                        Log("updateSetting.xml file saved");
                    }
                    
                }
            }
            catch (FormatException e)
            {
                Log("error : " + e.ToString());
            }
        }

        private void UpdateConnectionString()
        {
            try
            {
                // In order to get the value from the textBox named 'EDITA1' I needed to add the line:
                // '/PathValue = [EDITA1]' to the CustomActionData property of the CustomAction We added. 

                string serverName = Context.Parameters["SN"];

                string databaseName = Context.Parameters["DBN"];
                string userName = Context.Parameters["UN"];
                string password = Context.Parameters["PN"];



                // Get the path to the executable file that is being installed on the target computer
                string assemblypath = Context.Parameters["assemblypath"];
                string appConfigPath = assemblypath + ".config";

                // Write the path to the app.config file
                XmlDocument doc = new XmlDocument();
                doc.Load(appConfigPath);

                XmlNode configuration = null;
                foreach (XmlNode node in doc.ChildNodes)
                    if (node.Name == "configuration")
                        configuration = node;

                if (configuration != null)
                {
                    XmlNode connectionsNode = null;
                    foreach (XmlNode node in configuration.ChildNodes)
                        if (node.Name == "connectionStrings")
                            connectionsNode = node;

                    if (connectionsNode != null)
                    {
                        XmlNode conNode = null;
                        XmlNode conBackNode = null;
                        foreach (XmlNode node in connectionsNode.ChildNodes)
                        {
                            if (node.Attributes["name"] != null)
                            {
                                if (node.Attributes["name"].Value == "TMN.Properties.Settings.TMNConnectionString")
                                    conNode = node;
                                if (node.Attributes["name"].Value == "TMN.Properties.Settings.TMNConnectionStringBack")
                                    conBackNode = node;
                            }
                        }

                        if (conNode != null)
                        {
                            XmlAttribute att = conNode.Attributes["connectionString"];
                            att.Value = string.Format("Data Source={0};Initial Catalog={1};User ID={2};Password={3}", serverName, databaseName, userName, password); // Update the configuration file

                            if (conBackNode != null)
                            {
                                XmlAttribute backatt = conBackNode.Attributes["connectionString"];
                                backatt.Value = string.Format("Data Source={0};Initial Catalog={1};User ID={2};Password={3}", serverName.Replace("130", "132"), databaseName, userName, password); // Update the configuration file
                            }
                            // Save the configuration file
                            doc.Save(appConfigPath);
                        }
                    }


                    if (serverName == "10.2.16.130") // || serverName == "10.7.64.130")
                    {
                        // Get the 'appSettings' node
                        XmlNode applicationSettingNode = null;
                        foreach (XmlNode node in configuration.ChildNodes)
                            if (node.Name == "applicationSettings")
                                applicationSettingNode = node;

                        XmlNode tmnSettingNode = null;
                        foreach (XmlNode node in applicationSettingNode.ChildNodes)
                            if (node.Name == "TMN.Properties.Settings")
                                tmnSettingNode = node;

                        if (tmnSettingNode != null)
                        {
                            // Get the node with the attribute key="FilePath"
                            XmlNode licenseNode = null;
                            foreach (XmlNode node in tmnSettingNode.ChildNodes)
                            {
                                if (node.Attributes["name"] != null)
                                    if (node.Attributes["name"].Value == "LicenseKey")
                                        licenseNode = node;
                            }

                            if (licenseNode != null)
                            {
                                XmlNode node = licenseNode.ChildNodes[0];
                                node.InnerText = LicenseManager.GetLicense(); // Update the configuration file

                                // Save the configuration file
                                doc.Save(appConfigPath);
                            }
                        }
                    }
                }
            }
            catch (FormatException e)
            {
                Log("error" + e.ToString());
            }
        }

        private void Log(string message)
        {
            File.AppendAllText(Path.Combine(Path.GetDirectoryName(Context.Parameters["assemblypath"]), "InstallerSetup.Log"), message + "\r\n");
        }
    }
}
