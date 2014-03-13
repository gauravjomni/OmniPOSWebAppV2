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
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.ApplicationBlocks.Data;
using MyDB;
using MyTool;
using MyQuery;
using Commons;

namespace PosModifiers
{
    public partial class AddModifier : System.Web.UI.Page
    {
        DB mConnection = new DB() ;
        DataSet ds = new DataSet();
        Common Fn = new Common();
        MyToolSet iTool = new MyToolSet();
        SQLQuery Qry = new SQLQuery();
        //string sQuery = "";


        public AddModifier()
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
                    Session["bckurl"] = "AddModifier.aspx";
                    //Response.Redirect("Select_Restaurants.aspx?bckurl=" + Session["bckurl"]);
                    Server.Transfer("Notification.aspx");
                    return;
                }

                if (Request.QueryString["modfid"] != null)
                {
                    string modfid = "";

                    if (Request.QueryString["mode"] != null)
                    {
                        if (Request.QueryString["mode"] == "edit")
                            Mode.Value = "edit";
                        else if (Request.QueryString["mode"] == "clone")
                            Mode.Value = "clone";
                    }


                    SqlParameter[] ArParams = new SqlParameter[9];

                    modfid = iTool.decryptString(Request.QueryString["modfid"]);

                    ArParams[0] = new SqlParameter("@ModifierId", SqlDbType.Int);
                    ArParams[0].Value = modfid;

                    // @UserName Output Parameter
                    ArParams[1] = new SqlParameter("@ModifierName", SqlDbType.VarChar, 50);
                    ArParams[1].Direction = ParameterDirection.Output;

                    // @Status Output Parameter
                    ArParams[2] = new SqlParameter("@Price1", SqlDbType.Decimal);
                    ArParams[2].Direction = ParameterDirection.Output;
                    ArParams[2].Scale = 2;

                    ArParams[3] = new SqlParameter("@ModifierLevelID", SqlDbType.Int);
                    ArParams[3].Direction = ParameterDirection.Output;

                    ArParams[4] = new SqlParameter("@SortOrder", SqlDbType.Int);
                    ArParams[4].Direction = ParameterDirection.Output;

                    // @Status Output Parameter
                    ArParams[5] = new SqlParameter("@Status", SqlDbType.Int);
                    ArParams[5].Direction = ParameterDirection.Output;

                    ArParams[6] = new SqlParameter("@GST", SqlDbType.Bit);
                    ArParams[6].Direction = ParameterDirection.Output;

                    ArParams[7] = new SqlParameter("@Name2", SqlDbType.VarChar,50);
                    ArParams[7].Direction = ParameterDirection.Output;

                    ArParams[8] = new SqlParameter("@Price2", SqlDbType.Decimal);
                    ArParams[8].Direction = ParameterDirection.Output;
                    ArParams[8].Scale = 2;
                    
                    try
                    {
                        // Call ExecuteNonQuery static method of SqlHelper class
                        // We pass in database connection string, command type, stored procedure name and an array of SqlParameter objects
                        SqlHelper.ExecuteNonQuery(mConnection.GetConnection(), CommandType.StoredProcedure, "getModifierDetails", ArParams);

                        // Display results in text box using the values of output parameters	
                        
                        txtModName.Value = ArParams[1].Value.ToString();
                        txtPrice1.Value =  ArParams[2].Value.ToString();
                        TxtOrder.Value = ArParams[4].Value.ToString();
                        Status.Checked = ArParams[5].Value.ToString() == "1" ? true : false;
                        ChkGST.Checked = ArParams[6].Value.ToString() == "True" ? true : false;
                        ModifierLevel.SelectedValue = ArParams[3].Value.ToString();
                        txtName2.Value = ArParams[7].Value.ToString();
                        txtPrice2.Value = ArParams[8].Value.ToString();
                        //ModifierID.Value = modfid;

                        if (Mode.Value == "add" || Mode.Value == "clone")
                            ModifierID.Value = "-1";
                        else
                            ModifierID.Value = modfid;

                        //display name on top
                        string itemType = "Product Modifier";
                        LblHead.Text = char.ToUpper(Mode.Value[0]) + Mode.Value.Substring(1) + " " + itemType + " [ " + ArParams[1].Value.ToString() + " ]";

                    }
                    catch (Exception ex)
                    {
                        // throw an exception
                        throw ex;
                    }

                    //                    sQuery = "selinsert into omni_user_group(UserGroupName,CreateDate,IsActive) " +
                    //                    " values(@usrgrpname,@CreateDate,@status)";
                }

                Dictionary<string, string> dict;
                dict = new Dictionary<string, string>() { { "Rest_ID", Session["R_ID"].ToString() } };

