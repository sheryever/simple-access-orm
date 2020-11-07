using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
#if !NETSTANDARD2_1
using System.Data.SqlClient;
#endif
#if NETSTANDARD2_1
using Microsoft.Data.SqlClient;
#endif
using System.Linq.Expressions;
using SimpleAccess.Core.Entity.RepoWrapper;
using System.Security.Cryptography;
using System.Runtime.InteropServices;
using SimpleAccess.Core.Entity;

namespace SimpleAccess.SqlServer
{

    public static partial class SqlRepositoryExtensions
    {

#if !NET40

#endif

        #region Aggregate functions

        //        public static dynamic GetAggregate<TEntity>(this ISqlRepository sqlRepository
        //            , Func<TEntity, object> countOf = null, Func<TEntity, object> sumOf = null
        //            , Func<TEntity, object> minOf = null, Func<TEntity, object> maxOf = null
        //            , Func<TEntity, object> avgOf = null
        //            , Expression<Func<TEntity, bool>> where = null
        //            , Action<HavingBuilder<TEntity>> having = null)
        //            where TEntity : class, new()
        //        {
        //            return GetAggregateWithGroupBy<TEntity>(sqlRepository, countOf, sumOf, minOf, maxOf, avgOf, where, null, having).FirstOrDefault();
        //        }

        //        public static IEnumerable<dynamic> GetAggregateWithGroupBy<TEntity>(this ISqlRepository sqlRepository
        //            , Func<TEntity, object> countOf = null, Func<TEntity, object> sumOf = null
        //            , Func<TEntity, object> minOf = null, Func<TEntity, object> maxOf = null
        //            , Func<TEntity, object> avgOf = null
        //            , Expression<Func<TEntity, bool>> where = null
        //            , Func<TEntity, object> groupBy = null
        //            , Action<HavingBuilder<TEntity>> having = null)
        //            where TEntity : class, new()
        //        {

        //            if (countOf == null && sumOf == null && minOf == null && maxOf == null && avgOf == null )
        //            {
        //                throw new NullReferenceException($"At least one must be provided {nameof(countOf)}, {nameof(sumOf)}, {nameof(minOf)},{nameof(maxOf)}, {nameof(avgOf)}");
        //            }

        //            string commandText = 
        //@"SELECT {groupByColumns}, {columns} 
        //    FROM {table} 
        //    {whereClause} 
        //    {groupByClause} 
        //    {havingClause}";

        //            var aggregateColums = new List<string>();

        //            if (groupBy != null)
        //            {
        //                var groupByProperties = LoadEntityProperties<TEntity>(GetSelectedProperties(groupBy));

        //                commandText = commandText.Replace("{groupByColumns}", string.Join(", ", groupByProperties.Select(g => g.Value.Name)));
        //                commandText = commandText.Replace("{groupByClause}", "GROUP BY " + string.Join(", ", groupByProperties.Select(g => g.Value.Name)));

        //            }
        //            else
        //            {
        //                commandText = commandText.Replace("{groupBy}", "");
        //            }

        //            if (countOf != null)
        //            {
        //                var returnedType = countOf.Invoke(new TEntity());

        //                if (returnedType is TEntity)
        //                {
        //                    aggregateColums.Add("COUNT(*) as CountOfAll");

        //                }
        //                else
        //                {
        //                    var groupByProperties = LoadEntityProperties<TEntity>(GetSelectedProperties(countOf));

        //                    aggregateColums.Add(string.Join(", ", groupByProperties.Select(c => $"COUNT({c.Value.Name}) as CountOf{c.Value.Name}")));

        //                }
        //            }

        //            if (sumOf != null)
        //            {
        //                var groupByProperties = LoadEntityProperties<TEntity>(GetSelectedProperties(sumOf));

        //                aggregateColums.Add(string.Join(", ", groupByProperties.Select(c => $"SUM({c.Value.Name}) as SumOf{c.Value.Name}")));
        //            }

        //            if (minOf != null)
        //            {
        //                var groupByProperties = LoadEntityProperties<TEntity>(GetSelectedProperties(minOf));

        //                aggregateColums.Add(string.Join(", ", groupByProperties.Select(c => $"MIN({c.Value.Name}) as MinOf{c.Value.Name}")));
        //            }

        //            if (maxOf != null)
        //            {
        //                var groupByProperties = LoadEntityProperties<TEntity>(GetSelectedProperties(maxOf));

