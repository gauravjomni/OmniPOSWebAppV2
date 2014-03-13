using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.ApplicationBlocks.Data;
using MyDB;
using MyTool;
using MyQuery;
using Commons;

namespace PosCloning
{
    public partial class ProductClone : System.Web.UI.Page
    {
        DB mConnection = new DB() ;
        DataSet ds = new DataSet();
        Common Fn = new Common();
        MyToolSet iTool = new MyToolSet();
        SQLQuery Qry = new SQLQuery();
        //string sQuery = "";

        string strProductImage = "";


        public ProductClone()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            string productid = "";

            if (Session["R_Initial"] != null && Session["R_Initial"] != "")
                RestInitial.Value = Session["R_Initial"].ToString(); 

            txtPrice1.Attributes.Add("onkeyup", "copyval('" + txtPrice1.ClientID + "','" + txtPrice2.ClientID + "')");

            if (!Page.IsPostBack)
            {
                if (Session["R_ID"] == "" || Session["R_ID"] == null)
                {
                    Session["bckurl"] = "AddUser.aspx";
                    //Response.Redirect("Select_Restaurants.aspx?bckurl=" + Session["bckurl"]);
                    Server.Transfer("Select_Restaurants.aspx");
                    return;
                }

                if (Request.QueryString["prid"] != null)
                {

                    if (Request.QueryString["mode"] != null)
                        Mode.Value = "add";

                    SqlParameter[] ArParams = new SqlParameter[17];

                    productid = iTool.decryptString(Request.QueryString["prid"]);

                    ArParams[0] = new SqlParameter("@ProductID", SqlDbType.Int);
                    ArParams[0].Value = productid;

                    // @UserName Output Parameter
                    ArParams[1] = new SqlParameter("@ProductName", SqlDbType.VarChar, 50);
                    ArParams[1].Direction = ParameterDirection.Output;

                    ArParams[2] = new SqlParameter("@ProductName2", SqlDbType.VarChar, 50);
                    ArParams[2].Direction = ParameterDirection.Output;

                    ArParams[3] = new SqlParameter("@ProductDesc", SqlDbType.VarChar,5000);
                    ArParams[3].Direction = ParameterDirection.Output;

                    ArParams[4] = new SqlParameter("@ProductColor", SqlDbType.Char, 10);
                    ArParams[4].Direction = ParameterDirection.Output;

                    ArParams[5] = new SqlParameter("@GST", SqlDbType.Bit);
                    ArParams[5].Direction = ParameterDirection.Output;

                    ArParams[6] = new SqlParameter("@HasOpenPrice", SqlDbType.Bit);
                    ArParams[6].Direction = ParameterDirection.Output;

                    ArParams[7] = new SqlParameter("@ProductPrice1", SqlDbType.Decimal,7);
                    ArParams[7].Scale = 2;
                    ArParams[7].Direction = ParameterDirection.Output;

                    ArParams[8] = new SqlParameter("@ProductPrice2", SqlDbType.Decimal,7);
                    ArParams[8].Direction = ParameterDirection.Output;
                    ArParams[8].Scale = 2;

                    ArParams[9] = new SqlParameter("@StockInHand", SqlDbType.Int);
                    ArParams[9].Direction = ParameterDirection.Output;

                    ArParams[10] = new SqlParameter("@Status", SqlDbType.Int);
                    ArParams[10].Direction = ParameterDirection.Output;

                    ArParams[11] = new SqlParameter("@CategoryID", SqlDbType.Int);
                    ArParams[11].Direction = ParameterDirection.Output;

                    ArParams[12] = new SqlParameter("@ParentID", SqlDbType.Int);
                    ArParams[12].Direction = ParameterDirection.Output;

                    ArParams[13] = new SqlParameter("@ProductImagePath", SqlDbType.VarChar,255);
                    ArParams[13].Direction = ParameterDirection.Output;

                    ArParams[14] = new SqlParameter("@ChangePrice", SqlDbType.Bit);
                    ArParams[14].Direction = ParameterDirection.Output;

                    ArParams[15] = new SqlParameter("@SortOrder", SqlDbType.Int);
                    ArParams[15].Direction = ParameterDirection.Output;

                    ArParams[16] = new SqlParameter("@CourseID", SqlDbType.Int);
                    ArParams[16].Direction = ParameterDirection.Output;

                    try
                    {
                        // Call ExecuteNonQuery static method of SqlHelper class
                        // We pass in database connection string, command type, stored procedure name and an array of SqlParameter objects
                        SqlHelper.ExecuteNonQuery(mConnection.GetConnection(), CommandType.StoredProcedure, "getProductDetails", ArParams);

                        // Display results in text box using the values of output parameters	
                        txtProductName.Value = ArParams[1].Value.ToString();
                        txtProductName2.Value = ArParams[2].Value.ToString();
                        txtProductDesc.Value = ArParams[3].Value.ToString();
                        txtColor.Text = ArParams[4].Value.ToString();
                        ChkGST.Checked = ArParams[5].Value.ToString() == "True" ? true : false;
                        ChkOpenPrice.Checked = ArParams[6].Value.ToString() == "True" ? true : false;
                        txtPrice1.Value = String.Format("{0:0.00}",ArParams[7].Value);
                        txtPrice2.Value = String.Format("{0:n2}", ArParams[8].Value);
                        
                        txtInHand.Value = ArParams[9].Value.ToString();
                        Status.Checked = ArParams[10].Value.ToString() == "1" ? true : false;
                        SubCategory.SelectedValue = ArParams[11].Value.ToString();
                        Category.SelectedValue = ArParams[12].Value.ToString();
                        Product_Img.Value = ArParams[13].Value.ToString();
                        ChkChangePrice.Checked = ArParams[14].Value.ToString() == "True" ? true : false;
                        txtSort.Value = ArParams[15].Value.ToString();

                        if (ArParams[16].Value.ToString() != "-1")
                            Course.SelectedValue = ArParams[16].Value.ToString();

                        ProductID.Value = productid;

                        if (Product_Img.Value.Trim() != "")
                            Prdt_Img.ImageUrl = Product_Img.Value;

                        if (ArParams[12].Value.ToString() != "")
                            populateChildCategory(Convert.ToInt32(ArParams[12].Value));


                    }
                    catch (Exception ex)
                    {
                        // throw an exception
                        throw ex;
                    }

                    //                    sQuery = "selinsert into omni_user_group(UserGroupName,CreateDate,IsActive) " +
                    //                    " values(@usrgrpname,@CreateDate,@status)";
                }

                //mConnection.PopulateDropDown_List(UserGroup, Qry.GetUserGroupSQL(), "UserGroupName", "UserGroupID", "");

                if (Session["R_ID"].ToString() != "")
                {
                    Dictionary<string, string> dict;

                    Fn.PopulateDropDown_List(Category, Qry.GetParentCategoriesSQL(Convert.ToInt32(Session["R_ID"])), "CategoryName", "CategoryID", "");

                    dict = new Dictionary<string, string>() { { "IsActive", "1" } };
                    Fn.PopulateListBox(Printer, Qry.getPrinterSQL(dict, "Rest_ID", Session["R_ID"].ToString()), "PrinterName", "PrinterID", "");

                    dict = new Dictionary<string, string>() { { "IsActive", "1" } };
                    Fn.PopulateDropDown_List(Kitchen, Qry.getKitchenSQL(dict,"Rest_ID",Session["R_ID"].ToString()), "KitchenName", "KitchenID", "");

                    //dict = new Dictionary<string, string>() { { "IsActive", "1" } };
                    //Fn.PopulateDropDown_List(Course, Qry.GetCoursesSQL(dict,"Rest_ID",Session["R_ID"].ToString()), "CourseName", "CourseID", "");
                    Fn.PopulateDropDown_List(Course, Qry.GetCoursesSQL(Session["R_ID"].ToString()) , "CourseName", "CourseID", "");

                    ds = Fn.LoadModifiers(Convert.ToInt32(Session["R_ID"]));
                    ModfRepeater.DataSource = ds;
                    ModfRepeater.DataBind();

                    ds = Fn.LoadCookingOptions(Convert.ToInt32(Session["R_ID"]));
                    CookingRepeater.DataSource = ds;
                    CookingRepeater.DataBind();

                }

                if (Mode.Value == "add")
                {
                    SqlParameter[] ArParamsPrinterOpt = new SqlParameter[3];

                    foreach (ListItem item in Printer.Items)
                    {
                        ArParamsPrinterOpt[0] = new SqlParameter("@ProductID", SqlDbType.Int);
                        ArParamsPrinterOpt[0].Value = productid;

                        ArParamsPrinterOpt[1] = new SqlParameter("@PrinterID", SqlDbType.Int);
                        ArParamsPrinterOpt[1].Value = item.Value;

                        ArParamsPrinterOpt[2] = new SqlParameter("@flag", SqlDbType.Bit);
                        ArParamsPrinterOpt[2].Direction = ParameterDirection.Output;

                        SqlHelper.ExecuteNonQuery(mConnection.GetConnection(), CommandType.StoredProcedure, "getProduct_Printer_Option", ArParamsPrinterOpt);

                        if ((bool)ArParamsPrinterOpt[2].Value == true)
                            item.Selected = true;
                    }

                    SqlParameter[] ArParamsKitchenOpt = new SqlParameter[2];
                    ArParamsKitchenOpt[0] = new SqlParameter("@ProductID", SqlDbType.Int);
                    ArParamsKitchenOpt[0].Value = productid;

                    ArParamsKitchenOpt[1] = new SqlParameter("@KitchenID", SqlDbType.Int);
                    ArParamsKitchenOpt[1].Direction = ParameterDirection.Output;

                    SqlHelper.ExecuteNonQuery(mConnection.GetConnection(), CommandType.StoredProcedure, "getProduct_Kitchen_Option", ArParamsKitchenOpt);

                    Kitchen.SelectedValue = ArParamsKitchenOpt[1].Value.ToString();

                    //                    Printer.SelectedValue = ArParamsOpt[2].Value.ToString();

                    foreach (RepeaterItem Item in ModfRepeater.Items)
                    {
                        HtmlInputCheckBox chkModifier = (HtmlInputCheckBox)Item.FindControl("ChkModifier");
                        //ArParamsT[1].Value = chkModifier.Value;

                        SqlParameter[] ArParamsT = new SqlParameter[3];
                        ArParamsT[0] = new SqlParameter("@ProductID", SqlDbType.Int);
                        ArParamsT[0].Value = productid;

                        ArParamsT[1] = new SqlParameter("@ModifierID", SqlDbType.Int);
                        ArParamsT[1].Value = chkModifier.Value;

                        ArParamsT[2] = new SqlParameter("@flag", SqlDbType.Bit);
                        ArParamsT[2].Direction = ParameterDirection.Output;

                        SqlHelper.ExecuteNonQuery(mConnection.GetConnection(), CommandType.StoredProcedure, "getProductModifiers", ArParamsT);


                        if ((bool)ArParamsT[2].Value == true)
                            chkModifier.Checked = true;
                    }

                    foreach (RepeaterItem Item in CookingRepeater.Items)
                    {
                        HtmlInputCheckBox ChkOpt = (HtmlInputCheckBox)Item.FindControl("ChkCookingOpt");
                        //ArParamsT[1].Value = chkModifier.Value;

                        SqlParameter[] ArParamsT = new SqlParameter[3];
                        ArParamsT[0] = new SqlParameter("@ProductID", SqlDbType.Int);
                        ArParamsT[0].Value = productid;

                        ArParamsT[1] = new SqlParameter("@OptionID", SqlDbType.Int);
                        ArParamsT[1].Value = ChkOpt.Value;

                        ArParamsT[2] = new SqlParameter("@flag", SqlDbType.Bit);
                        ArParamsT[2].Direction = ParameterDirection.Output;

                        SqlHelper.ExecuteNonQuery(mConnection.GetConnection(), CommandType.StoredProcedure, "getProductCookingOptions", ArParamsT);


                        if ((bool)ArParamsT[2].Value == true)
                            ChkOpt.Checked = true;
                    }

                }

            }

