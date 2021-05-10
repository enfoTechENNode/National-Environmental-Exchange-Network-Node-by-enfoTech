using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;

// NOTE: If you change the interface name "IService" here, you must also update the reference to "IService" in Web.config.
[ServiceContract]
public interface IDataWizardService
{

    [OperationContract]
    string GetOperationData(int value);

    [OperationContract]
    string GetUploadFileList(string filetype);

    [OperationContract]
    string GetFileTypes();

    [OperationContract]
    string GetFileID(string filename, string filetype);

    [OperationContract]
    DataTable GetUploadFileTable(string filetype);

    [OperationContract]
    string GetFileContent(string filename, string filetype);

    [OperationContract]
    bool SetOperationData(int value, string config);

    [OperationContract]
    bool SetUploadFile(int id,string filename,string filetype,byte[] content);

    [OperationContract]
    bool DeleteUploadFile(int id);

    [OperationContract]
    int AddUploadFile(string filename, string filetype, byte[] content);

    [OperationContract]
    bool TestDbConnection(string dbStr);

    [OperationContract]
    string GetReturnURL();

    // TODO: Add your service operations here
}

// Use a data contract as illustrated in the sample below to add composite types to service operations.
//[DataContract]
//public class CompositeType
//{
//    bool boolValue = true;
//    string stringValue = "Hello ";

//    [DataMember]
//    public bool BoolValue
//    {
//        get { return boolValue; }
//        set { boolValue = value; }
//    }

//    [DataMember]
//    public string StringValue
//    {
//        get { return stringValue; }
//        set { stringValue = value; }
//    }

//}