        //                aggregateColums.Add(string.Join(", ", groupByProperties.Select(c => $"MAX({c.Value.Name}) as MaxOf{c.Value.Name}")));
        //            }

        //            if (avgOf != null)
        //            {
        //                var groupByProperties = LoadEntityProperties<TEntity>(GetSelectedProperties(avgOf));

        //                aggregateColums.Add(string.Join(", ", groupByProperties.Select(c => $"AVG({c.Value.Name}) as AvgOf{c.Value.Name}")));
        //            }

        //            commandText = commandText.Replace("{columns}", string.Join(", ", aggregateColums));

        //            string whereClause = "";
        //            if (sqlRepository is SqlSpRepository)
        //            {
        //                var entityInfo = SqlSpRepositorySetting.GetEntityInfo(typeof(TEntity));
        //                commandText = commandText.Replace("{table}", entityInfo.DbObjectViewName);
        //                whereClause = where == null ? "" : DynamicQuery.CreateDbParametersFormWhereExpression(where, entityInfo);
        //            }
        //            else
        //            {
        //                var entityInfo = SqlEntityRepositorySetting.GetEntityInfo(typeof(TEntity));
        //                commandText = commandText.Replace("{table}", entityInfo.DbObjectViewName);
        //                whereClause = where == null ? "" : DynamicQuery.CreateDbParametersFormWhereExpression(where, entityInfo);
        //            }
        //            commandText = commandText.Replace("{whereClause}", whereClause);

        //            if (having != null)
        //            {
        //                var havingBuilder = new HavingBuilder<TEntity>();
        //                having.Invoke(havingBuilder);
        //                commandText = commandText.Replace("{havingClause}", havingBuilder.GetHaving());
        //            }
        //            else
        //            {
        //                commandText = commandText.Replace("{havingClause}", "");
        //            }

        //            var result = sqlRepository.SimpleAccess.ExecuteDynamics(commandText, CommandType.Text);

        //            return result;

        //        }


        //        public static dynamic GetAggregate<TEntity>(this ISqlRepository sqlRepository
        //            , Func<TEntity, object> countOf = null, Func<TEntity, object> sumOf = null
        //            , Func<TEntity, object> minOf = null, Func<TEntity, object> maxOf = null
        //            , Func<TEntity, object> avgOf = null
        //            , Expression<Func<TEntity, bool>> where = null
        //            , Expression<Func<Aggregator<TEntity>, bool>> having = null)
        //            where TEntity : class, new()
        //        {
        //            return GetAggregateWithGroupBy<TEntity>(sqlRepository, countOf, sumOf, minOf, maxOf, avgOf, where, null, having).FirstOrDefault();
        //        }

        //        public static IEnumerable<dynamic> GetAggregateWithGroupBy<TEntity>(this ISqlRepository sqlRepository
        //            , Func<TEntity, object> countOf = null, Func<TEntity, object> sumOf = null
        //            , Func<TEntity, object> minOf = null, Func<TEntity, object> maxOf = null
        //            , Func<TEntity, object> avgOf = null
        //            , Expression<Func<TEntity, bool>> where = null
        //            , Func<TEntity, object> groupBy = null
        //            , Expression<Func<Aggregator<TEntity>, bool>> having = null)
        //            where TEntity : class, new()
        //        {

        //            if (countOf == null && sumOf == null && minOf == null && maxOf == null && avgOf == null)
        //            {
        //                throw new NullReferenceException($"At least one must be provided {nameof(countOf)}, {nameof(sumOf)}, {nameof(minOf)},{nameof(maxOf)}, {nameof(avgOf)}");
        //            }

        //            string commandText =
        //@"SELECT {groupByColumns}, {columns} 
        //    FROM {table} 
        //    {whereClause} 
        //    {groupByClause} 
        //    {havingClause}";

        //            var aggregateColums = new List<string>();

        //            if (groupBy != null)
        //            {
        //                var groupByProperties = LoadEntityProperties<TEntity>(GetSelectedProperties(groupBy));

        //                commandText = commandText.Replace("{groupByColumns}", string.Join(", ", groupByProperties.Select(g => g.Value.Name)));
        //                commandText = commandText.Replace("{groupByClause}", "GROUP BY " + string.Join(", ", groupByProperties.Select(g => g.Value.Name)));

        //            }
        //            else
        //            {
        //                commandText = commandText.Replace("{groupBy}", "");
        //            }

