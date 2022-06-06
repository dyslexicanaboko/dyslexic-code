namespace DepressingFigures.Lib
{
    public enum Gender { Male, Female }

    public class Profile
    {
        /// <summary>Used to determine the user's age.</summary>
        public DateTime BirthDate { get; set; }

        /// <summary>Biological gender of the user at birth. Women live longer than men typically.</summary>
        public Gender BiologicalGender { get; set; }

        /// <summary>Hours of sleep per night.</summary>
        public int SleepHoursPerNight { get; set; }

        /// <summary>Does the user have a job, job can be anything.</summary>
        public bool IsEmployed { get; set; }

        /// <summary>Hours spent working per week.</summary>
        public int WorkHoursPerWeek { get; set; }

        /* Theoretical bad habits
         * Example: Smoking subtracts X number of years off your life
         * 
         * Smoking
         * Cancer risk
         * Gender
         */
    }
}
