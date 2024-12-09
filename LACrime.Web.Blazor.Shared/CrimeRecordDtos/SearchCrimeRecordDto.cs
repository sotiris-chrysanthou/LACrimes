using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LACrimes.Model;
using LACrimes.Web.Blazor.Shared.AreaDtos;
using LACrimes.Web.Blazor.Shared.PremisDtos;
using LACrimes.Web.Blazor.Shared.StatusDtos;
using LACrimes.Web.Blazor.Shared.StreetDtos;
using LACrimes.Web.Blazor.Shared.SubAreaDtos;
using LACrimes.Web.Blazor.Shared.VictimDtos;
using LACrimes.Web.Blazor.Shared.WeaponDtos;

namespace LACrimes.Web.Blazor.Shared.CrimeRecordDtos {
    public class SearchCrimeRecordDto {
        public Guid ID { get; set; }
        public string DrNo { get; set; } = null!;
        public DateTime DateRptd { get; set; }
        public DateTime DateOcc { get; set; }
        public TimeOnly TimeOcc { get; set; }
        public SubAreaDto? SubArea { get; set; }
        public AreaDto? Area { get; set; }
        public VictimDto? Victim { get; set; }
        public PremisDto? Premis { get; set; }
        public StatusDto? Status { get; set; }
        public WeaponDto? Weapon { get; set; }
        public StreetDto? Street { get; set; }
        public StreetDto? CrossStreet { get; set; }
        public double? Lat { get; set; }
        public double? Lon { get; set; }
        public List<CrimeDto> Crimes { get; set; } = new();
        public List<int> Severities { get; set; } = new();

        public SearchCrimeRecordDto() { }

        public SearchCrimeRecordDto(CrimeRecord crimeRecord) {
            ID = crimeRecord.ID;
            DrNo = crimeRecord.DrNo;
            DateRptd = crimeRecord.DateRptd;
            DateOcc = crimeRecord.DateOcc;
            TimeOcc = crimeRecord.TimeOcc;
            SubArea = crimeRecord.SubArea != null ? new SubAreaDto(crimeRecord.SubArea) : null;
            Area = crimeRecord.SubArea?.Area != null ? new AreaDto(crimeRecord.SubArea.Area) : null;
            Victim = crimeRecord.Victim != null ? new VictimDto(crimeRecord.Victim) : null;
            Premis = crimeRecord.Premis != null ? new PremisDto(crimeRecord.Premis) : null;
            Status = crimeRecord.Status != null ? new StatusDto(crimeRecord.Status) : null;
            Weapon = crimeRecord.Weapon != null ? new WeaponDto(crimeRecord.Weapon) : null;
            Street = crimeRecord.Street != null ? new StreetDto(crimeRecord.Street) : null;
            CrossStreet = crimeRecord.CrossStreet != null ? new StreetDto(crimeRecord.CrossStreet) : null;
            Lat = crimeRecord.Coordinates?.Lat;
            Lon = crimeRecord.Coordinates?.Lon;

            if(crimeRecord.CrimeSeverities != null) {
                foreach(var cs in crimeRecord.CrimeSeverities) {
                    if(cs.Crime != null) {
                        Crimes.Add(new CrimeDto(cs.Crime));
                        Severities.Add(cs.Severity);
                    }
                }
            }
        }
    }
}
