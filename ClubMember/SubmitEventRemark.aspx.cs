using System;
using System.Data;
using System.Web;
using System.Web.UI;

public partial class ClubMember_SubmitEventRemark : System.Web.UI.Page
{
    FanClubDB myFanClubDB = new FanClubDB();
    Helpers myHelpers = new Helpers();
    string userName = HttpContext.Current.User.Identity.Name;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            PopulateEventDropDownList();
        }
    }

    protected void PopulateEventDropDownList()
    {
        //***************
        // Uses TODO 10 *
        //***************
        DataTable dtEventsJoined = myFanClubDB.GetJoinedEventsIdAndName(userName);

        // Populate the events joined dropdown list if the query result is not null and contains the required data.
        if (dtEventsJoined != null)
        {
            if (dtEventsJoined.Columns["EVENTID"] != null & dtEventsJoined.Columns["EVENTNAME"] != null)
            {
                if (dtEventsJoined.Rows.Count != 0)
                {
                    ddlEvents.DataSource = dtEventsJoined;
                    ddlEvents.DataValueField = "EVENTID";
                    ddlEvents.DataTextField = "EVENTNAME";
                    ddlEvents.DataBind();
                }
                else
                {
                    myHelpers.ShowMessage(lblResultMessage, "You have not joined any events.");
                }
            }
            else // An SQL error occurred.
            {
                myHelpers.ShowMessage(lblResultMessage, "*** The SELECT statement does not retrieve the event id and/or name.");
            }
        }
        else // An SQL error occurred.
        {
            myHelpers.ShowMessage(lblResultMessage, "*** There is an error in the SQL statement that retrieves the events.");
        }
        ddlEvents.Items.Insert(0, "-- Select --");
        ddlEvents.Items.FindByText("-- Select --").Value = "none selected";
        ddlEvents.SelectedIndex = 0;
    }

    protected void btnSubmitRemark_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            string remarkId = myHelpers.GetNextTableId("Remark", "remarkId").ToString();
            // Collect the required remark information.
            string eventId = ddlEvents.SelectedValue.ToString();
            string subject = myHelpers.CleanInput(txtSubject.Text.Trim());
            string text = myHelpers.CleanInput(txtRemark.Text.Trim());
            string submissionDate = DateTime.Now.ToString("dd-MMM-yyyy");

            //***************
            // Uses TODO 35 *
            //***************
            if (myFanClubDB.SubmitRemark(remarkId, subject, text, submissionDate, "", "", "event", userName, "null", "null", eventId))
            {
                myHelpers.ShowMessage(lblResultMessage, "Your remark for the event - " + ddlEvents.SelectedItem.Text + " - has been submitted.");
                ResetPage();
            }
            else  // An SQL error occurred.
            {
                myHelpers.ShowMessage(lblResultMessage, "*** There is an error in the INSERT statement for submitting a remark.");
            }
        }
    }

    private void ResetPage()
    {
        ddlEvents.SelectedIndex = 0;
        txtSubject.Text = string.Empty;
        txtRemark.Text = string.Empty;
    }
}