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

namespace PosKitchens
{
    public partial class AddKitchen : System.Web.UI.Page
    {
        DB mConnection = new DB() ;
        DataSet ds = new DataSet();
        Common Fn = new Common();
        MyToolSet iTool = new MyToolSet();
        string sQuery = "";


        public AddKitchen()
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
                    Session["bckurl"] = "AddKitchen.aspx";
                    //Response.Redirect("Select_Restaurants.aspx?bckurl=" + Session["bckurl"]);
                    Server.Transfer("Notification.aspx");
                    return;
                }


                if (Request.QueryString["kid"] != null)
                {
                    string kid = "";
                    SqlParameter[] ArParams = new SqlParameter[3];

                    kid = iTool.decryptString(Request.QueryString["kid"]);

                    ArParams[0] = new SqlParameter("@KitchenID", SqlDbType.Int);
                    ArParams[0].Value = kid;

                    // @UserGroupName Output Parameter
                    ArParams[1] = new SqlParameter("@KitchenName", SqlDbType.VarChar, 50);
                    ArParams[1].Direction = ParameterDirection.Output;

                    // @Status Output Parameter
                    ArParams[2] = new SqlParameter("@Status", SqlDbType.Int);
                    ArParams[2].Direction = ParameterDirection.Output;

                    try
                    {
                        // Call ExecuteNonQuery static method of SqlHelper class
                        // We pass in database connection string, command type, stored procedure name and an array of SqlParameter objects
                        SqlHelper.ExecuteNonQuery(mConnection.GetConnection(), CommandType.StoredProcedure, "getKitchenDetails", ArParams);

                        // Display results in text box using the values of output parameters	
                        txtKitchenName.Value = ArParams[1].Value.ToString();
                        Status.Checked = ArParams[2].Value.ToString() == "1" ? true : false;
                        //Mode.Value = "edit";
                        //KID.Value =  kid;

                        if (Request.QueryString["mode"] != null)
                        {
                            if (Request.QueryString["mode"] == "edit")
                                Mode.Value = "edit";
                            else if (Request.QueryString["mode"] == "clone")
                                Mode.Value = "clone";
                        }

                        //display name on top
                        string itemType = "Kitchen";
                        LblHead.Text = char.ToUpper(Mode.Value[0]) + Mode.Value.Substring(1) + " " + itemType + " [ " + ArParams[1].Value.ToString() + " ]";

                        if (Mode.Value == "add" || Mode.Value == "clone")
                            KID.Value = "-1";
                        else
                            KID.Value = kid;

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
                string strKitchenName = iTool.formatInputString(txtKitchenName.Value);
                bool flag = false;

                DateTime sDate = DateTime.Now;
                sDate = Fn.GetCommonDate(sDate, Session["DateFormat"]);

                SqlParameter[] ArParams = new SqlParameter[7];
                ArParams[0] = new SqlParameter("@KitchenName", SqlDbType.VarChar, 50);
                ArParams[0].Value = strKitchenName;

                ArParams[1] = new SqlParameter("@Rest_ID", SqlDbType.Int);
                ArParams[1].Value = Session["R_ID"];

                ArParams[2] = new SqlParameter("@Status", SqlDbType.Int);
                ArParams[2].Value = Status.Checked ? 1 : 0;

                ArParams[3] = new SqlParameter("@sDate", SqlDbType.DateTime);
                ArParams[3].Value = sDate;

                ArParams[4] = new SqlParameter("@LoggedUserID", SqlDbType.Int);
                ArParams[4].Value = Session["UserID"];

                ArParams[5] = new SqlParameter("@Mode", SqlDbType.VarChar,20);
                ArParams[5].Value = Mode.Value;

                ArParams[6] = new SqlParameter("@KitchenID", SqlDbType.Int);
                ArParams[6].Value = KID.Value;

                Dictionary<string, string> dict;
                dict = new Dictionary<string, string>() { { "Rest_ID", Session["R_ID"].ToString() } };

                if (KID.Value == "-1" && (Mode.Value == "add" || Mode.Value == "clone"))
                {
                    dict = null;
                    flag = Fn.CheckRecordExists(dict, "omni_Kitchen", "KitchenName", "KitchenName", "", "", "", ArParams);
                }
                else
                {
                    //========= FOR IMPLEMENTING MORE "AND" CLAUSE IN SQL OPERATION ================//

                    //dict = new Dictionary<string, string>() { { "UserGroupId", GroupID.Value } };
                    //flag = Fn.CheckRecordExists(dict, "omni_user_group", "UserGroupName", "UserGroupName", "edit", "UserGroupId", GroupID.Value, ArParams);

                    //=============================================================================//

                    flag = Fn.CheckRecordExists(dict, "omni_Kitchen", "KitchenName", "KitchenName", "edit", "KitchenID", KID.Value, ArParams);
                }

                if (flag == true)
                {
                    LblKitchen.Text = "Kitchen Name already exist. Try with another one.";
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
                            SqlHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, "SP_omni_kitchen_Update", ArParams);

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
                Response.Redirect("Kitchens.aspx");
        }
    }
}