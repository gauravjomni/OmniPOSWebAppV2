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

namespace PosLocation
{
    public partial class CloneLocation : System.Web.UI.Page
    {
        DB mConnection = new DB() ;
        DataSet ds = new DataSet();
        Common Fn = new Common();
        MyToolSet iTool = new MyToolSet();
        SQLQuery Qry = new SQLQuery();
        //string sQuery = "";

        string strProductImage = "";


        public CloneLocation()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            Fn.switchingbeteenlocation2company(true);

            if (!Page.IsPostBack)
            {
                //if (Session["R_ID"] == "" || Session["R_ID"] == null)
                //{
                //    Session["bckurl"] = "AddUser.aspx";
                //    Server.Transfer("Select_Restaurants.aspx");
                //    return;
                //}

                Fn.PopulateListBox(FromLoation, Qry.GetRestaurantSQL(""), "RestName", "Rest_ID", "");
                Fn.PopulateListBox(ToLocation, Qry.GetRestaurantSQL(""), "RestName", "Rest_ID", "");

            }
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string CloneLocationLastUpDate = string.Empty;
                bool flag = false;
                string errmsg = string.Empty;
                string copyModule = string.Empty;
                string srclocation = FromLoation.SelectedValue;
                string trgtlocation = ToLocation.SelectedValue;

                LblError.Text = "";
                //DateTime sDate = DateTime.Now;
                //sDate = Fn.GetCommonDate(sDate, Session["DateFormat"]);

                if (FromLoation.SelectedValue.ToString() != "" && ToLocation.SelectedValue.ToString() != "" && (FromLoation.SelectedValue== ToLocation.SelectedValue))
                {
                    //flag1 = true;
                    flag = true;
                    errmsg = "Source Location & Destination Location Should Not Be The Same!!!";
                }

