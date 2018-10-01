using System;
using System.Web.UI.WebControls;
using System.Windows.Forms;

/// <summary>
/// Helpers for the Fan Club Website
/// </summary>
public class Helpers
{
    OracleDBAccess myOracleDBAccess = new OracleDBAccess();
    FanClubDB myFanClubDB = new FanClubDB();
    private string sql;

    public Helpers()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public string CleanInput(string input)
    {
        // Replace single quote by two quotes.
        return input.Replace("'", "''");
    }

    public bool DateIsValid(string date)
    {
        if (date.Trim() == "") { return false; }
        DateTime resultDate;
        return DateTime.TryParse(date, out resultDate);
    }

    public int GetColumnIndexByName(GridView gv, string columnName)
    {
        // Helper method to get GridView column index by a column's DataField name.
        for (int i = 0; i < gv.Columns.Count; i++)
        {
            if (gv.Columns[i] is TemplateField)
            {
                if (((BoundField)gv.Columns[i]).DataField.ToString().Trim() == columnName.Trim())
                { return i; }
            }
        }
        MessageBox.Show("Column '" + columnName + "' was not found \n in the GridView '" + gv.ID.ToString() + "'.");
        return -1;
    }

    public bool InsertRegisteredUser(string userName, string firstName, string lastName,
        string gender, string phoneNo, string email)
    {
        sql = "insert into RegisteredUser values ('" + userName + "', '" + firstName + "' ,'" +
            lastName + "', '" + gender + "', '" + phoneNo + "', '" + email + "')";
        return myFanClubDB.UpdateData(sql);
    }

    public bool IsInteger(string number)
    {
        int n;
        return int.TryParse(number, out n);
    }

    public decimal GetNextTableId(string tableName, string idName)
    {
        sql = "select max(" + idName + ") from " + tableName;
        return myOracleDBAccess.GetAggregateValue(sql) + 1;
    }

    public void ShowMessage(System.Web.UI.WebControls.Label labelControl, string message)
    {
        if (message.Substring(0, 3) == "***") // Error message.
        {
            labelControl.ForeColor = System.Drawing.Color.Red;
        }
        else // Information message.
        {
            labelControl.ForeColor = System.Drawing.Color.DarkOrange; // "#ebebeb"
        }
        labelControl.Text = message;
        labelControl.Visible = true;
    }

    public bool UserEmailIsValid(string previousUserEmail, string newUserEmail)
    {
        if (previousUserEmail != newUserEmail)
        {
            sql = "select count(*) from RegisteredUser where userEmail='" + newUserEmail + "'";
            if (myOracleDBAccess.GetAggregateValue(sql) != 0)
            {
                return false;
            }
        }
        return true;
    }

    public bool ValidateUser(string userName)
    {
        // Check if user is a registered user.
        sql = "select count(*) from RegisteredUser where userName='" + userName + "'";
        if (myOracleDBAccess.GetAggregateValue(sql) == 1)
        {
            return true;
        }
        // Check if user is an employee.
        sql = "select count(*) from Employee where userName='" + userName + "'";
        if (myOracleDBAccess.GetAggregateValue(sql) == 1)
        {
            return true;
        }
        // User is not in fan club database.
        return false;
    }
}