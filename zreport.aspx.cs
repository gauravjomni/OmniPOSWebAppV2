using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Xml.Linq;
using MyDB;
using MyQuery;
using MyTool;
using Commons;

namespace PosReport
{
    public partial class zreport : System.Web.UI.Page
    {
        DB mConnection = new DB();
        Common Fn = new Common();
        MyToolSet iTool = new MyToolSet();
        SQLQuery Qry = new SQLQuery();
        DataSet ds = new DataSet();

        public string StrCurrency = string.Empty;
        public string CurrDateTime = String.Format("{0:dd-MM-yyyy hh:mm:ss}", DateTime.Now);
        public decimal TipAmount = 0;
        public decimal TaxAmt = 0;
        public decimal SurCharge = 0;
        public decimal Discount = 0;
        public decimal TotalAmount = 0;
        public decimal CashSale = 0;
        public decimal CardSale = 0;
        public decimal VoucherSale = 0;
        public decimal TotalGrossAmt = 0;
        public decimal TotalNetAmt = 0;
        public decimal TotalFloatAmt = 0;
        public decimal TotalRefundAmt = 0;
        public decimal TotalPayoutAmt = 0;
        public decimal TotalInDrawerAmt = 0;

        public string fromdate = string.Empty;
        public string tilldate = string.Empty;

