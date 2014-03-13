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

public partial class Admin_Company : System.Web.UI.Page
{
    DB mConnection = new DB();
    DataSet ds = new DataSet();
    Common Fn = new Common();
    protected MyToolSet iTool = new MyToolSet();

    protected void Page_Load(object sender, EventArgs e)
    {


        /*mConnection = new DB();
        ds = Fn.LoadUsers(null, "Rest_ID", Session["R_ID"].ToString());
        UserRepeater.DataSource = ds;
        UserRepeater.DataBind();*/
        string strusrid = string.Empty;


        if (Session["AdminUserID"] == "" || Session["AdminUserID"] == null)
        {
            Session["bckurl"] = "Company.aspx";
            //Response.Redirect("Select_Restaurants.aspx?bckurl=" + Session["bckurl"]);
            Server.Transfer("OmniLogin.aspx");
            return;
        }
        else
        {
            Header_LoggedUserName.Text = Session["AdminUserID"].ToString();
        }

        if (!IsPostBack)
        {
            bindCompany();
        }

    }

    private void bindCompany()
    {
        using (SqlConnection conn = mConnection.GetAdminConnection())
        {
            conn.Open();

            try
            {
                ds = Fn.LoadAdminCompanies(null, conn);
                UserRepeater.DataSource = ds;
                UserRepeater.DataBind();
            }
            catch (Exception ex)
            {
                /*trans.Rollback();*/
                throw ex;
            }

            finally
            {
                conn.Close();
            }

        }
    }


    private string UpdateConnectionString(int CompanyId, string commandName)
    {
        SqlConnection conn;
        try
        {
            conn = mConnection.GetAdminConnection();
            string companyName = string.Empty;
            string companyCode = string.Empty;
            string dbName = string.Empty;
            string dbUserName = string.Empty;
            string dbPassword = string.Empty;
            string dbConnectionString;
            string EmailAddress = string.Empty;

           // string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["LocalSqlServer1"].ToString();
            DBInstall target = new DBInstall(); // TODO: Initialize to an appropriate value
            var builder = new SqlConnectionStringBuilder(conn.ConnectionString);
            using (conn)
            {
                conn.Open();
                Dictionary<string, string> dict = new Dictionary<string, string>() { { "companyId", CompanyId.ToString() } };
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
                        EmailAddress = ds.Tables[0].Rows[0]["EmailAddress"].ToString();
                        dbConnectionString = target.CreateConnectionString(false, builder.DataSource, dbName, dbUserName, dbPassword, 5000);

                        SqlParameter[] ArParams = new SqlParameter[3];
                        ArParams[0] = new SqlParameter("@CompanyID", SqlDbType.Int);
                        ArParams[0].Value = CompanyId;
                        ArParams[1] = new SqlParameter("@DBConnectionString", SqlDbType.NVarChar, 500);
                        ArParams[1].Value = dbConnectionString;
                        ArParams[2] = new SqlParameter("@CommandName", SqlDbType.NVarChar, 50);
                        ArParams[2].Value = commandName;
                        if (SqlHelper.ExecuteNonQuery(conn, CommandType.StoredProcedure, "SP_Company_Update_ConString", ArParams) == -1)
                        {
                            return "Error in Activation/Deactivation, database does not exist. Error code 'SPCUCS' ";
                        }
                        else
                        {
                            if (EmailAddress == null || EmailAddress == "")
                            {
                                return "Company is activated, but no email address exist for the company. Please inform company through other media.";
                            }
                            string msg = SendEmail(EmailAddress);
                            if (msg == "")
                            {
                                return msg;
                            }
                            else
                            {
                                return "Company is activated, error in sending email." +msg;
                            }
                        }
                    }
                    return "Error in Activation/Deactivation- Unable to find company information.";
                }
                return "Error in Activation/Deactivation- Unable to find company information.";
            }
            //return "Error - Unable to make conection to admin database";
        }

        catch (Exception ex)
        {
            // CreateLogFiles.ErrorLog(ex.Message.ToString() & " - " & System.IO.Path.GetFileName(System.Web.HttpContext.Current.Request.Url.AbsolutePath) & " - btnCreate_Click")
            // lblError.Text = "Error - Please contact Administrator "
            // Exit Sub
            return "Error in Activation/Deactivation- Unknown";
        }
       
        conn.Close();
        



        //Response.Redirect("Company.aspx");
    }
    protected void UserRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
            string msg = UpdateConnectionString(Convert.ToInt32(e.CommandArgument), e.CommandName);
            if ( msg == "")
            {
                bindCompany();

               // lblError.Text = "Company is activated, Issue in sending email.";

            }
            else
            {
                lblError.Text = msg;
                lblError.Visible = true;
            }
    }
    private string SendEmail(string pTo)
    {
        try
        {
            string pSubject;


            pSubject = "Welcome to OmniPos Web";

            string mBody = "";
            mBody = "<div style=\"FONT-FAMILY:Arial; \">";

            mBody += "Welcome to OmniPos Web<br/><br/> Please login to <a href='http://omniposwe.com' OmniPos with following credentials'";

            mBody += "<br/><br/>Your login Id: Admin <br/>Password: 0mn1p0s";
            mBody += "<br/><br/>Please change your password for security reasons.";

            mBody += "<br/><br/><br/><b>Regards,</b><br/>";
            mBody += "<b>Omni Systems</b><br/></div>";

            MailTools.EmailManager sm = new MailTools.EmailManager(pTo, mBody, pSubject);

            //sm.sendEmail();
            sm.sendMail();
            return "";
        }
        catch (Exception ex)
        {
            return ex.ToString();
        }
    }
}