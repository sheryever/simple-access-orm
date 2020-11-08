using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

// #if NET40
namespace SimpleAccess.Core.Entity
{


    namespace RepoWrapper
    {
        /// <summary>
        /// Dynamic query class.
        /// </summary>
        public sealed class DynamicQuery
        {
            /// <summary>
            /// Gets the insert query.
            /// </summary>
            /// <param name="tableName">Name of the table.</param>
            /// <param name="item">The item.</param>
            /// <returns>
            /// The Sql query based on the item properties.
            /// </returns>
            public static string GetInsertQuery(string tableName, dynamic item)
            {
                PropertyInfo[] props = item.GetType().GetProperties();
                string[] columns = props.Select(p => p.Name).Where(s => s != "ID").ToArray();

                return string.Format("INSERT INTO {0} ({1}) OUTPUT inserted.ID VALUES (@{2})",
                                     tableName,
                                     string.Join(",", columns),
                                     string.Join(",@", columns));
            }

            /// <summary>
            /// Gets the update query.
            /// </summary>
            /// <param name="tableName">Name of the table.</param>
            /// <param name="item">The item.</param>
            /// <returns>
            /// The Sql query based on the item properties.
            /// </returns>
            public static string GetUpdateQuery(string tableName, dynamic item)
            {
                PropertyInfo[] props = item.GetType().GetProperties();
                string[] columns = props.Select(p => p.Name).ToArray();

                var parameters = columns.Select(name => name + "=@" + name).ToList();

                return string.Format("UPDATE {0} SET {1} WHERE ID=@ID", tableName, string.Join(",", parameters));
            }

            /// <summary>
            /// Gets the dynamic query.
            /// </summary>
            /// <param name="tableName">Name of the table.</param>
            /// <param name="expression">The expression.</param>
            /// <returns>A result object with the generated sql and dynamic params.</returns>
            public static QueryResult GetDynamicQuery<T>(string tableName, Expression<Func<T, bool>> expression)
            {
                var queryProperties = new List<QueryParameter>();
                var body = (BinaryExpression)expression.Body;
                IDictionary<string, Object> expando = new ExpandoObject();
                var builder = new StringBuilder();

                // walk the tree and build up a list of query parameter objects
                // from the left and right branches of the expression tree
                WalkTree(body, ExpressionType.Default, ref queryProperties);

                // convert the query parms into a SQL string and dynamic property object
                builder.Append("SELECT * FROM ");
                builder.Append(tableName);
                builder.Append(" WHERE ");

                for (int i = 0; i < queryProperties.Count(); i++)
                {
                    QueryParameter item = queryProperties[i];

                    if (!string.IsNullOrEmpty(item.LinkingOperator) && i > 0)
                    {
                        builder.Append(string.Format("{0} {1} {2} @{1} ", item.LinkingOperator, item.PropertyName,
                                                     item.QueryOperator));
                    }
                    else
                    {
                        builder.Append(string.Format("{0} {1} @{0} ", item.PropertyName, item.QueryOperator));
                    }

                    expando[item.PropertyName] = item.PropertyValue;
                }

                return new QueryResult(builder.ToString().TrimEnd(), expando);
            }

