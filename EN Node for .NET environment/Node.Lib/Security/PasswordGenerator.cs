#region (C) enfotech & consulting, Inc. 2005
// 
// All rights are reserved. 
// 
// File:		PasswordGenerator.cs
// Company:		enfoTech & Consulting Inc.
// OS:			Windows XP Pro (SP1, English)
// Compiler:	Visual Studio .NET (Version 8.0.50215)
//				Microsoft .NET Framework 2.0 (Version 2.0.50215)
// History:		06/14/2005 Ryan Teising Creation
// 
#endregion 

using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace Node.Lib.Security
{
	/// <summary>
	/// Provides the properties and methods to generate the password.
	/// </summary>
	public class PasswordGenerator
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="EAF.Lib.Security.PasswordGenerator">PasswordGenerator</see> class.
		/// </summary>
		public PasswordGenerator()
		{
			this.pwdCharArray = pwdString.ToCharArray();
			this.Minimum = DefaultMinimum;
			this.Maximum = DefaultMaximum;
			this.ConsecutiveCharacters = false;
			this.RepeatCharacters = true;
			this.ExcludeSymbols = false;

			rng = new RNGCryptoServiceProvider();
		}

		/// <summary>
		/// Get the crytographic random number.
		/// </summary>
		/// <param name="lBound">The lower bound number.</param>
		/// <param name="uBound">The upper bound number.</param>
		/// <returns>The crytographic random number.</returns>
		protected int GetCryptographicRandomNumber(int lBound, int uBound)
		{
			// Assumes lBound >= 0 && lBound < uBound
			// returns an int >= lBound and < uBound
			uint urndnum;
			byte[] rndnum = new Byte[4];
			if (lBound >= uBound - 1)
			{
				// test for degenerate case where only lBound can be returned
				return lBound;
			}

			uint xcludeRndBase = (uint.MaxValue -
				(uint.MaxValue % (uint)(uBound - lBound)));

			do
			{
				rng.GetBytes(rndnum);
				urndnum = System.BitConverter.ToUInt32(rndnum, 0);
			} while (urndnum >= xcludeRndBase);

			return (int)(urndnum % (uBound - lBound)) + lBound;
		}

		/// <summary>
		/// Gets the random character.
		/// </summary>
		/// <returns>The character.</returns>
		protected char GetRandomCharacter()
		{
			int upperBound = pwdCharArray.GetUpperBound(0);

			if (true == this.ExcludeSymbols)
			{
				upperBound = PasswordGenerator.UBoundDigit;
			}

			int randomCharPosition = GetCryptographicRandomNumber(
				pwdCharArray.GetLowerBound(0), upperBound);

			char randomChar = pwdCharArray[randomCharPosition];

			return randomChar;
		}

		/// <summary>
		/// Generates a password.
		/// </summary>
		/// <returns>The password.</returns>
		public string Generate()
		{
			// Pick random length between minimum and maximum   
			int pwdLength = GetCryptographicRandomNumber(this.Minimum,
				this.Maximum);

			StringBuilder pwdBuffer = new StringBuilder();
			pwdBuffer.Capacity = this.Maximum;

			// Generate random characters
			char lastCharacter, nextCharacter;

			// Initial dummy character flag
			lastCharacter = nextCharacter = '\n';

			for (int i = 0; i < pwdLength; i++)
			{
				nextCharacter = GetRandomCharacter();

				if (false == this.ConsecutiveCharacters)
				{
					while (lastCharacter == nextCharacter)
					{
						nextCharacter = GetRandomCharacter();
					}
				}

				if (false == this.RepeatCharacters)
				{
					string temp = pwdBuffer.ToString();
					int duplicateIndex = temp.IndexOf(nextCharacter);
					while (-1 != duplicateIndex)
					{
						nextCharacter = GetRandomCharacter();
						duplicateIndex = temp.IndexOf(nextCharacter);
					}
				}

				if ((null != this.Exclusions))
				{
					while (-1 != this.Exclusions.IndexOf(nextCharacter))
					{
						nextCharacter = GetRandomCharacter();
					}
				}

				pwdBuffer.Append(nextCharacter);
				lastCharacter = nextCharacter;
			}

			if (null != pwdBuffer)
			{
				return pwdBuffer.ToString();
			}
			else
			{
				return String.Empty;
			}
		}

		/// <summary>
		/// Gets or sets the exclusions.
		/// </summary>
		/// <returns>The exclusive string.</returns>
		public string Exclusions
		{
			get { return this.exclusionSet; }
			set { this.exclusionSet = value; }
		}

		/// <summary>
		/// Gets or sets the minimum size of password.
		/// </summary>
		/// <returns>An integer to show the minimum size of password.</returns>
		public int Minimum
		{
			get { return this.minSize; }
			set { this.minSize = value; }
		}

		/// <summary>
		/// Gets or sets the maximum size of password.
		/// </summary>
		/// <returns>An integer to show the maximum size of password.</returns>
		public int Maximum
		{
			get { return this.maxSize; }
			set { this.maxSize = value; }
		}

		/// <summary>
		/// Checks whether or not the password contains the excluded symbols.
		/// </summary>
		/// <returns><b>true</b>, if the password contains the excluded symbol; <b>false</b>, otherwise.</returns>
		public bool ExcludeSymbols
		{
			get { return this.hasSymbols; }
			set { this.hasSymbols = value; }
		}

		/// <summary>
		/// Checks whether or not the password contains the repeating character.
		/// </summary>
		/// <returns><b>true</b>, if the password contains the repeating character; <b>false</b>, otherwise.</returns>
		public bool RepeatCharacters
		{
			get { return this.hasRepeating; }
			set { this.hasRepeating = value; }
		}

		/// <summary>
		/// Checks whether or not the password contains the consecutive characters.
		/// </summary>
		/// <returns><b>true</b>, if the password contains the consecutive character; <b>false</b>, otherwise.</returns>
		public bool ConsecutiveCharacters
		{
			get { return this.hasConsecutive; }
			set { this.hasConsecutive = value; }
		}

		private const int DefaultMinimum = 8;
		private const int DefaultMaximum = 8;
		private const int UBoundDigit = 61;

		private RNGCryptoServiceProvider rng;
		private int minSize;
		private int maxSize;
		private bool hasRepeating;
		private bool hasConsecutive;
		private bool hasSymbols;
		private string exclusionSet = "Il1oO0";   // for default.
		private string pwdString = "abcdefghijklmnopqrstuvwxyzABCDEFG" +
			"HIJKLMNOPQRSTUVWXYZ0123456789";
		private char[] pwdCharArray;
	}
}
