using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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

        string BuildValueOperand(Type valueType, object value);

        void ClearDbParameters();
        void LoadOutParametersProperties(object instance);
    }
}
