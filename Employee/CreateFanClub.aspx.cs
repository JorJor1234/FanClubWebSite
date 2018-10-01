using System;

public partial class Employee_CreateFanClub : System.Web.UI.Page
{
    FanClubDB myFanClubDB = new FanClubDB();
    Helpers myHelpers = new Helpers();

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnCreateFanClub_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            lblResultMessage.Visible = false;
            // Collect the fan club information.
            string fanClubName = myHelpers.CleanInput(txtFanClubName.Text.Trim());
            string description = myHelpers.CleanInput(txtDescription.Text.Trim());
            string dateEstablished = txtDateEstablished.Text.Trim();
            string fanClubId = myHelpers.GetNextTableId("FanClub", "clubId").ToString();

            if (fanClubId != "0")
            {
                //***************
                // Uses TODO 25 *
                //***************
                if (myFanClubDB.CreateFanClub(fanClubId, fanClubName, description, dateEstablished))
                {
                    myHelpers.ShowMessage(lblResultMessage, "The fan club - " + fanClubName + " - has been created.");
                }
                else // An SQL error occurred.
                {
                    myHelpers.ShowMessage(lblResultMessage, "*** There is an error in the INSERT statement that creates a fan club.");
                }
            }
            else // An SQL error occurred.
            {
                myHelpers.ShowMessage(lblResultMessage, "*** There is an error in the SELECT statement that retrieves the maximum fan club id.");
            }
        }
    }

    protected void btnCalendar_Click(object sender, EventArgs e)
    {
        if (calDateEstablished.Visible == false)
        {
            calDateEstablished.Visible = true;
            btnCalendar.Text = "Hide Calendar";
        }
        else
        {
            calDateEstablished.Visible = false;
            btnCalendar.Text = "Show Calendar";
        }
    }

    protected void calBirthdate_SelectionChanged(object sender, EventArgs e)
    {
        txtDateEstablished.Text = calDateEstablished.SelectedDate.ToString("dd-MMM-yyyy");
        calDateEstablished.Visible = false;
        btnCalendar.Text = "Show Calendar";
    }

    protected void cvDateEstablished_ServerValidate(object source, System.Web.UI.WebControls.ServerValidateEventArgs args)
    {
        if (!myHelpers.DateIsValid(txtDateEstablished.Text))
        {
            args.IsValid = false;
        }
    }
}