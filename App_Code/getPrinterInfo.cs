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
public class getPrinterInfo : System.Web.Services.WebService {

    public getPrinterInfo()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
        
    }

    [WebMethod]
    /*public string HelloWorld() {
        return "Hello World";
    }*/

    public XmlElement getPrinterDetails(string param, string val)
    {
        DataSet ds = new DataSet();
        Common Fn = new Common();
        DB mConnection = new DB();

        //dict = new Dictionary<string, string>() { { "UserGroupId", GroupID.Value } };
        Dictionary<string, string> dict = null;

        try
        {

            XmlDocument doc = new XmlDocument();
            XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", null,null);
            doc.AppendChild(dec);
            XmlElement DocRoot;
            DocRoot = doc.CreateElement("PrinterObjects");
            doc.AppendChild(DocRoot);

            using (SqlConnection conn = mConnection.GetConnection())
            {
                conn.Open();

                try
                {
                    ds = Fn.LoadPrinters(dict, param, val,conn);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            XmlNode PrinterInfo = doc.CreateElement("PrinterInfo");
                            DocRoot.AppendChild(PrinterInfo);

                            XmlNode PrinterId = doc.CreateElement("PrinterId");
                            PrinterId.InnerText = dr["PrinterId"].ToString();
                            PrinterInfo.AppendChild(PrinterId);

                            XmlNode PrinterName = doc.CreateElement("PrinterName");
                            PrinterName.InnerText = dr["PrinterName"].ToString();
                            PrinterInfo.AppendChild(PrinterName);

                            XmlNode IPAddress = doc.CreateElement("IPAddress");
                            IPAddress.InnerText = dr["IPAddress"].ToString();
                            PrinterInfo.AppendChild(IPAddress);

                            XmlNode PrinterType = doc.CreateElement("PrinterType");
                            PrinterType.InnerText = dr["PrinterType"].ToString();
                            PrinterInfo.AppendChild(PrinterType);

                            XmlNode PosorItem = doc.CreateElement("PosorItem");
                            PosorItem.InnerText = dr["PosorItem"].ToString();
                            PrinterInfo.AppendChild(PosorItem);

                            XmlNode NoOfCopies = doc.CreateElement("NoOfCopies");
                            NoOfCopies.InnerText = dr["NoOfCopies"].ToString();
                            PrinterInfo.AppendChild(NoOfCopies);

                            XmlNode Trigger_Cash_Drawer = doc.CreateElement("Trigger_Cash_Drawer");
                            Trigger_Cash_Drawer.InnerText = dr["Trigger_Cash_Drawer"].ToString();
                            PrinterInfo.AppendChild(Trigger_Cash_Drawer);

                            XmlNode IsPrintIpAddress = doc.CreateElement("IsPrintIpAddress");
                            IsPrintIpAddress.InnerText = dr["IsPrintIpAddress"].ToString();
                            PrinterInfo.AppendChild(IsPrintIpAddress);

                            XmlNode Status = doc.CreateElement("Status");
                            Status.InnerText = dr["Status"].ToString();
                            PrinterInfo.AppendChild(Status);

                            XmlNode CreateDate = doc.CreateElement("CreateDate");
                            CreateDate.InnerText = dr["CreatedOn"].ToString();
                            PrinterInfo.AppendChild(CreateDate);

                            XmlNode CreatedByUserID = doc.CreateElement("CreatedByUserID");
                            CreatedByUserID.InnerText = dr["CreatedByUserID"].ToString();
                            PrinterInfo.AppendChild(CreatedByUserID);

                            XmlNode ModifyDate = doc.CreateElement("ModifyDate");
                            ModifyDate.InnerText = dr["ModifiedDate"].ToString();
                            PrinterInfo.AppendChild(ModifyDate);

                            XmlNode ModifiedByUserID = doc.CreateElement("ModifiedByUserID");
                            ModifiedByUserID.InnerText = dr["ModifiedByUserID"].ToString();
                            PrinterInfo.AppendChild(ModifiedByUserID);
                        }
                    }
                    else
                    {
                        XmlNode PrinterInfo = doc.CreateElement("PrinterInfo");
                        PrinterInfo.InnerText = "No Data";
                        DocRoot.AppendChild(PrinterInfo);
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

