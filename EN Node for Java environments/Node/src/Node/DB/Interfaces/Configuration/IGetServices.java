package Node.DB.Interfaces.Configuration;

/**
 * <p>This class create IGetServices interface.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public interface IGetServices {
	/**
	 * SaveGetServices.
	 * @param nodeIdentifier.
	 * @param nodeName.
	 * @param nodeAddress.
	 * @param organizationIdentifier.
	 * @param nodeContact.
	 * @param nodeVersionIdentifier.
	 * @param nodeDeploymentTypeCode.
	 * @param nodeStatus.
	 * @param nodePropertyName.
	 * @param nodePropertyValue.
	 * @param north.
	 * @param south.
	 * @param east.
	 * @param west.
	 * @return boolean
	 */
	public boolean SaveGetServices (String nodeIdentifier,String nodeName,String nodeAddress,String organizationIdentifier,
			  String nodeContact,String nodeVersionIdentifier,String nodeDeploymentTypeCode,String nodeStatus,String[] nodePropertyName,
			  String[] nodePropertyValue,String north,String south,String east,String west);

	// WI 21296
	/**
	 * getDedlFile.
	 * @param version.
	 * @param fileName.
	 * @return byte[]
	 */
	public byte[] getConfigFile(String version,String fileName);
	
	// WI 21296
	/**
	 * SaveDedlFile.
	 * @param xmlFile.
	 * @param version.
	 * @param fileName.
	 * @return boolean
	 */
	public boolean saveConfigFile(byte[] xmlFile, String version,String fileName);
}
