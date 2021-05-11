using System;
using System.Collections;
using System.Data;
using System.Xml;

using Node.Lib.AppSystem;
using Node.Lib.Security;
using Node.Core;
using Node.Core.Data;
using Node.Core.Data.Interfaces;

namespace Node.Core.Biz.Objects
{
    /// <summary>
    /// Operation Class retrieves operation related Informatuion.
    /// Database Source: NODE_OPERATION.
    /// </summary>
    public class Operation
    {
        #region Public Properties
        /// <summary>
        /// Identifier of Operation.
        /// </summary>
        public int ID
        {
            get { return this.id; }
            set
            {
                if (value >= 0)
                    this.id = value;
                else
                    this.id = -1;
            }
        }
        /// <summary>
        /// DomainID of Operation.
        /// </summary>
        public int DomainID
        {
            get { return this.domainID; }
            set
            {
                if (value >= 0)
                    this.domainID = value;
                else
                    this.domainID = -1;
            }
        }
        /// <summary>
        /// DomainName of Operation.
        /// </summary>
        public string DomainName
        {
            get { return this.domainName; }
            set
            {
                string input = value;
                if (value == null || value.Trim().Equals(""))
                    input = null;
                else if (value.Length > 50)
                    input = value.Substring(0, 50);
                this.domainName = input;
            }
        }
        /// <summary>
        /// DomainStatus of Operation.
        /// </summary>
        public string DomainStatus
        {
            get
            {
                if (this.domainStatus == null)
                    return Phrase.STATUS_STOPPED;
                else
                    return this.domainStatus;
            }
            set
            {
                if (value != null)
                {
                    if (value.Equals(Phrase.STATUS_RUNNING) || value.Equals(Phrase.STATUS_STOPPED))
                        this.domainStatus = value;
                }
                else
                    this.domainStatus = Phrase.STATUS_STOPPED; 
            }
        }
        /// <summary>
        /// WebServiceID of Operation.
        /// </summary>
        public int WebServiceID
        {
            get { return this.wsID; }
            set
            {
                if (value >= 0)
                    this.wsID = value;
                else
                    this.wsID = -1;
            }
        }
        /// <summary>
        /// WebServiceName of Operation.
        /// </summary>
        public string WebServiceName
        {
            get { return this.wsName; }
            set
            {
                if (value != null)
                {
                    string temp = value.ToUpper();
                    if (temp.Equals(Phrase.WEB_SERVICE_AUTHENTICATE) ||
                        temp.Equals(Phrase.WEB_SERVICE_DOWNLOAD) ||
                        temp.Equals(Phrase.WEB_SERVICE_GETSERVICES) ||
                        temp.Equals(Phrase.WEB_SERVICE_GETSTATUS) ||
                        temp.Equals(Phrase.WEB_SERVICE_NODEPING) ||
                        temp.Equals(Phrase.WEB_SERVICE_NOTIFY) ||
                        temp.Equals(Phrase.WEB_SERVICE_QUERY) ||
                        temp.Equals(Phrase.WEB_SERVICE_SOLICIT) ||
                        temp.Equals(Phrase.WEB_SERVICE_EXECUTE) ||
                        temp.Equals(Phrase.WEB_SERVICE_SUBMIT))
                        this.wsName = temp;
                    else
                        this.wsName = null;
                }
                else
                    this.wsName = null;
            }
        }
        /// <summary>
        /// Name of Operation.
        /// </summary>
        public string Name
        {
            get { return this.name; }
            set
            {
                string input = value;
                if (value == null || value.Trim().Equals(""))
                    input = null;
                else if (value.Length > 50)
                    input = value.Substring(0, 50);
                this.name = input;
            }
        }
        /// <summary>
        /// Description of Operation.
        /// </summary>
        public string Description
        {
            get { return this.description; }
            set
            {
                string input = value;
                if (value == null || value.Trim().Equals(""))
                    input = null;
                else if (value.Length > 255)
                    input = value.Substring(0, 255);
                this.description = input;
            }
        }
        /// <summary>
        /// Type of Operation.
        /// </summary>
        public string Type
        {
            get { return this.type; }
            set
            {
                if (value != null && (value.Equals(Phrase.OPERATION_TYPE_WEB_SERVICE) || value.Equals(Phrase.OPERATION_TYPE_SCHEDULED_TASK)))
                    this.type = value;
                else
                    this.type = null;
            }
        }
        /// <summary>
        /// Version of Operation.
        /// </summary>
        public string Version
        {
            get { return this.version; }
            set { this.version = value; }
        }
        /// <summary>
        /// Config of Operation.
        /// </summary>
        public XmlDocument Config
        {
            get { return this.config; }
            set
            {
                this.config = value; 
                //if (value != null)
                //{
                //    XmlDocument temp = value;
                //    XmlNode node = temp.SelectSingleNode("/Operation");
                //    if (node != null)
                //    {
                //        this.config = value;
                //    }
                //    else
                //        this.config = null;
                //}
                //else
                //    this.config = null;
            }
        }
        /// <summary>
        /// Status of Operation.
        /// </summary>
        public string Status
        {
            get { return this.status; }
            set
            {
                if (value != null)
                {
                    if (value.Equals(Phrase.STATUS_RUNNING) || value.Equals(Phrase.STATUS_STOPPED))
                        this.status = value;
                    else
                        this.status = Phrase.STATUS_STOPPED;
                }
                else
                    this.status = Phrase.STATUS_STOPPED;
            }
        }
        /// <summary>
        /// Message of Operation.
        /// </summary>
        public string Message
        {
            get { return this.message; }
            set
            {
                string input = value;
                if (value == null || value.Trim().Equals(""))
                    input = null;
                else if (value.Length > 1000)
                    input = value.Substring(0, 1000);
                this.message = input;
            }
        }
        /// <summary>
        /// PreProcesses of Operation.
        /// </summary>
        public ArrayList PreProcesses
        {
            get
            {
                ArrayList retList = new ArrayList();
                if (this.config == null || this.config.SelectSingleNode("/Operation/PreProcess") == null || this.wsName == null)
                    return retList;
                XmlNodeList preProcNodes = this.config.SelectNodes("/Operation/PreProcess/Sequence");
                foreach (XmlNode preProcNode in preProcNodes)
                {
                    string className = preProcNode.SelectSingleNode("ClassName").InnerText;
                    string dllPath = preProcNode.SelectSingleNode("DllName").InnerText;
                    int sequence = int.Parse(preProcNode.Attributes["number"].Value);
                    retList.Add(new OpProcess(ProcessType.PRE_PROCESS, className, dllPath, this.wsName, sequence));
                }
                return retList;
            }
        }
        /// <summary>
        /// Process of Operation.
        /// </summary>
        public OpProcess Process
        {
            get
            {
                if (this.config == null || this.config.SelectSingleNode("/Operation/Process") == null)
                    return null;
                string className = this.config.SelectSingleNode("/Operation/Process/ClassName").InnerText;
                string dllPath = this.config.SelectSingleNode("/Operation/Process/DllName").InnerText;
                OpProcess proc = null;
                if (this.wsName != null)
                    proc = new OpProcess(ProcessType.PROCESS, className, dllPath, this.wsName);
                else
                    proc = new OpProcess(ProcessType.PROCESS, className, dllPath);
                XmlNode solicitNode = this.config.SelectSingleNode("/Operation/Process/Solicit");
                if (solicitNode != null)
                {
                    XmlNode startNode = solicitNode.SelectSingleNode("SolicitStartTime");
                    XmlNode endNode = solicitNode.SelectSingleNode("SolicitEndTime");
                    if (startNode != null && endNode != null)
                    {
                        proc.IsSolicitRestrictedToTimeInterval = true;
                        if (!startNode.InnerText.Trim().Equals(""))
                            proc.SolicitStartTime = startNode.InnerText;
                        if (!endNode.InnerText.Trim().Equals(""))
                            proc.SolicitEndTime = endNode.InnerText;
                    }
                    XmlNode submitNode = solicitNode.SelectSingleNode("SubmitCredentials");
                    if (submitNode != null)
                    {
                        proc.HasSolicitSubmitCredentials = true;
                        XmlNode uidNode = submitNode.SelectSingleNode("UserID");
                        if (uidNode != null && !uidNode.InnerText.Trim().Equals(""))
                            proc.SolicitSubmitUID = uidNode.InnerText;
                        XmlNode pwdNode = submitNode.SelectSingleNode("Password");
                        if (pwdNode != null && !pwdNode.InnerText.Trim().Equals(""))
                            proc.SolicitSubmitPWD = new Cryptography().Decrypting(pwdNode.InnerText, Phrase.CryptKey);
                        XmlNode dataFlowNode = submitNode.SelectSingleNode("DataFlowName");
                        if (dataFlowNode != null && !dataFlowNode.InnerText.Trim().Equals(""))
                            proc.SolicitSubmitDataFlow = dataFlowNode.InnerText;
                    }
                }
                return proc;
            }
            set
            {
                if (this.config == null)
                    this.config = new XmlDocument();
                XmlNode operationNode = this.config.SelectSingleNode("/Operation");
                if (operationNode == null)
                {
                    operationNode = this.config.CreateElement("Operation");
                    this.config.AppendChild(operationNode);
                }
                XmlNode processNode = operationNode.SelectSingleNode("Process");
                if (processNode == null)
                {
                    processNode = this.config.CreateElement("Process");
                    XmlNode postProcessNode = operationNode.SelectSingleNode("PostProcess");
                    if (postProcessNode != null)
                        operationNode.InsertBefore(processNode, postProcessNode);
                    else
                        operationNode.AppendChild(processNode);
                }
                XmlNode classNameNode = processNode.SelectSingleNode("ClassName");
                if (classNameNode == null)
                {
                    classNameNode = this.config.CreateElement("ClassName");
                    processNode.AppendChild(classNameNode);
                }
                classNameNode.InnerText = value.ClassName;
                XmlNode dllPathNode = processNode.SelectSingleNode("DllName");
                if (dllPathNode == null)
                {
                    dllPathNode = this.config.CreateElement("DllName");
                    processNode.AppendChild(dllPathNode);
                }
                dllPathNode.InnerText = value.DllPath;
                if (value.WebServiceName != null && value.WebServiceName.Equals(Phrase.WEB_SERVICE_SOLICIT))
                {
                    XmlNode solicitNode = processNode.SelectSingleNode("Solicit");
                    if (solicitNode == null)
                    {
                        solicitNode = this.config.CreateElement("Solicit");
                        processNode.AppendChild(solicitNode);
                    }
                    if (value.IsSolicitRestrictedToTimeInterval)
                    {
                        XmlNode startNode = solicitNode.SelectSingleNode("SolicitStartTime");
                        if (startNode == null)
                        {
                            startNode = this.config.CreateElement("SolicitStartTime");
                            solicitNode.AppendChild(startNode);
                        }
                        startNode.InnerText = value.SolicitStartTime;
                        XmlNode endNode = solicitNode.SelectSingleNode("SolicitEndTime");
                        if (endNode == null)
                        {
                            endNode = this.config.CreateElement("SolicitEndTime");
                            solicitNode.AppendChild(endNode);
                        }
                        endNode.InnerText = value.SolicitEndTime;
                    }
                    else
                    {
                        XmlNode startNode = solicitNode.SelectSingleNode("SolicitStartTime");
                        if (startNode != null)
                            solicitNode.RemoveChild(startNode);
                        XmlNode endNode = solicitNode.SelectSingleNode("SolicitEndTime");
                        if (endNode != null)
                            solicitNode.RemoveChild(endNode);
                    }
                    if (value.HasSolicitSubmitCredentials)
                    {
                        XmlNode submitNode = solicitNode.SelectSingleNode("SubmitCredentials");
                        if (submitNode == null)
                        {
                            submitNode = this.config.CreateElement("SubmitCredentials");
                            solicitNode.AppendChild(submitNode);
                        }
                        XmlNode uidNode = submitNode.SelectSingleNode("UserID");
                        if (uidNode == null)
                        {
                            uidNode = this.config.CreateElement("UserID");
                            submitNode.AppendChild(uidNode);
                        }
                        uidNode.InnerText = value.SolicitSubmitUID != null && !value.SolicitSubmitUID.Trim().Equals("") ? value.SolicitSubmitUID : "";
                        XmlNode pwdNode = submitNode.SelectSingleNode("Password");
                        if (pwdNode == null)
                        {
                            pwdNode = this.config.CreateElement("Password");
                            submitNode.AppendChild(pwdNode);
                        }
                        if (value.SolicitSubmitPWD != null && !value.SolicitSubmitPWD.Trim().Equals(""))
                            pwdNode.InnerText = new Cryptography().Encrypting(value.SolicitSubmitPWD, Phrase.CryptKey);
                        else
                            pwdNode.InnerText = "";
                        XmlNode dataFlowNode = submitNode.SelectSingleNode("DataFlowName");
                        if (dataFlowNode == null)
                        {
                            dataFlowNode = this.config.CreateElement("DataFlowName");
                            submitNode.AppendChild(dataFlowNode);
                        }
                        dataFlowNode.InnerText = value.SolicitSubmitDataFlow != null && !value.SolicitSubmitDataFlow.Trim().Equals("") ? value.SolicitSubmitDataFlow : "";
                    }
                }
            }
        }
        /// <summary>
        /// PostProcesses of Operation.
        /// </summary>
        public ArrayList PostProcesses
        {
            get
            {
                ArrayList retList = new ArrayList();
                if (this.config == null || this.config.SelectSingleNode("/Operation/PostProcess") == null || this.wsName == null)
                    return retList;
                XmlNodeList postProcNodes = this.config.SelectNodes("/Operation/PostProcess/Sequence");
                foreach (XmlNode postProcNode in postProcNodes)
                {
                    string className = postProcNode.SelectSingleNode("ClassName").InnerText;
                    string dllPath = postProcNode.SelectSingleNode("DllName").InnerText;
                    int sequence = int.Parse(postProcNode.Attributes["number"].Value);
                    retList.Add(new OpProcess(ProcessType.POST_PROCESS, className, dllPath, this.wsName, sequence));
                }
                return retList;
            }
        }
        /// <summary>
        /// Parameters of Operation.
        /// </summary>
        public ArrayList Parameters
        {
            get
            {
                ArrayList retList = new ArrayList();
                if (this.config == null || this.config.SelectSingleNode("/Operation/Parameters/Parameter") == null)
                    return retList;
                XmlNodeList parametersList = this.config.SelectNodes("/Operation/Parameters/Parameter");
                foreach (XmlNode parameterNode in parametersList)
                {
                    OpParameter param = null;

                    if (parameterNode.Attributes.Count == 0)
                    {
                        XmlAttribute xa = this.config.CreateAttribute("type");
                        xa.Value = "";
                        parameterNode.Attributes.Append(xa);
                        xa = this.config.CreateAttribute("encoding");
                        xa.Value = "";
                        parameterNode.Attributes.Append(xa);
                        xa = this.config.CreateAttribute("occurrencenumber");
                        xa.Value = "";
                        parameterNode.Attributes.Append(xa);
                        xa = this.config.CreateAttribute("requiredindicator");
                        xa.Value = "";
                        parameterNode.Attributes.Append(xa);
                        xa = this.config.CreateAttribute("typeDescriptor");
                        xa.Value = "";
                        parameterNode.Attributes.Append(xa);
                    }

                    string name = parameterNode.SelectSingleNode("Name").InnerText;

                    if (parameterNode.SelectSingleNode("Value") != null && !parameterNode.SelectSingleNode("Value").InnerText.Trim().Equals(""))
                        param = new OpParameter(name, parameterNode.SelectSingleNode("Value").InnerText);
                    else
                        param = new OpParameter(name);

                    param.DEDLType = parameterNode.Attributes["type"].Value;
                    param.DEDLEncoding = parameterNode.Attributes["encoding"].Value;
                    param.DEDLOccurenceNumber = parameterNode.Attributes["occurrencenumber"].Value;
                    param.DEDLRequiredIndicator = parameterNode.Attributes["requiredindicator"].Value;
                    param.DEDLTypeDescriptor = parameterNode.Attributes["typeDescriptor"].Value;

                    retList.Add(param);
                }
                return retList;
            }
        }
        /// <summary>
        /// EmailReceivers of Operation.
        /// </summary>
        public ArrayList EmailReceivers
        {
            get
            {
                ArrayList retList = new ArrayList();
                if (this.config == null || this.config.SelectSingleNode("/Operation/EmailAddresses/Email") == null)
                    return retList;
                XmlNodeList emailList = this.config.SelectNodes("/Operation/EmailAddresses/Email");
                foreach (XmlNode email in emailList)
                    retList.Add(email.InnerText);
                return retList;
            }
        }
        /// <summary>
        /// TaskSchedule of Operation.
        /// </summary>
        public TaskSchedule TaskSchedule
        {
            get { return this.taskSchedule; }
            set { this.taskSchedule = value; }
        }
        /// <summary>
        /// Task of Operation.
        /// </summary>
        public Task Task
        {
            get { return this.task; }
            set { this.task = value; }
        }
        /// <summary>
        /// CreatedDate of Operation.
        /// </summary>
        public DateTime CreatedDate
        {
            get { return this.createdDate; }
            set { this.createdDate = value; }
        }
        /// <summary>
        /// CreatedBy of Operation.
        /// </summary>
        public string CreatedBy
        {
            get { return this.createdBy; }
            set
            {
                string input = value;
                if (value == null || value.Trim().Equals(""))
                    input = null;
                else if (value.Length > 50)
                    input = value.Substring(0, 50);
                this.createdBy = input;
            }
        }
        /// <summary>
        /// UpdatedDate of Operation.
        /// </summary>
        public DateTime UpdatedDate
        {
            get { return this.updatedDate; }
            set { this.updatedDate = value; }
        }
        /// <summary>
        /// UpdatedBy of Operation.
        /// </summary>
        public string UpdatedBy
        {
            get { return this.updatedBy; }
            set
            {
                string input = value;
                if (value == null || value.Trim().Equals(""))
                    input = null;
                else if (value.Length > 50)
                    input = value.Substring(0, 50);
                this.updatedBy = input;
            }
        }
        /// <summary>
        /// Publish Indicator of Operation.
        /// </summary>
        public string PublishInd{get;set;}
        /// <summary>
        /// RESTFul Indicator of Operation.
        /// </summary>
        public string RESTInd { get; set; }
        #endregion

