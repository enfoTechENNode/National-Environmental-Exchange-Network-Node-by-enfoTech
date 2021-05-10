package Node.API;

import java.io.ByteArrayOutputStream;
import java.io.DataOutputStream;
import java.io.InputStream;
import java.util.ArrayList;
import java.util.Enumeration;
import java.util.Hashtable;
import java.util.Iterator;
import java.util.List;
import java.util.zip.ZipEntry;
import java.util.zip.ZipFile;
import java.util.zip.ZipInputStream;
import java.util.zip.ZipOutputStream;
import java.security.MessageDigest;
import org.apache.log4j.Level;
import org.dom4j.Document;
import org.dom4j.Element;
import org.dom4j.Node;
import org.dom4j.io.SAXReader;
import org.safehaus.uuid.UUID;
import org.safehaus.uuid.UUIDGenerator;

import com.enfotech.basecomponent.jndi.JNDIAccess;

import Node.Biz.Administration.OperationLog;
import Node.DB.DBManager;
import Node.DB.Interfaces.INodeOperation;
import Node.DB.Interfaces.INodeOperationLog;
import Node.DB.Interfaces.INodeOperationParameter;
import Node.DB.Interfaces.Configuration.IOperationMgr;
import Node.DB.Oracle.NodeOperationLog;
import Node.DB.Oracle.NodeOperationParameter;
import Node.Utils.LoggingUtils;
import Node.Utils.Utility;
import Node.WebServices.Document.ClsNodeDocument;
import java.io.File;
import java.io.IOException;
import java.io.FileFilter;
import Node.Phrase;

import java.io.FileNotFoundException;
import java.io.PrintStream;
import java.io.ByteArrayInputStream;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import Node.Utils.AppUtils;

/**
 * <p>API for several Node Utility Functions, including Node Operation Logging</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * 
 * @author enfoTech
 * @version 2.0
 */
public class NodeUtils {
	/**
	 * Constructs an object to call Node Utility Functions.
	 */
	public NodeUtils() {
	}

	/**
	 * Create Operation Log.
	 * <p>
	 * This call creates an operation log for a transaction id. The status of
	 * the transaction can then be updated (and eventually ended) with
	 * subsequent UpdateOperationLog calls.
	 * </p>
	 * <p>
	 * If no transaction id is supplied, then a new transaction id is generated
	 * and inserted into the database. It is strongly recommended that one
	 * supply a transaction id when calling this function so that the code that
	 * calls this function can control the transaction id.
	 * </p>
	 * <p>
	 * The Handler for a web service or the Task Application calls this function
	 * before calling Operation specific code, so subsequent operation specific
	 * code should call the UpdateOperationLog function(s) to update usernames,
	 * statuses, or tokens. Calling this function would create a completely
	 * separate Log and not recommended in most cases.
	 * </p>
	 * <p>
	 * This function inserts the parameter names and values for the transaction.
	 * There are a few separate paramters for that have a separate input
	 * parameter that are only applicable to some Handlers (for instance,
	 * returnURL is only used for solicit requests). The rest of the parameters
	 * are supplied in the paramNames array and paramValues array. The
	 * paramNames and paramValues array should be of the same length, otherwise
	 * they will not be logged.
	 * </p>
	 * <p>
	 * This function returns the internal Operation Log ID used in the Node
	 * database. This id could be used to update this operation log later. The
	 * transactio id could also be used.
	 * </p>
	 * 
	 * @param loggerName
	 *            (Required) Name of the log4j Logger Name (in case Logging
	 *            should take place). See Node.Phrase for details.
	 * @param opID
	 *            (Required) ID of the Operation being Logged.
	 * @param userName
	 *            (Optional) User Name of the user making the request.
	 * @param transID
	 *            (Optional) the transaction id.
	 * @param status
	 *            (Required) the starting status of the transaction.
	 * @param message
	 *            (Required) the starting message of the transaction.
	 * @param requestorIP
	 *            (Optional) the requestor's IP Address.
	 * @param supplTransID
	 *            (Optional) the supplied transaction id parameter value.
	 * @param token
	 *            (Optional) the security token supplied.
	 * @param nodeAddress
	 *            (Optional) the node address parameter value.
	 * @param returnURL
	 *            (Optional) the return url parameter value.
	 * @param serviceType
	 *            (Optional) the service type parameter value.
	 * @param hostName
	 *            (Optional) the name of the host serving the request.
	 * @param paramNames
	 *            (Optional) array of parameter names.
	 * @param paramValues
	 *            (Optional) array of parameter values.
	 * @return the id of the Operation Log for use in subsequent
	 *         OperationLogging, -1 if an error occurs.
	 */
	public int CreateOperationLog(String loggerName, int opID, String userName,
			String transID, String status, String message, String requestorIP,
			String supplTransID, String token, String nodeAddress,
			String returnURL, String serviceType, String hostName,
			String[] paramNames, Object[] paramValues) {
		int retInt = -1;
		try {
			if (opID >= 0 && status != null && message != null) {
				INodeOperationLog opLogDB = DBManager
						.GetNodeOperationLog(loggerName);
				if (transID == null)
					transID = this.GenerateTransID(Phrase.UUID);
				retInt = opLogDB.CreateOperationLog(transID, opID, userName,
						status, message, requestorIP, supplTransID, token,
						nodeAddress, returnURL, serviceType, hostName,
						paramNames, paramValues);
			}
		} catch (Exception e) {
			LoggingUtils.Log("Could not Update Operation Log: " + e.toString(),
					Level.ERROR, loggerName);
		}
		return retInt;
	}

