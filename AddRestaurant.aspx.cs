using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
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
using settings_appl;

namespace PosRestaurant
{
    public partial class AddRestaurant : System.Web.UI.Page
    {
        DB mConnection = new DB() ;
        DataSet ds = new DataSet();
        Common Fn = new Common();
        MyToolSet iTool = new MyToolSet();
        SQLQuery Qry = new SQLQuery();
        
        //string sQuery = "";
        
        
        public AddRestaurant()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            //txtName.Attributes.Add("onkeyup", "assignusrname('" + txtName.ClientID + "','" + txtUserName.ClientID + "','" + RestInitial.ClientID + "')");
            string str_Sort_Course_By = string.Empty;
            string str_KitchenView_Timeout = string.Empty;
            string str_Quick_Service = string.Empty;
            string str_Table_Layout_Type = string.Empty;
            string str_Sort_Items_By = string.Empty;
            string str_Sort_SubCat_By = string.Empty;
            string str_Sort_Products = string.Empty;
            string str_No_Of_Devices = string.Empty;
            string str_scanner_mode = string.Empty, str_hold_and_fire = string.Empty;

            if (!Page.IsPostBack)
            {
                    using (SqlConnection conn = mConnection.GetConnection())
                    {
                        conn.Open();

                        try
                        {

                            if (Request.QueryString["RID"] != null)
                            {
                                string rid = "";


                                if (Request.QueryString["mode"] != null)
                                    Mode.Value = "edit";

                                SqlParameter[] ArParams = new SqlParameter[29];

                                rid = iTool.decryptString(Request.QueryString["RID"]);

                                ArParams[0] = new SqlParameter("@RestID", SqlDbType.Int);
                                ArParams[0].Value = rid;

                                ArParams[1] = new SqlParameter("@RestName", SqlDbType.VarChar, 150);
                                ArParams[1].Direction = ParameterDirection.Output;

                                ArParams[2] = new SqlParameter("@Initial", SqlDbType.Char, 5);
                                ArParams[2].Direction = ParameterDirection.Output;

                                ArParams[3] = new SqlParameter("@Address1", SqlDbType.VarChar, 255);
                                ArParams[3].Direction = ParameterDirection.Output;

                                ArParams[4] = new SqlParameter("@Address2", SqlDbType.VarChar, 255);
                                ArParams[4].Direction = ParameterDirection.Output;

                                ArParams[5] = new SqlParameter("@City", SqlDbType.VarChar, 50);
                                ArParams[5].Direction = ParameterDirection.Output;

                                ArParams[6] = new SqlParameter("@State", SqlDbType.Int);
                                ArParams[6].Direction = ParameterDirection.Output;

                                ArParams[7] = new SqlParameter("@Zip", SqlDbType.VarChar, 50);
                                ArParams[7].Direction = ParameterDirection.Output;

                                ArParams[8] = new SqlParameter("@Phone", SqlDbType.VarChar, 50);
                                ArParams[8].Direction = ParameterDirection.Output;

                                ArParams[9] = new SqlParameter("@Fax", SqlDbType.VarChar, 50);
                                ArParams[9].Direction = ParameterDirection.Output;

                                ArParams[10] = new SqlParameter("@Email", SqlDbType.VarChar, 255);
                                ArParams[10].Direction = ParameterDirection.Output;

                                ArParams[11] = new SqlParameter("@TablesCount", SqlDbType.Int);
                                ArParams[11].Direction = ParameterDirection.Output;

                                //ArParams[12] = new SqlParameter("@Header", SqlDbType.VarChar, 255);
                                //ArParams[12].Direction = ParameterDirection.Output;

                                /**************  For Footer ***********************************/

                                ArParams[12] = new SqlParameter("@Footer1", SqlDbType.VarChar, 1000);
                                ArParams[12].Direction = ParameterDirection.Output;

                                ArParams[13] = new SqlParameter("@Footer2", SqlDbType.VarChar, 1000);
                                ArParams[13].Direction = ParameterDirection.Output;

                                /**************  For Footer ***********************************/

                                ArParams[14] = new SqlParameter("@KitchenView", SqlDbType.Bit);
                                ArParams[14].Direction = ParameterDirection.Output;

                                ArParams[15] = new SqlParameter("@ExpediteView", SqlDbType.Bit);
                                ArParams[15].Direction = ParameterDirection.Output;

                                ArParams[16] = new SqlParameter("@Status", SqlDbType.Int);
                                ArParams[16].Direction = ParameterDirection.Output;

                                /**************  For Header ***********************************/

                                ArParams[17] = new SqlParameter("@Header_Name", SqlDbType.VarChar, 255);
                                ArParams[17].Direction = ParameterDirection.Output;

                                ArParams[18] = new SqlParameter("@Header_Address1", SqlDbType.VarChar, 1000);
                                ArParams[18].Direction = ParameterDirection.Output;

                                ArParams[19] = new SqlParameter("@Header_City", SqlDbType.VarChar, 50);
                                ArParams[19].Direction = ParameterDirection.Output;

                                ArParams[20] = new SqlParameter("@Header_State", SqlDbType.VarChar, 50);
                                ArParams[20].Direction = ParameterDirection.Output;

                                ArParams[21] = new SqlParameter("@Header_Zip", SqlDbType.VarChar, 20);
                                ArParams[21].Direction = ParameterDirection.Output;

                                ArParams[22] = new SqlParameter("@Header_Phone", SqlDbType.VarChar, 20);
                                ArParams[22].Direction = ParameterDirection.Output;

                                ArParams[23] = new SqlParameter("@Header_ABN", SqlDbType.VarChar, 25);
                                ArParams[23].Direction = ParameterDirection.Output;

                                ArParams[24] = new SqlParameter("@Header_TaxInvoice", SqlDbType.Bit);
                                ArParams[24].Direction = ParameterDirection.Output;

                                ArParams[25] = new SqlParameter("@Header_Website", SqlDbType.VarChar, 100);
                                ArParams[25].Direction = ParameterDirection.Output;

                                ArParams[26] = new SqlParameter("@Header_Email", SqlDbType.VarChar, 100);
                                ArParams[26].Direction = ParameterDirection.Output;

                                ArParams[27] = new SqlParameter("@Tax", SqlDbType.Bit);
                                ArParams[27].Direction = ParameterDirection.Output;

                                ArParams[28] = new SqlParameter("@WebSite", SqlDbType.VarChar, 100);
                                ArParams[28].Direction = ParameterDirection.Output;

                                /**************  For Header ***********************************/

                                // Call ExecuteNonQuery static method of SqlHelper class
                                // We pass in database connection string, command type, stored procedure name and an array of SqlParameter objects
                                SqlHelper.ExecuteNonQuery(conn, CommandType.StoredProcedure, "getRestaurantDetails", ArParams);

                                // Display results in text box using the values of output parameters	

                                //display name on top
                                string itemType = "Location";
                                LblHead.Text = char.ToUpper(Mode.Value[0]) + Mode.Value.Substring(1) + " " + itemType + " [ " + ArParams[1].Value.ToString() + " ]";

                                txtRestName.Value = ArParams[1].Value.ToString();
                                txtInitial.Value = ArParams[2].Value.ToString().Trim();
                                txtAddress1.Value = ArParams[3].Value.ToString();
                                txtAddress2.Value = ArParams[4].Value.ToString();
                                txtCity.Value = ArParams[5].Value.ToString();
                                //txtState.Value =  ArParams[6].Value.ToString();
                                State.SelectedValue = ArParams[6].Value.ToString();
                                txtZip.Value = ArParams[7].Value.ToString();
                                txtPhone.Value = ArParams[8].Value.ToString();
                                txtFax.Value = ArParams[9].Value.ToString();
                                txtEmail.Value = ArParams[10].Value.ToString();
                                TxtWebsite.Value = ArParams[28].Value.ToString();
                                txtTableCount.Value = ArParams[11].Value.ToString();
                                //txtHeader.Value = ArParams[12].Value.ToString();

                                /**************  For Footer ***********************************/

                                txtFooter1.Value = ArParams[12].Value.ToString();
                                txtFooter2.Value = ArParams[13].Value.ToString();

                                /**************  For Footer ***********************************/

                                KitchenView.Checked = ArParams[14].Value.Equals(true) ? true : false;
                                ExpediteView.Checked = ArParams[15].Value.Equals(true) ? true : false;
                                ChkTax.Checked = ArParams[27].Value.Equals(true) ? true : false;
                                Status.Checked = ArParams[16].Value.ToString() == "1" ? true : false;

                                /**************  For Header ***********************************/
                                txtHeaderName.Value = ArParams[17].Value.ToString();
                                txtHeaderAddress1.Value = ArParams[18].Value.ToString();
                                txtHeadercity.Value = ArParams[19].Value.ToString();
                                txtHeaderState.Value = ArParams[20].Value.ToString();
                                txtHeaderZip.Value = ArParams[21].Value.ToString();
                                txtHeaderPhone.Value = ArParams[22].Value.ToString();
                                txtHeaderABN.Value = ArParams[23].Value.ToString();
                                ChkTaxInvoice.Checked = ArParams[24].Value.Equals(true) ? true : false;
                                txtHeaderWebsite.Value = ArParams[25].Value.ToString();
                                txtHeaderEmail.Value = ArParams[26].Value.ToString();

                                /**************  For Footer ***********************************/


                                /*************** For Settings **********************************/

                                SqlParameter[] ArParams1 = new SqlParameter[25];

                                ArParams1[0] = new SqlParameter("@RestID", SqlDbType.Int);
                                ArParams1[0].Value = rid;

                                ArParams1[1] = new SqlParameter("@CustomerView", SqlDbType.Bit);
                                ArParams1[1].Direction = ParameterDirection.Output;

                                ArParams1[2] = new SqlParameter("@AddLines_Between_Order_Item", SqlDbType.Bit);
                                ArParams1[2].Direction = ParameterDirection.Output;

                                ArParams1[3] = new SqlParameter("@Sort_Course_By", SqlDbType.TinyInt);
                                ArParams1[3].Direction = ParameterDirection.Output;

                                ArParams1[4] = new SqlParameter("@Print_Transferred_Order", SqlDbType.Bit);
                                ArParams1[4].Direction = ParameterDirection.Output;

                                ArParams1[5] = new SqlParameter("@Print_Deleted_Order", SqlDbType.Bit);
                                ArParams1[5].Direction = ParameterDirection.Output;

                                ArParams1[6] = new SqlParameter("@Print_Voided_Items", SqlDbType.Bit);
                                ArParams1[6].Direction = ParameterDirection.Output;

                                ArParams1[7] = new SqlParameter("@KitchenView_Timeout", SqlDbType.TinyInt);
                                ArParams1[7].Direction = ParameterDirection.Output;

                                ArParams1[8] = new SqlParameter("@Allow_Void_Items", SqlDbType.Bit);
                                ArParams1[8].Direction = ParameterDirection.Output;

                                ArParams1[9] = new SqlParameter("@Allow_Delete_Order", SqlDbType.Bit);
                                ArParams1[9].Direction = ParameterDirection.Output;

                                ArParams1[10] = new SqlParameter("@Quick_Service", SqlDbType.TinyInt);
                                ArParams1[10].Direction = ParameterDirection.Output;

                                ArParams1[11] = new SqlParameter("@Table_Layout_Type", SqlDbType.TinyInt);
                                ArParams1[11].Direction = ParameterDirection.Output;

                                ArParams1[12] = new SqlParameter("@Auto_Prompt_Tip", SqlDbType.Bit);
                                ArParams1[12].Direction = ParameterDirection.Output;

                                ArParams1[13] = new SqlParameter("@Sort_Items_By", SqlDbType.TinyInt);
                                ArParams1[13].Direction = ParameterDirection.Output;

                                ArParams1[14] = new SqlParameter("@Sort_SubCat_By", SqlDbType.TinyInt);
                                ArParams1[14].Direction = ParameterDirection.Output;

                                ArParams1[15] = new SqlParameter("@Sort_Products", SqlDbType.TinyInt);
                                ArParams1[15].Direction = ParameterDirection.Output;

                                ArParams1[16] = new SqlParameter("@No_Of_Devices", SqlDbType.TinyInt);
                                ArParams1[16].Direction = ParameterDirection.Output;

                                ArParams1[17] = new SqlParameter("@Table_Layout", SqlDbType.Bit);
                                ArParams1[17].Direction = ParameterDirection.Output;

                                ArParams1[18] = new SqlParameter("@Email_Z_Report", SqlDbType.Bit);
                                ArParams1[18].Direction = ParameterDirection.Output;

                                ArParams1[19] = new SqlParameter("@Notify_No_Sale", SqlDbType.Bit);
                                ArParams1[19].Direction = ParameterDirection.Output;

                                ArParams1[20] = new SqlParameter("@ShouldEnableClockIn", SqlDbType.Bit);
                                ArParams1[20].Direction = ParameterDirection.Output;

                                ArParams1[21] = new SqlParameter("@ScannerMode", SqlDbType.TinyInt);
                                ArParams1[21].Direction = ParameterDirection.Output;

                                ArParams1[22] = new SqlParameter("@CashDrawerBalancing", SqlDbType.Bit);
                                ArParams1[22].Direction = ParameterDirection.Output;

                                ArParams1[23] = new SqlParameter("@HoldAndFire", SqlDbType.TinyInt);
                                ArParams1[23].Direction = ParameterDirection.Output;

                                ArParams1[24] = new SqlParameter("@NoSaleLimit", SqlDbType.Int);
                                ArParams1[24].Direction = ParameterDirection.Output;

                                SqlHelper.ExecuteNonQuery(conn, CommandType.StoredProcedure, "[getRestaurant_Settings_Detail]", ArParams1);

                                Customer_View.Checked = ArParams1[1].Value.Equals(true) ? true : false;
                                Add_Lines.Checked = ArParams1[2].Value.Equals(true) ? true : false;
                                Sort_Course_By.SelectedValue = ArParams1[3].Value.ToString();
                                Print_Transfer.Checked = ArParams1[4].Value.Equals(true) ? true : false;
                                Print_Deleted_Orders.Checked = ArParams1[5].Value.Equals(true) ? true : false;
                                Print_Voided_Items.Checked = ArParams1[6].Value.Equals(true) ? true : false;
                                KitchenView_Timeout.SelectedValue = ArParams1[7].Value.ToString();
                                Allow_Void_Items.Checked = ArParams1[8].Value.Equals(true) ? true : false;
                                Allow_Delete_Order.Checked = ArParams1[9].Value.Equals(true) ? true : false;
                                Quick_Service.SelectedValue = ArParams1[10].Value.ToString();
                                Table_Layout_Type.SelectedValue = ArParams1[11].Value.ToString();
                                Auto_Prompt_Tip.Checked = ArParams1[12].Value.Equals(true) ? true : false;
                                Sort_Items_By.SelectedValue = ArParams1[13].Value.ToString();
                                Sort_SubCat_By.SelectedValue = ArParams1[14].Value.ToString();
                                Sort_Products.SelectedValue = ArParams1[15].Value.ToString();
                                No_Of_Devices.SelectedValue = ArParams1[16].Value.ToString();
                                Table_Layout.Checked = ArParams1[17].Value.Equals(true) ? true : false;
                                Email_Z_Report.Checked = ArParams1[18].Value.Equals(true) ? true : false;
                                No_Sale_Notify.Checked = ArParams1[19].Value.Equals(true) ? true : false;
                                EnableClockIn.Checked = ArParams1[20].Value.Equals(true) ? true : false;
                                drawerBalancinCheckBoxg.Checked = ArParams1[22].Value.Equals(true) ? true : false;
                                NoSaleLimitText.Value = ArParams1[24].Value.ToString();

                                str_Sort_Course_By = (ArParams1[3].Value.ToString() != "") ? ArParams1[3].Value.ToString() : "";
                                str_KitchenView_Timeout = (ArParams1[7].Value.ToString() != "") ? ArParams1[7].Value.ToString() : "";
                                str_Quick_Service = (ArParams1[10].Value.ToString() != "") ? ArParams1[10].Value.ToString() : "";
                                str_Table_Layout_Type = (ArParams1[11].Value.ToString() != "") ? ArParams1[11].Value.ToString() : "";
                                str_Sort_Items_By = (ArParams1[13].Value.ToString() != "") ? ArParams1[13].Value.ToString() : "";
                                str_Sort_SubCat_By = (ArParams1[14].Value.ToString() != "") ? ArParams1[14].Value.ToString() : "";
                                str_Sort_Products = (ArParams1[15].Value.ToString() != "") ? ArParams1[15].Value.ToString() : "";
                                str_No_Of_Devices = (ArParams1[16].Value.ToString() != "") ? ArParams1[16].Value.ToString() : "";
                                str_scanner_mode = (ArParams1[21].Value.ToString() != "") ? ArParams1[21].Value.ToString() : "";
                                str_hold_and_fire = (ArParams1[23].Value.ToString() != "") ? ArParams1[23].Value.ToString() : "";

                                /**************************************************************/

                                RestID.Value = rid;
                            }


                            Fn.PopulateDropDown_List(State, Qry.GetStateSQL("isactive", "1"), "StateName", "StateID", "", conn);
                            Fn.PopulateListBoxWithEnumValues(Sort_Course_By, typeof(settings_config.Course_Management_By), str_Sort_Course_By);
                            Fn.PopulateListBoxWithEnumValues(KitchenView_Timeout, typeof(settings_config.Counter), str_KitchenView_Timeout);
                            Fn.PopulateListBoxWithEnumValues(Quick_Service, typeof(settings_config.Quick_Service), str_Quick_Service);
                            Fn.PopulateListBoxWithEnumValues(Table_Layout_Type, typeof(settings_config.TableLayoutType), str_Table_Layout_Type);
                            Fn.PopulateListBoxWithEnumValues(Sort_Items_By, typeof(settings_config.SortItemBy), str_Sort_Items_By);
                            Fn.PopulateListBoxWithEnumValues(Sort_SubCat_By, typeof(settings_config.SortItemBy), str_Sort_SubCat_By);
                            Fn.PopulateListBoxWithEnumValues(Sort_Products, typeof(settings_config.SortItemBy), str_Sort_Products);
                            Fn.PopulateListBoxWithEnumValues(No_Of_Devices, typeof(settings_config.Counter), str_No_Of_Devices);
                            Fn.SelectListBoxWithEnumValues(ScannerModeList, typeof(settings_config.ScannerModeOptions), str_scanner_mode);
                            Fn.SelectListBoxWithEnumValues(holdAndFireList, typeof(settings_config.HoldAndFireOptions), str_hold_and_fire);

                        }
                        catch (Exception ex)
                        {
                            // throw an exception
                            throw ex;
                        }

                        finally
                        {
                            conn.Close();
                        }


                    }
               

            }
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                bool flag = false;

