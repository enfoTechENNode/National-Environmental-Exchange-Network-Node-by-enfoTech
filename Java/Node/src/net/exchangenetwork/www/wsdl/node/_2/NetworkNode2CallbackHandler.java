
/**
 * NetworkNode2CallbackHandler.java
 *
 * This file was auto-generated from WSDL
 * by the Apache Axis2 version: 1.4.1  Built on : Aug 19, 2008 (10:13:39 LKT)
 */

    package net.exchangenetwork.www.wsdl.node._2;

    /**
     *  NetworkNode2CallbackHandler Callback class, Users can extend this class and implement
     *  their own receiveResult and receiveError methods.
     */
    public abstract class NetworkNode2CallbackHandler{



    protected Object clientData;

    /**
    * User can pass in any object that needs to be accessed once the NonBlocking
    * Web service call is finished and appropriate method of this CallBack is called.
    * @param clientData Object mechanism by which the user can pass in user data
    * that will be avilable at the time this callback is called.
    */
    public NetworkNode2CallbackHandler(Object clientData){
        this.clientData = clientData;
    }

    /**
    * Please use this constructor if you don't want to set any clientData
    */
    public NetworkNode2CallbackHandler(){
        this.clientData = null;
    }

    /**
     * Get the client data
     */

     public Object getClientData() {
        return clientData;
     }

        
           /**
            * auto generated Axis2 call back method for Execute method
            * override this method for handling normal response from Execute operation
            */
           public void receiveResultExecute(
                    net.exchangenetwork.www.schema.node._2.ExecuteResponse result
                        ) {
           }

          /**
           * auto generated Axis2 Error handler
           * override this method for handling error response from Execute operation
           */
            public void receiveErrorExecute(java.lang.Exception e) {
            }
                
           /**
            * auto generated Axis2 call back method for Authenticate method
            * override this method for handling normal response from Authenticate operation
            */
           public void receiveResultAuthenticate(
                    net.exchangenetwork.www.schema.node._2.AuthenticateResponse result
                        ) {
           }

          /**
           * auto generated Axis2 Error handler
           * override this method for handling error response from Authenticate operation
           */
            public void receiveErrorAuthenticate(java.lang.Exception e) {
            }
                
           /**
            * auto generated Axis2 call back method for Download method
            * override this method for handling normal response from Download operation
            */
           public void receiveResultDownload(
                    net.exchangenetwork.www.schema.node._2.DownloadResponse result
                        ) {
           }

          /**
           * auto generated Axis2 Error handler
           * override this method for handling error response from Download operation
           */
            public void receiveErrorDownload(java.lang.Exception e) {
            }
                
           /**
            * auto generated Axis2 call back method for GetStatus method
            * override this method for handling normal response from GetStatus operation
            */
           public void receiveResultGetStatus(
                    net.exchangenetwork.www.schema.node._2.GetStatusResponse result
                        ) {
           }

          /**
           * auto generated Axis2 Error handler
           * override this method for handling error response from GetStatus operation
           */
            public void receiveErrorGetStatus(java.lang.Exception e) {
            }
                
           /**
            * auto generated Axis2 call back method for NodePing method
            * override this method for handling normal response from NodePing operation
            */
           public void receiveResultNodePing(
                    net.exchangenetwork.www.schema.node._2.NodePingResponse result
                        ) {
           }

          /**
           * auto generated Axis2 Error handler
           * override this method for handling error response from NodePing operation
           */
            public void receiveErrorNodePing(java.lang.Exception e) {
            }
                
           /**
            * auto generated Axis2 call back method for GetServices method
            * override this method for handling normal response from GetServices operation
            */
           public void receiveResultGetServices(
                    net.exchangenetwork.www.schema.node._2.GetServicesResponse result
                        ) {
           }

          /**
           * auto generated Axis2 Error handler
           * override this method for handling error response from GetServices operation
           */
            public void receiveErrorGetServices(java.lang.Exception e) {
            }
                
           /**
            * auto generated Axis2 call back method for Submit method
            * override this method for handling normal response from Submit operation
            */
           public void receiveResultSubmit(
                    net.exchangenetwork.www.schema.node._2.SubmitResponse result
                        ) {
           }

          /**
           * auto generated Axis2 Error handler
           * override this method for handling error response from Submit operation
           */
            public void receiveErrorSubmit(java.lang.Exception e) {
            }
                
           /**
            * auto generated Axis2 call back method for Notify method
            * override this method for handling normal response from Notify operation
            */
           public void receiveResultNotify(
                    net.exchangenetwork.www.schema.node._2.NotifyResponse result
                        ) {
           }

          /**
           * auto generated Axis2 Error handler
           * override this method for handling error response from Notify operation
           */
            public void receiveErrorNotify(java.lang.Exception e) {
            }
                
           /**
            * auto generated Axis2 call back method for Solicit method
            * override this method for handling normal response from Solicit operation
            */
           public void receiveResultSolicit(
                    net.exchangenetwork.www.schema.node._2.SolicitResponse result
                        ) {
           }

          /**
           * auto generated Axis2 Error handler
           * override this method for handling error response from Solicit operation
           */
            public void receiveErrorSolicit(java.lang.Exception e) {
            }
                
           /**
            * auto generated Axis2 call back method for Query method
            * override this method for handling normal response from Query operation
            */
           public void receiveResultQuery(
                    net.exchangenetwork.www.schema.node._2.QueryResponse result
                        ) {
           }

          /**
           * auto generated Axis2 Error handler
           * override this method for handling error response from Query operation
           */
            public void receiveErrorQuery(java.lang.Exception e) {
            }
                


    }
    