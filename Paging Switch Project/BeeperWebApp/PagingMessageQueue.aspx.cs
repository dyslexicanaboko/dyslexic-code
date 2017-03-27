using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PagingSwitchLibrary.DataAccess;
using PagingSwitchLibrary.BusinessObjects;

namespace BeeperWebApp
{
    public partial class PagingMessageQueue : GenericPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PagingQueueDAL.Initialize();

                LoadPagingQueue(PagingQueue.GetAllMessages());
            }
        }

        protected void ddlMessageTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadPagingQueue(PagingQueue.GetMessages(Convert.ToInt32(ddlMessageTypes.SelectedValue)));
        }

        private void LoadPagingQueue(object dataSource)
        {
            gvwPagingQueue.DataSource = dataSource;
            gvwPagingQueue.DataBind();
        }

        protected void gvwPagingQueue_OnRowDatabound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Button btnField = (Button)e.Row.Cells[8].Controls[0];

                btnField.CommandArgument = e.Row.RowIndex.ToString(); //Copy the QueueID to the Command Button Argument
            }
        }
        
        protected void gvwPagingQueue_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "apply")
            {
                int intRowIndex = 0,
                    intQueueID = 0,
                    intAction = 0;

                intRowIndex = Convert.ToInt32(e.CommandArgument);

                GridViewRow gvr = gvwPagingQueue.Rows[intRowIndex];

                intQueueID = Convert.ToInt32(gvr.Cells[0].Text);

                DropDownList dllOptions = (DropDownList)gvr.Cells[7].FindControl("ddlActions");

                intAction = Convert.ToInt32(dllOptions.SelectedValue);
            }
        }
    }
}