            /// <summary>
            /// Gets the dynamic query.
            /// </summary>
            /// <param name="tableName">Name of the table.</param>
            /// <param name="expression">The expression.</param>
            /// <returns>A result object with the generated sql and dynamic params.</returns>
            public static string CreateDbParametersFormWhereExpression<TISqlBuilder, TDbParameter, TEntitiy>(Expression<Func<TEntitiy, bool>> expression, EntityInfo<TISqlBuilder, TDbParameter> entityInfo)
                where TISqlBuilder : ISqlBuilder<TDbParameter>, new()
                where TDbParameter : IDataParameter
            {
                BinaryExpression binaryExpressionBody = null;
                MethodCallExpression methodCallExpressionBody = null;
                var queryProperties = new List<QueryParameter>();
                if (expression.Body is BinaryExpression)
                {
                    binaryExpressionBody = (BinaryExpression)expression.Body;
                    // walk the tree and build up a list of query parameter objects
                    // from the left and right branches of the expression tree
                    WalkTree(binaryExpressionBody, ExpressionType.Default, ref queryProperties);


                }
                else if (expression.Body is MethodCallExpression)
                {
                    methodCallExpressionBody = (MethodCallExpression) expression.Body;
                    // walk the tree and build up a list of query parameter objects
                    // from the left and right branches of the expression tree
                    WalkTree(methodCallExpressionBody, ExpressionType.Default, ref queryProperties);

                }

                IDictionary<string, Object> expando = new ExpandoObject();
                var builder = new StringBuilder();

                // convert the query parms into a SQL string and dynamic property object

                builder.Append(" WHERE ");

                var entityType = typeof(TEntitiy);
                
                for (int i = 0; i < queryProperties.Count(); i++)
                {
                    QueryParameter item = queryProperties[i];

                    var propertyInfo = entityType.GetProperty(item.PropertyName);
                    if (!string.IsNullOrEmpty(item.LinkingOperator) && i > 0)
                    {
                        //builder.Append(string.Format("{0} {1} {2} @{1} ", item.LinkingOperator, item.PropertyName,
                        builder.Append(string.Format("{0}  {1} ", item.LinkingOperator,
                            entityInfo.SqlBuilder.BuildWhereExpression(item.PropertyName, propertyInfo.PropertyType,
                                item.QueryOperator, item.PropertyValue)));

                    }
                    else
                    {
                        builder.Append(entityInfo.SqlBuilder.BuildWhereExpression(item.PropertyName, propertyInfo.PropertyType,
                                    item.QueryOperator, item.PropertyValue));
//                                entityInfo.SqlBuilder.BuildValueOperand(propertyInfo.PropertyType, item.PropertyValue)));
                        
                    }
                }

                return builder.ToString();
            }


            /// <summary>
            /// Gets the dynamic query.
            /// </summary>
            /// <param name="tableName">Name of the table.</param>
            /// <param name="expression">The expression.</param>
            /// <returns>A result object with the generated sql and dynamic params.</returns>
            public static string CreateDbParametersFormHavingExpression<TISqlBuilder, TDbParameter, TEntitiy>(Expression<Func<TEntitiy, bool>> expression, EntityInfo<TISqlBuilder, TDbParameter> entityInfo)
                where TISqlBuilder : ISqlBuilder<TDbParameter>, new()
                where TDbParameter : IDataParameter
            {
                BinaryExpression binaryExpressionBody = null;
                MethodCallExpression methodCallExpressionBody = null;
                var queryProperties = new List<QueryHavingParameter>();
                if (expression.Body is BinaryExpression)
                {
                    binaryExpressionBody = (BinaryExpression)expression.Body;
                    // walk the tree and build up a list of query parameter objects
                    // from the left and right branches of the expression tree
                    WalkTreeHavingExpressoin(binaryExpressionBody, ExpressionType.Default, ref queryProperties);
                }
                else if (expression.Body is MethodCallExpression)
                {
                    methodCallExpressionBody = (MethodCallExpression)expression.Body;
                    // walk the tree and build up a list of query parameter objects
                    // from the left and right branches of the expression tree
                    WalkTreeHavingExpressoin(methodCallExpressionBody, ExpressionType.Default, ref queryProperties);

                }

                IDictionary<string, Object> expando = new ExpandoObject();
                var builder = new StringBuilder();

                // convert the query parms into a SQL string and dynamic property object

                builder.Append(" HAVING ");

                var entityType = typeof(TEntitiy).GetGenericArguments()[0];

                for (int i = 0; i < queryProperties.Count(); i++)
                {
                    QueryHavingParameter item = queryProperties[i];

                    var propertyInfo = entityType.GetProperty(item.PropertyName);
                    if (!string.IsNullOrEmpty(item.LinkingOperator) && i > 0)
                    {
                        //builder.Append(string.Format("{0} {1} {2} @{1} ", item.LinkingOperator, item.PropertyName,
                        builder.Append(string.Format("{0}  {1} ", item.LinkingOperator,
                            entityInfo.SqlBuilder.BuildWhereExpression($"{item.FunctionName}({item.PropertyName})", propertyInfo.PropertyType,
                                item.QueryOperator, item.PropertyValue)));

                    }
                    else
                    {
                        builder.Append(entityInfo.SqlBuilder.BuildWhereExpression($"{item.FunctionName}({item.PropertyName})", propertyInfo.PropertyType,
                                    item.QueryOperator, item.PropertyValue));
                        //                                entityInfo.SqlBuilder.BuildValueOperand(propertyInfo.PropertyType, item.PropertyValue)));

                    }
                }

                return builder.ToString();
            }


