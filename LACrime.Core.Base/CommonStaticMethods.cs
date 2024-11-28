using System.Data;
using LACrimes.EF.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace LACrime.Core.Base {
    public static class LACrimeSys {
        public static async Task<DataTable> ExecuteQueryAsync(LACrimeDbContext context, string sqlQuery, NpgsqlParameter[] parameters) {
            using(var connection = context.Database.GetDbConnection()) {
                await connection.OpenAsync();
                using(var command = connection.CreateCommand()) {
                    command.CommandText = sqlQuery;
                    command.Parameters.AddRange(parameters);

                    using(var reader = await command.ExecuteReaderAsync()) {
                        var dataTable = new DataTable();
                        dataTable.Load(reader);
                        return dataTable;
                    }
                }
            }
        }
    }
}
