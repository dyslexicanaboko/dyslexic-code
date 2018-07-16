using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace TestingGroundsCS
{
    public class BingSearches
    {
        public static void PerformSearches(int sleepInSeconds = 2, int searches = 100)
        {
            string strBase = "http://www.bing.com/search?q=";
            string strDict = "http://www.wordgenerator.net/application/p.php?id=dictionary_words&type=50_definition&spaceflag=false";
            string strQuery;
            string strURI;
            string strResponse;

            //WebClient bing = new WebClient();
            WebClient dictionary = new WebClient();

            for (int i = 0; i < searches; i++)
            {
                try
                {
                    //strQuery = "test";
                    strQuery = dictionary.DownloadString(strDict).Substring(0, 20);

                    strURI = strBase + strQuery;

                    strResponse = FullRequest(strURI);

                    Console.WriteLine("Search : " + (i + 1) + "\t q=" + strQuery + " Length: " + strResponse.Length + " Contains cvid? " + strResponse.Contains("cvid"));

                    System.Threading.Thread.Sleep(sleepInSeconds * 1000);
                }
                catch
                { 
                    
                }
            }
        }

        public static string FullRequest(string uri)
        {
            Uri targetUri = new Uri(uri);

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(targetUri);
            
            webRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            webRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/30.0.1599.101 Safari/537.36";

            webRequest.Headers["Accept-Encoding"] = "gzip,deflate,sdch";
            webRequest.Headers["Accept-Language"] = "en-US,en;q=0.8";

            webRequest.CookieContainer = new CookieContainer();
            webRequest.CookieContainer.Add(new Cookie("SRCHUID", "V=2&GUID=60E40A245AF2404BB82FF26F702816D8", targetUri.AbsolutePath, targetUri.Host));
            webRequest.CookieContainer.Add(new Cookie("SRCHUSR", "AUTOREDIR=0&GEOVAR=&DOB=20130427", targetUri.AbsolutePath, targetUri.Host));
            webRequest.CookieContainer.Add(new Cookie("MUIDB", "1A5282975BEB6FCB38A381B25FEB6F8F", targetUri.AbsolutePath, targetUri.Host));
            webRequest.CookieContainer.Add(new Cookie("PPLState", "1", targetUri.AbsolutePath, targetUri.Host));
            webRequest.CookieContainer.Add(new Cookie("_RwBf", "A=672534CC4476B6522189C218FFFFFFFF&s=10&o=0", targetUri.AbsolutePath, targetUri.Host));
            webRequest.CookieContainer.Add(new Cookie("RMS", "F=OAAABgAAAAR", targetUri.AbsolutePath, targetUri.Host));
            webRequest.CookieContainer.Add(new Cookie("_SSI", "CW=1903&CH=995", targetUri.AbsolutePath, targetUri.Host));
            webRequest.CookieContainer.Add(new Cookie("_UR", "OMW=1", targetUri.AbsolutePath, targetUri.Host));
            webRequest.CookieContainer.Add(new Cookie("MUID", "1A5282975BEB6FCB38A381B25FEB6F8F", targetUri.AbsolutePath, targetUri.Host));
            webRequest.CookieContainer.Add(new Cookie("_FS", "NU=1&mkt=en-US", targetUri.AbsolutePath, targetUri.Host));
            webRequest.CookieContainer.Add(new Cookie("SRCHD", "MS=3087584&D=2798640&AF=NOFORM&SM=1", targetUri.AbsolutePath, targetUri.Host));
            webRequest.CookieContainer.Add(new Cookie("_U", "1OO1gQTJL0ZyRq23PFKwPdYIGvqZ7R03qUU5rg1wox-eJ1eXGIWMJakI7NT64tJp2poe-gr3sqT4gX9bmNkRhXLwwBtQFGcnN713wp2mQarU", targetUri.AbsolutePath, targetUri.Host));
            webRequest.CookieContainer.Add(new Cookie("ANON", "A=672534CC4476B6522189C218FFFFFFFF&E=ec9&W=1", targetUri.AbsolutePath, targetUri.Host));
            webRequest.CookieContainer.Add(new Cookie("KievRPSAuth", "FAAqARRaTOJILtFsMkpLVWSG6AN6C%2fsvRwNmAAAEgAAACO31TY1hpwv36ABMiKlBdO1KCbcjoKPlFLDiDSQjU+6%2fKQf7l3Xa3X4aV4vnnimPvqYB1Sus6eYcpAPYKf7oG2JS2bMD3%2f+SYRscdMdyPmW7MmaxF6qr2ZhleJokHEBcnsFpXoB52Ltfqtvkl7s6S0Jc%2fLDgCw1erJBb+Ixhx3sLa2bvthOJzrgIs8IXNrIYHu9zZs+MW6+lnXCW2GjkJIFOtqqe6%2fJxe4ZSXl1tZNEZZQnS5Dv4zEHtTit8YoDL5pE6W+rV6UMgc6n9p3Hjoa40INOdzqdDj8qkey6LuYDDqMxPci96sT8L%2fn1BpS6w5iFzFADD7pOyP9U8zF7rHOfX0X8gmKa3Iw%3d%3d", targetUri.AbsolutePath, targetUri.Host));
            webRequest.CookieContainer.Add(new Cookie("KievRPSSecAuth", "FAAqARRaTOJILtFsMkpLVWSG6AN6C%2fsvRwNmAAAEgAAACGf7owM5+7bi6AAovEYdADpH1dEtoxEi1TnGs8jZzKrOsaLzt51DuYN3dkLxLjiCc5HJMJI5z7GavLSsr3UCq3aS5DiD+vyijELp44%2fAV+3pPVjF3UZi8DV5FLpbQRItsu2nKPgIuZVno+BwKV1Lm9uzguPVtYOIxUzj0tIfavHN06kkBFBYWOD+nudTyt5MKAJ6AKblR7i8Yh4sGTTEl8li0nDuLiaUGist7wLOEnoMd6S8PgpjYQBDrSOJZq2XkuBuNUba03pctClTJ0cp%2f+03zuxi1vdq%2fYiqFCrEQzwsLbCOLpBQGDr6EhblKpLyg4n6FABAbpb+oJj0nXycR%2fk9z694tlxMGA%3d%3d", targetUri.AbsolutePath, targetUri.Host));
            webRequest.CookieContainer.Add(new Cookie("NAP", "V=1.9&E=e6f&C=y9WtvRiv_sPbhvxjltR8gEY3hn03rd7-TN5dEH-eda3y4TRqjYynaQ&W=1", targetUri.AbsolutePath, targetUri.Host));
            webRequest.CookieContainer.Add(new Cookie("WLS", "C=aff49287d31f1ab9&N=Eli&TS=63519997498", targetUri.AbsolutePath, targetUri.Host));
            webRequest.CookieContainer.Add(new Cookie("SRCHHPGUSR", "CW=1903&CH=666", targetUri.AbsolutePath, targetUri.Host));
            webRequest.CookieContainer.Add(new Cookie("_HOP", "", targetUri.AbsolutePath, targetUri.Host));
            webRequest.CookieContainer.Add(new Cookie("DUP", "Q=ht0O7hiLsTkgXKqCajKv&T=185260160&IG=1982157074f54b6e8d2510b2d466c8ea&V=1&A=2", targetUri.AbsolutePath, targetUri.Host));
            webRequest.CookieContainer.Add(new Cookie("_SS", "SID=290CF01C2DF7466B898F5BCF9019E129&bIm=041585&R=213&nhIm=55-", targetUri.AbsolutePath, targetUri.Host));
            webRequest.CookieContainer.Add(new Cookie("FBS", "WTS=1384405760053&CR=-1", targetUri.AbsolutePath, targetUri.Host));
            webRequest.CookieContainer.Add(new Cookie("SCRHDN", "ASD=0&DURL=#", targetUri.AbsolutePath, targetUri.Host));

            WebResponse webRequestResponse = webRequest.GetResponse();

            string strResponse;

            using (StreamReader sr = new StreamReader(webRequestResponse.GetResponseStream()))
            {
                strResponse = sr.ReadToEnd();

                //Console.WriteLine(strResponse);
                //Console.WriteLine(strResponse.Contains("cvid"));
            }

            return strResponse;
        }
    }
}
