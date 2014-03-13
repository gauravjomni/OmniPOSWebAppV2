using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Web.SessionState;
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
public class OrderInfoUpdate : System.Web.Services.WebService {

    DB mConnection = new DB();
    Common Fn = new Common();
    DataSet ds = new DataSet();

    public OrderInfoUpdate () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
        
    }

    [WebMethod]
    public XmlElement  UpdateOrderDetails(string data)
    {

        XmlDocument doc = new XmlDocument();
        XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", null, null);
        doc.AppendChild(dec);
        XmlElement DocRoot;
        DocRoot = doc.CreateElement("StatusObjects");
        doc.AppendChild(DocRoot);

        try
        {
            //var dummydata = "{\"order\": {\"totalSurcharge\":\"0.00\",\"tableClosedOn\":null,\"tableOpenedOn\":\"2013-01-07 14:23:24\",  \"userId\":\"22\",\"totalAmount\":\"76.36\",\"orderNo\":\"000005\",\"totalDiscount\":\"0.00\",\"paymentType\":1,\"tipAmount\":\"0.00\",\"tableNo\":\"347215_TA5\",\"orderedOn\":\"2013-01-07 15:03:09\",\"totalTax\":\"6.36\",\"noOfGuest\":\"0\",\"companyId\":\"1\",\"grossAmount\":\"70.00\",\"restaurantId\":1,\"deviceId\":\"5\",\"date\":\"2013-01-07 15:03:12\" },\"Product\" : [{\"quantity\":\"1.00\",\"amount\":\"38.18\",\"prodId\":\"32\"},{\"quantity\":\"1.00\",\"amount\":\"38.18\",\"prodId\":\"24\"},{\"quantity\":\"1.00\",\"amount\":\"38.18\",\"prodId\":\"22\"}]}";
            // var dummydata = "{\"Order\": {\"totalSurcharge\":\"0.00\",\"tableClosedOn\":null,\"tableOpenedOn\":\"2013-01-07 14:23:24\",  \"userId\":\"22\",\"totalAmount\":\"76.36\",\"ordertranID\":\"3333333\",\"orderNo\":\"000004\",\"totalDiscount\":\"0.00\",\"paymentType\":1,\"tipAmount\":\"0.00\",\"tableNo\":\"347215_TA5\",\"orderedOn\":\"2013-01-07 15:03:09\",\"totalTax\":\"6.36\",\"noOfGuest\":\"0\",\"companyId\":\"1\",\"grossAmount\":\"70.00\",\"restaurantId\":1,\"deviceId\":\"5\",\"date\":\"2013-01-07 15:03:12\" },\"PaymentInfo\":[{\"type\":\"1\",\"Amt\":\"50.00\"},{\"type\":\"2\",\"Amt\":\"50.00\"},{\"type\":\"3\",\"Amt\":\"0.00\"}],\"ProductInfo\" : [{\"quantity\":\"1.00\",\"amount\":\"38.18\",\"prodId\":\"32\"},{\"quantity\":\"1.00\",\"amount\":\"38.18\",\"prodId\":\"24\"},{\"quantity\":\"1.00\",\"amount\":\"38.18\",\"prodId\":\"22\"}]}";

            var dummydata = data;

//            HttpContext.Current.Response.Write("Data->" + data + "<br>");

            JObject Data = JObject.Parse(dummydata);

            var Orders = Data["Order"].ToString();
            var PaymentInfo = Data["PaymentInfo"].ToString();
            var Products = Data["ProductInfo"].ToString();

            string ordertranid = string.Empty;
            string orderno = string.Empty;
            string trandate = string.Empty;
            string orderedon = string.Empty;
            string tableopenedon = string.Empty;
            string tableclosedon = string.Empty;
            string tableno = string.Empty;
            string noofguest = string.Empty;
            string restid = string.Empty;
            string deviceid = string.Empty;
            string usrid = string.Empty;
            string paymenttype = string.Empty;
            string totalamount = string.Empty;
            string totSurcharge = string.Empty;
            string totDiscount = string.Empty;
            string tipAmount = string.Empty;
            string totTax = string.Empty;
            string grossAmount = string.Empty;

            JObject OrderDetails = JObject.Parse(Orders);

/*            HttpContext.Current.Response.Write("TranDate->" + OrderDetails["date"].ToString() + "<br>");
            HttpContext.Current.Response.Write("orderedOn->" + OrderDetails["orderedOn"].ToString() + "<br>");
            HttpContext.Current.Response.Write("tableOpenedOn->" + OrderDetails["tableOpenedOn"].ToString() + "<br>");
            HttpContext.Current.Response.Write("tableClosedOn->" + OrderDetails["tableClosedOn"].ToString() + "<br>");
            */

            ordertranid = OrderDetails["ordertranID"].ToString();
            orderno =  OrderDetails["orderNo"].ToString();
            trandate = OrderDetails["date"].ToString();        // 02/07/2013 01:04:00 +05:30  as Sample

            orderedon = OrderDetails["orderedOn"].ToString();
            tableopenedon = OrderDetails["tableOpenedOn"].ToString();
            tableclosedon = OrderDetails["tableClosedOn"].ToString();
            tableno =  OrderDetails["tableNo"].ToString();
            noofguest = OrderDetails["noOfGuest"].ToString();
            restid = OrderDetails["restaurantId"].ToString();
            deviceid = OrderDetails["deviceId"].ToString();
            usrid =  OrderDetails["userId"].ToString();
            //paymenttype = OrderDetails["paymentType"].ToString();
            totalamount = OrderDetails["totalAmount"].ToString();
            totSurcharge = OrderDetails["totalSurcharge"].ToString();
            totDiscount = OrderDetails["totalDiscount"].ToString();
            tipAmount = OrderDetails["tipAmount"].ToString();
            totTax = OrderDetails["totalTax"].ToString();
            grossAmount = OrderDetails["grossAmount"].ToString();

            //IList<string> Products = Data.SelectToken("Product").Select(s => (string)s).ToList();

            using (SqlConnection conn = mConnection.GetConnection())
            {
                conn.Open();

                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        string strTimeZoneID = string.Empty;

                        //uncomment following line: sometime this causes db connectivity issue
                        //strTimeZoneID = Fn.GetServerTimeZone();
                        strTimeZoneID = "Cen. Australia Standard Time";

                        if (trandate != null && trandate != "" )
                        {
                            DateTime sDate = DateTime.Now;
                            string Dt = String.Format("{0:yyyy-MM-dd hh:mm:ss}", sDate);

                            SqlParameter[] ArParams = new SqlParameter[22];
                            ArParams[0] = new SqlParameter("@OrderID", SqlDbType.VarChar, 50);
                            ArParams[0].Value = orderno;

                            ArParams[1] = new SqlParameter("@TransactionDate", SqlDbType.DateTime);
                            //ArParams[1].Value = Fn.ConvertDateIntoAnotherTimeZoneDate(trandate, strTimeZoneID);
                            ArParams[1].Value = Fn.ConvertIntoServerTime(trandate);

                            ArParams[2] = new SqlParameter("@OrderedOn", SqlDbType.DateTime);
                            ArParams[2].Value = (orderedon == null || orderedon == "") ? null : Fn.ConvertIntoServerTime(orderedon);

                            ArParams[3] = new SqlParameter("@TableId", SqlDbType.VarChar,50);
                            ArParams[3].Value = tableno;

                            ArParams[4] = new SqlParameter("@NoOfGuest", SqlDbType.Int);
                            ArParams[4].Value = noofguest;

                            ArParams[5] = new SqlParameter("@Table_OpenedOn", SqlDbType.DateTime);
                            //ArParams[5].Value = tableopenedon;
                            ArParams[5].Value = (tableopenedon == null || tableopenedon == "") ? null : Fn.ConvertIntoServerTime(tableopenedon);

                            ArParams[6] = new SqlParameter("@Table_ClosedOn", SqlDbType.DateTime);
                            //ArParams[6].Value = (tableclosedon == null || tableclosedon == "") ? null : tableclosedon;
                            ArParams[6].Value = (tableclosedon == null || tableclosedon == "") ? null : Fn.ConvertIntoServerTime(tableclosedon);

                            ArParams[7] = new SqlParameter("@Rest_id", SqlDbType.Int);
                            ArParams[7].Value = restid;

                            ArParams[8] = new SqlParameter("@UserID", SqlDbType.Int);
                            ArParams[8].Value = usrid;

                            ArParams[9] = new SqlParameter("@DeviceID", SqlDbType.Int);
                            ArParams[9].Value = deviceid;

                            ArParams[10] = new SqlParameter("@GrossAmount", SqlDbType.Decimal);
                            ArParams[10].Value = (grossAmount == null || grossAmount == "") ? "0.00" : grossAmount; 

                            ArParams[11] = new SqlParameter("@TotalTax", SqlDbType.Decimal);
                            ArParams[11].Value = (totTax == null || totTax == "") ? "0.00" : totTax; 

                            ArParams[12] = new SqlParameter("@TipAmount", SqlDbType.Decimal);
                            ArParams[12].Value = (tipAmount == null || tipAmount == "") ? "0.00" : tipAmount;

                            ArParams[13] = new SqlParameter("@Discount", SqlDbType.Decimal);
                            ArParams[13].Value = (totDiscount == null || totDiscount == "") ? "0.00" : totDiscount;

                            ArParams[14] = new SqlParameter("@Surcharge", SqlDbType.Decimal);
                            ArParams[14].Value = (totSurcharge == null || totSurcharge == "") ? "0.00" : totSurcharge;

                            ArParams[15] = new SqlParameter("@TotalAmount", SqlDbType.Decimal);
                            ArParams[15].Value = (totalamount == null || totalamount == "") ? "0.00" : totalamount;

                            ArParams[16] = new SqlParameter("@PaymentType", SqlDbType.Int);
                            ArParams[16].Value = "0";

                            ArParams[17] = new SqlParameter("@CreatedByUserID", SqlDbType.Int);
                            ArParams[17].Value =  usrid;

                            ArParams[18] = new SqlParameter("@CreatedOn", SqlDbType.DateTime);
                            ArParams[18].Value = Dt; 

                            ArParams[19] = new SqlParameter("@isPaid", SqlDbType.Bit);
                            ArParams[19].Value = true;

                            ArParams[20] = new SqlParameter("@Mode", SqlDbType.VarChar, 20);
                            ArParams[20].Value = "add";

                            ArParams[21] = new SqlParameter("@OrderTranID", SqlDbType.VarChar,50);
                            ArParams[21].Value = ordertranid;
                            
                            SqlHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, "SP_omni_OrderInfo_Update", ArParams);

                            JArray PaymentsArray = JArray.Parse(PaymentInfo);

                            for (int i = 0; i < PaymentsArray.Count; i++)
                            {
                                string orderid = string.Empty;
                                string paymenttypeid = string.Empty;
                                string paidamt = string.Empty;

                                orderid = orderno;
                                paymenttypeid = PaymentsArray[i]["type"].ToString();
                                paidamt = PaymentsArray[i]["Amount"].ToString();

                                SqlParameter[] ArParamsO = new SqlParameter[4];
                                ArParamsO[0] = new SqlParameter("@OrderTranID", SqlDbType.VarChar, 50);
                                ArParamsO[0].Value = ordertranid;

                                ArParamsO[1] = new SqlParameter("@PaymentTypeID", SqlDbType.Int);
                                ArParamsO[1].Value = paymenttypeid;

                                ArParamsO[2] = new SqlParameter("@PaidAmount", SqlDbType.Decimal);
                                ArParamsO[2].Value = paidamt;

                                ArParamsO[3] = new SqlParameter("@TranDate", SqlDbType.DateTime);
                                ArParamsO[3].Value = Dt;
                                
                                if (paymenttypeid !="" && Convert.ToDecimal(paidamt) >0)
                                    SqlHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, "SP_omni_Order_PaymentInfo_Update", ArParamsO);
                            }

                            JArray ProductsArray = JArray.Parse(Products);
                            
                            for (int i = 0; i < ProductsArray.Count; i++)
                            {
                                string productid = ProductsArray[i]["prodId"].ToString();

                                //we need to check whether or not the product id is valid; sometimes users add an open item which is not present in the current DB;
                                //we need to add other logics to add open items and show them in the report in future; for the time being stop it in the following way
                                //Note: Assuming the productId is of integer type;
                                int pid = 0;
                                bool isProductIdValid = int.TryParse(productid, out pid);

                                if (productid.Length > 0 && pid == 0)
                                {
                                    isProductIdValid = true;
                                    productid = "0";
                                }

                                if (isProductIdValid)
                                {
                                    string amount = string.Empty;
                                    string qty = string.Empty;
                                    string modifierids = string.Empty;

                                    //productid = ProductsArray[i]["prodId"].ToString();
                                    amount = ProductsArray[i]["Amount"].ToString();
                                    qty = ProductsArray[i]["quantity"].ToString();
                                    modifierids = ProductsArray[i]["modifiers"].ToString();

                                    SqlParameter[] ArParamsT = new SqlParameter[5];
                                    ArParamsT[0] = new SqlParameter("@OrderTranID", SqlDbType.VarChar, 50);
                                    ArParamsT[0].Value = ordertranid;

                                    ArParamsT[1] = new SqlParameter("@ProductID", SqlDbType.Int);
                                    ArParamsT[1].Value = productid;

                                    ArParamsT[2] = new SqlParameter("@Qty", SqlDbType.Decimal);
                                    ArParamsT[2].Value = qty;

                                    ArParamsT[3] = new SqlParameter("@Amount", SqlDbType.Decimal);
                                    ArParamsT[3].Value = amount;

                                    ArParamsT[4] = new SqlParameter("@modifiers", SqlDbType.VarChar, 50);
                                    ArParamsT[4].Value = modifierids;

                                    if (productid != "" && Convert.ToDecimal(amount) > 0)
                                        SqlHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, "SP_omni_Order_ProductInfo_Update", ArParamsT);
                                }
                            }
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

            XmlNode StatusInfo2 = doc.CreateElement("StatusMessage");
            StatusInfo2.InnerText = e.Message.ToString();
            DocRoot.AppendChild(StatusInfo2);
            return DocRoot;
        }

    }
    
}

