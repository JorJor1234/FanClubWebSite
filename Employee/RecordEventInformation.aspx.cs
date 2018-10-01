using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;

public partial class Employee_RecordEventInformation : System.Web.UI.Page
{
    FanClubDB myFanClubDB = new FanClubDB();
    Helpers myHelpers = new Helpers();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PopulateFanClubDropDownList();
            pnlEventNames.Visible = false;
            pnlJoinsEvent.Visible = false;
        }
    }

    protected void btnRecordEventInformation_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            // Update the paid fee and attended event information.
            foreach (GridViewRow row in gvJoinsEvent.Rows)
            {
                DropDownList ddlPaidFee = (DropDownList)(row.FindControl("ddlPaidFee"));
                DropDownList ddlAttended = (DropDownList)(row.FindControl("ddlAttended"));
                string paidFee = ddlPaidFee.SelectedValue;
                string attended = ddlAttended.SelectedValue;
                string eventId = row.Cells[2].Text.Trim();
                string userName = row.Cells[3].Text.Trim();

                //***************
                // Uses TODO 20 *
                //***************
                if (myFanClubDB.UpdatePaidFeeAndAttendance(eventId, userName, paidFee, attended))
                {
                    myHelpers.ShowMessage(lblResultMessage, "The paid fee and attendance information for the event - " + ddlEvents.SelectedItem.Text + " - has been recorded.");
                }
                else
                {
                    myHelpers.ShowMessage(lblResultMessage, "*** There is an error in the UPDATE statement for recording the event fee paid and attendance.");
                }
            }
        }
    }

    protected void PopulateFanClubDropDownList()
    {
        DataTable dtFanClubs = myFanClubDB.GetFanClubs();

        // Populate the dropdown list with the fan club ids and names if the result is not null and contains the required data.
        if (dtFanClubs != null)
        {
            if (dtFanClubs.Columns.Contains("CLUBID") & dtFanClubs.Columns.Contains("CLUBNAME"))
            {
                if (dtFanClubs.Rows.Count != 0)
                {
                    ddlFanClubs.DataSource = dtFanClubs;
                    ddlFanClubs.DataValueField = "CLUBID";
                    ddlFanClubs.DataTextField = "CLUBNAME";
                    ddlFanClubs.DataBind();
                }
                else
                {
                    myHelpers.ShowMessage(lblResultMessage, "There are no fan clubs.");
                }
            }
            else // An SQL error occurred.
            {
                myHelpers.ShowMessage(lblResultMessage, "*** The SELECT statement does not retrieve the the fan club ids and/or names.");
            }
        }
        else // An SQL error occurred.
        {
            myHelpers.ShowMessage(lblResultMessage, "*** There is an error in the SELECT statement that retrieves the fan club information.");
        }
        ddlFanClubs.Items.Insert(0, "-- Select --");
        ddlFanClubs.Items.FindByText("-- Select --").Value = "none selected";
        ddlFanClubs.SelectedIndex = 0;
    }

    protected void PopulateEventDropDownList(string clubId)
    {
        lblResultMessage.Visible = false;
        //***************
        // Uses TODO 09 *
        //***************
        DataTable dtEvents = myFanClubDB.GetEventsIdAndName(clubId);

        // Populate the web page with the event information if the result is not null.
        if (dtEvents != null)
        {
            if (dtEvents.Columns.Contains("EVENTID") & dtEvents.Columns.Contains("EVENTNAME"))
            {
                ddlEvents.DataSource = dtEvents;
                ddlEvents.DataValueField = "EVENTID";
                ddlEvents.DataTextField = "EVENTNAME";
                ddlEvents.DataBind();
                if (dtEvents.Rows.Count == 0)
                {
                    myHelpers.ShowMessage(lblResultMessage, "There are no events for - " + ddlFanClubs.SelectedItem.Text + ".");
                    pnlEventNames.Visible = false;
                }
                else
                {
                    pnlEventNames.Visible = true;
                }
            }
            else // An SQL error occurred.
            {
                myHelpers.ShowMessage(lblResultMessage, "*** The SELECT statement does not retrieve the event ids and/or names.");
            }
        }
        else // An SQL error occurred.
        {
            myHelpers.ShowMessage(lblResultMessage, "*** There is an error in the SELECT statement that retrieves the event ids and names.");
        }
        ddlEvents.Items.Insert(0, "-- Select --");
        ddlEvents.Items.FindByText("-- Select --").Value = "none selected";
        ddlEvents.SelectedIndex = 0;
    }

    protected void ddlEvents_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblResultMessage.Visible = false;
        //***************
        // Uses TODO 14 *
        //***************
        DataTable dtJoinsEvent = myFanClubDB.GetJoinsEvent(ddlEvents.SelectedValue);

        // Show the joins event information if the query result is not null.
        if (dtJoinsEvent != null)
        {
            if (dtJoinsEvent.Columns.IndexOf("EVENTID") == 0 & dtJoinsEvent.Columns.IndexOf("USERNAME") == 1 &
                 dtJoinsEvent.Columns.IndexOf("PAIDFEE") == 2 & dtJoinsEvent.Columns.IndexOf("ATTENDED") == 3)
            {
                if (dtJoinsEvent.Rows.Count != 0)
                {
                    gvJoinsEvent.DataSource = dtJoinsEvent;
                    gvJoinsEvent.DataBind();
                    pnlJoinsEvent.Visible = true;
                }
                else
                {
                    myHelpers.ShowMessage(lblResultMessage, "No one has joined this event.");
                }
            }
            else // The eventId, userName, paidFee and attended values either are not all retrieved or are not in the correct order.
            {
                myHelpers.ShowMessage(lblResultMessage, "*** The SELECT statement either does not retrieve the correct attribute values and/or they are not in the correct order.");
            }
        }
        else // An SQL error occurred.
        {
            myHelpers.ShowMessage(lblResultMessage, "*** There is an error in the SELECT statement that retrieves all the information about who joined an event.");
        }
    }

    protected void ddlFanClubs_SelectedIndexChanged(object sender, EventArgs e)
    {
        string clubId = ddlFanClubs.SelectedValue;
        PopulateEventDropDownList(clubId);
        pnlJoinsEvent.Visible = false;
    }

    protected void gvJoinsEvent_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        // Format the gridview headers and data.
        if (e.Row.Cells.Count == 6)
        {
            DropDownList ddlPaidFee = (DropDownList)e.Row.FindControl("ddlPaidFee");
            DropDownList ddlAttended = (DropDownList)e.Row.FindControl("ddlAttended");

            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Controls[2].Visible = false;
                e.Row.Cells[3].Text = "User Name";
                e.Row.Controls[4].Visible = false;
                e.Row.Controls[5].Visible = false;
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Controls[2].Visible = false;
                e.Row.Controls[4].Visible = false;
                e.Row.Controls[5].Visible = false;
                if (e.Row.Cells[4].Text != "&nbsp;" & e.Row.Cells[4].Text.Length == 1)
                {
                    ddlPaidFee.SelectedValue = e.Row.Cells[4].Text;
                }
                if (e.Row.Cells[5].Text != "&nbsp;" & e.Row.Cells[5].Text.Length == 1)
                {
                    ddlAttended.SelectedValue = e.Row.Cells[5].Text;
                }
            }
        }
    }

}