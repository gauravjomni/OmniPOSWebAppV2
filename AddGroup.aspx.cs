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

namespace PosUserGroup
{
    public partial class AddGroup : System.Web.UI.Page
    {
        DB mConnection = new DB() ;
        DataSet ds = new DataSet();
        Common Fn = new Common();
        MyToolSet iTool = new MyToolSet();
        string sQuery = "";


        public AddGroup()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                if (Request.QueryString["grpid"] != null)
                {
                    string grpid = "";
                    SqlParameter[] ArParams = new SqlParameter[3];

                    grpid = iTool.decryptString(Request.QueryString["grpid"]);

                    ArParams[0] = new SqlParameter("@UserGroupID", SqlDbType.Int);
                    ArParams[0].Value = grpid;

                    // @UserGroupName Output Parameter
                    ArParams[1] = new SqlParameter("@UserGroupName", SqlDbType.VarChar, 50);
                    ArParams[1].Direction = ParameterDirection.Output;

                    // @Status Output Parameter
                    ArParams[2] = new SqlParameter("@Status", SqlDbType.Int);
                    ArParams[2].Direction = ParameterDirection.Output;

                    try
                    {
                        // Call ExecuteNonQuery static method of SqlHelper class
                        // We pass in database connection string, command type, stored procedure name and an array of SqlParameter objects
                        SqlHelper.ExecuteNonQuery(mConnection.GetConnection(), CommandType.StoredProcedure, "getUserGroupDetails", ArParams);

                        // Display results in text box using the values of output parameters	
                        txtGroupName.Value = ArParams[1].Value.ToString();
                        Status.Checked = ArParams[2].Value.ToString() == "1" ? true : false;
                        Mode.Value = "edit";
                        GroupID.Value = grpid;

                        //display name on top
                        string itemType = "User Group";
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
            }
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string strUsrGrpName = iTool.formatInputString(txtGroupName.Value);
                bool flag = false;

                DateTime sDate = DateTime.Now;
                sDate = Fn.GetCommonDate(sDate, Session["DateFormat"]);

                SqlParameter[] ArParams = new SqlParameter[6];
                ArParams[0] = new SqlParameter("@UserGroupName", SqlDbType.VarChar, 50);
                ArParams[0].Value = strUsrGrpName;

                ArParams[1] = new SqlParameter("@Status", SqlDbType.Int);
                ArParams[1].Value = Status.Checked ? 1 : 0;

                ArParams[2] = new SqlParameter("@sDate", SqlDbType.DateTime);
                ArParams[2].Value = sDate;

                ArParams[3] = new SqlParameter("@LoggedUserID", SqlDbType.Int);
                ArParams[3].Value = Session["UserID"];

                ArParams[4] = new SqlParameter("@Mode", SqlDbType.VarChar,20);
                ArParams[4].Value = Mode.Value;

                ArParams[5] = new SqlParameter("@UserGroupID", SqlDbType.Int);
                ArParams[5].Value = GroupID.Value;

                Dictionary<string, string> dict;

                if (GroupID.Value == "-1")
                {
                    dict = null;
                    flag = Fn.CheckRecordExists(dict, "omni_user_group", "UserGroupName", "UserGroupName", "", "", "", ArParams);
                }
                else
                {
                    //========= FOR IMPLEMENTING MORE "AND" CLAUSE IN SQL OPERATION ================//

                    //dict = new Dictionary<string, string>() { { "UserGroupId", GroupID.Value } };
                    //flag = Fn.CheckRecordExists(dict, "omni_user_group", "UserGroupName", "UserGroupName", "edit", "UserGroupId", GroupID.Value, ArParams);

                    //=============================================================================//

                    flag = Fn.CheckRecordExists(null, "omni_user_group", "UserGroupName", "UserGroupName", "edit", "UserGroupId", GroupID.Value, ArParams);
                }

                if (flag == true)
                {
                    LblGrp.Text = "Group Name already exist. Try with another one.";
                    return;
                }

                //if (Request.QueryString["grpid"] == null && Mode.Value=="add")
                //{
                //    sQuery = "insert into omni_user_group(UserGroupName,CreateDate,IsActive) " +
                //        " values(@usrgrpname,@CreateDate,@status)";
                //}
                //else
                //{
                //    sQuery = "update omni_user_group set UserGroupName = @usrgrpname,IsActive=@status where UserGroupID = " + Request.QueryString["grpid"];
                //}

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
                            SqlHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, "SP_omni_user_group_Update", ArParams);

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
                Response.Redirect("UserGroups.aspx");
        }
}
}