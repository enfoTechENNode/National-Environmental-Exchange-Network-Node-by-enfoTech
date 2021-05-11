#region (C) enfotech & consulting, Inc. 2005
// 
// All rights are reserved. 
// 
// File:		Parameter.cs
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
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data.Odbc;
using System.Data.OracleClient;

namespace Node.Lib.Data
{
	/// <summary>
	/// Represents the parameter used in a query.
	/// </summary>
	public class Parameter
	{
		private string parameterName = "";
		private object parameterValue = null;
		private int size = 0;
		private ParameterDirection direction = ParameterDirection.Input;
        private bool binary = false;
        private DataBaseType dbtype = DataBaseType.String; 

        public enum DataBaseType
        { 
            String,
            Image,
            BLOB,
            CLOB
        }

		/// <overloads>This constructor has two overloads.</overloads>
		/// <summary>
		/// Initializes a new instance of the <see cref="EAF.Lib.Data.Parameter">Parameter</see> class.
		/// </summary>
		public Parameter()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="EAF.Lib.Data.Parameter">Parameter</see> class.
		/// </summary>
		/// <param name="name">The name of parameter.</param>
		/// <param name="val">The value of parameter. The object type will be convert to database type.</param>
		public Parameter(string name, object val)
		{
			this.parameterName = name;
			this.parameterValue = ((val is Int32) && (Int32.Parse(val + "") == 0)) ? Convert.ToInt32(val) : val;
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="EAF.Lib.Data.Parameter">Parameter</see> class.
        /// </summary>
        /// <param name="name">The name of parameter.</param>
        /// <param name="val">The value of parameter. The object type will be convert to database type.</param>
        /// <param name="size">The value of parameter size.</param>
        public Parameter(string name, object val, int size)
        {
            this.parameterName = name;
            this.parameterValue = ((val is Int32) && (Int32.Parse(val + "") == 0)) ? Convert.ToInt32(val) : val;
            this.size = size;
        }

		/// <summary>
		/// Gets or sets the parameter name.
		/// </summary>
		public string ParameterName
		{
			get { return this.parameterName; }
			set { this.parameterName = value; }
		}

		/// <summary>
		/// Gets or sets the parameter value.
		/// </summary>
		public object Value
		{
			get { return this.parameterValue; }
			set { this.parameterValue = value; }
		}

		/// <summary>
		/// Gets or sets the parameter direction.
		/// </summary>
		public ParameterDirection Direction
		{
			get { return this.direction; }
			set { this.direction = value; }
		}

		/// <summary>
		/// Gets or sets the parameter size.
		/// </summary>
		public int Size
		{
			get { return this.size; }
			set { this.size = value; }
		}

        public bool isBinary
        {
            get { return this.binary; }
            set { this.binary = value; }
        }

        public DataBaseType Type
        {
            get { return this.dbtype; }
            set { this.dbtype = value; } 
        }

		/// <summary>
		/// Converts the current parameter to <see cref="System.Data.SqlClient.SqlParameter">SqlParameter</see> object.
		/// </summary>
		/// <returns>A <see cref="System.Data.SqlClient.SqlParameter">SqlParameter</see> to be converted.</returns>
		public SqlParameter ConvertToSqlParameter()
		{
            //DateTime dt;
            //if (!(this.Value is DateTime) && DateTime.TryParse(this.Value + "", out dt))
            //    this.Value = dt;

			SqlParameter par = new SqlParameter(this.ParameterName, this.Value);
			par.Direction = this.Direction;
			par.Size = this.Size;
            if (this.binary && this.dbtype == DataBaseType.Image)
            {
                par.SqlDbType = SqlDbType.Image;
            }
			return par;
		}

		/// <summary>
		/// Converts the currrent parameter to <see cref="System.Data.OracleClient.OracleParameter">OralceParameter</see> object.
		/// </summary>
		/// <returns>An <see cref="System.Data.OracleClient.OracleParameter">OralceParameter</see> to be converted.</returns>
		public OracleParameter ConvertToOracleParameter()
		{
            //DateTime dt;
            //if (!(this.Value is DateTime) && DateTime.TryParse(this.Value + "", out dt))
            //    this.Value = dt;
                
			OracleParameter par = new OracleParameter(this.ParameterName.Replace("@", ":"), this.Value);
			par.Direction = this.Direction;
			par.Size = this.Size;
            if (this.binary)
            {
                if (this.dbtype == DataBaseType.CLOB)
                {
                    par.OracleType = OracleType.Clob;
                }
                else if (this.dbtype == DataBaseType.BLOB)
                {
                    par.OracleType = OracleType.Blob;
                }
            }
			return par;
		}

		/// <summary>
		/// Converts the current parameter to <see cref="System.Data.OleDb.OleDbParameter">OleDbParameter</see> object.
		/// </summary>
		/// <returns>An <see cref="System.Data.OleDb.OleDbParameter">OleDbParameter</see> to be converted.</returns>
		public OleDbParameter ConvertToOleDbParameter()
		{
			OleDbParameter par = new OleDbParameter(this.ParameterName, this.Value);
			par.Direction = this.Direction;
			if (this.Value is Int32)
			{
				par.OleDbType = OleDbType.Integer;
				par.Size = Convert.ToString(Int32.MaxValue).Length;
			}
			if (this.Value is DateTime)
			{
				par.OleDbType = OleDbType.Date;
				par.Size = 20;
			}
			else
			{
				par.OleDbType = OleDbType.VarChar;
				par.Size = Convert.ToString(this.Value).Length;
			}
			return par;
		}

		/// <summary>
		/// Converts the current parameter to <see cref="System.Data.Odbc.OdbcParameter">OdbcParameter</see> object.
		/// </summary>
		/// <returns>An <see cref="System.Data.Odbc.OdbcParameter">OdbcParameter</see> to be converted.</returns>
		public OdbcParameter ConvertToOdbcParameter()
		{
			OdbcParameter par = new OdbcParameter(this.parameterName, this.Value);
			par.Direction = this.Direction;
			par.Size = this.Size;
			return par;
		}
	}
}
