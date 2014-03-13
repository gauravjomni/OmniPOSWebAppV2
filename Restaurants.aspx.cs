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
using System.Data.SqlClient;
using MyDB;
using MyQuery;
using Commons;
using MyTool;

namespace PosLocation
{
    public partial class Restaurants : System.Web.UI.Page
    {
        DB mConnection;
        DataSet ds = new DataSet();
        Common Fn = new Common();
        protected MyToolSet iTool = new MyToolSet();

        public Restaurants()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            mConnection = new DB();
            ds = Fn.LoadRestaurants();
            RestaurantRepeater.DataSource = ds;
            RestaurantRepeater.DataBind();
            SQLQuery Qry = new SQLQuery();
        
            string restuarantid = string.Empty;
         //   string confvalue="Ok";
            //rid4 = Session["R_ID"].ToString();
          //    rid = Request.QueryString["RID"].ToString();
     //       rid1 = Request.QueryString["Rest_ID"].ToString();
     //       rid2 = Request.QueryString["Rest_Id"].ToString();
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
                                    if (Request.QueryString["RID"] != null && Request.QueryString["RID"] != "")
                                    {
                                        restuarantid = iTool.decryptString(Request.QueryString["RID"].ToString());
                                      
                                            SqlHelper.ExecuteNonQuery(trans, CommandType.Text, Qry.DeleteRec(null, "omni_Restuarnt_info", 1, "Rest_ID", restuarantid));
                                            trans.Commit();
                                            Response.Redirect("Restaurants.aspx");
                                          
                                    }
                                }
                                     
                            }

                        }
                        catch (Exception ex)
                        {
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