            if (Mode.Value == "add")
            {
                txtSort.Value = System.Configuration.ConfigurationSettings.AppSettings["DefaultSortOrder"];
                txtPrice1.Value = System.Configuration.ConfigurationSettings.AppSettings["DefaultPriceValue"];
                txtPrice2.Value = System.Configuration.ConfigurationSettings.AppSettings["DefaultPriceValue"];
            }
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                bool flag1 = false;
                bool flag2 = false;
                bool flag3 = false;
                int product_id = 0;

                string strProductName = iTool.formatInputString(txtProductName.Value);
                string strProductName2 = iTool.formatInputString(txtProductName2.Value);
                string strProductDesc = iTool.formatInputString(txtProductDesc.Value);
                string strProductColor = iTool.formatInputString(txtColor.Text);

                string strProductPrice1 = iTool.formatInputString(txtPrice1.Value);
                string strProductPrice2 = iTool.formatInputString(txtPrice2.Value);
                string strStockInHand = iTool.formatInputString(txtInHand.Value);
                string targetProductImgPath = System.Configuration.ConfigurationSettings.AppSettings["ProductImagePath"];

                string strKitchenID = Kitchen.SelectedValue;
                string strPrinterID = Printer.SelectedValue;
                string strSortOrder = iTool.formatInputString(txtSort.Value);

