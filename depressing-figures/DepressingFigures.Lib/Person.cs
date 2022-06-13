namespace DepressingFigures.Lib
{
    public abstract class Person
    {
        //CDC data for life expectancy in the united states
        public abstract int MaxAgeYears { get; protected set; }
        
        public DateTime BirthDate { get; private set; }

        public DateTime Current { get; private set; }

        public DateTime Expiration { get; private set; }

        public TimeSpan Spent { get; private set; }
        
        public TimeSpan Remainder { get; private set; }

        public void SetAge(DateTime birthDate)
        {
            var dtm = DateTime.Now.Date;

            BirthDate = birthDate;

            Current = new DateTime(dtm.Year, birthDate.Month, birthDate.Day);

            Expiration = birthDate.AddYears(MaxAgeYears);

            Spent = dtm - birthDate; //From birth to today
            
            Remainder = Expiration - Current; //From today to expiration
        }

        public void OverrideMaxAgeYears(int years) => MaxAgeYears = years;
    }
}
