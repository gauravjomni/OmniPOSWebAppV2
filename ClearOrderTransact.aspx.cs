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

namespace PosTools
{
    public partial class ClearOrderTransact : System.Web.UI.Page
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

        public ClearOrderTransact()
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
                    Session["bckurl"] = "ViewOrderTransaction.aspx";
                    Server.Transfer("Notification.aspx");
                    return;
                }

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
                            BtnDelete.Visible = true;
                        else
                            BtnDelete.Visible = false;

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

        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            fromdate = iTool.formatInputString(Request.Form["txtFromDate"]);
            tilldate = iTool.formatInputString(Request.Form["txtTillDate"]);

            string confirmValue = Request.Form["confirm_value"];

            if (confirmValue == "Yes")
            {
                try
                {
                    using (SqlConnection conn = mConnection.GetConnection())
                    {
                        conn.Open();

                        using (SqlTransaction trans = conn.BeginTransaction())
                        {
                            try
                            {
                                foreach (RepeaterItem rptItem in OrderTranHistoryRepeater.Items)
                                {
                                    HtmlInputCheckBox chkOrder = (HtmlInputCheckBox)rptItem.FindControl("Order");
                                    HtmlGenericControl OrderID = (HtmlGenericControl)rptItem.FindControl("OrderID");

                                    if (chkOrder.Checked)
                                    {
                                        SqlParameter[] ArParams = new SqlParameter[3];
                                        ArParams[0] = new SqlParameter("@Order_TranID", SqlDbType.VarChar, 50);
                                        ArParams[0].Value = OrderID.InnerHtml;

                                        ArParams[1] = new SqlParameter("@RestID", SqlDbType.Int);
                                        ArParams[1].Value = Session["R_ID"];

                                        ArParams[2] = new SqlParameter("@Mode", SqlDbType.VarChar, 20);
                                        ArParams[2].Value = "del";

                                        SqlHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, "SP_omni_ClearOrderTrans", ArParams);

                                    }
                                }
                                trans.Commit();

                                
                                Dictionary<string, string> dict;
                                dict = new Dictionary<string, string>() { { "a.Rest_ID", Session["R_ID"].ToString() } };
                                ds = Fn.LoadOrderTransactionData(dict, fromdate, tilldate, StrCurrency, conn);

                                if (ds.Tables[0].Rows.Count > 0)
                                    BtnDelete.Visible = true;
                                else
                                    BtnDelete.Visible = false;

                                OrderTranHistoryRepeater.DataSource = ds;
                                OrderTranHistoryRepeater.DataBind();
                            }
                            catch (Exception ex)
                            {
                                trans.Rollback();
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

          }

        protected void OrderTranHistoryRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
//                HtmlInputCheckBox chkOrder = (HtmlInputCheckBox)e.Item.FindControl("Order");
//                chkOrder.Attributes.Add("onclick", "checkitem('" + chkOrder.ClientID + "')");
            }
            else if (e.Item.ItemType == ListItemType.Footer)
            {
                if (OrderTranHistoryRepeater.Items.Count < 1)
                {
                    Label lblFooter = (Label)e.Item.FindControl("lblEmptyData");
                    lblFooter.Visible = true;
                }
            }
        }
   }   
}