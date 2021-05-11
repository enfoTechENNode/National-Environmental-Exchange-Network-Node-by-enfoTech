package Node2.web.Configurations;

import java.io.PrintWriter;
import java.text.SimpleDateFormat;
import java.util.Enumeration;
import java.util.HashMap;
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
import Node.Utils.Utility;
import Node2.model.Configurations.PageLayout;
import Node2.service.Configurations.PageLayoutManager;
import Node2.service.Domains.OperationLogManager;
/**
 * <p>This class create ConfigurationsAction.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class ConfigurationsAction extends DispatchAction {

	private String startIndex;
	private String recordsReturned;
	private String sort;
	private String dir;

	private static Log log = LogFactory.getLog(ConfigurationsAction.class);
	private PageLayoutManager pageLayoutMgr = null;
	private OperationLogManager operationLogMgr = null;
	SimpleDateFormat dateFormat = new SimpleDateFormat("MM/dd/yyyy HH:mm:ss");
	  /**
	   * setPageLayoutManager
	   * @param pageLayoutMgr
	   * @return 
	   */
	public void setPageLayoutManager(PageLayoutManager pageLayoutMgr) {
		this.pageLayoutMgr = pageLayoutMgr;
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
	   * getPlugInLayout
	   * @param mapping
	   * @param form
	   * @param request
	   * @param response
	   * @return ActionForward
	   */
	public ActionForward getPlugInLayout(ActionMapping mapping, ActionForm form,
			HttpServletRequest request, HttpServletResponse response)
			throws Exception {
		// get page metadata
		getPageMetaData(request);
		String ret = "";
		if (log.isDebugEnabled()) {
			log.debug("entering 'getPlugInLayout' method...");
		}
		HttpSession session = request.getSession();
	    User user = new User((String)session.getAttribute(Phrase.USER_SESSION),Phrase.AdministrationLoggerName);

	    boolean nodeAdmin = user.IsNodeAdmin();

	    if (nodeAdmin){
	    	ret = "ADMIN";
	    }else{
		  String[] userDomains = user.GetAssignedDomains();
		  if (userDomains != null){
		    for (int i = 0; i < userDomains.length; i++){
		      if (userDomains[i] != null && i == 0)
		    	  ret = userDomains[i];
		      else ret += "," + userDomains[i];
		    }
		  }
	    }
		printMsgToClient(ret, response);

		return null;
	}
	
	  /**
	   * getLayout
	   * @param mapping
	   * @param form
	   * @param request
	   * @param response
	   * @return ActionForward
	   */
	public ActionForward getLayout(ActionMapping mapping, ActionForm form,
			HttpServletRequest request, HttpServletResponse response)
			throws Exception {
		// get page metadata
		getPageMetaData(request);
		String ret = "{\"pageLayout\":[{\"para0\":\"[]\",\"para1\":\"[]\",\"para2\":\"[]\"}]}";
		if (log.isDebugEnabled()) {
			log.debug("entering 'getLayout' method...");
		}
	/*	List opList = mgr.getOperations( startIndex, recordsReturned,null,null,null,null,null);
		Map map = new HashMap();
		//map.put("records", opList);
		map.put("results", opList);
		JSONObject jsonObject = JSONObject.fromObject(map);

		String records = jsonObject.toString();
		records = records.substring(records.indexOf("{") + 1);*/
/*		records = "{\"recordsReturned\":"+ this.recordsReturned +","
		+ "\"totalRecords\":"+ mgr.getTotalRecords() +","
		+ "\"startIndex\":"+ this.startIndex +"," 
		+ "\"sort\":"+ (this.sort==null?null:"\""+this.sort+"\"") +","
		+ "\"dir\":\""+ this.dir +"\","
		+ records;
*/		//records = "{\"total\":"+ mgr.getTotalRecords() +","	+ records;
		// System.out.println(records);

		//printMsgToClient(records, response);
		HttpSession session = request.getSession();
	    User user = new User((String)session.getAttribute(Phrase.USER_SESSION),Phrase.AdministrationLoggerName);
		PageLayout pageLayout = pageLayoutMgr.getPageLayout(Integer.toString(user.GetUserID()));
		if(pageLayout!=null)
			ret = pageLayout.getColumns();
		
		printMsgToClient(ret, response);
		
		return null;
	}

	  /**
	   * resetLayout
	   * @param mapping
	   * @param form
	   * @param request
	   * @param response
	   * @return ActionForward
	   */
	public ActionForward resetLayout(ActionMapping mapping, ActionForm form,
			HttpServletRequest request, HttpServletResponse response)
			throws Exception {
		String ret = "{\"pageLayout\":[{\"para0\":\"[]\",\"para1\":\"[]\",\"para2\":\"[]\"}]}";
		boolean status;
		
		if (log.isDebugEnabled()) {
			log.debug("entering 'getLayout' method...");
		}
		HttpSession session = request.getSession();
	    User user = new User((String)session.getAttribute(Phrase.USER_SESSION),Phrase.AdministrationLoggerName);
	    status = pageLayoutMgr.deletePageLayout(Long.valueOf(Integer.toString(user.GetUserID())));
	    
	    if(status){
			printMsgToClient(ret, response);	    	
	    }else{
	    	ret = "fail";
			printMsgToClient(ret, response);	    		    	
	    }
		
		return null;
	}

	  /**
	   * setLayout
	   * @param mapping
	   * @param form
	   * @param request
	   * @param response
	   * @return ActionForward
	   */
	public ActionForward setLayout(ActionMapping mapping, ActionForm form,
			HttpServletRequest request, HttpServletResponse response)
			throws Exception {
		// get page metadata
		getPageMetaData(request);
		HttpSession session = request.getSession();
		PageLayout pageLayout = new PageLayout(),oldpageLayout=null;
	    User user = new User((String)session.getAttribute(Phrase.USER_SESSION),Phrase.AdministrationLoggerName);
		
		String columns = request.getParameter("columns");
		boolean status;
		
		pageLayout.setUserId(Long.valueOf(Integer.toString(user.GetUserID())));
		pageLayout.setColumns(columns);
		pageLayout.setUpdatedDate(Utility.GetNowTimeStamp());
		pageLayout.setUpdatedBy(user.GetLoginName());
		
		// check update or insert
		oldpageLayout = pageLayoutMgr.getPageLayout(Integer.toString(user.GetUserID()));
		if(oldpageLayout==null){
			pageLayout.setCreatedDate(Utility.GetNowTimeStamp());
			pageLayout.setCreatedBy(user.GetLoginName());
			status = pageLayoutMgr.setPageLayout(pageLayout,Phrase.INSERT);
		}else{
			pageLayout.setCreatedDate(oldpageLayout.getCreatedDate());
			pageLayout.setCreatedBy(oldpageLayout.getCreatedBy());			
			status = pageLayoutMgr.setPageLayout(pageLayout,Phrase.UPDATE);
		}
		if(status)	printMsgToClient("Configurations have been saved.", response);
		else printMsgToClient("Fail to save Configurations.", response);
		return null;
	}

	// WI 33641
	  /**
	   * handleRestServiceIntroduction
	   * @param mapping
	   * @param form
	   * @param request
	   * @param response
	   * @return ActionForward
	   */
	public ActionForward handleRestServiceIntroduction(ActionMapping mapping, ActionForm form,
			HttpServletRequest request, HttpServletResponse response)
			throws Exception {

		HttpSession session = request.getSession();
		User user= new User((String)session.getAttribute(Phrase.USER_SESSION),Phrase.AdministrationLoggerName);;
		ISystemConfiguration config = DBManager.GetSystemConfiguration(Phrase.AdministrationLoggerName);
	    String header = config.GetRestServiceIntroductionHeader();
	    String content = config.GetRestServiceIntroductionContent();
		String act = request.getParameter("act");
	    Map map = new HashMap(); 
	    if(user.IsNodeAdmin()){
		    if(!Utility.isNullOrEmpty(act) && act.equalsIgnoreCase("save")){
			    String[] introduction = new String[2];
			    
			    introduction[0] = request.getParameter("header");
			    introduction[1] = request.getParameter("content");
			    
			    if(config.SaveRestServiceIntroduction(introduction)){
				    map.put( "success", Boolean.TRUE );  
				    map.put( "msg", "Your changes have been successfully saved." );  
			    }else{
				    map.put( "success", Boolean.FALSE );  
				    map.put( "msg", "Failed to save changes due to system timeout. Please logout of the application, login back in, and try again." );  
			    }
		    }else{
			    map.put( "header", header );  
			    map.put( "content", content ); 
		    }
		    JSONObject jsonObject = JSONObject.fromObject( map ); 
		    printMsgToClient(jsonObject.toString(), response);	    	
	    }else{
            printMsgToClient("Only Administrator group users can change RESTful Web Services Configuration.", response);
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
