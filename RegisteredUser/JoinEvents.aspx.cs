using System;
using System.Data;
using System.Globalization;
using System.Web;
using System.Web.UI.WebControls;

public partial class RegisteredUser_JoinEvents : System.Web.UI.Page
{
    FanClubDB myFanClubDB = new FanClubDB();
    Helpers myHelpers = new Helpers();
    string userName = HttpContext.Current.User.Identity.Name;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PopulateEventsNotJoined();
        }
    }

    protected void PopulateEventsNotJoined()
    {
        //***************
        // Uses TODO 13 *
        //***************
        DataTable dtEventsNotJoined = myFanClubDB.GetEventsNotJoined(userName);

        // Show the events not joined if it is not null.
        if (dtEventsNotJoined != null)
        {
            if (dtEventsNotJoined.Columns.IndexOf("EVENTID") == 0)
            {
                if (dtEventsNotJoined.Rows.Count != 0)
                {
                    gvEventsNotJoined.DataSource = dtEventsNotJoined;
                    gvEventsNotJoined.DataBind();
                    ShowNotJoinedEvents(null);
                }
                else
                {
                    HideNotJoinedEvents("There are no more events for you to join.");
                }
            }
            else // An SQL error occurred.
            {
                HideNotJoinedEvents("*** The SELECT statement either does not retrieve the event id or it is not the first attribute in the result.");
            }
        }
        else // An SQL error occurred.
        {
            HideNotJoinedEvents("*** There is an error in the SELECT statement that retrieves the events not joined.");
        }
    }

    protected void btnJoinSelectedEvents_Click(object sender, EventArgs e)
    {
        string eventId = null;
        int eventQuota;

        // Get the selected event and add the person to the event.
        foreach (GridViewRow row in gvEventsNotJoined.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkRow = (row.Cells[0].FindControl("chkSelected") as CheckBox);
                if (chkRow != null && chkRow.Checked)
                {
                    eventId = row.Cells[1].Text;
                    if (!myHelpers.IsInteger(eventId) | !myHelpers.IsInteger(row.Cells[8].Text))
                    {
                        myHelpers.ShowMessage(lblResultMessage, "*** The attributes in the SELECT statement that retrieves the events not joined are not in the correct order.");
                        return;
                    }
                    eventQuota = int.Parse(row.Cells[8].Text);
                    //***************
                    // Uses TODO 16 *
                    //***************
                    if (!myFanClubDB.JoinEvent(eventId, userName, "N", ""))
                    {
                        ShowNotJoinedEvents("*** There is an error in the INSERT statement for joining an event.");
                        return;
                    }
                    // Make the event unavailable if the quota has been reached.
                    if (eventQuota == 1)
                    {
                        //***************
                        // Uses TODO 21 *
                        //***************
                        if (!myFanClubDB.SetEventUnavailable(eventId))
                        {
                            ShowNotJoinedEvents("*** There is an error in the UPDATE statement that makes an event unavailable.");
                            return;
                        }
                    }
                }
            }
        }

        // Show the result of the update.
        if (eventId == null)
        {
            myHelpers.ShowMessage(lblResultMessage, "You have not selected any events.");
        }
        else
        {
            Response.Redirect("CurrentEventsJoined.aspx");
        }
    }

    protected void gvEventsNotJoined_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.Cells.Count == 9)
        {
            // Format the gridview headers and data.
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[1].Visible = false;
                e.Row.Cells[2].Text = "NAME";
                e.Row.Cells[3].Text = "DATE";
                e.Row.Cells[4].Text = "TIME";
                e.Row.Cells[6].Text = "MEMBER&nbsp;FEE";
                e.Row.Cells[7].Text = "NONMEMBER&nbsp;FEE";
                e.Row.Cells[8].Text = "REMAINING&nbsp;QUOTA";
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[1].Visible = false;
                if (e.Row.Cells[3].Text != "&nbsp;")
                {
                    e.Row.Cells[3].Text = DateTime.Parse(e.Row.Cells[3].Text).ToString("dd-MMM-yyyy", CultureInfo.InvariantCulture);
                }
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Center;
            }
        }
    }

    private void ShowNotJoinedEvents(string message)
    {
        if (message != null)
        {
            myHelpers.ShowMessage(lblResultMessage, message);
        }
        else
        {
            lblResultMessage.Visible = false;
            pnlEventsNotJoined.Visible = true;
        }
    }

    private void HideNotJoinedEvents(string message)
    {
        if (message != null)
        {
            myHelpers.ShowMessage(lblResultMessage, message);
        }
        else
        {
            lblResultMessage.Visible = false;
            pnlEventsNotJoined.Visible = false;
        }
    }
}