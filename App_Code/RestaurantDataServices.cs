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

/// <summary>
/// Summary description for RestaurantDataServices
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class RestaurantDataServices : System.Web.Services.WebService {

    DataSet ds = new DataSet();
    Common Fn = new Common();
    DB mConnection = new DB();
    Dictionary<string, string> dict = null;
    string paramValue = null, valValue = null;

    public RestaurantDataServices () {

    }

    /*
     *  All the methods below take params: Rest_ID, and val  
     * 
     */

    [WebMethod]
    public XmlElement getUsers(string param, string val)
    {
        string[] dataElements = new string[] { "UserID", "UserName", "UserPin", "UserEmail", "UserGroupID", "IsActive", "FirstName", "LastName", "UserAlias", "UserPHone", "StartDate", "CreateDate", "CreatedByUserID", "ModifyDate", "ModifiedByUserID", "HourlyRate"};
        string[] dataElementKeys = new string[] { "UserID", "UserName", "UserPin", "UserEmail", "UserGroupID", "Status", "FirstName", "LastName", "UserAlias", "UserPHone", "StartDate", "CreateDate", "CreatedByUserID", "ModifyDate", "ModifiedByUserID", "HourlyRate" };
        paramValue = param;
        valValue = val;
        XmlElement doc = this.getDataElement(Fn.LoadAllUserInfoSQL_WebService, "UserObjects", "UserInfo", dataElements, dataElementKeys);

        return doc;
    }

    [WebMethod]
    public XmlElement getProducts(string param, string val)
    {
        /*
        string[] dataElements = new string[] { "ProductID", "ProductName", "ProductName2", "ProductDescription", "Color", "GST", "HasOpenPrice", "ChangePrice", "Price1", "Price2", "StockInHand", "SortOrder", "CategoryID", "ProductImageWithPath", "CourseID", "IsActive", "CreateDate", "CreatedByUserID", "ModifyDate", "ModifiedByUserID" };
        string[] dataElementKeys = new string[] { "ProductID", "ProductName", "ProductName2", "ProductDescription", "Color", "GST", "HasOpenPrice", "ChangePrice", "Price1", "Price2", "StockInHand", "SortOrder", "CategoryID", "ProductImageWithPath", "CourseID", "IsActive", "CreatedOn", "CreatedByUserID", "ModifiedOn", "ModifiedByUserID" };
         paramValue = param;
        valValue = val;
        XmlElement doc = this.getDataElement(Fn.LoadProducts, "ProductObjects", "ProductInfo", dataElements, dataElementKeys);

        return doc;

        */
        DataSet ds1 = new DataSet();
        DataSet ds2 = new DataSet();
        DataSet ds3 = new DataSet();
        DataSet ds4 = new DataSet();

        try
        {
            XmlDocument doc = new XmlDocument();
            XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", null, null);
            doc.AppendChild(dec);
            XmlElement DocRoot;
            DocRoot = doc.CreateElement("ProductObjects");
            doc.AppendChild(DocRoot);

            using (SqlConnection conn = mConnection.GetConnection())
            {
                conn.Open();

                try
                {
                    ds = Fn.LoadProducts(dict, param, val, conn);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            XmlNode ProductInfo = doc.CreateElement("ProductInfo");
                            DocRoot.AppendChild(ProductInfo);

                            XmlNode ProductID = doc.CreateElement("ProductID");
                            ProductID.InnerText = dr["ProductID"].ToString();
                            ProductInfo.AppendChild(ProductID);

                            XmlNode ProductName = doc.CreateElement("ProductName");
                            ProductName.InnerText = dr["ProductName"].ToString();
                            ProductInfo.AppendChild(ProductName);

                            XmlNode ProductName2 = doc.CreateElement("ProductName2");
                            ProductName2.InnerText = dr["ProductName2"].ToString();
                            ProductInfo.AppendChild(ProductName2);

                            XmlNode ProductDescription = doc.CreateElement("ProductDescription");
                            ProductDescription.InnerText = dr["ProductDescription"].ToString();
                            ProductInfo.AppendChild(ProductDescription);

                            XmlNode Color = doc.CreateElement("Color");
                            Color.InnerText = dr["Color"].ToString();
                            ProductInfo.AppendChild(Color);

                            XmlNode GST = doc.CreateElement("GST");
                            GST.InnerText = dr["GST"].ToString();
                            ProductInfo.AppendChild(GST);

                            XmlNode HasOpenPrice = doc.CreateElement("HasOpenPrice");
                            HasOpenPrice.InnerText = dr["HasOpenPrice"].ToString();
                            ProductInfo.AppendChild(HasOpenPrice);

                            XmlNode ChangePrice = doc.CreateElement("ChangePrice");
                            ChangePrice.InnerText = dr["ChangePrice"].ToString();
                            ProductInfo.AppendChild(ChangePrice);

                            XmlNode Price1 = doc.CreateElement("Price1");
                            Price1.InnerText = dr["Price1"].ToString();
                            ProductInfo.AppendChild(Price1);

                            XmlNode Price2 = doc.CreateElement("Price2");
                            Price2.InnerText = dr["Price2"].ToString();
                            ProductInfo.AppendChild(Price2);

                            XmlNode Points = doc.CreateElement("Points");
                            Points.InnerText = dr["Points"].ToString();
                            ProductInfo.AppendChild(Points);

                            XmlNode StockInHand = doc.CreateElement("StockInHand");
                            StockInHand.InnerText = dr["StockInHand"].ToString();
                            ProductInfo.AppendChild(StockInHand);

                            XmlNode SortOrder = doc.CreateElement("SortOrder");
                            SortOrder.InnerText = dr["SortOrder"].ToString();
                            ProductInfo.AppendChild(SortOrder);

                            XmlNode CategoryID = doc.CreateElement("CategoryID");
                            CategoryID.InnerText = dr["CategoryID"].ToString();
                            ProductInfo.AppendChild(CategoryID);

                            XmlNode ProductImageWithPath = doc.CreateElement("ProductImageWithPath");
                            ProductImageWithPath.InnerText = (dr["ProductImageWithPath"] == "" || dr["ProductImageWithPath"] == null) ? "" : "http://" + HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath + "/" + dr["ProductImageWithPath"].ToString();
                            ProductInfo.AppendChild(ProductImageWithPath);

                            XmlNode CourseID = doc.CreateElement("CourseID");
                            CourseID.InnerText = dr["CourseID"].ToString();
                            ProductInfo.AppendChild(CourseID);

                            XmlNode IsActive = doc.CreateElement("IsActive");
                            IsActive.InnerText = dr["Status"].ToString();
                            ProductInfo.AppendChild(IsActive);

                            XmlNode CreateDate = doc.CreateElement("CreateDate");
                            CreateDate.InnerText = dr["CreatedOn"].ToString();
                            ProductInfo.AppendChild(CreateDate);

                            XmlNode CreatedByUserID = doc.CreateElement("CreatedByUserID");
                            CreatedByUserID.InnerText = dr["CreatedByUserID"].ToString();
                            ProductInfo.AppendChild(CreatedByUserID);

                            XmlNode ModifyDate = doc.CreateElement("ModifyDate");
                            ModifyDate.InnerText = dr["ModifiedOn"].ToString();
                            ProductInfo.AppendChild(ModifyDate);

                            XmlNode ModifiedByUserID = doc.CreateElement("ModifiedByUserID");
                            ModifiedByUserID.InnerText = dr["ModifiedByUserID"].ToString();
                            ProductInfo.AppendChild(ModifiedByUserID);

                            XmlNode ModifierObject = doc.CreateElement("ModifierObjects");
                            ProductInfo.AppendChild(ModifierObject);

                            ds1 = Fn.LoadProductModifiers(null, "ProductID", dr["ProductID"].ToString(), conn);

                            if (ds1.Tables[0].Rows.Count > 0)
                            {
                                foreach (DataRow dr1 in ds1.Tables[0].Rows)
                                {
                                    XmlNode ProductModifierInfo = doc.CreateElement("ProductModifierInfo");
                                    ModifierObject.AppendChild(ProductModifierInfo);

                                    XmlNode ProductModifierID = doc.CreateElement("ProductModifierID");
                                    ProductModifierID.InnerText = dr1["Product_ModifierId"].ToString();
                                    ProductModifierInfo.AppendChild(ProductModifierID);

                                    XmlNode ModifierID = doc.CreateElement("ModifierID");
                                    ModifierID.InnerText = dr1["ModifierID"].ToString();
                                    ProductModifierInfo.AppendChild(ModifierID);
                                }
                            }
                            else
                            {
                                XmlNode ProductModifierInfo = doc.CreateElement("ProductModifierInfo");
                                ProductModifierInfo.InnerText = "No Data";
                                ModifierObject.AppendChild(ProductModifierInfo);
                            }


                            XmlNode cookingOptionObjects = doc.CreateElement("cookingOptionObjects");
                            ProductInfo.AppendChild(cookingOptionObjects);

                            ds2 = Fn.LoadProductCookingOptions(null, "ProductID", dr["ProductID"].ToString(), conn);

                            if (ds2.Tables[0].Rows.Count > 0)
                            {
                                foreach (DataRow dr2 in ds2.Tables[0].Rows)
                                {
                                    XmlNode ProductCookingOptionInfo = doc.CreateElement("ProductCookingOptionInfo");
                                    cookingOptionObjects.AppendChild(ProductCookingOptionInfo);

                                    XmlNode ProductCookingOptionID = doc.CreateElement("ProductCookingOptionID");
                                    ProductCookingOptionID.InnerText = dr2["Product_CookingOptionId"].ToString();
                                    ProductCookingOptionInfo.AppendChild(ProductCookingOptionID);

                                    XmlNode OptionID = doc.CreateElement("OptionID");
                                    OptionID.InnerText = dr2["OptionID"].ToString();
                                    ProductCookingOptionInfo.AppendChild(OptionID);
                                }
                            }
                            else
                            {
                                XmlNode ProductCookingOptionInfo = doc.CreateElement("ProductCookingOptionInfo");
                                ProductCookingOptionInfo.InnerText = "No Data";
                                cookingOptionObjects.AppendChild(ProductCookingOptionInfo);
                            }

                            Dictionary<string, string> dict1 = new Dictionary<string, string>() { { "OptionType", "K" } }; ;

                            XmlNode kitchenObjects = doc.CreateElement("kitchenObjects");
                            ProductInfo.AppendChild(kitchenObjects);

                            ds3 = Fn.LoadProductKitchenPrinters(dict1, "ProductID", dr["ProductID"].ToString(), conn);

                            if (ds3.Tables[0].Rows.Count > 0)
                            {
                                foreach (DataRow dr3 in ds3.Tables[0].Rows)
                                {
                                    XmlNode ProductKitchenInfo = doc.CreateElement("ProductKitchenInfo");
                                    kitchenObjects.AppendChild(ProductKitchenInfo);

                                    XmlNode ProductKtchID = doc.CreateElement("kitchenId");
                                    ProductKtchID.InnerText = dr3["Product_Ktch_PrntID"].ToString();
                                    ProductKitchenInfo.AppendChild(ProductKtchID);

                                    XmlNode OptionID = doc.CreateElement("OptionID");
                                    OptionID.InnerText = dr3["OptionID"].ToString();
                                    ProductKitchenInfo.AppendChild(OptionID);

                                    XmlNode OptionType = doc.CreateElement("OptionType");
                                    OptionType.InnerText = dr3["OptionName"].ToString();
                                    ProductKitchenInfo.AppendChild(OptionType);
                                }
                            }
                            else
                            {
                                XmlNode ProductKitchenInfo = doc.CreateElement("ProductKitchenInfo");
                                ProductKitchenInfo.InnerText = "No Data";
                                kitchenObjects.AppendChild(ProductKitchenInfo);
                            }

                            Dictionary<string, string> dict2 = new Dictionary<string, string>() { { "OptionType", "P" } }; ;

                            XmlNode printerObjects = doc.CreateElement("printerObjects");
                            ProductInfo.AppendChild(printerObjects);

                            ds4 = Fn.LoadProductKitchenPrinters(dict2, "ProductID", dr["ProductID"].ToString(), conn);

                            if (ds4.Tables[0].Rows.Count > 0)
                            {
                                foreach (DataRow dr4 in ds4.Tables[0].Rows)
                                {
                                    XmlNode ProductPrinterInfo = doc.CreateElement("ProductPrinterInfo");
                                    printerObjects.AppendChild(ProductPrinterInfo);

                                    XmlNode printerId = doc.CreateElement("printerId");
                                    printerId.InnerText = dr4["Product_Ktch_PrntID"].ToString();
                                    ProductPrinterInfo.AppendChild(printerId);

                                    XmlNode OptionID = doc.CreateElement("OptionID");
                                    OptionID.InnerText = dr4["OptionID"].ToString();
                                    ProductPrinterInfo.AppendChild(OptionID);

                                    XmlNode OptionType = doc.CreateElement("OptionType");
                                    OptionType.InnerText = dr4["OptionName"].ToString();
                                    ProductPrinterInfo.AppendChild(OptionType);
                                }
                            }
                            else
                            {
                                XmlNode ProductPrinterInfo = doc.CreateElement("ProductPrinterInfo");
                                ProductPrinterInfo.InnerText = "No Data";
                                printerObjects.AppendChild(ProductPrinterInfo);
                            }

                        }
                    }
                    else
                    {
                        XmlNode ProductInfo = doc.CreateElement("ProductInfo");
                        ProductInfo.InnerText = "No Data";
                        DocRoot.AppendChild(ProductInfo);
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
    public XmlElement getCategories(string param, string val)
    {
        string[] dataElements = new string[] { "CategoryName", "CategoryId", "Name2", "ParentId", "SortOrder", "Status", "CreateDate", "CreatedByUserID", "ModifyDate", "ModifiedByUserID" };
        string[] dataElementKeys = new string[] { "CategoryName", "CategoryID", "Name2", "ParentId", "SortOrder", "Status", "CreateDate", "CreatedByUserID", "ModifiedDate", "ModifiedByUserID" };
        paramValue = param;
        valValue = val;
        XmlElement doc = this.getDataElement(Fn.LoadCategories, "CategoryObjects", "CategoryInfo", dataElements, dataElementKeys);

        return doc;
    }


    [WebMethod]
    public XmlElement getCookingOptions(string param, string val)
    {
        string[] dataElements = new string[] { "OptionID", "OptionName", "Status", "CreateDate", "CreatedByUserID", "ModifyDate", "ModifiedByUserID" };
        string[] dataElementKeys = new string[] { "OptionID", "OptionName", "Status", "CreatedOn", "CreatedByUserID", "ModifiedOn", "ModifiedByUserID" };
        paramValue = param;
        valValue = val;
        XmlElement doc = this.getDataElement(Fn.LoadCookingOptions, "CookingOptionObjects", "CookingOptionInfo", dataElements, dataElementKeys);

        return doc;
    }


    [WebMethod]
    public XmlElement getCourses(string param, string val)
    {
        string[] dataElements = new string[] { "CourseID", "CourseName", "SortOrder", "Status", "CreateDate", "CreatedByUserID", "ModifyDate", "ModifiedByUserID" };
        string[] dataElementKeys = new string[] { "CourseID", "CourseName", "SortOrder", "Status", "CreatedOn", "CreatedByUserID", "ModifiedOn", "ModifiedByUserID" };
        paramValue = param;
        valValue = val;
        XmlElement doc = this.getDataElement(Fn.LoadCourses, "CourseObjects", "CourseInfo", dataElements, dataElementKeys);

        return doc;
    }

    [WebMethod]

    public XmlElement getPOSDevices(string param, string val)
    {
        string[] dataElements = new string[] { "DeviceID", "DeviceName", "PrinterID", "Status", "CreateDate", "CreatedByUserID", "ModifyDate", "ModifiedByUserID" };
        string[] dataElementKeys = new string[] { "DeviceID", "DeviceName", "PrinterID", "Status", "CreatedOn", "CreatedByUserID", "ModifiedOn", "ModifiedByUserID" };
        paramValue = param;
        valValue = val;
        XmlElement doc = this.getDataElement(Fn.LoadDeviceInfo, "DeviceInfoObjects", "DeviceInfo", dataElements, dataElementKeys);

        return doc;
    }

    [WebMethod]

    public XmlElement getSpecialInstructions(string param, string val)
    {
        string[] dataElements = new string[] { "InstructionID", "Message", "Status", "CreateDate", "CreatedByUserID", "ModifyDate", "ModifiedByUserID" };
        string[] dataElementKeys = new string[] { "InstructionID", "Message", "Status", "CreatedOn", "CreatedByUserID", "ModifiedOn", "ModifiedByUserID" };
        paramValue = param;
        valValue = val;
        XmlElement doc = this.getDataElement(Fn.LoadInstruction, "InstructionObjects", "InstructionInfo", dataElements, dataElementKeys);

        return doc;
    }

    [WebMethod]

    public XmlElement getKitchens(string param, string val)
    {
        string[] dataElements = new string[] { "KitchenId", "KitchenName", "SortOrder", "Status", "CreateDate", "CreatedByUserID", "ModifyDate", "ModifiedByUserID" };
        string[] dataElementKeys = new string[] { "KitchenId", "KitchenName", "SortOrder", "Status", "CreatedOn", "CreatedByUserID", "ModifiedOn", "ModifiedByUserID" };
        paramValue = param;
        valValue = val;
        XmlElement doc = this.getDataElement(Fn.LoadKitchens, "KitchenObjects", "KitchenInfo", dataElements, dataElementKeys);

        return doc;
    }


    [WebMethod]

    public XmlElement getProductModifiers(string param, string val)
    {
        string[] dataElements = new string[] { "ModifierId", "ModifierName", "Name2", "Price1", "Price2", "Description", "ModifierLevelID", "SortOrder", "GST", "Status", "CreateDate", "CreatedByUserID", "ModifyDate", "ModifiedByUserID" };
        string[] dataElementKeys = new string[] { "ModifierId", "ModifierName", "Name2", "Price1", "Price2", "Description", "ModifierLevelID", "SortOrder", "GST", "Status", "CreatedOn", "CreatedByUserID", "ModifiedOn", "ModifiedByUserID" };
        paramValue = param;
        valValue = val;
        XmlElement doc = this.getDataElement(Fn.LoadModifiers, "ModifierObjects", "ModifierInfo", dataElements, dataElementKeys);

        return doc;
    }

    [WebMethod]
    public XmlElement getModifierLevels(string param, string val)
    {
        string[] dataElements = new string[] { "LevelID", "ModifierLevelName", "Status", "CreateDate", "CreatedByUserID", "ModifyDate", "ModifiedByUserID" };
        string[] dataElementKeys = new string[] { "LevelID", "ModifierLevelName", "Status", "CreatedOn", "CreatedByUserID", "ModifiedOn", "ModifiedByUserID" };
        paramValue = param;
        valValue = val;
        XmlElement doc = this.getDataElement(Fn.LoadModifierLevel, "ModifierLevelObjects", "ModifierLevelInfo", dataElements, dataElementKeys);

        return doc;
    }

    [WebMethod]
    public XmlElement getNotes(string param, string val)
    {
        string[] dataElements = new string[] { "NoteID", "Message", "Status", "CreateDate", "CreatedByUserID", "ModifyDate", "ModifiedByUserID" };
        string[] dataElementKeys = new string[] { "NoteID", "Message", "Status", "CreatedOn", "CreatedByUserID", "ModifiedOn", "ModifiedByUserID" };
        paramValue = param;
        valValue = val;
        XmlElement doc = this.getDataElement(Fn.LoadNotes, "NoteObjects", "NoteInfo", dataElements, dataElementKeys);

        return doc;
    }

    [WebMethod]
    public XmlElement getPrinters(string param, string val)
    {
        string[] dataElements = new string[] { "PrinterId", "PrinterName", "IPAddress", "PrinterType", "PosorItem", "NoOfCopies", "Trigger_Cash_Drawer", "IsPrintIpAddress", "Status", "CreateDate", "CreatedByUserID", "ModifyDate", "ModifiedByUserID" };
        string[] dataElementKeys = new string[] { "PrinterId", "PrinterName", "IPAddress", "PrinterType", "PosorItem", "NoOfCopies", "Trigger_Cash_Drawer", "IsPrintIpAddress", "Status", "CreatedOn", "CreatedByUserID", "ModifiedDate", "ModifiedByUserID" };
        paramValue = param;
        valValue = val;
        XmlElement doc = this.getDataElement(Fn.LoadPrinters, "PrinterObjects", "PrinterInfo", dataElements, dataElementKeys);

        return doc;
    }


    [WebMethod]
    public XmlElement getTaxRates(string param, string val)
    {
        string[] dataElements = new string[] { "InfoID", "TaxLiteral", "TaxRate", "Status", "CreateDate", "CreatedByUserID", "ModifyDate", "ModifiedByUserID" };
        string[] dataElementKeys = new string[] { "TaxInfoID", "TaxInfoLiteral", "TaxRate", "Status", "CreatedOn", "CreatedByUserID", "ModifiedOn", "ModifiedByUserID" };
        paramValue = param;
        valValue = val;
        XmlElement doc = this.getDataElement(Fn.LoadTaxInfo, "TaxInfoObjects", "TaxInfo", dataElements, dataElementKeys);

        return doc;
    }

    //####### Private Methods ########

    private XmlElement getDataElement(Func<Dictionary<string, string>, string, string, SqlConnection, DataSet> dataSetGetter, string rootElementName, string subRootElementName, string[] subSubElementNames, string[] subSubElementKeys)
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
                    ds = dataSetGetter(dict, paramValue, valValue, conn);

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
