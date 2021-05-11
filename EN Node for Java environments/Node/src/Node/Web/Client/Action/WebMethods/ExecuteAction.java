package Node.Web.Client.Action.WebMethods;

import java.io.PrintWriter;
import java.io.StringWriter;
import java.net.URL;
import java.rmi.RemoteException;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.apache.log4j.Level;
import org.apache.struts.action.ActionForm;
import org.apache.struts.action.ActionForward;
import org.apache.struts.action.ActionMapping;

import Node.Phrase;
import Node.Web.Client.BaseAction;
import Node.Web.Client.Bean.WebMethods.ExecuteBean;
import Node.WebServices.Requestor.NodeRequestor;
/**
 * <p>This class create ExecuteAction.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class ExecuteAction extends BaseAction {
	  /**
	   * Constructor
	   * @param 
	   * @return 
	   */
  public ExecuteAction() {
  }

  /**
   * formExecute
   * @param mapping
   * @param form
   * @param request
   * @param response
   * @return ActionForward
   */
  public ActionForward formExecute (ActionMapping mapping, ActionForm form, HttpServletRequest request, HttpServletResponse resonse) throws Exception
  {
    ExecuteBean bean = (ExecuteBean)form;
    String ret = null;
    String version = request.getParameter("version");
    if (request.getMethod().equalsIgnoreCase("GET"))
      this.ClearPage(bean);
    this.SetPage(bean);
    String act = request.getParameter("act");
    if (act != null && !act.equals("")) {
      if (act.equalsIgnoreCase("ADD_PARAMETER")){
        this.AddParameter(bean);
      	if (version.equalsIgnoreCase(Phrase.ver_1)){
    		ret = "execute";
    	}else{
    		ret = "executeV2";
    	}    	    	  
      }
      if (act.equalsIgnoreCase("REMOVE_PARAMETER")){
    	if (version.equalsIgnoreCase(Phrase.ver_1)){
    		ret = "execute";
    	}else{
    		ret = "executeV2";
    	}    	    	  
    	this.RemoveParameter(bean);    	  
      }
      if (act.equalsIgnoreCase("EXECUTE"))
      	if (version.equalsIgnoreCase(Phrase.ver_1)){
            this.Execute(bean);
    		ret = "execute";
    	}else{
            this.ExecuteV2(bean);
    		ret = "executeV2";
    	}
    }else{
    	if (version.equalsIgnoreCase(Phrase.ver_1)){
    		ret = "execute";
    	}else{
    		ret = "executeV2";
    	}    	
    }
	return mapping.findForward(ret);
  }

  /**
   * SetPage
   * @param bean
   * @return 
   */
  protected void SetPage (ExecuteBean bean)
  {
    super.SetPage(bean);
    this.SetParamNames(bean);
  }

  /**
   * ClearPage
   * @param bean
   * @return 
   */
  private void ClearPage (ExecuteBean bean)
  {
    bean.setInterfaceName("");
    bean.setMethodName(null);
    bean.setNumParams("0");
    bean.setParameter1("");
    bean.setParameter2("");
    bean.setParameter3("");
    bean.setParameter4("");
    bean.setParameter5("");
    bean.setParameter6("");
    bean.setParameter7("");
    bean.setParameter8("");
    bean.setParameter9("");
    bean.setParameter10("");
    bean.setParameter11("");
    bean.setParameter12("");
    bean.setParameter13("");
    bean.setParameter14("");
    bean.setParameter15("");
    bean.setParameter16("");
    bean.setParameter17("");
    bean.setParameter18("");
    bean.setParameter19("");
    bean.setParameter20("");
    bean.setParameter21("");
    bean.setParameter22("");
    bean.setParameter23("");
    bean.setParameter24("");
    bean.setParameter25("");
    bean.setParameter26("");
    bean.setParameter27("");
    bean.setParameter28("");
    bean.setParameter29("");
    bean.setParameter30("");
    bean.setParamName1("");
    bean.setParamName2("");
    bean.setParamName3("");
    bean.setParamName4("");
    bean.setParamName5("");
    bean.setParamName6("");
    bean.setParamName7("");
    bean.setParamName8("");
    bean.setParamName9("");
    bean.setParamName10("");
    bean.setParamName11("");
    bean.setParamName12("");
    bean.setParamName13("");
    bean.setParamName14("");
    bean.setParamName15("");
    bean.setParamName16("");
    bean.setParamName17("");
    bean.setParamName18("");
    bean.setParamName19("");
    bean.setParamName20("");
    bean.setParamName21("");
    bean.setParamName22("");
    bean.setParamName23("");
    bean.setParamName24("");
    bean.setParamName25("");
    bean.setParamName26("");
    bean.setParamName27("");
    bean.setParamName28("");
    bean.setParamName29("");
    bean.setParamName30("");
    bean.setResult("");
    bean.setError("");
  }

  /**
   * AddParameter
   * @param bean
   * @return 
   */
  private void AddParameter (ExecuteBean bean)
  {
    String temp = bean.getNumParams();
    int num = 0;
    if (!temp.equals(""))
      num = Integer.parseInt(temp);
    if (num < 30)
      num++;
    bean.setNumParams(num+"");
  }

  /**
   * RemoveParameter
   * @param bean
   * @return 
   */
  private void RemoveParameter (ExecuteBean bean)
  {
    String temp = bean.getNumParams();
    int num = 0;
    if (!temp.equals(""))
      num = Integer.parseInt(temp);
    if (num > 0)
      num--;
    bean.setNumParams(num+"");
  }

  /**
   * SetParamNames
   * @param bean
   * @return 
   */
  private void SetParamNames (ExecuteBean bean)
  {
      bean.setAquaColor("blue");
      bean.setParamName1("Parameter 1:");
      bean.setParamName2("Parameter 2:");
      bean.setParamName3("Parameter 3:");
      bean.setParamName4("Parameter 4:");
      bean.setParamName5("Parameter 5:");
      bean.setParamName6("Parameter 6:");
      bean.setParamName7("Parameter 7:");
      bean.setParamName8("Parameter 8:");
      bean.setParamName9("Parameter 9:");
      bean.setParamName10("Parameter 10:");
      bean.setParamName11("Parameter 11:");
      bean.setParamName12("Parameter 12:");
      bean.setParamName13("Parameter 13:");
      bean.setParamName14("Parameter 14:");
      bean.setParamName15("Parameter 15:");
      bean.setParamName16("Parameter 16:");
      bean.setParamName17("Parameter 17:");
      bean.setParamName18("Parameter 18:");
      bean.setParamName19("Parameter 19:");
      bean.setParamName20("Parameter 20:");
      bean.setParamName21("Parameter 21:");
      bean.setParamName22("Parameter 22:");
      bean.setParamName23("Parameter 23:");
      bean.setParamName24("Parameter 24:");
      bean.setParamName25("Parameter 25:");
      bean.setParamName26("Parameter 26:");
      bean.setParamName27("Parameter 27:");
      bean.setParamName28("Parameter 28:");
      bean.setParamName29("Parameter 29:");
      bean.setParamName30("Parameter 30:");
  }

  /**
   * Execute
   * @param bean
   * @return 
   */
  private void Execute (ExecuteBean bean)
  {
    String token = bean.getToken();
    if (token != null && token.equals(""))
      token = null;
    String interfaceName = bean.getInterfaceName();
    String methodName = bean.getMethodName();
    String[] params = bean.getParamValues();
    try {
      URL node = new URL(this.GetNodeAddress(bean));
      NodeRequestor requestor = new NodeRequestor(node, Phrase.ClientLoggerName);
      bean.setResult(requestor.execute(token, interfaceName, params));
    } catch (RemoteException e) {
      bean.setResult(e.getMessage());
    } catch (Exception e) {
      bean.setError("Could Not Call Execute");
      this.Log("Could not call Execute: " + e.toString(), Level.ERROR);
      bean.setResult("");
    }
  }

  /**
   * ExecuteV2
   * @param bean
   * @return 
   */
  private void ExecuteV2 (ExecuteBean bean)
  {
	    String token = bean.getToken();
	    if (token != null && token.equals(""))
	      token = null;
	    String interfaceName = bean.getInterfaceName();
	    String methodName = bean.getMethodName();
	    String[] params = bean.getParamValues();
	    bean.setRet(null);
	    try {
	      URL node = new URL(this.GetNodeAddress_V2(bean));
	      Node2.webservice.Requestor.NodeRequestor requestor = new Node2.webservice.Requestor.NodeRequestor(node, Phrase.ClientLoggerName);
	      bean.setRet(requestor.execute(token, interfaceName, methodName, params));
	    } catch (RemoteException e) {
	    	StringWriter sw = new StringWriter();
	    	e.printStackTrace(new PrintWriter(sw));
	    	bean.setResult(sw.toString());
	    } catch (Exception e) {
	      //bean.setError(e.getMessage());
	      this.Log("Could not call Execute: " + e.toString(), Level.ERROR);
	    	StringWriter sw = new StringWriter();
	    	e.printStackTrace(new PrintWriter(sw));
	    	bean.setResult(sw.toString());
	    }
	  }
}
