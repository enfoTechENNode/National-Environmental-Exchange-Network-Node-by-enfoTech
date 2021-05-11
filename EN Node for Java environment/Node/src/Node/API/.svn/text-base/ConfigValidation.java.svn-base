package Node.API;

import java.sql.Connection;
import java.sql.ResultSet;
import java.sql.Statement;
import java.util.ArrayList;
import java.util.Hashtable;
import java.util.List;

import org.dom4j.Attribute;
import org.dom4j.Document;
import org.dom4j.Node;

import com.enfotech.basecomponent.db.IDBAdapter;
import com.enfotech.basecomponent.db.OleDBAdapter;
import com.enfotech.basecomponent.db.OracleDBAdapter;
import com.enfotech.basecomponent.db.SQLDBAdapter;
import com.enfotech.basecomponent.typelib.data.DataTable;
import com.enfotech.basecomponent.typelib.xml.XmlDocument;
import com.enfotech.basecomponent.typelib.xml.XmlNode;
import com.enfotech.basecomponent.typelib.xml.XmlNodeList;
import com.enfotech.basecomponent.utility.Utility;

/**
 * <p>Validate configuration</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * 
 * @author enfoTech
 * @version 2.0
 */

public class ConfigValidation {
  /**
   * The constant value of MSSQL.
   */
  public static final String MSSQL = "MSSQL";

  /**
   * The constant value of ORACLE.
   */
  public static final String ORACLE = "ORACLE";

  /**
   * The constant value of UNKNOWN.
   */
  public static final String UNKNOWN = "UNKNOWN";

  protected static final String TYPE = "type";
  protected static final String TARGET = "target";
  protected static final String GOTO = "goto";
  protected static final String STEP = "step";
  protected static final String STOP = "stop";
  protected static final String JUMP = "JUMP";
  protected static final String XPATH = "XPath";
  protected static final String REG_EXP = "RegExp";
  protected static final String MAX_LENGTH = "MaxLength";
  protected static final String MIN_LENGTH = "MinLength";
  protected static final String EQUAL_LENGTH = "EqualLength";
  protected static final String SIG_LENGTH = "SigNumLenMax";
  protected static final String NODE_VALUE = "NodeValue";
  protected static final String SQL_VALUE = "SQLValue";
  protected static final String SQL_DATA_LIST = "SQLDataList";
  protected static final String NODE_EXIST = "NodeExist";
  protected static final String NODE_COUNT = "NodeCount";

  protected Hashtable htDB = new Hashtable();
  protected ArrayList arrMessage = new ArrayList();
  protected Node source = null;
  protected Document ruleDoc = null;
  protected XmlDocument errDoc = null;
  protected Hashtable ruleHash = null;
  private Connection con = null;
  private ArrayList errorTypeList = new ArrayList();

  //protected Node namespaceNode = null;

  /**
   * Create RuleValidation object.
   */
  public ConfigValidation(Document rule, Node sourceNode) {
    this.source = sourceNode;
    this.ruleDoc = rule;
    if (this.source != null) {
      //this.namespaceNode = this.source.OwnerDocument().DocumentElement();
    }

    //get rule hash
    //long getStart = System.currentTimeMillis();
    this.ruleHash = this.getRuleHash(this.ruleDoc);
    //System.out.println("getRuleHash "+(System.currentTimeMillis()-getStart));

  }

