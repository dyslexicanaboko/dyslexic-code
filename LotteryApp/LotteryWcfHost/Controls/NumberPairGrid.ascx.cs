using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LotteryBaseLogic;
using LotteryWcfService;

namespace LotteryWcfHost.Controls
{
    public partial class NumberPairGrid : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        public void CompareSet(List<string> numbers)
        {
            if (numbers == null || numbers.Count == 0)
                return;

            //This is a list of numbers to check - there will only be one winning number
            List<LotteryNumbers> lst = new List<LotteryNumbers>();

            foreach (string n in numbers)
            {
                if(!string.IsNullOrWhiteSpace(n))
                    lst.Add(new LotteryNumbers(n));
            }

            LotteryService client = new LotteryService();
             
            LotteryInfo info = client.GetTodaysWinningNumber();

            LotteryNumbers win = new LotteryNumbers(info.Number);

            List<LNPair> gLst = new List<LNPair>();

            foreach (LotteryNumbers ln in lst)
                gLst.Add(new LNPair(win, ln));

            gvw.DataSource = gLst;
            gvw.DataBind();
        }

        protected void gvw_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
                SetLotteryNumberSetColor(e.Row);
        }

        private void SetLotteryNumberSetColor(GridViewRow row)
        {
            SetColorsForCombination(row, "L", "Lotto");
            SetColorsForCombination(row, "P", "Player");
        }

        private void SetColorsForCombination(GridViewRow row, string colorPrefix, string boxPrefix)
        {
            for (int i = 1; i <= 6; i++)
                row.GetTextBox("txt" + boxPrefix + i).BackColor = System.Drawing.Color.FromName(row.GetLabel("lbl" + colorPrefix + "Color" + i).Text);
        }
    }
}