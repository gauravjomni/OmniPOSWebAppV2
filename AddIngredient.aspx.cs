using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.ApplicationBlocks.Data;
using MyDB;
using MyTool;
using MyQuery;
using Commons;

namespace PosIngredients
{
    public partial class AddIngredient : System.Web.UI.Page
    {
        DB mConnection = new DB() ;
        DataSet ds = new DataSet();
        Common Fn = new Common();
        MyToolSet iTool = new MyToolSet();
        SQLQuery Qry = new SQLQuery();

        string sQuery = "";


        public AddIngredient()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                if (Session["R_ID"] == "" || Session["R_ID"] == null)
                {
                    Session["bckurl"] = "Ingredients.aspx";
                    Server.Transfer("Notification.aspx");
                    return;
                }
                else
                {
                    Fn.PopulateDropDown_List(Unit, Qry.getUnitSQL(Session["R_ID"].ToString()), "UnitName", "UnitID", "");

                    Dictionary<string, string> dict = null;
                    Fn.PopulateDropDown_List(Supplier, Qry.GetSupplierSQL(dict), "SupplierName", "SupplierID", "");                    
                }

                if (Request.QueryString["ingid"] != null)
                {
                    string ingid = "";
                    SqlParameter[] ArParams = new SqlParameter[11];

                    ingid = iTool.decryptString(Request.QueryString["ingid"]);

                    ArParams[0] = new SqlParameter("@IngredientID", SqlDbType.Int);
                    ArParams[0].Value = ingid;

                    ArParams[1] = new SqlParameter("@IngredientName", SqlDbType.VarChar, 150);
                    ArParams[1].Direction = ParameterDirection.Output;

                    ArParams[2] = new SqlParameter("@Price", SqlDbType.Decimal, 7);
                    ArParams[2].Direction = ParameterDirection.Output;
                    ArParams[2].Scale = 2;

                    ArParams[3] = new SqlParameter("@UnitID", SqlDbType.Int);
                    ArParams[3].Direction = ParameterDirection.Output;

                    ArParams[4] = new SqlParameter("@BarCode", SqlDbType.VarChar, 50);
                    ArParams[4].Direction = ParameterDirection.Output;

                    // @Status Output Parameter
                    ArParams[5] = new SqlParameter("@Status", SqlDbType.Int);
                    ArParams[5].Direction = ParameterDirection.Output;

                    ArParams[6] = new SqlParameter("@SupplierID", SqlDbType.Int);
                    ArParams[6].Direction = ParameterDirection.Output;

                    ArParams[7] = new SqlParameter("@IsDailyItem", SqlDbType.Bit);
                    ArParams[7].Direction = ParameterDirection.Output;

                    ArParams[8] = new SqlParameter("@OpQty", SqlDbType.Decimal);
                    ArParams[8].Direction = ParameterDirection.Output;
                    ArParams[8].Scale = 2;

                    ArParams[9] = new SqlParameter("@ReOrdQty", SqlDbType.Decimal);
                    ArParams[9].Direction = ParameterDirection.Output;
                    ArParams[9].Scale = 2;

                    ArParams[10] = new SqlParameter("@ReOrdLvl", SqlDbType.Decimal);
                    ArParams[10].Direction = ParameterDirection.Output;
                    ArParams[10].Scale = 2;

                    try
                    {
                        // Call ExecuteNonQuery static method of SqlHelper class
                        // We pass in database connection string, command type, stored procedure name and an array of SqlParameter objects
                        SqlHelper.ExecuteNonQuery(mConnection.GetConnection(), CommandType.StoredProcedure, "getIngredientDetails", ArParams);

                        // Display results in text box using the values of output parameters	
                        txtIngredientName.Value = ArParams[1].Value.ToString();
                        txtPrice.Value = ArParams[2].Value.ToString();
                        Unit.SelectedValue = ArParams[3].Value.ToString();
                        txtBarCode.Value = ArParams[4].Value.ToString();
                        Status.Checked = ArParams[5].Value.ToString() == "1" ? true : false;
                        Supplier.SelectedValue = ArParams[6].Value.ToString();
                        DailyItem.Checked = ArParams[7].Value.ToString() == "True" ? true : false;
                        txtOpQty.Value = ArParams[8].Value.ToString();
                        txtReOrdQty.Value = ArParams[9].Value.ToString();
                        txtReOrdLvl.Value = ArParams[10].Value.ToString();
                        txtOpQty.Value = ArParams[8].Value.ToString();

                        if (Request.QueryString["mode"] != null)
                        {
                            if (Request.QueryString["mode"] == "edit")
                                Mode.Value = "edit";
                            else if (Request.QueryString["mode"] == "clone")
                                Mode.Value = "clone";
                        }

                        //display name on top
                        string itemType = "Ingredient";
                        LblHead.Text = char.ToUpper(Mode.Value[0]) + Mode.Value.Substring(1) + " " + itemType + " [ " + ArParams[1].Value.ToString() + " ]";

                        if (Mode.Value == "add" || Mode.Value == "clone")
                            IngID.Value = "-1";
                        else
                            IngID.Value = ingid;

                    }
                    catch (Exception ex)
                    {
                        // throw an exception
                        throw ex;
                    }
                }

