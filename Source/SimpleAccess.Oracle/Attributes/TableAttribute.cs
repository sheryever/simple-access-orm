using System;

namespace SimpleAccess.Oracle
{

    /// <summary>
    /// Specifies the database table/view name of the Entity.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class EntityAttribute : Attribute
    {
        /// <summary>
        /// Database table/view name.
        /// </summary>
        public string EntityName { get; private set; }

        /// <summary>
        /// Specifies the database table/view name of the Entity.
        /// </summary>
        /// <param name="entityName"> Table/View name.</param>
        public EntityAttribute(string entityName)
        {
            this.EntityName = entityName;
        }
    }
}
