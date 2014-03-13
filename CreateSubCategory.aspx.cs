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

namespace PosCategory
{
    public partial class AddSubCategory : System.Web.UI.Page
    {
        DB mConnection = new DB() ;
        DataSet ds = new DataSet();
        Common Fn = new Common();
        SQLQuery Qry = new SQLQuery();
        MyToolSet iTool = new MyToolSet();
        //string sQuery = "";


        public AddSubCategory()
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
                    Session["bckurl"] = "CreateSubCategory.aspx";
                    Server.Transfer("Notification.aspx");
                    return;
                }

                if (Request.QueryString["catid"] != null)
                {
                    string catid = "";
                    SqlParameter[] ArParams = new SqlParameter[6];

                    catid = iTool.decryptString(Request.QueryString["catid"]);

                    ArParams[0] = new SqlParameter("@CategoryId", SqlDbType.Int);
                    ArParams[0].Value = catid;

                    // @UserGroupName Output Parameter
                    ArParams[1] = new SqlParameter("@CategoryName", SqlDbType.VarChar, 50);
                    ArParams[1].Direction = ParameterDirection.Output;

                    ArParams[2] = new SqlParameter("@ParentID", SqlDbType.Int);
                    ArParams[2].Direction = ParameterDirection.Output;

                    // @Status Output Parameter
                    ArParams[3] = new SqlParameter("@Status", SqlDbType.Int);
                    ArParams[3].Direction = ParameterDirection.Output;

                    ArParams[4] = new SqlParameter("@Name2", SqlDbType.VarChar,100);
                    ArParams[4].Direction = ParameterDirection.Output;

                    ArParams[5] = new SqlParameter("@SortOrder", SqlDbType.Int);
                    ArParams[5].Direction = ParameterDirection.Output;
                    
                    try
                    {
                        // Call ExecuteNonQuery static method of SqlHelper class
                        // We pass in database connection string, command type, stored procedure name and an array of SqlParameter objects
                        SqlHelper.ExecuteNonQuery(mConnection.GetConnection(), CommandType.StoredProcedure, "getCategoryDetails", ArParams);

                        // Display results in text box using the values of output parameters	
                        txtCatName.Value = ArParams[1].Value.ToString();
                        ParentCategory.SelectedValue = ArParams[2].Value.ToString();
                        Status.Checked = ArParams[3].Value.ToString() == "1" ? true : false;
                        txtCatName2.Value = ArParams[4].Value.ToString();
                        txtSort.Value = ArParams[5].Value.ToString();
                        //Mode.Value = "edit";
                        //CatID.Value = catid;

                        if (Request.QueryString["mode"] != null)
                        {
                            if (Request.QueryString["mode"] == "edit")
                                Mode.Value = "edit";
                            else if (Request.QueryString["mode"] == "clone")
                                Mode.Value = "clone";
                        }

                        //display name on top
                        string itemType = "Subcategory";
                        LblHead.Text = char.ToUpper(Mode.Value[0]) + Mode.Value.Substring(1) + " " + itemType + " [ " + ArParams[1].Value.ToString() + " ]";

                        if (Mode.Value == "add" || Mode.Value == "clone")
                            CatID.Value = "-1";
                        else
                            CatID.Value = catid;

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
                dict = new Dictionary<string, string>() { { "Rest_ID", Session["R_ID"].ToString() } , { "IsActive","1" } };

                Fn.PopulateDropDown_List(ParentCategory,Qry.getParentCategorySQL(dict), "CategoryName", "CategoryID", "");

                //if (Mode.Value == "add" || Mode.Value == "clone")
                if (Mode.Value == "add")
                    txtSort.Value = System.Configuration.ConfigurationSettings.AppSettings["DefaultSortOrder"];

            }
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string strCatName = iTool.formatInputString(txtCatName.Value);
                string strCatName2 = iTool.formatInputString(txtCatName2.Value);
                string strSortOrder = iTool.formatInputString(txtSort.Value);

                bool flag = false;

                DateTime sDate = DateTime.Now;
                sDate = Fn.GetCommonDate(sDate, Session["DateFormat"]);

                SqlParameter[] ArParams = new SqlParameter[10];
                ArParams[0] = new SqlParameter("@CategoryName", SqlDbType.VarChar, 50);
                ArParams[0].Value = strCatName;

                ArParams[1] = new SqlParameter("@ParentID", SqlDbType.Int);
                ArParams[1].Value = ParentCategory.SelectedValue;
                
                ArParams[2] = new SqlParameter("@Status", SqlDbType.Int);
                ArParams[2].Value = Status.Checked ? 1 : 0;

                ArParams[3] = new SqlParameter("@sDate", SqlDbType.DateTime);
                ArParams[3].Value = sDate;

                ArParams[4] = new SqlParameter("@LoggedUserID", SqlDbType.Int);
                ArParams[4].Value = Session["UserID"];

                ArParams[5] = new SqlParameter("@Mode", SqlDbType.VarChar,20);
                ArParams[5].Value = Mode.Value;

                ArParams[6] = new SqlParameter("@CategoryId", SqlDbType.Int);
                ArParams[6].Value = CatID.Value;

                ArParams[7] = new SqlParameter("@Rest_ID", SqlDbType.Int);
                ArParams[7].Value = Session["R_ID"];

                ArParams[8] = new SqlParameter("@Name2", SqlDbType.VarChar,100);
                ArParams[8].Value = strCatName2;

                ArParams[9] = new SqlParameter("@SortOrder", SqlDbType.Int);
                ArParams[9].Value = strSortOrder;

                Dictionary<string, string> dict;
                dict = new Dictionary<string, string>() { { "Rest_ID", Session["R_ID"].ToString() } };

                if ( CatID.Value == "-1" && (Mode.Value =="add" || Mode.Value=="clone"))
                {
                    //dict = null;
                    flag = Fn.CheckRecordExists(dict, "omni_Item_Categories", "CategoryName", "CategoryName", "", "", "", ArParams);
                }
                else
                {
                    //========= FOR IMPLEMENTING MORE "AND" CLAUSE IN SQL OPERATION ================//

                    //dict = new Dictionary<string, string>() { { "UserGroupId", GroupID.Value } };
                    //flag = Fn.CheckRecordExists(dict, "omni_user_group", "UserGroupName", "UserGroupName", "edit", "UserGroupId", GroupID.Value, ArParams);

                    //=============================================================================//

                    flag = Fn.CheckRecordExists(dict, "omni_Item_Categories", "CategoryName", "CategoryName", "edit", "CategoryID", CatID.Value, ArParams);
                }

                if (flag == true)
                {
                    LblGrp.Text = "SubCategory Name already exist. Try with another one.";
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
                            SqlHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, "SP_omni_cate_subcate_Update", ArParams);

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
                Response.Redirect("Categories.aspx");
        }
    }
}