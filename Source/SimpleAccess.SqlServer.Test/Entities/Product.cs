using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAccess.SqlServer.Test
{
    [Entity("Products")]
    public class ProductDatabaseUniqueIdentifier
    {
        [PrimaryKey(UniqueIdGeneration = UniqueIdGeneration.Database)]
        public string Id { get; set; }
        public string Name { get; set; }
    }

    [Entity("Products")]
    public class ProductCleintGuid
    {
        [PrimaryKey(UniqueIdGeneration = UniqueIdGeneration.Client)]
        public string Id { get; set; }
        public string Name { get; set; }
    }

    [Entity("Products")]
    public class ProductUserDefine
    {
        [PrimaryKey(UniqueIdGeneration = UniqueIdGeneration.None)]
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
