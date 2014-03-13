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

namespace PosApp
{
    public partial class AddSupplier : System.Web.UI.Page
    {
        DB mConnection = new DB() ;
        DataSet ds = new DataSet();
        Common Fn = new Common();
        MyToolSet iTool = new MyToolSet();
        SQLQuery Qry = new SQLQuery();
        public string strjoindate;
        //string sQuery = "";


        public AddSupplier()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            Fn.switchingbeteenlocation2company(true);

            if (Session["R_Initial"] != null && Session["R_Initial"] != "")
                RestInitial.Value = Session["R_Initial"].ToString(); 

            if (!Page.IsPostBack)
            {
                /*if (Session["R_ID"] == "" || Session["R_ID"] == null)
                {
                    Session["bckurl"] = "AddCustomer.aspx";
                    //Response.Redirect("Select_Restaurants.aspx?bckurl=" + Session["bckurl"]);
                    Server.Transfer("Notification.aspx");
                    return;
                }*/

                if (Request.QueryString["spid"] != null)
                {
                    string suppid = "";

                    //if (Request.QueryString["mode"] != null)
                    //    Mode.Value = "edit";

                    SqlParameter[] ArParams = new SqlParameter[9];

                    suppid = iTool.decryptString(Request.QueryString["spid"]);

                    ArParams[0] = new SqlParameter("@SuppID", SqlDbType.Int);
                    ArParams[0].Value =suppid;

                    // @UserName Output Parameter
                    ArParams[1] = new SqlParameter("@FirstName", SqlDbType.VarChar, 50);
                    ArParams[1].Direction = ParameterDirection.Output;

                    // @Status Output Parameter
                    ArParams[2] = new SqlParameter("@LastName", SqlDbType.VarChar, 50);
                    ArParams[2].Direction = ParameterDirection.Output;

                    ArParams[3] = new SqlParameter("@Email", SqlDbType.VarChar, 100);
                    ArParams[3].Direction = ParameterDirection.Output;

                    ArParams[4] = new SqlParameter("@Phone", SqlDbType.VarChar, 20);
                    ArParams[4].Direction = ParameterDirection.Output;

                    // @Status Output Parameter
                    ArParams[5] = new SqlParameter("@Address1", SqlDbType.VarChar,255);
                    ArParams[5].Direction = ParameterDirection.Output;

                    ArParams[6] = new SqlParameter("@Address2", SqlDbType.VarChar,255);
                    ArParams[6].Direction = ParameterDirection.Output;

                    ArParams[7] = new SqlParameter("@ZipCode", SqlDbType.VarChar ,25);
                    ArParams[7].Direction = ParameterDirection.Output;

                    ArParams[8] = new SqlParameter("@Status", SqlDbType.Int);
                    ArParams[8].Direction = ParameterDirection.Output;

                    try
                    {
                        // Call ExecuteNonQuery static method of SqlHelper class
                        // We pass in database connection string, command type, stored procedure name and an array of SqlParameter objects
                        SqlHelper.ExecuteNonQuery(mConnection.GetConnection(), CommandType.StoredProcedure, "getSupplierDetails", ArParams);

                        // Display results in text box using the values of output parameters	

                        txtFirstName.Value =  ArParams[1].Value.ToString();
                        txtLastName.Value = ArParams[2].Value.ToString();
                        txtEmail.Value = ArParams[3].Value.ToString();
                        txtPhone.Value = ArParams[4].Value.ToString();
                        txtAddress1.Value = ArParams[5].Value.ToString();
                        txtAddress2.Value = ArParams[6].Value.ToString();
                        txtZipCode.Value = ArParams[7].Value.ToString();
                        Status.Checked = ArParams[8].Value.ToString() == "1" ? true : false;

                        if (Request.QueryString["mode"] != null)
                        {
                            if (Request.QueryString["mode"] == "edit")
                                Mode.Value = "edit";
                            else if (Request.QueryString["mode"] == "clone")
                                Mode.Value = "clone";
                        }

                        //display name on top
                        string itemType = "Supplier";
                        LblHead.Text = char.ToUpper(Mode.Value[0]) + Mode.Value.Substring(1) + " " + itemType + " [ " + ArParams[1].Value.ToString() + " ]";

                        if (Mode.Value == "add" || Mode.Value == "clone")
                            SuppID.Value = "-1";
                        else
                            SuppID.Value = suppid;
                    }
                    catch (Exception ex)
                    {
                        // throw an exception
                        throw ex;
                    }

                }

            }
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                bool flag = false;

