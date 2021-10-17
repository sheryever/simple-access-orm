using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Dynamic;
using System.Reflection;
using SimpleAccess.Core;
using SimpleAccess.Core.Logger;

namespace SimpleAccess.SQLite
{
    /// <summary>
    /// Represent the SimpleAccess interface for SQL Server
    /// </summary>
    //    public interface ISqlSimpleAccess :
    //        ISimpleAccess <SQLiteConnection, SQLiteTransaction, SQLiteCommand, SQLiteParameter, SQLiteDataReader, SQLiteSqlBuilder>
    //#if !NET40
    //        , ISimpleAccessAsync<SQLiteConnection, SQLiteTransaction, SQLiteCommand, SQLiteParameter, SQLiteDataReader, SQLiteSqlBuilder, SQLiteTransactionAsyncContext>
    //#endif 
    //        , IDisposable
    //    {
    public interface ISQLiteSimpleAccess :
        ISimpleAccess<SQLiteConnection, SQLiteTransaction, SQLiteCommand, SQLiteParameter, SQLiteDataReader, SQLiteTransactionAsyncContext>
        //, ISimpleAccessAsync<SQLiteConnection, SQLiteTransaction, SQLiteCommand, SQLiteParameter, SQLiteDataReader, SQLiteTransactionAsyncContext>
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
        /// <param name="SQLiteParameters">Options for controlling the SQL. </param>
        /// 
        /// <returns> The new command. </returns>
        SQLiteCommand CreateCommand(string commandText, CommandType commandType, params SQLiteParameter[] SQLiteParameters);

        /// <summary> Creates a command. </summary>
        /// 
        /// <param name="SQLiteTransaction"> The SQL transaction. </param>
        /// <param name="commandText"> The query string. </param>
        /// <param name="commandType"> Type of the command. </param>
        /// <param name="SQLiteParameters"> Options for controlling the SQL. </param>
        /// 
        /// <returns> The new command. </returns>
        SQLiteCommand CreateCommand(SQLiteTransaction SQLiteTransaction, string commandText, CommandType commandType
            , params SQLiteParameter[] SQLiteParameters);

        /// <summary> SQL data reader to <see cref="ExpandoObject"/>. </summary>
        /// 
        /// <param name="reader"> The reader. </param>
        /// 
        /// <returns> . </returns>
        dynamic SQLiteDataReaderToExpando(SQLiteDataReader reader);

        /// <summary> Gets a dynamic SQL data. </summary>
        /// 
        /// <param name="reader"> The reader. </param>
        /// 
        /// <returns> The dynamic SQL data. </returns>
        IList<dynamic> GetDynamicSqlData(SQLiteDataReader reader);

        /// <summary> Gets a dynamic SQL data. </summary>
        /// 
        /// <param name="reader"> The reader. </param>
        /// 
        /// <returns> The dynamic SQL data. </returns>
        Task<IList<dynamic>> GetDynamicSqlDataAsync(SQLiteDataReader reader);

        /// <summary> Build SQLiteParameter Array from dynamic object. </summary>
        ///  <param name="paramObject"> The dynamic object as parameters. </param>
        /// <returns> SQLiteParameter[] object and if paramObject is null then return null </returns>
        SQLiteParameter[] BuildDbParameters(object paramObject);
    }
}
