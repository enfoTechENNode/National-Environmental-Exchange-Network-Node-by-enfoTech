package Node.Biz.Default.Download;

import java.rmi.RemoteException;
import java.util.ArrayList;

import org.apache.log4j.Level;

import DataFlow.Component.Interface.WebServiceParameter;
import Node.Phrase;
import Node.Biz.Administration.OperationLog;
import Node.Biz.Custom.ProcParam;
import Node.Biz.Interfaces.Download.IProcess;
import Node.DB.DBManager;
import Node.DB.Interfaces.INodeDomain;
import Node.DB.Interfaces.INodeFileCabin;
import Node.DB.Interfaces.INodeOperation;
import Node.DB.Interfaces.INodeOperationLog;
import Node.Utils.LoggingUtils;
import Node.Web.Administration.Bean.NodeMonitoring.TransactionViewBean;
import Node.WebServices.Document.ClsNodeDocument;
/**
 * <p>This class create Download Process.</p>
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
	 * @param transID The transaction ID
	 * @param dataFlow The data flow name
	 * @param docs The download file object array
	 * @param param The process parameter 
	 * @throws RemoteException
	 * @return ClsNodeDocument[] The download file object array
	 */
  public ClsNodeDocument[] Execute (String token, String transID, String dataFlow, ClsNodeDocument[] docs, ProcParam param) throws RemoteException
  {
    ClsNodeDocument[] retDocs = null;
    OperationLog log = null;
    boolean validDomain = false;
    INodeDomain domainDB = null;
     String[] domainNameList;
    LoggingUtils.Log("Download>>>Process>>> token is:"+ token +" transID is: "+transID +" dataFlow is:"+dataFlow+" First docs name is:"+ (docs!=null && docs.length>0?docs[0].getName():"null")+" docs type is:"+ (docs!=null && docs.length>0?docs[0].getType():"null"),Level.DEBUG,Phrase.WebServicesLoggerName);
    try {
		INodeFileCabin cabinDB = DBManager.GetNodeFileCabin(Phrase.WebServicesLoggerName);
        INodeOperation opDB = DBManager.GetNodeOperation(Phrase.WebServicesLoggerName);
        INodeOperationLog logDB = DBManager.GetNodeOperationLog(Phrase.WebServicesLoggerName);
        // WI21157
        // check if dataflow is domain or not
		domainDB = DBManager.GetNodeDomain(Phrase.WebServicesLoggerName);
		opDB = DBManager.GetNodeOperation(Phrase.WebServicesLoggerName);
		domainNameList = domainDB.GetDomains();
		if(dataFlow!=null && !dataFlow.equals("")){
			for(int i=0;i<domainNameList.length;i++){
				if(domainNameList[i].equalsIgnoreCase(dataFlow)){
					validDomain = true;
					break;
				}            		
			}    	  
		}
		
        log = logDB.GetOperationLog(transID);
        if(validDomain){	// Handle 2.0
            String[] operationArr = opDB.GetOperations(dataFlow);
            String documentName = docs!=null&&docs.length>0?docs[0].getName():null;
            TransactionViewBean bean = null;
            String status = null;
        	if(log!=null){
        		bean = this.SetReport(log);
        		status = this.CheckTransactionStatus(bean);
        	}
            if(documentName!=null && !documentName.equals("") && !documentName.equalsIgnoreCase("null") && documentName.equalsIgnoreCase(Phrase.Node20_Report) && status.equalsIgnoreCase(Phrase.CompleteStatus)){
        		String content = this.PrintReport(bean);
        		retDocs = new ClsNodeDocument[1];
        		retDocs[0] = new ClsNodeDocument();
        		retDocs[0].setContent(content.getBytes());
        		retDocs[0].setName(Phrase.Node20_Report);
        		retDocs[0].setType(Phrase.FLAT_TYPE);
            }else if(documentName!=null && !documentName.equals("") && !documentName.equalsIgnoreCase("null") && documentName.equalsIgnoreCase(Phrase.Node20_Error) && status.equalsIgnoreCase(Phrase.FailedStatus)){
        		String content = this.PrintReport(bean);
        		retDocs = new ClsNodeDocument[1];
        		retDocs[0] = new ClsNodeDocument();
        		retDocs[0].setContent(content.getBytes());
        		retDocs[0].setName(Phrase.Node20_Error);
        		retDocs[0].setType(Phrase.FLAT_TYPE);

            }else if(documentName!=null && !documentName.equals("") && !documentName.equalsIgnoreCase("null") && documentName.equalsIgnoreCase(Phrase.Node20_Processed) && status.equalsIgnoreCase(Phrase.ProcessedStatus)){
        		String content = this.PrintReport(bean);
        		retDocs = new ClsNodeDocument[1];
        		retDocs[0] = new ClsNodeDocument();
        		retDocs[0].setContent(content.getBytes());
        		retDocs[0].setName(Phrase.Node20_Processed);
        		retDocs[0].setType(Phrase.FLAT_TYPE);
            }else{
                retDocs = cabinDB.GetDocuments(transID,dataFlow,operationArr,docs);        	
            }        	
        }else{	// Handle 1.1
        	retDocs = cabinDB.GetDocuments(transID,dataFlow,docs);
        }
        /*
        ClsNodeDocument[] searchDocs = cabinDB.GetDocuments(transID, true);
        if (searchDocs != null) {
          if (docs != null && docs.length > 0) {
            ClsNodeDocument[] tempDocs = new ClsNodeDocument[searchDocs.length];
            int index = 0;
            for (int i = 0; i < docs.length; i++) {
              for (int j = 0; j < searchDocs.length; i++) {
                if (docs[i].getName().equalsIgnoreCase(searchDocs[j].getName())) {
                  tempDocs[index] = searchDocs[j];
                  index++;
                }
              }
            }
            if (index > 0) {
              retDocs = new ClsNodeDocument [index];
              for (int i = 0; i < index; i++)
                retDocs[i] = tempDocs[i];
            }
          }
          else
            retDocs = searchDocs;
      }
      else {
        if (docs != null && docs.length > 0) {
          INodeFileCabin cabinDB = DBManager.GetNodeFileCabin(Phrase.WebServicesLoggerName);
          retDocs = cabinDB.GetDocuments(docs);
          if (retDocs == null)
            throw new RemoteException(Phrase.FileNotFound);
        }
        else
          throw new RemoteException(Phrase.FileNotFound);
      }
      if (retDocs == null || retDocs.length <= 0)
        throw new RemoteException(Phrase.FileNotFound);*/
    } catch (RemoteException e) {
      throw e;
    } catch (Exception e) {
      throw new RemoteException(Phrase.InternalError, e);
    }
    return retDocs;
  }
  
	/**
	 * SetReport
	 * @param OperationLog The operation log object
	 * @throws 
	 * @return TransactionViewBean The TransactionViewBean object
	 */
  private TransactionViewBean SetReport (OperationLog log)
  {
	  TransactionViewBean bean = new TransactionViewBean();
	  if (log != null) {
		  ArrayList details = new ArrayList();
		  String opName = log.GetOperationName();
		  if (opName != null) {
			  ArrayList temp = new ArrayList();
			  temp.add("opName");
			  temp.add("Operation Name");
			  temp.add(opName);
			  details.add(temp);
		  }
		  String transID = log.GetTransID();
		  if (transID != null) {
			  ArrayList temp = new ArrayList();
			  temp.add("transID");
			  temp.add("Transaction ID");
			  temp.add(transID);
			  details.add(temp);
		  }
		  String opType = log.GetOperationType();
		  if (opType != null) {
			  ArrayList temp = new ArrayList();
			  temp.add("opType");
			  temp.add("Operation Type");
			  temp.add(opType);
			  details.add(temp);
		  }
		  String wsName = log.GetWebServiceName();
		  if (wsName != null) {
			  ArrayList temp = new ArrayList();
			  temp.add("wsName");
			  temp.add("Web Service Name");
			  temp.add(wsName);
			  details.add(temp);
		  }
		  String domain = log.GetDomain();
		  if (domain != null) {
			  ArrayList temp = new ArrayList();
			  temp.add("domain");
			  temp.add("Domain");
			  temp.add(domain);
			  details.add(temp);
		  }
		  String userName = log.GetUserName();
		  if (userName != null) {
			  ArrayList temp = new ArrayList();
			  temp.add("userName");
			  temp.add("User Name");
			  temp.add(userName);
			  details.add(temp);
		  }
		  String reqIP = log.GetRequestorIP();
		  if (reqIP != null) {
			  ArrayList temp = new ArrayList();
			  temp.add("reqIP");
			  temp.add("Requestor IP Address");
			  temp.add(reqIP);
			  details.add(temp);
		  }
		  String hostName = log.GetHostName();
		  if (hostName != null) {
			  ArrayList temp = new ArrayList();
			  temp.add("hostName");
			  temp.add("Host Name");
			  temp.add(hostName);
			  details.add(temp);
		  }
		  String token = log.GetToken();
		  if (token != null) {
			  ArrayList temp = new ArrayList();
			  temp.add("token");
			  temp.add("Security Token");
			  temp.add(token);
			  details.add(temp);
		  }
		  String suppTransID = log.GetSupplTransID();
		  if (suppTransID != null) {
			  ArrayList temp = new ArrayList();
			  temp.add("suppTransID");
			  temp.add("Supplied Transaction ID");
			  temp.add(suppTransID);
			  details.add(temp);
		  }
		  String nodeAddress = log.GetNodeAddress();
		  if (nodeAddress != null) {
			  ArrayList temp = new ArrayList();
			  temp.add("nodeAddress");
			  temp.add("Node Address");
			  temp.add(nodeAddress);
			  details.add(temp);
		  }
		  String retURL = log.GetReturnURL();
		  if (retURL != null) {
			  ArrayList temp = new ArrayList();
			  temp.add("retURL");
			  temp.add("Return URL");
			  temp.add(retURL);
			  details.add(temp);
		  }
		  String servType = log.GetServiceType();
		  if (servType != null) {
			  ArrayList temp = new ArrayList();
			  temp.add("servType");
			  temp.add("Service Type");
			  temp.add(servType);
			  details.add(temp);
		  }
		  String startDate = log.GetStartDate();
		  if (startDate != null) {
			  ArrayList temp = new ArrayList();
			  temp.add("startDate");
			  temp.add("Start Date");
			  temp.add(startDate);
			  details.add(temp);
		  }
		  String endDate = log.GetEndDate();
		  if (endDate != null) {
			  ArrayList temp = new ArrayList();
			  temp.add("endDate");
			  temp.add("End Date");
			  temp.add(endDate);
			  details.add(temp);
		  }
		  bean.setDetails(details);
		  bean.setParameters(log.GetParameters());
		  ArrayList status = log.GetStatus();
		  ArrayList statusInput = null;
		  if (status != null && status.size() > 0) {
			  statusInput = new ArrayList();
			  for (int i = 0; i < status.size(); i++) {
				  ArrayList stat = (ArrayList)status.get(i);
				  if (stat != null && stat.size() >= 3) {
					  ArrayList temp = new ArrayList();
					  temp.add("status"+i);
					  temp.add(stat.get(0));
					  temp.add(stat.get(1));
					  temp.add(stat.get(2));
					  statusInput.add(temp);
				  }
			  }
		  }
		  bean.setStatus(statusInput);
	  }
	  return bean;
  }

	/**
	 * CheckTransactionStatus
	 * @param bean The TransactionViewBean object
	 * @throws 
	 * @return String The status
	 */
  private String CheckTransactionStatus (TransactionViewBean bean)
  {
	  String ret = null;
	  ArrayList statusArr = bean.getStatus();
	  if (statusArr != null) {
		  ArrayList temp = (ArrayList)statusArr.get(0);
		  if (temp != null && temp.size() >= 4) {
			  ret = (String)temp.get(1);
		  }
	  }
	  return ret;
  }
  
	/**
	 * PrintReport
	 * @param bean The TransactionViewBean object
	 * @throws 
	 * @return String The report
	 */
  private String PrintReport (TransactionViewBean bean)
  {
	  String ret = "";
	  ArrayList details = bean.getDetails();
	  if (details != null) {
		  for (int i = 0; i < details.size(); i++) {
			  ArrayList temp = (ArrayList)details.get(i);
			  if (temp != null && temp.size() == 3) {
				  String title = (String)temp.get(1);
				  String detail = (String)temp.get(2);
				  ret += title + ": " + detail + "\n";
			  }
		  }
	  }
	  ArrayList status = bean.getStatus();
	  if (status != null) {
		  for (int i = 0; i < status.size(); i++) {
			  ArrayList temp = (ArrayList)status.get(i);
			  if (temp != null && temp.size() >= 4) {
				  String title = (String)temp.get(1);
				  String detail = (String)temp.get(2);
				  String date = (String)temp.get(2);
				  ret += title + ": " + detail +", " + date + "\n";
			  }
		  }
	  }
	  return ret;
  }

}
