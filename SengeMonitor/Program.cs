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


    }
}