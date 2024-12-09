using System.Data.Common;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using LACrimes.EF.Repository;
using LACrimes.Model;
using LACrimes.Web.Blazor.Server.Controllers;
using LACrimes.Web.Blazor.Server.Helpers;
using LACrimes.Web.Blazor.Shared;
using LACrimes.Web.Blazor.Shared.CrimeRecordDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LACrimes.Web.Blazor.Server.Operations {
    [ApiController]
    [Route("api/[controller]")]
    public class BulkDataImport : ControllerBase {
        private readonly CrimeRecordController _crimeRecordController;
        private readonly IEntityRepo<Crime> _crimeRepo;
        private readonly ILogger<BulkDataImport> _logger;
        private laLists _lists = new laLists();


        public BulkDataImport(CrimeRecordController crimeRecordController, IEntityRepo<Crime> crimeRepo, ILogger<BulkDataImport> logger) {
            _crimeRecordController = crimeRecordController;
            _crimeRepo = crimeRepo;
            _logger = logger;
        }

        [HttpPost("UploadChunk")]
        public IActionResult UploadChunk() {
            try {
                string message = String.Empty;
                var formFile = Request.Form.Files.FirstOrDefault();
                if(formFile == null) {
                    message = "No file uploaded.";
                    _ = laLogger.Log(_logger, message, LogType.Error);
                    return BadRequest(message);
                }

                var tempFilePath = Path.Combine(Path.GetTempPath(), formFile.FileName);

                using var stream = new FileStream(tempFilePath, FileMode.Append);
                formFile.CopyTo(stream);
                message = $"Chunk uploaded successfully: {formFile.FileName}";
                _ = laLogger.Log(_logger, message, LogType.Information);
                return Ok(message);
            } catch(Exception ex) {
                string errorMessage = $"An error occurred while uploading the file chunk: {ex.Message}";
                _ = laLogger.Log(_logger, errorMessage, LogType.Error);
                return StatusCode(500, errorMessage);
            }
        }

        [HttpPost("FinalizeUpload")]
        public async Task<IActionResult> FinalizeUpload([FromForm] string fileName) {
            try {
                var stopwatch = System.Diagnostics.Stopwatch.StartNew();
                var tempFilePath = Path.Combine(Path.GetTempPath(), fileName);
                if(!System.IO.File.Exists(tempFilePath)) {
                    string errorMessage = $"File not found on server: {tempFilePath}";
                    _ = laLogger.Log(_logger, errorMessage, LogType.Error);
                    return BadRequest(errorMessage);
                }

                // Process the file inside a scoped block to ensure all resources are disposed
                List<CrimeRecordCsvModel> csvRecords;
                using(var reader = new StreamReader(tempFilePath)) {
                    var config = new CsvConfiguration(CultureInfo.InvariantCulture) {
                        HeaderValidated = null,
                        MissingFieldFound = null,
                        Delimiter = ",",
                        BadDataFound = null
                    };
                    using(var csv = new CsvReader(reader, config)) {
                        csv.Context.RegisterClassMap<CrimeRecordCsvModelMap>();
                        csvRecords = csv.GetRecords<CrimeRecordCsvModel>().ToList();
                    }
                }

                // Build and post crimes as per your existing code
                var crimeCodeDescriptions = await BuildCrimeCodeDescriptions(csvRecords);
                stopwatch.Stop();
                var elapsedTime = stopwatch.Elapsed;
                string infoMessage = $"Time taken for BuildCrimeCodeDescriptions: {elapsedTime}";
                _ = laLogger.Log(_logger, infoMessage, LogType.Information);
                stopwatch = System.Diagnostics.Stopwatch.StartNew();
                foreach(var csvRecord in csvRecords) {
                    try {
                        // Ensure crm_cd is the same as crm_cd_1
                        csvRecord.CrmCd1 = csvRecord.CrmCd;

                        var crimeRecordDto = await MapCsvModelToDto(csvRecord);
                        if(crimeRecordDto == null) {
                            string errorMessage = $"Failed to process record with DrNo: {csvRecord.DrNo}";
                            _ = laLogger.Log(_logger, errorMessage, LogType.Error);
                            continue;
                        }

                        // Post the CrimeRecordDto
                        (object result, laLists? templists) = await _crimeRecordController.PostCrimeRecord(crimeRecordDto, _lists);
                        if(templists != null) {
                            _lists = templists;
                        }
                        //if(result.Result is ObjectResult objectResult && objectResult.StatusCode != StatusCodes.Status200OK) {
                        //}
                        if(result is DbException dbEx) {

                            _ = laLogger.Log(_logger, $"Failed to import record with DrNo: {crimeRecordDto.DrNo}.", LogType.Error);
                            _ = laLogger.Log(_logger, "DbException: " + dbEx.Message, LogType.Error);
                            if(dbEx.InnerException != null) {
                                _ = laLogger.Log(_logger, "dbInnerException: " + dbEx.InnerException.Message, LogType.Error);
                            }
                            continue;
                        } else if(result is DbUpdateException dbUpdateEx) {
                            _ = laLogger.Log(_logger, $"Failed to import record with DrNo: {crimeRecordDto.DrNo}.", LogType.Error);
                            _ = laLogger.Log(_logger, "DbUpdateException: " + dbUpdateEx.Message, LogType.Error);
                            if(dbUpdateEx.InnerException != null) {
                                _ = laLogger.Log(_logger, "DbUpdateInnerException: " + dbUpdateEx.InnerException.Message, LogType.Error);
                            }
                            continue;
                        } else if(result is Exception ex) {
                            _ = laLogger.Log(_logger, $"Failed to import record with DrNo: {crimeRecordDto.DrNo}.", LogType.Error);
                            _ = laLogger.Log(_logger, "Exception: " + ex.Message, LogType.Error);
                            if(ex.InnerException != null) {
                                _ = laLogger.Log(_logger, "InnerException: " + ex.InnerException.Message, LogType.Error);
                            }
                            continue;
                        }

                    } catch(Exception ex) {
                        string errorMessage = $"An error occurred while processing record with DrNo: {csvRecord.DrNo}: {ex.Message}";
                        _ = laLogger.Log(_logger, errorMessage, LogType.Error, ex: ex);
                    }
                }

                // After all operations, delete the temporary file
                System.IO.File.Delete(tempFilePath);
                stopwatch.Stop();
                elapsedTime = stopwatch.Elapsed;
                Console.WriteLine($"Time taken for PostAsync: {elapsedTime}");
                return Ok("Data imported successfully");
            } catch(Exception ex) {
                string errorMessage = $"An error occurred while importing data: {ex.Message}";
                _ = laLogger.Log(_logger, errorMessage, LogType.Error, throwEx: true, ex);
                return StatusCode(500, errorMessage);
            }
        }

        private async Task<Dictionary<int, string>> BuildCrimeCodeDescriptions(List<CrimeRecordCsvModel> csvRecords) {
            var crimeCodeDescriptions = new Dictionary<int, string>();
            CrimeRepo crimeRepo = new CrimeRepo();
            foreach(var csvRecord in csvRecords) {
                if(int.TryParse(csvRecord.CrmCd, out int crmCd) && !string.IsNullOrWhiteSpace(csvRecord.CrmCdDesc)) {

                    if(!crimeCodeDescriptions.ContainsKey(crmCd)) {
                        IList<Crime> crimes = await crimeRepo.GetAll(c => c.Code == crmCd);
                        if(crimes.Any()) {
                            Crime crm = crimes.First();
                            if(crm.Desc != csvRecord.CrmCdDesc) {
                                string errorMessage = $"Crime code {crmCd} has different descriptions: {crm.Desc} and {csvRecord.CrmCdDesc}. DrNo: {csvRecord.DrNo}";
                                _ = laLogger.Log(_logger, errorMessage, LogType.Error);
                                continue;
                            }
                            crimeCodeDescriptions[crmCd] = crm.Desc;
                            continue;
                        }
                        crimeCodeDescriptions[crmCd] = csvRecord.CrmCdDesc;

                        // Create a new Crime instance
                        Crime crime = new Crime {
                            Code = crmCd,
                            Desc = csvRecord.CrmCdDesc
                        };

                        // Add the crime to the database
                        await _crimeRepo.Add(crime);
                    }
                }
            }

            return crimeCodeDescriptions;
        }

        private async Task<CrimeRecordDto?> MapCsvModelToDto(CrimeRecordCsvModel csvModel) {
            var crimeSeverities = await GetCrimeSeveritiesFromCsvModel(csvModel);
            if(crimeSeverities == null) {
                string message = $"Error processing CrimeRecordCsvModel for DrNo {csvModel.DrNo}";
                _ = laLogger.Log(_logger, message, LogType.Error, throwEx: true);
                return null;
            }
            DateTime dateRptd;
            DateTime dateOcc;

            try {
                dateRptd = DateTime.ParseExact(csvModel.DateRptd, "MM/dd/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
            } catch(Exception ex) {
                var errorMessage = $"Error parsing DateRptd for DrNo {csvModel.DrNo}: {ex.Message}{Environment.NewLine}";
                _ = laLogger.Log(_logger, errorMessage, LogType.Warning);
                dateRptd = DateTime.MinValue;
            }

            try {
                dateOcc = DateTime.ParseExact(csvModel.DateOcc, "MM/dd/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
            } catch(Exception ex) {
                var errorMessage = $"Error parsing DateOcc for DrNo {csvModel.DrNo}: {ex.Message}{Environment.NewLine}";
                _ = laLogger.Log(_logger, errorMessage, LogType.Warning);
                dateOcc = DateTime.MinValue;
            }

            int victAge;
            if(!int.TryParse(csvModel.VictAge, out victAge)) {
                var errorMessage = $"Error parsing VictAge for DrNo {csvModel.DrNo}: Invalid integer value{Environment.NewLine}";
                _ = laLogger.Log(_logger, errorMessage, LogType.Warning);
                victAge = 0;
            }

            int premisCode = 0;
            if(!string.IsNullOrEmpty(csvModel.PremisCd) && !int.TryParse(csvModel.PremisCd, out premisCode)) {
                var errorMessage = $"Error parsing PremisCd for DrNo {csvModel.DrNo}: Invalid integer value{Environment.NewLine}";
                _ = laLogger.Log(_logger, errorMessage, LogType.Warning);
                premisCode = 0;
            }

            int weaponCode = 0;
            if(!string.IsNullOrEmpty(csvModel.WeaponUsedCd) && !int.TryParse(csvModel.WeaponUsedCd, out weaponCode)) {
                var errorMessage = $"Error parsing WeaponUsedCd for DrNo {csvModel.DrNo}: Invalid integer value{Environment.NewLine}";
                _ = laLogger.Log(_logger, errorMessage, LogType.Warning);
                weaponCode = 0;
            }

            double lat;
            if(!double.TryParse(csvModel.Lat.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out lat)) {
                var errorMessage = $"Error parsing Lat for DrNo {csvModel.DrNo}: Invalid double value{Environment.NewLine}";
                _ = laLogger.Log(_logger, errorMessage, LogType.Warning);
                lat = 0;
            }

            double lon;
            if(!double.TryParse(csvModel.Lon.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out lon)) {
                var errorMessage = $"Error parsing Lon for DrNo {csvModel.DrNo}: Invalid double value{Environment.NewLine}";
                _ = laLogger.Log(_logger, errorMessage, LogType.Warning);
                lon = 0;
            }

            var crimeRecordDto = new CrimeRecordDto {
                DrNo = csvModel.DrNo,
                DateRptd = dateRptd,
                DateOcc = dateOcc,
                TimeOcc = ParseTimeOcc(csvModel.TimeOcc),
                AreaCode = csvModel.Area,
                AreaName = csvModel.AreaName,
                RpdDistNo = csvModel.RptDistNo,
                VictAge = victAge,
                VictSex = csvModel.VictSex,
                VictimDescent = csvModel.VictDescent,
                PremisCode = premisCode,
                PremisDesc = csvModel.PremisDesc,
                WeaponCode = weaponCode,
                WeaponDesc = csvModel.WeaponDesc,
                StatusCode = csvModel.Status,
                StatusDesc = csvModel.StatusDesc,
                StreetName = csvModel.Location,
                CrossStreetName = csvModel.CrossStreet,
                Lat = lat,
                Lon = lon,
                CrimeSeverities = crimeSeverities
            };

            return crimeRecordDto;
        }

        private TimeOnly ParseTimeOcc(string timeOcc) {
            if(int.TryParse(timeOcc, out var timeInt)) {
                var hours = timeInt / 100;
                var minutes = timeInt % 100;
                return new TimeOnly(hours, minutes);
            }
            return TimeOnly.MinValue;
        }

        private async Task<List<CrimeSeverityDto>?> GetCrimeSeveritiesFromCsvModel(CrimeRecordCsvModel csvModel) {
            var crimeSeverities = new List<CrimeSeverityDto>();

            if(string.IsNullOrWhiteSpace(csvModel.CrmCd1)) {
                string errorMessage = $"Invalid crm_cd_1 in record with DrNo {csvModel.DrNo}.";
                _ = laLogger.Log(_logger, errorMessage, LogType.Error, throwEx: true);
                return null;
            }
            if(csvModel.CrmCd != csvModel.CrmCd1) {
                string errorMessage = $"crm_cd must be the same as crm_cd_1 in record with DrNo {csvModel.DrNo}.";
                _ = laLogger.Log(_logger, errorMessage, LogType.Error, throwEx: true);
                return null;
            }

            // crm_cd_1 is required and has severity 1
            if(int.TryParse(csvModel.CrmCd1, out int crimeCode1)) {
                var existingCrimes = await _crimeRepo.GetAll(c => c.Code == crimeCode1);
                if(existingCrimes.Any()) {
                    crimeSeverities.Add(new CrimeSeverityDto {
                        Code = crimeCode1,
                        Desc = csvModel.CrmCdDesc,
                        Severity = 1
                    });
                } else {
                    string errorMessage = $"Crime with code {crimeCode1} not found in database";
                    _ = laLogger.Log(_logger, errorMessage, LogType.Error, throwEx: true);
                    return null;
                }
            } else {
                string errorMessage = $"Invalid crm_cd_1 in record with DrNo {csvModel.DrNo}.";
                _ = laLogger.Log(_logger, errorMessage, LogType.Error, throwEx: true);
                return null;
            }

            // crm_cd_2, crm_cd_3, crm_cd_4
            await AddOptionalCrimeSeverity(csvModel.CrmCd2, 2);
            await AddOptionalCrimeSeverity(csvModel.CrmCd3, 3);
            await AddOptionalCrimeSeverity(csvModel.CrmCd4, 4);

            return crimeSeverities;

            async Task AddOptionalCrimeSeverity(string crmCdField, int severity) {
                if(int.TryParse(crmCdField, out int crimeCode)) {
                    var existingCrimes = await _crimeRepo.GetAll(c => c.Code == crimeCode);
                    if(existingCrimes.Any()) {
                        crimeSeverities.Add(new CrimeSeverityDto {
                            Code = crimeCode,
                            Desc = existingCrimes.First().Desc,
                            Severity = severity
                        });
                    } else {
                        crimeSeverities.Add(new CrimeSeverityDto {
                            Code = crimeCode,
                            Desc = " ",
                            Severity = severity
                        });
                        string errorMessage = $"Crime with code {crimeCode} not found in database for record with for DrNo {csvModel.DrNo}. New crime created with code: {crmCdField} and empty description. Correct it later if necessary.{Environment.NewLine}";
                        _ = laLogger.Log(_logger, errorMessage, LogType.Warning);
                    }
                }
            }
        }
    }
}
