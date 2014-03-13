using System;
using System.Data;

/// <summary>
/// Summary description for Login
/// </summary>
namespace Signout
{
    partial class Default : System.Web.UI.Page
    {
        public Default()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        protected void Page_Load(object sender, System.EventArgs e) 
        {
            Session["Insuser_id"] = "";
            Session["Insuser_name"] = "";
            Session.Abandon();
            Session.Clear();
            //Response.Redirect("Default.aspx",false);
        }
    }
}