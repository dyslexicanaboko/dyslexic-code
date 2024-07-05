using C = DepressingFigures.Lib.Constants;

namespace DepressingFigures.Lib
{
    public class LifeSpanService
    {   
        public Estimations Calculate(Profile profile)
        {
            var result = new Estimations();

            //Load gender specific values
            Person person = profile.BiologicalGender == Gender.Male ? new Male() : new Female();

            person.SetAge(profile.BirthDate);

            //Only override the max age if an age is provided
            if (profile.ExpectedMaxAgeYears.HasValue) person.OverrideMaxAgeYears(profile.ExpectedMaxAgeYears.Value);

            person.SetConstants(profile.SleepHoursPerNight);

            if (profile.IsEmployed) person.SetWorkHours(profile.WorkHoursPerWeek);

            result.Person = person;
            result.Expiration = person.Expiration;
            result.Years = SetRatios(person.MaxYears, person.SpentYears);
            result.Seconds = SetRatios(person.MaxSeconds, person.Spent.TotalSeconds);
            (result.SleepHours, result.AwakeHours) = CalculateSleepHours(person, profile.SleepHoursPerNight);
            
            if(profile.IsEmployed) result.WorkingHours = CalculateWorkingHours(person, profile.WorkHoursPerWeek);

            return result;
        }

        private Ratios CalculateWorkingHours(Person person, int workHoursPerWeek)
        {
            //Number of hours spent working in lifetime
            var working = person.SpentWeeks * workHoursPerWeek;

            var slaving = SetRatios(person.MaxWorkHours, working);

            return slaving;
        }

        private (Ratios, Ratios) CalculateSleepHours(Person person, int sleepHoursPerNight)
        {
            //Number of hours spent sleeping in lifetime
            var asleep = person.Spent.TotalDays * sleepHoursPerNight;
            var awake = person.Spent.TotalDays * (C.HoursPerDay - sleepHoursPerNight);

            var sleeping = SetRatios(person.MaxSleepHours, asleep);
            var waking = SetRatios(person.MaxAwakeHours, awake);

            return (sleeping, waking);
        }

        private Ratios SetRatios(double total, double part)
        {
            var r = new Ratios();

            r.Remaining = total - part;
            r.Spent = part;
            r.PercentSpent = SafeDivide(r.Spent, total);
            r.PercentRemaining = 1d - r.PercentSpent;

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