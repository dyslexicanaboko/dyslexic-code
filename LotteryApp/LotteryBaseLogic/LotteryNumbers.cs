using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServerOps;

namespace LotteryBaseLogic
{
    public class LotteryNumbers
    {
        public LotteryNumbers()
        {
            for (int i = 0; i < 6; i++)
                m_numbers.Add(0);
        }

        public LotteryNumbers(string lotteryString)
        {
            ParseString(lotteryString);
        }

        #region Properties
        private List<int> m_numbers = new List<int>();
        protected List<int> Numbers
        {
            get { return m_numbers; }
        }

        public int Number1 
        { 
            get { return m_numbers[0]; }
            set { m_numbers[0] = Math.Abs(value); }
        }

        public int Number2
        {
            get { return m_numbers[1]; }
            set { m_numbers[1] = Math.Abs(value); }
        }

        public int Number3
        {
            get { return m_numbers[2]; }
            set { m_numbers[2] = Math.Abs(value); }
        }

        public int Number4
        {
            get { return m_numbers[3]; }
            set { m_numbers[3] = Math.Abs(value); }
        }

        public int Number5
        {
            get { return m_numbers[4]; }
            set { m_numbers[4] = Math.Abs(value); }
        }

        public int Number6
        {
            get { return m_numbers[5]; }
            set { m_numbers[5] = Math.Abs(value); }
        }

        public string Jackpot { get; set; }
        public string Status { get; set; }
        #endregion

        public void SetNumber(int index, string numberValue)
        {
            m_numbers[index] = numberValue.ConvertToInt32();
        }

        private void ParseString(string lotteryNumbers)
        {
            string[] arr = lotteryNumbers.Split("-".ToCharArray());

            if (m_numbers.Count > 0)
                m_numbers.Clear();

            foreach(string s in arr)
                m_numbers.Add(s.ConvertToInt32());

            m_numbers.Sort();
        }

        public static LotteryNumbers ParseLotteryNumberString(string lotteryNumbers)
        {
            return new LotteryNumbers(lotteryNumbers);
        }

        public static Dictionary<int, int> CrossExamineSets(LotteryNumbers set1, LotteryNumbers set2)
        {
            Dictionary<int, int> dict = new Dictionary<int,int>();

            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    if (set1.Numbers[i] == set2.Numbers[j])
                        dict.Add(i, j);
                }
            }

            return dict;
        }

        public bool HasDuplicateValues()
        {
            //Compare the count of the list versus a distinct list (not including zero)
            return m_numbers.Count != m_numbers.Where(n => n != 0).Distinct().Count();
        }

        public int Sum()
        {
            return m_numbers.Sum();
        }

        public void Sort()
        {
            m_numbers.Sort();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            foreach (int n in m_numbers)
                sb.Append(n.ToString("00")).Append("-");

            sb.Remove(sb.Length - 1, 1); //Remove trailing -

            return sb.ToString();
        }
    }
}
