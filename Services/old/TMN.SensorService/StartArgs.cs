using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TMN
{
    internal static class StartArgs
    {
        public static bool ShowDebugLogs;
        public static bool ShowWatchDogLogs;
        public static bool ShowPortDataLogs;
        public static int ResetTimeout = 2;
        public static int RequestInterval = 1;
    }
}
