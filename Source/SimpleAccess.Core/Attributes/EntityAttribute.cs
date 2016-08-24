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
        /// Database default view name for select.
        /// </summary>
        public string DefaultView { get; set; }

        /// <summary>
        /// Specifies the database table/view name of the Entity.
        /// </summary>
        /// <param name="entityName"> Table/View name.</param>
        public EntityAttribute(string entityName)
        {
            if (string.IsNullOrEmpty(entityName))
            {
                throw new ArgumentNullException(nameof(entityName));
            }
            DefaultView = EntityName = entityName;
            
        }

        /// <summary>
        /// Specifies the database table/view name of the Entity.
        /// </summary>
        /// <param name="entityName"> Table/View name.</param>
        /// <param neme="defaultView"> Type of the entity in the database. </param>
        public EntityAttribute(string entityName, string defaultView)
        {
            if (string.IsNullOrEmpty(entityName))
            {
                throw new ArgumentNullException(nameof(entityName));
            }

            if (string.IsNullOrEmpty(defaultView))
            {
                throw new ArgumentNullException(nameof(defaultView));
            }

            EntityName = entityName;
            DefaultView = defaultView;
        }
    }
}