                string strRestName = iTool.formatInputString(txtRestName.Value);
                string strInitial = (txtInitial.Value);
                string strAddress1 = iTool.formatInputString(txtAddress1.Value);
                string strAddress2 = iTool.formatInputString(txtAddress2.Value);
                string strCity= iTool.formatInputString(txtCity.Value);
                //string strState = iTool.formatInputString(txtState.Value);
                string strState = iTool.formatInputString(State.SelectedValue);
                string strZip = iTool.formatInputString(txtZip.Value);
                string strPhone = iTool.formatInputString(txtPhone.Value);
                string strFax = iTool.formatInputString(txtFax.Value);

                string strEmail = iTool.formatInputString(txtEmail.Value);
                string strWebsite = iTool.formatInputString(TxtWebsite.Value);
                
                string tblCount = iTool.formatInputString(txtTableCount.Value);

                //string strHeader = iTool.formatInputString(txtHeader.Value);

                /************** Footer *************************************/

                string strFooter1 = iTool.formatInputString(txtFooter1.Value);
                string strFooter2 = iTool.formatInputString(txtFooter2.Value);

                /************** Footer *************************************/

                /************** Header *************************************/

                string strHeaderName = iTool.formatInputString(txtHeaderName.Value);
                string strHeaderAddress1 = iTool.formatInputString(txtHeaderAddress1.Value);
                string strHeadercity = iTool.formatInputString(txtHeadercity.Value);
                string strHeaderState = iTool.formatInputString(txtHeaderState.Value);
                string strHeaderZip = iTool.formatInputString(txtHeaderZip.Value);

