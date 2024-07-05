using C = DepressingFigures.Lib.Constants;

namespace DepressingFigures.Lib
{
    public abstract class Person
    {
        /// <summary> Estimated max expected age in years before expiration. </summary>
        public abstract int MaxYears { get; protected set; }

        /// <summary> Max weeks available to spend. </summary>
        public double MaxWeeks { get; private set; }

        /// <summary> Max life days available to spend. </summary>
        public double MaxDays { get; private set; }

        /// <summary> Max life hours available to spend. </summary>
        public double MaxHours { get; private set; }

        /// <summary> Max life seconds available to spend. </summary>
        public double MaxSeconds { get; private set; }

        /// <summary> Max amount of sleep hours available to spend. </summary>
        public double MaxSleepHours { get; private set; }

        /// <summary> Max amount of awake hours available to spend. </summary>
        public double MaxAwakeHours { get; private set; }

        /// <summary> Max amount of work hours available to spend (based on 52 week year). </summary>
        public double MaxWorkHours { get; private set; }

        /// <summary> Years of life spent already. </summary>
        public double SpentYears { get; private set; }

        /// <summary> Weeks of life spent already. </summary>
        public double SpentWeeks { get; private set; }

        /// <summary> Date of birth. </summary>
        public DateTime BirthDate { get; private set; }

        /// <summary> Current age. </summary>
        public DateTime Current { get; private set; }

        /// <summary> Estimated expiration age. </summary>
        public DateTime Expiration { get; private set; }

        /// <summary> Time spent as of today. </summary>
        public TimeSpan Spent { get; private set; }
        
        /// <summary> Time remaining as of today. </summary>
        public TimeSpan Remainder { get; private set; }

        public void SetAge(DateTime birthDate)
        {
            //TODO: Should be injected?
            var dtm = DateTime.Now.Date;

            BirthDate = birthDate;

            //Current age
            Current = new DateTime(dtm.Year, birthDate.Month, birthDate.Day);

            Expiration = birthDate.AddYears(MaxYears);

            Spent = dtm - birthDate; //From birth to today
            
            Remainder = Expiration - Current; //From today to expiration
        }

        /// <summary>
        /// Override the default CDC data provided by the government about 
        /// the maximum life expectancy age of a gender.
        /// </summary>
        /// <param name="years"></param>
        public void OverrideMaxAgeYears(int years) => MaxYears = years;

        /// <summary>
        /// Set constants based on this person's profile answers. These constants will be used
        /// to calculate a personalized estimation.
        /// </summary>
        /// <param name="sleepHoursPerNight">Hours of sleep per night (per day)</param>
        public void SetConstants(double sleepHoursPerNight)
        {
            MaxWeeks = C.WeeksPerYear * MaxYears;
            
            MaxDays = C.DaysPerYear * MaxYears;

            MaxHours = C.HoursPerYear * MaxYears;

            MaxSeconds = C.SecondsPerYear * MaxYears;

            MaxSleepHours = MaxDays * sleepHoursPerNight;
            
            MaxAwakeHours = MaxDays * (C.HoursPerDay - sleepHoursPerNight);

            SpentYears = Spent.TotalDays / C.DaysPerYear;

            SpentWeeks = Spent.TotalDays / C.DaysPerWeek;
        }

        public void SetWorkHours(double workHoursPerWeek)
        {
            MaxWorkHours = MaxWeeks * workHoursPerWeek;
        }
    }
}
