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

namespace PosState
{
    public partial class States : System.Web.UI.Page
    {
        DB mConnection = new DB();
        DataSet ds = new DataSet();
        Common Fn = new Common();
        protected MyToolSet iTool = new MyToolSet();

        public States()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string Stateid = string.Empty;

            Fn.switchingbeteenlocation2company(true);

            using (SqlConnection conn = mConnection.GetConnection())
            {
                conn.Open();

                try
                {
					if (!IsPostBack)
					{
						if (Request.QueryString["mode"] != null && Request.QueryString["mode"] == "del")
						{
							if (Request.QueryString["id"] != null && Request.QueryString["id"] != "")
							{
								Stateid = iTool.decryptString(Request.QueryString["id"]);

								DateTime sDate = DateTime.Now;
								sDate = Fn.GetCommonDate(sDate, Session["DateFormat"]);

								SqlParameter[] ArParams = new SqlParameter[4];
								ArParams[0] = new SqlParameter("@StateName", SqlDbType.VarChar, 50);
								ArParams[0].Value = "";

								ArParams[1] = new SqlParameter("@Status", SqlDbType.Int);
								ArParams[1].Value = 0;

								ArParams[2] = new SqlParameter("@Mode", SqlDbType.VarChar, 20);
								ArParams[2].Value = "del";

								ArParams[3] = new SqlParameter("@StateID", SqlDbType.Int);
								ArParams[3].Value = Stateid;

								if (Fn.CheckRecordExists(null, "omni_Restuarnt_info", "StateID", Stateid, conn))
									Msg.Visible = true;
								else
								{
									Msg.Visible = false;
									
									using (SqlTransaction trans = conn.BeginTransaction())
				                    {
										try
										{
											SqlHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, "SP_omni_State_Update", ArParams);
											trans.Commit();														
										}
										catch (Exception ex)
										{
											trans.Rollback();
											throw ex;
										}
		                    		}
								}
							}
						}
					}

                    ds = Fn.LoadStates(conn);
                    UserGroupRepeater.DataSource = ds;
                    UserGroupRepeater.DataBind();

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

    }
}