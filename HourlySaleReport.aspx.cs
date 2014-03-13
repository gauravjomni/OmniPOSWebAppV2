using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Xml.Linq;
using MyDB;
using MyQuery;
using System.Web.UI.DataVisualization;
using MyTool;
using Commons;
using System.Drawing;
using System.Web.UI.DataVisualization.Charting;

public partial class HourlySaleReport : System.Web.UI.Page
{
    DB mConnection = new DB();
    Common Fn = new Common();
    MyToolSet iTool = new MyToolSet();
    SQLQuery Qry = new SQLQuery();
    DataSet ds = new DataSet();
    public string strCurrency = string.Empty;
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
    public decimal TotalRefundAmt = 0;
    public decimal TotalPayoutAmt = 0;
    public decimal TotalInDrawerAmt = 0;

    public string fromdate = string.Empty;
    public string tilldate = string.Empty;

    public string fromdatenew = string.Empty;
    public string tilldatenew = string.Empty;

    public string fromdater = string.Empty;
    public string tilldater = string.Empty;
    decimal totalamount = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Currency"] != null && Session["Currency"] != "")
            strCurrency = Session["Currency"].ToString();

       // Fn.switchingbeteenlocation2company(true);

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

        Common com = new Common();
        Dictionary<string, string> headerFooter = com.getHeaderAndFooter(Session["R_ID"].ToString());
        string Header_Name = headerFooter["Header_Name"];
        string location_Name = Session["R_Name"].ToString();

    /*    string Header_Address1 = headerFooter["Header_Address1"];
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
        }

        LblRepo.InnerText = "Date Selected From " + String.Format("{0:dd MMM yyyy}", fromdatenew) + " To " + String.Format("{0:dd,MMM yyyy}", tilldatenew);
        Common com = new Common();
        Dictionary<string, string> restIds = com.getRestaurentIdandName();
        OPSaleFinder saleFinder = new OPSaleFinder();
        Dictionary<string, string> dict = null;
        Dictionary<string, decimal> totalamountdict = new Dictionary<string, decimal>();
        try
        {
            using (SqlConnection conn = mConnection.GetConnection())
            {
                try
                {
                    conn.Open();
                      
                    string restName;
                    bool res = restIds.TryGetValue(Session["R_ID"].ToString(), out restName);
                    totalamountdict = saleFinder.getHourlySale(fromdate, tilldate, Session["R_ID"].ToString(), conn);
                        
                    string[] allKeys = totalamountdict.Keys.ToArray<string>();
                    List<string> keys = totalamountdict.Keys.ToList<string>();
                    foreach (string hour in keys)
                    {
                        decimal amt;
                        res = totalamountdict.TryGetValue(hour, out amt);
                        z_report.Visible = true;

                        string nextHour = "";

                        if(keys.IndexOf(hour)+1 < keys.Count)
                            nextHour = keys[keys.IndexOf(hour)+1];
                        totalamount += amt;

                        string tHour = MyToolSet.get12HourTimeFormat(Convert.ToInt32(hour));

                        string tNextHour = "";

                        if(nextHour.Length > 0)
                           tNextHour = MyToolSet.get12HourTimeFormat(Convert.ToInt32(nextHour));

                        this.displayRestaurantSaleDetails(tHour, amt, tNextHour);
                    }
                   
                    amountlabel.InnerText = strCurrency + totalamount.ToString();
                    
                    
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
        {
            throw ex;
        }
    }
    private void displayRestaurantSaleDetails(string hour, decimal amt, string nextHour)
    {
      
     
        /* TableCell tc3 = new TableCell();
         TableCell tc4 = new TableCell();
         TableRow tr2 = new TableRow();
         tc3.Text = "Cash";
         tc4.Text = strCurrency + Convert.ToString(CashSale);
         tr2.Controls.Add(tc3);
         tr2.Controls.Add(tc4);

         TableCell tc5 = new TableCell();
         TableCell tc6 = new TableCell();
         TableRow tr3 = new TableRow();
         tc5.Text = "Card";
         tc6.Text = strCurrency + Convert.ToString(CardSale);
         tr3.Controls.Add(tc5);
         tr3.Controls.Add(tc6);

         TableCell tc7 = new TableCell();
         TableCell tc8 = new TableCell();
         TableRow tr4 = new TableRow();
         tc7.Text = "Voucher";
         tc8.Text = strCurrency + Convert.ToString(VoucherSale);
         tr4.Controls.Add(tc7);
         tr4.Controls.Add(tc8);

         salesbylocationtable.Controls.Add(tr1);
         salesbylocationtable.Controls.Add(tr2);
         salesbylocationtable.Controls.Add(tr3);
         salesbylocationtable.Controls.Add(tr4);

         //For Refund and payout

         TableCell tc9 = new TableCell();
         TableCell tc10 = new TableCell();
         TableRow tr5 = new TableRow();
         tc9.Text = "Refund";
         tc10.Text = strCurrency + Convert.ToString(TotalRefundAmt);
         tr5.Controls.Add(tc9);
         tr5.Controls.Add(tc10);
         */
        
        TableCell tc14 = new TableCell();
        TableCell tc12 = new TableCell();
        TableRow tr6 = new TableRow();
     //   tr6.BackColor = Color.AliceBlue;

        if(nextHour.Length <= 0)
            tc14.Text = "From" + " " + Convert.ToString(hour)+ " " + "To -";
        else
            tc14.Text = "From" + " " + Convert.ToString(hour)+ " " + "To "+  Convert.ToString(nextHour);

        tc14.Font.Bold = true;
        tr6.Controls.Add(tc12);
        tc12.Text = strCurrency + Convert.ToString(amt);
        tc12.Width = 135;
        tr6.Controls.Add(tc14);
        tr6.Controls.Add(tc12);


      /*  TableCell tc15 = new TableCell();
        TableCell tc16 = new TableCell();
        TableRow tr8 = new TableRow();
        tc15.Text = "TOTAL Net Sale";
        tc16.Text = strCurrency + Convert.ToString(netSale);
        tr8.Controls.Add(tc15);
        tr8.Controls.Add(tc16);

        TableCell tc17 = new TableCell();
        TableCell tc18 = new TableCell();
        TableRow tr9 = new TableRow();
        tc17.Text = "GST Included (10%)";
        tc18.Text = strCurrency + Convert.ToString(TaxAmt);
        tr9.Controls.Add(tc17);
        tr9.Controls.Add(tc18);
        */
       /* salesbylocationtable.Controls.Add(tr10);
        salesbylocationtable.Controls.Add(tr1);
        salesbylocationtable.Controls.Add(tr2);
        salesbylocationtable.Controls.Add(tr3);
        salesbylocationtable.Controls.Add(tr4);
        salesbylocationtable.Controls.Add(tr5);
         salesbylocationtable.Controls.Add(tr9);
        salesbylocationtable.Controls.Add(tr8);
        */
        salesbylocationtable.Controls.Add(tr6);
        //salesbylocationtable.Controls.Add(tr7);



    }
}