	/**
	 * Update Operation Log Status
	 * <p>
	 * The Operation Log is updated with the input status and message for the
	 * Operation Log with Operation Log ID supplied. If the opLogID parameter is
	 * less than 0, then the return value is less than 0.
	 * </p>
	 * <p>
	 * The isLastUpdate parameter indicates whether the transaction is logging
	 * the last status and message for that transaction. This allows the end
	 * date and time to be populated in the parenet Operation Log Table. If it
	 * is the last update to the status and message, this value should be true.
	 * Otherwise, it should be false.
	 * </p>
	 * 
	 * @param loggerName
	 *            name of the log4j Logger Name (in case Logging should take
	 *            place). See Node.Phrase for details.
	 * @param opLogID
	 *            Operation Log ID.
	 * @param status
	 *            the new status of the operation transaction.
	 * @param message
	 *            the new status message of the operation transaction.
	 * @param isLastUpdate
	 *            indicates whether this is the last status update for this
	 *            transaction.
	 * @param isDebug
	 *            indicates whether this is the debug log
	 * @return the operation log id if successful, -1 otherwise.
	 */
	public int UpdateOperationLog(String loggerName, int opLogID,
			String status, String message, boolean isLastUpdate,boolean isDebug) {
		int retInt = -1;
		try {
			INodeOperationLog opLogDB = DBManager
					.GetNodeOperationLog(loggerName);
			retInt = opLogDB.UpdateOperationLog(opLogID, status, message,
					isLastUpdate,isDebug);
		} catch (Exception e) {
			LoggingUtils.Log("Could not Update Operation Log: " + e.toString(),
					Level.ERROR, loggerName);
		}
		return retInt;
	}

	/**
	 * Update Operation Log Status
	 * <p>
	 * The Operation Log is updated with the input status and message for the
	 * Operation Log with transaction id supplied. If the transID parameter is
	 * null or the empty string, then the return value is less than 0.
	 * </p>
	 * <p>
	 * The isLastUpdate parameter indicates whether the transaction is logging
	 * the last status and message for that transaction. This allows the end
	 * date and time to be populated in the parenet Operation Log Table. If it
	 * is the last update to the status and message, this value should be true.
	 * Otherwise, it should be false.
	 * </p>
	 * 
	 * @param loggerName
	 *            name of the log4j Logger Name (in case Logging should take
	 *            place). See Node.Phrase for details.
	 * @param transID
	 *            transaction id to be updated.
	 * @param status
	 *            the new status of the operation transaction.
	 * @param message
	 *            the new status message of the operation transaction.
	 * @param isLastUpdate
	 *            indicates whether this is the last status update for this
	 *            transaction.
	 * @param isDebug
	 *            indicates whether this is the debug log
	 * @return the operation log id if successful, -1 otherwise.
	 */
	public int UpdateOperationLog(String loggerName, String transID,
			String status, String message, boolean isLastUpdate,boolean isDebug) {
		int retInt = -1;
		try {
			INodeOperationLog opLogDB = DBManager
					.GetNodeOperationLog(loggerName);
			retInt = opLogDB.UpdateOperationLog(transID, status, message,
					isLastUpdate,isDebug);
		} catch (Exception e) {
			LoggingUtils.Log("Could not Update Operation Log: " + e.toString(),
					Level.ERROR, loggerName);
		}
		return retInt;
	}

	/**
	 * Update Operation Log Status
	 * <p>
	 * The Operation Log is updated with the input status and message for the
	 * Operation Log with Operation Log ID supplied. If the opLogID parameter is
	 * less than 0, then the return value is less than 0.
	 * </p>
	 * <p>
	 * The isLastUpdate parameter indicates whether the transaction is logging
	 * the last status and message for that transaction. This allows the end
	 * date and time to be populated in the parenet Operation Log Table. If it
	 * is the last update to the status and message, this value should be true.
	 * Otherwise, it should be false.
	 * </p>
	 * 
	 * @param loggerName
	 *            name of the log4j Logger Name (in case Logging should take
	 *            place). See Node.Phrase for details.
	 * @param opLogID
	 *            Operation Log ID.
	 * @param status
	 *            the new status of the operation transaction.
	 * @param message
	 *            the new status message of the operation transaction.
	 * @param isLastUpdate
	 *            indicates whether this is the last status update for this
	 *            transaction.
	 * @return the operation log id if successful, -1 otherwise.
	 */
	public int UpdateOperationLog(String loggerName, int opLogID,
			String status, String message, boolean isLastUpdate) {
		int retInt = -1;
		try {
			INodeOperationLog opLogDB = DBManager
					.GetNodeOperationLog(loggerName);
			retInt = opLogDB.UpdateOperationLog(opLogID, status, message,
					isLastUpdate);
		} catch (Exception e) {
			LoggingUtils.Log("Could not Update Operation Log: " + e.toString(),
					Level.ERROR, loggerName);
		}
		return retInt;
	}

	/**
	 * Update Operation Log Status
	 * <p>
	 * The Operation Log is updated with the input status and message for the
	 * Operation Log with transaction id supplied. If the transID parameter is
	 * null or the empty string, then the return value is less than 0.
	 * </p>
	 * <p>
	 * The isLastUpdate parameter indicates whether the transaction is logging
	 * the last status and message for that transaction. This allows the end
	 * date and time to be populated in the parenet Operation Log Table. If it
	 * is the last update to the status and message, this value should be true.
	 * Otherwise, it should be false.
	 * </p>
	 * 
	 * @param loggerName
	 *            name of the log4j Logger Name (in case Logging should take
	 *            place). See Node.Phrase for details.
	 * @param transID
	 *            transaction id to be updated.
	 * @param status
	 *            the new status of the operation transaction.
	 * @param message
	 *            the new status message of the operation transaction.
	 * @param isLastUpdate
	 *            indicates whether this is the last status update for this
	 *            transaction.
	 * @return the operation log id if successful, -1 otherwise.
	 */
	public int UpdateOperationLog(String loggerName, String transID,
			String status, String message, boolean isLastUpdate) {
		int retInt = -1;
		try {
			INodeOperationLog opLogDB = DBManager
					.GetNodeOperationLog(loggerName);
			retInt = opLogDB.UpdateOperationLog(transID, status, message,
					isLastUpdate);
		} catch (Exception e) {
			LoggingUtils.Log("Could not Update Operation Log: " + e.toString(),
					Level.ERROR, loggerName);
		}
		return retInt;
	}

