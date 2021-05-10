#region (C) enfotech & consulting, Inc. 2005
// 
// All rights are reserved. 
// 
// File:		LogLevel.cs
// Company:		enfoTech & Consulting Inc.
// OS:			Windows XP Pro (SP1, English)
// Compiler:	Visual Studio .NET (Version 8.0.41115)
//				Microsoft .NET Framework 2.0 (Version 2.0.41115)
// History:		05/05/2005 Danwen Sun Creation
// 
#endregion

using System;
using System.Collections.Generic;
using System.Text;

namespace Node.Lib.Audit
{
	/// <summary>
	/// Represents the data log level.
	/// </summary>
	public enum LogLevel
	{
		/// <summary>
		/// Verbose level is used for debug information.
		/// </summary>
		Verbose = 0,		// for debug information
		/// <summary>
		/// Information level is used for nomral information.
		/// </summary>
		Information = 1,	// for normal information
		/// <summary>
		/// Warning level is used for warning information.
		/// </summary>
		Warning = 2,		// for warning information
		/// <summary>
		/// Error level is used for error information.
		/// </summary>
		Error = 3,			// for error information
		/// <summary>
		/// Critical level is used for fatal error information.
		/// </summary>
		Critical = 4		// for fatal error information
	};
}
