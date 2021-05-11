using System.Web.Profile;
using System.Configuration.Provider;
using System.Collections.Generic;
using System.Collections.Specialized;
using System;
using System.Data;
using System.Configuration;
using System.Diagnostics;
using System.Web;
using System.Collections;

using Node.Lib.Data;

/*

This provider works with the following schema for the table of user data.

#
# Table structure for table 'profiles'
#

CREATE TABLE sys_profiles (
  UniqueID int(8) NOT NULL auto_increment,
  Username varchar(255) NOT NULL default '',
  ApplicationName varchar(255) NOT NULL default '',
  IsAnonymous tinyint(1) default '0',
  LastActivityDate datetime default NULL,
  LastUpdatedDate datetime default NULL,
  PRIMARY KEY  (UniqueID),
  UNIQUE KEY PKProfiles (Username,ApplicationName),
  UNIQUE KEY PKID (UniqueID)
)


*/
namespace Node.Lib.UI.Provider
{
    public sealed class CustProfileProvider : ProfileProvider
    {
        //
        // Global connection string, generic exception message, event log info.
        //

        private string eventSource = "CustProfileProvider";
        private string eventLog = "Application";
        private string exceptionMessage = "An exception occurred. Please check the event log.";
        private string connectionString;


        //
        // If false, exceptions are thrown to the caller. If true,
        // exceptions are written to the event log.
        //

        private bool pWriteExceptionsToEventLog;

        public bool WriteExceptionsToEventLog
        {
            get { return pWriteExceptionsToEventLog; }
            set { pWriteExceptionsToEventLog = value; }
        }



        //
        // System.Configuration.Provider.ProviderBase.Initialize Method
        //

        public override void Initialize(string name, NameValueCollection config)
        {

            //
            // Initialize values from web.config.
            //

            if (config == null)
                throw new ArgumentNullException("config");

            if (name == null || name.Length == 0)
                name = "CustProfileProvider";

            if (String.IsNullOrEmpty(config["description"]))
            {
                config.Remove("description");
                config.Add("description", "Custom Profile provider");
            }

            // Initialize the abstract base class.
            base.Initialize(name, config);


            if (config["applicationName"] == null || config["applicationName"].Trim() == "")
            {
                pApplicationName = System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath;
            }
            else
            {
                pApplicationName = config["applicationName"];
            }


            //
            // Initialize connection string.
            //

            ConnectionStringSettings pConnectionStringSettings = ConfigurationManager.
                ConnectionStrings[config["connectionStringName"]];

            if (pConnectionStringSettings == null ||
                pConnectionStringSettings.ConnectionString.Trim() == "")
            {
                throw new ProviderException("Connection string cannot be blank.");
            }

            //connectionString = pConnectionStringSettings.ConnectionString;
            connectionString = pConnectionStringSettings.Name;

        }


        //
        // System.Configuration.SettingsProvider.ApplicationName
        //

        private string pApplicationName;

        public override string ApplicationName
        {
            get { return pApplicationName; }
            set { pApplicationName = value; }
        }



        //
        // System.Configuration.SettingsProvider methods.
        //

        //
        // SettingsProvider.GetPropertyValues
        //

        public override SettingsPropertyValueCollection
              GetPropertyValues(SettingsContext context,
                    SettingsPropertyCollection ppc)
        {
            string username = (string)context["UserName"];
            bool isAuthenticated = (bool)context["IsAuthenticated"];

            // The serializeAs attribute is ignored in this provider implementation.

            SettingsPropertyValueCollection svc =
                new SettingsPropertyValueCollection();

            foreach (SettingsProperty prop in ppc)
            {
                SettingsPropertyValue pv = new SettingsPropertyValue(prop);

                //switch (prop.Name)
                //{
                    // TO DO: If you want to add custom attributes to the user profile, implement them here
                    //
                    // Example on how to add a Theme attribute :
                    //case "Theme":
                    //    pv.PropertyValue = GetTheme(username, isAuthenticated);
                    //    break;
                    /*
                    case "ZipCode":
                        pv.PropertyValue = GetZipCode(username, isAuthenticated);
                        break;
                    case "Customers":
                        pv.PropertyValue = GetCustomers(username, isAuthenticated);
                        break; */
                //    default:
                //        throw new ProviderException("Unsupported property.");
                //}

                svc.Add(pv);
            }

            UpdateActivityDates(username, isAuthenticated, true);

            return svc;
        }


