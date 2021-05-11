using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Data;

using Node.Core.Data;

namespace Node.Core.Biz.Objects
{
    /// <summary>
    /// ServiceRegistration is used to access service registration XML file. 
    /// </summary>
    public class ServiceRegistration
    {
        #region Public Constructors
        /// <summary>
        /// Constructor of ServiceRegistration.
        /// </summary>
        public ServiceRegistration()
        {
            this.ServiceReg = new DBManager().GetConfigurationsDB().GetServiceRegistration();
        }
        /// <summary>
        /// Constructor of ServiceRegistration.
        /// </summary>
        /// <param name="registration">Service Registration XML in XmlDocument</param>
        public ServiceRegistration(XmlDocument registration)
        {
            this.ServiceReg = registration;
        }

        #endregion

        #region Public Methods
        /// <summary>
        /// The method update service registration file.
        /// </summary>
        /// <param name="methodName">The name of Method</param>
        /// <param name="dataFlow">The name of dataflow</param>
        /// <param name="serviceIdentifier">The identifier of service</param>
        /// <param name="serviceDesc">The description of service</param>
        /// <param name="serviceDocuURL">The url of service</param>
        /// <param name="serviceProps">The properties of service</param>
        /// <param name="styleSheetURL">The url of stylesheet</param>
        /// <param name="parameters">The parameters of service</param>
        /// <returns></returns>
        public XmlDocument UpdateService(string methodName, string dataFlow, string serviceIdentifier, string serviceDesc, string serviceDocuURL, string[] serviceProps, string styleSheetURL, DataTable parameters)
        {
            XmlNode existingServiceNode = null;
            if (!string.IsNullOrEmpty(dataFlow))
                existingServiceNode = this.ServiceReg.SelectSingleNode("/NetworkNodes/NetworkNodeDetails/NodeServiceList/Service[MethodName/text() = '" + methodName + "' and DataFlow/text() = '" + dataFlow + "']");
            else
                existingServiceNode = this.ServiceReg.SelectSingleNode("/NetworkNodes/NetworkNodeDetails/NodeServiceList/Service[MethodName/text() = '" + methodName + "']");
            if (existingServiceNode != null && existingServiceNode.ParentNode != null)
            {
                existingServiceNode.ParentNode.RemoveChild(existingServiceNode);
            }

            XmlNode serviceElement = this.ServiceReg.CreateElement("Service");

            XmlNode methodNameElement = this.ServiceReg.CreateElement("MethodName");
            methodNameElement.InnerText = methodName;
            serviceElement.AppendChild(methodNameElement);

            XmlNode dataFlowElement = this.ServiceReg.CreateElement("DataFlow");
            dataFlowElement.InnerText = dataFlow;
            serviceElement.AppendChild(dataFlowElement);

            XmlNode serviceIdentifierElement = this.ServiceReg.CreateElement("ServiceIdentifier");
            serviceIdentifierElement.InnerText = serviceIdentifier;
            serviceElement.AppendChild(serviceIdentifierElement);

            XmlNode serviceDescriptionElement = this.ServiceReg.CreateElement("ServiceDescription");
            serviceDescriptionElement.InnerText = serviceDesc;
            serviceElement.AppendChild(serviceDescriptionElement);

            XmlNode serviceDocumentURLElement = this.ServiceReg.CreateElement("ServiceDocumentURL");
            serviceDocumentURLElement.InnerText = serviceDocuURL;
            serviceElement.AppendChild(serviceDocumentURLElement);

            XmlNode styleSheetURLElement = this.ServiceReg.CreateElement("StyleSheetURL");
            styleSheetURLElement.InnerText = styleSheetURL;
            serviceElement.AppendChild(styleSheetURLElement);

            //Start Service Property
            XmlNode servicePropertyElement = this.ServiceReg.CreateElement("ServiceProperty");
            XmlNode propertyNameElement = this.ServiceReg.CreateElement("PropertyName");
            propertyNameElement.InnerText = serviceProps[0];
            servicePropertyElement.AppendChild(propertyNameElement);

            XmlNode propertyValueElement = this.ServiceReg.CreateElement("PropertyValue");
            propertyValueElement.InnerText = serviceProps[1];
            servicePropertyElement.AppendChild(propertyValueElement);
            serviceElement.AppendChild(servicePropertyElement);
            //End Service Property

            //Start Parameter
            if (parameters != null && parameters.Rows.Count > 0)
            {
                foreach (DataRow dr in parameters.Rows)
                {
                    XmlNode serviceParameterElement = this.ServiceReg.CreateElement("Parameter");
                    serviceParameterElement.Attributes.Append(this.ServiceReg.CreateAttribute("ParameterTypeDescriptor"));
                    serviceParameterElement.Attributes["ParameterTypeDescriptor"].Value = dr["TYPE_DESC"].ToString();

                    serviceParameterElement.Attributes.Append(this.ServiceReg.CreateAttribute("PrameterRequiredIndicator"));
                    serviceParameterElement.Attributes["PrameterRequiredIndicator"].Value = dr["REQUIREDIND"].ToString();

                    serviceParameterElement.Attributes.Append(this.ServiceReg.CreateAttribute("ParameterType"));
                    serviceParameterElement.Attributes["ParameterType"].Value = dr["PARAM_TYPE"].ToString();

                    serviceParameterElement.Attributes.Append(this.ServiceReg.CreateAttribute("ParameterName"));
                    serviceParameterElement.Attributes["ParameterName"].Value = dr["PARAM_NAME"].ToString();

                    serviceParameterElement.Attributes.Append(this.ServiceReg.CreateAttribute("ParameterSortIndex"));
                    serviceParameterElement.Attributes["ParameterSortIndex"].Value = dr["SORT_INDEX"].ToString();

                    serviceParameterElement.Attributes.Append(this.ServiceReg.CreateAttribute("ParameterOccurenceNumber"));
                    serviceParameterElement.Attributes["ParameterOccurenceNumber"].Value = dr["OCCURENCE_NO"].ToString();

                    serviceParameterElement.Attributes.Append(this.ServiceReg.CreateAttribute("ParameterEncoding"));
                    serviceParameterElement.Attributes["ParameterEncoding"].Value = dr["ENCODING"].ToString();

                    serviceParameterElement.InnerText = dr["PARAM_VALUE"].ToString();

                    serviceElement.AppendChild(serviceParameterElement);
                }
            }
            //End Parameters

            this.ServiceReg.SelectSingleNode("/NetworkNodes/NetworkNodeDetails/NodeServiceList").AppendChild(serviceElement);

            return this.ServiceReg;
        }
        /// <summary>
        /// The method returns nodeidentifier.
        /// </summary>
        /// <returns>nodeidentifier</returns>
        public string GetNodeIdentifier()
        {
            return this.ServiceReg.SelectSingleNode("/NetworkNodes/NetworkNodeDetails/NodeIdentifier").InnerText;
        }
        /// <summary>
        /// The method updates node identifier.
        /// </summary>
        /// <param name="nodeIdentifier">nodeIdentifier</param>
        /// <returns></returns>
        public string SetNodeIdentifier(string nodeIdentifier)
        {
            this.ServiceReg.SelectSingleNode("/NetworkNodes/NetworkNodeDetails/NodeIdentifier").InnerText = nodeIdentifier;
            return "";
        }
        /// <summary>
        /// The method returns node name. 
        /// </summary>
        /// <returns>node name.</returns>
        public string GetNodeName()
        {
            return this.ServiceReg.SelectSingleNode("/NetworkNodes/NetworkNodeDetails/NodeName").InnerText;
        }
        /// <summary>
        /// The method updates node name.
        /// </summary>
        /// <param name="nodeName">nodeName</param>
        /// <returns></returns>
        public string SetNodeName(string nodeName)
        {
            this.ServiceReg.SelectSingleNode("/NetworkNodes/NetworkNodeDetails/NodeName").InnerText = nodeName;
            return "";
        }
        /// <summary>
        /// The method get node address.
        /// </summary>
        /// <returns>node address.</returns>
        public string GetNodeAddress()
        {
            return this.ServiceReg.SelectSingleNode("/NetworkNodes/NetworkNodeDetails/NodeAddress").InnerText;
        }
        /// <summary>
        /// The method updates Node address.
        /// </summary>
        /// <param name="nodeAddress">nodeAddress</param>
        /// <returns></returns>
        public string SetNodeAddress(string nodeAddress)
        {
            this.ServiceReg.SelectSingleNode("/NetworkNodes/NetworkNodeDetails/NodeAddress").InnerText = nodeAddress;
            return "";
        }
        /// <summary>
        /// The method gets organizationidentifier.
        /// </summary>
        /// <returns>organization identifier.</returns>
        public string GetOrganizationIdentifier()
        {
            return this.ServiceReg.SelectSingleNode("/NetworkNodes/NetworkNodeDetails/OrganizationIdentifier").InnerText;
        }
        /// <summary>
        /// The method updates organization identifier.
        /// </summary>
        /// <param name="organizationIdentifier">organizationIdentifier</param>
        /// <returns></returns>
        public string SetOrganizationIdentifier(string organizationIdentifier)
        {
            this.ServiceReg.SelectSingleNode("/NetworkNodes/NetworkNodeDetails/OrganizationIdentifier").InnerText = organizationIdentifier;
            return "";
        }
        /// <summary>
        /// The method returns node contact.
        /// </summary>
        /// <returns>node contact.</returns>
        public string GetNodeContact()
        {
            return this.ServiceReg.SelectSingleNode("/NetworkNodes/NetworkNodeDetails/NodeContact").InnerText;
        }
        /// <summary>
        /// The method updates node contact
        /// </summary>
        /// <param name="nodeContact">nodeContact</param>
        /// <returns></returns>
        public string SetNodeContact(string nodeContact)
        {
            this.ServiceReg.SelectSingleNode("/NetworkNodes/NetworkNodeDetails/NodeContact").InnerText = nodeContact;
            return "";
        }
        /// <summary>
        /// The method returns node version identifier. 
        /// </summary>
        /// <returns>node version identifier.</returns>
        public string GetNodeVersionIdentifier()
        {
            return this.ServiceReg.SelectSingleNode("/NetworkNodes/NetworkNodeDetails/NodeVersionIdentifier").InnerText;
        }
        /// <summary>
        /// The method updates node version identifier.
        /// </summary>
        /// <param name="nodeVersionIdentifier">nodeVersionIdentifier</param>
        /// <returns></returns>
        public string SetNodeVersionIdentifier(string nodeVersionIdentifier)
        {
            this.ServiceReg.SelectSingleNode("/NetworkNodes/NetworkNodeDetails/NodeVersionIdentifier").InnerText = nodeVersionIdentifier;
            return "";
        }
        /// <summary>
        /// The method gets node deployment type code
        /// </summary>
        /// <returns>node depeloyment type code.</returns>
        public string GetNodeDeploymentTypeCode()
        {
            return this.ServiceReg.SelectSingleNode("/NetworkNodes/NetworkNodeDetails/NodeDeploymentTypeCode").InnerText;
        }
        /// <summary>
        /// The method updates node deployment type code
        /// </summary>
        /// <param name="nodeDeploymentTypeCode">nodeDeploymentTypeCode</param>
        /// <returns></returns>
        public string SetNodeDeploymentTypeCode(string nodeDeploymentTypeCode)
        {
            this.ServiceReg.SelectSingleNode("/NetworkNodes/NetworkNodeDetails/NodeDeploymentTypeCode").InnerText = nodeDeploymentTypeCode;
            return "";
        }
        /// <summary>
        /// The method returns node status.
        /// </summary>
        /// <returns>node status.</returns>
        public string GetNodeStatus()
        {
            return this.ServiceReg.SelectSingleNode("/NetworkNodes/NetworkNodeDetails/NodeStatus").InnerText;
        }
        /// <summary>
        /// The method updates node status.
        /// </summary>
        /// <param name="status">status</param>
        /// <returns></returns>
        public string SetNodeStatus(string status)
        {
            this.ServiceReg.SelectSingleNode("/NetworkNodes/NetworkNodeDetails/NodeStatus").InnerText = status;
            return "";
        }
        /// <summary>
        /// The method returns node property name
        /// </summary>
        /// <returns>name property name</returns>
        public string GetNodePropertyName()
        {
            return this.ServiceReg.SelectSingleNode("/NetworkNodes/NetworkNodeDetails/NodeProperty/PropertyName").InnerText;
        }
        /// <summary>
        /// The method updates node property name.
        /// </summary>
        /// <param name="nodePropertyName">nodePropertyName</param>
        /// <returns></returns>
        public string SetNodePropertyName(string nodePropertyName)
        {
            this.ServiceReg.SelectSingleNode("/NetworkNodes/NetworkNodeDetails/NodeProperty/PropertyName").InnerText = nodePropertyName;
            return "";
        }
        /// <summary>
        /// The method returns node property value.
        /// </summary>
        /// <returns>node property value.</returns>
        public string GetNodePropertyValue()
        {
            return this.ServiceReg.SelectSingleNode("/NetworkNodes/NetworkNodeDetails/NodeProperty/PropertyValue").InnerText;
        }
        /// <summary>
        /// The method updates node property value.
        /// </summary>
        /// <param name="nodePropertyValue">nodePropertyValue</param>
        /// <returns></returns>
        public string SetNodePropertyValue(string nodePropertyValue)
        {
            this.ServiceReg.SelectSingleNode("/NetworkNodes/NetworkNodeDetails/NodeProperty/PropertyValue").InnerText = nodePropertyValue;
            return "";
        }
        /// <summary>
        /// The method returns boundingbox east point.
        /// </summary>
        /// <returns>bounding box east point.</returns>
        public string GetBoundingCoordinateEast()
        {
            return this.ServiceReg.SelectSingleNode("/NetworkNodes/NetworkNodeDetails/BoundingBoxDetails/BoundingCoordinateEast").InnerText;
        }
        /// <summary>
        /// The methods updates boundingbox east point.
        /// </summary>
        /// <param name="east">east</param>
        /// <returns></returns>
        public string SetBoundingCoordinateEast(string east)
        {
            this.ServiceReg.SelectSingleNode("/NetworkNodes/NetworkNodeDetails/BoundingBoxDetails/BoundingCoordinateEast").InnerText = east;
            return "";
        }
        /// <summary>
        /// The method return boundingbox north point
        /// </summary>
        /// <returns>boundingbox north point.</returns>
        public string GetBoundingCoordinateNorth()
        {
            return this.ServiceReg.SelectSingleNode("/NetworkNodes/NetworkNodeDetails/BoundingBoxDetails/BoundingCoordinateNorth").InnerText;
        }
        /// <summary>
        /// The method updates boundingbox north point.
        /// </summary>
        /// <param name="north"></param>
        /// <returns></returns>
        public string SetBoundingCoordinateNorth(string north)
        {
            this.ServiceReg.SelectSingleNode("/NetworkNodes/NetworkNodeDetails/BoundingBoxDetails/BoundingCoordinateNorth").InnerText = north;
            return "";
        }
        /// <summary>
        /// The method returns boundingbox south point.
        /// </summary>
        /// <returns>boundingbox south point.</returns>
        public string GetBoundingCoordinateSouth()
        {
            return this.ServiceReg.SelectSingleNode("/NetworkNodes/NetworkNodeDetails/BoundingBoxDetails/BoundingCoordinateSouth").InnerText;
        }
        /// <summary>
        /// The method updates boundingbox south point.
        /// </summary>
        /// <param name="south">south</param>
        /// <returns></returns>
        public string SetBoundingCoordinateSouth(string south)
        {
            this.ServiceReg.SelectSingleNode("/NetworkNodes/NetworkNodeDetails/BoundingBoxDetails/BoundingCoordinateSouth").InnerText = south;
            return "";
        }
        /// <summary>
        /// The method returns boundingbox west point.
        /// </summary>
        /// <returns>boundingbox west point.</returns>
        public string GetBoundingCoordinateWest()
        {
            return this.ServiceReg.SelectSingleNode("/NetworkNodes/NetworkNodeDetails/BoundingBoxDetails/BoundingCoordinateWest").InnerText;
        }
        /// <summary>
        /// The method updates boundingbox west point. 
        /// </summary>
        /// <param name="west"></param>
        /// <returns></returns>
        public string SetBoundingCoordinateWest(string west)
        {
            this.ServiceReg.SelectSingleNode("/NetworkNodes/NetworkNodeDetails/BoundingBoxDetails/BoundingCoordinateWest").InnerText = west;
            return "";
        }
        /// <summary>
        /// The method return datatable contain all the service related info.
        /// </summary>
        /// <returns>METHOD_NAME,DATA_FLOW,SERVICE_ID,DOCUMENT_URL,SS_URL.</returns>
        public DataTable GetServices()
        {
            DataTable dtServices = new DataTable();
            XmlNodeList services = this.ServiceReg.SelectNodes("/NetworkNodes/NetworkNodeDetails/NodeServiceList/Service");
            if (services != null && services.Count > 0)
            {
                dtServices.Columns.Add("METHOD_NAME");
                dtServices.Columns.Add("DATA_FLOW");
                dtServices.Columns.Add("SERVICE_ID");
                dtServices.Columns.Add("DOCUMENT_URL");
                dtServices.Columns.Add("SS_URL");
                foreach (XmlNode service in services)
                {
                    DataRow dr = dtServices.NewRow();
                    dr["METHOD_NAME"] = service.SelectSingleNode("MethodName").InnerText;
                    dr["DATA_FLOW"] = service.SelectSingleNode("DataFlow").InnerText;
                    dr["SERVICE_ID"] = service.SelectSingleNode("ServiceIdentifier").InnerText;
                    dr["DOCUMENT_URL"] = service.SelectSingleNode("ServiceDocumentURL").InnerText;
                    dr["SS_URL"] = service.SelectSingleNode("StyleSheetURL").InnerText;
                    dtServices.Rows.Add(dr);
                }
            }
            return dtServices;
        }
        /// <summary>
        /// The method returns XmlDocuemnt.
        /// </summary>
        /// <returns>XmlDocument</returns>
        public XmlDocument GetServiceXML()
        {
            return this.ServiceReg;
        }
        /// <summary>
        /// The method returns list of service by method name.
        /// </summary>
        /// <param name="methodName">The method name</param>
        /// <returns>A list of XmlNode related to service.</returns>
        public XmlNodeList GetService(string methodName)
        {
            if (string.IsNullOrEmpty(methodName))
                return this.ServiceReg.SelectNodes("/NetworkNodes/NetworkNodeDetails/NodeServiceList/Service");
            else
                return this.ServiceReg.SelectNodes("/NetworkNodes/NetworkNodeDetails/NodeServiceList/Service[MethodName='" + methodName + "']");
        }
        /// <summary>
        /// The method returns a list of service by method name and dataflow name.
        /// </summary>
        /// <param name="methodName">The name of method.</param>
        /// <param name="dataFlow">The name of dataflow.</param>
        /// <returns>a list of service</returns>
        public XmlNodeList GetService(string methodName, string dataFlow)
        {
            if (string.IsNullOrEmpty(dataFlow))
                return GetService(methodName);
            else
                return this.ServiceReg.SelectNodes("/NetworkNodes/NetworkNodeDetails/NodeServiceList/Service[MethodName='" + methodName + "' and DataFlow/text() = '" + dataFlow + "']");
        }
        /// <summary>
        /// The methods returns service related info by method name and dataflow name.
        /// </summary>
        /// <param name="methodName">The name of method.</param>
        /// <param name="dataFlow">The name of dataflow.</param>
        /// <returns>DataFlow,ServiceIdentifier,ServiceDescription,ServiceDocumentURL,StyleSheetURL.</returns>
        public string[] GetServiceInfo(string methodName, string dataFlow)
        {
            string[] serviceInfo = new string[6];
            XmlNode serviceNode;
            if (string.IsNullOrEmpty(dataFlow))
                serviceNode = this.ServiceReg.SelectSingleNode("/NetworkNodes/NetworkNodeDetails/NodeServiceList/Service[MethodName='" + methodName + "']");
            else
                serviceNode = this.ServiceReg.SelectSingleNode("/NetworkNodes/NetworkNodeDetails/NodeServiceList/Service[MethodName='" + methodName + "' and DataFlow/text() = '" + dataFlow + "']");
            if (serviceNode != null)
            {
                serviceInfo[0] = methodName;
                serviceInfo[1] = serviceNode.SelectSingleNode("DataFlow").InnerText;
                serviceInfo[2] = serviceNode.SelectSingleNode("ServiceIdentifier").InnerText;
                serviceInfo[3] = serviceNode.SelectSingleNode("ServiceDescription").InnerText;
                serviceInfo[4] = serviceNode.SelectSingleNode("ServiceDocumentURL").InnerText;
                serviceInfo[5] = serviceNode.SelectSingleNode("StyleSheetURL").InnerText;
            }
            return serviceInfo;
        }
        /// <summary>
        /// The method returns a service Property info by method name and dataflow.
        /// </summary>
        /// <param name="methodName">The method name</param>
        /// <param name="dataFlow">The dataflow name</param>
        /// <returns>PropertyName,PropertyValue.</returns>
        public string[] GetServicePropertyInfo(string methodName, string dataFlow)
        {
            string[] servicePropInfo = new string[2];
            XmlNode serviceNode = null;
            if (string.IsNullOrEmpty(dataFlow))
                serviceNode = this.ServiceReg.SelectSingleNode("/NetworkNodes/NetworkNodeDetails/NodeServiceList/Service[MethodName='" + methodName + "']");
            else
                serviceNode = this.ServiceReg.SelectSingleNode("/NetworkNodes/NetworkNodeDetails/NodeServiceList/Service[MethodName='" + methodName + "' and DataFlow/text() = '" + dataFlow + "']");
            if (serviceNode != null)
            {
                servicePropInfo[0] = serviceNode.SelectSingleNode("ServiceProperty/PropertyName").InnerText;
                servicePropInfo[1] = serviceNode.SelectSingleNode("ServiceProperty/PropertyValue").InnerText;
            }
            return servicePropInfo;
        }
        /// <summary>
        /// The method returns a data table related service parameters by method name and dataflow.
        /// </summary>
        /// <param name="methodName">The method name</param>
        /// <param name="dataFlow">The dataflow name</param>
        /// <returns>PARAM_NAME,SORT_INDEX,OCCURENCE_NO,ENCODING,PARAM_TYPE,TYPE_DESC,REQUIREDIND,PARAM_VALUE.</returns>
        public DataTable GetServiceParameters(string methodName, string dataFlow)
        {
            DataTable dtParam = new DataTable();
            XmlNode serviceNode = null;
            if (string.IsNullOrEmpty(dataFlow))
                serviceNode = this.ServiceReg.SelectSingleNode("/NetworkNodes/NetworkNodeDetails/NodeServiceList/Service[MethodName='" + methodName + "']");
            else
                serviceNode = this.ServiceReg.SelectSingleNode("/NetworkNodes/NetworkNodeDetails/NodeServiceList/Service[MethodName='" + methodName + "' and DataFlow/text() = '" + dataFlow + "']");

            if (serviceNode != null)
            {
                XmlNodeList paramsNodes = serviceNode.SelectNodes("Parameter");
                if (paramsNodes != null && paramsNodes.Count > 0)
                {
                    dtParam.Columns.Add("PARAM_NAME");
                    dtParam.Columns.Add("SORT_INDEX");
                    dtParam.Columns.Add("OCCURENCE_NO");
                    dtParam.Columns.Add("ENCODING");
                    dtParam.Columns.Add("PARAM_TYPE");
                    dtParam.Columns.Add("TYPE_DESC");
                    dtParam.Columns.Add("REQUIREDIND");
                    dtParam.Columns.Add("PARAM_VALUE");
                    foreach (XmlNode paramNode in paramsNodes)
                    {
                        DataRow dr = dtParam.NewRow();
                        dr["REQUIREDIND"] = paramNode.Attributes["PrameterRequiredIndicator"].Value;
                        dr["PARAM_NAME"] = paramNode.Attributes["ParameterName"].Value;
                        dr["SORT_INDEX"] = paramNode.Attributes["ParameterSortIndex"].Value;
                        dr["OCCURENCE_NO"] = paramNode.Attributes["ParameterOccurenceNumber"].Value;
                        dr["ENCODING"] = paramNode.Attributes["ParameterEncoding"].Value;
                        dr["PARAM_TYPE"] = paramNode.Attributes["ParameterType"].Value;
                        dr["TYPE_DESC"] = paramNode.Attributes["ParameterTypeDescriptor"].Value;
                        dr["PARAM_VALUE"] = paramNode.InnerText;
                        dtParam.Rows.Add(dr);
                    }
                }
            }
            return dtParam;
        }
        /// <summary>
        /// The method remove service based on method name and dataflow.
        /// </summary>
        /// <param name="methodName">The method name.</param>
        /// <param name="dataFlow">The dataflow name.</param>
        /// <returns>Trun if success</returns>
        public bool RemoveService(string methodName, string dataFlow)
        {
            XmlNode existingServiceNode = null;
            if (string.IsNullOrEmpty(dataFlow))
                existingServiceNode = this.ServiceReg.SelectSingleNode("/NetworkNodes/NetworkNodeDetails/NodeServiceList/Service[MethodName/text() = '" + methodName + "']");
            else
                existingServiceNode = this.ServiceReg.SelectSingleNode("/NetworkNodes/NetworkNodeDetails/NodeServiceList/Service[MethodName/text() = '" + methodName + "' and DataFlow/text() = '" + dataFlow + "']");
            if (existingServiceNode != null && existingServiceNode.ParentNode != null)
            {
                existingServiceNode.ParentNode.RemoveChild(existingServiceNode);
                return true;
            }
            return false;
        }
        /// <summary>
        /// The method update current state of service registration XML file.
        /// </summary>
        public void Save()
        {
            new DBManager().GetConfigurationsDB().UpdateServiceRegistration(this.ServiceReg);
        }

        #endregion

        #region Private Fields

        private XmlDocument ServiceReg = null;
        
        #endregion
    }
}
