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

public class checkReportInfo : System.Web.Services.WebService
{

    public checkReportInfo()
    {
        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod (EnableSession=true) ]

    public XmlElement checkingReportDetails(string data)
    {
        DB mConnection = new DB();
        Common Fn = new Common();
        SQLQuery Qry = new SQLQuery();
        DataSet ds = new DataSet();


        Dictionary<string, string> dict = null;
        string StrCurrency = string.Empty;

        decimal TipAmount = 0;
        decimal TaxAmt = 0;
        decimal SurCharge = 0;
        decimal Discount = 0;
        decimal TotalAmount = 0;
        decimal CashSale = 0;
        decimal CardSale = 0;
        decimal VoucherSale = 0;
        decimal TotalGrossAmt = 0;
        decimal TotalNetAmt = 0;
        decimal TotalRefundAmt = 0;
        decimal TotalPayoutAmt = 0;
        decimal TotalInDrawerAmt = 0;
        decimal TotalFloatAmt = 0;
        SqlDataReader OrderedProductModifiersReader = null;
        SqlDataReader SaleInfoReader = null;
        SqlDataReader TotalSaleByPaymentTypeReader = null;
        SqlDataReader RefundAmountReader = null;
        SqlDataReader PayoutAmountReader = null;
        SqlDataReader TotalDrawerAmtReader = null;

        try
        {
            XmlDocument doc = new XmlDocument();
            XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", null,null);
            doc.AppendChild(dec);
            XmlElement DocRoot=null;
            DocRoot = doc.CreateElement("CheckReportInfo");
            doc.AppendChild(DocRoot);

            var dummydata = data;

            JObject Data = JObject.Parse(dummydata);
            var ParamDetails = Data["checkReportInfo"].ToString();
            JObject ReportDetails = JObject.Parse(ParamDetails);

            string fromdate = ReportDetails["FromDate"].ToString();
            string tilldate = ReportDetails["TillDate"].ToString();
            string rest_Id = ReportDetails["restaurantId"].ToString();


            using (SqlConnection conn = mConnection.GetConnection())
            {
                conn.Open();

                StrCurrency = Fn.GetTableColumnValue(dict, "omni_CompanyInfo", "CurrencySymbol", "", "",conn);

                //using (SqlTransaction trans = conn.BeginTransaction())
                //{
                try
                {
                    if (fromdate != "" && Fn.ValidateDate(fromdate))
                        fromdate = String.Format("{0:yyyy-MM-dd}", Fn.ConvertDateIntoAnotherFormat2(fromdate));

                    if (tilldate != "" && Fn.ValidateDate(tilldate))
                        tilldate = String.Format("{0:yyyy-MM-dd}", Fn.ConvertDateIntoAnotherFormat2(tilldate));
                    
                    dict = new Dictionary<string, string>() { { "Rest_ID", rest_Id } };

                    SaleInfoReader = SqlHelper.ExecuteReader(conn, CommandType.Text, Qry.GetSaleInfoForXReportSQL(dict, fromdate, tilldate));

                    while (SaleInfoReader.Read())
                    {
                        TipAmount = (decimal)SaleInfoReader["TipAmount"];
                        TaxAmt = (decimal)SaleInfoReader["TotalTax"];
                        SurCharge = (decimal)SaleInfoReader["Surcharge"];
                        Discount = (decimal)SaleInfoReader["Discount"];
                        TotalAmount = (decimal)SaleInfoReader["TotalAmount"];
                    }

                    SaleInfoReader.Close();
                    
                    /****************************************************************************/

                    dict = new Dictionary<string, string>() { { "Rest_ID", rest_Id }, { "b.PaymentTypeID", "1" } };
                    TotalSaleByPaymentTypeReader = SqlHelper.ExecuteReader(conn, CommandType.Text, Qry.GetTotalSaleByPaymentTypeForXReportSQL(dict, fromdate, tilldate));

                    while (TotalSaleByPaymentTypeReader.Read())
                    {
                        CashSale = (decimal)TotalSaleByPaymentTypeReader["PaidAmount"];
                    }

                    TotalSaleByPaymentTypeReader.Close();

                    dict = new Dictionary<string, string>() { { "Rest_ID", rest_Id }, { "b.PaymentTypeID", "2" } };
                    TotalSaleByPaymentTypeReader = SqlHelper.ExecuteReader(conn, CommandType.Text, Qry.GetTotalSaleByPaymentTypeForXReportSQL(dict, fromdate, tilldate));

                    while (TotalSaleByPaymentTypeReader.Read())
                    {
                        CardSale = (decimal)TotalSaleByPaymentTypeReader["PaidAmount"];
                    }

                    TotalSaleByPaymentTypeReader.Close();

                    dict = new Dictionary<string, string>() { { "Rest_ID", rest_Id }, { "b.PaymentTypeID", "3" } };
                    TotalSaleByPaymentTypeReader = SqlHelper.ExecuteReader(conn, CommandType.Text, Qry.GetTotalSaleByPaymentTypeForXReportSQL(dict, fromdate, tilldate));

                    while (TotalSaleByPaymentTypeReader.Read())
                    {
                        VoucherSale = (decimal)TotalSaleByPaymentTypeReader["PaidAmount"];
                    }

                    TotalSaleByPaymentTypeReader.Close();

                    TotalGrossAmt = (CashSale + CardSale + VoucherSale + SurCharge) - Discount;
                    TotalNetAmt = TotalGrossAmt - TaxAmt;

                    dict = new Dictionary<string, string>() { { "Rest_ID", rest_Id } };
                    RefundAmountReader = SqlHelper.ExecuteReader(conn, CommandType.Text, Qry.GetTotalRefundForXReportSQL(dict, fromdate, tilldate));

                    while (RefundAmountReader.Read())
                    {
                        TotalRefundAmt = (decimal)RefundAmountReader["Amount"];
                    }
                    RefundAmountReader.Close();

                    dict = new Dictionary<string, string>() { { "Rest_ID", rest_Id } };
                    PayoutAmountReader = SqlHelper.ExecuteReader(conn, CommandType.Text, Qry.GetTotalPayoutForXReportSQL(dict, fromdate, tilldate));

                    while (PayoutAmountReader.Read())
                    {
                        TotalPayoutAmt = (decimal)PayoutAmountReader["Amount"];
                    }
                    PayoutAmountReader.Close();

                    TotalInDrawerAmt = (TotalNetAmt + TotalFloatAmt + TipAmount) - (TotalRefundAmt + TotalPayoutAmt);

                    XmlNode From_Date = doc.CreateElement("FromDate");
                    From_Date.InnerText = fromdate;
                    DocRoot.AppendChild(From_Date);

                    XmlNode Till_Date = doc.CreateElement("TillDate");
                    Till_Date.InnerText = tilldate;
                    DocRoot.AppendChild(Till_Date);

                    XmlNode TotalCashSale = doc.CreateElement("CashSale");
                    TotalCashSale.InnerText = CashSale.ToString();
                    DocRoot.AppendChild(TotalCashSale);

                    XmlNode TotalCardSale = doc.CreateElement("CardSale");
                    TotalCardSale.InnerText = CardSale.ToString();
                    DocRoot.AppendChild(TotalCardSale);

                    XmlNode TotalVoucherSale = doc.CreateElement("VoucherSale");
                    TotalVoucherSale.InnerText = VoucherSale.ToString();
                    DocRoot.AppendChild(TotalVoucherSale);

                    XmlNode TotalRefundAmount = doc.CreateElement("RefundAmount");
                    TotalRefundAmount.InnerText = TotalRefundAmt.ToString();
                    DocRoot.AppendChild(TotalRefundAmount);

                    XmlNode TotalPayoutAmount = doc.CreateElement("PayoutAmount");
                    TotalPayoutAmount.InnerText = TotalPayoutAmt.ToString();
                    DocRoot.AppendChild(TotalPayoutAmount);

                    XmlNode TipAmt = doc.CreateElement("TipAmount");
                    TipAmt.InnerText = TipAmount.ToString();
                    DocRoot.AppendChild(TipAmt);

                    XmlNode TaxAmount = doc.CreateElement("TaxAmount");
                    TaxAmount.InnerText = TaxAmt.ToString();
                    DocRoot.AppendChild(TaxAmount);

                    XmlNode SurChargeAmount = doc.CreateElement("SurChargeAmount");
                    SurChargeAmount.InnerText = SurCharge.ToString();
                    DocRoot.AppendChild(SurChargeAmount);

                    XmlNode DiscountAmount = doc.CreateElement("DiscountAmount");
                    DiscountAmount.InnerText = Discount.ToString();
                    DocRoot.AppendChild(DiscountAmount);

                    XmlNode TotalNetAmount = doc.CreateElement("TotalNetAmount");
                    TotalNetAmount.InnerText = TotalNetAmt.ToString();
                    DocRoot.AppendChild(TotalNetAmount);

                    XmlNode TotalGrossAmount = doc.CreateElement("GrossAmount");
                    TotalGrossAmount.InnerText = TotalGrossAmt.ToString();
                    DocRoot.AppendChild(TotalGrossAmount);

                    XmlNode DrawerObject = doc.CreateElement("DrawerObjects");
                    DocRoot.AppendChild(DrawerObject);

                    dict = new Dictionary<string, string>() { { "a.Rest_ID", rest_Id } };
                    TotalDrawerAmtReader = SqlHelper.ExecuteReader(conn, CommandType.Text, Qry.GetTotalInDrawerForXReportSQL(dict, fromdate, tilldate));

                    if (TotalDrawerAmtReader.HasRows)
                    {
                        while (TotalDrawerAmtReader.Read())
                        {
                            decimal drawcashsale = 0;
                            decimal drawcardsale = 0;
                            decimal drawvchsale = 0;
                            decimal payout = 0;
                            decimal refund = 0;
                            decimal drawerinhand = 0;

                            drawcashsale = (decimal)TotalDrawerAmtReader["CashSale"];
                            drawcardsale = (decimal)TotalDrawerAmtReader["CardSale"];
                            drawvchsale = (decimal)TotalDrawerAmtReader["VoucherSale"];

                            payout = (decimal)TotalDrawerAmtReader["PayOut"];
                            refund = (decimal)TotalDrawerAmtReader["Refund"];

                            drawcashsale = drawcashsale - (payout + refund);
                            drawerinhand = drawcashsale;

                            XmlNode DrawerInfo = doc.CreateElement("DrawerInfo");
                            DrawerObject.AppendChild(DrawerInfo);

                            XmlNode PrinterID = doc.CreateElement("ID");
                            PrinterID.InnerText = TotalDrawerAmtReader["deviceid"].ToString();
                            DrawerInfo.AppendChild(PrinterID);

                            XmlNode PrinterName = doc.CreateElement("Name");
                            PrinterName.InnerText = TotalDrawerAmtReader["printername"].ToString();
                            DrawerInfo.AppendChild(PrinterName);

                            XmlNode DrawerInHand = doc.CreateElement("Amount");
                            DrawerInHand.InnerText = String.Format("{0:0.00}", drawerinhand);
                            DrawerInfo.AppendChild(DrawerInHand);
                        }
                    }

                    TotalDrawerAmtReader.Close();


                    XmlNode EmployeeDetails = doc.CreateElement("EmployeeDetails");
                    DocRoot.AppendChild(EmployeeDetails);

                    dict = new Dictionary<string, string>() { { "Rest_ID", rest_Id} };
                    ds = Fn.LoadEmployeeBreakUpSaleForZReport(dict, fromdate, tilldate,conn);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            XmlNode EmployeeInfo = doc.CreateElement("EmployeeInfo");
                            EmployeeDetails.AppendChild(EmployeeInfo);

                            XmlNode UserID = doc.CreateElement("ID");
                            UserID.InnerText = dr["UserID"].ToString();
                            EmployeeInfo.AppendChild(UserID);

                            XmlNode Name = doc.CreateElement("Name");
                            Name.InnerText = dr["Employee"].ToString();
                            EmployeeInfo.AppendChild(Name);

                            XmlNode Employee_CashSale = doc.CreateElement("CashSale");
                            Employee_CashSale.InnerText = dr["CashSale"].ToString();
                            EmployeeInfo.AppendChild(Employee_CashSale);

                            XmlNode Employee_CardSale = doc.CreateElement("CardSale");
                            Employee_CardSale.InnerText = dr["CardSale"].ToString();
                            EmployeeInfo.AppendChild(Employee_CardSale);

                            XmlNode Employee_VoucherSale = doc.CreateElement("VoucherSale");
                            Employee_VoucherSale.InnerText = dr["VoucherSale"].ToString();
                            EmployeeInfo.AppendChild(Employee_VoucherSale);
                        }
                    }
                    /*else
                    {
                        XmlNode EmployeeInfo = doc.CreateElement("EmployeeInfo");
                        EmployeeInfo.InnerText = "No Data";
                        DocRoot.AppendChild(EmployeeInfo);
                    }*/

                    ds.Dispose();

                    XmlNode CategoryDetails = doc.CreateElement("CategoryDetails");
                    DocRoot.AppendChild(CategoryDetails);

                    dict = new Dictionary<string, string>() { { "a.Rest_ID" , rest_Id } };
                    ds = Fn.LoadCategoryBreakUpSaleForZReport(dict, fromdate, tilldate,conn);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            XmlNode CategoryInfo = doc.CreateElement("CategoryInfo");
                            CategoryDetails.AppendChild(CategoryInfo);

                            XmlNode ID = doc.CreateElement("ID");
                            ID.InnerText = dr["CategoryID"].ToString();
                            CategoryInfo.AppendChild(ID);

                            XmlNode CategoryName = doc.CreateElement("Name");
                            CategoryName.InnerText = dr["CategoryName"].ToString();
                            CategoryInfo.AppendChild(CategoryName);

                            XmlNode Category_Amount = doc.CreateElement("Amount");
                            Category_Amount.InnerText = dr["Amount"].ToString();
                            CategoryInfo.AppendChild(Category_Amount);
                        }
                    }
                    /*else
                    {
                        XmlNode CategoryInfo = doc.CreateElement("CategoryInfo");
                        CategoryInfo.InnerText = "No Data";
                        DocRoot.AppendChild(CategoryInfo);
                    }*/

