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
public class getModifierInfo : System.Web.Services.WebService {

    public getModifierInfo()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
        
    }

    [WebMethod]
    /*public string HelloWorld() {
        return "Hello World";
    }*/

    public XmlElement getModifierDetails(string param, string val)
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
            DocRoot = doc.CreateElement("ModifierObjects");
            doc.AppendChild(DocRoot);

            using (SqlConnection conn = mConnection.GetConnection())
            {
                conn.Open();

                try
                {
                    ds = Fn.LoadModifiers(dict, param, val,conn);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            XmlNode ModifierInfo = doc.CreateElement("ModifierInfo");
                            DocRoot.AppendChild(ModifierInfo);

                            XmlNode ModifierId = doc.CreateElement("ModifierId");
                            ModifierId.InnerText = dr["ModifierId"].ToString();
                            ModifierInfo.AppendChild(ModifierId);

                            XmlNode ModifierName = doc.CreateElement("ModifierName");
                            ModifierName.InnerText = dr["ModifierName"].ToString();
                            ModifierInfo.AppendChild(ModifierName);

                            XmlNode Name2 = doc.CreateElement("Name2");
                            Name2.InnerText = dr["Name2"].ToString();
                            ModifierInfo.AppendChild(Name2);

                            XmlNode Price1 = doc.CreateElement("Price1");
                            Price1.InnerText = dr["Price1"].ToString();
                            ModifierInfo.AppendChild(Price1);

                            XmlNode Price2 = doc.CreateElement("Price2");
                            Price2.InnerText = dr["Price2"].ToString();
                            ModifierInfo.AppendChild(Price2);

                            XmlNode Description = doc.CreateElement("Description");
                            Description.InnerText = dr["Description"].ToString();
                            ModifierInfo.AppendChild(Description);

                            XmlNode ModifierLevelID = doc.CreateElement("ModifierLevelID");
                            ModifierLevelID.InnerText = dr["ModifierLevelID"].ToString();
                            ModifierInfo.AppendChild(ModifierLevelID);

                            XmlNode SortOrder = doc.CreateElement("SortOrder");
                            SortOrder.InnerText = dr["SortOrder"].ToString();
                            ModifierInfo.AppendChild(SortOrder);

                            XmlNode GST = doc.CreateElement("GST");
                            GST.InnerText = dr["GST"].ToString();
                            ModifierInfo.AppendChild(GST);

                            XmlNode Status = doc.CreateElement("Status");
                            Status.InnerText = dr["Status"].ToString();
                            ModifierInfo.AppendChild(Status);

                            XmlNode CreateDate = doc.CreateElement("CreateDate");
                            CreateDate.InnerText = dr["CreatedOn"].ToString();
                            ModifierInfo.AppendChild(CreateDate);

                            XmlNode CreatedByUserID = doc.CreateElement("CreatedByUserID");
                            CreatedByUserID.InnerText = dr["CreatedByUserID"].ToString();
                            ModifierInfo.AppendChild(CreatedByUserID);

                            XmlNode ModifyDate = doc.CreateElement("ModifyDate");
                            ModifyDate.InnerText = dr["ModifiedOn"].ToString();
                            ModifierInfo.AppendChild(ModifyDate);

                            XmlNode ModifiedByUserID = doc.CreateElement("ModifiedByUserID");
                            ModifiedByUserID.InnerText = dr["ModifiedByUserID"].ToString();
                            ModifierInfo.AppendChild(ModifiedByUserID);
                        }
                    }
                    else
                    {
                        XmlNode ModifierLevelInfo = doc.CreateElement("ModifierLevelInfo");
                        ModifierLevelInfo.InnerText = "No Data";
                        DocRoot.AppendChild(ModifierLevelInfo);

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

