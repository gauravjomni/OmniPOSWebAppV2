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

namespace PosIng
{
    public partial class Ingredients : System.Web.UI.Page
    {
        DB mConnection = new DB();
        DataSet ds = new DataSet();
        SQLQuery Qry = new SQLQuery();
        Common Fn = new Common();
        protected MyToolSet iTool = new MyToolSet();

        public Ingredients()
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

            string ingid = string.Empty;

            if (Session["R_ID"] == "" || Session["R_ID"] == null)
            {
                Session["bckurl"] = "Ingredients.aspx";
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
                                        ingid = iTool.decryptString(Request.QueryString["id"]);
                                        Dictionary<string, string> dict;

                                        DateTime sDate = DateTime.Now;
                                        sDate = Fn.GetCommonDate(sDate, Session["DateFormat"]);

                                        SqlParameter[] ArParams = new SqlParameter[13];
                                        ArParams[0] = new SqlParameter("@IngredientName", SqlDbType.VarChar, 150);
                                        ArParams[0].Value = "";

                                        ArParams[1] = new SqlParameter("@Price", SqlDbType.Decimal);
                                        ArParams[1].Value = 0;

                                        ArParams[2] = new SqlParameter("@BarCode", SqlDbType.VarChar,50);
                                        ArParams[2].Value = 0;

                                        ArParams[3] = new SqlParameter("@UnitID", SqlDbType.Int);
                                        ArParams[3].Value = 0;

                                        ArParams[4] = new SqlParameter("@Status", SqlDbType.Int);
                                        ArParams[4].Value = 0;

                                        ArParams[5] = new SqlParameter("@RestID", SqlDbType.Int);
                                        ArParams[5].Value = Session["R_ID"];

                                        ArParams[6] = new SqlParameter("@sDate", SqlDbType.DateTime);
                                        ArParams[6].Value = sDate;

                                        ArParams[7] = new SqlParameter("@LoggedUserID", SqlDbType.Int);
                                        ArParams[7].Value = Convert.ToInt32(Session["UserID"]);

                                        ArParams[8] = new SqlParameter("@Mode", SqlDbType.VarChar, 20);
                                        ArParams[8].Value = "del";

                                        ArParams[9] = new SqlParameter("@Ingredient", SqlDbType.Int);
                                        ArParams[9].Value = ingid;

                                        dict = null;
                                        dict = new Dictionary<string, string>() { { "Rest_ID", Session["R_ID"].ToString() }, { "ProductType", "Ing" } };

                                        if (Fn.CheckRecordExists(dict, "omni_purchasedetail", "ProductID", ingid,trans))
                                            Msg.Visible = true;
                                        else
                                        {
                                            dict = null;
                                            dict = new Dictionary<string, string>() { { "Rest_ID", Session["R_ID"].ToString() }, { "MixingType", "Ingredient" } };

                                            if (Fn.CheckRecordExists(dict, "omni_Product_Ingredient_SubreciepeDetails", "IngredientID", ingid, trans))
                                                Msg.Visible = true;
                                            else
                                            {
                                                dict = null;
                                                dict = new Dictionary<string, string>() { { "Rest_ID", Session["R_ID"].ToString() }};

                                                if (Fn.CheckRecordExists(dict, "omni_SubRecipe_Mixing_Details", "IngredientID", ingid, trans))
                                                    Msg.Visible = true;
                                                else
                                                {
                                                    Msg.Visible = false;
                                                    SqlHelper.ExecuteNonQuery(trans, CommandType.Text, Qry.DeleteRec(null, "omni_Items_Ingredients", 1, "IngredientID", ingid));
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

                    ds = Fn.LoadIngredients(null, "a.Rest_ID", Session["R_ID"].ToString(), conn);
                    IngredientRepeater.DataSource = ds;
                    IngredientRepeater.DataBind(); 

                }
            }
            catch (Exception ex)
            { }  
        }

        protected void IngredientRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (IngredientRepeater.Items.Count < 1)
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