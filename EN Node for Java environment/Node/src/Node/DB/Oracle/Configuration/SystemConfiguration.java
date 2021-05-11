package Node.DB.Oracle.Configuration;

import com.enfotech.basecomponent.db.IDBAdapter;
import com.enfotech.basecomponent.typelib.xml.XmlDocument;
import com.enfotech.basecomponent.typelib.xml.XmlNodeList;
import com.enfotech.basecomponent.utility.security.Cryptography;
import com.enfotech.basecomponent.utility.xml.XmlUtility;
import java.sql.ResultSet;
import java.util.Hashtable;
import org.apache.log4j.Level;
import org.apache.log4j.Logger;

import Node.DB.Interfaces.Configuration.ISystemConfiguration;
import Node.DB.Oracle.NodeDB;
import Node.Phrase;
import Node.Utils.LoggingUtils;
import Node.Utils.Utility;
/**
 * <p>This class create SystemConfiguration.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class SystemConfiguration extends NodeDB implements ISystemConfiguration  {
  private String TableName = this.SysConfigTableName;
  private String ConfigName = this.SysConfigConfigName;
  private String ConfigXML = this.SysConfigConfigXML;

  private Hashtable hash = new Hashtable();

	/**
	 * Constructor.
	 * @param loggerName.
	 * @return 
	 */
  public SystemConfiguration(String loggerName) {
    super(loggerName);
    this.Init();
  }

	/**
	 * Init.
	 * @param .
	 * @return 
	 */
  private void Init ()
  {
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String sql = "select extractValue(" + this.ConfigXML + ",'/Configuration/Application/NodeStatus/Status/text()') NodeStatus ";
      sql += ", extractValue(" + this.ConfigXML + ",'/Configuration/NodeSettings/TokenLifeTime/@Enabled') TokenLifeTimeEnabled ";
      sql += ", extractValue(" + this.ConfigXML + ",'/Configuration/NodeSettings/TokenLifeTime/@time') TokenLifeTime ";
      sql += ", extractValue(" + this.ConfigXML + ",'/Configuration/NodeSettings/@name') NodeName ";
      sql += ", extractValue(" + this.ConfigXML + ",'/Configuration/Application/NodeStatus/Message/text()') NodeMessage ";
      sql += ", extractValue(" + this.ConfigXML + ",'/Configuration/NodeSettings/NAAS/URL/text()') NAASURL ";
      sql += ", extractValue(" + this.ConfigXML + ",'/Configuration/NodeSettings/NodeURL/text()') NodeURL ";
      sql += ", extractValue(" + this.ConfigXML + ",'/Configuration/NodeSettings/NodeAdministrator/Name/text()') NodeAdminName ";
      sql += ", extractValue(" + this.ConfigXML + ",'/Configuration/NodeSettings/NodeAdministrator/Credentials/UserID/text()') NodeAdminUID ";
      sql += ", extractValue(" + this.ConfigXML + ",'/Configuration/NodeSettings/NodeAdministrator/Credentials/Password/text()') NodeAdminPWD ";
      sql += ", extractValue(" + this.ConfigXML + ",'/Configuration/Application/LoggingLevel/Node.Administration/text()') NodeAdminLogLevel ";
      sql += ", extractValue(" + this.ConfigXML + ",'/Configuration/Application/LoggingLevel/Node.Client/text()') NodeClientLogLevel ";
      sql += ", extractValue(" + this.ConfigXML + ",'/Configuration/Application/LoggingLevel/Node.Task/text()') NodeTaskLogLevel ";
      sql += ", extractValue(" + this.ConfigXML + ",'/Configuration/Application/LoggingLevel/Node.WebServices/text()') NodeWebServicesLogLevel ";
      sql += ", extractValue(" + this.ConfigXML + ",'/Configuration/NodeSettings2/NodeURL/text()') NodeURL2 ";
      sql += ", extractValue(" + this.ConfigXML + ",'/Configuration/RestfulService/Introduction/Header/text()') IntroductionHeader "; // WI 33641
      sql += ", extractValue(" + this.ConfigXML + ",'/Configuration/RestfulService/Introduction/Content/text()') IntroductionContent "; // WI 33641
      sql += "from " + this.TableName + " ";
      sql += "where " + this.ConfigName + " = 'system.config'";
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      if (rs != null && rs.last() && rs.getRow() == 1) {
        String temp = rs.getString("NodeStatus");
        if (temp != null)
          this.hash.put("NodeStatus",rs.getString("NodeStatus"));
        boolean tokenLifeTimeEnabled = rs.getBoolean("TokenLifeTimeEnabled");
        if (tokenLifeTimeEnabled)
          this.hash.put("TokenLifeTime",""+rs.getInt("TokenLifeTime"));
        else
          this.hash.put("TokenLifeTime","-1");
        temp = rs.getString("NodeName");
        if (temp != null)
          this.hash.put("NodeName",temp);
        temp = rs.getString("NodeMessage");
        if (temp != null)
          this.hash.put("NodeMessage",temp);
        temp = rs.getString("NAASURL");
        if (temp != null)
          this.hash.put("NAASURL",temp);
        temp = rs.getString("NodeURL");
        if (temp != null)
          this.hash.put("NodeURL",temp);
        temp = rs.getString("NodeAdminName");
        if (temp != null)
          this.hash.put("NodeAdminName",temp);
        temp = rs.getString("NodeAdminUID");
        if (temp != null)
          this.hash.put("NodeAdminUID",temp);
        temp = rs.getString("NodeAdminPWD");
        if (temp != null)
          this.hash.put("NodeAdminPWD",temp);
        temp = rs.getString("NodeAdminLogLevel");
        if (temp != null)
          this.hash.put("NodeAdminLogLevel",temp);
        temp = rs.getString("NodeClientLogLevel");
        if (temp != null)
          this.hash.put("NodeClientLogLevel",temp);
        temp = rs.getString("NodeTaskLogLevel");
        if (temp != null)
          this.hash.put("NodeTaskLogLevel",temp);
        temp = rs.getString("NodeWebServicesLogLevel");
        if (temp != null)
          this.hash.put("NodeWebServicesLogLevel",temp);
        temp = rs.getString("NodeURL2");
        if (temp != null)
          this.hash.put("NodeURL2", temp);
        // WI 33641
        temp = rs.getString("IntroductionHeader");
        if (temp != null)
            this.hash.put("IntroductionHeader", temp);
        temp = rs.getString("IntroductionContent");
        if (temp != null)
            this.hash.put("IntroductionContent", temp);
      }
      if (rs != null)
        rs.close();
      // Proxy
      sql = "select extractValue(" + this.ConfigXML + ",'/Configuration/ProxySettings/@status') ProxyStatus ";
      sql += ", extractValue(" + this.ConfigXML + ",'/Configuration/ProxySettings/@host') ProxyHost ";
      sql += ", extractValue(" + this.ConfigXML + ",'/Configuration/ProxySettings/@port') ProxyPort ";
      sql += ", extractValue(" + this.ConfigXML + ",'/Configuration/ProxySettings/Credentials/UserID/text()') ProxyUID ";
      sql += ", extractValue(" + this.ConfigXML + ",'/Configuration/ProxySettings/Credentials/Password/text()') ProxyPWD ";
      sql += "from " + this.TableName + " ";
      sql += "where " + this.ConfigName + " = 'system.config'";
      rs = db.GetResultSet(sql);
      if (rs != null && rs.last() && rs.getRow() == 1) {
        this.hash.put("ProxyStatus",rs.getString("ProxyStatus"));
        String temp = "";
        if ((temp = rs.getString("ProxyHost")) == null)
          temp = "";
        this.hash.put("ProxyHost",temp);
        if ((temp = rs.getString("ProxyPort")) == null)
          temp = "";
        this.hash.put("ProxyPort",temp);
        if ((temp = rs.getString("ProxyUID")) == null)
          temp = "";
        this.hash.put("ProxyUID",temp);
        if ((temp = rs.getString("ProxyPWD")) == null)
          temp = "";
        this.hash.put("ProxyPWD",temp);
      }
      else {
        this.hash.put("ProxyStatus","I");
        this.hash.put("ProxyHost","");
        this.hash.put("ProxyPort","");
        this.hash.put("ProxyUID","");
        this.hash.put("ProxyPWD","");
      }
      this.SetClientURL(db);
      this.SetClientURL_V2(db);
      if (rs != null)
        rs.close();
      // FTP
      sql = "select extractValue(" + this.ConfigXML + ",'/Configuration/FTPSettings/@status') FTPStatus ";
      sql += ", extractValue(" + this.ConfigXML + ",'/Configuration/FTPSettings/@host') FTPHost ";
      sql += ", extractValue(" + this.ConfigXML + ",'/Configuration/FTPSettings/@port') FTPPort ";
      sql += ", extractValue(" + this.ConfigXML + ",'/Configuration/FTPSettings/Credentials/UserID/text()') FTPUID ";
      sql += ", extractValue(" + this.ConfigXML + ",'/Configuration/FTPSettings/Credentials/Password/text()') FTPPWD ";
      sql += "from " + this.TableName + " ";
      sql += "where " + this.ConfigName + " = 'system.config'";
      rs = db.GetResultSet(sql);
      if (rs != null && rs.last() && rs.getRow() == 1) {
        this.hash.put("FTPStatus",rs.getString("FTPStatus"));
        String temp = "";
        if ((temp = rs.getString("FTPHost")) == null)
          temp = "";
        this.hash.put("FTPHost",temp);
        if ((temp = rs.getString("FTPPort")) == null)
          temp = "";
        this.hash.put("FTPPort",temp);
        if ((temp = rs.getString("FTPUID")) == null)
          temp = "";
        this.hash.put("FTPUID",temp);
        if ((temp = rs.getString("FTPPWD")) == null)
          temp = "";
        this.hash.put("FTPPWD",temp);
      }
      else {
        this.hash.put("FTPStatus","I");
        this.hash.put("FTPHost","");
        this.hash.put("FTPPort","");
        this.hash.put("FTPUID","");
        this.hash.put("FTPPWD","");
      }
      if (rs != null)
          rs.close();
      // Email Server
      String[] xpaths = new String[] {
          "/Configuration/AutoMail/EmailTemplate/Sender/Name/text()",
          "/Configuration/AutoMail/EmailTemplate/Sender/EmailAddress/text()",
          "/Configuration/AutoMail/EmailTemplate/Sender/Credentials/UserID/text()",
          "/Configuration/AutoMail/EmailTemplate/Sender/Credentials/Password/text()",
          "/Configuration/AutoMail/EmailTemplate/Subject/text()",
          "/Configuration/AutoMail/EmailTemplate/@template"
      };
      String[] names = new String[] {
          "SenderName","SenderEmail","SenderUID","SenderPWD","SenderSubject","EmailTemplate"
      };
      sql = this.GetXDBSelectStr(this.ConfigXML,xpaths,names,this.TableName,new String[]{this.ConfigName},new String[]{"system.config"});
      rs = db.GetResultSet(sql);
      if (rs != null && rs.last() && rs.getRow() == 1) {
        String temp = rs.getString("SenderName");
        if (temp != null)
          this.hash.put("SenderName",temp);
        temp = rs.getString("SenderEmail");
        if (temp != null)
          this.hash.put("SenderEmail",temp);
        temp = rs.getString("SenderUID");
        if (temp != null)
          this.hash.put("SenderUID",temp);
        temp = rs.getString("SenderPWD");
        if (temp != null)
          this.hash.put("SenderPWD",temp);
        temp = rs.getString("SenderSubject");
        if (temp != null)
          this.hash.put("SenderSubject",temp);
        temp = rs.getString("EmailTemplate");
        if (temp != null)
          this.hash.put("EmailTemplate",temp);
      }
      this.SetCCLists(db);
    } catch (Exception e) {
      java.io.StringWriter sw = new java.io.StringWriter();
      e.printStackTrace(new java.io.PrintWriter(sw));
      try { sw.close(); } catch (Exception ex) { }
      this.LogException("Could Not Get SystemConfiguration: " + sw.toString());
    }
    finally {
      try {
        if (rs != null)
          rs.close();
        if (db != null)
          db.Close();
      } catch (Exception e) {
        this.LogException(e.toString());
      }
    }
  }

	/**
	 * SetClientURL.
	 * @param db.
	 * @return 
	 */
  private void SetClientURL (IDBAdapter db)
  {
    ResultSet rs = null;
    try {
      String sql = this.GetXDBSelectStr(this.ConfigXML,new String[]{"/Configuration/ClientSettings"},new String[]{"ClientConfig"},
                                        new String[]{"getStringVal()"},this.TableName,new String[]{this.ConfigName},
                                        new String[]{"system.config"});
      rs = db.GetResultSet(sql);
      if (rs != null && rs.last() && rs.getRow() == 1) {
        String str = rs.getString("ClientConfig");
        if (str != null) {
          XmlDocument client = new XmlDocument();
          client.LoadXml(str);
          XmlNodeList list = client.SelectNodes("/ClientSettings/WebServicesURL");
          if (list != null && list.Count() > 0)
            for (int i = 0; i < list.Count(); i++)
              this.hash.put("ClientURLV1_"+i,list.ItemOf(i).GetInnerText());
        }
      }
    } catch (Exception e) {
      this.LogException("Could Not Get Client URLs: " + e.toString());
    }
    finally {
      try {
        if (rs != null)
          rs.close();
      } catch (Exception e) {
        this.LogException(e.toString());
      }
    }
  }

	/**
	 * SetClientURL_V2.
	 * @param db.
	 * @return 
	 */
  private void SetClientURL_V2 (IDBAdapter db)
  {
    ResultSet rs = null;
    try {
      String sql = this.GetXDBSelectStr(this.ConfigXML,new String[]{"/Configuration/ClientSettings2"},new String[]{"ClientConfig"},
                                        new String[]{"getStringVal()"},this.TableName,new String[]{this.ConfigName},
                                        new String[]{"system.config"});
      rs = db.GetResultSet(sql);
      if (rs != null && rs.last() && rs.getRow() == 1) {
        String str = rs.getString("ClientConfig");
        if (str != null) {
          XmlDocument client = new XmlDocument();
          client.LoadXml(str);
          XmlNodeList list = client.SelectNodes("/ClientSettings2/WebServicesURL");
          if (list != null && list.Count() > 0)
            for (int i = 0; i < list.Count(); i++)
              this.hash.put("ClientURLV2_"+i,list.ItemOf(i).GetInnerText());
        }
      }
    } catch (Exception e) {
      this.LogException("Could Not Get Client URLs: " + e.toString());
    }
    finally {
      try {
        if (rs != null)
          rs.close();
      } catch (Exception e) {
        this.LogException(e.toString());
      }
    }
  }

	/**
	 * SetCCLists.
	 * @param db.
	 * @return 
	 */
  private void SetCCLists (IDBAdapter db)
  {
    ResultSet rs = null;
    try {
      String sql = this.GetXDBSelectStr(this.ConfigXML,new String[]{"/Configuration/AutoMail/EmailTemplate/To"},
                                        new String[]{"CCList"},new String[]{"getStringVal()"},this.TableName,
                                        new String[]{this.ConfigName},new String[]{"system.config"});
      rs = db.GetResultSet(sql);
      if (rs != null && rs.last() && rs.getRow() == 1) {
        String ccListStr = rs.getString("CCList");
        if (ccListStr != null) {
          XmlDocument ccDoc = new XmlDocument();
          ccDoc.LoadXml(ccListStr);
          XmlNodeList list = ccDoc.SelectNodes("/To/CC");
          if (list != null && list.Count() > 0)
            for (int i = 0; i < list.Count(); i++)
              this.hash.put("CCList" + i,list.ItemOf(i).GetInnerText());
          list = ccDoc.SelectNodes("/To/BCC");
          if (list != null && list.Count() > 0)
            for (int i = 0; i < list.Count(); i++)
              this.hash.put("BCCList" + i,list.ItemOf(i).GetInnerText());
        }
      }
    } catch (Exception e) {
      this.LogException("Could Not Get CCLists: " + e.toString());
    }
    finally {
      try {
        if (rs != null)
          rs.close();
      } catch (Exception e) {
        this.LogException(e.toString());
      }
    }
  }

	/**
	 * GetEmailServerHost.
	 * @param .
	 * @return String
	 */
  public String GetEmailServerHost ()
  {
    String retString = null;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String sql = this.GetXDBSelectStr(this.SysConfigConfigXML,new String[]{"/Configuration/AutoMail/EmailServer/@host"},
                                 new String[]{"EmailServerHost"},this.SysConfigTableName,new String[]{this.SysConfigConfigName},
                                 new String[]{"system.config"});
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      if (rs != null && rs.last() && rs.getRow() == 1)
        retString = rs.getString("EmailServerHost");
    } catch (Exception e) {
      this.LogException("Could Not Get Email Server Host: " + e.toString());
    } finally {
      try {
        if (rs != null)
          rs.close();
        if (db != null)
          db.Close();
      } catch (Exception e) {
        this.LogException(e.toString());
      }
    }
    return retString;
  }

	/**
	 * GetEmailServerPort.
	 * @param .
	 * @return String
	 */
  public String GetEmailServerPort ()
  {
    String retString = null;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String sql = this.GetXDBSelectStr(this.SysConfigConfigXML,new String[]{"/Configuration/AutoMail/EmailServer/@port"},
                                 new String[]{"EmailServerPort"},this.SysConfigTableName,new String[]{this.SysConfigConfigName},
                                 new String[]{"system.config"});
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      if (rs != null && rs.last() && rs.getRow() == 1)
        retString = rs.getString("EmailServerPort");
    } catch (Exception e) {
      this.LogException("Could Not Get Email Server Host: " + e.toString());
    } finally {
      try {
        if (rs != null)
          rs.close();
        if (db != null)
          db.Close();
      } catch (Exception e) {
        this.LogException(e.toString());
      }
    }
    return retString;
  }

	/**
	 * SaveEmailSettings.
	 * @param host.
	 * @param port.
	 * @param userSenderName.
	 * @param userEmail.
	 * @param userUID.
	 * @param userPWD.
	 * @param userCCList.
	 * @param userBCCList.
	 * @param taskSenderName.
	 * @param taskEmail.
	 * @param taskUID.
	 * @param taskPWD.
	 * @param taskCCList.
	 * @param taskBCCList.
	 * @return boolean
	 */
  public boolean SaveEmailSettings (String host, String port, String userSenderName, String userEmail, String userUID, String userPWD,
                                    String[] userCCList, String[] userBCCList, String taskSenderName, String taskEmail, String taskUID,
                                    String taskPWD, String[] taskCCList, String[] taskBCCList)
  {
    boolean retBool = false;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      Cryptography crypt = new Cryptography();

      // system.config
      String sql = "update " + this.SysConfigTableName + " set " + this.SysConfigConfigXML + " = updateXML(" + this.SysConfigConfigXML;
      sql += ",'/Configuration/AutoMail'";
      sql += ",'<AutoMail><EmailServer name=\"USER\" status=\"A\" host=\"";
      sql += host != null ? XmlUtility.replaceByEntityRef(host) : "";
      sql += "\" port=\"";
      sql += port != null ? XmlUtility.replaceByEntityRef(port) : "";
      sql += "\" />";
      sql += "<EmailTemplate name=\"UserManagement\" status=\"A\" server=\"USER\" template=\"UserAccount.eml\">";
      sql += "<Subject>Node User Account</Subject><Sender><Name>";
      sql += userSenderName != null ? XmlUtility.replaceByEntityRef(userSenderName) : "";
      sql += "</Name><EmailAddress>";
      sql += userEmail != null ? XmlUtility.replaceByEntityRef(userEmail) : "";
      sql += "</EmailAddress><Credentials><UserID>";
      sql += userUID != null ? XmlUtility.replaceByEntityRef(userUID) : "" ;
      sql += "</UserID><Password>";
      sql += userPWD != null && !userPWD.equals("") ? XmlUtility.replaceByEntityRef(crypt.Encrypting(userPWD,Phrase.CryptKey)) : "";
      sql += "</Password></Credentials></Sender><To>";
      if (userCCList != null)
        for (int i = 0; i < userCCList.length; i++)
          sql += "<CC>" + XmlUtility.replaceByEntityRef(userCCList[i]) + "</CC>";
      if (userBCCList != null)
        for (int i = 0; i < userBCCList.length; i++)
          sql += "<BCC>" + XmlUtility.replaceByEntityRef(userBCCList[i]) + "</BCC>";
      sql += "</To></EmailTemplate></AutoMail>')";
      sql += " where " + this.SysConfigConfigName + " = 'system.config'";
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);

      // task.config
      if (rs != null)
        rs.close();
      sql = "update " + this.SysConfigTableName + " set " + this.SysConfigConfigXML + " = updateXML(" + this.SysConfigConfigXML;
      sql += ",'/Configuration/AutoMail'";
      sql += ",'<AutoMail><EmailServer name=\"TASK\" status=\"A\" host=\"";
      sql += host != null ? XmlUtility.replaceByEntityRef(host) : "";
      sql += "\" port=\"";
      sql += port != null ? XmlUtility.replaceByEntityRef(port) : "";
      sql += "\" />";
      sql += "<EmailTemplate name=\"TaskStatus\" status=\"A\" server=\"TASK\" template=\"TaskRunStatus.eml\">";
      sql += "<Subject>Node Task Status</Subject><Sender><Name>";
      sql += taskSenderName != null ? XmlUtility.replaceByEntityRef(taskSenderName) : "";
      sql += "</Name><EmailAddress>";
      sql += taskEmail != null ? XmlUtility.replaceByEntityRef(taskEmail) : "";
      sql += "</EmailAddress><Credentials><UserID>";
      sql += taskUID != null ? XmlUtility.replaceByEntityRef(taskUID) : "" ;
      sql += "</UserID><Password>";
      sql += taskPWD != null && !taskPWD.equals("") ? XmlUtility.replaceByEntityRef(crypt.Encrypting(taskPWD,Phrase.CryptKey)) : "";
      sql += "</Password></Credentials></Sender><To>";
      if (taskCCList != null)
        for (int i = 0; i < taskCCList.length; i++)
          sql += "<CC>" + XmlUtility.replaceByEntityRef(taskCCList[i]) + "</CC>";
      if (taskBCCList != null)
        for (int i = 0; i < taskBCCList.length; i++)
          sql += "<BCC>" + XmlUtility.replaceByEntityRef(taskBCCList[i]) + "</BCC>";
      sql += "</To></EmailTemplate></AutoMail>')";
      sql += " where " + this.SysConfigConfigName + " = 'task.config'";
      rs = db.GetResultSet(sql);

      retBool = true;
    } catch (Exception e) {
      this.LogException("Could Not Save Email Settings: " + e.toString());
    } finally {
      try {
        if (rs != null)
          rs.close();
        if (db != null)
          db.Close();
      } catch (Exception e) {
        this.LogException(e.toString());
      }
    }
    return retBool;
  }

	/**
	 * GetQueries.
	 * @param .
	 * @return String[]
	 */
  public String[] GetQueries ()
  {
    String[] retArray = null;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      db = this.GetNodeDB();
      int index = 1;
      String[] temp = null;
      for (; true; index++) {
        String sql = "select extractValue(" + this.ConfigXML + ",'/Configuration/NodeSettings/Query[" + index + "]/text()') Query";
        sql += " from " + this.TableName;
        sql += " where " + this.ConfigName + " = 'system.config'";
        sql += " and existsNode(" + this.ConfigXML + ",'/Configuration/NodeSettings/Query[" + index + "]') = 1";
        rs = db.GetResultSet(sql);
        if (rs != null && rs.last() && rs.getRow() == 1) {
          if (temp == null || temp.length == index - 1)
            temp = this.GetNewList(temp);
          temp[index-1] = rs.getString("Query");
        }
        else
          break;
        if (rs != null) {
          rs.close();
          rs = null;
        }
      }
      retArray = new String [index - 1];
      for (int i = 0; i < index - 1; i++)
        retArray[i] = temp[i];
    } catch (Exception e) {
      this.LogException("Could Not Get Queries: " + e.toString());
    }
    finally {
      try {
        if (rs != null)
          rs.close();
        if (db != null)
          db.Close();
      } catch (Exception e) {
        this.LogException(e.toString());
      }
    }
    return retArray;
  }

	/**
	 * GetNewList.
	 * @param old.
	 * @return String[]
	 */
  private String[] GetNewList (String[] old)
  {
    String[] retArray = null;
    if (old == null || old.length == 0)
      retArray = new String[1];
    else {
      retArray = new String[old.length * 2];
      for (int i = 0; i < old.length; i++)
        retArray[i] = old[i];
    }
    return retArray;
  }

	/**
	 * SaveConfigSettings.
	 * @param nodeStatus.
	 * @param tokenLifeTime.
	 * @param nodeName.
	 * @param nodeMessage.
	 * @param naasURL.
	 * @param nodeURL.
	 * @param nodeURLV2.
	 * @param proxyStatus.
	 * @param proxyHost.
	 * @param proxyPort.
	 * @param proxyUID.
	 * @param proxyPWD.
	 * @param nodeAdminName.
	 * @param nodeAdminUID.
	 * @param nodeAdminPWD.
	 * @param adminLogLevel.
	 * @param clientLogLevel.
	 * @param taskLogLevel.
	 * @param webServicesLogLevel.
	 * @param clientURLs.
	 * @return boolean
	 */
  public boolean SaveConfigSettings (String nodeStatus, int tokenLifeTime, String nodeName, String nodeMessage, String naasURL,
                                     String nodeURL, String nodeURLV2, boolean proxyStatus, String proxyHost, String proxyPort, String proxyUID,
                                     String proxyPWD, String nodeAdminName, String nodeAdminUID, String nodeAdminPWD,
                                     Level adminLogLevel, Level clientLogLevel, Level taskLogLevel, Level webServicesLogLevel,
                                     String[] clientURLs)
  {
    boolean retBool = false;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      Cryptography crypt = new Cryptography();
      String sql = "update " + this.TableName + " set " + this.ConfigXML + " = updateXML(" + this.ConfigXML;
      sql += ",'/Configuration/Application/NodeStatus/Status', '<Status>" + XmlUtility.replaceByEntityRef(nodeStatus) + "</Status>'";
      if (tokenLifeTime > 0) {
        sql += ",'/Configuration/NodeSettings/TokenLifeTime/@Enabled','true'";
        sql += ",'/Configuration/NodeSettings/TokenLifeTime/@time','" + tokenLifeTime + "'";
      }
      else
        sql += ",'/Configuration/NodeSettings/TokenLifeTime/@Enabled','false'";
      sql += ",'/Configuration/NodeSettings/@name','" + nodeName + "'";
      sql += ",'/Configuration/Application/NodeStatus/Message','<Message>" + XmlUtility.replaceByEntityRef(nodeMessage) + "</Message>'";
      sql += ",'/Configuration/NodeSettings/NAAS/URL','<URL>" + XmlUtility.replaceByEntityRef(naasURL) + "</URL>'";
      sql += ",'/Configuration/NodeSettings/NodeURL','<NodeURL>" + XmlUtility.replaceByEntityRef(nodeURL) + "</NodeURL>'";
      sql += ",'/Configuration/NodeSettings2/NodeURL','<NodeURL>" + XmlUtility.replaceByEntityRef(nodeURLV2) + "</NodeURL>'";
      sql += ",'/Configuration/ProxySettings','<ProxySettings status=\"";
      if (proxyStatus) sql += "A"; else sql += "I";
      sql += "\" host=\"";
      if (proxyStatus) sql += XmlUtility.replaceByEntityRef(proxyHost);
      sql += "\" port=\"";
      if (proxyStatus) sql += XmlUtility.replaceByEntityRef(proxyPort);
      sql += "\"><Credentials><UserID>";
      if (proxyStatus) sql += XmlUtility.replaceByEntityRef(proxyUID);
      sql += "</UserID><Password>";
      if (proxyStatus && proxyPWD != null && !proxyPWD.equals("")) sql += XmlUtility.replaceByEntityRef(crypt.Encrypting(proxyPWD,Phrase.CryptKey));
      sql += "</Password></Credentials></ProxySettings>'";
      sql += ",'/Configuration/NodeSettings/NodeAdministrator/Name','<Name>" + XmlUtility.replaceByEntityRef(nodeAdminName) + "</Name>'";
      sql += ",'/Configuration/NodeSettings/NodeAdministrator/Credentials/UserID','<UserID>" + XmlUtility.replaceByEntityRef(nodeAdminUID) + "</UserID>'";
      sql += ",'/Configuration/NodeSettings/NodeAdministrator/Credentials/Password','<Password>";
      if (nodeAdminPWD != null && !nodeAdminPWD.equals("")) sql += XmlUtility.replaceByEntityRef(crypt.Encrypting(nodeAdminPWD,Phrase.CryptKey));
      sql += "</Password>'";
      sql += ",'/Configuration/Application/LoggingLevel/Node.Administration', '<Node.Administration>" + XmlUtility.replaceByEntityRef(adminLogLevel.toString()) + "</Node.Administration>'";
      sql += ",'/Configuration/Application/LoggingLevel/Node.Client', '<Node.Client>" + XmlUtility.replaceByEntityRef(clientLogLevel.toString()) + "</Node.Client>'";
      sql += ",'/Configuration/Application/LoggingLevel/Node.Task', '<Node.Task>" + XmlUtility.replaceByEntityRef(taskLogLevel.toString()) + "</Node.Task>'";
      sql += ",'/Configuration/Application/LoggingLevel/Node.WebServices', '<Node.WebServices>" + XmlUtility.replaceByEntityRef(webServicesLogLevel.toString()) + "</Node.WebServices>'";
      if (clientURLs != null) {
        sql += ",'/Configuration/ClientSettings','<ClientSettings>";
        for (int i = 0; i < clientURLs.length; i++)
          sql += "<WebServicesURL>" + XmlUtility.replaceByEntityRef(clientURLs[i]) + "</WebServicesURL>";
        sql += "</ClientSettings>'";
      }
      sql += ") where " + this.ConfigName + " = 'system.config'";
      sql += " and existsNode(" + this.ConfigXML + ",'/Configuration/Application/NodeStatus/Status') = 1";
      sql += " and existsNode(" + this.ConfigXML + ",'/Configuration/NodeSettings/TokenLifeTime') = 1";
      sql += " and existsNode(" + this.ConfigXML + ",'/Configuration/NodeSettings') = 1";
      sql += " and existsNode(" + this.ConfigXML + ",'/Configuration/Application/NodeStatus/Message') = 1";
      sql += " and existsNode(" + this.ConfigXML + ",'/Configuration/NodeSettings/NAAS/URL') = 1";
      sql += " and existsNode(" + this.ConfigXML + ",'/Configuration/NodeSettings/NodeURL') = 1";
      sql += " and existsNode(" + this.ConfigXML + ",'/Configuration/NodeSettings/NodeAdministrator/Name') = 1";
      sql += " and existsNode(" + this.ConfigXML + ",'/Configuration/NodeSettings/NodeAdministrator/Credentials/UserID') = 1";
      sql += " and existsNode(" + this.ConfigXML + ",'/Configuration/NodeSettings/NodeAdministrator/Credentials/Password') = 1";
      sql += " and existsNode(" + this.ConfigXML + ",'/Configuration/Application/LoggingLevel/Node.Administration') = 1";
      sql += " and existsNode(" + this.ConfigXML + ",'/Configuration/Application/LoggingLevel/Node.Client') = 1";
      sql += " and existsNode(" + this.ConfigXML + ",'/Configuration/Application/LoggingLevel/Node.Task') = 1";
      sql += " and existsNode(" + this.ConfigXML + ",'/Configuration/Application/LoggingLevel/Node.WebServices') = 1";
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);

      // Log Settings
      Logger administration = Logger.getLogger(Phrase.AdministrationLoggerName);
      administration.setLevel(adminLogLevel);
      Logger client = Logger.getLogger(Phrase.ClientLoggerName);
      client.setLevel(clientLogLevel);
      Logger task = Logger.getLogger(Phrase.TaskLoggerName);
      task.setLevel(taskLogLevel);
      Logger webServices = Logger.getLogger(Phrase.WebServicesLoggerName);
      webServices.setLevel(webServicesLogLevel);

      retBool = true;
    } catch (Exception e) {
      this.LogException("Could not save Configuration Settings: " + e.toString());
    }
    finally {
      try {
        if (rs != null)
          rs.close();
        if (db != null)
          db.Close();
      } catch (Exception e) {
        this.LogException(e.toString());
      }
      this.Init();
    }
    return retBool;
  }

	/**
	 * SaveClientURLs.
	 * @param clientURLs.
	 * @return boolean
	 */
  public boolean SaveClientURLs (String[] clientURLs)
  {
    boolean retBool = false;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String sql = "update " + this.TableName + " set " + this.ConfigXML + " = updateXML(" + this.ConfigXML;
      if (clientURLs != null) {
        sql += ",'/Configuration/ClientSettings','<ClientSettings>";
        for (int i = 0; i < clientURLs.length; i++)
          sql += "<WebServicesURL>" + XmlUtility.replaceByEntityRef(clientURLs[i]) + "</WebServicesURL>";
        sql += "</ClientSettings>'";
      }
      sql += ") where " + this.ConfigName + " = 'system.config'";
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      retBool = true;
    } catch (Exception e) {
      this.LogException("Could not Save Client URLs: " + e.toString());
    }
    finally {
      try {
        if (rs != null)
          rs.close();
        if (db != null)
          db.Close();
      } catch (Exception e) {
        this.LogException(e.toString());
      }
    }
    return retBool;
  }

	/**
	 * SaveClientURLs_V2.
	 * @param clientURLs.
	 * @return boolean
	 */
  public boolean SaveClientURLs_V2 (String[] clientURLs)
  {
    boolean retBool = false;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String sql = "update " + this.TableName + " set " + this.ConfigXML + " = updateXML(" + this.ConfigXML;
      if (clientURLs != null) {
        sql += ",'/Configuration/ClientSettings2','<ClientSettings2>";
        for (int i = 0; i < clientURLs.length; i++)
          sql += "<WebServicesURL>" + XmlUtility.replaceByEntityRef(clientURLs[i]) + "</WebServicesURL>";
        sql += "</ClientSettings2>'";
      }
      sql += ") where " + this.ConfigName + " = 'system.config'";
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      retBool = true;
    } catch (Exception e) {
      this.LogException("Could not Save Client URLs2: " + e.toString());
    }
    finally {
      try {
        if (rs != null)
          rs.close();
        if (db != null)
          db.Close();
      } catch (Exception e) {
        this.LogException(e.toString());
      }
    }
    return retBool;
  }

	// WI 33641
	/**
	 * SaveRestServiceIntroduction.
	 * 
	 * @param introduction
	 *            .
	 * @return boolean
	 */
	public boolean SaveRestServiceIntroduction(String[] introduction) {
		boolean retBool = false;
		IDBAdapter db = null;
		ResultSet rs = null;
		try {
			String sql = "update " + this.TableName + " set " + this.ConfigXML
					+ " = updateXML(" + this.ConfigXML;
			if (introduction != null) {
				sql += ",'/Configuration/RestfulService/Introduction'," 
						+ "'<Introduction>" 
						+ "<Header>"
						+ Utility.convertToXmlValue(introduction[0])
						+ "</Header>"
						+ "<Content>"
						+ Utility.convertToXmlValue(introduction[1])
						+ "</Content>" 
						+ "</Introduction>'";
			}
			sql += ") where " + this.ConfigName + " = 'system.config'";
			db = this.GetNodeDB();
			rs = db.GetResultSet(sql);
			retBool = true;
			
			  sql = "select extractValue(" + this.ConfigXML + ",'/Configuration/Application/NodeStatus/Status/text()') NodeStatus ";
			  sql += ", extractValue(" + this.ConfigXML + ",'/Configuration/RestfulService/Introduction/Header/text()') IntroductionHeader "; 
			  sql += "from " + this.TableName + " ";
			  sql += "where " + this.ConfigName + " = 'system.config'";
			  db = this.GetNodeDB();
			  rs = db.GetResultSet(sql);
			  if (rs != null && rs.last() && rs.getRow() == 1) {
			    String temp = rs.getString("NodeStatus");
			    temp = rs.getString("IntroductionHeader");
			    if (temp == null){
			    	retBool = false;
			    }
			  }			
		} catch (Exception e) {
			this.LogException("Could not Save RESTful Services Introduction: "
					+ e.toString());
		} finally {
			try {
				if (rs != null)
					rs.close();
				if (db != null)
					db.Close();
			} catch (Exception e) {
				this.LogException(e.toString());
			}
		}
		return retBool;
	}

