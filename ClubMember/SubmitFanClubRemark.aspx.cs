using System;
using System.Data;
using System.Web;
using System.Web.UI;

public partial class ClubMember_SubmitFanClubRemark : System.Web.UI.Page
{
    FanClubDB myFanClubDB = new FanClubDB();
    Helpers myHelpers = new Helpers();
    string userName = HttpContext.Current.User.Identity.Name;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            lblResultMessage.Visible = false;
            PopulateFanClubDropDownList();
        }
    }

    protected void PopulateFanClubDropDownList()
    {
        //***************
        // Uses TODO 22 *
        //***************

        // Retrieve the fan clubs.
        DataTable dtFanClubs = myFanClubDB.GetFanClubsJoined(userName);

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
                }
                else
                {
                    myHelpers.ShowMessage(lblResultMessage, "*** Internal system error - no fan clubs joined. Please contact 3311 Rep.");
                }
            }
            else // An SQL error occurred.
            {
                myHelpers.ShowMessage(lblResultMessage, "*** The SQL statement does not retrieve the fan club id and/or name.");
            }
        }
        else // An SQL error occurred.
        {
            myHelpers.ShowMessage(lblResultMessage, "*** There is an error in the SQL statement that retrieves the fan clubs.");
        }
        ddlFanClubs.Items.Insert(0, "-- Select --");
        ddlFanClubs.Items.FindByText("-- Select --").Value = "none selected";
        ddlFanClubs.SelectedIndex = 0;
    }

    protected void btnSubmitRemark_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            string remarkId = myHelpers.GetNextTableId("Remark", "remarkId").ToString();
            // Collect the required remark information.
            string clubId = ddlFanClubs.SelectedValue.ToString();
            string subject = myHelpers.CleanInput(txtSubject.Text.Trim());
            string text = myHelpers.CleanInput(txtRemark.Text.Trim());
            string submissionDate = DateTime.Now.ToString("dd-MMM-yyyy");

            //***************
            // Uses TODO 35 *
            //***************
            if (myFanClubDB.SubmitRemark(remarkId, subject, text, submissionDate, "", "", "club", userName, "null", clubId, "null"))
            {
                myHelpers.ShowMessage(lblResultMessage, "Your remark for the fan club - " + ddlFanClubs.SelectedItem.Text + " - has been submitted.");
                ResetPage();
            }
            else
            {
                myHelpers.ShowMessage(lblResultMessage, "*** There is an error in the INSERT statement for submitting a remark.");
            }
        }
    }

    private void ResetPage()
    {
        ddlFanClubs.SelectedIndex = 0;
        txtSubject.Text = string.Empty;
        txtRemark.Text = string.Empty;
    }
}