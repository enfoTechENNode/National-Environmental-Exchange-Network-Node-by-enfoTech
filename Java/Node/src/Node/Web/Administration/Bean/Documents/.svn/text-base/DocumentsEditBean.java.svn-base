package Node.Web.Administration.Bean.Documents;

import org.apache.struts.upload.FormFile;

import Node.Web.Administration.BaseBean;
/**
 * <p>This class create DocumentsEditBean.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class DocumentsEditBean extends BaseBean {
  private String title = "";
  private String message = "";

  private int id = -1;
  private String name = "";
  private String type = "";
  private String size = "";
  private String dataFlow = "";
  private String[] ops = null;
  private String transID = "";
  private String transIDError = "";
  private String source = "";
  private String sourceName = "";
  private FormFile upFile = null;

  /**
   * Constructor
   * @param 
   * @return 
   */
  public DocumentsEditBean() {
  }

  /**
   * setTitle
   * @param input
   * @return 
   */
  public void setTitle(String input)
  {
    this.title = input;
  }
  public String getTitle()
  {
    return this.title;
  }

  /**
   * setId
   * @param input
   * @return 
   */
  public void setId (int input)
  {
    this.id = input;
  }
  /**
   * getId
   * @param 
   * @return int
   */
  public int getId ()
  {
    return this.id;
  }

  /**
   * setMessage
   * @param input
   * @return 
   */
  public void setMessage (String input)
  {
    this.message = input;
  }
  /**
   * getMessage
   * @param 
   * @return String
   */
  public String getMessage ()
  {
    return this.message;
  }

  /**
   * setName
   * @param input
   * @return 
   */
  public void setName (String input)
  {
    this.name = input;
  }
  /**
   * getName
   * @param 
   * @return String
   */
  public String getName ()
  {
    return this.name;
  }

  /**
   * setType
   * @param input
   * @return 
   */
  public void setType (String input)
  {
    this.type = input;
  }
  /**
   * getType
   * @param 
   * @return String
   */
  public String getType ()
  {
    return this.type;
  }

  /**
   * setSize
   * @param input
   * @return 
   */
  public void setSize (String input)
  {
    try
    {
      int size = Integer.parseInt(input);
      if (size > 1000)
      {
        int remain = size % 1000;
        size = size / 1000;
        if (remain >= 500)
          size++;
        this.size = "" + size;
      }
      else
        this.size = "< 1";
    }
    catch (Exception e)
    {
      this.size = "< 1";
    }
  }
  /**
   * getSize
   * @param 
   * @return String
   */
  public String getSize ()
  {
    return this.size;
  }

  /**
   * setDataFlow
   * @param input
   * @return 
   */
  public void setDataFlow (String input)
  {
    this.dataFlow = input;
  }
  /**
   * getDataFlow
   * @param 
   * @return String
   */
  public String getDataFlow ()
  {
    return this.dataFlow;
  }

  /**
   * setOps
   * @param input
   * @return 
   */
  public void setOps (String[] input)
  {
    this.ops = input;
  }
  /**
   * getOps
   * @param 
   * @return String
   */
  public String[] getOps ()
  {
    return this.ops;
  }

  /**
   * setTransID
   * @param input
   * @return 
   */
  public void setTransID (String input)
  {
    this.transID = input;
  }
  /**
   * getTransID
   * @param 
   * @return String
   */
  public String getTransID ()
  {
    return this.transID;
  }

  /**
   * setTransIDError
   * @param input
   * @return 
   */
  public void setTransIDError (String input)
  {
    this.transIDError = input;
  }
  /**
   * getTransIDError
   * @param 
   * @return String
   */
  public String getTransIDError ()
  {
    return this.transIDError;
  }

  /**
   * setSource
   * @param input
   * @return 
   */
  public void setSource (String input)
  {
    this.source = input;
  }
  /**
   * getSource
   * @param 
   * @return String
   */
  public String getSource ()
  {
    return this.source;
  }

  /**
   * setSourceName
   * @param input
   * @return 
   */
  public void setSourceName (String input)
  {
    this.sourceName = input;
  }
  /**
   * getSourceName
   * @param 
   * @return String
   */
  public String getSourceName ()
  {
    return this.sourceName;
  }

  /**
   * setUpFile
   * @param input
   * @return 
   */
  public void setUpFile (FormFile input)
  {
    this.upFile = input;
  }
  /**
   * getUpFile
   * @param 
   * @return String
   */
  public FormFile getUpFile ()
  {
    return this.upFile;
  }
}
