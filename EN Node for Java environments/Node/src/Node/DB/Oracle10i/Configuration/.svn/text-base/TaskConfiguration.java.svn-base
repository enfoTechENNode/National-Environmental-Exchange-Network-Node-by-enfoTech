package Node.DB.Oracle10i.Configuration;

import com.enfotech.basecomponent.db.IDBAdapter;
import com.enfotech.basecomponent.typelib.xml.XmlAttributeCollection;
import com.enfotech.basecomponent.typelib.xml.XmlDocument;
import com.enfotech.basecomponent.typelib.xml.XmlElement;
import com.enfotech.basecomponent.typelib.xml.XmlNode;
import com.enfotech.basecomponent.typelib.xml.XmlNodeList;
import com.enfotech.basecomponent.utility.security.Cryptography;
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.text.SimpleDateFormat;
import java.util.Hashtable;
import javax.xml.parsers.DocumentBuilder;
import javax.xml.parsers.DocumentBuilderFactory;
import EnfoTech.Task.XjavaClass;
import EnfoTech.Task.Xschedule;
import EnfoTech.Task.Xservice;
import EnfoTech.Task.Xtask;
import oracle.jdbc.OracleResultSet;
import oracle.sql.CLOB;
import org.w3c.dom.Document;
import org.w3c.dom.NodeList;

