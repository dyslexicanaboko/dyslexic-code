namespace DepressingFigures.Lib
{
    /// <summary>
    /// Numbers that are unbiased universal truth and do not belong to any one class.
    /// </summary>
    public class Constants
    {
        /// <summary> Seconds in a day. </summary>
        public const double SecondsPerDay = 86400;

        /// <summary> Hours in a day. </summary>
        public const double HoursPerDay = 24;

        /// <summary> Days in a day. </summary>
        public const double DaysPerYear = 365;

        /// <summary> Days in a week. </summary>
        public const double DaysPerWeek = 7;

        /// <summary> Weeks in a year. </summary>
        public const double WeeksPerYear = 52;

        /// <summary> Hours in a year. </summary>
        public const double HoursPerYear = DaysPerYear * HoursPerDay;

        /// <summary> Seconds in a year. </summary>
        public const double SecondsPerYear = DaysPerYear * SecondsPerDay;
    }
}
