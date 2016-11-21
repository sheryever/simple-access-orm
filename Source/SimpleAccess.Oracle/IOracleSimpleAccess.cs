using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using System.Dynamic;
using System.Reflection;
using Oracle.ManagedDataAccess.Client;
using SimpleAccess.Core;
using SimpleAccess.Core.Logger;

namespace SimpleAccess.Oracle
{
    /// <summary>
    /// SimpleAccess implementation for Oracle.
    /// </summary>
    public interface IOracleSimpleAccess : 
        ISimpleAccess <OracleConnection, OracleTransaction, OracleCommand, OracleParameter, OracleDataReader, OracleSqlBuilder>
        , IDisposable
    {
        

        /// <summary>
        /// SimpleLogger to log exception
        /// </summary>
        ISimpleLogger SimpleLogger { get; }
        

            /// <summary> Creates a command. </summary>
        /// 
        /// <param name="commandText"> The query string. </param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="oracleParameters">Options for controlling the SQL. </param>
        /// 
        /// <returns> The new command. </returns>
        OracleCommand CreateCommand(string commandText, CommandType commandType, params OracleParameter[] oracleParameters);

        /// <summary> Creates a command. </summary>
        /// 
        /// <param name="sqlTransaction"> The SQL transaction. </param>
        /// <param name="commandText"> The query string. </param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="oracleParameters"> Options for controlling the SQL. </param>
        /// 
        /// <returns> The new command. </returns>
        OracleCommand CreateCommand(OracleTransaction sqlTransaction, string commandText, CommandType commandType
            , params OracleParameter[] oracleParameters);

        /// <summary> SQL data reader to <see cref="ExpandoObject"/>. </summary>
        /// 
        /// <param name="reader"> The reader. </param>
        /// 
        /// <returns> . </returns>
        dynamic OracleDataReaderToExpando(OracleDataReader reader);

        /// <summary> Gets a dynamic SQL data. </summary>
        /// 
        /// <param name="reader"> The reader. </param>
        /// 
        /// <returns> The dynamic SQL data. </returns>
        IList<dynamic> GetDynamicSqlData(OracleDataReader reader);

        /// <summary> Build OracleParameter Array from dynamic object. </summary>
        ///  <param name="paramObject"> The dynamic object as parameters. </param>
        /// <returns> OracleParameter[] object and if paramObject is null then return null </returns>
        OracleParameter[] BuildOracleParameters(object paramObject);
    }
}
