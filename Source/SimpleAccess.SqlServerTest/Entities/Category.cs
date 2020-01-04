using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleAccess;

namespace SimpleAccess.SqlServerTestNetCore2.Entities
{

    [Entity("Category")]
    public class Category
    {
        [Identity]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
