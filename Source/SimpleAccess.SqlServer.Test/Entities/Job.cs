using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleAccess;

namespace SimpleAccess.SqlServer.TestNetCore2.Entities
{

    [Entity("Jobs")]
    public class Job
    {
        //[Identity]
        [PrimaryKey("[dbo].[Seq_Jobs]")]
        public int Id { get; set; }
        public string Name { get; set; }
    }

}
