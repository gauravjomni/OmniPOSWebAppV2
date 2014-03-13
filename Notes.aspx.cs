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

namespace PosNote
{
    public partial class Notes : System.Web.UI.Page
    {
        DB mConnection = new DB();
        DataSet ds = new DataSet();
        Common Fn = new Common();
        protected MyToolSet iTool = new MyToolSet();

        public Notes()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string noteid = string.Empty;
/*          mConnection = new DB();
            ds = Fn.LoadNotes(null, "Rest_ID", Session["R_ID"].ToString());
            NoteRepeater.DataSource = ds;
            NoteRepeater.DataBind();
*/

            if (Session["R_ID"] == "" || Session["R_ID"] == null)
            {
                Session["bckurl"] = "Notes.aspx";
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
                                        noteid = iTool.decryptString(Request.QueryString["id"]);
                                        Dictionary<string, string> dict;
                                        dict = new Dictionary<string, string>() { { "Rest_ID", Session["R_ID"].ToString() } };

                                        DateTime sDate = DateTime.Now;
                                        sDate = Fn.GetCommonDate(sDate, Session["DateFormat"]);

                                        SqlParameter[] ArParams = new SqlParameter[7];
                                        ArParams[0] = new SqlParameter("@Message", SqlDbType.Text);
                                        ArParams[0].Value = "";

                                        ArParams[1] = new SqlParameter("@sDate", SqlDbType.DateTime);
                                        ArParams[1].Value = sDate;

                                        ArParams[2] = new SqlParameter("@LoggedUserID", SqlDbType.Int);
                                        ArParams[2].Value = Convert.ToInt32(Session["UserID"]);

                                        ArParams[3] = new SqlParameter("@Rest_ID", SqlDbType.Int);
                                        ArParams[3].Value = Session["R_ID"];

                                        ArParams[4] = new SqlParameter("@Mode", SqlDbType.VarChar, 20);
                                        ArParams[4].Value = "del";

                                        ArParams[5] = new SqlParameter("@NoteID", SqlDbType.Int);
                                        ArParams[5].Value = noteid;

                                        ArParams[6] = new SqlParameter("@Status", SqlDbType.Int);
                                        ArParams[6].Value = 0;

                                        SqlHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, "SP_omni_OrderNote", ArParams);
                                        trans.Commit();
                                    }
                                }
                            }

                            ds = Fn.LoadNotes(null, "Rest_ID", Session["R_ID"].ToString());
                            NoteRepeater.DataSource = ds;
                            NoteRepeater.DataBind();
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

        protected void NoteRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (NoteRepeater.Items.Count < 1)
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