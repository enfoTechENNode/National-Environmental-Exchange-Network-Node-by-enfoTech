package Node2.web.OperationMgr.model;

import Node.Web.Administration.BaseBean;

/**
 * <p>This class create OperationMgrTemplateBean.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class OperationMgrTemplateBean extends BaseBean {

	private String templateId = "";
	private String templateName = "";
	private String transformSuffix = "";
	/**
	 * @return the templateId
	 */
	public String getTemplateId() {
		return templateId;
	}
	/**
	 * @param templateId the templateId to set
	 */
	public void setTemplateId(String templateId) {
		this.templateId = templateId;
	}
	/**
	 * @return the templateName
	 */
	public String getTemplateName() {
		return templateName;
	}
	/**
	 * @param templateName the templateName to set
	 */
	public void setTemplateName(String templateName) {
		this.templateName = templateName;
	}
	/**
	 * @return the transformSuffix
	 */
	public String getTransformSuffix() {
		return transformSuffix;
	}
	/**
	 * @param transformSuffix the transformSuffix to set
	 */
	public void setTransformSuffix(String transformSuffix) {
		this.transformSuffix = transformSuffix;
	}
	

	
}
