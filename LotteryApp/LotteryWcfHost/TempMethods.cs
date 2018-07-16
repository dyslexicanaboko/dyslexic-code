using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LotteryWcfHost
{
    public static class TempMethods
    {
        public static IEnumerable<Control> FlattenChildren(this Control control)
        {
            var children = control.Controls.Cast<Control>();

            return children.SelectMany(c => FlattenChildren(c)).Concat(children);
        }

        public static Label GetLabel(this Control control, string labelID)
        {
            return (Label)control.FindControl(labelID);
        }

        public static TextBox GetTextBox(this Control control, string textBoxID)
        {
            return (TextBox)control.FindControl(textBoxID);
        }
    }
}