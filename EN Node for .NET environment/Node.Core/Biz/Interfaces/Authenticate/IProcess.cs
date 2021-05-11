﻿using System;

using Node.Core.Biz.Manageable.Parameters;

namespace Node.Core.Biz.Interfaces.Authenticate
{
    /// <summary>
    /// The Interface for Authenticate Process
    /// </summary>
    public interface IProcess
    {
        /// <summary>
        /// Process of Authenticate.
        /// </summary>
        /// <param name="userID">The user ID of the person or system. It is recommended that an email address be used as the userID in the Exchange NetWork.</param>
        /// <param name="credential">The user credential for accessing the network services. </param>
        /// <param name="authenticationMethod">Specifies which authentication methods are to be used.</param>
        /// <param name="param">Operation related parameters</param>
        string Execute(string userID, string credential, string authenticationMethod, ProcParam param);
    }
}
