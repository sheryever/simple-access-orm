using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
