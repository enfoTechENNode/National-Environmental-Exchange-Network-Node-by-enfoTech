package com.enfotech.rest.service.domain.impl;

import java.io.ByteArrayInputStream;
import java.math.BigInteger;
import java.util.ArrayList;
import java.util.Hashtable;
import java.util.Map;

import javax.xml.namespace.QName;

import net.exchangenetwork.www.schema.node._2.EncodingType;
import net.exchangenetwork.www.schema.node._2.GenericXmlType;
import net.exchangenetwork.www.schema.node._2.ParameterType;
import net.exchangenetwork.www.schema.node._2.QueryResponse;
import net.exchangenetwork.www.schema.node._2.ResultSetType;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import Node.Phrase;
import Node.Biz.Administration.Operation;
import Node.DB.DBManager;
import Node.DB.Interfaces.INodeOperation;
import Node.DB.Interfaces.Configuration.ISystemConfiguration;
import Node.Utils.Utility;

import com.enfotech.rest.dao.domain.OperationDao;
import com.enfotech.rest.service.domain.QueryService;

@Service
public class QueryServiceImpl implements QueryService{

	@Autowired
	private OperationDao operationDao;
	
	@Override
	public ArrayList<Object[]> getPublicQueryOperations() {		
		return operationDao.getPublicQueryOperations();
	}
	
	@Override
	public Operation getOperation(String opID){
		return operationDao.getOperation(opID);
	}
	
	@SuppressWarnings("unchecked")
	@Override
	public String invoke(Hashtable<String,Object> allParamHt) {
		String version = Phrase.ver_1;
		String dataflow = null;
		String serviceRequest = null;
		String rowId = null;
		String maxRows = null;
		String format = null;
		String securityToken = null;
		String[] parametersName = null;
		Object[] parametersValue = null;
		ArrayList<Hashtable<String,String>> paramList = new ArrayList<Hashtable<String,String>>();
		String result = null;
		ResultSetType resultSetType = new ResultSetType();
		QueryResponse ret = new QueryResponse();
		GenericXmlType response = null;
	    String path = Utility.GetTempFilePath() + "/temp/";
		String timeId = Utility.GetSysTimeString();
		String sourceFile = "QueryResult_"+ timeId + "." + Phrase.XML_TYPE.toLowerCase();
		String zipFile = "QueryResult_"+ timeId + "." + Phrase.ZIP_TYPE.toLowerCase();
		
		try {
			INodeOperation opDB = DBManager.GetNodeOperation(Phrase.WebServicesLoggerName);
			if(!allParamHt.isEmpty()){
				dataflow = allParamHt.get(Phrase.DATAFLOW) +"";
				serviceRequest = allParamHt.get(Phrase.REQUEST) +"";
				rowId = allParamHt.get(Phrase.ROWID) +"";
				maxRows = allParamHt.get(Phrase.MAXROWS) +"";
				format = allParamHt.get(Phrase.FORMAT) +"";
				securityToken = allParamHt.get(Phrase.SECURITY_TOKEN) +"";
				if(securityToken.isEmpty()){
					securityToken = Phrase.PUBLIC_USERS;
				}
				paramList = (ArrayList<Hashtable<String, String>>) allParamHt.get("params");
				if(paramList != null && !paramList.isEmpty()){
					parametersName = new String[paramList.size()];
					parametersValue = new Object[paramList.size()];
					int i=0;
					for(Hashtable<String,String> paramHt : paramList){
						if(paramHt!=null && !paramHt.isEmpty()){
							for(Map.Entry<String, String> param: paramHt.entrySet()){
								parametersName[i] = param.getKey();
								parametersValue[i] = param.getValue();
							}
							i++;
						}											
					}
				}
				
				if(!dataflow.isEmpty() && !serviceRequest.isEmpty() ){
				    Operation op = opDB.GetActiveOperation(serviceRequest,Phrase.WEB_METHOD_QUERY);
				    if(op != null){
					    version = op.getVersion();
					    	
					    if(version.equalsIgnoreCase(Phrase.ver_1)){
					    	Node.Biz.Handler.WebMethods.QueryHandler handler = new Node.Biz.Handler.WebMethods.QueryHandler(allParamHt.get("clientHost") +"",allParamHt.get("hostName") +"",securityToken,serviceRequest,new BigInteger(rowId),new BigInteger(maxRows),parametersValue);
					    	result = (String)handler.Invoke();
					    }else if(version.equalsIgnoreCase(Phrase.ver_2)){
							ParameterType[] parameterTypeArr = null;
							if(parametersName!=null && parametersName.length > 0){
								parameterTypeArr = new ParameterType[parametersName.length];
								for(int i=0;i<parameterTypeArr.length;i++){
									parameterTypeArr[i] = ParameterType.Factory.fromString(parametersValue[i]+"", "");
									parameterTypeArr[i].setParameterName(parametersName[i]);
									QName type = new QName("String");
									parameterTypeArr[i].setParameterType(type);
									EncodingType encodeType = EncodingType.Factory.fromValue("None");
									parameterTypeArr[i].setParameterEncoding(encodeType);
								}
							}

					    	Node2.webservice.Handler.WebMethods.QueryHandler handler = new Node2.webservice.Handler.WebMethods.QueryHandler(allParamHt.get("clientHost") +"",allParamHt.get("hostName") +"",securityToken,dataflow,serviceRequest,new BigInteger(rowId),new BigInteger(maxRows),parameterTypeArr);
					        resultSetType = (ResultSetType)handler.Invoke();
					        response = resultSetType.getResults();
							if(response != null && response.getFormat() != null && response.getFormat().getValue() != null && response.getFormat().getValue().equalsIgnoreCase(Phrase.ZIP_TYPE)){
								result = response.getExtraElement().getText();
								Utility.decode(result,path+zipFile);
								ArrayList<String> fileList = Utility.unZipFile(zipFile, path);
								if(fileList != null && fileList.size() > 0){
									for(int i=0;i<fileList.size();i++){
										result = new String(Utility.readFile(path + (String)fileList.get(i)));				
							      		Utility.delFile(path + (String)fileList.get(i));					
									}
								}
					      		Utility.delFile(path + zipFile);
							}else{
								result = response.getExtraElement().toString();							
							}
						}
				    	Utility.writeFile(path+sourceFile, new ByteArrayInputStream(result.getBytes()));
				    	result = path+sourceFile;
					    if(!Utility.isNullOrEmpty(format) && format.equalsIgnoreCase(Phrase.ZIP_TYPE)){
					    	Utility.zipFile(sourceFile, zipFile, path);
					    	result = path + zipFile;
					    	Utility.delFile(path + sourceFile);
					    }				    	
				    }else{
				    	result = "Error: Can't find operation, please check the Dataflow and Request. ";
				    }
				}
			}

		} catch (Exception e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
			result = "Error: " + e.getMessage();
		};
		return result;
	}

	@Override
	public ArrayList<String> getRestfulServiceIntroduction() {
		ArrayList<String> restServiceIntroduction = new ArrayList<String>();
		ISystemConfiguration config = null;
		try {
			config = DBManager.GetSystemConfiguration(Phrase.WebServicesLoggerName);
			if(config != null){
				restServiceIntroduction.add(config.GetRestServiceIntroductionHeader());
				restServiceIntroduction.add(config.GetRestServiceIntroductionContent());				
			}
		} catch (Exception e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		return restServiceIntroduction;
	}


}
