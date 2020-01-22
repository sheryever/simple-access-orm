using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
#if !NETSTANDARD2_1
using System.Data.SqlClient;
#endif
#if NETSTANDARD2_1
using Microsoft.Data.SqlClient;
#endif
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SimpleAccess.Core;
using SimpleAccess.Core.Entity;

namespace SimpleAccess.SqlServer
{

    public class SqlEntitiesSqlBuilder : SqlServerSqlBuilder, ISqlBuilder<SqlParameter>
    {


        //public List<IDataParameter> DataParameters { get; set; }


        public override IDataParameter[] CreateSqlParametersFromProperties(ParametersType parametersType)
        {
            throw new NotImplementedException();
        }

        ///// <summary>
        ///// Create parameters from object properties
        ///// </summary>
        ///// <param name="parametersType"></param>
        ///// <returns></returns>
        //public IDataParameter[] CreateSqlParametersFromProperties(object entity)
        //{
        //    var entityParameters = new EntityParameters();

        //    var procedureType = entity.GetType();
        //    var propertiesForDataParams = procedureType.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Default);

        //    DataParameters =
        //        propertiesForDataParams.Select(propertyInfo => CreateDataParameter(propertyInfo, parametersType, propertiesForDataParams, OutParameterPropertyInfoCollection, OutParameters))
        //            .Where(p => p != null).ToList();

        //    return DataParameters.ToArray();
        //}


        public string SelectAllStatement { get; private set; }

        public string CreateSelectAllStatement()
        {
            var properties = EntityInfo.GetPropertyInfos();
            string[] columns = new string[properties.Length];
            var i = 0;
            foreach (var propertyInfo in properties)
            {
                columns[i++] = propertyInfo.Name;
            }

            return $"SELECT {String.Join("\n\t, ", columns)}\n\t FROM {EntityInfo.DbObjectViewName}";
        }

        public override string GetGetAllStatement()
        {
            return SelectAllStatement ?? (SelectAllStatement = CreateSelectAllStatement());
        }

        public string InsertStatement { get; private set; }

        public string CreateInsertStatement()
        {
            BuildEntityInsertParameters();

            string[] columns = new string[EntityInsertParameters.DataParametersDictionary.Count];
            var keyProperty = "";
            SqlParameter sqlParameter = null;
            KeyAttribute keyAttribute = null;
            var query = "";
            var i = 0;
            foreach (var parameterInfo in EntityInsertParameters.DataParametersDictionary)
            {
                sqlParameter = parameterInfo.Value;

                columns[i++] = sqlParameter.ParameterName.Substring(1);
                
                if (sqlParameter.Direction == ParameterDirection.InputOutput)
                {
                    keyAttribute =
                        parameterInfo.Key.GetCustomAttributes(true)
                        .FirstOrDefault(a => a is KeyAttribute) as KeyAttribute;
                    keyProperty = sqlParameter.ParameterName;
                }
            }

            if ((keyAttribute == null) || (!keyAttribute.IsIdentity && keyAttribute.DbSequence == null))
            {
                 query = $"INSERT INTO {EntityInfo.DbObjectName} \n\t({String.Join("\n\t, ", columns)})\n\t VALUES \n\t(@{String.Join("\n\t, @", columns)});";
            }
            else if (keyAttribute.IsIdentity)
            {
                query = $"INSERT INTO {EntityInfo.DbObjectName} \n\t({String.Join("\n\t, ", columns)})\n\t VALUES \n\t(@{String.Join("\n\t, @", columns)}); SELECT {keyProperty} = SCOPE_IDENTITY();";
            }
            else if (keyAttribute.DbSequence != null)
            {
                query = $"SELECT {keyProperty} = NEXT VALUE FOR {keyAttribute.DbSequence};\n INSERT INTO {EntityInfo.DbObjectName} \n\t({String.Join("\n\t, ", columns)})\n\t VALUES \n\t(@{String.Join("\n\t, @", columns)}); ";
            }

            return query;
        }

        public override string GetInsertStatement()
        {
            return InsertStatement ?? (InsertStatement = CreateInsertStatement());
        }

        public string UpdateStatement { get; private set; }
        public string CreateUpdateStatement()
        {
            BuildEntityUpdateParameters();
            var columnsSb = new StringBuilder();
            var keyProperty = "";
            SqlParameter sqlParameter = null;
            KeyAttribute keyAttribute = null;
            var query = "";

            foreach (var parameterInfo in EntityUpdateParameters.DataParametersDictionary)
            {
                sqlParameter = parameterInfo.Value;
                var columnName = sqlParameter.ParameterName.Substring(1);
                var propertyInfo = parameterInfo.Key;

                columnsSb.Append($" {columnName} = @{columnName},");

                keyAttribute = propertyInfo.GetCustomAttributes(true).FirstOrDefault(a => a is KeyAttribute) as KeyAttribute;
                if (keyAttribute != null)
                {
                    keyProperty = columnName;
                }
            }

            if (keyProperty == null)
            {
                throw new InvalidOperationException($"The object of type \"{EntityInfo.EntityType.FullName}\" cannot be updated without KeyAttribute or IdentityAttribute. Please mark the primary key with KeyAttribute or IdentityAttribute");
            }
            
            query = $"UPDATE {EntityInfo.DbObjectName} \n\tSET {columnsSb.ToString().Substring(0, columnsSb.Length-1)}\n\t WHERE \t{keyProperty} = @{keyProperty};";

            return query;
        }

        public override string GetUpdateStatement()
        {
            return UpdateStatement ?? (UpdateStatement = CreateUpdateStatement());
        }

        private string DeleteStatement { get; set; }
        public override string GetDeleteStatement()
        {
            return DeleteStatement ?? (DeleteStatement = CreateDeleteStatement() + " WHERE Id = @id;");
        }
        public string CreateDeleteStatement()
        {
            return $"DELETE {EntityInfo.DbObjectName}";
        }
        private string SoftDeleteStatement { get; set; }
        public override string GetSoftDeleteStatement()
        {
            return SoftDeleteStatement ?? (SoftDeleteStatement = $"UPDATE {EntityInfo.DbObjectName}\n\t SET IsDeleted = 1 WHERE Id = @id");
        }

        private string DeleteAllStatement { get; set; }

        public string CreateDeleteAllStatement()
        {
            return $"DELETE {EntityInfo.DbObjectName}; ";
        }

        public override string GetDeleteAllStatement()
        {
            return DeleteAllStatement ?? (DeleteAllStatement = CreateDeleteAllStatement());
        }

        public override string GetGetByIdStatement()
        {
            return SelectAllStatement ?? (SelectAllStatement = CreateSelectAllStatement());
        }

        public override string GetFindStatement()
        {
            return SelectAllStatement ?? (SelectAllStatement = CreateSelectAllStatement());
        }
    }
}