/**
	 * SaveUserCCList.
	 * @param ccList.
	 * @return boolean
	 */
  public boolean SaveUserCCList (String[] ccList)
  {
    boolean retBool = false;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String value = "<To>";
      if (ccList != null)
        for (int i = 0; i < ccList.length; i++)
          value += "<CC>" + ccList[i] + "</CC>";
      String[] bcc = this.GetUserEmailBCCList();
      if (bcc != null)
        for (int i = 0; i < bcc.length; i++)
          value += "<BCC>" + bcc[i] + "</BCC>";
      value += "</To>";
      String sql = this.GetUpdateXDBString(this.TableName,this.ConfigXML,new String[]{"/Configuration/AutoMail/EmailTemplate/To"},
                                           new String[]{value},new String[]{this.ConfigName},new String[]{"system.config"});
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      retBool = true;
    } catch (Exception e) {
      this.LogException("Could not Save UserCC List: " + e.toString());
    }
    finally {
      try {
        if (rs != null)
          rs.close();
        if (db != null)
          db.Close();
      } catch (Exception e) {
        this.LogException(e.toString());
      }
    }
    return retBool;
  }

	/**
	 * SaveUserBCCList.
	 * @param bccList.
	 * @return boolean
	 */
  public boolean SaveUserBCCList (String[] bccList)
  {
    boolean retBool = false;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String value = "<To>";
      String[] cc = this.GetUserEmailCCList();
      if (cc != null)
        for (int i = 0; i < cc.length; i++)
          value += "<CC>" + cc[i] + "</CC>";
      if (bccList != null)
        for (int i = 0; i < bccList.length; i++)
          value += "<BCC>" + bccList[i] + "</BCC>";
      value += "</To>";
      String sql = this.GetUpdateXDBString(this.TableName,this.ConfigXML,new String[]{"/Configuration/AutoMail/EmailTemplate/To"},
                                           new String[]{value},new String[]{this.ConfigName},new String[]{"system.config"});
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      retBool = true;
    } catch (Exception e) {
      this.LogException("Could not Save UserBCC List: " + e.toString());
    }
    finally {
      try {
        if (rs != null)
          rs.close();
        if (db != null)
          db.Close();
      } catch (Exception e) {
        this.LogException(e.toString());
      }
    }
    return retBool;
  }

	/**
	 * GetNodeStatus.
	 * @param bccList.
	 * @return String
	 */
  public String GetNodeStatus ()
  {
    return (String)this.hash.get("NodeStatus");
  }

	/**
	 * GetTokenLifeTime.
	 * @param .
	 * @return int
	 */
  public int GetTokenLifeTime ()
  {
    return Integer.parseInt((String)this.hash.get("TokenLifeTime"));
  }

	/**
	 * GetNodeName.
	 * @param .
	 * @return String
	 */
  public String GetNodeName ()
  {
    return (String)this.hash.get("NodeName");
  }

	/**
	 * GetNodeMessage.
	 * @param .
	 * @return String
	 */
  public String GetNodeMessage ()
  {
    return (String)this.hash.get("NodeMessage");
  }

	/**
	 * GetNAASURL.
	 * @param .
	 * @return String
	 */
  public String GetNAASURL ()
  {
    return (String)this.hash.get("NAASURL");
  }

	/**
	 * GetNodeURL.
	 * @param .
	 * @return String
	 */
  public String GetNodeURL ()
  {
    return (String)this.hash.get("NodeURL");
  }

	/**
	 * GetNodeURL_V2.
	 * @param .
	 * @return String
	 */
  public String GetNodeURL_V2()
  {
	  return (String)this.hash.get("NodeURL2");
  }
  
	/**
	 * GetProxyStatus.
	 * @param .
	 * @return boolean
	 */
  public boolean GetProxyStatus ()
  {
    String status = (String)this.hash.get("ProxyStatus");
    if (status.equalsIgnoreCase("A"))
      return true;
    else
      return false;
  }

	/**
	 * GetProxyHost.
	 * @param .
	 * @return String
	 */
  public String GetProxyHost ()
  {
    if (this.GetProxyStatus())
      return (String)this.hash.get("ProxyHost");
    else
      return "";
  }

	/**
	 * GetProxyPort.
	 * @param .
	 * @return String
	 */
  public String GetProxyPort ()
  {
    if (this.GetProxyStatus())
      return (String)this.hash.get("ProxyPort");
    else
      return "";
  }

	/**
	 * GetProxyUID.
	 * @param .
	 * @return String
	 */
  public String GetProxyUID ()
  {
    if (this.GetProxyStatus())
      return (String)this.hash.get("ProxyUID");
    else
      return "";
  }

	/**
	 * GetProxyPWD.
	 * @param .
	 * @return String
	 */
  public String GetProxyPWD ()
  {
    if (this.GetProxyStatus()) {
      Cryptography crypt = new Cryptography();
      try {
        String pwd = (String)this.hash.get("ProxyPWD");
        if (pwd != null && !pwd.equals(""))
          return crypt.Decrypting(pwd, Node.Phrase.CryptKey);
      }
      catch (Exception e) {
        this.LogException("Could not decrypt Proxy Password: " + e.toString());
      }
    }
    return "";
  }

	/**
	 * GetFTPStatus.
	 * @param .
	 * @return boolean
	 */
  public boolean GetFTPStatus ()
  {
    String status = (String)this.hash.get("FTPStatus");
    if (status.equalsIgnoreCase("A"))
      return true;
    else
      return false;
  }

	/**
	 * GetFTPHost.
	 * @param .
	 * @return String
	 */
  public String GetFTPHost ()
  {
    if (this.GetFTPStatus())
      return (String)this.hash.get("FTPHost");
    else
      return "";
  }

	/**
	 * GetFTPPort.
	 * @param .
	 * @return String
	 */
  public String GetFTPPort ()
  {
    if (this.GetFTPStatus())
      return (String)this.hash.get("FTPPort");
    else
      return "";
  }

	/**
	 * GetFTPUID.
	 * @param .
	 * @return String
	 */
  public String GetFTPUID ()
  {
    if (this.GetFTPStatus())
      return (String)this.hash.get("FTPUID");
    else
      return "";
  }

	/**
	 * GetFTPPWD.
	 * @param .
	 * @return String
	 */
  public String GetFTPPWD ()
  {
    /*if (this.GetProxyStatus()) {
      Cryptography crypt = new Cryptography();
      try {
        String pwd = (String)this.hash.get("FTPPWD");
        if (pwd != null && !pwd.equals(""))
          return crypt.Decrypting(pwd, Node.Phrase.CryptKey);
      }
      catch (Exception e) {
        this.LogException("Could not decrypt Proxy Password: " + e.toString());
      }
    }
    return "";*/
    if (this.GetFTPStatus())
        return (String)this.hash.get("FTPPWD");
      else
        return "";

  }

	/**
	 * GetNodeAdminName.
	 * @param .
	 * @return String
	 */
  public String GetNodeAdminName ()
  {
    return (String)this.hash.get("NodeAdminName");
  }

	/**
	 * GetNodeAdminUID.
	 * @param .
	 * @return String
	 */
  public String GetNodeAdminUID ()
  {
    return (String)this.hash.get("NodeAdminUID");
  }

	/**
	 * GetNodeAdminPWD.
	 * @param .
	 * @return String
	 */
  public String GetNodeAdminPWD ()
  {
    Cryptography crypt = new Cryptography();
    try {
      String pwd = (String)this.hash.get("NodeAdminPWD");
      if (pwd != null && !pwd.equals(""))
        return crypt.Decrypting(pwd, Phrase.CryptKey);
    } catch (Exception e) {
      this.LogException("Could not decrypt Node Administrator Password: " + e.toString());
    }
    return "";
  }

	/**
	 * GetAdministrationLogLevel.
	 * @param .
	 * @return Level
	 */
  public Level GetAdministrationLogLevel ()
  {
    String levelStr = (String)this.hash.get("NodeAdminLogLevel");
    return LoggingUtils.ParseLevel(levelStr);
  }

	/**
	 * GetClientLogLevel.
	 * @param .
	 * @return Level
	 */
  public Level GetClientLogLevel ()
  {
    String levelStr = (String)this.hash.get("NodeClientLogLevel");
    return LoggingUtils.ParseLevel(levelStr);
  }

	/**
	 * GetTaskLogLevel.
	 * @param .
	 * @return Level
	 */
  public Level GetTaskLogLevel ()
  {
    String levelStr = (String)this.hash.get("NodeTaskLogLevel");
    return LoggingUtils.ParseLevel(levelStr);
  }

	/**
	 * GetWebServicesLogLevel.
	 * @param .
	 * @return Level
	 */
  public Level GetWebServicesLogLevel ()
  {
    String levelStr = (String)this.hash.get("NodeWebServicesLogLevel");
    return LoggingUtils.ParseLevel(levelStr);
  }

	/**
	 * GetClientNodeURLs.
	 * @param .
	 * @return String[]
	 */
  public String[] GetClientNodeURLs ()
  {
    String[] clientURLs =null;
    String[] retArray = null;
    for (int i = 0; true; i++) {
      String temp = (String)this.hash.get("ClientURLV1_" + i);
      if (temp != null)
        clientURLs = this.EnterValueArray(clientURLs,temp,i);
      else if (clientURLs != null) {
        retArray = new String [i];
        for (int j = 0; j < i; j++)
          retArray[j] = clientURLs[j];
        break;
      }
      else
        break;
    }
    return retArray;
  }

	/**
	 * GetClientNodeURLs_V2.
	 * @param .
	 * @return String[]
	 */
  public String[] GetClientNodeURLs_V2 ()
  {
    String[] clientURLs2 =null;
    String[] retArray = null;
    for (int i = 0; true; i++) {
      String temp = (String)this.hash.get("ClientURLV2_" + i);
      if (temp != null)
        clientURLs2 = this.EnterValueArray(clientURLs2,temp,i);
      else if (clientURLs2 != null) {
        retArray = new String [i];
        for (int j = 0; j < i; j++)
          retArray[j] = clientURLs2[j];
        break;
      }
      else
        break;
    }
    return retArray;
  }

	/**
	 * GetUserEmailSender.
	 * @param .
	 * @return String
	 */
  public String GetUserEmailSender ()
  {
    return (String)this.hash.get("SenderName");
  }

	/**
	 * GetUserEmailSenderEmail.
	 * @param .
	 * @return String
	 */
  public String GetUserEmailSenderEmail ()
  {
    return (String)this.hash.get("SenderEmail");
  }

	/**
	 * GetUserEmailUID.
	 * @param .
	 * @return String
	 */
  public String GetUserEmailUID ()
  {
    return (String)this.hash.get("SenderUID");
  }

	/**
	 * GetUserEmailPWD.
	 * @param .
	 * @return String
	 */
  public String GetUserEmailPWD ()
  {
    String pwd = null;
    try {
      Cryptography crypt = new Cryptography();
      String temp = (String)this.hash.get("SenderPWD");
      if (temp != null && !temp.equals(""))
        pwd = crypt.Decrypting(temp, Phrase.CryptKey);
    } catch (Exception e) {
      this.LogException("Could Not Decrypt User Email PWD: " + e.toString());
    }
    return pwd;
  }


	/**
	 * GetUserEmailCCList.
	 * @param .
	 * @return String[]
	 */
  public String[] GetUserEmailCCList ()
  {
    String[] retArray = null;
    String[] tempArray = null;
    for (int i = 0; true; i++) {
      String cc = (String)this.hash.get("CCList" + i);
      if (cc != null)
        tempArray = this.EnterValueArray(tempArray, cc, i);
      else {
        retArray = new String [i];
        for (int j = 0; j < i; j++)
          retArray[j] = tempArray[j];
        break;
      }
    }
    return retArray;
  }

	/**
	 * GetUserEmailBCCList.
	 * @param .
	 * @return String[]
	 */
  public String[] GetUserEmailBCCList ()
  {
    String[] retArray = null;
    String[] tempArray = null;
    for (int i = 0; true; i++) {
      String bcc = (String)this.hash.get("BCCList" + i);
      if (bcc != null)
        tempArray = this.EnterValueArray(tempArray, bcc, i);
      else {
        retArray = new String [i];
        for (int j = 0; j < i; j++)
          retArray[j] = tempArray[j];
        break;
      }
    }
    return retArray;
  }

	/**
	 * GetUserEmailSubject.
	 * @param .
	 * @return String
	 */
  public String GetUserEmailSubject ()
  {
    return (String)this.hash.get("SenderSubject");
  }

	/**
	 * GetEmailTemplateLocation.
	 * @param .
	 * @return String
	 */
  public String GetEmailTemplateLocation ()
  {
    return (String)this.hash.get("EmailTemplate");
  }

  // WI 33641
	/**
	 * GetRestServiceIntroductionHeader.
	 * 
	 * @param 
	 *            .
	 * @return String
	 */
	public String GetRestServiceIntroductionHeader() {
		return (String) this.hash.get("IntroductionHeader");
	}

	/**
	 * GetRestServiceIntroductionContent.
	 * 
	 * @param 
	 *            .
	 * @return String
	 */
	public String GetRestServiceIntroductionContent() {
		return (String) this.hash.get("IntroductionContent");
	}

	/**
	 * EnterValueArray.
	 * @param original.
	 * @param value.
	 * @param index.
	 * @return String[]
	 */
  private String[] EnterValueArray (String[] original, String value, int index)
  {
    if (original == null || original.length == 0)
      return new String[]{value};
    if (index < original.length) {
      original[index] = value;
      return original;
    }
    String[] retArray = new String [original.length*2];
    for (int i = 0; i < original.length; i++)
      retArray[i] = original[i];
    retArray[original.length] = value;
    return retArray;
  }
}
