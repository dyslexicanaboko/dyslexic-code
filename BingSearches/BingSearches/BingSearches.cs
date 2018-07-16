using System;
using System.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using NLog;

namespace BingSearches
{
    public class BingRewards
    {
        const int DICTIONARY_TERMS_PER_HIT = 50;
        const string URI_REWARDS = "http://www.bing.com/rewards/dashboard"; //Rewards page URL
        const string URI_BING = "http://www.bing.com/"; //Bing base URL
        const string URI_BING_SEARCH = "http://www.bing.com/search?q="; //Bing search URL
        const string URI_DICT = "http://www.wordgenerator.net/application/p.php?id=dictionary_words&type=50_definition&spaceflag=false"; //Word generator URL

        List<string> SearchTerms = new List<string>();
        Dictionary<string, string> Cookies { get; set; } //Cookies unique to the Bing user
        WebClient DictionaryClient = new WebClient(); //Random word website reference
        Random R = new Random(); //Random generator reference
        string CookieDataFilePath { get; set; }
        string FilePath { get; set; }
        string OutputPath { get; set; }
        bool TurnSleepOff { get; set; }

        public int Searches { get; private set; }
        public int DictionaryHits { get; private set; }

        public BingRewards(int searches = 100)
        {
            Searches = searches;

            DictionaryHits = GetNumberOfDictionaryHits(searches);

            FilePath = GetCurrentDirectory();

            OutputPath = GetOutputPath(FilePath);

            CookieDataFilePath = GetCookieDataFilePath(FilePath);

            TurnSleepOff = GetTurnSleepOff();

            //Print all of this data for logging reasons
            string strPrint =
                "File Path: " + FilePath + Environment.NewLine +
                "Output Path: " + OutputPath + Environment.NewLine +
                "Cookie Data File Path: " + CookieDataFilePath + Environment.NewLine +
                "Sleep: " + !TurnSleepOff + Environment.NewLine +
                "Dictionary Hits: " + DictionaryHits;

            Utils.LogMessage(strPrint);

            Cookies = GetRequestCookieDataFromConfig();

            SearchTerms = GetSearchTerms();
        }

        private int GetNumberOfDictionaryHits(int searches)
        {
            return Convert.ToInt32(Math.Ceiling(((decimal)searches) / ((decimal)DICTIONARY_TERMS_PER_HIT)));
        }

        /// <summary>
        /// Get the file path of the cookie data. If none is found then the default is returned.
        /// </summary>
        /// <returns></returns>
        private bool GetTurnSleepOff()
        {
            string strValue = ConfigurationManager.AppSettings["TurnSleepOff"];

            if (string.IsNullOrWhiteSpace(strValue))
                strValue = "false";

            return Convert.ToBoolean(strValue);
        }

        private string GetCookieDataFilePath(string basePath)
        {
            string strValue = ConfigurationManager.AppSettings["CookieDataFilePath"];

            if (string.IsNullOrWhiteSpace(strValue))
                strValue = "CookieData.txt";

            return Path.Combine(basePath, strValue);
        }

        /// <summary>
        /// Get the output path for any files that need to be outputted during run time
        /// </summary>
        /// <returns></returns>
        private string GetOutputPath(string basePath)
        {
            string strValue = ConfigurationManager.AppSettings["OuputFilePath"];

            if (string.IsNullOrWhiteSpace(strValue))
                strValue = "Output";

            return Path.Combine(basePath, strValue);
        }

        /// <summary>
        /// Get the current directory
        /// </summary>
        /// <returns></returns>
        private string GetCurrentDirectory()
        {
            return Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        }

