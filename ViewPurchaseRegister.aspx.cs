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
    public partial class ViewPurchaseRegister : System.Web.UI.Page
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

        public string fromdate = string.Empty;
        public string tilldate = string.Empty;

        public string fromdater = string.Empty;
        public string tilldater = string.Empty;

        decimal totqty = 0;
        decimal totunit_price = 0;
        decimal totPurchaseAmt = 0;
        Dictionary<string, string> dict = null;
        string suppid = string.Empty;
        string prodtype = string.Empty;

        public ViewPurchaseRegister()
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Common com = new Common();

            // ends here
           
            if (Session["Currency"] != null && Session["Currency"] != "")
                StrCurrency = Session["Currency"].ToString();

            if (!IsPostBack)
            {

                if (Session["R_ID"] == "" || Session["R_ID"] == null)
                {
                    Session["bckurl"] = "ViewPurchaseRegister.aspx";
                    Server.Transfer("Notification.aspx");
                    return;
                }
                else
                {
                    Dictionary<string, string> headerFooter = com.getHeaderAndFooter(Session["R_ID"].ToString());
                    // string compinfo = com.GetCompanyInfo();
                    // string Company_Name = compinfo["Company_Name"];
                    string Header_Name = headerFooter["Header_Name"];
                    string location_Name = Session["R_Name"].ToString();

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
                
                }

                //                txtFromDate = String.Format("{0:MM/dd/yyyy}", DateTime.Now);
                //              txtTillDate.Text = String.Format("{0:MM/dd/yyyy}", DateTime.Now);
                fromdate = String.Format("{0:MM/dd/yyyy}", DateTime.Now);
                tilldate = String.Format("{0:MM/dd/yyyy}", DateTime.Now);
                fromdater = fromdate;
                tilldater = tilldate;
            }
            else
            {
                fromdater = Request.Form["txtFromDate"];
                tilldater = Request.Form["txtTillDate"];
                suppid = Supplier.SelectedValue;
                prodtype = ProductType.SelectedValue;

                if (suppid != "")
                    Supplier.SelectedValue = suppid;

                if (prodtype!="")
                    ProductType.SelectedValue = prodtype;
            }
            BtnPrint.Attributes.Add("onclick", "PrintDoc()");
            
            dict = null;
            Fn.PopulateDropDown_List(Supplier, Qry.GetSupplierSQL(dict), "SupplierName", "SupplierID", "");
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            fromdate = iTool.formatInputString(Request.Form["txtFromDate"]);
            tilldate = iTool.formatInputString(Request.Form["txtTillDate"]);
            suppid = Supplier.SelectedValue;
            prodtype = ProductType.SelectedValue;

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
            
            
            Dictionary<string, string> dict;
            dict = new Dictionary<string, string>() { { "Rest_ID", Session["R_ID"].ToString() } };

            try
            {
                using (SqlConnection conn = mConnection.GetConnection())
                {
                    conn.Open();

                    try
                    {
                        dict = new Dictionary<string, string>() { { "a.Rest_ID", Session["R_ID"].ToString() }};

                        if (suppid != "")
                            dict.Add("a.SupplierID", suppid);

                        if (prodtype != "")
                            dict.Add("ProductType", prodtype);

                        ds = Fn.LoadPurchaseItemData(dict, fromdate, tilldate, conn);

                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            BtnPrint.Visible = true;
                            //MsgTip.Visible = true;
                        }
                        else
                            BtnPrint.Visible = false;

                        PurchaseHistoryRepeater.DataSource = ds;
                        PurchaseHistoryRepeater.DataBind();
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
                }
            }
            catch (Exception ex)
            { }   
            
        }

        protected void PurchaseHistoryRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            string footerstring = string.Empty;


            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                totqty += Convert.ToDecimal(DataBinder.Eval(e.Item.DataItem, "Qty"));
                totunit_price += Convert.ToDecimal(DataBinder.Eval(e.Item.DataItem, "Unit_Price"));
                totPurchaseAmt += Convert.ToDecimal(DataBinder.Eval(e.Item.DataItem, "TotalAmt"));
            }
            else if (e.Item.ItemType == ListItemType.Footer)
            {
                if (PurchaseHistoryRepeater.Items.Count < 1)
                {
                    footerstring = "<tr>";
                    footerstring += "<td colspan=\"8\" align=\"center\">No Data To Display.</td>";
                    footerstring += "</tr>";
                }
                else
                {
                    footerstring += "<tr>";
                    footerstring += "<td colspan=\"5\" style=\"color:red\"><b><i>Total : <i><b></td>";

                    footerstring += "<td style=\"color:red\"><b> " + totqty + "</b></td>";
                    footerstring += "<td style=\"color:red\"><b> " + StrCurrency + totunit_price + "</b></td>";
                    footerstring += "<td style=\"color:red\"><b> " + StrCurrency + totPurchaseAmt + "</b></td>";
                    footerstring += "</tr>";
                }
                Label lblFooter = (Label)e.Item.FindControl("Footer");
                lblFooter.Text = footerstring;
                lblFooter.Visible = true;
            }


        }
    }   
}