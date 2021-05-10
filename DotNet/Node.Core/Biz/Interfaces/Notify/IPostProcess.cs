using System;

using Node.Core.Biz.Manageable.Parameters;
using Node.Core.Document;

namespace Node.Core.Biz.Interfaces.Notify
{
    /// <summary>
    /// The Interface for Notify PostProcess
    /// </summary>
    public interface IPostProcess
    {
        /// <summary>
        /// PostProcess of Notifiy.
        /// </summary>
        /// <param name="securityToken">The Token which is result of Authentication Operation.</param>
        /// <param name="nodeAddress">For document nofification, the parameter contains a network node address where the document can be downloaded. It should contain the initiator's node address, or be empty if not applicable, for event and status notifications.</param>
        /// <param name="dataFlow">Specifies which dataflow are to be used. </param>
        /// <param name="documents">The reserved container for the requesed documents.</param>
        /// <param name="param">Operation related parameters</param>
        /// <returns>string of result</returns>
        void Execute(string securityToken, string nodeAddress, string dataFlow, NodeDocument[] documents, PostParam param);
    }
}
