package Node.API;

import com.enfotech.basecomponent.utility.xml.XmlUtility;
import java.io.InputStream;

/**
 * <p>This class provides utilities to validate XML Schema Files.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */
public class XML {
  XmlUtility Utility = null;

  /**
   * Constructs an object to call XML Utility Functions.
   */
  public XML() {
    this.Utility = new XmlUtility();
  }

  /**
   * Checks validation against schema.
   * @param xml A String value of XML instance with specified schema location link.
   * @return true if validated successfully; otherwise, false. To use ValidateMessage() call to get detailed message.
   */
  public boolean CheckValidation(String xml) {
    return this.Utility.CheckValidation(xml);
  }

  /**
   * Checks validation against schema.
   * @param xml A String value of Xml instance.
   * @param schemaStream A InputStream object contains the schema source.
   * @return true if validated successfully; otherwise, false. To use ValidateMessage() call to get detailed message.
   */
  public boolean CheckValidation(String xml, InputStream schemaStream) {
    return this.Utility.CheckValidation(xml,schemaStream);
  }

  /**
   * Checks validation against schema.
   * @param xml A String value of XML instance.
   * @param schema A Object[] contains all schema information.
   * The object array could be URI of schema, InputStream, InputSource, File.
   * @return true if validated successfully; otherwise, false. To use ValidateMessage() call to get detailed message.
   */
  public boolean CheckValidation(String xml, Object[] schema) {
    return this.Utility.CheckValidation(xml,schema);
  }

  /**
   * Checks validation against schema source.
   * @param xml A String value of XML instance.
   * @param schemaSource A String value of schema source.
   * @return true if validated successfully; otherwise, false. To use ValidateMessage() call to get detailed message.
   */
  public boolean CheckValidation(String xml, String schemaSource) {
    return this.Utility.CheckValidation(xml,schemaSource);
  }

  /**
   * Checks validation against schema URL.
   * @param xml A String value of XML instance.
   * @param schemaURI The URL of schema.
   * @param proxyServer The proxy server name of IP.
   * @param proxyPort The proxy port number.
   * @param userName The user name for authentication of the proxy server. use null value if do not need authentication.
   * @param password The password for authentication of the proxy server.
   * @return true if validated successfully; otherwise, false. To use ValidateMessage() call to get detailed message.
   */
  public boolean CheckValidation(String xml, String schemaURI, String proxyServer, String proxyPort, String userName, String password) {
    return this.Utility.CheckValidation(xml,schemaURI,proxyServer,proxyPort,userName,password);
  }

  /**
   * Checks if the Xml instance is well formed.
   * @param xml A String value of XML instance.
   * @return true if validated successfully; otherwise, false. To use ValidateMessage() call to get detailed message.
   */
  public boolean CheckWellFormed(String xml) {
    return this.Utility.CheckWellFormed(xml);
  }

  /**
   * Gets the validation message.
   * @return A String value of validation message.
   */
  public String ValidateMessage() {
    return this.Utility.ValidateMessage();
  }
}
