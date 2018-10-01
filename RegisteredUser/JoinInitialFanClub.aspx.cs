using FanClubWebSite;
using Microsoft.AspNet.Identity;
using System;
using System.Web;
using System.Web.UI.WebControls;

public partial class RegisteredUser_JoinInitialFanClub : System.Web.UI.Page
{
    FanClubDB myFanClubDB = new FanClubDB();
    Helpers myHelpers = new Helpers();
    string userName = HttpContext.Current.User.Identity.Name;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            calBirthdate.Visible = false;
            lblResultMessage.Visible = false;
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            // Collect and validate the club member information.
            string occupation = myHelpers.CleanInput(txtOccupation.Text.Trim());
            string educationLevel = ddlEducationLevel.SelectedValue;
            string birthdate = txtBirthdate.Text.Trim();

            //***************
            // Uses TODO 06 *
            //***************
            if (myFanClubDB.InsertClubMember(userName, birthdate, occupation, educationLevel))
            {
                // Change the role of the user to Club Member.
                var manager = new UserManager();
                IdentityResult roleResult = new IdentityResult();
                roleResult = manager.RemoveFromRole(HttpContext.Current.User.Identity.GetUserId(), "Registered User");
                if (!roleResult.Succeeded)
                {
                    lblResultMessage.Text = "*** " + roleResult.Errors;
                    return;
                }
                roleResult = manager.AddToRole(HttpContext.Current.User.Identity.GetUserId(), "Club Member");
                if (!roleResult.Succeeded)
                {
                    lblResultMessage.Text = "*** " + roleResult.Errors;
                    return;
                }
                Response.Redirect("~/ClubMember/JoinFanClubs.aspx");
            }
            else // An SQL error occurred.
            {
                myHelpers.ShowMessage(lblResultMessage, "*** There is an error in the INSERT statement that adds the " +
                    "club member information.");
            }
        }
    }

    protected void btnCalendar_Click(object sender, EventArgs e)
    {
        if (calBirthdate.Visible == false)
        {
            calBirthdate.Visible = true;
            btnCalendar.Text = "Hide Calendar";
        }
        else
        {
            calBirthdate.Visible = false;
            btnCalendar.Text = "Show Calendar";
        }
    }

    protected void calBirthdate_SelectionChanged(object sender, EventArgs e)
    {
        txtBirthdate.Text = calBirthdate.SelectedDate.ToString("dd-MMM-yyyy");
        calBirthdate.Visible = false;
        btnCalendar.Text = "Show Calendar";
    }

    protected void cvBirthdate_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (!myHelpers.DateIsValid(txtBirthdate.Text))
        {
            args.IsValid = false;
        }
    }
}