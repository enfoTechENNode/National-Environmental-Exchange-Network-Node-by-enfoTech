/**
 * PolicyInfo.java
 *
 * This file was auto-generated from WSDL
 * by the Apache Axis 1.2alpha Dec 01, 2003 (04:33:24 EST) WSDL2Java emitter.
 */

package Node.NAAS.Types.Policy;

public class PolicyInfo  implements java.io.Serializable {
    private java.lang.String policyId;
    private java.lang.String node;
    private java.lang.String subject;
    private java.lang.String method;
    private java.lang.String request;
    private java.lang.String params;
    private java.lang.String action;

    public PolicyInfo() {
    }


    /**
     * Gets the policyId value for this PolicyInfo.
     *
     * @return policyId
     */
    public java.lang.String getPolicyId() {
        return policyId;
    }


    /**
     * Sets the policyId value for this PolicyInfo.
     *
     * @param policyId
     */
    public void setPolicyId(java.lang.String policyId) {
        this.policyId = policyId;
    }


    /**
     * Gets the node value for this PolicyInfo.
     *
     * @return node
     */
    public java.lang.String getNode() {
        return node;
    }


    /**
     * Sets the node value for this PolicyInfo.
     *
     * @param node
     */
    public void setNode(java.lang.String node) {
        this.node = node;
    }


    /**
     * Gets the subject value for this PolicyInfo.
     *
     * @return subject
     */
    public java.lang.String getSubject() {
        return subject;
    }


    /**
     * Sets the subject value for this PolicyInfo.
     *
     * @param subject
     */
    public void setSubject(java.lang.String subject) {
        this.subject = subject;
    }


    /**
     * Gets the method value for this PolicyInfo.
     *
     * @return method
     */
    public java.lang.String getMethod() {
        return method;
    }


    /**
     * Sets the method value for this PolicyInfo.
     *
     * @param method
     */
    public void setMethod(java.lang.String method) {
        this.method = method;
    }


    /**
     * Gets the request value for this PolicyInfo.
     *
     * @return request
     */
    public java.lang.String getRequest() {
        return request;
    }


    /**
     * Sets the request value for this PolicyInfo.
     *
     * @param request
     */
    public void setRequest(java.lang.String request) {
        this.request = request;
    }


    /**
     * Gets the params value for this PolicyInfo.
     *
     * @return params
     */
    public java.lang.String getParams() {
        return params;
    }


    /**
     * Sets the params value for this PolicyInfo.
     *
     * @param params
     */
    public void setParams(java.lang.String params) {
        this.params = params;
    }


    /**
     * Gets the action value for this PolicyInfo.
     *
     * @return action
     */
    public java.lang.String getAction() {
        return action;
    }


    /**
     * Sets the action value for this PolicyInfo.
     *
     * @param action
     */
    public void setAction(java.lang.String action) {
        this.action = action;
    }

    private java.lang.Object __equalsCalc = null;
    public synchronized boolean equals(java.lang.Object obj) {
        if (!(obj instanceof PolicyInfo)) return false;
        PolicyInfo other = (PolicyInfo) obj;
        if (obj == null) return false;
        if (this == obj) return true;
        if (__equalsCalc != null) {
            return (__equalsCalc == obj);
        }
        __equalsCalc = obj;
        boolean _equals;
        _equals = true &&
            ((this.policyId==null && other.getPolicyId()==null) ||
             (this.policyId!=null &&
              this.policyId.equals(other.getPolicyId()))) &&
            ((this.node==null && other.getNode()==null) ||
             (this.node!=null &&
              this.node.equals(other.getNode()))) &&
            ((this.subject==null && other.getSubject()==null) ||
             (this.subject!=null &&
              this.subject.equals(other.getSubject()))) &&
            ((this.method==null && other.getMethod()==null) ||
             (this.method!=null &&
              this.method.equals(other.getMethod()))) &&
            ((this.request==null && other.getRequest()==null) ||
             (this.request!=null &&
              this.request.equals(other.getRequest()))) &&
            ((this.params==null && other.getParams()==null) ||
             (this.params!=null &&
              this.params.equals(other.getParams()))) &&
            ((this.action==null && other.getAction()==null) ||
             (this.action!=null &&
              this.action.equals(other.getAction())));
        __equalsCalc = null;
        return _equals;
    }

