package Node2.web.Services;

import java.io.PrintWriter;
import java.math.BigDecimal;
import java.math.BigInteger;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.Hashtable;
import java.util.List;
import java.util.Map;

import javax.servlet.ServletOutputStream;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.HttpSession;

import net.exchangenetwork.schema.dedl.x1.DataConstrainType;
import net.exchangenetwork.schema.dedl.x1.DataElementDescriptionType;
import net.exchangenetwork.schema.dedl.x1.DataElementListDocument;
import net.exchangenetwork.schema.dedl.x1.DataElementListType;
import net.exchangenetwork.schema.dedl.x1.DataElementPropertyType;
import net.exchangenetwork.schema.dedl.x1.DataElementValueType;
import net.exchangenetwork.schema.dedl.x1.DataSourceListType;
import net.exchangenetwork.schema.dedl.x1.ElementDataSourceType;
import net.exchangenetwork.schema.ends.x2.EncodingType;
import net.exchangenetwork.schema.ends.x2.NetworkNodeListType;
import net.exchangenetwork.schema.ends.x2.NetworkNodeType;
import net.exchangenetwork.schema.ends.x2.NetworkNodesDocument;
import net.exchangenetwork.schema.ends.x2.NodeBoundingBoxType;
import net.exchangenetwork.schema.ends.x2.NodeMethodTypeCode;
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
import org.w3c.dom.Element;
import org.w3c.dom.Node;

import Node.Phrase;
import Node.Biz.Administration.Operation;
import Node.Biz.Administration.User;
import Node.DB.DBManager;
import Node.DB.Interfaces.INodeOperation;
import Node.DB.Interfaces.Configuration.IGetServices;
import Node.Utils.LoggingUtils;
import Node.Utils.Utility;
import Node.Web.Administration.BaseAction;
import Node2.web.Services.model.DedlElementValueBean;
import Node2.web.Services.model.DedlGeneralBean;
import Node2.web.Services.model.DedlPropertyBean;
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

