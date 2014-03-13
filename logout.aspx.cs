using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class _BackEnd_logout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["Insuser_id"] = "";
        Session["Insuser_name"] = "";
        Session.Abandon();
        //Response.Redirect("Default.aspx",false);
        Server.Transfer("Default.aspx");
    }
}
