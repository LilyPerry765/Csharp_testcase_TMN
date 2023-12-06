using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enterprise;
using System.Globalization;
using System.Text.RegularExpressions;

namespace TMN
{
    
    public partial class LogAlarm
    {
        public override string ToString()
        {
            return Center.DisplayName;
        }

        //~LogAlarm()
        //{
        //    Logger.WriteInfo("~LogAlarm");
        //}
    }
}
