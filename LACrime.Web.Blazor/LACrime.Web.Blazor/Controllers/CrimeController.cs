using LACrimes.EF.Repository;
using LACrimes.Model;
using LACrimes.Web.Blazor.Shared;
using System.Data.Common;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using System.Resources;
using LACrimes.Web.Blazor.Server.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace LACrimes.Web.Blazor.Server.Controllers {
    public class CrimeController : Controller {

        private readonly IEntityRepo<Crime> _crimeRepo;
        //private readonly IValidator _validator;
        private String _errorMessage;

        public CrimeController(IEntityRepo<Crime> repo) {
            _crimeRepo = repo;
            _errorMessage = String.Empty;
        }

        // GET: api/<Crime>/get/null
        [Route("/api/Crime/get/{predicateStr}")]
        [HttpGet]
        [Authorize(Roles = "User,Admin")]
        public async Task<ActionResult<IEnumerable<CrimeDto>?>> Get(string predicateStr) {
            Expression<Func<Crime, bool>>? predicate = null;
            try {
                predicate = PredicateBuilder.BuildPredicate<Crime>(predicateStr);
                var result = await _crimeRepo.GetAll(predicate);
                var selectCrimeRecordList = result.Select(crm => new CrimeDto(crm)).ToList();
                return Ok(selectCrimeRecordList);
            } catch(DbException dbEx) {
                return NotFound(dbEx);
            }
        }
    }
}