            /// <summary>
            /// Gets the dynamic query.
            /// </summary>
            /// <param name="tableName">Name of the table.</param>
            /// <param name="expression">The expression.</param>
            /// <returns>A result object with the generated sql and dynamic params.</returns>
            public static string CreateDbParametersFormHavingExpression<TISqlBuilder, TDbParameter, TEntitiy>(Expression<Func<Aggregator, TEntitiy, bool>> expression, EntityInfo<TISqlBuilder, TDbParameter> entityInfo)
                where TISqlBuilder : ISqlBuilder<TDbParameter>, new()
                where TDbParameter : IDataParameter
            {
                BinaryExpression binaryExpressionBody = null;
                MethodCallExpression methodCallExpressionBody = null;
                var queryProperties = new List<QueryHavingParameter>();
                if (expression.Body is BinaryExpression)
                {
                    binaryExpressionBody = (BinaryExpression)expression.Body;
                    // walk the tree and build up a list of query parameter objects
                    // from the left and right branches of the expression tree
                    WalkTreeHavingExpressoin(binaryExpressionBody, ExpressionType.Default, ref queryProperties);
                }
                else if (expression.Body is MethodCallExpression)
                {
                    methodCallExpressionBody = (MethodCallExpression)expression.Body;
                    // walk the tree and build up a list of query parameter objects
                    // from the left and right branches of the expression tree
                    WalkTreeHavingExpressoin(methodCallExpressionBody, ExpressionType.Default, ref queryProperties);

                }

                IDictionary<string, Object> expando = new ExpandoObject();
                var builder = new StringBuilder();

                // convert the query parms into a SQL string and dynamic property object

                builder.Append(" HAVING ");

                var entityType = typeof(TEntitiy).GetGenericArguments()[0];

                for (int i = 0; i < queryProperties.Count(); i++)
                {
                    QueryHavingParameter item = queryProperties[i];

                    var propertyInfo = entityType.GetProperty(item.PropertyName);
                    if (!string.IsNullOrEmpty(item.LinkingOperator) && i > 0)
                    {
                        //builder.Append(string.Format("{0} {1} {2} @{1} ", item.LinkingOperator, item.PropertyName,
                        builder.Append(string.Format("{0}  {1} ", item.LinkingOperator,
                            entityInfo.SqlBuilder.BuildWhereExpression($"{item.FunctionName}({item.PropertyName})", propertyInfo.PropertyType,
                                item.QueryOperator, item.PropertyValue)));

                    }
                    else
                    {
                        builder.Append(entityInfo.SqlBuilder.BuildWhereExpression($"{item.FunctionName}({item.PropertyName})", propertyInfo.PropertyType,
                                    item.QueryOperator, item.PropertyValue));
                        //                                entityInfo.SqlBuilder.BuildValueOperand(propertyInfo.PropertyType, item.PropertyValue)));

                    }
                }

                return builder.ToString();
            }

