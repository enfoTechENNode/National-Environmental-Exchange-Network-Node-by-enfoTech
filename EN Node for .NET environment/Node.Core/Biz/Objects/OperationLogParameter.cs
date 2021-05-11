using System;
using System.Collections.Generic;
using System.Text;

namespace Node.Core.Biz.Objects
{
    /// <summary>
    /// OperationLogParameter Class retrieves Operation Log Parameter Informatuion.
    /// Database Source: NODE_OPERATION_LOG_PARAMETER
    /// </summary>
    public class OperationLogParameter
    {
        #region Public Properties
        /// <summary>
        /// Name of Parameter.
        /// </summary>
        public string ParameterName { get { return this.paramName; } set { this.paramName = value; } }
        /// <summary>
        /// Value of Parameter.
        /// </summary>
        public string ParameterValue { get { return this.paramValue; } set { this.paramValue = value; } }

        #endregion

        #region Public Constructor

        /// <summary>
        /// Constructor or OperationLogParameter.
        /// </summary>
        public OperationLogParameter()
        {
        }

        #endregion

        #region Private Fields

        private string paramName = null;
        private string paramValue = null;

        #endregion
    }
}
