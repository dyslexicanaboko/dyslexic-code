using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RouletteSimulator
{
    public class Result
    {
        private static List<int> redNumbers = new List<int>() { 1, 3, 5, 7, 9, 12, 14, 16, 18, 19, 21, 23, 25, 27, 30, 32, 34, 36 };

        private static List<int> blackNumbers = new List<int>() { 2, 4, 6, 8, 10, 11, 13, 15, 17, 20, 22, 24, 26, 28, 29, 31, 33, 35 };

        public static int Bet = 10;
        public static int WinFactor = 1;
        public static int Balance = 100;
        public static int Red = 0;
        public static int Black = 0;
        public static int Green = 0;
        public static int Wins = 0;
        public static int Losses = 0;

        public Result(int number)
        {
            Number = number;

            NumberString = GetNumberString(number);
            Color = GetDrawColor(number);

            if (Number == 0 || Number == 37)
            {
                Profile = string.Empty;
                Direction = string.Empty;
            }
            else
            {
                Profile = (number % 2 == 0) ? "Evn" : "Odd";
                Direction = (number > 18) ? "Dn" : "Up";
            }
        }

        public int Number { get; private set; }
        public string NumberString { get; private set; }
        public string Color { get; private set; }
        public string Direction { get; private set; }
        public string Profile { get; private set; }

        public static string GetNumberString(int draw)
        {
            return (draw == 37) ? "D0" : draw.ToString("00");
        }

        public static string GetDrawColor(int draw)
        {
            if (redNumbers.Contains(draw))
            {
                Red++;

                return "Red";
            }

            if (blackNumbers.Contains(draw))
            {
                Black++;

                return "Blk";
            }

            Green++;

            return "Grn";
        }

        public void CompareNumberGuess(int guess)
        {
            Console.WriteLine("Number: {0}, Result: {1}, {2}:{3} = {4}, Bal = {5}", NumberString, CalculateBalance(Number == guess), Wins, Losses, (Wins - Losses), Balance);
        }

        public void Compare(Bet guess)
        {
            bool winColor;
            bool winDirection;
            string resultColor;
            string resultDirection;
            string outCome;

            winColor = Color.ToLower() == guess.Color.ToLower();

            resultColor = CalculateBalance(winColor);

            winDirection = Direction.ToLower() == guess.Direction.ToLower();

            resultDirection = CalculateBalance(winDirection);

            if (winColor && winDirection)
                outCome = "GD";
            else if (winColor != winDirection)
                outCome = "OK";
            else
                outCome = "BD";

            Console.WriteLine("{0}/{1} = {2}, {3}:{4} = {5}, Bal = {6}", resultColor, resultDirection, outCome, Wins, Losses, Wins - Losses, Balance);
        }

        public static string Statistics()
        {
            return string.Format("Black: {0} Red: {1} Green: {2}", Black, Red, Green);
        }

        private string CalculateBalance(bool win)
        {
            if (win)
            {
                Wins++;

                Balance += Bet * WinFactor;

                return "W";
            }
            else
            {
                Losses++;

                Balance -= Bet;

                return "L";
            }
        }

        public override string ToString()
        {
            return NumberString + " " + Color + " " + Direction + " " + Profile;
        }
    }
}