  /**
   * Validates the source xml document based on specified rule.
   * @throws Exception If any error occurs.
   */
  public void Validates() throws Exception {
    if (this.source == null || this.ruleDoc == null) {
      return;
    }

    this.arrMessage = new ArrayList();

    // get db connections. sometime they need multiple db connections.
    GetDBConnect();

    List parents = this.ruleDoc.selectNodes(".//ParentNode");
    if (parents.size() == 0) {
      return;
    }

    String parentRoot = "";
    String nodeType = "";
    Node parent = (Node) parents.get(0);
    try{
	    while (parent != null) {
	      boolean bNext = true;

	      //String parentRoot = parent.Attributes().GetNamedItem("root").GetValue();
	      parentRoot = this.getAttributeValue(parent, ".//@root");
	      // added by Alan C.
	      //String nodeType = (parent.Attributes().GetNamedItem("nodeType") == null)?
	      //    "":parent.Attributes().GetNamedItem("nodeType").GetValue();
	      nodeType = this.getAttributeValue(parent, ".//@nodeType");
	      // end added
	      List nodeList = this.source.selectNodes(parentRoot);

	      Hashtable ht = null;

	      String nextTarget = null;
	      //long begin = System.currentTimeMillis();
	      for (int i = 0; i < nodeList.size(); i++) {
	        /*print trace
	        if(i>0)
	        {
	          System.out.println("LOOP_" + (i - 1) + "\t" +
	                             (System.currentTimeMillis() - begin));
	          begin = System.currentTimeMillis();
	        }*/
	        // modified by Alan C.
	        String index = (!nodeType.trim().equalsIgnoreCase("LOOP")) ? "" :
	            (i + 1) + "";
	        // end modified
	        ht = ValidateNodes(parent, (Node) nodeList.get(i), index);
	        if (ht == null) {
	          continue;
	        }
	        else {
	          String nextType = (String) ht.get(TYPE);
	          if (nextType.trim().equalsIgnoreCase(JUMP)) {
	            String target = (String) ht.get(TARGET);
	            if (!target.trim().equals("")) {
	              //parent = this.ruleDoc.SelectSingleNode(
	              //    ".//ParentNode[@id='" + target + "']");
	              List nodes = this.ruleDoc.selectNodes(
	                  ".//ParentNode[@id='" + target + "']");
	              if(nodes==null)
	                parent = null;
	              else
	                parent = (Node)nodes.get(0);
	            }
	            else {
	              //parent = parent.NextSibling();
	              String nextId = this.getNextSiblingId(parent);
	              List nodes = this.ruleDoc.selectNodes(
	                  ".//ParentNode[@id='" + nextId + "']");
	              if(nodes == null || nodes.size()<=0)
	                parent = null;
	              else
	                parent = (Node)nodes.get(0);
	            }
	            bNext =false;
	            break;
	          }
	          else if (nextType.trim().equalsIgnoreCase(STOP)) {
	        	  nextTarget = (String) ht.get(TARGET);
	            continue;
	          }
	        }
	      }
	      //parent = parent.NextSibling();
	      if(bNext)
	      {
	    	  String nextId = (nextTarget == null || nextTarget.equalsIgnoreCase(""))? this.getNextSiblingId(parent) : nextTarget;
		      List nodes = this.ruleDoc.selectNodes(
		                  ".//ParentNode[@id='" + nextId + "']");
		      if(nodes == null || nodes.size()<=0)
		        parent = null;
		      else
		        parent = (Node)nodes.get(0);
	      }
	    }
    }
    catch(Exception ex){
    	throw ex;
    }
    finally{
    	if(con != null){
    		con.close();
    	}
    }
  }

  /**
   * Sets the XmlDocument for Error Message.
   * @param doc XmlDocument
   */
  public void SetErrorMessage(XmlDocument doc) {
    this.errDoc = doc;
  }

