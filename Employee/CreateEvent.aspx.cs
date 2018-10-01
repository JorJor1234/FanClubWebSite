using System;
using System.Data;
using System.Web.UI.WebControls;

public partial class Employee_CreateEvent : System.Web.UI.Page
{
    FanClubDB myFanClubDB = new FanClubDB();
    Helpers myHelpers = new Helpers();
    private DataTable dtEventFanClubs = new DataTable();
    private DataTable dtFanClubs = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            // Create a DataTable to store the selected fan clubs holding the event.
            dtEventFanClubs.Columns.Add("CLUBID");
            dtEventFanClubs.Columns.Add("Selected Fan Clubs");

            PopulateFanClubDropDownList();
            PopulateEmployeeDropDownList();
        }
        else
        {
            dtEventFanClubs = (DataTable)ViewState["EventFanClubsDataTable"];
        }
        // The event fan clubs DataTable is stored in ViewState so it can be recreated when the page is loaded.
        ViewState["EventFanClubsDataTable"] = dtEventFanClubs;
    }

    protected void PopulateFanClubDropDownList()
    {
        if (ViewState["FanClubsDropDownList"] == null)
        {
            dtFanClubs = myFanClubDB.GetFanClubs();
        }
        else
        {
            dtFanClubs = (DataTable)ViewState["FanClubsDropDownList"];
        }

        // Populate the dropdown list with the available fan clubs if the query result is not null and contains the required data.
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
                    ViewState["FanClubsDropDownList"] = dtFanClubs;
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
            myHelpers.ShowMessage(lblResultMessage, "*** There is an error in the SELECT statement that retrieves all the fan clubs.");
        }
        ddlFanClubs.Items.Insert(0, "-- Select --");
        ddlFanClubs.Items.FindByText("-- Select --").Value = "none selected";
        ddlFanClubs.SelectedIndex = 0;
    }

    protected void PopulateEmployeeDropDownList()
    {
        //***************
        // Uses TODO 02 *
        //***************
        DataTable dtEmployees = myFanClubDB.GetEmployees();

        // Populate the dropdown list with the available employees if the query result is not null and contains the required data.
        if (dtEmployees != null & dtEmployees.Columns["EMPLOYEEID"] != null & dtEmployees.Columns["EMPLOYEENAME"] != null)
        {
            if (dtEmployees.Rows.Count != 0)
            {
                ddlEmployee.DataSource = dtEmployees;
                ddlEmployee.DataValueField = "EMPLOYEEID";
                ddlEmployee.DataTextField = "EMPLOYEENAME";
                ddlEmployee.DataBind();
            }
            else
            {
                myHelpers.ShowMessage(lblResultMessage, "There are no employees.");
            }
        }
        else // An SQL error occurred.
        {
            myHelpers.ShowMessage(lblResultMessage, "*** There is an error in the SELECT statement that retrieves all the employees.");
        }
        ddlEmployee.Items.Insert(0, "-- Select --");
        ddlEmployee.Items.FindByText("-- Select --").Value = "none selected";
        ddlEmployee.SelectedIndex = 0;
    }

    protected void btnCreateEvent_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            lblResultMessage.Visible = false;
            // Collect the event information.
            string employeeId = ddlEmployee.SelectedValue;
            string inputEventName = txtEventName.Text.Trim();
            string eventName = myHelpers.CleanInput(inputEventName);
            string venue = myHelpers.CleanInput(txtVenue.Text.Trim());
            string eventQuota = txtEventQuota.Text.Trim();
            string isAvailable = ddlIsAvailable.SelectedValue;
            string isMemberOnly = ddlIsMemberOnly.SelectedValue;
            string memberFee = txtMemberFee.Text.Trim() == "" ? "0" : txtMemberFee.Text.Trim();
            string nonmemberFee = txtNonmemberFee.Text.Trim() == "" ? "0" : txtNonmemberFee.Text.Trim();
            string eventTime = txtEventTime.Text.Trim();
            string eventDate = txtEventDate.Text.Trim();
            string eventId = myHelpers.GetNextTableId("Event", "eventId").ToString();

            if (eventId != "0")
            {
                //*********************
                // Uses TODOs 17 & 18 *
                //*********************
                if (myFanClubDB.CreateEvent(dtEventFanClubs, eventId, eventName, eventDate, eventTime, 
                    venue, memberFee, nonmemberFee, eventQuota, isAvailable, isMemberOnly, employeeId))
                {
                    ResetInputForm();
                    myHelpers.ShowMessage(lblResultMessage, "The event - " + inputEventName + " - has been created.");
                }
                else // An SQL error occurred.
                {
                    myHelpers.ShowMessage(lblResultMessage, "*** There is an error in the INSERT statement that creates an event.");
                }
            }
            else // An SQL error occurred.
            {
                myHelpers.ShowMessage(lblResultMessage, "*** There is an error in the SELECT statement that retrieves the maximum event id.");
            }
        }
    }

    protected void btnAddFanClub_Click(object sender, EventArgs e)
    {
        // Add the selected fan club to the list of fan clubs.
        DataRow dr = dtEventFanClubs.NewRow();
        dr["CLUBID"] = ddlFanClubs.SelectedItem.Value;
        dr["Selected Fan Clubs"] = ddlFanClubs.SelectedItem.Text;
        dtEventFanClubs.Rows.Add(dr);
        gvEventFanClubs.DataSource = dtEventFanClubs;
        gvEventFanClubs.DataBind();

        // Remove the selected fan club from the dropdown list.
        ddlFanClubs.Items.Remove(ddlFanClubs.SelectedItem);
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
            txtNonmemberFee.ReadOnly = true;
        }
        else
        {
            txtNonmemberFee.ReadOnly = false;
        }
    }

    protected void gvEventFanClubs_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Controls[0].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Controls[0].Visible = false;
        }
    }

    protected void cvFansClubs_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (gvEventFanClubs.Rows.Count == 0)
        {
            args.IsValid = false;
        }
    }

    protected void cvEventDate_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (!myHelpers.DateIsValid(txtEventDate.Text))
        {
            args.IsValid = false;
        }
    }

    private void ResetInputForm()
    {
        lblResultMessage.Visible = false;
        ddlFanClubs.SelectedIndex = 0;
        ddlEmployee.SelectedIndex = 0;
        txtEventName.Text = "";
        txtVenue.Text = "";
        txtEventQuota.Text = "0";
        ddlIsAvailable.SelectedIndex = 0;
        ddlIsMemberOnly.SelectedIndex = 0;
        txtMemberFee.Text = "0";
        txtNonmemberFee.Text = "0";
        txtNonmemberFee.ReadOnly = false;
        txtEventTime.Text = "0000";
        txtEventDate.Text = "dd-MMM-yyyy";
        calEventDate.Visible = false;
        btnCalendar.Text = "Show Calendar";
        gvEventFanClubs.DataSource = null;
        gvEventFanClubs.DataBind();
        dtEventFanClubs.Clear();
        ViewState["EventFanClubsDataTable"] = dtEventFanClubs;
        PopulateFanClubDropDownList();
    }
}