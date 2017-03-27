using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IisManagementTools;
using Microsoft.Web.Administration;
using System.Web.Mvc;

namespace IisSiteViewerWebApp.Models
{
    public class DirectoryModel
    {
        public ProcessModelIdentityType? IdentityType { get; set; }

        public string ServiceAccount { get; set; }
    }
}