using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleAccess;

namespace SimpleAccess.SQLite.TestNetCore2.Entities
{

    [Entity("People")]
    public class Person
    {
        [Identity]
        //[PrimaryKey("[dbo].[Seq_People]")]
        public int Id { get; set; }
        [NotMapped]
        public long RowNumber { get; set; }
        public Gender? Gender { get; set; }
        public string FullName { get; set; }

        public string Phone { get; set; }
        public string Address { get; set; }
        public decimal BasicSalary { get; set; }
        public int? Transport { get; set; }
        [NotMapped]
        public bool Alive { get; set; }

        [NotMapped]
        public virtual string ExtraField {get;set;}
    }

    public enum Gender
    {
        Male = 1,
        Female
    }
}
