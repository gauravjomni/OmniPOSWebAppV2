/// <summary>
/// Summary description for Sale
/// </summary>
using System.Collections.Generic;
public class Sale
{
    public List<ChartSale> ChartSaleData { get; set; }
    public string ChartImage { get; set; }
    public string TransactDate { get; set; }
    public decimal TipAmount { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal SurCharge { get; set; }
    public decimal Discount { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal TotalRefundAmount { get; set; }
    public decimal TotalPayoutAmount { get; set; }
    public decimal CashSale { get; set; }
    public decimal CardSale { get; set; }
    public decimal VoucherSale { get; set; }
    public decimal NoSaleCount { get; set; }
    public decimal TotalGrossAmount { get; set; }
    public decimal TotalNetAmount { get; set; }
    public decimal TotalFloatAmt { get; set; }
    public List<Drawer> Drawers { get; set; }
}

public class Drawer
{
    public string PrinterName { get; set; }
    public decimal CashSale { get; set; }
    public decimal CardSale { get; set; }
    public decimal VoucherSale { get; set; }
    public decimal PayOut { get; set; }
    public decimal PayRefund { get; set; }
}

public class ChartSale
{
    public string Name { get; set; }
    public string Value { get; set; }
}

public class BarChartData
{
    public string Scale { get; set; }
    public int CashSale { get; set; }
    public int CardSale { get; set; }
    public int VoucherSale { get; set; }
    public int Discount { get; set; }
    public int Payout { get; set; }
    public int PayRefund { get; set; }
}


/*
    public class OrderHistoryData
    {
        public string ID { get; set; }
        public string RMSSkuID { get; set; }
        public string OrderDate { get; set; }
        public string AddressDetail { get; set; }
        public List<ItemDetail> OrderDetails { get; set; }
    }
 */