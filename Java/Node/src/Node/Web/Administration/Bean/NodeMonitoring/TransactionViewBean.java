package Node.Web.Administration.Bean.NodeMonitoring;

import java.util.ArrayList;

import Node.Web.Administration.BaseBean;
/**
 * <p>This class create TransactionViewBean.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class TransactionViewBean extends BaseBean {
  private ArrayList details = null;
  private ArrayList parameters = null;
  private ArrayList status = null;

  /**
   * Constructor
   * @param 
   * @return 
   */
  public TransactionViewBean() {
  }

  /**
   * setDetails
   * @param list
   * @return 
   */
  public void setDetails (ArrayList list)
  {
    this.details = list;
  }
  /**
   * getDetails
   * @param 
   * @return ArrayList
   */
  public ArrayList getDetails ()
  {
    return this.details;
  }

  /**
   * setParameters
   * @param list
   * @return 
   */
  public void setParameters (ArrayList list)
  {
    this.parameters = list;
  }
  /**
   * getParameters
   * @param 
   * @return ArrayList
   */
  public ArrayList getParameters ()
  {
    return this.parameters;
  }

  /**
   * setStatus
   * @param list
   * @return 
   */
  public void setStatus (ArrayList list)
  {
    this.status = list;
  }
  /**
   * getStatus
   * @param 
   * @return ArrayList
   */
  public ArrayList getStatus ()
  {
    return this.status;
  }
}
