using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RouletteSimulator
{
    public class OutsideBet : Bet
    {
        public OutsideBet()
        {
            MinimumBet = 10;
        }

        public BetColor Color { get; set; }
        public BetDirection Direction { get; set; }
        public BetProfile Profile { get; set; }

        public void ParseColor(string value)
        {
            Color = value.ToLower().StartsWith("b") ? BetColor.Black : BetColor.Red;
        }

        public void ParseDirection(string value)
        {
            Direction = value.ToLower().StartsWith("u") ? BetDirection.Up : BetDirection.Down;
        }

        public void ParseProfile(string value)
        {
            Profile = value.ToLower().StartsWith("e") ? BetProfile.Even : BetProfile.Odd;
        }

        public override string ToString()
        {
            List<string> lst = new List<string>();

            if(Color != BetColor.Neither)
                lst.Add(Color.ToString());

            if(Direction != BetDirection.Neither)
                lst.Add(Direction.ToString());

            if(Profile != BetProfile.Neither)
                lst.Add(Profile.ToString());

            return string.Join(", ", lst);
        }
    }
}
