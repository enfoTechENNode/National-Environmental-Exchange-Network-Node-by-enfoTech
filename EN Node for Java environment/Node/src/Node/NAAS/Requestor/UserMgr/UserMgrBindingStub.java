/**
 * UserMgrBindingStub.java
 *
 * This file was auto-generated from WSDL
 * by the Apache Axis 1.2alpha Dec 01, 2003 (04:33:24 EST) WSDL2Java emitter.
 */

package Node.NAAS.Requestor.UserMgr;

import Node.Phrase;

public class UserMgrBindingStub extends org.apache.axis.client.Stub implements Node.NAAS.Interfaces.UserMgr.UserMgrPortType {
    private java.util.Vector cachedSerClasses = new java.util.Vector();
    private java.util.Vector cachedSerQNames = new java.util.Vector();
    private java.util.Vector cachedSerFactories = new java.util.Vector();
    private java.util.Vector cachedDeserFactories = new java.util.Vector();

    static org.apache.axis.description.OperationDesc [] _operations;

    static {
        _operations = new org.apache.axis.description.OperationDesc[5];
        _initOperationDesc1();
    }

    private static void _initOperationDesc1(){
        org.apache.axis.description.OperationDesc oper;
        oper = new org.apache.axis.description.OperationDesc();
        oper.setName("AddUser");
        oper.addParameter(new javax.xml.namespace.QName("", "adminName"), new javax.xml.namespace.QName("http://www.w3.org/2001/XMLSchema", "string"), java.lang.String.class, org.apache.axis.description.ParameterDesc.IN, false, false);
        oper.addParameter(new javax.xml.namespace.QName("", "adminPwd"), new javax.xml.namespace.QName("http://exchangenetwork.net/schema/usermgr.xsd", "PasswordType"), Node.NAAS.Types.UserMgr.PasswordType.class, org.apache.axis.description.ParameterDesc.IN, false, false);
        oper.addParameter(new javax.xml.namespace.QName("", "userEmail"), new javax.xml.namespace.QName("http://www.w3.org/2001/XMLSchema", "string"), java.lang.String.class, org.apache.axis.description.ParameterDesc.IN, false, false);
        oper.addParameter(new javax.xml.namespace.QName("", "userType"), new javax.xml.namespace.QName("http://exchangenetwork.net/schema/usermgr.xsd", "UserType"), Node.NAAS.Types.UserMgr.UserType.class, org.apache.axis.description.ParameterDesc.IN, false, false);
        oper.addParameter(new javax.xml.namespace.QName("", "userPwd"), new javax.xml.namespace.QName("http://exchangenetwork.net/schema/usermgr.xsd", "PasswordType"), Node.NAAS.Types.UserMgr.PasswordType.class, org.apache.axis.description.ParameterDesc.IN, false, false);
        oper.addParameter(new javax.xml.namespace.QName("", "confirmUserPwd"), new javax.xml.namespace.QName("http://exchangenetwork.net/schema/usermgr.xsd", "PasswordType"), Node.NAAS.Types.UserMgr.PasswordType.class, org.apache.axis.description.ParameterDesc.IN, false, false);
        oper.addParameter(new javax.xml.namespace.QName("", "affiliate"), new javax.xml.namespace.QName("http://exchangenetwork.net/schema/usermgr.xsd", "StateId"), Node.NAAS.Types.UserMgr.StateId.class, org.apache.axis.description.ParameterDesc.IN, false, false);
        oper.setReturnType(new javax.xml.namespace.QName("http://www.w3.org/2001/XMLSchema", "string"));
        oper.setReturnClass(java.lang.String.class);
        oper.setReturnQName(new javax.xml.namespace.QName("", "return"));
//        oper.setStyle(org.apache.axis.enum.Style.RPC);
//        oper.setUse(org.apache.axis.enum.Use.ENCODED);
        oper.setStyle(org.apache.axis.constants.Style.RPC);
        oper.setUse(org.apache.axis.constants.Use.ENCODED);
        _operations[0] = oper;

        oper = new org.apache.axis.description.OperationDesc();
        oper.setName("DeleteUser");
        oper.addParameter(new javax.xml.namespace.QName("", "adminName"), new javax.xml.namespace.QName("http://www.w3.org/2001/XMLSchema", "string"), java.lang.String.class, org.apache.axis.description.ParameterDesc.IN, false, false);
        oper.addParameter(new javax.xml.namespace.QName("", "adminPwd"), new javax.xml.namespace.QName("http://exchangenetwork.net/schema/usermgr.xsd", "PasswordType"), Node.NAAS.Types.UserMgr.PasswordType.class, org.apache.axis.description.ParameterDesc.IN, false, false);
        oper.addParameter(new javax.xml.namespace.QName("", "userEmail"), new javax.xml.namespace.QName("http://www.w3.org/2001/XMLSchema", "string"), java.lang.String.class, org.apache.axis.description.ParameterDesc.IN, false, false);
        oper.setReturnType(new javax.xml.namespace.QName("http://www.w3.org/2001/XMLSchema", "string"));
        oper.setReturnClass(java.lang.String.class);
        oper.setReturnQName(new javax.xml.namespace.QName("", "return"));
//        oper.setStyle(org.apache.axis.enum.Style.RPC);
//        oper.setUse(org.apache.axis.enum.Use.ENCODED);
        oper.setStyle(org.apache.axis.constants.Style.RPC);
        oper.setUse(org.apache.axis.constants.Use.ENCODED);
        _operations[1] = oper;

        oper = new org.apache.axis.description.OperationDesc();
        oper.setName("UpdateUser");
        oper.addParameter(new javax.xml.namespace.QName("", "adminName"), new javax.xml.namespace.QName("http://www.w3.org/2001/XMLSchema", "string"), java.lang.String.class, org.apache.axis.description.ParameterDesc.IN, false, false);
        oper.addParameter(new javax.xml.namespace.QName("", "adminPwd"), new javax.xml.namespace.QName("http://exchangenetwork.net/schema/usermgr.xsd", "PasswordType"), Node.NAAS.Types.UserMgr.PasswordType.class, org.apache.axis.description.ParameterDesc.IN, false, false);
        oper.addParameter(new javax.xml.namespace.QName("", "userEmail"), new javax.xml.namespace.QName("http://www.w3.org/2001/XMLSchema", "string"), java.lang.String.class, org.apache.axis.description.ParameterDesc.IN, false, false);
        oper.addParameter(new javax.xml.namespace.QName("", "userType"), new javax.xml.namespace.QName("http://exchangenetwork.net/schema/usermgr.xsd", "UserType"), Node.NAAS.Types.UserMgr.UserType.class, org.apache.axis.description.ParameterDesc.IN, false, false);
        oper.addParameter(new javax.xml.namespace.QName("", "userPwd"), new javax.xml.namespace.QName("http://exchangenetwork.net/schema/usermgr.xsd", "PasswordType"), Node.NAAS.Types.UserMgr.PasswordType.class, org.apache.axis.description.ParameterDesc.IN, false, false);
        oper.addParameter(new javax.xml.namespace.QName("", "owner"), new javax.xml.namespace.QName("http://www.w3.org/2001/XMLSchema", "string"), java.lang.String.class, org.apache.axis.description.ParameterDesc.IN, false, false);
        oper.addParameter(new javax.xml.namespace.QName("", "affiliate"), new javax.xml.namespace.QName("http://exchangenetwork.net/schema/usermgr.xsd", "StateId"), Node.NAAS.Types.UserMgr.StateId.class, org.apache.axis.description.ParameterDesc.IN, false, false);
        oper.setReturnType(new javax.xml.namespace.QName("http://www.w3.org/2001/XMLSchema", "string"));
        oper.setReturnClass(java.lang.String.class);
        oper.setReturnQName(new javax.xml.namespace.QName("", "return"));
//        oper.setStyle(org.apache.axis.enum.Style.RPC);
//        oper.setUse(org.apache.axis.enum.Use.ENCODED);
        oper.setStyle(org.apache.axis.constants.Style.RPC);
        oper.setUse(org.apache.axis.constants.Use.ENCODED);
        _operations[2] = oper;

        oper = new org.apache.axis.description.OperationDesc();
        oper.setName("GetUserList");
        oper.addParameter(new javax.xml.namespace.QName("", "adminName"), new javax.xml.namespace.QName("http://www.w3.org/2001/XMLSchema", "string"), java.lang.String.class, org.apache.axis.description.ParameterDesc.IN, false, false);
        oper.addParameter(new javax.xml.namespace.QName("", "adminPwd"), new javax.xml.namespace.QName("http://exchangenetwork.net/schema/usermgr.xsd", "PasswordType"), Node.NAAS.Types.UserMgr.PasswordType.class, org.apache.axis.description.ParameterDesc.IN, false, false);
        oper.addParameter(new javax.xml.namespace.QName("", "userEmail"), new javax.xml.namespace.QName("http://www.w3.org/2001/XMLSchema", "string"), java.lang.String.class, org.apache.axis.description.ParameterDesc.IN, false, false);
        oper.addParameter(new javax.xml.namespace.QName("", "userState"), new javax.xml.namespace.QName("http://exchangenetwork.net/schema/usermgr.xsd", "StateId"), Node.NAAS.Types.UserMgr.StateId.class, org.apache.axis.description.ParameterDesc.IN, false, false);
        oper.addParameter(new javax.xml.namespace.QName("", "rowId"), new javax.xml.namespace.QName("http://www.w3.org/2001/XMLSchema", "integer"), java.math.BigInteger.class, org.apache.axis.description.ParameterDesc.IN, false, false);
        oper.addParameter(new javax.xml.namespace.QName("", "maxRows"), new javax.xml.namespace.QName("http://www.w3.org/2001/XMLSchema", "integer"), java.math.BigInteger.class, org.apache.axis.description.ParameterDesc.IN, false, false);
        oper.setReturnType(new javax.xml.namespace.QName("http://neien.org/schema/usermgr.xsd", "UserList"));
        oper.setReturnClass(Node.NAAS.Types.UserMgr.UserInfo[].class);
        oper.setReturnQName(new javax.xml.namespace.QName("", "return"));
//        oper.setStyle(org.apache.axis.enum.Style.RPC);
//        oper.setUse(org.apache.axis.enum.Use.ENCODED);
        oper.setStyle(org.apache.axis.constants.Style.RPC);
        oper.setUse(org.apache.axis.constants.Use.ENCODED);
        _operations[3] = oper;

        oper = new org.apache.axis.description.OperationDesc();
        oper.setName("ChangePwd");
        oper.addParameter(new javax.xml.namespace.QName("", "userEmail"), new javax.xml.namespace.QName("http://www.w3.org/2001/XMLSchema", "string"), java.lang.String.class, org.apache.axis.description.ParameterDesc.IN, false, false);
        oper.addParameter(new javax.xml.namespace.QName("", "password"), new javax.xml.namespace.QName("http://exchangenetwork.net/schema/usermgr.xsd", "PasswordType"), Node.NAAS.Types.UserMgr.PasswordType.class, org.apache.axis.description.ParameterDesc.IN, false, false);
        oper.addParameter(new javax.xml.namespace.QName("", "newPwd"), new javax.xml.namespace.QName("http://exchangenetwork.net/schema/usermgr.xsd", "PasswordType"), Node.NAAS.Types.UserMgr.PasswordType.class, org.apache.axis.description.ParameterDesc.IN, false, false);
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
    public UserMgrBindingStub() throws org.apache.axis.AxisFault {
         this(null);
    }

	  /**
	   * Constructor
	   * @param endpointURL
	   * @param service
	   * @return 
	   */
   public UserMgrBindingStub(java.net.URL endpointURL, javax.xml.rpc.Service service) throws org.apache.axis.AxisFault {
         this(service);
         super.cachedEndpoint = endpointURL;
    }

	  /**
	   * Constructor
	   * @param service
	   * @return 
	   */
    public UserMgrBindingStub(javax.xml.rpc.Service service) throws org.apache.axis.AxisFault {
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
            qName = new javax.xml.namespace.QName("http://exchangenetwork.net/schema/usermgr.xsd", "StateId");
            cachedSerQNames.add(qName);
            cls = Node.NAAS.Types.UserMgr.StateId.class;
            cachedSerClasses.add(cls);
            cachedSerFactories.add(enumsf);
            cachedDeserFactories.add(enumdf);

            qName = new javax.xml.namespace.QName("http://neien.org/schema/usermgr.xsd", "UserList");
            cachedSerQNames.add(qName);
            cls = Node.NAAS.Types.UserMgr.UserInfo[].class;
            cachedSerClasses.add(cls);
            cachedSerFactories.add(arraysf);
            cachedDeserFactories.add(arraydf);

            qName = new javax.xml.namespace.QName("http://neien.org/schema/usermgr.xsd", "UserInfo");
            cachedSerQNames.add(qName);
            cls = Node.NAAS.Types.UserMgr.UserInfo.class;
            cachedSerClasses.add(cls);
            cachedSerFactories.add(beansf);
            cachedDeserFactories.add(beandf);

            qName = new javax.xml.namespace.QName("http://exchangenetwork.net/schema/usermgr.xsd", "PasswordType");
            cachedSerQNames.add(qName);
            cls = Node.NAAS.Types.UserMgr.PasswordType.class;
            cachedSerClasses.add(cls);
            cachedSerFactories.add(simplesf);
            cachedDeserFactories.add(simpledf);

            qName = new javax.xml.namespace.QName("http://exchangenetwork.net/schema/usermgr.xsd", "UserType");
            cachedSerQNames.add(qName);
            cls = Node.NAAS.Types.UserMgr.UserType.class;
            cachedSerClasses.add(cls);
            cachedSerFactories.add(enumsf);
            cachedDeserFactories.add(enumdf);

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
	   * addUser
	   * @param adminName
	   * @param adminPwd
	   * @param userEmail
	   * @param userType
	   * @param userPwd
	   * @param confirmUserPwd
	   * @param affiliate
	   * @return String
	   */
    public java.lang.String addUser(java.lang.String adminName, Node.NAAS.Types.UserMgr.PasswordType adminPwd, java.lang.String userEmail, Node.NAAS.Types.UserMgr.UserType userType, Node.NAAS.Types.UserMgr.PasswordType userPwd,Node.NAAS.Types.UserMgr.PasswordType confirmUserPwd, Node.NAAS.Types.UserMgr.StateId affiliate) throws java.rmi.RemoteException {
        if (super.cachedEndpoint == null) {
            throw new org.apache.axis.NoEndPointException();
        }
        org.apache.axis.client.Call _call = createCall();
        _call.setOperation(_operations[0]);
        _call.setSOAPVersion(org.apache.axis.soap.SOAPConstants.SOAP11_CONSTANTS);
        _call.setOperationName(new javax.xml.namespace.QName("http://exchangenetwork.net/schema/usermgr.xsd", "AddUser"));

        setRequestHeaders(_call);
        setAttachments(_call);
        java.lang.Object _resp = _call.invoke(new java.lang.Object[] {adminName, adminPwd, userEmail, userType, userPwd, confirmUserPwd, affiliate});

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
	   * deleteUser
	   * @param adminName
	   * @param adminPwd
	   * @param userEmail
	   * @return String
	   */
    public java.lang.String deleteUser(java.lang.String adminName, Node.NAAS.Types.UserMgr.PasswordType adminPwd, java.lang.String userEmail) throws java.rmi.RemoteException {
        if (super.cachedEndpoint == null) {
            throw new org.apache.axis.NoEndPointException();
        }
        org.apache.axis.client.Call _call = createCall();
        _call.setOperation(_operations[1]);
        _call.setSOAPVersion(org.apache.axis.soap.SOAPConstants.SOAP11_CONSTANTS);
        _call.setOperationName(new javax.xml.namespace.QName("http://exchangenetwork.net/schema/usermgr.xsd", "DeleteUser"));

        setRequestHeaders(_call);
        setAttachments(_call);
        java.lang.Object _resp = _call.invoke(new java.lang.Object[] {adminName, adminPwd, userEmail});

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
	   * updateUser
	   * @param adminName
	   * @param adminPwd
	   * @param userEmail
	   * @param userType
	   * @param userPwd
	   * @param owner
	   * @param affiliate
	   * @return String
	   */
    public java.lang.String updateUser(java.lang.String adminName, Node.NAAS.Types.UserMgr.PasswordType adminPwd, java.lang.String userEmail, Node.NAAS.Types.UserMgr.UserType userType, Node.NAAS.Types.UserMgr.PasswordType userPwd, java.lang.String owner, Node.NAAS.Types.UserMgr.StateId affiliate) throws java.rmi.RemoteException {
        if (super.cachedEndpoint == null) {
            throw new org.apache.axis.NoEndPointException();
        }
        org.apache.axis.client.Call _call = createCall();
        _call.setOperation(_operations[2]);
        _call.setSOAPVersion(org.apache.axis.soap.SOAPConstants.SOAP11_CONSTANTS);
        _call.setOperationName(new javax.xml.namespace.QName("http://exchangenetwork.net/schema/usermgr.xsd", "UpdateUser"));

        setRequestHeaders(_call);
        setAttachments(_call);
        java.lang.Object _resp = _call.invoke(new java.lang.Object[] {adminName, adminPwd, userEmail, userType, userPwd, owner, affiliate});

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
	   * getUserList
	   * @param adminName
	   * @param adminPwd
	   * @param userEmail
	   * @param userState
	   * @param rowId
	   * @param maxRows
	   * @return UserInfo[]
	   */
    public Node.NAAS.Types.UserMgr.UserInfo[] getUserList(java.lang.String adminName, Node.NAAS.Types.UserMgr.PasswordType adminPwd, java.lang.String userEmail, Node.NAAS.Types.UserMgr.StateId userState, java.math.BigInteger rowId, java.math.BigInteger maxRows) throws java.rmi.RemoteException {
        if (super.cachedEndpoint == null) {
            throw new org.apache.axis.NoEndPointException();
        }
        org.apache.axis.client.Call _call = createCall();
        _call.setOperation(_operations[3]);
        _call.setSOAPVersion(org.apache.axis.soap.SOAPConstants.SOAP11_CONSTANTS);
        _call.setOperationName(new javax.xml.namespace.QName("http://exchangenetwork.net/schema/usermgr.xsd", "GetUserList"));

        setRequestHeaders(_call);
        setAttachments(_call);
        java.lang.Object _resp = _call.invoke(new java.lang.Object[] {adminName, adminPwd, userEmail, userState, rowId, maxRows});

        if (_resp instanceof java.rmi.RemoteException) {
            throw (java.rmi.RemoteException)_resp;
        }
        else {
          extractAttachments(_call);
        try {
                return (Node.NAAS.Types.UserMgr.UserInfo[]) _resp;
            } catch (java.lang.Exception _exception) {
                return (Node.NAAS.Types.UserMgr.UserInfo[]) org.apache.axis.utils.JavaUtils.convert(_resp, Node.NAAS.Types.UserMgr.UserInfo[].class);
            }
          }
    }

	  /**
	   * changePwd
	   * @param userEmail
	   * @param password
	   * @param newPwd
	   * @return String
	   */
    public java.lang.String changePwd(java.lang.String userEmail, Node.NAAS.Types.UserMgr.PasswordType password, Node.NAAS.Types.UserMgr.PasswordType newPwd) throws java.rmi.RemoteException {
        if (super.cachedEndpoint == null) {
            throw new org.apache.axis.NoEndPointException();
        }
        org.apache.axis.client.Call _call = createCall();
        _call.setOperation(_operations[4]);
        _call.setSOAPVersion(org.apache.axis.soap.SOAPConstants.SOAP11_CONSTANTS);
        _call.setOperationName(new javax.xml.namespace.QName("http://exchangenetwork.net/schema/usermgr.xsd", "ChangePwd"));

        setRequestHeaders(_call);
        setAttachments(_call);
        java.lang.Object _resp = _call.invoke(new java.lang.Object[] {userEmail, password, newPwd});

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