            /// <summary>
            /// Gets the dynamic query.
            /// </summary>
            /// <param name="tableName">Name of the table.</param>
            /// <param name="expression">The expression.</param>
            /// <returns>A result object with the generated sql and dynamic params.</returns>
            public static string CreateAggregateColumnsFormAggregateExpression<TISqlBuilder, TDbParameter, TEntitiy>(Expression<Func<Aggregator, TEntitiy, object>> expression, EntityInfo<TISqlBuilder, TDbParameter> entityInfo)
                where TISqlBuilder : ISqlBuilder<TDbParameter>, new()
                where TDbParameter : IDataParameter
            {
                BinaryExpression binaryExpressionBody = null;
                MethodCallExpression methodCallExpressionBody = null;
                var queryProperties = new List<QueryHavingParameter>();
                if (expression.Body is BinaryExpression)
                {
                    binaryExpressionBody = (BinaryExpression)expression.Body;
                    // walk the tree and build up a list of query parameter objects
                    // from the left and right branches of the expression tree
                    WalkTreeHavingExpressoin(binaryExpressionBody, ExpressionType.Default, ref queryProperties);
                }
                else if (expression.Body is MethodCallExpression)
                {
                    methodCallExpressionBody = (MethodCallExpression)expression.Body;
                    // walk the tree and build up a list of query parameter objects
                    // from the left and right branches of the expression tree
                    WalkTreeHavingExpressoin(methodCallExpressionBody, ExpressionType.Default, ref queryProperties);

                }

                IDictionary<string, Object> expando = new ExpandoObject();
                var builder = new StringBuilder();

                // convert the query parms into a SQL string and dynamic property object

                builder.Append(" HAVING ");

                var entityType = typeof(TEntitiy).GetGenericArguments()[0];

                for (int i = 0; i < queryProperties.Count(); i++)
                {
                    QueryHavingParameter item = queryProperties[i];

                    var propertyInfo = entityType.GetProperty(item.PropertyName);
                    if (!string.IsNullOrEmpty(item.LinkingOperator) && i > 0)
                    {
                        //builder.Append(string.Format("{0} {1} {2} @{1} ", item.LinkingOperator, item.PropertyName,
                        builder.Append(string.Format("{0}  {1} ", item.LinkingOperator,
                            entityInfo.SqlBuilder.BuildWhereExpression($"{item.FunctionName}({item.PropertyName})", propertyInfo.PropertyType,
                                item.QueryOperator, item.PropertyValue)));

                    }
                    else
                    {
                        builder.Append(entityInfo.SqlBuilder.BuildWhereExpression($"{item.FunctionName}({item.PropertyName})", propertyInfo.PropertyType,
                                    item.QueryOperator, item.PropertyValue));
                        //                                entityInfo.SqlBuilder.BuildValueOperand(propertyInfo.PropertyType, item.PropertyValue)));

                    }
                }

                return builder.ToString();
            }


            /// <summary>
            /// Walks the tree.
            /// </summary>
            /// <param name="body">The body.</param>
            /// <param name="linkingType">Type of the linking.</param>
            /// <param name="queryProperties">The query properties.</param>
            private static void WalkTree(BinaryExpression body, ExpressionType linkingType,
                                         ref List<QueryParameter> queryProperties)
            {
                if (body.NodeType != ExpressionType.AndAlso && body.NodeType != ExpressionType.OrElse)
                {
                    string propertyName = GetPropertyName(body);
                    object propertyValue = GetExpressionValue(body);
                    //dynamic propertyValue = GetExpressionValue(body);
                    string opr = GetOperator(body.NodeType);
                    string link = GetOperator(linkingType);

                    queryProperties.Add(new QueryParameter(link, propertyName, propertyValue, opr));
                }
                else
                {
                    if (body.Left is BinaryExpression)
                    {
                        WalkTree((BinaryExpression)body.Left, body.NodeType, ref queryProperties);
                    }
                    else if(body.Left is MethodCallExpression)
                    {
                        WalkTree((MethodCallExpression)body.Left, body.NodeType, ref queryProperties);
                    }
                    if (body.Right is BinaryExpression)
                    {
                        WalkTree((BinaryExpression)body.Right, body.NodeType, ref queryProperties);
                    }
                    else if (body.Right is MethodCallExpression)
                    {
                        WalkTree((MethodCallExpression)body.Right, body.NodeType, ref queryProperties);
                    }
                }
            }

