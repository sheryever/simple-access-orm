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
using System.Linq;
#pragma warning restore CS0246 // The type or namespace name 'System' could not be found (are you missing a using directive or an assembly reference?)
#pragma warning disable CS0246 // The type or namespace name 'System' could not be found (are you missing a using directive or an assembly reference?)
using System.Reflection;
#pragma warning restore CS0246 // The type or namespace name 'System' could not be found (are you missing a using directive or an assembly reference?)
#pragma warning disable CS0246 // The type or namespace name 'System' could not be found (are you missing a using directive or an assembly reference?)
using System.Text;
#pragma warning restore CS0246 // The type or namespace name 'System' could not be found (are you missing a using directive or an assembly reference?)
#pragma warning disable CS0246 // The type or namespace name 'System' could not be found (are you missing a using directive or an assembly reference?)
using System.Threading.Tasks;
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
        /// <param name="parametesType"></param>
        /// <param name="propertyInfos"></param>
        /// <returns></returns>
        IDataParameter CreateDataParameter(PropertyInfo propertyInfo, ParametersType parametesType,
            IEnumerable<PropertyInfo> propertyInfos, IList<PropertyInfo> outParameterPropertyInfoCollection , List<IDataParameter> outDataParameters );

        EntityParameters<TDbParameter> CreateEntityParameters(object entity, bool checkForIdentityColumn);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string GetSelectAllStatement();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string GetInsertStatement();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string GetUpdateSatetment();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string GetDeleteStatment();

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
        /// Clear all DbParamters of both insert and update EntityParameters
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
