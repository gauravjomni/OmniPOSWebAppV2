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
    public partial class View_CompanyItemSold : System.Web.UI.Page
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
        public string Company_Name = null;
        public string Company_Address = null;

        public string fromdate = string.Empty;
        public string tilldate = string.Empty;
        public string fromdatenew = string.Empty;
        public string tilldatenew = string.Empty;
        public string fromdater = string.Empty;
        public string tilldater = string.Empty;


        public View_CompanyItemSold()
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

            Fn.switchingbeteenlocation2company(true);

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
                fromdater = String.Format("{0:MM/dd/yyyy}", iTool.formatInputString(Request.Form["txtFromDate"]));
            }
            if (tilldate != "" && Fn.ValidateDate(tilldate))
            {
                tilldate = String.Format("{0:yyyy-MM-dd}", Fn.ConvertDateIntoAnotherFormat2(tilldate));
                tilldatenew = String.Format("{0:dd-MM-yyyy}", Fn.ConvertDateIntoAnotherFormat3(tilldate));
                tilldater = String.Format("{0:MM/dd/yyyy}", iTool.formatInputString(Request.Form["txtTillDate"]));
            }
            if (fromdate == tilldate)
            {
                DateTime dt = DateTime.Parse(tilldate).AddDays(1);
                tilldate = String.Format("{0:yyyy-MM-dd}", dt);

                // tilldate = String.Format("{0:yyyy-MM-dd}", Fn.ConvertDateIntoAnotherFormat2(tilldate));
            }
            LblRepo.InnerText = "Date Selected From " + String.Format("{0:dd MMM yyyy}", fromdatenew) + " To " + String.Format("{0:dd,MMM yyyy}", tilldatenew);

            try
            {
                using (SqlConnection conn = mConnection.GetConnection())
                {
                    conn.Open();

                    try
                    {
                        Dictionary<string, string> dict;
                        dict = null;        // new Dictionary<string, string>() { { "a.Rest_ID", Session["R_ID"].ToString() } };
                        //ds = Fn.LoadItemSold(dict, fromdate, tilldate);
                        //ds = Fn.LoadUniqueItemSoldInRange(dict, fromdate, tilldate);
                        ds = Fn.LoadUniqueItemSoldInRange(dict, fromdate, tilldate,conn);
						
						if (ds.Tables[0].Rows.Count > 1)
							BtnPrint.Visible = true;
						else
							BtnPrint.Visible = false;							
						
                        ProductSaleInfoRepeater.DataSource = ds;
                        ProductSaleInfoRepeater.DataBind();

                        Product.Visible = true;
                        ProductOption.Visible = true;

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
            catch (Exception ex)
            { }        
        }

        protected void ProductSaleInfoRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            string footerstring = string.Empty;

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                TotalAmount += Convert.ToDecimal(DataBinder.Eval(e.Item.DataItem, "Amount"));
                Literal LtrCurrency = (Literal)e.Item.FindControl("Currency");
                //LtrCurrency.Text = Fn.GetTableColumnValue(null,"omni_CompanyInfo","CurrencySymbol","","");
                LtrCurrency.Text = StrCurrency;
                //    Fn.GetTableColumnValue(null,"omni_CompanyInfo","CurrencySymbol","","");
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
                    footerstring += "<td colspan=\"2\" style=\"color:red\"><b><i>TOTAL : <i><b></td>";
                    footerstring += "<td style=\"color:red\"><b>" + Session["Currency"].ToString() +  TotalAmount + "</b></td>";
                }
                Label lblFooter = (Label)e.Item.FindControl("Footer");
                lblFooter.Text = footerstring;
                lblFooter.Visible = true;
            }

            //}

        }
    }
}