using System;
using System.IO;
using System.Security.Permissions;
using Enterprise;
namespace TMN
{


    public class Watcher : IDisposable
    {

        //private static Watcher instance;

        //public static Watcher Instance
        //{
        //    get
        //    {
        //        if (instance == null)
        //            instance = new Watcher();
        //        return instance;
        //    }
        //}


        private FileSystemWatcher watcher = null;
        public void Run(string directoryName)
        {
            try
            {
                if(watcher != null)
                    watcher.Dispose();
                // Create a new FileSystemWatcher and set its properties.
                watcher = new FileSystemWatcher();
                watcher.Path = directoryName;
                /* Watch for changes in LastAccess and LastWrite times, and
                   the renaming of files or directories. */
                watcher.NotifyFilter = NotifyFilters.LastWrite;

                // Only watch text files.
                watcher.Filter = "*.*";

                watcher.IncludeSubdirectories = false;
                // Add event handlers.
                //instance.Changed += new FileSystemEventHandler(OnChanged);
                //instance.Created += new FileSystemEventHandler(OnChanged);
                //instance.Deleted += new FileSystemEventHandler(OnChanged);
                //instance.Renamed += new RenamedEventHandler(OnRenamed);

                // Begin watching.
                watcher.EnableRaisingEvents = true;
                watcher.Error += new ErrorEventHandler(watcher_Error);
                // Wait for the user to quit the program.
            }
            catch (ArgumentException ex)
            {
                throw new DirectoryNotFoundException("Watcher can't set to folder.", ex);
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
            }
        }

        private void watcher_Error(object sender, ErrorEventArgs e)
        {
            Logger.Write(e.GetException(), "Watcher error");
            Run(watcher.Path);
            Logger.WriteInfo("Watcher running ...");
        }

        public event FileSystemEventHandler Changed
        {
            add
            {
                watcher.Changed += value;
            }
            remove
            {
                watcher.Changed -= value;
            }
        }

        public void Dispose()
        {
            if(watcher != null)
                watcher.Dispose();
        }

    }

}
