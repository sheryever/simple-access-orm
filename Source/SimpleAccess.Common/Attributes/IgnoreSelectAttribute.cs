using System;

namespace SimpleAccess
{
    /// <summary>
    /// Use for Ignore the property in Select statement.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class IgnoreSelectAttribute : Attribute
    {
    }
}
