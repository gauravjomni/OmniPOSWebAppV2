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
using Commons;
using MyTool;

namespace PosDevice
{
    public partial class PosDevices : System.Web.UI.Page
    {
        DB mConnection = new DB();
        DataSet ds = new DataSet();
        Common Fn = new Common();
        protected MyToolSet iTool = new MyToolSet();

        public PosDevices()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            /*mConnection = new DB();
            ds = Fn.LoadDeviceInfo(null, "Rest_ID", Session["R_ID"].ToString());
            DeviceInfoRepeater.DataSource = ds;
            DeviceInfoRepeater.DataBind();*/

            string deviceid = string.Empty;

            if (Session["R_ID"] == "" || Session["R_ID"] == null)
            {
                Session["bckurl"] = "PosDevices.aspx";
                Server.Transfer("Notification.aspx");
                return;
            }

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
                                        deviceid = iTool.decryptString(Request.QueryString["id"]);

                                        Dictionary<string, string> dict;
                                        dict = null;

                                        DateTime sDate = DateTime.Now;
                                        sDate = Fn.GetCommonDate(sDate, Session["DateFormat"]);

                                        SqlParameter[] ArParams = new SqlParameter[8];
                                        ArParams[0] = new SqlParameter("@DeviceName", SqlDbType.VarChar, 100);
                                        ArParams[0].Value = "";

                                        ArParams[1] = new SqlParameter("@PrinterID", SqlDbType.Int);
                                        ArParams[1].Value = 0;

                                        ArParams[2] = new SqlParameter("@Rest_ID", SqlDbType.Int);
                                        ArParams[2].Value = Session["R_ID"];

                                        ArParams[3] = new SqlParameter("@LoggedUserID", SqlDbType.Int);
                                        ArParams[3].Value = Convert.ToInt32(Session["UserID"]);

                                        ArParams[4] = new SqlParameter("@sDate", SqlDbType.DateTime);
                                        ArParams[4].Value = sDate;

                                        ArParams[5] = new SqlParameter("@Mode", SqlDbType.VarChar, 20);
                                        ArParams[5].Value = "del";

                                        ArParams[6] = new SqlParameter("@DeviceID", SqlDbType.Int);
                                        ArParams[6].Value = deviceid;

                                        ArParams[7] = new SqlParameter("@Status", SqlDbType.Int);
                                        ArParams[7].Value = 0;

                                        SqlHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, "SP_omni_DeviceInfo_Update", ArParams);

                                        trans.Commit();
                                    }
                                }
                            }

                                ds = Fn.LoadDeviceInfo(null, "Rest_ID", Session["R_ID"].ToString());
                                DeviceInfoRepeater.DataSource = ds;
                                DeviceInfoRepeater.DataBind();

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
            
        }

        protected void DeviceInfoRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (DeviceInfoRepeater.Items.Count < 1)
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