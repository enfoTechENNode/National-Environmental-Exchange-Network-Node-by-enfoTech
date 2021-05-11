using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Services.Protocols;

using Node.Core.Biz.Interfaces.GetServices;
using Node.Core.Biz.Manageable.Parameters;
using Node.Core.Data;
using Node.Core.Data.Interfaces;

namespace Node.Core.Default.GetServices
{
    /// <summary>
    /// The defalut plug-in class for GetServices Operation.
    /// </summary>
    public class Process : IProcess
    {
        /// <summary>
        /// retrun a list of avaliable service name.
        /// </summary>
        /// <param name="token">The security toke.</param>
        /// <param name="serviceType">The type of service.</param>
        /// <param name="param">The operation parameters.</param>
        /// <returns>The array of service name.</returns>
        public string[] Execute(string token, string serviceType, ProcParam param)
        {
            IGetServices gsDB = new DBManager().GetGetServicesDB();
            if (serviceType != null && !serviceType.Trim().Equals(""))
            {
                string[] services = null;
                switch (serviceType.ToUpper())
                {
                    case "SERVICETYPE":
                        services = gsDB.GetServiceTypes();
                        break;
                    case "SERVICETYPES":
                        services = gsDB.GetServiceTypes();
                        break;
                    case "INTERFACES":
                        services = gsDB.GetWebServices();
                        break;
                    case "QUERY":
                        services = gsDB.GetQueries();
                        break;
                    case "SOLICIT":
                        services = gsDB.GetSolicits();
                        break;
                    default:
                        if (gsDB.TestDomainName(serviceType))
                            services = gsDB.GetOpNamesFromDomain(serviceType);
                        else
                            throw new SoapException(Phrase.E_INVALID_PARAMETER, SoapException.ClientFaultCode);
                        break;
                }
                return services;
            }
            throw new SoapException(Phrase.E_INVALID_PARAMETER, SoapException.ClientFaultCode);
        }
    }
}
