using System;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading;
using System.ComponentModel;

/// <summary>
/// Summary description for EmailManager
/// </summary>
/// 
namespace MailTools
{
    public class EmailManager
    {
        string _mailTo, _mailFrom, _mailBody, _mailPassword, _mailSubject;
       
        public EmailManager(String inMailTo, String inMailBody, String subject)
        {
            this._mailBody = inMailBody;
            this._mailTo = inMailTo;
            this._mailSubject = subject;

            this._mailFrom = System.Configuration.ConfigurationManager.AppSettings["MailFrom_User"].ToString();
            this._mailPassword = System.Configuration.ConfigurationManager.AppSettings["MailFrom_pwd"].ToString();
         }

        public void sendEmail() //this code is not working
        {
            /*
            MailMessage mail = new MailMessage();
            mail.From = new System.Net.Mail.MailAddress(this._mailFrom);
            string smtphost = System.Configuration.ConfigurationManager.AppSettings["SMTPSetting"].ToString();

            // The important part -- configuring the SMTP client
            SmtpClient smtp = new SmtpClient();
            smtp.Port = 25;   
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network; 
            smtp.UseDefaultCredentials = false; 
            smtp.Credentials = new NetworkCredential(this._mailFrom, this._mailPassword);

            string host = System.Configuration.ConfigurationManager.AppSettings["SMTPSetting"].ToString() + ":25";

            smtp.Host = host; //"smtp.gmail.com";

            //recipient address
            mail.To.Add(new MailAddress(this._mailTo));

            //Formatted mail body
            mail.IsBodyHtml = true;

            mail.Subject = this._mailSubject;
            mail.Body = this._mailBody;
            smtp.Send(mail);
             * */
        }

        public bool sendMail()
        {
            string mailFrom = System.Configuration.ConfigurationManager.AppSettings["MailFrom_User"].ToString();
            string mailFromPWD = System.Configuration.ConfigurationManager.AppSettings["MailFrom_pwd"].ToString();

            System.Web.Mail.MailFormat pFormat;
            pFormat = System.Web.Mail.MailFormat.Html;

            System.Web.Mail.MailMessage mailObj = new System.Web.Mail.MailMessage();

            string smtphost = System.Configuration.ConfigurationManager.AppSettings["SMTPSetting"].ToString();
            mailObj.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserver", smtphost);
            mailObj.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserverport", "25");
            mailObj.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusing", "2");
            mailObj.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", "1");
            //Use 0 for anonymous
            mailObj.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", mailFrom);
            mailObj.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", mailFromPWD);
            // mailObj.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpusessl", "true");
            mailObj.BodyFormat = pFormat;
            mailObj.Subject = this._mailSubject;
            mailObj.Body = this._mailBody;
            mailObj.To = this._mailTo;
            mailObj.From = mailFrom;

            string host = System.Configuration.ConfigurationManager.AppSettings["SMTPSetting"].ToString() + ":25";

            bool result = false;
            System.Web.Mail.SmtpMail.SmtpServer = host;
            try
            {
                System.Web.Mail.SmtpMail.Send(mailObj);
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
            }

            return result;
        }
    }
}