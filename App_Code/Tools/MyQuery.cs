using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

/// <summary>
/// Summary description for MyQuery
/// </summary>

namespace MyQuery
{
    public class SQLQuery
    {
        string SQL = "";
        string PSQL = "";
        string addl = "";
        string add2 = "";

        public SQLQuery()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public string GetLastUpdatesSQL(Dictionary<string, string> myDictionary, string LogName)
        {
            SQL = "select TransactDate from omni_Last_Updates where ActionName='" + LogName + "'";

            if (myDictionary != null)
            {
                List<string> list = new List<string>(myDictionary.Keys);

                // Loop through list
                foreach (string k in list)
                {
                    addl += " And " + k + "= '" + myDictionary[k] + "' ";
                }
            }

            SQL += addl;
            return SQL;
        }

        public string GetServerTimeZoneSQL()
        {
            SQL = "select OptionValue from omni_Option_Settings where OptionName='TimeZone'";
            return SQL;
        }

        public string GetOptionSettingsValueSQL(string param)
        {
            SQL = "select OptionValue from omni_Option_Settings where OptionName='" + param + "'";
            return SQL;
        }

        public string GetCompanySQL()
        {
            SQL = "select CompanyName, Address , CurrencySymbol, DateFormat from omni_CompanyInfo";
            return SQL;
        }

        public string GetCustomerSQL()
        {
            SQL = "select CustomerID, (FirstName + ' ' +  LastName) as CustomerName, FirstName, LastName, Email, Phone, Fax, Address1, Address2, ZipCode, IsActive as Status from omni_Customers";
            return SQL;
        }

        public string GetSupplierSQL()
        {
            SQL = "select SupplierID, SupplierCompanyName, (FirstName + ' ' +  LastName) as SupplierName, Email, Phone, IsActive as Status from omni_Suppliers";
            return SQL;
        }
        public string GetSupplierSQL(string restid)
        {
            SQL = "select CAST('' as varchar(10)) as  SupplierID, '' as SupplierName";
            SQL += " UNION ALL ";
            SQL += "select CAST(SupplierID as varchar(10)) as SupplierID, (FirstName + ' ' +  LastName) as SupplierName from omni_Suppliers where rest_id='" + restid + "' and isactive=1";
            return SQL;
        }

        public string GetSupplierSQL(Dictionary<string, string> myDictionary)
        {
            SQL = "select CAST('' as varchar(10)) as  SupplierID, '' as SupplierName";
            SQL += " UNION ALL ";
            //SQL += "select CAST(SupplierID as varchar(10)) as SupplierID, (FirstName + ' ' +  LastName) as SupplierName from omni_Suppliers where isactive=1";
            SQL += "select CAST(SupplierID as varchar(10)) as SupplierID, SupplierCompanyName as SupplierName from omni_Suppliers where isactive=1";

            if (myDictionary != null)
            {
                List<string> list = new List<string>(myDictionary.Keys);

                // Loop through list
                foreach (string k in list)
                {
                    addl += " And " + k + "= '" + myDictionary[k] + "' ";
                }
            }

            SQL += addl;
            return SQL;
        }

        public string GetSupplierDetailSQL(Dictionary<string, string> myDictionary)
        {
            SQL = "select SupplierID, SupplierCompanyName as SupplierName, Address1, Address2 from omni_Suppliers where isactive=1";
            addl = "";

            if (myDictionary != null)
            {
                List<string> list = new List<string>(myDictionary.Keys);

                // Loop through list
                foreach (string k in list)
                {
                    addl += " And " + k + "= '" + myDictionary[k] + "' ";
                }
            }

            SQL += addl;
            return SQL;
        }

        public string GetStateSQL()
        {
            SQL = "select StateID, StateName, IsActive as Status from omni_State";
            return SQL;
        }

        public string GetStateSQL(string whfld, string whval)
        {
            SQL = "select CAST('' AS varchar(10)) as  StateID, '' as StateName, '' as Status union all ";

            if (whfld != "" && whval != "")
            {
                SQL += "select StateID, StateName, IsActive as Status from omni_State";
                SQL += " where " + whfld + "='" + whval + "'";
            }
            return SQL;
        }

        public string GetAppModuleSQL()
        {
            SQL = "select UserGroupID, UserGroupName from omni_user_group where isactive=1";
            return SQL;
        }

        public string GetAppModuleSQL(int val, DataSet ds, string tablename)
        {
            if (val == 0)
                //SQL = "select MenuID,MenuName, ParentMenuID,NavigateURL from omni_Modules where ParentMenuId=0 and IsActive=1 and islocationbased=1";
                SQL = "select MenuID,MenuName, ParentMenuID,NavigateURL from omni_Modules where ParentMenuId=0 and IsActive=1";
            else
                //SQL = "select MenuID,MenuName, ParentMenuID,NavigateURL from omni_Modules where ParentMenuId>0 and IsActive=1 order by SubMenuOrder";
                // SQL = "select MenuID,MenuName, ParentMenuID,NavigateURL from omni_Modules where ParentMenuId>0 and islocationbased=1";
                SQL = "select MenuID,MenuName, ParentMenuID,('~/'+NavigateURL) as NavigateURL from omni_Modules where ParentMenuId>0 and IsActive=1";

            SQL += "  order by MainMenuOrder";
            return SQL;
        }

        public string GetAppModuleSQL(int val, DataSet ds, string tablename, string param, int param1)
        {
            if (val == 0)
            {
                //SQL = "Select a.UserGroupID, b.MenuID, b.ParentMenuID, b.MenuName, NavigateURL from omni_Group_Permissions a inner join ";
                //SQL += "omni_Modules b on (a.MenuID = b.MenuID) and b.ParentMenuID=0 and b.IsActive=1 and b.Islocationbased=1 and a.UserGroupID=" + param1;

                SQL = "Select a.UserGroupID, b.MenuID, b.ParentMenuID, b.MenuName, ('~/'+NavigateURL) as NavigateURL, b.IsLocationBased, b.IsMenuShownWithinLocationLevel from omni_Group_Permissions a inner join ";
                SQL += "omni_Modules b on (a.MenuID = b.MenuID) and b.ParentMenuID=0 and b.IsActive=1 and IsViewInMenuNavigation = 1 and a.UserGroupID=" + param1;
            }
            else
            {
                //SQL = "Select a.UserGroupID, b.MenuID, b.ParentMenuID ,b.MenuName, NavigateURL from omni_Group_Permissions a inner join ";
                //SQL += "omni_Modules b on (a.MenuID = b.MenuID) and b.ParentMenuID>0 and b.IsActive=1 and b.Islocationbased=1 and a.UserGroupID=" + param1;

                SQL = "Select a.UserGroupID, b.MenuID, b.ParentMenuID ,b.MenuName, ('~/'+NavigateURL) as NavigateURL, b.IsLocationBased, b.IsMenuShownWithinLocationLevel from omni_Group_Permissions a inner join ";
                SQL += "omni_Modules b on (a.MenuID = b.MenuID) and b.ParentMenuID>0 and b.IsActive=1 and IsViewInMenuNavigation=1 and a.UserGroupID=" + param1;
            }
            SQL += "  order by MainMenuOrder";
            return SQL;
        }

        //public string GetAppModuleSQL(DataSet ds, string tablename, string param)
        //{
        //    SQL = "Select a.UserGroupID, b.MenuID, b.ParentMenuID ,b.MenuName, NavigateURL from omni_Group_Permissions a inner join ";
        //    SQL += "omni_Modules b on (a.MenuID = b.MenuID) and b.IsActive=1 and a.UserGroupID=" + param1;
        //    SQL += "  order by MainMenuOrder";
        //    return SQL;
        //}

        public string CheckAssignedModuleForGroupSQL(string param1, string param2, string mode)
        {
            if (mode == "Child")
            {
                //SQL = "Select a.UserGroupID, b.MenuID, b.ParentMenuID ,b.MenuName, NavigateURL from omni_Group_Permissions a inner join ";
                //SQL += " omni_Modules b on (a.MenuID = b.MenuID) and b.ParentMenuID>0 and b.IsActive=1 and a.UserGroupID='" + param1 + "' and NavigateURL='" + param2 + "'";
                SQL = "Select a.UserGroupID, b.MenuID, b.ParentMenuID ,b.MenuName, NavigateURL from omni_Group_Permissions a inner join ";
                SQL += " omni_Modules b on (a.MenuID = b.MenuID) and b.ParentMenuID>0 and a.UserGroupID='" + param1 + "' and NavigateURL='" + param2 + "'";
            }
            else
            {
                SQL = "Select a.UserGroupID, b.MenuID, b.ParentMenuID, b.MenuName, NavigateURL from omni_Group_Permissions a inner join ";
                SQL += "omni_Modules b on (a.MenuID = b.MenuID) and b.ParentMenuID=0 and a.UserGroupID=" + param1 + "' and NavigateURL='" + param2 + "'";
            }
            return SQL;
        }

        public string GetTimeZoneSQL()
        {
            SQL = "select TimeZoneID, TimeZoneDisplayName from omni_TimeZone";
            return SQL;
        }

        public string GetUserGroupSQL()
        {
            //            SQL = "select UserGroupID, UserGroupName, (case when Isactive=1 then 'Active' else 'InActive' end) as Status from omni_user_group";
            SQL = "select UserGroupID, UserGroupName, Isactive as Status, CreateDate, CreatedByUserID, ModifyDate, ModifiedByUserID from omni_user_group";
            //            SQL = "select '' AS UserGroupID, '' as UserGroupName, 0 as Status from omni_user_group";
            //            SQL += " UNION ";
            //            SQL += "select UserGroupID, UserGroupName, Isactive as Status from omni_user_group";
            return SQL;
        }

        public string GetUserGroupSQL(int param)
        {
            SQL = "select CAST('' AS varchar(10)) as  UserGroupID, '' as UserGroupName";
            SQL += " UNION ";
            SQL += "select CAST(UserGroupID as varchar(10)) as UserGroupID, UserGroupName from omni_user_group where isactive=1";
            return SQL;
        }

        public string GetUserGroupSQL(string param)
        {
            SQL = "select UserGroupID, UserGroupName from omni_user_group where isactive=1";

            if (param != "")
                addl = param;

            SQL += addl;
            return SQL;
        }

        public string GetUserSQL()
        {
            SQL = "select a.UserID, UserName,UserPassword, a.UserGroupID, b.UserGroupName,a.IsActive as Status,c.RestName from omni_users a inner join omni_user_group b on (a.UserGroupID = b.UserGroupID) inner join omni_Restuarnt_info c on (a.Rest_Id = c.Rest_Id)";
            return SQL;
        }

        public string GetUserSQL(string param)
        {
            //            SQL = "select UserID, UserName,UserGroupID, IsActive from omni_users";
            SQL = "select a.UserID, UserName, UserPassword, a.UserGroupID, b.UserGroupName,a.IsActive as Status,c.RestName from omni_users a inner join omni_user_group b on (a.UserGroupID = b.UserGroupID) inner join omni_Restuarnt_info c on (a.Rest_Id = c.Rest_Id)";

            if (param != "")
                addl = param;

            SQL += addl;
            return SQL;
        }

        public string GetUserSQL(Dictionary<string, string> myDictionary, string whfld, string whval)
        {
            if (whfld != "" && whval != "")
            {
                SQL = "select a.UserID, UserName, UserPin, UserEmail, UserPassword, a.Rest_ID, a.UserGroupID , b.UserGroupName, a.IsActive as Status,c.RestName from omni_users a inner join omni_user_group b on (a.UserGroupID = b.UserGroupID) inner join omni_Restuarnt_info c on (a.Rest_Id = c.Rest_Id)";
                SQL += " where a." + whfld + "='" + whval + "'";

                if (myDictionary != null)
                {
                    List<string> list = new List<string>(myDictionary.Keys);

                    // Loop through list
                    foreach (string k in list)
                    {
                        addl += " And " + k + "= '" + myDictionary[k] + "' ";
                    }
                }

                SQL += addl;
            }
            return SQL;
        }

        public string GetAdminCompanySQL(Dictionary<string, string> myDictionary)
        {

            SQL = "SELECT Id,Name,Code,DBName,DBUserName,DBPassword,DBConnectionString,CreatedDate,ModifiedDate,EmailAddress,IsDataBaseCreated FROM Company where 1=1 ";
            addl = "";

            if (myDictionary != null)
            {
                List<string> list = new List<string>(myDictionary.Keys);

                // Loop through list
                foreach (string k in list)
                {
                    addl += " And Id = '" + myDictionary[k] + "' ";
                }
            }

            SQL += addl;

            return SQL;
        }

        public string GetUserSQL_webservice(Dictionary<string, string> myDictionary, string whfld, string whval)             // for webservice call
        {
            if (whfld != "" && whval != "")
            {
                SQL = "select a.UserID, UserName, UserPin, UserEmail, a.Rest_ID, a.UserGroupID , b.UserGroupName, a.IsActive as Status, Isnull(a.FirstName,'') as FirstName, isnull(a.LastName,'') as LastName, isnull(a.UserAlias,'') as UserAlias, ";
                SQL += "isnull(a.UserPhone,'') as UserPHone, isnull(a.StartDate,'') as StartDate, isnull(a.Createdate,'') as CreateDate, isnull(a.CreatedByUserID,'') as CreatedByUserID, isnull(a.ModifyDate,'') as ModifyDate, isnull( a.ModifiedByUserID,'') as ModifiedByUserID, ";
                SQL += "c.RestName from omni_users a inner join omni_user_group b on (a.UserGroupID = b.UserGroupID) inner join omni_Restuarnt_info c on (a.Rest_Id = c.Rest_Id)";
                SQL += " where a." + whfld + "='" + whval + "'";

                if (myDictionary != null)
                {
                    List<string> list = new List<string>(myDictionary.Keys);

                    // Loop through list
                    foreach (string k in list)
                    {
                        addl += " And " + k + "= '" + myDictionary[k] + "' ";
                    }
                }

                SQL += addl;
            }
            return SQL;
        }

        public string getAllUserInfoSQL_WebService(Dictionary<string, string> myDictionary, string whfld, string whval)     // for webservice call
        {
            SQL = "select a.UserID, UserName, UserPin, UserEmail, a.Rest_ID, a.UserGroupID , b.UserGroupName, a.IsActive as Status, Isnull(a.FirstName,'') as FirstName, isnull(a.LastName,'') as LastName, isnull(a.UserAlias,'') as UserAlias, isnull(a.HourlyRate,'0') as HourlyRate, ";
            SQL += "isnull(a.UserPhone,'') as UserPHone, isnull(a.StartDate,'') as StartDate, isnull(a.Createdate,'') as CreateDate, isnull(a.CreatedByUserID,'') as CreatedByUserID, isnull(a.ModifyDate,'') as ModifyDate, isnull( a.ModifiedByUserID,'') as ModifiedByUserID, ";
            SQL += "c.RestName from omni_users a inner join omni_user_group b on (a.UserGroupID = b.UserGroupID) inner join omni_Restuarnt_info c on (a.Rest_Id = c.Rest_Id)";

            if (whfld != "" && whval != "")
            {
                SQL += " where a." + whfld + "='" + whval + "'";

                if (myDictionary != null)
                {
                    List<string> list = new List<string>(myDictionary.Keys);

                    // Loop through list
                    foreach (string k in list)
                    {
                        addl += " And " + k + "= '" + myDictionary[k] + "' ";
                    }
                }

                SQL += addl;
            }
            return SQL;
        }


        public string GetCoursesSQL(string val)
        {
            SQL = "select CAST('' as varchar(10)) as  CourseID, '' as CourseName";
            SQL += " union all ";
            SQL += "select CAST(CourseID as varchar(10)) as  CourseID, CourseName from omni_Courses where isactive=1 and Rest_ID='" + val + "'";
            return SQL;
        }

        public string GetCoursesSQL(Dictionary<string, string> myDictionary, string whfld, string whval)             // for webservice call
        {
            addl = "";
            SQL = "select CourseID, CourseName, SortOrder, CreatedByUserID, CreatedOn, ModifiedByUserID, ModifiedOn, IsActive as Status from omni_Courses";

            if (whfld != "" && whval != "")
            {
                SQL += " where " + whfld + "='" + whval + "'";

                if (myDictionary != null)
                {
                    List<string> list = new List<string>(myDictionary.Keys);

                    // Loop through list
                    foreach (string k in list)
                    {
                        addl += " And " + k + "= '" + myDictionary[k] + "' ";
                    }
                }

                SQL += addl;
            }
            return SQL;
        }

        public string GetGroupPermissionSQL()
        {
            SQL = "select MenuID, MenuName from omni_Modules";
            return SQL;
        }

        public string GetRestaurantSQL()
        {
            SQL = "select Rest_ID,RestName , Initials, City, StateName as State ,Zip, Phone, Header_Address1, Header_ABN from omni_Restuarnt_info a inner join omni_State b on (a.StateID = b.StateID and a.IsActive=1)";
            return SQL;
        }

        public string GetRestaurantSQL(string mode)
        {
            SQL = "select Rest_ID,RestName from omni_Restuarnt_info where isactive = 1";
            return SQL;
        }

