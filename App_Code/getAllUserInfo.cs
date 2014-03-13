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
public class getAllUserInfo : System.Web.Services.WebService {

    public getAllUserInfo () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
        
    }

    [WebMethod]
    /*public string HelloWorld() {
        return "Hello World";
    }*/

    public XmlElement getAllUserDetails(string param, string val)
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
            DocRoot = doc.CreateElement("UserObjects");
            doc.AppendChild(DocRoot);

            using (SqlConnection conn = mConnection.GetConnection())
            {
                conn.Open();

                try
                {
            		ds = Fn.LoadAllUserInfoSQL_WebService(dict, param, val,conn);

		            if (ds.Tables[0].Rows.Count > 0)
        		    {
                       foreach (DataRow dr in ds.Tables[0].Rows)
                       {
                           XmlNode UserInfo = doc.CreateElement("UserInfo");
                           DocRoot.AppendChild(UserInfo);

                           XmlNode UserID = doc.CreateElement("UserID");
                           UserID.InnerText= dr["UserID"].ToString();
                           UserInfo.AppendChild(UserID);

                           XmlNode UserName = doc.CreateElement("UserName");
                           UserName.InnerText = dr["UserName"].ToString();
                           UserInfo.AppendChild(UserName);

                           XmlNode UserPin = doc.CreateElement("UserPin");
                           UserPin.InnerText = dr["UserPin"].ToString();
                           UserInfo.AppendChild(UserPin);

                           XmlNode UserEmail = doc.CreateElement("UserEmail");
                           UserEmail.InnerText = dr["UserEmail"].ToString();
                           UserInfo.AppendChild(UserEmail);

                           XmlNode UserGroupID = doc.CreateElement("UserGroupID");
                           UserGroupID.InnerText = dr["UserGroupID"].ToString();
                           UserInfo.AppendChild(UserGroupID);

                           XmlNode IsActive = doc.CreateElement("IsActive");
                           IsActive.InnerText = dr["Status"].ToString();
                           UserInfo.AppendChild(IsActive);

                           XmlNode FirstName = doc.CreateElement("FirstName");
                           FirstName.InnerText = dr["FirstName"].ToString();
                           UserInfo.AppendChild(FirstName);

                           XmlNode LastName = doc.CreateElement("LastName");
                           LastName.InnerText = dr["LastName"].ToString();
                           UserInfo.AppendChild(LastName);

                           XmlNode UserAlias = doc.CreateElement("UserAlias");
                           UserAlias.InnerText = dr["UserAlias"].ToString();
                           UserInfo.AppendChild(UserAlias);

                           XmlNode UserPHone = doc.CreateElement("UserPHone");
                           UserPHone.InnerText = dr["UserPHone"].ToString();
                           UserInfo.AppendChild(UserPHone);

                           XmlNode StartDate = doc.CreateElement("StartDate");
                           StartDate.InnerText = dr["StartDate"].ToString();
                           UserInfo.AppendChild(StartDate);

                           XmlNode CreateDate = doc.CreateElement("CreateDate");
                           CreateDate.InnerText = dr["CreateDate"].ToString();
                           UserInfo.AppendChild(CreateDate);

                           XmlNode CreatedByUserID = doc.CreateElement("CreatedByUserID");
                           CreatedByUserID.InnerText = dr["CreatedByUserID"].ToString();
                           UserInfo.AppendChild(CreatedByUserID);

                           XmlNode ModifyDate = doc.CreateElement("ModifyDate");
                           ModifyDate.InnerText = dr["ModifyDate"].ToString();
                           UserInfo.AppendChild(ModifyDate);

                           XmlNode ModifiedByUserID = doc.CreateElement("ModifiedByUserID");
                           ModifiedByUserID.InnerText = dr["ModifiedByUserID"].ToString();
                           UserInfo.AppendChild(ModifiedByUserID);
                        }
                    }
		            else
        		    {
                        XmlNode UserInfo = doc.CreateElement("UserInfo");
                        UserInfo.InnerText = "No Data";                
                        DocRoot.AppendChild(UserInfo); 
                    }
		           
				}
				catch(Exception ex)
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

