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

namespace PosOrder
{
    public partial class CreatePurchaseOrder : System.Web.UI.Page
    {
        
        DB mConnection = new DB() ;
        
        public DataSet ds = new DataSet();
        Common Fn = new Common();
        MyToolSet iTool = new MyToolSet();
        SQLQuery Qry = new SQLQuery();
        //string sQuery = "";

        public string strSuppID = string.Empty;
        string strPurchOrdNo = string.Empty;
        public string strPurchOrdDt = string.Empty;
        string strDeliverToName = string.Empty;
        string strDeliverAddress1 = string.Empty;
        string strDeliverAddress2 = string.Empty;
        public string strTaxAmt = "0.00";
        public string strSubTotalAmt = "0.00";
        public string strTotalAmt = "0.00";
        public string strProduct_Ingredients = string.Empty;
        public int errorinpos = -1;
        string strNote = string.Empty;
        Dictionary<string, string> dict = null;

        public string[] arrProdName = null;
        public string[] arrProdID = null;
        public string[] arrProdUnit = null;
        public string[] arrProdType = null;
        public string[] arrProdCost = null;
        public string[] arrProdQty = null;
        public string[] arrProdTotalAmt = null;

        public DataSet dsReqdPurch = new DataSet();
        public string strItemID = string.Empty;
        public string strItemName = string.Empty;
        public string strItemType = string.Empty;
        public string strUnitName = string.Empty;
        public string strUnitPrice = string.Empty;
        public string strReOrderLevel = string.Empty;
        public string strBalQty = string.Empty;
        public string strHoldQty = string.Empty;
        public string strReqdQtyAmt = string.Empty;
        

        public CreatePurchaseOrder()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            string purchase_id = "";

            if (Session["R_Initial"] != null && Session["R_Initial"] != "")
                RestInitial.Value = Session["R_Initial"].ToString();
            else
            //if (Session["R_ID"] == "" || Session["R_ID"] == null)
            {
                Session["bckurl"] = "CreatePurchaseOrder.aspx";
                Server.Transfer("Select_Restaurants.aspx");
                return;
            }