        public string GetRestaurantSQL(string whfld, string whparam)
        {
            SQL = "select a.UserID,UserGroupID ,UserName ,a.Rest_id, b.RestName, b.Initials from omni_users a ";
            SQL += "left join omni_Restuarnt_info b on (a.Rest_id = b.Rest_Id ) ";
            SQL += " where " + whfld + "='" + whparam + "'";
            return SQL;
        }

        public string GetRestaurantNameSQL(string whparam)
        {
            SQL = "select RestName from omni_Restuarnt_info ";
            SQL += " where Rest_Id ='" + whparam + "'";
            return SQL;
        }

        public string ShouldEmailZReportSQL(string whparam)
        {
            SQL = "select shouldEmailZReport from omni_Restuarnt_info_Settings ";
            SQL += " where Rest_Id ='" + whparam + "'";
            return SQL;
        }

        public string GetCompanyEmailSQL()
        {
            SQL = "select Email from omni_Companyinfo ";
            return SQL;
        }

        public string GetRestaurantHeaderAndFooter(string whparam)
        {
            SQL = "select Header_Name,Header_Address1,Header_City,Header_State,Header_Zip,Header_Email,Header_ABN,Footer1,Footer2 from omni_Restuarnt_info ";
            SQL += " where Rest_Id ='" + whparam + "'";
            return SQL;
        }

        public string GetRestaurantSQL(Dictionary<string, Byte[]> myDictionary, string whfld, string whval)             // for webservice call
        {
            SQL = "select b.Rest_Id, RestName,Initials, Address1, Address2, City, StateID, Zip, Phone, Fax, Email , TablesCount, ";
            SQL += "IsDeleted, DeletedOn, DeletedBy, ModifiedOn, ModifiedBy, CreatedOn, CreatedBy, Header_Name, Header_Address1, ";
            SQL += "Header_City, Header_State, Header_Zip, Header_Phone, Header_ABN, Header_TaxInvoice, Header_Website, Header_Email, ";
            SQL += "Footer1, Footer2, KitchenView, ExpediteView,b.Tax, b.IsActive as Status, CreatedOn, CreatedBy, ModifiedOn, ModifiedBy";

            /********************* Added on 26th May 2013 *******************************************/

            SQL += ", isnull(CustomerView,0) as CustomerView, isnull(AddLines_Between_Order_Item,0) as AddLines_Between_Order_Item, ";
            SQL += "  isnull(Sort_Course_By,0) as Sort_Course_By, isnull(Print_Transferred_Order,0) as Print_Transferred_Order, ";
            SQL += "  isnull(Print_Deleted_Order,0) as Print_Deleted_Order, isnull(Print_Voided_Items,0) as Print_Voided_Items, ";        // added on 26th May 2013
            SQL += "  isnull(Kitchen_View_Timeout,0) as Kitchen_View_Timeout, isnull(Allow_Void_Order_Item,0) as Allow_Void_Order_Item, ";
            SQL += "  isnull(Allow_Delete_Send_Order,0) as Allow_Delete_Send_Order, isnull(Quick_Service,0) as Quick_Service, ";
            SQL += "  isnull(Table_Layout_Type,0) as Table_Layout_Type, isnull(Auto_Prompt_Tip,0) as Auto_Prompt_Tip, ";
            SQL += "  isnull(Sort_Items_By,0) as Sort_Items_By, isnull(Sort_SubCategories_By,0) as Sort_SubCategories_By, ";
            SQL += "  isnull(Sort_Products_By,0) as Sort_Products_By, isnull(No_Of_Devices,0) as No_Of_Devices, isnull(Use_Table_Layout,0) as Use_Table_Layout, isnull(shouldNotifyNoSale,0) as Notify_No_Sale, ";
            SQL += "  isnull(ShouldEnableClockIn,0) as ShouldEnableClockIn, isnull(ScannerMode,0) as ScannerMode, ";
            SQL += "  isnull(CashDrawerBalancing,0) as CashDrawerBalancing, isnull(HoldAndFire,0) as HoldAndFire, ";
            SQL += "  isnull(NoSaleLimit,0) as NoSaleLimit ";

            /****************************************************************************************/


            SQL += " from omni_users a inner join omni_Restuarnt_info b on (a.Rest_id = b.Rest_Id)";


            /********************* Added on 26th May 2013 *******************************************/

            SQL += " left join omni_Restuarnt_info_Settings c on (b.Rest_id = c.Rest_Id) ";

            /****************************************************************************************/


            if (whfld != "" && whval != "")
                SQL += " where " + whfld + "='" + whval + "'";

            if (myDictionary != null)
            {
                List<string> list = new List<string>(myDictionary.Keys);

                // Loop through list
                foreach (string k in list)
                {
                    addl += " And " + k + "= '" + myDictionary[k] + "' ";
                }
            }
            SQL += addl;

            return SQL;
        }

        public string GetRestaurantSQL_WebService(Dictionary<string, Byte[]> myDictionary, string whfld, string whval)             // for webservice call
        {
            SQL = "select RestName,Initials, Address1, Address2, City, StateID, Zip, Phone, Fax, Email , TablesCount, ";
            SQL += "IsDeleted, DeletedOn, DeletedBy, ModifiedOn, ModifiedBy, CreatedOn, CreatedBy, Header_Name, Header_Address1, ";
            SQL += "Header_City, Header_State, Header_Zip, Header_Phone, Header_ABN, Header_TaxInvoice, Header_Website, Header_Email, ";
            SQL += "Footer1, Footer2, KitchenView, ExpediteView, b.Tax, b.IsActive";

            /********************* Added on 26th May 2013 *******************************************/

            SQL += ", isnull(CustomerView,0) as CustomerView, isnull(AddLines_Between_Order_Item,0) as AddLines_Between_Order_Item, ";
            SQL += "  isnull(Sort_Course_By,0) as Sort_Course_By, isnull(Print_Transferred_Order,0) as Print_Transferred_Order, ";
            SQL += "  isnull(Print_Deleted_Order,0) as Print_Deleted_Order, isnull(Print_Voided_Items,0) as Print_Voided_Items, ";        // added on 26th May 2013
            SQL += "  isnull(Kitchen_View_Timeout,0) as Kitchen_View_Timeout, isnull(Allow_Void_Order_Item,0) as Allow_Void_Order_Item, ";
            SQL += "  isnull(Allow_Delete_Send_Order,0) as Allow_Delete_Send_Order, isnull(Quick_Service,0) as Quick_Service, ";
            SQL += "  isnull(Table_Layout_Type,0) as Table_Layout_Type, isnull(Auto_Prompt_Tip,0) as Auto_Prompt_Tip, ";
            SQL += "  isnull(Sort_Items_By,0) as Sort_Items_By, isnull(Sort_SubCategories_By,0) as Sort_SubCategories_By, ";
            SQL += "  isnull(Sort_Products_By,0) as Sort_Products_By, isnull(No_Of_Devices,0) as No_Of_Devices, isnull(Use_Table_Layout,0) as Use_Table_Layout";

            /****************************************************************************************/

            SQL += " from omni_users a inner join omni_Restuarnt_info b on (a.Rest_id = b.Rest_Id)";

            /********************* Added on 26th May 2013 *******************************************/

            SQL += " left join omni_Restuarnt_info_Settings c on (b.Rest_id = c.Rest_Id) ";

            /****************************************************************************************/


            SQL += " where a.UserName = @UserName and userpassword = @usrpwd";

            //if (whfld != "" && whval != "")
            //    SQL += " where " + whfld + "='" + whval + "'";

            if (myDictionary != null)
            {
                List<string> list = new List<string>(myDictionary.Keys);

                // Loop through list
                foreach (string k in list)
                {
                    addl += " And " + k + "= '" + myDictionary[k] + "' ";
                }
            }
            SQL += addl;

            return SQL;
        }

        public string GetCategoriesSQL()
        {
            SQL = "select a.categoryid,a.CategoryName, a.parentid,isnull(b.categoryname,'') as Parent , a.IsActive as Status from omni_item_categories a ";
            SQL += "left join omni_item_categories b on (a.parentid = b.CategoryID)";
            return SQL;
        }

        public string GetCategoriesSQL(int mode)
        {
            SQL = "select a.categoryid,a.CategoryName, a.parentid,isnull(b.categoryname,'') as Parent , ";
            SQL += "(case when a.parentid = 0 then 'CreateCategory.aspx' else 'CreateSubCategory.aspx' end) as URL, ";
            SQL += "a.IsActive as Status from omni_item_categories a left join omni_item_categories b on (a.parentid = b.CategoryId)";
            return SQL;
        }

        public string GetCategoriesSQL(string param, string val)
        {
            SQL = "select categoryid,CategoryName,isnull(Name2,'') as Name2,  parentid,(case when parentid = 0 then 'CreateCategory.aspx' else 'CreateSubCategory.aspx' end) as URL, ";
            SQL += "CreatedByUserID, CreateDate, ModifiedByUserID, ModifiedDate, isnull(SortOrder,0) as SortOrder, IsActive as Status from omni_item_categories where isactive=1 and " + param + "='" + val + "'";
            return SQL;
        }

        public string GetCategoriesSQL(Dictionary<string, string> myDictionary, string whfld, string whval, string mode)
        {
            addl = "";

            SQL = "select a.categoryid,a.CategoryName,isnull(a.Name2,'') as Name2,  a.parentid, ISNULL( b.CategoryName,'') as Parent,(case when a.parentid = 0 then 'CreateCategory.aspx' else 'CreateSubCategory.aspx' end) as URL, ";
            SQL += " a.CreatedByUserID, a.CreateDate, a.ModifiedByUserID, a.ModifiedDate, isnull(a.SortOrder,0) as SortOrder, a.IsActive as Status from omni_item_categories a left join omni_Item_Categories b on (a.ParentId=b.CategoryId)";

            if (whfld != "" && whval != "")
                SQL += " where a." + whfld + "='" + whval + "'";

            if (myDictionary != null)
            {
                List<string> list = new List<string>(myDictionary.Keys);

                // Loop through list
                foreach (string k in list)
                {
                    addl += " And " + k + "= '" + myDictionary[k] + "' ";
                }
            }

            SQL += addl;
            return SQL;
        }


        public string GetCategoriesSQL(Dictionary<string, string> myDictionary, string whfld, string whval)
        {
            addl = "";

            SQL = "select a.categoryid,a.CategoryName, isnull(a.Name2,'') as Name2, a.parentid,isnull(b.categoryname,'') as Parent , ";
            SQL += "(case when a.parentid = 0 then 'CreateCategory.aspx' else 'CreateSubCategory.aspx' end) as URL, ";
            SQL += "a.CreatedByUserID, a.CreateDate, a.ModifiedByUserID, a.ModifiedDate, isnull(a.SortOrder,0) as SortOrder, a.IsActive as Status from omni_item_categories a left join omni_item_categories b on (a.parentid = b.CategoryId)";

            if (whfld != "" && whval != "")
                SQL += " where a." + whfld + "='" + whval + "'";

            if (myDictionary != null)
            {
                List<string> list = new List<string>(myDictionary.Keys);

                // Loop through list
                foreach (string k in list)
                {
                    addl += " And " + k + "= '" + myDictionary[k] + "' ";
                }
            }

            SQL += addl;
            return SQL;
        }

        public string GetNotesSQL(Dictionary<string, string> myDictionary, string whfld, string whval)
        {
            SQL = "select NoteID, SUBSTRING(Message,0,50) + '....'   as Message, IsActive as Status , CreatedByUserID, CreatedOn, ModifiedByUserID, ModifiedOn from omni_Order_Note";

            if (whfld != "" && whval != "")
            {
                SQL += " where " + whfld + "='" + whval + "'";
            }

            if (myDictionary != null)
            {
                List<string> list = new List<string>(myDictionary.Keys);

                // Loop through list
                foreach (string k in list)
                {
                    addl += " And " + k + "= '" + myDictionary[k] + "' ";
                }
            }

            SQL += addl;
            return SQL;
        }

        public string GetInstructionSQL(Dictionary<string, string> myDictionary, string whfld, string whval)
        {
            SQL = "select InstructionID, SUBSTRING(Message,0,50) + '....'   as Message, IsActive as Status, CreatedByUserID, CreatedOn, ModifiedByUserID, ModifiedOn from omni_Kitchen_Instruction";

            if (whfld != "" && whval != "")
            {
                SQL += " where " + whfld + "='" + whval + "'";
            }

            if (myDictionary != null)
            {
                List<string> list = new List<string>(myDictionary.Keys);

                // Loop through list
                foreach (string k in list)
                {
                    addl += " And " + k + "= '" + myDictionary[k] + "' ";
                }
            }

            SQL += addl;
            return SQL;
        }

        public string GetTaxInfoSQL(Dictionary<string, string> myDictionary, string whfld, string whval)
        {
            addl = "";
            SQL = "select TaxInfoID, TaxInfoLiteral, TaxRate, IsActive as Status, CreatedByUserID, CreatedOn, ModifiedByUserID, ModifiedOn from omni_TaxInfo";

            if (whfld != "" && whval != "")
            {
                SQL += " where " + whfld + "='" + whval + "'";
            }

            if (myDictionary != null)
            {
                List<string> list = new List<string>(myDictionary.Keys);

                // Loop through list
                foreach (string k in list)
                {
                    addl += " And " + k + "= '" + myDictionary[k] + "' ";
                }
            }

            SQL += addl;
            return SQL;
        }

        public string GetDeviceInfoSQL(Dictionary<string, string> myDictionary, string whfld, string whval)
        {
            addl = "";

            SQL = "select DeviceID, DeviceName, PrinterID, IsActive as Status, CreatedByUserID, CreatedOn, ModifiedByUserID, ModifiedOn from omni_Device";

            if (whfld != "" && whval != "")
            {
                SQL += " where " + whfld + "='" + whval + "'";
            }

            if (myDictionary != null)
            {
                List<string> list = new List<string>(myDictionary.Keys);

                // Loop through list
                foreach (string k in list)
                {
                    addl += " And " + k + "= '" + myDictionary[k] + "' ";
                }
            }

            SQL += addl;
            return SQL;
        }

        public string GetParentCategoriesSQL(int restid)
        {
            SQL = "select cast('' as varchar(10)) as categoryid, '' as CategoryName from omni_item_categories";
            SQL += " Union ";
            SQL += "select cast(categoryid as varchar(10)) as categoryid, CategoryName from omni_item_categories where ParentID = 0 and IsActive=1 and Rest_ID=" + restid;
            return SQL;
        }

        public string GetChildCategoriesSQL(int restid, int value)
        {
            SQL = "select categoryid , CategoryName from omni_item_categories where ParentID = " + value + " and IsActive=1 and Rest_ID=" + restid;
            return SQL;
        }

        public string getParentCategorySQL()
        {
            SQL = "select CAST(' ' as varchar(10)) as categoryid ,'' as CategoryName";
            SQL += " Union All ";
            SQL += "select CAST(categoryid as varchar(10)) as categoryid ,CategoryName from omni_item_categories Where ParentID = 0";
            return SQL;
        }

        public string getParentCategorySQL(Dictionary<string, string> myDictionary)
        {
            SQL = "select CAST(' ' as varchar(10)) as categoryid ,'' as CategoryName";
            SQL += " Union All ";
            SQL += "select CAST(categoryid as varchar(10)) as categoryid ,CategoryName from omni_item_categories Where ParentID = 0";

            /*            if (whfld != "")
                            SQL += " where " + whfld + " ='" + whlval + "'";
            */

            if (myDictionary != null)
            {
                List<string> list = new List<string>(myDictionary.Keys);

                // Loop through list
                foreach (string k in list)
                {
                    addl += " And " + k + "= '" + myDictionary[k] + "' ";
                }
            }

            SQL += addl;
            return SQL;
        }


        public string getPrinterSQL()
        {
            SQL = "select PrinterID, PrinterName, IPAddress, PrinterType ,IsActive as Status from omni_Printers";
            return SQL;
        }

        public string getPrinterSQL(int mode)
        {
            SQL = "select CAST('' AS VARCHAR(10)) AS PrinterID, '' AS PrinterName, '' as IPAddress from omni_Printers";
            SQL += " UNION ";
            SQL += "select CAST(PrinterID AS VARCHAR(10)) AS PrinterID, PrinterName, IPAddress from omni_Printers where isactive=1";
            return SQL;
        }

        public string getPrinterSQL(int mode, string whfld, string whlval)
        {
            SQL = "select CAST('' AS VARCHAR(10)) AS PrinterID, '' AS PrinterName, '' as IPAddress from omni_Printers";
            SQL += " UNION ";
            SQL += "select CAST(PrinterID AS VARCHAR(10)) AS PrinterID, PrinterName, IPAddress from omni_Printers where isactive=1 and Rest_ID=" + mode;

            if (whfld != "" && whlval != "")
            {
                SQL += " and " + whfld + " ='" + whlval + "'";
            }
            return SQL;
        }

