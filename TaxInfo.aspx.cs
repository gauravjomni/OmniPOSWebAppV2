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

namespace PosTaxes
{
    public partial class TaxInfo : System.Web.UI.Page
    {
        DB mConnection = new DB();
        DataSet ds = new DataSet();
        Common Fn = new Common();
        protected MyToolSet iTool = new MyToolSet();

        public TaxInfo()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            /*mConnection = new DB();
            ds = Fn.LoadTaxInfo(null, "Rest_ID", Session["R_ID"].ToString());
            TaxInfoRepeater.DataSource = ds;
            TaxInfoRepeater.DataBind();*/

            string taxinfoid = string.Empty;

            if (Session["R_ID"] == "" || Session["R_ID"] == null)
            {
                Session["bckurl"] = "TaxInfo.aspx";
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
                                        taxinfoid = iTool.decryptString(Request.QueryString["id"]);
                                        Dictionary<string, string> dict;

                                        DateTime sDate = DateTime.Now;
                                        sDate = Fn.GetCommonDate(sDate, Session["DateFormat"]);

                                        SqlParameter[] ArParams = new SqlParameter[8];
                                        ArParams[0] = new SqlParameter("@Literal", SqlDbType.VarChar, 100);
                                        ArParams[0].Value = "";

                                        ArParams[1] = new SqlParameter("@TaxRate", SqlDbType.Decimal);
                                        ArParams[1].Value = 0;

                                        ArParams[2] = new SqlParameter("@Rest_ID", SqlDbType.Int);
                                        ArParams[2].Value = Session["R_ID"];

                                        ArParams[3] = new SqlParameter("@LoggedUserID", SqlDbType.Int);
                                        ArParams[3].Value = Convert.ToInt32(Session["UserID"]);

                                        ArParams[4] = new SqlParameter("@sDate", SqlDbType.DateTime);
                                        ArParams[4].Value = sDate;

                                        ArParams[5] = new SqlParameter("@Mode", SqlDbType.VarChar, 20);
                                        ArParams[5].Value = "del";

                                        ArParams[6] = new SqlParameter("@TaxInfoID", SqlDbType.Int);
                                        ArParams[6].Value = taxinfoid;

                                        ArParams[7] = new SqlParameter("@Status", SqlDbType.Int);
                                        ArParams[7].Value = 0;

                                        dict = new Dictionary<string, string>() { { "Rest_ID", Session["R_ID"].ToString() } };

/*                                        if (Fn.CheckRecordExists(null, "omni_Product_Cooking_Options", "OptionID", optionid))
                                            Msg.Visible = true;
                                        else
                                        {
                                            Msg.Visible = false; */
                                            SqlHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, "SP_omni_TaxInfo_Update", ArParams);
                                            trans.Commit();
                                        //}
                                    }
                                }
                            }

                            ds = Fn.LoadTaxInfo(null, "Rest_ID", Session["R_ID"].ToString());
                            TaxInfoRepeater.DataSource = ds;
                            TaxInfoRepeater.DataBind();
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

        protected void TaxInfoRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (TaxInfoRepeater.Items.Count < 1)
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