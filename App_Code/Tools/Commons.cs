using System;
using System.Data;
using System.Configuration;
using System.Globalization;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using MyDB;
using MyQuery;
/// <summary>
/// Summary description for Commons
/// </summary>
/// 
namespace Commons
{
    public class Common
    {
        DB AppConnection;
        SQLQuery Qry = new SQLQuery();
        string SQL = "";

        public enum EmployeeType
        {
            Manager = 1,
            TeamLeader = 2,
            Senior = 3,
            Junior = 4
        }

        public Common()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public string GetServerTimeZone()
        {
            SqlDataReader oReader;
            string ServerTimeZoneID = string.Empty;
            AppConnection = new DB();
            SQL = Qry.GetServerTimeZoneSQL();

            oReader = SqlHelper.ExecuteReader(AppConnection.GetConnection(), CommandType.Text, SQL);

            if (oReader.Read())
                ServerTimeZoneID = (string)oReader["OptionValue"];

            oReader.Close();
            return ServerTimeZoneID;
        }

        public string GetOptionSettings(string param)
        {
            SqlDataReader oReader;
            string OptionVal = string.Empty;
            AppConnection = new DB();
            SQL = Qry.GetOptionSettingsValueSQL(param);

            oReader = SqlHelper.ExecuteReader(AppConnection.GetConnection(), CommandType.Text, SQL);

            if (oReader.Read())
                OptionVal = (string)oReader["OptionValue"];

            oReader.Close();
            return OptionVal;
        }

        public string getRestaurantNameForRestId(string restId)
        {
            SqlDataReader oReader;
            string restName = string.Empty;
            AppConnection = new DB();
            SQL = Qry.GetRestaurantNameSQL(restId);

            oReader = SqlHelper.ExecuteReader(AppConnection.GetConnection(), CommandType.Text, SQL);

            if (oReader.Read())
                restName = (string)oReader["RestName"];

            oReader.Close();
            return restName;
        }

        public Dictionary<string, string> getHeaderAndFooter(string restId)
        {
            SqlDataReader oReader;
            AppConnection = new DB();
            SQL = Qry.GetRestaurantHeaderAndFooter(restId);           
            Dictionary<string, string> dict = new Dictionary<string,string>();

            oReader = SqlHelper.ExecuteReader(AppConnection.GetConnection(), CommandType.Text, SQL);

            if (oReader.Read())
            {
                dict.Add("Header_Name", (string)oReader["Header_Name"]);
                dict.Add("Header_Address1", (string)oReader["Header_Address1"]);
                dict.Add("Header_City", (string)oReader["Header_City"]);

                dict.Add("Header_State", (string)oReader["Header_State"]);
                dict.Add("Header_Zip", (string)oReader["Header_Zip"]);
                dict.Add("Header_Email", (string)oReader["Header_Email"]);

                dict.Add("Header_ABN", (string)oReader["Header_ABN"]);
                dict.Add("Footer1", (string)oReader["Footer1"]);
                dict.Add("Footer2", (string)oReader["Footer2"]);
            }

            oReader.Close();
            return dict;
        }

        public Dictionary<string, string> getRestaurentIdandName()
        {
            SqlDataReader oReader;
            AppConnection = new DB();
            SQL = Qry.GetAllRestaurentIdandNameSQL();
            Dictionary<string, string> dict = new Dictionary<string, string>();
               oReader = SqlHelper.ExecuteReader(AppConnection.GetConnection(), CommandType.Text, SQL);
               if (oReader.HasRows)
               {
                   while (oReader.Read())
                   {
                       dict.Add((string)oReader["Rest_Id"].ToString(), (string)oReader["RestName"]);
                   }
               }

            oReader.Close();
            return dict;
        }

        public Dictionary<string, string> getAllUserIdAndName_ForRestID(string restId)
        {
            SqlDataReader oReader;
            AppConnection = new DB();
            SQL = Qry.GetAllUserIdandNameForRestIDSQL(restId);
            Dictionary<string, string> dict = new Dictionary<string, string>();
            oReader = SqlHelper.ExecuteReader(AppConnection.GetConnection(), CommandType.Text, SQL);
            if (oReader.HasRows)
            {
                while (oReader.Read())
                {
                    dict.Add((string)oReader["UserId"].ToString(), (string)oReader["UserName"]);
                }
            }

            oReader.Close();
            return dict;
        }

        public bool shouldEmailZReportForRestId(string restId)
        {
            SqlDataReader oReader;
            bool shouldEmail = false;
            AppConnection = new DB();
            SQL = Qry.ShouldEmailZReportSQL(restId);

            oReader = SqlHelper.ExecuteReader(AppConnection.GetConnection(), CommandType.Text, SQL);

            if (oReader.Read())
                shouldEmail = (bool)oReader["shouldEmailZReport"];

            oReader.Close();
            return shouldEmail;
        }

        public string getCompanyEmailID()
        {
            SqlDataReader oReader;
            AppConnection = new DB();
            SQL = Qry.GetCompanyEmailSQL();

            string email = "";
            oReader = SqlHelper.ExecuteReader(AppConnection.GetConnection(), CommandType.Text, SQL);

            if (oReader.Read())
                email = (string)oReader["Email"];

            oReader.Close();
            return email;
        }

        public string GetCompanyInfo()
        {
            SqlDataReader oReader;
            string Companyname=string.Empty;
            AppConnection = new DB();
            SQL = Qry.GetCompanySQL();

            oReader = SqlHelper.ExecuteReader(AppConnection.GetConnection(), CommandType.Text, SQL);

            if (oReader.Read())
                Companyname = (string)oReader["CompanyName"];
            
            oReader.Close();
            return Companyname;
        }

        public string GetCompanyInfo(SqlConnection con)
        {
            SqlDataReader oReader;
            string Companyname = string.Empty;
            //AppConnection = new DB();
            SQL = Qry.GetCompanySQL();

            oReader = SqlHelper.ExecuteReader(con, CommandType.Text, SQL);

            if (oReader.Read())
                Companyname = (string)oReader["CompanyName"];

            oReader.Close();
            return Companyname;
        }

        public string GetOrderIDS(string frdate, string tldate, string restid)
        {
            SqlDataReader oReader;
            string strOrOrderIDS = string.Empty;
            AppConnection = new DB();

            oReader = SqlHelper.ExecuteReader(AppConnection.GetConnection(), CommandType.Text, Qry.getOrderIDSSQL(frdate,tldate,restid));

            if (oReader.Read())
                strOrOrderIDS = (string)oReader["OrderIDS"];

            oReader.Close();
            return strOrOrderIDS;
        }

        public DataSet LoadSupplier()
        {
            AppConnection = new DB();
            return SqlHelper.ExecuteDataset(AppConnection.GetConnection(), CommandType.Text, Qry.GetSupplierSQL());
        }

        public string getSupplierDetails(Dictionary<string, string> myDictionary)
        {
            string stroutput = string.Empty;
            AppConnection = new DB();
            SqlDataReader oReader;

            oReader = SqlHelper.ExecuteReader(AppConnection.GetConnection(), CommandType.Text, Qry.GetSupplierDetailSQL(myDictionary));

            while(oReader.Read())
            {
                stroutput += oReader["Address1"] + "~" + oReader["Address2"];
            }

            oReader.Close();
            return stroutput;
        }



        public DataSet LoadCustomer()
        {
            AppConnection = new DB();
            return SqlHelper.ExecuteDataset(AppConnection.GetConnection(), CommandType.Text, Qry.GetCustomerSQL());
        }

        public DataSet LoadCustomer(SqlConnection con)
        {
            //AppConnection = new DB();
            return SqlHelper.ExecuteDataset(con, CommandType.Text, Qry.GetCustomerSQL());
        }

        public DataSet LoadCustomersFromCompanyLevel(Dictionary<string, string> myDictionary, string whfld, string whval, SqlConnection con)
        {
            //AppConnection = new DB();
             return SqlHelper.ExecuteDataset(con, CommandType.Text, Qry.GetCustomerSQL());
        }

        public DataSet LoadUserGroupsFromCompanyLevel(Dictionary<string, string> myDictionary, string whfld, string whval, SqlConnection con)
        {
            //AppConnection = new DB();
            return SqlHelper.ExecuteDataset(con, CommandType.Text, Qry.GetUserGroupSQL());
        }

        public DataSet LoadStates()
        {
            AppConnection = new DB();
            return SqlHelper.ExecuteDataset(AppConnection.GetConnection(), CommandType.Text, Qry.GetStateSQL());
        }