        public string getPrinterSQL(Dictionary<string, string> myDictionary, string whfld, string whlval)
        {
            addl = "";

            SQL = "select PrinterID, PrinterName, IPAddress, PrinterType, IsActive as Status, PosorItem, NoOfCopies, Trigger_Cash_Drawer, IsPrintIpAddress, CreatedByUserID, CreatedOn, ModifiedByUserID, ModifiedDate  from omni_Printers";

            if (whfld != "" && whlval != "")
                SQL += " where " + whfld + " ='" + whlval + "'";

            if (myDictionary != null)
            {
                List<string> list = new List<string>(myDictionary.Keys);

                // Loop through list
                foreach (string k in list)
                {
                    addl += " And " + k + "= '" + myDictionary[k] + "' ";
                }
            }

            SQL += addl;
            return SQL;
        }


        public string getKitchenSQL()
        {
            SQL = "select KitchenID, KitchenName,IsActive as Status from omni_Kitchen";
            return SQL;
        }

        public string getKitchenSQL(int mode)
        {
            SQL = "select CAST('' as varchar(10)) as  KitchenID, '' as KitchenName from omni_Kitchen where isactive=1";
            SQL += " union ";
            SQL += "select CAST(KitchenID as varchar(10)) as  KitchenID, KitchenName from omni_Kitchen where isactive=1";
            return SQL;
        }

        public string getKitchenSQL(Dictionary<string, string> myDictionary, string whfld, string whlval)
        {
            addl = "";
            SQL = "select KitchenID, KitchenName,SortOrder, IsActive, CreatedOn,CreatedByUserID, ModifiedOn, ModifiedByUserID, IsActive as Status from omni_Kitchen";

            if (whfld != "" && whlval != "")
                SQL += " where " + whfld + " ='" + whlval + "'";

            if (myDictionary != null)
            {
                List<string> list = new List<string>(myDictionary.Keys);

                // Loop through list
                foreach (string k in list)
                {
                    addl += " And " + k + "= '" + myDictionary[k] + "' ";
                }
            }

            SQL += addl;
            return SQL;
        }

        public string getKitchenSQL(Dictionary<string, string> myDictionary, string whfld, string whlval, int mode)
        {
            SQL = "select CAST('' as varchar(10)) as  KitchenID, '' as KitchenName from omni_Kitchen";
            SQL += " union ";
            SQL += "select CAST(KitchenID as varchar(10)) as  KitchenID, KitchenName from omni_Kitchen";

            if (whfld != "" && whlval != "")
                SQL += " where " + whfld + " ='" + whlval + "'";

            if (myDictionary != null)
            {
                List<string> list = new List<string>(myDictionary.Keys);

                // Loop through list
                foreach (string k in list)
                {
                    addl += " And " + k + "= '" + myDictionary[k] + "' ";
                }
            }

            SQL += addl;
            return SQL;
        }

        public string getCookingOptionSQL()
        {
            SQL = "select OptionID, OptionName from omni_Cooking_Options";
            return SQL;
        }

        public string getCookingOptionSQL(int restid)
        {
            SQL = "select OptionID, OptionName from omni_Cooking_Options where isactive=1 and Rest_ID =" + restid;
            return SQL;
        }


        /*        public string getCookingOptionSQL(int mode)
                {
                    SQL = "select CAST('' as varchar(10)) as  OptionID, '' as OptionName";
                    SQL += " union ";
                    SQL += "select CAST(OptionID as varchar(10)) as  OptionID, OptionName from omni_Cooking_Options where isactive=1";
                    return SQL;
                }
        */

        public string getCookingOptionSQL(Dictionary<string, string> myDictionary, string whfld, string whlval)
        {
            addl = "";
            SQL = "select OptionID, OptionName, IsActive as Status, CreatedByUserID, CreatedOn, ModifiedByUserID, ModifiedOn from omni_Cooking_Options";

            if (whfld != "" && whlval != "")
                SQL += " where " + whfld + " ='" + whlval + "'";

            if (myDictionary != null)
            {
                List<string> list = new List<string>(myDictionary.Keys);

                // Loop through list
                foreach (string k in list)
                {
                    addl += " And " + k + "= '" + myDictionary[k] + "' ";
                }
            }

            SQL += addl;
            return SQL;
        }




        public string getModifierLevelSQL()
        {
            SQL = "select LevelID, ModifierLevelName,IsActive as Status from omni_Modifiers_Level";
            return SQL;
        }

        public string getModifierLevelSQL(Dictionary<string, string> myDictionary, string whfld, string whlval)
        {
            addl = "";
            SQL = "select LevelID, ModifierLevelName, IsActive as Status, CreatedOn, CreatedByUserID, ModifiedByUserID, ModifiedOn from omni_Modifiers_Level";

            if (whfld != "")
                SQL += " where " + whfld + " ='" + whlval + "'";

            if (myDictionary != null)
            {
                List<string> list = new List<string>(myDictionary.Keys);

                // Loop through list
                foreach (string k in list)
                {
                    addl += " And " + k + "= '" + myDictionary[k] + "' ";
                }
            }

            SQL += addl;
            return SQL;
        }


        public string getUnitSQL(string val)
        {
            SQL = "select CAST('' as varchar(10)) as  UnitID, '' as UnitName ";
            SQL += " UNION ALL ";
            SQL += "select CAST(UnitID as varchar(10)) as  UnitID, UnitName from omni_UnitMaster where isactive=1 and Rest_ID ='" + val + "'";
            return SQL;
        }

        public string getUnitSQL(int restid)
        {
            SQL = "select UnitID, UnitName, IsActive as status from omni_UnitMaster where isactive=1 and Rest_ID =" + restid;
            return SQL;
        }

        public string getUnitSQL(Dictionary<string, string> myDictionary, string whfld, string whlval)
        {
            addl = "";
            SQL = "select UnitID, UnitName, IsActive as status from omni_UnitMaster";

            if (whfld != "" && whlval != "")
                SQL += " where " + whfld + " ='" + whlval + "'";

            if (myDictionary != null)
            {
                List<string> list = new List<string>(myDictionary.Keys);

                // Loop through list
                foreach (string k in list)
                {
                    addl += " And " + k + "= '" + myDictionary[k] + "' ";
                }
            }

            SQL += addl;
            return SQL;
        }

        public string getIngredientWithSubRecipeSQL(int restid)
        {
            SQL = "select final.* from (";
            SQL += "select a.IngredientID as IngredientID, a.IngredientName as IngredientName, a.Price as Price,a.UnitID as UnitID, b.UnitName as Unit, 'Ingredient' as Type, a.IsActive as Status, a.Rest_ID from omni_Items_Ingredients a  ";
            SQL += " inner join ";
            SQL += " omni_UnitMaster b on (a.UnitID = b.UnitID) where a.isactive=1 and a.rest_id=" + restid;
            SQL += " union all ";
            SQL += "select SubRecipeID as IngredientID, SubRecipeName as IngredientName, 0 as Price, '' as UnitID, '' as UnitName, 'Sub Recipe' as Type, IsActive as Status, Rest_ID  from omni_SubRecipe ) as final ";
            SQL += "where final.Status=1 and final.rest_id=" + restid;
            return SQL;

        }

        public string getIngredientWithSubRecipeSQL(Dictionary<string, string> myDictionary, string whfld, string whlval, string addlfld, string addlfldval)
        {
            addl = "";
            SQL = "select final.* from (";
            SQL += "select a.IngredientID as IngredientID, a.IngredientName as IngredientName, a.Price as Price,a.UnitID as UnitID, b.UnitName as Unit, 'Ingredient' as Type, a.IsActive as Status, a.Rest_ID from omni_Items_Ingredients a  ";
            SQL += " inner join ";
            SQL += " omni_UnitMaster b on (a.UnitID = b.UnitID) and " + addlfld + "='" + addlfldval + "'";
            SQL += " union all ";
            SQL += "select SubRecipeID as IngredientID, SubRecipeName as IngredientName, 0 as Price, a.UnitID as UnitID, b.UnitName as Unit, 'Sub Recipe' as Type, a.IsActive as Status, a.Rest_ID  from omni_SubRecipe a ";
            SQL += " inner join ";
            SQL += " omni_UnitMaster b on (a.UnitID = b.UnitID) ";
            SQL += " ) as final ";

            if (whfld != "" && whlval != "")
                SQL += " where " + whfld + " ='" + whlval + "'";

            if (myDictionary != null)
            {
                List<string> list = new List<string>(myDictionary.Keys);

                // Loop through list
                int i = 0;

                foreach (string k in list)
                {
                    if ((whlval == "" || whlval == null) && i == 0)
                        addl = " where " + k + "= '" + myDictionary[k] + "' ";
                    else
                        addl += " And " + k + "= '" + myDictionary[k] + "' ";
                    i++;
                }
            }

            SQL += addl + " order by IngredientName";
            return SQL;
        }

        public string getIngredientSQL(int restid)
        {
            SQL = "select a.IngredientID as IngredientID, a.IngredientName as IngredientName, a.Price as Price,a.UnitID as UnitID, b.UnitName as Unit, a.IsActive as Status from omni_Items_Ingredients a  ";
            SQL += " inner join ";
            SQL += " omni_UnitMaster b on (a.UnitID = b.UnitID) where a.isactive=1 and a.rest_id=" + restid;
            return SQL;
        }

        public string getIngredientWithinRecipeSQL(Dictionary<string, string> myDictionary, string whfld, string whlval)
        {
            addl = "";
            SQL = "select a.IngredientID, (select IngredientName from omni_Items_Ingredients where IngredientId = a.IngredientID) as IngredientName, ";
            SQL += " a.Qty as Qty, b.UnitID as UnitID, (select UnitName from omni_UnitMaster where UnitId = b.UnitId ) as Unit from omni_SubRecipe_Mixing_Details a  ";
            SQL += " inner join ";
            SQL += " omni_Items_Ingredients b on (a.IngredientID = b.IngredientID) ";

            if (whfld != "" && whlval != "")
                SQL += " where " + whfld + " ='" + whlval + "'";

            if (myDictionary != null)
            {
                List<string> list = new List<string>(myDictionary.Keys);

                int i = 0;
                // Loop through list
                foreach (string k in list)
                {
                    if ((whlval == "" || whlval == null) && i == 0)
                        addl = " where " + k + "= '" + myDictionary[k] + "' ";
                    else
                        addl += " And " + k + "= '" + myDictionary[k] + "' ";
                    i++;
                }
            }

            SQL += addl;
            return SQL;
        }

        public string getIngredientSQL(Dictionary<string, string> myDictionary, string whfld, string whlval)
        {
            addl = "";
            SQL = "select a.IngredientID as IngredientID, a.IngredientName as IngredientName, a.Price as Price,a.UnitID as UnitID, b.UnitName as Unit, a.IsActive as Status from omni_Items_Ingredients a  ";
            SQL += " inner join ";
            SQL += " omni_UnitMaster b on (a.UnitID = b.UnitID) ";

            if (whfld != "" && whlval != "")
                SQL += " where " + whfld + " ='" + whlval + "'";

            if (myDictionary != null)
            {
                List<string> list = new List<string>(myDictionary.Keys);

                int i = 0;
                // Loop through list
                foreach (string k in list)
                {
                    if ((whlval == "" || whlval == null) && i == 0)
                        addl = " where " + k + "= '" + myDictionary[k] + "' ";
                    else
                        addl += " And " + k + "= '" + myDictionary[k] + "' ";
                    i++;
                }
            }

            SQL += addl;
            return SQL;
        }

        public string getSubRecipeSQL(Dictionary<string, string> myDictionary, string whfld, string whval)
        {
            addl = "";
            SQL = "select a.SubRecipeID as SubRecipeID, a.SubRecipeName as SubRecipeName, a.UnitID as UnitID, b.UnitName as Unit, a.IsActive as Status from omni_SubRecipe a  ";
            SQL += " inner join ";
            SQL += " omni_UnitMaster b on (a.UnitID = b.UnitID) ";

            if (whfld != "" && whval != "")
                SQL += " where " + whfld + " ='" + whval + "'";

            if (myDictionary != null)
            {
                List<string> list = new List<string>(myDictionary.Keys);

                // Loop through list
                foreach (string k in list)
                {
                    addl += " And " + k + "= '" + myDictionary[k] + "' ";
                }
            }

            SQL += addl;
            return SQL;
        }

        public string getProductSQL()
        {
            SQL = "select a.ProductID, a.ProductName,a.Price1, a.IsActive as Status, b.CategoryName  from omni_Products a ";
            SQL += "inner join omni_Item_Categories b on (a.CategoryID = b.CategoryID)";
            return SQL;
        }

        public string getProductSQL(Dictionary<string, string> myDictionary, string whfld, string whlval)
        {
            addl = "";
            //SQL = "select a.ProductID, a.ProductName, a.ProductName2, a.ProductDescription, a.Color, a.GST, a.HasOpenPrice, IsNull(a.ChangePrice,0) as ChangePrice, a.Price1, a.Price2, a.StockInHand, IsNull(a.SortOrder,0) as SortOrder, ";
            //SQL += " a.CategoryID, (select ParentId from omni_Item_Categories where CategoryID=a.CategoryID) as ParentCategoryID, ";      // new addition on 2nd May 2013 
            //SQL += " a.ProductImageWithPath, IsNull(a.CourseID,'') as CourseID, a.IsActive as Status,a.CreatedByUserID, a.CreatedOn, a.ModifiedOn,a.ModifiedByUserID ,b.CategoryName  from omni_Products a ";
            ////SQL += "inner join omni_Item_Categories b on (a.CategoryID = b.CategoryID)";
            //SQL += "left join omni_Item_Categories b on (a.CategoryID = b.CategoryID)";


            SQL = "select final.*,c.CategoryName as ParentCatName  from (";
            SQL += "select a.Rest_ID, a.ProductID, a.ProductName, a.ProductName2, a.ProductDescription, a.Color, a.GST, a.HasOpenPrice, IsNull(a.ChangePrice,0) as ChangePrice, a.Price1, a.Price2, a.StockInHand, IsNull(a.SortOrder,0) as SortOrder, a.Points, ";
            SQL += " a.CategoryID, (select ParentId from omni_Item_Categories where CategoryID=a.CategoryID) as ParentCategoryID, ";      // new addition on 2nd May 2013 
            SQL += " a.ProductImageWithPath, IsNull(a.CourseID,'') as CourseID, a.IsActive as Status,a.CreatedByUserID, a.CreatedOn, a.ModifiedOn,a.ModifiedByUserID ,b.CategoryName  from omni_Products a ";
            SQL += "left join omni_Item_Categories b on (a.CategoryID = b.CategoryID)) as final ";
            SQL += "left join omni_Item_Categories c on (final.ParentCategoryID = c.CategoryId) ";

            if (whfld != "" && whlval != "")
                //SQL += " where a." + whfld + " ='" + whlval + "'";
                SQL += " where final." + whfld + " ='" + whlval + "'";

            if (myDictionary != null)
            {
                List<string> list = new List<string>(myDictionary.Keys);

                // Loop through list
                foreach (string k in list)
                {
                    addl += " And " + k + "= '" + myDictionary[k] + "' ";
                }
            }

            SQL += addl + " order by ParentCatName, ProductName asc";
            return SQL;
        }

        public string GetProductsWithIngredientAndSubRecipesSQL(Dictionary<string, string> myDictionary, string whfld, string whlval, string restid)
        {
            addl = "";
            SQL = "select ID, ProductId, IngredientID, MixingType, ";
            SQL += " (select [dbo].GetProductIngredientSubRecipeName(IngredientID,MixingType,'" + restid + "')) As IngSubRcpName, ";
            SQL += " (select [dbo].GetIngredient_SubRcpUnit(IngredientID,ProductId ,MixingType,'" + restid + "')) as Unit from ";
            SQL += " omni_Product_Ingredient_SubreciepeDetails ";

            if (whfld != "" && whlval != "")
                SQL += " where " + whfld + " ='" + whlval + "'";

            if (myDictionary != null)
            {
                List<string> list = new List<string>(myDictionary.Keys);

                int i = 0;
                // Loop through list
                foreach (string k in list)
                {
                    if ((whlval == "" || whlval == null) && i == 0)
                        addl = " where " + k + "= '" + myDictionary[k] + "' ";
                    else
                        addl += " And " + k + "= '" + myDictionary[k] + "' ";
                    i++;
                }
            }

            SQL += addl;
            return SQL;
        }

        public string getProductModifiersSQL(Dictionary<string, string> myDictionary, string whfld, string whlval)
        {
            addl = "";
            SQL = "select Product_ModifierID, ModifierID, ProductID  from omni_Product_Modifiers";

            if (whfld != "" && whlval != "")
                SQL += " where " + whfld + " ='" + whlval + "'";

            if (myDictionary != null)
            {
                List<string> list = new List<string>(myDictionary.Keys);

                // Loop through list
                foreach (string k in list)
                {
                    addl += " And " + k + "= '" + myDictionary[k] + "' ";
                }
            }

            SQL += addl;
            return SQL;
        }

        public string getProductCookingOptionSQL(Dictionary<string, string> myDictionary, string whfld, string whlval)
        {
            addl = "";
            SQL = "select Product_CookingOptionID, OptionID, ProductID  from omni_Product_Cooking_Options";

            if (whfld != "" && whlval != "")
                SQL += " where " + whfld + " ='" + whlval + "'";

            if (myDictionary != null)
            {
                List<string> list = new List<string>(myDictionary.Keys);

                // Loop through list
                foreach (string k in list)
                {
                    addl += " And " + k + "= '" + myDictionary[k] + "' ";
                }
            }

            SQL += addl;
            return SQL;
        }