  /**
   * Gets the validation message.
   * @throws Exception If any error occurs.
   * @return The String contains the validation message.
   */
  public String GetValidateMessage() throws Exception {
    if (this.arrMessage.size() == 0) {
      return "";
    }

    if (this.errDoc == null) {
      return this.arrMessage.toString();
    }
    else {
      XmlNodeList errorNodes = this.errDoc.SelectNodes(".//ErrorMessage");
      int count = errorNodes.Count();
      if (count == 0) {
        return this.errDoc.OuterXml();
      }
      for (int i = 0; i < this.arrMessage.size(); i++) {
        ArrayList arTemp = (ArrayList)this.arrMessage.get(i);
        XmlNode node = null;
        if (i < count) {
          node = errorNodes.ItemOf(i);
        }
        else {
          XmlNode cloneNode = errorNodes.ItemOf(0).CloneNode(true);
          XmlNode lastNode = errorNodes.ItemOf(count - 1);
          node = lastNode.ParentNode().InsertAfter(cloneNode,
              lastNode);
        }
        // modified by Alan C.
        /*
         node.SelectSingleNode(".//Value").SetInnerText(arTemp.get(0) +
            "");
         node.SelectSingleNode(".//XPath").SetInnerText(arTemp.get(1) +
            "");
         node.SelectSingleNode(".//Location").SetInnerText(arTemp.get(2) +
            "");
         node.SelectSingleNode(".//ErrorMessageText").SetInnerText(
            arTemp.get(3) + "");
         */
        node.SelectNodes(".//Value").ItemOf(0).SetInnerText(arTemp.get(0) +
            "");
        node.SelectNodes(".//XPath").ItemOf(0).SetInnerText(arTemp.get(1) +
            "");
        node.SelectNodes(".//Location").ItemOf(0).SetInnerText(arTemp.get(2) +
            "");
        node.SelectNodes(".//ErrorMessageText").ItemOf(0).SetInnerText(
            arTemp.get(3) + "");
        // end modified
        node.SelectNodes(".//Category").ItemOf(0).SetInnerText(arTemp.get(4) + "");

        if(node != null) this.errorTypeList.add(node);
      }
      //return this.errDoc.OuterXml();
      return com.enfotech.basecomponent.utility.xml.XmlUtility.toXmlString(this.errDoc.node.getOwnerDocument());
    }
  }

  public ArrayList getErrorTypeList(){
    return this.errorTypeList;
  }
  /**
   * Validates the nodes which belong to a Parent Node.
   * @param parentNode The specified parent node in the rule document.
   * @param validateNode The node to be validated which is specified in root attribute in the parent node.
   * @param index The index of validateNode location.
   * @throws Exception Any error occurs.
   * @return A Hashtable contains next step information.
   */
  protected Hashtable ValidateNodes(Node parentNode, Node validateNode,
                                    String index) throws Exception {
	try{
    List nodes = parentNode.selectNodes(".//Node");
    if (nodes.size() == 0) {
      return null;
    }
    Node node = (Node) nodes.get(0);
    Hashtable ht = null;
    while (node != null) {
      ht = ValidateNodeRules(node, validateNode, index);
      if (ht == null) {
        //node = node.NextSibling();
        String nextId = this.getNextSiblingId(node);
        node = (Node)ruleHash.get(nextId);
      }
      else {
        String nextType = (String) ht.get(TYPE);
        if (nextType.trim().equalsIgnoreCase(GOTO)) {
          String target = (String) ht.get(TARGET);
          if (!target.trim().equals("")) {
            node = (Node)ruleHash.get(target);
          }
          else {
            String nextId = this.getNextSiblingId(node);
            node = (Node)ruleHash.get(nextId);
          }
        }
        else {
          return ht;
        }
      }
    }
	}catch(Exception ex){
    	throw ex;
    }
    return null;
  }

