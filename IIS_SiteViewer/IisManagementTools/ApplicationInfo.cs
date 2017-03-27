using Microsoft.Web.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace IisManagementTools
{
    public class ApplicationInfo
    {
        public const string LOCALHOST = "localhost";

        private Regex _regex = new Regex(@"\d");
        private Application _application;
        private Site _site;

        protected ApplicationInfo()
        {

        }

        public ApplicationInfo(Site site, Application application, ApplicationPool applicationPool)
        {
            _site = site;
            _application = application;

            ApplicationPool = new ApplicationPoolInfo(applicationPool);

            Name = GetApplicationName(site, application);

            Uris = GetUris(site, application);

            //DefaultDocument = GetDefaultDocuments(site)[0], //I am not sure if this makes sense yet
        }

        public string Name { get; set; }

        public string DefaultDocument { get; set; }

        public ApplicationPoolInfo ApplicationPool { get; set; }

        public List<Uri> Uris { get; set; }

        private string GetApplicationName(Site site, Application application)
        {
            string strPath = application.Path.Replace("/", string.Empty);

            return string.IsNullOrWhiteSpace(strPath) ? "Root" : strPath;
        }

        private List<Uri> GetUris(Site site, Application application)
        {
            var lst = new List<Uri>();

            UriBuilder ub = null;

            foreach (Binding b in site.Bindings.Where(x => HasPortNumber(x)).ToList())
            {
                ub = new UriBuilder();
                ub.Scheme = b.Protocol;
                ub.Host = GetHost(b);
                ub.Port = GetPort(b);
                ub.Path = application.Path;

                lst.Add(ub.Uri);
            }

            return lst;
        }

        private string GetHost(Binding binding)
        {
            if (!string.IsNullOrWhiteSpace(binding.Host))
                return binding.Host;

            return LOCALHOST;
        }

        private int GetPort(Binding binding)
        {
            if (binding.EndPoint != null)
                return binding.EndPoint.Port;

            return Convert.ToInt32(binding.BindingInformation.Replace(":", string.Empty).Replace("*", string.Empty));
        }

        private bool HasPortNumber(Binding binding)
        {
            return _regex.IsMatch(binding.BindingInformation);
        }
    }
}
