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

namespace PosPrinters
{
    public partial class AddPrinter : System.Web.UI.Page
    {
        DB mConnection = new DB() ;
        DataSet ds = new DataSet();
        Common Fn = new Common();
        MyToolSet iTool = new MyToolSet();
        string sQuery = "";


        public AddPrinter()
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
                    Session["bckurl"] = "Courses.aspx";
                    //Response.Redirect("Select_Restaurants.aspx?bckurl=" + Session["bckurl"]);
                    Server.Transfer("Notification.aspx");
                    return;
                }


                if (Request.QueryString["prnid"] != null)
                {
                    string prnid = "";
                    SqlParameter[] ArParams = new SqlParameter[9];

                    prnid = iTool.decryptString(Request.QueryString["prnid"]);

                    ArParams[0] = new SqlParameter("@PrinterID", SqlDbType.Int);
                    ArParams[0].Value = prnid;

                    // @UserGroupName Output Parameter
                    ArParams[1] = new SqlParameter("@PrinterName", SqlDbType.VarChar, 50);
                    ArParams[1].Direction = ParameterDirection.Output;

                    ArParams[2] = new SqlParameter("@IPAddress", SqlDbType.VarChar, 20);
                    ArParams[2].Direction = ParameterDirection.Output;

                    ArParams[3] = new SqlParameter("@PosOrItem", SqlDbType.Char, 1);
                    ArParams[3].Direction = ParameterDirection.Output;

                    ArParams[4] = new SqlParameter("@IsPrintIPAddress", SqlDbType.Char, 1);
                    ArParams[4].Direction = ParameterDirection.Output;

                    // @Status Output Parameter
                    ArParams[5] = new SqlParameter("@Status", SqlDbType.Int);
                    ArParams[5].Direction = ParameterDirection.Output;

                    ArParams[6] = new SqlParameter("@PrinterType", SqlDbType.Char,1);
                    ArParams[6].Direction = ParameterDirection.Output;

                    ArParams[7] = new SqlParameter("@NoOfCopies", SqlDbType.Int);
                    ArParams[7].Direction = ParameterDirection.Output;

                    ArParams[8] = new SqlParameter("@Trigger_Cash_Drawer", SqlDbType.TinyInt);
                    ArParams[8].Direction = ParameterDirection.Output;

                    try
                    {
                        // Call ExecuteNonQuery static method of SqlHelper class
                        // We pass in database connection string, command type, stored procedure name and an array of SqlParameter objects
                        SqlHelper.ExecuteNonQuery(mConnection.GetConnection(), CommandType.StoredProcedure, "getPrinterDetails", ArParams);

                        // Display results in text box using the values of output parameters	
                        txtPrinterName.Value = ArParams[1].Value.ToString();
                        txtIP.Value = ArParams[2].Value.ToString();
                        PosItem.SelectedValue = ArParams[3].Value.ToString();
                        ChkIPAddess.Checked = ArParams[4].Value.ToString() == "1" ? true : false;
                        Status.Checked = ArParams[5].Value.ToString() == "1" ? true : false;
                        PrinterType.SelectedValue = ArParams[6].Value.ToString();
                        txtCopies.Value = ArParams[7].Value.ToString();
                        ChkTCD.Checked = ArParams[8].Value.ToString() == "1" ? true : false;
                        //Mode.Value = "edit";
                        //PrinterID.Value  = prnid;

                        if (Request.QueryString["mode"] != null)
                        {
                            if (Request.QueryString["mode"] == "edit")
                                Mode.Value = "edit";
                            else if (Request.QueryString["mode"] == "clone")
                                Mode.Value = "clone";
                        }

                        //display name on top
                        string itemType = "Printer";
                        LblHead.Text = char.ToUpper(Mode.Value[0]) + Mode.Value.Substring(1) + " " + itemType + " [ " + ArParams[1].Value.ToString() + " ]";

                        if (Mode.Value == "add" || Mode.Value == "clone")
                            PrinterID.Value = "-1";
                        else
                            PrinterID.Value = prnid;


                        if (Mode.Value == "edit")
                        {
                            if (PrinterType.SelectedValue == "D")
                            {
                                txtCopies.Visible = true;
                                LBLcopies.Visible = true;
                                Copies.Visible = true;
                                
                            }
                            else if (PrinterType.SelectedValue == "P")
                            {
                                //txtCopies.Visible = false;
                                //LBLcopies.Visible = false;
                                LBLTCD.Visible = true;
                                ChkTCD.Visible = true;
                                TCD.Visible = true;
                            }
                        }
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

            PrinterType.Attributes.Add("onclick", "showhide('" + PrinterType.ClientID + "','" + txtCopies.ClientID + "','" + ChkTCD.ClientID + "','" + Copies.ClientID + "','" + LBLcopies.ClientID + "','" + TCD.ClientID  + "','" + LBLTCD.ClientID + "','" + Mode.Value + "')");
            Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "hide('" + Mode.Value + "','" + Copies.ClientID + "','" + TCD.ClientID + "','" + PrinterType.ClientID + "')", true);

            if (Mode.Value == "add" || Mode.Value == "clone")
                txtCopies.Value = System.Configuration.ConfigurationSettings.AppSettings["PrinterCopies"];
        }


        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string strPrinterName = iTool.formatInputString(txtPrinterName.Value);
                string strIPAddress = iTool.formatInputString(txtIP.Value);
                string strNoOfCopies = iTool.formatInputString(txtCopies.Value);

