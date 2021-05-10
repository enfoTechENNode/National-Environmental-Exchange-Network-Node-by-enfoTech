package Node2.web.Documents;

import java.io.PrintWriter;
import java.math.BigDecimal;
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
import org.dom4j.Attribute;
import org.dom4j.Document;
import org.dom4j.Node;

import Node.Phrase;
import Node.Biz.Administration.Operation;
import Node.Biz.Administration.User;
import Node.Utils.Utility;
import Node2.service.Documents.DocumentManager;
/**
 * <p>This class create DocumentAction.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class DocumentAction extends DispatchAction {

	private String startIndex;
	private String recordsReturned;
	private String sort;
	private String dir;

	private static Log log = LogFactory.getLog(DocumentAction.class);
	private DocumentManager documentMgr = null;
	SimpleDateFormat dateFormat = new SimpleDateFormat("MM/dd/yyyy HH:mm:ss");
	  /**
	   * setDocumentManager
	   * @param documentManager
	   * @return 
	   */
	public void setDocumentManager(DocumentManager documentManager) {
		this.documentMgr = documentManager;
	}

	  /**
	   * getDocumentList
	   * @param mapping
	   * @param form
	   * @param request
	   * @param response
	   * @return ActionForward
	   */
	public ActionForward getDocumentList(ActionMapping mapping, ActionForm form,
			HttpServletRequest request, HttpServletResponse response)
			throws Exception {
		// get page metadata
		getPageMetaData(request);
		HttpSession session = request.getSession();
		User user= new User((String)session.getAttribute(Phrase.USER_SESSION),Phrase.AdministrationLoggerName);;
		
		if (log.isDebugEnabled()) {
			log.debug("entering 'getDocumentList' method...");
		}
		String documentName = request.getParameter("documentName");
		String transId = request.getParameter("transId");
		String domainName = request.getParameter("domainName");
		String dataFlowName = request.getParameter("dataFlowName");
		String startdt = request.getParameter("startdt");
		String enddt = request.getParameter("enddt");
		
		List documentList = documentMgr.getDocuments(startIndex,recordsReturned,sort,dir,user,Phrase.AdministrationLoggerName, documentName, transId, domainName, dataFlowName, startdt, enddt);

		String records = changeDocumentSearchListToJsonString(documentList);
		printMsgToClient(records, response);

		return null;
	}

	  /**
	   * getOperationMgrDocumentList
	   * @param mapping
	   * @param form
	   * @param request
	   * @param response
	   * @return ActionForward
	   */
	public ActionForward getOperationMgrDocumentList(ActionMapping mapping, ActionForm form,
			HttpServletRequest request, HttpServletResponse response)
			throws Exception {
		// get page metadata
		getPageMetaData(request);
		HttpSession session = request.getSession();
		User user= new User((String)session.getAttribute(Phrase.USER_SESSION),Phrase.AdministrationLoggerName);;
		
		if (log.isDebugEnabled()) {
			log.debug("entering 'getDocumentList' method...");
		}
		String documentName = request.getParameter("documentName");
		String transId = request.getParameter("transId");
		String domainName = request.getParameter("domainName");
		String dataFlowNameInput = request.getParameter("dataFlowName");
		String conditionName = request.getParameter("conditionName");
		String conditionSign = request.getParameter("conditionSign");
		String conditionValue = request.getParameter("conditionValue");
		String conditionStyle = request.getParameter("conditionStyle");
		String startdt = request.getParameter("startdt");
		String enddt = request.getParameter("enddt");
		String[] dataFlowName;
		List dataFlowNameList = new ArrayList();
		byte[] fileByte = null;
		Operation[] opList = null;
		Document doc4jVR = null;
		
		if(dataFlowNameInput==null || dataFlowNameInput.equals("")){	// get granted data flow name
/*	        IOperationMgr operationMgr = DBManager.getOperationMgr(Phrase.AdministrationLoggerName);
			fileByte = operationMgr.GetOperationListFile();
	        opList = Operation.GetOperationsList(Phrase.AdministrationLoggerName,user);
	        List grantOperationMgrBeanList = new ArrayList();
			if (fileByte != null){
			    org.dom4j.io.SAXReader reader = new org.dom4j.io.SAXReader();
			    doc4jVR = reader.read(new ByteArrayInputStream(fileByte));
			    // get operationId and Name
			    List operationMgrBeanList = doc4jVR.selectNodes(".//OperationList/Operation");

			    if (operationMgrBeanList != null && operationMgrBeanList.size() > 0) {
			    	for(Iterator i = operationMgrBeanList.iterator(); i.hasNext();){
			    		Node operationMgrBean = (Node)i.next();
			    		for(int j=0;j<opList.length;j++){
				    		if(this.getAttributeValue(operationMgrBean,"@id").trim().equalsIgnoreCase(Integer.toString(((Operation)opList[j]).GetOperationID()))){			    			
				    			dataFlowNameList.add((String)((Operation)opList[j]).GetOpName());
				    		}			    			
			    		}
			    	}
			    }

			}			
			dataFlowName = new String[dataFlowNameList.size()];
			int j=0;
			for(Iterator i = dataFlowNameList.iterator(); i.hasNext();){
				dataFlowName[j] = (String)i.next();
				j++;
			}
*/
			
			dataFlowName = new String[1];
			dataFlowName[0] = "";
		}else{
			dataFlowName = new String[1];
			dataFlowName[0] = dataFlowNameInput;
		}
		List documentList = documentMgr.getOperationMgrDocuments(startIndex,recordsReturned,sort,dir,user,Phrase.AdministrationLoggerName, documentName, transId, domainName, dataFlowName, startdt, enddt, conditionName,conditionSign,conditionValue, conditionStyle);

		String records = changeOpDocumentSearchListToJsonString(conditionName,documentList);
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
		String document = null;
		// get page metadata
		getPageMetaData(request);
		HttpSession session = request.getSession();
		if (log.isDebugEnabled()) {
			log.debug("entering 'getDocumentList' method...");
		}
	    try {
	        User admin = new User((String)session.getAttribute(Phrase.USER_SESSION),Phrase.AdministrationLoggerName);
	        domainList = admin.GetDomainsAvailable(null, Phrase.AdministrationLoggerName);
	        document = Utility.changeStringArrayToJsonString(domainList,"domainName");
			printMsgToClient(document, response);

	    } catch (Exception e) {
	    	log.debug("NodeMonitoring.do: Could Not Set Web Page: "+e.toString());
	    }

		return null;
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
		
		boolean status = false;
		if (log.isDebugEnabled()) {
			log.debug("entering 'document delete' method...");
		}

		status = documentMgr.removeDocument(request.getParameter("fileId"));

		if(status)	printMsgToClient("The document has been deleted.", response);
		else printMsgToClient("Fail to delete document.", response);
		return null;
	}

	  /**
	   * deleteDocuments
	   * @param mapping
	   * @param form
	   * @param request
	   * @param response
	   * @return ActionForward
	   */
	public ActionForward deleteDocuments(ActionMapping mapping, ActionForm form,
			HttpServletRequest request, HttpServletResponse response)
			throws Exception {
		
		boolean status = false;
		if (log.isDebugEnabled()) {
			log.debug("entering 'deleteDocuments' method...");
		}
		
		String fileIdJson = request.getParameter("fileIdList");
		status = documentMgr.removeDocuments(fileIdJson);

		if(status)	printMsgToClient("The document has been deleted.", response);
		else printMsgToClient("Fail to delete document.", response);
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
	   * changeDocumentSearchListToJsonString
	   * @param aList
	   * @return String
	   */
	private String changeDocumentSearchListToJsonString(List aList ){
		String records = "";
		Object[] retList;
		List jsonList = new ArrayList();
		
		if(aList!=null){
			for(int i=0;i<aList.size();i++){
	        	retList = (Object[])aList.get(i);
	            jsonList.add("{\"gridId\":" + i
	                        +",\"fileId\":" + "\"" + ((BigDecimal)retList[0]).toString() + "\"" 
	                        +",\"fileName\":" + "\"" + (String)retList[1] + "\"" 
	            			+",\"fileType\":" + "\"" + (String)retList[2] + "\"" 
	            			+",\"fileSize\":" + "\"" + ((retList[3]==null? new BigDecimal(0):(BigDecimal)retList[3])).toString() + "\"" 
	            			+",\"transId\":" + "\"" + (String)retList[4] + "\"" 
	            			+",\"domainName\":" + "\"" + (String)retList[5] + "\"" 
	               			+",\"dataFlowName\":" + "\"" + (String)retList[6] + "\"" 
	               			+",\"updatedDate\":" + "\"" + (retList[7]==null?"None":retList[7]) + "\"" 
	            			+"}");                  		
			}
			
			JSONArray jsonArray = JSONArray.fromObject(jsonList);           
			
			Map map = new HashMap();
			map.put("results", jsonArray);
			JSONObject jsonObject = JSONObject.fromObject(map);
			records = jsonObject.toString();
			records = records.substring(records.indexOf("{") + 1);
			records = "{\"total\":"+ documentMgr.getTotalRecords() +","+ records;			
		}else{
            jsonList.add("{\"gridId\":"
                    +",\"fileId\":" + "\"\"" 
                    +",\"fileName\":" + "\"\"" 
        			+",\"fileType\":" + "\"\"" 
        			+",\"fileSize\":" + "\"\"" 
        			+",\"transId\":" + "\"\"" 
        			+",\"domainName\":" + "\"\"" 
           			+",\"dataFlowName\":" + "\"\"" 
           			+",\"updatedDate\":" + "\"\"" 
        			+"}");                  		
			JSONArray jsonArray = JSONArray.fromObject(jsonList);           
			Map map = new HashMap();
			map.put("results", jsonArray);
			JSONObject jsonObject = JSONObject.fromObject(map);
			records = jsonObject.toString();
			records = records.substring(records.indexOf("{") + 1);

			records = "{\"total\":0"+","+ records;					
		}
		
		return records;
	}

	  /**
	   * changeOpDocumentSearchListToJsonString
	   * @param conditionName
	   * @param aList
	   * @return String
	   */
	private String changeOpDocumentSearchListToJsonString(String conditionName,List aList ){
		String records = "";
		Object[] retList;
		List jsonList = new ArrayList();
		String[] columnArr = conditionName.split(",");
		String json = "";
		
		if(aList!=null){
			for(int i=0;i<aList.size();i++){
	        	retList = (Object[])aList.get(i);
	        	json = "{\"gridId\":" + i
                +",\"fileId\":" + "\"" + ((BigDecimal)retList[0]).toString() + "\"" 
                +",\"fileName\":" + "\"" + (String)retList[1] + "\"" 
    			+",\"fileType\":" + "\"" + (String)retList[2] + "\"" 
    			+",\"fileSize\":" + "\"" + ((retList[3]==null? new BigDecimal(0):(BigDecimal)retList[3])).toString() + "\"" 
    			+",\"transId\":" + "\"" + (String)retList[4] + "\"" 
    			+",\"documentStatus\":" + "\"" + (String)retList[5] + "\"" 
    			+",\"submitStatus\":" + "\"" + (retList[retList.length-2]==null?" ": (String)retList[retList.length-2])+ "\""
	            +",\"submitStatusReport\":" + "\"" + (retList[retList.length-1]==null? " " : ((BigDecimal)retList[retList.length-1]).toString()) + "\""
    			+",\"domainName\":" + "\"" + (String)retList[6] + "\"" 
       			+",\"dataFlowName\":" + "\"" + (String)retList[7] + "\"" 
       			+",\"updatedDate\":" + "\"" + (retList[8]==null?"None":retList[8]) + "\"" ;
	        	
	        	if(conditionName != null && !conditionName.equals("")){
		            for(int j=0;j<columnArr.length;j++){
		            	json += ",\"" + columnArr[j] + "\":" + "\"" + (retList[9+j]==null?"":retList[9+j]) + "\"";
		            }	        		
	        	}
	        	
	        	json += "}";
	        	
	            jsonList.add(json);
			}
			
			JSONArray jsonArray = JSONArray.fromObject(jsonList);           
			
			Map map = new HashMap();
			map.put("results", jsonArray);
			JSONObject jsonObject = JSONObject.fromObject(map);
			records = jsonObject.toString();
			records = records.substring(records.indexOf("{") + 1);
			records = "{\"total\":"+ documentMgr.getTotalRecords() +","+ records;			
		}else{
            jsonList.add("{\"gridId\":"
                    +",\"fileId\":" + "\"\"" 
                    +",\"fileName\":" + "\"\"" 
        			+",\"fileType\":" + "\"\"" 
        			+",\"fileSize\":" + "\"\"" 
        			+",\"transId\":" + "\"\"" 
        			+",\"documentStatus\":" + "\"\"" 
        			+",\"domainName\":" + "\"\"" 
           			+",\"dataFlowName\":" + "\"\"" 
           			+",\"updatedDate\":" + "\"\"" 
        			+",\"submitStatus\":" + "\"\"" 
        			+",\"submitStatusReport\":" + "\"\""  
        			+"}");                  		
			JSONArray jsonArray = JSONArray.fromObject(jsonList);           
			Map map = new HashMap();
			map.put("results", jsonArray);
			JSONObject jsonObject = JSONObject.fromObject(map);
			records = jsonObject.toString();
			records = records.substring(records.indexOf("{") + 1);

			records = "{\"total\":0"+","+ records;					
		}
		
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
			sort = "fileId";
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
	   * getAttributeValue
	   * @param node
	   * @param xPath
	   * @return String
	   */
	private String getAttributeValue(Node node, String xPath) {
		String value = "";

		if (node == null || xPath == null) {
			return value;
		}

		Node attNode = node.selectSingleNode(xPath);
		if (attNode != null) {
			Attribute att = (Attribute) attNode;
			value = att.getValue();
		}

		return value;
	}

}
