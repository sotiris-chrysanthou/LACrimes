using System.Text.Json.Serialization;

namespace LACrimes.Web.Blazor.Shared.Quest {
    [JsonSerializable(typeof(Quest2ReportDto))]
    public class Quest4ReportDto {
        public int Hour { get; set; }
        public double AverageCrimes { get; set; }
    }
}
