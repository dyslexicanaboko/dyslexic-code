using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LotteryWcfHost.Controls;

namespace LotteryWcfHost
{
    public partial class CheckNumbers : System.Web.UI.Page
    {
        private const string CookieName = "FlotteryNumbers";
        private const string TextBox = "TextBox";
        private const string CheckBox = "CheckBox";
        private HttpCookie NumbersCookie
        {
            get { return Request.Cookies[CookieName]; }
            set { Response.Cookies.Set(value); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            lblTimeStamp.Text = DateTime.Now.ToString();

            if (!IsPostBack)
                LoadNumbers();
        }

        protected void btnCheckNumbers_Click(object sender, EventArgs e)
        {
            if (chkBxSaveNumbers.Checked)
                SaveNumbers();
            else
                ClearNumbers();

            ctrlNumbers.CompareSet(txtNumbers.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None).ToList());
        }

        private void LoadNumbers()
        {
            HttpCookie c = NumbersCookie;

            if (c != null)
            {
                txtNumbers.Text = Server.UrlDecode(c[TextBox]);
                chkBxSaveNumbers.Checked = Convert.ToBoolean(c[CheckBox]);
            }
        }

        private void SaveNumbers()
        {
            HttpCookie c = NumbersCookie;

            if (c == null)
                c = new HttpCookie(CookieName);

            c[TextBox] = Server.UrlEncode(txtNumbers.Text);
            c[CheckBox] = chkBxSaveNumbers.Checked.ToString();
            c.Expires = DateTime.Now.AddMonths(1);

            NumbersCookie = c;
        }

        private void ClearNumbers()
        {
            HttpCookie c = NumbersCookie;

            c[TextBox] = string.Empty;
            c[CheckBox] = chkBxSaveNumbers.Checked.ToString();

            NumbersCookie = c;
        }
    }
}