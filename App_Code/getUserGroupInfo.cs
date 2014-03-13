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
public class getUserGroupInfo : System.Web.Services.WebService {

    public getUserGroupInfo()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
        
    }

    [WebMethod]
    /*public string HelloWorld() {
        return "Hello World";
    }*/

    //public XmlElement getUserGroupDetails(string param, string val)
    public XmlElement getUserGroupDetails()
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
            DocRoot = doc.CreateElement("UserGroupObjects");
            doc.AppendChild(DocRoot);

            using (SqlConnection conn = mConnection.GetConnection())
            {
                conn.Open();

                try
                {
                    ds = Fn.LoadUserGroups(conn);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            XmlNode UserGroupInfo = doc.CreateElement("UserGroupInfo");
                            DocRoot.AppendChild(UserGroupInfo);

                            XmlNode UserGroupID = doc.CreateElement("UserGroupID");
                            UserGroupID.InnerText = dr["UserGroupID"].ToString();
                            UserGroupInfo.AppendChild(UserGroupID);

                            XmlNode UserGroupName = doc.CreateElement("UserGroupName");
                            UserGroupName.InnerText = dr["UserGroupName"].ToString();
                            UserGroupInfo.AppendChild(UserGroupName);

                            XmlNode Status = doc.CreateElement("Status");
                            Status.InnerText = dr["Status"].ToString();
                            UserGroupInfo.AppendChild(Status);

                            XmlNode CreateDate = doc.CreateElement("CreateDate");
                            CreateDate.InnerText = dr["CreateDate"].ToString();
                            UserGroupInfo.AppendChild(CreateDate);

                            XmlNode CreatedByUserID = doc.CreateElement("CreatedByUserID");
                            CreatedByUserID.InnerText = dr["CreatedByUserID"].ToString();
                            UserGroupInfo.AppendChild(CreatedByUserID);

                            XmlNode ModifyDate = doc.CreateElement("ModifyDate");
                            ModifyDate.InnerText = dr["ModifyDate"].ToString();
                            UserGroupInfo.AppendChild(ModifyDate);

                            XmlNode ModifiedByUserID = doc.CreateElement("ModifiedByUserID");
                            ModifiedByUserID.InnerText = dr["ModifiedByUserID"].ToString();
                            UserGroupInfo.AppendChild(ModifiedByUserID);
                        }
                    }
                    else
                    {
                        XmlNode UserGroupInfo = doc.CreateElement("UserGroupInfo");
                        UserGroupInfo.InnerText = "No Data";
                        DocRoot.AppendChild(UserGroupInfo);
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

