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

namespace PosInventory
{
    public partial class GoodsReceivedNotes : System.Web.UI.Page
    {
        DB mConnection = new DB();
        DataSet ds = new DataSet();
        Common Fn = new Common();
        SQLQuery Qry = new SQLQuery();
        protected MyToolSet iTool = new MyToolSet();
        Dictionary<string, string> dict = null;

        public GoodsReceivedNotes()
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
            string grnid = string.Empty;

            if (Session["R_ID"] == "" || Session["R_ID"] == null)
            {
                Session["bckurl"] = "GoodsReceivedNotes.aspx";
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
                                        grnid = iTool.decryptString(Request.QueryString["id"]);

                                        DateTime sDate = DateTime.Now;
                                        sDate = Fn.GetCommonDate(sDate, Session["DateFormat"]);

                                        //if (Fn.CheckRecordExists(dict, "omni_Item_ReceivedNotes", "GRNNo", grnid,trans))
                                        //    Msg.Visible = true;
                                        //else
                                        //{
                                        //    Msg.Visible = false; 

                                            dict = null;
                                            dict = new Dictionary<string, string>() { { "Rest_ID", Session["R_ID"].ToString() } };
                                            SqlHelper.ExecuteNonQuery(trans, CommandType.Text, Qry.DeleteRec(dict, "omni_Item_ReceivedNotesDetail", 1, "GRNNO", grnid));

                                            //dict = null;
                                            //dict = new Dictionary<string, string>() { { "Rest_ID", Session["R_ID"].ToString() }, {"Tran_Type", "RC"} };
                                            //SqlHelper.ExecuteNonQuery(trans, CommandType.Text, Qry.DeleteRec(dict, "omni_Tran_ItemHistory", 1, "Tran_No", grnid));

                                            dict = null;
                                            dict = new Dictionary<string, string>() { { "Rest_ID", Session["R_ID"].ToString() } };
                                            SqlHelper.ExecuteNonQuery(trans, CommandType.Text, Qry.DeleteRec(dict, "omni_Item_ReceivedNotes", 1, "GRNNo", grnid));

                                            trans.Commit();
                                       // }
                                    }
                                }
//
//                                Fn.PopulateDropDown_List(Category, Qry.GetParentCategoriesSQL(Convert.ToInt32(Session["R_ID"])), "CategoryName", "CategoryID", "");
                                
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

                    dict = null;
                    dict = new Dictionary<string, string>() { { "a.Rest_ID", Session["R_ID"].ToString() } };

                    ds = Fn.LoadGoodsReceivedNotes(dict, "a.IsActive", "1", conn);
                    GRNRepeater.DataSource = ds;
                    GRNRepeater.DataBind();

                }
            }
            catch (Exception ex)
            { }        

        }


        protected void GRNRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (GRNRepeater.Items.Count < 1)
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
