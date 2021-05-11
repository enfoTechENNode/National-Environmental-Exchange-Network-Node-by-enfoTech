using System;
using System.Collections;
using System.Data;

using Node.Core.Data;

namespace Node.Core.Biz.Objects
{
    /// <summary>
    /// Domain Class retrieves Domain Informatuion.
    /// Database Source: NODE_DOMAIN.
    /// </summary>
    public class Domain
    {
        #region Public Properties
        /// <summary>
        /// identifier for Domain.
        /// </summary>
        public int ID
        {
            get { return this.id; }
            set { this.id = value; }
        }
        /// <summary>
        /// Name of Domain.
        /// </summary>
        public string Name
        {
            get { return this.name; }
            set
            {
                string input = value;
                if (value != null && value.Trim().Equals(""))
                    input = null;
                else if (value != null && value.Length > 50)
                    input = value.Substring(0, 50);
                this.name = input;
            }
        }
        /// <summary>
        /// Status of Domain.
        /// </summary>
        public string Status
        {
            get { return this.status; }
            set
            {
                string input = value;
                if (value != null && value.Trim().Equals(""))
                    input = null;
                else if (value != null && value.Length > 10)
                    input = value.Substring(0, 10);
                this.status = input;
            }
        }
        /// <summary>
        /// Security right to submit.
        /// </summary>
        public bool AllowSubmit
        {
            get { return (bool)this.allWS[Domain.SUBMIT_KEY]; }
            set
            {
                this.allWS.Remove(Domain.SUBMIT_KEY);
                this.allWS.Add(Domain.SUBMIT_KEY, value);
            }
        }
        /// <summary>
        /// Security right to download
        /// </summary>
        public bool AllowDownload
        {
            get { return (bool)this.allWS[Domain.DOWNLOAD_KEY]; }
            set
            {
                this.allWS.Remove(Domain.DOWNLOAD_KEY);
                this.allWS.Add(Domain.DOWNLOAD_KEY, value);
            }
        }
        /// <summary>
        /// Security right to query.
        /// </summary>
        public bool AllowQuery
        {
            get { return (bool)this.allWS[Domain.QUERY_KEY]; }
            set
            {
                this.allWS.Remove(Domain.QUERY_KEY);
                this.allWS.Add(Domain.QUERY_KEY, value);
            }
        }
        /// <summary>
        /// Security right to solicit.
        /// </summary>
        public bool AllowSolicit
        {
            get { return (bool)this.allWS[Domain.SOLICIT_KEY]; }
            set
            {
                this.allWS.Remove(Domain.SOLICIT_KEY);
                this.allWS.Add(Domain.SOLICIT_KEY, value);
            }
        }
        /// <summary>
        /// Security right to notify.
        /// </summary>
        public bool AllowNotify
        {
            get { return (bool)this.allWS[Domain.NOTIFY_KEY]; }
            set
            {
                this.allWS.Remove(Domain.NOTIFY_KEY);
                this.allWS.Add(Domain.NOTIFY_KEY, value);
            }
        }
        /// <summary>
        /// Status Message for Domain.
        /// </summary>
        public string StatusMessage
        {
            get { return this.statusMsg; }
            set
            {
                string input = value;
                if (value != null && value.Trim().Equals(""))
                    input = null;
                else if (value != null && value.Length > 1000)
                    input = value.Substring(0, 1000);
                this.statusMsg = input;
            }
        }
        /// <summary>
        /// Description of Domain.
        /// </summary>
        public string Description
        {
            get { return this.description; }
            set
            {
                string input = value;
                if (value != null && value.Trim().Equals(""))
                    input = null;
                else if (value != null && value.Length > 100)
                    input = value.Substring(0, 100);
                this.description = input;
            }
        }
        /// <summary>
        /// A list of Domain Admin user id.
        /// </summary>
        public ArrayList AdminIDs
        {
            get { return this.adminIDs; }
            set
            {
                if (value != null)
                    this.adminIDs = value;
                else
                    this.adminIDs = new ArrayList();
            }
        }

        /// <summary>
        /// Created Date of Domain
        /// </summary>
        public DateTime CreatedDate
        {
            get { return this.createdDate; }
            set { this.createdDate = value; }
        }

        /// <summary>
        /// The Console User that Created this Domain
        /// </summary>
        public string CreatedBy
        {
            get { return this.createdBy; }
            set { this.createdBy = value; }
        }

        /// <summary>
        /// Updated Date of Domain
        /// </summary>
        public DateTime UpdatedDate
        {
            get { return this.updatedDate; }
            set { this.updatedDate = value; }
        }

