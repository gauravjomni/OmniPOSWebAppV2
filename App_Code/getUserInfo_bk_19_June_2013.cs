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
public class getUserInfo_bk_19_June_2013 : System.Web.Services.WebService {

    public getUserInfo_bk_19_June_2013()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
        
    }

    [WebMethod]
    /*public string HelloWorld() {
        return "Hello World";
    }*/

    public XmlElement getUserDetails(string param, string val)
    {
        DataSet ds = new DataSet();
        Common Fn = new Common();
        
        //dict = new Dictionary<string, string>() { { "UserGroupId", GroupID.Value } };
        Dictionary<string, string> dict = null;

        try
        {

            ds = Fn.LoadUsers_WebService(dict, param, val);

            if (ds.Tables[0].Rows.Count > 0)
            {
                XmlDataDocument xmldata = new XmlDataDocument(ds);
                XmlElement xmlElement = xmldata.DocumentElement;
                return xmlElement; 
            }
            else
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml("<UserPin><Data>Not Found</Data></UserPin>");
                XmlElement root = doc.DocumentElement;
                return root;
            }
                
        }
        catch (Exception e)
        {
            HttpContext.Current.Response.Write(e.Message.ToString());
            return null;
        }

    }

    public XmlElement getAllUserInfo(string param, string val)
    {
        DataSet ds = new DataSet();
        Common Fn = new Common();

        //dict = new Dictionary<string, string>() { { "UserGroupId", GroupID.Value } };
        Dictionary<string, string> dict = null;

        try
        {

            XmlDocument doc = new XmlDocument();
            XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", null,null);
            doc.AppendChild(dec);
            XmlElement DocRoot;
            DocRoot = doc.CreateElement("UserObjects");
            doc.AppendChild(DocRoot);

            ds = Fn.LoadAllUserInfoSQL_WebService(dict, param, val);

            if (ds.Tables[0].Rows.Count > 0)
            {
               foreach (DataRow dr in ds.Tables[0].Rows)
                {
                   XmlNode UserInfo = doc.CreateElement("UserInfo");
                   XmlNode UserID = doc.CreateElement("UserID");
                   UserID.Value = dr["UserID"].ToString();
                   UserInfo.AppendChild(UserID); 
                }
            }
            else
            {
                XmlNode UserInfo = doc.CreateElement("UserInfo");
                XmlNode UserID = doc.CreateElement("UserID");
                UserID.Value = "";
                UserInfo.AppendChild(UserID); 
            }
            return DocRoot;
        }
        catch (Exception e)
        {
            HttpContext.Current.Response.Write(e.Message.ToString());
            return null;
        }

    }

    public XmlElement getRestaurantDetails(string param, string val)
    {
        DataSet ds = new DataSet();
        Common Fn = new Common();

        //dict = new Dictionary<string, string>() { { "UserGroupId", GroupID.Value } };
        Dictionary<string, string> dict = null;

        try
        {

            ds = Fn.LoadUsers(dict, param, val);

            if (ds.Tables[0].Rows.Count > 0)
            {
                XmlDataDocument xmldata = new XmlDataDocument(ds);
                XmlElement xmlElement = xmldata.DocumentElement;
                return xmlElement;
            }
            else
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml("<UserPin><Data>Not Found</Data></UserPin>");
                XmlElement root = doc.DocumentElement;
                return root;
            }

        }
        catch (Exception e)
        {
            HttpContext.Current.Response.Write(e.Message.ToString());
            return null;
        }

    }
    
}

