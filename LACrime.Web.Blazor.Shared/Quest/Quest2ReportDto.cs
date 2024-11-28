using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LACrimes.Web.Blazor.Shared.Quest {
    [JsonSerializable(typeof(Quest2ReportDto))]
    public class Quest2ReportDto {
        public DateTime ReportDate { get; set; }
        public int TotalReports { get; set; }
    }
}