                    ds.Dispose();

                    XmlNode ProductDetails = doc.CreateElement("ProductDetails");
                    DocRoot.AppendChild(ProductDetails);


                    dict = new Dictionary<string, string>() { { "a.Rest_ID", rest_Id } };
                    ds = Fn.LoadProductBreakUpSaleForZReport(dict, fromdate, tilldate,conn);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            XmlNode ProductInfo = doc.CreateElement("ProductInfo");
                            ProductDetails.AppendChild(ProductInfo);

                            XmlNode ID = doc.CreateElement("ID");
                            ID.InnerText = dr["ProductID"].ToString();
                            ProductInfo.AppendChild(ID);

                            XmlNode ProductName = doc.CreateElement("Name");
                            ProductName.InnerText = dr["ProductName"].ToString();
                            ProductInfo.AppendChild(ProductName);

                            XmlNode Product_Amount = doc.CreateElement("Amount");
                            Product_Amount.InnerText = dr["Amount"].ToString();
                            ProductInfo.AppendChild(Product_Amount);
                        }
                    }
                    /*else
                    {
                        XmlNode ProductInfo = doc.CreateElement("ProductInfo");
                        ProductInfo.InnerText = "No Data";
                        DocRoot.AppendChild(ProductInfo);
                    }*/

                    ds.Dispose();

                    XmlNode ModifierDetails = doc.CreateElement("ModifierDetails");
                    DocRoot.AppendChild(ModifierDetails);

                    dict = new Dictionary<string, string>() { { "a.Rest_ID", rest_Id } };

                    using (OrderedProductModifiersReader = SqlHelper.ExecuteReader(conn, CommandType.Text, Qry.GetProductOptionBreakUpSaleForZReportSQL(dict, fromdate, tilldate)))
                    {

                        string ordamt = string.Empty;
                        string ordqty = string.Empty;
                        string ordprodmodifiers = string.Empty;
                        //string ordprodmodifierid = string.Empty;
                        string bckcolor = string.Empty;
                        //string[] ordprodmodifierid = null;
                        List<string> ordprodmodifierid = new List<string>();

                        if (OrderedProductModifiersReader.HasRows)
                        {
                            int k = 0;

                            while (OrderedProductModifiersReader.Read())
                            {
                                ordqty = OrderedProductModifiersReader["Qty"].ToString();
                                ordprodmodifiers = OrderedProductModifiersReader["Modifiers"].ToString();
                                
                                string[] ordprodmodifierids = ordprodmodifiers.Split('|');

                                if (ordprodmodifierids.Length >= 0)
                                {
                                    for (int i = 0; i < ordprodmodifierids.Length; i++)
                                    {
                                        if (ordprodmodifierids[i].Trim() != "")
                                        {
                                            //ordprodmodifierid[k] = ordprodmodifierids[i].ToString();
                                            ordprodmodifierid.Add(ordprodmodifierids[i]);
                                            k++;
                                            }
                                    }
                                }
                            }
                        }
                        /* else
                         {
                             XmlNode ModiferInfo = doc.CreateElement("ModiferInfo");
                             ModiferInfo.InnerText = "No Data";
                             DocRoot.AppendChild(ModiferInfo);
                         }
                         */

                        OrderedProductModifiersReader.Close();

                        for (int i = 0; i < ordprodmodifierid.Count; i++)
                        {
                            string modifierid = ordprodmodifierid[i];
                            dict = new Dictionary<string, string>() { { "Rest_ID", rest_Id } };
                            string modifiername = Fn.GetTableColumnValue(dict, "omni_Modifiers", "ModifierName", "ModifierID", modifierid, conn);
                            ordamt = Fn.GetTableColumnValue(dict, "omni_Modifiers", "Price1", "ModifierID", modifierid, conn);

                            if (modifiername != "" && modifiername != null)
                            {
                                XmlNode ModiferInfo = doc.CreateElement("ModiferInfo");
                                ModifierDetails.AppendChild(ModiferInfo);

                                XmlNode ID = doc.CreateElement("ID");
                                ID.InnerText = modifierid;
                                ModiferInfo.AppendChild(ID);

                                XmlNode ModifierName = doc.CreateElement("Name");
                                ModifierName.InnerText = modifiername.ToString() + "(" + ordqty + ")";
                                ModiferInfo.AppendChild(ModifierName);

                                XmlNode OrderedAmount = doc.CreateElement("Amount");
                                OrderedAmount.InnerText = ordamt;
                                ModiferInfo.AppendChild(OrderedAmount);
                            }

                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                finally
                {
                    conn.Close();
                }
                    return DocRoot;
                //}
            }
        }
        
        catch (Exception e)
        {
            HttpContext.Current.Response.Write(e.Message.ToString());
            return null;
        }

    }

}

