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

namespace PosCategory
{
    public partial class Categories : System.Web.UI.Page
    {
        DB mConnection = new DB();
        DataSet ds = new DataSet();
        Common Fn = new Common();
        protected MyToolSet iTool = new MyToolSet();

        public Categories()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            /*mConnection = new DB();
            ds = Fn.LoadCategories(null, "Rest_ID", Session["R_ID"].ToString());
            CategoryRepeater.DataSource = ds;
            CategoryRepeater.DataBind(); */

            string categoryid = string.Empty;

            if (Session["R_ID"] == "" || Session["R_ID"] == null)
            {
                Session["bckurl"] = "Categories.aspx";
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
                                        categoryid = iTool.decryptString(Request.QueryString["id"]);
                                        Dictionary<string, string> dict;

                                        DateTime sDate = DateTime.Now;
                                        sDate = Fn.GetCommonDate(sDate, Session["DateFormat"]);

                                        SqlParameter[] ArParams = new SqlParameter[9];
                                        ArParams[0] = new SqlParameter("@CategoryName", SqlDbType.VarChar, 50);
                                        ArParams[0].Value = "";

                                        ArParams[1] = new SqlParameter("@Status", SqlDbType.Int);
                                        ArParams[1].Value = 0;

                                        ArParams[2] = new SqlParameter("@sDate", SqlDbType.DateTime);
                                        ArParams[2].Value = sDate;

                                        ArParams[3] = new SqlParameter("@LoggedUserID", SqlDbType.Int);
                                        ArParams[3].Value = Session["UserID"];

                                        ArParams[4] = new SqlParameter("@Mode", SqlDbType.VarChar, 20);
                                        ArParams[4].Value = "del";

                                        ArParams[5] = new SqlParameter("@CategoryId", SqlDbType.Int);
                                        ArParams[5].Value = categoryid;

                                        ArParams[6] = new SqlParameter("@Rest_ID", SqlDbType.Int);
                                        ArParams[6].Value = Session["R_ID"];

                                        ArParams[7] = new SqlParameter("@Name2", SqlDbType.VarChar, 100);
                                        ArParams[7].Value = "";

                                        ArParams[8] = new SqlParameter("@SortOrder", SqlDbType.Int);
                                        ArParams[8].Value = 0; 

                                        dict = new Dictionary<string, string>() { { "Rest_ID", Session["R_ID"].ToString() } };

                                        if (Fn.CheckRecordExists(dict, "omni_Products", "CategoryID", categoryid))
                                            Msg.Visible = true;
                                        else
                                        {
                                            string childid = Fn.GetTableColumnValue(dict,"omni_Item_Categories","CategoryID","ParentID",categoryid);
                                            
                                            if (childid !="" &&  childid!=null)
                                                Msg.Visible = true;
                                            else
                                            {
                                                Msg.Visible = false;
                                                SqlHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, "SP_omni_cate_subcate_Update", ArParams);
                                                trans.Commit();
                                            }
                                        }
                                    }
                                }
                            }

                            ds = Fn.LoadCategories(null, "Rest_ID", Session["R_ID"].ToString());
                            CategoryRepeater.DataSource = ds;
                            CategoryRepeater.DataBind();
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

        protected void CategoryRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (CategoryRepeater.Items.Count < 1)
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