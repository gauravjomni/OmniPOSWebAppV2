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

namespace PosModifiers
{
    public partial class Modifiers : System.Web.UI.Page
    {
        DB mConnection = new DB();
        DataSet ds = new DataSet();
        Common Fn = new Common();
        protected MyToolSet iTool = new MyToolSet();

        public Modifiers()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            /*mConnection = new DB();
            ds = Fn.LoadModifiers(null, "Rest_ID", Session["R_ID"].ToString());
            ModfRepeater.DataSource = ds;
            ModfRepeater.DataBind(); */

            string modifierid = string.Empty;

            if (Session["R_ID"] == "" || Session["R_ID"] == null)
            {
                Session["bckurl"] = "Modifiers.aspx";
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
                                        modifierid = iTool.decryptString(Request.QueryString["id"]);
                                        Dictionary<string, string> dict;

                                        DateTime sDate = DateTime.Now;
                                        sDate = Fn.GetCommonDate(sDate, Session["DateFormat"]);

                                        SqlParameter[] ArParams = new SqlParameter[13];
                                        ArParams[0] = new SqlParameter("@ModifierName", SqlDbType.VarChar, 50);
                                        ArParams[0].Value = "";

                                        ArParams[1] = new SqlParameter("@Price1", SqlDbType.Decimal);
                                        ArParams[1].Value = 0;

                                        ArParams[2] = new SqlParameter("@SortOrder", SqlDbType.Int);
                                        ArParams[2].Value = 0;

                                        ArParams[3] = new SqlParameter("@ModifierLevelID", SqlDbType.Int);
                                        ArParams[3].Value = 0;

                                        ArParams[4] = new SqlParameter("@Status", SqlDbType.Int);
                                        ArParams[4].Value = 0;

                                        ArParams[5] = new SqlParameter("@GST", SqlDbType.Int);
                                        ArParams[5].Value = 0;

                                        ArParams[6] = new SqlParameter("@RestID", SqlDbType.Int);
                                        ArParams[6].Value = Session["R_ID"];

                                        ArParams[7] = new SqlParameter("@sDate", SqlDbType.DateTime);
                                        ArParams[7].Value = sDate;

                                        ArParams[8] = new SqlParameter("@LoggedUserID", SqlDbType.Int);
                                        ArParams[8].Value = Convert.ToInt32(Session["UserID"]);

                                        ArParams[9] = new SqlParameter("@Mode", SqlDbType.VarChar, 20);
                                        ArParams[9].Value = "del";

                                        ArParams[10] = new SqlParameter("@ModifierId", SqlDbType.Int);
                                        ArParams[10].Value = modifierid;

                                        ArParams[11] = new SqlParameter("@Name2", SqlDbType.VarChar, 50);
                                        ArParams[11].Value = "";

                                        ArParams[12] = new SqlParameter("@Price2", SqlDbType.Decimal);
                                        ArParams[12].Value = 0;
                                        
                                        dict = new Dictionary<string, string>() { { "Rest_ID", Session["R_ID"].ToString() } };

                                        if (Fn.CheckRecordExists(null, "omni_Product_Modifiers", "ModifierID", modifierid))
                                            Msg.Visible = true;
                                        else
                                        {
                                            Msg.Visible = false;
                                            SqlHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, "SP_omni_Modifier_Update", ArParams);
                                            trans.Commit();
                                        }
                                    }
                                }
                            }

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

                    ds = Fn.LoadModifiers(null, "Rest_ID", Session["R_ID"].ToString(), conn);
                    ModfRepeater.DataSource = ds;
                    ModfRepeater.DataBind(); 

                }
            }
            catch (Exception ex)
            { }  
        }

        protected void ModfRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (ModfRepeater.Items.Count < 1)
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