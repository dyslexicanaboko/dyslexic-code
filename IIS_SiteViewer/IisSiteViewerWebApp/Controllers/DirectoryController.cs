using IisManagementTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IisSiteViewerWebApp.Controllers
{
    public class DirectoryController : Controller
    {
        // GET: Directory
        public ActionResult IndexFilter(Models.DirectoryModel directory)
        {
            var identityType = directory.IdentityType;
            var serviceAccount = directory.ServiceAccount;

            //Microsoft.Web.Administration.ProcessModelIdentityType? identityType, string serviceAccount

            List<SiteInfo> lst = GetAllSites();

            //Doing a lambda query was difficult
            if (identityType.HasValue)
            {

                IEnumerable<ApplicationInfo> remove = null;

                if (serviceAccount == null)
                    remove = lst.SelectMany(x => x.Applications.Where(y => y.ApplicationPool.IdentityType != identityType.Value));
                else
                    remove = lst.SelectMany(x => x.Applications.Where(y => y.ApplicationPool.IdentityType != identityType.Value && y.ApplicationPool.IdentityUser != serviceAccount));

                List<ApplicationInfo> lstRemove = remove.ToList();

                SiteInfo si = null;

                for (int i = lst.Count - 1; i > -1; i--)
                {
                    si = lst[i];

                    foreach (ApplicationInfo a in lstRemove)
                        si.Applications.Remove(a);

                    if (si.Applications.Count == 0)
                        lst.RemoveAt(i);
                }
            }

            ModelState.Clear();

            return View("Index", lst);
        }

        public ActionResult Index()
        {
            return View(GetAllSites());   
        }

        private List<SiteInfo> GetAllSites()
        {
            using (var svc = new IisManagementService())
            {
                return svc.GetSitesDirectory();
            }
        }
    }
}