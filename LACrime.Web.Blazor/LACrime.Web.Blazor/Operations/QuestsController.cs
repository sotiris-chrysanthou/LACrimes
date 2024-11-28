using System.Data;
using LACrime.Core.Base;
using LACrimes.EF.Context;
using LACrimes.EF.Repository;
using LACrimes.Model;
using LACrimes.Web.Blazor.Shared;
using LACrimes.Web.Blazor.Shared.Quest;
using Microsoft.AspNetCore.Mvc;
using Npgsql;


namespace LACrimes.Web.Blazor.Server.Operations {
    [Route("api/[controller]")]
    [ApiController]
    public class QuestsController : ControllerBase {
        private readonly IEntityRepo<CrimeRecord> _crimeRecordRepo;
        private readonly LACrimeDbContext _context;

        public QuestsController(LACrimeDbContext context, IEntityRepo<CrimeRecord> crimeRecordRepo) {
            _crimeRecordRepo = crimeRecordRepo;
            _context = context;
        }

        [HttpGet("Quest1")]
        public async Task<IActionResult> Quest1([FromQuery] DateTime startDate, [FromQuery] DateTime endDate) {
            try {
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "SqlQueries", "Quest1.sql");
                string sqlQuery = await System.IO.File.ReadAllTextAsync(filePath);
                if(sqlQuery == null) {
                    return BadRequest("SQL Query not found");
                }

                var parameters = new[] {
                    new NpgsqlParameter("@startDate", startDate),
                    new NpgsqlParameter("@endDate", endDate),
                    new NpgsqlParameter("@severity", 1)
                };

                DataTable dt = await LACrimeSys.ExecuteQueryAsync(_context, sqlQuery, parameters);
                var groupedCrimeRecords = dt.AsEnumerable()
                    .Select(row => new Quest1ReportDto {
                        CrmCd = row["CrmCd"].ToString(),
                        CrimeDescription = row["CrimeDescription"].ToString(),
                        TotalReports = Convert.ToInt32(row["TotalReports"])
                    })
                    .OrderByDescending(r => r.TotalReports)
                    .ToList();

                return Ok(groupedCrimeRecords);
            } catch(Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Quest2")]
        public async Task<IActionResult> Quest2([FromQuery] int crmCd, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate) {
            try {
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "SqlQueries", "Quest2.sql");
                string sqlQuery = await System.IO.File.ReadAllTextAsync(filePath);

                if(sqlQuery == null) {
                    return BadRequest("SQL Query not found");
                }

                var parameters = new[] {
            new NpgsqlParameter("@crmCd", crmCd),
            new NpgsqlParameter("@startDate", startDate),
            new NpgsqlParameter("@endDate", endDate)
        };

                DataTable dt = await LACrimeSys.ExecuteQueryAsync(_context, sqlQuery, parameters);
                var crimeReports = dt.AsEnumerable()
                    .Select(row => new {
                        ReportDate = Convert.ToDateTime(row["ReportDate"]),
                        TotalReports = Convert.ToInt32(row["TotalReports"])
                    })
                    .ToList();

                return Ok(crimeReports);
            } catch(Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Quest3")]
        public async Task<IActionResult> Quest3([FromQuery] DateTime date) {
            try {
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "SqlQueries", "Quest3.sql");
                string sqlQuery = await System.IO.File.ReadAllTextAsync(filePath);

                if(sqlQuery == null) {
                    return BadRequest("SQL Query not found");
                }

                var parameters = new[] {
                    new NpgsqlParameter("@date", date)
                };

                DataTable dt = await LACrimeSys.ExecuteQueryAsync(_context, sqlQuery, parameters);
                var quest3ReportDto = dt.AsEnumerable()
                    .Select(row => new Quest3ReportDto {
                        AreaCode = row["AreaCode"].ToString(),
                        AreaName = row["AreaName"].ToString(),
                        CrimeCode = row["CrimeCode"].ToString(),
                        CrimeDescription = row["CrimeDescription"].ToString(),
                        TotalReports = Convert.ToInt32(row["TotalReports"])
                    })
                    .ToList();

                return Ok(quest3ReportDto);
            } catch(Exception ex) {
                return BadRequest(ex.Message);
            }
        }
    }
}
