﻿using System;
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
    public partial class periodic_zreport : System.Web.UI.Page
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
        public decimal TotalAmountcat = 0;
        public decimal TotalAmountProduct = 0;
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

        public string fromdatenew = string.Empty;
        public string tilldatenew = string.Empty;

        public string fromdater = string.Empty;
        public string tilldater = string.Empty;
        public int noSaleCount = 0;

        public periodic_zreport()
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

            Common com = new Common();
            Dictionary<string, string> headerFooter = com.getHeaderAndFooter(Session["R_ID"].ToString());
            // string compinfo = com.GetCompanyInfo();
            // string Company_Name = compinfo["Company_Name"];
            string Header_Name = headerFooter["Header_Name"];
            string location_Name = Session["R_Name"].ToString();
          /*  string Header_Address1 = headerFooter["Header_Address1"];
            string Header_City = headerFooter["Header_City"];

            string Header_State = headerFooter["Header_State"];
            string Header_Zip = headerFooter["Header_Zip"];
            string Header_Email = headerFooter["Header_Email"];

            string Header_ABN = headerFooter["Header_ABN"];
            string Footer1 = headerFooter["Footer1"];
            string Footer2 = headerFooter["Footer2"];
           */
         
            // Displaying the company details under Sales(Chart) category
            if (Header_Name != null && Header_Name != "")
            {
                company.Text = Header_Name;
                company.Visible = true;

            }

            if (location_Name != null && location_Name != "")
            {
                location.Text = location_Name;
                location.Visible = true;

            }
           /* if (Header_City != null && Header_City != "")
            {
                City.Text = Header_City;
                City.Visible = true;
            }
            if (Header_State != null && Header_State != "")
            {
                State.Text = Header_State;
                State.Visible = true;
            }
            if (Header_Zip != null && Header_Zip != "")
            {
                Zip.Text = Header_Zip;
                Zip.Visible = true;
            }
            if (Header_Email != null && Header_Email != "")
            {
                email.Text = Header_Email;
                email.Visible = true;
            }
            if (Header_ABN != null && Header_ABN != "")
            {
                Abn.Text = Header_ABN;
                Abn.Visible = true;
            }
            */

            if (Session["Currency"] != null && Session["Currency"] != "")
                StrCurrency = Session["Currency"].ToString();


            if (Session["R_ID"] == "" || Session["R_ID"] == null)
            {
                Session["bckurl"] = "Periodic_ZReport.aspx";
                Server.Transfer("Notification.aspx");
                return;
            }

            if (!IsPostBack)
            {
                fromdate = String.Format("{0:MM/dd/yyyy}", DateTime.Now);
                tilldate = String.Format("{0:MM/dd/yyyy}", DateTime.Now);
                fromdatenew = String.Format("{0:MM/dd/yyyy}", DateTime.Now);
                tilldatenew = String.Format("{0:MM/dd/yyyy}", DateTime.Now);
                fromdater = fromdate;
                tilldater = tilldate;
            }
            else
            {
                fromdater = Request.Form["txtFromDate"];
                tilldater = Request.Form["txtTillDate"];
            }
			BtnPrint.Attributes.Add("onclick", "PrintDoc()");
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {

            fromdate = iTool.formatInputString(Request.Form["txtFromDate"]);
            tilldate = iTool.formatInputString(Request.Form["txtTillDate"]);

            if (fromdate != "" && Fn.ValidateDate(fromdate))
            {
                fromdate = String.Format("{0:yyyy-MM-dd}", Fn.ConvertDateIntoAnotherFormat2(fromdate));
                fromdatenew = String.Format("{0:dd-MM-yyyy}", Fn.ConvertDateIntoAnotherFormat3(fromdate));
                //fromdate = String.Format("{0:yyyy-MM-dd}", fromdate);
                fromdater = String.Format("{0:MM/dd/yyyy}", iTool.formatInputString(Request.Form["txtFromDate"]));
            }

            if (tilldate != "" && Fn.ValidateDate(tilldate))
            {
                tilldate = String.Format("{0:yyyy-MM-dd}", Fn.ConvertDateIntoAnotherFormat2(tilldate));
                tilldatenew = String.Format("{0:dd-MM-yyyy}", Fn.ConvertDateIntoAnotherFormat3(tilldate));
                //tilldate = String.Format("{0:yyyy-MM-dd}", tilldate);
                tilldater = String.Format("{0:MM/dd/yyyy}", iTool.formatInputString(Request.Form["txtTillDate"]));
            }

            if (fromdate == tilldate)
            {
                DateTime dt = DateTime.Parse(tilldate).AddDays(1);
                tilldate = String.Format("{0:yyyy-MM-dd}", dt);

               // tilldate = String.Format("{0:yyyy-MM-dd}", Fn.ConvertDateIntoAnotherFormat2(tilldate));
            }

            LblRepo.InnerText = "Date Selected From " + String.Format("{0:dd MMM yyyy}", fromdatenew) + " To " + String.Format("{0:dd,MMM yyyy}", tilldatenew);

            OPSaleFinder sf = new OPSaleFinder();
            noSaleCount = sf.getNoSaleCount(fromdate, tilldate, Session["R_ID"].ToString(), mConnection.GetConnection());

            try
            {
                using (SqlConnection conn = mConnection.GetConnection())
                {
                    conn.Open();

                    using (SqlTransaction trans = conn.BeginTransaction())
                    {
                        try
                        {
                            Dictionary<string, string> dict;
                            dict = new Dictionary<string, string>() { { "Rest_ID", Session["R_ID"].ToString() } };

                           
                            /*string LastZReportDate = string.Empty;
                            SqlDataReader LastZReportTranDateRdr = SqlHelper.ExecuteReader(trans, CommandType.Text, Qry.GetLastUpdatesSQL(dict, "ZReport"));

                            if (LastZReportTranDateRdr.Read())
                            {
                                LastZReportDate = LastZReportTranDateRdr["TransactDate"].ToString();
                                LastZReportDate = String.Format("{0:yyyy-MM-dd HH:mm:ss}", Convert.ToDateTime(LastZReportDate));
                                LblRepo.InnerText = "From : " + String.Format("{0:dd-MM-yyyy HH:mm:ss}", Convert.ToDateTime(LastZReportDate)) + " Till Now";

                                fromdate = LastZReportDate;
                                tilldate = String.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now);
                            }
                            else
                            {
                                LblRepo.InnerText = "UpTo : " + String.Format("{0:dd-MM-yyyy HH:mm:ss}", Convert.ToDateTime(DateTime.Now));
                                fromdate = "";
                                tilldate = String.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now);
                            } 

                            LastZReportTranDateRdr.Close();
                            */

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

                            //add optem items if exist
                            DataSet openItemDS = Fn.LoadOpenItemBreakUpSale(dict, fromdate, tilldate);

                            if (openItemDS.Tables[0].Rows.Count > 0)
                            {
                                string qty = "", amount = "";
                                foreach (DataRow inner_dr in openItemDS.Tables[0].Rows)
                                {
                                    qty = inner_dr["Qty"].ToString();
                                    amount = inner_dr["Amount"].ToString();
                                }

                                if (qty.Length > 0 && amount.Length > 0)
                                {
                                    DataRow dr = ds.Tables[0].NewRow();
                                    dr["Amount"] = amount;
                                    dr["Qty"] = qty;
                                    dr["ProductName"] = "Open Item(s)";
                                    dr["CategoryID"] = "0";
                                    dr["ProductID"] = "0";

                                    ds.Tables[0].Rows.Add(dr);
                                }
                            }
                            //


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
                                                msg += "<td align=\"right\" style=\"color:Black; font-style:italic;\">" + StrCurrency + ordamt + "</td>";
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
/*                            SqlParameter[] ArParams = new SqlParameter[3];

                            ArParams[0] = new SqlParameter("@Action", SqlDbType.VarChar,50);
                            ArParams[0].Value = "ZReport";

                            ArParams[1] = new SqlParameter("@TransactDate", SqlDbType.DateTime);
                            ArParams[1].Value = DateTime.Now;

                            ArParams[2] = new SqlParameter("@Rest_ID", SqlDbType.Int);
                            ArParams[2].Value = Session["R_ID"];

                            SqlHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, "SP_omni_LastUpdates", ArParams);

                            trans.Commit(); */
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
                    }
                }
            }
            catch (Exception ex)
            { }        
        }

        protected void CategorySaleInfoRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            string footerstring = string.Empty;

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                TotalAmountcat += Convert.ToDecimal(DataBinder.Eval(e.Item.DataItem, "Amount"));
            }
            else if (e.Item.ItemType == ListItemType.Footer)
            {
                if (CategorySaleInfoRepeater.Items.Count < 1)
                {
                    footerstring = "<tr>";
                    footerstring += "<td colspan=\"3\" align=\"center\">No Data To Display.</td>";
                    footerstring += "</tr>";
                }
                else
                {
                    footerstring += "<tr>";
                    footerstring += "<td colspan=\"1\" style=\"color:black\"><b>TOTAL : <b></td>";
                    footerstring += "<td style=\"color:black\"><b>" + StrCurrency + TotalAmountcat + "</b></td>";
                }
                Label lblFooter = (Label)e.Item.FindControl("Footer");
                lblFooter.Text = footerstring;
                lblFooter.Visible = true;
            }

            //}

        }

        protected void ProductSaleInfoRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            string footerstring = string.Empty;

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                TotalAmountProduct += Convert.ToDecimal(DataBinder.Eval(e.Item.DataItem, "Amount"));
            }
            else if (e.Item.ItemType == ListItemType.Footer)
            {
                if (ProductSaleInfoRepeater.Items.Count < 1)
                {
                    footerstring = "<tr>";
                    footerstring += "<td colspan=\"3\" align=\"center\">No Data To Display.</td>";
                    footerstring += "</tr>";
                }
                else
                {
                    footerstring += "<tr>";
                    footerstring += "<td colspan=\"1\" style=\"color:black\"><b>TOTAL : <b></td>";
                    footerstring += "<td style=\"color:black\"><b>" + StrCurrency + TotalAmountProduct + "</b></td>";
                }
                Label lblFooter = (Label)e.Item.FindControl("FooterProduct");
                lblFooter.Text = footerstring;
                lblFooter.Visible = true;
            }

            //}

        }
    }
}