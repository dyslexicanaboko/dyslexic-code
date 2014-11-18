using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace GamersClock
{
    public class TimeTracker
    {
        //Default formats
        public const string DATE_FORMAT = "MM/dd/yyyy";
        public const string TIME_FORMAT = "hh:mm:ss tt";

        #region Properties
        /// <summary>
        /// The current Date and Time formats being used for this instance
        /// </summary>
        public Format DateTimeFormat { get; private set; }

        /// <summary>
        /// The start time set for when this instance was instantiated. Used to determine elapsed time.
        /// </summary>
        public DateTime StartTime { get; private set; }
        
        /// <summary>
        /// The property used as the source of time keeping. All questions about what the current time is
        /// are obtained from this property instead of calling any other authority.
        /// </summary>
        public DateTime SourceDate { get { return TimeUtil.LocalTime; } }

        /// <summary>
        /// Get the current Date as a string formatted using the current Date Format
        /// </summary>
        public string CurrentDate { get { return SourceDate.ToString(DateTimeFormat.DateFormat); } }

        /// <summary>
        /// Get the current Time as a string formatted using the current Time Format
        /// </summary>
        public string CurrentTime { get { return SourceDate.ToString(DateTimeFormat.TimeFormat); } }
        
        /// <summary>
        /// Get the elapsed time since this instance has been instantiated. 
        /// This gets the difference between the Source Date and Start Time properties.
        /// </summary>
        public TimeSpan ElapsedTime { get { return SourceDate - StartTime; } }
        #endregion

        /// <summary>
        /// Initialize the Time Tracker using the default Date and Time Format Strings
        /// </summary>
        public TimeTracker()
        {
            DateTimeFormat = new Format()
            {
                DateFormat = DATE_FORMAT,
                TimeFormat = TIME_FORMAT
            };

            StartTime = SourceDate;
        }

        /// <summary>
        /// Initialize the Time Tracker using provided Date and Time Format Strings
        /// </summary>
        /// <param name="dateFormat">Format string for Date portion of DateTime</param>
        /// <param name="timeFormat">Format string for Time portion of DateTime</param>
        public TimeTracker(string dateFormat, string timeFormat)
        {
            DateTimeFormat = new Format()
            {
                DateFormat = dateFormat,
                TimeFormat = timeFormat
            };

            StartTime = SourceDate;
        }

        /// <summary>
        /// Reset the elapsed time by resetting the start time to now
        /// </summary>
        public void ResetElapsedTime()
        {
            StartTime = TimeUtil.LocalTime;
        }

        /// <summary>
        /// Change the Date and Time format strings
        /// </summary>
        /// <param name="format"></param>
        public void SetFormat(Format format)
        {
            DateTimeFormat.DateFormat = format.DateFormat;
            DateTimeFormat.TimeFormat = format.TimeFormat;
        }

        /// <summary>
        /// Get the Current Date and Time format strings as a Format object.
        /// A clone of this instances object is returned.
        /// </summary>
        /// <returns></returns>
        public Format GetCurrentFormat()
        {
            return (Format)DateTimeFormat.Clone();
        }

        /// <summary>
        /// Get the default Date and Time format strings as a Format object
        /// utilizing the TimeTracker's default format strings.
        /// </summary>
        /// <returns></returns>
        public static Format GetDefaultFormat()
        {
            return new Format()
            {
                DateFormat = DATE_FORMAT,
                TimeFormat = TIME_FORMAT
            };
        }

        /// <summary>
        /// Inner class that provides a way to package up the Date and Time format strings separately.
        /// </summary>
        public class Format : ICloneable
        {
            /// <summary>
            /// Format string only for the Date part of a DateTime object
            /// </summary>
            public string DateFormat { get; set; }
            
            /// <summary>
            /// Format string only for the Time part of a DateTime object
            /// </summary>
            public string TimeFormat { get; set; }

            /// <summary>
            /// Returns an independent instance of this object
            /// </summary>
            /// <returns>Format object as object</returns>
            public object Clone()
            {
                return new Format()
                {
                    DateFormat = DateFormat,
                    TimeFormat = TimeFormat
                };
            }
        }
    }
}
