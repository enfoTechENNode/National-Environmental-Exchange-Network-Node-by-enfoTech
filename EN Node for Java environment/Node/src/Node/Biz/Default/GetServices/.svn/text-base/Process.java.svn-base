package Node.Biz.Default.GetServices;

import java.math.BigDecimal;
import java.math.BigInteger;
import java.rmi.RemoteException;
import java.util.ArrayList;
import java.util.Hashtable;
import java.util.Iterator;

import net.exchangenetwork.schema.ends.x2.EncodingType;
import net.exchangenetwork.schema.ends.x2.NetworkNodeListType;
import net.exchangenetwork.schema.ends.x2.NetworkNodeType;
import net.exchangenetwork.schema.ends.x2.NetworkNodesDocument;
import net.exchangenetwork.schema.ends.x2.NodeBoundingBoxType;
import net.exchangenetwork.schema.ends.x2.NodeMethodTypeCode;
import net.exchangenetwork.schema.ends.x2.ObjectPropertyType;
import net.exchangenetwork.schema.ends.x2.RequestParameterType;
import net.exchangenetwork.schema.ends.x2.ServiceDescriptionListType;
import net.exchangenetwork.schema.ends.x2.ServiceDescriptionListType.Service;

import org.apache.log4j.Level;
import org.apache.xmlbeans.impl.values.XmlObjectBase;

