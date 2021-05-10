
/**
 * NetworkNode2MessageReceiverInOut.java
 *
 * This file was auto-generated from WSDL
 * by the Apache Axis2 version: 1.4.1  Built on : Aug 19, 2008 (10:13:39 LKT)
 */
        package net.exchangenetwork.www.wsdl.node._2;

import java.util.Iterator;

import javax.xml.namespace.QName;
import javax.xml.stream.XMLStreamException;

import org.apache.axiom.om.OMAttribute;
import org.apache.axiom.om.OMElement;
import org.apache.axiom.om.impl.llom.OMNodeImpl;
import org.apache.axiom.om.impl.llom.util.AXIOMUtil;
import org.apache.axiom.soap.SOAPBody;

import Node.Phrase;

        /**
        *  NetworkNode2MessageReceiverInOut message receiver
        */

        public class NetworkNode2MessageReceiverInOut extends org.apache.axis2.receivers.AbstractInOutMessageReceiver{


        public void invokeBusinessLogic(org.apache.axis2.context.MessageContext msgContext, org.apache.axis2.context.MessageContext newMsgContext)
        throws org.apache.axis2.AxisFault{

        try {

        // get the implementation class for the Web Service
        Object obj = getTheImplementationObject(msgContext);

        NetworkNode2SkeletonInterface skel = (NetworkNode2SkeletonInterface)obj;
        //Out Envelop
        org.apache.axiom.soap.SOAPEnvelope envelope = null;
        //Find the axisOperation that has been set by the Dispatch phase.
        org.apache.axis2.description.AxisOperation op = msgContext.getOperationContext().getAxisOperation();
        if (op == null) {
        throw new org.apache.axis2.AxisFault("Operation is not located, if this is doclit style the SOAP-ACTION should specified via the SOAP Action to use the RawXMLProvider");
        }

        java.lang.String methodName;
        if((op.getName() != null) && ((methodName = org.apache.axis2.util.JavaUtils.xmlNameToJava(op.getName().getLocalPart())) != null)){

        

            if("Execute".equals(methodName)){
                
                net.exchangenetwork.www.schema.node._2.ExecuteResponse executeResponse21 = null;
	                        net.exchangenetwork.www.schema.node._2.Execute wrappedParam =
                                                             (net.exchangenetwork.www.schema.node._2.Execute)fromOM(
                                    msgContext.getEnvelope().getBody().getFirstElement(),
                                    net.exchangenetwork.www.schema.node._2.Execute.class,
                                    getEnvelopeNamespaces(msgContext.getEnvelope()));
                                                
                                               executeResponse21 =
                                                   
                                                   
                                                         skel.Execute(wrappedParam)
                                                    ;
                                            
                                        envelope = toEnvelope(getSOAPFactory(msgContext), executeResponse21, false);
                                    } else 

            if("Authenticate".equals(methodName)){
                
                net.exchangenetwork.www.schema.node._2.AuthenticateResponse authenticateResponse23 = null;
	                        net.exchangenetwork.www.schema.node._2.Authenticate wrappedParam =
                                                             (net.exchangenetwork.www.schema.node._2.Authenticate)fromOM(
                                    msgContext.getEnvelope().getBody().getFirstElement(),
                                    net.exchangenetwork.www.schema.node._2.Authenticate.class,
                                    getEnvelopeNamespaces(msgContext.getEnvelope()));
                                                
                                               authenticateResponse23 =
                                                   
                                                   
                                                         skel.Authenticate(wrappedParam)
                                                    ;
                                            
                                        envelope = toEnvelope(getSOAPFactory(msgContext), authenticateResponse23, false);
                                    } else 

            if("Download".equals(methodName)){
                
                net.exchangenetwork.www.schema.node._2.DownloadResponse downloadResponse25 = null;
                //Change soap body to add the necessary element changed by enfotech begin
                SOAPBody sb = msgContext.getEnvelope().getBody();
                OMElement downloadEls = sb.getFirstElement();
                Iterator docEls = downloadEls.getChildElements();
                OMElement documentName = null;
                OMElement documentFormat = null;
                OMElement documentContent = null;
                while(docEls.hasNext()){
                	OMElement doc = (OMElement)docEls.next();
                	if(doc.getLocalName().equalsIgnoreCase("documents")){
                		Iterator docElements = doc.getChildElements();
            			boolean hasDocumentName = false;
            			boolean hasDocumentFormat = false;
            			boolean hasDocumentContent = false;
            			OMElement docElement = null;
                		while(docElements.hasNext()){
                			docElement = (OMElement)docElements.next();                			
                        	if(docElement.getLocalName().equalsIgnoreCase("documentName")){
                        		hasDocumentName = true;
                        		documentName = docElement;
                        	}
                        	if(docElement.getLocalName().equalsIgnoreCase("documentFormat")){
                        		hasDocumentFormat = true;
                        		documentFormat = docElement;
                        	}
                        	if(docElement.getLocalName().equalsIgnoreCase("documentContent")){
                        		hasDocumentContent = true;
                        		documentContent = docElement;
                        	}
                		}
                    	if(!hasDocumentName){
                    		documentName = AXIOMUtil.stringToOM("<documentName/>");
                    		documentName.setNamespace(doc.getNamespace());
                    		doc.addChild(documentName);
                    	}
                    	if(!hasDocumentFormat && !hasDocumentContent){
                    		documentFormat = AXIOMUtil.stringToOM("<documentFormat>XML</documentFormat>");
                    		documentFormat.setNamespace(doc.getNamespace());
                    		doc.addChild(documentFormat);
                    	}else if(!hasDocumentFormat && hasDocumentContent){
                    		documentFormat = AXIOMUtil.stringToOM("<documentFormat>XML</documentFormat>");
                    		documentFormat.setNamespace(doc.getNamespace());
                    		documentContent.detach();
                    		doc.addChild(documentFormat);                    		
                    		doc.addChild(documentContent);                    		
                    	}
                    	if(!hasDocumentContent){
                    		documentContent = AXIOMUtil.stringToOM("<documentContent/>");
                    		documentContent.setNamespace(doc.getNamespace());
                    		doc.addChild(documentContent);
                    	}                		                			               			               		
                	}
                }
                //Change soap body to add the necessary element changed by enfotech end
	                        net.exchangenetwork.www.schema.node._2.Download wrappedParam =
                                                             (net.exchangenetwork.www.schema.node._2.Download)fromOM(
                                    msgContext.getEnvelope().getBody().getFirstElement(),
                                    net.exchangenetwork.www.schema.node._2.Download.class,
                                    getEnvelopeNamespaces(msgContext.getEnvelope()));
                                                
                                               downloadResponse25 =
                                                   
                                                   
                                                         skel.Download(wrappedParam)
                                                    ;
                                            
                                        envelope = toEnvelope(getSOAPFactory(msgContext), downloadResponse25, false);
                                    } else 

            if("GetStatus".equals(methodName)){
                
                net.exchangenetwork.www.schema.node._2.GetStatusResponse getStatusResponse27 = null;
	                        net.exchangenetwork.www.schema.node._2.GetStatus wrappedParam =
                                                             (net.exchangenetwork.www.schema.node._2.GetStatus)fromOM(
                                    msgContext.getEnvelope().getBody().getFirstElement(),
                                    net.exchangenetwork.www.schema.node._2.GetStatus.class,
                                    getEnvelopeNamespaces(msgContext.getEnvelope()));
                                                
                                               getStatusResponse27 =
                                                   
                                                   
                                                         skel.GetStatus(wrappedParam)
                                                    ;
                                            
                                        envelope = toEnvelope(getSOAPFactory(msgContext), getStatusResponse27, false);
                                    } else 

            if("NodePing".equals(methodName)){
                
                net.exchangenetwork.www.schema.node._2.NodePingResponse nodePingResponse29 = null;
	                        net.exchangenetwork.www.schema.node._2.NodePing wrappedParam =
                                                             (net.exchangenetwork.www.schema.node._2.NodePing)fromOM(
                                    msgContext.getEnvelope().getBody().getFirstElement(),
                                    net.exchangenetwork.www.schema.node._2.NodePing.class,
                                    getEnvelopeNamespaces(msgContext.getEnvelope()));
                                                
                                               nodePingResponse29 =
                                                   
                                                   
                                                         skel.NodePing(wrappedParam)
                                                    ;
                                            
                                        envelope = toEnvelope(getSOAPFactory(msgContext), nodePingResponse29, false);
                                    } else 

            if("GetServices".equals(methodName)){
                
                net.exchangenetwork.www.schema.node._2.GetServicesResponse getServicesResponse31 = null;
	                        net.exchangenetwork.www.schema.node._2.GetServices wrappedParam =
                                                             (net.exchangenetwork.www.schema.node._2.GetServices)fromOM(
                                    msgContext.getEnvelope().getBody().getFirstElement(),
                                    net.exchangenetwork.www.schema.node._2.GetServices.class,
                                    getEnvelopeNamespaces(msgContext.getEnvelope()));
                                                
                                               getServicesResponse31 =
                                                   
                                                   
                                                         skel.GetServices(wrappedParam)
                                                    ;
                                            
                                        envelope = toEnvelope(getSOAPFactory(msgContext), getServicesResponse31, false);
                                    } else 

            if("Submit".equals(methodName)){
                
                net.exchangenetwork.www.schema.node._2.SubmitResponse submitResponse33 = null;
	                        net.exchangenetwork.www.schema.node._2.Submit wrappedParam =
                                                             (net.exchangenetwork.www.schema.node._2.Submit)fromOM(
                                    msgContext.getEnvelope().getBody().getFirstElement(),
                                    net.exchangenetwork.www.schema.node._2.Submit.class,
                                    getEnvelopeNamespaces(msgContext.getEnvelope()));
                                                
                                               submitResponse33 =
                                                   
                                                   
                                                         skel.Submit(wrappedParam)
                                                    ;
                                            
                                        envelope = toEnvelope(getSOAPFactory(msgContext), submitResponse33, false);
                                    } else 

            if("Notify".equals(methodName)){
                
                net.exchangenetwork.www.schema.node._2.NotifyResponse notifyResponse35 = null;
	                        net.exchangenetwork.www.schema.node._2.Notify wrappedParam =
                                                             (net.exchangenetwork.www.schema.node._2.Notify)fromOM(
                                    msgContext.getEnvelope().getBody().getFirstElement(),
                                    net.exchangenetwork.www.schema.node._2.Notify.class,
                                    getEnvelopeNamespaces(msgContext.getEnvelope()));
                                                
                                               notifyResponse35 =
                                                   
                                                   
                                                         skel.Notify(wrappedParam)
                                                    ;
                                            
                                        envelope = toEnvelope(getSOAPFactory(msgContext), notifyResponse35, false);
                                    } else 

            if("Solicit".equals(methodName)){
                
                net.exchangenetwork.www.schema.node._2.SolicitResponse solicitResponse37 = null;
	                        net.exchangenetwork.www.schema.node._2.Solicit wrappedParam =
                                                             (net.exchangenetwork.www.schema.node._2.Solicit)fromOM(
                                    msgContext.getEnvelope().getBody().getFirstElement(),
                                    net.exchangenetwork.www.schema.node._2.Solicit.class,
                                    getEnvelopeNamespaces(msgContext.getEnvelope()));
                                                
                                               solicitResponse37 =
                                                   
                                                   
                                                         skel.Solicit(wrappedParam)
                                                    ;
                                            
                                        envelope = toEnvelope(getSOAPFactory(msgContext), solicitResponse37, false);
                                    } else 

            if("Query".equals(methodName)){
                
                net.exchangenetwork.www.schema.node._2.QueryResponse queryResponse39 = null;
	                        net.exchangenetwork.www.schema.node._2.Query wrappedParam =
                                                             (net.exchangenetwork.www.schema.node._2.Query)fromOM(
                                    msgContext.getEnvelope().getBody().getFirstElement(),
                                    net.exchangenetwork.www.schema.node._2.Query.class,
                                    getEnvelopeNamespaces(msgContext.getEnvelope()));
                                                
                                               queryResponse39 =
                                                   
                                                   
                                                         skel.Query(wrappedParam)
                                                    ;
                                            
                                        envelope = toEnvelope(getSOAPFactory(msgContext), queryResponse39, false);

                                        //Change soap body to remove the extra element changed by enfotech begin
                                        SOAPBody sb = envelope.getBody();
                                        OMElement queryResponseEl = sb.getFirstElement();
                                        String body = queryResponseEl.toStringWithConsume();
                                        body = body.replaceAll("<qResult>", "");
                                        body = body.replaceAll("</qResult>", "");
                                        Iterator it = sb.getChildrenWithLocalName("QueryResponse");
                                        int i = 0;
                                        while(it.hasNext()){
                                        	OMElement resp = (OMElement)it.next();
                                        	if(i==0){
                                        		resp.detach();
                                        	}
                                        	i++;
                                        }
                                        sb.addChild(AXIOMUtil.stringToOM(body));
                                        //Change soap body to remove the extra element changed by enfotech end
                                        
                                        
            } else {
              throw new java.lang.RuntimeException("method not found");
            }
        
            
        newMsgContext.setEnvelope(envelope);
        }
        } catch (NodeFaultMessage e) {

            msgContext.setProperty(org.apache.axis2.Constants.FAULT_NAME,"NodeFaultDetailType");
            org.apache.axis2.AxisFault f = createAxisFault(e);
            if (e.getFaultMessage() != null){
                f.setDetail(toOM(e.getFaultMessage(),false));
            }
            throw f;
            }
        
        catch (java.lang.Exception e) {
        throw org.apache.axis2.AxisFault.makeFault(e);
        }
        }
        
        //
            private  org.apache.axiom.om.OMElement  toOM(net.exchangenetwork.www.schema.node._2.Execute param, boolean optimizeContent)
            throws org.apache.axis2.AxisFault {

            
                        try{
                             return param.getOMElement(net.exchangenetwork.www.schema.node._2.Execute.MY_QNAME,
                                          org.apache.axiom.om.OMAbstractFactory.getOMFactory());
                        } catch(org.apache.axis2.databinding.ADBException e){
                            throw org.apache.axis2.AxisFault.makeFault(e);
                        }
                    

            }
        
            private  org.apache.axiom.om.OMElement  toOM(net.exchangenetwork.www.schema.node._2.ExecuteResponse param, boolean optimizeContent)
            throws org.apache.axis2.AxisFault {

            
                        try{
                             return param.getOMElement(net.exchangenetwork.www.schema.node._2.ExecuteResponse.MY_QNAME,
                                          org.apache.axiom.om.OMAbstractFactory.getOMFactory());
                        } catch(org.apache.axis2.databinding.ADBException e){
                            throw org.apache.axis2.AxisFault.makeFault(e);
                        }
                    

            }
        
            private  org.apache.axiom.om.OMElement  toOM(net.exchangenetwork.www.schema.node._2.NodeFaultDetailType param, boolean optimizeContent)
            throws org.apache.axis2.AxisFault {

            
                        try{
                             return param.getOMElement(net.exchangenetwork.www.schema.node._2.NodeFaultDetailType.MY_QNAME,
                                          org.apache.axiom.om.OMAbstractFactory.getOMFactory());
                        } catch(org.apache.axis2.databinding.ADBException e){
                            throw org.apache.axis2.AxisFault.makeFault(e);
                        }
                    

            }
        
            private  org.apache.axiom.om.OMElement  toOM(net.exchangenetwork.www.schema.node._2.Authenticate param, boolean optimizeContent)
            throws org.apache.axis2.AxisFault {

            
                        try{
                             return param.getOMElement(net.exchangenetwork.www.schema.node._2.Authenticate.MY_QNAME,
                                          org.apache.axiom.om.OMAbstractFactory.getOMFactory());
                        } catch(org.apache.axis2.databinding.ADBException e){
                            throw org.apache.axis2.AxisFault.makeFault(e);
                        }
                    

            }
        
            private  org.apache.axiom.om.OMElement  toOM(net.exchangenetwork.www.schema.node._2.AuthenticateResponse param, boolean optimizeContent)
            throws org.apache.axis2.AxisFault {

            
                        try{
                             return param.getOMElement(net.exchangenetwork.www.schema.node._2.AuthenticateResponse.MY_QNAME,
                                          org.apache.axiom.om.OMAbstractFactory.getOMFactory());
                        } catch(org.apache.axis2.databinding.ADBException e){
                            throw org.apache.axis2.AxisFault.makeFault(e);
                        }
                    

            }
        
            private  org.apache.axiom.om.OMElement  toOM(net.exchangenetwork.www.schema.node._2.Download param, boolean optimizeContent)
            throws org.apache.axis2.AxisFault {

            
                        try{
                             return param.getOMElement(net.exchangenetwork.www.schema.node._2.Download.MY_QNAME,
                                          org.apache.axiom.om.OMAbstractFactory.getOMFactory());
                        } catch(org.apache.axis2.databinding.ADBException e){
                            throw org.apache.axis2.AxisFault.makeFault(e);
                        }
                    

            }
        
            private  org.apache.axiom.om.OMElement  toOM(net.exchangenetwork.www.schema.node._2.DownloadResponse param, boolean optimizeContent)
            throws org.apache.axis2.AxisFault {

            
                        try{
                             return param.getOMElement(net.exchangenetwork.www.schema.node._2.DownloadResponse.MY_QNAME,
                                          org.apache.axiom.om.OMAbstractFactory.getOMFactory());
                        } catch(org.apache.axis2.databinding.ADBException e){
                            throw org.apache.axis2.AxisFault.makeFault(e);
                        }
                    

            }
        
            private  org.apache.axiom.om.OMElement  toOM(net.exchangenetwork.www.schema.node._2.GetStatus param, boolean optimizeContent)
            throws org.apache.axis2.AxisFault {

            
                        try{
                             return param.getOMElement(net.exchangenetwork.www.schema.node._2.GetStatus.MY_QNAME,
                                          org.apache.axiom.om.OMAbstractFactory.getOMFactory());
                        } catch(org.apache.axis2.databinding.ADBException e){
                            throw org.apache.axis2.AxisFault.makeFault(e);
                        }
                    

            }
        
            private  org.apache.axiom.om.OMElement  toOM(net.exchangenetwork.www.schema.node._2.GetStatusResponse param, boolean optimizeContent)
            throws org.apache.axis2.AxisFault {

            
                        try{
                             return param.getOMElement(net.exchangenetwork.www.schema.node._2.GetStatusResponse.MY_QNAME,
                                          org.apache.axiom.om.OMAbstractFactory.getOMFactory());
                        } catch(org.apache.axis2.databinding.ADBException e){
                            throw org.apache.axis2.AxisFault.makeFault(e);
                        }
                    

            }
        
            private  org.apache.axiom.om.OMElement  toOM(net.exchangenetwork.www.schema.node._2.NodePing param, boolean optimizeContent)
            throws org.apache.axis2.AxisFault {

            
                        try{
                             return param.getOMElement(net.exchangenetwork.www.schema.node._2.NodePing.MY_QNAME,
                                          org.apache.axiom.om.OMAbstractFactory.getOMFactory());
                        } catch(org.apache.axis2.databinding.ADBException e){
                            throw org.apache.axis2.AxisFault.makeFault(e);
                        }
                    

            }
        
            private  org.apache.axiom.om.OMElement  toOM(net.exchangenetwork.www.schema.node._2.NodePingResponse param, boolean optimizeContent)
            throws org.apache.axis2.AxisFault {

            
                        try{
                             return param.getOMElement(net.exchangenetwork.www.schema.node._2.NodePingResponse.MY_QNAME,
                                          org.apache.axiom.om.OMAbstractFactory.getOMFactory());
                        } catch(org.apache.axis2.databinding.ADBException e){
                            throw org.apache.axis2.AxisFault.makeFault(e);
                        }
                    

            }
        
            private  org.apache.axiom.om.OMElement  toOM(net.exchangenetwork.www.schema.node._2.GetServices param, boolean optimizeContent)
            throws org.apache.axis2.AxisFault {

            
                        try{
                             return param.getOMElement(net.exchangenetwork.www.schema.node._2.GetServices.MY_QNAME,
                                          org.apache.axiom.om.OMAbstractFactory.getOMFactory());
                        } catch(org.apache.axis2.databinding.ADBException e){
                            throw org.apache.axis2.AxisFault.makeFault(e);
                        }
                    

            }
        
            private  org.apache.axiom.om.OMElement  toOM(net.exchangenetwork.www.schema.node._2.GetServicesResponse param, boolean optimizeContent)
            throws org.apache.axis2.AxisFault {

            
                        try{
                             return param.getOMElement(net.exchangenetwork.www.schema.node._2.GetServicesResponse.MY_QNAME,
                                          org.apache.axiom.om.OMAbstractFactory.getOMFactory());
                        } catch(org.apache.axis2.databinding.ADBException e){
                            throw org.apache.axis2.AxisFault.makeFault(e);
                        }
                    

            }
        
            private  org.apache.axiom.om.OMElement  toOM(net.exchangenetwork.www.schema.node._2.Submit param, boolean optimizeContent)
            throws org.apache.axis2.AxisFault {

            
                        try{
                             return param.getOMElement(net.exchangenetwork.www.schema.node._2.Submit.MY_QNAME,
                                          org.apache.axiom.om.OMAbstractFactory.getOMFactory());
                        } catch(org.apache.axis2.databinding.ADBException e){
                            throw org.apache.axis2.AxisFault.makeFault(e);
                        }
                    

            }
        
            private  org.apache.axiom.om.OMElement  toOM(net.exchangenetwork.www.schema.node._2.SubmitResponse param, boolean optimizeContent)
            throws org.apache.axis2.AxisFault {

            
                        try{
                             return param.getOMElement(net.exchangenetwork.www.schema.node._2.SubmitResponse.MY_QNAME,
                                          org.apache.axiom.om.OMAbstractFactory.getOMFactory());
                        } catch(org.apache.axis2.databinding.ADBException e){
                            throw org.apache.axis2.AxisFault.makeFault(e);
                        }
                    

            }
        
            private  org.apache.axiom.om.OMElement  toOM(net.exchangenetwork.www.schema.node._2.Notify param, boolean optimizeContent)
            throws org.apache.axis2.AxisFault {

            
                        try{
                             return param.getOMElement(net.exchangenetwork.www.schema.node._2.Notify.MY_QNAME,
                                          org.apache.axiom.om.OMAbstractFactory.getOMFactory());
                        } catch(org.apache.axis2.databinding.ADBException e){
                            throw org.apache.axis2.AxisFault.makeFault(e);
                        }
                    

            }
        
            private  org.apache.axiom.om.OMElement  toOM(net.exchangenetwork.www.schema.node._2.NotifyResponse param, boolean optimizeContent)
            throws org.apache.axis2.AxisFault {

            
                        try{
                             return param.getOMElement(net.exchangenetwork.www.schema.node._2.NotifyResponse.MY_QNAME,
                                          org.apache.axiom.om.OMAbstractFactory.getOMFactory());
                        } catch(org.apache.axis2.databinding.ADBException e){
                            throw org.apache.axis2.AxisFault.makeFault(e);
                        }
                    

            }
        
            private  org.apache.axiom.om.OMElement  toOM(net.exchangenetwork.www.schema.node._2.Solicit param, boolean optimizeContent)
            throws org.apache.axis2.AxisFault {

            
                        try{
                             return param.getOMElement(net.exchangenetwork.www.schema.node._2.Solicit.MY_QNAME,
                                          org.apache.axiom.om.OMAbstractFactory.getOMFactory());
                        } catch(org.apache.axis2.databinding.ADBException e){
                            throw org.apache.axis2.AxisFault.makeFault(e);
                        }
                    

            }
        
            private  org.apache.axiom.om.OMElement  toOM(net.exchangenetwork.www.schema.node._2.SolicitResponse param, boolean optimizeContent)
            throws org.apache.axis2.AxisFault {

            
                        try{
                             return param.getOMElement(net.exchangenetwork.www.schema.node._2.SolicitResponse.MY_QNAME,
                                          org.apache.axiom.om.OMAbstractFactory.getOMFactory());
                        } catch(org.apache.axis2.databinding.ADBException e){
                            throw org.apache.axis2.AxisFault.makeFault(e);
                        }
                    

            }
        
            private  org.apache.axiom.om.OMElement  toOM(net.exchangenetwork.www.schema.node._2.Query param, boolean optimizeContent)
            throws org.apache.axis2.AxisFault {

            
                        try{
                             return param.getOMElement(net.exchangenetwork.www.schema.node._2.Query.MY_QNAME,
                                          org.apache.axiom.om.OMAbstractFactory.getOMFactory());
                        } catch(org.apache.axis2.databinding.ADBException e){
                            throw org.apache.axis2.AxisFault.makeFault(e);
                        }
                    

            }
        
            private  org.apache.axiom.om.OMElement  toOM(net.exchangenetwork.www.schema.node._2.QueryResponse param, boolean optimizeContent)
            throws org.apache.axis2.AxisFault {

            
                        try{
                             return param.getOMElement(net.exchangenetwork.www.schema.node._2.QueryResponse.MY_QNAME,
                                          org.apache.axiom.om.OMAbstractFactory.getOMFactory());
                        } catch(org.apache.axis2.databinding.ADBException e){
                            throw org.apache.axis2.AxisFault.makeFault(e);
                        }
                    

            }
        
                    private  org.apache.axiom.soap.SOAPEnvelope toEnvelope(org.apache.axiom.soap.SOAPFactory factory, net.exchangenetwork.www.schema.node._2.ExecuteResponse param, boolean optimizeContent)
                        throws org.apache.axis2.AxisFault{
                      try{
                          org.apache.axiom.soap.SOAPEnvelope emptyEnvelope = factory.getDefaultEnvelope();
                           
                                    emptyEnvelope.getBody().addChild(param.getOMElement(net.exchangenetwork.www.schema.node._2.ExecuteResponse.MY_QNAME,factory));
                                

                         return emptyEnvelope;
                    } catch(org.apache.axis2.databinding.ADBException e){
                        throw org.apache.axis2.AxisFault.makeFault(e);
                    }
                    }
                    
                         private net.exchangenetwork.www.schema.node._2.ExecuteResponse wrapExecute(){
                                net.exchangenetwork.www.schema.node._2.ExecuteResponse wrappedElement = new net.exchangenetwork.www.schema.node._2.ExecuteResponse();
                                return wrappedElement;
                         }
                    
                    private  org.apache.axiom.soap.SOAPEnvelope toEnvelope(org.apache.axiom.soap.SOAPFactory factory, net.exchangenetwork.www.schema.node._2.AuthenticateResponse param, boolean optimizeContent)
                        throws org.apache.axis2.AxisFault{
                      try{
                          org.apache.axiom.soap.SOAPEnvelope emptyEnvelope = factory.getDefaultEnvelope();
                           
                                    emptyEnvelope.getBody().addChild(param.getOMElement(net.exchangenetwork.www.schema.node._2.AuthenticateResponse.MY_QNAME,factory));
                                

                         return emptyEnvelope;
                    } catch(org.apache.axis2.databinding.ADBException e){
                        throw org.apache.axis2.AxisFault.makeFault(e);
                    }
                    }
                    
                         private net.exchangenetwork.www.schema.node._2.AuthenticateResponse wrapAuthenticate(){
                                net.exchangenetwork.www.schema.node._2.AuthenticateResponse wrappedElement = new net.exchangenetwork.www.schema.node._2.AuthenticateResponse();
                                return wrappedElement;
                         }
                    
                    private  org.apache.axiom.soap.SOAPEnvelope toEnvelope(org.apache.axiom.soap.SOAPFactory factory, net.exchangenetwork.www.schema.node._2.DownloadResponse param, boolean optimizeContent)
                        throws org.apache.axis2.AxisFault{
                      try{
                          org.apache.axiom.soap.SOAPEnvelope emptyEnvelope = factory.getDefaultEnvelope();
                           
                                    emptyEnvelope.getBody().addChild(param.getOMElement(net.exchangenetwork.www.schema.node._2.DownloadResponse.MY_QNAME,factory));
                                

                         return emptyEnvelope;
                    } catch(org.apache.axis2.databinding.ADBException e){
                        throw org.apache.axis2.AxisFault.makeFault(e);
                    }
                    }
                    
                         private net.exchangenetwork.www.schema.node._2.DownloadResponse wrapDownload(){
                                net.exchangenetwork.www.schema.node._2.DownloadResponse wrappedElement = new net.exchangenetwork.www.schema.node._2.DownloadResponse();
                                return wrappedElement;
                         }
                    
                    private  org.apache.axiom.soap.SOAPEnvelope toEnvelope(org.apache.axiom.soap.SOAPFactory factory, net.exchangenetwork.www.schema.node._2.GetStatusResponse param, boolean optimizeContent)
                        throws org.apache.axis2.AxisFault{
                      try{
                          org.apache.axiom.soap.SOAPEnvelope emptyEnvelope = factory.getDefaultEnvelope();
                           
                                    emptyEnvelope.getBody().addChild(param.getOMElement(net.exchangenetwork.www.schema.node._2.GetStatusResponse.MY_QNAME,factory));
                                

                         return emptyEnvelope;
                    } catch(org.apache.axis2.databinding.ADBException e){
                        throw org.apache.axis2.AxisFault.makeFault(e);
                    }
                    }
                    
                         private net.exchangenetwork.www.schema.node._2.GetStatusResponse wrapGetStatus(){
                                net.exchangenetwork.www.schema.node._2.GetStatusResponse wrappedElement = new net.exchangenetwork.www.schema.node._2.GetStatusResponse();
                                return wrappedElement;
                         }
                    
                    private  org.apache.axiom.soap.SOAPEnvelope toEnvelope(org.apache.axiom.soap.SOAPFactory factory, net.exchangenetwork.www.schema.node._2.NodePingResponse param, boolean optimizeContent)
                        throws org.apache.axis2.AxisFault{
                      try{
                          org.apache.axiom.soap.SOAPEnvelope emptyEnvelope = factory.getDefaultEnvelope();
                           
                                    emptyEnvelope.getBody().addChild(param.getOMElement(net.exchangenetwork.www.schema.node._2.NodePingResponse.MY_QNAME,factory));
                                

                         return emptyEnvelope;
                    } catch(org.apache.axis2.databinding.ADBException e){
                        throw org.apache.axis2.AxisFault.makeFault(e);
                    }
                    }
                    
                         private net.exchangenetwork.www.schema.node._2.NodePingResponse wrapNodePing(){
                                net.exchangenetwork.www.schema.node._2.NodePingResponse wrappedElement = new net.exchangenetwork.www.schema.node._2.NodePingResponse();
                                return wrappedElement;
                         }
                    
                    private  org.apache.axiom.soap.SOAPEnvelope toEnvelope(org.apache.axiom.soap.SOAPFactory factory, net.exchangenetwork.www.schema.node._2.GetServicesResponse param, boolean optimizeContent)
                        throws org.apache.axis2.AxisFault{
                      try{
                          org.apache.axiom.soap.SOAPEnvelope emptyEnvelope = factory.getDefaultEnvelope();
                           
                                    emptyEnvelope.getBody().addChild(param.getOMElement(net.exchangenetwork.www.schema.node._2.GetServicesResponse.MY_QNAME,factory));
                                

                         return emptyEnvelope;
                    } catch(org.apache.axis2.databinding.ADBException e){
                        throw org.apache.axis2.AxisFault.makeFault(e);
                    }
                    }
                    
                         private net.exchangenetwork.www.schema.node._2.GetServicesResponse wrapGetServices(){
                                net.exchangenetwork.www.schema.node._2.GetServicesResponse wrappedElement = new net.exchangenetwork.www.schema.node._2.GetServicesResponse();
                                return wrappedElement;
                         }
                    
                    private  org.apache.axiom.soap.SOAPEnvelope toEnvelope(org.apache.axiom.soap.SOAPFactory factory, net.exchangenetwork.www.schema.node._2.SubmitResponse param, boolean optimizeContent)
                        throws org.apache.axis2.AxisFault{
                      try{
                          org.apache.axiom.soap.SOAPEnvelope emptyEnvelope = factory.getDefaultEnvelope();
                           
                                    emptyEnvelope.getBody().addChild(param.getOMElement(net.exchangenetwork.www.schema.node._2.SubmitResponse.MY_QNAME,factory));
                                

                         return emptyEnvelope;
                    } catch(org.apache.axis2.databinding.ADBException e){
                        throw org.apache.axis2.AxisFault.makeFault(e);
                    }
                    }
                    
                         private net.exchangenetwork.www.schema.node._2.SubmitResponse wrapSubmit(){
                                net.exchangenetwork.www.schema.node._2.SubmitResponse wrappedElement = new net.exchangenetwork.www.schema.node._2.SubmitResponse();
                                return wrappedElement;
                         }
                    
                    private  org.apache.axiom.soap.SOAPEnvelope toEnvelope(org.apache.axiom.soap.SOAPFactory factory, net.exchangenetwork.www.schema.node._2.NotifyResponse param, boolean optimizeContent)
                        throws org.apache.axis2.AxisFault{
                      try{
                          org.apache.axiom.soap.SOAPEnvelope emptyEnvelope = factory.getDefaultEnvelope();
                           
                                    emptyEnvelope.getBody().addChild(param.getOMElement(net.exchangenetwork.www.schema.node._2.NotifyResponse.MY_QNAME,factory));
                                

                         return emptyEnvelope;
                    } catch(org.apache.axis2.databinding.ADBException e){
                        throw org.apache.axis2.AxisFault.makeFault(e);
                    }
                    }
                    
                         private net.exchangenetwork.www.schema.node._2.NotifyResponse wrapNotify(){
                                net.exchangenetwork.www.schema.node._2.NotifyResponse wrappedElement = new net.exchangenetwork.www.schema.node._2.NotifyResponse();
                                return wrappedElement;
                         }
                    
                    private  org.apache.axiom.soap.SOAPEnvelope toEnvelope(org.apache.axiom.soap.SOAPFactory factory, net.exchangenetwork.www.schema.node._2.SolicitResponse param, boolean optimizeContent)
                        throws org.apache.axis2.AxisFault{
                      try{
                          org.apache.axiom.soap.SOAPEnvelope emptyEnvelope = factory.getDefaultEnvelope();
                           
                                    emptyEnvelope.getBody().addChild(param.getOMElement(net.exchangenetwork.www.schema.node._2.SolicitResponse.MY_QNAME,factory));
                                

                         return emptyEnvelope;
                    } catch(org.apache.axis2.databinding.ADBException e){
                        throw org.apache.axis2.AxisFault.makeFault(e);
                    }
                    }
                    
                         private net.exchangenetwork.www.schema.node._2.SolicitResponse wrapSolicit(){
                                net.exchangenetwork.www.schema.node._2.SolicitResponse wrappedElement = new net.exchangenetwork.www.schema.node._2.SolicitResponse();
                                return wrappedElement;
                         }
                    
                    private  org.apache.axiom.soap.SOAPEnvelope toEnvelope(org.apache.axiom.soap.SOAPFactory factory, net.exchangenetwork.www.schema.node._2.QueryResponse param, boolean optimizeContent)
                        throws org.apache.axis2.AxisFault{
                      try{
                          org.apache.axiom.soap.SOAPEnvelope emptyEnvelope = factory.getDefaultEnvelope();
                           
                                    emptyEnvelope.getBody().addChild(param.getOMElement(net.exchangenetwork.www.schema.node._2.QueryResponse.MY_QNAME,factory));
                                

                         return emptyEnvelope;
                    } catch(org.apache.axis2.databinding.ADBException e){
                        throw org.apache.axis2.AxisFault.makeFault(e);
                    }
                    }
                    
                         private net.exchangenetwork.www.schema.node._2.QueryResponse wrapQuery(){
                                net.exchangenetwork.www.schema.node._2.QueryResponse wrappedElement = new net.exchangenetwork.www.schema.node._2.QueryResponse();
                                return wrappedElement;
                         }
                    


        /**
        *  get the default envelope
        */
        private org.apache.axiom.soap.SOAPEnvelope toEnvelope(org.apache.axiom.soap.SOAPFactory factory){
        return factory.getDefaultEnvelope();
        }


        private  java.lang.Object fromOM(
        org.apache.axiom.om.OMElement param,
        java.lang.Class type,
        java.util.Map extraNamespaces) throws org.apache.axis2.AxisFault{

        try {
        
                if (net.exchangenetwork.www.schema.node._2.Execute.class.equals(type)){
                
                           return net.exchangenetwork.www.schema.node._2.Execute.Factory.parse(param.getXMLStreamReaderWithoutCaching());
                    

                }
           
                if (net.exchangenetwork.www.schema.node._2.ExecuteResponse.class.equals(type)){
                
                           return net.exchangenetwork.www.schema.node._2.ExecuteResponse.Factory.parse(param.getXMLStreamReaderWithoutCaching());
                    

                }
           
                if (net.exchangenetwork.www.schema.node._2.NodeFaultDetailType.class.equals(type)){
                
                           return net.exchangenetwork.www.schema.node._2.NodeFaultDetailType.Factory.parse(param.getXMLStreamReaderWithoutCaching());
                    

                }
           
                if (net.exchangenetwork.www.schema.node._2.Authenticate.class.equals(type)){
                
                           return net.exchangenetwork.www.schema.node._2.Authenticate.Factory.parse(param.getXMLStreamReaderWithoutCaching());
                    

                }
           
                if (net.exchangenetwork.www.schema.node._2.AuthenticateResponse.class.equals(type)){
                
                           return net.exchangenetwork.www.schema.node._2.AuthenticateResponse.Factory.parse(param.getXMLStreamReaderWithoutCaching());
                    

                }
           
                if (net.exchangenetwork.www.schema.node._2.NodeFaultDetailType.class.equals(type)){
                
                           return net.exchangenetwork.www.schema.node._2.NodeFaultDetailType.Factory.parse(param.getXMLStreamReaderWithoutCaching());
                    

                }
           
                if (net.exchangenetwork.www.schema.node._2.Download.class.equals(type)){
                
                           return net.exchangenetwork.www.schema.node._2.Download.Factory.parse(param.getXMLStreamReaderWithoutCaching());
                    

                }
           
                if (net.exchangenetwork.www.schema.node._2.DownloadResponse.class.equals(type)){
                
                           return net.exchangenetwork.www.schema.node._2.DownloadResponse.Factory.parse(param.getXMLStreamReaderWithoutCaching());
                    

                }
           
                if (net.exchangenetwork.www.schema.node._2.NodeFaultDetailType.class.equals(type)){
                
                           return net.exchangenetwork.www.schema.node._2.NodeFaultDetailType.Factory.parse(param.getXMLStreamReaderWithoutCaching());
                    

                }
           
                if (net.exchangenetwork.www.schema.node._2.GetStatus.class.equals(type)){
                
                           return net.exchangenetwork.www.schema.node._2.GetStatus.Factory.parse(param.getXMLStreamReaderWithoutCaching());
                    

                }
           
                if (net.exchangenetwork.www.schema.node._2.GetStatusResponse.class.equals(type)){
                
                           return net.exchangenetwork.www.schema.node._2.GetStatusResponse.Factory.parse(param.getXMLStreamReaderWithoutCaching());
                    

                }
           
                if (net.exchangenetwork.www.schema.node._2.NodeFaultDetailType.class.equals(type)){
                
                           return net.exchangenetwork.www.schema.node._2.NodeFaultDetailType.Factory.parse(param.getXMLStreamReaderWithoutCaching());
                    

                }
           
                if (net.exchangenetwork.www.schema.node._2.NodePing.class.equals(type)){
                
                           return net.exchangenetwork.www.schema.node._2.NodePing.Factory.parse(param.getXMLStreamReaderWithoutCaching());
                    

                }
           
                if (net.exchangenetwork.www.schema.node._2.NodePingResponse.class.equals(type)){
                
                           return net.exchangenetwork.www.schema.node._2.NodePingResponse.Factory.parse(param.getXMLStreamReaderWithoutCaching());
                    

                }
           
                if (net.exchangenetwork.www.schema.node._2.NodeFaultDetailType.class.equals(type)){
                
                           return net.exchangenetwork.www.schema.node._2.NodeFaultDetailType.Factory.parse(param.getXMLStreamReaderWithoutCaching());
                    

                }
           
                if (net.exchangenetwork.www.schema.node._2.GetServices.class.equals(type)){
                
                           return net.exchangenetwork.www.schema.node._2.GetServices.Factory.parse(param.getXMLStreamReaderWithoutCaching());
                    

                }
           
                if (net.exchangenetwork.www.schema.node._2.GetServicesResponse.class.equals(type)){
                
                           return net.exchangenetwork.www.schema.node._2.GetServicesResponse.Factory.parse(param.getXMLStreamReaderWithoutCaching());
                    

                }
           
                if (net.exchangenetwork.www.schema.node._2.NodeFaultDetailType.class.equals(type)){
                
                           return net.exchangenetwork.www.schema.node._2.NodeFaultDetailType.Factory.parse(param.getXMLStreamReaderWithoutCaching());
                    

                }
           
                if (net.exchangenetwork.www.schema.node._2.Submit.class.equals(type)){
                
                           return net.exchangenetwork.www.schema.node._2.Submit.Factory.parse(param.getXMLStreamReaderWithoutCaching());
                    

                }
           
                if (net.exchangenetwork.www.schema.node._2.SubmitResponse.class.equals(type)){
                
                           return net.exchangenetwork.www.schema.node._2.SubmitResponse.Factory.parse(param.getXMLStreamReaderWithoutCaching());
                    

                }
           
                if (net.exchangenetwork.www.schema.node._2.NodeFaultDetailType.class.equals(type)){
                
                           return net.exchangenetwork.www.schema.node._2.NodeFaultDetailType.Factory.parse(param.getXMLStreamReaderWithoutCaching());
                    

                }
           
                if (net.exchangenetwork.www.schema.node._2.Notify.class.equals(type)){
                
                           return net.exchangenetwork.www.schema.node._2.Notify.Factory.parse(param.getXMLStreamReaderWithoutCaching());
                    

                }
           
                if (net.exchangenetwork.www.schema.node._2.NotifyResponse.class.equals(type)){
                
                           return net.exchangenetwork.www.schema.node._2.NotifyResponse.Factory.parse(param.getXMLStreamReaderWithoutCaching());
                    

                }
           
                if (net.exchangenetwork.www.schema.node._2.NodeFaultDetailType.class.equals(type)){
                
                           return net.exchangenetwork.www.schema.node._2.NodeFaultDetailType.Factory.parse(param.getXMLStreamReaderWithoutCaching());
                    

                }
           
                if (net.exchangenetwork.www.schema.node._2.Solicit.class.equals(type)){
                
                           return net.exchangenetwork.www.schema.node._2.Solicit.Factory.parse(param.getXMLStreamReaderWithoutCaching());
                    

                }
           
                if (net.exchangenetwork.www.schema.node._2.SolicitResponse.class.equals(type)){
                
                           return net.exchangenetwork.www.schema.node._2.SolicitResponse.Factory.parse(param.getXMLStreamReaderWithoutCaching());
                    

                }
           
                if (net.exchangenetwork.www.schema.node._2.NodeFaultDetailType.class.equals(type)){
                
                           return net.exchangenetwork.www.schema.node._2.NodeFaultDetailType.Factory.parse(param.getXMLStreamReaderWithoutCaching());
                    

                }
           
                if (net.exchangenetwork.www.schema.node._2.Query.class.equals(type)){
                
                           return net.exchangenetwork.www.schema.node._2.Query.Factory.parse(param.getXMLStreamReaderWithoutCaching());
                    

                }
           
                if (net.exchangenetwork.www.schema.node._2.QueryResponse.class.equals(type)){
                
                           return net.exchangenetwork.www.schema.node._2.QueryResponse.Factory.parse(param.getXMLStreamReaderWithoutCaching());
                    

                }
           
                if (net.exchangenetwork.www.schema.node._2.NodeFaultDetailType.class.equals(type)){
                
                           return net.exchangenetwork.www.schema.node._2.NodeFaultDetailType.Factory.parse(param.getXMLStreamReaderWithoutCaching());
                    

                }
           
        } catch (java.lang.Exception e) {
        throw org.apache.axis2.AxisFault.makeFault(e);
        }
           return null;
        }



    

        /**
        *  A utility method that copies the namepaces from the SOAPEnvelope
        */
        private java.util.Map getEnvelopeNamespaces(org.apache.axiom.soap.SOAPEnvelope env){
        java.util.Map returnMap = new java.util.HashMap();
        java.util.Iterator namespaceIterator = env.getAllDeclaredNamespaces();
        while (namespaceIterator.hasNext()) {
        org.apache.axiom.om.OMNamespace ns = (org.apache.axiom.om.OMNamespace) namespaceIterator.next();
        returnMap.put(ns.getPrefix(),ns.getNamespaceURI());
        }
        return returnMap;
        }

        private org.apache.axis2.AxisFault createAxisFault(java.lang.Exception e) {
        org.apache.axis2.AxisFault f;
        Throwable cause = e.getCause();
        if (cause != null) {
            f = new org.apache.axis2.AxisFault(e.getMessage(), cause);
        } else {
            f = new org.apache.axis2.AxisFault(e.getMessage());
        }

        return f;
    }

        }//end of class
    