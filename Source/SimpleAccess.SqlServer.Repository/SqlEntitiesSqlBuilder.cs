using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
#if !NETSTANDARD2_1
using System.Data.SqlClient;
#endif
#if NETSTANDARD2_1 || NET6_0_OR_GREATER
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
            var columns = new List<string>();
            var i = 0;

            foreach (var propertyInfo in properties)
            {
                if (propertyInfo.GetCustomAttributes(true).Any(a => a is IgnoreSelectAttribute || a is NotMappedAttribute))
                {
                    continue;
                }

                columns.Add($"[{propertyInfo.Name}]");
            }

            return $"SELECT {String.Join("\n\t, ", columns)}\n\t FROM {EntityInfo.DbObjectViewName}";
        }

        public override string GetGetAllStatement()
        {
            return SelectAllStatement ?? (SelectAllStatement = CreateSelectAllStatement());
        }

        public override string GetGetPagedListStatement()
        {
            var query = $@" SELECT @totalRows = COUNT(*) FROM {EntityInfo.DbObjectViewName}
                                    {{whereClause}};

                                SELECT {{columns}} FROM {EntityInfo.DbObjectViewName}
                                    {{whereClause}}
                                    ORDER BY {{sortExpression}}
                                  OFFSET @startIndex ROWS 
                                  FETCH NEXT @pageSize ROWS ONLY;
                                ";

            return query;
        }


        public string InsertStatement { get; private set; }

        public string CreateInsertStatement()
        {
            BuildEntityInsertParameters();

            //List<string> columns = new string[EntityInsertParameters.DataParametersDictionary.Count];
            List<string> columns = new List<string>(); //[EntityInsertParameters.DataParametersDictionary.Count];
            List<string> parameters = new List<string>();
            var keyProperty = "";
            SqlParameter sqlParameter = null;
            PrimaryKeyAttribute primaryKeyAttribute = null;
            var query = "";
            var i = 0;
            foreach (var parameterInfo in EntityInsertParameters.DataParametersDictionary)
            {
                sqlParameter = parameterInfo.Value;

                if (parameterInfo.Key.GetCustomAttributes(true).Any(a => a is IgnoreInsertAttribute || a is NotMappedAttribute))
                {
                    continue;
                }

                if (sqlParameter.Direction == ParameterDirection.InputOutput)
                {
                    primaryKeyAttribute =
                        parameterInfo.Key.GetCustomAttributes(true)
                        .FirstOrDefault(a => a is PrimaryKeyAttribute) as PrimaryKeyAttribute;
                    keyProperty = sqlParameter.ParameterName;
                    if (primaryKeyAttribute.IsIdentity) continue;
                }

                columns.Add($"[{sqlParameter.ParameterName.Substring(1)}]");
                parameters.Add(sqlParameter.ParameterName.Substring(1));
            }

            if ((primaryKeyAttribute == null) || (!primaryKeyAttribute.IsIdentity 
                && primaryKeyAttribute.DbSequence == null 
                && primaryKeyAttribute.UniqueIdGeneration == UniqueIdGeneration.None))
            {
                 query = $"INSERT INTO [{EntityInfo.DbObjectName}] \n\t({String.Join("\n\t, ", columns)})\n\t VALUES \n\t(@{String.Join("\n\t, @", parameters)});";
            }
            else if (primaryKeyAttribute.IsIdentity)
            {
                query = $"INSERT INTO [{EntityInfo.DbObjectName}] \n\t({String.Join("\n\t, ", columns)})\n\t VALUES \n\t(@{String.Join("\n\t, @", parameters)}); SELECT {keyProperty} = SCOPE_IDENTITY();";
            }
            else if (primaryKeyAttribute.UniqueIdGeneration == UniqueIdGeneration.Client)
            {
                var keySqlParameter = EntityInsertParameters.DataParametersDictionary.Values.First(p => p.ParameterName == keyProperty);
                keySqlParameter.Value = Guid.NewGuid().ToString();
                query = $"INSERT INTO [{EntityInfo.DbObjectName}] \n\t({String.Join("\n\t, ", columns)})\n\t VALUES \n\t(@{String.Join("\n\t, @", parameters)});";
            }
            else if (primaryKeyAttribute.UniqueIdGeneration == UniqueIdGeneration.Database)
            {
                var keySqlParameter = EntityInsertParameters.DataParametersDictionary.Values.First(p => p.ParameterName == keyProperty);
                keySqlParameter.Size = 40;

                query = $"SELECT {keyProperty} = NewId();\n INSERT INTO [{EntityInfo.DbObjectName}] \n\t({String.Join("\n\t, ", columns)})\n\t VALUES \n\t(@{String.Join("\n\t, @", parameters)});";
            }
            else if (primaryKeyAttribute.DbSequence != null)
            {
                query = $"SELECT {keyProperty} = NEXT VALUE FOR {primaryKeyAttribute.DbSequence};\n INSERT INTO [{EntityInfo.DbObjectName}] \n\t({String.Join("\n\t, ", columns)})\n\t VALUES \n\t(@{String.Join("\n\t, @", parameters)}); ";
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
            PrimaryKeyAttribute primaryKeyAttribute = null;
            var query = "";

            foreach (var parameterInfo in EntityUpdateParameters.DataParametersDictionary)
            {
                sqlParameter = parameterInfo.Value;
                var columnName = sqlParameter.ParameterName.Substring(1);
                var propertyInfo = parameterInfo.Key;

                if (parameterInfo.Key.GetCustomAttributes(true).Any(a => a is IgnoreUpdateAttribute || a is NotMappedAttribute))
                {
                    continue;
                }

                primaryKeyAttribute = propertyInfo.GetCustomAttributes(true).FirstOrDefault(a => a is PrimaryKeyAttribute) as PrimaryKeyAttribute;
                if (primaryKeyAttribute != null)
                {
                    keyProperty = columnName;
                    continue;
                }

                columnsSb.Append($" [{columnName}] = @{columnName},");


            }

            if (keyProperty == null)
            {
                throw new InvalidOperationException($"The object of type \"{EntityInfo.EntityType.FullName}\" cannot be updated without PrimaryKeyAttribute or IdentityAttribute. Please mark the primary key with PrimaryKeyAttribute or IdentityAttribute");
            }
            
            query = $"UPDATE {EntityInfo.DbObjectName} \n\tSET {columnsSb.ToString().Substring(0, columnsSb.Length-1)}\n\t WHERE \t[{keyProperty}] = @{keyProperty};";

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
            return $"DELETE [{EntityInfo.DbObjectName}]";
        }
        private string SoftDeleteStatement { get; set; }
        public override string GetSoftDeleteStatement()
        {
            return SoftDeleteStatement ?? (SoftDeleteStatement = $"UPDATE [{EntityInfo.DbObjectName}]\n\t SET IsDeleted = 1 WHERE Id = @id;");
        }

        private string DeleteAllStatement { get; set; }

        public string CreateDeleteAllStatement()
        {
            return $"DELETE [{EntityInfo.DbObjectName}]";
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