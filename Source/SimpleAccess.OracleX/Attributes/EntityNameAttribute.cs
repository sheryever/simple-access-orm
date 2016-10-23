using System;

namespace SimpleAccess.Oracle
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
