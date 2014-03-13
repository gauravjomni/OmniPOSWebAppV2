using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using MyDB;
using Commons;
using MyQuery;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


/// <summary>
/// Summary description for getUserInfo
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]

public class orderAdjustment : System.Web.Services.WebService
{

    public orderAdjustment()
    {
        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod (EnableSession=true) ]

    public bool  processAdjust(string data)
    {
        bool flag = false;

        DB mConnection = new DB();
        Common Fn = new Common();
        SQLQuery Qry = new SQLQuery();
        DataSet ds = new DataSet();

        Dictionary<string, string> dict = null;
        string StrCurrency = string.Empty;

        string OrderIDS = string.Empty;
        string fromdate = string.Empty;
        string tilldate = string.Empty;
        string user_id = string.Empty;
        string rest_Id = string.Empty;
        decimal adjPercent = 0;


//      StrCurrency = Fn.GetTableColumnValue(dict, "omni_CompanyInfo", "CurrencySymbol", "", "");

        var dummydata = data;

        JObject Data = JObject.Parse(dummydata);
        var ParamDetails = Data["OrderAdjust"].ToString();
        JObject AdjustmentDetails = JObject.Parse(ParamDetails);

        fromdate = AdjustmentDetails["FromDate"].ToString();
        tilldate = AdjustmentDetails["TillDate"].ToString();
        rest_Id = AdjustmentDetails["restaurantId"].ToString();
        user_id = AdjustmentDetails["UserID"].ToString();
        adjPercent = (decimal)AdjustmentDetails["AdjustPercent"];


        if (fromdate != "" && Fn.ValidateDate(fromdate))
            fromdate = String.Format("{0:yyyy-MM-dd}", Fn.ConvertDateIntoAnotherFormat2(fromdate));

        if (tilldate != "" && Fn.ValidateDate(tilldate))
            tilldate = String.Format("{0:yyyy-MM-dd}", Fn.ConvertDateIntoAnotherFormat2(tilldate));

        DateTime sDate = DateTime.Now;

        try
        {
            using (SqlConnection conn = mConnection.GetConnection())
            {
                conn.Open();

                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        OrderIDS = Fn.GetOrderIDS(fromdate, tilldate, rest_Id);

                        SqlParameter[] ArParams = new SqlParameter[6];
                        ArParams[0] = new SqlParameter("@FromDate", SqlDbType.DateTime);
                        ArParams[0].Value = fromdate;

                        ArParams[1] = new SqlParameter("@TillDate", SqlDbType.DateTime);
                        ArParams[1].Value = tilldate;

                        ArParams[2] = new SqlParameter("@Rest_ID", SqlDbType.Int);
                        ArParams[2].Value = rest_Id;

                        ArParams[3] = new SqlParameter("@Adjustment", SqlDbType.Decimal);
                        ArParams[3].Value = adjPercent;
                        //ArParams[3].Scale = 2;

                        ArParams[4] = new SqlParameter("@LoggedUserID", SqlDbType.Int);
                        ArParams[4].Value = user_id;

                        ArParams[5] = new SqlParameter("@sDate", SqlDbType.DateTime);
                        ArParams[5].Value = sDate;

                        SqlHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, "SP_omni_Order_Adjustment_Update", ArParams);


                        SqlParameter[] ArParams1 = new SqlParameter[6];
                        ArParams1[0] = new SqlParameter("@FromDate", SqlDbType.DateTime);
                        ArParams1[0].Value = fromdate;

                        ArParams1[1] = new SqlParameter("@TillDate", SqlDbType.DateTime);
                        ArParams1[1].Value = tilldate;

                        ArParams1[2] = new SqlParameter("@Rest_ID", SqlDbType.Int);
                        ArParams1[2].Value = rest_Id;

                        ArParams1[3] = new SqlParameter("@OrderTranID", SqlDbType.Text);
                        ArParams1[3].Value = OrderIDS;

                        ArParams1[4] = new SqlParameter("@LoggedUserID", SqlDbType.Int);
                        ArParams1[4].Value = user_id;

                        ArParams1[5] = new SqlParameter("@sDate", SqlDbType.DateTime);
                        ArParams1[5].Value = sDate;

                        SqlHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, "SP_omni_Order_Adjustment_Log_Update", ArParams1);

                        trans.Commit();
                        flag = true;
                    }

                    catch (Exception ex)
                    {
                        // throw exception						
                        trans.Rollback();
                        throw ex;
                    }

                    finally
                    {
                        conn.Close();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            //Response.Write(ex.Message);
            //Response.End();
        }
        return flag;
    }

}

