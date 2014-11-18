using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices; 

namespace GamersClock
{
    /// <summary>
    /// Alternative way of obtaining the current DateTime without using DateTime.Now which apparently has a memory leak in it
    /// </summary>
    /// <remarks>
    /// This is where I got this class from:
    /// http://blog.liranchen.com/2010/07/datetimenow-causes-boxing.html
    /// </remarks>
    public static class TimeUtil
    {
        [DllImport("kernel32.dll")]
        static extern void GetLocalTime(out SYSTEMTIME time);

        [StructLayout(LayoutKind.Sequential)]
        private struct SYSTEMTIME
        {
            public ushort Year;
            public ushort Month;
            public ushort DayOfWeek;
            public ushort Day;
            public ushort Hour;
            public ushort Minute;
            public ushort Second;
            public ushort Milliseconds;
        }

        public static DateTime LocalTime
        {
            get
            {
                SYSTEMTIME nativeTime;
                GetLocalTime(out nativeTime);

                return new DateTime(nativeTime.Year, nativeTime.Month, nativeTime.Day,
                                    nativeTime.Hour, nativeTime.Minute, nativeTime.Second,
                                    nativeTime.Milliseconds, DateTimeKind.Local);
            }
        }
    }

}
