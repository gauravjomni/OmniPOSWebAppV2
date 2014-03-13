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
using Commons;
using MyTool;

namespace PosUserGroup
{
    public partial class UserGroups : System.Web.UI.Page
    {
        DB mConnection = new DB();
        DataSet ds = new DataSet();
        Common Fn = new Common();
        protected MyToolSet iTool = new MyToolSet();

        public UserGroups()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            /*mConnection = new DB();
            ds = Fn.LoadUserGroups();
            UserGroupRepeater.DataSource = ds;
            UserGroupRepeater.DataBind();*/

            string usrgrpid = string.Empty;

            try
            {
                using (SqlConnection conn = mConnection.GetConnection())
                {
                    conn.Open();

                    using (SqlTransaction trans = conn.BeginTransaction())
                    {
                        try
                        {
                            if (!IsPostBack)
                            {
                                if (Request.QueryString["mode"] != null && Request.QueryString["mode"] == "del")
                                {
                                    if (Request.QueryString["id"] != null && Request.QueryString["id"] != "")
                                    {
                                        usrgrpid = iTool.decryptString(Request.QueryString["id"]);
                                        Dictionary<string, string> dict;

                                        DateTime sDate = DateTime.Now;
                                        sDate = Fn.GetCommonDate(sDate, Session["DateFormat"]);

                                        SqlParameter[] ArParams = new SqlParameter[6];
                                        ArParams[0] = new SqlParameter("@UserGroupName", SqlDbType.VarChar, 50);
                                        ArParams[0].Value = "";

                                        ArParams[1] = new SqlParameter("@Status", SqlDbType.Int);
                                        ArParams[1].Value = 0;

                                        ArParams[2] = new SqlParameter("@sDate", SqlDbType.DateTime);
                                        ArParams[2].Value = sDate;

                                        ArParams[3] = new SqlParameter("@LoggedUserID", SqlDbType.Int);
                                        ArParams[3].Value = Session["UserID"];

                                        ArParams[4] = new SqlParameter("@Mode", SqlDbType.VarChar, 20);
                                        ArParams[4].Value = "del";

                                        ArParams[5] = new SqlParameter("@UserGroupID", SqlDbType.Int);
                                        ArParams[5].Value = usrgrpid; 
                                        
                                        dict = new Dictionary<string, string>() { { "Rest_ID", Session["R_ID"].ToString() } };

                                        if (Fn.CheckRecordExists(null, "omni_users", "UserGroupID", usrgrpid))
                                            Msg.Visible = true;
                                        else
                                        {
                                            Msg.Visible = false;
                                            SqlHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, "SP_omni_user_group_Update", ArParams);
                                            trans.Commit();
                                        }
                                    }
                                }
                            }

                            ds = Fn.LoadUserGroups();
                            UserGroupRepeater.DataSource = ds;
                            UserGroupRepeater.DataBind();
                        }
                        catch (Exception ex)
                        {
                            trans.Rollback();
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
}