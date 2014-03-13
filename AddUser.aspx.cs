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

namespace PosUser
{
    public partial class AddUser : System.Web.UI.Page
    {
        DB mConnection = new DB() ;
        DataSet ds = new DataSet();
        Common Fn = new Common();
        MyToolSet iTool = new MyToolSet();
        SQLQuery Qry = new SQLQuery();
        public string strjoindate;
        //string sQuery = "";


        public AddUser()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["R_Initial"] != null && Session["R_Initial"] != "")
                RestInitial.Value = Session["R_Initial"].ToString(); 

            txtName.Attributes.Add("onkeyup", "assignusrname('" + txtName.ClientID + "','" + txtUserName.ClientID + "','" + RestInitial.ClientID + "')");

            if (!Page.IsPostBack)
            {
                if (Session["R_ID"] == "" || Session["R_ID"] == null)
                {
                    Session["bckurl"] = "AddUser.aspx";
                    //Response.Redirect("Select_Restaurants.aspx?bckurl=" + Session["bckurl"]);
                    Server.Transfer("Notification.aspx");
                    return;
                }

                using (SqlConnection conn = mConnection.GetConnection())
                {
                    conn.Open();

                    try
                    {                        
                        if (Request.QueryString["usrid"] != null)
                        {
                            string usrid = "";

                            //if (Request.QueryString["mode"] != null)
                            //    Mode.Value = "edit";

                            SqlParameter[] ArParams = new SqlParameter[13];

                            usrid = iTool.decryptString(Request.QueryString["usrid"]);

                            ArParams[0] = new SqlParameter("@UserID", SqlDbType.Int);
                            ArParams[0].Value = usrid;

                            // @UserName Output Parameter
                            ArParams[1] = new SqlParameter("@UserAlias", SqlDbType.VarChar, 50);
                            ArParams[1].Direction = ParameterDirection.Output;

                            // @Status Output Parameter
                            ArParams[2] = new SqlParameter("@UserName", SqlDbType.VarChar, 50);
                            ArParams[2].Direction = ParameterDirection.Output;

                            ArParams[3] = new SqlParameter("@UserPassword", SqlDbType.Binary, 50);
                            ArParams[3].Direction = ParameterDirection.Output;

                            ArParams[4] = new SqlParameter("@UserEmail", SqlDbType.VarChar, 100);
                            ArParams[4].Direction = ParameterDirection.Output;

                            // @Status Output Parameter
                            ArParams[5] = new SqlParameter("@Status", SqlDbType.Int);
                            ArParams[5].Direction = ParameterDirection.Output;

                            ArParams[6] = new SqlParameter("@UserGroupID", SqlDbType.Int);
                            ArParams[6].Direction = ParameterDirection.Output;

                            ArParams[7] = new SqlParameter("@UserPin", SqlDbType.Int);
                            ArParams[7].Direction = ParameterDirection.Output;

                            ArParams[8] = new SqlParameter("@FirstName", SqlDbType.VarChar, 50);
                            ArParams[8].Direction = ParameterDirection.Output;

                            ArParams[9] = new SqlParameter("@LastName", SqlDbType.VarChar,50);
                            ArParams[9].Direction = ParameterDirection.Output;

                            ArParams[10] = new SqlParameter("@UserPhone", SqlDbType.VarChar, 20);
                            ArParams[10].Direction = ParameterDirection.Output;

                            ArParams[11] = new SqlParameter("@StartDate", SqlDbType.DateTime);
                            ArParams[11].Direction = ParameterDirection.Output;

                            ArParams[12] = new SqlParameter("@HourlyRate", SqlDbType.Decimal);
                            ArParams[12].Direction = ParameterDirection.Output; 

                            // Call ExecuteNonQuery static method of SqlHelper class
                            // We pass in database connection string, command type, stored procedure name and an array of SqlParameter objects
                            SqlHelper.ExecuteNonQuery(conn, CommandType.StoredProcedure, "getUserDetails", ArParams);

                            // Display results in text box using the values of output parameters	

                            UTF8Encoding Encoding = new UTF8Encoding();
                            Decoder decoder = Encoding.GetDecoder();

                            byte[] pwd = System.Text.Encoding.Unicode.GetBytes(ArParams[3].Value.ToString());
                            string pwd1 = System.Text.Encoding.Unicode.GetString(pwd);

                            int count = decoder.GetCharCount(pwd, 0, pwd.Length);
                            char[] arr = new char[count];
                            decoder.GetChars(pwd, 0, pwd.Length, arr, 0);
                            string text = new string(arr);
                            txtPassword.Value = text;

                            txtName.Value = ArParams[1].Value.ToString();
                            txtUserName.Value = ArParams[2].Value.ToString();
                            //txtPassword.Attributes.Add("value", "parijat");
                            txtEmail.Value = ArParams[4].Value.ToString();
                            Status.Checked = ArParams[5].Value.ToString() == "1" ? true : false;
                            UserGroup.SelectedValue = ArParams[6].Value.ToString();
                            txtUserPin.Value = ArParams[7].Value.ToString();
                            txtFirstName.Value = ArParams[8].Value.ToString();
                            txtLastName.Value = ArParams[9].Value.ToString();
                            txtPhone.Value = ArParams[10].Value.ToString();
                            strjoindate = String.Format("{0:" + Session["DateFormat"] + "}", ArParams[11].Value);
                            txtHourlyRate.Value = String.Format("{0:0.00}", ArParams[12].Value);
                            //PostedOn.Value = String.Format("{0:" + Session["DateFormat"] + "}", ArParams[11].Value);

                            //  UserID.Value = usrid;

                            if (Request.QueryString["mode"] != null)
                            {
                                if (Request.QueryString["mode"] == "edit")
                                    Mode.Value = "edit";
                                else if (Request.QueryString["mode"] == "clone")
                                    Mode.Value = "clone";
                            }

                            //display name on top
                            string itemType = "User";
                            LblHead.Text = char.ToUpper(Mode.Value[0]) + Mode.Value.Substring(1) + " " + itemType + " [ " + ArParams[1].Value.ToString() + " ]";

                            if (Mode.Value == "add" || Mode.Value == "clone")
                                UserID.Value = "-1";
                            else
                                UserID.Value = usrid;


                        }
                    
                        Fn.PopulateDropDown_List(UserGroup, Qry.GetUserGroupSQL(), "UserGroupName", "UserGroupID", "",conn);

                        if (Request.QueryString["grpid"] != null)
                            UserGroup.SelectedValue = Request.QueryString["grpid"].ToString();

                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        conn.Close();
                    }

                }
            }
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                bool flag1 = false;
                bool flag2 = false;
                bool flag3 = false;

