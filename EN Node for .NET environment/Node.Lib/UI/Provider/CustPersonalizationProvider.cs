using System;
using System.Configuration.Provider;
using System.Security.Permissions;
using System.Web;
using System.Web.UI.WebControls.WebParts;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Data;

using Node.Lib.Data;

/*

This provider works with the following schema for the table of user data.

#
# Table structure for table profiles
#

CREATE TABLE sys_personalization (
  username varchar(255) default NULL,
  path varchar(255) default NULL,
  applicationname varchar(255) default NULL,
  personalizationblob image default NULL
)

CREATE TABLE SYS_PERSONALIZATION (
  username varchar(255) default NULL,
  path varchar(255) default NULL,
  applicationname varchar(255) default NULL,
  personalizationblob blob  default NULL
)

*/

namespace Node.Lib.UI.Provider
{
    public class CustPersonalizationProvider : PersonalizationProvider
    {
        private string m_ApplicationName;
        public override string ApplicationName
        {
            get { return m_ApplicationName; }
            set { m_ApplicationName = value; }
        }

        private string m_ConnectionStringName;

        public string ConnectionStringName
        {
            get { return m_ConnectionStringName; }
            set { m_ConnectionStringName = value; }
        }

        public override void Initialize(string name,
            NameValueCollection config)
        {
            // Verify that config isn't null
            if (config == null)
                throw new ArgumentNullException("config");

            // Assign the provider a default name if it doesn't have one
            if (String.IsNullOrEmpty(name))
                name = "CustPersonalizationProvider";

            // Add a default "description" attribute to config if the
            // attribute doesn't exist or is empty
            if (string.IsNullOrEmpty(config["description"]))
            {
                config.Remove("description");
                config.Add("description",
                    "Custom personalization provider");
            }

            // Call the base class's Initialize method
            base.Initialize(name, config);

            if (string.IsNullOrEmpty(config["connectionStringName"]))
            {
                throw new ProviderException
                    ("ConnectionStringName property has not been specified");
            }
            else
            {
                m_ConnectionStringName = config["connectionStringName"];
                config.Remove("connectionStringName");
            }

            if (string.IsNullOrEmpty(config["applicationName"]))
            {
                throw new ProviderException
                    ("applicationName property has not been specified");
            }
            else
            {
                m_ApplicationName = config["applicationName"];
                config.Remove("applicationName");
            }

            // Throw an exception if unrecognized attributes remain
            if (config.Count > 0)
            {
                string attr = config.GetKey(0);
                if (!String.IsNullOrEmpty(attr))
                    throw new ProviderException
                        ("Unrecognized attribute: " + attr);
            }

        }

        protected override void LoadPersonalizationBlobs
            (WebPartManager webPartManager, string path, string userName,
            ref byte[] sharedDataBlob, ref byte[] userDataBlob)
        {
            // Load shared state
            sharedDataBlob = null;
            userDataBlob = null;
            object sharedBlobDataObject = null;
            object userBlobDataObject = null;
            string sSQLShared = null;
            string sSQLUser = null;
            DBAdapter db = new DBAdapter(m_ConnectionStringName);
            DataSet ds = new DataSet();
            try
            {
                sSQLUser = "SELECT personalizationblob FROM SYS_PERSONALIZATION" + Environment.NewLine +
                    "WHERE username = '" + userName + "' AND " + Environment.NewLine +
                    "path = '" + path + "' AND " + Environment.NewLine +
                    "applicationname = '" + m_ApplicationName + "'";
                sSQLShared = "SELECT personalizationblob FROM SYS_PERSONALIZATION" + Environment.NewLine +
                    "WHERE username IS NULL AND " + Environment.NewLine +
                    "path = '" + path + "' AND " + Environment.NewLine +
                    "applicationname = '" + m_ApplicationName + "'";

                db.GetDataSet("SYS_PERSONALIZATION_Shared", sSQLShared, ds);
                db.GetDataSet("SYS_PERSONALIZATION_User", sSQLUser, ds);

                if (ds.Tables["SYS_PERSONALIZATION_Shared"].Rows.Count > 0)
                    sharedBlobDataObject = ds.Tables["SYS_PERSONALIZATION_Shared"].Rows[0][0];
                if (ds.Tables["SYS_PERSONALIZATION_User"].Rows.Count > 0)
                    sharedBlobDataObject = ds.Tables["SYS_PERSONALIZATION_User"].Rows[0][0];

                if (sharedBlobDataObject != null)
                    sharedDataBlob =
                        (byte[])sharedBlobDataObject;
                if (userBlobDataObject != null)
                    userDataBlob =
                        (byte[])userBlobDataObject;
            }
            catch (FileNotFoundException)
            {
                // Not an error if file doesn't exist
            }
            finally
            {

                sSQLUser = null;
                sSQLShared = null;
                if (db != null)
                    db.Close();
            }
        }

        protected override void ResetPersonalizationBlob
            (WebPartManager webPartManager, string path, string userName)
        {
            // Delete the specified personalization file
            string sSQL = null;
            DBAdapter db = new DBAdapter(m_ConnectionStringName);
            try
            {
                sSQL = "DELETE FROM SYS_PERSONALIZATION WHERE username = '" + userName + "' AND path = '" + path + "' AND applicationname = '" + m_ApplicationName + "'";

                db.ExecuteNonQuery(sSQL);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (db != null)
                    db.Close();
            }
        }

        protected override void SavePersonalizationBlob
            (WebPartManager webPartManager, string path, string userName,
            byte[] dataBlob)
        {
            string sSQL = "SELECT * FROM SYS_PERSONALIZATION WHERE username = '" + userName + "' AND path = '" + path + "' and applicationname = '" + m_ApplicationName + "'";
            DBAdapter db = new DBAdapter(m_ConnectionStringName);
            DataSet ds = new DataSet();
            try
            {
                db.GetDataSet("SYS_PERSONALIZATION", sSQL, ds);
                DataTable pTable = ds.Tables[0];

                if (pTable.Rows.Count > 0)
                {
                    pTable.Rows[0]["personalizationblob"] = dataBlob;
                }
                else
                {
                    DataRow newRow = pTable.NewRow();
                    newRow["username"] = userName;
                    newRow["path"] = path;
                    newRow["applicationname"] = m_ApplicationName;
                    newRow["personalizationblob"] = dataBlob;
                    pTable.Rows.Add(newRow);
                }
                db.UpdateDataSet("SYS_PERSONALIZATION", ds);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (db != null)
                    db.Close();
            }

        }

        public override PersonalizationStateInfoCollection FindState
            (PersonalizationScope scope, PersonalizationStateQuery query,
            int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotSupportedException();
        }

        public override int GetCountOfState(PersonalizationScope scope,
            PersonalizationStateQuery query)
        {
            throw new NotSupportedException();
        }

        public override int ResetState(PersonalizationScope scope,
            string[] paths, string[] usernames)
        {
            throw new NotSupportedException();
        }

        public override int ResetUserState(string path,
            DateTime userInactiveSinceDate)
        {
            throw new NotSupportedException();
        }
    }

}
