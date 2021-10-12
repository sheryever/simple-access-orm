using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using SimpleAccess.Core.Entity.RepoWrapper;

namespace SimpleAccess.Repository
{
    public class PagedData<TEntity>
        where TEntity : class, new()
    {
        public IEnumerable<TEntity> Data { get; set; }
        public long TotalRows { get; set; }
    }
}