                else
                {
                    if (ChkCookingOption.Checked == false && ChkKitchen.Checked == false && ChkPrinter.Checked == false &&
                        ChkCourse.Checked == false && ChkModifierLevel.Checked == false && ChkModifier.Checked == false &&
                        ChkCategory.Checked == false && ChkSubCategory.Checked == false && ChkProduct.Checked == false)
                    {
                        flag = true;
                        errmsg = "Error In Copying!!!. Please Select Any Option Either From (Management General) Level Or Management (Others) Level";
                    }

                    if (ChkModifier.Checked)
                    {
                        if (Fn.CheckRecordCountForCloning("omni_Modifiers_Level", "ModifierLevelName", srclocation, trgtlocation, "") == false)
                        {
                            //flag2 = true;
                            flag = true;
                            errmsg = "Error In Copying!!!. No Modifier Level Found In The Target Location So First Try To Copy Modifier Level.";
                        }
                    }

                    if (ChkSubCategory.Checked)
                    {
                        if (Fn.CheckRecordCountForCloning("omni_Item_Categories", "CategoryName", srclocation, trgtlocation, "") == false)
                        {
                            //                        flag3 = true;
                            flag = true;
                            errmsg = "Error In Copying!!!. No Category Found In The Target Location So First Try To Copy Category.";
                        }
                    }

                    /* if (ChkCourse.Checked)
                     {
                         if (Fn.CheckRecordCountForCloning("omni_Courses", "CourseName", srclocation, trgtlocation,"") == false)
                         {
                             flag = true;
                             errmsg = "Error In Copying!!!. No Course Found In The Target Location So First Try To Copy Course.";
                         }
                     }*/



                    if (ChkProduct.Checked == true)
                    {
                        if (Fn.CheckRecordCountForCloning("omni_Cooking_Options", "OptionName", srclocation, trgtlocation, "") == false)
                        {
                            //flag4 = true;
                            flag = true;
                            errmsg = "Error In Copying!!!. No Cooking Option Found In The Target Location So First Try To Copy Cooking Option.";
                        }
                        else if (Fn.CheckRecordCountForCloning("omni_Kitchen", "KitchenName", srclocation, trgtlocation, "") == false)
                        {
                            //   flag5 = true;
                            flag = true;
                            errmsg = "Error In Copying!!!. No Kitchen Found In The Target Location So First Try To Copy Kitchen.";
                        }
                        else if (Fn.CheckRecordCountForCloning("omni_Printers", "PrinterName", srclocation, trgtlocation, "") == false)
                        {
                            //   flag6 = true;
                            flag = true;
                            errmsg = "Error In Copying!!!. No Printer Found In The Target Location So First Try To Copy Printer.";
                        }
                        else if (Fn.CheckRecordCountForCloning("omni_Modifiers_Level", "ModifierLevelName", srclocation, trgtlocation, "") == false)
                        {
                            flag = true;
                            errmsg = "Error In Copying!!!. No Modifier Level Found In The Target Location So First Try To Copy Modifier Level.";
                        }
                        else if (Fn.CheckRecordCountForCloning("omni_Modifiers", "ModifierName", srclocation, trgtlocation, "") == false)
                        {
                            flag = true;
                            errmsg = "Error In Copying!!!. No Modifier Found In The Target Location So First Try To Copy Modifier.";
                        }
                        else if (Fn.CheckRecordCountForCloning("omni_Item_Categories", "CategoryName", srclocation, trgtlocation, " and parentid=0") == false)
                        {
                            flag = true;
                            errmsg = "Error In Copying!!!. No Category Found In The Target Location So First Try To Copy Category.";
                        }
                        else if (Fn.CheckRecordCountForCloning("omni_Item_Categories", "CategoryName", srclocation, trgtlocation, " and parentid>0") == false)
                        {
                            flag = true;
                            errmsg = "Error In Copying!!!. No SubCategory Found In The Target Location So First Try To Copy SubCategory.";
                        }
                        else if (Fn.CheckRecordCountForCloning("omni_Courses", "CourseName", srclocation, trgtlocation, "") == false)
                        {
                            flag = true;
                            errmsg = "Error In Copying!!!. No Course Found In The Target Location So First Try To Copy Course.";
                        }
                    }
                }
                if (flag == true)
                {
                    LblError.Text = errmsg;
                    return;
                }

/*              if (flag1 == true)
                {
                    LblError.Text = "Source Location & Destination Location Should Not Be The Same!!!";
                    return;
                }

                if (flag2 == true)
                {
                    LblError.Text = "Error In Copying!!!. No Modifier Level Found In The Target Location So First Try To Copy Modifier Level.";
                    return;
                }

                if (flag3 == true)
                {
                    LblError.Text = "Error In Copying!!!. No Category Found In The Target Location So First Try To Copy Category.";
                    return;
                }
*/

                /*if (flag2 == true)
                {
                    LblError.Text = "Since Product depends On Modifier, Category/SubCategory So For Copying Product You Have To Select All The Above Options[ModifierLevel,Modifier,Category/SubCategory] Source Location & Destination Location Should Not Be The Same!!!";
                    return;
                }*/

