package Node.Web.Administration.Action.Domains;

import java.text.ParsePosition;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;
import java.util.Iterator;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.apache.log4j.Level;
import org.apache.struts.action.ActionForm;
import org.apache.struts.action.ActionForward;
import org.apache.struts.action.ActionMapping;

import Node.Phrase;
import Node.API.WebServiceParameterMode;
import Node.Biz.Administration.Domain;
import Node.Biz.Administration.NAASIntegration;
import Node.Biz.Administration.Operation;
import Node.Biz.Administration.Schedule;
import Node.DB.DBManager;
import Node.DB.Interfaces.INodeOperation;
import Node.DB.Interfaces.INodeOperationLog;
import Node.DB.Interfaces.INodeWebService;
import Node.NAAS.Requestor.NAASRequestor;
import Node.Utils.LoggingUtils;
import Node.Web.Administration.BaseAction;
import Node.Web.Administration.Bean.Domains.OperationsWizardBean;

import com.enfotech.basecomponent.jndi.JNDIAccess;
/**
 * <p>This class create OperationsWizardAction.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class OperationsWizardAction extends BaseAction {
	  /**
	   * Constructor
	   * @param 
	   * @return 
	   */
	public OperationsWizardAction() {
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
		this.Log("Executing OperationsWizard.do",Level.INFO);
		OperationsWizardBean bean = (OperationsWizardBean)form;
		String ret = "create";
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
			//Operation op = new Operation(bean.getOpID(),Phrase.AdministrationLoggerName);
			Operation op = new Operation(Integer.parseInt(request.getParameter("opID")),Phrase.AdministrationLoggerName);
			Domain domain = new Domain(op.GetDomain());
			this.ClearWebPage(bean,domain);
			this.SetWebPage(bean,op);
			if(op.GetWizardFlag().equalsIgnoreCase("yes")){
				bean.setIsWizard("yes");
			}else{
				bean.setIsWizard("no");    	  
			}
			if(bean.getName().length()==0){
				bean.setNameError("Please input operation name.");
				ret="back";
			}              	  
			else{
				ret = "next";
			}
		}
		String act = request.getParameter("act");
		String selectWizard = request.getParameter("selectWizard");
		String operationType = request.getParameter("operationType");
		if (act != null) {
			this.Log("OperationsWizard.do act = " + act,Level.DEBUG);
			if (act.equalsIgnoreCase("CREATE")) {
				ret="create";
			}
			if (act.equalsIgnoreCase("NEXT")) {
				Operation op = new Operation(bean.getOpID(),Phrase.AdministrationLoggerName);
				if(bean.getName().length()==0){
					bean.setNameError("Please input operation name.");
					ret="back";
				}              	  
				else{
					if(op.GetOperationID()!=-1){
						this.SetWebPage(bean, op);
					}
					ret = "next";
				}
			}
			if (act.equalsIgnoreCase("BACK")) {
				ret="back";
			}
			if (act.equalsIgnoreCase("CANCEL")) {
				ret="cancel";
			}
			if (selectWizard != null && selectWizard.equalsIgnoreCase("yes")) {
				bean.setIsWizard("yes");
			}else if (selectWizard != null && selectWizard.equalsIgnoreCase("no")) {
				bean.setIsWizard("no");
			}
			if (operationType != null && operationType.equalsIgnoreCase(Phrase.WEB_SERVICE_OPERATION)) {
				bean.setSelectedIndex(0);
				bean.setType(Phrase.WEB_SERVICE_OPERATION);
			}
			if (operationType != null && operationType.equalsIgnoreCase(Phrase.SCHEDULED_TASK_OPERATION)) {
				bean.setSelectedIndex(1);
				bean.setType(Phrase.SCHEDULED_TASK_OPERATION);
			}
			if (act.equalsIgnoreCase("ADD_PRE_PROCESS")){
				this.SetPreProcess(bean,request);
				ret = "changeParams";
			}
			if (act.equalsIgnoreCase("REMOVE_PRE_PROCESS")){
				this.RemoveSelectedPreProcesses(bean,request);
				ret = "changeParams";
			}
			if (act.equalsIgnoreCase("ADD_POST_PROCESS")){
				this.SetPostProcess(bean,request);
				ret = "changeParams";
			}
			if (act.equalsIgnoreCase("REMOVE_POST_PROCESS")){
				this.RemoveSelectedPostProcesses(bean,request);
				ret = "changeParams";
			}
			if (act.equalsIgnoreCase("ADD_TASK_PARAMETERS")){
				this.SetTaskParameters(bean,request);
				ret = "changeParams";
			}
			if (act.equalsIgnoreCase("REMOVE_TASK_PARAMETERS")){
				this.RemoveSelectedTaskParameters(bean,request);
				ret = "changeParams";    	  
			}
			if (act.equalsIgnoreCase("ADD_WEBSERVICE_PARAMETERS")){
				this.SetWebServiceParameters(bean,request);
				ret = "changeParams";
			}
			if (act.equalsIgnoreCase("REMOVE_WEBSERVICE_PARAMETERS")){
				this.RemoveSelectedWebServiceParameters(bean,request);
				ret = "changeParams";    	  
			}
			if (act.equalsIgnoreCase("DELETE")) {
				if (this.Delete(bean))
					return mapping.findForward("deleted");
			}
			if (act.equalsIgnoreCase("SAVE") || act.equalsIgnoreCase("OnlySAVE")) {
				if(bean.getIsWizard()!=null && bean.getIsWizard().equalsIgnoreCase("no")){
					this.SetWebServiceParameters(bean,request);
					Operation op = this.SaveOperation(bean, request);
					if (op != null)
						this.SetWebPage(bean, op);    		  
					ret = "save";
				}else if(bean.getIsWizard()!=null && bean.getIsWizard().equalsIgnoreCase("yes")){
					Operation op = null;
					this.SetWebServiceParameters(bean,request);
					if(bean.getOpID()<0){
						op = this.SaveOperationWizard(bean, request);    			  
					}else{
						if (this.IsValidInputWizard(bean,bean.getOpID(),request)) {
							op = new Operation(bean.getOpID(),Phrase.AdministrationLoggerName);
							// Change variables in operation config file
							//this.SetWebServiceParameters(bean,request);
							// WI 21296
							ArrayList webServiceparamsList = bean.getWebServiceParameters();
							/*String[] webServiceparams = null;
							if(webServiceparamsList!=null && webServiceparamsList.size()>0){
								webServiceparams = new String[webServiceparamsList.size()];
								for(int i=0;i<webServiceparamsList.size();i++){
									webServiceparams[i] = (String)((ArrayList)webServiceparamsList.get(i)).get(1);
								}
								op.SetWebServiceParametersBlockWizard(op.GetOperationID(),Phrase.AdministrationLoggerName, webServiceparams);	
								op.SetWebServiceParameterNames(bean.getWebServiceParameters());
							}*/
							ArrayList webServiceParamList = new ArrayList();
							Iterator it = bean.getWebServiceParameters().iterator();
							while(it.hasNext()){
								ArrayList tmp = (ArrayList)it.next();
								WebServiceParameterMode wspm = new WebServiceParameterMode();
								wspm.setParamName((String)tmp.get(1));
								wspm.setParamType((String)tmp.get(2));
								wspm.setParamTypeDesc((String)tmp.get(3));
								wspm.setParamOccNo((String)tmp.get(4));
								wspm.setParamEncoding((String)tmp.get(5));
								wspm.setParamReqInd((String)tmp.get(6));
								webServiceParamList.add(wspm);
							}
							// WI 22711
							op.SetWebServiceParametersBlockWizard(op.GetOperationID(),Phrase.AdministrationLoggerName, webServiceParamList);	
							if (bean.getType().equals(Phrase.WEB_SERVICE_OPERATION)) {
								op.SetWebServiceParameterNames(webServiceparamsList);
								op.setWebServiceParameters(webServiceparamsList);
							}
							// WI 21296
							String version = (String)request.getSession().getAttribute(Phrase.NodeVersion);
							op.setVersion(version);
							op.SetStatus(bean.getStatus());
							op.setIsPublish(bean.getIsPublish());
							op.SetMessage(bean.getStatusMessage());
							op.SetDescription(bean.getDescription());
							// WI 33641
							op.setIsRest(bean.getIsRest());
		
							if (bean.getType().equals(Phrase.SCHEDULED_TASK_OPERATION)) {
								this.SetTaskParameters(bean,request);
								op.SetTaskParameters(bean.getParameters());
								op.SetTaskSchedule(this.SetSchedule(bean));							
							}
							boolean isSaved = op.Save(Phrase.AdministrationLoggerName);
							if (!isSaved) {
								bean.setMessage("Error Saving Operation");
								this.Log("OperationsWizard.do: Unable to Save Operation "+op!=null?op.GetOpName():"",Level.WARN);
								op = null;
							}
							// WI 22233
							if(isSaved && !op.GetType().equalsIgnoreCase(Phrase.SCHEDULED_TASK_OPERATION) && !this.setAllPolicy(op.GetWebService(), op.GetOpName(), request.getParameter("isPolicyDeny"))){
								bean.setMessage("Save operation in Node successfully. Unable to save current explicit NAAS settings to NAAS.");
							}else{
								bean.setMessage("Save successfully.");
							}
						}
					}
					if (op != null && act.equalsIgnoreCase("SAVE")){
						this.SetWebPage(bean, op);    		      		  
						ret = mapping.getForward();
						String dotNetHost = (String) JNDIAccess.GetJNDIValue(Phrase.dotNetHost, false);
						String dotNetHostPort = (String) JNDIAccess.GetJNDIValue(Phrase.dotNetHostPort, false);
						if(dotNetHost==null || dotNetHost.equals("")){
							bean.setMessage("Wizard function is not available.");
							ret = "save";
						}else{
							return (new ActionForward("http://"+dotNetHost+(dotNetHostPort.equalsIgnoreCase("")?"":":"+dotNetHostPort)+"/Node.DataWizard/Pages/DataWizard/DataFlowWizard.aspx?OPID="+op.GetOperationID(),true));    				  
						}
					}else if(op != null && act.equalsIgnoreCase("OnlySAVE")){
						bean.setMessage("Save successfully.");
						this.SetWebPage(bean, op);    		      		  
						ret = "save";
					}else if(op == null && bean.getMessage()!=null){
						ret = "save";
					}
					else ret = "back";
				}
			}
			if (act.equalsIgnoreCase("RUN")) {
				Operation op = new Operation(bean.getOpID(),Phrase.AdministrationLoggerName);
				op.SetTaskSchedule(this.SetSchedule(bean));
				boolean ok = false;
				ok = op.SaveTask(Phrase.AdministrationLoggerName);
				if(ok){
					bean.setMessage("Task has been scheduled.");
					this.SetWebPage(bean, op);    		  					
				}else{
					bean.setMessage("Fail to schedule Task.");
					this.SetWebPage(bean, op);    		  
				}
				ret = "save";
			}
		}
		return mapping.findForward(ret);
	}

	  /**
	   * SetWebPage
	   * @param bean
	   * @param session
	   * @return 
	   */
	private void SetWebPage (OperationsWizardBean bean, Operation op) throws Exception
	{
		this.Log("OperationsWizard.do: Setting Web Page Operation "+op!=null?op.GetOpName():"",Level.DEBUG);
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
				// WI 21296
				bean.setIsPublish(op.getIsPublish());
				// WI 22233
				bean.setIsPolicyDeny(this.verifyPolicy(op.GetWebService(), op.GetOpName()));
				// WI 33641
				bean.setIsRest(op.getIsRest());
				try {
					if (op.IsDefaultProcess())
						bean.setUseDefault("on");
					else
						bean.setUseDefault("");
					bean.setLogLevel(op.GetLoggingLevel()==null?null:op.GetLoggingLevel().toString());
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
					// WI 21296
					bean.setWebServiceParameters(op.getWebServiceParameters());
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
			this.Log("OperationsWizard.do: Unable to Retrieve Operation, op is null",Level.WARN);
		}
	}

	  /**
	   * ClearWebPage
	   * @param bean
	   * @param d
	   * @return 
	   */
	private void ClearWebPage (OperationsWizardBean bean, Domain d) throws Exception
	{
		this.Log("OperationsWizard.do: Clearing Web Page",Level.DEBUG);
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
		bean.setParameters(null);
		bean.setWebServiceParameters(null);
		bean.setIsWizard("no");
		bean.setWebServiceParameters(null);
	}

	  /**
	   * ClearErrors
	   * @param bean
	   * @return 
	   */
	private void ClearErrors (OperationsWizardBean bean)
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
	private Operation SaveOperation (OperationsWizardBean bean, HttpServletRequest request)
	{
		this.Log("OperationsWizard.do: Saving Operation "+bean.getName(),Level.DEBUG);
		String wsName = null;
	    String version = (String)request.getSession().getAttribute(Phrase.NodeVersion);
		if (bean.getType().equalsIgnoreCase(Phrase.WEB_SERVICE_OPERATION))
			wsName = bean.getWebService();
		if (wsName != null && wsName.equals(""))
			wsName = null;
		Operation op = null;
		try {
			if (bean.getOpID() < 0)
				op = new Operation(bean.getName(),wsName,Phrase.AdministrationLoggerName);
			else
				op = new Operation(bean.getOpID(),Phrase.AdministrationLoggerName);
		} catch (Exception e) { };
		if (this.IsValidInput(bean,op.GetOperationID(),request)) {
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
					// WI 21296
					op.setIsPublish(bean.getIsPublish());
					// WI 33641
					op.setIsRest(bean.getIsRest());
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
					//this.SetWebServiceParameters(bean, request);
					// WI 21296
					/*
					String[] webServiceParams = null;
					if(bean.getWebServiceParameters()!=null){
						webServiceParams = new String[bean.getWebServiceParameters().size()];						
					}
					if(webServiceParams != null && webServiceParams.length>0){
						Iterator it = bean.getWebServiceParameters().iterator();
						int counter = 0;
						while(it.hasNext()){
							webServiceParams[counter++] = ((ArrayList)it.next()).get(1)+"";
						}
					}*/
					ArrayList webServiceParamList = new ArrayList();
					if(bean.getWebServiceParameters() != null){
						Iterator it = bean.getWebServiceParameters().iterator();
						while(it.hasNext()){
							ArrayList tmp = (ArrayList)it.next();
							WebServiceParameterMode wspm = new WebServiceParameterMode();
							wspm.setParamName((String)tmp.get(1));
							wspm.setParamType((String)tmp.get(2));
							wspm.setParamTypeDesc((String)tmp.get(3));
							wspm.setParamOccNo((String)tmp.get(4));
							wspm.setParamEncoding((String)tmp.get(5));
							wspm.setParamReqInd((String)tmp.get(6));
							webServiceParamList.add(wspm);
						}						
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
					
					// WI 21296
					//op.SetWebServiceParametersBlock(webServiceParams);	
					//op.SetWebServiceParameterNames(bean.getWebServiceParameters());
					op.SetWebServiceParametersBlock(webServiceParamList);	
					op.setWebServiceParameters(bean.getWebServiceParameters());
				}
				else {
					op.SetClassName(bean.getTaskClassName());
					op.SetMethodName("Execute");
					op.setIsPublish(null);
					// WI 33641
					op.setIsRest(null);
					this.SetTaskParameters(bean,request);
					op.SetTaskParameters(bean.getParameters());
					op.SetTaskSchedule(this.SetSchedule(bean));
				}
				if (op != null) {
					boolean isSaved = op.Save(Phrase.AdministrationLoggerName);
					if (!isSaved) {
						bean.setMessage("Error Saving Operation");
						this.Log("OperationsWizard.do: Unable to Save Operation "+op!=null?op.GetOpName():"",Level.WARN);
						op = null;
					}
					else{
						// WI 22233
						if(isSaved && !op.GetType().equalsIgnoreCase(Phrase.SCHEDULED_TASK_OPERATION) && !this.setAllPolicy(op.GetWebService(), op.GetOpName(), request.getParameter("isPolicyDeny"))){
							bean.setMessage("Save operation in Node successfully. Unable to save current explicit NAAS settings to NAAS.");
						}else{
							bean.setMessage("Saved Successfully");
						}
					}
				}
			} catch (Exception e) {
				this.Log("Could Not Save Operation: " + e.toString(),Level.ERROR);
				bean.setMessage("Database Error");
				op = null;
			}
		}else op = null;
		return op;
	}

	  /**
	   * SaveOperationWizard
	   * @param bean
	   * @param request
	   * @return Operation
	   */
	private Operation SaveOperationWizard (OperationsWizardBean bean, HttpServletRequest request)
	{
		this.Log("OperationsWizard.do: Saving Wizard Operation "+bean.getName(),Level.DEBUG);
		String wsName = null;
		String config = null;
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
		if (this.IsValidInputWizard(bean,op.GetOperationID(),request)) {
			try {
		    	op.setVersion(version);
				op.SetOpName(bean.getName());
				op.SetStatus(bean.getStatus());
				op.SetMessage(bean.getStatusMessage());
				op.SetDescription(bean.getDescription());
				op.SetType(bean.getType());
				op.SetDomain((String)request.getSession().getAttribute(Phrase.DOMAIN_SESSION));
				if (bean.getType().equals(Phrase.WEB_SERVICE_OPERATION)) {	// save webservices
					// WI 21296
					op.setIsPublish(bean.getIsPublish());
					// WI 33641
					op.setIsRest(bean.getIsRest());
					//op.SetLoggingLevel(LoggingUtils.ParseLevel(bean.getLogLevel()));
					op.SetWebService(bean.getWebService());
					/*this.SetPreProcess(bean, request);
              op.SetPreProcesses(bean.getPreProcesses());*/
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
					/* op.SetProcessBlock(bean.getProcClass(),solicitTime1,solicitTime2,submitUID,submitPWD,authClassName);
    		          this.SetPostProcess(bean,request);
    		          op.SetPostProcesses(bean.getPostProcesses());*/
					config = "<?xml version=\"1.0\" encoding=\"utf-8\"?><process><extension><operation name=\"" + op.GetOpName() + "\" id=\"" + op.GetOperationID() + "\" domain=\"" + op.GetDomain() +"\" webservice=\""+bean.getWebService()+"\" type=\""+ bean.getType() + "\" version=\"" + version + "\"><documentation></documentation></operation></extension><partnerLinks /><variables><variable name=\"webMethodName\">"+ bean.getWebService() + "</variable><variable name=\"operationName\">"+ op.GetOpName()+"</variable><variable name=\"transactionId\" /><variable name=\"securityToken\" /><variable name=\"dataflow\" /><variable name=\"request\" /><variable name=\"rowId\" /><variable name=\"maxRows\" />";
					if (bean.getWebService().equalsIgnoreCase(Phrase.WEB_METHOD_SOLICIT)){
						if(version.equalsIgnoreCase(Phrase.ver_1)){
							config += "<variable name=\"returnURL\"/>";
						}else if(version.equalsIgnoreCase(Phrase.ver_2)){
							config += "<variable name=\"recipient\"/><variable name=\"notificationURI\"/>";							
						}
					}
					ArrayList webServiceparams = bean.getWebServiceParameters();
					if (webServiceparams != null && !webServiceparams.isEmpty()) {
						Iterator it = webServiceparams.iterator();						
						while(it.hasNext()){
							ArrayList temp = (ArrayList)it.next();
							// WI 21296
							config = config+ "<variable "
											+ Phrase.WEBSERVICE_PARAMETER_NAME + "=" + "\"" + (String)temp.get(1) + "\" "
											+ Phrase.WEBSERVICE_PARAMETER_TYPE + "=" + "\"" + (String)temp.get(2) + "\" "
											+ Phrase.WEBSERVICE_PARAMETER_TYPE_DESCRIPTION + "=" + "\"" + (String)temp.get(3) + "\" "
											+ Phrase.WEBSERVICE_PARAMETER_OCCURENCE_NUMBER + "=" + "\"" + (String)temp.get(4) + "\" "
											+ Phrase.WEBSERVICE_PARAMETER_ENCODING + "=" + "\"" + (String)temp.get(5) + "\" "
											+ Phrase.WEBSERVICE_PARAMETER_REQIRED_INDICATOR + "=" + "\"" + (String)temp.get(6) + "\" "
											+ "/>";							
						}
						// WI 21296
						op.setWebServiceParameters(webServiceparams);
					}
					config = config + "</variables><sequence /></process>";
					op.SetConfig(config);
				}else {	// save tasks
					op.SetClassName(Phrase.StartWizardTask);
					op.SetMethodName("Execute");
					this.SetTaskParameters(bean,request);
					op.SetTaskParameters(bean.getParameters());
					op.SetTaskSchedule(this.SetSchedule(bean));
					// WI 22711
					// For Node 2.0 save config to Operation table
					config = "<?xml version=\"1.0\" encoding=\"utf-8\"?><process><extension><operation name=\"" + op.GetOpName() + "\" id=\"" + op.GetOperationID() + "\" domain=\"" + op.GetDomain() +"\" webservice=\"NONE\" type=\""+ bean.getType() + "\" version=\"VER_20\"><documentation></documentation></operation></extension><partnerLinks /><variables><variable name=\"webMethodName\">" + bean.getWebService() + "</variable><variable name=\"operationName\">"+ op.GetOpName()+"</variable><variable name=\"transactionId\" /><variable name=\"securityToken\" /><variable name=\"dataflow\" /><variable name=\"request\" />";
					ArrayList taskParams = bean.getParameters();
					if (taskParams != null && taskParams.size() > 0) {
						for (int i = 0; i < taskParams.size(); i++) {
							ArrayList temp = (ArrayList)taskParams.get(i);
							config = config+ "<variable name=\""+(String)temp.get(1)+"\" />";
						}
					}
					config = config + "</variables><sequence/></process>";
					op.SetConfig(config);
				}

				if (op != null) {
					boolean isSaved = op.Save(Phrase.AdministrationLoggerName);
					if (!isSaved) {
						bean.setMessage("Error Saving Operation");
						this.Log("OperationsWizard.do: Unable to Save Operation "+op!=null?op.GetOpName():"",Level.WARN);
						op = null;
					}
					else{	// Must update the operation using new operation Id
						if (bean.getType().equals(Phrase.WEB_SERVICE_OPERATION)) {
							config = "<?xml version=\"1.0\" encoding=\"utf-8\"?><process><extension><operation name=\"" + op.GetOpName() + "\" id=\"" + op.GetOperationID() + "\" domain=\"" + op.GetDomain() +"\" webservice=\""+(bean.getWebService()==null || bean.getWebService().equalsIgnoreCase("")?"NONE":bean.getWebService()) +"\" type=\"" + bean.getType() + "\" version=\"VER_20\"><documentation></documentation></operation></extension><partnerLinks /><variables><variable name=\"webMethodName\">"+ (bean.getWebService()==null || bean.getWebService().equalsIgnoreCase("")?"NONE":bean.getWebService()) +"</variable><variable name=\"operationName\">"+ op.GetOpName()+"</variable><variable name=\"transactionId\" /><variable name=\"securityToken\" /><variable name=\"dataflow\" /><variable name=\"request\" /><variable name=\"rowId\" /><variable name=\"maxRows\" /><variable name=\"documents\" />";        				
						}else{
							config = "<?xml version=\"1.0\" encoding=\"utf-8\"?><process><extension><operation name=\"" + op.GetOpName() + "\" id=\"" + op.GetOperationID() + "\" domain=\"" + op.GetDomain() +"\" webservice=\""+(bean.getWebService()==null || bean.getWebService().equalsIgnoreCase("")?"NONE":bean.getWebService()) +"\" type=\"" + bean.getType() + "\" version=\"VER_20\"><documentation></documentation></operation></extension><partnerLinks /><variables><variable name=\"webMethodName\">"+ (bean.getWebService()==null || bean.getWebService().equalsIgnoreCase("")?"Execute":bean.getWebService()) +"</variable><variable name=\"operationName\">"+ op.GetOpName()+"</variable><variable name=\"transactionId\" /><variable name=\"securityToken\" /><variable name=\"dataflow\" /><variable name=\"request\" />";
						}
						if (bean.getWebService().equalsIgnoreCase(Phrase.WEB_METHOD_SOLICIT)){
							if(version.equalsIgnoreCase(Phrase.ver_1)){
								config += "<variable name=\"returnURL\"/>";
							}else if(version.equalsIgnoreCase(Phrase.ver_2)){
								config += "<variable name=\"recipient\"/><variable name=\"notificationURI\"/>";							
							}
						}
						ArrayList webServiceparams = bean.getWebServiceParameters();
						if (webServiceparams != null && webServiceparams.size() > 0) {
							for (int i = 0; i < webServiceparams.size(); i++) {
								ArrayList temp = (ArrayList)webServiceparams.get(i);
								// WI 21296
								//config = config+ "<variable name=\""+(String)temp.get(1)+"\" />";
								config = config+ "<variable "
												+ Phrase.WEBSERVICE_PARAMETER_NAME + "=" + "\"" + (String)temp.get(1) + "\" "
												+ Phrase.WEBSERVICE_PARAMETER_TYPE + "=" + "\"" + (String)temp.get(2) + "\" "
												+ Phrase.WEBSERVICE_PARAMETER_TYPE_DESCRIPTION + "=" + "\"" + (String)temp.get(3) + "\" "
												+ Phrase.WEBSERVICE_PARAMETER_OCCURENCE_NUMBER + "=" + "\"" + (String)temp.get(4) + "\" "
												+ Phrase.WEBSERVICE_PARAMETER_ENCODING + "=" + "\"" + (String)temp.get(5) + "\" "
												+ Phrase.WEBSERVICE_PARAMETER_REQIRED_INDICATOR + "=" + "\"" + (String)temp.get(6) + "\" "
												+ "/>";							
							}
						}
						config = config + "</variables><sequence /></process>";
						op.SetConfig(config);
						isSaved = op.SaveWithoutChangeTaskSchedule(Phrase.AdministrationLoggerName); // save new opid without change schedule
						if (!isSaved) {
							bean.setMessage("Error Saving Operation");
							this.Log("OperationsWizard.do: Unable to Save Operation "+op!=null?op.GetOpName():"",Level.WARN);
							op = null;
						}else bean.setMessage("Saved Successfully");    			
					}
				}
			} catch (Exception e) {
				this.Log("Could Not Save Operation: " + e.toString(),Level.ERROR);
				bean.setMessage("Database Error");
				op = null;
			}    	
		}else op = null;
		return op;
	}

	  /**
	   * IsValidInput
	   * @param bean
	   * @param opID
	   * @return boolean
	   */
	private boolean IsValidInput (OperationsWizardBean bean, int opID, HttpServletRequest request)
	{
		this.Log("Validating Input OperationsWizard.do",Level.DEBUG);
		boolean isValid = true;
		String temp = bean.getName();
		if (temp == null || temp.equals("")) {
			bean.setMessage("Enter a Name");
			isValid = false;
		}
		String isValidParam = this.checkParameters(request);
		if(isValid && !isValidParam.equalsIgnoreCase("ok")){
			bean.setMessage(isValidParam);
			isValid = false;
		}
		if (isValid && bean.getType().equals(Phrase.WEB_SERVICE_OPERATION)) {
			if (opID < 0) {
				try {
					if (!Operation.IsUniqueName(Phrase.AdministrationLoggerName,bean.getName(),bean.getWebService())) {
						bean.setMessage("Enter a Different Name");
						isValid = false;
					}
				} catch (Exception e) {
					bean.setMessage("Enter a Different Name");
					isValid = false;
				}
			}
			else {
				try {
					if (!bean.getStatus().equalsIgnoreCase("Inactive") && !Operation.CanMarkActive(Phrase.AdministrationLoggerName,opID,bean.getName(),bean.getWebService())) {
						bean.setMessage("Cannot Mark to Active Status");
						isValid = false;
					}
				} catch (Exception e) {
					bean.setMessage("Cannot Mark to Active Status");
					isValid = false;
				}
			}
			// WI 21296
			if(isValid && bean.getIsPublish().equals("")){
				bean.setMessage("Please select Publish Web Service.");
				isValid = false;
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
		if (!isValid && (bean.getMessage()==null || bean.getMessage().equalsIgnoreCase(""))) {
			bean.setMessage("Validation Errors");
			this.Log("Invalid Input OperationsWizard.do",Level.DEBUG);
		}
		else if(isValid) bean.setMessage("");
		return isValid;
	}

	  /**
	   * IsValidInputWizard
	   * @param bean
	   * @param opID
	   * @return boolean
	   */
	private boolean IsValidInputWizard (OperationsWizardBean bean, int opID, HttpServletRequest request)
	{
		this.Log("Validating Input OperationsWizard.do",Level.DEBUG);
		boolean isValid = true;
		String temp = bean.getName();
		if (temp == null || temp.equals("")) {
			bean.setMessage("Enter a Name");
			isValid = false;
		}
		// WI 21296
		String isValidParam = this.checkParameters(request);
		if(isValid && !isValidParam.equalsIgnoreCase("ok")){
			bean.setMessage(isValidParam);
			isValid = false;
		}
		if (isValid && bean.getType().equals(Phrase.WEB_SERVICE_OPERATION)) {
			if (opID < 0) {
				try {
					if (!Operation.IsUniqueName(Phrase.AdministrationLoggerName,bean.getName(),bean.getWebService())) {
						bean.setMessage("Enter a Different Name");
						isValid = false;
					}
				} catch (Exception e) {
					bean.setMessage("Enter a Different Name");
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
			// WI 21296
			if(isValid && bean.getIsPublish().equals("")){
				bean.setMessage("Please select Publish Web Service.");
				isValid = false;
			}
			/*      temp = bean.getProcClass();
      if (!bean.getUseDefault().equals("on") && (temp == null || temp.equals(""))) {
        bean.setProcClassError("Enter a Valid Class");
        isValid = false;
      }
      temp = bean.getAuthorizationClassName();
      if (bean.getUseAuthorization().equals("on") && (temp == null || temp.equals(""))) {
        bean.setAuthorizationClassNameError("Enter a Valid Class");
        isValid = false;
      }*/
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
			/*temp = bean.getTaskClassName();
      if (temp == null || temp.equals("")) {
        bean.setTaskClassNameError("Enter a Class Name");
        isValid = false;
      }

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
		if (!isValid && (bean.getMessage()==null || bean.getMessage().equalsIgnoreCase(""))) {
			bean.setMessage("Validation Errors");
			this.Log("Invalid Input OperationsWizard.do",Level.DEBUG);
		}
		else if(isValid) bean.setMessage("");
		
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
	private void SetPreProcess (OperationsWizardBean bean, HttpServletRequest request)
	{
		this.Log("OperationsWizard.do: Setting Pre Processes",Level.DEBUG);
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
	private void RemoveSelectedPreProcesses (OperationsWizardBean bean, HttpServletRequest request)
	{
		this.Log("OperationsWizard.do: Removing Pre Processes",Level.DEBUG);
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
	private void SetPostProcess (OperationsWizardBean bean, HttpServletRequest request)
	{
		this.Log("OperationsWizard.do: Setting Post Processes",Level.DEBUG);
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
	private void RemoveSelectedPostProcesses (OperationsWizardBean bean, HttpServletRequest request)
	{
		this.Log("OperationsWizard.do: Removing Post Processes",Level.DEBUG);
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
	private void SetSolicitTimes (OperationsWizardBean bean, String[] times) throws Exception
	{
		this.Log("OperationsWizard.do: Setting Solicit Times",Level.DEBUG);
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
	   * @return 
	   */
	private String[] GetSolicitTime (OperationsWizardBean bean)
	{
		this.Log("OperationsWizard.do: Getting Solicit Times",Level.DEBUG);
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
	private void SetSubmitCredentials (OperationsWizardBean bean, String[] credentials)
	{
		this.Log("OperationsWizard.do: Setting Submit Credentials",Level.DEBUG);
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
	private void GetDefaultProcess (OperationsWizardBean bean)
	{
		this.Log("OperationsWizard.do: Getting Default Process for "+bean.getWebService(),Level.DEBUG);
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
	   * SetTaskParameters
	   * @param bean
	   * @param request
	   * @return 
	   */
	private void SetTaskParameters (OperationsWizardBean bean, HttpServletRequest request)
	{
		this.Log("OperationsWizard.do: Setting Task Parameters",Level.DEBUG);
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
	   * SetWebServiceParameters
	   * @param bean
	   * @param request
	   * @return 
	   */
	private void SetWebServiceParameters (OperationsWizardBean bean, HttpServletRequest request)
	{
		this.Log("OperationsWizard.do: Setting WebService Parameters",Level.DEBUG);
		ArrayList list = new ArrayList();
		String sequenceAdd = request.getParameter("sequenceAdd");
		String nameAdd = request.getParameter("paramNameAdd");
		//String typeAdd = request.getParameter("paramTypeAdd");
		//String valueAdd = request.getParameter("paramValueAdd");
		// WI 21296
		String paramTypeAdd = request.getParameter("paramTypeAdd");
		String paramTypeDescAdd = request.getParameter("paramTypeDescAdd");
		String paramOccNoAdd = request.getParameter("paramOccNoAdd");
		String paramEncodingAdd = request.getParameter("paramEncodingAdd");
		String paramReqIndAdd = request.getParameter("paramReqIndAdd");
		ArrayList toAdd = null;
		int place = -1;
		if (nameAdd != null && !nameAdd.equals("")) {
			try {
				place = Integer.parseInt(sequenceAdd);
				if (place >= 0) {
					toAdd = new ArrayList();
					toAdd.add(place + "");
					toAdd.add(nameAdd);
					//toAdd.add(typeAdd);
					//toAdd.add(valueAdd);
					// WI 21296
					toAdd.add(paramTypeAdd);
					toAdd.add(paramTypeDescAdd);
					toAdd.add(paramOccNoAdd);
					toAdd.add(paramEncodingAdd);
					toAdd.add(paramReqIndAdd);
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
				//toAdd.add(valueAdd);
				// WI 21296
				toAdd.add(paramTypeAdd);
				toAdd.add(paramTypeDescAdd);
				toAdd.add(paramOccNoAdd);
				toAdd.add(paramEncodingAdd);
				toAdd.add(paramReqIndAdd);
				place = Integer.MAX_VALUE;
			}
		}
		int count = 1;
		for (int i = 0; true; i++) {
			String paramName = request.getParameter("paramName" + i);
			//String paramType = request.getParameter("paramType"+i);
			//String paramValue = request.getParameter("paramValue"+i);
			// WI 21296
			String paramType = request.getParameter("paramType" + i);
			String paramTypeDesc = request.getParameter("paramTypeDesc" + i);
			String paramOccNo = request.getParameter("paramOccNo" + i);
			String paramEncoding = request.getParameter("paramEncoding" + i);
			String paramReqInd = request.getParameter("paramReqInd" + i);
			if (place == count) {
				list.add(toAdd);
				count++;
			}
			if (paramName != null && !paramName.equals("")) {
				ArrayList temp = new ArrayList();
				temp.add(0, count + "");
				temp.add(1, paramName);
				//temp.add(2,paramType);
				//temp.add(2, paramValue);
				// WI 21296
				temp.add(2, paramType);
				temp.add(3, paramTypeDesc);
				temp.add(4, paramOccNo);
				temp.add(5, paramEncoding);
				temp.add(6, paramReqInd);
				list.add(temp);
				count++;
			}
			else if (paramName == null) {
				if (toAdd != null && place > count) {
					toAdd = new ArrayList();
					toAdd.add(count+"");
					toAdd.add(nameAdd);
					//toAdd.add(typeAdd);
					//toAdd.add(valueAdd);
					// WI 21296
					toAdd.add(2, paramTypeAdd);
					toAdd.add(3, paramTypeDescAdd);
					toAdd.add(4, paramOccNoAdd);
					toAdd.add(5, paramEncodingAdd);
					toAdd.add(6, paramReqIndAdd);
					list.add(toAdd);
				}
				bean.setWebServiceParameters(list);
				break;
			}
		}
	}

	  /**
	   * RemoveSelectedTaskParameters
	   * @param bean
	   * @param request
	   * @return 
	   */
	private void RemoveSelectedTaskParameters (OperationsWizardBean bean, HttpServletRequest request)
	{
		this.Log("OperationsWizard.do: Removing Task Parameters",Level.DEBUG);
		ArrayList list = new ArrayList();
		int count = 1;
		for (int i = 0; true; i++) {
			String selected = request.getParameter("removeParam" + i);
			String paramName = request.getParameter("paramName" + i);
			//String paramType = request.getParameter("paramType"+i);
			String paramValue = request.getParameter("paramValue" + i);
			if (paramName != null && !paramName.equals("") && !(selected != null && selected.equals("on"))) {
				ArrayList temp = new ArrayList();
				temp.add(0, count+"");
				temp.add(1, paramName);
				//temp.add(2,paramType);
				temp.add(2, paramValue);
				list.add(temp);
				count++;
			}
			else if (paramName == null) {
				bean.setParameters(list);
				break;
			}
		}
	}

	  /**
	   * RemoveSelectedWebServiceParameters
	   * @param bean
	   * @param request
	   * @return 
	   */
	private void RemoveSelectedWebServiceParameters (OperationsWizardBean bean, HttpServletRequest request)
	{
		this.Log("OperationsWizard.do: Removing WebService Parameters",Level.DEBUG);
		ArrayList list = new ArrayList();
		int count = 1;
		for (int i = 0; true; i++) {
			String selected = request.getParameter("removeParam" + i);
			String paramName = request.getParameter("paramName" + i);
			// WI 21296
			String paramType = request.getParameter("paramType" + i);
			String paramTypeDesc = request.getParameter("paramTypeDesc" + i);
			String paramOccNo = request.getParameter("paramOccNo" + i);
			String paramEncoding = request.getParameter("paramEncoding" + i);
			String paramReqInd = request.getParameter("paramReqInd" + i);
			if (paramName != null && !paramName.equals("") && !(selected != null && selected.equals("on"))) {
				ArrayList temp = new ArrayList();
				temp.add(0, count+"");
				temp.add(1, paramName);
				// WI 21296
				temp.add(2, paramType);
				temp.add(3, paramTypeDesc);
				temp.add(4, paramOccNo);
				temp.add(5, paramEncoding);
				temp.add(6, paramReqInd);
				list.add(temp);
				count++;
			}
			else if (paramName == null) {
				bean.setWebServiceParameters(list);
				break;
			}
		}
	}

	  /**
	   * SetSchedule
	   * @param bean
	   * @return Schedule
	   */
	private Schedule SetSchedule (OperationsWizardBean bean) throws Exception
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
	private void GetSchedule (OperationsWizardBean bean, Schedule schedule)
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
	private void SetCheckBoxes (OperationsWizardBean bean, HttpServletRequest request)
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
	   * @param 
	   * @return boolean
	   */
	private boolean Delete (OperationsWizardBean bean)
	{
		this.Log("OperationsWizard.do: Deleting Operation",Level.DEBUG);
		boolean retBool = false;
		try {
			Operation op = new Operation(bean.getName(),bean.getWebService(),Phrase.AdministrationLoggerName);
			if (op != null) {
				INodeOperation opDB = DBManager.GetNodeOperation(Phrase.AdministrationLoggerName);
				int opID = opDB.GetOperationID(op.GetOpName());				
				INodeOperationLog opLogDB = DBManager.GetNodeOperationLog(Phrase.AdministrationLoggerName);
				boolean hasOpLog = opLogDB.hasOperationLog(opID);
				if(hasOpLog){	// Check operation Log 
					bean.setMessage("Could Not Delete Operation, Transaction(s) have already been loggged.");    		  					
				}else{
					if(op.GetWizardFlag().equalsIgnoreCase("no")){
						if (op.Delete(Phrase.AdministrationLoggerName))
							retBool = true;
						else
							bean.setMessage("Could Not Delete Operation, Transaction(s) have already been loggged.");    		  
					}else{
						op.SetOperationID(opID);
						if (op.Delete(Phrase.AdministrationLoggerName))
							retBool = true;
						else
							bean.setMessage("Could Not Delete Operation, Transaction(s) have already been loggged.");    		  
					}					
				}
			}
		} catch (Exception e) {
			this.Log("OperationsWizard.do: Error Deleting Operation",Level.ERROR);
		}
		return retBool;
	}
	// WI 21296
	  /**
	   * Check Parameter
	   * @param bean
	   * @param 
	   * @return boolean
	   */
	private String checkParameters (HttpServletRequest request)
	{
		String ret = "ok";
		String paramName = null;
		String occNo = null;
		String encoding = null;
		String reqInd = null;
		String expression = "^[-+]?[0-9]*";
		Pattern pattern = Pattern.compile(expression);
		Matcher matcher = null;
		
		for (int i = 0; true; i++) {
			paramName = request.getParameter("paramName" + i);
			if(paramName == null || paramName.equalsIgnoreCase("")){
				break;
			}
			
			occNo = request.getParameter("paramOccNo" + i);
			
			// WI 22224
			if(occNo != null){
				matcher = pattern.matcher(occNo);
			}
			if(occNo!=null && !occNo.equalsIgnoreCase("") && !matcher.matches()){   
				ret = "The Parameter Occurence Number should be integer.";  
				break;
			}   
			
			encoding = request.getParameter("paramEncoding" + i);
			if(encoding!=null && !encoding.equalsIgnoreCase("") && !encoding.equalsIgnoreCase(Phrase.XML_TYPE)
				&& !encoding.equalsIgnoreCase(Phrase.ZIP_TYPE)
				&& !encoding.equalsIgnoreCase(Phrase.BASE64_TYPE)
				&& !encoding.equalsIgnoreCase(Phrase.ENCRYPT_TYPE)
				&& !encoding.equalsIgnoreCase(Phrase.DIGEST_TYPE)
				&& !encoding.equalsIgnoreCase(Phrase.NONE_TYPE)
			){
				ret = "The Parameter Encoding should be: " 
					+ Phrase.XML_TYPE + ","
					+ Phrase.ZIP_TYPE + ","
					+ Phrase.BASE64_TYPE + ","
					+ Phrase.ENCRYPT_TYPE + ","
					+ Phrase.DIGEST_TYPE + ","
					+ Phrase.NONE_TYPE + "."
					;  
				break;				
			}

			reqInd = request.getParameter("paramReqInd" + i);
			if(reqInd!=null && !reqInd.equalsIgnoreCase("") && !reqInd.equalsIgnoreCase("true")
				&& !reqInd.equalsIgnoreCase("false")
			){
				ret = "The Parameter Required Indicator should be: true or false.";  
				break;				
			}
		}

		return ret;
	
	}
	// WI 22233
	  /**
	   * Set All Policy
	   * @param webMethodName
	   * @param opName
	   * @param toPermit
	   * @return boolean
	   */
	private boolean setAllPolicy(String webMethodName, String opName, String toPermit) 
	{
	    NAASIntegration naas = new NAASIntegration(Phrase.AdministrationLoggerName);
	    boolean ret = true;
	    if(toPermit.equalsIgnoreCase("Y")){
	    	ret = naas.setAllPolicy(webMethodName, opName, NAASRequestor.ACTION_DENY);
	    }else{
	    	String isExit = verifyPolicy(webMethodName, opName);
	    	if(isExit!= null && isExit.equalsIgnoreCase("deny")){
		    	ret = naas.setAllPolicy(webMethodName, opName, "");	    		    		
	    	}
	    }
	    return ret;
	}
	
	// WI 22233
	  /**
	   * verify Policy
	   * @param webMethodName
	   * @param opName
	   * @return String
	   */
	private String verifyPolicy(String webMethodName, String opName)
	{
	    NAASIntegration naas = new NAASIntegration(Phrase.AdministrationLoggerName);
	    String ret = naas.verifyPolicy(null,webMethodName, opName);
	    return ret;
	}
	
}
