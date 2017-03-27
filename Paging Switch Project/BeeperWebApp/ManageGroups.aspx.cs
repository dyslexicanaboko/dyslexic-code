using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PagingSwitchLibrary.DataAccess;

/* Add/Edit/Delete:
 *      Pagers
 *      Owners
 *      Groups
 */

namespace BeeperWebApp
{
    public partial class ManageGroups : GenericPage
    {
        private int _groupID = 0;

        private string _groupName = string.Empty;

        private DataTable _dtPagerOwners,
                          _dtExistingGroups;

        protected void Page_Load(object sender, EventArgs e)
        {
            GroupManagementDAL.Initialize();

            //First time page load
            if (!IsPostBack)
            {
                multiViewManagement.ActiveViewIndex = 0;

                _dtPagerOwners = OwnerDAL.GetPagerOwners();
            }

            //All Post Backs
            if (IsPostBack)
            { 
                
            }

            switch (multiViewManagement.ActiveViewIndex)
            { 
                case 0:
                    if (_dtPagerOwners != null && !IsPostBack)
                        LoadPagerOwners(_dtPagerOwners);
                    break;
                case 1:
                    break;
            }
        }

        protected void gvwOwners_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (!btnCreateGroup.Visible && (e.Row.DataItemIndex >= 0))
            {
                string strPagerOwnerID = e.Row.Cells[0].Text;

                CheckBox cb = e.Row.FindControl("chkBxInclude") as CheckBox;

                DataRow[] arrDr = _dtPagerOwners.Select("PagerOwnerID = " + strPagerOwnerID);

                if (arrDr[0]["Added"].ToString() == "1")
                    cb.Checked = true;
            }
        }

        protected void btnCreateGroup_Click(object sender, EventArgs e)
        {
            try
            {
                _groupID = GroupManagementDAL.InsertGroup(txtGroupName.Text);

                if (_groupID > -1)
                    InsertAllSelectedMembers(_groupID);
                else
                    lblMessages.Text = "Group Exists Already!";
            }
            catch (Exception ex)
            {
                LogException(ex);

                throw;
            }
        }

        protected void btnSaveChanges_Click(object sender, EventArgs e)
        {
            try
            {
                _groupID = Convert.ToInt32(Cache["groupIDForEdit"]);

                //Update the group
                if (GroupManagementDAL.UpdateGroup(_groupID, txtGroupName.Text))
                {
                    //Delete the members
                    if (GroupManagementDAL.DeleteGroupMembers(_groupID))
                    {
                        //Re-Insert the members
                        InsertAllSelectedMembers(_groupID);
                    }
                    else
                        lblMessages.Text = "Could not remove Group Members for Update.";
                }
                else
                    lblMessages.Text = "Could not update group.";

                SetEditView();
            }
            catch (Exception ex)
            {
                LogException(ex);

                throw;
            }
        }

        protected void lnkBtnAddGroup_Click(object sender, EventArgs e)
        {
            SetView(0);

            txtGroupName.Text = string.Empty;

            lblViewAddTitle.Text = "Add New Group";

            btnCreateGroup.Visible = true;
            btnSaveChanges.Visible = false;

            _dtPagerOwners = OwnerDAL.GetPagerOwners();

            LoadPagerOwners(_dtPagerOwners);
        }

        protected void lnkBtnEditGroup_Click(object sender, EventArgs e)
        {
            SetEditView();
        }

        private void SetEditView()
        { 
            SetView(1);

            LoadExistingGroups();
        }

        private void LoadPagerOwners(DataTable dt)
        {
            gvwOwners.DataSource = dt;
            gvwOwners.DataBind();
        }

        public void LoadExistingGroups()
        {
            _dtExistingGroups = GroupManagementDAL.GetExistingGroups();

            gvwEditOwners.DataSource = _dtExistingGroups;
            gvwEditOwners.DataBind();
        }

        protected void gvwEditOwners_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            int intRowID = 0;
            
            GridViewRow gvr = null;

            intRowID = Convert.ToInt32(e.CommandArgument);

            gvr = gvwEditOwners.Rows[intRowID];

            _groupID = Convert.ToInt32(gvr.Cells[0].Text);
            _groupName = gvr.Cells[1].Text;

            lblViewAddTitle.Text = "Edit Group " + _groupID;
        }

        protected void gvwEditOwners_OnRowEditing(object sender, GridViewEditEventArgs e)
        {
            SetView(0);

            btnCreateGroup.Visible = false;
            btnSaveChanges.Visible = true;

            Cache["groupIDForEdit"] = _groupID;

            txtGroupName.Text = _groupName;

            _dtPagerOwners = GroupManagementDAL.GetExistingGroupAndMembers(_groupID);

            LoadPagerOwners(_dtPagerOwners);
        }

        protected void gvwEditOwners_OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (!GroupManagementDAL.DeleteGroup(_groupID))
                lblMessages.Text = string.Format("Group {0} Not Deleted", _groupID);

            LoadExistingGroups();
        }

        private void SetView(int index)
        {
            multiViewManagement.ActiveViewIndex = index;

            lblMessages.Text = string.Empty;
        }

        private bool InsertAllSelectedMembers(int intGroupID)
        {
            bool success = false;
            
            List<string> lstGroupMembers = null;

            try
            {
                lstGroupMembers = GetAllSelectedMembers();

                if (lstGroupMembers.Count > 0)
                {
                    success = GroupManagementDAL.InsertGroupMembers(intGroupID, lstGroupMembers);

                    if (!success)
                        throw new Exception("Members Insert Failed");
                }
            }
            catch (Exception ex)
            {
                LogException(ex);

                throw;
            }

            return success;
        }

        private List<string> GetAllSelectedMembers()
        {
            List<string> lstPageOwnerIds = new List<string>();

            string strPagerOwnerID = string.Empty;
            CheckBox cb = null;

            foreach (GridViewRow gvwRow in gvwOwners.Rows)
            {
                //cb = gvwRow.FindControl("chkBxInclude") as CheckBox;
                cb = gvwRow.FindControl("chkBxInclude") as CheckBox;

                if (cb.Checked)
                {
                    strPagerOwnerID = gvwRow.Cells[0].Text;
                    lstPageOwnerIds.Add(strPagerOwnerID);        
                }
            }

            return lstPageOwnerIds;
        }

        //OnRowDataBound="gvwEditOwners_OnRowDataBound" 
        //protected void gvwEditOwners_OnRowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    int intGroupID = 0;

        //    if (e.Row.GetType().ToString() == "DataControlFieldCell")
        //    {
        //        intGroupID = Convert.ToInt32(e.Row.Cells[0].Text);

        //        //e.Row.Cells[3].Attributes["CommandArgument"]
        //    }
        //}
    }
}
