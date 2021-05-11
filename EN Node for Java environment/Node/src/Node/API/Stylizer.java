package Node.API;

// For write operation
import javax.xml.transform.Transformer;
import javax.xml.transform.TransformerFactory;
import javax.xml.transform.stream.StreamSource;
import javax.xml.transform.stream.StreamResult;
import org.xml.sax.InputSource;

import java.io.*;
import javax.xml.transform.sax.SAXSource;
import java.util.Hashtable;

/**
 * <p>This class provides utilities to transfer xml file using stylesheet.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */
public class Stylizer {

  protected Hashtable parms = new Hashtable();

  public static void main(String[] parms) {
    try
    {
      /*Stylizer s = new Stylizer();
      s.transform("D:\\a\\EDWR_v2.2_to_EDI.xslt",
                  new java.io.
                  FileInputStream("D:\\a\\submission.xml"));
      ByteArrayOutputStream out = s.getOutput();
      java.io.FileOutputStream f = new java.io.FileOutputStream("d:\\a\\out.txt");
      f.write(out.toByteArray());
      f.close();
      f = null;*/
      Stylizer s = new Stylizer();
      FileInputStream fi = new FileInputStream("E:\\temp\\SubmissionXMLFile4495.xml");
      s.transformToFile("E:\\temp\\Node_TemplateFile_tmp20090729035240.xslt",fi,"E:\\temp\\myfile.html");
      fi.close();
      System.out.println("ok.");
      fi = null;
    }
    catch(Exception e)
    {
      e.printStackTrace();
    }

  }

  protected java.io.ByteArrayOutputStream output;

	/**
	 * Transform a stylesheet file
	 * @param stylesheetUrl
	 *            stylesheet template file url
	 * @param datafile
	 *            xml file
	 * @throws Exception
	 *             is an error occurs.
	 * @return transaction id if successful.
	 */
  public void transform(String stylesheetUrl, InputStream datafile) throws
      Exception {
    //factory.setNamespaceAware(true);
    //factory.setValidating(true);
    try {
      File stylesheet = new File(stylesheetUrl);

      // Use a Transformer for output
      TransformerFactory tFactory =
          TransformerFactory.newInstance();
      StreamSource stylesource = new StreamSource(stylesheet);
      Transformer transformer = tFactory.newTransformer(stylesource);

      BufferedInputStream bis = new BufferedInputStream(datafile);
      InputSource input = new InputSource(bis);
      SAXSource ssource = new SAXSource(input);

      output = new ByteArrayOutputStream();
      StreamResult result = new StreamResult(output);

      //set parameters
      if(!this.parms.isEmpty())
      {
        String name = "";
        String value = "";
        java.util.Enumeration keys = this.parms.keys();
        while(keys.hasMoreElements())
        {
          Object key = keys.nextElement();
          name = key+"";
          value = this.parms.get(key)+"";
          transformer.setParameter(name, value);
        }
      }

      transformer.transform(ssource, result);

    }
    catch (Exception e) {
      throw e;
    }
  }

	/**
	 * transformToFile a stylesheet file
	 * @param stylesheetUrl
	 *            stylesheet template file url
	 * @param datafile
	 *            xml file
	 * @param fileDir
	 *            output file
	 * @throws Exception
	 *             is an error occurs.
	 * @return transaction id if successful.
	 */
  public void transformToFile(String stylesheetUrl, InputStream datafile, String fileDir) throws
      Exception {
    try {
      File stylesheet = new File(stylesheetUrl);

      // Use a Transformer for output
      TransformerFactory tFactory =
          TransformerFactory.newInstance();
      StreamSource stylesource = new StreamSource(stylesheet);
      Transformer transformer = tFactory.newTransformer(stylesource);

      BufferedInputStream bis = new BufferedInputStream(datafile);
      InputSource input = new InputSource(bis);
      SAXSource ssource = new SAXSource(input);

      //output = new ByteArrayOutputStream();
      FileOutputStream output = new FileOutputStream(fileDir);
      StreamResult result = new StreamResult(output);

      //set parameters
      if(!this.parms.isEmpty())
      {
        String name = "";
        String value = "";
        java.util.Enumeration keys = this.parms.keys();
        while(keys.hasMoreElements())
        {
          Object key = keys.nextElement();
          name = key+"";
          value = this.parms.get(key)+"";
          transformer.setParameter(name, value);
        }
      }

      transformer.transform(ssource, result);

      output.close();
      output= null;

    }
    catch (Exception e) {
      throw e;
    }
  }

  public java.io.ByteArrayOutputStream getOutput() {
    return output;
  }

  public void setOutput(java.io.ByteArrayOutputStream output) {
    this.output = output;
  }

  public void setParameter(String name, String value)
  {
    if(this.parms.containsKey(name))
      this.parms.remove(name);
    this.parms.put(name, value);
  }

  public void removeParameter(String name)
  {
    if(this.parms.containsKey(name))
      this.parms.remove(name);
  }

}
