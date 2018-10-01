using System;
using System.Data;
using System.Web;
using System.Web.UI;

public partial class ClubMember_ViewEventRemarks : System.Web.UI.Page
{
    FanClubDB myFanClubDB = new FanClubDB();
    Helpers myHelpers = new Helpers();
    string userName = HttpContext.Current.User.Identity.Name;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            lblResultMessage.Visible = false;
            pnlEvent.Visible = false;
            pnlSubject.Visible = false;
            pnlRemark.Visible = false;
            PopulateEventDropDownList();
        }
    }

    protected void PopulateEventDropDownList()
    {
        //***************
        // Uses TODO 29 *
        //***************
        DataTable dtEvents = myFanClubDB.GetEventsWithRemarksFromUserName(userName);

        // Show the events in a dropdown list if the query result is not null and contains the required data.
        if (dtEvents != null)
        {
            if (dtEvents.Columns["EVENTID"] != null & dtEvents.Columns["EVENTNAME"] != null)
            {
                if (dtEvents.Rows.Count != 0)
                {
                    ddlEvents.DataSource = dtEvents;
                    ddlEvents.DataValueField = "EVENTID";
                    ddlEvents.DataTextField = "EVENTNAME";
                    ddlEvents.DataBind();
                    pnlEvent.Visible = true;
                }
                else
                {
                    myHelpers.ShowMessage(lblResultMessage, "You have not submitted a remark for any event.");
                }
            }
            else
            {
                myHelpers.ShowMessage(lblResultMessage, "*** The SQL statement that retrieves the events does not return the event id and/or the event name");
            }
        }
        else
        {
            myHelpers.ShowMessage(lblResultMessage, "*** There is an error in the SQL statement that retrieves the events.");
        }
        ddlEvents.Items.Insert(0, "-- Select --");
        ddlEvents.Items.FindByText("-- Select --").Value = "none selected";
        ddlEvents.SelectedIndex = 0;
    }

    protected void ddlEvents_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlSubject.Visible = false;
        pnlRemark.Visible = false;

        if (Page.IsValid)
        {
            string eventId = ddlEvents.SelectedItem.Value;

            //***************
            // Uses TODO 30 *
            //***************
            DataTable dtEventRemarks = myFanClubDB.GetEventRemarksForUserName(userName, eventId);

            // Show the subjects in a dropdown list if the query result is not null and contains the required data.
            if (dtEventRemarks != null)
            {
                if (dtEventRemarks.Columns["SUBJECT"] != null)
                {
                    if (dtEventRemarks.Rows.Count != 0)
                    {
                        ddlSubject.DataSource = dtEventRemarks;
                        ddlSubject.DataValueField = "";
                        ddlSubject.DataTextField = "SUBJECT";
                        ddlSubject.DataBind();
                        ViewState["dtEventRemarks"] = dtEventRemarks;
                    }
                    else
                    {
                        myHelpers.ShowMessage(lblResultMessage, "*** Internal system error - no remarks for the selected event. Please contact 3311 Rep.");
                    }
                }
                else
                {
                    myHelpers.ShowMessage(lblResultMessage, "*** The SQL statement does not retrieve the remark subject.");
                }
            }
            else
            {
                myHelpers.ShowMessage(lblResultMessage, "*** There is an error in the SQL statement that retrieves the remarks for the selected event.");
            }
            ddlSubject.Items.Insert(0, "-- Select --");
            ddlSubject.Items.FindByText("-- Select --").Value = "none selected";
            ddlSubject.SelectedIndex = 0;
            pnlSubject.Visible = true;
        }
    }

    protected void ddlSubject_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            DataTable dtEventRemarks = (DataTable)ViewState["dtEventRemarks"];
            // Show the event remark.
            int selectedRemark = ddlSubject.SelectedIndex - 1;
            lblSubmissionDateText.Text = ((DateTime)(dtEventRemarks.Rows[selectedRemark]["SUBMISSIONDATE"])).ToString("dd-MMM-yyyy");
            lblActionTakenText.Text = dtEventRemarks.Rows[selectedRemark]["ACTIONTAKEN"].ToString();
            lblRemarkText.Text = dtEventRemarks.Rows[selectedRemark]["TEXT"].ToString();
            pnlRemark.Visible = true;
        }
        else
        {
            pnlRemark.Visible = false;
        }
    }

}