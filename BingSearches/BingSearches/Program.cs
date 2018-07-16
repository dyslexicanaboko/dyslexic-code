using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace BingSearches
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                MineRewards(args);

#if DEBUG
                Console.WriteLine(Environment.NewLine + Environment.NewLine + "Press any key to continue...");
                Console.Read();
#endif

                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                Utils.LogError(ex);

                Environment.Exit(1);
            }
        }

        static void MineRewards(string[] args)
        {

            int intSearches = 92;

            if (args.Length == 1)
                intSearches = Convert.ToInt32(args[0]);

            BingRewards obj = new BingRewards(intSearches);

            Utils.LogMessage("Single Credit Rewards" + Environment.NewLine);

            //First search all of those single credit rewards searches and what not
            obj.EarnAllSingleCreditRewards();

            Utils.LogMessage("Bing Search Rewards" + Environment.NewLine);

            //Second perform bing searches to get in that daily search quota
            obj.PerformBingSearches();
        }
    }
}
