using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleAccess;

namespace SimpleAccess.SqlServer.TestNetCore2.Entities
{

    [Entity("EmployeeJobs")]
    public class EmployeeJob
    {
        [PrimaryKey()]
        public int EmployeeId { get; set; }
        [PrimaryKey()]
        public int JobId { get; set; }
    }

}
