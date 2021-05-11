package Node.Biz.Administration;

import java.io.Serializable;
import java.sql.Date;
import java.util.ArrayList;
import java.util.TreeMap;

import org.apache.log4j.Level;

import DataFlow.Component.Interface.WebServiceParameter;
import Node.Phrase;
import Node.API.WebServiceParameterMode;
import Node.DB.DBManager;
import Node.DB.Interfaces.INodeOperation;
import Node.DB.Interfaces.Configuration.ITaskConfiguration;
import Node.Utils.LoggingUtils;

import com.enfotech.basecomponent.typelib.xml.XmlDocument;
import com.enfotech.basecomponent.typelib.xml.XmlElement;
import com.enfotech.basecomponent.typelib.xml.XmlNode;
import com.enfotech.basecomponent.typelib.xml.XmlNodeList;
import com.enfotech.basecomponent.utility.security.Cryptography;

/**
 * <p>This class create Operation class.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class Operation implements Serializable{
  /**
	 * 
	 */
  private static final long serialVersionUID = 744421917784941669L;
  public static int SUCCESS = 0;
  public static int DATABASE_ERROR = -1;
  public static int CODE_VALIDATION_ERROR = -2;

  private int OpID = -1;
  private String Domain = null;
  private String WebService = null;
  private String OpName = null;
  private String Description = null;
  private String Type = null;
  private XmlDocument Config = null;
  private String Status = null;
  private String Message = null;
  private boolean IsDefaultProcess = false;
  private String ClassName = null;
  private String MethodName = null;
  private String[] ParamNames = null;
  private String[] ParamTypes = null;
  private String[] ParamValues = null;
  private Schedule TaskSchedule = null;
  private Date CreatedDate = null;
  private String CreatedBy = null;
  private Date UpdatedDate = null;
  private String UpdatedBy = null;
  private String version = null;
  // WI 21296
  private ArrayList WebServiceParameters = null;
  private String isPublish = null;
  // WI 33641
  private String isRest = null;

  /**
   * Constructor.
   * @param opID.
    * @return 
   */
  public Operation (int opID)
  {
    this.OpID = opID;
  }

  /**
   * Constructor.
   * @param opName.
   * @param wsName.
    * @return 
   */
  public Operation (String opName, String wsName)
  {
    this.OpName = opName;
    this.WebService = wsName;
  }

  /**
   * Constructor.
   * @param opID.
   * @param loggerName.
    * @return 
   */
  public Operation (int opID, String loggerName) throws Exception
  {
    Operation op = null;
    if (opID >= 0) {
      INodeOperation opDB = DBManager.GetNodeOperation(loggerName);
      op = opDB.GetOperation(opID);
    }
    if (op != null)
      this.Init(op);
    else
      this.OpID = opID;
  }

  /**
   * Constructor.
   * @param opName.
   * @param webServiceName.
   * @param loggerName.
    * @return 
   */
  public Operation (String opName, String webServiceName, String loggerName) throws Exception
  {
    Operation op = null;
    if (opName != null) {
      INodeOperation opDB = DBManager.GetNodeOperation(loggerName);
      op = opDB.GetOperation(opName,webServiceName);
    }
    if (op != null)
      this.Init(op);
    else
      this.OpName = opName;
  }

  /**
   * Initial Class.
   * @param op.
   * @return 
   */
  private void Init (Operation op)
  {
    this.CreatedBy = op.GetCreatedBy();
    this.CreatedDate = op.GetCreatedDate();
    this.Description = op.GetDescription();
    this.Domain = op.GetDomain();
    this.Message = op.GetMessage();
    this.OpID = op.GetOperationID();
    this.OpName = op.GetOpName();
    this.Status = op.GetStatus();
    this.Type = op.GetType();
    this.UpdatedBy = op.GetUpdatedBy();
    this.UpdatedDate = op.GetUpdatedDate();
    if (this.Type != null && this.Type.equals(Phrase.WEB_SERVICE_OPERATION)) {
      this.WebService = op.GetWebService();
      // WI 21296
      this.isPublish = op.getIsPublish();
      // WI 33641
      this.isRest = op.getIsRest();

      this.ParamNames = op.GetParamNames();
      this.SetConfig(op.GetConfig());
      try {
        this.IsDefaultProcess = this.IsDefault(this.GetProcessClass());
      }
      catch (Exception e) {}
      // WI 21296
      this.WebServiceParameters = op.getWebServiceParameters();
    }
    else if (this.Type != null && this.Type.equals(Phrase.SCHEDULED_TASK_OPERATION)) {
      this.SetConfig(op.GetConfig());
      this.ClassName = op.GetClassName();
      this.MethodName = op.GetMethodName();
      this.ParamNames = op.GetParamNames();
      this.ParamTypes = op.GetParamTypes();
      this.ParamValues = op.GetParamValues();
      this.TaskSchedule = op.GetTaskSchedule();
    }
  }

  public void SetOperationID (int input)
  {
    this.OpID = input;
  }
  public int GetOperationID ()
  {
    return this.OpID;
  }

  public void SetDomain (String input)
  {
    this.Domain = input;
  }
  public String GetDomain ()
  {
    return this.Domain;
  }

  public void SetWebService (String input)
  {
    this.WebService = input;
  }
  public String GetWebService ()
  {
    return this.WebService;
  }

  public void SetOpName (String input)
  {
    this.OpName = input;
  }
  public String GetOpName ()
  {
    return this.OpName;
  }

  public void SetDescription (String input)
  {
    this.Description = input;
  }
  public String GetDescription ()
  {
    return this.Description;
  }

  public void SetType (String input)
  {
    this.Type = input;
  }
  public String GetType ()
  {
    return this.Type;
  }

  public void SetConfig (String input)
  {
    try {
      if (input != null) {
        this.Config = new XmlDocument();
        this.Config.LoadXml(input);
      }
      else
        this.Config = null;
    } catch (Exception e) {
      this.Config = null;
    }
  }
  public String GetConfig ()
  {
    String retString = null;
    try {
      retString = this.Config.OuterXml();
    } catch (Exception e) {
    }
    return retString;
  }

  public void SetStatus (String input)
  {
    this.Status = input;
  }
  public String GetStatus ()
  {
    return this.Status;
  }

  public void SetMessage (String input)
  {
    this.Message = input;
  }
  public String GetMessage ()
  {
    return this.Message;
  }

  /**
 * @return the isPublish
 */
public String getIsPublish() {
	return isPublish;
}

/**
 * @param isPublish the isPublish to set
 */
