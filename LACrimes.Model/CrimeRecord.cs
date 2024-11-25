using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LACrimes.Model {
    public class CrimeRecord {
        public CrimeRecord() { }

        public CrimeRecord(String drNo, DateTime dateRptd, DateTime dateOcc, TimeOnly timeOcc,
                           Guid? subAreaID, Guid? victimID,
                           Guid? premisID, Guid? weaponID, Guid? streetID,
                           Guid? crossStreetID, Guid? coordinatesID) {
            ID = Guid.NewGuid();
            DrNo = drNo;
            DateRptd = dateRptd;
            DateOcc = dateOcc;
            TimeOcc = timeOcc;
            SubAreaID = subAreaID;
            VictimID = victimID;
            PremisID = premisID;
            WeaponID = weaponID;
            StreetID = streetID;
            CrossStreetID = crossStreetID;
            CoordinatesID = coordinatesID;
        }

        [Required] public Guid ID { get; set; }
        [Required] public String DrNo { get; set; } = null!;
        [Required] public DateTime DateRptd { get; set; }
        [Required] public DateTime DateOcc { get; set; }
        [Required] public TimeOnly TimeOcc { get; set; }

        public Guid? SubAreaID { get; set; }
        public SubArea SubArea { get; set; } = null!;

        public Guid? VictimID { get; set; }
        public Victim Victim { get; set; } = null!;

        public Guid? PremisID { get; set; }
        public Premis? Premis { get; set; } = null;

        public Guid? StatusID { get; set; }
        public Status Status { get; set; } = null!;

        public Guid? WeaponID { get; set; }
        public Weapon? Weapon { get; set; } = null;

        public Guid? StreetID { get; set; }
        public Street Street { get; set; } = null!;

        public Guid? CrossStreetID { get; set; }
        public Street? CrossStreet { get; set; } = null;

        public Guid? CoordinatesID { get; set; }
        public Coordinates Coordinates { get; set; } = null!;

        [Required]
        public List<CrimeSeverity> CrimeSeverities { get; set; } = null!;
    }
}