                string strHeaderPhone = iTool.formatInputString(txtHeaderPhone.Value);
                string strHeaderABN = iTool.formatInputString(txtHeaderABN.Value);
                string strHeaderWebsite = iTool.formatInputString(txtHeaderWebsite.Value);
                string strHeaderEmail = iTool.formatInputString(txtHeaderEmail.Value);


                /************** Header *************************************/

                DateTime sDate = DateTime.Now;
                sDate = Fn.GetCommonDate(sDate, Session["DateFormat"]);

                SqlParameter[] ArParams = new SqlParameter[32];

                ArParams[0] = new SqlParameter("@RestName", SqlDbType.VarChar, 150);
                ArParams[0].Value = strRestName;

                ArParams[1] = new SqlParameter("@Initials", SqlDbType.Char, 5);
                ArParams[1].Value = strInitial;

                ArParams[2] = new SqlParameter("@Address1", SqlDbType.VarChar, 255);
                ArParams[2].Value = strAddress1;

                ArParams[3] = new SqlParameter("@Address2", SqlDbType.VarChar, 255);
                ArParams[3].Value = strAddress2;

                ArParams[4] = new SqlParameter("@City", SqlDbType.VarChar,50);
                ArParams[4].Value = strCity;

                ArParams[5] = new SqlParameter("@State", SqlDbType.Int);
                ArParams[5].Value = strState;