                strProductImage = txtImage.PostedFile.FileName;

                DateTime sDate = DateTime.Now;
                sDate = Fn.GetCommonDate(sDate, Session["DateFormat"]);

                SqlParameter[] ArParams = new SqlParameter[20];
                ArParams[0] = new SqlParameter("@ProductName", SqlDbType.VarChar, 50);
                ArParams[0].Value = strProductName;

                ArParams[1] = new SqlParameter("@ProductName2", SqlDbType.VarChar, 50);
                ArParams[1].Value = strProductName2;

                ArParams[2] = new SqlParameter("@ProductDesc", SqlDbType.VarChar,3000);
                ArParams[2].Value = strProductDesc;

                ArParams[3] = new SqlParameter("@ProductColor", SqlDbType.Char,10);
                ArParams[3].Value = strProductColor;

                ArParams[4] = new SqlParameter("@GST", SqlDbType.Bit);
                ArParams[4].Value = ChkGST.Checked ? true : false;

                ArParams[5] = new SqlParameter("@HasOpenPrice", SqlDbType.Bit);
                ArParams[5].Value =  ChkOpenPrice.Checked ? true : false;

                ArParams[6] = new SqlParameter("@ProductPrice1", SqlDbType.Decimal);
                ArParams[6].Value = (strProductPrice1=="") ? 0 : Convert.ToDecimal(strProductPrice1);

