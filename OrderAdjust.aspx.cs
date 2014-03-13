using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Drawing;
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

namespace CSharpDemoEditGrid
{
    public partial class OrderAdjust : System.Web.UI.Page
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

        public string fromdate;
        public string tilldate;

        public string fromdater = string.Empty;
        public string tilldater = string.Empty;

        decimal totnetamt = 0;
        decimal totgrossamt = 0;
        decimal tottipamt = 0;
        decimal totsurcharge = 0;
        decimal totdiscount = 0;
        decimal tottax = 0;

        public OrderAdjust()
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
             
            if (Session["Currency"] != null && Session["Currency"] != "")
                StrCurrency = Session["Currency"].ToString();

            if (!IsPostBack)
            {

                if (Session["R_ID"] == "" || Session["R_ID"] == null)
                {
                    Session["bckurl"] = "OrderAdjust.aspx";
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
                fromdater = iTool.formatInputString(Request.Form["txtFromDate"]); ;
                tilldater = iTool.formatInputString(Request.Form["txtTillDate"]); ;
            }
            //BtnPrint.Attributes.Add("onclick", "PrintDoc()");
           
            Adjustment.Attributes.Add("onclick", "calcreduceamt('" + HiddenOrderedAmt.ClientID + "','" + HiddenAdjust.ClientID + "','" + FinalAmt.ClientID + "','" + Adjustment.ClientID + "','" + StrCurrency + "')");

            HiddenAdjust.Value = Fn.GetOptionSettings("AdjustmentPercentange");
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            fromdate = iTool.formatInputString(Request.Form["txtFromDate"]);
            tilldate = iTool.formatInputString(Request.Form["txtTillDate"]);

            if (fromdate != "" && Fn.ValidateDate(fromdate))
            {
                fromdate = String.Format("{0:yyyy-MM-dd}", Fn.ConvertDateIntoAnotherFormat2(fromdate));
                //fromdate = Fn.ConvertDateIntoAnotherFormat1(fromdate);
                //LblRepo.InnerText = "From : " + String.Format("{0:dd-MM-yyyy}", Convert.ToDateTime(fromdate));
                fromdater = String.Format("{0:MM/dd/yyyy}", iTool.formatInputString(Request.Form["txtFromDate"]));                     
            }
            if (tilldate != "" && Fn.ValidateDate(tilldate))
            {
                tilldate = String.Format("{0:yyyy-MM-dd}",Fn.ConvertDateIntoAnotherFormat2(tilldate));
                //tilldate = Fn.ConvertDateIntoAnotherFormat1(tilldate);
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
                        ds = Fn.LoadOrderTransactionData(dict,fromdate,tilldate,conn);

/*                        if (ds.Tables[0].Rows.Count > 0)
                            BtnPrint.Visible = true;
                        else
                            BtnPrint.Visible = false;
*/
                        DataGrid1.DataSource = ds;
                        DataGrid1.DataBind();

                        if (Convert.ToDecimal(HiddenOrderedAmt.Value) > 0)
                            OrderSummary.Visible = true;
                        else
                            OrderSummary.Visible = false;
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
            }   
            
        }

