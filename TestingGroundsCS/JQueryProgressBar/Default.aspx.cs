using System;
using System.Threading;

namespace WebApplication1
{
    public partial class _Default : System.Web.UI.Page
    {
        [System.Web.Services.WebMethod]         
        public static string GetText()
        {
            for (int i = 0; i < 10; i ++)
            {                
                Thread.Sleep(1000);
            }

            Code.GetText();

            return "All finished!";
        }
    }
}
