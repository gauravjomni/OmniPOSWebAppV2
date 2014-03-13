using System;
using System.Data;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Security.Cryptography;
using System.Globalization;
using System.Text;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using MyTool;
using MyDB;
using MyQuery;
using Commons;
using MailTools;

public partial class Admin_OmniLogin : System.Web.UI.Page
{
    SQLQuery Qry = new SQLQuery();
    DB mConnection = new DB();
    Common Fn = new Common();

    protected void Page_Load(object sender, System.EventArgs e)
    {

        if (!IsPostBack)
        {
            //mConnection.PopulateDropDown_List(UserGroup, Qry.GetUserGroupSQL(), "UserGroupName", "UserGroupID", "");
        }
    }


    protected void CMDLogin_Click(object sender, EventArgs e)
    {
        MyToolSet iTool = new MyToolSet();
        DB db = new DB();

        string user_id = "";
        string user_name = "";
        string sQuery = "";

        string uname = iTool.formatInputString(txtUserName.Value);
        string pwd = iTool.formatInputString(txtUserPass.Value);


        Dictionary<string, string> dict = new Dictionary<string, string>() { };

        SqlParameter[] arParms = new SqlParameter[3];

        // @UsrName Input Parameter 
        arParms[0] = new SqlParameter("@usrname", SqlDbType.VarChar, 25);
        arParms[0].Value = uname;

        // @Password Input Parameter
        arParms[1] = new SqlParameter("@usrpwd", SqlDbType.VarChar, 50);
        arParms[1].Value = pwd;

        using (SqlConnection conn = mConnection.GetAdminConnection())
        {
            conn.Open();

            try
            {
                SqlDataReader oReader = SqlHelper.ExecuteReader(conn, CommandType.Text, Qry.AdminLoginCredentialSQL(dict), arParms);

                //while (oReader.Read())
                if (oReader.Read())
                {
                    user_id = oReader["UserID"].ToString();

                    Session["AdminUserID"] = oReader["UserID"];
                   

                }
                else
                {
                    lblMsg.Text = "!Invalid UserName / Password / User Group";
                    oReader.Close();
                    return;
                }
                oReader.Close();
                Response.Redirect("Company.aspx");
            }
            catch (Exception ex)
            {
                // throw an exception
                throw ex;
            }

            finally
            {
                conn.Close();
            }

        }

    }
}