        public DataSet LoadStates(SqlConnection con)
        {
            //AppConnection = new DB();
            return SqlHelper.ExecuteDataset(con, CommandType.Text, Qry.GetStateSQL());
        }

        public DataSet LoadAPPModules()
        {
            AppConnection = new DB();
            SQL = "select * from omni_Modules where IsActive=1";
            return SqlHelper.ExecuteDataset(AppConnection.GetConnection(), CommandType.Text,SQL );
        }

        public void LoadAPPModules(int val,DataSet ds,string tablename)
        {
            AppConnection = new DB();

            //if (val == 0)
            //    SQL = "select MenuID,MenuName, ParentMenuID,NavigateURL from omni_Modules where ParentMenuId=0 and IsActive=1";
            //else
            //    SQL = "select MenuID,MenuName, ParentMenuID,NavigateURL from omni_Modules where ParentMenuId>0 and IsActive=1 order by SubMenuOrder";

            //return SqlHelper.ExecuteDataset(AppConnection.GetConnection(), CommandType.Text, SQL);
            SqlHelper.FillDataset(AppConnection.GetConnection(), CommandType.Text, Qry.GetAppModuleSQL(val,ds,tablename), ds, new string[] { tablename });
        }

        public void LoadAPPModules(int val, DataSet ds, string tablename, SqlConnection conn)
        {
            //AppConnection = new DB();

            //if (val == 0)
            //    SQL = "select MenuID,MenuName, ParentMenuID,NavigateURL from omni_Modules where ParentMenuId=0 and IsActive=1";
            //else
            //    SQL = "select MenuID,MenuName, ParentMenuID,NavigateURL from omni_Modules where ParentMenuId>0 and IsActive=1 order by SubMenuOrder";

            //return SqlHelper.ExecuteDataset(AppConnection.GetConnection(), CommandType.Text, SQL);
            SqlHelper.FillDataset(conn, CommandType.Text, Qry.GetAppModuleSQL(val, ds, tablename), ds, new string[] { tablename });
        }

        public void LoadAppModules(int val,DataSet ds,string tablename,string param, int param1)
        {
            AppConnection = new DB();
            
            //if (val == 0) 
            //{
            //    SQL = "Select a.UserGroupID, b.MenuID, b.ParentMenuID, b.MenuName, NavigateURL from omni_Group_Permissions a inner join ";
            //    SQL += "omni_Modules b on (a.MenuID = b.MenuID) and b.ParentMenuID=0 and b.IsActive=1 and a.UserGroupID=" + param1;
            //}
            //else 
            //{
            //    SQL = "Select a.UserGroupID, b.MenuID, b.ParentMenuID ,b.MenuName, NavigateURL from omni_Group_Permissions a inner join ";
            //    SQL += "omni_Modules b on (a.MenuID = b.MenuID) and b.ParentMenuID>1 and b.IsActive=1 and a.UserGroupID=" + param1;
            //}
            SqlHelper.FillDataset(AppConnection.GetConnection(), CommandType.Text, Qry.GetAppModuleSQL(val,ds, tablename, param, param1)  , ds, new string[] { tablename });
      }

        public void LoadAppModules(int val, DataSet ds, string tablename, string param, int param1, SqlConnection conn)
        {
            //AppConnection = new DB();
            SqlHelper.FillDataset(conn, CommandType.Text, Qry.GetAppModuleSQL(val, ds, tablename, param, param1), ds, new string[] { tablename });
        }

        public bool CheckAssignedModuleForGroup(string param1, string param2, string mode)
        {
            AppConnection = new DB();
            bool flag = false;
            SqlDataReader oReader;

            oReader = SqlHelper.ExecuteReader(AppConnection.GetConnection(), CommandType.Text, Qry.CheckAssignedModuleForGroupSQL(param1, param2, mode));

            if (oReader.Read())
                flag = true;
            else
                flag = false;

            oReader.Close();
            return flag;
        }

        public bool CheckAssignedModuleForGroup(string param1, string param2, string mode, SqlConnection con)
        {
            //AppConnection = new DB();
            bool flag = false;
            SqlDataReader oReader;

            oReader = SqlHelper.ExecuteReader(con, CommandType.Text, Qry.CheckAssignedModuleForGroupSQL(param1, param2, mode));

            if (oReader.Read())
                flag = true;
            else
                flag = false;

            oReader.Close();
            return flag;
        }

        public DataSet LoadUserGroups()
        {
            AppConnection = new DB();
            return SqlHelper.ExecuteDataset(AppConnection.GetConnection(), CommandType.Text,Qry.GetUserGroupSQL());
        }

        public DataSet LoadUserGroups(SqlConnection con)
        {
            //AppConnection = new DB();
            return SqlHelper.ExecuteDataset(con, CommandType.Text, Qry.GetUserGroupSQL());
        }

        public DataSet LoadUsers()
        {
            AppConnection = new DB();
            return SqlHelper.ExecuteDataset(AppConnection.GetConnection(), CommandType.Text, Qry.GetUserSQL());
        }

        public DataSet LoadUsers(Dictionary<string, string> myDictionary, string whfld, string whval)             
        {
            AppConnection = new DB();
            return SqlHelper.ExecuteDataset(AppConnection.GetConnection(), CommandType.Text, Qry.GetUserSQL(myDictionary,whfld, whval));
        }

        public DataSet LoadAdminCompanies(Dictionary<string, string> myDictionary,SqlConnection conn)
        {
            //AppConnection = new DB();
            return SqlHelper.ExecuteDataset(conn, CommandType.Text, Qry.GetAdminCompanySQL(myDictionary));
        }

        public DataSet LoadUsers(Dictionary<string, string> myDictionary, string whfld, string whval, SqlConnection conn)
        {
            //AppConnection = new DB();
            return SqlHelper.ExecuteDataset(conn, CommandType.Text, Qry.GetUserSQL(myDictionary, whfld, whval));
        }

        public DataSet LoadUsers_WebService(Dictionary<string, string> myDictionary, string whfld, string whval)             
        {
            AppConnection = new DB();
            return SqlHelper.ExecuteDataset(AppConnection.GetConnection(), CommandType.Text, Qry.GetUserSQL_webservice(myDictionary, whfld, whval));            
        }

        public DataSet LoadUsers_WebService(Dictionary<string, string> myDictionary, string whfld, string whval, SqlConnection con)
        {
            //AppConnection = new DB();
            return SqlHelper.ExecuteDataset(con, CommandType.Text, Qry.GetUserSQL_webservice(myDictionary, whfld, whval));
        }

        public DataSet LoadAllUserInfoSQL_WebService(Dictionary<string, string> myDictionary, string whfld, string whval)             
        {
            AppConnection = new DB();
            return SqlHelper.ExecuteDataset(AppConnection.GetConnection(), CommandType.Text, Qry.getAllUserInfoSQL_WebService(myDictionary, whfld, whval));            
        }

        public DataSet LoadAllUserInfoSQL_WebService(Dictionary<string, string> myDictionary, string whfld, string whval,SqlConnection con)
        {
            //AppConnection = new DB();
            return SqlHelper.ExecuteDataset(con, CommandType.Text, Qry.getAllUserInfoSQL_WebService(myDictionary, whfld, whval));
        }

        public DataSet LoadRestaurant_WebService(Dictionary<string, Byte[]> myDictionary, string whfld, string whval, SqlParameter[] arParms)
        {
            AppConnection = new DB();

            SqlDataReader oReader = SqlHelper.ExecuteReader(AppConnection.GetConnection(), CommandType.Text, Qry.GetRestaurantSQL_WebService(myDictionary, whfld, whval), arParms);

            if (oReader.Read())
                return SqlHelper.ExecuteDataset(AppConnection.GetConnection(), CommandType.Text, Qry.GetRestaurantSQL(myDictionary, whfld, whval));
            else
            {
                DataSet ds1 = new DataSet();
                DataTable Table = ds1.Tables.Add();
                return ds1;
            }
        }

        public DataSet LoadRestaurant_WebService(Dictionary<string, Byte[]> myDictionary, string whfld, string whval, SqlParameter[] arParms, SqlConnection conn)
        {
            //AppConnection = new DB();

            //SqlDataReader oReader = SqlHelper.ExecuteReader(conn, CommandType.Text, Qry.GetRestaurantSQL_WebService(myDictionary, whfld, whval), arParms);

            //if (oReader.Read())
            return SqlHelper.ExecuteDataset(conn, CommandType.Text, Qry.GetRestaurantSQL(myDictionary, whfld, whval), arParms);
/*            else
            {
                DataSet ds1 = new DataSet();
                DataTable Table = ds1.Tables.Add();
                return ds1;
            }*/
        }