  /**
   * Validates the specified node by its rules.
   * @param node The specified node.
   * @param validateNode The node to be validated which is specified in root attribute in the parent node.
   * @param index The index of validateNode location.
   * @throws Exception Any error occurs.
   * @return A Hashtable contains next step information.
   */
  protected Hashtable ValidateNodeRules(Node node, Node validateNode,
                                        String index) throws
      Exception {
    //String type = node.Attributes().GetNamedItem("type").GetValue();
    String type = this.getAttributeValue(node, "@type");
    String nodeValue = null;

    List list = validateNode.selectNodes(node.selectSingleNode(
        ".//NodeValue").getText());
    Node nodeObj = null;
    if(list != null && list.size()>0)
      nodeObj = (Node)list.get(0);

    if (nodeObj == null) {

      /*System.out.println("Can not find '" +
                         ((Node)node.selectNodes(".//NodeValue").get(0)).getText() +
                         "'");
       */
    }

    //get node value.
    if (type.trim().equalsIgnoreCase(XPATH)) {
      //nodeValue = (nodeObj == null) ? null : nodeObj.getText();
    	try{
    	nodeValue = (nodeObj == null) ? null : nodeObj.getStringValue();
    	}catch(Exception ee){
    		System.out.println(ee.toString());
    	}
    }
    //get all rules
    List rules = node.selectNodes(".//NodeRule");
    if (rules.size() == 0) {
      return null;
    }
    Node rule = (Node)rules.get(0);
    Hashtable ht = null;
    String nextType = "";
    //long begin = System.currentTimeMillis();
    while (rule != null) {
      //begin = System.currentTimeMillis();
      ht = ValidateNodeRule(nodeValue, rule, nodeObj, index);
      //System.out.println("NodeRule"+rule.selectSingleNode("@id").getText()+"\t"+(System.currentTimeMillis()-begin));
      nextType = (String) ht.get(TYPE);
      // look up for next rule.
      if (nextType.trim().equalsIgnoreCase(STEP)) {
        String target = (String) ht.get(TARGET);
        if (!target.trim().equals("")) {
          rule = (Node)this.ruleHash.get(target);
        }
        else {
          //rule = rule.NextSibling();
          String nextId = this.getNextSiblingId(rule);
          rule = (Node)this.ruleHash.get(nextId);
        }
      }
      else {
        return ht;
      }
    }
    return null;
  }

  /**
   * Validates the node based on specified rule.
   * @param nodeValue The value of validated node.
   * @param rule The XmlNode rule node.
   * @param validateNode The validateNode defined in root attribute in the parent node.
   * @param index The index of validateNode location.
   * @throws Exception Any error occurs.
   * @return A Hashtable contains the next step info.
   */
  protected Hashtable ValidateNodeRule(String nodeValue, Node rule,
                                       Node validateNode, String index) throws
      Exception {
    String type = null;
    String target = null;
    String category = null;
    String message = null;
    Node resultNode = null;

    String value = GetNodeValue(nodeValue, rule, validateNode);
    if (CheckNodeValue(value, rule, validateNode)) {
      // modified by Alan C.
      //resultNode = rule.SelectSingleNode(".//Yes");
      resultNode = (Node)rule.selectNodes("Yes").get(0);
      // end modified
    }
    else {
      // modified by Alan C.
      //resultNode = rule.SelectSingleNode(".//No");
      resultNode = (Node)rule.selectNodes("No").get(0);
      // end modified
    }
    //type = resultNode.Attributes().GetNamedItem("type").GetValue();
    type = this.getAttributeValue(resultNode, "@type");
    //Node tarNode = resultNode.Attributes().GetNamedItem("target");
    //target = (tarNode == null) ? "" : tarNode.GetValue();
    target = this.getAttributeValue(resultNode, "@target");
    category = this.getAttributeValue(resultNode, "@category");
    message = resultNode.getText();

    if (!message.trim().equals("")) {
      ArrayList arTemp = new ArrayList();
      //arTemp.add(value);
      arTemp.add(this.convertToXmlValue(nodeValue)); //modify by Maggie, always use Node value as value in error message
      arTemp.add(GetXPath(rule));
      arTemp.add(index);
      arTemp.add(message);
      arTemp.add(category);
      this.arrMessage.add(arTemp);
    }
    Hashtable ht = new Hashtable();
    ht.put(TYPE, type);
    ht.put(TARGET, target);
    return ht;
  }

