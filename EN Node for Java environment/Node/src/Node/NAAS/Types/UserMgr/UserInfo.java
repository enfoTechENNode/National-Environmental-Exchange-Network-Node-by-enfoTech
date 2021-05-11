/**
 * UserInfo.java
 *
 * This file was auto-generated from WSDL
 * by the Apache Axis 1.2alpha Dec 01, 2003 (04:33:24 EST) WSDL2Java emitter.
 */

package Node.NAAS.Types.UserMgr;

public class UserInfo  implements java.io.Serializable {
    private java.lang.String userId;
    private java.lang.String userGroup;
    private java.lang.String owner;
    private java.lang.String node;
    private java.lang.String affiliate;

    public UserInfo() {
    }


    /**
     * Gets the userId value for this UserInfo.
     *
     * @return userId
     */
    public java.lang.String getUserId() {
        return userId;
    }


    /**
     * Sets the userId value for this UserInfo.
     *
     * @param userId
     */
    public void setUserId(java.lang.String userId) {
        this.userId = userId;
    }


    /**
     * Gets the userGroup value for this UserInfo.
     *
     * @return userGroup
     */
    public java.lang.String getUserGroup() {
        return userGroup;
    }


    /**
     * Sets the userGroup value for this UserInfo.
     *
     * @param userGroup
     */
    public void setUserGroup(java.lang.String userGroup) {
        this.userGroup = userGroup;
    }


    /**
     * Gets the owner value for this UserInfo.
     *
     * @return owner
     */
    public java.lang.String getOwner() {
        return owner;
    }


    /**
     * Sets the owner value for this UserInfo.
     *
     * @param owner
     */
    public void setOwner(java.lang.String owner) {
        this.owner = owner;
    }


    /**
     * Gets the node value for this UserInfo.
     *
     * @return node
     */
    public java.lang.String getNode() {
        return node;
    }


    /**
     * Sets the node value for this UserInfo.
     *
     * @param node
     */
    public void setNode(java.lang.String node) {
        this.node = node;
    }


    /**
     * Gets the affiliate value for this UserInfo.
     *
     * @return affiliate
     */
    public java.lang.String getAffiliate() {
        return affiliate;
    }


    /**
     * Sets the affiliate value for this UserInfo.
     *
     * @param affiliate
     */
    public void setAffiliate(java.lang.String affiliate) {
        this.affiliate = affiliate;
    }

    private java.lang.Object __equalsCalc = null;
    public synchronized boolean equals(java.lang.Object obj) {
        if (!(obj instanceof UserInfo)) return false;
        UserInfo other = (UserInfo) obj;
        if (obj == null) return false;
        if (this == obj) return true;
        if (__equalsCalc != null) {
            return (__equalsCalc == obj);
        }
        __equalsCalc = obj;
        boolean _equals;
        _equals = true &&
            ((this.userId==null && other.getUserId()==null) ||
             (this.userId!=null &&
              this.userId.equals(other.getUserId()))) &&
            ((this.userGroup==null && other.getUserGroup()==null) ||
             (this.userGroup!=null &&
              this.userGroup.equals(other.getUserGroup()))) &&
            ((this.owner==null && other.getOwner()==null) ||
             (this.owner!=null &&
              this.owner.equals(other.getOwner()))) &&
            ((this.node==null && other.getNode()==null) ||
             (this.node!=null &&
              this.node.equals(other.getNode()))) &&
            ((this.affiliate==null && other.getAffiliate()==null) ||
             (this.affiliate!=null &&
              this.affiliate.equals(other.getAffiliate())));
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
        if (getUserId() != null) {
            _hashCode += getUserId().hashCode();
        }
        if (getUserGroup() != null) {
            _hashCode += getUserGroup().hashCode();
        }
        if (getOwner() != null) {
            _hashCode += getOwner().hashCode();
        }
        if (getNode() != null) {
            _hashCode += getNode().hashCode();
        }
        if (getAffiliate() != null) {
            _hashCode += getAffiliate().hashCode();
        }
        __hashCodeCalc = false;
        return _hashCode;
    }

    // Type metadata
    private static org.apache.axis.description.TypeDesc typeDesc =
        new org.apache.axis.description.TypeDesc(UserInfo.class);

    static {
        typeDesc.setXmlType(new javax.xml.namespace.QName("http://neien.org/schema/usermgr.xsd", "UserInfo"));
        org.apache.axis.description.ElementDesc elemField = new org.apache.axis.description.ElementDesc();
        elemField.setFieldName("userId");
        elemField.setXmlName(new javax.xml.namespace.QName("http://neien.org/schema/usermgr.xsd", "UserId"));
        elemField.setXmlType(new javax.xml.namespace.QName("http://www.w3.org/2001/XMLSchema", "string"));
        typeDesc.addFieldDesc(elemField);
        elemField = new org.apache.axis.description.ElementDesc();
        elemField.setFieldName("userGroup");
        elemField.setXmlName(new javax.xml.namespace.QName("http://neien.org/schema/usermgr.xsd", "UserGroup"));
        elemField.setXmlType(new javax.xml.namespace.QName("http://www.w3.org/2001/XMLSchema", "string"));
        typeDesc.addFieldDesc(elemField);
        elemField = new org.apache.axis.description.ElementDesc();
        elemField.setFieldName("owner");
        elemField.setXmlName(new javax.xml.namespace.QName("http://neien.org/schema/usermgr.xsd", "Owner"));
        elemField.setXmlType(new javax.xml.namespace.QName("http://www.w3.org/2001/XMLSchema", "string"));
        typeDesc.addFieldDesc(elemField);
        elemField = new org.apache.axis.description.ElementDesc();
        elemField.setFieldName("node");
        elemField.setXmlName(new javax.xml.namespace.QName("http://neien.org/schema/usermgr.xsd", "Node"));
        elemField.setXmlType(new javax.xml.namespace.QName("http://www.w3.org/2001/XMLSchema", "string"));
        typeDesc.addFieldDesc(elemField);
        elemField = new org.apache.axis.description.ElementDesc();
        elemField.setFieldName("affiliate");
        elemField.setXmlName(new javax.xml.namespace.QName("http://neien.org/schema/usermgr.xsd", "Affiliate"));
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
