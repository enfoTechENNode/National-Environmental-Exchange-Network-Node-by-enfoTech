package Node2.webservice.Handler.WebMethods;

import DataFlow.Component.Interface.IActionProcess;
import DataFlow.Component.Interface.WebServiceParameter;
import Node.Phrase;
/**
 * <p>This class create ExecuteHandler.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class ExecuteHandler extends Node.Biz.Handler.WebMethods.ExecuteHandler {

	  /**
	   * Constructor
	   * @param requestorIP Requester IP address
	   * @param hostName Host name
	   * @param token Token
	   * @param interfaceName Interface name
	   * @param methodName Method Name
	   * @param params parameters objects
	   * @return 
	   */
  public ExecuteHandler(String requestorIP,String hostName,String token, String interfaceName, String methodName, Object[] params) {
    super(requestorIP,hostName,token, interfaceName, methodName, params);
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
      process.CreateActionParameter(WebServiceParameter.interfaceName, this.interfaceName);
      process.CreateActionParameter(WebServiceParameter.methodName, this.methodName);
      process.CreateActionParameter(WebServiceParameter.parameters, this.Params);

      return process.Execute(dataflowConfig);
  }

}