import Node.Biz.Administration.Schedule;
import Node.DB.Interfaces.Configuration.ITaskConfiguration;
import Node.DB.Oracle.NodeDB;
import Node.Phrase;
import Node.Utils.Utility;
import org.apache.log4j.Level;
import Node.Utils.LoggingUtils;
/**
 * <p>This class create TaskConfiguration.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class TaskConfiguration extends NodeDB implements ITaskConfiguration {
  private Hashtable Hash = null;

  private String TableName = "SYS_CONFIG";
  private String ConfigName = "CONFIG_NAME";
  private String ConfigXML = "CONFIG_XML";
  private String UpdatedDate = "UPDATED_DTTM";

  private Document Config = null;

	/**
	 * constructor.
	 * @param loggerName.
	 * @return 
	 * @deprecated
	 */
  public TaskConfiguration(String loggerName) {
    super(loggerName);
    this.Hash = new Hashtable();
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      //String sql = this.GetSelectStr(new String[]{this.ConfigXML},this.TableName,new String[]{this.ConfigName},new String[]{"task.config"});
      String sql = this.GetXDBSelectStr(this.ConfigXML,new String[]{"/Configuration"},new String[]{"Config"},new String[]{"getCLOBVal()"},this.TableName,new String[]{this.ConfigName},new String[]{"task.config"});
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      if (rs != null && rs.last() && rs.getRow() == 1) {
        CLOB clob = ((OracleResultSet)rs).getCLOB("Config");
        DocumentBuilderFactory factory = DocumentBuilderFactory.newInstance();
        DocumentBuilder builder = factory.newDocumentBuilder();
        this.Config = builder.parse(clob.getAsciiStream());
      }
    } catch (Exception e) {
      this.LogException("Could Not Get task.config: " + e.toString());
    } finally {
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
  }

	/**
	 * Init.
	 * @param 
	 * @return 
	 * @deprecated
	 */
  private void Init ()
  {
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String[] xpaths = new String[] {
        "/Configuration/AutoMail/EmailTemplate/Sender/Name/text()","/Configuration/AutoMail/EmailTemplate/Sender/EmailAddress/text()",
        "/Configuration/AutoMail/EmailTemplate/Sender/Credentials/UserID/text()",
        "/Configuration/AutoMail/EmailTemplate/Sender/Credentials/Password/text()",
        "/Configuration/AutoMail/EmailTemplate/@template"
      };
      String[] names = new String[] {
        "SenderName","SenderEmail","SenderUID","SenderPWD","EmailTemplate"
      };
      String sql = this.GetXDBSelectStr(this.ConfigXML,xpaths,names,this.TableName,new String[]{this.ConfigName},new String[]{"task.config"});
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      if (rs != null && rs.last() && rs.getRow() == 1) {
        String temp = rs.getString("SenderName");
        if (temp != null)
          this.Hash.put("SenderName",temp);
        temp = rs.getString("SenderEmail");
        if (temp != null)
          this.Hash.put("SenderEmail",temp);
        temp = rs.getString("SenderUID");
        if (temp != null)
          this.Hash.put("SenderUID",temp);
        temp = rs.getString("SenderPWD");
        if (temp != null)
          this.Hash.put("SenderPWD",temp);
        temp = rs.getString("EmailTemplate");
        if (temp != null)
          this.Hash.put("EmailTemplate", temp);
      }
      if (rs != null)
        rs.close();
      this.SetCCLists(db);
    } catch (Exception e) {
      this.LogException("Could Not Get task.config Values: " + e.toString());
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
  }

	/**
	 * SetCCLists.
	 * @param db.
	 * @return 
	 * @deprecated
	 */
  private void SetCCLists (IDBAdapter db)
  {
    ResultSet rs = null;
    try {
      String sql = this.GetXDBSelectStr(this.ConfigXML,new String[]{"/Configuration/AutoMail/EmailTemplate/To"},
                                        new String[]{"CCList"},new String[]{"getStringVal()"},this.TableName,
                                        new String[]{this.ConfigName},new String[]{"task.config"});
      rs = db.GetResultSet(sql);
      if (rs != null && rs.last() && rs.getRow() == 1) {
        String ccListStr = rs.getString("CCList");
        if (ccListStr != null) {
          XmlDocument ccDoc = new XmlDocument();
          ccDoc.LoadXml(ccListStr);
          XmlNodeList list = ccDoc.SelectNodes("/To/CC");
          if (list != null && list.Count() > 0)
            for (int i = 0; i < list.Count(); i++)
              this.Hash.put("CCList" + i,list.ItemOf(i).GetInnerText());
          list = ccDoc.SelectNodes("/To/BCC");
          if (list != null && list.Count() > 0)
            for (int i = 0; i < list.Count(); i++)
              this.Hash.put("BCCList" + i,list.ItemOf(i).GetInnerText());
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
	 * GetTasks.
	 * @param db.
	 * @return Xtask[]
	 * @deprecated
	 */
  public Xtask[] GetTasks ()
  {
    Xtask[] tasks = null;
    if (this.Config != null) {
      NodeList list = this.Config.getElementsByTagName("task");
      if (list != null && list.getLength() > 0) {
        tasks = new Xtask [list.getLength()];
        for (int i = 0; i < list.getLength(); i++)
          tasks[i] = new Xtask(list.item(i));
      }
    }
    return tasks;
  }

	/**
	 * GetService.
	 * @param serviceName.
	 * @return Xservice
	 * @deprecated
	 */
  public Xservice GetService (String serviceName)
  {
    Xservice service = null;
    if (this.Config != null) {
      NodeList list = this.Config.getElementsByTagName("service");
      if (list != null && list.getLength() > 0) {
        for (int i = 0; i < list.getLength(); i++) {
          Xservice temp = new Xservice(list.item(i));
          if (temp.getName().equals(serviceName)) {
            service = temp;
            break;
          }
        }
      }
    }
    return service;
  }

	/**
	 * GetJavaClass.
	 * @param name.
	 * @return XjavaClass
	 * @deprecated
	 */
  public XjavaClass GetJavaClass (String name)
  {
    XjavaClass jClass = null;
    if (this.Config != null) {
      NodeList list = this.Config.getElementsByTagName("javaClass");
      if (list != null && list.getLength() > 0) {
        for (int i = 0; i < list.getLength(); i++) {
          XjavaClass temp = new XjavaClass(list.item(i));
          if (temp.getUniqueName().equals(name)) {
            jClass = temp;
            break;
          }
        }
      }
    }
    return jClass;
  }

	/**
	 * GetSchedule.
	 * @param name.
	 * @return Xschedule
	 * @deprecated
	 */
  public Xschedule GetSchedule (String name)
  {
    Xschedule schedule = null;
    if (this.Config != null) {
      NodeList list = this.Config.getElementsByTagName("schedule");
      if (list != null && list.getLength() > 0) {
        for (int i = 0; i < list.getLength(); i++) {
          Xschedule temp = new Xschedule(list.item(i));
          if (temp.getUniqueName().equals(name)) {
            schedule = temp;
            break;
          }
        }
      }
    }
    return schedule;
  }

	/**
	 * GetUpdatedDTTM.
	 * @param 
	 * @return long
	 * @deprecated
	 */
  public long GetUpdatedDTTM ()
  {
    long retLong = -1;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String sql = this.GetSelectStr(new String[]{this.UpdatedDate},this.TableName,new String[]{this.ConfigName},new String[]{"task.config"});
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      if (rs != null && rs.last() && rs.getRow() == 1) {
        String d = rs.getString(this.UpdatedDate);
        SimpleDateFormat sdf = new SimpleDateFormat("MM/dd/yyyy HH:mm:ss");
        java.util.Date date = sdf.parse(d);
        if (date != null)
          retLong = date.getTime();
      }
    } catch (Exception e) {
      this.LogException("Could Not Get Updated DTTM: " + e.toString());
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
    return retLong;
  }

	/**
	 * GetTaskEmailSender.
	 * @param 
	 * @return String
	 * @deprecated
	 */
  public String GetTaskEmailSender ()
  {
    return (String)this.Hash.get("SenderName");
  }

	/**
	 * GetTaskEmailSenderEmail.
	 * @param 
	 * @return String
	 * @deprecated
	 */
  public String GetTaskEmailSenderEmail ()
  {
    return (String)this.Hash.get("SenderEmail");
  }

	/**
	 * GetTaskEmailUID.
	 * @param 
	 * @return String
	 * @deprecated
	 */
  public String GetTaskEmailUID ()
  {
    return (String)this.Hash.get("SenderUID");
  }

	/**
	 * GetTaskEmailPWD.
	 * @param 
	 * @return String
	 * @deprecated
	 */
 public String GetTaskEmailPWD ()
  {
    String pwd = null;
    try {
      Cryptography crypt = new Cryptography();
      String temp = (String)this.Hash.get("SenderPWD");
      if (temp != null && !temp.equals(""))
        pwd = crypt.Decrypting(temp, Phrase.CryptKey);
    } catch (Exception e) {
      this.LogException("Could Not Decrypt Task Email PWD: " + e.toString());
    }
    return pwd;
  }

	/**
	 * GetTaskEmailCCList.
	 * @param 
	 * @return String[]
	 * @deprecated
	 */
  public String[] GetTaskEmailCCList ()
  {
    String[] retArray = null;
    String[] tempArray = null;
    for (int i = 0; true; i++) {
      String cc = (String)this.Hash.get("CCList" + i);
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
	 * GetTaskEmailBCCList.
	 * @param 
	 * @return String[]
	 * @deprecated
	 */
  public String[] GetTaskEmailBCCList ()
  {
    String[] retArray = null;
    String[] tempArray = null;
    for (int i = 0; true; i++) {
      String bcc = (String)this.Hash.get("BCCList" + i);
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
	 * EmailTemplate.
	 * @param 
	 * @return String
	 * @deprecated
	 */
  public String GetEmailTempate()
  {
    return (String)this.Hash.get("EmailTemplate");
  }

	/**
	 * SaveTaskCCList.
	 * @param ccList.
	 * @return boolean
	 * @deprecated
	 */
  public boolean SaveTaskCCList (String[] ccList)
  {
    boolean retBool = false;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String value = "<To>";
      if (ccList != null)
        for (int i = 0; i < ccList.length; i++)
          value += "<CC>" + ccList[i] + "</CC>";
      String[] bcc = this.GetTaskEmailBCCList();
      if (bcc != null)
        for (int i = 0; i < bcc.length; i++)
          value += "<BCC>" + bcc[i] + "</BCC>";
      value += "</To>";
      String sql = this.GetUpdateXDBString(this.TableName,this.ConfigXML,new String[]{"/Configuration/AutoMail/EmailTemplate/To"},
                                           new String[]{value},new String[]{this.ConfigName},new String[]{"task.config"});
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      retBool = true;
    } catch (Exception e) {
      this.LogException("Could not Save TaskCC List: " + e.toString());
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
	 * SaveTaskBCCList.
	 * @param bccList.
	 * @return boolean
	 * @deprecated
	 */
  public boolean SaveTaskBCCList (String[] bccList)
  {
    boolean retBool = false;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String value = "<To>";
      String[] cc = this.GetTaskEmailCCList();
      if (cc != null)
        for (int i = 0; i < cc.length; i++)
          value += "<CC>" + cc[i] + "</CC>";
      if (bccList != null)
        for (int i = 0; i < bccList.length; i++)
          value += "<BCC>" + bccList[i] + "</BCC>";
      value += "</To>";
      String sql = this.GetUpdateXDBString(this.TableName,this.ConfigXML,new String[]{"/Configuration/AutoMail/EmailTemplate/To"},
                                           new String[]{value},new String[]{this.ConfigName},new String[]{"task.config"});
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      retBool = true;
    } catch (Exception e) {
      this.LogException("Could not Save TaskBCC List: " + e.toString());
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
	 * SaveTaskStatus.
	 * @param myXTask.
	 * @return boolean
	 * @deprecated
	 */
  public boolean SaveTaskStatus (Xtask myXTask)
  {
  boolean retBool = false;
  IDBAdapter db = null;
  ResultSet rs = null;
  try {
    LoggingUtils.Log("TaskConfiguration>>>SaveTaskStatus>>>get into SaveTaskStatus()", Level.DEBUG, Phrase.TaskLoggerName);
    String taskName = myXTask.getName();
    String taskStatus = myXTask.getStatus().substring(0,myXTask.getStatus().indexOf("_"));
    String sql = this.GetUpdateXDBString(this.TableName,this.ConfigXML,new String[]{"//Configuration/scheduledTasks/task[@name=\""+taskName+"\"][@schedule=\""+taskName+"Schedule\"][@service=\""+taskName+"Service\"]/@status"},
                                         new String[]{taskStatus},new String[]{this.ConfigName},new String[]{"task.config"});
    LoggingUtils.Log("TaskConfiguration>>>SaveTaskStatus>>>The update sql is:"+sql, Level.DEBUG, Phrase.TaskLoggerName);
    db = this.GetNodeDB();
    rs = db.GetResultSet(sql);
    retBool = true;
  } catch (Exception e) {
    this.LogException("Could not Save TaskBCC List: " + e.toString());
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
   * Save Task
   * @param taskName String
   * @param className String
   * @param methodName String
   * @param paramNames String[]
   * @param values String[]
   * @param schedule Schedule
   * @return boolean true if successful, false otherwise
	 * @deprecated
   */
  public boolean SaveTask (String taskName, String className, String methodName, String[] paramNames, String[] paramTypes, String[] values, Schedule schedule)
  {
    // debug
    LoggingUtils.Log("TaskConfiguration>>>SaveTask>>>Go into SaveTask.", Level.DEBUG, Phrase.TaskLoggerName);
    // end debug
    boolean retBool = false;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String sql = "select extract("+this.ConfigXML+",'/Configuration').getClobVal() CONFIG,"+this.UpdatedDate;
      sql += " from "+this.TableName+" where "+this.ConfigName+" = 'task.config'";
      LoggingUtils.Log("TaskConfiguration>>>SaveTask>>>The sql is: "+sql, Level.DEBUG, Phrase.TaskLoggerName);
     db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      if (rs != null && rs.last() && rs.getRow() == 1) {
        rs.updateDate(this.UpdatedDate,Utility.GetNowDate());
        rs.updateTimestamp(this.UpdatedDate,Utility.GetNowTimeStamp());
        rs.updateRow();
        OracleResultSet ors = (OracleResultSet)rs;
        String xml = this.GetClobString(ors.getCLOB("CONFIG"));
        String updated = this.UpdateTaskConfig(xml,taskName,className,methodName,paramNames,paramTypes,values,schedule);
        if (updated != null) {
          rs.close();
          Connection conn = db.GetConnection();
          sql = "update "+this.TableName+" set "+this.ConfigXML+" = XMLType(?),"+this.UpdatedBy+" = 'system',CONFIG_CLOB=?";
          sql += " where "+this.ConfigName+" = 'task.config'";
          PreparedStatement statement = conn.prepareStatement(sql);
          statement.setObject(1,this.CreateCLOB(db,updated));
          statement.setObject(2,this.CreateCLOB(db,updated));
          if (statement.executeUpdate() == 1)
            retBool = true;
        }
      }
      retBool = true;
    } catch (Exception e) {
      java.io.StringWriter sw = new java.io.StringWriter();
      e.printStackTrace(new java.io.PrintWriter(sw));
      try { sw.close(); } catch (Exception ex) { }
      this.LogException("Could not Save task.config: " + sw.toString());
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
   * Save Task without updating date
   * @param taskName String
   * @param className String
   * @param methodName String
   * @param paramNames String[]
   * @param values String[]
   * @param schedule Schedule
   * @return boolean true if successful, false otherwise
	 * @deprecated
   */
  public boolean SaveTaskWithoutUpdatingDate (String taskName, String className, String methodName, String[] paramNames, String[] paramTypes, String[] values, Schedule schedule)
  {
    // debug
    //System.out.println("**** SaveTaskWithoutUpdatingDate invoked. ****");
    // end debug
    boolean retBool = false;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String sql = "select extract("+this.ConfigXML+",'/Configuration').getClobVal() CONFIG,"+this.UpdatedDate;
      sql += " from "+this.TableName+" where "+this.ConfigName+" = 'task.config'";
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      if (rs != null && rs.last() && rs.getRow() == 1) {
//        rs.updateDate(this.UpdatedDate,Utility.GetNowDate());
//        rs.updateTimestamp(this.UpdatedDate,Utility.GetNowTimeStamp());
        rs.updateRow();
        OracleResultSet ors = (OracleResultSet)rs;
        String xml = this.GetClobString(ors.getCLOB("CONFIG"));
        String updated = this.UpdateTaskConfig(xml,taskName,className,methodName,paramNames,paramTypes,values,schedule);
        if (updated != null) {
          rs.close();
          Connection conn = db.GetConnection();
          sql = "update "+this.TableName+" set "+this.ConfigXML+" = XMLType(?),"+this.UpdatedBy+" = 'system',CONFIG_CLOB=?";
          sql += " where "+this.ConfigName+" = 'task.config'";
          PreparedStatement statement = conn.prepareStatement(sql);
          statement.setObject(1,this.CreateCLOB(db,updated));
          statement.setObject(2,this.CreateCLOB(db,updated));
          if (statement.executeUpdate() == 1)
            retBool = true;
        }
      }
      retBool = true;
    } catch (Exception e) {
      java.io.StringWriter sw = new java.io.StringWriter();
      e.printStackTrace(new java.io.PrintWriter(sw));
      try { sw.close(); } catch (Exception ex) { }
      this.LogException("Could not Save task.config: " + sw.toString());
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
   * Delete Task
   * @param taskName String
   * @return boolean true if successful, false otherwise
	 * @deprecated
   */
  public boolean DeleteTask (String taskName)
  {
    boolean retBool = false;
    IDBAdapter db = null;
    ResultSet rs = null;
    try {
      String sql = "select extract("+this.ConfigXML+",'/Configuration').getClobVal() CONFIG,"+this.UpdatedDate;
      sql += " from "+this.TableName+" where "+this.ConfigName+" = 'task.config'";
      db = this.GetNodeDB();
      rs = db.GetResultSet(sql);
      if (rs != null && rs.last() && rs.getRow() == 1) {
        rs.updateDate(this.UpdatedDate,Utility.GetNowDate());
        rs.updateTimestamp(this.UpdatedDate,Utility.GetNowTimeStamp());
        rs.updateRow();
        OracleResultSet ors = (OracleResultSet)rs;
        String replace = this.DeleteTask(this.GetClobString(ors.getCLOB("CONFIG")),taskName);
        rs.close();
        if (replace != null) {
          rs.close();
          Connection conn = db.GetConnection();
          sql = "update "+this.TableName+" set "+this.ConfigXML+" = XMLType(?),"+this.UpdatedBy+" = 'system'";
          sql += " where "+this.ConfigName+" = 'task.config'";
          PreparedStatement statement = conn.prepareStatement(sql);
          statement.setObject(1,this.CreateCLOB(db,replace));
          if (statement.executeUpdate() == 1)
            retBool = true;
        }
      }
      retBool = true;
    } catch (Exception e) {
      this.LogException("Could not Delete Task: " + e.toString());
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
   * UpdateTaskConfig
   * @param original
   * @param taskName
   * @param className
   * @param methodName
   * @param paramNames
   * @param paramTypes
   * @param values
   * @param schedule
   * @return String
	 * @deprecated
   */
  private String UpdateTaskConfig (String original, String taskName, String className, String methodName, String[] paramNames, String[] paramTypes, String[] values, Schedule schedule) throws Exception
  {
    XmlDocument doc = new XmlDocument();
    doc.LoadXml(original);

    XmlNode classSetting = null;
    XmlNode servicesSetting = null;
    XmlNode scheduledTasks = null;

    String scheduleName = null;
    String serviceName = null;
    String classBlockName = null;
    XmlNodeList list = doc.SelectNodes("/Configuration/scheduledTasks/task");
    if (list != null && list.Count() > 0) {
      for (int i = 0; i < list.Count(); i++) {
        XmlAttributeCollection coll = list.ItemOf(i).Attributes();
        /*changed by charlie zhang 2007-9-13 begin */
        if (coll.Count() > 0 && coll.GetNamedItem("name") != null) {
          if (coll.GetNamedItem("name").GetValue().equals(taskName)) {
            if (schedule.GetType().equalsIgnoreCase(schedule.TYPE_INACTIVE))
              coll.GetNamedItem("status").SetValue("I_C");
            else
              coll.GetNamedItem("status").SetValue("A_C");
            Node.Utils.LoggingUtils.Log("TaskConfiguration>>>UpdateTaskConfig>>>coll.GetNamedItem(\"name\") is: "+coll.GetNamedItem("name").GetValue(), Level.DEBUG, Phrase.AdministrationLoggerName);
            Node.Utils.LoggingUtils.Log("TaskConfiguration>>>UpdateTaskConfig>>>coll.GetNamedItem(\"status\").GetValue() is: "+coll.GetNamedItem("status").GetValue(), Level.DEBUG, Phrase.AdministrationLoggerName);
            serviceName = coll.GetNamedItem("service").GetValue();
            scheduleName = coll.GetNamedItem("schedule").GetValue();
            break;
          }
          /*changed by charlie zhang 2007-9-13 end */
        }
      }
    }
    servicesSetting = doc.SelectSingleNode("/Configuration/ServicesSetting");
    if (serviceName != null && scheduleName != null) {
      list = doc.SelectNodes("/Configuration/ServicesSetting/service");
      if (list != null && list.Count() > 0) {
        for (int i = 0; i < list.Count(); i++) {
          XmlNode node = list.ItemOf(i);
          XmlAttributeCollection coll = node.Attributes();
          if (coll.Count() > 0 && coll.GetNamedItem("name") != null) {
            if (coll.GetNamedItem("name").GetValue().equals(serviceName)) {
              classBlockName = coll.GetNamedItem("service").GetValue();
              XmlElement element = this.GetServiceBlock(doc,serviceName,classBlockName,methodName,paramNames,paramTypes,values);
              servicesSetting.InsertBefore(element,node);
              servicesSetting.RemoveChild(node);
              break;
            }
          }
        }
      }
      scheduledTasks = doc.SelectSingleNode("/Configuration/scheduledTasks");
      list = doc.SelectNodes("/Configuration/scheduledTasks/schedule");
      if (list != null && list.Count() > 0) {
        for (int i = 0; i < list.Count(); i++) {
          XmlNode node = list.ItemOf(i);
          XmlAttributeCollection coll = node.Attributes();
          if (coll.Count() > 0 && coll.GetNamedItem("uniqueName") != null) {
            if (coll.GetNamedItem("uniqueName").GetValue().equals(scheduleName)) {
              XmlElement element = this.GetScheduleBlock(doc,scheduleName,schedule);
              scheduledTasks.InsertBefore(element,node);
              scheduledTasks.RemoveChild(node);
              break;
            }
          }
        }
      }
      classSetting = doc.SelectSingleNode("/Configuration/classSetting");
      list = doc.SelectNodes("/Configuration/classSetting/javaClass");
      if (list != null && list.Count() > 0) {
        for (int i = 0; i < list.Count(); i++) {
          XmlAttributeCollection coll = list.ItemOf(i).Attributes();
          if (coll.Count() > 0 && coll.GetNamedItem("uniqueName") != null) {
            if (coll.GetNamedItem("uniqueName").GetValue().equals(classBlockName)) {
              coll.GetNamedItem("class").SetValue(className);
              break;
            }
          }
        }
      }
    }
    else {
      // Class Setting
      XmlElement classBlock = doc.CreateElement("javaClass");
      classBlock.SetAttribute("class",className);
      classBlock.SetAttribute("status","A");
      classBlock.SetAttribute("uniqueName",taskName+"Class");
      classSetting = doc.SelectSingleNode("/Configuration/classSetting");
      classSetting.AppendChild(classBlock);

      // Service Setting
      XmlElement service = this.GetServiceBlock(doc,taskName+"Service",taskName+"Class",methodName,paramNames,paramTypes,values);
      servicesSetting = doc.SelectSingleNode("/Configuration/ServicesSetting");
      servicesSetting.AppendChild(service);

      // Schedule
      XmlElement scheduleElement = this.GetScheduleBlock(doc,taskName+"Schedule",schedule);
      scheduledTasks = doc.SelectSingleNode("/Configuration/scheduledTasks");
      XmlNodeList schedules = doc.SelectNodes("/Configuration/scheduledTasks/schedule");
      if (schedules != null && schedules.Count() > 0) {
        XmlNode node = schedules.ItemOf(schedules.Count()-1);
        scheduledTasks.InsertAfter(scheduleElement,node);
      }
      else
        scheduledTasks.AppendChild(scheduleElement);

      // Task
      XmlElement taskElement = this.GetTaskBlock(doc,taskName,taskName+"Schedule",taskName+"Service",schedule.GetType().equalsIgnoreCase(schedule.TYPE_INACTIVE)?"I":"A");
      scheduledTasks.AppendChild(taskElement);
    }
    return doc.OuterXml();
  }

  /**
   * Delete Task
   * @param original
   * @param taskName
   * @return String
	 * @deprecated
   */
  private String DeleteTask (String original, String taskName) throws Exception
  {
    XmlDocument doc = new XmlDocument();
    doc.LoadXml(original);

    String serviceName = null;
    String scheduleName = null;
    String className = null;

    XmlNode scheduledTasks = doc.SelectSingleNode("/Configuration/scheduledTasks");
    XmlNodeList taskList = scheduledTasks.SelectNodes("task");
    if (taskList != null) {
      for (int i = 0; i < taskList.Count(); i++) {
        XmlNode task = taskList.ItemOf(i);
        if (task != null) {
          XmlAttributeCollection collection = task.Attributes();
          if (collection != null) {
            XmlNode attr = collection.GetNamedItem("name");
            if (attr != null && attr.GetValue().equals(taskName)) {
              serviceName = collection.GetNamedItem("service").GetValue();
              scheduleName = collection.GetNamedItem("schedule").GetValue();
              scheduledTasks.RemoveChild(task);
              break;
            }
          }
        }
      }
    }

    if (scheduleName != null) {
      XmlNodeList scheduleList = scheduledTasks.SelectNodes("schedule");
      if (scheduleList != null) {
        for (int i = 0; i < scheduleList.Count(); i++) {
          XmlNode schedule = scheduleList.ItemOf(i);
          if (schedule != null) {
            XmlAttributeCollection coll = schedule.Attributes();
            if (coll != null) {
              XmlNode attr = coll.GetNamedItem("uniqueName");
              if (attr != null && attr.GetValue().equals(scheduleName)) {
                scheduledTasks.RemoveChild(schedule);
                break;
              }
            }
          }
        }
      }
    }

    XmlNode servicesSetting = doc.SelectSingleNode("/Configuration/ServicesSetting");
    if (serviceName != null) {
      XmlNodeList serviceList = servicesSetting.SelectNodes("service");
      if (serviceList != null) {
        for (int i = 0; i < serviceList.Count(); i++) {
          XmlNode service = serviceList.ItemOf(i);
          if (service != null) {
            XmlAttributeCollection coll = service.Attributes();
            if (coll != null) {
              XmlNode attr = coll.GetNamedItem("name");
              if (attr != null && attr.GetValue().equals(serviceName)) {
                className = coll.GetNamedItem("service").GetValue();
                servicesSetting.RemoveChild(service);
                break;
              }
            }
          }
        }
      }
    }

    XmlNode classSetting = doc.SelectSingleNode("/Configuration/classSetting");
    if (className != null) {
      XmlNodeList javaClassList = classSetting.SelectNodes("javaClass");
      if (javaClassList != null) {
        for (int i = 0; i < javaClassList.Count(); i++) {
          XmlNode javaClass = javaClassList.ItemOf(i);
          if (javaClass != null) {
            XmlAttributeCollection coll = javaClass.Attributes();
            if (coll != null) {
              XmlNode attr = coll.GetNamedItem("uniqueName");
              if (attr != null && attr.GetValue().equals(className)) {
                classSetting.RemoveChild(javaClass);
                break;
              }
            }
          }
        }
      }
    }
    return doc.OuterXml();
  }

  /**
   * InsertClass
   * @param block
   * @param name
   * @param className
   * @return String
	 * @deprecated
   */
  private String InsertClass (String block, String name, String className) throws Exception
  {
    XmlDocument doc = new XmlDocument();
    doc.LoadXml(block);
    XmlElement classSetting = doc.DocumentElement();
    XmlElement javaClass = doc.CreateElement("javaClass");
    javaClass.SetAttribute("class",className);
    javaClass.SetAttribute("status","A");
    javaClass.SetAttribute("uniqueName",name);
    classSetting.AppendChild(javaClass);
    return "<classSetting>"+classSetting.InnerXml()+"</classSetting>";
  }

  /**
   * GetServiceBlock
   * @param doc
   * @param name
   * @param className
   * @param methodName
   * @param paramNames
   * @param paramTypes
   * @param values
   * @return String
	 * @deprecated
   */
  private XmlElement GetServiceBlock (XmlDocument doc, String name, String className, String methodName, String[] paramNames, String[] paramTypes, String[] values) throws Exception
  {
    XmlElement root = null;
    if (doc == null) {
      doc = new XmlDocument();
      doc.LoadXml("<service></service>");
      root = doc.DocumentElement();
    }
    else
      root = doc.CreateElement("service");
    root.SetAttribute("name",name);
    root.SetAttribute("method",methodName);
    root.SetAttribute("service",className);
    root.SetAttribute("type","javaClass");
    int count = 1;
    if (paramNames != null && paramNames.length > 0 && values != null && values.length == paramNames.length &&
        paramTypes != null && paramTypes.length == paramNames.length) {
      for (int i = 0; i < paramNames.length; i++) {
        XmlElement element = doc.CreateElement("serviceParm");
        int temp = i + 1;
        element.SetAttribute("key",temp+"");
        element.SetAttribute("name",paramNames[i]);
        element.SetAttribute("type",paramTypes[i]);
        element.SetAttribute("value",values[i]);
        root.AppendChild(element);
        count++;
      }
    }
    XmlElement element = doc.CreateElement("returnParm");
    element.SetAttribute("key",count+"");
    element.SetAttribute("name","return");
    element.SetAttribute("type","string");
    root.AppendChild(element);
    return root;
  }

  /**
   * InsertServiceBlock
   * @param block
   * @param name
   * @param className
   * @param methodName
   * @param paramNames
   * @param paramTypes
   * @param values
   * @return String
	 * @deprecated
   */
  private String InsertServiceBlock (String block, String name, String className, String methodName, String[] paramNames, String[] paramTypes, String[] values) throws Exception
  {
    XmlDocument servicesSetting = new XmlDocument();
    servicesSetting.LoadXml(block);
    XmlElement element = this.GetServiceBlock(servicesSetting, name, className, methodName, paramNames, paramTypes, values);
    servicesSetting.AppendChild(element);
    return "<ServicesSetting>"+servicesSetting.InnerXml()+"</ServicesSetting>";
  }

  /**
   * GetScheduleBlock
   * @param doc
   * @param name
   * @param schedule
   * @return XmlElement
	 * @deprecated
   */
  private XmlElement GetScheduleBlock (XmlDocument doc, String name, Schedule schedule) throws Exception
  {
    XmlElement root = null;
    if (doc == null) {
      doc = new XmlDocument();
      doc.LoadXml("<schedule></schedule>");
      root = doc.DocumentElement();
    }
    else
      root = doc.CreateElement("schedule");
    SimpleDateFormat format = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");
    root.SetAttribute("uniqueName",name);
    String type = schedule.GetType();
    root.SetAttribute("type",type.toLowerCase());
    if (!type.equals(schedule.TYPE_INACTIVE)) {
      root.SetAttribute("startDateTime", format.format(schedule.GetStartDate()));
      root.SetAttribute("endDateTime", format.format(schedule.GetEndDate()));
      if (type.equals(schedule.TYPE_SECONDS) || type.equals(schedule.TYPE_DAYS))
        root.SetAttribute("interval", schedule.GetInterval());
      else if (!type.equals(schedule.TYPE_INACTIVE)) {
        root.SetAttribute("time", schedule.GetTime());
        if (type.equals(schedule.TYPE_WEEKS)) {
          String[] daysOfWeek = schedule.GetDayOfWeek();
          String temp = "";
          for (int i = 0; i < daysOfWeek.length; i++) {
            if (i != 0) temp += ",";
            temp += daysOfWeek[i];
          }
          root.SetAttribute("dayOfWeek", temp);
        }
        else {
          String[] daysOfMonth = schedule.GetDayOfMonth();
          String temp = "";
          for (int i = 0; i < daysOfMonth.length; i++) {
            if (i != 0) temp += ",";
            temp += daysOfMonth[i];
          }
          root.SetAttribute("dayOfMonth", temp);
          if (type.equals(schedule.TYPE_YEARS) || type.equals(schedule.TYPE_ONCE)) {
            String[] monthsOfYear = schedule.GetMonthOfYear();
            temp = "";
            for (int i = 0; i < monthsOfYear.length; i++) {
              if (i != 0) temp += ",";
              temp += monthsOfYear[i];
            }
            root.SetAttribute("monthOfYear", temp);
          }
        }
      }
    }
    return root;
  }

  /**
   * InsertTaskBlock
   * @param block
   * @param scheduleName
   * @param schedule
   * @param taskName
   * @param serviceName
   * @return XmlElement
	 * @deprecated
   */
  private String InsertTaskBlock (String block, String scheduleName, Schedule schedule, String taskName, String serviceName) throws Exception
  {
    XmlDocument doc = new XmlDocument();
    doc.LoadXml(block);
    XmlElement root = doc.DocumentElement();
    root.AppendChild(this.GetScheduleBlock(doc,scheduleName,schedule));
    root.AppendChild(this.GetTaskBlock(doc,taskName,scheduleName,serviceName,schedule.GetType()));
    return "<scheduledTasks>"+root.InnerXml()+"</scheduledTasks>";
  }

  /**
   * GetTaskBlock
   * @param doc
   * @param name
   * @param scheduleName
   * @param serviceName
   * @param status
   * @return XmlElement
	 * @deprecated
   */
  private XmlElement GetTaskBlock (XmlDocument doc, String name, String scheduleName, String serviceName, String status) throws Exception
  {
    XmlElement root = null;
    if (doc == null) {
      doc = new XmlDocument();
      doc.LoadXml("<task></task>");
      root = doc.DocumentElement();
    }
    else
      root = doc.CreateElement("task");
    root.SetAttribute("name",name);
    root.SetAttribute("schedule",scheduleName);
    root.SetAttribute("service",serviceName);
    root.SetAttribute("status",status.equalsIgnoreCase("INACTIVE")?"I":"A");
    return root;
  }

  /**
   * EnterValueArray
   * @param original
   * @param value
   * @param index
   * @return String[]
	 * @deprecated
   */
  private String[] EnterValueArray (String[] original, String value, int index)
  {
    if (original == null || original.length == 0)
      return new String[] {
          value};
    if (index < original.length) {
      original[index] = value;
      return original;
    }
    String[] retArray = new String[original.length * 2];
    for (int i = 0; i < original.length; i++)
      retArray[i] = original[i];
    retArray[original.length] = value;
    return retArray;
  }
}
