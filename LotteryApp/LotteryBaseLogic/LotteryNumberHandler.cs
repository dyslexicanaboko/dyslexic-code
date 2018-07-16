using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;
using ServerOps;

namespace LotteryBaseLogic
{
    public static class LotteryNumberHandler
    {
        private static DateTime _lockDate = DateTime.MinValue;
        private static LotteryNumbers _currentLotto = null;

        public static LotteryNumbers CheckLottery(string lotteryURI)
        {
            LotteryNumbers ln;

            if (_currentLotto != null && _lockDate.Date == DateTime.Today.Date)
                ln = _currentLotto;
            else
            {
                ln = GetLottoFromWeb(lotteryURI);

                _currentLotto = ln;

                _lockDate = DateTime.Today.Date;
            }

            return ln;
        }

        private static LotteryNumbers GetLottoFromWeb(string lotteryURI)
        {
            string dummy = string.Empty;

            string strContent = ScreenScrapeLotto(lotteryURI);

            //Clean up the incoming html content
            strContent = Utils.HtmlDecode(strContent); //Decode all HTML encoded content
            strContent = Regex.Replace(strContent, @"\s+", " "); //Replace all multi spaces with single spaces
            strContent = strContent.Replace("\n", ""); //Remove all carriage return characters
            strContent = strContent.Replace("\r", ""); //Remove all line feed characters
            strContent = strContent.Replace("\t", ""); //Remove all tab characters

            LotteryNumbers ln = new LotteryNumbers();

            //Find each lottery number in the html
            MatchCollection mc = Regex.Matches(strContent, "<span class='balls' title='[0-9]+'>([0-9]+)</span>");
            //MatchCollection mc = Regex.Matches(strContent, "<span class=\\\"ball\\\" title=\\\"[0-9]+\\\">([0-9]+)</span>");

            //Loop through the collection for each number
            for (int i = 0; i < mc.Count; i++)
                ln.SetNumber(i, mc[i].Groups[1].ToString());

            //Find the Jackpot figure
            mc = Regex.Matches(strContent, "<p class='gameJackpot'>([$A-Za-z0-9 ]+)</p>");
            //mc = Regex.Matches(strContent, "<td class=\\\"columnLast nextJackpot\\\">([$A-Za-z0-9 ]+)</td>");

            ln.Jackpot = mc[0].Groups[1].ToString().Trim();

            //Find the status of this match
            mc = Regex.Matches(strContent, "<p class='lotto [a-zA-Z]+'>([A-Za-z0-9 ]+)</p>");
            //mc = Regex.Matches(strContent, "<div style=\\\"font-size: 90%; color: #E00034;[-;:a-zA-Z0-9 ]+\\\">([A-Za-z0-9 ]+)</div>");

            ln.Status = mc[0].Groups[1].ToString().Trim();

            ln.Sort();

            return ln;
        }

        private static string ScreenScrapeLotto(string lotteryURI)
        {
            string strPageContents = string.Empty;

            strPageContents = Utils.Download_URL_Contents(lotteryURI);
            //Utils.FileToString("F:\\Dump\\Florida Lotto.htm", out strPageContents);
            //Utils.FileToString("F:\\Dump\\FloridaLotto_v2.htm", out strPageContents);

            //Have to find a smaller segment to work with so search for these bounds
            int s = strPageContents.IndexOf("<div class='gamePageNumbers'><div class='nextJackpot'><p class='regWeight'>Next Jackpot:</p>");
            int e = strPageContents.IndexOf("<table class='style1 games'>", s);

            //Original search pattern
            //int s = strPageContents.IndexOf("<table class=\"lottoResults\"");
            //int e = strPageContents.IndexOf("</table>", s);

            string strLotteryNumbers = strPageContents.Substring(s, e - s);

            return strLotteryNumbers;
        }
    }
}
