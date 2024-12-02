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

        [HttpGet("Quest4")]
        public async Task<IActionResult> Quest4([FromQuery] DateTime startDate, [FromQuery] DateTime endDate) {
            try {
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "SqlQueries", "Quest4.sql");
                string sqlQuery = await System.IO.File.ReadAllTextAsync(filePath);

                if(sqlQuery == null) {
                    return BadRequest("SQL Query not found");
                }

                var parameters = new[] {
                    new NpgsqlParameter("@startDate", startDate),
                    new NpgsqlParameter("@endDate", endDate)
                };

                DataTable dt = await LACrimeSys.ExecuteQueryAsync(_context, sqlQuery, parameters);
                var quest4ReportDto = dt.AsEnumerable()
                    .Select(row => new Quest4ReportDto {
                        Hour = Convert.ToInt32(row["hour"]),
                        AverageCrimes = Convert.ToDouble(row["AverageCrimes"])
                    })
                    .ToList();

                return Ok(quest4ReportDto);
            } catch(Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Quest5")]
        public async Task<IActionResult> Quest5([FromQuery] DateTime date, [FromQuery] double minLat, [FromQuery] double maxLat, [FromQuery] double minLon, [FromQuery] double maxLon) {
            try {
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "SqlQueries", "Quest5.sql");
                string sqlQuery = await System.IO.File.ReadAllTextAsync(filePath);

                if(sqlQuery == null) {
                    return BadRequest("SQL Query not found");
                }

                var parameters = new[] {
                    new NpgsqlParameter("@date", date),
                    new NpgsqlParameter("@minLat", minLat),
                    new NpgsqlParameter("@maxLat", maxLat),
                    new NpgsqlParameter("@minLon", minLon),
                    new NpgsqlParameter("@maxLon", maxLon)
                };

                DataTable dt = await LACrimeSys.ExecuteQueryAsync(_context, sqlQuery, parameters);
                var quest5ReportDto = dt.AsEnumerable()
                    .Select(row => new Quest5ReportDto {
                        CrmCd = row["CrmCd"].ToString(),
                        TotalReports = Convert.ToInt32(row["TotalReports"])
                    })
                    .FirstOrDefault();

                return Ok(quest5ReportDto);
            } catch(Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Quest6")]
        public async Task<IActionResult> Quest6([FromQuery] DateTime startDate, [FromQuery] DateTime endDate) {
            try {
                var parameters = new[]
                {
                    new NpgsqlParameter("@startDate", startDate),
                    new NpgsqlParameter("@endDate", endDate)
                };

                // Εκτέλεση του δεύτερου query: Top 5 Rpt Dist No
                string rptDistFilePath = Path.Combine(Directory.GetCurrentDirectory(), "SqlQueries", "Quest6_TopRptDistNo.sql");
                string rptDistSqlQuery = await System.IO.File.ReadAllTextAsync(rptDistFilePath);

                if(string.IsNullOrEmpty(rptDistSqlQuery)) {
                    return BadRequest("SQL Query for Top Rpt Dist No not found");
                }

                DataTable rptDistNoTable = await LACrimeSys.ExecuteQueryAsync(_context, rptDistSqlQuery, parameters);

                var topRptDistNos = rptDistNoTable.AsEnumerable()
                    .Select(row => new Quest6RptDistNoDto {
                        RptDistNo = row["RptDistNo"].ToString() ?? string.Empty,
                        DateOccurred = Convert.ToDateTime(row["DateOccurred"]),
                        TotalCrimes = Convert.ToInt32(row["TotalCrimes"])
                    })
                    .ToList();

                // Εκτέλεση του πρώτου query: Top 5 Area Names
                string areaFilePath = Path.Combine(Directory.GetCurrentDirectory(), "SqlQueries", "Quest6_TopAreas.sql");
                string areaSqlQuery = await System.IO.File.ReadAllTextAsync(areaFilePath);

                if(string.IsNullOrEmpty(areaSqlQuery)) {
                    return BadRequest("SQL Query for Top Areas not found");
                }

                DataTable areaTable = await LACrimeSys.ExecuteQueryAsync(_context, areaSqlQuery, parameters);

                var topAreas = areaTable.AsEnumerable()
                    .Select(row => new Quest6AreaDto {
                        AreaName = row["AreaName"].ToString() ?? string.Empty,
                        DateOccurred = Convert.ToDateTime(row["DateOccurred"]),
                        TotalCrimes = Convert.ToInt32(row["TotalCrimes"])
                    })
                    .ToList();


                var result = new Quest6ReportDto {
                    TopAreas = topAreas,
                    TopRptDistNos = topRptDistNos
                };

                return Ok(result);
            } catch(Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Quest7")]
        public async Task<IActionResult> Quest7([FromQuery] DateTime startDate, [FromQuery] DateTime endDate) {
            try {
                var parameters = new[] {
                    new NpgsqlParameter("@startDate", startDate),
                    new NpgsqlParameter("@endDate", endDate)
                };

                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "SqlQueries", "Quest7.sql");
                string sqlQuery = await System.IO.File.ReadAllTextAsync(filePath);

                if(string.IsNullOrEmpty(sqlQuery)) {
                    return BadRequest("SQL Query not found");
                }

                DataTable dt = await LACrimeSys.ExecuteQueryAsync(_context, sqlQuery, parameters);
                var quest7ReportDto = dt.AsEnumerable()
                    .Select(row => new Quest7ReportDto {
                        AreaName = row["AreaName"].ToString(),
                        CrimeCode1 = row["Crime1"].ToString(),
                        Crime1Desc = row["Crime1Desc"].ToString(),
                        CrimeCode2 = row["Crime2"].ToString(),
                        Crime2Desc = row["Crime2Desc"].ToString(),
                        CoOccurrences = Convert.ToInt32(row["CoOccurrences"])
                    })
                    .FirstOrDefault();

                if(quest7ReportDto == null) {
                    return NotFound();
                }

                return Ok(quest7ReportDto);
            } catch(Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Quest8")]
        public async Task<IActionResult> Quest8([FromQuery] int code, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate) {
            try {
                var parameters = new[] {
                    new NpgsqlParameter("@code", code),
                    new NpgsqlParameter("@startdate", startDate),
                    new NpgsqlParameter("@enddate", endDate)
                };

                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "SqlQueries", "Quest8.sql");
                string sqlQuery = await System.IO.File.ReadAllTextAsync(filePath);

                if(string.IsNullOrEmpty(sqlQuery)) {
                    return BadRequest("SQL Query not found");
                }

                DataTable dt = await LACrimeSys.ExecuteQueryAsync(_context, sqlQuery, parameters);
                var quest8ReportDto = dt.AsEnumerable()
                    .Select(row => new Quest8ReportDto {
                        Crime1Code = row["Crime1Code"].ToString(),
                        Crime1Description = row["Crime1Desc"].ToString(),
                        Crime2Code = row["Crime2Code"].ToString(),
                        Crime2Description = row["Crime2Desc"].ToString(),
                        CrimeCount = Convert.ToInt32(row["CrimeCount"])
                    })
                    .FirstOrDefault();

                if(quest8ReportDto == null) {
                    return NotFound();
                }

                return Ok(quest8ReportDto);
            } catch(Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Quest9")]
        public async Task<IActionResult> Quest9() {
            try {
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "SqlQueries", "Quest9.sql");
                string sqlQuery = await System.IO.File.ReadAllTextAsync(filePath);

                if(string.IsNullOrEmpty(sqlQuery)) {
                    return BadRequest("SQL Query not found");
                }

                NpgsqlParameter[] parameters = new NpgsqlParameter[] {};
                DataTable dt = await LACrimeSys.ExecuteQueryAsync(_context, sqlQuery, parameters);
                var quest9Reports = dt.AsEnumerable()
                    .Select(row => new Quest9ReportDto {
                        AgeGroupStart = Convert.ToInt32(row["AgeGroupStart"]),
                        AgeGroup = row["AgeGroup"].ToString(),
                        WeaponCode = row["WeaponCode"].ToString(),
                        WeaponDescription = row["WeaponDescription"].ToString(),
                        Count = Convert.ToInt32(row["Count"])
                    })
                    .OrderBy(r => r.AgeGroupStart)
                    .ToList();

                return Ok(quest9Reports);
            } catch(Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Quest10")]
        public async Task<IActionResult> Quest10([FromQuery] int crimeCode) {
            try {
                string areaFilePath = Path.Combine(Directory.GetCurrentDirectory(), "SqlQueries", "Quest10Area.sql");
                string areaSqlQuery = await System.IO.File.ReadAllTextAsync(areaFilePath);

                if(string.IsNullOrEmpty(areaSqlQuery)) {
                    return BadRequest("SQL Query for Area not found");
                }

                var parameters = new[] {
                    new NpgsqlParameter("@code", crimeCode)
                };

                DataTable areaTable = await LACrimeSys.ExecuteQueryAsync(_context, areaSqlQuery, parameters);
                var areaReport = areaTable.AsEnumerable()
                    .Select(row => new Quest10AreaReportDto {
                        AreaName = row["AreaName"].ToString(),
                        StartDate = Convert.ToDateTime(row["StartDate"]),
                        EndDate = Convert.ToDateTime(row["EndDate"]),
                        GapDays = Convert.ToDouble(row["MaxGapDays"])
                    })
                    .FirstOrDefault();

                string rptDistNoFilePath = Path.Combine(Directory.GetCurrentDirectory(), "SqlQueries", "Quest10RptDistNo.sql");
                string rptDistNoSqlQuery = await System.IO.File.ReadAllTextAsync(rptDistNoFilePath);

                if(string.IsNullOrEmpty(rptDistNoSqlQuery)) {
                    return BadRequest("SQL Query for Rpt Dist No not found");
                }

                DataTable rptDistNoTable = await LACrimeSys.ExecuteQueryAsync(_context, rptDistNoSqlQuery, parameters);
                var rptDistNoReport = rptDistNoTable.AsEnumerable()
                    .Select(row => new Quest10RptDistNoReportDto {
                        RptDistNo = row["RptDistNo"].ToString(),
                        StartDate = Convert.ToDateTime(row["StartDate"]),
                        EndDate = Convert.ToDateTime(row["EndDate"]),
                        GapDays = Convert.ToDouble(row["MaxGapDays"])
                    })
                    .FirstOrDefault();

                var result = new Quest10ReportDto {
                    AreaReport = areaReport,
                    RptDistNoReport = rptDistNoReport
                };

                return Ok(result);
            } catch(Exception ex) {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("Quest11")]
        public async Task<IActionResult> Quest11([FromQuery] int crimeCode1, [FromQuery] int crimeCode2) {
            try {
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "SqlQueries", "Quest11.sql");
                string sqlQuery = await System.IO.File.ReadAllTextAsync(filePath);

                if(string.IsNullOrEmpty(sqlQuery)) {
                    return BadRequest("SQL Query not found");
                }

                var parameters = new[] {
                    new NpgsqlParameter("@code1", crimeCode1),
                    new NpgsqlParameter("@code2", crimeCode2)
                };

                DataTable dt = await LACrimeSys.ExecuteQueryAsync(_context, sqlQuery, parameters);
                var quest11Reports = dt.AsEnumerable()
                    .Select(row => new Quest11ReportDto {
                        AreaName = row["AreaName"].ToString(),
                        CrimeDate = Convert.ToDateTime(row["CrimeDate"]),
                        Crime1Code = row["Crime1Code"].ToString(),
                        Crime2Code = row["Crime2Code"].ToString(),
                        CrimesCount = Convert.ToInt32(row["CrimesCount"])
                    })
                    .ToList();

                return Ok(quest11Reports);
            } catch(Exception ex) {
                return BadRequest(ex.Message);
            }
        }
    }
}