        #region Public Constructors
        /// <summary>
        /// Constructor of Operation.
        /// </summary>
        public Operation()
        {
        }
        /// <summary>
        /// Constructor of Operation.
        /// </summary>
        /// <param name="opID">Operation ID.</param>
        public Operation(int opID)
        {
            this.id = opID;
            this.Init(new DBManager().GetOperationsDB().GetOperation(opID));
        }
        /// <summary>
        /// Constructor of Operation.
        /// </summary>
        /// <param name="opName">The name of opertion.</param>
        public Operation(string opName)
        {
            this.Name = opName;
            this.Init(new DBManager().GetOperationsDB().GetOperation(opName));
        }
        /// <summary>
        /// Constructor of Operation.
        /// </summary>
        /// <param name="opName">The name of opertion.</param>
        /// <param name="wsName">The name of web method.</param>
        public Operation(string opName, string wsName)
        {
            this.Name = opName;
            this.WebServiceName = wsName;
            this.Init(new DBManager().GetOperationsDB().GetOperation(opName, wsName));
        }

        #endregion

        #region Public Methods
        /// <summary>
        /// Add PreProcess configuration.
        /// </summary>
        /// <param name="pre">OpProcess</param>
        public void AddPreProcess(OpProcess pre)
        {
            if (this.config == null)
                this.config = new XmlDocument();
            XmlNode operationNode = this.config.SelectSingleNode("/Operation");
            if (operationNode == null)
            {
                operationNode = this.config.CreateElement("Operation");
                this.config.AppendChild(operationNode);
            }
            XmlNode preProcess = operationNode.SelectSingleNode("PreProcess");
            if (preProcess == null)
            {
                preProcess = this.config.CreateElement("PreProcess");
                operationNode.AppendChild(preProcess);
            }
            XmlNodeList preProcesses = preProcess.SelectNodes("Sequence");
            int i = 0;
            for (; i < preProcesses.Count && i + 1 < pre.Sequence; i++)
                ;
            XmlNode preProcessToMove = null;
            if (i < preProcesses.Count)
                preProcessToMove = preProcesses.Item(i);
            XmlElement sequence = this.config.CreateElement("Sequence");
            XmlAttribute numAttr = this.config.CreateAttribute("number");
            numAttr.Value = "" + pre.Sequence;
            sequence.Attributes.Append(numAttr);
            XmlElement className = this.config.CreateElement("ClassName");
            className.InnerText = pre.ClassName;
            sequence.AppendChild(className);
            XmlElement dllPath = this.config.CreateElement("DllName");
            dllPath.InnerText = pre.DllPath;
            sequence.AppendChild(dllPath);
            if (preProcessToMove != null)
                preProcess.InsertBefore(sequence, preProcessToMove);
            else
                preProcess.AppendChild(sequence);
            i++;
            for (; i < preProcesses.Count; i++)
            {
                int temp = i + 1;
                preProcesses.Item(i).Attributes["number"].Value = "" + temp;
            }
        }
        /// <summary>
        /// Remove PreProcess configuration.
        /// </summary>
        public void RemovePreProcesses()
        {
            if (this.config != null)
            {
                XmlNode preProcess = this.config.SelectSingleNode("/Operation/PreProcess");
                if (preProcess != null)
                    preProcess.ParentNode.RemoveChild(preProcess);
            }
        }
        /// <summary>
        /// Add PostProcess configuration.
        /// </summary>
        /// <param name="post">OpProcess</param>
        public void AddPostProcess(OpProcess post)
        {
            if (this.config == null)
                this.config = new XmlDocument();
            XmlNode operationNode = this.config.SelectSingleNode("/Operation");
            if (operationNode == null)
            {
                operationNode = this.config.CreateElement("Operation");
                this.config.AppendChild(operationNode);
            }
            XmlNode postProcess = operationNode.SelectSingleNode("PostProcess");
            if (postProcess == null)
            {
                postProcess = this.config.CreateElement("PostProcess");
                operationNode.AppendChild(postProcess);
            }
            XmlNodeList postProcesses = postProcess.SelectNodes("Sequence");
            int i = 0;
            for (; i < postProcesses.Count && i + 1 < post.Sequence; i++)
                ;
            XmlNode postProcessToMove = null;
            if (i < postProcesses.Count)
                postProcessToMove = postProcesses.Item(i);
            XmlElement sequence = this.config.CreateElement("Sequence");
            XmlAttribute numAttr = this.config.CreateAttribute("number");
            numAttr.Value = "" + post.Sequence;
            sequence.Attributes.Append(numAttr);
            XmlElement className = this.config.CreateElement("ClassName");
            className.InnerText = post.ClassName;
            sequence.AppendChild(className);
            XmlElement dllPath = this.config.CreateElement("DllName");
            dllPath.InnerText = post.DllPath;
            sequence.AppendChild(dllPath);
            if (postProcessToMove != null)
                postProcess.InsertBefore(sequence, postProcessToMove);
            else
                postProcess.AppendChild(sequence);
            i++;
            for (; i < postProcesses.Count; i++)
            {
                int temp = i + 1;
                postProcesses.Item(i).Attributes["number"].Value = "" + temp;
            }
        }
        /// <summary>
        /// Remove PostProcess configuration.
        /// </summary>
        public void RemovePostProcesses()
        {
            if (this.config != null)
            {
                XmlNode postProcess = this.config.SelectSingleNode("/Operation/PostProcess");
                if (postProcess != null)
                    postProcess.ParentNode.RemoveChild(postProcess);
            }
        }
        /// <summary>
        /// Add Parameter configuration.
        /// </summary>
        /// <param name="param">OpParameter</param>
        public void AddParameter(OpParameter param)
        {
            if (this.config == null)
                this.config = new XmlDocument();
            XmlNode operationNode = this.config.SelectSingleNode("/Operation");
            if (operationNode == null)
            {
                operationNode = this.config.CreateElement("Operation");
                this.config.AppendChild(operationNode);
            }
            XmlNode parametersNode = operationNode.SelectSingleNode("Parameters");
            if (parametersNode == null)
            {
                parametersNode = this.config.CreateElement("Parameters");
                operationNode.AppendChild(parametersNode);
            }
            XmlElement newParam = this.config.CreateElement("Parameter");
            
            XmlAttribute xa = this.config.CreateAttribute("type");
            xa.Value = param.DEDLType;
            newParam.Attributes.Append(xa);
            xa = this.config.CreateAttribute("encoding");
            xa.Value = param.DEDLEncoding;
            newParam.Attributes.Append(xa);
            xa = this.config.CreateAttribute("occurrencenumber");
            xa.Value = param.DEDLOccurenceNumber;
            newParam.Attributes.Append(xa);
            xa = this.config.CreateAttribute("requiredindicator");
            xa.Value = param.DEDLRequiredIndicator;
            newParam.Attributes.Append(xa);
            xa = this.config.CreateAttribute("typeDescriptor");
            xa.Value = param.DEDLRequiredIndicator;
            newParam.Attributes.Append(xa);

            XmlElement newName = this.config.CreateElement("Name");
            newName.InnerText = param.Name;
            newParam.AppendChild(newName);
            XmlElement newValue = this.config.CreateElement("Value");
            newValue.InnerText = param.Value;
            newParam.AppendChild(newValue);
            parametersNode.AppendChild(newParam);



        }
        /// <summary>
        /// Remove Parameter configuration
        /// </summary>
        public void RemoveParameters()
        {
            if (this.config != null)
            {
                XmlNode operationNode = this.config.SelectSingleNode("/Operation");
                if (operationNode != null)
                {
                    XmlNode parametersNode = operationNode.SelectSingleNode("Parameters");
                    if (parametersNode != null)
                        operationNode.RemoveChild(parametersNode);
                }
            }
        }
        /// <summary>
        /// Add Email Receiever.
        /// </summary>
        /// <param name="email">the email string</param>
        public void AddEmailReceiver(string email)
        {
            if (this.config == null)
                this.config = new XmlDocument();
            XmlNode operationNode = this.config.SelectSingleNode("/Operation");
            if (operationNode == null)
            {
                operationNode = this.config.CreateElement("Operation");
                this.config.AppendChild(operationNode);
            }
            XmlNode emailAddressesNode = operationNode.SelectSingleNode("EmailAddresses");
            if (emailAddressesNode == null)
            {
                emailAddressesNode = this.config.CreateElement("EmailAddresses");
                operationNode.AppendChild(emailAddressesNode);
            }
            XmlElement emailNode = this.config.CreateElement("Email");
            emailNode.InnerText = email;
            emailAddressesNode.AppendChild(emailNode);
        }
        /// <summary>
        /// Remove Email Receiver.
        /// </summary>
        public void RemoveEmailReceivers()
        {
            if (this.config != null)
            {
                XmlNode operationNode = this.config.SelectSingleNode("/Operation");
                if (operationNode != null)
                {
                    XmlNode emailAddressesNode = operationNode.SelectSingleNode("EmailAddresses");
                    if (emailAddressesNode != null)
                        operationNode.RemoveChild(emailAddressesNode);
                }
            }
        }
        /// <summary>
        /// Save Operation.
        /// </summary>
        /// <param name="domainAdmin">The domain admin account</param>
        /// <returns>Error message if fail.</returns>
        public string Save(string domainAdmin)
        {
            IOperations opDB = new DBManager().GetOperationsDB();
            if (this.ID < 0)
            {
                Operation op = null;
                if (this.Type == Phrase.OPERATION_TYPE_WEB_SERVICE)
                    op = opDB.GetOperation(this.Name, this.WebServiceName);
                else
                    op = opDB.GetOperation(this.Name);
                if (op != null)
                    return "Please Select a different name.";
            }
            new DBManager().GetOperationsDB().SaveOperation(this, domainAdmin);
            return null;
        }
        /// <summary>
        /// Delete Operation.
        /// </summary>
        /// <returns>Error message if fail.</returns>
        public string Delete()
        {
            IOperations opDB = new DBManager().GetOperationsDB();
            if (this.ID < 0)
            {
                Operation op = null;
                if (this.Type == Phrase.OPERATION_TYPE_WEB_SERVICE)
                    op = opDB.GetOperation(this.Name, this.WebServiceName);
                else
                    op = opDB.GetOperation(this.Name);
                if (op != null)
                    return "Please Select a different name.";
            }
            new DBManager().GetOperationsDB().DeleteOperation(this);
            return null;
        }

