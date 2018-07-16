using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RouletteSimulator
{
    public class Simulator
    {
        private static Dictionary<int, int> numberIndex = new Dictionary<int, int>();

        private static List<Result> results = new List<Result>();

        public static void Run(int rounds = 10, bool stepThrough = false, bool guesses = false)
        {
            int draw = 0;

            string[] arrGuess;

            Random rand = new Random();

            Result rr = null;
            Bet g = null;

            if(guesses)
                Console.WriteLine("Place your bets using color and direction separated by a comma - Ex: Blk, Up");

            for (int i = 0; i < rounds; i++)
            {
                if (guesses)
                {
                    g = new Bet();

                    arrGuess = Console.ReadLine().Trim().Split(',');

                    g.Color = arrGuess[0].Trim();
                    g.Direction = arrGuess[1].Trim();
                }

                draw = rand.Next(37);

                CatalogNumber(draw);

                rr = new Result(draw);

                results.Add(rr);

                Console.WriteLine(rr);

                if (guesses)
                    rr.Compare(g);

                if (stepThrough && !guesses)
                    Console.ReadLine();

                Console.WriteLine();
            }

            GetStats();
        }

        public static void StraightUpHumanBet(int balance, int maxBalance = 0)
        {
            if (balance < Result.Bet)
                throw new Exception("Starting balance cannot be less than the outside bet: $" + Result.Bet);

            if (maxBalance < balance)
                maxBalance = balance * 2;

            bool loop = true;

            int draw = 0, round = 0;

            string[] strBets;

            Random rand = new Random();

            Result.Bet = 1;
            Result.WinFactor = 35;

            Result rr = null;
            Bet g = null;

            Console.WriteLine("Place your bets by entering a number between 0 and 36 or 37 for double zero");

            while (loop)
            {
                Console.WriteLine("Round: {0:00}", ++round);

                strBets = Console.ReadLine().Trim().Split(',');

                Result.Bet = Convert.ToInt32(strBets[1].Trim());

                draw = rand.Next(37);

                CatalogNumber(draw);

                rr = new Result(draw);

                results.Add(rr);

                Console.WriteLine(rr);

                rr.CompareNumberGuess(Convert.ToInt32(strBets[0].Trim()));

                Console.WriteLine();

                if (StopSimulation(maxBalance))
                    loop = false;
            }

            GetStats();
        }

        public static void StraightUpConstantBet(int balance, int luckyNumber, int bet = 1, int maxBalance = 0)
        {
            if (balance < Result.Bet)
                throw new Exception("Starting balance cannot be less than the outside bet: $" + Result.Bet);

            if (maxBalance < balance)
                maxBalance = balance * 2;

            bool loop = true;

            int draw = 0, round = 0;

            string[] strBets;

            Random rand = new Random();

            Result.Bet = bet;
            Result.WinFactor = 35;

            Result rr = null;
            Bet g = null;

            Console.WriteLine("Place your bets by entering a number between 0 and 36 or 37 for double zero");

            while (loop)
            {
                Console.WriteLine("Round: {0:00}", ++round);

                draw = rand.Next(37);

                CatalogNumber(draw);

                rr = new Result(draw);

                results.Add(rr);

                Console.WriteLine(rr);

                rr.CompareNumberGuess(luckyNumber);

                Console.WriteLine();

                if (StopSimulation(maxBalance))
                    loop = false;
            }

            GetStats();
        }

        public static void OutsideHumanBet(int balance, int maxBalance = 0)
        {
            if (balance < Result.Bet)
                throw new Exception("Starting balance cannot be less than the outside bet: $" + Result.Bet);

            if (maxBalance < balance)
                maxBalance = balance * 2;

            bool loop = true;

            int draw = 0, round = 0;

            string[] arrGuess;

            Random rand = new Random();

            Result rr = null;
            Bet g = null;

            Console.WriteLine("Place your bets using color and direction separated by a comma - Ex:B, U or Blk, Up or Black, Up");

            while (loop)
            {
                Console.WriteLine("Round: {0:00}", ++round);

                arrGuess = Console.ReadLine().Trim().Split(',');

                g = new Bet(arrGuess[0].Trim(), arrGuess[1].Trim());

                draw = rand.Next(37);

                CatalogNumber(draw);

                rr = new Result(draw);

                results.Add(rr);

                Console.WriteLine(rr);

                rr.Compare(g);

                Console.WriteLine();

                if (StopSimulation(maxBalance))
                    loop = false;
            }

            GetStats();
        }

        public static void OutsideConstantBet(int balance, Bet constantGuess)
        {
            bool loop = true;

            int draw = 0, rounds = 0;

            Random rand = new Random();

            Result.Balance = balance;
            Result rr = null;

            Console.WriteLine("Same Bet Every Round of: " + constantGuess + " @ $" + balance + "\n");

            while(loop)
            {
                rounds++;

                Console.WriteLine("Round {0:00}", rounds);

                draw = rand.Next(37);

                CatalogNumber(draw);

                rr = new Result(draw);

                results.Add(rr);

                Console.WriteLine(rr);

                rr.Compare(constantGuess);

                Console.WriteLine();

                if (StopSimulation(balance * 2))
                    loop = false;
            }

            GetStats();
        }

        private static bool StopSimulation(int maxBalance)
        {
            return (Result.Balance == 0 || Result.Balance >= maxBalance);
        }

        private static void CatalogNumber(int draw)
        {
            if (numberIndex.ContainsKey(draw))
                numberIndex[draw]++;
            else
                numberIndex.Add(draw, 1);
        }

        private static void GetStats()
        {
            Console.WriteLine(Result.Statistics() + "\n");

            Dictionary<int, int> byKey = numberIndex.OrderBy(x => x.Key).ToDictionary(x => x.Key, y => y.Value);

            Console.WriteLine("Ordered By Number");

            foreach (KeyValuePair<int, int> kvp in byKey)
                Console.WriteLine(kvp);

            Dictionary<int, int> byValue = numberIndex.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, y => y.Value);

            Console.WriteLine("\nOrdered Descending By Number of Appearances");

            foreach (KeyValuePair<int, int> kvp in byValue)
                Console.WriteLine(kvp);
        }
    }
}
