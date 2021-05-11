package Node2.web.OperationMgr;

import java.io.ByteArrayInputStream;
import java.io.File;
import java.io.FileInputStream;
import java.io.InputStream;
import java.io.PrintWriter;
import java.net.URL;
import java.rmi.RemoteException;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.Iterator;
import java.util.List;
import java.util.Map;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.HttpSession;

import net.sf.json.JSONArray;
import net.sf.json.JSONObject;

import org.apache.log4j.Level;
import org.apache.struts.action.ActionForm;
import org.apache.struts.action.ActionForward;
import org.apache.struts.action.ActionMapping;
import org.apache.struts.upload.FormFile;
import org.dom4j.Attribute;
import org.dom4j.Element;
import org.dom4j.Node;
import org.dom4j.io.SAXReader;

import Node.Phrase;
import Node.API.NodeUtils;
import Node.API.Stylizer;
import Node.API.ValidationManager;
import Node.API.XML;
import Node.Biz.Administration.Document;
import Node.Biz.Administration.Operation;
import Node.Biz.Administration.OperationManager;
import Node.Biz.Administration.User;
import Node.DB.DBManager;
import Node.DB.Interfaces.INodeDomain;
import Node.DB.Interfaces.INodeFileCabin;
import Node.DB.Interfaces.INodeOperation;
import Node.DB.Interfaces.INodeOperationParameter;
import Node.DB.Interfaces.Configuration.IConfigurationMgr;
import Node.DB.Interfaces.Configuration.IOperationMgr;
import Node.DB.Interfaces.Configuration.ISystemConfiguration;
import Node.Utils.AppUtils;
import Node.Utils.Utility;
import Node.Web.Administration.BaseAction;
import Node.WebServices.Document.ClsNodeDocument;
import Node.WebServices.Requestor.NodeRequestor;
import Node2.web.OperationMgr.model.OperationMgrBean;
import Node2.web.OperationMgr.model.OperationMgrGenerateParameterBean;
import Node2.web.OperationMgr.model.OperationMgrParameterBean;
import Node2.web.OperationMgr.model.OperationMgrSubmissionBean;
import Node2.web.OperationMgr.model.OperationMgrTemplateBean;
import Node2.web.OperationMgr.model.OperationMgrValidateBean;

