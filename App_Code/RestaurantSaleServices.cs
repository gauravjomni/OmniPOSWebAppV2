using System;
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
using MyQuery;
using System.IO;
using System.Text;
using MailTools;

/// <summary>
/// Summary description for RestaurantSaleServices
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class RestaurantSaleServices : System.Web.Services.WebService {


    DB mConnection = new DB();
    Common Fn = new Common();
    SQLQuery Qry = new SQLQuery();
    DataSet ds = new DataSet();

    Dictionary<string, string> dict = null;
    string StrCurrency = string.Empty;

    public RestaurantSaleServices () {

    }

    /*
        This method returns a restaurant's basic sale detail and sold items (Subcat and products) report for a certain date range; 
     *  restID represents ID of a particular restaurant (company) location.
        Format: XML
     */
    [WebMethod]
    public XmlElement getBasicAndCategorySalesReport(string fromdate, string tilldate, string rest_Id)
    {
        decimal TipAmount = 0, TaxAmt = 0, SurCharge = 0, Discount = 0, TotalAmount = 0, CashSale = 0, CardSale = 0, VoucherSale = 0, TotalGrossAmt = 0;
        decimal TotalNetAmt = 0, TotalRefundAmt = 0, TotalPayoutAmt = 0, TotalInDrawerAmt = 0, TotalFloatAmt = 0;

        SqlDataReader SaleInfoReader = null;

        try
        {
            XmlDocument doc = new XmlDocument();
            XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", null, null);
            doc.AppendChild(dec);
            XmlElement DocRoot = null;
            DocRoot = doc.CreateElement("BasicReport");
            doc.AppendChild(DocRoot);

            using (SqlConnection conn = mConnection.GetConnection())
            {
                conn.Open();

                StrCurrency = Fn.GetTableColumnValue(dict, "omni_CompanyInfo", "CurrencySymbol", "", "", conn);

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

                    OPSaleFinder sf = new OPSaleFinder();

                    CashSale = sf.getTotalSaleForPaymentType("1", fromdate, tilldate, rest_Id, conn);
                    CardSale = sf.getTotalSaleForPaymentType("2", fromdate, tilldate, rest_Id, conn);
                    VoucherSale = sf.getTotalSaleForPaymentType("3", fromdate, tilldate, rest_Id, conn);

                    TotalGrossAmt = (CashSale + CardSale + VoucherSale + SurCharge) - Discount;
                    TotalNetAmt = TotalGrossAmt - TaxAmt;

                    TotalRefundAmt = sf.getTotalRefundOrPayout(true, fromdate, tilldate, rest_Id, conn);
                    TotalPayoutAmt = sf.getTotalRefundOrPayout(false, fromdate, tilldate, rest_Id, conn);

                    TotalInDrawerAmt = (TotalNetAmt + TotalFloatAmt + TipAmount) - (TotalRefundAmt + TotalPayoutAmt);

                    DocRoot.AppendChild(XMLNodeCreator.xmlNodeForElement("cash_sale", CashSale.ToString(), doc));
                    DocRoot.AppendChild(XMLNodeCreator.xmlNodeForElement("card_sale", CardSale.ToString(), doc));
                    DocRoot.AppendChild(XMLNodeCreator.xmlNodeForElement("voucher_sale", VoucherSale.ToString(), doc));
                    DocRoot.AppendChild(XMLNodeCreator.xmlNodeForElement("total_refund", TotalRefundAmt.ToString(), doc));
                    DocRoot.AppendChild(XMLNodeCreator.xmlNodeForElement("total_payout", TotalPayoutAmt.ToString(), doc));
                    DocRoot.AppendChild(XMLNodeCreator.xmlNodeForElement("total_tips", TipAmount.ToString(), doc));
                    DocRoot.AppendChild(XMLNodeCreator.xmlNodeForElement("total_tax", TaxAmt.ToString(), doc));

                    DocRoot.AppendChild(XMLNodeCreator.xmlNodeForElement("total_surcharge", SurCharge.ToString(), doc));
                    DocRoot.AppendChild(XMLNodeCreator.xmlNodeForElement("total_discount", Discount.ToString(), doc));
                    DocRoot.AppendChild(XMLNodeCreator.xmlNodeForElement("net_sale", TotalNetAmt.ToString(), doc));
                    DocRoot.AppendChild(XMLNodeCreator.xmlNodeForElement("gross_sale", TotalGrossAmt.ToString(), doc));

                    XmlNode ItemSoldReport = doc.CreateElement("ItemSoldReport");
                    DocRoot.AppendChild(ItemSoldReport);

                    //Subcategory Breakdown
                    XmlNode subCategories = doc.CreateElement("subCategories");
                    ItemSoldReport.AppendChild(subCategories);

                    dict = new Dictionary<string, string>() { { "a.Rest_ID", rest_Id } };
                    // ds = Fn.LoadCategoryBreakUpSaleForZReport(dict, fromdate, tilldate, conn);

                    //only cash sale allowed
                    ds = SqlHelper.ExecuteDataset(conn, CommandType.Text, Qry.getCashSubCategoryBreakUpSaleSQL(dict, fromdate, tilldate));

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            XmlNode CategoryInfo = doc.CreateElement("SubCategory");
                            subCategories.AppendChild(CategoryInfo);

                            string categoryId = dr["CategoryID"].ToString();

                            CategoryInfo.AppendChild(XMLNodeCreator.xmlNodeForElement("sub_cat_id", categoryId, doc));
                            CategoryInfo.AppendChild(XMLNodeCreator.xmlNodeForElement("sub_cat_name", dr["CategoryName"].ToString(), doc));
                            CategoryInfo.AppendChild(XMLNodeCreator.xmlNodeForElement("total_amount", dr["Amount"].ToString(), doc));

                            //products-start breakdown
                            XmlNode ProductDetails = doc.CreateElement("products");
                            CategoryInfo.AppendChild(ProductDetails);
                            Dictionary<string, string> dict2 = new Dictionary<string, string>() { { "a.Rest_ID", rest_Id } };
                            //DataSet ds2 = Fn.LoadProductSoldBreakDownByCategoryId(dict2, fromdate, tilldate, conn, categoryId);

                            //only cash sales allowed
                            DataSet ds2 = SqlHelper.ExecuteDataset(conn, CommandType.Text, Qry.getCashProductSoldBreakDownByCategoryIdSQL(dict2, fromdate, tilldate, categoryId));

                            if (ds2.Tables[0].Rows.Count > 0)
                            {
                                foreach (DataRow inner_dr in ds2.Tables[0].Rows)
                                {
                                    XmlNode ProductInfo = doc.CreateElement("Product");
                                    ProductDetails.AppendChild(ProductInfo);

                                    ProductInfo.AppendChild(XMLNodeCreator.xmlNodeForElement("prod_id", inner_dr["ProductID"].ToString(), doc));
                                    ProductInfo.AppendChild(XMLNodeCreator.xmlNodeForElement("prod_name", inner_dr["ProductName"].ToString(), doc));
                                    // ProductDetails.AppendChild(this.xmlNodeForElement("subcategoryId", inner_dr["CategoryID"].ToString(), doc));
                                    ProductInfo.AppendChild(XMLNodeCreator.xmlNodeForElement("price", inner_dr["Amount"].ToString(), doc));
                                    ProductInfo.AppendChild(XMLNodeCreator.xmlNodeForElement("qty", inner_dr["Qty"].ToString(), doc));
                                }
                            }

                            ds2.Dispose();
                            //products-end
                        }
                    }

                    ds.Dispose();
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
            }
        }

        catch (Exception e)
        {
            HttpContext.Current.Response.Write(e.Message.ToString());
            return null;
        }
    }


    [WebMethod(EnableSession = true)]
    // returns current sales total or X-Report
    // Used when client requests for X-Report
    public XmlElement getCurrentSaleSummary(string param, string val)
    {
        try
        {
            XmlDocument doc = new XmlDocument();
            XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", null, null);
            doc.AppendChild(dec);
            XmlElement DocRoot;
            DocRoot = doc.CreateElement("XReportInfo");
            doc.AppendChild(DocRoot);

            using (SqlConnection conn = mConnection.GetConnection())
            {
                conn.Open();

                StrCurrency = Fn.GetTableColumnValue(dict, "omni_CompanyInfo", "CurrencySymbol", "", "", conn);

                try
                {
                    string LastZReportDate = string.Empty;
                    string msg = string.Empty;
                    string fromdate = string.Empty;
                    string tilldate = string.Empty;

                    decimal TipAmount = 0, TaxAmt = 0, SurCharge = 0, Discount = 0, TotalAmount = 0, CashSale = 0;
                    decimal CardSale = 0, VoucherSale = 0, TotalGrossAmt = 0, TotalNetAmt = 0, TotalRefundAmt = 0, TotalPayoutAmt = 0;

                    SqlParameter[] ArParam = new SqlParameter[1];

                    ArParam[0] = new SqlParameter("@CurrDate", SqlDbType.DateTime);
                    ArParam[0].Direction = ParameterDirection.Output;

                    try
                    {
                        SqlHelper.ExecuteNonQuery(conn, CommandType.StoredProcedure, "getCurrentDate", ArParam);
                        tilldate = String.Format("{0:yyyy-MM-dd HH:mm:ss}", ArParam[0].Value);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                    dict = new Dictionary<string, string>() { { param, val } };
                    SqlDataReader LastZReportTranDateRdr = SqlHelper.ExecuteReader(conn, CommandType.Text, Qry.GetLastUpdatesSQL(dict, "ZReport"));

                    if (LastZReportTranDateRdr.Read())
                    {
                        LastZReportDate = LastZReportTranDateRdr["TransactDate"].ToString();
                        LastZReportDate = String.Format("{0:yyyy-MM-dd HH:mm:ss}", Convert.ToDateTime(LastZReportDate));
                        fromdate = LastZReportDate;
                    }
                    else
                        fromdate = "";

                    LastZReportTranDateRdr.Close();

                    SqlDataReader SaleInfoReader = SqlHelper.ExecuteReader(conn, CommandType.Text, Qry.GetSaleInfoForXReportSQL(dict, fromdate, tilldate));

                    while (SaleInfoReader.Read())
                    {
                        TipAmount = (decimal)SaleInfoReader["TipAmount"];
                        TaxAmt = (decimal)SaleInfoReader["TotalTax"];
                        SurCharge = (decimal)SaleInfoReader["Surcharge"];
                        Discount = (decimal)SaleInfoReader["Discount"];
                        TotalAmount = (decimal)SaleInfoReader["TotalAmount"];
                    }

                    SaleInfoReader.Close();


                    dict = new Dictionary<string, string>() { { param, val } };
                    SqlDataReader RefundAmountReader = SqlHelper.ExecuteReader(conn, CommandType.Text, Qry.GetTotalRefundForXReportSQL(dict, fromdate, tilldate));

                    while (RefundAmountReader.Read())
                    {
                        TotalRefundAmt = (decimal)RefundAmountReader["Amount"];
                    }
                    RefundAmountReader.Close();

                    dict = new Dictionary<string, string>() { { param, val } };
                    SqlDataReader PayoutAmountReader = SqlHelper.ExecuteReader(conn, CommandType.Text, Qry.GetTotalPayoutForXReportSQL(dict, fromdate, tilldate));

                    while (PayoutAmountReader.Read())
                    {
                        TotalPayoutAmt = (decimal)PayoutAmountReader["Amount"];
                    }
                    PayoutAmountReader.Close();


                    dict = new Dictionary<string, string>() { { param, val }, { "b.PaymentTypeID", "1" } };
                    SqlDataReader TotalSaleByPaymentTypeReader = SqlHelper.ExecuteReader(conn, CommandType.Text, Qry.GetTotalSaleByPaymentTypeForXReportSQL(dict, fromdate, tilldate));
                    while (TotalSaleByPaymentTypeReader.Read())
                    {
                        CashSale = (decimal)TotalSaleByPaymentTypeReader["PaidAmount"];
                    }

                    TotalSaleByPaymentTypeReader.Close();

                    dict = new Dictionary<string, string>() { { param, val }, { "b.PaymentTypeID", "2" } };
                    TotalSaleByPaymentTypeReader = SqlHelper.ExecuteReader(conn, CommandType.Text, Qry.GetTotalSaleByPaymentTypeForXReportSQL(dict, fromdate, tilldate));

                    while (TotalSaleByPaymentTypeReader.Read())
                    {
                        CardSale = (decimal)TotalSaleByPaymentTypeReader["PaidAmount"];
                    }

                    TotalSaleByPaymentTypeReader.Close();

                    dict = new Dictionary<string, string>() { { param, val }, { "b.PaymentTypeID", "3" } };
                    TotalSaleByPaymentTypeReader = SqlHelper.ExecuteReader(conn, CommandType.Text, Qry.GetTotalSaleByPaymentTypeForXReportSQL(dict, fromdate, tilldate));

                    while (TotalSaleByPaymentTypeReader.Read())
                    {
                        VoucherSale = (decimal)TotalSaleByPaymentTypeReader["PaidAmount"];
                    }

                    TotalSaleByPaymentTypeReader.Close();

                    TotalGrossAmt = (CashSale + CardSale + VoucherSale + SurCharge) - Discount;
                    TotalNetAmt = TotalGrossAmt - TaxAmt;
                    TotalNetAmt = TotalNetAmt - (TotalRefundAmt + TotalPayoutAmt);  // new addition

                    DocRoot.AppendChild(XMLNodeCreator.xmlNodeForElement("FromDate", fromdate, doc));
                    DocRoot.AppendChild(XMLNodeCreator.xmlNodeForElement("TillDate", tilldate, doc));
                    DocRoot.AppendChild(XMLNodeCreator.xmlNodeForElement("CashSale", CashSale.ToString(), doc));
                    DocRoot.AppendChild(XMLNodeCreator.xmlNodeForElement("CardSale", CardSale.ToString(), doc));
                    DocRoot.AppendChild(XMLNodeCreator.xmlNodeForElement("VoucherSale", VoucherSale.ToString(), doc));
                    DocRoot.AppendChild(XMLNodeCreator.xmlNodeForElement("RefundAmount", TotalRefundAmt.ToString(), doc));
                    DocRoot.AppendChild(XMLNodeCreator.xmlNodeForElement("PayoutAmount", TotalPayoutAmt.ToString(), doc));
                    DocRoot.AppendChild(XMLNodeCreator.xmlNodeForElement("TipAmount", TipAmount.ToString(), doc));
                    DocRoot.AppendChild(XMLNodeCreator.xmlNodeForElement("TaxAmount", TaxAmt.ToString(), doc));
                    DocRoot.AppendChild(XMLNodeCreator.xmlNodeForElement("SurChargeAmount", SurCharge.ToString(), doc));
                    DocRoot.AppendChild(XMLNodeCreator.xmlNodeForElement("DiscountAmount", Discount.ToString(), doc));
                    DocRoot.AppendChild(XMLNodeCreator.xmlNodeForElement("TotalNetAmount", TotalNetAmt.ToString(), doc));
                    DocRoot.AppendChild(XMLNodeCreator.xmlNodeForElement("GrossAmount", TotalGrossAmt.ToString(), doc));

                    XmlNode DrawerObject = doc.CreateElement("DrawerObjects");
                    DocRoot.AppendChild(DrawerObject);

                    dict = new Dictionary<string, string>() { { "a." + param, val } };
                    SqlDataReader TotalDrawerAmtReader = SqlHelper.ExecuteReader(conn, CommandType.Text, Qry.GetTotalInDrawerForXReportSQL(dict, fromdate, tilldate));

                    if (TotalDrawerAmtReader.HasRows)
                    {
                        while (TotalDrawerAmtReader.Read())
                        {
                            decimal drawcashsale = 0, drawcardsale = 0, drawvchsale = 0, payout = 0, refund = 0, drawerinhand = 0;

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

                            DrawerInfo.AppendChild(XMLNodeCreator.xmlNodeForElement("ID", TotalDrawerAmtReader["deviceid"].ToString(), doc));
                            DrawerInfo.AppendChild(XMLNodeCreator.xmlNodeForElement("Name", TotalDrawerAmtReader["printername"].ToString(), doc));
                            DrawerInfo.AppendChild(XMLNodeCreator.xmlNodeForElement("Amount", String.Format("{0:0.00}", drawerinhand), doc));
                        }
                    }

                    TotalDrawerAmtReader.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
            }
            return DocRoot;
        }
        catch (Exception e)
        {
            HttpContext.Current.Response.Write(e.Message.ToString());
            return null;
        }
    }

    [WebMethod(EnableSession = true)]

    public XmlElement generateZAnd_GetEndOfDaySummary(string param, string val, string CurrentZReportDoneOn)
    {
        //locals
        decimal TipAmount = 0, TaxAmt = 0, SurCharge = 0, Discount = 0, TotalAmount = 0, CashSale = 0, CardSale = 0;
        decimal VoucherSale = 0, TotalGrossAmt = 0, TotalNetAmt = 0, TotalRefundAmt = 0, TotalPayoutAmt = 0, TotalInDrawerAmt = 0;
        decimal TotalFloatAmt = 0;

        string fromdate = string.Empty;
        string tilldate = string.Empty;
        string locationName = string.Empty, companyEmail = string.Empty;
        bool shouldEmailZReport = false;

        try
        {
            XmlDocument doc = new XmlDocument();
            XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", null, null);
            doc.AppendChild(dec);
            XmlElement DocRoot;
            DocRoot = doc.CreateElement("ZReportInfo");
            doc.AppendChild(DocRoot);

            using (SqlConnection conn = mConnection.GetConnection())
            {
                conn.Open();

                StrCurrency = Fn.GetTableColumnValue(dict, "omni_CompanyInfo", "CurrencySymbol", "", "", conn);
                locationName = Fn.getRestaurantNameForRestId(val);
                companyEmail = Fn.getCompanyEmailID();

                shouldEmailZReport = Fn.shouldEmailZReportForRestId(val);

                if (null == locationName)
                    locationName = "Taste Location";

                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        tilldate = String.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now);

                        dict = new Dictionary<string, string>() { { param, val } };

                        string LastZReportDate = string.Empty;
                        SqlDataReader LastZReportTranDateRdr = SqlHelper.ExecuteReader(trans, CommandType.Text, Qry.GetLastUpdatesSQL(dict, "ZReport"));

                        if (LastZReportTranDateRdr.Read())
                        {
                            LastZReportDate = LastZReportTranDateRdr["TransactDate"].ToString();
                            LastZReportDate = String.Format("{0:yyyy-MM-dd HH:mm:ss}", Convert.ToDateTime(LastZReportDate));
                            fromdate = LastZReportDate;
                        }
                        else
                            fromdate = "";

                        LastZReportTranDateRdr.Close();

                        SqlDataReader SaleInfoReader = SqlHelper.ExecuteReader(trans, CommandType.Text, Qry.GetSaleInfoForXReportSQL(dict, fromdate, tilldate));

                        while (SaleInfoReader.Read())
                        {
                            TipAmount = (decimal)SaleInfoReader["TipAmount"];
                            TaxAmt = (decimal)SaleInfoReader["TotalTax"];
                            SurCharge = (decimal)SaleInfoReader["Surcharge"];
                            Discount = (decimal)SaleInfoReader["Discount"];
                            TotalAmount = (decimal)SaleInfoReader["TotalAmount"];
                        }

                        SaleInfoReader.Close();

                        dict = new Dictionary<string, string>() { { param, val }, { "b.PaymentTypeID", "1" } };
                        SqlDataReader TotalSaleByPaymentTypeReader = SqlHelper.ExecuteReader(trans, CommandType.Text, Qry.GetTotalSaleByPaymentTypeForXReportSQL(dict, fromdate, tilldate));

                        while (TotalSaleByPaymentTypeReader.Read())
                        {
                            CashSale = (decimal)TotalSaleByPaymentTypeReader["PaidAmount"];
                        }

                        TotalSaleByPaymentTypeReader.Close();

                        dict = new Dictionary<string, string>() { { param, val }, { "b.PaymentTypeID", "2" } };
                        TotalSaleByPaymentTypeReader = SqlHelper.ExecuteReader(trans, CommandType.Text, Qry.GetTotalSaleByPaymentTypeForXReportSQL(dict, fromdate, tilldate));

                        while (TotalSaleByPaymentTypeReader.Read())
                        {
                            CardSale = (decimal)TotalSaleByPaymentTypeReader["PaidAmount"];
                        }

                        TotalSaleByPaymentTypeReader.Close();

                        dict = new Dictionary<string, string>() { { param, val }, { "b.PaymentTypeID", "3" } };
                        TotalSaleByPaymentTypeReader = SqlHelper.ExecuteReader(trans, CommandType.Text, Qry.GetTotalSaleByPaymentTypeForXReportSQL(dict, fromdate, tilldate));

                        while (TotalSaleByPaymentTypeReader.Read())
                        {
                            VoucherSale = (decimal)TotalSaleByPaymentTypeReader["PaidAmount"];
                        }

                        TotalSaleByPaymentTypeReader.Close();

                        TotalGrossAmt = (CashSale + CardSale + VoucherSale + SurCharge) - Discount;
                        TotalNetAmt = TotalGrossAmt - TaxAmt;

                        dict = new Dictionary<string, string>() { { param, val } };
                        SqlDataReader RefundAmountReader = SqlHelper.ExecuteReader(trans, CommandType.Text, Qry.GetTotalRefundForXReportSQL(dict, fromdate, tilldate));

                        while (RefundAmountReader.Read())
                        {
                            TotalRefundAmt = (decimal)RefundAmountReader["Amount"];
                        }
                        RefundAmountReader.Close();

                        dict = new Dictionary<string, string>() { { param, val } };
                        SqlDataReader PayoutAmountReader = SqlHelper.ExecuteReader(trans, CommandType.Text, Qry.GetTotalPayoutForXReportSQL(dict, fromdate, tilldate));

                        while (PayoutAmountReader.Read())
                        {
                            TotalPayoutAmt = (decimal)PayoutAmountReader["Amount"];
                        }
                        PayoutAmountReader.Close();

                        TotalInDrawerAmt = (TotalNetAmt + TotalFloatAmt + TipAmount) - (TotalRefundAmt + TotalPayoutAmt);


                        DocRoot.AppendChild(XMLNodeCreator.xmlNodeForElement("FromDate", fromdate, doc));
                        DocRoot.AppendChild(XMLNodeCreator.xmlNodeForElement("TillDate", tilldate, doc));
                        DocRoot.AppendChild(XMLNodeCreator.xmlNodeForElement("CashSale", CashSale.ToString(), doc));
                        DocRoot.AppendChild(XMLNodeCreator.xmlNodeForElement("CardSale", CardSale.ToString(), doc));
                        DocRoot.AppendChild(XMLNodeCreator.xmlNodeForElement("VoucherSale", VoucherSale.ToString(), doc));
                        DocRoot.AppendChild(XMLNodeCreator.xmlNodeForElement("RefundAmount", TotalRefundAmt.ToString(), doc));
                        DocRoot.AppendChild(XMLNodeCreator.xmlNodeForElement("PayoutAmount", TotalPayoutAmt.ToString(), doc));
                        DocRoot.AppendChild(XMLNodeCreator.xmlNodeForElement("TipAmount", TipAmount.ToString(), doc));
                        DocRoot.AppendChild(XMLNodeCreator.xmlNodeForElement("TaxAmount", TaxAmt.ToString(), doc));
                        DocRoot.AppendChild(XMLNodeCreator.xmlNodeForElement("SurChargeAmount", SurCharge.ToString(), doc));
                        DocRoot.AppendChild(XMLNodeCreator.xmlNodeForElement("DiscountAmount", Discount.ToString(), doc));
                        DocRoot.AppendChild(XMLNodeCreator.xmlNodeForElement("TotalNetAmount", TotalNetAmt.ToString(), doc));
                        DocRoot.AppendChild(XMLNodeCreator.xmlNodeForElement("GrossAmount", TotalGrossAmt.ToString(), doc));

                        XmlNode DrawerObject = doc.CreateElement("DrawerObjects");
                        DocRoot.AppendChild(DrawerObject);

                        dict = new Dictionary<string, string>() { { "a." + param, val } };
                        SqlDataReader TotalDrawerAmtReader = SqlHelper.ExecuteReader(trans, CommandType.Text, Qry.GetTotalInDrawerForXReportSQL(dict, fromdate, tilldate));

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

                                DrawerInfo.AppendChild(XMLNodeCreator.xmlNodeForElement("ID", TotalDrawerAmtReader["deviceid"].ToString(), doc));
                                DrawerInfo.AppendChild(XMLNodeCreator.xmlNodeForElement("Name", TotalDrawerAmtReader["printername"].ToString(), doc));
                                DrawerInfo.AppendChild(XMLNodeCreator.xmlNodeForElement("Amount", String.Format("{0:0.00}", drawerinhand), doc));
                            }
                        }

                        TotalDrawerAmtReader.Close();

                        XmlNode EmployeeDetails = doc.CreateElement("EmployeeDetails");
                        DocRoot.AppendChild(EmployeeDetails);

                        dict = new Dictionary<string, string>() { { param, val } };
                        ds = Fn.LoadEmployeeBreakUpSaleForZReport(dict, fromdate, tilldate, trans);

                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                XmlNode EmployeeInfo = doc.CreateElement("EmployeeInfo");
                                EmployeeDetails.AppendChild(EmployeeInfo);

                                EmployeeInfo.AppendChild(XMLNodeCreator.xmlNodeForElement("ID", dr["UserID"].ToString(), doc));
                                EmployeeInfo.AppendChild(XMLNodeCreator.xmlNodeForElement("Name", dr["Employee"].ToString(), doc));
                                EmployeeInfo.AppendChild(XMLNodeCreator.xmlNodeForElement("CashSale", dr["CashSale"].ToString(), doc));
                                EmployeeInfo.AppendChild(XMLNodeCreator.xmlNodeForElement("CardSale", dr["CardSale"].ToString(), doc));
                                EmployeeInfo.AppendChild(XMLNodeCreator.xmlNodeForElement("VoucherSale", dr["VoucherSale"].ToString(), doc));
                            }
                        }

                        ds.Dispose();

                        XmlNode CategoryDetails = doc.CreateElement("CategoryDetails");
                        DocRoot.AppendChild(CategoryDetails);

                        dict = new Dictionary<string, string>() { { "a." + param, val } };
                        ds = Fn.LoadCategoryBreakUpSaleForZReport(dict, fromdate, tilldate, trans);

                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                XmlNode CategoryInfo = doc.CreateElement("CategoryInfo");
                                CategoryDetails.AppendChild(CategoryInfo);

                                CategoryInfo.AppendChild(XMLNodeCreator.xmlNodeForElement("ID", dr["CategoryID"].ToString(), doc));
                                CategoryInfo.AppendChild(XMLNodeCreator.xmlNodeForElement("Name", dr["CategoryName"].ToString(), doc));
                                CategoryInfo.AppendChild(XMLNodeCreator.xmlNodeForElement("Amount", dr["Amount"].ToString(), doc));
                            }
                        }

                        ds.Dispose();

                        XmlNode ProductDetails = doc.CreateElement("ProductDetails");
                        DocRoot.AppendChild(ProductDetails);


                        dict = new Dictionary<string, string>() { { "a." + param, val } };
                        ds = Fn.LoadProductBreakUpSaleForZReport(dict, fromdate, tilldate, trans);

                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                XmlNode ProductInfo = doc.CreateElement("ProductInfo");
                                ProductDetails.AppendChild(ProductInfo);

                                ProductInfo.AppendChild(XMLNodeCreator.xmlNodeForElement("ID", dr["ProductID"].ToString(), doc));
                                ProductInfo.AppendChild(XMLNodeCreator.xmlNodeForElement("Name", dr["ProductName"].ToString(), doc));
                                ProductInfo.AppendChild(XMLNodeCreator.xmlNodeForElement("Amount", dr["Amount"].ToString(), doc));
                            }
                        }

                        ds.Dispose();

                        XmlNode ModifierDetails = doc.CreateElement("ModifierDetails");
                        DocRoot.AppendChild(ModifierDetails);

                        dict = new Dictionary<string, string>() { { "a." + param, val } };
                        using (SqlDataReader OrderedProductModifiersReader = SqlHelper.ExecuteReader(trans, CommandType.Text, Qry.GetProductOptionBreakUpSaleForZReportSQL(dict, fromdate, tilldate)))
                        {
                            string ordamt = string.Empty;
                            string ordqty = string.Empty;
                            string ordprodmodifiers = string.Empty;
                            //string ordprodmodifierid = string.Empty;
                            string bckcolor = string.Empty;
                            List<string> ordprodmodifierid = new List<string>();

                            if (OrderedProductModifiersReader.HasRows)
                            {
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
                                                ordprodmodifierid.Add(ordprodmodifierids[i]);
                                        }
                                    }
                                }
                            }
                            OrderedProductModifiersReader.Close();

                            for (int i = 0; i < ordprodmodifierid.Count; i++)
                            {
                                string modifierid = ordprodmodifierid[i];
                                dict = new Dictionary<string, string>() { { param, val } };
                                string modifiername = Fn.GetTableColumnValue(dict, "omni_Modifiers", "ModifierName", "ModifierID", modifierid, trans);

                                dict = new Dictionary<string, string>() { { param, val } };
                                ordamt = Fn.GetTableColumnValue(dict, "omni_Modifiers", "Price1", "ModifierID", modifierid, trans);

                                if (modifiername != "" && modifiername != null)
                                {
                                    XmlNode ModiferInfo = doc.CreateElement("ModiferInfo");
                                    ModifierDetails.AppendChild(ModiferInfo);

                                    ModiferInfo.AppendChild(XMLNodeCreator.xmlNodeForElement("ID", modifierid, doc));
                                    ModiferInfo.AppendChild(XMLNodeCreator.xmlNodeForElement("Name", modifiername.ToString() + "(" + ordqty + ")", doc));
                                    ModiferInfo.AppendChild(XMLNodeCreator.xmlNodeForElement("Amount", ordamt, doc));
                                }
                            }
                        }


                        /****************  Update Last ZReport Done ********************************/

                        CurrentZReportDoneOn = Fn.ConvertIntoServerTime(CurrentZReportDoneOn);
                        CurrentZReportDoneOn = String.Format("{0:yyyy-MM-dd HH:mm:ss}", Convert.ToDateTime(CurrentZReportDoneOn));


                        SqlParameter[] ArParams = new SqlParameter[3];

                        ArParams[0] = new SqlParameter("@Action", SqlDbType.VarChar, 50);
                        ArParams[0].Value = "ZReport";

                        ArParams[1] = new SqlParameter("@TransactDate", SqlDbType.DateTime);
                        ArParams[1].Value = CurrentZReportDoneOn;

                        ArParams[2] = new SqlParameter("@Rest_ID", SqlDbType.Int);
                        ArParams[2].Value = val;

                        SqlHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, "SP_omni_LastUpdates", ArParams);

                        trans.Commit();

                        /****************************************************************************/


                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        throw ex;
                    }

                    finally
                    {
                        conn.Close();
                    }
                }
            }

            if (shouldEmailZReport)
            {
                //use following line to send email to company
                if (companyEmail.Length > 0)
                {
                    string pTo;
                    string pSubject;

                    string host = HttpContext.Current.Request.Url.Host;

                    if (host.Contains("test") || locationName.Contains("test") || locationName.Contains("TEST SITE"))
                        pTo = "omniposapp@gmail.com";
                    else
                        pTo = companyEmail;

                    pSubject = "End-of-Day Report from " + locationName;

                    string mBody = "";
                    mBody = "<div style=\"FONT-FAMILY:Arial; \">";

                    mBody += "End-of-day report details<br/><br/>";

                    mBody += "<b>Date:</b>&nbsp;" + DateTime.Now.ToShortDateString() + "<br/>";

                    mBody += "<b>Location:</b>&nbsp;" + locationName + "<br/>";
                    mBody += "<b>Z-Report:</b> from&nbsp;<u>" + fromdate + "</u> to&nbsp;<u>" + tilldate + "</u><br/><br/>";

                    mBody += "Report details:</b>";

                    mBody += "<table border='1' width='100%'><tr><td><b>Description</b></td><td><b>Amount</b></td></tr>";
                    mBody += "<tr><td>Cash</td>";
                    mBody += "<td>" + String.Format("{0:0.00}", CashSale) + "</td>";

                    mBody += "<tr><td>Card</td>";
                    mBody += "<td>" + String.Format("{0:0.00}", CardSale) + "</td>";

                    mBody += "<tr><td>Voucher</td>";
                    mBody += "<td>" + String.Format("{0:0.00}", VoucherSale) + "</td>";

                    mBody += "<tr><td>Tips</td>";
                    mBody += "<td>" + String.Format("{0:0.00}", TipAmount) + "</td>";

                    mBody += "<tr><td>Total Discount</td>";
                    mBody += "<td>" + String.Format("{0:0.00}", Discount) + "</td>";

                    mBody += "<tr><td>Total Surcharge</td>";
                    mBody += "<td>" + String.Format("{0:0.00}", SurCharge) + "</td>";

                    mBody += "<tr><td>FLOAT (+)</td>";
                    mBody += "<td>" + String.Format("{0:0.00}", TotalFloatAmt) + "</td>";

                    mBody += "<tr><td>REFUND (+)</td>";
                    mBody += "<td>" + String.Format("{0:0.00}", TotalRefundAmt) + "</td>";

                    mBody += "<tr><td>PAYOUT (+)</td>";
                    mBody += "<td>" + String.Format("{0:0.00}", TotalPayoutAmt) + "</td>";

                    mBody += "</table><br/><br/>";

                    mBody += "<b>GST Included (10%):&nbsp;</b>" + String.Format("{0:0.00}", TaxAmt) + "<br/>";
                    mBody += "<b>TOTAL Net Sale:&nbsp;</b>" + String.Format("{0:0.00}", TotalNetAmt) + "<br/>";
                    mBody += "<b>TOTAL Gross Sale:&nbsp;</b>" + String.Format("{0:0.00}", TotalGrossAmt) + "<br/><br/>";

                    mBody += "Thank you for using OmniPOS.<br/><br/>";
                    mBody += "<b>Regards,</b><br/>";
                    mBody += "<b>Omni Systems</b><br/></div>";

                    EmailManager sm = new EmailManager(pTo, mBody, pSubject);

                    //sm.sendEmail();
                    sm.sendMail();
                }
            }

            return DocRoot;
        }
        catch (Exception e)
        {
            HttpContext.Current.Response.Write(e.Message.ToString());
            return null;
        }

    }

}