        protected void BtnAdjustApply_Click(object sender, EventArgs e)
        {
            string OrderTranIDs = string.Empty;

            fromdate = iTool.formatInputString(Request.Form["txtFromDate"]);
            tilldate = iTool.formatInputString(Request.Form["txtTillDate"]);

            if (fromdate != "" && Fn.ValidateDate(fromdate))
                fromdate = String.Format("{0:yyyy-MM-dd}", Fn.ConvertDateIntoAnotherFormat2(fromdate));

            if (tilldate != "" && Fn.ValidateDate(tilldate))
                tilldate = String.Format("{0:yyyy-MM-dd}", Fn.ConvertDateIntoAnotherFormat2(tilldate));

                DateTime sDate = DateTime.Now;
                sDate = Fn.GetCommonDate(sDate, Session["DateFormat"]);

            try
            {
                using (SqlConnection conn = mConnection.GetConnection())
                {
                    conn.Open();
                    using (SqlTransaction trans = conn.BeginTransaction())
                    {
                        try
                        {

                            SqlParameter[] ArParams = new SqlParameter[6];
                            ArParams[0] = new SqlParameter("@FromDate", SqlDbType.DateTime);
                            ArParams[0].Value = fromdate;

                            ArParams[1] = new SqlParameter("@TillDate", SqlDbType.DateTime);
                            ArParams[1].Value = tilldate;

                            ArParams[2] = new SqlParameter("@Rest_ID", SqlDbType.Int);
                            ArParams[2].Value = Convert.ToInt32(Session["R_ID"]);

                            ArParams[3] = new SqlParameter("@Adjustment", SqlDbType.Decimal);
                            ArParams[3].Value = HiddenAdjust.Value;
                            //ArParams[3].Scale = 2;

                            ArParams[4] = new SqlParameter("@LoggedUserID", SqlDbType.Int);
                            ArParams[4].Value = Session["UserID"]; 

                            ArParams[5] = new SqlParameter("@sDate", SqlDbType.DateTime);
                            ArParams[5].Value = sDate;

                            SqlHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, "SP_omni_Order_Adjustment_Update", ArParams);

                            SqlParameter[] ArParams1 = new SqlParameter[6];
                            ArParams1[0] = new SqlParameter("@FromDate", SqlDbType.DateTime);
                            ArParams1[0].Value = fromdate;

                            ArParams1[1] = new SqlParameter("@TillDate", SqlDbType.DateTime);
                            ArParams1[1].Value = tilldate;

                            ArParams1[2] = new SqlParameter("@Rest_ID", SqlDbType.Int);
                            ArParams1[2].Value = Convert.ToInt32(Session["R_ID"]);

                            ArParams1[3] = new SqlParameter("@OrderTranID", SqlDbType.Text);
                            ArParams1[3].Value = HiddenOrderTransIDs.Value;

                            ArParams1[4] = new SqlParameter("@LoggedUserID", SqlDbType.Int);
                            ArParams1[4].Value = Session["UserID"];

                            ArParams1[5] = new SqlParameter("@sDate", SqlDbType.DateTime);
                            ArParams1[5].Value = sDate;

                            SqlHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, "SP_omni_Order_Adjustment_Log_Update", ArParams1);

                            trans.Commit();

                            //DataGrid1.DataSource = LoadfromDB(fromdate, tilldate);
                            //DataGrid1.DataBind();
                            Message.Visible = true;
                            OrderSummary.Visible= false;
                        }

                        catch (Exception ex)
                        {
                            // throw exception						
                            trans.Rollback();
                            //txtResults.Text = "Transfer Error";
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
                //Response.Write(ex.Message);
                //Response.End();

            } 
        }

        public ICollection ListofDesignations()
        {
            ArrayList a = new ArrayList();
            a.Add("Project Manager");
            a.Add("Project Lead");
            a.Add("Team Lead");
            a.Add("Software Developer");
            return a;
        }

        protected void DataGrid1_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            string footerstring = string.Empty;
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    totnetamt += Convert.ToDecimal(DataBinder.Eval(e.Item.DataItem, "GrossAmount") == null ? 0 : DataBinder.Eval(e.Item.DataItem, "GrossAmount")); 
                    tottipamt += Convert.ToDecimal(DataBinder.Eval(e.Item.DataItem, "TipAmount") == null ? 0 : DataBinder.Eval(e.Item.DataItem, "TipAmount")); 
                    totsurcharge += Convert.ToDecimal(DataBinder.Eval(e.Item.DataItem, "Surcharge") == null ? 0 : DataBinder.Eval(e.Item.DataItem, "Surcharge")); 
                    totdiscount += Convert.ToDecimal(DataBinder.Eval(e.Item.DataItem, "Discount") == null ? 0 : DataBinder.Eval(e.Item.DataItem, "Discount")); 
                    tottax += Convert.ToDecimal(DataBinder.Eval(e.Item.DataItem, "TotalTax") == null ? 0 : DataBinder.Eval(e.Item.DataItem, "TotalTax"));
                    TotalAmount += Convert.ToDecimal(DataBinder.Eval(e.Item.DataItem, "TotalAmount") == null ? 0 : DataBinder.Eval(e.Item.DataItem, "TotalAmount"));
                    HiddenOrderTransIDs.Value += DataBinder.Eval(e.Item.DataItem, "Order_TranID") + ",";
                }
                else if (e.Item.ItemType == ListItemType.Footer)
                {
                    if (DataGrid1.Items.Count < 1)
                    {
                        footerstring = "<tr>";
                        footerstring += "<td colspan=\"9\" align=\"center\">No Data To Display.</td>";
                        footerstring += "</tr>";
                    }
                    else
                    {
                        footerstring += "<tr>";
                        footerstring += "<td colspan=\"4\" style=\"color:red\"><b><i>Total : <i><b></td>";
                        footerstring += "<td style=\"color:red\"><b>" + StrCurrency + totnetamt + "</b></td>";
                        footerstring += "<td style=\"color:red\"><b>" + StrCurrency + tottipamt + "</b></td>";
                        footerstring += "<td style=\"color:red\"><b>" + StrCurrency + totsurcharge + "</b></td>";
                        footerstring += "<td style=\"color:red\"><b>" + StrCurrency + totdiscount + "</b></td>";
                        footerstring += "<td style=\"color:red\"><b>" + StrCurrency + tottax + "</b></td>";
                        footerstring += "<td style=\"color:red\"><b>" + StrCurrency + TotalAmount + "</b></td>";
                        footerstring += "</tr>";
                    }

                    Label lblFooter = (Label)e.Item.FindControl("Footer");
                    lblFooter.Text = footerstring;
                    lblFooter.Visible = true;

                    HiddenOrderedAmt.Value = TotalAmount.ToString();

                    OrderedAmt.Text = "(" + StrCurrency + TotalAmount.ToString() + ")";
                    OrderSummary.Visible = true;

                    if (HiddenOrderTransIDs.Value.Length > 0)
                        HiddenOrderTransIDs.Value = HiddenOrderTransIDs.Value.Substring(0, HiddenOrderTransIDs.Value.Length - 1);
                }

