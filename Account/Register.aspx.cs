using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Web.UI;
using FanClubWebSite;
public partial class Account_Register : Page
{
    Helpers myHelpers = new Helpers();

    protected void CreateUser_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            string userName = myHelpers.CleanInput(UserName.Text.Trim());
            string firstName = myHelpers.CleanInput(FirstName.Text.Trim());
            string lastName = myHelpers.CleanInput(LastName.Text.Trim());
            string userEmail = myHelpers.CleanInput(Email.Text.Trim());

            var manager = new UserManager();
            var user = new ApplicationUser() { UserName = userName };
            IdentityResult result = manager.Create(user, Password.Text);
            if (result.Succeeded)
            {
                IdentityResult roleResult = manager.AddToRole(user.Id, "Registered User");
                if (roleResult.Succeeded)
                {
                    myHelpers.InsertRegisteredUser(userName, firstName, lastName, Gender.Text.ToUpper(), PhoneNo.Text, userEmail);
                    IdentityHelper.SignIn(manager, user, isPersistent: false);
                    IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
                }
                else
                {
                    ErrorMessage.Text = roleResult.Errors.FirstOrDefault();
                }
            }
            else
            {
                ErrorMessage.Text = result.Errors.FirstOrDefault();
            }
        }
    }

    protected void cvUserEmail_ServerValidate(object source, System.Web.UI.WebControls.ServerValidateEventArgs args)
    {
        if (!myHelpers.UserEmailIsValid("", Email.Text.Trim()))
        {
            args.IsValid = false;
        }
    }

}