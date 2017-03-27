using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PagingSwitchLibrary.BusinessObjects;
using PagingSwitchLibrary.DataAccess;

namespace BeeperWebApp
{
    public partial class SendPage : PublicPage
    {
        private DataTable _dtOwners,
                          _dtGroups;

        protected void Page_Load(object sender, EventArgs e)
        {
            GroupManagementDAL.Initialize();
        }

        protected void btnSend_OnClick(object sender, EventArgs e)
        {
            PagingQueueDAL.Initialize();

            BuildPagingListAndSend();
        }

        protected void lnkBtnShowOwners_Click(object sender, EventArgs e)
        {
            if (!gvwOwners.Visible)
                _dtOwners = OwnerDAL.GetPagerOwners();

            BindAndShowGrid(lnkBtnShowOwners, gvwOwners, _dtOwners);
        }

        protected void lnkBtnShowGroups_Click(object sender, EventArgs e)
        {
            if(!gvwGroups.Visible)
                _dtGroups = GroupManagementDAL.GetExistingGroups();

            BindAndShowGrid(lnkBtnShowGroups, gvwGroups, _dtGroups);
        }

        //This function is for the grids, when asked to be shown, depending on
        //the visible state this function will either show or not show the grid
        //and change the invoking link button text
        private void BindAndShowGrid(LinkButton linkButton, GridView gvw, object dataSource)
        {
            string strText = linkButton.Text.ToLower();

            //Find out which button invoked this function
            if (strText.Contains("own"))
                strText = "Owners";
            else
                strText = "Groups";

            //If the gridview is not visible
            if (!gvw.Visible)
            {
                strText = "Hide " + strText; //Change text

                //Make the grid visible and bind to data source
                gvw.Visible = true;
                gvw.DataSource = dataSource;
                gvw.DataBind();
            }
            else //Otherwise do the exact opposite
            {
                strText = "Show " + strText;

                gvw.Visible = false;
            }

            linkButton.Text = strText; //Update the link button text

            ajaxUpdatePanel.Update(); //Update the AJAX Update Panel
        }

        //This function literally builds a list of subscriberIDs and then sends
        //the provided message to them.
        private void BuildPagingListAndSend()
        {
            List<int> lstSubscriberID = null,
                      lstGroupID = null,
                      lstOwnerSubscriberID = null;

            try
            {
                lstSubscriberID = new List<int>();

                //Only add the fill in box to the list if there is something there
                if(!string.IsNullOrEmpty(txtSubscriberID.Text))
                    lstSubscriberID.Add(Convert.ToInt32(txtSubscriberID.Text));

                //Only add selected elements to the list if the grid is visible
                if (gvwGroups.Visible)
                {
                    //Get the selected elements
                    lstGroupID = Groups.GetSelectedValues(gvwGroups, 0, "chkBxInclude");

                    //For the selected groups, get the subscriber IDs
                    lstOwnerSubscriberID = Groups.GetDistinctSubscriberIDs(lstGroupID);

                    //Combine it with the master list of subscriber IDs
                    CombineSubscriberLists(lstSubscriberID, lstOwnerSubscriberID);
                }

                if (gvwOwners.Visible)
                {
                    lstOwnerSubscriberID = Groups.GetSelectedValues(gvwOwners, 1, "chkBxInclude");

                    CombineSubscriberLists(lstSubscriberID, lstOwnerSubscriberID);
                }

                if (lstSubscriberID.Count > 0)
                {
                    PagingQueue.InsertMessageInQueue(lstSubscriberID, txtMessageText.Text, Request.UserHostAddress);

                    lblMessage.Text = "Pager message(s) put in the Paging Queue";

                    if (!chkBxPreserveData.Checked)
                    {
                        gvwGroups.Visible = false;
                        gvwOwners.Visible = false;
                        txtMessageText.Text = string.Empty;
                        txtSubscriberID.Text = string.Empty;
                    }
                }
                else
                    lblMessage.Text = "You need to select or provide at least one subscriber!";
            }
            catch (Exception ex)
            {
                LogException(ex);

                throw;
            }
        }

        //This function will take the master list of subscriber IDs and add any elements
        //it doesn't have with the contributing list. No duplicates will be added.
        private void CombineSubscriberLists(List<int> lstMaster, List<int> lstContributor)
        {
            foreach (int id in lstContributor)
            {
                if (!lstMaster.Contains(id))
                    lstMaster.Add(id);
            }
        }
    }
}
