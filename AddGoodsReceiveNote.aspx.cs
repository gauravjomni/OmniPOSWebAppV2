using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.ApplicationBlocks.Data;
using MyDB;
using MyTool;
using MyQuery;
using Commons;

namespace PosInvoice
{
    public partial class AddGoodsReceiveNote : System.Web.UI.Page
    {
        
        DB mConnection = new DB() ;
        public DataSet ds = new DataSet();
        Common Fn = new Common();
        MyToolSet iTool = new MyToolSet();
        SQLQuery Qry = new SQLQuery();
        //string sQuery = "";

        string grn_id = "";
        string strGRNNo = string.Empty;
        public string strGRNDt = string.Empty;
        string purchid = string.Empty;
        public string strPurchOrdDt = string.Empty;
        string strDeliverToName = string.Empty;
        string strDeliverAddress1 = string.Empty;
        string strDeliverAddress2 = string.Empty;
        public string strTaxAmt = "0.00";
        public string strSubTotalAmt = "0.00";
        public string strDiscountAmt = "0.00";
        public string strTotalAmt = "0.00";
        public string strPaidAmt = "0.00";
        public string strDueAmt = "0.00";
        public string strProduct_Ingredients = string.Empty;
        public int errorinpos = -1;
        string strNote = string.Empty;
        string strPOrderNo = string.Empty;
        Dictionary<string, string> dict = null;
        List<string> list = new List<string>();

        public string[] arrItemName = null;
        public string[] arrProdID = null;
        public string[] arrProdName = null;
        public string[] arrProdUnit = null;
        public string[] arrProdType = null;
        public string[] arrProdCost = null;
        public string[] arrProdQty = null;
        public string[] arrProdTotalAmt = null;

        public AddGoodsReceiveNote()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["R_Initial"] != null && Session["R_Initial"] != "")
                RestInitial.Value = Session["R_Initial"].ToString();


