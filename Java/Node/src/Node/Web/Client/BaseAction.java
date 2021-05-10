package Node.Web.Client;

import java.util.Iterator;
import java.util.List;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.HttpSession;

import org.apache.commons.fileupload.DiskFileUpload;
import org.apache.commons.fileupload.FileItem;
import org.apache.log4j.Level;
import org.apache.log4j.Logger;
import org.apache.struts.action.Action;
import org.apache.struts.action.ActionForm;
import org.apache.struts.action.ActionForward;
import org.apache.struts.action.ActionMapping;

import Node.Phrase;
import Node.DB.DBManager;
import Node.DB.Interfaces.Configuration.ISystemConfiguration;
import Node.Utils.LoggingUtils;
/**
 * <p>This class create BaseAction.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public abstract class BaseAction extends Action {
  private HttpServletRequest Request = null;
  private String NodeURLKey = "CLIENT_NODE_URL_KEY";
  private String NodeURLKey_V2 = "CLIENT_NODE_URL_KEY_V2";
  private String TokenKey = "CLIENT_TOKEN_KEY";

  /**
   * Constructor
   * @param 
   * @return 
   */
  public BaseAction() {
    this.SetLoggerLevel();
  }


  /**
   * execute
   * @param mapping
   * @param form
   * @param request
   * @param response
   * @return ActionForward
   */
  public ActionForward execute (ActionMapping mapping, ActionForm form, HttpServletRequest request, HttpServletResponse response) throws Exception
  {
    this.Request = request;
    return formExecute (mapping, form, request, response);
  }

  /**
   * formExecute
   * @param mapping
   * @param form
   * @param request
   * @param response
   * @return ActionForward
   */
  public abstract ActionForward formExecute (ActionMapping mapping, ActionForm form, HttpServletRequest request, HttpServletResponse resonse) throws Exception;

  /**
   * SetPage
   * @param form
   * @return 
   */
  protected void SetPage (BaseBean form)
  {
    HttpSession session = this.Request.getSession();
 //   this.SetClientURLs(form);
    this.SetNodeAddress(session, form);
    this.SetNodeAddress_V2(session, form);
    String token = (String)session.getAttribute(this.TokenKey);
    if (token != null)
      form.setToken(token);
    form.setError("");
    form.setError("");
  }

  /**
   * SetNodeAddress
   * @param session
   * @param form
   * @return 
   */
  private void SetNodeAddress (HttpSession session, BaseBean form)
  {
    String nodeAddress = form.getNodeAddress1();
    if (nodeAddress == null || nodeAddress.equals("")) {
      nodeAddress = form.getNodeAddress2();
      if (nodeAddress == null || nodeAddress.equals("")) {
        nodeAddress = (String)session.getAttribute(this.NodeURLKey);
        if (nodeAddress == null || nodeAddress.equals("")) {
          form.setNodeAddress1("");
          form.setNodeAddress2("");
        }
      }
    }
    if (nodeAddress != null && !nodeAddress.equals("")) {
      session.setAttribute(this.NodeURLKey, nodeAddress);
      if (this.IsNodeAddressOneOfClientURLs(nodeAddress, form)) {
        form.setNodeAddress1(nodeAddress);
        form.setNodeAddress2("");
      }
      else {
        form.setNodeAddress1("");
        form.setNodeAddress2(nodeAddress);
      }
    }
  }

  /**
   * SetNodeAddress_V2
   * @param session
   * @param form
   * @return 
   */
  private void SetNodeAddress_V2 (HttpSession session, BaseBean form)
  {
    String nodeAddress = form.getNodeAddress3();
    if (nodeAddress == null || nodeAddress.equals("")) {
      nodeAddress = form.getNodeAddress4();
      if (nodeAddress == null || nodeAddress.equals("")) {
        nodeAddress = (String)session.getAttribute(this.NodeURLKey_V2);
        if (nodeAddress == null || nodeAddress.equals("")) {
          form.setNodeAddress3("");
          form.setNodeAddress4("");
        }
      }
    }
    if (nodeAddress != null && !nodeAddress.equals("")) {
      session.setAttribute(this.NodeURLKey_V2, nodeAddress);
      if (this.IsNodeAddressOneOfClientURLs_V2(nodeAddress, form)) {
        form.setNodeAddress3(nodeAddress);
        form.setNodeAddress4("");
      }
      else {
        form.setNodeAddress3("");
        form.setNodeAddress4(nodeAddress);
      }
    }
  }

  /**
   * IsNodeAddressOneOfClientURLs
   * @param nodeAddress
   * @param form
   * @return boolean
   */
  private boolean IsNodeAddressOneOfClientURLs (String nodeAddress, BaseBean form)
  {
    boolean retBool = false;
    String[] clientURLs = form.getClientURLs();
    if (clientURLs != null) {
      for (int i = 0; i < clientURLs.length; i++) {
        if (clientURLs[i].equalsIgnoreCase(nodeAddress)) {
          retBool = true;
          break;
        }
      }
    }
    return retBool;
  }

  /**
   * IsNodeAddressOneOfClientURLs_V2
   * @param nodeAddress
   * @param form
   * @return boolean
   */
  private boolean IsNodeAddressOneOfClientURLs_V2 (String nodeAddress, BaseBean form)
  {
    boolean retBool = false;
    String[] clientURLs = form.getClientURLs_V2();
    if (clientURLs != null) {
      for (int i = 0; i < clientURLs.length; i++) {
        if (clientURLs[i].equalsIgnoreCase(nodeAddress)) {
          retBool = true;
          break;
        }
      }
    }
    return retBool;
  }

  /**
   * Log
   * @param message
   * @param level
   * @return 
   */
  protected void Log (String message, Level level)
  {
    LoggingUtils.Log(message, level, Phrase.ClientLoggerName);
  }

  /**
   * GetNodeAddress
   * @param bean
   * @return String
   */
  protected String GetNodeAddress (BaseBean bean)
  {
    String nodeAddress = bean.getNodeAddress1();
    if (nodeAddress == null || nodeAddress.equals(""))
      nodeAddress = bean.getNodeAddress2();
    if (nodeAddress == null || nodeAddress.equals(""))
      bean.setError("No Node Address, Please Select One");
    return nodeAddress;
  }
  
  /**
   * GetNodeAddress_V2
   * @param bean
   * @return String
   */
  protected String GetNodeAddress_V2 (BaseBean bean)
  {
    String nodeAddress = bean.getNodeAddress3();
    if (nodeAddress == null || nodeAddress.equals(""))
      nodeAddress = bean.getNodeAddress4();
    if (nodeAddress == null || nodeAddress.equals(""))
      bean.setError("No Node Address, Please Select One");
    return nodeAddress;
  }

  /**
   * SetToken
   * @param token
   * @return 
   */
  protected void SetToken (String token)
  {
    HttpSession session = this.Request.getSession();
    session.setAttribute(this.TokenKey, token);
  }

  /**
   * GetFileContent
   * @param request
   * @param name
   * @return byte[]
   */
  protected byte[] GetFileContent (HttpServletRequest request, String name)
  {
    byte[] content = null;
    try {
      DiskFileUpload upload = new DiskFileUpload();
      List list = upload.parseRequest(request);
      if (list != null) {
        Iterator iter = list.iterator();
        while (iter.hasNext()) {
          FileItem fileItem = (FileItem)iter.next();
          if(!fileItem.isFormField() && fileItem.getName().equals(name)) {
            content = fileItem.get();
            break;
          }
        }
      }
    } catch (Exception e) {
      content = null;
    }
    return content;
  }

  /**
   * SetLoggerLevel
   * @param 
   * @return 
   */
  private void SetLoggerLevel()
  {
    Logger logger = Logger.getLogger(Phrase.ClientLoggerName);

    // Get Configuration - Set Log Level
    try {
      ISystemConfiguration config = DBManager.GetSystemConfiguration(Phrase.ClientLoggerName);
      if (config != null) {
        Level level = config.GetClientLogLevel();
        if (level != null)
          logger.setLevel(level);
        else
          logger.error("Could Not Find Client Log Level in system.config");
      }
      else
        logger.error("Could not get system.config from Configuration File");
    } catch (Exception e) {
      logger.error("Could Not Get Database Connection for Configuration of Logger");
    }
  }

/*
  private String[] SetClientURLs (BaseBean form)
  {
    String[] clientURLs = null;
    try {
      ISystemConfiguration configDB = DBManager.GetSystemConfiguration(Phrase.ClientLoggerName);
      form.setClientURLs(configDB.GetClientNodeURLs());
    } catch (Exception e) {
      LoggingUtils.Log("Could not get ClientURLs: " + e.toString(), Level.ERROR, Phrase.ClientLoggerName);
    }
    return clientURLs;
  }*/
}
