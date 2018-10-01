using System;
using System.Data;

public partial class Employee_ModifyFanClub : System.Web.UI.Page
{
    FanClubDB myFanClubDB = new FanClubDB();
    Helpers myHelpers = new Helpers();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PopulateFanClubDropDownList();
        }
    }

    protected void btnUpdateFanClub_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            // Collect the information required to update a fan club.
            string inputFanClubName = ViewState["currentClubName"].ToString();
            string fanClubId = ddlFanClubName.SelectedItem.Value;
            string fanClubName = myHelpers.CleanInput(txtFanClubName.Text.Trim());
            string description = myHelpers.CleanInput(txtDescription.Text.Trim());
            string dateEstablished = txtDateEstablished.Text.Trim();

            if (FanClubIsChanged(fanClubName, description, dateEstablished))
            {
                //***************
                // Uses TODO 26 *
                //***************
                if (myFanClubDB.UpdateFanClub(fanClubId, fanClubName, description, dateEstablished))
                {
                    pnlFanClubInfo.Visible = false;
                    PopulateFanClubDropDownList();
                    ddlFanClubName.SelectedIndex = 0;
                    myHelpers.ShowMessage(lblResultMessage, "The fan club - " + inputFanClubName + " - has been updated.");
                }
                else
                {
                    myHelpers.ShowMessage(lblResultMessage, "*** There is an error in the UPDATE statement that updates the fan club information.");
                }
            }
            else
            {
                myHelpers.ShowMessage(lblResultMessage, "You have not changed any fan club information.");
            }
        }
    }

    protected void PopulateFanClubDropDownList()
    {
        lblResultMessage.Visible = false;

        DataTable dtFanClubs = myFanClubDB.GetFanClubs();

        // Show the available fan clubs if the query result is not null and contains the required data.
        if (dtFanClubs != null)
        {
            if (dtFanClubs.Columns["CLUBID"] != null & dtFanClubs.Columns["CLUBNAME"] != null)
            {
                if (dtFanClubs.Rows.Count != 0)
                {
                    ddlFanClubName.DataSource = dtFanClubs;
                    ddlFanClubName.DataValueField = "CLUBID";
                    ddlFanClubName.DataTextField = "CLUBNAME";
                    ddlFanClubName.DataBind();
                    ViewState["FanClubs"] = dtFanClubs;
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
            myHelpers.ShowMessage(lblResultMessage, "*** There is an error in the SELECT statement that retrieves all data for all fan clubs.");
        }
        ddlFanClubName.Items.Insert(0, "-- Select --");
        ddlFanClubName.Items.FindByText("-- Select --").Value = "none selected";
        ddlFanClubName.SelectedIndex = 0;
    }

    protected void PopulateFanClubInformation(int selectedFanClub)
    {
        DataTable dtFanClubs = ViewState["FanClubs"] as DataTable;
        ViewState["currentClubName"] = txtFanClubName.Text = dtFanClubs.Rows[selectedFanClub - 1]["CLUBNAME"].ToString();
        ViewState["currentDescription"] = txtDescription.Text = dtFanClubs.Rows[selectedFanClub - 1]["DESCRIPTION"].ToString();
        ViewState["currentDateEstablished"] = txtDateEstablished.Text = ((DateTime)dtFanClubs.Rows[selectedFanClub - 1]["DATEESTABLISHED"]).ToString("dd-MMM-yyyy");
        pnlFanClubInfo.Visible = true;
    }

    protected void btnCalendar_Click(object sender, EventArgs e)
    {
        if (calDateEstablished.Visible == false)
        {
            calDateEstablished.Visible = true;
            btnCalendar.Text = "Hide Calendar";
        }
        else
        {
            calDateEstablished.Visible = false;
            btnCalendar.Text = "Show Calendar";
        }
    }

    protected void calBirthdate_SelectionChanged(object sender, EventArgs e)
    {
        txtDateEstablished.Text = calDateEstablished.SelectedDate.ToString("dd-MMM-yyyy");
        calDateEstablished.Visible = false;
        btnCalendar.Text = "Show Calendar";
    }

    protected void ddlFanClubName_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlFanClubName.SelectedIndex != 0)
        {
            PopulateFanClubInformation(ddlFanClubName.SelectedIndex);
        }
    }

    private bool FanClubIsChanged(string newClubName, string newDescription, string newDateEstablished)
    {
        if (ViewState["currentClubName"].ToString() == newClubName & ViewState["currentDescription"].ToString() == newDescription &
            ViewState["currentDateEstablished"].ToString() == newDateEstablished)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    protected void cvDateEstablished_ServerValidate(object source, System.Web.UI.WebControls.ServerValidateEventArgs args)
    {
        if (!myHelpers.DateIsValid(txtDateEstablished.Text))
        {
            args.IsValid = false;
        }
    }

}