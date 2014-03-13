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
    public partial class xreport : System.Web.UI.Page
    {
        DB mConnection = new DB();
        Common Fn = new Common();
        MyToolSet iTool = new MyToolSet();
        SQLQuery Qry = new SQLQuery();
        DataSet ds = new DataSet();

        public string StrCurrency = string.Empty; 
        public string CurrDateTime = String.Format("{0:dd-MM-yyyy hh:mm:ss}",DateTime.Now);
        public decimal TipAmount=0;
        public decimal TaxAmt=0; 
        public decimal SurCharge=0 ;
        public decimal Discount=0;
        public decimal  TotalAmount=0;
        public decimal CashSale=0;
        public decimal CardSale=0;
        public decimal VoucherSale=0;
        public decimal TotalGrossAmt=0;
        public decimal TotalNetAmt=0;
        public decimal TotalFloatAmt = 0;
        public decimal TotalRefundAmt = 0;
        public decimal TotalPayoutAmt = 0;
        public decimal TotalInDrawerAmt=0;
        public decimal TotalReduceSaleAmt = 0;

        public string fromdate = string.Empty;
        public string tilldate = string.Empty;
        public string lastZdate = string.Empty;
        public int noSaleCount = 0;

        public xreport()
        {
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            // adding local information below Current Summary header:
            Common com = new Common();
            Dictionary<string, string> headerFooter = com.getHeaderAndFooter(Session["R_ID"].ToString());
            // string compinfo = com.GetCompanyInfo();
            // string Company_Name = compinfo["Company_Name"];
            string Header_Name = headerFooter["Header_Name"];
            string location_Name = Session["R_Name"].ToString();
            /*string Header_Address1 = headerFooter["Header_Address1"];
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
           /*
          if (Header_City != null && Header_City != "")
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
            // ends here
            //txtFromDate.Attributes.Add("onclick", "disabletextbox('" + txtFromDate.ClientID + "')");
            //txtFromDate.Attributes.Add("onblur", "enabletextbox('" + txtFromDate.ClientID + "')");

            //txtTillDate.Attributes.Add("onclick", "disabletextbox('" + txtTillDate.ClientID + "')");
            //txtTillDate.Attributes.Add("onblur", "enabletextbox('" + txtTillDate.ClientID + "')");

            //if (!IsPostBack)
            //{
            //    txtFromDate.Text = String.Format("{0:MM/dd/yyyy}", DateTime.Now);
            //    txtTillDate.Text = String.Format("{0:MM/dd/yyyy}", DateTime.Now);
            //}

            if (Session["Currency"] != null && Session["Currency"] != "")
                StrCurrency = Session["Currency"].ToString();

            if (Session["R_ID"] == "" || Session["R_ID"] == null)
            {
                Session["bckurl"] = "XReport.aspx";
                Server.Transfer("Notification.aspx");
                return;
            }
				BtnPrint.Attributes.Add("onclick", "PrintDoc()");

            this.generateXReport();
        }

        private void generateXReport()
        {
            OPSaleFinder sf = new OPSaleFinder();

            try
            {
                using (SqlConnection conn = mConnection.GetConnection())
                {
                    conn.Open();    
                    
                    using (SqlTransaction trans = conn.BeginTransaction())
                    {
                        try
                        {
                            string LastZReportDate = string.Empty;
                            string msg = string.Empty;
                            string bckcolor = string.Empty;
                            int i = 0;
                            string tilldate = string.Empty;

                            SqlParameter[] ArParam = new SqlParameter[1];

                            ArParam[0] = new SqlParameter("@CurrDate", SqlDbType.DateTime);
                            ArParam[0].Direction = ParameterDirection.Output;

                            try
                            {
                                SqlHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, "getCurrentDate", ArParam);
                                tilldate = String.Format("{0:yyyy-MM-dd HH:mm:ss}", ArParam[0].Value);
                            }
                            catch(Exception ex)
                            {
                            }
                            
                            Dictionary<string, string> dict;
                            dict = new Dictionary<string, string>() { { "Rest_ID", Session["R_ID"].ToString() } };
                            SqlDataReader LastZReportTranDateRdr = SqlHelper.ExecuteReader(trans, CommandType.Text, Qry.GetLastUpdatesSQL(dict, "ZReport"));

                            if (LastZReportTranDateRdr.Read())
                            {
                                LastZReportDate = LastZReportTranDateRdr["TransactDate"].ToString();
                                LastZReportDate = String.Format("{0:yyyy-MM-dd HH:mm:ss}", Convert.ToDateTime(LastZReportDate));
                                LblRepo.InnerText = "Date Selected From : " + String.Format("{0:dd-MM-yyyy HH:mm:ss}", Convert.ToDateTime(LastZReportDate)) + " Till Now";

                                fromdate = LastZReportDate;
                                //tilldate = String.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now);
                            }
                            else
                            {
                                LblRepo.InnerText = "UpTo : " + String.Format("{0:dd-MM-yyyy HH:mm:ss}", Convert.ToDateTime(DateTime.Now));
                                fromdate = "";
                                //tilldate = String.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now);
                            }

                            lastZdate = String.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(LastZReportDate));

                            LastZReportTranDateRdr.Close();

                            SqlDataReader SaleInfoReader = SqlHelper.ExecuteReader(trans, CommandType.Text,Qry.GetSaleInfoForXReportSQL(dict,fromdate,tilldate));

                            while (SaleInfoReader.Read())
                            {
                                TipAmount = (decimal)SaleInfoReader["TipAmount"];
                                TaxAmt = (decimal)SaleInfoReader["TotalTax"];
                                SurCharge = (decimal)SaleInfoReader["Surcharge"];
                                Discount = (decimal)SaleInfoReader["Discount"];
                                TotalAmount = (decimal)SaleInfoReader["TotalAmount"];
                            }

                            SaleInfoReader.Close();


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
                                TotalPayoutAmt = (decimal)PayoutAmountReader ["Amount"];
                            }
                            PayoutAmountReader.Close();


                            dict = new Dictionary<string, string>() { { "Rest_ID", Session["R_ID"].ToString() }, { "b.PaymentTypeID", "1" } };
                            SqlDataReader TotalSaleByPaymentTypeReader = SqlHelper.ExecuteReader(trans, CommandType.Text, Qry.GetTotalSaleByPaymentTypeForXReportSQL(dict, fromdate, tilldate));
                            while (TotalSaleByPaymentTypeReader.Read())
                            {
                                CashSale = (decimal)TotalSaleByPaymentTypeReader["PaidAmount"];
                                //CashSale = CashSale - TotalRefundAmt;
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

                            TotalNetAmt = TotalNetAmt - (TotalRefundAmt + TotalPayoutAmt);  // new addition

                            //TotalInDrawerAmt = (TotalNetAmt + TotalFloatAmt + TipAmount) - (TotalRefundAmt + TotalPayoutAmt);

                            dict = new Dictionary<string, string>() { { "a.Rest_ID", Session["R_ID"].ToString() } };
                            SqlDataReader TotalDrawerAmtReader = SqlHelper.ExecuteReader(trans, CommandType.Text, Qry.GetTotalInDrawerForXReportSQL(dict, fromdate, tilldate));

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

                                drawerinhand = drawcashsale ;

                                if ((i % 2) == 0)
                                    bckcolor = "white";
                                else
                                    bckcolor = "";

                                msg +="<tr style=\"background-color:\"" + bckcolor + ">";
                                msg += "<td align=\"center\" style=\"color:Red;\"> Total in DRAWER (" + TotalDrawerAmtReader["printername"] + ")</td>";
                                msg += "<td align=\"center\" style=\"color:Red;font-style:italic;\">" + StrCurrency + String.Format("{0:0.00}", drawerinhand) + "</td>";
                                msg +="</tr>";
                                i++;
                            }

                            TotalDrawerAmtReader.Close();
                            Drawer.Text = msg;
                            //TotalValue  = Fn.GetTableColumnValue(null,"
                                
                                //Qry.getLastInsertedID(null, "omni_Products", "productid", 1, "", ""));
                            x_report.Visible = true;
							BtnPrint.Visible =true;

                            // refund / payout should be deducted from net total and refund will be deducted from net only for cash sale
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

            noSaleCount = sf.getNoSaleCount(lastZdate, null, Session["R_ID"].ToString(), mConnection.GetConnection());

        }
       

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            //fromdate = iTool.formatInputString(txtFromDate.Text);
            //tilldate = iTool.formatInputString(txtTillDate.Text);

            //if (fromdate != "" && Fn.ValidateDate(fromdate))
            //{
            //    fromdate = String.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(fromdate));
            //    LblRepo.InnerText = "From : " + String.Format("{0:dd-MM-yyyy}", Convert.ToDateTime(fromdate));
                    
            //}
            //if (tilldate != "" && Fn.ValidateDate(tilldate))
            //{
            //    tilldate = String.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(tilldate));
            //    LblRepo.InnerText += " To " + String.Format("{0:dd-MM-yyyy}", Convert.ToDateTime(tilldate)) + " till now";
            //}

          this.generateXReport();
        }
    }
}