

using SimpleAccess.Core.Entity;
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
using System.Reflection;
#pragma warning restore CS0246 // The type or namespace name 'System' could not be found (are you missing a using directive or an assembly reference?)

namespace SimpleAccess.Core
{
    public interface ISqlBuilder<TDbParameter>
        where TDbParameter : IDataParameter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyInfo"></param>
        /// <param name="parametersType"></param>
        /// <param name="propertyInfos"></param>
        /// <param name="outParameterPropertyInfoCollection"></param>
        /// <param name="outDataParameters"></param>
        /// <returns></returns>
        IDataParameter CreateDataParameter(PropertyInfo propertyInfo, ParametersType parametersType,
            IEnumerable<PropertyInfo> propertyInfos, IList<PropertyInfo> outParameterPropertyInfoCollection , List<IDataParameter> outDataParameters );

        EntityParameters<TDbParameter> CreateEntityParameters(bool createInsertParameter);

        IEntityInfo EntityInfo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        void InitSqlBuilder(IEntityInfo entityInfo);


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string GetInsertStatement();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string GetUpdateStatement();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string GetDeleteStatement();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string GetDeleteAllStatement();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string GetSoftDeleteStatement();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string GetGetAllStatement();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string GetGetPagedListStatement();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string GetGetByIdStatement();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string GetFindStatement();



        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        EntityParameters<TDbParameter> GetInsertParameters(object entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        EntityParameters<TDbParameter> GetUpdateParameters(object entity);

        /// <summary>
        /// Create parameters from object properties
        /// </summary>
        /// <param name="parametersType"></param>
        /// <returns></returns>
        IDataParameter[] CreateSqlParametersFromProperties(ParametersType parametersType);

        /// <summary>
        /// 
        /// </summary>
        List<PropertyInfo> OutParameterPropertyInfoCollection { get; set; }

        string BuildWhereExpression(string propertyName, Type valueType, string @operator, object value);

        /// <summary>
        /// Clear all DbParameters of both insert and update EntityParameters
        /// </summary>
        void ClearDbParameters();

        /// <summary>
        /// Load all the properties from DbParameters which were marked as ParameterDirection.Out
        /// </summary>
        /// <param name="entityParameters">The EntityParameters object based on TDataParameters in ISimpleAccess</param>
        /// <param name="instance"> The instance of object </param>
        void LoadOutParametersProperties(EntityParameters<TDbParameter> entityParameters, object instance);

    }
}
