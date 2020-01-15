using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IIS_SiteViewerWebApp
{
    /// <summary>
    /// Summary description for AppSiteView
    /// </summary>
    public class AppSiteView
    {
        #region Properties
        public String VirtualPath { get; set; }
        
        public String ParentURL { get; set; }
        
        public String Name
        {
            get { return VirtualPath.TrimStart('/'); }
        }

        public String AbsoluteURL
        {
            get { return ParentURL.TrimStart('*', ':', '8', '0', ':') + VirtualPath; }
        }
        #endregion

        public AppSiteView(String parentURL, String virtualPath)
	    {
            ParentURL = parentURL;
            
            VirtualPath = virtualPath;
	    }
    }
}