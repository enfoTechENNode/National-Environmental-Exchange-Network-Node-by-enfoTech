/**
 * NetworkPolicyBindingStub.java
 *
 * This file was auto-generated from WSDL
 * by the Apache Axis 1.2alpha Dec 01, 2003 (04:33:24 EST) WSDL2Java emitter.
 */

package Node.NAAS.Requestor.Policy;

public class NetworkPolicyBindingStub extends org.apache.axis.client.Stub implements Node.NAAS.Interfaces.Policy.NetworkPolicyPortType {
    private java.util.Vector cachedSerClasses = new java.util.Vector();
    private java.util.Vector cachedSerQNames = new java.util.Vector();
    private java.util.Vector cachedSerFactories = new java.util.Vector();
    private java.util.Vector cachedDeserFactories = new java.util.Vector();

    static org.apache.axis.description.OperationDesc [] _operations;

    static {
        _operations = new org.apache.axis.description.OperationDesc[5];
        _initOperationDesc1();
    }

	  /**
	   * _initOperationDesc1
	   * @param 
	   * @return 
	   */
    private static void _initOperationDesc1(){
        org.apache.axis.description.OperationDesc oper;
        oper = new org.apache.axis.description.OperationDesc();
        oper.setName("SetPolicy");
        oper.addParameter(new javax.xml.namespace.QName("", "userId"), new javax.xml.namespace.QName("http://www.w3.org/2001/XMLSchema", "string"), java.lang.String.class, org.apache.axis.description.ParameterDesc.IN, false, false);
        oper.addParameter(new javax.xml.namespace.QName("", "credential"), new javax.xml.namespace.QName("http://exchangenetwork.net/schema/cdxPolicy.xsd", "PasswordType"), Node.NAAS.Types.Policy.PasswordType.class, org.apache.axis.description.ParameterDesc.IN, false, false);
        oper.addParameter(new javax.xml.namespace.QName("", "subject"), new javax.xml.namespace.QName("http://www.w3.org/2001/XMLSchema", "string"), java.lang.String.class, org.apache.axis.description.ParameterDesc.IN, false, false);
        oper.addParameter(new javax.xml.namespace.QName("", "method"), new javax.xml.namespace.QName("http://exchangenetwork.net/schema/cdxPolicy.xsd", "MethodName"), Node.NAAS.Types.Policy.MethodName.class, org.apache.axis.description.ParameterDesc.IN, false, false);
        oper.addParameter(new javax.xml.namespace.QName("", "request"), new javax.xml.namespace.QName("http://www.w3.org/2001/XMLSchema", "string"), java.lang.String.class, org.apache.axis.description.ParameterDesc.IN, false, false);
        oper.addParameter(new javax.xml.namespace.QName("", "params"), new javax.xml.namespace.QName("http://www.w3.org/2001/XMLSchema", "string"), java.lang.String.class, org.apache.axis.description.ParameterDesc.IN, false, false);
        oper.addParameter(new javax.xml.namespace.QName("", "decision"), new javax.xml.namespace.QName("http://exchangenetwork.net/schema/cdxPolicy.xsd", "ActionType"), Node.NAAS.Types.Policy.ActionType.class, org.apache.axis.description.ParameterDesc.IN, false, false);
        oper.setReturnType(new javax.xml.namespace.QName("http://www.w3.org/2001/XMLSchema", "string"));
        oper.setReturnClass(java.lang.String.class);
        oper.setReturnQName(new javax.xml.namespace.QName("", "return"));
//        oper.setStyle(org.apache.axis.enum.Style.RPC);
//        oper.setUse(org.apache.axis.enum.Use.ENCODED);
        oper.setStyle(org.apache.axis.constants.Style.RPC);
        oper.setUse(org.apache.axis.constants.Use.ENCODED);
        _operations[0] = oper;

        oper = new org.apache.axis.description.OperationDesc();
        oper.setName("DeletePolicy");
        oper.addParameter(new javax.xml.namespace.QName("", "userId"), new javax.xml.namespace.QName("http://www.w3.org/2001/XMLSchema", "string"), java.lang.String.class, org.apache.axis.description.ParameterDesc.IN, false, false);
        oper.addParameter(new javax.xml.namespace.QName("", "credential"), new javax.xml.namespace.QName("http://exchangenetwork.net/schema/cdxPolicy.xsd", "PasswordType"), Node.NAAS.Types.Policy.PasswordType.class, org.apache.axis.description.ParameterDesc.IN, false, false);
        oper.addParameter(new javax.xml.namespace.QName("", "subject"), new javax.xml.namespace.QName("http://www.w3.org/2001/XMLSchema", "string"), java.lang.String.class, org.apache.axis.description.ParameterDesc.IN, false, false);
        oper.addParameter(new javax.xml.namespace.QName("", "method"), new javax.xml.namespace.QName("http://exchangenetwork.net/schema/cdxPolicy.xsd", "MethodName"), Node.NAAS.Types.Policy.MethodName.class, org.apache.axis.description.ParameterDesc.IN, false, false);
        oper.addParameter(new javax.xml.namespace.QName("", "request"), new javax.xml.namespace.QName("http://www.w3.org/2001/XMLSchema", "string"), java.lang.String.class, org.apache.axis.description.ParameterDesc.IN, false, false);
        oper.addParameter(new javax.xml.namespace.QName("", "params"), new javax.xml.namespace.QName("http://www.w3.org/2001/XMLSchema", "string"), java.lang.String.class, org.apache.axis.description.ParameterDesc.IN, false, false);
        oper.setReturnType(new javax.xml.namespace.QName("http://www.w3.org/2001/XMLSchema", "string"));
        oper.setReturnClass(java.lang.String.class);
        oper.setReturnQName(new javax.xml.namespace.QName("", "return"));
//        oper.setStyle(org.apache.axis.enum.Style.RPC);
//        oper.setUse(org.apache.axis.enum.Use.ENCODED);
        oper.setStyle(org.apache.axis.constants.Style.RPC);
        oper.setUse(org.apache.axis.constants.Use.ENCODED);
        _operations[1] = oper;

        oper = new org.apache.axis.description.OperationDesc();
        oper.setName("GetPolicyList");
        oper.addParameter(new javax.xml.namespace.QName("", "userId"), new javax.xml.namespace.QName("http://www.w3.org/2001/XMLSchema", "string"), java.lang.String.class, org.apache.axis.description.ParameterDesc.IN, false, false);
        oper.addParameter(new javax.xml.namespace.QName("", "credential"), new javax.xml.namespace.QName("http://exchangenetwork.net/schema/cdxPolicy.xsd", "PasswordType"), Node.NAAS.Types.Policy.PasswordType.class, org.apache.axis.description.ParameterDesc.IN, false, false);
        oper.addParameter(new javax.xml.namespace.QName("", "subject"), new javax.xml.namespace.QName("http://www.w3.org/2001/XMLSchema", "string"), java.lang.String.class, org.apache.axis.description.ParameterDesc.IN, false, false);
        oper.addParameter(new javax.xml.namespace.QName("", "rowId"), new javax.xml.namespace.QName("http://www.w3.org/2001/XMLSchema", "integer"), java.math.BigInteger.class, org.apache.axis.description.ParameterDesc.IN, false, false);
        oper.addParameter(new javax.xml.namespace.QName("", "maxRows"), new javax.xml.namespace.QName("http://www.w3.org/2001/XMLSchema", "integer"), java.math.BigInteger.class, org.apache.axis.description.ParameterDesc.IN, false, false);
        oper.setReturnType(new javax.xml.namespace.QName("http://exchangenetwork.net/schema/cdxPolicy.xsd", "PolicyList"));
        oper.setReturnClass(Node.NAAS.Types.Policy.PolicyInfo[].class);
        oper.setReturnQName(new javax.xml.namespace.QName("", "return"));
//        oper.setStyle(org.apache.axis.enum.Style.RPC);
//        oper.setUse(org.apache.axis.enum.Use.ENCODED);
        oper.setStyle(org.apache.axis.constants.Style.RPC);
        oper.setUse(org.apache.axis.constants.Use.ENCODED);
        _operations[2] = oper;

        oper = new org.apache.axis.description.OperationDesc();
        oper.setName("GetAuthEvents");
        oper.addParameter(new javax.xml.namespace.QName("", "userId"), new javax.xml.namespace.QName("http://www.w3.org/2001/XMLSchema", "string"), java.lang.String.class, org.apache.axis.description.ParameterDesc.IN, false, false);
        oper.addParameter(new javax.xml.namespace.QName("", "credential"), new javax.xml.namespace.QName("http://exchangenetwork.net/schema/cdxPolicy.xsd", "PasswordType"), Node.NAAS.Types.Policy.PasswordType.class, org.apache.axis.description.ParameterDesc.IN, false, false);
        oper.addParameter(new javax.xml.namespace.QName("", "subject"), new javax.xml.namespace.QName("http://www.w3.org/2001/XMLSchema", "string"), java.lang.String.class, org.apache.axis.description.ParameterDesc.IN, false, false);
        oper.addParameter(new javax.xml.namespace.QName("", "rowId"), new javax.xml.namespace.QName("http://www.w3.org/2001/XMLSchema", "integer"), java.math.BigInteger.class, org.apache.axis.description.ParameterDesc.IN, false, false);
        oper.addParameter(new javax.xml.namespace.QName("", "maxRows"), new javax.xml.namespace.QName("http://www.w3.org/2001/XMLSchema", "integer"), java.math.BigInteger.class, org.apache.axis.description.ParameterDesc.IN, false, false);
        oper.setReturnType(new javax.xml.namespace.QName("http://exchangenetwork.net/schema/cdxPolicy.xsd", "AuthEventList"));
        oper.setReturnClass(Node.NAAS.Types.Policy.AuthEventType[].class);
        oper.setReturnQName(new javax.xml.namespace.QName("", "return"));
//        oper.setStyle(org.apache.axis.enum.Style.RPC);
//        oper.setUse(org.apache.axis.enum.Use.ENCODED);
        oper.setStyle(org.apache.axis.constants.Style.RPC);
        oper.setUse(org.apache.axis.constants.Use.ENCODED);
        _operations[3] = oper;

        oper = new org.apache.axis.description.OperationDesc();
        oper.setName("VerifyPolicy");
        oper.addParameter(new javax.xml.namespace.QName("", "subject"), new javax.xml.namespace.QName("http://www.w3.org/2001/XMLSchema", "string"), java.lang.String.class, org.apache.axis.description.ParameterDesc.IN, false, false);
        oper.addParameter(new javax.xml.namespace.QName("", "node"), new javax.xml.namespace.QName("http://exchangenetwork.net/schema/cdxPolicy.xsd", "NodeId"), Node.NAAS.Types.Policy.NodeId.class, org.apache.axis.description.ParameterDesc.IN, false, false);
        oper.addParameter(new javax.xml.namespace.QName("", "method"), new javax.xml.namespace.QName("http://exchangenetwork.net/schema/cdxPolicy.xsd", "MethodName"), Node.NAAS.Types.Policy.MethodName.class, org.apache.axis.description.ParameterDesc.IN, false, false);
        oper.addParameter(new javax.xml.namespace.QName("", "request"), new javax.xml.namespace.QName("http://www.w3.org/2001/XMLSchema", "string"), java.lang.String.class, org.apache.axis.description.ParameterDesc.IN, false, false);
        oper.addParameter(new javax.xml.namespace.QName("", "params"), new javax.xml.namespace.QName("http://www.w3.org/2001/XMLSchema", "string"), java.lang.String.class, org.apache.axis.description.ParameterDesc.IN, false, false);
        oper.setReturnType(new javax.xml.namespace.QName("http://www.w3.org/2001/XMLSchema", "string"));
        oper.setReturnClass(java.lang.String.class);
        oper.setReturnQName(new javax.xml.namespace.QName("", "return"));
//        oper.setStyle(org.apache.axis.enum.Style.RPC);
//        oper.setUse(org.apache.axis.enum.Use.ENCODED);
        oper.setStyle(org.apache.axis.constants.Style.RPC);
        oper.setUse(org.apache.axis.constants.Use.ENCODED);
        _operations[4] = oper;
    }

