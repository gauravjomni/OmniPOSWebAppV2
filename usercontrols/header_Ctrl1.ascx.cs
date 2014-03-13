using System;
using System.Data;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using MyQuery;
using Commons;


/// <summary>
/// Summary description for Login
/// </summary>
namespace UserControls
{
    partial class header_ctrl : System.Web.UI.UserControl
    {

        public header_ctrl()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {

            SQLQuery Qry = new SQLQuery();
            Common Fn = new Common();
            //LabelCompany.Text = Fn.GetCompanyInfo();
            Header_LoggedUserName.Text = Session["UserName"].ToString();

            if (Session["R_ID"] != null && Session["R_ID"] != "")
                Lbl_Selected_Restaurant.Text = "Restaurant : " + Session["R_Name"].ToString();

        }
    }
}

