using System;

namespace SimpleAccess.Oracle
{

    /// <summary>
    /// Specifies that the property is not an SqlParameter.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class NotASpParameterAttribute : Attribute
    {

    }
}
