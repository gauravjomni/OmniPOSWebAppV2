using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public partial class parsedata : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        var data = "{\"order\": {\"totalSurcharge\":\"0.00\",\"tableClosedOn\":null,\"tableOpenedOn\":\"2013-01-07 14:23:24\",  \"userId\":\"22\",\"totalAmount\":\"76.36\",\"orderNo\":\"000005\",\"totalDiscount\":\"0.00\",\"paymentType\":null,\"tipAmount\":\"0.00\",\"tableNo\":\"347215_TA5\",\"orderedOn\":\"2013-01-07 15:03:09\",\"totalTax\":\"6.36\",\"noOfGuest\":\"0\",\"companyId\":\"1\",\"grossAmount\":\"70.00\",\"restaurantId\":null,\"deviceId\":\"5\",\"date\":\"2013-01-07 15:03:12\" },\"Product\" : [{\"quantity\":\"1.00\",\"amount\":\"38.18\",\"prodId\":\"32\"},{\"quantity\":\"1.00\",\"amount\":\"38.18\",\"prodId\":\"24\"},{\"quantity\":\"1.00\",\"amount\":\"38.18\",\"prodId\":\"22\"}]}";

        JObject o = JObject.Parse(data);

        var orderdata = o["order"].ToString();
        var productdata = o["Product"].ToString();

        JObject orderdetails = JObject.Parse(orderdata);
        Response.Write("Order ->" + o["order"]);
        Response.Write("Order No ->" + orderdetails["orderNo"]);

        Response.Write("<br />");
        Response.Write("<br />");

        Response.Write("Product (0) ->" + o["Product"][0]);

        JObject prdetails = JObject.Parse(o["Product"][0].ToString());
        Response.Write("<br />");

        Response.Write("Product details of (0) ->" + prdetails["prodId"]);
        Response.Write("Product details of (0) ->" + prdetails["prodId"]);
    }
}