        #endregion

        #region Public Static Methods
        /// <summary>
        /// Get List of Domains that can be Used in GetServices Calls on this Node
        /// </summary>
        /// <returns>ArrayList of Domain Name</returns>
        public static ArrayList RetrieveGetServicesOperationNames()
        {
            IGetServices gsDB = new DBManager().GetGetServicesDB();
            return gsDB.RetrieveGetServicesOperationNames();
        }
        /// <summary>
        /// Get names of request under solicit web service.
        /// </summary>
        /// <returns>Array of request names</returns>
        public static ArrayList RetrieveSolicitOperationNames()
        {
            IOperations opDB = new DBManager().GetOperationsDB();
            string[] names = opDB.GetSolicitNames();
            ArrayList retList = new ArrayList();
            if (names != null && names.Length > 0)
                foreach (string s in names)
                    retList.Add(s);
            return retList;
        }
        /// <summary>
        /// Get names of request under solicit web service and specified node version.
        /// </summary>
        /// <param name="version">Node Version. The value can be <see cref="Node.Core.Phrase.VERSION_11">VER_11</see> or <see cref="Node.Core.Phrase.VERSION_20">VER_20</see></param>
        /// <returns>Array of request names</returns>
        public static ArrayList RetrieveSolicitOperationNames(string version)
        {
            IOperations opDB = new DBManager().GetOperationsDB();
            string[] names = opDB.GetSolicitNames(version);
            ArrayList retList = new ArrayList();
            if (names != null && names.Length > 0)
                foreach (string s in names)
                    retList.Add(s);
            return retList;
        }
        /// <summary>
        /// Get a list of parameters under solicit web service.
        /// </summary>
        /// <returns>a Hashtable contain key/value pair</returns>
        public static Hashtable RetrieveSolicitParameterNames()
        {
            IOperations opDB = new DBManager().GetOperationsDB();
            return opDB.GetSolicitNameParameterPairs();
        }
        /// <summary>
        /// Get names of request under query web service.
        /// </summary>
        /// <returns>Array of request names</returns>
        public static ArrayList RetrieveQueryOperationNames()
        {
            IOperations opDB = new DBManager().GetOperationsDB();
            string[] names = opDB.GetQueryNames();
            ArrayList retList = new ArrayList();
            if (names != null && names.Length > 0)
                foreach (string s in names)
                    retList.Add(s);
            return retList;
        }
        /// <summary>
        /// Get names of request under query web service and specified node version.
        /// </summary>
        /// <returns>Array of request names</returns>
        public static ArrayList RetrieveQueryOperationNames(string version)
        {
            IOperations opDB = new DBManager().GetOperationsDB();
            string[] names = opDB.GetQueryNames(version);
            ArrayList retList = new ArrayList();
            if (names != null && names.Length > 0)
                foreach (string s in names)
                    retList.Add(s);
            return retList;
        }
        /// <summary>
        /// Get a list of parameters under query web service.
        /// </summary>
        /// <returns>a Hashtable contain key/value pair</returns>
        public static Hashtable RetrieveQueryParameterNames()
        {
            IOperations opDB = new DBManager().GetOperationsDB();
            return opDB.GetQueryNameParameterPairs();
        }
        /// <summary>
        /// Get unique opeation name under specified Domain
        /// </summary>
        /// <param name="domainAdmin">Name of Domain</param>
        /// <returns>Array of operation names</returns>
        public static ArrayList GetUniqueOperationNames(string domainAdmin)
        {
            IOperations opDB = new DBManager().GetOperationsDB();
            string[] names = opDB.GetUniqueOperationNames(domainAdmin);
            ArrayList retList = new ArrayList();
            retList.Add("");
            if (names != null)
                foreach (string s in names)
                    retList.Add(s);
            return retList;
        }
        /// <summary>
        /// Get a DataTable for the Operations Data Grid
        /// </summary>
        /// <param name="domainAdmin">Domain Administrator who is loged in.</param>
        /// <returns>Columns: OPERATION_ID, OPERATION_NAME, DOMAIN_NAME, WEB_SERVICE_NAME</returns>
        public static DataTable GetOperationsDataGrid(string domainAdmin)
        {
            return new DBManager().GetOperationsDB().GetOperationsDataGrid(domainAdmin);
        }
        /// <summary>
        /// Get a DataTable for the Operations by UserName
        /// </summary>
        /// <param name="domainAdmin">Domain Administrator who is loged in.</param>
        /// <param name="versionNo">Version No</param>
        /// <returns>Columns: OPERATION_ID, OPERATION_NAME</returns>
        public static DataTable GetOperationsByUser(string domainAdmin, string versionNo)
        {
            return new DBManager().GetOperationsDB().GetOperationsByUser(domainAdmin, versionNo);
        }
        /// <summary>
        /// Get a DataTable for the Operations Data Grid
        /// </summary>
        /// <param name="domainAdmin">Domain Administrator who is loged in.</param>
        /// <param name="versionNo">Version No</param>
        /// <returns>Columns: OPERATION_ID, OPERATION_NAME</returns>
        public static DataTable GetOperationsByUserForOperationMgr(string domainAdmin, string versionNo)
        {
            return new DBManager().GetOperationsDB().GetOperationsByUserForOperationMgr(domainAdmin, versionNo);
        }
        /// <summary>
        /// Get the Operations List for a Domain
        /// </summary>
        /// <param name="domainName">The Domain Name</param>
        /// <returns>Columns: OPERATION_ID, OPERATION_NAME</returns>
        public static DataTable GetOperationsList(string domainName)
        {
            DataTable dt = new DBManager().GetOperationsDB().GetOperationsList(domainName);
            DataRow dr = dt.NewRow();
            dr["OPERATION_ID"] = -1;
            dr["OPERATION_NAME"] = "";
            dt.Rows.InsertAt(dr, 0);
            return dt;
        }
        /// <summary>
        /// Search the Node Database for Operations
        /// </summary>
        /// <param name="domain">The Domain of the Operation(s)</param>
        /// <param name="opID">The Operation ID of the Operation</param>
        /// <param name="opType">The Operation Type of the Operation(s)</param>
        /// <param name="wsID">The Web Service ID of the Operation(s)</param>
        /// <param name="status">The Status of the Operation(s)</param>
        /// <returns>Columns: OPERATION_ID, OPERATION_NAME, OPERATION_TYPE, WEB_SERVICE_NAME, OPERATION_STATUS_CD, OPERATION_STATUS_MSG</returns>
        public static DataTable SearchOperations(string domain, int opID, string opType, int wsID, string status)
        {
            return new DBManager().GetOperationsDB().SearchOperations(domain, opID, opType, wsID, status);
        }