                ArParams[6] = new SqlParameter("@Zip", SqlDbType.VarChar, 50);
                ArParams[6].Value = strZip;

                ArParams[7] = new SqlParameter("@Phone", SqlDbType.VarChar, 50);
                ArParams[7].Value = strPhone;

                ArParams[8] = new SqlParameter("@Fax", SqlDbType.VarChar, 50);
                ArParams[8].Value = strFax;

                ArParams[9] = new SqlParameter("@Email", SqlDbType.VarChar, 255);
                ArParams[9].Value = strEmail;

                ArParams[31] = new SqlParameter("@WebSite", SqlDbType.VarChar,100);
                ArParams[31].Value = strWebsite;

                ArParams[10] = new SqlParameter("@TablesCount", SqlDbType.Int);
                ArParams[10].Value = tblCount; 

                //ArParams[11] = new SqlParameter("@Header", SqlDbType.VarChar, 255);
                //ArParams[11].Value = strHeader;

                ArParams[11] = new SqlParameter("@Footer1", SqlDbType.VarChar, 1000);
                ArParams[11].Value = strFooter1;

                ArParams[12] = new SqlParameter("@Footer2", SqlDbType.VarChar, 1000);
                ArParams[12].Value = strFooter2;

                ArParams[13] = new SqlParameter("@KitchenView", SqlDbType.Bit);
                ArParams[13].Value = KitchenView.Checked ? true : false;

