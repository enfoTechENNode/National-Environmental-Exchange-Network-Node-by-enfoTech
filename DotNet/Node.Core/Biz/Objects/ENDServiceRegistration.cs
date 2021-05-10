using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Node.Core.Data;
using System.Xml.Linq;
using DataFlow.Component.Interface;
using Node.Core.Biz.Manageable;


namespace Node.Core.Biz.Objects
{
    /// <summary>
    /// ENDServiceRegistration is used to access service registration XML file. 
    /// </summary>
    public class ENDSServiceRegistration
    {
        private XDocument ServiceReg;
        private string _version = "2.0";
        /// <summary>
        /// Constructor of ENDS Service Registration.
        /// </summary>
        public ENDSServiceRegistration()
        {
            string inputConfig = new DBManager().GetConfigurationsDB().GetENDSServiceRegistration();
            ServiceReg =  XDocument.Parse(inputConfig);
            InitialENDSRS();
        }
        private void InitialENDSRS()
        {
            XElement xNode = ServiceReg.Descendants("NetworkNodeDetails").Where(x => x.Element("NodeVersionIdentifier").Value == this.NodeVersionIdentifier).First<XElement>();
            NodeIdentifier = xNode.Element("NodeIdentifier").Value;
            NodeName = xNode.Element("NodeName").Value;
            NodeAddress = xNode.Element("NodeAddress").Value;
            OrganizationIdentifier = xNode.Element("OrganizationIdentifier").Value;
            NodeContact = xNode.Element("NodeContact").Value;
            NodeDeploymentTypeCode = xNode.Element("NodeDeploymentTypeCode").Value;
            NodeStatus = xNode.Element("NodeStatus").Value;

            XElement xBound = xNode.Element("BoundingBoxDetails");
            BoundingCoordinateEast = xBound.Element("BoundingCoordinateEast").Value;
            BoundingCoordinateNorth = xBound.Element("BoundingCoordinateNorth").Value;
            BoundingCoordinateSouth = xBound.Element("BoundingCoordinateSouth").Value;
            BoundingCoordinateWest = xBound.Element("BoundingCoordinateWest").Value;
        }

        public string NodeIdentifier { get; set; }
        public string NodeName { get; set; }
        public string NodeAddress { get; set; }
        public string OrganizationIdentifier { get; set; }
        public string NodeContact { get; set; }
        public string NodeDeploymentTypeCode { get; set; }
        public string NodeStatus { get; set; }
        public string BoundingCoordinateEast { get; set; }
        public string BoundingCoordinateNorth { get; set; }
        public string BoundingCoordinateSouth { get; set; }
        public string BoundingCoordinateWest { get; set; }
        public string NodeVersionIdentifier 
        {
            get 
            {
                return _version;
            }
            set 
            {
                _version = value;
                InitialENDSRS();
            }
        }


        public bool Save()
        {
            bool bSave = false;

            XElement xe = ServiceReg.Descendants("NetworkNodeDetails").Where(x => x.Element("NodeVersionIdentifier").Value == NodeVersionIdentifier).First<XElement>();

            xe.Element("NodeIdentifier").Value = NodeIdentifier;
            xe.Element("NodeName").Value = NodeName;
            xe.Element("NodeAddress").Value = NodeAddress;
            xe.Element("OrganizationIdentifier").Value = OrganizationIdentifier;
            xe.Element("NodeContact").Value = NodeContact;
            xe.Element("NodeDeploymentTypeCode").Value = NodeDeploymentTypeCode;
            xe.Element("NodeStatus").Value = NodeStatus;
            xe.Element("BoundingBoxDetails").Element("BoundingCoordinateEast").Value = BoundingCoordinateEast;
            xe.Element("BoundingBoxDetails").Element("BoundingCoordinateNorth").Value = BoundingCoordinateNorth;
            xe.Element("BoundingBoxDetails").Element("BoundingCoordinateSouth").Value = BoundingCoordinateSouth;
            xe.Element("BoundingBoxDetails").Element("BoundingCoordinateWest").Value = BoundingCoordinateWest;


            StringBuilder sb = new StringBuilder();
            sb.AppendLine(ServiceReg.Declaration.ToString());
            sb.Append(this.ServiceReg.ToString());
            bSave = new DBManager().GetConfigurationsDB().UpdateENDSServiceRegistration(sb.ToString());

            return bSave;
        }

