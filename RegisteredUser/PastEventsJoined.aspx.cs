using System;
using System.Data;
using System.Globalization;
using System.Web;
using System.Web.UI.WebControls;

public partial class RegisteredUser_PastEventsJoined : System.Web.UI.Page
{
    FanClubDB myFanClubDB = new FanClubDB();
    Helpers myHelpers = new Helpers();
    string userName = HttpContext.Current.User.Identity.Name;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PopulatePastEventsJoined();
        }
    }

    protected void PopulatePastEventsJoined()
    {
        //***************
        // Uses TODO 12 *
        //***************
        DataTable dtEventsJoined = myFanClubDB.GetPastEventsJoined(userName);

        // Show the events joined query result if it is not null.
        if (dtEventsJoined != null)
        {
            if (dtEventsJoined.Rows.Count != 0)
            {
                gvPastEventsJoined.DataSource = dtEventsJoined;
                gvPastEventsJoined.DataBind();
                ShowJoinedEvents(null);
            }
            else
            {
                HideJoinedEvents("You do not have any past events.");
            }
        }
        else // An SQL error occurred.
        {
            HideJoinedEvents("*** There is an error in the SELECT statement that retrieves the events joined.");
        }
    }

    protected void gvPastEventsJoined_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.Cells.Count == 7)
        {
            // Format the gridview headers and data.
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Visible = false;
                e.Row.Cells[1].Text = "NAME";
                e.Row.Cells[2].Text = "DATE";
                e.Row.Cells[3].Text = "TIME";
                e.Row.Cells[5].Text = "MEMBER&nbsp;FEE";
                e.Row.Cells[6].Text = "NONMEMBER&nbsp;FEE";
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Visible = false;
                if (e.Row.Cells[2].Text != "&nbsp;")
                {
                    e.Row.Cells[2].Text = DateTime.Parse(e.Row.Cells[2].Text).ToString("dd-MMM-yyyy", CultureInfo.InvariantCulture);
                }
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Center;
            }
        }
    }

    private void ShowJoinedEvents(string message)
    {
        if (message != null)
        {
            myHelpers.ShowMessage(lblResultMessage, message);
            pnlEventsJoined.Visible = false;
        }
        else
        {
            lblResultMessage.Visible = false;
            pnlEventsJoined.Visible = true;
        }
    }

    private void HideJoinedEvents(string message)
    {
        if (message != null)
        {
            myHelpers.ShowMessage(lblResultMessage, message);
            pnlEventsJoined.Visible = false;

        }
        else
        {
            lblResultMessage.Visible = false;
            pnlEventsJoined.Visible = true;
        }
    }
}