                ArParams[14] = new SqlParameter("@ExpediteView", SqlDbType.Bit);
                ArParams[14].Value = ExpediteView.Checked ? true : false;

                ArParams[30] = new SqlParameter("@Tax", SqlDbType.Bit);
                ArParams[30].Value = ChkTax.Checked ? true : false;

                ArParams[15] = new SqlParameter("@Status", SqlDbType.Int);
                ArParams[15].Value = Status.Checked ? 1 : 0;

                ArParams[16] = new SqlParameter("@sDate", SqlDbType.DateTime);
                ArParams[16].Value = sDate;

                ArParams[17] = new SqlParameter("@LoggedUserID", SqlDbType.Int);
                ArParams[17].Value = Convert.ToInt32(Session["UserID"]);

                ArParams[18] = new SqlParameter("@Mode", SqlDbType.VarChar, 20);
                ArParams[18].Value = Mode.Value;

                ArParams[19] = new SqlParameter("@RestID", SqlDbType.Int);
                ArParams[19].Value = RestID.Value;


                /**************** Header *************************************/

                ArParams[20] = new SqlParameter("@Header_Name", SqlDbType.VarChar, 255);
                ArParams[20].Value = strHeaderName;

                ArParams[21] = new SqlParameter("@Header_Address1", SqlDbType.VarChar, 1000);
                ArParams[21].Value = strHeaderAddress1;

