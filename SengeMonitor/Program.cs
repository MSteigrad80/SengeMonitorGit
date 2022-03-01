using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace SengeMonitor
{
    public class Program
    {
        string DSN = string.Format(@"C:\SengDataNeu\");
        string DSNNeu = string.Format(@"C:\Users\ManfredSteigrad\OneDrive - M.Steigrad\SenDataAquire\");

        private int TimeOutMillis = 2000;
        FileSystemWatcher fsw = new FileSystemWatcher(@"C:\SengDataNeu\");
        System.Threading.Timer m_timer = null;
        List<string> files  = new List<string>();

        static void main(string[] args)
        {
            Program program = new Program();
            program.Initial();
        }
        public void Initial()
        {
            fsw.NotifyFilter = NotifyFilters.LastWrite;
            fsw.Filter = "*.csv";
            fsw.Changed += new FileSystemEventHandler(OnChanged);
            fsw.Created += new FileSystemEventHandler(OnChanged);
            fsw.Deleted += new FileSystemEventHandler(OnChanged);

            fsw.EnableRaisingEvents = true;
            if (m_timer == null)
            {
                m_timer = new System.Threading.Timer(new
                    System.Threading.TimerCallback(OnWatchedFileChange), null,
                    Timeout.Infinite, Timeout.Infinite);
            }
            Console.WriteLine("Press \'Enter\' to quit");
            Console.ReadLine();
        }
        void OnChanged(Object s, FileSystemEventArgs e)
        {
            Mutex mutex = new Mutex(false, "FSW");
            mutex.WaitOne();
            if (!files.Contains(e.Name))
            {
                files.Add(e.Name);
            }
            mutex.ReleaseMutex();
            m_timer.Change(TimeOutMillis, Timeout.Infinite);
        }
        public void OnWatchedFileChange(object state)
        {
            List<string> backup = new List<string>();
            Mutex mutex = new Mutex(false, "FSW");
            mutex.WaitOne();
            backup.AddRange(files);
            files.Clear();
            mutex.ReleaseMutex();
            foreach(string file in backup)
            {
                Console.WriteLine(DSN + file + " changed");
                File.Copy(DSN + file, DSNNeu + file, true);
            }
        }
    }
}