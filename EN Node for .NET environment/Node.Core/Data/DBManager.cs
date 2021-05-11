//using EAF.Lib.Data;
using System;
using System.Configuration;

using Node.Core;
using Node.Core.Data.Interfaces;

namespace Node.Core.Data
{
    /// <summary>
    /// The database manager offer flexibility of switching different database providers(SQL and Oracle).
    /// </summary>
    public class DBManager
    {
        /// <summary>
        /// Constructor of DBManager.
        /// </summary>
        public DBManager()
        {
        }
        /// <summary>
        /// Get configuration related Database Object.
        /// </summary>
        /// <returns>IConfigurations</returns>
        public IConfigurations GetConfigurationsDB()
        {
            return new Node.Core.Data.SQLServer.Configurations();
        }
        /// <summary>
        /// Get operation manager related data access layer.
        /// </summary>
        /// <returns>IOperationManager</returns>
        public IOperationManager GetOperationManagerDB()
        {
            return new Node.Core.Data.SQLServer.OperationManager();
        }
        /// <summary>
        /// Get logging related Database Object.
        /// </summary>
        /// <returns>ILogging</returns>
        public ILogging GetLoggingDB()
        {
            return new Node.Core.Data.Common.Logging();
        }
        /// <summary>
        /// Get opeations related Database Object.
        /// </summary>
        /// <returns>IOperations</returns>
        public IOperations GetOperationsDB()
        {
            return new Node.Core.Data.SQLServer.Operations();
        }
        /// <summary>
        /// Get users related Database Object
        /// </summary>
        /// <returns>IUser</returns>
        public IUsers GetUsersDB()
        {
            return new Node.Core.Data.Common.Users();
        }
        /// <summary>
        /// Get documents related Database Object.
        /// </summary>
        /// <returns>IDocuments</returns>
        public IDocuments GetDocumentsDB()
        {
            return new Node.Core.Data.Common.Documents();
        }
        /// <summary>
        /// Get Services related Database Object.
        /// </summary>
        /// <returns>IGetServices</returns>
        public IGetServices GetGetServicesDB()
        {
            return new Node.Core.Data.Common.GetServices();
        }
        /// <summary>
        /// Get Operation Logs Database Object
        /// </summary>
        /// <returns>IOperationLogs</returns>
        public IOperationLogs GetOperationLogsDB()
        {
            return new Node.Core.Data.Common.OperationLogs();
        }
        /// <summary>
        /// Get Domains Database Object.
        /// </summary>
        /// <returns>IDomains</returns>
        public IDomains GetDomainsDB()
        {
            return new Node.Core.Data.Common.Domains();
        }

        /// <summary>
        /// Get Web Services Database Object.
        /// </summary>
        /// <returns>IWebServices</returns>
        public IWebServices GetWebServicesDB()
        {
            return new Node.Core.Data.Common.WebServices();
        }
    }
}
