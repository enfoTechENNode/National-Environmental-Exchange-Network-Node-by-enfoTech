/**
 * ActionType.java
 *
 * This file was auto-generated from WSDL
 * by the Apache Axis 1.2alpha Dec 01, 2003 (04:33:24 EST) WSDL2Java emitter.
 */

package Node.NAAS.Types.Policy;

public class ActionType implements java.io.Serializable {
    private java.lang.String _value_;
    private static java.util.HashMap _table_ = new java.util.HashMap();

    // Constructor
    protected ActionType(java.lang.String value) {
        _value_ = value;
        _table_.put(_value_,this);
    }

    public static final java.lang.String _Deny = "Deny";
    public static final java.lang.String _Permit = "Permit";
    public static final ActionType Deny = new ActionType(_Deny);
    public static final ActionType Permit = new ActionType(_Permit);
    public java.lang.String getValue() { return _value_;}
    public static ActionType fromValue(java.lang.String value)
          throws java.lang.IllegalArgumentException {
        ActionType enumeration = (ActionType)
            _table_.get(value);
        if (enumeration==null) throw new java.lang.IllegalArgumentException();
        return enumeration;
    }
    public static ActionType fromString(java.lang.String value)
          throws java.lang.IllegalArgumentException {
        return fromValue(value);
    }
    public boolean equals(java.lang.Object obj) {return (obj == this);}
    public int hashCode() { return toString().hashCode();}
    public java.lang.String toString() { return _value_;}
    public java.lang.Object readResolve() throws java.io.ObjectStreamException { return fromValue(_value_);}
    public static org.apache.axis.encoding.Serializer getSerializer(
           java.lang.String mechType,
           java.lang.Class _javaType,
           javax.xml.namespace.QName _xmlType) {
        return
          new org.apache.axis.encoding.ser.EnumSerializer(
            _javaType, _xmlType);
    }
    public static org.apache.axis.encoding.Deserializer getDeserializer(
           java.lang.String mechType,
           java.lang.Class _javaType,
           javax.xml.namespace.QName _xmlType) {
        return
          new org.apache.axis.encoding.ser.EnumDeserializer(
            _javaType, _xmlType);
    }
    // Type metadata
    private static org.apache.axis.description.TypeDesc typeDesc =
        new org.apache.axis.description.TypeDesc(ActionType.class);

    static {
        typeDesc.setXmlType(new javax.xml.namespace.QName("http://exchangenetwork.net/schema/cdxPolicy.xsd", "ActionType"));
    }
    /**
     * Return type metadata object
     */
    public static org.apache.axis.description.TypeDesc getTypeDesc() {
        return typeDesc;
    }

}
