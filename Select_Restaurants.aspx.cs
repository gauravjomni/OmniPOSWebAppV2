using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.ApplicationBlocks.Data;
using MyDB;
using Commons;
using MyTool;

namespace PosLocation
{
    public partial class Select_Restaurants : System.Web.UI.Page
    {
        DB mConnection = new DB();
        DataSet ds = new DataSet();
        Common Fn = new Common();
        protected MyToolSet iTool = new MyToolSet();

        public Select_Restaurants()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        protected void Page_Load(object sender, EventArgs e)
        {
           // mConnection = new DB();

            using (SqlConnection conn = mConnection.GetConnection())
            {
                conn.Open();

                try
                {
                    if (Fn.CheckRecordCount(null, "omni_State", "isactive", "1",conn) == false)
                    {
                        Session["IsState"] = false;
                        Server.Transfer("AddState.aspx");
                    }
                    else
                    {
                        if (Fn.CheckRecordCount(null, "omni_Restuarnt_info", "isactive", "1",conn) == false)
                            Server.Transfer("AddRestaurant.aspx");
                        else
                        {
                            //ds = Fn.LoadRestaurants();
                            ds = Fn.LoadRestaurants(conn);
                            RestaurantRepeater.DataSource = ds;
                            RestaurantRepeater.DataBind();
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

        protected void RestaurantRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HtmlAnchor ha = (HtmlAnchor) e.Item.FindControl("link");
                Button bt = (Button)e.Item.FindControl("Select");
                bt.Click += new EventHandler(Select_Click);

                //ha.HRef = Session["bckurl"].ToString() + "?RID=" + rid.Value.ToString() ;
                //ha.Attributes.Add("onclick", "javascript:setrestaurant()");
                
                //Format label text as required
            }
        }

        protected void Select_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            RepeaterItem ritem = (RepeaterItem)btn.NamingContainer;
            HiddenField rid = (HiddenField)ritem.FindControl("RestID");
            HiddenField rinitial = (HiddenField)ritem.FindControl("RestInitial");
            HiddenField rname = (HiddenField)ritem.FindControl("Rest_Name");
            HiddenField rHeaderAddress = (HiddenField)ritem.FindControl("Header_Address");
            HiddenField rHeaderABN = (HiddenField)ritem.FindControl("Header_ABN");

            ManageSession.User.R_ID = rid.Value.GetInt();
            ManageSession.User.R_Initial = rinitial.Value;
            ManageSession.User.R_Name = rname.Value;
            ManageSession.User.R_HeaderAddress = rHeaderAddress.Value;
            ManageSession.User.R_HeaderABN = rHeaderABN.Value;


            Session["R_ID"] = rid.Value;
            Session["R_Initial"] = rinitial.Value;
            Session["R_Name"] = rname.Value;

            Session["R_HeaderAddress"] = rHeaderAddress.Value;
            Session["R_HeaderABN"] = rHeaderABN.Value;

            if (Session["bckurl"] != "" && Session["bckurl"] != null)
                Server.Transfer(Session["bckurl"].ToString());
            else
                Server.Transfer("Home.aspx");
        }

       
}
}