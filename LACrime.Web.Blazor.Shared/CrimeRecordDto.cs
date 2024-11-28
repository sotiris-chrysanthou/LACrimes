using System.ComponentModel.DataAnnotations;
using LACrimes.Model;

namespace LACrimes.Web.Blazor.Shared {
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
        [Required] public String DrNo { get; set; } = null!;
        [Required] public DateTime DateRptd { get; set; }
        [Required] public DateTime DateOcc { get; set; }
        [Required] public TimeOnly TimeOcc { get; set; }

        #region Relations
        public List<CrimeSeverityDto> CrimeSeverities { get; set; } = null!;
        public Guid? AreaID { get; set; }
        public String AreaCode { get; set; } = null!;
        public String AreaName { get; set; } = null!;
        public Guid? SubAreaID { get; set; }
        public String RpdDistNo { get; set; } = null!;

        public Guid? VictimID { get; set; }
        public int VictAge { get; set; }
        [StringLength(1)] public String? VictSex { get; set; } = null!;
        [StringLength(1)] public String? VictimDescent { get; set; } = null!;

        public Guid? PremisID { get; set; }
        public int? PremisCode { get; set; }
        public String? PremisDesc { get; set; } = null!;

        public Guid? StatusID { get; set; }
        public String StatusCode { get; set; } = null!;
        public String StatusDesc { get; set; } = null!;

        public Guid? WeaponID { get; set; }
        public int? WeaponCode { get; set; }
        public String? WeaponDesc { get; set; } = null!;

        public Guid? StreetID { get; set; }
        public String StreetName { get; set; } = null!;
        public Guid? CrossStreetID { get; set; }
        public String? CrossStreetName { get; set; } = null!;

        public Guid? CoordinatesID { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
        #endregion


    }
}