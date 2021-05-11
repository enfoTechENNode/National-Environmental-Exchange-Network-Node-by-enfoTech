using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DataFlow.Component.Interface;

namespace Node.Core.API
{
    /// <summary>
    /// ActionParameter is defined as basic parameter type to commnincate between the IActionComponent.
    /// </summary>
    public class ActionParameter : IActionParameter
    {
        //***********************************************************************
        // Public Members
        //***********************************************************************
        #region Public Members

        #endregion

        //***********************************************************************
        // Private Members
        //***********************************************************************
        #region Private Members
        private string name;
        private object value;
        private string type;
        private ActionParameterDirection direction = ActionParameterDirection.None;

        #endregion

        //***********************************************************************
        // Constructors
        //***********************************************************************
        #region Constructors

        #endregion

        //***********************************************************************
        // Delegate Events
        //***********************************************************************
        #region Delegate Events

        #endregion

        //***********************************************************************
        // Public Properties
        //***********************************************************************
        #region Public Properties
        /// <summary>
        /// Name of ActionParameter.
        /// </summary>
        public string ParameterName
        {
            get { return this.name; }
            set { this.name = value; }
        }
        /// <summary>
        /// Value of ActionParameter.
        /// </summary>
        public object ParameterValue
        {
            get { return this.value; }
            set { this.value = value; }
        }
        /// <summary>
        /// Type of ActionParameter.
        /// </summary>
        public string ParameterType
        {
            get { return this.type; }
            set { this.type = value; }
        }
        /// <summary>
        /// Direction of ActionParameter.
        /// </summary>
        public ActionParameterDirection Direction
        {
            get { return this.direction; }
            set { this.direction = value; }
        }

        /// <summary>
        /// Gets or sets the DEDL Encoding value.
        /// </summary>
        public string DEDLEncoding { get; set; }

        /// <summary>
        /// Gets or sets the DEDL Occurence Number value.
        /// </summary>
        public string DEDLOccurenceNumber { get; set; }

        /// <summary>
        /// Gets or sets the DEDL type value.
        /// </summary>
        public string DEDLType { get; set; }

        /// <summary>
        /// Gets or sets the DEDL RequiredIndicator value.
        /// </summary>
        public string DEDLRequiredIndicator { get; set; }
        /// <summary>
        /// Gets or sets the DEDL RequiredIndicator value.
        /// </summary>
        public string DEDLTypeDescriptor { get; set; }


        #endregion

        //***********************************************************************
        // Protected Properties
        //***********************************************************************
        #region Protected Properties

        #endregion

        //***********************************************************************
        // Public Methods
        //***********************************************************************
        #region Public Methods

        #endregion

        //***********************************************************************
        // Protected Methods
        //***********************************************************************
        #region Protected Methods

        #endregion

        //***********************************************************************
        // Private Methods
        //***********************************************************************
        #region Private Methods

        #endregion

        //***********************************************************************
        // Internal Handlers
        //***********************************************************************
        #region Internal Handlers

        #endregion
    }
}