        public DataSet LoadRestaurants()
        {
            AppConnection = new DB();
            return SqlHelper.ExecuteDataset(AppConnection.GetConnection(), CommandType.Text, Qry.GetRestaurantSQL());
        }

        public DataSet LoadRestaurants(SqlConnection conn)
        {
            //AppConnection = new DB();
            return SqlHelper.ExecuteDataset(conn, CommandType.Text, Qry.GetRestaurantSQL());
        }

        public DataSet LoadCategories()
        {
            AppConnection = new DB();
            return SqlHelper.ExecuteDataset(AppConnection.GetConnection(), CommandType.Text, Qry.GetCategoriesSQL());
        }

        public DataSet LoadCategories(int mode)
        {
            AppConnection = new DB();
            return SqlHelper.ExecuteDataset(AppConnection.GetConnection(), CommandType.Text, Qry.GetCategoriesSQL(1));
        }

        public DataSet LoadCategories(Dictionary<string, string> myDictionary, string whfld, string whval)
        {
            AppConnection = new DB();
            return SqlHelper.ExecuteDataset(AppConnection.GetConnection(), CommandType.Text, Qry.GetCategoriesSQL(myDictionary, whfld,whval));
        }

        public DataSet LoadCategories(Dictionary<string, string> myDictionary, string whfld, string whval,SqlConnection con)
        {
            //AppConnection = new DB();
            return SqlHelper.ExecuteDataset(con, CommandType.Text, Qry.GetCategoriesSQL(myDictionary, whfld, whval));
        }

        public DataSet LoadInstruction(Dictionary<string, string> myDictionary, string whfld, string whval)
        {
            AppConnection = new DB();
            return SqlHelper.ExecuteDataset(AppConnection.GetConnection(), CommandType.Text, Qry.GetInstructionSQL(myDictionary, whfld, whval));
        }

        public DataSet LoadInstruction(Dictionary<string, string> myDictionary, string whfld, string whval,SqlConnection con)
        {
            //AppConnection = new DB();
            return SqlHelper.ExecuteDataset(con, CommandType.Text, Qry.GetInstructionSQL(myDictionary, whfld, whval));
        }

        public DataSet LoadTaxInfo(Dictionary<string, string> myDictionary, string whfld, string whval)
        {
            AppConnection = new DB();
            return SqlHelper.ExecuteDataset(AppConnection.GetConnection(), CommandType.Text, Qry.GetTaxInfoSQL(myDictionary, whfld, whval));
        }

        public DataSet LoadTaxInfo(Dictionary<string, string> myDictionary, string whfld, string whval,SqlConnection con)
        {
            //AppConnection = new DB();
            return SqlHelper.ExecuteDataset(con, CommandType.Text, Qry.GetTaxInfoSQL(myDictionary, whfld, whval));
        }

        public DataSet LoadDeviceInfo(Dictionary<string, string> myDictionary, string whfld, string whval)
        {
            AppConnection = new DB();
            return SqlHelper.ExecuteDataset(AppConnection.GetConnection(), CommandType.Text, Qry.GetDeviceInfoSQL(myDictionary, whfld, whval));
        }

        public DataSet LoadDeviceInfo(Dictionary<string, string> myDictionary, string whfld, string whval, SqlConnection con)
        {
            //AppConnection = new DB();
            return SqlHelper.ExecuteDataset(con, CommandType.Text, Qry.GetDeviceInfoSQL(myDictionary, whfld, whval));
        }

        public DataSet LoadNotes(Dictionary<string, string> myDictionary, string whfld, string whval)
        {
            AppConnection = new DB();
            return SqlHelper.ExecuteDataset(AppConnection.GetConnection(), CommandType.Text, Qry.GetNotesSQL(myDictionary, whfld, whval));
        }

        public DataSet LoadNotes(Dictionary<string, string> myDictionary, string whfld, string whval,SqlConnection con)
        {
            //AppConnection = new DB();
            return SqlHelper.ExecuteDataset(con, CommandType.Text, Qry.GetNotesSQL(myDictionary, whfld, whval));
        }

        public DataSet LoadCourses(Dictionary<string, string> myDictionary, string whfld, string whval)
        {
            AppConnection = new DB();
            return SqlHelper.ExecuteDataset(AppConnection.GetConnection(), CommandType.Text, Qry.GetCoursesSQL(myDictionary, whfld, whval));
        }

        public DataSet LoadCourses(Dictionary<string, string> myDictionary, string whfld, string whval, SqlConnection con)
        {
            //AppConnection = new DB();
            return SqlHelper.ExecuteDataset(con, CommandType.Text, Qry.GetCoursesSQL(myDictionary, whfld, whval));
        }

        public DataSet LoadParentCategory(int restid)
        {
            AppConnection = new DB();
            return SqlHelper.ExecuteDataset(AppConnection.GetConnection(), CommandType.Text, Qry.GetParentCategoriesSQL(restid));
        }

        public DataSet LoadPrinters()
        {
            AppConnection = new DB();
            return SqlHelper.ExecuteDataset(AppConnection.GetConnection(), CommandType.Text, Qry.getPrinterSQL());
        }

        public DataSet LoadPrinters(Dictionary<string, string> myDictionary,  string whfld, string whparam)
        {
            AppConnection = new DB();
            return SqlHelper.ExecuteDataset(AppConnection.GetConnection(), CommandType.Text, Qry.getPrinterSQL(myDictionary, whfld,whparam));
        }

        public DataSet LoadPrinters(Dictionary<string, string> myDictionary, string whfld, string whparam,SqlConnection con)
        {
            //AppConnection = new DB();
            return SqlHelper.ExecuteDataset(con, CommandType.Text, Qry.getPrinterSQL(myDictionary, whfld, whparam));
        }
        
        public DataSet LoadKitchens()
        {
            AppConnection = new DB();
            return SqlHelper.ExecuteDataset(AppConnection.GetConnection(), CommandType.Text, Qry.getKitchenSQL());
        }

        public DataSet LoadKitchens(Dictionary<string, string> myDictionary,  string whfld, string whparam)
        {
            AppConnection = new DB();
            return SqlHelper.ExecuteDataset(AppConnection.GetConnection(), CommandType.Text, Qry.getKitchenSQL(myDictionary, whfld,whparam));
        }

        public DataSet LoadKitchens(Dictionary<string, string> myDictionary, string whfld, string whparam, SqlConnection con)
        {
            //AppConnection = new DB();
            return SqlHelper.ExecuteDataset(con, CommandType.Text, Qry.getKitchenSQL(myDictionary, whfld, whparam));
        }

        public DataSet LoadCookingOptions()
        {
            AppConnection = new DB();
            return SqlHelper.ExecuteDataset(AppConnection.GetConnection(), CommandType.Text, Qry.getCookingOptionSQL());
        }

        public DataSet LoadCookingOptions(Dictionary<string, string> myDictionary, string whfld, string whparam)
        {
            AppConnection = new DB();
            return SqlHelper.ExecuteDataset(AppConnection.GetConnection(), CommandType.Text, Qry.getCookingOptionSQL(myDictionary, whfld, whparam));
        }

        public DataSet LoadCookingOptions(Dictionary<string, string> myDictionary, string whfld, string whparam, SqlConnection con)
        {
            //AppConnection = new DB();
            return SqlHelper.ExecuteDataset(con, CommandType.Text, Qry.getCookingOptionSQL(myDictionary, whfld, whparam));
        }
        
        public DataSet LoadModifierLevel()
        {
            AppConnection = new DB();
            return SqlHelper.ExecuteDataset(AppConnection.GetConnection(), CommandType.Text, Qry.getModifierLevelSQL());
        }

        public DataSet LoadModifierLevel(Dictionary<string, string> myDictionary,  string whfld, string whparam)
        {
            AppConnection = new DB();
            return SqlHelper.ExecuteDataset(AppConnection.GetConnection(), CommandType.Text, Qry.getModifierLevelSQL(myDictionary, whfld,whparam));
        }

        public DataSet LoadModifierLevel(Dictionary<string, string> myDictionary, string whfld, string whparam, SqlConnection con)
        {
            //AppConnection = new DB();
            return SqlHelper.ExecuteDataset(con, CommandType.Text, Qry.getModifierLevelSQL(myDictionary, whfld, whparam));
        }
        
        public DataSet LoadModifiers(int restid)
        {
            AppConnection = new DB();
            return SqlHelper.ExecuteDataset(AppConnection.GetConnection(), CommandType.Text, Qry.getModifierSQL(restid));
        }

