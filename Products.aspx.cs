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

namespace PosProduct
{
    public partial class Products : System.Web.UI.Page
    {
        DB mConnection = new DB();
        DataSet ds = new DataSet();
        Common Fn = new Common();
        SQLQuery Qry = new SQLQuery();
        protected MyToolSet iTool = new MyToolSet();
        protected string categoryid = string.Empty;
        protected string subcategoryid = string.Empty;
        protected string strProductName = string.Empty;


        public Products()
        {
            //
            // TODO: Add constructor logic here
            //
            //Load += new EventHandler(Page_Load);
        }

    /*    protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }
    */

        private void InitializeComponent()
        {
            Session.Remove("scatid");
            Session.Remove("ssubcatid");
            Session.Remove("sproduct");
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            string productid = string.Empty;

            if (Session["R_ID"] == "" || Session["R_ID"] == null)
            {
                Session["bckurl"] = "Products.aspx";
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
                                        productid = iTool.decryptString(Request.QueryString["id"]);
                                        Dictionary<string, string> dict;

                                        DateTime sDate = DateTime.Now;
                                        sDate = Fn.GetCommonDate(sDate, Session["DateFormat"]);

                                        SqlParameter[] ArParams = new SqlParameter[20];
                                        ArParams[0] = new SqlParameter("@ProductName", SqlDbType.VarChar, 50);
                                        ArParams[0].Value = "";

                                        ArParams[1] = new SqlParameter("@ProductName2", SqlDbType.VarChar, 50);
                                        ArParams[1].Value = "";

                                        ArParams[2] = new SqlParameter("@ProductDesc", SqlDbType.VarChar, 3000);
                                        ArParams[2].Value = "";

                                        ArParams[3] = new SqlParameter("@ProductColor", SqlDbType.Char, 10);
                                        ArParams[3].Value = "";

                                        ArParams[4] = new SqlParameter("@GST", SqlDbType.Bit);
                                        ArParams[4].Value = false;

                                        ArParams[5] = new SqlParameter("@HasOpenPrice", SqlDbType.Bit);
                                        ArParams[5].Value = false;

                                        ArParams[6] = new SqlParameter("@ProductPrice1", SqlDbType.Decimal);
                                        ArParams[6].Value = 0;

                                        ArParams[7] = new SqlParameter("@ProductPrice2", SqlDbType.Decimal);
                                        ArParams[7].Value = 0;

                                        ArParams[8] = new SqlParameter("@StockInHand", SqlDbType.Int);
                                        ArParams[8].Value = 0;      // this field will be readonly

                                        ArParams[9] = new SqlParameter("@Status", SqlDbType.Int);
                                        ArParams[9].Value = 0;

                                        ArParams[10] = new SqlParameter("@sDate", SqlDbType.DateTime);
                                        ArParams[10].Value = sDate;

                                        ArParams[11] = new SqlParameter("@LoggedUserID", SqlDbType.Int);
                                        ArParams[11].Value = Convert.ToInt32(Session["UserID"]);

                                        ArParams[12] = new SqlParameter("@RestID", SqlDbType.Int);
                                        ArParams[12].Value = Session["R_ID"];

                                        ArParams[13] = new SqlParameter("@Mode", SqlDbType.VarChar, 20);
                                        ArParams[13].Value = "del";

                                        ArParams[14] = new SqlParameter("@CategoryID", SqlDbType.Int);
                                        ArParams[14].Value = 0;

                                        ArParams[15] = new SqlParameter("@ProductID", SqlDbType.Int);
                                        ArParams[15].Value = productid;

                                        ArParams[16] = new SqlParameter("@ProductImageWithPath", SqlDbType.VarChar, 255);
                                        ArParams[16].Value = "";

                                        ArParams[17] = new SqlParameter("@ChangePrice", SqlDbType.Bit);
                                        ArParams[17].Value = false;

                                        ArParams[18] = new SqlParameter("@SortOrder", SqlDbType.Int);
                                        ArParams[18].Value = 0;

                                        ArParams[19] = new SqlParameter("@CourseID", SqlDbType.Int);
                                        ArParams[19].Value = 0;

                                        dict = new Dictionary<string, string>() { { "Rest_ID", Session["R_ID"].ToString() } };

                                        /*                                        if (Fn.CheckRecordExists(dict, "omni_user_group", "ModifierID", modifierid))
                                                                                    Msg.Visible = true;
                                                                                else
                                                                                {
                                                                                    Msg.Visible = false; */
                                        SqlHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, "SP_omni_Product_Update", ArParams);

                                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, Qry.DeleteRec(null, "omni_Product_Modifiers", 1, "ProductID", productid));
                                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, Qry.DeleteRec(null, "omni_Product_Cooking_Options", 1, "ProductID", productid));