public class GetServicesAction extends BaseAction {
	  /**
	   * Constructor
	   * @param 
	   * @return 
	   */
	public GetServicesAction() {
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
		GetServicesBean bean = (GetServicesBean)form;
		User user= new User((String)session.getAttribute(Phrase.USER_SESSION),Phrase.AdministrationLoggerName);
		String version = (String)session.getAttribute(Phrase.NodeVersion);
	    boolean isSaved = false;
	    FormFile file = null;
		String nodeIdentifier = "";
		String nodeName = "";
		String nodeAddress = "";
		String organizationIdentifier = "";
		String nodeContact = "";
		String nodeVersionIdentifier = "";
		String nodeDeploymentTypeCode = "";
		String nodeStatus = "";
		String[] nodePropertyName = null;
		String[] nodePropertyValue = null;
		String north = "";
		String south = "";
		String east = "";
		String west = "";
		String act = request.getParameter("act");
		String ret = null;
		String inputJson = null;
		byte[] fileByte = null;
		String fileStr = null;
		NetworkNodesDocument networkNodesDocument = null;
		DataElementListDocument dataElementListDocument = null;
	    boolean isError = false;

	    //bean.setMessage("");
        IGetServices getService = DBManager.getGetServices(Phrase.AdministrationLoggerName);
        // WI 35458
        if(user.IsNodeAdmin()){
    		if(act!=null && act.equalsIgnoreCase("getGeneralData")){
    			fileByte = getService.getConfigFile(version,Phrase.REGISTRATION_FILE_NAME);
    			if (fileByte != null){
    				fileStr = new String(fileByte);
    				networkNodesDocument = NetworkNodesDocument.Factory.parse(fileStr);
    				ret = "{\"getServicesGeneralData\":[{\"nodeIdentifier\": \""+networkNodesDocument.getNetworkNodes().getNetworkNodeDetailsArray(0).getNodeIdentifier().trim()+"\"," +
    						" \"nodeName\": \""+networkNodesDocument.getNetworkNodes().getNetworkNodeDetailsArray(0).getNodeName().trim()+"\"," +
    						" \"nodeAddress\": \""+networkNodesDocument.getNetworkNodes().getNetworkNodeDetailsArray(0).getNodeAddress().trim()+"\"," +
    						" \"organizationIdentifier\": \""+networkNodesDocument.getNetworkNodes().getNetworkNodeDetailsArray(0).getOrganizationIdentifier().trim()+"\", " +
    						"\"nodeContact\": \""+networkNodesDocument.getNetworkNodes().getNetworkNodeDetailsArray(0).getNodeContact().trim()+"\"," +
    						" \"nodeVersionIdentifier\": \""+networkNodesDocument.getNetworkNodes().getNetworkNodeDetailsArray(0).getNodeVersionIdentifier().toString().trim()+"\"," +
    						" \"nodeDeploymentTypeCode\": \""+networkNodesDocument.getNetworkNodes().getNetworkNodeDetailsArray(0).getNodeDeploymentTypeCode().toString().trim()+"\", " +
    						"\"nodeStatus\": \""+networkNodesDocument.getNetworkNodes().getNetworkNodeDetailsArray(0).getNodeStatus().toString().trim()+"\"," +
    						" \"north\": \""+networkNodesDocument.getNetworkNodes().getNetworkNodeDetailsArray(0).getBoundingBoxDetails().getBoundingCoordinateNorth().toString().trim()+"\"," +
    						" \"south\": \""+networkNodesDocument.getNetworkNodes().getNetworkNodeDetailsArray(0).getBoundingBoxDetails().getBoundingCoordinateSouth().toString().trim()+"\", " +
    						"\"east\": \""+networkNodesDocument.getNetworkNodes().getNetworkNodeDetailsArray(0).getBoundingBoxDetails().getBoundingCoordinateEast().toString().trim()+"\", " +
    						"\"west\": \""+networkNodesDocument.getNetworkNodes().getNetworkNodeDetailsArray(0).getBoundingBoxDetails().getBoundingCoordinateWest().toString().trim()+"\"}]}";

    			}else ret = "";
    		}else if(act!=null && act.equalsIgnoreCase("getNodePropertyList")){
    			fileByte = getService.getConfigFile(version,Phrase.REGISTRATION_FILE_NAME);
    			if (fileByte != null){
    				fileStr = new String(fileByte);
    				networkNodesDocument = NetworkNodesDocument.Factory.parse(fileStr);
    				ObjectPropertyType[] objectPropertyTypeList = networkNodesDocument.getNetworkNodes().getNetworkNodeDetailsArray(0).getNodePropertyArray();
    				if(objectPropertyTypeList!=null && objectPropertyTypeList.length>0){
    					ret = changePropertyListToJsonString(objectPropertyTypeList);
    				}else ret = "{\"total\":0,\"results\":[]}";
    			}else ret = "{\"total\":0,\"results\":[]}";
    		}else if(act!=null && act.equalsIgnoreCase("getDedlGeneralDataList")){
    			fileByte = getService.getConfigFile(null,Phrase.DEDL_FILE_NAME);
    			ret = "{\"total\":0,\"results\":[]}";
    			if (fileByte != null){
    				fileStr = new String(fileByte);
    				dataElementListDocument = DataElementListDocument.Factory.parse(fileStr);
    				DataElementListType dataElementListType = dataElementListDocument.getDataElementList();
    				if(dataElementListType!=null){
    					DataElementDescriptionType[] dataElementList = dataElementListType.getDataElementArray();
    					if(dataElementList!=null && dataElementList.length>0){
    						ret = changeDataElementListToJsonString(dataElementList);						
    					}
    				}
    			}	
    		}else if(act!=null && act.equalsIgnoreCase("getDedlPropertyDataList")){
    			fileByte = getService.getConfigFile(null,Phrase.DEDL_FILE_NAME);
    			ret = "{\"total\":0,\"results\":[]}";
    			if (fileByte != null){
    				fileStr = new String(fileByte);
    				dataElementListDocument = DataElementListDocument.Factory.parse(fileStr);
    				DataElementListType dataElementListType = dataElementListDocument.getDataElementList();
    				if(dataElementListType!=null){
    					DataElementDescriptionType[] dataElementList = dataElementListType.getDataElementArray();
    					if(dataElementList!=null && dataElementList.length>0){
    						for(int i=0;i<dataElementList.length;i++){
    							String dedlElementIdentifier = request.getParameter("dedlElementIdentifier");
    							if(dedlElementIdentifier!=null && dedlElementIdentifier.equalsIgnoreCase(dataElementList[i].getElementIdentifier())){
    								DataElementPropertyType[] dataElementPropertyTypeList = dataElementList[i].getPropertyArray();
    								ret = changeDataElementPropertyListToJsonString(dataElementPropertyTypeList,dedlElementIdentifier);														
    								break;
    							}							
    						}
    					}
    				}
    			}			
    		}else if(act!=null && act.equalsIgnoreCase("getDedlElementValueDataList")){
    			fileByte = getService.getConfigFile(null,Phrase.DEDL_FILE_NAME);
    			ret = "{\"total\":0,\"results\":[]}";
    			if (fileByte != null){
    				fileStr = new String(fileByte);
    				dataElementListDocument = DataElementListDocument.Factory.parse(fileStr);
    				DataElementListType dataElementListType = dataElementListDocument.getDataElementList();
    				if(dataElementListType!=null){
    					DataElementDescriptionType[] dataElementList = dataElementListType.getDataElementArray();
    					if(dataElementList!=null && dataElementList.length>0){
    						for(int i=0;i<dataElementList.length;i++){
    							String dedlElementIdentifier = request.getParameter("dedlElementIdentifier");
    							if(dedlElementIdentifier!=null && dedlElementIdentifier.equalsIgnoreCase(dataElementList[i].getElementIdentifier())){
    								DataElementValueType[] dataElementValueTypeList = dataElementList[i].getElementValueArray();
    								ret = changeDataElementValueListToJsonString(dataElementValueTypeList,dedlElementIdentifier);														
    								break;
    							}							
    						}
    					}
    				}
    			}			
    		}else if(act!=null && act.equalsIgnoreCase("upload")){
    			return mapping.findForward("upload");			
    		}else if(act!=null && act.equalsIgnoreCase("saveUpload")){
    	        file = bean.getUploadFile();
    	        if (file != null && !file.getFileName().trim().equals("")){
    	        	fileByte = file.getFileData();
    				isSaved = getService.saveConfigFile(fileByte,version,Phrase.REGISTRATION_FILE_NAME);
    		        if (!isSaved) {
    		        	this.Log("GetServices.do: Unable to Save Registration Settings",Level.FATAL);
    		        	bean.setMessage("Fail to upload file.");
    		        }
    		        else bean.setMessage("Upload file successfully.");
    		        
    	        	return mapping.findForward("upload");				
    	        }
    		}else if(act!=null && act.equalsIgnoreCase("showDownload")){
    			return mapping.findForward("download");			
    		}else if(act!=null && act.equalsIgnoreCase("download")){
    			String configName = null;
    			// WI 21296
    			configName = Utility.getConfigFileNameWithVersion(version, Phrase.REGISTRATION_FILE_NAME);					
    			response.setContentType("text/xml");
    			response.addHeader("Content-Disposition","attachment;filename="+configName);
    			ServletOutputStream out = response.getOutputStream();
    			fileByte = Utility.generateGetServiceFile(version).getBytes();
    			//fileByte = getService.getConfigFile(version,Phrase.REGISTRATION_FILE_NAME);
    			if (fileByte != null)
    				for (int i = 0; i < fileByte.length; i++)
    					out.write(fileByte[i]);
    			out.close();
    			fileByte = null;
    			return null;			
    		}else if(act!=null && act.equalsIgnoreCase("saveRegistrationData")){
    			if(request.getParameter("dataString")!=null){
    				inputJson = request.getParameter("dataString");
    				
    				JSONObject jsonObject = JSONObject.fromObject(inputJson);   
    				String nodeGeneralDataJson = jsonObject.getString("NodeGeneralData");
    				JSONArray nodePropertyListJson = jsonObject.getJSONArray("NodePropertyList");
    				JSONArray nodeServicesListJson = jsonObject.getJSONArray("NodeServicesList");
    				JSONArray nodeServicesPropertyListJson = jsonObject.getJSONArray("NodeServicesPropertyList");
    				JSONArray nodeServicesParametersListJson = jsonObject.getJSONArray("NodeServicesParametersList");
    				GetServicesNodePropertyBean[] getServicesNodePropertyBeanList = null;
    				GetServicesNodeServicesBean[] getServicesNodeServicesBeanList = null;
    				GetServicesNodeServicesPropertyBean[] getServicesNodeServicesPropertyBeanList = null;
    				GetServicesNodeServicesParametersBean[] getServicesNodeServicesParametersBeanList = null;
    				jsonObject = JSONObject.fromObject(nodeGeneralDataJson);
    				
    				GetServicesDataBean getServicesDataBean = (GetServicesDataBean)JSONObject.toBean(jsonObject, GetServicesDataBean.class);
    				
    				if(nodePropertyListJson!=null){
    					Object[] getServicesNodePropertyBeanJsonList = nodePropertyListJson.toArray();				
    					getServicesNodePropertyBeanList = new GetServicesNodePropertyBean[getServicesNodePropertyBeanJsonList.length];
    					for(int i=0;i<getServicesNodePropertyBeanList.length;i++){
    						getServicesNodePropertyBeanList[i] = (GetServicesNodePropertyBean)JSONObject.toBean((JSONObject)getServicesNodePropertyBeanJsonList[i], GetServicesNodePropertyBean.class);
    					}
    				}
    				
    				/*if(nodeServicesListJson!=null){
    					Object[] getServicesNodeServicesBeanJsonList = nodeServicesListJson.toArray();				
    					getServicesNodeServicesBeanList = new GetServicesNodeServicesBean[getServicesNodeServicesBeanJsonList.length];
    					for(int i=0;i<getServicesNodeServicesBeanJsonList.length;i++){
    						getServicesNodeServicesBeanList[i] = (GetServicesNodeServicesBean)JSONObject.toBean((JSONObject)getServicesNodeServicesBeanJsonList[i], GetServicesNodeServicesBean.class);
    					}
    				}

    				if(nodeServicesPropertyListJson!=null){
    					Object[] getServicesNodeServicesPropertyBeanJsonList = nodeServicesPropertyListJson.toArray();				
    					getServicesNodeServicesPropertyBeanList = new GetServicesNodeServicesPropertyBean[getServicesNodeServicesPropertyBeanJsonList.length];
    					for(int i=0;i<getServicesNodeServicesPropertyBeanJsonList.length;i++){
    						getServicesNodeServicesPropertyBeanList[i] = (GetServicesNodeServicesPropertyBean)JSONObject.toBean((JSONObject)getServicesNodeServicesPropertyBeanJsonList[i], GetServicesNodeServicesPropertyBean.class);
    					}
    				}

    				if(nodeServicesParametersListJson!=null){
    					Object[] getServicesNodeServicesParametersBeanJsonList = nodeServicesParametersListJson.toArray();				
    					getServicesNodeServicesParametersBeanList = new GetServicesNodeServicesParametersBean[getServicesNodeServicesParametersBeanJsonList.length];
    					for(int i=0;i<getServicesNodeServicesParametersBeanList.length;i++){
    						getServicesNodeServicesParametersBeanList[i] = (GetServicesNodeServicesParametersBean)JSONObject.toBean((JSONObject)getServicesNodeServicesParametersBeanJsonList[i], GetServicesNodeServicesParametersBean.class);
    					}
    				}*/

    				fileByte = getService.getConfigFile(version,Phrase.REGISTRATION_FILE_NAME);
    				if (fileByte != null){
    					fileStr = new String(fileByte);
    					
    					// change general data
    					networkNodesDocument = NetworkNodesDocument.Factory.parse(fileStr);
    					networkNodesDocument.getNetworkNodes().getNetworkNodeDetailsArray(0).setNodeIdentifier(getServicesDataBean.getNodeIdentifier());
    					networkNodesDocument.getNetworkNodes().getNetworkNodeDetailsArray(0).setNodeName(getServicesDataBean.getNodeName());
    					networkNodesDocument.getNetworkNodes().getNetworkNodeDetailsArray(0).setNodeAddress(getServicesDataBean.getNodeAddress());
    					networkNodesDocument.getNetworkNodes().getNetworkNodeDetailsArray(0).setOrganizationIdentifier(getServicesDataBean.getOrganizationIdentifier());
    					networkNodesDocument.getNetworkNodes().getNetworkNodeDetailsArray(0).setNodeContact(getServicesDataBean.getNodeContact());
    					net.exchangenetwork.schema.ends.x2.NodeVersionCode.Enum myEnum1 = net.exchangenetwork.schema.ends.x2.NodeVersionCode.Enum.forString(getServicesDataBean.getNodeVersionIdentifier());
    					networkNodesDocument.getNetworkNodes().getNetworkNodeDetailsArray(0).setNodeVersionIdentifier(myEnum1);
    					net.exchangenetwork.schema.ends.x2.NodeStageCode.Enum myEnum2 = net.exchangenetwork.schema.ends.x2.NodeStageCode.Enum.forString(getServicesDataBean.getNodeDeploymentTypeCode());
    					networkNodesDocument.getNetworkNodes().getNetworkNodeDetailsArray(0).setNodeDeploymentTypeCode(myEnum2);
    					net.exchangenetwork.schema.ends.x2.NodeStatusCode.Enum myEnum3 = net.exchangenetwork.schema.ends.x2.NodeStatusCode.Enum.forString(getServicesDataBean.getNodeStatus());
    					networkNodesDocument.getNetworkNodes().getNetworkNodeDetailsArray(0).setNodeStatus(myEnum3);
    					networkNodesDocument.getNetworkNodes().getNetworkNodeDetailsArray(0).getBoundingBoxDetails().setBoundingCoordinateNorth(new BigDecimal((getServicesDataBean.getNorth()==null || getServicesDataBean.getNorth().equals(""))?"0":getServicesDataBean.getNorth()));
    					networkNodesDocument.getNetworkNodes().getNetworkNodeDetailsArray(0).getBoundingBoxDetails().setBoundingCoordinateSouth(new BigDecimal((getServicesDataBean.getSouth()==null || getServicesDataBean.getSouth().equals(""))?"0":getServicesDataBean.getSouth()));
    					networkNodesDocument.getNetworkNodes().getNetworkNodeDetailsArray(0).getBoundingBoxDetails().setBoundingCoordinateEast(new BigDecimal((getServicesDataBean.getEast()==null || getServicesDataBean.getEast().equals(""))?"0":getServicesDataBean.getEast()));
    					networkNodesDocument.getNetworkNodes().getNetworkNodeDetailsArray(0).getBoundingBoxDetails().setBoundingCoordinateWest(new BigDecimal((getServicesDataBean.getWest()==null || getServicesDataBean.getWest().equals(""))?"0":getServicesDataBean.getWest()));

    					//change Node property list
    					ObjectPropertyType[] objectPropertyTypeList = null;
    					if(getServicesNodePropertyBeanList!=null && getServicesNodePropertyBeanList.length>0){
    						objectPropertyTypeList = new ObjectPropertyType[getServicesNodePropertyBeanList.length];
    						for(int i=0;i<getServicesNodePropertyBeanList.length;i++){
    							objectPropertyTypeList[i] = ObjectPropertyType.Factory.newInstance();
    							objectPropertyTypeList[i].setPropertyName(getServicesNodePropertyBeanList[i].getNodePropertyName());
    							XmlAnySimpleType propertyValue = XmlAnySimpleType.Factory.newInstance();
    							propertyValue.setStringValue(getServicesNodePropertyBeanList[i].getNodePropertyValue());
    							objectPropertyTypeList[i].setPropertyValue(propertyValue);
    						}						
    					}
    					networkNodesDocument.getNetworkNodes().getNetworkNodeDetailsArray(0).setNodePropertyArray(objectPropertyTypeList);
    					
    					/*
    					//change Node Services list
    					Service[] serviceList = null;
    					ArrayList newServiceList = new ArrayList();
    					Service myService = null;
    					int serviceIndex = -1;
    					int newServiceIndex = -1;
    					ArrayList delServiceIndex = new ArrayList();
    					int delServiceFlag = -1;
    					if(getServicesNodeServicesBeanList!=null && getServicesNodeServicesBeanList.length>0){
    						serviceList = networkNodesDocument.getNetworkNodes().getNetworkNodeDetailsArray(0).getNodeServiceList().getServiceArray();
    						newServiceIndex = serviceList.length;
    						for(int i=0;i<serviceList.length;i++){ // find out which records have been deleted
    							delServiceFlag = -1;
    							for(int j=0;j<getServicesNodeServicesBeanList.length;j++){
    								if(serviceList[i].getDataflow().equalsIgnoreCase(getServicesNodeServicesBeanList[j].getDataFlow())){
    									delServiceFlag = 0;
    									break;
    								}
    							}
    							if(delServiceFlag == -1) delServiceIndex.add(new Integer(i));
    						}
    						for(int i=0;i<getServicesNodeServicesBeanList.length;i++){	// find out which records have been updated
    							for(int j=0;j<serviceList.length;j++){
    								if(serviceList[j].getDataflow().equalsIgnoreCase(getServicesNodeServicesBeanList[i].getDataFlow())){
    									myService = serviceList[j];
    									serviceIndex = j;
    								}
    							}
    							if(serviceIndex==-1 && serviceList.length > getServicesNodeServicesBeanList.length){ // there are records have been deleted
    								continue;
    							}else if(serviceIndex==-1 && serviceList.length <= getServicesNodeServicesBeanList.length){ // there are records have been added
    								myService = Service.Factory.newInstance();
    								newServiceIndex++;
    							}
    							
    							net.exchangenetwork.schema.ends.x2.NodeMethodTypeCode.Enum servicesEnum = net.exchangenetwork.schema.ends.x2.NodeMethodTypeCode.Enum.forString(getServicesNodeServicesBeanList[i].getMethodName());
    							myService.setMethodName(servicesEnum);
    							myService.setDataflow(getServicesNodeServicesBeanList[i].getDataFlow());
    							myService.setServiceIdentifier(getServicesNodeServicesBeanList[i].getServiceIdentifier());
    							myService.setServiceDescription(getServicesNodeServicesBeanList[i].getServiceDescription());
    							myService.setServiceDocumentURL(getServicesNodeServicesBeanList[i].getServiceDocumentURL());
    							String[] styleSheetURLList = getServicesNodeServicesBeanList[i].getStyleSheetURL().split(";");
    							if(styleSheetURLList!=null && styleSheetURLList.length>0){
    								myService.setStyleSheetURLArray(null); // clean old URL
    								for(int j=0;j<styleSheetURLList.length;j++){
    									myService.addNewStyleSheetURL().setStringValue(styleSheetURLList[j]);																	
    								}
    							}
    							// change Node Services property list
    							if(myService.getDataflow()!=null && getServicesNodeServicesPropertyBeanList.length>0 && myService.getDataflow().equalsIgnoreCase(getServicesNodeServicesPropertyBeanList[0].getNodeServicesPropertyDataFlow())){
    								if(getServicesNodeServicesPropertyBeanList.length==1 && getServicesNodeServicesPropertyBeanList[0].getNodeServicesPropertyName().equalsIgnoreCase("") && getServicesNodeServicesPropertyBeanList[0].getNodeServicesPropertyValue().equalsIgnoreCase("")){
    									objectPropertyTypeList = null;
    								}else{
    									objectPropertyTypeList = new ObjectPropertyType[getServicesNodeServicesPropertyBeanList.length];
    									for(int k=0;k<getServicesNodeServicesPropertyBeanList.length;k++){
    										objectPropertyTypeList[k] = ObjectPropertyType.Factory.newInstance();
    										objectPropertyTypeList[k].setPropertyName(getServicesNodeServicesPropertyBeanList[k].getNodeServicesPropertyName());
    										XmlAnySimpleType propertyValue = XmlAnySimpleType.Factory.newInstance();
    										propertyValue.setStringValue(getServicesNodeServicesPropertyBeanList[k].getNodeServicesPropertyValue());
    										objectPropertyTypeList[k].setPropertyValue(propertyValue);
    									}									
    								}
    								myService.setServicePropertyArray(objectPropertyTypeList);
    							}
    							// change Node Services parameter list
    							if(myService.getDataflow()!=null && getServicesNodeServicesParametersBeanList.length>0 && myService.getDataflow().equalsIgnoreCase(getServicesNodeServicesParametersBeanList[0].getNodeServicesParametersDataFlow())){
    								if(getServicesNodeServicesParametersBeanList.length==1 && getServicesNodeServicesParametersBeanList[0].getNodeServicesParametersEncoding().equalsIgnoreCase("")){
    									myService.setParameterArray(null);	
    								}else{
    									RequestParameterType[] requestParameterTypeList = null;
    									requestParameterTypeList = new RequestParameterType[getServicesNodeServicesParametersBeanList.length];
    									for(int k=0;k<getServicesNodeServicesParametersBeanList.length;k++){
    										requestParameterTypeList[k] = RequestParameterType.Factory.newInstance();
    										requestParameterTypeList[k].setParameterName(getServicesNodeServicesParametersBeanList[k].getNodeServicesParametersName());
    										requestParameterTypeList[k].setStringValue(getServicesNodeServicesParametersBeanList[k].getNodeServicesParametersValue());
    										requestParameterTypeList[k].setParameterSortIndex(!getServicesNodeServicesParametersBeanList[k].getNodeServicesParametersSortIndex().equalsIgnoreCase("")?new BigInteger(getServicesNodeServicesParametersBeanList[k].getNodeServicesParametersSortIndex()):null);
    										requestParameterTypeList[k].setParameterOccurrenceNumber(getServicesNodeServicesParametersBeanList[k].getNodeServicesParametersOccurenceNo());
    										net.exchangenetwork.schema.ends.x2.EncodingType.Enum encodeEnum = net.exchangenetwork.schema.ends.x2.EncodingType.Enum.forString(getServicesNodeServicesParametersBeanList[k].getNodeServicesParametersEncoding());
    										requestParameterTypeList[k].setParameterEncoding(encodeEnum);
    										requestParameterTypeList[k].setParameterType(getServicesNodeServicesParametersBeanList[k].getNodeServicesParametersType());
    										requestParameterTypeList[k].setParameterTypeDescriptor(getServicesNodeServicesParametersBeanList[k].getNodeServicesParametersTypeDesc());
    										requestParameterTypeList[k].setParameterRequiredIndicator(getServicesNodeServicesParametersBeanList[k].getNodeServicesParametersRequired().equalsIgnoreCase("true")?true:false);

    									}
    									myService.setParameterArray(requestParameterTypeList);									
    								}
    							}
    							if(serviceIndex!=-1) serviceList[serviceIndex] = myService;
    							else newServiceList.add(myService); 
    						}
    						for(int i=0;i<serviceList.length;i++){	// delete deleted records
    							for(int j=0;j<delServiceIndex.size();j++){
    								if(i==((Integer)delServiceIndex.get(j)).intValue()){
    									serviceList[i] = null;
    									break;
    								}															
    							}
    						}
    						for(int i=0;i<serviceList.length;i++){	// create a new service list
    							if(serviceList[i]!=null){								
    								newServiceList.add(serviceList[i]);
    							}															
    						}
    					}
    					
    					// get the new Services
    					Service[] newServiceArray = new Service[newServiceList.size()];
    					for(int i=0;i<newServiceArray.length;i++){
    						newServiceArray[i] = (Service)newServiceList.get(i);
    					}
    					networkNodesDocument.getNetworkNodes().getNetworkNodeDetailsArray(0).getNodeServiceList().setServiceArray(newServiceArray);
    					*/
    					
    				}else ret = "Fail to get Registration file from database.";
    				//File tf = new File("c:\\tempf.xml");
    				//networkNodesDocument.save(tf);
    				isSaved = getService.saveConfigFile(networkNodesDocument.xmlText().getBytes(),version,Phrase.REGISTRATION_FILE_NAME);
    		        if (!isSaved) {
    		        	this.Log("GetServices.do: Unable to Save Registration Settings",Level.FATAL);
    		        	ret = "Fail to upload file.";
    		        }
    		        else ret = "Update Registration file successfully.";
    			}else ret = "Fail to update Registration file.";			
    		}else if(act!=null && act.equalsIgnoreCase("saveDedlData")){ // WI 21296
    			if(request.getParameter("dataString")!=null){
    				inputJson = request.getParameter("dataString");
    				
    				JSONObject jsonObject = JSONObject.fromObject(inputJson);   
    				JSONArray dedlGeneralListJson = jsonObject.getJSONArray("dedlGeneralList");
    				JSONArray dedlProperyListJson = jsonObject.getJSONArray("dedlProperyList");
    				JSONArray dedlElementValueListJson = jsonObject.getJSONArray("dedlElementValueList");
    				DedlGeneralBean[] dedlGeneralBeanList = null;
    				DedlPropertyBean[] dedlPropertyBeanList = null;
    				DedlElementValueBean[] dedlElementValueBeanList = null;
    												
    				if(dedlGeneralListJson!=null){
    					Object[] dedlGeneralBeanJsonList = dedlGeneralListJson.toArray();				
    					dedlGeneralBeanList = new DedlGeneralBean[dedlGeneralBeanJsonList.length];
    					for(int i=0;i<dedlGeneralBeanJsonList.length;i++){
    						dedlGeneralBeanList[i] = (DedlGeneralBean)JSONObject.toBean((JSONObject)dedlGeneralBeanJsonList[i], DedlGeneralBean.class);
    					}
    				}

    				if(dedlProperyListJson!=null){
    					Object[] dedlProperyBeanJsonList = dedlProperyListJson.toArray();				
    					dedlPropertyBeanList = new DedlPropertyBean[dedlProperyBeanJsonList.length];
    					for(int i=0;i<dedlProperyBeanJsonList.length;i++){
    						dedlPropertyBeanList[i] = (DedlPropertyBean)JSONObject.toBean((JSONObject)dedlProperyBeanJsonList[i], DedlPropertyBean.class);
    					}
    				}
    				
    				if(dedlElementValueListJson!=null){
    					Object[] dedlElementValueBeanJsonList = dedlElementValueListJson.toArray();				
    					dedlElementValueBeanList = new DedlElementValueBean[dedlElementValueBeanJsonList.length];
    					for(int i=0;i<dedlElementValueBeanJsonList.length;i++){
    						dedlElementValueBeanList[i] = (DedlElementValueBean)JSONObject.toBean((JSONObject)dedlElementValueBeanJsonList[i], DedlElementValueBean.class);
    					}
    				}

    				fileByte = getService.getConfigFile(null,Phrase.DEDL_FILE_NAME);
    				if (fileByte != null){
    					fileStr = new String(fileByte);
    					dataElementListDocument = DataElementListDocument.Factory.parse(fileStr);
    					DataElementListType dataElementListType = dataElementListDocument.getDataElementList();
    					if(dataElementListType!=null){
    						DataElementDescriptionType[] dataElementList = dataElementListType.getDataElementArray();
    						//change Data Element list
    						ArrayList newDataElementList = new ArrayList();
    						DataElementDescriptionType myDataElement = null;
    						int dataElementIndex = -1;
    						int newDataElementIndex = -1;
    						ArrayList delDataElementIndex = new ArrayList();
    						int delDataElementFlag = -1;
    						if(dataElementList!=null && dataElementList.length>0){
    							newDataElementIndex = dataElementList.length;
    							
    							for(int i=0;i<dataElementList.length;i++){ // find out which records have been deleted
    								delDataElementFlag = -1;
    								for(int j=0;j<dedlGeneralBeanList.length;j++){
    									if(dataElementList[i].getElementIdentifier()!= null && dataElementList[i].getElementIdentifier().equalsIgnoreCase(dedlGeneralBeanList[j].getElementIdentifier())){
    										delDataElementFlag = 0;
    										break;
    									}
    								}
    								if(delDataElementFlag == -1) delDataElementIndex.add(new Integer(i));
    							}
    							for(int i=0;i<dedlGeneralBeanList.length;i++){	// find out which records have been updated
    								for(int j=0;j<dataElementList.length;j++){
    									if(dataElementList[j].getElementIdentifier()!= null && dataElementList[j].getElementIdentifier().equalsIgnoreCase(dedlGeneralBeanList[i].getElementIdentifier())){
    										myDataElement = dataElementList[j];
    										dataElementIndex = j;
    									}
    								}
    								if(dataElementIndex==-1 && dataElementList.length > dedlGeneralBeanList.length){ // there are records have been deleted
    									continue;
    								}else if(dataElementIndex==-1 && dataElementList.length <= dedlGeneralBeanList.length){ // there are records have been added
    									myDataElement = DataElementDescriptionType.Factory.newInstance();
    									newDataElementIndex++;
    								}
    								
    								myDataElement.setElementIdentifier(dedlGeneralBeanList[i].getElementIdentifier());
    								myDataElement.setApplicationDomain(dedlGeneralBeanList[i].getApplicationDomain());
    								myDataElement.setElementType(dedlGeneralBeanList[i].getElementType());
    								myDataElement.setDescription(dedlGeneralBeanList[i].getDescription());
    								myDataElement.setKeywords(dedlGeneralBeanList[i].getKeywords());
    								myDataElement.setOwner(dedlGeneralBeanList[i].getOwner());
    								myDataElement.setElementLabel(dedlGeneralBeanList[i].getElementLabel());
    								myDataElement.setDefaultValue(dedlGeneralBeanList[i].getDefaultValue());
    								
    								DataConstrainType dataConstrainType = null;
    								if(myDataElement.getDataConstrains() == null){
    									dataConstrainType = myDataElement.addNewDataConstrains();
    								}else{
    									dataConstrainType = myDataElement.getDataConstrains();
    								}
    								if(dedlGeneralBeanList[i].getUpperLimit() != null && !dedlGeneralBeanList[i].getUpperLimit().equals("")){
    									if(dedlGeneralBeanList[i].getUpperLimit().matches("^[-+]?\\d*$")){
    										dataConstrainType.setUpperLimit(new BigInteger(dedlGeneralBeanList[i].getUpperLimit()));
    									}else{
    										ret = "The UpperLimit must be number.";
    										isError = true;
    										break;
    									}
    								}
    								if(dedlGeneralBeanList[i].getLowerLimit() != null && !dedlGeneralBeanList[i].getLowerLimit().equals("")){
    									if(dedlGeneralBeanList[i].getLowerLimit().matches("^[-+]?\\d*$")){
    										dataConstrainType.setLowerLimit(new BigInteger(dedlGeneralBeanList[i].getLowerLimit()));
    									}else{
    										ret = "The LowerLimit must be number.";
    										isError = true;
    										break;
    									}
    								}
    								if(dedlGeneralBeanList[i].getAllowMultiSelect() != null && !dedlGeneralBeanList[i].getAllowMultiSelect().equals("")){
    									dataConstrainType.setAllowMultiSelect(Boolean.valueOf(dedlGeneralBeanList[i].getAllowMultiSelect()).booleanValue());
    								}
    								if(dedlGeneralBeanList[i].getAdditionalValuesIndicator() != null && !dedlGeneralBeanList[i].getAdditionalValuesIndicator().equals("")){
    									dataConstrainType.setAdditionalValuesIndicator(Boolean.valueOf(dedlGeneralBeanList[i].getAdditionalValuesIndicator()).booleanValue());
    								}
    								if(dedlGeneralBeanList[i].getOptionality() != null && !dedlGeneralBeanList[i].getOptionality().equals("")){
    									dataConstrainType.setOptionality(Boolean.valueOf(dedlGeneralBeanList[i].getOptionality()).booleanValue());
    								}
    								dataConstrainType.setWildcard(dedlGeneralBeanList[i].getWildcard());
    								dataConstrainType.setFormatString(dedlGeneralBeanList[i].getFormatString());
    								dataConstrainType.setValidationRules(dedlGeneralBeanList[i].getValidationRules());
    								
    								ElementDataSourceType elementDataSourceType = null;
    								if(myDataElement.getDataSource() == null){
    									elementDataSourceType = myDataElement.addNewDataSource();;
    								}else{
    									elementDataSourceType = myDataElement.getDataSource();
    								}
    									
    								DataSourceListType.Enum dataSourceTypeEnum = DataSourceListType.Enum.forString(dedlGeneralBeanList[i].getDataSourceType());
    								elementDataSourceType.setDataSourceType(dataSourceTypeEnum);
    								elementDataSourceType.setConnectionDescriptor(dedlGeneralBeanList[i].getConnectionDescriptor());
    								elementDataSourceType.setAccessStatement(dedlGeneralBeanList[i].getAccessStatement());
    								elementDataSourceType.setParameters(dedlGeneralBeanList[i].getParameters());
    								elementDataSourceType.setTransformation(dedlGeneralBeanList[i].getTransformation());
    								
    								// change Element property list
    								if(myDataElement.getElementIdentifier()!=null && dedlPropertyBeanList.length>0 && myDataElement.getElementIdentifier().equalsIgnoreCase(dedlPropertyBeanList[0].getPropertyElementID())){
    									if(dedlPropertyBeanList.length==1 && dedlPropertyBeanList[0].getPropertyName().equalsIgnoreCase("") && dedlPropertyBeanList[0].getPropertyValue().equalsIgnoreCase("")){
    										myDataElement.setPropertyArray(null);
    									}else{
    										DataElementPropertyType[] dataElementPropertyTypeList = new DataElementPropertyType[dedlPropertyBeanList.length];
    										for(int k=0;k<dedlPropertyBeanList.length;k++){
    											dataElementPropertyTypeList[k] = DataElementPropertyType.Factory.newInstance();
    											dataElementPropertyTypeList[k].setPropertyName(dedlPropertyBeanList[k].getPropertyName());
    											if(dedlPropertyBeanList[k].getPropertyValue() != null && !dedlPropertyBeanList[k].getPropertyValue().equalsIgnoreCase("")){
    												XmlAnySimpleType propertyValue = XmlAnySimpleType.Factory.newInstance();
    												propertyValue.setStringValue(dedlPropertyBeanList[k].getPropertyValue());
    												dataElementPropertyTypeList[k].setPropertyValue(propertyValue);												
    											}else{
    												dataElementPropertyTypeList[k].setPropertyValue(null);
    											}
    										}									
    										myDataElement.setPropertyArray(dataElementPropertyTypeList);
    									}
    								}
    								
    								// change Node Services parameter list
    								if(myDataElement.getElementIdentifier()!=null && dedlElementValueBeanList.length>0 && myDataElement.getElementIdentifier().equalsIgnoreCase(dedlElementValueBeanList[0].getElementElementID())){
    									if(dedlElementValueBeanList.length==1 && dedlElementValueBeanList[0].getElementValue().equalsIgnoreCase("") && dedlElementValueBeanList[0].getElementValueLabel().equalsIgnoreCase("")){
    										myDataElement.setElementValueArray(null);	
    									}else{
    										DataElementValueType[] dataElementValueTypeList = new DataElementValueType[dedlElementValueBeanList.length];
    										for(int k=0;k<dedlElementValueBeanList.length;k++){
    											dataElementValueTypeList[k] = DataElementValueType.Factory.newInstance();
    											dataElementValueTypeList[k].setStringValue(dedlElementValueBeanList[k].getElementValue());
    											dataElementValueTypeList[k].setValueLabel(dedlElementValueBeanList[k].getElementValueLabel());
    										}
    										myDataElement.setElementValueArray(dataElementValueTypeList);									
    									}
    								}
    								if(dataElementIndex!=-1) dataElementList[dataElementIndex] = myDataElement;
    								else newDataElementList.add(myDataElement); 
    							}
    							for(int i=0;i<dataElementList.length;i++){	// delete deleted records
    								for(int j=0;j<delDataElementIndex.size();j++){
    									if(i==((Integer)delDataElementIndex.get(j)).intValue()){
    										dataElementList[i] = null;
    										break;
    									}															
    								}
    							}
    							for(int i=0;i<dataElementList.length;i++){	// create a new service list
    								if(dataElementList[i]!=null){								
    									newDataElementList.add(dataElementList[i]);
    								}															
    							}
    						}
    												
    						// get the new DataElement
    						DataElementDescriptionType[] newDataElementArray = new DataElementDescriptionType[newDataElementList.size()];
    						for(int i=0;i<newDataElementArray.length;i++){
    							newDataElementArray[i] = (DataElementDescriptionType)newDataElementList.get(i);
    						}
    						dataElementListType.setDataElementArray(newDataElementArray);
    						dataElementListDocument.setDataElementList(dataElementListType);
    					}										
    				}else ret = "Fail to get dedl file from database.";
    				//File tf = new File("c:\\tempf.xml");
    				//networkNodesDocument.save(tf);
    				if(!isError){
    					isSaved = getService.saveConfigFile(dataElementListDocument.xmlText().getBytes(),null,Phrase.DEDL_FILE_NAME);
    			        if (!isSaved) {
    			        	this.Log("GetServices.do: Unable to Save dedl Settings",Level.FATAL);
    			        	ret = "Fail to Save dedl Settings.";
    			        }else ret = "Update dedl file successfully.";
    				}else{
    		        	this.Log("GetServices.do: Unable to Save dedl Settings",Level.FATAL);					
    				}
    			}else ret = "Fail to Save dedl Settings.";			
    		}else if(act!=null && act.equalsIgnoreCase("saveServiceParamData")){
    			if(request.getParameter("dataString")!=null){
//    				inputList = request.getParameter("dataString").split(",");
//    			    nodeIdentifier = inputList[0];
//    				nodeName = inputList[1];
//    				nodeAddress = inputList[2];
//    				organizationIdentifier = inputList[3];
//    				nodeContact = inputList[4];
//    				nodeVersionIdentifier = inputList[5];
//    				nodeDeploymentTypeCode = inputList[6];
//    				nodeStatus = inputList[7];
//    				north = inputList[8];
//    				south = inputList[9];
//    				east = inputList[10];
//    				west = inputList[11];

    		        isSaved = getService.SaveGetServices(nodeIdentifier,nodeName,nodeAddress,organizationIdentifier,
    		        		nodeContact,nodeVersionIdentifier,nodeDeploymentTypeCode,nodeStatus,nodePropertyName,
    		        		nodePropertyValue,north,south,east,west);
    		        if (!isSaved) {
    		        	this.Log("Configurations.do: Unable to Save Configurations Settings",Level.WARN);
    		        }
    		        else ret = "Update parameter data successfully.";				
    			}			
    		}else if(act!=null && act.equalsIgnoreCase("uploadDedl")){ // WI 34032
    			bean.setMessage("");
    			return mapping.findForward("uploadDedl");			
    		}else if(act!=null && act.equalsIgnoreCase("saveUploadDedl")){
    	        file = bean.getUploadFile();
    	        if (file != null && !file.getFileName().trim().equals("")){
    	        	fileByte = file.getFileData();
    				isSaved = getService.saveConfigFile(fileByte,null,Phrase.DEDL_FILE_NAME);
    		        if (!isSaved) {
    		        	this.Log("GetServices.do: Unable to Save Dedl file.",Level.FATAL);
    		        	bean.setMessage("Fail to upload file.");
    		        }
    		        else bean.setMessage("Upload file successfully.");
    		        
    	        	return mapping.findForward("uploadDedl");				
    	        }
    		}        	
        }else{
        	ret = "Only Administrator group users can change Node Registration. ";
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
	   * changePropertyListToJsonString
	   * @param aList
	   * @return String
	   */
	private String changePropertyListToJsonString(ObjectPropertyType[] aList ){
		String records = "";
		List jsonList = new ArrayList();
		for(int i=0;i<aList.length;i++){
            jsonList.add("{\"gridId\":" + i
                        +",\"nodePropertyName\":" + "\"" + (aList[i].getPropertyName()==null?"":aList[i].getPropertyName().trim()) + "\"" 
                        +",\"nodePropertyValue\":" + "\"" + (aList[i].getPropertyValue()==null?"":((XmlObjectBase)aList[i].getPropertyValue()).getStringValue().trim()) + "\"" 
            			+"}");                  		
		}
		
		JSONArray jsonArray = JSONArray.fromObject(jsonList);           
		
		Map map = new HashMap();
		map.put("results", jsonArray);
		JSONObject jsonObject = JSONObject.fromObject(map);
		records = jsonObject.toString();
		records = records.substring(records.indexOf("{") + 1);
		records = "{\"total\":"+ aList.length +","	+ records;
		
		return records;
	}
	
	  /**
	   * changeServiceListToJsonString
	   * @param aList
	   * @return String
	   */
	private String changeServiceListToJsonString(Service[] aList ){
		String records = "";
		List jsonList = new ArrayList();
		StyleSheetType[] styleSheetURLList = null;
		String styleSheetURL = "";
		for(int i=0;i<aList.length;i++){
            styleSheetURLList = aList[i].getStyleSheetURLArray();
			if(styleSheetURLList!=null && styleSheetURLList.length>0){
				for(int j=0;j<styleSheetURLList.length;j++){
					if(j==0){
						styleSheetURL = styleSheetURLList[j].getStyleSheetTypeValue();
					}else{
						styleSheetURL = styleSheetURL + ";" + styleSheetURLList[j].getStyleSheetTypeValue() ;
					}					
				}				
			}else{
				
			}
            jsonList.add("{\"gridId\":" + i
                    +",\"methodName\":" + "\"" + (aList[i].getMethodName()==null?"":aList[i].getMethodName().toString().trim()) + "\"" 
                    +",\"dataFlow\":" + "\"" + (aList[i].getDataflow()==null?"":aList[i].getDataflow().trim()) + "\"" 
                    +",\"serviceIdentifier\":" + "\"" + (aList[i].getServiceIdentifier()==null?"":aList[i].getServiceIdentifier().trim()) + "\"" 
                    +",\"serviceDescription\":" + "\"" + (aList[i].getServiceDescription()==null?"":aList[i].getServiceDescription().trim()) + "\"" 
                    +",\"serviceDocumentURL\":" + "\"" + (aList[i].getServiceDocumentURL()==null?"":aList[i].getServiceDocumentURL().trim()) + "\"" 
                    +",\"styleSheetURL\":" + "\"" + (styleSheetURL==null?"":styleSheetURL.trim()) + "\"" 
        			+"}");                  		
		}
		
		JSONArray jsonArray = JSONArray.fromObject(jsonList);           
		
		Map map = new HashMap();
		map.put("results", jsonArray);
		JSONObject jsonObject = JSONObject.fromObject(map);
		records = jsonObject.toString();
		records = records.substring(records.indexOf("{") + 1);
		records = "{\"total\":"+ aList.length +","	+ records;
		
		return records;
	}

	  /**
	   * changeServicePropertyListToJsonString
	   * @param aList
	   * @param serviceDataFlow
	   * @return String
	   */
	private String changeServicePropertyListToJsonString(ObjectPropertyType[] aList, String serviceDataFlow ){
		String records = "";
		List jsonList = new ArrayList();
		for(int i=0;i<aList.length;i++){
            jsonList.add("{\"gridId\":" + i
            			+",\"nodeServicesPropertyDataFlow\":" + "\"" + (serviceDataFlow==null?"":serviceDataFlow.trim()) + "\"" 
                        +",\"nodeServicesPropertyName\":" + "\"" + (aList[i].getPropertyName()==null?"":aList[i].getPropertyName().trim()) + "\"" 
                        +",\"nodeServicesPropertyValue\":" + "\"" + (aList[i].getPropertyValue()==null?"":((XmlObjectBase)aList[i].getPropertyValue()).getStringValue().trim()) + "\"" 
            			+"}");                  		
		}
		
		JSONArray jsonArray = JSONArray.fromObject(jsonList);           
		
		Map map = new HashMap();
		map.put("results", jsonArray);
		JSONObject jsonObject = JSONObject.fromObject(map);
		records = jsonObject.toString();
		records = records.substring(records.indexOf("{") + 1);
		records = "{\"total\":"+ aList.length +","	+ records;
		
		return records;
	}

	  /**
	   * changeServiceParameterListToJsonString
	   * @param aList
	   * @param serviceDataFlow
	   * @return String
	   */
	private String changeServiceParameterListToJsonString(RequestParameterType[] aList, String serviceDataFlow){
		String records = "";
		List jsonList = new ArrayList();
		for(int i=0;i<aList.length;i++){
            jsonList.add("{\"gridId\":" + i
            			+",\"nodeServicesParametersDataFlow\":" + "\"" + (serviceDataFlow==null?"":serviceDataFlow.trim()) + "\"" 
                        +",\"nodeServicesParametersName\":" + "\"" + (aList[i].getParameterName()==null?"":aList[i].getParameterName().trim()) + "\"" 
                        +",\"nodeServicesParametersValue\":" + "\"" + (aList[i].getStringValue()==null?"":aList[i].getStringValue().trim()) + "\"" 
                        +",\"nodeServicesParametersSortIndex\":" + "\"" + (aList[i].getParameterSortIndex()==null ?"0":aList[i].getParameterSortIndex().toString().trim()) + "\"" 
                        +",\"nodeServicesParametersOccurenceNo\":" + "\"" + (aList[i].getParameterOccurrenceNumber()==null?"":aList[i].getParameterOccurrenceNumber().toString().trim()) + "\"" 
                        +",\"nodeServicesParametersEncoding\":" + "\"" + (aList[i].getParameterEncoding()==null?"":aList[i].getParameterEncoding().toString().trim()) + "\"" 
                        +",\"nodeServicesParametersType\":" + "\"" + (aList[i].getParameterType()==null?"":aList[i].getParameterType().trim()) + "\"" 
                        +",\"nodeServicesParametersTypeDesc\":" + "\"" + (aList[i].getParameterTypeDescriptor()==null?"":aList[i].getParameterTypeDescriptor().trim()) + "\"" 
                        +",\"nodeServicesParametersRequired\":" + "\"" + aList[i].getParameterRequiredIndicator() + "\"" 
            			+"}");                  		
		}
		
		JSONArray jsonArray = JSONArray.fromObject(jsonList);           
		
		Map map = new HashMap();
		map.put("results", jsonArray);
		JSONObject jsonObject = JSONObject.fromObject(map);
		records = jsonObject.toString();
		records = records.substring(records.indexOf("{") + 1);
		records = "{\"total\":"+ aList.length +","	+ records;
		
		return records;
	}

	// WI 21296
	  /**
	   * changeDataElementListToJsonString
	   * @param aList
	   * @return String
	   */
	private String changeDataElementListToJsonString(DataElementDescriptionType[] aList ){
		String records = "";
		List jsonList = new ArrayList();
		for(int i=0;i<aList.length;i++){
          jsonList.add("{\"gridId\":" + i
                  +",\"elementIdentifier\":" + "\"" + (aList[i].getElementIdentifier()==null?"":aList[i].getElementIdentifier().toString().trim()) + "\"" 
                  +",\"applicationDomain\":" + "\"" + (aList[i].getApplicationDomain()==null?"":aList[i].getApplicationDomain().trim()) + "\"" 
                  +",\"elementType\":" + "\"" + (aList[i].getElementType()==null?"":aList[i].getElementType().trim()) + "\"" 
                  +",\"description\":" + "\"" + (aList[i].getDescription()==null?"":aList[i].getDescription().trim()) + "\"" 
                  +",\"keywords\":" + "\"" + (aList[i].getKeywords()==null?"":aList[i].getKeywords().trim()) + "\"" 
                  +",\"owner\":" + "\"" + (aList[i].getOwner()==null?"":aList[i].getOwner().trim()) + "\"" 
                  +",\"elementLabel\":" + "\"" + (aList[i].getElementLabel()==null?"":aList[i].getElementLabel().trim()) + "\"" 
                  +",\"defaultValue\":" + "\"" + (aList[i].getDefaultValue()==null?"":aList[i].getDefaultValue().trim()) + "\"" 
                  +",\"upperLimit\":" + "\"" + ((aList[i].getDataConstrains()==null) || (aList[i].getDataConstrains()!=null && aList[i].getDataConstrains().getUpperLimit()==null)?"":aList[i].getDataConstrains().getUpperLimit()+"".trim()) + "\"" 
                  +",\"lowerLimit\":" + "\"" + ((aList[i].getDataConstrains()==null) || (aList[i].getDataConstrains()!=null && aList[i].getDataConstrains().getLowerLimit()==null)?"":aList[i].getDataConstrains().getLowerLimit()+"".trim()) + "\"" 
                  +",\"allowMultiSelect\":" + "\"" + (aList[i].getDataConstrains()!=null && aList[i].getDataConstrains().getAllowMultiSelect()) + "\"" 
                  +",\"additionalValuesIndicator\":" + "\"" + (aList[i].getDataConstrains()!=null && aList[i].getDataConstrains().getAdditionalValuesIndicator()) + "\"" 
                  +",\"optionality\":" + "\"" + (aList[i].getDataConstrains()!=null && aList[i].getDataConstrains().getOptionality()) + "\"" 
                  +",\"wildcard\":" + "\"" + ((aList[i].getDataConstrains()==null) || (aList[i].getDataConstrains()!=null && aList[i].getDataConstrains().getWildcard()==null)?"":aList[i].getDataConstrains().getWildcard().trim()) + "\"" 
                  +",\"formatString\":" + "\"" + ((aList[i].getDataConstrains()==null) || (aList[i].getDataConstrains()!=null && aList[i].getDataConstrains().getFormatString()==null)?"":aList[i].getDataConstrains().getFormatString().trim()) + "\"" 
                  +",\"validationRules\":" + "\"" + ((aList[i].getDataConstrains()==null) || (aList[i].getDataConstrains()!=null && aList[i].getDataConstrains().getValidationRules()==null)?"":aList[i].getDataConstrains().getValidationRules().trim()) + "\"" 
                  +",\"dataSourceType\":" + "\"" + (aList[i].getDataSource()==null || aList[i].getDataSource().getDataSourceType()==null?"":aList[i].getDataSource().getDataSourceType().toString().trim()) + "\"" 
                  +",\"connectionDescriptor\":" + "\"" + (aList[i].getDataSource()==null || aList[i].getDataSource().getConnectionDescriptor()==null?"":aList[i].getDataSource().getConnectionDescriptor().trim()) + "\"" 
                  +",\"accessStatement\":" + "\"" + (aList[i].getDataSource()==null || aList[i].getDataSource().getAccessStatement()==null?"":aList[i].getDataSource().getAccessStatement().trim()) + "\"" 
                  +",\"parameters\":" + "\"" + (aList[i].getDataSource()==null || aList[i].getDataSource().getParameters()==null?"":aList[i].getDataSource().getParameters().trim()) + "\"" 
                  +",\"transformation\":" + "\"" + (aList[i].getDataSource()==null || aList[i].getDataSource().getTransformation()==null?"":aList[i].getDataSource().getTransformation().trim()) + "\"" 
       			+"}");                  		
		}
		
		JSONArray jsonArray = JSONArray.fromObject(jsonList);           
		
		Map map = new HashMap();
		map.put("results", jsonArray);
		JSONObject jsonObject = JSONObject.fromObject(map);
		records = jsonObject.toString();
		records = records.substring(records.indexOf("{") + 1);
		records = "{\"total\":"+ aList.length +","	+ records;
		
		return records;
	}
	
	// WI 21296
	  /**
	   * changeDataElementPropertyListToJsonString
	   * @param aList
	   * @return String
	   */
	private String changeDataElementPropertyListToJsonString(DataElementPropertyType[] aList, String elementID ){
	String records = "";
		List jsonList = new ArrayList();
		String propertyValue = "";
		for(int i=0;i<aList.length;i++){
			if(aList[i].getPropertyValue()!=null){
				propertyValue = aList[i].getPropertyValue().xmlText();
				propertyValue = propertyValue.substring(propertyValue.indexOf(">")+1,propertyValue.length());
				if(propertyValue != null && !propertyValue.equalsIgnoreCase("")){
					propertyValue = propertyValue.substring(0,propertyValue.indexOf("<"));					
				}
			}
			jsonList.add("{\"gridId\":" + i
	                +",\"propertyElementID\":" + "\"" + elementID.trim() + "\"" 
	                +",\"propertyName\":" + "\"" + (aList[i].getPropertyName()==null?"":aList[i].getPropertyName().trim()) + "\"" 
	                +",\"propertyValue\":" + "\"" + (propertyValue==null?"":propertyValue.trim()) + "\"" 
	     			+"}");                  		
		}
		
		JSONArray jsonArray = JSONArray.fromObject(jsonList);           
		
		Map map = new HashMap();
		map.put("results", jsonArray);
		JSONObject jsonObject = JSONObject.fromObject(map);
		records = jsonObject.toString();
		records = records.substring(records.indexOf("{") + 1);
		records = "{\"total\":"+ aList.length +","	+ records;
		
		return records;
	}

	// WI 21296
	  /**
	   * changeDataElementValueListToJsonString
	   * @param aList
	   * @return String
	   */
	private String changeDataElementValueListToJsonString(DataElementValueType[] aList, String elementID ){
	String records = "";
		List jsonList = new ArrayList();
		for(int i=0;i<aList.length;i++){
			jsonList.add("{\"gridId\":" + i
		              +",\"elementElementID\":" + "\"" + elementID.trim() + "\"" 
		              +",\"elementValue\":" + "\"" + (aList[i].getStringValue()==null?"":aList[i].getStringValue().trim()) + "\"" 
		              +",\"elementValueLabel\":" + "\"" + (aList[i].getValueLabel()==null?"":aList[i].getValueLabel().trim()) + "\"" 
   			+"}");                  		
		}
		
		JSONArray jsonArray = JSONArray.fromObject(jsonList);           
		
		Map map = new HashMap();
		map.put("results", jsonArray);
		JSONObject jsonObject = JSONObject.fromObject(map);
		records = jsonObject.toString();
		records = records.substring(records.indexOf("{") + 1);
		records = "{\"total\":"+ aList.length +","	+ records;
		
		return records;
	}
}