                string strFirstName = iTool.formatInputString(txtFirstName.Value);
                string strLastName = iTool.formatInputString(txtLastName.Value);
                string strEmail = iTool.formatInputString(txtEmail.Value);
                string strPhone = iTool.formatInputString(txtPhone.Value);
                string strAddress1 = iTool.formatInputString(txtAddress1.Value);
                string strAddress2 = iTool.formatInputString(txtAddress2.Value);
                string strZipCode = iTool.formatInputString(txtZipCode.Value);

                DateTime sDate = DateTime.Now;
                sDate = Fn.GetCommonDate(sDate, Session["DateFormat"]);

                SqlParameter[] ArParams = new SqlParameter[13];
                ArParams[0] = new SqlParameter("@FirstName", SqlDbType.VarChar, 50);
                ArParams[0].Value = strFirstName;

                ArParams[1] = new SqlParameter("@LastName", SqlDbType.VarChar, 50);
                ArParams[1].Value = strLastName;

                ArParams[2] = new SqlParameter("@Email", SqlDbType.VarChar, 100);
                ArParams[2].Value =  strEmail;

                ArParams[3] = new SqlParameter("@Phone", SqlDbType.VarChar, 20);
                ArParams[3].Value = strPhone;

                ArParams[4] = new SqlParameter("@Address1", SqlDbType.VarChar, 255);
                ArParams[4].Value = strAddress1;

                ArParams[5] = new SqlParameter("@Address2", SqlDbType.VarChar, 255);
                ArParams[5].Value = strAddress2;

                ArParams[6] = new SqlParameter("@ZipCode", SqlDbType.VarChar, 25);
                ArParams[6].Value = strZipCode;

                ArParams[7] = new SqlParameter("@Status", SqlDbType.Int);
                ArParams[7].Value = Status.Checked ? 1 : 0;

                ArParams[8] = new SqlParameter("@sDate", SqlDbType.DateTime);
                ArParams[8].Value = sDate;

                ArParams[9] = new SqlParameter("@LoggedUserID", SqlDbType.Int);
                ArParams[9].Value = Convert.ToInt32(Session["UserID"]);

                ArParams[10] = new SqlParameter("@RestID", SqlDbType.Int);
                ArParams[10].Value = 0;

                ArParams[11] = new SqlParameter("@Mode", SqlDbType.VarChar,20);
                ArParams[11].Value = Mode.Value;

                ArParams[12] = new SqlParameter("@SuppID", SqlDbType.Int);
                ArParams[12].Value = SuppID.Value;

                Dictionary<string, string> dict;
                dict = null;    // new Dictionary<string, string>() { { "Rest_ID", Session["R_ID"].ToString() } };

                if (SuppID.Value == "-1" && (Mode.Value=="add" || Mode.Value == "clone"))
                    flag = Fn.CheckRecordExists(dict, "omni_Suppliers", "Email", "Email", "", "", "", ArParams);
                else
                    flag = Fn.CheckRecordExists(dict, "omni_Suppliers", "Email", "Email", "edit", "SupplierID", SuppID.Value, ArParams);

                if (flag == true)
                {
                    LblEmail.Text = "Email already exist. Try with another one.";
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
                            SqlHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, "SP_omni_Supplier_Update", ArParams);

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
                Response.Redirect("ViewSupplier.aspx");
        }

    }
}