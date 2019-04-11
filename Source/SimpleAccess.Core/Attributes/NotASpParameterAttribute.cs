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
    /// Specifies that the property is not an SqlParameter.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    [Obsolete("Declare the property as virtual to have same behavior.")]
    public class NotASpParameterAttribute : Attribute
    {

    }
}
