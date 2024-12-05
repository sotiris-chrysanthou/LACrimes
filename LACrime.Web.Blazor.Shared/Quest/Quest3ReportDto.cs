using System.Text.Json.Serialization;

namespace LACrimes.Web.Blazor.Shared.Quest {
    [JsonSerializable(typeof(Quest3ReportDto))]

    public class Quest3ReportDto {
        public String? AreaCode { get; set; } = null;
        public String? AreaName { get; set; } = null;
        public String? CrimeCode { get; set; } = null;
        public String? CrimeDescription { get; set; } = null;
        public int TotalReports { get; set; }
    }
}
