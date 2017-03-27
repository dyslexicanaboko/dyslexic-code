using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeeperWebApp
{
    /// <summary>
    /// This class is purely for public pages ONLY, it inherits from
    /// the WebUtils base class so it can still use the tools it provides
    /// </summary>
    public class PublicPage : WebUtils
    {
        public PublicPage()
            : base()
        { 
        
        }
    }
}
