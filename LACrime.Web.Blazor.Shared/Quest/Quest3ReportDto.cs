using System.Text.Json.Serialization;

namespace LACrimes.Web.Blazor.Shared.Quest {
    [JsonSerializable(typeof(Quest2ReportDto))]

    public class Quest3ReportDto {
        public string? AreaCode { get; set; } = null;
        public string? AreaName { get; set; } = null;
        public string? CrimeCode { get; set; } = null;
        public string? CrimeDescription { get; set; } = null;
        public int TotalReports { get; set; }
    }
}