        public DataSet LoadModifiers(Dictionary<string, string> myDictionary, string whfld, string whparam)
        {
            AppConnection = new DB();
            return SqlHelper.ExecuteDataset(AppConnection.GetConnection(), CommandType.Text, Qry.getModifierSQL(myDictionary, whfld,whparam));
        }

        public DataSet LoadModifiers(Dictionary<string, string> myDictionary, string whfld, string whparam,SqlConnection Con)
        {
            //AppConnection = new DB();
            //return SqlHelper.ExecuteDataset(AppConnection.GetConnection(), CommandType.Text, Qry.getModifierSQL(myDictionary, whfld,whparam));
            return SqlHelper.ExecuteDataset(Con, CommandType.Text, Qry.getModifierSQL(myDictionary, whfld, whparam));
            //return SqlHelper.ExecuteDataset(Con, CommandType.Text, Qry.getProductSQL(myDictionary, whfld, whparam));
        }

        public DataSet LoadUnits(Dictionary<string, string> myDictionary, string whfld, string whparam, SqlConnection con)
        {
            //AppConnection = new DB();
            return SqlHelper.ExecuteDataset(con, CommandType.Text, Qry.getUnitSQL(myDictionary, whfld, whparam));
        }

        public String getIngredients(Dictionary<string, string> myDictionary,string whfld,string whparam )
        {
            string strall = "";
            SqlDataReader oReader;
            AppConnection = new DB();
                                                           
            SQL= Qry.GetTableColumnValueSQL(myDictionary, "omni_Items_Ingredients", "IngredientName", whfld , whparam);

            oReader = SqlHelper.ExecuteReader(AppConnection.GetConnection(), CommandType.Text, SQL);

            while (oReader.Read())
            {
                strall += (string)oReader["IngredientName"] +",";
            }

            //strall = strall.Substring(0, -1);

            oReader.Close();
            return strall;
        }

        public DataSet LoadIngredientsWithSubRecipe(int restid)
        {
            AppConnection = new DB();
            return SqlHelper.ExecuteDataset(AppConnection.GetConnection(), CommandType.Text, Qry.getIngredientWithSubRecipeSQL(restid));
        }

        public DataSet LoadIngredientsWithSubRecipe(Dictionary<string, string> myDictionary, string whfld, string whval, string addlfld, string addlfldval)
        {
            AppConnection = new DB();
            return SqlHelper.ExecuteDataset(AppConnection.GetConnection(), CommandType.Text, Qry.getIngredientWithSubRecipeSQL(myDictionary,whfld,whval,addlfld, addlfldval));
        }

        public DataSet LoadIngredients(int restid)
        {
            AppConnection = new DB();
            return SqlHelper.ExecuteDataset(AppConnection.GetConnection(), CommandType.Text, Qry.getIngredientSQL(restid));
        }

        public DataSet LoadIngredientsWithinRecipe(Dictionary<string, string> myDictionary, string whfld, string whval)
        {
            AppConnection = new DB();
            return SqlHelper.ExecuteDataset(AppConnection.GetConnection(), CommandType.Text, Qry.getIngredientWithinRecipeSQL(myDictionary,whfld,whval));
        }
    
		
		public string LoadIngredientsAsString(Dictionary<string, string> myDictionary, string whfld, string whfldval)
		{
            string strcollection = "";
            SqlDataReader oReader;
            AppConnection = new DB();
			
            SQL = Qry.getIngredientSQL(myDictionary, whfld, whfldval);

            oReader = SqlHelper.ExecuteReader(AppConnection.GetConnection(), CommandType.Text, SQL);

            if (oReader.HasRows)
            {
                while (oReader.Read())
                {
                    strcollection += "{id:" + oReader["IngredientID"].ToString() + ",value:\"" + (string)oReader["IngredientName"] + "\",unit:\"" + oReader["Unit"].ToString() + "\"},";
                }
            }

            strcollection = strcollection.Substring(0, (strcollection.Length) - 1);
            oReader.Close();
            return strcollection;		
		}
		
        public DataSet LoadIngredients(Dictionary<string, string> myDictionary, string whfld, string whparam, SqlConnection con)
        {
               return SqlHelper.ExecuteDataset(con, CommandType.Text, Qry.getIngredientSQL(myDictionary, whfld, whparam));
        }
		
        public DataSet LoadSubRecipes(Dictionary<string, string> myDictionary, string whfld, string whparam, SqlConnection con)
        {
            return SqlHelper.ExecuteDataset(con, CommandType.Text, Qry.getSubRecipeSQL(myDictionary, whfld, whparam));
        }

        public DataSet LoadProducts()
        {
            AppConnection = new DB();
            return SqlHelper.ExecuteDataset(AppConnection.GetConnection(), CommandType.Text, Qry.getProductSQL());
        }

        public DataSet LoadProducts(Dictionary<string, string> myDictionary, string whfld, string whparam)
        {
            AppConnection = new DB();
            return SqlHelper.ExecuteDataset(AppConnection.GetConnection(), CommandType.Text, Qry.getProductSQL(myDictionary,whfld,whparam));
        }

        public DataSet LoadProducts(Dictionary<string, string> myDictionary, string whfld, string whparam,SqlConnection Con)
        {
        //    AppConnection = new DB();
            return SqlHelper.ExecuteDataset(Con, CommandType.Text, Qry.getProductSQL(myDictionary, whfld, whparam));
        }

        public DataSet LoadProductsWithIngredientAndSubRecipes(Dictionary<string, string> myDictionary, string whfld, string whparam, string restid)
        {
            return SqlHelper.ExecuteDataset(AppConnection.GetConnection(), CommandType.Text, Qry.GetProductsWithIngredientAndSubRecipesSQL(myDictionary, whfld, whparam,restid));
        }


        public DataSet LoadProductModifiers(Dictionary<string, string> myDictionary, string whfld, string whparam)
        {
            AppConnection = new DB();
            return SqlHelper.ExecuteDataset(AppConnection.GetConnection(), CommandType.Text, Qry.getProductModifiersSQL(myDictionary, whfld, whparam));
        }

        public DataSet LoadProductModifiers(Dictionary<string, string> myDictionary, string whfld, string whparam, SqlConnection con)
        {
            //AppConnection = new DB();
            return SqlHelper.ExecuteDataset(con, CommandType.Text, Qry.getProductModifiersSQL(myDictionary, whfld, whparam));
        }


        public DataSet LoadProductCookingOptions(Dictionary<string, string> myDictionary, string whfld, string whparam)
        {
            AppConnection = new DB();
            return SqlHelper.ExecuteDataset(AppConnection.GetConnection(), CommandType.Text, Qry.getProductCookingOptionSQL(myDictionary, whfld, whparam));
        }

        public DataSet LoadProductCookingOptions(Dictionary<string, string> myDictionary, string whfld, string whparam, SqlConnection con)
        {
           // AppConnection = new DB();
            return SqlHelper.ExecuteDataset(con, CommandType.Text, Qry.getProductCookingOptionSQL(myDictionary, whfld, whparam));
        }

        public DataSet LoadProductKitchenPrinters(Dictionary<string, string> myDictionary, string whfld, string whparam)
        {
            AppConnection = new DB();
            return SqlHelper.ExecuteDataset(AppConnection.GetConnection(), CommandType.Text, Qry.getProductKitchenPrinterSQL(myDictionary, whfld, whparam));
        }

        public DataSet LoadProductKitchenPrinters(Dictionary<string, string> myDictionary, string whfld, string whparam,SqlConnection con)
        {
            //AppConnection = new DB();
            return SqlHelper.ExecuteDataset(con, CommandType.Text, Qry.getProductKitchenPrinterSQL(myDictionary, whfld, whparam));
        }

        public DataSet LoadCookingOptions(int restid)
        {
            AppConnection = new DB();
            return SqlHelper.ExecuteDataset(AppConnection.GetConnection(), CommandType.Text, Qry.getCookingOptionSQL(restid));
        }
        //public void LoadGroupPermissions(int val,DataSet ds,string tablename)
        //{
        //    AppConnection = new DB();
        //    SqlHelper.FillDataset(AppConnection.GetConnection(), CommandType.Text, Qry.GetGroupPermissionSQL(), ds, new string[] { tablename });
        //}

        public DataSet LoadSaleInfoForXReport(Dictionary<string, string> myDictionary, string val1, string val2)
        {
            AppConnection = new DB();
            return SqlHelper.ExecuteDataset(AppConnection.GetConnection(), CommandType.Text, Qry.GetSaleInfoForXReportSQL(myDictionary,val1,val2));
        }

