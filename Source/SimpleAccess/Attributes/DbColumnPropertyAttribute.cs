using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAccess
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DbColumnPropertyAttribute : Attribute
    {
        public string DbColumnProperty { get; private set; }

        public DbColumnPropertyAttribute(string value)
        {
            this.DbColumnProperty = value;
        }
    }
}