	  /**
	   * Constructor
	   * @param 
	   * @return 
	   */
    public NetworkPolicyBindingStub() throws org.apache.axis.AxisFault {
         this(null);
    }

	  /**
	   * Constructor
	   * @param endpointURL
	   * @param service
	   * @return 
	   */
    public NetworkPolicyBindingStub(java.net.URL endpointURL, javax.xml.rpc.Service service) throws org.apache.axis.AxisFault {
         this(service);
         super.cachedEndpoint = endpointURL;
    }

	  /**
	   * Constructor
	   * @param service
	   * @return 
	   */
    public NetworkPolicyBindingStub(javax.xml.rpc.Service service) throws org.apache.axis.AxisFault {
        if (service == null) {
            super.service = new org.apache.axis.client.Service();
        } else {
            super.service = service;
          }
            java.lang.Class cls;
            javax.xml.namespace.QName qName;
            java.lang.Class beansf = org.apache.axis.encoding.ser.BeanSerializerFactory.class;
            java.lang.Class beandf = org.apache.axis.encoding.ser.BeanDeserializerFactory.class;
            java.lang.Class enumsf = org.apache.axis.encoding.ser.EnumSerializerFactory.class;
            java.lang.Class enumdf = org.apache.axis.encoding.ser.EnumDeserializerFactory.class;
            java.lang.Class arraysf = org.apache.axis.encoding.ser.ArraySerializerFactory.class;
            java.lang.Class arraydf = org.apache.axis.encoding.ser.ArrayDeserializerFactory.class;
            java.lang.Class simplesf = org.apache.axis.encoding.ser.SimpleSerializerFactory.class;
            java.lang.Class simpledf = org.apache.axis.encoding.ser.SimpleDeserializerFactory.class;
            qName = new javax.xml.namespace.QName("http://exchangenetwork.net/schema/cdxPolicy.xsd", "MethodName");
            cachedSerQNames.add(qName);
            cls = Node.NAAS.Types.Policy.MethodName.class;
            cachedSerClasses.add(cls);
            cachedSerFactories.add(enumsf);
            cachedDeserFactories.add(enumdf);

            qName = new javax.xml.namespace.QName("http://exchangenetwork.net/schema/cdxPolicy.xsd", "AuthEventList");
            cachedSerQNames.add(qName);
            cls = Node.NAAS.Types.Policy.AuthEventType[].class;
            cachedSerClasses.add(cls);
            cachedSerFactories.add(arraysf);
            cachedDeserFactories.add(arraydf);

            qName = new javax.xml.namespace.QName("http://exchangenetwork.net/schema/cdxPolicy.xsd", "AuthEventType");
            cachedSerQNames.add(qName);
            cls = Node.NAAS.Types.Policy.AuthEventType.class;
            cachedSerClasses.add(cls);
            cachedSerFactories.add(beansf);
            cachedDeserFactories.add(beandf);

            qName = new javax.xml.namespace.QName("http://exchangenetwork.net/schema/cdxPolicy.xsd", "PolicyInfo");
            cachedSerQNames.add(qName);
            cls = Node.NAAS.Types.Policy.PolicyInfo.class;
            cachedSerClasses.add(cls);
            cachedSerFactories.add(beansf);
            cachedDeserFactories.add(beandf);

            qName = new javax.xml.namespace.QName("http://exchangenetwork.net/schema/cdxPolicy.xsd", "PasswordType");
            cachedSerQNames.add(qName);
            cls = Node.NAAS.Types.Policy.PasswordType.class;
            cachedSerClasses.add(cls);
            cachedSerFactories.add(simplesf);
            cachedDeserFactories.add(simpledf);

            qName = new javax.xml.namespace.QName("http://exchangenetwork.net/schema/cdxPolicy.xsd", "NodeId");
            cachedSerQNames.add(qName);
            cls = Node.NAAS.Types.Policy.NodeId.class;
            cachedSerClasses.add(cls);
            cachedSerFactories.add(enumsf);
            cachedDeserFactories.add(enumdf);

            qName = new javax.xml.namespace.QName("http://exchangenetwork.net/schema/cdxPolicy.xsd", "ActionType");
            cachedSerQNames.add(qName);
            cls = Node.NAAS.Types.Policy.ActionType.class;
            cachedSerClasses.add(cls);
            cachedSerFactories.add(enumsf);
            cachedDeserFactories.add(enumdf);

            qName = new javax.xml.namespace.QName("http://exchangenetwork.net/schema/cdxPolicy.xsd", "PolicyList");
            cachedSerQNames.add(qName);
            cls = Node.NAAS.Types.Policy.PolicyInfo[].class;
            cachedSerClasses.add(cls);
            cachedSerFactories.add(arraysf);
            cachedDeserFactories.add(arraydf);

    }

