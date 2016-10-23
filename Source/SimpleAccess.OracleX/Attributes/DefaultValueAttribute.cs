using System;

namespace SimpleAccess.Oracle
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
