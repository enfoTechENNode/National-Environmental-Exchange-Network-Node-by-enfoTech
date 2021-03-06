package com.enfotech.rest.domain.node;
// Generated Oct 11, 2013 2:00:01 PM by Hibernate Tools 3.6.0



/**
 * NodeOperationLogParameter generated by hbm2java
 */
public class NodeOperationLogParameter  implements java.io.Serializable {


     private NodeOperationLogParameterId id;
     private NodeOperationLog nodeOperationLog;
     private String parameterValue;

    public NodeOperationLogParameter() {
    }

	
    public NodeOperationLogParameter(NodeOperationLogParameterId id, NodeOperationLog nodeOperationLog) {
        this.id = id;
        this.nodeOperationLog = nodeOperationLog;
    }
    public NodeOperationLogParameter(NodeOperationLogParameterId id, NodeOperationLog nodeOperationLog, String parameterValue) {
       this.id = id;
       this.nodeOperationLog = nodeOperationLog;
       this.parameterValue = parameterValue;
    }
   
    public NodeOperationLogParameterId getId() {
        return this.id;
    }
    
    public void setId(NodeOperationLogParameterId id) {
        this.id = id;
    }
    public NodeOperationLog getNodeOperationLog() {
        return this.nodeOperationLog;
    }
    
    public void setNodeOperationLog(NodeOperationLog nodeOperationLog) {
        this.nodeOperationLog = nodeOperationLog;
    }
    public String getParameterValue() {
        return this.parameterValue;
    }
    
    public void setParameterValue(String parameterValue) {
        this.parameterValue = parameterValue;
    }




}


