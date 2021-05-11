package Node.DB.Interfaces;
/**
 * <p>This class create ISequenceNo interface.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public interface ISequenceNo {
  // Get (and set) next Sequence Number
	  /**
	   * GetNextSeqNumber
	   * @param tableName
	   * @param columnName
	   * @return int
	   */
  public int GetNextSeqNumber (String tableName, String columnName);
}
