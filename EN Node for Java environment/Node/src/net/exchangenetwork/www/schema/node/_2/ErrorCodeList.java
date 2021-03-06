
/**
 * ErrorCodeList.java
 *
 * This file was auto-generated from WSDL
 * by the Apache Axis2 version: 1.4.1  Built on : Aug 19, 2008 (10:13:44 LKT)
 */
            
                package net.exchangenetwork.www.schema.node._2;
            

            /**
            *  ErrorCodeList bean class
            */
        
        public  class ErrorCodeList
        implements org.apache.axis2.databinding.ADBBean{
        
                public static final javax.xml.namespace.QName MY_QNAME = new javax.xml.namespace.QName(
                "http://www.exchangenetwork.net/schema/node/2",
                "ErrorCodeList",
                "ns2");

            

        private static java.lang.String generatePrefix(java.lang.String namespace) {
            if(namespace.equals("http://www.exchangenetwork.net/schema/node/2")){
                return "ns2";
            }
            return org.apache.axis2.databinding.utils.BeanUtil.getUniquePrefix();
        }

        

                        /**
                        * field for ErrorCodeList
                        */

                        
                                    protected java.lang.String localErrorCodeList ;
                                
                            private static java.util.HashMap _table_ = new java.util.HashMap();

                            // Constructor
                            
                                protected ErrorCodeList(java.lang.String value, boolean isRegisterValue) {
                                    localErrorCodeList = value;
                                    if (isRegisterValue){
                                        
                                               _table_.put(localErrorCodeList, this);
                                           
                                    }

                                }
                            
                                    public static final java.lang.String _E_UnknownUser =
                                        org.apache.axis2.databinding.utils.ConverterUtil.convertToString("E_UnknownUser");
                                
                                    public static final java.lang.String _E_InvalidCredential =
                                        org.apache.axis2.databinding.utils.ConverterUtil.convertToString("E_InvalidCredential");
                                
                                    public static final java.lang.String _E_TransactionId =
                                        org.apache.axis2.databinding.utils.ConverterUtil.convertToString("E_TransactionId");
                                
                                    public static final java.lang.String _E_UnknownMethod =
                                        org.apache.axis2.databinding.utils.ConverterUtil.convertToString("E_UnknownMethod");
                                
                                    public static final java.lang.String _E_ServiceUnavailable =
                                        org.apache.axis2.databinding.utils.ConverterUtil.convertToString("E_ServiceUnavailable");
                                
                                    public static final java.lang.String _E_AccessDenied =
                                        org.apache.axis2.databinding.utils.ConverterUtil.convertToString("E_AccessDenied");
                                
                                    public static final java.lang.String _E_InvalidToken =
                                        org.apache.axis2.databinding.utils.ConverterUtil.convertToString("E_InvalidToken");
                                
                                    public static final java.lang.String _E_FileNotFound =
                                        org.apache.axis2.databinding.utils.ConverterUtil.convertToString("E_FileNotFound");
                                
                                    public static final java.lang.String _E_TokenExpired =
                                        org.apache.axis2.databinding.utils.ConverterUtil.convertToString("E_TokenExpired");
                                
                                    public static final java.lang.String _E_ValidationFailed =
                                        org.apache.axis2.databinding.utils.ConverterUtil.convertToString("E_ValidationFailed");
                                
                                    public static final java.lang.String _E_ServerBusy =
                                        org.apache.axis2.databinding.utils.ConverterUtil.convertToString("E_ServerBusy");
                                
                                    public static final java.lang.String _E_RowIdOutofRange =
                                        org.apache.axis2.databinding.utils.ConverterUtil.convertToString("E_RowIdOutofRange");
                                
                                    public static final java.lang.String _E_FeatureUnsupported =
                                        org.apache.axis2.databinding.utils.ConverterUtil.convertToString("E_FeatureUnsupported");
                                
                                    public static final java.lang.String _E_VersionMismatch =
                                        org.apache.axis2.databinding.utils.ConverterUtil.convertToString("E_VersionMismatch");
                                
                                    public static final java.lang.String _E_InvalidFileName =
                                        org.apache.axis2.databinding.utils.ConverterUtil.convertToString("E_InvalidFileName");
                                
                                    public static final java.lang.String _E_InvalidFileType =
                                        org.apache.axis2.databinding.utils.ConverterUtil.convertToString("E_InvalidFileType");
                                
                                    public static final java.lang.String _E_InvalidDataflow =
                                        org.apache.axis2.databinding.utils.ConverterUtil.convertToString("E_InvalidDataflow");
                                
                                    public static final java.lang.String _E_InvalidParameter =
                                        org.apache.axis2.databinding.utils.ConverterUtil.convertToString("E_InvalidParameter");
                                
                                    public static final java.lang.String _E_AuthMethod =
                                        org.apache.axis2.databinding.utils.ConverterUtil.convertToString("E_AuthMethod");
                                
                                    public static final java.lang.String _E_Unknown =
                                        org.apache.axis2.databinding.utils.ConverterUtil.convertToString("E_Unknown");
                                
                                    public static final java.lang.String _E_QueryReturnSetTooBig =
                                        org.apache.axis2.databinding.utils.ConverterUtil.convertToString("E_QueryReturnSetTooBig");
                                
                                    public static final java.lang.String _E_DBMSError =
                                        org.apache.axis2.databinding.utils.ConverterUtil.convertToString("E_DBMSError");
                                
                                    public static final java.lang.String _E_RecipientNotSupported =
                                        org.apache.axis2.databinding.utils.ConverterUtil.convertToString("E_RecipientNotSupported");
                                
                                    public static final java.lang.String _E_NotificationURINotSupported =
                                        org.apache.axis2.databinding.utils.ConverterUtil.convertToString("E_NotificationURINotSupported");
                                
                                public static final ErrorCodeList E_UnknownUser =
                                    new ErrorCodeList(_E_UnknownUser,true);
                            
                                public static final ErrorCodeList E_InvalidCredential =
                                    new ErrorCodeList(_E_InvalidCredential,true);
                            
                                public static final ErrorCodeList E_TransactionId =
                                    new ErrorCodeList(_E_TransactionId,true);
                            
                                public static final ErrorCodeList E_UnknownMethod =
                                    new ErrorCodeList(_E_UnknownMethod,true);
                            
                                public static final ErrorCodeList E_ServiceUnavailable =
                                    new ErrorCodeList(_E_ServiceUnavailable,true);
                            
                                public static final ErrorCodeList E_AccessDenied =
                                    new ErrorCodeList(_E_AccessDenied,true);
                            
                                public static final ErrorCodeList E_InvalidToken =
                                    new ErrorCodeList(_E_InvalidToken,true);
                            
                                public static final ErrorCodeList E_FileNotFound =
                                    new ErrorCodeList(_E_FileNotFound,true);
                            
                                public static final ErrorCodeList E_TokenExpired =
                                    new ErrorCodeList(_E_TokenExpired,true);
                            
                                public static final ErrorCodeList E_ValidationFailed =
                                    new ErrorCodeList(_E_ValidationFailed,true);
                            
                                public static final ErrorCodeList E_ServerBusy =
                                    new ErrorCodeList(_E_ServerBusy,true);
                            
                                public static final ErrorCodeList E_RowIdOutofRange =
                                    new ErrorCodeList(_E_RowIdOutofRange,true);
                            
                                public static final ErrorCodeList E_FeatureUnsupported =
                                    new ErrorCodeList(_E_FeatureUnsupported,true);
                            
                                public static final ErrorCodeList E_VersionMismatch =
                                    new ErrorCodeList(_E_VersionMismatch,true);
                            
                                public static final ErrorCodeList E_InvalidFileName =
                                    new ErrorCodeList(_E_InvalidFileName,true);
                            
                                public static final ErrorCodeList E_InvalidFileType =
                                    new ErrorCodeList(_E_InvalidFileType,true);
                            
                                public static final ErrorCodeList E_InvalidDataflow =
                                    new ErrorCodeList(_E_InvalidDataflow,true);
                            
                                public static final ErrorCodeList E_InvalidParameter =
                                    new ErrorCodeList(_E_InvalidParameter,true);
                            
                                public static final ErrorCodeList E_AuthMethod =
                                    new ErrorCodeList(_E_AuthMethod,true);
                            
                                public static final ErrorCodeList E_Unknown =
                                    new ErrorCodeList(_E_Unknown,true);
                            
                                public static final ErrorCodeList E_QueryReturnSetTooBig =
                                    new ErrorCodeList(_E_QueryReturnSetTooBig,true);
                            
                                public static final ErrorCodeList E_DBMSError =
                                    new ErrorCodeList(_E_DBMSError,true);
                            
                                public static final ErrorCodeList E_RecipientNotSupported =
                                    new ErrorCodeList(_E_RecipientNotSupported,true);
                            
                                public static final ErrorCodeList E_NotificationURINotSupported =
                                    new ErrorCodeList(_E_NotificationURINotSupported,true);
                            

                                public java.lang.String getValue() { return localErrorCodeList;}

                                public boolean equals(java.lang.Object obj) {return (obj == this);}
                                public int hashCode() { return toString().hashCode();}
                                public java.lang.String toString() {
                                
                                        return localErrorCodeList.toString();
                                    

                                }

                        

     /**
     * isReaderMTOMAware
     * @return true if the reader supports MTOM
     */
   public static boolean isReaderMTOMAware(javax.xml.stream.XMLStreamReader reader) {
        boolean isReaderMTOMAware = false;
        
        try{
          isReaderMTOMAware = java.lang.Boolean.TRUE.equals(reader.getProperty(org.apache.axiom.om.OMConstants.IS_DATA_HANDLERS_AWARE));
        }catch(java.lang.IllegalArgumentException e){
          isReaderMTOMAware = false;
        }
        return isReaderMTOMAware;
   }
     
     
        /**
        *
        * @param parentQName
        * @param factory
        * @return org.apache.axiom.om.OMElement
        */
       public org.apache.axiom.om.OMElement getOMElement (
               final javax.xml.namespace.QName parentQName,
               final org.apache.axiom.om.OMFactory factory) throws org.apache.axis2.databinding.ADBException{


        
                org.apache.axiom.om.OMDataSource dataSource =
                       new org.apache.axis2.databinding.ADBDataSource(this,MY_QNAME){

                 public void serialize(org.apache.axis2.databinding.utils.writer.MTOMAwareXMLStreamWriter xmlWriter) throws javax.xml.stream.XMLStreamException {
                       ErrorCodeList.this.serialize(MY_QNAME,factory,xmlWriter);
                 }
               };
               return new org.apache.axiom.om.impl.llom.OMSourcedElementImpl(
               MY_QNAME,factory,dataSource);
            
       }

         public void serialize(final javax.xml.namespace.QName parentQName,
                                       final org.apache.axiom.om.OMFactory factory,
                                       org.apache.axis2.databinding.utils.writer.MTOMAwareXMLStreamWriter xmlWriter)
                                throws javax.xml.stream.XMLStreamException, org.apache.axis2.databinding.ADBException{
                           serialize(parentQName,factory,xmlWriter,false);
         }

         public void serialize(final javax.xml.namespace.QName parentQName,
                               final org.apache.axiom.om.OMFactory factory,
                               org.apache.axis2.databinding.utils.writer.MTOMAwareXMLStreamWriter xmlWriter,
                               boolean serializeType)
            throws javax.xml.stream.XMLStreamException, org.apache.axis2.databinding.ADBException{
            
                
                //We can safely assume an element has only one type associated with it
                
                            java.lang.String namespace = parentQName.getNamespaceURI();
                            java.lang.String localName = parentQName.getLocalPart();
                        
                            if (! namespace.equals("")) {
                                java.lang.String prefix = xmlWriter.getPrefix(namespace);

                                if (prefix == null) {
                                    prefix = generatePrefix(namespace);

                                    xmlWriter.writeStartElement(prefix, localName, namespace);
                                    xmlWriter.writeNamespace(prefix, namespace);
                                    xmlWriter.setPrefix(prefix, namespace);

                                } else {
                                    xmlWriter.writeStartElement(namespace, localName);
                                }

                            } else {
                                xmlWriter.writeStartElement(localName);
                            }

                            // add the type details if this is used in a simple type
                               if (serializeType){
                                   java.lang.String namespacePrefix = registerPrefix(xmlWriter,"http://www.exchangenetwork.net/schema/node/2");
                                   if ((namespacePrefix != null) && (namespacePrefix.trim().length() > 0)){
                                       writeAttribute("xsi","http://www.w3.org/2001/XMLSchema-instance","type",
                                           namespacePrefix+":ErrorCodeList",
                                           xmlWriter);
                                   } else {
                                       writeAttribute("xsi","http://www.w3.org/2001/XMLSchema-instance","type",
                                           "ErrorCodeList",
                                           xmlWriter);
                                   }
                               }
                            
                                          if (localErrorCodeList==null){
                                            
                                                     throw new org.apache.axis2.databinding.ADBException("Value cannot be null !!");
                                                
                                         }else{
                                        
                                                       xmlWriter.writeCharacters(localErrorCodeList);
                                            
                                         }
                                    
                            xmlWriter.writeEndElement();

                    

        }

         /**
          * Util method to write an attribute with the ns prefix
          */
          private void writeAttribute(java.lang.String prefix,java.lang.String namespace,java.lang.String attName,
                                      java.lang.String attValue,javax.xml.stream.XMLStreamWriter xmlWriter) throws javax.xml.stream.XMLStreamException{
              if (xmlWriter.getPrefix(namespace) == null) {
                       xmlWriter.writeNamespace(prefix, namespace);
                       xmlWriter.setPrefix(prefix, namespace);

              }

              xmlWriter.writeAttribute(namespace,attName,attValue);

         }

        /**
          * Util method to write an attribute without the ns prefix
          */
          private void writeAttribute(java.lang.String namespace,java.lang.String attName,
                                      java.lang.String attValue,javax.xml.stream.XMLStreamWriter xmlWriter) throws javax.xml.stream.XMLStreamException{
                if (namespace.equals(""))
              {
                  xmlWriter.writeAttribute(attName,attValue);
              }
              else
              {
                  registerPrefix(xmlWriter, namespace);
                  xmlWriter.writeAttribute(namespace,attName,attValue);
              }
          }


           /**
             * Util method to write an attribute without the ns prefix
             */
            private void writeQNameAttribute(java.lang.String namespace, java.lang.String attName,
                                             javax.xml.namespace.QName qname, javax.xml.stream.XMLStreamWriter xmlWriter) throws javax.xml.stream.XMLStreamException {

                java.lang.String attributeNamespace = qname.getNamespaceURI();
                java.lang.String attributePrefix = xmlWriter.getPrefix(attributeNamespace);
                if (attributePrefix == null) {
                    attributePrefix = registerPrefix(xmlWriter, attributeNamespace);
                }
                java.lang.String attributeValue;
                if (attributePrefix.trim().length() > 0) {
                    attributeValue = attributePrefix + ":" + qname.getLocalPart();
                } else {
                    attributeValue = qname.getLocalPart();
                }

                if (namespace.equals("")) {
                    xmlWriter.writeAttribute(attName, attributeValue);
                } else {
                    registerPrefix(xmlWriter, namespace);
                    xmlWriter.writeAttribute(namespace, attName, attributeValue);
                }
            }
        /**
         *  method to handle Qnames
         */

        private void writeQName(javax.xml.namespace.QName qname,
                                javax.xml.stream.XMLStreamWriter xmlWriter) throws javax.xml.stream.XMLStreamException {
            java.lang.String namespaceURI = qname.getNamespaceURI();
            if (namespaceURI != null) {
                java.lang.String prefix = xmlWriter.getPrefix(namespaceURI);
                if (prefix == null) {
                    prefix = generatePrefix(namespaceURI);
                    xmlWriter.writeNamespace(prefix, namespaceURI);
                    xmlWriter.setPrefix(prefix,namespaceURI);
                }

                if (prefix.trim().length() > 0){
                    xmlWriter.writeCharacters(prefix + ":" + org.apache.axis2.databinding.utils.ConverterUtil.convertToString(qname));
                } else {
                    // i.e this is the default namespace
                    xmlWriter.writeCharacters(org.apache.axis2.databinding.utils.ConverterUtil.convertToString(qname));
                }

            } else {
                xmlWriter.writeCharacters(org.apache.axis2.databinding.utils.ConverterUtil.convertToString(qname));
            }
        }

        private void writeQNames(javax.xml.namespace.QName[] qnames,
                                 javax.xml.stream.XMLStreamWriter xmlWriter) throws javax.xml.stream.XMLStreamException {

            if (qnames != null) {
                // we have to store this data until last moment since it is not possible to write any
                // namespace data after writing the charactor data
                java.lang.StringBuffer stringToWrite = new java.lang.StringBuffer();
                java.lang.String namespaceURI = null;
                java.lang.String prefix = null;

                for (int i = 0; i < qnames.length; i++) {
                    if (i > 0) {
                        stringToWrite.append(" ");
                    }
                    namespaceURI = qnames[i].getNamespaceURI();
                    if (namespaceURI != null) {
                        prefix = xmlWriter.getPrefix(namespaceURI);
                        if ((prefix == null) || (prefix.length() == 0)) {
                            prefix = generatePrefix(namespaceURI);
                            xmlWriter.writeNamespace(prefix, namespaceURI);
                            xmlWriter.setPrefix(prefix,namespaceURI);
                        }

                        if (prefix.trim().length() > 0){
                            stringToWrite.append(prefix).append(":").append(org.apache.axis2.databinding.utils.ConverterUtil.convertToString(qnames[i]));
                        } else {
                            stringToWrite.append(org.apache.axis2.databinding.utils.ConverterUtil.convertToString(qnames[i]));
                        }
                    } else {
                        stringToWrite.append(org.apache.axis2.databinding.utils.ConverterUtil.convertToString(qnames[i]));
                    }
                }
                xmlWriter.writeCharacters(stringToWrite.toString());
            }

        }


         /**
         * Register a namespace prefix
         */
         private java.lang.String registerPrefix(javax.xml.stream.XMLStreamWriter xmlWriter, java.lang.String namespace) throws javax.xml.stream.XMLStreamException {
                java.lang.String prefix = xmlWriter.getPrefix(namespace);

                if (prefix == null) {
                    prefix = generatePrefix(namespace);

                    while (xmlWriter.getNamespaceContext().getNamespaceURI(prefix) != null) {
                        prefix = org.apache.axis2.databinding.utils.BeanUtil.getUniquePrefix();
                    }

                    xmlWriter.writeNamespace(prefix, namespace);
                    xmlWriter.setPrefix(prefix, namespace);
                }

                return prefix;
            }


  
        /**
        * databinding method to get an XML representation of this object
        *
        */
        public javax.xml.stream.XMLStreamReader getPullParser(javax.xml.namespace.QName qName)
                    throws org.apache.axis2.databinding.ADBException{


        
                
                //We can safely assume an element has only one type associated with it
                 return new org.apache.axis2.databinding.utils.reader.ADBXMLStreamReaderImpl(MY_QNAME,
                            new java.lang.Object[]{
                            org.apache.axis2.databinding.utils.reader.ADBXMLStreamReader.ELEMENT_TEXT,
                            org.apache.axis2.databinding.utils.ConverterUtil.convertToString(localErrorCodeList)
                            },
                            null);

        }

  

     /**
      *  Factory class that keeps the parse method
      */
    public static class Factory{

        
        
                public static ErrorCodeList fromValue(java.lang.String value)
                      throws java.lang.IllegalArgumentException {
                    ErrorCodeList enumeration = (ErrorCodeList)
                       
                               _table_.get(value);
                           

                    if (enumeration==null) throw new java.lang.IllegalArgumentException();
                    return enumeration;
                }
                public static ErrorCodeList fromString(java.lang.String value,java.lang.String namespaceURI)
                      throws java.lang.IllegalArgumentException {
                    try {
                       
                                       return fromValue(org.apache.axis2.databinding.utils.ConverterUtil.convertToString(value));
                                   

                    } catch (java.lang.Exception e) {
                        throw new java.lang.IllegalArgumentException();
                    }
                }

                public static ErrorCodeList fromString(javax.xml.stream.XMLStreamReader xmlStreamReader,
                                                                    java.lang.String content) {
                    if (content.indexOf(":") > -1){
                        java.lang.String prefix = content.substring(0,content.indexOf(":"));
                        java.lang.String namespaceUri = xmlStreamReader.getNamespaceContext().getNamespaceURI(prefix);
                        return ErrorCodeList.Factory.fromString(content,namespaceUri);
                    } else {
                       return ErrorCodeList.Factory.fromString(content,"");
                    }
                }
            

        /**
        * static method to create the object
        * Precondition:  If this object is an element, the current or next start element starts this object and any intervening reader events are ignorable
        *                If this object is not an element, it is a complex type and the reader is at the event just after the outer start element
        * Postcondition: If this object is an element, the reader is positioned at its end element
        *                If this object is a complex type, the reader is positioned at the end element of its outer element
        */
        public static ErrorCodeList parse(javax.xml.stream.XMLStreamReader reader) throws java.lang.Exception{
            ErrorCodeList object = null;
                // initialize a hash map to keep values
                java.util.Map attributeMap = new java.util.HashMap();
                java.util.List extraAttributeList = new java.util.ArrayList();
            

            int event;
            java.lang.String nillableValue = null;
            java.lang.String prefix ="";
            java.lang.String namespaceuri ="";
            try {
                
                while (!reader.isStartElement() && !reader.isEndElement())
                    reader.next();

                

                
                // Note all attributes that were handled. Used to differ normal attributes
                // from anyAttributes.
                java.util.Vector handledAttributes = new java.util.Vector();
                

                    
                while(!reader.isEndElement()) {
                    if (reader.isStartElement()  || reader.hasText()){
                
                                    java.lang.String content = reader.getElementText();
                                    
                                        if (content.indexOf(":") > 0) {
                                            // this seems to be a Qname so find the namespace and send
                                            prefix = content.substring(0, content.indexOf(":"));
                                            namespaceuri = reader.getNamespaceURI(prefix);
                                            object = ErrorCodeList.Factory.fromString(content,namespaceuri);
                                        } else {
                                            // this seems to be not a qname send and empty namespace incase of it is
                                            // check is done in fromString method
                                            object = ErrorCodeList.Factory.fromString(content,"");
                                        }
                                        
                                        
                             } else {
                                reader.next();
                             }  
                           }  // end of while loop
                        



            } catch (javax.xml.stream.XMLStreamException e) {
                throw new java.lang.Exception(e);
            }

            return object;
        }

        }//end of factory class

        

        }
           
          