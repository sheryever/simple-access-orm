#pragma warning disable CS0246 // The type or namespace name 'System' could not be found (are you missing a using directive or an assembly reference?)
using System;
#pragma warning restore CS0246 // The type or namespace name 'System' could not be found (are you missing a using directive or an assembly reference?)

namespace SimpleAccess
{
    /// <summary>
    /// Use for mappint the property with database table column.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class DbColumnAttribute : Attribute
    {
        /// <summary>
        /// Database table column name.
        /// </summary>
        public string DbColumn { get; private set; }

        /// <summary>
        /// Initialize the attribute.
        /// </summary>
        /// <param name="dbColumn">Database table column name of the marked property.</param>
        public DbColumnAttribute(string dbColumn)
        {
            this.DbColumn = dbColumn;
        }
    }
}
