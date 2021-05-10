package com.enfotech.rest.domain.node;
// Generated Oct 11, 2013 2:00:01 PM by Hibernate Tools 3.6.0



/**
 * NodeAccountTypeXref generated by hbm2java
 */
public class NodeAccountTypeXref  implements java.io.Serializable {


     private long accountTypeXrefId;
     private SysUserInfo sysUserInfo;
     private NodeAccountType nodeAccountType;
     private NodeDomain nodeDomain;

    public NodeAccountTypeXref() {
    }

	
    public NodeAccountTypeXref(long accountTypeXrefId) {
        this.accountTypeXrefId = accountTypeXrefId;
    }
    public NodeAccountTypeXref(long accountTypeXrefId, SysUserInfo sysUserInfo, NodeAccountType nodeAccountType, NodeDomain nodeDomain) {
       this.accountTypeXrefId = accountTypeXrefId;
       this.sysUserInfo = sysUserInfo;
       this.nodeAccountType = nodeAccountType;
       this.nodeDomain = nodeDomain;
    }
   
    public long getAccountTypeXrefId() {
        return this.accountTypeXrefId;
    }
    
    public void setAccountTypeXrefId(long accountTypeXrefId) {
        this.accountTypeXrefId = accountTypeXrefId;
    }
    public SysUserInfo getSysUserInfo() {
        return this.sysUserInfo;
    }
    
    public void setSysUserInfo(SysUserInfo sysUserInfo) {
        this.sysUserInfo = sysUserInfo;
    }
    public NodeAccountType getNodeAccountType() {
        return this.nodeAccountType;
    }
    
    public void setNodeAccountType(NodeAccountType nodeAccountType) {
        this.nodeAccountType = nodeAccountType;
    }
    public NodeDomain getNodeDomain() {
        return this.nodeDomain;
    }
    
    public void setNodeDomain(NodeDomain nodeDomain) {
        this.nodeDomain = nodeDomain;
    }




}


