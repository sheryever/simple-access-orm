using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Dynamic;
using System.Reflection;
using SimpleAccess.Core;
using SimpleAccess.Core.Logger;

namespace SimpleAccess.SqlServer
{
    /// <summary>
    /// Sql Server implementaion for SimpleAccess
    /// </summary>
    public interface ISqlSimpleAccess : 
        ISimpleAccess <SqlConnection, SqlTransaction, SqlCommand, SqlParameter, SqlDataReader, SqlServerSqlBuilder>
        , IDisposable
    {
        

        /// <summary>
        /// SimpleLogger to log exception
        /// </summary>
        ISimpleLogger SimpleLogger { get; }

        
        /// <summary> Creates a command. </summary>
        /// 
        /// <param name="commandText">   The query string. </param>
        /// <param name="commandType">   Type of the command. </param>
        /// <param name="sqlParameters"> Options for controlling the SQL. </param>
        /// 
        /// <returns> The new command. </returns>
        SqlCommand CreateCommand(string commandText, CommandType commandType, params SqlParameter[] sqlParameters);

        /// <summary> Creates a command. </summary>
        /// 
        /// <param name="sqlTransaction"> The SQL transaction. </param>
        /// <param name="commandText">    The query string. </param>
        /// <param name="commandType">    Type of the command. </param>
        /// <param name="sqlParameters">  Options for controlling the SQL. </param>
        /// 
        /// <returns> The new command. </returns>
        SqlCommand CreateCommand(SqlTransaction sqlTransaction, string commandText, CommandType commandType
            , params SqlParameter[] sqlParameters);

        /// <summary> SQL data reader to expando. </summary>
        /// 
        /// <param name="reader"> The reader. </param>
        /// 
        /// <returns> . </returns>
        dynamic SqlDataReaderToExpando(SqlDataReader reader);

        /// <summary> Gets a dynamic SQL data. </summary>
        /// 
        /// <param name="reader"> The reader. </param>
        /// 
        /// <returns> The dynamic SQL data. </returns>
        IList<dynamic> GetDynamicSqlData(SqlDataReader reader);

        /// <summary> Build SqlParameter Array from dynamic object. </summary>
        ///  <param name="paramObject"> The dynamic object as parameters. </param>
        /// <returns> SqlParameter[] object and if paramObject is null then return null </returns>
        SqlParameter[] BuildSqlParameters(dynamic paramObject);
    }
}
