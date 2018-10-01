using FanClubWebSite;
using Microsoft.AspNet.Identity;
using System;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;

public partial class RegisteredUser_ManageRegisteredUserInformation : System.Web.UI.Page
{

    FanClubDB myFanClubDB = new FanClubDB();
    Helpers myHelpers = new Helpers();
    string userName = HttpContext.Current.User.Identity.Name;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string userId = HttpContext.Current.User.Identity.GetUserId();
            var manager = new UserManager();
            PopulateRegisterUserInformation();
            if (manager.IsInRole(userId, "Club Member"))
            {
                PopulateClubMemberInformation();
                pnlClubMemberInfo.Visible = true;
            }
            else
            {
                pnlClubMemberInfo.Visible = false;
            }
        }
    }

    protected void PopulateRegisterUserInformation()
    {
        //***************
        // Uses TODO 03 *
        //***************
        DataTable dtRegisteredUser = myFanClubDB.GetRegisteredUser(userName);

        // Show the registered user information if the query result is not null.
        if (dtRegisteredUser != null)
        {
            if (dtRegisteredUser.Rows.Count == 1)
            {
                ViewState["currentUserName"] = txtUserName.Text = dtRegisteredUser.Rows[0]["USERNAME"].ToString().Trim();
                ViewState["currentFirstName"] = txtFirstName.Text = dtRegisteredUser.Rows[0]["FIRSTNAME"].ToString().Trim();
                ViewState["currentLastName"] = txtLastName.Text = dtRegisteredUser.Rows[0]["LASTNAME"].ToString().Trim();
                ViewState["currentGender"] = txtGender.Text = dtRegisteredUser.Rows[0]["GENDER"].ToString();
                ViewState["currentPhoneNo"] = txtPhoneNo.Text = dtRegisteredUser.Rows[0]["PHONENO"].ToString();
                ViewState["currentUserEmail"] = txtUserEmail.Text = dtRegisteredUser.Rows[0]["USEREMAIL"].ToString().Trim();
            }
            else
            {
                myHelpers.ShowMessage(lblResultMessage, "*** There is an error in the SELECT statement that retrieves registered user information.");
            }
        }
        else // An SQL error occurred.
        {
            myHelpers.ShowMessage(lblResultMessage, "*** There is an error in the SELECT statement that retrieves registered user information.");
        }
    }

    protected void PopulateClubMemberInformation()
    {
        //***************
        // Uses TODO 05 *
        //***************
        DataTable dtClubMember = myFanClubDB.GetClubMember(userName);

        // Show the club member information if the query result is not null.
        if (dtClubMember != null)
        {
            if (dtClubMember.Rows.Count == 1)
            {
                ViewState["currentOccupation"] = txtOccupation.Text = dtClubMember.Rows[0]["OCCUPATION"].ToString();
                ViewState["currentEducationLevel"] = dtClubMember.Rows[0]["EDUCATIONLEVEL"].ToString().Trim();
                ddlEducationLevel.Items.FindByValue(dtClubMember.Rows[0]["EDUCATIONLEVEL"].ToString().Trim()).Selected = true;
                ViewState["currentBirthdate"] = txtBirthdate.Text = ((DateTime)dtClubMember.Rows[0]["BIRTHDATE"]).ToString("dd-MMM-yyyy");
            }
            else
            {
                myHelpers.ShowMessage(lblResultMessage, "*** There is an error in the SELECT statement that retrieves club member information.");
            }
        }
        else // An SQL error occurred.
        {
            myHelpers.ShowMessage(lblResultMessage, "*** There is an error in the SELECT statement that retrieves club member information.");
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            lblResultMessage.Visible = false;
            // Collect the registered user information for updating.
            string newFirstName = myHelpers.CleanInput(txtFirstName.Text.Trim());
            string newLastName = myHelpers.CleanInput(txtLastName.Text.Trim());
            string newGender = txtGender.Text.ToUpper();
            string newPhoneNo = txtPhoneNo.Text;
            string newUserEmail = myHelpers.CleanInput(txtUserEmail.Text.Trim());
            string resultMessage = "You have not changed any information.";

            // Update the registered user information if it has changed.
            if (RegisteredUserIsChanged(newFirstName, newLastName, newGender, newPhoneNo, newUserEmail))
            {
                //***************
                // Uses TODO 04 *
                //***************
                if (myFanClubDB.UpdateRegisteredUser(userName, newFirstName, newLastName, newGender, newPhoneNo, newUserEmail))
                {
                    PopulateRegisterUserInformation();
                    resultMessage = "Your information has been updated.";
                }
                else // An SQL error occurred.
                {
                    resultMessage = "*** There is an error in the UPDATE statement that updates the registered user information.";
                }
            }

            // Update the club member information if it has changed.
            if (pnlClubMemberInfo.Visible == true)
            {
                // Collect and validate the club member information for updating.
                string newOccupation = myHelpers.CleanInput(txtOccupation.Text.Trim());
                string newEducationLevel = ddlEducationLevel.SelectedValue;
                string newBirthdate = txtBirthdate.Text.Trim();

                if (ClubMemberIsChanged(newOccupation, newEducationLevel, newBirthdate))
                {
                    //***************
                    // Uses TODO 07 *
                    //***************
                    if (myFanClubDB.UpdateClubMember(userName, newBirthdate, newOccupation, newEducationLevel))
                    {
                        PopulateClubMemberInformation();
                        resultMessage = "Your information has been updated.";
                    }
                    else
                    {
                        if (resultMessage.Substring(0, 3) == "***")
                        {
                            resultMessage = resultMessage + "<br />*** There is an error in the UPDATE statement that updates the club member information.";
                        }
                        else
                        {
                            resultMessage = "*** There is an error in the UPDATE statement that updates the club member information.";
                        }
                    }
                }
            }

            myHelpers.ShowMessage(lblResultMessage, resultMessage);
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

    protected void cvUserEmail_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (!myHelpers.UserEmailIsValid(ViewState["currentUserEmail"].ToString(), txtUserEmail.Text.Trim()))
        {
            args.IsValid = false;
        }
    }

    private bool RegisteredUserIsChanged(string newFirstName, string newLastName, string newGender, string newPhoneNo, string newUserEmail)
    {
        if (ViewState["currentFirstName"].ToString() == newFirstName & ViewState["currentLastName"].ToString() == newLastName &
            ViewState["currentGender"].ToString() == newGender & ViewState["currentPhoneNo"].ToString() == newPhoneNo &
            ViewState["currentUserEmail"].ToString() == newUserEmail)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private bool ClubMemberIsChanged(string newOccupation, string newEducationLevel, string newBirthdate)
    {
        if (ViewState["currentOccupation"].ToString() == newOccupation & ViewState["currentEducationLevel"].ToString() == newEducationLevel &
            ViewState["currentBirthdate"].ToString() == newBirthdate)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

}