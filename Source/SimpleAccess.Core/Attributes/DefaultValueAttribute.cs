#pragma warning disable CS0246 // The type or namespace name 'System' could not be found (are you missing a using directive or an assembly reference?)
using System;
#pragma warning restore CS0246 // The type or namespace name 'System' could not be found (are you missing a using directive or an assembly reference?)
#pragma warning disable CS0246 // The type or namespace name 'System' could not be found (are you missing a using directive or an assembly reference?)
using System.Collections.Generic;
#pragma warning restore CS0246 // The type or namespace name 'System' could not be found (are you missing a using directive or an assembly reference?)
#pragma warning disable CS0246 // The type or namespace name 'System' could not be found (are you missing a using directive or an assembly reference?)
using System.Linq;
#pragma warning restore CS0246 // The type or namespace name 'System' could not be found (are you missing a using directive or an assembly reference?)
#pragma warning disable CS0246 // The type or namespace name 'System' could not be found (are you missing a using directive or an assembly reference?)
using System.Text;
#pragma warning restore CS0246 // The type or namespace name 'System' could not be found (are you missing a using directive or an assembly reference?)
#pragma warning disable CS0246 // The type or namespace name 'System' could not be found (are you missing a using directive or an assembly reference?)
using System.Threading.Tasks;
#pragma warning restore CS0246 // The type or namespace name 'System' could not be found (are you missing a using directive or an assembly reference?)

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
