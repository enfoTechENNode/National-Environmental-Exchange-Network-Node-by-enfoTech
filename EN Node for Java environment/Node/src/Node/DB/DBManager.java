package Node.DB;

import java.util.ResourceBundle;

import Node.Phrase;
import Node.DB.Interfaces.INodeAccountType;
import Node.DB.Interfaces.INodeDB;
import Node.DB.Interfaces.INodeDomain;
import Node.DB.Interfaces.INodeFileCabin;
import Node.DB.Interfaces.INodeOperation;
import Node.DB.Interfaces.INodeOperationLog;
import Node.DB.Interfaces.INodeOperationLogStatus;
import Node.DB.Interfaces.INodeOperationParameter;
import Node.DB.Interfaces.INodeUser;
import Node.DB.Interfaces.INodeUserOperation;
import Node.DB.Interfaces.INodeWebService;
import Node.DB.Interfaces.Configuration.IConfigurationMgr;
import Node.DB.Interfaces.Configuration.IGetServices;
import Node.DB.Interfaces.Configuration.IOperationMgr;
import Node.DB.Interfaces.Configuration.ISystemConfiguration;
import Node.DB.Interfaces.Configuration.ITaskConfiguration;

import com.enfotech.basecomponent.jndi.JNDIAccess;
/**
 * <p>This class create DBManager.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class DBManager {
	private String dbType = null;

	  /**
	   * Constructor
	   * @param 
	   * @return 
	   */
	private DBManager() {
		try {
			dbType = (String) JNDIAccess.GetJNDIValue(Phrase.dbType, false);
			if (this.dbType == null) {
				ResourceBundle bundle = ResourceBundle.getBundle("application");
				this.dbType = (String) bundle.getObject(Phrase.dbType);
			}
		} catch (Exception e) {
		}
	}

	/* Added 2007-7-18 begin */
	  /**
	   * Constructor
	   * @param dbTypeName
	   * @return 
	   */
	private DBManager(String dbTypeName) {
		try {
			dbType = (String) JNDIAccess.GetJNDIValue(dbTypeName, false);
			if (this.dbType == null) {
				ResourceBundle bundle = ResourceBundle.getBundle("application");
				this.dbType = (String) bundle.getObject(Phrase.dbType);
			}
		} catch (Exception e) {
		}
	}

	/* Added 2007-7-18 end */

	  /**
	   * GetSystemConfiguration
	   * @param loggerName
	   * @return ISystemConfiguration
	   */
	public static ISystemConfiguration GetSystemConfiguration(String loggerName)
			throws Exception {
		DBManager manager = new DBManager();
//		if (manager.dbType.equalsIgnoreCase("oracle9i")) {
//			return new Node.DB.Oracle.Configuration.SystemConfiguration(loggerName);
//		} else if (manager.dbType.equalsIgnoreCase("oracle10i") || manager.dbType.equalsIgnoreCase("oracle10g")) {
//			return new Node.DB.Oracle10i.Configuration.SystemConfiguration(
//					loggerName);
//		}
//		return null;

		return new Node.DB.Oracle.Configuration.SystemConfiguration(loggerName);

		}

	  /**
	   * GetNodeUser
	   * @param loggerName
	   * @return INodeUser
	   */
	public static INodeUser GetNodeUser(String loggerName) throws Exception {
		DBManager manager = new DBManager();
//		if (manager.dbType.equalsIgnoreCase("oracle9i")) {
//			return new Node.DB.Oracle.NodeUser(loggerName);
//		} else if (manager.dbType.equalsIgnoreCase("oracle10i") || manager.dbType.equalsIgnoreCase("oracle10g")) {
//			return new Node.DB.Oracle10i.NodeUser(loggerName);
//		}
//		return null;

		return new Node.DB.Oracle.NodeUser(loggerName);
	
	}

	  /**
	   * GetNodeOperationLog
	   * @param loggerName
	   * @return INodeOperationLog
	   */
	public static INodeOperationLog GetNodeOperationLog(String loggerName)
			throws Exception {
		DBManager manager = new DBManager();
//		if (manager.dbType.equalsIgnoreCase("oracle9i")) {
//			return new Node.DB.Oracle.NodeOperationLog(loggerName);
//		} else if (manager.dbType.equalsIgnoreCase("oracle10i") || manager.dbType.equalsIgnoreCase("oracle10g")) {
//			return new Node.DB.Oracle10i.NodeOperationLog(loggerName);
//		}
//		return null;

		return new Node.DB.Oracle.NodeOperationLog(loggerName);

	}

	  /**
	   * GetNodeOperation
	   * @param loggerName
	   * @return INodeOperation
	   */
	public static INodeOperation GetNodeOperation(String loggerName)
			throws Exception {
		DBManager manager = new DBManager();
//		if (manager.dbType.equalsIgnoreCase("oracle9i")) {
//			return new Node.DB.Oracle.NodeOperation(loggerName);
//		} else if (manager.dbType.equalsIgnoreCase("oracle10i") || manager.dbType.equalsIgnoreCase("oracle10g")) {
//			return new Node.DB.Oracle10i.NodeOperation(loggerName);
//		}
//		return null;
		return new Node.DB.Oracle.NodeOperation(loggerName);
	}

	  /**
	   * GetNodeOperationParameter
	   * @param loggerName
	   * @return INodeOperationParameter
	   */
	public static INodeOperationParameter GetNodeOperationParameter(
			String loggerName) throws Exception {
		DBManager manager = new DBManager();
//		if (manager.dbType.equalsIgnoreCase("oracle9i")) {
//			return new Node.DB.Oracle.NodeOperationParameter(loggerName);
//		} else if (manager.dbType.equalsIgnoreCase("oracle10i") || manager.dbType.equalsIgnoreCase("oracle10g")) {
//			return new Node.DB.Oracle10i.NodeOperationParameter(loggerName);
//		}
//		return null;
		return new Node.DB.Oracle.NodeOperationParameter(loggerName);
	}

	  /**
	   * GetNodeWebService
	   * @param loggerName
	   * @return INodeWebService
	   */
	public static INodeWebService GetNodeWebService(String loggerName)
			throws Exception {
		DBManager manager = new DBManager();
//		if (manager.dbType.equalsIgnoreCase("oracle9i")) {
//			return new Node.DB.Oracle.NodeWebService(loggerName);
//		} else if (manager.dbType.equalsIgnoreCase("oracle10i") || manager.dbType.equalsIgnoreCase("oracle10g")) {
//			return new Node.DB.Oracle10i.NodeWebService(loggerName);
//		}
//		return null;
		return new Node.DB.Oracle.NodeWebService(loggerName);
	}

	  /**
	   * GetNodeDomain
	   * @param loggerName
	   * @return INodeDomain
	   */
	public static INodeDomain GetNodeDomain(String loggerName) throws Exception {
		DBManager manager = new DBManager();
//		if (manager.dbType.equalsIgnoreCase("oracle9i")) {
//			return new Node.DB.Oracle.NodeDomain(loggerName);
//		} else if (manager.dbType.equalsIgnoreCase("oracle10i") || manager.dbType.equalsIgnoreCase("oracle10g")) {
//			return new Node.DB.Oracle10i.NodeDomain(loggerName);
//		}
//		return null;
		return new Node.DB.Oracle.NodeDomain(loggerName);
	}

	  /**
	   * GetNodeOperationLogStatus
	   * @param loggerName
	   * @return INodeOperationLogStatus
	   */
	public static INodeOperationLogStatus GetNodeOperationLogStatus(
			String loggerName) throws Exception {
		DBManager manager = new DBManager();
//		if (manager.dbType.equalsIgnoreCase("oracle9i")) {
//			return new Node.DB.Oracle.NodeOperationLogStatus(loggerName);
//		} else if (manager.dbType.equalsIgnoreCase("oracle10i") || manager.dbType.equalsIgnoreCase("oracle10g")) {
//			return new Node.DB.Oracle10i.NodeOperationLogStatus(loggerName);
//		}
//		return null;
		return new Node.DB.Oracle.NodeOperationLogStatus(loggerName);
	}

	  /**
	   * GetNodeAccountType
	   * @param loggerName
	   * @return INodeAccountType
	   */
	public static INodeAccountType GetNodeAccountType(String loggerName)
			throws Exception {
		DBManager manager = new DBManager();
//		if (manager.dbType.equalsIgnoreCase("oracle9i")) {
//			return new Node.DB.Oracle.NodeAccountType(loggerName);
//		} else if (manager.dbType.equalsIgnoreCase("oracle10i") || manager.dbType.equalsIgnoreCase("oracle10g")) {
//			return new Node.DB.Oracle10i.NodeAccountType(loggerName);
//		}
//		return null;
		return new Node.DB.Oracle.NodeAccountType(loggerName);
	}

	  /**
	   * GetNodeFileCabin
	   * @param loggerName
	   * @return INodeFileCabin
	   */
	public static INodeFileCabin GetNodeFileCabin(String loggerName)
			throws Exception {
		DBManager manager = new DBManager();
//		if (manager.dbType.equalsIgnoreCase("oracle9i")) {
//			return new Node.DB.Oracle.NodeFileCabin(loggerName);
//		} else if (manager.dbType.equalsIgnoreCase("oracle10i") || manager.dbType.equalsIgnoreCase("oracle10g")) {
//			return new Node.DB.Oracle10i.NodeFileCabin(loggerName);
//		}
//		return null;
		return new Node.DB.Oracle.NodeFileCabin(loggerName);
	}

	  /**
	   * GetNodeUserOperation
	   * @param loggerName
	   * @return INodeUserOperation
	   */
	public static INodeUserOperation GetNodeUserOperation(String loggerName)
			throws Exception {
		DBManager manager = new DBManager();
//		if (manager.dbType.equalsIgnoreCase("oracle9i")) {
//			return new Node.DB.Oracle.NodeUserOperation(loggerName);
//		} else if (manager.dbType.equalsIgnoreCase("oracle10i") || manager.dbType.equalsIgnoreCase("oracle10g")) {
//			return new Node.DB.Oracle10i.NodeUserOperation(loggerName);
//		}
//		return null;
		return new Node.DB.Oracle.NodeUserOperation(loggerName);
	}

	  /**
	   * GetTaskConfiguration
	   * @param loggerName
	   * @return ITaskConfiguration
	   */
	public static ITaskConfiguration GetTaskConfiguration(String loggerName)
			throws Exception {
		DBManager manager = new DBManager();
//		if (manager.dbType.equalsIgnoreCase("oracle9i")) {
//			return new Node.DB.Oracle.Configuration.TaskConfiguration(loggerName);
//		} else if (manager.dbType.equalsIgnoreCase("oracle10i") || manager.dbType.equalsIgnoreCase("oracle10g")) {
//			return new Node.DB.Oracle10i.Configuration.TaskConfiguration(
//					loggerName);
//		}
//		return null;
		return new Node.DB.Oracle.Configuration.TaskConfiguration(loggerName);
	}


	/* Added by Charlie Zhang by 2007-12-7 --- 2007-12-31END */

	  /**
	   * getGetServices
	   * @param loggerName
	   * @param dbTypeName
	   * @return IGetServices
	   */
	public static IGetServices getGetServices(String loggerName) {
		DBManager manager = new DBManager();
//		if (manager.dbType.equalsIgnoreCase("oracle9i")) {
//			return new Node.DB.Oracle.GetServices(loggerName);
//		} else if (manager.dbType.equalsIgnoreCase("oracle10i") || manager.dbType.equalsIgnoreCase("oracle10g")) {
//			return new Node.DB.Oracle10i.GetServices(loggerName);
//		}
//		return null;
		return new Node.DB.Oracle.Configuration.GetServices(loggerName);
	}

	/* Added by Charlie Zhang by 2008-4-27 --- 2008-4-30 END */

	  /**
	   * getOperationMgr
	   * @param loggerName
	   * @return IOperationMgr
	   */
	public static IOperationMgr getOperationMgr(String loggerName) {
		DBManager manager = new DBManager();
//		if (manager.dbType.equalsIgnoreCase("oracle9i")) {
//			return new Node.DB.Oracle.Configuration.OperationMgr(loggerName);
//		} else if (manager.dbType.equalsIgnoreCase("oracle10i") || manager.dbType.equalsIgnoreCase("oracle10g")) {
//			return new Node.DB.Oracle10i.Configuration.OperationMgr(loggerName);
//		}
//		return null;
		return new Node.DB.Oracle.Configuration.OperationMgr(loggerName);
	}
	
	  /**
	   * GetConfigurationMgr
	   * @param loggerName
	   * @return IConfigurationMgr
	   */
	public static IConfigurationMgr GetConfigurationMgr(String loggerName)
	{
		DBManager manager = new DBManager();
//		if (manager.dbType.equalsIgnoreCase("oracle9i")) {
//			return new Node.DB.Oracle.Configuration.ConfigurationMgr(loggerName);
//		} else if (manager.dbType.equalsIgnoreCase("oracle10i") || manager.dbType.equalsIgnoreCase("oracle10g")) {
//			return new Node.DB.Oracle10i.Configuration.ConfigurationMgr(loggerName);
//		}
//		return null;
		return new Node.DB.Oracle.Configuration.ConfigurationMgr(loggerName);
	}

	  /**
	   * getNodeDB
	   * @param loggerName
	   * @return INodeDB
	   */
	public static INodeDB getNodeDB(String loggerName) {
		DBManager manager = new DBManager();
//		if (manager.dbType.equalsIgnoreCase("oracle9i")) {
//			return new Node.DB.Oracle.NodeDB(loggerName);
//		} else if (manager.dbType.equalsIgnoreCase("oracle10i") || manager.dbType.equalsIgnoreCase("oracle10g")) {
//			return new Node.DB.Oracle10i.NodeDB(loggerName);
//		}
//		return null;
		return new Node.DB.Oracle.NodeDB(loggerName);
	}

}
