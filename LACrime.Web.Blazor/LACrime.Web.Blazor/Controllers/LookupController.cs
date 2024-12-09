using LACrimes.EF.Context;
using LACrimes.EF.Repository;
using LACrimes.Model;
using LACrimes.Web.Blazor.Shared;
using LACrimes.Web.Blazor.Shared.AreaDtos;
using LACrimes.Web.Blazor.Shared.PremisDtos;
using LACrimes.Web.Blazor.Shared.StatusDtos;
using LACrimes.Web.Blazor.Shared.StreetDtos;
using LACrimes.Web.Blazor.Shared.SubAreaDtos;
using LACrimes.Web.Blazor.Shared.WeaponDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LACrimes.Web.Blazor.Server.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "User,Admin")]
    public class LookupController : ControllerBase {
        private readonly IEntityRepo<SubArea> _subAreaRepo;
        private readonly IEntityRepo<Area> _areaRepo;
        private readonly IEntityRepo<Crime> _crimeRepo;
        private readonly IEntityRepo<Premis> _premisRepo;
        private readonly IEntityRepo<Status> _statusRepo;
        private readonly IEntityRepo<Street> _streetRepo;
        private readonly IEntityRepo<Weapon> _weaponRepo;

        public LookupController(
            IEntityRepo<SubArea> subAreaRepo,
            IEntityRepo<Area> areaRepo,
            IEntityRepo<Crime> crimeRepo,
            IEntityRepo<Premis> premisRepo,
            IEntityRepo<Status> statusRepo,
            IEntityRepo<Street> streetRepo,
            IEntityRepo<Weapon> weaponRepo) {
            _subAreaRepo = subAreaRepo;
            _areaRepo = areaRepo;
            _crimeRepo = crimeRepo;
            _premisRepo = premisRepo;
            _statusRepo = statusRepo;
            _streetRepo = streetRepo;
            _weaponRepo = weaponRepo;
        }

        [HttpGet("SubAreas")]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> GetSubAreas() {
            var subAreas = await _subAreaRepo.GetAll();
            var subAreaDtos = subAreas.Select(sa => new SubAreaDto(sa)).ToList();
            return Ok(subAreaDtos);
        }

        [HttpGet("Areas")]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> GetAreas() {
            var areas = await _areaRepo.GetAll();
            var areaDtos = areas.Select(a => new AreaDto(a)).ToList();
            return Ok(areaDtos);
        }

        [HttpGet("Crimes")]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> GetCrimes() {
            var crimes = await _crimeRepo.GetAll();
            var crimeDtos = crimes.Select(c => new CrimeDto(c)).ToList();
            return Ok(crimeDtos);
        }

        [HttpGet("Premises")]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> GetPremises() {
            var premises = await _premisRepo.GetAll();
            var premisDtos = premises.Select(p => new PremisDto(p)).ToList();
            return Ok(premisDtos);
        }

        [HttpGet("Statuses")]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> GetStatuses() {
            var statuses = await _statusRepo.GetAll();
            var statusDtos = statuses.Select(s => new StatusDto(s)).ToList();
            return Ok(statusDtos);
        }

        [HttpGet("Streets")]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> GetStreets() {
            var streets = await _streetRepo.GetAll();
            var streetDtos = streets.Select(s => new StreetDto(s)).ToList();
            return Ok(streetDtos);
        }

        [HttpGet("Weapons")]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> GetWeapons() {
            var weapons = await _weaponRepo.GetAll();
            var weaponDtos = weapons.Select(w => new WeaponDto(w)).ToList();
            return Ok(weaponDtos);
        }
    }
}
