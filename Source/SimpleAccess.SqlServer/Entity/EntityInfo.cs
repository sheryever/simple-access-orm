using System;
using System.Reflection;

namespace SimpleAccess.Entity
{
    public class EntityInfo
    {
        public EntityInfo(Type type)
        {
            this.Type = type;
            LoadData();
        }

        private void LoadData()
        {
            LoadTypeName();
        }

        public string Name { get; set; }
        public Type Type { get; set; }

        private void LoadTypeName()
        {
            var customAttr = Type.GetCustomAttribute<EntityAttribute>();
            Name  = customAttr != null ?  customAttr.EntityName :  this.Type.Name;
        }
    }
}
