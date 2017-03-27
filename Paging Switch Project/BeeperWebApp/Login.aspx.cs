using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace BeeperWebApp
{
    //This class should NOT implement the CommonBasePage, if you do implement it, 
    //there will be an infinite page load loop.
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            lblError.Visible = false;

            if (!string.IsNullOrEmpty(txtUserName.Text) && !string.IsNullOrEmpty(txtPassWord.Text))
            {
                //if (txtUserName.Text == "a" && txtPassWord.Text == "a") //This is for debugging only
                if (Membership.ValidateUser(txtUserName.Text, txtPassWord.Text))
                {
                    FormsAuthentication.RedirectFromLoginPage(txtUserName.Text, false);
                    Response.Redirect("Default.aspx", false);
                }
                else
                    ShowError("Invalid Creds");
            }
            else
                ShowError("Either the Username or Password is blank.");
        }
   
        private void ShowError(string msg)
        {
            lblError.Text = msg;
            lblError.Visible = true;
            divError.Visible = true;
        }
    }
}
