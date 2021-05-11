#region (C) enfotech & consulting, Inc. 2005
// 
// All rights are reserved. 
// 
// File:		SecurityExceptionHandler.cs
// Company:		enfoTech & Consulting Inc.
// OS:			Windows XP Pro (SP1, English)
// Compiler:	Visual Studio .NET (Version 8.0.50215)
//				Microsoft .NET Framework 2.0 (Version 2.0.50215)
// History:		06/14/2005 Danwen Sun Creation
// 
#endregion 

using System;
using System.Collections.Generic;
using System.Text;

namespace Node.Lib.Security
{
	/// <summary>
	/// Represents the exception handler for security operation.
	/// </summary>
	public class SecurityExceptionHandler : ApplicationException
	{
		/// <summary>
		/// Initializes a <see cref="EAF.Lib.Security.SecurityExceptionHandler">SecurityExceptionHandler</see> class.
		/// </summary>
		public SecurityExceptionHandler()
			: base()
		{
		}

		/// <summary>
		/// Initializes a <see cref="EAF.Lib.Security.SecurityExceptionHandler">SecurityExceptionHandler</see> class.
		/// </summary>
		/// <param name="message">The error message.</param>
		public SecurityExceptionHandler(string message)
			: base(message)
		{
		}

		/// <summary>
		/// Initializes a <see cref="EAF.Lib.Security.SecurityExceptionHandler">SecurityExceptionHandler</see> class.
		/// </summary>
		/// <param name="message">The error message.</param>
		/// <param name="innerException">The <see cref="System.Exception">Exception</see> object contains the exception information.</param>
		public SecurityExceptionHandler(string message, Exception innerException)
			: base(message, innerException)
		{
		}

	}
}
