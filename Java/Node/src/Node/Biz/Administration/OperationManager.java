package Node.Biz.Administration;

import java.sql.Date;
/**
 * <p>This class create OperationManager class.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class OperationManager {
	private int submitId = -1;
	private String operationName = null;
	private String statusCD = null;
	private String submitURL = null;
	private Date submitDate = null;
	private String version = null;
	private String transId = null;
	private String supplyTransId = null;
	private String dataFlow = null;

	public int getSubmitId() {
		return submitId;
	}

	public void setSubmitId(int submitId) {
		this.submitId = submitId;
	}

	public String getOperationName() {
		return operationName;
	}

	public void setOperationName(String operationName) {
		this.operationName = operationName;
	}

	public String getStatusCD() {
		return statusCD;
	}

	public void setStatusCD(String statusCD) {
		this.statusCD = statusCD;
	}

	public String getSubmitURL() {
		return submitURL;
	}

	public void setSubmitURL(String submitURL) {
		this.submitURL = submitURL;
	}

	public Date getSubmitDate() {
		return submitDate;
	}

	public void setSubmitDate(Date submitDate) {
		this.submitDate = submitDate;
	}

	public String getVersion() {
		return version;
	}

	public void setVersion(String version) {
		this.version = version;
	}

	public String getTransId() {
		return transId;
	}

	public void setTransId(String transId) {
		this.transId = transId;
	}

	public String getSupplyTransId() {
		return supplyTransId;
	}

	public void setSupplyTransId(String supplyTransId) {
		this.supplyTransId = supplyTransId;
	}

	public String getDataFlow() {
		return dataFlow;
	}

	public void setDataFlow(String dataFlow) {
		this.dataFlow = dataFlow;
	}

}
