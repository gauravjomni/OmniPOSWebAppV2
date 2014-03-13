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

namespace PosCompany
{
    public partial class CompanySettings : System.Web.UI.Page
    {
        DB mConnection = new DB() ;
        DataSet ds = new DataSet();
        Common Fn = new Common();
        MyToolSet iTool = new MyToolSet();
        SQLQuery Qry = new SQLQuery();
        //string sQuery = "";


        public CompanySettings()
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
/*                if (Session["R_ID"] == "" || Session["R_ID"] == null)
                {
                    Session["bckurl"] = "AddUser.aspx";
                    Server.Transfer("Select_Restaurants.aspx");
                    return;
                } */

                if (Fn.CheckRecordCount(null, "omni_CompanyInfo", "", "") == false)
                    Mode.Value = "add";
                else
                {
                    Mode.Value = "edit";

                    SqlParameter[] ArParams = new SqlParameter[9];

                    ArParams[0] = new SqlParameter("@CompanyName", SqlDbType.VarChar, 50);
                    ArParams[0].Direction = ParameterDirection.Output;

                    ArParams[1] = new SqlParameter("@Address", SqlDbType.VarChar, 2000);
                    ArParams[1].Direction = ParameterDirection.Output;

                    ArParams[2] = new SqlParameter("@Email", SqlDbType.VarChar, 100);
                    ArParams[2].Direction = ParameterDirection.Output;

                    ArParams[3] = new SqlParameter("@Phone", SqlDbType.VarChar, 50);
                    ArParams[3].Direction = ParameterDirection.Output;

                    ArParams[4] = new SqlParameter("@Fax", SqlDbType.VarChar, 50);
                    ArParams[4].Direction = ParameterDirection.Output;

                    ArParams[5] = new SqlParameter("@ABNNo", SqlDbType.VarChar, 25);
                    ArParams[5].Direction = ParameterDirection.Output;

                    ArParams[6] = new SqlParameter("@Tax", SqlDbType.Decimal,7);
                    ArParams[6].Direction = ParameterDirection.Output;
                    ArParams[6].Scale = 2;

                    ArParams[7] = new SqlParameter("@Currency", SqlDbType.VarChar, 5);
                    ArParams[7].Direction = ParameterDirection.Output;

                    ArParams[8] = new SqlParameter("@DateFormat", SqlDbType.VarChar, 20);
                    ArParams[8].Direction = ParameterDirection.Output;

                    try
                    {
                        // Call ExecuteNonQuery static method of SqlHelper class
                        // We pass in database connection string, command type, stored procedure name and an array of SqlParameter objects
                        SqlHelper.ExecuteNonQuery(mConnection.GetConnection(), CommandType.StoredProcedure, "getCompanyDetails", ArParams);

                        // Display results in text box using the values of output parameters	

                        txtCompanyName.Value = ArParams[0].Value.ToString();
                        txtAddress.Value = ArParams[1].Value.ToString();
                        txtEmail.Value = ArParams[2].Value.ToString();
                        txtPhone.Value = ArParams[3].Value.ToString();
                        txtFax.Value = ArParams[4].Value.ToString();
                        txtABNNo.Value = ArParams[5].Value.ToString();
                        txtRate.Value = ArParams[6].Value.ToString(); ;
                        txtCurrency.Value = ArParams[7].Value.ToString();
                        DDDtFmt.SelectedValue = ArParams[8].Value.ToString();

                        //display name on top
                        string itemType = "Company";
                        LblHead.Text = char.ToUpper(Mode.Value[0]) + Mode.Value.Substring(1) + " " + itemType + " [ " + ArParams[0].Value.ToString() + " ]";
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

                string strCompName = iTool.formatInputString(txtCompanyName.Value);
                string strAddress = iTool.formatInputString(txtAddress.Value);
                string strEmail = iTool.formatInputString(txtEmail.Value);

                string strPhone = iTool.formatInputString(txtPhone.Value);
                string strFax = iTool.formatInputString(txtFax.Value);
                string strABNNo = iTool.formatInputString(txtABNNo.Value);
                string strTax = iTool.formatInputString(txtRate.Value);
                string strCurrency = iTool.formatInputString(txtCurrency.Value);
                string strDateFormat = DDDtFmt.SelectedValue;

                DateTime sDate = DateTime.Now;
                sDate = Fn.GetCommonDate(sDate, Session["DateFormat"]);

                SqlParameter[] ArParams = new SqlParameter[12];
                ArParams[0] = new SqlParameter("@CompanyName", SqlDbType.VarChar, 50);
                ArParams[0].Value = strCompName;

                ArParams[1] = new SqlParameter("@Address", SqlDbType.VarChar, 2000);
                ArParams[1].Value =  strAddress;

                ArParams[2] = new SqlParameter("@Email", SqlDbType.VarChar, 100);
                ArParams[2].Value = strEmail;

                ArParams[3] = new SqlParameter("@Phone", SqlDbType.VarChar, 50);
                ArParams[3].Value = strPhone;

                ArParams[4] = new SqlParameter("@Fax", SqlDbType.VarChar, 50);
                ArParams[4].Value =  strFax;

                ArParams[5] = new SqlParameter("@ABNNo", SqlDbType.VarChar, 25);
                ArParams[5].Value =   strABNNo;

                ArParams[6] = new SqlParameter("@Tax", SqlDbType.Decimal);
                ArParams[6].Value = Convert.ToDecimal(strTax);

                ArParams[7] = new SqlParameter("@Currency", SqlDbType.VarChar,5);
                ArParams[7].Value = strCurrency;

                ArParams[8] = new SqlParameter("@DateFormat", SqlDbType.VarChar,20);
                ArParams[8].Value = strDateFormat;

                ArParams[9] = new SqlParameter("@sDate", SqlDbType.DateTime);
                ArParams[9].Value = sDate;

                ArParams[10] = new SqlParameter("@LoggedUserID", SqlDbType.Int);
                ArParams[10].Value = Convert.ToInt32(Session["UserID"]);

                //ArParams[9] = new SqlParameter("@RestID", SqlDbType.Int);
                //ArParams[9].Value = Session["R_ID"];

                ArParams[11] = new SqlParameter("@Mode", SqlDbType.VarChar,20);
                ArParams[11].Value = Mode.Value;


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
                                SqlHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, "SP_omni_Company_Update", ArParams);

                                trans.Commit();
                                //txtResults.Text = "Transfer Completed";
                                Session["Currency"] = strCurrency;

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
                Response.Redirect("Home.aspx");
        }

    }
}