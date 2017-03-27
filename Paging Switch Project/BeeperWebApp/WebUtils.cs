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
    public class WebUtils : System.Web.UI.Page
    {
        protected Label lblMessages = new Label();

        protected void SetMessage(string message, Color clrMessage)
        {
            lblMessages.Text = message;
            lblMessages.ForeColor = clrMessage;
        }

        protected void Message(string message)
        {
            SetMessage(message, Color.Black);
        }

        protected void ErrorMessage(string message)
        {
            SetMessage(message, Color.Red);
        }

        protected void SuccessMessage(string message)
        {
            SetMessage(message, Color.Green);
        }

        protected void WarningMessage(string message)
        {
            SetMessage(message, Color.Yellow);
        }

        protected void LogException(Exception ex)
        {
            ExceptionHandler.RecordExceptionToFile(string.Empty, ex, Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\_log\\" + Utils.DateBasedIdentifier("BeeperWebApp_", Utils.Order.Prefix) + ".log");
        }

        protected void LogNotes(string notes)
        {
            ExceptionHandler.RecordNotes(notes);
        }
    }
}
