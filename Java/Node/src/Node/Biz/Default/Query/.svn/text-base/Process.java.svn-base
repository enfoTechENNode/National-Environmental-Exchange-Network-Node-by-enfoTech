package Node.Biz.Default.Query;

import java.io.ByteArrayInputStream;
import java.io.InputStream;
import java.math.BigInteger;
import java.rmi.RemoteException;
import java.util.ArrayList;

import net.exchangenetwork.www.schema.node._2.ParameterType;
import net.exchangenetwork.www.schema.node._2.ResultSetType;

import org.apache.log4j.Level;

import Node.Phrase;
import Node.Biz.Custom.ProcParam;
import Node.Biz.Interfaces.Query.IProcess;
import Node.DB.DBManager;
import Node.DB.Interfaces.INodeDomain;
import Node.DB.Interfaces.INodeOperation;
import Node.Utils.LoggingUtils;
import Node.Utils.Utility;
/**
 * <p>This class create Query Process.</p>
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
	 * @param request The request name
     * @param rowID The row ID
     * @param maxRows The max row
	 * @param params The parameter object array
	 * @param param The process parameter 
	 * @throws RemoteException
	 * @return String The query result
	 */
	public String Execute (String token, String request, BigInteger rowID, BigInteger maxRows, Object[] params, ProcParam param) throws RemoteException{
		String ret = null;
		String[] retStr = null;
		String[] opList = null;
		String dataFlow = (String)param.GetHashtable().get("dataflow");
		String requestName = request;
		boolean validDataFlow = false;
		boolean validRequestName = false;

        LoggingUtils.Log("QueryProcess>>> The rowID is: "+rowID+" The maxRows is: "+maxRows, Level.DEBUG, Phrase.WebServicesLoggerName);
        
		if(param.GetHashtable().containsKey("dataflow")){	// handle Node 2.0 must copy this section, then implement the second Execute
			INodeDomain domainDB = null;
			INodeOperation opDB = null;
			String[] domainNameList;
			try {
				domainDB = DBManager.GetNodeDomain(Phrase.WebServicesLoggerName);
				opDB = DBManager.GetNodeOperation(Phrase.WebServicesLoggerName);
				domainNameList = domainDB.GetDomains();
				if(dataFlow!=null && !dataFlow.equals("")){
					for(int i=0;i<domainNameList.length;i++){
						if(domainNameList[i].equalsIgnoreCase(dataFlow)){
							validDataFlow = true;
							break;
						}            		
					}    	  
				}else{
					validDataFlow = true;
				}
				if(validDataFlow){
					if(requestName!=null && !(requestName.equals(""))){
						for(int i=0;i<domainNameList.length;i++){
							if(validRequestName) break;
							opList = opDB.GetOperations(domainNameList[i]);
							for(int j=0;opList != null && j<opList.length;j++){
								if(opList[j]!=null && opList[j].equalsIgnoreCase(requestName)){
									validRequestName = true;
									break;
								}
							}
						}    	  
					}
					if(validRequestName){
						
						// The return value is a json string structure to replace an object structure since the old version only return string
						ret = this.Execute(token, requestName , request, rowID, maxRows, params, param);
					}else ret = Phrase.InvalidRequest;
				}else ret = Phrase.InvalidDataFlow;
			} catch (RemoteException e) {
				throw e;
			} catch (Exception e) {
				e.printStackTrace();
				throw new RemoteException(e.getMessage());
			}
		}else{	  
			ret="<result/>";
		}
		return ret;
	}

	/**
	 * Execute
	 * @param token The authentication token
	 * @param requestName The request name
     * @param rowID The row ID
     * @param maxRows The max row
	 * @param params The process parameter object array
	 * @param param The process parameter 
	 * @throws RemoteException
	 * @return String The query result
	 */
	// handle 2.0. Here implement the test case of NCT.
	public String Execute (String token, String requestName, String request, BigInteger rowID, BigInteger maxRows, Object[] params, ProcParam param) throws RemoteException
	{
		boolean zipFlag = false;
		ResultSetType resultSetType = new ResultSetType();
		String content = "";
		String[] paramNames = null;
		String[] paramValues = null;
		String[] paramTypes = null;
		String[] paramEncodes = null;
		String retStr = null;
		String ret = null;
		
		try {
			if(params!=null && params.length>0){
				paramNames = new String[params.length];
				paramValues = new String[params.length];
				paramTypes = new String[params.length];
				paramEncodes = new String[params.length];
				// check data type and encode,change complex value to string
				for(int i=0;params != null && i<params.length;i++){
					paramNames[i] = ((ParameterType)params[i]).getParameterName();
					paramValues[i] = ((ParameterType)params[i]).getString();
					paramTypes[i] = ((ParameterType)params[i]).getParameterType()==null?null:((ParameterType)params[i]).getParameterType().toString();
					paramEncodes[i] = ((ParameterType)params[i]).getParameterEncoding()==null?null:((ParameterType)params[i]).getParameterEncoding().getValue();

					if(paramEncodes[i]!=null && paramEncodes[i].equalsIgnoreCase(Phrase.BASE64_TYPE)){
					    String path = Utility.GetTempFilePath()+"/temp/";
						String sourceFile = "QueryTmpDecode";
						Utility.decode(paramValues[i], sourceFile);
						content = Utility.readFile(path+sourceFile).toString();
						Utility.delFile(path+sourceFile);
					}else if(paramEncodes[i]!=null && paramEncodes[i].equalsIgnoreCase(Phrase.ZIP_TYPE)){
					    String path = Utility.GetTempFilePath()+"/temp/";
						String zipFile = "QueryTmp.zip";
						String unzipFile = "QueryTmp";
						InputStream is = new ByteArrayInputStream(paramValues[i].getBytes());
						Utility.writeFile(path+zipFile, is);
						// WI 21088
						ArrayList fileList = Utility.unZipFile(zipFile, path);
						if(fileList != null && fileList.size() > 0){
							for(int j=0;j<fileList.size();j++){
								content = new String(Utility.readFile(path + (String)fileList.get(j)));				
					      		Utility.delFile(path+(String)fileList.get(j));					
							}
						}
						/*Utility.unZipFile(zipFile, unzipFile, path);
						params[i] = Utility.readFile(path+unzipFile).toString();
						Utility.delFile(path+unzipFile);*/
						Utility.delFile(path+zipFile);
					}
				}
			}
			String[] names = {"Row 1 text","Row 2 text","Row 3 text","Row 4 text","Row 5 text",
					"Row 6 text","Row 7 text","Row 8 text","Row 9 text","Row 10 text"};
			int rowNum = 0;
			int j = 0;
			String[] retList = null;
			if(maxRows.intValue()==-1) retList = names;
			else if(maxRows.intValue()>names.length){
				rowNum = names.length-rowID.intValue();
				retList = new String[rowNum];
				for(int i=rowID.intValue();rowID!=null && i<names.length;i++){
					retList[j] = names[i];
					j++;
				}
			}
			else{
				rowNum = maxRows.intValue()-rowID.intValue();
				retList = new String[rowNum];
				for(int i=rowID.intValue();rowID!=null && i<maxRows.intValue();i++){
					retList[j] = names[i];
					j++;
				}
			}
			if(retList != null){
				// Create json string must use qt' to replace the quotation mark "
				retStr = "<?xml version=qt'1.0qt'?><QueryResult xmlns=qt'http://www.exchangenetwork.net/schema/NCT/1qt' xmlns:xsi=qt'http://www.w3.org/2001/XMLSchema-instanceqt'>";
				if(maxRows!=null && maxRows.intValue() != -1 && maxRows.intValue() <= retList.length && retList.length!=10){
					resultSetType.setRowCount(maxRows==null?BigInteger.valueOf(0):maxRows);
					resultSetType.setLastSet(false);
					for(int i=0;i<resultSetType.getRowCount().intValue();i++){
						content = content+"<row>"+retList[i]+"</row>";
					}
				}else if(maxRows!=null && maxRows.intValue() == -1){
					resultSetType.setRowCount(BigInteger.valueOf(retList.length));
					resultSetType.setLastSet(true);
					for(int i=0;i<retList.length;i++){	// -1 means max result
						content = content+"<row>"+retList[i]+"</row>";
					}
				}else {
					resultSetType.setRowCount(BigInteger.valueOf(retList.length));
					resultSetType.setLastSet(true);        		
					for(int i=0;i<resultSetType.getRowCount().intValue();i++){
						content = content+"<row>"+retList[i]+"</row>";
					}
				}
				// zip or unzip
				for(int i=0;params!= null && i<params.length;i++){
					if(params[i]!=null && params[i] instanceof ParameterType){
						//System.out.println("The Params is:" +((ParameterType)params[i]).getString());
						if(((ParameterType)params[i])!=null && ((ParameterType)params[i]).getString().equalsIgnoreCase("zipped")){
							//resultSetType.setLastSet(true);
							zipFlag = true;
							break;
						}else if(((ParameterType)params[i]).getParameterEncoding()!=null && ((ParameterType)params[i]).getParameterEncoding().getValue().equalsIgnoreCase(Phrase.XML_TYPE)){
							String isZipped = Utility.xmlToString(((ParameterType)params[i]).getString());
							if(isZipped.equalsIgnoreCase("zipped")||isZipped.equalsIgnoreCase("\n\tzipped\n")||isZipped.equalsIgnoreCase("\t\nzipped\n")){
								//resultSetType.setLastSet(true);
								zipFlag = true;
								break;  						  
							}
						}        		        			
					}
				}
				retStr = retStr + content + "</QueryResult>";
			}       

		} catch (RemoteException e) {
			e.printStackTrace();
			throw e;
		} catch (Exception e) {
			e.printStackTrace();
			throw new RemoteException(e.getMessage());
		}
		if(retStr!=null){
			ret = "{\"LastSet\":\""+ resultSetType.getLastSet() 
			+ "\",\"RowCount\":" + resultSetType.getRowCount() 
			+ ",\"Content\":\"" + retStr 
			+ "\"}";
		}
		return ret;
	}

}
