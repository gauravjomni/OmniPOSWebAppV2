using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using MyDB;
using Commons;


/// <summary>
/// Summary description for getUserInfo
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class getRestaurantInfo : System.Web.Services.WebService {

    public getRestaurantInfo()
    {
        //Uncomment the following line if using designed components 
        //InitializeComponent();         
    }

    [WebMethod]
    /*public string HelloWorld() {
        return "Hello World";
    }*/

    public XmlElement getRestaurantDetails(string param1, string val1, string param2 , string val2)
    {
        DataSet ds = new DataSet();
        Common Fn = new Common();
        DB mConnection = new DB();

        MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
        Byte[] hashedDataBytes;
        
        UTF8Encoding encoder = new UTF8Encoding();
        hashedDataBytes = md5Hasher.ComputeHash(encoder.GetBytes(val2));


        SqlParameter[] arParms = new SqlParameter[2];
        SqlParameter[] ArParamsComp = new SqlParameter[9];

        arParms[0] = new SqlParameter("@UserName", SqlDbType.VarChar,25);
        arParms[0].Value = val1;

        // @Password Input Parameter
        arParms[1] = new SqlParameter("@usrpwd", SqlDbType.Binary, 50);
        arParms[1].Value = hashedDataBytes;

//        Dictionary<string, Byte[]> dict = new Dictionary<string, Byte[]>() { { "UserPassword", hashedDataBytes } };

        try
        {
            ArParamsComp[0] = new SqlParameter("@CompanyName", SqlDbType.VarChar, 50);
            ArParamsComp[0].Direction = ParameterDirection.Output;

            ArParamsComp[1] = new SqlParameter("@Address", SqlDbType.VarChar, 2000);
            ArParamsComp[1].Direction = ParameterDirection.Output;

            ArParamsComp[2] = new SqlParameter("@Email", SqlDbType.VarChar, 100);
            ArParamsComp[2].Direction = ParameterDirection.Output;

            ArParamsComp[3] = new SqlParameter("@Phone", SqlDbType.VarChar, 50);
            ArParamsComp[3].Direction = ParameterDirection.Output;

            ArParamsComp[4] = new SqlParameter("@Fax", SqlDbType.VarChar, 50);
            ArParamsComp[4].Direction = ParameterDirection.Output;

            ArParamsComp[5] = new SqlParameter("@ABNNo", SqlDbType.VarChar, 25);
            ArParamsComp[5].Direction = ParameterDirection.Output;

            ArParamsComp[6] = new SqlParameter("@Tax", SqlDbType.Decimal, 7);
            ArParamsComp[6].Direction = ParameterDirection.Output;
            ArParamsComp[6].Scale = 2;

            ArParamsComp[7] = new SqlParameter("@Currency", SqlDbType.VarChar, 5);
            ArParamsComp[7].Direction = ParameterDirection.Output;

            ArParamsComp[8] = new SqlParameter("@DateFormat", SqlDbType.VarChar, 20);
            ArParamsComp[8].Direction = ParameterDirection.Output;

            XmlDocument doc = new XmlDocument();
            XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", null,null);
            doc.AppendChild(dec);
            XmlElement DocRoot;
            DocRoot = doc.CreateElement("RestaurantInfo");
            doc.AppendChild(DocRoot);

            using (SqlConnection conn = mConnection.GetConnection())
            {
                conn.Open();

                string company = Fn.GetCompanyInfo(conn);

                XmlNode Company_Name = doc.CreateElement("Company");
                Company_Name.InnerText = company;
                DocRoot.AppendChild(Company_Name);

                try
                {
                    SqlHelper.ExecuteNonQuery(conn, CommandType.StoredProcedure, "getCompanyDetails", ArParamsComp);

                    //ds = Fn.LoadRestaurant_WebService(null, param1, val1, arParms);
                    ds = Fn.LoadRestaurant_WebService(null, param1, val1, arParms, conn);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            XmlNode RestID = doc.CreateElement("RestID");
                            RestID.InnerText = dr["Rest_Id"].ToString();
                            DocRoot.AppendChild(RestID);

                            XmlNode RestName = doc.CreateElement("RestName");
                            RestName.InnerText = dr["RestName"].ToString();
                            DocRoot.AppendChild(RestName);

                            XmlNode Initials = doc.CreateElement("Initials");
                            Initials.InnerText = dr["Initials"].ToString();
                            DocRoot.AppendChild(Initials);

                            XmlNode Address1 = doc.CreateElement("Address1");
                            Address1.InnerText = dr["Address1"].ToString();
                            DocRoot.AppendChild(Address1);

                            XmlNode Address2 = doc.CreateElement("Address2");
                            Address2.InnerText = dr["Address2"].ToString();
                            DocRoot.AppendChild(Address2);

                            XmlNode City = doc.CreateElement("City");
                            City.InnerText = dr["City"].ToString();
                            DocRoot.AppendChild(City);

                            XmlNode StateID = doc.CreateElement("StateID");
                            StateID.InnerText = dr["StateID"].ToString();
                            DocRoot.AppendChild(StateID);

                            XmlNode Zip = doc.CreateElement("Zip");
                            Zip.InnerText = dr["Zip"].ToString();
                            DocRoot.AppendChild(Zip);

                            XmlNode Phone = doc.CreateElement("Phone");
                            Phone.InnerText = dr["Phone"].ToString();
                            DocRoot.AppendChild(Phone);

                            XmlNode Fax = doc.CreateElement("Fax");
                            Fax.InnerText = dr["Fax"].ToString();
                            DocRoot.AppendChild(Fax);

                            XmlNode Email = doc.CreateElement("Email");
                            Email.InnerText = dr["Email"].ToString();
                            DocRoot.AppendChild(Email);

                            XmlNode TablesCount = doc.CreateElement("TablesCount");
                            TablesCount.InnerText = dr["TablesCount"].ToString();
                            DocRoot.AppendChild(TablesCount);


                            XmlNode Header_Name = doc.CreateElement("Header_Name");
                            Header_Name.InnerText = dr["Header_Name"].ToString();
                            DocRoot.AppendChild(Header_Name);

                            XmlNode Header_Address1 = doc.CreateElement("Header_Address1");
                            Header_Address1.InnerText = dr["Header_Address1"].ToString();
                            DocRoot.AppendChild(Header_Address1);

                            XmlNode Header_City = doc.CreateElement("Header_City");
                            Header_City.InnerText = dr["Header_City"].ToString();
                            DocRoot.AppendChild(Header_City);

                            XmlNode Header_State = doc.CreateElement("Header_State");
                            Header_State.InnerText = dr["Header_State"].ToString();
                            DocRoot.AppendChild(Header_State);

                            XmlNode Header_Zip = doc.CreateElement("Header_Zip");
                            Header_Zip.InnerText = dr["Header_Zip"].ToString();
                            DocRoot.AppendChild(Header_Zip);

                            XmlNode Header_Phone = doc.CreateElement("Header_Phone");
                            Header_Phone.InnerText = dr["Header_Phone"].ToString();
                            DocRoot.AppendChild(Header_Phone);

                            XmlNode Header_ABN = doc.CreateElement("Header_ABN");
                            Header_ABN.InnerText = dr["Header_ABN"].ToString();
                            DocRoot.AppendChild(Header_ABN);

                            XmlNode Header_TaxInvoice = doc.CreateElement("Header_TaxInvoice");
                            Header_TaxInvoice.InnerText = dr["Header_TaxInvoice"].ToString();
                            DocRoot.AppendChild(Header_TaxInvoice);

                            XmlNode Header_Website = doc.CreateElement("Header_Website");
                            Header_Website.InnerText = dr["Header_Website"].ToString();
                            DocRoot.AppendChild(Header_Website);

                            XmlNode Header_Email = doc.CreateElement("Header_Email");
                            Header_Email.InnerText = dr["Header_Email"].ToString();
                            DocRoot.AppendChild(Header_Email);

                            XmlNode Footer1 = doc.CreateElement("Footer1");
                            Footer1.InnerText = dr["Footer1"].ToString();
                            DocRoot.AppendChild(Footer1);

                            XmlNode Footer2 = doc.CreateElement("Footer2");
                            Footer2.InnerText = dr["Footer2"].ToString();
                            DocRoot.AppendChild(Footer2);

                            XmlNode KitchenView = doc.CreateElement("KitchenView");
                            KitchenView.InnerText = dr["KitchenView"].ToString();
                            DocRoot.AppendChild(KitchenView);

                            XmlNode ExpediteView = doc.CreateElement("ExpediteView");
                            ExpediteView.InnerText = dr["ExpediteView"].ToString();
                            DocRoot.AppendChild(ExpediteView);

                            XmlNode Tax_InclusiveView = doc.CreateElement("Tax");
                            Tax_InclusiveView.InnerText = dr["Tax"].ToString();
                            DocRoot.AppendChild(Tax_InclusiveView);

                            XmlNode CreateDate = doc.CreateElement("CreateDate");
                            CreateDate.InnerText = dr["CreatedOn"].ToString();
                            DocRoot.AppendChild(CreateDate);

                            XmlNode CreatedByUserID = doc.CreateElement("CreatedByUserID");
                            CreatedByUserID.InnerText = dr["CreatedBy"].ToString();
                            DocRoot.AppendChild(CreatedByUserID);

                            XmlNode ModifyDate = doc.CreateElement("ModifyDate");
                            ModifyDate.InnerText = dr["ModifiedOn"].ToString();
                            DocRoot.AppendChild(ModifyDate);

                            XmlNode ModifiedByUserID = doc.CreateElement("ModifiedByUserID");
                            ModifiedByUserID.InnerText = dr["ModifiedBy"].ToString();
                            DocRoot.AppendChild(ModifiedByUserID);

                            XmlNode CurrencySymbol = doc.CreateElement("CurrencySymbol");
                            CurrencySymbol.InnerText = ArParamsComp[7].Value.ToString();
                            DocRoot.AppendChild(CurrencySymbol);

                            //XmlNode IsDeleted = doc.CreateElement("IsDeleted");
                            //IsDeleted.InnerText = dr["IsDeleted"].ToString();
                            //DocRoot.AppendChild(IsDeleted);

                            //XmlNode DeletedOn = doc.CreateElement("DeletedOn");
                            //DeletedOn.InnerText = dr["DeletedOn"].ToString();
                            //DocRoot.AppendChild(DeletedOn);

                            XmlNode Status = doc.CreateElement("Status");
                            Status.InnerText = dr["Status"].ToString();
                            DocRoot.AppendChild(Status);

                            /*********** Settings ****************************/

                            XmlNode CustomerView = doc.CreateElement("CustomerView");
                            CustomerView.InnerText = dr["CustomerView"].ToString();
                            DocRoot.AppendChild(CustomerView);

                            XmlNode AddLines_Between_Order_Item = doc.CreateElement("AddLinesBetweenOrderItem");
                            AddLines_Between_Order_Item.InnerText = dr["AddLines_Between_Order_Item"].ToString();
                            DocRoot.AppendChild(AddLines_Between_Order_Item);

                            XmlNode Sort_Course_By = doc.CreateElement("SortCourseBy");
                            Sort_Course_By.InnerText = dr["Sort_Course_By"].ToString();
                            DocRoot.AppendChild(Sort_Course_By);

                            XmlNode Print_Transferred_Order = doc.CreateElement("PrintTransferredOrder");
                            Print_Transferred_Order.InnerText = dr["Print_Transferred_Order"].ToString();
                            DocRoot.AppendChild(Print_Transferred_Order);

                            XmlNode Print_Deleted_Order = doc.CreateElement("PrintDeletedOrder");
                            Print_Deleted_Order.InnerText = dr["Print_Deleted_Order"].ToString();
                            DocRoot.AppendChild(Print_Deleted_Order);

                            XmlNode Print_Voided_Items = doc.CreateElement("PrintVoidedItems");
                            Print_Voided_Items.InnerText = dr["Print_Voided_Items"].ToString();
                            DocRoot.AppendChild(Print_Voided_Items);

                            XmlNode Kitchen_View_Timeout = doc.CreateElement("KitchenViewTimeout");
                            Kitchen_View_Timeout.InnerText = dr["Kitchen_View_Timeout"].ToString();
                            DocRoot.AppendChild(Kitchen_View_Timeout);

                            XmlNode Allow_Void_Order_Item = doc.CreateElement("AllowVoidOrderItem");
                            Allow_Void_Order_Item.InnerText = dr["Allow_Void_Order_Item"].ToString();
                            DocRoot.AppendChild(Allow_Void_Order_Item);

                            XmlNode Allow_Delete_Send_Order = doc.CreateElement("AllowDeleteSendOrder");
                            Allow_Delete_Send_Order.InnerText = dr["Allow_Delete_Send_Order"].ToString();
                            DocRoot.AppendChild(Allow_Delete_Send_Order);

                            XmlNode Quick_Service = doc.CreateElement("QuickService");
                            Quick_Service.InnerText = dr["Quick_Service"].ToString();
                            DocRoot.AppendChild(Quick_Service);

                            XmlNode Table_Layout_Type = doc.CreateElement("TableLayoutType");
                            Table_Layout_Type.InnerText = dr["Table_Layout_Type"].ToString();
                            DocRoot.AppendChild(Table_Layout_Type);

                            XmlNode Auto_Prompt_Tip = doc.CreateElement("AutoPromptTip");
                            Auto_Prompt_Tip.InnerText = dr["Auto_Prompt_Tip"].ToString();
                            DocRoot.AppendChild(Auto_Prompt_Tip);

                            XmlNode Sort_Items_By = doc.CreateElement("SortItemsBy");
                            Sort_Items_By.InnerText = dr["Sort_Items_By"].ToString();
                            DocRoot.AppendChild(Sort_Items_By);

                            XmlNode Sort_SubCategories_By = doc.CreateElement("SortSubCategoriesBy");
                            Sort_SubCategories_By.InnerText = dr["Sort_SubCategories_By"].ToString();
                            DocRoot.AppendChild(Sort_SubCategories_By);

                            XmlNode Sort_Products_By = doc.CreateElement("SortProductsBy");
                            Sort_Products_By.InnerText = dr["Sort_Products_By"].ToString();
                            DocRoot.AppendChild(Sort_Products_By);

                            XmlNode No_Of_Devices = doc.CreateElement("NoOfDevices");
                            No_Of_Devices.InnerText = dr["No_Of_Devices"].ToString();
                            DocRoot.AppendChild(No_Of_Devices);

                            XmlNode Use_Table_Layout = doc.CreateElement("UseTableLayout");
                            Use_Table_Layout.InnerText = dr["Use_Table_Layout"].ToString();
                            DocRoot.AppendChild(Use_Table_Layout);

                            XmlNode Notify_No_Sale = doc.CreateElement("ShouldNotifyNoSale");
                            TablesCount.InnerText = dr["Notify_No_Sale"].ToString();
                            DocRoot.AppendChild(Notify_No_Sale);


                            /**************************************************/

                        }
                    }
                    else
                    {
                        XmlNode RestInfo = doc.CreateElement("RestInfo");
                        RestInfo.InnerText = "No Data";
                        DocRoot.AppendChild(RestInfo);

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
			 return DocRoot;
        }
        catch (Exception e)
        {
            HttpContext.Current.Response.Write(e.Message.ToString());
            return null;
        }

    }

}

