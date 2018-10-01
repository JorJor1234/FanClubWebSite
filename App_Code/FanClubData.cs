using Oracle.DataAccess.Client;
using System;
using System.Configuration;
using System.Data;
using System.Windows.Forms;

//**********************************************************
//* THE CODE IN THIS CLASS CANNOT BE MODIFIED OR ADDED TO. *
//*        Report problems to 3311rep@cse.ust.hk.          *
//**********************************************************

public class FanClubData
{

    // Set the connection string to connect to the FanClub database.
    OracleConnection FanClubDBConnection = new OracleConnection(ConfigurationManager.ConnectionStrings["FanClubConnectionString"].ConnectionString);

    // Process a SQL SELECT statement.
    public DataTable GetData(string sql)
    {
        try
        {
            if (sql.Trim() == "")
            {
                throw new ArgumentException("The SQL statement is empty.");
            }

            DataTable dt = new DataTable();
            if (FanClubDBConnection.State != ConnectionState.Open)
            {
                FanClubDBConnection.Open();
                OracleDataAdapter da = new OracleDataAdapter(sql, FanClubDBConnection);
                da.Fill(dt);
                FanClubDBConnection.Close();
            }
            else
            {
                OracleDataAdapter da = new OracleDataAdapter(sql, FanClubDBConnection);
                da.Fill(dt);
            }
            return dt;
        }
        catch (ArgumentException ex)
        {
            MessageBox.Show(ex.Message);
        }
        catch (FormatException ex)
        {
            MessageBox.Show(ex.Message);
        }
        catch (OracleException ex)
        {
            MessageBox.Show(ex.Message);
        }
        return null;
    }

    // Process an SQL SELECT statement that returns only a single value.
    // Returns 0 if the table is empty or the column has no values.
    public decimal GetAggregateValue(string sql)
    {
        try
        {
            if (sql.Trim() == "")
            {
                throw new ArgumentException("The SQL statement is empty.");
            }
            object aggregateValue;
            if (FanClubDBConnection.State != ConnectionState.Open)
            {
                FanClubDBConnection.Open();
                OracleCommand SQLCmd = new OracleCommand(sql, FanClubDBConnection);
                SQLCmd.CommandType = CommandType.Text;
                aggregateValue = SQLCmd.ExecuteScalar();
                FanClubDBConnection.Close();
            }
            else
            {
                OracleCommand SQLCmd = new OracleCommand(sql, FanClubDBConnection);
                SQLCmd.CommandType = CommandType.Text;
                aggregateValue = SQLCmd.ExecuteScalar();
            }
            return (DBNull.Value == aggregateValue ? 0 : Convert.ToDecimal(aggregateValue));
        }
        catch (ArgumentException ex)
        {
            MessageBox.Show(ex.Message);
        }
        catch (FormatException ex)
        {
            MessageBox.Show(ex.Message);
        }
        catch (OracleException ex)
        {
            MessageBox.Show(ex.Message);
        }
        return -1;
    }

    // Process SQL INSERT, UPDATE and DELETE statements.
    public void SetData(string sql, OracleTransaction trans)
    {
        try
        {
            if (sql.Trim() == "")
            {
                throw new ArgumentException("The SQL statement is empty.");
            }

            OracleCommand SQLCmd = new OracleCommand(sql, FanClubDBConnection);
            SQLCmd.Transaction = trans;
            SQLCmd.CommandType = CommandType.Text;
            SQLCmd.ExecuteNonQuery();
        }
        catch (ArgumentException ex)
        {
            FanClubDBConnection.Close();
            MessageBox.Show(ex.Message);
        }
        catch (FormatException ex)
        {
            FanClubDBConnection.Close();
            MessageBox.Show(ex.Message);
        }
        catch (ApplicationException ex)
        {
            FanClubDBConnection.Close();
            MessageBox.Show(ex.Message);
        }
        catch (OracleException ex)
        {
            FanClubDBConnection.Close();
            MessageBox.Show(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            FanClubDBConnection.Close();
            MessageBox.Show(ex.Message);
        }
    }

    public OracleTransaction BeginTransaction()
    {
        if (FanClubDBConnection.State != ConnectionState.Open)
        {
            FanClubDBConnection.Open();
            OracleTransaction trans = FanClubDBConnection.BeginTransaction();
            return trans;
        }
        else
        {
            OracleTransaction trans = FanClubDBConnection.BeginTransaction();
            return trans;
        }
    }

    public void CommitTransaction(OracleTransaction trans)
    {
        try
        {
            if (FanClubDBConnection.State == ConnectionState.Open)
            {
                trans.Commit();
                FanClubDBConnection.Close();
            }
        }
        catch (ApplicationException ex)
        {
            MessageBox.Show(ex.Message);
        }
        catch (FormatException ex)
        {
            MessageBox.Show(ex.Message);
        }
        catch (OracleException ex)
        {
            MessageBox.Show(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    public int GetNextTableId(string idName, string tableName)
    {
        // Get the next available id for the table by selecting the maximum current id and adding 1.
        decimal maxTableId = GetAggregateValue("select max(" + idName + ") from " + tableName);
        // Check whether this is the first record being added to the database.
        if (maxTableId == -1)
        {
            MessageBox.Show("Error getting table id. \n Please contact 3311 Rep.");
        }
        return Convert.ToInt32(maxTableId + 1);
    }
}