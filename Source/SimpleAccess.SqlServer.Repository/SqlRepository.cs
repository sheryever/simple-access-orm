using System;
using System.Collections.Generic;
using System.Data;
#if !NETSTANDARD2_1
using System.Data.SqlClient;
#endif
#if NETSTANDARD2_1 || NET6_0_OR_GREATER
using Microsoft.Data.SqlClient;
#endif
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using SimpleAccess.Core.Entity.RepoWrapper;

namespace SimpleAccess.SqlServer
{
    /// <summary> Implements SqlRepository base SqlSimpleAccess with command type stored procedures. </summary>
    [Obsolete("The class " +nameof(SqlRepository)+ " has renamed to " + nameof(SqlSpRepository) + " and will be deprecated in the future version")]
    public class SqlRepository : SqlSpRepository
    {
        #region Constructor

        /// <summary> Constructor. </summary>
        /// 
        /// <param name="sqlSimpleAccess"> The SQL connection. </param>
        public SqlRepository(ISqlSimpleAccess sqlSimpleAccess) : base(sqlSimpleAccess)  {  }

        /// <summary> Constructor. </summary>
        /// 
        /// <param name="connection"> The connection string. </param>
        public SqlRepository(string connection): base (connection) { }

        /// <summary> Default constructor. </summary>
        public SqlRepository(): base() {  }

        #endregion

    }

}
