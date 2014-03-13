using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ManageSession
/// </summary>
public class ManageSession
{
    /// <summary>
    /// session variable to store logged-in user's id.
    /// </summary>
    public static User User
    {
        get
        {
            return HttpContext.Current.Session["UserInfo"] != null ? (User)HttpContext.Current.Session["UserInfo"] : null;
        }
        set { HttpContext.Current.Session["UserInfo"] = value; }
    }

    ///// <summary>
    ///// session variable to store logged-in user's id.
    ///// </summary>
    //public static Physician Physician
    //{
    //    get
    //    {
    //        return HttpContext.Current.Session["Physician"] != null ? (Physician)HttpContext.Current.Session["Physician"] : null;
    //    }
    //    set { HttpContext.Current.Session["Physician"] = value; }
    //}
}