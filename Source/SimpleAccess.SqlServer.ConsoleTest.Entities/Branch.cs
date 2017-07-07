using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleAccess;

namespace SimpleAccess.SqlServer.ConsoleTest.Entities
{

    [Entity("Branches")]
    public class Branch
    {
        [Identity]
        public int Id { get; set; }

        public int CityId { get; set; }
        public string Name { get; set; }
        public string PhoneNumbers { get; set; }
        public string Address { get; set; }

        public string Address2 { get; set; }
    }
}
