package com.enfotech.rest.webservice;

import java.io.ByteArrayInputStream;
import java.io.File;
import java.io.FileInputStream;
import java.io.IOException;
import java.net.InetAddress;
import java.net.UnknownHostException;
import java.sql.Date;
import java.util.ArrayList;
import java.util.Hashtable;
import java.util.Map;
import java.util.TreeMap;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.ui.ModelMap;
import org.springframework.util.FileCopyUtils;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.ResponseBody;
import org.springframework.web.servlet.ModelAndView;

import Node.Phrase;
import Node.Biz.Administration.Operation;
import Node.Biz.Administration.Schedule;
import Node.DB.DBManager;
import Node.DB.Interfaces.INodeOperation;
import Node.Utils.Utility;

import com.enfotech.rest.service.domain.QueryService;

/**
 * Handles the input rest request and return result base on different request
 * type / format
 */
@Controller
public class RestHomeController {
	private boolean isDebug = true;
	@Autowired
	private QueryService queryService;
	
	@RequestMapping(value = "/index", method = RequestMethod.GET)
	public ModelAndView index(HttpServletRequest request, HttpServletResponse response) {
		ModelAndView ret = new ModelAndView();
		// Get Applications in all domain and return
		ArrayList<Object[]> operationLst = queryService.getPublicQueryOperations();
		ArrayList<String> introduction = queryService.getRestfulServiceIntroduction();
//		ArrayList<Object[]> operationLst = new ArrayList<Object[]>();
//		Object[] objArr1 = {10,"AQDERawdata_v2","AQDE"};
//		Object[] objArr2 = {1,"AQSMonitoring_v2","AQDE"};
//		Object[] objArr3 = {12,"WQXv1","WQX"};
//		operationLst.add(objArr1);
//		operationLst.add(objArr2);
//		operationLst.add(objArr3);
		
		TreeMap<String,ArrayList<Hashtable<String,String>>> domainHt = new TreeMap<String,ArrayList<Hashtable<String,String>>>();
		ArrayList<Hashtable<String,String>> opList = null;
		if(operationLst != null && !operationLst.isEmpty()){
			for(Object[] opObjArr : operationLst){
				if(opObjArr.length > 0){
					Hashtable<String,String> opHt = new Hashtable<String,String>();
					opHt.put(opObjArr[0]+"", opObjArr[1]+"");
					if(domainHt.containsKey(opObjArr[2]+"")){
						opList = domainHt.get(opObjArr[2]+"");
					}else{
						opList = new ArrayList<Hashtable<String,String>>();
					}
					opList.add(opHt);
					domainHt.put(opObjArr[2]+"", opList);			
				}
			}
		}
		ret.addObject("domainHt",domainHt);
		if(introduction == null) {
			introduction = new ArrayList<String>();
			introduction.add("");
			introduction.add("");
		}
		ret.addObject("introduction",introduction.toArray(new String[introduction.size()]));
		ret.setViewName("index");
		return ret;
	}

	@SuppressWarnings({ "unchecked" })
	@RequestMapping(value = "/getOperation", method = RequestMethod.GET)
	public @ResponseBody OpJson getOperation(HttpServletRequest request, HttpServletResponse response) {
		Map<String, String[]> paramMp = request.getParameterMap();
		// Get Operation
        Operation op = null;
        
		if(!paramMp.isEmpty()){
			for (Map.Entry<String, String[]> entry : paramMp.entrySet()) {
				if(entry.getKey().equalsIgnoreCase(Phrase.OPID)){
					op = this.queryService.getOperation(entry.getValue()[0]);
					break;
				}
			}
		}
		OpJson opJson = new OpJson();
		if(op != null){
			opJson.setDescription(op.GetDescription());
			opJson.setDomain(op.GetDomain());
			opJson.setMessage(op.GetMessage());
			opJson.setOpName(op.GetOpName());
			opJson.setVersion(op.getVersion());
			opJson.setWebService(op.GetWebService());
			opJson.setWebServiceParameters(op.getWebServiceParameters());
		}
		return opJson;
				
	}

