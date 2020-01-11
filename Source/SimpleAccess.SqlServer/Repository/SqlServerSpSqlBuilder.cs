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

    public class SqlServerSpSqlBuilder : SqlServerSqlBuilder, ISqlBuilder<SqlParameter>
    {

        private EntityInfo<SqlServerSpSqlBuilder, SqlParameter> _entityInfo;

        //public List<IDataParameter> DataParameters { get; set; }

        public override void InitSqlBuilder(object entityInfo)
        {
            _entityInfo = entityInfo as EntityInfo<SqlServerSpSqlBuilder, SqlParameter>;

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
        
        public string GetSpGetAllStatement()
        {
            return string.Format(SqlSpRepositorySetting.SpGetAllPattern, _entityInfo.DbObjectName);
        }

        public string GetSpGetByIdStatement()
        {
            return string.Format(SqlSpRepositorySetting.SpGetByIdPattern, _entityInfo.DbObjectName);
        }

        public string GetSpFindStatement()
        {
            return string.Format(SqlSpRepositorySetting.SpFindPattern, _entityInfo.DbObjectName);
        }

        public string GetSpInsertStatement()
        {
            return string.Format(SqlSpRepositorySetting.SpInsertPattern, _entityInfo.DbObjectName);
        }

        public string GetSpUpdateStatement()
        {
            return string.Format(SqlSpRepositorySetting.SpUpdatePattern, _entityInfo.DbObjectName);
        }

        public string GetSpDeleteStatement()
        {
            return string.Format(SqlSpRepositorySetting.SpDeletePattern, _entityInfo.DbObjectName);
        }

        public string GetSpDeleteAllStatement()
        {
            return string.Format(SqlSpRepositorySetting.SpDeleteAllPattern, _entityInfo.DbObjectName);
        }

        public string GetSpSoftDeleteStatement()
        {
            return string.Format(SqlSpRepositorySetting.SpSoftDeletePattern, _entityInfo.DbObjectName);
        }

        public override string GetSelectStatement()
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
    }
}