  /**
   * Gets the node value.
   * @param nodeValue The node value for xpath
   * @param rule The XmlNode represents the rule.
   * @param validateNode The XmlNode to be validated.
   * @throws Exception Any error occurs.
   * @return The String value to check.
   */
  protected String GetNodeValue(String nodeValue, Node rule,
                                Node validateNode) throws Exception {
    String value = null;
    String sql = null;
    String dbSource = null;
    IDBAdapter db = null;
    NodeUtils utils = new NodeUtils();

    if (validateNode == null) {
      return value;
    }

    // get value type
    // modified by Alan C.
    //String valueType = rule.SelectSingleNode(".//Value").Attributes().
    //    GetNamedItem("type").GetValue().trim();
    String valueType = this.getAttributeValue(rule.selectSingleNode("Value"), "@type");
    // end modified
    // get value to validate
    if (valueType.equalsIgnoreCase(NODE_VALUE)) {
      value = nodeValue;
    }
    else if (valueType.equalsIgnoreCase(XPATH)) {
      // modified by Alan C.
      //value = validateNode.SelectSingleNode(rule.SelectSingleNode(
      //    ".//Value").GetInnerText(), this.namespaceNode).GetInnerText();
      value = validateNode.selectSingleNode(rule.selectSingleNode("Value").getText()).getText();
      // end modified
    }
    else if (valueType.equalsIgnoreCase(SQL_VALUE)) {
      // modified by Alan C.
      //XmlNode ruleValue = rule.SelectSingleNode(".//Value");
      Node ruleValue = (Node)rule.selectNodes("Value").get(0);
      // end modified
      //dbSource = ruleValue.Attributes().GetNamedItem("source").
      //    GetValue().trim();
      dbSource = this.getAttributeValue(ruleValue, "@source");
      db = (IDBAdapter)this.htDB.get(dbSource);
      if (con == null){
          db.Open();
      }

      con = db.GetConnection();

      Statement stm = con.createStatement();
      ResultSet set = null;
      if (db != null) {
        sql = BuildSQL(ruleValue.getText(), validateNode);
        if (sql == null) {
          return null;
        }
        try{

        	set = stm.executeQuery(sql);
        	if(set!= null){
	        	if(set.next()){
		            value = set.getObject(1) +"";
	        	}
        	}
        }catch(java.sql.SQLException ex){
          throw new Exception("SQL Exception: " + sql);
        }
        finally{
        	if(stm != null){
        		stm.close();
        	}
        	if(set != null){
        		set.close();
        	}
        }
      }

    }
    return value;
  }

