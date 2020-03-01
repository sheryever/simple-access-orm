using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleAccess;

namespace SimpleAccess.SqlServer.TestNetCore2.Entities
{

    [Entity("People")]
    public class Person
    {
        //[Identity]
        [PrimaryKey("[dbo].[Seq_People]")]
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Phone { get; set; }
    }
}
