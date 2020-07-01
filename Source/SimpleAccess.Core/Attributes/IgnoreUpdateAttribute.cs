using System;

namespace SimpleAccess
{
    /// <summary>
    /// Use for Ignore the property in Update statement.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class IgnoreUpdateAttribute : Attribute
    {
    }
}