        public string BuildENDS()
        {
            DataTable dt = new DBManager().GetGetServicesDB().GetServiceForENDS();
            XElement NodeServiceList1 = new XElement("NodeServiceList");
            XElement NodeServiceList2 = new XElement("NodeServiceList");

            XElement Node1 = this.ServiceReg.Descendants("NetworkNodeDetails").Where(x => x.Element("NodeVersionIdentifier").Value == "1.1").First<XElement>();
            Node1.Add(NodeServiceList1);
            XElement Node2 = this.ServiceReg.Descendants("NetworkNodeDetails").Where(x => x.Element("NodeVersionIdentifier").Value == "2.0").First<XElement>();
            Node2.Add(NodeServiceList2);

            if (dt != null)
            {
                XElement opNode = null;
                foreach (DataRow aRow in dt.Rows)
                {
                    Operation op = new Operation((int)aRow["OPERATION_ID"]);

                    if (op.Version.Trim().ToUpper() == "VER_11")
                    {
                        opNode = NodeServiceList1;
                    }
                    else
                    {
                        opNode = NodeServiceList2;
                    }

                    XElement service = new XElement("Service");
                    XElement xe = new XElement("MethodName");

                    string sWSName = "";
                    switch (op.WebServiceName.Trim().ToUpper())
                    {
                        case "QUERY":
                            sWSName = "Query";
                            break;
                        case "SOLICIT":
                            sWSName = "Solicit";
                            break;
                        default:
                            sWSName = op.WebServiceName;
                            break;
                    }

                    xe.Value = "" + sWSName;

                    service.Add(xe);

                    xe = new XElement("Dataflow");
                    xe.Value = ""+op.DomainName;
                    service.Add(xe);

                    xe = new XElement("ServiceIdentifier");
                    xe.Value = ""+op.Name;
                    service.Add(xe);

                    xe = new XElement("ServiceDescription");
                    xe.Value = ""+op.Description;
                    service.Add(xe);

                    if (op.Config.DocumentElement.Name.ToUpper() == "PROCESS")
                    {
                        DllManager dllMgr = new DllManager();
                        IActionProcess process = dllMgr.GetActionProcess();
                        IActionOperation actionOp = process.GetActionOperation(op.Config.OuterXml);
                        for (int i = 0; i < actionOp.Variables.Count; i++)
                        {
                            IActionParameter param = (IActionParameter)actionOp.Variables[i];

                            xe = new XElement("Parameter");
                            XAttribute xa = new XAttribute("ParameterName", "" + param.ParameterName);
                            xe.Add(xa);
                            xa = new XAttribute("ParameterType", "" + param.DEDLType);
                            xe.Add(xa);
                            xa = new XAttribute("ParameterTypeDescriptor", "" + param.DEDLTypeDescriptor);
                            xe.Add(xa);

                            if ("" + param.DEDLOccurenceNumber != "")
                            {
                                xa = new XAttribute("ParameterEncoding", "" + param.DEDLEncoding);
                                xe.Add(xa);
                            }
                            if ("" + param.DEDLOccurenceNumber  != "")
                            {
                                xa = new XAttribute("ParameterOccurrenceNumber", "" + param.DEDLOccurenceNumber);
                                xe.Add(xa);
                            }
                            if ("" + param.DEDLRequiredIndicator != "")
                            {
                                xa = new XAttribute("ParameterRequiredIndicator", "" + param.DEDLRequiredIndicator);
                                xe.Add(xa);
                            }


                            service.Add(xe);
                        }

                    }
                    else
                    {
                        if (op.Parameters != null && op.Parameters.Count > 0)
                        {
                            for (int i = 0; i < op.Parameters.Count; i++)
                            {
                                OpParameter param = (OpParameter)op.Parameters[i];

                                xe = new XElement("Parameter");
                                XAttribute xa = new XAttribute("ParameterName", "" + param.Name);
                                xe.Add(xa);
                                xa = new XAttribute("ParameterType", "" + param.DEDLType);
                                xe.Add(xa);
                                xa = new XAttribute("ParameterTypeDescriptor", "" + param.DEDLTypeDescriptor);
                                xe.Add(xa);

                                if ("" + param.DEDLOccurenceNumber != "")
                                {
                                    xa = new XAttribute("ParameterEncoding", "" + param.DEDLEncoding);
                                    xe.Add(xa);
                                }
                                if ("" + param.DEDLOccurenceNumber != "")
                                {
                                    xa = new XAttribute("ParameterOccurrenceNumber", "" + param.DEDLOccurenceNumber);
                                    xe.Add(xa);
                                }
                                if ("" + param.DEDLRequiredIndicator != "")
                                {
                                    xa = new XAttribute("ParameterRequiredIndicator", "" + param.DEDLRequiredIndicator);
                                    xe.Add(xa);
                                }

                                service.Add(xe);
                            }
                        }
                    }
                    opNode.Add(service);
                }
            }

            return this.ServiceReg.Declaration + Environment.NewLine + this.ServiceReg.ToString().Replace("xmlns=\"\"",""); 
        }
        
    }
}
