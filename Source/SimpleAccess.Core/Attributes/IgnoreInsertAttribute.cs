using System;

namespace SimpleAccess
{
    /// <summary>
    /// Use for Ignore the property in Insert statement.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class IgnoreInsertAttribute : Attribute
    {
    }
}
