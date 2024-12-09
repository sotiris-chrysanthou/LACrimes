using System.ComponentModel.DataAnnotations;
using LACrimes.Model;

namespace LACrimes.Web.Blazor.Shared.CrimeRecordDtos {
    public class CrimeRecordDto {
        public CrimeRecordDto(CrimeRecord crmR) {
            ID = crmR.ID;
            DrNo = crmR.DrNo;
            DateRptd = crmR.DateRptd;
            DateOcc = crmR.DateOcc;
            TimeOcc = crmR.TimeOcc;
            foreach(var crmSev in crmR.CrimeSeverities) {
                CrimeSeverities.Add(new CrimeSeverityDto(crmSev));
            }
            AreaCode = crmR.SubArea.Area.Code;
            AreaName = crmR.SubArea.Area.Name;
            RpdDistNo = crmR.SubArea.RpdDistNo;
            VictAge = crmR.Victim.Age;
            VictSex = crmR.Victim.Sex?.ToString();
            VictimDescent = crmR.Victim.Descent?.ToString();
            PremisCode = crmR.Premis?.Code;
            PremisDesc = crmR.Premis?.Desc;
            StatusCode = crmR.Status.Code;
            StatusDesc = crmR.Status.Desc;
            WeaponCode = crmR.Weapon?.Code;
            WeaponDesc = crmR.Weapon?.Desc;
            StreetName = crmR.Street.Name;
            CrossStreetName = crmR.CrossStreet?.Name;
            Lat = crmR.Coordinates.Lat;
            Lon = crmR.Coordinates.Lon;
        }

        public CrimeRecordDto() {

        }

        [Required] public Guid? ID { get; set; }
        [Required] public string DrNo { get; set; } = null!;
        [Required] public DateTime DateRptd { get; set; }
        [Required] public DateTime DateOcc { get; set; }
        [Required] public TimeOnly TimeOcc { get; set; }

        #region Relations
        public List<CrimeSeverityDto> CrimeSeverities { get; set; } = new List<CrimeSeverityDto>();
        public Guid? AreaID { get; set; }
        public string AreaCode { get; set; } = null!;
        public string AreaName { get; set; } = null!;
        public Guid? SubAreaID { get; set; }
        public string RpdDistNo { get; set; } = null!;

        public Guid? VictimID { get; set; }
        public int VictAge { get; set; }
        [StringLength(1)] public string? VictSex { get; set; } = null!;
        [StringLength(1)] public string? VictimDescent { get; set; } = null!;

        public Guid? PremisID { get; set; }
        public int? PremisCode { get; set; }
        public string? PremisDesc { get; set; } = null!;

        public Guid? StatusID { get; set; }
        public string StatusCode { get; set; } = null!;
        public string StatusDesc { get; set; } = null!;

        public Guid? WeaponID { get; set; }
        public int? WeaponCode { get; set; }
        public string? WeaponDesc { get; set; } = null!;

        public Guid? StreetID { get; set; }
        public string StreetName { get; set; } = null!;
        public Guid? CrossStreetID { get; set; }
        public string? CrossStreetName { get; set; } = null!;

        public Guid? CoordinatesID { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
        #endregion


    }
}