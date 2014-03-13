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
public class getNoteInfo_bk_19_June_2013 : System.Web.Services.WebService {

    public getNoteInfo_bk_19_June_2013()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
        
    }

    [WebMethod]
    /*public string HelloWorld() {
        return "Hello World";
    }*/

    public XmlElement getNoteDetails(string param, string val)
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
            DocRoot = doc.CreateElement("NoteObjects");
            doc.AppendChild(DocRoot);

            ds = Fn.LoadNotes(dict, param, val);

            if (ds.Tables[0].Rows.Count > 0)
            {
               foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    XmlNode NoteInfo = doc.CreateElement("NoteInfo");
                    DocRoot.AppendChild(NoteInfo);

                    XmlNode NoteID = doc.CreateElement("NoteID");
                    NoteID.InnerText = dr["NoteID"].ToString();
                    NoteInfo.AppendChild(NoteID);

                    XmlNode Message = doc.CreateElement("Message");
                    Message.InnerText = dr["Message"].ToString();
                    NoteInfo.AppendChild(Message);

                   XmlNode Status = doc.CreateElement("Status");
                   Status.InnerText = dr["Status"].ToString();
                   NoteInfo.AppendChild(Status);

                   XmlNode CreateDate = doc.CreateElement("CreateDate");
                   CreateDate.InnerText = dr["CreatedOn"].ToString();
                   NoteInfo.AppendChild(CreateDate);

                   XmlNode CreatedByUserID = doc.CreateElement("CreatedByUserID");
                   CreatedByUserID.InnerText = dr["CreatedByUserID"].ToString();
                   NoteInfo.AppendChild(CreatedByUserID);

                   XmlNode ModifyDate = doc.CreateElement("ModifyDate");
                   ModifyDate.InnerText = dr["ModifiedOn"].ToString();
                   NoteInfo.AppendChild(ModifyDate);

                   XmlNode ModifiedByUserID = doc.CreateElement("ModifiedByUserID");
                   ModifiedByUserID.InnerText = dr["ModifiedByUserID"].ToString();
                   NoteInfo.AppendChild(ModifiedByUserID);
                }
            }
            else
            {
                XmlNode NoteInfo = doc.CreateElement("NoteInfo");
                NoteInfo.InnerText = "No Data";
                DocRoot.AppendChild(NoteInfo); 
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

