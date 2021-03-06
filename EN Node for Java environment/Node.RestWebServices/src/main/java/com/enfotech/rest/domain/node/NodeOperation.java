package com.enfotech.rest.domain.node;
// Generated Oct 11, 2013 2:00:01 PM by Hibernate Tools 3.6.0


import java.sql.Clob;
import java.util.Date;
import java.util.HashSet;
import java.util.Set;

/**
 * NodeOperation generated by hbm2java
 */
public class NodeOperation  implements java.io.Serializable {


     private long operationId;
     private NodeDomain nodeDomain;
     private NodeWebService nodeWebService;
     private String operationName;
     private String operationDesc;
     private String operationType;
     private Clob operationConfig;
     private String operationStatusCd;
     private String operationStatusMsg;
     private Date createdDttm;
     private String createdBy;
     private Date updatedDttm;
     private String updatedBy;
     private String versionNo;
     private Clob operationConfigClob;
     private String publishInd;
     private String restInd;
     private Set<NodeOperationLog> nodeOperationLogs = new HashSet<NodeOperationLog>(0);
     private Set<SysUserInfo> sysUserInfos = new HashSet<SysUserInfo>(0);

    public NodeOperation() {
    }

	
    public NodeOperation(long operationId, String operationStatusCd, String versionNo) {
        this.operationId = operationId;
        this.operationStatusCd = operationStatusCd;
        this.versionNo = versionNo;
    }
    public NodeOperation(long operationId, NodeDomain nodeDomain, NodeWebService nodeWebService, String operationName, String operationDesc, String operationType, Clob operationConfig, String operationStatusCd, String operationStatusMsg, Date createdDttm, String createdBy, Date updatedDttm, String updatedBy, String versionNo, Clob operationConfigClob, String publishInd, Set<NodeOperationLog> nodeOperationLogs, Set<SysUserInfo> sysUserInfos) {
       this.operationId = operationId;
       this.nodeDomain = nodeDomain;
       this.nodeWebService = nodeWebService;
       this.operationName = operationName;
       this.operationDesc = operationDesc;
       this.operationType = operationType;
       this.operationConfig = operationConfig;
       this.operationStatusCd = operationStatusCd;
       this.operationStatusMsg = operationStatusMsg;
       this.createdDttm = createdDttm;
       this.createdBy = createdBy;
       this.updatedDttm = updatedDttm;
       this.updatedBy = updatedBy;
       this.versionNo = versionNo;
       this.operationConfigClob = operationConfigClob;
       this.publishInd = publishInd;
       this.nodeOperationLogs = nodeOperationLogs;
       this.sysUserInfos = sysUserInfos;
    }
   
    public long getOperationId() {
        return this.operationId;
    }
    
    public void setOperationId(long operationId) {
        this.operationId = operationId;
    }
    public NodeDomain getNodeDomain() {
        return this.nodeDomain;
    }
    
    public void setNodeDomain(NodeDomain nodeDomain) {
        this.nodeDomain = nodeDomain;
    }
    public NodeWebService getNodeWebService() {
        return this.nodeWebService;
    }
    
    public void setNodeWebService(NodeWebService nodeWebService) {
        this.nodeWebService = nodeWebService;
    }
    public String getOperationName() {
        return this.operationName;
    }
    
    public void setOperationName(String operationName) {
        this.operationName = operationName;
    }
    public String getOperationDesc() {
        return this.operationDesc;
    }
    
    public void setOperationDesc(String operationDesc) {
        this.operationDesc = operationDesc;
    }
    public String getOperationType() {
        return this.operationType;
    }
    
    public void setOperationType(String operationType) {
        this.operationType = operationType;
    }
    public Clob getOperationConfig() {
        return this.operationConfig;
    }
    
    public void setOperationConfig(Clob operationConfig) {
        this.operationConfig = operationConfig;
    }
    public String getOperationStatusCd() {
        return this.operationStatusCd;
    }
    
    public void setOperationStatusCd(String operationStatusCd) {
        this.operationStatusCd = operationStatusCd;
    }
    public String getOperationStatusMsg() {
        return this.operationStatusMsg;
    }
    
    public void setOperationStatusMsg(String operationStatusMsg) {
        this.operationStatusMsg = operationStatusMsg;
    }
    public Date getCreatedDttm() {
        return this.createdDttm;
    }
    
    public void setCreatedDttm(Date createdDttm) {
        this.createdDttm = createdDttm;
    }
    public String getCreatedBy() {
        return this.createdBy;
    }
    
    public void setCreatedBy(String createdBy) {
        this.createdBy = createdBy;
    }
    public Date getUpdatedDttm() {
        return this.updatedDttm;
    }
    
    public void setUpdatedDttm(Date updatedDttm) {
        this.updatedDttm = updatedDttm;
    }
    public String getUpdatedBy() {
        return this.updatedBy;
    }
    
    public void setUpdatedBy(String updatedBy) {
        this.updatedBy = updatedBy;
    }
    public String getVersionNo() {
        return this.versionNo;
    }
    
    public void setVersionNo(String versionNo) {
        this.versionNo = versionNo;
    }
    public Clob getOperationConfigClob() {
        return this.operationConfigClob;
    }
    
    public void setOperationConfigClob(Clob operationConfigClob) {
        this.operationConfigClob = operationConfigClob;
    }
    public String getPublishInd() {
        return this.publishInd;
    }
    
    public void setPublishInd(String publishInd) {
        this.publishInd = publishInd;
    }
    public Set<NodeOperationLog> getNodeOperationLogs() {
        return this.nodeOperationLogs;
    }
    
    public void setNodeOperationLogs(Set<NodeOperationLog> nodeOperationLogs) {
        this.nodeOperationLogs = nodeOperationLogs;
    }
    public Set<SysUserInfo> getSysUserInfos() {
        return this.sysUserInfos;
    }
    
    public void setSysUserInfos(Set<SysUserInfo> sysUserInfos) {
        this.sysUserInfos = sysUserInfos;
    }


	public void setRestInd(String restInd) {
		this.restInd = restInd;
	}


	public String getRestInd() {
		return restInd;
	}




}


