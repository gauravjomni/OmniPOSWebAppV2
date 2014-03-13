using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyDB;
using MyQuery;
using MyTool;
using Commons;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;

using System.Web.UI.DataVisualization.Charting;
using System.Drawing;
using System.Web.UI.DataVisualization;

public partial class Default3 : System.Web.UI.Page
{
    public string StrCurrency = string.Empty;
  
    public string CurrDateTime = String.Format("{0:dd-MM-yyyy hh:mm:ss}", DateTime.Now);
    public string fromdate = string.Empty;
    public string tilldate = string.Empty;
    public string fromdatenew = string.Empty;
    public string tilldatenew = string.Empty;
    public string fromdater = string.Empty;
    public string tilldater = string.Empty;

    DB mConnection = new DB();
    Common Fn = new Common();
    MyToolSet iTool = new MyToolSet();
    SQLQuery Qry = new SQLQuery();
    DataSet ds = new DataSet();

    DataSet employeeDataSet = new DataSet();
    DataSet subCategoryDataSet = new DataSet();
    DataSet subQtyDataSet = new DataSet();
    
    //data variables
    decimal cash = 0, card = 0, voucher = 0, refund = 0, payout = 0, gst = 0, surcharge = 0, discount = 0;
    decimal grossSale = 0, netSale = 0;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        
       // Label11.Visible = false;
        if (Session["Currency"] != null && Session["Currency"] != "")
            StrCurrency = Session["Currency"].ToString();

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
    /*    if (Header_City != null && Header_City != "")
        { City.Text = Header_City;
         City.Visible = true;
        }
        if (Header_State != null && Header_State != "")
        {
            State.Text = Header_State;
            State.Visible = true;
        }
        if (Header_Zip != null && Header_Zip != "")
        { Zip.Text = Header_Zip;
        Zip.Visible = true;
        }
        if (Header_Email != null && Header_Email != "")
        { email.Text = Header_Email;
            email.Visible = true;
        }
        if (Header_ABN != null && Header_ABN != "")
        { Abn.Text = Header_ABN;
          Abn.Visible = true;
        }
*/
    }
    protected void viewReportAction(object sender, EventArgs e)
    {
        //load the report first
        this.loadSalesReport();

        string[] titles = { "Cash", "Card", "Voucher", "Refund", "Payout", "GST"};
        decimal[] values = { cash, card, voucher, refund, payout, gst };
        Dictionary<string, decimal> labels = new Dictionary<string, decimal> { { "Net Sale", netSale }, { "Gross Sale", grossSale } };

        //Chart chart = new Chart();
        this.generatePieChart(Chart1, values, titles, labels);
        this.generateBarChart(Chart2, values, titles, labels,"Total Sale","Amount(In $)");
    
        //call employee chart report here
        generateEmployeCHartReport();
        generateTotalSales();
        //Call sub category report chart here
        generateSubCategoryChartReport();
        generateSubCategoryQtyChartReport();
      
    }

    //loads sales report such as cash, card, voucher, net & gross sale for particular date range
    private void loadSalesReport()
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
                        Dictionary<string, string> dict1;
                        Dictionary<string, string> dict2;
                        

                        dict = new Dictionary<string, string>() { { "Rest_ID", Session["R_ID"].ToString() } };

                        SqlDataReader SaleInfoReader = SqlHelper.ExecuteReader(trans, CommandType.Text, Qry.GetSaleInfoForXReportSQL(dict, fromdate,tilldate));

                        decimal totalAmount = 0;
                        while (SaleInfoReader.Read())
                        {
                            gst = (decimal)SaleInfoReader["TotalTax"];
                            surcharge = (decimal)SaleInfoReader["Surcharge"];
                            discount = (decimal)SaleInfoReader["Discount"];
                            totalAmount = (decimal)SaleInfoReader["TotalAmount"];
                        }

                        SaleInfoReader.Close();

                        dict = new Dictionary<string, string>() { { "Rest_ID", Session["R_ID"].ToString() }, { "b.PaymentTypeID", "1" } };
                        SqlDataReader TotalSaleByPaymentTypeReader = SqlHelper.ExecuteReader(trans, CommandType.Text, Qry.GetTotalSaleByPaymentTypeForXReportSQL(dict, fromdate,tilldate));

                        while (TotalSaleByPaymentTypeReader.Read())
                        {
                            cash = (decimal)TotalSaleByPaymentTypeReader["PaidAmount"];
                        }

                        TotalSaleByPaymentTypeReader.Close();

                        dict = new Dictionary<string, string>() { { "Rest_ID", Session["R_ID"].ToString() }, { "b.PaymentTypeID", "2" } };
                        TotalSaleByPaymentTypeReader = SqlHelper.ExecuteReader(trans, CommandType.Text, Qry.GetTotalSaleByPaymentTypeForXReportSQL(dict, fromdate,tilldate));

                        while (TotalSaleByPaymentTypeReader.Read())
                        {
                            card = (decimal)TotalSaleByPaymentTypeReader["PaidAmount"];
                        }

                        TotalSaleByPaymentTypeReader.Close();

                        dict = new Dictionary<string, string>() { { "Rest_ID", Session["R_ID"].ToString() }, { "b.PaymentTypeID", "3" } };
                        TotalSaleByPaymentTypeReader = SqlHelper.ExecuteReader(trans, CommandType.Text, Qry.GetTotalSaleByPaymentTypeForXReportSQL(dict, fromdate,tilldate));

                        while (TotalSaleByPaymentTypeReader.Read())
                        {
                            voucher = (decimal)TotalSaleByPaymentTypeReader["PaidAmount"];
                        }

                        TotalSaleByPaymentTypeReader.Close();

                        grossSale = (card + cash + voucher + surcharge) - discount;
                        netSale = grossSale - gst;

                        dict = new Dictionary<string, string>() { { "Rest_ID", Session["R_ID"].ToString() } };
                        SqlDataReader RefundAmountReader = SqlHelper.ExecuteReader(trans, CommandType.Text, Qry.GetTotalRefundForXReportSQL(dict, fromdate,tilldate));

                        while (RefundAmountReader.Read())
                        {
                            refund = (decimal)RefundAmountReader["Amount"];
                        }
                        RefundAmountReader.Close();

                        dict = new Dictionary<string, string>() { { "Rest_ID", Session["R_ID"].ToString() } };
                        SqlDataReader PayoutAmountReader = SqlHelper.ExecuteReader(trans, CommandType.Text, Qry.GetTotalPayoutForXReportSQL(dict, fromdate,tilldate));

                        while (PayoutAmountReader.Read())
                        {
                            payout = (decimal)PayoutAmountReader["Amount"];
                        }
                        PayoutAmountReader.Close();
                        // For displaying Gross Sale and Net Sale starts:
                        Title testTitle = new Title();
                        Title testTitle1 = new Title();
                        testTitle1.Text = "NetSale:"+ Convert.ToString(netSale);
                        testTitle1.Font = new Font(FontFamily.GenericSansSerif, 9F, FontStyle.Regular);
                        testTitle1.IsDockedInsideChartArea = false;
                        testTitle1.Docking = Docking.Bottom;
                        //   testTitle.TextOrientation = TextOrientation.Auto;
                        testTitle1.DockedToChartArea = Chart1.ChartAreas[0].Name;
                        Chart1.Titles.Add(testTitle1);

                         testTitle.Text = "Gross Sale:"+ Convert.ToString(grossSale);
                        testTitle.Font = new Font(FontFamily.GenericSansSerif, 9F, FontStyle.Regular);
                        testTitle.IsDockedInsideChartArea = false;
                        testTitle.Docking = Docking.Bottom;
                        //   testTitle.TextOrientation = TextOrientation.Auto;
                        testTitle.DockedToChartArea = Chart1.ChartAreas[0].Name;
                        Chart1.Titles.Add(testTitle);
                        // For displaying Gross Sale and Net Sale ends:
                        //load employe sales
                        dict = new Dictionary<string, string>() { { "Rest_ID", Session["R_ID"].ToString() } };
                        employeeDataSet = Fn.LoadEmployeeBreakUpSaleForZReport(dict, fromdate, tilldate);
                        // load sub category for sales:
                        dict1 = new Dictionary<string, string>() { { "a.Rest_ID", Session["R_ID"].ToString() } };
                        subCategoryDataSet = Fn.LoadCategoryBreakUpSaleForZReport(dict1, fromdate, tilldate);

                        dict2=new Dictionary<string, string>() { { "a.Rest_ID", Session["R_ID"].ToString() } };
                        subQtyDataSet = SqlHelper.ExecuteDataset(trans, CommandType.Text, Qry.GetCategoryQtyBreakUpSaleForZReportSQL(dict2, fromdate, tilldate));

                                               
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
        { }       
    }

    private void generateTotalSales()
    {

        decimal allTotal = 0;
        if (employeeDataSet.Tables[0].Rows.Count > 0)
        {
            string[] titlesempsales = new string[employeeDataSet.Tables[0].Rows.Count];
            decimal[] valuesempsales = new decimal[employeeDataSet.Tables[0].Rows.Count];

            int i = 0;
            foreach (DataRow inner_dr in employeeDataSet.Tables[0].Rows)
            {
                if (inner_dr["Employee"].ToString().Length > 0)
                {
                    titlesempsales[i] = inner_dr["Employee"].ToString();

                    decimal total = 0;
                    if (inner_dr["Total"].ToString().Length > 0)
                    {
                        total = Convert.ToDecimal(inner_dr["Total"].ToString());
                        allTotal += Convert.ToDecimal(inner_dr["Total"].ToString());
                    }

                    valuesempsales[i] = total;

                    
                    i++;
                }
            }

            Dictionary<string, decimal> labelsempsales = new Dictionary<string, decimal> { { "Total", allTotal } };
            this.generatePieChart(Chart3, valuesempsales, titlesempsales, labelsempsales);
            this.generateBarChart(Chart4, valuesempsales, titlesempsales, labelsempsales,"Employees","Amount(In $)");
            // for displaying total label starts:
            Title testTitle1 = new Title();
            testTitle1.Text = "Total:" + Convert.ToString(allTotal);
            testTitle1.Font = new Font(FontFamily.GenericSansSerif, 9F, FontStyle.Regular);
            testTitle1.IsDockedInsideChartArea = false;
            testTitle1.Docking = Docking.Bottom;
            //testTitle1.TextOrientation = TextOrientation.Auto;
            testTitle1.DockedToChartArea = Chart3.ChartAreas[0].Name;
            Chart3.Titles.Add(testTitle1);
            //ends here

        }
    }


    private void generateEmployeCHartReport()
    {
        if (employeeDataSet.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow inner_dr in employeeDataSet.Tables[0].Rows)
            {
                decimal total = 0;
                if (inner_dr["Total"].ToString().Length > 0)
                { total = Convert.ToDecimal(inner_dr["Total"].ToString()); }

                if (total > 0)
                {
                    string empName = inner_dr["Employee"].ToString();

                    decimal cash = 0;

                    if (inner_dr["CashSale"].ToString().Length > 0)
                    { cash = Convert.ToDecimal(inner_dr["CashSale"].ToString()); }
                    decimal card = 0;
                    if (inner_dr["CardSale"].ToString().Length > 0)
                    { card = Convert.ToDecimal(inner_dr["CardSale"].ToString()); }
                    decimal voucher = 0;
                    if (inner_dr["VoucherSale"].ToString().Length > 0)
                    { voucher = Convert.ToDecimal(inner_dr["VoucherSale"].ToString()); }


                    string[] titlesemp = { "Cash", "Card", "Voucher" };
                    decimal[] valuesemp = { cash, card, voucher };
                    Dictionary<string, decimal> labelsemp = new Dictionary<string, decimal> { { "Total", total } };

                    Label11.Visible = true;
                    TableCell tcel = new TableCell();
                    TableCell tce2 = new TableCell();
                    TableCell tce3 = new TableCell();
                    TableCell tce4 = new TableCell();

                    //trial..
                    System.Web.UI.WebControls.Label lm = new Label();
                    lm.Font.Bold = true;
                    lm.Text = "Employee";
                    tce3.Controls.Add(lm);
                    System.Web.UI.WebControls.Label ll = new Label();
                    ll.Font.Bold = true;
                    ll.Text = empName;
                    tce4.Controls.Add(ll);
                    // trail ends

                    tcel.Text = "Employee report";
                    Chart empChart = new Chart();
                    Chart empChart2 = new Chart();
                    empChart.Height = 300;
                    empChart.Width = 400;
                    empChart2.Height = 300;
                    empChart2.Width = 400;
                    Series ser = new Series();
                    Series seremp = new Series();

                    Title t = new Title();
                    t.Name = "Title1";
                    empChart.Titles.Add(t);
                    Title t1 = new Title();
                    t1.Name = "Title2";
                    empChart2.Titles.Add(t1);

                    ChartArea ca = new ChartArea();
                    ca.Name = "ChartArea1";
                    ChartArea ch = new ChartArea();
                    ch.Name = "NewChart";

                    empChart.ChartAreas.Add(ca);
                    empChart2.ChartAreas.Add(ch);

                    Legend lg = new Legend();
                    lg.Name = "Default3";
                    lg.Docking = Docking.Bottom;
                    lg.IsTextAutoFit = true;
                    lg.LegendStyle = LegendStyle.Row;
                    empChart.Legends.Add(lg);

                    Legend lg1 = new Legend();
                    lg1.Name = "Default3";
                    lg.Docking = Docking.Bottom;
                    lg1.IsEquallySpacedItems = true;
                    lg1.LegendStyle = LegendStyle.Row;
                    empChart2.Legends.Add(lg1);
                    ser.Name = "Default3";
                    seremp.Name = "Default3";
                    empChart.Series.Add(ser);
                    empChart2.Series.Add(seremp);
                    this.generatePieChart(empChart, valuesemp, titlesemp, labelsemp);
                    this.generateBarChart(empChart2, valuesemp, titlesemp, labelsemp,"Sale","Amount(In $)");

                    tcel.Controls.Add(empChart);
                    tce2.Controls.Add(empChart2);

                    TableRow tr = new TableRow();
                    tr.Controls.Add(tcel);
                    tr.Controls.Add(tce2);
                    TableRow tr1 = new TableRow();
                    tr1.Controls.Add(tce3);
                    tr1.Controls.Add(tce4);
                    table2.Controls.Add(tr1);
                    table2.Controls.Add(tr);
                    // for displaying total label starts:
                    Title testTitle1 = new Title();
                    testTitle1.Text = "Total:" + Convert.ToString(total);
                    testTitle1.Font = new Font(FontFamily.GenericSansSerif, 9F, FontStyle.Regular);
                    testTitle1.IsDockedInsideChartArea = false;
                    testTitle1.Docking = Docking.Bottom;
                    //   testTitle1.TextOrientation = TextOrientation.Auto;
                    testTitle1.DockedToChartArea = Chart1.ChartAreas[0].Name;
                    empChart.Titles.Add(testTitle1);

                    //ends here
                }
               
            }
           
        }
    }

    private void generateSubCategoryChartReport()
    {
        decimal allTotal = 0;
        if (subCategoryDataSet.Tables[0].Rows.Count > 0)
        {
            string[] titlesemp = new string[subCategoryDataSet.Tables[0].Rows.Count];
            decimal[] valuesemp = new decimal[subCategoryDataSet.Tables[0].Rows.Count];
            int i = 0;
            foreach (DataRow inner_dr in subCategoryDataSet.Tables[0].Rows)
            {
                titlesemp[i] = inner_dr["CategoryName"].ToString();
                valuesemp[i] = Convert.ToDecimal(inner_dr["Amount"].ToString());

                allTotal += Convert.ToDecimal(inner_dr["Amount"].ToString());
                i++;
            }
            Dictionary<string, decimal> labelsemp = new Dictionary<string, decimal> { { "Amount", allTotal } };
            Label4.Visible = true;
            this.generatePieChart(Chart5, valuesemp, titlesemp, labelsemp);
            this.generateBarChart(Chart6, valuesemp, titlesemp, labelsemp,"Categories","Amount(In $)");
            // for displaying total label starts:
            Title testTitle1 = new Title();
            testTitle1.Text = "Total:" + Convert.ToString(allTotal);
            testTitle1.Font = new Font(FontFamily.GenericSansSerif, 9F, FontStyle.Regular);
            testTitle1.IsDockedInsideChartArea = false;
            testTitle1.Docking = Docking.Bottom;
            //   testTitle1.TextOrientation = TextOrientation.Auto;
            testTitle1.DockedToChartArea = Chart5.ChartAreas[0].Name;
            Chart5.Titles.Add(testTitle1);
            //Adding title ends here:

        }
    }

    private void generateSubCategoryQtyChartReport()
    {
        decimal allTotal = 0;
        if (subQtyDataSet.Tables[0].Rows.Count > 0)
        {
            string[] titlesemp = new string[subQtyDataSet.Tables[0].Rows.Count];
            decimal[] valuesemp = new decimal[subQtyDataSet.Tables[0].Rows.Count];
            int i = 0;
            foreach (DataRow inner_dr in subQtyDataSet.Tables[0].Rows)
            {
                titlesemp[i] = inner_dr["CategoryName"].ToString();
                valuesemp[i] = Convert.ToDecimal(inner_dr["Quantity"].ToString());

                allTotal += Convert.ToDecimal(inner_dr["Quantity"].ToString());
                i++;
            }
            Dictionary<string, decimal> labelsemp = new Dictionary<string, decimal> { { "Quantity", allTotal } };
            Label5.Visible = true;

            this.generatePieChartQty(Chart7, valuesemp, titlesemp, labelsemp);
            this.generateBarChart(Chart8, valuesemp, titlesemp, labelsemp, "Categories", "Quantity");
            // for displaying total label starts:
            Title testTitle1 = new Title();
            testTitle1.Text = "Total:" + Convert.ToString(allTotal);
            testTitle1.Font = new Font(FontFamily.GenericSansSerif, 9F, FontStyle.Regular);
            testTitle1.IsDockedInsideChartArea = false;
            testTitle1.Docking = Docking.Bottom;
            //   testTitle1.TextOrientation = TextOrientation.Auto;
            testTitle1.DockedToChartArea = Chart7.ChartAreas[0].Name;
            Chart7.Titles.Add(testTitle1);
            //Adding title ends here:

        }
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
        chart.Series[0].Label = StrCurrency + "#VALY";
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
        Employee.Visible = true;
        SubCategory.Visible = true;
    }

    private void generateBarChart(Chart chart,decimal[] yValues, string[] xValues, Dictionary<string, decimal> labels, string xTitle, string yTitle)
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
        Employee.Visible = true;
        SubCategory.Visible = true;

    }
    private void generatePieChartQty(Chart chart, decimal[] yValues, string[] xValues, Dictionary<string, decimal> labels)
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
        chart.Series[0].Label = "#VALY";
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
        Employee.Visible = true;
        SubCategory.Visible = true;
    }
   
    
    
    protected void BtnPrint_Click(object sender, EventArgs e)
    {

    }

    public Docking lables { get; set; }
}