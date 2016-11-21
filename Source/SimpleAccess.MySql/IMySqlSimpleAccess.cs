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
using SimpleAccess.Core;
using SimpleAccess.Core.Logger;
using MySql.Data.MySqlClient;

namespace SimpleAccess.MySql
{
    /// <summary>
    /// SimpleAccess implementation for MySql.
    /// </summary>
    public interface IMySqlSimpleAccess : 
        ISimpleAccess <MySqlConnection, MySqlTransaction, MySqlCommand, MySqlParameter, MySqlDataReader, MySqlSqlBuilder>
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
        /// <param name="sqlParameters">Options for controlling the SQL. </param>
        /// 
        /// <returns> The new command. </returns>
        MySqlCommand CreateCommand(string commandText, CommandType commandType, params MySqlParameter[] sqlParameters);

        /// <summary> Creates a command. </summary>
        /// 
        /// <param name="sqlTransaction"> The SQL transaction. </param>
        /// <param name="commandText"> The query string. </param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="sqlParameters"> Options for controlling the SQL. </param>
        /// 
        /// <returns> The new command. </returns>
        MySqlCommand CreateCommand(MySqlTransaction sqlTransaction, string commandText, CommandType commandType
            , params MySqlParameter[] sqlParameters);

        /// <summary> SQL data reader to <see cref="ExpandoObject"/>. </summary>
        /// 
        /// <param name="reader"> The reader. </param>
        /// 
        /// <returns> . </returns>
        dynamic SqlDataReaderToExpando(MySqlDataReader reader);

        /// <summary> Gets a dynamic SQL data. </summary>
        /// 
        /// <param name="reader"> The reader. </param>
        /// 
        /// <returns> The dynamic SQL data. </returns>
        IList<dynamic> GetDynamicSqlData(MySqlDataReader reader);

        /// <summary> Build MySqlParameter Array from dynamic object. </summary>
        ///  <param name="paramObject"> The dynamic object as parameters. </param>
        /// <returns> MySqlParameter[] object and if paramObject is null then return null </returns>
        MySqlParameter[] BuildSqlParameters(object paramObject);
    }
}
