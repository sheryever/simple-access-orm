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
    public class NotASpParameterAttribute : Attribute
    {

    }
}
