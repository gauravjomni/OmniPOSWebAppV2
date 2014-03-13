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
using MyQuery;
using Commons;

namespace PosGroupPerm
{
    public partial class GroupPermissions : System.Web.UI.Page
    {
        DB mConnection = new DB() ;
        DataSet ds = new DataSet();
        Common Fn = new Common();
        SQLQuery Qry = new SQLQuery();
        MyToolSet iTool = new MyToolSet();
        string sQuery = "";


        public GroupPermissions()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                chkall.Attributes.Add("onclick", "selectAll(this)");

                //mConnection = new DB();

                //SqlHelper.FillDataset(mConnection.GetConnection(), CommandType.Text, "select MenuID,MenuName,NavigateURL from omni_Modules where ParentMenuId=0", ds, new string[] { "P_omni_Modules" });

                using (SqlConnection conn = mConnection.GetConnection())
                {
                    conn.Open();

                    try
                    {
                        Fn.LoadAPPModules(0, ds, "P_omni_GrpPerms",conn);
                        Fn.LoadAPPModules(1, ds, "C_omni_GrpPerms", conn);

                        ds.Relations.Add("ParentChildGroup", ds.Tables["P_omni_GrpPerms"].Columns["MenuID"], ds.Tables["C_omni_GrpPerms"].Columns["ParentMenuID"], false);
                        ParentGroupPermRepeater.DataSource = ds.Tables["P_omni_GrpPerms"];
                        ParentGroupPermRepeater.DataBind();

                        Fn.PopulateDropDown_List(DDUserGroup, Qry.GetUserGroupSQL(1), "UserGroupName", "UserGroupID", "",conn);

                        if (Request.QueryString["grpid"] != null)
                            DDUserGroup.SelectedValue = Request.QueryString["grpid"].ToString();

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

        protected void ParentGroupPermRepeater_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HiddenField HD = (HiddenField)e.Item.FindControl("ChkParent");
                HD.Value = ds.Tables[0].Rows[e.Item.ItemIndex]["MenuID"].ToString();
            }
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime sDate = DateTime.Now;
                sDate = Fn.GetCommonDate(sDate, Session["DateFormat"]);

                string strUserGroup = DDUserGroup.SelectedValue;

                Dictionary<string, string> dict;

                using (SqlConnection conn = mConnection.GetConnection())
                {
                    conn.Open();

                    using (SqlTransaction trans = conn.BeginTransaction())
                    {
                        // Establish command parameters

                        try
                        {
                            // Call ExecuteNonQuery static method of SqlHelper class for debit and credit operations.
                            // We pass in SqlTransaction object, command type, stored procedure name, and a comma delimited list of SqlParameters

                            SqlHelper.ExecuteNonQuery(trans, CommandType.Text, Qry.DeleteRec(null,"omni_Group_Permissions",1,"UserGroupID",strUserGroup));

                                foreach (RepeaterItem item in ParentGroupPermRepeater.Items)
                                {
                                    if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                                    {
                                        Repeater Child = (Repeater)item.FindControl("ChildGroupPermRepeater");
                                        HiddenField HD = (HiddenField)item.FindControl("ChkParent");

                                        string ParentMenuID = HD.Value;

                                        foreach (RepeaterItem subitem in Child.Items)
                                        {
                                            HtmlInputCheckBox chkChild = (HtmlInputCheckBox)subitem.FindControl("ChkChild");

                                            if (chkChild.Checked == true)
                                            {
//                                                Response.Write("Item Checked :" + chkChild.Value);

                                                SqlParameter[] ArParams = new SqlParameter[6];
                                                ArParams[0] = new SqlParameter("@UserGroupID", SqlDbType.Int);
                                                ArParams[0].Value = strUserGroup;

                                                ArParams[1] = new SqlParameter("@ParentMenuID", SqlDbType.Int);
                                                ArParams[1].Value = ParentMenuID;

                                                ArParams[2] = new SqlParameter("@MenuID", SqlDbType.Int);
                                                ArParams[2].Value = chkChild.Value;

                                                ArParams[3] = new SqlParameter("@LoggedUserID", SqlDbType.Int);
                                                ArParams[3].Value = Convert.ToInt32(Session["UserID"]);

                                                ArParams[4] = new SqlParameter("@sDate", SqlDbType.DateTime);
                                                ArParams[4].Value = sDate;

                                                ArParams[5] = new SqlParameter("@Mode", SqlDbType.Char,10);
                                                ArParams[5].Value = Mode.Value;

                                                SqlHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, "SP_omni_GroupPermission_Update", ArParams);
                                            }
                                        }
                                    }
                                }
                            trans.Commit();
                            //txtResults.Text = "Transfer Completed";
                            Panel.Visible = true;
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

            }
            catch(Exception ex)
            {
            }
            
        }

        protected void CmdView_Click(object sender, EventArgs e)
        {
            bool chkboxstat =false ;

            string strUserGroupid = DDUserGroup.SelectedValue;

            using (SqlConnection conn = mConnection.GetConnection())
            {
                conn.Open();

                try
                {
                    Fn.LoadAPPModules(0, ds, "P_omni_GrpPerms",conn);
                    Fn.LoadAPPModules(1, ds, "C_omni_GrpPerms",conn);
                    
                    ds.Relations.Add("ParentChildGroup", ds.Tables["P_omni_GrpPerms"].Columns["MenuID"], ds.Tables["C_omni_GrpPerms"].Columns["ParentMenuID"], false);
                    ParentGroupPermRepeater.DataSource = ds.Tables["P_omni_GrpPerms"];
                    ParentGroupPermRepeater.DataBind();

                    foreach (RepeaterItem item in ParentGroupPermRepeater.Items)
                    {
                        if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                        {
                            Repeater Child = (Repeater)item.FindControl("ChildGroupPermRepeater");

                            foreach (RepeaterItem subitem in Child.Items)
                            {
                                HtmlInputCheckBox chkChild = (HtmlInputCheckBox)subitem.FindControl("ChkChild");

                                Dictionary<string, string> dict;
                                dict = new Dictionary<string, string>() { { "MenuID", chkChild.Value } };

                                if (Fn.CheckRecordExists(dict, "omni_Group_Permissions", "UserGroupID", strUserGroupid,conn))
                                {
                                    chkChild.Checked = true;
                                    Mode.Value = "edit";
                                    chkboxstat = true;
                                }
                                else
                                    chkboxstat =false;
                            }
                        }
                    }

                    if (chkboxstat == true)
                        chkall.Checked = true;
                    else
                        chkall.Checked = false;

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