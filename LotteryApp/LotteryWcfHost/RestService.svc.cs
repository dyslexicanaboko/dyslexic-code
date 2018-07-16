using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LotteryWcfService;

namespace LotteryWcfHost
{
    public class RestService : ILotteryService
    {
        private static LotteryService _wcfService;

        public RestService()
        {
            if (_wcfService == null)
                _wcfService = new LotteryService();
        }

        public LotteryInfo GetTodaysWinningNumber()
        {
            return _wcfService.GetTodaysWinningNumber();
        }

        public DateTime IsAlive()
        {
            return _wcfService.IsAlive();
        }

        public string Ping()
        {
            return _wcfService.Ping();
        }
    }
}