                ArParams[22] = new SqlParameter("@Header_City", SqlDbType.VarChar, 50);
                ArParams[22].Value = strHeadercity;

                ArParams[23] = new SqlParameter("@Header_State", SqlDbType.VarChar, 50);
                ArParams[23].Value = strHeaderState;

                ArParams[24] = new SqlParameter("@Header_Zip", SqlDbType.VarChar, 20);
                ArParams[24].Value = strHeaderZip;

                ArParams[25] = new SqlParameter("@Header_Phone", SqlDbType.VarChar, 20);
                ArParams[25].Value = strHeaderPhone;

                ArParams[26] = new SqlParameter("@Header_ABN", SqlDbType.VarChar, 25);
                ArParams[26].Value = strHeaderABN;

                ArParams[27] = new SqlParameter("@Header_TaxInvoice", SqlDbType.Bit);
                ArParams[27].Value = ChkTaxInvoice.Checked ? 1 : 0;

                ArParams[28] = new SqlParameter("@Header_Website", SqlDbType.VarChar, 100);
                ArParams[28].Value = strHeaderWebsite;

                ArParams[29] = new SqlParameter("@Header_Email", SqlDbType.VarChar, 100);
                ArParams[29].Value = strHeaderEmail;


                /****************  Header *************************************/

