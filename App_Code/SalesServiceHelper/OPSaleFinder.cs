using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using MyDB;
using Commons;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using MyQuery;
using System.IO;
using System.Text;

/// <summary>
/// Provides different sales report for a restaurant location in a date range
/// </summary>
public class OPSaleFinder
{
	public OPSaleFinder()
	{
	}

    //returns total sale for a given payment type(mode) within a specific date range
    public decimal getTotalSaleForPaymentType(string paymentType, string fromdate, string tilldate, string rest_Id, SqlConnection conn) //1=cash, 2=card, 3=voucher
    {
        Dictionary<string, string> dict = null;
        SqlDataReader sqlDataReader = null;
        SQLQuery query = new SQLQuery();
        decimal totalSale = 0;

        dict = new Dictionary<string, string>() { { "Rest_ID", rest_Id }, { "b.PaymentTypeID", paymentType } };
        sqlDataReader = SqlHelper.ExecuteReader(conn, CommandType.Text, query.GetTotalSaleByPaymentTypeForXReportSQL(dict, fromdate, tilldate));

        while (sqlDataReader.Read())
        {
            totalSale = (decimal)sqlDataReader["PaidAmount"];
        }

        sqlDataReader.Close();

        return totalSale;
    }

    //returns total refund/payout amount within a specific date range
    public decimal getTotalRefundOrPayout(Boolean isRefund, string fromdate, string tilldate, string rest_Id, SqlConnection conn) //1=cash, 2=card, 3=voucher
    {
        Dictionary<string, string> dict = new Dictionary<string, string>() { { "Rest_ID", rest_Id } };
        SqlDataReader sqlDataReader = null;
        SQLQuery query = new SQLQuery();
        decimal totalSale = 0;

        if (isRefund)
            sqlDataReader = SqlHelper.ExecuteReader(conn, CommandType.Text, query.GetTotalRefundForXReportSQL(dict, fromdate, tilldate));
        else
            sqlDataReader = SqlHelper.ExecuteReader(conn, CommandType.Text, query.GetTotalPayoutForXReportSQL(dict, fromdate, tilldate));

        while (sqlDataReader.Read())
        {
            totalSale = (decimal)sqlDataReader["Amount"];
        }

        sqlDataReader.Close();

        return totalSale;
    }

    //returns total hourly sale for a location from given date to to date
    public Dictionary<string, decimal> getHourlySale(string fromdate, string tilldate, string rest_Id, SqlConnection conn) 
    {
        Dictionary<string, string> dict = new Dictionary<string, string>() { { "Rest_ID", rest_Id } };
        SqlDataReader sqlDataReader = null;
        SQLQuery query = new SQLQuery();

        string hour = string.Empty;
        decimal totalSale = 0;

        sqlDataReader = SqlHelper.ExecuteReader(conn, CommandType.Text, query.getTotalHourlySaleSQL(dict, fromdate, tilldate));

        Dictionary<string, decimal> resultDict = new Dictionary<string,decimal>();

        while (sqlDataReader.Read())
        {
            hour = sqlDataReader["saleHour"].ToString();
            totalSale = (decimal)sqlDataReader["amount"];
            resultDict.Add(hour, totalSale);
        }

        sqlDataReader.Close();

        return resultDict;
    }

    //returns user attendance
    public SqlDataReader getUserAttendanceReport(string fromdate, string tilldate, string rest_Id, string userId, SqlConnection conn)
    {
        Dictionary<string, string> dict = new Dictionary<string, string>() { { "a.RestID", rest_Id } };
        SqlDataReader sqlDataReader = null;
        SQLQuery query = new SQLQuery();

        sqlDataReader = SqlHelper.ExecuteReader(conn, CommandType.Text, query.getUserAttendanceSQL(dict, fromdate, tilldate, userId));

        return sqlDataReader;
    }

    //no sale count
    public int getNoSaleCount(string fromdate, string tilldate, string rest_Id, SqlConnection conn)
    {
        Dictionary<string, string> dict = new Dictionary<string, string>() { { "a.RestID", rest_Id } };
        SqlDataReader sqlDataReader = null;
        SQLQuery query = new SQLQuery();

        string hour = string.Empty;
        int count = 0;

        sqlDataReader = SqlHelper.ExecuteReader(conn, CommandType.Text, query.getNoSaleCountSQL(dict, fromdate, tilldate));

        while (sqlDataReader.Read())
        {
            string count_str = sqlDataReader["NoSaleCount"].ToString();
            count += Convert.ToInt32(count_str);
        }

        sqlDataReader.Close();

        return count;
    }
}