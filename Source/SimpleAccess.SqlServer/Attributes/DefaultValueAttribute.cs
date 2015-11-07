using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAccess
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DefaultValueAttribute : Attribute
    {
        public object DefaultValue { get; private set; }

        public DefaultValueAttribute(object value)
        {
            this.DefaultValue = value;
        }

        public DefaultValueAttribute(DateTime value)
        {
            this.DefaultValue = value;
        }
    }
}
