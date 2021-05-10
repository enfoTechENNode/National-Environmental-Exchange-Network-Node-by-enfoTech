package com.enfotech.rest.domain.node;

// Generated Oct 3, 2013 4:48:47 PM by Hibernate Tools 3.4.0.CR1

import java.math.BigDecimal;

/**
 * RcraSubDtlId generated by hbm2java
 */
public class RcraSubDtlId implements java.io.Serializable {

	private BigDecimal rcraSubId;
	private String facEpaId;

	public RcraSubDtlId() {
	}

	public RcraSubDtlId(BigDecimal rcraSubId, String facEpaId) {
		this.rcraSubId = rcraSubId;
		this.facEpaId = facEpaId;
	}

	public BigDecimal getRcraSubId() {
		return this.rcraSubId;
	}

	public void setRcraSubId(BigDecimal rcraSubId) {
		this.rcraSubId = rcraSubId;
	}

	public String getFacEpaId() {
		return this.facEpaId;
	}

	public void setFacEpaId(String facEpaId) {
		this.facEpaId = facEpaId;
	}

	public boolean equals(Object other) {
		if ((this == other))
			return true;
		if ((other == null))
			return false;
		if (!(other instanceof RcraSubDtlId))
			return false;
		RcraSubDtlId castOther = (RcraSubDtlId) other;

		return ((this.getRcraSubId() == castOther.getRcraSubId()) || (this
				.getRcraSubId() != null && castOther.getRcraSubId() != null && this
				.getRcraSubId().equals(castOther.getRcraSubId())))
				&& ((this.getFacEpaId() == castOther.getFacEpaId()) || (this
						.getFacEpaId() != null
						&& castOther.getFacEpaId() != null && this
						.getFacEpaId().equals(castOther.getFacEpaId())));
	}

	public int hashCode() {
		int result = 17;

		result = 37 * result
				+ (getRcraSubId() == null ? 0 : this.getRcraSubId().hashCode());
		result = 37 * result
				+ (getFacEpaId() == null ? 0 : this.getFacEpaId().hashCode());
		return result;
	}

}