                Fn.PopulateDropDown_List(ModifierLevel, Qry.getModiferLevelSQL(dict,"IsActive","1"), "ModifierLevelName", "LevelId", "");
                txtPrice1.Attributes.Add("onkeyup", "copyval('" + txtPrice1.ClientID + "','" + txtPrice2.ClientID + "')");

                if (Mode.Value == "add")
                {
                    TxtOrder.Value = System.Configuration.ConfigurationSettings.AppSettings["DefaultSortOrder"];
                    txtPrice1.Value = System.Configuration.ConfigurationSettings.AppSettings["DefaultPriceValue"];
                    txtPrice2.Value = System.Configuration.ConfigurationSettings.AppSettings["DefaultPriceValue"];
                }
            }
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                bool flag1 = false;


                string strModifierName = iTool.formatInputString(txtModName.Value);
                string strPrice1 = iTool.formatInputString(txtPrice1.Value);
                string strSortOrder = iTool.formatInputString(TxtOrder.Value);
                string strName2 = iTool.formatInputString(txtName2.Value);
                string strPrice2 = iTool.formatInputString(txtPrice2.Value);

                DateTime sDate = DateTime.Now;
                sDate = Fn.GetCommonDate(sDate, Session["DateFormat"]);

                SqlParameter[] ArParams = new SqlParameter[13];
                ArParams[0] = new SqlParameter("@ModifierName", SqlDbType.VarChar, 50);
                ArParams[0].Value = strModifierName;

                ArParams[1] = new SqlParameter("@Price1", SqlDbType.Decimal);
                ArParams[1].Value = strPrice1;

                ArParams[2] = new SqlParameter("@SortOrder", SqlDbType.Int);
                ArParams[2].Value = strSortOrder;

                ArParams[3] = new SqlParameter("@ModifierLevelID", SqlDbType.Int);
                ArParams[3].Value = ModifierLevel.SelectedValue;

                ArParams[4] = new SqlParameter("@Status", SqlDbType.Int);
                ArParams[4].Value = Status.Checked ? 1 : 0;

                ArParams[5] = new SqlParameter("@GST", SqlDbType.Int);
                ArParams[5].Value = ChkGST.Checked  ? 1 : 0;

                ArParams[6] = new SqlParameter("@RestID", SqlDbType.Int);
                ArParams[6].Value = Session["R_ID"];
                
                ArParams[7] = new SqlParameter("@sDate", SqlDbType.DateTime);
                ArParams[7].Value = sDate;

                ArParams[8] = new SqlParameter("@LoggedUserID", SqlDbType.Int);
                ArParams[8].Value = Convert.ToInt32(Session["UserID"]);

                ArParams[9] = new SqlParameter("@Mode", SqlDbType.VarChar,20);
                ArParams[9].Value = Mode.Value;

                ArParams[10] = new SqlParameter("@ModifierId", SqlDbType.Int);
                ArParams[10].Value = ModifierID.Value;

                ArParams[11] = new SqlParameter("@Name2", SqlDbType.VarChar,50);
                ArParams[11].Value = strName2;

                ArParams[12] = new SqlParameter("@Price2", SqlDbType.Decimal);
                ArParams[12].Value = strPrice2!="" ? strPrice2 : "0.00";

                Dictionary<string, string> dict;

                dict = new Dictionary<string, string>() { { "Rest_ID", Session["R_ID"].ToString() } };

                if (ModifierID.Value == "-1" && (Mode.Value == "add" || Mode.Value=="clone"))
                {
//                    dict = null;
                    flag1 = Fn.CheckRecordExists(dict, "omni_Modifiers", "ModifierName", "ModifierName", "", "", "", ArParams);
                }
                else
                {
                    //========= FOR IMPLEMENTING MORE "AND" CLAUSE IN SQL OPERATION ================//

                    //dict = new Dictionary<string, string>() { { "UserGroupId", GroupID.Value } };
                    //flag = Fn.CheckRecordExists(dict, "omni_user_group", "UserGroupName", "UserGroupName", "edit", "UserGroupId", GroupID.Value, ArParams);

                    //=============================================================================//

                    flag1 = Fn.CheckRecordExists(dict, "omni_Modifiers", "ModifierName", "ModifierName", "edit", "ModifierID", ModifierID.Value, ArParams);

                }

                if (flag1 == true)
                {
                    LblModName.Text = "Modifier Name already exist. Try with another one.";
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
                            //SqlHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, "Debit", paramFromAcc, paramDebitAmount);
                            //SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sQuery, ArParams);
                            SqlHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, "SP_omni_Modifier_Update", ArParams);

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
                Response.Redirect("Modifiers.aspx");
        }

    }
}