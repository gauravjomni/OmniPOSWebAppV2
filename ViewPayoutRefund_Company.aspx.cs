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
    public partial class ViewPayoutRefund_Company : System.Web.UI.Page
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
        public string Company_Name = null;
        public string Company_Address = null;

        public string fromdate = string.Empty;
        public string tilldate = string.Empty;

        public string fromdater = string.Empty;
        public string tilldater = string.Empty;

        decimal totamt = 0;

        public ViewPayoutRefund_Company()
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Common com = new Common();
            using (SqlConnection conn = mConnection.GetConnection())
            {
                try
                {
                    conn.Open();
                    SqlDataReader headerReader = SqlHelper.ExecuteReader(conn, CommandType.Text, Qry.GetCompanySQL());
                    //  string headerFooter = Qry.GetCompanySQL();
                    while (headerReader.Read())
                    {
                        Company_Name = headerReader["CompanyName"].ToString();
                        Company_Address = headerReader["Address"].ToString();
                    }
                    if (Company_Name != null && Company_Name != "")
                    {
                        companyname.Text = Company_Name;
                        companyname.Visible = true;
                    }
                    if (Company_Address != null && Company_Address != "")
                    {
                        companyaddress.Text = Company_Address;
                        companyaddress.Visible = true;
                    }
                    headerReader.Close();
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
           

            if (Session["Currency"]!=null &&  Session["Currency"] != "")
                StrCurrency = Session["Currency"].ToString();

            if (!IsPostBack)
            {
                //                txtFromDate = String.Format("{0:MM/dd/yyyy}", DateTime.Now);
                //              txtTillDate.Text = String.Format("{0:MM/dd/yyyy}", DateTime.Now);
                Fn.switchingbeteenlocation2company(true);

                fromdate = String.Format("{0:MM/dd/yyyy}", DateTime.Now);
                tilldate = String.Format("{0:MM/dd/yyyy}", DateTime.Now);
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
                //LblRepo.InnerText = "From : " + String.Format("{0:dd-MM-yyyy}", Convert.ToDateTime(fromdate));
                fromdater = String.Format("{0:MM/dd/yyyy}", iTool.formatInputString(Request.Form["txtFromDate"]));                     
            }
            if (tilldate != "" && Fn.ValidateDate(tilldate))
            {
                tilldate = String.Format("{0:yyyy-MM-dd}", Fn.ConvertDateIntoAnotherFormat2(tilldate));
                //LblRepo.InnerText += " To " + String.Format("{0:dd-MM-yyyy}", Convert.ToDateTime(tilldate)) + " till now";
                tilldater = String.Format("{0:MM/dd/yyyy}", iTool.formatInputString(Request.Form["txtTillDate"]));
            }
            
            
/*            Dictionary<string, string> dict;
            dict = new Dictionary<string, string>() { { "Rest_ID", Session["R_ID"].ToString() } }; */

            try
            {
                using (SqlConnection conn = mConnection.GetConnection())
                {
                    conn.Open();

                    try
                    {
                        //dict = new Dictionary<string, string>() { { "A.Rest_ID", Session["R_ID"].ToString() }};
                        ds = Fn.LoadPayoutRefundTransactionData(null,fromdate,tilldate,conn);

                        if (ds.Tables[0].Rows.Count > 0)
                            BtnPrint.Visible = true;
                        else
                            BtnPrint.Visible = false;

                        PayoutRefundTranHistoryRepeater.DataSource = ds;
                        PayoutRefundTranHistoryRepeater.DataBind();
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
            }
            catch (Exception ex)
            { }   
            
        }

        protected void PayoutRefundTranHistoryRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            string footerstring = string.Empty;
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                totamt += Convert.ToDecimal(DataBinder.Eval(e.Item.DataItem, "Amount"));
            }
            else if (e.Item.ItemType == ListItemType.Footer)
            {
                if (PayoutRefundTranHistoryRepeater.Items.Count < 1)
                {
                    footerstring = "<tr>";
                    footerstring += "<td colspan=\"6\" align=\"center\">No Data To Display.</td>";
                    footerstring += "</tr>";
                }
                else
                {
                    footerstring += "<tr>";
                    footerstring += "<td colspan=\"5\" style=\"color:red\"><b><i>Total : <i><b></td>";
                    footerstring += "<td style=\"color:red;text-align:right; padding-right:25px;\"><b>" + StrCurrency + totamt + "</b></td>";
                    footerstring += "<td> </td>";
                    footerstring += "</tr>";
                }
                Label lblFooter = (Label)e.Item.FindControl("Footer");
                lblFooter.Text = footerstring;
                lblFooter.Visible = true;
            }

            //}

        }
}
}