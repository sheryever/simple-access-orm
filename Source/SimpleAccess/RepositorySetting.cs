using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleAccess.Entity;

namespace SimpleAccess
{
    public static class RepositorySetting
    {
        static RepositorySetting()
        {
            EntityInfos = new Dictionary<int, EntityInfo>();
        }

        public static Dictionary<int,EntityInfo> EntityInfos { get; set; }

        public static EntityInfo GetEntityInfo(Type type)
        {
            EntityInfo entityInfo = null;
            if (EntityInfos.TryGetValue(type.GetHashCode(), out entityInfo))
                return entityInfo;

            entityInfo = new EntityInfo(type);
            EntityInfos.Add(type.GetHashCode(), entityInfo);

            return entityInfo;
        }
    }
}
