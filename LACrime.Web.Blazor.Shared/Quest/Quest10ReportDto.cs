using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LACrimes.Web.Blazor.Shared.Quest {
    [JsonSerializable(typeof(Quest10ReportDto))]

    public class Quest10ReportDto {
        public Quest10AreaReportDto? AreaReport { get; set; }
        public Quest10RptDistNoReportDto? RptDistNoReport { get; set; }
    }


    public class Quest10AreaReportDto {
        public string? AreaName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double GapDays { get; set; }
    }

    public class Quest10RptDistNoReportDto {
        public string? RptDistNo { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double GapDays { get; set; }
    }

}
