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
public class getCustomerInfo : System.Web.Services.WebService {

    public getCustomerInfo()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
        
    }

    [WebMethod]
    /*public string HelloWorld() {
        return "Hello World";
    }*/

    public XmlElement getAllCustomerInfo()
    {
        DataSet ds = new DataSet();
        Common Fn = new Common();
        DB mConnection = new DB();

        try
        {

            XmlDocument doc = new XmlDocument();
            XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", null,null);
            doc.AppendChild(dec);
            XmlElement DocRoot;
            DocRoot = doc.CreateElement("CustomerObjects");
            doc.AppendChild(DocRoot);

            using (SqlConnection conn = mConnection.GetConnection())
            {
                conn.Open();

                try
                {
                    ds = Fn.LoadCustomer(conn);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            XmlNode CustomerInfo = doc.CreateElement("CustomerInfo");
                            DocRoot.AppendChild(CustomerInfo);

                            XmlNode CustomerID = doc.CreateElement("ID");
                            CustomerID.InnerText = dr["CustomerId"].ToString();
                            CustomerInfo.AppendChild(CustomerID);

                            XmlNode FirstName = doc.CreateElement("FirstName");
                            FirstName.InnerText = dr["FirstName"].ToString();
                            CustomerInfo.AppendChild(FirstName);

                            XmlNode LastName = doc.CreateElement("LastName");
                            LastName.InnerText = dr["LastName"].ToString();
                            CustomerInfo.AppendChild(LastName);

                            XmlNode Email = doc.CreateElement("Email");
                            Email.InnerText = dr["Email"].ToString();
                            CustomerInfo.AppendChild(Email);

                            XmlNode Phone = doc.CreateElement("Phone");
                            Phone.InnerText = dr["Phone"].ToString();
                            CustomerInfo.AppendChild(Phone);

                            XmlNode Fax = doc.CreateElement("Fax");
                            Fax.InnerText = dr["Fax"].ToString();
                            CustomerInfo.AppendChild(Fax);

                            XmlNode Address1 = doc.CreateElement("Address1");
                            Address1.InnerText = dr["Address1"].ToString();
                            CustomerInfo.AppendChild(Address1);

                            XmlNode Address2 = doc.CreateElement("Address2");
                            Address2.InnerText = dr["Address2"].ToString();
                            CustomerInfo.AppendChild(Address2);

                            XmlNode ZipCode = doc.CreateElement("ZipCode");
                            ZipCode.InnerText = dr["ZipCode"].ToString();
                            CustomerInfo.AppendChild(ZipCode);
                        }
                    }
                    else
                    {
                        //    XmlNode CustomerInfo = doc.CreateElement("CustomerInfo");
                        //    XmlNode CustomerID = doc.CreateElement("CustomerID");
                        //    CustomerID.Value = "";
                        //    CustomerInfo.AppendChild(CustomerID); 
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

