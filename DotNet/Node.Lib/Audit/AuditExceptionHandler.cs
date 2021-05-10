#region (C) enfotech & consulting, Inc. 2005
// 
// All rights are reserved. 
// 
// File:		DataExceptionHandler.cs
// Company:		enfoTech & Consulting Inc.
// OS:			Windows XP Pro (SP1, English)
// Compiler:	Visual Studio .NET (Version 8.0.41115)
//				Microsoft .NET Framework 2.0 (Version 2.0.50215)
// History:		05/10/2005 Danwen Sun Creation
// 
#endregion 

using System;
using System.Collections.Generic;
using System.Text;

namespace Node.Lib.Audit
{
	/// <summary>
	/// Represents the exception handler for audit operation.
	/// </summary>
	public class AuditExceptionHandler : ApplicationException
	{
		/// <summary>
		/// Initializes a <see cref="EAF.Lib.Audit.AuditExceptionHandler">AuditExceptionHandler</see> class.
		/// </summary>
		public AuditExceptionHandler() : base()
		{
		}

		/// <summary>
		/// Initializes a <see cref="EAF.Lib.Audit.AuditExceptionHandler">AuditExceptionHandler</see> class.
		/// </summary>
		/// <param name="message">The error message.</param>
		public AuditExceptionHandler(string message) : base(message)
		{
		}

		/// <summary>
		/// Initializes a <see cref="EAF.Lib.Audit.AuditExceptionHandler">AuditExceptionHandler</see> class.
		/// </summary>
		/// <param name="message">The error message.</param>
		/// <param name="innerException">The <see cref="System.Exception">Exception</see> object contains the exception information.</param>
		public AuditExceptionHandler(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
