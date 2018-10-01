using System;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;

public partial class RegisteredPerson_FanClubsJoined : System.Web.UI.Page
{
    FanClubDB myFanClubDB = new FanClubDB();
    Helpers myHelpers = new Helpers();
    string userName = HttpContext.Current.User.Identity.Name;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PopulateFanClubsJoined();
        }
    }

    protected void PopulateFanClubsJoined()
    {
        //***************
        // Uses TODO 22 *
        //***************
        DataTable dtFanClubsJoined = myFanClubDB.GetFanClubsJoined(userName);

        // Show the fan clubs joined if the query result is not null.
        if (dtFanClubsJoined != null)
        {
            if (dtFanClubsJoined.Rows.Count != 0)
            {
                gvFanClubsJoined.DataSource = dtFanClubsJoined;
                gvFanClubsJoined.DataBind();
            }
            else
            {
                HideJoinedFanClubs("You have not joined any fan clubs.");
            }
        }
        else // An SQL error occurred.
        {
            HideJoinedFanClubs("*** There is an error in the SELECT statement that retrieves the fan clubs joined.");
        }
    }

    protected void gvFanClubsJoined_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        // Format the gridview headers and data.
        if (e.Row.Cells.Count == 3)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Controls[0].Visible = false;
                e.Row.Cells[1].Text = "NAME";
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Controls[0].Visible = false;
            }
        }
    }

    private void ShowJoinedFanClubs(string message)
    {
        if (message != null)
        {
            myHelpers.ShowMessage(lblResultMessage, message);
            pnlFanClubsJoined.Visible = false;
        }
        else
        {
            lblResultMessage.Visible = false;
            pnlFanClubsJoined.Visible = true;
        }
    }

    private void HideJoinedFanClubs(string message)
    {
        if (message != null)
        {
            myHelpers.ShowMessage(lblResultMessage, message);
            pnlFanClubsJoined.Visible = false;

        }
        else
        {
            lblResultMessage.Visible = false;
            pnlFanClubsJoined.Visible = true;
        }
    }

}