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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using MyQuery;
using System.IO;
using System.Text;
using MailTools;

/// <summary>
/// Summary description for RestaurantModifierServices
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]

public class RestaurantModifierServices : System.Web.Services.WebService {

    DB mConnection = new DB();
    Common Fn = new Common();
    SQLQuery Qry = new SQLQuery();
    DataSet ds = new DataSet();

    Dictionary<string, string> dict = null;
    string StrCurrency = string.Empty;

    public RestaurantModifierServices () {
    }

    /*
    *  This method allows us to adjust the sale in a date range;
    *  The changeList contains a list of product which in turn contains the org_qty, adj_qty, org_amt, _adj_amt, and id to be processed;
    *  
   */
    [WebMethod]
    public XmlElement adjustSaleBySubcategoryChangeList(string fromdate, string tilldate, string rest_Id, string changeList)
    {
        string changeListXml;
        DataSet productDataset = new DataSet();
        DataTable productDataTable = new DataTable("Products");

        changeListXml = changeList.ToString();
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(changeListXml);
        productDataset.ReadXml(new StringReader(changeListXml));
        productDataTable = productDataset.Tables[2];

        int hcount = productDataTable.Rows.Count;

        if (fromdate != "" && Fn.ValidateDate(fromdate))
            fromdate = String.Format("{0:yyyy-MM-dd}", Fn.ConvertDateIntoAnotherFormat2(fromdate));

        if (tilldate != "" && Fn.ValidateDate(tilldate))
            tilldate = String.Format("{0:yyyy-MM-dd}", Fn.ConvertDateIntoAnotherFormat2(tilldate));

        OPProduct[] allProducts = new OPProduct[hcount];

        for (int row = 0; row < hcount; row++)
        {
            int id = Convert.ToInt32(productDataTable.Rows[row]["id"].ToString());
            decimal org_qty = Convert.ToDecimal(productDataTable.Rows[row]["org_qty"].ToString());
            decimal adj_qty = Convert.ToDecimal(productDataTable.Rows[row]["adj_qty"].ToString());
            decimal org_amt = Convert.ToDecimal(productDataTable.Rows[row]["org_amt"].ToString());
            decimal adj_amt = Convert.ToDecimal(productDataTable.Rows[row]["adj_amt"].ToString());

            OPProduct prod = new OPProduct(id, org_qty, adj_qty, org_amt, adj_amt);
            allProducts[row] = prod;
        }

        //database operation
        OPSaleAdjustment adjustment = new OPSaleAdjustment();
        bool result = adjustment.applyAdjustmentOnProducts(allProducts, fromdate, tilldate, rest_Id);

        XmlDocument doc = new XmlDocument();
        XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", null, null);
        doc.AppendChild(dec);
        XmlElement DocRoot = null;
        DocRoot = doc.CreateElement("Result");
        doc.AppendChild(DocRoot);

        string isSuccess = (result ? "true" : "false");

        DocRoot.AppendChild(XMLNodeCreator.xmlNodeForElement("IsSuccess", isSuccess, doc));
        return DocRoot;
    }
}