                ArParams[7] = new SqlParameter("@ProductPrice2", SqlDbType.Decimal);
                ArParams[7].Value = (strProductPrice2 == "") ? 0 : Convert.ToDecimal(strProductPrice2);

                ArParams[8] = new SqlParameter("@StockInHand", SqlDbType.Int);
                //ArParams[8].Value = strStockInHand;
                ArParams[8].Value = 0;      // this field will be readonly

                ArParams[9] = new SqlParameter("@Status", SqlDbType.Int);
                ArParams[9].Value = Status.Checked ? 1 : 0;

                ArParams[10] = new SqlParameter("@sDate", SqlDbType.DateTime);
                ArParams[10].Value = sDate;

                ArParams[11] = new SqlParameter("@LoggedUserID", SqlDbType.Int);
                ArParams[11].Value = Convert.ToInt32(Session["UserID"]);

                ArParams[12] = new SqlParameter("@RestID", SqlDbType.Int);
                ArParams[12].Value = Session["R_ID"];

                ArParams[13] = new SqlParameter("@Mode", SqlDbType.VarChar,20);
                ArParams[13].Value = Mode.Value;

                ArParams[14] = new SqlParameter("@CategoryID", SqlDbType.Int);
                ArParams[14].Value = SubCategory.SelectedValue;

