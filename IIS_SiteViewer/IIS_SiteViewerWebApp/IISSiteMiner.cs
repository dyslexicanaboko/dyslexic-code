using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Web.Administration;

namespace IIS_SiteViewerWebApp
{
    public static class IISSiteMiner
    {
        public static List<AppSiteView> GetSiteView(string siteName)
        {
            Site site = GetSite(siteName);

            String parentURL = GetHostName(site);

            List<AppSiteView> applicationList = new List<AppSiteView>();

            foreach (Application app in GetApplications(site))
            {
                if (app.Path != "/")
                {
                    AppSiteView SiteView = new AppSiteView(parentURL, app.Path);

                    applicationList.Add(SiteView);
                }
            }

            return applicationList;
        }

        public static SiteCollection GetAllSites()
        {
            return new ServerManager().Sites;
        }

        private static Site GetSite(string siteName)
        {
            Site site = (from s in (new ServerManager().Sites) where s.Name == siteName select s).FirstOrDefault();

            if (site == null)
                throw new Exception("The Web Site Name \"" + siteName + "\" could not be found in IIS!");

            return site;
        }

        private static ApplicationCollection GetApplications(Site site)
        {
            //HttpContext.Current.Trace.Warn("Site ID3: " + site + "/n");
            
            ApplicationCollection appColl = site.Applications;
            
            return appColl;
        }

        private static String GetHostName(Site site)
        {
            BindingCollection bindings = site.Bindings;

            String bind = null;
            
            foreach (Binding binding in bindings)
                if (binding.Host != null)
                    return binding.ToString();
            
            return bind;
        }
    }
}
   