using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Xml.Linq;
using MyDB;
using MyQuery;
using System.Web.UI.DataVisualization;
using MyTool;
using Commons;
using System.Drawing;
using System.Web.UI.DataVisualization.Charting;

public partial class UserAttendancePage : System.Web.UI.Page
{
    DB mConnection = new DB();
    Common Fn = new Common();
    MyToolSet iTool = new MyToolSet();
    SQLQuery Qry = new SQLQuery();
    DataSet ds = new DataSet();
    public string strCurrency = string.Empty;
    public string CurrDateTime = String.Format("{0:dd-MM-yyyy hh:mm:ss}", DateTime.Now);

    public string fromdate = string.Empty;
    public string tilldate = string.Empty;

    public string fromdatenew = string.Empty;
    public string tilldatenew = string.Empty;

    public string fromdater = string.Empty;
    public string tilldater = string.Empty;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Currency"] != null && Session["Currency"] != "")
            strCurrency = Session["Currency"].ToString();

        // Fn.switchingbeteenlocation2company(true);

        if (!IsPostBack)
        {
            fromdate = String.Format("{0:MM/dd/yyyy}", DateTime.Now);
            tilldate = String.Format("{0:MM/dd/yyyy}", DateTime.Now);
            fromdatenew = String.Format("{0:MM/dd/yyyy}", DateTime.Now);
            tilldatenew = String.Format("{0:MM/dd/yyyy}", DateTime.Now);
            fromdater = fromdate;
            tilldater = tilldate;
        }
        else
        {
            fromdater = Request.Form["txtFromDate"];
            tilldater = Request.Form["txtTillDate"];
        }

        Common com = new Common();
        Dictionary<string, string> headerFooter = com.getHeaderAndFooter(Session["R_ID"].ToString());
        string Header_Name = headerFooter["Header_Name"];
        string location_Name = Session["R_Name"].ToString();
    /*    string Header_Address1 = headerFooter["Header_Address1"];
        string Header_City = headerFooter["Header_City"];

        string Header_State = headerFooter["Header_State"];
        string Header_Zip = headerFooter["Header_Zip"];
        string Header_Email = headerFooter["Header_Email"];

        string Header_ABN = headerFooter["Header_ABN"];
        string Footer1 = headerFooter["Footer1"];
        string Footer2 = headerFooter["Footer2"];
       */
        // Displaying the company details under Sales(Chart) category
        if (Header_Name != null && Header_Name != "")
        {
            company.Text = Header_Name;
            company.Visible = true;

        }


        if (location_Name != null && location_Name != "")
        {
            location.Text = location_Name;
            location.Visible = true;

        }

 /*       if (Header_City != null && Header_City != "")
        {
            City.Text = Header_City;
            City.Visible = true;
        }
        if (Header_State != null && Header_State != "")
        {
            State.Text = Header_State;
            State.Visible = true;
        }
        if (Header_Zip != null && Header_Zip != "")
        {
            Zip.Text = Header_Zip;
            Zip.Visible = true;
        }
        if (Header_Email != null && Header_Email != "")
        {
            email.Text = Header_Email;
            email.Visible = true;
        }
        if (Header_ABN != null && Header_ABN != "")
        {
            Abn.Text = Header_ABN;
            Abn.Visible = true;
        }
        */

