package Node.DB.Interfaces.Configuration;

import java.util.ArrayList;

import org.apache.struts.upload.FormFile;
/**
 * <p>This class create IOperationMgr interface.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public interface IOperationMgr {

	/**
	 * GetOperationListFile.
	 * @param .
	 * @return byte[]
	 */
	public byte[] GetOperationListFile();


	/**
	 * SaveOperationListFile.
	 * @param xmlFile.
	 * @return boolean
	 */
	public boolean SaveOperationListFile(byte[] xmlFile);

	/**
	 * SaveSubListFile.
	 * @param xmlFile.
	 * @return boolean
	 */
	public boolean SaveSubListFile(FormFile xmlFile);

	/**
	 * GetOperationSubFileID.
	 * @param name.
	 * @return String
	 */
	public String GetOperationSubFileID(String name);	

	/**
	 * DeleteSubFileByID.
	 * @param id.
	 * @return boolean
	 */
	public boolean DeleteSubFileByID(String id);

	/**
	 * AddOperationManger.
	 * @param operationName.
	 * @param statusCD.
	 * @param submitURL.
	 * @param version.
	 * @param transID.
	 * @param supTransID.
	 * @param xmlFile.
	 * @param dataFlow.
	 * @return boolean
	 */
	public boolean AddOperationManger(String operationName,String statusCD,String submitURL,
			String version,String transID,String supTransID,byte[] xmlFile,String dataFlow);

	/**
	 * SaveOperationMangerFile.
	 * @param submitId.
	 * @param xmlFile.
	 * @return boolean
	 */
	public boolean SaveOperationMangerFile(int submitId, byte[] xmlFile);

	/**
	 * CheckGetStatus.
	 * @param opId.
	 * @return boolean
	 */
	public boolean CheckGetStatus(int opId) ;

	/**
	 * GetOperationsManagerTableList
	 * @param transId
	 * @return ArrayList
	 */
	public ArrayList GetOperationsManagerTableList (String transId);

	/**
	 * CheckUserNameFromOperation_log_parameter.
	 * @param token.
	 * @param webserviceName.
	 * @return String
	 */
	public String GetUserNameFromOperation_log_parameter(String token, String webserviceName) ;

	/**
	 * CheckPasswordFromOperation_log_parameter.
	 * @param token.
	 * @param webserviceName.
	 * @return String
	 */
	public String GetPasswordFromOperation_log_parameter(String token, String webserviceName) ;
}
