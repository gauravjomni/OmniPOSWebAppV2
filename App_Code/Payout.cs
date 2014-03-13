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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


/// <summary>
/// Summary description for getUserInfo
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class Payout : System.Web.Services.WebService {

    DB mConnection = new DB();
    Common Fn = new Common();
    DataSet ds = new DataSet();

    public Payout()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
        
    }

    [WebMethod]
    public XmlElement UpdatePayout(string data)
    {

        XmlDocument doc = new XmlDocument();
        XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", null, null);
        doc.AppendChild(dec);
        XmlElement DocRoot;
        DocRoot = doc.CreateElement("StatusObjects");
        doc.AppendChild(DocRoot);

        try
        {

//          var dummydata = "{\"Payout\":{\"paymentType\":\"2\",\"restaurantId\":\"1\",\"description\":\"Gggg\",\"payoutAmount\":\"20.00\",\"payoutTranID\":\"Payout_B965145B_312662\",\"companyId\":\"1\",\"date\":\"10-01-2013, 12:36 PM\",\"deviceId\":\"2\",\"userId\":\"3\"}}";
            
            var dummydata = data;

            string paymenttype = string.Empty;
            string restid = string.Empty;
            string comments = string.Empty;
            string amount = string.Empty;
            string trandate = string.Empty;
            string deviceid = string.Empty;
            string PayoutTranID = string.Empty;
            string usrid = string.Empty;

            JObject Payouts = JObject.Parse(dummydata);

            var Payout_Data = Payouts["Payout"].ToString();

            JObject Data = JObject.Parse(Payout_Data);

            paymenttype = Data["paymentType"].ToString();
            restid = Data["restaurantId"].ToString();
            comments = Data["description"].ToString();
            amount = Data["payoutAmount"].ToString();
            trandate = Data["date"].ToString();
            deviceid = Data["deviceId"].ToString();
            PayoutTranID = Data["payoutTranID"].ToString();
            usrid =  Data["userId"].ToString();
            
            using (SqlConnection conn = mConnection.GetConnection())
            {
                conn.Open();

                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        if (trandate != null && trandate != "")
                        {
                            DateTime sDate = DateTime.Now;
                            string Dt = String.Format("{0:yyyy-MM-dd hh:mm:ss}", sDate);

                            SqlParameter[] ArParams = new SqlParameter[10];
                            ArParams[0] = new SqlParameter("@PayoutTranID", SqlDbType.VarChar, 50);
                            ArParams[0].Value = PayoutTranID;

                            ArParams[1] = new SqlParameter("@PayoutDate", SqlDbType.DateTime);
                            //ArParams[1].Value = trandate;
                            ArParams[1].Value = Fn.ConvertIntoServerTime(trandate);

                            ArParams[2] = new SqlParameter("@DeviceID", SqlDbType.Int);
                            ArParams[2].Value = deviceid;

                            ArParams[3] = new SqlParameter("@Comments", SqlDbType.VarChar, 5000);
                            ArParams[3].Value = comments;

                            ArParams[4] = new SqlParameter("@Rest_ID", SqlDbType.Int);
                            ArParams[4].Value = restid;

                            ArParams[5] = new SqlParameter("@PaymentTypeID", SqlDbType.Int);
                            ArParams[5].Value = paymenttype;

                            ArParams[6] = new SqlParameter("@Amount", SqlDbType.Decimal);
                            ArParams[6].Value = amount;

                            ArParams[7] = new SqlParameter("@CreatedByUserID", SqlDbType.Int);
                            ArParams[7].Value = usrid;

                            ArParams[8] = new SqlParameter("@CreatedOn", SqlDbType.DateTime);
                            ArParams[8].Value = Dt;

                            ArParams[9] = new SqlParameter("@Mode", SqlDbType.VarChar, 20);
                            ArParams[9].Value = "add";

                            SqlHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, "SP_omni_Payout_Update", ArParams);

                            trans.Commit();
                        }
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


            //HttpContext.Current.Response.Write("cnt->" + ProductsArray.Count);
            XmlNode StatusInfo = doc.CreateElement("Status");
            StatusInfo.InnerText = "True";
            DocRoot.AppendChild(StatusInfo);
            return DocRoot;
        }
        catch (Exception e)
        {
            //HttpContext.Current.Response.Write(e.Message.ToString());
            XmlNode StatusInfo = doc.CreateElement("Status");
            StatusInfo.InnerText = "False";
            DocRoot.AppendChild(StatusInfo);
            return DocRoot;
        }
    }    
}