                                        dict = new Dictionary<string, string>() { { "OptionType", "P" } };
                                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, Qry.DeleteRec(dict, "omni_Product_Kitchen_Printer_Options", 1, "ProductID", productid));

                                        dict = new Dictionary<string, string>() { { "OptionType", "K" } };
                                        SqlHelper.ExecuteNonQuery(trans, CommandType.Text, Qry.DeleteRec(dict, "omni_Product_Kitchen_Printer_Options", 1, "ProductID", productid));
                                        trans.Commit();
                                        //}
                                    }
                                }

                                Fn.PopulateDropDown_List(Category, Qry.GetParentCategoriesSQL(Convert.ToInt32(Session["R_ID"])), "CategoryName", "CategoryID", "");
                                
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

                    Dictionary<string, string> dict1 = null;

                    if (!IsPostBack)
                    {
                        if (Session["scatid"] != "" && Session["scatid"] != null)
                        {
                            Category.SelectedValue = Session["scatid"].ToString();
                            categoryid = Session["scatid"].ToString();

                            Fn.PopulateDropDown_List(SubCategory, Qry.GetChildCategoriesSQL(Convert.ToInt32(Session["R_ID"]), Convert.ToInt32(categoryid)), "CategoryName", "CategoryID", "");
                            SubCategory.Items.Insert(0, new ListItem("", ""));
                            //SubCategory.SelectedValue = Session["ssubcatid"].ToString();
                        }

                        if (Session["ssubcatid"] != "" && Session["ssubcatid"] != null)
                        {
                            SubCategory.SelectedValue = Session["ssubcatid"].ToString();
                            subcategoryid = Session["ssubcatid"].ToString();
                        }

                        if (Session["sproduct"] != "" && Session["sproduct"] != null)
                        {
                            txtProductName.Value = Session["sproduct"].ToString();
                            strProductName = Session["sproduct"].ToString();
                        }
                    }
                    else
                    {
                        Session["scatid"] = Category.SelectedValue;
                        Session["ssubcatid"] = SubCategory.SelectedValue;
                        Session["sproduct"] = iTool.formatInputString(txtProductName.Value);

//                        categoryid = Category.SelectedValue;
//                        subcategoryid = SubCategory.SelectedValue;
//                        strProductName = iTool.formatInputString(txtProductName.Value);
                    }

                    if (categoryid !="")
                        dict1 = new Dictionary<string, string>() { { "final.ParentCategoryID", categoryid } };

                    if (subcategoryid !="")
                        dict1 = new Dictionary<string, string>() { { "final.CategoryID", subcategoryid } };

                    if (strProductName !="")
                        dict1 = new Dictionary<string, string>() { { "ProductName", strProductName } };

                    ds = Fn.LoadProducts(dict1, "Rest_ID", Session["R_ID"].ToString(), conn);
                    ProductRepeater.DataSource = ds;
                    ProductRepeater.DataBind();

                }
            }
            catch (Exception ex)
            { }        

        }

        protected void Category_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (Session["R_ID"].ToString() != "" && Category.SelectedValue.Trim() != "")
            {
                Fn.PopulateDropDown_List(SubCategory, Qry.GetChildCategoriesSQL(Convert.ToInt32(Session["R_ID"]), Convert.ToInt32(Category.SelectedValue)), "CategoryName", "CategoryID", "");
                SubCategory.Items.Insert(0, new ListItem("",""));
                SubCategory.SelectedIndex = 0;
            }
        }

        protected void ProductRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (ProductRepeater.Items.Count < 1)
            {
                if (e.Item.ItemType == ListItemType.Footer)
                {
                    Label lblFooter = (Label)e.Item.FindControl("lblEmptyData");
                    lblFooter.Visible = true;
                }
            }
        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                categoryid = Category.SelectedValue;
                subcategoryid = SubCategory.SelectedValue;
                strProductName = iTool.formatInputString(txtProductName.Value);

                Session["scatid"] =  categoryid;
                Session["ssubcatid"] = subcategoryid;
                Session["sproduct"] = strProductName;

                try
                {
                    using (SqlConnection conn = mConnection.GetConnection())
                    {
                        conn.Open();

                        try
                        {
                            Dictionary<string, string> dict = null;

                            if (categoryid.Trim()!="")
                                dict = new Dictionary<string, string>() { { "final.ParentCategoryID", categoryid } };

                            if (subcategoryid.Trim()!="")
                                dict = new Dictionary<string, string>() { { "final.CategoryID", subcategoryid } };

                            if (strProductName.Trim() != "")
                            {
                                if (categoryid.Trim() != "")
                                    dict = new Dictionary<string, string>() { { "final.ParentCategoryID", categoryid }, { "ProductName", strProductName } };
                                if (subcategoryid.Trim()!="")
                                    dict = new Dictionary<string, string>() { { "final.ParentCategoryID", categoryid }, { "final.CategoryID", subcategoryid }, { "ProductName", strProductName } };
                            }
                            ds = Fn.LoadProducts(dict, "Rest_ID", Session["R_ID"].ToString(), conn);
                            ProductRepeater.DataSource = ds;
                            ProductRepeater.DataBind();
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
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

    }
}


/*

select 
 (select ProductID from omni_Products where productname= b.ProductName and Rest_ID=5) as ProductID,
 (select OptionID from (select PrinterID as OptionID, PrinterName as OptionName,Rest_ID,'P' as OptionType from omni_Printers 
 union select KitchenId as OptionID, KitchenName as OptionName,Rest_ID,'K' as OptionType from omni_Kitchen ) as Q where Q.OptionName = c.OptionName and Rest_ID = 5 ) as OptionID,c.OptionType, 5 as Rest_ID from omni_Product_Kitchen_Printer_Options a 
 left join (select PrinterID as OptionID, PrinterName as OptionName,Rest_ID,'P' as OptionType from omni_Printers 
 union select KitchenId as OptionID, KitchenName as OptionName,Rest_ID,'K' as OptionType from omni_Kitchen ) as c on (a.OptionID = c.OptionID and a.OptionType = c.OptionType) left join 
 omni_Products b on  a.ProductID = b.ProductID 
 where a.Rest_ID=1


*/