        public string getProductKitchenPrinterSQL(Dictionary<string, string> myDictionary, string whfld, string whlval)
        {
            addl = "";
            SQL = "select Product_Ktch_PrntID, ProductID, OptionID, (case when OptionType='P' then 'Printer' else 'Kitchen' end) as OptionName, OptionType";
            SQL += " from omni_Product_Kitchen_Printer_Options";

            if (whfld != "" && whlval != "")
                SQL += " where " + whfld + " ='" + whlval + "'";

            if (myDictionary != null)
            {
                List<string> list = new List<string>(myDictionary.Keys);

                // Loop through list
                foreach (string k in list)
                {
                    addl += " And " + k + "= '" + myDictionary[k] + "' ";
                }
            }

            SQL += addl + " Order By OptionType";
            return SQL;
        }

        public string getModiferLevelSQL(int restid)
        {
            SQL = "select CAST('' as varchar(10)) as LevelID, '' as ModifierLevelName, IsActive as Status from omni_Modifiers_Level";
            SQL += " union ";
            SQL += "select CAST(LevelID as varchar(10)) as LevelID, ModifierLevelName, IsActive as Status from omni_Modifiers_Level where Rest_ID=" + restid;
            return SQL;
        }

        public string getModiferLevelSQL(Dictionary<string, string> myDictionary, string whfld, string whlval)
        {
            SQL = "select CAST('' as varchar(10)) as LevelID, '' as ModifierLevelName, '' as Status";
            SQL += " union all ";
            SQL += "select CAST(LevelID as varchar(10)) as LevelID, ModifierLevelName, IsActive as Status from omni_Modifiers_Level";

            if (whfld != "" && whlval != "")
                SQL += " where " + whfld + " ='" + whlval + "'";

            if (myDictionary != null)
            {
                List<string> list = new List<string>(myDictionary.Keys);

                // Loop through list
                foreach (string k in list)
                {
                    addl += " And " + k + "= '" + myDictionary[k] + "' ";
                }
            }

            SQL += addl;
            return SQL;
        }


        public string getModifierSQL(int restid)
        {
            SQL = "select a.ModifierID, a.ModifierName,a.Price1, a.IsActive as Status, b.ModifierLevelName  from omni_Modifiers a ";
            SQL += "inner join omni_Modifiers_Level b on (a.ModifierLevelID = b.LevelID) and a.IsActive=1 and a.Rest_ID =" + restid;
            return SQL;
        }

        public string getModifierSQL(Dictionary<string, string> myDictionary, string whfld, string whlval)
        {
            addl = "";

            SQL = "select a.ModifierID, a.ModifierName,a.Name2, a.Price1, IsNull(a.Price2,0.00) as Price2, Isnull(a.Description,'') as Description, a.ModifierLevelID, a.GST, a.SortOrder, a.IsActive as Status, a.CreatedByUserID, a.CreatedOn, a.ModifiedByUserID, a.ModifiedOn,  b.ModifierLevelName  from omni_Modifiers a ";
            SQL += "inner join omni_Modifiers_Level b on (a.ModifierLevelID = b.LevelID)";

            if (whfld != "" && whlval != "")
                SQL += " where a." + whfld + " ='" + whlval + "'";

            if (myDictionary != null)
            {
                List<string> list = new List<string>(myDictionary.Keys);

                // Loop through list
                foreach (string k in list)
                {
                    addl += " And " + k + "= '" + myDictionary[k] + "' ";
                }
            }

            SQL += addl;
            return SQL;
        }

        public string GetSaleInfoForXReportSQL(Dictionary<string, string> myDictionary, string val1, string val2)
        {
            addl = "";

            /*          SQL = "select a.OrderID,TransactionDate,TableId,NoOfGuest,UserID,DeviceID,GrossAmount,TipAmount,TotalTax, Surcharge, Discount,TotalAmount,";
                        SQL += "b.PaymentTypeID,b.PaidAmount,c.PaymentType  from omni_OrderInfo a inner join omni_OrderPaymentInfo b on (a.OrderID=b.OrderId)";
                        SQL += " inner join omni_PaymentType c on (b.PaymentTypeID=c.PaymentTypeID) ";
                        SQL += " and CONVERT(VARCHAR(26), a.TransactionDate, 23)>='" + val1 + "' and CONVERT(VARCHAR(26), a.TransactionDate, 23) <='" + val2 + "'";
            */

            /*            SQL = "select Isnull(sum(TipAmount),0.00) as TipAmount, isnull(sum(TotalTax),0.00) as TotalTax, isnull(sum(Surcharge),0.00) as Surcharge, isnull(sum(Discount),0.00) as Discount,isnull(sum(TotalAmount),0.00) as TotalAmount,";
                        SQL += "isnull(sum(b.PaidAmount),0.00) as PaidAmount from omni_OrderInfo a inner join omni_OrderPaymentInfo b on (a.Order_TranID=b.Order_TranID)";
                        SQL += " inner join omni_PaymentType c on (b.PaymentTypeID=c.PaymentTypeID) "; */

            //            SQL = "select Isnull(sum(TipAmount),0.00) as TipAmount, isnull(sum(TotalTax),0.00) as TotalTax, isnull(sum(Surcharge),0.00) as Surcharge, isnull(sum(Discount),0.00) as Discount,isnull(sum(TotalAmount),0.00) as TotalAmount from omni_OrderInfo";

            SQL = "select Isnull(sum(TipAmount),0.00) as TipAmount, ISNULL( sum(Convert(Decimal(10,2),(TotalTax - ((TotalTax * AdjustmentPercent) /100)))),0) as TotalTax, isnull(sum(Surcharge),0.00) as Surcharge, isnull(sum(Discount),0.00) as Discount,isnull(sum(TotalAmount),0.00) as TotalAmount from omni_OrderInfo ";

            /*if (val1 == "")
                SQL += " and CONVERT(VARCHAR(24), a.TransactionDate, 120) <='" + val2 + "'";
            else
                SQL += " and CONVERT(VARCHAR(24), a.TransactionDate, 120)>='" + val1 + "' and CONVERT(VARCHAR(24), a.TransactionDate, 120) <='" + val2 + "'";
            */

            if (val1 == val2)
                SQL += " where CONVERT(VARCHAR(24), TransactionDate, 120) >='" + val1 + "' and CONVERT(VARCHAR(24), TransactionDate, 120) <='" + val2 + "' ";
            else
                SQL += " where CONVERT(VARCHAR(24), TransactionDate, 120)>='" + val1 + "' and CONVERT(VARCHAR(24), TransactionDate, 120) <='" + val2 + "'";


            if (myDictionary != null)
            {
                List<string> list = new List<string>(myDictionary.Keys);

                // Loop through list
                foreach (string k in list)
                {
                    addl += " And " + k + "= '" + myDictionary[k] + "' ";
                }
            }

            SQL += addl;

            //HttpContext.Current.Response.Write(SQL);
            return SQL;
        }

        public string GetSaleInfoForXReportSQL(Dictionary<string, string> myDictionary, string val)
        {
            addl = "";
            SQL = "select Isnull(sum(TipAmount),0.00) as TipAmount, isnull(sum(TotalTax),0.00) as TotalTax, isnull(sum(Surcharge),0.00) as Surcharge, isnull(sum(Discount),0.00) as Discount,isnull(sum(TotalAmount),0.00) as TotalAmount,";
            SQL += "isnull(sum(b.PaidAmount),0.00) as PaidAmount from omni_OrderInfo a inner join omni_OrderPaymentInfo b on (a.Order_TranID=b.Order_TranID)";
            SQL += " inner join omni_PaymentType c on (b.PaymentTypeID=c.PaymentTypeID) ";

            if (val != "" && val != null)
                SQL += " and CONVERT(VARCHAR(26), a.TransactionDate, 23)>'" + val + "'";

            if (myDictionary != null)
            {
                List<string> list = new List<string>(myDictionary.Keys);

                // Loop through list
                foreach (string k in list)
                {
                    addl += " And " + k + "= '" + myDictionary[k] + "' ";
                }
            }

            SQL += addl;
            //HttpContext.Current.Response.Write(SQL);
            return SQL;
        }

        public string GetTotalReduceAmtSQL(Dictionary<string, string> myDictionary, string val1, string val2)
        {
            addl = "";

            SQL = "select Isnull(sum(ReduceSaleAmt),0.00) as TotalReduceSaleAmt from omni_OrderInfo";

            if (val1 == "")
                SQL += " whewre CONVERT(VARCHAR(24), TransactionDate, 120) <='" + val2 + "'";
            else
                SQL += " where CONVERT(VARCHAR(24), TransactionDate, 120)>='" + val1 + "' and CONVERT(VARCHAR(24), TransactionDate, 120) <='" + val2 + "'";

            if (myDictionary != null)
            {
                List<string> list = new List<string>(myDictionary.Keys);

                // Loop through list
                foreach (string k in list)
                {
                    addl += " And " + k + "= '" + myDictionary[k] + "' ";
                }
            }

            SQL += addl;
            //HttpContext.Current.Response.Write(SQL);
            return SQL;
        }
        public string GetAllRestaurentIdandNameSQL()
        {
            SQL = " SELECT  Rest_Id,RestName from omni_Restuarnt_info where IsActive=1";

            return SQL;
        }

        public string GetAllUserIdandNameForRestIDSQL(string restId)
        {
            SQL = " SELECT  UserId, (FirstName + ' ' + LastName) as UserName from omni_users where Rest_id = " + restId;

            return SQL;
        }


        public string GetTotalSaleByPaymentTypeForXReportSQL(Dictionary<string, string> myDictionary, string val1, string val2)
        {
            addl = "";

            //            SQL = "select Isnull(sum(b.PaidAmount),0.00) as PaidAmount from omni_OrderInfo a inner join omni_OrderPaymentInfo b on (a.Order_TranID=b.Order_TranID)";
            //            SQL += " inner join omni_PaymentType c on (b.PaymentTypeID=c.PaymentTypeID) ";

            PSQL = "select Isnull(sum(b.PaidAmount),0.00) as PaidAmount from omni_OrderInfo a ";
            SQL = " inner join omni_OrderPaymentInfo b on (a.Order_TranID=b.Order_TranID)";
            SQL += " inner join omni_PaymentType c on (b.PaymentTypeID=c.PaymentTypeID) ";

            if (val1 == val2)
                SQL += " where CONVERT(VARCHAR(24), a.TransactionDate, 120) >='" + val1 + "' and CONVERT(VARCHAR(24), a.TransactionDate, 120) <='" + val2 + "'";
            else
                SQL += " where CONVERT(VARCHAR(24), a.TransactionDate, 120)>='" + val1 + "' and CONVERT(VARCHAR(24), a.TransactionDate, 120) <='" + val2 + "'";

            if (myDictionary != null)
            {
                List<string> list = new List<string>(myDictionary.Keys);

                // Loop through list
                foreach (string k in list)
                {
                    if (k == "b.PaymentTypeID" && myDictionary[k] == "1")
                        PSQL = "select isnull(sum(Convert(Decimal(10,2),(b.PaidAmount - ((b.PaidAmount * a.AdjustmentPercent)/100)))),0) as PaidAmount from omni_OrderInfo a ";

                    addl += " And " + k + "= '" + myDictionary[k] + "' ";
                }
            }

            //SQL += addl;
            SQL = PSQL + SQL + addl;
            //HttpContext.Current.Response.Write(SQL);
            return SQL;
        }

        public string GetCashSaleByPaymentTypeForLocationComparisonSQL(Dictionary<string, string> myDictionary, string val1, string val2)
        {
            addl = "";

            //            SQL = "select Isnull(sum(b.PaidAmount),0.00) as PaidAmount from omni_OrderInfo a inner join omni_OrderPaymentInfo b on (a.Order_TranID=b.Order_TranID)";
            //            SQL += " inner join omni_PaymentType c on (b.PaymentTypeID=c.PaymentTypeID) ";

            SQL = "select d.RestName, Isnull(sum(b.PaidAmount),0.00) as CashSale, Isnull(sum(a.TotalTax),0.00) as GST from omni_OrderInfo a ";
            SQL += " inner join omni_OrderPaymentInfo b on (a.Order_TranID=b.Order_TranID)";
            SQL += "inner join omni_PaymentType c on (b.PaymentTypeID=c.PaymentTypeID) ";
            SQL += "inner join omni_Restuarnt_info d on (a.Rest_id=d.Rest_id)";

            if (val1 == val2)
                SQL += " where CONVERT(VARCHAR(24), a.TransactionDate, 120) >='" + val1 + "' and CONVERT(VARCHAR(24), a.TransactionDate, 120) <='" + val2 + "' and c.PaymentTypeID='1'";
            else
                SQL += " where CONVERT(VARCHAR(24), a.TransactionDate, 120)>='" + val1 + "' and CONVERT(VARCHAR(24), a.TransactionDate, 120) <='" + val2 + "'and c.PaymentTypeID='1'";


            if (myDictionary != null)
            {
                List<string> list = new List<string>(myDictionary.Keys);

                // Loop through list
                foreach (string k in list)
                {
                    // if (k == "b.PaymentTypeID" && myDictionary[k] == "1")
                    //    PSQL = "select isnull(sum(Convert(Decimal(10,2),(b.PaidAmount - ((b.PaidAmount * a.AdjustmentPercent)/100)))),0) as PaidAmount from omni_OrderInfo a ";

                    addl += " And " + k + "= '" + myDictionary[k] + "' ";
                }
            }

            //SQL += addl;
            SQL += addl + " Group By d.RestName;";
            //HttpContext.Current.Response.Write(SQL);
            return SQL;
        }

        public string GetCardSaleByPaymentTypeForLocationComparisonSQL(Dictionary<string, string> myDictionary, string val1, string val2)
        {
            addl = "";

            //            SQL = "select Isnull(sum(b.PaidAmount),0.00) as PaidAmount from omni_OrderInfo a inner join omni_OrderPaymentInfo b on (a.Order_TranID=b.Order_TranID)";
            //            SQL += " inner join omni_PaymentType c on (b.PaymentTypeID=c.PaymentTypeID) ";

            SQL = "select d.RestName, Isnull(sum(b.PaidAmount),0.00) as CardSale, Isnull(sum(a.TotalTax),0.00) as GST from omni_OrderInfo a ";
            SQL += " inner join omni_OrderPaymentInfo b on (a.Order_TranID=b.Order_TranID)";
            SQL += "inner join omni_PaymentType c on (b.PaymentTypeID=c.PaymentTypeID) ";
            SQL += "inner join omni_Restuarnt_info d on (a.Rest_id=d.Rest_id)";

            if (val1 == val2)
                SQL += " where CONVERT(VARCHAR(24), a.TransactionDate, 120) >='" + val1 + "' and CONVERT(VARCHAR(24), a.TransactionDate, 120) <='" + val2 + "' and c.PaymentTypeID='2'";
            else
                SQL += " where CONVERT(VARCHAR(24), a.TransactionDate, 120)>='" + val1 + "' and CONVERT(VARCHAR(24), a.TransactionDate, 120) <='" + val2 + "'and c.PaymentTypeID='2'";


            if (myDictionary != null)
            {
                List<string> list = new List<string>(myDictionary.Keys);

                // Loop through list
                foreach (string k in list)
                {
                    // if (k == "b.PaymentTypeID" && myDictionary[k] == "1")
                    //    PSQL = "select isnull(sum(Convert(Decimal(10,2),(b.PaidAmount - ((b.PaidAmount * a.AdjustmentPercent)/100)))),0) as PaidAmount from omni_OrderInfo a ";

                    addl += " And " + k + "= '" + myDictionary[k] + "' ";
                }
            }

            //SQL += addl;
            SQL += " Group By d.RestName;";
            //HttpContext.Current.Response.Write(SQL);
            return SQL;
        }

