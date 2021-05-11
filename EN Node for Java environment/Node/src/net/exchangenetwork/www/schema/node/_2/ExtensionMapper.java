
/**
 * ExtensionMapper.java
 *
 * This file was auto-generated from WSDL
 * by the Apache Axis2 version: 1.4.1  Built on : Aug 19, 2008 (10:13:44 LKT)
 */

            package net.exchangenetwork.www.schema.node._2;
            /**
            *  ExtensionMapper class
            */
        
        public  class ExtensionMapper{

          public static java.lang.Object getTypeObject(java.lang.String namespaceURI,
                                                       java.lang.String typeName,
                                                       javax.xml.stream.XMLStreamReader reader) throws java.lang.Exception{

              
                  if (
                  "http://www.exchangenetwork.net/schema/node/2".equals(namespaceURI) &&
                  "ResultSetType".equals(typeName)){
                   
                            return  net.exchangenetwork.www.schema.node._2.ResultSetType.Factory.parse(reader);
                        

                  }

              
                  if (
                  "http://www.exchangenetwork.net/schema/node/2".equals(namespaceURI) &&
                  "NotificationTypeCode".equals(typeName)){
                   
                            return  net.exchangenetwork.www.schema.node._2.NotificationTypeCode.Factory.parse(reader);
                        

                  }

              
                  if (
                  "http://www.exchangenetwork.net/schema/node/2".equals(namespaceURI) &&
                  "NodeDocumentType".equals(typeName)){
                   
                            return  net.exchangenetwork.www.schema.node._2.NodeDocumentType.Factory.parse(reader);
                        

                  }

              
                  if (
                  "http://www.exchangenetwork.net/schema/node/2".equals(namespaceURI) &&
                  "GenericXmlType".equals(typeName)){
                   
                            return  net.exchangenetwork.www.schema.node._2.GenericXmlType.Factory.parse(reader);
                        

                  }

              
                  if (
                  "http://www.exchangenetwork.net/schema/node/2".equals(namespaceURI) &&
                  "EncodingType".equals(typeName)){
                   
                            return  net.exchangenetwork.www.schema.node._2.EncodingType.Factory.parse(reader);
                        

                  }

              
                  if (
                  "http://www.exchangenetwork.net/schema/node/2".equals(namespaceURI) &&
                  "NotificationMessageCategoryType".equals(typeName)){
                   
                            return  net.exchangenetwork.www.schema.node._2.NotificationMessageCategoryType.Factory.parse(reader);
                        

                  }

              
                  if (
                  "http://www.w3.org/2005/05/xmlmime".equals(namespaceURI) &&
                  "hexBinary".equals(typeName)){
                   
                            return  org.w3.www._2005._05.xmlmime.HexBinary.Factory.parse(reader);
                        

                  }

              
                  if (
                  "http://www.exchangenetwork.net/schema/node/2".equals(namespaceURI) &&
                  "NotificationURIType".equals(typeName)){
                   
                            return  net.exchangenetwork.www.schema.node._2.NotificationURIType.Factory.parse(reader);
                        

                  }

              
                  if (
                  "http://www.exchangenetwork.net/schema/node/2".equals(namespaceURI) &&
                  "StatusResponseType".equals(typeName)){
                   
                            return  net.exchangenetwork.www.schema.node._2.StatusResponseType.Factory.parse(reader);
                        

                  }

              
                  if (
                  "http://www.exchangenetwork.net/schema/node/2".equals(namespaceURI) &&
                  "AttachmentType".equals(typeName)){
                   
                            return  net.exchangenetwork.www.schema.node._2.AttachmentType.Factory.parse(reader);
                        

                  }

              
                  if (
                  "http://www.exchangenetwork.net/schema/node/2".equals(namespaceURI) &&
                  "PasswordType".equals(typeName)){
                   
                            return  net.exchangenetwork.www.schema.node._2.PasswordType.Factory.parse(reader);
                        

                  }

              
                  if (
                  "http://www.w3.org/2005/05/xmlmime".equals(namespaceURI) &&
                  "base64Binary".equals(typeName)){
                   
                            return  org.w3.www._2005._05.xmlmime.Base64Binary.Factory.parse(reader);
                        

                  }

              
                  if (
                  "http://www.w3.org/2005/05/xmlmime".equals(namespaceURI) &&
                  "contentType_type0".equals(typeName)){
                   
                            return  org.w3.www._2005._05.xmlmime.ContentType_type0.Factory.parse(reader);
                        

                  }

              
                  if (
                  "http://www.exchangenetwork.net/schema/node/2".equals(namespaceURI) &&
                  "TransactionStatusCode".equals(typeName)){
                   
                            return  net.exchangenetwork.www.schema.node._2.TransactionStatusCode.Factory.parse(reader);
                        

                  }

              
                  if (
                  "http://www.exchangenetwork.net/schema/node/2".equals(namespaceURI) &&
                  "NotificationMessageType".equals(typeName)){
                   
                            return  net.exchangenetwork.www.schema.node._2.NotificationMessageType.Factory.parse(reader);
                        

                  }

              
                  if (
                  "http://www.exchangenetwork.net/schema/node/2".equals(namespaceURI) &&
                  "ErrorCodeList".equals(typeName)){
                   
                            return  net.exchangenetwork.www.schema.node._2.ErrorCodeList.Factory.parse(reader);
                        

                  }

              
                  if (
                  "http://www.exchangenetwork.net/schema/node/2".equals(namespaceURI) &&
                  "NodeStatusCode".equals(typeName)){
                   
                            return  net.exchangenetwork.www.schema.node._2.NodeStatusCode.Factory.parse(reader);
                        

                  }

              
                  if (
                  "http://www.exchangenetwork.net/schema/node/2".equals(namespaceURI) &&
                  "ParameterType".equals(typeName)){
                   
                            return  net.exchangenetwork.www.schema.node._2.ParameterType.Factory.parse(reader);
                        

                  }

              
                  if (
                  "http://www.exchangenetwork.net/schema/node/2".equals(namespaceURI) &&
                  "allNNI".equals(typeName)){
                   
                            return  net.exchangenetwork.www.schema.node._2.AllNNI.Factory.parse(reader);
                        

                  }

              
                  if (
                  "http://www.exchangenetwork.net/schema/node/2".equals(namespaceURI) &&
                  "DocumentFormatType".equals(typeName)){
                   
                            return  net.exchangenetwork.www.schema.node._2.DocumentFormatType.Factory.parse(reader);
                        

                  }

              
             throw new org.apache.axis2.databinding.ADBException("Unsupported type " + namespaceURI + " " + typeName);
          }

        }
    