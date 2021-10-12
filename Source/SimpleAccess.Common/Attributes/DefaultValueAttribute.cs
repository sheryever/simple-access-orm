using System;

namespace SimpleAccess
{
    /// <summary>
    /// Used to set the default value on Entity Property
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class DefaultValueAttribute : Attribute
    {
        /// <summary>
        /// Default value on the marked property
        /// </summary>
        public object DefaultValue { get; private set; }

        /// <summary>
        /// Initialize the DefaultValueAttribute
        /// </summary>
        /// <param name="value"> All type of value except DataTime </param>
        public DefaultValueAttribute(object value)
        {
            this.DefaultValue = value;
        }

        /// <summary>
        /// Initialize the DefaultValueAttribute
        /// </summary>
        /// <param name="value"> The DataTime value </param>
        public DefaultValueAttribute(DateTime value)
        {
            this.DefaultValue = value;
        }
    }
}
