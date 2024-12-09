using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LACrimes.Web.Blazor.Shared.Quest {
    [JsonSerializable(typeof(Quest13ReportDto))]
    public class Quest13ReportDto {
        public DateTime CrimeDate { get; set; }
        public string AreaName { get; set; } = string.Empty;
        public int WeaponCode { get; set; }
        public string WeaponDesc { get; set; } = string.Empty;
        public int CrimeCode { get; set; }
        public string CrimeDesc { get; set; } = string.Empty;
        public string ListOfDrNo { get; set; } = string.Empty;
    }
}