                /************** Settings *************************************/

                SqlParameter[] ArParams1 = new SqlParameter[26];

                ArParams1[0] = new SqlParameter("@CustomerView", SqlDbType.Bit);
                ArParams1[0].Value = Customer_View.Checked ? 1 : 0;

                ArParams1[1] = new SqlParameter("@AddLines_Between_Order_Item", SqlDbType.Bit);
                ArParams1[1].Value = Add_Lines.Checked ? 1 : 0;

                ArParams1[2] = new SqlParameter("@Sort_Course_By", SqlDbType.TinyInt);
                ArParams1[2].Value = (Sort_Course_By.SelectedValue == "") ? "0" : Sort_Course_By.SelectedValue;

                ArParams1[3] = new SqlParameter("@Print_Transferred_Order", SqlDbType.Bit);
                ArParams1[3].Value = Print_Transfer.Checked ? 1 : 0;

                ArParams1[4] = new SqlParameter("@Print_Deleted_Order", SqlDbType.Bit);
                ArParams1[4].Value = Print_Deleted_Orders.Checked ? 1 : 0;

                ArParams1[5] = new SqlParameter("@Print_Voided_Items", SqlDbType.Bit);
                ArParams1[5].Value = Print_Voided_Items.Checked ? 1 : 0;

                ArParams1[6] = new SqlParameter("@KitchenView_Timeout", SqlDbType.TinyInt);
                ArParams1[6].Value = (KitchenView_Timeout.SelectedValue == "") ? "0" : KitchenView_Timeout.SelectedValue;

                ArParams1[7] = new SqlParameter("@Allow_Void_Items", SqlDbType.Bit);
                ArParams1[7].Value = Allow_Void_Items.Checked ? 1 : 0;

                ArParams1[8] = new SqlParameter("@Allow_Delete_Order", SqlDbType.Bit);
                ArParams1[8].Value = Allow_Delete_Order.Checked ? 1 : 0;

                ArParams1[9] = new SqlParameter("@Quick_Service", SqlDbType.TinyInt);
                ArParams1[9].Value = (Quick_Service.SelectedValue == "") ? "0" : Quick_Service.SelectedValue;

                ArParams1[10] = new SqlParameter("@Table_Layout_Type", SqlDbType.TinyInt);
                ArParams1[10].Value = (Table_Layout_Type.SelectedValue == "") ? "0" : Table_Layout_Type.SelectedValue;

                ArParams1[11] = new SqlParameter("@Auto_Prompt_Tip", SqlDbType.Bit);
                ArParams1[11].Value = Auto_Prompt_Tip.Checked ? 1 : 0;

                ArParams1[12] = new SqlParameter("@Sort_Items_By", SqlDbType.TinyInt);
                ArParams1[12].Value = (Sort_Items_By.SelectedValue == "") ? "0" : Sort_Items_By.SelectedValue;

                ArParams1[13] = new SqlParameter("@Sort_SubCat_By", SqlDbType.TinyInt);
                ArParams1[13].Value = (Sort_SubCat_By.SelectedValue == "") ? "0" : Sort_SubCat_By.SelectedValue;

                ArParams1[14] = new SqlParameter("@Sort_Products", SqlDbType.TinyInt);
                ArParams1[14].Value = (Sort_Products.SelectedValue == "") ? "0" : Sort_Products.SelectedValue;

                ArParams1[15] = new SqlParameter("@No_Of_Devices", SqlDbType.TinyInt);
                ArParams1[15].Value = (No_Of_Devices.SelectedValue == "") ? "0" : No_Of_Devices.SelectedValue;

                ArParams1[16] = new SqlParameter("@Table_Layout", SqlDbType.Bit);
                ArParams1[16].Value = Table_Layout.Checked ? 1 : 0;

