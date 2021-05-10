#region (C) enfotech & consulting, Inc. 2005
// 
// All rights are reserved. 
// 
// File:		Cryptography.cs
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
using System.Security.Cryptography;

namespace Node.Lib.Security
{
	/// <summary>
	/// Provides the properties and methods of cryptographic services.
	/// </summary>
	public class Cryptography
	{
		private SymmetricAlgorithm mobjCryptoService;
		private string defaultKey = "cryptoDfk369";

		/// <overloads>This constructor has three overloads.</overloads>
		/// <summary>
		/// Initializes a new instance of the <see cref="EAF.Lib.Security.Cryptography">Cryptography</see> class.
		/// </summary>
		public Cryptography()
		{
			mobjCryptoService = new RijndaelManaged();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="EAF.Lib.Security.Cryptography">Cryptography</see> class with given <see cref="System.Security.Cryptography.SymmetricAlgorithm">SymmetricAlgorithm</see> object.
		/// </summary>
		/// <param name="ServiceProvider">A given <see cref="System.Security.Cryptography.SymmetricAlgorithm">SymmetricAlgorithm</see> object.</param>
		public Cryptography(SymmetricAlgorithm ServiceProvider)
		{
			mobjCryptoService = ServiceProvider;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="EAF.Lib.Security.Cryptography">Cryptography</see> class with a given algorithm.
		/// </summary>
		/// <param name="NetSelected">A given algorithm as <see cref="System.Security.Cryptography.DES">DES</see>, <see cref="System.Security.Cryptography.RC2">RC2</see>, or <see cref="System.Security.Cryptography.Rijndael">Rijndael</see>.</param>
		public Cryptography(SymmProvEnum NetSelected)
		{
			switch (NetSelected)
			{
				case SymmProvEnum.DES:
					mobjCryptoService = new DESCryptoServiceProvider();
					break;
				case SymmProvEnum.RC2:
					mobjCryptoService = new RC2CryptoServiceProvider();
					break;
				case SymmProvEnum.Rijndael:
					mobjCryptoService = new RijndaelManaged();
					break;
			}
		}

		/// <summary>
		/// Supported .Net intrinsic SymmetricAlgorithm classes.
		/// </summary>
		public enum SymmProvEnum : int
		{
			/// <summary>
			/// Represents the algorithm for the Data Encryption Standard (DES).
			/// </summary>
			DES,
			/// <summary>
			/// Represents the algorithm for the RC2
			/// </summary>
			RC2,
			/// <summary>
			/// Represents the symmetric encryption algorithm for the Rijndael
			/// </summary>
			Rijndael
		}

		/// <summary>
		/// Encrypts a source with the default security key.
		/// </summary>
		/// <param name="Source">A string which need to be encrypted.</param>
		/// <returns>The encrypting string.</returns>
		public string Encrypting(string Source)
		{
			return Encrypting(Source, this.defaultKey);
		}

		/// <summary>
		/// Decrypts a source with the default security key.
		/// </summary>
		/// <param name="Source">An encrypting string which need to be decrypted.</param>
		/// <returns>A decrypting string.</returns>
		public string Decrypting(string Source)
		{
			return Decrypting(Source, this.defaultKey);
		}

		/// <summary>
		/// Encrypts a source with the default security key.
		/// </summary>
		/// <param name="Source">A byte array which need to be encrypted.</param>
		/// <returns>The encrypting string.</returns>
		public string Encrypting(byte[] Source)
		{
			return Encrypting(Source, this.defaultKey);
		}

		/// <summary>
		/// Decrypts a source with the default security key.
		/// </summary>
		/// <param name="Source">An encrypting string which need to be decrypted.</param>
		/// <returns>A decrypting byte[].</returns>
		public byte[] DecryptingToByteArray(string Source)
		{
			return DecryptingToByteArray(Source, this.defaultKey);
		}

		/// <summary>
		/// Encrypts a source with the given security key.
		/// </summary>
		/// <param name="Source">A string which need to be encrypted.</param>
		/// <param name="Key">The given security Key.</param>
		/// <returns>The encrypting string.</returns>
		public string Encrypting(string Source, string Key)
		{
			if ((Source == null) || (Key == null) || (Source.Length == 0) || (Key.Length == 0))
				return "";

            byte[] bytIn = UnicodeEncoding.UTF8.GetBytes(Source);
			// create a MemoryStream so that the process can be done without I/O files
			System.IO.MemoryStream ms = new System.IO.MemoryStream();

			byte[] bytKey = GetLegalKey(Key);

			// set the private key
			mobjCryptoService.Key = bytKey;
			mobjCryptoService.IV = bytKey;

			// create an Encryptor from the Provider Service instance
			ICryptoTransform encrypto = mobjCryptoService.CreateEncryptor();

			// create Crypto Stream that transforms a stream using the encryption
			CryptoStream cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Write);

			// write out encrypted content into MemoryStream
			cs.Write(bytIn, 0, bytIn.Length);
			cs.FlushFinalBlock();

			// convert into Base64 so that the result can be used in xml
			return System.Convert.ToBase64String(ms.GetBuffer(), 0, (int)ms.Length);
		}

		/// <summary>
		/// Decrypts a source with the given security key.
		/// </summary>
		/// <param name="Source">An encrypting string which need to be decrypted.</param>
		/// <param name="Key">The given security key.</param>
		/// <returns>A decrypting string.</returns>
		public string Decrypting(string Source, string Key)
		{
			if ((Source == null) || (Key == null) || (Source.Length == 0) || (Key.Length == 0))
				return "";

			// convert from Base64 to binary
			byte[] bytIn = System.Convert.FromBase64String(Source);
			// create a MemoryStream with the input
			System.IO.MemoryStream ms = new System.IO.MemoryStream(bytIn, 0, bytIn.Length);

			byte[] bytKey = GetLegalKey(Key);

			// set the private key
			mobjCryptoService.Key = bytKey;
			mobjCryptoService.IV = bytKey;

			// create a Decryptor from the Provider Service instance
			ICryptoTransform encrypto = mobjCryptoService.CreateDecryptor();

			// create Crypto Stream that transforms a stream using the decryption
			CryptoStream cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Read);

			// read out the result from the Crypto Stream
			System.IO.StreamReader sr = new System.IO.StreamReader(cs);
			return sr.ReadToEnd();
		}

