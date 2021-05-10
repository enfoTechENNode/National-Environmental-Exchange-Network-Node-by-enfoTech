package Node.API;

import com.enfotech.basecomponent.typelib.xml.XmlDocument;
import org.dom4j.Document;
import java.io.ByteArrayInputStream;
import java.io.File;
import java.io.IOException;
import java.util.List;
import org.dom4j.Node;
import org.dom4j.Attribute;

import com.enfotech.basecomponent.db.IDBAdapter;
import com.enfotech.basecomponent.jndi.JNDIAccess;
import Node.Utils.AppUtils;
import java.io.InputStream;
import Node.API.NodeUtils;
import java.io.FileInputStream;
import Node.API.Stylizer;
import Node.Configuration.JNDI.JNDIResources;
import Node.DB.Interfaces.INodeDB;
import Node.Utils.LoggingUtils;
import Node.Utils.Utility;
import org.apache.log4j.Level;
import Node.Phrase;
import java.util.ArrayList;
import com.enfotech.basecomponent.typelib.xml.XmlNode;

public class ValidationManager {
	/**
	 * <p>This class provides utilities to validate xml file.</p>
	 * <p>Company: enfoTech & Consulting, Inc.</p>
	 * @author enfoTech
	 * @version 2.0
	 */

	public ValidationManager() {
	}

	private boolean debug = false;
	private String validationMessage = null;
	private String validationError = null;
	private XmlDocument ValidationMessageDoc;
	private String jndiName = "jdbc/node";
	private static String root = AppUtils.getAppRoot();
	private String ValidationErrorTemplatePath = (String) JNDIAccess
			.GetJNDIValue("ValidationErrorTemplate", false);

	private String ValidationErrorXSTLPath = (String) JNDIAccess.GetJNDIValue(
			"ValidationErrorXSTL", false);

	/**
	 * Do Validate
	 * 
	 * @param validationRuleContext
	 *            validation rule file
	 * @param xmlContent
	 *            xml file which need to be validated
	 */
	public String PerformValidationForTesting(byte[] validationRuleContext,
			byte[] xmlContent) throws Exception {
		org.dom4j.io.SAXReader reader = new org.dom4j.io.SAXReader();
		Document doc4jVR = reader.read(new ByteArrayInputStream(
				validationRuleContext));
		// set database connection
		List dbs = doc4jVR.selectNodes(".//DBSource/DB");
		boolean isErrorType = false;
		ArrayList myArr = null;

		// set database connection
		if (dbs != null && dbs.size() > 0) {
			for (int i = 0; i < dbs.size(); i++) {
				String type = this
						.getAttributeValue((Node) dbs.get(i), "@name");
				IDBAdapter db = JNDIResources.GetNodeDB();
				String dbConnection = JNDIResources.DBConnectionString;
				if (dbConnection != null && !dbConnection.equalsIgnoreCase("")) {
					Node node = (Node) dbs.get(i);
					Attribute att = (Attribute) node
							.selectSingleNode("//@source");
					att.setText("ORACLE:" + dbConnection);
				}
			}
		}

		Document doc4jXML = null;

		try {
			doc4jXML = reader.read(new ByteArrayInputStream(xmlContent));
		} catch (Exception ex) {
			reader.setEncoding("ISO-8859-1");
			doc4jXML = reader.read(new ByteArrayInputStream(xmlContent));
		}
		ConfigValidation rv = new ConfigValidation(doc4jVR, doc4jXML);
		rv.SetErrorMessage(this.getErrorTemplate());
		rv.Validates();
		this.validationMessage = rv.GetValidateMessage();
		myArr = rv.getErrorTypeList();
		for (int i = 0; i < myArr.size(); i++) {
			XmlNode node = (XmlNode) myArr.get(i);
			if (node.SelectNodes(".//Category").ItemOf(0).GetInnerText()
					.equalsIgnoreCase("error")) {
				isErrorType = true;
				break;
			}
		}
		if (this.validationMessage != null
				&& !this.validationMessage.equalsIgnoreCase("")) {
			Stylizer s = new Stylizer();
			// set parameter
			s.setParameter("rootDir", root);
			// Start Transform
			String xsltpath = null;

			xsltpath = root + ValidationErrorXSTLPath;

			InputStream filecontent = new ByteArrayInputStream(
					this.validationMessage.getBytes());

			String path = Utility.GetTempFilePath();
			// String errorHtmlPath = root + "temp/Node_Error_Report" +
			// Utility.GetSysTimeString() + ".html";
			String errorHtmlPath = path + "/temp/Node_Error_Report"
					+ Utility.GetSysTimeString() + ".html";
			s.transformToFile(xsltpath, filecontent, errorHtmlPath);

			filecontent.close();
			this.validationError = errorHtmlPath;
			// Response
			/*
			 * FileInputStream fi = new FileInputStream(root +
			 * "temp/Node_DCM_error.html"); byte[] output = new
			 * byte[fi.available()]; fi.read(output); InputStream is = new
			 * ByteArrayInputStream(this.validationMessage.getBytes());
			 * Utils.writeFile("c:\\myfile",is);
			 */
			// System.out.println(this.validationMessage);
		} else {
			this.validationError = "ok";
		}
		return this.validationError;
	}

	public String getValidationMessage() {
		return this.validationMessage;
	}

	/**
	 * Get Error Template
	 * @return XmlDocument
	 * @throws Exception
	 */
	protected XmlDocument getErrorTemplate() throws Exception {
		byte[] errMsgTemplateContent = null;
		if (debug) {
			errMsgTemplateContent = Utility
					.readFile("D:\\temp\\ValidationErrorTemplate.xml");
		} else {
			errMsgTemplateContent = Utility.readFile(root
					+ ValidationErrorTemplatePath);
		}
		XmlDocument errMsgTemplate = new XmlDocument();
		errMsgTemplate.Load(new java.io.ByteArrayInputStream(
				errMsgTemplateContent));

		// clean resource
		errMsgTemplateContent = null;

		return errMsgTemplate;
	}

	/**
	 * Get Node attribute value
	 * @return XmlDocument
	 * @throws Exception
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
		}

		return value;
	}

	/**
	 * @param args
	 * @throws Exception
	 */
	public static void main(String[] args) throws Exception {
		ValidationManager t = new ValidationManager();
		// FileContent xmlFile =
		// WQXUtil.readFile("C:\\Program Files\\Apache Software Foundation\\Tomcat 5.0\\webapps\\edwr.web\\config\\template\\dwr\\v2\\12263.xml");
		// FileContent ruleFile =
		// WQXUtil.readFile("C:\\Program Files\\Apache Software Foundation\\Tomcat 5.0\\webapps\\edwr.web\\config\\template\\dwr\\v2\\SDWIS_ValidationConfig_1.xml");
		// byte[] xmlFile = Utils.readFile("c:\\HEEITest.xml");
		// byte[] ruleFile = Utils.readFile("c:\\MyRule.xml");
		byte[] xmlFile = Utility.readFile("D:\\temp\\QAR_test_101z.xml");
		byte[] ruleFile = Utility
				.readFile("D:\\temp\\test_DCM_ValidationConfig_v2z.xml");

		// ByteArrayInputStream bis = new ByteArrayInputStream(xmlFile);
		// ActivityDocument ad = ActivityDocument.Factory.parse(bis);

		t.PerformValidationForTesting(ruleFile, xmlFile);
		System.out.println("ok");

	}

}
