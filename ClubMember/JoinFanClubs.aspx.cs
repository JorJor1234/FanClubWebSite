using FanClubWebSite;
using Microsoft.AspNet.Identity;
using System;
using System.Data;
using System.Globalization;
using System.Web;
using System.Web.UI.WebControls;

public partial class RegisteredPerson_JoinFanClubs : System.Web.UI.Page
{
    FanClubDB myFanClubDB = new FanClubDB();
    Helpers myHelpers = new Helpers();
    string userName = HttpContext.Current.User.Identity.Name;
    

    protected void Page_Load(object sender, EventArgs e)
    {
        var manager = new UserManager();
        if (manager.IsInRole(HttpContext.Current.User.Identity.GetUserId(), "Registered User"))
        {
            Response.Redirect("~/RegisteredUser/JoinInitialFanClub.aspx");
        }
        if (!IsPostBack)
        {
            PopulateFanClubsNotJoined();
        }
    }

    protected void PopulateFanClubsNotJoined()
    {
        //***************
        // Uses TODO 23 *
        //***************
        DataTable dtFanClubsNotJoined = myFanClubDB.GetFanClubsNotJoined(userName);

        // Show the fan clubs not joined if the query result is not null.
        if (dtFanClubsNotJoined != null)
        {
            gvFanClubsNotJoined.DataSource = dtFanClubsNotJoined;
            gvFanClubsNotJoined.DataBind();

            // Show fan clubs not joined result.
            if (dtFanClubsNotJoined.Rows.Count == 0)
            {
                HideNotJoinedFanClubs("There are no more fan clubs for you to join.");
            }
            else
            {
                ShowNotJoinedFanClubs(null);
            }
        }
        else  // An SQL error occurred.
        {
            HideNotJoinedFanClubs("*** There is an error in the SELECT statement that retrieves the fan clubs not joined.");
        }
    }

    protected void btnJoinSelectedFanClubs_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            string clubId = null;
            string joinDate = DateTime.Now.ToString("dd-MMM-yyyy");
            string howInformed;

            if (HowInformedIsValid())
            {
                // Update fan clubs joined with the selected fan clubs.
                foreach (GridViewRow row in gvFanClubsNotJoined.Rows)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("chkSelected") as CheckBox);
                    if (chkRow != null && chkRow.Checked)
                    {
                        // Get the values for how informed and club id.
                        DropDownList ddlHowInformed = (DropDownList)row.FindControl("ddlHowInformed");
                        howInformed = ddlHowInformed.SelectedValue;
                        clubId = row.Cells[2].Text;
                        //***************
                        // Uses TODO 24 *
                        //***************
                        if (!myFanClubDB.JoinFanClub(clubId, userName, joinDate, howInformed))
                        {
                            ShowNotJoinedFanClubs("*** There is an error in the SQL INSERT statement for joining a fan club.");
                            return;
                        }
                    }
                }

                // Show the result of the update.
                if (clubId == null)
                {
                    myHelpers.ShowMessage(lblResultMessage, "You have not selected any fan clubs.");
                }
                else
                {
                    Response.Redirect("FanClubsJoined");
                }
            }
        }
    }

    protected void gvFanClubsNotJoined_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        // Format the fan clubs not joined gridview headers and data.
        if (e.Row.Cells.Count == 6)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Controls[2].Visible = false;
                e.Row.Cells[3].Text = "CLUB&nbsp;NAME";
                e.Row.Cells[5].Text = "DATE&nbsp;ESTABLISHED";
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Controls[2].Visible = false;
                if (e.Row.Cells[5].Text != "&nbsp;")
                {
                    e.Row.Cells[5].Text = DateTime.Parse(e.Row.Cells[5].Text).ToString("dd-MMM-yyyy", CultureInfo.InvariantCulture);
                    e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Center;
                }
            }
        }
    }

    private bool HowInformedIsValid()
    {
        string howInformed;
        // Check if no value was selected for how informed in a checked row.
        foreach (GridViewRow row in gvFanClubsNotJoined.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkRow = (row.Cells[0].FindControl("chkSelected") as CheckBox);
                if (chkRow != null && chkRow.Checked)
                {
                    // Get the value for how informed.
                    DropDownList ddlHowInformed = (DropDownList)row.FindControl("ddlHowInformed");
                    howInformed = ddlHowInformed.SelectedValue;
                    // Exit if no value was selected for how informed in the check row.
                    if (howInformed == "none selected")
                    {
                        string clubName = row.Cells[3].Text;
                        ShowNotJoinedFanClubs("*** Please select a value for How Informed for " + clubName + ".");
                        return false;
                    }
                }
            }
        }
        return true;
    }

    private void ShowNotJoinedFanClubs(string message)
    {
        if (message != null)
        {
            myHelpers.ShowMessage(lblResultMessage, message);
        }
        else
        {
            lblResultMessage.Visible = false;
            pnlFanClubsNotJoined.Visible = true;
        }
    }

    private void HideNotJoinedFanClubs(string message)
    {
        if (message != null)
        {
            myHelpers.ShowMessage(lblResultMessage, message);
        }
        else
        {
            lblResultMessage.Visible = false;
            pnlFanClubsNotJoined.Visible = false;
        }
    }

}