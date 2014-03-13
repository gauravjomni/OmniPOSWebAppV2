using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Common
/// </summary>
public static class Generic
{
    #region GetInt
    /// <summary>
    /// Get numeric value to validate the number data
    /// </summary>
    /// <param name="strInt">String Value</param>
    /// <returns>Return numeric value if valid otherwise 0</returns>
    public static int GetInt(this string strInt)
    {
        int outInt;
        int.TryParse(strInt, out outInt);
        return outInt;
    }
    #endregion

    #region GetApplicationPath
    /// <summary>
    /// Get Application Path
    /// </summary>
    /// <returns>Application Path</returns>
    public static string GetApplicationPath()
    {
        string strAppPath = Convert.ToString(HttpContext.Current.Request.ApplicationPath);
        try
        {
            if (strAppPath == "/")
                strAppPath = string.Empty;

            return strAppPath;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    #endregion
}