  /**
   * Checks the node value against validation rule.
   * @param value The value to check
   * @param rule The XmlNode represents the rule.
   * @param validateNode The XmlNode to be validated.
   * @throws Exception Any error occurs.
   * @return boolean If validates successful, return true; otherwise, false.
   */
  protected boolean CheckNodeValue(String value, Node rule,
                                   Node validateNode) throws Exception {
    String sql = null;
    String dbSource = null;
    IDBAdapter db = null;


    boolean bValidate = false;
    if (value == null || value.trim().equalsIgnoreCase("null")) {
      return bValidate;
    }
    value = value.trim();

    // get validator type.
    // modified by Alan C.
    //XmlNode validator = rule.SelectSingleNode(".//Validator");
    Node validator = (Node)rule.selectNodes("Validator").get(0);
    // end modified
    //String validatorType = validator.Attributes().GetNamedItem("type").
    //    GetValue().trim();
    String validatorType = this.getAttributeValue(validator, "@type");
    if (validatorType.equalsIgnoreCase(REG_EXP)) {
      bValidate = value.trim().matches(validator.getText());
    }
    else if (validatorType.equalsIgnoreCase(SQL_DATA_LIST)) {
      //dbSource = validator.Attributes().GetNamedItem("source").GetValue().
      //    trim();
      try{
      dbSource = this.getAttributeValue(validator, "@source");
      db = (IDBAdapter)this.htDB.get(dbSource);
      if (db != null) {
        DataTable dt = new DataTable();
        sql = BuildSQL(validator.getText(), validateNode);
        if (sql == null) {
          return bValidate;
        }

        db.GetDataTable(sql, dt);
        for (int i = 0; i < dt.Rows().Count(); i++) {
          if (value.equalsIgnoreCase( (String) dt.Rows().Item(i).Get(
              0))) {
            bValidate = true;
            break;
          }
        }
        dt = null;
      }
      db = null;
      }catch(Exception ex){
    	  throw new Exception(
          "Cannot run sql statement.");
      }
    }
    else if (validatorType.equalsIgnoreCase(MAX_LENGTH)) {
      try {
        int len = Integer.parseInt(validator.getText());
        bValidate = value.length() <= len;
      }
      catch (Exception ex) {
        throw new Exception(
            "The specified max length is not an integer.");
      }
    }
    else if (validatorType.equalsIgnoreCase(EQUAL_LENGTH)) {
      try {
        int len = Integer.parseInt(validator.getText());
        bValidate = value.length() == len;
      }
      catch (Exception ex) {
        throw new Exception(
            "The specified equal length is not an integer.");
      }
    }
    else if (validatorType.equalsIgnoreCase(MIN_LENGTH)) {
        try {
          int len = Integer.parseInt(validator.getText());
          bValidate = value.length() >= len;
        }
        catch (Exception ex) {
          throw new Exception(
              "The specified min length is not an integer.");
        }
      }
    else if (validatorType.equalsIgnoreCase(SIG_LENGTH)) {
      try {
        int len = Integer.parseInt(validator.getText());
        bValidate = CheckSignificantNumMaxLength(value, len);
      }
      catch (Exception ex) {
        throw new Exception(
            "The specified max length is not a integer.");
      }
    }
    else if (validatorType.equalsIgnoreCase(NODE_EXIST)) {
	    try {
	      bValidate = validateNode.hasContent();
	    }
	    catch (Exception ex) {
	      throw new Exception(
	          "The specified node does not exist.");
	    }
    }
    else if (validatorType.equalsIgnoreCase(NODE_COUNT)) {
	    try {
	      int len = 0;
	      if(this.getAttributeValue(validator, "@source").indexOf(",")>0){
		      String nodePath[] = this.getAttributeValue(validator, "@source").split(",");
		      for(int i=0; i<nodePath.length; i++){
		    	  List list = validateNode.selectNodes(nodePath[i]);
		    	  len += list.size();
		      }
	      }
	      else{
	    	  String nodePath = this.getAttributeValue(validator, "@source");
	    	  List list = validateNode.selectNodes(nodePath);
	    	  len += list.size();
	      }

	      String cri1[] = null;
	      if( validator.getText().indexOf(",")>0){
	    	  cri1 = validator.getText().split(",");
	      }
	      else{
	    	  cri1 = new String [1];
	    	  cri1[0] = validator.getText();
	      }

	      bValidate = true;
	      for(int i=0; i<cri1.length; i++){
	    	  String cri2[] = cri1[i].split(":");
	    	  String cri3 = cri2[0].toString().trim();
	    	  int cri4 =  Integer.parseInt(cri2[1].toString().trim());
	    	  if(cri3.equalsIgnoreCase("g")){
	    		  if(!(len >cri4)){
	    			  bValidate = false;
	    			  break;
	    		  }
	    	  }
	    	  else if(cri3.equalsIgnoreCase("ge")){
	    		  if(!(len >=cri4)){
	    			  bValidate = false;
	    			  break;
	    		  }
	    	  }
	    	  else if(cri3.equalsIgnoreCase("l")){
	    		  if(!(len <cri4)){
	    			  bValidate = false;
	    			  break;
	    		  }
	    	  }
	    	  else if(cri3.equalsIgnoreCase("le")){
	    		  if(!(len <= cri4)){
	    			  bValidate = false;
	    			  break;
	    		  }
	    	  }
	    	  else if(cri3.equalsIgnoreCase("e")){
	    		  if(!(len == cri4)){
	    			  bValidate = false;
	    			  break;
	    		  }
	    	  }
	      }
	    }
	    catch (Exception ex) {
	      throw new Exception(
	          "The specified node count does not exist.");
	    }
    }
    return bValidate;
  }

