namespace DepressingFigures.Lib
{
    public class Estimations
    {
        public DateTime Expiration { get; set; }

        public Ratios Years { get; set; } = new Ratios();
        
        public Ratios Seconds { get; set; } = new Ratios();

        public Ratios Sleep { get; set; } = new Ratios();

        public Ratios Awake { get; set; } = new Ratios();
        
        public Ratios Working { get; set; } = new Ratios();
    }
}
