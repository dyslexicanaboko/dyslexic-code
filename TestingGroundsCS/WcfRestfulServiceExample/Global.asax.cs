using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Routing;
using System.ServiceModel.Activation;

namespace ServiceApp
{
    public class Global : System.Web.HttpApplication
    {
        protected void RegisterRoutes(RouteCollection routeTable)
        {
            routeTable.Add(new ServiceRoute("fucko", new WebServiceHostFactory(), typeof(AmazingService)));
            //routeTable.Add(new ServiceRoute("OData", new WebServiceHostFactory(), typeof(AmazingService)));
        }

        protected void Application_Start(object sender, EventArgs e)
        {
            //RegisterRoutes(RouteTable.Routes);
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}