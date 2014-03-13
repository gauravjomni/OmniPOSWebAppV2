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
using Commons;
using MyTool;


public partial class Admin_AddCompany : System.Web.UI.Page
{
    DB mConnection = new DB();
    DataSet ds = new DataSet();
    Common Fn = new Common();
    protected MyToolSet iTool = new MyToolSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AdminUserID"] == "" || Session["AdminUserID"] == null)
        {
            Session["bckurl"] = "AddCompany.aspx";
            //Response.Redirect("Select_Restaurants.aspx?bckurl=" + Session["bckurl"]);
            Server.Transfer("OmniLogin.aspx");
            return;
        }
        else
        {
            Header_LoggedUserName.Text = Session["AdminUserID"].ToString();
        }
        LblCodeName.Text = "";
        LblCodeName.Text = "";
        LblDBName.Text = "";
        LblPasswordConf.Text = "";
        lblError.Text = "";

        if (!IsPostBack)
        {
            if (Request["comid"] != null && Request["comid"] != "")
            {
                BtnUpdate.Visible = true;
                BtnCreate.Visible = true;
                using (SqlConnection conn = mConnection.GetAdminConnection())
                {
                    try
                    {
                        conn.Open();
                        Dictionary<string, string> dict = new Dictionary<string, string>() { { "companyId", iTool.decryptString(Request["comid"].ToString()) } };
                        ds = Fn.LoadAdminCompanies(dict, conn);
                        if (ds != null)
                        {
                            if (ds.Tables.Count > 0)
                            {
                                txName.Value = ds.Tables[0].Rows[0]["Name"].ToString();
                                txtCode.Value = ds.Tables[0].Rows[0]["Code"].ToString();
                                txtDBName.Value = ds.Tables[0].Rows[0]["DBName"].ToString();
                                txtUserName.Value = ds.Tables[0].Rows[0]["DBUserName"].ToString();
                                txtPassword.Value = ds.Tables[0].Rows[0]["DBPassword"].ToString();
                                txtPasswordConf.Value = ds.Tables[0].Rows[0]["DBPassword"].ToString();
                                txtEmailAdd.Value = ds.Tables[0].Rows[0]["EmailAddress"].ToString(); 
                            }
                        }
                    }
                    catch
                    {
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                //if (Request["mode"] == "clone")
                //{
                //    BtnCreate.Visible = true;
                //}
            }
        }
    }

    protected void Save(int compId)
    {
        try
        {
            bool flag1 = false;
            bool flag2 = false;

            string strName = iTool.formatInputString(txName.Value);
            string strCode = iTool.formatInputString(txtCode.Value);
            string strDBName = iTool.formatInputString(txtDBName.Value);
            string strDBUserName = (txtUserName.Value);

            string strPassword = iTool.formatInputString(txtPassword.Value);
            string strPasswordConf = iTool.formatInputString(txtPasswordConf.Value);
            string strEmailAdd = iTool.formatInputString(txtEmailAdd.Value);

            //bool active = true;
            //bool NullConnectionString = false;
            //if (!Status.Checked)
            //{
            //    active = false;
            //    NullConnectionString = true;
            //}

           

            SqlParameter[] ArParams = new SqlParameter[9];
            ArParams[0] = new SqlParameter("@Name", SqlDbType.VarChar, 50);
            ArParams[0].Value = strName;

            ArParams[1] = new SqlParameter("@Code", SqlDbType.VarChar, 50);
            ArParams[1].Value = strCode;

            ArParams[2] = new SqlParameter("@DBName", SqlDbType.VarChar, 50);
            ArParams[2].Value = strDBName;

            ArParams[3] = new SqlParameter("@DBUserName", SqlDbType.VarChar, 100);
            ArParams[3].Value = strDBUserName;

            ArParams[4] = new SqlParameter("@DBPassword", SqlDbType.VarChar,100);
            ArParams[4].Value = strPassword;

            ArParams[5] = new SqlParameter("@EmailAddress", SqlDbType.VarChar, 250);
            ArParams[5].Value = strEmailAdd;

           // ArParams[6] = new SqlParameter("@Active", SqlDbType.Bit);
          //  ArParams[6].Value = active;

            ArParams[7] = new SqlParameter("@CompanyID", SqlDbType.Int);
            //if (Request["comid"] != null && Request["comid"] != "")
            if (compId != null && compId > 0)
            {

              //  ArParams[7].Value = iTool.decryptString(compId.ToString());
                ArParams[7].Value = compId.ToString();
            }

           // ArParams[8] = new SqlParameter("@ConnectionString", SqlDbType.VarChar, 250);
            
            //if (!NullConnectionString)
            //{
            //    DBInstall target = new DBInstall();
            //    var builder = new SqlConnectionStringBuilder(ConfigurationManager.ConnectionStrings["Admin"].ConnectionString);
            //    string dbConnectionString = target.CreateConnectionString(false, builder.DataSource, strDBName, strDBUserName, strPassword, 5000);

            //    ArParams[8].Value = dbConnectionString;
            //}
          
            Dictionary<string, string> dict;


            using (SqlConnection conn = mConnection.GetAdminConnection())
            {
                conn.Open();

                try
                {


                   // if (Request["comid"] == null || Request["comid"] == "")
                    if (compId == null || compId <=0)
                    {
                        dict = null;
                        flag1 = Fn.CheckRecordExists(dict, "Company", "Code", "Code", "", "", "", ArParams, conn);
                        flag2 = Fn.CheckRecordExists(dict, "Company", "DBName", "DBName", "", "", "", ArParams, conn);
                    }
                    else
                    {
                        
                        flag1 = Fn.CheckRecordExists(null, "Company", "Code", "Code", "edit", "Id", compId.ToString(), ArParams, conn);
                        flag2 = Fn.CheckRecordExists(null, "Company", "DBName", "DBName", "edit", "Id", compId.ToString(), ArParams, conn);
                    }

                    if (flag1 == true)
                    {
                        LblCodeName.Text = "Code already exist. Try with another one.";
                        return;
                    }
                    if (strCode.Length> 6)
                    {
                        LblCodeName.Text = "Code should be less than 6 characters with alphanumeric characters only";
                        return;
                    }

                    if (flag2 == true)
                    {
                        LblDBName.Text = "Database Name already exist. Try with another one.";
                        return;
                    }

                    if (txtPassword.Value != "" && txtPasswordConf.Value != "")
                    {
                        if (txtPassword.Value != txtPasswordConf.Value)
                        {
                            LblPasswordConf.Text = "Password & Confirm Password should be the same.";
                            return;
                        }
                    }


                    if (txtPassword.Value.Length == 0 && compId == null)
                    {
                        ReqdPassword.IsValid = true;
                        ReqdPassword.Validate();
                        return;
                    }

                    if (txtPasswordConf.Value.Length == 0 && compId == null)
                    {
                        ReqdtxtPasswordConf.IsValid = false;
                        ReqdtxtPasswordConf.Validate();
                        return;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                using (SqlTransaction trans = conn.BeginTransaction())
                {
                   
                    try
                    {
                        // Call ExecuteNonQuery static method of SqlHelper class for debit and credit operations.
                        // We pass in SqlTransaction object, command type, stored procedure name, and a comma delimited list of SqlParameters

                        // Perform the debit operation
                        //SqlHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, "Debit", paramFromAcc, paramDebitAmount);
                        //SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sQuery, ArParams);
                       // SqlHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, "SP_Company_Update", ArParams);
                        DataSet ds = SqlHelper.ExecuteDataset(trans, CommandType.StoredProcedure, "SP_Company_Update", ArParams);
                        lblError.Text = "Company information added/updated sucessfully. \n Please create the database (if not created) and activate the company.";
                        BtnCreate.Visible = true;
                        BtnUpdate.Visible = true;
                        if (ds.Tables[0].Rows[0][0].ToString() == "" )
                            ViewState["comid"] = ds.Tables[0].Rows[0][1];
                        else
                        ViewState["comid"] = ds.Tables[0].Rows[0][0];
                            
                      //  Server.Transfer("AddCompany.aspx?comid=" + Request["comid"]);
                        //Response.Redirect("AddCompany.aspx?comid=" + Request["comid"]);
                        //txtResults.Text = " Completed";

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
                        trans.Commit();
                        conn.Close();
                    }
                }
            }
            //SqlHelper.ExecuteNonQuery(mConnection.GetConnection(), CommandType.Text, sQuery, ArParams);

        }
        catch (Exception ex)
        {
            lblError.Text = "Error in adding/updating company, please check data and try again";
            return;
            // CreateLogFiles.ErrorLog(ex.Message.ToString() & " - " & System.IO.Path.GetFileName(System.Web.HttpContext.Current.Request.Url.AbsolutePath) & " - btnCreate_Click")
            // lblError.Text = "Error - Please contact Administrator "
            // Exit Sub
        }
       
    }
    protected void BtnCreate_Click(object sender, EventArgs e)
    {
            if (ViewState["comid"] == "" || ViewState["comid"] == null || ViewState["comid"]=="{}")
            {
                if (Request["comid"] == null || Request["comid"] == "")
                {
                    lblError.Text = "Please select or create new company first.";
                    return;
                }
            }
        string companyName = string.Empty;
        string companyCode = string.Empty;
        string dbName = string.Empty;
        string dbUserName = string.Empty;
        string dbPassword = string.Empty;
        int companyId;
        try
        {
            if (ViewState["comid"] != null && ViewState["comid"] != "" && ViewState["comid"] != "{}")
            companyId = Convert.ToInt32((ViewState["comid"].ToString()));
            else
                companyId = Convert.ToInt32(iTool.decryptString(Request["comid"].ToString()));
        }
        catch (Exception ex)
        {
            lblError.Text = "Error in finding company, please try again.";
            return;
        }
        string connectionString = string.Empty;
        DBInstall target;
        using (SqlConnection conn = mConnection.GetAdminConnection())
        {
            try
            {
                conn.Open();
                Dictionary<string, string> dict = new Dictionary<string, string>() { { "companyId", companyId.ToString() } };
                ds = Fn.LoadAdminCompanies(dict, conn);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        companyName = ds.Tables[0].Rows[0]["Name"].ToString();
                        companyCode = ds.Tables[0].Rows[0]["Code"].ToString();
                        dbName = ds.Tables[0].Rows[0]["DBName"].ToString();
                        dbUserName = ds.Tables[0].Rows[0]["DBUserName"].ToString();
                        dbPassword = ds.Tables[0].Rows[0]["DBPassword"].ToString();
                    }
                }
            }
            catch
            {
                lblError.Text = "Error In Reading the Company Information, please try again. ";
                return;
            }
            finally
            {
                conn.Close();
                connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["LocalSqlServer1"].ToString();
                target = new DBInstall(); // TODO: Initialize to an appropriate value
            }
        }
        if (!string.IsNullOrEmpty(dbName))
        {
            string collation = "SQL_Latin1_General_CP1_CI_AS"; // TODO: Initialize to an appropriate value
            //string databaseName = dbName; // TODO: Initialize to an appropriate value
            try
            {
                target.CreateDatabase(connectionString, collation, dbName);
                target.CreateSchema(dbName, connectionString);
                target.CreateDBCredentails(connectionString, dbName, dbUserName, dbPassword);
                UpdateConnectionString(companyId, "", "Deactivate");
                lblError.Text = "Company database is created/upgraded sucessfully. please activate company.";
            }
            catch (Exception ex)
            {
                target.DropDatabase(connectionString, dbName);
               // lblError.Text = "Error in creating Database, please check the company information and try again.";
                lblError.Text = ex.ToString();
                return;
            }
           
        }

        else
        {
            lblError.Text = "Database Name is null, please check the company information.";
            return;
        }
    }

    private void UpdateConnectionString(int CompanyId, string dbConnectionString, string commandName)
    {
        try
        {
            SqlParameter[] ArParams = new SqlParameter[3];
            ArParams[0] = new SqlParameter("@CompanyID", SqlDbType.Int);
            ArParams[0].Value = CompanyId;
            ArParams[1] = new SqlParameter("@DBConnectionString", SqlDbType.NVarChar, 500);
            ArParams[1].Value = dbConnectionString;
            ArParams[2] = new SqlParameter("@CommandName", SqlDbType.NVarChar, 50);
            ArParams[2].Value = commandName;

            using (SqlConnection conn = mConnection.GetAdminConnection())
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        SqlHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, "SP_Company_Update_ConString", ArParams);
                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
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
        catch (Exception ex)
        {
            // CreateLogFiles.ErrorLog(ex.Message.ToString() & " - " & System.IO.Path.GetFileName(System.Web.HttpContext.Current.Request.Url.AbsolutePath) & " - btnCreate_Click")
            // lblError.Text = "Error - Please contact Administrator "
            // Exit Sub
        }
        //Response.Redirect("Company.aspx");
    }
    protected void BtnUpdate_Click(object sender, EventArgs e)
    {
        if (ViewState["comid"] == "" || ViewState["comid"] == null || ViewState["comid"] == "{}")
        {
            if (Request["comid"] == null || Request["comid"] == "")
            {
                lblError.Text = "Please select or create new company first.";
                return;
            }
        }
        int companyId = 0;
        if (ViewState["comid"] != null && ViewState["comid"] != "" && ViewState["comid"]!="{}")
            companyId = Convert.ToInt32((ViewState["comid"].ToString()));
        else
            companyId = Convert.ToInt32(iTool.decryptString(Request["comid"].ToString()));
        Save(Convert.ToInt32(companyId));
        
    }
    protected void BtnAdd_Click(object sender, EventArgs e)
    {
        Save(0);
        
    }
}