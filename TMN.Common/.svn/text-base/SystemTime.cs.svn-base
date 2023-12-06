using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace TMN
{
    public class SystemTime
    {
        //[DllImport("coredll.dll")]
        //private extern static void GetSystemTime(ref SYSTEMTIME lpSystemTime);

        //[DllImport("coredll.dll")]
        //private extern static uint SetSystemTime(ref SYSTEMTIME lpSystemTime);
        [DllImport("kernel32.dll")]
        static extern bool SetLocalTime([In] ref SYSTEMTIME lpLocalTime);

        [StructLayout(LayoutKind.Sequential)]
        public struct SYSTEMTIME
        {
            public ushort wYear;
            public ushort wMonth;
            public ushort wDayOfWeek;
            public ushort wDay;
            public ushort wHour;
            public ushort wMinute;
            public ushort wSecond;
            public ushort wMilliseconds;
        }

        //public static DateTime GetTime()
        //{
        //    // Call the native GetSystemTime method 
        //    // with the defined structure.
        //    SYSTEMTIME stime = new SYSTEMTIME();
        //    GetSystemTime(ref stime);
        //    return new DateTime(stime.wYear, stime.wMonth, stime.wDay, stime.wHour, stime.wMinute, stime.wSecond, stime.wMilliseconds);
        //    // Show the current time.           
        //    //MessageBox.Show("Current Time: " +
        //    //    stime.wHour.ToString() + ":"
        //    //    + stime.wMinute.ToString());
        //}

        public static void SetTime(DateTime time )
        {
            // Call the native GetSystemTime method 
            // with the defined structure.
            SYSTEMTIME systime = new SYSTEMTIME();
            //GetSystemTime(ref systime);

            // Set the system clock ahead one hour.
            systime.wDay = (ushort)time.Day; systime.wMonth = (ushort)time.Month; systime.wYear = (ushort)time.Year;
            systime.wHour = (ushort)time.Hour; systime.wMinute = (ushort)time.Minute; systime.wSecond = (ushort)time.Second; systime.wMilliseconds = (ushort)time.Millisecond;
            SetLocalTime(ref systime);
            //MessageBox.Show("New time: " + systime.wHour.ToString() + ":"
            //    + systime.wMinute.ToString());
        }

    }
}