        /// <summary>
        /// Get all of the search terms to use up front all in one shot.
        /// The list will be used later during the bing searches.
        /// </summary>
        /// <returns>List of 100 dictionary words or list of 100 random numbers as string</returns>
        /// <remarks>
        /// If there are any failures then 100 random numbers are returned instead.
        /// These dictionary terms will be used to search bing
        /// </remarks>
        private List<string> GetSearchTerms()
        {
            List<string> lst = new List<string>();

            try
            {
                int index;
                string strPage;
                string[] arr;

                Utils.LogMessage("Getting Search Terms...");

                for (int i = 0; i < DictionaryHits; i++)
                {
                    strPage = DictionaryClient.DownloadString(URI_DICT);

                    if (string.IsNullOrEmpty(strPage))
                        throw new Exception("Dictionary page content was empty or null");

                    arr = strPage.Split(new string[] { "</p>," }, StringSplitOptions.None);

                    foreach (string token in arr)
                    {
                        index = token.IndexOf("<br");

                        if (string.IsNullOrEmpty(token) || index == -1)
                            continue;

                        lst.Add(token.Substring(0, index));

                        //Console.WriteLine(SearchTerms[SearchTerms.Count - 1]);
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.LogError(ex);

                lst = GenerateNumbers();
            }
            finally
            {
                Utils.LogMessage(lst.Count + " Search Terms Loaded");
            }

            return lst;
        }

        /// <summary>
        /// Generate a list of numbers as string to be used as search terms
        /// </summary>
        /// <param name="amount">The number of numbers to generate</param>
        /// <returns>List of numbers as strings</returns>
        private List<string> GenerateNumbers(int amount = 100)
        {
            List<string> lst = new List<string>();

            for (int i = 0; i < 100; i++)
                lst.Add(R.Next().ToString());

            return lst;
        }

        /// <summary>
        /// Perform a variable number of searches on Bing to earn x points per y searches
        /// </summary>
        /// <param name="searches">The number of searches to perform</param>
        public void PerformBingSearches()
        {
            string strQuery;
            string strURI;
            string strResponse;

            for (int i = 0; i < Searches; i++)
            {
                try
                {
                    strQuery = SearchTerms[i];

                    strURI = URI_BING_SEARCH + strQuery;

                    strResponse = PerformHttpGet(strURI);

                    Utils.LogMessage("[" + (i + 1) + "] Query: \"" + strQuery + "\", Response Length: " + strResponse.Length);
                }
                catch(Exception ex)
                {
                    Utils.LogError(ex, "Failure on: [" + (i + 1) + "] :: " + ex.ToString());
                }

                SleepForRandomDuration();
            }
        }

        /// <summary>
        /// Sleep the current thread for a random duration between 1 and 30 inclusive.
        /// </summary>
        private void SleepForRandomDuration()
        {
            if (TurnSleepOff)
                return;

            int intSleep = R.Next(29) + 1;

            Utils.LogMessage(string.Format(Environment.NewLine + "Sleep for {0} seconds...", intSleep));

            System.Threading.Thread.Sleep(intSleep * 1000);
        }

        /// <summary>
        /// Go to the Bing Rewards dashboard and navigate to all of the available single credit rewards links
        /// </summary>
        public void EarnAllSingleCreditRewards()
        {
            string strURI;
            string strResponse;
            string strRewardsPage = PerformHttpGet(URI_REWARDS);

            #region Notes
            //This is the test file for debugging
            //string strRewardsPage = ReadFile("RewardsPageTest.txt");

            /* This is the RegEx pattern with no escape sequences
             * \<a href="(/rewardsapp/redirect\?url=%2f.*?\&amp;ml=MissionTaskLayout&amp;nid=&amp;rh=)"
             * 
             * This is the RegEx pattern broken up into parts:
             * Part 1: \<a href="
             *      The first slash means Lookahead
             * Part 2: (/rewardsapp/redirect\?url=%2f
             *      The open round bracket means group start
             * Part 3: .*?\
             *      This means match anything
             *      The last slash means Lookbefore
             * Part 4: &amp;ml=MissionTaskLayout&amp;nid=&amp;rh=)"
             *      This is the rest of the string literal
             *      The ending round bracket is the group close followed by a trailing soft quote
             */
            #endregion

            string strPattern = "\\<a href=\"(/rewardsapp/redirect\\?url=.*?\\&amp;ml=MissionTaskLayout&amp;nid=&amp;rh=)\"";

            Regex r = new Regex(strPattern, RegexOptions.Singleline);

            MatchCollection mc = r.Matches(strRewardsPage);

            int i = 0;

            foreach (Match m in mc)
            {
                try
                {
                    i++;

                    //Join the base URI and the Path that is extracted from the matched group
                    strURI = new Uri(new Uri(URI_BING), System.Web.HttpUtility.HtmlDecode(m.Groups[1].Value)).AbsoluteUri;

                    //Perform a full Http Get in order to execute getting that single credit (going to the link)
                    strResponse = PerformHttpGet(strURI);

                    Utils.LogMessage("[" + i + "] URI: " + strURI + ", Length: " + strResponse.Length + Environment.NewLine);
                }
                catch (Exception ex)
                {
                    Utils.LogError(ex, "Failure on: [" + i + "] :: " + ex.ToString());
                }

                SleepForRandomDuration();
            }

            Utils.LogMessage(string.Format("Today's Single Rewards: {0}", i));

            if (i == 0)
                WriteFile("RewardsPage_" + DateTime.Today.ToString("yyyy.MM.dd") + ".htm", strRewardsPage);
        }

        /// <summary>
        /// Simply read a file at the specified path
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns>File contents as string</returns>
        private string ReadFile(string filePath)
        {
            using (StreamReader sr = new StreamReader(filePath))
            {
                return sr.ReadToEnd();
            }
        }

        private void WriteFile(string fileName, string content)
        {
            using (StreamWriter sw = new StreamWriter(Path.Combine(OutputPath, fileName)))
            {
                sw.Write(content);
            }
        }

        /// <summary>
        /// Get the cookie data from the CookieData.txt file as a Dictionary collection
        /// </summary>
        /// <returns>Key = Cookie, Value = cookie value</returns>
        private Dictionary<string, string> GetRequestCookieDataFromConfig()
        {
            string strData = ReadFile(CookieDataFilePath);

            if (string.IsNullOrWhiteSpace(strData))
                throw new Exception("You must provide your Bing Response Cookie Header data. Get it by using Fiddler and paste it into the \"CookieData.txt\" file as is.");

            Dictionary<string, string> dict = new Dictionary<string, string>();

            int index = 0;
            string key;
            string value;

            //First get all of the key value pairs. They are delimited by "; " (semi-colon and space)
            string[] arrKVP = strData.Split(new string[] { "; " }, StringSplitOptions.None);

            foreach (string kvp in arrKVP)
            {
                //Find only the first equal sign, everything else is the value of the cookie
                index = kvp.IndexOf("=");

                key = kvp.Substring(0, index); //Cookie name

                //Add 1 to the index to skip the first "="
                value = kvp.Substring(index + 1, (kvp.Length - key.Length - 1)); //Value of the cookie

                //Store for later
                dict.Add(key, value);
            }

            Utils.LogMessage(string.Format("Cookies Loaded: {0}{1}", dict.Count, Environment.NewLine));

            return dict;
        }

        /// <summary>
        /// Create a Http Web Request using doctored headers and cookie values.
        /// The cookie values are pulled from the Cookie Dictionary.
        /// </summary>
        /// <param name="uri">Target URI</param>
        /// <returns></returns>
        private HttpWebRequest GetWebRequest(string uri)
        {
            Uri targetUri = new Uri(uri);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(targetUri);

            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/30.0.1599.101 Safari/537.36";

            request.Headers["Accept-Encoding"] = "gzip,deflate,sdch";
            request.Headers["Accept-Language"] = "en-US,en;q=0.8";

            request.CookieContainer = new CookieContainer();
            
            foreach(KeyValuePair<string, string> kvp in Cookies)
                request.CookieContainer.Add(new Cookie(kvp.Key, kvp.Value, targetUri.AbsolutePath, targetUri.Host));

            return request;
        }

        /// <summary>
        /// Perform an Http Web Request for the provided URI and get the response
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public string PerformHttpGet(string uri)
        {
            return PerformHttpGet(GetWebRequest(uri));
        }

        /// <summary>
        /// Using an existing Http Web Request get the response as a string
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public string PerformHttpGet(HttpWebRequest request)
        {
            WebResponse webRequestResponse = request.GetResponse();

            string strResponse;

            using (StreamReader sr = new StreamReader(webRequestResponse.GetResponseStream()))
            {
                strResponse = sr.ReadToEnd();
            }

            return strResponse;
        }
    }
}

//private HttpWebRequest GetWebRequest(string uri)
//{
//    Uri targetUri = new Uri(uri);

//    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(targetUri);

//    request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
//    request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/30.0.1599.101 Safari/537.36";

//    request.Headers["Accept-Encoding"] = "gzip,deflate,sdch";
//    request.Headers["Accept-Language"] = "en-US,en;q=0.8";

//    request.CookieContainer = new CookieContainer();
//    request.CookieContainer.Add(new Cookie("SRCHUID", "V=2&GUID=60E40A245AF2404BB82FF26F702816D8", targetUri.AbsolutePath, targetUri.Host));
//    request.CookieContainer.Add(new Cookie("SRCHUSR", "AUTOREDIR=0&GEOVAR=&DOB=20130427", targetUri.AbsolutePath, targetUri.Host));
//    request.CookieContainer.Add(new Cookie("MUIDB", "1A5282975BEB6FCB38A381B25FEB6F8F", targetUri.AbsolutePath, targetUri.Host));
//    request.CookieContainer.Add(new Cookie("PPLState", "1", targetUri.AbsolutePath, targetUri.Host));
//    request.CookieContainer.Add(new Cookie("_RwBf", "A=672534CC4476B6522189C218FFFFFFFF&s=10&o=0", targetUri.AbsolutePath, targetUri.Host));
//    request.CookieContainer.Add(new Cookie("RMS", "F=OAAABgAAAAR", targetUri.AbsolutePath, targetUri.Host));
//    request.CookieContainer.Add(new Cookie("_SSI", "CW=1903&CH=995", targetUri.AbsolutePath, targetUri.Host));
//    request.CookieContainer.Add(new Cookie("_UR", "OMW=1", targetUri.AbsolutePath, targetUri.Host));
//    request.CookieContainer.Add(new Cookie("MUID", "1A5282975BEB6FCB38A381B25FEB6F8F", targetUri.AbsolutePath, targetUri.Host));
//    request.CookieContainer.Add(new Cookie("_FS", "NU=1&mkt=en-US", targetUri.AbsolutePath, targetUri.Host));
//    request.CookieContainer.Add(new Cookie("SRCHD", "MS=3087584&D=2798640&AF=NOFORM&SM=1", targetUri.AbsolutePath, targetUri.Host));
//    request.CookieContainer.Add(new Cookie("_U", "1OO1gQTJL0ZyRq23PFKwPdYIGvqZ7R03qUU5rg1wox-eJ1eXGIWMJakI7NT64tJp2poe-gr3sqT4gX9bmNkRhXLwwBtQFGcnN713wp2mQarU", targetUri.AbsolutePath, targetUri.Host));
//    request.CookieContainer.Add(new Cookie("ANON", "A=672534CC4476B6522189C218FFFFFFFF&E=ec9&W=1", targetUri.AbsolutePath, targetUri.Host));
//    request.CookieContainer.Add(new Cookie("KievRPSAuth", "FAAqARRaTOJILtFsMkpLVWSG6AN6C%2fsvRwNmAAAEgAAACO31TY1hpwv36ABMiKlBdO1KCbcjoKPlFLDiDSQjU+6%2fKQf7l3Xa3X4aV4vnnimPvqYB1Sus6eYcpAPYKf7oG2JS2bMD3%2f+SYRscdMdyPmW7MmaxF6qr2ZhleJokHEBcnsFpXoB52Ltfqtvkl7s6S0Jc%2fLDgCw1erJBb+Ixhx3sLa2bvthOJzrgIs8IXNrIYHu9zZs+MW6+lnXCW2GjkJIFOtqqe6%2fJxe4ZSXl1tZNEZZQnS5Dv4zEHtTit8YoDL5pE6W+rV6UMgc6n9p3Hjoa40INOdzqdDj8qkey6LuYDDqMxPci96sT8L%2fn1BpS6w5iFzFADD7pOyP9U8zF7rHOfX0X8gmKa3Iw%3d%3d", targetUri.AbsolutePath, targetUri.Host));
//    request.CookieContainer.Add(new Cookie("KievRPSSecAuth", "FAAqARRaTOJILtFsMkpLVWSG6AN6C%2fsvRwNmAAAEgAAACGf7owM5+7bi6AAovEYdADpH1dEtoxEi1TnGs8jZzKrOsaLzt51DuYN3dkLxLjiCc5HJMJI5z7GavLSsr3UCq3aS5DiD+vyijELp44%2fAV+3pPVjF3UZi8DV5FLpbQRItsu2nKPgIuZVno+BwKV1Lm9uzguPVtYOIxUzj0tIfavHN06kkBFBYWOD+nudTyt5MKAJ6AKblR7i8Yh4sGTTEl8li0nDuLiaUGist7wLOEnoMd6S8PgpjYQBDrSOJZq2XkuBuNUba03pctClTJ0cp%2f+03zuxi1vdq%2fYiqFCrEQzwsLbCOLpBQGDr6EhblKpLyg4n6FABAbpb+oJj0nXycR%2fk9z694tlxMGA%3d%3d", targetUri.AbsolutePath, targetUri.Host));
//    request.CookieContainer.Add(new Cookie("NAP", "V=1.9&E=e6f&C=y9WtvRiv_sPbhvxjltR8gEY3hn03rd7-TN5dEH-eda3y4TRqjYynaQ&W=1", targetUri.AbsolutePath, targetUri.Host));
//    request.CookieContainer.Add(new Cookie("WLS", "C=aff49287d31f1ab9&N=Eli&TS=63519997498", targetUri.AbsolutePath, targetUri.Host));
//    request.CookieContainer.Add(new Cookie("SRCHHPGUSR", "CW=1903&CH=666", targetUri.AbsolutePath, targetUri.Host));
//    request.CookieContainer.Add(new Cookie("_HOP", "", targetUri.AbsolutePath, targetUri.Host));
//    request.CookieContainer.Add(new Cookie("DUP", "Q=ht0O7hiLsTkgXKqCajKv&T=185260160&IG=1982157074f54b6e8d2510b2d466c8ea&V=1&A=2", targetUri.AbsolutePath, targetUri.Host));
//    request.CookieContainer.Add(new Cookie("_SS", "SID=290CF01C2DF7466B898F5BCF9019E129&bIm=041585&R=213&nhIm=55-", targetUri.AbsolutePath, targetUri.Host));
//    request.CookieContainer.Add(new Cookie("FBS", "WTS=1384405760053&CR=-1", targetUri.AbsolutePath, targetUri.Host));
//    request.CookieContainer.Add(new Cookie("SCRHDN", "ASD=0&DURL=#", targetUri.AbsolutePath, targetUri.Host));

//    return request;
//}