        BtnPrint.Attributes.Add("onclick", "PrintDoc()");
 }



    protected void BtnSave_Click(object sender, EventArgs e)
    {
        fromdate = iTool.formatInputString(Request.Form["txtFromDate"]);
        tilldate = iTool.formatInputString(Request.Form["txtTillDate"]);

        if (fromdate != "" && Fn.ValidateDate(fromdate))
        {
            fromdate = String.Format("{0:yyyy-MM-dd}", Fn.ConvertDateIntoAnotherFormat2(fromdate));
            fromdatenew = String.Format("{0:dd-MM-yyyy}", Fn.ConvertDateIntoAnotherFormat3(fromdate));
            fromdater = String.Format("{0:MM/dd/yyyy}", iTool.formatInputString(Request.Form["txtFromDate"]));
        }
        if (tilldate != "" && Fn.ValidateDate(tilldate))
        {
            tilldate = String.Format("{0:yyyy-MM-dd}", Fn.ConvertDateIntoAnotherFormat2(tilldate));
            tilldatenew = String.Format("{0:dd-MM-yyyy}", Fn.ConvertDateIntoAnotherFormat3(tilldate));
            tilldater = String.Format("{0:MM/dd/yyyy}", iTool.formatInputString(Request.Form["txtTillDate"]));
        }
        if (fromdate == tilldate)
        {
            DateTime dt = DateTime.Parse(tilldate).AddDays(1);
            tilldate = String.Format("{0:yyyy-MM-dd}", dt);
        }

        LblRepo.InnerText = "Date Selected From " + String.Format("{0:dd MMM yyyy}", fromdatenew) + " To " + String.Format("{0:dd,MMM yyyy}", tilldatenew);

        this.getUserAttendanceReport();

        z_report.Visible = true;
    }

    private void getUserAttendanceReport()
    {
        Common com = new Common();

        Dictionary<string, string> usersDict = com.getAllUserIdAndName_ForRestID(Session["R_ID"].ToString());
        OPSaleFinder saleFinder = new OPSaleFinder();
       
        try
        {
            using (SqlConnection conn = mConnection.GetConnection())
            {
                try
                {
                    conn.Open();

                    foreach (string userId in usersDict.Keys)
                    {
                        string userName;
                        string loginTime = string.Empty, logoutTime = string.Empty, duration = string.Empty;
                        SqlDataReader sqlDataReader = null;

                        usersDict.TryGetValue(userId, out userName);
                        sqlDataReader = saleFinder.getUserAttendanceReport(fromdate, tilldate, Session["R_ID"].ToString(), userId, conn);

                        this.ShowUserAttendenceDetails(userName, sqlDataReader);

                        /*
                        if (sqlDataReader.HasRows)
                        {
                            while (sqlDataReader.Read())
                            {
                                loginTime = (string)sqlDataReader["LoginTime"].ToString();
                                logoutTime = (string)sqlDataReader["LogoutTime"].ToString();
                                duration = (string)sqlDataReader["Duration"].ToString();

                                //this.ShowUserAttendenceDetails(userName, loginTime, logoutTime, duration);
                            }
                        }

                        sqlDataReader.Close();*/
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

        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void ShowUserAttendenceDetails(string userName, SqlDataReader sqlDataReader)
    {
        TableCell tc9 = new TableCell();
        TableCell tc10 = new TableCell();
        TableCell tc11 = new TableCell();
        TableCell tc12 = new TableCell();
        TableRow tr4 = new TableRow();
        tr4.BackColor = Color.Gray;
        tr4.ForeColor = Color.White;
        tc9.Text = "User Name";
        tc10.Text = Convert.ToString(userName);
        tc9.Font.Bold = true;
        tc10.Font.Bold = true;
        tr4.Controls.Add(tc9);
        tr4.Controls.Add(tc10);
        tr4.Controls.Add(tc11);
        tr4.Controls.Add(tc12);

        TableCell tc5 = new TableCell();
        TableCell tc6 = new TableCell();
        TableCell tc7 = new TableCell();
        TableCell tc8 = new TableCell();

        string loginTime = string.Empty, logoutTime = string.Empty, duration = string.Empty;
        Int32 durationnew = 0,min=0,sec=0;

       salesbylocationtable.Controls.Add(tr4);


        if (sqlDataReader.HasRows)
        {
            while (sqlDataReader.Read())
            {
                TableRow tr3 = new TableRow();

                if (sqlDataReader["LoginTime"].ToString() != null && sqlDataReader["LoginTime"].ToString() != "")
                { loginTime = (string)sqlDataReader["LoginTime"].ToString(); }
                if (sqlDataReader["LogoutTime"].ToString() != null && sqlDataReader["LogoutTime"].ToString() != "")
                { logoutTime = (string)sqlDataReader["LogoutTime"].ToString(); }
                if (sqlDataReader["Duration"].ToString() != null && sqlDataReader["Duration"].ToString() != "")
                { duration = (string)sqlDataReader["Duration"].ToString(); }

                
                durationnew = Convert.ToInt32(duration)/3600;
                duration = (Convert.ToInt32(duration) % 3600).ToString();
                min = Convert.ToInt32(duration) / 60;
                duration = (Convert.ToInt32(duration) % 60).ToString();
                sec = Convert.ToInt32(duration);
                
                TableCell tc_login = new TableCell();
                TableCell tc_logout = new TableCell();
                TableCell tc_dur = new TableCell();
                TableCell tc_rate = new TableCell();

                tc_login.Text = loginTime;
                tc_logout.Text = logoutTime;
                tc_dur.Text = durationnew.ToString() + "hrs " + min.ToString() + "mins " + sec.ToString() + "sec";
                tc_rate.Text = "$0";

                tr3.Controls.Add(tc_login);
                tr3.Controls.Add(tc_logout);
                tr3.Controls.Add(tc_dur);
                tr3.Controls.Add(tc_rate);

                salesbylocationtable.Controls.Add(tr3);
            }
        }

        sqlDataReader.Close();
    }

}