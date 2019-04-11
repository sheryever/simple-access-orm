#pragma warning disable CS0246 // The type or namespace name 'System' could not be found (are you missing a using directive or an assembly reference?)
using System;
#pragma warning restore CS0246 // The type or namespace name 'System' could not be found (are you missing a using directive or an assembly reference?)
#pragma warning disable CS0246 // The type or namespace name 'System' could not be found (are you missing a using directive or an assembly reference?)
using System.Collections.Generic;
#pragma warning restore CS0246 // The type or namespace name 'System' could not be found (are you missing a using directive or an assembly reference?)
#pragma warning disable CS0246 // The type or namespace name 'System' could not be found (are you missing a using directive or an assembly reference?)
using System.Data;
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
    /// Specifies that the ParameterDirection for a SqlParameter Property.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ParameterDirectionAttribute : Attribute
    {
        /// <summary>
        /// Direction of the Marked property in DbParameter
        /// </summary>
        public ParameterDirection SpParameterDirection { get; private set; }

        /// <summary>
        /// Specifies that the ParameterDirection for a SqlParameter Property.
        /// </summary>
        /// <param name="spParameterDirection"> Direction for the SqlParameter.</param>
        public ParameterDirectionAttribute(ParameterDirection spParameterDirection)
        {
            this.SpParameterDirection = spParameterDirection;
        }
    }
}
