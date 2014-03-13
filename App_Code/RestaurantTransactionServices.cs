using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Xml;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using MyDB;
using Commons;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

/// <summary>
/// Provides service to add restaurant transactions such as order/payment trsaction, payouts, refunds etc. 
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class RestaurantTransactionServices : System.Web.Services.WebService {

    DB mConnection = new DB();
    Common Fn = new Common();
    DataSet ds = new DataSet();

    public RestaurantTransactionServices () {

    }

    /*
     * Adds an order trasaction. An order transaction contains other trasactions such as payment;
     * It also adds number of sold items record. The amount will be increased to the current sale
     */
    [WebMethod]
    public XmlElement addOrderTransaction(string data)
    {

        XmlDocument doc = new XmlDocument();
        XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", null, null);
        doc.AppendChild(dec);
        XmlElement DocRoot;
        DocRoot = doc.CreateElement("StatusObjects");
        doc.AppendChild(DocRoot);

        try
        {
            var dummydata = data;

            JObject Data = JObject.Parse(dummydata);

            var Orders = Data["Order"].ToString();
            var PaymentInfo = Data["PaymentInfo"].ToString();
            var Products = Data["ProductInfo"].ToString();

            string ordertranid = string.Empty, orderno = string.Empty, trandate = string.Empty, orderedon = string.Empty;
            string tableopenedon = string.Empty, tableclosedon = string.Empty, tableno = string.Empty, noofguest = string.Empty;
            string restid = string.Empty, deviceid = string.Empty, usrid = string.Empty, paymenttype = string.Empty;
            string totalamount = string.Empty, totSurcharge = string.Empty, totDiscount = string.Empty, tipAmount = string.Empty;
            string totTax = string.Empty, grossAmount = string.Empty, customerId = string.Empty;

            JObject OrderDetails = JObject.Parse(Orders);

            ordertranid = OrderDetails["ordertranID"].ToString();
            orderno = OrderDetails["orderNo"].ToString();
            trandate = OrderDetails["date"].ToString();        // 02/07/2013 01:04:00 +05:30  as Sample

            orderedon = OrderDetails["orderedOn"].ToString();
            tableopenedon = OrderDetails["tableOpenedOn"].ToString();
            tableclosedon = OrderDetails["tableClosedOn"].ToString();
            tableno = OrderDetails["tableNo"].ToString();
            noofguest = OrderDetails["noOfGuest"].ToString();
            restid = OrderDetails["restaurantId"].ToString();
            deviceid = OrderDetails["deviceId"].ToString();
            usrid = OrderDetails["userId"].ToString();
            //paymenttype = OrderDetails["paymentType"].ToString();
            totalamount = OrderDetails["totalAmount"].ToString();
            totSurcharge = OrderDetails["totalSurcharge"].ToString();
            totDiscount = OrderDetails["totalDiscount"].ToString();
            tipAmount = OrderDetails["tipAmount"].ToString();
            totTax = OrderDetails["totalTax"].ToString();
            grossAmount = OrderDetails["grossAmount"].ToString();
            customerId = OrderDetails["customerID"].ToString();

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

                        if (trandate != null && trandate != "")
                        {
                            DateTime sDate = DateTime.Now;
                            string Dt = String.Format("{0:yyyy-MM-dd hh:mm:ss}", sDate);

                            SqlParameter[] ArParams = new SqlParameter[23];
                            ArParams[0] = new SqlParameter("@OrderID", SqlDbType.VarChar, 50);
                            ArParams[0].Value = orderno;

                            ArParams[1] = new SqlParameter("@TransactionDate", SqlDbType.DateTime);
                            //ArParams[1].Value = Fn.ConvertDateIntoAnotherTimeZoneDate(trandate, strTimeZoneID);
                            ArParams[1].Value = Fn.ConvertIntoServerTime(trandate);

                            ArParams[2] = new SqlParameter("@OrderedOn", SqlDbType.DateTime);
                            ArParams[2].Value = (orderedon == null || orderedon == "") ? null : Fn.ConvertIntoServerTime(orderedon);

                            ArParams[3] = new SqlParameter("@TableId", SqlDbType.VarChar, 50);
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
                            ArParams[17].Value = usrid;

                            ArParams[18] = new SqlParameter("@CreatedOn", SqlDbType.DateTime);
                            ArParams[18].Value = Dt;

                            ArParams[19] = new SqlParameter("@isPaid", SqlDbType.Bit);
                            ArParams[19].Value = true;

                            ArParams[20] = new SqlParameter("@Mode", SqlDbType.VarChar, 20);
                            ArParams[20].Value = "add";

                            ArParams[21] = new SqlParameter("@OrderTranID", SqlDbType.VarChar, 50);
                            ArParams[21].Value = ordertranid;

                            ArParams[22] = new SqlParameter("@CustomerID", SqlDbType.Int);
                            ArParams[22].Value = customerId;

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

                                if (paymenttypeid != "" && Convert.ToDecimal(paidamt) > 0)
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

    [WebMethod]
    /*
     * Adds refund transaction; refund is a trasaction which is performed in the 
     * restaurant to refund some amount to customers; the amount will be decreased from the total sale
     */
    public XmlElement addRefundTrasaction(string data)
    {
        XmlDocument doc = new XmlDocument();
        XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", null, null);
        doc.AppendChild(dec);
        XmlElement DocRoot;
        DocRoot = doc.CreateElement("StatusObjects");
        doc.AppendChild(DocRoot);

        try
        {
            var dummydata = data;

            string paymenttype = string.Empty, restid = string.Empty, comments = string.Empty, amount = string.Empty;
            string trandate = string.Empty, deviceid = string.Empty, prodID = string.Empty, RefundTranID = string.Empty;
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
            usrid = Data["userId"].ToString();

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


    /*
    * Adds payout transaction; payout is a trasaction which is performed in the 
    * restaurant to pay some amount to customers or suppliers; the amount will be decreased from the total sale
    */
    [WebMethod]
    public XmlElement addPayoutTrasaction(string data)
    {
        XmlDocument doc = new XmlDocument();
        XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", null, null);
        doc.AppendChild(dec);
        XmlElement DocRoot;
        DocRoot = doc.CreateElement("StatusObjects");
        doc.AppendChild(DocRoot);

        try
        {
            var dummydata = data;

            string paymenttype = string.Empty, restid = string.Empty, comments = string.Empty, amount = string.Empty, trandate = string.Empty;
            string deviceid = string.Empty, PayoutTranID = string.Empty, usrid = string.Empty;

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
            usrid = Data["userId"].ToString();

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

            XmlNode StatusInfo = doc.CreateElement("Status");
            StatusInfo.InnerText = "True";
            DocRoot.AppendChild(StatusInfo);
            return DocRoot;
        }
        catch (Exception e)
        {
            XmlNode StatusInfo = doc.CreateElement("Status");
            StatusInfo.InnerText = "False";
            DocRoot.AppendChild(StatusInfo);
            return DocRoot;
        }
    }

    /*
   * Adds no-sale count transaction; 
   */
    [WebMethod]
    public XmlElement addNoSaleTrasaction(string data)
    {
        XmlDocument doc = new XmlDocument();
        XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", null, null);
        doc.AppendChild(dec);
        XmlElement DocRoot;
        DocRoot = doc.CreateElement("StatusObjects");
        doc.AppendChild(DocRoot);

        try
        {
            var dummydata = data;

            string restid = string.Empty, note = string.Empty, date = string.Empty;
            string deviceid = string.Empty, tranID = string.Empty, usrid = string.Empty, count = string.Empty ;

            JObject NoSales = JObject.Parse(dummydata);

            var Payout_Data = NoSales["NoSale"].ToString();

            JObject Data = JObject.Parse(Payout_Data);

            restid = Data["restaurantId"].ToString();
            note = Data["note"].ToString();
            count = Data["count"].ToString();
            date = Data["date"].ToString();
            deviceid = Data["deviceId"].ToString();
            usrid = Data["userId"].ToString();
            tranID = Data["ID"].ToString();

            using (SqlConnection conn = mConnection.GetConnection())
            {
                conn.Open();

                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        if (date != null && date != "")
                        {
                            DateTime sDate = DateTime.Now;
                            string Dt = String.Format("{0:yyyy-MM-dd hh:mm:ss}", sDate);

                            SqlParameter[] ArParams = new SqlParameter[7];
                            ArParams[0] = new SqlParameter("@TranID", SqlDbType.VarChar, 50);
                            ArParams[0].Value = tranID;

                            ArParams[1] = new SqlParameter("@Date", SqlDbType.DateTime);
                            ArParams[1].Value = Fn.ConvertIntoServerTime(date);

                            ArParams[2] = new SqlParameter("@DeviceID", SqlDbType.Int);
                            ArParams[2].Value = deviceid;

                            ArParams[3] = new SqlParameter("@Note", SqlDbType.VarChar, 500);
                            ArParams[3].Value = note;

                            ArParams[4] = new SqlParameter("@Rest_ID", SqlDbType.Int);
                            ArParams[4].Value = restid;

                            ArParams[5] = new SqlParameter("@UserID", SqlDbType.Int);
                            ArParams[5].Value = usrid;

                            ArParams[6] = new SqlParameter("@Count", SqlDbType.Int);
                            ArParams[6].Value = count;

                            SqlHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, "SP_omni_NoSale_Update", ArParams);

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

            XmlNode StatusInfo = doc.CreateElement("Status");
            StatusInfo.InnerText = "True";
            DocRoot.AppendChild(StatusInfo);
            return DocRoot;
        }
        catch (Exception e)
        {
            XmlNode StatusInfo = doc.CreateElement("Status");
            StatusInfo.InnerText = "False";
            DocRoot.AppendChild(StatusInfo);
            return DocRoot;
        }
    }

    /*
    * Adds user attendance; 
  */
    [WebMethod]
    public XmlElement addUserAttendance(string data)
    {
        XmlDocument doc = new XmlDocument();
        XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", null, null);
        doc.AppendChild(dec);
        XmlElement DocRoot;
        DocRoot = doc.CreateElement("StatusObjects");
        doc.AppendChild(DocRoot);

        try
        {
            var dummydata = data;

            string restid = string.Empty, date = string.Empty, userStatus = string.Empty;
            string loginTime = string.Empty, tranID = string.Empty, usrid = string.Empty, logoutTime = string.Empty;

            JObject NoSales = JObject.Parse(dummydata);

            var User_Data = NoSales["UserAttendance"].ToString();

            JObject Data = JObject.Parse(User_Data);

            restid = Data["restaurantId"].ToString();
            userStatus = Data["userStatus"].ToString();
            logoutTime = Data["logoutTime"].ToString();
            date = Data["date"].ToString();
            loginTime = Data["loginTime"].ToString();
            usrid = Data["userId"].ToString();
            tranID = Data["ID"].ToString();

            using (SqlConnection conn = mConnection.GetConnection())
            {
                conn.Open();

                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        if (date != null && date != "")
                        {
                            DateTime sDate = DateTime.Now;
                            string Dt = String.Format("{0:yyyy-MM-dd hh:mm:ss}", sDate);

                            SqlParameter[] ArParams = new SqlParameter[7];
                            ArParams[0] = new SqlParameter("@TranID", SqlDbType.VarChar, 50);
                            ArParams[0].Value = tranID;

                            ArParams[1] = new SqlParameter("@Date", SqlDbType.DateTime);
                            ArParams[1].Value = Fn.ConvertIntoServerTime(date);

                            ArParams[2] = new SqlParameter("@LoginTime", SqlDbType.DateTime);

                            if (userStatus.Equals("Login"))
                                ArParams[2].Value = Fn.ConvertIntoServerTime(loginTime);
                            else
                                ArParams[2].Value = null;

                            ArParams[3] = new SqlParameter("@LogoutTime", SqlDbType.DateTime);

                            if (userStatus.Equals("Logout"))
                                ArParams[3].Value = Fn.ConvertIntoServerTime(logoutTime);
                            else
                                ArParams[3].Value = null;

                            ArParams[4] = new SqlParameter("@Rest_ID", SqlDbType.Int);
                            ArParams[4].Value = restid;

                            ArParams[5] = new SqlParameter("@UserID", SqlDbType.Int);
                            ArParams[5].Value = usrid;

                            ArParams[6] = new SqlParameter("@Status", SqlDbType.VarChar, 50);
                            ArParams[6].Value = userStatus;

                            SqlHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, "SP_omni_Add_User_Attendance", ArParams);

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

            XmlNode StatusInfo = doc.CreateElement("Status");
            StatusInfo.InnerText = "True";
            DocRoot.AppendChild(StatusInfo);
            return DocRoot;
        }
        catch (Exception e)
        {
            XmlNode StatusInfo = doc.CreateElement("Status");
            StatusInfo.InnerText = "False";
            DocRoot.AppendChild(StatusInfo);
            return DocRoot;
        }
    }    
}