        public DataSet LoadEmployeeBreakUpSaleForZReport(Dictionary<string, string> myDictionary, string val1, string val2)
        {
            AppConnection = new DB();
            return SqlHelper.ExecuteDataset(AppConnection.GetConnection(), CommandType.Text, Qry.GetEmployeeBreakUpSaleForZReportSQL(myDictionary, val1, val2));
        }

        public DataSet LoadEmployeeBreakUpSaleForZReport(Dictionary<string, string> myDictionary, string val1, string val2,SqlConnection con)
        {
            //AppConnection = new DB();
            return SqlHelper.ExecuteDataset(con, CommandType.Text, Qry.GetEmployeeBreakUpSaleForZReportSQL(myDictionary, val1, val2));
        }

        public DataSet LoadEmployeeBreakUpSaleForZReport(Dictionary<string, string> myDictionary, string val1, string val2, SqlTransaction tran)
        {
            //AppConnection = new DB();
            return SqlHelper.ExecuteDataset(tran, CommandType.Text, Qry.GetEmployeeBreakUpSaleForZReportSQL(myDictionary, val1, val2));
        }

        public DataSet LoadEmployeeBreakUpSaleForZReport(Dictionary<string, string> myDictionary, string val)
        {
            AppConnection = new DB();
            return SqlHelper.ExecuteDataset(AppConnection.GetConnection(), CommandType.Text, Qry.GetEmployeeBreakUpSaleForZReportSQL(myDictionary, val));
        }

        public DataSet LoadCategoryBreakUpSaleForZReport(Dictionary<string, string> myDictionary, string val1, string val2,SqlConnection con)
        {
            //AppConnection = new DB();
            return SqlHelper.ExecuteDataset(con, CommandType.Text, Qry.GetCategoryBreakUpSaleForZReportSQL(myDictionary, val1, val2));
        }

        public DataSet LoadCategoryBreakUpSaleForZReport(Dictionary<string, string> myDictionary, string val1, string val2, SqlTransaction tran)
        {
            //AppConnection = new DB();
            return SqlHelper.ExecuteDataset(tran, CommandType.Text, Qry.GetCategoryBreakUpSaleForZReportSQL(myDictionary, val1, val2));
        }

        public DataSet LoadCategoryBreakUpSaleForZReport(Dictionary<string, string> myDictionary, string val1, string val2)
        {
            AppConnection = new DB();
            return SqlHelper.ExecuteDataset(AppConnection.GetConnection(), CommandType.Text, Qry.GetCategoryBreakUpSaleForZReportSQL(myDictionary, val1, val2));
        }

        public DataSet LoadCategoryBreakUpSaleForZReport(Dictionary<string, string> myDictionary, string val)
        {
            AppConnection = new DB();
            return SqlHelper.ExecuteDataset(AppConnection.GetConnection(), CommandType.Text, Qry.GetCategoryBreakUpSaleForZReportSQL(myDictionary, val));
        }

        public DataSet LoadProductBreakUpSaleForZReport(Dictionary<string, string> myDictionary, string val1, string val2, SqlConnection con)
        {
            //AppConnection = new DB();
            return SqlHelper.ExecuteDataset(con, CommandType.Text, Qry.GetProductBreakUpSaleForZReportSQL(myDictionary, val1, val2));
        }

        public DataSet LoadProductSoldBreakDownByCategoryId(Dictionary<string, string> myDictionary, string val1, string val2, SqlConnection con, string categoryId)
        {
            //AppConnection = new DB();
            return SqlHelper.ExecuteDataset(con, CommandType.Text, Qry.getProductSoldBreakDownByCategoryIdSQL(myDictionary, val1, val2, categoryId));
        }

        public DataSet LoadProductBreakUpSaleForZReport(Dictionary<string, string> myDictionary, string val1, string val2,SqlTransaction tran)
        {
            //AppConnection = new DB();
            return SqlHelper.ExecuteDataset(tran, CommandType.Text, Qry.GetProductBreakUpSaleForZReportSQL(myDictionary, val1, val2));
        }

        public DataSet LoadProductBreakUpSaleForZReport(Dictionary<string, string> myDictionary, string val1, string val2)
        {
            AppConnection = new DB();
            return SqlHelper.ExecuteDataset(AppConnection.GetConnection(), CommandType.Text, Qry.GetProductBreakUpSaleForZReportSQL(myDictionary, val1, val2));
        }

        public DataSet LoadOpenItemBreakUpSale(Dictionary<string, string> myDictionary, string val1, string val2)
        {
            AppConnection = new DB();
            return SqlHelper.ExecuteDataset(AppConnection.GetConnection(), CommandType.Text, Qry.GetOpenItemBreakUpSaleSQL(myDictionary, val1, val2));
        }

        public DataSet LoadItemSold(Dictionary<string, string> myDictionary, string val1, string val2)
        {
            AppConnection = new DB();
            return SqlHelper.ExecuteDataset(AppConnection.GetConnection(), CommandType.Text, Qry.GetItemSoldSQL(myDictionary, val1, val2));
        }

        public DataSet LoadUniqueItemSoldInRange(Dictionary<string, string> myDictionary, string val1, string val2)
        {
            AppConnection = new DB();
            return SqlHelper.ExecuteDataset(AppConnection.GetConnection(), CommandType.Text, Qry.GetUniqueItemSoldInRangeSQL(myDictionary, val1, val2));
        }

        public DataSet LoadUniqueItemSoldInRange(Dictionary<string, string> myDictionary, string val1, string val2, SqlConnection Con)
        {
            //AppConnection = new DB();
            return SqlHelper.ExecuteDataset(Con, CommandType.Text, Qry.GetUniqueItemSoldInRangeSQL(myDictionary, val1, val2));
        }                

        public DataSet LoadProductBreakUpSaleForZReport(Dictionary<string, string> myDictionary, string val)
        {
            AppConnection = new DB();
            return SqlHelper.ExecuteDataset(AppConnection.GetConnection(), CommandType.Text, Qry.GetProductBreakUpSaleForZReportSQL(myDictionary, val));
        }

        public DataSet LoadProductOptionBreakUpSaleForZReport(Dictionary<string, string> myDictionary, string val1, string val2)
        {
            AppConnection = new DB();
            return SqlHelper.ExecuteDataset(AppConnection.GetConnection(), CommandType.Text, Qry.GetProductOptionBreakUpSaleForZReportSQL(myDictionary, val1, val2));
        }

        public DataSet LoadOrderTransactionData(Dictionary<string, string> myDictionary, string val1, string val2,SqlConnection Con)
        {
        //    AppConnection = new DB();
            return SqlHelper.ExecuteDataset(Con, CommandType.Text, Qry.getOrderTransactionSQL(myDictionary, val1,val2));
        }

        public DataSet LoadOrderTransactionData(Dictionary<string, string> myDictionary, string val1, string val2, string val3, SqlConnection Con)
        {
            //    AppConnection = new DB();
            return SqlHelper.ExecuteDataset(Con, CommandType.Text, Qry.getOrderTransactionSQL(myDictionary, val1, val2, val3));
        }

        public DataSet LoadPayoutRefundTransactionData(Dictionary<string, string> myDictionary, string val1, string val2, SqlConnection Con)
        {
            //    AppConnection = new DB();
            return SqlHelper.ExecuteDataset(Con, CommandType.Text, Qry.getPayoutRefundTransactionSQL(myDictionary, val1, val2));
        }

        public DataSet LoadPurchaseOrders(Dictionary<string, string> myDictionary, string val1, string val2, SqlConnection Con)
        {
            return SqlHelper.ExecuteDataset(Con, CommandType.Text, Qry.getPurchaseOrderSQL(myDictionary, val1, val2));
        }

        public DataSet LoadPurchaseOrderDetails(Dictionary<string, string> myDictionary, string val1, string val2, SqlConnection Con)
        {
            return SqlHelper.ExecuteDataset(Con, CommandType.Text, Qry.getPurchaseOrderDetailSQL(myDictionary, val1, val2));
        }

        public DataSet LoadPurchaseItemData(Dictionary<string, string> myDictionary, string val1, string val2, SqlConnection Con)
        {
            return SqlHelper.ExecuteDataset(Con, CommandType.Text, Qry.getPurchaseItemDataSQL(myDictionary, val1, val2));
        }

        public DataSet LoadProdWithIngToSBePurchaseFromSupplier(Dictionary<string, string> myDictionary, string val1, string val2, string splopertor, string spfld1, string spfld2)
        {
            AppConnection = new DB();
            return SqlHelper.ExecuteDataset(AppConnection.GetConnection(), CommandType.Text, Qry.getProdWithIngToSBePurchaseFromSupplierSQL(myDictionary, val1, val2, splopertor, spfld1, spfld2));
        }

