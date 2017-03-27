using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using PagingSwitchLibrary.DataAccess;

namespace PagingSwitchLibrary.BusinessObjects
{
    public class Groups : BaseMethods
    {
        public static List<int> GetDistinctSubscriberIDs(List<int> lstGroupIDs)
        {
            DataTable dt = null;
            StringBuilder sb = null;
            List<int> lstSubscriberIDs = null;

            try
            {
                sb = new StringBuilder();
                
                lstSubscriberIDs = new List<int>();

                sb.Append("'");

                foreach (int groupID in lstGroupIDs)
                {
                    sb.Append(groupID);
                    sb.Append(",");
                }

                sb.Remove(sb.Length - 1, 1);

                sb.Append("'");

                dt = GroupManagementDAL.GetDistinctSubscriberIDs(sb.ToString());

                foreach (DataRow dr in dt.Rows)
                    lstSubscriberIDs.Add(Convert.ToInt32(dr["SubscriberID"]));
            }
            catch (Exception ex)
            {
                LogException(ex);

                throw;
            }

            return lstSubscriberIDs;
        }

        public static List<int> GetSelectedValues(GridView gvw, int cellID, string checkBoxName)
        {
            List<int> lstSelectedIds = new List<int>();

            string strID = string.Empty;
            CheckBox cb = null;

            foreach (GridViewRow gvwRow in gvw.Rows)
            {
                cb = gvwRow.FindControl(checkBoxName) as CheckBox;

                if (cb.Checked)
                {
                    strID = gvwRow.Cells[cellID].Text;
                    lstSelectedIds.Add(Convert.ToInt32(strID));
                }
            }

            return lstSelectedIds;
        }
    }
}