	/**
	 * Update the User Name Field in the Operation Log
	 * <p>
	 * The user name field is populated in the operation log where the
	 * transaction id is equal to the transID input parameter.
	 * </p>
	 * <p>
	 * This function is generally used after authorization takes place. Because
	 * logging of the transaction begins before authorization occurs, the user
	 * name is not normally known when creating the operation log. If
	 * authorization succeeds, this function is called by the web service
	 * handler to update the user name requesting the transaction. It should not
	 * normally be called within the processing (pre,process,or post) of an
	 * operation.
	 * </p>
	 * 
	 * @param loggerName
	 *            name of the log4j Logger Name (in case Logging should take
	 *            place). See Node.Phrase for details.
	 * @param transID
	 *            transaction id to be updated.
	 * @param userName
	 *            name of the user to be updated.
	 * @return the operation log id if successful, -1 otherwise.
	 */
	public int UpdateOperationLogUserName(String loggerName, String transID,
			String userName) {
		int retInt = -1;
		try {
			INodeOperationLog opLogDB = DBManager
					.GetNodeOperationLog(loggerName);
			retInt = opLogDB.UpdateOperationLogUserName(transID, userName);
		} catch (Exception e) {
			LoggingUtils.Log("Could not Update Operation Log: " + e.toString(),
					Level.ERROR, loggerName);
		}
		return retInt;
	}

	/**
	 * Update the Security Token in the Operation Log
	 * <p>
	 * The security token field is populated in the operation log where the
	 * transaction id is equal to the transID input parameter.
	 * </p>
	 * <p>
	 * This is used in the authenticate handler after authentication completes
	 * to update the security token when the the authentication process
	 * succeeds. This is necessary because the security token will not been
	 * known when the handler creates the operation log. Any other web service
	 * handler (besides NodePing) will supply the security token when creating
	 * the operation log (as the security token is an input parameter).
	 * </p>
	 * 
	 * @param loggerName
	 *            name of the log4j Logger Name (in case Logging should take
	 *            place). See Node.Phrase for details.
	 * @param transID
	 *            transaction id to be updated.
	 * @param token
	 *            security token to be updated.
	 * @return the operation log id if successful, -1 otherwise.
	 */
	public int UpdateOperationLogToken(String loggerName, String transID,
			String token) {
		int retInt = -1;
		try {
			INodeOperationLog opLogDB = DBManager
					.GetNodeOperationLog(loggerName);
			retInt = opLogDB.UpdateOperationLogToken(transID, token);
		} catch (Exception e) {
			LoggingUtils.Log("Could not Update Operation Log: " + e.toString(),
					Level.ERROR, loggerName);
		}
		return retInt;
	}

	/**
	 * Generate a unique Transaction ID
	 * <p>
	 * The transaction id is based on a random number and current time
	 * algorithm.
	 * </p>
	 * <p>
	 * This is the function that the handlers use to generate transaction ids.
	 * </p>
	 * 
	 * @throws Exception
	 *             is an error occurs.
	 * @return transaction id if successful.
	 */
	public String GenerateTransID(String style) throws Exception {
		String ret = null;
		if (style.equalsIgnoreCase(Phrase.MD5)) {
			long t1 = (new java.util.Date()).getTime();
			double q1 = Math.random();
			MessageDigest md = MessageDigest.getInstance("MD5");
			md.update("password".getBytes());
			md.update(this.MakeBytes(t1, q1));
			byte[] hex = md.digest();
			ret = this.ToHexString(hex);
		} else if (style.equalsIgnoreCase(Phrase.UUID)) {
			ret = getUUID();
		}
		return ret;
	}

	/**
	 * Zip a Collection of Files.
	 * <p>
	 * This method zips all of the files contained in the docs array. If docs is
	 * null or docs.length <= 0, then null is returned.
	 * </p>
	 * 
	 * @param docs
	 *            array of documents to zip.
	 * @throws Exception
	 *             if some error occurs.
	 * @return zipped file in the form of a byte array.
	 */
	public byte[] Zip(ClsNodeDocument[] docs) throws Exception {
		byte[] retBytes = null;
		if (docs != null && docs.length > 0) {
			byte[][] content = new byte[docs.length][];
			String[] names = new String[docs.length];
			for (int i = 0; i < docs.length; i++) {
				content[i] = docs[i].getContent();
				names[i] = docs[i].getName();
			}
			retBytes = this.Zip(content, names);
		}
		return retBytes;
	}

	/**
	 * Zip a Collection of Files from hard driver.
	 * <p>
	 * This method zips all of the files contained in the docs array. If docs is
	 * null or docs.length <= 0, then null is returned.
	 * </p>
	 * 
	 * @param docs
	 *            array of documents to zip.
	 * @throws Exception
	 *             if some error occurs.
	 * @return zipped file in the form of a byte array.
	 */
	public static byte[] Zip(String path, ArrayList docsNameList)
			throws Exception {
		byte[] retZip = null;
		byte[] content = null;
		String fileName = null;
		ByteArrayOutputStream baos = new ByteArrayOutputStream();
		ZipOutputStream zos = new ZipOutputStream(baos);
		if (docsNameList != null && !docsNameList.isEmpty()) {
			Iterator it = docsNameList.iterator();
			while (it.hasNext()) {
				fileName = (String) it.next();
				content = Utility.readFile(path + fileName);
				if (content != null) {
					ZipEntry entry = new ZipEntry(fileName);
					zos.putNextEntry(entry);
					zos.write(content);
					zos.flush();
					zos.closeEntry();
				}
				content = null;
				Utility.delFile(path + fileName);
			}
			zos.close();
			baos.close();
			retZip = baos.toByteArray();
		}
		return retZip;
	}

	/**
	 * Zip a Collection of Files from hard driver.
	 * <p>
	 * This method zips all of the files contained in the docs array. If docs is
	 * null or docs.length <= 0, then null is returned.
	 * </p>
	 * 
	 * @param docs
	 *            array of documents to zip.
	 * @param zipFileName
	 *            zipped file name.
	 * @throws Exception
	 *             if some error occurs.
	 * @return boolean true/false.
	 */
	public static boolean Zip(String path, ArrayList docsNameList,
			String zipFileName) {
		boolean ret = false;
		byte[] content = null;
		String fileName = null;
		FileOutputStream fos = null;
		ZipOutputStream zos = null;
		try {
			fos = new FileOutputStream(new File(path + zipFileName));
			zos = new ZipOutputStream(fos);
			if (docsNameList != null && !docsNameList.isEmpty()) {
				Iterator it = docsNameList.iterator();
				while (it.hasNext()) {
					fileName = (String) it.next();
					content = Utility.readFile(path + fileName);
					if (content != null) {
						ZipEntry entry = new ZipEntry(fileName);
						zos.putNextEntry(entry);
						zos.write(content);
						zos.flush();
						zos.closeEntry();
					}
					content = null;
					Utility.delFile(path + fileName);
				}
				ret = true;
			}
		} catch (Exception e) {
			e.printStackTrace();
		} finally {
			try {
				zos.close();
				fos.close();
			} catch (IOException e) {
				e.printStackTrace();
			}
		}
		return ret;
	}

