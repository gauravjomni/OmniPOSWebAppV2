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

namespace PosState
{
    public partial class AddState : System.Web.UI.Page
    {
        
        DB mConnection = new DB() ;
        DataSet ds = new DataSet();
        Common Fn = new Common();
        MyToolSet iTool = new MyToolSet();
        string sQuery = "";


        public AddState()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Fn.switchingbeteenlocation2company(true);
            
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["stid"] != null)
                {
                    string stid = "";
                    SqlParameter[] ArParams = new SqlParameter[3];

                    stid = iTool.decryptString(Request.QueryString["stid"]);

                    ArParams[0] = new SqlParameter("@StateID", SqlDbType.Int);
                    ArParams[0].Value = stid;

                    // @UserGroupName Output Parameter
                    ArParams[1] = new SqlParameter("@StateName", SqlDbType.VarChar, 50);
                    ArParams[1].Direction = ParameterDirection.Output;

                    // @Status Output Parameter
                    ArParams[2] = new SqlParameter("@Status", SqlDbType.Int);
                    ArParams[2].Direction = ParameterDirection.Output;

                    using (SqlConnection conn = mConnection.GetConnection())
                    {
                        conn.Open();

                        try
                        {
                            // Call ExecuteNonQuery static method of SqlHelper class
                            // We pass in database connection string, command type, stored procedure name and an array of SqlParameter objects
                            SqlHelper.ExecuteNonQuery(conn, CommandType.StoredProcedure, "getStateDetails", ArParams);

                            // Display results in text box using the values of output parameters	
                            txtStateName.Value = ArParams[1].Value.ToString();
                            Status.Checked = ArParams[2].Value.ToString() == "1" ? true : false;
                            Mode.Value = "edit";
                            StateID.Value = stid;

                            //display name on top
                            string itemType = "State";
                            LblHead.Text = char.ToUpper(Mode.Value[0]) + Mode.Value.Substring(1) + " " + itemType + " [ " + ArParams[1].Value.ToString() + " ]";
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
//                    sQuery = "selinsert into omni_user_group(UserGroupName,CreateDate,IsActive) " +
//                    " values(@usrgrpname,@CreateDate,@status)";
                }
            }
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
                string strStateName = iTool.formatInputString(txtStateName.Value);
                bool flag = false;

                DateTime sDate = DateTime.Now;
                sDate = Fn.GetCommonDate(sDate, Session["DateFormat"]);

                SqlParameter[] ArParams = new SqlParameter[4];
                ArParams[0] = new SqlParameter("@StateName", SqlDbType.VarChar, 50);
                ArParams[0].Value = strStateName;

                ArParams[1] = new SqlParameter("@Status", SqlDbType.Int);
                ArParams[1].Value = Status.Checked ? 1 : 0;

                ArParams[2] = new SqlParameter("@Mode", SqlDbType.VarChar,20);
                ArParams[2].Value = Mode.Value;

                ArParams[3] = new SqlParameter("@StateID", SqlDbType.Int);
                ArParams[3].Value = StateID.Value;

                using (SqlConnection conn = mConnection.GetConnection())
                {
                    conn.Open();

                    try
                    {
                        Dictionary<string, string> dict;

                        if (StateID.Value == "-1")
                        {
                            dict = null;
                            flag = Fn.CheckRecordExists(dict, "omni_State", "StateName", "StateName", "", "", "", ArParams,conn);
                        }
                        else
                        {
                            //========= FOR IMPLEMENTING MORE "AND" CLAUSE IN SQL OPERATION ================//

                            //dict = new Dictionary<string, string>() { { "UserGroupId", GroupID.Value } };
                            //flag = Fn.CheckRecordExists(dict, "omni_user_group", "UserGroupName", "UserGroupName", "edit", "UserGroupId", GroupID.Value, ArParams);

                            //=============================================================================//

                            flag = Fn.CheckRecordExists(null, "omni_State", "StateName", "StateName", "edit", "StateId", StateID.Value, ArParams,conn);
                        }

                        if (flag == true)
                        {
                            LblState.Text = "State Name already exist. Try with another one.";
                            return;
                        }

                        using (SqlTransaction trans = conn.BeginTransaction())
                        {
                            // Establish command parameters
                            // @AccountNo (From Account)
                            try
                            {
                                // Call ExecuteNonQuery static method of SqlHelper class for debit and credit operations.
                                // We pass in SqlTransaction object, command type, stored procedure name, and a comma delimited list of SqlParameters

                                // Perform the debit operation
                                //SqlHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, "Debit", paramFromAcc, paramDebitAmount);
                                //SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sQuery, ArParams);
                                SqlHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, "SP_omni_State_Update", ArParams);

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

                        }

                    }
                
					catch(Exception ex)
					{
					   // CreateLogFiles.ErrorLog(ex.Message.ToString() & " - " & System.IO.Path.GetFileName(System.Web.HttpContext.Current.Request.Url.AbsolutePath) & " - btnCreate_Click")
					   // lblError.Text = "Error - Please contact Administrator "
					   // Exit Sub
					}
                    finally
                    {
                        conn.Close();
                    }

				}

				if (Session["IsState"] != null)
				{
					if ((bool)Session["IsState"] == false)
					{
						Session["IsState"] = true;
						Response.Redirect("Select_Restaurants.aspx");
					}
					else
						Response.Redirect("States.aspx");
				}
				else
					Response.Redirect("States.aspx");

        }
	}
}