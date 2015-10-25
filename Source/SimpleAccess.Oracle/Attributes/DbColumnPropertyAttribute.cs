using System;

namespace SimpleAccess.Oracle
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