	/**
	 * Zip a Collection of Files.
	 * <p>
	 * This method zips all of the files contained in the content array. If
	 * content is null or fileNames is null or their lengths do not match or
	 * their lengths are equal to 0, then null is returned. The byte[] in
	 * content[i] should correspond to the file named in fileNames[i].
	 * </p>
	 * 
	 * @param content
	 *            array of byte[], each array is a zip entry comprised of byte[]
	 *            content
	 * @param fileNames
	 *            list of file names, each a file name for each array entry of
	 *            bytes
	 * @throws Exception
	 *             if some error occurs.
	 * @return zipped file in the form of a byte array.
	 */
	public byte[] Zip(byte[][] content, String[] fileNames) throws Exception {
		byte[] retZip = null;
		if (content != null && content.length > 0 && fileNames != null
				&& fileNames.length == content.length) {
			ByteArrayOutputStream baos = new ByteArrayOutputStream();
			ZipOutputStream zos = new ZipOutputStream(baos);
			for (int i = 0; i < content.length; i++) {
				String fileName = null;
				if (fileNames != null && fileNames.length > i)
					fileName = fileNames[i];
				ZipEntry entry = new ZipEntry(fileName);
				zos.putNextEntry(entry);
				zos.write(content[i]);
				zos.flush();
				zos.closeEntry();
			}
			zos.close();
			baos.close();
			retZip = baos.toByteArray();
		}
		return retZip;
	}

	/**
	 * UnZip a zip file into a collection of files.
	 * <p>
	 * This method unzips the zip file that can be read for the input parameter
	 * is. If is is null, then null is returned.
	 * </p>
	 * <p>
	 * The return value is an array of type byte[]. Each entry in the return
	 * double array is an array of bytes that represents the bytes of one,
	 * unzipped file.
	 * </p>
	 * 
	 * @param is
	 *            InputStream of a zipped file.
	 * @throws Exception
	 *             if some error occurs.
	 * @return array of byte[] content (Unzipped)
	 */
	public byte[][] UnZip(InputStream is) throws Exception {
		byte[][] retContent = null;
		if (is != null) {
			ZipInputStream zis = new ZipInputStream(is);
			ArrayList list = new ArrayList();
			while (zis.getNextEntry() != null) {
				ByteArrayOutputStream baos = new ByteArrayOutputStream();
				byte[] temp = new byte[1024];
				int read = 0;
				while ((read = zis.read(temp)) >= 0)
					baos.write(temp, 0, read);
				baos.flush();
				baos.close();
				list.add(baos.toByteArray());
			}
			if (!list.isEmpty()) {
				retContent = new byte[list.size()][];
				for (int i = 0; i < list.size(); i++)
					retContent[i] = (byte[]) list.get(i);
			}
		}
		return retContent;
	}

	/**
	 * UnZip a zip file into a collection of files.
	 * <p>
	 * This method unzips the zip file that can be read for the input parameter
	 * is. If is is null, then null is returned.
	 * </p>
	 * <p>
	 * The return value is an array of type byte[]. Each entry in the return
	 * double array is an array of bytes that represents the bytes of one,
	 * unzipped file.
	 * </p>
	 * 
	 * @param inputZip
	 *            byte array of a zipped file.
	 * @throws Exception
	 *             if some error occurs.
	 * @return array of byte[] content (Unzipped)
	 */
	public static byte[][] UnZip(byte[] inputZip) throws Exception {
		InputStream is = new ByteArrayInputStream(inputZip);
		byte[][] retContent = null;
		if (is != null) {
			ZipInputStream zis = new ZipInputStream(is);
			ArrayList list = new ArrayList();
			while (zis.getNextEntry() != null) {
				ByteArrayOutputStream baos = new ByteArrayOutputStream();
				byte[] temp = new byte[1024];
				int read = 0;
				while ((read = zis.read(temp)) >= 0)
					baos.write(temp, 0, read);
				baos.flush();
				baos.close();
				list.add(baos.toByteArray());
			}
			if (!list.isEmpty()) {
				retContent = new byte[list.size()][];
				for (int i = 0; i < list.size(); i++)
					retContent[i] = (byte[]) list.get(i);
			}
		}
		return retContent;
	}

	/**
	 * Create a byte array.
	 * <p>
	 * This method create a byte array.
	 * </p>
	 * <p>
	 * The return value is byte[].
	 * </p>
	 * 
	 * @param t
	 *            is long.
	 * @param q
	 *            is double.
	 * @throws Exception
	 *             if some error occurs.
	 * @return byte array
	 */
	private byte[] MakeBytes(long t, double q) throws Exception {
		ByteArrayOutputStream byteOut = new ByteArrayOutputStream();
		DataOutputStream dataOut = new DataOutputStream(byteOut);
		dataOut.writeLong(t);
		dataOut.writeDouble(q);
		return byteOut.toByteArray();
	}

	/**
	 * Create ToHexString string.
	 * <p>
	 * The return value is String.
	 * </p>
	 * 
	 * @param b
	 *            is byte array.
	 * @throws Exception
	 *             if some error occurs.
	 * @return String
	 */

	private String ToHexString(byte[] b) {
		StringBuffer sb = new StringBuffer(b.length * 2);
		for (int i = 0; i < b.length; i++) {
			// look up high nibble char
			sb.append(this.HexChar[(b[i] & 0xf0) >>> 4]);

			// look up low nibble char
			sb.append(this.HexChar[b[i] & 0x0f]);
		}
		return sb.toString();
	}

	/**
	 * Create ToHexChar.
	 * <p>
	 * The return value is char array.
	 * </p>
	 * 
	 * @return char array
	 */
	private static char[] HexChar = { '0', '1', '2', '3', '4', '5', '6', '7',
			'8', '9', 'a', 'b', 'c', 'd', 'e', 'f' };

