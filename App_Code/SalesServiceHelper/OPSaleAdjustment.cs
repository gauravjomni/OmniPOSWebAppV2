using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using MyDB;
using Commons;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using MyQuery;
using System.IO;
using System.Text;

/// <summary>
/// This class is very private. Functionality it does is very confidential. 
/// Do not expose this functionaly to the outside company. 
/// 
/// </summary>
public class OPSaleAdjustment
{
	public OPSaleAdjustment()
	{
		
	}

    /*
    *   applies the adjustment on the given products sale in a range
    *   
    */
    public bool applyAdjustmentOnProducts(OPProduct[] products, string fromdate, string tilldate, string rest_Id)
    {
        DB mConnection = new DB();
        Common Fn = new Common();
        DataSet ds = new DataSet();

        using (SqlConnection conn = mConnection.GetConnection())
        {
            conn.Open();

            try
            {
                for (int i = 0; i < products.Count(); i++)
                {
                    OPProduct product = products[i];
                    this.applyAdjustmentOnProduct(product, fromdate, tilldate, rest_Id, conn);
                }
            }

            catch (Exception ex)
            {
                return false;
                throw ex;
            }

            finally
            {
                conn.Close();
            }
        }

        return true;
    }

    // THIS IS THE MAIN CONCEPT WHERE WE APPLY REDUCE/INCREASE ON EACH PRODUCT
    // reduce or increase the products sale 
    // OPProduct contains values org_qty and _adj_qty; org_qty represents original quantity which never changes. 
    // so here reduce and increase is known by the diff(org_qty, adj_qty)
    private void applyAdjustmentOnProduct(OPProduct product, string fromdate, string tilldate, string rest_Id, SqlConnection conn)
    {
        Common Fn = new Common();
        SQLQuery Qry = new SQLQuery();

        decimal toBeChangedQty = 0;
        bool shouldReduce = false;

        if (product.adj_qty < product.org_qty)
        {
            shouldReduce = true;
            toBeChangedQty = product.org_qty - product.adj_qty;
        }
        else
        {
            shouldReduce = false;
            toBeChangedQty = product.adj_qty - product.org_qty;
        }

        //select products
        Dictionary<string, string> dict2 = new Dictionary<string, string>() { { "b.Rest_ID", rest_Id } };
        DataSet ds2 = SqlHelper.ExecuteDataset(conn, CommandType.Text, Qry.getCashProductsSoldInRangeByProductIdSQL(dict2, fromdate, tilldate, product.id));

        int rowCount = ds2.Tables[0].Rows.Count;
        string[] appliedOrderIDs = new string[rowCount];

        if (rowCount > 0)
        {
            OPProduct[] selectedProducts = new OPProduct[rowCount];
            int tabCount = 0;
            bool shouldApplyChanges = false;
            decimal totalQty = 0;
            foreach (DataRow inner_dr in ds2.Tables[0].Rows)
            {
                int productId = Convert.ToInt32(inner_dr["ProductID"].ToString());
                string order_TranID = inner_dr["Order_TranID"].ToString();
                decimal qty = Convert.ToDecimal(inner_dr["Qty"].ToString());
                decimal amount = Convert.ToDecimal(inner_dr["Amount"].ToString());
                totalQty += qty;

                OPProduct prod = new OPProduct(productId, qty, 0, amount, 0);
                selectedProducts[tabCount] = prod;
                prod.orderTransId = order_TranID;
                appliedOrderIDs[tabCount] = order_TranID;
                tabCount++;
            }
            ds2.Dispose();

            if (shouldReduce && totalQty < toBeChangedQty)
                toBeChangedQty = 0;

            while (toBeChangedQty > 0)
            {
                foreach (OPProduct prod in selectedProducts)
                {
                    if (shouldReduce)
                    {
                        if (prod.org_qty > 0)
                        {
                            if (toBeChangedQty < 1 && toBeChangedQty > 0)
                            {
                                decimal change_qty = prod.org_qty - toBeChangedQty;
                                if (change_qty >= 0)
                                {
                                    prod.org_qty = change_qty;
                                    toBeChangedQty -= toBeChangedQty;
                                    prod.hasEffected = true;
                                    shouldApplyChanges = true;
                                }
                            }
                            else
                            {
                                decimal change_qty = prod.org_qty - 1;
                                if (change_qty >= 0)
                                {
                                    prod.org_qty = change_qty;
                                    toBeChangedQty--;
                                    prod.hasEffected = true;
                                    shouldApplyChanges = true;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (toBeChangedQty < 1 && toBeChangedQty > 0)
                        {
                            prod.org_qty += toBeChangedQty;
                            toBeChangedQty -= toBeChangedQty;
                            prod.hasEffected = true;
                            shouldApplyChanges = true;
                        }
                        else
                        {
                            prod.org_qty += 1;
                            toBeChangedQty--;
                            prod.hasEffected = true;
                            shouldApplyChanges = true;
                        }
                    }

                    prod.org_amt = product.unitPrice() * prod.org_qty;

                    if (toBeChangedQty <= 0)
                        break;
                }
            }

            if (shouldApplyChanges)
            {
                foreach (OPProduct prod in selectedProducts)
                {
                    if (prod.hasEffected)
                    {
                        SqlParameter[] ArParamsT = new SqlParameter[5];
                        ArParamsT[0] = new SqlParameter("@OrderTranID", SqlDbType.VarChar, 50);
                        ArParamsT[0].Value = prod.orderTransId;

                        ArParamsT[1] = new SqlParameter("@ProductID", SqlDbType.Int);
                        ArParamsT[1].Value = prod.id;

                        ArParamsT[2] = new SqlParameter("@Qty", SqlDbType.Decimal);
                        ArParamsT[2].Value = prod.org_qty;

                        ArParamsT[3] = new SqlParameter("@Amount", SqlDbType.Decimal);
                        ArParamsT[3].Value = prod.org_amt;

                        using (SqlTransaction trans = conn.BeginTransaction())
                        {
                            try
                            {
                                SqlHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, "SP_omni_Order_Product_Update", ArParamsT);

                                trans.Commit();
                            }

                            catch (Exception ex)
                            {
                                // throw exception
                                trans.Rollback();
                                throw ex;
                            }

                        }

                    }
                }

                //update order and payment transactions
                this.adjustTotalsForOrders(appliedOrderIDs, rest_Id, conn);
            }
        }
    }

    //since we reduced/increased the individual item's sold amount; now its time to update respective order and payment transactions
    //here we have the list of orderInfo's IDs whose totals is to be re-calculated and update them
    //in this way we will have consistent figures all over the report
    private void adjustTotalsForOrders(string[] orderIDs, string rest_Id, SqlConnection conn)
    {
        //remove duplicates
        string[] newOrderIDs = orderIDs.Distinct().ToArray();

        Common Fn = new Common();
        SQLQuery Qry = new SQLQuery();

        foreach (string orderTransId in newOrderIDs)
        {
            Dictionary<string, string> dict2 = new Dictionary<string, string>() { { "b.Rest_ID", rest_Id } };

            DataSet ds2 = SqlHelper.ExecuteDataset(conn, CommandType.Text, Qry.getTotalsAmountForOrderInfoSQL(dict2, orderTransId)); //load new total_amount, discount and surcharge for this order

            DataSet ds3 = SqlHelper.ExecuteDataset(conn, CommandType.Text, Qry.getTotalTaxableAmountForOrderInfoSQL(dict2, orderTransId)); //load taxableAmount

            DataSet paymtDs = SqlHelper.ExecuteDataset(conn, CommandType.Text, Qry.getPaymetInfoIdForOrderInfoSQL(dict2, orderTransId)); //load payment table

            if (ds2.Tables[0].Rows.Count > 0)
            {
                decimal grossAmount = 0, netAmount = 0, totalAmount = 0, discount = 0, surcharge = 0, totalTaxableAmount = 0, totalTax = 0;
                int orderPaymentId = 0;

                foreach (DataRow inner_dr in ds2.Tables[0].Rows)
                {
                    totalAmount = Convert.ToDecimal(inner_dr["TotalAmount"].ToString());
                    discount = Convert.ToDecimal(inner_dr["Discount"].ToString());
                    surcharge = Convert.ToDecimal(inner_dr["Surcharge"].ToString());
                }

                foreach (DataRow inner_dr in ds3.Tables[0].Rows)
                {
                    totalTaxableAmount = Convert.ToDecimal(inner_dr["TotalTaxAmount"].ToString());
                }

                foreach (DataRow inner_dr in paymtDs.Tables[0].Rows)
                {
                    orderPaymentId = Convert.ToInt32(inner_dr["OrderPaymentId"].ToString());
                }

                //calculate GST assuming 10% inclusive (only in AUS)
                //NOTE: for other countries this code has to be changed
                totalTax = (totalTaxableAmount / (100 + 10)) * 10;
                grossAmount = (totalAmount + surcharge) - discount;
                netAmount = grossAmount - totalTax;


                //update order
                SqlParameter[] ArParamsT = new SqlParameter[6];
                ArParamsT[0] = new SqlParameter("@OrderTranID", SqlDbType.VarChar, 50);
                ArParamsT[0].Value = orderTransId;

                ArParamsT[1] = new SqlParameter("@Rest_id", SqlDbType.Int);
                ArParamsT[1].Value = rest_Id;

                ArParamsT[2] = new SqlParameter("@GrossAmount", SqlDbType.Decimal);
                ArParamsT[2].Value = grossAmount;

                ArParamsT[3] = new SqlParameter("@TotalTax", SqlDbType.Decimal);
                ArParamsT[3].Value = totalTax;

                ArParamsT[4] = new SqlParameter("@TotalAmount", SqlDbType.Decimal);
                ArParamsT[4].Value = netAmount;

                ArParamsT[5] = new SqlParameter("@OrderPaymentId", SqlDbType.Int);
                ArParamsT[5].Value = orderPaymentId;

                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        SqlHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, "SP_omni_OrderTransaction_Update", ArParamsT);

                        trans.Commit();
                    }

                    catch (Exception ex)
                    {
                        // throw exception
                        trans.Rollback();
                        throw ex;
                    }

                }
            }

            ds2.Dispose();
            ds3.Dispose();
        }
    }
}