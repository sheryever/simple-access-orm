using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using SimpleAccess.Core;


namespace SimpleAccess.Core.Entity
{
    /// <summary>
    /// Represents the SimpleAccess Entity information.
    /// The EntityInfo is create and cache the stored procedure name, queris and parameters
    /// </summary>
    public class EntityInfo<TParameterBuilder>
        where TParameterBuilder : IParameterBuilder, new()
    {
        //Muhammad Ahsan
        private List<PropertyInfo> _outParameterPropertyInfoCollection;
        private List<IDataParameter> _outParameters;
        private List<IDataParameter> _dataParameters;
        private readonly IParameterBuilder _parameterBuilder;

        /// <summary>
        /// Get the Insert statement or StoredProcedure Paramenters based on TDataParameters in ISimple
        /// </summary>
        public IDataParameter[] InsertParameters { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public IDataParameter[] UpdateParameters { get; private set; }

        /// <summary>
        /// Default select statement with all coulmns of the entity
        /// </summary>
        public string SelectAllStatement { get; private set; }

        /// <summary>
        /// Default insert statement with all coulmns and parameters of the entity
        /// </summary>
        public string InsertStatement { get; private set; }

        /// <summary>
        /// Default update statement with all coulmns and parameters of the entity
        /// </summary>
        public string UpdateSatetment { get; private set; }

        /// <summary>
        /// Default delete statement with id parameter of the entity
        /// </summary>
        public string DeleteStatment { get; private set; }

        /// <summary>
        /// Initialize the new object
        /// </summary>
        /// <param name="type"> The Entity </param>
        public EntityInfo(Type type)
        {
            EntityType = type;
            LoadEntityInformation();
            _parameterBuilder = new TParameterBuilder();
        }

        /// <summary>
        /// Table/View Name of the Entity extracted from the <see cref="EntityAttribute"/> if the Entity is marked with it, otherwise the same name of Entity
        /// </summary>
        public string DbObjectName { get; private set; }

        /// <summary>
        /// Stored procedure prefix of the Entity extracted from the <see cref="StoredProcedureNameKeyWordAttribute"/> 
        /// if the Entity is marked with it, otherwise the same name of Entity
        /// </summary>
        public string StoredProcedureNameKeyWord { get; private set; }

        /// <summary>
        /// The Type of the Entity.
        /// </summary>
        public Type EntityType { get; set; }

        /// <summary>
        /// Load entity name from <see cref="EntityAttribute"/> if entity is marked ortherwise that the entity name
        /// </summary>
        private void LoadEntityInformation()
        {
            var entityAttribute = EntityType.GetCustomAttributes(true).FirstOrDefault(c => c is EntityAttribute);
            if (entityAttribute != null)
            {
                DbObjectName = ((EntityAttribute)entityAttribute).EntityName;
            }
            else
            {
                DbObjectName = EntityType.Name;
            }

            var storedProcedureNameKeywordAttribute = EntityType.GetCustomAttributes(true).FirstOrDefault(c => c is StoredProcedureNameKeyWordAttribute);
            if (storedProcedureNameKeywordAttribute != null)
            {
                StoredProcedureNameKeyWord = ((StoredProcedureNameKeyWordAttribute)storedProcedureNameKeywordAttribute).NameKeyWord;
            }
            else
            {
                StoredProcedureNameKeyWord = EntityType.Name;
            }

        }

        /// <summary>
        /// Create paramters from object properties
        /// </summary>
        /// <param name="parametersType"></param>
        /// <returns></returns>
        public IDataParameter[] CreateSqlParametersFromProperties(ParametersType parametersType)
        {
            
            _outParameterPropertyInfoCollection = new List<PropertyInfo>();
            _outParameters = new List<IDataParameter>();

            var procedureType = this.GetType();
            var propertiesForDataParams = procedureType.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Default);

            _dataParameters =
                propertiesForDataParams.Select(propertyInfo => _parameterBuilder.CreateDataParameter(propertyInfo, parametersType, propertiesForDataParams, _outParameterPropertyInfoCollection, _outParameters))
                    .Where(p => p != null).ToList();

            return _dataParameters.ToArray();
        }

        /// <summary>
        /// Clear all DbParamters
        /// </summary>
        public void ClearSpParameters()
        {
            _outParameters.Clear();
            _dataParameters.Clear();
            _outParameterPropertyInfoCollection.Clear();
        }

        /// <summary>
        /// Load all the properties from DbParameters which were marked as ParameterDirection.Out
        /// </summary>
        /// <param name="instance"> The instance of object </param>
        public void LoadOutParametersProperties(object instance)
        {
            _outParameterPropertyInfoCollection.ForEach(p => {
                var propertyName = p.Name;
                try
                {
                    p.SetValue(instance, _outParameters.Single(
                        sp => sp.ParameterName == string.Format("@{0}", propertyName)).Value, new object[] { });
                }
                catch (Exception ex)
                {
                    throw new Exception(string.Format("Error in reading @{0} out parameter value", propertyName), ex);
                }
            });
            // ClearSpParameters();
        }
    }

}
