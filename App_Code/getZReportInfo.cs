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
using MailTools;


/// <summary>
/// Summary description for getUserInfo
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]

public class getZReportInfo : System.Web.Services.WebService
{

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

    string fromdate = string.Empty;
    string tilldate = string.Empty;
    string locationName = string.Empty, companyEmail = string.Empty;

    public getZReportInfo()
    {
        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod (EnableSession=true) ]

    public XmlElement getZReportDetails(string param, string val, string CurrentZReportDoneOn)
    {
        DB mConnection = new DB();
        Common Fn = new Common();
        SQLQuery Qry = new SQLQuery();
        DataSet ds = new DataSet();

        Dictionary<string, string> dict = null;
        string StrCurrency = string.Empty;
        bool shouldEmailZReport = false;

        try
        {
            XmlDocument doc = new XmlDocument();
            XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", null,null);
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

                if(null == locationName)
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

                        dict = new Dictionary<string, string>() { {  param,  val } };
                        SqlDataReader PayoutAmountReader = SqlHelper.ExecuteReader(trans, CommandType.Text, Qry.GetTotalPayoutForXReportSQL(dict, fromdate, tilldate));

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
                        /*else
                        {
                            XmlNode DrawerInfo = doc.CreateElement("DrawerInfo");
                            DrawerInfo.InnerText = "No Data";
                            DrawerObject.AppendChild(DrawerInfo);
                        }*/

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

/*                              XmlNode Employee_Total = doc.CreateElement("Total");
                                Employee_Total.InnerText = StrCurrency + dr["Total"].ToString();
                                EmployeeInfo.AppendChild(Employee_Total); */
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

                        dict = new Dictionary<string, string>() { { "a." +  param,  val } };
                        ds = Fn.LoadCategoryBreakUpSaleForZReport(dict, fromdate, tilldate,trans);

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


                        dict = new Dictionary<string, string>() { { "a." + param,  val } };
                        ds = Fn.LoadProductBreakUpSaleForZReport(dict, fromdate, tilldate,trans);

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
                       /* else
                        {
                            XmlNode ModiferInfo = doc.CreateElement("ModiferInfo");
                            ModiferInfo.InnerText = "No Data";
                            DocRoot.AppendChild(ModiferInfo);
                        }
                        */
                        



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

            
            //use following line to send email to company
            if (shouldEmailZReport)
            {
                if (companyEmail.Length > 0)
                {
                    this.sendReportToOwner();
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

    private void sendReportToOwner()
    {
        string pTo;
        string pSubject;

        string host = HttpContext.Current.Request.Url.Host;

        if (host.Contains("test") || locationName.Contains("test") || locationName.Contains("TEST SITE"))
            pTo = "omniposapp@gmail.com";
        else
            pTo = companyEmail;

        pSubject = "End-of-Day Report from "+  locationName;

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

        /*
        mBody += "<tr><td>GST Included (10 %)</td>";
        mBody += "<td>" + String.Format("{0:0.00}", TaxAmt) + "</td>";

        mBody += "<tr><td>TOTAL Net Sale</td>";
        mBody += "<td>" + String.Format("{0:0.00}", TotalNetAmt) + "</td>";

        mBody += "<tr><td>TOTAL Gross Sale</td>";
        mBody += "<td>" + String.Format("{0:0.00}", TotalGrossAmt) + "</td>";
        */

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

