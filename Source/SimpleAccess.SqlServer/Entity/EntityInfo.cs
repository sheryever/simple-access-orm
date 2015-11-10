using System;
using System.Reflection;

namespace SimpleAccess.Entity
{
    /// <summary>
    /// Represents the SimpleAccess Entity information.
    /// The <see cref="EntityInfo"/> is only used for caching the stored procedure name
    /// </summary>
    public class EntityInfo
    {
        /// <summary>
        /// Initialize the new object
        /// </summary>
        /// <param name="type"> The Entity </param>
        public EntityInfo(Type type)
        {
            this.Type = type;
            LoadData();
        }

        private void LoadData()
        {
            LoadTypeName();
        }

        /// <summary>
        /// Simple Name of the Entity.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The Type of the Entity.
        /// </summary>
        public Type Type { get; set; }

        private void LoadTypeName()
        {
            var customAttr = Type.GetCustomAttribute<EntityAttribute>();
            Name  = customAttr != null ?  customAttr.EntityName :  this.Type.Name;
        }
    }
}
