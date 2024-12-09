using System.Net.Http;
using LACrimes.EF.Repository;
using LACrimes.Model;
using LACrimes.Web.Blazor.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.JSInterop;

namespace LACrimes.Web.Blazor.Server.Operations {
    [Route("api/[controller]")]
    [ApiController]
    public class CrimeCheckController : Controller {

        [HttpGet]
        public async Task<ActionResult<CrimeCheckResponse?>> Execute(int crimeCode, string? crimeDesc) {
            try {
                CrimeCheckResponse response = new CrimeCheckResponse();
                CrimeRepo crimeRepo = new CrimeRepo();
                IList<Crime> crimeList = await crimeRepo.GetAll(c => c.Code == crimeCode);
                if(crimeList is not null && crimeList.Count > 0) {
                    Crime crime = crimeList[0];
                    if(!String.IsNullOrEmpty(crimeDesc) && crime.Desc != crimeDesc) {
                        //await jsRuntime.InvokeVoidAsync("alert", $"There is already a crime with code {crimeDto.Code} which has description {crimeDto.Desc}");
                        response.Message = $"There is already a crime with code {crime.Code} which has description {crime.Desc}";
                        response.CrimeDto = null;
                        response.Status = CrimeCheckStatus.CrimeFoundDescDifferent;
                        return Ok(response);
                    }
                    response.Message = $"Crime with code {crime.Code} found";
                    response.CrimeDto = new CrimeDto(crime);
                    response.Status = CrimeCheckStatus.CrimeFound;
                    return Ok(response);
                }
                if(String.IsNullOrEmpty(crimeDesc)) {
                    //await jsRuntime.InvokeVoidAsync("alert", $"There is no crime with code {crimeCode}. Plese fill Description or choose another code ");
                    response.Message = $"There is no crime with code {crimeCode}. Plese fill Description or choose another code ";
                    response.CrimeDto = null;
                    response.Status = CrimeCheckStatus.CrimeNotFoundDescEmpty;
                    return Ok(response);
                }

                response.CrimeDto = new CrimeDto() { Code = crimeCode, Desc = crimeDesc };
                response.Message = $"Crime with code {crimeCode} not found. A new crime will be created after submit";
                response.Status = CrimeCheckStatus.CrimeWillBeCreated;
                return Ok(response);
            } catch(Exception ex) {
                return BadRequest(ex.Message);
            }
        }
    }
}
