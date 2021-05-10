package Node.Web.Client.Action.Utility;

import java.io.PrintWriter;
import java.io.StringWriter;
import java.util.Hashtable;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.apache.struts.action.ActionForm;
import org.apache.struts.action.ActionForward;
import org.apache.struts.action.ActionMapping;

import Node.Web.Client.BaseAction;
import Node.Web.Client.Bean.Utility.XMLBuilderBean;

import com.enfotech.basecomponent.db.OracleDBAdapter;
import com.enfotech.basecomponent.typelib.xml.XmlDocument;
import com.enfotech.node.xml.XMLBuilder;
/**
 * <p>This class create XMLBuilderAction.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class XMLBuilderAction
    extends BaseAction {

	  /**
	   * Constructor
	   * @param 
	   * @return 
	   */
  public XMLBuilderAction() {
  }

  /**
   * formExecute
   * @param mapping
   * @param form
   * @param request
   * @param response
   * @return ActionForward
   */
  public ActionForward formExecute(ActionMapping mapping, ActionForm form,
                                   HttpServletRequest request,
                                   HttpServletResponse resonse) throws
      Exception {

    XMLBuilderBean aForm = (XMLBuilderBean) form;
    aForm.setBuilderResult("");

    if (aForm.getAct().equalsIgnoreCase("build")) {

      try {

        XmlDocument template = new XmlDocument();
        template.LoadXml(new String(aForm.getMapFile().getFileData()));
        XMLBuilder builder = new XMLBuilder(template);
        builder.SetDB(new OracleDBAdapter(aForm.getDbConn()));
        Hashtable keys = new Hashtable();
        if (!aForm.getKey1_name().equalsIgnoreCase("") &&
            !aForm.getKey1_value().equalsIgnoreCase("")) {
          keys.put(aForm.getKey1_name(), aForm.getKey1_value());

        }
        if (!aForm.getKey2_name().equalsIgnoreCase("") &&
            !aForm.getKey2_value().equalsIgnoreCase("")) {
          keys.put(aForm.getKey2_name(), aForm.getKey2_value());

        }
        builder.SetKeys(keys);

        byte[] content = builder.Generate();
        String result = new String(content);
        aForm.setBuilderResult(result);
      }
      catch (Exception e) {
        aForm.setBuilderResult(this.stack2string(e));
      }

    }

    return mapping.findForward("success");
  }

  /**
   * stack2string
   * @param e
   * @return String
   */
  public String stack2string(Exception e) {
    try {
      StringWriter sw = new StringWriter();
      PrintWriter pw = new PrintWriter(sw);
      e.printStackTrace(pw);
      return sw.toString();
    }
    catch (Exception e2) {
      return "bad stack2string";
    }
  }

}