	/**
	 * Handles the input rest request and return query result base on different
	 * format
	 */
	@SuppressWarnings("unchecked")
	@RequestMapping(value = "/Query", method = RequestMethod.GET)
	public void query(HttpServletRequest request, HttpServletResponse response) {
		ModelAndView ret = new ModelAndView();
		Map<String, String[]> paramMp = request.getParameterMap();
		String paramsString = null;
		String[] paramsArr = null;
		String[] paramArr = null;
		ArrayList<Hashtable<String,String>> paramList = new ArrayList<Hashtable<String,String>>();
		Hashtable<String,Object> allParamHt = new Hashtable<String,Object>();
	    String clientHost = Utility.getIpFromRequest(request);
	    if(clientHost == null) clientHost = "000.000.000.000";
	    String hostName = "";
	    String path= Utility.GetTempFilePath()+"/temp/";
	    String fileName = "QueryResult_"+Utility.GetSysTimeString()+"."+Phrase.XML_TYPE;
	    boolean isError = false;
	    
	    try {
	    	hostName = InetAddress.getLocalHost().getHostName();
		} catch (UnknownHostException e1) {
			// TODO Auto-generated catch block
			e1.printStackTrace();
		}
	    String resultFilePath = null; 

		// Get parameters
		if(!paramMp.isEmpty()){
			allParamHt.put(Phrase.SECURITY_TOKEN, "");
			for (Map.Entry<String, String[]> entry : paramMp.entrySet()) {
				if(entry.getKey().equalsIgnoreCase(Phrase.DATAFLOW)){
					allParamHt.put(Phrase.DATAFLOW, entry.getValue()[0]);
				}else if(entry.getKey().equalsIgnoreCase(Phrase.REQUEST)){
					allParamHt.put(Phrase.REQUEST, entry.getValue()[0]);					
				}else if(entry.getKey().equalsIgnoreCase(Phrase.PARAMS)){
					paramsString = entry.getValue()[0];
					if(!paramsString.isEmpty()){
						paramsArr = paramsString.split(";");
						if(paramsArr.length > 0){
							for(String param : paramsArr){
								paramArr = param.split("\\|");
								Hashtable<String,String> paramHt = new Hashtable<String,String>();
								if(paramArr.length > 1){
									paramHt.put(paramArr[0], paramArr[1]);									
								}else{
									paramHt.put(paramArr[0],"");
								}
								paramList.add(paramHt);
							}
						}
					}
					allParamHt.put(Phrase.PARAMS, paramList);					
				}else if(entry.getKey().equalsIgnoreCase(Phrase.ROWID)){
					allParamHt.put(Phrase.ROWID, entry.getValue()[0]);					
				}else if(entry.getKey().equalsIgnoreCase(Phrase.MAXROWS)){
					allParamHt.put(Phrase.MAXROWS, entry.getValue()[0]);					
				}else if(entry.getKey().equalsIgnoreCase(Phrase.FORMAT)){
					allParamHt.put(Phrase.FORMAT, entry.getValue()[0]);					
				}else if(entry.getKey().equalsIgnoreCase(Phrase.SECURITY_TOKEN)){
					allParamHt.put(Phrase.SECURITY_TOKEN, entry.getValue()[0]);					
				}
				if(isDebug) System.out.println("key,val: " + entry.getKey() + "," + entry.getValue()[0]);				
				
			}
			
			allParamHt.put("clientHost", clientHost);
			allParamHt.put("hostName", hostName);
			allParamHt.put(Phrase.PARAMS, paramList);
			
			// Call query handler based on version
			resultFilePath = this.queryService.invoke(allParamHt);
			
			if(!Utility.isNullOrEmpty(resultFilePath)){
				
				if(resultFilePath.contains("Error")){
					//ret.addObject("errMsg",resultFilePath);
					//ret.setViewName("error");
					try {
						if(resultFilePath.contains("exception")){
							resultFilePath = "Error: error occurred while processing your RESTful request.  Please contact the data provider for further assistance.";
						}
						Utility.writeFile(path+fileName, new ByteArrayInputStream(resultFilePath.getBytes()));
						// Return value 
						resultFilePath = path+fileName;
						isError = true;
					} catch (Exception e) {
						// TODO Auto-generated catch block
						e.printStackTrace();
					}
				}
					// Return value 
				File file = new File(resultFilePath);					
				
				fileName = resultFilePath.substring(resultFilePath.lastIndexOf("/")+1);
				response.setContentLength(new Long(file.length()).intValue());
				if(isError){ // Error return html message
					response.setContentType(Phrase.MIME_HTML);
				}else{
					if(allParamHt.get(Phrase.FORMAT)!=null && (allParamHt.get(Phrase.FORMAT)+"").equalsIgnoreCase(Phrase.ZIP_TYPE)){
						response.setContentType(Phrase.MIME_ZIP); 
					}else{
						response.setContentType(Phrase.MIME_XML); 
					}					
					response.setHeader("Content-Disposition","attachment; filename="+fileName+"");			
				}
				try {
					FileCopyUtils.copy(new FileInputStream(file),response.getOutputStream());
				} catch (IOException e) {
					e.printStackTrace();
				}finally{
			    	Utility.delFile(resultFilePath);
				}													
			}
		}
		//return ret;
	}

	class OpJson {
		  private String Domain = "";
		  private String WebService = "";
		  private String OpName = "";
		  private String Description = "";
		  private String Message = "";
		  private String version = "";
		  private ArrayList WebServiceParameters = null;
		/**
		 * @return the domain
		 */
		public String getDomain() {
			return Domain;
		}
		/**
		 * @param domain the domain to set
		 */
		public void setDomain(String domain) {
			Domain = domain;
		}
		/**
		 * @return the webService
		 */
		public String getWebService() {
			return WebService;
		}
		/**
		 * @param webService the webService to set
		 */
		public void setWebService(String webService) {
			WebService = webService;
		}
		/**
		 * @return the opName
		 */
		public String getOpName() {
			return OpName;
		}
		/**
		 * @param opName the opName to set
		 */
		public void setOpName(String opName) {
			OpName = opName;
		}
		/**
		 * @return the description
		 */
		public String getDescription() {
			return Description;
		}
		/**
		 * @param description the description to set
		 */
		public void setDescription(String description) {
			Description = description;
		}
		/**
		 * @return the message
		 */
		public String getMessage() {
			return Message;
		}
		/**
		 * @param message the message to set
		 */
		public void setMessage(String message) {
			Message = message;
		}
		/**
		 * @return the version
		 */
		public String getVersion() {
			return version;
		}
		/**
		 * @param version the version to set
		 */
		public void setVersion(String version) {
			this.version = version;
		}
		/**
		 * @return the webServiceParameters
		 */
		public ArrayList getWebServiceParameters() {
			return WebServiceParameters;
		}
		/**
		 * @param webServiceParameters the webServiceParameters to set
		 */
		public void setWebServiceParameters(ArrayList webServiceParameters) {
			WebServiceParameters = webServiceParameters;
		}
		  
	}
}
