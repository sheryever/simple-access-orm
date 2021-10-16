using System;
using System.Collections.Generic;

namespace SimpleAccess.Repository
{
    public class PagedData<TEntity>
        where TEntity : class, new()
    {
        public IEnumerable<TEntity> Data { get; set; }
        public long TotalRows { get; set; }
    }
}
