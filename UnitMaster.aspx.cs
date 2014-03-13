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
using MyQuery;
using MyTool;

namespace PosUnit
{
    public partial class UnitMaster : System.Web.UI.Page
    {
        DB mConnection = new DB();
        DataSet ds = new DataSet();
        Common Fn = new Common();
        SQLQuery Qry = new SQLQuery();
        protected MyToolSet iTool = new MyToolSet();

        public UnitMaster()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string Unitid = string.Empty;

            if (Session["R_ID"] == "" || Session["R_ID"] == null)
            {
                Session["bckurl"] = "UnitMaster.aspx";
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
                                        Unitid = iTool.decryptString(Request.QueryString["id"]);
                                        Dictionary<string, string> dict;

                                        DateTime sDate = DateTime.Now;
                                        sDate = Fn.GetCommonDate(sDate, Session["DateFormat"]);

                                        SqlParameter[] ArParams = new SqlParameter[7];
                                        ArParams[0] = new SqlParameter("@UnitName", SqlDbType.VarChar, 50);
                                        ArParams[0].Value = "";

                                        ArParams[1] = new SqlParameter("@Status", SqlDbType.Int);
                                        ArParams[1].Value = 0;

                                        ArParams[2] = new SqlParameter("@sDate", SqlDbType.DateTime);
                                        ArParams[2].Value = sDate;

                                        ArParams[3] = new SqlParameter("@Rest_ID", SqlDbType.Int);
                                        ArParams[3].Value = Session["R_ID"];

                                        ArParams[4] = new SqlParameter("@LoggedUserID", SqlDbType.Int);
                                        ArParams[4].Value = Session["UserID"];

                                        ArParams[5] = new SqlParameter("@Mode", SqlDbType.VarChar, 20);
                                        ArParams[5].Value = "del";

                                        ArParams[6] = new SqlParameter("@UnitID", SqlDbType.Int);
                                        ArParams[6].Value = Unitid;

                                        dict = new Dictionary<string, string>() { { "Rest_ID", Session["R_ID"].ToString() } };

                                        if (Fn.CheckRecordExists(dict, "omni_Items_Ingredients", "UnitID", Unitid,trans))
                                            Msg.Visible = true;
                                        else
                                        {
                                            dict = null;
                                            dict = new Dictionary<string, string>() { { "Rest_ID", Session["R_ID"].ToString() } };

                                            if (Fn.CheckRecordExists(dict, "omni_SubRecipe", "UnitID", Unitid,trans))
                                                Msg.Visible = true;
                                            else
                                            {
                                                if (Fn.CheckRecordExists(dict, "omni_Products", "UnitID", Unitid, trans))
                                                    Msg.Visible = true;
                                                else
                                                {
                                                    Msg.Visible = false;
                                                    SqlHelper.ExecuteNonQuery(trans, CommandType.Text, Qry.DeleteRec(null, "omni_UnitMaster", 1, "UnitID", Unitid));
                                                    trans.Commit();
                                                }
                                            }
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

                    ds = Fn.LoadUnits(null, "Rest_ID", Session["R_ID"].ToString(), conn);
                    UnitRepeater.DataSource = ds;
                    UnitRepeater.DataBind(); 
                }
            }
            catch (Exception ex)
            { }  
        }

        protected void UnitRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (UnitRepeater.Items.Count < 1)
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