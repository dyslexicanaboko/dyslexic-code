using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RouletteSimulator
{
    public class InsideBet : Bet
    {
        public InsideBet()
        {
            MinimumBet = 5;
        }

        public int Number { get; set; }
        public string NumberString { get { return Result.} }
    }
}
