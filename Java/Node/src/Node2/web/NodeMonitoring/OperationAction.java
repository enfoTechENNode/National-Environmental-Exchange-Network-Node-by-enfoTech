package Node2.web.NodeMonitoring;

import java.io.PrintWriter;
import java.sql.Date;
import java.sql.Timestamp;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Enumeration;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.HttpSession;

import net.sf.json.JSONArray;
import net.sf.json.JSONObject;

import org.apache.commons.logging.Log;
import org.apache.commons.logging.LogFactory;
import org.apache.struts.action.ActionForm;
import org.apache.struts.action.ActionForward;
import org.apache.struts.action.ActionMapping;
import org.apache.struts.action.ActionMessage;
import org.apache.struts.action.ActionMessages;
import org.apache.struts.action.DynaActionForm;
import org.apache.struts.actions.DispatchAction;

import Node.Phrase;
import Node.Biz.Administration.User;
import Node.DB.DBManager;
import Node.DB.Interfaces.INodeDomain;
import Node.DB.Interfaces.INodeUser;
import Node.Utils.Utility;
import Node2.model.Domains.Operation;
import Node2.model.Domains.OperationLog;
import Node2.service.Domains.WebserviceManager;
import Node2.service.Domains.DomainManager;
import Node2.service.Domains.OperationLogManager;
import Node2.service.Domains.OperationLogStatusManager;
import Node2.service.Domains.OperationManager;
/**
 * <p>This class create OperationAction.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class OperationAction extends DispatchAction {

	private boolean debug = false;
	private String startIndex;
	private String recordsReturned;
	private String sort;
	private String dir;

	private static Log log = LogFactory.getLog(OperationAction.class);
	private OperationManager mgr = null;
	private OperationLogManager operationLogMgr = null;
	private OperationLogStatusManager operationLogStatusMgr = null;
	private DomainManager domainMgr = null;
	private WebserviceManager webserviceMgr = null;
	SimpleDateFormat dateFormat = new SimpleDateFormat("MM/dd/yyyy HH:mm:ss");

	  /**
	   * setOperationManager
	   * @param operationManager
	   * @return 
	   */
	public void setOperationManager(OperationManager operationManager) {
		this.mgr = operationManager;
	}
	
	  /**
	   * setOperationLogManager
	   * @param operationLogManager
	   * @return 
	   */
	public void setOperationLogManager(OperationLogManager operationLogManager) {
		this.operationLogMgr = operationLogManager;
	}
	
	  /**
	   * setOperationLogStatusManager
	   * @param operationLogStatusManager
	   * @return 
	   */
	public void setOperationLogStatusManager(OperationLogStatusManager operationLogStatusManager) {
		this.operationLogStatusMgr = operationLogStatusManager;
	}

	  /**
	   * setDomainManager
	   * @param domainManager
	   * @return 
	   */
	public void setDomainManager(DomainManager domainManager) {
		this.domainMgr = domainManager;
	}

	  /**
	   * setWebserviceManager
	   * @param webserviceManager
	   * @return 
	   */
	public void setWebserviceManager(WebserviceManager webserviceManager) {
		this.webserviceMgr = webserviceManager;
	}

	  /**
	   * init
	   * @param mapping
	   * @param form
	   * @param request
	   * @param response
	   * @return ActionForward
	   */
	public ActionForward init(ActionMapping mapping, ActionForm form,
			HttpServletRequest request, HttpServletResponse response)
			throws Exception {
		if (log.isDebugEnabled()) {
			log.debug("entering 'init' method...");
		}
		/*List opList = mgr.getOperations();
		Map map = new HashMap();
		map.put("records", opList);

		JSONObject jsonObject = JSONObject.fromObject(map);

		String ret = jsonObject.toString();
		ret = ret.substring(ret.indexOf("{") + 1);
*/
		return mapping.findForward("initial");
	}

	  /**
	   * delete
	   * @param mapping
	   * @param form
	   * @param request
	   * @param response
	   * @return ActionForward
	   */
	public ActionForward delete(ActionMapping mapping, ActionForm form,
			HttpServletRequest request, HttpServletResponse response)
			throws Exception {
		if (log.isDebugEnabled()) {
			log.debug("entering 'delete' method...");
		}

		mgr.removeOperation(request.getParameter("Operation.id"));

		ActionMessages messages = new ActionMessages();
		messages.add(ActionMessages.GLOBAL_MESSAGE, new ActionMessage(
				"Operation.deleted"));

		saveMessages(request, messages);

		return list(mapping, form, request, response);
	}

	  /**
	   * edit
	   * @param mapping
	   * @param form
	   * @param request
	   * @param response
	   * @return ActionForward
	   */
	public ActionForward edit(ActionMapping mapping, ActionForm form,
			HttpServletRequest request, HttpServletResponse response)
			throws Exception {
		if (log.isDebugEnabled()) {
			log.debug("entering 'edit' method...");
		}

		DynaActionForm OperationForm = (DynaActionForm) form;
		String OperationId = request.getParameter("id");

		// null OperationId indicates an add
		if (OperationId != null) {
			Operation Operation = mgr.getOperation(OperationId);

			if (Operation == null) {
				ActionMessages errors = new ActionMessages();
				errors.add(ActionMessages.GLOBAL_MESSAGE, new ActionMessage(
						"Operation.missing"));
				// saveErrors(request, errors);

				return mapping.findForward("list");
			}

			OperationForm.set("Operation", Operation);
		}

		return mapping.findForward("edit");
	}

	  /**
	   * list
	   * @param mapping
	   * @param form
	   * @param request
	   * @param response
	   * @return ActionForward
	   */
	public ActionForward list(ActionMapping mapping, ActionForm form,
			HttpServletRequest request, HttpServletResponse response)
			throws Exception {
		// get page metadata
		getPageMetaData(request);
		
		if (log.isDebugEnabled()) {
			log.debug("entering 'list' method...");
		}
		String opName = request.getParameter("opName");
		String opType = request.getParameter("opType");
		String opWebservice = request.getParameter("opWebservice");
		String opStatus = request.getParameter("opStatus");
		String opDomain = request.getParameter("opDomain");
		String opUserId = request.getParameter("opUserId");
		String opSecurityToken = request.getParameter("opSecurityToken");
		String startdt = request.getParameter("startdt");
		String enddt = request.getParameter("enddt");
		
		List opList = mgr.getOperations( startIndex, recordsReturned,opName,opType,opWebservice,opStatus,opDomain);
		Map map = new HashMap();
		//map.put("records", opList);
		map.put("results", opList);
		JSONObject jsonObject = JSONObject.fromObject(map);

		String records = jsonObject.toString();
		records = records.substring(records.indexOf("{") + 1);
/*		records = "{\"recordsReturned\":"+ this.recordsReturned +","
		+ "\"totalRecords\":"+ mgr.getTotalRecords() +","
		+ "\"startIndex\":"+ this.startIndex +"," 
		+ "\"sort\":"+ (this.sort==null?null:"\""+this.sort+"\"") +","
		+ "\"dir\":\""+ this.dir +"\","
		+ records;
*/		records = "{\"total\":"+ mgr.getTotalRecords() +","	+ records;
		// System.out.println(records);

		printMsgToClient(records, response);

		return null;
	}

	  /**
	   * getOperationNameList
	   * @param mapping
	   * @param form
	   * @param request
	   * @param response
	   * @return ActionForward
	   */
	public ActionForward getOperationNameList(ActionMapping mapping, ActionForm form,
			HttpServletRequest request, HttpServletResponse response)
			throws Exception {
		String[] opNameList = null;
		String opName = null;
		// get page metadata
		getPageMetaData(request);
		HttpSession session = request.getSession();
		if (log.isDebugEnabled()) {
			log.debug("entering 'getOperationNameList' method...");
		}
	    try {
	        User admin = new User((String)session.getAttribute(Phrase.USER_SESSION),Phrase.AdministrationLoggerName);
	        opNameList = Node.Biz.Administration.OperationLog.GetUniqueOperationNameList(Phrase.AdministrationLoggerName,admin);
	        opName = Utility.changeStringArrayToJsonString(opNameList,"operationName");
			printMsgToClient(opName, response);

	    } catch (Exception e) {
	    	log.debug("NodeMonitoring.do: Could Not Set Web Page: "+e.toString());
	    }

		return null;
	}

	  /**
	   * getOperatioIdNameList
	   * @param mapping
	   * @param form
	   * @param request
	   * @param response
	   * @return ActionForward
	   */
	public ActionForward getOperatioIdNameList(ActionMapping mapping, ActionForm form,
			HttpServletRequest request, HttpServletResponse response)
			throws Exception {
		Node.Biz.Administration.Operation[] opList = null;
		String opIdName = null;
		// get page metadata
		getPageMetaData(request);
		HttpSession session = request.getSession();
		if (log.isDebugEnabled()) {
			log.debug("entering 'getOperationNameList' method...");
		}
	    try {
	        User admin = new User((String)session.getAttribute(Phrase.USER_SESSION),Phrase.AdministrationLoggerName);
	        // WI 22317 
	        opList = Node.Biz.Administration.Operation.GetAllOperationsList(Phrase.AdministrationLoggerName,admin);
	        opIdName = this.changeOperaionIdNameListToJsonString(opList);
			printMsgToClient(opIdName, response);

	    } catch (Exception e) {
	    	log.debug("NodeMonitoring.do: Could Not Set Web Page: "+e.toString());
	    }

		return null;
	}

	  /**
	   * getOperationLogList
	   * @param mapping
	   * @param form
	   * @param request
	   * @param response
	   * @return ActionForward
	   */
	public ActionForward getOperationLogList(ActionMapping mapping, ActionForm form,
			HttpServletRequest request, HttpServletResponse response)
			throws Exception {
		// get page metadata
		getPageMetaData(request);
		
		if (log.isDebugEnabled()) {
			log.debug("entering 'getOperationLogList' method...");
		}
		HttpSession session = request.getSession();
        INodeUser userDB;
		userDB = DBManager.GetNodeUser(Phrase.AdministrationLoggerName);
		User admin = userDB.GetUser((String)session.getAttribute(Phrase.USER_SESSION));
		String[] domainPermission = null;
		String opName = request.getParameter("opName");
		String opType = request.getParameter("opType");
		String opWebservice = request.getParameter("opWebservice");
		String opStatus = request.getParameter("opStatus");
		String opDomain = request.getParameter("opDomain");
		String opUserId = request.getParameter("opUserId");
		String opSecurityToken = request.getParameter("opSecurityToken");
		String opTransactionId = request.getParameter("opTransactionId");
		String startdt = request.getParameter("startdt");
		String enddt = request.getParameter("enddt");
		String version =  request.getParameter("ver");

		if (admin.IsConsoleUser()) {
			domainPermission = null;
			INodeDomain domainDB = DBManager.GetNodeDomain(Phrase.AdministrationLoggerName);
			if (admin.IsNodeAdmin()) {
				domainPermission = domainDB.GetDomains();
				HashMap map = new HashMap();
				if (domainPermission != null)
					for (int i = 0; i < domainPermission.length; i++)
						map.put(domainPermission[i],domainPermission[i]);
				if (!map.isEmpty()) {
					Object[] temp = map.values().toArray();
					domainPermission = new String [temp.length];
					for (int i = 0; i < domainPermission.length; i++)
						domainPermission[i] = (String)temp[i];
				}
				else
					domainPermission = null;
			}
			else
				domainPermission = admin.GetAssignedDomains();
		}
		List opList = operationLogMgr.getOperationLogs( startIndex, recordsReturned,sort,dir,opName,opType,opWebservice,opStatus,opDomain,
				opUserId,opSecurityToken,opTransactionId,startdt,enddt,domainPermission,version);

		String records = changeOperationLogSearchListToJsonString(opList);
		if(debug){
			System.out.println(records);
		}
		printMsgToClient(records, response);

		return null;
	}

	  /**
	   * getTaskLogList
	   * @param mapping
	   * @param form
	   * @param request
	   * @param response
	   * @return ActionForward
	   */
	public ActionForward getTaskLogList(ActionMapping mapping, ActionForm form,
			HttpServletRequest request, HttpServletResponse response)
			throws Exception {
		// get page metadata
		getPageMetaData(request);
		
		if (log.isDebugEnabled()) {
			log.debug("entering 'getTaskLogList' method...");
		}
		HttpSession session = request.getSession();
        INodeUser userDB;
		userDB = DBManager.GetNodeUser(Phrase.AdministrationLoggerName);
		User admin = userDB.GetUser((String)session.getAttribute(Phrase.USER_SESSION));
		String[] domainPermission = null;
		String opName = request.getParameter("opName");
		String opType = request.getParameter("opType");
		String opWebservice = request.getParameter("opWebservice");
		String opStatus = request.getParameter("opStatus");
		String opDomain = request.getParameter("opDomain");
		String opUserId = request.getParameter("opUserId");
		String opSecurityToken = request.getParameter("opSecurityToken");
		String opTransactionId = request.getParameter("opTransactionId");
		String startdt = request.getParameter("startdt");
		String enddt = request.getParameter("enddt");
		String version =  request.getParameter("ver");
		
		if (admin.IsConsoleUser()) {
			domainPermission = null;
			INodeDomain domainDB = DBManager.GetNodeDomain(Phrase.AdministrationLoggerName);
			if (admin.IsNodeAdmin()) {
				domainPermission = domainDB.GetDomains();
				HashMap map = new HashMap();
				if (domainPermission != null)
					for (int i = 0; i < domainPermission.length; i++)
						map.put(domainPermission[i],domainPermission[i]);
				if (!map.isEmpty()) {
					Object[] temp = map.values().toArray();
					domainPermission = new String [temp.length];
					for (int i = 0; i < domainPermission.length; i++)
						domainPermission[i] = (String)temp[i];
				}
				else
					domainPermission = null;
			}
			else
				domainPermission = admin.GetAssignedDomains();
		}

		List opList = operationLogMgr.getScheduledTasksLogs( startIndex, recordsReturned,sort,dir,opName,opType,opWebservice,opStatus,opDomain,
				opUserId,opSecurityToken,opTransactionId,startdt,enddt,domainPermission,version);

		String records = changeTaskLogSearchListToJsonString(opList);
		if(debug){
			System.out.println(records);
		}
		printMsgToClient(records, response);

		return null;
	}

	  /**
	   * getNotificationLogList
	   * @param mapping
	   * @param form
	   * @param request
	   * @param response
	   * @return ActionForward
	   */
	public ActionForward getNotificationLogList(ActionMapping mapping, ActionForm form,
			HttpServletRequest request, HttpServletResponse response)
			throws Exception {
		// get page metadata
		getPageMetaData(request);
		
		if (log.isDebugEnabled()) {
			log.debug("entering 'getNotificationLogList' method...");
		}
		HttpSession session = request.getSession();
        INodeUser userDB;
		userDB = DBManager.GetNodeUser(Phrase.AdministrationLoggerName);
		User admin = userDB.GetUser((String)session.getAttribute(Phrase.USER_SESSION));
		String[] domainPermission = null;
		String nodeAddress = request.getParameter("nodeAddress");
		String opWebservice = request.getParameter("opWebservice");
		String startdt = request.getParameter("startdt");
		String enddt = request.getParameter("enddt");
		String version =  request.getParameter("ver");
		
		if (admin.IsConsoleUser()) {
			domainPermission = null;
			INodeDomain domainDB = DBManager.GetNodeDomain(Phrase.AdministrationLoggerName);
			if (admin.IsNodeAdmin()) {
				domainPermission = domainDB.GetDomains();
				HashMap map = new HashMap();
				if (domainPermission != null)
					for (int i = 0; i < domainPermission.length; i++)
						map.put(domainPermission[i],domainPermission[i]);
				if (!map.isEmpty()) {
					Object[] temp = map.values().toArray();
					domainPermission = new String [temp.length];
					for (int i = 0; i < domainPermission.length; i++)
						domainPermission[i] = (String)temp[i];
				}
				else
					domainPermission = null;
			}
			else
				domainPermission = admin.GetAssignedDomains();
		}

		List opList = operationLogMgr.getNotificationLogs(startIndex, recordsReturned, sort, dir, nodeAddress, opWebservice, startdt, enddt,domainPermission, version);

		String records = changeNotificationSearchListToJsonString(opList);
		if(debug){
			System.out.println("notificationLog record is: " + records);
		}
		printMsgToClient(records, response);

		return null;
	}

	  /**
	   * getOperationStatusList
	   * @param mapping
	   * @param form
	   * @param request
	   * @param response
	   * @return ActionForward
	   */
	public ActionForward getOperationStatusList(ActionMapping mapping, ActionForm form,
			HttpServletRequest request, HttpServletResponse response)
			throws Exception {
		String[] opStatusList = null;
		String opStatus = null;
		// get page metadata
		getPageMetaData(request);
		HttpSession session = request.getSession();
		if (log.isDebugEnabled()) {
			log.debug("entering 'getOperationStatusList' method...");
		}
	    try {
	        User admin = new User((String)session.getAttribute(Phrase.USER_SESSION),Phrase.AdministrationLoggerName);
	        opStatusList = Node.Biz.Administration.OperationLog.GetUniqueStatusList(Phrase.AdministrationLoggerName,admin);
	        opStatus = Utility.changeStringArrayToJsonString(opStatusList,"opStatus");
			printMsgToClient(opStatus, response);

	    } catch (Exception e) {
	    	log.debug("NodeMonitoring.do: Could Not Set Web Page: "+e.toString());
	    }

		return null;
	}

	  /**
	   * getDomainList
	   * @param mapping
	   * @param form
	   * @param request
	   * @param response
	   * @return ActionForward
	   */
	public ActionForward getDomainList(ActionMapping mapping, ActionForm form,
			HttpServletRequest request, HttpServletResponse response)
			throws Exception {
		String[] opDomainList = null;
		String opDomain = null;
		// get page metadata
		getPageMetaData(request);
		HttpSession session = request.getSession();
		if (log.isDebugEnabled()) {
			log.debug("entering 'getDomainList' method...");
		}
	    try {
	        User admin = new User((String)session.getAttribute(Phrase.USER_SESSION),Phrase.AdministrationLoggerName);
	        opDomainList = admin.GetDomainsAvailable(null, Phrase.AdministrationLoggerName);
	        opDomain = Utility.changeStringArrayToJsonString(opDomainList,"opDomain");
			printMsgToClient(opDomain, response);

	    } catch (Exception e) {
	    	log.debug("NodeMonitoring.do: Could Not Set Web Page: "+e.toString());
	    }

		return null;
	}

	  /**
	   * delegateTransactionView
	   * @param mapping
	   * @param form
	   * @param request
	   * @param response
	   * @return ActionForward
	   */
	public ActionForward delegateTransactionView(ActionMapping mapping, ActionForm form,
			HttpServletRequest request, HttpServletResponse response)
			throws Exception {
		ActionForward actionForward = null;
		if (log.isDebugEnabled()) {
			log.debug("entering 'delegateTransactionView' method...");
		}
		actionForward = mapping.findForward("delegateTransactionView");
		String path = actionForward.getPath();
		path += "?opLogID="+request.getParameter("opLogID");
		actionForward = new ActionForward(path);
		return actionForward;
	}


	  /**
	   * save
	   * @param mapping
	   * @param form
	   * @param request
	   * @param response
	   * @return ActionForward
	   */
	public ActionForward save(ActionMapping mapping, ActionForm form,
			HttpServletRequest request, HttpServletResponse response)
			throws Exception {
		if (log.isDebugEnabled()) {
			log.debug("entering 'save' method...");
		}

		// run validation rules on this form
		ActionMessages errors = form.validate(mapping, request);
		if (!errors.isEmpty()) {
			// saveErrors(request, errors);
			return mapping.findForward("edit");
		}

		DynaActionForm OperationForm = (DynaActionForm) form;
		mgr.saveOperation((Operation) OperationForm.get("Operation"));

		ActionMessages messages = new ActionMessages();
		messages.add(ActionMessages.GLOBAL_MESSAGE, new ActionMessage(
				"Operation.saved"));
		saveMessages(request, messages);

		return list(mapping, form, request, response);
	}

	  /**
	   * unspecified
	   * @param mapping
	   * @param form
	   * @param request
	   * @param response
	   * @return ActionForward
	   */
	public ActionForward unspecified(ActionMapping mapping, ActionForm form,
			HttpServletRequest request, HttpServletResponse response)
			throws Exception {
		return list(mapping, form, request, response);
	}

	public static void printMsgToClient(String result,
			HttpServletResponse response) throws Exception {
		//response.setCharacterEncoding("UTF-8");
		PrintWriter out = response.getWriter();
		try {
			out.print(result);
		} finally {
			out.close();
		}
	}

	  /**
	   * getPageMetaData
	   * @param result
	   * @return 
	   */
	private void getPageMetaData(HttpServletRequest request) {
		startIndex = request.getParameter("start");
		if (startIndex == null) {
			startIndex = "0"; // define default value for parameter start if this page is called without parameter start.
		}
		
		recordsReturned = request.getParameter("limit");
		if ( (recordsReturned == null) || (0 == recordsReturned.length())) {
			recordsReturned = "10"; // define default value for parameter pagesize if this page is called without parameter pagesize.
		}

		sort = request.getParameter("sort");
		if ( (sort == null) || (0 == sort.length())) {
			sort = "operationId";
		}else{
			JSONArray jsonArray = JSONArray.fromObject(sort);
			JSONObject jsonObject = JSONObject.fromObject(jsonArray.get(0)); 
			sort =  jsonObject.get("property")+"";
			dir = jsonObject.get("direction")+"";			
			
		}
		if ( (dir == null) || (0 == dir.length())) {
			dir = "Desc";
		}
	}

	  /**
	   * changeOperationLogSearchListToJsonString
	   * @param aList
	   * @return String
	   */
	private String changeOperationLogSearchListToJsonString(List aList ){
		String records = "";
		Object[] retList;
		List jsonList = new ArrayList();
		Timestamp startTime = null, endTime = null;
		
		for(int i=0;i<aList.size();i++){
        	retList = (Object[])aList.get(i);
        	if(retList[8] instanceof Date){
        		startTime = new Timestamp(((Date)retList[8]).getTime());
        	}else if(retList[8] instanceof Timestamp){
        		startTime = (Timestamp)retList[8]; 
        	}
        	if(retList[9] instanceof Date){
        		endTime = new Timestamp(((Date)retList[9]).getTime());
        	}else if(retList[9] instanceof Timestamp){
        		endTime = (Timestamp)retList[9]; 
        	}
            jsonList.add("{\"gridId\":" + i
                        +",\"operationLogId\":" + "\"" + retList[0] + "\"" 
                        +",\"operationName\":" + "\"" + (String)retList[1] + "\"" 
            			+",\"operationType\":" + "\"" + (String)retList[2] + "\"" 
            			+",\"webServiceName\":" + "\"" + (String)retList[3] + "\"" 
            			+",\"domainName\":" + "\"" + (String)retList[4] + "\"" 
            			+",\"userName\":" + "\"" + (String)retList[5] + "\"" 
               			+",\"token\":" + "\"" + (String)retList[6] + "\"" 
               			+",\"transId\":" + "\"" + (String)retList[7] + "\"" 
               			+",\"startDate\":" + "\"" + (startTime==null?"None":dateFormat.format(startTime)) + "\"" 
               			+",\"endDate\":" + "\"" + (endTime==null?"None":dateFormat.format(endTime)) + "\"" 
            			+",\"operationLogStatusCD\":" + "\"" + (String)retList[10] + "\"" 
            			+"}");                  		
		}
		
		JSONArray jsonArray = JSONArray.fromObject(jsonList);           
		
		Map map = new HashMap();
		map.put("results", jsonArray);
		JSONObject jsonObject = JSONObject.fromObject(map);
		records = jsonObject.toString();
		records = records.substring(records.indexOf("{") + 1);
		records = "{\"total\":"+ operationLogMgr.getTotalOperationLogRecords() +","	+ records;
		
		return records;
	}
	
	  /**
	   * changeTaskLogSearchListToJsonString
	   * @param aList
	   * @return String
	   */
	private String changeTaskLogSearchListToJsonString(List aList ){
		String records = "";
		Object[] retList;
		List jsonList = new ArrayList();

		for(int i=0;i<aList.size();i++){
        	retList = (Object[])aList.get(i);
            jsonList.add("{\"gridId\":" + i
                        +",\"operationLogId\":" + "\"" + retList[0] + "\"" 
                        +",\"operationName\":" + "\"" + (String)retList[1] + "\"" 
            			+",\"operationType\":" + "\"" + (String)retList[2] + "\"" 
            			+",\"webServiceName\":" + "\"" + (String)retList[3] + "\"" 
            			+",\"domainName\":" + "\"" + (String)retList[4] + "\"" 
            			+",\"userName\":" + "\"" + (String)retList[5] + "\"" 
               			+",\"token\":" + "\"" + (String)retList[6] + "\"" 
               			+",\"transId\":" + "\"" + (String)retList[7] + "\"" 
               			+",\"startDate\":" + "\"" + (retList[8]==null?"None":dateFormat.format((Timestamp)retList[8])) + "\"" 
               			+",\"endDate\":" + "\"" + (retList[9]==null?"None":dateFormat.format((Timestamp)retList[9])) + "\"" 
            			+",\"operationLogStatusCD\":" + "\"" + (String)retList[10] + "\"" 
            			+"}");                  		
		}
		
		JSONArray jsonArray = JSONArray.fromObject(jsonList);           
		
		Map map = new HashMap();
		map.put("results", jsonArray);
		JSONObject jsonObject = JSONObject.fromObject(map);
		records = jsonObject.toString();
		records = records.substring(records.indexOf("{") + 1);
		records = "{\"total\":"+ operationLogMgr.getTotalScheduledTasksLogRecords() +","+ records;
		
		return records;
	}

	  /**
	   * changeNotificationSearchListToJsonString
	   * @param aList
	   * @return String
	   */
	private String changeNotificationSearchListToJsonString(List aList ){
		String records = "";
		Object[] retList;
		List jsonList = new ArrayList();

		for(int i=0;i<aList.size();i++){
        	retList = (Object[])aList.get(i);
            jsonList.add("{\"gridId\":" + i
                        +",\"operationLogId\":" + "\"" + retList[0] + "\"" 
                        +",\"nodeAddress\":" + "\"" + ((String)retList[1]==null?" ":(String)retList[1]) + "\"" 
               			+",\"startDate\":" + "\"" + (retList[2]==null?"None":dateFormat.format((Timestamp)retList[2])) + "\"" 
            			+"}");                  		
		}
		
		JSONArray jsonArray = JSONArray.fromObject(jsonList);           
		
		Map map = new HashMap();
		map.put("results", jsonArray);
		JSONObject jsonObject = JSONObject.fromObject(map);
		records = jsonObject.toString();
		records = records.substring(records.indexOf("{") + 1);
		records = "{\"total\":"+ operationLogMgr.getTotalNotificationLogRecords() +","+ records;
		
		return records;
	}

	  /**
	   * changeOperationLogListToJsonString
	   * @param oList
	   * @return String
	   */
	private String changeOperationLogListToJsonString(List oList){
		String records = "";
		String tmp = "";
		List jsonList = new ArrayList();
		OperationLog opLog = null;
		
        for(int i=0;i<oList.size();i++){
        	opLog = (OperationLog)oList.get(i);
        	jsonList.add("{operationLogId:" + opLog.getOperationLogId()
        			+",\"transId\":" + "\"" + opLog.getTransId() + "\"" 
        			+",\"userName\":" + "\"" + opLog.getUserName() + "\"" 
        			+",\"requestorIP\":" + "\"" + opLog.getRequestorIP() + "\"" 
        			+",\"suppliedTransId\":" + "\"" + opLog.getSuppliedTransId() + "\"" 
        			+",\"token\":" + "\"" + opLog.getToken() + "\"" 
        			+",\"nodeAddress\":" + "\"" + opLog.getNodeAddress() + "\"" 
        			+",\"returnURL\":" + "\"" + opLog.getReturnURL() + "\"" 
        			+",\"serviceType\":" + "\"" + opLog.getServiceType() + "\"" 
        			+",\"startDate\":" + "\"" + opLog.getStartDate() + "\"" 
        			+",\"endDate\":" + "\"" + opLog.getEndDate() + "\"" 
        			+",\"hostName\":" + "\"" + opLog.getHostName() + "\"" 
        			+",\"createdBy\":" + "\"" + opLog.getCreatedBy() + "\"" 
        			+",\"createdDate\":" + "\"" + opLog.getCreatedDate() + "\"" 
        			+",\"updatedBy\":" + "\"" + opLog.getUpdatedBy() + "\"" 
        			+",\"updatedDate\":" + "\"" + opLog.getUpdatedDate() + "\"" 
        			+"}");
        }
        JSONArray jsonArray = JSONArray.fromObject(jsonList);           
		
		Map map = new HashMap();
		//map.put("records", opList);
		map.put("results", jsonArray);
		JSONObject jsonObject = JSONObject.fromObject(map);
		records = jsonObject.toString();
		records = records.substring(records.indexOf("{") + 1);
		records = "{\"total\":"+ oList.size() +","	+ records;
		
		return records;
	}

	  /**
	   * changeOperaionIdNameListToJsonString
	   * @param aList
	   * @return String
	   */
	private String changeOperaionIdNameListToJsonString(Node.Biz.Administration.Operation[] aList ){
		String records = "";
		Node.Biz.Administration.Operation op;
		List jsonList = new ArrayList();

		for(int i=0;i<aList.length;i++){
        	op = (Node.Biz.Administration.Operation)aList[i];
            jsonList.add("{\"operationId\":" + "\"" + op.GetOperationID() + "\"" 
                        +",\"operationName\":" + "\"" + op.GetOpName() + "\"" 
            			+"}");                  		
		}
		
		JSONArray jsonArray = JSONArray.fromObject(jsonList);           
		
		Map map = new HashMap();
		map.put("results", jsonArray);
		JSONObject jsonObject = JSONObject.fromObject(map);
		records = jsonObject.toString();
		records = records.substring(records.indexOf("{") + 1);
		records = "{\"total\":"+ aList.length +","+ records;
		
		return records;
	}


}
