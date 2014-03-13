using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace Logged_panel
{
    public partial class Home : System.Web.UI.Page
    {
        public Home()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("Default.aspx");
            }

            if (Request.Form["RestID"] != null && Request.Form["RestID"] != "")
            {
                Response.Write(Request.Form["RestID"]);

                /*Session["R_ID"] = rid.Value;
                Session["R_Initial"] = rinitial.Value;
                Session["R_Name"] = rname.Value;*/


            }

            if ((Session["R_ID"] !="" && Session["R_ID"]!=null) && (Session["R_Name"] != "" && Session["R_Name"]!=null))
            {
                string msg = string.Empty;
                msg = "<div class=\"notification attention png_bg\">";
                msg += "<div><h5>Selected Location : <span class=\"success\"><i>" + Session["R_Name"] + "</span></i></h5></div></div>";
                Notification.Text = msg;
                Notification.Visible = true;
            }
            else
            {
                Notification.Text = "";
                Notification.Visible = false;
            }
        }
    }
}