	/**
	 * buildNitobiSql used to build a special sql for nitobi component
	 * 
	 * @param tableName
	 *            : The name of table
	 * @param tableColumns
	 *            : The column string of content which should be seperate by
	 *            commas, etc: a,b,c
	 * @param startParameter
	 *            : The starte index of records
	 * @param recordSum
	 *            : The sum of records
	 * @param pagesizeParameter
	 *            : The scroll size of records
	 * @param sortColumn
	 *            : The sort column of table
	 * @param sortDirection
	 *            : The sort direction of table
	 * @param condition
	 *            : extra condition, need added with "AND" key word, etc:
	 *            "AND col = 1"
	 * @return sql: sql statement
	 * @throws Exception
	 */

	public static String buildNitobiSql(String tableName, String tableColumns,
			String startParameter, int recordSum, String pagesizeParameter,
			String sortColumn, String sortDirection, String condition) {
		String sql = null;
		if (condition != null)
			sql = "SELECT "
					+ tableColumns
					+ " FROM (SELECT "
					+ tableColumns
					+ " FROM (SELECT ROWNUM AS ID,"
					+ tableColumns
					+ " FROM (SELECT "
					+ tableColumns
					+ " FROM "
					+ tableName
					+ " WHERE 1=1 "
					+ condition
					+ " ORDER BY "
					+ sortColumn
					+ " "
					+ ((sortDirection.equalsIgnoreCase("Asc")) ? "Asc" : "Desc")
					+ ")) WHERE ID > " + Integer.parseInt(startParameter)
					+ " AND ID <= " + recordSum + ")" + " WHERE ROWNUM <= "
					+ Integer.parseInt(pagesizeParameter);
		else
			sql = "SELECT "
					+ tableColumns
					+ " FROM (SELECT "
					+ tableColumns
					+ " FROM (SELECT ROWNUM AS ID,"
					+ tableColumns
					+ " FROM (SELECT "
					+ tableColumns
					+ " FROM "
					+ tableName
					+ " ORDER BY "
					+ sortColumn
					+ " "
					+ ((sortDirection.equalsIgnoreCase("Asc")) ? "Asc" : "Desc")
					+ ")) WHERE ID > " + Integer.parseInt(startParameter)
					+ " AND ID <= " + recordSum + ")" + " WHERE ROWNUM <= "
					+ Integer.parseInt(pagesizeParameter);
		return sql;
	}

	/**
	 * Creates a temporary file in the proper directory to allow for cleanup
	 * after execution. This method delegates to
	 * {@link File#createTempFile(java.lang.String, java.lang.String, java.io.File)}
	 * so refer to it for more documentation. Any file created using this method
	 * should be considered as deleted at JVM exit; therefore, do not use this
	 * method to create files that need to be persistent between application
	 * runs.
	 * 
	 * @param prefix
	 *            the prefix string used in generating the file name; must be at
	 *            least three characters long
	 * @param suffix
	 *            the suffix string to be used in generating the file's name;
	 *            may be null, in which case the suffix ".tmp" will be used
	 * @return an abstract pathname denoting a newly created empty file
	 * @throws IOException
	 *             if a file could not be created
	 */
	public static File createTempFile(String prefix, String suffix)
			throws IOException, Exception {
		// Check to see if you have already initialized a temp directory
		// for this class.
		if (sTmpDir == null) {
			// Initialize your temp directory. You use the java temp directory
			// property, so you are sure to find the files on the next run.
			String tmpDirName = System.getProperty("java.io.tmpdir");
			File tmpDir = File.createTempFile(TEMP_DIR_PREFIX, ".tmp",
					new File(tmpDirName));

			// Delete the file if one was automatically created by the JVM.
			// You are going to use the name of the file as a directory name,
			// so you do not want the file laying around.
			tmpDir.delete();

			// Create a lock before creating the directory so
			// there is no race condition with another application trying
			// to clean your temp dir.
			// Save the lock path into ArrayList. This will allow the
			// TempFileManager to clean the overall temp directory next time.
			lockFiles.add(tmpDir.getName() + ".lck");

			// Make a temp directory that you will use for all future requests.
			if (!tmpDir.mkdirs()) {
				throw new IOException("Unable to create temporary directory:"
						+ tmpDir.getAbsolutePath());
			}

			sTmpDir = tmpDir;
		}

		// Generate a temp file for the user in your temp directory
		// and return it.
		return File.createTempFile(prefix, suffix, sTmpDir);
	}

	/**
	 * Deletes all of the files in the given directory, recursing into any sub
	 * directories found. Also deletes the root directory.
	 * 
	 * @param rootDir
	 *            the root directory to be recursively deleted
	 * @throws IOException
	 *             if any file or directory could not be deleted
	 */
	private static void recursiveDelete(File rootDir) throws IOException {
		// Select all the files
		File[] files = rootDir.listFiles();
		for (int i = 0; i < files.length; i++) {
			// If the file is a directory, we will
			// recursively call delete on it.
			if (files[i].isDirectory()) {
				recursiveDelete(rootDir);
			} else {
				// It is just a file so we are safe to
				// delete it
				if (!files[i].delete()) {
					throw new IOException("Could not delete: "
							+ files[i].getAbsolutePath());
				}
			}
		}

		// Finally, delete the root directory now
		// that all of the files in the directory have
		// been properly deleted.
		if (!rootDir.delete()) {
			throw new IOException("Could not delete: "
					+ rootDir.getAbsolutePath());
		}
	}

	/**
	 * The prefix for the temp directory in the system temp directory
	 */
	private final static String TEMP_DIR_PREFIX = "tmp-mgr-";

