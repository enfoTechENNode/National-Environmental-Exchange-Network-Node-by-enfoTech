package Node2.web.Configurations;

import java.io.PrintWriter;
import java.math.BigDecimal;
import java.math.BigInteger;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.Hashtable;
import java.util.Iterator;
import java.util.List;
import java.util.Map;
import java.util.Vector;

import javax.servlet.ServletOutputStream;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.HttpSession;

import net.exchangenetwork.schema.ends.x2.NetworkNodesDocument;
import net.exchangenetwork.schema.ends.x2.ObjectPropertyType;
import net.exchangenetwork.schema.ends.x2.RequestParameterType;
import net.exchangenetwork.schema.ends.x2.ServiceDescriptionListType;
import net.exchangenetwork.schema.ends.x2.StyleSheetType;
import net.exchangenetwork.schema.ends.x2.ServiceDescriptionListType.Service;
import net.sf.json.JSONArray;
import net.sf.json.JSONObject;

import org.apache.log4j.Level;
import org.apache.struts.action.ActionForm;
import org.apache.struts.action.ActionForward;
import org.apache.struts.action.ActionMapping;
import org.apache.struts.upload.FormFile;
import org.apache.xmlbeans.XmlAnySimpleType;
import org.apache.xmlbeans.impl.values.XmlObjectBase;

import Node.Phrase;
import Node.Biz.Administration.Operation;
import Node.Biz.Administration.User;
import Node.DB.DBManager;
import Node.DB.Interfaces.INodeOperation;
import Node.DB.Interfaces.Configuration.IGetServices;
import Node.Web.Administration.BaseAction;
import Node2.web.Services.model.GetServicesDataBean;
import Node2.web.Services.model.GetServicesNodePropertyBean;
import Node2.web.Services.model.GetServicesNodeServicesBean;
import Node2.web.Services.model.GetServicesNodeServicesParametersBean;
import Node2.web.Services.model.GetServicesNodeServicesPropertyBean;
/**
 * <p>This class create GetServicesAction.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class DataWizardConfigAction extends BaseAction {
	  /**
	   * Constructor
	   * @param 
	   * @return 
	   */
	public DataWizardConfigAction() {
	}


	  /**
	   * formExecute
	   * @param mapping
	   * @param form
	   * @param request
	   * @param response
	   * @return ActionForward
	   */
	public ActionForward formExecute(ActionMapping mapping, ActionForm form,
			HttpServletRequest request, HttpServletResponse response)
			throws Exception {
		HttpSession session = request.getSession();
		DataWizardConfigBean bean = (DataWizardConfigBean)form;
		User user= new User((String)session.getAttribute(Phrase.USER_SESSION),Phrase.AdministrationLoggerName);;
	    boolean isSaved = false;
	    FormFile file = null;
		String act = request.getParameter("act");
		String ret = null;
		byte[] fileByte = null;
		String opID = request.getParameter("opID");
		Operation opObj = null;

	    bean.setMessage("");
        INodeOperation op = DBManager.GetNodeOperation(Phrase.AdministrationLoggerName);
        if(user.IsNodeAdmin()){
			fillBean(bean,op);
    		if(act!=null && act.equalsIgnoreCase("upload")){
    			return mapping.findForward("upload");			
    		}else if(act!=null && act.equalsIgnoreCase("saveUpload")){
    	        file = bean.getUploadFile();
    	        if (file != null && !file.getFileName().trim().equals("")){
    	        	fileByte = file.getFileData();
    	        	opObj = op.GetOperation(Integer.parseInt(opID));
    	        	opObj.SetConfig(new String(fileByte));
    	        	isSaved = opObj.Save(Phrase.AdministrationLoggerName);
    		        if (!isSaved) {
    		        	this.Log("DataWizardConfigAction.do>>> Unable to Save Operation Configuration file",Level.FATAL);
    		        	bean.setMessage("Fail to upload file.");
    		        }
    		        else bean.setMessage("Upload file successfully.");
    		        
    	        	return mapping.findForward("upload");				
    	        }
    		}else if(act!=null && act.equalsIgnoreCase("download")){
    			response.setContentType("text/xml");
    			response.addHeader("Content-Disposition","attachment;filename=DataWizardConfig.xml");
    			ServletOutputStream out = response.getOutputStream();
    			fileByte = op.GetOperationConfig(Integer.parseInt(opID)).getBytes();
    			if (fileByte != null)
    				for (int i = 0; i < fileByte.length; i++)
    					out.write(fileByte[i]);
    			out.close();
    			ret="";			
    		}
        }else{
        	ret = "Only Administrator group users can change Data Wizard Configuration. ";
        }

        printMsgToClient(ret, response);

		return null;
	}

	  /**
	   * printMsgToClient
	   * @param result
	   * @param response
	   * @return 
	   */
	private static void printMsgToClient(String result,
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
	   * fillBean
	   * @param bean
	   * @param op
	   * @return 
	   */
	private void fillBean(DataWizardConfigBean bean,INodeOperation op) throws Exception {
		Hashtable assignedOpIDArr = null;
		String[] opIDs = null;
		String[] opNames = null;
		int i=0;
		
		assignedOpIDArr = op.GetAllDataWizardOperationsIDList();
		if(assignedOpIDArr != null){
			opIDs = new String[assignedOpIDArr.size()];
			opNames = new String[assignedOpIDArr.size()];
			Iterator it = assignedOpIDArr.keySet().iterator();
			while(it.hasNext()){
				opIDs[i] = (String)it.next();
				opNames[i] = (String)assignedOpIDArr.get(opIDs[i]);				
				i++;
			}
			bean.setOpIDs(opIDs);
			bean.setOpNames(opNames);
		}
		
	}

}