            //}

        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
           // InitializeComponent();
           // base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.DataGrid1.CancelCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid1_CancelCommand);
            this.DataGrid1.EditCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid1_EditCommand);
            this.DataGrid1.UpdateCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DataGrid1_UpdateCommand);
            this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion
        //for handling datagrid Update
        private void DataGrid1_UpdateCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            DataGrid1.EditItemIndex = -1;

            fromdate = iTool.formatInputString(Request.Form["txtFromDate"]);
            tilldate = iTool.formatInputString(Request.Form["txtTillDate"]);

            fromdate = String.Format("{0:yyyy-MM-dd}", Fn.ConvertDateIntoAnotherFormat2(fromdate));
            tilldate = String.Format("{0:yyyy-MM-dd}", Fn.ConvertDateIntoAnotherFormat2(tilldate));

            // ID
            TableCell cell1 = (TableCell)e.Item.Controls[0];
            string orderid = ((Label)cell1.Controls[1]).Text;

            //transactiondate
            TableCell cell2 = (TableCell)e.Item.Controls[1];
            string trandate = ((Label)cell2.Controls[1]).Text;
            //string desg = ((DropDownList)cell2.Controls[3]).SelectedValue;

            //Sex
            /*			string sex = "M";
                        TableCell cell3 = (TableCell)e.Item.Controls[3];
                        if((( RadioButton )cell3.Controls[1]).Checked)
                            sex = "Male";
                        if((( RadioButton )cell3.Controls[3]).Checked)
                            sex = "Female";
            */

            //OrderNo
            TableCell cell3 = (TableCell)e.Item.Controls[2];
            string orderno = ((Label)cell3.Controls[1]).Text;

            //Orderdt
            TableCell cell4 = (TableCell)e.Item.Controls[3];
            string orderdt = ((Label)cell4.Controls[1]).Text;

            //NetAmt
            TableCell cell5 = (TableCell)e.Item.Controls[4];
            string net = ((Label)cell5.Controls[1]).Text;

            //tipAmt
            TableCell cell6 = (TableCell)e.Item.Controls[5];
            string tip = ((Label)cell6.Controls[1]).Text;

            //srcharge
            TableCell cell7 = (TableCell)e.Item.Controls[6];
            string srchg = ((Label)cell7.Controls[1]).Text;

            //Discount
            TableCell cell8 = (TableCell)e.Item.Controls[7];
            string disc = ((Label)cell8.Controls[1]).Text;

            //Tax
            TableCell cell9 = (TableCell)e.Item.Controls[8];
            string tax = ((Label)cell9.Controls[1]).Text;

            //Gross
            TableCell cell10 = (TableCell)e.Item.Controls[9];
            string Gross = ((Label)cell10.Controls[1]).Text;
            
            TableCell cell11 = (TableCell)e.Item.Controls[10];
            string ReduceSaleInPercent = ((TextBox)cell11.Controls[1]).Text;

            try
            {
                using (SqlConnection conn = mConnection.GetConnection())
                {
                    conn.Open();
                    using (SqlTransaction trans = conn.BeginTransaction())
                    {
                        try
                        {
                            SqlParameter[] ArParams = new SqlParameter[3];
                            ArParams[0] = new SqlParameter("@OrderID", SqlDbType.VarChar, 50);
                            ArParams[0].Value = orderid;

                            ArParams[1] = new SqlParameter("@ReduceSaleInPercent", SqlDbType.Decimal);
                            ArParams[1].Value = (ReduceSaleInPercent == null || ReduceSaleInPercent == "") ? "0.00" : ReduceSaleInPercent;

                            ArParams[2] = new SqlParameter("@Rest_ID", SqlDbType.Int);
                            ArParams[2].Value = Convert.ToInt32(Session["R_ID"]);

                            SqlHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, "SP_omni_Order_ReduceSale_Update", ArParams);
                            trans.Commit();

                            DataGrid1.DataSource = LoadfromDB(fromdate, tilldate); 
                            DataGrid1.DataBind();

                        }

                        catch (Exception ex)
                        {
                            // throw exception						
                            trans.Rollback();
                            //txtResults.Text = "Transfer Error";
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

        //for handling datagrid cancel button
        private void DataGrid1_CancelCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            fromdate = iTool.formatInputString(Request.Form["txtFromDate"]);
            tilldate = iTool.formatInputString(Request.Form["txtTillDate"]);

            fromdate = String.Format("{0:yyyy-MM-dd}", Fn.ConvertDateIntoAnotherFormat2(fromdate));
            tilldate = String.Format("{0:yyyy-MM-dd}", Fn.ConvertDateIntoAnotherFormat2(tilldate));

            DataGrid1.EditItemIndex = -1;
            DataGrid1.DataSource = LoadfromDB(fromdate, tilldate);
            DataGrid1.DataBind();
        }

        //for handling datagrid editing
        private void DataGrid1_EditCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            fromdate = iTool.formatInputString(Request.Form["txtFromDate"]);
            tilldate = iTool.formatInputString(Request.Form["txtTillDate"]);

            if (IsPostBack)
            {
                fromdate = String.Format("{0:yyyy-MM-dd}", Fn.ConvertDateIntoAnotherFormat2(fromdate));
                tilldate = String.Format("{0:yyyy-MM-dd}", Fn.ConvertDateIntoAnotherFormat2(tilldate));
                DataGrid1.EditItemIndex = e.Item.ItemIndex;
                DataGrid1.DataSource = LoadfromDB(fromdate, tilldate);
                DataGrid1.DataBind();
            }
            else
            {
                fromdater = fromdate;
                tilldater = tilldate;
            }
        }

        private ICollection LoadfromDB(string fromdate,string tilldate)
        {
            try
            {
                using (SqlConnection conn = mConnection.GetConnection())
                {
                    conn.Open();

                    try
                    {
                        Dictionary<string, string> dict = new Dictionary<string, string>() { { "Rest_ID", Session["R_ID"].ToString() } };
                        ds = Fn.LoadOrderTransactionData(dict, fromdate, tilldate, conn);

                        //if (ds.Tables[0].Rows.Count > 0)
                        //    BtnPrint.Visible = true;
                        //else
                        //    BtnPrint.Visible = false;


                        DataGrid1.DataSource = ds;
                        DataGrid1.DataBind();
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

            return ds.Tables[0].DefaultView;
                //dt.DefaultView;
        }


    }   
}