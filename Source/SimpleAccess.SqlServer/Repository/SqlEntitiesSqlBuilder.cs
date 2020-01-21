using System;
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
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SimpleAccess.Core;
using SimpleAccess.Core.Entity;

namespace SimpleAccess.SqlServer
{

    public class SqlEntitiesSqlBuilder : SqlServerSqlBuilder, ISqlBuilder<SqlParameter>
    {

        private EntityInfo<SqlEntitiesSqlBuilder, SqlParameter> _entityInfo;

        //public List<IDataParameter> DataParameters { get; set; }

        public override void InitSqlBuilder(object entityInfo)
        {
            _entityInfo = entityInfo as EntityInfo<SqlEntitiesSqlBuilder, SqlParameter>;

            if (_entityInfo == null)
            {
                throw new InvalidOperationException("Invalid entityInfo parameter, while 'EntityInfo<SqlServerSqlBuilder, SqlParameter>' is required.");
            }
        }
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
        public string InsertStatement { get; private set; }
        public string UpdateStatement { get; private set; }
        public string DeleteStatement { get; private set; }

        public string CreateSelectAllStatement(EntityInfo<SqlEntitiesSqlBuilder, SqlParameter> entityInfo)
        {
            var properties = entityInfo.EntityType.GetProperties();
            string[] columns = new string[properties.Length];
            var i = 0;
            foreach (var propertyInfo in properties)
            {
                columns[i++] = propertyInfo.Name;
            }

            return $"SELECT {String.Join("\n\t, ", columns)}\n\t FROM {entityInfo.DbObjectViewName}";
        }

        public string GetSelectStatement()
        {
            SelectAllStatement  = SelectAllStatement ??
                                  CreateSelectAllStatement(_entityInfo);

            return SelectAllStatement;
        }

        public string CreateInsertStatement(EntityInfo<SqlEntitiesSqlBuilder, SqlParameter> entityInfo)
        {
            var properties = entityInfo.EntityType.GetProperties();
            string[] columns = new string[properties.Length];
            var i = 0;
            foreach (var propertyInfo in properties)
            {
                columns[i++] = propertyInfo.Name;
            }

            return $"SELECT {String.Join("\n\t, ", columns)}\n\t FROM {entityInfo.DbObjectViewName}";
        }

        public override string GetDeleteAllStatement()
        {
            throw new NotImplementedException();
        }

        public override string GetInsertStatement()
        {
            throw new NotImplementedException();
        }

        public override string GetUpdateStatement()
        {
            throw new NotImplementedException();
        }

        public override string GetDeleteStatement()
        {
            throw new NotImplementedException();
        }

        public override string GetSoftDeleteStatement()
        {
            throw new NotImplementedException();
        }

        public override string GetGetAllStatement()
        {
            throw new NotImplementedException();
        }

        public override string GetGetByIdStatement()
        {
            throw new NotImplementedException();
        }

        public override string GetFindStatement()
        {
            throw new NotImplementedException();
        }
    }
}