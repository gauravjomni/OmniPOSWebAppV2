using System;
using System.Web.UI;

public partial class Admin_Dashboard : System.Web.UI.Page
{
    #region Page Variables
    protected string PathName = Generic.GetApplicationPath();
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.ClientScript.RegisterStartupScript(GetType(), "S1",
        "<script>alert('1111');AddDatePicker('#txtFromDate');AddDatePicker('#txtToDate');</script>");

    }
}