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

namespace PosNote
{
    public partial class AddNote : System.Web.UI.Page
    {
        DB mConnection = new DB() ;
        DataSet ds = new DataSet();
        Common Fn = new Common();
        MyToolSet iTool = new MyToolSet();
        SQLQuery Qry = new SQLQuery();
        //string sQuery = "";


        public AddNote()
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
                    Session["bckurl"] = "AddNote.aspx";
                    Server.Transfer("Notification.aspx");
                    return;
                }

//                if (Fn.CheckRecordCount(null, "omni_Kitchen_Instruction", "", "") == false)
                    //Mode.Value = "add";
//                else
//                {
//                    Mode.Value = "edit";

                if (Request.QueryString["noteid"] != null)
                {
                    string strnoteid = "";


                    strnoteid = iTool.decryptString(Request.QueryString["noteid"]);

                    SqlParameter[] ArParams = new SqlParameter[3];

                    ArParams[0] = new SqlParameter("@NoteID", SqlDbType.Int);
                    ArParams[0].Value = strnoteid;

                    ArParams[1] = new SqlParameter("@Message", SqlDbType.VarChar, 8000);
                    ArParams[1].Direction = ParameterDirection.Output;

                    ArParams[2] = new SqlParameter("@Status", SqlDbType.Int);
                    ArParams[2].Direction = ParameterDirection.Output;


                    try
                    {
                        // Call ExecuteNonQuery static method of SqlHelper class
                        // We pass in database connection string, command type, stored procedure name and an array of SqlParameter objects
                        SqlHelper.ExecuteNonQuery(mConnection.GetConnection(), CommandType.StoredProcedure, "getOrder_Note", ArParams);

                        // Display results in text box using the values of output parameters	

                        txtMessage.Value =  ArParams[1].Value.ToString();
                        Status.Checked = ArParams[2].Value.ToString() == "1" ? true : false;
                        //NoteID.Value = strnoteid;

                        if (Request.QueryString["mode"] != null)
                        {
                            if (Request.QueryString["mode"] == "edit")
                                Mode.Value = "edit";
                            else if (Request.QueryString["mode"] == "clone")
                                Mode.Value = "clone";
                        }

                        //display name on top
                        string itemType = "Note";
                        LblHead.Text = char.ToUpper(Mode.Value[0]) + Mode.Value.Substring(1) + " " + itemType + " [ " + ArParams[1].Value.ToString() + " ]";

                        if (Mode.Value == "add" || Mode.Value == "clone")
                            NoteID.Value = "-1";
                        else
                            NoteID.Value = strnoteid;

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

                string strMessage = iTool.formatInputString(txtMessage.Value);

                DateTime sDate = DateTime.Now;
                sDate = Fn.GetCommonDate(sDate, Session["DateFormat"]);

                SqlParameter[] ArParams = new SqlParameter[7];
                ArParams[0] = new SqlParameter("@Message", SqlDbType.Text);
                ArParams[0].Value = strMessage;

                ArParams[1] = new SqlParameter("@sDate", SqlDbType.DateTime);
                ArParams[1].Value = sDate;

                ArParams[2] = new SqlParameter("@LoggedUserID", SqlDbType.Int);
                ArParams[2].Value = Convert.ToInt32(Session["UserID"]);

                ArParams[3] = new SqlParameter("@Rest_ID", SqlDbType.Int);
                ArParams[3].Value = Session["R_ID"];

                ArParams[4] = new SqlParameter("@Mode", SqlDbType.VarChar,20);
                ArParams[4].Value = Mode.Value;

                ArParams[5] = new SqlParameter("@NoteID", SqlDbType.Int);
                ArParams[5].Value = NoteID.Value;

                ArParams[6] = new SqlParameter("@Status", SqlDbType.Int);
                ArParams[6].Value = Status.Checked ? 1 : 0;
                
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
                                SqlHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, "SP_omni_OrderNote", ArParams);

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
                Response.Redirect("Notes.aspx");
        }

    }
}