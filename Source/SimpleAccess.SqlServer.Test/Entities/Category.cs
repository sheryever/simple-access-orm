﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleAccess;

namespace SimpleAccess.SqlServer.TestNetCore2.Entities
{

    [Entity("Categories")]
    public class Category
    {
        [Identity]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [NotMapped]
        public int? DummyField { get; set; }
    }
}