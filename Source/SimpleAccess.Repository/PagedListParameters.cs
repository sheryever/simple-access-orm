using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
 
namespace SimpleAccess.Repository
{

    /// <summary>
    /// The class will be moved to SimpleAccess.Extensions
    /// </summary>
    public class PagedListParameters<T>
        where T : DbParameter, new()
    {
        public PagedListParameters()
        {
            Parameters = new List<T>();
        }

        public PagedListParameters(params T[] otherParameters)
        {
            Parameters = otherParameters.ToList();
        }

        public PagedListParameters(int startRowIndex, int pageSize)
            : this()
        {
            this.StartRowIndex = startRowIndex;
            this.PageSize = pageSize;
        }

        public PagedListParameters(int startRowIndex, int pageSize, string sortExpression)
            : this(startRowIndex, pageSize, new T { ParameterName = "@sortExpression", Value = sortExpression })
        {

        }

        public PagedListParameters(int startRowIndex, int pageSize, params T[] otherParameters)
            : this(otherParameters)
        {
            this.StartRowIndex = startRowIndex;
            this.PageSize = pageSize;
        }

        public PagedListParameters(int startRowIndex, int pageSize, string sortExpression, params T[] otherParameters)
            : this(otherParameters)
        {
            var sortExpressionParam = new T { ParameterName = "@sortExpression", Value = sortExpression };
            Parameters.Add(sortExpressionParam);
            this.StartRowIndex = startRowIndex;
            this.PageSize = pageSize;
        }

        public void AddOtherParams(object otherParameters)
        {
            var otherParametersObj = otherParameters as Object;
            if (otherParametersObj != null)
            {
                var sqlParams = otherParametersObj.GetType().GetProperties().Select(
                   param =>
                   {

                       object value = param.GetValue(otherParameters);

                       if (value is IDataParameter)
                       { return value as T; }
                       if (param.Name.GetType().Name.ToLower() == "string" && value != null)
                       {
                           value = SafeSqlLiteral(value.ToString());
                       }
                       return new T { ParameterName = "@" + Clean(param.Name), Value = value ?? DBNull.Value };
                   }).ToList();
                Parameters.AddRange(sqlParams);
            }
        }

        private static string SafeSqlLiteral(string inputSQL)
        {
            return inputSQL.Replace("'", "''");
        }

        private static string Clean(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                switch (name[0])
                {
                    case '@':
                    case ':':
                    case '?':
                        return name.Substring(1);
                }
            }
            return name;
        }

        private List<T> Parameters { get; set; }
        private T _totalRowsParam;

        public int StartRowIndex { get; set; }
        public int PageSize { get; set; }

        public long TotalRows
        {
            get { return (long)(_totalRowsParam.Value == DBNull.Value || _totalRowsParam.Value == null ? 0 : _totalRowsParam.Value); }
        }

        public T[] GetParametersToExecute()
        {

            if (!Parameters.Exists(p => p.ParameterName == "@startIndex"))
            {
                _totalRowsParam = new T
                {
                    ParameterName = "@totalRows",
                    Value = 0,
                    DbType = DbType.Int64,
                    Direction = ParameterDirection.Output
                };
                Parameters.Add(new T { ParameterName = "@startIndex", Value = StartRowIndex });
                Parameters.Add(new T { ParameterName = "@pageSize", Value = PageSize });
                Parameters.Add(_totalRowsParam);
            }
            else
            {
                Parameters.First(p => p.ParameterName == "@startIndex").Value = this.StartRowIndex;
                Parameters.First(p => p.ParameterName == "@pageSize").Value = this.PageSize;
            }

            return Parameters.ToArray();
        }

        private T CreateT(string parameterName, object value)
        {
            return new T { ParameterName = parameterName, Value = value ?? DBNull.Value };
        }
    }


}
