using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LotteryBaseLogic;

namespace LotteryWcfHost
{
    public class LNPair
    {
        //public LNPair()
        //{ 
        
        //}

        public LNPair(LotteryNumbers winner, LotteryNumbers player)
        {
            Lotto1 = winner.Number1;
            Lotto2 = winner.Number2;
            Lotto3 = winner.Number3;
            Lotto4 = winner.Number4;
            Lotto5 = winner.Number5;
            Lotto6 = winner.Number6;

            Player1 = player.Number1;
            Player2 = player.Number2;
            Player3 = player.Number3;
            Player4 = player.Number4;
            Player5 = player.Number5;
            Player6 = player.Number6;

            SetPairStats(winner, player);
        }

        public int Lotto1 { get; private set; }
        public int Lotto2 { get; private set; }
        public int Lotto3 { get; private set; }
        public int Lotto4 { get; private set; }
        public int Lotto5 { get; private set; }
        public int Lotto6 { get; private set; }

        public string LColor1 { get; private set; }
        public string LColor2 { get; private set; }
        public string LColor3 { get; private set; }
        public string LColor4 { get; private set; }
        public string LColor5 { get; private set; }
        public string LColor6 { get; private set; }

        public int Player1 { get; private set; }
        public int Player2 { get; private set; }
        public int Player3 { get; private set; }
        public int Player4 { get; private set; }
        public int Player5 { get; private set; }
        public int Player6 { get; private set; }

        public string PColor1 { get; private set; }
        public string PColor2 { get; private set; }
        public string PColor3 { get; private set; }
        public string PColor4 { get; private set; }
        public string PColor5 { get; private set; }
        public string PColor6 { get; private set; }

        public int MatchCount { get; private set; }
        public string Status { get; private set; }

        private void SetPairStats(LotteryNumbers winner, LotteryNumbers player)
        {
            Dictionary<int, int> matches = LotteryNumbers.CrossExamineSets(winner, player);

            LinkedList<string> lst = LotteryUtil.GetBoxColors();

            LinkedList<string>.Enumerator iter = lst.GetEnumerator();
            iter.MoveNext();

            foreach (KeyValuePair<int, int> kvp in matches)
            {
                SetLottoColor(kvp.Key, kvp.Value, iter.Current);

                iter.MoveNext();
            }

            MatchCount = matches.Count;

            Status = LotteryUtil.GetPlayerStatus(MatchCount);
        }

        private void SetLottoColor(int lotteryPosition, int playerPosition, string colorName)
        {
            switch (lotteryPosition)
            {
                case 0:
                    LColor1 = colorName;
                    break;

                case 1:
                    LColor2 = colorName;
                    break;

                case 2:
                    LColor3 = colorName;
                    break;

                case 3:
                    LColor4 = colorName;
                    break;

                case 4:
                    LColor5 = colorName;
                    break;

                case 5:
                    LColor6 = colorName;
                    break;
            }

            switch (playerPosition)
            {
                case 0:
                    PColor1 = colorName;
                    break;

                case 1:
                    PColor2 = colorName;
                    break;

                case 2:
                    PColor3 = colorName;
                    break;

                case 3:
                    PColor4 = colorName;
                    break;

                case 4:
                    PColor5 = colorName;
                    break;

                case 5:
                    PColor6 = colorName;
                    break;
            }
        }
    }
}