	  /**
	   * createCall
	   * @param 
	   * @return Call
	   */
    private org.apache.axis.client.Call createCall() throws java.rmi.RemoteException {
        try {
            org.apache.axis.client.Call _call =
                    (org.apache.axis.client.Call) super.service.createCall();
            if (super.maintainSessionSet) {
                _call.setMaintainSession(super.maintainSession);
            }
            if (super.cachedUsername != null) {
                _call.setUsername(super.cachedUsername);
            }
            if (super.cachedPassword != null) {
                _call.setPassword(super.cachedPassword);
            }
            if (super.cachedEndpoint != null) {
                _call.setTargetEndpointAddress(super.cachedEndpoint);
            }
            if (super.cachedTimeout != null) {
                _call.setTimeout(super.cachedTimeout);
            }
            if (super.cachedPortName != null) {
                _call.setPortName(super.cachedPortName);
            }
            java.util.Enumeration keys = super.cachedProperties.keys();
            while (keys.hasMoreElements()) {
                java.lang.String key = (java.lang.String) keys.nextElement();
                _call.setProperty(key, super.cachedProperties.get(key));
            }
            // All the type mapping information is registered
            // when the first call is made.
            // The type mapping information is actually registered in
            // the TypeMappingRegistry of the service, which
            // is the reason why registration is only needed for the first call.
            synchronized (this) {
                if (firstCall()) {
                    // must set encoding style before registering serializers
                    _call.setSOAPVersion(org.apache.axis.soap.SOAPConstants.SOAP11_CONSTANTS);
                    _call.setEncodingStyle(org.apache.axis.Constants.URI_SOAP11_ENC);
                    for (int i = 0; i < cachedSerFactories.size(); ++i) {
                        java.lang.Class cls = (java.lang.Class) cachedSerClasses.get(i);
                        javax.xml.namespace.QName qName =
                                (javax.xml.namespace.QName) cachedSerQNames.get(i);
                        java.lang.Class sf = (java.lang.Class)
                                 cachedSerFactories.get(i);
                        java.lang.Class df = (java.lang.Class)
                                 cachedDeserFactories.get(i);
                        _call.registerTypeMapping(cls, qName, sf, df, false);
                    }
                }
            }
            return _call;
        }
        catch (java.lang.Throwable t) {
            throw new org.apache.axis.AxisFault("Failure trying to get the Call object", t);
        }
    }