    private boolean __hashCodeCalc = false;
    public synchronized int hashCode() {
        if (__hashCodeCalc) {
            return 0;
        }
        __hashCodeCalc = true;
        int _hashCode = 1;
        if (getPolicyId() != null) {
            _hashCode += getPolicyId().hashCode();
        }
        if (getNode() != null) {
            _hashCode += getNode().hashCode();
        }
        if (getSubject() != null) {
            _hashCode += getSubject().hashCode();
        }
        if (getMethod() != null) {
            _hashCode += getMethod().hashCode();
        }
        if (getRequest() != null) {
            _hashCode += getRequest().hashCode();
        }
        if (getParams() != null) {
            _hashCode += getParams().hashCode();
        }
        if (getAction() != null) {
            _hashCode += getAction().hashCode();
        }
        __hashCodeCalc = false;
        return _hashCode;
    }

    // Type metadata
    private static org.apache.axis.description.TypeDesc typeDesc =
        new org.apache.axis.description.TypeDesc(PolicyInfo.class);

    static {
        typeDesc.setXmlType(new javax.xml.namespace.QName("http://exchangenetwork.net/schema/cdxPolicy.xsd", "PolicyInfo"));
        org.apache.axis.description.ElementDesc elemField = new org.apache.axis.description.ElementDesc();
        elemField.setFieldName("policyId");
        elemField.setXmlName(new javax.xml.namespace.QName("http://exchangenetwork.net/schema/cdxPolicy.xsd", "PolicyId"));
        elemField.setXmlType(new javax.xml.namespace.QName("http://www.w3.org/2001/XMLSchema", "string"));
        typeDesc.addFieldDesc(elemField);
        elemField = new org.apache.axis.description.ElementDesc();
        elemField.setFieldName("node");
        elemField.setXmlName(new javax.xml.namespace.QName("http://exchangenetwork.net/schema/cdxPolicy.xsd", "Node"));
        elemField.setXmlType(new javax.xml.namespace.QName("http://www.w3.org/2001/XMLSchema", "string"));
        typeDesc.addFieldDesc(elemField);
        elemField = new org.apache.axis.description.ElementDesc();
        elemField.setFieldName("subject");
        elemField.setXmlName(new javax.xml.namespace.QName("http://exchangenetwork.net/schema/cdxPolicy.xsd", "Subject"));
        elemField.setXmlType(new javax.xml.namespace.QName("http://www.w3.org/2001/XMLSchema", "string"));
        typeDesc.addFieldDesc(elemField);
        elemField = new org.apache.axis.description.ElementDesc();
        elemField.setFieldName("method");
        elemField.setXmlName(new javax.xml.namespace.QName("http://exchangenetwork.net/schema/cdxPolicy.xsd", "Method"));
        elemField.setXmlType(new javax.xml.namespace.QName("http://www.w3.org/2001/XMLSchema", "string"));
        typeDesc.addFieldDesc(elemField);
        elemField = new org.apache.axis.description.ElementDesc();
        elemField.setFieldName("request");
        elemField.setXmlName(new javax.xml.namespace.QName("http://exchangenetwork.net/schema/cdxPolicy.xsd", "Request"));
        elemField.setXmlType(new javax.xml.namespace.QName("http://www.w3.org/2001/XMLSchema", "string"));
        typeDesc.addFieldDesc(elemField);
        elemField = new org.apache.axis.description.ElementDesc();
        elemField.setFieldName("params");
        elemField.setXmlName(new javax.xml.namespace.QName("http://exchangenetwork.net/schema/cdxPolicy.xsd", "Params"));
        elemField.setXmlType(new javax.xml.namespace.QName("http://www.w3.org/2001/XMLSchema", "string"));
        typeDesc.addFieldDesc(elemField);
        elemField = new org.apache.axis.description.ElementDesc();
        elemField.setFieldName("action");
        elemField.setXmlName(new javax.xml.namespace.QName("http://exchangenetwork.net/schema/cdxPolicy.xsd", "Action"));
        elemField.setXmlType(new javax.xml.namespace.QName("http://www.w3.org/2001/XMLSchema", "string"));
        typeDesc.addFieldDesc(elemField);
    }

    /**
     * Return type metadata object
     */
    public static org.apache.axis.description.TypeDesc getTypeDesc() {
        return typeDesc;
    }

    /**
     * Get Custom Serializer
     */
    public static org.apache.axis.encoding.Serializer getSerializer(
           java.lang.String mechType,
           java.lang.Class _javaType,
           javax.xml.namespace.QName _xmlType) {
        return
          new  org.apache.axis.encoding.ser.BeanSerializer(
            _javaType, _xmlType, typeDesc);
    }

    /**
     * Get Custom Deserializer
     */
    public static org.apache.axis.encoding.Deserializer getDeserializer(
           java.lang.String mechType,
           java.lang.Class _javaType,
           javax.xml.namespace.QName _xmlType) {
        return
          new  org.apache.axis.encoding.ser.BeanDeserializer(
            _javaType, _xmlType, typeDesc);
    }

}