        //            if (countOf != null)
        //            {
        //                var returnedType = countOf.Invoke(new TEntity());

        //                if (returnedType is TEntity)
        //                {
        //                    aggregateColums.Add("COUNT(*) as CountOfAll");

        //                }
        //                else
        //                {
        //                    var groupByProperties = LoadEntityProperties<TEntity>(GetSelectedProperties(countOf));

        //                    aggregateColums.Add(string.Join(", ", groupByProperties.Select(c => $"COUNT({c.Value.Name}) as CountOf{c.Value.Name}")));

        //                }
        //            }

        //            if (sumOf != null)
        //            {
        //                var groupByProperties = LoadEntityProperties<TEntity>(GetSelectedProperties(sumOf));

        //                aggregateColums.Add(string.Join(", ", groupByProperties.Select(c => $"SUM({c.Value.Name}) as SumOf{c.Value.Name}")));
        //            }

        //            if (minOf != null)
        //            {
        //                var groupByProperties = LoadEntityProperties<TEntity>(GetSelectedProperties(minOf));

        //                aggregateColums.Add(string.Join(", ", groupByProperties.Select(c => $"MIN({c.Value.Name}) as MinOf{c.Value.Name}")));
        //            }

        //            if (maxOf != null)
        //            {
        //                var groupByProperties = LoadEntityProperties<TEntity>(GetSelectedProperties(maxOf));

        //                aggregateColums.Add(string.Join(", ", groupByProperties.Select(c => $"MAX({c.Value.Name}) as MaxOf{c.Value.Name}")));
        //            }

        //            if (avgOf != null)
        //            {
        //                var groupByProperties = LoadEntityProperties<TEntity>(GetSelectedProperties(avgOf));

        //                aggregateColums.Add(string.Join(", ", groupByProperties.Select(c => $"AVG({c.Value.Name}) as AvgOf{c.Value.Name}")));
        //            }

        //            commandText = commandText.Replace("{columns}", string.Join(", ", aggregateColums));

        //            string whereClause = "", havingClause ="";
        //            if (sqlRepository is SqlSpRepository)
        //            {
        //                var entityInfo = SqlSpRepositorySetting.GetEntityInfo(typeof(TEntity));
        //                commandText = commandText.Replace("{table}", entityInfo.DbObjectViewName);
        //                whereClause = where == null ? "" : DynamicQuery.CreateDbParametersFormWhereExpression(where, entityInfo);
        //                havingClause = having == null ? "" : DynamicQuery.CreateDbParametersFormHavingExpression(having, entityInfo);

        //                DynamicQuery.CreateDbParametersFormHavingExpression(having, entityInfo);
        //            }
        //            else
        //            {
        //                var entityInfo = SqlEntityRepositorySetting.GetEntityInfo(typeof(TEntity));
        //                commandText = commandText.Replace("{table}", entityInfo.DbObjectViewName);
        //                whereClause = where == null ? "" : DynamicQuery.CreateDbParametersFormWhereExpression(where, entityInfo);
        //                havingClause = having == null ? "" : DynamicQuery.CreateDbParametersFormHavingExpression(having, entityInfo);

        //            }
        //            commandText = commandText.Replace("{whereClause}", whereClause);

        //            commandText = commandText.Replace("{havingClause}", havingClause);


        //            var result = sqlRepository.SimpleAccess.ExecuteDynamics(commandText, CommandType.Text);

        //            return result;

        //        }
        //public static IEnumerable<dynamic> GetAggregate<TEntity>(this ISqlRepository sqlRepository
        //    , Func<Aggregator, TEntity, object> aggregator
        //    , Expression<Func<TEntity, bool>> where = null
        //    , Expression<Func<Aggregator, TEntity, bool>> having = null)
        //    where TEntity : class, new()
        //{
        //    return GetAggregateWithGroupBy<TEntity>(sqlRepository, aggregator, where, null, having).FirstOrDefault();
        //}


        //public static IEnumerable<dynamic> GetAggregateWithGroupBy<TEntity>(this ISqlRepository sqlRepository
        //    , Func<Aggregator, TEntity, object> aggregator
        //    , Expression<Func<TEntity, bool>> where = null
        //    , Func<TEntity, object> groupBy = null
        //    , Expression<Func<Aggregator, TEntity, bool>> having = null)
        //    where TEntity : class, new()
        //{

