using System.Text.Json.Serialization;

namespace LACrimes.Web.Blazor.Shared.Quest {
    [JsonSerializable(typeof(Quest2ReportDto))]
    public class Quest5ReportDto {
        public string? CrmCd { get; set; }
        public int TotalReports { get; set; }
    }
}
