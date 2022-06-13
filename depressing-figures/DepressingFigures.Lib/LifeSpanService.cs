namespace DepressingFigures.Lib
{
    public class LifeSpanService
    {
        public const double SecondsInDay = 86400;
        public const double HoursPerDay = 24;
        public const double DaysInYear = 365;
        public const double HoursInYear = DaysInYear * HoursPerDay;
        public const double SecondsInYear = DaysInYear * SecondsInDay;

        public Estimations Calculate(Profile profile)
        {
            var result = new Estimations();

            //Load gender specific values
            Person person = profile.BiologicalGender == Gender.Male ? new Male() : new Female();

            person.SetAge(profile.BirthDate);

            //Only override the max age if an age is provided
            if (profile.ExpectedMaxAgeYears.HasValue) person.OverrideMaxAgeYears(profile.ExpectedMaxAgeYears.Value);

            var maxSeconds = SecondsInYear * person.MaxAgeYears;
            var spentYears = person.Spent.TotalDays / DaysInYear;
            var maxHours = person.MaxAgeYears * HoursInYear;

            result.Person = person;
            result.Expiration = person.Expiration;
            result.Years = SetRatios(person.MaxAgeYears, spentYears);
            result.Seconds = SetRatios(maxSeconds, person.Spent.TotalSeconds);
            (result.SleepHours, result.AwakeHours) = CalculateSleepHours(person, maxHours, profile.SleepHoursPerNight);
            
            if(profile.IsEmployed) result.WorkingHours = CalculateWorkingHours(person, maxHours, profile.WorkHoursPerWeek);

            return result;
        }

        private Ratios CalculateWorkingHours(Person person, double maxHours, int workHoursPerWeek)
        {
            //Number of hours spent working in lifetime
            var working = person.Spent.TotalDays * workHoursPerWeek;

            var slaving = SetRatios(maxHours, working);

            return slaving;
        }

        private (Ratios, Ratios) CalculateSleepHours(Person person, double maxHours, int sleepHoursPerNight)
        {
            //Number of hours spent sleeping in lifetime
            var asleep = person.Spent.TotalDays * sleepHoursPerNight;
            var awake = HoursPerDay - asleep;

            var sleeping = SetRatios(maxHours, asleep);
            var waking = SetRatios(maxHours, awake);

            return (sleeping, waking);
        }

        private Ratios SetRatios(double total, double part)
        {
            var r = new Ratios();

            r.Remaining = total - part;
            r.Spent = part;
            r.PercentSpent = SafeDivide(r.Spent, total);
            r.PercentRemaining = 1.0 - r.PercentSpent;

            return r;
        }

        private double SafeDivide(double numerator, double denominator)
        {
            if (denominator == 0) return 0;

            var r = numerator / denominator;

            return r;
        }
    }
}