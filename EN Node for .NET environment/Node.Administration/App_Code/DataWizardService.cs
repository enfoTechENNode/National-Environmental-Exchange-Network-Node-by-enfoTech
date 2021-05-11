using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Xml;
using System.Web;
using System.Data;
using System.Configuration;

using Node.Core.Data;
using Node.Core;
using Node.Core.Biz.Objects;
using Node.Core.Data.Interfaces;
using Node.Lib.Data;
using System.IO;

// NOTE: If you change the class name "DataWizardService" here, you must also update the reference to "DataWizardService" in Web.config.
public class DataWizardService : IDataWizardService
{
    #region IDataWizardService Members

    public string GetOperationData(int value)
    {
        DBManager dbMgr = new DBManager();
        Operation op = dbMgr.GetOperationsDB().GetOperation(value);
        if (op != null)
            return op.Config.OuterXml;
        return "";
    }

    public string GetUploadFileList(string filetype)
    {
        DBManager dbMgr = new DBManager();
        IConfigurations configDb = dbMgr.GetConfigurationsDB();
        DataTable dt = configDb.GetConfigNames(filetype);

        DataSet ds = new DataSet();
        ds.Tables.Add(dt);
        return ds.GetXml();
    }

    public DataTable GetUploadFileTable(string filetype)
    {
        DBManager dbMgr = new DBManager();
        IConfigurations configDb = dbMgr.GetConfigurationsDB();
        return configDb.GetConfigNames(filetype);
    }

    public string GetFileContent(string filename, string filetype)
    {
        DBManager dbMgr = new DBManager();
        IConfigurations configDb = dbMgr.GetConfigurationsDB();
        return configDb.GetConfig(filename, filetype);
    }

    public string GetFileTypes()
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        dt.Columns.Add("FileType");
        ds.Tables.Add(dt);

        dt.Rows.Add(new object[] { Phrase.CONFIG_TYPE_COMPOSE });
        dt.Rows.Add(new object[] { Phrase.CONFIG_TYPE_COMPOSE_TEMPLATE });
        dt.Rows.Add(new object[] { Phrase.CONFIG_TYPE_POPULATE });
        dt.Rows.Add(new object[] { Phrase.CONFIG_TYPE_RULE });
        dt.Rows.Add(new object[] { Phrase.CONFIG_TYPE_XSLT });

        return ds.GetXml();
    }

    public bool SetOperationData(int value, string config)
    {
        DBManager dbMgr = new DBManager();
        return dbMgr.GetOperationsDB().UpdateOperationConfig(value + "", config);
    }

    public bool SetUploadFile(int id, string filename, string filetype, byte[] content)
    {
        DBManager dbMgr = new DBManager();
        IConfigurations configDb = dbMgr.GetConfigurationsDB();
        return configDb.SaveConfig(id + "", filename, filetype, ConverByteToString(content));
    }

    public int AddUploadFile(string filename, string filetype, byte[] content)
    {
        DBManager dbMgr = new DBManager();
        IConfigurations configDb = dbMgr.GetConfigurationsDB();
        return configDb.AddConfig(filename, filetype, ConverByteToString(content));
    }

    public bool DeleteUploadFile(int id)
    {
        DBManager dbMgr = new DBManager();
        IConfigurations configDb = dbMgr.GetConfigurationsDB();
        return configDb.DeleteConfig(id + "");
    }

    public string GetFileID(string filename, string filetype)
    {
        DBManager dbMgr = new DBManager();
        IConfigurations configDb = dbMgr.GetConfigurationsDB();
        return configDb.GetConfigID(filename, filetype);
    }

    public bool TestDbConnection(string dbStr)
    {
        if (dbStr == null || dbStr.Trim() == String.Empty)
            return false;

        string[] ss = dbStr.Split(":".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
        if (ss.Length != 2)
            return false;

        string provider = "";
        if (ss[0].Trim().ToLower() == "mssql")
            provider = DBAdapter.MSSQL_Provider;
        else if (ss[0].Trim().ToLower() == "oracle")
            provider = DBAdapter.Oracle_Provider;
        else
            provider = DBAdapter.OleDB_Provider;

        try
        {
            DBAdapter db = new DBAdapter(provider, ss[1]);
            db.Open();
            db.Close();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public string GetReturnURL()
    {
        string sResult = "";
        if (ConfigurationManager.AppSettings["DataWizardReturnURL"] != null &&
            ConfigurationManager.AppSettings["DataWizardReturnURL"].ToString() != "")
            sResult = ConfigurationManager.AppSettings["DataWizardReturnURL"].ToString();
        return sResult;
    }


    private string ConverByteToString(byte[] content)
    {
        string output = "";

        MemoryStream ms = new MemoryStream(content);
        ms.Position = 0;
        StreamReader sr = new StreamReader(ms, true);
        output = sr.ReadToEnd();
        ms.Close();
        sr.Close();

        return output;
    }

    #endregion
}
