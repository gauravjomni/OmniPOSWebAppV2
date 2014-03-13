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
    public partial class qreport : System.Web.UI.Page
    {
        DB mConnection = new DB();
        Common Fn = new Common();
        MyToolSet iTool = new MyToolSet();
        SQLQuery Qry = new SQLQuery();
        DataSet ds = new DataSet();

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

        public qreport()
        {
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            txtFromDate.Attributes.Add("onclick", "disabletextbox('" + txtFromDate.ClientID + "')");
            txtFromDate.Attributes.Add("onblur", "enabletextbox('" + txtFromDate.ClientID + "')");

            txtTillDate.Attributes.Add("onclick", "disabletextbox('" + txtTillDate.ClientID + "')");
            txtTillDate.Attributes.Add("onblur", "enabletextbox('" + txtTillDate.ClientID + "')");

            if (!IsPostBack)
            {
                txtFromDate.Text = String.Format("{0:MM/dd/yyyy}", DateTime.Now);
                txtTillDate.Text = String.Format("{0:MM/dd/yyyy}", DateTime.Now);
            }
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                fromdate = iTool.formatInputString(txtFromDate.Text);
                tilldate = iTool.formatInputString(txtTillDate.Text);

                if (fromdate != "" && Fn.ValidateDate(fromdate))
                {
                    fromdate = String.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(fromdate));
                    LblRepo.InnerText = "From : " + String.Format("{0:dd-MM-yyyy}", Convert.ToDateTime(fromdate));

                }
                if (tilldate != "" && Fn.ValidateDate(tilldate))
                {
                    tilldate = String.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(tilldate));
                    LblRepo.InnerText += " To " + String.Format("{0:dd-MM-yyyy}", Convert.ToDateTime(tilldate)) + " till now";
                }

                using (SqlConnection conn = mConnection.GetConnection())
                {
                    conn.Open();

                    using (SqlTransaction trans = conn.BeginTransaction())
                    {
                        try
                        {
                            Dictionary<string, string> dict;
                            dict = new Dictionary<string, string>() { { "Rest_ID", Session["R_ID"].ToString() } };

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

                            dict = new Dictionary<string, string>() { { "Rest_ID", Session["R_ID"].ToString() }, { "b.PaymentTypeID", "1" } };
                            SqlDataReader TotalSaleByPaymentTypeReader = SqlHelper.ExecuteReader(trans, CommandType.Text, Qry.GetTotalSaleByPaymentTypeForXReportSQL(dict, fromdate, tilldate));
                            while (TotalSaleByPaymentTypeReader.Read())
                            {
                                CashSale = (decimal)TotalSaleByPaymentTypeReader["PaidAmount"];
                            }

                            TotalSaleByPaymentTypeReader.Close();

                            dict = new Dictionary<string, string>() { { "Rest_ID", Session["R_ID"].ToString() }, { "b.PaymentTypeID", "2" } };
                            TotalSaleByPaymentTypeReader = SqlHelper.ExecuteReader(trans, CommandType.Text, Qry.GetTotalSaleByPaymentTypeForXReportSQL(dict, fromdate, tilldate));

                            while (TotalSaleByPaymentTypeReader.Read())
                            {
                                CardSale = (decimal)TotalSaleByPaymentTypeReader["PaidAmount"];
                            }

                            TotalSaleByPaymentTypeReader.Close();

                            dict = new Dictionary<string, string>() { { "Rest_ID", Session["R_ID"].ToString() }, { "b.PaymentTypeID", "3" } };
                            TotalSaleByPaymentTypeReader = SqlHelper.ExecuteReader(trans, CommandType.Text, Qry.GetTotalSaleByPaymentTypeForXReportSQL(dict, fromdate, tilldate));

                            while (TotalSaleByPaymentTypeReader.Read())
                            {
                                VoucherSale = (decimal)TotalSaleByPaymentTypeReader["PaidAmount"];
                            }

                            TotalSaleByPaymentTypeReader.Close();

                            TotalGrossAmt = (CashSale + CardSale + VoucherSale + SurCharge) - Discount;
                            TotalNetAmt = TotalGrossAmt - TaxAmt;

                            dict = new Dictionary<string, string>() { { "Rest_ID", Session["R_ID"].ToString() } };
                            SqlDataReader RefundAmountReader = SqlHelper.ExecuteReader(trans, CommandType.Text, Qry.GetTotalRefundForXReportSQL(dict, fromdate, tilldate));

                            while (RefundAmountReader.Read())
                            {
                                TotalRefundAmt = (decimal)RefundAmountReader["Amount"];
                            }
                            RefundAmountReader.Close();

                            dict = new Dictionary<string, string>() { { "Rest_ID", Session["R_ID"].ToString() } };
                            SqlDataReader PayoutAmountReader = SqlHelper.ExecuteReader(trans, CommandType.Text, Qry.GetTotalPayoutForXReportSQL(dict, fromdate, tilldate));

                            while (PayoutAmountReader.Read())
                            {
                                TotalPayoutAmt = (decimal)PayoutAmountReader["Amount"];
                            }
                            PayoutAmountReader.Close();

                            TotalInDrawerAmt = (TotalNetAmt + TotalFloatAmt + TipAmount) - (TotalRefundAmt + TotalPayoutAmt);

                            dict = new Dictionary<string, string>() { { "Rest_ID", Session["R_ID"].ToString() } };
                            ds = Fn.LoadEmployeeBreakUpSaleForZReport(dict, fromdate, tilldate);
                            EmployeeSaleInfoRepeater.DataSource = ds;
                            EmployeeSaleInfoRepeater.DataBind();

                            dict = new Dictionary<string, string>() { { "a.Rest_ID", Session["R_ID"].ToString() } };
                            ds = Fn.LoadCategoryBreakUpSaleForZReport(dict, fromdate, tilldate);
                            CategorySaleInfoRepeater.DataSource = ds;
                            CategorySaleInfoRepeater.DataBind();

                            dict = new Dictionary<string, string>() { { "a.Rest_ID", Session["R_ID"].ToString() } };
                            ds = Fn.LoadProductBreakUpSaleForZReport(dict, fromdate, tilldate);
                            ProductSaleInfoRepeater.DataSource = ds;
                            ProductSaleInfoRepeater.DataBind();

                            dict = new Dictionary<string, string>() { { "a.Rest_ID", Session["R_ID"].ToString() } };
                            SqlDataReader OrderedProductModifiersReader = SqlHelper.ExecuteReader(trans, CommandType.Text, Qry.GetProductOptionBreakUpSaleForZReportSQL(dict, fromdate, tilldate));

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
                                                msg += "<td align=\"center\" style=\"color:Black; font-style:italic;\">$" + ordamt + "</td>";
                                                msg += "</tr>";
                                            }
                                        }
                                    }
                                }

                            }

                            OrderedProductModifiersReader.Close();

                            Product_Options_Details.Text = msg;

                            x_report.Visible = true;
                            Employee.Visible = true;
                            SubMenu.Visible = true;
                            Product.Visible = true;
                            ProductOption.Visible = true;

                        }
                        catch (Exception ex)
                        {
                            // throw exception						
                            //txtResults.Text = "Transfer Error";
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