using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.DataAccess.Client;
using System.Data;

/// <summary>
/// Summary description for DBAcces
/// </summary>
public class DBAccess
{
    FanClubData myFanClubData = new FanClubData();
    private string sql;

    /**************************************************************************/
    /* TODO: Construct the SQL statement to insert a newly registered person. */
    /**************************************************************************/
    public void InsertPerson(string userName, string firstName, string lastName, string gender, string phoneNo, string email)
    {
        sql = "insert into Person values ('" + userName + "', '" + firstName + "' ,'" + lastName + "', '" + gender + "', '" + phoneNo + "', '" + email + "')";
        OracleTransaction trans = myFanClubData.BeginTransaction();
        myFanClubData.SetData(sql, trans);
        myFanClubData.CommitTransaction(trans);
    }

    public DataTable GetClubsJoined(string userName)
    {
        /*******************************************************************/
        /* TODO: Construct the SQL statement to retrieve the club name     */
        /* and description of the fan clubs the person HAS ALREADY JOINED. */
        /*******************************************************************/
        sql = "select clubName, description from FanClub, HasMember " + 
            "where FanClub.clubId=HasMember.clubId and userName='" + userName + "'";
        DataTable ClubsJoined = myFanClubData.GetData(sql);
        return ClubsJoined;
    }

    public DataTable GetClubsAvailableToJoin(string userName)
    {
        /************************************************************************************/
        /* TODO: Construct the SQL statement to retrieve the club id, club name, decription */
        /* and date extablished of the fan clubs the person HAS NOT JOINED.                 */
        /************************************************************************************/
        sql = "select * from FanClub minus " +
            "select FanClub.clubId, clubName, description, dateEstablished from FanClub, HasMember " +
            "where FanClub.clubId=HasMember.clubId and userName='" + userName + "'";
        DataTable ClubsNotJoined = myFanClubData.GetData(sql);
        return ClubsNotJoined;
    }

    public void JoinFanClub(string clubId, string userName, string howInformed)
    {

    }
}