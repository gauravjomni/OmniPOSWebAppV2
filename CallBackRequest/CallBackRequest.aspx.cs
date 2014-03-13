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
using Commons;

public partial class CallBack_CallBack : System.Web.UI.Page
{
    #region Page load

    /// <summary>
    /// Page load event
    /// </summary>
    /// <param name="sender">Sender page object</param>
    /// <param name="e">EventArguments page object</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        // Identified the call coming from which page and for what action need to be performed
        CallIdentification();
    }

    #endregion

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
            if (Request.UrlReferrer.OriginalString.ToLower().Contains("/report/dashboard.aspx"))
            {
                if (CheckUserSession())
                {
                    if (Request.QueryString["exp"] == null)
                    {
                        GetSalesReport();
                    }
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
    #endregion

    #region Check Admin Session
    /// <summary>
    /// Check Admin Session
    /// </summary>
    /// <returns>Boolean</returns>
    private bool CheckUserSession()
    {
        if (ManageSession.User != null)
        {
            var objUser = ManageSession.User;
            return (objUser != null);
        }
        return false;
    }
    #endregion

    #region Get Sales Report
    /// <summary>
    /// Get Sales Report
    /// </summary>
    private void GetSalesReport()
    {
        DateTime? fromDate = null;
        DateTime? toDate = null; ;

        if (!string.IsNullOrEmpty(Request.Params["fromDt"]))
        {
            fromDate = DateTime.Parse(Request.Params["fromDt"]);
        }

        if (!string.IsNullOrEmpty(Request.Params["toDt"]))
        {
            toDate = DateTime.Parse(Request.Params["toDt"]);
        }

        var objDALRequest = new DALRequest();
        var objSale = objDALRequest.GetCurrentSales(fromDate, toDate);

        if (objSale != null)
        {
            if (objSale.ChartSaleData != null)
            {
                RenderBarChart(objSale.ChartSaleData, 1, false, 600, 600);
                RenderPieChart(objSale.ChartSaleData, 1, true, 600, 600);
            }

            Response.Write((new JavaScriptSerializer()).Serialize(objSale));
        }
    }
    #endregion
    #region Bind Chart
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
            ChartArea3DStyle areaStyle = new ChartArea3DStyle(chartArea);
            areaStyle.Rotation = 0;

            Axis yAxis = new Axis(chartArea, AxisName.Y);
            Axis xAxis = new Axis(chartArea, AxisName.X);

            //chartArea.AxisX.Title = "Scales";
            //chartArea.AxisY.Title = dataTblPie.Rows[0]["PositionTitle"].ToString(); //"Position";

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
            string chartFilePath = Server.MapPath("~") + "\\Image\\Chart\\" + reportID.ToString() + (withLegend ? "\\BarChart.png" : "\\BarChart.png");
            chartBarControl.SaveImage(chartFilePath, ChartImageFormat.Png);

            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    #endregion

    #region RenderPieChart
    /// <summary>
    /// 
    /// </summary>
    /// <param name="objChartSale"></param>
    /// <param name="reportID"></param>
    /// <param name="withLegend"></param>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <returns></returns>
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
                                        (objChartSale[iCnt].Name.ToLower().Equals("gross amount") ?
                                            "#0033CC" :
                                            "#0033FF"))))))));
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
            string chartFilePath = Server.MapPath("~") + "\\Image\\Chart\\" + reportID.ToString() + (withLegend ? "\\PieChart.png" : "\\PieChart.png");
            chartPieControl.SaveImage(chartFilePath, ChartImageFormat.Png);

            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    #endregion

    #endregion
}