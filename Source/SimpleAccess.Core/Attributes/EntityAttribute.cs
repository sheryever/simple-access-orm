using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAccess
{

    /// <summary>
    /// Specifies the database table/view name of the Entity.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class EntityAttribute : Attribute
    {
        /// <summary>
        /// Database table/view name.
        /// </summary>
        public string EntityName { get; private set; }

        /// <summary>
        /// Object type in the database (Table/View)
        /// </summary>
        public DbObjectType Type { get; set; }


        /// <summary>
        /// Specifies the database table/view name of the Entity.
        /// </summary>
        /// <param name="entityName"> Table/View name.</param>
        public EntityAttribute(string entityName)
        {
            this.EntityName = entityName;
        }

        /// <summary>
        /// Specifies the database table/view name of the Entity.
        /// </summary>
        /// <param name="entityName"> Table/View name.</param>
        /// <param neme="dbObjectType"> Type of the entity in the database. </param>
        public EntityAttribute(string entityName, DbObjectType dbObjectType)
        {
            this.EntityName = entityName;
            Type = dbObjectType;
        }
    }

    /// <summary>
    /// Type of the entity in the database
    /// </summary>
    public enum DbObjectType
    {
        /// <summary>
        /// Table in the Database
        /// </summary>
        Table,
        /// <summary>
        /// View in the Database
        /// </summary>
        View
    }
}