        public zreport()
        {
        }
        protected void Page_Load(object sender, EventArgs e)
        {
/*          txtFromDate.Attributes.Add("onclick", "disabletextbox('" + txtFromDate.ClientID + "')");
            txtFromDate.Attributes.Add("onblur", "enabletextbox('" + txtFromDate.ClientID + "')");

            txtTillDate.Attributes.Add("onclick", "disabletextbox('" + txtTillDate.ClientID + "')");
            txtTillDate.Attributes.Add("onblur", "enabletextbox('" + txtTillDate.ClientID + "')"); */

            /*if (!IsPostBack)
            {
                txtFromDate.Text = String.Format("{0:MM/dd/yyyy}", DateTime.Now);
                txtTillDate.Text = String.Format("{0:MM/dd/yyyy}", DateTime.Now);
            }*/

            if (Session["Currency"] != null && Session["Currency"] != "")
                StrCurrency = Session["Currency"].ToString();

            if (Session["R_ID"] == "" || Session["R_ID"] == null)
            {
                Session["bckurl"] = "ZReport.aspx";
                Server.Transfer("Notification.aspx");
                return;
            }

			BtnPrint.Attributes.Add("onclick", "PrintDoc()");
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = mConnection.GetConnection())
                {
                    conn.Open();

                    using (SqlTransaction trans = conn.BeginTransaction())
                    {
                        try
                        {
                            string tilldate = string.Empty;
                            string tilldate1 = string.Empty;

                            if (ClntDtTm.Value != "")
                            {
                                tilldate = Fn.ConvertIntoServerTime(ClntDtTm.Value);
                                tilldate = String.Format("{0:yyyy-MM-dd HH:mm:ss}", Convert.ToDateTime(tilldate));
                                tilldate1 = String.Format("{0:dd-MM-yyyy HH:mm:ss}", Convert.ToDateTime(tilldate));
                            }
/*                            SqlParameter[] ArParam = new SqlParameter[1];

                            ArParam[0] = new SqlParameter("@CurrDate", SqlDbType.DateTime);
                            ArParam[0].Direction = ParameterDirection.Output;

                            try
                            {
                                SqlHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, "getCurrentDate", ArParam);
                                tilldate = String.Format("{0:yyyy-MM-dd HH:mm:ss}", ArParam[0].Value);
                            }
                            catch (Exception ex)
                            {
                            }
*/
  
                            Dictionary<string, string> dict;
                            dict = new Dictionary<string, string>() { { "Rest_ID", Session["R_ID"].ToString() } };

                            string LastZReportDate = string.Empty;
                            SqlDataReader LastZReportTranDateRdr = SqlHelper.ExecuteReader(trans, CommandType.Text, Qry.GetLastUpdatesSQL(dict, "ZReport"));

                            if (LastZReportTranDateRdr.Read())
                            {
                                LastZReportDate = LastZReportTranDateRdr["TransactDate"].ToString();
                                LastZReportDate = String.Format("{0:yyyy-MM-dd HH:mm:ss}", Convert.ToDateTime(LastZReportDate));
                                LblRepo.InnerText = "From : " + String.Format("{0:dd-MM-yyyy HH:mm:ss}", Convert.ToDateTime(LastZReportDate)) + " Till " + tilldate1;

                                fromdate = LastZReportDate;
                                //tilldate = String.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now);
                            }
                            else
                            {
                                //LblRepo.InnerText = "UpTo : " + String.Format("{0:dd-MM-yyyy HH:mm:ss}", Convert.ToDateTime(DateTime.Now));
                                LblRepo.InnerText = "UpTo : " + tilldate1;
                                fromdate = "";
                                //tilldate = String.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now);
                            }

                            LastZReportTranDateRdr.Close();

                            //SqlDataReader SaleInfoReader = SqlHelper.ExecuteReader(trans, CommandType.Text, Qry.GetSaleInfoForXReportSQL(dict, LastZReportDate));
                            SqlDataReader SaleInfoReader = SqlHelper.ExecuteReader(trans, CommandType.Text, Qry.GetSaleInfoForXReportSQL(dict, fromdate,tilldate));

                            while (SaleInfoReader.Read())
                            {
                                TipAmount = (decimal)SaleInfoReader["TipAmount"];
                                TaxAmt = (decimal)SaleInfoReader["TotalTax"];
                                SurCharge = (decimal)SaleInfoReader["Surcharge"];
                                Discount = (decimal)SaleInfoReader["Discount"];
                                TotalAmount = (decimal)SaleInfoReader["TotalAmount"];
                            }

                            SaleInfoReader.Close();

                            dict = new Dictionary<string, string>() { { "Rest_ID", Session["R_ID"].ToString() }, { "b.PaymentTypeID", "1" } };
//                            SqlDataReader TotalSaleByPaymentTypeReader = SqlHelper.ExecuteReader(trans, CommandType.Text, Qry.GetTotalSaleByPaymentTypeForXReportSQL(dict, LastZReportDate ));
                            SqlDataReader TotalSaleByPaymentTypeReader = SqlHelper.ExecuteReader(trans, CommandType.Text, Qry.GetTotalSaleByPaymentTypeForXReportSQL(dict, fromdate,tilldate));

                            while (TotalSaleByPaymentTypeReader.Read())
                            {
                                CashSale = (decimal)TotalSaleByPaymentTypeReader["PaidAmount"];
                            }

                            TotalSaleByPaymentTypeReader.Close();

                            dict = new Dictionary<string, string>() { { "Rest_ID", Session["R_ID"].ToString() }, { "b.PaymentTypeID", "2" } };
                            //TotalSaleByPaymentTypeReader = SqlHelper.ExecuteReader(trans, CommandType.Text, Qry.GetTotalSaleByPaymentTypeForXReportSQL(dict, LastZReportDate ));
                            TotalSaleByPaymentTypeReader = SqlHelper.ExecuteReader(trans, CommandType.Text, Qry.GetTotalSaleByPaymentTypeForXReportSQL(dict, fromdate,tilldate));

                            while (TotalSaleByPaymentTypeReader.Read())
                            {
                                CardSale = (decimal)TotalSaleByPaymentTypeReader["PaidAmount"];
                            }

                            TotalSaleByPaymentTypeReader.Close();

                            dict = new Dictionary<string, string>() { { "Rest_ID", Session["R_ID"].ToString() }, { "b.PaymentTypeID", "3" } };
                            //TotalSaleByPaymentTypeReader = SqlHelper.ExecuteReader(trans, CommandType.Text, Qry.GetTotalSaleByPaymentTypeForXReportSQL(dict, LastZReportDate));
                            TotalSaleByPaymentTypeReader = SqlHelper.ExecuteReader(trans, CommandType.Text, Qry.GetTotalSaleByPaymentTypeForXReportSQL(dict, fromdate,tilldate));

                            while (TotalSaleByPaymentTypeReader.Read())
                            {
                                VoucherSale = (decimal)TotalSaleByPaymentTypeReader["PaidAmount"];
                            }

                            TotalSaleByPaymentTypeReader.Close();

                            TotalGrossAmt = (CashSale + CardSale + VoucherSale + SurCharge) - Discount;
                            TotalNetAmt = TotalGrossAmt - TaxAmt;

                            dict = new Dictionary<string, string>() { { "Rest_ID", Session["R_ID"].ToString() } };
                            //SqlDataReader RefundAmountReader = SqlHelper.ExecuteReader(trans, CommandType.Text, Qry.GetTotalRefundForXReportSQL(dict, LastZReportDate));
                            SqlDataReader RefundAmountReader = SqlHelper.ExecuteReader(trans, CommandType.Text, Qry.GetTotalRefundForXReportSQL(dict, fromdate,tilldate));

                            while (RefundAmountReader.Read())
                            {
                                TotalRefundAmt = (decimal)RefundAmountReader["Amount"];
                            }
                            RefundAmountReader.Close();

                            dict = new Dictionary<string, string>() { { "Rest_ID", Session["R_ID"].ToString() } };
                            //SqlDataReader PayoutAmountReader = SqlHelper.ExecuteReader(trans, CommandType.Text, Qry.GetTotalPayoutForXReportSQL(dict, LastZReportDate));
                            SqlDataReader PayoutAmountReader = SqlHelper.ExecuteReader(trans, CommandType.Text, Qry.GetTotalPayoutForXReportSQL(dict, fromdate,tilldate));

                            while (PayoutAmountReader.Read())
                            {
                                TotalPayoutAmt = (decimal)PayoutAmountReader["Amount"];
                            }
                            PayoutAmountReader.Close();

                            TotalInDrawerAmt = (TotalNetAmt + TotalFloatAmt + TipAmount) - (TotalRefundAmt + TotalPayoutAmt);

                            dict = new Dictionary<string, string>() { { "Rest_ID", Session["R_ID"].ToString() } };
                            //ds = Fn.LoadEmployeeBreakUpSaleForZReport(dict, LastZReportDate);
                            ds = Fn.LoadEmployeeBreakUpSaleForZReport(dict, fromdate,tilldate);
                            EmployeeSaleInfoRepeater.DataSource = ds;
                            EmployeeSaleInfoRepeater.DataBind();

                            dict = new Dictionary<string, string>() { { "a.Rest_ID", Session["R_ID"].ToString() } };
                            //ds = Fn.LoadCategoryBreakUpSaleForZReport(dict, LastZReportDate);
                            ds = Fn.LoadCategoryBreakUpSaleForZReport(dict, fromdate ,tilldate);
                            CategorySaleInfoRepeater.DataSource = ds;
                            CategorySaleInfoRepeater.DataBind();

                            dict = new Dictionary<string, string>() { { "a.Rest_ID", Session["R_ID"].ToString() } };
                            //ds = Fn.LoadProductBreakUpSaleForZReport(dict, LastZReportDate);
                            ds = Fn.LoadProductBreakUpSaleForZReport(dict, fromdate,tilldate);
                            ProductSaleInfoRepeater.DataSource = ds;
                            ProductSaleInfoRepeater.DataBind();

                            dict = new Dictionary<string, string>() { { "a.Rest_ID", Session["R_ID"].ToString() } };
                            //SqlDataReader OrderedProductModifiersReader = SqlHelper.ExecuteReader(trans, CommandType.Text, Qry.GetProductOptionBreakUpSaleForZReportSQL(dict, LastZReportDate));
                            SqlDataReader OrderedProductModifiersReader = SqlHelper.ExecuteReader(trans, CommandType.Text, Qry.GetProductOptionBreakUpSaleForZReportSQL(dict, fromdate,tilldate));

                            string ordamt = string.Empty;
                            string ordqty = string.Empty;
                            string ordprodmodifiers = string.Empty;
                            string ordprodmodifierid = string.Empty;
                            string msg = string.Empty;
                            string bckcolor = string.Empty;

                            while (OrderedProductModifiersReader.Read())
                            {
                                ordqty = OrderedProductModifiersReader["Qty"].ToString();
                                ordprodmodifiers = OrderedProductModifiersReader["Modifiers"].ToString();

                                string[] ordprodmodifierids = ordprodmodifiers.Split('|');

                                if (ordprodmodifierids.Length >= 0)
                                {
                                    for (int i = 0; i < ordprodmodifierids.Length; i++)
                                    {
                                        if ((i % 2) == 0)
                                            bckcolor = "white";
                                        else
                                            bckcolor = "";

                                        ordprodmodifierid = ordprodmodifierids[i];

                                        if (ordprodmodifierid != "" && ordprodmodifierid != null)
                                        {
                                            dict = new Dictionary<string, string>() { { "Rest_ID", Session["R_ID"].ToString() } };
                                            string modifiername = Fn.GetTableColumnValue(dict, "omni_Modifiers", "ModifierName", "ModifierID", ordprodmodifierid);

                                            dict = new Dictionary<string, string>() { { "Rest_ID", Session["R_ID"].ToString() } };
                                            ordamt = Fn.GetTableColumnValue(dict, "omni_Modifiers", "Price1", "ModifierID", ordprodmodifierid);

                                            if (modifiername != "" && modifiername != null)
                                            {
                                                msg += "<tr style=\"background-color:\"" + bckcolor + ">";
                                                msg += "<td align=\"center\" style=\"color:Black;\">" + modifiername + "(" + ordqty + ")</td>";
                                                msg += "<td align=\"center\" style=\"color:Black; font-style:italic;\">" + Session["Currency"] + ordamt + "</td>";
                                                msg += "</tr>";
                                            }
                                        }
                                    }
                                }

                            }

                            OrderedProductModifiersReader.Close();

                            Product_Options_Details.Text = msg;

                            z_report.Visible = true;
                            Employee.Visible = true;
                            SubMenu.Visible = true;
                            Product.Visible = true;
                            ProductOption.Visible = true;
							
							BtnPrint.Visible = true;
                            SqlParameter[] ArParams = new SqlParameter[3];

                            ArParams[0] = new SqlParameter("@Action", SqlDbType.VarChar,50);
                            ArParams[0].Value = "ZReport";

                            ArParams[1] = new SqlParameter("@TransactDate", SqlDbType.DateTime);
                            ArParams[1].Value = tilldate;

                            ArParams[2] = new SqlParameter("@Rest_ID", SqlDbType.Int);
                            ArParams[2].Value = Session["R_ID"];

                            SqlHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, "SP_omni_LastUpdates", ArParams);

                            trans.Commit();
                        }
                        catch (Exception ex)
                        {
                            // throw exception						
                            //txtResults.Text = "Transfer Error";
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
            { }        
        }
    }
}