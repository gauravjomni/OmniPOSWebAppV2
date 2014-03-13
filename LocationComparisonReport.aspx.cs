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


public partial class LocationComparisonReport : System.Web.UI.Page
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

        if (Session["Currency"] != "")
            strCurrency = Session["Currency"].ToString();

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
        }

        LblRepo.InnerText = "Date Selected From " + String.Format("{0:dd MMM yyyy}", fromdatenew) + " To " + String.Format("{0:dd,MMM yyyy}", tilldatenew);

        Common com = new Common();
        Dictionary<string, string> restIds = com.getRestaurentIdandName();
                        
        OPSaleFinder saleFinder = new OPSaleFinder();
        SqlDataReader SaleInfoReader = null;
        Dictionary<string, string> dict = null;

        Dictionary<string, decimal> restDetails = new Dictionary<string,decimal>();
         //string titlesempsales[]=new string[];

        try
        {
            {
                using (SqlConnection conn = mConnection.GetConnection())
                {
                    try
                    {
                        conn.Open();

                        foreach (string rest_Id in restIds.Keys)
                        {
                            dict = new Dictionary<string, string>() { { "Rest_ID", rest_Id } };
                    
                            string restName;
                            bool res = restIds.TryGetValue(rest_Id, out restName);

                            SaleInfoReader = SqlHelper.ExecuteReader(conn, CommandType.Text, Qry.GetSaleInfoForXReportSQL(dict, fromdate, tilldate));

                            while (SaleInfoReader.Read())
                            {
                                TipAmount = (decimal)SaleInfoReader["TipAmount"];
                                TaxAmt = (decimal)SaleInfoReader["TotalTax"];
                                SurCharge = (decimal)SaleInfoReader["Surcharge"];
                                Discount = (decimal)SaleInfoReader["Discount"];
                                TotalAmount = (decimal)SaleInfoReader["TotalAmount"];
                            }

                            SaleInfoReader.Close();

                            CashSale = saleFinder.getTotalSaleForPaymentType("1", fromdate, tilldate, rest_Id, conn);
                            CardSale = saleFinder.getTotalSaleForPaymentType("2", fromdate, tilldate, rest_Id, conn);
                            VoucherSale = saleFinder.getTotalSaleForPaymentType("3", fromdate, tilldate, rest_Id, conn);

                            TotalGrossAmt = (CashSale + CardSale + VoucherSale + SurCharge) - Discount;
                            TotalNetAmt = TotalGrossAmt - TaxAmt;

                            TotalRefundAmt = saleFinder.getTotalRefundOrPayout(true, fromdate, tilldate, rest_Id, conn);
                            TotalPayoutAmt = saleFinder.getTotalRefundOrPayout(false, fromdate, tilldate, rest_Id, conn);

                            z_report.Visible = true;
                            this.displayRestaurantSaleDetails(restName,CashSale, CardSale, VoucherSale, TotalRefundAmt, TotalPayoutAmt, TotalGrossAmt, TotalNetAmt, TaxAmt);
                            
                            restDetails.Add(restName, TotalGrossAmt);
                        }
                       
                        Dictionary<string, decimal> labels = new Dictionary<string, decimal> { { "Total Sale", 0 } };

                        this.generatePieChart(Chart1, restDetails.Values.ToArray<decimal>(), restDetails.Keys.ToArray<string>(), labels);
                        this.generateBarChart(Chart2, restDetails.Values.ToArray<decimal>(), restDetails.Keys.ToArray<string>(), labels, "Locations", "Amount(In $)");
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
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void displayRestaurantSaleDetails(string restName,decimal CashSale, decimal CardSale, decimal VoucherSale, decimal TotalRefundAmt, decimal TotalPayoutAmt, decimal grossSale, decimal netSale, decimal TaxAmt)
    {
        // Adding Location Name Header:
        TableCell tc19 = new TableCell();
        TableCell tc20 = new TableCell();
        TableRow tr10 = new TableRow();
       // tr10.Font.Bold = true;
        tr10.BackColor = Color.AliceBlue;
        tc19.Text = "Location:";
        tc20.Text = restName;
        tc19.Font.Bold = true;
        tc20.Font.Bold = true;
        tr10.Controls.Add(tc19);
        tr10.Controls.Add(tc20);

        TableCell tc1 = new TableCell();
        TableCell tc2 = new TableCell();
        TableRow tr1 = new TableRow();
        tr1.BackColor = Color.Gray;
        tr1.ForeColor = Color.White;
        tc1.Text = "Description";
        tc2.Text = "Amount";
        tr1.Controls.Add(tc1);
        tr1.Controls.Add(tc2);


        TableCell tc3 = new TableCell();
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
        tc6.Text = strCurrency+ Convert.ToString(CardSale);
        tr3.Controls.Add(tc5);
        tr3.Controls.Add(tc6);

        TableCell tc7 = new TableCell();
        TableCell tc8 = new TableCell();
        TableRow tr4 = new TableRow();
        tc7.Text = "Voucher";
        tc8.Text = strCurrency+ Convert.ToString(VoucherSale);
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
        tc10.Text = strCurrency+ Convert.ToString(TotalRefundAmt);
        tr5.Controls.Add(tc9);
        tr5.Controls.Add(tc10);

        TableCell tc11 = new TableCell();
        TableCell tc12 = new TableCell();
        TableRow tr6 = new TableRow();
        tc11.Text = "Payout";
        tc12.Text = strCurrency+ Convert.ToString(TotalPayoutAmt);
        tr6.Controls.Add(tc11);
        tr6.Controls.Add(tc12);


        TableCell tc13 = new TableCell();
        TableCell tc14 = new TableCell();
        TableRow tr7 = new TableRow();
        tc13.Text = "TOTAL Gross Sale";
        tc14.Text = strCurrency+ Convert.ToString(TotalGrossAmt);
        tr7.Controls.Add(tc13);
        tr7.Controls.Add(tc14);

        TableCell tc15 = new TableCell();
        TableCell tc16 = new TableCell();
        TableRow tr8 = new TableRow();
        tc15.Text = "TOTAL Net Sale";
        tc16.Text = strCurrency+ Convert.ToString(netSale);
        tr8.Controls.Add(tc15);
        tr8.Controls.Add(tc16);

        TableCell tc17 = new TableCell();
        TableCell tc18 = new TableCell();
        TableRow tr9 = new TableRow();
        tc17.Text = "GST Included (10%)";
        tc18.Text = strCurrency+ Convert.ToString(TaxAmt);
        tr9.Controls.Add(tc17);
        tr9.Controls.Add(tc18);

        salesbylocationtable.Controls.Add(tr10);
        salesbylocationtable.Controls.Add(tr1);
        salesbylocationtable.Controls.Add(tr2);
        salesbylocationtable.Controls.Add(tr3);
        salesbylocationtable.Controls.Add(tr4);
        salesbylocationtable.Controls.Add(tr5);
        salesbylocationtable.Controls.Add(tr6);
        salesbylocationtable.Controls.Add(tr9);
        salesbylocationtable.Controls.Add(tr8);
        salesbylocationtable.Controls.Add(tr7);
       
        
        
    }
    private void generatePieChart(Chart chart, decimal[] yValues, string[] xValues, Dictionary<string, decimal> labels)
    {
        //For generating Pie Chart

        chart.Series[0].Points.DataBindXY(xValues, yValues);

        //Set the colors of the Pie chart
        /*   if (yValues.Length == 1)
               chart.Series[0].Points[0].Color = Color.MediumSeaGreen;
           else
           {
               chart.Series[0].Points[0].Color = Color.MediumSeaGreen;
               chart.Series[0].Points[1].Color = Color.PaleGreen;
               chart.Series[0].Points[2].Color = Color.Red;
           }
         */
        if (yValues.Length <= 1)
        {
            chart.Series[0].Color = Color.FromArgb(154, 196, 84);

        }
        else if (yValues.Length <= 3)
        {
            chart.Series[0].Color = Color.FromArgb(254, 233, 124);
        }
        else
        {
            chart.Series[0].Color = Color.FromArgb(199, 57, 12);
        }


        //Set Pie chart type
        chart.Series[0].ChartType = SeriesChartType.Pie;

        //Set labels style (Inside, Outside, Disabled)
        // chart.Series[0]["PieLabelStyle"] = "Disabled";
        chart.Series[0]["PieLabelStyle"] = "Outside";
        chart.Series[0].Label = strCurrency + "#VALY";
        chart.Series[0].LegendText = "#VALX";

        //Set chart title, color and font
        //Chart1.Titles[0].Text = "Pie Chart Title";
        chart.Titles[0].ForeColor = Color.DarkBlue;
        chart.Titles[0].ShadowColor = Color.LightGray;
        chart.Titles[0].Font = new Font("Arial Black", 14, FontStyle.Bold);

        //Enable 3D
        chart.ChartAreas[0].Area3DStyle.Enable3D = true;

        //Default, SoftEdge, Concave (not enabled if 3D)
        chart.Series[0]["PieDrawingStyle"] = "SoftEdge";

        // Disable/Enable the Legend
        chart.Legends[0].Enabled = true;
        saleschart_report.Visible = true;
       
    }

    private void generateBarChart(Chart chart, decimal[] yValues, string[] xValues, Dictionary<string, decimal> labels, string xTitle, string yTitle)
    {
        // For generating BarChart
        chart.Series[0].Points.DataBindXY(xValues, yValues);

        /*  if (yValues.Length == 5)
            {
                chart.Series[0].Points[0].Color = Color.Black;
                chart.Series[0].Points[1].Color = Color.Bisque;
                chart.Series[0].Points[2].Color = Color.Blue;
                chart.Series[0].Points[3].Color = Color.BlueViolet;
                chart.Series[0].Points[4].Color = Color.Brown;
            }
            else if (yValues.Length == 3)
            {
                chart.Series[0].Points[0].Color = Color.Black;
                chart.Series[0].Points[1].Color = Color.Bisque;
                chart.Series[0].Points[2].Color = Color.Blue;
            }
    
         */
        if (yValues.Length <= 3)
        {
            chart.Series[0].Color = Color.FromArgb(254, 233, 124);
        }
        else if (yValues.Length <= 5)
        {
            chart.Series[0].Color = Color.FromArgb(199, 57, 12);
        }
        else
        {
            chart.Series[0].Color = Color.FromArgb(154, 196, 84);
        }

        //   int[] colors = { 0x66aaee, 0xeebb22, 0xbbbbbb, 0x8844ff, 0xdd2222, 0x009900 };
        //   chart.setColors2(Chart.DataColor, colors);

        //chart.Series[0].Color = Color.FromArgb(18,52, 99);

        //chart.Series[0].Color = Color.Sienna;

        // Chart2.Series["Default3"].Points[5].Color = Color.CornflowerBlue;
        chart.ChartAreas[0].AxisY.Title = yTitle;
        chart.ChartAreas[0].AxisX.Title = xTitle;
        chart.Series[0].ChartType = SeriesChartType.Column;
        chart.ChartAreas[0].Area3DStyle.Enable3D = true;

        saleschart_report.Visible = true;
        
    }
}