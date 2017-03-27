using Microsoft.Web.Administration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
//C:\Windows\System32\inetsrv\Microsoft.Web.Administration.dll

namespace IisManagementTools
{
    public class IisManagementService : IDisposable
    {
        public const string DEFAULT_DOCUMENT = "system.webServer/defaultDocument";

        ServerManager _manager = null;
        Regex _regex = new Regex(@"\d");

        public IisManagementService()
        {
            _manager = new ServerManager();
        }

        public void Dispose()
        {
            _manager.Dispose();
        }

        public SiteCollection GetSites()
        {
            return _manager.Sites;
        }

        public List<string> GetDefaultDocuments(Site site)
        {
            Configuration webConfig = site.GetWebConfiguration();
            ConfigurationSection defaultDocumentSection = webConfig.GetSection(DEFAULT_DOCUMENT);
            ConfigurationElementCollection filesCollection = defaultDocumentSection.GetCollection("files");

            //filesCollection[0].GetAttributeValue("value")
            return filesCollection.Select(x => Convert.ToString(x.GetAttributeValue("value"))).ToList();
        }

        public List<SiteInfo> GetSitesDirectory()
        {
            SiteCollection sites = GetSites();

            var lst = new List<SiteInfo>();

            foreach (Site s in sites)
                lst.Add(GetSiteInfoFromSite(s));

            return lst;
        }

        private bool DoesSitePhysicalPathExist(Site site)
        {
            return Directory.Exists(site.Applications[0].VirtualDirectories[0].PhysicalPath);
        }

        public SiteInfo GetSiteInfoFromSite(Site site)
        {
            var lst = new List<SiteInfo>();
            
            ApplicationPool ap = null;

            var obj = new SiteInfo()
            {
                Name = site.Name,
                State = site.State
            };

            if (DoesSitePhysicalPathExist(site))
            {
                obj.DefaultDocument = GetDefaultDocuments(site)[0]; //I am not sure if this makes sense yet

                foreach (Application app in site.Applications)
                {
                    ap = _manager.ApplicationPools[app.ApplicationPoolName];

                    obj.Applications.Add(new ApplicationInfo(site, app, ap)
                    {
                        DefaultDocument = GetDefaultDocuments(site)[0], //I am not sure if this makes sense yet
                    });
                }
            }

            return obj;
        }

        public List<ApplicationPool> GetApplicationPools(ProcessModelIdentityType? identityType = null, string applicationPoolName = null)
        {
            IEnumerable<ApplicationPool> q = _manager.ApplicationPools;

            if(identityType.HasValue)
                q = q.Where(x => x.ProcessModel.IdentityType == identityType.Value);

            if (q.Count() == 0)
                throw new ApplicationException($"No application pools were found for the ProcessModelIdentityType \"{identityType}\".");

            //If an application pool name is actually provided then search by that name
            if (!string.IsNullOrWhiteSpace(applicationPoolName))
                q = q.Where(x => x.Name == applicationPoolName);

            if (q.Count() == 0)
                throw new ApplicationException($"There are no application pools that match the search criteria: \"{applicationPoolName}\" (Tip: Searches are an exact match).");

            return q.ToList();
        }

        public List<ApplicationPoolCredential> GetApplicationPoolCredentials(string applicationPoolName = null)
        {
            return GetApplicationPools(ProcessModelIdentityType.SpecificUser, applicationPoolName)
                    .Select(x => new ApplicationPoolCredential(x))
                    .ToList();
        }

        public void ChangeCredentials(string username, string password, string applicationPoolName = null)
        {
            List<ApplicationPoolCredential> lst = GetApplicationPoolCredentials(applicationPoolName);

            foreach (ApplicationPoolCredential pm in lst)
            {
                pm.Username = username;
                pm.Password = password;
            }

            _manager.CommitChanges();
        }
    }
}
