using System;
using System.Data;
using System.Web;
using System.Web.UI;

public partial class ClubMember_ViewFanClubRemarks : System.Web.UI.Page
{
    FanClubDB myFanClubDB = new FanClubDB();
    Helpers myHelpers = new Helpers();
    string userName = HttpContext.Current.User.Identity.Name;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            lblResultMessage.Visible = false;
            pnlFanClub.Visible = false;
            pnlSubject.Visible = false;
            pnlRemark.Visible = false;
            PopulateFanClubDropDownList();
        }
    }

    protected void PopulateFanClubDropDownList()
    {
        //***************
        // Uses TODO 33 *
        //***************
        DataTable dtFanClubs = myFanClubDB.GetFanClubsWithRemarksFromUserName(userName);

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
                    pnlFanClub.Visible = true;
                }
                else
                {
                    myHelpers.ShowMessage(lblResultMessage, "You have not submitted a remark for any fan club.");
                }
            }
            else
            {
                myHelpers.ShowMessage(lblResultMessage, "*** The SQL statement that retrieves the fan clubs does not return the club id and/or the club name");
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

        if (Page.IsValid)
        {
            string clubId = ddlFanClubs.SelectedItem.Value;

            //***************
            // Uses TODO 34 *
            //***************
            DataTable dtFanClubRemarks = myFanClubDB.GetFanClubRemarksForUserName(userName, clubId);

            // Show the subjects in a dropdown list if the query result is not null and contains the required data.
            if (dtFanClubRemarks != null)
            {
                if (dtFanClubRemarks.Columns["SUBJECT"] != null)
                {
                    if (dtFanClubRemarks.Rows.Count != 0)
                    {
                        ddlSubject.DataSource = dtFanClubRemarks;
                        ddlSubject.DataValueField = "";
                        ddlSubject.DataTextField = "SUBJECT";
                        ddlSubject.DataBind();
                        ViewState["dtFanClubRemarks"] = dtFanClubRemarks;
                    }
                    else
                    {
                        myHelpers.ShowMessage(lblResultMessage, "*** Internal system error - no remarks for the selected fan club. Please contact 3311 Rep.");
                    }
                }
                else
                {
                    myHelpers.ShowMessage(lblResultMessage, "*** The SQL statement does not retrieve the remark subject.");
                }
            }
            else
            {
                myHelpers.ShowMessage(lblResultMessage, "*** There is an error in the SQL statement that retrieves the remarks for the selected fan club.");
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
            DataTable dtFanClubRemarks = (DataTable)ViewState["dtFanClubRemarks"];
            // Show the fan club remark.
            int selectedRemark = ddlSubject.SelectedIndex - 1;
            lblSubmissionDateText.Text = ((DateTime)(dtFanClubRemarks.Rows[selectedRemark]["SUBMISSIONDATE"])).ToString("dd-MMM-yyyy");
            lblActionTakenText.Text = dtFanClubRemarks.Rows[selectedRemark]["ACTIONTAKEN"].ToString();
            lblRemarkText.Text = dtFanClubRemarks.Rows[selectedRemark]["TEXT"].ToString();
            pnlRemark.Visible = true;
        }
        else
        {
            pnlRemark.Visible = false;
        }
    }

}