package Node.Biz.Handler;

import com.enfotech.basecomponent.typelib.xml.XmlNode;
import java.lang.reflect.Method;
import java.math.BigInteger;
import java.rmi.RemoteException;

import net.exchangenetwork.www.schema.node._2.NotificationMessageType;
import net.exchangenetwork.www.schema.node._2.NotificationURIType;
import Node.Biz.Custom.*;
import Node.Phrase;
import Node.WebServices.Document.ClsNodeDocument;
/**
 * <p>This class create Reflection Process.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class Reflection {
	  /**
	   * Constructor.
	   * @param 
	   * @return 
	   */
  private Reflection() {
  }

  public static Object Execute (String webMethod, int type, XmlNode node, Object[] params) throws RemoteException
  {
    Object retObject = null;
    try {
      XmlNode classNameNode = node.SelectSingleNode("ClassName");
      XmlNode methodNameNode = node.SelectSingleNode("MethodName");
      if (classNameNode != null && methodNameNode != null)
        retObject = Reflection.Execute(webMethod, type, classNameNode.GetInnerText(), methodNameNode.GetInnerText(), params);
    } catch (RemoteException e) {
      throw e;
    } catch (Exception e) {
      throw new RemoteException(Phrase.InternalError, e);
    }
    return retObject;
  }

  /**
   * Execute.
   * @param webMethod Web method
   * @param type Process type
   * @param className Process class name
   * @param methodName Method Name
   * @param params Input parameters
   * @return Object Result
   */
  public static Object Execute (String webMethod, int type, String className, String methodName, Object[] params) throws RemoteException
  {
    Object retObject = null;
    try {
      Class executeClass = Class.forName(className);
      Class[] interfaces = executeClass.getInterfaces();
      if (interfaces != null && interfaces.length > 0) {
        Object obj = executeClass.newInstance();
        for (int i = 0; i < interfaces.length; i++) {
          String name = interfaces[i].getName();
          // Authenticate
          if (name.equalsIgnoreCase("Node.Biz.Interfaces.Authenticate.IPreProcess")
              && webMethod.equalsIgnoreCase(Phrase.WEB_METHOD_AUTHENTICATE) && type == Phrase.PRE_PROCESS) {
            Node.Biz.Interfaces.Authenticate.IPreProcess executeInterface = (Node.Biz.Interfaces.Authenticate.IPreProcess)obj;
            if(params.length>4){	// Execute 2.0 
            	String t = "";
            	((PreParam)params[4]).GetHashtable().put("domainName", (Object)params[3]==null?t:(String)params[3]);
                retObject = executeInterface.Execute((String)params[0],(String)params[1],(String)params[2],(PreParam)params[4]);
            }else{	// Execute 1.1 
                retObject = executeInterface.Execute((String)params[0],(String)params[1],(String)params[2],(PreParam)params[3]);
            }
          }
          else if (name.equalsIgnoreCase("Node.Biz.Interfaces.Authenticate.IProcess")
              && webMethod.equalsIgnoreCase(Phrase.WEB_METHOD_AUTHENTICATE) && type == Phrase.PROCESS) {
            Node.Biz.Interfaces.Authenticate.IProcess executeInterface = (Node.Biz.Interfaces.Authenticate.IProcess)obj;
            if(params.length>4){	// Execute 2.0 
            	String t = "";
            	((ProcParam)params[4]).GetHashtable().put("domainName", (Object)params[3]==null?t:(String)params[3]);
                retObject = executeInterface.Execute((String)params[0],(String)params[1],(String)params[2],(ProcParam)params[4]);
            }else{	// Execute 1.1 
                retObject = executeInterface.Execute((String)params[0],(String)params[1],(String)params[2],(ProcParam)params[3]);
            }
          }
          else if (name.equalsIgnoreCase("Node.Biz.Interfaces.Authenticate.IPostProcess")
              && webMethod.equalsIgnoreCase(Phrase.WEB_METHOD_AUTHENTICATE) && type == Phrase.POST_PROCESS) {
            Node.Biz.Interfaces.Authenticate.IPostProcess executeInterface = (Node.Biz.Interfaces.Authenticate.IPostProcess)obj;
            if(params.length>4){	// Execute 2.0 
            	String t = "";
            	((PostParam)params[4]).GetHashtable().put("domainName", (Object)params[3]==null?t:(String)params[3]);
                retObject = executeInterface.Execute((String)params[0],(String)params[1],(String)params[2],(PostParam)params[4]);
            }else{	// Execute 1.1 
                retObject = executeInterface.Execute((String)params[0],(String)params[1],(String)params[2],(PostParam)params[3]);
            }
          }
          // Download
          else if (name.equalsIgnoreCase("Node.Biz.Interfaces.Download.IPreProcess")
              && webMethod.equalsIgnoreCase(Phrase.WEB_METHOD_DOWNLOAD) && type == Phrase.PRE_PROCESS) {
            Node.Biz.Interfaces.Download.IPreProcess executeInterface = (Node.Biz.Interfaces.Download.IPreProcess)obj;
            retObject = executeInterface.Execute((String)params[0],(String)params[1],(String)params[2],(ClsNodeDocument[])params[3],(PreParam)params[4]);
          }
          else if (name.equalsIgnoreCase("Node.Biz.Interfaces.Download.IProcess")
              && webMethod.equalsIgnoreCase(Phrase.WEB_METHOD_DOWNLOAD) && type == Phrase.PROCESS) {
            Node.Biz.Interfaces.Download.IProcess executeInterface = (Node.Biz.Interfaces.Download.IProcess)obj;
            retObject = executeInterface.Execute((String)params[0],(String)params[1],(String)params[2],(ClsNodeDocument[])params[3],(ProcParam)params[4]);
          }
          else if (name.equalsIgnoreCase("Node.Biz.Interfaces.Download.IPostProcess")
              && webMethod.equalsIgnoreCase(Phrase.WEB_METHOD_DOWNLOAD) && type == Phrase.POST_PROCESS) {
            Node.Biz.Interfaces.Download.IPostProcess executeInterface = (Node.Biz.Interfaces.Download.IPostProcess)obj;
            retObject = executeInterface.Execute((String)params[0],(String)params[1],(String)params[2],(ClsNodeDocument[])params[3],(PostParam)params[4]);
          }
          // GetServices
          else if (name.equalsIgnoreCase("Node.Biz.Interfaces.GetServices.IPreProcess")
              && webMethod.equalsIgnoreCase(Phrase.WEB_METHOD_GETSERVICES) && type == Phrase.PRE_PROCESS) {
            Node.Biz.Interfaces.GetServices.IPreProcess executeInterface = (Node.Biz.Interfaces.GetServices.IPreProcess)obj;
            retObject = executeInterface.Execute((String)params[0],(String)params[1],(PreParam)params[2]);
          }
          else if (name.equalsIgnoreCase("Node.Biz.Interfaces.GetServices.IProcess")
              && webMethod.equalsIgnoreCase(Phrase.WEB_METHOD_GETSERVICES) && type == Phrase.PROCESS) {
            Node.Biz.Interfaces.GetServices.IProcess executeInterface = (Node.Biz.Interfaces.GetServices.IProcess)obj;
            if(params.length>3){	// Execute 2.0 
            	((ProcParam)params[3]).GetHashtable().put("version2", (String)params[2]);
                retObject = executeInterface.Execute((String)params[0],(String)params[1],(ProcParam)params[3]);
            }else{	// Execute 1.1 
                retObject = executeInterface.Execute((String)params[0],(String)params[1],(ProcParam)params[2]);
            }
          }
          else if (name.equalsIgnoreCase("Node.Biz.Interfaces.GetServices.IPostProcess")
              && webMethod.equalsIgnoreCase(Phrase.WEB_METHOD_GETSERVICES) && type == Phrase.POST_PROCESS) {
            Node.Biz.Interfaces.GetServices.IPostProcess executeInterface = (Node.Biz.Interfaces.GetServices.IPostProcess)obj;
            retObject = executeInterface.Execute((String)params[0],(String)params[1],(PostParam)params[2]);
          }
          // GetStatus
          else if (name.equalsIgnoreCase("Node.Biz.Interfaces.GetStatus.IPreProcess")
              && webMethod.equalsIgnoreCase(Phrase.WEB_METHOD_GETSTATUS) && type == Phrase.PRE_PROCESS) {
            Node.Biz.Interfaces.GetStatus.IPreProcess executeInterface = (Node.Biz.Interfaces.GetStatus.IPreProcess)obj;
            retObject = executeInterface.Execute((String)params[0],(String)params[1],(PreParam)params[2]);
          }
          else if (name.equalsIgnoreCase("Node.Biz.Interfaces.GetStatus.IProcess")
              && webMethod.equalsIgnoreCase(Phrase.WEB_METHOD_GETSTATUS) && type == Phrase.PROCESS) {
            Node.Biz.Interfaces.GetStatus.IProcess executeInterface = (Node.Biz.Interfaces.GetStatus.IProcess)obj;
            retObject = executeInterface.Execute((String)params[0],(String)params[1],(ProcParam)params[2]);
          }
          else if (name.equalsIgnoreCase("Node.Biz.Interfaces.GetStatus.IPostProcess")
              && webMethod.equalsIgnoreCase(Phrase.WEB_METHOD_GETSTATUS) && type == Phrase.POST_PROCESS) {
            Node.Biz.Interfaces.GetStatus.IPostProcess executeInterface = (Node.Biz.Interfaces.GetStatus.IPostProcess)obj;
            retObject = executeInterface.Execute((String)params[0],(String)params[1],(PostParam)params[2]);
          }
          // NodePing
          else if (name.equalsIgnoreCase("Node.Biz.Interfaces.NodePing.IPreProcess")
              && webMethod.equalsIgnoreCase(Phrase.WEB_METHOD_NODEPING) && type == Phrase.PRE_PROCESS) {
            Node.Biz.Interfaces.NodePing.IPreProcess executeInterface = (Node.Biz.Interfaces.NodePing.IPreProcess)obj;
            retObject = executeInterface.Execute((String)params[0],(PreParam)params[1]);
          }
          else if (name.equalsIgnoreCase("Node.Biz.Interfaces.NodePing.IProcess")
              && webMethod.equalsIgnoreCase(Phrase.WEB_METHOD_NODEPING) && type == Phrase.PROCESS) {
            Node.Biz.Interfaces.NodePing.IProcess executeInterface = (Node.Biz.Interfaces.NodePing.IProcess)obj;
            retObject = executeInterface.Execute((String)params[0],(ProcParam)params[1]);
          }
          else if (name.equalsIgnoreCase("Node.Biz.Interfaces.NodePing.IPostProcess")
              && webMethod.equalsIgnoreCase(Phrase.WEB_METHOD_NODEPING) && type == Phrase.POST_PROCESS) {
            Node.Biz.Interfaces.NodePing.IPostProcess executeInterface = (Node.Biz.Interfaces.NodePing.IPostProcess)obj;
            retObject = executeInterface.Execute((String)params[0],(PostParam)params[1]);
          }
          // Notify
          else if (name.equalsIgnoreCase("Node.Biz.Interfaces.Notify.IPreProcess")
              && webMethod.equalsIgnoreCase(Phrase.WEB_METHOD_NOTIFY) && type == Phrase.PRE_PROCESS) {
            Node.Biz.Interfaces.Notify.IPreProcess executeInterface = (Node.Biz.Interfaces.Notify.IPreProcess)obj;
            if(params.length>5){	// Execute 2.0
            	String t = "";
            	((PreParam)params[5]).GetHashtable().put("messageType", (Object)params[4]==null?t:(String)params[4]);
                retObject = executeInterface.Execute((String)params[0],(String)params[1],(String)params[2],(ClsNodeDocument[])params[3],(PreParam)params[5]);
            }else{	// Execute 1.1 
                retObject = executeInterface.Execute((String)params[0],(String)params[1],(String)params[2],(ClsNodeDocument[])params[3],(PreParam)params[4]);
            }
          }
          else if (name.equalsIgnoreCase("Node.Biz.Interfaces.Notify.IProcess")
              && webMethod.equalsIgnoreCase(Phrase.WEB_METHOD_NOTIFY) && type == Phrase.PROCESS) {
            Node.Biz.Interfaces.Notify.IProcess executeInterface = (Node.Biz.Interfaces.Notify.IProcess)obj;
            if(params.length>5){	// Execute 2.0 
            	String t = "";
            	((ProcParam)params[5]).GetHashtable().put("messageType", (Object)params[4]==null?t:(Object)params[4]);
                retObject = executeInterface.Execute((String)params[0],(String)params[1],(String)params[2],(ClsNodeDocument[])params[3],(ProcParam)params[5]);
            }else{	// Execute 1.1 
                retObject = executeInterface.Execute((String)params[0],(String)params[1],(String)params[2],(ClsNodeDocument[])params[3],(ProcParam)params[4]);
            }
          }
          else if (name.equalsIgnoreCase("Node.Biz.Interfaces.Notify.IPostProcess")
              && webMethod.equalsIgnoreCase(Phrase.WEB_METHOD_NOTIFY) && type == Phrase.POST_PROCESS) {
            Node.Biz.Interfaces.Notify.IPostProcess executeInterface = (Node.Biz.Interfaces.Notify.IPostProcess)obj;
            if(params.length>5){	// Execute 2.0 
            	String t = "";
            	((PostParam)params[5]).GetHashtable().put("messageType", (Object)params[4]==null?t:(String)params[4]);
                retObject = executeInterface.Execute((String)params[0],(String)params[1],(String)params[2],(ClsNodeDocument[])params[3],(PostParam)params[5]);
            }else{	// Execute 1.1 
                retObject = executeInterface.Execute((String)params[0],(String)params[1],(String)params[2],(ClsNodeDocument[])params[3],(PostParam)params[4]);
            }
          }
          // Query
          else if (name.equalsIgnoreCase("Node.Biz.Interfaces.Query.IPreProcess")
              && webMethod.equalsIgnoreCase(Phrase.WEB_METHOD_QUERY) && type == Phrase.PRE_PROCESS) {
            Node.Biz.Interfaces.Query.IPreProcess executeInterface = (Node.Biz.Interfaces.Query.IPreProcess)obj;
            if(params.length>6){	// Execute 2.0 
            	String t = "";
            	((PreParam)params[6]).GetHashtable().put("dataflow", (Object)params[5]==null?t:(String)params[5]);
                retObject = executeInterface.Execute((String)params[0],(String)params[1],(BigInteger)params[2],(BigInteger)params[3],(Object[])params[4],(PreParam)params[6]);
            }
            else{// Execute 1.1 
            	retObject = executeInterface.Execute((String)params[0],(String)params[1],(BigInteger)params[2],(BigInteger)params[3],(Object[])params[4],(PreParam)params[5]);
            }
          }
          else if (name.equalsIgnoreCase("Node.Biz.Interfaces.Query.IProcess")
              && webMethod.equalsIgnoreCase(Phrase.WEB_METHOD_QUERY) && type == Phrase.PROCESS) {
            Node.Biz.Interfaces.Query.IProcess executeInterface = (Node.Biz.Interfaces.Query.IProcess)obj;
            if(params.length>6){	// Execute 2.0 
            	String t = "";
            	((ProcParam)params[6]).GetHashtable().put("dataflow", (Object)params[5]==null?t:(String)params[5]);
                retObject = executeInterface.Execute((String)params[0],(String)params[1],(BigInteger)params[2],(BigInteger)params[3],(Object[])params[4],(ProcParam)params[6]);
           }else{// Execute 1.1 
            	retObject = executeInterface.Execute((String)params[0],(String)params[1],(BigInteger)params[2],(BigInteger)params[3],(Object[])params[4],(ProcParam)params[5]);
            }
           }
          else if (name.equalsIgnoreCase("Node.Biz.Interfaces.Query.IPostProcess")
              && webMethod.equalsIgnoreCase(Phrase.WEB_METHOD_QUERY) && type == Phrase.POST_PROCESS) {
            Node.Biz.Interfaces.Query.IPostProcess executeInterface = (Node.Biz.Interfaces.Query.IPostProcess)obj;
            if(params.length>6){	// Execute 2.0 
            	String t = "";
            	((PostParam)params[6]).GetHashtable().put("dataflow", (Object)params[5]==null?t:(String)params[5]);
                retObject = executeInterface.Execute((String)params[0],(String)params[1],(BigInteger)params[2],(BigInteger)params[3],(Object[])params[4],(PostParam)params[6]);
            }else{// Execute 1.1 
                retObject = executeInterface.Execute((String)params[0],(String)params[1],(BigInteger)params[2],(BigInteger)params[3],(Object[])params[4],(PostParam)params[5]);            	
            }
          }
          /* Solicit
          else if (name.equalsIgnoreCase("Node.Biz.Interfaces.Solicit.IPreProcess")
              && webMethod.equalsIgnoreCase(Phrase.WEB_METHOD_SOLICIT) && type == Phrase.PRE_PROCESS) {
            Node.Biz.Interfaces.Solicit.IPreProcess executeInterface = (Node.Biz.Interfaces.Solicit.IPreProcess)obj;
            retObject = executeInterface.Execute((String)params[0],(String)params[1],(String)params[2],(Object[])params[3],(PreParam)params[4]);
          }
          else if (name.equalsIgnoreCase("Node.Biz.Interfaces.Solicit.IProcess")
              && webMethod.equalsIgnoreCase(Phrase.WEB_METHOD_SOLICIT) && type == Phrase.PROCESS) {
            Node.Biz.Interfaces.Solicit.IProcess executeInterface = (Node.Biz.Interfaces.Solicit.IProcess)obj;
            retObject = executeInterface.Execute((String)params[0],(String)params[1],(String)params[2],(Object[])params[3],(ProcParam)params[4]);
          }
          else if (name.equalsIgnoreCase("Node.Biz.Interfaces.Solicit.IPostProcess")
              && webMethod.equalsIgnoreCase(Phrase.WEB_METHOD_SOLICIT) && type == Phrase.POST_PROCESS) {
            Node.Biz.Interfaces.Solicit.IPostProcess executeInterface = (Node.Biz.Interfaces.Solicit.IPostProcess)obj;
            retObject = executeInterface.Execute((String)params[0],(String)params[1],(String)params[2],(Object[])params[3],(PostParam)params[4]);
          }*/
          // Submit
          else if (name.equalsIgnoreCase("Node.Biz.Interfaces.Submit.IPreProcess")
              && webMethod.equalsIgnoreCase(Phrase.WEB_METHOD_SUBMIT) && type == Phrase.PRE_PROCESS) {
            Node.Biz.Interfaces.Submit.IPreProcess executeInterface = (Node.Biz.Interfaces.Submit.IPreProcess)obj;
            if(params.length>7){	// Execute 2.0 
            	String t = "";
            	((PreParam)params[8]).GetHashtable().put("flowOperation", (String)params[3]==null?t:(String)params[3]);
            	String[] tList = {""};
            	NotificationURIType[] nList = new NotificationURIType[1];
            	nList[0] = new NotificationURIType();
            	((PreParam)params[8]).GetHashtable().put("recipients", params[4]==null?tList:(String[])params[4]);
            	((PreParam)params[8]).GetHashtable().put("notificationURIType", params[5]==null?nList:(NotificationURIType[])params[5]);
            	((PreParam)params[8]).GetHashtable().put("notificationType", params[6]==null?t:(String)params[6]);
            	// Here input flowoperation as dataflow for backwards compatible 
                retObject = executeInterface.Execute((String)params[0],(String)params[1],(String)params[3],(ClsNodeDocument[])params[7],(PreParam)params[8]);
            }else{	// Execute 1.1 
                retObject = executeInterface.Execute((String)params[0],(String)params[1],(String)params[2],(ClsNodeDocument[])params[3],(PreParam)params[4]);
            }
          }
          else if (name.equalsIgnoreCase("Node.Biz.Interfaces.Submit.IProcess")
              && webMethod.equalsIgnoreCase(Phrase.WEB_METHOD_SUBMIT) && type == Phrase.PROCESS) {
            Node.Biz.Interfaces.Submit.IProcess executeInterface = (Node.Biz.Interfaces.Submit.IProcess)obj;
            if(params.length>7){	// Execute 2.0 
            	String t = "";
            	((ProcParam)params[8]).GetHashtable().put("flowOperation", (String)params[3]==null?t:(String)params[3]);
            	String[] tList = {""};
            	NotificationURIType[] nList =  new NotificationURIType[1];
            	nList[0] = new NotificationURIType();
            	((ProcParam)params[8]).GetHashtable().put("recipients", params[4]==null?tList:(String[])params[4]);
            	((ProcParam)params[8]).GetHashtable().put("notificationURIType", params[5]==null?nList:(NotificationURIType[])params[5]);
            	((ProcParam)params[8]).GetHashtable().put("notificationType", params[6]==null?t:(String)params[6]);
            	// Here input flowoperation as dataflow for backwards compatible 
                retObject = executeInterface.Execute((String)params[0],(String)params[1],(String)params[3],(ClsNodeDocument[])params[7],(ProcParam)params[8]);
            }else{	// Execute 1.1 
            	retObject = executeInterface.Execute((String)params[0],(String)params[1],(String)params[2],(ClsNodeDocument[])params[3],(ProcParam)params[4]);
            }
          }
          else if (name.equalsIgnoreCase("Node.Biz.Interfaces.Submit.IPostProcess")
              && webMethod.equalsIgnoreCase(Phrase.WEB_METHOD_SUBMIT) && type == Phrase.POST_PROCESS) {
            Node.Biz.Interfaces.Submit.IPostProcess executeInterface = (Node.Biz.Interfaces.Submit.IPostProcess)obj;
            if(params.length>7){	// Execute 2.0 
            	String t = "";
            	((PostParam)params[8]).GetHashtable().put("flowOperation", (String)params[3]==null?t:(String)params[3]);
            	String[] tList = {""};
            	NotificationURIType[] nList =  new NotificationURIType[1];
            	nList[0] = new NotificationURIType();
            	((PostParam)params[8]).GetHashtable().put("recipients", params[4]==null?tList:(String[])params[4]);
            	((PostParam)params[8]).GetHashtable().put("notificationURIType", params[5]==null?nList:(NotificationURIType[])params[5]);
            	((PostParam)params[8]).GetHashtable().put("notificationType", params[6]==null?t:(String)params[6]);
            	// Here input flowoperation as dataflow for backwards compatible 
                retObject = executeInterface.Execute((String)params[0],(String)params[1],(String)params[3],(ClsNodeDocument[])params[7],(PostParam)params[8]);
            }else{	// Execute 1.1 
                retObject = executeInterface.Execute((String)params[0],(String)params[1],(String)params[2],(ClsNodeDocument[])params[3],(PostParam)params[4]);
            }
          }
          // Execute
          else if (name.equalsIgnoreCase("Node.Biz.Interfaces.Execute.IPreProcess")
              && webMethod.equalsIgnoreCase(Phrase.WEB_METHOD_EXECUTE) && type == Phrase.PRE_PROCESS) {
            Node.Biz.Interfaces.Execute.IPreProcess executeInterface = (Node.Biz.Interfaces.Execute.IPreProcess)obj;
            retObject = executeInterface.Execute((String)params[0],(String)params[1],(String)params[2],(Object[])params[3],(PreParam)params[4]);
          }
          else if (name.equalsIgnoreCase("Node.Biz.Interfaces.Execute.IProcess")
              && webMethod.equalsIgnoreCase(Phrase.WEB_METHOD_EXECUTE) && type == Phrase.PROCESS) {
            Node.Biz.Interfaces.Execute.IProcess executeInterface = (Node.Biz.Interfaces.Execute.IProcess)obj;
            retObject = executeInterface.Execute((String)params[0],(String)params[1],(String)params[2],(Object[])params[3],(ProcParam)params[4]);
          }
          else if (name.equalsIgnoreCase("Node.Biz.Interfaces.Execute.IPostProcess")
              && webMethod.equalsIgnoreCase(Phrase.WEB_METHOD_EXECUTE) && type == Phrase.POST_PROCESS) {
            Node.Biz.Interfaces.Execute.IPostProcess executeInterface = (Node.Biz.Interfaces.Execute.IPostProcess)obj;
            retObject = executeInterface.Execute((String)params[0],(String)params[1],(String)params[2],(Object[])params[3],(PostParam)params[4]);
          }
        }
      }
    } catch (RemoteException e) {
      throw e;
    } catch (Exception e) {
      throw new RemoteException(Phrase.InternalError, e);
    }
    return retObject;
  }

  /**
   * Invoke.
   * @param className Process class name
   * @param methodName Method Name
   * @param params Input parameters
   * @return Object Result
   */
  public static Object Invoke (String className, String methodName, Object[] params) throws RemoteException
  {
    Object retObj = null;
    try {
      Class executeClass = Class.forName(className);
      Method[] methods = executeClass.getMethods();
      if (methods != null) {
        Method method = null;
        for (int i = 0; i < methods.length; i++) {
          if (methods[i].getName().equalsIgnoreCase(methodName)) {
            method = methods[i];
            break;
          }
        }
        if (method != null) {
          Class[] paramTypes = method.getParameterTypes();
          if (paramTypes != null && paramTypes.length == params.length) {
            boolean okay = true;
            for (int i = 0; i < paramTypes.length; i++) {
              if (!paramTypes[i].getName().equals(params[i].getClass().getName())) {
                okay = false;
                break;
              }
            }
            if (okay)
              retObj = method.invoke(executeClass.newInstance(), params);
          }
        }
      }
    } catch (Exception e) {
      throw new RemoteException(Phrase.InternalError,e);
    }
    return retObj;
  }
}
