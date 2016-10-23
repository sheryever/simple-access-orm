using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Dynamic;
using SimpleAccess.Core;
using SimpleAccess.Core.Logger;

namespace SimpleAccess.SQLite
{
    /// <summary>
    /// Sqlite implementation for SimpleAccess
    /// </summary>
    public interface ISQLiteSimpleAccess : 
        ISimpleAccess <SQLiteConnection, SQLiteTransaction, SQLiteCommand, SQLiteParameter, SQLiteDataReader, SQLiteSqlBuilder>
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
        /// <param name="sqliteParameters"> Options for controlling the SQL. </param>
        /// 
        /// <returns> The new command. </returns>
        SQLiteCommand CreateCommand(string commandText, CommandType commandType, params SQLiteParameter[] sqliteParameters);

        /// <summary> Creates a command. </summary>
        /// 
        /// <param name="sqliteTransaction"> The SQL transaction. </param>
        /// <param name="commandText">    The query string. </param>
        /// <param name="commandType">    Type of the command. </param>
        /// <param name="sqliteParameters">  Options for controlling the SQL. </param>
        /// 
        /// <returns> The new command. </returns>
        SQLiteCommand CreateCommand(SQLiteTransaction sqliteTransaction, string commandText, CommandType commandType
            , params SQLiteParameter[] sqliteParameters);

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

        /// <summary> Build SQLiteParameter Array from dynamic object. </summary>
        ///  <param name="paramObject"> The dynamic object as parameters. </param>
        /// <returns> SQLiteParameter[] object and if paramObject is null then return null </returns>
        SQLiteParameter[] BuildSQLiteParameters(object paramObject);
    }
}
