using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BeeperWebApp
{
    public partial class _Default : GenericPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            List<string> lstPageLinks = new List<string>();

            DirectoryInfo dir = new DirectoryInfo(Server.MapPath("~/"));

            FileInfo[] pages = dir.GetFiles("*.aspx");

            gvwDirectory.DataSource = pages;
            gvwDirectory.DataBind();
        }
    }
}