        public string GetVoucherSaleByPaymentTypeForLocationComparisonSQL(Dictionary<string, string> myDictionary, string val1, string val2)
        {
            addl = "";

            //            SQL = "select Isnull(sum(b.PaidAmount),0.00) as PaidAmount from omni_OrderInfo a inner join omni_OrderPaymentInfo b on (a.Order_TranID=b.Order_TranID)";
            //            SQL += " inner join omni_PaymentType c on (b.PaymentTypeID=c.PaymentTypeID) ";

            SQL = "select d.RestName, Isnull(sum(b.PaidAmount),0.00) as VoucherSale, Isnull(sum(a.TotalTax),0.00) as GST from omni_OrderInfo a ";
            SQL += " inner join omni_OrderPaymentInfo b on (a.Order_TranID=b.Order_TranID)";
            SQL += "inner join omni_PaymentType c on (b.PaymentTypeID=c.PaymentTypeID) ";
            SQL += "inner join omni_Restuarnt_info d on (a.Rest_id=d.Rest_id)";

            if (val1 == val2)
                SQL += " where CONVERT(VARCHAR(24), a.TransactionDate, 120) >='" + val1 + "' and CONVERT(VARCHAR(24), a.TransactionDate, 120) <='" + val2 + "' and c.PaymentTypeID='3'";
            else
                SQL += " where CONVERT(VARCHAR(24), a.TransactionDate, 120)>='" + val1 + "' and CONVERT(VARCHAR(24), a.TransactionDate, 120) <='" + val2 + "'and c.PaymentTypeID='3'";


            if (myDictionary != null)
            {
                List<string> list = new List<string>(myDictionary.Keys);

                // Loop through list
                foreach (string k in list)
                {
                    // if (k == "b.PaymentTypeID" && myDictionary[k] == "1")
                    //    PSQL = "select isnull(sum(Convert(Decimal(10,2),(b.PaidAmount - ((b.PaidAmount * a.AdjustmentPercent)/100)))),0) as PaidAmount from omni_OrderInfo a ";

                    addl += " And " + k + "= '" + myDictionary[k] + "' ";
                }
            }

            //SQL += addl;
            SQL += " Group By d.RestName;";
            //HttpContext.Current.Response.Write(SQL);
            return SQL;
        }
        public string GetTotalSaleByPaymentTypeForXReportSQL(Dictionary<string, string> myDictionary, string val)
        {
            addl = "";

            SQL = "select Isnull(sum(b.PaidAmount),0.00) as PaidAmount from omni_OrderInfo a inner join omni_OrderPaymentInfo b on (a.Order_TranID=b.Order_TranID)";
            SQL += " inner join omni_PaymentType c on (b.PaymentTypeID=c.PaymentTypeID) ";

            if (val != "" && val != null)
                SQL += " and CONVERT(VARCHAR(26), a.TransactionDate, 23)>='" + val + "'";

            if (myDictionary != null)
            {
                List<string> list = new List<string>(myDictionary.Keys);

                // Loop through list
                foreach (string k in list)
                {
                    addl += " And " + k + "= '" + myDictionary[k] + "' ";
                }
            }

            SQL += addl;
            return SQL;
        }

        public string GetTotalInDrawerForXReportSQL(Dictionary<string, string> myDictionary, string val1, string val2)
        {
            addl = "";

            /*            SQL = "select a.DeviceID, c.PrinterName, sum(TotalAmount) as Total from omni_OrderInfo a ";
                        SQL += "inner join omni_Device b on (a.DeviceID = b.DeviceID) right join omni_Printers c on (b.PrinterID = c.PrinterId) ";
                        SQL += " where CONVERT(VARCHAR(24), TransactionDate, 120) >='" + val1 + "' and CONVERT(VARCHAR(24), TransactionDate, 120) <='" + val2 + "'";

            */
            SQL = "select outerq.*,isnull(g.amount,0) as PayOut, isnull(h.amount,0) as Refund  from ";
            SQL += "(select lt.deviceid,lt.printername,sum(lt.CashSale) as CashSale,sum(lt.CardSale) as CardSale,sum(lt.VoucherSale) as VoucherSale from ";
            SQL += "(select a.DeviceID, c.PrinterName, Isnull((select PaidAmount from omni_orderpaymentinfo ";
            SQL += "where paymenttypeid=1 and order_tranid = a.order_tranid),0) as CashSale , ";
            SQL += "Isnull((select PaidAmount from omni_orderpaymentinfo where paymenttypeid=2 and order_tranid = a.order_tranid),0) as CardSale, ";
            SQL += "isnull((select PaidAmount from omni_orderpaymentinfo where paymenttypeid=3 and order_tranid = a.order_tranid),0) as VoucherSale from omni_OrderInfo a inner join omni_Device b on (a.DeviceID = b.DeviceID) ";
            SQL += "right join omni_Printers c on (b.PrinterID = c.PrinterId)";
            SQL += " where CONVERT(VARCHAR(24), a.TransactionDate, 120)>='" + val1 + "' and CONVERT(VARCHAR(24), a.TransactionDate, 120)<='" + val2 + "' ";


            if (myDictionary != null)
            {
                List<string> list = new List<string>(myDictionary.Keys);

                // Loop through list
                foreach (string k in list)
                {
                    addl += " And " + k + "= '" + myDictionary[k] + "' ";
                }
            }

            //            SQL += addl + " group by a.DeviceID,c.PrinterName";
            SQL += addl;		//a.Rest_id=

            SQL += ") as lt group by lt.deviceid,lt.printername) as outerq ";
            SQL += "left join omni_Payout g on ( (outerq.deviceid = g.deviceid) ";
            SQL += "and CONVERT(VARCHAR(24), g.TransactionDate, 120)>='" + val1 + "' and CONVERT(VARCHAR(24), g.TransactionDate, 120)<='" + val2 + "' )";
            SQL += "left join omni_refund h on ( (outerq.deviceid = h.deviceid) and CONVERT(VARCHAR(24), h.refundDate, 120)>='" + val1 + "' and CONVERT(VARCHAR(24), h.refundDate, 120)<='" + val2 + "')";

            return SQL;
        }

        public string GetTotalInDrawerForXReportSQL(Dictionary<string, string> myDictionary, string val)
        {
            addl = "";

            SQL = "select a.DeviceID, c.PrinterName, sum(TotalAmount) as Total from omni_OrderInfo a ";
            SQL += "inner join omni_Device b on (a.DeviceID = b.DeviceID) right join omni_Printers c on (b.PrinterID = c.PrinterId) ";

            if (val != "" && val != null)
                SQL += " where CONVERT(VARCHAR(24), TransactionDate, 120) >='" + val + "'";

            if (myDictionary != null)
            {
                List<string> list = new List<string>(myDictionary.Keys);

                // Loop through list
                int i = 0;

                foreach (string k in list)
                {
                    if ((val == "" || val == null) && i == 0)
                        addl = " where " + k + "= '" + myDictionary[k] + "' ";
                    else
                        addl += " And " + k + "= '" + myDictionary[k] + "' ";
                    i++;
                }
            }

            SQL += addl + " group by a.DeviceID,c.PrinterName";
            return SQL;
        }

        public string GetEmployeeBreakUpSaleForZReportSQL(Dictionary<string, string> myDictionary, string val1, string val2)
        {
            addl = "";

            SQL = "select isnull(usr.FirstName,'') + '' + isnull(usr.LastName,'') as Employee,  sale.UserID,SUM(col1) as CashSale,SUM(col2) as CardSale,SUM(col3) as VoucherSale, ";
            SQL += "(SUM(col1)+SUM(col2)+SUM(col3)) as Total from ";
            //          SQL += "(select a.UserID, (select isnull(sum(Convert(Decimal(14,2),(PaidAmount - ((PaidAmount * a.ReduceSaleInPercent) /100)))),0) from omni_OrderPaymentInfo where Order_TranID=a.Order_TranID and PaymentTypeID=1 ) col1, ";
            SQL += "(select a.UserID, (select isnull(Convert(Decimal(14,2),(PaidAmount - ((PaidAmount * a.AdjustmentPercent) /100))),0.00) from omni_OrderPaymentInfo where Order_TranID=a.Order_TranID and PaymentTypeID=1 ) col1, ";
            SQL += "(select isnull(sum(PaidAmount),0.00) from omni_OrderPaymentInfo where Order_TranID=a.Order_TranID and PaymentTypeID=2) col2, ";
            SQL += "(select isnull(sum(PaidAmount),0.00) from omni_OrderPaymentInfo where Order_TranID=a.Order_TranID and PaymentTypeID=3) col3 ";


            if (val1 == val2)
                SQL += "from omni_OrderInfo a where CONVERT(VARCHAR(24), TransactionDate, 120) >='" + val1 + "' and CONVERT(VARCHAR(24), TransactionDate, 120) <='" + val2 + "'";
            else
                SQL += "from omni_OrderInfo a where  CONVERT(VARCHAR(24), TransactionDate, 120)>='" + val1 + "' and CONVERT(VARCHAR(24), TransactionDate, 120) <='" + val2 + "'";

            if (myDictionary != null)
            {
                List<string> list = new List<string>(myDictionary.Keys);

                // Loop through list
                foreach (string k in list)
                {
                    addl += " And " + k + "= '" + myDictionary[k] + "' ";
                }
            }

            SQL += addl + " ) as sale  inner join omni_users usr on (sale.UserID = usr.UserId) group by sale.UserID,usr.FirstName,usr.LastName";
            return SQL;
        }

        public string GetEmployeeBreakUpSaleForZReportSQL(Dictionary<string, string> myDictionary, string val)
        {
            addl = "";

            SQL = "select isnull(usr.FirstName,'') + '' + isnull(usr.LastName,'') as Employee,  sale.UserID,SUM(col1) as CashSale,SUM(col2) as CardSale,SUM(col3) as VoucherSale, ";
            SQL += "(SUM(col1)+SUM(col2)+SUM(col3)) as Total from ";
            SQL += "(select a.UserID, (select isnull(sum(PaidAmount),0.00) from omni_OrderPaymentInfo where Order_TranID=a.Order_TranID and PaymentTypeID=1 ) col1, ";
            SQL += "(select isnull(sum(PaidAmount),0.00) from omni_OrderPaymentInfo where Order_TranID=a.Order_TranID and PaymentTypeID=2) col2, ";
            SQL += "(select isnull(sum(PaidAmount),0.00) from omni_OrderPaymentInfo where Order_TranID=a.Order_TranID and PaymentTypeID=3) col3 from omni_OrderInfo a ";

            if (val != "" && val != null)
                SQL += " where  CONVERT(VARCHAR(26), TransactionDate, 23)>='" + val + "'";

            if (myDictionary != null)
            {
                List<string> list = new List<string>(myDictionary.Keys);

                // Loop through list
                int i = 0;
                foreach (string k in list)
                {
                    if ((val == "" || val == null) && i == 0)
                        addl = " where " + k + "= '" + myDictionary[k] + "' ";
                    else
                        addl += " And " + k + "= '" + myDictionary[k] + "' ";
                    i++;
                }
            }

            SQL += addl + " ) as sale  inner join omni_users usr on (sale.UserID = usr.UserId) group by sale.UserID,usr.FirstName,usr.LastName";
            return SQL;
        }

        public string GetCategoryBreakUpSaleForZReportSQL(Dictionary<string, string> myDictionary, string val1, string val2)
        {
            addl = "";

            SQL = "select sum(b.Amount) as Amount, d.CategoryName, d.CategoryID from omni_OrderInfo a inner join omni_Order_ProductDetails b on a.Order_TranID = b.Order_TranID ";
            SQL += "right join omni_Products c on (c.ProductID = b.ProductID) right join omni_Item_Categories d on (d.CategoryId=c.CategoryID) ";

            // new            SQL = "select sum(b.Amount) as Amount, d.CategoryName from omni_OrderInfo a left join omni_Order_ProductDetails b on a.Order_TranID = b.Order_TranID ";
            //            SQL += "inner join omni_Products c on (c.ProductID = b.ProductID) inner join omni_Item_Categories d on (d.CategoryId=c.CategoryID) ";

            if (val1 == val2)
                SQL += "where CONVERT(VARCHAR(24), TransactionDate, 120) >='" + val1 + "' and CONVERT(VARCHAR(24), TransactionDate, 120) <='" + val2 + "'";
            else
                SQL += " where CONVERT(VARCHAR(24), TransactionDate, 120)>='" + val1 + "' and CONVERT(VARCHAR(24), TransactionDate, 120) <='" + val2 + "'";

            if (myDictionary != null)
            {
                List<string> list = new List<string>(myDictionary.Keys);

                // Loop through list
                foreach (string k in list)
                {
                    addl += " And " + k + "= '" + myDictionary[k] + "' ";
                }
            }

            SQL += addl + " group by d.CategoryName,d.CategoryID";
            return SQL;
        }

        public string GetCategoryQtyBreakUpSaleForZReportSQL(Dictionary<string, string> myDictionary, string val1, string val2)
        {
            addl = "";

            SQL = "select sum(b.Qty) as Quantity, d.CategoryName, d.CategoryID from omni_OrderInfo a inner join omni_Order_ProductDetails b on a.Order_TranID = b.Order_TranID ";
            SQL += "right join omni_Products c on (c.ProductID = b.ProductID) right join omni_Item_Categories d on (d.CategoryId=c.CategoryID) ";

            // new            SQL = "select sum(b.Amount) as Amount, d.CategoryName from omni_OrderInfo a left join omni_Order_ProductDetails b on a.Order_TranID = b.Order_TranID ";
            //            SQL += "inner join omni_Products c on (c.ProductID = b.ProductID) inner join omni_Item_Categories d on (d.CategoryId=c.CategoryID) ";

            if (val1 == "")
                SQL += "where CONVERT(VARCHAR(24), TransactionDate, 120) <='" + val2 + "'";
            else
                SQL += " where CONVERT(VARCHAR(24), TransactionDate, 120)>='" + val1 + "' and CONVERT(VARCHAR(24), TransactionDate, 120) <='" + val2 + "'";

            if (myDictionary != null)
            {
                List<string> list = new List<string>(myDictionary.Keys);

                // Loop through list
                foreach (string k in list)
                {
                    addl += " And " + k + "= '" + myDictionary[k] + "' ";
                }
            }

            SQL += addl + " group by d.CategoryName,d.CategoryID";
            return SQL;
        }

        //returns category cash sales ;
        //TODO: add isPartialPayment to differentiate partial payments
        public string getCashSubCategoryBreakUpSaleSQL(Dictionary<string, string> myDictionary, string val1, string val2)
        {
            addl = "";

            SQL = "select sum(b.Amount) as Amount, d.CategoryName, d.CategoryID from omni_OrderInfo a inner join omni_Order_ProductDetails b on a.Order_TranID = b.Order_TranID ";
            SQL += "right join omni_Products c on (c.ProductID = b.ProductID) right join omni_Item_Categories d on (d.CategoryId=c.CategoryID) right join omni_OrderPaymentInfo e on (a.Order_TranID = e.Order_TranID) and (e.PaymentTypeID = 1)";

            // new            SQL = "select sum(b.Amount) as Amount, d.CategoryName from omni_OrderInfo a left join omni_Order_ProductDetails b on a.Order_TranID = b.Order_TranID ";
            //            SQL += "inner join omni_Products c on (c.ProductID = b.ProductID) inner join omni_Item_Categories d on (d.CategoryId=c.CategoryID) ";

            if (val1 == "")
                SQL += "where CONVERT(VARCHAR(24), TransactionDate, 120) <='" + val2 + "'";
            else
                SQL += " where CONVERT(VARCHAR(24), TransactionDate, 120)>='" + val1 + "' and CONVERT(VARCHAR(24), TransactionDate, 120) <='" + val2 + "'";

            if (myDictionary != null)
            {
                List<string> list = new List<string>(myDictionary.Keys);

                // Loop through list
                foreach (string k in list)
                {
                    addl += " And " + k + "= '" + myDictionary[k] + "' ";
                }
            }

            SQL += addl + " group by d.CategoryName,d.CategoryID";
            return SQL;
        }

        public string GetCategoryBreakUpSaleForZReportSQL(Dictionary<string, string> myDictionary, string val)
        {
            addl = "";

            SQL = "select sum(b.Amount) as Amount, d.CategoryName from omni_OrderInfo a inner join omni_Order_ProductDetails b on a.Order_TranID = b.Order_TranID ";
            SQL += "right join omni_Products c on (c.ProductID = b.ProductID) right join omni_Item_Categories d on (d.CategoryId=c.CategoryID) ";

            if (val != "" && val != null)
                SQL += "where CONVERT(VARCHAR(26), TransactionDate, 23)>='" + val + "'";

            if (myDictionary != null)
            {
                List<string> list = new List<string>(myDictionary.Keys);

                // Loop through list
                int i = 0;

                foreach (string k in list)
                {
                    if ((val == "" || val == null) && i == 0)
                        addl = " where " + k + "= '" + myDictionary[k] + "' ";
                    else
                        addl += " And " + k + "= '" + myDictionary[k] + "' ";
                    i++;
                }
            }

            SQL += addl + " group by d.CategoryName";
            return SQL;
        }

        public string GetProductOptionBreakUpSaleForZReportSQL(Dictionary<string, string> myDictionary, string val1, string val2)
        {
            addl = "";
            SQL = "select sum(b.Amount) as Amount,b.Qty ,isnull(b.ModifierIDS,'') as Modifiers ,c.ProductName from omni_OrderInfo a ";
            SQL += "inner join omni_Order_ProductDetails b on a.Order_TranID = b.Order_TranID right join omni_Products c on (c.ProductID = b.ProductID) ";

            if (val1 == val2)
                SQL += " where CONVERT(VARCHAR(24), TransactionDate, 120) >='" + val1 + "' and CONVERT(VARCHAR(24), TransactionDate, 120) <='" + val2 + "'";
            else
                SQL += " where CONVERT(VARCHAR(24), TransactionDate, 120) >='" + val1 + "' and CONVERT(VARCHAR(24), TransactionDate, 120) <='" + val2 + "'";

            if (myDictionary != null)
            {
                List<string> list = new List<string>(myDictionary.Keys);

                // Loop through list
                foreach (string k in list)
                {
                    addl += " And " + k + "= '" + myDictionary[k] + "' ";
                }
            }

            SQL += addl + " group by c.ProductName,b.Qty,b.ModifierIDS";
            return SQL;
        }

