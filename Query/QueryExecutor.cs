using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkCore.Query.Extensions.Query
{
    internal static class QueryExecutor
    {
        /// <summary>
        /// internal querying function
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="sql"></param>
        /// <param name="resultSetMappingTypes"></param>
        /// <param name="commandType"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static IList<IList> QueryMultiple(this DbContext dbContext,
            string sql,
            ICollection<Type>? resultSetMappingTypes,
            CommandType commandType = CommandType.StoredProcedure,
            params SqlParameter[]? parameters)
        {
            var resultSets = new List<IList>();

            DbCommand command = CreateCommand(dbContext, sql, commandType, parameters);

            var types = resultSetMappingTypes?.ToArray();
            int counter = 0;

            using (var reader = command.ExecuteReader())
            {
                do
                {
                    if (types == null || counter > types.Length - 1) { break; }
                    var resultSetValues = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(types[counter]));

                    while (reader.Read())
                    {
                        Materializer.MaterializeRecord(types, counter, reader, resultSetValues);
                    }
                    resultSets.Add(resultSetValues);
                    counter++;
                }
                while (reader.NextResult());
                reader.Close();
            }
            return resultSets;
        }
        /// <summary>
        /// Internal querying function
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="sql"></param>
        /// <param name="resultSetMappingTypes"></param>
        /// <param name="commandType"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static async Task<IList<IList>> QueryMultipleAsync(this DbContext dbContext,
            string sql,
            ICollection<Type>? resultSetMappingTypes,
            CommandType commandType = CommandType.StoredProcedure,
            params SqlParameter[]? parameters)
        {
            var resultSets = new List<IList>();

            DbCommand command = CreateCommand(dbContext, sql, commandType, parameters);

            var types = resultSetMappingTypes?.ToArray();
            int counter = 0;

            using (var reader = await command.ExecuteReaderAsync())
            {
                do
                {
                    if (types == null || counter > types.Length - 1) { break; }
                    var resultSetValues = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(types[counter]));

                    while (reader.Read())
                    {
                        Materializer.MaterializeRecord(types, counter, reader, resultSetValues);
                    }
                    resultSets.Add(resultSetValues);
                    counter++;
                }
                while (await reader.NextResultAsync());
                reader.Close();
            }
            return resultSets;
        }
        /// <summary>
        /// Create dbCommand
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="sql"></param>
        /// <param name="commandType"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private static DbCommand CreateCommand(DbContext dbContext, string sql, CommandType commandType = CommandType.StoredProcedure, SqlParameter[]? parameters = null)
        {
            var connection = dbContext.Database.GetDbConnection();
            var command = connection.CreateCommand();
            command.CommandText = sql;
            command.CommandType = commandType;

            if (parameters != null && parameters.Any())
                command.Parameters.AddRange(parameters);

            if (command.Connection.State != ConnectionState.Open)
                command.Connection.Open();
            return command;
        }
    }
}
