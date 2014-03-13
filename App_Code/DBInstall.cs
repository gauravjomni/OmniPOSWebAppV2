using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Threading;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management;
using Microsoft.SqlServer.Management.Common;
using System.IO;

public class DBInstall
{
    public bool SqlServerDatabaseExists(string connectionString)
    {
        try
        {
            //just try to connect
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
            }
            return true;
        }
        catch
        {
            return false;
        }
    }

    public void CreateDBCredentails(string connectionString, string databaseName, string userName, string password)
    {
        string strScriptPath = AppDomain.CurrentDomain.BaseDirectory.ToString() + System.Configuration.ConfigurationManager.AppSettings["ScriptDBUser"];

        FileInfo file = new FileInfo(strScriptPath);
        string strscript = file.OpenText().ReadToEnd();
        string strupdatescript = strscript.Replace("DBNAME", databaseName);
        strupdatescript = strupdatescript.Replace("DBUserName", userName);
        strupdatescript = strupdatescript.Replace("DBPassword", password);

        var builder = new SqlConnectionStringBuilder(connectionString);

        builder.InitialCatalog = "master";
        var masterCatalogConnectionString = builder.ToString();

        using (var conn = new SqlConnection(masterCatalogConnectionString))
        {

            Server server = new Server(new ServerConnection(conn));
            server.ConnectionContext.ExecuteNonQuery(strupdatescript);

        }

    }
    public string CreateDatabase(string connectionString, string collation, string databaseName)
    {
        try
        {
            //parse database name
            var builder = new SqlConnectionStringBuilder(connectionString);

            builder.InitialCatalog = "master";
            var masterCatalogConnectionString = builder.ToString();
            string query = string.Format("if not Exists (SELECT name FROM master.dbo.sysdatabases WHERE name = '{0}') begin CREATE DATABASE [{0}] COLLATE SQL_Latin1_General_CP1_CI_AS end", databaseName);
            if (!String.IsNullOrWhiteSpace(collation))
               // query = string.Format("{0} COLLATE {1}", query, collation);
            using (var conn = new SqlConnection(masterCatalogConnectionString))
            {
                conn.Open();
                using (var command = new SqlCommand(query, conn))
                {
                    command.ExecuteNonQuery();
                }
            }

            return string.Empty;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void DropDatabase(string connectionString, string databaseName)
    {

        try
        {
            //parse database name
            var builder = new SqlConnectionStringBuilder(connectionString);

            builder.InitialCatalog = "master";
            var masterCatalogConnectionString = builder.ToString();
            string query = string.Format("DROP DATABASE [{0}]", databaseName);

            using (var conn = new SqlConnection(masterCatalogConnectionString))
            {
                conn.Open();
                using (var command = new SqlCommand(query, conn))
                {
                    command.ExecuteNonQuery();
                }
            }


        }
        catch (Exception ex)
        {
            // throw ex;
        }

    }


    public string CreateConnectionString(bool trustedConnection, string serverName, string databaseName, string userName, string password, int timeout = 0)
    {
        var builder = new SqlConnectionStringBuilder();
        builder.IntegratedSecurity = trustedConnection;
        builder.DataSource = serverName;
        builder.InitialCatalog = databaseName;
        if (!trustedConnection)
        {
            builder.UserID = userName;
            builder.Password = password;
        }
        builder.PersistSecurityInfo = false;
        builder.MultipleActiveResultSets = true;
        if (timeout > 0)
        {
            builder.ConnectTimeout = timeout;
        }
        return builder.ConnectionString;
    }


    public void CreateSchema(string databaseName, string connectionString)
    {
        //parse database name

        string strScriptPath = AppDomain.CurrentDomain.BaseDirectory.ToString() + System.Configuration.ConfigurationManager.AppSettings["ScriptPath"];
        var builder = new SqlConnectionStringBuilder(connectionString);
        builder.InitialCatalog = databaseName;
        var masterCatalogConnectionString = builder.ToString();
        FileInfo file = new FileInfo(strScriptPath);
        string strscript = file.OpenText().ReadToEnd();
        string strupdatescript = strscript.Replace("[V2_OmniPOS]", databaseName);
        //strupdatescript = strupdatescript.Replace("GO", "");
        using (var conn = new SqlConnection(masterCatalogConnectionString))
        {
            //  conn.Open();
            Server server = new Server(new ServerConnection(conn));
            server.ConnectionContext.ExecuteNonQuery(strupdatescript);

        }

    }

    //public void InstallDataBase()
    //{
    //    string connectionString = null;
    //    var sqlCsb = new SqlConnectionStringBuilder(connectionString);
    //    sqlCsb.MultipleActiveResultSets = true;
    //    connectionString = sqlCsb.ToString();
    //    if (!SqlServerDatabaseExists(connectionString))
    //    {
    //        //create database
    //        var collation = "SQL_Latin1_General_CP1_CI_AS";
    //        //var errorCreatingDatabase = null; //= CreateDatabase(connectionString, collation);
    //        //if (!String.IsNullOrEmpty(errorCreatingDatabase))
    //        //    throw new Exception(errorCreatingDatabase);
    //        //else
    //        //{
    //        //    Thread.Sleep(3000);
    //        //}
    //    }

    //}


}