        public string GetProductOptionBreakUpSaleForZReportSQL(Dictionary<string, string> myDictionary, string val)
        {
            addl = "";
            SQL = "select sum(b.Amount) as Amount,b.Qty ,isnull(b.ModifierIDS,'') as Modifiers ,c.ProductName from omni_OrderInfo a ";
            SQL += "inner join omni_Order_ProductDetails b on a.Order_TranID = b.Order_TranID right join omni_Products c on (c.ProductID = b.ProductID) ";

            if (val != "" && val != null)
                SQL += "where CONVERT(VARCHAR(26), TransactionDate, 23)>='" + val + "'";

            if (myDictionary != null)
            {
                List<string> list = new List<string>(myDictionary.Keys);

                // Loop through list
                int i = 0;

                foreach (string k in list)
                {
                    if ((val == "" || val == null) && i == 0)
                        addl = " where " + k + "= '" + myDictionary[k] + "' ";
                    else
                        addl += " And " + k + "= '" + myDictionary[k] + "' ";
                    i++;
                }
            }

            SQL += addl + " group by c.ProductName,b.Qty,b.ModifierIDS";
            return SQL;
        }

        public string GetProductBreakUpSaleForZReportSQL(Dictionary<string, string> myDictionary, string val1, string val2)
        {
            addl = "";
            //SQL = "select sum(b.Amount) as Amount,b.Qty ,c.ProductName from omni_OrderInfo a ";
            SQL = "select sum(b.Amount) as Amount,sum(b.Qty) as Qty ,c.ProductName,c.CategoryID,c.ProductID,b.Qty from omni_OrderInfo a ";
            SQL += "inner join omni_Order_ProductDetails b on a.Order_TranID = b.Order_TranID ";
            SQL += "right join omni_Products c on (c.ProductID = b.ProductID) ";

            if (val1 == val2)
                SQL += "where CONVERT(VARCHAR(24), TransactionDate, 120) >='" + val1 + "' and CONVERT(VARCHAR(24), TransactionDate, 120) <='" + val2 + "'";
            else
                SQL += "where CONVERT(VARCHAR(24), TransactionDate, 120)>='" + val1 + "' and CONVERT(VARCHAR(24), TransactionDate, 120) <='" + val2 + "'";


            if (myDictionary != null)
            {
                List<string> list = new List<string>(myDictionary.Keys);

                // Loop through list
                foreach (string k in list)
                {
                    addl += " And " + k + "= '" + myDictionary[k] + "' ";
                }
            }

            //SQL += addl + " group by c.ProductName,b.Qty";
            SQL += addl + " group by c.ProductName, c.CategoryID, c.ProductID,b.Qty";
            return SQL;
        }


        //fetching open items
        //open items are those which are (products) are not in the products db, but are in the order_product_details table with product_id 0
        //we could add product with 0 product_id, but left for future work
        public string GetOpenItemBreakUpSaleSQL(Dictionary<string, string> myDictionary, string val1, string val2)
        {
            addl = "";
            SQL = "select sum(b.Amount) as Amount,sum(b.Qty) as Qty from omni_OrderInfo a ";
            SQL += "inner join omni_Order_ProductDetails b on a.Order_TranID = b.Order_TranID ";
            SQL += "where b.ProductID NOT IN (select ProductID from omni_Products) And (";

            if (val1 == "")
                SQL += "CONVERT(VARCHAR(24), TransactionDate, 120) <='" + val2 + "')";
            else
                SQL += "CONVERT(VARCHAR(24), TransactionDate, 120)>='" + val1 + "' and CONVERT(VARCHAR(24), TransactionDate, 120) <='" + val2 + "')";

            if (myDictionary != null)
            {
                List<string> list = new List<string>(myDictionary.Keys);

                // Loop through list
                foreach (string k in list)
                {
                    addl += " And " + k + "= '" + myDictionary[k] + "' ";
                }
            }
            return SQL;
        }


        //returns only those orders whose payment type is cash
        public string getCashPaymentOrdersInRangeSQL(Dictionary<string, string> myDictionary, string val1, string val2)
        {
            addl = "";
            SQL = "select a.Order_TranID from omni_OrderInfo a ";
            SQL += "inner join omni_OrderPaymentInfo b on (a.Order_TranID = b.Order_TranID) and (b.PaymentTypeID = 1)";

            if (val1 == "")
                SQL += "where CONVERT(VARCHAR(24), TransactionDate, 120) <='" + val2 + "'";
            else
                SQL += "where CONVERT(VARCHAR(24), TransactionDate, 120)>='" + val1 + "' and CONVERT(VARCHAR(24), TransactionDate, 120) <='" + val2 + "'";

            if (myDictionary != null)
            {
                List<string> list = new List<string>(myDictionary.Keys);

                // Loop through list
                foreach (string k in list)
                {
                    addl += " And " + k + "= '" + myDictionary[k] + "' ";
                }
            }

            //SQL += addl + " group by c.ProductName,b.Qty";
            SQL += addl + " group by a.Order_TranID";
            return SQL;
        }

        //returns all products sale breakdown by category id
        public string getProductSoldBreakDownByCategoryIdSQL(Dictionary<string, string> myDictionary, string val1, string val2, string categoryId)
        {
            addl = "";
            //SQL = "select sum(b.Amount) as Amount,b.Qty ,c.ProductName from omni_OrderInfo a ";
            SQL = "select sum(b.Amount) as Amount,sum(b.Qty) as Qty ,c.ProductName,c.CategoryID,c.ProductID,b.Qty from omni_OrderInfo a ";
            SQL += "inner join omni_Order_ProductDetails b on a.Order_TranID = b.Order_TranID ";
            SQL += "right join omni_Products c on (c.ProductID = b.ProductID) and c.CategoryID = '" + categoryId + "'";

            if (val1 == "")
                SQL += "where CONVERT(VARCHAR(24), TransactionDate, 120) <='" + val2 + "'";
            else
                SQL += "where CONVERT(VARCHAR(24), TransactionDate, 120)>='" + val1 + "' and CONVERT(VARCHAR(24), TransactionDate, 120) <='" + val2 + "'";


            if (myDictionary != null)
            {
                List<string> list = new List<string>(myDictionary.Keys);

                // Loop through list
                foreach (string k in list)
                {
                    addl += " And " + k + "= '" + myDictionary[k] + "' ";
                }
            }

            //SQL += addl + " group by c.ProductName,b.Qty";
            SQL += addl + " group by c.ProductName, c.CategoryID, c.ProductID,b.Qty";
            return SQL;
        }

        //returns cash products sale breakdown by category id
        public string getCashProductSoldBreakDownByCategoryIdSQL(Dictionary<string, string> myDictionary, string val1, string val2, string categoryId)
        {
            addl = "";
            //SQL = "select sum(b.Amount) as Amount,b.Qty ,c.ProductName from omni_OrderInfo a ";
            SQL = "select sum(b.Amount) as Amount,sum(b.Qty) as Qty ,c.ProductName,c.CategoryID,c.ProductID from omni_OrderInfo a ";
            SQL += "inner join omni_Order_ProductDetails b on a.Order_TranID = b.Order_TranID ";
            SQL += "right join omni_Products c on (c.ProductID = b.ProductID) and c.CategoryID = '" + categoryId + "' ";
            SQL += "right join omni_OrderPaymentInfo d on (a.Order_TranID = d.Order_TranID) and (d.PaymentTypeID = 1)";

            if (val1 == "")
                SQL += "where CONVERT(VARCHAR(24), TransactionDate, 120) <='" + val2 + "'";
            else
                SQL += "where CONVERT(VARCHAR(24), TransactionDate, 120)>='" + val1 + "' and CONVERT(VARCHAR(24), TransactionDate, 120) <='" + val2 + "'";


            if (myDictionary != null)
            {
                List<string> list = new List<string>(myDictionary.Keys);

                // Loop through list
                foreach (string k in list)
                {
                    addl += " And " + k + "= '" + myDictionary[k] + "' ";
                }
            }

            //SQL += addl + " group by c.ProductName,b.Qty";
            SQL += addl + " group by c.ProductID, c.ProductName, c.CategoryID";
            return SQL;
        }

        //returns those products whose payment mode is cash spefic to a productId
        public string getCashProductsSoldInRangeByProductIdSQL(Dictionary<string, string> myDictionary, string val1, string val2, int productId)
        {
            addl = "";
            //SQL = "select sum(b.Amount) as Amount,b.Qty ,c.ProductName from omni_OrderInfo a ";
            SQL = "select a.Qty, a.Amount, a.Order_TranID, a.ProductID from omni_Order_ProductDetails a ";
            SQL += "inner join omni_OrderInfo b on (a.Order_TranID = b.Order_TranID) and (a.ProductID = " + productId + ")";
            SQL += "right join omni_OrderPaymentInfo c on (b.Order_TranID = c.Order_TranID) and (c.PaymentTypeID = 1)";

            if (val1 == "")
                SQL += "where CONVERT(VARCHAR(24), TransactionDate, 120) <='" + val2 + "'";
            else
                SQL += "where CONVERT(VARCHAR(24), TransactionDate, 120)>='" + val1 + "' and CONVERT(VARCHAR(24), TransactionDate, 120) <='" + val2 + "'";


            if (myDictionary != null)
            {
                List<string> list = new List<string>(myDictionary.Keys);

                // Loop through list
                foreach (string k in list)
                {
                    addl += " And " + k + "= '" + myDictionary[k] + "' ";
                }
            }

            SQL += addl + " group by a.Amount, a.Order_TranID, a.ProductID,a.Qty";
            return SQL;
        }

        //returns total amount (from product_orderDetails table), discount and surcharge for a order
        public string getTotalsAmountForOrderInfoSQL(Dictionary<string, string> myDictionary, string orderTansID)
        {
            addl = "";
            //SQL = "select sum(b.Amount) as Amount,b.Qty ,c.ProductName from omni_OrderInfo a ";
            SQL = "select sum(a.Amount) as TotalAmount, b.Discount, b.Surcharge from omni_Order_ProductDetails a ";
            SQL += "inner join omni_OrderInfo b on (a.Order_TranID = b.Order_TranID) and (b.Order_TranID = '" + orderTansID + "') ";

            if (myDictionary != null)
            {
                List<string> list = new List<string>(myDictionary.Keys);

                // Loop through list
                foreach (string k in list)
                {
                    addl += " And " + k + "= '" + myDictionary[k] + "' ";
                }
            }

            SQL += addl + " group by b.Discount, b.Surcharge";
            return SQL;
        }

        //returns cash paymtTransId for a order
        public string getPaymetInfoIdForOrderInfoSQL(Dictionary<string, string> myDictionary, string orderTansID)
        {
            addl = "";
            //SQL = "select sum(b.Amount) as Amount,b.Qty ,c.ProductName from omni_OrderInfo a ";
            SQL = "select a.OrderPaymentId from omni_OrderPaymentInfo a ";
            SQL += "inner join omni_OrderInfo b on (a.Order_TranID = '" + orderTansID + "') ";

            if (myDictionary != null)
            {
                List<string> list = new List<string>(myDictionary.Keys);

                // Loop through list
                foreach (string k in list)
                {
                    addl += " And " + k + "= '" + myDictionary[k] + "' ";
                }
            }

            SQL += addl + " group by a.OrderPaymentId";
            return SQL;
        }

        //returns taxable total amount (from product_orderDetails table) for a order
        public string getTotalTaxableAmountForOrderInfoSQL(Dictionary<string, string> myDictionary, string orderTansID)
        {
            addl = "";
            //SQL = "select sum(b.Amount) as Amount,b.Qty ,c.ProductName from omni_OrderInfo a ";
            SQL = "select sum(a.Amount) as TotalTaxAmount from omni_Order_ProductDetails a ";
            SQL += "inner join omni_OrderInfo b on (a.Order_TranID = b.Order_TranID) and (b.Order_TranID = '" + orderTansID + "') ";
            SQL += "right join omni_Products c on (c.ProductID = a.ProductID) and c.GST = '" + 1 + "'";

            if (myDictionary != null)
            {
                List<string> list = new List<string>(myDictionary.Keys);

                // Loop through list
                foreach (string k in list)
                {
                    addl += " And " + k + "= '" + myDictionary[k] + "' ";
                }
            }

            SQL += addl + "";
            return SQL;
        }

        public string GetItemSoldSQL(Dictionary<string, string> myDictionary, string val1, string val2)
        {
            addl = "";
            SQL = "select CONVERT(VARCHAR(20),a.TransactionDate,107) as TransactionDate, sum(b.Amount) as Amount, sum(b.Qty) as Qty ,c.ProductName from omni_OrderInfo a ";
            SQL += "inner join omni_Order_ProductDetails b on a.Order_TranID = b.Order_TranID ";
            SQL += "right join omni_Products c on (c.ProductID = b.ProductID) ";

            if (val1 == "")
                SQL += "where CONVERT(VARCHAR(24), TransactionDate, 120) <='" + val2 + "'";
            else
                SQL += "where CONVERT(VARCHAR(24), TransactionDate, 120)>='" + val1 + "' and CONVERT(VARCHAR(24), TransactionDate, 120) <='" + val2 + "'";


            if (myDictionary != null)
            {
                List<string> list = new List<string>(myDictionary.Keys);

                // Loop through list
                foreach (string k in list)
                {
                    addl += " And " + k + "= '" + myDictionary[k] + "' ";
                }
            }

            SQL += addl + " group by CONVERT(VARCHAR(20),a.TransactionDate,107) ,c.ProductName order by TransactionDate desc,Qty desc";
            //            SQL += addl + " group by a.TransactionDate ,c.ProductName order by TransactionDate desc,Qty desc";
            return SQL;
        }

        public string GetUniqueItemSoldInRangeSQL(Dictionary<string, string> myDictionary, string val1, string val2)
        {
            addl = "";
            SQL = "select sum(b.Amount) as Amount, sum(b.Qty) as Qty ,c.ProductName from omni_OrderInfo a ";
            SQL += "inner join omni_Order_ProductDetails b on a.Order_TranID = b.Order_TranID ";
            SQL += "right join omni_Products c on (c.ProductID = b.ProductID) ";

            if (val1 == val2)
                SQL += "where CONVERT(VARCHAR(24), TransactionDate, 120)>='" + val1 + "' and CONVERT(VARCHAR(24), TransactionDate, 120) <='" + val2 + "'";
            else
                SQL += "where CONVERT(VARCHAR(24), TransactionDate, 120)>='" + val1 + "' and CONVERT(VARCHAR(24), TransactionDate, 120) <='" + val2 + "'";


            if (myDictionary != null)
            {
                List<string> list = new List<string>(myDictionary.Keys);

                // Loop through list
                foreach (string k in list)
                {
                    addl += " And " + k + "= '" + myDictionary[k] + "' ";
                }
            }

            SQL += addl + " group by ProductName order by Qty desc";
            return SQL;
        }

        public string GetProductBreakUpSaleForZReportSQL(Dictionary<string, string> myDictionary, string val)
        {
            addl = "";
            SQL = "select sum(b.Amount) as Amount,b.Qty ,c.ProductName from omni_OrderInfo a ";
            SQL += "inner join omni_Order_ProductDetails b on a.Order_TranID = b.Order_TranID ";
            SQL += "right join omni_Products c on (c.ProductID = b.ProductID) ";

            if (val != "" && val != null)
                SQL += "where CONVERT(VARCHAR(26), TransactionDate, 23)>='" + val + "'";

            if (myDictionary != null)
            {
                List<string> list = new List<string>(myDictionary.Keys);

                // Loop through list
                int i = 0;
                foreach (string k in list)
                {
                    if ((val == "" || val == null) && i == 0)
                        addl = " where " + k + "= '" + myDictionary[k] + "' ";
                    else
                        addl += " And " + k + "= '" + myDictionary[k] + "' ";
                    i++;
                }
            }

            SQL += addl + " group by c.ProductName,b.Qty";
            return SQL;
        }

        public string GetTotalRefundForXReportSQL(Dictionary<string, string> myDictionary, string val1, string val2)
        {
            addl = "";

            SQL = "select Isnull(sum(Amount),0.00) as Amount from omni_Refund";

            if (val1 == val2)
                SQL += " where CONVERT(VARCHAR(24), RefundDate, 120) >='" + val1 + "' and CONVERT(VARCHAR(24), RefundDate, 120) <='" + val2 + "'";
            else
                SQL += " where CONVERT(VARCHAR(24), RefundDate, 120)>='" + val1 + "' and CONVERT(VARCHAR(24), RefundDate, 120) <='" + val2 + "'";

            if (myDictionary != null)
            {
                List<string> list = new List<string>(myDictionary.Keys);

                // Loop through list
                foreach (string k in list)
                {
                    addl += " And " + k + "= '" + myDictionary[k] + "' ";
                }
            }

            SQL += addl;
            return SQL;
        }

