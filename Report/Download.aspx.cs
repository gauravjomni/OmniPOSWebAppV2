using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Serialization;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;


public partial class Admin_Download : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Request.UrlReferrer == null)
            {
                Response.Redirect("~/Default.aspx?sessionexpired");
            }
            else
            {
                if (Request.QueryString["exp"] != null && Request["hdnFrom"] != null)
                {
                    string exportTo = Request.QueryString["exp"].ToString();
                    switch (exportTo)
                    {
                        case "excel":
                            DownloadExcel();
                            break;
                        case "pdf":
                            DownloadPDF();
                            //DownloadData();
                            break;
                        case "csv":
                            DownloadCSV();
                            break;
                        case "xml":
                            DownloadXML();
                            break;
                        case "json":
                            DownloadJson();
                            break;
                    }
                }
            }
        }
    }

    protected string ImagePath()
    {
        return @"http://localhost:1729/v2.omniposweb.com/image/chart/1/BarChartLegend.png";
    }

    private void DownloadPDF()
    {
        if (Request["hdnFrom"] == null)
            return;

        var objSale = (Sale)(new JavaScriptSerializer()).Deserialize(Request["hdnFrom"].ToString(), typeof(Sale));

        var objChartSale = new List<ChartSale>()
        {
            //new ChartSale{ Name="Chart", Value=@"file:///D:/Arun/Freelancing/OmniPos/v2.omniposweb.com/Image/Chart/1/BarChartLegend.png"},
            new ChartSale{ Name="Cash", Value=Convert.ToString(objSale.CashSale)},
            new ChartSale{ Name="Card", Value=Convert.ToString(objSale.CardSale)},
            new ChartSale{ Name="Voucher", Value=Convert.ToString(objSale.VoucherSale)},
            new ChartSale{ Name="Total Discount", Value=Convert.ToString(objSale.Discount)},
            new ChartSale{ Name="Total Surcharge", Value=Convert.ToString(objSale.SurCharge)},
            new ChartSale{ Name="FLOAT(+)", Value=Convert.ToString(objSale.TotalFloatAmt)},
            new ChartSale{ Name="REFUND(-)", Value=Convert.ToString(objSale.TotalRefundAmount)},
            new ChartSale{ Name="PAYOUT(-)", Value=Convert.ToString(objSale.TotalPayoutAmount)},
            new ChartSale{ Name="TIPS(+)", Value=Convert.ToString(objSale.TipAmount)},
            new ChartSale{ Name="GST Included (10 %)", Value=Convert.ToString(objSale.TaxAmount)},
            new ChartSale{ Name="No Sale Count", Value=Convert.ToString(objSale.NoSaleCount)},
            new ChartSale{ Name="TOTAL Net Sale", Value=Convert.ToString(objSale.TotalNetAmount)},
            new ChartSale{ Name="TOTAL Gross Sale", Value=Convert.ToString(objSale.TotalGrossAmount)}
        };

        foreach (var drawer in objSale.Drawers)
        {
            objChartSale.Add(new ChartSale { Name = "Total in DRAWER (" + drawer.PrinterName + ")", Value = Convert.ToString(drawer.CashSale - (drawer.PayOut + drawer.PayRefund)) });
        }

        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment;filename=SaleReport.pdf");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);

        gvDownload.AllowPaging = false;
        gvDownload.DataSource = objChartSale;
        gvDownload.DataBind();
        
        //Change the Header Row back to white color
        gvDownload.HeaderRow.Style.Add("background-color", "#E7E7E7");

        //Apply style to Individual Cells
        var sb = new StringBuilder();
        for (var k = 0; k < gvDownload.Columns.Count; k++)
        {
            gvDownload.HeaderRow.Cells[k].Style.Add("background-color", "#E7E7E7");
        }

        for (int i = 0; i < gvDownload.Rows.Count; i++)
        {
            GridViewRow row = gvDownload.Rows[i];

            //Change Color back to white
            row.BackColor = System.Drawing.Color.White;

            //Apply text style to each Row
            row.Attributes.Add("class", "textmode");

            //Apply style to Individual Cells of Alternating Row
            if (i % 2 != 0)
            {
                for (var k = 0; k < gvDownload.Columns.Count; k++)
                {
                    row.Cells[k].Style.Add("background-color", "#CCCCCC");
                }
            }
        }

        gvDownload.RenderControl(hw);
        StringReader sr = new StringReader(sw.ToString());
        Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
        HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
        PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        pdfDoc.Open();
        htmlparser.Parse(sr);
        pdfDoc.Close();
        Response.Write(pdfDoc);
        Response.End();
    }

    private void DownloadExcel()
    {
        if (Request["hdnFrom"] == null)
            return;

        var objSale = (Sale)(new JavaScriptSerializer()).Deserialize(Request["hdnFrom"].ToString(), typeof(Sale));

        var objChartSale = new List<ChartSale>()
        {
            new ChartSale{ Name="Cash", Value=Convert.ToString(objSale.CashSale)},
            new ChartSale{ Name="Card", Value=Convert.ToString(objSale.CardSale)},
            new ChartSale{ Name="Voucher", Value=Convert.ToString(objSale.VoucherSale)},
            new ChartSale{ Name="Total Discount", Value=Convert.ToString(objSale.Discount)},
            new ChartSale{ Name="Total Surcharge", Value=Convert.ToString(objSale.SurCharge)},
            new ChartSale{ Name="FLOAT(+)", Value=Convert.ToString(objSale.TotalFloatAmt)},
            new ChartSale{ Name="REFUND(-)", Value=Convert.ToString(objSale.TotalRefundAmount)},
            new ChartSale{ Name="PAYOUT(-)", Value=Convert.ToString(objSale.TotalPayoutAmount)},
            new ChartSale{ Name="TIPS(+)", Value=Convert.ToString(objSale.TipAmount)},
            new ChartSale{ Name="GST Included (10 %)", Value=Convert.ToString(objSale.TaxAmount)},
            new ChartSale{ Name="No Sale Count", Value=Convert.ToString(objSale.NoSaleCount)},
            new ChartSale{ Name="TOTAL Net Sale", Value=Convert.ToString(objSale.TotalNetAmount)},
            new ChartSale{ Name="TOTAL Gross Sale", Value=Convert.ToString(objSale.TotalGrossAmount)}
        };

        foreach (var drawer in objSale.Drawers)
        {
            objChartSale.Add(new ChartSale { Name = "Total in DRAWER (" + drawer.PrinterName + ")", Value = Convert.ToString(drawer.CashSale - (drawer.PayOut + drawer.PayRefund)) });
        }

        Response.Clear();
        Response.Buffer = true;

        //Response.AddHeader("content-disposition","attachment;filename=GridViewExport.csv");
        Response.AddHeader("content-disposition", "attachment;filename=SaleReport.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);

        gvDownload.AllowPaging = false;
        gvDownload.DataSource = objChartSale;
        gvDownload.DataBind();

        //Change the Header Row back to white color
        gvDownload.HeaderRow.Style.Add("background-color", "#E7E7E7");

        //Apply style to Individual Cells
        var sb = new StringBuilder();
        for (var k = 0; k < gvDownload.Columns.Count; k++)
        {
            gvDownload.HeaderRow.Cells[k].Style.Add("background-color", "#E7E7E7");
        }

        for (int i = 0; i < gvDownload.Rows.Count; i++)
        {
            GridViewRow row = gvDownload.Rows[i];

            //Change Color back to white
            row.BackColor = System.Drawing.Color.White;

            //Apply text style to each Row
            row.Attributes.Add("class", "textmode");

            //Apply style to Individual Cells of Alternating Row
            if (i % 2 != 0)
            {
                for (var k = 0; k < gvDownload.Columns.Count; k++)
                {
                    row.Cells[k].Style.Add("background-color", "#CCCCCC");
                }
            }
        }
        gvDownload.RenderControl(hw);

        //style to format numbers to string
        string style = @"<style> .textmode { mso-number-format:\@; } </style>";
        Response.Write(style);
        Response.Output.Write(sw.ToString());
        Response.Flush();
        Response.End();
    }

    private void DownloadCSV()
    {
        //if (Request["hidFrom"] != null && Request["hdnToDate"] != null)
        if (Request["hdnFrom"] == null)
            return;

        var objSale = (Sale)(new JavaScriptSerializer()).Deserialize(Request["hdnFrom"].ToString(), typeof(Sale));
        var fromStr = Request["hdnFrom"];
        var toStr = Request["hdnTo"];

        if (1 == 1)
        {
            Server.ScriptTimeout = 600000;

            //Get the Reponse Ready for Exporting
            Response.Clear();
            Response.Buffer = true;

            //Set the name of your CSV file here
            Response.AddHeader("content-disposition", "attachment;filename=SaleReport.csv");
            Response.Charset = "";
            Response.ContentType = "application/text";

            EnableViewState = false;

            var sb = new StringBuilder();
            sb.Append("Description,Amount in ($)");
            sb.Append("\r\n");
            sb.Append("Cash,"); sb.Append(objSale.CashSale); sb.Append("\r\n");
            sb.Append("Card,"); sb.Append(objSale.CardSale); sb.Append("\r\n");
            sb.Append("Voucher,"); sb.Append(objSale.VoucherSale); sb.Append("\r\n");
            sb.Append("Total Discount,"); sb.Append(objSale.Discount); sb.Append("\r\n");
            sb.Append("Total Surcharge,"); sb.Append(objSale.SurCharge); sb.Append("\r\n");
            sb.Append("FLOAT(+),"); sb.Append(objSale.TotalFloatAmt); sb.Append("\r\n");
            sb.Append("REFUND(-),"); sb.Append(objSale.TotalRefundAmount); sb.Append("\r\n");
            sb.Append("PAYOUT(-),"); sb.Append(objSale.TotalPayoutAmount); sb.Append("\r\n");
            sb.Append("TIPS(+),"); sb.Append(objSale.TipAmount); sb.Append("\r\n");
            sb.Append("GST Included (10 %),"); sb.Append(objSale.TaxAmount); sb.Append("\r\n");
            sb.Append("No Sale Count,"); sb.Append(objSale.NoSaleCount); sb.Append("\r\n");
            sb.Append("TOTAL Net Sale,"); sb.Append(objSale.TotalNetAmount); sb.Append("\r\n");
            sb.Append("<b>TOTAL Gross Sale</b>,"); sb.Append(objSale.TotalGrossAmount); sb.Append("\r\n");
            //Iterates through each of your rows
            foreach (var drawer in objSale.Drawers)
            {
                sb.Append("Total in DRAWER (" + drawer.PrinterName + "),"); sb.Append(drawer.CashSale - (drawer.PayOut + drawer.PayRefund));
                sb.Append("\r\n");
            }

            //Writes out your CSV File
            Response.Output.Write(sb.ToString());
            Response.Flush();
            Response.End();
        }
        //else
        //{
        //    Page.ClientScript.RegisterStartupScript(GetType(), "S1", "<script>alert('Sorry, no order data found for selected date range.');history.go(-1);</script>");
        //}
    }

    private void DownloadXML()
    {
        //if (Request["hidFrom"] != null && Request["hdnToDate"] != null)
        if (Request["hdnFrom"] == null)
            return;

        var objSale = (Sale)(new JavaScriptSerializer()).Deserialize(Request["hdnFrom"].ToString(), typeof(Sale));
        var fromStr = Request["hdnFrom"];
        var toStr = Request["hdnTo"];

        if (1 == 1)
        {
            Server.ScriptTimeout = 600000;

            //Get the Reponse Ready for Exporting
            Response.Clear();
            Response.Buffer = true;

            Response.AddHeader("content-disposition", "attachment;filename=SaleReport.xml");
            Response.Charset = "";
            Response.ContentType = "application/xml";

            EnableViewState = false;

            var stringwriter1 = new System.IO.StringWriter();
            var serializer1 = new XmlSerializer(typeof(Sale));
            serializer1.Serialize(stringwriter1, objSale);

            string xmlText1 = stringwriter1.ToString();

            Response.Output.Write(xmlText1);
            Response.Flush();
            Response.End();
        }
        //else
        //{
        //    Page.ClientScript.RegisterStartupScript(GetType(), "S1", "<script>alert('Sorry, no order data found for selected date range.');history.go(-1);</script>");
        //}
    }

    private void DownloadJson()
    {
        //if (Request["hidFrom"] != null && Request["hdnToDate"] != null)
        if (Request["hdnFrom"] == null)
            return;

        var objSale = (Sale)(new JavaScriptSerializer()).Deserialize(Request["hdnFrom"].ToString(), typeof(Sale));
        var fromStr = Request["hdnFrom"];
        var toStr = Request["hdnTo"];

        if (1 == 1)
        {
            Server.ScriptTimeout = 600000;

            //Get the Reponse Ready for Exporting
            Response.Clear();
            Response.Buffer = true;

            Response.AddHeader("content-disposition", "attachment;filename=SaleReport.txt");
            Response.ContentType = "application/json";
            Response.Charset = "";

            EnableViewState = false;

            Response.Output.Write(Request["hdnFrom"].ToString());
            Response.Flush();
            Response.End();
        }
        //else
        //{
        //    Page.ClientScript.RegisterStartupScript(GetType(), "S1", "<script>alert('Sorry, no order data found for selected date range.');history.go(-1);</script>");
        //}
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        //base.VerifyRenderingInServerForm(control);
    }

    #region Download XML
    /// <summary>
    /// Download Order Data
    /// </summary>
    private void DownloadData(string exportTo)
    {
        //if (Request["hidFrom"] != null && Request["hdnToDate"] != null)
        if (Request["hdnFrom"] == null)
            return;

        var objSale = (Sale)(new JavaScriptSerializer()).Deserialize(Request["hdnFrom"].ToString(), typeof(Sale));
        var fromStr = Request["hdnFrom"];
        var toStr = Request["hdnTo"];

        if (1 == 1)
        {
            Server.ScriptTimeout = 600000;

            //Get the Reponse Ready for Exporting
            Response.Clear();
            Response.Buffer = true;

            //Set the name of your CSV file here
            var fileName = string.Empty;
            switch (exportTo)
            {
                case "xml":
                    fileName = "SaleReport.xml";
                    Response.ContentType = "application/xml";

                    var stringwriter1 = new System.IO.StringWriter();
                    var serializer1 = new XmlSerializer(typeof(Sale));
                    serializer1.Serialize(stringwriter1, objSale);

                    string xmlText1 = stringwriter1.ToString();
                    break;
                case "json":
                    fileName = "SaleReport.json";
                    Response.ContentType = "application/json";
                    break;
            }
            Response.AddHeader("content-disposition", "attachment;filename=" + fileName);
            Response.Charset = "";

            EnableViewState = false;

            Response.Output.Write(Request["hdnFrom"].ToString());
            Response.Flush();
            Response.End();
        }
        //else
        //{
        //    Page.ClientScript.RegisterStartupScript(GetType(), "S1", "<script>alert('Sorry, no order data found for selected date range.');history.go(-1);</script>");
        //}
    }
    #endregion
}