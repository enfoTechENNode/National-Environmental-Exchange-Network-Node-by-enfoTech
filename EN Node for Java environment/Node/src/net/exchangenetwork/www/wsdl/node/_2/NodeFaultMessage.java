
/**
 * NodeFaultMessage.java
 *
 * This file was auto-generated from WSDL
 * by the Apache Axis2 version: 1.4.1  Built on : Aug 19, 2008 (10:13:39 LKT)
 */

package net.exchangenetwork.www.wsdl.node._2;

public class NodeFaultMessage extends java.lang.Exception{
    
    private net.exchangenetwork.www.schema.node._2.NodeFaultDetailType faultMessage;
    
    public NodeFaultMessage() {
        super("NodeFaultMessage");
    }
           
    public NodeFaultMessage(java.lang.String s) {
       super(s);
    }
    
    public NodeFaultMessage(java.lang.String s, java.lang.Throwable ex) {
      super(s, ex);
    }
    
    public void setFaultMessage(net.exchangenetwork.www.schema.node._2.NodeFaultDetailType msg){
       faultMessage = msg;
    }
    
    public net.exchangenetwork.www.schema.node._2.NodeFaultDetailType getFaultMessage(){
       return faultMessage;
    }
}
    