  /**
   * Builds the sql statement.
   * @param sql The original sql statement with xpath inside.
   * @param validateNode The XmlNode of validateNode.
   * @throws Exception Any error occurs.
   * @return A SQL statement with replaced node value.
   */
  protected String BuildSQL(String sql, Node validateNode) throws Exception {
    ArrayList arr = new ArrayList();
    String ss = sql;
    while (ss.indexOf("xpath:$") != -1) {
      ss = ss.substring(ss.indexOf("xpath:$") + 7);
      if (ss.indexOf("$") != -1) {
        arr.add(ss.substring(0, ss.indexOf("$")));
        if (ss.length() == ss.indexOf("$") + 1) {
          break;
        }
        else {
          ss = ss.substring(ss.indexOf("$") + 1);
        }
      }
    }
    for (int j = 0; j < arr.size(); j++) {
      String xpath = (String) arr.get(j);
      Node xpathNode = validateNode.selectSingleNode(xpath);
      if (xpathNode == null) {
        //System.out.println("Can not find '" + xpath + "'.");
        return null;
      }
      String value = xpathNode.getText();
      sql = Utility.replace(sql, "xpath:$" + xpath + "$", value.replaceAll("'", "''"));
    }
    // debug
    // System.out.println("SQL statement for validation: " + sql);
    // end debug
    return sql;
  }

  /**
   * Check significant Number maximum length.
   * @param value The value of input number.
   * @param len The length of number.
   * @return true of false.
   */
  protected boolean CheckSignificantNumMaxLength(String value, int len) {
    int result = -1;
    try {
      int temp = 0;
      int index = value.indexOf(".");
      if (index == -1) {
        while (value.endsWith("0")) {
          value = value.substring(0, value.length() - 1);
        }
        temp = Integer.parseInt(value);
      }
      else {
        // modified by Alan C.
        //temp = Integer.parseInt(value.substring(0, index - 1) +
        temp = Integer.parseInt(value.substring(0, index) +
                                (value.substring(index + 1)));
      }
      result = (temp + "").length();
    }
    catch (Exception e) {
      return false;
    }
    return result <= len;
  }

  /**
   * Get the XPath of input Node.
   * @param rule The Node of xml file.
   * @throws Exception Any error occurs.
   * @return The xpath.
   */
  protected String GetXPath(Node rule) throws Exception {
    String xpath = "";
    Node node = rule.getParent();
    if (node != null) {
      Node parentNode = node.getParent();
      if (parentNode != null) {
        //String parentRoot = parentNode.Attributes().GetNamedItem("root").
        //    GetValue();
        String parentRoot = this.getAttributeValue(parentNode, ".//@root");
        List nodeList = this.source.selectNodes(parentRoot);
        //xpath = GetFullPath((Node)nodeList.get(0));
        xpath = GetFullPath((Node)nodeList.get(0));
      }
    }
    //node.Attributes().GetNamedItem("type").GetValue()
    if (this.getAttributeValue(node, ".//@type").equalsIgnoreCase(
        XPATH)) {
      // modified by Alan C.
      //String nodeValue = node.SelectSingleNode("NodeValue").GetInnerText();
      String nodeValue = ((Node)node.selectNodes("NodeValue").get(0)).getText();
      // end modified
      nodeValue = nodeValue.replace('/', '.');
      xpath = xpath + "." + nodeValue;
    }
    return xpath;
  }

  /**
   * Get the Full XPath of input Node.
   * @param node The Node of xml file.
   * @return The xpath.
   */
  protected String GetFullPath(Node node) {
    if (node == null) {
      return "";
    }
/*
    String path = "EN:"+node.getName();
    Node parent = node.getParent();
    if (parent != null) {
      String parentPath = GetFullPath(parent);
      if (parentPath.trim() != "") {
        path = parentPath + "." + "EN:"+node.getName();
      }
    }
    else {
      path = "";
    }
      }
 */
  String path = node.getPath();
  path = path.replaceAll("/", ".").substring(1, path.length());
  return path;
  }

