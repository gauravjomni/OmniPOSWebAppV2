using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using MailTools;
using System.Xml;

/// <summary>
/// Summary description for CompanyUtilityServices
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class CompanyUtilityServices : System.Web.Services.WebService {

    public CompanyUtilityServices () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public XmlElement sendEMailWithBody(string mailBody, string subject, string toReceipent)
    {
        EmailManager sm = new EmailManager(toReceipent, mailBody, subject);

        bool result = sm.sendMail();

        XmlDocument doc = new XmlDocument();
        XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", null, null);
        doc.AppendChild(dec);
        XmlElement DocRoot = null;
        DocRoot = doc.CreateElement("Result");
        doc.AppendChild(DocRoot);

        string isSuccess = (result ? "true" : "false");

        DocRoot.AppendChild(XMLNodeCreator.xmlNodeForElement("IsSuccess", isSuccess, doc));
        return DocRoot;
    }

    [WebMethod]
    public XmlElement sendNoSaleNotificationByEmailID(string emailId, string locationName, string companyName)
    {
        string pSubject = "No Sale Notification from " + locationName;

        string mBody = "";
        mBody = "<div style=\"FONT-FAMILY:Arial; \">";

        mBody += "<u>No Sale Notification</u><br/><br/>";

        mBody += "<b>Date:</b>&nbsp;" + DateTime.Now.ToShortDateString() + "<br/>";

        mBody += "<b>Location:</b>&nbsp;" + locationName + " ( " + companyName + " )" + "<br/>";

        mBody += "<br>This is to notify that the No Sale counter has been reached.<br>Please take your necessary action.</b>";

        mBody += "<br><br>Thank you for using OmniPOS.<br/><br/>";
        mBody += "<b>Regards,</b><br/>";
        mBody += "<b>Omni Systems (Admin)</b><br/></div>";

        return this.sendEMailWithBody(mBody, pSubject, emailId);
    }
    
}