	  /**
	   * setPolicy
	   * @param userId
	   * @param credential
	   * @param subject
	   * @param method
	   * @param request
	   * @param params
	   * @param decision
	   * @return String
	   */
    public java.lang.String setPolicy(java.lang.String userId, Node.NAAS.Types.Policy.PasswordType credential, java.lang.String subject, Node.NAAS.Types.Policy.MethodName method, java.lang.String request, java.lang.String params, Node.NAAS.Types.Policy.ActionType decision) throws java.rmi.RemoteException {
        if (super.cachedEndpoint == null) {
            throw new org.apache.axis.NoEndPointException();
        }
        org.apache.axis.client.Call _call = createCall();
        _call.setOperation(_operations[0]);
        _call.setUseSOAPAction(true);
        _call.setSOAPActionURI("");
        _call.setSOAPVersion(org.apache.axis.soap.SOAPConstants.SOAP11_CONSTANTS);
        _call.setOperationName(new javax.xml.namespace.QName("http://exchangenetwork.net/schema/cdxPolicy.xsd", "SetPolicy"));

        setRequestHeaders(_call);
        setAttachments(_call);
        java.lang.Object _resp = _call.invoke(new java.lang.Object[] {userId, credential, subject, method, request, params, decision});

        if (_resp instanceof java.rmi.RemoteException) {
            throw (java.rmi.RemoteException)_resp;
        }
        else {
            extractAttachments(_call);
            try {
                return (java.lang.String) _resp;
            } catch (java.lang.Exception _exception) {
                return (java.lang.String) org.apache.axis.utils.JavaUtils.convert(_resp, java.lang.String.class);
            }
        }
    }