        //
        // SettingsProvider.SetPropertyValues
        //

        public override void SetPropertyValues(SettingsContext context,
                       SettingsPropertyValueCollection ppvc)
        {
            // The serializeAs attribute is ignored in this provider implementation.

            string username = (string)context["UserName"];
            bool isAuthenticated = (bool)context["IsAuthenticated"];
            int uniqueID = GetUniqueID(username, isAuthenticated, false);
            if (uniqueID == 0)
                uniqueID = CreateProfileForUser(username, isAuthenticated);

            foreach (SettingsPropertyValue pv in ppvc)
            {
                switch (pv.Property.Name)
                {
                    // TO DO: If you want to add custom attributes to the user profile, implement them here
                    //
                    // Example on how to add a Theme attribute :
                    //case "Theme":
                    //    SetTheme(uniqueID, (string)pv.PropertyValue);
                    //    break;
                    default:
                        throw new ProviderException("Unsupported property.");
                }
            }

            UpdateActivityDates(username, isAuthenticated, false);
        }

        //
        // UpdateActivityDates
        // Updates the LastActivityDate and LastUpdatedDate values 
        // when profile properties are accessed by the
        // GetPropertyValues and SetPropertyValues methods. 
        // Passing true as the activityOnly parameter will update
        // only the LastActivityDate.
        //

