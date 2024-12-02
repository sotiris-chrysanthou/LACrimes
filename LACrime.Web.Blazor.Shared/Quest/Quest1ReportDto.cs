using System.Text.Json.Serialization;

namespace LACrimes.Web.Blazor.Shared.Quest {
    [JsonSerializable(typeof(Quest1ReportDto))]
    public class Quest1ReportDto {
        public String? CrmCd { get; set; } = null;
        public String? CrimeDescription { get; set; } = null;
        public int TotalReports { get; set; }
    }
}