        public string GetTotalRefundForLocationComparisonReportSQL(Dictionary<string, string> myDictionary, string val1, string val2)
        {
            //addl = "";

            SQL = "select c.RestName, b.PaymentTypeID, Isnull(sum(a.Amount),0.00) as Refund from omni_Refund a";
            SQL += " inner join omni_PaymentType b on (a.PaymentTypeID=b.PaymentTypeID)";
            SQL += " inner join omni_Restuarnt_info c on (a.Rest_id=c.Rest_id)";

            if (val1 == val2)
                SQL += " where CONVERT(VARCHAR(24), RefundDate, 120) >='" + val1 + "' and CONVERT(VARCHAR(24), RefundDate, 120) <='" + val2 + "'";
            else
                SQL += " where CONVERT(VARCHAR(24), RefundDate, 120)>='" + val1 + "' and CONVERT(VARCHAR(24), RefundDate, 120) <='" + val2 + "'";

            if (myDictionary != null)
            {
                List<string> list = new List<string>(myDictionary.Keys);

                // Loop through list
                foreach (string k in list)
                {
                    addl += " And " + k + "= '" + myDictionary[k] + "' ";
                }
            }

            //  SQL += addl;
            SQL += "Group By c.RestName, b.PaymentTypeID";
            return SQL;
        }

        public string GetTotalRefundForXReportSQL(Dictionary<string, string> myDictionary, string val)
        {
            addl = "";

            SQL = "select Isnull(sum(Amount),0.00) as Amount from omni_Refund";

            if (val != "" && val != null)
                SQL += " where CONVERT(VARCHAR(26), RefundDate, 23)>='" + val + "'";

            if (myDictionary != null)
            {
                List<string> list = new List<string>(myDictionary.Keys);

                // Loop through list
                int i = 0;

                foreach (string k in list)
                {
                    if ((val == "" || val == null) && i == 0)
                        addl = " where " + k + "= '" + myDictionary[k] + "' ";
                    else
                        addl += " And " + k + "= '" + myDictionary[k] + "' ";
                    i++;
                }
            }

            SQL += addl;
            return SQL;
        }

        public string GetTotalPayoutForXReportSQL(Dictionary<string, string> myDictionary, string val1, string val2)
        {
            addl = "";

            SQL = "select Isnull(sum(Amount),0.00) as Amount from omni_Payout";

            if (val1 == val2)
                SQL += " where CONVERT(VARCHAR(24), TransactionDate, 120) >='" + val1 + "' and CONVERT(VARCHAR(24), TransactionDate, 120) <='" + val2 + "'";
            else
                SQL += " where CONVERT(VARCHAR(24), TransactionDate, 120)>='" + val1 + "' and CONVERT(VARCHAR(24), TransactionDate, 120) <='" + val2 + "'";

            if (myDictionary != null)
            {
                List<string> list = new List<string>(myDictionary.Keys);

                // Loop through list
                foreach (string k in list)
                {
                    addl += " And " + k + "= '" + myDictionary[k] + "' ";
                }
            }

            SQL += addl;
            return SQL;
        }

        //hourly sale
        public string getTotalHourlySaleSQL(Dictionary<string, string> myDictionary, string val1, string val2)
        {
            addl = "";


            SQL = "select DATEPART(HOUR, a.TransactionDate) as saleHour, Isnull(sum(b.PaidAmount),0.00) as amount from omni_OrderInfo a inner join omni_OrderPaymentInfo b " +
                "on (a.Order_TranID=b.Order_TranID)";

            if (val1 == val2)
                SQL += " where CONVERT(VARCHAR(24), TransactionDate, 120) >='" + val1 + "' and CONVERT(VARCHAR(24), TransactionDate, 120) <='" + val2 + "'";
            else
                SQL += " where CONVERT(VARCHAR(24), TransactionDate, 120)>='" + val1 + "' and CONVERT(VARCHAR(24), TransactionDate, 120) <='" + val2 + "'";

            if (myDictionary != null)
            {
                List<string> list = new List<string>(myDictionary.Keys);

                // Loop through list
                foreach (string k in list)
                {
                    addl += " And " + k + "= '" + myDictionary[k] + "' ";
                }
            }

            SQL += addl + " Group By DATEPART(HOUR, a.TransactionDate) Order By DATEPART(HOUR, a.TransactionDate) ASC";
            return SQL;
        }

        //user attendance report query
        public string getUserAttendanceSQL(Dictionary<string, string> myDictionary, string val1, string val2, string userId)
        {
            addl = "";

            SQL = "select RIGHT(CONVERT(VARCHAR, a.LoginTime, 100),7) as LoginTime, RIGHT(CONVERT(VARCHAR, a.LogoutTime, 100),7) ";
            SQL += " as LogoutTime, DATEDIFF(SECOND, a.LoginTime, a.LogoutTime) as Duration from omni_UserAttendance a ";
            SQL += " where a.UserID = " + userId + " ";

            if (val1 == val2)
                SQL += " and CONVERT(VARCHAR(24), a.Date, 120) >='" + val1 + "' and CONVERT(VARCHAR(24), a.Date, 120) <='" + val2 + "'";
            else
                SQL += " and CONVERT(VARCHAR(24), a.Date, 120)>='" + val1 + "' and CONVERT(VARCHAR(24), a.Date, 120) <='" + val2 + "'";

            if (myDictionary != null)
            {
                List<string> list = new List<string>(myDictionary.Keys);

                foreach (string k in list)
                {
                    addl += " And " + k + "= '" + myDictionary[k] + "' ";
                }
            }

            SQL += addl;
            return SQL;
        }

        //no sale count query
        public string getNoSaleCountSQL(Dictionary<string, string> myDictionary, string val1, string val2)
        {
            addl = "";

            SQL = "select count(a.NoSaleCount) as NoSaleCount from omni_NoSale a";

            if (val1 == val2)
                SQL += " where CONVERT(VARCHAR(24), a.Date, 120) >='" + val1 + "' and CONVERT(VARCHAR(24), a.Date, 120) <='" + val2 + "' ";
            else if (val2 == null)
                SQL += " where CONVERT(VARCHAR(24), a.Date, 120) >='" + val1 + "'";
            else
                SQL += " where CONVERT(VARCHAR(24), a.Date, 120)>='" + val1 + "' and CONVERT(VARCHAR(24), a.Date, 120) <='" + val2 + "'";

            if (myDictionary != null)
            {
                List<string> list = new List<string>(myDictionary.Keys);

                foreach (string k in list)
                {
                    addl += " And " + k + "= '" + myDictionary[k] + "' ";
                }
            }

            SQL += addl;
            return SQL;
        }

        public string GetTotalPayoutForSalesComparisonByLocationReportSQL(Dictionary<string, string> myDictionary, string val1, string val2)
        {
            //   addl = "";

            SQL = "select c.RestName, b.PaymentTypeID, Isnull(sum(a.Amount),0.00) as payout from omni_Payout a";
            SQL += " inner join omni_PaymentType b on (a.PaymentTypeID=b.PaymentTypeID)";
            SQL += " inner join omni_Restuarnt_info c on (a.Rest_id=c.Rest_id)";

            if (val1 == val2)
                SQL += " where CONVERT(VARCHAR(24), TransactionDate, 120) >='" + val1 + "' and CONVERT(VARCHAR(24), TransactionDate, 120) <='" + val2 + "'";
            else
                SQL += " where CONVERT(VARCHAR(24), TransactionDate, 120)>='" + val1 + "' and CONVERT(VARCHAR(24), TransactionDate, 120) <='" + val2 + "'";

            if (myDictionary != null)
            {
                List<string> list = new List<string>(myDictionary.Keys);

                // Loop through list
                foreach (string k in list)
                {
                    addl += " And " + k + "= '" + myDictionary[k] + "' ";
                }
            }
            // SQL += addl;
            SQL += "Group By c.RestName, b.PaymentTypeID";
            return SQL;
        }

        public string GetTotalPayoutForXReportSQL(Dictionary<string, string> myDictionary, string val)
        {
            addl = "";

            SQL = "select Isnull(sum(Amount),0.00) as Amount from omni_Payout";

            if (val != "" && val != null)
                SQL += " where CONVERT(VARCHAR(26), TransactionDate, 23)>='" + val + "'";

            if (myDictionary != null)
            {
                List<string> list = new List<string>(myDictionary.Keys);

                // Loop through list
                int i = 0;

                foreach (string k in list)
                {
                    if ((val == "" || val == null) && i == 0)
                        addl = " where " + k + "= '" + myDictionary[k] + "' ";
                    else
                        addl += " And " + k + "= '" + myDictionary[k] + "' ";
                    i++;
                }
            }

            SQL += addl;
            return SQL;
        }

        public string getOrderTransactionSQL(Dictionary<string, string> myDictionary, string val1, string val2)
        {
            addl = "";
            //SQL = "select * from omni_OrderInfo ";
            SQL = "select a.*, b.DeviceName, (ISNULL(c.firstname,'')+' ' + ISNULL(c.lastname,'')) as Name from omni_OrderInfo a ";
            SQL += " left join omni_Device b on (a.DeviceID=b.DeviceID and b.IsActive=1)";
            SQL += " left join omni_users c on (a.UserID=c.UserId and c.IsActive=1)";


            if (val1 == "")
                SQL += "where TransactionDate <='" + val2 + "'";
            else
                SQL += "where TransactionDate>='" + val1 + "' and TransactionDate <='" + val2 + "'";

            if (myDictionary != null)
            {
                List<string> list = new List<string>(myDictionary.Keys);

                // Loop through list
                foreach (string k in list)
                {
                    addl += " And " + k + "= '" + myDictionary[k] + "' ";
                }
            }

            SQL += addl + " order by transactiondate desc";
            //HttpContext.Current.Response.Write(SQL);
            return SQL;
        }

        public string getOrderTransactionSQL(Dictionary<string, string> myDictionary, string val1, string val2, string val3)
        {
            addl = "";
            //SQL = "select * from omni_OrderInfo ";
            SQL = "select a.*, b.DeviceName, (ISNULL(c.firstname,'')+' ' + ISNULL(c.lastname,'')) as Name ,";
            SQL += " dbo.GetOrderPaymentModeStatus (a.Order_TranID,'" + val3 + "') as PaymentModeStatus from omni_OrderInfo a ";
            SQL += " left join omni_Device b on (a.DeviceID=b.DeviceID and b.IsActive=1)";
            SQL += " left join omni_users c on (a.UserID=c.UserId and c.IsActive=1)";

            if (val1 == "")
                //SQL += "where TransactionDate <='" + val2 + "'";
                SQL += " where CONVERT(VARCHAR(26), TransactionDate, 23) <='" + val2 + "'";
            else
                //SQL += "where TransactionDate>='" + val1 + "' and TransactionDate <='" + val2 + "'";
                SQL += " where CONVERT(VARCHAR(26), TransactionDate, 23)>='" + val1 + "' and CONVERT(VARCHAR(26), TransactionDate, 23) <='" + val2 + "'";


            if (myDictionary != null)
            {
                List<string> list = new List<string>(myDictionary.Keys);

                // Loop through list
                foreach (string k in list)
                {
                    addl += " And " + k + "= '" + myDictionary[k] + "' ";
                }
            }

            SQL += addl + " order by transactiondate desc";
            return SQL;
        }

        public string getOrderIDSSQL(string frdate, string tldate, string restid)
        {
            addl = "";
            //SQL = "select dbo.getCommaDelimitedDataWithinDateRange('" + frdate + "','" + tldate + "','" + restid + "')  as OrderIDS";
            SQL = "select dbo.getOrderIDSWithinDateRangeNew('" + frdate + "','" + tldate + "','" + restid + "')  as OrderIDS";
            return SQL;
        }

        public string getPayoutRefundTransactionSQL(Dictionary<string, string> myDictionary, string val1, string val2)
        {
            addl = "";

            SQL = "select A.*,B.PaymentType, C.DeviceName , (isnull(d.FirstName,'') + isnull(d.LastName,'')) Name from ( select TransactionID,TransactionDate as TransactDate,DeviceID,PaymentTypeID,Amount, Rest_ID, 'Payout' as [type] , CreatedByUserID as UserID from omni_Payout  ";
            SQL += " union all ";
            SQL += " select TransactionID,RefundDate as TransactDate,DeviceID,PaymentTypeID,Amount, Rest_ID,'Refund' as [type], CreatedByUserID as UserID from omni_Refund ) as A ";
            SQL += " left join omni_PaymentType B on (A.PaymentTypeID= B.PaymentTypeID)";
            SQL += " left join omni_Device C on (A.DeviceID =C.DeviceID)";
            SQL += " left join omni_users d on (A.UserID = d.UserId)";

            if (val1 == val2)
                //SQL += " where A.TransactDate <='" + val2 + "'";
                SQL += " where CONVERT(VARCHAR(26), A.TransactDate, 23) >='" + val1 + "' and CONVERT(VARCHAR(26), A.TransactDate, 23) <='" + val2 + "'";
            else
                //SQL += " where A.TransactDate>='" + val1 + "' and A.TransactDate<='" + val2 + "'";
                SQL += " where CONVERT(VARCHAR(26), A.TransactDate, 23)>='" + val1 + "' and CONVERT(VARCHAR(26), A.TransactDate, 23)<='" + val2 + "'";

            if (myDictionary != null)
            {
                List<string> list = new List<string>(myDictionary.Keys);

                // Loop through list
                foreach (string k in list)
                {
                    addl += " And " + k + "= '" + myDictionary[k] + "' ";
                }
            }

            SQL += addl + " order by a.TransactDate,[type] desc";

            return SQL;
        }

        public string getProductIngredientsCombinedSQL(Dictionary<string, string> myDictionary, string whfld, string whval)
        {
            addl = "";
            SQL = "select final.* from (";
            SQL += " select IngredientID as ID, IngredientName as Product, a.UnitID, b.UnitName, Price as UnitCost, 'Ing' as Type, a.IsActive,a.SupplierID,a.Rest_ID from omni_Items_Ingredients a ";
            SQL += " inner join omni_UnitMaster b on (a.UnitID = b.UnitID) ";
            SQL += " Union All ";
            SQL += " select ProductID as ID, ProductName as Product, a.UnitID, b.UnitName, Price1 as UnitCost, 'Prod' as Type, a.IsActive,a.SupplierID,a.Rest_ID from omni_Products a";
            SQL += " inner join omni_UnitMaster b on (a.UnitID = b.UnitID)) final ";

            if (whfld != "" && whval != "")
                SQL += " where final." + whfld + " ='" + whval + "'";

            if (myDictionary != null)
            {
                List<string> list = new List<string>(myDictionary.Keys);

                // Loop through list

                int i = 0;

                foreach (string k in list)
                {
                    if ((whfld == "" || whval == "") && i == 0)
                        addl = " where final." + k + "= '" + myDictionary[k] + "' ";
                    else
                        addl += " And final." + k + "= '" + myDictionary[k] + "' ";
                    i++;
                }
            }

            SQL += addl + " order by Type,Product";
            return SQL;
        }

        public string GetSubRecipeIngredientCombinedSQL(Dictionary<string, string> myDictionary, string whfld, string whval)
        {
            addl = "";
            SQL = "select final.* from (";
            SQL += " select IngredientID as ID, IngredientName as Product, a.UnitID, b.UnitName, 'Ingredient' as Type, a.IsActive,a.SupplierID,a.Rest_ID from omni_Items_Ingredients a ";
            SQL += " inner join omni_UnitMaster b on (a.UnitID = b.UnitID) ";
            SQL += " Union All ";
            SQL += " select SubRecipeID as ID, SubRecipeName as Product, c.UnitID, d.UnitName, 'Sub Recipe' as Type, c.IsActive,'' as SupplierID, c.Rest_ID from omni_SubRecipe c";
            SQL += " inner join omni_UnitMaster d on (c.UnitID = d.UnitID) ";
            SQL += " ) final ";

            if (whfld != "" && whval != "")
                SQL += " where final." + whfld + " ='" + whval + "'";

            if (myDictionary != null)
            {
                List<string> list = new List<string>(myDictionary.Keys);

                // Loop through list

                int i = 0;

                foreach (string k in list)
                {
                    if ((whfld == "" || whval == "") && i == 0)
                        addl = " where final." + k + "= '" + myDictionary[k] + "' ";
                    else
                        addl += " And final." + k + "= '" + myDictionary[k] + "' ";
                    i++;
                }
            }

            SQL += addl + " order by Type,Product";
            return SQL;
        }

        public string getPurchaseOrderSQL(Dictionary<string, string> myDictionary, string whfld, string whval)
        {
            addl = "";

            SQL = " select POID,PONo, PODate ,a.SupplierID, (b.FirstName + ' ' + b.LastName) as SupplierName, TotalAmt, a.IsActive as Status, a.IsInvoiceGenerated as Invoiced from omni_PurchaseMaster a ";
            SQL += " inner join  omni_Suppliers b on (a.supplierid= b.supplierid) ";

            if (whfld != "" && whval != "")
                SQL += " where " + whfld + " ='" + whval + "'";

            if (myDictionary != null)
            {
                List<string> list = new List<string>(myDictionary.Keys);

                // Loop through list

                int i = 0;

                foreach (string k in list)
                {
                    if ((whfld == "" || whval == "") && i == 0)
                        addl = " where " + k + "= '" + myDictionary[k] + "' ";
                    else
                        addl += " And " + k + "= '" + myDictionary[k] + "' ";
                    i++;
                }
            }

            SQL += addl + " order by a.PODate desc, a.POID desc";
            return SQL;
        }

