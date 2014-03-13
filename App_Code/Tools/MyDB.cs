using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
//using Microsoft.ApplicationBlocks.Data;

/// <summary>
/// Summary description for AppDB
/// </summary>
namespace MyDB
{
    public partial class DB
    {
        public SqlConnection conn;

        public DB()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public SqlConnection GetConnection()
        {
            //string strConnString = ConfigurationManager.ConnectionStrings["LocalSqlServer1"].ConnectionString;
            ////string strConnString = ConfigurationManager.AppSettings["LocalSqlServer1"];
            //conn = new SqlConnection(strConnString);
            //return conn;
            if (HttpContext.Current.Session["CompanyConnString"] == null)
            {
                HttpContext.Current.Session["CompanyConnString"] = "";
            }
            if (HttpContext.Current.Session["CompanyConnString"].ToString().Trim() == "")
            {
                string strConnString = ConfigurationManager.ConnectionStrings["Admin"].ConnectionString;
                string companyCode = "";
                if (HttpContext.Current.Session["CompanyCode"] != null)
                {
                    companyCode = Convert.ToString(HttpContext.Current.Session["CompanyCode"]);
                }
                string strCompanyConnString = string.Empty;
                using (SqlConnection con = new SqlConnection(strConnString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "Select  DBConnectionString from  Company where  Code='" + companyCode + "'";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    SqlDataReader oReader = cmd.ExecuteReader();
                    if (oReader.Read())
                    {
                        strCompanyConnString = oReader[0].ToString();
                    }
                    HttpContext.Current.Session["CompanyConnString"] = strCompanyConnString;
                }
            }
            conn = new SqlConnection(HttpContext.Current.Session["CompanyConnString"].ToString().Trim());
            return conn;

        }

        public SqlConnection GetAdminConnection()
        {
            string strConnString = ConfigurationManager.ConnectionStrings["Admin"].ConnectionString;
            //string strConnString = ConfigurationManager.AppSettings["LocalSqlServer1"];
            conn = new SqlConnection(strConnString);
            return conn;
        }

    }
}