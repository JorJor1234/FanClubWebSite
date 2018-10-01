using System;
using System.Data;
using System.Web;
using System.Web.UI;

public partial class Employee_ProcessEventRemarks : System.Web.UI.Page
{
    FanClubDB myFanClubDB = new FanClubDB();
    Helpers myHelpers = new Helpers();
    private string userName = HttpContext.Current.User.Identity.Name;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            lblResultMessage.Visible = false;
            pnlEvents.Visible = false;
            pnlSubject.Visible = false;
            pnlRemark.Visible = false;
            ViewState["employeeId"] = GetEmployeeId(userName);
            if (ViewState["employeeId"].ToString() == "")
            {
                return;
            }
            else
            {
                PopulateEventDropDownList();
            }
        }
    }

    protected void PopulateEventDropDownList()
    {
        //***************
        // Uses TODO 27 *
        //***************
        DataTable dtEvents = myFanClubDB.GetEventIdAndNameWithRemarksForEmployee(ViewState["employeeId"].ToString());

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
                    pnlEvents.Visible = true;
                }
                else
                {
                    myHelpers.ShowMessage(lblResultMessage, "All event remarks have been assigned for processing.");
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
        ddlSubject.Items.Clear();
        string eventId = ddlEvents.SelectedItem.Value;

        //***************
        // Uses TODO 28 *
        //***************
        DataTable dtEventRemarks = myFanClubDB.GetAvailableEventRemarksForProcessing(eventId, ViewState["employeeId"].ToString());

        // Show the subjects in a dropdown list if the query result is not null and contains the required data.
        if (dtEventRemarks != null)
        {
            if (dtEventRemarks.Columns["REMARKID"] != null & dtEventRemarks.Columns["SUBJECT"] != null)
            {
                if (dtEventRemarks.Rows.Count != 0)
                {
                    ddlSubject.DataSource = dtEventRemarks;
                    ddlSubject.DataValueField = "REMARKID";
                    ddlSubject.DataTextField = "SUBJECT";
                    ddlSubject.DataBind();
                    ViewState["dtEventRemarks"] = dtEventRemarks;
                    pnlSubject.Visible = true;
                }
                else
                {
                    myHelpers.ShowMessage(lblResultMessage, "*** Internal system error - no remarks for the selected event. Please contact 3311 Rep.");
                }
            }
            else
            {
                myHelpers.ShowMessage(lblResultMessage, "*** The SQL statement does not retrieve the remark id and/or subject.");
            }
        }
        else
        {
            myHelpers.ShowMessage(lblResultMessage, "*** There is an error in the SQL statement that retrieves the remarks for the selected event.");
        }
        ddlSubject.Items.Insert(0, "-- Select --");
        ddlSubject.Items.FindByText("-- Select --").Value = "none selected";
        ddlSubject.SelectedIndex = 0;
    }

    protected void ddlSubject_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblResultMessage.Visible = false;

        DataTable dtEventRemarks = (DataTable)ViewState["dtEventRemarks"];
        // Show the event remark.
        int selectedRemark = ddlSubject.SelectedIndex - 1;
        string remarkId = dtEventRemarks.Rows[selectedRemark]["REMARKID"].ToString().Trim();
        lblUserNameText.Text = dtEventRemarks.Rows[selectedRemark]["USERNAME"].ToString().Trim();
        lblSubmissionDateText.Text = ((DateTime)(dtEventRemarks.Rows[selectedRemark]["SUBMISSIONDATE"])).ToString("dd-MMM-yyyy");
        txtActionTaken.Text = dtEventRemarks.Rows[selectedRemark]["ACTIONTAKEN"].ToString().Trim();
        lblRemarkText.Text = dtEventRemarks.Rows[selectedRemark]["TEXT"].ToString().Trim();
        string status = dtEventRemarks.Rows[selectedRemark]["STATUS"].ToString().Trim();
        if (status == "")
        {
            ddlStatus.SelectedValue = "read";
            //***************
            // Uses TODO 36 *
            //***************
            if (!myFanClubDB.UpdateRemark(remarkId, "read", txtActionTaken.Text, ViewState["employeeId"].ToString()))
            {
                pnlRemark.Visible = false;
                myHelpers.ShowMessage(lblResultMessage, "*** There is an error in the UPDATE statement that modifies a remark.");
            }
            else
            {
                dtEventRemarks.Rows[selectedRemark]["STATUS"] = "read";
                ViewState["dtEventRemarks"] = dtEventRemarks;
                pnlRemark.Visible = true;
            }
        }
        else
        {
            ddlStatus.SelectedValue = status;
            pnlRemark.Visible = true;
        }
    }

    protected void btnUpdateRemark_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            lblResultMessage.Visible = false;

            // Collect the updated remark information.
            string remarkId = ddlSubject.SelectedValue;
            string status = ddlStatus.SelectedItem.Value;
            string actionTaken = myHelpers.CleanInput(txtActionTaken.Text.Trim());

            if (RemarkIsChanged(status, actionTaken))
            {
                //***************
                // Uses TODO 36 *
                //***************
                if (myFanClubDB.UpdateRemark(remarkId, status, actionTaken, ViewState["employeeId"].ToString()))
                {
                    pnlEvents.Visible = false;
                    pnlSubject.Visible = false;
                    pnlRemark.Visible = false;
                    myHelpers.ShowMessage(lblResultMessage, "The remark has been updated.");
                }
                else
                {
                    myHelpers.ShowMessage(lblResultMessage, "*** There is an error in the UPDATE statement that modifies a remark.");
                }
            }
            else
            {
                myHelpers.ShowMessage(lblResultMessage, "You have not changed any remark information.");
            }
        }
    }

    private string GetEmployeeId(string userName)
    {
        //***************
        // Uses TODO 01 *
        //***************
        DataTable dtEmployeId = myFanClubDB.GetEmployeeId(userName);

        // Show the event information if the query result is not null.
        if (dtEmployeId != null)
        {
            if (dtEmployeId.Rows.Count == 1)
            {
                return dtEmployeId.Rows[0]["EMPLOYEEID"].ToString();
            }
        }
        // An SQL error occurred.
        myHelpers.ShowMessage(lblResultMessage, "*** There is an error in the SELECT statement that retrieves the employee id.");
        return "";
    }

    private bool RemarkIsChanged(string newStatus, string newActionTaken)
    {
        // Get previous values for status and action taken.
        int selectedRemark = ddlSubject.SelectedIndex - 1;
        DataTable dtEventRemarks = (DataTable)ViewState["dtEventRemarks"];
        string actionTaken = dtEventRemarks.Rows[selectedRemark]["ACTIONTAKEN"].ToString().Trim();
        string status = dtEventRemarks.Rows[selectedRemark]["STATUS"].ToString().Trim();

        if (status == newStatus & actionTaken == newActionTaken)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}