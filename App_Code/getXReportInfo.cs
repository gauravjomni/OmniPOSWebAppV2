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

/// <summary>
/// Summary description for getUserInfo
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class getXReportInfo : System.Web.Services.WebService
{
    public getXReportInfo()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
        
    }

    [WebMethod (EnableSession=true) ]

    public XmlElement getXReportDetails(string param, string val)
    {
        DB mConnection = new DB();
        Common Fn = new Common();
        SQLQuery Qry = new SQLQuery();

        //dict = new Dictionary<string, string>() { { "UserGroupId", GroupID.Value } };
        Dictionary<string, string> dict = null;
        string StrCurrency = string.Empty;

        try
        {
            XmlDocument doc = new XmlDocument();
            XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", null,null);
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
					//int i = 0;
					string fromdate = string.Empty;
					string tilldate = string.Empty;

					decimal TipAmount=0;
					decimal TaxAmt=0; 
					decimal SurCharge=0 ;
					decimal Discount=0;
					decimal  TotalAmount=0;
					decimal CashSale=0;
					decimal CardSale=0;
					decimal VoucherSale=0;
					decimal TotalGrossAmt=0;
					decimal TotalNetAmt=0;
					//decimal TotalFloatAmt = 0;
					decimal TotalRefundAmt = 0;
					decimal TotalPayoutAmt = 0;

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
						//tilldate = String.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now);
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

					dict = new Dictionary<string, string>() { { "a."+param, val } };
					SqlDataReader TotalDrawerAmtReader = SqlHelper.ExecuteReader(conn, CommandType.Text, Qry.GetTotalInDrawerForXReportSQL(dict, fromdate, tilldate));

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
				   /* else
					{
						XmlNode DrawerInfo = doc.CreateElement("DrawerInfo");
						DrawerInfo.InnerText = "No Data";
						DrawerObject.AppendChild(DrawerInfo);
					}*/

					TotalDrawerAmtReader.Close();
				}
				catch (Exception ex)
				{
					// throw exception						
					//txtResults.Text = "Transfer Error";
					//trans.Rollback();
					throw ex;
				}
				finally
				{
					conn.Close();
				}
					
					
                //}
				
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