	/**
	 * The temp directory to generate all files in
	 */
	private static File sTmpDir = null;
	private static ArrayList lockFiles = new ArrayList();
	/**
	 * Static block used to clean up any old temp directories found -- the JVM
	 * will run this block when a class loader loads the class.
	 */
	static {
		try {
			// Clean up any old temp directories by listing
			// all of the files, using a filter that will
			// return only directories that start with your
			// prefix.
			FileFilter tmpDirFilter = new FileFilter() {
				public boolean accept(File pathname) {
					return (pathname.isDirectory() && pathname.getName()
							.startsWith(TEMP_DIR_PREFIX));
				}
			};

			// Get the system temp directory and filter the files.
			String tmpDirName = System.getProperty("java.io.tmpdir");
			File tmpDir = new File(tmpDirName);
			File[] tmpFiles = tmpDir.listFiles(tmpDirFilter);

			// Find all the files that do not have a lock by
			// checking if the lock file exists.
			for (int i = 0; i < tmpFiles.length; i++) {
				File tmpFile = tmpFiles[i];

				// Create a file to represent the lock and test.
				if (!lockFiles.contains(tmpFile.getName() + ".lck")) {
					// Delete the contents of the directory since
					// it is no longer locked.
					LoggingUtils.Log(
							"TempFileManager::deleting old temp directory "
									+ tmpFile, Level.DEBUG,
							Phrase.AdministrationLoggerName);

					try {
						recursiveDelete(tmpFile);
					} catch (IOException ex) {
						// You log at a fine level since not being able to
						// delete
						// the temp directory should not stop the application
						// from performing correctly. However, if the
						// application
						// generates a lot of temp files, this could become
						// a disk space problem and the level should be raised.
						LoggingUtils.Log("TempFileManager::unable to delete "
								+ tmpFile.getAbsolutePath(), Level.DEBUG,
								Phrase.AdministrationLoggerName);

						// Print the exception.
						ByteArrayOutputStream ostream = new ByteArrayOutputStream();
						ex.printStackTrace(new PrintStream(ostream));

						LoggingUtils.Log(ostream.toString(), Level.DEBUG,
								Phrase.AdministrationLoggerName);
					}
				}
			}
		} catch (Exception ex) {
			ex.printStackTrace();
		}
	}

	/**
	 * Unzip file.
	 * <p>
	 * The return value is FileInputStream.
	 * </p>
	 * 
	 * @param b
	 *            is byte array.
	 * @throws Exception
	 *             if some error occurs.
	 * @return FileInputStream
	 */
	public static FileInputStream getUnZipFile(byte[] zippedContent)
			throws Exception {
		FileInputStream fis = null;
		ZipInputStream zis = null;
		String path = Utility.GetTempFilePath();
		try {
			ByteArrayInputStream bis = new ByteArrayInputStream(zippedContent);
			zis = new ZipInputStream(bis);

			// get the first entry
			ZipEntry ze = zis.getNextEntry();

			// write to a output stream
			File aTmpFile = new File(path + "/temp/Node_Unzip_tmp"
					+ Utility.GetSysTimeString() + ".txt");
			aTmpFile.deleteOnExit();
			FileOutputStream fos = new FileOutputStream(aTmpFile);
			byte[] buffer = new byte[1024];
			int length;
			while ((length = zis.read(buffer)) > 0) {
				fos.write(buffer, 0, length);
				fos.flush();
			}
			bis.close();
			fos.close();
			zis.close();

			fis = new FileInputStream(aTmpFile);
		} catch (Exception ex) {
			throw ex;
		}
		return fis;
	}

	/**
	 * Unzip file.
	 * <p>
	 * The return value is File.
	 * </p>
	 * 
	 * @param zippedContent
	 *            is byte array.
	 * @throws Exception
	 *             if some error occurs.
	 * @return File
	 */
	public static File getUnZipFilePath(byte[] zippedContent) throws Exception {
		File aTmpFile = null;
		ZipInputStream zis = null;
		FileOutputStream fos = null;
		ByteArrayInputStream bis = null;
		String path = Utility.GetTempFilePath();
		try {
			bis = new ByteArrayInputStream(zippedContent);
			zis = new ZipInputStream(bis);

			// get the first entry
			ZipEntry ze = zis.getNextEntry();

			// write to a output stream
			aTmpFile = new File(path + "/temp/" + Phrase.UNZIP
					+ Utility.GetSysTimeString() + "_" + ze.getName());
			aTmpFile.deleteOnExit();
			fos = new FileOutputStream(aTmpFile);
			byte[] buffer = new byte[1024];
			int length;
			while ((length = zis.read(buffer)) > 0) {
				fos.write(buffer, 0, length);
				fos.flush();
			}
		} catch (Exception ex) {
			throw ex;
		} finally {
			bis.close();
			fos.close();
			zis.close();
		}
		return aTmpFile;
	}

	/**
	 * Unzip big file.
	 * <p>
	 * The return value is File.
	 * </p>
	 * 
	 * @param zippedFilePath
	 *            is file path.
	 * @throws Exception
	 *             if some error occurs.
	 * @return File
	 */
	public static File getUnZipFilePath(String zippedFilePath) throws Exception {
		File aTmpFile = null;
		ZipInputStream zis = null;
		FileInputStream fis = null;
		FileOutputStream fos = null;
		String path = Utility.GetTempFilePath();
		try {
			fis = new FileInputStream(zippedFilePath);
			zis = new ZipInputStream(fis);

			// get the first entry
			ZipEntry ze = zis.getNextEntry();

			// write to a output stream
			aTmpFile = new File(path + "/temp/" + Phrase.UNZIP
					+ Utility.GetSysTimeString() + "_" + ze.getName());
			aTmpFile.deleteOnExit();
			fos = new FileOutputStream(aTmpFile);
			byte[] buffer = new byte[1024];
			int length;
			while ((length = zis.read(buffer)) > 0) {
				fos.write(buffer, 0, length);
				fos.flush();
			}
		} catch (Exception ex) {
			throw ex;
		} finally {
			fis.close();
			fos.close();
			zis.close();
		}
		return aTmpFile;
	}

	/**
	 * UnZip a zip file into a HashTable.
	 * <p>
	 * This method unzips the zip file that can be read for the input parameter</p>
	 * is.
	 * <p>
	 * The return value is HashTable.</p>
	 * 
	 * @param zippedFilePath
	 *            the file path for the zipped Content.
	 * @throws Exception
	 *             if some error occurs.
	 * @return HashTable, key is file name, object is content (Unzipped)
	 */
	public static Hashtable getUnZipFilePathHashTable(String zippedFilePath)
			throws Exception {
		Hashtable ht = new Hashtable();
		File file = null;
		try {
			file = NodeUtils.getUnZipFilePath(zippedFilePath);
			String filePath = file.getPath();
			ht.put(file.getName(), filePath.getBytes());

		} catch (Exception e) {
			e.printStackTrace();
		} finally {
		}
		return ht;
	}

