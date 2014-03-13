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

namespace PosTaxInfo
{
    public partial class AddTaxInfo : System.Web.UI.Page
    {
        DB mConnection = new DB() ;
        DataSet ds = new DataSet();
        Common Fn = new Common();
        MyToolSet iTool = new MyToolSet();
        SQLQuery Qry = new SQLQuery();
        //string sQuery = "";


        public AddTaxInfo()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["R_Initial"] != null && Session["R_Initial"] != "")
                RestInitial.Value = Session["R_Initial"].ToString(); 


            if (!Page.IsPostBack)
            {
                if (Session["R_ID"] == "" || Session["R_ID"] == null)
                {
                    Session["bckurl"] = "AddTaxInfo.aspx";
                    Server.Transfer("Notification.aspx");
                    return;
                }

//                if (Fn.CheckRecordCount(null, "omni_Kitchen_Instruction", "", "") == false)
                    //Mode.Value = "add";
//                else
//                {
//                    Mode.Value = "edit";

                if (Request.QueryString["txid"] != null)
                {
                    string txid = "";

                    if (Request.QueryString["mode"] != null)
                        Mode.Value = "edit";

                    txid = iTool.decryptString(Request.QueryString["txid"]);

                    SqlParameter[] ArParams = new SqlParameter[4];

                    ArParams[0] = new SqlParameter("@TaxInfoID", SqlDbType.Int);
                    ArParams[0].Value = txid ;

                    ArParams[1] = new SqlParameter("@Literal", SqlDbType.VarChar, 100);
                    ArParams[1].Direction = ParameterDirection.Output;

                    ArParams[2] = new SqlParameter("@TaxRate", SqlDbType.Decimal);
                    ArParams[2].Direction = ParameterDirection.Output;
                    ArParams[2].Scale = 2;

                    ArParams[3] = new SqlParameter("@Status", SqlDbType.Int);
                    ArParams[3].Direction = ParameterDirection.Output;

                    try
                    {
                        // Call ExecuteNonQuery static method of SqlHelper class
                        // We pass in database connection string, command type, stored procedure name and an array of SqlParameter objects
                        SqlHelper.ExecuteNonQuery(mConnection.GetConnection(), CommandType.StoredProcedure, "getTaxInfo", ArParams);

                        // Display results in text box using the values of output parameters	

                        txtLiteral.Value =  ArParams[1].Value.ToString();
                        TaxRate.Value = ArParams[2].Value.ToString();
                        Status.Checked = ArParams[3].Value.ToString() == "1" ? true : false;
                        TaxInfoID.Value = txid;

                        //display name on top
                        string itemType = "Tax Rate";
                        LblHead.Text = char.ToUpper(Mode.Value[0]) + Mode.Value.Substring(1) + " " + itemType + " [ " + ArParams[1].Value.ToString() + " ]";
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

                string strLiteral = iTool.formatInputString(txtLiteral.Value);
                string strTaxRate = iTool.formatInputString(TaxRate.Value);

                DateTime sDate = DateTime.Now;
                sDate = Fn.GetCommonDate(sDate, Session["DateFormat"]);

                SqlParameter[] ArParams = new SqlParameter[8];
                ArParams[0] = new SqlParameter("@Literal", SqlDbType.VarChar,100);
                ArParams[0].Value = strLiteral;

                ArParams[1] = new SqlParameter("@TaxRate", SqlDbType.Decimal);
                ArParams[1].Value = strTaxRate;

                ArParams[2] = new SqlParameter("@Rest_ID", SqlDbType.Int);
                ArParams[2].Value = Session["R_ID"];

                ArParams[3] = new SqlParameter("@LoggedUserID", SqlDbType.Int);
                ArParams[3].Value = Convert.ToInt32(Session["UserID"]);

                ArParams[4] = new SqlParameter("@sDate", SqlDbType.DateTime);
                ArParams[4].Value = sDate;

                ArParams[5] = new SqlParameter("@Mode", SqlDbType.VarChar,20);
                ArParams[5].Value = Mode.Value;

                ArParams[6] = new SqlParameter("@TaxInfoID", SqlDbType.Int);
                ArParams[6].Value = TaxInfoID.Value;

                ArParams[7] = new SqlParameter("@Status", SqlDbType.Int);
                ArParams[7].Value = Status.Checked ? 1 : 0;

                Dictionary<string, string> dict;
                dict = new Dictionary<string, string>() { { "Rest_ID", Session["R_ID"].ToString() } };

                if (TaxInfoID.Value == "-1")
                    flag = Fn.CheckRecordExists(dict, "omni_TaxInfo", "TaxInfoLiteral", "Literal", "", "", "", ArParams);
                else
                    flag = Fn.CheckRecordExists(dict, "omni_TaxInfo", "TaxInfoLiteral", "Literal", "edit", "TaxInfoID", TaxInfoID.Value, ArParams);

                if (flag == true)
                {
                    LblLiteral.Text = "Literal Name already exist. Try with another one.";
                    return;
                }

                using (SqlConnection conn = mConnection.GetConnection())
                    {
                        conn.Open();

                        using (SqlTransaction trans = conn.BeginTransaction())
                        {
                            // Establish command parameters
                            // @AccountNo (From Account)

                            try
                            {
                                // Call ExecuteNonQuery static method of SqlHelper class for debit and credit operations.
                                // We pass in SqlTransaction object, command type, stored procedure name, and a comma delimited list of SqlParameters

                                // Perform the debit operation
                                SqlHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, "SP_omni_TaxInfo_Update", ArParams);

                                trans.Commit();
                                Panel.Visible = true;
                                //txtResults.Text = "Transfer Completed";

                            }

                            catch (Exception ex)
                            {
                                // throw exception						
                                trans.Rollback();
                                //txtResults.Text = "Transfer Error";
                                throw ex;
                            }

                            finally                            {
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
                Response.Redirect("TaxInfo.aspx");
        }

    }
}