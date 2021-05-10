package Node2.web.OperationMgr.model;

import Node.Web.Administration.BaseBean;

/**
 * <p>This class create OperationMgrParameterBean.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class OperationMgrParameterBean extends BaseBean {

	private String parameterId = "";
	private String parameterName = "";
	private String parameterValue = "";
	/**
	 * @return the parameterId
	 */
	public String getParameterId() {
		return parameterId;
	}
	/**
	 * @param parameterId the parameterId to set
	 */
	public void setParameterId(String parameterId) {
		this.parameterId = parameterId;
	}
	/**
	 * @return the parameterName
	 */
	public String getParameterName() {
		return parameterName;
	}
	/**
	 * @param parameterName the parameterName to set
	 */
	public void setParameterName(String parameterName) {
		this.parameterName = parameterName;
	}
	/**
	 * @return the parameterValue
	 */
	public String getParameterValue() {
		return parameterValue;
	}
	/**
	 * @param parameterValue the parameterValue to set
	 */
	public void setParameterValue(String parameterValue) {
		this.parameterValue = parameterValue;
	}
	
	
}
