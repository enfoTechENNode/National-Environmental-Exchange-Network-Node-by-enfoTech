package Node2.web.Status;

import java.io.PrintWriter;
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
import org.apache.struts.actions.DispatchAction;

import Node.Phrase;
import Node.Biz.Administration.User;
import Node.DB.DBManager;
import Node.DB.Interfaces.Configuration.ISystemConfiguration;
import Node2.model.Status.Status;
import Node2.service.Status.StatusManager;
/**
 * <p>This class create StatusAction.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class StatusAction extends DispatchAction {

	private boolean debug = true;
	private String startIndex;
	private String recordsReturned;
	private String sort;
	private String dir;

	private static Log log = LogFactory.getLog(StatusAction.class);
	private StatusManager statusMgr = null;
	SimpleDateFormat dateFormat = new SimpleDateFormat("MM/dd/yyyy HH:mm:ss");

	  /**
	   * setStatusManager
	   * @param statusMgr
	   * @return 
	   */
	public void setStatusManager(StatusManager statusMgr) {
		this.statusMgr = statusMgr;
	}
	
	  /**
	   * getStatusList
	   * @param mapping
	   * @param form
	   * @param request
	   * @param response
	   * @return ActionForward
	   */
	public ActionForward getStatusList(ActionMapping mapping, ActionForm form,
			HttpServletRequest request, HttpServletResponse response)
			throws Exception {
		// get page metadata
		getPageMetaData(request);
		HttpSession session = request.getSession();
		User user= new User((String)session.getAttribute(Phrase.USER_SESSION),Phrase.AdministrationLoggerName);;
		
		if (log.isDebugEnabled()) {
			log.debug("entering 'getStatusList' method...");
		}
		
		List statusList = statusMgr.getStatusList(startIndex, recordsReturned);
	    ISystemConfiguration config = DBManager.GetSystemConfiguration(Phrase.AdministrationLoggerName);
		String status = config.GetNodeMessage();

		String records = changeStatusListListToJsonString(statusList,status);
		printMsgToClient(records, response);

		return null;
	}

	  /**
	   * getStatus
	   * @param mapping
	   * @param form
	   * @param request
	   * @param response
	   * @return ActionForward
	   */
	public ActionForward getStatus(ActionMapping mapping, ActionForm form,
			HttpServletRequest request, HttpServletResponse response)
			throws Exception {
		// get page metadata
		getPageMetaData(request);
		HttpSession session = request.getSession();
		User user= new User((String)session.getAttribute(Phrase.USER_SESSION),Phrase.AdministrationLoggerName);;
		
		if (log.isDebugEnabled()) {
			log.debug("entering 'getStatus' method...");
		}
	    ISystemConfiguration config = DBManager.GetSystemConfiguration(Phrase.AdministrationLoggerName);
		String status = config.GetNodeMessage();
		
		printMsgToClient(status, response);

		return null;
	}

	  /**
	   * printMsgToClient
	   * @param result
	   * @param response
	   * @return 
	   */
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
	   * changeStatusListListToJsonString
	   * @param aList
	   * @param status
	   * @return 
	   */
	private String changeStatusListListToJsonString(List aList,String status ){
		String records = "";
		Status ret;
		List jsonList = new ArrayList();

		if(aList.size()>0){
            jsonList.add("{\"gridId\":" + 0
                    +",\"pId\":" + "\"" + "status" + "\"" 
                    +",\"processName\":" + "\"" + "status" + "\"" 
        			+",\"processStatus\":" + "\"" + status + "\"" 
        			+",\"operationId\":" + "\"" + "status" + "\"" 
        			+"}");                  		
		
			for(int i=0;i<aList.size();i++){
	        	ret = (Status)aList.get(i);
	            jsonList.add("{\"gridId\":" + i+1
	                        +",\"pId\":" + "\"" + ((Long)ret.getProcessId()).toString() + "\"" 
	                        +",\"processName\":" + "\"" + ret.getProcessName() + "\"" 
	            			+",\"processStatus\":" + "\"" + ret.getProcessStatus() + "\"" 
	            			+",\"operationId\":" + "\"" + ret.getOperationId() + "\"" 
	            			+"}");                  		
			}						
		}else{
            jsonList.add("{\"gridId\":" + 0
                    +",\"pId\":" + "\"" + "status" + "\"" 
                    +",\"processName\":" + "\"" + "status" + "\"" 
        			+",\"processStatus\":" + "\"" + status + "\"" 
        			+",\"operationId\":" + "\"" + "status" + "\"" 
        			+"}");                  					
		}
		JSONArray jsonArray = JSONArray.fromObject(jsonList);           
		
		Map map = new HashMap();
		map.put("results", jsonArray);
		JSONObject jsonObject = JSONObject.fromObject(map);
		records = jsonObject.toString();
		records = records.substring(records.indexOf("{") + 1);
		records = "{\"total\":"+ statusMgr.getTotalRecords() +","+ records;
		
		return records;
	}

	  /**
	   * getPageMetaData
	   * @param request
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

}
