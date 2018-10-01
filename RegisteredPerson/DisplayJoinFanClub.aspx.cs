using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FanClubWebSite;

public partial class RegisteredPerson_DisplayJoinFanClub : System.Web.UI.Page
{
    DBAccess myDBAccess = new DBAccess();
    private string userName = "tracytse"; /* HttpContext.Current.User.ToString(); */

    protected void Page_Load(object sender, EventArgs e)
    {
        pnlFanClubsAvailableToJoin.Visible = false;
        ShowFanClubsJoined();
    }

    protected void ShowFanClubsJoined()
    {
        /*********/
        /* TODO #*/
        /*********/
        DataTable dtClubsJoined = myDBAccess.GetClubsJoined(userName);

        // If the DataTable is null, an SQL error occurred, so exit.
        if (dtClubsJoined == null)
        {
            lblResultMessage.Text = "*** There is an error in the SQL statement that retrieves the fan clubs already joined.";
            lblResultMessage.Visible = true;
            return;
        }

        gvClubsJoined.DataSource = dtClubsJoined;
        gvClubsJoined.DataBind();

        // Show clubs joined result.
        if (dtClubsJoined.Rows.Count == 0)
        {
            lblResultMessage.Text = "You have not joined any fan clubs.";
            lblResultMessage.Visible = true;
            HideClubsJoined();
        }
        else
        {
            lblResultMessage.Visible = false;
            ShowClubsJoined();
        }
    }

    protected void ShowFanClubsAvailableToJoin()
    {
        /*********/
        /* TODO #*/
        /*********/
        DataTable dtClubsAvailableToJoin = myDBAccess.GetClubsAvailableToJoin(userName);

        // If the DataTable is null, an SQL error occurred, so exit.
        if (dtClubsAvailableToJoin == null)
        {
            lblResultMessage.Text = "*** There is an error in the SQL statement that retrieves the fan clubs not joined.";
            lblResultMessage.Visible = true;
            return;
        }

        gvClubsAvailableToJoin.DataSource = dtClubsAvailableToJoin;
        gvClubsAvailableToJoin.DataBind();

        // Show clubs available to join result.
        if (dtClubsAvailableToJoin.Rows.Count == 0)
        {
            lblResultMessage.Text = "There are no fan clubs for you to join.";
            lblResultMessage.Visible = true;
            HideClubsAvailableToJoin();
        }
        else
        {
            lblResultMessage.Visible = false;
            ShowClubsAvailableToJoin();
        }
    }


    protected void ShowClubsJoined()
    {
        lblClubsJoined.Visible = true;
        gvClubsJoined.Visible = true;
    }

    protected void HideClubsJoined()
    {
        lblClubsJoined.Visible = false;
        gvClubsJoined.Visible = false;
    }

    protected void ShowClubsAvailableToJoin()
    {
        lblClubsAvailableToJoin.Visible = true;
        gvClubsAvailableToJoin.Visible = true;
        btnJoinSelectedFanClubs.Visible = true;
    }

    protected void HideClubsAvailableToJoin()
    {
        lblClubsAvailableToJoin.Visible = false;
        gvClubsAvailableToJoin.Visible = false;
        btnJoinSelectedFanClubs.Visible = false;
    }

    protected void btnShowFanClubsAvailableToJoin_Click(object sender, EventArgs e)
    {
        pnlFanClubsAvailableToJoin.Visible = true;
        ShowFanClubsAvailableToJoin();
    }


    protected void btnJoinSelectedFanClubs_Click(object sender, EventArgs e)
    {
        string clubId;

        // Determine if any fan club was selected.
        foreach (GridViewRow row in gvClubsAvailableToJoin.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkRow = (row.Cells[0].FindControl("chkSelected") as CheckBox);
                if (chkRow != null && chkRow.Checked)
                {
                    // Get the club id code.
                    clubId = row.Cells[1].Text;
                    //******************************************************
                    // TODO ?: Construct the SQL statement to join a club. *
                    //******************************************************
                    myDBAccess.JoinFanClub(clubId, userName, howInformed);
                }
            }
        }
    }

    protected void gvClubsAvailableToJoin_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        // Hide the club id column in the gridview.
        if (e.Row.RowType == DataControlRowType.Header | e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Controls[1].Visible = false;
        }
    }
}