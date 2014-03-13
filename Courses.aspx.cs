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

namespace PosCourse
{
    public partial class Courses : System.Web.UI.Page
    {
        DB mConnection = new DB();
        DataSet ds = new DataSet();
        Common Fn = new Common();
        protected MyToolSet iTool = new MyToolSet();

        public Courses()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            /*mConnection = new DB();
            ds = Fn.LoadCourses(null, "Rest_ID", Session["R_ID"].ToString());

            CourseRepeater.DataSource = ds;
            CourseRepeater.DataBind();*/
            string Courseid = string.Empty;

            if (Session["R_ID"] == "" || Session["R_ID"] == null)
            {
                Session["bckurl"] = "Courses.aspx";
                //Response.Redirect("Select_Restaurants.aspx?bckurl=" + Session["bckurl"]);
                Server.Transfer("Notification.aspx");
                return;
            }


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
                                        Courseid= iTool.decryptString(Request.QueryString["id"]);
                                        Dictionary<string, string> dict;
                                        dict = null;

                                        DateTime sDate = DateTime.Now;
                                        sDate = Fn.GetCommonDate(sDate, Session["DateFormat"]);

                                        SqlParameter[] ArParams = new SqlParameter[8];
                                        ArParams[0] = new SqlParameter("@CourseName", SqlDbType.VarChar, 100);
                                        ArParams[0].Value = "";

                                        ArParams[1] = new SqlParameter("@SortOrder", SqlDbType.Int);
                                        ArParams[1].Value = 0;

                                        ArParams[2] = new SqlParameter("@Status", SqlDbType.Int);
                                        ArParams[2].Value = 0;

                                        ArParams[3] = new SqlParameter("@sDate", SqlDbType.DateTime);
                                        ArParams[3].Value = sDate;

                                        ArParams[4] = new SqlParameter("@Rest_ID", SqlDbType.Int);
                                        ArParams[4].Value = Convert.ToInt32(Session["R_ID"]);

                                        ArParams[5] = new SqlParameter("@LoggedUserID", SqlDbType.Int);
                                        ArParams[5].Value = Convert.ToInt32(Session["UserID"]);

                                        ArParams[6] = new SqlParameter("@Mode", SqlDbType.VarChar, 20);
                                        ArParams[6].Value = "del";

                                        ArParams[7] = new SqlParameter("@CourseID", SqlDbType.Int);
                                        ArParams[7].Value = Courseid;

                                        if (Fn.CheckRecordExists(null, "omni_Products", "CourseID", Courseid))
                                        {
                                            Msg.Visible = true;
                                        }
                                        else
                                        {
                                            Msg.Visible = false;
                                            SqlHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, "SP_omni_Course_Update", ArParams);
                                            trans.Commit();
                                        }
                                    }
                                }
                            }

                            ds = Fn.LoadCourses(null, "Rest_ID", Session["R_ID"].ToString());
                            CourseRepeater.DataSource = ds;
                            CourseRepeater.DataBind();
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

        protected void CourseRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (CourseRepeater.Items.Count < 1)
            {
                if (e.Item.ItemType == ListItemType.Footer)
                {
                    Label lblFooter = (Label)e.Item.FindControl("lblEmptyData");
                    lblFooter.Visible = true;
                }
            }
        }

    }
}