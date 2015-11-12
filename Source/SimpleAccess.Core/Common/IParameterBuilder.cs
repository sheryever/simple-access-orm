using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAccess.Core
{
    public interface IParameterBuilder
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


    }
}
