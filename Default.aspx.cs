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

/// <summary>
/// Summary description for Login
/// </summary>
namespace Login
{
    partial class Default : System.Web.UI.Page
    {
        SQLQuery Qry = new SQLQuery();
        DB mConnection = new DB();
        Common Fn = new Common();

        public Default()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        protected void Page_Load(object sender, System.EventArgs e) 
        {
            if (!IsPostBack)
            {
                //using (SqlConnection conn = mConnection.GetConnection())
                //{
                //    conn.Open();

                //    try
                //    {
                //        //Fn.PopulateDropDown_List(UserGroup, Qry.GetUserGroupSQL(), "UserGroupName", "UserGroupID", "");
                //        Fn.PopulateDropDown_List(UserGroup, Qry.GetUserGroupSQL(), "UserGroupName", "UserGroupID", "", conn);
                //    }
                //    catch (Exception ex)
                //    {
                //        throw ex;
                //    }
                //    finally
                //    {
                //        conn.Close();
                //    }
                //}
                //mConnection.PopulateDropDown_List(UserGroup, Qry.GetUserGroupSQL(), "UserGroupName", "UserGroupID", "");
            }
        }


        protected void CMDLogin_Click(object sender, EventArgs e)
        {
            MyToolSet iTool = new MyToolSet();
            DB db = new DB();

            string user_id = "";
            string user_name = "";
            string user_groupid = "";
            string sQuery = "";
           
            string uname = iTool.formatInputString(txtUserName.Value);
            string pwd = iTool.formatInputString(txtUserPass.Value);


            string[] arrUserName = uname.Split(new [] {'_'},2);
            string strCompanyCode = string.Empty;
            string strUserName = string.Empty;
            if (arrUserName.Count() > 1)
            {
                strCompanyCode = arrUserName[0];
                strUserName = arrUserName[1];
                Session["CompanyCode"] = strCompanyCode;
                // yourString.Split(new []{'-'},2).Select(s => s.Trim())
            }
            else
            {

                lblMsg.Text = "!Invalid UserName / Password ";
                return;
            
            }
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            Byte[] hashedDataBytes;
            UTF8Encoding encoder = new UTF8Encoding();
            hashedDataBytes = md5Hasher.ComputeHash(encoder.GetBytes(pwd));

            user_groupid = "1";//UserGroup.SelectedValue;

            Dictionary<string, string> dict = new Dictionary<string, string>() { { "UserGroupID", user_groupid } };

            SqlParameter[] arParms = new SqlParameter[3];

            // @UsrName Input Parameter 
            arParms[0] = new SqlParameter("@usrname", SqlDbType.VarChar, 25);
           // arParms[0].Value = uname;
            arParms[0].Value = strUserName;

            // @Password Input Parameter
            arParms[1] = new SqlParameter("@usrpwd", SqlDbType.Binary, 50);
            arParms[1].Value = hashedDataBytes;

            // @usertype Input Parameter
            arParms[2] = new SqlParameter("@UserGroupID", SqlDbType.Int);
            arParms[2].Value = user_groupid;



            using (SqlConnection conn = mConnection.GetConnection())
            {
                if (conn.ConnectionString == "" || conn.ConnectionString == null)
                {
                    lblMsg.Text = "Your company is not active, Please contact Omni";
                    return;
                }
                conn.Open();

                try
                {
                    SqlDataReader oReader = SqlHelper.ExecuteReader(conn, CommandType.Text, Qry.LoginCredentialSQL(dict), arParms);

                    //while (oReader.Read())
                    if (oReader.Read())
                    {

                        var objUser = new User
                        {
                            UserId = Convert.ToString(oReader["UserID"]).GetInt(),
                            UserName = Convert.ToString(oReader["UserName"]),
                            UserGroupID = Convert.ToString(oReader["UserGroupID"]).GetInt(),
                            FirstName = Convert.ToString(oReader["FirstName"]),
                            LastName = Convert.ToString(oReader["LastName"])
                        };

                        ManageSession.User = objUser;

                        user_id = oReader["UserID"].ToString();
                        user_name = oReader["UserName"].ToString();
                        Session["UserGroupID"] = oReader["UserGroupID"];
                        Session["UserID"] = oReader["UserID"];
                        Session["UserName"] = oReader["UserName"];

                        //Session["DateFormat"] = "dd/MM/yyyy";
                        Session["DateFormat"] = "MM/dd/yyyy";
                        Session["DateFormatSQL"] = "103";
                        //Server.Transfer("Home.aspx");
                    }
                    else
                    {
                        lblMsg.Text = "!Invalid UserName / Password ";
                        oReader.Close();
                        return;
                    }
                    oReader.Close();

                    SqlDataReader CompanyRset = SqlHelper.ExecuteReader(conn, CommandType.Text, Qry.GetCompanySQL());

                    if (CompanyRset.Read())
                    {
                        Session["Currency"] = CompanyRset["CurrencySymbol"].ToString();
                    }
                    CompanyRset.Close();


                    if (Session["UserGroupID"].ToString() == "1")
                        Response.Redirect("Select_Restaurants.aspx",false);
                    else
                    {
                        SqlDataReader UserRest = SqlHelper.ExecuteReader(conn, CommandType.Text, Qry.GetRestaurantSQL("UserID", Session["UserID"].ToString()));

                        if (UserRest.Read())
                        {
                            ManageSession.User.R_ID = UserRest["Rest_id"].ToString().GetInt();
                            ManageSession.User.R_Initial = (string)UserRest["Initials"].ToString();
                            ManageSession.User.R_Name = (string)UserRest["RestName"].ToString();


                            Session["R_ID"] = UserRest["Rest_id"].ToString();
                            Session["R_Initial"] = (string)UserRest["Initials"].ToString();
                            Session["R_Name"] = (string)UserRest["RestName"].ToString();
                        }
                        UserRest.Close();

                        Response.Redirect("Home.aspx");
                    }

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


}