		/// <summary>
		/// Encrypts a source with the given security key.
		/// </summary>
		/// <param name="Source">A byte array which need to be encrypted.</param>
		/// <param name="Key">The given security Key.</param>
		/// <returns>The encrypting string.</returns>
		public string Encrypting(byte[] Source, string Key)
		{
			if ((Source == null) || (Key == null) || (Source.Length == 0) || (Key.Length == 0))
				return "";

			// create a MemoryStream so that the process can be done without I/O files
			System.IO.MemoryStream ms = new System.IO.MemoryStream();

			byte[] bytKey = GetLegalKey(Key);

			// set the private key
			mobjCryptoService.Key = bytKey;
			mobjCryptoService.IV = bytKey;

			// create an Encryptor from the Provider Service instance
			ICryptoTransform encrypto = mobjCryptoService.CreateEncryptor();

			// create Crypto Stream that transforms a stream using the encryption
			CryptoStream cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Write);

			// write out encrypted content into MemoryStream
			cs.Write(Source, 0, Source.Length);
			cs.FlushFinalBlock();

			// convert into Base64 so that the result can be used in xml
			return System.Convert.ToBase64String(ms.GetBuffer(), 0, (int)ms.Length);
		}

		/// <summary>
		/// Decrypts a source with the given security key.
		/// </summary>
		/// <param name="Source">An encrypting string which need to be decrypted.</param>
		/// <param name="Key">The given security key.</param>
		/// <returns>A decrypting byte[].</returns>
		public byte[] DecryptingToByteArray(string Source, string Key)
		{
			if ((Source == null) || (Key == null) || (Source.Length == 0) || (Key.Length == 0))
				return null;

			// convert from Base64 to binary
			byte[] bytIn = System.Convert.FromBase64String(Source);
			// create a MemoryStream with the input
			System.IO.MemoryStream ms = new System.IO.MemoryStream(bytIn, 0, bytIn.Length);

			byte[] bytKey = GetLegalKey(Key);

			// set the private key
			mobjCryptoService.Key = bytKey;
			mobjCryptoService.IV = bytKey;

			// create a Decryptor from the Provider Service instance
			ICryptoTransform encrypto = mobjCryptoService.CreateDecryptor();

			// create Crypto Stream that transforms a stream using the decryption
			CryptoStream cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Read);

			// read out the result from the Crypto Stream
			System.IO.BinaryReader br = new System.IO.BinaryReader(cs);
			return br.ReadBytes((int)ms.Length);
		}

		/// <summary>
		/// Gets a byte array of the given security key.
		/// </summary>
		/// <param name="Key">The given security Key.</param>
		/// <returns>A byte array of the given security key.</returns>
		/// <remarks>
		/// Depending on the legal key size limitations of a specific CryptoService provider
		/// and length of the private key provided, padding the secret key with space character
		/// to meet the legal size of the algorithm.
		/// </remarks>
		private byte[] GetLegalKey(string Key)
		{
			string sTemp;

			if (mobjCryptoService.LegalKeySizes.Length > 0)
			{
				int lessSize = 0, moreSize = mobjCryptoService.LegalKeySizes[0].MinSize;
				// key sizes are in bits
				while (Key.Length * 8 > moreSize)
				{
					lessSize = moreSize;
					moreSize += mobjCryptoService.LegalKeySizes[0].SkipSize;
				}
				sTemp = Key.PadRight(moreSize / 8, ' ');
			}
			else
				sTemp = Key;

			// convert the secret key to byte array
            return UnicodeEncoding.UTF8.GetBytes(sTemp);
		}	
	}
}
