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
using MyQuery;
using Commons;
using MyTool;

namespace PosSettings{

    public partial class Option_Settings : System.Web.UI.Page
    {
        DB mConnection = new DB();
        DataSet ds = new DataSet();
        Common Fn = new Common();
        SQLQuery Qry = new SQLQuery();
        protected MyToolSet iTool = new MyToolSet();

        public Option_Settings()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    using (SqlConnection conn = mConnection.GetConnection())
                    {
                        conn.Open();

                        using (SqlTransaction trans = conn.BeginTransaction())
                        {
                            try
                            {
                                SqlParameter[] ArParams = new SqlParameter[2];

                                ArParams[0] = new SqlParameter("@OptionName", SqlDbType.VarChar, 50);
                                ArParams[0].Value = "AdjustmentPercentange";

                                ArParams[1] = new SqlParameter("@OptionValue", SqlDbType.VarChar, 50);
                                ArParams[1].Direction = ParameterDirection.Output;

                                SqlHelper.ExecuteNonQuery(mConnection.GetConnection(), CommandType.StoredProcedure, "getOption_Settings", ArParams);
                                txtAdjustment.Value =  ArParams[1].Value.ToString();
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
                catch (Exception ex)
                { }
            }
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsPostBack)
                {
                    string strAdjustment = iTool.formatInputString(txtAdjustment.Value);

                    SqlParameter[] ArParams = new SqlParameter[2];

                    ArParams[0] = new SqlParameter("@OptionName", SqlDbType.VarChar, 50);
                    ArParams[0].Value = "AdjustmentPercentange";

                    ArParams[1] = new SqlParameter("@OptionValue", SqlDbType.VarChar, 50);
                    ArParams[1].Value = strAdjustment;

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

                                SqlHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, "SP_omni_Option_Settings_Update", ArParams);
                                trans.Commit();
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
                }
            }
            catch (Exception ex)
            {

                // CreateLogFiles.ErrorLog(ex.Message.ToString() & " - " & System.IO.Path.GetFileName(System.Web.HttpContext.Current.Request.Url.AbsolutePath) & " - btnCreate_Click")
                // lblError.Text = "Error - Please contact Administrator "
                // Exit Sub
            }
        }
}
}