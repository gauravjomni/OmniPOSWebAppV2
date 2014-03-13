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
public class getCatSubCategoryInfo : System.Web.Services.WebService {

    public getCatSubCategoryInfo()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
        
    }

    [WebMethod]
    /*public string HelloWorld() {
        return "Hello World";
    }*/

    public XmlElement getCatSubCategoryDetails(string param, string val)
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
            DocRoot = doc.CreateElement("CategoryObjects");
            doc.AppendChild(DocRoot);

            using (SqlConnection conn = mConnection.GetConnection())
            {
                conn.Open();

                try
                {
                    ds = Fn.LoadCategories(dict, param, val,conn);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                       foreach (DataRow dr in ds.Tables[0].Rows)
                       {
                           XmlNode CategoryInfo = doc.CreateElement("CategoryInfo");
                           DocRoot.AppendChild(CategoryInfo);

                           XmlNode CategoryID = doc.CreateElement("CategoryId");
                           CategoryID.InnerText = dr["CategoryID"].ToString();
                           CategoryInfo.AppendChild(CategoryID);

                           XmlNode CategoryName = doc.CreateElement("CategoryName");
                           CategoryName.InnerText = dr["CategoryName"].ToString();
                           CategoryInfo.AppendChild(CategoryName);

                           XmlNode Name2 = doc.CreateElement("Name2");
                           Name2.InnerText = dr["Name2"].ToString();
                           CategoryInfo.AppendChild(Name2);

                           XmlNode ParentId = doc.CreateElement("ParentId");
                           ParentId.InnerText = dr["ParentId"].ToString();
                           CategoryInfo.AppendChild(ParentId);

                           XmlNode SortOrder = doc.CreateElement("SortOrder");
                           SortOrder.InnerText = dr["SortOrder"].ToString();
                           CategoryInfo.AppendChild(SortOrder);

                           XmlNode Status = doc.CreateElement("Status");
                           Status.InnerText = dr["Status"].ToString();
                           CategoryInfo.AppendChild(Status);

                           XmlNode CreateDate = doc.CreateElement("CreateDate");
                           CreateDate.InnerText = dr["CreateDate"].ToString();
                           CategoryInfo.AppendChild(CreateDate);

                           XmlNode CreatedByUserID = doc.CreateElement("CreatedByUserID");
                           CreatedByUserID.InnerText = dr["CreatedByUserID"].ToString();
                           CategoryInfo.AppendChild(CreatedByUserID);

                           XmlNode ModifyDate = doc.CreateElement("ModifyDate");
                           ModifyDate.InnerText = dr["ModifiedDate"].ToString();
                           CategoryInfo.AppendChild(ModifyDate);

                           XmlNode ModifiedByUserID = doc.CreateElement("ModifiedByUserID");
                           ModifiedByUserID.InnerText = dr["ModifiedByUserID"].ToString();
                           CategoryInfo.AppendChild(ModifiedByUserID);
                       }
                    }
                    else
                    {
                        XmlNode CategoryInfo = doc.CreateElement("CategoryInfo");
                        CategoryInfo.InnerText = "No Data";
                        DocRoot.AppendChild(CategoryInfo); 
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

