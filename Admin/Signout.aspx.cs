﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Signout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["Insuser_id"] = "";
        Session["Insuser_name"] = "";
        Session.Abandon();
        Session.Clear();
    }
}