	/**
	 * UnZip a zip file into a HashTable.
	 * <p>
	 * This method unzips the zip file that can be read for the input parameter</p>
	 * is.
	 * <p>
	 * The return value is HashTable.</p>
	 * 
	 * @param zippedContent
	 *            byte[] of a zipped content.
	 * @throws Exception
	 *             if some error occurs.
	 * @return HashTable, key is file name, object is content (Unzipped)
	 */
	public static Hashtable getUnZipFilePathHashTable(byte[] zippedContent)
			throws Exception {
		Hashtable ht = new Hashtable();
		File file = null;
		try {
			file = NodeUtils.getUnZipFilePath(zippedContent);
			String filePath = file.getPath();
			ht.put(file.getName(), filePath.getBytes());

		} catch (Exception e) {
			e.printStackTrace();
		} finally {
		}
		return ht;
	}

	/**
	 * UnZip a zip file into a HashTable.
	 * <p>
	 * This method unzips the zip file that can be read for the input parameter
	 * is.
	 * <p>
	 * The return value is HashTable.
	 * 
	 * @param filename
	 *            the file name for the zipped Content.
	 * @param zippedContent
	 *            byte[] of a zipped content.
	 * @throws Exception
	 *             if some error occurs.
	 * @return HashTable, key is file name, object is content (Unzipped)
	 */
	public static Hashtable getUnZipFileHashTable(String filename,
			byte[] zippedContent) throws Exception {
		final int BUFFER = 1024;
		InputStream is = new ByteArrayInputStream(zippedContent);
		Hashtable ht = new Hashtable();
		ZipEntry zipEntry = null;
		try {
			if (is != null) {
				ZipInputStream zis = new ZipInputStream(is);
				while ((zipEntry = zis.getNextEntry()) != null) {
					ByteArrayOutputStream baos = new ByteArrayOutputStream();
					byte[] temp = new byte[BUFFER];
					int read = 0;
					while ((read = zis.read(temp)) >= 0)
						baos.write(temp, 0, read);
					baos.flush();
					baos.close();
					ht.put(zipEntry.getName(), baos.toByteArray());
				}
			}
		} catch (Exception e) {
			e.printStackTrace();
		} finally {
		}
		return ht;
	}

	/**
	 * UnZip a zip file into a HashTable.
	 * <p>
	 * This method unzips the zip file that can be read for the input parameter</p>
	 * is.
	 * <p>
	 * The return value is HashTable.</p>
	 * 
	 * @param zippedContent
	 *            byte[] of a zipped content.
	 * @throws Exception
	 *             if some error occurs.
	 * @return HashTable, key is file name, object is content (Unzipped)
	 */
	public static Hashtable getUnZipFileHashTable(byte[] zippedContent)
			throws Exception {
		return getUnZipFileHashTable("zippedContent", zippedContent);
	}

	/**
	 * UnZip a zip file into a HashTable.
	 * <p>
	 * This method unzips the zip file that can be read for the input parameter</p>
	 * is.
	 * <p>
	 * The return value is HashTable.</p>
	 * 
	 * @param is
	 *            ClsNodeDocument of a zipped file.
	 * @throws Exception
	 *             if some error occurs.
	 * @return HashTable, key is file name, object is content (Unzipped)
	 */
	public static Hashtable getUnZipFileHashTable(ClsNodeDocument zippedFile)
			throws Exception {
		return getUnZipFileHashTable(zippedFile.getName(), zippedFile
				.getContent());
	}

	/**
	 * UUID Generator.
	 * <p>
	 * This method generate UUID with prefix"_".</p>
	 * <p>
	 * The return value is UUID string.</p>
	 * 
	 * @param
	 * @throws Exception
	 *             if some error occurs.
	 * @return String
	 */
	public static String getUUID() throws Exception {
		UUIDGenerator uuidGern = UUIDGenerator.getInstance();
		UUID uuid = new UUID();
		uuid = uuidGern.generateRandomBasedUUID();
		return "_" + uuid.toString();

	}

	/**
	 * UUID Generator.
	 * <p>
	 * This method generate UUID.</p>
	 * <p>
	 * The return value is UUID string.</p>
	 * 
	 * @param
	 * @throws Exception
	 *             if some error occurs.
	 * @return String
	 */
	public static String getRawUUID() throws Exception {
		UUIDGenerator uuidGern = UUIDGenerator.getInstance();
		UUID uuid = new UUID();
		uuid = uuidGern.generateRandomBasedUUID();
		return uuid.toString();

	}

	/**
	 * UnZip a zip file into a byte array.
	 * <p>
	 * This method unzips the zip file that can be read for the input parameter</p>
	 * is.
	 * <p>
	 * The return value is byte[].</p>
	 * 
	 * @param zippedContent
	 *            byte[] of a zipped file.
	 * @throws Exception
	 *             if some error occurs.
	 * @return byte[] content (Unzipped)
	 */

	public static byte[] getUnZipByteArray(byte[] zippedContent)
			throws Exception {
		ZipInputStream zis = null;
		ByteArrayOutputStream bros = new ByteArrayOutputStream();

		try {
			InputStream is = new ByteArrayInputStream(zippedContent);
			zis = new ZipInputStream(is);
			ZipEntry ze = zis.getNextEntry(); // get the first entry

			byte[] bytes = new byte[1024];
			int numRead = 0;
			while ((numRead = zis.read(bytes)) > 0) {
				bros.write(bytes, 0, numRead);
			}

			is.close();
			zis.close();
		} catch (Exception ex) {
			throw ex;
		}
		return bros.toByteArray();
	}