        //    if (aggregator == null)
        //    {
        //        throw new NullReferenceException($"{nameof(aggregator)} cannot be null");
        //    }

        //    string commandText = @"SELECT {groupByColumns}, {columns} 
        //                            FROM {table} 
        //                            {whereClause} 
        //                            {groupByClause} 
        //                            {havingClause}";

        //    var aggregateColums = new List<string>();

        //    if (groupBy != null)
        //    {
        //        var groupByProperties = LoadEntityProperties<TEntity>(GetSelectedProperties(groupBy));

        //        commandText = commandText.Replace("{groupByColumns}", string.Join(", ", groupByProperties.Select(g => g.Value.Name)));
        //        commandText = commandText.Replace("{groupByClause}", "GROUP BY " + string.Join(", ", groupByProperties.Select(g => g.Value.Name)));

        //    }
        //    else
        //    {
        //        commandText = commandText.Replace("{groupBy}", "");
        //    }



        //    string whereClause = "", havingClause = "";
        //    if (sqlRepository is SqlSpRepository)
        //    {
        //        var entityInfo = SqlSpRepositorySetting.GetEntityInfo(typeof(TEntity));
        //        commandText = commandText.Replace("{table}", entityInfo.DbObjectViewName);
        //        whereClause = where == null ? "" : DynamicQuery.CreateDbParametersFormWhereExpression(where, entityInfo);
        //        havingClause = having == null ? "" : DynamicQuery.CreateDbParametersFormHavingExpression(having, entityInfo);

        //        DynamicQuery.CreateDbParametersFormHavingExpression(having, entityInfo);
        //    }
        //    else
        //    {
        //        var entityInfo = SqlEntityRepositorySetting.GetEntityInfo(typeof(TEntity));


        //        if (aggregator != null)
        //        {
        //            var returnedType = aggregator.Invoke(new Aggregator(), new TEntity());

        //            if (returnedType is TEntity)
        //            {
        //                aggregateColums.Add("COUNT(*) AS CountOfAll");
        //            }
        //            else
        //            {
        //                var groupByProperties = DynamicQuery.CreateAggregateColumnsFormAggregateExpression(aggregator, entityInfo);

        //                aggregateColums.Add(string.Join(", ", groupByProperties.Select(c => $"COUNT({c.Value.Name}) as CountOf{c.Value.Name}")));
        //            }
        //        }

        //        commandText = commandText.Replace("{columns}", string.Join(", ", aggregateColums));

        //        commandText = commandText.Replace("{table}", entityInfo.DbObjectViewName);
        //        whereClause = where == null ? "" : DynamicQuery.CreateDbParametersFormWhereExpression(where, entityInfo);
        //        havingClause = having == null ? "" : DynamicQuery.CreateDbParametersFormHavingExpression(having, entityInfo);

        //    }


        //    commandText = commandText.Replace("{whereClause}", whereClause);

        //    commandText = commandText.Replace("{havingClause}", havingClause);


        //    var result = sqlRepository.SimpleAccess.ExecuteDynamics(commandText, CommandType.Text);

        //    return result;

        //}

        //public static dynamic GetAggregate2<TEntity>(this ISqlRepository sqlRepository
        //    , Func<TEntity, object> countOf = null, Func<TEntity, object> sumOf = null
        //    , Func<TEntity, object> minOf = null, Func<TEntity, object> maxOf = null
        //    , Func<TEntity, object> avgOf = null
        //    , Expression<Func<TEntity, bool>> where = null
        //    , Expression<Func<Aggregator<TEntity>, bool>> having = null)
        //    where TEntity : class, new()
        //{
        //    var entityInfo = SqlEntityRepositorySetting.GetEntityInfo(typeof(TEntity));

        //    var havingClause = DynamicQuery.CreateDbParametersFormHavingExpression(having, entityInfo);

        //    throw new NotImplementedException();
        //    //GetAggregateWithGroupBy<TEntity>(sqlRepository, countOf, sumOf, minOf, maxOf, avgOf, where, null, having).FirstOrDefault();
        //}



        #endregion
        //public class HavingBuilder<TEntity>
        //    where TEntity : class, new()
        //{
        //    private string _having = "";
        //    public HavingBuilder<TEntity> Sum<TKey>(Expression<Func<TEntity, TKey>> selector)
        //    {
        //        var column = GetSingleSelectedProperty(selector);

        //        _having += $" SUM({column}) ";