            /// <summary>
            /// Walks the tree.
            /// </summary>
            /// <param name="body">The body.</param>
            /// <param name="linkingType">Type of the linking.</param>
            /// <param name="queryProperties">The query properties.</param>
            private static void WalkTreeHavingExpressoin(BinaryExpression body, ExpressionType linkingType,
                                         ref List<QueryHavingParameter> queryProperties)
            {
                if (body.NodeType != ExpressionType.AndAlso && body.NodeType != ExpressionType.OrElse)
                {
                    dynamic aggregateFuncAndColumn = GetHavingFunctionAndColumn(body);
                    dynamic propertyValue = GetExpressionHavingValue(body);
                    if (propertyValue.GetType().IsGenericType)
                    {
                        propertyValue = $"{propertyValue.FunctionName}({propertyValue.ColumnName})";
                    }
                    //dynamic propertyValue = GetExpressionValue(body);
                    string opr = GetOperator(body.NodeType);
                    string link = GetOperator(linkingType);

                    queryProperties.Add(new QueryHavingParameter(link, aggregateFuncAndColumn.FunctionName, aggregateFuncAndColumn.ColumnName, propertyValue, opr));
                }
                else
                {
                    if (body.Left is BinaryExpression)
                    {
                        WalkTreeHavingExpressoin((BinaryExpression)body.Left, body.NodeType, ref queryProperties);
                    }
                    else if (body.Left is MethodCallExpression)
                    {
                        WalkTreeHavingExpressoin((MethodCallExpression)body.Left, body.NodeType, ref queryProperties);
                    }
                    if (body.Right is BinaryExpression)
                    {
                        WalkTreeHavingExpressoin((BinaryExpression)body.Right, body.NodeType, ref queryProperties);
                    }
                    else if (body.Right is MethodCallExpression)
                    {
                        WalkTreeHavingExpressoin((MethodCallExpression)body.Right, body.NodeType, ref queryProperties);
                    }
                }
            }


            /// <summary>
            /// Walks the tree.
            /// </summary>
            /// <param name="body">The body.</param>
            /// <param name="linkingType">Type of the linking.</param>
            /// <param name="queryProperties">The query properties.</param>
            private static void WalkTree(MethodCallExpression body, ExpressionType linkingType,
                                         ref List<QueryParameter> queryProperties)
            {
                string propertyName = GetPropertyName(body);
                dynamic propertyValue = GetExpressionValue(body);
                string opr = body.Method.Name;
                string link = GetOperator(linkingType);

                queryProperties.Add(new QueryParameter(link, propertyName, propertyValue, opr));
            }

            /// <summary>
            /// Walks the tree.
            /// </summary>
            /// <param name="body">The body.</param>
            /// <param name="linkingType">Type of the linking.</param>
            /// <param name="queryProperties">The query properties.</param>
            private static void WalkTreeHavingExpressoin(MethodCallExpression body, ExpressionType linkingType,
                                         ref List<QueryHavingParameter> queryProperties)
            {
                dynamic aggregateFuncAndColumn = GetHavingFunctionAndColumn(body);
                dynamic propertyValue = GetExpressionHavingValue(body);
                string opr = body.Method.Name;
                string link = GetOperator(linkingType);

                queryProperties.Add(new QueryHavingParameter(link, aggregateFuncAndColumn.FunctionName, aggregateFuncAndColumn.ColumnName, propertyValue, opr));
            }


            /// <summary>
            /// Gets the name of the property.
            /// </summary>
            /// <param name="body">The body.</param>
            /// <returns>The property name for the property expression.</returns>
            private static string GetPropertyName(BinaryExpression body)
            {
                string propertyName = body.Left.ToString().Split(new char[] { '.' })[1];

                if (body.Left.NodeType == ExpressionType.Convert)
                {
                    // hack to remove the trailing ) when convering. Need proper handling
                    propertyName = propertyName.Replace(")", string.Empty);
                    if (propertyName.IndexOf(",") > 0)
                    {
                        propertyName =  propertyName.Substring(0, propertyName.IndexOf(","));
                    }
                }

                return propertyName;
            }

