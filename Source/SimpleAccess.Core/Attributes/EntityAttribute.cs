#pragma warning disable CS0246 // The type or namespace name 'System' could not be found (are you missing a using directive or an assembly reference?)
using System;
#pragma warning restore CS0246 // The type or namespace name 'System' could not be found (are you missing a using directive or an assembly reference?)
#pragma warning disable CS0246 // The type or namespace name 'System' could not be found (are you missing a using directive or an assembly reference?)
using System.Collections.Generic;
#pragma warning restore CS0246 // The type or namespace name 'System' could not be found (are you missing a using directive or an assembly reference?)
#pragma warning disable CS0246 // The type or namespace name 'System' could not be found (are you missing a using directive or an assembly reference?)
using System.Data;
#pragma warning restore CS0246 // The type or namespace name 'System' could not be found (are you missing a using directive or an assembly reference?)
#pragma warning disable CS0246 // The type or namespace name 'System' could not be found (are you missing a using directive or an assembly reference?)
using System.Linq;
#pragma warning restore CS0246 // The type or namespace name 'System' could not be found (are you missing a using directive or an assembly reference?)
#pragma warning disable CS0246 // The type or namespace name 'System' could not be found (are you missing a using directive or an assembly reference?)
using System.Text;
#pragma warning restore CS0246 // The type or namespace name 'System' could not be found (are you missing a using directive or an assembly reference?)
#pragma warning disable CS0246 // The type or namespace name 'System' could not be found (are you missing a using directive or an assembly reference?)
using System.Threading.Tasks;
#pragma warning restore CS0246 // The type or namespace name 'System' could not be found (are you missing a using directive or an assembly reference?)

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