	/**
	 * AddSearchParameter add parameter from OperationList.config file to Log
	 * parameter table.
	 * <p>
	 * This method is used to add search parameter from OperationList.config
	 * file to Log parameter table.
	 * </p>
	 * <p>
	 * The return value is object[].</p>
	 * 
	 * @param opId
	 *            ,
	 * @param String
	 *            [] which is name,
	 * @param Object
	 *            [] which is value,
	 * @throws Exception
	 *             if some error occurs.
	 * @return object[],first is Object[] indicating name, second Object[]
	 *         indicating value
	 */
	public Object[] AddSearchParameter(String opId, String[] name,
			Object[] value) throws Exception {
		IOperationMgr operationMgr = DBManager
				.getOperationMgr(Phrase.AdministrationLoggerName);
		byte[] fileByte = operationMgr.GetOperationListFile();
		Object[] ret = new Object[2];
		if (fileByte != null) {
			ArrayList myName = new ArrayList();
			ArrayList myValue = new ArrayList();

			for (int i = 0; i < name.length; i++) {
				myName.add(name[i]);
				myValue.add(value[i]);
			}
			List operationMgrParameterList = null;
			SAXReader reader = new SAXReader();
			Document doc4jVR = reader.read(new ByteArrayInputStream(fileByte));
			Element rootElm = doc4jVR.getRootElement();
			// get operationId and Name
			List operationList = rootElm.elements("Operation");

			if (operationList != null && operationList.size() > 0) {
				Element upload = null;
				Element parameters = null;
				for (Iterator i = operationList.iterator(); i.hasNext();) {
					Element operation = (Element) i.next();
					// get parameter
					if (operation.attribute("id").getText().trim()
							.equalsIgnoreCase(opId.trim())) {
						upload = operation.element("Upload");
						parameters = upload.element("Parameters");
						operationMgrParameterList = parameters
								.elements("Parameter");

						break;
					}
				}
				if (operationMgrParameterList != null
						&& operationMgrParameterList.size() > 0) {
					for (Iterator i = operationMgrParameterList.iterator(); i
							.hasNext();) {
						Element parameter = (Element) i.next();
						myName.add(parameter.attribute("name").getText());
						myValue.add(parameter.getText());
					}
					ret[0] = myName.toArray();
					ret[1] = myValue.toArray();
				}
			}

		}

		return ret;
	}

	/**
	 * UpdateOperationLogParameter update parameter of Log parameter table.
	 * <p>
	 * This method is used to update parameter of Log parameter table.
	 * </p>
	 * <p>
	 * The return value is boolean.
	 * 
	 * @param LoggerName
	 *            ,
	 * @param transID
	 *            ,
	 * @param dataFlow
	 *            ,
	 * @param ClsNodeDocument[]
	 *            ,
	 * @throws Exception
	 *             if some error occurs.
	 * @return boolean. true/success false/fail
	 */
	public boolean UpdateOperationLogParameter(String LoggerName,
			String transID, String dataFlow, ClsNodeDocument[] docs)
			throws Exception {
		Object[] retVal = null;
		boolean ret = false;
		byte[] fileByte = null;
		Document doc4j = null;
		String[] names = null;
		String[] values = null;
		Object[] tmpName = null;
		Object[] tmpValue = null;
		List operationMgrParameterList = null;
		String[] parameterArr = null;
		String[] newParameter = null;

		INodeOperationLog logDB = DBManager.GetNodeOperationLog(LoggerName);
		INodeOperationParameter paramDB = DBManager
				.GetNodeOperationParameter(LoggerName);
		retVal = paramDB.GetParameterValues(transID);

		// get xpath value from parameter table
		if (retVal != null) {
			tmpName = (Object[]) retVal[0];
			names = new String[tmpName.length];
			for (int i = 0; i < names.length; i++) {
				names[i] = tmpName[i].toString();
			}
			tmpValue = (Object[]) retVal[1];
			values = new String[tmpValue.length];
			for (int i = 0; i < values.length; i++) {
				if (tmpValue[i] != null) {
					values[i] = tmpValue[i].toString();
				} else {
					values[i] = "";
				}
			}
		} else {
			return ret;
		}

		// get xpath structure from OperationList.config file
		IOperationMgr operationMgr = DBManager.getOperationMgr(LoggerName);
		INodeOperation operationDB = DBManager.GetNodeOperation(LoggerName);
		int operationID = operationDB.GetOperationID(dataFlow);
		fileByte = operationMgr.GetOperationListFile();
		if (fileByte != null) {
			SAXReader reader = new SAXReader();
			doc4j = reader.read(new ByteArrayInputStream(fileByte));
			Element rootElm = doc4j.getRootElement();
			// get operationId and Name
			List operationList = rootElm.elements("Operation");

			if (operationList != null && operationList.size() > 0) {
				for (Iterator i = operationList.iterator(); i.hasNext();) {
					Element operation = (Element) i.next();
					// get parameter
					if (operation.attribute("id").getText().trim()
							.equalsIgnoreCase(Integer.toString(operationID))) {
						Element upload = operation.element("Upload");
						Element parameters = upload.element("Parameters");
						operationMgrParameterList = parameters
								.elements("Parameter");

						break;
					}
				}
			}

		}

		if (operationMgrParameterList != null
				&& operationMgrParameterList.size() > 0) {
			parameterArr = new String[operationMgrParameterList.size()];
			for (int i = 0; i < operationMgrParameterList.size(); i++) {
				parameterArr[i] = ((Element) operationMgrParameterList.get(i))
						.attributeValue("name").trim();
			}
			// change value based on name and xpath
			if (docs != null && docs.length > 0) {
				for (int i = 0; i < docs.length; i++) {
					if (docs[i].getType().equalsIgnoreCase(Phrase.ZIP_TYPE)) {
						byte[] zipContent = docs[i].getContent();
						fileByte = NodeUtils.getUnZipByteArray(zipContent);
					} else {
						fileByte = docs[i].getContent();
					}
					if (fileByte != null) {
						newParameter = new String[values.length];
						SAXReader reader = new SAXReader();
						doc4j = reader.read(new ByteArrayInputStream(fileByte));
						for (int m = 0; newParameter != null
								&& m < newParameter.length; m++) {
							newParameter[m] = values[m];
						}
						for (int j = 0; parameterArr != null
								&& j < parameterArr.length; j++) {
							for (int k = 0; k < values.length; k++) {
								if (parameterArr[j].trim().equals(names[k])) {
									Node node = doc4j
											.selectSingleNode(values[k]);
									newParameter[k] = node == null ? "" : node
											.getText().trim();
								}
							}
						}

					}
				}
				OperationLog opLog = logDB.GetOperationLog(transID);
				paramDB.UpdateExistParameterValues(opLog.GetOpLogID(), names,
						newParameter);
			}
		}
		return ret;
	}
}