            /// <summary>
            /// Gets the name of the property.
            /// </summary>
            /// <param name="body">The body.</param>
            /// <returns>The property name for the property expression.</returns>
            private static dynamic GetHavingFunctionAndColumn(BinaryExpression body)
            {
                // cut the bullshit
                var values = body.Left.ToString().Split(new char[] { '.' });

                var function = values[1].Substring(0, values[1].IndexOf("("));
                var column = values[2].Substring(0, values[2].IndexOf(")"));

                //if (body.Left.NodeType == ExpressionType.Convert)
                //{
                //    // hack to remove the trailing ) when convering.
                //    propertyName = propertyName.Replace(")", string.Empty);
                //}

                return new { FunctionName = function, ColumnName = column };
            }

            private static object GetExpressionValue(MethodCallExpression body)
            {
                if (body.Arguments[0] is ConstantExpression)
                {
                    return ((dynamic) body.Arguments[0]).Value;
                }
                else
                {
                    LambdaExpression lambda = Expression.Lambda(body.Arguments[0]);
                    var compiledExpression = lambda.Compile();
                    return compiledExpression.DynamicInvoke();
                }
            }

            private static object GetExpressionValue(BinaryExpression body)
            {
                if (body.Right is ConstantExpression)
                {
                    return ((dynamic)body.Right).Value;
                }
                else
                {
                    LambdaExpression lambda = Expression.Lambda(body.Right);
                    var compiledExpression = lambda.Compile();
                    return compiledExpression.DynamicInvoke();
                }
            }

            private static object GetExpressionHavingValue(MethodCallExpression body)
            {
                if (body.Arguments[0] is ConstantExpression)
                {
                    return ((dynamic)body.Arguments[0]).Value;
                }
                else
                {
                    LambdaExpression lambda = Expression.Lambda(body.Arguments[0]);
                    var compiledExpression = lambda.Compile();
                    return compiledExpression.DynamicInvoke();
                }
            }

            private static object GetExpressionHavingValue(BinaryExpression body)
            {
                if (body.Right is ConstantExpression)
                {
                    return ((dynamic)body.Right).Value;
                }
                // Beleive me don't have time for this
                else if(body.Right.GetType().Name.IndexOf("InstanceMethodCallExpression") > -1)
                {
                    return GetHavingFunctionAndColumn(body);
                }
                else
                {
                    LambdaExpression lambda = Expression.Lambda(body.Right);
                    var compiledExpression = lambda.Compile();
                    return compiledExpression.DynamicInvoke();
                }
            }

            /// <summary>
            /// Gets the name of the property.
            /// </summary>
            /// <param name="body">The body.</param>
            /// <returns>The property name for the property expression.</returns>
            private static string GetPropertyName(MethodCallExpression body)
            {
                string propertyName = body.Object.ToString().Split(new char[] { '.' })[1];

                return propertyName;
            }


            /// <summary>
            /// Gets the name of the property.
            /// </summary>
            /// <param name="body">The body.</param>
            /// <returns>The property name for the property expression.</returns>
            private static string GetHavingFunctionAndColumn(MethodCallExpression body)
            {
                string propertyName = body.Object.ToString().Split(new char[] { '.' })[1];

                return propertyName;
            }

            /// <summary>
            /// Gets the operator.
            /// </summary>
            /// <param name="type">The type.</param>
            /// <returns>
            /// The expression types SQL server equivalent operator.
            /// </returns>
            /// <exception cref="System.NotImplementedException"></exception>
            private static string GetOperator(ExpressionType type)
            {
                switch (type)
                {
                    case ExpressionType.Equal:
                        return " = ";
                    case ExpressionType.NotEqual:
                        return " != ";
                    case ExpressionType.LessThan:
                        return " < ";
                    case ExpressionType.LessThanOrEqual:
                        return " <= ";
                    case ExpressionType.GreaterThan:
                        return " > ";
                    case ExpressionType.GreaterThanOrEqual:
                        return " >= ";
                    case ExpressionType.AndAlso:
                    case ExpressionType.And:
                        return " AND ";
                    case ExpressionType.Or:
                    case ExpressionType.OrElse:
                        return " OR ";
                    case ExpressionType.Default:
                        return string.Empty;
                    default:
                        throw new NotImplementedException();
                }
            }
        }

#region Expression helpers
        public static class ExpressionExtensions
        {
            public static string GetMethodName<T>(this Expression<Func<T>> expression)
            {
                var body = (MethodCallExpression)expression.Body;
                return body.Method.Name;
            }

