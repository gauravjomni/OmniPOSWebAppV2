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
using Commons;

namespace PosModifiers
{
    public partial class AddModifierLevel : System.Web.UI.Page
    {
        DB mConnection = new DB() ;
        DataSet ds = new DataSet();
        Common Fn = new Common();
        MyToolSet iTool = new MyToolSet();
        string sQuery = "";


        public AddModifierLevel()
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
                    Session["bckurl"] = "AddModifierLevel.aspx";
                    //Response.Redirect("Select_Restaurants.aspx?bckurl=" + Session["bckurl"]);
                    Server.Transfer("Notification.aspx");
                    return;
                }


                if (Request.QueryString["modfid"] != null)
                {
                    string modfid = "";
                    SqlParameter[] ArParams = new SqlParameter[3];
                    
                    modfid =  iTool.decryptString(Request.QueryString["modfid"]);


                    ArParams[0] = new SqlParameter("@LevelID", SqlDbType.Int);
                    ArParams[0].Value = modfid;

                    // @UserGroupName Output Parameter
                    ArParams[1] = new SqlParameter("@ModifierLevelName", SqlDbType.VarChar, 50);
                    ArParams[1].Direction = ParameterDirection.Output;

                    // @Status Output Parameter
                    ArParams[2] = new SqlParameter("@Status", SqlDbType.Int);
                    ArParams[2].Direction = ParameterDirection.Output;

                    try
                    {
                        // Call ExecuteNonQuery static method of SqlHelper class
                        // We pass in database connection string, command type, stored procedure name and an array of SqlParameter objects
                        SqlHelper.ExecuteNonQuery(mConnection.GetConnection(), CommandType.StoredProcedure, "getModifierLevelDetails", ArParams);

                        // Display results in text box using the values of output parameters	
                        txtModLevelName.Value = ArParams[1].Value.ToString();
                        Status.Checked = ArParams[2].Value.ToString() == "1" ? true : false;
                        //Mode.Value = "edit";
                        

                        if (Request.QueryString["mode"] != null)
                        {
                            if (Request.QueryString["mode"] == "edit")
                                Mode.Value = "edit";
                            else if (Request.QueryString["mode"] == "clone")
                                Mode.Value = "clone";
                        }

                        //display name on top
                        string itemType = "Modifier Level";
                        LblHead.Text = char.ToUpper(Mode.Value[0]) + Mode.Value.Substring(1) + " " + itemType + " [ " + ArParams[1].Value.ToString() + " ]";

                        if (Mode.Value == "add" || Mode.Value == "clone")
                            ModLevelID.Value = "-1";
                        else
                            ModLevelID.Value = modfid;

                    }
                    catch (Exception ex)
                    {
                        // throw an exception
                        throw ex;
                    }

//                    sQuery = "selinsert into omni_user_group(UserGroupName,CreateDate,IsActive) " +
//                    " values(@usrgrpname,@CreateDate,@status)";
                }
            }
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string strModLevelName = iTool.formatInputString(txtModLevelName.Value);
                bool flag = false;

                DateTime sDate = DateTime.Now;
                sDate = Fn.GetCommonDate(sDate, Session["DateFormat"]);

                SqlParameter[] ArParams = new SqlParameter[7];
                ArParams[0] = new SqlParameter("@ModifierLevelName", SqlDbType.VarChar, 50);
                ArParams[0].Value = strModLevelName;

                ArParams[1] = new SqlParameter("@Status", SqlDbType.Int);
                ArParams[1].Value = Status.Checked ? 1 : 0;

                ArParams[2] = new SqlParameter("@sDate", SqlDbType.DateTime);
                ArParams[2].Value = sDate;

                ArParams[3] = new SqlParameter("@LoggedUserID", SqlDbType.Int);
                ArParams[3].Value = Session["UserID"];

                ArParams[4] = new SqlParameter("@Mode", SqlDbType.VarChar,20);
                ArParams[4].Value = Mode.Value;

                ArParams[5] = new SqlParameter("@RestID", SqlDbType.Int);
                ArParams[5].Value = Session["R_ID"];

                ArParams[6] = new SqlParameter("@LevelID", SqlDbType.Int);
                ArParams[6].Value = ModLevelID.Value;

                Dictionary<string, string> dict;
                dict = new Dictionary<string, string>() { { "Rest_ID", Session["R_ID"].ToString() } };

                if (ModLevelID.Value == "-1" && (Mode.Value=="add" || Mode.Value=="clone"))
                {
                    //dict = null;
                    flag = Fn.CheckRecordExists(dict, "omni_Modifiers_Level", "ModifierLevelName", "ModifierLevelName", "", "", "", ArParams);
                }
                else
                {
                    //========= FOR IMPLEMENTING MORE "AND" CLAUSE IN SQL OPERATION ================//

                    //dict = new Dictionary<string, string>() { { "UserGroupId", GroupID.Value } };
                    //flag = Fn.CheckRecordExists(dict, "omni_user_group", "UserGroupName", "UserGroupName", "edit", "UserGroupId", GroupID.Value, ArParams);

                    //=============================================================================//

                    flag = Fn.CheckRecordExists(dict, "omni_Modifiers_Level", "ModifierLevelName", "ModifierLevelName", "edit", "LevelId", ModLevelID.Value, ArParams);
                }

                if (flag == true)
                {
                    LblModName.Text = "Modifiers Level Name already exist. Try with another one.";
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
                            SqlHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, "SP_omni_modifer_level_Update", ArParams);

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
                Response.Redirect("Modifierlevel.aspx");
        }
}
}