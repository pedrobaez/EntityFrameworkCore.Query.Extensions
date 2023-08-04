using EntityFrameworkCore.Query.Extensions.Query;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace EntityFrameworkCore.Query.Extensions
{
    public static class EFCoreExtensions
    {
        /// <summary>
        /// Execute void query
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="sql"></param>
        /// <param name="commandType"></param>
        /// <param name="parameters"></param>
        public static void
          Execute(this DbContext dbContext, string sql, CommandType commandType = CommandType.StoredProcedure, params SqlParameter[]? parameters)
        {
            QueryExecutor.QueryMultiple(dbContext, sql, resultSetMappingTypes: null , commandType, parameters);
        }
        /// <summary>
        /// Execute Async void query
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="sql"></param>
        /// <param name="commandType"></param>
        /// <param name="parameters"></param>
        public static async void
         ExecuteAsync(this DbContext dbContext, string sql, CommandType commandType = CommandType.StoredProcedure, params SqlParameter[]? parameters)
        {
           await QueryExecutor.QueryMultipleAsync(dbContext, sql, resultSetMappingTypes: null, commandType, parameters);
        }

        /// <summary>
        /// QuerySingle result set
        /// <code>
        /// context.QuerySingle<TModel>("dbo.procedureName", CommandType.StoredProcedure ,new SqlParameter("@p0","pValue"));
        /// </code>
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <param name="dbContext"></param>
        /// <param name="sql"></param>
        /// <param name="commandType"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static IReadOnlyCollection<T1>
           QuerySingle<T1>(this DbContext dbContext, string sql, CommandType commandType = CommandType.StoredProcedure, params SqlParameter[]? parameters)
        {
            var resultSetMappingTypes = new[] { typeof(T1) };

            var resultSets = QueryExecutor.QueryMultiple(dbContext, sql, resultSetMappingTypes, commandType, parameters);

            return (IReadOnlyCollection<T1>)resultSets[0];
        }
        /// <summary>
        /// QuerySingle resultset Async
        /// <code>
        /// context.QuerySingleAsync<TModel>("dbo.procedureName", CommandType.StoredProcedure ,new SqlParameter("@p0","pValue"));
        /// </code>
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <param name="dbContext"></param>
        /// <param name="sql"></param>
        /// <param name="commandType"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static async Task<IReadOnlyCollection<T1>>
           QuerySingleAsync<T1>(this DbContext dbContext, string sql, CommandType commandType = CommandType.StoredProcedure, params SqlParameter[]? parameters)
        {
            var resultSetMappingTypes = new[] { typeof(T1) };

            var resultSets = await QueryExecutor.QueryMultipleAsync(dbContext, sql, resultSetMappingTypes, commandType, parameters);

            return (IReadOnlyCollection<T1>)resultSets[0];
        }
        /// <summary>
        /// QueryMultipleAsync resultSets
        /// <code>
        /// var results = context.QueryMultipleAsync<TModel1,TModel2>("dbo.procedureName",CommandType.StoreProcedure,new SqlParameter("@p0","pValue"))
        /// </code>
        /// </summary>
        /// <typeparam name="T1">First resultSet</typeparam>
        /// <typeparam name="T2">Second resultSet</typeparam>
        /// <param name="dbContext"></param>
        /// <param name="sql"></param>
        /// <param name="commandType"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static async Task<(IReadOnlyCollection<T1> FirstResultSet, IReadOnlyCollection<T2> SecondResultSet)>
           QueryMultipleAsync<T1, T2>(this DbContext dbContext, string sql, CommandType commandType = CommandType.StoredProcedure, params SqlParameter[]? parameters)
        {
            var resultSetMappingTypes = new[] { typeof(T1), typeof(T2) };

            var resultSets = await QueryExecutor.QueryMultipleAsync(dbContext, sql, resultSetMappingTypes, commandType, parameters);

            return ((IReadOnlyCollection<T1>)resultSets[0], (IReadOnlyCollection<T2>)resultSets[1]);
        }
        /// <summary>
        /// QueryMultiple resultSets
        /// <code>
        /// var results = context.QueryMultiple<TModel1,TModel2>("dbo.procedureName",CommandType.StoreProcedure,new SqlParameter("@p0","pValue"))
        /// </code>
        /// </summary>
        /// <typeparam name="T1">First resultSet</typeparam>
        /// <typeparam name="T2">Second resultSet</typeparam>
        /// <param name="dbContext"></param>
        /// <param name="sql"></param>
        /// <param name="commandType"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static (IReadOnlyCollection<T1> FirstResultSet, IReadOnlyCollection<T2> SecondResultSet)
          QueryMultiple<T1, T2>(this DbContext dbContext, string sql, CommandType commandType = CommandType.StoredProcedure, params SqlParameter[]? parameters)
        {
            var resultSetMappingTypes = new[] { typeof(T1), typeof(T2) };

            var resultSets = QueryExecutor.QueryMultiple(dbContext, sql, resultSetMappingTypes, commandType, parameters);

            return ((IReadOnlyCollection<T1>)resultSets[0], (IReadOnlyCollection<T2>)resultSets[1]);
        }
        /// <summary>
        /// QueryMultipleAsync resultSets
        /// <code>
        /// var results = context.QueryMultipleAsync<TModel1,TModel2,TModel3>("dbo.procedureName",CommandType.StoreProcedure,new SqlParameter("@p0","pValue"))
        /// </code>
        /// </summary>
        /// <typeparam name="T1">First resultSet</typeparam>
        /// <typeparam name="T2">Second resultSet</typeparam>
        /// <typeparam name="T3">Third resultSet</typeparam>
        /// <param name="dbContext"></param>
        /// <param name="sql"></param>
        /// <param name="commandType"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static async Task<(IReadOnlyCollection<T1> FirstResultSet, IReadOnlyCollection<T2> SecondResultSet, IReadOnlyCollection<T3> ThirdResultSet)>
         QueryMultipleAsync<T1, T2, T3>(this DbContext dbContext, string sql, CommandType commandType = CommandType.StoredProcedure, params SqlParameter[]? parameters)
        {
            var resultSetMappingTypes = new[] { typeof(T1), typeof(T2), typeof(T3) };

            var resultSets = await QueryExecutor.QueryMultipleAsync(dbContext, sql, resultSetMappingTypes, commandType, parameters);

            return ((IReadOnlyCollection<T1>)resultSets[0], (IReadOnlyCollection<T2>)resultSets[1], (IReadOnlyCollection<T3>)resultSets[2]);
        }
        /// <summary>
        /// QueryMultiple resultSets
        /// <code>
        /// var results = context.QueryMultiple<TModel1,TModel2,TModel3>("dbo.procedureName",CommandType.StoreProcedure,new SqlParameter("@p0","pValue"))
        /// </code>
        /// </summary>
        /// <typeparam name="T1">First resultSet</typeparam>
        /// <typeparam name="T2">Second resultSet</typeparam>
        /// <typeparam name="T3">Third resultSet</typeparam>
        /// <param name="dbContext"></param>
        /// <param name="sql"></param>
        /// <param name="commandType"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static (IReadOnlyCollection<T1> FirstResultSet, IReadOnlyCollection<T2> SecondResultSet, IReadOnlyCollection<T3> ThirdResultSet)
        QueryMultiple<T1, T2, T3>(this DbContext dbContext, string sql, CommandType commandType = CommandType.StoredProcedure, params SqlParameter[]? parameters)
        {
            var resultSetMappingTypes = new[] { typeof(T1), typeof(T2), typeof(T3) };

            var resultSets = QueryExecutor.QueryMultiple(dbContext, sql, resultSetMappingTypes, commandType, parameters);

            return ((IReadOnlyCollection<T1>)resultSets[0], (IReadOnlyCollection<T2>)resultSets[1], (IReadOnlyCollection<T3>)resultSets[2]);
        }

        /// <summary>
        /// QueryMultipleAsync resultSets
        /// </summary>
        /// <typeparam name="T1">First resultSet</typeparam>
        /// <typeparam name="T2">Second resultSet</typeparam>
        /// <typeparam name="T3">Third resultSet</typeparam>
        /// <typeparam name="T4">Fourth resultSet</typeparam>
        /// <param name="dbContext"></param>
        /// <param name="sql"></param>
        /// <param name="commandType"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static async Task<(IReadOnlyCollection<T1> FirstResultSet, IReadOnlyCollection<T2> SecondResultSet, IReadOnlyCollection<T3> ThirdResultSet, IReadOnlyCollection<T4> FourthResultSet)>
       QueryMultipleAsync<T1, T2, T3, T4>(this DbContext dbContext, string sql, CommandType commandType = CommandType.StoredProcedure, params SqlParameter[]? parameters)
        {
            var resultSetMappingTypes = new[] { typeof(T1), typeof(T2), typeof(T3), typeof(T4) };

            var resultSets = await QueryExecutor.QueryMultipleAsync(dbContext, sql, resultSetMappingTypes, commandType, parameters);

            return ((IReadOnlyCollection<T1>)resultSets[0], (IReadOnlyCollection<T2>)resultSets[1], (IReadOnlyCollection<T3>)resultSets[2], (IReadOnlyCollection<T4>)resultSets[3]);
        }
        /// <summary>
        /// QueryMultiple resultSets
        /// </summary>
        /// <typeparam name="T1">First resultSet</typeparam>
        /// <typeparam name="T2">Second resultSet</typeparam>
        /// <typeparam name="T3">Third resultSet</typeparam>
        /// <typeparam name="T4">Fourth resultSet</typeparam>
        /// <param name="dbContext"></param>
        /// <param name="sql"></param>
        /// <param name="commandType"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static (IReadOnlyCollection<T1> FirstResultSet, IReadOnlyCollection<T2> SecondResultSet, IReadOnlyCollection<T3> ThirdResultSet, IReadOnlyCollection<T4> FourthResultSet)
      QueryMultiple<T1, T2, T3, T4>(this DbContext dbContext, string sql, CommandType commandType = CommandType.StoredProcedure, params SqlParameter[]? parameters)
        {
            var resultSetMappingTypes = new[] { typeof(T1), typeof(T2), typeof(T3), typeof(T4) };

            var resultSets = QueryExecutor.QueryMultiple(dbContext, sql, resultSetMappingTypes, commandType, parameters);

            return ((IReadOnlyCollection<T1>)resultSets[0], (IReadOnlyCollection<T2>)resultSets[1], (IReadOnlyCollection<T3>)resultSets[2], (IReadOnlyCollection<T4>)resultSets[3]);
        }

    }

}
