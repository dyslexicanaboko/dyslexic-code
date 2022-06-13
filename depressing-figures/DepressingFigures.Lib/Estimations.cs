namespace DepressingFigures.Lib
{
    public class Estimations
    {
        public Person Person { get; set; }

        public DateTime Expiration { get; set; }

        public Ratios Years { get; set; } = new Ratios();
        
        public Ratios Seconds { get; set; } = new Ratios();

        public Ratios SleepHours { get; set; } = new Ratios();

        public Ratios AwakeHours { get; set; } = new Ratios();
        
        public Ratios WorkingHours { get; set; } = new Ratios();
    }
}