  /**
   * Get the Database connection.
   * @param 
   * @throws Exception Any error occurs.
   * @return 
   */
  protected void GetDBConnect() throws Exception {
    if (this.ruleDoc == null) {
      return;
    }
    Node DbSource = this.ruleDoc.selectSingleNode(
        ".//DBSource");
    if (DbSource == null) {
      return;
    }

    List dbs = DbSource.selectNodes(".//DB");
    for (int i = 0; i < dbs.size(); i++) {
      Node db = (Node)dbs.get(i);
      String name = this.getAttributeValue(db, ".//@name");//db.Attributes().GetNamedItem("name").GetValue();
      String dbSource = this.getAttributeValue(db, ".//@source");//db.Attributes().GetNamedItem("source").GetValue();
      this.htDB.put(name, GetDataAdapter(dbSource));
    }
  }

  /**
   * Get the Database adapter.
   * @param connectionSetting Database connection string
   * @throws Exception Any error occurs.
   * @return Database adapter
   */
  protected IDBAdapter GetDataAdapter(String connectionSetting) throws
      Exception {
    if (connectionSetting == null || connectionSetting.trim().equals("")) {
      return null;
    }
    String[] values = connectionSetting.split(":", 2);
    if (values.length != 2) {
      return null;
    }
    String DBType = values[0];
    String DBConnectionString = values[1];

    if (DBType.equalsIgnoreCase(MSSQL)) {
      return new SQLDBAdapter(DBConnectionString);
    }
    else if (DBType.equalsIgnoreCase(ORACLE)) {
      //return new OracleDBAdapter(DBConnectionString);
      return new OracleDBAdapter(DBConnectionString);
    }
    else {
      return new OleDBAdapter(DBConnectionString);
    }
  }

  /**
   * Get the attribute value of xml Node.
   * @param node The Node of xml file.
   * @param xPath The xPath of xml file
   * @throws 
   * @return String value of attribute
   */
  protected String getAttributeValue(Node node, String xPath) {
    String value = "";

    if (node == null || xPath == null) {
      return value;
    }

    Node attNode = node.selectSingleNode(xPath);
    if (attNode != null) {
      Attribute att = (Attribute) attNode;
      value = att.getValue();
      att = null;
    }
    attNode = null;

    return value;
  }

  /**
   * Get the next sibling ID value of xml Node.
   * @param node The Node of xml file.
   * @throws 
   * @return String value of sibling ID
   */
  protected String getNextSiblingId(Node node)
  {
    String nextId = "";
    String[] Ids = node.selectSingleNode("@id").getText().split("[.]");
    int id = Integer.parseInt(Ids[Ids.length-1]) + 1;
    Ids[Ids.length-1] = id+"";
    for(int i=0; i<Ids.length; i++)
      nextId = (nextId.equalsIgnoreCase("")?Ids[i]:"."+Ids[i]);
    return nextId;
  }

  /**
   * Put the rule content of xml into a hashtable.
   * @param doc The content of xml file.
   * @throws 
   * @return hashtable of rule content
   */
  protected Hashtable getRuleHash(Document doc)
  {
    Hashtable hash = new Hashtable();

    List nodes = doc.selectNodes(".//Node");
    Node node = null;
    String id = "";
    for(int i=0; i<nodes.size(); i++)
    {
      node = (Node)nodes.get(i);
      id = this.getAttributeValue(node, "@id");
      hash.put(id, node);
      List nodeRules = node.selectNodes(".//NodeRule");
      if(nodeRules!=null)
      {
        Node nodeRule = null;
        for(int r=0; r<nodeRules.size(); r++)
        {
          nodeRule = (Node)nodeRules.get(r);
          id = this.getAttributeValue(nodeRule, "@id");
          hash.put(id, nodeRule);
        }
      }
      nodeRules = null;
    }
    nodes = null;
    return hash;
  }

  /**
   * Convert xml specific value.
   * @param value The content of xml file.
   * @throws 
   * @return hashtable of rule content
   */
  public static String convertToXmlValue(String value)
  {
    //String conValue = null;
    if(value == null) return value;
    String[] specialValue = new String[]{"&", "<", ">", "\"", "'"};
    String[] xmlValue = new String[]{"&amp;", "&lt;", "&gt;", "&quot;", "&apos;"};
    for(int i=0; i<specialValue.length; i++)
      value = value.replaceAll(specialValue[i], xmlValue[i]);

    return value;
  }

}