        public DataSet LoadProdWithIngToSBePurchaseFromSupplier(Dictionary<string, string> myDictionary, string val1, string val2, string splopertor, string spfld1 , string spfld2 , SqlConnection Con)
        {
            return SqlHelper.ExecuteDataset(Con, CommandType.Text, Qry.getProdWithIngToSBePurchaseFromSupplierSQL(myDictionary, val1, val2, splopertor,spfld1, spfld2));
        }

        public DataSet LoadGoodsReceivedNotes(Dictionary<string, string> myDictionary, string val1, string val2, SqlConnection Con)
        {
            return SqlHelper.ExecuteDataset(Con, CommandType.Text, Qry.getGoodsReceivedNoteSQL(myDictionary, val1, val2));
        }

        public DataSet LoadAllItemStockData(Dictionary<string, string> myDictionary, string whfld, string whval, string sploperator, SqlConnection Con)
        {
            return SqlHelper.ExecuteDataset(Con, CommandType.Text, Qry.getAllItemStockSQL(myDictionary, whfld, whval, sploperator));
        }

        public bool CheckRecordExists(Dictionary<string, string> myDictionary, string tablename, string whfld, string whparam)
        {
            bool flag = false;
            SqlDataReader oReader;

            AppConnection = new DB();
            SQL = Qry.CheckRecordExistsSQL(myDictionary, tablename, whfld, whparam);

            oReader = SqlHelper.ExecuteReader(AppConnection.GetConnection(), CommandType.Text, SQL);

            if (oReader.Read())
                flag = true;
            else
                flag = false;

            oReader.Close();
            return flag;
        }

        public bool CheckRecordExists(Dictionary<string, string> myDictionary, string tablename, string whfld, string whparam, SqlConnection conn)
        {
            bool flag = false;
            SqlDataReader oReader;

            //AppConnection = new DB();
            SQL = Qry.CheckRecordExistsSQL(myDictionary, tablename, whfld, whparam);

            oReader = SqlHelper.ExecuteReader(conn, CommandType.Text, SQL);            

            if (oReader.Read())
                flag = true;
            else
                flag = false;

            oReader.Close();
            return flag;
        }

        public bool CheckRecordExists(Dictionary<string, string> myDictionary, string tablename, string whfld, string whparam,SqlTransaction tran)
        {
            bool flag = false;
            SqlDataReader oReader;

            //AppConnection = new DB();
            SQL = Qry.CheckRecordExistsSQL(myDictionary, tablename, whfld, whparam);

            oReader = SqlHelper.ExecuteReader(tran, CommandType.Text, SQL);

            if (oReader.Read())
                flag = true;
            else
                flag = false;

            oReader.Close();
            return flag;
        }

        public bool CheckRecordExists(Dictionary<string, string> myDictionary, string tablename, string whfld, string whparam, string mode, string Pkey, string Pval, SqlParameter[] Params)
        {
            bool flag = false;
            SqlDataReader oReader;

            AppConnection = new DB();
            SQL = Qry.CheckRecordExistsSQL(myDictionary, tablename, whfld, whparam, mode, Pkey, Pval);

            oReader = SqlHelper.ExecuteReader(AppConnection.GetConnection(), CommandType.Text, SQL, Params);

            if (oReader.Read())
                flag = true;
            else
                flag = false;

            oReader.Close();
            return flag;
        }

        public bool CheckRecordExists(Dictionary<string, string> myDictionary, string tablename, string whfld, string whparam, string mode, string Pkey, string Pval, SqlParameter[] Params, SqlConnection conn)
        {
            bool flag = false;
            SqlDataReader oReader;

            //AppConnection = new DB();
            SQL = Qry.CheckRecordExistsSQL(myDictionary, tablename, whfld, whparam, mode, Pkey, Pval);

            oReader = SqlHelper.ExecuteReader(conn, CommandType.Text, SQL, Params);

            if (oReader.Read())
                flag = true;
            else
                flag = false;

            oReader.Close();
            return flag;
        }

        public bool CheckRecordCountForCloning(string tablename, string fldname, string sourcelocation, string targetlocation, string andquerystr)
        {
            bool flag = false;
            SqlDataReader oReader;

            AppConnection = new DB();

            oReader = SqlHelper.ExecuteReader(AppConnection.GetConnection(), CommandType.Text, Qry.CheckRecordCountForCloningSQL(tablename, fldname, sourcelocation, targetlocation, andquerystr));

            if (oReader.Read())
                flag = true;
            else
                flag = false;

            oReader.Close();
            return flag;
        }
        
        public bool CheckRecordCountForCloning(string tablename, string fldname, string sourcelocation, string targetlocation, string andquerystr, string compfldname, string  LastDate)
        {
            bool flag = false;
            SqlDataReader oReader;

            AppConnection = new DB();

            oReader = SqlHelper.ExecuteReader(AppConnection.GetConnection(), CommandType.Text, Qry.CheckRecordCountForCloningSQL(tablename, fldname, sourcelocation, targetlocation, andquerystr, compfldname, LastDate));

            if (oReader.Read())
                flag = true;
            else
                flag = false;

            oReader.Close();
            return flag;
        }


        public bool CheckRecordCount(Dictionary<string, string> myDictionary, string tablename, string whfld, string whparam)
        {
            bool flag = false;
            SqlDataReader oReader;

            AppConnection = new DB();

            oReader = SqlHelper.ExecuteReader(AppConnection.GetConnection(), CommandType.Text, Qry.CheckRecordCountSQL(myDictionary, tablename, whfld, whparam));

            if (oReader.Read())
                flag = true;
            else
                flag = false;

            oReader.Close();
            return flag;
        }

        public bool CheckRecordCount(Dictionary<string, string> myDictionary, string tablename, string whfld, string whparam, SqlConnection conn)
        {
            bool flag = false;
            SqlDataReader oReader;

            //AppConnection = new DB();

            oReader = SqlHelper.ExecuteReader(conn, CommandType.Text, Qry.CheckRecordCountSQL(myDictionary, tablename, whfld, whparam));

            if (oReader.Read())
                flag = true;
            else
                flag = false;

            oReader.Close();
            return flag;
        }

        public void PopulateDropDown_List(DropDownList ddrlist, string strSQL, string Keyfld, string Keyval, string myval)
        {
            SqlDataReader DDR = null;
            AppConnection = new DB();

            DDR = SqlHelper.ExecuteReader(AppConnection.GetConnection(), CommandType.Text, strSQL);

            //DDR = runSQLReturnDataReader(strSQL);
            ddrlist.DataSource = DDR;
            ddrlist.DataTextField = Keyfld;
            ddrlist.DataValueField = Keyval;
            ddrlist.DataBind();
            //int myval1 = 0;
            //myval1 = Convert.ToInt32(myval);


            if (myval != "")
            {
                for (int cnt = 0; cnt < ddrlist.Items.Count; cnt++)
                {
                    //if (Convert.ToInt32(ddrlist.Items[cnt].Value) == myval1)
                    string lval = "";
                    lval = ddrlist.Items[cnt].Value.ToString();
                    lval = lval.Trim();
                    myval = myval.Trim();

                    if (lval == myval)
                    {
                        ddrlist.SelectedIndex = cnt;
                        break;
                    }
                }
            }
            DDR.Close();
            // return myval;
        }

        public void PopulateDropDown_List(DropDownList ddrlist, string strSQL, string Keyfld, string Keyval, string myval,SqlConnection conn)
        {
            SqlDataReader DDR = null;

            DDR = SqlHelper.ExecuteReader(conn, CommandType.Text, strSQL);

            //DDR = runSQLReturnDataReader(strSQL);
            ddrlist.DataSource = DDR;
            ddrlist.DataTextField = Keyfld;
            ddrlist.DataValueField = Keyval;
            ddrlist.DataBind();
            //int myval1 = 0;
            //myval1 = Convert.ToInt32(myval);


            if (myval != "")
            {
                for (int cnt = 0; cnt < ddrlist.Items.Count; cnt++)
                {
                    //if (Convert.ToInt32(ddrlist.Items[cnt].Value) == myval1)
                    string lval = "";
                    lval = ddrlist.Items[cnt].Value.ToString();
                    lval = lval.Trim();
                    myval = myval.Trim();

                    if (lval == myval)
                    {
                        ddrlist.SelectedIndex = cnt;
                        break;
                    }
                }
            }
            DDR.Close();
            // return myval;
        }

