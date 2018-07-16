using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RouletteSimulator
{
    public enum BetColor 
    { 
        Neither = 0,
        Red = 1,
        Black = 2
    }

    public enum BetDirection
    { 
        Neither = 0,
        Up = 1,
        Down = 2
    }

    public enum BetProfile
    { 
        Neither = 0,
        Even = 1,
        Odd = 2
    }

    public abstract class Bet
    {
        public static int MinimumBet { get; set; }

        public Bet()
        { 
        
        }

        private string _color;
        public string Color 
        {
            get { return _color; }
            set { _color = value.ToLower().StartsWith("b") ? "Blk" : "Red"; } 
        }
        
        private string _direction;
        public string Direction 
        { 
            get { return _direction; }
            set { _direction = value.ToLower().StartsWith("u") ? "Up" : "Dn"; }
        }

        public override string ToString()
        {
            return Color + ", " + Direction;
        }
    }
}
