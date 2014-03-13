using System;
using System.Collections;
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
using Commons;
using MyTool;

namespace PosApp
{
    public partial class ViewCustomer : System.Web.UI.Page
    {
        DB mConnection = new DB();
        DataSet ds = new DataSet();
        Common Fn = new Common();
        protected MyToolSet iTool = new MyToolSet();

        public ViewCustomer()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string CustId = string.Empty;

            Fn.switchingbeteenlocation2company(true);

            try
            {
                using (SqlConnection conn = mConnection.GetConnection())
                {
                    conn.Open();

                    using (SqlTransaction trans = conn.BeginTransaction())
                    {
                        try
                        {
                            if (!IsPostBack)
                            {
                                if (Request.QueryString["mode"] != null && Request.QueryString["mode"] == "del")
                                {
                                    if (Request.QueryString["id"] != null && Request.QueryString["id"] != "")
                                    {
                                        CustId = iTool.decryptString(Request.QueryString["id"]);


                                        DateTime sDate = DateTime.Now;
                                        sDate = Fn.GetCommonDate(sDate, Session["DateFormat"]);

                                        SqlParameter[] ArParams = new SqlParameter[13];
                                        ArParams[0] = new SqlParameter("@FirstName", SqlDbType.VarChar, 50);
                                        ArParams[0].Value = "";

                                        ArParams[1] = new SqlParameter("@LastName", SqlDbType.VarChar, 50);
                                        ArParams[1].Value = "";

                                        ArParams[2] = new SqlParameter("@Email", SqlDbType.VarChar, 100);
                                        ArParams[2].Value = "";

                                        ArParams[3] = new SqlParameter("@Phone", SqlDbType.VarChar, 20);
                                        ArParams[3].Value = "";

                                        ArParams[4] = new SqlParameter("@Address1", SqlDbType.VarChar, 255);
                                        ArParams[4].Value = "";

                                        ArParams[5] = new SqlParameter("@Address2", SqlDbType.VarChar, 255);
                                        ArParams[5].Value = "";

                                        ArParams[6] = new SqlParameter("@ZipCode", SqlDbType.VarChar, 25);
                                        ArParams[6].Value = "";

                                        ArParams[7] = new SqlParameter("@Status", SqlDbType.Int);
                                        ArParams[7].Value = 0;

                                        ArParams[8] = new SqlParameter("@sDate", SqlDbType.DateTime);
                                        ArParams[8].Value = sDate;

                                        ArParams[9] = new SqlParameter("@LoggedUserID", SqlDbType.Int);
                                        ArParams[9].Value = 0;

                                        ArParams[10] = new SqlParameter("@RestID", SqlDbType.Int);
                                        ArParams[10].Value = 0;

                                        ArParams[11] = new SqlParameter("@Mode", SqlDbType.VarChar, 20);
                                        ArParams[11].Value = "del";

                                        ArParams[12] = new SqlParameter("@CustID", SqlDbType.Int);
                                        ArParams[12].Value = CustId;

                                        /*if (Fn.CheckRecordExists(null, "omni_Customers", "CustID", CustId))
                                            Msg.Visible = true;
                                        else
                                        {
                                            Msg.Visible = false;*/
                                            SqlHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, "SP_omni_Customer_Update", ArParams);
                                            trans.Commit();
                                        //}
                                    }
                                }
                            }

                            ds = Fn.LoadCustomer();
                            CustomerRepeater.DataSource = ds;
                            CustomerRepeater.DataBind();
                        }
                        catch (Exception ex)
                        {
                            trans.Rollback();
                            throw ex;
                        }

                        finally
                        {
                            conn.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            { }  

/*            mConnection = new DB();
            ds = Fn.LoadStates();
            UserGroupRepeater.DataSource = ds;
            UserGroupRepeater.DataBind(); */
        }

        protected void CustomerRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (CustomerRepeater.Items.Count < 1)
            {
                if (e.Item.ItemType == ListItemType.Footer)
                {
                    Label lblFooter = (Label)e.Item.FindControl("lblEmptyData");
                    lblFooter.Visible = true;
                }
            }
        }

    }
}