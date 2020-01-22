using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;


namespace SimpleAccess.Core
{
    /// <summary>
    /// Represents the SimpleAccess 
    /// information.
    /// The EntityInfo is create and cache the stored procedure name, quires and parameters
    /// </summary>
    public class EntityParameters<TDbParameter>
        where TDbParameter : IDataParameter
    {
        public IDictionary<PropertyInfo, TDbParameter> OutParametersDictionary { get; set; }
        public IDictionary<PropertyInfo, TDbParameter> DataParametersDictionary { get; set; }


        public EntityParameters ()
        {
            OutParametersDictionary = new Dictionary<PropertyInfo, TDbParameter>();
            DataParametersDictionary = new Dictionary<PropertyInfo, TDbParameter>();
        }

        /// <summary>
        /// Load all the properties from DbParameters which were marked as ParameterDirection.Out
        /// </summary>
        /// <param name="instance"> The instance of object </param>
        public void LoadOutParametersProperties(object instance)
        {

            foreach (var propertyInfo in OutParametersDictionary)
            {
                try
                {
                    propertyInfo.Key.SetValue(instance, propertyInfo.Value.Value, new object[] { });
                }
                catch (Exception ex)
                {
                    throw new Exception(string.Format("Error in reading @{0} out parameter value", propertyInfo.Key.Name), ex);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="createAction"></param>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public static EntityParameters<TDbParameter> Create(
             Action<IDictionary<PropertyInfo, TDbParameter>, IDictionary<PropertyInfo, TDbParameter>, bool> createAction
            , bool checkForIdentity)
        {
            var entityParameters = new EntityParameters<TDbParameter>();
            createAction.Invoke(entityParameters.DataParametersDictionary, entityParameters.OutParametersDictionary, checkForIdentity);

            return entityParameters;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="createAction"></param>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public void FillParameters<TEntity>(TEntity entity,
            Action<TEntity, IDictionary<PropertyInfo, TDbParameter>> fillAction)
        {
            fillAction.Invoke(entity, DataParametersDictionary);
        }
    }
}
