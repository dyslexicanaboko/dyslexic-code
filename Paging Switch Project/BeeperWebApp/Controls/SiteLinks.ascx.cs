using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BeeperWebApp.Controls
{
    public partial class SiteLinks : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<string> lstPageLinks = new List<string>();

                DirectoryInfo dir = new DirectoryInfo(Server.MapPath("~/"));

                FileInfo[] pages = dir.GetFiles("*.aspx");

                ddlSiteLinks.DataSource = pages;
                ddlSiteLinks.DataTextField = "Name";
                ddlSiteLinks.DataValueField = "Name";
                ddlSiteLinks.DataBind();

                ddlSiteLinks.Items.Insert(0, "Navigation...");
            }
        }

        protected void ddlSiteLinks_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect(ddlSiteLinks.SelectedValue);
        }
    }
}