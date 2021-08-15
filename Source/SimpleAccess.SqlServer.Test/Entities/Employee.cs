using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleAccess;

namespace SimpleAccess.SqlServer.TestNetCore2.Entities
{

    [Entity("Employees")]
    public class Employee
    {
        //[Identity]
        [PrimaryKey("[dbo].[Seq_Employees]")]
        public int Id { get; set; }

        public string FullName { get; set; }
        public bool IsOnDuty { get; set; }

        public string Phone { get; set; }
        public string Address { get; set; }
        public decimal BasicSalary { get; set; }
        public int? Transport { get; set; }
        public decimal? Inssurance { get; set; }
        public string Department { get; set; }

    }
}
