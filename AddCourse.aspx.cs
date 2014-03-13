using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.ApplicationBlocks.Data;
using MyDB;
using MyTool;
using Commons;

namespace PosCourse
{
    public partial class AddCourse : System.Web.UI.Page
    {
        DB mConnection = new DB() ;
        DataSet ds = new DataSet();
        Common Fn = new Common();
        MyToolSet iTool = new MyToolSet();
        string sQuery = "";


        public AddCourse()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["R_ID"] == "" || Session["R_ID"] == null)
                {
                    Session["bckurl"] = "Courses.aspx";
                    //Response.Redirect("Select_Restaurants.aspx?bckurl=" + Session["bckurl"]);
                    Server.Transfer("Notification.aspx");
                    return;
                }


                if (Request.QueryString["courseid"] != null)
                {
                    string courseid = "";
                    SqlParameter[] ArParams = new SqlParameter[4];

                    courseid = iTool.decryptString(Request.QueryString["courseid"]);

                    ArParams[0] = new SqlParameter("@CourseID", SqlDbType.Int);
                    ArParams[0].Value = courseid;

                    // @UserGroupName Output Parameter
                    ArParams[1] = new SqlParameter("@CourseName", SqlDbType.VarChar, 100);
                    ArParams[1].Direction = ParameterDirection.Output;

                    // @Status Output Parameter
                    ArParams[2] = new SqlParameter("@SortOrder", SqlDbType.Int);
                    ArParams[2].Direction = ParameterDirection.Output;

                    ArParams[3] = new SqlParameter("@Status", SqlDbType.Int);
                    ArParams[3].Direction = ParameterDirection.Output;

                    try
                    {
                        // Call ExecuteNonQuery static method of SqlHelper class
                        // We pass in database connection string, command type, stored procedure name and an array of SqlParameter objects
                        SqlHelper.ExecuteNonQuery(mConnection.GetConnection(), CommandType.StoredProcedure, "getCourseDetails", ArParams);

                        // Display results in text box using the values of output parameters	
                        txtcourseName.Value = ArParams[1].Value.ToString();
                        TxtOrder.Value = ArParams[2].Value.ToString();
                        Status.Checked = ArParams[3].Value.ToString() == "1" ? true : false;
                        //Mode.Value = "edit";
                        //CourseID.Value = courseid;

                        if (Request.QueryString["mode"] != null)
                        {
                            if (Request.QueryString["mode"] == "edit")
                                Mode.Value = "edit";
                            else if (Request.QueryString["mode"] == "clone")
                                Mode.Value = "clone";
                        }

                        //display name on top
                        string itemType = "Course";
                        LblHead.Text = char.ToUpper(Mode.Value[0]) + Mode.Value.Substring(1) + " " + itemType + " [ " + ArParams[1].Value.ToString() + " ]";

                        if (Mode.Value == "add" || Mode.Value == "clone")
                            CourseID.Value = "-1";
                        else
                            CourseID.Value = courseid;

                    }
                    catch (Exception ex)
                    {
                        // throw an exception
                        throw ex;
                    }

                }

                //if (Mode.Value == "add" || Mode.Value == "clone")
                if (Mode.Value == "add")
                    TxtOrder.Value = System.Configuration.ConfigurationSettings.AppSettings["DefaultSortOrder"];

            }
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string strCourseName = iTool.formatInputString(txtcourseName.Value);
                string strSorterOrder = iTool.formatInputString(TxtOrder.Value);

                bool flag = false;

                DateTime sDate = DateTime.Now;
                sDate = Fn.GetCommonDate(sDate, Session["DateFormat"]);

                SqlParameter[] ArParams = new SqlParameter[8];
                ArParams[0] = new SqlParameter("@CourseName", SqlDbType.VarChar, 100);
                ArParams[0].Value = strCourseName;

                ArParams[1] = new SqlParameter("@SortOrder", SqlDbType.Int);
                ArParams[1].Value = strSorterOrder;

                ArParams[2] = new SqlParameter("@Status", SqlDbType.Int);
                ArParams[2].Value = Status.Checked ? 1 : 0;

                ArParams[3] = new SqlParameter("@sDate", SqlDbType.DateTime);
                ArParams[3].Value = sDate;

                ArParams[4] = new SqlParameter("@Rest_ID", SqlDbType.Int);
                ArParams[4].Value = Convert.ToInt32(Session["R_ID"]) ;

                ArParams[5] = new SqlParameter("@LoggedUserID", SqlDbType.Int);
                ArParams[5].Value =  Convert.ToInt32(Session["UserID"]);

                ArParams[6] = new SqlParameter("@Mode", SqlDbType.VarChar,20);
                ArParams[6].Value = Mode.Value;

                ArParams[7] = new SqlParameter("@CourseID", SqlDbType.Int);
                ArParams[7].Value = CourseID.Value;

                Dictionary<string, string> dict;

                if (CourseID.Value == "-1" && (Mode.Value=="add" || Mode.Value == "clone"))
                {
                    dict = null;
                    flag = Fn.CheckRecordExists(dict, "omni_Courses", "CourseName", "CourseName", "", "", "", ArParams);
                }
                else
                {
                    //========= FOR IMPLEMENTING MORE "AND" CLAUSE IN SQL OPERATION ================//

                    //dict = new Dictionary<string, string>() { { "UserGroupId", GroupID.Value } };
                    //flag = Fn.CheckRecordExists(dict, "omni_user_group", "UserGroupName", "UserGroupName", "edit", "UserGroupId", GroupID.Value, ArParams);

                    //=============================================================================//

                    flag = Fn.CheckRecordExists(null, "omni_Courses", "CourseName", "CourseName", "edit", "CourseId", CourseID.Value, ArParams);
                }

                if (flag == true)
                {
                    LblCourse.Text = "Course Name already exist. Try with another one.";
                    return;
                }

                using (SqlConnection conn = mConnection.GetConnection())
                {
                    conn.Open();

                    using (SqlTransaction trans = conn.BeginTransaction())
                    {
                        // Establish command parameters
                        // @AccountNo (From Account)
                        //SqlParameter paramFromAcc = new SqlParameter("@usrgrpname", SqlDbType.VarChar,50);
                        //paramFromAcc.Value = "12345";

                        try
                        {
                            // Call ExecuteNonQuery static method of SqlHelper class for debit and credit operations.
                            // We pass in SqlTransaction object, command type, stored procedure name, and a comma delimited list of SqlParameters
                            SqlHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, "SP_omni_Course_Update", ArParams);
                            trans.Commit();
                            //txtResults.Text = "Transfer Completed";
                        }
                        catch (Exception ex)
                        {
                            // throw exception						
                            trans.Rollback();
                            //txtResults.Text = "Transfer Error";
                            throw ex;
                        }

                        finally
                        {
                            conn.Close();
                        }
                    }
                }



                //SqlHelper.ExecuteNonQuery(mConnection.GetConnection(), CommandType.Text, sQuery, ArParams);

            }
            catch(Exception ex)
            {

           // CreateLogFiles.ErrorLog(ex.Message.ToString() & " - " & System.IO.Path.GetFileName(System.Web.HttpContext.Current.Request.Url.AbsolutePath) & " - btnCreate_Click")
           // lblError.Text = "Error - Please contact Administrator "
           // Exit Sub
            }
                Response.Redirect("Courses.aspx");
        }
    }
}