	  /**
	   * deletePolicy
	   * @param userId
	   * @param credential
	   * @param subject
	   * @param method
	   * @param request
	   * @param params
	   * @return String
	   */
    public java.lang.String deletePolicy(java.lang.String userId, Node.NAAS.Types.Policy.PasswordType credential, java.lang.String subject, Node.NAAS.Types.Policy.MethodName method, java.lang.String request, java.lang.String params) throws java.rmi.RemoteException {
        if (super.cachedEndpoint == null) {
            throw new org.apache.axis.NoEndPointException();
        }
        org.apache.axis.client.Call _call = createCall();
        _call.setOperation(_operations[1]);
        _call.setUseSOAPAction(true);
        _call.setSOAPActionURI("");
        _call.setSOAPVersion(org.apache.axis.soap.SOAPConstants.SOAP11_CONSTANTS);
        _call.setOperationName(new javax.xml.namespace.QName("http://exchangenetwork.net/schema/cdxPolicy.xsd", "DeletePolicy"));

        setRequestHeaders(_call);
        setAttachments(_call);
        java.lang.Object _resp = _call.invoke(new java.lang.Object[] {userId, credential, subject, method, request, params});

        if (_resp instanceof java.rmi.RemoteException) {
            throw (java.rmi.RemoteException)_resp;
        }
        else {
            extractAttachments(_call);
            try {
                return (java.lang.String) _resp;
            } catch (java.lang.Exception _exception) {
                return (java.lang.String) org.apache.axis.utils.JavaUtils.convert(_resp, java.lang.String.class);
            }
        }
    }