                ArParams[15] = new SqlParameter("@ProductID", SqlDbType.Int);
                ArParams[15].Value = -1;

                if (strProductImage != "")
                {
                    txtImage.PostedFile.SaveAs(Server.MapPath("/product_images") + strProductImage);
                    strProductImage = "~/" + targetProductImgPath + strProductImage;

                    if (Product_Img.Value.Trim() != "")
                        File.Delete(Product_Img.Value.Trim());
                }
                else
                    strProductImage = Product_Img.Value;

                ArParams[16] = new SqlParameter("@ProductImageWithPath", SqlDbType.VarChar,255);
                ArParams[16].Value =  strProductImage;

                ArParams[17] = new SqlParameter("@ChangePrice", SqlDbType.Bit);
                ArParams[17].Value = ChkChangePrice.Checked ? true : false; 

                ArParams[18] = new SqlParameter("@SortOrder", SqlDbType.Int);
                ArParams[18].Value = strSortOrder;

                ArParams[19] = new SqlParameter("@CourseID", SqlDbType.Int);
                ArParams[19].Value = (Course.SelectedValue == "" || Course.SelectedValue == null) ? "-1" : Course.SelectedValue;

                Dictionary<string, string> dict;

                if (ProductID.Value == "-1" && Mode.Value=="add")
                {
                    //dict = null;
                    dict = new Dictionary<string, string>() { { "Rest_ID", Session["R_ID"].ToString() } };
                    flag1 = Fn.CheckRecordExists(dict, "omni_Products", "ProductName", "ProductName", "", "", "", ArParams);
                }
                else
                {
                    //========= FOR IMPLEMENTING MORE "AND" CLAUSE IN SQL OPERATION ================//

                    //dict = new Dictionary<string, string>() { { "UserGroupId", GroupID.Value } };
                    //flag = Fn.CheckRecordExists(dict, "omni_user_group", "UserGroupName", "UserGroupName", "edit", "UserGroupId", GroupID.Value, ArParams);

                    //=============================================================================//

                    flag1 = Fn.CheckRecordExists(null, "omni_Products", "ProductName", "ProductName", "edit", "ProductID", ProductID.Value, ArParams);
                }

                if (flag1 == true)
                {
                   LblProductName.Text = "Product Name already exist. Try with another one.";
                   return;
                }

                if (flag2 == true)
                {
                    //LblEmail.Text = "Email already exist. Try with another one.";
                    return;
                }

                if (flag3 == true)
                {
                    //LblUserPin.Text = "User Pin is already assigned. Try with another one.";
                    return;
                }


                //if (txtPassword.Value !="" && txtPasswordConf.Value !="")
                //{
                //    if (txtPassword.Value != txtPasswordConf.Value){
                //        LblPasswordConf.Text = "Password & Confirm Password should be the same.";
                //            return;}
                //}