            if (!Page.IsPostBack)
            {

                if (Request.QueryString["prchid"] != null)
                {
                    if (Request.QueryString["modes"] != null)
                    {
                        if (Request.QueryString["mode"] == "edit")
                            Mode.Value = "edit";
                        else if (Request.QueryString["mode"] == "clone")
                            Mode.Value = "clone";
                    }

                    SqlParameter[] ArParams = new SqlParameter[12];

                    purchase_id = iTool.decryptString(Request.QueryString["prchid"]);

                    ArParams[0] = new SqlParameter("@POID", SqlDbType.Int);
                    ArParams[0].Value = purchase_id;

                    ArParams[1] = new SqlParameter("@PONo", SqlDbType.VarChar, 50);
                    ArParams[1].Direction = ParameterDirection.Output;

                    ArParams[2] = new SqlParameter("@PODate", SqlDbType.DateTime);
                    ArParams[2].Direction = ParameterDirection.Output;

                    ArParams[3] = new SqlParameter("@SupplierID", SqlDbType.Int);
                    ArParams[3].Direction = ParameterDirection.Output;

                    ArParams[4] = new SqlParameter("@DeliverToName", SqlDbType.VarChar, 150);
                    ArParams[4].Direction = ParameterDirection.Output;

                    ArParams[5] = new SqlParameter("@DeliverAddress1", SqlDbType.VarChar, 200);
                    ArParams[5].Direction = ParameterDirection.Output;

                    ArParams[6] = new SqlParameter("@DeliverAddress2", SqlDbType.VarChar, 200);
                    ArParams[6].Direction = ParameterDirection.Output;

                    ArParams[7] = new SqlParameter("@TotalAmt", SqlDbType.Decimal, 14);
                    ArParams[7].Scale = 2;
                    ArParams[7].Direction = ParameterDirection.Output;

                    ArParams[8] = new SqlParameter("@TaxAmt", SqlDbType.Decimal, 8);
                    ArParams[8].Direction = ParameterDirection.Output;
                    ArParams[8].Scale = 2;

                    ArParams[9] = new SqlParameter("@OrderNote", SqlDbType.VarChar, 5000);
                    ArParams[9].Direction = ParameterDirection.Output;

                    ArParams[10] = new SqlParameter("@RestID", SqlDbType.Int);
                    ArParams[10].Value = Convert.ToInt32(Session["R_ID"]);

                    ArParams[11] = new SqlParameter("@SupplierName", SqlDbType.VarChar,50);
                    ArParams[11].Value = ParameterDirection.Output;

                    try
                    {
                        // Call ExecuteNonQuery static method of SqlHelper class
                        // We pass in database connection string, command type, stored procedure name and an array of SqlParameter objects
                        SqlHelper.ExecuteNonQuery(mConnection.GetConnection(), CommandType.StoredProcedure, "getPurchaseMasterInfo", ArParams);

                        //        //display name on top
                                string itemType = "Purchase Order";
                        LblHead.Text = char.ToUpper(Mode.Value[0]) + Mode.Value.Substring(1) + " " + itemType + " [ " + ArParams[1].Value.ToString() + " ]";
                        //        LblHead.Text = char.ToUpper(Mode.Value[0]) + Mode.Value.Substring(1) + " " + itemType;
                        //        LBLPrdct.Text = "Product Info [ " + ArParams[1].Value.ToString() + " ]";
                        //        LblProductNameHeader.Text = "<h4 class=\"content-box1\">Product Name :: [ " + ArParams[1].Value.ToString() + "  ]</h4>";

                        // Display results in text box using the values of output parameters	

                        PurchOrderNo.Value = ArParams[1].Value.ToString();
                        strPurchOrdDt = String.Format("{0:MM/dd/yyyy}", ArParams[2].Value);
                        strDeliverToName = ArParams[4].Value.ToString();
                        DeliveryAddress1.Value = ArParams[5].Value.ToString();
                        DeliveryAddress2.Value = ArParams[6].Value.ToString();

                        strSubTotalAmt = String.Format("{0:n2}", (Convert.ToDecimal(ArParams[7].Value) - Convert.ToDecimal(ArParams[8].Value)));
                        strTotalAmt = String.Format("{0:n2}", ArParams[7].Value);
                        strTaxAmt = String.Format("{0:n2}", ArParams[8].Value);
                        txtNote.Value = ArParams[9].Value.ToString();

                        if (ArParams[3].Value.ToString() != "-1")
                            Supplier.SelectedValue = ArParams[3].Value.ToString();

                        //        ChkChangePrice.Checked = ArParams[19].Value.ToString() == "True" ? true : false;

                        if (Mode.Value == "add" || Mode.Value == "clone")
                            POID.Value = "-1";
                        else
                            POID.Value = purchase_id;

                        dict = null;
                        ds = null;
                        ds = Fn.LoadPurchaseOrderDetails(dict, "POID", purchase_id, mConnection.GetConnection());

                        dict = new Dictionary<string, string>() { { "IsActive", "1" }, { "SupplierID", ArParams[3].Value.ToString() } };
                        strProduct_Ingredients = Fn.GetProductIngredientCombinedInJsonStringFromDB(dict, "Rest_ID", Session["R_ID"].ToString());

                        if (Mode.Value == "edit")
                        {
                            dict = new Dictionary<string, string>() { { "Rest_ID", Session["R_ID"].ToString() } };

                            if (Fn.CheckRecordExists(dict, "omni_Item_ReceivedNotes", "POID", purchase_id))
                            {
                                ErrorMsg.InnerText = "Since already GRN is generated against this Purchase Order so you could not update the order details.";
                                Msg.Visible = true;
                                BtnSave.Enabled = false;
                                Supplier.Enabled = false;
                            }
                            else
                            {
                                ErrorMsg.InnerText = "";
                                Msg.Visible = false;
                                BtnSave.Enabled = true;
                                Supplier.Enabled = true;
                            }                                
                        }
                    }
                    catch (Exception ex)
                    {
                        // throw an exception
                        throw ex;
                    }
                }

                if (Session["R_ID"].ToString() != "")
                {
                    dict = null;
                    Fn.PopulateDropDown_List(Supplier, Qry.GetSupplierSQL(dict), "SupplierName", "SupplierID", "");

                    dict = null;
                    dict = new Dictionary<string, string>() { { "IsActive", "1" } };
                   // strProduct_Ingredients = Fn.GetProductIngredientCombinedInJsonStringFromDB(dict, "Rest_ID", Session["R_ID"].ToString());
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
                dict = null;
                dict = new Dictionary<string, string>() { { "IsActive", "1" }, {"SupplierID", Supplier.SelectedValue} };
                strProduct_Ingredients = Fn.GetProductIngredientCombinedInJsonStringFromDB(dict, "Rest_ID", Session["R_ID"].ToString());
                strSuppID = Supplier.SelectedValue;

                if (strSuppID != "")
                {
                    //strSuppID = iTool.encryptString(strSuppID);
                    dict = null;
                    dict = new Dictionary<string, string>() { { "SupplierID", strSuppID } };
                    dsReqdPurch = Fn.LoadProdWithIngToSBePurchaseFromSupplier(dict, "Rest_ID", Session["R_ID"].ToString(), "<", "(((IsNull(OpQty,0.00)+IsNull(b.Qty,0.00))- IsNull(c.Qty,0.00)))", "Items.ReOrderLevel");
                    popup.Visible = true;
                }
            }

                DateTime sDate = DateTime.Now;
                strPurchOrdDt = String.Format("{0:MM/dd/yyyy}",Fn.GetCommonDate(sDate, Session["DateFormat"]));

                dict = null;
                dict = new Dictionary<string, string>() { { "Rest_ID", Session["R_ID"].ToString() } };

             
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {

                bool flag1 = false;
                bool flag2 = true;
                int purchase_id = 0;

                strPurchOrdNo = iTool.formatInputString(PurchOrderNo.Value);
                strPurchOrdDt = iTool.formatInputString(Request.Form["PurchOrderDt"]);
                strDeliverAddress1 = DeliveryAddress1.Value;
                strDeliverAddress2 = DeliveryAddress2.Value;
                strTaxAmt = iTool.formatInputString(Request.Form["txtTaxAmt"]);
                strSubTotalAmt = iTool.formatInputString(Request.Form["subtotal"]);
                strTotalAmt = iTool.formatInputString(Request.Form["totalamt"]);
                strNote = iTool.formatInputString(txtNote.Value);

                if (Request.Form["PurchOrderDt"].Trim()=="")
                {
                    EmptyError.Visible = true;
                    return;
                }
                else
                    EmptyError.Visible = false;

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

                    SqlParameter[] ArParams = new SqlParameter[15];
                    ArParams[0] = new SqlParameter("@POID", SqlDbType.Int);
                    ArParams[0].Value = POID.Value;

                    ArParams[1] = new SqlParameter("@PONo", SqlDbType.VarChar, 50);
                    ArParams[1].Value = strPurchOrdNo;

                    ArParams[2] = new SqlParameter("@PODate", SqlDbType.DateTime);
                    ArParams[2].Value = strPurchOrdDt;

                    ArParams[3] = new SqlParameter("@SupplierID", SqlDbType.Int);
                    ArParams[3].Value = Supplier.SelectedValue;

                    ArParams[4] = new SqlParameter("@TotalAmt", SqlDbType.Decimal);
                    ArParams[4].Value =  (strTotalAmt=="") ? 0 : Convert.ToDecimal(strTotalAmt);

                    ArParams[5] = new SqlParameter("@TaxAmt", SqlDbType.Decimal);
                    ArParams[5].Value =  (strTaxAmt=="") ? 0 : Convert.ToDecimal(strTaxAmt); 

                    ArParams[6] = new SqlParameter("@DeliverToName", SqlDbType.VarChar,150);
                    ArParams[6].Value = "";

                    ArParams[7] = new SqlParameter("@DeliverAddress1", SqlDbType.VarChar,200);
                    ArParams[7].Value = strDeliverAddress1;

                    ArParams[8] = new SqlParameter("@DeliverAddress2", SqlDbType.VarChar,200);
                    ArParams[8].Value = strDeliverAddress2;

                    ArParams[9] = new SqlParameter("@Status", SqlDbType.Int);
                    ArParams[9].Value = 1;

                    ArParams[10] = new SqlParameter("@sDate", SqlDbType.DateTime);
                    ArParams[10].Value = sDate;

                    ArParams[11] = new SqlParameter("@LoggedUserID", SqlDbType.Int);
                    ArParams[11].Value = Convert.ToInt32(Session["UserID"]);

                    ArParams[12] = new SqlParameter("@RestID", SqlDbType.Int);
                    ArParams[12].Value = Session["R_ID"];

                    ArParams[13] = new SqlParameter("@OrderNote", SqlDbType.VarChar,5000);
                    ArParams[13].Value = strNote;

                    ArParams[14] = new SqlParameter("@Mode", SqlDbType.VarChar, 20);
                    ArParams[14].Value = Mode.Value;

                    dict = null;
                    dict = new Dictionary<string, string>() { { "Rest_ID", Session["R_ID"].ToString() } };

                    if (POID.Value == "-1" && (Mode.Value == "add" || Mode.Value == "clone"))
                        flag1 = Fn.CheckRecordExists(dict, "omni_PurchaseMaster", "PONo", "PONo", "", "", "", ArParams);
                    else
                        flag1 = Fn.CheckRecordExists(dict, "omni_PurchaseMaster", "PONo", "PONo", "edit", "POID", POID.Value, ArParams);

                    if (flag1 == true)
                    {
                        ErrorMsg.InnerText = "Purchase OrderNo already exist. Try with another one.";
                        //LblPOrnerSNO.Text = "Purchase OrderNo already exist. Try with another one.";
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
                                dict = new Dictionary<string, string>() { { "IsActive", "1" }, { "Rest_ID", Session["R_ID"].ToString() }, { "SupplierID", Supplier.SelectedValue } };

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
                        //LblDetail.Text = "No Product(s) / Ingredient(s) are choosen. Choose atleast any single Product/Ingredient.";
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
                            SqlHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, "SP_omni_PurchaseMaster_Update", ArParams);

                            if (Mode.Value == "add" || Mode.Value == "clone")
                                purchase_id = (int)SqlHelper.ExecuteScalar(trans, CommandType.Text, Qry.getLastInsertedID(null, "omni_PurchaseMaster", "POID", 1, "", ""));
                            else
                                purchase_id = Convert.ToInt32(POID.Value);

                            SqlParameter[] ArParamsT = new SqlParameter[4];

                            if (Mode.Value == "edit")
                            {
                                dict = new Dictionary<string, string>() { { "Rest_ID", Session["R_ID"].ToString()  } };
                                SqlHelper.ExecuteNonQuery(trans, CommandType.Text, Qry.DeleteRec(dict, "omni_PurchaseDetail", 1, "POID", purchase_id.ToString()));
                            }

							SqlParameter[] ArParamsD = new SqlParameter[7];

							for (int i = 0; i<prod_id.Length;i++)
							{
                                if (prod_id[i].Trim() != "" && prod_name[i].Trim() != "" && purc_qty[i].Trim() !="" &&  purc_unitcost[i].Trim()!="")
                                {
                                    ArParamsD[0] = new SqlParameter("@POID", SqlDbType.Int);
                                    ArParamsD[0].Value = purchase_id;

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

                                    SqlHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, "SP_omni_PurchaseDetail_Update", ArParamsD);
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
                Response.Redirect("PurchaseOrders.aspx");
        }

        public string getAvailableProducts(string suppid)
        {
            dict = null;
            dict = new Dictionary<string, string>() { { "IsActive", "1" }, { "SupplierID", suppid.ToString() } };
            strProduct_Ingredients = Fn.GetProductIngredientCombinedInJsonStringFromDB(dict, "Rest_ID", Session["R_ID"].ToString());
            return strProduct_Ingredients;
        }
}
}