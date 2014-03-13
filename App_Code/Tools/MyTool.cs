using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for Class1
/// </summary>
namespace MyTool
{
    public partial class MyToolSet
    {
        public string encodingString = "ASDSERR~__~";

        public MyToolSet()
        {

        }
        public string formatDate(string input)
        {
            DateTime dd = Convert.ToDateTime(input);
            return dd.ToString("dd/MM/yyyy");
        }

        public string formatDateintoTwelveHour(string input)
        {
            DateTime dd = Convert.ToDateTime(input);
            string hh = "RIGHT(CONVERT(VARCHAR,dd,100), 7)";
            return hh;
        }

        public string formatDate_MDY(string input)
        {
            DateTime dd = Convert.ToDateTime(input);
            return dd.ToString("MM/dd/yyyy");
        }

        public string formatInputString(string input)
        {
            return input.Trim().Replace("'", "''").ToString();
        }

        public string formatInputString_New(string input)
        {
            return input.Replace("'", "''").ToString();
        }


        public string encryptString(string input)
        {
            string s1 = encodingString + input;
            return base64Encode(s1);
        }

        public string decryptString(string input)
        {
            string s1 = base64Decode(input);
            return s1.Replace(encodingString, "");
            //return base64Encode(s1);
        }


        public string base64Encode(string data)
        {
            try
            {
                byte[] encData_byte = new byte[data.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(data);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception e)
            {
                throw new Exception("Error in base64Encode" + e.Message);
            }
        }

        public string base64Decode(string data)
        {
            try
            {
                System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
                System.Text.Decoder utf8Decode = encoder.GetDecoder();

                byte[] todecode_byte = Convert.FromBase64String(data);
                int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
                char[] decoded_char = new char[charCount];
                utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
                string result = new String(decoded_char);
                return result;
            }
            catch (Exception e)
            {
                throw new Exception("Error in base64Decode" + e.Message);
            }
        }

        public Control FindControlRecursive(Control Root, string Id)
        {
            if (Root.ID == Id)
                return Root;
            foreach (Control Ctl in Root.Controls)
            {
                Control FoundCtl = FindControlRecursive(Ctl, Id);
                if (FoundCtl != null)
                    return FoundCtl;
            }
            return null;
        }

        public string GetPageName()
        {
            string sPagePath = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
            System.IO.FileInfo oFileInfo = new System.IO.FileInfo(sPagePath);
            string sPageName = oFileInfo.Name.Trim();
            return sPageName;
        }

        public static string get12HourTimeFormat(int time)
        {
            string result = "";
            string p = "AM";
            if (time > 12)
            {
                time = time - 12;
                p = "PM";
            }

            result = time + " " + p;

            return result;
        }

    }

}