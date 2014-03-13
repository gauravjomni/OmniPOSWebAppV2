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

namespace PosUsers
{
    public partial class Users : System.Web.UI.Page
    {
        DB mConnection = new DB();
        DataSet ds = new DataSet();
        Common Fn = new Common();
        protected MyToolSet iTool = new MyToolSet();

        public Users()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            /*mConnection = new DB();
            ds = Fn.LoadUsers(null, "Rest_ID", Session["R_ID"].ToString());
            UserRepeater.DataSource = ds;
            UserRepeater.DataBind();*/
            string strusrid = string.Empty;


            if (Session["R_ID"] == "" || Session["R_ID"] == null)
            {
                Session["bckurl"] = "Users.aspx";
                //Response.Redirect("Select_Restaurants.aspx?bckurl=" + Session["bckurl"]);
                Server.Transfer("Notification.aspx");
                return;
            }

                using (SqlConnection conn = mConnection.GetConnection())
                {
                    conn.Open();

                    try
                    {
                        //if (!IsPostBack)
                        //{
                        //    if (Request.QueryString["mode"] != null && Request.QueryString["mode"] == "del")
                        //    {
                        //        if (Request.QueryString["id"] != null && Request.QueryString["id"] != "")
                        //        {
                        //            strusrid = iTool.decryptString(Request.QueryString["id"]);
                        //            Dictionary<string, string> dict;

                        //            DateTime sDate = DateTime.Now;
                        //            sDate = Fn.GetCommonDate(sDate, Session["DateFormat"]);

                        //            SqlParameter[] ArParams = new SqlParameter[17];
                        //            ArParams[0] = new SqlParameter("@UserAlias", SqlDbType.VarChar, 50);
                        //            ArParams[0].Value = "";

                        //            ArParams[1] = new SqlParameter("@UserName", SqlDbType.VarChar, 50);
                        //            ArParams[1].Value = "";

                        //            ArParams[2] = new SqlParameter("@UserPassword", SqlDbType.Binary, 50);
                        //            ArParams[2].Value = null;

                        //            ArParams[3] = new SqlParameter("@PassCheck", SqlDbType.Char, 1);
                        //            ArParams[3].Value = "";

                        //            ArParams[4] = new SqlParameter("@UserEmail", SqlDbType.VarChar, 100);
                        //            ArParams[4].Value = "";

                        //            ArParams[5] = new SqlParameter("@Status", SqlDbType.Int);
                        //            ArParams[5].Value = 0;

                        //            ArParams[6] = new SqlParameter("@UserGroupID", SqlDbType.Int);
                        //            ArParams[6].Value = 0;

                        //            ArParams[7] = new SqlParameter("@sDate", SqlDbType.DateTime);
                        //            ArParams[7].Value = sDate;

                        //            ArParams[8] = new SqlParameter("@LoggedUserID", SqlDbType.Int);
                        //            ArParams[8].Value = Convert.ToInt32(Session["UserID"]);

                        //            ArParams[9] = new SqlParameter("@RestID", SqlDbType.Int);
                        //            ArParams[9].Value = Session["R_ID"];

                        //            ArParams[10] = new SqlParameter("@Mode", SqlDbType.VarChar, 20);
                        //            ArParams[10].Value = "del";

                        //            ArParams[11] = new SqlParameter("@UserID", SqlDbType.Int);
                        //            ArParams[11].Value = strusrid;

                        //            ArParams[12] = new SqlParameter("@UserPin", SqlDbType.Int);
                        //            ArParams[12].Value = 0;

                        //            ArParams[13] = new SqlParameter("@FirstName", SqlDbType.VarChar, 50);
                        //            ArParams[13].Value = "";

                        //            ArParams[14] = new SqlParameter("@LastName", SqlDbType.VarChar, 50);
                        //            ArParams[14].Value = "";

                        //            ArParams[15] = new SqlParameter("@UserPhone", SqlDbType.VarChar, 20);
                        //            ArParams[15].Value = "";

                        //            ArParams[16] = new SqlParameter("@StartDate", SqlDbType.DateTime);
                        //            ArParams[16].Value = DateTime.Now; 
                                    
                        //            dict = new Dictionary<string, string>() { { "Rest_ID", Session["R_ID"].ToString() } };

                        //            if (Fn.CheckRecordExists(dict, "omni_user_group", "ModifierID", modifierid))
                        //                Msg.Visible = true;
                        //            else
                        //            {
                        //                Msg.Visible = false;
                        //                SqlHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, "SP_omni_Modifier_Update", ArParams);
                        //                trans.Commit();
                        //            }
                        //        }
                        //    }
                        //}

                        //ds = Fn.LoadUsers(null, "Rest_ID", Session["R_ID"].ToString());
                        ds = Fn.LoadUsers(null, "Rest_ID", Session["R_ID"].ToString(), conn);
                        UserRepeater.DataSource = ds;
                        UserRepeater.DataBind();
                    }
                    catch (Exception ex)
                    {
                        /*trans.Rollback();*/
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