                string strName = iTool.formatInputString(txtName.Value);
                string strFirstName = iTool.formatInputString(txtFirstName.Value);
                string strLastName = iTool.formatInputString(txtLastName.Value);
                string strUserName = (txtUserName.Value);
                string strUserPhone = iTool.formatInputString(txtPhone.Value);
                string strUserPin  = iTool.formatInputString(txtUserPin.Value);
//                string strJoinDate = (datepicker.Value != "") ? datepicker.Value : "01/01/1900";
                string strJoinDate = (Request.Form["txtJoinDate"] != "") ? Request.Form["txtJoinDate"] : "01/01/1900";
                DateTime joinDt = DateTime.ParseExact(strJoinDate, Session["DateFormat"].ToString(), System.Globalization.CultureInfo.InvariantCulture);

                string strPassword = iTool.formatInputString(txtPassword.Value);
                string strPasswordConf = iTool.formatInputString(txtPasswordConf.Value);
                string strEmail = iTool.formatInputString(txtEmail.Value);
                string strHourlyRate = iTool.formatInputString(txtHourlyRate.Value);

                MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
                Byte[] hashedDataBytes;

                UTF8Encoding encoder = new UTF8Encoding();
                hashedDataBytes = md5Hasher.ComputeHash(encoder.GetBytes(strPassword));

                DateTime sDate = DateTime.Now;
                sDate = Fn.GetCommonDate(sDate, Session["DateFormat"]);

                SqlParameter[] ArParams = new SqlParameter[18];
                ArParams[0] = new SqlParameter("@UserAlias", SqlDbType.VarChar, 50);
                ArParams[0].Value = strName;

                ArParams[1] = new SqlParameter("@UserName", SqlDbType.VarChar, 50);
                ArParams[1].Value = strUserName;

                ArParams[3] = new SqlParameter("@PassCheck", SqlDbType.Char, 1);

                if (txtPassword.Value.Length == 0 && Mode.Value == "edit")
                {
                    ArParams[2] = new SqlParameter("@UserPassword", SqlDbType.Binary, 50);
                    ArParams[2].Value = null;
                    ArParams[3].Value = "0";
                }
                else
                {
                    ArParams[2] = new SqlParameter("@UserPassword", SqlDbType.Binary, 50);
                    ArParams[2].Value = hashedDataBytes;
                    ArParams[3].Value = "1";
                }

                ArParams[4] = new SqlParameter("@UserEmail", SqlDbType.VarChar, 100);
                ArParams[4].Value = strEmail;

