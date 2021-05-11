package Node.Biz.Administration;

import java.util.ArrayList;

import Node.DB.DBManager;
import Node.DB.Interfaces.INodeDomain;
/**
 * <p>This class create Status class.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class Status {
  private String NodeStatus = null;
  private String NodeMessage = null;
  private ArrayList Domains = null;
  private ArrayList Operations = null;

  public Status ()
  {

  }

  /**
   * Constructor.
   * @param loggerName .
   * @return 
   */
  public Status(String loggerName) throws Exception
  {
    Status status = null;
    INodeDomain domainDB = DBManager.GetNodeDomain(loggerName);
    status = domainDB.GetNodeStatus();
    if (status != null)
      this.Init(status);
  }

  /**
   * Initial Class.
   * @param s.
   * @return 
   */
  private void Init (Status s)
  {
    if (s != null) {
      this.NodeStatus = s.GetNodeStatus();
      this.NodeMessage = s.GetNodeMessage();
      this.Domains = s.GetDomains();
      this.Operations = s.GetOperations();
    }
  }

  public void SetNodeStatus (String input)
  {
    this.NodeStatus = input;
  }
  public String GetNodeStatus ()
  {
    return this.NodeStatus;
  }

  public void SetNodeMessage (String input)
  {
    this.NodeMessage = input;
  }
  public String GetNodeMessage ()
  {
    return this.NodeMessage;
  }

  /**
   * Add Domain.
   * @param list.
   * @return
   */
  public void AddDomain (ArrayList list)
  {
    if (this.Domains == null)
      this.Domains = new ArrayList();
    this.Domains.add(list);
  }
  public ArrayList GetDomains ()
  {
    return this.Domains;
  }

  /**
   * Add Operation.
   * @param list.
   * @return
   */
  public void AddOperation (ArrayList operation)
  {
    if (this.Operations == null)
      this.Operations = new ArrayList();
    this.Operations.add(operation);
  }
  public ArrayList GetOperations ()
  {
    return this.Operations;
  }
}
