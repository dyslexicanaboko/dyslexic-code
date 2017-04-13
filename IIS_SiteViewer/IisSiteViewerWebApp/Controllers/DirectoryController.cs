using IisManagementTools;
using IisSiteViewerWebApp.Models;
using Microsoft.Web.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace IisSiteViewerWebApp.Controllers
{
    //[Authorize]
    public class DirectoryController : Controller
    {
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult IndexFilter(DirectoryModel model) //FormCollection form
        {
            var m = model;

            List<SiteInfo> lst = GetAllSites();

            //Doing a lambda query was difficult
            if (m.IdentityType.HasValue)
            {

                IEnumerable<ApplicationInfo> remove = null;

                if (m.ServiceAccount == null)
                    remove = lst.SelectMany(x => x.Applications.Where(y => y.ApplicationPool.IdentityType != m.IdentityType.Value));
                else
                    remove = lst.SelectMany(x => x.Applications.Where(y => y.ApplicationPool.IdentityType != m.IdentityType.Value && y.ApplicationPool.IdentityUser != m.ServiceAccount));

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

            m.Data = lst;

            return View("Index", m);
        }

        private void GetModelFromFormCollection(FormCollection form)
        {
            ProcessModelIdentityType? identityType = null;
            ProcessModelIdentityType it = ProcessModelIdentityType.ApplicationPoolIdentity;

            if (Enum.TryParse<ProcessModelIdentityType>(Convert.ToString(form["ddlIdentityType"]), out it))
                identityType = it;

            var serviceAccount = Convert.ToString(form["ddlServiceAccount"]);
        }

        private DirectoryModel GetModel(List<SiteInfo> data)
        {
            return new DirectoryModel() { Data = data };
        }

        public ActionResult Index()
        {
            return View(GetModel(GetAllSites()));
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