/**
 * AuthMethod.java
 *
 * This file was auto-generated from WSDL
 * by the Apache Axis WSDL2Java emitter.
 */

package Node.NAAS.Types.Auth;

public class AuthMethod implements java.io.Serializable {
    private java.lang.String _value_;
    private static java.util.HashMap _table_ = new java.util.HashMap();

    // Constructor
    protected AuthMethod(java.lang.String value) {
        _value_ = value;
        _table_.put(_value_,this);
    }

    // NAAS Resource Uri for Token Validation
    public static final java.lang.String NAASResourceUri = "http://epacdxnode.csc.com/xml/cdx_v10.wsdl";

    public static final java.lang.String _value1 = "password";
    public static final java.lang.String _value2 = "digest";
    public static final java.lang.String _value3 = "certificate";
    public static final java.lang.String _value4 = "SAML-password";
    public static final AuthMethod password = new AuthMethod(_value1);
    public static final AuthMethod digest = new AuthMethod(_value2);
    public static final AuthMethod certificate = new AuthMethod(_value3);
    public static final AuthMethod SAMLpassword = new AuthMethod(_value4);
    public java.lang.String getValue() { return _value_;}
    public static AuthMethod fromValue(java.lang.String value)
          throws java.lang.IllegalStateException {
        AuthMethod authMethod = (AuthMethod)
            _table_.get(value);
        if (authMethod==null) throw new java.lang.IllegalStateException();
        return authMethod;
    }
    /**
     * fromString
     * @param value
	 * @return AuthMethod
     */
    public static AuthMethod fromString(java.lang.String value)
          throws java.lang.IllegalStateException {
        return fromValue(value);
    }
    public boolean equals(java.lang.Object obj) {return (obj == this);}
    public int hashCode() { return toString().hashCode();}
    public java.lang.String toString() { return _value_;}
    public java.lang.Object readResolve() throws java.io.ObjectStreamException { return fromValue(_value_);}
}
