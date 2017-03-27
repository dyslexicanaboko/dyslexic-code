using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ServerOps;
using PagingSwitchLibrary.DataAccess;

namespace BeeperWebApp
{
    public partial class ManageOwnersAndPagers : GenericPage
    {
        private GridViewRow gvwRow;

        private int _gvwRowID,
                    _pagerOwnerID,
                    _pagerID;

        private DataTable _dtUnassignedPagers,
                          _dtPagers,
                          _dtOwners;

        protected void Page_Load(object sender, EventArgs e)
        {
            OwnerDAL.Initialize();

            if (!IsPostBack)
            {
                LoadOwnersGrid();

                LoadPagerData();
            }
        }

        protected void btnAddOwner_Click(object sender, EventArgs e)
        {
            int intPagerID = -1;

            try
            {
                if (!int.TryParse(ddlExistingPagers.SelectedValue, out intPagerID))
                    intPagerID = -1;

                OwnerDAL.InsertPagerOwner
                (
                    txtFirstName.Text,
                    txtLastName.Text,
                    txtEmail.Text,
                    txtPhone.Text,
                    txtNotes.Text,
                    intPagerID
                );

                LoadOwnersGrid();
                ajaxOwnersUpdatePanel.Update();

                if (intPagerID != -1)
                {
                    LoadPagerData();
                    ajaxPagersUpdatePanel.Update();
                }
            }
            catch (Exception ex)
            {
                LogException(ex);
                
                throw;
            }
        }

        protected void btnAddPager_Click(object sender, EventArgs e)
        {
            int intSubscriberID = 0;

            try
            {
                intSubscriberID = txtSubscriberID.Text.ConvertToInt32();

                if (intSubscriberID == -1)
                {
                    lblMessage.Text = "Invalid Subscriber ID";

                    return;
                }

                PagerDAL.InsertPager
                (
                    intSubscriberID,
                    txtIndividualID.Text.ConvertToInt32(),
                    txtGroupID.Text.ConvertToInt32(),
                    txtMailDropID.Text.ConvertToInt32(),
                    txtBagID.Text.ConvertToInt32(),
                    txtPagerNotes.Text.ConvertToString()
                );

                LoadPagerData();

                ajaxPagerDDLUpdatePanel.Update();
                ajaxPagersUpdatePanel.Update();
            }
            catch (Exception ex)
            {
                LogException(ex);

                throw;
            }
        }

        private void LoadOwnersGrid()
        {
            _dtOwners = OwnerDAL.GetPagerOwners();
            
            gvwOwners.DataSource = _dtOwners;
            gvwOwners.DataBind();
        }

        private void LoadPagerData()
        {
            _dtPagers = PagerDAL.GetPagers();

            gvwPagers.DataSource = _dtPagers;
            gvwPagers.DataBind();

            LoadPagersDropDown();
        }

        private void LoadPagersDropDown()
        { 
            _dtUnassignedPagers = PagerDAL.GetUnassignedPagers();

            ddlExistingPagers.DataSource = _dtUnassignedPagers;
            ddlExistingPagers.DataTextField = "SubscriberID";
            ddlExistingPagers.DataValueField = "PagerID";
            ddlExistingPagers.DataBind();

            ddlExistingPagers.Items.Insert(0, new ListItem("none", "-1"));
        }

        protected void gvwOwners_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            _gvwRowID = Convert.ToInt32(e.CommandArgument);

            gvwRow = gvwOwners.Rows[_gvwRowID];

            _pagerOwnerID = Convert.ToInt32(gvwRow.Cells[0].Text);

            Cache["pagerOwnerID"] = _pagerOwnerID;
        }

        protected void gvwOwners_OnRowEditing(object sender, GridViewEditEventArgs e)
        {
            btnAddOwner.Visible = false;
            btnUpdateOwner.Visible = true;
            txtSubscriberIDEditView.Visible = true;

            txtFirstName.Text = Server.HtmlDecode(gvwRow.Cells[1].Text);
            txtLastName.Text = Server.HtmlDecode(gvwRow.Cells[2].Text);
            txtEmail.Text = Server.HtmlDecode(gvwRow.Cells[3].Text);
            txtPhone.Text = Server.HtmlDecode(gvwRow.Cells[4].Text);
            txtNotes.Text = Server.HtmlDecode(gvwRow.Cells[5].Text);
            txtSubscriberIDEditView.Text = Server.HtmlDecode(gvwRow.Cells[6].Text);

            ajaxPagerDDLUpdatePanel.Update();
        }

