using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;

public partial class ShowFanClubs : System.Web.UI.Page
{
    FanClubDB myFanClubDB = new FanClubDB();
    Helpers myHelpers = new Helpers();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PopulateFanClubs();
        }
    }

    protected void PopulateFanClubs()
    {
        DataTable dtFanClubs = myFanClubDB.GetFanClubs();

        // Show the fan clubs if the query result is not null and some data is returned.
        if (dtFanClubs != null)
        {
            if (dtFanClubs.Rows.Count != 0)
            {
                gvFanClubs.DataSource = dtFanClubs;
                gvFanClubs.DataBind();
            }
            else
            {
                myHelpers.ShowMessage(lblResultMessage, "*** INTERNAL SYSTEM ERROR. There are no fan clubs! Please contact 3311 Rep");
            }
        }
        else // An SQL error occurred.
        {
            myHelpers.ShowMessage(lblResultMessage, "*** There is an error in the SELECT statement that retrieves the fan clubs joined.");
        }
    }

    protected void gvFanClubs_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        // Format the gridview headers and data; hide the club id.
        if (e.Row.Cells.Count == 4)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Visible = false;
                e.Row.Cells[1].Text = "NAME";
                e.Row.Cells[3].Text = "DATE&nbsp;ESTABLISHED";
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Controls[0].Visible = false;
                if (e.Row.Cells[3].Text != "&nbsp;")
                {
                    e.Row.Cells[3].Text = DateTime.Parse(e.Row.Cells[3].Text).ToString("dd-MMM-yyyy", CultureInfo.InvariantCulture);
                }
            }
        }
    }

}