public void setIsPublish(String isPublish) {
	this.isPublish = isPublish;
}

/**
 * @return the isRest
 */
public String getIsRest() {
	return isRest;
}

/**
 * @param isRest the isRest to set
 */
public void setIsRest(String isRest) {
	this.isRest = isRest;
}

/**
   * Is Default Process.
   * @return success
   */
  public boolean IsDefaultProcess () throws Exception
  {
    boolean retBool = false;
    if (this.Config != null) {
      XmlNode process = this.Config.SelectSingleNode("/Operation/Process/ClassName");
      if (process != null)
        retBool = this.IsDefault(process.GetInnerText());
    }
    return retBool;
  }

  /**
   * Set Logging Level.
   * @param level
   * @return
   */
  public void SetLoggingLevel (Level level) throws Exception
  {
    if (this.Config != null && level != null) {
      XmlNode node = this.Config.SelectSingleNode("/Operation/LoggingLevel");
      if (node != null)
        node.SetInnerText(level.toString());
      else {
        node = this.Config.SelectSingleNode("/Operation");
        XmlElement logLevel = this.Config.CreateElement("LoggingLevel");
        logLevel.SetInnerText(level.toString());
        XmlNode after = node.SelectSingleNode("PreProcess");
        if (after == null)
          after = node.SelectSingleNode("Process");
        node.InsertBefore(logLevel,after);
      }
    }
  }
  /**
   * Get Logging Level.
   * @return Level
   */
  public Level GetLoggingLevel () throws Exception
  {
    if (this.Type != null && this.Type.equalsIgnoreCase(Phrase.WEB_SERVICE_OPERATION) && this.Config != null) {
      XmlNode node = this.Config.SelectSingleNode("/Operation/LoggingLevel");
      if (node != null)
        return LoggingUtils.ParseLevel(node.GetInnerText());
      else
        return Level.DEBUG;
    }
    else if (this.Type != null && this.Type.equalsIgnoreCase(Phrase.SCHEDULED_TASK_OPERATION))
      return Level.DEBUG;
    return null;
  }

  public void SetClassName (String input)
  {
    this.ClassName = input;
  }
  public String GetClassName ()
  {
    return this.ClassName;
  }

  public void SetMethodName (String input)
  {
    this.MethodName = input;
  }
  public String GetMethodName ()
  {
    return this.MethodName;
  }

  public void SetParamNames (String[] input)
  {
    this.ParamNames = input;
  }
  public String[] GetParamNames ()
  {
    return this.ParamNames;
  }

  public void SetParamTypes (String[] input)
  {
    this.ParamTypes = input;
  }
  public String[] GetParamTypes ()
  {
    return this.ParamTypes;
  }

  public void SetParamValues (String[] input)
  {
    this.ParamValues = input;
  }
  public String[] GetParamValues ()
  {
    return this.ParamValues;
  }

  /**
   * Set Task Parameters.
   * @param list
   * @return 
   */
  public void SetTaskParameters (ArrayList list)
  {
    if (list != null && list.size() > 0) {
      this.ParamNames = new String[list.size()];
      this.ParamTypes = new String[list.size()];
      this.ParamValues = new String[list.size()];
      for (int i = 0; i < list.size(); i++) {
        ArrayList temp = (ArrayList)list.get(i);
        if (temp != null && temp.size() >= 3) {
          this.ParamNames[i] = (String)temp.get(1);
          this.ParamTypes[i] = "String";
          this.ParamValues[i] = (String)temp.get(2);
        }
      }
    }
    else {
      this.ParamNames = null;
      this.ParamTypes = null;
      this.ParamValues = null;
    }
  }
  
  public void SetWebServiceParameterNames (ArrayList list)
  {
    if (list != null && list.size() > 0) {
      this.ParamNames = new String[list.size()];
      this.ParamTypes = new String[list.size()];
      this.ParamValues = new String[list.size()];
      for (int i = 0; i < list.size(); i++) {
        ArrayList temp = (ArrayList)list.get(i);
        if (temp != null && temp.size() >= 2) {
          this.ParamNames[i] = (String)temp.get(1);
          this.ParamTypes[i] = "String";
          this.ParamValues[i] = "";
        }
      }
    }
    else {
      this.ParamNames = null;
      this.ParamTypes = null;
      this.ParamValues = null;
    }
  }
  
  public ArrayList GetWebServiceParameterNames ()
  {
    ArrayList retList = null;
    if (this.ParamNames != null && this.ParamNames.length > 0) {
      retList = new ArrayList();
      for (int i = 0; i < this.ParamNames.length; i++) {
        ArrayList temp = new ArrayList();
        int sequence = i + 1;
        temp.add(sequence+"");
        temp.add(this.ParamNames[i]);
        temp.add("");
        retList.add(temp);
      }
    }
    return retList;
  }
  
  public ArrayList GetTaskParameters ()
  {
    ArrayList retList = null;
    if (this.ParamNames != null && this.ParamNames.length > 0 && this.ParamValues != null &&
        this.ParamValues.length == this.ParamNames.length) {
      retList = new ArrayList();
      for (int i = 0; i < this.ParamNames.length; i++) {
        ArrayList temp = new ArrayList();
        int sequence = i + 1;
        temp.add(sequence+"");
        temp.add(this.ParamNames[i]);
        temp.add(this.ParamValues[i]);
        retList.add(temp);
      }
    }
    return retList;
  }

  public void SetTaskSchedule (Schedule input)
  {
    this.TaskSchedule = input;
  }
  /**
   * Set Task Schedule.
   * @param type.
   * @param startDateTime.
   * @param endDateTime.
   * @param interval.
   * @param dayOfWeek.
   * @param dayOfMonth.
   * @param monthOfYear.
   * @return 
   */
  public void SetTaskSchedule (String type, Date startDateTime, Date endDateTime, String interval, String[] dayOfWeek, String[] dayOfMonth, String[] monthOfYear)
  {
    Schedule schedule = new Schedule(type);
    String t = null;
    if ((t = schedule.GetType()) != null) {
      schedule.SetStartDate(startDateTime);
      schedule.SetEndDate(endDateTime);
      if (t.equals(schedule.TYPE_SECONDS) || t.equals(schedule.TYPE_DAYS))
        schedule.SetInterval(interval);
      else if (t.equals(schedule.TYPE_WEEKS))
        schedule.SetDayOfWeek(dayOfWeek);
      else {
        schedule.SetDayOfMonth(dayOfMonth);
        if (t.equals(schedule.TYPE_YEARS) || t.equals(schedule.TYPE_ONCE))
          schedule.SetMonthOfYear(monthOfYear);
      }
    }
  }
  public Schedule GetTaskSchedule ()
  {
    return this.TaskSchedule;
  }

  public void SetCreatedDate (Date input)
  {
    this.CreatedDate = input;
  }
  public Date GetCreatedDate ()
  {
    return this.CreatedDate;
  }

  public void SetCreatedBy (String input)
  {
    this.CreatedBy = input;
  }
  public String GetCreatedBy ()
  {
    return this.CreatedBy;
  }

  public void SetUpdatedDate (Date input)
  {
    this.UpdatedDate = input;
  }
  public Date GetUpdatedDate ()
  {
    return this.UpdatedDate;
  }

  public void SetUpdatedBy (String input)
  {
    this.UpdatedBy = input;
  }
  public String GetUpdatedBy ()
  {
    return this.UpdatedBy;
  }

  public String getVersion() {
	return version;
  }

  public void setVersion(String version) {
	this.version = version;
  }
  
  // WI 21296
  public ArrayList getWebServiceParameters() {
	  return WebServiceParameters;
  }

  public void setWebServiceParameters(ArrayList parameters) {
	  WebServiceParameters = parameters;
  }

