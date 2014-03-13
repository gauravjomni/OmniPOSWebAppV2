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

namespace PosRecipe
{
    public partial class SubRecipes : System.Web.UI.Page
    {
        DB mConnection = new DB();
        DataSet ds = new DataSet();
        Common Fn = new Common();
        SQLQuery Qry = new SQLQuery();
        protected MyToolSet iTool = new MyToolSet();

        public SubRecipes()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string subrcptid = string.Empty;

            if (Session["R_ID"] == "" || Session["R_ID"] == null)
            {
                Session["bckurl"] = "SubRecipes.aspx";
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
                                        subrcptid = iTool.decryptString(Request.QueryString["id"]);
                                        Dictionary<string, string> dict;

                                        DateTime sDate = DateTime.Now;
                                        sDate = Fn.GetCommonDate(sDate, Session["DateFormat"]);

                                        SqlParameter[] ArParams = new SqlParameter[8];
                                        ArParams[0] = new SqlParameter("@SubRecipeID", SqlDbType.Int);
                                        ArParams[0].Value = subrcptid;

                                        ArParams[1] = new SqlParameter("@SubRecipeName", SqlDbType.VarChar,150);
                                        ArParams[1].Value = "";
                                        
                                        ArParams[2] = new SqlParameter("@UnitID", SqlDbType.Int);
                                        ArParams[2].Value = "";

                                        ArParams[3] = new SqlParameter("@Rest_ID", SqlDbType.Int);
                                        ArParams[3].Value = Session["R_ID"];

                                        ArParams[4] = new SqlParameter("@LoggedUserID", SqlDbType.Int);
                                        ArParams[4].Value = Session["UserID"];

                                        ArParams[5] = new SqlParameter("@sDate", SqlDbType.DateTime);
                                        ArParams[5].Value = sDate;

                                        ArParams[6] = new SqlParameter("@Status", SqlDbType.Int);
                                        ArParams[6].Value = "";                                        

                                        ArParams[7] = new SqlParameter("@Mode", SqlDbType.VarChar, 20);
                                        ArParams[7].Value = "del";

                                        dict = null;
                                        dict = new Dictionary<string, string>() { { "Rest_ID", Session["R_ID"].ToString() }, { "MixingType", "Sub Recipe" } };

                                        if (Fn.CheckRecordExists(dict, "omni_Product_Ingredient_SubreciepeDetails", "IngredientId", subrcptid,trans))
                                            Msg.Visible = true;
                                        else
                                        {
                                            //if (Fn.CheckRecordExists(dict, "omni_Product_Kitchen_Printer_Options", "OptionID", Printid))
                                            //    Msg.Visible = true;
                                            //else
                                            //{
                                                Msg.Visible = false;
                                                SqlHelper.ExecuteNonQuery(trans, CommandType.Text, Qry.DeleteRec(null, "omni_SubRecipe", 1, "SubRecipeID", subrcptid));
                                                trans.Commit();
                                            //}
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

                    ds = Fn.LoadSubRecipes(null, "a.Rest_ID", Session["R_ID"].ToString(), conn);
                    SubRecipeRepeater.DataSource = ds;
                    SubRecipeRepeater.DataBind(); 
                }
            }
            catch (Exception ex)
            { }  
        }

        protected void SubRecipeRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (SubRecipeRepeater.Items.Count < 1)
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