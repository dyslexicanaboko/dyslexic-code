using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.ServiceModel;
using ServerOps;
using LotteryBaseLogic;

namespace LotteryWcfService
{
    public class LotteryService : ILotteryService
    {
        private const string CACHE_FILE = "CachedValues.dat";

        public LotteryService()
        {
            //When the thread is spun up, make sure to load the cached values
            if (_lastCheckedTimeStamp == DateTime.MinValue || _lastPulledLotteryNumber == null)
                LoadCachedValues();
        }

        #region Properties
        private static string _lotteryURI = null;
        private static string LotteryURI
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_lotteryURI))
                    _lotteryURI = ConfigurationManager.AppSettings["LotteryURI"];

                return _lotteryURI;
            }
        }

        //These members are to maintain state without having to hit the site repeatedly
        private static DateTime _lastCheckedTimeStamp = DateTime.MinValue;
        private static DateTime LastCheckedTimeStamp
        {
            get { return _lastCheckedTimeStamp; }
            set { _lastCheckedTimeStamp = value; }
        }

        private static LotteryNumbers _lastPulledLotteryNumber = null;
        private static LotteryNumbers LastPulledLottoNumber
        {
            get { return _lastPulledLotteryNumber; }
            set { _lastPulledLotteryNumber = value; }
        }

        static public string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }
        #endregion Properties

        public LotteryInfo GetTodaysWinningNumber()
        {
            DateTime dtmNow = DateTime.Today;
            LotteryNumbers numbers;

            /* Only hit the website if and only if:
             *      1. If the cached string is blank, empty or null OR
             *      2. It hasn't been checked today already AND it is Thursday OR Monday */
            if (LastPulledLottoNumber == null || //The last pulled number is useless OR
                LastCheckedTimeStamp.Date != dtmNow.Date && //The last time checked wasn't today AND
                (dtmNow.DayOfWeek == DayOfWeek.Monday //The day of the week is Monday OR Thursday
                || dtmNow.DayOfWeek == DayOfWeek.Thursday))
            {
                numbers = LotteryNumberHandler.CheckLottery(LotteryURI);

                CachePulledValue(numbers);
            }
            else
                numbers = LastPulledLottoNumber;

            return new LotteryInfo(numbers);
        }

        private void CachePulledValue(LotteryNumbers numbers)
        {
            LastCheckedTimeStamp = DateTime.Now;

            LastPulledLottoNumber = numbers;

            try
            {
                ServerOps.Utils.WriteToFile(Path.Combine(AssemblyDirectory, CACHE_FILE), LastCheckedTimeStamp.ToString() + "|" + LastPulledLottoNumber.SerializeToXmlString < LotteryNumbers>(), false);
            }
            catch (Exception ex)
            {
                if (1 == 1) { }            
            }
        }

        private void LoadCachedValues()
        {
            string strRaw = string.Empty;

            try
            {
                if (ServerOps.Utils.FileToString(Path.Combine(AssemblyDirectory, CACHE_FILE), out strRaw))
                {
                    //Remove unwanted characters
                    strRaw = strRaw.Replace("\n", "").Replace("\r", "");

                    string[] arr = strRaw.Split("|".ToCharArray());

                    LastCheckedTimeStamp = Convert.ToDateTime(arr[0]);

                    LastPulledLottoNumber.DeserializeFromXmlString<LotteryNumbers>(arr[1]);
                }
            }
            catch (Exception ex)
            {
                if (1 == 1) { }
            }
        }

        public DateTime IsAlive()
        {
            return DateTime.Now;
        }

        public string Ping()
        {
            return "Pong?! @ " + DateTime.Now.ToString();
        }
    }
}
