using FanClubWebSite;
using Microsoft.AspNet.Identity;
using System;
using System.Web;
using System.Web.Providers.Entities;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using static ASP.global_asax;

public partial class SiteMaster : MasterPage
{
    private const string AntiXsrfTokenKey = "__AntiXsrfToken";
    private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
    private string _antiXsrfTokenValue;

    protected void Page_Init(object sender, EventArgs e)
    {
        // The code below helps to protect against XSRF attacks
        var requestCookie = Request.Cookies[AntiXsrfTokenKey];
        Guid requestCookieGuidValue;
        if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
        {
            // Use the Anti-XSRF token from the cookie
            _antiXsrfTokenValue = requestCookie.Value;
            Page.ViewStateUserKey = _antiXsrfTokenValue;
        }
        else
        {
            // Generate a new Anti-XSRF token and save to the cookie
            _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
            Page.ViewStateUserKey = _antiXsrfTokenValue;

            var responseCookie = new HttpCookie(AntiXsrfTokenKey)
            {
                HttpOnly = true,
                Value = _antiXsrfTokenValue
            };
            if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
            {
                responseCookie.Secure = true;
            }
            Response.Cookies.Set(responseCookie);
        }

        Page.PreLoad += master_Page_PreLoad;
    }

    protected void master_Page_PreLoad(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // Set Anti-XSRF token
            ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
            ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
        }
        else
        {
            // Validate the Anti-XSRF token
            if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
            {
                throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        // Homepage menu items.
        liShowFanClubs.Visible = true;
        liShowEvents.Visible = true;
        // Hide other menu items.
        // Fan club menu items.
        liFanClubDropDown.Visible = false;
        liJoinInitialFanClub.Visible = false;
        liFanClubsJoined.Visible = false;
        liJoinFanClubs.Visible = false;
        liSubmitFanClubRemark.Visible = false;
        liViewFanClubRemarks.Visible = false;
        liCreateFanClub.Visible = false;
        liModifyFanClub.Visible = false;
        liProcessFanClubRemarks.Visible = false;
        // Event menu items.
        liEventDropDown.Visible = false;
        liCurrentEventsJoined.Visible = false;
        liPastEventsJoined.Visible = false;
        liJoinEvents.Visible = false;
        liSubmitEventRemark.Visible = false;
        liViewEventRemarks.Visible = false;
        liCreateEvent.Visible = false;
        liModifyEvent.Visible = false;
        liProcessEventRemarks.Visible = false;
        liRecordEventAttendance.Visible = false;
        // Manage own information.
        liManageInformation.Visible = false;

        string userId = HttpContext.Current.User.Identity.GetUserId();
        var manager = new UserManager();

        if (userId != null)
        {
            if (manager.IsInRole(userId, "Employee"))
            {
                // Hide homepage menu items.
                liShowFanClubs.Visible = false;
                liShowEvents.Visible = false;
                // Fan club menu items.
                liFanClubDropDown.Visible = true;
                liCreateFanClub.Visible = true;
                liModifyFanClub.Visible = true;
                liProcessFanClubRemarks.Visible = true;
                // Event menu items.
                liEventDropDown.Visible = true;
                liCreateEvent.Visible = true;
                liModifyEvent.Visible = true;
                liProcessEventRemarks.Visible = true;
                liRecordEventAttendance.Visible = true;
            }

            if (manager.IsInRole(userId, "Registered User"))
            {
                // Hide homepage menu items.
                liShowFanClubs.Visible = false;
                liShowEvents.Visible = false;
                // Fan clubr menu items.
                liFanClubDropDown.Visible = true;
                liJoinInitialFanClub.Visible = true;
                // Event menu items
                liEventDropDown.Visible = true;
                liCurrentEventsJoined.Visible = true;
                liPastEventsJoined.Visible = true;
                liJoinEvents.Visible = true;
                // Manage own information
                liManageInformation.Visible = true;
            }

            if (manager.IsInRole(userId, "Club Member"))
            {
                // Hide homepage menu items.
                liShowFanClubs.Visible = false;
                liShowEvents.Visible = false;
                // Fan club menu items.
                liFanClubDropDown.Visible = true;
                liJoinFanClubs.Visible = true;
                liSubmitFanClubRemark.Visible = true;
                liViewFanClubRemarks.Visible = true;
                // Event menu items.
                liEventDropDown.Visible = true;
                liCurrentEventsJoined.Visible = true;
                liPastEventsJoined.Visible = true;
                liJoinEvents.Visible = true;
                liSubmitEventRemark.Visible = true;
                liViewEventRemarks.Visible = true;
                // Manage own information.
                liManageInformation.Visible = true;
            }
        }

    }


    protected void Unnamed_LoggingOut(object sender, LoginCancelEventArgs e)
    {
        Context.GetOwinContext().Authentication.SignOut();
        Response.Redirect("~/Default.aspx");
    }
}