package Node2.web.Domains;

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
import Node.Utils.Utility;
import Node2.service.Domains.DomainManager;
/**
 * <p>This class create DomainAction.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class DomainAction extends DispatchAction {

	private String startIndex;
	private String recordsReturned;
	private String sort;
	private String dir;

	private static Log log = LogFactory.getLog(DomainAction.class);
	private DomainManager domainMgr = null;
	SimpleDateFormat dateFormat = new SimpleDateFormat("MM/dd/yyyy HH:mm:ss");
	  /**
	   * setDomainManager
	   * @param domainManager
	   * @return 
	   */
	public void setDomainManager(DomainManager domainManager) {
		this.domainMgr = domainManager;
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
		// get page metadata
		getPageMetaData(request);
		HttpSession session = request.getSession();
		User user= new User((String)session.getAttribute(Phrase.USER_SESSION),Phrase.AdministrationLoggerName);;
		
		if (log.isDebugEnabled()) {
			log.debug("entering 'list' method...");
		}
		String domainName = request.getParameter("domainName");
		String domainStatus = request.getParameter("domainStatus");
		String domainStatusMSG = request.getParameter("domainStatusMSG");
		
		List domainList = domainMgr.getDomains(startIndex,recordsReturned,sort,dir,user,Phrase.AdministrationLoggerName,domainName,domainStatus, domainStatusMSG);

		String records = changeDomainSearchListToJsonString(domainList);
//		System.out.println(records);
		printMsgToClient(records, response);

		return null;
	}

	  /**
	   * getDomainNameList
	   * @param mapping
	   * @param form
	   * @param request
	   * @param response
	   * @return ActionForward
	   */
	public ActionForward getDomainNameList(ActionMapping mapping, ActionForm form,
			HttpServletRequest request, HttpServletResponse response)
			throws Exception {
		String[] domainList = null;
		String domain = null;
		// get page metadata
		getPageMetaData(request);
		HttpSession session = request.getSession();
		if (log.isDebugEnabled()) {
			log.debug("entering 'getDomainList' method...");
		}
	    try {
	        User admin = new User((String)session.getAttribute(Phrase.USER_SESSION),Phrase.AdministrationLoggerName);
	        domainList = admin.GetDomainsAvailable(null, Phrase.AdministrationLoggerName);
	        domain = Utility.changeStringArrayToJsonString(domainList,"domainName");
			printMsgToClient(domain, response);

	    } catch (Exception e) {
	    	log.debug("NodeMonitoring.do: Could Not Set Web Page: "+e.toString());
	    }

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
	   * changeDomainSearchListToJsonString
	   * @param aList
	   * @return 
	   */
	private String changeDomainSearchListToJsonString(List aList ){
		String records = "";
		Object[] retList;
		List jsonList = new ArrayList();
		List jsonFilterList = new ArrayList();
		String tmpDomainId = "";
		String tmpDomainAdminList = "";
		int recordIndex = -1;

		for(int i=0;i<aList.size();i++){
        	retList = (Object[])aList.get(i);
        	if(tmpDomainId.equalsIgnoreCase(((Long)retList[0]).toString())){	// append admin to the adminlist
        		tmpDomainAdminList = tmpDomainAdminList + "," +(String)retList[2];
        		jsonList.set(recordIndex,"{\"gridId\":" + i
                        +",\"domainId\":" + "\"" + retList[0] + "\"" 
                        +",\"domainName\":" + "\"" + (String)retList[1] + "\"" 
            			+",\"domainAdmin\":" + "\"" + tmpDomainAdminList + "\"" 
            			+",\"domainStatusCD\":" + ((String)retList[3]==null?"\"" + "\"":"\""+(String)retList[3]+ "\"")
            			+",\"domainStatusMSG\":" + ((String)retList[4]==null?"\"" + "\"":"\""+(String)retList[4]+ "\"")
            			+"}");
        	}else{
                jsonList.add("{\"gridId\":" + i
                        +",\"domainId\":" + "\"" + retList[0] + "\"" 
                        +",\"domainName\":" + "\"" + (String)retList[1] + "\"" 
            			+",\"domainAdmin\":" + ((String)retList[2]==null?"\"" + "\"":"\""+(String)retList[2]+ "\"") 
            			+",\"domainStatusCD\":" + ((String)retList[3]==null?"\"" + "\"":"\""+(String)retList[3]+ "\"")
            			+",\"domainStatusMSG\":" + ((String)retList[4]==null?"\"" + "\"":"\""+(String)retList[4]+ "\"")
            			+"}");
                recordIndex ++;
            	tmpDomainAdminList = (String)retList[2];
        	}
        	tmpDomainId = ((Long)retList[0]).toString();
		}
		// handle startIndex and recordsReturned
		int len = 0;
		if(Integer.parseInt(recordsReturned)<jsonList.size()) len = Integer.parseInt(recordsReturned);
		else len = jsonList.size();
		for(int i=Integer.parseInt(startIndex);i<((Integer.parseInt(startIndex)+len)<jsonList.size()?(Integer.parseInt(startIndex)+len):jsonList.size());i++){
			jsonFilterList.add(jsonList.get(i));			
		}
		JSONArray jsonArray = JSONArray.fromObject(jsonFilterList);           
		
		Map map = new HashMap();
		map.put("results", jsonArray);
		JSONObject jsonObject = JSONObject.fromObject(map);
		records = jsonObject.toString();
		records = records.substring(records.indexOf("{") + 1);
		records = "{\"total\":"+ domainMgr.getTotalRecords() +","	+ records;
		
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
