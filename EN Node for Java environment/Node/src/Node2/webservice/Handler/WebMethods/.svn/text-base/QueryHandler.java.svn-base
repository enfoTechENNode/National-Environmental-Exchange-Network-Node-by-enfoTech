package Node2.webservice.Handler.WebMethods;

import java.io.ByteArrayInputStream;
import java.io.InputStream;
import java.io.PrintWriter;
import java.io.StringWriter;
import java.math.BigInteger;
import java.rmi.RemoteException;

import net.exchangenetwork.www.schema.node._2.DocumentFormatType;
import net.exchangenetwork.www.schema.node._2.GenericXmlType;
import net.exchangenetwork.www.schema.node._2.ParameterType;
import net.exchangenetwork.www.schema.node._2.ResultSetType;
import net.sf.json.JSONObject;

import org.apache.axiom.om.OMAbstractFactory;
import org.apache.axiom.om.OMElement;
import org.apache.axiom.om.OMFactory;
import org.apache.axiom.om.impl.llom.util.AXIOMUtil;
import org.apache.log4j.Level;

import DataFlow.Component.Interface.IActionProcess;
import DataFlow.Component.Interface.WebServiceParameter;
import Node.Phrase;
import Node.API.NodeUtils;
import Node.Biz.Handler.ExecuteOperation;
import Node.DB.DBManager;
import Node.DB.Interfaces.INodeOperation;
import Node.Utils.Utility;
/**
 * <p>This class create QueryHandler.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class QueryHandler extends Node.Biz.Handler.WebMethods.QueryHandler {
	private String dataflow = null;

	  /**
	   * Constructor
	   * @param requestorIP Requester IP address
	   * @param hostName Host name
	   * @param token Token
	   * @param dataflow Data flow name
	   * @param request Operation Name
	   * @param rowID Row ID
	   * @param maxRows Max rows
	   * @param params Input parameters
	   * @return 
	   */
  public QueryHandler(String requestorIP, String hostName, String token,String dataflow, String request, BigInteger rowID, BigInteger maxRows, Object[] params) {
    super(requestorIP, hostName, token, request, rowID, maxRows, params);
    this.dataflow = dataflow;
  }

  /**
   * Initialize
   * @param 
   * @return 
   */
  protected void Initialize () throws RemoteException
  {
    try {
      this.OpID = this.Initialize(this.Request,Phrase.WEB_METHOD_QUERY);
      if (this.OpID >= 0) {
        NodeUtils utils = new NodeUtils();
        String paramValues = "";
        if (this.Params != null && this.Params.length > 0) {
          for (int i = 0; i < this.Params.length; i++) {
            if (i != 0) paramValues += ",";
            paramValues += this.Params[i]+"";
          }
        }
        String[] names = new String[] { "dataflow","Request","Row ID","Max Rows","Parameter Values" };
        String[] values = new String[] { this.dataflow,this.Request,this.RowID.toString(),this.MaxRows.toString(),paramValues };
        utils.CreateOperationLog(Phrase.WebServicesLoggerName,this.OpID,null,this.TransID,Phrase.ReceivedStatus,Phrase.ReceivedMessage,
                                 this.RequestorIP,null,this.Token,null,null,null,this.HostName,names,values);
      }
      else
        throw new RemoteException(Phrase.ServiceUnavailable);
    } catch (RemoteException e) {
      NodeUtils nodeUtils = new NodeUtils();
      nodeUtils.UpdateOperationLog(Phrase.WebServicesLoggerName,this.TransID,Phrase.FailedStatus,e.toString(),true);
      throw e;
    } catch (Exception e) {
      this.Log("Could Not Initialize Query Handler: "+e.toString(),Level.ERROR);
      try {
        NodeUtils nodeUtils = new NodeUtils();
        nodeUtils.UpdateOperationLog(Phrase.WebServicesLoggerName,this.TransID,Phrase.FailedStatus,e.toString(),true);
      } catch (Exception ex) { }
      throw new RemoteException("Could Not Initilize Query Handler",e);
    }
  }

  /**
   * Authorize
   * @param 
   * @return String
   */
  protected String Authorize () throws RemoteException
  {
    String userID = null;
    NodeUtils utils = new NodeUtils();
    try {
      userID = this.AuthorizeRequest(this.OpID);
      utils.UpdateOperationLogUserName(Phrase.WebServicesLoggerName,this.TransID,userID);
    } catch (RemoteException e) {
      utils.UpdateOperationLog(Phrase.WebServicesLoggerName,this.TransID,Phrase.FailedStatus,Phrase.InvalidToken+": Insufficient Query Permission",true);
      throw new RemoteException(Phrase.InvalidToken);
    }
    return userID;
  }

  /**
   * ExecuteDataflow
   * @param dataflowConfig Data flow configuration content
   * @return Object
   */
  protected Object ExecuteDataflow(String dataflowConfig) throws Exception
  {
      IActionProcess process = GetActionProcess(Phrase.ver_2);
      process.CreateActionParameter(WebServiceParameter.transactionId, this.TransID);
      process.CreateActionParameter(WebServiceParameter.securityToken, this.Token);
      process.CreateActionParameter(WebServiceParameter.dataflow, this.dataflow);
      process.CreateActionParameter(WebServiceParameter.request, this.Request);
      process.CreateActionParameter(WebServiceParameter.rowId, this.RowID);
      process.CreateActionParameter(WebServiceParameter.maxRows, this.MaxRows);
      if (this.Params != null)
      {
          for (int i = 0; i < this.Params.length; i++)
          {
        	  ParameterType type = (ParameterType)this.Params[i];
              process.CreateActionParameter(type.getParameterName(), type.getString());
          }
      }

      return process.Execute(dataflowConfig);
  }
  
  /**
   * Execute
   * @param 
   * @return Object
   */
  protected Object Execute () throws RemoteException
  {
    Object retObj = null;
    String[] retList = null;
    String retStr = "";
    String content = "";
    boolean zipFlag = false;
	ResultSetType resultSetType = new ResultSetType();
    try {
      if (this.OpID >= 0) {
        INodeOperation opDB = DBManager.GetNodeOperation(Phrase.WebServicesLoggerName);
        ExecuteOperation exeOP = new ExecuteOperation(this);
        Object[] params = new Object[] { this.Token,this.Request,this.RowID,this.MaxRows,this.Params, this.dataflow};
        retObj = exeOP.ExecuteOperation(Phrase.WEB_METHOD_QUERY,opDB.GetOperationConfig(this.OpID),params,this.TransID,this.RequestorIP,this.LoggerName,this.UserName,this.Password);

        if(retObj!=null && (retObj instanceof String)){
        	retStr = (String)retObj;
        	
        	if(retStr.startsWith("{")){	// handle json string
        		retStr = retStr.replaceAll("\\n", "")
				.replaceAll("\\r", "")
				.replaceAll("\\r\\n", "")
				.replaceAll("\\n\\r", "")
				.replaceAll("\\t", "")
				.replaceAll("\\n\\t", "")
				.replaceAll("\\t\\n", "")
				.replaceAll(System.getProperty("line.separator"), "");
    			JSONObject jsonObject = JSONObject.fromObject(retStr);   
    			int rowCount = jsonObject.getInt("RowCount");
            	String lastSet =  jsonObject.getString("LastSet");
    			resultSetType.setRowCount(BigInteger.valueOf(rowCount));
    			if(lastSet.equalsIgnoreCase("false"))
    				resultSetType.setLastSet(false);				
    			else resultSetType.setLastSet(true);			
    			retStr = jsonObject.getString("Content");        		
    			// Return json string must be replaced back the quotation mark
    			retStr = retStr.replaceAll("qt'", "\"");
        	}else{	// set default rowcount and lastset to transform the result to wizard call
    			resultSetType.setRowCount(BigInteger.valueOf(0));
				resultSetType.setLastSet(true);				        		
        	}
				
        	OMFactory fac = OMAbstractFactory.getOMFactory();
            GenericXmlType genericXmlType = new GenericXmlType();
            DocumentFormatType documentFormatType = DocumentFormatType.Factory.fromValue(DocumentFormatType.XML.getValue());
            genericXmlType.setFormat(documentFormatType);
            OMElement value = null;
        	
            // zip or unzip
            for(int i=0;this.Params!= null && i<this.Params.length;i++){
            	if(this.Params[i]!=null && this.Params[i] instanceof ParameterType){
            		if(((ParameterType)this.Params[i])!=null && ((ParameterType)this.Params[i]).getString().equalsIgnoreCase("zipped")){
            			zipFlag = true;
            			break;
            		}else if(((ParameterType)this.Params[i]).getParameterEncoding()!=null && ((ParameterType)this.Params[i]).getParameterEncoding().getValue().equalsIgnoreCase(Phrase.XML_TYPE)){
            			String isZipped = Utility.xmlToString(((ParameterType)this.Params[i]).getString());
            			if(isZipped.equalsIgnoreCase("zipped")||isZipped.equalsIgnoreCase("\n\tzipped\n")||isZipped.equalsIgnoreCase("\t\nzipped\n")){
            				zipFlag = true;
            				break;  						  
            			}
            		}        		        			
            	}
            }
        	if(zipFlag){
        	    String path = Utility.GetTempFilePath() + "/temp/";
        		//String path = AppUtils.WebServicesRoot+"temp/";
        		String sourceFile = "QueryResultTmp"+ Utility.GetSysTimeString();
        		String zipFile = "QueryResultTmp"+ Utility.GetSysTimeString()+".zip";
        		InputStream is = new ByteArrayInputStream(retStr.getBytes());
        		Utility.writeFile(path+sourceFile, is);
        		Utility.zipFile(sourceFile, zipFile, path);
        		retStr = Utility.encode(path+zipFile);
          		Utility.delFile(path+sourceFile);
          		Utility.delFile(path+zipFile);
          		genericXmlType.setFormat(DocumentFormatType.Factory.fromValue(Phrase.ZIP_TYPE));
            	value = AXIOMUtil.stringToOM("<qResult>"+retStr+"</qResult>");
            	//value.setLocalName("");
        	}else if(Utility.isNullOrEmpty(retStr) || !retStr.startsWith("<")){
        		value = AXIOMUtil.stringToOM("<qResult>"+retStr+"</qResult>");
        	}else   value = AXIOMUtil.stringToOM(retStr);

        	resultSetType.setRowId(this.RowID);
            genericXmlType.setExtraElement(value);                
        	resultSetType.setResults(genericXmlType);
        }        
      }else
        throw new RemoteException("Operation is Not Available");
    } catch (RemoteException e) {
      StringWriter sw = new StringWriter();
      e.printStackTrace(new PrintWriter(sw));
      try
      {
        sw.close();
      } catch (Exception ex) { }
      NodeUtils utils = new NodeUtils();
      utils.UpdateOperationLog(Phrase.WebServicesLoggerName,this.TransID,Phrase.FailedStatus,sw.toString(),true);
      throw e;
    } catch (Exception e) {
      this.Log("Could Not Execute Operation: "+e.toString(),Level.ERROR);
      NodeUtils nodeUtils = new NodeUtils();
      nodeUtils.UpdateOperationLog(Phrase.WebServicesLoggerName,this.TransID,Phrase.FailedStatus,e.toString(),true);
      throw new RemoteException(Phrase.InternalError,e);
    }
    return resultSetType;
  }
}