        protected void gvwOwners_OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                if (!OwnerDAL.DeleteOwner(_pagerOwnerID))
                    lblMessage.Text = string.Format("Owner {0} Not Deleted", _pagerOwnerID);

                LoadOwnersGrid();
                LoadPagersDropDown();

                ajaxOwnersUpdatePanel.Update();
                ajaxPagerDDLUpdatePanel.Update();
            }
            catch (Exception ex)
            {
                LogException(ex);

                throw;
            }
        }

        protected void gvwPagers_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            _gvwRowID = Convert.ToInt32(e.CommandArgument);

            gvwRow = gvwPagers.Rows[_gvwRowID];

            _pagerID = Convert.ToInt32(gvwRow.Cells[0].Text);

            Cache["pagerID"] = _pagerID;
        }

        protected void gvwPagers_OnRowEditing(object sender, GridViewEditEventArgs e)
        {
            btnAddPager.Visible = false;
            btnUpdatePager.Visible = true;

            txtSubscriberID.Text = Server.HtmlDecode(gvwRow.Cells[1].Text);
            //txtIndividualID.Text = gvwRow.Cells[0].Text;
            //txtGroupID.Text = gvwRow.Cells[0].Text;
            //txtMailDropID.Text = gvwRow.Cells[0].Text;
            txtBagID.Text = Server.HtmlDecode(gvwRow.Cells[2].Text);
            txtPagerNotes.Text = Server.HtmlDecode(gvwRow.Cells[3].Text);

            ajaxPagersUpdatePanel.Update();
        }

        protected void gvwPagers_OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                if (!PagerDAL.DeletePager(_pagerID))
                    lblMessage.Text = string.Format("Pager {0} Not Deleted", _pagerID);

                LoadOwnersGrid();
                LoadPagerData();

                ajaxOwnersUpdatePanel.Update();
                ajaxPagersUpdatePanel.Update();
            }
            catch (Exception ex)
            {
                LogException(ex);

                throw;
            }
        }

        protected void btnUpdateOwner_Click(object sender, EventArgs e)
        {
            try
            {
                btnAddOwner.Visible = true;
                btnUpdateOwner.Visible = false;
                txtSubscriberIDEditView.Visible = false;

                _pagerOwnerID = Convert.ToInt32(Cache["pagerOwnerID"]);

                OwnerDAL.UpdatePagerOwner
                (
                    txtFirstName.Text,
                    txtLastName.Text,
                    txtEmail.Text,
                    txtPhone.Text,
                    txtNotes.Text,
                    ddlExistingPagers.SelectedValue.ConvertToInt32(),
                    _pagerOwnerID
                );

                txtFirstName.Text = string.Empty;
                txtLastName.Text = string.Empty;
                txtEmail.Text = string.Empty;
                txtPhone.Text = string.Empty;
                txtNotes.Text = string.Empty;
                txtSubscriberIDEditView.Text = string.Empty;

                LoadOwnersGrid();
                LoadPagersDropDown();

                ajaxOwnersUpdatePanel.Update();
                ajaxPagerDDLUpdatePanel.Update();
            }
            catch (Exception ex)
            {
                LogException(ex);

                throw;
            }
        }

        protected void btnUpdatePager_Click(object sender, EventArgs e)
        {
            try
            {
                btnAddPager.Visible = true;
                btnUpdatePager.Visible = false;

                PagerDAL.UpdatePager
                (
                    Convert.ToInt32(Cache["pagerID"]),
                    txtSubscriberID.Text.ConvertToInt32(),
                    txtBagID.Text.ConvertToInt32(),
                    txtPagerNotes.Text
                );

                //txtIndividualID.Text = gvwRow.Cells[0].Text;
                //txtGroupID.Text = gvwRow.Cells[0].Text;
                //txtMailDropID.Text = gvwRow.Cells[0].Text;

                txtSubscriberID.Text = string.Empty;
                txtBagID.Text = string.Empty;
                txtPagerNotes.Text = string.Empty;

                LoadOwnersGrid();
                LoadPagerData();

                ajaxPagersUpdatePanel.Update();
                ajaxPagerDDLUpdatePanel.Update();
                ajaxOwnersUpdatePanel.Update();
            }
            catch (Exception ex)
            {
                LogException(ex);

                throw;
            }
        }
    }
}
