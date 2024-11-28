using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LACrimes.Web.Blazor.Shared.Quest {
    [JsonSerializable(typeof(Quest1ReportDto))]
    public class Quest1ReportDto {
        public string? CrmCd { get; set; } = null;
        public string? CrimeDescription { get; set; } = null;
        public int TotalReports { get; set; }
    }
}
