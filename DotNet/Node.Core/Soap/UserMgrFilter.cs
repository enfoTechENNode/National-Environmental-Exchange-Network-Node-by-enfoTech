using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Microsoft.Web.Services2;

namespace Node.Core.Soap
{
    /// <summary>
    /// Modify outbound soap Message for NAAS userstate value.
    /// </summary>
    public class UserMgrFilter : SoapOutputFilter
    {
        /// <summary>
        /// The entry point of soap message modification.
        /// </summary>
        /// <param name="envelope">The soapenvelope of soap message.</param>
        public override void ProcessMessage(SoapEnvelope envelope)
        {
            envelope.Header.RemoveAll();
            XmlNodeList stateIDs = envelope.SelectNodes(".//userState");
            foreach (XmlNode idNode in stateIDs)
            {
                if (idNode.InnerText.Equals("Item"))
                    idNode.InnerText = "";
            }
        }
    }
}
