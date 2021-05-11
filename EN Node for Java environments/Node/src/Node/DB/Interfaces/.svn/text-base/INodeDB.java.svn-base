package Node.DB.Interfaces;

import oracle.sql.BLOB;
import oracle.sql.CLOB;

import com.enfotech.basecomponent.db.IDBAdapter;
/**
 * <p>This class create INodeDB interface.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public interface INodeDB {
	  /**
	   * GetNodeDB
	   * @param 
	   * @return IDBAdapter
	   */
  public IDBAdapter GetNodeDB();
  /**
   * CreateBLOB
   * @param db
   * @param content
   * @return BLOB
   */
  public BLOB CreateBLOB (IDBAdapter db, byte[] content) throws Exception;
  /**
   * CreateBLOBByDB
   * @param db
   * @param dbName
   * @param content
   * @return BLOB
   */
  public BLOB CreateBLOBByDB (IDBAdapter db, String dbName, byte[] content)throws Exception;
  /**
   * CreateCLOB
   * @param db
   * @param content
   * @return CLOB
   */
  public CLOB CreateCLOB (IDBAdapter db, String content) throws Exception;
  /**
   * CreateCLOBByDB
   * @param db
   * @param dbName
   * @param content
   * @return CLOB
   */
  public CLOB CreateCLOBByDB (IDBAdapter db, String dbName, String content) throws Exception;
  /**
   * GetBlobBytes
   * @param blob
   * @return byte[]
   */
  public byte[] GetBlobBytes (BLOB blob) throws Exception;
  /**
   * GetTrueClobString
   * @param clob
   * @return String
   */
  public String GetTrueClobString (CLOB clob) throws Exception;
  /**
   * GetClobString
   * @param clob
   * @return String
   */
  public String GetClobString (CLOB clob) throws Exception;

}
