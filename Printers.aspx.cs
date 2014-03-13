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

namespace PosPrinters
{
    public partial class PrinterList : System.Web.UI.Page
    {
        DB mConnection = new DB();
        DataSet ds = new DataSet();
        Common Fn = new Common();
        protected MyToolSet iTool = new MyToolSet();

        public PrinterList()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string Printid = string.Empty;
            /*   mConnection = new DB();
               ds = Fn.LoadPrinters(null, "Rest_ID", Session["R_ID"].ToString());
               PrinterRepeater.DataSource = ds;
               PrinterRepeater.DataBind(); */


            if (Session["R_ID"] == "" || Session["R_ID"] == null)
            {
                Session["bckurl"] = "Printers.aspx";
                //Response.Redirect("Select_Restaurants.aspx?bckurl=" + Session["bckurl"]);
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
                                        Printid = iTool.decryptString(Request.QueryString["id"]);
                                        Dictionary<string, string> dict;

                                        DateTime sDate = DateTime.Now;
                                        sDate = Fn.GetCommonDate(sDate, Session["DateFormat"]);

                                        SqlParameter[] ArParams = new SqlParameter[13];
                                        ArParams[0] = new SqlParameter("@PrinterName", SqlDbType.VarChar, 50);
                                        ArParams[0].Value = "";

                                        ArParams[1] = new SqlParameter("@IPAddress", SqlDbType.VarChar, 20);
                                        ArParams[1].Value = "";

                                        ArParams[2] = new SqlParameter("@PosOrItem", SqlDbType.Char, 1);
                                        ArParams[2].Value = '0';

                                        ArParams[3] = new SqlParameter("@IsPrintIPAddress", SqlDbType.Char, 1);
                                        ArParams[3].Value = '0';

                                        ArParams[4] = new SqlParameter("@Status", SqlDbType.Int);
                                        ArParams[4].Value = 0;

                                        ArParams[5] = new SqlParameter("@sDate", SqlDbType.DateTime);
                                        ArParams[5].Value = sDate;

                                        ArParams[6] = new SqlParameter("@Rest_ID", SqlDbType.Int);
                                        ArParams[6].Value = Session["R_ID"];

                                        ArParams[7] = new SqlParameter("@LoggedUserID", SqlDbType.Int);
                                        ArParams[7].Value = Session["UserID"];

                                        ArParams[8] = new SqlParameter("@Mode", SqlDbType.VarChar, 20);
                                        ArParams[8].Value = "del";

                                        ArParams[9] = new SqlParameter("@PrinterID", SqlDbType.Int);
                                        ArParams[9].Value = Printid;

                                        ArParams[10] = new SqlParameter("@PrinterType", SqlDbType.Char);
                                        ArParams[10].Value = '1';

                                        ArParams[11] = new SqlParameter("@NoOfCopies", SqlDbType.Int);
                                        ArParams[11].Value = 0;

                                        ArParams[12] = new SqlParameter("@Trigger_Cash_Drawer", SqlDbType.TinyInt);
                                        ArParams[12].Value =  0;

                                        dict = new Dictionary<string, string>() { { "Rest_ID", Session["R_ID"].ToString() } };

                                        if (Fn.CheckRecordExists(null, "omni_Device", "PrinterID", Printid))
                                            Msg.Visible = true;
                                        else
                                        {
                                            dict = new Dictionary<string, string>() { { "OptionType", "P" } };

                                            if (Fn.CheckRecordExists(dict, "omni_Product_Kitchen_Printer_Options", "OptionID", Printid))
                                                Msg.Visible = true;
                                            else
                                            {
                                                Msg.Visible = false;
                                                SqlHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, "SP_omni_Printer_Update", ArParams);
                                                trans.Commit();
                                            }
                                        }
                                    }
                                }
                            }

                            ds = Fn.LoadPrinters(null, "Rest_ID", Session["R_ID"].ToString());
                            PrinterRepeater.DataSource = ds;
                            PrinterRepeater.DataBind(); 
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

        protected void PrinterRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (PrinterRepeater.Items.Count < 1)
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