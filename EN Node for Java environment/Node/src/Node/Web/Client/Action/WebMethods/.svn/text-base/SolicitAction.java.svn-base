package Node.Web.Client.Action.WebMethods;

import java.io.PrintWriter;
import java.io.StringWriter;
import java.net.URL;
import java.rmi.RemoteException;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import net.exchangenetwork.www.schema.node._2.NotificationTypeCode;
import net.exchangenetwork.www.schema.node._2.NotificationURIType;

import org.apache.log4j.Level;
import org.apache.struts.action.ActionForm;
import org.apache.struts.action.ActionForward;
import org.apache.struts.action.ActionMapping;

import Node.DB.DBManager;
import Node.DB.Interfaces.Configuration.ISystemConfiguration;
import Node.DB.Interfaces.INodeOperation;
import Node.Phrase;
import Node.Web.Client.BaseAction;
import Node.Web.Client.Bean.WebMethods.SolicitBean;
import Node.WebServices.Requestor.NodeRequestor;
/**
 * <p>This class create SolicitAction.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class SolicitAction extends BaseAction {
	  /**
	   * Constructor
	   * @param 
	   * @return 
	   */
  public SolicitAction() {
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
	  SolicitBean bean = (SolicitBean)form;
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
				  ret = "solicit";
			  }else{
				  ret = "solicitV2";
			  }    	    	  
		  }
		  if (act.equalsIgnoreCase("REMOVE_PARAMETER")){
			  this.RemoveParameter(bean);    	  
			  if (version.equalsIgnoreCase(Phrase.ver_1)){
				  ret = "solicit";
			  }else{
				  ret = "solicitV2";
			  }    	    	  
		  }
		  if (act.equalsIgnoreCase("SOLICIT"))
			  if (version.equalsIgnoreCase(Phrase.ver_1)){
				  this.Solicit(bean);
				  ret = "solicit";
			  }else{
				  this.SolicitV2(bean);
				  ret = "solicitV2";
			  }
	  }else{
		  if (version.equalsIgnoreCase(Phrase.ver_1)){
			  ret = "solicit";
		  }else{
			  ret = "solicitV2";
		  }    	
	  }
	  return mapping.findForward(ret);
  }

  /**
   * SetPage
   * @param bean
   * @return 
   */
  private void SetPage (SolicitBean bean)
  {
    super.SetPage(bean);
    this.SetAvailableRequests(bean);
    this.SetParamNames(bean);
    String request1 = bean.getRequest1();
    if (request1 != null && !request1.equals(""))
      bean.setRequest2("");
  }

  /**
   * ClearPage
   * @param bean
   * @return 
   */
  private void ClearPage (SolicitBean bean)
  {
    bean.setAquaColor("blue");
    bean.setAvailableRequests(null);
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
    bean.setParameter31("");
    bean.setParameter32("");
    bean.setParameter33("");
    bean.setParameter34("");
    bean.setParameter35("");
    bean.setParameter36("");
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
    bean.setParamName31("");
    bean.setParamName32("");
    bean.setParamName33("");
    bean.setParamName34("");
    bean.setParamName35("");
    bean.setParamName36("");
    bean.setParamType1("");
    bean.setParamType2("");
    bean.setParamType3("");
    bean.setParamType4("");
    bean.setParamType5("");
    bean.setParamType6("");
    bean.setParamType7("");
    bean.setParamType8("");
    bean.setParamType9("");
    bean.setParamType10("");
    bean.setParamType11("");
    bean.setParamType12("");
    bean.setParamType13("");
    bean.setParamType14("");
    bean.setParamType15("");
    bean.setParamType16("");
    bean.setParamType17("");
    bean.setParamType18("");
    bean.setParamType19("");
    bean.setParamType20("");
    bean.setParamType21("");
    bean.setParamType22("");
    bean.setParamType23("");
    bean.setParamType24("");
    bean.setParamType25("");
    bean.setParamType26("");
    bean.setParamType27("");
    bean.setParamType28("");
    bean.setParamType29("");
    bean.setParamType30("");
    bean.setParamType31("");
    bean.setParamType32("");
    bean.setParamType33("");
    bean.setParamType34("");
    bean.setParamType35("");
    bean.setParamType36("");
    bean.setParamEncode1("");
    bean.setParamEncode2("");
    bean.setParamEncode3("");
    bean.setParamEncode4("");
    bean.setParamEncode5("");
    bean.setParamEncode6("");
    bean.setParamEncode7("");
    bean.setParamEncode8("");
    bean.setParamEncode9("");
    bean.setParamEncode10("");
    bean.setParamEncode11("");
    bean.setParamEncode12("");
    bean.setParamEncode13("");
    bean.setParamEncode14("");
    bean.setParamEncode15("");
    bean.setParamEncode16("");
    bean.setParamEncode17("");
    bean.setParamEncode18("");
    bean.setParamEncode19("");
    bean.setParamEncode20("");
    bean.setParamEncode21("");
    bean.setParamEncode22("");
    bean.setParamEncode23("");
    bean.setParamEncode24("");
    bean.setParamEncode25("");
    bean.setParamEncode26("");
    bean.setParamEncode27("");
    bean.setParamEncode28("");
    bean.setParamEncode29("");
    bean.setParamEncode30("");
    bean.setParamEncode31("");
    bean.setParamEncode32("");
    bean.setParamEncode33("");
    bean.setParamEncode34("");
    bean.setParamEncode35("");
    bean.setParamEncode36("");
    bean.setRequest1("");
    bean.setRequest2("");
    bean.setResult("");
    bean.setReturnURL("");
    bean.setError("");
  }

  /**
   * SetParamNames
   * @param bean
   * @return 
   */
  private void SetParamNames (SolicitBean bean)
  {
	  String request1 = bean.getRequest1();
	  boolean updated = false;
	  if (request1 != null && !request1.equals("")) {
		  try {
			  INodeOperation opDB = DBManager.GetNodeOperation(Phrase.ClientLoggerName);
			  String[] parameters = opDB.GetParameters(Phrase.WEB_METHOD_SOLICIT,request1);
			  if (parameters != null && parameters.length > 0) {
				  updated = true;
				  bean.setNumParams(parameters.length+"");
				  bean.setAquaColor("off");
				  if (parameters.length > 0)
					  bean.setParamName1(parameters[0]);
				  else
					  bean.setParamName1("Parameter 1:");
				  if (parameters.length > 1)
					  bean.setParamName2(parameters[1]);
				  else
					  bean.setParamName2("Parameter 2:");
				  if (parameters.length > 2)
					  bean.setParamName3(parameters[2]);
				  else
					  bean.setParamName3("Parameter 3:");
				  if (parameters.length > 3)
					  bean.setParamName4(parameters[3]);
				  else
					  bean.setParamName4("Parameter 4:");
				  if (parameters.length > 4)
					  bean.setParamName5(parameters[4]);
				  else
					  bean.setParamName5("Parameter 5:");
				  if (parameters.length > 5)
					  bean.setParamName6(parameters[5]);
				  else
					  bean.setParamName6("Parameter 6:");
				  if (parameters.length > 6)
					  bean.setParamName7(parameters[6]);
				  else
					  bean.setParamName7("Parameter 7:");
				  if (parameters.length > 7)
					  bean.setParamName8(parameters[7]);
				  else
					  bean.setParamName8("Parameter 8:");
				  if (parameters.length > 8)
					  bean.setParamName9(parameters[8]);
				  else
					  bean.setParamName9("Parameter 9:");
				  if (parameters.length > 9)
					  bean.setParamName10(parameters[9]);
				  else
					  bean.setParamName10("Parameter 10:");
				  if (parameters.length > 10)
					  bean.setParamName11(parameters[10]);
				  else
					  bean.setParamName11("Parameter 11:");
				  if (parameters.length > 11)
					  bean.setParamName12(parameters[11]);
				  else
					  bean.setParamName12("Parameter 12:");
				  if (parameters.length > 12)
					  bean.setParamName13(parameters[12]);
				  else
					  bean.setParamName13("Parameter 13:");
				  if (parameters.length > 13)
					  bean.setParamName14(parameters[13]);
				  else
					  bean.setParamName14("Parameter 14:");
				  if (parameters.length > 14)
					  bean.setParamName15(parameters[14]);
				  else
					  bean.setParamName15("Parameter 15:");
				  if (parameters.length > 15)
					  bean.setParamName16(parameters[15]);
				  else
					  bean.setParamName16("Parameter 16:");
				  if (parameters.length > 16)
					  bean.setParamName17(parameters[16]);
				  else
					  bean.setParamName17("Parameter 17:");
				  if (parameters.length > 17)
					  bean.setParamName18(parameters[17]);
				  else
					  bean.setParamName18("Parameter 18:");
				  if (parameters.length > 18)
					  bean.setParamName19(parameters[18]);
				  else
					  bean.setParamName19("Parameter 19:");
				  if (parameters.length > 19)
					  bean.setParamName20(parameters[19]);
				  else
					  bean.setParamName20("Parameter 20:");
				  if (parameters.length > 20)
					  bean.setParamName21(parameters[20]);
				  else
					  bean.setParamName21("Parameter 21:");
				  if (parameters.length > 21)
					  bean.setParamName22(parameters[21]);
				  else
					  bean.setParamName22("Parameter 22:");
				  if (parameters.length > 22)
					  bean.setParamName23(parameters[22]);
				  else
					  bean.setParamName23("Parameter 23:");
				  if (parameters.length > 23)
					  bean.setParamName24(parameters[23]);
				  else
					  bean.setParamName24("Parameter 24:");
				  if (parameters.length > 24)
					  bean.setParamName25(parameters[24]);
				  else
					  bean.setParamName25("Parameter 25:");
				  if (parameters.length > 25)
					  bean.setParamName26(parameters[25]);
				  else
					  bean.setParamName26("Parameter 26:");
				  if (parameters.length > 26)
					  bean.setParamName27(parameters[26]);
				  else
					  bean.setParamName27("Parameter 27:");
				  if (parameters.length > 27)
					  bean.setParamName28(parameters[27]);
				  else
					  bean.setParamName28("Parameter 28:");
				  if (parameters.length > 28)
					  bean.setParamName29(parameters[28]);
				  else
					  bean.setParamName29("Parameter 29:");
				  if (parameters.length > 29)
					  bean.setParamName30(parameters[29]);
				  else
					  bean.setParamName30("Parameter 30:");
				  if (parameters.length > 30)
					  bean.setParamName31(parameters[30]);
				  else
					  bean.setParamName31("Parameter 31:");
				  if (parameters.length > 31)
					  bean.setParamName32(parameters[31]);
				  else
					  bean.setParamName32("Parameter 32:");
				  if (parameters.length > 32)
					  bean.setParamName33(parameters[32]);
				  else
					  bean.setParamName33("Parameter 33:");
				  if (parameters.length > 33)
					  bean.setParamName34(parameters[33]);
				  else
					  bean.setParamName34("Parameter 34:");
				  if (parameters.length > 34)
					  bean.setParamName35(parameters[34]);
				  else
					  bean.setParamName35("Parameter 35:");
				  if (parameters.length > 35)
					  bean.setParamName36(parameters[35]);
				  else
					  bean.setParamName36("Parameter 36:");
			  }
		  } catch (Exception e) {
			  this.Log("Could Not Set Param Names: " + e.toString(),Level.ERROR);
		  }

	  }
	  if (!updated) {
		  bean.setAquaColor("blue");
		  if (bean.getParamName1()==null || bean.getParamName1().equalsIgnoreCase(""))
			  bean.setParamName1("Parameter Name 1:");
		  if (bean.getParamName2()==null || bean.getParamName2().equalsIgnoreCase(""))
			  bean.setParamName2("Parameter Name 2:");
		  if (bean.getParamName3()==null || bean.getParamName3().equalsIgnoreCase(""))
			  bean.setParamName3("Parameter Name 3:");
		  if (bean.getParamName4()==null || bean.getParamName4().equalsIgnoreCase(""))
			  bean.setParamName4("Parameter Name 4:");
		  if (bean.getParamName5()==null || bean.getParamName5().equalsIgnoreCase(""))
			  bean.setParamName5("Parameter Name 5:");
		  if (bean.getParamName6()==null || bean.getParamName6().equalsIgnoreCase(""))
			  bean.setParamName6("Parameter Name 6:");
		  if (bean.getParamName7()==null || bean.getParamName7().equalsIgnoreCase(""))
			  bean.setParamName7("Parameter Name 7:");
		  if (bean.getParamName8()==null || bean.getParamName8().equalsIgnoreCase(""))
			  bean.setParamName8("Parameter Name 8:");
		  if (bean.getParamName9()==null || bean.getParamName9().equalsIgnoreCase(""))
			  bean.setParamName9("Parameter Name 9:");
		  if (bean.getParamName10()==null || bean.getParamName10().equalsIgnoreCase(""))
			  bean.setParamName10("Parameter Name 10:");
		  if (bean.getParamName11()==null || bean.getParamName11().equalsIgnoreCase(""))
			  bean.setParamName11("Parameter Name 11:");
		  if (bean.getParamName12()==null || bean.getParamName12().equalsIgnoreCase(""))
			  bean.setParamName12("Parameter Name 12:");
		  if (bean.getParamName13()==null || bean.getParamName13().equalsIgnoreCase(""))
			  bean.setParamName13("Parameter Name 13:");
		  if (bean.getParamName14()==null || bean.getParamName14().equalsIgnoreCase(""))
			  bean.setParamName14("Parameter Name 14:");
		  if (bean.getParamName15()==null || bean.getParamName15().equalsIgnoreCase(""))
			  bean.setParamName15("Parameter Name 15:");
		  if (bean.getParamName16()==null || bean.getParamName16().equalsIgnoreCase(""))
			  bean.setParamName16("Parameter Name 16:");
		  if (bean.getParamName17()==null || bean.getParamName17().equalsIgnoreCase(""))
			  bean.setParamName17("Parameter Name 17:");
		  if (bean.getParamName18()==null || bean.getParamName18().equalsIgnoreCase(""))
			  bean.setParamName18("Parameter Name 18:");
		  if (bean.getParamName19()==null || bean.getParamName19().equalsIgnoreCase(""))
			  bean.setParamName19("Parameter Name 19:");
		  if (bean.getParamName20()==null || bean.getParamName20().equalsIgnoreCase(""))
			  bean.setParamName20("Parameter Name 20:");
		  if (bean.getParamName21()==null || bean.getParamName21().equalsIgnoreCase(""))
			  bean.setParamName21("Parameter Name 21:");
		  if (bean.getParamName22()==null || bean.getParamName22().equalsIgnoreCase(""))
			  bean.setParamName22("Parameter Name 22:");
		  if (bean.getParamName23()==null || bean.getParamName23().equalsIgnoreCase(""))
			  bean.setParamName23("Parameter Name 23:");
		  if (bean.getParamName24()==null || bean.getParamName24().equalsIgnoreCase(""))
			  bean.setParamName24("Parameter Name 24:");
		  if (bean.getParamName25()==null || bean.getParamName25().equalsIgnoreCase(""))
			  bean.setParamName25("Parameter Name 25:");
		  if (bean.getParamName26()==null || bean.getParamName26().equalsIgnoreCase(""))
			  bean.setParamName26("Parameter Name 26:");
		  if (bean.getParamName27()==null || bean.getParamName27().equalsIgnoreCase(""))
			  bean.setParamName27("Parameter Name 27:");
		  if (bean.getParamName28()==null || bean.getParamName28().equalsIgnoreCase(""))
			  bean.setParamName28("Parameter Name 28:");
		  if (bean.getParamName29()==null || bean.getParamName29().equalsIgnoreCase(""))
			  bean.setParamName29("Parameter Name 29:");
		  if (bean.getParamName30()==null || bean.getParamName30().equalsIgnoreCase(""))
			  bean.setParamName30("Parameter Name 30:");
		  if (bean.getParamName31()==null || bean.getParamName31().equalsIgnoreCase(""))
			  bean.setParamName31("Parameter Name 31:");
		  if (bean.getParamName32()==null || bean.getParamName32().equalsIgnoreCase(""))
			  bean.setParamName32("Parameter Name 32:");
		  if (bean.getParamName33()==null || bean.getParamName33().equalsIgnoreCase(""))
			  bean.setParamName33("Parameter Name 33:");
		  if (bean.getParamName34()==null || bean.getParamName34().equalsIgnoreCase(""))
			  bean.setParamName34("Parameter Name 34:");
		  if (bean.getParamName35()==null || bean.getParamName35().equalsIgnoreCase(""))
			  bean.setParamName35("Parameter Name 35:");
		  if (bean.getParamName36()==null || bean.getParamName36().equalsIgnoreCase(""))
			  bean.setParamName36("Parameter Name 36:");
	  }
  }

  /**
   * SetAvailableRequests
   * @param bean
   * @return 
   */
  private void SetAvailableRequests (SolicitBean bean)
  {
    try {
      INodeOperation opDB = DBManager.GetNodeOperation(Phrase.ClientLoggerName);
      bean.setAvailableRequests(opDB.GetSolicits());
    } catch (Exception e) {
      this.Log("Could Not Set Available Requests: " + e.toString(), Level.ERROR);
    }
  }

  /**
   * AddParameter
   * @param bean
   * @return 
   */
  private void AddParameter (SolicitBean bean)
  {
    String temp = bean.getNumParams();
    int num = 0;
    if (!temp.equals(""))
      num = Integer.parseInt(temp);
    if (num < 36)
      num++;
    bean.setNumParams(num+"");
  }

  /**
   * RemoveParameter
   * @param bean
   * @return 
   */
  private void RemoveParameter (SolicitBean bean)
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
   * Solicit
   * @param bean
   * @return 
   */
  private void Solicit (SolicitBean bean)
  {
    String token = bean.getToken();
    if (token != null && token.equals(""))
      token = null;
    String returnURL = bean.getReturnURL();
    if (returnURL != null && returnURL.equals(""))
      returnURL = null;
    String request = bean.getRequest1();
    if (request == null || request.equals(""))
      request = bean.getRequest2();
    if (request != null && request.equals(""))
      request = null;
    String[] params = bean.getParamValues();
    try {
      URL node = new URL(this.GetNodeAddress(bean));
      NodeRequestor requestor = new NodeRequestor(node, Phrase.ClientLoggerName);
      bean.setResult(requestor.solicit(token,returnURL,request,params));
    } catch (RemoteException e) {
      bean.setResult(e.getMessage());
    } catch (Exception e) {
      bean.setError("Could Not Call Solicit");
      this.Log("Could not call Solicit: " + e.toString(), Level.ERROR);
      bean.setResult("");
    }
  }

  /**
   * SolicitV2
   * @param bean
   * @return 
   */
  private void SolicitV2 (SolicitBean bean)
  {
    String token = bean.getToken();
    if (token != null && token.equals(""))
      token = "";
    String dataFlow = bean.getDataFlow();
    if (dataFlow != null && dataFlow.equals(""))
    	dataFlow = "";
    String request = bean.getRequest1();
    if (request == null || request.equals(""))
      request = bean.getRequest2();
    if (request != null && request.equals(""))
      request = "";
    String recipients = bean.getRecipients();
    String[] recipientList = null;    	
    if (recipients != null && !recipients.equals("")){
        recipientList = recipients.split(",");    	
    }
    String notificationURI = bean.getNotificationURI();
    String notificationTypes = bean.getNotificationType();
    String[] notificationURITypeStrList = null;
    if (notificationURI != null && !notificationURI.equals("")){
    	notificationURITypeStrList = notificationURI.split(","); 
    }
    String[] paramsNames = bean.getParamNames();
    String[] params = bean.getParamValues();
    String[] paramTypes = bean.getParamTypes();
    String[] paramEncodes = bean.getParamEncodes();
    try {
      URL node = new URL(this.GetNodeAddress_V2(bean));
      // WI 23609
      for(int i=0;params != null && i< params.length; i++){
    	  if(params[i]==null){
    		  params[i]="";
    	  }
      }
      Node2.webservice.Requestor.NodeRequestor requestor = new Node2.webservice.Requestor.NodeRequestor(node, Phrase.ClientLoggerName);
      bean.setResult(requestor.solicit(token,dataFlow,request,recipientList,notificationURITypeStrList,notificationTypes,paramsNames,params,paramTypes,paramEncodes));
    } catch (RemoteException e) {
        StringWriter sw = new StringWriter();
        e.printStackTrace(new PrintWriter(sw));
        bean.setResult(sw.toString());
    } catch (Exception e) {
      bean.setError("Could Not Call Solicit");
      this.Log("Could not call Solicit: " + e.toString(), Level.ERROR);
      StringWriter sw = new StringWriter();
      e.printStackTrace(new PrintWriter(sw));
      bean.setResult(sw.toString());
    }
  }

}