import com.enfotech.basecomponent.typelib.xml.XmlDocument;
import com.enfotech.basecomponent.utility.security.Cryptography;
/**
 * <p>This class create OperationMgrAction.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class OperationMgrAction extends BaseAction {
	private boolean debug = false;
	  /**
	   * Constructor
	   * @param 
	   * @return 
	   */
	public OperationMgrAction() {
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
		OperationMgrBean bean = (OperationMgrBean)form;
		Operation[] opList = null;
		User user= new User((String)session.getAttribute(Phrase.USER_SESSION),Phrase.AdministrationLoggerName);;
	    boolean isSaved = false;
	    FormFile file = null;
		String operationId = "";
		String operationName = "";
		String transId = "";
		String validationFileId = "";
		String submissionFileId = "";
		String act = request.getParameter("act");
		String ret = "{\"total\":0,\"results\":[]}";
		String inputJson = null;
		byte[] fileByte = null;
		org.dom4j.Document doc4jVR = null;
		String path = Utility.GetTempFilePath() + "/temp/";
		String fileID = null;
		String submitId = null;
		
	    //bean.setMessage("");
		try {
	        IOperationMgr operationMgr = DBManager.getOperationMgr(Phrase.AdministrationLoggerName);
	        INodeOperationParameter paramDB = DBManager.GetNodeOperationParameter(Phrase.AdministrationLoggerName);
	        IConfigurationMgr configMgr = DBManager.GetConfigurationMgr(Phrase.AdministrationLoggerName);
	        INodeFileCabin documentMgr = DBManager.GetNodeFileCabin(Phrase.AdministrationLoggerName);
	        	
			if(act!=null && act.equalsIgnoreCase("getOperationList")){
				fileByte = operationMgr.GetOperationListFile();
				if (fileByte != null){
				    SAXReader reader = new SAXReader();
				    doc4jVR = reader.read(new ByteArrayInputStream(fileByte));
				    // get operationId and Name
				    List operationMgrBeanList = doc4jVR.selectNodes(".//OperationList/Operation");
	
				    if (operationMgrBeanList != null && operationMgrBeanList.size() > 0) {
				    	ret = changeOperationListToJsonString(operationMgrBeanList);
				    }
				}
			}else if(act!=null && act.equalsIgnoreCase("checkGetStatusAvalible")){
				operationId = request.getParameter("operationId").trim();
				Operation op = new Operation(Integer.parseInt(operationId),Phrase.AdministrationLoggerName);
				if (op != null){
				    SAXReader reader = new SAXReader();
				    String config = op.GetConfig();
				    if(config!=null){
				        XmlDocument doc = new XmlDocument();
				        doc.LoadXml(op.GetConfig());		
					    if (doc.DocumentElement().Name().toLowerCase().trim().equals("process")) {
							ret = "true";
					    }else ret = "false";				    				    	
				    }
				}
			}else if(act!=null && act.equalsIgnoreCase("getGrantOperationList")){
				fileByte = operationMgr.GetOperationListFile();
				// WI 22317
		        opList = Operation.GetAllOperationsList(Phrase.AdministrationLoggerName,user);
		        List grantOperationMgrBeanList = new ArrayList();
				if (fileByte != null){
				    SAXReader reader = new SAXReader();
				    doc4jVR = reader.read(new ByteArrayInputStream(fileByte));
				    // get operationId and Name
				    List operationMgrBeanList = doc4jVR.selectNodes(".//OperationList/Operation");
	
				    if (operationMgrBeanList != null && operationMgrBeanList.size() > 0) {
				    	for(Iterator i = operationMgrBeanList.iterator(); i.hasNext();){
				    		Node operationMgrBean = (Node)i.next();
				    		for(int j=0;j<opList.length;j++){
					    		if(this.getAttributeValue(operationMgrBean,"@id").trim().equalsIgnoreCase(Integer.toString(((Operation)opList[j]).GetOperationID()))){			    			
					    			grantOperationMgrBeanList.add(operationMgrBean);
					    		}			    			
				    		}
				    	}
					    if (!grantOperationMgrBeanList.isEmpty()) {
							// WI 22065
					    	grantOperationMgrBeanList = Utility.sortOperationNode(grantOperationMgrBeanList);
					    	ret = changeOperationListToJsonString(grantOperationMgrBeanList);
					    }
				    }
				}
			}else if(act!=null && act.equalsIgnoreCase("getOperationTemplateList")){
				fileByte = operationMgr.GetOperationListFile();
				if (fileByte != null){
					List operationMgrTemplateBeanList = null;
				    SAXReader reader = new SAXReader();
				    doc4jVR = reader.read(new ByteArrayInputStream(fileByte));
				    // get operationId and Name
				    List operationMgrBeanList = doc4jVR.selectNodes(".//OperationList/Operation");
	
				    if (operationMgrBeanList != null && operationMgrBeanList.size() > 0) {
				    	for(int i=0;i<operationMgrBeanList.size();i++){
				    		if(this.getAttributeValue((Node) operationMgrBeanList.get(i),"@id").trim().equalsIgnoreCase(request.getParameter("operationId").trim())){			    			
				    			List viewList = ((Node) operationMgrBeanList.get(i)).selectNodes("View");
				    			operationMgrTemplateBeanList = ((Node)viewList.get(0)).selectNodes("Template");
				    			break;
				    		}
				    	}
					    if (operationMgrTemplateBeanList != null && operationMgrTemplateBeanList.size() > 0) {
					    	ret = changeOperationListTemplateToJsonString(operationMgrTemplateBeanList);
					    }
				    }
				}
			}else if(act!=null && act.equalsIgnoreCase("getOperationValidateList")){
				operationId =  request.getParameter("operationId").trim();
				fileByte = operationMgr.GetOperationListFile();
				if (fileByte != null){
					List operationMgrValidateBeanList = null;
				    SAXReader reader = new SAXReader();
				    doc4jVR = reader.read(new ByteArrayInputStream(fileByte));
				    // get operationId and Name
				    List operationMgrBeanList = doc4jVR.selectNodes(".//OperationList/Operation");
	
				    if (operationMgrBeanList != null && operationMgrBeanList.size() > 0) {
				    	for(int i=0;i<operationMgrBeanList.size();i++){
				    		if(this.getAttributeValue((Node) operationMgrBeanList.get(i),"@id").trim().equalsIgnoreCase(operationId)){			    			
				    			List valdiateList = ((Node) operationMgrBeanList.get(i)).selectNodes("Validate");
				    			operationMgrValidateBeanList = ((Node)valdiateList.get(0)).selectNodes("File");
				    			break;
				    		}
				    	}
					    if (operationMgrValidateBeanList != null && operationMgrValidateBeanList.size() > 0) {
					    	ret = changeOperationListValidateToJsonString(operationMgrValidateBeanList);
					    }
				    }
				}
			}else if(act!=null && act.equalsIgnoreCase("getOperationParameterList")){
				fileByte = operationMgr.GetOperationListFile();
				if (fileByte != null){
					List operationMgrParameterList = null;
				    SAXReader reader = new SAXReader();
				    doc4jVR = reader.read(new ByteArrayInputStream(fileByte));
				    Element rootElm = doc4jVR.getRootElement();
				    // get operationId and Name
				    List operationList = rootElm.elements("Operation");
	
				    if (operationList != null && operationList.size() > 0) {
			    		for(Iterator i = operationList.iterator(); i.hasNext(); ){
			    			Element operation = (Element) i.next();
			    			// get parameter
					    	if(operation.attribute("id").getText().trim().equalsIgnoreCase(request.getParameter("operationId").trim())){			    			
						    	Element upload = operation.element("Upload");
						    	Element parameters = upload.element("Parameters");
						    	operationMgrParameterList = parameters.elements("Parameter");
						    	
						    	break;								    	
				    		}
				    	}
					    if (operationMgrParameterList != null && operationMgrParameterList.size() > 0) {
					    	ret = changeOperationListParameterToJsonString(operationMgrParameterList);
					    }
				    }
				}
			}else if(act!=null && act.equalsIgnoreCase("getOperationWebServiceParameterList")){
				operationId =  request.getParameter("operationId").trim();
				if (operationId != null){
					Operation op = new Operation(Integer.parseInt(operationId),Phrase.AdministrationLoggerName);
					ArrayList webServiceParamList = op.GetWebServiceParameterNames();
					if(webServiceParamList!=null && !webServiceParamList.isEmpty()){
						ret = changeOperationWebServiceParameterListToJsonString(webServiceParamList);
					}
				}
			}else if(act!=null && act.equalsIgnoreCase("getFilterConditionNameList")){
				operationId = request.getParameter("operationId").trim();
				// Get history filter parameters
				List nameList = paramDB.GetParameterNames(Integer.parseInt(operationId));
				// Get new created filter parameters
				List operationMgrParameterList = null;
				fileByte = operationMgr.GetOperationListFile();
				if (fileByte != null){
				    SAXReader reader = new SAXReader();
				    doc4jVR = reader.read(new ByteArrayInputStream(fileByte));
				    Element rootElm = doc4jVR.getRootElement();
				    // get operationId and Name
				    List operationList = rootElm.elements("Operation");
	
				    if (operationList != null && operationList.size() > 0) {
			    		for(Iterator i = operationList.iterator(); i.hasNext(); ){
			    			Element operation = (Element) i.next();
			    			// get parameter
					    	if(operation.attribute("id").getText().trim().equalsIgnoreCase(request.getParameter("operationId").trim())){			    			
						    	Element upload = operation.element("Upload");
						    	Element parameters = upload.element("Parameters");
						    	operationMgrParameterList = parameters.elements("Parameter");
						    	
						    	break;								    	
				    		}
				    	}
				    }
				}
				List newParaList = new ArrayList();
			    if (operationMgrParameterList != null && operationMgrParameterList.size() > 0) {
			    	boolean hasPara = false;
			    	for(Iterator i = operationMgrParameterList.iterator(); i.hasNext(); ){
			    		Element parameter = (Element)i.next();
			    		hasPara = false;
				    	for(Iterator j = nameList.iterator(); j.hasNext(); ){
				    		String paraName = (String)j.next();
				    		if(parameter.attribute("name").getText().trim().equalsIgnoreCase(paraName)){
				    			hasPara = true;
				    			break;
				    		}
				    	}
				    	if(!hasPara){
				    		newParaList.add(parameter.attribute("name").getText().trim());
				    	}
			    	}
			    }
			    for(Iterator k = newParaList.iterator(); k.hasNext(); ){
			    	nameList.add(k.next());
		    	}
				if (nameList != null && nameList.size() > 0) {
					ret = changeConditionNameListToJsonString(nameList);
				}			
			}else if(act!=null && act.equalsIgnoreCase("getOperationManagerTableList")){
				transId =  request.getParameter("transId").trim();
				if (transId != null){
					ArrayList managerTableList = operationMgr.GetOperationsManagerTableList(transId);
					if(managerTableList!=null && !managerTableList.isEmpty()){
						ret = changeOperationManagerTableListToJsonString(managerTableList);
					}
				}
			}else if(act!=null && act.equalsIgnoreCase("addOperationList")){
				fileByte = operationMgr.GetOperationListFile();
				if (fileByte != null){
					List operationMgrTemplateBeanList = null;
				    SAXReader reader = new SAXReader();
				    doc4jVR = reader.read(new ByteArrayInputStream(fileByte));
				    // Create a new operation element
	
				    if (doc4jVR != null) {
				    	Element root = doc4jVR.getRootElement();
				    	Element newOperation = root.addElement("Operation")
				    							.addAttribute("id", "")
				    							.addAttribute("name", "")
				    							.addAttribute("version", "");
				    	Element newDataFlow = newOperation.addElement("DataFlow");
				    	Element newUpload = newOperation.addElement("Upload")
											.addAttribute("Enable", "");
				    	Element newParameters = newUpload.addElement("Parameters");
				    	Element newGenerate = newOperation.addElement("Generate")
											.addAttribute("Enable", "");
				    	Element newSubmit = newOperation.addElement("Submit")
											.addAttribute("Enable", "");
				    						newSubmit.addElement("URL");
				    						newSubmit.addElement("UserName");
				    						newSubmit.addElement("Password");
				    						newSubmit.addElement("DomainName");
				    	Element newView = newOperation.addElement("View")
											.addAttribute("Enable", "");
//				    	Element newTemplate = newView.addElement("Template")
//				    						.addAttribute("id","");
//				    	
				    	Element newValidate = newOperation.addElement("Validate");
//				    	Element newFile = newValidate.addElement("File")
//				    						.addAttribute("id","");
				    	Element getStatus = newOperation.addElement("GetStatus")
											.addAttribute("Enable", "");
											getStatus.addElement("Complete");
											getStatus.addElement("Error");
					    	
				    	fileByte = doc4jVR.asXML().getBytes(Phrase.UTF_8);
				    }
				    operationMgr.SaveOperationListFile(fileByte);
				}			
			}else if(act!=null && act.equalsIgnoreCase("addParameterList")){
				fileByte = operationMgr.GetOperationListFile();
				if (fileByte != null && request.getParameter("operationId") != null && !request.getParameter("operationId").equals("")){
					operationId = request.getParameter("operationId").trim();
					if (fileByte != null){
					    SAXReader reader = new SAXReader();
					    doc4jVR = reader.read(new ByteArrayInputStream(fileByte));
					    Element rootElm = doc4jVR.getRootElement();
					    // get operationId and Name
					    List operationList = rootElm.elements("Operation");
			    		for(Iterator i = operationList.iterator(); i.hasNext(); ){
			    			Element operation = (Element) i.next();
			    			// add new element
					    	if(operation.attribute("id").getText().trim().equalsIgnoreCase(operationId)){			    			
						    	Element upload = operation.element("Upload");				    		
						    	Element parameters = upload.element("Parameters");
						    	List paraList = parameters.elements("Parameter");
						    	int maxCount = 0;
						    	for(Iterator it=paraList.iterator();it.hasNext();){
						    		Element para = (Element)it.next();
						    		String counter = para.attributeValue("id").trim();
						    		if(maxCount < Integer.parseInt(counter)){
						    			maxCount = Integer.parseInt(counter);
						    		}
						    	}
						    	Element newPara = parameters.addElement("Parameter");
						    	newPara.addAttribute("id", Integer.toString(maxCount+1));
						    	newPara.addAttribute("name", "");
						    	break;
					    	}
					    }
					}
					String filest = doc4jVR.asXML();
			    	fileByte = filest.getBytes(Phrase.UTF_8);
				    operationMgr.SaveOperationListFile(fileByte);
				}			
			}else if(act!=null && act.equalsIgnoreCase("deleteOperationList")){
				if(request.getParameter("dataString")!=null){
					inputJson = request.getParameter("dataString");
					
					JSONObject jsonObject = JSONObject.fromObject(inputJson);   
					JSONArray operationListJson = jsonObject.getJSONArray("operationList");
					OperationMgrBean[] operationMgrBeanInputArr = null;
	
					if(operationListJson!=null){
						Object[] operationListJsonList = operationListJson.toArray();				
						operationMgrBeanInputArr = new OperationMgrBean[operationListJsonList.length];
						for(int i=0;i<operationMgrBeanInputArr.length;i++){
							operationMgrBeanInputArr[i] = (OperationMgrBean)JSONObject.toBean((JSONObject)operationListJsonList[i], OperationMgrBean.class);
						}
					}
	
					fileByte = operationMgr.GetOperationListFile();
					if (fileByte != null){
					    SAXReader reader = new SAXReader();
					    doc4jVR = reader.read(new ByteArrayInputStream(fileByte));
					    // Create a new operation element
	
					    // get operationId and Name
					    List operationMgrBeanList = null;
	
				    	for(int i=0;i<operationMgrBeanInputArr.length;i++){
				    		operationMgrBeanList = doc4jVR.selectNodes(".//OperationList/Operation");
				    		for(Iterator j = operationMgrBeanList.iterator(); j.hasNext(); ){
				    			Node myNode = (Node)j.next();
						    	if(this.getAttributeValue(myNode,"@id").trim().equalsIgnoreCase(operationMgrBeanInputArr[i].getOperationId())){			    			
						    		myNode.detach();
					    			break;
						    	}
						    }				    		
				    	}
					}
					String filest = doc4jVR.asXML();
			    	fileByte = filest.getBytes(Phrase.UTF_8);
				    operationMgr.SaveOperationListFile(fileByte);
				}
			}else if(act!=null && act.equalsIgnoreCase("updateOperationList")){
				if(request.getParameter("dataString")!=null){
					inputJson = request.getParameter("dataString");
					
					// Transform json to array
					JSONObject jsonObject = JSONObject.fromObject(inputJson);   
					JSONArray operationListJson = jsonObject.getJSONArray("operationList");
					OperationMgrBean[] operationMgrBeanInputArr = null;
	
					if(operationListJson!=null){
						Object[] operationListJsonList = operationListJson.toArray();				
						operationMgrBeanInputArr = new OperationMgrBean[operationListJsonList.length];
						for(int i=0;i<operationMgrBeanInputArr.length;i++){
							operationMgrBeanInputArr[i] = (OperationMgrBean)JSONObject.toBean((JSONObject)operationListJsonList[i], OperationMgrBean.class);
						}
					}
					// update file
					fileByte = operationMgr.GetOperationListFile();
					if (fileByte != null){
					    SAXReader reader = new SAXReader();
					    doc4jVR = reader.read(new ByteArrayInputStream(fileByte));
					    Element rootElm = doc4jVR.getRootElement();
					    // get operationId and Name
					    List operationMgrBeanList = rootElm.elements("Operation");
					    boolean isChange = false;
				    	for(int i=0;i<operationMgrBeanInputArr.length;i++){
				    		isChange = false;
				    		for(Iterator j = operationMgrBeanList.iterator(); j.hasNext(); ){
				    			Element operation = (Element) j.next();
				    			// change existing element
						    	if(operation.attribute("id").getText().trim().equalsIgnoreCase(operationMgrBeanInputArr[i].getOperationId())){			    			
	
						    		operation.attribute("version").setText(operationMgrBeanInputArr[i].getOperationVersion());
						    		
						    		Element newDataFlow = operation.element("DataFlow");
							    	newDataFlow.setText(operationMgrBeanInputArr[i].getDataFlow());
							    	
							    	Element newUpload = operation.element("Upload");
							    	newUpload.attribute("Enable").setText(operationMgrBeanInputArr[i].getUpload());
							    	
							    	Element newGenerate = operation.element("Generate");
							    	newGenerate.attribute("Enable").setText(operationMgrBeanInputArr[i].getGenerate());
	
							    	Element newSubmit = operation.element("Submit");
							    	newSubmit.attribute("Enable").setText(operationMgrBeanInputArr[i].getSubmit());
							    	Element newURL = newSubmit.element("URL");
							    	newURL.setText(operationMgrBeanInputArr[i].getURL());
							    	Element newUserName = newSubmit.element("UserName");
							    	newUserName.setText(operationMgrBeanInputArr[i].getUserName());
							    	Element newPassword = newSubmit.element("Password");
							    	Cryptography crypt = new Cryptography();
							    	newPassword.setText(crypt.Encrypting(operationMgrBeanInputArr[i].getPassword(),Phrase.CryptKey));
							    	Element newDomainName = newSubmit.element("DomainName");
							    	if(newDomainName==null){
							    		newDomainName = newSubmit.addElement("DomainName");
							    	}
							    	newDomainName.setText(operationMgrBeanInputArr[i].getDomainName());
	
							    	Element newView = operation.element("View");
							    	newView.attribute("Enable").setText(operationMgrBeanInputArr[i].getView());
							    	
							    	Element newGetStatus = operation.element("GetStatus");
							    	Element newComplete = null;
							    	Element newError = null;
							    	if(newGetStatus==null){
							    		newGetStatus = operation.addElement("GetStatus");
							    		newGetStatus.addAttribute("Enable", "");
							    		newComplete = newGetStatus.addElement("Complete");
							    		newError = newGetStatus.addElement("Error");
							    	}
							    	newGetStatus.attribute("Enable").setText(operationMgrBeanInputArr[i].getGetStatus());
							    	newComplete = newGetStatus.element("Complete");
							    	newError = newGetStatus.element("Error");
							    	newComplete.setText(operationMgrBeanInputArr[i].getGetStatusComplete());
							    	newError.setText(operationMgrBeanInputArr[i].getGetStatusError());

							    	isChange = true;
							    	break;
						    	}
						    }
				    		if(!isChange){	// add new element
					    		for(Iterator j = operationMgrBeanList.iterator(); j.hasNext(); ){
					    			Element operation = (Element) j.next();
					    			// change existing element
							    	if(operation.attribute("id").getText().trim() == null || operation.attribute("id").getText().trim().equalsIgnoreCase("")){			    			
	
							    		operation.attribute("id").setText(operationMgrBeanInputArr[i].getOperationId());
							    		operation.attribute("name").setText(operationMgrBeanInputArr[i].getOperationName());
							    		operation.attribute("version").setText(operationMgrBeanInputArr[i].getOperationVersion());
							    		
							    		Element newDataFlow = operation.element("DataFlow");
								    	newDataFlow.setText(operationMgrBeanInputArr[i].getDataFlow());
								    	
								    	Element newUpload = operation.element("Upload");
								    	newUpload.attribute("Enable").setText(operationMgrBeanInputArr[i].getUpload());
								    	
								    	Element newGenerate = operation.element("Generate");
								    	newGenerate.attribute("Enable").setText(operationMgrBeanInputArr[i].getGenerate());
	
								    	Element newSubmit = operation.element("Submit");
								    	newSubmit.attribute("Enable").setText(operationMgrBeanInputArr[i].getSubmit());
								    	Element newURL = newSubmit.element("URL");
								    	newURL.setText(operationMgrBeanInputArr[i].getURL());
								    	Element newUserName = newSubmit.element("UserName");
								    	newUserName.setText(operationMgrBeanInputArr[i].getUserName());
								    	Element newPassword = newSubmit.element("Password");
								    	Cryptography crypt = new Cryptography();
								    	newPassword.setText(crypt.Encrypting(operationMgrBeanInputArr[i].getPassword(),Phrase.CryptKey));
	
								    	Element newView = operation.element("View");
								    	newView.attribute("Enable").setText(operationMgrBeanInputArr[i].getView());

								    	isChange = true;
								    	break;
							    	}
							    }			    			
				    		}
				    	}
					}
					String filest = doc4jVR.asXML();
			    	fileByte = filest.getBytes(Phrase.UTF_8);
				    operationMgr.SaveOperationListFile(fileByte);
				}
			}else if(act!=null && act.equalsIgnoreCase("updateParameterList")){
				if(request.getParameter("dataString")!=null && request.getParameter("operationId")!=null){
					inputJson = request.getParameter("dataString");
					
					// Transform json to array
					JSONObject jsonObject = JSONObject.fromObject(inputJson);   
					JSONArray parameterListJson = jsonObject.getJSONArray("parameterList");
					OperationMgrParameterBean[] operationMgrParameterBeanInputArr = null;
	
					if(parameterListJson!=null){
						Object[] parameterListJsonList = parameterListJson.toArray();				
						operationMgrParameterBeanInputArr = new OperationMgrParameterBean[parameterListJsonList.length];
						for(int i=0;i<operationMgrParameterBeanInputArr.length;i++){
							operationMgrParameterBeanInputArr[i] = (OperationMgrParameterBean)JSONObject.toBean((JSONObject)parameterListJsonList[i], OperationMgrParameterBean.class);
						}
					}
					// update file
					fileByte = operationMgr.GetOperationListFile();
					if (fileByte != null){
					    SAXReader reader = new SAXReader();
					    doc4jVR = reader.read(new ByteArrayInputStream(fileByte));
					    Element rootElm = doc4jVR.getRootElement();
					    // get operationId and Name
					    List operationList = rootElm.elements("Operation");
				    	for(int i=0;i<operationMgrParameterBeanInputArr.length;i++){
				    		for(Iterator j = operationList.iterator(); j.hasNext(); ){
				    			Element operation = (Element) j.next();
				    			// change existing element
						    	if(operation.attribute("id").getText().trim().equalsIgnoreCase(request.getParameter("operationId").trim())){			    			
	
							    	Element upload = operation.element("Upload");				    		
							    	Element parameters = upload.element("Parameters");
							    	List paraList = parameters.elements("Parameter");
							    	
							    	for(Iterator k = paraList.iterator(); k.hasNext(); ){
						    			Element para = (Element) k.next();
							    		
								    	if(para.attribute("id").getText().trim().equalsIgnoreCase(operationMgrParameterBeanInputArr[i].getParameterId())){
									    	para.attribute("name").setText(operationMgrParameterBeanInputArr[i].getParameterName());							    		
									    	para.setText(operationMgrParameterBeanInputArr[i].getParameterValue());
								    	}						    		
							    	}
							    	break;
						    	}
						    }
				    	}
					}
					String filest = doc4jVR.asXML();
			    	fileByte = filest.getBytes(Phrase.UTF_8);
				    operationMgr.SaveOperationListFile(fileByte);
				}
			}else if(act!=null && act.equalsIgnoreCase("updateTransformSuffix")){
				if(request.getParameter("dataString")!=null && request.getParameter("operationId")!=null){
					operationId = request.getParameter("operationId");
					inputJson = request.getParameter("dataString");
					
					// Transform json to array
					JSONObject jsonObject = JSONObject.fromObject(inputJson);   
					JSONArray transformSuffixListJson = jsonObject.getJSONArray("transformSuffixList");
					OperationMgrTemplateBean[] operationMgrTemplateBeanInputArr = null;
	
					if(transformSuffixListJson!=null){
						Object[] transformSuffixJsonList = transformSuffixListJson.toArray();				
						operationMgrTemplateBeanInputArr = new OperationMgrTemplateBean[transformSuffixJsonList.length];
						for(int i=0;i<operationMgrTemplateBeanInputArr.length;i++){
							operationMgrTemplateBeanInputArr[i] = (OperationMgrTemplateBean)JSONObject.toBean((JSONObject)transformSuffixJsonList[i], OperationMgrTemplateBean.class);
						}
					}
	
					fileByte = operationMgr.GetOperationListFile();
					if (fileByte != null){
					    SAXReader reader = new SAXReader();
					    doc4jVR = reader.read(new ByteArrayInputStream(fileByte));
					    // get operationId and Name
					    Element rootElm = doc4jVR.getRootElement();
					    List operationMgrBeanList = rootElm.elements("Operation");
					    
				    	for(int i=0;i<operationMgrTemplateBeanInputArr.length;i++){
				    		for(Iterator j = operationMgrBeanList.iterator(); j.hasNext(); ){
				    			Element operation = (Element) j.next();
						    	if(operation.attribute("id").getText().trim().equalsIgnoreCase(operationId)){			    			
							    	Element myView = operation.element("View");
								    List myTemplateList = myView.elements("Template");
						    		for(Iterator k = myTemplateList.iterator(); k.hasNext(); ){
						    			Element myTemplate = (Element) k.next();
						    			if(myTemplate.attribute("id").getText().trim().equalsIgnoreCase(operationMgrTemplateBeanInputArr[i].getTemplateId())){
						    				myTemplate.attribute("transformSuffix").setText(operationMgrTemplateBeanInputArr[i].getTransformSuffix());							    		
						    			}
						    		}
						    	}
						    }				    		
				    	}
					}
					String filest = doc4jVR.asXML();
			    	fileByte = filest.getBytes(Phrase.UTF_8);
				    operationMgr.SaveOperationListFile(fileByte);
				}
			}else if(act!=null && act.equalsIgnoreCase("uploadOperationFile")){
				bean.clean();
				return mapping.findForward("uploadOperationFile");			
			}else if(act!=null && act.equalsIgnoreCase("uploadTemplateFile")){
				operationId = request.getParameter("id");
				bean.clean();
				bean.setOperationId(operationId);
				return mapping.findForward("uploadTemplateFile");			
			}else if(act!=null && act.equalsIgnoreCase("uploadValidationFile")){
				operationId = request.getParameter("id");
				bean.clean();
				bean.setOperationId(operationId);
				return mapping.findForward("uploadValidationFile");			
			}else if(act!=null && act.equalsIgnoreCase("updateOperationListFile")){
		        file = bean.getUploadFile();
		        if (file != null && !file.getFileName().trim().equals("")){
			    	fileByte = file.getFileData();
			    	isSaved = operationMgr.SaveOperationListFile(fileByte);
			        if (isSaved) {
			        	bean.setMessage("Upload " + file.getFileName() +" successfully.");
			        }
			        else{
			        	this.Log("OperationMgr.do: Unable to Save " + file.getFileName() + " file.",Level.FATAL);
			        	bean.setMessage("Fail to upload " + file.getFileName() + " file.");
			        }		        
		        	return mapping.findForward("uploadOperationFile");				
		        }
			}else if(act!=null && act.equalsIgnoreCase("updateTemplateList")){
		        file = bean.getUploadFile();
		        if (file != null && !file.getFileName().trim().equals("")){
					isSaved = operationMgr.SaveSubListFile(file);
			        if (isSaved) {
			        	// update operation list file
			        	String templateId = operationMgr.GetOperationSubFileID(file.getFileName());
			        	
						// update file
						fileByte = operationMgr.GetOperationListFile();
						if (fileByte != null){
						    SAXReader reader = new SAXReader();
						    doc4jVR = reader.read(new ByteArrayInputStream(fileByte));
						    Element rootElm = doc4jVR.getRootElement();
						    // get operationId and Name
						    List operationMgrBeanList = rootElm.elements("Operation");
						    boolean isChange = false;
				    		for(Iterator j = operationMgrBeanList.iterator(); j.hasNext(); ){
				    			Element operation = (Element) j.next();
				    			// change existing element
						    	if(operation.attribute("id").getText().trim().equalsIgnoreCase(bean.getOperationId())){			    			
						    		
							    	Element newView = operation.element("View");
							    	List templateList = newView.elements("Template");
							    	for(Iterator k = templateList.iterator(); k.hasNext(); ){
							    		Element template = (Element) k.next();
							    		if(template.attribute("id").getText().trim().equalsIgnoreCase(templateId)){	
							    			template.setText(file.getFileName());
									    	isChange = true;
							    		}
							    	}
							    	break;
						    	}
						    }
				    		if(!isChange){	// add new element
					    		for(Iterator j = operationMgrBeanList.iterator(); j.hasNext(); ){
					    			Element operation = (Element) j.next();
					    			// change existing element
							    	if(operation.attribute("id").getText().trim().equalsIgnoreCase(bean.getOperationId())){			    			
	
								    	Element newView = operation.element("View");
								    	Element newTemplate = newView.addElement("Template")
								    							.addAttribute("id", templateId)
		    													.addAttribute("transformSuffix", "");
								    	newTemplate.setText(file.getFileName());
								    	isChange = true;
								    	break;
							    	}
							    }			    			
				    		}
						}
						String filest = doc4jVR.asXML();
				    	fileByte = filest.getBytes(Phrase.UTF_8);
					    operationMgr.SaveOperationListFile(fileByte);
			        	bean.setMessage("Upload " + file.getFileName() +" successfully.");
			        }
			        else{
			        	this.Log("OperationMgr.do: Unable to Save " + file.getFileName() + " file.",Level.FATAL);
			        	bean.setMessage("Fail to upload " + file.getFileName() + " file.");
			        }
			        
		        	return mapping.findForward("uploadTemplateFile");				
		        }
			}else if(act!=null && act.equalsIgnoreCase("updateValidateList")){
		        file = bean.getUploadFile();
		        if (file != null && !file.getFileName().trim().equals("")){
		        	if(this.HasValidationFile(file, bean)){
						isSaved = operationMgr.SaveSubListFile(file);
				        if (isSaved) {
				        	// update operation list file
				        	String validateFileId = operationMgr.GetOperationSubFileID(file.getFileName());
				        	
							// update file
							fileByte = operationMgr.GetOperationListFile();
							if (fileByte != null){
							    SAXReader reader = new SAXReader();
							    doc4jVR = reader.read(new ByteArrayInputStream(fileByte));
							    Element rootElm = doc4jVR.getRootElement();
							    // get operationId and Name
							    List operationMgrBeanList = rootElm.elements("Operation");
							    boolean isChange = false;
					    		for(Iterator j = operationMgrBeanList.iterator(); j.hasNext(); ){
					    			Element operation = (Element) j.next();
					    			// change existing element
							    	if(operation.attribute("id").getText().trim().equalsIgnoreCase(bean.getOperationId())){			    			
							    		
								    	Element newValidate = operation.element("Validate");
								    	List fileList = newValidate.elements("File");
								    	for(Iterator k = fileList.iterator(); k.hasNext(); ){
								    		Element fileElement = (Element) k.next();
								    		if(fileElement.attribute("id").getText().trim().equalsIgnoreCase(validateFileId)){	
								    			fileElement.setText(file.getFileName());
										    	isChange = true;
								    		}
								    	}
								    	break;
							    	}
							    }
					    		if(!isChange){	// add new element
						    		for(Iterator j = operationMgrBeanList.iterator(); j.hasNext(); ){
						    			Element operation = (Element) j.next();
						    			// change existing element
								    	if(operation.attribute("id").getText().trim().equalsIgnoreCase(bean.getOperationId())){			    			
		
									    	Element newValidate = operation.element("Validate");
									    	Element newFile = newValidate.addElement("File")
									    							.addAttribute("id", validateFileId);
									    	newFile.setText(file.getFileName());
									    	isChange = true;
									    	break;
								    	}
								    }			    			
					    		}
							}
							String filest = doc4jVR.asXML();
					    	fileByte = filest.getBytes(Phrase.UTF_8);
						    operationMgr.SaveOperationListFile(fileByte);
				        	bean.setMessage("Upload " + file.getFileName() +" successfully.");
				        }
				        else{
				        	this.Log("OperationMgr.do: Unable to Save " + file.getFileName() + " file.",Level.FATAL);
				        	bean.setMessage("Fail to upload " + file.getFileName() + " file.");
				        }		        		
		        	}else{
		        		bean.setMessage("You only can upload one validation file!");
		        	}
			        
		        	return mapping.findForward("uploadValidationFile");				
		        }
			}else if(act!=null && act.equalsIgnoreCase("deleteTemplateList")){
				if(request.getParameter("dataString")!=null){
					inputJson = request.getParameter("dataString");
					
					JSONObject jsonObject = JSONObject.fromObject(inputJson);   
					operationId = jsonObject.getString("operationId");
					JSONArray templateListJson = jsonObject.getJSONArray("templateList");
					OperationMgrTemplateBean[] operationMgrTemplateBeanInputArr = null;
	
					if(templateListJson!=null){
						Object[] templateListJsonList = templateListJson.toArray();				
						operationMgrTemplateBeanInputArr = new OperationMgrTemplateBean[templateListJsonList.length];
						for(int i=0;i<operationMgrTemplateBeanInputArr.length;i++){
							operationMgrTemplateBeanInputArr[i] = (OperationMgrTemplateBean)JSONObject.toBean((JSONObject)templateListJsonList[i], OperationMgrTemplateBean.class);
						}
					}
	
					fileByte = operationMgr.GetOperationListFile();
					if (fileByte != null){
					    SAXReader reader = new SAXReader();
					    doc4jVR = reader.read(new ByteArrayInputStream(fileByte));
					    // get operationId and Name
					    Element rootElm = doc4jVR.getRootElement();
					    List operationMgrBeanList = rootElm.elements("Operation");
					    boolean isChange = false;
					    
				    	for(int i=0;i<operationMgrTemplateBeanInputArr.length;i++){
				    		for(Iterator j = operationMgrBeanList.iterator(); j.hasNext(); ){
				    			Element operation = (Element) j.next();
						    	if(operation.attribute("id").getText().trim().equalsIgnoreCase(operationId)){			    			
							    	Element myView = operation.element("View");
								    List myTemplateList = myView.elements("Template");
						    		for(Iterator k = myTemplateList.iterator(); k.hasNext(); ){
						    			Element myTemplate = (Element) k.next();
						    			if(myTemplate.attribute("id").getText().trim().equalsIgnoreCase(operationMgrTemplateBeanInputArr[i].getTemplateId())){
						    				isChange = operationMgr.DeleteSubFileByID(myTemplate.attribute("id").getText().trim());
						    				if(isChange){
							    				myView.remove(myTemplate);					    															    
								    			break;
						    				}else{
						    		        	this.Log("OperationMgr.do: Unable to delete template file",Level.ERROR);
						    		        	ret = "Unable to delete template file.";
						    		        	printMsgToClient(ret, response);
	
						    		    		return null;
						    		        	
						    				}
						    			}
						    		}
						    	}
						    }				    		
				    	}
					}
					String filest = doc4jVR.asXML();
			    	fileByte = filest.getBytes(Phrase.UTF_8);
				    operationMgr.SaveOperationListFile(fileByte);
				}
			}else if(act!=null && act.equalsIgnoreCase("deleteValidateList")){
				if(request.getParameter("dataString")!=null){
					inputJson = request.getParameter("dataString");
					
					JSONObject jsonObject = JSONObject.fromObject(inputJson);   
					operationId = jsonObject.getString("operationId");
					JSONArray valdiateListJson = jsonObject.getJSONArray("validateList");
					OperationMgrValidateBean[] operationMgrValdiateBeanInputArr = null;
	
					if(valdiateListJson!=null){
						Object[] valdiateListJsonList = valdiateListJson.toArray();				
						operationMgrValdiateBeanInputArr = new OperationMgrValidateBean[valdiateListJsonList.length];
						for(int i=0;i<operationMgrValdiateBeanInputArr.length;i++){
							operationMgrValdiateBeanInputArr[i] = (OperationMgrValidateBean)JSONObject.toBean((JSONObject)valdiateListJsonList[i], OperationMgrValidateBean.class);
						}
					}
	
					fileByte = operationMgr.GetOperationListFile();
					if (fileByte != null){
					    SAXReader reader = new SAXReader();
					    doc4jVR = reader.read(new ByteArrayInputStream(fileByte));
					    // get operationId and Name
					    Element rootElm = doc4jVR.getRootElement();
					    List operationMgrBeanList = rootElm.elements("Operation");
					    boolean isChange = false;
					    
				    	for(int i=0;i<operationMgrValdiateBeanInputArr.length;i++){
				    		for(Iterator j = operationMgrBeanList.iterator(); j.hasNext(); ){
				    			Element operation = (Element) j.next();
						    	if(operation.attribute("id").getText().trim().equalsIgnoreCase(operationId)){			    			
							    	Element myValidate = operation.element("Validate");
								    List myFileList = myValidate.elements("File");
						    		for(Iterator k = myFileList.iterator(); k.hasNext(); ){
						    			Element myFile = (Element) k.next();
						    			if(myFile.attribute("id").getText().trim().equalsIgnoreCase(operationMgrValdiateBeanInputArr[i].getValidateFileId())){
						    				isChange = operationMgr.DeleteSubFileByID(myFile.attribute("id").getText().trim());
						    				if(isChange){
						    					myValidate.remove(myFile);					    															    
								    			break;
						    				}else{
						    		        	this.Log("OperationMgr.do: Unable to delete validation file",Level.ERROR);
						    		        	ret = "Unable to delete validation file.";
						    		        	printMsgToClient(ret, response);
	
						    		    		return null;
						    		        	
						    				}
						    			}
						    		}
						    	}
						    }				    		
				    	}
					}
					String filest = doc4jVR.asXML();
			    	fileByte = filest.getBytes(Phrase.UTF_8);
				    operationMgr.SaveOperationListFile(fileByte);
				}
			}else if(act!=null && act.equalsIgnoreCase("deleteParameterList")){
				if(request.getParameter("dataString")!=null){
					inputJson = request.getParameter("dataString");
					
					JSONObject jsonObject = JSONObject.fromObject(inputJson);   
					operationId = jsonObject.getString("operationId");
					JSONArray parameterListJson = jsonObject.getJSONArray("parameterList");
					OperationMgrParameterBean[] operationMgrParameterBeanInputArr = null;
	
					if(parameterListJson!=null){
						Object[] parameterListJsonList = parameterListJson.toArray();				
						operationMgrParameterBeanInputArr = new OperationMgrParameterBean[parameterListJsonList.length];
						for(int i=0;i<operationMgrParameterBeanInputArr.length;i++){
							operationMgrParameterBeanInputArr[i] = (OperationMgrParameterBean)JSONObject.toBean((JSONObject)parameterListJsonList[i], OperationMgrParameterBean.class);
						}
					}
	
					fileByte = operationMgr.GetOperationListFile();
					if (fileByte != null){
					    SAXReader reader = new SAXReader();
					    doc4jVR = reader.read(new ByteArrayInputStream(fileByte));
					    // get operationId and Name
					    Element rootElm = doc4jVR.getRootElement();
					    List operationList = rootElm.elements("Operation");
					    boolean isChange = false;
					    
				    	for(int i=0;i<operationMgrParameterBeanInputArr.length;i++){
				    		for(Iterator j = operationList.iterator(); j.hasNext(); ){
				    			Element operation = (Element) j.next();
						    	if(operation.attribute("id").getText().trim().equalsIgnoreCase(operationId)){			    			
							    	Element upload = operation.element("Upload");				    		
							    	Element parameters = upload.element("Parameters");
								    List paraList = parameters.elements("Parameter");
						    		for(Iterator k = paraList.iterator(); k.hasNext(); ){
						    			Element para = (Element) k.next();
						    			if(para.attribute("id").getText().trim().equalsIgnoreCase(operationMgrParameterBeanInputArr[i].getParameterId())){
						    				//isChange = operationMgr.DeleteSubFileByID(para.attribute("id").getText().trim());
						    				isChange = true;
						    				if(isChange){
						    					parameters.remove(para);					    															    
								    			break;
						    				}else{
						    		        	this.Log("OperationMgr.do: Unable to delete parameter",Level.ERROR);
						    		        	ret = "Unable to delete parameter.";
						    		        	printMsgToClient(ret, response);
	
						    		    		return null;
						    		        	
						    				}
						    			}
						    		}
						    	}
						    }				    		
				    	}
					}
					String filest = doc4jVR.asXML();
			    	fileByte = filest.getBytes(Phrase.UTF_8);
				    operationMgr.SaveOperationListFile(fileByte);
				}
			}else if(act!=null && act.equalsIgnoreCase("viewDocument")){
				String documentId = request.getParameter("documentId");
				String templateId = request.getParameter("templateId");
				String onlineTrans = request.getParameter("onlineTrans");
				operationId = request.getParameter("operationId");
								
				if(onlineTrans!=null && onlineTrans.equalsIgnoreCase("y")){	// Transform file online
					bean.clean();
					bean.setView(documentId+","+templateId);
					return mapping.findForward("viewDocument");								
				}else{														// Download Transform file
					//WI 17860
					Document document = null;
					InputStream documentFile = null;
					byte[] templateFile = null;
					if (documentId != null && templateId != null) {
						document = documentMgr.GetDocument(Integer.parseInt(documentId));
						if (document.GetType().equalsIgnoreCase(Phrase.ZIP_TYPE)) {
							documentFile = NodeUtils.getUnZipFile(document.GetContent());
						} else {
							documentFile = new ByteArrayInputStream(document.GetContent());
						}
						templateFile = configMgr.GetConfig(Integer.parseInt(templateId)).getBytes(Phrase.UTF_8);
		
						//String path = AppUtils.getAppRoot() + "temp/";
						String temFile = "Node_TemplateFile_tmp"+ Utility.GetSysTimeString() + ".xslt";
						InputStream is = new ByteArrayInputStream(templateFile);
						Utility.writeFile(path + temFile, is);
						// Transform file
						Stylizer s = new Stylizer();
					    s.setParameter("rootDir", AppUtils.getAppRoot());
					    // Find transform file suffix
					    String transformSuffix = "xml";
						fileByte = operationMgr.GetOperationListFile();
						if (fileByte != null){
						    SAXReader reader = new SAXReader();
						    doc4jVR = reader.read(new ByteArrayInputStream(fileByte));
						    // get operationId and Name
						    Element rootElm = doc4jVR.getRootElement();
						    List operationMgrBeanList = rootElm.elements("Operation");
						    
				    		for(Iterator j = operationMgrBeanList.iterator(); j.hasNext(); ){
				    			Element operation = (Element) j.next();
						    	if(operation.attribute("id").getText().trim().equalsIgnoreCase(operationId)){			    			
							    	Element myView = operation.element("View");
								    List myTemplateList = myView.elements("Template");
						    		for(Iterator k = myTemplateList.iterator(); k.hasNext(); ){
						    			Element myTemplate = (Element) k.next();
						    			if(myTemplate.attribute("id").getText().trim().equalsIgnoreCase(templateId)){
						    				transformSuffix = myTemplate.attribute("transformSuffix").getText().trim();							    		
						    			}
						    		}
						    	}
						    }				    							    	
						}
					    
					    String outputFileName = "Node_TransformFile_tmp" + Utility.GetSysTimeString() + "." + transformSuffix;
						String outputFilePath = path + outputFileName;
						s.transformToFile(path + temFile, documentFile, outputFilePath);
						if (is != null) {
							is.close();
							is = null;
						}
						if (documentFile != null) {
							documentFile.close();
							documentFile = null;
						}
						// delete temp unzip template file
						File myDelFile = new File(path+temFile);
						if (myDelFile.exists()) {
							myDelFile.delete();
						}
						bean.setView(outputFileName);
						return mapping.findForward("downloadViewDocument");								
					}else{
						ret = "Please check document and template file.";
					}					
				}
			}else if(act!=null && act.equalsIgnoreCase("showDocument")){
				String[] viewPara = bean.getView().split(",");
				String documentId = viewPara[0];
				String templateId = viewPara[1];
	
				Document document = null;
				InputStream documentFile = null;
				byte[] templateFile = null;
				if (documentId != null && templateId != null) {
					document = documentMgr.GetDocument(Integer.parseInt(documentId));
					if (document.GetType().equalsIgnoreCase(Phrase.ZIP_TYPE)) {
						documentFile = NodeUtils.getUnZipFile(document.GetContent());
					} else {
						documentFile = new ByteArrayInputStream(document.GetContent());
					}
					templateFile = configMgr.GetConfig(Integer.parseInt(templateId)).getBytes(Phrase.UTF_8);
	
					//String path = AppUtils.getAppRoot() + "temp/";
					String temFile = "Node_TemplateFile_tmp"+ Utility.GetSysTimeString() + ".xslt";
					InputStream is = new ByteArrayInputStream(templateFile);
					Utility.writeFile(path + temFile, is);
					// Transform file
					Stylizer s = new Stylizer();
				    s.setParameter("rootDir", AppUtils.getAppRoot());
					String outputFilePath = path + "Node_TransformFile_tmp" + Utility.GetSysTimeString() + ".txt";
					s.transformToFile(path + temFile, documentFile, outputFilePath);
					FileInputStream fi = new FileInputStream(outputFilePath);
					byte[] output = new byte[fi.available()];
					fi.read(output);
	//				ret = new String(output);
					response.getOutputStream().write(output);
					response.getOutputStream().close();
					if (is != null) {
						is.close();
						is = null;
					}
					if (fi != null) {
						fi.close();
						fi = null;
					}
					if (documentFile != null) {
						documentFile.close();
						documentFile = null;
					}
					// delete temp unzip file
					File myDelFile = new File(path+temFile);
					if (myDelFile.exists()) {
						myDelFile.delete();
					}
					myDelFile = new File(outputFilePath);
					if (myDelFile.exists()) {
						myDelFile.delete();
					}
					myDelFile = null;
					return null;
				}else{
					ret = "Please check document and template file.";
				}
			}else if(act!=null && act.equalsIgnoreCase("uploadSubmissionFile")){
				//submissionFileId = request.getParameter("submissionFileId");
				//transId = request.getParameter("transId");
				//validationFileId = request.getParameter("validateFileId");
				operationId = request.getParameter("operationId");
				operationName = request.getParameter("operationName");
				// get validationFileId
				fileByte = operationMgr.GetOperationListFile();
				if (fileByte != null){
					List operationMgrValidateBeanList = null;
				    SAXReader reader = new SAXReader();
				    doc4jVR = reader.read(new ByteArrayInputStream(fileByte));
				    // get operationId and Name
				    List operationMgrBeanList = doc4jVR.selectNodes(".//OperationList/Operation");
	
				    if (operationMgrBeanList != null && operationMgrBeanList.size() > 0) {
				    	for(int i=0;i<operationMgrBeanList.size();i++){
				    		if(this.getAttributeValue((Node) operationMgrBeanList.get(i),"@id").trim().equalsIgnoreCase(operationId.trim())){			    			
				    			List valdiateList = ((Node) operationMgrBeanList.get(i)).selectNodes("Validate");
				    			operationMgrValidateBeanList = ((Node)valdiateList.get(0)).selectNodes("File");
				    			break;
				    		}
				    	}
					    if (operationMgrValidateBeanList != null && operationMgrValidateBeanList.size() > 0) {
					    	validationFileId = ((Element)operationMgrValidateBeanList.get(0)).attribute("id").getText().trim();
					    }
				    }
				}
				bean.clean();
//				bean.setSubmissionFileId(submissionFileId);
//				bean.setTransID(transId);
				bean.setValidationFileId(validationFileId);
				bean.setOperationName(operationName);
				return mapping.findForward("uploadSubmissionFile");			
			}else if(act!=null && act.equalsIgnoreCase("validateSubmissionFile")){
				file = bean.getUploadFile();
				//submissionFileId = bean.getSubmissionFileId();
				validationFileId = bean.getValidationFileId();
				operationName = bean.getOperationName();
				//transId = bean.getTransID();
				if (file != null && !file.getFileName().trim().equals("")){
					String tmpSubmissionFile = null;
					// get file type
					String[] fileNameArr = file.getFileName().split("[.]");
					String fileType = fileNameArr[fileNameArr.length-1];
					if(validationFileId!=null && !validationFileId.equals("")){
						if(fileType.equalsIgnoreCase(Phrase.ZIP_TYPE)){
							fileByte = NodeUtils.getUnZipByteArray(file.getFileData());
						}else{
							fileByte = file.getFileData();
						}
						// Write file to temp folder for future real upload
						//String path = AppUtils.getAppRoot() + "temp/";
						tmpSubmissionFile = fileNameArr[0] +"_tmp"+ Utility.GetSysTimeString() +"."+ ((fileType.equalsIgnoreCase(Phrase.ZIP_TYPE))?Phrase.XML_TYPE:fileType);
						InputStream is = new ByteArrayInputStream(fileByte);
						Utility.writeFile(path + tmpSubmissionFile, is);

						String ruleFile = configMgr.GetConfig(Integer.parseInt(validationFileId));
						// Do validation
						XML validator = new XML();

						if(validator.CheckWellFormed(new String(fileByte))){
							ValidationManager t = new ValidationManager();
							String validateStatus = t.PerformValidationForTesting(ruleFile.getBytes(Phrase.UTF_8),fileByte);
							
							// Rewrite file to its original file type. 
							File myDelFile = new File(path +tmpSubmissionFile);
							if (myDelFile.exists()) {
								myDelFile.delete();
							}
							myDelFile = null;		            	
							tmpSubmissionFile = fileNameArr[0] +"_tmp"+ Utility.GetSysTimeString() +"."+ fileType;
							Utility.writeFile(path + tmpSubmissionFile, file.getInputStream());
							session.setAttribute("tmpSubmissionFile", tmpSubmissionFile);
							
							if(!validateStatus.equalsIgnoreCase("ok")){
								FileInputStream fi = new FileInputStream(validateStatus);
								byte[] output = new byte[fi.available()];
								fi.read(output);
								ret = new String(output);
								output = null;
								ret = ret.replaceAll("value=\"\" name=\"filename\"", "name=\"filename\" value=\""+ tmpSubmissionFile +"\"");
								ret = ret.replaceAll("value=\"\" name=\"fileID\"", "name=\"fileID\" value=\""+ submissionFileId +"\"");
								ret = ret.replaceAll("value=\"\" name=\"operationName\"", "name=\"operationName\" value=\""+ operationName +"\"");
								ret = ret.replaceAll("value=\"\" name=\"transID\"", "name=\"transID\" value=\""+ transId +"\"");
								if (is != null) {
									is.close();
									is = null;
								}
								if (fi != null) {
									fi.close();
									fi = null;
								}
								// delete temp files
								myDelFile = new File(validateStatus);
								if (myDelFile.exists()) {
									myDelFile.delete();
								}
								myDelFile = null;		            	
							}else{
								// Call webservice
								String version = (String)session.getAttribute("version");
								String token = this.Authenticate(version);	        
								ClsNodeDocument[] doc = new ClsNodeDocument[1];
								doc[0] = new ClsNodeDocument();
								doc[0].setContent(file.getFileData());
								doc[0].setType(fileType);
								doc[0].setName(file.getFileName().trim());

								if (token !=null ) {
									String result = this.Upload(version, token, transId ,operationName, doc);		        	
									ret = "<html><head/><body><div id='successful'>Upload " + file.getFileName().trim() +" successfully. The transaction ID is: " + result +"</div></body></html>";
									// delete temp files
									myDelFile = new File(path + tmpSubmissionFile);
									if (myDelFile.exists()) {
										myDelFile.delete();
									}
									myDelFile = null;
								}
								else{
									this.Log("OperationMgr.do>>> Unable to upload " + file.getFileName().trim() + " file.",Level.FATAL);
									ret = "<html><head/><body><div id='fail'>Fail to upload " + file.getFileName().trim() + " file.</div></body></html>";
								}		        				
							}
						}else{
							ret = "<html><head/><body><div id='invalid'>"+file.getFileName() + " is not well form.</div></body></html>";			        	
						}
					}else{
						// no validation and upload directly
						ClsNodeDocument[] docs = new ClsNodeDocument[1];
						docs[0] = new ClsNodeDocument();
						docs[0].setName(file.getFileName());
						docs[0].setContent(file.getFileData());
						docs[0].setType(fileType);
						ret = this.SaveSubmissionFile(request,operationName,tmpSubmissionFile,docs);		
					}
				}else{
					ret = "<html><head/><body><div id='upload'>Please upload file.</div></body></html>";
				}			        		    			        				
			}else if(act!=null && act.equalsIgnoreCase("saveSubmissionFile")){
				String tmpFileName = (String)session.getAttribute("tmpSubmissionFile");
				operationName = request.getParameter("operationName");
				ret = this.SaveSubmissionFile(request,operationName,tmpFileName,null);
/*				String tmpFileName = request.getParameter("filename");
				//submissionFileId = request.getParameter("fileID");
				operationName = request.getParameter("operationName");
				transId = request.getParameter("transID");
				String path = AppUtils.getAppRoot() + "temp/";
				String tmpSubmissionFilePath = path + tmpFileName;
				
				fileByte = Utility.readFile(tmpSubmissionFilePath);
		        ClsNodeDocument[] doc = new ClsNodeDocument[1];
		        doc[0] = new ClsNodeDocument();
		        doc[0].setContent(fileByte);
		        // get file type
				String[] fileNameArr = tmpFileName.split("[.]");
				doc[0].setType(fileNameArr[fileNameArr.length-1]);
		        tmpFileName = fileNameArr[0].split("_tmp")[0] + "." + fileNameArr[fileNameArr.length-1];
		        doc[0].setName(tmpFileName);
		        
		        // Call webservice
		        String version = (String)session.getAttribute("version");
		        String token = this.Authenticate(version);	        
		        
		        if (token !=null ) {
		        	String result = this.Upload(version, token, transId ,operationName, doc);		        	
		        	ret = "Upload " + tmpFileName +" successfully. The transcation ID is: " + result;
					// delete temp files
					File myDelFile = new File(tmpSubmissionFilePath);
					if (myDelFile.exists()) {
						myDelFile.delete();
					}
					myDelFile = null;
		        }
		        else{
		        	this.Log("OperationMgr.do: Unable to upload " + tmpFileName + " file.",Level.FATAL);
		        	ret = "Fail to upload " + tmpFileName + " file.";
		        }		        				
*/
				
			}else if(act!=null && act.equalsIgnoreCase("cancelSubmissionFile")){
				//String path = AppUtils.getAppRoot() + "temp/";
				String tmpFileName = (String)session.getAttribute("tmpSubmissionFile");
				session.setAttribute("tmpSubmissionFile","");
				if(tmpFileName!=null && !tmpFileName.equalsIgnoreCase("")){
					// delete temp files
					File myDelFile = new File(path + tmpFileName);
					if (myDelFile.exists()) {
						myDelFile.delete();
					}
					myDelFile = null;					
					ret = "Upload file process has been canceled.";
				}else{
					ret = "There is no upload file.";					
				}
			}else if(act!=null && act.equalsIgnoreCase("generateFile")){
				if(request.getParameter("dataString")!=null){
					inputJson = request.getParameter("dataString");
					OperationMgrGenerateParameterBean[] generateParameterBeanInputArr = null;
			        String[] paramsNames = null;
			        String[] params = null;
			        String[] paramTypes = null;
			        String[] paramEncodes = null;
					
					if(!inputJson.equalsIgnoreCase("")){
						JSONObject jsonObject = JSONObject.fromObject(inputJson);   
						JSONArray generateParameterListJson = jsonObject.getJSONArray("generateParameterList");
								
						if(generateParameterListJson!=null){
							Object[] generateParameterListJsonList = generateParameterListJson.toArray();				
							generateParameterBeanInputArr = new OperationMgrGenerateParameterBean[generateParameterListJsonList.length];
							for(int i=0;i<generateParameterBeanInputArr.length;i++){
								generateParameterBeanInputArr[i] = (OperationMgrGenerateParameterBean)JSONObject.toBean((JSONObject)generateParameterListJsonList[i], OperationMgrGenerateParameterBean.class);
							}
						}						
				        paramsNames = new String[generateParameterBeanInputArr.length];
				        params = new String[generateParameterBeanInputArr.length];
				        paramTypes = new String[generateParameterBeanInputArr.length];
				        paramEncodes = new String[generateParameterBeanInputArr.length];
					}
					
			        // Call webservice
			        String version = (String)session.getAttribute("version");
			        String token = this.Authenticate(version);	        
			        String dataFlow = request.getParameter("dataFlow");
			        
			        if (token !=null ) {
			        	if(generateParameterBeanInputArr!=null){
				        	for(int i=0;i<generateParameterBeanInputArr.length;i++){
				        		paramsNames[i] = generateParameterBeanInputArr[i].getGenerateParameterName();
				        		params[i] = generateParameterBeanInputArr[i].getGenerateParameterValue();
				        		paramTypes[i] = "string";
				        		paramEncodes[i] = "None";
				        	}			        		
			        	}
			        	String result = this.Solicit(version, token, null, dataFlow, null, paramsNames, params, paramTypes, paramEncodes);		        	
			        	ret = "Generate file successfully. The transcation ID is: " + result;
			        }
			        else{
			        	this.Log("OperationMgr.do: Unable to generate file.",Level.FATAL);
			        	ret = "Fail to generate file.";
			        }		        				
				}
		        
			}else if(act!=null && act.equalsIgnoreCase("submitFile")){
				if(request.getParameter("dataStr")!=null){
					operationName = request.getParameter("operationName").trim();
					inputJson = request.getParameter("dataStr");
					OperationMgrSubmissionBean[] opSubmissionBeanInputArr = null;
			        Document doc= null;
			        ClsNodeDocument[] docArr = null;
			        
					if(!inputJson.equalsIgnoreCase("")){
						JSONObject jsonObject = JSONObject.fromObject(inputJson);   
						JSONArray fileListJson = jsonObject.getJSONArray("submissionFileList");
								
						if(fileListJson!=null){
							Object[] fileListJsonList = fileListJson.toArray();				
							opSubmissionBeanInputArr = new OperationMgrSubmissionBean[fileListJsonList.length];
							for(int i=0;i<opSubmissionBeanInputArr.length;i++){
								opSubmissionBeanInputArr[i] = (OperationMgrSubmissionBean)JSONObject.toBean((JSONObject)fileListJsonList[i], OperationMgrSubmissionBean.class);
							}
						}						
						docArr = new ClsNodeDocument[opSubmissionBeanInputArr.length];
					}
					for(int i=0;opSubmissionBeanInputArr!=null && i<opSubmissionBeanInputArr.length;i++){
						fileID = opSubmissionBeanInputArr[i].getFileId();
						doc = documentMgr.GetDocument(Integer.parseInt(opSubmissionBeanInputArr[i].getFileId()));
						docArr[i] = new ClsNodeDocument();
						docArr[i].setName(doc.GetName());
						docArr[i].setContent(doc.GetContent());
						docArr[i].setType(doc.GetType());						
					}
			        

					// find operation
					fileByte = operationMgr.GetOperationListFile();
					
					String version = Phrase.ver_1;
					Element URL = null;
			    	Element userName = null;
			    	Element password = null;
			    	Element domainName = null;
			    	// WI 23016
			    	Element dataFlow = null;
			    	
			    	String opID = null;
			    	
					
					if (fileByte != null){
						List operationMgrParameterList = null;
					    SAXReader reader = new SAXReader();
					    doc4jVR = reader.read(new ByteArrayInputStream(fileByte));
					    Element rootElm = doc4jVR.getRootElement();
					    // get operationId and Name
					    List operationList = rootElm.elements("Operation");
		
					    if (operationList != null && operationList.size() > 0) {
				    		for(Iterator i = operationList.iterator(); i.hasNext(); ){
				    			Element operation = (Element) i.next();
				    			// get parameter
						    	if(operation.attribute("name").getText().trim().equalsIgnoreCase(operationName)){
						    		version = operation.attribute("version").getText().trim();
							    	opID = operation.attribute("id").getText().trim();
							    	dataFlow = operation.element("DataFlow");
							    	
							    	Element submit = operation.element("Submit");
							    	URL = submit.element("URL");
							    	userName = submit.element("UserName");
							    	password = submit.element("Password");
							    	domainName = submit.element("DomainName");
							    	
							    	break;								    	
					    		}
					    	}
					    }
					}

					// Call webservice										
			    	Cryptography crypt = new Cryptography();
					String passwordStr = crypt.Decrypting(password.getText().trim(),Phrase.CryptKey);
					
			        String token = this.AuthenticateRemote(version, URL.getText().trim(), userName.getText().trim(), passwordStr,domainName.getText().trim());	        
			        operationName = request.getParameter("operationName");
			        transId="";
			        
			        if (token !=null ) {
						String operationManagerTransID = documentMgr.GetDocumentTransactionID(Integer.parseInt(fileID));
			        	String result = this.Submit(Integer.parseInt(opID),version, token, transId ,operationName, docArr,URL.getText().trim(), operationManagerTransID,dataFlow.getText().trim());		        	
			        	ret = "Submit files successfully. The transcation ID is: " + result;
			        }
			        else{
			        	this.Log("OperationMgr.do: Unable to submit files.",Level.FATAL);
			        	ret = "Fail to submit files.";
			        }		        				
				}else{
					ret = "Fail to submit files.";
				}
			}
			
			printMsgToClient(ret, response);
	
			return null;
		} catch (Exception e) {
			if(e.getMessage()==null){
				ret = "Fail to finish operation.";
			}else{
				ret = e.getMessage();
			}
			printMsgToClient(ret, response);
			throw e;
		}
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
	   * changeOperationListToJsonString
	   * @param aList
	   * @return String
	   */
	private String changeOperationListToJsonString(List aList ) throws Exception{
		String records = "";
		List jsonList = new ArrayList();
    	Cryptography crypt = new Cryptography();

		for(int i=0;i<aList.size();i++){
			String password = crypt.Decrypting(this.getElementValue((Node) aList.get(i),"Submit/Password").trim(),Phrase.CryptKey);
            jsonList.add("{\"gridId\":" + i
                        +",\"operationId\":" + "\"" + this.getAttributeValue((Node) aList.get(i),"@id").trim() + "\"" 
                        +",\"operationName\":" + "\"" + this.getAttributeValue((Node) aList.get(i),"@name").trim() + "\"" 
                        +",\"operationVersion\":" + "\"" + this.getAttributeValue((Node) aList.get(i),"@version").trim() + "\"" 
                        +",\"dataFlow\":" + "\"" + this.getElementValue((Node) aList.get(i),"DataFlow").trim() + "\"" 
                        +",\"upload\":" + "\"" + this.getAttributeValue(((Node) aList.get(i)).selectSingleNode("Upload"),"@Enable").trim() + "\"" 
                        +",\"generate\":" + "\"" + this.getAttributeValue(((Node) aList.get(i)).selectSingleNode("Generate"),"@Enable").trim() + "\"" 
                        +",\"submit\":" + "\"" + this.getAttributeValue(((Node) aList.get(i)).selectSingleNode("Submit"),"@Enable").trim() + "\"" 
                        +",\"URL\":" + "\"" + this.getElementValue((Node) aList.get(i),"Submit/URL").trim() + "\"" 
                        +",\"userName\":" + "\"" + this.getElementValue((Node) aList.get(i),"Submit/UserName").trim() + "\"" 
                        +",\"password\":" + "\"" + password + "\"" 
                        +",\"domainName\":" + "\"" + this.getElementValue((Node) aList.get(i),"Submit/DomainName").trim() + "\"" 
                        +",\"getStatus\":" + "\"" + this.getAttributeValue(((Node) aList.get(i)).selectSingleNode("GetStatus"),"@Enable").trim() + "\"" 
                        +",\"getStatusError\":" + "\"" + this.getElementValue((Node) aList.get(i),"GetStatus/Error").trim() + "\"" 
                        +",\"getStatusComplete\":" + "\"" + this.getElementValue((Node) aList.get(i),"GetStatus/Complete").trim() + "\"" 
            			+"}");                  		
		}
		
		JSONArray jsonArray = JSONArray.fromObject(jsonList);           
		
		Map map = new HashMap();
		map.put("results", jsonArray);
		JSONObject jsonObject = JSONObject.fromObject(map);
		records = jsonObject.toString();
		records = records.substring(records.indexOf("{") + 1);
		records = "{\"total\":"+ aList.size() +","	+ records;
		
		return records;
	}
	
	  /**
	   * changeOperationListTemplateToJsonString
	   * @param aList
	   * @return String
	   */
	private String changeOperationListTemplateToJsonString(List aList ){
		String records = "{\"total\":0,\"results\":{}}";
		List jsonList = new ArrayList();
		if(aList != null){
			for(int i=0;i<aList.size();i++){
	            jsonList.add("{\"gridId\":" + i
	                        +",\"templateId\":" + "\"" + this.getAttributeValue((Node) aList.get(i),"@id").trim() + "\"" 
	                        +",\"templateName\":" + "\"" + ((Node) aList.get(i)).getText().trim() + "\"" 
	                        +",\"transformSuffix\":" + "\"" + this.getAttributeValue((Node) aList.get(i),"@transformSuffix").trim() + "\"" 
	            			+"}");                  		
			}
			
			JSONArray jsonArray = JSONArray.fromObject(jsonList);           
			
			Map map = new HashMap();
			map.put("results", jsonArray);
			JSONObject jsonObject = JSONObject.fromObject(map);
			records = jsonObject.toString();
			records = records.substring(records.indexOf("{") + 1);
			records = "{\"total\":"+ aList.size() +","	+ records;			
		}
		
		return records;
	}
	
	  /**
	   * changeOperationListValidateToJsonString
	   * @param aList
	   * @return String
	   */
	private String changeOperationListValidateToJsonString(List aList ){
		String records = "{\"total\":0,\"results\":{}}";
		List jsonList = new ArrayList();
		if(aList != null){
			for(int i=0;i<aList.size();i++){
	            jsonList.add("{\"gridId\":" + i
	                        +",\"validateFileId\":" + "\"" + this.getAttributeValue((Node) aList.get(i),"@id").trim() + "\"" 
	                        +",\"validateFileName\":" + "\"" + ((Node) aList.get(i)).getText().trim() + "\"" 
	            			+"}");                  		
			}
			
			JSONArray jsonArray = JSONArray.fromObject(jsonList);           
			
			Map map = new HashMap();
			map.put("results", jsonArray);
			JSONObject jsonObject = JSONObject.fromObject(map);
			records = jsonObject.toString();
			records = records.substring(records.indexOf("{") + 1);
			records = "{\"total\":"+ aList.size() +","	+ records;			
		}
		
		return records;
	}
	
	  /**
	   * changeOperationListParameterToJsonString
	   * @param aList
	   * @return String
	   */
	private String changeOperationListParameterToJsonString(List aList ){
		String records = "";
		List jsonList = new ArrayList();
		for(int i=0;i<aList.size();i++){
            jsonList.add("{\"gridId\":" + i
                        +",\"parameterId\":" + "\"" + this.getAttributeValue((Node) aList.get(i),"@id").trim() + "\"" 
                        +",\"parameterName\":" + "\"" + this.getAttributeValue((Node) aList.get(i),"@name").trim() + "\"" 
                        +",\"parameterValue\":" + "\"" + ((Node) aList.get(i)).getText().trim() + "\"" 
            			+"}");                  		
		}
		
		JSONArray jsonArray = JSONArray.fromObject(jsonList);           
		
		Map map = new HashMap();
		map.put("results", jsonArray);
		JSONObject jsonObject = JSONObject.fromObject(map);
		records = jsonObject.toString();
		records = records.substring(records.indexOf("{") + 1);
		records = "{\"total\":"+ aList.size() +","	+ records;
		
		return records;
	}
	
	  /**
	   * changeOperationWebServiceParameterListToJsonString
	   * @param aList
	   * @return String
	   */
	private String changeOperationWebServiceParameterListToJsonString(List aList ){
		String records = "";
		List jsonList = new ArrayList();
		for(int i=0;i<aList.size();i++){
            jsonList.add("{\"gridId\":" + i
                        +",\"generateParameterName\":" + "\"" + ((String)((ArrayList) aList.get(i)).get(1)).trim() + "\"" 
                        +",\"generateParameterValue\":" + "\"" + "" + "\"" 
            			+"}");                  		
		}
		
		JSONArray jsonArray = JSONArray.fromObject(jsonList);           
		
		Map map = new HashMap();
		map.put("results", jsonArray);
		JSONObject jsonObject = JSONObject.fromObject(map);
		records = jsonObject.toString();
		records = records.substring(records.indexOf("{") + 1);
		records = "{\"total\":"+ aList.size() +","	+ records;
		
		return records;
	}

	  /**
	   * changeConditionNameListToJsonString
	   * @param aList
	   * @return String
	   */
	private String changeConditionNameListToJsonString(List aList ){
		String records = "";
		List jsonList = new ArrayList();
		for(int i=0;i<aList.size();i++){
            jsonList.add("{\"gridId\":" + i
                        +",\"conditionId\":" + "\"" + i + "\"" 
                        +",\"conditionName\":" + "\"" + (String)aList.get(i) + "\"" 
            			+"}");                  		
		}
		
		JSONArray jsonArray = JSONArray.fromObject(jsonList);           
		
		Map map = new HashMap();
		map.put("results", jsonArray);
		JSONObject jsonObject = JSONObject.fromObject(map);
		records = jsonObject.toString();
		records = records.substring(records.indexOf("{") + 1);
		records = "{\"total\":"+ aList.size() +","	+ records;
		
		return records;
	}
	
	  /**
	   * changeOperationManagerTableListToJsonString
	   * @param aList
	   * @return String
	   */
	private String changeOperationManagerTableListToJsonString(ArrayList aList ){
		String records = "";
		List jsonList = new ArrayList();
		OperationManager opMgr = new OperationManager();
		SimpleDateFormat f = new SimpleDateFormat("yyyyMMddhhmmss");
		for(int i=0;i<aList.size();i++){
			opMgr = (OperationManager)aList.get(i);
            jsonList.add("{\"gridId\":" + i
                        +",\"submitId\":" + "\"" + opMgr.getSubmitId() + "\"" 
                        +",\"operationName\":" + "\"" + (opMgr.getOperationName()==null?"":opMgr.getOperationName().trim()) + "\"" 
                        +",\"statusCD\":" + "\"" + (opMgr.getStatusCD()==null?"":opMgr.getStatusCD().trim()) + "\"" 
                        +",\"submitURL\":" + "\"" + (opMgr.getSubmitURL()==null?"":opMgr.getSubmitURL()) + "\"" 
                        +",\"submitDate\":" + "\"" + (opMgr.getSubmitDate()==null?"":(com.enfotech.basecomponent.utility.Utility.GetDateTime(opMgr.getSubmitDate(), "yyyy-MM-dd"))) + "\"" 
                        +",\"version\":" + "\"" + (opMgr.getVersion()==null?"":opMgr.getVersion()) + "\"" 
                        +",\"transId\":" + "\"" + (opMgr.getTransId()==null?"":opMgr.getTransId()) + "\"" 
                        +",\"supplyTransId\":" + "\"" + (opMgr.getSupplyTransId()==null?" ":opMgr.getSupplyTransId()) + "\"" 
                        +",\"dataFlow\":" + "\"" + (opMgr.getDataFlow()==null?"":opMgr.getDataFlow()) + "\"" 
            			+"}");                  		
		}
		
		JSONArray jsonArray = JSONArray.fromObject(jsonList);           
		
		Map map = new HashMap();
		map.put("results", jsonArray);
		JSONObject jsonObject = JSONObject.fromObject(map);
		records = jsonObject.toString();
		records = records.substring(records.indexOf("{") + 1);
		records = "{\"total\":"+ aList.size() +","	+ records;
		
		return records;
	}

	  /**
	   * getElementValue
	   * @param node
	   * @param xPath
	   * @return String
	   */
	private String getElementValue(Node node, String xPath) {
		String value = "";

		if (node == null || xPath == null) {
			return value;
		}

		Node attNode = node.selectSingleNode(xPath);
		if (attNode != null) {
			value = attNode.getText();			
		}
		
		return value;
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
	
	  /**
	   * Authenticate
	   * @param version
	   * @return String
	   */
	private String Authenticate(String version) throws Exception {
		try {
			ISystemConfiguration config = DBManager.GetSystemConfiguration(Phrase.AdministrationLoggerName);
			String userID = null;
			String password = null;
			String authMethod = Phrase.AUTHENTICATION_METHOD_PASSWORD;
			String nodeAddress = null;
			URL node = null;
			String token = null;
			String domainName = "";
			
			if(debug){
				userID = "DCMLocal";
				password = "password";
			}else{
				userID = config.GetNodeAdminUID();
				password = config.GetNodeAdminPWD();				
			}
			if(version.equalsIgnoreCase(Phrase.ver_1)){
				nodeAddress = config.GetNodeURL();
				node = new URL(nodeAddress);
				NodeRequestor requestor = new NodeRequestor(node,Phrase.AdministrationLoggerName);
				token = requestor.authenticate(userID, password, authMethod);
			}else{
				nodeAddress = config.GetNodeURL_V2();
				node = new URL(nodeAddress);
			    Node2.webservice.Requestor.NodeRequestor requestor = new Node2.webservice.Requestor.NodeRequestor(node, Phrase.AdministrationLoggerName);
				token = requestor.authenticate(userID, password, authMethod,domainName);
			}
			if (token != null && !token.equals("")) {
				return token;
			} else {
				return null;
			}
		} catch (Exception e) {
			throw e;
		}
	}

	  /**
	   * AuthenticateRemote
	   * @param version
	   * @param nodeAddress
	   * @param userID
	   * @param password
	   * @param domainName
	   * @return String
	   */
	private String AuthenticateRemote(String version, String nodeAddress, String userID, String password,String domainName) throws Exception {
		try {
			String authMethod = Phrase.AUTHENTICATION_METHOD_PASSWORD;
			URL node = null;
			String token = null;
			
			if(debug){
				userID = "DCMLocal";
				password = "password";
			}
			if(version.equalsIgnoreCase(Phrase.ver_1)){
				node = new URL(nodeAddress);
				NodeRequestor requestor = new NodeRequestor(node,Phrase.AdministrationLoggerName);
				token = requestor.authenticate(userID, password, authMethod);
			}else{
				node = new URL(nodeAddress);
			    Node2.webservice.Requestor.NodeRequestor requestor = new Node2.webservice.Requestor.NodeRequestor(node, Phrase.AdministrationLoggerName);
				token = requestor.authenticate(userID, password, authMethod,domainName);
			}
			if (token != null && !token.equals("")) {
				return token;
			} else {
				return null;
			}
		} catch (Exception e) {
			throw e;
		}
	}

	  /**
	   * Upload
	   * @param version
	   * @param token
	   * @param transID
	   * @param dataFlow
	   * @param docs
	   * @return String
	   */
	 private String Upload(String version, String token, String transID,String dataFlow, ClsNodeDocument[] docs) throws Exception {
			String nodeAddress = null;
			URL node = null;
			String result = null;
		try {
			ISystemConfiguration config = DBManager.GetSystemConfiguration(Phrase.AdministrationLoggerName);
			if(version.equalsIgnoreCase(Phrase.ver_1)){
				nodeAddress = config.GetNodeURL();
				node = new URL(nodeAddress);
				NodeRequestor requestor = new NodeRequestor(node,Phrase.AdministrationLoggerName);
				result = requestor.submit(token, transID, dataFlow, docs);
			}else{
				nodeAddress = config.GetNodeURL_V2();
				node = new URL(nodeAddress);
			    Node2.webservice.Requestor.NodeRequestor requestor = new Node2.webservice.Requestor.NodeRequestor(node, Phrase.AdministrationLoggerName);
			    
			    INodeDomain domainDB = DBManager.GetNodeDomain(Phrase.AdministrationLoggerName);
			    String domain = domainDB.GetDomainName(dataFlow, Phrase.WEB_METHOD_SUBMIT);
			    String flowOperation = dataFlow;
			    String[] recipientList = null;
			    String[] notificationURIList = null;
			    String notificationTypes = "None";
			    result = requestor.submit(token, transID, domain, flowOperation,recipientList,notificationURIList,notificationTypes,docs);
			}
			return result;
		}catch (Exception e) {
			this.Log("OperationMgrAction.do: Error - Submit " + e.getMessage(), Level.ERROR);
			throw e;
		}
	}
	  
	  /**
	   * Solicit
	   * @param version
	   * @param token
	   * @param transID
	   * @param dataFlow
	   * @param returnURL
	   * @param paramsNames
	   * @param params
	   * @param paramTypes
	   * @param paramEncodes
	   * @return String
	   */
	  private String Solicit(String version, String token, String transID,
			String dataFlow, String returnURL,String[] paramsNames, String[] params,
			String[] paramTypes, String[] paramEncodes) throws Exception {
		String nodeAddress = null;
		URL node = null;
		String result = null;
		try {
			ISystemConfiguration config = DBManager.GetSystemConfiguration(Phrase.AdministrationLoggerName);
			if (version.equalsIgnoreCase(Phrase.ver_1)) {
				nodeAddress = config.GetNodeURL();
				node = new URL(nodeAddress);
				NodeRequestor requestor = new NodeRequestor(node,
						Phrase.AdministrationLoggerName);
				result = requestor.solicit(token, returnURL, dataFlow, params);
			} else {
				nodeAddress = config.GetNodeURL_V2();
				node = new URL(nodeAddress);
				Node2.webservice.Requestor.NodeRequestor requestor = new Node2.webservice.Requestor.NodeRequestor(
						node, Phrase.AdministrationLoggerName);

			    INodeDomain domainDB = DBManager.GetNodeDomain(Phrase.AdministrationLoggerName);
			    String domain = domainDB.GetDomainName(dataFlow, Phrase.WEB_METHOD_SOLICIT);
				String flowOperation = dataFlow;
				String[] recipientList = null;
				String[] notificationURIList = null;
				String notificationTypes = "None";
				String[] notificationURITypeStrList = null;
				result = requestor.solicit(token, domain, flowOperation,
						recipientList, notificationURITypeStrList,
						notificationTypes, paramsNames, params, paramTypes,
						paramEncodes);
			}
			return result;
		} catch (Exception e) {
			this.Log("OperationMgrAction.do: Error - Solicit " + e.getMessage(), Level.ERROR);
			throw e;
		}
	}

	  /**
	   * Submit
	   * @param opID
	   * @param version
	   * @param token
	   * @param transID
	   * @param operationName
	   * @param docs
	   * @param nodeAddress
	   * @param operationManagerTransID
	   * @return String
	   */
	private String Submit(int opID,String version, String token, String transID,
			String operationName, ClsNodeDocument[] docs, String nodeAddress,String operationManagerTransID,String dataFlow) throws Exception {
		URL node = null;
		String result = null;
		try {
			if (version.equalsIgnoreCase(Phrase.ver_1)) {
				node = new URL(nodeAddress);
				NodeRequestor requestor = new NodeRequestor(node,
						Phrase.AdministrationLoggerName);
				result = requestor.submit(token, transID, null, docs);
			} else if (version.equalsIgnoreCase(Phrase.ver_2)){
				node = new URL(nodeAddress);
				Node2.webservice.Requestor.NodeRequestor requestor = new Node2.webservice.Requestor.NodeRequestor(
						node, Phrase.AdministrationLoggerName);

			    INodeDomain domainDB = DBManager.GetNodeDomain(Phrase.AdministrationLoggerName);
			    INodeOperation operation = DBManager.GetNodeOperation(Phrase.AdministrationLoggerName);
			    int domainID =operation.GetDomainID(opID);
			    // WI 23016
			    String domain = dataFlow;
			    String flowOperation = "default";
			    String[] recipientList = null;
			    String[] notificationURIList = null;
			    String notificationTypes = "None";
			    result = requestor.submit(token, transID, domain, flowOperation,recipientList,notificationURIList,notificationTypes,docs);
			}
			if(result != null){
				// Add Operation Manger table to feed GetStatus Task
				this.AddOperationManger(result, opID, operationManagerTransID);				
			}
			return result;
		} catch (Exception e) {
			this.Log("OperationMgrAction.do: Error - Submit " + e.getMessage(),
					Level.ERROR);
			throw e;
		}
	}
		  
	  /**
	   * SaveSubmissionFile
	   * @param request
	   * @param operationName
	   * @param tmpFileName
	   * @param document
	   * @return String
	   */
	private String SaveSubmissionFile(HttpServletRequest request,String operationName, String tmpFileName, ClsNodeDocument[] document) throws Exception {
		HttpSession session = request.getSession();
		String transId = request.getParameter("transID")==null?"":request.getParameter("transID");
		byte[] fileByte = null;
		String fileType = null;
		String ret = null;
		
		if(tmpFileName!=null && !tmpFileName.equalsIgnoreCase("")){	// The file was validated and ignore
			String path = Utility.GetTempFilePath() + "/temp/";
			//String path = AppUtils.getAppRoot() + "temp/";
			String tmpSubmissionFilePath = path + tmpFileName;
			String zipFile = "";
			
			fileByte = Utility.readFile(tmpSubmissionFilePath);
	        ClsNodeDocument[] doc = new ClsNodeDocument[1];
	        doc[0] = new ClsNodeDocument();
	        // get file type
			String[] fileNameArr = tmpFileName.split("[.]");
			fileType = fileNameArr[fileNameArr.length-1];
			doc[0].setType(fileType);
	        tmpFileName = fileNameArr[0].split("_tmp")[0] + "." + fileNameArr[fileNameArr.length-1];
	        doc[0].setName(tmpFileName);
	        doc[0].setContent(fileByte);
	        //doc[0].setId("Ignore");
	        transId = "Ignore";
	        
	        // Call webservice
	        String version = (String)session.getAttribute("version");
	        String token = this.Authenticate(version);	        
	        
	        if (token !=null ) {
	        	String result = this.Upload(version, token, transId ,operationName, doc);		        	
	        	ret = "Upload " + tmpFileName +" successfully. The transcation ID is: " + result;
	        }
	        else{
	        	this.Log("OperationMgr.do>>> Unable to upload " + tmpFileName + " file.",Level.FATAL);
	        	ret = "Fail to upload " + tmpFileName + " file.";
	        }
			// delete temp files
			File myDelFile = new File(tmpSubmissionFilePath);
			if (myDelFile.exists()) {
				myDelFile.delete();
			}
			myDelFile = null;
			session.setAttribute("tmpSubmissionFile","");
		}else if((tmpFileName==null || tmpFileName.equalsIgnoreCase("")) && document!=null){ // The file was not validated
	        // Call webservice
	        String version = (String)session.getAttribute("version");
	        String token = this.Authenticate(version);	        
	        
	        if (token !=null ) {
	        	String result = this.Upload(version, token, transId ,operationName, document);		        	
	        	ret = "Upload " + document[0].getName() +" successfully. The transcation ID is: " + result;
	        }			
	        else{
	        	this.Log("OperationMgr.do>>> Unable to upload " + document[0].getName() + " file.",Level.FATAL);
	        	ret = "Fail to upload " + document[0].getName() + " file.";
	        }
		}else{
        	ret = "Fail to upload " + tmpFileName + " file.";
		}
        return ret;
	}

	  /**
	   * HasValidationFile
	   * @param file
	   * @param bean
	   * @return boolean
	   */
	private boolean HasValidationFile(FormFile file,OperationMgrBean bean) throws Exception {
        IOperationMgr operationMgr = DBManager.getOperationMgr(Phrase.AdministrationLoggerName);
    	String validateFileId = operationMgr.GetOperationSubFileID(file.getFileName());
		org.dom4j.Document doc4jVR = null;
		boolean ret = false;
    	
		// check file
		byte[] fileByte = null;
		fileByte = operationMgr.GetOperationListFile();
		if (fileByte != null){
		    SAXReader reader = new SAXReader();
		    doc4jVR = reader.read(new ByteArrayInputStream(fileByte));
		    Element rootElm = doc4jVR.getRootElement();
		    // get operationId and Name
		    List operationMgrBeanList = rootElm.elements("Operation");
    		for(Iterator j = operationMgrBeanList.iterator(); j.hasNext(); ){
    			Element operation = (Element) j.next();
    			// change existing element
		    	if(operation.attribute("id").getText().trim().equalsIgnoreCase(bean.getOperationId())){			    			
		    		
			    	Element newValidate = operation.element("Validate");
			    	List fileList = newValidate.elements("File");
			    	if(fileList==null || fileList.size()==0){
			    		ret = true;
			    	}
			    	for(Iterator k = fileList.iterator(); k.hasNext(); ){
			    		Element fileElement = (Element) k.next();
			    		if(fileElement.attribute("id").getText().trim().equalsIgnoreCase(validateFileId)){	
					    	ret = true;
					    	break;
			    		}
			    	}
		    	}
		    	if(ret) break;
		    }
		}
		return ret;
	}
	
	  /**
	   * AddOperationManger
	   * @param supTransID
	   * @param opID
	   * @param operationManagerTransID
	   * @return boolean
	   */
	protected void AddOperationManger (String supTransID, int opID, String operationManagerTransID) throws RemoteException{
	  String submitURL = "";
	  IOperationMgr opMgr = DBManager.getOperationMgr(Phrase.WebServicesLoggerName);
	  String version = null;
	  // WI 24094
	  String dataflow = null;
	  try {
		  if(opMgr.CheckGetStatus(opID)){
			  Operation op;
			  op = new Operation(opID,Phrase.WebServicesLoggerName);

			  byte[] fileByte = opMgr.GetOperationListFile();
			  if (fileByte != null){
				  SAXReader reader = new SAXReader();
				  org.dom4j.Document doc4jVR = reader.read(new ByteArrayInputStream(fileByte));
				  // get operationId and Name
				  List operationMgrBeanList = null;

				  operationMgrBeanList = doc4jVR.selectNodes(".//OperationList/Operation");
				  for(Iterator j = operationMgrBeanList.iterator(); j.hasNext(); ){
					  org.dom4j.Node myNode = (org.dom4j.Node)j.next();
					  if(this.getAttributeValue(myNode,"@id").trim().equalsIgnoreCase(Integer.toString(opID))){			    			
						  submitURL = this.getElementValue(myNode, ".//Submit/URL");
						  version = this.getAttributeValue(myNode,"@version");
						  dataflow = this.getElementValue(myNode, ".//DataFlow");
						  break;
					  }
				  }				    		
			  }
			  if(dataflow == null || dataflow.equals("")){
				  dataflow = op.GetDomain();
			  }
			  opMgr.AddOperationManger(op.GetOpName(), Phrase.PendingStatus, submitURL, version, operationManagerTransID, supTransID, null, dataflow);				  					  
			  	  
		  }
	  }catch (Exception e) {
		  throw new RemoteException(Phrase.InternalError,e);
	  }
  }
}
