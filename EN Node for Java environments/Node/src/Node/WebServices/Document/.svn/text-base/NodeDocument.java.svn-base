/**
 * NodeDocument.java
 *
 * This file was auto-generated from WSDL
 * by the Apache Axis WSDL2Java emitter.
 */

package Node.WebServices.Document;

public class NodeDocument  implements java.io.Serializable {
    private java.lang.String name;
    private java.lang.String type;
    //private byte[] content;

    //added by Maggie H.
    private Object content;

    /**
     * Constructor
     * @param 
     * @return 
     */
    public NodeDocument() {
        this.type = "other"; //set other as default
    }

    /**
     * getName
     * @param 
     * @return String
     */
    public java.lang.String getName() {
        return name;
    }

    /**
     * setName
     * @param name
     * @return 
     */
    public void setName(java.lang.String name) {
        this.name = name;
    }

    /**
     * getType
     * @param 
     * @return String
     */
    public java.lang.String getType() {
        return type;
    }

    /**
     * setType
     * @param type
     * @return 
     */
    public void setType(java.lang.String type) {
        this.type = type;
    }

    //updated by Maggie H.
    /**
     * getContent
     * @param 
     * @return Object
     */
    public Object getContent() {
        return content;
    }

    //updated by Maggie H.
    /**
     * setContent
     * @param content
     * @return 
     */
    public void setContent(Object content) {
        this.content = content;
    }

//<< Begin -- Copy from CDX DNC source code

    /**
   * This method allways return byte[] from our content. It's neccassary for BLOB storage.
   * @return
   */
    public byte[] obtainContentBytes()
    {
        return NodeDocumentContentConverter.convertToBytes(this);
    }

    /**
   * This method set Content of cdxDocument base on requested type. Content type could be
   * CONTENT_TYPE_BYTES = 0 or CONTENT_TYPE_ATTACHMENT = 1.
   * Before execute this method, cdxDocument type must be set.
   * @param data
   * @param type
   */
    public void putContent(byte[] data, int contentType)
    {
        if(contentType == NodeDocumentContentConverter.CONTENT_TYPE_BYTES)
        {
            this.content = data;
        }
        else if(contentType == NodeDocumentContentConverter.CONTENT_TYPE_ATTACHMENT)
        {
            if(this.type == null || this.type.trim().equals(""))
            {
                throw new IllegalStateException("Please, set cdxDocument Type before call this method!");
            }
            this.content = NodeDocumentContentConverter.convertToAttachment(data, type);
        }
        else
        {
            throw new IllegalArgumentException("Content Type = '" + contentType + "' is not supported!" +
                                     " Please, check NodeDocumentContentConverter for supported types.");
        }
    }

//>> End -- Copy from CDX DNC source code

    private java.lang.Object __equalsCalc = null;
    public synchronized boolean equals(java.lang.Object obj) {
        if (!(obj instanceof NodeDocument)) return false;
        NodeDocument other = (NodeDocument) obj;
        if (obj == null) return false;
        if (this == obj) return true;
        if (__equalsCalc != null) {
            return (__equalsCalc == obj);
        }
        __equalsCalc = obj;
        boolean _equals;
        _equals = true &&
            ((this.name==null && other.getName()==null) ||
             (this.name!=null &&
              this.name.equals(other.getName()))) &&
            ((this.type==null && other.getType()==null) ||
             (this.type!=null &&
              this.type.equals(other.getType()))) &&
            ((this.content==null && other.getContent()==null) ||
             (this.content!=null &&
              //updated by Maggie H.
              //java.util.Arrays.equals(this.content, other.getContent())));
              this.content.equals(other.getContent())));
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
        if (getName() != null) {
            _hashCode += getName().hashCode();
        }
        if (getType() != null) {
            _hashCode += getType().hashCode();
        }
        if (getContent() != null) {
            for (int i=0;
                 i<java.lang.reflect.Array.getLength(getContent());
                 i++) {
                java.lang.Object obj = java.lang.reflect.Array.get(getContent(), i);
                if (obj != null &&
                    !obj.getClass().isArray()) {
                    _hashCode += obj.hashCode();
                }
            }
        }
        __hashCodeCalc = false;
        return _hashCode;
    }

    // Type metadata
    private static org.apache.axis.description.TypeDesc typeDesc =
        new org.apache.axis.description.TypeDesc(NodeDocument.class);

    static {
        typeDesc.setXmlType(new javax.xml.namespace.QName("http://www.ExchangeNetwork.net/schema/v1.0/node.xsd", "NodeDocument"));
        org.apache.axis.description.ElementDesc elemField = new org.apache.axis.description.ElementDesc();
        elemField.setFieldName("name");
        elemField.setXmlName(new javax.xml.namespace.QName("http://www.ExchangeNetwork.net/schema/v1.0/node.xsd", "name"));
        elemField.setXmlType(new javax.xml.namespace.QName("http://www.w3.org/2001/XMLSchema", "string"));
        typeDesc.addFieldDesc(elemField);
        elemField = new org.apache.axis.description.ElementDesc();
        elemField.setFieldName("type");
        elemField.setXmlName(new javax.xml.namespace.QName("http://www.ExchangeNetwork.net/schema/v1.0/node.xsd", "type"));
        elemField.setXmlType(new javax.xml.namespace.QName("http://www.w3.org/2001/XMLSchema", "string"));
        typeDesc.addFieldDesc(elemField);
        elemField = new org.apache.axis.description.ElementDesc();
        elemField.setFieldName("content");
        elemField.setXmlName(new javax.xml.namespace.QName("http://www.ExchangeNetwork.net/schema/v1.0/node.xsd", "content"));
        elemField.setXmlType(new javax.xml.namespace.QName("http://www.w3.org/2001/XMLSchema", "base64Binary"));
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