        public void PopulateDropDown_ListWithCustomTable(DropDownList ddrlist, string strSQL, string Keyfld, string Keyval, string myval)
        {
            SqlDataReader DDR = null;
            AppConnection = new DB();

            DDR = SqlHelper.ExecuteReader(AppConnection.GetConnection(), CommandType.Text, strSQL);

            //DDR = runSQLReturnDataReader(strSQL);
            ddrlist.DataSource = DDR;
            ddrlist.DataTextField = Keyfld;
            ddrlist.DataValueField = Keyval;
            ddrlist.DataBind();
            //int myval1 = 0;
            //myval1 = Convert.ToInt32(myval);


            if (myval != "")
            {
                for (int cnt = 0; cnt < ddrlist.Items.Count; cnt++)
                {
                    //if (Convert.ToInt32(ddrlist.Items[cnt].Value) == myval1)
                    string lval = "";
                    lval = ddrlist.Items[cnt].Value.ToString();
                    lval = lval.Trim();
                    myval = myval.Trim();

                    if (lval == myval)
                    {
                        ddrlist.SelectedIndex = cnt;
                        break;
                    }
                }
            }
            DDR.Close();
            // return myval;
        }

        public void PopulateDropDown_List_Custom(DropDownList ddrlist, string[] param, int maxrotate, string recval)
        {
            if (maxrotate >0)  // 5
            {
                ddrlist.Items.Add("");

                foreach (string val in param)           //PAD
                {
                    if (val != "")
                    {
                        for (int cnt = 1; cnt <= maxrotate; cnt++)       // 1 < 5
                        {
                            string lstitem = val + cnt.ToString();      // PAD1
                            ddrlist.Items.Add(lstitem);

                            if (lstitem.Trim() == recval.Trim())
                                ddrlist.SelectedValue = recval;
                        }
                    }
                }
            }
        }

        public void PopulateListBox(ListBox ddrlist, string strSQL, string Keyfld, string Keyval, string myval)
        {
            SqlDataReader DDR = null;
            AppConnection = new DB();

            DDR = SqlHelper.ExecuteReader(AppConnection.GetConnection(), CommandType.Text, strSQL);

            //DDR = runSQLReturnDataReader(strSQL);
            ddrlist.DataSource = DDR;
            ddrlist.DataTextField = Keyfld;
            ddrlist.DataValueField = Keyval;
            ddrlist.DataBind();
            //int myval1 = 0;
            //myval1 = Convert.ToInt32(myval);


            if (myval != "")
            {
                for (int cnt = 0; cnt < ddrlist.Items.Count; cnt++)
                {
                    //if (Convert.ToInt32(ddrlist.Items[cnt].Value) == myval1)
                    string lval = "";
                    lval = ddrlist.Items[cnt].Value.ToString();
                    lval = lval.Trim();
                    myval = myval.Trim();

                    if (lval == myval)
                    {
                        ddrlist.SelectedIndex = cnt;
                        break;
                    }
                }
            }
            DDR.Close();
            // return myval;
        }

        public void PopulateListBoxWithEnumValues(DropDownList ddrlist,Type EnumType, string val )
        {
            int pos=0, cnt = 0;
            Array Values = System.Enum.GetValues(EnumType);
            ddrlist.Items.Insert(0, new ListItem("", ""));
            //ddrlist.SelectedIndex = 0;

            foreach (int Value in Values)
            {
                string Display = Enum.GetName(EnumType, Value);
                ListItem Item = new ListItem(Display, Value.ToString());
                ddrlist.Items.Add(Item);

                if (val == Value.ToString())
                    pos = Value;
                else
                    cnt++;
            }
            ddrlist.SelectedIndex = pos;
        }

        public void SelectListBoxWithEnumValues(DropDownList ddrlist, Type EnumType, string val)
        {
            int pos = 0, cnt = 0;
            Array Values = System.Enum.GetValues(EnumType);
            
            foreach (int Value in Values)
            {
                string Display = Enum.GetName(EnumType, Value);
                ListItem Item = new ListItem(Display, Value.ToString());
                ddrlist.Items.Add(Item);

                if (val == Value.ToString())
                    pos = Value;
                else
                    cnt++;
            }
            ddrlist.SelectedIndex = pos;
        }

        public string GetTableColumnValue(Dictionary<string, string> myDictionary, string TableName, string getfldname, string whfld, string whval)
        {
            AppConnection = new DB();
            return (string) SqlHelper.ExecuteScalar(AppConnection.GetConnection(), CommandType.Text, Qry.GetTableColumnValueSQL(myDictionary, TableName, getfldname,whfld,whval));
        }

        public string GetTableColumnValue(Dictionary<string, string> myDictionary, string TableName, string getfldname, string whfld, string whval, SqlConnection conn)
        {
            return (string)SqlHelper.ExecuteScalar(conn, CommandType.Text, Qry.GetTableColumnValueSQL(myDictionary, TableName, getfldname, whfld, whval));
        }

        public string GetTableColumnValue(Dictionary<string, string> myDictionary, string TableName, string getfldname, string whfld, string whval, SqlTransaction tran)
        {
            return (string)SqlHelper.ExecuteScalar(tran, CommandType.Text, Qry.GetTableColumnValueSQL(myDictionary, TableName, getfldname, whfld, whval));
        }

        public DataSet GetTableColumnValue(Dictionary<string, string> myDictionary, string TableName, string getfldname, string whfld, string whval, int mode,SqlConnection conn)
        {
            return SqlHelper.ExecuteDataset(conn, CommandType.Text, Qry.GetTableColumnValueSQL(myDictionary, TableName, getfldname, whfld, whval));
        }


        public string GetTableColumnValue(SqlConnection con, int mode, SqlParameter[] param)
        {
            return (string)SqlHelper.ExecuteScalar(con, CommandType.StoredProcedure , "getTableColumn", param);
        }

        public string GetSubRecipeIngredientCombinedInJsonStringFromDB(Dictionary<string, string> myDictionary, string whfld, string whval)
        {
            //Format                    
            // {id:1,value:"Thomas",cp:134},{id:65,value:"Richard",cp:1743},{id:235,value:"Sarat Ghosh",cp:7342},
            // {id:982,value:"Nina",cp:21843},{id:724,value:"Pinta",cp:35}, {id:78,value:"Santa Maria",cp:787}

            string strcollection = "";
            SqlDataReader oReader;
            AppConnection = new DB();
            SQL = Qry.GetSubRecipeIngredientCombinedSQL(myDictionary, whfld, whval);

            oReader = SqlHelper.ExecuteReader(AppConnection.GetConnection(), CommandType.Text, SQL);

            if (oReader.HasRows)
            {
                while (oReader.Read())
                {
                    strcollection += "{id:" + oReader["ID"].ToString() + ",value:\"" + (string)oReader["Product"] + "\",type:\"" + oReader["Type"].ToString() + "\",unit:\"" + oReader["UnitName"].ToString() + "\"},";
                }
            }

            strcollection = strcollection.Substring(0, (strcollection.Length) - 1);
            oReader.Close();
            return strcollection;
        }

        public string GetProductIngredientCombinedInJsonStringFromDB(Dictionary<string, string> myDictionary, string whfld, string whval)
        {
            //Format                    
            // {id:1,value:"Thomas",cp:134},{id:65,value:"Richard",cp:1743},{id:235,value:"Sarat Ghosh",cp:7342},
            // {id:982,value:"Nina",cp:21843},{id:724,value:"Pinta",cp:35}, {id:78,value:"Santa Maria",cp:787}

            string strcollection = "";
            SqlDataReader oReader;
            AppConnection = new DB();
            SQL = Qry.getProductIngredientsCombinedSQL(myDictionary, whfld, whval);

            oReader = SqlHelper.ExecuteReader(AppConnection.GetConnection(), CommandType.Text, SQL);

            if (oReader.HasRows)
            {
                while (oReader.Read())
                {
                    strcollection += "{id:" + oReader["ID"].ToString() + ",value:\"" + (string)oReader["Product"] + "\",type:\"" + oReader["Type"].ToString() + "\",unit:\"" + oReader["UnitName"].ToString() + "\",unitcost:\"" + Convert.ToDecimal(oReader["UnitCost"]) + "\"},";
                }
                strcollection = strcollection.Substring(0, (strcollection.Length) - 1);
            }

            oReader.Close();
            return strcollection;
        }
        
