using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Node.Core.Data;
using System.Xml.Linq;

namespace Node.Core.Biz.Objects
{
    /// <summary>
    /// DEDL is used to access DEDL XML file. 
    /// </summary>
    public class DEDL
    {
        private XDocument _dedl;
        XNamespace dedl = "http://www.exchangenetwork.net/schema/dedl/1";
        /// <summary>
        /// The constructor of DEDL class. 
        /// </summary>
        public DEDL()
        {
            string inputConfig = new DBManager().GetConfigurationsDB().GetDEDL();
            _dedl = XDocument.Parse(inputConfig);
        }

        public List<DEDLDataElement> GetDataElements()
        {
            List<DEDLDataElement> lstDataElements = new List<DEDLDataElement>();
            foreach (XElement xe in _dedl.Descendants("DataElement"))
                lstDataElements.Add(new DEDLDataElement(xe));
            return lstDataElements;
        }

        public bool AddDataElement(string name)
        {
            bool bOk = false;

            if (_dedl.Root.Descendants("DataElement").Where(x => x.Element("ElementIdentifier").Value == name).Count() == 0)
            {
                XElement newData = new XElement("DataElement");
                _dedl.Root.Add(newData);

                XElement xe = new XElement("ElementIdentifier",name);
                newData.Add(xe);
                xe = new XElement("ApplicationDomain");
                newData.Add(xe);
                xe = new XElement("ElementType");
                newData.Add(xe);
                xe = new XElement("Description");
                newData.Add(xe);
                xe = new XElement("Keywords");
                newData.Add(xe);
                xe = new XElement("Owner");
                newData.Add(xe);
                xe = new XElement("ElementLabel");
                newData.Add(xe);
                xe = new XElement("DefaultValue");
                newData.Add(xe);
                xe = new XElement("LastUpdated");
                newData.Add(xe);

                XElement xe2 = new XElement("DataConstrains");
                xe = new XElement("AllowMultiSelect");
                xe2.Add(xe);
                xe = new XElement("AdditionalValuesIndicator");
                xe2.Add(xe);
                xe = new XElement("Optionality");
                xe2.Add(xe);
                xe = new XElement("Wildcard");
                xe2.Add(xe);
                xe = new XElement("FormatString");
                xe2.Add(xe);
                xe = new XElement("ValidationRules");
                xe2.Add(xe);
                newData.Add(xe2);

                bOk = true;
            }
            return bOk;
        }

        public DEDLDataElement GetDataElement(string id)
        {
            XElement xe = _dedl.Root.Descendants("DataElement").Where(x => x.Element("ElementIdentifier").Value == id).First<XElement>();
            return new DEDLDataElement(xe);
        }

        public bool Save()
        {
            bool bSave = false;
            string sXML = _dedl.Declaration.ToString() + Environment.NewLine + _dedl.ToString(); 
            bSave = new DBManager().GetConfigurationsDB().UpdateDEDL(sXML);
            return bSave;
        }
    }

    public class DEDLDataElement
    {
        private XElement _dataelement;
        public DEDLDataElement(XElement xe)
        {
            _dataelement = xe;
        }
        public string ElementIdentifier 
        {
            get { return _dataelement.Element("ElementIdentifier").Value; }
        }
        public string ApplicationDomain
        {
            get { return _dataelement.Element("ApplicationDomain").Value; }
            set { _dataelement.Element("ApplicationDomain").Value = value; }
        }
        public string ElementType
        {
            get { return _dataelement.Element("ElementType").Value; }
            set { _dataelement.Element("ElementType").Value = value; }
        }
        public string Description
        {
            get { return _dataelement.Element("Description").Value; }
            set { _dataelement.Element("Description").Value = value; }
        }
        public string Keywords
        {
            get { return _dataelement.Element("Keywords").Value; }
            set { _dataelement.Element("Keywords").Value = value; }
        }
        public string Owner
        {
            get { return _dataelement.Element("Owner").Value; }
            set { _dataelement.Element("Owner").Value = value; }
        }
        public string ElementLabel
        {
            get { return _dataelement.Element("ElementLabel").Value; }
            set { _dataelement.Element("ElementLabel").Value = value; }
        }
        public string DefaultValue
        {
            get { return _dataelement.Element("DefaultValue").Value; }
            set { _dataelement.Element("DefaultValue").Value = value; }
        }
        public string LastUpdated
        {
            get { return _dataelement.Element("LastUpdated").Value; }
            set { _dataelement.Element("LastUpdated").Value = value; }
        }

