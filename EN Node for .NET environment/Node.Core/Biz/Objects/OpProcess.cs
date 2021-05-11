using System;
using System.Collections;

namespace Node.Core.Biz.Objects
{
    /// <summary>
    /// opProcess Class is stored Plug-In Operation Informatuion.
    /// </summary>
    public class OpProcess
    {
        #region Public Properties

        /// <summary>
        /// Stored PlugIn PlugIn Class ProcessType.
        /// </summary>
        public ProcessType Type
        {
            get { return this.type; }
            set { this.type = value; }
        }
        /// <summary>
        /// Stored Name of PlugIn Class. 
        /// </summary>
        public string ClassName
        {
            get { return this.className; }
            set
            {
                string input = value;
                if (value == null || value.Trim().Equals(""))
                    input = "";
                this.className = input;
            }
        }
        /// <summary>
        /// Stored PDLL location of PlugIn Class.
        /// </summary>
        public string DllPath
        {
            get { return this.dllPath; }
            set
            {
                string input = value;
                if (value == null || value.Trim().Equals(""))
                    input = "";
                this.dllPath = input;
            }
        }
        /// <summary>
        /// Stored Sequence of Plug-In Class.
        /// </summary>
        public int Sequence
        {
            get { return this.sequence; }
            set { this.sequence = value; }
        }
        /// <summary>
        /// Stored WebService Name of PlugIn Class.
        /// </summary>
        public string WebServiceName
        {
            get { return this.wsName; }
            set
            {
                switch (value)
                {
                    case Phrase.WEB_SERVICE_AUTHENTICATE:
                        this.wsName = value;
                        break;
                    case Phrase.WEB_SERVICE_DOWNLOAD:
                        this.wsName = value;
                        break;
                    case Phrase.WEB_SERVICE_GETSERVICES:
                        this.wsName = value;
                        break;
                    case Phrase.WEB_SERVICE_GETSTATUS:
                        this.wsName = value;
                        break;
                    case Phrase.WEB_SERVICE_NODEPING:
                        this.wsName = value;
                        break;
                    case Phrase.WEB_SERVICE_NOTIFY:
                        this.wsName = value;
                        break;
                    case Phrase.WEB_SERVICE_QUERY:
                        this.wsName = value;
                        break;
                    case Phrase.WEB_SERVICE_SOLICIT:
                        this.wsName = value;
                        break;
                    case Phrase.WEB_SERVICE_SUBMIT:
                        this.wsName = value;
                        break;
                    default:
                        this.wsName = null;
                        break;
                }
            }
        }
        /// <summary>
        /// Stored PlugIn Class's indicator for Solicit restricted to time interval.
        /// </summary>
        public bool IsSolicitRestrictedToTimeInterval
        {
            get { return this.solRestrictedTime; }
            set { this.solRestrictedTime = value; }
        }
        /// <summary>
        /// Stored Solicit Start Time for PlugIn Class.
        /// </summary>
        public string SolicitStartTime
        {
            get { return this.solStart.ToString("HH:mm"); }
            set
            {
                if (value != null && !value.Trim().Equals(""))
                    this.solStart = DateTime.Parse(value);
                else
                    this.solStart = DateTime.MinValue;
            }
        }
        /// <summary>
        /// Stored Solicit End Time for PlugIn Class.
        /// </summary>
        public string SolicitEndTime
        {
            get { return this.solEnd.ToString("HH:mm"); }
            set
            {
                if (value != null && !value.Trim().Equals(""))
                    this.solEnd = DateTime.Parse(value);
                else
                    this.solEnd = DateTime.MaxValue;
            }
        }
        /// <summary>
        /// Stored PlugIn Class's indicator for Solicit Submit Credentials.
        /// </summary>
        public bool HasSolicitSubmitCredentials
        {
            get { return this.solSubmitCredentials; }
            set { this.solSubmitCredentials = value; }
        }
        /// <summary>
        /// Stored Solicit/Submit User ID for PlugIn Class.
        /// </summary>
        public string SolicitSubmitUID
        {
            get { return this.solSubmitUID; }
            set
            {
                string input = value;
                if (value == null || value.Trim().Equals(""))
                    input = "";
                this.solSubmitUID = input;
            }
        }
        /// <summary>
        /// Stored Solicit/Submit Password for PlugIn Class.
        /// </summary>
        public string SolicitSubmitPWD
        {
            get { return this.solSubmitPWD; }
            set
            {
                string input = value;
                if (value == null || value.Trim().Equals(""))
                    input = "";
                this.solSubmitPWD = input;
            }
        }
        /// <summary>
        /// Stored Solicit/Submit DataFlow Name for PlugIn Class.
        /// </summary>
        public string SolicitSubmitDataFlow
        {
            get { return this.solSubmitDataFlow; }
            set
            {
                string input = value;
                if (value == null || value.Trim().Equals(""))
                    input = "";
                this.solSubmitDataFlow = input;
            }
        }

        #endregion

        #region Public Constructors
        /// <summary>
        /// Constructor of OpProcess.
        /// </summary>
        /// <param name="type">The Process Type for the PlugIn Class.</param>
        /// <param name="className">The Class Name for the PlugIn Class.</param>
        /// <param name="dllPath">The Class Location for the PlugIn Class.</param>
        public OpProcess(ProcessType type, string className, string dllPath)
        {
            this.type = type;
            this.className = className;
            this.dllPath = dllPath;
        }
        /// <summary>
        /// Constructor of OpProcess.
        /// </summary>
        /// <param name="type">The Process Type for the PlugIn Class.</param>
        /// <param name="className">The Class Name for the PlugIn Class.</param>
        /// <param name="dllPath">The Class Location for the PlugIn Class.</param>
        /// <param name="wsName">The web services name for the PlugIn Class.</param>
        public OpProcess(ProcessType type, string className, string dllPath, string wsName)
        {
            this.type = type;
            this.className = className;
            this.dllPath = dllPath;
            this.wsName = wsName;
        }
        /// <summary>
        /// Constructor of OpProcess
        /// </summary>
        /// <param name="type">The Process Type for the PlugIn Class.</param>
        /// <param name="className">The Class Name for the PlugIn Class.</param>
        /// <param name="dllPath">The Class Location for the PlugIn Class.</param>
        /// <param name="wsName">The web services name for the PlugIn Class.</param>
        /// <param name="sequence">The sequence for the PlugIn Class.</param>
        public OpProcess(ProcessType type, string className, string dllPath, string wsName, int sequence)
        {
            this.type = type;
            this.className = className;
            this.dllPath = dllPath;
            this.sequence = sequence;
            this.wsName = wsName;
        }

        #endregion

        #region Private Fields

        private ProcessType type;
        private string className = null;
        private string dllPath = null;
        private int sequence = -1;
        private string wsName = null;

        private bool solRestrictedTime = false;
        private DateTime solStart;
        private DateTime solEnd;
        private bool solSubmitCredentials = false;
        private string solSubmitUID = null;
        private string solSubmitPWD = null;
        private string solSubmitDataFlow = null;

        #endregion
    }
}
