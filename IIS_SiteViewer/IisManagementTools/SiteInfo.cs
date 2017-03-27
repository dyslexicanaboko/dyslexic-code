using Microsoft.Web.Administration;
using System.Collections.Generic;

namespace IisManagementTools
{
    public class SiteInfo : ApplicationInfo
    {
        public SiteInfo()
            : base()
        {
            Applications = new List<ApplicationInfo>();
        }

        public List<ApplicationInfo> Applications { get; set; }

        public ObjectState State { get; set; }
    }
}
