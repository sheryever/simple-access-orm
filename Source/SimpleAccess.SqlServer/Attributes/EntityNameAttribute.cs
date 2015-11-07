using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAccess
{
    [AttributeUsage(AttributeTargets.Class)]
    public class EntityNameAttribute : Attribute
    {
        public string EntityName { get; private set; }

        public EntityNameAttribute(string name)
        {
            this.EntityName = name;
        }
    }
}
