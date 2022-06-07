namespace DepressingFigures.Lib
{
    public class LifeSpanService
    {
        public const double DaysInYear = 365;

        public Estimations Calculate(Profile profile)
        {
            var result = new Estimations();

            //Load gender specific values
            Person person = profile.BiologicalGender == Gender.Male ? new Male() : new Female();

            person.SetAge(profile.BirthDate);

            //var remainder = person.Expiration.Year - (person.Current.TotalDays / DaysInYear);

            return result;
        }

        //private Ratios SetRatios(double total, double part)
        //{
            
        //}
    }
}