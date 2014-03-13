using System;
using System.Collections;
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
/// Summary description for getUserInfo
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class getAllProductInfo : System.Web.Services.WebService {

    public getAllProductInfo()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent();
    }

    [WebMethod]
    /*public string HelloWorld() {
        return "Hello World";
    }*/

    public XmlElement getAllProductDetails(string param, string val)
    {
        DB mConnection = new DB();
        DataSet ds = new DataSet();
        DataSet ds1 = new DataSet();
        DataSet ds2 = new DataSet();
        DataSet ds3 = new DataSet();
        DataSet ds4 = new DataSet();
        Common Fn = new Common();

        //dict = new Dictionary<string, string>() { { "UserGroupId", GroupID.Value } };
        Dictionary<string, string> dict = null;

        try
        {
            XmlDocument doc = new XmlDocument();
            XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", null,null);
            doc.AppendChild(dec);
            XmlElement DocRoot;
            DocRoot = doc.CreateElement("ProductObjects");
            doc.AppendChild(DocRoot);

            using (SqlConnection conn = mConnection.GetConnection())
            {
                conn.Open();

                try
                {
                    ds = Fn.LoadProducts(dict, param, val,conn);

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

                            ds1 = Fn.LoadProductModifiers(null, "ProductID", dr["ProductID"].ToString(),conn);

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

                            ds2 = Fn.LoadProductCookingOptions(null, "ProductID", dr["ProductID"].ToString(),conn);

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

                            ds3 = Fn.LoadProductKitchenPrinters(dict1, "ProductID", dr["ProductID"].ToString(),conn);

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

                            ds4 = Fn.LoadProductKitchenPrinters(dict2, "ProductID", dr["ProductID"].ToString(),conn);

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

}