	  /**
	   * getPolicyList
	   * @param userId
	   * @param credential
	   * @param subject
	   * @param rowId
	   * @param maxRows
	   * @return PolicyInfo[]
	   */
    public Node.NAAS.Types.Policy.PolicyInfo[] getPolicyList(java.lang.String userId, Node.NAAS.Types.Policy.PasswordType credential, java.lang.String subject, java.math.BigInteger rowId, java.math.BigInteger maxRows) throws java.rmi.RemoteException {
        if (super.cachedEndpoint == null) {
            throw new org.apache.axis.NoEndPointException();
        }
        org.apache.axis.client.Call _call = createCall();
        _call.setOperation(_operations[2]);
        _call.setUseSOAPAction(true);
        _call.setSOAPActionURI("");
        _call.setSOAPVersion(org.apache.axis.soap.SOAPConstants.SOAP11_CONSTANTS);
        _call.setOperationName(new javax.xml.namespace.QName("http://exchangenetwork.net/schema/cdxPolicy.xsd", "GetPolicyList"));

        setRequestHeaders(_call);
        setAttachments(_call);
        java.lang.Object _resp = _call.invoke(new java.lang.Object[] {userId, credential, subject, rowId, maxRows});

        if (_resp instanceof java.rmi.RemoteException) {
            throw (java.rmi.RemoteException)_resp;
        }
        else {
            extractAttachments(_call);
            try {
                return (Node.NAAS.Types.Policy.PolicyInfo[]) _resp;
            } catch (java.lang.Exception _exception) {
                return (Node.NAAS.Types.Policy.PolicyInfo[]) org.apache.axis.utils.JavaUtils.convert(_resp, Node.NAAS.Types.Policy.PolicyInfo[].class);
            }
        }
    }