import Node.Phrase;
import Node.Biz.Administration.Operation;
import Node.Biz.Custom.ProcParam;
import Node.Biz.Interfaces.GetServices.IProcess;
import Node.DB.DBManager;
import Node.DB.Interfaces.INodeDomain;
import Node.DB.Interfaces.INodeOperation;
import Node.DB.Interfaces.INodeWebService;
import Node.DB.Interfaces.Configuration.IGetServices;
import Node.Utils.LoggingUtils;
import Node.Utils.Utility;
/**
 * <p>This class create GetServices Process.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class Process implements IProcess {
	  /**
	   * Constructor.
	   * @param
	   * @return 
	   */
	public Process() {
	}

	/**
	 * Execute
	 * @param token The authentication token
	 * @param serviceType The service type name
	 * @param param The process parameter 
	 * @throws RemoteException
	 * @return String[] The service name array
	 */
	public String[] Execute(String token, String serviceType, ProcParam param)
			throws RemoteException {
		if (serviceType == null)
			throw new RemoteException(Phrase.InvalidParameter);
		String[] retArray = null;
		int[] retIdArray = null;
		ArrayList retParams = null;
		Hashtable retParamH = null;
		ArrayList wsParams = null;
		try {
			if (param.GetHashtable().containsKey("version2")) { // handle 2.0
		        LoggingUtils.Log("GetServicesProcess>>> The token is:"+token+"  The serviceType is: " + serviceType, Level.DEBUG, Phrase.WebServicesLoggerName);
				if (serviceType.equalsIgnoreCase(Phrase.AllServices)) {					
	                retArray = new String[1];
	                retArray[0] = Utility.generateGetServiceFile(Phrase.ver_2);											
			        LoggingUtils.Log("GetServicesProcess>>> The retArray is:"+retArray[0], Level.DEBUG, Phrase.WebServicesLoggerName);
				}else if (serviceType.equalsIgnoreCase(Phrase.WEB_METHOD_QUERY)) {
					INodeOperation opDB = DBManager.GetNodeOperation(Phrase.WebServicesLoggerName);
					retArray = opDB.GetQueries(Phrase.ver_2);
					if(retArray != null && retArray.length != 0){
						String[] wsArr = new String[retArray.length];
						for(int i=0;i<retArray.length;i++){
							wsArr[i] = Phrase.WEB_METHOD_QUERY;
						}
						retIdArray = opDB.GetOperationIDs(wsArr, retArray);
					}
					NetworkNodesDocument networkNodesDocument = NetworkNodesDocument.Factory.newInstance();
					NetworkNodeListType networkNodeListType = NetworkNodeListType.Factory.newInstance();
					NetworkNodeType networkNodeType = networkNodeListType.addNewNetworkNodeDetails();
					ServiceDescriptionListType serviceDescriptionListType = ServiceDescriptionListType.Factory.newInstance();
					NodeMethodTypeCode nodeMethodTypeCode = NodeMethodTypeCode.Factory.newInstance();
					RequestParameterType[] requestParameterTypeList = null;
			        // WI 21296
			        IGetServices getService = DBManager.getGetServices(Phrase.WebServicesLoggerName);
					byte[] fileByte = getService.getConfigFile(Phrase.ver_2,Phrase.REGISTRATION_FILE_NAME);
					if (fileByte != null){
						String fileStr = new String(fileByte);
						NetworkNodesDocument networkNodesDocumentGeneral = NetworkNodesDocument.Factory.parse(fileStr);
						
						// Set General Data
						networkNodeType.setNodeIdentifier(networkNodesDocumentGeneral.getNetworkNodes().getNetworkNodeDetailsArray(0).getNodeIdentifier().trim());
						networkNodeType.setNodeName(networkNodesDocumentGeneral.getNetworkNodes().getNetworkNodeDetailsArray(0).getNodeIdentifier().trim());
						networkNodeType.setNodeAddress(networkNodesDocumentGeneral.getNetworkNodes().getNetworkNodeDetailsArray(0).getNodeIdentifier().trim());
						networkNodeType.setOrganizationIdentifier(networkNodesDocumentGeneral.getNetworkNodes().getNetworkNodeDetailsArray(0).getNodeIdentifier().trim());
						networkNodeType.setNodeContact(networkNodesDocumentGeneral.getNetworkNodes().getNetworkNodeDetailsArray(0).getNodeIdentifier().trim());
						net.exchangenetwork.schema.ends.x2.NodeVersionCode.Enum enumNodeVersionID = net.exchangenetwork.schema.ends.x2.NodeVersionCode.Enum.forString(networkNodesDocumentGeneral.getNetworkNodes().getNetworkNodeDetailsArray(0).getNodeVersionIdentifier().toString().trim());
						networkNodeType.setNodeVersionIdentifier(enumNodeVersionID);
						net.exchangenetwork.schema.ends.x2.NodeStageCode.Enum enumNodeDeploymentType = net.exchangenetwork.schema.ends.x2.NodeStageCode.Enum.forString(networkNodesDocumentGeneral.getNetworkNodes().getNetworkNodeDetailsArray(0).getNodeDeploymentTypeCode().toString().trim());
						networkNodeType.setNodeDeploymentTypeCode(enumNodeDeploymentType);
						net.exchangenetwork.schema.ends.x2.NodeStatusCode.Enum enumNodeStatus = net.exchangenetwork.schema.ends.x2.NodeStatusCode.Enum.forString(networkNodesDocumentGeneral.getNetworkNodes().getNetworkNodeDetailsArray(0).getNodeStatus().toString().trim());
						networkNodeType.setNodeStatus(enumNodeStatus);
						NodeBoundingBoxType nodeBoundingBoxType = NodeBoundingBoxType.Factory.newInstance();
						nodeBoundingBoxType.setBoundingCoordinateNorth(new BigDecimal(networkNodesDocumentGeneral.getNetworkNodes().getNetworkNodeDetailsArray(0).getBoundingBoxDetails().getBoundingCoordinateNorth().toString().trim()));
						nodeBoundingBoxType.setBoundingCoordinateSouth(new BigDecimal(networkNodesDocumentGeneral.getNetworkNodes().getNetworkNodeDetailsArray(0).getBoundingBoxDetails().getBoundingCoordinateSouth().toString().trim()));
						nodeBoundingBoxType.setBoundingCoordinateEast(new BigDecimal(networkNodesDocumentGeneral.getNetworkNodes().getNetworkNodeDetailsArray(0).getBoundingBoxDetails().getBoundingCoordinateEast().toString().trim()));
						nodeBoundingBoxType.setBoundingCoordinateWest(new BigDecimal(networkNodesDocumentGeneral.getNetworkNodes().getNetworkNodeDetailsArray(0).getBoundingBoxDetails().getBoundingCoordinateWest().toString().trim()));
						networkNodeType.setBoundingBoxDetails(nodeBoundingBoxType);

						// Set Node Property
						ObjectPropertyType[] objectPropertyTypeList = networkNodesDocumentGeneral.getNetworkNodes().getNetworkNodeDetailsArray(0).getNodePropertyArray();
						if(objectPropertyTypeList!=null && objectPropertyTypeList.length>0){
							networkNodeType.setNodePropertyArray(objectPropertyTypeList);
						}
					};
					
					if(retIdArray!=null){						
						for(int i=0;i<retIdArray.length;i++){
					        LoggingUtils.Log("GetServicesProcess>>> The retIdArray[i] is:"+retIdArray[i], Level.DEBUG, Phrase.WebServicesLoggerName);
							Service service = serviceDescriptionListType.addNewService();
							Operation op = opDB.GetOperation(retIdArray[i]);
							String opName = op.GetOpName();
					        LoggingUtils.Log("GetServicesProcess>>> The opName is:"+opName, Level.DEBUG, Phrase.WebServicesLoggerName);
							String wsName = op.GetWebService();
							retParams = op.getWebServiceParameters();
							requestParameterTypeList = null;
							if(retParams!=null){
								requestParameterTypeList = new RequestParameterType[retParams.size()];
								for(int j=0;j<retParams.size();j++){
									//retParamH.put(opName, retParams[j]);
									requestParameterTypeList[j] = RequestParameterType.Factory.newInstance();
									wsParams = (ArrayList)retParams.get(j);
									if(wsParams != null){
										requestParameterTypeList[j].setParameterName((String)wsParams.get(1));										
										requestParameterTypeList[j].setParameterType((String)wsParams.get(2));										
										requestParameterTypeList[j].setParameterTypeDescriptor((String)wsParams.get(3));										
										requestParameterTypeList[j].setParameterSortIndex(BigInteger.valueOf(j+1));
										if((String)wsParams.get(4) != null && !((String)wsParams.get(4)).equals("")){
											requestParameterTypeList[j].setParameterOccurrenceNumber((String)wsParams.get(4));
										}
										if((String)wsParams.get(5) != null && !((String)wsParams.get(5)).equals("")){
											EncodingType.Enum encodingTypeEnum = EncodingType.Enum.forString((String)wsParams.get(5));
											requestParameterTypeList[j].setParameterEncoding(encodingTypeEnum);
										}
										if((String)wsParams.get(6) != null && !((String)wsParams.get(6)).equals("")){
											requestParameterTypeList[j].setParameterRequiredIndicator((new Boolean((String)wsParams.get(6))).booleanValue());
										}
								        LoggingUtils.Log("GetServicesProcess>>> The retParams name is:"+requestParameterTypeList[j].getParameterName(), Level.DEBUG, Phrase.WebServicesLoggerName);											
									}									
								}
							}
							if(wsName!=null && wsName.equalsIgnoreCase(nodeMethodTypeCode.QUERY.toString().toUpperCase())){
								service.setMethodName(nodeMethodTypeCode.QUERY);									
							}else if(wsName!=null && wsName.equalsIgnoreCase(nodeMethodTypeCode.SOLICIT.toString().toUpperCase())){
								service.setMethodName(nodeMethodTypeCode.SOLICIT);																		
							}else if(wsName!=null && wsName.equalsIgnoreCase(nodeMethodTypeCode.EXECUTE.toString().toUpperCase())){
								service.setMethodName(nodeMethodTypeCode.EXECUTE);																		
							}
							service.setDataflow(op.GetDomain());
							service.setServiceIdentifier(opName);
							service.setServiceDescription(op.GetDescription()==null?"":op.GetDescription());
							service.setParameterArray(requestParameterTypeList);
					        LoggingUtils.Log("GetServicesProcess>>> The opName is:"+opName, Level.DEBUG, Phrase.WebServicesLoggerName);
						}
						networkNodeType.setNodeServiceList(serviceDescriptionListType);
					}
					// create output xml
					networkNodesDocument.setNetworkNodes(networkNodeListType);
	                retArray = new String[1];
	                retArray[0] = networkNodesDocument.toString();											
			        LoggingUtils.Log("GetServicesProcess>>> The retArray is:"+retArray[0], Level.DEBUG, Phrase.WebServicesLoggerName);
				}else if (serviceType.equalsIgnoreCase(Phrase.WEB_METHOD_SOLICIT)) {
					INodeOperation opDB = DBManager.GetNodeOperation(Phrase.WebServicesLoggerName);
					retArray = opDB.GetSolicits(Phrase.ver_2);
					if(retArray != null && retArray.length != 0){
						String[] wsArr = new String[retArray.length];
						for(int i=0;i<retArray.length;i++){
							wsArr[i] = Phrase.WEB_METHOD_SOLICIT;
						}
						retIdArray = opDB.GetOperationIDs(wsArr, retArray);
					}
					NetworkNodesDocument networkNodesDocument = NetworkNodesDocument.Factory.newInstance();
					NetworkNodeListType networkNodeListType = NetworkNodeListType.Factory.newInstance();
					NetworkNodeType networkNodeType = networkNodeListType.addNewNetworkNodeDetails();
					ServiceDescriptionListType serviceDescriptionListType = ServiceDescriptionListType.Factory.newInstance();
					NodeMethodTypeCode nodeMethodTypeCode = NodeMethodTypeCode.Factory.newInstance();
					RequestParameterType[] requestParameterTypeList = null;
			        // WI 21296
			        IGetServices getService = DBManager.getGetServices(Phrase.WebServicesLoggerName);
					byte[] fileByte = getService.getConfigFile(Phrase.ver_2,Phrase.REGISTRATION_FILE_NAME);
					if (fileByte != null){
						String fileStr = new String(fileByte);
						NetworkNodesDocument networkNodesDocumentGeneral = NetworkNodesDocument.Factory.parse(fileStr);
						
						// Set General Data
						networkNodeType.setNodeIdentifier(networkNodesDocumentGeneral.getNetworkNodes().getNetworkNodeDetailsArray(0).getNodeIdentifier().trim());
						networkNodeType.setNodeName(networkNodesDocumentGeneral.getNetworkNodes().getNetworkNodeDetailsArray(0).getNodeIdentifier().trim());
						networkNodeType.setNodeAddress(networkNodesDocumentGeneral.getNetworkNodes().getNetworkNodeDetailsArray(0).getNodeIdentifier().trim());
						networkNodeType.setOrganizationIdentifier(networkNodesDocumentGeneral.getNetworkNodes().getNetworkNodeDetailsArray(0).getNodeIdentifier().trim());
						networkNodeType.setNodeContact(networkNodesDocumentGeneral.getNetworkNodes().getNetworkNodeDetailsArray(0).getNodeIdentifier().trim());
						net.exchangenetwork.schema.ends.x2.NodeVersionCode.Enum enumNodeVersionID = net.exchangenetwork.schema.ends.x2.NodeVersionCode.Enum.forString(networkNodesDocumentGeneral.getNetworkNodes().getNetworkNodeDetailsArray(0).getNodeVersionIdentifier().toString().trim());
						networkNodeType.setNodeVersionIdentifier(enumNodeVersionID);
						net.exchangenetwork.schema.ends.x2.NodeStageCode.Enum enumNodeDeploymentType = net.exchangenetwork.schema.ends.x2.NodeStageCode.Enum.forString(networkNodesDocumentGeneral.getNetworkNodes().getNetworkNodeDetailsArray(0).getNodeDeploymentTypeCode().toString().trim());
						networkNodeType.setNodeDeploymentTypeCode(enumNodeDeploymentType);
						net.exchangenetwork.schema.ends.x2.NodeStatusCode.Enum enumNodeStatus = net.exchangenetwork.schema.ends.x2.NodeStatusCode.Enum.forString(networkNodesDocumentGeneral.getNetworkNodes().getNetworkNodeDetailsArray(0).getNodeStatus().toString().trim());
						networkNodeType.setNodeStatus(enumNodeStatus);
						NodeBoundingBoxType nodeBoundingBoxType = NodeBoundingBoxType.Factory.newInstance();
						nodeBoundingBoxType.setBoundingCoordinateNorth(new BigDecimal(networkNodesDocumentGeneral.getNetworkNodes().getNetworkNodeDetailsArray(0).getBoundingBoxDetails().getBoundingCoordinateNorth().toString().trim()));
						nodeBoundingBoxType.setBoundingCoordinateSouth(new BigDecimal(networkNodesDocumentGeneral.getNetworkNodes().getNetworkNodeDetailsArray(0).getBoundingBoxDetails().getBoundingCoordinateSouth().toString().trim()));
						nodeBoundingBoxType.setBoundingCoordinateEast(new BigDecimal(networkNodesDocumentGeneral.getNetworkNodes().getNetworkNodeDetailsArray(0).getBoundingBoxDetails().getBoundingCoordinateEast().toString().trim()));
						nodeBoundingBoxType.setBoundingCoordinateWest(new BigDecimal(networkNodesDocumentGeneral.getNetworkNodes().getNetworkNodeDetailsArray(0).getBoundingBoxDetails().getBoundingCoordinateWest().toString().trim()));
						networkNodeType.setBoundingBoxDetails(nodeBoundingBoxType);

						// Set Node Property
						ObjectPropertyType[] objectPropertyTypeList = networkNodesDocumentGeneral.getNetworkNodes().getNetworkNodeDetailsArray(0).getNodePropertyArray();
						if(objectPropertyTypeList!=null && objectPropertyTypeList.length>0){
							networkNodeType.setNodePropertyArray(objectPropertyTypeList);
						}
					};
					
					if(retIdArray!=null){						
						for(int i=0;i<retIdArray.length;i++){
					        LoggingUtils.Log("GetServicesProcess>>> The retIdArray[i] is:"+retIdArray[i], Level.DEBUG, Phrase.WebServicesLoggerName);
							Service service = serviceDescriptionListType.addNewService();
							Operation op = opDB.GetOperation(retIdArray[i]);
							String opName = op.GetOpName();
					        LoggingUtils.Log("GetServicesProcess>>> The opName is:"+opName, Level.DEBUG, Phrase.WebServicesLoggerName);
							String wsName = op.GetWebService();
							retParams = op.getWebServiceParameters();
							requestParameterTypeList = null;
							if(retParams!=null){
								requestParameterTypeList = new RequestParameterType[retParams.size()];
								for(int j=0;j<retParams.size();j++){
									//retParamH.put(opName, retParams[j]);
									requestParameterTypeList[j] = RequestParameterType.Factory.newInstance();
									wsParams = (ArrayList)retParams.get(j);
									if(wsParams != null){
										requestParameterTypeList[j].setParameterName((String)wsParams.get(1));										
										requestParameterTypeList[j].setParameterType((String)wsParams.get(2));										
										requestParameterTypeList[j].setParameterTypeDescriptor((String)wsParams.get(3));										
										requestParameterTypeList[j].setParameterSortIndex(BigInteger.valueOf(j+1));
										if((String)wsParams.get(4) != null && !((String)wsParams.get(4)).equals("")){
											requestParameterTypeList[j].setParameterOccurrenceNumber((String)wsParams.get(4));
										}
										if((String)wsParams.get(5) != null && !((String)wsParams.get(5)).equals("")){
											EncodingType.Enum encodingTypeEnum = EncodingType.Enum.forString((String)wsParams.get(5));
											requestParameterTypeList[j].setParameterEncoding(encodingTypeEnum);
										}
										if((String)wsParams.get(6) != null && !((String)wsParams.get(6)).equals("")){
											requestParameterTypeList[j].setParameterRequiredIndicator((new Boolean((String)wsParams.get(6))).booleanValue());
										}
								        LoggingUtils.Log("GetServicesProcess>>> The retParams name is:"+requestParameterTypeList[j].getParameterName(), Level.DEBUG, Phrase.WebServicesLoggerName);											
									}									
								}
							}
							if(wsName!=null && wsName.equalsIgnoreCase(nodeMethodTypeCode.QUERY.toString().toUpperCase())){
								service.setMethodName(nodeMethodTypeCode.QUERY);									
							}else if(wsName!=null && wsName.equalsIgnoreCase(nodeMethodTypeCode.SOLICIT.toString().toUpperCase())){
								service.setMethodName(nodeMethodTypeCode.SOLICIT);																		
							}else if(wsName!=null && wsName.equalsIgnoreCase(nodeMethodTypeCode.EXECUTE.toString().toUpperCase())){
								service.setMethodName(nodeMethodTypeCode.EXECUTE);																		
							}
							service.setDataflow(op.GetDomain());
							service.setServiceIdentifier(opName);
							service.setServiceDescription(op.GetDescription()==null?"":op.GetDescription());
							service.setParameterArray(requestParameterTypeList);
					        LoggingUtils.Log("GetServicesProcess>>> The opName is:"+opName, Level.DEBUG, Phrase.WebServicesLoggerName);
						}
						networkNodeType.setNodeServiceList(serviceDescriptionListType);
					}
					// create output xml
					networkNodesDocument.setNetworkNodes(networkNodeListType);
	                retArray = new String[1];
	                retArray[0] = networkNodesDocument.toString();											
			        LoggingUtils.Log("GetServicesProcess>>> The retArray is:"+retArray[0], Level.DEBUG, Phrase.WebServicesLoggerName);
				}else if (serviceType.equalsIgnoreCase(Phrase.WEB_METHOD_EXECUTE)) {
					INodeOperation opDB = DBManager.GetNodeOperation(Phrase.WebServicesLoggerName);
					retArray = opDB.GetExecutes(Phrase.ver_2);
					if(retArray != null && retArray.length != 0){
						String[] wsArr = new String[retArray.length];
						for(int i=0;i<retArray.length;i++){
							wsArr[i] = Phrase.WEB_METHOD_EXECUTE;
						}
						retIdArray = opDB.GetOperationIDs(wsArr, retArray);						
					}
					NetworkNodesDocument networkNodesDocument = NetworkNodesDocument.Factory.newInstance();
					NetworkNodeListType networkNodeListType = NetworkNodeListType.Factory.newInstance();
					NetworkNodeType networkNodeType = networkNodeListType.addNewNetworkNodeDetails();
					ServiceDescriptionListType serviceDescriptionListType = ServiceDescriptionListType.Factory.newInstance();
					NodeMethodTypeCode nodeMethodTypeCode = NodeMethodTypeCode.Factory.newInstance();
					RequestParameterType[] requestParameterTypeList = null;
			        // WI 21296
			        IGetServices getService = DBManager.getGetServices(Phrase.WebServicesLoggerName);
					byte[] fileByte = getService.getConfigFile(Phrase.ver_2,Phrase.REGISTRATION_FILE_NAME);
					if (fileByte != null){
						String fileStr = new String(fileByte);
						NetworkNodesDocument networkNodesDocumentGeneral = NetworkNodesDocument.Factory.parse(fileStr);
						
						// Set General Data
						networkNodeType.setNodeIdentifier(networkNodesDocumentGeneral.getNetworkNodes().getNetworkNodeDetailsArray(0).getNodeIdentifier().trim());
						networkNodeType.setNodeName(networkNodesDocumentGeneral.getNetworkNodes().getNetworkNodeDetailsArray(0).getNodeIdentifier().trim());
						networkNodeType.setNodeAddress(networkNodesDocumentGeneral.getNetworkNodes().getNetworkNodeDetailsArray(0).getNodeIdentifier().trim());
						networkNodeType.setOrganizationIdentifier(networkNodesDocumentGeneral.getNetworkNodes().getNetworkNodeDetailsArray(0).getNodeIdentifier().trim());
						networkNodeType.setNodeContact(networkNodesDocumentGeneral.getNetworkNodes().getNetworkNodeDetailsArray(0).getNodeIdentifier().trim());
						net.exchangenetwork.schema.ends.x2.NodeVersionCode.Enum enumNodeVersionID = net.exchangenetwork.schema.ends.x2.NodeVersionCode.Enum.forString(networkNodesDocumentGeneral.getNetworkNodes().getNetworkNodeDetailsArray(0).getNodeVersionIdentifier().toString().trim());
						networkNodeType.setNodeVersionIdentifier(enumNodeVersionID);
						net.exchangenetwork.schema.ends.x2.NodeStageCode.Enum enumNodeDeploymentType = net.exchangenetwork.schema.ends.x2.NodeStageCode.Enum.forString(networkNodesDocumentGeneral.getNetworkNodes().getNetworkNodeDetailsArray(0).getNodeDeploymentTypeCode().toString().trim());
						networkNodeType.setNodeDeploymentTypeCode(enumNodeDeploymentType);
						net.exchangenetwork.schema.ends.x2.NodeStatusCode.Enum enumNodeStatus = net.exchangenetwork.schema.ends.x2.NodeStatusCode.Enum.forString(networkNodesDocumentGeneral.getNetworkNodes().getNetworkNodeDetailsArray(0).getNodeStatus().toString().trim());
						networkNodeType.setNodeStatus(enumNodeStatus);
						NodeBoundingBoxType nodeBoundingBoxType = NodeBoundingBoxType.Factory.newInstance();
						nodeBoundingBoxType.setBoundingCoordinateNorth(new BigDecimal(networkNodesDocumentGeneral.getNetworkNodes().getNetworkNodeDetailsArray(0).getBoundingBoxDetails().getBoundingCoordinateNorth().toString().trim()));
						nodeBoundingBoxType.setBoundingCoordinateSouth(new BigDecimal(networkNodesDocumentGeneral.getNetworkNodes().getNetworkNodeDetailsArray(0).getBoundingBoxDetails().getBoundingCoordinateSouth().toString().trim()));
						nodeBoundingBoxType.setBoundingCoordinateEast(new BigDecimal(networkNodesDocumentGeneral.getNetworkNodes().getNetworkNodeDetailsArray(0).getBoundingBoxDetails().getBoundingCoordinateEast().toString().trim()));
						nodeBoundingBoxType.setBoundingCoordinateWest(new BigDecimal(networkNodesDocumentGeneral.getNetworkNodes().getNetworkNodeDetailsArray(0).getBoundingBoxDetails().getBoundingCoordinateWest().toString().trim()));
						networkNodeType.setBoundingBoxDetails(nodeBoundingBoxType);

						// Set Node Property
						ObjectPropertyType[] objectPropertyTypeList = networkNodesDocumentGeneral.getNetworkNodes().getNetworkNodeDetailsArray(0).getNodePropertyArray();
						if(objectPropertyTypeList!=null && objectPropertyTypeList.length>0){
							networkNodeType.setNodePropertyArray(objectPropertyTypeList);
						}
					};
					
					if(retIdArray!=null){						
						for(int i=0;i<retIdArray.length;i++){
					        LoggingUtils.Log("GetServicesProcess>>> The retIdArray[i] is:"+retIdArray[i], Level.DEBUG, Phrase.WebServicesLoggerName);
							Service service = serviceDescriptionListType.addNewService();
							Operation op = opDB.GetOperation(retIdArray[i]);
							String opName = op.GetOpName();
					        LoggingUtils.Log("GetServicesProcess>>> The opName is:"+opName, Level.DEBUG, Phrase.WebServicesLoggerName);
							String wsName = op.GetWebService();
							retParams = op.getWebServiceParameters();
							requestParameterTypeList = null;
							if(retParams!=null){
								requestParameterTypeList = new RequestParameterType[retParams.size()];
								for(int j=0;j<retParams.size();j++){
									//retParamH.put(opName, retParams[j]);
									requestParameterTypeList[j] = RequestParameterType.Factory.newInstance();
									wsParams = (ArrayList)retParams.get(j);
									if(wsParams != null){
										requestParameterTypeList[j].setParameterName((String)wsParams.get(1));										
										requestParameterTypeList[j].setParameterType((String)wsParams.get(2));										
										requestParameterTypeList[j].setParameterTypeDescriptor((String)wsParams.get(3));										
										requestParameterTypeList[j].setParameterSortIndex(BigInteger.valueOf(j+1));
										if((String)wsParams.get(4) != null && !((String)wsParams.get(4)).equals("")){
											requestParameterTypeList[j].setParameterOccurrenceNumber((String)wsParams.get(4));
										}
										if((String)wsParams.get(5) != null && !((String)wsParams.get(5)).equals("")){
											EncodingType.Enum encodingTypeEnum = EncodingType.Enum.forString((String)wsParams.get(5));
											requestParameterTypeList[j].setParameterEncoding(encodingTypeEnum);
										}
										if((String)wsParams.get(6) != null && !((String)wsParams.get(6)).equals("")){
											requestParameterTypeList[j].setParameterRequiredIndicator((new Boolean((String)wsParams.get(6))).booleanValue());
										}
								        LoggingUtils.Log("GetServicesProcess>>> The retParams name is:"+requestParameterTypeList[j].getParameterName(), Level.DEBUG, Phrase.WebServicesLoggerName);											
									}									
								}
							}
							if(wsName!=null && wsName.equalsIgnoreCase(nodeMethodTypeCode.QUERY.toString().toUpperCase())){
								service.setMethodName(nodeMethodTypeCode.QUERY);									
							}else if(wsName!=null && wsName.equalsIgnoreCase(nodeMethodTypeCode.SOLICIT.toString().toUpperCase())){
								service.setMethodName(nodeMethodTypeCode.SOLICIT);																		
							}else if(wsName!=null && wsName.equalsIgnoreCase(nodeMethodTypeCode.EXECUTE.toString().toUpperCase())){
								service.setMethodName(nodeMethodTypeCode.EXECUTE);																		
							}
							service.setDataflow(op.GetDomain());
							service.setServiceIdentifier(opName);
							service.setServiceDescription(op.GetDescription()==null?"":op.GetDescription());
							service.setParameterArray(requestParameterTypeList);
					        LoggingUtils.Log("GetServicesProcess>>> The opName is:"+opName, Level.DEBUG, Phrase.WebServicesLoggerName);
						}
						networkNodeType.setNodeServiceList(serviceDescriptionListType);
					}
					// create output xml
					networkNodesDocument.setNetworkNodes(networkNodeListType);
	                retArray = new String[1];
	                retArray[0] = networkNodesDocument.toString();											
			        LoggingUtils.Log("GetServicesProcess>>> The retArray is:"+retArray[0], Level.DEBUG, Phrase.WebServicesLoggerName);
				}
			} else { // handle 1.1
				if (serviceType.equalsIgnoreCase(Phrase.ServiceType))
					retArray = Phrase.SERVICE_TYPES;
				else if (serviceType.equalsIgnoreCase(Phrase.Interfaces)) {
					INodeWebService wsDB = DBManager.GetNodeWebService(Phrase.WebServicesLoggerName);
					retArray = wsDB.GetWSList();
				} else if (serviceType.equalsIgnoreCase(Phrase.Queries)) {
					INodeOperation opDB = DBManager.GetNodeOperation(Phrase.WebServicesLoggerName);
					retArray = opDB.GetQueries(Phrase.ver_1);
				} else if (serviceType.equalsIgnoreCase(Phrase.WEB_METHOD_SOLICIT)) {
					INodeOperation opDB = DBManager.GetNodeOperation(Phrase.WebServicesLoggerName);
					retArray = opDB.GetSolicits(Phrase.ver_1);
				} else if (serviceType.equalsIgnoreCase(Phrase.Domains)) {
					INodeDomain domainDB = DBManager.GetNodeDomain(Phrase.WebServicesLoggerName);
					retArray = domainDB.GetDomains();
				} else {
					INodeDomain domainDB = DBManager.GetNodeDomain(Phrase.WebServicesLoggerName);
					retArray = domainDB.GetOperations(serviceType);
					if (retArray == null)
						throw new RemoteException(Phrase.InvalidParameter);
				}
			}
		} catch (RemoteException e) {
			throw e;
		} catch (Exception e) {
			throw new RemoteException(Phrase.InternalError);
		}
        LoggingUtils.Log("GetServicesProcess>>> Before return.", Level.DEBUG, Phrase.WebServicesLoggerName);
		return retArray;
	}
}
