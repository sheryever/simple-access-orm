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
    /// The EntityInfo is create and cache the stored procedure name, quires and parameters
    /// </summary>
    public class EntityInfo<TISqlBuilder, TDbParameter>
        where TISqlBuilder : ISqlBuilder<TDbParameter>, new()
        where TDbParameter : IDataParameter
    {
        private List<PropertyInfo> OutParameterPropertyInfoCollection
        {
            get { return SqlBuilder.OutParameterPropertyInfoCollection; }
        }

        public readonly ISqlBuilder<TDbParameter> SqlBuilder;

        /// <summary>
        /// Get the Insert statement or StoredProcedure Parameters based on TDataParameters in ISimple
        /// </summary>
        public EntityParameters<TDbParameter> GetInsertParameters(object entity)
        {
            return SqlBuilder.GetInsertParameters(entity);
        }

        /// <summary>
        /// Get the Update statement or StoredProcedure Parameters based on TDataParameters in ISimple
        /// </summary>
        public EntityParameters<TDbParameter> GetUpdateParameters(object entity)
        {
            return SqlBuilder.GetUpdateParameters(entity);
        }

        /// <summary>
        /// Default select statement with all columns of the entity
        /// </summary>
        public string GetSelectAllStatement()
        {
            return SqlBuilder.GetSelectAllStatement();
        }

        /// <summary>
        /// Default insert statement with all columns and parameters of the entity
        /// </summary>
        public string GetInsertStatement()
        {
            return SqlBuilder.GetInsertStatement();


        }

        /// <summary>
        /// Default update statement with all columns and parameters of the entity
        /// </summary>
        public string GetUpdateSatetment()
        {
            return SqlBuilder.GetUpdateSatetment();
        }

        /// <summary>
        /// Default delete statement with id parameter of the entity
        /// </summary>
        public string GetDeleteStatment()
        {
            return SqlBuilder.GetDeleteStatment();
        }

        /// <summary>
        /// Initialize the new object
        /// </summary>
        /// <param name="type"> The Entity </param>
        public EntityInfo(Type type)
        {
            EntityType = type;
            LoadEntityInformation();
            SqlBuilder = new TISqlBuilder();
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
        /// Load entity name from <see cref="EntityAttribute"/> if entity is marked otherwise that the entity name
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
        /// Clear all DbParamters
        /// </summary>
        public void ClearDbParameters()
        {
            SqlBuilder.ClearDbParameters();
        }

        /// <summary>
        /// Load all the properties from DbParameters which were marked as ParameterDirection.Out
        /// </summary>
        /// <param name="instance"> The instance of object </param>
        public void LoadOutParametersProperties(object instance)
        {

            SqlBuilder.LoadOutParametersProperties(instance);
        }
    }

}
