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
using System.Data.SqlClient;
using MyDB;
using MyQuery;
using Commons;
using MyTool;

namespace PosOrder
{
    public partial class PurchaseOrders : System.Web.UI.Page
    {
        DB mConnection = new DB();
        DataSet ds = new DataSet();
        Common Fn = new Common();
        SQLQuery Qry = new SQLQuery();
        protected MyToolSet iTool = new MyToolSet();
        Dictionary<string, string> dict = null;

        public PurchaseOrders()
        {
            //
            // TODO: Add constructor logic here
            //
            //Load += new EventHandler(Page_Load);
        }

    /*    protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }
    */

        private void InitializeComponent()
        {
            //Session.Remove("scatid");
            //Session.Remove("ssubcatid");
            //Session.Remove("sproduct");
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            string purchaseordid = string.Empty;

            if (Session["R_ID"] == "" || Session["R_ID"] == null)
            {
                Session["bckurl"] = "PurchaseOrders.aspx";
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
                                        purchaseordid = iTool.decryptString(Request.QueryString["id"]);
                                        dict = new Dictionary<string, string>() { { "Rest_ID", Session["R_ID"].ToString() } };

                                        if (Fn.CheckRecordExists(dict, "omni_Item_ReceivedNotes", "POID", purchaseordid))
                                            Msg.Visible = true;
                                        else
                                        {
                                            Msg.Visible = false;
                                            dict = new Dictionary<string, string>() { { "Rest_ID", Session["R_ID"].ToString() } };

                                            SqlHelper.ExecuteNonQuery(trans, CommandType.Text, Qry.DeleteRec(dict, "omni_PurchaseDetail", 1, "POID", purchaseordid));
                                            SqlHelper.ExecuteNonQuery(trans, CommandType.Text, Qry.DeleteRec(dict, "omni_PurchaseMaster", 1, "POID", purchaseordid));

                                            trans.Commit();
                                        }
                                    }
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

                    dict = new Dictionary<string, string>() { { "a.Rest_ID", Session["R_ID"].ToString() } };

                    ds = Fn.LoadPurchaseOrders(dict, "a.IsActive", "1", conn);
                    POOrderRepeater.DataSource = ds;
                    POOrderRepeater.DataBind();

                }
            }
            catch (Exception ex)
            { }        

        }


        protected void POOrderRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (POOrderRepeater.Items.Count < 1)
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
