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
    public partial class ViewOrderTransacttion : System.Web.UI.Page
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

        decimal totnetamt = 0;
        decimal totgrossamt = 0;
        decimal tottipamt = 0;
        decimal totsurcharge = 0;
        decimal totdiscount = 0;
        decimal tottax = 0;

        public ViewOrderTransacttion()
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
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
      /*      if (Header_City != null && Header_City != "")
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
           
            if (Session["Currency"] != null && Session["Currency"] != "")
                StrCurrency = Session["Currency"].ToString();

            if (!IsPostBack)
            {

                if (Session["R_ID"] == "" || Session["R_ID"] == null)
                {
                    Session["bckurl"] = "ViewOrderTransaction.aspx";
                    Server.Transfer("Notification.aspx");
                    return;
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
                        ds = Fn.LoadOrderTransactionData(dict,fromdate,tilldate, StrCurrency, conn);

                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            BtnPrint.Visible = true;
                            MsgTip.Visible = true;
                        }
                        else
                            BtnPrint.Visible = false;

                        OrderTranHistoryRepeater.DataSource = ds;
                        OrderTranHistoryRepeater.DataBind();
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

        protected void OrderTranHistoryRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            string footerstring = string.Empty;
            
           /* if (OrderTranHistoryRepeater.Items.Count < 1)
            {
                if (e.Item.ItemType == ListItemType.Footer)
                {
                    Label lblFooter = (Label)e.Item.FindControl("Footer");
                    footerstring="<tr>";
                    footerstring += "<td colspan=\"9\" align=\"center\">No Data To Display.</td>";
                    footerstring += "</tr>";
                    lblFooter.Text = footerstring;
                    lblFooter.Visible = true;
                }
            }
            else
            {*/
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    totnetamt += Convert.ToDecimal(DataBinder.Eval(e.Item.DataItem, "GrossAmount"));
                    tottipamt += Convert.ToDecimal(DataBinder.Eval(e.Item.DataItem, "TipAmount"));
                    totsurcharge += Convert.ToDecimal(DataBinder.Eval(e.Item.DataItem, "Surcharge"));
                    totdiscount += Convert.ToDecimal(DataBinder.Eval(e.Item.DataItem, "Discount"));
                    tottax += Convert.ToDecimal(DataBinder.Eval(e.Item.DataItem, "TotalTax"));
                    TotalAmount += Convert.ToDecimal(DataBinder.Eval(e.Item.DataItem, "TotalAmount"));
                }
                else if (e.Item.ItemType == ListItemType.Footer)
                {
                    if (OrderTranHistoryRepeater.Items.Count < 1)
                    {
                        footerstring = "<tr>";
                        footerstring += "<td colspan=\"13\" align=\"center\">No Data To Display.</td>";
                        footerstring += "</tr>";
                    }
                    else
                    {
                        footerstring += "<tr>";
                        footerstring += "<td colspan=\"4\" style=\"color:red\"><b><i>Total : <i><b></td>";
                        
                       // footerstring += "<td style=\"color:red\"><b>Tips<br/> " + StrCurrency + tottipamt + "</b></td>";
                        footerstring += "<td style=\"color:red\"><b>Surcharge<br/> " + StrCurrency + totsurcharge + "</b></td>";
                        footerstring += "<td style=\"color:red\"><b>Discount<br/> " + StrCurrency + totdiscount + "</b></td>";
                        footerstring += "<td style=\"color:red\"><b>GST<br/> " + StrCurrency + tottax + "</b></td>";
                        footerstring += "<td style=\"color:red\"><b>Net<br/> " + StrCurrency + TotalAmount + "</b></td>";
                        footerstring += "<td style=\"color:red\"><b>Gross<br/> " + StrCurrency + totnetamt + "</b></td>";
                        footerstring += "<td colspan=\"3\">&nbsp;</td>";
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