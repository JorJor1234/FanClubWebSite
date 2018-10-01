using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Employee_ProcessFanClubRemarks : System.Web.UI.Page
{
    FanClubDB myFanClubDB = new FanClubDB();
    Helpers myHelpers = new Helpers();
    private string userName = HttpContext.Current.User.Identity.Name;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            lblResultMessage.Visible = false;
            pnlFanClubs.Visible = false;
            pnlSubject.Visible = false;
            pnlRemark.Visible = false;
            ViewState["employeeId"] = GetEmployeeId(userName);
            if (ViewState["employeeId"].ToString() == "")
            {
                return;
            }
            else
            {
                PopulateFanClubDropDownList();
            }
        }
    }

    protected void PopulateFanClubDropDownList()
    {
        //***************
        // Uses TODO 31 *
        //***************
        DataTable dtFanClubs = myFanClubDB.GetFanClubIdAndNameWithRemarksForEmployee(ViewState["employeeId"].ToString());

        // Show the fan clubs in a dropdown list if the query result is not null and contains the required data.
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
                    pnlFanClubs.Visible = true;
                }
                else
                {
                    myHelpers.ShowMessage(lblResultMessage, "All fan club remarks have been assigned for processing.");
                }
            }
            else
            {
                myHelpers.ShowMessage(lblResultMessage, "*** The SQL statement that retrieves the fan clubs does not return the fan club id and/or the fan club name");
            }
        }
        else
        {
            myHelpers.ShowMessage(lblResultMessage, "*** There is an error in the SQL statement that retrieves the fan clubs.");
        }
        ddlFanClubs.Items.Insert(0, "-- Select --");
        ddlFanClubs.Items.FindByText("-- Select --").Value = "none selected";
        ddlFanClubs.SelectedIndex = 0;
    }

    protected void ddlFanClubs_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlSubject.Visible = false;
        pnlRemark.Visible = false;
        ddlSubject.Items.Clear();
        string clubId = ddlFanClubs.SelectedItem.Value;

        //***************
        // Uses TODO 32 *
        //***************
        DataTable dtFanClubRemarks = myFanClubDB.GetAvailableFanClubRemarksForProcessing(clubId, ViewState["employeeId"].ToString());

        // Show the subjects in a dropdown list if the query result is not null and contains the required data.
        if (dtFanClubRemarks != null)
        {
            if (dtFanClubRemarks.Columns["REMARKID"] != null & dtFanClubRemarks.Columns["SUBJECT"] != null)
            {
                if (dtFanClubRemarks.Rows.Count != 0)
                {
                    ddlSubject.DataSource = dtFanClubRemarks;
                    ddlSubject.DataValueField = "REMARKID";
                    ddlSubject.DataTextField = "SUBJECT";
                    ddlSubject.DataBind();
                    ViewState["dtFanClubRemarks"] = dtFanClubRemarks;
                    pnlSubject.Visible = true;
                }
                else
                {
                    myHelpers.ShowMessage(lblResultMessage, "*** Internal system error - no remarks for the selected fan club. Please contact 3311 Rep.");
                }
            }
            else
            {
                myHelpers.ShowMessage(lblResultMessage, "*** The SQL statement does not retrieve the remark id and/or subject.");
            }
        }
        else
        {
            myHelpers.ShowMessage(lblResultMessage, "*** There is an error in the SQL statement that retrieves the remarks for the selected fan club.");
        }
        ddlSubject.Items.Insert(0, "-- Select --");
        ddlSubject.Items.FindByText("-- Select --").Value = "none selected";
        ddlSubject.SelectedIndex = 0;
    }

    protected void ddlSubject_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblResultMessage.Visible = false;

        DataTable dtFanClubRemarks = (DataTable)ViewState["dtFanClubRemarks"];
        // Show the fan club remark.
        int selectedRemark = ddlSubject.SelectedIndex - 1;
        string remarkId = dtFanClubRemarks.Rows[selectedRemark]["REMARKID"].ToString().Trim();
        lblUserNameText.Text = dtFanClubRemarks.Rows[selectedRemark]["USERNAME"].ToString().Trim();
        lblSubmissionDateText.Text = ((DateTime)(dtFanClubRemarks.Rows[selectedRemark]["SUBMISSIONDATE"])).ToString("dd-MMM-yyyy");
        txtActionTaken.Text = dtFanClubRemarks.Rows[selectedRemark]["ACTIONTAKEN"].ToString().Trim();
        lblRemarkText.Text = dtFanClubRemarks.Rows[selectedRemark]["TEXT"].ToString().Trim();
        string status = dtFanClubRemarks.Rows[selectedRemark]["STATUS"].ToString().Trim();
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
                dtFanClubRemarks.Rows[selectedRemark]["STATUS"] = "read";
                ViewState["dtFanClubRemarks"] = dtFanClubRemarks;
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
                    pnlFanClubs.Visible = false;
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

        // Show the fan club information if the query result is not null.
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
        DataTable dtFanClubRemarks = (DataTable)ViewState["dtFanClubRemarks"];
        string actionTaken = dtFanClubRemarks.Rows[selectedRemark]["ACTIONTAKEN"].ToString().Trim();
        string status = dtFanClubRemarks.Rows[selectedRemark]["STATUS"].ToString().Trim();

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