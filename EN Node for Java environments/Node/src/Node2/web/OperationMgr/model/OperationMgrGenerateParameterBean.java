package Node2.web.OperationMgr.model;

import Node.Web.Administration.BaseBean;

/**
 * <p>This class create OperationMgrGenerateParameterBean.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class OperationMgrGenerateParameterBean extends BaseBean {

	private String generateParameterName = "";
	private String generateParameterValue = "";
	/**
	 * @return the generateParameterName
	 */
	public String getGenerateParameterName() {
		return generateParameterName;
	}
	/**
	 * @param generateParameterName the generateParameterName to set
	 */
	public void setGenerateParameterName(String generateParameterName) {
		this.generateParameterName = generateParameterName;
	}
	/**
	 * @return the generateParameterValue
	 */
	public String getGenerateParameterValue() {
		return generateParameterValue;
	}
	/**
	 * @param generateParameterValue the generateParameterValue to set
	 */
	public void setGenerateParameterValue(String generateParameterValue) {
		this.generateParameterValue = generateParameterValue;
	}
	
	
}
