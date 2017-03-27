using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ServerOps;

namespace BeeperWebApp
{
    public class GenericPage : WebUtils
    {
        public GenericPage() : base()
        { 
            
        }

        override protected void OnInit(EventArgs e)
        {
            this.Load += new EventHandler(GenericPage_Load);
            base.OnInit(e);
        }

        protected void GenericPage_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //If the user is NOT logged in, then redirect them to the login page.
                if (!Request.IsAuthenticated)
                    Response.Redirect("~/Login.aspx", true);
            }
        }
    }
}