                ArParams1[17] = new SqlParameter("@Email_Z_Report", SqlDbType.Bit);
                ArParams1[17].Value = Email_Z_Report.Checked ? 1 : 0;

                ArParams1[18] = new SqlParameter("@Notify_No_Sale", SqlDbType.Bit);
                ArParams1[18].Value = No_Sale_Notify.Checked ? 1 : 0;

                ArParams1[19] = new SqlParameter("@Rest_ID", SqlDbType.Int);
                ArParams1[19].Value = RestID.Value;

                ArParams1[20] = new SqlParameter("@Mode", SqlDbType.VarChar, 20);
                ArParams1[20].Value = Mode.Value;

                ArParams1[21] = new SqlParameter("@ShouldEnableClockIn", SqlDbType.Bit);
                ArParams1[21].Value = EnableClockIn.Checked ? 1 : 0;

                ArParams1[22] = new SqlParameter("@ScannerMode", SqlDbType.TinyInt);
                ArParams1[22].Value = (ScannerModeList.SelectedValue == "") ? "0" : ScannerModeList.SelectedValue;

                ArParams1[23] = new SqlParameter("@CashDrawerBalancing", SqlDbType.Bit);
                ArParams1[23].Value = drawerBalancinCheckBoxg.Checked ? 1 : 0;

                ArParams1[24] = new SqlParameter("@HoldAndFire", SqlDbType.TinyInt);
                ArParams1[24].Value = (holdAndFireList.SelectedValue == "") ? "0" : holdAndFireList.SelectedValue;

                ArParams1[24] = new SqlParameter("@NoSaleLimit", SqlDbType.Int);
                ArParams1[24].Value = (NoSaleLimitText.Value == "") ? "0" : NoSaleLimitText.Value;

                /****************************************************************/

                Dictionary<string, string> dict;

                using (SqlConnection conn = mConnection.GetConnection())
                {
                    conn.Open();

                    try
                    {
                        if (RestID.Value == "-1" && Mode.Value == "add")
                        {
                            dict = null;
                            flag = Fn.CheckRecordExists(dict, "omni_Restuarnt_info", "Initials", "Initials", "", "", "", ArParams, conn);
                        }
                        else
                        {
                            //========= FOR IMPLEMENTING MORE "AND" CLAUSE IN SQL OPERATION ================//

                            //dict = new Dictionary<string, string>() { { "UserGroupId", GroupID.Value } };
                            //flag = Fn.CheckRecordExists(dict, "omni_user_group", "UserGroupName", "UserGroupName", "edit", "UserGroupId", GroupID.Value, ArParams);

                            //=============================================================================//

                            flag = Fn.CheckRecordExists(null, "omni_Restuarnt_info", "Initials", "Initials", "edit", "Rest_ID", RestID.Value, ArParams, conn);
                        }

                        if (flag == true)
                        {
                            LblInitial.Text = "This Initial is already defined. Try with another one.";
                            return;
                        }

                        if (RestID.Value == "-1" && Mode.Value == "add")
                        {
                            dict = null;
                            flag = Fn.CheckRecordExists(dict, "omni_Restuarnt_info", "Email", "Email", "", "", "", ArParams, conn);
                        }
                        else
                        {
                            flag = Fn.CheckRecordExists(null, "omni_Restuarnt_info", "Email", "Email", "edit", "Rest_ID", RestID.Value, ArParams, conn);
                        }

                        if (flag == true)
                        {
                            LblEmail.Text = "Email already exist. Try with another one.";
                            return;
                        }

                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }


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

                            // Perform the debit operation
                            //SqlHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, "Debit", paramFromAcc, paramDebitAmount);
                            //SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sQuery, ArParams);




                            SqlHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, "SP_omni_Restaurant_Info_Update", ArParams);

                            SqlHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, "SP_omni_Restaurant_Settings_Info_Update", ArParams1);
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
            catch (Exception ex)
            {
                throw ex;
                // CreateLogFiles.ErrorLog(ex.Message.ToString() & " - " & System.IO.Path.GetFileName(System.Web.HttpContext.Current.Request.Url.AbsolutePath) & " - btnCreate_Click")
                // lblError.Text = "Error - Please contact Administrator "
                // Exit Sub
            }
            //Response.Redirect("Restaurants.aspx");
            Response.Redirect("Select_Restaurants.aspx");
        }

    }
}