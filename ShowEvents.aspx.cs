using System;
using System.Data;
using System.Globalization;
using System.Web.UI.WebControls;

public partial class ShowEvents : System.Web.UI.Page
{
    FanClubDB myFanClubDB = new FanClubDB();
    Helpers myHelpers = new Helpers();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PopulateEvents();
        }
    }

    protected void PopulateEvents()
    {
        DataTable dtEvents = myFanClubDB.GetCurrentEvents();

        // Show the current events query result if it is not null and returns data.
        if (dtEvents != null)
        {
            if (dtEvents.Rows.Count != 0)
            {
                gvEvents.DataSource = dtEvents;
                gvEvents.DataBind();
            }
            else
            {
                myHelpers.ShowMessage(lblResultMessage, "There are no current events.");
            }
        }
        else // An SQL error occurred.
        {
            myHelpers.ShowMessage(lblResultMessage, "*** There is an error in the SELECT statement that retrieves all the current events.");
        }

    }

    protected void gvEvents_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.Cells.Count == 8)
        {
            // Format the gridview headers and data.
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Text = "NAME";
                e.Row.Cells[1].Text = "DATE";
                e.Row.Cells[2].Text = "TIME";
                e.Row.Cells[4].Text = "MEMBER&nbsp;FEE";
                e.Row.Cells[5].Text = "NONMEMBER&nbsp;FEE";
                e.Row.Cells[6].Text = "QUOTA";
                e.Row.Cells[7].Text = "MEMBER&nbsp;ONLY";
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[1].Text != "&nbsp;")
                {
                    e.Row.Cells[1].Text = DateTime.Parse(e.Row.Cells[1].Text).ToString("dd-MMM-yyyy", CultureInfo.InvariantCulture);
                }
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Center;
            }
        }
    }

}