        /// <summary>
        /// The Console user that Updated this Domain
        /// </summary>
        public string UpdatedBy
        {
            get { return this.updatedBy; }
            set { this.updatedBy = value; }
        }

        #endregion

        #region Public Constructors
        /// <summary>
        /// Constructor of Domain.
        /// </summary>
        public Domain()
        {
            this.allWS.Add(Domain.SUBMIT_KEY, false);
            this.allWS.Add(Domain.DOWNLOAD_KEY, false);
            this.allWS.Add(Domain.QUERY_KEY, false);
            this.allWS.Add(Domain.SOLICIT_KEY, false);
            this.allWS.Add(Domain.NOTIFY_KEY, false);
        }
        /// <summary>
        /// Constructor of Domain.
        /// </summary>
        /// <param name="name">Name of Domain</param>
        public Domain(string name)
        {
            this.name = name;
            this.allWS.Add(Domain.SUBMIT_KEY, false);
            this.allWS.Add(Domain.DOWNLOAD_KEY, false);
            this.allWS.Add(Domain.QUERY_KEY, false);
            this.allWS.Add(Domain.SOLICIT_KEY, false);
            this.allWS.Add(Domain.NOTIFY_KEY, false);
            this.Init(new DBManager().GetDomainsDB().GetDomain(this.name));
        }

        #endregion

        #region Public Static Methods
        /// <summary>
        /// Get a DataTable of Domains
        /// </summary>
        /// <param name="domainAdmin">LoginName of Domain Administrator</param>
        /// <returns>Columns: DOMAIN_ID, DOMAIN_NAME</returns>
        public static DataTable GetDomainsDropDownList(string domainAdmin)
        {
            DataTable dt =  new DBManager().GetDomainsDB().GetDomainDropDownList(domainAdmin);
            DataRow dr = dt.NewRow();
            dr["DOMAIN_ID"] = -1;
            dr["DOMAIN_NAME"] = "";
            dt.Rows.InsertAt(dr, 0);
            return dt;
        }

        /// <summary>
        /// Search the Node Database for Domains
        /// </summary>
        /// <param name="domainID">The id of the domain to return, null or empty string if not to be included in query</param>
        /// <param name="domainStatus">The status of the domain to return, null or empty string if not to be included in query</param>
        /// <param name="domainAdmin">The Domain Administrator who is logged into the system.</param>
        /// <returns>Columns: DOMAIN_ID, DOMAIN_NAME, DOMAIN_STATUS_CD, DOMAIN_STATUS_MSG</returns>
        public static DataTable SearchDomains(string domainID, string domainStatus, string domainAdmin)
        {
            return new DBManager().GetDomainsDB().GetDomainSearchGrid(domainID, domainStatus, domainAdmin);
        }

        #endregion

        #region Public Methods
        /// <summary>
        /// Save a Domain
        /// </summary>
        /// <param name="domainAdmin">The Domain Administrator logged into the System</param>
        public void Save(string domainAdmin)
        {
            new DBManager().GetDomainsDB().SaveDomain(this, domainAdmin);
        }

        #endregion

        #region Private Constants

        private const int SUBMIT_KEY = 0;
        private const int DOWNLOAD_KEY = 1;
        private const int QUERY_KEY = 2;
        private const int SOLICIT_KEY = 3;
        private const int NOTIFY_KEY = 4;

        #endregion

        #region Private Fields

        private int id = -1;
        private string name = null;
        private string status = null;
        private Hashtable allWS = new Hashtable();
        private string statusMsg = null;
        private string description = null;
        private ArrayList adminIDs = new ArrayList();
        private DateTime createdDate;
        private string createdBy = null;
        private DateTime updatedDate;
        private string updatedBy = null;

        #endregion

        #region Private Methods

        private void Init(Domain d)
        {
            if (d != null && d.id >= 0)
            {
                this.id = d.id;
                this.name = d.name;
                this.status = d.status;
                this.statusMsg = d.statusMsg;
                this.description = d.description;
                this.AllowSubmit = d.AllowSubmit;
                this.AllowDownload = d.AllowDownload;
                this.AllowQuery = d.AllowQuery;
                this.AllowSolicit = d.AllowSolicit;
                this.AllowNotify = d.AllowNotify;
                this.adminIDs = d.adminIDs;
                this.createdDate = d.createdDate;
                this.createdBy = d.createdBy;
                this.updatedDate = d.updatedDate;
                this.updatedBy = d.updatedBy;
            }
        }

        #endregion
    }
}
