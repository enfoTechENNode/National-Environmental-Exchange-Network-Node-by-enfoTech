using System;
using System.Collections.Generic;
using System.Text;

namespace Node.Core.Biz.Objects
{
    /// <summary>
    /// OpParameter Class is stored Plug-In Operation Parameter Information.
    /// </summary>
    public class OpParameter
    {
        #region Public Properties
        /// <summary>
        /// Name of Parameter.
        /// </summary>
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }
        /// <summary>
        /// Value of Parameter.
        /// </summary>
        public string Value
        {
            get { return this.value; }
            set { this.value = value; }
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
        /// Gets or sets the DEDL TypeDescriptor value.
        /// </summary>
        public string DEDLTypeDescriptor { get; set; }


        #endregion

        #region Public Constructors

        /// <summary>
        /// The Constructor of OpParameter.
        /// </summary>
        /// <param name="name">Name of the Parameter</param>

        public OpParameter(string name)
        {
            this.name = name;
        }
        /// <summary>
        /// The Constructor of OpParamerer.
        /// </summary>
        /// <param name="name">Name of Paramerer.</param>
        /// <param name="value">Value of Parameter.</param>
        public OpParameter(string name, string value)
        {
            this.name = name;
            this.value = value;
        }

        #endregion

        #region Private Fields

        private string name = null;
        private string value = null;

        



        #endregion
    }
}