                using (SqlConnection conn = mConnection.GetConnection())
                {
                    conn.Open();

                    using (SqlTransaction trans = conn.BeginTransaction())
                    {
                        // Establish command parameters
                        // @AccountNo (From Account)
                        //SqlParameter paramFromAcc = new SqlParameter("@usrgrpname", SqlDbType.VarChar,50);
                        //paramFromAcc.Value = "12345";

                        try
                        {
                            // Call ExecuteNonQuery static method of SqlHelper class for debit and credit operations.
                            // We pass in SqlTransaction object, command type, stored procedure name, and a comma delimited list of SqlParameters

                            // Perform the debit operation
                            //SqlHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, "Debit", paramFromAcc, paramDebitAmount);
                            //SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sQuery, ArParams);
                            SqlHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, "SP_omni_Product_Update", ArParams);

                            if (Mode.Value == "add")
                                product_id = (int)SqlHelper.ExecuteScalar(trans, CommandType.Text, Qry.getLastInsertedID(null, "omni_Products", "productid", 1, "", ""));
                            else
                                product_id = Convert.ToInt32(ProductID.Value);

                            SqlParameter[] ArParamsT = new SqlParameter[3];

                            if (Mode.Value == "edit")
                            {
                                SqlHelper.ExecuteNonQuery(trans, CommandType.Text, Qry.DeleteRec(null, "omni_Product_Modifiers", 1, "ProductID", product_id.ToString()));
                                SqlHelper.ExecuteNonQuery(trans, CommandType.Text, Qry.DeleteRec(null, "omni_Product_Cooking_Options", 1, "ProductID", product_id.ToString()));

                                dict = new Dictionary<string, string>() { { "OptionType", "P"  } };
                                SqlHelper.ExecuteNonQuery(trans, CommandType.Text, Qry.DeleteRec(dict, "omni_Product_Kitchen_Printer_Options", 1, "ProductID", product_id.ToString()));
                            }

                            foreach (RepeaterItem Item in ModfRepeater.Items)
                            {
                                HtmlInputCheckBox chkModifier = (HtmlInputCheckBox)Item.FindControl("ChkModifier");

                                if (chkModifier.Checked)
                                {
                                    ArParamsT[0] = new SqlParameter("@ProductID", SqlDbType.Int);
                                    ArParamsT[0].Value = product_id;

                                    ArParamsT[1] = new SqlParameter("@ModifierID", SqlDbType.Int);
                                    ArParamsT[1].Value = chkModifier.Value;

                                    ArParamsT[2] = new SqlParameter("@Mode", SqlDbType.Char, 10);
                                    ArParamsT[2].Value = Mode.Value;
                                    SqlHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, "SP_omni_Product_Options_Update", ArParamsT);
                                }
                            }

                            foreach (RepeaterItem Item in CookingRepeater.Items)
                            {
                                HtmlInputCheckBox chkCookOpt = (HtmlInputCheckBox)Item.FindControl("ChkCookingOpt");

                                if (chkCookOpt.Checked)
                                {
                                    ArParamsT[0] = new SqlParameter("@ProductID", SqlDbType.Int);
                                    ArParamsT[0].Value = product_id;

                                    ArParamsT[1] = new SqlParameter("@OptionID", SqlDbType.Int);
                                    ArParamsT[1].Value = chkCookOpt.Value;

                                    ArParamsT[2] = new SqlParameter("@Mode", SqlDbType.Char, 10);
                                    ArParamsT[2].Value = Mode.Value;
                                    SqlHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, "SP_omni_Product_Cooking_Options_Update", ArParamsT);
                                }
                            }

/*                            SqlParameter[] ArParamsOpt = new SqlParameter[4];

                            ArParamsOpt[0] = new SqlParameter("@KitchenID", SqlDbType.Int);
                            ArParamsOpt[0].Value = strKitchenID;

                            ArParamsOpt[1] = new SqlParameter("@PrinterID", SqlDbType.Int);
                            ArParamsOpt[1].Value = strPrinterID;

                            ArParamsOpt[2] = new SqlParameter("@Mode", SqlDbType.VarChar,10);
                            ArParamsOpt[2].Value = Mode.Value;

                            ArParamsOpt[3] = new SqlParameter("@ProductID", SqlDbType.Int);
                            ArParamsOpt[3].Value = product_id;

                            SqlHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, "SP_omni_Product_Kitchen_Printer_Options_Update", ArParamsOpt);
*/

