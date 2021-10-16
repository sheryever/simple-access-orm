using System;
using System.Collections.Generic;
using System.Data;
#if !NETSTANDARD2_1
using System.Data.SqlClient;
#endif
#if NETSTANDARD2_1
using Microsoft.Data.SqlClient;
#endif
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using SimpleAccess.Core.Entity.RepoWrapper;

namespace SimpleAccess.SqlServer
{
    public class PagedData<TEntity>
        where TEntity : class, new()
    {
        public IEnumerable<TEntity> Data { get; set; }
        public long TotalRows { get; set; }
    }
}
