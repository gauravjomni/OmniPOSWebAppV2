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
using Newtonsoft.Json;


public partial class ajax : System.Web.UI.Page
{
    DB mConnection = new DB();
    Common Fn = new Common();
    MyToolSet iTool = new MyToolSet();
    SQLQuery Qry = new SQLQuery();
    public static string strList;

    protected void Page_Load(object sender, EventArgs e)
    {
        /*if (IsPostBack)
        {*/
/*            string strIngName = iTool.formatInputString(Request.Form["Ing_Name"]);
            string strlist = "";
            Dictionary<string, string> dict;
            dict = new Dictionary<string, string>() { { "a.Rest_ID", Session["R_ID"].ToString() } };
            
            if (strIngName!="")
                strlist = Fn.GetIngredientColumnValue_Conv_IntoString(dict, "IngredientName", strIngName, mConnection.GetConnection());
        */

        string strList = "{\"page\":null,\"total\":0,\"records\":\"10\",\"rows\":[{\"id\":\"1\",\"name\":\"C++\",\"author\":\"Balaguruswamy\"},{\"id\":\"2\",\"name\":\"Visual Basic\",\"author\":\"Balaguruswamy\"},{\"id\":\"3\",\"name\":\"C\",\"author\":\"Kanitkar\"},{\"id\":\"4\",\"name\":\"Java\",\"author\":\"Stroustoup\"},{\"id\":\"5\",\"name\":\"PHP6\",\"author\":\"Wrox\"}]}";
        Response.Write(strList);


		//string strList = "['India'",'Pakistan'",\"China\",\"Indiana\",\"Indonesia\"]";

        //strList = "[{\"id\":\"1\",\"name\":\"C++\",\"author\":\"Balaguruswamy\"},{\"id\":\"2\",\"name\":\"Visual Basic\",\"author\":\"Balaguruswamy\"},{\"id\":\"3\",\"name\":\"C\",\"author\":\"Kanitkar\"},{\"id\":\"4\",\"name\":\"Java\",\"author\":\"Stroustoup\"},{\"id\":\"5\",\"name\":\"PHP6\",\"author\":\"Wrox\"}]";

        //Response.Write(strList);
        //}                
    }


}