                ArParams[5] = new SqlParameter("@Status", SqlDbType.Int);
                ArParams[5].Value = Status.Checked ? 1 : 0;

                ArParams[6] = new SqlParameter("@UserGroupID", SqlDbType.Int);
                ArParams[6].Value = UserGroup.SelectedValue;
                
                ArParams[7] = new SqlParameter("@sDate", SqlDbType.DateTime);
                ArParams[7].Value = sDate;

                ArParams[8] = new SqlParameter("@LoggedUserID", SqlDbType.Int);
                ArParams[8].Value = Convert.ToInt32(Session["UserID"]);

                ArParams[9] = new SqlParameter("@RestID", SqlDbType.Int);
                ArParams[9].Value = Session["R_ID"];

                ArParams[10] = new SqlParameter("@Mode", SqlDbType.VarChar,20);
                ArParams[10].Value = Mode.Value;

                ArParams[11] = new SqlParameter("@UserID", SqlDbType.Int);
                ArParams[11].Value = UserID.Value;

                ArParams[12] = new SqlParameter("@UserPin", SqlDbType.Int);
                ArParams[12].Value = strUserPin;

                ArParams[13] = new SqlParameter("@FirstName", SqlDbType.VarChar, 50);
                ArParams[13].Value = strFirstName;

                ArParams[14] = new SqlParameter("@LastName", SqlDbType.VarChar, 50);
                ArParams[14].Value = strLastName;

                ArParams[15] = new SqlParameter("@UserPhone", SqlDbType.VarChar, 20);
                ArParams[15].Value = strUserPhone;

                ArParams[16] = new SqlParameter("@StartDate", SqlDbType.DateTime);
                ArParams[16].Value = Fn.GetCommonDate(joinDt, Session["DateFormat"]);

                ArParams[17] = new SqlParameter("@HourlyRate", SqlDbType.Decimal);
                ArParams[17].Value = (strHourlyRate == "") ? 0 : Convert.ToDecimal(strHourlyRate);

                Dictionary<string, string> dict;


                using (SqlConnection conn = mConnection.GetConnection())
                {
                    conn.Open();

                    try
                    {
                        if (Fn.ValidateDate(strJoinDate) == false)
                        {
                            LblDate.Text = "Please Enter Proper Date";
                            return;
                        }

                        if (UserID.Value == "-1" && (Mode.Value == "add" || Mode.Value == "clone"))
                        {
                            dict = null;
                            flag1 = Fn.CheckRecordExists(dict, "omni_users", "UserName", "UserName", "", "", "", ArParams,conn);
                            flag2 = Fn.CheckRecordExists(dict, "omni_users", "UserEmail", "UserEmail", "", "", "", ArParams,conn);
                            flag3 = Fn.CheckRecordExists(dict, "omni_users", "UserPin", "UserPin", "", "", "", ArParams,conn);
                        }
                        else
                        {
                            //========= FOR IMPLEMENTING MORE "AND" CLAUSE IN SQL OPERATION ================//

                            //dict = new Dictionary<string, string>() { { "UserGroupId", GroupID.Value } };
                            //flag = Fn.CheckRecordExists(dict, "omni_user_group", "UserGroupName", "UserGroupName", "edit", "UserGroupId", GroupID.Value, ArParams);

                            //=============================================================================//

                            flag1 = Fn.CheckRecordExists(null, "omni_users", "UserName", "UserName", "edit", "UserID", UserID.Value, ArParams,conn);
                            flag2 = Fn.CheckRecordExists(null, "omni_users", "UserEmail", "UserEmail", "edit", "UserID", UserID.Value, ArParams,conn);
                            flag3 = Fn.CheckRecordExists(null, "omni_users", "UserPin", "UserPin", "edit", "UserID", UserID.Value, ArParams,conn);
                        }

                        if (flag1 == true)
                        {
                            LblUserName.Text = "UserName already exist. Try with another one.";
                            return;
                        }

                        if (flag2 == true)
                        {
                            LblEmail.Text = "Email already exist. Try with another one.";
                            return;
                        }

                        if (flag3 == true)
                        {
                            LblUserPin.Text = "User Pin is already assigned. Try with another one.";
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


                        if (txtPassword.Value.Length == 0 && Mode.Value == "add")
                        {
                            ReqdPassword.IsValid = true;
                            ReqdPassword.Validate();
                            return;
                        }

                        if (txtPasswordConf.Value.Length == 0 && Mode.Value == "add")
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
                            SqlHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, "SP_omni_user_Update", ArParams);

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
                Response.Redirect("Users.aspx");
        }

    }
}