package Node.Web.Administration.Action.Domains;

import java.text.ParsePosition;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.apache.log4j.Level;
import org.apache.struts.action.ActionForm;
import org.apache.struts.action.ActionForward;
import org.apache.struts.action.ActionMapping;

import Node.Phrase;
import Node.Biz.Administration.Domain;
import Node.Biz.Administration.Operation;
import Node.Biz.Administration.Schedule;
import Node.DB.DBManager;
import Node.DB.Interfaces.INodeWebService;
import Node.Utils.LoggingUtils;
import Node.Web.Administration.BaseAction;
import Node.Web.Administration.Bean.Domains.OperationsEditBean;
/**
 * <p>This class create OperationsEditAction.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class OperationsEditAction extends BaseAction {
	  /**
	   * Constructor
	   * @param 
	   * @return 
	   */
  public OperationsEditAction() {
  }

  /**
   * formExecute
   * @param mapping
   * @param form
   * @param request
   * @param response
   * @return ActionForward
   */
  public ActionForward formExecute (ActionMapping mapping, ActionForm form, HttpServletRequest request, HttpServletResponse response) throws Exception
  {
    this.Log("Executing OperationsEdit.do",Level.INFO);
    OperationsEditBean bean = (OperationsEditBean)form;
    bean.setMessage("");
    this.SetCheckBoxes(bean,request);
    this.ClearErrors(bean);
    String isNew = request.getParameter("new");
    if (isNew != null && isNew.equals("true")) {
      String domainName = (String)request.getSession().getAttribute(Phrase.DOMAIN_SESSION);
      Domain d = new Domain(domainName,Phrase.AdministrationLoggerName);
      this.ClearWebPage(bean,d);
      bean.setTitle("New Operation");
    }
    String opID = request.getParameter("opID");
    if (opID != null && !opID.equals("")) {
      //Changed by Charlie Zhang 9-6-2007 begin
      Operation op = new Operation(bean.getOpID(),Phrase.AdministrationLoggerName);
      //Operation op = new Operation(Integer.parseInt(request.getParameter("opID")),Phrase.AdministrationLoggerName);
      //Changed by Charlie Zhang 9-6-2007 end
      this.SetWebPage(bean,op);
    }
    String act = request.getParameter("act");
    if (act != null) {
      this.Log("OperationsEdit.do act = " + act,Level.DEBUG);
      if (act.equalsIgnoreCase("WEB_SERVICE_TAB")) {
        bean.setSelectedIndex(0);
        bean.setType(Phrase.WEB_SERVICE_OPERATION);
      }
      if (act.equalsIgnoreCase("SCHEDULED_TASK_TAB")) {
        bean.setSelectedIndex(1);
        bean.setType(Phrase.SCHEDULED_TASK_OPERATION);
      }
      if (act.equalsIgnoreCase("ADD_PRE_PROCESS"))
        this.SetPreProcess(bean,request);
      if (act.equalsIgnoreCase("REMOVE_PRE_PROCESS"))
        this.RemoveSelectedPreProcesses(bean,request);
      if (act.equalsIgnoreCase("ADD_POST_PROCESS"))
        this.SetPostProcess(bean,request);
      if (act.equalsIgnoreCase("REMOVE_POST_PROCESS"))
        this.RemoveSelectedPostProcesses(bean,request);
      if (act.equalsIgnoreCase("ADD_PARAMETERS"))
        this.SetParameters(bean,request);
      if (act.equalsIgnoreCase("REMOVE_PARAMETERS"))
        this.RemoveSelectedParameters(bean,request);
      if (act.equalsIgnoreCase("DELETE")) {
        if (this.Delete(bean))
          return mapping.findForward("deleted");
      }
      if (act.equalsIgnoreCase("SAVE")) {
        Operation op = this.SaveOperation(bean, request);
        if (op != null)
          this.SetWebPage(bean, op);
      }
    }
    return mapping.findForward("save");
  }

  /**
   * SetWebPage
   * @param bean
   * @param session
   * @return 
   */
  private void SetWebPage (OperationsEditBean bean, Operation op) throws Exception
  {
    this.Log("OperationsEdit.do: Setting Web Page Operation "+op!=null?op.GetOpName():"",Level.DEBUG);
    if (op != null) {
      bean.setOpID(op.GetOperationID());
      bean.setDescription(op.GetDescription());
      bean.setName(op.GetOpName());
      bean.setStatus(op.GetStatus());
      bean.setStatusMessage(op.GetMessage());
      bean.setTitle(op.GetOpName());
      bean.setType(op.GetType());
      if (op.GetType().equals(Phrase.WEB_SERVICE_OPERATION)) {
        if (op.GetDomain().equals(Phrase.NODE_DOMAIN)) {
          //String[] ws = new String[] { "AUTHENTICATE","DOWNLOAD","GETSERVICES","GETSTATUS","NODEPING","NOTIFY","QUERY","SOLICIT","SUBMIT" };
          INodeWebService wsDB = DBManager.GetNodeWebService(Phrase.WebServicesLoggerName);
          bean.setAvailWebServices(wsDB.GetWSList());
        }
        else {
          try {
            Domain d = new Domain(op.GetDomain(),Phrase.AdministrationLoggerName);
            bean.setAvailWebServices(d.GetAllowedWS());
          } catch (Exception e) {
            this.Log("Could Not Get List of Web Services Available: " + e.toString(),Level.ERROR);
          }
        }
        bean.setWebService(op.GetWebService());
        try {
          if (op.IsDefaultProcess())
            bean.setUseDefault("on");
          else
            bean.setUseDefault("");
          bean.setLogLevel(op.GetLoggingLevel().toString());
          bean.setPreProcesses(op.GetPreProcesses());
          bean.setProcClass(op.GetProcessClass());
          bean.setPostProcesses(op.GetPostProcesses());
          if (op.GetWebService() != null && op.GetWebService().equalsIgnoreCase(Phrase.WEB_METHOD_SOLICIT)) {
            this.SetSolicitTimes(bean,op.GetSolicitTimes());
            this.SetSubmitCredentials(bean, op.GetSolicitSubmitCredentials());
          }
          else if (op.GetWebService() != null && op.GetWebService().equalsIgnoreCase(Phrase.WEB_METHOD_AUTHENTICATE)) {
            String authClassName = op.GetAuthorizationClassName();
            if (authClassName != null)
            {
              bean.setAuthorizationClassName(authClassName);
              bean.setUseAuthorization("on");
            }
            else
              bean.setUseAuthorization("");
          }
          else {
            bean.setAnytime("on");
            bean.setUseSubmit("");
          }

        } catch (Exception e) {
          this.Log("Could Not Get Pre/Post Processes: "+e.toString(),Level.ERROR);
        }
      }
      else {
        bean.setTaskClassName(op.GetClassName());
        bean.setTaskMethodName(op.GetMethodName());
        bean.setParameters(op.GetTaskParameters());
        this.GetSchedule(bean,op.GetTaskSchedule());
      }
    }
    else {
      bean.setMessage("Unable to Retrieve Operation");
      this.Log("OperationsEdit.do: Unable to Retrieve Operation, op is null",Level.WARN);
    }
  }

  /**
   * ClearWebPage
   * @param bean
   * @param d
   * @return 
   */
  private void ClearWebPage (OperationsEditBean bean, Domain d) throws Exception
  {
    this.Log("OperationsEdit.do: Clearing Web Page",Level.DEBUG);
    bean.setOpID(-1);
    bean.setDescription("");
    bean.setName("");
    bean.setNameError("");
    bean.setStatus("");
    bean.setStatusMessage("");
    bean.setTitle("");
    bean.setType(Phrase.WEB_SERVICE_OPERATION);
    bean.setSelectedIndex(0);
    if (d.GetDomainName().equals(Phrase.NODE_DOMAIN)) {
        INodeWebService wsDB = DBManager.GetNodeWebService(Phrase.WebServicesLoggerName);
        bean.setAvailWebServices(wsDB.GetWSList());
    }
    else
      bean.setAvailWebServices(d.GetAllowedWS());
    bean.setLogLevel("");
    bean.setWebService("");
    bean.setUseDefault("");
    bean.setPreProcesses(null);
    bean.setProcClass("");
    bean.setProcClassError("");
    bean.setAuthorizationClassName("");
    bean.setAuthorizationClassNameError("");
    bean.setPostProcesses(null);
    bean.setBeginHour("");
    bean.setBeginMinute("");
    bean.setBeginSecond("");
    bean.setEndHour("");
    bean.setEndMinute("");
    bean.setEndSecond("");
    bean.setSubmitUserID("");
    bean.setSubmitPassword("");
    bean.setAnytime("on");
    bean.setUseSubmit("");
    bean.setTaskClassName("");
    bean.setTaskMethodName("");
    bean.setScheduleType("Inactive");
  }

  /**
   * ClearErrors
   * @param bean
   * @return 
   */
  private void ClearErrors (OperationsEditBean bean)
  {
    bean.setNameError("");
    bean.setStatusError("");
    bean.setProcClassError("");
    bean.setTaskClassNameError("");
    bean.setTaskMethodNameError("");
    bean.setStartDateError("");
    bean.setTaskStartTimeError("");
    bean.setEndDateError("");
    bean.setTaskEndTimeError("");
    bean.setIntervalError("");
    bean.setDayError("");
    bean.setDayOfMonthError("");
    bean.setMonthOfYearError("");
  }

  /**
   * SaveOperation
   * @param bean
   * @param request
   * @return Operation
   */
  private Operation SaveOperation (OperationsEditBean bean, HttpServletRequest request)
  {
    this.Log("OperationsEdit.do: Saving Operation "+bean.getName(),Level.DEBUG);
    String wsName = null;
    String version = (String)request.getSession().getAttribute(Phrase.NodeVersion);
    
    if (bean.getType().equalsIgnoreCase(Phrase.WEB_SERVICE_OPERATION))
      wsName = bean.getWebService();
    if (wsName != null && wsName.equals(""))
      wsName = null;
    Operation op = null;
    try {
      if (bean.getOpID() < 0)
        op = new Operation(bean.getName(),wsName);
      else
        op = new Operation(bean.getOpID(),Phrase.AdministrationLoggerName);
    } catch (Exception e) { };
    if (this.IsValidInput(bean,op.GetOperationID())) {
      try {
        //op = new Operation(bean.getName(),wsName,Phrase.AdministrationLoggerName);
    	op.setVersion(version);
        op.SetOpName(bean.getName());
        op.SetStatus(bean.getStatus());
        op.SetMessage(bean.getStatusMessage());
        op.SetDescription(bean.getDescription());
        op.SetType(bean.getType());
        op.SetDomain((String)request.getSession().getAttribute(Phrase.DOMAIN_SESSION));
        if (bean.getType().equals(Phrase.WEB_SERVICE_OPERATION)) {
          op.SetLoggingLevel(LoggingUtils.ParseLevel(bean.getLogLevel()));
          op.SetWebService(bean.getWebService());
          this.SetPreProcess(bean, request);
          op.SetPreProcesses(bean.getPreProcesses());
          String[] solicitTime = this.GetSolicitTime(bean);
          String solicitTime1 = null;
          String solicitTime2 = null;
          if (solicitTime != null && solicitTime.length >= 2) {
            solicitTime1 = solicitTime[0];
            solicitTime2 = solicitTime[1];
          }
          String submitUID = null;
          String submitPWD = null;
          if (bean.getUseSubmit().equalsIgnoreCase("on")) {
            submitUID = bean.getSubmitUserID();
            submitPWD = bean.getSubmitPassword();
          }
          this.GetDefaultProcess(bean);
          String authClassName = null;
          if (bean.getWebService().equalsIgnoreCase(Phrase.WEB_METHOD_AUTHENTICATE) && bean.getUseAuthorization().equals("on"))
            authClassName = bean.getAuthorizationClassName();
          op.SetProcessBlock(bean.getProcClass(),solicitTime1,solicitTime2,submitUID,submitPWD,authClassName);
          this.SetPostProcess(bean,request);
          op.SetPostProcesses(bean.getPostProcesses());
        }
        else {
          op.SetClassName(bean.getTaskClassName());
          op.SetMethodName("Execute");
          this.SetParameters(bean,request);
          op.SetTaskParameters(bean.getParameters());
          op.SetTaskSchedule(this.SetSchedule(bean));
        }
        if (op != null) {
          boolean isSaved = op.Save(Phrase.AdministrationLoggerName);
          if (!isSaved) {
            bean.setMessage("Error Saving Operation");
            this.Log("OperationsEdit.do: Unable to Save Operation "+op!=null?op.GetOpName():"",Level.WARN);
            op = null;
          }
          else
            bean.setMessage("Saved Successfully");
        }
      } catch (Exception e) {
        this.Log("Could Not Save Operation: " + e.toString(),Level.ERROR);
        bean.setMessage("Database Error");
        op = null;
      }
    }
    else
      op = null;
    return op;
  }

  /**
   * IsValidInput
   * @param bean
   * @param opID
   * @return boolean
   */
  private boolean IsValidInput (OperationsEditBean bean, int opID)
  {
    this.Log("Validating Input OperationsEdit.do",Level.DEBUG);
    boolean isValid = true;
    String temp = bean.getName();
    if (temp == null || temp.equals("")) {
      bean.setNameError("Enter a Name");
      isValid = false;
    }
    if (isValid && bean.getType().equals(Phrase.WEB_SERVICE_OPERATION)) {
      if (opID < 0) {
        try {
          if (!Operation.IsUniqueName(Phrase.AdministrationLoggerName,bean.getName(),bean.getWebService())) {
            bean.setNameError("Enter a Different Name");
            isValid = false;
          }
        } catch (Exception e) {
            bean.setNameError("Enter a Different Name");
          isValid = false;
        }
      }
      else {
        try {
          if (!bean.getStatus().equalsIgnoreCase("Inactive") && !Operation.CanMarkActive(Phrase.AdministrationLoggerName,opID,bean.getName(),bean.getWebService())) {
            bean.setStatusError("Cannot Mark to Active Status");
            isValid = false;
          }
        } catch (Exception e) {
          bean.setStatusError("Cannot Mark to Active Status");
          isValid = false;
        }
      }
      temp = bean.getProcClass();
      if (!bean.getUseDefault().equals("on") && (temp == null || temp.equals(""))) {
        bean.setProcClassError("Enter a Valid Class");
        isValid = false;
      }
      temp = bean.getAuthorizationClassName();
      if (bean.getUseAuthorization().equals("on") && (temp == null || temp.equals(""))) {
        bean.setAuthorizationClassNameError("Enter a Valid Class");
        isValid = false;
      }
    }
    else if (isValid && bean.getType().equals(Phrase.SCHEDULED_TASK_OPERATION)) {
      if (bean.getTitle().equals("New Operation")) {
        try {
          if (!Operation.IsUniqueName(Phrase.AdministrationLoggerName,bean.getName(),null)) {
            bean.setNameError("Enter a Different Name");
            isValid = false;
          }
        } catch (Exception e) {
          isValid = false;
        }
      }
      temp = bean.getTaskClassName();
      if (temp == null || temp.equals("")) {
        bean.setTaskClassNameError("Enter a Class Name");
        isValid = false;
      }
      /*
      temp = bean.getTaskMethodName();
      if (temp == null || temp.equals("")) {
        bean.setTaskMethodNameError("Enter a Method Name");
        isValid = false;
      }*/
      String scheduleType = bean.getScheduleType();
      if (scheduleType != null && !scheduleType.equals("INACTIVE")) {
        SimpleDateFormat dateFormat = new SimpleDateFormat("MM/dd/yyyy");
        try {
          Date d = dateFormat.parse(bean.getStartDate());
        } catch (Exception e) {
          bean.setStartDateError("Enter a Valid Start Date");
          isValid = false;
        }
        SimpleDateFormat timeFormat = new SimpleDateFormat("HH:mm:ss");
        try {
          Date d = timeFormat.parse(bean.getTaskStartHour()+":"+bean.getTaskStartMinute()+":"+bean.getTaskStartSecond());
        } catch (Exception e) {
          bean.setTaskStartTimeError("Enter a Valid Start Time");
          isValid = false;
        }
        if (!scheduleType.equals("ONCE")) {
          try {
            Date d = dateFormat.parse(bean.getEndDate());
          } catch (Exception e) {
            bean.setEndDateError("Enter a Valid End Date");
            isValid = false;
          }
          try {
            Date d = timeFormat.parse(bean.getTaskEndHour()+":"+bean.getTaskEndMinute()+":"+bean.getTaskEndSecond());
          } catch (Exception e) {
            bean.setTaskEndTimeError("Enter a Valid End Time");
            isValid = false;
          }
        }
        if (scheduleType.equals("SECONDS") || scheduleType.equals("DAYS")) {
          temp = bean.getInterval();
          if (temp == null || temp.equals("")) {
            bean.setIntervalError("Enter an Interval");
            isValid = false;
          }
        }
        else if (scheduleType.equals("WEEKLY")) {
          if (!bean.getSunday().equals("1") && !bean.getMonday().equals("2") && !bean.getTuesday().equals("3") && !bean.getWednesday().equals("4") && !bean.getThursday().equals("5") && !bean.getFriday().equals("6") && !bean.getSaturday().equals("7")) {
            bean.setDayError("Enter a Day of the Week");
            isValid = false;
          }
        }
        else if (!scheduleType.equals("ONCE")) {
          temp = bean.getDayOfMonth();
          if (temp == null || temp.equals("")) {
            bean.setDayOfMonthError("Enter a Valid Day of Month");
            isValid = false;
          }
          else {
            try {
              String[] tokens = temp.split(",");
              if (!this.OneOfDayOfMonths(tokens))
                throw new Exception();
            } catch (Exception e) {
              bean.setDayOfMonthError("Enter Valid Days of the Month");
              isValid = false;
            }
          }
          if (scheduleType.equals("YEARLY")) {
            temp = bean.getMonthOfYear();
            if (temp == null || temp.equals("")){
              bean.setMonthOfYearError("Enter a Valid Month of the Year");
              isValid = false;
            }
            else {
              try {
                String[] tokens = temp.split(",");
                if (!this.OneOfMonthsOfYear(tokens))
                  throw new Exception();
              } catch (Exception e) {
                bean.setMonthOfYearError("Enter Valid Months of the Year");
                isValid = false;
              }
            }
          }
        }
      }
    }
    if (!isValid) {
      bean.setMessage("Validation Errors");
      this.Log("Invalid Input OperationsEdit.do",Level.DEBUG);
    }
    else
      bean.setMessage("");
    return isValid;
  }

  /**
   * OneOfDayOfMonths
   * @param tokens
   * @return boolean
   */
  private boolean OneOfDayOfMonths (String[] tokens)
  {
    boolean retBool = false;
    if (tokens != null && tokens.length > 0) {
      retBool = true;
      for (int i = 0; i < tokens.length; i++) {
        String temp = tokens[i].trim();
        if (!(temp.equals("1") || temp.equals("2") || temp.equals("3") || temp.equals("4") || temp.equals("5") ||
            temp.equals("6") || temp.equals("7") || temp.equals("8") || temp.equals("9") || temp.equals("10") ||
            temp.equals("11") || temp.equals("12") || temp.equals("13") || temp.equals("14") || temp.equals("15") ||
            temp.equals("16") || temp.equals("17") || temp.equals("18") || temp.equals("19") || temp.equals("20") ||
            temp.equals("21") || temp.equals("22") || temp.equals("23") || temp.equals("24") || temp.equals("25") ||
            temp.equals("26") || temp.equals("27") || temp.equals("28") || temp.equals("29") || temp.equals("30") ||
            temp.equals("31") || temp.equalsIgnoreCase("LAST"))) {
          retBool = false;
          break;
        }
      }
    }
    return retBool;
  }

  /**
   * OneOfMonthsOfYear
   * @param tokens
   * @return boolean
   */
  private boolean OneOfMonthsOfYear (String[] tokens)
  {
    boolean retBool = false;
    if (tokens != null && tokens.length > 0) {
      retBool = true;
      for (int i = 0; i < tokens.length; i++) {
        String temp = tokens[i].trim();
        if (!(temp.equals("1") || temp.equals("2") || temp.equals("3") || temp.equals("4") || temp.equals("5") ||
            temp.equals("6") || temp.equals("7") || temp.equals("8") || temp.equals("9") || temp.equals("10") ||
            temp.equals("11") || temp.equals("12"))) {
          retBool = false;
          break;
        }
      }
    }
    return retBool;
  }

  /**
   * SetPreProcess
   * @param bean
   * @param request
   * @return 
   */
  private void SetPreProcess (OperationsEditBean bean, HttpServletRequest request)
  {
    this.Log("OperationsEdit.do: Setting Pre Processes",Level.DEBUG);
    ArrayList list = new ArrayList();
    String sequenceAdd = request.getParameter("preSequenceAdd");
    String classAdd = request.getParameter("preClassAdd");
    ArrayList toAdd = null;
    int place = -1;
    if (classAdd != null && !classAdd.equals("")) {
      try {
        place = Integer.parseInt(sequenceAdd);
        if (place >= 0) {
          toAdd = new ArrayList();
          toAdd.add(place + "");
          toAdd.add(classAdd);
        }
        else {
          toAdd = null;
          place = -1;
        }
      } catch (Exception e) {
        toAdd = new ArrayList();
        toAdd.add("1000000000000000");
        toAdd.add(classAdd);
        place = Integer.MAX_VALUE;
      }
    }
    int count = 1;
    for (int i = 0; true; i++) {
      String className = request.getParameter("preClass" + i);
      if (place == i + 1) {
        list.add(toAdd);
        count++;
      }
      if (className != null && !className.equals("")) {
        ArrayList temp = new ArrayList();
        temp.add(0, count + "");
        temp.add(1, className);
        list.add(temp);
        count++;
      }
      else if (className == null) {
        if (toAdd != null && place > count) {
          ArrayList temp = new ArrayList();
          temp.add(count+"");
          temp.add(classAdd);
          list.add(temp);
        }
        bean.setPreProcesses(list);
        break;
      }
    }
  }

  /**
   * RemoveSelectedPreProcesses
   * @param bean
   * @param request
   * @return 
   */
  private void RemoveSelectedPreProcesses (OperationsEditBean bean, HttpServletRequest request)
  {
    this.Log("OperationsEdit.do: Removing Pre Processes",Level.DEBUG);
    ArrayList list = new ArrayList();
    int count = 1;
    for (int i = 0; true; i++) {
      String selected = request.getParameter("preCheck" + i);
      String className = request.getParameter("preClass" + i);
      if (className != null && !className.equals("") && !(selected != null && selected.equals("on"))) {
        ArrayList temp = new ArrayList();
        temp.add(0, count+"");
        temp.add(1, className);
        list.add(temp);
        count++;
      }
      else if (className == null) {
        bean.setPreProcesses(list);
        break;
      }
    }
  }

  /**
   * SetPostProcess
   * @param bean
   * @param request
   * @return 
   */
  private void SetPostProcess (OperationsEditBean bean, HttpServletRequest request)
  {
    this.Log("OperationsEdit.do: Setting Post Processes",Level.DEBUG);
    ArrayList list = new ArrayList();
    String sequenceAdd = request.getParameter("postSequenceAdd");
    String classAdd = request.getParameter("postClassAdd");
    ArrayList toAdd = null;
    int place = -1;
    if (classAdd != null && !classAdd.equals("")) {
      try {
        place = Integer.parseInt(sequenceAdd);
        if (place >= 0) {
          toAdd = new ArrayList();
          toAdd.add(place + "");
          toAdd.add(classAdd);
        }
        else {
          toAdd = null;
          place = -1;
        }
      } catch (Exception e) {
        toAdd = new ArrayList();
        toAdd.add("10000000000000");
        toAdd.add(classAdd);
        place = Integer.MAX_VALUE;
      }
    }
    int count = 1;
    for (int i = 0; true; i++) {
      String className = request.getParameter("postClass" + i);
      if (place == i + 1) {
        list.add(toAdd);
        count++;
      }
      if (className != null && !className.equals("")) {
        ArrayList temp = new ArrayList();
        temp.add(0, count + "");
        temp.add(1, className);
        list.add(temp);
        count++;
      }
      else if (className == null) {
        if (toAdd != null && place > count) {
          toAdd = new ArrayList();
          toAdd.add(count+"");
          toAdd.add(classAdd);
          list.add(toAdd);
        }
        bean.setPostProcesses(list);
        break;
      }
    }
  }

  /**
   * RemoveSelectedPostProcesses
   * @param bean
   * @param request
   * @return 
   */
  private void RemoveSelectedPostProcesses (OperationsEditBean bean, HttpServletRequest request)
  {
    this.Log("OperationsEdit.do: Removing Post Processes",Level.DEBUG);
    ArrayList list = new ArrayList();
    int count = 1;
    for (int i = 0; true; i++) {
      String selected = request.getParameter("postCheck" + i);
      String className = request.getParameter("postClass" + i);
      if (className != null && !className.equals("") && !(selected != null && selected.equals("on"))) {
        ArrayList temp = new ArrayList();
        temp.add(0, count+"");
        temp.add(1, className);
        list.add(temp);
        count++;
      }
      else if (className == null) {
        bean.setPostProcesses(list);
        break;
      }
    }
  }

  /**
   * SetSolicitTimes
   * @param bean
   * @param times
   * @return 
   */
  private void SetSolicitTimes (OperationsEditBean bean, String[] times) throws Exception
  {
    this.Log("OperationsEdit.do: Setting Solicit Times",Level.DEBUG);
    if (times != null && times.length == 2) {
      SimpleDateFormat timeFormat = new SimpleDateFormat("HH:mm:ss");
      SimpleDateFormat hourFormat = new SimpleDateFormat("HH");
      SimpleDateFormat minuteFormat = new SimpleDateFormat("mm");
      SimpleDateFormat secondFormat = new SimpleDateFormat("ss");
      Date d = timeFormat.parse(times[0]);
      bean.setBeginHour(hourFormat.format(d));
      bean.setBeginMinute(minuteFormat.format(d));
      bean.setBeginSecond(secondFormat.format(d));
      d = timeFormat.parse(times[1]);
      bean.setEndHour(hourFormat.format(d));
      bean.setEndMinute(minuteFormat.format(d));
      bean.setEndSecond(secondFormat.format(d));
      bean.setAnytime("");
    }
    else
      bean.setAnytime("on");
  }

  /**
   * GetSolicitTime
   * @param bean
   * @return String[]
   */
  private String[] GetSolicitTime (OperationsEditBean bean)
  {
    this.Log("OperationsEdit.do: Getting Solicit Times",Level.DEBUG);
    String anytime = bean.getAnytime();
    if (anytime != null && anytime.equalsIgnoreCase("on")) {
      bean.setBeginHour("");
      bean.setBeginMinute("");
      bean.setBeginSecond("");
      bean.setEndHour("");
      bean.setEndMinute("");
      bean.setEndSecond("");
      return null;
    }
    else {
      String[] retArray = new String[2];
      String hour = bean.getBeginHour();
      String minute = bean.getBeginMinute();
      String second = bean.getBeginSecond();
      if (hour == null || hour.equals("") || minute == null || minute.equals("") ||
          second == null || second.equals(""))
        retArray[0] = null;
      else
        retArray[0] = hour + ":" + minute + ":" + second;
      hour = bean.getEndHour();
      minute = bean.getEndMinute();
      second = bean.getEndSecond();
      if (hour == null || hour.equals("") || minute == null || minute.equals("") ||
          second == null || second.equals(""))
        retArray[1] = null;
      else
        retArray[1] = hour + ":" + minute + ":" + second;
      return retArray;
    }
  }

  /**
   * SetSubmitCredentials
   * @param bean
   * @param credentials
   * @return 
   */
  private void SetSubmitCredentials (OperationsEditBean bean, String[] credentials)
  {
    this.Log("OperationsEdit.do: Setting Submit Credentials",Level.DEBUG);
    if (credentials != null && credentials.length == 2) {
      bean.setSubmitUserID(credentials[0]);
      bean.setSubmitPassword(credentials[1]);
      bean.setUseSubmit("on");
    }
    else
      bean.setUseSubmit("");
  }

  /**
   * GetDefaultProcess
   * @param bean
   * @return 
   */
  private void GetDefaultProcess (OperationsEditBean bean)
  {
    this.Log("OperationsEdit.do: Getting Default Process for "+bean.getWebService(),Level.DEBUG);
    if (bean.getUseDefault().equals("on")) {
      String wsName = bean.getWebService();
      if (wsName.equals(Phrase.WEB_METHOD_AUTHENTICATE))
        bean.setProcClass(Phrase.DEFAULT_AUTHENTICATE);
      if (wsName.equals(Phrase.WEB_METHOD_DOWNLOAD))
        bean.setProcClass(Phrase.DEFAULT_DOWNLOAD);
      if (wsName.equals(Phrase.WEB_METHOD_GETSERVICES))
        bean.setProcClass(Phrase.DEFAULT_GETSERVICES);
      if (wsName.equals(Phrase.WEB_METHOD_GETSTATUS))
        bean.setProcClass(Phrase.DEFAULT_GETSTATUS);
      if (wsName.equals(Phrase.WEB_METHOD_NODEPING))
        bean.setProcClass(Phrase.DEFAULT_NODEPING);
      if (wsName.equals(Phrase.WEB_METHOD_NOTIFY))
        bean.setProcClass(Phrase.DEFAULT_NOTIFY);
      if (wsName.equals(Phrase.WEB_METHOD_SUBMIT))
          bean.setProcClass(Phrase.DEFAULT_SUBMIT);
      if (wsName.equals(Phrase.WEB_METHOD_EXECUTE))
          bean.setProcClass(Phrase.DEFAULT_EXECUTE);
    }
  }

  /**
   * SetParameters
   * @param bean
   * @param request
   * @return 
   */
  private void SetParameters (OperationsEditBean bean, HttpServletRequest request)
  {
    this.Log("OperationsEdit.do: Setting Task Parameters",Level.DEBUG);
    ArrayList list = new ArrayList();
    String sequenceAdd = request.getParameter("sequenceAdd");
    String nameAdd = request.getParameter("paramNameAdd");
    //String typeAdd = request.getParameter("paramTypeAdd");
    String valueAdd = request.getParameter("paramValueAdd");
    ArrayList toAdd = null;
    int place = -1;
    if (nameAdd != null && !nameAdd.equals("") && valueAdd != null &&
        !valueAdd.equals("")) {
      try {
        place = Integer.parseInt(sequenceAdd);
        if (place >= 0) {
          toAdd = new ArrayList();
          toAdd.add(place + "");
          toAdd.add(nameAdd);
          //toAdd.add(typeAdd);
          toAdd.add(valueAdd);
        }
        else {
          toAdd = null;
          place = -1;
        }
      } catch (Exception e) {
        toAdd = new ArrayList();
        toAdd.add("10000000000000");
        toAdd.add(nameAdd);
        //toAdd.add(typeAdd);
        toAdd.add(valueAdd);
        place = Integer.MAX_VALUE;
      }
    }
    int count = 1;
    for (int i = 0; true; i++) {
      String paramName = request.getParameter("paramName" + i);
      //String paramType = request.getParameter("paramType"+i);
      String paramValue = request.getParameter("paramValue"+i);
      if (place == i + 1) {
        list.add(toAdd);
        count++;
      }
      if (paramName != null && !paramName.equals("") && paramValue != null && !paramValue.equals("")) {
        ArrayList temp = new ArrayList();
        temp.add(0, count + "");
        temp.add(1, paramName);
        //temp.add(2,paramType);
        temp.add(2, paramValue);
        list.add(temp);
        count++;
      }
      else if (paramName == null || paramValue == null) {
        if (toAdd != null && place > count) {
          toAdd = new ArrayList();
          toAdd.add(count+"");
          toAdd.add(nameAdd);
          //toAdd.add(typeAdd);
          toAdd.add(valueAdd);
          list.add(toAdd);
        }
        bean.setParameters(list);
        break;
      }
    }
  }

  /**
   * RemoveSelectedParameters
   * @param bean
   * @param request
   * @return 
   */
  private void RemoveSelectedParameters (OperationsEditBean bean, HttpServletRequest request)
  {
    this.Log("OperationsEdit.do: Removing Parameters",Level.DEBUG);
    ArrayList list = new ArrayList();
    int count = 1;
    for (int i = 0; true; i++) {
      String selected = request.getParameter("removeParam" + i);
      String paramName = request.getParameter("paramName" + i);
      //String paramType = request.getParameter("paramType"+i);
      String paramValue = request.getParameter("paramValue" + i);
      if (paramName != null && !paramName.equals("") && paramValue != null && !paramValue.equals("") && !(selected != null && selected.equals("on"))) {
        ArrayList temp = new ArrayList();
        temp.add(0, count+"");
        temp.add(1, paramName);
        //temp.add(2,paramType);
        temp.add(2, paramValue);
        list.add(temp);
        count++;
      }
      else if (paramName == null || paramValue == null) {
        bean.setParameters(list);
        break;
      }
    }
  }

  /**
   * SetSchedule
   * @param bean
   * @return Schedule
   */
  private Schedule SetSchedule (OperationsEditBean bean) throws Exception
  {
    Schedule retSchedule = null;
    String type = bean.getScheduleType();
    if (type != null && !type.equals("")) {
      retSchedule = new Schedule(type);
      if (!type.equals(retSchedule.TYPE_INACTIVE)) {
        SimpleDateFormat format = new SimpleDateFormat("MM/dd/yyyy HH:mm:ss");
        String startDate = bean.getStartDate()+" "+bean.getTaskStartHour()+":"+bean.getTaskStartMinute()+":"+bean.getTaskStartSecond();
        retSchedule.SetStartDate(format.parse(startDate,new ParsePosition(0)));
        if (!type.equals(retSchedule.TYPE_ONCE)) {
          String endDate = bean.getEndDate()+" "+bean.getTaskEndHour()+":"+bean.getTaskEndMinute()+":"+bean.getTaskEndSecond();
          retSchedule.SetEndDate((Date)format.parse(endDate,new ParsePosition(0)));
        }
        if (type.equals(retSchedule.TYPE_SECONDS) || type.equals(retSchedule.TYPE_DAYS))
          retSchedule.SetInterval(bean.getInterval());
        else if (type.equals(retSchedule.TYPE_WEEKS)) {
          ArrayList list = new ArrayList ();
          if (bean.getSunday().equals("1")) list.add("1");
          if (bean.getMonday().equals("2")) list.add("2");
          if (bean.getTuesday().equals("3")) list.add("3");
          if (bean.getWednesday().equals("4")) list.add("4");
          if (bean.getThursday().equals("5")) list.add("5");
          if (bean.getFriday().equals("6")) list.add("6");
          if (bean.getSaturday().equals("7")) list.add("7");
          if (!list.isEmpty()) {
            String[] days = new String[list.size()];
            for (int i = 0; i < list.size(); i++)
              days[i] = (String)list.get(i);
            retSchedule.SetDayOfWeek(days);
          }
        }
        else if (!type.equals(retSchedule.TYPE_ONCE)) {
          String daysOfMonth = bean.getDayOfMonth();
          if (daysOfMonth != null && !daysOfMonth.equals("")) {
            String[] tokens = daysOfMonth.split(",");
            if (tokens != null && tokens.length > 0)
              retSchedule.SetDayOfMonth(tokens);
          }
          if (type.equals(retSchedule.TYPE_YEARS)) {
            String monthsOfYear = bean.getMonthOfYear();
            if (monthsOfYear != null && !monthsOfYear.equals("")) {
              String[] tokens = monthsOfYear.split(",");
              if (tokens != null && tokens.length > 0)
                retSchedule.SetMonthOfYear(tokens);
            }
          }
        }
      }
    }
    return retSchedule;
  }

  /**
   * GetSchedule
   * @param bean
   * @param schedule
   * @return 
   */
  private void GetSchedule (OperationsEditBean bean, Schedule schedule)
  {
    if (schedule != null) {
      bean.setScheduleType(schedule.GetType());
      if (!schedule.GetType().equals(schedule.TYPE_INACTIVE)) {
        SimpleDateFormat date = new SimpleDateFormat("MM/dd/yyyy");
        SimpleDateFormat hour = new SimpleDateFormat("HH");
        SimpleDateFormat minute = new SimpleDateFormat("mm");
        SimpleDateFormat second = new SimpleDateFormat("ss");
        Date startDate = schedule.GetStartDate();
        bean.setStartDate(date.format(startDate));
        bean.setTaskStartHour(hour.format(startDate));
        bean.setTaskStartMinute(minute.format(startDate));
        bean.setTaskStartSecond(second.format(startDate));
        if (!schedule.GetType().equals(schedule.TYPE_ONCE)) {
          Date endDate = schedule.GetEndDate();
          bean.setEndDate(date.format(endDate));
          bean.setTaskEndHour(hour.format(endDate));
          bean.setTaskEndMinute(minute.format(endDate));
          bean.setTaskEndSecond(second.format(endDate));
        }
        if (schedule.GetType().equals(schedule.TYPE_SECONDS) || schedule.GetType().equals(schedule.TYPE_DAYS))
          bean.setInterval(schedule.GetInterval());
        else if (schedule.GetType().equals(schedule.TYPE_WEEKS)) {
          String[] weeks = schedule.GetDayOfWeek();
          if (weeks != null && weeks.length > 0) {
            for (int i = 0; i < weeks.length; i++) {
              if (weeks[i].equals("1"))
                bean.setSunday("1");
              if (weeks[i].equals("2"))
                bean.setMonday("2");
              if (weeks[i].equals("3"))
                bean.setTuesday("3");
              if (weeks[i].equals("4"))
                bean.setWednesday("4");
              if (weeks[i].equals("5"))
                bean.setThursday("5");
              if (weeks[i].equals("6"))
                bean.setFriday("6");
              if (weeks[i].equals("7"))
                bean.setSaturday("7");
            }
          }
        }
        else if (!schedule.GetType().equals(schedule.TYPE_ONCE)) {
          String[] days = schedule.GetDayOfMonth();
          if (days != null && days.length > 0) {
            String input = "";
            for (int i = 0; i < days.length; i++) {
              if (i != 0) input += ",";
              input += days[i];
            }
            bean.setDayOfMonth(input);
          }
          if (schedule.GetType().equals(schedule.TYPE_YEARS)) {
            String[] months = schedule.GetMonthOfYear();
            if (months != null && months.length > 0) {
              String input = "";
              for (int i = 0; i < months.length; i++) {
                if (i != 0) input += ",";
                input += months[i];
              }
            }
          }
        }
      }
    }
  }

  /**
   * SetCheckBoxes
   * @param bean
   * @param request
   * @return 
   */
  private void SetCheckBoxes (OperationsEditBean bean, HttpServletRequest request)
  {
    String useDefault = request.getParameter("useDefault");
    if (useDefault == null)
      bean.setUseDefault("");
    else if (useDefault.equals("on")) {
      bean.setUseDefault(useDefault);
      this.GetDefaultProcess(bean);
    }
    String useSubmit = request.getParameter("useSubmit");
    if (useSubmit == null) {
      bean.setUseSubmit("");
      bean.setSubmitUserID("");
      bean.setSubmitPassword("");
    }
    else
      bean.setUseSubmit(useSubmit);
    String anytime = request.getParameter("anytime");
    if (anytime == null)
      bean.setAnytime("");
    else {
      bean.setAnytime(anytime);
      bean.setBeginHour("");
      bean.setBeginMinute("");
      bean.setBeginSecond("");
      bean.setEndHour("");
      bean.setEndMinute("");
      bean.setEndSecond("");
    }
    String useAuthorization = request.getParameter("useAuthorization");
    if (useAuthorization == null)
    {
      bean.setUseAuthorization("");
      bean.setAuthorizationClassName("");
      bean.setAuthorizationClassNameError("");
    }
    else {
      bean.setUseAuthorization(useAuthorization);
    }
  }

  /**
   * Delete
   * @param bean
   * @return boolean
   */
  private boolean Delete (OperationsEditBean bean)
  {
    this.Log("OperationsEdit.do: Deleting Operation",Level.DEBUG);
    boolean retBool = false;
    try {
      Operation op = new Operation(bean.getName(),bean.getWebService(),Phrase.AdministrationLoggerName);
      if (op != null) {
        if (op.Delete(Phrase.AdministrationLoggerName))
          retBool = true;
        else
          bean.setMessage("Could Not Delete Operation, Transaction(s) have already been loggged.");
      }
    } catch (Exception e) {
      this.Log("OperationsEdit.do: Error Deleting Operation",Level.ERROR);
    }
    return retBool;
  }
}
