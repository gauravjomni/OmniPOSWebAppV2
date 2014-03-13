using System;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using MyDB;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using System.Data.SqlClient;

namespace Commons
{
    /// <summary>
    /// Summary description for DALRequest
    /// </summary>
    public class DALRequest
    {
        public Sale GetCurrentSales(DateTime? dateFrom, DateTime? dateTo)
        {
            Sale objSale = null;

            var AppConnection = new DB();

            var sqlPrm = new SqlParameter[4];
            sqlPrm[0] = new SqlParameter("@RestaurantID", ManageSession.User.R_ID);
            sqlPrm[1] = new SqlParameter("@LogName", "ZReport");
            sqlPrm[2] = new SqlParameter("@FromDate", dateFrom);
            sqlPrm[3] = new SqlParameter("@ToDate", dateTo);

            var oReader = SqlHelper.ExecuteReader(AppConnection.GetConnection(), CommandType.StoredProcedure, "GetCurrentSales", sqlPrm);

            if (oReader.HasRows)
            {
                while (oReader.Read())
                {
                    List<ChartSale> objChartSale = null;
                    List<Drawer> objDrawer = null;

                    if ((oReader["ChartSaleData"]) != null)
                    {
                        var chartXML = Convert.ToString(oReader["ChartSaleData"]);
                        if (!string.IsNullOrEmpty(chartXML))
                        {
                            var serializer = new XmlSerializer(typeof(List<ChartSale>));
                            var stringReader = new StringReader(chartXML);

                            objChartSale = serializer.Deserialize(stringReader) as List<ChartSale>;
                        }
                    }

                    if ((oReader["Drawers"]) != null)
                    {
                        var drawerXML = Convert.ToString(oReader["Drawers"]);
                        if (!string.IsNullOrEmpty(drawerXML))
                        {
                            var serializer = new XmlSerializer(typeof(List<Drawer>));
                            var stringReader = new StringReader(drawerXML);

                            objDrawer = serializer.Deserialize(stringReader) as List<Drawer>;
                        }
                    }

                    objSale = new Sale
                    {
                        TransactDate = Convert.ToString(oReader["TransactDate"]),
                        CashSale = Convert.ToDecimal(oReader["CashSale"]),
                        CardSale = Convert.ToDecimal(oReader["CardSale"]),
                        VoucherSale = Convert.ToDecimal(oReader["VoucherSale"]),
                        Discount = Convert.ToDecimal(oReader["Discount"]),
                        SurCharge = Convert.ToDecimal(oReader["SurCharge"]),
                        TotalRefundAmount = Convert.ToDecimal(oReader["TotalRefundAmount"]),
                        TotalPayoutAmount = Convert.ToDecimal(oReader["TotalPayoutAmount"]),
                        TipAmount = Convert.ToDecimal(oReader["TipAmount"]),
                        TaxAmount = Convert.ToDecimal(oReader["TaxAmount"]),
                        NoSaleCount = Convert.ToInt32(oReader["NoSaleCount"]),
                        TotalNetAmount = Convert.ToDecimal(oReader["TotalNetAmount"]),
                        TotalGrossAmount = Convert.ToDecimal(oReader["TotalGrossAmount"]),
                        TotalAmount = Convert.ToDecimal(oReader["TotalAmount"]),
                        Drawers = objDrawer,
                        ChartSaleData = objChartSale
                    };
                }
            }
            oReader.Close();
            return objSale;
        }
    }
}