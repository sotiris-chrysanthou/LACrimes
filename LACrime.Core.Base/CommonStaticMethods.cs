﻿using System.Data;
using LACrimes.EF.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace LACrime.Core.Base {
    public static class LACrimeSys {
        public static async Task<DataTable> ExecuteQueryAsync(LACrimeDbContext context, string sqlQuery, NpgsqlParameter[] parameters) {
            var connectionString = context.Database.GetConnectionString();

            if(string.IsNullOrEmpty(connectionString)) {
                throw new InvalidOperationException("Database connection string is null or empty.");
            }

            using(var connection = new NpgsqlConnection(connectionString)) {
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
