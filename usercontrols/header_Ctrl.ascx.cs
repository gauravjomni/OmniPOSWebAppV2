using System;
using System.Data;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Collections;
using MyTool;
using MyDB;
using MyQuery;
using Commons;


/// <summary>
/// Summary description for Login
/// </summary>
namespace UserControls
{
    partial class header_ctrl : System.Web.UI.UserControl
    {
        public MyToolSet iTool = new MyToolSet();
        public SQLQuery Qry = new SQLQuery();
        public Common Fn = new Common();
        DB mConnection = new DB();

        public header_ctrl()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        protected void Page_Load(object sender, System.EventArgs e)
        {
            string pagename="";
            bool flag =false;

            //if (Session["UserID"] == null)
            //{
            //    Response.Redirect("Default.aspx");
            //}

            using (SqlConnection conn = mConnection.GetConnection())
            {
                conn.Open();

                try
                {
                    LabelCompany.Text = Fn.GetCompanyInfo(conn);

                    Header_LoggedUserName.Text = Session["UserName"].ToString().ToUpper();

                    if (Session["R_ID"] != null && Session["R_ID"] != "")
                    {
                        Lbl_Selected_Restaurant.Text = "Location : " + Session["R_Name"].ToString();

                        if (Session["R_HeaderAddress"] != null && Session["R_HeaderAddress"] != "" && Session["R_HeaderABN"] != null && Session["R_HeaderABN"] != "")
                            LblRestInfo.Text = "Address : " + Session["R_HeaderAddress"] + "<br /> ABN No : " + Session["R_HeaderABN"];

                        pagename = iTool.GetPageName();

                        flag = Fn.CheckAssignedModuleForGroup(Session["UserGroupID"].ToString(), pagename.ToString(), "Child",conn);

                        if (flag == false)
                        {
                            Response.Redirect("Error.aspx");
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
            }

        }

        override protected void OnInit(EventArgs e)
        {
            this.Load += new System.EventHandler(this.Page_Load);
        }	

       
        protected void  LogOut_Click(object sender, EventArgs e)
        {
                //Session.Abandon();
                //Response.Redirect("Default.aspx");            
        }
    }
}