        #endregion

        #region Private Variables

        private int id = -1;
        private int domainID = -1;
        private string domainName = null;
        private string domainStatus = null;
        private int wsID = -1;
        private string wsName = null;
        private string name = null;
        private string description = null;
        private string type = null;
        private XmlDocument config = null;
        private string status = null;
        private string message = null;
        private TaskSchedule taskSchedule = null;
        private Task task = null;
        private DateTime createdDate;
        private string createdBy = null;
        private DateTime updatedDate;
        private string updatedBy = null;
        private string version = null;
        #endregion

        #region Private Methods

        private void Init(Operation op)
        {
            if (op != null && op.ID >= 0)
            {
                this.id = op.id;
                this.name = op.name;
                this.type = op.type;
                this.config = op.config;
                this.domainID = op.domainID;
                this.domainName = op.domainName;
                this.domainStatus = op.domainStatus;
                this.wsID = op.wsID;
                this.wsName = op.wsName;
                this.status = op.status;
                this.message = op.message;
                this.description = op.description;
                this.taskSchedule = op.taskSchedule;
                this.task = op.task;
                this.createdDate = op.createdDate;
                this.createdBy = op.createdBy;
                this.updatedDate = op.updatedDate;
                this.updatedBy = op.updatedBy;
                this.PublishInd = op.PublishInd;
                this.version = op.version;
                this.RESTInd = op.RESTInd;
            }
        }

        #endregion
    }
}
