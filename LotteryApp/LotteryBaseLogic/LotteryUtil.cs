using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotteryBaseLogic
{
    public static class LotteryUtil
    {
        public static readonly List<string> BoxColors = new List<string>()
        {
            "LightGoldenrodYellow",
            "LightCoral",
            "LightCyan",
            "LightGray",
            "LightGreen",
            "LightPink"
        };

        public static LinkedList<string> GetBoxColors()
        {
            return new LinkedList<string>(BoxColors);
        }

        public static string GetPlayerStatus(int numberOfMatches)
        {
            string status = string.Empty;

            switch (numberOfMatches)
            {
                case 6:
                    status = "Congrats you are now wealthy!!!!";
                    break;
                case 5:
                    status = "Not what I was looking for, but hey I'll take it!";
                    break;
                case 4:
                    status = "At least it's something...";
                    break;
                case 3:
                    status = "Better than a free ticket I guess...";
                    break;
                case 2:
                    status = "Oh goody a free drawing... only if you paid for XTRA...";
                    break;
                default:
                    status = "Better luck next time.";
                    break;
            }

            return status;
        }
    }
}
