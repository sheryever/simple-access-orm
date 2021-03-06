﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;


namespace SimpleAccess.Core.Entity
{

    public interface IEntityInfo
    {
        Type EntityType { get; }
        PropertyInfo[] GetPropertyInfos();
        string DbObjectName { get;  }
        string DbObjectViewName { get;  }
    }

    /// <summary>
    /// Represents the SimpleAccess Entity information.
    /// The EntityInfo is create and cache the stored procedure name, quires and parameters
    /// </summary>
    public class EntityInfo<TISqlBuilder, TDbParameter> : IEntityInfo
        where TISqlBuilder : ISqlBuilder<TDbParameter>, new()
        where TDbParameter : IDataParameter
    {
        //private List<PropertyInfo> OutParameterPropertyInfoCollection
        //{
        //    get { return SqlBuilder.OutParameterPropertyInfoCollection; }
        //}

        public readonly ISqlBuilder<TDbParameter> SqlBuilder;

        /// <summary>
        /// Get the Insert statement or StoredProcedure Parameters based on TDataParameters in ISimpleAccess
        /// </summary>
        public EntityParameters<TDbParameter> GetInsertParameters(object entity)
        {
            return SqlBuilder.GetInsertParameters(entity);
        }

        /// <summary>
        /// Get the Update statement or StoredProcedure Parameters based on TDataParameters in ISimpleAccess
        /// </summary>
        public EntityParameters<TDbParameter> GetUpdateParameters(object entity)
        {
            return SqlBuilder.GetUpdateParameters(entity);
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
            SqlBuilder.InitSqlBuilder(this);
        }

        /// <summary>
        /// Table/View Name of the Entity extracted from the <see cref="EntityAttribute"/> if the Entity is marked with it, otherwise the same name of Entity
        /// </summary>
        public string DbObjectName { get; private set; }

        public string DbObjectViewName { get; private set; }

        private PropertyInfo[] PropertyInfos { get; set; }

        public PropertyInfo[] GetPropertyInfos()
        {
            PropertyInfos = PropertyInfos ?? (PropertyInfos = EntityType.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Default));

            return PropertyInfos;
        }


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
                var entityAttr = ((EntityAttribute) entityAttribute);
                DbObjectName = entityAttr.EntityName;
                DbObjectViewName = entityAttr.DefaultView ?? entityAttr.EntityName;
            }
            else
            {
                DbObjectViewName = DbObjectName = EntityType.Name;
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
        /// Clear all DbParameters
        /// </summary>
        public void ClearDbParameters()
        {
            SqlBuilder.ClearDbParameters();
        }

        /// <summary>
        /// Load all the properties from DbParameters which were marked as ParameterDirection.Out
        /// </summary>
        /// <param name="entityParameters">The EntityParameters object based on TDataParameters in ISimpleAccess</param>
        /// <param name="instance"> The instance of object </param>
        public void LoadOutParametersProperties(EntityParameters<TDbParameter> entityParameters, object instance)
        {
            entityParameters.LoadOutParametersProperties(instance);
            //SqlBuilder.LoadOutParametersProperties(instance);
        }
    }

}
