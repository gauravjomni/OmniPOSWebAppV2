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
public class Refunds : System.Web.Services.WebService {

    DB mConnection = new DB();
    Common Fn = new Common();
    DataSet ds = new DataSet();

    public Refunds()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
        
    }

    [WebMethod]
    public XmlElement  UpdateRefund(string data)
    {

        XmlDocument doc = new XmlDocument();
        XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", null, null);
        doc.AppendChild(dec);
        XmlElement DocRoot;
        DocRoot = doc.CreateElement("StatusObjects");
        doc.AppendChild(DocRoot);

        try
        {

//          var dummydata = "{\"Refund\":{\"paymentType\":\"1\",\"restaurantId\":\"1\",\"description\":\"All Dirty\",\"refundAmount\":\"100.00\",\"companyId\":\"1\",\"date\":\"08-01-2013, 08:52 PM\",\"prodName\":\"Plain Rice\",\"deviceId\":\"2\",\"prodID\":\"1\",\"refundTranID\":\"RF-00001\",\"userId\":\"33\"}}";

//            var dummydata = "{"Refund":{"paymentType":"1","restaurantId":"7","description":"Yuuj","userId":"26","refundAmount":"20.00","companyId":"1","date":"02/06/2013 16:59:48 p05:30","prodName":"Tyuhg","refundTranID":"Refund_15638907_428211","deviceId":"9","prodID":"1"}}";

            var dummydata = data;

            string paymenttype = string.Empty;
            string restid = string.Empty;
            string comments = string.Empty;
            string amount = string.Empty;
            string trandate = string.Empty;
            string deviceid = string.Empty;
            string prodID = string.Empty;
            string RefundTranID = string.Empty;
            string usrid = string.Empty;

            JObject Refunds = JObject.Parse(dummydata);

            var Refund_Data = Refunds["Refund"].ToString();

            JObject Data = JObject.Parse(Refund_Data);

            paymenttype = Data["paymentType"].ToString();
            restid = Data["restaurantId"].ToString();
            comments = Data["description"].ToString();
            amount = Data["refundAmount"].ToString();
            trandate = Data["date"].ToString();
            deviceid = Data["deviceId"].ToString();
            prodID = Data["prodID"].ToString();
            RefundTranID = Data["refundTranID"].ToString();
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

                            SqlParameter[] ArParams = new SqlParameter[11];
                            ArParams[0] = new SqlParameter("@RefundTranID", SqlDbType.VarChar, 50);
                            ArParams[0].Value = RefundTranID;

                            ArParams[1] = new SqlParameter("@RefundDate", SqlDbType.DateTime);
                            //ArParams[1].Value = trandate;
                            ArParams[1].Value = Fn.ConvertIntoServerTime(trandate);

                            ArParams[2] = new SqlParameter("@ProductID", SqlDbType.Int);
                            ArParams[2].Value = prodID;

                            ArParams[3] = new SqlParameter("@DeviceID", SqlDbType.Int);
                            ArParams[3].Value = deviceid;

                            ArParams[4] = new SqlParameter("@Comments", SqlDbType.VarChar, 5000);
                            ArParams[4].Value = comments;

                            ArParams[5] = new SqlParameter("@Rest_ID", SqlDbType.Int);
                            ArParams[5].Value = restid;

                            ArParams[6] = new SqlParameter("@PaymentTypeID", SqlDbType.Int);
                            ArParams[6].Value = paymenttype;

                            ArParams[7] = new SqlParameter("@Amount", SqlDbType.Decimal);
                            ArParams[7].Value = amount;

                            ArParams[8] = new SqlParameter("@CreatedByUserID", SqlDbType.Int);
                            ArParams[8].Value = usrid;

                            ArParams[9] = new SqlParameter("@CreatedOn", SqlDbType.DateTime);
                            ArParams[9].Value = Dt;

                            ArParams[10] = new SqlParameter("@Mode", SqlDbType.VarChar, 20);
                            ArParams[10].Value = "add";

                            SqlHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, "SP_omni_Refund_Update", ArParams);

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