/**
   * Get Logger Name.
   * @return LoggerName
   */
  public String GetLoggerName ()
  {
    String loggerName = this.OpName;
    if (this.Type.equalsIgnoreCase(Phrase.WEB_SERVICE_OPERATION))
      loggerName += this.WebService;
    return this.Type.equalsIgnoreCase(Phrase.WEB_SERVICE_OPERATION) ? Phrase.WebServicesLoggerName+"."+loggerName :
        Phrase.TaskLoggerName+"."+loggerName;
  }

  /**
   * Save.
   * @param loggerName
   * @return success
   */
  public boolean Save (String loggerName) throws Exception
  {
    boolean retBool = false;
    if (this.Domain != null && (this.OpID >= 0 || this.OpName != null)) {
      INodeOperation opDB = DBManager.GetNodeOperation(loggerName);
      if (this.OpID >= 0)
    	  // WI 21296,33641
        retBool = opDB.SaveOperation(this.OpID,this.Description,this.Config!=null?this.Config.OuterXml():null,this.Status+";"+this.version+";"+this.isPublish+";"+this.isRest,this.Message);
      else if (this.OpName != null) {
        this.OpID = opDB.SaveOperation(this.OpName,this.Domain,this.WebService,this.Description,this.Type,this.Config!=null?this.Config.OuterXml():null,
                                       this.Status+";"+this.version+";"+this.isPublish+";"+this.isRest,this.Message);
        if (this.OpID >= 0)
          retBool = true;
      }
    }
    if (retBool && this.Type != null && this.Type.equals(Phrase.SCHEDULED_TASK_OPERATION)) {
      ITaskConfiguration taskDB = DBManager.GetTaskConfiguration(loggerName);
      if (this.OpName != null && this.ClassName != null && this.MethodName != null && this.TaskSchedule != null && this.TaskSchedule.ValidateSchedule()){
      //if (this.OpName != null && this.TaskSchedule != null && this.TaskSchedule.ValidateSchedule())
        retBool = taskDB.SaveTask(this.OpName,this.ClassName,this.MethodName,this.ParamNames,this.ParamTypes,this.ParamValues,this.TaskSchedule);
      }else
        retBool = false;
    }
    return retBool;
  }

  /**
   * Save Without Change Task Schedule.
   * @param loggerName
   * @return success
   */
  public boolean SaveWithoutChangeTaskSchedule (String loggerName) throws Exception
  {
    boolean retBool = false;
    if (this.Domain != null && (this.OpID >= 0 || this.OpName != null)) {
      INodeOperation opDB = DBManager.GetNodeOperation(loggerName);
      if (this.OpID >= 0)
        retBool = opDB.SaveOperation(this.OpID,this.Description,this.Config!=null?this.Config.OuterXml():null,this.Status,this.Message);
      else if (this.OpName != null) {
        this.OpID = opDB.SaveOperation(this.OpName,this.Domain,this.WebService,this.Description,this.Type,this.Config!=null?this.Config.OuterXml():null,
                                       this.Status,this.Message);
        if (this.OpID >= 0)
          retBool = true;
      }
    }
    return retBool;
  }

  /**
   * Save Task Without Updating Task ConfigDate.
   * @param loggerName
   * @return success
   */
  public boolean TaskSaveWithoutUpdatingTaskConfigDate(String loggerName) throws Exception
  {
    boolean retBool = false;
    if (this.Domain != null && (this.OpID >= 0 || this.OpName != null)) {
      INodeOperation opDB = DBManager.GetNodeOperation(loggerName);
      if (this.OpID >= 0)
        retBool = opDB.SaveOperation(this.OpID,this.Description,this.Config!=null?this.Config.OuterXml():null,this.Status,this.Message);
      else if (this.OpName != null) {
        this.OpID = opDB.SaveOperation(this.OpName,this.Domain,this.WebService,this.Description,this.Type,this.Config!=null?this.Config.OuterXml():null,
                                       this.Status,this.Message);
        if (this.OpID >= 0)
          retBool = true;
      }
    }
    if (retBool && this.Type != null && this.Type.equals(Phrase.SCHEDULED_TASK_OPERATION)) {
      ITaskConfiguration taskDB = DBManager.GetTaskConfiguration(loggerName);
      if (this.OpName != null && this.ClassName != null && this.MethodName != null && this.TaskSchedule != null && this.TaskSchedule.ValidateSchedule()){
        // debug
        //System.out.println("**** TaskSaveWithoutUpdatingTaskConfigDate: calling SaveTaskWithoutUpdatingDate ****");
        String paramValuesStr = "";
        for(int i = 0; i < this.ParamValues.length; i++){
          if(i == 0){
            paramValuesStr += this.ParamValues[i];
          }else{
            paramValuesStr += ", " + this.ParamValues[i];
          }
        }
        //System.out.println("**** TaskSaveWithoutUpdatingTaskConfigDate: calling SaveTaskWithoutUpdatingDate: paramValues: " + paramValuesStr + " ****");
        // end debug
        retBool = taskDB.SaveTaskWithoutUpdatingDate(this.OpName,this.ClassName,this.MethodName,this.ParamNames,this.ParamTypes,this.ParamValues,this.TaskSchedule);
      }else
        retBool = false;
    }
    return retBool;
  }


  /**
   * Delete.
   * @param loggerName
   * @return success
   */
  public boolean Delete (String loggerName) throws Exception
  {
    boolean retBool = true;
    if (this.Type != null && this.Type.equalsIgnoreCase(Phrase.SCHEDULED_TASK_OPERATION))
      retBool = this.DeleteTask(loggerName,this.OpName);
    INodeOperation opDB = DBManager.GetNodeOperation(loggerName);
    return retBool && opDB.DeleteOperation(this.OpID);
  }

  /**
   * Get PreProcesses.
   * @return PreProcess List
   */
  public ArrayList GetPreProcesses () throws Exception
  {
    ArrayList list = null;
    if (this.Config != null) {
      XmlNode pre = this.Config.SelectSingleNode("/Operation/PreProcess");
      if (pre != null) {
        XmlNodeList nodes = pre.SelectNodes("Sequence");
        if (nodes != null && nodes.Count() > 0) {
          list = new ArrayList();
          for (int i = 0; i < nodes.Count(); i++) {
            XmlNode node = nodes.ItemOf(i);
            ArrayList temp = new ArrayList();
            int num = Integer.parseInt(node.SelectSingleNode("@number").GetInnerText());
            temp.add(0,num+"");
            temp.add(1,node.SelectSingleNode("ClassName").GetInnerText());
            list.add(i,temp);
          }
        }
      }
    }
    return list;
  }

  /**
   * Get Processes.
   * @return PreProcess List
   */
  public String GetProcessClass () throws Exception
  {
    String retString = null;
    if (this.Config != null) {
      XmlNode proc = this.Config.SelectSingleNode("/Operation/Process");
      if (proc != null)
        retString = proc.SelectSingleNode("ClassName").GetInnerText();
    }
    this.IsDefaultProcess = this.IsDefault(retString);
    return retString;
  }

  /**
   * Get WizardFlag.
   * @return Wizard Flag
   */
  public String GetWizardFlag () throws Exception
  {
    String retString = "no";
    if (this.Config != null) {
      XmlNode proc = this.Config.SelectSingleNode("/process");
      if (proc != null)
        retString = "yes";
    }else if(this.Type!= null && this.Type.equalsIgnoreCase(Phrase.WEB_SERVICE_OPERATION)){
    	retString = "yes";
    }else if(this.Type!= null && this.Type.equalsIgnoreCase(Phrase.SCHEDULED_TASK_OPERATION)){
    	retString = "no";
    }else if(this.OpID == -1 && this.GetOpName()!=null && !this.GetOpName().equalsIgnoreCase("")){
    	retString = "yes";
    }
    return retString;
  }

  /**
   * Get Post Processes.
   * @return Post Processes List
   */
  public ArrayList GetPostProcesses () throws Exception
  {
    ArrayList list = null;
    if (this.Config != null) {
      XmlNode post = this.Config.SelectSingleNode("/Operation/PostProcess");
      if (post != null) {
        XmlNodeList nodes = post.SelectNodes("Sequence");
        if (nodes != null && nodes.Count() > 0) {
          list = new ArrayList();
          for (int i = 0; i < nodes.Count(); i++) {
            XmlNode node = nodes.ItemOf(i);
            ArrayList temp = new ArrayList();
            int num = Integer.parseInt(node.SelectSingleNode("@number").GetInnerText());
            temp.add(0,num+"");
            temp.add(1,node.SelectSingleNode("ClassName").GetInnerText());
            list.add(i,temp);
          }
        }
      }
    }
    return list;
  }

  /**
   * Get Solicit Times.
   * @return Solicit Times
   */
  public String[] GetSolicitTimes () throws Exception
  {
    String[] retArray = null;
    if (this.Config != null) {
      XmlNode proc = this.Config.SelectSingleNode("/Operation/Process");
      if (proc != null) {
        XmlNode solicit = proc.SelectSingleNode("Solicit");
        if (solicit != null) {
          XmlNode startTime = solicit.SelectSingleNode("SolicitStartTime");
          XmlNode endTime = solicit.SelectSingleNode("SolicitEndTime");
          if (startTime != null && endTime != null) {
            retArray = new String [2];
            retArray[0] = startTime.GetInnerText();
            retArray[1] = endTime.GetInnerText();
          }
        }
      }
    }
    return retArray;
  }

  /**
   * Get Solicit Submit Credentials.
   * @return Solicit Submit Credentials
   */
   public String[] GetSolicitSubmitCredentials () throws Exception
  {
    String[] retArray = null;
    if (this.Config != null) {
      XmlNode proc = this.Config.SelectSingleNode("/Operation/Process");
      if (proc != null) {
        XmlNode credentials = proc.SelectSingleNode("Solicit/SubmitCredentials");
        if (credentials != null) {
          XmlNode id = credentials.SelectSingleNode("UserID");
          XmlNode pwd = credentials.SelectSingleNode("Password");
          if (id != null && pwd != null) {
            retArray = new String [2];
            retArray[0] = id.GetInnerText();
            Cryptography crypt = new Cryptography();
            String temp = pwd.GetInnerText();
            if (temp != null && !temp.equals(""))
              retArray[1] = crypt.Decrypting(temp,Phrase.CryptKey);
            else
              retArray = null;
          }
        }
      }
    }
    return retArray;
  }

   /**
    * Get Authorization Class Name.
    * @return Authorization Class Name
    */
  public String GetAuthorizationClassName () throws Exception
  {
    String retClassName = null;
    if (this.Config != null)
    {
      XmlNode authClass = this.Config.SelectSingleNode("/Operation/Process/Authorization");
      if (authClass != null)
      {
        XmlNode classNode = authClass.SelectSingleNode("ClassName");
        if (classNode != null && !classNode.GetInnerText().trim().equals(""))
        {
          retClassName = classNode.GetInnerText();
        }
      }
    }
    return retClassName;
  }

  /**
   * Set PreProcesses.
   * @param list
   * @return 
   */
  public void SetPreProcesses (ArrayList list) throws Exception
  {
    if (this.Config == null) {
      this.Config = new XmlDocument();
      this.Config.LoadXml("<Operation></Operation>");
    }
    if (this.Config != null && list != null && list.size() > 0) {
      XmlNode pre = this.Config.SelectSingleNode("PreProcess");
      if (pre == null) {
        pre = this.Config.CreateElement("PreProcess");
        XmlNode proc = this.Config.SelectSingleNode("Process");
        if (proc != null)
          this.Config.InsertBefore(pre,proc);
        else
          this.Config.AppendChild(pre);
      }
      pre.RemoveAll();
      for (int i = 0; i < list.size(); i++) {
        ArrayList inter = (ArrayList)list.get(i);
        int temp = i + 1;
        XmlElement sequence = this.Config.CreateElement("Sequence");
        sequence.SetAttribute("number",temp+"");
        XmlElement className = this.Config.CreateElement("ClassName");
        className.SetInnerText((String)inter.get(1));
        XmlElement methodName = this.Config.CreateElement("MethodName");
        methodName.SetInnerText("Execute");
        sequence.AppendChild(className);
        sequence.AppendChild(methodName);
        pre.AppendChild(sequence);
      }
    }
    else {
      if (this.Config != null) {
        XmlNode operation = this.Config.SelectSingleNode("/Operation");
        if (operation != null) {
          XmlNode pre = operation.SelectSingleNode("PreProcess");
          if (pre != null)
            operation.RemoveChild(pre);
        }
      }
    }
  }

  /**
   * Set Process Block.
   * @param process
   * @param startTime
   * @param endTime
   * @param uid
   * @param pwd
   * @param authClass
   * @return 
   */
  public void SetProcessBlock (String process, String startTime, String endTime, String uid, String pwd, String authClass) throws Exception
  {
    if (this.Config == null) {
      this.Config = new XmlDocument();
      this.Config.LoadXml("<Operation></Operation>");
    }
    XmlNode proc = this.Config.SelectSingleNode("/Operation/Process");
    if (proc == null) {
      proc = this.Config.CreateElement("Process");
      XmlNode operation = this.Config.SelectSingleNode("/Operation");
      XmlNode pre = operation.SelectSingleNode("PreProcess");
      if (pre != null)
        operation.InsertAfter(proc,pre);
      else {
        XmlNode post = operation.SelectSingleNode("PostProcess");
        if (post != null)
          operation.InsertBefore(proc,post);
        else
          operation.AppendChild(proc);
      }
    }
    proc.RemoveAll();
    XmlNode className = this.Config.CreateElement("ClassName");
    className.SetInnerText(process);
    proc.AppendChild(className);
    XmlNode methodName = this.Config.CreateElement("MethodName");
    methodName.SetInnerText("Execute");
    proc.AppendChild(methodName);
    if ((startTime != null && !startTime.equals("") && endTime != null && !endTime.equals("")) ||
        (uid != null && !uid.equals("") && pwd != null && !pwd.equals(""))) {
      XmlNode solicit = this.Config.CreateElement("Solicit");
      if (startTime != null && !startTime.equals("") && endTime != null && !endTime.equals("")) {
        XmlNode start = this.Config.CreateElement("SolicitStartTime");
        start.SetInnerText(startTime);
        solicit.AppendChild(start);
        XmlNode end = this.Config.CreateElement("SolicitEndTime");
        end.SetInnerText(endTime);
        solicit.AppendChild(end);
      }
      if (uid != null && !uid.equals("") && pwd != null && !pwd.equals("")) {
        XmlNode credentials = this.Config.CreateElement("SubmitCredentials");
        XmlNode user = this.Config.CreateElement("UserID");
        user.SetInnerText(uid);
        credentials.AppendChild(user);
        XmlNode password = this.Config.CreateElement("Password");
        Cryptography crypt = new Cryptography();
        password.SetInnerText(crypt.Encrypting(pwd,Phrase.CryptKey));
        credentials.AppendChild(password);
        solicit.AppendChild(credentials);
      }
      proc.AppendChild(solicit);
    }
    if (authClass != null && !authClass.trim().equals(""))
    {
      XmlNode authorization = this.Config.CreateElement("Authorization");
      XmlNode classNameNode = this.Config.CreateElement("ClassName");
      classNameNode.SetInnerText(authClass);
      authorization.AppendChild(classNameNode);
      XmlNode methodNameNode = this.Config.CreateElement("MethodName");
      methodNameNode.SetInnerText("Execute");
      authorization.AppendChild(methodNameNode);
      proc.AppendChild(authorization);
    }
  }

  /**
   * Set Process Block Wizard.
   * @param process
   * @param startTime
   * @param endTime
   * @param uid
   * @param pwd
   * @param authClass
   * @return 
   */
  public void SetProcessBlockWizard (String process, String startTime, String endTime, String uid, String pwd, String authClass) throws Exception
  {
    if (this.Config == null) {
      this.Config = new XmlDocument();
      this.Config.LoadXml("<Operation></Operation>");
    }
    XmlNode proc = this.Config.SelectSingleNode("/Operation/Process");
    if (proc == null) {
      proc = this.Config.CreateElement("Process");
      XmlNode operation = this.Config.SelectSingleNode("/Operation");
      XmlNode pre = operation.SelectSingleNode("PreProcess");
      if (pre != null)
        operation.InsertAfter(proc,pre);
      else {
        XmlNode post = operation.SelectSingleNode("PostProcess");
        if (post != null)
          operation.InsertBefore(proc,post);
        else
          operation.AppendChild(proc);
      }
    }
    proc.RemoveAll();
    XmlNode className = this.Config.CreateElement("ClassName");
    className.SetInnerText(process);
    proc.AppendChild(className);
    XmlNode methodName = this.Config.CreateElement("MethodName");
    methodName.SetInnerText("Execute");
    proc.AppendChild(methodName);
    if ((startTime != null && !startTime.equals("") && endTime != null && !endTime.equals("")) ||
        (uid != null && !uid.equals("") && pwd != null && !pwd.equals(""))) {
      XmlNode solicit = this.Config.CreateElement("Solicit");
      if (startTime != null && !startTime.equals("") && endTime != null && !endTime.equals("")) {
        XmlNode start = this.Config.CreateElement("SolicitStartTime");
        start.SetInnerText(startTime);
        solicit.AppendChild(start);
        XmlNode end = this.Config.CreateElement("SolicitEndTime");
        end.SetInnerText(endTime);
        solicit.AppendChild(end);
      }
      if (uid != null && !uid.equals("") && pwd != null && !pwd.equals("")) {
        XmlNode credentials = this.Config.CreateElement("SubmitCredentials");
        XmlNode user = this.Config.CreateElement("UserID");
        user.SetInnerText(uid);
        credentials.AppendChild(user);
        XmlNode password = this.Config.CreateElement("Password");
        Cryptography crypt = new Cryptography();
        password.SetInnerText(crypt.Encrypting(pwd,Phrase.CryptKey));
        credentials.AppendChild(password);
        solicit.AppendChild(credentials);
      }
      proc.AppendChild(solicit);
    }
    if (authClass != null && !authClass.trim().equals(""))
    {
      XmlNode authorization = this.Config.CreateElement("Authorization");
      XmlNode classNameNode = this.Config.CreateElement("ClassName");
      classNameNode.SetInnerText(authClass);
      authorization.AppendChild(classNameNode);
      XmlNode methodNameNode = this.Config.CreateElement("MethodName");
      methodNameNode.SetInnerText("Execute");
      authorization.AppendChild(methodNameNode);
      proc.AppendChild(authorization);
    }
  }

  /**
   * Set PostProcesses.
   * @param list
   * @return 
   */
  public void SetPostProcesses (ArrayList list) throws Exception
  {
    if (this.Config == null) {
      this.Config = new XmlDocument();
      this.Config.LoadXml("<Operation></Operation>");
    }
    if (this.Config != null && list != null && list.size() > 0) {
      XmlNode post = this.Config.SelectSingleNode("PostProcess");
      if (post == null) {
        post = this.Config.CreateElement("PostProcess");
        XmlNode proc = this.Config.SelectSingleNode("Process");
        this.Config.InsertAfter(post,proc);
      }
      post.RemoveAll();
      for (int i = 0; i < list.size(); i++) {
        ArrayList inter = (ArrayList)list.get(i);
        int temp = i + 1;
        XmlElement sequence = this.Config.CreateElement("Sequence");
        sequence.SetAttribute("number",temp+"");
        XmlElement className = this.Config.CreateElement("ClassName");
        className.SetInnerText((String)inter.get(1));
        XmlElement methodName = this.Config.CreateElement("MethodName");
        methodName.SetInnerText("Execute");
        sequence.AppendChild(className);
        sequence.AppendChild(methodName);
        post.AppendChild(sequence);
      }
    }
    else {
      if (this.Config != null) {
        XmlNode operation = this.Config.SelectSingleNode("/Operation");
        if (operation != null) {
          XmlNode post = operation.SelectSingleNode("PostProcess");
          if (post != null)
            operation.RemoveChild(post);
        }
      }
    }
  }

  /**
   * Set WebService Parameters Block.
   * @param paramNames
   * @return 
   */
  public void SetWebServiceParametersBlock (String[] paramNames) throws Exception
  {
	  if (this.Config == null) {
		  this.Config = new XmlDocument();
		  this.Config.LoadXml("<Operation></Operation>");
	  }
	  XmlNode proc = this.Config.SelectSingleNode("/Operation/Process");
	  if (this.Config != null && paramNames != null && paramNames.length > 0) {
		  XmlNodeList parameterNameList = this.Config.SelectNodes("/Operation/Process/ParameterName");
		  if (parameterNameList != null) {
			  for (int i = 0; i < parameterNameList.Count(); i++) {
				  parameterNameList.ItemOf(i).RemoveAll();
			  }
		  }
		  for (int i = 0; i < paramNames.length; i++) {
			  String inter = paramNames[i];
			  XmlElement parameterName = this.Config.CreateElement("ParameterName");
			  parameterName.SetInnerText(inter);
			  proc.AppendChild(parameterName);
		  }
	  }else {
		  if (this.Config != null) {
			  XmlNode operation = this.Config.SelectSingleNode("/Operation");
			  if (operation != null) {
				  XmlNode parameterName = operation.SelectSingleNode("/Process/ParameterName");
				  if (parameterName != null)
					  operation.RemoveChild(parameterName);
			  }
		  }
	  }
  }

  // WI 21296
  /**
   * Set WebService Parameters Block.
   * @param ArrayList of WebService Parameters
   * @return 
   */
  public void SetWebServiceParametersBlock (ArrayList params) throws Exception
  {
	  if (this.Config == null) {
		  this.Config = new XmlDocument();
		  this.Config.LoadXml("<Operation></Operation>");
	  }
	  XmlNode proc = this.Config.SelectSingleNode("/Operation/Process");
	  if (this.Config != null && params != null && !params.isEmpty()) {
          String paramName = "";
          String paramType = "";
          String paramTypeDesc = "";
          String paramOccNo = "";
          String paramEncoding = "";
          String paramReqInd = "";

          XmlNodeList parameterNameList = this.Config.SelectNodes("/Operation/Process/ParameterName");
		  if (parameterNameList != null) {
			  for (int i = 0; i < parameterNameList.Count(); i++) {
				  parameterNameList.ItemOf(i).RemoveAll();
			  }
		  }
		  for (int i = 0; i < params.size(); i++) {
			  WebServiceParameterMode wsp = (WebServiceParameterMode)params.get(i);
			  
			  paramName = wsp.getParamName();
			  paramType = wsp.getParamType();
			  paramTypeDesc = wsp.getParamTypeDesc();
			  paramOccNo = wsp.getParamOccNo();
			  paramEncoding = wsp.getParamEncoding();
			  paramReqInd = wsp.getParamReqInd();
			  
			  XmlElement parameterName = this.Config.CreateElement("ParameterName");
			  parameterName.SetInnerText(paramName);
			  
			  parameterName.SetAttribute(Phrase.WEBSERVICE_PARAMETER_TYPE, paramType);
			  parameterName.SetAttribute(Phrase.WEBSERVICE_PARAMETER_TYPE_DESCRIPTION, paramTypeDesc);
			  parameterName.SetAttribute(Phrase.WEBSERVICE_PARAMETER_OCCURENCE_NUMBER, paramOccNo);
			  parameterName.SetAttribute(Phrase.WEBSERVICE_PARAMETER_ENCODING, paramEncoding);
			  parameterName.SetAttribute(Phrase.WEBSERVICE_PARAMETER_REQIRED_INDICATOR, paramReqInd);
			  
			  proc.AppendChild(parameterName);
		  }
	  }else {
		  if (this.Config != null) {
			  XmlNode operation = this.Config.SelectSingleNode("/Operation");
			  if (operation != null) {
				  XmlNode parameterName = operation.SelectSingleNode("/Process/ParameterName");
				  if (parameterName != null)
					  operation.RemoveChild(parameterName);
			  }
		  }
	  }
  }

  /**
   * Set WebService Parameters Block Wizard.
   * @param opID
   * @param loggerName
   * @param paramNames
   * @return 
   */
  public void SetWebServiceParametersBlockWizard (int opID,String loggerName,String[] paramNames) throws Exception
  {
	  if (this.Config == null) {
		  this.Config = new XmlDocument();
	      INodeOperation opDB = DBManager.GetNodeOperation(loggerName);
	      String xmlFile = opDB.GetOperationConfig(opID);
		  this.Config.LoadXml(xmlFile);
	  }
	  XmlNode variables = this.Config.SelectSingleNode("/process/variables");
	  if (this.Config != null && paramNames != null && paramNames.length > 0) {
		  XmlNodeList variableNameList = this.Config.SelectNodes("/process/variables/variable");

		  if (variableNameList != null) {
			  for (int i = 0; i < variableNameList.Count(); i++){
				  XmlNode varNode = variableNameList.ItemOf(i);
				  String name = varNode.Attributes().GetNamedItem("name").GetValue();
				  if (WebServiceParameter.getParameters().contains(name))
					  continue;
				  else
					  variableNameList.ItemOf(i).ParentNode().RemoveChild(variableNameList.ItemOf(i));
			  }
		  }
		  for (int i = 0; i < paramNames.length; i++) {
			  String name = paramNames[i];
			  if (WebServiceParameter.getParameters().contains(name))
				  continue;
			  else{
				  String inter = paramNames[i];
				  XmlElement parameterName = this.Config.CreateElement("variable");
				  parameterName.SetAttribute("name", inter);
				  variables.AppendChild(parameterName);
			  }
		  }
	  }else {
		  if (this.Config != null) {
			  XmlNodeList variableNameList = this.Config.SelectNodes("/process/variables/variable");

			  if (variableNameList != null) {
				  for (int i = 0; i < variableNameList.Count(); i++){
					  XmlNode varNode = variableNameList.ItemOf(i);
					  String name = varNode.Attributes().GetNamedItem(Phrase.WEBSERVICE_PARAMETER_NAME).GetValue();
					  if (WebServiceParameter.getParameters().contains(name))
						  continue;
					  else
						  variableNameList.ItemOf(i).ParentNode().RemoveChild(variableNameList.ItemOf(i));
				  }
			  }
		  }
	  }
  }

  // WI 21296
  /**
   * Set WebService Parameters Block Wizard.
   * @param opID
   * @param loggerName
   * @param paramNames
   * @return 
   */
  public void SetWebServiceParametersBlockWizard (int opID,String loggerName,ArrayList params) throws Exception
  {
      String paramName = "";
      String paramType = "";
      String paramTypeDesc = "";
      String paramOccNo = "";
      String paramEncoding = "";
      String paramReqInd = "";

      if (this.Config == null) {
		  this.Config = new XmlDocument();
	      INodeOperation opDB = DBManager.GetNodeOperation(loggerName);
	      String xmlFile = opDB.GetOperationConfig(opID);
		  this.Config.LoadXml(xmlFile);
	  }
	  XmlNode variables = this.Config.SelectSingleNode("/process/variables");
	  if (this.Config != null && params != null && !params.isEmpty()) {
		  XmlNodeList variableNameList = this.Config.SelectNodes("/process/variables/variable");

		  if (variableNameList != null) {
			  for (int i = 0; i < variableNameList.Count(); i++){
				  XmlNode varNode = variableNameList.ItemOf(i);
				  String name = varNode.Attributes().GetNamedItem(Phrase.WEBSERVICE_PARAMETER_NAME).GetValue();
				  if (WebServiceParameter.getParameters().contains(name))
					  continue;
				  else
					  variableNameList.ItemOf(i).ParentNode().RemoveChild(variableNameList.ItemOf(i));
			  }
		  }
		  for (int i = 0; i < params.size(); i++) {
			  WebServiceParameterMode wsp = (WebServiceParameterMode)params.get(i);
			  
			  paramName = wsp.getParamName();
			  paramType = wsp.getParamType();
			  paramTypeDesc = wsp.getParamTypeDesc();
			  paramOccNo = wsp.getParamOccNo();
			  paramEncoding = wsp.getParamEncoding();
			  paramReqInd = wsp.getParamReqInd();
			  
			  if (WebServiceParameter.getParameters().contains(paramName))
				  continue;
			  else{
				  XmlElement parameterName = this.Config.CreateElement("variable");
				  parameterName.SetAttribute(Phrase.WEBSERVICE_PARAMETER_NAME, paramName);
				  parameterName.SetAttribute(Phrase.WEBSERVICE_PARAMETER_TYPE, paramType);
				  parameterName.SetAttribute(Phrase.WEBSERVICE_PARAMETER_TYPE_DESCRIPTION, paramTypeDesc);
				  parameterName.SetAttribute(Phrase.WEBSERVICE_PARAMETER_OCCURENCE_NUMBER, paramOccNo);
				  parameterName.SetAttribute(Phrase.WEBSERVICE_PARAMETER_ENCODING, paramEncoding);
				  parameterName.SetAttribute(Phrase.WEBSERVICE_PARAMETER_REQIRED_INDICATOR, paramReqInd);
				  variables.AppendChild(parameterName);
			  }
		  }
	  }else {
		  if (this.Config != null) {
			  XmlNodeList variableNameList = this.Config.SelectNodes("/process/variables/variable");

			  if (variableNameList != null) {
				  for (int i = 0; i < variableNameList.Count(); i++){
					  XmlNode varNode = variableNameList.ItemOf(i);
					  String name = varNode.Attributes().GetNamedItem(Phrase.WEBSERVICE_PARAMETER_NAME).GetValue();
					  if (WebServiceParameter.getParameters().contains(name))
						  continue;
					  else
						  variableNameList.ItemOf(i).ParentNode().RemoveChild(variableNameList.ItemOf(i));
				  }
			  }
		  }
	  }
  }

  /**
   * SaveTask.
   * @param loggerName
   * @return success
   */
  public boolean SaveTask (String loggerName) throws Exception
  {
    ITaskConfiguration taskDB = DBManager.GetTaskConfiguration(loggerName);
    if (this.OpName != null && this.ClassName != null && this.MethodName != null && this.TaskSchedule != null && this.TaskSchedule.ValidateSchedule())
      return taskDB.SaveTask(this.OpName,this.ClassName,this.MethodName,this.ParamNames,this.ParamTypes,this.ParamValues,this.TaskSchedule);
    else
      return false;
  }

  /**
   * Get Operations.
   * @param domainName
   * @param loggerName
   * @return operation array
   */
  public static String[] GetOperations (String domainName, String loggerName) throws Exception
  {
    INodeOperation opDB = DBManager.GetNodeOperation(loggerName);
    return opDB.GetOperations(domainName);
  }

  /**
   * Get Operation Full Names.
   * @param domainName
   * @param loggerName
   * @return operation Names array
   */
  public static String[] GetOperationFullNames (String domainName, String loggerName) throws Exception
  {
    INodeOperation opDB = DBManager.GetNodeOperation(loggerName);
    return opDB.GetOperationFullNames(domainName);
  }

  /**
   * Search Operations.
   * @param loggerName
   * @param domain
   * @param name
   * @param type
   * @param method
   * @param status
   * @param version
   * @return operation array
   */
  public static Operation[] SearchOperations (String loggerName, String domain, String name, String type, String method, String status,String version) throws Exception
  {
    INodeOperation opDB = DBManager.GetNodeOperation(loggerName);
    return opDB.Search(domain,name,type,method,status,version);
  }

  /**
   * Get Operations List.
   * @param loggerName
   * @param admin
   * @return operation array
   */
  public static Operation[] GetOperationsList (String loggerName, User admin) throws Exception
  {
    Operation[] retArray = null;
    if (admin != null) {
      Operation[] tempArray = null;
      INodeOperation opDB = DBManager.GetNodeOperation(loggerName);
      if (admin.IsNodeAdmin())
        tempArray = opDB.GetOperationsList(null);
      else
        tempArray = opDB.GetOperationsList(admin.GetAssignedDomains());
      if (tempArray != null && tempArray.length > 0) {
        TreeMap map = new TreeMap();
        for (int i = 0; i < tempArray.length; i++)
          if (!tempArray[i].GetWebService().equalsIgnoreCase(Phrase.WEB_METHOD_NODEPING) && !tempArray[i].GetWebService().equalsIgnoreCase(Phrase.WEB_METHOD_AUTHENTICATE))
            map.put(tempArray[i].GetDomain()+" "+tempArray[i].GetOpName()+" "+tempArray[i].GetWebService(),tempArray[i]);
        if (!map.isEmpty()) {
          Object[] temp = map.values().toArray();
          if (temp != null && temp.length > 0) {
            retArray = new Operation [temp.length];
            for (int i = 0; i < retArray.length; i++)
              retArray[i] = (Operation)temp[i];
          }
        }
      }
    }
    return retArray;
  }

  // WI 22317 
  /**
   * Get Operations List.
   * @param loggerName
   * @param admin
   * @return operation array
   */
  public static Operation[] GetAllOperationsList (String loggerName, User admin) throws Exception
  {
    Operation[] retArray = null;
    if (admin != null) {
      Operation[] tempArray = null;
      INodeOperation opDB = DBManager.GetNodeOperation(loggerName);
      if (admin.IsNodeAdmin())
        tempArray = opDB.GetAllOperationsList(null);
      else
        tempArray = opDB.GetAllOperationsList(admin.GetAssignedDomains());
      if (tempArray != null && tempArray.length > 0) {
        TreeMap map = new TreeMap();
        for (int i = 0; i < tempArray.length; i++)
          if ((tempArray[i].GetWebService()!= null && tempArray[i].GetOpName()!= null && !tempArray[i].GetOpName().equalsIgnoreCase("DEFAULT") && !tempArray[i].GetWebService().equalsIgnoreCase(Phrase.WEB_METHOD_NODEPING) && !tempArray[i].GetWebService().equalsIgnoreCase(Phrase.WEB_METHOD_AUTHENTICATE))
        	|| (tempArray[i].GetType() != null && tempArray[i].GetType().equalsIgnoreCase(Phrase.SCHEDULED_TASK_OPERATION))	  
          )
            map.put(tempArray[i].GetDomain()+" "+tempArray[i].GetOpName()+" "+tempArray[i].GetWebService(),tempArray[i]);
        if (!map.isEmpty()) {
          Object[] temp = map.values().toArray();
          if (temp != null && temp.length > 0) {
            retArray = new Operation [temp.length];
            for (int i = 0; i < retArray.length; i++)
              retArray[i] = (Operation)temp[i];
          }
        }
      }
    }
    return retArray;
  }

  /**
   * Is Unique Name.
   * @param loggerName
   * @param opName
   * @param webService
   * @return success
   */
  public static boolean IsUniqueName (String loggerName, String opName, String webService) throws Exception
  {
    INodeOperation opDB = DBManager.GetNodeOperation(loggerName);
    return opDB.IsUniqueName(opName,webService);
  }

  /**
   * Can Mark Active.
   * @param loggerName
   * @param opID
   * @param opName
   * @param webService
   * @return success
   */
  public static boolean CanMarkActive (String loggerName, int opID, String opName, String webService) throws Exception
  {
    INodeOperation opDB = DBManager.GetNodeOperation(loggerName);
    return opDB.CanMarkActive(opID,opName,webService);
  }

  /**
   * Delete Task.
   * @param loggerName
   * @param taskName
   * @return success
   */
  public static boolean DeleteTask (String loggerName, String taskName) throws Exception
  {
    ITaskConfiguration taskDB = DBManager.GetTaskConfiguration(loggerName);
    return taskDB.DeleteTask(taskName);
  }

  /**
   * Can Run Task.
   * @return success
   */
  public boolean CanRunTask ()
  {
    boolean retBool = false;
    if (this.Status != null && this.Status.equals(Phrase.RUNNING_STATUS)) {
      if (this.TaskSchedule != null && !this.TaskSchedule.GetType().equalsIgnoreCase("INACTIVE"))
        retBool = true;
    }
    return retBool;
  }

  /**
   * Start Task.
   * @param loggerName
   * @return
   */
  public void StartTask (String loggerName) throws Exception
  {
    INodeOperation opDB = DBManager.GetNodeOperation(loggerName);
    opDB.StartTask(this.OpName);
  }

  /**
   * Stop Task.
   * @param loggerName
   * @return
   */
  public void StopTask (String loggerName) throws Exception
  {
    INodeOperation opDB = DBManager.GetNodeOperation(loggerName);
    opDB.StopTask(this.OpName);
  }

  /**
   * Is Default.
   * @param className
   * @return success
   */
  private boolean IsDefault (String className)
  {
    boolean retBool = false;
    if (className == null)
      return false;
    if (className.equals(Phrase.DEFAULT_AUTHENTICATE))
      retBool = true;
    if (className.equals(Phrase.DEFAULT_DOWNLOAD))
      retBool = true;
    if (className.equals(Phrase.DEFAULT_GETSERVICES))
      retBool = true;
    if (className.equals(Phrase.DEFAULT_GETSTATUS))
      retBool = true;
    if (className.equals(Phrase.DEFAULT_NODEPING))
      retBool = true;
    if (className.equals(Phrase.DEFAULT_NOTIFY))
      retBool = true;
    if (className.equals(Phrase.DEFAULT_SUBMIT))
        retBool = true;
    if (className.equals(Phrase.DEFAULT_EXECUTE))
        retBool = true;
    return retBool;
  }
}