        public string getPurchaseOrderDetailSQL(Dictionary<string, string> myDictionary, string whfld, string whval)
        {
            addl = "";

            SQL = " select POID, ProductID,(select [dbo].GetProductIngredientName(ProductID,ProductType,Rest_ID)) as ProductName , (select [dbo].GetProductUnit(ProductID,ProductType,Rest_ID)) as UnitName, ProductType, Qty, UnitPrice from omni_PurchaseDetail ";

            if (whfld != "" && whval != "")
                SQL += " where " + whfld + " ='" + whval + "'";

            if (myDictionary != null)
            {
                List<string> list = new List<string>(myDictionary.Keys);

                // Loop through list

                int i = 0;

                foreach (string k in list)
                {
                    if ((whfld == "" || whval == "") && i == 0)
                        addl = " where " + k + "= '" + myDictionary[k] + "' ";
                    else
                        addl += " And " + k + "= '" + myDictionary[k] + "' ";
                    i++;
                }
            }

            SQL += addl;
            return SQL;
        }

        public string getGoodsReceivedNoteSQL(Dictionary<string, string> myDictionary, string whfld, string whval)
        {
            addl = "";

            SQL += "select GRNNo, Tran_Code, Tran_Date,PONo, a.SupplierID, (b.FirstName + ' ' + b.LastName) as SupplierName, Grn_Amount as TotalAmt, a.IsActive as Status, a.IsFullPaid as IsFullPaid from omni_Item_ReceivedNotes a ";
            SQL += " inner join  omni_Suppliers b on (a.supplierid= b.supplierid) ";

            if (whfld != "" && whval != "")
                SQL += " where " + whfld + " ='" + whval + "'";

            if (myDictionary != null)
            {
                List<string> list = new List<string>(myDictionary.Keys);

                // Loop through list

                int i = 0;

                foreach (string k in list)
                {
                    if ((whfld == "" || whval == "") && i == 0)
                        addl = " where " + k + "= '" + myDictionary[k] + "' ";
                    else
                        addl += " And " + k + "= '" + myDictionary[k] + "' ";
                    i++;
                }
            }

            SQL += addl + " order by a.Tran_Date desc";
            return SQL;
        }

        public string getPurchaseItemDataSQL(Dictionary<string, string> myDictionary, string val1, string val2)
        {
            addl = "";

            SQL = "select a.GRNNo, Tran_Code, Tran_Date, POID, PONo, a.SupplierID, b.ProductID, (select dbo.GetProductIngredientName(b.ProductID,b.ProductType,a.Rest_ID)) as ProductName , ";
            SQL += " b.Qty, b.ProductType, b.Unit_Price, Convert(decimal(10,2),(qty*unit_price)) as TotalAmt,  a.IsActive as Status, a.IsFullPaid as IsFullPaid,  (c.FirstName + ' ' + c.LastName) as SupplierName ";
            SQL += " from omni_Item_ReceivedNotes a ";
            SQL += " inner join  omni_Item_ReceivedNotesDetail b on (a.GRNNO= b.GRNNO) ";
            SQL += " inner join  omni_Suppliers c on (a.supplierid= c.supplierid) ";

            if (val1 == "")
                SQL += " where CONVERT(VARCHAR(26), a.Tran_Date, 23) <='" + val2 + "'";
            else
                SQL += " where CONVERT(VARCHAR(26), a.Tran_Date, 23)>='" + val1 + "' and CONVERT(VARCHAR(26), a.Tran_Date, 23)<='" + val2 + "'";

            if (myDictionary != null)
            {
                List<string> list = new List<string>(myDictionary.Keys);

                // Loop through list

                int i = 0;

                foreach (string k in list)
                {
                    if ((val1 == "" && val2 == "") && i == 0)
                        addl = " where " + k + "= '" + myDictionary[k] + "' ";
                    else
                        addl += " And " + k + "= '" + myDictionary[k] + "' ";
                    i++;
                }
            }

            //SQL += addl + " group by a.GRNNo,Tran_Code,Tran_Date,POID,PONo,a.SupplierID,b.ProductID,a.Rest_ID,b.ProductType,b.Qty,b.Unit_Price,Grn_Amount,Paid_Amount,Due_Amount,a.IsActive,IsFullPaid,FirstName,LastName";
            SQL += addl + " order by a.Tran_Date desc";
            return SQL;
        }

        public string getProdWithIngToSBePurchaseFromSupplierSQL(Dictionary<string, string> myDictionary, string whfld, string whval, string splopertor, string spfld1, string spfld2)
        {
            addl = "";
            add2 = " and " + whfld + "='" + whval + "'";

            SQL = "select Items.ItemID, Items.SupplierID, Items.ItemName, Items.ItemType, Items.UnitID, Items.UnitName, Items.UnitPrice, Items.ReOrderLevel, Items.ReOrderQty, IsNull((Items.ReOrderLevel + Items.ReOrderQty),0.00) as HoldQty, IsNull(Items.OpQty,0.00) as OpQty, IsNull(b.Qty,0.00) as QtyIn, IsNull(c.Qty,0.00) as QtyOut, (((IsNull(OpQty,0.00)+IsNull(b.Qty,0.00))- IsNull(c.Qty,0.00))) as BalQty, ";
            SQL += " Convert(Decimal(18,2),(((IsNull(OpQty,0.00)+IsNull(b.Qty,0.00))- IsNull(c.Qty,0.00)) * UnitPrice)) as BalAmt, Convert(Decimal(18,2),(Items.UnitPrice * (IsNull((Items.ReOrderLevel + Items.ReOrderQty),0.00) - (((IsNull(OpQty,0.00)+IsNull(b.Qty,0.00))- IsNull(c.Qty,0.00)))))) as ReqdQtyAmt from ";
            SQL += "(select ProductID as ItemID, SupplierID, ProductName as ItemName, UnitID, (select dbo.GetProductUnit(ProductID, 'Prod', Rest_ID)) as UnitName ,Price1 as UnitPrice, ReOrderLevel,ReOrderQty, OpQty as OpQty, 'Prod' as ItemType from omni_Products where IsActive=1 " + add2;
            SQL += " union all select IngredientID as ItemID, SupplierID, IngredientName as ItemName, UnitID, (select dbo.GetProductUnit(IngredientID, 'Ing', Rest_ID)) as UnitName, Price as UnitPrice, ReOrderLevel, ReOrderQty, OpQty as OpQty, 'Ing' as ItemType from omni_Items_Ingredients where IsActive=1 " + add2 + ")";
            SQL += " as Items";
            SQL += " left join omni_Tran_ItemHistory b on (Items.ItemID = b.ProductID and Items.ItemType=b.ProductType and b.Tran_Type='RC' and b.Rest_ID='" + whval + "')";
            SQL += " left join omni_Tran_ItemHistory c on (Items.ItemID = c.ProductID and Items.ItemType=c.ProductType and c.Tran_Type='DL' and c.Rest_ID='" + whval + "')";

            if (myDictionary != null)
            {
                List<string> list = new List<string>(myDictionary.Keys);

                // Loop through list

                int i = 0;

                foreach (string k in list)
                {
                    if (i == 0)
                        addl = " where " + k + "= '" + myDictionary[k] + "' ";
                    else
                        addl += " And " + k + "= '" + myDictionary[k] + "' ";
                    i++;
                }
            }

            if (addl == "")
                //addl += " Where " + spfld1 + splopertor + "=" + spfld2;
                addl += " Where " + spfld1 + splopertor + spfld2;
            else
                //addl += " And " + spfld1 + splopertor + "=" + spfld2;
                addl += " And " + spfld1 + splopertor + spfld2;

            SQL += addl + " order by Items.ItemName";
            return SQL;
        }


        public string getAllItemStockSQL(Dictionary<string, string> myDictionary, string whfld, string whval, string splopertor)
        {
            addl = "";
            add2 = " and " + whfld + "='" + whval + "'";

            SQL = "select Items.ItemID, Items.SupplierID, Items.ItemName, Items.ItemType,Items.UnitPrice, Items.ReOrderLevel, Items.ReOrderQty, IsNull(Items.OpQty,0.00) as OpQty, IsNull(b.Qty,0.00) as QtyIn, IsNull(c.Qty,0.00) as QtyOut, (((IsNull(OpQty,0.00)+IsNull(b.Qty,0.00))- IsNull(c.Qty,0.00))) as BalQty, ";
            SQL += " Convert(Decimal(18,2),(((IsNull(OpQty,0.00)+IsNull(b.Qty,0.00))- IsNull(c.Qty,0.00)) * UnitPrice)) as BalAmt from ";
            SQL += "(select ProductID as ItemID, SupplierID, ProductName as ItemName, Price1 as UnitPrice, ReOrderLevel,ReOrderQty, OpQty as OpQty, 'Prod' as ItemType from omni_Products where IsActive=1 " + add2;
            SQL += " union all select IngredientID as ItemID, SupplierID, IngredientName as ItemName, Price as UnitPrice, ReOrderLevel, ReOrderQty, OpQty as OpQty, 'Ing' as ItemType from omni_Items_Ingredients where IsActive=1 " + add2 + ")";
            SQL += " as Items";
            SQL += " left join omni_Tran_ItemHistory b on (Items.ItemID = b.ProductID and Items.ItemType=b.ProductType and b.Tran_Type='RC' and b.Rest_ID='" + whval + "')";
            SQL += " left join omni_Tran_ItemHistory c on (Items.ItemID = c.ProductID and Items.ItemType=c.ProductType and c.Tran_Type='DL' and c.Rest_ID='" + whval + "')";

            if (myDictionary != null)
            {
                List<string> list = new List<string>(myDictionary.Keys);

                // Loop through list

                int i = 0;

                foreach (string k in list)
                {
                    if (i == 0)
                    {
                        if (splopertor == "")
                            addl = " where " + k + "= '" + myDictionary[k] + "' ";
                        else
                            addl = " where " + k + splopertor + "= " + myDictionary[k];
                    }
                    else
                    {
                        if (splopertor == "")
                            addl += " And " + k + "= '" + myDictionary[k] + "' ";
                        else
                            addl += " And " + k + splopertor + "= " + myDictionary[k];
                    }
                    i++;
                }
            }

            SQL += addl + " order by Items.ItemName";
            return SQL;
        }

        public string getLastInsertedID(Dictionary<string, string> myDictionary, string tblname, string fldname, int mode, string whfld, string whlval)
        {
            addl = "";
            SQL = "SELECT max(" + fldname + ") FROM " + tblname;

            if (whfld != "")
                SQL += " where " + whfld + " ='" + whlval + "'";

            if (myDictionary != null)
            {
                List<string> list = new List<string>(myDictionary.Keys);

                // Loop through list
                foreach (string k in list)
                {
                    addl += " And " + k + "= '" + myDictionary[k] + "' ";
                }
            }

            SQL += addl;
            return SQL;
        }

        public string DeleteRec(Dictionary<string, string> myDictionary, string tblname, int mode, string whfld, string whlval)
        {
            addl = "";
            SQL = "delete from  " + tblname;

            if (whfld != "")
                SQL += " where " + whfld + " ='" + whlval + "'";

            if (myDictionary != null)
            {
                List<string> list = new List<string>(myDictionary.Keys);

                // Loop through list
                foreach (string k in list)
                {
                    addl += " And " + k + "= '" + myDictionary[k] + "' ";
                }
            }

            SQL += addl;
            return SQL;
        }

        public string LoginCredentialSQL(Dictionary<string, string> myDictionary)
        {
            SQL = "select UserID,UserGroupID ,UserName,FirstName, LastName from omni_users u where username = @usrname ";

            if (myDictionary != null)
            {
                List<string> list = new List<string>(myDictionary.Keys);

                // Loop through list
                foreach (string k in list)
                {
                    addl += " And " + k + "= '" + myDictionary[k] + "' ";
                }
            }

            SQL += addl + " and isactive=1";
            return SQL;
        }

        public string AdminLoginCredentialSQL(Dictionary<string, string> myDictionary)
        {
            SQL = "select UserID from Login u where UserId = @usrname and password = @usrpwd";

            if (myDictionary != null)
            {
                List<string> list = new List<string>(myDictionary.Keys);

                // Loop through list
                foreach (string k in list)
                {
                    addl += " And " + k + "= '" + myDictionary[k] + "' ";
                }
            }

            SQL += addl + " and Active=1";
            return SQL;
        }



        public string GetTableColumnValueSQL(Dictionary<string, string> myDictionary, string TableName, string getfldname, string whfld, string whval)
        {
            addl = "";
            add2 = "";
            SQL = "select CAST(" + getfldname + " as varchar(15)) as " + getfldname + " from " + TableName;

            if (whfld != "" && whval != "")
                addl = " Where " + whfld + "='" + whval + "'";

            if (myDictionary != null)
            {
                List<string> list = new List<string>(myDictionary.Keys);

                // Loop through list
                foreach (string k in list)
                {
                    add2 += " And " + k + " ='" + myDictionary[k] + "' ";
                }
            }
            SQL += addl + add2;
            return SQL;
        }

        public string CheckRecordExistsSQL(Dictionary<string, string> myDictionary, string tablename, string whfld, string whparam, string mode, string PKey, string Pval)
        {
            addl = "";
            add2 = "";
            SQL = "select " + whfld + " from " + tablename + " where " + whfld + " = @" + whparam + "";

            if (myDictionary != null)
            {
                List<string> list = new List<string>(myDictionary.Keys);

                // Loop through list
                foreach (string k in list)
                {
                    addl += " And " + k + " ='" + myDictionary[k] + "' ";
                }
            }
            if (mode == "edit")
                add2 = " And " + PKey + "<> '" + Pval + "'";

            SQL += addl + add2;
            return SQL;
        }

        public string CheckRecordCountForCloningSQL(string tablename, string fldname, string source, string target, string andquerystr)
        {
            SQL = "select * from " + tablename + " where Rest_ID='" + target + "' ";
            SQL += "and " + fldname + " in ";

            if (andquerystr != "")
                SQL += "(select " + fldname + " from " + tablename + " where Rest_ID='" + source + "' " + andquerystr + ")";
            else
                SQL += "(select " + fldname + " from " + tablename + " where Rest_ID='" + source + "')";

            SQL += andquerystr;
            return SQL;
        }

        public string CheckRecordCountForCloningSQL(string tablename, string fldname, string source, string target, string andquerystr, string compfldname, string LastDate)
        {
            addl = "";
            SQL = "select * from " + tablename + " where Rest_ID='" + target + "' ";
            SQL += "and " + fldname + " in ";

            if (andquerystr != "")
                SQL += "(select " + fldname + " from " + tablename + " where Rest_ID='" + source + "' " + andquerystr + ")";
            else
                SQL += "(select " + fldname + " from " + tablename + " where Rest_ID='" + source + "')";

            if (LastDate != "")
                addl = " And " + compfldname + ">='" + LastDate + "'";

            SQL += andquerystr + addl;
            return SQL;
        }

        public string CheckRecordExistsSQL(Dictionary<string, string> myDictionary, string tablename, string whfld, string whparam)
        {
            add2 = "";
            SQL = "select * from " + tablename;

            if (whfld != "" && whparam != "")
                addl = " where " + whfld + " ='" + whparam + "'";

            if (myDictionary != null)
            {
                List<string> list = new List<string>(myDictionary.Keys);

                // Loop through list
                foreach (string k in list)
                {
                    add2 += " And " + k + " ='" + myDictionary[k] + "' ";
                }
            }

            SQL += addl + add2;
            return SQL;
        }

        public string CheckRecordCountSQL(Dictionary<string, string> myDictionary, string tablename, string whfld, string whparam)
        {
            SQL = "select * from " + tablename;

            if (whfld != "" && whparam != "")
            {
                addl = " where " + whfld + " = '" + whparam + "'";
            }

            if (myDictionary != null)
            {
                List<string> list = new List<string>(myDictionary.Keys);

                // Loop through list
                foreach (string k in list)
                {
                    add2 += " And " + k + " ='" + myDictionary[k] + "' ";
                }
            }

            SQL += addl + add2;
            return SQL;
        }


        public string GetTableColListSQL(Dictionary<string, string> myDictionary, string tablename, IList<string> tblColumn, int colcount)
        {
            addl = "";
            string addlcol = "";
            string spSQL = "";

            if (tblColumn != null)
            {
                int k = 1;
                foreach (string value in tblColumn)
                {
                    if (k == 1)
                    {
                        addlcol += "CAST(" + value + " as varchar(15))" + " as " + value + ",";
                        spSQL += "CAST('' as varchar(15)) as " + value + ",";
                    }
                    else
                    {
                        addlcol += value + " as " + value + ",";
                        spSQL += " '' as " + value + ",";
                    }
                    k++;
                }
                addlcol = addlcol.Substring(0, (addlcol.Length) - 1);
                spSQL = spSQL.Substring(0, (spSQL.Length) - 1);

                if (colcount > 0)
                    spSQL = "Select " + spSQL + " union all ";
            }


            SQL = "select " + addlcol + " from " + tablename;

            if (myDictionary != null)
            {
                List<string> list = new List<string>(myDictionary.Keys);

                int i = 0;

                foreach (string k in list)
                {
                    if (i == 0)
                        addl = " where " + k + "= '" + myDictionary[k] + "' ";
                    else
                        addl += " And " + k + "= '" + myDictionary[k] + "' ";
                    i++;
                }
            }

            SQL = spSQL + SQL + addl;
            return SQL;
        }
    }
}