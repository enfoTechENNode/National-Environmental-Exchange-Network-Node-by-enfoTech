package Node.Biz.Handler.WebMethods;

import java.io.PrintWriter;
import java.io.StringWriter;
import java.math.BigInteger;
import java.rmi.RemoteException;
import org.apache.log4j.Level;

import DataFlow.Component.Interface.*;

import Node.API.NodeUtils;
import Node.Biz.Handler.Handler;
import Node.Biz.Handler.ExecuteOperation;
import Node.DB.DBManager;
import Node.DB.Interfaces.INodeOperation;
import Node.Phrase;
/**
 * <p>This class create QueryHandler Process.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class QueryHandler extends Handler {
	protected String Request = null;
	protected BigInteger RowID = null;
	protected BigInteger MaxRows = null;
	protected Object[] Params = null;
	protected int OpID = -1;

	  /**
	   * Constructor.
	   * @param requestorIP Requester IP address
	   * @param hostName Host name
	   * @param token Token
	   * @param request Operation Name
	   * @param rowID Row ID
	   * @param maxRows Max rows
	   * @param params Input parameters
	   * @return 
	   */
  public QueryHandler(String requestorIP, String hostName, String token, String request, BigInteger rowID, BigInteger maxRows, Object[] params) {
    super(requestorIP);
    this.HostName = hostName;
    this.Token = token;
    this.Request = request;
    this.RowID = rowID;
    this.MaxRows = maxRows;
    this.Params = params;
  }

  /**
   * Initialize.
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
        String[] names = new String[] { "Request","Row ID","Max Rows","Parameter Values" };
        String[] values = new String[] { this.Request,this.RowID.toString(),this.MaxRows.toString(),paramValues };
        this.OpLogID = utils.CreateOperationLog(Phrase.WebServicesLoggerName,this.OpID,null,this.TransID,Phrase.ReceivedStatus,Phrase.ReceivedMessage,
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
   * Authorize.
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
   * ExecuteDataflow.
   * @param dataflowConfig Data flow configuration content
   * @return Object
   */
  protected Object ExecuteDataflow(String dataflowConfig) throws Exception
  {
      IActionProcess process = GetActionProcess(Phrase.ver_1);
      process.CreateActionParameter(WebServiceParameter.transactionId, this.TransID);
      process.CreateActionParameter(WebServiceParameter.securityToken, this.Token);
      process.CreateActionParameter(WebServiceParameter.request, this.Request);
      process.CreateActionParameter(WebServiceParameter.rowId, this.RowID);
      process.CreateActionParameter(WebServiceParameter.maxRows, this.MaxRows);

      INodeOperation nodeOpe = DBManager.GetNodeOperation(Phrase.WebServicesLoggerName);
      String[] pars = nodeOpe.GetParameters(this.OpID);
      if (this.Params != null && pars.length == this.Params.length)
      {
          for (int i = 0; i < pars.length; i++)
              process.CreateActionParameter(pars[i].trim(), this.Params[i]);
      }
      return process.Execute(dataflowConfig);
  }

  /**
   * Execute.
   * @param 
   * @return Object
   */
  protected Object Execute () throws RemoteException
  {
    Object retObj = null;
    try {
      if (this.OpID >= 0) {
        INodeOperation opDB = DBManager.GetNodeOperation(Phrase.WebServicesLoggerName);
        ExecuteOperation exeOP = new ExecuteOperation(this);
        Object[] params = new Object[] { this.Token,this.Request,this.RowID,this.MaxRows,this.Params };
        retObj = exeOP.ExecuteOperation(Phrase.WEB_METHOD_QUERY,opDB.GetOperationConfig(this.OpID),params,this.TransID,this.RequestorIP,this.LoggerName,this.UserName,this.Password);
      }
      else
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
    return retObj;
  }
}
