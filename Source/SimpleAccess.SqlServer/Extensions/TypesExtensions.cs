using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleAccess.SqlServer
{
    public static class TypesExtensions
    {

        public static string CreateWhereWithAnd(this object source)
        {
            var whereClause = "WHERE ";
            var sets = new List<string>();


            foreach (var propertyInfo in source.GetType().GetProperties())
            {
                sets.Add($"{propertyInfo.Name.Replace("@", "")} = @{propertyInfo.Name}");
            }

            return whereClause + string.Join(" AND ", sets.ToArray());
        }
    }
}