                  // ==================  For Multiple Printer Assigned For Each Product ===================/

                            SqlParameter[] ArParamsPrinterOpt = new SqlParameter[2];

                            foreach (ListItem item in Printer.Items)
                            {
                                if (item.Selected)
                                {
                                    ArParamsPrinterOpt[0] = new SqlParameter("@PrinterID", SqlDbType.Int);
                                    ArParamsPrinterOpt[0].Value = item.Value;

                                    ArParamsPrinterOpt[1] = new SqlParameter("@ProductID", SqlDbType.Int);
                                    ArParamsPrinterOpt[1].Value = product_id;

                                    SqlHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, "SP_omni_Product_Printer_Options_Update", ArParamsPrinterOpt);
                                }
                            }

                  // ======================================================================================/  

                 // ==================   Kitchen Assigned For Each Product ===================/
                            SqlParameter[] ArParamsKitchenOpt = new SqlParameter[3];

                            ArParamsKitchenOpt[0] = new SqlParameter("@KitchenID", SqlDbType.Int);
                            ArParamsKitchenOpt[0].Value = strKitchenID;

                            ArParamsKitchenOpt[1] = new SqlParameter("@Mode", SqlDbType.VarChar, 10);
                            ArParamsKitchenOpt[1].Value = Mode.Value;

                            ArParamsKitchenOpt[2] = new SqlParameter("@ProductID", SqlDbType.Int);
                            ArParamsKitchenOpt[2].Value = product_id;

                            SqlHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, "SP_omni_Product_Kitchen_Options_Update", ArParamsKitchenOpt);

                    // ======================================================================================/  

                            trans.Commit();
                            //txtResults.Text = "Transfer Completed";

                        }

                        catch (Exception ex)
                        {
                            // throw exception						
                            trans.Rollback();
                            //txtResults.Text = "Transfer Error";
                            throw ex;
                        }

                        finally
                        {
                            conn.Close();
                        }
                    }
                }



                //SqlHelper.ExecuteNonQuery(mConnection.GetConnection(), CommandType.Text, sQuery, ArParams);

            }
            catch(Exception ex)
            {

           // CreateLogFiles.ErrorLog(ex.Message.ToString() & " - " & System.IO.Path.GetFileName(System.Web.HttpContext.Current.Request.Url.AbsolutePath) & " - btnCreate_Click")
           // lblError.Text = "Error - Please contact Administrator "
           // Exit Sub
            }
                Response.Redirect("Products.aspx");
        }

        protected void Category_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Session["R_ID"].ToString() != "" && Category.SelectedValue.Trim() !="")
                Fn.PopulateDropDown_List(SubCategory, Qry.GetChildCategoriesSQL(Convert.ToInt32(Session["R_ID"]),Convert.ToInt32(Category.SelectedValue)), "CategoryName", "CategoryID", "");

        }

        protected void populateChildCategory(int val)
        {
            if (Session["R_ID"].ToString() != "" && val >0)
                Fn.PopulateDropDown_List(SubCategory, Qry.GetChildCategoriesSQL(Convert.ToInt32(Session["R_ID"]), val), "CategoryName", "CategoryID", "");
        }

        protected void Repeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HtmlInputCheckBox chk = (HtmlInputCheckBox)e.Item.FindControl("ChkModifier");
                chk.Value = ds.Tables[0].Rows[e.Item.ItemIndex]["ModifierID"].ToString();
            }
        }

        protected void CookingRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HtmlInputCheckBox chk = (HtmlInputCheckBox)e.Item.FindControl("ChkCookingOpt");
                chk.Value = ds.Tables[0].Rows[e.Item.ItemIndex]["OptionID"].ToString();
            }
        }        
    }
}