using Microsoft.AspNetCore.Mvc;
using LACrimes;
using LACrimes.EF.Repository;
using System.ComponentModel.DataAnnotations;
using LACrimes.Model;
using Microsoft.AspNetCore.Authorization;
using System.Data.Common;
using LACrimes.Web.Blazor.Shared;
using System.Linq.Expressions;
using LACrimes.Web.Blazor.Server;
using Microsoft.Extensions.Localization;
using System.Resources;
using System.Linq.Dynamic.Core;
using LACrimes.Web.Blazor.Server.Helpers;
using Microsoft.EntityFrameworkCore;
using LACrimes.Web.Blazor.Shared.CrimeRecordDtos;
using LACrimes.EF.Context;

namespace LACrimes.Web.Blazor.Server.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class CrimeRecordController : ControllerBase {
        private readonly IEntityRepo<CrimeRecord> _crimeRecordRepo;
        private static readonly ResourceManager _lacrResource = new ResourceManager("LACrimes.Web.Blazor.lacrResource", typeof(CrimeRecordController).Assembly);
        //private readonly IValidator _validator;
        private String _errorMessage;

        public CrimeRecordController(IEntityRepo<CrimeRecord> repo) {
            _crimeRecordRepo = repo;
            _errorMessage = String.Empty;
        }

        // GET: api/<CrimeRecordController>/get/null
        [Route("/api/CrimeRecord/get/{predicateStr}")]
        [HttpGet]
        [Authorize(Roles = "User,Admin")]
        public async Task<ActionResult<IEnumerable<CrimeRecordDto>?>> Get(string predicateStr) {
            Expression<Func<CrimeRecord, bool>>? predicate = null;
            try {
                predicate = PredicateBuilder.BuildPredicate<CrimeRecord>(predicateStr);
                var result = await _crimeRecordRepo.GetAll(predicate);
                var selectCrimeRecordList = result.Select(crmR => new CrimeRecordDto(crmR)).ToList();
                return Ok(selectCrimeRecordList);
            } catch(DbException dbEx) {
                return NotFound(dbEx);
            }
        }

        // GET api/<CrimeRecordController>/get/5
        [Route("/api/CrimeRecord/get/{id:guid}")]
        [HttpGet]
        [Authorize(Roles = "User,Admin")]
        public async Task<ActionResult<CrimeRecordDto?>> GetById(Guid id) {
            try {
                var result = await _crimeRecordRepo.GetById(id);
                if(result == null) {
                    return NotFound("The requested crime record was not found. Please contact support");
                }
                CrimeRecordDto crimeRecordDto = new CrimeRecordDto(result);
                return Ok(crimeRecordDto);
            } catch(DbException) {
                return NotFound("The requested crime record was not found. Please contact support");
            }
        }

        // GET api/<CrimeRecordController>/details/5
        [Route("/api/CrimeRecord/details/{id:guid}")]
        [HttpGet]
        [Authorize(Roles = "User,Admin")]
        public async Task<ActionResult<CrimeRecordDto?>> GetByIdDetails(Guid id) {
            try {
                var result = await _crimeRecordRepo.GetById(id);
                if(result == null) {
                    return NotFound("The requested crime record was not found. Please contact support");
                }
                CrimeRecordDto crimeRecordDto = new CrimeRecordDto(result);
                return Ok(crimeRecordDto);
            } catch(DbException) {
                return NotFound("The requested crime record was not found. Please contact support");
            }
        }

        // POST api/<CrimeRecordController>
        [HttpPost]
        [Authorize(Roles = "User,Admin")]
        public async Task<ActionResult<String>> Post(CrimeRecordDto crimeRecordDto) {
            (object result, laLists? lists) = await PostCrimeRecord(crimeRecordDto);
            if(result is not null && result is Exception) {
                return Ok(((Exception)result).Message);
            } else if(result is not null && result is DbException) {
                return BadRequest(((DbException)result).Message);
            } else if(result is not null && result is DbUpdateException) {
                return BadRequest(((DbUpdateException)result).Message);
            }
            if(result is not null && result is String)
                return BadRequest((String)result);
            return BadRequest("An error occurred while creating the crime record. Please contact support");
        }

        internal async Task<(object, laLists?)> PostCrimeRecord(CrimeRecordDto crimeRecordDto, laLists? lists = null) {
            try {
                bool update = crimeRecordDto.ID != null;
                (CrimeRecord crimeRecord, lists) = await CrmRctrlHelper.CreateCrimeRecord(crimeRecordDto, lists: lists);
                if(update) {
                    await _crimeRecordRepo.Update(crimeRecord.ID, crimeRecord);
                    await CrmRctrlHelper.ManageCrimeSeverities(crimeRecordDto, crimeRecord);
                    return ($"Crime record updated successfully with DrNo {crimeRecord.DrNo}", lists);
                }
                await _crimeRecordRepo.Add(crimeRecord);
                crimeRecordDto.ID = crimeRecord.ID;
                await CrmRctrlHelper.PostNewCrimeSeverities(crimeRecordDto);
                return ($"Crime record created successfully with DrNo {crimeRecord.DrNo}", lists);
            } catch(DbException ex) {
                return (ex, lists);
            } catch(DbUpdateException ex) {
                return (ex, lists);
            } catch(Exception ex) {
                return (ex, lists);
            }
        }



        // PUT api/<CrimeRecordController>/5
        [HttpPut]
        [Authorize(Roles = "User,Admin")]
        public async Task<ActionResult> Put(CrimeRecordDto crimeRecordDto) {
            (object result, laLists? lists) = await PostCrimeRecord(crimeRecordDto);
            if(result is not null && result is Exception) {
                return Ok(((Exception)result).Message);
            } else if(result is not null && result is DbException) {
                return BadRequest(((DbException)result).Message);
            } else if(result is not null && result is DbUpdateException) {
                return BadRequest(((DbUpdateException)result).Message);
            }
            if(result is not null && result is String)
                return BadRequest((String)result);
            return BadRequest("An error occurred while creating the crime record. Please contact support");


        }

        // DELETE api/<CrimeRecordController>/5
        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "User,Admin")]
        public async Task<ActionResult> Delete(Guid id) {
            try {
                var crimeRecord = await _crimeRecordRepo.GetById(id);
                if(crimeRecord is null) {
                    return NotFound("Crime record not found");
                }
                await _crimeRecordRepo.Delete(id);
                return Ok();

            } catch(DbException ex) {
                return BadRequest(ex.Message);
            }
        }


    }
}
