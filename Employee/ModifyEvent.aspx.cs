using System;
using System.Data;

public partial class Employee_ModifyEvent : System.Web.UI.Page
{
    FanClubDB myFanClubDB = new FanClubDB();
    Helpers myHelpers = new Helpers();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PopulateFanClubDropDownList();
            pnlEventNames.Visible = false;
            pnlEventInformation.Visible = false;
        }
    }

    protected void btnUpdateEvent_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            lblResultMessage.Visible = false;
            // Collect the updated event information.

            string inputEventName = ViewState["currentEventName"].ToString();
            string eventId = ddlEvents.SelectedValue;
            string eventName = myHelpers.CleanInput(txtEventName.Text.Trim());
            string eventDate = Convert.ToDateTime(txtEventDate.Text.Trim()).ToString("dd-MMM-yyyy");
            string eventTime = txtEventTime.Text.Trim();
            string venue = myHelpers.CleanInput(txtVenue.Text.Trim());
            string memberFee = txtMemberFee.Text.Trim() == "" ? "0" : txtMemberFee.Text.Trim();
            string nonmemberFee = txtNonmemberFee.Text.Trim() == "" ? "0" : txtNonmemberFee.Text.Trim();
            string eventQuota = txtEventQuota.Text.Trim();
            string isAvailable = ddlIsAvailable.SelectedValue;
            string isMemberOnly = ddlIsMemberOnly.SelectedValue;
            string employeeId = ddlEmployee.SelectedValue;

            if (EventIsChanged(eventName, eventDate, eventTime, venue, memberFee, nonmemberFee, eventQuota, isAvailable, isMemberOnly, employeeId))
            {
                //***************
                // Uses TODO 19 *
                //***************
                if (myFanClubDB.UpdateEvent(eventId, eventName, eventDate, eventTime, venue, memberFee, nonmemberFee, eventQuota, isAvailable, isMemberOnly, employeeId))
                {
                    pnlEventNames.Visible = false;
                    pnlEventInformation.Visible = false;
                    ddlFanClubs.SelectedIndex = 0;
                    myHelpers.ShowMessage(lblResultMessage, "The event - " + inputEventName + " - has been updated.");
                }
                else
                {
                    myHelpers.ShowMessage(lblResultMessage, "*** There is an error in the UPDATE statement that modifies the event information.");
                }
            }
            else
            {
                myHelpers.ShowMessage(lblResultMessage, "You have not changed any event information.");
            }
        }
    }

    protected void PopulateFanClubDropDownList()
    {
        DataTable dtFanClubs = myFanClubDB.GetFanClubs();

        // Populate the dropdown list with the fan club ids and names if the result is not null and contains the required data.
        if (dtFanClubs != null)
        {
            if (dtFanClubs.Columns["CLUBID"] != null & dtFanClubs.Columns["CLUBNAME"] != null)
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
                myHelpers.ShowMessage(lblResultMessage, "*** The SELECT statement does not retrieve the fan club ids and/or names.");
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
        // Uses TODO 15 *
        //***************
        DataTable dtEvents = myFanClubDB.GetModifiableEventsIdAndName(clubId);

        // Populate the web page with the event information if the result is not null.
        if (dtEvents != null)
        {
            if (dtEvents.Columns["EVENTID"] != null & dtEvents.Columns["EVENTNAME"] != null)
            {
                ddlEvents.DataSource = dtEvents;
                ddlEvents.DataValueField = "EVENTID";
                ddlEvents.DataTextField = "EVENTNAME";
                ddlEvents.DataBind();
                if (dtEvents.Rows.Count == 0)
                {
                    myHelpers.ShowMessage(lblResultMessage, "There are no events that can be updated for - " + ddlFanClubs.SelectedItem.Text + ".");
                    pnlEventNames.Visible = false;
                }
                else
                {
                    pnlEventNames.Visible = true;
                }
            }
            else // An SQL error occurred.
            {
                myHelpers.ShowMessage(lblResultMessage, "*** The SELECT statement does not retrieve the event ids and/or and names.");
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

    protected void PopulateEmployeeDropDownList(string employeeId)
    {
        //***************
        // Uses TODO 02 *
        //***************
        DataTable dtEmployees = myFanClubDB.GetEmployees();

        // Populate the dropdown list with the available employees if the DataTable is not null.
        if (dtEmployees != null)
        {
            if (dtEmployees.Columns["EMPLOYEEID"] != null & dtEmployees.Columns["EMPLOYEENAME"] != null)
            {
                if (dtEmployees.Rows.Count != 0)
                {
                    ddlEmployee.DataSource = dtEmployees;
                    ddlEmployee.DataValueField = "EMPLOYEEID";
                    ddlEmployee.DataTextField = "EMPLOYEENAME";
                    ddlEmployee.DataBind();
                    ddlEmployee.SelectedValue = employeeId;
                }
                else
                {
                    myHelpers.ShowMessage(lblResultMessage, "There are no employees.");
                }
            }
            else // An SQL error occurred.
            {
                myHelpers.ShowMessage(lblResultMessage, "*** The SELECT statement does not retrieve the employee id and/or name.");
            }
        }
        else // An SQL error occurred.
        {
            myHelpers.ShowMessage(lblResultMessage, "*** There is an error in the SELECT statement that retrieves all the employees.");
        }
        ddlEmployee.Items.Insert(0, "-- Select --");
        ddlEmployee.Items.FindByText("-- Select --").Value = "none selected";
    }

    protected void PopulateEventInformation(DataTable dtEvent)
    {
        // Populate the web page with the event informatin and save it in ViewState for update validation.
        ViewState["currentEventName"] = txtEventName.Text = ddlEvents.SelectedItem.Text;
        ViewState["currentVenue"] = txtVenue.Text = dtEvent.Rows[0]["VENUE"].ToString();
        ViewState["currentEventQuota"] = txtEventQuota.Text = dtEvent.Rows[0]["EVENTQUOTA"].ToString();
        ViewState["currentIsAvailable"] = ddlIsAvailable.SelectedValue = dtEvent.Rows[0]["ISAVAILABLE"].ToString();
        ViewState["currentIsMemberOnly"] = ddlIsMemberOnly.SelectedValue = dtEvent.Rows[0]["ISMEMBERONLY"].ToString();
        ViewState["currentMemberFee"] = txtMemberFee.Text = dtEvent.Rows[0]["MEMBERFEE"].ToString();
        ViewState["currentNonmemberFee"] = txtNonmemberFee.Text = dtEvent.Rows[0]["NONMEMBERFEE"].ToString();
        ViewState["currentEventTime"] = txtEventTime.Text = dtEvent.Rows[0]["EVENTTIME"].ToString();
        ViewState["currentEventDate"] = txtEventDate.Text = (Convert.ToDateTime(dtEvent.Rows[0]["EVENTDATE"])).ToString("dd-MMM-yyyy");
        ViewState["currentEmployeeId"] = dtEvent.Rows[0]["EMPLOYEEID"].ToString();
        PopulateEmployeeDropDownList(dtEvent.Rows[0]["EMPLOYEEID"].ToString());
        if (txtNonmemberFee.Text == "0")
        {
            txtNonmemberFee.ReadOnly = true;
        }
    }

    protected void ddlEvents_SelectedIndexChanged(object sender, EventArgs e)
    {
        //***************
        // Uses TODO 08 *
        //***************
        DataTable dtEvent = myFanClubDB.GetEventInformation(ddlEvents.SelectedValue);

        // Show the event information if the query result is not null.
        if (dtEvent != null)
        {
            if (dtEvent.Rows.Count != 0)
            {
                PopulateEventInformation(dtEvent);
                pnlEventInformation.Visible = true;
            }
            else
            {
                myHelpers.ShowMessage(lblResultMessage, "*** Internal system error retrieving event information using Event dropdown list. Please contact 3311 Rep.");
            }
        }
        else // An SQL error occurred.
        {
            myHelpers.ShowMessage(lblResultMessage, "*** There is an error in the SELECT statement that retrieves event information.");
        }
    }

    protected void ddlFanClubs_SelectedIndexChanged(object sender, EventArgs e)
    {
        string clubId = ddlFanClubs.SelectedValue;
        PopulateEventDropDownList(clubId);
        pnlEventInformation.Visible = false;
    }

    protected void btnCalendar_Click(object sender, EventArgs e)
    {
        if (calEventDate.Visible == false)
        {
            calEventDate.Visible = true;
            btnCalendar.Text = "Hide Calendar";
        }
        else
        {
            calEventDate.Visible = false;
            btnCalendar.Text = "Show Calendar";
        }
    }

    protected void calEventDate_SelectionChanged(object sender, EventArgs e)
    {
        txtEventDate.Text = calEventDate.SelectedDate.ToString("dd-MMM-yyyy");
        calEventDate.Visible = false;
        btnCalendar.Text = "Show Calendar";
    }

    protected void ddlIsMemberOnly_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlIsMemberOnly.SelectedValue == "Y")
        {
            txtNonmemberFee.Text = "0";
            txtNonmemberFee.ReadOnly = true;
        }
        else
        {
            txtNonmemberFee.ReadOnly = false;
        }
    }

    protected void cvEventDate_ServerValidate(object source, System.Web.UI.WebControls.ServerValidateEventArgs args)
    {
        if (!myHelpers.DateIsValid(txtEventDate.Text))
        {
            args.IsValid = false;
        }
    }

    private bool EventIsChanged(string newEventName, string newEventDate, string newEventTime, string newVenue,
        string newMemberFee, string newNonmemberFee, string newEventQuota, string newIsAvailable, string newIsMemberOnly,
        string newEmployeeId)
    {
        if (ViewState["currentEventName"].ToString() == newEventName & ViewState["currentEventDate"].ToString() == newEventDate &
             ViewState["currentEventTime"].ToString() == newEventTime & ViewState["currentVenue"].ToString() == newVenue &
             ViewState["currentMemberFee"].ToString() == newMemberFee & ViewState["currentNonmemberFee"].ToString() == newNonmemberFee &
             ViewState["currentEventQuota"].ToString() == newEventQuota & ViewState["currentIsAvailable"].ToString() == newIsAvailable &
             ViewState["currentIsMemberOnly"].ToString() == newIsMemberOnly & ViewState["currentEmployeeId"].ToString() == newEmployeeId)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}