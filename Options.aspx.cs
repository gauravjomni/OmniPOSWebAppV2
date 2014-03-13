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

namespace PosOption
{
    public partial class OptionList : System.Web.UI.Page
    {
        DB mConnection = new DB();
        DataSet ds = new DataSet();
        Common Fn = new Common();
        protected MyToolSet iTool = new MyToolSet();

        public OptionList()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            /*mConnection = new DB();
            ds = Fn.LoadCookingOptions(null, "Rest_ID", Session["R_ID"].ToString());
            OptionRepeater.DataSource = ds;
            OptionRepeater.DataBind();*/

            string optionid = string.Empty;


            if (Session["R_ID"] == "" || Session["R_ID"] == null)
            {
                Session["bckurl"] = "Options.aspx";
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
                                        optionid = iTool.decryptString(Request.QueryString["id"]);
                                        Dictionary<string, string> dict;

                                        DateTime sDate = DateTime.Now;
                                        sDate = Fn.GetCommonDate(sDate, Session["DateFormat"]);

                                        SqlParameter[] ArParams = new SqlParameter[7];
                                        ArParams[0] = new SqlParameter("@OptionName", SqlDbType.VarChar, 100);
                                        ArParams[0].Value = "";

                                        ArParams[1] = new SqlParameter("@Rest_ID", SqlDbType.Int);
                                        ArParams[1].Value = Session["R_ID"];

                                        ArParams[2] = new SqlParameter("@Status", SqlDbType.Int);
                                        ArParams[2].Value = 0;

                                        ArParams[3] = new SqlParameter("@sDate", SqlDbType.DateTime);
                                        ArParams[3].Value = sDate;

                                        ArParams[4] = new SqlParameter("@LoggedUserID", SqlDbType.Int);
                                        ArParams[4].Value = Session["UserID"];

                                        ArParams[5] = new SqlParameter("@Mode", SqlDbType.VarChar, 20);
                                        ArParams[5].Value = "del";

                                        ArParams[6] = new SqlParameter("@OptionID", SqlDbType.Int);
                                        ArParams[6].Value = optionid;

                                        dict = new Dictionary<string, string>() { { "Rest_ID", Session["R_ID"].ToString() } };

                                        if (Fn.CheckRecordExists(null, "omni_Product_Cooking_Options", "OptionID", optionid))
                                            Msg.Visible = true;
                                        else
                                        {
                                            Msg.Visible = false;
                                            SqlHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, "SP_omni_Cooking_Option_Update", ArParams);
                                            trans.Commit();
                                        }
                                    }
                                }
                            }

                            ds = Fn.LoadCookingOptions(null, "Rest_ID", Session["R_ID"].ToString());
                            OptionRepeater.DataSource = ds;
                            OptionRepeater.DataBind();
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

        protected void OptionRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (OptionRepeater.Items.Count < 1)
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