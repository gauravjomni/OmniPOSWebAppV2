using System;
using System.Collections.Generic;
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
using System.Security.Cryptography;
using System.Text;

/// <summary>
/// Summary description for CompanyDataServices
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class CompanyDataServices : System.Web.Services.WebService {

    DataSet ds = new DataSet();
    Common Fn = new Common();
    DB mConnection = new DB();

    public CompanyDataServices () {

       
    }

    [WebMethod]
    public XmlElement getRestaurant(string param1, string val1, string param2, string val2)
    {
        MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
        Byte[] hashedDataBytes;

        UTF8Encoding encoder = new UTF8Encoding();
        hashedDataBytes = md5Hasher.ComputeHash(encoder.GetBytes(val2));

        SqlParameter[] arParms = new SqlParameter[2];
        SqlParameter[] ArParamsComp = new SqlParameter[9];

        arParms[0] = new SqlParameter("@UserName", SqlDbType.VarChar, 25);
        arParms[0].Value = val1;

        // @Password Input Parameter
        arParms[1] = new SqlParameter("@usrpwd", SqlDbType.Binary, 50);
        arParms[1].Value = hashedDataBytes;

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
            XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", null, null);
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

                    ds = Fn.LoadRestaurant_WebService(null, param1, val1, arParms, conn);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            DocRoot.AppendChild(XMLNodeCreator.xmlNodeForElement("RestID", dr["Rest_Id"].ToString(), doc));
                            DocRoot.AppendChild(XMLNodeCreator.xmlNodeForElement("RestName", dr["RestName"].ToString(), doc));
                            DocRoot.AppendChild(XMLNodeCreator.xmlNodeForElement("Initials", dr["Initials"].ToString(), doc));
                            DocRoot.AppendChild(XMLNodeCreator.xmlNodeForElement("Address1", dr["Address1"].ToString(), doc));

                            DocRoot.AppendChild(XMLNodeCreator.xmlNodeForElement("Address2", dr["Address2"].ToString(), doc));
                            DocRoot.AppendChild(XMLNodeCreator.xmlNodeForElement("City", dr["City"].ToString(), doc));
                            DocRoot.AppendChild(XMLNodeCreator.xmlNodeForElement("StateID", dr["StateID"].ToString(), doc));
                            DocRoot.AppendChild(XMLNodeCreator.xmlNodeForElement("Zip", dr["Zip"].ToString(), doc));

                            DocRoot.AppendChild(XMLNodeCreator.xmlNodeForElement("Phone", dr["Phone"].ToString(), doc));
                            DocRoot.AppendChild(XMLNodeCreator.xmlNodeForElement("Fax", dr["Fax"].ToString(), doc));
                            DocRoot.AppendChild(XMLNodeCreator.xmlNodeForElement("Email", dr["Email"].ToString(), doc));
                            DocRoot.AppendChild(XMLNodeCreator.xmlNodeForElement("TablesCount", dr["TablesCount"].ToString(), doc));

                            DocRoot.AppendChild(XMLNodeCreator.xmlNodeForElement("Header_Name", dr["Header_Name"].ToString(), doc));
                            DocRoot.AppendChild(XMLNodeCreator.xmlNodeForElement("Header_Address1", dr["Header_Address1"].ToString(), doc));
                            DocRoot.AppendChild(XMLNodeCreator.xmlNodeForElement("Header_City", dr["Header_City"].ToString(), doc));
                            DocRoot.AppendChild(XMLNodeCreator.xmlNodeForElement("Header_State", dr["Header_State"].ToString(), doc));

                            DocRoot.AppendChild(XMLNodeCreator.xmlNodeForElement("Header_Zip", dr["Header_Zip"].ToString(), doc));
                            DocRoot.AppendChild(XMLNodeCreator.xmlNodeForElement("Header_Phone", dr["Header_Phone"].ToString(), doc));
                            DocRoot.AppendChild(XMLNodeCreator.xmlNodeForElement("Header_ABN", dr["Header_ABN"].ToString(), doc));
                            DocRoot.AppendChild(XMLNodeCreator.xmlNodeForElement("Header_TaxInvoice", dr["Header_TaxInvoice"].ToString(), doc));

                            DocRoot.AppendChild(XMLNodeCreator.xmlNodeForElement("Header_Website", dr["Header_Website"].ToString(), doc));
                            DocRoot.AppendChild(XMLNodeCreator.xmlNodeForElement("Header_Email", dr["Header_Email"].ToString(), doc));
                            DocRoot.AppendChild(XMLNodeCreator.xmlNodeForElement("Footer1", dr["Footer1"].ToString(), doc));
                            DocRoot.AppendChild(XMLNodeCreator.xmlNodeForElement("Footer2", dr["Footer2"].ToString(), doc));

                            DocRoot.AppendChild(XMLNodeCreator.xmlNodeForElement("KitchenView", dr["KitchenView"].ToString(), doc));
                            DocRoot.AppendChild(XMLNodeCreator.xmlNodeForElement("ExpediteView", dr["ExpediteView"].ToString(), doc));
                            DocRoot.AppendChild(XMLNodeCreator.xmlNodeForElement("Tax", dr["Tax"].ToString(), doc));
                            DocRoot.AppendChild(XMLNodeCreator.xmlNodeForElement("CreateDate", dr["CreatedOn"].ToString(), doc));

                            DocRoot.AppendChild(XMLNodeCreator.xmlNodeForElement("ModifyDate", dr["ModifiedOn"].ToString(), doc));
                            DocRoot.AppendChild(XMLNodeCreator.xmlNodeForElement("ModifiedByUserID", dr["ModifiedBy"].ToString(), doc));
                            DocRoot.AppendChild(XMLNodeCreator.xmlNodeForElement("CurrencySymbol", ArParamsComp[7].Value.ToString(), doc));
                            DocRoot.AppendChild(XMLNodeCreator.xmlNodeForElement("Status", dr["Status"].ToString(), doc));

                            /*********** Settings ****************************/

                            DocRoot.AppendChild(XMLNodeCreator.xmlNodeForElement("CustomerView", dr["CustomerView"].ToString(), doc));
                            DocRoot.AppendChild(XMLNodeCreator.xmlNodeForElement("AddLinesBetweenOrderItem", dr["AddLines_Between_Order_Item"].ToString(), doc));
                            DocRoot.AppendChild(XMLNodeCreator.xmlNodeForElement("SortCourseBy", dr["Sort_Course_By"].ToString(), doc));
                            DocRoot.AppendChild(XMLNodeCreator.xmlNodeForElement("PrintTransferredOrder", dr["Print_Transferred_Order"].ToString(), doc));

                            DocRoot.AppendChild(XMLNodeCreator.xmlNodeForElement("PrintDeletedOrder", dr["Print_Deleted_Order"].ToString(), doc));
                            DocRoot.AppendChild(XMLNodeCreator.xmlNodeForElement("PrintVoidedItems", dr["Print_Voided_Items"].ToString(), doc));
                            DocRoot.AppendChild(XMLNodeCreator.xmlNodeForElement("KitchenViewTimeout", dr["Kitchen_View_Timeout"].ToString(), doc));
                            DocRoot.AppendChild(XMLNodeCreator.xmlNodeForElement("AllowVoidOrderItem", dr["Allow_Void_Order_Item"].ToString(), doc));

                            DocRoot.AppendChild(XMLNodeCreator.xmlNodeForElement("AllowDeleteSendOrder", dr["Allow_Delete_Send_Order"].ToString(), doc));
                            DocRoot.AppendChild(XMLNodeCreator.xmlNodeForElement("QuickService", dr["Quick_Service"].ToString(), doc));
                            DocRoot.AppendChild(XMLNodeCreator.xmlNodeForElement("TableLayoutType", dr["Table_Layout_Type"].ToString(), doc));
                            DocRoot.AppendChild(XMLNodeCreator.xmlNodeForElement("AutoPromptTip", dr["Auto_Prompt_Tip"].ToString(), doc));

                            DocRoot.AppendChild(XMLNodeCreator.xmlNodeForElement("SortItemsBy", dr["Sort_Items_By"].ToString(), doc));
                            DocRoot.AppendChild(XMLNodeCreator.xmlNodeForElement("SortSubCategoriesBy", dr["Sort_SubCategories_By"].ToString(), doc));
                            DocRoot.AppendChild(XMLNodeCreator.xmlNodeForElement("SortProductsBy", dr["Sort_Products_By"].ToString(), doc));
                            DocRoot.AppendChild(XMLNodeCreator.xmlNodeForElement("NoOfDevices", dr["No_Of_Devices"].ToString(), doc));

                            DocRoot.AppendChild(XMLNodeCreator.xmlNodeForElement("UseTableLayout", dr["Use_Table_Layout"].ToString(), doc));
                            DocRoot.AppendChild(XMLNodeCreator.xmlNodeForElement("ShouldNotifyNoSale", dr["Notify_No_Sale"].ToString(), doc));

                            DocRoot.AppendChild(XMLNodeCreator.xmlNodeForElement("ShouldEnableClockIn", dr["ShouldEnableClockIn"].ToString(), doc));
                            DocRoot.AppendChild(XMLNodeCreator.xmlNodeForElement("ScannerMode", dr["ScannerMode"].ToString(), doc));

                            DocRoot.AppendChild(XMLNodeCreator.xmlNodeForElement("CashDrawerBalancing", dr["CashDrawerBalancing"].ToString(), doc));
                            DocRoot.AppendChild(XMLNodeCreator.xmlNodeForElement("HoldAndFire", dr["HoldAndFire"].ToString(), doc));
                            DocRoot.AppendChild(XMLNodeCreator.xmlNodeForElement("NoSaleLimit", dr["NoSaleLimit"].ToString(), doc));
                            /**************************************************/

                        }
                    }
                    else
                    {
                        DocRoot.AppendChild(XMLNodeCreator.xmlNodeForElement("RestInfo", "No Data", doc));
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


    [WebMethod]
    public XmlElement getCustomers()
    {
        string[] dataElements = new string[] { "ID", "FirstName", "LastName", "Email", "Phone", "Fax", "Address1", "Address2", "ZipCode" };
        string[] dataElementKeys = new string[] { "CustomerId", "FirstName", "LastName", "Email", "Phone", "Fax", "Address1", "Address2", "ZipCode" };
        XmlElement doc = this.getDataElement(Fn.LoadCustomer, "CustomerObjects", "CustomerInfo", dataElements, dataElementKeys);

        return doc;
    }


    [WebMethod]

    public XmlElement getUserGroups()
    {
        string[] dataElements = new string[] { "UserGroupID", "UserGroupName", "Status", "CreateDate", "CreatedByUserID", "ModifyDate", "ModifiedByUserID" };
        string[] dataElementKeys = new string[] { "UserGroupID", "UserGroupName", "Status", "CreateDate", "CreatedByUserID", "ModifyDate", "ModifiedByUserID" };
        XmlElement doc = this.getDataElement(Fn.LoadUserGroups, "UserGroupObjects", "UserGroupInfo", dataElements, dataElementKeys);

        return doc;
    }


    ///////////////////////////
    //####### Private Methods ########

    private XmlElement getDataElement(Func<SqlConnection, DataSet> dataSetGetter, string rootElementName, string subRootElementName, string[] subSubElementNames, string[] subSubElementKeys)
    {
        try
        {
            XmlDocument doc = new XmlDocument();
            XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", null, null);
            doc.AppendChild(dec);
            XmlElement DocRoot;

            DocRoot = doc.CreateElement(rootElementName);
            doc.AppendChild(DocRoot);

            using (SqlConnection conn = mConnection.GetConnection())
            {
                conn.Open();

                try
                {
                    ds = dataSetGetter(conn);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            XmlNode subRootInfo = doc.CreateElement(subRootElementName);
                            DocRoot.AppendChild(subRootInfo);

                            int keyIndex = 0;
                            foreach (string subSubElement in subSubElementNames)
                            {
                                string keyName = subSubElementKeys[keyIndex];
                                subRootInfo.AppendChild(XMLNodeCreator.xmlNodeForElement(subSubElement, dr[keyName].ToString(), doc));
                                keyIndex++;
                            }
                        }
                    }
                    else
                    {
                        DocRoot.AppendChild(XMLNodeCreator.xmlNodeForElement(subRootElementName, "No Data", doc));
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
