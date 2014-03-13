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
public class getCookingOption : System.Web.Services.WebService {

    public getCookingOption()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
        
    }

    [WebMethod]
    /*public string HelloWorld() {
        return "Hello World";
    }*/

    public XmlElement getgetCookingOptionDetails(string param, string val)
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
            DocRoot = doc.CreateElement("CookingOptionObjects");
            doc.AppendChild(DocRoot);

            using (SqlConnection conn = mConnection.GetConnection())
            {
                conn.Open();

                try
                {
                    ds = Fn.LoadCookingOptions(dict, param, val, conn);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            XmlNode CookingOptionInfo = doc.CreateElement("CookingOptionInfo");
                            DocRoot.AppendChild(CookingOptionInfo);

                            XmlNode OptionID = doc.CreateElement("OptionID");
                            OptionID.InnerText = dr["OptionID"].ToString();
                            CookingOptionInfo.AppendChild(OptionID);

                            XmlNode OptionName = doc.CreateElement("OptionName");
                            OptionName.InnerText = dr["OptionName"].ToString();
                            CookingOptionInfo.AppendChild(OptionName);

                            XmlNode Status = doc.CreateElement("Status");
                            Status.InnerText = dr["Status"].ToString();
                            CookingOptionInfo.AppendChild(Status);

                            XmlNode CreateDate = doc.CreateElement("CreateDate");
                            CreateDate.InnerText = dr["CreatedOn"].ToString();
                            CookingOptionInfo.AppendChild(CreateDate);

                            XmlNode CreatedByUserID = doc.CreateElement("CreatedByUserID");
                            CreatedByUserID.InnerText = dr["CreatedByUserID"].ToString();
                            CookingOptionInfo.AppendChild(CreatedByUserID);

                            XmlNode ModifyDate = doc.CreateElement("ModifyDate");
                            ModifyDate.InnerText = dr["ModifiedOn"].ToString();
                            CookingOptionInfo.AppendChild(ModifyDate);

                            XmlNode ModifiedByUserID = doc.CreateElement("ModifiedByUserID");
                            ModifiedByUserID.InnerText = dr["ModifiedByUserID"].ToString();
                            CookingOptionInfo.AppendChild(ModifiedByUserID);
                        }
                    }
                    else
                    {
                        XmlNode CookingOptionInfo = doc.CreateElement("CookingOptionInfo");
                        CookingOptionInfo.InnerText = "No Data";
                        DocRoot.AppendChild(CookingOptionInfo);
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

