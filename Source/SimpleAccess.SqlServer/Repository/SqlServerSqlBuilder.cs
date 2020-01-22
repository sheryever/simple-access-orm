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
using SimpleAccess.Core.Extensions;

namespace SimpleAccess.SqlServer
{

    public abstract class SqlServerSqlBuilder : ISqlBuilder<SqlParameter>
    {

        protected EntityParameters<SqlParameter> EntityInsertParameters { get; set; }
        protected EntityParameters<SqlParameter> EntityUpdateParameters { get; set; }
        public List<PropertyInfo> OutParameterPropertyInfoCollection { get; set; }
        public IEntityInfo EntityInfo { get; set; }

        public abstract string GetGetAllStatement();

        public abstract string GetGetByIdStatement();

        public abstract string GetFindStatement();
        public abstract string GetDeleteAllStatement();

        public abstract string GetInsertStatement();

        public abstract string GetUpdateStatement();

        public abstract string GetDeleteStatement();
        public abstract string GetSoftDeleteStatement();

        public void InitSqlBuilder(IEntityInfo entityInfo)
        {
            if (entityInfo == null)
            {
                throw new NullReferenceException("Invalid entityInfo parameter");
            }

            EntityInfo = entityInfo;// as EntityInfo<SqlEntitiesSqlBuilder, SqlParameter>;

        }
        public abstract IDataParameter[] CreateSqlParametersFromProperties(ParametersType parametersType);
        /// <summary>
        /// Create parameters from object properties
        /// </summary>
        /// <returns></returns>
        public EntityParameters<SqlParameter> CreateEntityParameters(bool checkForIdentityColumn)
        {

            var entityParameters = EntityParameters<SqlParameter>.Create((dataParameters, outParamsDictionary, checkForIdentity) =>
            {
                var propertiesForDataParams = EntityInfo.GetPropertyInfos();

                foreach (var propertyInfo in propertiesForDataParams)
                {
                    CreateDataParameter(propertyInfo, dataParameters, outParamsDictionary, checkForIdentity);
                }

            }, checkForIdentityColumn);

            return entityParameters;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyInfo"></param>
        /// <returns></returns>
        public void CreateDataParameter(PropertyInfo propertyInfo, IDictionary<PropertyInfo, SqlParameter> dataParameters, IDictionary<PropertyInfo, SqlParameter> outParamsDictionary, bool checkForIdentity)
        {
            var getMethodInfo = propertyInfo.GetGetMethod();
            if (getMethodInfo.IsVirtual && !getMethodInfo.IsFinal)
                return;

            var sqlParam = new SqlParameter();

            var attrbutes = propertyInfo.GetCustomAttributes(true);


            var dbColumnAttribute =
                attrbutes.FirstOrDefault(a => a is DbColumnAttribute) as DbColumnAttribute;

            if (dbColumnAttribute == null)
            {
                sqlParam.ParameterName = string.Format("@{0}", propertyInfo.Name);
            }
            else
            {
                sqlParam.ParameterName = string.Format("@{0}", dbColumnAttribute.DbColumn);
            }

            var outParaAttr = attrbutes.FirstOrDefault(a => a is ParameterDirectionAttribute) as ParameterDirectionAttribute;
            if (outParaAttr != null)
            {
                sqlParam.Direction = outParaAttr.SpParameterDirection;
                outParamsDictionary.Add(propertyInfo, sqlParam);
            }

            if (checkForIdentity)
            {
                var keyAttribute = attrbutes.FirstOrDefault(a => a is KeyAttribute) as KeyAttribute;

                if ((keyAttribute != null) && (keyAttribute.IsIdentity || keyAttribute.DbSequence != null))
                {
                    sqlParam.Direction = ParameterDirection.InputOutput;
                    outParamsDictionary.Add(propertyInfo, sqlParam);
                }

            }

            Debug.WriteLine(sqlParam.ParameterName);
            dataParameters.Add(propertyInfo, sqlParam);
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


        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyInfo"></param>
        /// <param name="parametersType"></param>
        /// <param name="propertyInfos"></param>
        /// <returns></returns>
        public IDataParameter CreateDataParameter(PropertyInfo propertyInfo, ParametersType parametersType,
            IEnumerable<PropertyInfo> propertyInfos, IList<PropertyInfo> outParameterPropertyInfoCollection, List<IDataParameter> outDataParameters)
        {
            object value = propertyInfo.GetValue(this, new object[] { });

            var sqlParam = new SqlParameter(string.Format("@{0}", propertyInfo.Name), value);

            if (propertyInfo.PropertyType.Name == "String" && value != null)
            {
                value = SafeSqlLiteral(value.ToString());
            }

            if ((propertyInfo.PropertyType.IsGenericType
                || propertyInfo.PropertyType.Name == "String") & value == null)
            {
                sqlParam.IsNullable = true;
                sqlParam.Value = DBNull.Value;
            }

            var attrbutes = propertyInfo.GetCustomAttributes(true);

            if (propertyInfo.GetGetMethod().IsVirtual)
                return null;

            //if (attrbutes.FirstOrDefault(a => a is NotASpParameterAttribute) != null)
            //    return null;

            
            var dbColumnAttribute =
                attrbutes.FirstOrDefault(a => a is DbColumnAttribute) as DbColumnAttribute;

            if (dbColumnAttribute != null)
            {
                sqlParam.ParameterName = string.Format("@{0}", dbColumnAttribute.DbColumn);
            }



            if (parametersType == ParametersType.Insert)
            {
                //var propertyDataType = propertyInfo.DeclaringType;

                var outParaAttr = attrbutes.FirstOrDefault(a => a is ParameterDirectionAttribute) as ParameterDirectionAttribute;
                if (outParaAttr != null)
                {
                    sqlParam.Direction = outParaAttr.SpParameterDirection;
                    outParameterPropertyInfoCollection.Add(propertyInfo);
                    outDataParameters.Add(sqlParam);
                }

                //if (propertyInfo.PropertyType.GetType() is DateTime
                //    || propertyInfo.PropertyType.GetType() is DateTime?)
                //{
                //    value = value == null || (DateTime)value == DateTime.MinValue ? new DateTime(2000, 1, 1) : value;                       
                //}
            }

            Debug.WriteLine(sqlParam.ParameterName);
            return sqlParam;
        }

        public void BuildEntityInsertParameters()
        {
            EntityInsertParameters = EntityInsertParameters ?? CreateEntityParameters(true);
        }


        public EntityParameters<SqlParameter> GetInsertParameters(object entity)
        {
            BuildEntityInsertParameters();

            EntityInsertParameters.FillParameters(entity, FillInsertParameters);

            return EntityInsertParameters;
        }

        public void FillInsertParameters(object entity, IDictionary<PropertyInfo, SqlParameter> parameters)
        {

            foreach (var dataParameter in parameters)
            {
                var propertyInfo = dataParameter.Key;
                var sqlParam = dataParameter.Value;

                object value = propertyInfo.GetValue(entity, new object[] {});


                if (propertyInfo.PropertyType.Name == "String" && value != null)
                {
                    value = SafeSqlLiteral(value.ToString());
                }

                if ((propertyInfo.PropertyType.IsGenericType
                    || propertyInfo.PropertyType.Name == "String") & value == null)
                {
                    ((SqlParameter) sqlParam).IsNullable = true;
                    sqlParam.Value = DBNull.Value;
                    continue;
                }

                sqlParam.Value = value;
                Debug.WriteLine("Filling SqlParameter: {0}", sqlParam.ParameterName);
            }


        }

        public void BuildEntityUpdateParameters()
        {
            EntityUpdateParameters = EntityUpdateParameters ?? CreateEntityParameters(false);
        }


        public EntityParameters<SqlParameter> GetUpdateParameters(object entity)
        {
            BuildEntityUpdateParameters();

            EntityUpdateParameters.FillParameters(entity, FillInsertParameters);

            return EntityUpdateParameters;
        }

        public string BuildWhereExpression(string propertyName, Type valueType, string @operator, object value)
        {
            var result = "";
            if (value == null && @operator == "=")
            {
                return string.Format(" {0} is null", propertyName);
            }
            if (@operator == "EndsWith")
            {
                return string.Format(" {0} LIKE '%{1}'", propertyName, SafeSqlLiteral(value.ToString()));
            }
            if (@operator == "StartsWith")
            {
                return string.Format(" {0} LIKE '{1}%'", propertyName, SafeSqlLiteral(value.ToString()));
            }
            if (@operator == "Contains")
            {
                return string.Format(" {0} LIKE '%{1}%'", propertyName, SafeSqlLiteral(value.ToString()));
            }
            if (valueType == typeof(bool))
            {
                result = string.Format(" {0} {1} {2} ", propertyName, @operator, (bool)value ? "1" : "0");
            }
            else if (valueType.In(typeof(string), typeof(TimeSpan), typeof(TimeSpan?), typeof(DateTime),
                typeof(DateTime?)))
            {
                result = string.Format("{0} {1} '{2}' ", propertyName, @operator, SafeSqlLiteral(value.ToString()));
            }
            else if (valueType.In(typeof(Int16), typeof(Int16?), typeof(int), typeof(int?), typeof(Int32), typeof(Int32?), typeof(Int64), typeof(Int64?), typeof(Single), typeof(Single?),
                                typeof(float), typeof(float?), typeof(decimal), typeof(decimal?), typeof(double), typeof(double?)))
            {
                result = string.Format("{0} {1} {2} ", propertyName, @operator, value.ToString());
            }
            else
            {
                throw new ArgumentException($"Invalid augument type {valueType.Name}");
            }

            return result;
        }

        /// <summary>
        /// Clear all DbParameters
        /// </summary>
        public void ClearDbParameters()
        {

            EntityInsertParameters = null;
            EntityUpdateParameters = null;

        }

        /// <summary>
        /// Load all the properties from DbParameters which were marked as ParameterDirection.Out
        /// </summary>
        /// <param name="entityParameters">The EntityParameters object based on TDataParameters in ISimpleAccess</param>
        /// <param name="instance"> The instance of object </param>
        public void LoadOutParametersProperties(EntityParameters<SqlParameter> entityParameters, object instance)
        {
            entityParameters.LoadOutParametersProperties(instance);
        }

        private string SafeSqlLiteral(string inputSql)
        {
            return inputSql.Replace("'", "''");
        }

    }


}