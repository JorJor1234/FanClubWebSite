using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Web;
using System.Web.UI;
using FanClubWebSite;
using System.Web.Security;

public partial class Account_Login : Page
{
    Helpers myHelpers = new Helpers();
    protected void Page_Load(object sender, EventArgs e)
    {
        RegisterHyperLink.NavigateUrl = "Register";
        var returnUrl = HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
        if (!String.IsNullOrEmpty(returnUrl))
        {
            RegisterHyperLink.NavigateUrl += "?ReturnUrl=" + returnUrl;
        }
    }

    protected void LogIn(object sender, EventArgs e)
    {
        if (IsValid)
        {
            // Validate the user password
            var manager = new UserManager();
            ApplicationUser user = manager.Find(UserName.Text, Password.Text);
            if (user != null)
            {
                // Validate user exists in Fan Club database; delete user login if not in database.
                if (myHelpers.ValidateUser(UserName.Text))
                {
                    IdentityHelper.SignIn(manager, user, RememberMe.Checked);
                    IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
                }
                else
                {
                    manager.Delete(user);
                    FailureText.Text = "Invalid username.";
                    ErrorMessage.Visible = true;
                }
            }
            else
            {
                FailureText.Text = "Invalid username.";
                ErrorMessage.Visible = true;
            }
        }
    }
}