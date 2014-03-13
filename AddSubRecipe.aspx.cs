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

namespace PosRecipe
{
    public partial class AddSubRecipe : System.Web.UI.Page
    {
        DB mConnection = new DB() ;
        public DataSet dsIngredients = new DataSet();
        Common Fn = new Common();
        MyToolSet iTool = new MyToolSet();
        SQLQuery Qry = new SQLQuery();

        string sQuery = "";
		public string strIngredients = "";
        public string[] arrIngredientName = null;
        public string[] arrIngredientID = null;
        public string[] arrIngredientQty = null;
        public string[] arrIngredientUnit = null;
        public int errorinpos = -1;
        public int rowStart;
        Dictionary<string, string> dict = null;


        public AddSubRecipe()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string sbrcpid = "";

            if (!Page.IsPostBack)
            {

                if (Session["R_ID"] == "" || Session["R_ID"] == null)
                {
                    Session["bckurl"] = "AddSubRecipe.aspx";
                    //Response.Redirect("Select_Restaurants.aspx?bckurl=" + Session["bckurl"]);
                    Server.Transfer("Notification.aspx");
                    return;
                }

                if (Request.QueryString["sbrcpid"] != null)
                {
                    
                    SqlParameter[] ArParams = new SqlParameter[4];

                    sbrcpid = iTool.decryptString(Request.QueryString["sbrcpid"]);

                    ArParams[0] = new SqlParameter("@SubRecipeID", SqlDbType.Int);
                    ArParams[0].Value = sbrcpid;

                    // @UserGroupName Output Parameter
                    ArParams[1] = new SqlParameter("@SubRecipeName", SqlDbType.VarChar, 150);
                    ArParams[1].Direction = ParameterDirection.Output;

                    ArParams[2] = new SqlParameter("@UnitID", SqlDbType.Int);
                    ArParams[2].Direction = ParameterDirection.Output;

                    // @Status Output Parameter
                    ArParams[3] = new SqlParameter("@Status", SqlDbType.Int);
                    ArParams[3].Direction = ParameterDirection.Output;

                    try
                    {
                        // Call ExecuteNonQuery static method of SqlHelper class
                        // We pass in database connection string, command type, stored procedure name and an array of SqlParameter objects
                        SqlHelper.ExecuteNonQuery(mConnection.GetConnection(), CommandType.StoredProcedure, "getSubRecipeDetails", ArParams);

                        // Display results in text box using the values of output parameters	
                        txtSubRecipeName.Value = ArParams[1].Value.ToString();
                        Status.Checked = ArParams[3].Value.ToString() == "1" ? true : false;
                        SubRecipeID.Value = sbrcpid;                      

                        if (ArParams[2].Value != "-1")
                            Unit.SelectedValue  = ArParams[2].Value.ToString();

                        Mode.Value = "edit";
                        

                        if (Request.QueryString["mode"] != null)
                        {
                            if (Request.QueryString["mode"] == "edit")
                                Mode.Value = "edit";
                            else if (Request.QueryString["mode"] == "clone")
                                Mode.Value = "clone";
                        }

                        //display name on top
                        string itemType = "Sub Recipe";
                        LblHead.Text = char.ToUpper(Mode.Value[0]) + Mode.Value.Substring(1) + " " + itemType + " [ " + ArParams[1].Value.ToString() + " ]";
                    }
                    catch (Exception ex)
                    {
                        // throw an exception
                        throw ex;
                    }                
                }

                if (Session["R_ID"].ToString() != "")
                    Fn.PopulateDropDown_List(Unit, Qry.getUnitSQL(Session["R_ID"].ToString()), "UnitName", "UnitID", "");

                if (Mode.Value == "edit" || Mode.Value == "clone")
                {
                    //==============  Get Ingredient / SubRecipe Details ===============================//

                    dict = null;
                    dsIngredients = null;
                    dict = new Dictionary<string, string>() { { "SubRecipeID", sbrcpid } };
                    dsIngredients = Fn.LoadIngredientsWithinRecipe(dict, "a.Rest_ID", Session["R_ID"].ToString());
                    rowStart = dsIngredients.Tables[0].Rows.Count + 1;					
                }

            }