        //        return this;
        //    }
        //    public HavingBuilder<TEntity> Count<TKey>(Expression<Func<TEntity, TKey>> selector)
        //    {
        //        var column = GetSingleSelectedProperty(selector);

        //        _having += $" COUNT({column}) ";

        //        return this;
        //    }
        //    public HavingBuilder<TEntity> Min<TKey>(Expression<Func<TEntity, TKey>> selector)
        //    {
        //        var column = GetSingleSelectedProperty(selector);

        //        _having += $" MIN({column}) ";

        //        return this;
        //    }
        //    public HavingBuilder<TEntity> Max<TKey>(Expression<Func<TEntity, TKey>> selector)
        //    {
        //        var column = GetSingleSelectedProperty(selector);

        //        _having += $" MAX({column}) ";

        //        return this;
        //    }
        //    public HavingBuilder<TEntity> Average<TKey>(Expression<Func<TEntity, TKey>> selector)
        //    {
        //        var column = GetSingleSelectedProperty(selector);

        //        _having += $" AVG({column}) ";

        //        return this;
        //    }
        //    public HavingBuilder<TEntity> GreaterThan()
        //    {
        //        _having += " > ";

        //        return this;
        //    }
        //    public HavingBuilder<TEntity> GreaterThan<T>(T value)
        //    {
        //        _having += $" > {value.ToString().Replace("'", "''")}";

        //        return this;
        //    }

        //    public HavingBuilder<TEntity> GreaterThanEqualTo()
        //    {
        //        _having += " >= ";

        //        return this;
        //    }
        //    public HavingBuilder<TEntity> GreaterThanEqualTo<T>(T value)
        //    {
        //        _having += $" >= {value.ToString().Replace("'", "''")}";

        //        return this;
        //    }

        //    public HavingBuilder<TEntity> LessThan()
        //    {

        //        _having += $" < ";

        //        return this;
        //    }
        //    public HavingBuilder<TEntity> LessThan<T>(T value)
        //    {
        //        _having += $" < {value.ToString().Replace("'", "''")}";

        //        return this;
        //    }

        //    public HavingBuilder<TEntity> LessThanEqualTo()
        //    {

        //        _having += $" <= ";

        //        return this;
        //    }
        //    public HavingBuilder<TEntity> LessThanEqualTo<T>(T value)
        //    {
        //        _having += $" <= {value.ToString().Replace("'", "''")}";

        //        return this;
        //    }

        //    public HavingBuilder<TEntity> EqualTo()
        //    {
        //        _having += " = ";

        //        return this;
        //    }
        //    public HavingBuilder<TEntity> EqualTo<T>(T value)
        //    {
        //        _having += $" = {value.ToString().Replace("'", "''")}";

        //        return this;
        //    }
        //    public HavingBuilder<TEntity> NotEqualTo()
        //    {
        //        _having += $" <> ";

        //        return this;
        //    }
        //    public HavingBuilder<TEntity> NotEqualTo<T>(T value)
        //    {
        //        _having += $" <> {value.ToString().Replace("'", "''")}";

        //        return this;
        //    }
        //    public HavingBuilder<TEntity> And()
        //    {
        //        _having += $" AND ";

        //        return this;
        //    }
        //    public HavingBuilder<TEntity> Or()
        //    {
        //        _having += $" OR ";

        //        return this;
        //    }

        //    public string GetHaving()
        //    {
        //        return " HAVING " + _having ;
        //    }
        //}

        //public class Aggregator<TEntity>
        //    where TEntity : class, new()
        //{
        //    private string _having = "";
        //    public TKey Sum<TKey>(Expression<Func<TEntity, TKey>> selector)
        //    {
        //        return default(TKey);
        //    }
        //    public TKey Count<TKey>(Expression<Func<TEntity, TKey>> selector)
        //    {
        //        return default(TKey);
        //    }
        //    public TKey Min<TKey>(Expression<Func<TEntity, TKey>> selector)
        //    {
        //        return default(TKey);
        //    }
        //    public TKey Max<TKey>(Expression<Func<TEntity, TKey>> selector)
        //    {
        //        return default(TKey);
        //    }
        //    public TKey Average<TKey>(Expression<Func<TEntity, TKey>> selector)
        //    {
        //        return default(TKey);
        //    }

        //    public string GetHaving()
        //    {
        //        return " HAVING " + _having;
        //    }
        //}




    }
}
