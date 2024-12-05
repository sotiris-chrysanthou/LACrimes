using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LACrimes.Web.Blazor.Shared.Quest {
    [JsonSerializable(typeof(Quest7ReportDto))]
    public class Quest7ReportDto {
        public String? AreaName { get; set; }
        public String? CrimeCode1 { get; set; }
        public String? Crime1Desc { get; set; }
        public String? CrimeCode2 { get; set; }
        public String? Crime2Desc { get; set; }
        public int CoOccurrences { get; set; }
    }
}
