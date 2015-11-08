using System;

namespace SimpleAccess
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DbColumnAttribute : Attribute
    {
        public string DbColumn { get; private set; }

        public DbColumnAttribute(string value)
        {
            this.DbColumn = value;
        }
    }
}
