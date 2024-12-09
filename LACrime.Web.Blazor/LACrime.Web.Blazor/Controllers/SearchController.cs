using BlazorBootstrap;
using LACrimes.EF.Repository;
using LACrimes.Model;
using LACrimes.Web.Blazor.Shared.CrimeRecordDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace LACrimes.Web.Blazor.Server.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase {
        private readonly IEntityRepo<CrimeRecord> _crimeRecordRepo;

        public SearchController(IEntityRepo<CrimeRecord> crimeRecordRepo) {
            _crimeRecordRepo = crimeRecordRepo;
        }

        [HttpGet("Search")]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> Search(
            [FromQuery] string? drno,
            [FromQuery] DateTime? daterptd,
            [FromQuery] DateTime? dateocc,
            [FromQuery] string? subAreaRpdDistNo,
            [FromQuery] string? areaCode,
            [FromQuery] string? areaName,
            [FromQuery] double? lat,
            [FromQuery] double? lon,
            [FromQuery] string? crimeDescription,
            [FromQuery] string? premisDescription,
            [FromQuery] string? statusDescription,
            [FromQuery] string? streetName,
            [FromQuery] string? crossStreetName,
            [FromQuery] int? victimAge,
            [FromQuery] string? victimSex,
            [FromQuery] string? victimDescent,
            [FromQuery] string? weaponDescription) {

            Expression<Func<CrimeRecord, bool>> predicate = crmR => true;

            if(!string.IsNullOrEmpty(drno)) {
                predicate = predicate.And(crmR => crmR.DrNo.Contains(drno));
            }

            if(daterptd.HasValue) {
                predicate = predicate.And(crmR => crmR.DateRptd.Date == daterptd.Value.Date);
            }

            if(dateocc.HasValue) {
                predicate = predicate.And(crmR => crmR.DateOcc.Date == dateocc.Value.Date);
            }

            if(!string.IsNullOrEmpty(subAreaRpdDistNo)) {
                predicate = predicate.And(crmR => crmR.SubArea.RpdDistNo.Contains(subAreaRpdDistNo));
            }

            if(!string.IsNullOrEmpty(areaCode)) {
                predicate = predicate.And(crmR => crmR.SubArea.Area.Code.Contains(areaCode));
            }

            if(!string.IsNullOrEmpty(areaName)) {
                predicate = predicate.And(crmR => crmR.SubArea.Area.Name.Contains(areaName));
            }

            if(lat.HasValue) {
                predicate = predicate.And(crmR => crmR.Coordinates.Lat == lat.Value);
            }

            if(lon.HasValue) {
                predicate = predicate.And(crmR => crmR.Coordinates.Lon == lon.Value);
            }

            if(!string.IsNullOrEmpty(crimeDescription)) {
                predicate = predicate.And(crmR => crmR.CrimeSeverities.Any(cs => cs.Crime.Desc.Contains(crimeDescription)));
            }

            if(!string.IsNullOrEmpty(premisDescription)) {

                predicate = predicate.And(crmR => crmR.Premis != null && crmR.Premis.Desc != null && crmR.Premis.Desc.Contains(premisDescription));
            }

            if(!string.IsNullOrEmpty(statusDescription)) {
                predicate = predicate.And(crmR => crmR.Status.Desc.Contains(statusDescription));
            }

            if(!string.IsNullOrEmpty(streetName)) {
                predicate = predicate.And(crmR => crmR.Street.Name.Contains(streetName));
            }

            if(!string.IsNullOrEmpty(crossStreetName)) {
                predicate = predicate.And(crmR => crmR.CrossStreet != null && crmR.CrossStreet.Name.Contains(crossStreetName)); ;
            }

            if(victimAge.HasValue) {
                predicate = predicate.And(crmR => crmR.Victim.Age == victimAge.Value);
            }

            if(!string.IsNullOrEmpty(victimSex)) {
                predicate = predicate.And(crmR => crmR.Victim.Sex != null && crmR.Victim.Sex.Contains(victimSex));
            }

            if(!string.IsNullOrEmpty(victimDescent)) {
                predicate = predicate.And(crmR => crmR.Victim.Descent != null && crmR.Victim.Descent.Contains(victimDescent));
            }

            if(!string.IsNullOrEmpty(weaponDescription)) {
                predicate = predicate.And(crmR => crmR.Weapon != null && crmR.Weapon.Desc != null && crmR.Weapon.Desc.Contains(weaponDescription));
            }

            var crimeRecords = await _crimeRecordRepo.GetAll(predicate, IncludeAll: true);

            var result = crimeRecords.Select(cr => new SearchCrimeRecordDto(cr)).ToList();

            return Ok(result);
        }
    }
}

