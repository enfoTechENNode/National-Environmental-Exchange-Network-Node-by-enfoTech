#region (C) enfotech & consulting, Inc. 2005
// 
// All rights are reserved. 
// 
// File:		DataExceptionHandler.cs
// Company:		enfoTech & Consulting Inc.
// OS:			Windows XP Pro (SP1, English)
// Compiler:	Visual Studio .NET (Version 8.0.41115)
//				Microsoft .NET Framework 2.0 (Version 2.0.41115)
// History:		01/26/2005 Danwen Sun Creation
// 
#endregion 

using System;
using System.Collections.Generic;
using System.Text;

namespace Node.Lib.Data
{
	/// <summary>
	/// Represents the exception handler for database operation.
	/// </summary>
	public class DataExceptionHandler : ApplicationException
	{
		/// <summary>
		/// Initializes a <see cref="EAF.Lib.Data.DataExceptionHandler">DataExceptionHandler</see> class.
		/// </summary>
		public DataExceptionHandler() : base()
		{
		}

		/// <summary>
		/// Initializes a <see cref="EAF.Lib.Data.DataExceptionHandler">DataExceptionHandler</see> class.
		/// </summary>
		/// <param name="message">The error message.</param>
		public DataExceptionHandler(string message) : base(message)
		{
		}

		/// <summary>
		/// Initializes a <see cref="EAF.Lib.Data.DataExceptionHandler">DataExceptionHandler</see> class.
		/// </summary>
		/// <param name="message">The error message.</param>
		/// <param name="innerException">The <see cref="System.Exception">Exception</see> object contains the exception information.</param>
		public DataExceptionHandler(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
