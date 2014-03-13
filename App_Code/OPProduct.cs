using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for OPProduct
/// </summary>
public class OPProduct
{
    public
        int id;

    public
    decimal org_qty, adj_qty, org_amt, adj_amt;

    public
    string orderTransId;

    public
    bool hasEffected;

    public OPProduct(int inId, decimal inOrgQty, decimal inAdjQty, decimal inOrgAmt, decimal inAdjAmt)
	{
		this.adj_amt = inAdjAmt;
        this.adj_qty = inAdjQty;
        this.id = inId;
        this.org_qty = inOrgQty;
        this.org_amt = inOrgAmt;
        this.hasEffected = false;
        this.orderTransId = null;
	}

    public decimal unitPrice()
    {
        decimal price = 0;

        price = this.org_amt / this.org_qty;

        return price;
    }
}