        public string AllowMultiSelect
        {
            get { return _dataelement.Element("DataConstrains").Element("AllowMultiSelect").Value; }
            set { _dataelement.Element("DataConstrains").Element("AllowMultiSelect").Value = value; }
        }

        public string AdditionalValuesIndicator
        {
            get { return _dataelement.Element("DataConstrains").Element("AdditionalValuesIndicator").Value; }
            set { _dataelement.Element("DataConstrains").Element("AdditionalValuesIndicator").Value = value; }
        }
        public string Optionality
        {
            get { return _dataelement.Element("DataConstrains").Element("Optionality").Value; }
            set { _dataelement.Element("DataConstrains").Element("Optionality").Value = value; }
        }
        public string Wildcard
        {
            get { return _dataelement.Element("DataConstrains").Element("Wildcard").Value; }
            set { _dataelement.Element("DataConstrains").Element("Wildcard").Value = value; }
        }
        public string FormatString
        {
            get { return _dataelement.Element("DataConstrains").Element("FormatString").Value; }
            set { _dataelement.Element("DataConstrains").Element("FormatString").Value = value; }
        }
        public string ValidationRules
        {
            get { return _dataelement.Element("DataConstrains").Element("ValidationRules").Value; }
            set { _dataelement.Element("DataConstrains").Element("ValidationRules").Value = value; }
        }

        public List<DEDLProperty> Properties 
        {
            get 
            { 
                List<DEDLProperty> lstProperty = new List<DEDLProperty>();
                foreach (XElement xe in _dataelement.Descendants("Property"))
                    lstProperty.Add(new DEDLProperty(xe));
                return lstProperty;
            }
        }

        public List<DEDLElementValue> ElementValues 
        {
            get 
            {
                List<DEDLElementValue> lstElementValue = new List<DEDLElementValue>();
                foreach (XElement xe in _dataelement.Descendants("ElementValue"))
                    lstElementValue.Add(new DEDLElementValue(xe));
                return lstElementValue;              
           }
        }

        public bool AddProperty(string name, string value)
        {
            bool bOk = false;

            if (_dataelement.Descendants("Property").Where(x => x.Element("PropertyName").Value == name).Count() == 0)
            {
                XElement newData = new XElement("Property");
                XElement xe = new XElement("PropertyName", name);
                newData.Add(xe);
                xe = new XElement("PropertyValue", value);
                newData.Add(xe);

                _dataelement.Add(newData);
                bOk = true;
            }
            return bOk;
            
        }
        public bool AddElementValue(string label, string value)
        {
            bool bOk = false;

            if (_dataelement.Descendants("ElementValue").Where(x => x.Attribute("ValueLabel").Value == label).Count() == 0)
            {
                XElement newData = new XElement("ElementValue",value);
                XAttribute xa = new XAttribute("ValueLabel", label);
                newData.Add(xa);
                _dataelement.Add(newData);
                bOk = true;
            }
            return bOk;

        }

        public DEDLProperty GetProperty(string id)
        {
            XElement xe = _dataelement.Descendants("Property").Where(x => x.Element("PropertyName").Value == id).First<XElement>();
            return new DEDLProperty(xe);
        }

        public DEDLElementValue GetElementValue(string id)
        {
            XElement xe = _dataelement.Descendants("ElementValue").Where(x => x.Attribute("ValueLabel").Value == id).First<XElement>();
            return new DEDLElementValue(xe);
        }

        public void Delete()
        {
            _dataelement.Remove();
        }

    }

    public class DEDLProperty
    {
        private XElement _property;

        public DEDLProperty(XElement xe)
        {
            _property = xe;
        }

        public string PropertyName 
        {
            get { return _property.Element("PropertyName").Value; }
        }

        public string PropertyValue
        {
            get { return _property.Element("PropertyValue").Value; }
            set { _property.Element("PropertyValue").Value = value; }
        }

        public void Delete()
        {
            _property.Remove();
        }
    }

    public class DEDLElementValue
    {
        private XElement _ElementValue;
        public DEDLElementValue(XElement xe)
        {
            _ElementValue = xe;
        }
        public string ValueLabel
        {
            get { return _ElementValue.Attribute("ValueLabel").Value; }
        }
        public string ElementValue
        {
            get { return _ElementValue.Value; }
            set { _ElementValue.Value = value; }
        }
        public void Delete()
        {
            _ElementValue.Remove();
        }
    }


}