                using (SqlConnection conn = mConnection.GetConnection())
                {
                    conn.Open();

                    using (SqlTransaction trans = conn.BeginTransaction())
                    {
                        try
                        {

                            SqlParameter[] ArParams = new SqlParameter[4];

                            DateTime sDate = DateTime.Now;
                            sDate = Fn.GetCommonDate(sDate, Session["DateFormat"]);

                            ArParams[0] = new SqlParameter("@SourceRestID", SqlDbType.Int);
                            ArParams[0].Value = srclocation;

                            ArParams[1] = new SqlParameter("@TargetRestID", SqlDbType.Int);
                            ArParams[1].Value = trgtlocation;

                            ArParams[2] = new SqlParameter("@CreatedByUserID", SqlDbType.Int);
                            ArParams[2].Value = Session["UserID"];

                            ArParams[3] = new SqlParameter("@CreatedOn", SqlDbType.DateTime);
                            ArParams[3].Value =sDate;
                            
                            if (ChkCookingOption.Checked)
                            {
                                SqlHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, "SP_omni_CookingOption_Clone", ArParams);
                                copyModule = "Cooking Option";
                            }
                            else if (ChkKitchen.Checked)
                            {
                                SqlHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, "SP_omni_Kitchen_Clone", ArParams);
                                copyModule = "Kitchen";
                            }
                            else if (ChkPrinter.Checked)
                            {
                                SqlHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, "SP_omni_Printer_Clone", ArParams);
                                copyModule = "Printer";
                            }
                            else if (ChkModifierLevel.Checked)
                            {
                                SqlHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, "SP_omni_ModifierLevel_Clone", ArParams);
                                copyModule = "ModifierLevel";
                            }
                            else if (ChkModifier.Checked)
                            {
                                SqlHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, "SP_omni_Modifier_Clone", ArParams);
                                copyModule = "Modifier";
                            }
                            else if (ChkCategory.Checked)
                            {
                                SqlHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, "SP_omni_Category_Clone", ArParams);
                                copyModule = "Category";
                            }
                            else if (ChkSubCategory.Checked)
                            {
                                SqlHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, "SP_omni_SubCategory_Clone", ArParams);
                                copyModule = "SubCategory";
                            }
                            else if (ChkCourse.Checked)
                            {
                                SqlHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, "SP_omni_Course_Clone", ArParams);
                                copyModule = "Course";
                            }
                            else if (ChkProduct.Checked)
                            {
                                SqlHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, "SP_omni_Product_Clone", ArParams);
                                copyModule = "Product";
                            }

/*                            SqlParameter[] ArParamsLast = new SqlParameter[3];

                            ArParamsLast[0] = new SqlParameter("@Action", SqlDbType.VarChar, 50);
                            ArParamsLast[0].Value = "Clone" + copyModule;

                            ArParamsLast[1] = new SqlParameter("@TransactDate", SqlDbType.DateTime);
                            ArParamsLast[1].Value = DateTime.Now;

                            ArParamsLast[2] = new SqlParameter("@Rest_ID", SqlDbType.Int);
                            ArParamsLast[2].Value = srclocation + "-" + trgtlocation;

                            SqlHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, "SP_omni_LastUpdates", ArParamsLast);
*/

                            trans.Commit();

                            Results.Text = copyModule +  " Data Copied Successfully.";
                        }

                        catch (Exception ex)
                        {
                            HttpContext.Current.Response.Write(ex.Message);
                            // throw exception						
                            trans.Rollback();
                            Results.Text = "Transfer Error";
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
            catch (Exception ex)
            {

                // CreateLogFiles.ErrorLog(ex.Message.ToString() & " - " & System.IO.Path.GetFileName(System.Web.HttpContext.Current.Request.Url.AbsolutePath) & " - btnCreate_Click")
                // lblError.Text = "Error - Please contact Administrator "
                // Exit Sub
            }
        }
}
}



/* ProductCopy

  insert into omni_Products(
  ProductName,ProductName2,ProductDescription,Color,GST,HasOpenPrice,ChangePrice,Price1,Price2,StockInHand,SortOrder,CategoryID,ProductImageWithPath,CourseID,Rest_ID,CreatedByUserID,CreatedOn,IsActive) select 
  ProductName,ProductName2,ProductDescription,Color,GST,HasOpenPrice,ChangePrice,Price1,Price2,StockInHand,SortOrder,CategoryID,ProductImageWithPath,CourseID,3 as Rest_ID,3 as CreatedByUserID,GETDATE() as CreatedOn,IsActive from omni_Products where Rest_ID=2

 insert into omni_Product_Modifiers (ModifierID,ProductID,Rest_ID)
 insert into omni_Product_Cooking_Options (ProductID, OptionID, Rest_ID)
 omni_Product_Kitchen_Printer_Options (ProductID, OptionID, OptionType, Rest_ID)
 * 
 
 */