            if (!Page.IsPostBack)
            {
                if (Session["R_ID"] == "" || Session["R_ID"] == null)
                {
                    Session["bckurl"] = "AddGoodsReceiveNote.aspx";
                    Server.Transfer("Select_Restaurants.aspx");
                    return;
                }

                if (Request.QueryString["grnid"] != null)
                {
                    if (Request.QueryString["mode"] != null)
                    {
                        if (Request.QueryString["mode"] == "edit")
                            Mode.Value = "edit";
                        else if (Request.QueryString["mode"] == "clone")
                            Mode.Value = "clone";
                    }

                    grn_id = iTool.decryptString(Request.QueryString["grnid"]);

                    SqlParameter[] ArParams = new SqlParameter[19];

                    if (grn_id != "")
                    {
                        ArParams[0] = new SqlParameter("@GRNNo", SqlDbType.Int);
                        ArParams[0].Value = Convert.ToInt32(grn_id);

                        ArParams[1] = new SqlParameter("@Tran_Code", SqlDbType.VarChar, 25);
                        ArParams[1].Direction = ParameterDirection.Output;

                        ArParams[2] = new SqlParameter("@Tran_Date", SqlDbType.DateTime);
                        ArParams[2].Direction = ParameterDirection.Output;

                        ArParams[3] = new SqlParameter("@POID", SqlDbType.Int);
                        ArParams[3].Direction = ParameterDirection.Output;

                        ArParams[4] = new SqlParameter("@PODate", SqlDbType.DateTime);
                        ArParams[4].Direction = ParameterDirection.Output;

                        ArParams[5] = new SqlParameter("@SupplierID", SqlDbType.Int);
                        ArParams[5].Direction = ParameterDirection.Output;

                        ArParams[6] = new SqlParameter("@SupplierName", SqlDbType.VarChar,50);
                        ArParams[6].Direction = ParameterDirection.Output;

                        ArParams[7] = new SqlParameter("@DeliverToName", SqlDbType.VarChar, 50);
                        ArParams[7].Direction = ParameterDirection.Output;

                        ArParams[8] = new SqlParameter("@DeliverAddress1", SqlDbType.VarChar, 150);
                        ArParams[8].Direction = ParameterDirection.Output;

                        ArParams[9] = new SqlParameter("@DeliverAddress2", SqlDbType.VarChar, 150);
                        ArParams[9].Direction = ParameterDirection.Output;

                        ArParams[10] = new SqlParameter("@TotalAmt", SqlDbType.Decimal, 16);
                        ArParams[10].Direction = ParameterDirection.Output;
                        ArParams[10].Scale = 2;

                        ArParams[11] = new SqlParameter("@TaxAmt", SqlDbType.Decimal, 8);
                        ArParams[11].Direction = ParameterDirection.Output;
                        ArParams[11].Scale = 2;

                        ArParams[12] = new SqlParameter("@Discount", SqlDbType.Decimal, 8);
                        ArParams[12].Direction = ParameterDirection.Output;
                        ArParams[12].Scale = 2;

                        ArParams[13] = new SqlParameter("@PaidAmt", SqlDbType.Decimal, 16);
                        ArParams[13].Direction = ParameterDirection.Output;
                        ArParams[13].Scale = 2;

                        ArParams[14] = new SqlParameter("@DueAmt", SqlDbType.Decimal, 16);
                        ArParams[14].Direction = ParameterDirection.Output;
                        ArParams[14].Scale = 2;

                        ArParams[15] = new SqlParameter("@Memo", SqlDbType.VarChar, 3000);
                        ArParams[15].Direction = ParameterDirection.Output;

                        ArParams[16] = new SqlParameter("@RestID", SqlDbType.Int);
                        ArParams[16].Value= Convert.ToInt32(Session["R_ID"]);

                        ArParams[17] = new SqlParameter("@PONO", SqlDbType.VarChar,25);
                        ArParams[17].Direction = ParameterDirection.Output;

                        ArParams[18] = new SqlParameter("@IsFullPaid", SqlDbType.Bit);
                        ArParams[18].Direction = ParameterDirection.Output;

                        try
                        {
                            SqlHelper.ExecuteNonQuery(mConnection.GetConnection(), CommandType.StoredProcedure, "getGoodsReceivedNoteInfo", ArParams);

                            //display name on top
                            string itemType = "Goods Receive Note";
                            LblHead.Text = char.ToUpper(Mode.Value[0]) + Mode.Value.Substring(1) + " " + itemType + " [ " + ArParams[1].Value.ToString() + " ]";
                            //        LblHead.Text = char.ToUpper(Mode.Value[0]) + Mode.Value.Substring(1) + " " + itemType;
                            //        LBLPrdct.Text = "Product Info [ " + ArParams[1].Value.ToString() + " ]";
                            //        LblProductNameHeader.Text = "<h4 class=\"content-box1\">Product Name :: [ " + ArParams[1].Value.ToString() + "  ]</h4>";

                            // Display results in text box using the values of output parameters	

                            GRNCode.Value = ArParams[1].Value.ToString();
                            strGRNDt = String.Format("{0:MM/dd/yyyy}", ArParams[2].Value);

                            if (ArParams[3].Value.ToString() != "")
                            {
                                PONo.SelectedValue = ArParams[3].Value.ToString();
                                purchid = ArParams[3].Value.ToString();
                            }

                            strPurchOrdDt = String.Format("{0:MM/dd/yyyy}", ArParams[4].Value);

                            SupplierID.Value = ArParams[5].Value.ToString();
                            txtSupplier.Value = ArParams[6].Value.ToString();
                            strDeliverToName = ArParams[7].Value.ToString();
                            DeliveryAddress1.Value = ArParams[8].Value.ToString();
                            DeliveryAddress2.Value = ArParams[9].Value.ToString();
                            strTotalAmt = String.Format("{0:n2}", ArParams[10].Value);
                            strTaxAmt = String.Format("{0:n2}", ArParams[11].Value);
                            strSubTotalAmt = String.Format("{0:n2}",(Convert.ToDecimal(strTotalAmt) - Convert.ToDecimal(strTaxAmt)));
                            strDiscountAmt = String.Format("{0:n2}", ArParams[12].Value);
                            strPaidAmt = String.Format("{0:n2}", ArParams[13].Value);
                            strDueAmt = String.Format("{0:n2}", ArParams[14].Value);
                            txtNote.InnerText = ArParams[15].Value.ToString();


                            if (Mode.Value == "add" || Mode.Value == "clone")
                            {
                                GRNNo.Value = "-1";
                                PONo.Enabled = true;
                            }
                            else
                            {
                                GRNNo.Value = grn_id;
                                PONo.Enabled = false;
                            }

                            dict = null;
                            ds = null;
                            ds = Fn.LoadPurchaseOrderDetails(dict, "POID", purchid, mConnection.GetConnection());
                            
                        }
                        catch (Exception ex)
                        {
                            // throw an exception
                            throw ex;
                        }
                    }

                }

                if (Session["R_ID"].ToString() != "")
                {
                    dict = null;
                    dict = new Dictionary<string, string>() { { "IsActive", "1" }, { "Rest_ID", Session["R_ID"].ToString() } };

                    list.Add("POID");
                    list.Add("PONo");
                    Fn.PopulateDropDown_ListWithCustomTable(PONo, Qry.GetTableColListSQL(dict, "omni_PurchaseMaster", list, 2), "PONo", "POID", "");
                }

                Session["prod_id"] = "";
                Session["prod_name"] = "";
                Session["prod_unit"] = "";
                Session["prod_type"] = "";
                Session["purc_unitcost"] = "";
                Session["purc_qty"] = "";
                Session["prod_totalamt"] = "";
            }
            else
            {
                purchid = PONo.SelectedValue;

                DateTime sDate = DateTime.Now;
                strGRNDt = String.Format("{0:MM/dd/yyyy}", Fn.GetCommonDate(sDate, Session["DateFormat"]));

                if (purchid != "")
                {
                    SqlParameter[] ArParams = new SqlParameter[12];

                    ArParams[0] = new SqlParameter("@POID", SqlDbType.Int);
                    ArParams[0].Value = Convert.ToInt32(purchid);

                    ArParams[1] = new SqlParameter("@PONo", SqlDbType.VarChar, 50);
                    ArParams[1].Direction = ParameterDirection.Output;

                    ArParams[2] = new SqlParameter("@PODate", SqlDbType.DateTime);
                    ArParams[2].Direction = ParameterDirection.Output;

                    ArParams[3] = new SqlParameter("@SupplierID", SqlDbType.Int);
                    ArParams[3].Direction = ParameterDirection.Output;

                    ArParams[4] = new SqlParameter("@SupplierName", SqlDbType.VarChar, 50);
                    ArParams[4].Direction = ParameterDirection.Output;

                    ArParams[5] = new SqlParameter("@DeliverToName", SqlDbType.VarChar, 150);
                    ArParams[5].Direction = ParameterDirection.Output;

                    ArParams[6] = new SqlParameter("@DeliverAddress1", SqlDbType.VarChar, 200);
                    ArParams[6].Direction = ParameterDirection.Output;

                    ArParams[7] = new SqlParameter("@DeliverAddress2", SqlDbType.VarChar, 200);
                    ArParams[7].Direction = ParameterDirection.Output;

                    ArParams[8] = new SqlParameter("@OrderNote", SqlDbType.VarChar, 5000);
                    ArParams[8].Direction = ParameterDirection.Output;

                    ArParams[9] = new SqlParameter("@TotalAmt", SqlDbType.Decimal, 14);
                    ArParams[9].Scale = 2;
                    ArParams[9].Direction = ParameterDirection.Output;

                    ArParams[10] = new SqlParameter("@TaxAmt", SqlDbType.Decimal, 8);
                    ArParams[10].Direction = ParameterDirection.Output;
                    ArParams[10].Scale = 2;

                    ArParams[11] = new SqlParameter("@RestID", SqlDbType.Int);
                    ArParams[11].Value = Convert.ToInt32(Session["R_ID"]);

                    try
                    {
                        SqlHelper.ExecuteNonQuery(mConnection.GetConnection(), CommandType.StoredProcedure, "getPurchaseMasterInfo", ArParams);

                        strPurchOrdDt = String.Format("{0:MM/dd/yyyy}", ArParams[2].Value);
                        txtSupplier.Value = ArParams[4].Value.ToString();
                        strDeliverToName = ArParams[5].Value.ToString();
                        DeliveryAddress1.Value = ArParams[6].Value.ToString();
                        DeliveryAddress2.Value = ArParams[7].Value.ToString();

                        strTotalAmt = String.Format("{0:n2}", ArParams[9].Value);
                        strTaxAmt = String.Format("{0:n2}", ArParams[10].Value);
                        strSubTotalAmt = String.Format("{0:n2}", (Convert.ToDecimal(strTotalAmt) - Convert.ToDecimal(strTaxAmt)));
                        Mode.Value = "Show";
                        SupplierID.Value = ArParams[3].Value.ToString();

                        dict = null;
                        ds = null;
                        dict = new Dictionary<string, string>() { { "Rest_ID", Session["R_ID"].ToString() } };
                        ds = Fn.LoadPurchaseOrderDetails(dict, "POID", purchid, mConnection.GetConnection());
                    }
                    catch (Exception ex)
                    {

                    }
                }
                else
                {
                    strPurchOrdDt = "";
                    txtSupplier.Value = "";
                    DeliveryAddress1.Value = "";
                    DeliveryAddress2.Value = "";
                }
            }

        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {

                bool flag1 = false;
                bool flag2 = true;
                int purchase_id = 0;

                strPOrderNo = PONo.SelectedItem.ToString();
                strGRNNo = iTool.formatInputString(GRNCode.Value);
                strGRNDt = iTool.formatInputString(Request.Form["GRNDt"]);
                strDiscountAmt = iTool.formatInputString(Request.Form["txtDisc"]);
                strPaidAmt = iTool.formatInputString(Request.Form["txtPaidAmt"]);
                strTaxAmt = iTool.formatInputString(Request.Form["txtTaxAmt"]);
                strSubTotalAmt = iTool.formatInputString(Request.Form["subtotal"]);
                strTotalAmt = iTool.formatInputString(Request.Form["totalamt"]);
                strDueAmt = iTool.formatInputString(Request.Form["txtDueAmt"]);
                strNote = iTool.formatInputString(txtNote.Value);

                if (Request.Form["GRNDt"].Trim() == "")
                {
                    EmptyError.Visible = true;
                    return;
                }
                else
                    EmptyError.Visible = false;

                Mode.Value = (Request.QueryString["mode"] == "edit") ? "edit" : "add";
                string[] prod_id = Request.Form.GetValues("purc_id");
                string[] prod_name = Request.Form.GetValues("purc_name");
                string[] prod_type = Request.Form.GetValues("prod_type");
                string[] purc_unitcost = Request.Form.GetValues("purc_unitcost");
                string[] purc_qty = Request.Form.GetValues("purc_qty");

                Session["prod_id"] = Request.Form.GetValues("purc_id");
                Session["prod_name"] = Request.Form.GetValues("purc_name");
                Session["prod_unit"] = Request.Form.GetValues("purc_unit");
                Session["prod_type"] = Request.Form.GetValues("prod_type");
                Session["purc_unitcost"] = Request.Form.GetValues("purc_unitcost");
                Session["purc_qty"] = Request.Form.GetValues("purc_qty");
                Session["prod_totalamt"] = Request.Form.GetValues("purc_amount");

                DateTime sDate = DateTime.Now;
                sDate = Fn.GetCommonDate(sDate, Session["DateFormat"]);

                SqlParameter[] ArParams = new SqlParameter[19];
                ArParams[0] = new SqlParameter("@GRNNo", SqlDbType.Int);
                ArParams[0].Value = GRNNo.Value;

                ArParams[1] = new SqlParameter("@Tran_Code", SqlDbType.VarChar, 20);
                ArParams[1].Value = strGRNNo;

                ArParams[2] = new SqlParameter("@Acc_Year", SqlDbType.Int);
                ArParams[2].Value = Convert.ToDateTime(strGRNDt).Year;

                ArParams[3] = new SqlParameter("@Acc_Month", SqlDbType.Int);
                ArParams[3].Value = Convert.ToDateTime(strGRNDt).Month;

                ArParams[4] = new SqlParameter("@Tran_Date", SqlDbType.DateTime);
                ArParams[4].Value =  strGRNDt;

                ArParams[5] = new SqlParameter("@POID", SqlDbType.Int);
                ArParams[5].Value = PONo.SelectedValue; 

                ArParams[6] = new SqlParameter("@PONo", SqlDbType.VarChar,25);
                ArParams[6].Value = strPOrderNo; 

                ArParams[7] = new SqlParameter("@Memo", SqlDbType.VarChar,3000);
                ArParams[7].Value = strNote;

                ArParams[8] = new SqlParameter("@Discount", SqlDbType.Decimal);
                ArParams[8].Value = Convert.ToDecimal(strDiscountAmt);

                ArParams[9] = new SqlParameter("@TotalAmount", SqlDbType.Decimal);
                ArParams[9].Value = Convert.ToDecimal(strTotalAmt);

                ArParams[10] = new SqlParameter("@TaxAmount", SqlDbType.Decimal);
                ArParams[10].Value = Convert.ToDecimal(strTaxAmt);

                ArParams[11] = new SqlParameter("@PaidAmount", SqlDbType.Decimal);
                ArParams[11].Value = Convert.ToDecimal(strPaidAmt);

                ArParams[12] = new SqlParameter("@DueAmount", SqlDbType.Decimal);
                ArParams[12].Value = Convert.ToDecimal(strDueAmt); 

                ArParams[13] = new SqlParameter("@Status", SqlDbType.Int);
                ArParams[13].Value = 1;

                ArParams[14] = new SqlParameter("@sDate", SqlDbType.DateTime);
                ArParams[14].Value = sDate;

                ArParams[15] = new SqlParameter("@LoggedUserID", SqlDbType.Int);
                ArParams[15].Value = Convert.ToInt32(Session["UserID"]);

                ArParams[16] = new SqlParameter("@RestID", SqlDbType.Int);
                ArParams[16].Value = Session["R_ID"];

                ArParams[17] = new SqlParameter("@SupplierID", SqlDbType.Int);
                ArParams[17].Value = SupplierID.Value;

                ArParams[18] = new SqlParameter("@Mode", SqlDbType.VarChar, 20);
                ArParams[18].Value = Mode.Value;

                dict = null;
                dict = new Dictionary<string, string>() { { "Rest_ID", Session["R_ID"].ToString() } };

                if (GRNNo.Value == "-1" && (Mode.Value == "add" || Mode.Value == "clone"))
                    flag1 = Fn.CheckRecordExists(dict, "omni_Item_ReceivedNotes", "Tran_Code", "Tran_Code", "", "", "", ArParams);
                else
                    flag1 = Fn.CheckRecordExists(dict, "omni_Item_ReceivedNotes", "Tran_Code", "Tran_Code", "edit", "GRNNo", GRNNo.Value, ArParams);

                if (flag1 == true)
                {
                    ErrorMsg.InnerText = "GRN already exist. Try with another one.";
                    Msg.Visible = true;
                    return;
                }

                if (prod_id != null && prod_id.Length > 0)
                {
                    for (int i = 0; i < prod_id.Length; i++)
                    {
                        if (prod_id[i].Trim() == "" && prod_name[i].Trim() == "")
                        {
                            flag2 = false;
                            errorinpos = i;
                            break;
                        }
                        else
                        {
                            dict = null;
                            dict = new Dictionary<string, string>() { { "IsActive", "1" }, { "Rest_ID", Session["R_ID"].ToString() }, { "SupplierID", SupplierID.Value } };

                            if (prod_type[i].Trim() == "Ing")
                            {
                                //dict = null;
                                //dict = new Dictionary<string, string>() { { "IsActive", "1" }, { "Rest_ID", Session["R_ID"].ToString() }, { "SupplierID", Supplier.SelectedValue } };
                                flag2 = Fn.CheckRecordExists(dict, "omni_Items_Ingredients", "IngredientName", iTool.formatInputString(prod_name[i].ToString()));
                            }
                            else if (prod_type[i].Trim() == "Prod")
                            {
                                flag2 = Fn.CheckRecordExists(dict, "omni_Products", "ProductName", iTool.formatInputString(prod_name[i].ToString()));
                            }
                            else
                                flag2 = false;

                            if (flag2 == false)
                            {
                                errorinpos = i;
                                break;
                            }
                        }
                    }
                }

                if (flag2 == false)
                {
                    ErrorMsg.InnerText = "!! Error In Submission. Either No Product(s) / Ingredient(s) are choosen or Wrong Product / Ingrdient entered for the choosen supplier.";
                    Msg.Visible = true;
                    return;
                }
                else
                    Msg.Visible = false;

                   using (SqlConnection conn = mConnection.GetConnection())
                   {
                    conn.Open();

                    using (SqlTransaction trans = conn.BeginTransaction())
                    {
                        try
                        {
                            SqlHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, "SP_omni_ItemReceivedNotes_Update", ArParams);

                            if (Mode.Value == "add" || Mode.Value == "clone")
                                grn_id = SqlHelper.ExecuteScalar(trans, CommandType.Text, Qry.getLastInsertedID(null, "omni_Item_ReceivedNotes", "GRNNo", 1, "", "")).ToString();
                            else
                                grn_id = GRNNo.Value;

                            if (Mode.Value == "edit")
                            {
                                dict = new Dictionary<string, string>() { { "Rest_ID", Session["R_ID"].ToString()  } };
                                SqlHelper.ExecuteNonQuery(trans, CommandType.Text, Qry.DeleteRec(dict, "omni_Item_ReceivedNotesDetail", 1, "GRNNo", grn_id.ToString()));

                                dict = null;
                                dict = new Dictionary<string, string>() { { "Rest_ID", Session["R_ID"].ToString()  }, { "Tran_No", grn_id.ToString() } };
                                SqlHelper.ExecuteNonQuery(trans, CommandType.Text, Qry.DeleteRec(dict, "omni_Tran_ItemHistory", 1, "Tran_Code", strGRNNo.ToString()));
                            }

							SqlParameter[] ArParamsD = new SqlParameter[7];

							for (int i = 0; i<prod_id.Length;i++)
							{
                                if (prod_id[i].Trim() != "" && prod_name[i].Trim() != "" && purc_qty[i].Trim() !="" &&  purc_unitcost[i].Trim()!="")
                                {
                                    ArParamsD[0] = new SqlParameter("@GRNNO", SqlDbType.Int);
                                    ArParamsD[0].Value = grn_id;

                                    ArParamsD[1] = new SqlParameter("@ProductID", SqlDbType.Int);
                                    ArParamsD[1].Value = (prod_id[i] != "" || prod_id[i] != null) ? iTool.formatInputString(prod_id[i]) : "";

                                    ArParamsD[2] = new SqlParameter("@ProductType", SqlDbType.Char, 10);
                                    ArParamsD[2].Value = (prod_type[i] != "" || prod_type[i] != null) ? iTool.formatInputString(prod_type[i]) : "";

                                    ArParamsD[3] = new SqlParameter("@Qty", SqlDbType.Decimal);
                                    ArParamsD[3].Value = (purc_qty[i] != "" || purc_qty[i] != null) ? Convert.ToDecimal(iTool.formatInputString(purc_qty[i])) : 0;

                                    ArParamsD[4] = new SqlParameter("@UnitPrice", SqlDbType.Decimal);
                                    ArParamsD[4].Value = (purc_unitcost[i] != "" || purc_unitcost[i] != null) ? Convert.ToDecimal(iTool.formatInputString(purc_unitcost[i])) : 0;

                                    ArParamsD[5] = new SqlParameter("@RestID", SqlDbType.Int);
                                    ArParamsD[5].Value = Session["R_ID"];

                                    ArParamsD[6] = new SqlParameter("@Mode", SqlDbType.VarChar, 20);
                                    ArParamsD[6].Value = Mode.Value;

                                    SqlHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, "SP_omni_Item_ReceivedNotesDetail_Update", ArParamsD);
                                }
							}

                            trans.Commit();
                            //txtResults.Text = "Transfer Completed";
                        }
                        catch (Exception ex)
                        {
                           //  throw exception						
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
            catch(Exception ex)
            {

           //// CreateLogFiles.ErrorLog(ex.Message.ToString() & " - " & System.IO.Path.GetFileName(System.Web.HttpContext.Current.Request.Url.AbsolutePath) & " - btnCreate_Click")
           //// lblError.Text = "Error - Please contact Administrator "
           //// Exit Sub
            }
                Response.Redirect("GoodsReceivedNotes.aspx");
        }

        public string getAvailableProducts(string suppid)
        {
            dict = null;
            dict = new Dictionary<string, string>() { { "IsActive", "1" }, { "SupplierID", suppid.ToString() } };
            //strProduct_Ingredients = Fn.GetProductIngredientCombinedInJsonStringFromDB(dict, "Rest_ID", Session["R_ID"].ToString());
            return strProduct_Ingredients;
        }
}
}