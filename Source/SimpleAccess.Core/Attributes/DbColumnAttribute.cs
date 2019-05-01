using System;

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
            DbColumn = dbColumn;
        }
    }
}
