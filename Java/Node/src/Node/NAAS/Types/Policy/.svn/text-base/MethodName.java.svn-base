/**
 * MethodName.java
 *
 * This file was auto-generated from WSDL
 * by the Apache Axis 1.2alpha Dec 01, 2003 (04:33:24 EST) WSDL2Java emitter.
 */

package Node.NAAS.Types.Policy;

public class MethodName implements java.io.Serializable {
    private java.lang.String _value_;
    private static java.util.HashMap _table_ = new java.util.HashMap();

    // Constructor
    protected MethodName(java.lang.String value) {
        _value_ = value;
        _table_.put(_value_,this);
    }

    public static final java.lang.String _Any = "Any";
    public static final java.lang.String _Submit = "Submit";
    public static final java.lang.String _Download = "Download";
    public static final java.lang.String _Authenticate = "Authenticate";
    public static final java.lang.String _Query = "Query";
    public static final java.lang.String _GetStatus = "GetStatus";
    public static final java.lang.String _Notify = "Notify";
    public static final java.lang.String _Solicit = "Solicit";
    public static final java.lang.String _GetServices = "GetServices";
    public static final java.lang.String _Execute = "Execute";
    public static final MethodName Any = new MethodName(_Any);
    public static final MethodName Submit = new MethodName(_Submit);
    public static final MethodName Download = new MethodName(_Download);
    public static final MethodName Authenticate = new MethodName(_Authenticate);
    public static final MethodName Query = new MethodName(_Query);
    public static final MethodName GetStatus = new MethodName(_GetStatus);
    public static final MethodName Notify = new MethodName(_Notify);
    public static final MethodName Solicit = new MethodName(_Solicit);
    public static final MethodName GetServices = new MethodName(_GetServices);
    public static final MethodName Execute = new MethodName(_Execute);
    public java.lang.String getValue() { return _value_;}
    public static MethodName fromValue(java.lang.String value)
          throws java.lang.IllegalArgumentException {
        MethodName enumeration = (MethodName)
            _table_.get(value);
        if (enumeration==null) throw new java.lang.IllegalArgumentException();
        return enumeration;
    }
    public static MethodName fromString(java.lang.String value)
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
        new org.apache.axis.description.TypeDesc(MethodName.class);

    static {
        typeDesc.setXmlType(new javax.xml.namespace.QName("http://exchangenetwork.net/schema/cdxPolicy.xsd", "MethodName"));
    }
    /**
     * Return type metadata object
     */
    public static org.apache.axis.description.TypeDesc getTypeDesc() {
        return typeDesc;
    }

}