        private void UpdateActivityDates(string username, bool isAuthenticated, bool activityOnly)
        {
            DateTime activityDate = DateTime.Now;
            DBAdapter db = new DBAdapter(connectionString);
            string sSql = "";
            if (activityOnly)
            {
                sSql = "UPDATE SYS_PROFILES Set LastActivityDate = '" + activityDate.ToString("yyyy-MM-dd HH:mm:ss") + "' " +
                      "WHERE Username = '" + username + "' AND ApplicationName = '" + pApplicationName + "' AND IsAnonymous = " + GetIntFromBool(!isAuthenticated) + "";
            }
            else
            {
                sSql = "UPDATE SYS_PROFILES Set LastActivityDate = '" + activityDate.ToString("yyyy-MM-dd HH:mm:ss") + "', LastUpdatedDate = '" + activityDate.ToString("yyyy-MM-dd HH:mm:ss") + "' " +
                      "WHERE Username = '" + username + "' AND ApplicationName = '" + pApplicationName + "' AND IsAnonymous = " + GetIntFromBool(!isAuthenticated) + "";
            }
            try
            {
                db.ExecuteNonQuery(sSql);
            }
            catch (Exception ex)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(ex, "UpdateActivityDates");
                    throw new ProviderException(exceptionMessage);
                }
                else
                {
                    throw ex;
                }
            }
            finally
            {
                if (db != null)
                    db.Close();
            }


        }

        private int GetIntFromBool(bool p_Bool)
        {
            if (p_Bool)
                return 1;
            else
                return 0;
        }


        //private string GetTheme(string username, bool isAuthenticated)
        //{
        //    int iIsAuth = 0;
        //    if (isAuthenticated)
        //        iIsAuth = 1;
        //    OdbcConnection conn = new OdbcConnection(connectionString);
        //    OdbcCommand cmd = new
        //      OdbcCommand("SELECT Theme FROM Profiles " +
        //        "INNER JOIN Themes ON Profiles.UniqueID = Themes.UniqueID " +
        //        "WHERE Username = '" + username + "' AND ApplicationName = '" + pApplicationName + "' And IsAnonymous = " + iIsAuth + "", conn);
        //    string outList = "Default";

        //    OdbcDataReader reader = null;

        //    try
        //    {
        //        conn.Open();

        //        reader = cmd.ExecuteReader();

        //        while (reader.Read())
        //        {
        //            outList = reader.GetString(0);
        //        }
        //    }
        //    catch (MySqlException e)
        //    {
        //        if (WriteExceptionsToEventLog)
        //        {
        //            WriteToEventLog(e, "GetThemes");
        //            throw new ProviderException(exceptionMessage);
        //        }
        //        else
        //        {
        //            throw e;
        //        }
        //    }
        //    finally
        //    {
        //        if (reader != null) { reader.Close(); }

        //        conn.Close();
        //    }

        //    return outList;
        //}



        //
        // 
        // Example of setting the custom Theme attrinute value
        // 
        //
        //
        //private void SetTheme(int uniqueID, string p_Theme)
        //{
        //    string Theme = "";
        //    OdbcConnection conn = new OdbcConnection(connectionString);
        //    OdbcCommand cmd = new OdbcCommand("DELETE FROM Themes WHERE UniqueID = ?", conn);
        //    cmd.Parameters.Add("?UniqueID", OdbcType.Int).Value = uniqueID;

        //    OdbcCommand cmd2 = new OdbcCommand("INSERT INTO Themes (UniqueID, Theme) " +
        //               "Values(?, ?)", conn);
        //    cmd2.Parameters.Add("?UniqueID", OdbcType.Int).Value = uniqueID;
        //    cmd2.Parameters.Add("?Theme", OdbcType.VarChar, 10);

        //    MySqlTransaction tran = null;

        //    try
        //    {
        //        conn.Open();
        //        tran = conn.BeginTransaction();
        //        cmd.Transaction = tran;
        //        cmd2.Transaction = tran;

        //        // Delete any existing values;
        //        cmd.ExecuteNonQuery();
        //        cmd2.Parameters["?Theme"].Value = p_Theme;
        //        cmd2.ExecuteNonQuery();

        //        tran.Commit();
        //    }
        //    catch (MySqlException e)
        //    {
        //        try
        //        {
        //            tran.Rollback();
        //        }
        //        catch
        //        {
        //        }

        //        if (WriteExceptionsToEventLog)
        //        {
        //            WriteToEventLog(e, "SetTheme");
        //            throw new ProviderException(exceptionMessage);
        //        }
        //        else
        //        {
        //            throw e;
        //        }
        //    }
        //    finally
        //    {
        //        conn.Close();
        //    }
        //}

        //
        // GetUniqueID
        //   Retrieves the uniqueID from the database for the current user and application.
        //

        private int GetUniqueID(string username, bool isAuthenticated, bool ignoreAuthenticationType)
        {
            DBAdapter db = new DBAdapter(connectionString);

            string sSql = " SELECT UniqueID FROM SYS_PROFILES " +
                    " WHERE Username = '" + username +
                    "' AND ApplicationName = '" + ApplicationName + "'";

            int uniqueID = 0;

            if (!ignoreAuthenticationType)
            {
                if (isAuthenticated)
                    sSql += " AND IsAnonymous = 0";
                else
                    sSql += " AND IsAnonymous = 1";
            }
            try
            {
               
                System.Data.Common.DbDataReader dbReader =  db.ExecuteReader(sSql);
                if (dbReader.HasRows)
                    uniqueID = dbReader.GetInt32(0);
            }
            catch (Exception ex)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(ex, "GetUniqueID");
                    throw new ProviderException(exceptionMessage);
                }
                else
                {
                    throw ex;
                }
            }
            finally
            {
                if (db != null)
                    db.Close();
            }
            return uniqueID;

        }


        //
        // CreateProfileForUser
        // If no user currently exists in the database, 
        // a user record is created during
        // the call to the GetUniqueID private method.
        //

        private int CreateProfileForUser(string username, bool isAuthenticated)
        {
            // Check for valid user name.

            if (username == null)
                throw new ArgumentNullException("User name cannot be null.");
            if (username.Length > 255)
                throw new ArgumentException("User name exceeds 255 characters.");
            if (username.IndexOf(",") > 0)
                throw new ArgumentException("User name cannot contain a comma (,).");


            DBAdapter db = new DBAdapter(connectionString);
            DataSet ds = new DataSet();
            string sSql = "select * from SYS_PROFILES where 1<>1";
            int uniqueID = 0;
            try
            {
                db.GetDataSet("SYS_PROFILES", sSql, ds);
                DataTable dt = ds.Tables[0];
                DataRow newRow = dt.NewRow();
                newRow["Username"] = username;
                newRow["ApplicationName"] = pApplicationName;
                newRow["LastActivityDate"] = DateTime.Now;
                newRow["LastUpdatedDate"] = DateTime.Now;
                newRow["IsAnonymous"] = GetIntFromBool(!isAuthenticated);

                dt.Rows.Add(newRow);
                db.UpdateDataSet("sys_profiles", ds);
                uniqueID = (int)dt.Rows[0]["UniqueID"];
            }
            catch (Exception ex)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(ex, "CreateProfileForUser");
                    throw new ProviderException(exceptionMessage);
                }
                else
                {
                    throw ex;
                }
            }
            finally
            {
                if (db != null)
                    db.Close();
            }
            return uniqueID;

        }


        //
        // ProfileProvider.DeleteProfiles(ProfileInfoCollection)
        //

        public override int DeleteProfiles(ProfileInfoCollection profiles)
        {
            List<string> listUserName = new List<string>();
            try
            {
                foreach (ProfileInfo p in profiles)
                {
                    listUserName.Add(p.UserName);
                }

            }
            catch (Exception e)
            {

                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(e, "DeleteProfiles(ProfileInfoCollection)");
                    throw new ProviderException(exceptionMessage);
                }
                else
                {
                    throw e;
                }
            }
            finally
            {

            }

            return DeleteProfiles(listUserName.ToArray());
        }


        //
        // ProfileProvider.DeleteProfiles(String[])
        //

        public override int DeleteProfiles(String[] usernames)
        {
            int deleteCount = 0;

            DBAdapter db = new DBAdapter(connectionString);
            try
            {
                db.BeginTransaction();
                foreach (string user in usernames)
                {
                    int uniqueID = GetUniqueID(user, false, true);
                    string sSql = "DELETE * FROM SYS_PROFILES WHERE UniqueID = " + uniqueID;
                    db.ExecuteNonQuery(sSql);
                }
                db.CommitTransaction();
            }
            catch (Exception e)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(e, "DeleteProfiles(String())");
                    throw new ProviderException(exceptionMessage);
                }
                else
                {
                    throw e;
                }
            }
            finally
            {
                if (db != null)
                    db.Close();
            }

            return deleteCount;
        }


        //
        // ProfileProvider.DeleteInactiveProfiles
        //

        public override int DeleteInactiveProfiles(
          ProfileAuthenticationOption authenticationOption,
          DateTime userInactiveSinceDate)
        {
            DBAdapter db = new DBAdapter(connectionString);
            DataSet ds = new DataSet();
            string sSql = "SELECT Username FROM SYS_PROFILES " +
                    " WHERE ApplicationName = '" + ApplicationName + "'" +
                    " AND LastActivityDate <= '" + userInactiveSinceDate.ToShortDateString() + "'";

            switch (authenticationOption)
            {
                case ProfileAuthenticationOption.Anonymous:
                    sSql = sSql + " AND IsAnonymous = 1"; 
                    break;
                case ProfileAuthenticationOption.Authenticated:
                    sSql = sSql + " AND IsAnonymous = 0";
                    break;
                default:
                    break;
            }

            List<string> listUserName = new List<string>();
            try 
            {
                
                db.GetDataSet("SYS_PROFILES", sSql, ds);
                foreach (DataRow aRow in ds.Tables[0].Rows)
                {
                    listUserName.Add(""+aRow[0]);
                }
            }
            catch (Exception e)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(e, "DeleteInactiveProfiles");
                    throw new ProviderException(exceptionMessage);
                }
                else
                {
                    throw e;
                }
            }
            finally
            {
                if (db != null)
                    db.Close();
            }

            // Delete profiles.

            return DeleteProfiles(listUserName.ToArray());
        }


        //
        // DeleteProfile
        // Deletes profile data from the database for the 
        // specified user name.
        //

        //private bool DeleteProfile(string username, MySqlConnection conn, MySqlTransaction tran)
        //{
        //    // Check for valid user name.
        //    if (username == null)
        //        throw new ArgumentNullException("User name cannot be null.");
        //    if (username.Length > 255)
        //        throw new ArgumentException("User name exceeds 255 characters.");
        //    if (username.IndexOf(",") > 0)
        //        throw new ArgumentException("User name cannot contain a comma (,).");


        //    int uniqueID = GetUniqueID(username, false, true);

        //    MySqlCommand cmd1 = new MySqlCommand("DELETE * FROM ProfileData WHERE UniqueID = ?UniqueID ", conn);
        //    cmd1.Parameters.Add("?UniqueID", MySqlDbType.Int32).Value = uniqueID;
        //    MySqlCommand cmd2 = new MySqlCommand("DELETE * FROM Themes WHERE UniqueID = ?UniqueID ", conn);
        //    cmd2.Parameters.Add("?UniqueID", MySqlDbType.Int32).Value = uniqueID;
        //    MySqlCommand cmd3 = new MySqlCommand("DELETE * FROM Profiles WHERE UniqueID = ?UniqueID ", conn);
        //    cmd3.Parameters.Add("?UniqueID", MySqlDbType.Int32).Value = uniqueID;

        //    cmd1.Transaction = tran;
        //    cmd2.Transaction = tran;
        //    cmd3.Transaction = tran;

        //    int numDeleted = 0;

        //    // Exceptions will be caught by the calling method.
        //    numDeleted += cmd1.ExecuteNonQuery();
        //    numDeleted += cmd2.ExecuteNonQuery();
        //    numDeleted += cmd3.ExecuteNonQuery();

        //    if (numDeleted == 0)
        //        return false;
        //    else
        //        return true;
        //}


        //
        // ProfileProvider.FindProfilesByUserName
        //

        public override ProfileInfoCollection FindProfilesByUserName(
          ProfileAuthenticationOption authenticationOption,
          string usernameToMatch,
          int pageIndex,
          int pageSize,
          out int totalRecords)
        {
            CheckParameters(pageIndex, pageSize);

            return GetProfileInfo(authenticationOption, usernameToMatch,
                null, pageIndex, pageSize, out totalRecords);
        }


        //
        // ProfileProvider.FindInactiveProfilesByUserName
        //

        public override ProfileInfoCollection FindInactiveProfilesByUserName(
          ProfileAuthenticationOption authenticationOption,
          string usernameToMatch,
          DateTime userInactiveSinceDate,
          int pageIndex,
          int pageSize,
          out int totalRecords)
        {
            CheckParameters(pageIndex, pageSize);

            return GetProfileInfo(authenticationOption, usernameToMatch, userInactiveSinceDate,
                  pageIndex, pageSize, out totalRecords);
        }


        //
        // ProfileProvider.GetAllProfiles
        //

        public override ProfileInfoCollection GetAllProfiles(
          ProfileAuthenticationOption authenticationOption,
          int pageIndex,
          int pageSize,
          out int totalRecords)
        {
            CheckParameters(pageIndex, pageSize);

            return GetProfileInfo(authenticationOption, null, null,
                  pageIndex, pageSize, out totalRecords);
        }


        //
        // ProfileProvider.GetAllInactiveProfiles
        //

        public override ProfileInfoCollection GetAllInactiveProfiles(
          ProfileAuthenticationOption authenticationOption,
          DateTime userInactiveSinceDate,
          int pageIndex,
          int pageSize,
          out int totalRecords)
        {
            CheckParameters(pageIndex, pageSize);

            return GetProfileInfo(authenticationOption, null, userInactiveSinceDate,
                  pageIndex, pageSize, out totalRecords);
        }



        //
        // ProfileProvider.GetNumberOfInactiveProfiles
        //

        public override int GetNumberOfInactiveProfiles(
          ProfileAuthenticationOption authenticationOption,
          DateTime userInactiveSinceDate)
        {
            int inactiveProfiles = 0;

            ProfileInfoCollection profiles =
              GetProfileInfo(authenticationOption, null, userInactiveSinceDate,
                  0, 0, out inactiveProfiles);

            return inactiveProfiles;
        }



        //
        // CheckParameters
        // Verifies input parameters for page size and page index. 
        // Called by GetAllProfiles, GetAllInactiveProfiles, 
        // FindProfilesByUserName, and FindInactiveProfilesByUserName.
        //

        private void CheckParameters(int pageIndex, int pageSize)
        {
            if (pageIndex < 0)
                throw new ArgumentException("Page index must 0 or greater.");
            if (pageSize < 1)
                throw new ArgumentException("Page size must be greater than 0.");
        }


        //
        // GetProfileInfo
        // Retrieves a count of profiles and creates a 
        // ProfileInfoCollection from the profile data in the 
        // database. Called by GetAllProfiles, GetAllInactiveProfiles,
        // FindProfilesByUserName, FindInactiveProfilesByUserName, 
        // and GetNumberOfInactiveProfiles.
        // Specifying a pageIndex of 0 retrieves a count of the results only.
        //

        private ProfileInfoCollection GetProfileInfo(
          ProfileAuthenticationOption authenticationOption,
          string usernameToMatch,
          object userInactiveSinceDate,
          int pageIndex,
          int pageSize,
          out int totalRecords)
        {

            DBAdapter db = new DBAdapter(connectionString);
           
            string sSql =  " SELECT Username, LastActivityDate, LastUpdatedDate, IsAnonymous "+
                            " FROM SYS_PROFILES WHERE ApplicationName ='" + ApplicationName + "'";


            // If searching for a user name to match, add the command text and parameters.

            if (usernameToMatch != null)
            {
                sSql = sSql + " AND Username LIKE '" + usernameToMatch + "'";
            }

            // If searching for inactive profiles, 
            // add the command text and parameters.

            if (userInactiveSinceDate != null)
            {
                sSql = sSql + " AND LastActivityDate <= '" + userInactiveSinceDate + "'";
            }

            // If searching for a anonymous or authenticated profiles,    
            // add the command text and parameters.

            switch (authenticationOption)
            {
                case ProfileAuthenticationOption.Anonymous:
                    sSql = sSql + " AND IsAnonymous = 1";
                    break;
                case ProfileAuthenticationOption.Authenticated:
                    sSql = sSql + " AND IsAnonymous = 0";
                    break;
                default:
                    break;
            }

            // Get the data.

            ProfileInfoCollection profiles = new ProfileInfoCollection();
            DataSet ds = new DataSet();
            try
            {
                db.GetDataSet("profile", sSql, ds);

                // Get the profile count.
                totalRecords = 0;
                if (ds.Tables["profile"].Rows.Count > 0)
                {
                    totalRecords = (int)ds.Tables["profilecount"].Rows.Count;
                }

                // No profiles found.
                if (totalRecords <= 0) { return profiles; }
                // Count profiles only.
                if (pageSize == 0) { return profiles; }

                DataTable dt = ds.Tables["profile"];

                foreach (DataRow aRow in dt.Rows)
                {
                    string sUserName = ""+aRow[0];
                    bool bisAnonymous = (int)aRow[3] == 1? true:false;
                    DateTime lastActDate = (DateTime)aRow[1];
                    DateTime lastUpdDate = (DateTime)aRow[2];
                    ProfileInfo p = new ProfileInfo(sUserName, bisAnonymous, lastActDate, lastUpdDate, 0);
                    profiles.Add(p);
                 }

            }
            catch (Exception e)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(e, "GetProfileInfo");
                    throw new ProviderException(exceptionMessage);
                }
                else
                {
                    throw e;
                }
            }
            finally
            {
                if (db != null)
                    db.Close();
            }

            return profiles;
        }

        //
        // WriteToEventLog
        // A helper function that writes exception detail to the event 
        // log. Exceptions are written to the event log as a security 
        // measure to prevent private database details from being 
        // returned to the browser. If a method does not return a 
        // status or Boolean value indicating whether the action succeeded 
        // or failed, the caller also throws a generic exception.
        //

        private void WriteToEventLog(Exception e, string action)
        {
            EventLog log = new EventLog();
            log.Source = eventSource;
            log.Log = eventLog;

            string message = "An exception occurred while communicating with the data source.\n\n";
            message += "Action: " + action + "\n\n";
            message += "Exception: " + e.ToString();

            log.WriteEntry(message);
        }
    }
}
