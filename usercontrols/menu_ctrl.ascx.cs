using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
//using Microsoft.ApplicationBlocks.Data;
using MyDB;
using Commons;

namespace MenuControl
{
    public partial class Menus : System.Web.UI.UserControl
    {
        DB mConnection = new DB();
        public string sPageName = "";
        public string HitMenuID = "";
        Common Fn = new Common();
        DataSet ds = new DataSet();


        public Menus()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            // do the bartman
            
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            //DB mConnection = new DB();

            string sPagePath = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
            System.IO.FileInfo oFileInfo = new System.IO.FileInfo(sPagePath);
            sPageName = oFileInfo.Name;

            Dictionary<string, string> dict;

            //HitMenuID = Fn.GetTableColumnValue(null, "omni_Modules", "ParentMenuID", "NavigateUrl", sPageName);


//            string cstext1 = "";

            //switch (sPageName)
            //{
            //    case "User.aspx":
            //        cstext1 = "<script type=text/javascript>showcurrent('pages','1','dashboard','blog') </script>";
            //        break;
            //    case "add_page.aspx":
            //        cstext1 = "<script type=text/javascript>showcurrent('pages','1','dashboard','blog') </script>";
            //        break;
            //    case "add_post.aspx":
            //        cstext1 = "<script type=text/javascript>showcurrent('blog','1','dashboard','pages') </script>";
            //        break;
            //    case "manage_posts.aspx":
            //        cstext1 = "<script type=text/javascript>showcurrent('blog','1','dashboard','pages') </script>";
            //        break;
            //    case "login_home.aspx":
            //        cstext1 = "<script type=text/javascript>showcurrent('dashboard','1','blog','pages') </script>";
            //        break;
            //}


            if (Session["UserID"] == null)
                Response.Redirect("Default.aspx");
            else
                LoggedUserName.Text = Session["UserName"].ToString();

            using (SqlConnection conn = mConnection.GetConnection())
            {
                conn.Open();

                try
                {
                    HitMenuID = Fn.GetTableColumnValue(null, "omni_Modules", "ParentMenuID", "NavigateUrl", sPageName, conn);

                    Fn.LoadAppModules(0, ds, "P_omni_Modules", "GP", Convert.ToInt32(Session["UserGroupID"]),conn);
                    Fn.LoadAppModules(1, ds, "C_omni_Modules", "GP", Convert.ToInt32(Session["UserGroupID"]),conn);

                    // if (Session["R_ID"] != null && Session["R_ID"] != "")
                    // {
                    ds.Relations.Add("ParentChild", ds.Tables["P_omni_Modules"].Columns["MenuID"], ds.Tables["C_omni_Modules"].Columns["ParentMenuID"], false);
                    // }
                    parentRepeater.DataSource = ds.Tables["P_omni_Modules"];
                    parentRepeater.DataBind();
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

        protected void parentRepeater_ItemCreated(object sender, RepeaterItemEventArgs e)
        {

            foreach (RepeaterItem item in parentRepeater.Items)
            {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    //HtmlAnchor HAParent = (HtmlAnchor)item.FindControl("Menu");
                    HiddenField HMenu = (HiddenField)item.FindControl("HMenu");

                    if (!IsPostBack)
                    {
                        DataRow[] result = ds.Tables["C_omni_Modules"].Select("ParentMenuId='3'");
                        Repeater Child = (Repeater)item.FindControl("childRepeater");


                        //foreach (DataRow row in result)
                        //{
                        //    TextBox HF = (TextBox)Child.FindControl("IsLocationBased");
                        //Response.Write("Parijat->" + HF.Text);
                        //    HtmlAnchor HAChild = (HtmlAnchor)Child.FindControl("SubMenu");
                        //    //HyperLink  = (HyperLink)Child.FindControl("SubMenu");
                        //    HAChild.HRef = row["NavigateURL"].ToString();
                        //    //.Response.Write("Parijat->: " + row["NavigateURL"].ToString());
                        //}


                        foreach (RepeaterItem subitem in Child.Items)
                        {
                            //HyperLink HA = (HyperLink)subitem.FindControl("SubMenu"); 
                            HtmlAnchor HA = (HtmlAnchor)subitem.FindControl("SubMenu");
                            HiddenField HF = (HiddenField)subitem.FindControl("IsLocationBased");
                            HiddenField HF1 = (HiddenField)subitem.FindControl("IsMenuShownWithinLocationLevel");

/*                            if (HA.HRef.ToString() == ("~/" + sPageName))
                                HA.Attributes.Add("class", "current");
                            else
                                HA.Attributes.Add("class", ""); */

                            if (HF.Value == "1")
                            {
                                if (Session["R_ID"] == null || Session["R_ID"].ToString() == "")
                                    HA.HRef = "~/Notification.aspx";
                            }
                            else
                            {
                                if (HttpContext.Current.Session["R_ID"] != "" && HF1.Value=="0")
                                {
                                    HA.Attributes.Add("onclick", "showWarning('" + HA.HRef + "');");
                                    HA.Style.Add("cursor", "pointer");
                                    HA.HRef = "";
                                }
                            }
                            // //   HtmlAnchor HAChild = (HtmlAnchor)subitem.FindControl("SubMenu");
                            //   // Response.Write(HAChild.HRef);

                            ////    //if (HAChild.HRef.Trim() == sPageName.Trim())
                            ////    //    HAChild.Attributes.Add("class", "current");
                        }
                    }
                }
            }

        }

        //protected void parentRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        //{
        //    if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        //    {
        //        DataRowView dv = e.Item.DataItem as DataRowView;

        //        if (dv != null)
        //        {
        //            HtmlAnchor ha = (HtmlAnchor)e.Item.FindControl("SubMenu");
        //            //ha.HRef = dv.Row[0].ToString();
        //        }
        //        //ha.Attributes.Add("onclick", "javascript:setrestaurant()");

        //        //Format label text as required
        //    }
        //}
    }

    
}