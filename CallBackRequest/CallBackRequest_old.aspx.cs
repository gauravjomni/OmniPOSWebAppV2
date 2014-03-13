using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using System.Web.UI.DataVisualization.Charting;
using System.IO;
using System.Data;
using System.Drawing;
using System.Xml.Serialization;

public partial class CallBackRequest_CallBackRequest : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SaleReport();

        // Identified the call coming from which page and for what action need to be performed
        //CallIdentification();

        //var objDrawer = new List<Drawer>{
        //    new Drawer{PrinterName="Name1", CardSale=100,CashSale=298, VoucherSale=0,PayOut=102,PayRefund=23},
        //    new Drawer{PrinterName="Name1", CardSale=100,CashSale=298, VoucherSale=0,PayOut=102,PayRefund=23},
        //    new Drawer{PrinterName="Name1", CardSale=100,CashSale=298, VoucherSale=0,PayOut=102,PayRefund=23},
        //    new Drawer{PrinterName="Name1", CardSale=100,CashSale=298, VoucherSale=0,PayOut=102,PayRefund=23},
        //    new Drawer{PrinterName="Name1", CardSale=100,CashSale=298, VoucherSale=0,PayOut=102,PayRefund=23}
        //};

        //var objChartSale = new List<ChartSale>
        //{
        //    new ChartSale{ Name="Name1", Value="Value1"},
        //    new ChartSale{ Name="Name2", Value="Value2"},
        //    new ChartSale{ Name="Name3", Value="Value3"},
        //    new ChartSale{ Name="Name4", Value="Value4"},
        //    new ChartSale{ Name="Name5", Value="Value5"},
        //    new ChartSale{ Name="Name6", Value="Value6"},
        //    new ChartSale{ Name="Name7", Value="Value7"}
        //};

        //var objSale = new Sale
        //{
        //    ChartSaleData = objChartSale,
        //    CardSale = 105,
        //    CashSale = 122,
        //    Discount = 19,
        //    drawers = objDrawer
        //};

        //var stringwriter1 = new System.IO.StringWriter();
        //var serializer1 = new XmlSerializer(typeof(Sale));
        //serializer1.Serialize(stringwriter1, objSale);

        //string xmlText1 = stringwriter1.ToString();

        //var stringReader1 = new System.IO.StringReader(xmlText1);
        //objSale = serializer1.Deserialize(stringReader1) as Sale;
    }

    #region CallIdentification
    /// <summary>
    /// Identified the call coming from which page and for what action need to be performed
    /// </summary>
    private void CallIdentification()
    {
        if (Request.UrlReferrer == null) return;
        try
        {
            // Request for participant section data
            if (Request.UrlReferrer.OriginalString.ToLower().Contains("/reports/dashboard.aspx"))
            {
                if (CheckUserSession())
                {
                    SaleReport();
                }
                else
                {
                    Response.Write("{\"session\":" + (new JavaScriptSerializer()).Serialize("1") + "}");
                }
            }
            else if (Request.UrlReferrer.OriginalString.ToLower().Contains("/admin/search.aspx") &&
                Request.Params["phy"] != null)
            {
                if (CheckUserSession())
                {
                    //PhysicianRemove();
                }
                else
                {
                    Response.Write("{\"session\":" + (new JavaScriptSerializer()).Serialize("1") + "}");
                }
            }
        }
        catch (System.Threading.ThreadAbortException)
        {
            //Respone.End calls the Application_CompleteRequest event to end the request 
            //to prevent that HttpContext.Current.ApplicationInstance.CompleteRequest() can be performed as an optional.
        }
        catch (Exception ex)
        {
            Response.Write("{\"success\":\"0\",\"error\":\"" + ex.Message + "\"}");
        }
        Response.End();
    }

    private void SaleReport()
    {
        var chartXML = @"<ArrayOfChartSale xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema'><ChartSale><Name>Tip Amount</Name><Value>1.000000000000000e+001</Value></ChartSale><ChartSale><Name>Tax Amount</Name><Value>7.565000000000001e+001</Value></ChartSale><ChartSale><Name>SurCharge</Name><Value>0.000000000000000e+000</Value></ChartSale><ChartSale><Name>Discount</Name><Value>7.000000000000000e+000</Value></ChartSale><ChartSale><Name>Total Amount</Name><Value>7.563500000000000e+002</Value></ChartSale><ChartSale><Name>Total Refund Amount</Name><Value>1.000000000000000e+001</Value></ChartSale><ChartSale><Name>Total Payout Amount</Name><Value>1.000000000000000e+001</Value></ChartSale><ChartSale><Name>Cash Sale</Name><Value>4.350000000000000e+002</Value></ChartSale><ChartSale><Name>Card Sale</Name><Value>9.400000000000000e+001</Value></ChartSale><ChartSale><Name>Voucher Sale</Name><Value>0.000000000000000e+000</Value></ChartSale><ChartSale><Name>Sale Count</Name><Value>0.000000000000000e+000</Value></ChartSale><ChartSale><Name>Total Gross Amount</Name><Value>5.220000000000000e+002</Value></ChartSale><ChartSale><Name>Total Net Amount</Name><Value>4.263500000000000e+002</Value></ChartSale></ArrayOfChartSale>";
        var drawerXML = @"<ArrayOfDrawer xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema'><Drawer><PrinterName>Payment Printer</PrinterName><CashSale>142.00</CashSale><CardSale>0.00</CardSale><VoucherSale>0.00</VoucherSale><PayOut>10.00</PayOut><Refund>10.00</Refund></Drawer><Drawer><PrinterName>Payment Printer</PrinterName><CashSale>293.00</CashSale><CardSale>94.00</CardSale><VoucherSale>0.00</VoucherSale><PayOut>0.00</PayOut><Refund>0.00</Refund></Drawer></ArrayOfDrawer>";

        List<ChartSale> objChartSale = null;
        if (!string.IsNullOrEmpty(chartXML))
        {
            var serializer = new XmlSerializer(typeof(List<ChartSale>));
            var stringReader = new StringReader(chartXML);

            objChartSale = serializer.Deserialize(stringReader) as List<ChartSale>;
        }

        List<Drawer> objDrawer = null;
        if (!string.IsNullOrEmpty(drawerXML))
        {
            var serializer = new XmlSerializer(typeof(List<Drawer>));
            var stringReader = new StringReader(drawerXML);

            objDrawer = serializer.Deserialize(stringReader) as List<Drawer>;
        }

        if (!string.IsNullOrEmpty(chartXML))
        {
            var objSale = new Sale
            {
                CashSale = 10,
                CardSale = 10,
                VoucherSale = 0,
                Discount = 0,
                SurCharge = 10,
                TotalRefundAmount = 10,
                TotalPayoutAmount = 10,
                TipAmount = 8,
                TaxAmount = 7,
                NoSaleCount = 0,
                TotalNetAmount = 88,
                TotalGrossAmount = 333,
                Drawers = objDrawer
            };

            //var objBarChartData = new List<BarChartData>
            //{
            //    new BarChartData
            //    {
            //        CashSale = 10,
            //        CardSale = 10,
            //        VoucherSale = 15,
            //        Discount = 40,
            //        Payout = 30
            //        PayRefund=10
            //    }
            //};
            //RenderBarChart(objChartSale, 1, true, 600, 600);

            objChartSale = new List<ChartSale>        {
            new ChartSale{ Name="Refund", Value="100"},
            new ChartSale{ Name="Payout", Value="20"},
            new ChartSale{ Name="Discount", Value="10"},
            new ChartSale{ Name="Voucher", Value="80"},
            new ChartSale{ Name="Card", Value="20"},
            new ChartSale{ Name="Cash", Value="40"}
            };

            RenderBarChart(objChartSale, 1, false, 600, 600);
            RenderPieChart(objChartSale, 1, true, 600, 600);

            //RenderBarChart(objChartSale, 1, true, 600, 600);
        }
    }
    #endregion

    #region Check Admin Session
    /// <summary>
    /// Check Admin Session
    /// </summary>
    /// <returns></returns>
    private bool CheckUserSession()
    {
        if (ManageSession.User != null)
        {
            var objUser = ManageSession.User;
            return (objUser != null);
        }
        return true;
    }
    #endregion

    private bool RenderPieChart(List<ChartSale> objChartSale, int? reportID, bool withLegend, int width, int height)
    {
        try
        {
            // Create a pie chart
            Chart chartPieControl = new Chart();
            chartPieControl.ID = "ChartPie1";

            // Build a pie series
            Series series = new Series("Default");
            series.ChartType = SeriesChartType.Pie;
            series.XValueMember = "Name";
            series.YValueMembers = "Value";
            series.YValueType = ChartValueType.Int32;
            series.IsValueShownAsLabel = true;
            series.ToolTip = "#VALY" + " - " + "#VALX";

            chartPieControl.Series.Add(series);

            // Define the chart area
            ChartArea chartArea = new ChartArea();
            ChartArea3DStyle areaStyle = new ChartArea3DStyle(chartArea);
            areaStyle.Rotation = 0;

            Axis yAxis = new Axis(chartArea, AxisName.Y);
            Axis xAxis = new Axis(chartArea, AxisName.X);

            chartPieControl.ChartAreas.Add(chartArea);

            chartArea.Area3DStyle.Enable3D = true;

            if (withLegend)
            {
                Legend legend = new Legend();
                legend.Name = "PieLegend";
                chartPieControl.Legends.Add(legend);
            }

            var myGreenColorPalette = new Color[objChartSale.Count];
            for (var iCnt = 0; iCnt < objChartSale.Count; iCnt++)
            {
                myGreenColorPalette[iCnt] =
                    ColorTranslator.FromHtml(
                    (objChartSale[iCnt].Name.ToLower().Equals("cash") ?
                        "#52D017" :
                        (objChartSale[iCnt].Name.ToLower().Equals("card") ?
                            "#59E817" :
                            (objChartSale[iCnt].Name.ToLower().Equals("voucher") ?
                                "#7FE817" :
                                (objChartSale[iCnt].Name.ToLower().Equals("discount") ?
                                    "#E55451" :
                                    (objChartSale[iCnt].Name.ToLower().Equals("payout") ?
                                        "#C11B17" :
                                        (objChartSale[iCnt].Name.ToLower().Equals("refund") ?
                                        "#E41B17" :
                                        "#E5E4E2")))))));
            }

            chartPieControl.Palette = ChartColorPalette.None;
            chartPieControl.PaletteCustomColors = myGreenColorPalette;

            chartPieControl.DataSource = objChartSale;
            chartPieControl.DataBind();

            // Save the chart as a 300 x 200 image
            chartPieControl.Width = new System.Web.UI.WebControls.Unit(width, System.Web.UI.WebControls.UnitType.Pixel);
            chartPieControl.Height = new System.Web.UI.WebControls.Unit(height, System.Web.UI.WebControls.UnitType.Pixel);

            if (!Directory.Exists(Server.MapPath("~") + "\\Image\\Chart\\" + reportID.ToString()))
            {
                Directory.CreateDirectory(Server.MapPath("~") + "\\Image\\Chart\\" + reportID.ToString());
            }
            string chartFilePath = Server.MapPath("~") + "\\Image\\Chart\\" + reportID.ToString() + (withLegend ? "\\PieChartLegend.png" : "\\PieChart.png");
            chartPieControl.SaveImage(chartFilePath, ChartImageFormat.Png);

            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    #region Render Pie Chart
    private bool RenderPieChart2(List<ChartSale> objChartSale, int? reportID, bool withLegend, int width, int height)
	{
		try
		{
			// Create a pie chart
			Chart chartPieControl = new Chart();
			chartPieControl.ID = "ChartPie1";

			// Build a pie series
			Series series = new Series("Default");
			series.ChartType = SeriesChartType.Pie;
			series.XValueMember = "Name";
			series.YValueMembers = "Value";
			series.YValueType = ChartValueType.Int32;
			series.IsValueShownAsLabel = true;
			series.ToolTip = "#VALY" + " - " + "#VALX";

			chartPieControl.Series.Add(series);

			// Define the chart area
			ChartArea chartArea = new ChartArea();
			ChartArea3DStyle areaStyle = new ChartArea3DStyle(chartArea);
			areaStyle.Rotation = 0;

			Axis yAxis = new Axis(chartArea, AxisName.Y);
			Axis xAxis = new Axis(chartArea, AxisName.X);

			chartPieControl.ChartAreas.Add(chartArea);

			if (withLegend)
			{
				Legend legend = new Legend();
				legend.Name = "PieLegend";
				chartPieControl.Legends.Add(legend);
			}

            //Color[] myGreenColorPalette = SetLegendColors(dataTblPie);

            //chartPieControl.Palette = ChartColorPalette.None;
            //chartPieControl.PaletteCustomColors = myGreenColorPalette;

			chartPieControl.DataSource = objChartSale;
			chartPieControl.DataBind();

			// Save the chart as a 300 x 200 image
			chartPieControl.Width = new System.Web.UI.WebControls.Unit(width, System.Web.UI.WebControls.UnitType.Pixel);
			chartPieControl.Height = new System.Web.UI.WebControls.Unit(height, System.Web.UI.WebControls.UnitType.Pixel);

			if (!Directory.Exists(Server.MapPath("~") + "\\Image\\Chart\\" + reportID.ToString()))
			{
				Directory.CreateDirectory(Server.MapPath("~") + "\\Image\\Chart\\" + reportID.ToString());
			}
			string chartFilePath = Server.MapPath("~") + "\\Image\\Chart\\" + reportID.ToString() + (withLegend ? "\\PieChartLegend.png" : "\\PieChart.png");
			chartPieControl.SaveImage(chartFilePath, ChartImageFormat.Png);

			return true;
		}
		catch (Exception ex)
		{
			return false;
		}
	}
	#endregion

    #region Render Bar Chart
    /// <summary>
    /// Render Bar Chart
    /// </summary>
    /// <param name="objChartSale">Data Table Pie chart</param>
    /// <param name="reportID">Requested report id</param>
    /// <param name="WithLegend">Chart with legend or not?</param>
    /// <param name="Width">Chart width</param>
    /// <param name="Height">Chart height</param>
    private bool RenderBarChart(List<ChartSale> objChartSale, int? reportID, bool withLegend, int width, int height)
    {
        try
        {
            // Create a bar chart
            Chart chartBarControl = new Chart();
            chartBarControl.ID = "ChartBar1";
            chartBarControl.BorderlineDashStyle = ChartDashStyle.Solid;
            //chartBarControl.Titles = "Sale Summary Report";            

            //chartBarControl.BackColor = Color.White;

            // Build a pie series
            Series series = new Series("Default");
            series.ChartType = SeriesChartType.StackedBar;
            series.XValueMember = "Name";
            series.YValueMembers = "Value";
            series.YValueType = ChartValueType.Int32;
            //series.IsValueShownAsLabel = true;
            series.ToolTip = "#VALY" + " - " + "#VALX";
            chartBarControl.Series.Add(series);

            ChartArea chartArea = new ChartArea();
            //chartArea.Name = "ChartArea1";
            //chartArea.BackColor = Color.WhiteSmoke;
            //chartArea.BackGradientStyle = GradientStyle.TopBottom;
            //chartArea.BorderDashStyle = ChartDashStyle.Solid;
            //chartArea.BorderWidth = 2;
            ChartArea3DStyle areaStyle = new ChartArea3DStyle(chartArea);
            areaStyle.Rotation = 0;

            Axis yAxis = new Axis(chartArea, AxisName.Y);
            Axis xAxis = new Axis(chartArea, AxisName.X);

            //chartArea.AxisX.Title = "AxisX";
            //chartArea.AxisY.Title = "AxisY"; // dataTblPie.Rows[0]["PositionTitle"].ToString(); //"Position";
            //chartArea.AxisX.LabelStyle.Font = new Font("Verdana", 2f, FontStyle.Regular);
            //chartArea.AxisY.LabelStyle.Font = new Font("Verdana", 2f, FontStyle.Regular);

            chartBarControl.ChartAreas.Add(chartArea);

            chartArea.Area3DStyle.Enable3D = false;

            //chartBarControl.ChartAreas[0].AxisX.Interval = 1;

            if (withLegend)
            {
                Legend legend = new Legend();
                legend.Name = "BarLegend";
                legend.Docking = Docking.Top;
                legend.DockedToChartArea = "ChartArea1";
                legend.IsDockedInsideChartArea = false;

                chartBarControl.Legends.Add(legend);
            }

            var myGreenColorPalette = new Color[objChartSale.Count];
            for (var iCnt = 0; iCnt < objChartSale.Count; iCnt++)
            {
                myGreenColorPalette[iCnt] =
                    ColorTranslator.FromHtml(
                    (objChartSale[iCnt].Name.ToLower().Equals("cash") ?
                        "#52D017" :
                        (objChartSale[iCnt].Name.ToLower().Equals("card") ?
                            "#59E817" :
                            (objChartSale[iCnt].Name.ToLower().Equals("voucher") ?
                                "#7FE817" :
                                (objChartSale[iCnt].Name.ToLower().Equals("discount") ?
                                    "#E55451" :
                                    (objChartSale[iCnt].Name.ToLower().Equals("payout") ?
                                        "#C11B17" :
                                        (objChartSale[iCnt].Name.ToLower().Equals("refund") ?
                                        "#E41B17" :
                                        "#E5E4E2")))))));
            }

            //chartBarControl.Palette = ChartColorPalette.None;
            chartBarControl.PaletteCustomColors = myGreenColorPalette;

            chartBarControl.DataSource = objChartSale;
            chartBarControl.DataBind();

            // Save the chart as a 300 x 200 image
            chartBarControl.Width = new System.Web.UI.WebControls.Unit(width, System.Web.UI.WebControls.UnitType.Pixel);
            chartBarControl.Height = new System.Web.UI.WebControls.Unit(height, System.Web.UI.WebControls.UnitType.Pixel);

            if (!Directory.Exists(Server.MapPath("~") + "\\Image\\Chart\\" + reportID.ToString()))
            {
                Directory.CreateDirectory(Server.MapPath("~") + "\\Image\\Chart\\" + reportID.ToString());
            }
            string chartFilePath = Server.MapPath("~") + "\\Image\\Chart\\" + reportID.ToString() + (withLegend ? "\\BarChartLegend.png" : "\\BarChart.png");
            chartBarControl.SaveImage(chartFilePath, ChartImageFormat.Png);

            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    #endregion
}