        public string GetIngredientColumnValue_Conv_IntoString(Dictionary<string, string> myDictionary, string whfld, string whval, SqlConnection con)
        {
            string strcollection = "";
            SqlDataReader oReader;
            AppConnection = new DB();
            SQL = Qry.getIngredientSQL(myDictionary, whfld, whval);

            oReader = SqlHelper.ExecuteReader(con, CommandType.Text, SQL);

            if (oReader.HasRows)
            {
                while (oReader.Read())
                {
                    strcollection += oReader["IngredientID"].ToString() + "~" + (string)oReader["IngredientName"] + "~" + oReader["UnitID"].ToString() + "~" + (string)oReader["Unit"] + ",";
                }
            }

            oReader.Close();
            return strcollection;
        }

        public DateTime GetCommonDate(DateTime sFromDate, object SessionDateFormat)
        {
            DateTime dresult = new DateTime();
            DateTimeFormatInfo usDtfi = new  CultureInfo("en-US").DateTimeFormat; // --MM/dd/yyyy
            DateTimeFormatInfo ukDtfi = new  CultureInfo("en-GB").DateTimeFormat; // --dd/MM/yyyy

            if (SessionDateFormat == "dd/MM/yyyy")
                dresult = Convert.ToDateTime(sFromDate,ukDtfi);
            else if(SessionDateFormat == "MM/dd/yyyy")
                dresult = Convert.ToDateTime(sFromDate,usDtfi);

            return dresult;
        }

        public bool ValidateDate(string date)
        {
            try
            {
                // for US, alter to suit if splitting on hyphen, comma, etc.
                string[] dateParts = date.Split('/');

                // create new date from the parts; if this does not fail
                // the method will return true and the date is valid
                DateTime testDate = new
                    DateTime(Convert.ToInt32(dateParts[2]),
                    Convert.ToInt32(dateParts[0]),
                    Convert.ToInt32(dateParts[1]));

                return true;
            }
            catch(Exception ex)
            {
                // if a test date cannot be created, the
                // method will return false
                return false;
            }
        }

        public static bool IsDate(Object obj)
        {
            string strDate = obj.ToString();
            try
            {
                DateTime dt = DateTime.Parse(strDate);
                if (dt != DateTime.MinValue && dt != DateTime.MaxValue)
                    return true;
                return false;
            }
            catch
            {
                return false;
            }
        }

        public string ConvertDateIntoAnotherTimeZoneDate(string dt,string timezoneid)
        {
            DateTime frmdt;
            DateTime newdt = DateTime.Now;
            string newdtfmt = string.Empty;

            if (dt != null && dt != "")
            {
                frmdt = Convert.ToDateTime(dt);
                newdt = new DateTime(frmdt.Year,frmdt.Month,frmdt.Day, frmdt.Hour,frmdt.Minute,frmdt.Second);

                TimeZoneInfo objTimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timezoneid);
                newdt = TimeZoneInfo.ConvertTime(newdt, objTimeZoneInfo);
                newdtfmt = String.Format("{0:yyyy-M-d HH:mm:ss}", newdt);
            }
            return newdtfmt;
        }

        public string ConvertDateIntoAnotherFormat1(string dt)
        {
            DateTime frmdt;
            DateTime newdt = DateTime.Now;
            string newdtfmt = string.Empty;

            if (dt != null && dt != "")
            {
                frmdt = Convert.ToDateTime(dt);
                newdt = new DateTime(frmdt.Year, frmdt.Month, frmdt.Day);
                newdtfmt = String.Format("{0:yyyy-dd-MM}", newdt);
            }
            return newdtfmt;
        }
        // created method for changing the date format to dd/MM/yyyy
        public string ConvertDateIntoAnotherFormat3(string dt)
        {
            DateTime frmdt;
            DateTime newdt = DateTime.Now;
            string newdtfmt = string.Empty;

            if (dt != null && dt != "")
            {
                frmdt = Convert.ToDateTime(dt);
                newdt = new DateTime(frmdt.Year, frmdt.Month, frmdt.Day);
                newdtfmt = String.Format("{0:dd-MM-yyyy}", newdt);
            }
            return newdtfmt;
        }

        public DateTime ConvertDateIntoAnotherFormat2(string date)
        {
            try
            {
                // for US, alter to suit if splitting on hyphen, comma, etc.
                string[] dateParts = date.Split('/');

                // create new date from the parts; if this does not fail
                // the method will return true and the date is valid
                DateTime testDate = new
                    DateTime(Convert.ToInt32(dateParts[2]),
                    Convert.ToInt32(dateParts[0]),
                    Convert.ToInt32(dateParts[1]));

                return testDate;
            }
            catch
            {
                // if a test date cannot be created, the
                // method will return false
                return Convert.ToDateTime("0000-00-00 00:00:00");
            }
        }

        //public DateTimeOffset ConvertIntoServerTime(string trandate)    
        public string ConvertIntoServerTime(string trandate)    
        {
            //trandate as sample 02/07/2013 01:04:00 +05:30
            string format = @"M/d/yyyy H:m:s zzz";
            string retServerTime = string.Empty;
            string arr;
            string[] PArrs;
            string[] MArrs;

            string newtrandate = trandate.Replace("\\","");
            if (trandate.IndexOf('p')!=-1) 
                newtrandate = trandate.Replace("p", "+");
            else if (trandate.IndexOf('m') != -1)
                newtrandate = trandate.Replace("m", "-");

//            HttpContext.Current.Response.Write(newtrandate);
			
            TimeSpan serverOffset = TimeZoneInfo.Local.GetUtcOffset(DateTimeOffset.Now);

            //DateTimeOffset clientTime = DateTimeOffset.ParseExact("2/7/2013 01:04:00 +05:30", format, CultureInfo.InvariantCulture);
			
																//	03/26/2013 18:25:52 +05:30
            DateTimeOffset clientTime = DateTimeOffset.ParseExact(newtrandate, format, CultureInfo.InvariantCulture);
            DateTimeOffset serverTime = clientTime.ToOffset(serverOffset);  // serverTime ->2/7/2013 5:34:00 AM +10:00 

            arr = serverTime.ToString();
			
//            HttpContext.Current.Response.Write("Arr->" + arr);
//            HttpContext.Current.Response.End();
			

            PArrs = arr.Split('+');

            if (PArrs.GetType().IsArray)
            {
                if (PArrs.Length >= 0)
                    retServerTime = PArrs[0];
            }
            else
            {
                MArrs = arr.Split('-');
                if (MArrs.GetType().IsArray)
                {
                    if (MArrs.Length >= 0)
                        retServerTime = MArrs[0];
                }
            }
            return retServerTime;
        }

        public void switchingbeteenlocation2company(bool mode)
        {
            if (mode == true)
            {
                if (HttpContext.Current.Session["R_ID"] != "" && HttpContext.Current.Session["R_Name"] != "")
                {
                    HttpContext.Current.Session["OR_ID"] = HttpContext.Current.Session["R_ID"];
                    HttpContext.Current.Session["OR_Name"] = HttpContext.Current.Session["R_Name"];

                    HttpContext.Current.Session["R_ID"] = "";
                    HttpContext.Current.Session["R_Name"] = "";
                }
            }
            else
            {
                if (HttpContext.Current.Session["OR_ID"] != "" && HttpContext.Current.Session["OR_Name"] != "")
                {
                    HttpContext.Current.Session["R_ID"] = HttpContext.Current.Session["OR_ID"];
                    HttpContext.Current.Session["R_Name"] = HttpContext.Current.Session["OR_Name"];

                    HttpContext.Current.Session["OR_ID"] = "";
                    HttpContext.Current.Session["OR_Name"] = "";
                }
            }
        }

        public static string[] GetStringInBetween(string strBegin,  string strEnd, string strSource,  bool includeBegin , bool includeEnd)
        {

            string[] result = { "", "" };

            int iIndexOfBegin = strSource.IndexOf(strBegin);

            if (iIndexOfBegin != -1)
            {

                // include the Begin string if desired

                if (includeBegin)

                    iIndexOfBegin -= strBegin.Length;

                strSource = strSource.Substring(iIndexOfBegin

                    + strBegin.Length);

                int iEnd = strSource.IndexOf(strEnd);

                if (iEnd != -1)
                {

                    // include the End string if desired

                    if (includeEnd)

                        iEnd += strEnd.Length;

                    result[0] = strSource.Substring(0, iEnd);

                    // advance beyond this segment

                    if (iEnd + strEnd.Length < strSource.Length)

                        result[1] = strSource.Substring(iEnd

                            + strEnd.Length);

                }

            }

            else

                // stay where we are

                result[1] = strSource;

            return result;

        }

    }
}