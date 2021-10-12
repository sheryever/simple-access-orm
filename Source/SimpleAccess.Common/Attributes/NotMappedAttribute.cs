using System;

namespace SimpleAccess
{
    /// <summary>
    /// Use for Ignore the property in all database operations.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class NotMappedAttribute : Attribute
    {
    }
}
