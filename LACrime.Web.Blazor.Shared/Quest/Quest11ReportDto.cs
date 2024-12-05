using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LACrimes.Web.Blazor.Shared.Quest {
    [JsonSerializable(typeof(Quest11ReportDto))]
    public class Quest11ReportDto {
        public string? AreaName { get; set; }
        public DateTime CrimeDate { get; set; }
        public string? Crime1Code { get; set; }
        public string? Crime2Code { get; set; }
        public int CrimesCount { get; set; }
    }
}