            dict = null;
            dict = new Dictionary<string, string>() { { "a.IsActive", "1" } };
            strIngredients = Fn.LoadIngredientsAsString(dict, "a.Rest_ID", Session["R_ID"].ToString());
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            int subRecipe_id = 0;
            try
            {
                string strSubRecipeName = iTool.formatInputString(txtSubRecipeName.Value);
                bool flag = false;
                bool flag2 = true;

                DateTime sDate = DateTime.Now;
                sDate = Fn.GetCommonDate(sDate, Session["DateFormat"]);

                string[] ingredient_ID = Request.Form.GetValues("IngredientID");
                string[] ingredient_Name = Request.Form.GetValues("IngredientName");
                string[] ingredient_Qty = Request.Form.GetValues("IngredientQty");
                string[] ingredient_Unit = Request.Form.GetValues("IngredientUnit");

                Session["ingredient_ID"] = Request.Form.GetValues("IngredientID");
                Session["ingredient_Name"] = Request.Form.GetValues("IngredientName");
                Session["ingredient_Qty"] = Request.Form.GetValues("IngredientQty");
                Session["ingredient_Unit"] = Request.Form.GetValues("IngredientUnit");

                SqlParameter[] ArParams = new SqlParameter[8];
                ArParams[0] = new SqlParameter("@SubRecipeName", SqlDbType.VarChar, 150);
                ArParams[0].Value = strSubRecipeName;

                ArParams[1] = new SqlParameter("@UnitID", SqlDbType.Int);
                ArParams[1].Value = Unit.SelectedValue; ;

                ArParams[2] = new SqlParameter("@Status", SqlDbType.Int);
                ArParams[2].Value = Status.Checked ? 1 : 0;

                ArParams[3] = new SqlParameter("@sDate", SqlDbType.DateTime);
                ArParams[3].Value = sDate;

                ArParams[4] = new SqlParameter("@LoggedUserID", SqlDbType.Int);
                ArParams[4].Value = Session["UserID"];

                ArParams[5] = new SqlParameter("@Mode", SqlDbType.VarChar,20);
                ArParams[5].Value = Mode.Value;

                ArParams[6] = new SqlParameter("@SubRecipeID", SqlDbType.Int);
                ArParams[6].Value = SubRecipeID.Value;

                ArParams[7] = new SqlParameter("@Rest_ID", SqlDbType.Int);
                ArParams[7].Value = Session["R_ID"]; ;

                dict = null;
                dict = new Dictionary<string, string>() { { "Rest_ID", Session["R_ID"].ToString() } };

                if (SubRecipeID.Value == "-1" && (Mode.Value=="add" || Mode.Value=="clone"))
                    flag = Fn.CheckRecordExists(dict, "omni_SubRecipe", "SubRecipeName", "SubRecipeName", "", "", "", ArParams);
                else
                    flag = Fn.CheckRecordExists(dict, "omni_SubRecipe", "SubRecipeName", "SubRecipeName", "edit", "SubRecipeId", SubRecipeID.Value, ArParams);

                if (flag == true)
                {
                    //LblSubRecipe.Text = "Sub Recipe Name already exist. Try with another one.";
                    ErrorMsg.InnerText = "Sub Recipe Name already exist. Try with another one.";
                    Msg.Visible = true;
                    return;
                }

                if (ingredient_ID != null && ingredient_ID.Length >0)
                {
                    for (int i = 0; i < ingredient_ID.Length; i++)
                    {
                        if (ingredient_ID[i].Trim() == "" && ingredient_Name[i].Trim() == "")
                        {
                            flag2 = false;
                            errorinpos = i;
                            break;
                        }
                        else
                        {
                            dict = null;
                            dict = new Dictionary<string, string>() { { "IsActive", "1" }, { "Rest_ID", Session["R_ID"].ToString() } };
                            flag2 = Fn.CheckRecordExists(dict, "omni_Items_Ingredients", "IngredientName", iTool.formatInputString(ingredient_Name[i].ToString()));

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
                    ErrorMsg.InnerText = "!! Error In Submission. Either No Ingredient(s) is choosen or Wrong Ingrdient entered.";
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
                        // Establish command parameters
                        // @AccountNo (From Account)
                        //SqlParameter paramFromAcc = new SqlParameter("@usrgrpname", SqlDbType.VarChar,50);
                        //paramFromAcc.Value = "12345";

                        try
                        {
                            SqlHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, "SP_omni_SubRecipe_Update", ArParams);

                            if (Mode.Value == "add" || Mode.Value == "clone")
                                subRecipe_id = (int)SqlHelper.ExecuteScalar(trans, CommandType.Text, Qry.getLastInsertedID(null, "omni_SubRecipe", "SubRecipeid", 1, "", ""));
                            else
                                subRecipe_id = Convert.ToInt32(SubRecipeID.Value);


                            SqlParameter[] ArParamsT = new SqlParameter[4];

                            if (Mode.Value == "edit")
                                SqlHelper.ExecuteNonQuery(trans, CommandType.Text, Qry.DeleteRec(null, "omni_SubRecipe_Mixing_Details", 1, "SubRecipeID", subRecipe_id.ToString()));


							SqlParameter[] ArParamsD = new SqlParameter[5];

                            if (ingredient_ID != null)
                            {
                                for (int i = 0; i < ingredient_ID.Length; i++)
                                {
                                    if (ingredient_ID[i].Trim() != "" && ingredient_Name[i].Trim() != "" && ingredient_Qty[i] != "" && ingredient_Unit[i]!="")
                                    {
                                        ArParamsD[0] = new SqlParameter("@SubRecipeID", SqlDbType.Int);
                                        ArParamsD[0].Value = subRecipe_id;

                                        ArParamsD[1] = new SqlParameter("@IngID", SqlDbType.Int);
                                        ArParamsD[1].Value = (ingredient_ID[i] != "" || ingredient_ID[i] != null) ? iTool.formatInputString(ingredient_ID[i]) : "";

                                        ArParamsD[2] = new SqlParameter("@Qty", SqlDbType.Decimal,6);
                                        ArParamsD[2].Value = (ingredient_Qty[i] != "" || ingredient_Qty[i] != null) ? iTool.formatInputString(ingredient_Qty[i]) : "";

                                        ArParamsD[3] = new SqlParameter("@RestID", SqlDbType.Int);
                                        ArParamsD[3].Value = Session["R_ID"];

                                        ArParamsD[4] = new SqlParameter("@Mode", SqlDbType.VarChar, 20);
                                        ArParamsD[4].Value = Mode.Value;

										SqlHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, "SP_omni_SubRecipe_Mixing_Options_Update", ArParamsD);
                                    }
                                }
                            }
							
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



                //SqlHelper.ExecuteNonQuery(mConnection.GetConnection(), CommandType.Text, sQuery, ArParams);

            }
            catch(Exception ex)
            {

           // CreateLogFiles.ErrorLog(ex.Message.ToString() & " - " & System.IO.Path.GetFileName(System.Web.HttpContext.Current.Request.Url.AbsolutePath) & " - btnCreate_Click")
           // lblError.Text = "Error - Please contact Administrator "
           // Exit Sub
            }
                Response.Redirect("SubRecipes.aspx");
        }

}
}