                if (PrinterType.SelectedValue == "P")
                    strNoOfCopies = System.Configuration.ConfigurationSettings.AppSettings["PrinterCopies"];

                if (PrinterType.SelectedValue == "D")
                    ChkTCD.Checked = false;

                bool flag1, flag2;

                DateTime sDate = DateTime.Now;
                sDate = Fn.GetCommonDate(sDate, Session["DateFormat"]);

                SqlParameter[] ArParams = new SqlParameter[13];
                ArParams[0] = new SqlParameter("@PrinterName", SqlDbType.VarChar, 50);
                ArParams[0].Value = strPrinterName;

                ArParams[1] = new SqlParameter("@IPAddress", SqlDbType.VarChar, 20);
                ArParams[1].Value = strIPAddress;

                ArParams[2] = new SqlParameter("@PosOrItem", SqlDbType.Char, 1);
                ArParams[2].Value = PosItem.SelectedValue;

                ArParams[3] = new SqlParameter("@IsPrintIPAddress", SqlDbType.Char, 1);
                ArParams[3].Value = ChkIPAddess.Checked ? '1' : '2';

                ArParams[4] = new SqlParameter("@Status", SqlDbType.Int);
                ArParams[4].Value = Status.Checked ? 1 : 0;

                ArParams[5] = new SqlParameter("@sDate", SqlDbType.DateTime);
                ArParams[5].Value = sDate;

                ArParams[6] = new SqlParameter("@Rest_ID", SqlDbType.Int);
                ArParams[6].Value = Session["R_ID"];

                ArParams[7] = new SqlParameter("@LoggedUserID", SqlDbType.Int);
                ArParams[7].Value = Session["UserID"];

                ArParams[8] = new SqlParameter("@Mode", SqlDbType.VarChar,20);
                ArParams[8].Value = Mode.Value;

                ArParams[9] = new SqlParameter("@PrinterID", SqlDbType.Int);
                ArParams[9].Value = PrinterID.Value;

                ArParams[10] = new SqlParameter("@PrinterType", SqlDbType.Char);
                ArParams[10].Value = PrinterType.SelectedValue;

                ArParams[11] = new SqlParameter("@NoOfCopies", SqlDbType.Int);
                ArParams[11].Value = strNoOfCopies;

                ArParams[12] = new SqlParameter("@Trigger_Cash_Drawer", SqlDbType.TinyInt);
                ArParams[12].Value = ChkTCD.Checked ?1:0;

                Dictionary<string, string> dict;
                dict = new Dictionary<string, string>() { { "Rest_ID", Session["R_ID"].ToString() } };

                if (PrinterID.Value == "-1" && (Mode.Value=="add" || Mode.Value=="clone"))
                {
                    //dict = null;
                    flag1 = Fn.CheckRecordExists(dict, "omni_Printers", "PrinterName", "PrinterName", "", "", "", ArParams);
                    flag2 = Fn.CheckRecordExists(dict, "omni_Printers", "IPAddress", "IPAddress", "", "", "", ArParams);
                }
                else
                {
                    //========= FOR IMPLEMENTING MORE "AND" CLAUSE IN SQL OPERATION ================//

                    //dict = new Dictionary<string, string>() { { "UserGroupId", GroupID.Value } };
                    //flag = Fn.CheckRecordExists(dict, "omni_user_group", "UserGroupName", "UserGroupName", "edit", "UserGroupId", GroupID.Value, ArParams);

                    //=============================================================================//

                    flag1 = Fn.CheckRecordExists(dict, "omni_Printers", "PrinterName", "PrinterName", "edit", "PrinterId", PrinterID.Value, ArParams);
                    flag2 = Fn.CheckRecordExists(dict, "omni_Printers", "IPAddress", "IPAddress", "edit", "PrinterId", PrinterID.Value, ArParams);
                }

                if (flag1 == true)
                {
                    LblGrp.Text = "Printer Name already exist. Try with another one.";
                    return;
                }

/*                if (flag2 == true)
                {
                    LblIP.Text = "IP Address already exist. Try with another one.";
                    return;
                }
*/

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
                            SqlHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, "SP_omni_Printer_Update", ArParams);

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
                Response.Redirect("Printers.aspx");
        }
}
}