                if (Mode.Value == "add" && !IsPostBack)
                {
                    txtOpQty.Value = System.Configuration.ConfigurationSettings.AppSettings["DefaultPriceValue"];
                    txtReOrdLvl.Value = System.Configuration.ConfigurationSettings.AppSettings["DefaultPriceValue"];
                    txtReOrdQty.Value = System.Configuration.ConfigurationSettings.AppSettings["DefaultPriceValue"];

                    txtBarCode.Value = System.Configuration.ConfigurationSettings.AppSettings["DefaultBarCode"];
                    Supplier.SelectedValue = System.Configuration.ConfigurationSettings.AppSettings["DefaultSupplier"];
                }

            }

        }


        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string strIngredientName =  iTool.formatInputString(txtIngredientName.Value);
                string strPrice = iTool.formatInputString(txtPrice.Value);
                string strBarcode = iTool.formatInputString(txtBarCode.Value);
                string strOpqty = iTool.formatInputString(txtOpQty.Value);
                string strReOrdqty = iTool.formatInputString(txtReOrdQty.Value);
                string strReOrdLvl = iTool.formatInputString(txtReOrdLvl.Value);
                bool flag;

                DateTime sDate = DateTime.Now;
                sDate = Fn.GetCommonDate(sDate, Session["DateFormat"]);

                SqlParameter[] ArParams = new SqlParameter[15];
                ArParams[0] = new SqlParameter("@IngredientName", SqlDbType.VarChar, 50);
                ArParams[0].Value = strIngredientName;

                ArParams[1] = new SqlParameter("@Price", SqlDbType.Decimal, 6);
                ArParams[1].Value = strPrice;

                ArParams[2] = new SqlParameter("@UnitID", SqlDbType.Int);
                ArParams[2].Value = Unit.SelectedValue;

                ArParams[3] = new SqlParameter("@BarCode", SqlDbType.VarChar,50);
                ArParams[3].Value = strBarcode;

                ArParams[4] = new SqlParameter("@Status", SqlDbType.Int);
                ArParams[4].Value = Status.Checked ? 1 : 0;

                ArParams[5] = new SqlParameter("@sDate", SqlDbType.DateTime);
                ArParams[5].Value = sDate;

                ArParams[6] = new SqlParameter("@Rest_ID", SqlDbType.Int);
                ArParams[6].Value = Session["R_ID"];

                ArParams[7] = new SqlParameter("@LoggedUserID", SqlDbType.Int);
                ArParams[7].Value = Session["UserID"];

                ArParams[8] = new SqlParameter("@Mode", SqlDbType.VarChar,20);
                ArParams[8].Value = Mode.Value;

                ArParams[9] = new SqlParameter("@IngID", SqlDbType.Int);
                ArParams[9].Value = IngID.Value;

                ArParams[10] = new SqlParameter("@SupplierID", SqlDbType.Int);
                ArParams[10].Value = Supplier.SelectedValue;

                ArParams[11] = new SqlParameter("@IsDailyItem", SqlDbType.Bit);
                ArParams[11].Value = DailyItem.Checked ? 1 : 0; 

                ArParams[12] = new SqlParameter("@OpQty", SqlDbType.Decimal);
                ArParams[12].Value = (strOpqty == "") ? 0 : Convert.ToDecimal(strOpqty);

                ArParams[13] = new SqlParameter("@ReOrdQty", SqlDbType.Decimal);
                ArParams[13].Value = (strReOrdqty == "") ? 0 : Convert.ToDecimal(strReOrdqty);

                ArParams[14] = new SqlParameter("@ReOrdLvl", SqlDbType.Decimal);
                ArParams[14].Value = (strReOrdLvl == "") ? 0 : Convert.ToDecimal(strReOrdLvl);

                Dictionary<string, string> dict;
                dict = new Dictionary<string, string>() { { "Rest_ID", Session["R_ID"].ToString() } };

                if (IngID.Value == "-1" && (Mode.Value=="add" || Mode.Value=="clone"))
                    flag = Fn.CheckRecordExists(dict, "omni_Items_Ingredients", "IngredientName", "IngredientName", "", "", "", ArParams);
                else
                    flag = Fn.CheckRecordExists(dict, "omni_Items_Ingredients", "IngredientName", "IngredientName", "edit", "IngredientId", IngID.Value, ArParams);

                if (flag == true)
                {
                    LblIngredient.Text = "Ingredient Name already exist. Try with another one.";
                    return;
                }


                using (SqlConnection conn = mConnection.GetConnection())
                {
                    conn.Open();

                    using (SqlTransaction trans = conn.BeginTransaction())
                    {
                        // Establish command parameters
                        // @AccountNo (From Account)
                        //SqlParameter paramFromAcc = new SqlParameter("@usrgrpname", SqlDbType.VarChar,50);
                        //paramFromAcc.Value = "12345";

                        try
                        {
                            // Call ExecuteNonQuery static method of SqlHelper class for debit and credit operations.
                            // We pass in SqlTransaction object, command type, stored procedure name, and a comma delimited list of SqlParameters

                            // Perform the debit operation
                            //SqlHelper.ExecuteNonQuerys(trans, CommandType.StoredProcedure, "Debit", paramFromAcc, paramDebitAmount);
                            //SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sQuery, ArParams);
                            SqlHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, "SP_omni_Ingredient_Update", ArParams);

                            trans.Commit();
                            //txtResults.Text = "Transfer Completed";

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
            catch(Exception ex)
            {

           // CreateLogFiles.ErrorLog(ex.Message.ToString() & " - " & System.IO.Path.GetFileName(System.Web.HttpContext.Current.Request.Url.AbsolutePath) & " - btnCreate_Click")
           // lblError.Text = "Error - Please contact Administrator "
           // Exit Sub
            }
                Response.Redirect("Ingredients.aspx");
        }
}
}