	  /**
	   * getAuthEvents
	   * @param userId
	   * @param credential
	   * @param subject
	   * @param rowId
	   * @param maxRows
	   * @return AuthEventType[]
	   */
    public Node.NAAS.Types.Policy.AuthEventType[] getAuthEvents(java.lang.String userId, Node.NAAS.Types.Policy.PasswordType credential, java.lang.String subject, java.math.BigInteger rowId, java.math.BigInteger maxRows) throws java.rmi.RemoteException {
        if (super.cachedEndpoint == null) {
            throw new org.apache.axis.NoEndPointException();
        }
        org.apache.axis.client.Call _call = createCall();
        _call.setOperation(_operations[3]);
        _call.setUseSOAPAction(true);
        _call.setSOAPActionURI("");
        _call.setSOAPVersion(org.apache.axis.soap.SOAPConstants.SOAP11_CONSTANTS);
        _call.setOperationName(new javax.xml.namespace.QName("http://exchangenetwork.net/schema/cdxPolicy.xsd", "GetAuthEvents"));

        setRequestHeaders(_call);
        setAttachments(_call);
        java.lang.Object _resp = _call.invoke(new java.lang.Object[] {userId, credential, subject, rowId, maxRows});

        if (_resp instanceof java.rmi.RemoteException) {
            throw (java.rmi.RemoteException)_resp;
        }
        else {
            extractAttachments(_call);
            try {
                return (Node.NAAS.Types.Policy.AuthEventType[]) _resp;
            } catch (java.lang.Exception _exception) {
                return (Node.NAAS.Types.Policy.AuthEventType[]) org.apache.axis.utils.JavaUtils.convert(_resp, Node.NAAS.Types.Policy.AuthEventType[].class);
            }
        }
    }

	  /**
	   * verifyPolicy
	   * @param subject
	   * @param node
	   * @param method
	   * @param request
	   * @param params
	   * @return String
	   */
    public java.lang.String verifyPolicy(java.lang.String subject, Node.NAAS.Types.Policy.NodeId node, Node.NAAS.Types.Policy.MethodName method, java.lang.String request, java.lang.String params) throws java.rmi.RemoteException {
        if (super.cachedEndpoint == null) {
            throw new org.apache.axis.NoEndPointException();
        }
        org.apache.axis.client.Call _call = createCall();
        _call.setOperation(_operations[4]);
        _call.setUseSOAPAction(true);
        _call.setSOAPActionURI("");
        _call.setSOAPVersion(org.apache.axis.soap.SOAPConstants.SOAP11_CONSTANTS);
        _call.setOperationName(new javax.xml.namespace.QName("http://exchangenetwork.net/schema/cdxPolicy.xsd", "VerifyPolicy"));

        setRequestHeaders(_call);
        setAttachments(_call);
        java.lang.Object _resp = _call.invoke(new java.lang.Object[] {subject, node, method, request, params});

        if (_resp instanceof java.rmi.RemoteException) {
            throw (java.rmi.RemoteException)_resp;
        }
        else {
            extractAttachments(_call);
            try {
                return (java.lang.String) _resp;
            } catch (java.lang.Exception _exception) {
                return (java.lang.String) org.apache.axis.utils.JavaUtils.convert(_resp, java.lang.String.class);
            }
        }
    }

}