            public static ReadOnlyCollection<Expression> GetInnerArguments<T>(this Expression<Func<T>> expression)
            {
                var body = (MethodCallExpression)expression.Body;
                return body.GetInnerArguments();
            }

            public static ReadOnlyCollection<Expression> GetInnerArguments(this MethodCallExpression expression)
            {
                var args = new List<Expression>();

                var arguments = expression.Arguments;

                foreach (var a in arguments)
                {
                    var methodCallExpression = a.AsMethodCallExpression();
                    if (methodCallExpression != null && methodCallExpression.Arguments.Count > 0)
                    {
                        args.AddRange(methodCallExpression.GetInnerArguments());
                    }
                    else
                    {
                        args.Add(a);
                    }
                }

                return new ReadOnlyCollection<Expression>(args.Where(a => a.NodeType == ExpressionType.MemberAccess).ToList());
            }

            public static MethodCallExpression AsMethodCallExpression(this Expression expression)
            {
                return expression as MethodCallExpression;
            }
        }

        internal enum ClauseType
        {
            Where, Having
        }
#endregion

        /// <summary>
        /// Class that models the data structure in coverting the expression tree into SQL and Params.
        /// </summary>
        internal class QueryParameter
        {
            public string LinkingOperator { get; set; }
            public string PropertyName { get; set; }
            public object PropertyValue { get; set; }
            public string QueryOperator { get; set; }

            /// <summary>
            /// Initializes a new instance of the <see cref="QueryParameter" /> class.
            /// </summary>
            /// <param name="linkingOperator">The linking operator.</param>
            /// <param name="propertyName">Name of the property.</param>
            /// <param name="propertyValue">The property value.</param>
            /// <param name="queryOperator">The query operator.</param>
            internal QueryParameter(string linkingOperator, string propertyName, object propertyValue, string queryOperator)
            {
                this.LinkingOperator = linkingOperator;
                this.PropertyName = propertyName;
                this.PropertyValue = propertyValue;
                this.QueryOperator = queryOperator;
            }
        }

        /// <summary>
        /// Class that models the data structure in coverting the expression tree into SQL Having and Params.
        /// </summary>
        internal class QueryHavingParameter
        {
            public string LinkingOperator { get; set; }
            public string FunctionName { get; set; }
            public string PropertyName { get; set; }
            public object PropertyValue { get; set; }
            public string QueryOperator { get; set; }

            /// <summary>
            /// Initializes a new instance of the <see cref="QueryParameter" /> class.
            /// </summary>
            /// <param name="linkingOperator">The linking operator.</param>
            /// <param name="propertyName">Name of the property.</param>
            /// <param name="propertyValue">The property value.</param>
            /// <param name="queryOperator">The query operator.</param>
            internal QueryHavingParameter(string linkingOperator, string functionName, string propertyName, object propertyValue, string queryOperator)
            {
                this.LinkingOperator = linkingOperator;
                this.FunctionName = functionName;
                this.PropertyName = propertyName;
                this.PropertyValue = propertyValue;
                this.QueryOperator = queryOperator;
            }
        }
    }
    public class Aggregator
    {
        private string _having = "";
        public TKey Sum<TKey>(TKey selector)
        {
            return default(TKey);
        }
        public TKey Count<TKey>(TKey selector)
        {
            return default(TKey);
        }
        public TKey Min<TKey>(TKey selector)
        {
            return default(TKey);
        }
        public TKey Max<TKey>(TKey selector)
        {
            return default(TKey);
        }
        public TKey Average<TKey>(TKey selector)
        {
            return default(TKey);
        }

        public string GetHaving()
        {
            return " HAVING " + _having;
        }
    }

}

namespace SimpleAccess.Core.Entity.RepoWrapper
{
    public class QueryResult
    {
        public string Query { get; set; }

        public IDictionary<string,object> Values { get; set; }
        public QueryResult(string query, IDictionary<string, object> values)
        {
            Query = query;
            Values = values;
        }
    }
}

// #endif